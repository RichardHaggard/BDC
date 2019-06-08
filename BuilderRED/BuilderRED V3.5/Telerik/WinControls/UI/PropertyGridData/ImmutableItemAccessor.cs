// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridData.ImmutableItemAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.WinControls.UI.PropertyGridData
{
  public class ImmutableItemAccessor : DescriptorItemAccessor
  {
    public ImmutableItemAccessor(PropertyGridItem owner, PropertyDescriptor descriptor)
      : base(owner, descriptor)
    {
    }

    public override object Value
    {
      get
      {
        return base.Value;
      }
      set
      {
        object valueOwner = this.Owner.GetValueOwner();
        PropertyGridItem instanceParentGridEntry = this.InstanceParentGridEntry;
        TypeConverter typeConverter = instanceParentGridEntry.TypeConverter;
        PropertyDescriptorCollection properties = typeConverter.GetProperties((ITypeDescriptorContext) instanceParentGridEntry, valueOwner);
        IDictionary propertyValues = (IDictionary) new Hashtable(properties.Count);
        object obj = (object) null;
        for (int index = 0; index < properties.Count; ++index)
        {
          if (this.PropertyDescriptor.Name != null && this.PropertyDescriptor.Name.Equals(properties[index].Name))
            propertyValues[(object) properties[index].Name] = value;
          else
            propertyValues[(object) properties[index].Name] = properties[index].GetValue(valueOwner);
        }
        try
        {
          obj = typeConverter.CreateInstance((ITypeDescriptorContext) instanceParentGridEntry, propertyValues);
        }
        catch (Exception ex)
        {
          int num = (int) RadMessageBox.Show(ex.Message);
        }
        if (obj == null)
          return;
        instanceParentGridEntry.Value = obj;
      }
    }

    private PropertyGridItem InstanceParentGridEntry
    {
      get
      {
        return this.Owner.Parent as PropertyGridItem;
      }
    }
  }
}
