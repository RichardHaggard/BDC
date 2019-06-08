// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueRectangleCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class AnimationValueRectangleCalculator : AnimationValueCalculator
  {
    public override Type AssociatedType
    {
      get
      {
        return typeof (Rectangle);
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
      Rectangle rectangle1 = Rectangle.Empty;
      if (currValue != null)
        rectangle1 = (Rectangle) currValue;
      if (endValue != null)
      {
        int initialValue1 = rectangle1.Left;
        int initialValue2 = rectangle1.Top;
        int width = rectangle1.Width;
        int height = rectangle1.Height;
        if ((object) startValue.GetType() == (object) typeof (Rectangle))
        {
          Rectangle rectangle2 = (Rectangle) startValue;
          initialValue1 = rectangle2.Left;
          initialValue2 = rectangle2.Top;
          width = rectangle2.Width;
          height = rectangle2.Height;
        }
        else if ((object) startValue.GetType() == (object) typeof (Point))
        {
          Point point = (Point) startValue;
          initialValue1 = point.X;
          initialValue2 = point.Y;
        }
        else
        {
          startValue.GetType();
          Type type = typeof (Size);
        }
        int endValue1 = initialValue1;
        int endValue2 = initialValue2;
        int endValue3 = width;
        int endValue4 = height;
        if ((object) endValue.GetType() == (object) typeof (Rectangle))
        {
          Rectangle rectangle2 = (Rectangle) endValue;
          endValue1 = rectangle2.Left;
          endValue2 = rectangle2.Top;
          endValue3 = rectangle2.Width;
          endValue4 = rectangle2.Height;
        }
        else if ((object) endValue.GetType() == (object) typeof (Point))
        {
          Point point = (Point) endValue;
          endValue1 = point.X;
          endValue2 = point.Y;
        }
        else if ((object) endValue.GetType() == (object) typeof (Size))
        {
          Size size = (Size) endValue;
          endValue3 = size.Width;
          endValue4 = size.Height;
        }
        return (object) new Rectangle(calc.CalculateCurrentValue(initialValue1, endValue1, currFrameNum, totalFrameNum), calc.CalculateCurrentValue(initialValue2, endValue2, currFrameNum, totalFrameNum), calc.CalculateCurrentValue(width, endValue3, currFrameNum, totalFrameNum), calc.CalculateCurrentValue(height, endValue4, currFrameNum, totalFrameNum));
      }
      if (step == null)
        return currValue;
      if ((object) step.GetType() == (object) typeof (Rectangle))
      {
        Rectangle rectangle2 = (Rectangle) step;
        return (object) new Rectangle(rectangle1.X + rectangle2.X, rectangle1.Y + rectangle2.Y, rectangle1.Width + rectangle2.Width, rectangle1.Height + rectangle2.Height);
      }
      if ((object) step.GetType() == (object) typeof (Point))
      {
        Point point = (Point) step;
        return (object) new Rectangle(rectangle1.X + point.X, rectangle1.Y + point.Y, rectangle1.Width, rectangle1.Height);
      }
      if ((object) step.GetType() != (object) typeof (Size))
        return currValue;
      Size size1 = (Size) step;
      return (object) new Rectangle(rectangle1.X, rectangle1.Y, rectangle1.Width + size1.Width, rectangle1.Height + size1.Height);
    }

    public override object CalculateInversedStep(object step)
    {
      Rectangle rectangle = (Rectangle) step;
      return (object) new Rectangle(-rectangle.X, -rectangle.Y, -rectangle.Width, -rectangle.Height);
    }

    public override object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames)
    {
      Rectangle rectangle1 = Rectangle.Empty;
      Rectangle rectangle2 = Rectangle.Empty;
      if ((object) animationEndValue.GetType() == (object) typeof (Rectangle))
        rectangle2 = (Rectangle) animationEndValue;
      else if ((object) animationEndValue.GetType() == (object) typeof (Point))
        rectangle2 = new Rectangle((Point) animationEndValue, Size.Empty);
      else if ((object) animationEndValue.GetType() == (object) typeof (Size))
        rectangle2 = new Rectangle(Point.Empty, (Size) animationEndValue);
      if ((object) animationStartValue.GetType() == (object) typeof (Rectangle))
        rectangle1 = (Rectangle) animationStartValue;
      else if ((object) animationStartValue.GetType() == (object) typeof (Point))
        rectangle1 = new Rectangle((Point) animationStartValue, Size.Empty);
      else if ((object) animationStartValue.GetType() == (object) typeof (Size))
        rectangle1 = new Rectangle(Point.Empty, (Size) animationStartValue);
      return (object) new Rectangle(this.CalculateIntStep(rectangle1.X, rectangle2.X, numFrames), this.CalculateIntStep(rectangle1.Y, rectangle2.Y, numFrames), this.CalculateIntStep(rectangle1.Width, rectangle2.Width, numFrames), this.CalculateIntStep(rectangle1.Height, rectangle2.Height, numFrames));
    }

    public override object CalculateAnimationEndValue(
      object animationStartValue,
      object animationStep,
      int numFrames)
    {
      Rectangle rectangle1 = Rectangle.Empty;
      Rectangle rectangle2 = (Rectangle) animationStep;
      if ((object) animationStartValue.GetType() == (object) typeof (Rectangle))
        rectangle1 = (Rectangle) animationStartValue;
      else if ((object) animationStartValue.GetType() == (object) typeof (Point))
        rectangle1 = new Rectangle((Point) animationStartValue, Size.Empty);
      else if ((object) animationStartValue.GetType() == (object) typeof (Size))
        rectangle1 = new Rectangle(Point.Empty, (Size) animationStartValue);
      return (object) new Rectangle(this.CalculateIntEndValue(rectangle1.X, rectangle2.X, numFrames), this.CalculateIntEndValue(rectangle1.Y, rectangle2.Y, numFrames), this.CalculateIntEndValue(rectangle1.Width, rectangle2.Width, numFrames), this.CalculateIntEndValue(rectangle1.Height, rectangle2.Height, numFrames));
    }
  }
}
