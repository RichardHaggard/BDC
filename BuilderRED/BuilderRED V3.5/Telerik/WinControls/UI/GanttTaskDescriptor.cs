// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttTaskDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class GanttTaskDescriptor
  {
    private LinkedList<PropertyDescriptor> parentDescriptor;
    private LinkedList<PropertyDescriptor> childDescriptor;
    private LinkedList<PropertyDescriptor> titleDescriptor;
    private LinkedList<PropertyDescriptor> startDescriptor;
    private LinkedList<PropertyDescriptor> endDescriptor;
    private LinkedList<PropertyDescriptor> progressDescriptor;

    public GanttTaskDescriptor()
    {
      this.childDescriptor = new LinkedList<PropertyDescriptor>();
      this.parentDescriptor = new LinkedList<PropertyDescriptor>();
      this.titleDescriptor = new LinkedList<PropertyDescriptor>();
      this.startDescriptor = new LinkedList<PropertyDescriptor>();
      this.endDescriptor = new LinkedList<PropertyDescriptor>();
      this.progressDescriptor = new LinkedList<PropertyDescriptor>();
    }

    public GanttTaskDescriptor(
      PropertyDescriptor parentDescriptor,
      PropertyDescriptor childDescriptor,
      PropertyDescriptor titleDescriptor,
      PropertyDescriptor startDescriptor,
      PropertyDescriptor endDescriptor,
      PropertyDescriptor progressDescriptor)
    {
      this.childDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        childDescriptor
      });
      this.parentDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        parentDescriptor
      });
      this.titleDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        titleDescriptor
      });
      this.startDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        startDescriptor
      });
      this.endDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        endDescriptor
      });
      this.progressDescriptor = new LinkedList<PropertyDescriptor>((IEnumerable<PropertyDescriptor>) new PropertyDescriptor[1]
      {
        progressDescriptor
      });
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

    public PropertyDescriptor TitleDescriptor
    {
      get
      {
        if (this.titleDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.titleDescriptor.First.Value;
      }
      set
      {
        if (this.titleDescriptor.First == null)
          this.titleDescriptor.AddFirst(value);
        else
          this.titleDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor StartDescriptor
    {
      get
      {
        if (this.startDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.startDescriptor.First.Value;
      }
      set
      {
        if (this.startDescriptor.First == null)
          this.startDescriptor.AddFirst(value);
        else
          this.startDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor EndDescriptor
    {
      get
      {
        if (this.endDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.endDescriptor.First.Value;
      }
      set
      {
        if (this.endDescriptor.First == null)
          this.endDescriptor.AddFirst(value);
        else
          this.endDescriptor.First.Value = value;
      }
    }

    public PropertyDescriptor ProgressDescriptor
    {
      get
      {
        if (this.progressDescriptor.First == null)
          return (PropertyDescriptor) null;
        return this.progressDescriptor.First.Value;
      }
      set
      {
        if (this.progressDescriptor.First == null)
          this.progressDescriptor.AddFirst(value);
        else
          this.progressDescriptor.First.Value = value;
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

    public void SetTitleDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.titleDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.titleDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetStartDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.startDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.startDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetEndDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.endDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.endDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public void SetProgressDescriptor(PropertyDescriptor descriptor, string path)
    {
      string[] strArray = path.Split('.');
      this.progressDescriptor.Clear();
      int index = 0;
      for (; descriptor != null; descriptor = index >= strArray.Length ? (PropertyDescriptor) null : descriptor.GetChildProperties().Find(strArray[index], true))
      {
        this.progressDescriptor.AddLast(descriptor);
        ++index;
      }
    }

    public object GetNestedValue(
      GanttViewDataItem item,
      LinkedList<PropertyDescriptor> nestedDescriptor)
    {
      object dataBoundItem = ((IDataItem) item).DataBoundItem;
      for (LinkedListNode<PropertyDescriptor> linkedListNode = nestedDescriptor.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
        dataBoundItem = linkedListNode.Value.GetValue(dataBoundItem);
      return dataBoundItem;
    }

    public void SetNestedValue(
      GanttViewDataItem item,
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

    public object GetChild(GanttViewDataItem item)
    {
      return this.GetNestedValue(item, this.childDescriptor);
    }

    public object GetParent(GanttViewDataItem item)
    {
      return this.GetNestedValue(item, this.parentDescriptor);
    }

    public object GetTitle(GanttViewDataItem item)
    {
      return this.GetNestedValue(item, this.titleDescriptor);
    }

    public object GetStart(GanttViewDataItem item)
    {
      return this.GetNestedValue(item, this.startDescriptor);
    }

    public object GetEnd(GanttViewDataItem item)
    {
      return this.GetNestedValue(item, this.endDescriptor);
    }

    public object GetProgress(GanttViewDataItem item)
    {
      return this.GetNestedValue(item, this.progressDescriptor);
    }

    public void SetChild(GanttViewDataItem item, object value)
    {
      this.SetNestedValue(item, this.childDescriptor, value);
    }

    public void SetParent(GanttViewDataItem item, object value)
    {
      this.SetNestedValue(item, this.parentDescriptor, value);
    }

    public void SetTitle(GanttViewDataItem item, object value)
    {
      this.SetNestedValue(item, this.titleDescriptor, value);
    }

    public void SetStart(GanttViewDataItem item, object value)
    {
      this.SetNestedValue(item, this.startDescriptor, value);
    }

    public void SetEnd(GanttViewDataItem item, object value)
    {
      this.SetNestedValue(item, this.endDescriptor, value);
    }

    public void SetProgress(GanttViewDataItem item, object value)
    {
      this.SetNestedValue(item, this.progressDescriptor, value);
    }
  }
}
