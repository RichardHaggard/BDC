// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTrackBarItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class RadTrackBarItem : RadItem
  {
    public static RadProperty TickStyleProperty = RadProperty.Register(nameof (TickStyle), typeof (TickStyles), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TickStyles.Both, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty MinimumProperty = RadProperty.Register("Minimum", typeof (int), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MaximumProperty = RadProperty.Register("Maximum", typeof (int), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LargeChangeProperty = RadProperty.Register(nameof (LargeChange), typeof (int), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SmallChangeProperty = RadProperty.Register(nameof (SmallChange), typeof (int), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowTicksProperty = RadProperty.Register(nameof (ShowTicks), typeof (bool), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FitToAvailableSizeProperty = RadProperty.Register(nameof (FitToAvailableSize), typeof (bool), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowSlideAreaProperty = RadProperty.Register(nameof (ShowSlideArea), typeof (bool), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ValueProperty = RadProperty.Register("Value", typeof (int), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SlideAreaWidthProperty = RadProperty.Register(nameof (SlideAreaWidth), typeof (int), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TrackBarOrientationProperty = RadProperty.Register(nameof (TrackBarOrientation), typeof (Orientation), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty TickFrequencyProperty = RadProperty.Register(nameof (TickFrequency), typeof (int), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SliderAreaGradientColor1Property = RadProperty.Register(nameof (SliderAreaGradientColor1), typeof (Color), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TickColorProperty = RadProperty.Register(nameof (TickColor), typeof (Color), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SliderAreaGradientColor2Property = RadProperty.Register(nameof (SliderAreaGradientColor2), typeof (Color), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Black, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SliderAreaGradientAngleProperty = RadProperty.Register(nameof (SliderAreaGradientAngle), typeof (float), typeof (RadTrackBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.0f, ElementPropertyOptions.AffectsDisplay));

    [RadPropertyDefaultValue("SliderAreaGradientColor1", typeof (RadTrackBarItem))]
    [Description("Gets or sets SliderArea's first background color.")]
    public virtual Color SliderAreaGradientColor1
    {
      get
      {
        return (Color) this.GetValue(RadTrackBarItem.SliderAreaGradientColor1Property);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.SliderAreaGradientColor1Property, (object) value);
      }
    }

    [Description("Gets or sets SliderArea's second background color.")]
    [RadPropertyDefaultValue("SliderAreaGradientColor2", typeof (RadTrackBarItem))]
    public virtual Color SliderAreaGradientColor2
    {
      get
      {
        return (Color) this.GetValue(RadTrackBarItem.SliderAreaGradientColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.SliderAreaGradientColor2Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("TickColor", typeof (RadTrackBarItem))]
    [Description("Gets or sets RadTrackBar's ticks color")]
    public virtual Color TickColor
    {
      get
      {
        return (Color) this.GetValue(RadTrackBarItem.TickColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.TickColorProperty, (object) value);
      }
    }

    [Description("Gets or sets the gradient angle of the SliderArea.")]
    [RadPropertyDefaultValue("SliderAreaGradientAngle", typeof (RadTrackBarItem))]
    public virtual float SliderAreaGradientAngle
    {
      get
      {
        return (float) this.GetValue(RadTrackBarItem.SliderAreaGradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.SliderAreaGradientAngleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("FitToAvailableSize", typeof (RadTrackBarItem))]
    [Description("Gets or sets whether the TrackBar should fit to available size")]
    public virtual bool FitToAvailableSize
    {
      get
      {
        return (bool) this.GetValue(RadTrackBarItem.FitToAvailableSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.FitToAvailableSizeProperty, (object) value);
      }
    }

    [Description("Gets or sets whether the SlideArea should be visible.")]
    [RadPropertyDefaultValue("ShowSlideArea", typeof (RadTrackBarItem))]
    public virtual bool ShowSlideArea
    {
      get
      {
        return (bool) this.GetValue(RadTrackBarItem.ShowSlideAreaProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.ShowSlideAreaProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ShowTicks", typeof (RadTrackBarItem))]
    [Description("Gets or sets Ticks Visibility")]
    public virtual bool ShowTicks
    {
      get
      {
        return (bool) this.GetValue(RadTrackBarItem.ShowTicksProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.ShowTicksProperty, (object) value);
      }
    }

    [Description("The number of positions the slider moves in response to mouse clicks.")]
    [RadPropertyDefaultValue("LargeChange", typeof (RadTrackBarItem))]
    public virtual int LargeChange
    {
      get
      {
        return (int) this.GetValue(RadTrackBarItem.LargeChangeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.LargeChangeProperty, (object) value);
      }
    }

    [Description("The number of positions the slider moves in response to a mouse click.")]
    [RadPropertyDefaultValue("SmallChange", typeof (RadTrackBarItem))]
    public virtual int SmallChange
    {
      get
      {
        return (int) this.GetValue(RadTrackBarItem.SmallChangeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.SmallChangeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TickFrequency", typeof (RadTrackBarItem))]
    [Description("The number of positions between tick marks.")]
    public virtual int TickFrequency
    {
      get
      {
        return (int) this.GetValue(RadTrackBarItem.TickFrequencyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.TickFrequencyProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TrackBarOrientation", typeof (RadTrackBarItem))]
    [Description("Gets or sets TrackBar's orientation.")]
    public virtual Orientation TrackBarOrientation
    {
      get
      {
        return (Orientation) this.GetValue(RadTrackBarItem.TrackBarOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.TrackBarOrientationProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("SlideAreaWidth", typeof (RadTrackBarItem))]
    [Description("Gets or sets the width of TrackBar's SlideArea.")]
    public virtual int SlideAreaWidth
    {
      get
      {
        return (int) this.GetValue(RadTrackBarItem.SlideAreaWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.SlideAreaWidthProperty, (object) value);
      }
    }

    [Description("Indicates the tick style of the trackBar")]
    [RadPropertyDefaultValue("TickStyle", typeof (RadTrackBarItem))]
    public virtual TickStyles TickStyle
    {
      get
      {
        return (TickStyles) this.GetValue(RadTrackBarItem.TickStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.TickStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Minimum", typeof (RadTrackBarItem))]
    [Description("Gets or sets a minimum int value for the trackbar position.")]
    public virtual int RangeMinimum
    {
      get
      {
        return (int) this.GetValue(RadTrackBarItem.MinimumProperty);
      }
      set
      {
        if (value < 0)
          value = 0;
        if (value > this.RangeMaximum)
          value = this.RangeMaximum;
        int num = (int) this.SetValue(RadTrackBarItem.MinimumProperty, (object) value);
      }
    }

    [Description("Gets or sets a maximum int value for the trackbar position.")]
    [RadPropertyDefaultValue("Maximum", typeof (RadTrackBarItem))]
    public virtual int RangeMaximum
    {
      get
      {
        return (int) this.GetValue(RadTrackBarItem.MaximumProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.MaximumProperty, (object) value);
        if (this.RangeMaximum < 0)
          this.RangeMaximum = 0;
        if (this.RangeMaximum >= this.RangeMinimum)
          return;
        this.RangeMinimum = this.RangeMaximum;
      }
    }

    [Description("Gets or sets the position of the Slider")]
    [RadPropertyDefaultValue("Value", typeof (RadTrackBarItem))]
    public virtual int RangeValue
    {
      get
      {
        return (int) this.GetValue(RadTrackBarItem.ValueProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarItem.ValueProperty, (object) value);
        if (this.RangeValue < this.RangeMinimum)
          this.RangeValue = this.RangeMinimum;
        if (this.RangeValue <= this.RangeMaximum)
          return;
        this.RangeValue = this.RangeMaximum;
      }
    }
  }
}
