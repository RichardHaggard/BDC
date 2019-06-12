// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.HslColor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (HslColorConverter))]
  public struct HslColor
  {
    public static readonly HslColor Empty = new HslColor();
    private double hue;
    private double saturation;
    private double luminance;
    private int alpha;

    private HslColor(int a, double h, double s, double l)
    {
      this.alpha = a;
      this.hue = h;
      this.saturation = s;
      this.luminance = l;
      this.A = a;
      this.H = this.hue;
      this.S = this.saturation;
      this.L = this.luminance;
    }

    private HslColor(double h, double s, double l)
    {
      this.alpha = (int) byte.MaxValue;
      this.hue = h;
      this.saturation = s;
      this.luminance = l;
    }

    private HslColor(Color color)
    {
      this.alpha = (int) color.A;
      this.hue = 0.0;
      this.saturation = 0.0;
      this.luminance = 0.0;
      this.RGBtoHSL(color);
    }

    public static HslColor FromArgb(int a, int r, int g, int b)
    {
      return new HslColor(Color.FromArgb(a, r, g, b));
    }

    public static HslColor FromColor(Color color)
    {
      return new HslColor(color);
    }

    public static HslColor FromAhsl(int a)
    {
      return new HslColor(a, 0.0, 0.0, 0.0);
    }

    public static HslColor FromAhsl(int a, HslColor hsl)
    {
      return new HslColor(a, hsl.hue, hsl.saturation, hsl.luminance);
    }

    public static HslColor FromAhsl(double h, double s, double l)
    {
      return new HslColor((int) byte.MaxValue, h, s, l);
    }

    public static HslColor FromAhsl(int a, double h, double s, double l)
    {
      return new HslColor(a, h, s, l);
    }

    public static bool operator ==(HslColor left, HslColor right)
    {
      return left.A == right.A && left.H == right.H && (left.S == right.S && left.L == right.L);
    }

    public static bool operator !=(HslColor left, HslColor right)
    {
      return !(left == right);
    }

    public override bool Equals(object obj)
    {
      if (obj is HslColor)
      {
        HslColor hslColor = (HslColor) obj;
        if (this.A == hslColor.A && this.H == hslColor.H && (this.S == hslColor.S && this.L == hslColor.L))
          return true;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return this.alpha.GetHashCode() ^ this.hue.GetHashCode() ^ this.saturation.GetHashCode() ^ this.luminance.GetHashCode();
    }

    [DefaultValue(0.0)]
    [Description("H Channel value")]
    [Category("Appearance")]
    public double H
    {
      get
      {
        return this.hue;
      }
      set
      {
        this.hue = value;
        this.hue = this.hue > 1.0 ? 1.0 : (this.hue < 0.0 ? 0.0 : this.hue);
      }
    }

    [Description("S Channel value")]
    [DefaultValue(0.0)]
    [Category("Appearance")]
    public double S
    {
      get
      {
        return this.saturation;
      }
      set
      {
        this.saturation = value;
        this.saturation = this.saturation > 1.0 ? 1.0 : (this.saturation < 0.0 ? 0.0 : this.saturation);
      }
    }

    [Category("Appearance")]
    [DefaultValue(0.0)]
    [Description("L Channel value")]
    public double L
    {
      get
      {
        return this.luminance;
      }
      set
      {
        this.luminance = value;
        this.luminance = this.luminance > 1.0 ? 1.0 : (this.luminance < 0.0 ? 0.0 : this.luminance);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color RgbValue
    {
      get
      {
        return this.HSLtoRGB();
      }
      set
      {
        this.RGBtoHSL(value);
      }
    }

    public int A
    {
      get
      {
        return this.alpha;
      }
      set
      {
        this.alpha = value > (int) byte.MaxValue ? (int) byte.MaxValue : (value < 0 ? 0 : value);
      }
    }

    public bool IsEmpty
    {
      get
      {
        if (this.alpha == 0 && this.H == 0.0 && this.S == 0.0)
          return this.L == 0.0;
        return false;
      }
    }

    private Color HSLtoRGB()
    {
      int num1 = this.Round(this.luminance * (double) byte.MaxValue);
      int num2 = this.Round((1.0 - this.saturation) * (this.luminance / 1.0) * (double) byte.MaxValue);
      double num3 = (double) (num1 - num2) / (double) byte.MaxValue;
      if (this.hue >= 0.0 && this.hue <= 1.0 / 6.0)
      {
        int green = this.Round((this.hue - 0.0) * num3 * 1530.0 + (double) num2);
        return Color.FromArgb(this.alpha, num1, green, num2);
      }
      if (this.hue <= 1.0 / 3.0)
        return Color.FromArgb(this.alpha, this.Round(-((this.hue - 1.0 / 6.0) * num3) * 1530.0 + (double) num1), num1, num2);
      if (this.hue <= 0.5)
      {
        int blue = this.Round((this.hue - 1.0 / 3.0) * num3 * 1530.0 + (double) num2);
        return Color.FromArgb(this.alpha, num2, num1, blue);
      }
      if (this.hue <= 2.0 / 3.0)
      {
        int green = this.Round(-((this.hue - 0.5) * num3) * 1530.0 + (double) num1);
        return Color.FromArgb(this.alpha, num2, green, num1);
      }
      if (this.hue <= 5.0 / 6.0)
        return Color.FromArgb(this.alpha, this.Round((this.hue - 2.0 / 3.0) * num3 * 1530.0 + (double) num2), num2, num1);
      if (this.hue > 1.0)
        return Color.FromArgb(this.alpha, 0, 0, 0);
      int blue1 = this.Round(-((this.hue - 5.0 / 6.0) * num3) * 1530.0 + (double) num1);
      return Color.FromArgb(this.alpha, num1, num2, blue1);
    }

    private void RGBtoHSL(Color color)
    {
      this.alpha = (int) color.A;
      int num1;
      int num2;
      if ((int) color.R > (int) color.G)
      {
        num1 = (int) color.R;
        num2 = (int) color.G;
      }
      else
      {
        num1 = (int) color.G;
        num2 = (int) color.R;
      }
      if ((int) color.B > num1)
        num1 = (int) color.B;
      else if ((int) color.B < num2)
        num2 = (int) color.B;
      int num3 = num1 - num2;
      this.luminance = (double) num1 / (double) byte.MaxValue;
      this.saturation = num1 != 0 ? (double) num3 / (double) num1 : 0.0;
      double num4 = num3 != 0 ? 60.0 / (double) num3 : 0.0;
      if (num1 == (int) color.R)
      {
        if ((int) color.G < (int) color.B)
          this.hue = (360.0 + num4 * (double) ((int) color.G - (int) color.B)) / 360.0;
        else
          this.hue = num4 * (double) ((int) color.G - (int) color.B) / 360.0;
      }
      else if (num1 == (int) color.G)
        this.hue = (120.0 + num4 * (double) ((int) color.B - (int) color.R)) / 360.0;
      else if (num1 == (int) color.B)
        this.hue = (240.0 + num4 * (double) ((int) color.R - (int) color.G)) / 360.0;
      else
        this.hue = 0.0;
    }

    private int Round(double val)
    {
      return (int) (val + 0.5);
    }
  }
}
