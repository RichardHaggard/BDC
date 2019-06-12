// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ImageTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Telerik.WinControls.Primitives
{
  public class ImageTypeConverter : TypeConverter
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
      return (object) TelerikHelper.ImageFromString((string) value);
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
      return (object) TelerikHelper.ImageToString((Image) value);
    }
  }
}
