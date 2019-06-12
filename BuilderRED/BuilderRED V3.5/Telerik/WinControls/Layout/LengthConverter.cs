// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layout.LengthConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace Telerik.WinControls.Layout
{
  public class LengthConverter : TypeConverter
  {
    private static float[] PixelUnitFactors = new float[4]{ 1f, 96f, 37.79528f, 1.333333f };
    private static string[] PixelUnitStrings = new string[4]{ "px", "in", "cm", "pt" };

    public override bool CanConvertFrom(
      ITypeDescriptorContext typeDescriptorContext,
      Type sourceType)
    {
      switch (Type.GetTypeCode(sourceType))
      {
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
          return true;
        default:
          return false;
      }
    }

    public override bool CanConvertTo(
      ITypeDescriptorContext typeDescriptorContext,
      Type destinationType)
    {
      return (object) destinationType == (object) typeof (InstanceDescriptor) || (object) destinationType == (object) typeof (string);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext typeDescriptorContext,
      CultureInfo cultureInfo,
      object source)
    {
      if (source == null)
        throw this.GetConvertFromException(source);
      if (source is string)
        return (object) LengthConverter.FromString((string) source, cultureInfo);
      return (object) Convert.ToDouble(source, (IFormatProvider) cultureInfo);
    }

    [SecurityCritical]
    public override object ConvertTo(
      ITypeDescriptorContext typeDescriptorContext,
      CultureInfo cultureInfo,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException(nameof (destinationType));
      if (value != null && value is float)
      {
        float f = (float) value;
        if ((object) destinationType == (object) typeof (string))
        {
          if (float.IsNaN(f))
            return (object) "Auto";
          return (object) Convert.ToString(f, (IFormatProvider) cultureInfo);
        }
        if ((object) destinationType == (object) typeof (InstanceDescriptor))
          return (object) new InstanceDescriptor((MemberInfo) typeof (float).GetConstructor(new Type[1]{ typeof (float) }), (ICollection) new object[1]{ (object) f });
      }
      throw this.GetConvertToException(value, destinationType);
    }

    internal static float FromString(string s, CultureInfo cultureInfo)
    {
      string str1 = s.Trim();
      string lowerInvariant = str1.ToLowerInvariant();
      int length = lowerInvariant.Length;
      int num1 = 0;
      float num2 = 1f;
      if (lowerInvariant == "auto")
        return float.NaN;
      for (int index = 0; index < LengthConverter.PixelUnitStrings.Length; ++index)
      {
        if (lowerInvariant.EndsWith(LengthConverter.PixelUnitStrings[index], StringComparison.Ordinal))
        {
          num1 = LengthConverter.PixelUnitStrings[index].Length;
          num2 = LengthConverter.PixelUnitFactors[index];
          break;
        }
      }
      string str2 = str1.Substring(0, length - num1);
      try
      {
        return Convert.ToSingle(str2, (IFormatProvider) cultureInfo) * num2;
      }
      catch (FormatException ex)
      {
        throw new FormatException(Telerik.WinControls.SR.GetString("LengthFormatError", (object) str2));
      }
    }

    internal static string ToString(float l, CultureInfo cultureInfo)
    {
      if (float.IsNaN(l))
        return "Auto";
      return Convert.ToString(l, (IFormatProvider) cultureInfo);
    }
  }
}
