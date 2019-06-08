// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SeparatorsElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class SeparatorsElement : BasePrimitive
  {
    public static RadProperty SeparatorWidthProperty = RadProperty.Register(nameof (SeparatorWidth), typeof (int), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty StepWidthProperty = RadProperty.Register(nameof (StepWidth), typeof (int), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 14, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorColor1Property = RadProperty.Register(nameof (SeparatorColor1), typeof (Color), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorColor2Property = RadProperty.Register(nameof (SeparatorColor2), typeof (Color), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorColor3Property = RadProperty.Register(nameof (SeparatorColor3), typeof (Color), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorColor4Property = RadProperty.Register(nameof (SeparatorColor4), typeof (Color), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.White, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorGradientAngleProperty = RadProperty.Register("SeparatorGeadientAngle", typeof (int), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorGradientPercentage1Property = RadProperty.Register(nameof (SeparatorGradientPercentage1), typeof (float), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.4f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorGradientPercentage2Property = RadProperty.Register(nameof (SeparatorGradientPercentage2), typeof (float), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.6f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ProgressOrientationProperty = RadProperty.Register(nameof (ProgressOrientation), typeof (ProgressOrientation), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ProgressOrientation.Left, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SweepAngleProperty = RadProperty.Register(nameof (SweepAngle), typeof (int), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NumberOfColorsProperty = RadProperty.Register(nameof (NumberOfColors), typeof (int), typeof (SeparatorsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.AffectsDisplay));
    private bool dash;
    private bool hatch;

    public int SeparatorWidth
    {
      get
      {
        return (int) this.GetValue(SeparatorsElement.SeparatorWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorWidthProperty, (object) value);
      }
    }

    public int StepWidth
    {
      get
      {
        return (int) this.GetValue(SeparatorsElement.StepWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.StepWidthProperty, (object) value);
      }
    }

    public Color SeparatorColor1
    {
      get
      {
        return (Color) this.GetValue(SeparatorsElement.SeparatorColor1Property);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorColor1Property, (object) value);
      }
    }

    public Color SeparatorColor2
    {
      get
      {
        return (Color) this.GetValue(SeparatorsElement.SeparatorColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorColor2Property, (object) value);
      }
    }

    public Color SeparatorColor3
    {
      get
      {
        return (Color) this.GetValue(SeparatorsElement.SeparatorColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorColor3Property, (object) value);
      }
    }

    public Color SeparatorColor4
    {
      get
      {
        return (Color) this.GetValue(SeparatorsElement.SeparatorColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorColor4Property, (object) value);
      }
    }

    public int SeparatorGradientAngle
    {
      get
      {
        return (int) this.GetValue(SeparatorsElement.SeparatorGradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorGradientAngleProperty, (object) value);
      }
    }

    public float SeparatorGradientPercentage1
    {
      get
      {
        return (float) this.GetValue(SeparatorsElement.SeparatorGradientPercentage1Property);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorGradientPercentage1Property, (object) value);
      }
    }

    public float SeparatorGradientPercentage2
    {
      get
      {
        return (float) this.GetValue(SeparatorsElement.SeparatorGradientPercentage2Property);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SeparatorGradientPercentage2Property, (object) value);
      }
    }

    public int NumberOfColors
    {
      get
      {
        return (int) this.GetValue(SeparatorsElement.NumberOfColorsProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.NumberOfColorsProperty, (object) value);
      }
    }

    public ProgressOrientation ProgressOrientation
    {
      get
      {
        return (ProgressOrientation) this.GetValue(SeparatorsElement.ProgressOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.ProgressOrientationProperty, (object) value);
      }
    }

    public int SweepAngle
    {
      get
      {
        return (int) this.GetValue(SeparatorsElement.SweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorsElement.SweepAngleProperty, (object) value);
      }
    }

    public bool Dash
    {
      get
      {
        return this.dash;
      }
      set
      {
        this.dash = value;
      }
    }

    public bool Hatch
    {
      get
      {
        return this.hatch;
      }
      set
      {
        this.hatch = value;
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      graphics.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      if (this.Size.Width <= 2 || this.Size.Height <= 2)
        return;
      int step = this.StepWidth + this.SeparatorWidth;
      float beginPoint = (float) this.StepWidth;
      (graphics.UnderlayGraphics as Graphics).SetClip(new RectangleF((float) this.Location.X, (float) this.Location.Y, (float) this.Size.Width, (float) this.Size.Height));
      Region clip = (graphics.UnderlayGraphics as Graphics).Clip;
      LinearGradientBrush brush = this.CreateBrush();
      if (this.ProgressOrientation == ProgressOrientation.Left || this.ProgressOrientation == ProgressOrientation.Right)
      {
        if (this.ProgressOrientation == ProgressOrientation.Right)
          beginPoint = (float) (this.Size.Width % step) + (float) this.StepWidth;
        float dx = (float) Math.Tan(this.DegreeToRadian((double) (90 - this.SweepAngle))) * (float) this.Size.Height;
        if (this.dash && !this.hatch)
          this.DrawHorizontalDashLines(graphics, beginPoint, step, dx, brush);
        if (this.hatch)
        {
          this.DrawHorizontalDashLines(graphics, beginPoint, step, dx, brush);
          this.DrawHorizontalDashLines(graphics, beginPoint, step, -dx, brush);
        }
      }
      else
      {
        if (this.ProgressOrientation == ProgressOrientation.Bottom)
          beginPoint = (float) (this.Size.Height % step) + (float) this.StepWidth;
        float dy = (float) Math.Tan(this.DegreeToRadian((double) (90 - this.SweepAngle))) * (float) this.Size.Width;
        if (this.dash && !this.hatch)
          this.DrawVerticalDashLines(graphics, beginPoint, step, dy, brush);
        if (this.hatch)
        {
          this.DrawVerticalDashLines(graphics, beginPoint, step, dy, brush);
          this.DrawVerticalDashLines(graphics, beginPoint, step, -dy, brush);
        }
      }
      (graphics.UnderlayGraphics as Graphics).Clip = clip;
      graphics.RestoreSmoothingMode();
    }

    private void DrawHorizontalDashLines(
      IGraphics graphics,
      float beginPoint,
      int step,
      float dx,
      LinearGradientBrush brush)
    {
      PointF[] points = new PointF[4];
      points[0] = new PointF(beginPoint, 0.0f);
      points[1] = new PointF(points[0].X + (float) this.SeparatorWidth, 0.0f);
      points[2] = new PointF(points[1].X - dx, (float) this.Size.Height);
      points[3] = new PointF(points[0].X - dx, (float) this.Size.Height);
      int num1 = (int) -(double) Math.Abs(dx);
      int num2 = (int) ((double) this.Size.Width + (double) Math.Abs(dx));
      if (this.ProgressOrientation == ProgressOrientation.Left)
      {
        for (int index = num1; index < num2; index += step)
        {
          points[0].X = (float) index;
          points[1].X = points[0].X + (float) this.SeparatorWidth;
          points[2].X = points[1].X - dx;
          points[3].X = points[0].X - dx;
          graphics.FillPolygon((Brush) brush, points);
        }
      }
      else
      {
        for (int index = num2; index > num1; index -= step)
        {
          points[0].X = (float) index;
          points[1].X = points[0].X + (float) this.SeparatorWidth;
          points[2].X = points[1].X - dx;
          points[3].X = points[0].X - dx;
          graphics.FillPolygon((Brush) brush, points);
        }
      }
    }

    private void DrawVerticalDashLines(
      IGraphics graphics,
      float beginPoint,
      int step,
      float dy,
      LinearGradientBrush brush)
    {
      PointF[] points = new PointF[4];
      points[0] = new PointF(0.0f, beginPoint);
      points[1] = new PointF(0.0f, points[0].Y + (float) this.SeparatorWidth);
      points[2] = new PointF((float) this.Size.Width, points[1].Y - dy);
      points[3] = new PointF((float) this.Size.Width, points[0].Y - dy);
      int num1 = (int) -(double) Math.Abs(dy);
      int num2 = (int) ((double) this.Size.Height + (double) Math.Abs(dy));
      if (this.ProgressOrientation == ProgressOrientation.Top)
      {
        for (int index = num1; index < num2; index += step)
        {
          points[0].Y = (float) index;
          points[1].Y = points[0].Y + (float) this.SeparatorWidth;
          points[2].Y = points[1].Y - dy;
          points[3].Y = points[0].Y - dy;
          graphics.FillPolygon((Brush) brush, points);
        }
      }
      else
      {
        for (int index = num2; index > num1; index -= step)
        {
          points[0].Y = (float) index;
          points[1].Y = points[0].Y + (float) this.SeparatorWidth;
          points[2].Y = points[1].Y + dy;
          points[3].Y = points[0].Y + dy;
          graphics.FillPolygon((Brush) brush, points);
        }
      }
    }

    private LinearGradientBrush CreateBrush()
    {
      RectangleF rect = new RectangleF(0.0f, 0.0f, (float) this.Size.Width, (float) this.Size.Height);
      if (this.NumberOfColors < 2)
        return new LinearGradientBrush(rect, this.SeparatorColor1, this.SeparatorColor1, (float) this.SeparatorGradientAngle);
      if (this.NumberOfColors < 3)
        return new LinearGradientBrush(rect, this.SeparatorColor1, this.SeparatorColor2, (float) this.SeparatorGradientAngle);
      Color[] colorArray = new Color[4];
      float[] numArray = new float[4];
      colorArray[0] = this.SeparatorColor1;
      colorArray[1] = this.SeparatorColor2;
      colorArray[2] = this.SeparatorColor2;
      colorArray[3] = this.SeparatorColor3;
      numArray[0] = 0.0f;
      numArray[1] = this.SeparatorGradientPercentage1;
      numArray[2] = this.SeparatorGradientPercentage2;
      numArray[3] = 1f;
      if (this.NumberOfColors > 3)
      {
        colorArray[2] = this.SeparatorColor3;
        colorArray[3] = this.SeparatorColor4;
      }
      return new LinearGradientBrush(rect, Color.White, Color.White, (float) this.SeparatorGradientAngle) { InterpolationColors = new ColorBlend() { Colors = colorArray, Positions = numArray } };
    }

    private double DegreeToRadian(double angle)
    {
      return Math.PI * angle / 180.0;
    }
  }
}
