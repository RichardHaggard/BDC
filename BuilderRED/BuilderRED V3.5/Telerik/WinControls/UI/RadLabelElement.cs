// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLabelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadLabelElement : RadItem
  {
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DisplayStyleProperty = RadProperty.Register("DisplayStyle", typeof (DisplayStyle), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DisplayStyle.ImageAndText, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ImageIndexProperty = RadProperty.Register(nameof (ImageIndex), typeof (int), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageKeyProperty = RadProperty.Register(nameof (ImageKey), typeof (string), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageAlignmentProperty = RadProperty.Register(nameof (ImageAlignment), typeof (ContentAlignment), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty TextImageRelationProperty = RadProperty.Register(nameof (TextImageRelation), typeof (TextImageRelation), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextImageRelation.Overlay, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty BorderVisibleProperty = RadProperty.Register(nameof (BorderVisible), typeof (bool), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextAlignmentProperty = RadProperty.Register(nameof (TextAlignment), typeof (ContentAlignment), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private TextPrimitive textPrimitive;
    private ImagePrimitive imagePrimitive;
    private ImageAndTextLayoutPanel layoutPanel;

    static RadLabelElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (RadLabelElement));
    }

    protected override void CreateChildElements()
    {
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.FitToSizeMode = RadFitToSizeMode.FitToParentContent;
      this.borderPrimitive.Visibility = ElementVisibility.Hidden;
      this.borderPrimitive.SmoothingMode = SmoothingMode.Default;
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.BackColor = Color.Transparent;
      this.fillPrimitive.BackColor2 = Color.Transparent;
      this.fillPrimitive.BackColor3 = Color.Transparent;
      this.fillPrimitive.BackColor4 = Color.Transparent;
      this.textPrimitive = new TextPrimitive();
      this.textPrimitive.Alignment = ContentAlignment.MiddleLeft;
      int num1 = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.textPrimitive.BindProperty(RadElement.AlignmentProperty, (RadObject) this, RadLabelElement.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      int num3 = (int) this.textPrimitive.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      this.textPrimitive.AutoEllipsis = true;
      this.textPrimitive.TextWrap = true;
      this.imagePrimitive = new ImagePrimitive();
      int num4 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num5 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadLabelElement.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadLabelElement.ImageProperty, PropertyBindingOptions.TwoWay);
      int num7 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadLabelElement.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.layoutPanel = new ImageAndTextLayoutPanel();
      int num8 = (int) this.layoutPanel.BindProperty(RadElement.StretchHorizontallyProperty, (RadObject) this, RadElement.StretchHorizontallyProperty, PropertyBindingOptions.OneWay);
      int num9 = (int) this.layoutPanel.BindProperty(RadElement.StretchVerticallyProperty, (RadObject) this, RadElement.StretchVerticallyProperty, PropertyBindingOptions.OneWay);
      int num10 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.DisplayStyleProperty, (RadObject) this, RadLabelElement.DisplayStyleProperty, PropertyBindingOptions.OneWay);
      int num11 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.TextAlignmentProperty, (RadObject) this, RadLabelElement.TextAlignmentProperty, PropertyBindingOptions.TwoWay);
      int num12 = (int) this.layoutPanel.BindProperty(RadLabelElement.TextAlignmentProperty, (RadObject) this, RadLabelElement.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      this.layoutPanel.ClipDrawing = true;
      int num13 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.ImageAlignmentProperty, (RadObject) this, RadLabelElement.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      int num14 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.TextImageRelationProperty, (RadObject) this, RadLabelElement.TextImageRelationProperty, PropertyBindingOptions.OneWay);
      this.layoutPanel.Children.Add((RadElement) this.imagePrimitive);
      this.layoutPanel.Children.Add((RadElement) this.textPrimitive);
      this.borderPrimitive.ZIndex = 2;
      int num15 = (int) this.borderPrimitive.BindProperty(RadElement.IsItemFocusedProperty, (RadObject) this, RadElement.IsFocusedProperty, PropertyBindingOptions.OneWay);
      this.layoutPanel.ZIndex = 3;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.layoutPanel);
    }

    [RadPropertyDefaultValue("BorderVisible", typeof (RadLabelElement))]
    public bool BorderVisible
    {
      get
      {
        return (bool) this.GetValue(RadLabelElement.BorderVisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLabelElement.BorderVisibleProperty, (object) value);
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

    [Description("Gets or sets the alignment of text content on the drawing surface.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("TextAlignment", typeof (RadLabelElement))]
    public virtual ContentAlignment TextAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(RadLabelElement.TextAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLabelElement.TextAlignmentProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadDescription("UseMnemonic", typeof (TextPrimitive))]
    [RadDefaultValue("UseMnemonic", typeof (TextPrimitive))]
    [Localizable(true)]
    public bool UseMnemonic
    {
      get
      {
        return this.textPrimitive.UseMnemonic;
      }
      set
      {
        this.textPrimitive.UseMnemonic = value;
      }
    }

    [Localizable(true)]
    [RadPropertyDefaultValue("Image", typeof (RadLabelElement))]
    [Category("Appearance")]
    [Description("Gets or sets the image that is displayed on a button element.")]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image Image
    {
      get
      {
        return (Image) this.GetValue(RadLabelElement.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLabelElement.ImageProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ImageIndex", typeof (RadLabelElement))]
    [Localizable(true)]
    [Description("Gets or sets the image list index value of the image displayed on the button control.")]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual int ImageIndex
    {
      get
      {
        return (int) this.GetValue(RadLabelElement.ImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLabelElement.ImageIndexProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ImageKey", typeof (RadLabelElement))]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Localizable(true)]
    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual string ImageKey
    {
      get
      {
        return (string) this.GetValue(RadLabelElement.ImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLabelElement.ImageKeyProperty, (object) value);
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [RadPropertyDefaultValue("TextImageRelation", typeof (RadLabelElement))]
    [Category("Appearance")]
    [Description("Gets or sets the position of text and image relative to each other.")]
    public virtual TextImageRelation TextImageRelation
    {
      get
      {
        return (TextImageRelation) this.GetValue(RadLabelElement.TextImageRelationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLabelElement.TextImageRelationProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the alignment of image content on the drawing surface.")]
    [RadPropertyDefaultValue("ImageAlignment", typeof (RadLabelElement))]
    public virtual ContentAlignment ImageAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(RadLabelElement.ImageAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLabelElement.ImageAlignmentProperty, (object) value);
      }
    }

    [Description("Gets the element responsible for painting the background of the label")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FillPrimitive LabelFill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    [Description("Gets the element responsible for painting the text of the label")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TextPrimitive LabelText
    {
      get
      {
        return this.textPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the image element responsible for painting the image part of the label")]
    public ImagePrimitive LabelImage
    {
      get
      {
        return this.imagePrimitive;
      }
    }

    [Description("Gets the BorderPrimitive responsible for painting the border part of the label")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BorderPrimitive LabelBorder
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadLabelElement.BorderVisibleProperty)
        this.borderPrimitive.Visibility = (bool) e.NewValue ? ElementVisibility.Visible : ElementVisibility.Hidden;
      else if (e.Property == RadLabelElement.TextAlignmentProperty && this.ElementTree != null)
      {
        RadLabel control = this.ElementTree.Control as RadLabel;
        if (control != null)
          this.layoutPanel.TextAlignment = !control.AutoSize ? this.TextAlignment : (this.RightToLeft ? ContentAlignment.TopRight : ContentAlignment.TopLeft);
      }
      else if (e.Property == RadElement.RightToLeftProperty)
        this.TextAlignment = this.RightToLeft ? ContentAlignment.TopRight : ContentAlignment.TopLeft;
      base.OnPropertyChanged(e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = SizeF.Empty;
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
          if (!this.BypassLayoutPolicies)
          {
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent)
              sz1 = SizeF.Add(sz1, (SizeF) this.Padding.Size);
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent || child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
              sz1 = SizeF.Add(sz1, (SizeF) this.BorderThickness.Size);
          }
          sizeF.Width = Math.Min(availableSize.Width, Math.Max(sizeF.Width, sz1.Width));
          sizeF.Height = Math.Min(availableSize.Height, Math.Max(sizeF.Height, sz1.Height));
        }
      }
      else
      {
        for (int index = 0; index < this.Children.Count; ++index)
          this.Children[index].Measure(availableSize);
        sizeF = (SizeF) this.Size;
      }
      return sizeF;
    }
  }
}
