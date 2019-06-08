// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewChildRowCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewChildRowCollection : IReadOnlyCollection<GridViewRowInfo>, IEnumerable<GridViewRowInfo>, IList, ICollection, IEnumerable, ITraversable
  {
    public static GridViewChildRowCollection Empty = new GridViewChildRowCollection();
    protected IReadOnlyCollection<GridViewRowInfo> rows;

    public GridViewChildRowCollection()
    {
      this.rows = (IReadOnlyCollection<GridViewRowInfo>) new GridViewChildRowCollection.GridViewDataRowCollection((IList<GridViewRowInfo>) new List<GridViewRowInfo>());
    }

    public void Load(IReadOnlyCollection<GridViewRowInfo> rows)
    {
      this.rows = rows;
    }

    public void Load(IList<GridViewRowInfo> rows)
    {
      this.rows = (IReadOnlyCollection<GridViewRowInfo>) new GridViewChildRowCollection.GridViewDataRowCollection(rows);
    }

    public virtual bool Contains(GridViewRowInfo item)
    {
      return this.rows.Contains(item);
    }

    public void CopyTo(GridViewRowInfo[] array, int arrayIndex)
    {
      this.rows.CopyTo(array, arrayIndex);
    }

    public virtual int Count
    {
      get
      {
        return this.rows.Count;
      }
    }

    public virtual GridViewRowInfo this[int index]
    {
      get
      {
        return this.rows[index];
      }
    }

    public virtual int IndexOf(GridViewRowInfo item)
    {
      if (item == null)
        return -1;
      return this.rows.IndexOf(item);
    }

    public IEnumerator<GridViewRowInfo> GetEnumerator()
    {
      return this.rows.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.rows.GetEnumerator();
    }

    int IList.Add(object value)
    {
      return -1;
    }

    void IList.Clear()
    {
    }

    bool IList.Contains(object value)
    {
      GridViewRowInfo gridViewRowInfo = value as GridViewRowInfo;
      if (gridViewRowInfo == null)
        throw new ArgumentException("Invalid argument type");
      return this.Contains(gridViewRowInfo);
    }

    int IList.IndexOf(object value)
    {
      GridViewRowInfo gridViewRowInfo = value as GridViewRowInfo;
      if (gridViewRowInfo == null)
        throw new ArgumentException("Invalid argument type");
      return this.IndexOf(gridViewRowInfo);
    }

    void IList.Insert(int index, object value)
    {
    }

    public bool IsFixedSize
    {
      get
      {
        return true;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public void Remove(object value)
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
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
        return (object) this.rows;
      }
    }

    object ITraversable.this[int index]
    {
      get
      {
        return (object) this[index];
      }
    }

    private class GridViewDataRowCollection : IReadOnlyCollection<GridViewRowInfo>, IEnumerable<GridViewRowInfo>, IEnumerable
    {
      private IList<GridViewRowInfo> rows;

      public GridViewDataRowCollection(IList<GridViewRowInfo> rows)
      {
        this.rows = rows;
      }

      public int Count
      {
        get
        {
          return this.rows.Count;
        }
      }

      public GridViewRowInfo this[int index]
      {
        get
        {
          return this.rows[index];
        }
      }

      public bool Contains(GridViewRowInfo value)
      {
        return this.rows.Contains(value);
      }

      public void CopyTo(GridViewRowInfo[] array, int index)
      {
        this.rows.CopyTo(array, index);
      }

      public IEnumerator<GridViewRowInfo> GetEnumerator()
      {
        return this.rows.GetEnumerator();
      }

      public int IndexOf(GridViewRowInfo value)
      {
        return this.rows.IndexOf(value);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this.GetEnumerator();
      }
    }
  }
}
