// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImeSupport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class ImeSupport : IDisposable
  {
    private string previousCompString = "";
    private readonly RadTextBoxControlElement textBoxControl;
    private Control wiredControl;
    private NativeWindowAdapter immSource;
    private IntPtr imeContext;
    private IntPtr imeOldContext;
    private IntPtr imeDefaultWnd;
    private bool shouldRepositionCaret;
    private bool imeActive;
    private int startCaretIndex;
    private Point startCaretLocation;

    public ImeSupport(RadTextBoxControlElement textBox)
    {
      this.textBoxControl = textBox;
      this.imeDefaultWnd = IntPtr.Zero;
      this.WireEvents();
    }

    private void WireEvents()
    {
      this.wiredControl = this.textBoxControl.ElementTree.Control;
      this.wiredControl.GotFocus += new EventHandler(this.OnGotKeyboardFocus);
      this.wiredControl.LostFocus += new EventHandler(this.OnLostKeyboardFocus);
    }

    private void UnwireEvents()
    {
      this.wiredControl.GotFocus -= new EventHandler(this.OnGotKeyboardFocus);
      this.wiredControl.LostFocus -= new EventHandler(this.OnLostKeyboardFocus);
      this.wiredControl = (Control) null;
    }

    internal void OnGotKeyboardFocus(object sender, EventArgs e)
    {
      this.CreateContext();
    }

    internal void OnLostKeyboardFocus(object sender, EventArgs e)
    {
      this.CloseIme();
      this.ClearContext();
    }

    private void CloseIme()
    {
      if (!(this.imeContext != IntPtr.Zero))
        return;
      ImeHelper.NotifyCompositionStringCancel(this.imeContext);
    }

    private void CreateContext()
    {
      if (this.textBoxControl.ElementTree == null || this.textBoxControl.ElementTree.Control == null || (this.textBoxControl.IsDisposed || this.textBoxControl.ElementTree.Control.IsDisposed))
        return;
      if (this.immSource != null && this.immSource.Handle != this.textBoxControl.ElementTree.Control.Handle)
        this.immSource.Dispose();
      this.immSource = new NativeWindowAdapter(this.textBoxControl.ElementTree.Control.Handle);
      if (this.immSource == null)
        return;
      this.imeDefaultWnd = ImeHelper.GetDefaultIMEWnd();
      this.imeContext = ImeHelper.GetContext(this.imeDefaultWnd);
      this.imeOldContext = ImeHelper.AssociateContext((NativeWindow) this.immSource, this.imeContext);
      this.immSource.AddHook(new WndHookDelegate(this.WndProc));
      ImeHelper.EnableImmComposition();
    }

    private IntPtr WndProc(
      IntPtr hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam,
      ref bool handled,
      ref bool defWndProc)
    {
      switch (msg)
      {
        case 269:
          this.StartComposition();
          break;
        case 270:
          this.EndComposition();
          break;
        case 271:
          if (this.IsKorean())
          {
            if ((int) lParam == 2048)
            {
              handled = !this.imeActive;
              this.shouldRepositionCaret = true;
              this.textBoxControl.OnIMECompositionResult(ImeHelper.GetImmCompositionString(this.imeContext, 2048));
              break;
            }
            if (this.shouldRepositionCaret)
            {
              this.shouldRepositionCaret = false;
              this.startCaretIndex = this.textBoxControl.CaretIndex;
              this.previousCompString = string.Empty;
              this.startCaretLocation = new Point(this.textBoxControl.Caret.ControlBoundingRectangle.Left, this.textBoxControl.ViewElement.ControlBoundingRectangle.Top);
              this.UpdateCompositionWindowLocation(this.startCaretLocation);
            }
          }
          this.UpdateCompositionWindow();
          break;
      }
      return IntPtr.Zero;
    }

    private bool IsKorean()
    {
      return InputLanguage.CurrentInputLanguage.Culture.LCID == 1042;
    }

    private void StartComposition()
    {
      if (this.imeActive)
        return;
      this.imeActive = true;
      this.startCaretIndex = this.textBoxControl.CaretIndex;
      if (this.textBoxControl.SelectionLength != 0)
        this.textBoxControl.Delete();
      this.previousCompString = string.Empty;
      this.startCaretLocation = new Point(this.textBoxControl.Caret.ControlBoundingRectangle.Left, this.textBoxControl.ViewElement.ControlBoundingRectangle.Top);
      this.textBoxControl.OnIMECompositionStarted();
    }

    private void EndComposition()
    {
      if (!this.imeActive)
        return;
      this.imeActive = false;
      this.textBoxControl.OnIMECompositionEnded();
    }

    private void ClearContext()
    {
      if (this.immSource == null)
        return;
      this.immSource.RemoveHook(new WndHookDelegate(this.WndProc));
      ImeHelper.AssociateContext((NativeWindow) this.immSource, this.imeOldContext);
      this.imeOldContext = IntPtr.Zero;
      ImeHelper.ReleaseContext(this.imeDefaultWnd, this.imeContext);
      this.immSource.Dispose();
      this.immSource = (NativeWindowAdapter) null;
      this.imeContext = IntPtr.Zero;
      this.imeDefaultWnd = IntPtr.Zero;
    }

    private void UpdateCompositionWindow()
    {
      string compositionString = ImeHelper.GetImmCompositionString(this.imeContext, 8);
      this.textBoxControl.Select(this.startCaretIndex, this.previousCompString.Length);
      this.textBoxControl.Insert(compositionString);
      this.previousCompString = compositionString;
      this.textBoxControl.Select(this.startCaretIndex, this.previousCompString.Length);
      TextPosition positionFromOffset = this.textBoxControl.Navigator.GetPositionFromOffset(this.startCaretIndex);
      TextPosition textPosition = this.textBoxControl.Navigator.GetPositionFromOffset(this.startCaretIndex + 1);
      if ((object) textPosition == null)
        textPosition = positionFromOffset;
      TextPosition position = textPosition;
      PointF location1 = this.textBoxControl.ViewElement.GetLocation(positionFromOffset);
      PointF location2 = this.textBoxControl.ViewElement.GetLocation(position);
      if ((double) location2.Y > (double) location1.Y)
      {
        location1.X = position.Line.Location.X;
        location1.Y = location2.Y;
      }
      int num = this.textBoxControl.ViewElement.Lines.Count > 0 ? (int) this.textBoxControl.ViewElement.Lines[0].Location.Y - this.textBoxControl.ViewElement.ControlBoundingRectangle.Y : 0;
      this.startCaretLocation = new Point((int) ((double) location1.X + (double) this.textBoxControl.ViewElement.ScrollOffset.Width), (int) ((double) location1.Y + (double) this.textBoxControl.ViewElement.ScrollOffset.Height) - num);
      this.UpdateCompositionWindowLocation(this.startCaretLocation);
    }

    private void UpdateCompositionWindowLocation(Point point)
    {
      if (!(this.imeContext != IntPtr.Zero))
        return;
      ImeHelper.SetCompositionWindow(this.imeContext, this.textBoxControl, point);
    }

    public void Dispose()
    {
      this.UnwireEvents();
      this.ClearContext();
    }
  }
}
