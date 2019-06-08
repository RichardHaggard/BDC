// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewSubDataItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
  public class ListViewSubDataItemCollection : IList, ICollection, IEnumerable
  {
    private List<object> values = new List<object>();
    private ListViewDataItem owner;

    public ListViewSubDataItemCollection(ListViewDataItem owner)
    {
      this.owner = owner;
    }

    public int Add(object value)
    {
      return ((IList) this.values).Add(value);
    }

    public void Clear()
    {
      this.values.Clear();
    }

    public bool Contains(object value)
    {
      return this.values.Contains(value);
    }

    public int IndexOf(object value)
    {
      return this.values.IndexOf(value);
    }

    public void Insert(int index, object value)
    {
      this.values.Insert(index, value);
    }

    public bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public void Remove(object value)
    {
      this.values.Remove(value);
    }

    public void RemoveAt(int index)
    {
      this.values.RemoveAt(index);
    }

    public object this[int index]
    {
      get
      {
        if (index >= 0 && index < this.values.Count)
          return this.values[index];
        return (object) string.Empty;
      }
      set
      {
        if (index < 0 || index >= this.values.Count)
          return;
        this.values[index] = value;
      }
    }

    public void CopyTo(Array array, int index)
    {
      ((ICollection) this.values).CopyTo(array, index);
    }

    public int Count
    {
      get
      {
        return this.values.Count;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return ((ICollection) this.values).IsSynchronized;
      }
    }

    public object SyncRoot
    {
      get
      {
        return ((ICollection) this.values).SyncRoot;
      }
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this.values.GetEnumerator();
    }
  }
}
