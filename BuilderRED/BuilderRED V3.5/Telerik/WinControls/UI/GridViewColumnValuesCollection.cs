// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewColumnValuesCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridViewColumnValuesCollection : IList, ICollection, IEnumerable
  {
    private GridViewColumnValuesCollection.Comparer comparer = new GridViewColumnValuesCollection.Comparer();
    private List<object> items;

    public GridViewColumnValuesCollection()
    {
      this.items = new List<object>();
    }

    public int Add(object value)
    {
      int num = this.items.BinarySearch(value, (IComparer<object>) this.comparer);
      if (num >= 0)
        return num;
      this.items.Insert(num * -1 - 1, value);
      return num * -1;
    }

    public void Clear()
    {
      this.items.Clear();
    }

    public bool Contains(object value)
    {
      return this.IndexOf(value) >= 0;
    }

    public int IndexOf(object value)
    {
      return this.items.BinarySearch(value, (IComparer<object>) this.comparer);
    }

    void IList.Insert(int index, object value)
    {
      this.Add(value);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public void Remove(object value)
    {
      int index = this.IndexOf(value);
      if (index < 0)
        return;
      this.items.RemoveAt(index);
    }

    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
    }

    public object this[int index]
    {
      get
      {
        return this.items[index];
      }
      set
      {
        if (this.items[index] == value)
          return;
        this.Remove(this.items[index]);
        this.Add(value);
      }
    }

    public void CopyTo(Array array, int index)
    {
      ((ICollection) this.items).CopyTo(array, index);
    }

    public int Count
    {
      get
      {
        return this.items.Count;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return ((ICollection) this.items).SyncRoot;
      }
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this.items.GetEnumerator();
    }

    private class Comparer : IComparer<object>
    {
      public int Compare(object x, object y)
      {
        if (x == y)
          return 0;
        if (x == null || x == DBNull.Value)
          return -1;
        if (y == null || y == DBNull.Value)
          return 1;
        if (x is Color)
          x = (object) ((Color) x).ToArgb();
        if (y is Color)
          y = (object) ((Color) y).ToArgb();
        IComparable comparable = x as IComparable;
        if (comparable == null)
          throw new ArgumentException("Argument_ImplementIComparable");
        return comparable.CompareTo(y);
      }
    }
  }
}
