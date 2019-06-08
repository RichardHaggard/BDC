// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls.UI
{
  public class RadFormBehavior : ThemedFormBehavior
  {
    private bool allowTheming = true;
    private RadFormElement formElement;
    private bool isMdiStripMeasured;
    private ScrollBarThumb scrollThumbCapture;
    private bool isMouseTracking;
    private Stopwatch dblClickStopwatch;
    private MdiClient mdiClient;
    private RadElement capturedNCItem;
    private bool stateChanged1;
    private bool stateChanged2;
    private FormWindowState oldWindowState;

    public RadFormBehavior()
    {
    }

    public RadFormBehavior(IComponentTreeHandler treeHandler)
      : base(treeHandler)
    {
    }

    public RadFormBehavior(IComponentTreeHandler treeHandler, bool shouldCreateChildren)
      : base(treeHandler, shouldCreateChildren)
    {
    }

    protected virtual Rectangle ScrollbarSizingGripBounds
    {
      get
      {
        return this.formElement.ScrollBarsFormSizingGrip.ControlBoundingRectangle;
      }
    }

    protected virtual Rectangle HorizontalScrollbarBounds
    {
      get
      {
        return this.SynchronizeHorizontalScrollbarState();
      }
    }

    protected virtual Rectangle VerticalScrollbarBounds
    {
      get
      {
        return this.SynchronizeVerticalScrollbarState();
      }
    }

    public override Rectangle CaptionTextBounds
    {
      get
      {
        return this.formElement.TitleBar.TitlePrimitive.ControlBoundingRectangle;
      }
    }

    public override Rectangle IconBounds
    {
      get
      {
        return this.formElement.TitleBar.IconPrimitive.ControlBoundingRectangle;
      }
    }

    public override Rectangle SystemButtonsBounds
    {
      get
      {
        return this.formElement.TitleBar.SystemButtons.ControlBoundingRectangle;
      }
    }

    public override Rectangle MenuBounds
    {
      get
      {
        return this.formElement.MdiControlStrip.ControlBoundingRectangle;
      }
    }

    public override bool AllowTheming
    {
      get
      {
        return this.allowTheming;
      }
      set
      {
        if (value == this.allowTheming)
          return;
        this.allowTheming = value;
        if (this.formElement == null)
          return;
        this.AdjustFormForThemingState();
      }
    }

    public override RadElement FormElement
    {
      get
      {
        return (RadElement) this.formElement;
      }
    }

    public override int CaptionHeight
    {
      get
      {
        if (this.Form.IsMdiChild && !this.IsMaximized && this.formElement.TitleBar.Visibility != ElementVisibility.Visible)
          this.formElement.TitleBar.Visibility = ElementVisibility.Visible;
        return (int) Math.Round((double) this.formElement.TitleBar.DesiredSize.Height, MidpointRounding.AwayFromZero);
      }
    }

    public override Padding BorderWidth
    {
      get
      {
        return this.formElement.BorderWidth;
      }
    }

    public override Padding ClientMargin
    {
      get
      {
        Padding p1 = this.CalculateDynamicClientMargin();
        if (this.Form.RightToLeft == RightToLeft.Yes)
          p1 = new Padding(p1.Left + this.VerticalScrollbarBounds.Width, p1.Top, p1.Right, p1.Bottom);
        else if (this.Form.RightToLeft == RightToLeft.No)
          p1 = new Padding(p1.Left, p1.Top, p1.Right + this.VerticalScrollbarBounds.Width, p1.Bottom);
        p1 = Padding.Add(p1, new Padding(0, 0, 0, this.HorizontalScrollbarBounds.Height));
        return p1;
      }
    }

    private void SynchronizeSizeGrip()
    {
      if (this.formElement == null)
        return;
      if (this.Form.VerticalScroll.Visible && this.Form.HorizontalScroll.Visible)
      {
        if (this.formElement.ScrollBarsFormSizingGrip.Visibility == ElementVisibility.Visible)
          return;
        this.formElement.ScrollBarsFormSizingGrip.Visibility = ElementVisibility.Visible;
      }
      else
      {
        if (this.formElement.ScrollBarsFormSizingGrip.Visibility == ElementVisibility.Collapsed)
          return;
        this.formElement.ScrollBarsFormSizingGrip.Visibility = ElementVisibility.Collapsed;
      }
    }

    protected virtual Rectangle SynchronizeVerticalScrollbarState()
    {
      RadScrollBarElement verticalScrollbar = this.formElement.VerticalScrollbar;
      if (this.Form.VerticalScroll.Visible)
      {
        if (verticalScrollbar.Visibility != ElementVisibility.Visible)
          verticalScrollbar.Visibility = ElementVisibility.Visible;
        return verticalScrollbar.ControlBoundingRectangle;
      }
      if (verticalScrollbar.Visibility == ElementVisibility.Visible)
        verticalScrollbar.Visibility = ElementVisibility.Collapsed;
      return Rectangle.Empty;
    }

    protected virtual Rectangle SynchronizeHorizontalScrollbarState()
    {
      RadScrollBarElement horizontalScrollbar = this.formElement.HorizontalScrollbar;
      if (this.Form.HorizontalScroll.Visible)
      {
        if (horizontalScrollbar.Visibility != ElementVisibility.Visible)
          horizontalScrollbar.Visibility = ElementVisibility.Visible;
        return horizontalScrollbar.ControlBoundingRectangle;
      }
      if (horizontalScrollbar.Visibility == ElementVisibility.Visible)
        horizontalScrollbar.Visibility = ElementVisibility.Collapsed;
      return Rectangle.Empty;
    }

    protected virtual void SynchronizeVerticalScrollbarValues()
    {
      if (!this.Form.VerticalScroll.Visible)
        return;
      RadScrollBarElement verticalScrollbar = this.formElement.VerticalScrollbar;
      verticalScrollbar.Minimum = this.Form.VerticalScroll.Minimum;
      verticalScrollbar.Maximum = this.Form.VerticalScroll.Maximum;
      verticalScrollbar.SmallChange = this.Form.VerticalScroll.SmallChange < 0 ? 0 : this.Form.VerticalScroll.SmallChange;
      verticalScrollbar.LargeChange = this.Form.VerticalScroll.LargeChange < 0 ? 0 : this.Form.VerticalScroll.LargeChange;
      verticalScrollbar.Value = this.Form.VerticalScroll.Value;
    }

    protected virtual void SynchronizeHorizontalScrollbarValues()
    {
      if (!this.Form.HorizontalScroll.Visible)
        return;
      RadScrollBarElement horizontalScrollbar = this.formElement.HorizontalScrollbar;
      horizontalScrollbar.Minimum = this.Form.HorizontalScroll.Minimum;
      horizontalScrollbar.Maximum = this.Form.HorizontalScroll.Maximum;
      horizontalScrollbar.SmallChange = this.Form.HorizontalScroll.SmallChange < 0 ? 0 : this.Form.HorizontalScroll.SmallChange;
      horizontalScrollbar.LargeChange = this.Form.HorizontalScroll.LargeChange < 0 ? 0 : this.Form.HorizontalScroll.LargeChange;
      horizontalScrollbar.Value = this.Form.HorizontalScroll.Value;
    }

    private void VerticalScrollbar_Scroll(object sender, ScrollEventArgs e)
    {
      this.Form.VerticalScroll.Value = e.NewValue;
    }

    private void HorizontalScrollbar_Scroll(object sender, ScrollEventArgs e)
    {
      this.Form.HorizontalScroll.Value = e.NewValue;
    }

    protected virtual int GetDynamicCaptionHeight()
    {
      int num = 0;
      if (!this.IsMaximized)
      {
        if (this.Form.FormBorderStyle != FormBorderStyle.None)
          num = this.CaptionHeight + this.BorderWidth.Top + this.formElement.TitleBar.Margin.Vertical;
      }
      else if (this.Form.FormBorderStyle != FormBorderStyle.None)
        num = DWMAPI.IsCompositionEnabled ? this.CaptionHeight + (this.Form.MaximumSize == Size.Empty ? SystemInformation.FrameBorderSize.Height : 0) : this.CaptionHeight + (this.Form.MaximumSize == Size.Empty ? SystemInformation.FixedFrameBorderSize.Height : 0);
      return num + this.GetAdjustedMDIStripHeight();
    }

    protected virtual Padding GetWindowRealNCMargin()
    {
      Telerik.WinControls.NativeMethods.RECT lpRect = new Telerik.WinControls.NativeMethods.RECT();
      Rectangle screen = this.ClientRectToScreen();
      lpRect.left = screen.Left;
      lpRect.top = screen.Top;
      lpRect.right = screen.Right;
      lpRect.bottom = screen.Bottom;
      CreateParams currentFormParams = this.CurrentFormParams;
      bool flag = Telerik.WinControls.NativeMethods.AdjustWindowRectEx(ref lpRect, currentFormParams.Style, this.Form.MainMenuStrip != null, currentFormParams.ExStyle);
      Padding padding = Padding.Empty;
      if (flag)
        padding = new Padding(screen.Left - lpRect.left, screen.Top - lpRect.top, lpRect.right - screen.Right, lpRect.bottom - screen.Bottom);
      return TelerikDpiHelper.ScalePadding(padding, this.FormElement.DpiScaleFactor);
    }

    private Rectangle ClientRectToScreen()
    {
      if (this.Form.IsHandleCreated)
        return this.Form.RectangleToScreen(this.Form.ClientRectangle);
      Rectangle clientRectangle = this.Form.ClientRectangle;
      clientRectangle.Offset(-this.Form.Location.X, -this.Form.Location.Y);
      return clientRectangle;
    }

    private int GetAdjustedMDIStripHeight()
    {
      int num = 0;
      if (this.IsMdiChildMaximized)
      {
        if (!this.IsMenuInForm)
        {
          if (this.formElement.MdiControlStrip.Visibility != ElementVisibility.Visible || !this.isMdiStripMeasured)
          {
            this.formElement.MdiControlStrip.Visibility = ElementVisibility.Visible;
            this.formElement.MdiControlStrip.ActiveMDIChild = this.MaximizedMDIChild;
            this.formElement.MdiControlStrip.Measure((SizeF) this.Form.Size);
            this.isMdiStripMeasured = true;
          }
          num += (int) this.formElement.MdiControlStrip.DesiredSize.Height;
        }
        else
        {
          RadMenu mainMenuInForm = this.MainMenuInForm;
          if (mainMenuInForm != null)
          {
            ElementVisibility elementVisibility = this.MaximizedMDIChild == null ? ElementVisibility.Collapsed : ElementVisibility.Visible;
            mainMenuInForm.MenuElement.SystemButtons.Visibility = elementVisibility;
          }
        }
        if (this.formElement.TitleBar.Text != this.Form.Text)
          this.formElement.TitleBar.Text = this.Form.Text;
      }
      else
      {
        if (this.formElement.MdiControlStrip.Visibility == ElementVisibility.Visible)
          this.formElement.MdiControlStrip.Visibility = ElementVisibility.Collapsed;
        if (this.formElement.TitleBar.Text != this.Form.Text)
          this.formElement.TitleBar.Text = this.Form.Text;
        this.isMdiStripMeasured = false;
      }
      return num;
    }

    public virtual Padding CalculateDynamicClientMargin()
    {
      Padding padding = new Padding();
      if (!this.IsMaximized || this.IsMaximized && this.Form.MaximumSize != Size.Empty)
      {
        if (this.Form.FormBorderStyle != FormBorderStyle.None)
        {
          padding = new Padding(this.BorderWidth.Left, this.GetDynamicCaptionHeight(), this.BorderWidth.Right, this.BorderWidth.Bottom);
          if (this.Form.IsMdiChild && this.IsMinimized)
            padding = new Padding(this.BorderWidth.Left, this.Form.Size.Height, this.BorderWidth.Right, this.BorderWidth.Bottom);
        }
        else
          padding = Padding.Empty;
      }
      else
      {
        Padding windowRealNcMargin = this.GetWindowRealNCMargin();
        padding = new Padding(windowRealNcMargin.Left, this.GetDynamicCaptionHeight(), windowRealNcMargin.Right, windowRealNcMargin.Bottom);
        if (this.Form.IsMdiChild)
        {
          int top = windowRealNcMargin.Top;
          if (this.Form.MainMenuStrip != null)
            top -= SystemInformation.MenuHeight;
          padding = new Padding(padding.Left, top, padding.Right, padding.Bottom);
        }
      }
      return padding;
    }

    private void TrackMouseLeaveMessage()
    {
      if (this.isMouseTracking || !this.Form.IsHandleCreated)
        return;
      if (!Telerik.WinControls.NativeMethods._TrackMouseEvent(new Telerik.WinControls.NativeMethods.TRACKMOUSEEVENT() { dwFlags = 19, hwndTrack = this.Form.Handle }))
        return;
      this.isMouseTracking = true;
    }

    protected virtual Point NCFromClientCoordinates(Point clientCoordinates)
    {
      clientCoordinates.Offset(this.ClientMargin.Left, this.ClientMargin.Top);
      return clientCoordinates;
    }

    private bool? MarkDoubleClickStart()
    {
      if (!this.dblClickStopwatch.IsRunning)
      {
        this.dblClickStopwatch.Reset();
        this.dblClickStopwatch.Start();
      }
      else
      {
        this.dblClickStopwatch.Stop();
        if (this.dblClickStopwatch.ElapsedMilliseconds < (long) SystemInformation.DoubleClickTime)
          return new bool?(true);
      }
      return new bool?();
    }

    protected override void OnFormAssociated()
    {
      base.OnFormAssociated();
      if (this.formElement == null && this.Form.RootElement.Children.Count > 0)
        this.formElement = this.Form.RootElement.Children[0] as RadFormElement;
      if (!this.Form.IsHandleCreated)
      {
        this.Form.Load += new EventHandler(this.Form_Load);
        this.Form.ControlAdded += new ControlEventHandler(this.OnForm_ControlAdded);
        this.Form.ControlRemoved += new ControlEventHandler(this.OnForm_ControlRemoved);
        this.Form.RightToLeftChanged += new EventHandler(this.OnForm_RightToLeftChanged);
        this.Form.ThemeNameChanged += new ThemeNameChangedEventHandler(this.OnForm_ThemeNameChanged);
        this.Form.SizeChanged += new EventHandler(this.Form_SizeChanged);
        this.Form.Layout += new LayoutEventHandler(this.Form_Layout);
      }
      else
      {
        foreach (Control control in (ArrangedElementCollection) this.Form.Controls)
        {
          if (control is MdiClient)
          {
            this.mdiClient = control as MdiClient;
            return;
          }
        }
      }
      this.dblClickStopwatch = new Stopwatch();
    }

    private void Form_Layout(object sender, LayoutEventArgs e)
    {
      this.SynchronizeScrollbarsValues();
    }

    protected internal void SynchronizeScrollbarsValues()
    {
      this.Form.PerformLayout((Control) this.Form, "Bounds");
      this.SynchronizeVerticalScrollbarValues();
      this.SynchronizeHorizontalScrollbarValues();
    }

    private void AdjustFormForThemingState()
    {
      if (!this.AllowTheming)
      {
        this.FormElement.Visibility = ElementVisibility.Collapsed;
        int num = (int) this.Form.RootElement.SetValue(RootRadElement.ApplyShapeToControlProperty, (object) false);
        this.Form.Region = (Region) null;
        this.Form.CallUpdateStyles();
      }
      else
      {
        this.FormElement.Visibility = ElementVisibility.Visible;
        int num = (int) this.Form.RootElement.SetValue(RootRadElement.ApplyShapeToControlProperty, (object) true);
        this.Form.CallUpdateStyles();
      }
    }

    protected void AdjustFormElementForFormState(int? formState)
    {
      int? nullable1 = formState;
      if ((nullable1.GetValueOrDefault() != 2 ? 0 : (nullable1.HasValue ? 1 : 0)) != 0)
      {
        int num1 = (int) this.formElement.SetValue(RadFormElement.FormWindowStateProperty, (object) FormWindowState.Maximized);
        if (this.Form.MaximumSize == Size.Empty)
        {
          if (!this.Form.IsMdiChild)
          {
            this.formElement.Border.Visibility = ElementVisibility.Collapsed;
            this.formElement.ImageBorder.Visibility = ElementVisibility.Collapsed;
          }
          int num2 = DWMAPI.IsCompositionEnabled ? SystemInformation.FrameBorderSize.Height : SystemInformation.FixedFrameBorderSize.Height;
          if (this.Form.IsMdiChild)
          {
            if (this.Form.AutoScroll && DWMAPI.IsCompositionEnabled)
              this.formElement.Margin = new Padding(this.formElement.BorderWidth.Left, -this.formElement.BorderWidth.Top, this.formElement.BorderWidth.Right, this.formElement.BorderWidth.Bottom);
            else
              this.formElement.Margin = new Padding(0, -this.formElement.TitleBar.ControlBoundingRectangle.Height, -1, -1);
          }
          else
            this.formElement.Margin = new Padding(TelerikDpiHelper.ScaleInt(num2, new SizeF(1f / this.FormElement.DpiScaleFactor.Width, 1f / this.FormElement.DpiScaleFactor.Height)));
        }
      }
      int? nullable2 = formState;
      if ((nullable2.GetValueOrDefault() != 1 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
      {
        int num = (int) this.formElement.SetValue(RadFormElement.FormWindowStateProperty, (object) FormWindowState.Minimized);
        this.formElement.Border.Visibility = ElementVisibility.Visible;
        this.formElement.ImageBorder.Visibility = ElementVisibility.Visible;
        if (this.Form.IsMdiChild)
          this.formElement.TitleBar.Visibility = ElementVisibility.Visible;
        this.formElement.Margin = Padding.Empty;
      }
      int? nullable3 = formState;
      if ((nullable3.GetValueOrDefault() != 0 ? 0 : (nullable3.HasValue ? 1 : 0)) == 0)
        return;
      int num3 = (int) this.formElement.SetValue(RadFormElement.FormWindowStateProperty, (object) FormWindowState.Normal);
      this.formElement.Border.Visibility = ElementVisibility.Visible;
      this.formElement.ImageBorder.Visibility = ElementVisibility.Visible;
      this.formElement.Margin = Padding.Empty;
      if (!this.Form.IsMdiChild)
        return;
      this.formElement.TitleBar.Visibility = ElementVisibility.Visible;
    }

    protected void AdjustFormElementForCurrentStyles()
    {
      this.AdjustSystemButtonsForStyle();
      if (this.Form.FormBorderStyle == FormBorderStyle.FixedToolWindow || this.Form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
      {
        int num1 = (int) this.formElement.TitleBar.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num3 = (int) this.formElement.ImageBorder.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num4 = (int) this.Form.RootElement.ResetValue(RadElement.ShapeProperty, ValueResetFlags.Local);
      }
      else if (this.Form.FormBorderStyle == FormBorderStyle.Sizable)
      {
        int num1 = (int) this.formElement.TitleBar.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num3 = (int) this.formElement.ImageBorder.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num4 = (int) this.Form.RootElement.ResetValue(RadElement.ShapeProperty, ValueResetFlags.Local);
      }
      else if (this.Form.FormBorderStyle == FormBorderStyle.FixedDialog || this.Form.FormBorderStyle == FormBorderStyle.FixedSingle || this.Form.FormBorderStyle == FormBorderStyle.Fixed3D)
      {
        int num1 = (int) this.formElement.TitleBar.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num3 = (int) this.formElement.ImageBorder.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num4 = (int) this.Form.RootElement.ResetValue(RadElement.ShapeProperty, ValueResetFlags.Local);
      }
      else if (this.Form.FormBorderStyle == FormBorderStyle.None)
      {
        int num1 = (int) this.formElement.TitleBar.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        int num2 = (int) this.formElement.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        int num3 = (int) this.formElement.ImageBorder.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        int num4 = (int) this.Form.RootElement.SetValue(RadElement.ShapeProperty, (object) null);
        this.Form.Region = (Region) null;
        this.Form.Invalidate();
      }
      if (this.Form.TopLevel || this.Form.Site != null || this.Form.IsMdiChild)
        return;
      this.formElement.Border.Visibility = ElementVisibility.Collapsed;
      this.formElement.ImageBorder.Visibility = ElementVisibility.Collapsed;
      this.formElement.ScrollBarsFormSizingGrip.Children[0].Visibility = ElementVisibility.Hidden;
      this.formElement.ScrollBarsFormSizingGrip.Children[1].Visibility = ElementVisibility.Hidden;
    }

    private void AdjustSystemButtonsForStyle()
    {
      if (this.Form.FormBorderStyle == FormBorderStyle.FixedToolWindow || this.Form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
      {
        this.formElement.TitleBar.MinimizeButton.Visibility = ElementVisibility.Collapsed;
        this.formElement.TitleBar.MaximizeButton.Visibility = ElementVisibility.Collapsed;
        this.formElement.TitleBar.HelpButton.Visibility = ElementVisibility.Collapsed;
        this.formElement.TitleBar.IconPrimitive.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        if (this.Form.FormBorderStyle != FormBorderStyle.Sizable && this.Form.FormBorderStyle != FormBorderStyle.FixedDialog && (this.Form.FormBorderStyle != FormBorderStyle.FixedSingle && this.Form.FormBorderStyle != FormBorderStyle.Fixed3D))
          return;
        this.formElement.TitleBar.SystemButtons.Visibility = this.Form.ControlBox ? ElementVisibility.Visible : ElementVisibility.Hidden;
        this.formElement.TitleBar.IconPrimitive.Visibility = !this.Form.ShowIcon || !this.Form.ControlBox ? ElementVisibility.Collapsed : ElementVisibility.Visible;
        if (!this.Form.ControlBox)
          return;
        this.formElement.TitleBar.MaximizeButton.Enabled = this.Form.MaximizeBox;
        this.formElement.TitleBar.MaximizeButton.Visibility = this.Form.MinimizeBox || this.Form.MaximizeBox ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        this.formElement.TitleBar.MinimizeButton.Enabled = this.Form.MinimizeBox;
        this.formElement.TitleBar.MinimizeButton.Visibility = this.Form.MaximizeBox || this.Form.MinimizeBox ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        if (!this.Form.MinimizeBox && !this.Form.MaximizeBox && this.Form.HelpButton)
        {
          int num1 = (int) this.formElement.TitleBar.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        }
        else
        {
          int num2 = (int) this.formElement.TitleBar.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        }
        this.formElement.TitleBar.IconPrimitive.Visibility = this.Form.ShowIcon ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    public override CreateParams CreateParams(CreateParams parameters)
    {
      CreateParams createParams = base.CreateParams(parameters);
      if (!this.Form.MinimizeBox)
        createParams.Style &= -131073;
      if (!this.Form.MaximizeBox)
        createParams.Style &= -65537;
      if (!this.Form.MinimizeBox && !this.Form.MaximizeBox && this.Form.HelpButton)
        createParams.ExStyle |= 1024;
      if (!this.Form.HelpButton)
        createParams.ExStyle &= -1025;
      if (!this.Form.ControlBox)
      {
        createParams.ExStyle &= -1025;
        createParams.Style &= -131073;
        createParams.Style &= -65537;
      }
      if (this.Form.ShowInTaskbar)
        createParams.ExStyle |= 262144;
      else
        createParams.ExStyle &= -262145;
      if (this.Form.FormBorderStyle == FormBorderStyle.None)
      {
        createParams.Style &= -12582913;
        createParams.Style &= -8388609;
        createParams.ExStyle &= -1025;
        createParams.Style &= -131073;
        createParams.Style &= -65537;
      }
      createParams.Style &= -1048577;
      createParams.Style &= -2097153;
      return createParams;
    }

    protected override void OnActiveMDIChildTextChanged()
    {
      base.OnActiveMDIChildTextChanged();
      this.formElement.TitleBar.Text = this.Form.Text;
    }

    protected override void OnWindowStateChanged(int currentFormState, int newFormState)
    {
      base.OnWindowStateChanged(currentFormState, newFormState);
      this.AdjustFormElementForFormState(new int?(newFormState));
      this.stateChanged1 = true;
    }

    public override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.formElement = this.CreateFormElement();
      parent.Children.Add((RadElement) this.formElement);
      this.formElement.TitleBar.Text = this.Form.Text;
      this.formElement.MdiControlStrip.MinimizeButton.Click += new EventHandler(this.MinimizeButton_Click);
      this.formElement.MdiControlStrip.MaximizeButton.Click += new EventHandler(this.MaximizeButton_Click);
      this.formElement.MdiControlStrip.CloseButton.Click += new EventHandler(this.CloseButton_Click);
      this.formElement.TitleBar.IconPrimitive.ShouldHandleMouseInput = true;
      this.formElement.HorizontalScrollbar.Scroll += new ScrollEventHandler(this.HorizontalScrollbar_Scroll);
      this.formElement.VerticalScrollbar.Scroll += new ScrollEventHandler(this.VerticalScrollbar_Scroll);
      this.formElement.MdiControlStrip.MaximizedMdiIcon.ShouldHandleMouseInput = true;
    }

    protected virtual RadFormElement CreateFormElement()
    {
      return new RadFormElement();
    }

    public override void InvalidateElement(RadElement element, Rectangle bounds)
    {
      this.InvalidateNC(bounds);
    }

    public override bool HandleWndProc(ref Message m)
    {
      if (m.Msg == 132 && !this.Form.IsMdiChild)
      {
        this.OnWMNCHittest(ref m);
        return true;
      }
      if (!this.AllowTheming)
        return false;
      switch (m.Msg)
      {
        case 3:
          this.OnWMMove(ref m);
          return true;
        case 6:
          this.OnWMActivate(ref m);
          return true;
        case 12:
          this.OnWmSetText(ref m);
          return true;
        case 125:
          this.OnWMStyleChanged(ref m);
          return true;
        case (int) sbyte.MaxValue:
          this.OnIconVisibilityChanged();
          break;
        case 128:
          this.OnWmSetIcon(ref m);
          return true;
        case 132:
          this.OnWMNCHittest(ref m);
          return true;
        case 160:
          this.OnWMNCMouseMove(ref m);
          return true;
        case 161:
          this.OnWmNCLeftMouseButtonDown(ref m);
          return true;
        case 162:
          this.OnWmNCLeftMouseButtonUp(ref m);
          return true;
        case 163:
          this.OnWMNCLButtonDblClk(ref m);
          return true;
        case 165:
          this.OnWMNCRightButtonUp(ref m);
          return true;
        case 279:
          this.OnContextMenuOpening(ref m);
          break;
        case 512:
          this.OnWmMouseMove(ref m);
          return true;
        case 514:
          this.OnWMLButtonUp(ref m);
          return true;
        case 522:
          this.OnWMMouseWheel(ref m);
          return true;
        case 546:
          this.OnWmMDIActivate(ref m);
          return true;
        case 674:
          this.OnWMNCMouseLeave(ref m);
          break;
        case 798:
          this.CallBaseWndProc(ref m);
          this.OnWMDWMCompositionChanged(ref m);
          break;
      }
      return base.HandleWndProc(ref m);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.Form.Load -= new EventHandler(this.Form_Load);
        this.Form.ControlAdded -= new ControlEventHandler(this.OnForm_ControlAdded);
        this.Form.ControlRemoved -= new ControlEventHandler(this.OnForm_ControlRemoved);
        this.Form.ThemeNameChanged -= new ThemeNameChangedEventHandler(this.OnForm_ThemeNameChanged);
        this.Form.RightToLeftChanged -= new EventHandler(this.OnForm_RightToLeftChanged);
        this.Form.SizeChanged -= new EventHandler(this.Form_SizeChanged);
        this.Form.Layout -= new LayoutEventHandler(this.Form_Layout);
      }
      base.Dispose(disposing);
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
      if (this.scrollThumbCapture != null)
      {
        this.scrollThumbCapture.CallDoMouseUp(e);
        this.scrollThumbCapture = (ScrollBarThumb) null;
      }
      if (this.capturedNCItem != null)
        this.capturedNCItem.CallDoMouseUp(e);
      this.CallBaseWndProc(ref m);
    }

    private void OnWmMouseMove(ref Message m)
    {
      if (this.scrollThumbCapture != null)
      {
        Point point = this.NCFromClientCoordinates(new Point(m.LParam.ToInt32()));
        this.scrollThumbCapture.CallDoMouseMove(new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 1));
      }
      else
        this.CallBaseWndProc(ref m);
    }

    private void OnWMMouseWheel(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      if (this.Form.VerticalScroll.Visible && this.Form.VerticalScroll.Value != this.formElement.VerticalScrollbar.Value && (this.Form.VerticalScroll.Value >= this.formElement.VerticalScrollbar.Minimum && this.Form.VerticalScroll.Value <= this.formElement.VerticalScrollbar.Maximum))
        this.formElement.VerticalScrollbar.Value = this.Form.VerticalScroll.Value;
      if (!this.Form.HorizontalScroll.Visible || this.Form.HorizontalScroll.Value == this.formElement.HorizontalScrollbar.Value || (this.Form.HorizontalScroll.Value < this.formElement.HorizontalScrollbar.Minimum || this.Form.HorizontalScroll.Value > this.formElement.HorizontalScrollbar.Maximum))
        return;
      this.formElement.HorizontalScrollbar.Value = this.Form.HorizontalScroll.Value;
    }

    private void OnWMNCHittest(ref Message m)
    {
      Point intPtr = this.ParseIntPtr(m.LParam);
      int num = 0;
      Point mappedWindowPoint = this.GetMappedWindowPoint(intPtr);
      if (this.Form.HorizontalScroll.Visible && this.Form.VerticalScroll.Visible && this.ScrollbarSizingGripBounds.Contains(mappedWindowPoint))
        num = 17;
      if (this.Form.VerticalScroll.Visible && this.VerticalScrollbarBounds.Contains(mappedWindowPoint))
        num = 19;
      if (this.Form.HorizontalScroll.Visible && this.HorizontalScrollbarBounds.Contains(mappedWindowPoint))
        num = 19;
      if (num != 0)
        m.Result = new IntPtr(num);
      else
        base.HandleWndProc(ref m);
    }

    private Point ParseIntPtr(IntPtr param)
    {
      if (IntPtr.Size == 4)
        return new Point(param.ToInt32());
      long int64 = param.ToInt64();
      return new Point((int) (short) int64, (int) (short) (int64 >> 16));
    }

    private void OnWMActivate(ref Message m)
    {
      base.HandleWndProc(ref m);
      if ((int) m.WParam == 1 || (int) m.WParam == 2)
        this.formElement.SetIsFormActiveInternal(true);
      else
        this.formElement.SetIsFormActiveInternal(false);
    }

    private void OnWmMDIActivate(ref Message m)
    {
      if (this.Form.IsMdiChild && this.Form.IsHandleCreated)
      {
        if (m.WParam == this.Form.Handle)
          this.formElement.SetIsFormActiveInternal(false);
        else if (m.LParam == this.Form.Handle)
          this.formElement.SetIsFormActiveInternal(true);
        m.Result = IntPtr.Zero;
      }
      this.CallBaseWndProc(ref m);
    }

    private void OnWMDWMCompositionChanged(ref Message m)
    {
      this.Form.CallUpdateStyles();
      if (!this.Form.IsMdiContainer)
        return;
      foreach (Form mdiChild in this.Form.MdiChildren)
      {
        if (mdiChild is RadFormControlBase)
          (mdiChild as RadFormControlBase).CallUpdateStyles();
      }
    }

    private void OnWMNCMouseLeave(ref Message m)
    {
      this.isMouseTracking = false;
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      MouseEventArgs e = new MouseEventArgs(this.capturedNCItem != null ? MouseButtons.Left : MouseButtons.None, 0, mappedWindowPoint.X, mappedWindowPoint.Y, 0);
      if (this.capturedNCItem != null)
        this.capturedNCItem.CallDoMouseUp(e);
      this.targetHandler.Behavior.OnMouseLeave((EventArgs) e);
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

    private void OnWmNCLeftMouseButtonUp(ref Message m)
    {
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 0);
      if (this.scrollThumbCapture != null)
      {
        this.scrollThumbCapture.CallDoMouseUp(e);
        this.scrollThumbCapture = (ScrollBarThumb) null;
      }
      if (this.capturedNCItem != null)
      {
        this.capturedNCItem.CallDoMouseUp(e);
        this.capturedNCItem = (RadElement) null;
      }
      else
        this.Form.Behavior.OnMouseUp(e);
      int num1 = 0;
      RadElement elementAtPoint = this.Form.ElementTree.GetElementAtPoint(mappedWindowPoint);
      if (elementAtPoint != null && elementAtPoint.Enabled)
      {
        if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBar.MaximizeButton))
          num1 = !this.Form.IsHandleCreated || !Telerik.WinControls.NativeMethods.IsZoomed(new HandleRef((object) null, this.Form.Handle)) ? 61488 : 61728;
        else if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBar.CloseButton))
          num1 = 61536;
        else if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBar.MinimizeButton))
          num1 = !this.Form.IsHandleCreated || !Telerik.WinControls.NativeMethods.IsIconic(new HandleRef((object) null, this.Form.Handle)) ? 61472 : 61728;
        else if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBar.HelpButton))
        {
          num1 = 61824;
        }
        else
        {
          if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBar.IconPrimitive))
          {
            bool? nullable = this.MarkDoubleClickStart();
            if (nullable.HasValue && nullable.Value)
            {
              int num2 = 61536;
              if (this.Form.IsHandleCreated)
              {
                Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, this.Form.Handle), 274, new IntPtr(num2), IntPtr.Zero);
                return;
              }
            }
            int num3 = 0;
            Rectangle screen = this.Form.RectangleToScreen(this.Form.ClientRectangle);
            IntPtr lparam = new IntPtr((num3 | screen.Y) << 16 | screen.X & (int) ushort.MaxValue);
            Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, this.Form.Handle), 787, IntPtr.Zero, lparam);
            return;
          }
          if (object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.MdiControlStrip.MaximizedMdiIcon))
          {
            Form maximizedMdiChild = this.MaximizedMDIChild;
            if (maximizedMdiChild == null || !maximizedMdiChild.IsHandleCreated)
              return;
            bool? nullable = this.MarkDoubleClickStart();
            if (nullable.HasValue && nullable.Value)
            {
              int num2 = 61536;
              Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, maximizedMdiChild.Handle), 274, new IntPtr(num2), IntPtr.Zero);
              return;
            }
            int num3 = 0;
            Rectangle screen = maximizedMdiChild.RectangleToScreen(maximizedMdiChild.ClientRectangle);
            IntPtr lparam = new IntPtr((num3 | screen.Y) << 16 | screen.X & (int) ushort.MaxValue);
            Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, maximizedMdiChild.Handle), 787, IntPtr.Zero, lparam);
            return;
          }
        }
      }
      if (num1 == 0 || !this.Form.IsHandleCreated)
        return;
      Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, this.Form.Handle), 274, new IntPtr(num1), IntPtr.Zero);
    }

    private void OnWMNCLButtonDblClk(ref Message m)
    {
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      this.Form.ElementTree.GetElementAtPoint(mappedWindowPoint)?.CallDoDoubleClick((EventArgs) new MouseEventArgs(Control.MouseButtons, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 0));
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
        if (elementAtPoint is ScrollBarThumb)
          this.scrollThumbCapture = elementAtPoint as ScrollBarThumb;
        bool flag = false;
        if (this.Form != null && this.Form.FormBorderStyle == FormBorderStyle.Fixed3D || (this.Form.FormBorderStyle == FormBorderStyle.FixedDialog || this.Form.FormBorderStyle == FormBorderStyle.FixedSingle) || this.Form.FormBorderStyle == FormBorderStyle.FixedToolWindow)
          flag = true;
        if (flag && elementAtPoint != null && (elementAtPoint.Enabled && !object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBar)) || elementAtPoint != null && elementAtPoint.Enabled && (!object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.TitleBar) && !object.ReferenceEquals((object) elementAtPoint, (object) this.formElement)) && !object.ReferenceEquals((object) elementAtPoint, (object) this.formElement.ScrollBarsFormSizingGrip))
          return;
        this.CallBaseWndProc(ref m);
      }
    }

    private void OnWmSetIcon(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      if ((int) m.WParam != 0 || this.formElement == null)
        return;
      if (m.LParam != IntPtr.Zero)
      {
        Icon icon = Icon.FromHandle(m.LParam);
        if (icon == null)
          return;
        this.formElement.SetFormElementIconInternal((Image) icon.ToBitmap());
        icon.Dispose();
        this.Form.CallSetClientSizeCore(this.Form.ClientSize.Width, this.Form.ClientSize.Height);
      }
      else
        this.formElement.SetFormElementIconInternal((Image) null);
    }

    private void OnWmSetText(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      this.formElement.TitleBar.Text = Marshal.PtrToStringAuto(m.LParam);
      this.RefreshNC();
    }

    private void OnWMStyleChanged(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      this.AdjustFormElementForCurrentStyles();
    }

    private void OnWMNCMouseMove(ref Message m)
    {
      this.CallBaseWndProc(ref m);
      this.TrackMouseLeaveMessage();
      Point mappedWindowPoint = this.GetMappedWindowPoint(new Point(m.LParam.ToInt32()));
      MouseEventArgs e = new MouseEventArgs(Control.MouseButtons, 1, mappedWindowPoint.X, mappedWindowPoint.Y, 1);
      if (this.scrollThumbCapture != null)
        this.scrollThumbCapture.CallDoMouseMove(e);
      else
        this.Form.Behavior.OnMouseMove(e);
    }

    internal void OnIconVisibilityChanged()
    {
      RadForm control = this.formElement.ElementTree.Control as RadForm;
      if (control == null)
        return;
      if (control.ShowIcon)
      {
        int num1 = (int) this.formElement.TitleBar.IconPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      else
      {
        int num2 = (int) this.formElement.TitleBar.IconPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      this.formElement.MdiControlStrip.Visibility = ElementVisibility.Collapsed;
      for (int index = 0; index < this.Form.MdiChildren.Length; ++index)
      {
        Form mdiChild = this.Form.MdiChildren[index];
        if (mdiChild.WindowState == FormWindowState.Maximized)
        {
          mdiChild.Close();
          break;
        }
      }
      if (!this.Form.IsHandleCreated)
        return;
      Telerik.WinControls.NativeMethods.SetWindowPos(new HandleRef((object) null, this.Form.Handle), new HandleRef((object) null, IntPtr.Zero), 0, 0, 0, 0, 547);
    }

    private void MaximizeButton_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.Form.MdiChildren.Length; ++index)
      {
        Form mdiChild = this.Form.MdiChildren[index];
        if (mdiChild.WindowState == FormWindowState.Maximized)
        {
          mdiChild.WindowState = FormWindowState.Normal;
          break;
        }
      }
    }

    private void MinimizeButton_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.Form.MdiChildren.Length; ++index)
      {
        Form mdiChild = this.Form.MdiChildren[index];
        if (mdiChild.WindowState == FormWindowState.Maximized)
        {
          mdiChild.WindowState = FormWindowState.Minimized;
          break;
        }
      }
    }

    private void Form_Load(object sender, EventArgs e)
    {
      this.Form.Load -= new EventHandler(this.Form_Load);
      if (this.Form.IsDesignMode)
      {
        int num = (int) this.formElement.SetDefaultValueOverride(RadFormElement.IsFormActiveProperty, (object) true);
      }
      this.AdjustFormElementForFormState(new int?(this.CurrentFormState));
      this.AdjustFormElementForCurrentStyles();
      this.SynchronizeScrollbarsValues();
    }

    private void OnForm_RightToLeftChanged(object sender, EventArgs e)
    {
      int num = (int) this.FormElement.SetValue(RadElement.RightToLeftProperty, (object) (this.Form.RightToLeft == RightToLeft.Yes));
    }

    private void OnForm_ThemeNameChanged(object source, ThemeNameChangedEventArgs args)
    {
      if (this.formElement == null)
        return;
      this.formElement.InvalidateMeasure();
      this.formElement.InvalidateArrange();
      this.formElement.UpdateLayout();
      this.Form.Update();
      this.Form.CallUpdateStyles();
      this.AdjustFormElementForFormState(new int?(this.CurrentFormState));
    }

    private void Form_SizeChanged(object sender, EventArgs e)
    {
      this.SynchronizeSizeGrip();
      this.SynchronizeScrollbarsValues();
      if (!this.stateChanged1 && !this.stateChanged2)
        return;
      if (this.stateChanged2)
      {
        this.stateChanged1 = false;
        this.stateChanged2 = false;
      }
      this.Form.PerformLayout((Control) this.Form, "Bounds");
    }

    private void OnForm_ControlRemoved(object sender, ControlEventArgs e)
    {
      this.SynchronizeSizeGrip();
      this.SynchronizeScrollbarsValues();
    }

    private void OnForm_ControlAdded(object sender, ControlEventArgs e)
    {
      this.SynchronizeSizeGrip();
      this.SynchronizeScrollbarsValues();
    }
  }
}
