// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLineItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadLineItem : RadItem
  {
    public static RadProperty SweepAngleProperty = RadProperty.Register(nameof (SweepAngle), typeof (int), typeof (RadLineItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineWidthProperty = RadProperty.Register(nameof (LineWidth), typeof (int), typeof (RadLineItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OrientationProperty = RadProperty.Register("Orientation", typeof (SepOrientation), typeof (RadLineItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SepOrientation.Horizontal, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineColorProperty = RadProperty.Register(nameof (LineColor), typeof (Color), typeof (RadLineItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Black, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineColor2Property = RadProperty.Register(nameof (LineColor2), typeof (Color), typeof (RadLineItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineStyleProperty = RadProperty.Register(nameof (LineStyle), typeof (RadLineItem.LineDrawingStyle), typeof (RadLineItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadLineItem.LineDrawingStyle.Bevel, ElementPropertyOptions.AffectsDisplay));
    private LinePrimitive linePrimitive;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
    }

    public virtual int LineWidth
    {
      get
      {
        return (int) this.GetValue(RadLineItem.LineWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLineItem.LineWidthProperty, (object) value);
      }
    }

    public virtual SepOrientation SeparatorOrientation
    {
      get
      {
        return (SepOrientation) this.GetValue(RadLineItem.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLineItem.OrientationProperty, (object) value);
      }
    }

    public virtual int SweepAngle
    {
      get
      {
        return (int) this.GetValue(RadLineItem.SweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLineItem.SweepAngleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("LineColor", typeof (RadLineItem))]
    public virtual Color LineColor
    {
      get
      {
        return (Color) this.GetValue(RadLineItem.LineColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLineItem.LineColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("LineColor2", typeof (RadLineItem))]
    public virtual Color LineColor2
    {
      get
      {
        return (Color) this.GetValue(RadLineItem.LineColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(RadLineItem.LineColor2Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("LineStyle", typeof (RadLineItem))]
    public virtual RadLineItem.LineDrawingStyle LineStyle
    {
      get
      {
        return (RadLineItem.LineDrawingStyle) this.GetValue(RadLineItem.LineStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadLineItem.LineStyleProperty, (object) value);
      }
    }

    protected override void CreateChildElements()
    {
      this.linePrimitive = new LinePrimitive();
      this.linePrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.linePrimitive.BackColor = Color.Black;
      this.linePrimitive.BackColor2 = Color.White;
      this.linePrimitive.BackColor3 = Color.White;
      this.linePrimitive.NumberOfColors = 3;
      this.linePrimitive.GradientStyle = GradientStyles.Linear;
      int num1 = (int) this.linePrimitive.BindProperty(VisualElement.BackColorProperty, (RadObject) this, RadLineItem.LineColorProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.linePrimitive.BindProperty(FillPrimitive.BackColor2Property, (RadObject) this, RadLineItem.LineColor2Property, PropertyBindingOptions.OneWay);
      int num3 = (int) this.linePrimitive.BindProperty(FillPrimitive.BackColor3Property, (RadObject) this, RadLineItem.LineColor2Property, PropertyBindingOptions.OneWay);
      int num4 = (int) this.linePrimitive.BindProperty(LinePrimitive.LineWidthProperty, (RadObject) this, RadLineItem.LineWidthProperty, PropertyBindingOptions.OneWay);
      int num5 = (int) this.linePrimitive.BindProperty(LinePrimitive.SweepAngleProperty, (RadObject) this, RadLineItem.SweepAngleProperty, PropertyBindingOptions.OneWay);
      int num6 = (int) this.linePrimitive.BindProperty(LinePrimitive.OrientationProperty, (RadObject) this, RadLineItem.OrientationProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.linePrimitive);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadLineItem.LineStyleProperty)
      {
        switch ((RadLineItem.LineDrawingStyle) e.NewValue)
        {
          case RadLineItem.LineDrawingStyle.Flat:
            this.linePrimitive.NumberOfColors = 1;
            this.UnbindBackColorProperties();
            int num1 = (int) this.linePrimitive.BindProperty(VisualElement.BackColorProperty, (RadObject) this, RadLineItem.LineColorProperty, PropertyBindingOptions.OneWay);
            break;
          case RadLineItem.LineDrawingStyle.Bevel:
            this.linePrimitive.NumberOfColors = 3;
            this.UnbindBackColorProperties();
            int num2 = (int) this.linePrimitive.BindProperty(VisualElement.BackColorProperty, (RadObject) this, RadLineItem.LineColorProperty, PropertyBindingOptions.OneWay);
            int num3 = (int) this.linePrimitive.BindProperty(FillPrimitive.BackColor2Property, (RadObject) this, RadLineItem.LineColor2Property, PropertyBindingOptions.OneWay);
            int num4 = (int) this.linePrimitive.BindProperty(FillPrimitive.BackColor3Property, (RadObject) this, RadLineItem.LineColor2Property, PropertyBindingOptions.OneWay);
            break;
          case RadLineItem.LineDrawingStyle.Emboss:
            this.linePrimitive.NumberOfColors = 3;
            this.UnbindBackColorProperties();
            int num5 = (int) this.linePrimitive.BindProperty(VisualElement.BackColorProperty, (RadObject) this, RadLineItem.LineColor2Property, PropertyBindingOptions.OneWay);
            int num6 = (int) this.linePrimitive.BindProperty(FillPrimitive.BackColor2Property, (RadObject) this, RadLineItem.LineColorProperty, PropertyBindingOptions.OneWay);
            int num7 = (int) this.linePrimitive.BindProperty(FillPrimitive.BackColor3Property, (RadObject) this, RadLineItem.LineColorProperty, PropertyBindingOptions.OneWay);
            break;
        }
      }
      base.OnPropertyChanged(e);
    }

    private void UnbindBackColorProperties()
    {
      int num1 = (int) this.linePrimitive.UnbindProperty(VisualElement.BackColorProperty);
      int num2 = (int) this.linePrimitive.UnbindProperty(FillPrimitive.BackColor2Property);
      int num3 = (int) this.linePrimitive.UnbindProperty(FillPrimitive.BackColor3Property);
    }

    public enum LineDrawingStyle
    {
      Flat,
      Bevel,
      Emboss,
    }
  }
}
