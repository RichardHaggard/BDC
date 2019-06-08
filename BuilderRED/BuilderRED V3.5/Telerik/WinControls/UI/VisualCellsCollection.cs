// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualCellsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class VisualCellsCollection : IList<GridCellElement>, ICollection<GridCellElement>, IEnumerable<GridCellElement>, IEnumerable
  {
    private GridRowElement row;

    public VisualCellsCollection(GridRowElement row)
    {
      this.row = row;
    }

    public int IndexOf(GridCellElement item)
    {
      if (this.row == null)
        return -1;
      if (!(this.row is GridVirtualizedRowElement))
        return this.row.Children.IndexOf((RadElement) item);
      GridVirtualizedRowElement row = (GridVirtualizedRowElement) this.row;
      if (item.ColumnInfo.PinPosition == PinnedColumnPosition.Left)
      {
        for (int index = 0; index < row.LeftPinnedColumns.Children.Count; ++index)
        {
          if (row.LeftPinnedColumns.Children[index] == item)
            return index;
        }
        return -1;
      }
      if (item.ColumnInfo.PinPosition == PinnedColumnPosition.Right)
      {
        for (int index = 0; index < row.RightPinnedColumns.Children.Count; ++index)
        {
          if (row.RightPinnedColumns.Children[index] == item)
            return index + row.LeftPinnedColumns.Children.Count + row.ScrollableColumns.Children.Count;
        }
        return -1;
      }
      for (int index = 0; index < row.ScrollableColumns.Children.Count; ++index)
      {
        if (row.ScrollableColumns.Children[index] == item)
          return index + row.LeftPinnedColumns.Children.Count;
      }
      return -1;
    }

    public void Insert(int index, GridCellElement item)
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    public GridCellElement this[int index]
    {
      get
      {
        if (this.row == null)
          return (GridCellElement) null;
        if (index < 0)
          return (GridCellElement) null;
        GridVirtualizedRowElement row = this.row as GridVirtualizedRowElement;
        if (row == null)
          return (GridCellElement) this.row.Children[index];
        int count1 = row.LeftPinnedColumns.Children.Count;
        if (index < count1)
          return (GridCellElement) row.LeftPinnedColumns.Children[index];
        int count2 = row.ScrollableColumns.Children.Count;
        index -= count1;
        if (index < count2)
          return (GridCellElement) row.ScrollableColumns.Children[index];
        index -= count2;
        if (index < row.RightPinnedColumns.Children.Count)
          return (GridCellElement) row.RightPinnedColumns.Children[index];
        return (GridCellElement) null;
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void Add(GridCellElement item)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(GridCellElement item)
    {
      return this.IndexOf(item) >= 0;
    }

    public void CopyTo(GridCellElement[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        if (this.row == null)
          return 0;
        GridVirtualizedRowElement row = this.row as GridVirtualizedRowElement;
        if (row != null)
          return row.LeftPinnedColumns.Children.Count + row.RightPinnedColumns.Children.Count + row.ScrollableColumns.Children.Count;
        return this.row.Children.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public bool Remove(GridCellElement item)
    {
      throw new NotImplementedException();
    }

    public IEnumerator<GridCellElement> GetEnumerator()
    {
      return (IEnumerator<GridCellElement>) new VisualCellsCollection.VisualCellsCollectionEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new VisualCellsCollection.VisualCellsCollectionEnumerator(this);
    }

    private class VisualCellsCollectionEnumerator : IEnumerator<GridCellElement>, IDisposable, IEnumerator
    {
      private int position = -1;
      private VisualCellsCollection collection;
      private GridCellElement current;

      public VisualCellsCollectionEnumerator(VisualCellsCollection collection)
      {
        this.collection = collection;
      }

      public GridCellElement Current
      {
        get
        {
          return this.current;
        }
      }

      public void Dispose()
      {
        this.collection = (VisualCellsCollection) null;
        this.current = (GridCellElement) null;
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.current;
        }
      }

      public bool MoveNext()
      {
        if (this.position >= this.collection.Count - 1)
          return false;
        ++this.position;
        this.current = this.collection[this.position];
        return true;
      }

      public void Reset()
      {
        this.current = (GridCellElement) null;
        this.position = -1;
      }
    }
  }
}
