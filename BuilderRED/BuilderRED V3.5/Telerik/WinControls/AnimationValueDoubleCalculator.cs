// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueDoubleCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class AnimationValueDoubleCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (double);
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
      if (endValue == null)
        return (object) ((double) currValue + (double) step);
      double initialValue = (double) startValue;
      double endValue1 = (double) endValue;
      return (object) calc.CalculateCurrentValue(initialValue, endValue1, currFrameNum, totalFrameNum);
    }

    public override object CalculateInversedStep(object step)
    {
      return (object) -(double) step;
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      return (object) this.CalculateDoubleStep((double) animationStartValue, (double) animationEndValue, numFrames);
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      return (object) this.CalculateDoubleEndValue((double) animationStartValue, (double) animationStep, numFrames);
    }
  }
}
