// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DotsRingWaitingBarIndicatorElement
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
  public class DotsRingWaitingBarIndicatorElement : BaseRingWaitingBarIndicatorElement
  {
    public static RadProperty DotRadiusProperty = RadProperty.Register(nameof (DotRadius), typeof (int), typeof (DotsRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 7, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LastDotRadiusProperty = RadProperty.Register(nameof (LastDotRadius), typeof (int), typeof (DotsRingWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 6, ElementPropertyOptions.AffectsDisplay));

    [Category("Appearance")]
    [RadPropertyDefaultValue("DotRadius", typeof (DotsRingWaitingBarIndicatorElement))]
    [RadRange(0, 2147483647)]
    public virtual int DotRadius
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(DotsRingWaitingBarIndicatorElement.DotRadiusProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(DotsRingWaitingBarIndicatorElement.DotRadiusProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("LastDotRadius", typeof (DotsRingWaitingBarIndicatorElement))]
    [RadRange(0, 2147483647)]
    [Category("Appearance")]
    public virtual int LastDotRadius
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(DotsRingWaitingBarIndicatorElement.LastDotRadiusProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(DotsRingWaitingBarIndicatorElement.LastDotRadiusProperty, (object) value);
      }
    }

    public override void Paint(Graphics graphics, Rectangle rectangle)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      using (SolidBrush solidBrush = new SolidBrush(this.ElementColor))
      {
        if (this.Parent == null)
          return;
        float dotRadius = (float) this.DotRadius;
        float num = (float) (this.DotRadius - this.LastDotRadius) / (float) this.ElementCount;
        for (int elementIndex = 0; elementIndex < this.ElementCount; ++elementIndex)
        {
          PointF dotLocation = this.CalculateDotLocation(elementIndex, rectangle);
          graphics.FillEllipse((Brush) solidBrush, new RectangleF(dotLocation, new SizeF(dotRadius, dotRadius)));
          solidBrush.Color = this.CalculateNextColor(solidBrush.Color, elementIndex);
          dotRadius -= num;
        }
      }
    }

    private Color CalculateNextColor(Color brushColor, int elementIndex)
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

    protected virtual PointF CalculateDotLocation(int elementIndex, Rectangle rectangle)
    {
      double leadingElementAngle = (double) this.CurrentLeadingElementAngle;
      double num1 = 360.0 / (double) this.ElementCount;
      double num2 = (double) this.ValidateAngle(this.RotationDirection != RotationDirection.CounterClockwise ? (int) (leadingElementAngle + num1 * (double) elementIndex) : (int) (leadingElementAngle - num1 * (double) elementIndex)) * Math.PI / 180.0;
      PointF pt = new PointF((float) this.Radius * (float) Math.Sin(num2), (float) this.Radius * (float) Math.Cos(num2));
      pt.X -= (float) this.DotRadius / 2f;
      pt.Y -= (float) this.DotRadius / 2f;
      pt = PointF.Add(pt, new Size(this.GetRectangleCenter(rectangle)));
      return pt;
    }

    public override void Animate(int step)
    {
      if (this.RotationDirection == RotationDirection.Clockwise)
        step *= -1;
      this.CurrentLeadingElementAngle = this.ValidateAngle(this.CurrentLeadingElementAngle + step * (360 / this.ElementCount));
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == DotsRingWaitingBarIndicatorElement.DotRadiusProperty)
      {
        if ((int) e.NewValue < 0)
          throw new ArgumentException("DotRadius cannot be negative.");
      }
      else if (e.Property == DotsRingWaitingBarIndicatorElement.LastDotRadiusProperty && (int) e.NewValue < 0)
        throw new ArgumentException("LastDotRadius cannot be negative.");
    }
  }
}
