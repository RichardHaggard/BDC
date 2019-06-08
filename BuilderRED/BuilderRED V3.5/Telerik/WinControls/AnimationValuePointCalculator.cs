// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValuePointCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class AnimationValuePointCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (Point);
      }
    }

    public override object CalculateAnimatedValue(
      object startValue,
      object endValue,
      object currValue,
      object step,
      int currFrameNum,
      int totalFrameNum,
      EasingCalculator calc)
    {
      Point point1 = (Point) step;
      Point point2 = (Point) currValue;
      return (object) new Point(point2.X + point1.X, point2.Y + point1.Y);
    }

    public override object CalculateInversedStep(object step)
    {
      Point point = (Point) step;
      return (object) new Size(-point.X, -point.Y);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      Point point1 = (Point) animationStartValue;
      Point point2 = (Point) animationEndValue;
      return (object) new Point(this.CalculateIntStep(point1.X, point2.X, numFrames), this.CalculateIntStep(point1.Y, point2.Y, numFrames));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      Point point1 = (Point) animationStartValue;
      Point point2 = (Point) animationStep;
      return (object) new Point(this.CalculateIntEndValue(point1.X, point2.X, numFrames), this.CalculateIntEndValue(point1.Y, point2.Y, numFrames));
    }
  }
}
