// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridData.DescriptorItemAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI.PropertyGridData
{
  public class DescriptorItemAccessor : ItemAccessor
  {
    private PropertyDescriptor descriptor;

    public DescriptorItemAccessor(PropertyGridItem owner, PropertyDescriptor descriptor)
      : base(owner)
    {
      this.descriptor = descriptor;
    }

    public override PropertyDescriptor PropertyDescriptor
    {
      get
      {
        return this.descriptor;
      }
    }

    public override string Name
    {
      get
      {
        return this.descriptor.Name;
      }
    }

    public override string DisplayName
    {
      get
      {
        return this.descriptor.DisplayName;
      }
    }

    public override string Description
    {
      get
      {
        return this.descriptor.Description;
      }
    }

    public override string Category
    {
      get
      {
        string category = this.descriptor.Category;
        if (category != null && category.Length != 0)
          return category;
        return base.Category;
      }
    }

    public override bool ReadOnly
    {
      get
      {
        return this.descriptor.IsReadOnly;
      }
    }

    public override AttributeCollection Attributes
    {
      get
      {
        return this.descriptor.Attributes;
      }
    }

    public override object Value
    {
      get
      {
        try
        {
          return this.GetPropertyValueCore(this.Owner.GetValueOwner());
        }
        catch (Exception ex)
        {
          return (object) ex;
        }
      }
      set
      {
        try
        {
          object valueOwner = this.Owner.GetValueOwner();
          object propertyValueCore = this.GetPropertyValueCore(valueOwner);
          if (value != null && value.Equals(propertyValueCore))
            return;
          this.SetPropertyValueCore(valueOwner, value);
        }
        catch (Exception ex)
        {
          int num = (int) RadMessageBox.Show(ex.Message);
        }
      }
    }

    public override Type PropertyType
    {
      get
      {
        return this.descriptor.PropertyType;
      }
    }

    public override UITypeEditor UITypeEditor
    {
      get
      {
        this.editor = (UITypeEditor) this.descriptor.GetEditor(typeof (UITypeEditor));
        return base.UITypeEditor;
      }
    }

    public override TypeConverter TypeConverter
    {
      get
      {
        try
        {
          if (this.converter == null)
            this.converter = this.descriptor.Converter;
        }
        catch
        {
        }
        return base.TypeConverter;
      }
    }

    protected virtual object GetPropertyValueCore(object target)
    {
      if (this.descriptor == null)
        return (object) null;
      if (target is ICustomTypeDescriptor)
        target = ((ICustomTypeDescriptor) target).GetPropertyOwner(this.descriptor);
      return this.descriptor.GetValue(target);
    }

    protected virtual void SetPropertyValueCore(object obj, object value)
    {
      if (this.descriptor == null)
        return;
      object component = obj;
      if (component is ICustomTypeDescriptor)
        component = ((ICustomTypeDescriptor) component).GetPropertyOwner(this.descriptor);
      bool flag = false;
      PropertyGridItem parent = this.Owner.Parent as PropertyGridItem;
      if (parent != null)
      {
        Type propertyType = parent.PropertyType;
        flag = propertyType.IsValueType || propertyType.IsArray;
      }
      if (component == null)
        return;
      this.descriptor.SetValue(component, value);
      if (!flag)
        return;
      parent.Value = obj;
    }
  }
}
