// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Themes.XmlSerialization.XmlPropertySettingValueConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.Themes.XmlSerialization
{
  public class XmlPropertySettingValueConverter : SerializationConverter
  {
    public override string ConvertToString(
      object propertyOwner,
      PropertyDescriptor property,
      object value)
    {
      if (propertyOwner is XmlPropertySetting)
      {
        string property1 = (propertyOwner as XmlPropertySetting).Property;
        if (string.IsNullOrEmpty(property1))
          return (string) null;
        RadProperty property2 = XmlPropertySetting.DeserializeProperty(property1);
        if (property2 != null)
          return XmlPropertySetting.SerializeValue(property2, value);
      }
      throw new NotSupportedException("Only XmlPropertySetting instances' value can be converted.");
    }

    public override object ConvertFromString(
      object propertyOwner,
      PropertyDescriptor property,
      string value)
    {
      if (propertyOwner is XmlPropertySetting)
      {
        string property1 = (propertyOwner as XmlPropertySetting).Property;
        if (string.IsNullOrEmpty(property1))
          return (object) null;
        RadProperty property2 = XmlPropertySetting.DeserializeProperty(property1);
        if (property2 != null)
          return XmlPropertySetting.DeserializeValue(property2, value);
      }
      throw new NotSupportedException("Only XmlPropertySetting instances' value can be converted.");
    }

    public override Type GetActualPropertyType(
      object propertyOwner,
      PropertyDescriptor property)
    {
      if (propertyOwner is XmlPropertySetting)
      {
        string property1 = (propertyOwner as XmlPropertySetting).Property;
        if (string.IsNullOrEmpty(property1))
          return (Type) null;
        RadProperty radProperty = XmlPropertySetting.DeserializeProperty(property1);
        if (radProperty != null && (object) radProperty.PropertyType != null)
          return radProperty.PropertyType;
      }
      throw new NotSupportedException("Only XmlPropertySetting instances' value can be converted.");
    }

    public override RadProperty GetRadProperty(
      object propertyOwner,
      PropertyDescriptor property)
    {
      if (propertyOwner is XmlPropertySetting)
      {
        string property1 = (propertyOwner as XmlPropertySetting).Property;
        if (string.IsNullOrEmpty(property1))
          return (RadProperty) null;
        RadProperty radProperty = XmlPropertySetting.DeserializeProperty(property1);
        if (radProperty != null)
          return radProperty;
      }
      throw new NotSupportedException("Only XmlPropertySetting instances' value can be converted.");
    }
  }
}
