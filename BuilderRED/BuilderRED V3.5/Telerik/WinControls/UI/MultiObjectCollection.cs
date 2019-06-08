// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiObjectCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [TypeDescriptionProvider(typeof (MultiObjectTypeDescriptionProvider))]
  public class MultiObjectCollection : IList<object>, ICollection<object>, IEnumerable<object>, IEnumerable
  {
    private ObservableCollection<object> objects;

    public MultiObjectCollection()
    {
      this.objects = new ObservableCollection<object>();
    }

    public MultiObjectCollection(object[] objects)
    {
      this.objects = new ObservableCollection<object>();
      foreach (object obj in objects)
        this.objects.Add(obj);
    }

    public int IndexOf(object item)
    {
      return this.objects.IndexOf(item);
    }

    public void Insert(int index, object item)
    {
      this.objects.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      this.objects.RemoveAt(index);
    }

    public object this[int index]
    {
      get
      {
        return this.objects[index];
      }
      set
      {
        this.objects[index] = value;
      }
    }

    public void Add(object item)
    {
      this.objects.Add(item);
    }

    public void Clear()
    {
      this.objects.Clear();
    }

    public bool Contains(object item)
    {
      return this.objects.Contains(item);
    }

    public void CopyTo(object[] array, int arrayIndex)
    {
      this.objects.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.objects.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool Remove(object item)
    {
      return this.objects.Remove(item);
    }

    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
      return this.objects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable) this.objects).GetEnumerator();
    }
  }
}
