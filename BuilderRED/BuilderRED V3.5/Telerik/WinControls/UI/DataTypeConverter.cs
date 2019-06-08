// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls.UI
{
  public class DataTypeConverter : TypeConverter
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

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == (object) typeof (string))
      {
        Type type = value as Type;
        if ((object) type != null)
          return (object) type.FullName;
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      string str = value as string;
      if (str == null)
        return base.ConvertFrom(context, culture, value);
      Type type = Type.GetType(str);
      if ((object) type == null)
        type = RadTypeResolver.Instance.GetTypeByName(str, false, false);
      if ((object) type != null)
        return (object) type;
      throw new ArgumentException("The string you have entered cannot be converted to a valid Type object. Please, verify the spelling and include any namespaces that might be needed.");
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return false;
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      return new TypeConverter.StandardValuesCollection((ICollection) new List<Type>()
      {
        typeof (byte),
        typeof (sbyte),
        typeof (short),
        typeof (ushort),
        typeof (int),
        typeof (uint),
        typeof (long),
        typeof (ulong),
        typeof (float),
        typeof (double),
        typeof (char),
        typeof (bool),
        typeof (string),
        typeof (Decimal),
        typeof (DateTime)
      });
    }
  }
}
