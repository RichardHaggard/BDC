// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.BorderPrimitive
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
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class BorderPrimitive : BasePrimitive, IBorderElement, IBoxStyle, IBoxElement, IPrimitiveElement, IShapedElement
  {
    public static readonly RadProperty BorderBoxStyleProperty = RadProperty.Register(nameof (BoxStyle), typeof (BorderBoxStyle), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) BorderBoxStyle.SingleBorder, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty BorderDrawModeProperty = RadProperty.Register(nameof (BorderDrawMode), typeof (BorderDrawModes), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) BorderDrawModes.RightOverTop, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty WidthProperty = RadProperty.Register(nameof (Width), typeof (float), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty LeftWidthProperty = RadProperty.Register(nameof (LeftWidth), typeof (float), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty TopWidthProperty = RadProperty.Register(nameof (TopWidth), typeof (float), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty RightWidthProperty = RadProperty.Register(nameof (RightWidth), typeof (float), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty BottomWidthProperty = RadProperty.Register(nameof (BottomWidth), typeof (float), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty LeftColorProperty = RadProperty.Register(nameof (LeftColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty TopColorProperty = RadProperty.Register(nameof (TopColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty RightColorProperty = RadProperty.Register(nameof (RightColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty BottomColorProperty = RadProperty.Register(nameof (BottomColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty LeftShadowColorProperty = RadProperty.Register(nameof (LeftShadowColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty TopShadowColorProperty = RadProperty.Register(nameof (TopShadowColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty RightShadowColorProperty = RadProperty.Register(nameof (RightShadowColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty BottomShadowColorProperty = RadProperty.Register(nameof (BottomShadowColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientAngleProperty = RadProperty.Register(nameof (GradientAngle), typeof (float), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 270f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientStyleProperty = RadProperty.Register(nameof (GradientStyle), typeof (GradientStyles), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Solid, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ForeColor2Property = RadProperty.Register(nameof (ForeColor2), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ForeColor3Property = RadProperty.Register(nameof (ForeColor3), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ForeColor4Property = RadProperty.Register(nameof (ForeColor4), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColorProperty = RadProperty.Register(nameof (InnerColor), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColor2Property = RadProperty.Register(nameof (InnerColor2), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.Control, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColor3Property = RadProperty.Register(nameof (InnerColor3), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColor4Property = RadProperty.Register(nameof (InnerColor4), typeof (Color), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDarkDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty PaintUsingParentShapeProperty = RadProperty.Register(nameof (PaintUsingParentShape), typeof (bool), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty BorderDashStyleProperty = RadProperty.Register(nameof (BorderDashStyle), typeof (DashStyle), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DashStyle.Solid, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty BorderDashPatternProperty = RadProperty.Register(nameof (BorderDashPattern), typeof (float[]), typeof (BorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private Padding ownBorderThickness = Padding.Empty;
    private BorderPrimitiveImpl borderPrimitiveImpl;
    private BorderPrimitive childPrimitive;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.borderPrimitiveImpl = new BorderPrimitiveImpl((IBorderElement) this, (IPrimitiveElement) this);
    }

    float IPrimitiveElement.BorderThickness
    {
      get
      {
        return this.Width;
      }
    }

    RectangleF IPrimitiveElement.GetPaintRectangle(
      float left,
      float angle,
      SizeF scale)
    {
      return this.GetPaintRectangle(left, angle, scale);
    }

    RectangleF IPrimitiveElement.GetExactPaintingRectangle(
      float angle,
      SizeF scale)
    {
      return this.GetPatchedRect(new RectangleF(0.0f, 0.0f, (float) (this.Size.Width - 1), (float) (this.Size.Height - 1)), angle, scale);
    }

    bool IPrimitiveElement.IsDesignMode
    {
      get
      {
        return this.IsDesignMode;
      }
    }

    protected virtual bool ShouldUsePaintBuffer()
    {
      return true;
    }

    ElementShape IShapedElement.GetCurrentShape()
    {
      return this.GetCurrentShape();
    }

    bool IPrimitiveElement.ShouldUsePaintBuffer()
    {
      return this.ShouldUsePaintBuffer();
    }

    public override Telerik.WinControls.Filter GetStylablePropertiesFilter()
    {
      return (Telerik.WinControls.Filter) new OrFilter(new Telerik.WinControls.Filter[3]{ (Telerik.WinControls.Filter) PropertyFilter.BorderPrimitiveFilter, (Telerik.WinControls.Filter) PropertyFilter.AppearanceFilter, (Telerik.WinControls.Filter) PropertyFilter.BehaviorFilter });
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.childPrimitive != null)
        this.childPrimitive.Measure(new SizeF(availableSize.Width - (float) this.ownBorderThickness.Horizontal, availableSize.Height - (float) this.ownBorderThickness.Vertical));
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      if (this.childPrimitive != null)
        this.childPrimitive.Arrange(new RectangleF((PointF) new Point(this.ownBorderThickness.Left, this.ownBorderThickness.Top), new SizeF(finalSize.Width - (float) this.ownBorderThickness.Horizontal, finalSize.Height - (float) this.ownBorderThickness.Vertical)));
      return sizeF;
    }

    protected internal override object GetDefaultValue(
      RadPropertyValue propVal,
      object baseDefaultValue)
    {
      RadProperty property = propVal.Property;
      if (property == VisualElement.ForeColorProperty)
        return (object) SystemColors.ControlDarkDark;
      if (property == RadElement.AutoSizeModeProperty)
        return (object) RadAutoSizeMode.Auto;
      if (property == RadElement.FitToSizeModeProperty)
        return (object) RadFitToSizeMode.FitToParentBounds;
      if (property != VisualElement.SmoothingModeProperty)
        return base.GetDefaultValue(propVal, baseDefaultValue);
      if (this.BoxStyle == BorderBoxStyle.FourBorders && this.GetCurrentShape() == null)
        return (object) SmoothingMode.Default;
      return (object) SmoothingMode.AntiAlias;
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == VisualElement.SmoothingModeProperty.Name)
        return new bool?(this.SmoothingMode != SmoothingMode.AntiAlias);
      if (property.Name == RadElement.FitToSizeModeProperty.Name)
        return new bool?(this.FitToSizeMode != RadFitToSizeMode.FitToParentBounds);
      return base.ShouldSerializeProperty(property);
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      this.borderPrimitiveImpl.PaintBorder(graphics, angle, scale);
    }

    protected override bool ShouldPaintUsingParentShape
    {
      get
      {
        return this.PaintUsingParentShape;
      }
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      this.SynchronizeWithParentBorderThickness();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == BorderPrimitive.BorderBoxStyleProperty || e.Property == BorderPrimitive.BorderDrawModeProperty || (e.Property == BorderPrimitive.WidthProperty || e.Property == BorderPrimitive.LeftWidthProperty) || (e.Property == BorderPrimitive.TopWidthProperty || e.Property == BorderPrimitive.RightWidthProperty || (e.Property == BorderPrimitive.BottomWidthProperty || e.Property == RadElement.VisibilityProperty)))
      {
        this.ownBorderThickness = this.GetOwnBorderThickness();
        this.SynchronizeWithParentBorderThickness();
      }
      base.OnPropertyChanged(e);
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      base.OnChildrenChanged(child, changeOperation);
      if (!(child is BorderPrimitive))
        return;
      this.SynchronizeWithParentBorderThickness();
      if (this.childPrimitive == null)
      {
        this.childPrimitive = child as BorderPrimitive;
      }
      else
      {
        if (!(child is BorderPrimitive) || this.childPrimitive != child || changeOperation != ItemsChangeOperation.Removed)
          return;
        this.childPrimitive = (BorderPrimitive) null;
        foreach (RadElement child1 in this.Children)
        {
          if (child1 is BorderPrimitive)
          {
            this.childPrimitive = child1 as BorderPrimitive;
            this.SynchronizeWithParentBorderThickness();
            break;
          }
        }
      }
    }

    private void SynchronizeWithParentBorderThickness()
    {
      RadElement parent = this.Parent;
      if (parent is BorderPrimitive || parent == null)
        return;
      if (this.Visibility == ElementVisibility.Collapsed)
        parent.BorderThickness = new Padding(0);
      else
        parent.BorderThickness = TelerikDpiHelper.ScalePadding(this.GetBorderThickness(), new SizeF(1f / this.DpiScaleFactor.Width, 1f / this.DpiScaleFactor.Height));
    }

    private Padding GetOwnBorderThickness()
    {
      Padding padding = new Padding();
      switch (this.BoxStyle)
      {
        case BorderBoxStyle.SingleBorder:
          padding.All = (int) Math.Round((double) this.Width, MidpointRounding.AwayFromZero);
          break;
        case BorderBoxStyle.FourBorders:
          padding.Left = (int) Math.Round((double) this.LeftWidth, MidpointRounding.AwayFromZero);
          padding.Top = (int) Math.Round((double) this.TopWidth, MidpointRounding.AwayFromZero);
          padding.Right = (int) Math.Round((double) this.RightWidth, MidpointRounding.AwayFromZero);
          padding.Bottom = (int) Math.Round((double) this.BottomWidth, MidpointRounding.AwayFromZero);
          break;
        case BorderBoxStyle.OuterInnerBorders:
          int num = (int) Math.Round((double) this.Width, MidpointRounding.AwayFromZero);
          padding.All = num;
          break;
      }
      return padding;
    }

    public Padding GetBorderThickness()
    {
      Padding p1 = this.ownBorderThickness = this.GetOwnBorderThickness();
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if (this.Children[index] is BorderPrimitive)
        {
          BorderPrimitive child = this.Children[index] as BorderPrimitive;
          p1 = Padding.Add(p1, child.GetBorderThickness());
        }
      }
      this.BorderThickness = p1;
      return p1;
    }

    [DefaultValue(RadFitToSizeMode.FitToParentBounds)]
    public override RadFitToSizeMode FitToSizeMode
    {
      get
      {
        return base.FitToSizeMode;
      }
      set
      {
        base.FitToSizeMode = value;
      }
    }

    [RadPropertyDefaultValue("BoxStyle", typeof (BorderPrimitive))]
    [Category("Box")]
    [Description("Determine the sizing style of the border's sides")]
    public BorderBoxStyle BoxStyle
    {
      get
      {
        return (BorderBoxStyle) this.GetValue(BorderPrimitive.BorderBoxStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.BorderBoxStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderDrawModes", typeof (BorderPrimitive))]
    [Category("Box")]
    public BorderDrawModes BorderDrawMode
    {
      get
      {
        return (BorderDrawModes) this.GetValue(BorderPrimitive.BorderDrawModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.BorderDrawModeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Width", typeof (BorderPrimitive))]
    [Category("Box")]
    [Description("Gets or sets the thickness of the border (if its BoxStyle is SingleBorder)")]
    public float Width
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(BorderPrimitive.WidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.WidthProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("LeftWidth", typeof (BorderPrimitive))]
    public float LeftWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(BorderPrimitive.LeftWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.LeftWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TopWidth", typeof (BorderPrimitive))]
    [Category("Box")]
    public float TopWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(BorderPrimitive.TopWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.TopWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("RightWidth", typeof (BorderPrimitive))]
    [Category("Box")]
    public float RightWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(BorderPrimitive.RightWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.RightWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BottomWidth", typeof (BorderPrimitive))]
    [Category("Box")]
    public float BottomWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(BorderPrimitive.BottomWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.BottomWidthProperty, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("LeftColor", typeof (BorderPrimitive))]
    [Category("Box")]
    public Color LeftColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.LeftColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.LeftColorProperty, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("TopColor", typeof (BorderPrimitive))]
    [Category("Box")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color TopColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.TopColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.TopColorProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("RightColor", typeof (BorderPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public Color RightColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.RightColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.RightColorProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("BottomColor", typeof (BorderPrimitive))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color BottomColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.BottomColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.BottomColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("LeftShadowColor", typeof (BorderPrimitive))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Box")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color LeftShadowColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.LeftShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.LeftShadowColorProperty, (object) value);
      }
    }

    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("TopShadowColor", typeof (BorderPrimitive))]
    [Category("Box")]
    public Color TopShadowColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.TopShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.TopShadowColorProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("RightShadowColor", typeof (BorderPrimitive))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color RightShadowColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.RightShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.RightShadowColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BottomShadowColor", typeof (BorderPrimitive))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Box")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color BottomShadowColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.BottomShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.BottomShadowColorProperty, (object) value);
      }
    }

    [Description("Specifies whether the BorderPrimitive should draw the GraphicsPath defined by its Parent.Shape. If false, it will draw its bounding rectangle.")]
    [RadPropertyDefaultValue("PaintUsingParentShape", typeof (BorderPrimitive))]
    public bool PaintUsingParentShape
    {
      get
      {
        return (bool) this.GetValue(BorderPrimitive.PaintUsingParentShapeProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.PaintUsingParentShapeProperty, (object) value);
      }
    }

    [Description("Specifies the style of dashed lines drawn with a border")]
    [RadPropertyDefaultValue("BorderDashStyle", typeof (BorderPrimitive))]
    public DashStyle BorderDashStyle
    {
      get
      {
        return (DashStyle) this.GetValue(BorderPrimitive.BorderDashStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.BorderDashStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderDashPattern", typeof (BorderPrimitive))]
    [Description("Specifies the pattern of dashed lines drawn when the BorderDashStyle is custom.")]
    public float[] BorderDashPattern
    {
      get
      {
        return (float[]) this.GetValue(BorderPrimitive.BorderDashPatternProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.BorderDashPatternProperty, (object) value);
      }
    }

    [Browsable(false)]
    public SizeF Offset
    {
      get
      {
        if (this.BoxStyle != BorderBoxStyle.SingleBorder)
          return new SizeF(this.LeftWidth, this.TopWidth);
        float width = this.Width;
        return new SizeF(width, width);
      }
    }

    [Browsable(false)]
    public SizeF BorderSize
    {
      get
      {
        return new SizeF(this.HorizontalWidth, this.VerticalWidth);
      }
    }

    [Browsable(false)]
    public float HorizontalWidth
    {
      get
      {
        if (this.BoxStyle == BorderBoxStyle.SingleBorder)
          return 2f * this.Width;
        return this.LeftWidth + this.RightWidth;
      }
    }

    [Browsable(false)]
    public float VerticalWidth
    {
      get
      {
        if (this.BoxStyle == BorderBoxStyle.SingleBorder)
          return 2f * this.Width;
        return this.TopWidth + this.BottomWidth;
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("GradientAngle", typeof (BorderPrimitive))]
    public float GradientAngle
    {
      get
      {
        return (float) this.GetValue(BorderPrimitive.GradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.GradientAngleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("GradientStyle", typeof (BorderPrimitive))]
    [Category("Appearance")]
    public GradientStyles GradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(BorderPrimitive.GradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.GradientStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ForeColor2", typeof (BorderPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    public virtual Color ForeColor2
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.ForeColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.ForeColor2Property, (object) value);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("ForeColor3", typeof (BorderPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color ForeColor3
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.ForeColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.ForeColor3Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("ForeColor4", typeof (BorderPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public virtual Color ForeColor4
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.ForeColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.ForeColor4Property, (object) value);
      }
    }

    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("InnerColor", typeof (BorderPrimitive))]
    [Category("Appearance")]
    public virtual Color InnerColor
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.InnerColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.InnerColorProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("InnerColor2", typeof (BorderPrimitive))]
    public virtual Color InnerColor2
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.InnerColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.InnerColor2Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("InnerColor3", typeof (BorderPrimitive))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color InnerColor3
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.InnerColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.InnerColor3Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("InnerColor4", typeof (BorderPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    public virtual Color InnerColor4
    {
      get
      {
        return (Color) this.GetValue(BorderPrimitive.InnerColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(BorderPrimitive.InnerColor4Property, (object) value);
      }
    }
  }
}
