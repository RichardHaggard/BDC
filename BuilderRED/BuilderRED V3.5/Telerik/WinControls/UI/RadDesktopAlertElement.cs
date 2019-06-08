// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDesktopAlertElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadDesktopAlertElement : LightVisualElement
  {
    public static RadProperty CaptionTextProperty = RadProperty.Register(nameof (CaptionText), typeof (string), typeof (RadDesktopAlertElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContentTextProperty = RadProperty.Register(nameof (ContentText), typeof (string), typeof (RadDesktopAlertElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContentImageProperty = RadProperty.Register(nameof (ContentImage), typeof (Image), typeof (RadDesktopAlertElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private bool showCloseButton = true;
    private bool showPinButton = true;
    private bool showOptionsButton = true;
    private AlertWindowCaptionElement alertWindowCaptionElement;
    private AlertWindowContentElement alertWindowContentElement;
    private AlertWindowButtonsPanel alertWindowButtonsPanel;
    private bool autoSizeHeight;

    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents by Height.")]
    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(false)]
    public bool AutoSizeHeight
    {
      get
      {
        return this.autoSizeHeight;
      }
      set
      {
        if (this.autoSizeHeight == value)
          return;
        this.autoSizeHeight = value;
        this.BypassLayoutPolicies = value;
        this.InvalidateMeasure();
        this.OnNotifyPropertyChanged(nameof (AutoSizeHeight));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a boolean value determining whether the options button is shown.")]
    public bool ShowOptionsButton
    {
      get
      {
        return this.showOptionsButton;
      }
      set
      {
        if (this.showOptionsButton == value)
          return;
        this.showOptionsButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowOptionsButton));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a boolean value determining whether the pin button is shown.")]
    public bool ShowPinButton
    {
      get
      {
        return this.showPinButton;
      }
      set
      {
        if (this.showPinButton == value)
          return;
        this.showPinButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowPinButton));
      }
    }

    [Description("Gets or sets a boolean value determining whether the close button is shown.")]
    [DefaultValue(true)]
    public bool ShowCloseButton
    {
      get
      {
        return this.showCloseButton;
      }
      set
      {
        if (this.showCloseButton == value)
          return;
        this.showCloseButton = value;
        this.OnNotifyPropertyChanged(nameof (ShowCloseButton));
      }
    }

    [Description("Gets or sets the alert's content image.")]
    public Image ContentImage
    {
      get
      {
        return this.GetValue(RadDesktopAlertElement.ContentImageProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(RadDesktopAlertElement.ContentImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the text of the alert's caption.")]
    public string CaptionText
    {
      get
      {
        return (string) this.GetValue(RadDesktopAlertElement.CaptionTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDesktopAlertElement.CaptionTextProperty, (object) value);
      }
    }

    [Description("Gets or sets the content text of the desktop alert. This text is displayed in the content area of the alert's popup.")]
    public string ContentText
    {
      get
      {
        return (string) this.GetValue(RadDesktopAlertElement.ContentTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDesktopAlertElement.ContentTextProperty, (object) value);
      }
    }

    [Browsable(false)]
    public AlertWindowCaptionElement CaptionElement
    {
      get
      {
        return this.alertWindowCaptionElement;
      }
    }

    [Browsable(false)]
    public AlertWindowContentElement ContentElement
    {
      get
      {
        return this.alertWindowContentElement;
      }
    }

    [Browsable(false)]
    public AlertWindowButtonsPanel ButtonsPanel
    {
      get
      {
        return this.alertWindowButtonsPanel;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      int verticalReservedSpace = this.GetVerticalReservedSpace();
      int horizontalReservedSpace = this.GetHorizontalReservedSpace();
      int num = (int) availableSize.Width - horizontalReservedSpace;
      float height = (float) ((int) availableSize.Height - verticalReservedSpace) - (this.alertWindowCaptionElement.DesiredSize.Height + this.alertWindowButtonsPanel.DesiredSize.Height);
      this.alertWindowContentElement.Measure(new SizeF((float) num, height));
      if (this.autoSizeHeight)
        height = (float) verticalReservedSpace + this.alertWindowContentElement.DesiredSize.Height;
      sizeF = new SizeF(sizeF.Width, this.alertWindowCaptionElement.DesiredSize.Height + this.alertWindowButtonsPanel.DesiredSize.Height + height);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      Padding borderThickness = this.GetBorderThickness();
      Padding padding = this.Padding;
      int horizontalReservedSpace = this.GetHorizontalReservedSpace();
      int verticalReservedSpace = this.GetVerticalReservedSpace();
      int num1 = (int) finalSize.Width - horizontalReservedSpace;
      int num2 = (int) finalSize.Height - verticalReservedSpace;
      int num3 = borderThickness.Left + padding.Left;
      int num4 = borderThickness.Top + padding.Top;
      this.alertWindowCaptionElement.Arrange(new RectangleF((float) num3, (float) num4, (float) num1, this.alertWindowCaptionElement.DesiredSize.Height));
      this.alertWindowButtonsPanel.Arrange(new RectangleF((float) num3, (float) ((int) finalSize.Height - (borderThickness.Bottom + padding.Bottom + (int) this.alertWindowButtonsPanel.DesiredSize.Height)), (float) num1, this.alertWindowButtonsPanel.DesiredSize.Height));
      float height = (float) num2 - (this.alertWindowCaptionElement.DesiredSize.Height + this.alertWindowButtonsPanel.DesiredSize.Height);
      this.alertWindowContentElement.Arrange(new RectangleF((float) this.alertWindowCaptionElement.ControlBoundingRectangle.Left, (float) this.alertWindowCaptionElement.ControlBoundingRectangle.Bottom, (float) num1, height));
      return sizeF;
    }

    private int GetHorizontalReservedSpace()
    {
      return this.Padding.Horizontal + this.GetBorderThickness().Horizontal;
    }

    private int GetVerticalReservedSpace()
    {
      return this.Padding.Vertical + this.GetBorderThickness().Vertical;
    }

    private Padding GetBorderThickness()
    {
      switch (this.BorderBoxStyle)
      {
        case BorderBoxStyle.SingleBorder:
          return new Padding((int) this.BorderWidth);
        case BorderBoxStyle.FourBorders:
          return new Padding((int) this.BorderLeftWidth, (int) this.BorderTopWidth, (int) this.BorderRightWidth, (int) this.BorderBottomWidth);
        case BorderBoxStyle.OuterInnerBorders:
          return new Padding((int) this.BorderWidth * 2);
        default:
          return Padding.Empty;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.alertWindowContentElement = new AlertWindowContentElement();
      this.alertWindowContentElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.alertWindowButtonsPanel = new AlertWindowButtonsPanel();
      this.alertWindowCaptionElement = new AlertWindowCaptionElement();
      int num1 = (int) this.alertWindowCaptionElement.TextAndButtonsElement.TextElement.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadDesktopAlertElement.CaptionTextProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.alertWindowContentElement.BindProperty(LightVisualElement.ImageProperty, (RadObject) this, RadDesktopAlertElement.ContentImageProperty, PropertyBindingOptions.OneWay);
      int num3 = (int) this.alertWindowContentElement.BindProperty(RadItem.TextProperty, (RadObject) this, RadDesktopAlertElement.ContentTextProperty, PropertyBindingOptions.OneWay);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Children.Add((RadElement) this.alertWindowCaptionElement);
      this.Children.Add((RadElement) this.alertWindowContentElement);
      this.Children.Add((RadElement) this.alertWindowButtonsPanel);
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      switch (propertyName)
      {
        case "ShowCloseButton":
          this.alertWindowCaptionElement.TextAndButtonsElement.CloseButton.Visibility = this.showCloseButton ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
        case "ShowPinButton":
          this.alertWindowCaptionElement.TextAndButtonsElement.PinButton.Visibility = this.showPinButton ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
        case "ShowOptionsButton":
          this.alertWindowCaptionElement.TextAndButtonsElement.OptionsButton.Visibility = this.showOptionsButton ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
      }
      base.OnNotifyPropertyChanged(propertyName);
    }
  }
}
