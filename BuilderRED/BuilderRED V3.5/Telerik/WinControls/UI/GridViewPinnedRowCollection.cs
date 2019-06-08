// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewPinnedRowCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewPinnedRowCollection : IReadOnlyCollection<GridViewRowInfo>, IEnumerable<GridViewRowInfo>, IEnumerable, ITraversable
  {
    private List<GridViewRowInfo> list = new List<GridViewRowInfo>();
    private List<GridViewRowInfo> sortedList = new List<GridViewRowInfo>();
    private GridViewInfo viewInfo;

    public GridViewPinnedRowCollection(GridViewInfo viewInfo)
    {
      this.viewInfo = viewInfo;
    }

    public int Count
    {
      get
      {
        return this.sortedList.Count;
      }
    }

    public GridViewRowInfo this[int index]
    {
      get
      {
        return this.sortedList[index];
      }
    }

    public bool Contains(GridViewRowInfo item)
    {
      return this.sortedList.Contains(item);
    }

    public void CopyTo(GridViewRowInfo[] array, int index)
    {
      this.sortedList.CopyTo(array, index);
    }

    public int IndexOf(GridViewRowInfo item)
    {
      return this.sortedList.IndexOf(item);
    }

    public IEnumerator<GridViewRowInfo> GetEnumerator()
    {
      return (IEnumerator<GridViewRowInfo>) new GridViewPinnedRowCollection.GridViewPinnedRowsCollectionEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new GridViewPinnedRowCollection.GridViewPinnedRowsCollectionEnumerator(this);
    }

    internal void Sort()
    {
      if (this.viewInfo == null || this.viewInfo.ViewTemplate == null)
        return;
      GridViewRowInfoComparer sortComparer = this.viewInfo.ViewTemplate.SortComparer as GridViewRowInfoComparer;
      if (sortComparer == null || sortComparer.Context.Count == 0)
        this.sortedList = new List<GridViewRowInfo>((IEnumerable<GridViewRowInfo>) this.list);
      else
        this.sortedList.Sort((IComparer<GridViewRowInfo>) sortComparer);
    }

    internal void UpdateRow(GridViewRowInfo row)
    {
      if (!(row is GridViewDataRowInfo))
        return;
      if (row.PinPosition == PinnedRowPosition.None)
      {
        this.Remove(row);
      }
      else
      {
        if (this.Contains(row))
          return;
        this.Add(row);
      }
    }

    internal void Add(GridViewRowInfo row)
    {
      this.list.Add(row);
      this.sortedList.Add(row);
      this.Sort();
    }

    internal void Remove(GridViewRowInfo rowInfo)
    {
      this.list.Remove(rowInfo);
      this.sortedList.Remove(rowInfo);
    }

    internal void Clear()
    {
      this.list.Clear();
      this.sortedList.Clear();
    }

    object ITraversable.this[int index]
    {
      get
      {
        return (object) this[index];
      }
    }

    public class GridViewPinnedRowsCollectionEnumerator : IEnumerator<GridViewRowInfo>, IDisposable, IEnumerator
    {
      private int position = -1;
      private GridViewPinnedRowCollection collection;

      public GridViewPinnedRowsCollectionEnumerator(GridViewPinnedRowCollection collection)
      {
        this.collection = collection;
      }

      public GridViewRowInfo Current
      {
        get
        {
          return this.collection[this.position];
        }
      }

      public void Dispose()
      {
        this.collection = (GridViewPinnedRowCollection) null;
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.collection[this.position];
        }
      }

      public bool MoveNext()
      {
        if (this.position >= this.collection.Count - 1)
          return false;
        ++this.position;
        return true;
      }

      public void Reset()
      {
        this.position = -1;
      }
    }
  }
}
