// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.FillPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  [Editor(typeof (RadFillEditor), typeof (UITypeEditor))]
  public class FillPrimitive : BasePrimitive, IFillElement, IPrimitiveElement, IShapedElement
  {
    public static RadProperty BackColor2Property = RadProperty.Register(nameof (BackColor2), typeof (Color), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.Control, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackColor3Property = RadProperty.Register(nameof (BackColor3), typeof (Color), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackColor4Property = RadProperty.Register(nameof (BackColor4), typeof (Color), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NumberOfColorsProperty = RadProperty.Register(nameof (NumberOfColors), typeof (int), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientStyleProperty = RadProperty.Register(nameof (GradientStyle), typeof (GradientStyles), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Linear, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientAngleProperty = RadProperty.Register(nameof (GradientAngle), typeof (float), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientPercentageProperty = RadProperty.Register(nameof (GradientPercentage), typeof (float), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.5f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientPercentage2Property = RadProperty.Register(nameof (GradientPercentage2), typeof (float), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.666f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty PaintUsingParentShapeProperty = RadProperty.Register(nameof (PaintUsingParentShape), typeof (bool), typeof (FillPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    internal static long ShouldUsePaintBufferStateKey = 137438953472;
    private FillPrimitiveImpl fillPrimitiveImpl;

    public FillPrimitive()
    {
      this.SetBitState(FillPrimitive.ShouldUsePaintBufferStateKey, true);
    }

    protected override void InitializeFields()
    {
      this.fillPrimitiveImpl = new FillPrimitiveImpl((IFillElement) this, (IPrimitiveElement) this);
      base.InitializeFields();
    }

    protected override bool ShouldPaintUsingParentShape
    {
      get
      {
        return this.PaintUsingParentShape;
      }
    }

    [DefaultValue(RadFitToSizeMode.FitToParentPadding)]
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

    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [Description("Second color component when gradient style is other than solid")]
    [RadPropertyDefaultValue("BackColor2", typeof (FillPrimitive))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public virtual Color BackColor2
    {
      get
      {
        return (Color) this.GetValue(FillPrimitive.BackColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.BackColor2Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("BackColor3", typeof (FillPrimitive))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color BackColor3
    {
      get
      {
        return (Color) this.GetValue(FillPrimitive.BackColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.BackColor3Property, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("BackColor4", typeof (FillPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public virtual Color BackColor4
    {
      get
      {
        return (Color) this.GetValue(FillPrimitive.BackColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.BackColor4Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("NumberOfColors", typeof (FillPrimitive))]
    [Description("Maximum number of colors to be used in any of the gradient styles (other than solid). Some styles like \"Glass\" always require using 4 colors, ignoring this property value")]
    [Category("Appearance")]
    public virtual int NumberOfColors
    {
      get
      {
        return (int) this.GetValue(FillPrimitive.NumberOfColorsProperty);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.NumberOfColorsProperty, (object) value);
      }
    }

    [Description("Style of fill to be used")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("GradientStyle", typeof (FillPrimitive))]
    public virtual GradientStyles GradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(FillPrimitive.GradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.GradientStyleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gradient angle to be applied with linear style of fill.")]
    [RadPropertyDefaultValue("GradientAngle", typeof (FillPrimitive))]
    public virtual float GradientAngle
    {
      get
      {
        return (float) this.GetValue(FillPrimitive.GradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.GradientAngleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("For liner gradient style with more than 2 colors, indicates the position of the gradient stop between the first and the second color components. Custom settings for other gradient styles.")]
    [RadPropertyDefaultValue("GradientPercentage", typeof (FillPrimitive))]
    public virtual float GradientPercentage
    {
      get
      {
        return (float) this.GetValue(FillPrimitive.GradientPercentageProperty);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.GradientPercentageProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("GradientPercentage2", typeof (FillPrimitive))]
    public virtual float GradientPercentage2
    {
      get
      {
        return (float) this.GetValue(FillPrimitive.GradientPercentage2Property);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.GradientPercentage2Property, (object) value);
      }
    }

    [Description("Specifies whether the FillPrimitive should fill the GraphicsPath defined by its Parent.Shape. If false, it will fill its bounding rectangle.")]
    [RadPropertyDefaultValue("PaintUsingParentShape", typeof (FillPrimitive))]
    public bool PaintUsingParentShape
    {
      get
      {
        return (bool) this.GetValue(FillPrimitive.PaintUsingParentShapeProperty);
      }
      set
      {
        int num = (int) this.SetValue(FillPrimitive.PaintUsingParentShapeProperty, (object) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldUsePaintBufferState
    {
      get
      {
        return this.BitState[FillPrimitive.ShouldUsePaintBufferStateKey];
      }
      set
      {
        this.SetBitState(FillPrimitive.ShouldUsePaintBufferStateKey, value);
      }
    }

    float IPrimitiveElement.BorderThickness
    {
      get
      {
        if (this.Parent != null)
          return (float) this.Parent.BorderThickness.Left;
        return 0.0f;
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
      return this.BitState[FillPrimitive.ShouldUsePaintBufferStateKey];
    }

    ElementShape IShapedElement.GetCurrentShape()
    {
      return this.GetCurrentShape();
    }

    bool IPrimitiveElement.ShouldUsePaintBuffer()
    {
      return this.ShouldUsePaintBuffer();
    }

    public override void PaintPrimitive(IGraphics g, float angle, SizeF scale)
    {
      this.fillPrimitiveImpl.PaintFill(g, angle, scale);
    }

    public override Filter GetStylablePropertiesFilter()
    {
      return (Filter) new OrFilter(new Filter[3]{ (Filter) PropertyFilter.FillPrimitiveFilter, (Filter) PropertyFilter.AppearanceFilter, (Filter) PropertyFilter.BehaviorFilter });
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == VisualElement.SmoothingModeProperty.Name)
        return new bool?(this.SmoothingMode != SmoothingMode.AntiAlias);
      if (property.Name == RadElement.FitToSizeModeProperty.Name)
        return new bool?(this.FitToSizeMode != RadFitToSizeMode.FitToParentBounds);
      return base.ShouldSerializeProperty(property);
    }

    protected internal override object GetDefaultValue(
      RadPropertyValue propVal,
      object baseDefaultValue)
    {
      RadProperty property = propVal.Property;
      if (property == RadElement.AutoSizeModeProperty)
        return (object) RadAutoSizeMode.Auto;
      if (property == RadElement.FitToSizeModeProperty)
        return (object) RadFitToSizeMode.FitToParentBounds;
      if (property == VisualElement.SmoothingModeProperty)
        return (object) SmoothingMode.AntiAlias;
      return base.GetDefaultValue(propVal, baseDefaultValue);
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      this.fillPrimitiveImpl.OnBoundsChanged((Rectangle) e.OldValue);
      base.OnBoundsChanged(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      this.fillPrimitiveImpl.InvalidateFillCache(e.Property);
      base.OnPropertyChanged(e);
    }
  }
}
