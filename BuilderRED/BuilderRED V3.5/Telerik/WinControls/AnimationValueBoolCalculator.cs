// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueBoolCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class AnimationValueBoolCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (bool);
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
        return currValue;
      return endValue;
    }

    public override object CalculateInversedStep(object step)
    {
      return step;
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      return animationEndValue;
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object step,
      int numFrames)
    {
      return step;
    }
  }
}
