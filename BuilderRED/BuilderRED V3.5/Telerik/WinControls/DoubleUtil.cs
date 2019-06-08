// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.DoubleUtil
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public static class DoubleUtil
  {
    private const float epsilon = 4.649123E-07f;

    public static bool AreClose(float value1, float value2)
    {
      if ((double) value1 == (double) value2)
        return true;
      float num1 = (float) (((double) Math.Abs(value1) + (double) Math.Abs(value2) + 10.0) * 4.6491228999912E-07);
      float num2 = value1 - value2;
      if (-(double) num1 < (double) num2)
        return (double) num1 > (double) num2;
      return false;
    }

    public static bool AreClose(PointF point1, PointF point2)
    {
      if (DoubleUtil.AreClose(point1.X, point2.X))
        return DoubleUtil.AreClose(point1.Y, point2.Y);
      return false;
    }

    public static bool AreClose(RectangleF rect1, RectangleF rect2)
    {
      if (rect1.IsEmpty)
        return rect2.IsEmpty;
      if (!rect2.IsEmpty && DoubleUtil.AreClose(rect1.X, rect2.X) && (DoubleUtil.AreClose(rect1.Y, rect2.Y) && DoubleUtil.AreClose(rect1.Height, rect2.Height)))
        return DoubleUtil.AreClose(rect1.Width, rect2.Width);
      return false;
    }

    public static bool AreClose(SizeF size1, SizeF size2)
    {
      if (DoubleUtil.AreClose(size1.Width, size2.Width))
        return DoubleUtil.AreClose(size1.Height, size2.Height);
      return false;
    }

    public static int DoubleToInt(float val)
    {
      if (0.0 >= (double) val)
        return (int) ((double) val - 0.5);
      return (int) ((double) val + 0.5);
    }

    public static bool GreaterThan(float value1, float value2)
    {
      if ((double) value1 > (double) value2)
        return !DoubleUtil.AreClose(value1, value2);
      return false;
    }

    public static bool GreaterThanOrClose(float value1, float value2)
    {
      if ((double) value1 <= (double) value2)
        return DoubleUtil.AreClose(value1, value2);
      return true;
    }

    public static bool IsBetweenZeroAndOne(float val)
    {
      if (DoubleUtil.GreaterThanOrClose(val, 0.0f))
        return DoubleUtil.LessThanOrClose(val, 1f);
      return false;
    }

    public static bool IsOne(float value)
    {
      return (double) Math.Abs(value - 1f) < 4.64912272946094E-06;
    }

    public static bool IsZero(float value)
    {
      return (double) Math.Abs(value) < 4.64912272946094E-06;
    }

    public static bool LessThan(float value1, float value2)
    {
      if ((double) value1 < (double) value2)
        return !DoubleUtil.AreClose(value1, value2);
      return false;
    }

    public static bool LessThanOrClose(float value1, float value2)
    {
      if ((double) value1 >= (double) value2)
        return DoubleUtil.AreClose(value1, value2);
      return true;
    }

    public static bool IsLessThan(double value1, double value2)
    {
      return DoubleUtil.LessThan(value1, value2);
    }

    public static bool LessThan(double value1, double value2)
    {
      if (value1 < value2)
        return !DoubleUtil.AreClose((float) value1, (float) value2);
      return false;
    }
  }
}
