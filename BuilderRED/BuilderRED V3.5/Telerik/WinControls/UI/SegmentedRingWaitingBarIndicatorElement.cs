// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SegmentedRingWaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class SegmentedRingWaitingBarIndicatorElement : BaseRingWaitingBarIndicatorElement
  {
    public static RadProperty SegmentDistanceProperty = RadProperty.Register(nameof (SegmentDistance), typeof (float), typeof (SegmentedRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsDisplay));
    private int angleBetweenSegments;
    private int segmentSweepAngle;
    private int innerSegmentSweepAngle;

    public SegmentedRingWaitingBarIndicatorElement()
    {
      this.CalculateSegmentAngles();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("SegmentDistance", typeof (SegmentedRingWaitingBarIndicatorElement))]
    [RadRange(0, 2147483647)]
    public virtual float SegmentDistance
    {
      get
      {
        return (float) this.GetValue(SegmentedRingWaitingBarIndicatorElement.SegmentDistanceProperty) * this.DpiScaleFactor.Width;
      }
      set
      {
        int num = (int) this.SetValue(SegmentedRingWaitingBarIndicatorElement.SegmentDistanceProperty, (object) value);
      }
    }

    protected virtual void CalculateSegmentAngles()
    {
      this.angleBetweenSegments = (int) ((double) (this.SegmentDistance / (float) this.Radius) * 180.0 / Math.PI);
      this.segmentSweepAngle = (int) (360.0 / (double) this.ElementCount - (double) this.angleBetweenSegments);
      this.innerSegmentSweepAngle = (int) (360.0 / (double) this.ElementCount - (double) (this.SegmentDistance / (float) this.InnerRadius) * 180.0 / Math.PI);
    }

    public override void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      using (SolidBrush solidBrush = new SolidBrush(this.ElementColor))
      {
        for (int elementIndex = 0; elementIndex < this.ElementCount; ++elementIndex)
        {
          using (GraphicsPath segmentPath = this.CreateSegmentPath(elementIndex))
            graphics.FillPath((Brush) solidBrush, segmentPath);
          solidBrush.Color = this.CalculateNextColor(solidBrush.Color, elementIndex);
        }
      }
    }

    protected virtual Color CalculateNextColor(Color brushColor, int elementIndex)
    {
      Color color;
      if (this.ElementNumberOfColors == 4)
      {
        int num1 = (int) Math.Round((double) this.ElementGradientPercentage * (double) (this.ElementCount - 1));
        int num2 = num1 < 0 ? 0 : num1;
        int num3 = (int) Math.Round((double) this.ElementGradientPercentage2 * (double) (this.ElementCount - 1));
        int num4 = num3 < 0 ? 0 : num3;
        Color startColor;
        Color endColor;
        int stepsCount;
        int maxStepsCount;
        if (elementIndex <= num2)
        {
          startColor = this.ElementColor;
          endColor = this.ElementColor2;
          stepsCount = elementIndex;
          maxStepsCount = num2;
        }
        else if (elementIndex <= num4)
        {
          startColor = this.ElementColor2;
          endColor = this.ElementColor3;
          stepsCount = elementIndex - num2;
          maxStepsCount = num4 - num2;
        }
        else
        {
          startColor = this.ElementColor3;
          endColor = this.ElementColor4;
          stepsCount = elementIndex - num4;
          maxStepsCount = this.ElementCount - 1 - num4;
        }
        color = this.CalculateColor(startColor, endColor, stepsCount, maxStepsCount);
      }
      else if (this.ElementNumberOfColors == 3)
      {
        int num1 = (int) Math.Round((double) this.ElementGradientPercentage * (double) (this.ElementCount - 1));
        int num2 = num1 < 0 ? 0 : num1;
        Color startColor;
        Color endColor;
        int stepsCount;
        int maxStepsCount;
        if (elementIndex <= num2)
        {
          startColor = this.ElementColor;
          endColor = this.ElementColor2;
          stepsCount = elementIndex;
          maxStepsCount = num2;
        }
        else
        {
          startColor = this.ElementColor2;
          endColor = this.ElementColor3;
          stepsCount = elementIndex - num2;
          maxStepsCount = this.ElementCount - 1 - num2;
        }
        color = this.CalculateColor(startColor, endColor, stepsCount, maxStepsCount);
      }
      else
        color = this.ElementNumberOfColors != 2 ? this.ElementColor : Color.FromArgb((int) brushColor.A - ((int) brushColor.A - (int) this.ElementColor2.A) / this.ElementCount, (int) brushColor.R - ((int) brushColor.R - (int) this.ElementColor2.R) / this.ElementCount, (int) brushColor.G - ((int) brushColor.G - (int) this.ElementColor2.G) / this.ElementCount, (int) brushColor.B - ((int) brushColor.B - (int) this.ElementColor2.B) / this.ElementCount);
      return color;
    }

    private Color CalculateColor(
      Color startColor,
      Color endColor,
      int stepsCount,
      int maxStepsCount)
    {
      if (maxStepsCount <= 0)
        return startColor;
      return Color.FromArgb((int) startColor.A - stepsCount * ((int) startColor.A - (int) endColor.A) / maxStepsCount, (int) startColor.R - stepsCount * ((int) startColor.R - (int) endColor.R) / maxStepsCount, (int) startColor.G - stepsCount * ((int) startColor.G - (int) endColor.G) / maxStepsCount, (int) startColor.B - stepsCount * ((int) startColor.B - (int) endColor.B) / maxStepsCount);
    }

    protected virtual GraphicsPath CreateSegmentPath(int elementIndex)
    {
      double leadingElementAngle = (double) this.CurrentLeadingElementAngle;
      double num1 = (double) (this.segmentSweepAngle - this.innerSegmentSweepAngle) / 2.0;
      double angle1;
      double num2;
      if (this.RotationDirection == RotationDirection.CounterClockwise)
      {
        angle1 = leadingElementAngle - (double) (this.angleBetweenSegments / 2 + (this.segmentSweepAngle + this.angleBetweenSegments) * elementIndex);
        num2 = (double) this.segmentSweepAngle;
        num1 *= -1.0;
      }
      else
      {
        angle1 = leadingElementAngle + (double) (this.angleBetweenSegments / 2 + (this.segmentSweepAngle + this.angleBetweenSegments) * elementIndex);
        num2 = (double) -this.segmentSweepAngle;
      }
      double angle2 = this.ValidateAngle(angle1);
      double angle3 = this.ValidateAngle(angle2 + num1);
      GraphicsPath graphicsPath = new GraphicsPath();
      Point rectangleCenter = this.GetRectangleCenter(this.BoundingRectangle);
      double angle4 = this.ValidateAngle(angle2 - num2);
      double angle5 = this.ValidateAngle(angle4 - num1);
      PointF pt1_1 = this.PointFromCenter((PointF) rectangleCenter, (double) this.InnerRadius, angle3);
      PointF pt2_1 = this.PointFromCenter((PointF) rectangleCenter, (double) this.Radius, angle2);
      graphicsPath.AddLine(pt1_1, pt2_1);
      RectangleF rectangleFromRadius1 = this.GetRectangleFromRadius((PointF) rectangleCenter, (float) this.Radius);
      graphicsPath.AddArc(rectangleFromRadius1, -(float) angle2, (float) num2);
      PointF pt1_2 = this.PointFromCenter((PointF) rectangleCenter, (double) this.Radius, angle4);
      PointF pt2_2 = this.PointFromCenter((PointF) rectangleCenter, (double) this.InnerRadius, angle5);
      graphicsPath.AddLine(pt1_2, pt2_2);
      graphicsPath.StartFigure();
      if (this.InnerRadius > 0)
      {
        RectangleF rectangleFromRadius2 = this.GetRectangleFromRadius((PointF) rectangleCenter, (float) this.InnerRadius);
        graphicsPath.AddArc(rectangleFromRadius2, (float) -angle3, num2 < 0.0 ? (float) -this.innerSegmentSweepAngle : (float) this.innerSegmentSweepAngle);
      }
      return graphicsPath;
    }

    public override void Animate(int step)
    {
      if (this.RotationDirection == RotationDirection.Clockwise)
        step *= -1;
      this.CurrentLeadingElementAngle = this.ValidateAngle(this.CurrentLeadingElementAngle + step * (this.angleBetweenSegments + this.segmentSweepAngle));
    }

    public override void ResetAnimation()
    {
      base.ResetAnimation();
      this.CalculateSegmentAngles();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != SegmentedRingWaitingBarIndicatorElement.SegmentDistanceProperty)
        return;
      if ((double) (float) e.NewValue < 0.0)
        throw new ArgumentException("SegmentDistance cannot be negative.");
      this.ResetAnimation();
    }
  }
}
