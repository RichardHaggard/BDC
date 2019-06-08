// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FormatInfoSerializationCoverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  internal class FormatInfoSerializationCoverter : SerializationConverter
  {
    public override Type GetActualPropertyType(
      object propertyOwner,
      PropertyDescriptor property)
    {
      return typeof (FormatInfoSerializationCoverter.FormatInfo);
    }

    public override RadProperty GetRadProperty(
      object propertyOwner,
      PropertyDescriptor property)
    {
      return (RadProperty) null;
    }

    public override object ConvertFromString(
      object propertyOwner,
      PropertyDescriptor property,
      string value)
    {
      return (object) null;
    }

    public override string ConvertToString(
      object propertyOwner,
      PropertyDescriptor property,
      object value)
    {
      return (string) null;
    }

    public class FortmatInfoConverter : CultureInfoConverter
    {
      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
      {
        if (value == null && (object) destinationType == (object) typeof (string))
          return (object) null;
        return base.ConvertTo(context, culture, value, destinationType);
      }

      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        if (value == null)
          return (object) null;
        return base.ConvertFrom(context, culture, value);
      }
    }

    [TypeConverter(typeof (FormatInfoSerializationCoverter.FortmatInfoConverter))]
    public class FormatInfo : CultureInfo
    {
      public FormatInfo(int culture)
        : base(culture)
      {
      }

      public FormatInfo(string name)
        : base(name)
      {
      }

      public FormatInfo(int culture, bool userOverride)
        : base(culture, userOverride)
      {
      }

      public FormatInfo(string name, bool userOverride)
        : base(name, userOverride)
      {
      }
    }
  }
}
