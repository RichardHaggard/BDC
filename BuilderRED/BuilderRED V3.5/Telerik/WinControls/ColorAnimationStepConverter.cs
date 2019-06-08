// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ColorAnimationStepConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls
{
  public class ColorAnimationStepConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return (object) sourceType == (object) typeof (string);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      string str = (string) value;
      if (string.IsNullOrEmpty(str))
      {
        ColorAnimationStep colorAnimationStep = new ColorAnimationStep(0, 0, 0, 0);
      }
      CultureInfo serializationCulture = AnimationValueCalculatorFactory.SerializationCulture;
      string[] strArray = str.Split(serializationCulture.TextInfo.ListSeparator[0]);
      if (strArray.Length < 2)
        strArray = str.Split(',');
      if (strArray.Length == 3)
      {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
        return (object) new ColorAnimationStep(0, (int) converter.ConvertFrom(context, serializationCulture, (object) strArray[0]), (int) converter.ConvertFrom(context, serializationCulture, (object) strArray[1]), (int) converter.ConvertFrom(context, serializationCulture, (object) strArray[2]));
      }
      if (strArray.Length != 4)
        throw this.GetConvertFromException(value);
      TypeConverter converter1 = TypeDescriptor.GetConverter(typeof (int));
      return (object) new ColorAnimationStep((int) converter1.ConvertFrom(context, serializationCulture, (object) strArray[0]), (int) converter1.ConvertFrom(context, serializationCulture, (object) strArray[1]), (int) converter1.ConvertFrom(context, serializationCulture, (object) strArray[2]), (int) converter1.ConvertFrom(context, serializationCulture, (object) strArray[3]));
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return (object) destinationType == (object) typeof (string);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      ColorAnimationStep colorAnimationStep = (ColorAnimationStep) value;
      CultureInfo serializationCulture = AnimationValueCalculatorFactory.SerializationCulture;
      if (colorAnimationStep == null)
        return (object) null;
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
      string[] strArray = new string[4]
      {
        (string) converter.ConvertTo(context, serializationCulture, (object) colorAnimationStep.A, destinationType),
        " " + (string) converter.ConvertTo(context, serializationCulture, (object) colorAnimationStep.R, destinationType),
        " " + (string) converter.ConvertTo(context, serializationCulture, (object) colorAnimationStep.G, destinationType),
        " " + (string) converter.ConvertTo(context, serializationCulture, (object) colorAnimationStep.B, destinationType)
      };
      return (object) string.Join(serializationCulture.TextInfo.ListSeparator, strArray);
    }
  }
}
