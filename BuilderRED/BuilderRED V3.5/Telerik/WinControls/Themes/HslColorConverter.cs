// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Themes.HslColorConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.WinControls.Themes
{
  public class HslColorConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if ((object) sourceType != (object) typeof (string))
        return base.CanConvertFrom(context, sourceType);
      return true;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if ((object) destinationType != (object) typeof (InstanceDescriptor))
        return base.CanConvertTo(context, destinationType);
      return true;
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      string str1 = value as string;
      if (str1 == null)
        return base.ConvertFrom(context, culture, value);
      object obj = (object) null;
      string str2 = str1.Trim();
      if (str2.Length == 0)
        return (object) HslColor.Empty;
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      char ch = culture.TextInfo.ListSeparator[0];
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (double));
      string[] strArray = str2.Split(ch);
      double[] numArray = new double[strArray.Length];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (double) converter.ConvertFromString(context, culture, strArray[index]);
      switch (numArray.Length)
      {
        case 1:
          obj = (object) HslColor.FromAhsl((int) numArray[0]);
          break;
        case 3:
          obj = (object) HslColor.FromAhsl(numArray[0], numArray[1], numArray[2]);
          break;
        case 4:
          obj = (object) HslColor.FromAhsl((int) numArray[0], numArray[1], numArray[2], numArray[3]);
          break;
      }
      if (obj == null)
        throw new ArgumentException("Invalid HSL color string representation.");
      return obj;
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException(nameof (destinationType));
      if (value is HslColor)
      {
        if ((object) destinationType == (object) typeof (string))
        {
          HslColor hslColor = (HslColor) value;
          if (hslColor == HslColor.Empty)
            return (object) string.Empty;
          if (culture == null)
            culture = CultureInfo.CurrentCulture;
          string separator = culture.TextInfo.ListSeparator + " ";
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (double));
          int num1 = 0;
          string[] strArray1;
          if (hslColor.A < (int) byte.MaxValue)
          {
            strArray1 = new string[4];
            strArray1[num1++] = converter.ConvertToString(context, culture, (object) hslColor.A);
          }
          else
            strArray1 = new string[3];
          string[] strArray2 = strArray1;
          int index1 = num1;
          int num2 = index1 + 1;
          string str1 = converter.ConvertToString(context, culture, (object) hslColor.H);
          strArray2[index1] = str1;
          string[] strArray3 = strArray1;
          int index2 = num2;
          int num3 = index2 + 1;
          string str2 = converter.ConvertToString(context, culture, (object) hslColor.S);
          strArray3[index2] = str2;
          string[] strArray4 = strArray1;
          int index3 = num3;
          int num4 = index3 + 1;
          string str3 = converter.ConvertToString(context, culture, (object) hslColor.L);
          strArray4[index3] = str3;
          return (object) string.Join(separator, strArray1);
        }
        if ((object) destinationType == (object) typeof (InstanceDescriptor))
        {
          object[] objArray = (object[]) null;
          HslColor hslColor = (HslColor) value;
          MemberInfo member;
          if (hslColor.IsEmpty)
            member = (MemberInfo) typeof (HslColor).GetField("Empty");
          else if (hslColor.A != (int) byte.MaxValue)
          {
            member = (MemberInfo) typeof (HslColor).GetMethod("FromAhsl", new Type[4]
            {
              typeof (int),
              typeof (int),
              typeof (int),
              typeof (int)
            });
            objArray = new object[4]
            {
              (object) hslColor.A,
              (object) hslColor.H,
              (object) hslColor.S,
              (object) hslColor.L
            };
          }
          else
          {
            member = (MemberInfo) typeof (HslColor).GetMethod("FromAhsl", new Type[3]
            {
              typeof (int),
              typeof (int),
              typeof (int)
            });
            objArray = new object[3]
            {
              (object) hslColor.H,
              (object) hslColor.S,
              (object) hslColor.L
            };
          }
          return (object) new InstanceDescriptor(member, (ICollection) objArray);
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
