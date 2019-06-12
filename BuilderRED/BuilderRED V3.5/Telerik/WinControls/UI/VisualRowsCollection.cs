// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualRowsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class VisualRowsCollection : IList<GridRowElement>, ICollection<GridRowElement>, IEnumerable<GridRowElement>, IEnumerable
  {
    private RowsContainerElement container;

    public VisualRowsCollection(RowsContainerElement container)
    {
      this.container = container;
    }

    public int IndexOf(GridRowElement item)
    {
      if (item.RowInfo.PinPosition == PinnedRowPosition.Top)
      {
        for (int index = 0; index < this.container.TopPinnedRows.Children.Count; ++index)
        {
          if (this.container.TopPinnedRows.Children[index] == item)
            return index;
        }
        return -1;
      }
      if (item.RowInfo.PinPosition == PinnedRowPosition.Bottom)
      {
        for (int index = 0; index < this.container.BottomPinnedRows.Children.Count; ++index)
        {
          if (this.container.BottomPinnedRows.Children[index] == item)
            return index + this.container.TopPinnedRows.Children.Count + this.container.ScrollableRows.Children.Count;
        }
        return -1;
      }
      for (int index = 0; index < this.container.ScrollableRows.Children.Count; ++index)
      {
        if (this.container.ScrollableRows.Children[index] == item)
          return index + this.container.TopPinnedRows.Children.Count;
      }
      return -1;
    }

    public void Insert(int index, GridRowElement item)
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    public GridRowElement this[int index]
    {
      get
      {
        if (index >= 0)
        {
          int count1 = this.container.TopPinnedRows.Children.Count;
          if (index < count1)
            return (GridRowElement) this.container.TopPinnedRows.Children[index];
          int count2 = this.container.ScrollableRows.Children.Count;
          index -= count1;
          if (index < count2)
            return (GridRowElement) this.container.ScrollableRows.Children[index];
          index -= count2;
          if (index < this.container.BottomPinnedRows.Children.Count)
            return (GridRowElement) this.container.BottomPinnedRows.Children[index];
        }
        return (GridRowElement) null;
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void Add(GridRowElement item)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(GridRowElement item)
    {
      return this.IndexOf(item) >= 0;
    }

    public void CopyTo(GridRowElement[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        return this.container.TopPinnedRows.Children.Count + this.container.BottomPinnedRows.Children.Count + this.container.ScrollableRows.Children.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public bool Remove(GridRowElement item)
    {
      throw new NotImplementedException();
    }

    public IEnumerator<GridRowElement> GetEnumerator()
    {
      return (IEnumerator<GridRowElement>) new VisualRowsCollection.VisualRowsCollectionEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new VisualRowsCollection.VisualRowsCollectionEnumerator(this);
    }

    public class VisualRowsCollectionEnumerator : IEnumerator<GridRowElement>, IDisposable, IEnumerator
    {
      private VisualRowsCollection collection;
      private GridRowElement current;
      private int position;

      public VisualRowsCollectionEnumerator(VisualRowsCollection collection)
      {
        this.collection = collection;
        this.position = -1;
      }

      public GridRowElement Current
      {
        get
        {
          return this.current;
        }
      }

      public void Dispose()
      {
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
        this.current = (GridRowElement) null;
        this.position = -1;
      }
    }
  }
}
