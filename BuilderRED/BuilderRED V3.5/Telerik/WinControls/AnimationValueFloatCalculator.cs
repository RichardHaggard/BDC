// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueFloatCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class AnimationValueFloatCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (float);
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
        return (object) (float) ((double) (float) currValue + (double) (float) step);
      float initialValue = (float) startValue;
      float endValue1 = (float) endValue;
      return (object) calc.CalculateCurrentValue(initialValue, endValue1, currFrameNum, totalFrameNum);
    }

    public override object CalculateInversedStep(object step)
    {
      return (object) (float) -(double) (float) step;
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      return (object) this.CalculateFloatStep((float) animationStartValue, (float) animationEndValue, numFrames);
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object step,
      int numFrames)
    {
      return (object) this.CalculateFloatEndValue((float) animationStartValue, (float) step, numFrames);
    }
  }
}
