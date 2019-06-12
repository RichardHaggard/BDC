// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttLinkDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class GanttLinkDescriptor
  {
    private LinkedList<PropertyDescriptor> startItemDescriptor;
    private LinkedList<PropertyDescriptor> endItemDescriptor;
    private LinkedList<PropertyDescriptor> linkTypeDescriptor;

    public GanttLinkDescriptor()
    {
      this.startItemDescriptor = new LinkedList<PropertyDescriptor>();
      this.endItemDescriptor = new LinkedList<PropertyDescriptor>();
      this.linkTypeDescriptor = new LinkedList<PropertyDescriptor>();
    }

    public GanttLinkDescriptor(
      PropertyDescriptor startItemDescriptor,
      PropertyDescriptor endItemDescriptor,
      PropertyDescriptor linkTypeDescriptor)
    {
      this.startItemDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        startItemDescriptor
      });
      this.endItemDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        endItemDescriptor
      });
      this.linkTypeDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        linkTypeDescriptor
      });
    }

    public PropertyDescriptor StartItemDescriptor
    {
      get
      {
        if (this.startItemDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.startItemDescriptor.First.Value;
      }
      set
      {
        if (this.startItemDescriptor.First == null)
          this.startItemDescriptor.AddFirst(value);
        else
          this.startItemDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor EndItemDescriptor
    {
      get
      {
        if (this.endItemDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.endItemDescriptor.First.Value;
      }
      set
      {
        if (this.endItemDescriptor.First == null)
          this.endItemDescriptor.AddFirst(value);
        else
          this.endItemDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor LinkTypeDescriptor
    {
      get
      {
        if (this.linkTypeDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.linkTypeDescriptor.First.Value;
      }
      set
      {
        if (this.linkTypeDescriptor.First == null)
          this.linkTypeDescriptor.AddFirst(value);
        else
          this.linkTypeDescriptor.First.Value = value;
      }
    }

    public void SetStartItemDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.startItemDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.startItemDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetEndItemDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.endItemDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.endItemDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetLinkTypeDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.linkTypeDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.linkTypeDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public object GetNestedValue(
      GanttViewLinkDataItem item,
      LinkedList<PropertyDescriptor> nestedDescriptor)
    {
      object dataBoundItem = ((IDataItem) item).DataBoundItem;
      for (LinkedListNode<PropertyDescriptor> linkedListNode = nestedDescriptor.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
        dataBoundItem = linkedListNode.Value.GetValue(dataBoundItem);
      return dataBoundItem;
    }

    public void SetNestedValue(
      GanttViewLinkDataItem item,
      LinkedList<PropertyDescriptor> nestedDescriptor,
      object value)
    {
      object dataBoundItem = ((IDataItem) item).DataBoundItem;
      LinkedListNode<PropertyDescriptor> first = nestedDescriptor.First;
      while (first != null)
      {
        if (first.Next == null)
        {
          object obj = value;
          TypeConverter converter = first.Value.Converter;
          if (value != null && converter.CanConvertFrom(value.GetType()))
            obj = converter.ConvertFrom(value);
          first.Value.SetValue(dataBoundItem, obj);
          break;
        }
        dataBoundItem = first.Value.GetValue(dataBoundItem);
      }
    }

    public object GetStartItem(GanttViewLinkDataItem item)
    {
      return this.GetNestedValue(item, this.startItemDescriptor);
    }

    public object GetEndItem(GanttViewLinkDataItem item)
    {
      return this.GetNestedValue(item, this.endItemDescriptor);
    }

    public object GetLinkType(GanttViewLinkDataItem item)
    {
      return this.GetNestedValue(item, this.linkTypeDescriptor);
    }

    public void SetStartItem(GanttViewLinkDataItem item, object value)
    {
      this.SetNestedValue(item, this.startItemDescriptor, value);
    }

    public void SetEndItem(GanttViewLinkDataItem item, object value)
    {
      this.SetNestedValue(item, this.endItemDescriptor, value);
    }

    public void SetLinkType(GanttViewLinkDataItem item, object value)
    {
      this.SetNestedValue(item, this.linkTypeDescriptor, value);
    }
  }
}
