// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FadingRingWaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class FadingRingWaitingBarIndicatorElement : BaseRingWaitingBarIndicatorElement
  {
    public static RadProperty ExpandCollapseRingProperty = RadProperty.Register(nameof (ExpandCollapseRing), typeof (bool), typeof (FadingRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SweepAngleProperty = RadProperty.Register(nameof (SweepAngle), typeof (int), typeof (FadingRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 240, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MinSweepAngleProperty = RadProperty.Register(nameof (MinSweepAngle), typeof (int), typeof (FadingRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10, ElementPropertyOptions.AffectsDisplay));
    private int currentSweepAngle;
    private IList<Color> colors;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.currentSweepAngle = this.SweepAngle;
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ExpandCollapseRing", typeof (FadingRingWaitingBarIndicatorElement))]
    public virtual bool ExpandCollapseRing
    {
      get
      {
        return (bool) this.GetValue(FadingRingWaitingBarIndicatorElement.ExpandCollapseRingProperty);
      }
      set
      {
        int num = (int) this.SetValue(FadingRingWaitingBarIndicatorElement.ExpandCollapseRingProperty, (object) value);
      }
    }

    [RadRange(0, 360)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("SweepAngle", typeof (FadingRingWaitingBarIndicatorElement))]
    public virtual int SweepAngle
    {
      get
      {
        return (int) this.GetValue(FadingRingWaitingBarIndicatorElement.SweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(FadingRingWaitingBarIndicatorElement.SweepAngleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadRange(0, 360)]
    [RadPropertyDefaultValue("MinSweepAngle", typeof (FadingRingWaitingBarIndicatorElement))]
    public virtual int MinSweepAngle
    {
      get
      {
        return (int) this.GetValue(FadingRingWaitingBarIndicatorElement.MinSweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(FadingRingWaitingBarIndicatorElement.MinSweepAngleProperty, (object) value);
      }
    }

    public override void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      using (GraphicsPath segmentPath = this.CreateSegmentPath())
      {
        int leadingElementAngle = this.CurrentLeadingElementAngle;
        int endAngle = this.EndAngle;
        if (leadingElementAngle == endAngle)
          return;
        switch (this.ElementNumberOfColors)
        {
          case 2:
            this.colors = (IList<Color>) new List<Color>(2);
            this.colors.Add(this.ElementColor);
            this.colors.Add(this.ElementColor2);
            break;
          case 3:
            this.colors = (IList<Color>) new List<Color>(3);
            this.colors.Add(this.ElementColor);
            this.colors.Add(this.ElementColor2);
            this.colors.Add(this.ElementColor3);
            break;
          case 4:
            this.colors = (IList<Color>) new List<Color>(4);
            this.colors.Add(this.ElementColor);
            this.colors.Add(this.ElementColor2);
            this.colors.Add(this.ElementColor3);
            this.colors.Add(this.ElementColor4);
            break;
          default:
            this.colors = (IList<Color>) new List<Color>(1);
            this.colors.Add(this.ElementColor);
            break;
        }
        Brush brush = this.ElementNumberOfColors > 1 ? (Brush) this.CreateGradientBrush(leadingElementAngle, endAngle) : (Brush) new SolidBrush(this.colors[0]);
        using (brush)
          graphics.FillPath(brush, segmentPath);
      }
    }

    protected virtual PathGradientBrush CreateGradientBrush(
      int startAngle,
      int endAngle)
    {
      PointF rectangleCenter = (PointF) this.GetRectangleCenter(this.BoundingRectangle);
      float radius = (float) this.Radius;
      List<PointF> pointFList = new List<PointF>(0);
      PointF pointF1 = new PointF(radius + 2f, 0.0f);
      int endRangeAngle = startAngle;
      int num = (this.RotationDirection == RotationDirection.Clockwise ? 1 : -1) * ((int) Math.Floor(1.0 / (Math.PI * (double) this.Radius / 180.0)) + 1);
      for (; endRangeAngle != endAngle; endRangeAngle = this.ValidateAngle(endRangeAngle + num))
      {
        Matrix matrix = new Matrix();
        matrix.Rotate((float) -endRangeAngle);
        PointF[] pts = new PointF[1]{ pointF1 };
        matrix.TransformPoints(pts);
        PointF pointF2 = new PointF(rectangleCenter.X + pts[0].X, rectangleCenter.Y + pts[0].Y);
        pointFList.Add(pointF2);
        if (!this.IsAngleInRange(endRangeAngle + num, endAngle, endRangeAngle))
          break;
      }
      pointFList.Add(rectangleCenter);
      PathGradientBrush pathGradientBrush = new PathGradientBrush(pointFList.ToArray());
      pathGradientBrush.CenterColor = this.ElementColor;
      pathGradientBrush.CenterPoint = rectangleCenter;
      double[] colorFactors;
      switch (this.ElementNumberOfColors)
      {
        case 3:
          colorFactors = new double[this.ElementNumberOfColors];
          colorFactors[0] = 0.0;
          colorFactors[1] = (double) this.ElementGradientPercentage;
          colorFactors[2] = 1.0;
          break;
        case 4:
          colorFactors = new double[this.ElementNumberOfColors];
          colorFactors[0] = 0.0;
          colorFactors[1] = (double) this.ElementGradientPercentage;
          colorFactors[2] = (double) this.ElementGradientPercentage2;
          colorFactors[3] = 1.0;
          break;
        default:
          colorFactors = new double[2]{ 0.0, 1.0 };
          break;
      }
      Color[] colorArray = new Color[pointFList.Count];
      for (int index = 0; index < pointFList.Count; ++index)
      {
        Color color = this.GetColor((double) index / (double) pointFList.Count, this.colors, colorFactors);
        colorArray[index] = color;
      }
      pathGradientBrush.SurroundColors = colorArray;
      return pathGradientBrush;
    }

    private bool IsAngleInRange(int angle, int startRangeAngle, int endRangeAngle)
    {
      angle %= 360;
      startRangeAngle %= 360;
      endRangeAngle %= 360;
      if (this.RotationDirection == RotationDirection.Clockwise)
      {
        if (startRangeAngle < endRangeAngle)
        {
          if (angle <= startRangeAngle)
            angle += 360;
          startRangeAngle += 360;
        }
        if (endRangeAngle <= angle)
          return angle <= startRangeAngle;
        return false;
      }
      if (startRangeAngle > endRangeAngle)
      {
        endRangeAngle += 360;
        if (angle < startRangeAngle)
          angle += 360;
      }
      if (startRangeAngle <= angle)
        return angle <= endRangeAngle;
      return false;
    }

    private Color GetColor(double position, IList<Color> colors, double[] colorFactors)
    {
      int num1 = Array.BinarySearch<double>(colorFactors, position);
      int index1 = Math.Max(0, Math.Min(num1 < 0 ? ~num1 - 1 : num1 - 1, colorFactors.Length - 1));
      int index2 = Math.Min(index1 + 1, colorFactors.Length - 1);
      if (index1 == index2)
        return colors[index1];
      double num2 = (position - colorFactors[index1]) / (colorFactors[index2] - colorFactors[index1]);
      return Color.FromArgb((int) colors[index1].A + (int) ((double) ((int) colors[index2].A - (int) colors[index1].A) * num2), (int) colors[index1].R + (int) ((double) ((int) colors[index2].R - (int) colors[index1].R) * num2), (int) colors[index1].G + (int) ((double) ((int) colors[index2].G - (int) colors[index1].G) * num2), (int) colors[index1].B + (int) ((double) ((int) colors[index2].B - (int) colors[index1].B) * num2));
    }

    protected virtual GraphicsPath CreateSegmentPath()
    {
      int num1 = this.RotationDirection == RotationDirection.Clockwise ? -this.currentSweepAngle : this.currentSweepAngle;
      int num2 = this.ValidateAngle(this.CurrentLeadingElementAngle);
      int endAngle = this.EndAngle;
      Point rectangleCenter = this.GetRectangleCenter(this.BoundingRectangle);
      GraphicsPath graphicsPath = new GraphicsPath();
      PointF pt1_1 = this.PointFromCenter((PointF) rectangleCenter, (double) this.InnerRadius, (double) num2);
      PointF pt2_1 = this.PointFromCenter((PointF) rectangleCenter, (double) this.Radius, (double) num2);
      graphicsPath.AddLine(pt1_1, pt2_1);
      RectangleF rectangleFromRadius1 = this.GetRectangleFromRadius((PointF) rectangleCenter, (float) this.Radius);
      graphicsPath.AddArc(rectangleFromRadius1, -(float) num2, (float) num1);
      PointF pt1_2 = this.PointFromCenter((PointF) rectangleCenter, (double) this.Radius, (double) endAngle);
      PointF pt2_2 = this.PointFromCenter((PointF) rectangleCenter, (double) this.InnerRadius, (double) endAngle);
      graphicsPath.AddLine(pt1_2, pt2_2);
      graphicsPath.StartFigure();
      if (this.InnerRadius > 0)
      {
        RectangleF rectangleFromRadius2 = this.GetRectangleFromRadius((PointF) rectangleCenter, (float) this.InnerRadius);
        graphicsPath.AddArc(rectangleFromRadius2, -(float) num2, (float) num1);
      }
      return graphicsPath;
    }

    public override void Animate(int step)
    {
      if (this.ExpandCollapseRing)
      {
        int num = this.ValidateAngle(this.CurrentLeadingElementAngle);
        int endAngle = this.EndAngle;
        if (endAngle == this.InitialStartElementAngle && this.currentSweepAngle < this.SweepAngle)
        {
          this.currentSweepAngle += step;
          if (this.currentSweepAngle > this.SweepAngle)
            this.currentSweepAngle = this.SweepAngle;
          base.Animate(step);
        }
        else if (this.RotationDirection == RotationDirection.Clockwise && this.CurrentLeadingElementAngle != this.InitialStartElementAngle && (this.InitialStartElementAngle > num && this.InitialStartElementAngle < num + step || this.InitialStartElementAngle - step < 0 && this.ValidateAngle(this.InitialStartElementAngle - step) < num))
          this.CurrentLeadingElementAngle = this.InitialStartElementAngle;
        else if (this.RotationDirection == RotationDirection.CounterClockwise && this.CurrentLeadingElementAngle != this.InitialStartElementAngle && (this.InitialStartElementAngle < num && this.InitialStartElementAngle > num - step || this.InitialStartElementAngle + step >= 360 && this.ValidateAngle(this.InitialStartElementAngle + step) > num))
          this.CurrentLeadingElementAngle = this.InitialStartElementAngle;
        else if (num == this.InitialStartElementAngle && this.currentSweepAngle > this.MinSweepAngle)
        {
          this.currentSweepAngle -= step;
          if (this.currentSweepAngle >= this.MinSweepAngle)
            return;
          this.currentSweepAngle = this.MinSweepAngle;
        }
        else if (this.RotationDirection == RotationDirection.Clockwise && this.CurrentLeadingElementAngle != this.ValidateAngle(this.InitialStartElementAngle - this.currentSweepAngle) && (this.InitialStartElementAngle > endAngle && this.InitialStartElementAngle < endAngle + step || this.InitialStartElementAngle - step < 0 && this.ValidateAngle(this.InitialStartElementAngle - step) < endAngle))
          this.CurrentLeadingElementAngle = this.ValidateAngle(this.InitialStartElementAngle - this.currentSweepAngle);
        else if (this.RotationDirection == RotationDirection.CounterClockwise && this.CurrentLeadingElementAngle != this.ValidateAngle(this.InitialStartElementAngle + this.currentSweepAngle) && (this.InitialStartElementAngle < endAngle && this.InitialStartElementAngle > endAngle - step || this.InitialStartElementAngle + step >= 360 && this.ValidateAngle(this.InitialStartElementAngle + step) > endAngle))
          this.CurrentLeadingElementAngle = this.ValidateAngle(this.InitialStartElementAngle + this.currentSweepAngle);
        else
          base.Animate(step);
      }
      else
        base.Animate(step);
    }

    public override void ResetAnimation()
    {
      base.ResetAnimation();
      if (this.ExpandCollapseRing)
        this.currentSweepAngle = this.MinSweepAngle;
      else
        this.currentSweepAngle = this.SweepAngle;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == FadingRingWaitingBarIndicatorElement.ExpandCollapseRingProperty)
        this.ResetAnimation();
      else if (e.Property == FadingRingWaitingBarIndicatorElement.SweepAngleProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0 || 360 < newValue)
          throw new ArgumentException("SweepAngle must be between 0 and 360.");
        if (newValue < this.MinSweepAngle)
          throw new ArgumentException("SweepAngle must be greater than MinSweepAngle.");
        this.ResetAnimation();
      }
      else
      {
        if (e.Property != FadingRingWaitingBarIndicatorElement.MinSweepAngleProperty)
          return;
        int newValue = (int) e.NewValue;
        if (newValue < 0 || 360 < newValue)
          throw new ArgumentException("MinSweepAngle must be between 0 and 360.");
        if (newValue > this.SweepAngle)
          throw new ArgumentException("MinSweepAngle must be lower than SweepAngle.");
        this.ResetAnimation();
      }
    }

    private int EndAngle
    {
      get
      {
        return this.ValidateAngle(this.CurrentLeadingElementAngle + (this.RotationDirection == RotationDirection.Clockwise ? this.currentSweepAngle : -this.currentSweepAngle));
      }
    }
  }
}
