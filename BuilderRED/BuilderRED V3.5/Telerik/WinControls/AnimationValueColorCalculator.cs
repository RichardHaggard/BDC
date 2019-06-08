// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueColorCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class AnimationValueColorCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (Color);
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
      Color color1 = (Color) currValue;
      ColorAnimationStep colorAnimationStep = (ColorAnimationStep) step;
      if (endValue == null)
        return (object) Color.FromArgb(Math.Max(0, Math.Min((int) byte.MaxValue, (int) color1.A + colorAnimationStep.A)), Math.Max(0, Math.Min((int) byte.MaxValue, (int) color1.R + colorAnimationStep.R)), Math.Max(0, Math.Min((int) byte.MaxValue, (int) color1.G + colorAnimationStep.G)), Math.Max(0, Math.Min((int) byte.MaxValue, (int) color1.B + colorAnimationStep.B)));
      Color color2 = (Color) startValue;
      Color color3 = (Color) endValue;
      return (object) Color.FromArgb(Math.Max(0, Math.Min((int) byte.MaxValue, calc.CalculateCurrentValue((int) color2.A, (int) color3.A, currFrameNum, totalFrameNum))), Math.Max(0, Math.Min((int) byte.MaxValue, calc.CalculateCurrentValue((int) color2.R, (int) color3.R, currFrameNum, totalFrameNum))), Math.Max(0, Math.Min((int) byte.MaxValue, calc.CalculateCurrentValue((int) color2.G, (int) color3.G, currFrameNum, totalFrameNum))), Math.Max(0, Math.Min((int) byte.MaxValue, calc.CalculateCurrentValue((int) color2.B, (int) color3.B, currFrameNum, totalFrameNum))));
    }

    public override object CalculateInversedStep(object step)
    {
      ColorAnimationStep colorAnimationStep = (ColorAnimationStep) step;
      return (object) new ColorAnimationStep(-colorAnimationStep.A, -colorAnimationStep.R, -colorAnimationStep.G, -colorAnimationStep.B);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      Color color1 = (Color) animationStartValue;
      Color color2 = (Color) animationEndValue;
      return (object) new ColorAnimationStep(this.CalculateIntStep((int) color1.A, (int) color2.A, numFrames, -255, (int) byte.MaxValue), this.CalculateIntStep((int) color1.R, (int) color2.R, numFrames, -255, (int) byte.MaxValue), this.CalculateIntStep((int) color1.G, (int) color2.G, numFrames, -255, (int) byte.MaxValue), this.CalculateIntStep((int) color1.B, (int) color2.B, numFrames, -255, (int) byte.MaxValue));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      Color color = (Color) animationStartValue;
      ColorAnimationStep colorAnimationStep = (ColorAnimationStep) animationStep;
      return (object) Color.FromArgb(this.CalculateIntEndValue((int) color.A, colorAnimationStep.A, numFrames, 0, (int) byte.MaxValue), this.CalculateIntEndValue((int) color.R, colorAnimationStep.R, numFrames, 0, (int) byte.MaxValue), this.CalculateIntEndValue((int) color.G, colorAnimationStep.G, numFrames, 0, (int) byte.MaxValue), this.CalculateIntEndValue((int) color.B, colorAnimationStep.B, numFrames, 0, (int) byte.MaxValue));
    }
  }
}
