// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadToolTip
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class RadToolTip : ToolTip
  {
    private readonly Timer initialDelayTimer = new Timer();
    public const int Padding = 10;
    public const int VerticalSpacingToCursor = 30;
    private Control toolTipWindowControl;
    private readonly RadToolTip.ShowToolTipDelegate ShowTooltip;
    private readonly IntPtr handle;
    private string currentText;
    private int currentDuration;
    private bool isDisposed;
    private bool checkRootElement;
    private Font tooltipFont;
    private FieldInfo drawToolTipEventArgsToolTipTextField;
    private RootRadElement associatedRootElement;

    public RadToolTip()
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
      System.Type type = typeof (ToolTip);
      this.ShowTooltip = (RadToolTip.ShowToolTipDelegate) Delegate.CreateDelegate(typeof (RadToolTip.ShowToolTipDelegate), (object) this, type.GetMethod(nameof (ShowTooltip), bindingAttr));
      this.handle = (IntPtr) type.GetProperty("Handle", bindingAttr).GetValue((object) this, (object[]) null);
      this.drawToolTipEventArgsToolTipTextField = typeof (DrawToolTipEventArgs).GetField("toolTipText", bindingAttr);
      this.initialDelayTimer.Tick += new EventHandler(this.InitialDelayTimer_Tick);
      this.Draw += new DrawToolTipEventHandler(this.RadToolTip_Draw);
    }

    public RadToolTip(RootRadElement root)
      : this()
    {
      this.associatedRootElement = root;
      this.checkRootElement = !(root is IFormRootElement);
    }

    public RadToolTip(IContainer container)
      : base(container)
    {
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      this.isDisposed = true;
      this.associatedRootElement = (RootRadElement) null;
      this.Draw -= new DrawToolTipEventHandler(this.RadToolTip_Draw);
      this.initialDelayTimer.Stop();
      this.initialDelayTimer.Tick -= new EventHandler(this.InitialDelayTimer_Tick);
      this.initialDelayTimer.Dispose();
    }

    public Font ToolTipFont
    {
      get
      {
        if (this.tooltipFont == null)
        {
          try
          {
            this.tooltipFont = Font.FromHfont(NativeMethods.SendMessage(new HandleRef((object) this, this.handle), 49, 0, 0));
          }
          catch (ArgumentException ex)
          {
            this.tooltipFont = Control.DefaultFont;
          }
        }
        return this.tooltipFont;
      }
    }

    public bool IsDisposed
    {
      get
      {
        return this.isDisposed;
      }
    }

    public void Show(string text)
    {
      this.Show(text, 0);
    }

    public void Show(string text, int duration)
    {
      this.Show(text, Cursor.Position, duration);
    }

    public void Show(string text, Point point)
    {
      this.Show(text, point, 0);
    }

    public void Show(string text, int x, int y)
    {
      this.Show(text, new Point(x, y), 0);
    }

    public void Show(string text, int x, int y, int duration)
    {
      this.Show(text, new Point(x, y), duration);
    }

    public void Show(string text, Point point, int duration)
    {
      if (duration < 0)
        throw new ArgumentException(string.Format("The duration of the RadToolTip cannot be set to {0}, value must be equal to or greater than 0", (object) duration));
      this.toolTipWindowControl = new Control();
      this.toolTipWindowControl.Location = this.CorrectShowPoint(point, text);
      this.SetToolTip(this.toolTipWindowControl, string.Empty);
      int num = this.InitialDelay - MouseHoverTimer.MouseOverTimerInterval;
      if (num <= 0)
      {
        this.ShowToolTipSafe(text, this.toolTipWindowControl, duration);
      }
      else
      {
        this.initialDelayTimer.Interval = num;
        this.currentText = text;
        this.currentDuration = duration;
        this.initialDelayTimer.Start();
      }
    }

    public void Hide()
    {
      if (this.toolTipWindowControl == null || this.toolTipWindowControl.IsDisposed)
        return;
      this.Hide((IWin32Window) this.toolTipWindowControl);
      this.toolTipWindowControl.Dispose();
      this.toolTipWindowControl = (Control) null;
    }

    protected virtual Point CorrectShowPoint(Point point, string text)
    {
      Size accordingToScreens = this.CalculateOffsetAccordingToScreens(point, text);
      if (this.IsBalloon)
        accordingToScreens.Height -= 30;
      point = new Point(point.X + accordingToScreens.Width, point.Y + accordingToScreens.Height);
      return point;
    }

    private Size CalculateOffsetAccordingToScreens(Point mousePosition, string toolTipText)
    {
      RadToolTip radToolTip = this;
      Size empty = Size.Empty;
      Size size = TextRenderer.MeasureText(toolTipText, radToolTip.ToolTipFont);
      Point point = new Point(mousePosition.X + size.Width, mousePosition.Y + size.Height);
      Screen screen = Screen.FromPoint(mousePosition);
      int height = screen.WorkingArea.Height;
      int width = screen.WorkingArea.Width;
      if (point.X >= width + screen.WorkingArea.X)
      {
        int num1 = this.IsBalloon ? 4 : 2;
        int num2 = screen.WorkingArea.X + width - point.X - 10 * num1;
        empty.Width += num2;
      }
      if (point.Y + 30 >= height + screen.WorkingArea.Y)
      {
        int num = screen.WorkingArea.Y + height - point.Y - 30 - 10;
        empty.Height += num;
      }
      return empty;
    }

    private void ShowToolTipSafe(string text, Control toolTipWindowControl, int duration)
    {
      if (this.IsDisposed || this.checkRootElement && (this.associatedRootElement == null || this.associatedRootElement.IsDisposing || this.associatedRootElement.IsDisposed || !this.associatedRootElement.IsMouseOver && !this.associatedRootElement.IsMouseOverElement && !this.associatedRootElement.ContainsMouse))
        return;
      if (toolTipWindowControl == null || toolTipWindowControl.IsDisposed)
        toolTipWindowControl = new Control();
      this.ShowTooltip(text, (IWin32Window) toolTipWindowControl, duration);
    }

    private void InitialDelayTimer_Tick(object sender, EventArgs e)
    {
      this.initialDelayTimer.Stop();
      this.ShowToolTipSafe(this.currentText, this.toolTipWindowControl, this.currentDuration);
    }

    private void RadToolTip_Draw(object sender, DrawToolTipEventArgs e)
    {
      this.drawToolTipEventArgsToolTipTextField.SetValue((object) e, (object) this.currentText);
    }

    private delegate void ShowToolTipDelegate(string text, IWin32Window window, int duration);
  }
}
