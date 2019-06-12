// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.ColorServices
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI.RadColorPicker
{
  public class ColorServices
  {
    public static Color ColorFromRGBRatios(double value1, double value2, double value3)
    {
      int blue;
      int green;
      int red;
      if (value3 == 0.0)
      {
        int num;
        blue = num = (int) (value2 * (double) byte.MaxValue);
        green = num;
        red = num;
      }
      else
      {
        float num1 = value2 > 0.5 ? (float) (value2 + value3 - value2 * value3) : (float) (value2 + value2 * value3);
        float num2 = (float) (2.0 * value2) - num1;
        red = ColorServices.GetColorChannelValue(num2, num1, (float) (value1 + 120.0));
        green = ColorServices.GetColorChannelValue(num2, num1, (float) value1);
        blue = ColorServices.GetColorChannelValue(num2, num1, (float) (value1 - 120.0));
      }
      return Color.FromArgb(red, green, blue);
    }

    private static int GetColorChannelValue(float value1, float value2, float value3)
    {
      if ((double) value3 > 360.0)
        value3 -= 360f;
      else if ((double) value3 < 0.0)
        value3 += 360f;
      if ((double) value3 < 60.0)
        value1 += (float) (((double) value2 - (double) value1) * (double) value3 / 60.0);
      else if ((double) value3 < 180.0)
        value1 = value2;
      else if ((double) value3 < 240.0)
        value1 += (float) (((double) value2 - (double) value1) * (240.0 - (double) value3) / 60.0);
      return (int) ((double) value1 * (double) byte.MaxValue);
    }

    public static float GetColorQuotient(float value1, float value2)
    {
      return (float) (Math.Atan2((double) value2, (double) value1) * 180.0 / Math.PI);
    }
  }
}
