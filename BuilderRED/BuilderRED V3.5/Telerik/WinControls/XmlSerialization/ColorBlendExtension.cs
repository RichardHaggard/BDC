// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.ColorBlendExtension
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.XmlSerialization
{
  public class ColorBlendExtension : XmlSerializerExtention, IValueProvider
  {
    private string themePropertyName;
    private IPropertiesProvider styleParameterSerivce;
    private Color originalColor;
    private int colorADiff;
    private double colorHDiff;
    private double colorSDiff;
    private double colorLDiff;

    public ColorBlendExtension()
    {
    }

    public ColorBlendExtension(
      IPropertiesProvider parameterProvider,
      string parameterName,
      HslColor blendColor,
      Color currentColor,
      Color originalColor)
    {
      this.styleParameterSerivce = parameterProvider;
      this.themePropertyName = parameterName;
      this.originalColor = originalColor;
      HslColor hslColor = HslColor.FromColor(currentColor);
      this.colorADiff = hslColor.A - blendColor.A;
      this.colorHDiff = hslColor.H - blendColor.H;
      this.colorSDiff = hslColor.S - blendColor.S;
      this.colorLDiff = hslColor.L - blendColor.L;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      IProvideTargetValue service = IServiceProviderHelper<IProvideTargetValue>.GetService(serviceProvider, typeof (ColorBlendExtension).ToString());
      this.styleParameterSerivce = IServiceProviderHelper<IPropertiesProvider>.GetService(serviceProvider, typeof (ColorBlendExtension).ToString());
      string[] strArray = service.SourceValue.Split(',');
      if (strArray.Length < 4)
        throw new ArgumentException("parameters not valid!");
      this.themePropertyName = strArray[0].Trim();
      if (string.IsNullOrEmpty(this.ThemePropertyName))
        throw new InvalidOperationException("The first argument of RelativeColor exptrssion should be the name of the ThemeProperty");
      int.TryParse(strArray[1].Trim(), out this.colorADiff);
      double.TryParse(strArray[2].Trim(), out this.colorHDiff);
      double.TryParse(strArray[3].Trim(), out this.colorSDiff);
      double.TryParse(strArray[4].Trim(), out this.colorLDiff);
      return (object) this;
    }

    public object GetValue()
    {
      object propertyValue = this.styleParameterSerivce.GetPropertyValue(this.ThemePropertyName);
      HslColor hslColor = HslColor.FromAhsl((int) byte.MaxValue);
      if (propertyValue == null)
        return (object) this.originalColor;
      if ((object) propertyValue.GetType() == (object) typeof (Color))
        hslColor = HslColor.FromColor((Color) propertyValue);
      else if (propertyValue is HslColor)
        hslColor = (HslColor) propertyValue;
      return (object) HslColor.FromAhsl(hslColor.A + this.colorADiff, Math.Abs(hslColor.H + this.colorHDiff) - Math.Floor(hslColor.H + this.colorHDiff), Math.Min(1.0, Math.Max(0.0, hslColor.S + this.colorSDiff)), Math.Min(1.0, Math.Max(0.0, hslColor.L + this.colorLDiff))).RgbValue;
    }

    public Color OriginalColor
    {
      get
      {
        return this.originalColor;
      }
    }

    public string ThemePropertyName
    {
      get
      {
        return this.themePropertyName;
      }
    }
  }
}
