// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationValueCalculator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public abstract class AnimationValueCalculator
  {
    public abstract System.Type AssociatedType { get; }

    public abstract object CalculateAnimatedValue(
      object startValue,
      object endValue,
      object currValue,
      object step,
      int currFrameNum,
      int totalFrameNum,
      EasingCalculator calc);

    public abstract object CalculateInversedStep(object step);

    public virtual string ConvertAnimationStepToString(object value)
    {
      if (value == null)
        return (string) null;
      string str = value.ToString();
      TypeConverter converter = TypeDescriptor.GetConverter(this.AssociatedType);
      if (converter != null)
      {
        if (converter.CanConvertTo(typeof (string)))
        {
          try
          {
            str = (string) converter.ConvertTo(value, typeof (string));
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Error converting animation value: " + ex.Message);
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show("Can't find TypeConverter to string for animation step " + value.ToString());
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("Can't find any TypeConverter for animation property " + value.ToString());
      }
      return str;
    }

    public virtual object ConvertToAnimationStepFromString(string value)
    {
      if (value == null)
        return (object) null;
      object obj = (object) null;
      TypeConverter converter = TypeDescriptor.GetConverter(this.AssociatedType);
      if (converter != null)
      {
        if (converter.CanConvertFrom(typeof (string)))
        {
          try
          {
            obj = converter.ConvertFrom((object) value);
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Error setting animation value: " + ex.Message);
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show(string.Format("The TypeConverter - {0} cannot convert from string, animation step of type {1}", (object) converter.ToString(), (object) this.AssociatedType.FullName));
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("Can't find any TypeConverter for animation step of type " + this.AssociatedType.FullName);
      }
      return obj;
    }

    public abstract object CalculateAnimationStep(
      object animationStartValue,
      object animationEndValue,
      int numFrames);

    protected int CalculateIntStep(int animationStartValue, int animationEndValue, int numFrames)
    {
      return this.CalculateIntStep(animationStartValue, animationEndValue, numFrames, int.MinValue, int.MaxValue);
    }

    protected int CalculateIntStep(
      int animationStartValue,
      int animationEndValue,
      int numFrames,
      int minValue,
      int maxValue)
    {
      if (numFrames <= 0)
        return 0;
      return Math.Min(maxValue, Math.Max(minValue, animationEndValue - animationStartValue) / numFrames);
    }

    protected float CalculateFloatStep(
      float animationStartValue,
      float animationEndValue,
      int numFrames)
    {
      return this.CalculateFloatStep(animationStartValue, animationEndValue, (float) numFrames, float.MinValue, float.MaxValue);
    }

    protected float CalculateFloatStep(
      float animationStartValue,
      float animationEndValue,
      float numFrames,
      float minValue,
      float maxValue)
    {
      if ((double) numFrames <= 0.0)
        return 0.0f;
      return Math.Min(maxValue, Math.Max(minValue, animationEndValue - animationStartValue) / numFrames);
    }

    protected double CalculateDoubleStep(
      double animationStartValue,
      double animationEndValue,
      int numFrames)
    {
      return this.CalculateDoubleStep(animationStartValue, animationEndValue, (double) numFrames, double.MinValue, double.MaxValue);
    }

    protected double CalculateDoubleStep(
      double animationStartValue,
      double animationEndValue,
      double numFrames,
      double minValue,
      double maxValue)
    {
      if (numFrames <= 0.0)
        return 0.0;
      return Math.Min(maxValue, Math.Max(minValue, animationEndValue - animationStartValue) / numFrames);
    }

    public abstract object CalculateAnimationEndValue(
      object animationStartValue,
      object step,
      int numFrames);

    protected float CalculateFloatEndValue(
      float animationStartValue,
      float animationStep,
      int numFrames)
    {
      return this.CalculateFloatEndValue(animationStartValue, animationStep, numFrames, float.MinValue, float.MaxValue);
    }

    protected float CalculateFloatEndValue(
      float animationStartValue,
      float animationStep,
      int numFrames,
      float minValue,
      float maxValue)
    {
      if (numFrames <= 0)
        return 0.0f;
      return Math.Min(maxValue, Math.Max(minValue, animationStartValue + animationStep * (float) numFrames));
    }

    protected double CalculateDoubleEndValue(
      double animationStartValue,
      double animationStep,
      int numFrames)
    {
      return this.CalculateDoubleEndValue(animationStartValue, animationStep, numFrames, double.MinValue, double.MaxValue);
    }

    protected double CalculateDoubleEndValue(
      double animationStartValue,
      double animationStep,
      int numFrames,
      double minValue,
      double maxValue)
    {
      if (numFrames <= 0)
        return 0.0;
      return Math.Min(maxValue, Math.Max(minValue, animationStartValue + animationStep * (double) numFrames));
    }

    protected int CalculateIntEndValue(int animationStartValue, int animationStep, int numFrames)
    {
      return this.CalculateIntEndValue(animationStartValue, animationStep, numFrames, int.MinValue, int.MaxValue);
    }

    protected int CalculateIntEndValue(
      int animationStartValue,
      int animationStep,
      int numFrames,
      int minValue,
      int maxValue)
    {
      if (numFrames <= 0)
        return 0;
      return Math.Min(maxValue, Math.Max(minValue, animationStartValue + animationStep * numFrames));
    }
  }
}
