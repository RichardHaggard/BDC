// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueFontCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class AnimationValueFontCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (Font);
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
      Font font = (Font) currValue;
      FontAnimationStep fontAnimationStep = (FontAnimationStep) step;
      return (object) new Font(font.FontFamily, font.Size + fontAnimationStep.SizeStep);
    }

    public override object ConvertToAnimationStepFromString(string value)
    {
      return (object) new FontAnimationStep(string.IsNullOrEmpty(value) ? 0.0f : float.Parse(value));
    }

    public override string ConvertAnimationStepToString(object value)
    {
      return (value as FontAnimationStep)?.SizeStep.ToString((IFormatProvider) AnimationValueCalculatorFactory.SerializationCulture);
    }

    public override object CalculateInversedStep(object step)
    {
      return (object) new FontAnimationStep(-((FontAnimationStep) step).SizeStep);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      return (object) new FontAnimationStep(this.CalculateFloatStep(((Font) animationStartValue).Size, ((Font) animationEndValue).Size, numFrames));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      return (object) new FontAnimationStep(this.CalculateFloatEndValue(((Font) animationStartValue).Size, ((FontAnimationStep) animationStep).SizeStep, numFrames));
    }
  }
}
