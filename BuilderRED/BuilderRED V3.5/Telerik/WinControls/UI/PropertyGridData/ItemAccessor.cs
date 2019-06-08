// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridData.ItemAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.UI.PropertyGridData
{
  public abstract class ItemAccessor : IItemAccessor
  {
    protected UITypeEditor editor;
    protected TypeConverter converter;
    private PropertyGridItem owner;

    public ItemAccessor(PropertyGridItem owner)
    {
      this.owner = owner;
    }

    public virtual string Name
    {
      get
      {
        return "";
      }
    }

    public virtual string DisplayName
    {
      get
      {
        return "";
      }
    }

    public virtual object Value
    {
      get
      {
        return (object) null;
      }
      set
      {
      }
    }

    public virtual string Description
    {
      get
      {
        return (string) null;
      }
    }

    public virtual string Category
    {
      get
      {
        return CategoryAttribute.Default.Category;
      }
    }

    public virtual bool ReadOnly
    {
      get
      {
        return false;
      }
    }

    public virtual AttributeCollection Attributes
    {
      get
      {
        return (AttributeCollection) null;
      }
    }

    public virtual Type PropertyType
    {
      get
      {
        return this.Value?.GetType();
      }
    }

    public virtual PropertyDescriptor PropertyDescriptor
    {
      get
      {
        return (PropertyDescriptor) null;
      }
    }

    public virtual UITypeEditor UITypeEditor
    {
      get
      {
        if (this.editor == null && (object) this.PropertyType != null)
          this.editor = (UITypeEditor) TypeDescriptor.GetEditor(this.PropertyType, typeof (UITypeEditor));
        return this.editor;
      }
    }

    public virtual TypeConverter TypeConverter
    {
      get
      {
        if (this.converter == null)
        {
          object component = this.Value;
          this.converter = component != null ? TypeDescriptor.GetConverter(component) : TypeDescriptor.GetConverter(this.PropertyType);
        }
        return this.converter;
      }
    }

    public PropertyGridItem Owner
    {
      get
      {
        return this.owner;
      }
    }
  }
}
