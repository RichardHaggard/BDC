// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueIntCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class AnimationValueIntCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (int);
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
        return (object) ((int) currValue + (int) step);
      int initialValue = (int) startValue;
      int endValue1 = (int) endValue;
      return (object) calc.CalculateCurrentValue(initialValue, endValue1, currFrameNum, totalFrameNum);
    }

    public override object CalculateInversedStep(object step)
    {
      return (object) -(int) step;
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      return (object) this.CalculateIntStep((int) animationStartValue, (int) animationEndValue, numFrames);
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      return (object) this.CalculateIntEndValue((int) animationStartValue, (int) animationStep, numFrames);
    }
  }
}
