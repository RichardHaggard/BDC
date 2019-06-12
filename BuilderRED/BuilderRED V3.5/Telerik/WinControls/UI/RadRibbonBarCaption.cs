// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarCaption
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadRibbonBarCaption : LightVisualElement
  {
    private bool canManageOwnerForm = true;
    private TextPrimitive captionText;
    private RadImageButtonElement helpButton;
    private RadImageButtonElement closeButton;
    private RadImageButtonElement minimizeButton;
    private RadImageButtonElement maximizeButton;
    private StackLayoutElement systemButtons;
    private RibbonBarCaptionLayoutPanel ribbonTextAndContextGroupPanel;
    private Point downPoint;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawText = false;
      this.StretchHorizontally = true;
      this.StretchVertically = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.captionText = new TextPrimitive();
      this.captionText.Class = "TitleCaption";
      this.captionText.TextAlignment = ContentAlignment.MiddleCenter;
      int num1 = (int) this.captionText.SetValue(RibbonBarCaptionLayoutPanel.CaptionTextProperty, (object) true);
      int num2 = (int) this.captionText.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
      this.ribbonTextAndContextGroupPanel = new RibbonBarCaptionLayoutPanel();
      this.ribbonTextAndContextGroupPanel.Children.Add((RadElement) this.captionText);
      this.Children.Add((RadElement) this.ribbonTextAndContextGroupPanel);
      this.systemButtons = new StackLayoutElement();
      this.systemButtons.Orientation = Orientation.Horizontal;
      this.systemButtons.StretchHorizontally = false;
      this.systemButtons.StretchVertically = true;
      this.systemButtons.FitInAvailableSize = true;
      this.systemButtons.Class = "SystemButtonsContainer";
      this.Children.Add((RadElement) this.systemButtons);
      this.helpButton = new RadImageButtonElement();
      this.helpButton.StateManager = new RibbonBarSystemButtonStateManager().StateManagerInstance;
      this.helpButton.ThemeRole = "RibbonHelpButton";
      this.helpButton.Class = "HelpButton";
      this.helpButton.Click += new EventHandler(this.OnHelpButtonClick);
      this.helpButton.ButtonFillElement.Class = "CaptionButtonFill";
      this.helpButton.BorderElement.Class = "CaptionButtonBorder";
      this.helpButton.StretchHorizontally = false;
      this.helpButton.Visibility = ElementVisibility.Collapsed;
      this.systemButtons.Children.Add((RadElement) this.helpButton);
      this.minimizeButton = new RadImageButtonElement();
      this.minimizeButton.StateManager = new RibbonBarSystemButtonStateManager().StateManagerInstance;
      this.minimizeButton.ThemeRole = "RibbonMinimizeButton";
      this.minimizeButton.Class = "MinimizeButton";
      this.minimizeButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.ribbon_minimize;
      this.minimizeButton.Click += new EventHandler(this.OnMinimize);
      this.minimizeButton.ButtonFillElement.Class = "CaptionButtonFill";
      this.minimizeButton.BorderElement.Class = "CaptionButtonBorder";
      this.minimizeButton.StretchHorizontally = false;
      this.systemButtons.Children.Add((RadElement) this.minimizeButton);
      this.maximizeButton = new RadImageButtonElement();
      this.maximizeButton.StateManager = new RibbonBarSystemButtonStateManager().StateManagerInstance;
      this.maximizeButton.ThemeRole = "RibbonMaximizeButton";
      this.maximizeButton.Class = "MaximizeButton";
      this.maximizeButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.ribbon_maximize;
      this.maximizeButton.Click += new EventHandler(this.OnMaximizeRestore);
      this.maximizeButton.ButtonFillElement.Class = "CaptionButtonFill";
      this.maximizeButton.BorderElement.Class = "CaptionButtonBorder";
      this.maximizeButton.StretchHorizontally = false;
      this.systemButtons.Children.Add((RadElement) this.maximizeButton);
      this.closeButton = new RadImageButtonElement();
      this.closeButton.StateManager = new RibbonBarSystemButtonStateManager().StateManagerInstance;
      this.closeButton.ThemeRole = "RibbonCloseButton";
      this.closeButton.Class = "CloseButton";
      this.closeButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.ribbon_close;
      this.closeButton.Click += new EventHandler(this.OnClose);
      this.closeButton.ButtonFillElement.Class = "CaptionButtonFill";
      this.closeButton.BorderElement.Class = "CaptionButtonBorder";
      this.closeButton.StretchHorizontally = false;
      this.systemButtons.Children.Add((RadElement) this.closeButton);
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Determines whether the parent form can be managed by the title bar.")]
    public bool CanManageOwnerForm
    {
      get
      {
        return this.canManageOwnerForm;
      }
      set
      {
        this.canManageOwnerForm = value;
      }
    }

    [Localizable(true)]
    public string Caption
    {
      get
      {
        return (string) this.GetValue(RadItem.TextProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.TextProperty, (object) value);
      }
    }

    [Browsable(false)]
    public RibbonBarCaptionLayoutPanel CaptionLayout
    {
      get
      {
        return this.ribbonTextAndContextGroupPanel;
      }
    }

    public RadButtonElement HelpButton
    {
      get
      {
        return (RadButtonElement) this.helpButton;
      }
    }

    public RadButtonElement MinimizeButton
    {
      get
      {
        return (RadButtonElement) this.minimizeButton;
      }
    }

    public RadButtonElement MaximizeButton
    {
      get
      {
        return (RadButtonElement) this.maximizeButton;
      }
    }

    public RadButtonElement CloseButton
    {
      get
      {
        return (RadButtonElement) this.closeButton;
      }
    }

    public StackLayoutElement SystemButtons
    {
      get
      {
        return this.systemButtons;
      }
    }

    public event TitleBarSystemEventHandler Close;

    public event TitleBarSystemEventHandler Minimize;

    public event TitleBarSystemEventHandler MaximizeRestore;

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.downPoint = e.Location;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.canManageOwnerForm || e.Button != MouseButtons.Left || !(this.downPoint != e.Location) || this.PointFromControl(e.Location).X >= this.Size.Width - this.systemButtons.Size.Width)
        return;
      Form form = this.FindForm();
      if (form == null)
        return;
      Telerik.WinControls.NativeMethods.ReleaseCapture();
      int lParam = e.X + (e.Y << 16);
      Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, 2, lParam);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (this.MaximizeButton.Visibility != ElementVisibility.Visible)
        return;
      this.OnMaximizeRestore((object) this, EventArgs.Empty);
    }

    protected void OnClose(object sender, EventArgs args)
    {
      if (this.Close != null)
        this.Close(sender, args);
      this.FindForm()?.Close();
    }

    protected void OnMinimize(object sender, EventArgs args)
    {
      if (this.Minimize != null)
        this.Minimize(sender, args);
      Form form = this.FindForm();
      if (form == null)
        return;
      form.WindowState = FormWindowState.Minimized;
    }

    protected void OnMaximizeRestore(object sender, EventArgs args)
    {
      if (this.MaximizeRestore != null)
        this.MaximizeRestore(sender, args);
      Form form = this.FindForm();
      if (form == null)
        return;
      if (form.WindowState == FormWindowState.Maximized)
        form.WindowState = FormWindowState.Normal;
      else
        form.WindowState = FormWindowState.Maximized;
    }

    protected void OnHelpButtonClick(object sender, EventArgs e)
    {
      Form form = this.FindForm();
      if (form == null || !form.IsHandleCreated)
        return;
      int num = 61824;
      Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, form.Handle), 274, new IntPtr(num), IntPtr.Zero);
    }

    private Form FindForm()
    {
      Form form = (Form) null;
      if (this.ElementTree != null)
        form = this.ElementTree.Control.FindForm();
      return form;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      SizeF empty = SizeF.Empty;
      SizeF availableSize1 = availableSize;
      this.systemButtons.Measure(availableSize);
      availableSize1.Width -= this.systemButtons.DesiredSize.Width;
      RadRibbonBar control = this.ElementTree.Control as RadRibbonBar;
      if (control != null && control.CompositionEnabled)
        availableSize1.Width -= 25f;
      this.ribbonTextAndContextGroupPanel.Measure(availableSize1);
      empty.Width = this.systemButtons.DesiredSize.Width + this.ribbonTextAndContextGroupPanel.DesiredSize.Width;
      empty.Height = Math.Max(this.systemButtons.DesiredSize.Height, this.ribbonTextAndContextGroupPanel.DesiredSize.Height);
      if (!float.IsInfinity(availableSize.Width))
        empty.Width = availableSize.Width;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (!this.RightToLeft)
      {
        this.systemButtons.Arrange(new RectangleF(clientRectangle.Right - this.systemButtons.DesiredSize.Width, clientRectangle.Top, this.systemButtons.DesiredSize.Width, clientRectangle.Height));
        this.ribbonTextAndContextGroupPanel.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width - this.systemButtons.DesiredSize.Width, clientRectangle.Height));
      }
      else
      {
        this.systemButtons.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Top, this.systemButtons.DesiredSize.Width, clientRectangle.Height));
        this.ribbonTextAndContextGroupPanel.Arrange(new RectangleF(clientRectangle.X + this.systemButtons.DesiredSize.Width, clientRectangle.Y, clientRectangle.Width - this.systemButtons.DesiredSize.Width, clientRectangle.Height));
      }
      return finalSize;
    }
  }
}
