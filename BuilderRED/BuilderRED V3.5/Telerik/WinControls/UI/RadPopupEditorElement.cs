// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPopupEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadPopupEditorElement : PopupEditorBaseElement
  {
    private readonly Size defaultPopupSize = new Size(200, 200);
    private RadDropDownStyle dropDownStyle = RadDropDownStyle.DropDownList;
    private Size dropDownMinSize = Size.Empty;
    private Size dropDownMaxSize = Size.Empty;
    private SizingMode sizingMode = SizingMode.UpDownAndRightBottom;
    private RadArrowButtonElement arrowButton;
    private RadDropDownListEditableAreaElement containerElement;
    private RadTextBoxElement editableElement;
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private RadEditorPopupControlBase popup;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.arrowButton = this.CreateArrowButtonElement();
      int num1 = (int) this.arrowButton.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      this.arrowButton.ClickMode = ClickMode.Press;
      this.arrowButton.Click += new EventHandler(this.arrow_Click);
      this.arrowButton.ZIndex = 1;
      this.arrowButton.StretchHorizontally = false;
      this.arrowButton.StretchVertically = true;
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "DropDownListBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "DropDownFill";
      this.fillPrimitive.ZIndex = -1;
      this.fillPrimitive.RadPropertyChanged += new RadPropertyChangedEventHandler(this.fillPrimitive_RadPropertyChanged);
      this.Children.Add((RadElement) this.fillPrimitive);
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.CanFocus = false;
      stackLayoutElement.StretchVertically = true;
      stackLayoutElement.StretchHorizontally = true;
      stackLayoutElement.Class = "DropDownListStack";
      stackLayoutElement.FitInAvailableSize = true;
      this.Children.Add((RadElement) stackLayoutElement);
      this.containerElement = new RadDropDownListEditableAreaElement((RadDropDownListElement) null);
      this.editableElement = (RadTextBoxElement) this.containerElement.TextBox;
      this.editableElement.Visibility = ElementVisibility.Hidden;
      this.containerElement.DrawText = true;
      stackLayoutElement.Children.Add((RadElement) this.containerElement);
      stackLayoutElement.Children.Add((RadElement) this.arrowButton);
      int num2 = (int) this.BindProperty(RadItem.TextProperty, (RadObject) this.editableElement, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
    }

    private void editableElement_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.F4 && (!e.Alt || e.KeyCode != Keys.Down))
        return;
      this.TooglePopupState();
    }

    protected virtual RadArrowButtonElement CreateArrowButtonElement()
    {
      return (RadArrowButtonElement) new RadDropDownListArrowButtonElement();
    }

    protected override RadPopupControlBase CreatePopupForm()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.DrawFill = false;
      this.popup = (RadEditorPopupControlBase) new RadPopupContainerForm((RadItem) this);
      int num1 = (int) this.popup.SizingGrip.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(0, 10));
      this.popup.SizingGrip.ZIndex = 100;
      this.popup.SizingGripDockLayout.Children.Add((RadElement) lightVisualElement);
      this.popup.SizingMode = SizingMode.UpDownAndRightBottom;
      this.popup.SizingGrip.ElementTree.Control.BringToFront();
      this.popup.AnimationEnabled = false;
      int num2 = (int) this.containerElement.TextBox.BindProperty(VisualElement.BackColorProperty, (RadObject) this.fillPrimitive, VisualElement.BackColorProperty, PropertyBindingOptions.TwoWay);
      this.WireEvents();
      return (RadPopupControlBase) this.popup;
    }

    protected virtual void WireEvents()
    {
      this.editableElement.KeyDown += new KeyEventHandler(this.editableElement_KeyDown);
      if (this.popup == null)
        return;
      this.popup.Resize += new EventHandler(this.popup_Resize);
    }

    protected virtual void UnwireEvents()
    {
      this.editableElement.KeyDown -= new KeyEventHandler(this.editableElement_KeyDown);
      if (this.popup == null)
        return;
      this.popup.Resize -= new EventHandler(this.popup_Resize);
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }

    private void popup_Resize(object sender, EventArgs e)
    {
      if (this.popup == null || this.popup.Controls.Count <= 0)
        return;
      this.popup.Controls[0].Size = new Size(this.popup.Size.Width, this.popup.Size.Height - (this.sizingMode != SizingMode.None ? this.popup.SizingGrip.MinSize.Height : 0));
    }

    protected override void OnPopupClosing(RadPopupClosingEventArgs e)
    {
      if (e.CloseReason == RadPopupCloseReason.Mouse && this.arrowButton.ContainsMouse)
        e.Cancel = true;
      else
        base.OnPopupClosing(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      bool newValue = (bool) e.NewValue;
      if (this.Shape != null)
        this.Shape.IsRightToLeft = newValue;
      if (this.ArrowButtonElement.Shape == null)
        return;
      this.ArrowButtonElement.Shape.IsRightToLeft = newValue;
    }

    public override void ShowPopup()
    {
      if (this.popup != null && this.popup.Controls.Count > 0)
      {
        this.popup.Controls[0].Location = Point.Empty;
        this.popup.Controls[0].SendToBack();
        this.popup.SizingMode = this.sizingMode;
        this.PopupContainerForm.Panel.Focus();
      }
      base.ShowPopup();
    }

    private void arrow_Click(object sender, EventArgs e)
    {
      this.TooglePopupState();
    }

    protected override Size GetPopupSize(RadPopupControlBase popup, bool measure)
    {
      Size size = this.defaultPopupSize;
      if (this.popup != null && popup.Controls.Count > 0)
      {
        if (this.DropDownSizingMode == SizingMode.None)
        {
          size = new Size(popup.Controls[0].Size.Width, popup.Controls[0].Size.Height);
        }
        else
        {
          int height = this.popup.SizingGrip.MinSize.Height;
          size = new Size(popup.Controls[0].Size.Width, popup.Controls[0].Size.Height + height);
        }
      }
      SizeF scaleFactor = new SizeF(1f, 1f);
      if (this.popup.LastShowDpiScaleFactor != this.DpiScaleFactor)
        scaleFactor = new SizeF(this.DpiScaleFactor.Width / this.popup.LastShowDpiScaleFactor.Width, this.DpiScaleFactor.Height / this.popup.LastShowDpiScaleFactor.Height);
      return TelerikDpiHelper.ScaleSize(size, scaleFactor);
    }

    private void fillPrimitive_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != VisualElement.BackColorProperty)
        return;
      int num = (int) this.editableElement.SetDefaultValueOverride(VisualElement.BackColorProperty, e.NewValue);
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadDropDownListElement);
      }
    }

    protected override void UpdatePopupMinMaxSize(RadPopupControlBase popup)
    {
      if (this.DropDownMinSize != Size.Empty)
        popup.MinimumSize = this.DropDownMinSize;
      if (!(this.DropDownMaxSize != Size.Empty))
        return;
      popup.MaximumSize = this.DropDownMaxSize;
    }

    public RadPopupContainerForm PopupContainerForm
    {
      get
      {
        return (RadPopupContainerForm) this.popup;
      }
    }

    public RadArrowButtonElement ArrowButtonElement
    {
      get
      {
        return this.arrowButton;
      }
    }

    [DefaultValue(SizingMode.UpDownAndRightBottom)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, vertical or a combination of them.")]
    public SizingMode DropDownSizingMode
    {
      get
      {
        return this.sizingMode;
      }
      set
      {
        this.sizingMode = value;
        if (this.popup == null)
          return;
        this.popup.SizingMode = this.sizingMode;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Represents the TextBox that is hosted inside.")]
    [Browsable(false)]
    public virtual RadTextBoxElement TextBoxElement
    {
      get
      {
        return this.editableElement;
      }
    }

    [Description("Represents the LightVisualElement that hosted TextBox inside.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RadDropDownListEditableAreaElement ContainerElement
    {
      get
      {
        return this.containerElement;
      }
    }

    [Category("Behavior")]
    [SettingsBindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    [Description("Gets or sets the text associated with this item.")]
    [Bindable(true)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        this.containerElement.Text = value;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [DefaultValue(RadDropDownStyle.DropDownList)]
    [Description("Gets or sets a value specifying the style of the RadDropDownList.")]
    public virtual RadDropDownStyle DropDownStyle
    {
      get
      {
        return this.dropDownStyle;
      }
      set
      {
        this.dropDownStyle = value;
        if (this.dropDownStyle == RadDropDownStyle.DropDownList)
        {
          this.editableElement.Visibility = ElementVisibility.Hidden;
          this.containerElement.DrawText = true;
        }
        else
        {
          this.editableElement.Visibility = ElementVisibility.Visible;
          this.containerElement.DrawText = false;
        }
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Set or get the TextBox visibility")]
    public virtual ElementVisibility TextBoxVisibility
    {
      get
      {
        return this.TextBoxElement.Visibility;
      }
      set
      {
        this.TextBoxElement.Visibility = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(typeof (Size), "0,0")]
    [Description("Gets or sets the drop down minimum size.")]
    [Category("Appearance")]
    public Size DropDownMinSize
    {
      get
      {
        return this.dropDownMinSize;
      }
      set
      {
        if (!(this.dropDownMinSize != value))
          return;
        this.dropDownMinSize = value;
        if (!(this.dropDownMaxSize != Size.Empty))
          return;
        if (this.dropDownMinSize.Width > this.dropDownMaxSize.Width)
          this.dropDownMaxSize.Width = this.dropDownMinSize.Width;
        if (this.dropDownMinSize.Height <= this.dropDownMaxSize.Height)
          return;
        this.dropDownMaxSize.Height = this.dropDownMinSize.Height;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down maximum size.")]
    [DefaultValue(typeof (Size), "0,0")]
    public Size DropDownMaxSize
    {
      get
      {
        return this.dropDownMaxSize;
      }
      set
      {
        if (!(this.dropDownMaxSize != value))
          return;
        this.dropDownMaxSize = value;
        if (!(this.dropDownMinSize != Size.Empty))
          return;
        if (this.dropDownMaxSize.Width < this.dropDownMinSize.Width)
          this.dropDownMinSize.Width = this.dropDownMaxSize.Width;
        if (this.dropDownMaxSize.Height >= this.dropDownMinSize.Height)
          return;
        this.dropDownMinSize.Height = this.dropDownMaxSize.Height;
      }
    }
  }
}
