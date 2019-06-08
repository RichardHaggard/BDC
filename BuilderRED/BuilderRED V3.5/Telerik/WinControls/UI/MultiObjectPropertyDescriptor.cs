// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiObjectPropertyDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Telerik.WinControls.UI
{
  public class MultiObjectPropertyDescriptor : PropertyDescriptor
  {
    private MultiObjectCollection objects;
    private Dictionary<Type, PropertyDescriptor> originalProperties;

    public MultiObjectPropertyDescriptor(
      MultiObjectCollection objects,
      Dictionary<Type, PropertyDescriptor> originalProperties)
      : base(originalProperties[objects[0].GetType()].Name, MultiObjectPropertyDescriptor.ConvertAttributeCollectionToArray(originalProperties[objects[0].GetType()].Attributes))
    {
      this.objects = objects;
      this.originalProperties = originalProperties;
    }

    private static Attribute[] ConvertAttributeCollectionToArray(
      AttributeCollection collection)
    {
      Attribute[] attributeArray = new Attribute[collection.Count];
      for (int index = 0; index < collection.Count; ++index)
        attributeArray[index] = collection[index];
      return attributeArray;
    }

    public override bool CanResetValue(object component)
    {
      return true;
    }

    public override Type ComponentType
    {
      get
      {
        return typeof (MultiObjectCollection);
      }
    }

    public override object GetValue(object component)
    {
      object[] objArray = new object[this.objects.Count];
      for (int index = 0; index < this.objects.Count; ++index)
      {
        try
        {
          objArray[index] = this.originalProperties[this.objects[index].GetType()].GetValue(this.objects[index]);
        }
        catch (TargetInvocationException ex)
        {
          PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.objects[index]).Find(this.originalProperties[this.objects[index].GetType()].Name, false);
          if (propertyDescriptor != null)
            objArray[index] = propertyDescriptor.GetValue(this.objects[index]);
        }
      }
      for (int index = 0; index < objArray.Length - 1; ++index)
      {
        if (objArray[index] == null || !objArray[index].Equals(objArray[index + 1]))
          return (object) null;
      }
      return objArray[0];
    }

    public override bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public override Type PropertyType
    {
      get
      {
        if (this.originalProperties.Count <= 0)
          return (Type) null;
        return this.originalProperties[this.objects[0].GetType()].PropertyType;
      }
    }

    public override void ResetValue(object component)
    {
      foreach (object component1 in (IEnumerable<object>) this.objects)
        this.originalProperties[component1.GetType()].ResetValue(component1);
    }

    public override void SetValue(object component, object value)
    {
      foreach (object component1 in (IEnumerable<object>) this.objects)
      {
        try
        {
          object obj = this.originalProperties[component1.GetType()].GetValue(component1);
          if (obj != null || value == null)
          {
            if (obj != null)
            {
              if (obj.Equals(value))
                continue;
            }
            else
              continue;
          }
          this.originalProperties[component1.GetType()].SetValue(component1, value);
        }
        catch (TargetInvocationException ex)
        {
          PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component1).Find(this.originalProperties[component1.GetType()].Name, false);
          if (propertyDescriptor != null)
          {
            object obj = propertyDescriptor.GetValue(component1);
            if (obj != null || value == null)
            {
              if (obj != null)
              {
                if (obj.Equals(value))
                  continue;
              }
              else
                continue;
            }
            propertyDescriptor.SetValue(component1, value);
          }
        }
      }
    }

    public override bool ShouldSerializeValue(object component)
    {
      return true;
    }
  }
}
