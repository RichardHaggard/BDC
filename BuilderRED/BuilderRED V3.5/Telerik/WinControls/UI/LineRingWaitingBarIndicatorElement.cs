// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LineRingWaitingBarIndicatorElement
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
  public class LineRingWaitingBarIndicatorElement : BaseRingWaitingBarIndicatorElement
  {
    public static RadProperty LineThicknessProperty = RadProperty.Register(nameof (LineThickness), typeof (double), typeof (LineRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2.0, ElementPropertyOptions.AffectsDisplay));
    private int angleBetweenLines;

    public LineRingWaitingBarIndicatorElement()
    {
      this.CalculateLineAngles();
    }

    [RadRange(0, 2147483647)]
    [RadPropertyDefaultValue("LineThickness", typeof (LineRingWaitingBarIndicatorElement))]
    [Category("Appearance")]
    public virtual double LineThickness
    {
      get
      {
        return (double) this.GetValue(LineRingWaitingBarIndicatorElement.LineThicknessProperty) * (double) this.DpiScaleFactor.Width;
      }
      set
      {
        int num = (int) this.SetValue(LineRingWaitingBarIndicatorElement.LineThicknessProperty, (object) value);
      }
    }

    public override void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      using (Pen pen = new Pen(this.ElementColor, (float) this.LineThickness))
      {
        for (int elementIndex = 0; elementIndex < this.ElementCount; ++elementIndex)
        {
          using (GraphicsPath linePath = this.CreateLinePath(elementIndex))
            graphics.DrawPath(pen, linePath);
          pen.Color = this.CalculateNextColor(pen.Color, elementIndex);
        }
      }
    }

    protected virtual Color CalculateNextColor(Color penColor, int elementIndex)
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
        color = this.ElementNumberOfColors != 2 ? this.ElementColor : Color.FromArgb((int) penColor.A - ((int) penColor.A - (int) this.ElementColor2.A) / this.ElementCount, (int) penColor.R - ((int) penColor.R - (int) this.ElementColor2.R) / this.ElementCount, (int) penColor.G - ((int) penColor.G - (int) this.ElementColor2.G) / this.ElementCount, (int) penColor.B - ((int) penColor.B - (int) this.ElementColor2.B) / this.ElementCount);
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

    protected virtual GraphicsPath CreateLinePath(int elementIndex)
    {
      int leadingElementAngle = this.CurrentLeadingElementAngle;
      int num = this.ValidateAngle(this.RotationDirection != RotationDirection.CounterClockwise ? leadingElementAngle + this.angleBetweenLines * elementIndex : leadingElementAngle - this.angleBetweenLines * elementIndex);
      Point rectangleCenter = this.GetRectangleCenter(this.BoundingRectangle);
      PointF pt1 = this.PointFromCenter((PointF) rectangleCenter, (double) this.InnerRadius, (double) num);
      PointF pt2 = this.PointFromCenter((PointF) rectangleCenter, (double) this.Radius, (double) num);
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine(pt1, pt2);
      return graphicsPath;
    }

    public override void Animate(int step)
    {
      if (this.RotationDirection == RotationDirection.Clockwise)
        step *= -1;
      this.CurrentLeadingElementAngle = this.ValidateAngle(this.CurrentLeadingElementAngle + step * this.angleBetweenLines);
    }

    public override void ResetAnimation()
    {
      base.ResetAnimation();
      this.CalculateLineAngles();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != LineRingWaitingBarIndicatorElement.LineThicknessProperty)
        return;
      if ((double) e.NewValue < 0.0)
        throw new ArgumentException("LineThickness cannot be negative.");
      this.ResetAnimation();
    }

    protected virtual void CalculateLineAngles()
    {
      this.angleBetweenLines = 360 / this.ElementCount;
    }
  }
}
