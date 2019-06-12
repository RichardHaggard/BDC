// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.FocusPrimitive
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
  public class FocusPrimitive : BasePrimitive
  {
    public static RadProperty ForeColor2Property = RadProperty.Register(nameof (ForeColor2), typeof (Color), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlDark), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ForeColor3Property = RadProperty.Register(nameof (ForeColor3), typeof (Color), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlDark), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ForeColor4Property = RadProperty.Register(nameof (ForeColor4), typeof (Color), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlDark), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColorProperty = RadProperty.Register(nameof (InnerColor), typeof (Color), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlLightLight), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColor2Property = RadProperty.Register(nameof (InnerColor2), typeof (Color), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.Control), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColor3Property = RadProperty.Register(nameof (InnerColor3), typeof (Color), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlDark), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerColor4Property = RadProperty.Register(nameof (InnerColor4), typeof (Color), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlDarkDark), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty BorderBoxStyleProperty = RadProperty.Register(nameof (BoxStyle), typeof (BorderBoxStyle), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) BorderBoxStyle.SingleBorder, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty GradientStyleProperty = RadProperty.Register(nameof (GradientStyle), typeof (GradientStyles), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Linear, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientAngleProperty = RadProperty.Register(nameof (GradientAngle), typeof (float), typeof (FocusPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f, ElementPropertyOptions.AffectsDisplay));
    private BorderPrimitive border;

    public FocusPrimitive(BorderPrimitive border)
    {
      this.border = border;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.Auto;
      this.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
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

    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("ForeColor2", typeof (FocusPrimitive))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color ForeColor2
    {
      get
      {
        return (Color) this.GetValue(FocusPrimitive.ForeColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.ForeColor2Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("ForeColor3", typeof (FocusPrimitive))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color ForeColor3
    {
      get
      {
        return (Color) this.GetValue(FocusPrimitive.ForeColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.ForeColor3Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ForeColor4", typeof (FocusPrimitive))]
    public virtual Color ForeColor4
    {
      get
      {
        return (Color) this.GetValue(FocusPrimitive.ForeColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.ForeColor4Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("InnerColor", typeof (FocusPrimitive))]
    public virtual Color InnerColor
    {
      get
      {
        return (Color) this.GetValue(FocusPrimitive.InnerColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.InnerColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("InnerColor2", typeof (FocusPrimitive))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public virtual Color InnerColor2
    {
      get
      {
        return (Color) this.GetValue(FocusPrimitive.InnerColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.InnerColor2Property, (object) value);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("InnerColor3", typeof (FocusPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color InnerColor3
    {
      get
      {
        return (Color) this.GetValue(FocusPrimitive.InnerColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.InnerColor3Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("InnerColor4", typeof (FocusPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public virtual Color InnerColor4
    {
      get
      {
        return (Color) this.GetValue(FocusPrimitive.InnerColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.InnerColor4Property, (object) value);
      }
    }

    [Description("Box")]
    [RadPropertyDefaultValue("BoxStyle", typeof (FocusPrimitive))]
    public BorderBoxStyle BoxStyle
    {
      get
      {
        return (BorderBoxStyle) this.GetValue(FocusPrimitive.BorderBoxStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.BorderBoxStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("GradientStyle", typeof (FocusPrimitive))]
    [Category("Appearance")]
    public GradientStyles GradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(FocusPrimitive.GradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.GradientStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("GradientAngle", typeof (FocusPrimitive))]
    [Category("Appearance")]
    public float GradientAngle
    {
      get
      {
        return (float) this.GetValue(FocusPrimitive.GradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(FocusPrimitive.GradientAngleProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.Size.Width <= 0 || this.Size.Height <= 0)
        return;
      Color[] gradientColors1 = new Color[4]{ this.ForeColor, this.ForeColor2, this.ForeColor3, this.ForeColor4 };
      int num = 1;
      if (this.border != null && this.border.BoxStyle == BorderBoxStyle.OuterInnerBorders)
        num = 2;
      Rectangle rectangle1 = new Rectangle(num - 1, num - 1, this.Size.Width - num - 1, this.Size.Height - num - 1);
      this.DrawRectangle(graphics, rectangle1, gradientColors1, 1f);
      Color[] gradientColors2 = new Color[4]{ this.InnerColor, this.InnerColor2, this.InnerColor3, this.InnerColor4 };
      Rectangle rectangle2 = Rectangle.Inflate(rectangle1, -1, -1);
      this.DrawRectangle(graphics, rectangle2, gradientColors2, 1f);
    }

    private void DrawRectangle(
      IGraphics graphics,
      Rectangle rectangle,
      Color[] gradientColors,
      float width)
    {
      if (this.BoxStyle == BorderBoxStyle.FourBorders)
        graphics.DrawRectangle(rectangle, this.ForeColor, PenAlignment.Inset, width);
      else if (this.GradientStyle == GradientStyles.Solid)
      {
        graphics.DrawRectangle(rectangle, gradientColors[0], PenAlignment.Inset, width);
      }
      else
      {
        if (this.GradientStyle != GradientStyles.Linear)
          return;
        graphics.DrawLinearGradientRectangle((RectangleF) rectangle, gradientColors, PenAlignment.Center, width, this.GradientAngle);
      }
    }
  }
}
