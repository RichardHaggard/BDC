// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyStoreItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class PropertyStoreItem : INotifyPropertyChanged
  {
    private Type propertyType;
    private string propertyName;
    private object value;
    private bool readOnly;
    private string category;
    private string label;
    private object originalValue;
    private string description;
    private List<Attribute> attributes;
    private RadPropertyStore owner;

    public PropertyStoreItem(Type propertyType, string propertyName, object value)
      : this(propertyType, propertyName, value, string.Empty, "Misc", false)
    {
    }

    public PropertyStoreItem(
      Type propertyType,
      string propertyName,
      object value,
      string description)
      : this(propertyType, propertyName, value, description, "Misc", false)
    {
    }

    public PropertyStoreItem(
      Type propertyType,
      string propertyName,
      object value,
      string description,
      string category)
      : this(propertyType, propertyName, value, description, category, false)
    {
    }

    public PropertyStoreItem(
      Type propertyType,
      string propertyName,
      object value,
      string description,
      string category,
      bool readOnly)
    {
      this.propertyType = propertyType;
      this.propertyName = propertyName;
      this.value = value;
      this.description = description;
      this.category = category;
      this.readOnly = readOnly;
      this.originalValue = value;
      this.attributes = new List<Attribute>();
    }

    public virtual Type PropertyType
    {
      get
      {
        return this.propertyType;
      }
      set
      {
        if ((object) this.propertyType == (object) value)
          return;
        this.propertyType = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (PropertyType)));
      }
    }

    public virtual string PropertyName
    {
      get
      {
        return this.propertyName;
      }
      set
      {
        if (!(this.propertyName != value))
          return;
        this.propertyName = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (PropertyName)));
      }
    }

    public virtual object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (this.value == value)
          return;
        TypeConverter converter = TypeDescriptor.GetConverter(this.PropertyType);
        if (value == null || this.PropertyType.IsAssignableFrom(value.GetType()))
        {
          this.value = value;
          this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Value)));
        }
        else if (converter.CanConvertFrom(value.GetType()))
        {
          this.value = converter.ConvertFrom(value);
          this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Value)));
        }
        else
        {
          object obj = converter.ConvertFromString(value.ToString());
          if (obj != null)
          {
            this.value = obj;
            this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Value)));
          }
        }
        if (this.owner == null)
          return;
        this.owner.OnItemValueChanged(this);
      }
    }

    public virtual string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        if (!(this.description != value))
          return;
        this.description = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Description)));
      }
    }

    public virtual bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        if (this.readOnly == value)
          return;
        this.readOnly = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ReadOnly)));
      }
    }

    public virtual string Category
    {
      get
      {
        if (string.IsNullOrEmpty(this.category))
          return "Misc";
        return this.category;
      }
      set
      {
        if (!(this.category != value))
          return;
        this.category = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Category)));
      }
    }

    public virtual string Label
    {
      get
      {
        return this.label;
      }
      set
      {
        if (!(this.label != value))
          return;
        this.label = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Label)));
      }
    }

    public virtual List<Attribute> Attributes
    {
      get
      {
        return this.attributes;
      }
      set
      {
        if (this.attributes == value)
          return;
        this.attributes = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Attributes)));
      }
    }

    public virtual RadPropertyStore Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        if (this.owner == value)
          return;
        this.owner = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Owner)));
      }
    }

    public virtual void ResetValue()
    {
      this.Value = this.originalValue;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs args)
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged((object) this, args);
      if (this.owner == null || !(args.PropertyName != "Owner"))
        return;
      this.owner.OnNotifyPropertyChanged(args);
    }
  }
}
