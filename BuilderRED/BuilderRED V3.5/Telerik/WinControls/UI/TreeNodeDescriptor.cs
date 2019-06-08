// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class TreeNodeDescriptor
  {
    private LinkedList<PropertyDescriptor> parentDescriptor;
    private LinkedList<PropertyDescriptor> valueDescriptor;
    private LinkedList<PropertyDescriptor> displayDescriptor;
    private LinkedList<PropertyDescriptor> childDescriptor;
    private LinkedList<PropertyDescriptor> checkedDescriptor;
    private TypeConverter toggleStateConverter;

    public TreeNodeDescriptor()
    {
      this.childDescriptor = new LinkedList<PropertyDescriptor>();
      this.parentDescriptor = new LinkedList<PropertyDescriptor>();
      this.valueDescriptor = new LinkedList<PropertyDescriptor>();
      this.displayDescriptor = new LinkedList<PropertyDescriptor>();
      this.checkedDescriptor = new LinkedList<PropertyDescriptor>();
      this.toggleStateConverter = (TypeConverter) new TreeNodeDescriptor.ToggleStateTypeConverter();
    }

    public TreeNodeDescriptor(
      PropertyDescriptor parentDescriptor,
      PropertyDescriptor childDescriptor,
      PropertyDescriptor valueDescriptor,
      PropertyDescriptor displayDescriptor,
      PropertyDescriptor checkedDescriptor)
    {
      this.childDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        childDescriptor
      });
      this.parentDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        parentDescriptor
      });
      this.valueDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        valueDescriptor
      });
      this.displayDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        displayDescriptor
      });
      this.checkedDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        checkedDescriptor
      });
    }

    public TreeNodeDescriptor(
      PropertyDescriptorCollection descriptors,
      string parentPath,
      string childPath,
      string valuePath,
      string displayPath,
      string checkPath)
    {
    }

    public void SetDisplayDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.displayDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.displayDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetChildDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.childDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.childDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetValueDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.valueDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.valueDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetCheckedDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.checkedDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.checkedDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetParentDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.parentDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.parentDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public PropertyDescriptor ParentDescriptor
    {
      get
      {
        if (this.parentDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.parentDescriptor.First.Value;
      }
      set
      {
        if (this.parentDescriptor.First == null)
          this.parentDescriptor.AddFirst(value);
        else
          this.parentDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor ValueDescriptor
    {
      get
      {
        if (this.valueDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.valueDescriptor.First.Value;
      }
      set
      {
        if (this.valueDescriptor.First == null)
          this.valueDescriptor.AddFirst(value);
        else
          this.valueDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor CheckedDescriptor
    {
      get
      {
        if (this.checkedDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.checkedDescriptor.First.Value;
      }
      set
      {
        if (this.checkedDescriptor.First == null)
          this.checkedDescriptor.AddFirst(value);
        else
          this.checkedDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor DisplayDescriptor
    {
      get
      {
        if (this.displayDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.displayDescriptor.First.Value;
      }
      set
      {
        if (this.displayDescriptor.First == null)
          this.displayDescriptor.AddFirst(value);
        else
          this.displayDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor ChildDescriptor
    {
      get
      {
        if (this.childDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.childDescriptor.First.Value;
      }
      set
      {
        if (this.childDescriptor.First == null)
          this.childDescriptor.AddFirst(value);
        else
          this.childDescriptor.First.Value = value;
      }
    }

    public TypeConverter Converter
    {
      get
      {
        return this.toggleStateConverter;
      }
      set
      {
        this.toggleStateConverter = value;
      }
    }

    public object GetNestedValue(RadTreeNode node, LinkedList<PropertyDescriptor> nestedDescriptor)
    {
      object dataBoundItem = node.DataBoundItem;
      LinkedListNode<PropertyDescriptor> linkedListNode = nestedDescriptor.First;
      try
      {
        for (; linkedListNode != null; linkedListNode = linkedListNode.Next)
          dataBoundItem = linkedListNode.Value.GetValue(dataBoundItem);
        return dataBoundItem;
      }
      catch
      {
        return (object) null;
      }
    }

    public void SetNestedValue(
      RadTreeNode node,
      LinkedList<PropertyDescriptor> nestedDescriptor,
      object value)
    {
      object dataBoundItem = node.DataBoundItem;
      LinkedListNode<PropertyDescriptor> first = nestedDescriptor.First;
      while (first != null)
      {
        if (first.Next == null)
        {
          object obj = value;
          if (this.Converter != null && first.Value == this.CheckedDescriptor && this.Converter.CanConvertTo(first.Value.PropertyType))
          {
            obj = this.Converter.ConvertTo(value, first.Value.PropertyType);
          }
          else
          {
            TypeConverter converter1 = first.Value.Converter;
            if (value != null && converter1.CanConvertFrom(value.GetType()))
              obj = converter1.ConvertFrom(value);
            else if (value != null)
            {
              TypeConverter converter2 = TypeDescriptor.GetConverter(value);
              if (converter2.CanConvertTo(first.Value.PropertyType))
                obj = converter2.ConvertTo(value, first.Value.PropertyType);
            }
          }
          first.Value.SetValue(dataBoundItem, obj);
          break;
        }
        dataBoundItem = first.Value.GetValue(dataBoundItem);
      }
    }

    public object GetParent(RadTreeNode node)
    {
      return this.GetNestedValue(node, this.parentDescriptor);
    }

    public void SetParent(RadTreeNode node, object value)
    {
      this.SetNestedValue(node, this.parentDescriptor, value);
    }

    public object GetValue(RadTreeNode node)
    {
      return this.GetNestedValue(node, this.valueDescriptor);
    }

    public void SetValue(RadTreeNode node, object value)
    {
      this.SetNestedValue(node, this.valueDescriptor, value);
    }

    public object GetChecked(RadTreeNode node)
    {
      return this.GetNestedValue(node, this.checkedDescriptor);
    }

    public void SetChecked(RadTreeNode node, object value)
    {
      this.SetNestedValue(node, this.checkedDescriptor, value);
    }

    public object GetDisplay(RadTreeNode node)
    {
      return this.GetNestedValue(node, this.displayDescriptor);
    }

    public void SetDisplay(RadTreeNode node, object value)
    {
      this.SetNestedValue(node, this.displayDescriptor, value);
    }

    public object GetChild(RadTreeNode node)
    {
      return this.GetNestedValue(node, this.childDescriptor);
    }

    public void SetChild(RadTreeNode node, object value)
    {
      this.SetNestedValue(node, this.childDescriptor, value);
    }

    internal class ToggleStateTypeConverter : TypeConverter
    {
      private List<System.Type> supportedTypes;

      public ToggleStateTypeConverter()
      {
        this.supportedTypes = new List<System.Type>()
        {
          typeof (Telerik.WinControls.Enumerations.ToggleState),
          typeof (CheckState),
          typeof (DBNull),
          typeof (bool),
          typeof (bool?),
          typeof (int)
        };
      }

      public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
      {
        if (this.supportedTypes.Contains(sourceType))
          return true;
        return base.CanConvertFrom(context, sourceType);
      }

      public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
      {
        if (this.supportedTypes.Contains(destinationType))
          return true;
        return base.CanConvertTo(context, destinationType);
      }

      public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
      {
        if (value is Telerik.WinControls.Enumerations.ToggleState)
          return (object) (Telerik.WinControls.Enumerations.ToggleState) value;
        if (value is CheckState)
        {
          switch ((CheckState) value)
          {
            case CheckState.Unchecked:
              return (object) Telerik.WinControls.Enumerations.ToggleState.Off;
            case CheckState.Checked:
              return (object) Telerik.WinControls.Enumerations.ToggleState.On;
            case CheckState.Indeterminate:
              return (object) Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
          }
        }
        else
        {
          if (value is bool?)
          {
            bool? nullable = (bool?) value;
            if (nullable.HasValue)
              return (object) (Telerik.WinControls.Enumerations.ToggleState) (nullable.Value ? 1 : 0);
            return (object) Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
          }
          if (value is bool)
            return (object) (Telerik.WinControls.Enumerations.ToggleState) ((bool) value ? 1 : 0);
          if (value is int)
          {
            switch ((int) value)
            {
              case 0:
                return (object) Telerik.WinControls.Enumerations.ToggleState.Off;
              case 1:
                return (object) Telerik.WinControls.Enumerations.ToggleState.On;
              default:
                return (object) Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
            }
          }
          else if (value is DBNull || value == null)
            return (object) Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
        }
        return base.ConvertFrom(context, culture, value);
      }

      public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        System.Type destinationType)
      {
        Telerik.WinControls.Enumerations.ToggleState toggleState = (Telerik.WinControls.Enumerations.ToggleState) value;
        if ((object) destinationType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState))
          return (object) toggleState;
        if ((object) destinationType == (object) typeof (CheckState))
        {
          switch (toggleState)
          {
            case Telerik.WinControls.Enumerations.ToggleState.Off:
              return (object) CheckState.Unchecked;
            case Telerik.WinControls.Enumerations.ToggleState.On:
              return (object) CheckState.Checked;
            case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
              return (object) CheckState.Indeterminate;
          }
        }
        else if ((object) destinationType == (object) typeof (bool?))
        {
          switch (toggleState)
          {
            case Telerik.WinControls.Enumerations.ToggleState.Off:
              return (object) false;
            case Telerik.WinControls.Enumerations.ToggleState.On:
              return (object) true;
            case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
              return (object) null;
          }
        }
        else if ((object) destinationType == (object) typeof (bool))
        {
          switch (toggleState)
          {
            case Telerik.WinControls.Enumerations.ToggleState.Off:
              return (object) false;
            case Telerik.WinControls.Enumerations.ToggleState.On:
              return (object) true;
            case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
              return (object) DBNull.Value;
          }
        }
        else if ((object) destinationType == (object) typeof (int))
        {
          switch (toggleState)
          {
            case Telerik.WinControls.Enumerations.ToggleState.Off:
              return (object) 0;
            case Telerik.WinControls.Enumerations.ToggleState.On:
              return (object) 1;
            case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
              return (object) 2;
          }
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }
    }
  }
}
