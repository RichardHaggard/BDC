// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Text;
using System.Threading;
using Telerik.WinControls.Data;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI.PropertyGridData;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItem : PropertyGridItemBase, IDataItem, ITypeDescriptorContext, IServiceProvider
  {
    private int sortOrder = -1;
    private bool? isThreeState = new bool?();
    private PropertyGridItem parentItem;
    private PropertyGridItemCollection items;
    private object originalValue;
    private string errorMessage;
    private object dataBoundItem;
    private IItemAccessor itemAccessor;
    protected object cachedValue;

    public PropertyGridItem(PropertyGridTableElement propertyGridElement)
      : this(propertyGridElement, (PropertyGridItem) null)
    {
    }

    public PropertyGridItem(
      PropertyGridTableElement propertyGridElement,
      PropertyGridItem parentItem)
      : base(propertyGridElement)
    {
      this.parentItem = parentItem;
    }

    public override string Name
    {
      get
      {
        return this.itemAccessor.Name;
      }
    }

    public override string Label
    {
      get
      {
        if (!string.IsNullOrEmpty(base.Label))
          return base.Label;
        if (!string.IsNullOrEmpty(this.itemAccessor.DisplayName))
          return this.itemAccessor.DisplayName;
        return this.Name;
      }
      set
      {
        base.Label = value;
      }
    }

    public override string Description
    {
      get
      {
        if (base.Description != null)
          return base.Description;
        return this.itemAccessor.Description;
      }
      set
      {
        base.Description = value;
      }
    }

    public virtual string Category
    {
      get
      {
        return this.itemAccessor.Category;
      }
    }

    public virtual bool ReadOnly
    {
      get
      {
        return this.itemAccessor.ReadOnly;
      }
    }

    public virtual object Value
    {
      get
      {
        if (!this.PropertyGridTableElement.UseCachedValues)
          this.cachedValue = this.itemAccessor.Value;
        return this.cachedValue;
      }
      set
      {
        object convertedValue = (object) null;
        object obj1 = this.Value;
        if (!this.ConvertValue(value, out convertedValue) || object.Equals(obj1, convertedValue))
          return;
        if (this.PropertyGridTableElement != null)
        {
          PropertyGridItemValueChangingEventArgs e = new PropertyGridItemValueChangingEventArgs((PropertyGridItemBase) this, convertedValue, this.Value);
          this.PropertyGridTableElement.OnPropertyValueChanging(e);
          if (e.Cancel)
            return;
        }
        this.itemAccessor.Value = convertedValue;
        if (this.ShouldGetChildItemsAnew(obj1, this.Value))
          this.items = this.GetChildItems(this, this.Value, value != null ? value.GetType() : this.PropertyType);
        if ((object) this.Accessor.GetType() == (object) typeof (DescriptorItemAccessor))
        {
          if (!this.IsModified)
          {
            object obj2;
            if (this.TryGetDefaultValue(out obj2))
              obj1 = obj2;
            this.originalValue = obj1;
            if (this.Value == null && this.Value == this.originalValue && obj2 == null || this.Value != null && this.Value.Equals(obj2))
              this.state[16] = false;
            else
              this.state[16] = true;
          }
          else if (this.originalValue != null && this.originalValue.Equals(convertedValue) || this.originalValue == null && convertedValue == null)
            this.state[16] = false;
        }
        if (this.PropertyGridTableElement == null || this.itemAccessor is ImmutableItemAccessor)
          return;
        this.PropertyGridTableElement.Update(PropertyGridTableElement.UpdateActions.ValueChanged);
        this.PropertyGridTableElement.OnPropertyValueChanged(new PropertyGridItemValueChangedEventArgs((PropertyGridItemBase) this));
      }
    }

    public virtual string FormattedValue
    {
      get
      {
        object obj = this.Value;
        string empty = string.Empty;
        PasswordPropertyTextAttribute attribute = this.Attributes[typeof (PasswordPropertyTextAttribute)] as PasswordPropertyTextAttribute;
        try
        {
          if (obj is string && attribute != null && attribute.Password)
            return this.ConvertToPasswordString(obj.ToString());
          TypeConverter typeConverter = this.TypeConverter;
          if (typeConverter != null)
          {
            if (typeConverter.CanConvertTo((ITypeDescriptorContext) this, typeof (string)))
              empty = typeConverter.ConvertToString((ITypeDescriptorContext) this, Thread.CurrentThread.CurrentCulture, obj);
          }
        }
        catch
        {
        }
        if (attribute != null && attribute.Password)
          return this.ConvertToPasswordString(empty);
        return empty;
      }
      set
      {
        TypeConverter typeConverter = this.TypeConverter;
        if (typeConverter == null || !typeConverter.CanConvertFrom((ITypeDescriptorContext) this, typeof (string)))
          return;
        this.Value = typeConverter.ConvertFromString((ITypeDescriptorContext) this, Thread.CurrentThread.CurrentCulture, value);
      }
    }

    public virtual object OriginalValue
    {
      get
      {
        return this.originalValue;
      }
    }

    public virtual bool IsModified
    {
      get
      {
        object obj;
        if (this.TryGetDefaultValue(out obj))
        {
          if ((object) this.PropertyType == (object) typeof (Color) && this.Value != null && (((Color) this.Value).IsEmpty && obj == null))
            return false;
          if (this.Value != null && !this.Value.Equals(obj) && !this.FormattedValue.Equals(obj))
            return true;
          if (this.Value == null && obj == null || this.Value != null && this.Value.Equals(obj))
            return false;
        }
        if (this.Accessor is DescriptorItemAccessor)
          return this.state[16];
        PropertyGridItem parent = this.Parent as PropertyGridItem;
        if (parent != null)
          return parent.IsModified;
        return false;
      }
    }

    public override bool Expandable
    {
      get
      {
        return this.GridItems.Count > 0;
      }
    }

    [DefaultValue(false)]
    public bool DefaultSortOrder { get; set; }

    public virtual int SortOrder
    {
      get
      {
        if (this.sortOrder > -1 && !this.DefaultSortOrder || this.itemAccessor == null)
          return this.sortOrder;
        RadSortOrderAttribute attribute = this.Attributes[typeof (RadSortOrderAttribute)] as RadSortOrderAttribute;
        if (attribute != null)
          this.sortOrder = attribute.Value;
        this.DefaultSortOrder = false;
        return this.sortOrder;
      }
      set
      {
        this.DefaultSortOrder = false;
        if (this.sortOrder == value)
          return;
        this.sortOrder = value;
        this.OnNotifyPropertyChanged(nameof (SortOrder));
      }
    }

    public virtual bool IsThreeState
    {
      get
      {
        if (this.isThreeState.HasValue)
          return this.isThreeState.Value;
        if ((object) this.PropertyType == (object) typeof (ToggleState))
          this.isThreeState = new bool?(true);
        if (this.itemAccessor != null)
        {
          RadCheckBoxThreeStateAttribute attribute = this.Attributes[typeof (RadCheckBoxThreeStateAttribute)] as RadCheckBoxThreeStateAttribute;
          if (attribute != null)
          {
            this.isThreeState = new bool?(attribute.Value);
            return this.isThreeState.Value;
          }
        }
        if (this.isThreeState.HasValue)
          return this.isThreeState.Value;
        return false;
      }
      set
      {
        if (!this.isThreeState.HasValue || this.isThreeState.Value == value)
          return;
        this.isThreeState = new bool?(value);
        this.OnNotifyPropertyChanged(nameof (IsThreeState));
      }
    }

    public virtual AttributeCollection Attributes
    {
      get
      {
        return this.itemAccessor.Attributes;
      }
    }

    public override PropertyGridItemCollection GridItems
    {
      get
      {
        if (this.items == null || this.items.Count == 0 && (object) this.PropertyType == (object) typeof (StyleSheet))
        {
          object obj = this.Value;
          Type objType = obj != null ? obj.GetType() : this.PropertyType;
          this.items = this.GetChildItems(this, obj, objType);
        }
        return this.items;
      }
    }

    public override PropertyGridItemBase Parent
    {
      get
      {
        return (PropertyGridItemBase) this.parentItem;
      }
    }

    public virtual string ErrorMessage
    {
      get
      {
        return this.errorMessage;
      }
      set
      {
        if (!(this.errorMessage != value))
          return;
        this.errorMessage = value;
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
        this.OnNotifyPropertyChanged(nameof (ErrorMessage));
      }
    }

    public virtual UITypeEditor UITypeEditor
    {
      get
      {
        return (UITypeEditor) this.PropertyDescriptor.GetEditor(typeof (UITypeEditor));
      }
    }

    public virtual TypeConverter TypeConverter
    {
      get
      {
        return this.itemAccessor.TypeConverter;
      }
    }

    public virtual Type PropertyType
    {
      get
      {
        return this.itemAccessor.PropertyType;
      }
    }

    public virtual PropertyDescriptor PropertyDescriptor
    {
      get
      {
        return this.itemAccessor.PropertyDescriptor;
      }
    }

    public IItemAccessor Accessor
    {
      get
      {
        return this.itemAccessor;
      }
      internal set
      {
        this.itemAccessor = value;
      }
    }

    public virtual void ResetValue()
    {
      if (!this.IsModified || this.PropertyGridTableElement.ReadOnly || this.ReadOnly)
        return;
      object obj = (object) null;
      if (this.originalValue == null && this.TryGetDefaultValue(out obj))
        this.originalValue = obj;
      this.Value = this.originalValue;
      this.state[16] = false;
      if (this.originalValue == null || this.originalValue.Equals(obj))
        return;
      this.originalValue = (object) null;
    }

    public virtual void BeginEdit()
    {
      this.PropertyGridTableElement.SelectedGridItem = (PropertyGridItemBase) this;
      this.PropertyGridTableElement.BeginEdit();
    }

    internal virtual object GetValueOwner()
    {
      if (this.parentItem == null)
        return this.PropertyGridTableElement.SelectedObject;
      return this.parentItem.Value;
    }

    private bool ConvertValue(object value, out object convertedValue)
    {
      convertedValue = value;
      if (value == null)
        return true;
      Type type = value.GetType();
      Type propertyType = this.PropertyType;
      if (propertyType.IsAssignableFrom(type))
        return true;
      TypeConverter typeConverter1 = this.PropertyDescriptor.Converter ?? TypeDescriptor.GetConverter(type);
      if (typeConverter1 != null && typeConverter1.CanConvertTo((ITypeDescriptorContext) this, propertyType))
      {
        convertedValue = typeConverter1.ConvertTo((ITypeDescriptorContext) this, Thread.CurrentThread.CurrentCulture, value, propertyType);
        return true;
      }
      TypeConverter typeConverter2 = this.TypeConverter;
      if (typeConverter2 != null && typeConverter2.CanConvertFrom((ITypeDescriptorContext) this, type))
      {
        convertedValue = typeConverter2.ConvertFrom((ITypeDescriptorContext) this, Thread.CurrentThread.CurrentCulture, value);
        return true;
      }
      IConvertible convertible1 = value as IConvertible;
      if (convertible1 == null)
      {
        convertedValue = (object) null;
        return false;
      }
      try
      {
        ref object local = ref convertedValue;
        IConvertible convertible2 = convertible1;
        Type conversionType = Nullable.GetUnderlyingType(propertyType);
        if ((object) conversionType == null)
          conversionType = propertyType;
        object obj = Convert.ChangeType((object) convertible2, conversionType);
        local = obj;
      }
      catch (InvalidCastException ex)
      {
        convertedValue = (object) null;
        return false;
      }
      return true;
    }

    protected internal virtual void SetParent(PropertyGridItem parent)
    {
      this.parentItem = parent;
    }

    protected virtual PropertyGridItemCollection GetChildItems(
      PropertyGridItem parentItem,
      object obj,
      Type objType)
    {
      PropertyGridItemCollection gridItemCollection = new PropertyGridItemCollection((IList<PropertyGridItem>) new List<PropertyGridItem>());
      if (obj == null)
        return gridItemCollection;
      try
      {
        if (!this.TypeConverter.GetPropertiesSupported((ITypeDescriptorContext) this))
          return gridItemCollection;
        PropertyDescriptorCollection properties = this.TypeConverter.GetProperties((ITypeDescriptorContext) this, this.Value, new Attribute[1]{ (Attribute) new BrowsableAttribute(true) });
        if (properties == null)
          return gridItemCollection;
        if ((properties == null || properties.Count == 0) && ((object) objType != null && objType.IsArray) && obj != null)
        {
          Array array = (Array) obj;
          for (int index = 0; index < array.Length; ++index)
          {
            PropertyGridItem owner = this.PropertyGridTableElement.NewItem(parentItem) as PropertyGridItem;
            owner.Accessor = (IItemAccessor) new ArrayItemAccessor(owner, index);
            gridItemCollection.AddProperty(owner);
          }
          return gridItemCollection;
        }
        bool instanceSupported = this.TypeConverter.GetCreateInstanceSupported((ITypeDescriptorContext) this);
        int num = 0;
        foreach (PropertyDescriptor propertyDescriptor in properties)
        {
          try
          {
            object component = obj;
            if (obj is ICustomTypeDescriptor)
              component = ((ICustomTypeDescriptor) obj).GetPropertyOwner(propertyDescriptor);
            propertyDescriptor.GetValue(component);
          }
          catch (Exception ex)
          {
            continue;
          }
          PropertyGridItem owner = this.PropertyGridTableElement.NewItem(parentItem) as PropertyGridItem;
          owner.Accessor = !instanceSupported ? (IItemAccessor) new DescriptorItemAccessor(owner, propertyDescriptor) : (IItemAccessor) new ImmutableItemAccessor(owner, propertyDescriptor);
          owner.SortOrder = num++;
          gridItemCollection.AddProperty(owner);
        }
      }
      catch (Exception ex)
      {
      }
      return gridItemCollection;
    }

    protected virtual bool TryGetDefaultValue(out object value)
    {
      value = (object) null;
      DefaultValueAttribute attribute = this.Attributes[typeof (DefaultValueAttribute)] as DefaultValueAttribute;
      if (attribute == null)
        return false;
      value = attribute.Value;
      return true;
    }

    protected virtual bool ShouldGetChildItemsAnew(object oldValue, object newValue)
    {
      if (oldValue == null && newValue == null)
        return false;
      if (oldValue == null && newValue != null)
      {
        if (this.TypeConverter.GetCreateInstanceSupported((ITypeDescriptorContext) this) || this.TypeConverter.GetPropertiesSupported((ITypeDescriptorContext) this))
          return true;
        return (object) this.PropertyType != (object) newValue.GetType();
      }
      if (oldValue != null && newValue != null && (!(this.TypeConverter is ExpandableObjectConverter) && !this.TypeConverter.GetPropertiesSupported((ITypeDescriptorContext) this) || !this.TypeConverter.CanConvertFrom(typeof (string))))
        return (object) oldValue.GetType() != (object) newValue.GetType();
      return true;
    }

    protected virtual string ConvertToPasswordString(string input)
    {
      StringBuilder stringBuilder = new StringBuilder(input.Length);
      for (int index = 0; index < input.Length; ++index)
        stringBuilder.Append('*');
      return stringBuilder.ToString();
    }

    public object DataBoundItem
    {
      get
      {
        return this.dataBoundItem;
      }
      set
      {
        if (value == this.dataBoundItem)
          return;
        this.dataBoundItem = value;
        this.itemAccessor = (IItemAccessor) new DescriptorItemAccessor(this, this.dataBoundItem as PropertyDescriptor);
      }
    }

    public int FieldCount
    {
      get
      {
        return 8;
      }
    }

    public object this[string name]
    {
      get
      {
        PropertyInfo property = this.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
        if ((object) property == null)
          return (object) null;
        return property.GetValue((object) this, (object[]) null);
      }
      set
      {
      }
    }

    public object this[int index]
    {
      get
      {
        PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        if (properties != null && properties.Length > 0 && index < properties.Length)
          return properties[index].GetValue((object) this, (object[]) null);
        return (object) null;
      }
      set
      {
      }
    }

    public int IndexOf(string name)
    {
      PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
      if (properties != null && properties.Length > 0)
      {
        for (int index = 0; index < properties.Length; ++index)
        {
          if (properties[index].Name == name)
            return index;
        }
      }
      return -1;
    }

    public IContainer Container
    {
      get
      {
        if (this.PropertyGridTableElement.ElementTree == null)
          return (IContainer) null;
        IComponent control = (IComponent) this.PropertyGridTableElement.ElementTree.Control;
        if (control != null)
        {
          ISite site = control.Site;
          if (site != null)
            return site.Container;
        }
        return (IContainer) null;
      }
    }

    public object Instance
    {
      get
      {
        object valueOwner = this.GetValueOwner();
        if (this.parentItem != null && valueOwner == null)
          return this.parentItem.Instance;
        return valueOwner;
      }
    }

    public void OnComponentChanged()
    {
    }

    public bool OnComponentChanging()
    {
      return true;
    }

    public object GetService(Type serviceType)
    {
      if ((object) serviceType == (object) typeof (PropertyGridItem))
        return (object) this;
      if (this.parentItem != null)
        return this.parentItem.GetService(serviceType);
      return (object) null;
    }
  }
}
