// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadButtonElement : RadButtonItem
  {
    public static RadProperty LargeImageProperty = RadProperty.Register(nameof (LargeImage), typeof (Image), typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty LargeImageIndexProperty = RadProperty.Register(nameof (LargeImageIndex), typeof (int), typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty LargeImageKeyProperty = RadProperty.Register(nameof (LargeImageKey), typeof (string), typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty SmallImageProperty = RadProperty.Register(nameof (SmallImage), typeof (Image), typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty SmallImageIndexProperty = RadProperty.Register(nameof (SmallImageIndex), typeof (int), typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty SmallImageKeyProperty = RadProperty.Register(nameof (SmallImageKey), typeof (string), typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty UseSmallImageListProperty = RadProperty.Register(nameof (UseSmallImageList), typeof (bool), typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private TextPrimitive textPrimitive;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private FocusPrimitive focusPrimitive;
    private ImageAndTextLayoutPanel layoutPanel;
    private ImagePrimitive imagePrimitive;

    static RadButtonElement()
    {
      RadElement.CanFocusProperty.OverrideMetadata(typeof (RadButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    }

    public RadButtonElement()
    {
    }

    public RadButtonElement(string text)
      : base(text)
    {
    }

    public RadButtonElement(string text, Image image)
      : base(text, image)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.CanFocus = true;
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "ButtonFill";
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "ButtonBorder";
      this.borderPrimitive.ZIndex = 2;
      int num1 = (int) this.borderPrimitive.BindProperty(RadElement.IsItemFocusedProperty, (RadObject) this, RadElement.IsFocusedProperty, PropertyBindingOptions.OneWay);
      this.textPrimitive = new TextPrimitive();
      int num2 = (int) this.textPrimitive.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      int num3 = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
      int num4 = (int) this.textPrimitive.BindProperty(TextPrimitive.TextAlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      this.imagePrimitive = new ImagePrimitive();
      int num5 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num6 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num7 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num8 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      int num9 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.UseSmallImageListProperty, (RadObject) this, RadButtonElement.UseSmallImageListProperty, PropertyBindingOptions.TwoWay);
      int num10 = (int) this.imagePrimitive.BindProperty(RadElement.AlignmentProperty, (RadObject) this, RadButtonItem.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      this.layoutPanel = new ImageAndTextLayoutPanel();
      this.layoutPanel.ZIndex = 3;
      int num11 = (int) this.layoutPanel.BindProperty(RadElement.StretchHorizontallyProperty, (RadObject) this, RadElement.StretchHorizontallyProperty, PropertyBindingOptions.OneWay);
      int num12 = (int) this.layoutPanel.BindProperty(RadElement.StretchVerticallyProperty, (RadObject) this, RadElement.StretchVerticallyProperty, PropertyBindingOptions.OneWay);
      int num13 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.DisplayStyleProperty, (RadObject) this, RadButtonItem.DisplayStyleProperty, PropertyBindingOptions.OneWay);
      int num14 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.ImageAlignmentProperty, (RadObject) this, RadButtonItem.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      int num15 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.TextAlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      int num16 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.TextImageRelationProperty, (RadObject) this, RadButtonItem.TextImageRelationProperty, PropertyBindingOptions.OneWay);
      this.layoutPanel.Children.Add((RadElement) this.imagePrimitive);
      this.layoutPanel.Children.Add((RadElement) this.textPrimitive);
      this.focusPrimitive = new FocusPrimitive(this.borderPrimitive);
      this.focusPrimitive.Class = "ButtonFocus";
      this.focusPrimitive.ZIndex = 4;
      this.focusPrimitive.Visibility = ElementVisibility.Hidden;
      int num17 = (int) this.focusPrimitive.BindProperty(RadElement.IsItemFocusedProperty, (RadObject) this, RadElement.IsFocusedProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.layoutPanel);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.focusPrimitive);
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FillPrimitive ButtonFillElement
    {
      get
      {
        return this.fillPrimitive;
      }
      protected set
      {
        this.fillPrimitive = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    public BorderPrimitive BorderElement
    {
      get
      {
        return this.borderPrimitive;
      }
      protected set
      {
        this.borderPrimitive = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TextPrimitive TextElement
    {
      get
      {
        return this.textPrimitive;
      }
      protected set
      {
        this.textPrimitive = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ImagePrimitive ImagePrimitive
    {
      get
      {
        return this.imagePrimitive;
      }
      protected set
      {
        this.imagePrimitive = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FocusPrimitive FocusPrimitive
    {
      get
      {
        return this.focusPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ImageAndTextLayoutPanel LayoutPanel
    {
      get
      {
        return this.layoutPanel;
      }
      protected set
      {
        this.layoutPanel = value;
      }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the large image that is displayed on a button element.")]
    [Browsable(false)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RadPropertyDefaultValue("LargeImage", typeof (RadButtonElement))]
    public virtual Image LargeImage
    {
      get
      {
        return (Image) this.GetValue(RadButtonElement.LargeImageProperty);
      }
    }

    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Description("Gets the large image list index value of the image displayed on the button control.")]
    [Browsable(false)]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [RadPropertyDefaultValue("LargeImageIndex", typeof (RadButtonElement))]
    public virtual int LargeImageIndex
    {
      get
      {
        return (int) this.GetValue(RadButtonElement.LargeImageIndexProperty);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the large key accessor for the image in the ImageList.")]
    [RadPropertyDefaultValue("LargeImageKey", typeof (RadButtonElement))]
    [Category("Appearance")]
    [Browsable(false)]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual string LargeImageKey
    {
      get
      {
        return (string) this.GetValue(RadButtonElement.LargeImageKeyProperty);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Appearance")]
    [Description("Gets or sets the large image that is displayed on a button element.")]
    [RadPropertyDefaultValue("SmallImage", typeof (RadButtonElement))]
    public virtual Image SmallImage
    {
      get
      {
        return (Image) this.GetValue(RadButtonElement.SmallImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonElement.SmallImageProperty, (object) value);
      }
    }

    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ElementTree.Control.SmallImageList")]
    [RadPropertyDefaultValue("SmallImageIndex", typeof (RadButtonElement))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Category("Appearance")]
    [Description("Gets or sets the small image list index value of the image displayed on the button control.")]
    public virtual int SmallImageIndex
    {
      get
      {
        return (int) this.GetValue(RadButtonElement.SmallImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonElement.SmallImageIndexProperty, (object) value);
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("SmallImageKey", typeof (RadButtonElement))]
    [Category("Appearance")]
    [Description("Gets or sets the small key accessor for the image in the ImageList.")]
    [RelatedImageList("ElementTree.Control.SmallImageList")]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual string SmallImageKey
    {
      get
      {
        return (string) this.GetValue(RadButtonElement.SmallImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonElement.SmallImageKeyProperty, (object) value);
      }
    }

    [Browsable(false)]
    public virtual bool UseSmallImageList
    {
      get
      {
        return (bool) this.GetValue(RadButtonElement.UseSmallImageListProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonElement.UseSmallImageListProperty, (object) value);
      }
    }

    [Description("Angle of rotation for the button image")]
    [RadDefaultValue("AngleTransform", typeof (ImagePrimitive))]
    [Category("Behavior")]
    public float ImagePrimitiveAngleTransform
    {
      get
      {
        if (this.imagePrimitive != null)
          return this.imagePrimitive.AngleTransform;
        return 0.0f;
      }
      set
      {
        if (this.imagePrimitive == null)
          return;
        this.imagePrimitive.AngleTransform = value;
      }
    }

    [Description("True if the text should wrap to the available layout rectangle otherwise, false.")]
    [RadPropertyDefaultValue("TextWrap", typeof (TextPrimitive))]
    [Category("Appearance")]
    public bool TextWrap
    {
      get
      {
        return this.textPrimitive.TextWrap;
      }
      set
      {
        this.textPrimitive.TextWrap = value;
      }
    }

    [Localizable(true)]
    [DefaultValue(true)]
    [Description("Includes the trailing space at the end of each line. By default the boundary rectangle returned by the Overload:System.Drawing.Graphics.MeasureString method excludes the space at the end of each line. Set this flag to include that space in measurement.")]
    [Category("Appearance")]
    public bool MeasureTrailingSpaces
    {
      get
      {
        return this.textPrimitive.MeasureTrailingSpaces;
      }
      set
      {
        this.textPrimitive.MeasureTrailingSpaces = value;
      }
    }

    [Description("Gets or sets a value indicating whether the border is shown.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool ShowBorder
    {
      get
      {
        return this.borderPrimitive.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.borderPrimitive.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsCancelClicked
    {
      get
      {
        return this.BitState[70368744177664L];
      }
      set
      {
        this.SetBitState(70368744177664L, value);
      }
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      if (!this.Enabled)
        return VisualStyleElement.Button.PushButton.Disabled;
      if (this.IsMouseDown)
      {
        if (!this.IsMouseOver)
          return VisualStyleElement.Button.PushButton.Hot;
        return VisualStyleElement.Button.PushButton.Pressed;
      }
      if (this.IsMouseOver)
        return VisualStyleElement.Button.PushButton.Hot;
      if (!this.IsDefault)
        return VisualStyleElement.Button.PushButton.Normal;
      return VisualStyleElement.Button.PushButton.Default;
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return this.GetXPVisualStyle();
    }

    protected override bool ShouldPaintChild(RadElement element)
    {
      bool? paintSystemSkin = this.paintSystemSkin;
      if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) != 0 && (element == this.fillPrimitive || element == this.borderPrimitive))
        return false;
      return base.ShouldPaintChild(element);
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      this.textPrimitive.ForeColor = SystemSkinManager.Instance.Renderer.GetColor(ColorProperty.TextColor);
    }

    protected override void OnClick(EventArgs e)
    {
      MouseEventArgs mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs != null && mouseEventArgs.Button != MouseButtons.Left)
        return;
      base.OnClick(e);
      if (this.ElementTree == null || this.ElementTree.Control == null || this.textPrimitive == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "Click", (object) this.textPrimitive.Text);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return this.MeasureButtonChildren(availableSize);
    }

    protected SizeF MeasureButtonChildren(SizeF availableSize)
    {
      SizeF sizeF1 = SizeF.Empty;
      if (this.AutoSize)
      {
        for (int index = 0; index < this.Children.Count; ++index)
        {
          RadElement child = this.Children[index];
          if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent || child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
            child.Measure(SizeF.Subtract(SizeF.Subtract(availableSize, (SizeF) this.Padding.Size), (SizeF) this.BorderThickness.Size));
          else
            child.Measure(availableSize);
          SizeF sz1 = child.DesiredSize;
          if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent)
            sz1 = SizeF.Add(sz1, (SizeF) this.Padding.Size);
          if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent || child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
          {
            SizeF sizeF2 = SizeF.Add(sz1, (SizeF) this.BorderThickness.Size);
            float width = sizeF2.Width;
            float height = sizeF2.Height;
            if (this.ElementTree.RootElement.MaxSize.Width > 0 && (double) sizeF2.Width > (double) this.ElementTree.Control.MaximumSize.Width)
              width = (float) this.ElementTree.Control.MaximumSize.Width;
            if (this.MaxSize.Width > 0 && (double) sizeF2.Width > (double) this.MaxSize.Width)
              width = (float) this.MaxSize.Width;
            if (this.ElementTree.RootElement.MaxSize.Height > 0 && (double) sizeF2.Height > (double) this.ElementTree.Control.MaximumSize.Height)
              height = (float) this.ElementTree.Control.MaximumSize.Height;
            if (this.MaxSize.Height > 0 && (double) sizeF2.Height > (double) this.MaxSize.Height)
              height = (float) this.MaxSize.Height;
            sz1 = new SizeF(width, height);
          }
          sizeF1.Width = Math.Min(availableSize.Width, Math.Max(sizeF1.Width, sz1.Width));
          sizeF1.Height = Math.Min(availableSize.Height, Math.Max(sizeF1.Height, sz1.Height));
        }
      }
      else
      {
        for (int index = 0; index < this.Children.Count; ++index)
          this.Children[index].Measure(availableSize);
        sizeF1 = (SizeF) this.Size;
      }
      return sizeF1;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
    }
  }
}
