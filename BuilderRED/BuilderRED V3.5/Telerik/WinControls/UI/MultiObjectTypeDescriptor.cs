// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiObjectTypeDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class MultiObjectTypeDescriptor : CustomTypeDescriptor
  {
    private MultiObjectCollection objects;

    public MultiObjectTypeDescriptor(MultiObjectCollection objects)
    {
      this.objects = objects;
    }

    public override PropertyDescriptorCollection GetProperties()
    {
      return this.GetProperties(new Attribute[0]);
    }

    public override PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      return new PropertyDescriptorCollection((PropertyDescriptor[]) this.GetCommonProperties(attributes).ToArray());
    }

    private List<MultiObjectPropertyDescriptor> GetCommonProperties(
      Attribute[] attributes)
    {
      Dictionary<Type, object> dictionary1 = new Dictionary<Type, object>();
      foreach (object obj in (IEnumerable<object>) this.objects)
      {
        Type type = obj.GetType();
        if (!dictionary1.ContainsKey(type))
          dictionary1.Add(type, obj);
      }
      Dictionary<Type, PropertyDescriptorCollection> dictionary2 = new Dictionary<Type, PropertyDescriptorCollection>();
      foreach (KeyValuePair<Type, object> keyValuePair in dictionary1)
      {
        Type key = keyValuePair.Key;
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(keyValuePair.Value, attributes, false);
        dictionary2.Add(key, properties);
      }
      Dictionary<PropertyDescriptor, int> dictionary3 = new Dictionary<PropertyDescriptor, int>();
      bool flag = true;
      foreach (Type key1 in dictionary1.Keys)
      {
        foreach (PropertyDescriptor key2 in dictionary2[key1])
        {
          if (dictionary3.ContainsKey(key2))
          {
            Dictionary<PropertyDescriptor, int> dictionary4;
            PropertyDescriptor index;
            (dictionary4 = dictionary3)[index = key2] = dictionary4[index] + 1;
          }
          else if (flag)
            dictionary3.Add(key2, 1);
        }
        flag = false;
      }
      List<MultiObjectPropertyDescriptor> propertyDescriptorList = new List<MultiObjectPropertyDescriptor>();
      foreach (KeyValuePair<PropertyDescriptor, int> keyValuePair in dictionary3)
      {
        if (keyValuePair.Value >= dictionary1.Count)
        {
          Dictionary<Type, PropertyDescriptor> originalProperties = new Dictionary<Type, PropertyDescriptor>();
          foreach (Type key in dictionary1.Keys)
            originalProperties.Add(key, dictionary2[key][keyValuePair.Key.Name]);
          MultiObjectPropertyDescriptor propertyDescriptor = new MultiObjectPropertyDescriptor(this.objects, originalProperties);
          propertyDescriptorList.Add(propertyDescriptor);
        }
      }
      return propertyDescriptorList;
    }
  }
}
