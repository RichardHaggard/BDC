// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColorProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Telerik.WinControls.UI
{
  public class ColorProvider
  {
    private static string[] basicColorNames = new string[48]{ "#ff8080", "#ffff80", "#80ff80", "#00ff80", "#80ffff", "#0080ff", "#ff80c0", "#ff80ff", "#ff0000", "#ffff00", "#80ff00", "#00ff40", "#00ffff", "#0080c0", "#8080c0", "#ff00ff", "#804040", "#ff8040", "#00ff00", "#008080", "#004080", "#8080ff", "#800040", "#ff0080", "#800000", "#ff8000", "#008000", "#008040", "#0000ff", "#0000a0", "#800080", "#8000ff", "#400000", "#804000", "#004000", "#004040", "#000080", "#000040", "#400040", "#400080", "#000000", "#808000", "#808040", "#808080", "#408080", "#c0c0c0", "#400040", "#ffffff" };
    private static Regex parseHex = new Regex("^\\s*#?(?<value>([a-fA-F0-9]{6})|([a-fA-F0-9]{8}))\\s*$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static Color[] BasicColors
    {
      get
      {
        Color[] colorArray = new Color[ColorProvider.basicColorNames.Length];
        for (int index = 0; index < colorArray.Length; ++index)
          colorArray[index] = ColorProvider.HexToColor(ColorProvider.basicColorNames[index]);
        return colorArray;
      }
    }

    public static Color[] SystemColors
    {
      get
      {
        return (Color[]) ColorProvider.GetColorsFromType(typeof (System.Drawing.SystemColors)).ToArray(typeof (Color));
      }
    }

    public static Color[] NamedColors
    {
      get
      {
        return (Color[]) ColorProvider.GetColorsFromType(typeof (Color)).ToArray(typeof (Color));
      }
    }

    public static Color HexToColor(string color)
    {
      Match match = ColorProvider.parseHex.Match(color);
      if (!match.Success || !match.Groups["value"].Success)
        return Color.Empty;
      string s = match.Groups["value"].ToString();
      uint num = uint.Parse(s, NumberStyles.HexNumber);
      if (s.Length == 6)
        num |= 4278190080U;
      return Color.FromArgb((int) num);
    }

    public static string ColorToHex(Color color)
    {
      if (color.A == byte.MaxValue)
        return string.Format("{0:X2}{1:X2}{2:X2}", (object) color.R, (object) color.G, (object) color.B);
      return string.Format("{0:X2}{1:X2}{2:X2}{2:X2}", (object) color.A, (object) color.R, (object) color.G, (object) color.B);
    }

    public static int Round(double val)
    {
      int num = (int) val;
      if ((int) (val * 100.0) % 100 >= 50)
        ++num;
      return num;
    }

    private static ArrayList GetColorsFromType(Type type)
    {
      ArrayList arrayList = new ArrayList();
      foreach (PropertyInfo property in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
      {
        if (property.PropertyType.Equals(typeof (Color)))
        {
          Color color = (Color) property.GetValue((object) type, (object[]) null);
          arrayList.Add((object) color);
        }
      }
      return arrayList;
    }
  }
}
