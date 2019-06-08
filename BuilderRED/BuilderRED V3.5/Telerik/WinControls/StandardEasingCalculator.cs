// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.StandardEasingCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class StandardEasingCalculator : EasingCalculator
  {
    private RadEasingType easingType = RadEasingType.InQuad;

    public StandardEasingCalculator()
    {
    }

    public StandardEasingCalculator(RadEasingType easingType)
    {
      this.EasingType = easingType;
    }

    public RadEasingType EasingType
    {
      get
      {
        return this.easingType;
      }
      set
      {
        this.easingType = value;
      }
    }

    private double OutBounce(double percentDone, double difference)
    {
      if (percentDone < 4.0 / 11.0)
        return difference * (121.0 / 16.0 * percentDone * percentDone);
      if (percentDone < 8.0 / 11.0)
        return difference * (121.0 / 16.0 * (percentDone - 6.0 / 11.0) * (percentDone - 6.0 / 11.0) + 0.75);
      if (percentDone < 10.0 / 11.0)
        return difference * (121.0 / 16.0 * (percentDone - 9.0 / 11.0) * (percentDone - 9.0 / 11.0) + 15.0 / 16.0);
      return difference * (121.0 / 16.0 * (percentDone - 21.0 / 22.0) * (percentDone - 21.0 / 22.0) + 63.0 / 64.0);
    }

    private double Elastic(double percentDone, double difference, double period, double amplitude)
    {
      if (period == 0.0)
        period = difference * 0.3;
      double num;
      if (amplitude == 0.0 || amplitude < Math.Abs(difference))
      {
        amplitude = difference;
        num = period / 4.0;
      }
      else
        num = period / (2.0 * Math.PI) * Math.Asin(difference / amplitude);
      return difference * Math.Pow(2.0, 10.0 * percentDone) * Math.Sin((percentDone * difference - num) * (2.0 * Math.PI) / period);
    }

    private double FastPow(double num, int pow)
    {
      double num1 = num;
      for (int index = 1; index < pow; ++index)
        num1 *= num;
      return num1;
    }

    public override int CalculateCurrentValue(
      int initialValue,
      int endValue,
      int currentFrame,
      int numFrames)
    {
      return (int) Math.Round(this.CalculateCurrentValue((double) initialValue, (double) endValue, currentFrame, numFrames));
    }

    public override float CalculateCurrentValue(
      float initialValue,
      float endValue,
      int currentFrame,
      int numFrames)
    {
      return (float) this.CalculateCurrentValue((double) initialValue, (double) endValue, currentFrame, numFrames);
    }

    public override double CalculateCurrentValue(
      double initialValue,
      double endValue,
      int currentFrame,
      int numFrames)
    {
      if (initialValue == endValue)
        return endValue;
      if (currentFrame <= 0)
        return initialValue;
      if (currentFrame >= numFrames)
        return endValue;
      double num1 = (double) currentFrame / (double) numFrames;
      double difference = endValue - initialValue;
      double period = 0.0;
      double amplitude = 0.0;
      double num2 = 1.70158;
      switch (this.EasingType)
      {
        case RadEasingType.InQuad:
          return difference * num1 * num1 + initialValue;
        case RadEasingType.OutQuad:
          return -difference * num1 * (num1 - 2.0) + initialValue;
        case RadEasingType.InOutQuad:
          if (num1 < 0.5)
            return 2.0 * difference * num1 * num1 + initialValue;
          return -difference * (2.0 * (num1 - 1.0) * (num1 - 1.0) - 1.0) + initialValue;
        case RadEasingType.InCubic:
          return difference * this.FastPow(num1, 3) + initialValue;
        case RadEasingType.OutCubic:
          return difference * (this.FastPow(num1 - 1.0, 3) + 1.0) + initialValue;
        case RadEasingType.InOutCubic:
          if (num1 < 0.5)
            return 4.0 * difference * this.FastPow(num1, 3) + initialValue;
          return difference * (4.0 * this.FastPow(num1 - 1.0, 3) + 1.0) + initialValue;
        case RadEasingType.InQuart:
          return difference * this.FastPow(num1, 4) + initialValue;
        case RadEasingType.OutQuart:
          return -difference * (this.FastPow(num1 - 1.0, 4) - 1.0) + initialValue;
        case RadEasingType.InOutQuart:
          if (num1 < 0.5)
            return 8.0 * difference * this.FastPow(num1, 4) + initialValue;
          return -difference * (8.0 * this.FastPow(num1 - 1.0, 4) - 1.0) + initialValue;
        case RadEasingType.InQuint:
          return difference * this.FastPow(num1, 5) + initialValue;
        case RadEasingType.OutQuint:
          return difference * (this.FastPow(num1 - 1.0, 5) + 1.0) + initialValue;
        case RadEasingType.InOutQuint:
          if (num1 < 0.5)
            return 8.0 * difference * this.FastPow(num1, 5) + initialValue;
          return difference * (8.0 * this.FastPow(num1 - 1.0, 5) + 1.0) + initialValue;
        case RadEasingType.InSine:
          return difference * (1.0 - Math.Cos(num1 * Math.PI / 2.0)) + initialValue;
        case RadEasingType.OutSine:
          return difference * Math.Sin(num1 * Math.PI / 2.0) + initialValue;
        case RadEasingType.InOutSine:
          return difference / 2.0 * (1.0 - Math.Cos(num1 * Math.PI)) + initialValue;
        case RadEasingType.InExponential:
          return difference * Math.Pow(2.0, 10.0 * (num1 - 1.0)) + initialValue;
        case RadEasingType.OutExponential:
          return difference * (1.0 - Math.Pow(2.0, -10.0 * num1)) + initialValue;
        case RadEasingType.InOutExponential:
          if (num1 < 0.5)
            return difference * Math.Pow(2.0, 20.0 * num1 - 11.0) + initialValue;
          return difference * (1.0 - Math.Pow(2.0, -20.0 * num1 + 9.0)) + initialValue;
        case RadEasingType.InCircular:
          return difference * (1.0 - Math.Sqrt(1.0 - num1 * num1)) + initialValue;
        case RadEasingType.OutCircular:
          return difference * Math.Sqrt(num1 * (2.0 - num1)) + initialValue;
        case RadEasingType.InOutCircular:
          if (num1 < 0.5)
            return difference / 2.0 * (1.0 - Math.Sqrt(1.0 - 4.0 * num1 * num1)) + initialValue;
          return difference / 2.0 * (1.0 + Math.Sqrt(1.0 - 4.0 * (num1 - 1.0) * (num1 - 1.0))) + initialValue;
        case RadEasingType.InElastic:
          return initialValue - this.Elastic(num1 - 1.0, difference, period, amplitude);
        case RadEasingType.OutElastic:
          return endValue + this.Elastic(-num1, difference, period, amplitude);
        case RadEasingType.InOutElastic:
          if (period == 0.0)
            period = difference * 0.45;
          if (num1 < 0.5)
            return initialValue - 0.5 * this.Elastic(num1 * 2.0 - 1.0, difference, period, amplitude);
          return endValue + 0.5 * this.Elastic(1.0 - num1 * 2.0, difference, period, amplitude);
        case RadEasingType.InBack:
          return difference * num1 * num1 * ((num2 + 1.0) * num1 - num2) + initialValue;
        case RadEasingType.OutBack:
          return difference * (this.FastPow(num1 - 1.0, 2) * ((num2 + 1.0) * (num1 - 1.0) + num2) + 1.0) + initialValue;
        case RadEasingType.InOutBack:
          double num3 = num2 * 1.525;
          if (num1 < 0.5)
            return 2.0 * difference * this.FastPow(num1, 2) * ((num3 + 1.0) * 2.0 * num1 - num3) + initialValue;
          return difference * (2.0 * this.FastPow(num1 - 1.0, 2) * ((num3 + 1.0) * 2.0 * (num1 - 1.0) + num3) + 1.0) + initialValue;
        case RadEasingType.InBounce:
          return difference - this.OutBounce(1.0 - num1, difference) + initialValue;
        case RadEasingType.OutBounce:
          return this.OutBounce(num1, difference) + initialValue;
        case RadEasingType.InOutBounce:
          if (num1 < 0.5)
            return 0.5 * (difference - this.OutBounce(1.0 - 2.0 * num1, difference)) + initialValue;
          return 0.5 * (difference + this.OutBounce(2.0 * num1 - 1.0, difference)) + initialValue;
        default:
          return difference * num1 + initialValue;
      }
    }
  }
}
