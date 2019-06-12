// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WaitingBarSeparatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class WaitingBarSeparatorElement : LightVisualElement
  {
    public static RadProperty DashProperty = RadProperty.Register(nameof (Dash), typeof (bool), typeof (WaitingBarSeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (WaitingBarSeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HatchProperty = RadProperty.Register(nameof (Hatch), typeof (bool), typeof (WaitingBarSeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SeparatorWidthProperty = RadProperty.Register(nameof (SeparatorWidth), typeof (int), typeof (WaitingBarSeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty StepWidthProperty = RadProperty.Register(nameof (StepWidth), typeof (int), typeof (WaitingBarSeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 14, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ProgressOrientationProperty = RadProperty.Register(nameof (ProgressOrientation), typeof (ProgressOrientation), typeof (WaitingBarSeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ProgressOrientation.Left, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SweepAngleProperty = RadProperty.Register(nameof (SweepAngle), typeof (int), typeof (WaitingBarSeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 45, ElementPropertyOptions.AffectsDisplay));

    static WaitingBarSeparatorElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new WaitingBarSeparatorStateManager(), typeof (WaitingBarSeparatorElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.SmoothingMode = SmoothingMode.AntiAlias;
    }

    public int SeparatorWidth
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(WaitingBarSeparatorElement.SeparatorWidthProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(WaitingBarSeparatorElement.SeparatorWidthProperty, (object) value);
      }
    }

    public int StepWidth
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(WaitingBarSeparatorElement.StepWidthProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(WaitingBarSeparatorElement.StepWidthProperty, (object) value);
      }
    }

    public ProgressOrientation ProgressOrientation
    {
      get
      {
        return (ProgressOrientation) this.GetValue(WaitingBarSeparatorElement.ProgressOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(WaitingBarSeparatorElement.ProgressOrientationProperty, (object) value);
      }
    }

    public int SweepAngle
    {
      get
      {
        return (int) this.GetValue(WaitingBarSeparatorElement.SweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(WaitingBarSeparatorElement.SweepAngleProperty, (object) value);
      }
    }

    [DefaultValue(true)]
    public bool Dash
    {
      get
      {
        return (bool) this.GetValue(WaitingBarSeparatorElement.DashProperty);
      }
      set
      {
        int num = (int) this.SetValue(WaitingBarSeparatorElement.DashProperty, (object) value);
        this.InvalidateMeasure(true);
      }
    }

    public bool Hatch
    {
      get
      {
        return (bool) this.GetValue(WaitingBarSeparatorElement.HatchProperty);
      }
      set
      {
        int num = (int) this.SetValue(WaitingBarSeparatorElement.HatchProperty, (object) value);
        this.InvalidateMeasure(true);
      }
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      graphics.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      if (this.Size.Width <= 2 || this.Size.Height <= 2)
        return;
      int step = this.StepWidth + this.SeparatorWidth;
      float beginPoint = (float) this.StepWidth;
      LinearGradientBrush brush = this.CreateBrush();
      if (this.ProgressOrientation == ProgressOrientation.Left || this.ProgressOrientation == ProgressOrientation.Right)
      {
        if (this.ProgressOrientation == ProgressOrientation.Left)
          beginPoint = (float) (this.Size.Width % step) + (float) this.StepWidth;
        float dx = (float) Math.Tan(this.DegreeToRadian((double) (90 - this.SweepAngle))) * (float) this.Size.Height;
        if (this.Dash && !this.Hatch)
          this.DrawHorizontalDashLines(graphics, beginPoint, step, dx, brush);
        if (this.Hatch)
        {
          this.DrawHorizontalDashLines(graphics, beginPoint, step, dx, brush);
          this.DrawHorizontalDashLines(graphics, beginPoint, step, -dx, brush);
        }
      }
      else
      {
        if (this.ProgressOrientation == ProgressOrientation.Top)
          beginPoint = (float) (this.Size.Height % step) + (float) this.StepWidth;
        float dy = (float) Math.Tan(this.DegreeToRadian((double) (90 - this.SweepAngle))) * (float) this.Size.Width;
        if (this.Dash && !this.Hatch)
          this.DrawVerticalDashLines(graphics, beginPoint, step, dy, brush);
        if (this.Hatch)
        {
          this.DrawVerticalDashLines(graphics, beginPoint, step, dy, brush);
          this.DrawVerticalDashLines(graphics, beginPoint, step, -dy, brush);
        }
      }
      graphics.RestoreSmoothingMode();
      base.PaintElement(graphics, angle, scale);
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
      if (this.ProgressOrientation == ProgressOrientation.Right)
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
      if (this.ProgressOrientation == ProgressOrientation.Bottom)
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
        return new LinearGradientBrush(rect, this.BackColor, this.BackColor, this.GradientAngle);
      if (this.NumberOfColors < 3)
        return new LinearGradientBrush(rect, this.BackColor, this.BackColor2, this.GradientAngle);
      Color[] colorArray = new Color[4];
      float[] numArray = new float[4];
      colorArray[0] = this.BackColor;
      colorArray[1] = this.BackColor2;
      colorArray[2] = this.BackColor2;
      colorArray[3] = this.BackColor3;
      numArray[0] = 0.0f;
      numArray[1] = this.GradientPercentage;
      numArray[2] = this.GradientPercentage2;
      numArray[3] = 1f;
      if (this.NumberOfColors > 3)
      {
        colorArray[2] = this.BackColor3;
        colorArray[3] = this.BackColor4;
      }
      return new LinearGradientBrush(rect, Color.White, Color.White, this.GradientAngle) { InterpolationColors = new ColorBlend() { Colors = colorArray, Positions = numArray } };
    }

    private double DegreeToRadian(double angle)
    {
      return Math.PI * angle / 180.0;
    }
  }
}
