// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValuePointFCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class AnimationValuePointFCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (PointF);
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
      PointF pointF1 = (PointF) step;
      PointF pointF2 = (PointF) currValue;
      return (object) new PointF(pointF2.X + pointF1.X, pointF2.Y + pointF1.Y);
    }

    public override object CalculateInversedStep(object step)
    {
      PointF pointF = (PointF) step;
      return (object) new PointF(-pointF.X, -pointF.Y);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      PointF pointF1 = (PointF) animationStartValue;
      PointF pointF2 = (PointF) animationEndValue;
      return (object) new PointF(this.CalculateFloatStep(pointF1.X, pointF2.X, numFrames), this.CalculateFloatStep(pointF1.Y, pointF2.Y, numFrames));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      PointF pointF1 = (PointF) animationStartValue;
      PointF pointF2 = (PointF) animationStep;
      return (object) new PointF(this.CalculateFloatEndValue(pointF1.X, pointF2.X, numFrames), this.CalculateFloatEndValue(pointF1.Y, pointF2.Y, numFrames));
    }
  }
}
