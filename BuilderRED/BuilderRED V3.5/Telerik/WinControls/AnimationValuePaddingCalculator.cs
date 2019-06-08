// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValuePaddingCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class AnimationValuePaddingCalculator : AnimationValueCalculator
  {
    public override System.Type AssociatedType
    {
      get
      {
        return typeof (Padding);
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
      {
        Padding padding1 = (Padding) step;
        Padding padding2 = (Padding) currValue;
        return (object) new Padding(padding2.Left + padding1.Left, padding2.Top + padding1.Top, padding2.Right + padding1.Right, padding2.Bottom + padding1.Bottom);
      }
      Padding padding3 = (Padding) startValue;
      Padding padding4 = (Padding) endValue;
      return (object) new Padding(calc.CalculateCurrentValue(padding3.Left, padding4.Left, currFrameNum, totalFrameNum), calc.CalculateCurrentValue(padding3.Top, padding4.Top, currFrameNum, totalFrameNum), calc.CalculateCurrentValue(padding3.Right, padding4.Right, currFrameNum, totalFrameNum), calc.CalculateCurrentValue(padding3.Bottom, padding4.Bottom, currFrameNum, totalFrameNum));
    }

    public override object CalculateInversedStep(object step)
    {
      Padding padding = (Padding) step;
      return (object) new Padding(-padding.Left, -padding.Top, -padding.Right, -padding.Bottom);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      Padding padding1 = (Padding) animationStartValue;
      Padding padding2 = (Padding) animationEndValue;
      return (object) new Padding(this.CalculateIntStep(padding1.Left, padding2.Left, numFrames), this.CalculateIntStep(padding1.Top, padding2.Top, numFrames), this.CalculateIntStep(padding1.Right, padding2.Right, numFrames), this.CalculateIntStep(padding1.Bottom, padding2.Bottom, numFrames));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      Padding padding1 = (Padding) animationStartValue;
      Padding padding2 = (Padding) animationStep;
      return (object) new Padding(this.CalculateIntEndValue(padding1.Left, padding2.Left, numFrames), this.CalculateIntEndValue(padding1.Top, padding2.Top, numFrames), this.CalculateIntEndValue(padding1.Right, padding2.Right, numFrames), this.CalculateIntEndValue(padding1.Bottom, padding2.Bottom, numFrames));
    }
  }
}
