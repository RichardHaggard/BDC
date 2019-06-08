// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ProgressBarPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class ProgressBarPrimitive : FillPrimitive
  {
    public static RadProperty DashProperty = RadProperty.Register(nameof (Dash), typeof (bool), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ValueProperty1 = RadProperty.Register(nameof (Value1), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ValueProperty2 = RadProperty.Register(nameof (Value2), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MinimumProperty = RadProperty.Register(nameof (Minimum), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MaximumProperty = RadProperty.Register(nameof (Maximum), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 100, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty StepProperty = RadProperty.Register(nameof (Step), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty StepWidthProperty = RadProperty.Register(nameof (StepWidth), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 8, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorWidthProperty = RadProperty.Register(nameof (SeparatorWidth), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 8, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HatchProperty = RadProperty.Register(nameof (Hatch), typeof (bool), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SweepAngleProperty = RadProperty.Register(nameof (SweepAngle), typeof (int), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorColorProperty1 = RadProperty.Register(nameof (SeparatorColor1), typeof (Color), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorColorProperty2 = RadProperty.Register(nameof (SeparatorColor2), typeof (Color), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ProgressOrientationProperty = RadProperty.Register(nameof (Orientation), typeof (ProgressOrientation), typeof (ProgressBarPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ProgressOrientation.Left, ElementPropertyOptions.AffectsDisplay));
    private const int DefaultSeparatorWidth = 8;
    private int width;
    private int height;
    private int sweepAngle;
    private bool image;
    private Rectangle progressRect;

    public bool HasImage
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
      }
    }

    public virtual ProgressOrientation Orientation
    {
      get
      {
        return (ProgressOrientation) this.GetValue(ProgressBarPrimitive.ProgressOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.ProgressOrientationProperty, (object) value);
      }
    }

    public virtual bool Dash
    {
      get
      {
        return (bool) this.GetValue(ProgressBarPrimitive.DashProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.DashProperty, (object) value);
      }
    }

    public virtual bool Hatch
    {
      get
      {
        return (bool) this.GetValue(ProgressBarPrimitive.HatchProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.HatchProperty, (object) value);
      }
    }

    public virtual int SweepAngle
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.SweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.SweepAngleProperty, (object) value);
      }
    }

    public virtual int StepWidth
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.StepWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.StepWidthProperty, (object) value);
      }
    }

    public virtual int SeparatorWidth
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.SeparatorWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.SeparatorWidthProperty, (object) value);
      }
    }

    public virtual int Value1
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.ValueProperty1);
      }
      set
      {
        if (value < this.Minimum)
          throw new ArgumentException("'" + (object) value + "' is not a valid value for 'Value'.\n'Value' must be between 'Minimum' and 'Maximum'.");
        if (value > this.Maximum)
          throw new ArgumentException("'" + (object) value + "' is not a valid value for 'Value'.\n'Value' must be between 'Minimum' and 'Maximum'.");
        int num = (int) this.SetValue(ProgressBarPrimitive.ValueProperty1, (object) value);
      }
    }

    public virtual int Value2
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.ValueProperty2);
      }
      set
      {
        if (value < this.Minimum)
          throw new ArgumentException("'" + (object) value + "' is not a valid value for 'Value'.\n'Value' must be between 'Minimum' and 'Maximum'.");
        if (value > this.Maximum)
          throw new ArgumentException("'" + (object) value + "' is not a valid value for 'Value'.\n'Value' must be between 'Minimum' and 'Maximum'.");
        int num = (int) this.SetValue(ProgressBarPrimitive.ValueProperty2, (object) value);
      }
    }

    public virtual int Minimum
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.MinimumProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.MinimumProperty, (object) value);
        if (this.Minimum > this.Maximum)
          this.Maximum = this.Minimum;
        if (this.Minimum > this.Value1)
          this.Value1 = this.Minimum;
        if (this.Minimum <= this.Value2)
          return;
        this.Value2 = this.Minimum;
      }
    }

    public virtual int Maximum
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.MaximumProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.MaximumProperty, (object) value);
        if (this.Maximum < this.Value1)
          this.Value1 = this.Maximum;
        if (this.Maximum < this.Value2)
          this.Value2 = this.Maximum;
        if (this.Maximum >= this.Minimum)
          return;
        this.Minimum = this.Maximum;
      }
    }

    public virtual int Step
    {
      get
      {
        return (int) this.GetValue(ProgressBarPrimitive.StepProperty);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.StepProperty, (object) value);
      }
    }

    public virtual Color SeparatorColor1
    {
      get
      {
        return (Color) this.GetValue(ProgressBarPrimitive.SeparatorColorProperty1);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.SeparatorColorProperty1, (object) value);
      }
    }

    public virtual Color SeparatorColor2
    {
      get
      {
        return (Color) this.GetValue(ProgressBarPrimitive.SeparatorColorProperty2);
      }
      set
      {
        int num = (int) this.SetValue(ProgressBarPrimitive.SeparatorColorProperty2, (object) value);
      }
    }

    private void SetSize()
    {
      this.width = this.Parent.Size.Width;
      this.height = this.Parent.Size.Height;
    }

    public override void PaintPrimitive(IGraphics g, float angle, SizeF scale)
    {
      if (this.Parent == null)
        return;
      this.SetSize();
      if (this.width <= 0 || this.height <= 0)
        return;
      int val2 = 4;
      Color[] colorStops = new Color[Math.Min(Math.Max(this.NumberOfColors, 1), val2)];
      float[] colorOffsets = new float[Math.Min(Math.Max(this.NumberOfColors, 1), val2)];
      if (this.NumberOfColors > 0)
      {
        colorStops[0] = this.BackColor;
        colorOffsets[0] = 0.0f;
      }
      if (this.NumberOfColors > 1)
      {
        colorStops[1] = this.BackColor2;
        colorOffsets[colorStops.Length - 1] = 1f;
      }
      if (this.NumberOfColors > 2)
      {
        colorStops[2] = this.BackColor3;
        colorOffsets[1] = this.GradientPercentage;
      }
      if (this.NumberOfColors > 3)
      {
        colorStops[3] = this.BackColor4;
        colorOffsets[2] = this.GradientPercentage2;
      }
      Rectangle rectangle1 = new Rectangle(Point.Empty, this.Size);
      int num1 = Math.Max(1, this.Maximum - this.Minimum);
      int num2 = (int) Math.Round((double) this.width * (double) this.Value1 / (double) num1);
      int num3 = (int) Math.Round((double) this.height * (double) this.Value1 / (double) num1);
      int num4 = (int) Math.Round((double) this.width * (double) this.Value2 / (double) num1);
      int num5 = (int) Math.Round((double) this.height * (double) this.Value2 / (double) num1);
      Math.Ceiling((double) num2 / 8.0);
      Rectangle rectangle2 = new Rectangle(0, 0, num2, this.height);
      Rectangle BorderRectangle = new Rectangle(0, 0, num4, this.height);
      switch (this.Orientation)
      {
        case ProgressOrientation.Top:
          rectangle2 = new Rectangle(0, 0, this.width, num3);
          BorderRectangle = new Rectangle(0, 0, this.width, num5);
          break;
        case ProgressOrientation.Bottom:
          rectangle2 = new Rectangle(0, this.height - num3, this.width, num3);
          BorderRectangle = new Rectangle(0, this.height - num5, this.width, num5);
          break;
        case ProgressOrientation.Right:
          rectangle2 = new Rectangle(this.width - num2, 0, num2, this.height);
          BorderRectangle = new Rectangle(this.width - num4, 0, num4, this.height);
          break;
      }
      this.progressRect = rectangle2;
      if (this.HasImage)
        return;
      switch (this.GradientStyle)
      {
        case GradientStyles.Solid:
          g.FillRectangle(BorderRectangle, Color.FromArgb(25, this.BackColor));
          g.FillRectangle(rectangle2, this.BackColor);
          break;
        case GradientStyles.Linear:
        case GradientStyles.Radial:
          g.FillRectangle(BorderRectangle, Color.FromArgb(25, this.BackColor));
          g.FillGradientRectangle(rectangle2, colorStops, colorOffsets, this.GradientStyle, this.GradientAngle, this.GradientPercentage, this.GradientPercentage2);
          break;
        case GradientStyles.Glass:
          g.FillRectangle(BorderRectangle, Color.FromArgb(25, this.BackColor));
          g.FillGlassRectangle(rectangle2, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2);
          break;
        case GradientStyles.OfficeGlass:
          g.FillRectangle(BorderRectangle, Color.FromArgb(25, this.BackColor));
          g.FillOfficeGlassRectangle(rectangle2, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2, true);
          break;
        case GradientStyles.OfficeGlassRect:
          g.FillRectangle(BorderRectangle, Color.FromArgb(25, this.BackColor));
          g.FillOfficeGlassRectangle(rectangle2, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2, false);
          break;
        case GradientStyles.Gel:
          g.FillRectangle(BorderRectangle, Color.FromArgb(25, this.BackColor));
          g.FillGellRectangle(rectangle2, colorStops, this.GradientPercentage, this.GradientPercentage2);
          break;
        case GradientStyles.Vista:
          g.FillRectangle(BorderRectangle, Color.FromArgb(25, this.BackColor));
          g.FillVistaRectangle(rectangle2, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2);
          break;
      }
      if (!this.Dash)
        return;
      this.DrawLines(g, num2, num3, num4, num5);
    }

    public Rectangle GetProgressRectangle()
    {
      return this.progressRect;
    }

    private void DrawLines(
      IGraphics g,
      int fillWidth,
      int fillHeight,
      int fillWidthValue2,
      int fillHeightValue2)
    {
      int num1 = Math.Max(this.width, this.height);
      Rectangle rectangle = new Rectangle(fillWidth, 0, this.width, this.height);
      if (this.width == 0 || this.height == 0)
        return;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, this.width, this.height), this.SeparatorColor1, this.SeparatorColor2, LinearGradientMode.Vertical);
      g.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      double num2;
      double num3 = num2 = 0.0;
      double num4 = num2;
      double num5 = num2;
      this.sweepAngle = this.SweepAngle;
      int num6 = num1 / this.StepWidth * 2;
      switch (this.Orientation)
      {
        case ProgressOrientation.Top:
          rectangle = new Rectangle(0, fillHeight, this.width, this.height - fillHeight);
          break;
        case ProgressOrientation.Bottom:
          rectangle = new Rectangle(0, 0, this.width, this.height - fillHeight);
          break;
        case ProgressOrientation.Right:
          rectangle = new Rectangle(0, 0, this.width - fillWidth, this.height);
          break;
      }
      Region clip = (g.UnderlayGraphics as Graphics).Clip;
      g.ExcludeClip(rectangle);
      if (this.sweepAngle < 90 + this.sweepAngle / 180 * 180)
      {
        double num7 = (double) this.StepWidth / Math.Cos((double) this.sweepAngle * Math.PI / 180.0);
        num5 = num7 + (double) this.height * Math.Tan((double) this.sweepAngle * Math.PI / 180.0);
        double num8 = 0.0;
        double height = (double) this.height;
        for (int index = -num6; index <= num6; ++index)
        {
          double num9 = num7 * (double) index + (double) this.height * Math.Tan((double) this.sweepAngle * Math.PI / 180.0);
          double num10 = num7 * (double) index + (double) this.SeparatorWidth / Math.Cos((double) this.sweepAngle * Math.PI / 180.0);
          double num11 = num10 + (double) this.height * Math.Tan((double) this.sweepAngle * Math.PI / 180.0);
          g.FillPolygon((Brush) linearGradientBrush, new PointF[4]
          {
            new PointF((float) num7 * (float) index, (float) num8),
            new PointF((float) num9, (float) height),
            new PointF((float) num11, (float) height),
            new PointF((float) num10, (float) num8)
          });
          if (this.Hatch)
            g.FillPolygon((Brush) linearGradientBrush, new PointF[4]
            {
              new PointF((float) num7 * (float) index, (float) height),
              new PointF((float) num9, (float) num8),
              new PointF((float) num11, (float) num8),
              new PointF((float) num10, (float) height)
            });
        }
      }
      else
      {
        double num7 = 0.0;
        double width = (double) this.width;
        if (this.Orientation == ProgressOrientation.Bottom || this.Orientation == ProgressOrientation.Top)
          width = (double) this.width;
        num3 = num4 + (double) (int) ((double) this.width / Math.Tan((double) this.sweepAngle * Math.PI / 180.0));
        for (int index = -num6; index <= num6; ++index)
        {
          double num8 = (double) (int) ((double) (index * this.StepWidth) / Math.Sin((double) this.sweepAngle * Math.PI / 180.0));
          double num9 = num8 + (double) (int) ((double) this.SeparatorWidth / Math.Sin((double) this.sweepAngle * Math.PI / 180.0));
          double num10 = num9 + (double) (int) ((double) this.width / Math.Tan((double) this.sweepAngle * Math.PI / 180.0));
          double num11 = num8 + (double) (int) ((double) this.width / Math.Tan((double) this.sweepAngle * Math.PI / 180.0));
          g.FillPolygon((Brush) linearGradientBrush, new PointF[4]
          {
            new PointF((float) num7, (float) num8),
            new PointF((float) width, (float) num11),
            new PointF((float) width, (float) num10),
            new PointF((float) num7, (float) num9)
          });
          if (this.Hatch)
            g.FillPolygon((Brush) linearGradientBrush, new PointF[4]
            {
              new PointF((float) num7, (float) num11),
              new PointF((float) width, (float) num8),
              new PointF((float) width, (float) num9),
              new PointF((float) num7, (float) num10)
            });
        }
      }
      linearGradientBrush.Dispose();
      g.RestoreSmoothingMode();
      (g.UnderlayGraphics as Graphics).Clip = clip;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      if (this.Children.Count < 2)
        return sizeF;
      BorderPrimitive child = this.Children[1] as BorderPrimitive;
      if (child == null)
        return sizeF;
      int num1 = 1;
      if (this.Minimum != 0)
        num1 = this.Maximum - this.Minimum;
      float num2 = finalSize.Width * (float) this.Value1 / (float) num1;
      double num3 = (double) finalSize.Height * (double) this.Value1 / (double) num1;
      if (this.Orientation == ProgressOrientation.Left)
        child.Arrange(new RectangleF(1f, 0.0f, num2 + 1f, sizeF.Height - 1f));
      return sizeF;
    }
  }
}
