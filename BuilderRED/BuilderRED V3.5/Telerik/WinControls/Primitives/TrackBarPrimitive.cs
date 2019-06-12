// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.TrackBarPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class TrackBarPrimitive : FillPrimitive
  {
    public static RadProperty TickStyleProperty = RadProperty.Register(nameof (TickStyle), typeof (TickStyles), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TickStyles.Both, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ThumbWidthProperty = RadProperty.Register(nameof (ThumbWidth), typeof (int), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 12, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MinimumProperty = RadProperty.Register(nameof (Minimum), typeof (int), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MaximumProperty = RadProperty.Register(nameof (Maximum), typeof (int), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SlideAreaWidthProperty = RadProperty.Register(nameof (SlideAreaWidth), typeof (int), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TrackBarOrientationProperty = RadProperty.Register(nameof (TrackBarOrientation), typeof (Orientation), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TickFrequencyProperty = RadProperty.Register(nameof (TickFrequency), typeof (int), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    [Description("The fifth color component when when gradient style is other than solid and number of colors property is greater than 2")]
    public static RadProperty BackColor5Property = RadProperty.Register(nameof (BackColor5), typeof (Color), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowTicksProperty = RadProperty.Register(nameof (ShowTicks), typeof (bool), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowSlideAreaProperty = RadProperty.Register(nameof (ShowSlideArea), typeof (bool), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FitToAvailableSizeProperty = RadProperty.Register(nameof (FitToAvailableSize), typeof (bool), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    [Description("The sixth color component when when gradient style is other than solid and number of colors property is greater than 2")]
    public static RadProperty BackColor6Property = RadProperty.Register(nameof (BackColor6), typeof (Color), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Blue, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SliderAreaGradientAngleProperty = RadProperty.Register(nameof (SliderAreaGradientAngle), typeof (float), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.0f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TickColorProperty = RadProperty.Register(nameof (TickColor), typeof (Color), typeof (TrackBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));

    public virtual Color TickColor
    {
      get
      {
        return (Color) this.GetValue(TrackBarPrimitive.TickColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.TickColorProperty, (object) value);
      }
    }

    public virtual float SliderAreaGradientAngle
    {
      get
      {
        return (float) this.GetValue(TrackBarPrimitive.SliderAreaGradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.SliderAreaGradientAngleProperty, (object) value);
      }
    }

    public virtual bool FitToAvailableSize
    {
      get
      {
        return (bool) this.GetValue(TrackBarPrimitive.FitToAvailableSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.FitToAvailableSizeProperty, (object) value);
      }
    }

    public virtual bool ShowSlideArea
    {
      get
      {
        return (bool) this.GetValue(TrackBarPrimitive.ShowSlideAreaProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.ShowSlideAreaProperty, (object) value);
      }
    }

    public virtual bool ShowTicks
    {
      get
      {
        return (bool) this.GetValue(TrackBarPrimitive.ShowTicksProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.ShowTicksProperty, (object) value);
      }
    }

    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("BackColor5", typeof (TrackBarPrimitive))]
    [Category("Appearance")]
    public virtual Color BackColor5
    {
      get
      {
        return (Color) this.GetValue(TrackBarPrimitive.BackColor5Property);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.BackColor5Property, (object) value);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("BackColor6", typeof (TrackBarPrimitive))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color BackColor6
    {
      get
      {
        return (Color) this.GetValue(TrackBarPrimitive.BackColor6Property);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.BackColor6Property, (object) value);
      }
    }

    public virtual int ThumbWidth
    {
      get
      {
        return (int) this.GetValue(TrackBarPrimitive.ThumbWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.ThumbWidthProperty, (object) value);
      }
    }

    public virtual Orientation TrackBarOrientation
    {
      get
      {
        return (Orientation) this.GetValue(TrackBarPrimitive.TrackBarOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.TrackBarOrientationProperty, (object) value);
      }
    }

    public virtual TickStyles TickStyle
    {
      get
      {
        return (TickStyles) this.GetValue(TrackBarPrimitive.TickStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.TickStyleProperty, (object) value);
      }
    }

    public virtual int TickFrequency
    {
      get
      {
        return (int) this.GetValue(TrackBarPrimitive.TickFrequencyProperty);
      }
      set
      {
        if (value <= 0)
          value = 1;
        int num = (int) this.SetValue(TrackBarPrimitive.TickFrequencyProperty, (object) value);
      }
    }

    public virtual int SlideAreaWidth
    {
      get
      {
        return (int) this.GetValue(TrackBarPrimitive.SlideAreaWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.SlideAreaWidthProperty, (object) value);
      }
    }

    public virtual int Minimum
    {
      get
      {
        return (int) this.GetValue(TrackBarPrimitive.MinimumProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.MinimumProperty, (object) value);
        if (this.Minimum <= this.Maximum)
          return;
        this.Maximum = this.Minimum;
      }
    }

    public virtual int Maximum
    {
      get
      {
        return (int) this.GetValue(TrackBarPrimitive.MaximumProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarPrimitive.MaximumProperty, (object) value);
        if (this.Maximum >= this.Minimum)
          return;
        this.Minimum = this.Maximum;
      }
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      if (this.TrackBarOrientation == Orientation.Horizontal)
        this.DrawHorizontalTrackFill(graphics, angle, scale);
      else
        this.DrawVerticalTrackFill(graphics, angle, scale);
    }

    private void DrawHorizontalTrackFill(IGraphics graphics, float angle, SizeF scale)
    {
      int num1 = this.Maximum - this.Minimum;
      if (num1 == 0)
        num1 = 1;
      int num2 = this.Bounds.Height / 2;
      double num3 = (double) (this.ThumbWidth / 2);
      switch (this.TickStyle)
      {
        case TickStyles.None:
        case TickStyles.Both:
          if (this.ShowSlideArea)
            graphics.FillGradientRectangle(new Rectangle(0, this.Bounds.Height / 2 - this.SlideAreaWidth / 2, this.Size.Width, this.SlideAreaWidth), new Color[2]
            {
              this.BackColor5,
              this.BackColor6
            }, new float[2]{ 0.0f, 1f }, GradientStyles.Linear, this.SliderAreaGradientAngle, 0.0f, 0.0f);
          if (!this.ShowTicks || this.FitToAvailableSize)
            break;
          graphics.DrawLine(this.TickColor, (float) num3, 0.0f, (float) num3, (float) (num2 / 2 - 1));
          graphics.DrawLine(this.TickColor, (float) num3, (float) (this.Bounds.Bottom - num2 / 2), (float) num3, (float) this.Bounds.Bottom);
          graphics.DrawLine(this.TickColor, (float) this.Bounds.Width - (float) num3, 0.0f, (float) this.Bounds.Width - (float) num3, (float) (num2 / 2 - 1));
          graphics.DrawLine(this.TickColor, (float) this.Bounds.Width - (float) num3, (float) (this.Bounds.Bottom - num2 / 2), (float) this.Bounds.Width - (float) num3, (float) this.Bounds.Bottom);
          for (int index = 0; index <= num1; ++index)
          {
            if (index % this.TickFrequency == 0)
            {
              int num4 = this.Bounds.Width - this.ThumbWidth;
              double num5 = (double) (this.ThumbWidth / 2 + index * num4 / num1);
              graphics.DrawLine(this.TickColor, (float) num5, 0.0f, (float) num5, (float) (num2 / 2 - 1));
              graphics.DrawLine(this.TickColor, (float) num5, (float) (this.Bounds.Bottom - num2 / 2), (float) num5, (float) this.Bounds.Bottom);
            }
          }
          break;
        case TickStyles.TopLeft:
          if (this.ShowSlideArea)
            graphics.FillGradientRectangle(new Rectangle(0, 3 * this.Bounds.Height / 4 - this.SlideAreaWidth / 2 - 1, this.Size.Width, this.SlideAreaWidth), new Color[2]
            {
              this.BackColor5,
              this.BackColor6
            }, new float[2]{ 0.0f, 1f }, GradientStyles.Linear, this.SliderAreaGradientAngle, 0.0f, 0.0f);
          if (!this.ShowTicks || this.FitToAvailableSize)
            break;
          graphics.DrawLine(this.TickColor, (float) num3, 0.0f, (float) num3, (float) (num2 - 1));
          graphics.DrawLine(this.TickColor, (float) this.Bounds.Width - (float) num3, 0.0f, (float) this.Bounds.Width - (float) num3, (float) (num2 - 1));
          for (int index = 0; index <= num1; ++index)
          {
            if (index % this.TickFrequency == 0)
            {
              int num4 = this.Bounds.Width - this.ThumbWidth;
              double num5 = (double) (this.ThumbWidth / 2 + index * num4 / num1);
              graphics.DrawLine(this.TickColor, (float) num5, 0.0f, (float) num5, (float) (num2 - 1));
            }
          }
          break;
        case TickStyles.BottomRight:
          if (this.ShowSlideArea)
            graphics.FillGradientRectangle(new Rectangle(0, this.Bounds.Height / 4 - this.SlideAreaWidth / 2, this.Size.Width, this.SlideAreaWidth), new Color[2]
            {
              this.BackColor5,
              this.BackColor6
            }, new float[2]{ 0.0f, 1f }, GradientStyles.Linear, this.SliderAreaGradientAngle, 0.0f, 0.0f);
          if (!this.ShowTicks || this.FitToAvailableSize)
            break;
          graphics.DrawLine(this.TickColor, (float) num3, (float) (this.Bounds.Bottom - num2 + 1), (float) num3, (float) this.Bounds.Bottom);
          graphics.DrawLine(this.TickColor, (float) this.Bounds.Width - (float) num3, (float) (this.Bounds.Bottom - num2 + 1), (float) this.Bounds.Width - (float) num3, (float) this.Bounds.Bottom);
          for (int index = 0; index <= num1; ++index)
          {
            if (index % this.TickFrequency == 0)
            {
              int num4 = this.Bounds.Width - this.ThumbWidth;
              double num5 = (double) (this.ThumbWidth / 2 + index * num4 / num1);
              graphics.DrawLine(this.TickColor, (float) num5, (float) (this.Bounds.Bottom - num2 + 1), (float) num5, (float) this.Bounds.Bottom);
            }
          }
          break;
      }
    }

    private void DrawVerticalTrackFill(IGraphics graphics, float angle, SizeF scale)
    {
      int num1 = this.Maximum - this.Minimum;
      if (num1 == 0)
        num1 = 1;
      int num2 = this.Bounds.Width / 2;
      double num3 = (double) (this.ThumbWidth / 2);
      switch (this.TickStyle)
      {
        case TickStyles.None:
        case TickStyles.Both:
          if (this.ShowSlideArea)
            graphics.FillGradientRectangle(new Rectangle(this.Bounds.Width / 2 - this.SlideAreaWidth / 2, 0, this.SlideAreaWidth, this.Size.Height), new Color[2]
            {
              this.BackColor5,
              this.BackColor6
            }, new float[2]{ 0.0f, 1f }, GradientStyles.Linear, this.SliderAreaGradientAngle, 0.0f, 0.0f);
          if (!this.ShowTicks || this.FitToAvailableSize)
            break;
          graphics.DrawLine(this.TickColor, 0.0f, (float) num3, (float) (num2 / 2 - 1), (float) num3);
          graphics.DrawLine(this.TickColor, (float) (this.Bounds.Right - num2 / 2), (float) num3, (float) this.Bounds.Right, (float) num3);
          graphics.DrawLine(this.TickColor, 0.0f, (float) this.Bounds.Height - (float) num3, (float) (num2 / 2 - 1), (float) this.Bounds.Height - (float) num3);
          graphics.DrawLine(this.TickColor, (float) (this.Bounds.Right - num2 / 2), (float) this.Bounds.Height - (float) num3, (float) (this.Bounds.Right - num2 / 2), (float) this.Bounds.Height - (float) num3);
          for (int index = 0; index <= num1; ++index)
          {
            if (index % this.TickFrequency == 0)
            {
              int num4 = this.Bounds.Height - this.ThumbWidth;
              double num5 = (double) (this.ThumbWidth / 2 + index * num4 / num1);
              graphics.DrawLine(this.TickColor, 0.0f, (float) num5, (float) (num2 / 2 - 1), (float) num5);
              graphics.DrawLine(this.TickColor, (float) (this.Bounds.Right - num2 / 2), (float) num5, (float) this.Bounds.Right, (float) num5);
            }
          }
          break;
        case TickStyles.TopLeft:
          if (this.ShowSlideArea)
            graphics.FillGradientRectangle(new Rectangle(3 * this.Bounds.Width / 4 - this.SlideAreaWidth / 2, 0, this.SlideAreaWidth, this.Size.Height), new Color[2]
            {
              this.BackColor5,
              this.BackColor6
            }, new float[2]{ 0.0f, 1f }, GradientStyles.Linear, this.SliderAreaGradientAngle, 0.0f, 0.0f);
          if (!this.ShowTicks || this.FitToAvailableSize)
            break;
          graphics.DrawLine(this.TickColor, 0.0f, (float) num3, (float) (num2 - 1), (float) num3);
          graphics.DrawLine(this.TickColor, 0.0f, (float) this.Bounds.Height - (float) num3, (float) (num2 - 1), (float) this.Bounds.Height - (float) num3);
          for (int index = 0; index <= num1; ++index)
          {
            if (index % this.TickFrequency == 0)
            {
              int num4 = this.Bounds.Height - this.ThumbWidth;
              double num5 = (double) (this.ThumbWidth / 2 + index * num4 / num1);
              graphics.DrawLine(this.TickColor, 0.0f, (float) num5, (float) (num2 - 1), (float) num5);
            }
          }
          break;
        case TickStyles.BottomRight:
          if (this.ShowSlideArea)
            graphics.FillGradientRectangle(new Rectangle(this.Bounds.Width / 4 - this.SlideAreaWidth / 2, 0, this.SlideAreaWidth, this.Size.Height), new Color[2]
            {
              this.BackColor5,
              this.BackColor6
            }, new float[2]{ 0.0f, 1f }, GradientStyles.Linear, this.SliderAreaGradientAngle, 0.0f, 0.0f);
          if (!this.ShowTicks || this.FitToAvailableSize)
            break;
          graphics.DrawLine(this.TickColor, (float) (this.Bounds.Right - num2 - 1), (float) num3, (float) this.Bounds.Right, (float) num3);
          graphics.DrawLine(this.TickColor, (float) (this.Bounds.Right - num2 - 1), (float) this.Bounds.Height - (float) num3, (float) this.Bounds.Right, (float) this.Bounds.Height - (float) num3);
          for (int index = 0; index <= num1; ++index)
          {
            if (index % this.TickFrequency == 0)
            {
              int num4 = this.Bounds.Height - this.ThumbWidth;
              double num5 = (double) (this.ThumbWidth / 2 + index * num4 / num1);
              graphics.DrawLine(this.TickColor, (float) (this.Bounds.Right - num2 - 1), (float) num5, (float) this.Bounds.Right, (float) num5);
            }
          }
          break;
      }
    }
  }
}
