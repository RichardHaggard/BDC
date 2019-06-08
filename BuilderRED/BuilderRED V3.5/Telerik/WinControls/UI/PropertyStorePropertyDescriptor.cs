// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyStorePropertyDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class PropertyStorePropertyDescriptor : PropertyDescriptor
  {
    private PropertyStoreItem item;

    public PropertyStorePropertyDescriptor(PropertyStoreItem item)
      : base(item.PropertyName, PropertyStorePropertyDescriptor.CreateAttributesArray(item))
    {
      this.item = item;
    }

    private static Attribute[] CreateAttributesArray(PropertyStoreItem item)
    {
      List<Attribute> attributeList = new List<Attribute>();
      foreach (Attribute attribute in item.Attributes)
        attributeList.Add(attribute);
      attributeList.Add((Attribute) new BrowsableAttribute(true));
      attributeList.Add((Attribute) new CategoryAttribute(item.Category));
      attributeList.Add((Attribute) new ReadOnlyAttribute(item.ReadOnly));
      attributeList.Add((Attribute) new DescriptionAttribute(item.Description));
      if (!string.IsNullOrEmpty(item.Label))
        attributeList.Add((Attribute) new DisplayNameAttribute(item.Label));
      return attributeList.ToArray();
    }

    public override bool CanResetValue(object component)
    {
      return true;
    }

    public override void ResetValue(object component)
    {
      this.item.ResetValue();
    }

    public override Type ComponentType
    {
      get
      {
        return typeof (RadPropertyStore);
      }
    }

    public override object GetValue(object component)
    {
      return this.item.Value;
    }

    public override bool IsReadOnly
    {
      get
      {
        return this.item.ReadOnly;
      }
    }

    public override Type PropertyType
    {
      get
      {
        return this.item.PropertyType;
      }
    }

    public override void SetValue(object component, object value)
    {
      this.item.Value = value;
    }

    public override bool ShouldSerializeValue(object component)
    {
      return true;
    }
  }
}
