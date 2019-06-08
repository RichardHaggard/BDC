// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueSizeFCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class AnimationValueSizeFCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (SizeF);
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
      SizeF sizeF = (SizeF) startValue;
      return (object) new SizeF(calc.CalculateCurrentValue(sizeF.Width, ((SizeF) endValue).Width, currFrameNum, totalFrameNum), calc.CalculateCurrentValue(sizeF.Height, ((SizeF) endValue).Height, currFrameNum, totalFrameNum));
    }

    public override object CalculateInversedStep(object step)
    {
      SizeF sizeF = (SizeF) step;
      return (object) new SizeF(-sizeF.Width, -sizeF.Height);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      SizeF sizeF1 = (SizeF) animationStartValue;
      SizeF sizeF2 = (SizeF) animationEndValue;
      return (object) new SizeF(this.CalculateFloatStep(sizeF1.Width, sizeF2.Width, numFrames), this.CalculateFloatStep(sizeF1.Height, sizeF2.Height, numFrames));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      SizeF sizeF1 = (SizeF) animationStartValue;
      SizeF sizeF2 = (SizeF) animationStep;
      return (object) new SizeF(this.CalculateFloatEndValue(sizeF1.Width, sizeF2.Width, numFrames), this.CalculateFloatEndValue(sizeF1.Height, sizeF2.Height, numFrames));
    }
  }
}
