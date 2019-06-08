// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Assistance.RadFilteredPropertiesConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Elements;

namespace Telerik.WinControls.Assistance
{
  internal class RadFilteredPropertiesConverter : ExpandableObjectConverter
  {
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, attributes);
      PropertyDescriptorCollection descriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
      foreach (PropertyDescriptor propertyDescriptor in properties)
      {
        if (propertyDescriptor.Name == "TipItems" && (object) propertyDescriptor.PropertyType == (object) typeof (RadItemReadOnlyCollection))
          descriptorCollection.Add(propertyDescriptor);
      }
      return descriptorCollection;
    }
  }
}
