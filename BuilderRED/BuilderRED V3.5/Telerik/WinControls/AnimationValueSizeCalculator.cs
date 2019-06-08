// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueSizeCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class AnimationValueSizeCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (Size);
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
      Size size1 = (Size) step;
      Size size2 = (Size) currValue;
      if (currFrameNum == totalFrameNum)
        return endValue;
      return (object) new Size(size2.Width + size1.Width, size2.Height + size1.Height);
    }

    public override object CalculateInversedStep(object step)
    {
      Size size = (Size) step;
      return (object) new Size(-size.Width, -size.Height);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      Size size1 = (Size) animationStartValue;
      Size size2 = (Size) animationEndValue;
      return (object) new Size(this.CalculateIntStep(size1.Width, size2.Width, numFrames), this.CalculateIntStep(size1.Height, size2.Height, numFrames));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      Size size1 = (Size) animationStartValue;
      Size size2 = (Size) animationStep;
      return (object) new Size(this.CalculateIntStep(size1.Width, size2.Width, numFrames), this.CalculateIntStep(size1.Height, size2.Height, numFrames));
    }
  }
}
