// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonFormBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadRibbonFormBehavior : ThemedFormBehavior
  {
    private bool allowTheming = true;
    private DateTime clickTimeStamp = DateTime.MinValue;
    private const int RegionHeightEllipse = 9;
    private const int MaxFormBorderHeight = 4;
    private const int WM_GETICON = 127;
    private RadRibbonFormBehavior.DwmExtendFrameIntoClientAreaDelegate DwmExtendFrameIntoClientArea;
    private RadRibbonFormBehavior.DwmDefWindowProcDelegate DwmDefWindowProc;
    private RadRibbonBar radRibbonBar;
    private RibbonFormElement formElement;
    private bool compositionEnabled;
    private FormWindowState oldWindowState;
    private List<object> oldRadRibbonBarElementSettings;
    private bool isMouseTracking;
    private RadElement capturedNCItem;
    private bool stateChanged1;
    private bool stateChanged2;

    public RadRibbonFormBehavior()
    {
      DllWrapper dllWrapper = new DllWrapper("dwmapi.dll");
      this.DwmExtendFrameIntoClientArea = (RadRibbonFormBehavior.DwmExtendFrameIntoClientAreaDelegate) dllWrapper.GetFunctionAsDelegate(nameof (DwmExtendFrameIntoClientArea), typeof (RadRibbonFormBehavior.DwmExtendFrameIntoClientAreaDelegate));
      this.DwmDefWindowProc = (RadRibbonFormBehavior.DwmDefWindowProcDelegate) dllWrapper.GetFunctionAsDelegate(nameof (DwmDefWindowProc), typeof (RadRibbonFormBehavior.DwmDefWindowProcDelegate));
    }

    public RadRibbonFormBehavior(IComponentTreeHandler treeHandler)
      : base(treeHandler)
    {
      DllWrapper dllWrapper = new DllWrapper("dwmapi.dll");
      this.DwmExtendFrameIntoClientArea = (RadRibbonFormBehavior.DwmExtendFrameIntoClientAreaDelegate) dllWrapper.GetFunctionAsDelegate(nameof (DwmExtendFrameIntoClientArea), typeof (RadRibbonFormBehavior.DwmExtendFrameIntoClientAreaDelegate));
      this.DwmDefWindowProc = (RadRibbonFormBehavior.DwmDefWindowProcDelegate) dllWrapper.GetFunctionAsDelegate(nameof (DwmDefWindowProc), typeof (RadRibbonFormBehavior.DwmDefWindowProcDelegate));
    }

    public RadRibbonFormBehavior(
      IComponentTreeHandler treeHandler,
      bool shouldHandleCreateChildItems)
      : base(treeHandler, shouldHandleCreateChildItems)
    {
      DllWrapper dllWrapper = new DllWrapper("dwmapi.dll");
      this.DwmExtendFrameIntoClientArea = (RadRibbonFormBehavior.DwmExtendFrameIntoClientAreaDelegate) dllWrapper.GetFunctionAsDelegate(nameof (DwmExtendFrameIntoClientArea), typeof (RadRibbonFormBehavior.DwmExtendFrameIntoClientAreaDelegate));
      this.DwmDefWindowProc = (RadRibbonFormBehavior.DwmDefWindowProcDelegate) dllWrapper.GetFunctionAsDelegate(nameof (DwmDefWindowProc), typeof (RadRibbonFormBehavior.DwmDefWindowProcDelegate));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.Form.SizeChanged -= new EventHandler(this.Form_SizeChanged);
        this.Form.Load -= new EventHandler(this.Form_Load);
        if (this.Form.MainMenuStrip is RadRibbonFormMainMenuStrip)
        {
          this.Form.MainMenuStrip.Dispose();
          this.Form.MainMenuStrip = (MenuStrip) null;
        }
      }
      base.Dispose(disposing);
    }

    public override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.formElement = new RibbonFormElement();
      parent.Children.Add((RadElement) this.formElement);
    }

    public override CreateParams CreateParams(CreateParams parameters)
    {
      CreateParams createParams = base.CreateParams(parameters);
      if (!this.CompositionEnabled)
        createParams.Style &= -12582913;
      return createParams;
    }

    protected virtual RadRibbonBar RibbonBar
    {
      get
      {
        if (this.radRibbonBar != null && object.ReferenceEquals((object) this.Form, (object) this.radRibbonBar.Parent) && (!this.radRibbonBar.Disposing && !this.radRibbonBar.IsDisposed))
          return this.radRibbonBar;
        this.radRibbonBar = (RadRibbonBar) null;
        foreach (Control control in (ArrangedElementCollection) this.Form.Controls)
        {
          if (control is RadRibbonBar)
            return this.radRibbonBar = control as RadRibbonBar;
        }
        return (RadRibbonBar) null;
      }
    }

    public override RadElement FormElement
    {
      get
      {
        return (RadElement) this.formElement;
      }
    }

    public override Padding BorderWidth
    {
      get
      {
        return (this.FormElement as RibbonFormElement).BorderThickness;
      }
    }

    public override Padding ClientMargin
    {
      get
      {
        if (this.Form.IsMdiChild && this.IsMinimized)
          return new Padding(this.BorderWidth.Left, this.Form.Size.Height, this.BorderWidth.Right, this.BorderWidth.Bottom);
        if (!this.CompositionEffectsEnabled || this.Form.IsDesignMode)
          return new Padding(this.BorderWidth.Left, this.BorderWidth.Top, this.BorderWidth.Right, this.IsMaximized ? 9 : this.BorderWidth.Bottom);
        return new Padding(SystemInformation.FrameBorderSize.Height, 0, SystemInformation.FrameBorderSize.Height, this.IsMaximized ? 9 : SystemInformation.FrameBorderSize.Height);
      }
    }

    public override int CaptionHeight
    {
      get
      {
        return 0;
      }
    }

    public int TopCompositionMargin
    {
      get
      {
        if (!this.IsMaximized)
          return SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height;
        return SystemInformation.CaptionHeight + 2 * SystemInformation.FrameBorderSize.Height;
      }
    }

    public bool CompositionEffectsEnabled
    {
      get
      {
        if (this.CompositionEnabled)
          return this.AllowTheming;
        return false;
      }
    }

    private bool CompositionEnabled
    {
      get
      {
        return this.compositionEnabled;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AllowTheming
    {
      get
      {
        return this.allowTheming;
      }
      set
      {
        this.allowTheming = value;
        if (this.allowTheming)
        {
          if (this.CompositionEnabled)
            this.UpdateFormForThemingState(true);
        }
        else if (this.CompositionEnabled)
          this.UpdateFormForThemingState(false);
        this.InitializeRibbon();
      }
    }

    public override Rectangle MenuBounds
    {
      get
      {
        return Rectangle.Empty;
      }
    }

    public override Rectangle CaptionTextBounds
    {
      get
      {
        if (this.RibbonBar != null)
          return this.RibbonBar.RibbonBarElement.RibbonCaption.CaptionLayout.CaptionTextElement.ControlBoundingRectangle;
        return Rectangle.Empty;
      }
    }

    public override Rectangle IconBounds
    {
      get
      {
        return Rectangle.Empty;
      }
    }

    public override Rectangle SystemButtonsBounds
    {
      get
      {
        if (this.RibbonBar != null)
          return this.RibbonBar.RibbonBarElement.RibbonCaption.SystemButtons.ControlBoundingRectangle;
        return Rectangle.Empty;
      }
    }

    public override bool HandleWndProc(ref Message m)
    {
      if (this.allowTheming && this.Form.IsMdiChild)
        return false;
      if (m.Msg == 125 && !this.AllowTheming)
        this.AdjustSystemButtonsForStyle();
      else if (m.Msg == 279)
      {
        this.OnContextMenuOpening(ref m);
      }
      else
      {
        if (m.Msg == 160)
        {
          this.OnWMNCMouseMove(ref m);
          return true;
        }
        if (m.Msg == 674)
        {
          this.OnWMNCMouseLeave(ref m);
          return base.HandleWndProc(ref m);
        }
        if (m.Msg == 163)
        {
          this.OnWMNCLButtonDblClk(ref m);
          return true;
        }
        if (m.Msg == 165)
        {
          this.OnWMNCRightButtonUp(ref m);
          return true;
        }
      }
      if (m.Msg == 798)
      {
        this.OnWMDWMCompositionChanged(ref m);
        return true;
      }
      if (m.Msg == 6)
      {
        this.OnWMActivate(ref m);
        return true;
      }
      if (m.Msg == 546)
      {
        this.OnWmMDIActivate(ref m);
        return true;
      }
      if (m.Msg == 128 && this.RibbonBar != null)
      {
        this.OnWmSetIcon(ref m);
        return true;
      }
      if (m.Msg == 161 && this.RibbonBar != null)
      {
        Point point = new Point();
        int int32 = m.LParam.ToInt32();
        point.X = (int) Telerik.WinControls.NativeMethods.LoWord(int32);
        point.Y = (int) Telerik.WinControls.NativeMethods.HiWord(int32);
        point = this.GetMappedWindowPoint(point);
        point.X -= this.ClientMargin.Left;
        point.Y -= this.ClientMargin.Top;
        if (this.RibbonBar.RibbonBarElement.IconPrimitive.ControlBoundingRectangle.Contains(point))
          return true;
        this.OnWmNCLeftMouseButtonDown(ref m);
        return true;
      }
      if (m.Msg == 162 && this.RibbonBar != null)
      {
        this.OnWmNCLeftMouseButtonUp(ref m);
        return true;
      }
      if (this.CompositionEffectsEnabled && !this.Form.IsDesignMode)
      {
        switch (m.Msg)
        {
          case 131:
            this.UpdateMDIButtonsAndRibbonCaptionText();
            return base.HandleWndProc(ref m);
          case 132:
            return this.OnWMHitTest(ref m);
          default:
            return false;
        }
      }
      else
      {
        if (m.Msg == 131)
          this.UpdateMDIButtonsAndRibbonCaptionText();
        return base.HandleWndProc(ref m);
      }
    }

    private void OnIconVisibilityChanged()
    {
      RadForm control = this.formElement.ElementTree.Control as RadForm;
      if (control == null)
        return;
      if (control.ShowIcon)
        this.formElement.TitleBarElement.IconPrimitive.Visibility = ElementVisibility.Visible;
      else
        this.formElement.TitleBarElement.IconPrimitive.Visibility = ElementVisibility.Collapsed;
    }

    private void OnWMMove(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      if (this.stateChanged1)
        this.stateChanged2 = true;
      if (this.oldWindowState == this.Form.WindowState)
        return;
      this.stateChanged1 = true;
    }

    private void OnWMLButtonUp(ref Message m)
    {
      Point point = this.NCFromClientCoordinates(new Point(m.LParam.ToInt32()));
      MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 1);
      if (this.capturedNCItem != null)
        this.capturedNCItem.CallDoMouseUp(e);
      this.CallBaseWndProc(ref m);
    }

    private void OnWmMouseMove(ref Message m)
    {
      this.CallBaseWndProc(ref m);
    }

    private void OnWMNCRightButtonUp(ref Message m)
    {
      Point screenPoint = new Point(m.LParam.ToInt32());
      Point mappedWindowPoint = this.GetMappedWindowPoint(screenPoint);
      this.Form.Behavior.OnMouseUp(new MouseEventArgs(MouseButtons.Right, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 0));
      if (!(this.Form.ElementTree.GetElementAtPoint(mappedWindowPoint) is RadFormTitleBarElement))
        return;
      IntPtr lparam = new IntPtr((0 | screenPoint.Y) << 16 | screenPoint.X & (int) ushort.MaxValue);
      Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this.Form, this.Form.Handle), 787, IntPtr.Zero, lparam);
    }

    private void OnWMNCLButtonDblClk(ref Message m)
    {
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      this.Form.ElementTree.GetElementAtPoint(mappedWindowPoint)?.CallDoDoubleClick((EventArgs) new MouseEventArgs(Control.MouseButtons, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 0));
      this.CallBaseWndProc(ref m);
    }

    private void OnWMNCMouseMove(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      this.TrackMouseLeaveMessage();
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      this.Form.Behavior.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 1));
    }

    private void TrackMouseLeaveMessage()
    {
      if (this.isMouseTracking || !this.Form.IsHandleCreated)
        return;
      if (!Telerik.WinControls.NativeMethods._TrackMouseEvent(new Telerik.WinControls.NativeMethods.TRACKMOUSEEVENT() { dwFlags = 19, hwndTrack = this.Form.Handle }))
        return;
      this.isMouseTracking = true;
    }

    private void OnWMNCMouseLeave(ref Message m)
    {
      this.isMouseTracking = false;
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      MouseEventArgs e = new MouseEventArgs(MouseButtons.None, 0, mappedWindowPoint.X, mappedWindowPoint.Y, 0);
      if (this.capturedNCItem != null)
        this.capturedNCItem.CallDoMouseUp(e);
      this.targetHandler.Behavior.OnMouseLeave((EventArgs) e);
    }

    private void OnWmSetText(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      this.formElement.TitleBarElement.Text = Marshal.PtrToStringAuto(m.LParam);
      this.RefreshNC();
    }

    private void OnWMStyleChanged(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      this.AdjustFormElementForCurrentStyles();
    }

    private void AdjustFormElementForCurrentStyles()
    {
      this.AdjustSystemButtonsForStyle();
      if (this.Form.FormBorderStyle == FormBorderStyle.FixedToolWindow || this.Form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
      {
        int num1 = (int) this.formElement.TitleBarElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num3 = (int) this.Form.RootElement.ResetValue(RadElement.ShapeProperty, ValueResetFlags.Local);
      }
      else if (this.Form.FormBorderStyle == FormBorderStyle.Sizable)
      {
        int num1 = (int) this.formElement.TitleBarElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num3 = (int) this.Form.RootElement.ResetValue(RadElement.ShapeProperty, ValueResetFlags.Local);
      }
      else if (this.Form.FormBorderStyle == FormBorderStyle.FixedDialog || this.Form.FormBorderStyle == FormBorderStyle.FixedSingle || this.Form.FormBorderStyle == FormBorderStyle.Fixed3D)
      {
        int num1 = (int) this.formElement.TitleBarElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num3 = (int) this.Form.RootElement.ResetValue(RadElement.ShapeProperty, ValueResetFlags.Local);
      }
      else if (this.Form.FormBorderStyle == FormBorderStyle.None)
      {
        int num1 = (int) this.formElement.TitleBarElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        int num3 = (int) this.Form.RootElement.SetValue(RadElement.ShapeProperty, (object) null);
        this.Form.Region = (Region) null;
        this.Form.Invalidate();
      }
      if (this.Form.TopLevel || this.Form.Site != null || this.Form.IsMdiChild)
        return;
      this.formElement.Border.Visibility = ElementVisibility.Collapsed;
    }

    private void OnWmMDIActivate(ref Message m)
    {
      if (this.Form.IsMdiChild && this.Form.IsHandleCreated)
      {
        if (m.WParam == this.Form.Handle)
        {
          int num1 = (int) this.formElement.SetDefaultValueOverride(RibbonFormElement.IsFormActiveProperty, (object) false);
        }
        else if (m.LParam == this.Form.Handle)
        {
          int num2 = (int) this.formElement.SetDefaultValueOverride(RibbonFormElement.IsFormActiveProperty, (object) true);
        }
        m.Result = IntPtr.Zero;
      }
      this.CallBaseWndProc(ref m);
    }

    private void OnWmNCLeftMouseButtonDown(ref Message m)
    {
      if (!this.Form.IsHandleCreated)
      {
        this.CallBaseWndProc(ref m);
      }
      else
      {
        Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
        RadElement elementAtPoint = this.Form.ElementTree.GetElementAtPoint(mappedWindowPoint);
        this.Form.Behavior.OnMouseDown(new MouseEventArgs(Control.MouseButtons, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 0));
        this.capturedNCItem = elementAtPoint;
        if (elementAtPoint != null && elementAtPoint.Enabled && (!object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBarElement) && !object.ReferenceEquals((object) elementAtPoint, (object) this.formElement)))
          return;
        this.CallBaseWndProc(ref m);
      }
    }

    private void OnWmNCLeftMouseButtonUp(ref Message m)
    {
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 0);
      if (this.capturedNCItem != null)
        this.capturedNCItem.CallDoMouseUp(e);
      else
        this.Form.Behavior.OnMouseUp(e);
      int num1 = 0;
      RadElement elementAtPoint = this.Form.ElementTree.GetElementAtPoint(mappedWindowPoint);
      if (elementAtPoint != null && elementAtPoint.Enabled)
      {
        if (this.Form.IsMdiChild && this.IsMinimized)
        {
          if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBarElement.MaximizeButton))
            num1 = !this.Form.IsHandleCreated || !Telerik.WinControls.NativeMethods.IsZoomed(new HandleRef((object) null, this.Form.Handle)) ? 61488 : 61728;
          else if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBarElement.CloseButton))
            num1 = 61536;
          else if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBarElement.MinimizeButton))
            num1 = !this.Form.IsHandleCreated || !Telerik.WinControls.NativeMethods.IsIconic(new HandleRef((object) null, this.Form.Handle)) ? 61472 : 61728;
          else if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBarElement.HelpButton))
            num1 = 61824;
          else if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBarElement.IconPrimitive))
          {
            int num2 = 0;
            Rectangle screen = this.Form.RectangleToScreen(this.Form.ClientRectangle);
            IntPtr lparam = new IntPtr((num2 | screen.Y) << 16 | screen.X & (int) ushort.MaxValue);
            Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, this.Form.Handle), 787, IntPtr.Zero, lparam);
            return;
          }
        }
        if (object.ReferenceEquals((object) elementAtPoint, (object) this.RibbonBar.RibbonBarElement.IconPrimitive))
        {
          TimeSpan timeSpan = DateTime.Now - this.clickTimeStamp;
          this.clickTimeStamp = DateTime.Now;
          if (timeSpan.TotalMilliseconds < (double) SystemInformation.DoubleClickTime)
          {
            int num2 = 61536;
            if (this.Form.IsHandleCreated)
            {
              Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, this.Form.Handle), 274, new IntPtr(num2), IntPtr.Zero);
              return;
            }
          }
          int num3 = 0;
          Rectangle screen = this.RibbonBar.RectangleToScreen(this.RibbonBar.RibbonBarElement.TabStripElement.ControlBoundingRectangle);
          IntPtr lparam = new IntPtr((num3 | screen.Y) << 16 | screen.X & (int) ushort.MaxValue);
          Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, this.Form.Handle), 787, IntPtr.Zero, lparam);
          return;
        }
      }
      if (num1 == 0 || !this.Form.IsHandleCreated)
        return;
      Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, this.Form.Handle), 274, new IntPtr(num1), IntPtr.Zero);
    }

    private void OnWmSetIcon(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      if ((int) m.WParam == 0 && m.LParam != IntPtr.Zero)
      {
        Icon icon = Icon.FromHandle(m.LParam);
        if (icon != null)
        {
          RadRibbonBarElement ribbonBarElement = this.RibbonBar.RibbonBarElement;
          if (ribbonBarElement != null)
          {
            Bitmap bitmap = icon.ToBitmap();
            int num = (int) ribbonBarElement.IconPrimitive.SetDefaultValueOverride(ImagePrimitive.ImageProperty, (object) bitmap);
            icon.Dispose();
            this.Form.CallSetClientSizeCore(this.Form.ClientSize.Width, this.Form.ClientSize.Height);
          }
        }
      }
      else if ((int) m.WParam == 0 && m.LParam == IntPtr.Zero)
      {
        RadRibbonBarElement ribbonBarElement = this.RibbonBar.RibbonBarElement;
        if (ribbonBarElement != null)
        {
          int num = (int) ribbonBarElement.IconPrimitive.SetDefaultValueOverride(ImagePrimitive.ImageProperty, (object) null);
        }
      }
      this.AdjustFormIconVisibility();
    }

    private void OnWMActivate(ref Message m)
    {
      base.HandleWndProc(ref m);
      if (this.RibbonBar == null)
        return;
      if (m.WParam != IntPtr.Zero)
        this.Form.Visible = this.radRibbonBar.IsLoaded;
      if ((int) m.WParam == 1 || (int) m.WParam == 2)
      {
        if (this.RibbonBar != null)
        {
          int num1 = (int) this.RibbonBar.RibbonBarElement.SetValue(RadRibbonBarElement.IsRibbonFormActiveProperty, (object) true);
        }
        int num2 = (int) this.FormElement.SetValue(RibbonFormElement.IsFormActiveProperty, (object) true);
      }
      else
      {
        if (this.RibbonBar != null)
        {
          int num1 = (int) this.RibbonBar.RibbonBarElement.SetValue(RadRibbonBarElement.IsRibbonFormActiveProperty, (object) false);
        }
        int num2 = (int) this.FormElement.SetValue(RibbonFormElement.IsFormActiveProperty, (object) false);
      }
    }

    private bool OnWMHitTest(ref Message m)
    {
      if (!this.Form.IsHandleCreated)
      {
        this.CallBaseWndProc(ref m);
        return true;
      }
      IntPtr result = new IntPtr();
      if (!this.DwmDefWindowProc(this.Form.Handle, m.Msg, m.WParam, m.LParam, ref result))
      {
        Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
        if (this.TopResizeFrame.Contains(mappedWindowPoint))
        {
          if (this.Form != null && this.Form.FormBorderStyle != FormBorderStyle.Sizable && this.Form.FormBorderStyle != FormBorderStyle.SizableToolWindow)
            return base.HandleWndProc(ref m);
          m.Result = new IntPtr(12);
        }
        else
        {
          if (this.RibbonBar == null || !new Rectangle(this.TopLeftResizeFrame.Right, 0, this.RibbonBar.Width, SystemInformation.CaptionHeight).Contains(mappedWindowPoint))
            return base.HandleWndProc(ref m);
          m.Result = new IntPtr(2);
        }
      }
      else
        m.Result = result;
      return true;
    }

    private void OnContextMenuOpening(ref Message m)
    {
      if (((int) m.LParam & 65536) == 0)
        return;
      RadFormControlBase targetHandler = this.targetHandler as RadFormControlBase;
      if (targetHandler == null)
        return;
      switch (targetHandler.FormBorderStyle)
      {
        case FormBorderStyle.FixedSingle:
        case FormBorderStyle.Fixed3D:
        case FormBorderStyle.FixedDialog:
        case FormBorderStyle.FixedToolWindow:
          Telerik.WinControls.NativeMethods.EnableMenuItem(m.WParam, 61440U, 1U);
          break;
        default:
          Telerik.WinControls.NativeMethods.EnableMenuItem(m.WParam, 61440U, 0U);
          break;
      }
    }

    private void OnWMDWMCompositionChanged(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      this.compositionEnabled = DWMAPI.IsCompositionEnabled;
      if (this.RibbonBar != null)
        this.RibbonBar.CompositionEnabled = this.CompositionEffectsEnabled;
      this.AdjustBehaviorForCompositionSate();
      this.Form.CallUpdateStyles();
      if (!this.CompositionEffectsEnabled)
        return;
      this.ExtendFrameIntoClientArea();
    }

    protected override void OnGetMinMaxInfo(MinMaxInfo minMaxInfo)
    {
      minMaxInfo.MinTrackSize = new Size(SystemInformation.MinimizedWindowSize.Width, 100);
      base.OnGetMinMaxInfo(minMaxInfo);
    }

    protected override void OnActiveMDIChildTextChanged()
    {
      base.OnActiveMDIChildTextChanged();
      if (this.RibbonBar == null)
        return;
      this.RibbonBar.Text = this.Form.Text;
    }

    public override void FormHandleCreated()
    {
      if (this.radRibbonBar == null)
        return;
      this.radRibbonBar.LoadElementTree();
    }

    public override bool OnAssociatedFormPaintBackground(PaintEventArgs args)
    {
      if (this.radRibbonBar == null)
        base.OnAssociatedFormPaintBackground(args);
      if (!this.CompositionEffectsEnabled)
        base.OnAssociatedFormPaintBackground(args);
      int compositionMargin = this.TopCompositionMargin;
      args.Graphics.SetClip(new Rectangle(0, 0, this.Form.Width, compositionMargin), CombineMode.Exclude);
      return false;
    }

    protected override void OnFormAssociated()
    {
      base.OnFormAssociated();
      this.Form.SizeChanged += new EventHandler(this.Form_SizeChanged);
      this.Form.ThemeNameChanged += new ThemeNameChangedEventHandler(this.Form_ThemeNameChanged);
      this.InitializeDummyMenuStrip();
      this.compositionEnabled = DWMAPI.IsCompositionEnabled;
      if (!this.Form.IsHandleCreated)
      {
        this.Form.Load += new EventHandler(this.Form_Load);
      }
      else
      {
        this.InitializeRibbon();
        if (this.Form.IsDesignMode || this.FormElement == null)
          return;
        this.AdjustBehaviorForCompositionSate();
        if (!this.CompositionEffectsEnabled)
          return;
        this.ExtendFrameIntoClientArea();
      }
    }

    private void Form_Load(object sender, EventArgs e)
    {
      this.InitializeRibbon();
      this.AdjustFormElementForFormState(new int?(this.CurrentFormState));
      this.AdjustBehaviorForCompositionSate();
      if (!this.CompositionEffectsEnabled)
        return;
      this.ExtendFrameIntoClientArea();
    }

    private void Form_SizeChanged(object sender, EventArgs e)
    {
      if (this.CompositionEnabled)
      {
        if (this.IsMaximized)
        {
          if (this.AllowTheming)
            this.ExtendFrameIntoClientArea();
          int num = this.AllowTheming ? 0 : 4;
          this.Form.Padding = new Padding(num, SystemInformation.FrameBorderSize.Height, num, 0);
        }
        else if (this.oldWindowState != this.Form.WindowState)
        {
          if (this.AllowTheming)
            this.ExtendFrameIntoClientArea();
          this.Form.Padding = Padding.Empty;
        }
      }
      if (this.oldWindowState != this.Form.WindowState && this.RibbonBar != null)
      {
        int num1 = (int) this.RibbonBar.RibbonBarElement.SetValue(RadRibbonBarElement.RibbonFormWindowStateProperty, (object) this.Form.WindowState);
      }
      this.oldWindowState = this.Form.WindowState;
      if (!this.CompositionEffectsEnabled || this.Form.IsDesignMode)
        this.ResetFormRegion();
      if (!this.stateChanged1 && !this.stateChanged2)
        return;
      if (this.stateChanged2)
      {
        this.stateChanged1 = false;
        this.stateChanged2 = false;
      }
      this.Form.PerformLayout((Control) this.Form, "Bounds");
    }

    private void Form_ThemeNameChanged(object source, ThemeNameChangedEventArgs args)
    {
      this.RefreshNC();
    }

    private void AdjustBehaviorForCompositionSate()
    {
      if (this.FormElement == null)
        return;
      RibbonFormElement formElement = this.FormElement as RibbonFormElement;
      if (this.CompositionEffectsEnabled && !this.Form.IsDesignMode && this.Form.TopLevel)
      {
        if (formElement.Visibility == ElementVisibility.Collapsed)
          return;
        formElement.Visibility = ElementVisibility.Collapsed;
        this.PrepareRibbonBarForCompositionState();
        this.Form.RootElement.Shape = (ElementShape) null;
        this.Form.Region = (Region) null;
      }
      else
      {
        if (formElement.Visibility == ElementVisibility.Visible)
          return;
        formElement.Visibility = ElementVisibility.Visible;
        this.Form.Padding = Padding.Empty;
        this.PrepareRibbonBarForCompositionState();
        int num = (int) this.Form.RootElement.ResetValue(RadElement.ShapeProperty, ValueResetFlags.Local);
      }
    }

    private void AdjustSystemButtonsForStyle()
    {
      RadRibbonForm form = this.Form as RadRibbonForm;
      if (form == null || form.RibbonBar == null)
        return;
      RadRibbonBarCaption ribbonCaption = form.RibbonBar.RibbonBarElement.RibbonCaption;
      switch (this.Form.FormBorderStyle)
      {
        case FormBorderStyle.FixedSingle:
        case FormBorderStyle.Fixed3D:
        case FormBorderStyle.FixedDialog:
        case FormBorderStyle.Sizable:
          ribbonCaption.SystemButtons.Visibility = this.Form.ControlBox ? ElementVisibility.Visible : ElementVisibility.Hidden;
          if (this.Form.ControlBox)
          {
            ribbonCaption.MaximizeButton.Enabled = this.Form.MaximizeBox;
            ribbonCaption.MaximizeButton.Visibility = this.Form.MinimizeBox || this.Form.MaximizeBox ? ElementVisibility.Visible : ElementVisibility.Collapsed;
            ribbonCaption.MinimizeButton.Enabled = this.Form.MinimizeBox;
            ribbonCaption.MinimizeButton.Visibility = this.Form.MaximizeBox || this.Form.MinimizeBox ? ElementVisibility.Visible : ElementVisibility.Collapsed;
            if (!this.Form.MinimizeBox && !this.Form.MaximizeBox && this.Form.HelpButton)
            {
              int num = (int) ribbonCaption.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
              break;
            }
            int num1 = (int) ribbonCaption.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
            break;
          }
          break;
        case FormBorderStyle.FixedToolWindow:
        case FormBorderStyle.SizableToolWindow:
          ribbonCaption.MinimizeButton.Visibility = ElementVisibility.Collapsed;
          ribbonCaption.MaximizeButton.Visibility = ElementVisibility.Collapsed;
          ribbonCaption.HelpButton.Visibility = ElementVisibility.Collapsed;
          break;
      }
      this.AdjustFormIconVisibility();
    }

    public void AdjustFormIconVisibility()
    {
      RadRibbonForm form = this.Form as RadRibbonForm;
      if (form == null || form.RibbonBar == null)
        return;
      if (this.Form.IsDesignMode && this.Form.Icon != null)
      {
        Bitmap bitmap = Bitmap.FromHicon(new Icon(this.Form.Icon, this.Form.Icon.Size).Handle);
        int num = (int) form.RibbonBar.RibbonBarElement.IconPrimitive.SetDefaultValueOverride(ImagePrimitive.ImageProperty, (object) bitmap);
      }
      if (form.RibbonBar.ApplicationMenuStyle != ApplicationMenuStyle.BackstageView)
      {
        int num1 = (int) form.RibbonBar.RibbonBarElement.IconPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
      else
      {
        switch (this.Form.FormBorderStyle)
        {
          case FormBorderStyle.FixedSingle:
          case FormBorderStyle.Fixed3D:
          case FormBorderStyle.FixedDialog:
          case FormBorderStyle.Sizable:
            int num2 = (int) form.RibbonBar.RibbonBarElement.IconPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) (ElementVisibility) (!this.Form.ShowIcon || !this.Form.ControlBox ? 2 : 0));
            if (!this.Form.ControlBox)
              break;
            if (this.Form.FormBorderStyle == FormBorderStyle.FixedDialog)
            {
              int num3 = (int) form.RibbonBar.RibbonBarElement.IconPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
              break;
            }
            int num4 = (int) form.RibbonBar.RibbonBarElement.IconPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) (ElementVisibility) (this.Form.ShowIcon ? 0 : 2));
            break;
          case FormBorderStyle.FixedToolWindow:
          case FormBorderStyle.SizableToolWindow:
            int num5 = (int) form.RibbonBar.RibbonBarElement.IconPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
            break;
        }
      }
    }

    private void InitializeDummyMenuStrip()
    {
      if (this.Form.MainMenuStrip != null || this.Form.IsDesignMode)
        return;
      this.Form.MainMenuStrip = (MenuStrip) new RadRibbonFormMainMenuStrip();
    }

    private void PrepareRibbonBarForCompositionState()
    {
      RadRibbonBar ribbonBar = this.RibbonBar;
      if (ribbonBar == null)
        return;
      if (this.oldRadRibbonBarElementSettings == null)
        this.oldRadRibbonBarElementSettings = new List<object>();
      if (this.CompositionEffectsEnabled)
      {
        if (this.oldRadRibbonBarElementSettings.Count == 0)
        {
          this.oldRadRibbonBarElementSettings.Add((object) ribbonBar.RibbonBarElement.CaptionFill.Visibility);
          this.oldRadRibbonBarElementSettings.Add((object) ribbonBar.RootElement.BackColor);
          this.oldRadRibbonBarElementSettings.Add((object) ribbonBar.RibbonBarElement.RibbonCaption.SystemButtons.Visibility);
          this.oldRadRibbonBarElementSettings.Add((object) ribbonBar.QuickAccessToolBarHeight);
          this.oldRadRibbonBarElementSettings.Add((object) ribbonBar.RibbonBarElement.RibbonCaption.SystemButtons.Margin);
          this.oldRadRibbonBarElementSettings.Add((object) ribbonBar.RibbonBarElement.RibbonCaption.Visibility);
          this.oldRadRibbonBarElementSettings.Add((object) ribbonBar.RibbonBarElement.Text);
        }
        if (this.Form.IsMdiChild)
        {
          ribbonBar.RibbonBarElement.RibbonCaption.Visibility = ElementVisibility.Collapsed;
          ribbonBar.RibbonBarElement.Text = string.Empty;
        }
        int num = (int) ribbonBar.RibbonBarElement.RibbonCaption.SystemButtons.SetDefaultValueOverride(RadElement.MarginProperty, (object) Padding.Add(ribbonBar.RibbonBarElement.RibbonCaption.SystemButtons.Margin, new Padding(20, 0, 0, 0)));
        ribbonBar.RibbonBarElement.CaptionFill.Visibility = ElementVisibility.Hidden;
        ribbonBar.RootElement.BackColor = Color.Transparent;
        ribbonBar.RibbonBarElement.RibbonCaption.SystemButtons.Visibility = ElementVisibility.Hidden;
      }
      else
      {
        if (this.oldRadRibbonBarElementSettings.Count <= 0)
          return;
        ribbonBar.RibbonBarElement.CaptionFill.Visibility = (ElementVisibility) this.oldRadRibbonBarElementSettings[0];
        ribbonBar.RootElement.BackColor = (Color) this.oldRadRibbonBarElementSettings[1];
        ribbonBar.RibbonBarElement.RibbonCaption.SystemButtons.Visibility = (ElementVisibility) this.oldRadRibbonBarElementSettings[2];
        ribbonBar.QuickAccessToolBarHeight = (int) this.oldRadRibbonBarElementSettings[3];
        ribbonBar.RibbonBarElement.RibbonCaption.SystemButtons.Margin = (Padding) this.oldRadRibbonBarElementSettings[4];
        ribbonBar.RibbonBarElement.TabStripElement.PositionOffset = (SizeF) new Size(0, 0);
        ribbonBar.RibbonBarElement.ApplicationButtonElement.PositionOffset = (SizeF) new Size(0, 0);
        if (!this.Form.IsMdiChild)
          return;
        ribbonBar.RibbonBarElement.RibbonCaption.Visibility = (ElementVisibility) this.oldRadRibbonBarElementSettings[5];
        ribbonBar.RibbonBarElement.Text = this.oldRadRibbonBarElementSettings[6].ToString();
      }
    }

    private void UpdateMDIButtonsAndRibbonCaptionText()
    {
      RadRibbonBar ribbonBar = this.RibbonBar;
      if (ribbonBar == null)
        return;
      ribbonBar.Text = this.Form.Text;
      ribbonBar.RibbonBarElement.MDIbutton.UpdateVisibility();
    }

    protected virtual void UpdateFormForThemingState(bool showAero)
    {
      if (this.RibbonBar != null)
        this.RibbonBar.CompositionEnabled = showAero;
      if (this.Form == null)
        return;
      this.AdjustBehaviorForCompositionSate();
      this.Form.CallUpdateStyles();
      if (!this.CompositionEffectsEnabled)
        return;
      this.ExtendFrameIntoClientArea();
    }

    private bool IsWindows8OrHigher
    {
      get
      {
        OperatingSystem osVersion = Environment.OSVersion;
        if (osVersion.Platform != PlatformID.Win32NT)
          return false;
        if (osVersion.Version.Major > 6)
          return true;
        if (osVersion.Version.Major == 6)
          return osVersion.Version.Minor >= 2;
        return false;
      }
    }

    private void ResetFormRegion()
    {
      if (!this.Form.IsHandleCreated && !this.Form.IsDesignMode)
        return;
      if (this.Form.WindowState == FormWindowState.Maximized && this.Form.MaximumSize == Size.Empty)
      {
        int num1 = this.CompositionEnabled ? Math.Min(SystemInformation.FrameBorderSize.Height, 4) : SystemInformation.FixedFrameBorderSize.Height;
        int num2 = num1;
        int num3 = num1;
        int x = num2 + num2;
        int y = num3 + num3;
        Rectangle rect = new Rectangle(new Point(x, y), new Size(this.Form.Size.Width - x * 2, this.Form.Size.Height - y * 2));
        using (GraphicsPath path = new GraphicsPath())
        {
          path.AddRectangle(rect);
          this.Form.Region = new Region(path);
        }
      }
      else if (this.Form.RootElement.Shape != null && this.Form.RootElement.ApplyShapeToControl)
        this.Form.Region = this.Form.RootElement.Shape.CreateRegion(new Rectangle(0, 0, this.Form.Width, this.Form.Height));
      else
        this.Form.Region = Telerik.WinControls.NativeMethods.CreateRoundRectRgn(new Rectangle(0, 0, this.Form.Width, this.Form.Height), 9);
    }

    private void ExtendFrameIntoClientArea()
    {
      if (!this.Form.IsHandleCreated)
        return;
      this.DwmExtendFrameIntoClientArea(this.Form.Handle, ref new Telerik.WinControls.NativeMethods.MARGINS()
      {
        cyTopHeight = this.TopCompositionMargin
      });
    }

    private void InitializeRibbon()
    {
      foreach (Control control in (ArrangedElementCollection) this.Form.Controls)
      {
        if (control is RadRibbonBar)
        {
          this.radRibbonBar = control as RadRibbonBar;
          this.radRibbonBar.CompositionEnabled = this.CompositionEffectsEnabled;
          break;
        }
      }
    }

    protected override void OnWindowStateChanged(int currentFormState, int newFormState)
    {
      base.OnWindowStateChanged(currentFormState, newFormState);
      this.AdjustFormElementForFormState(new int?(newFormState));
    }

    private void AdjustFormElementForFormState(int? formState)
    {
      int? nullable1 = formState;
      if ((nullable1.GetValueOrDefault() != 2 ? 0 : (nullable1.HasValue ? 1 : 0)) != 0)
      {
        int num1 = (int) this.formElement.SetValue(RibbonFormElement.FormWindowStateProperty, (object) FormWindowState.Maximized);
      }
      int? nullable2 = formState;
      if ((nullable2.GetValueOrDefault() != 0 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
      {
        int num2 = (int) this.formElement.SetValue(RibbonFormElement.FormWindowStateProperty, (object) FormWindowState.Normal);
        this.formElement.Border.Visibility = ElementVisibility.Visible;
      }
      int? nullable3 = formState;
      if ((nullable3.GetValueOrDefault() != 1 ? 0 : (nullable3.HasValue ? 1 : 0)) != 0)
      {
        int num2 = (int) this.formElement.SetValue(RibbonFormElement.FormWindowStateProperty, (object) FormWindowState.Minimized);
        this.formElement.Border.Visibility = ElementVisibility.Visible;
        if (!this.Form.IsMdiChild)
          return;
        RadTitleBarElement titleBarElement = this.formElement.TitleBarElement;
        titleBarElement.Visibility = ElementVisibility.Visible;
        titleBarElement.Text = this.Form.Text;
        titleBarElement.IconPrimitive.Visibility = this.Form.ShowIcon ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        titleBarElement.IconPrimitive.ShouldHandleMouseInput = true;
        titleBarElement.CloseButton.ShouldHandleMouseInput = true;
      }
      else
      {
        if (!this.Form.IsMdiChild)
          return;
        this.formElement.TitleBarElement.Visibility = ElementVisibility.Collapsed;
      }
    }

    protected virtual Point NCFromClientCoordinates(Point clientCoordinates)
    {
      clientCoordinates.Offset(this.ClientMargin.Left, this.ClientMargin.Top);
      return clientCoordinates;
    }

    private delegate void DwmExtendFrameIntoClientAreaDelegate(
      IntPtr wndHandle,
      ref Telerik.WinControls.NativeMethods.MARGINS margins);

    private delegate bool DwmDefWindowProcDelegate(
      IntPtr wndHandle,
      int msg,
      IntPtr wParam,
      IntPtr lParam,
      ref IntPtr result);
  }
}
