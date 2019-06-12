// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadImageShapeTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls
{
  public class RadImageShapeTypeConverter : ExpandableObjectConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if ((object) sourceType == (object) typeof (string))
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if ((object) destinationType == (object) typeof (string))
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (value is string)
        return (object) RadImageShape.Deserialize((string) value);
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == (object) typeof (string))
        return (object) RadImageShape.Serialize(value as RadImageShape);
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
