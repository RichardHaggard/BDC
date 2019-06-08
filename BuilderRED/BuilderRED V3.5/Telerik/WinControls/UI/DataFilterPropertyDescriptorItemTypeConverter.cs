// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterPropertyDescriptorItemTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class DataFilterPropertyDescriptorItemTypeConverter : TypeConverter
  {
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      bool flag = value is DataFilterComboDescriptorItem;
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value))
      {
        if (property.Name == "DescriptorName" || property.Name == "DescriptorType")
          propertyDescriptorList.Add(property);
        if (flag && (property.Name == "DataSource" || property.Name == "DisplayMember" || property.Name == "ValueMember"))
          propertyDescriptorList.Add(property);
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray(), true);
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
