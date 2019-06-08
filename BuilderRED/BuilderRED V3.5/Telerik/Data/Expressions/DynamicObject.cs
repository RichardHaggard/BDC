// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.DynamicObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Data.Expressions
{
  public abstract class DynamicObject : CustomTypeDescriptor, IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
    {
      return this.GetEnumeratorInternal();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumeratorInternal();
    }

    protected abstract IEnumerator<KeyValuePair<string, object>> GetEnumeratorInternal();

    protected abstract object GetValue(string name);

    protected abstract void SetValue(string name, object value);

    public override PropertyDescriptorCollection GetProperties()
    {
      return this.GetPropertiesInternal(new Attribute[0]);
    }

    public override PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      return this.GetPropertiesInternal(attributes);
    }

    public override object GetPropertyOwner(PropertyDescriptor pd)
    {
      return (object) this;
    }

    private PropertyDescriptorCollection GetPropertiesInternal(
      Attribute[] attributes)
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) this)
      {
        Type type = (Type) null;
        if (keyValuePair.Value != null)
          type = keyValuePair.Value.GetType();
        propertyDescriptorList.Add((PropertyDescriptor) new DynamicObject.DynamicProperty(keyValuePair.Key, type, (Attribute[]) null));
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray(), true);
    }

    private class DynamicProperty : PropertyDescriptor
    {
      private Type type;

      public DynamicProperty(string name, Type type, Attribute[] attr)
        : base(name, attr)
      {
        this.type = type;
      }

      public override bool CanResetValue(object component)
      {
        return false;
      }

      public override object GetValue(object component)
      {
        return ((DynamicObject) component).GetValue(this.Name);
      }

      public override void ResetValue(object component)
      {
        ((DynamicObject) component).SetValue(this.Name, (object) null);
      }

      public override void SetValue(object component, object value)
      {
        ((DynamicObject) component).SetValue(this.Name, value);
      }

      public override bool ShouldSerializeValue(object component)
      {
        return false;
      }

      public override Type ComponentType
      {
        get
        {
          return typeof (ExpressionContext);
        }
      }

      public override bool IsReadOnly
      {
        get
        {
          return true;
        }
      }

      public override Type PropertyType
      {
        get
        {
          return this.type;
        }
      }
    }
  }
}
