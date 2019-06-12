// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyStoreTypeDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class PropertyStoreTypeDescriptor : CustomTypeDescriptor
  {
    private RadPropertyStore store;

    public PropertyStoreTypeDescriptor(RadPropertyStore store)
    {
      this.store = store;
    }

    public override PropertyDescriptorCollection GetProperties()
    {
      List<PropertyStorePropertyDescriptor> propertyDescriptorList = new List<PropertyStorePropertyDescriptor>();
      foreach (PropertyStoreItem propertyStoreItem in this.store)
        propertyDescriptorList.Add(new PropertyStorePropertyDescriptor(propertyStoreItem));
      return new PropertyDescriptorCollection((PropertyDescriptor[]) propertyDescriptorList.ToArray(), true);
    }

    public override PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      return this.GetProperties();
    }
  }
}
