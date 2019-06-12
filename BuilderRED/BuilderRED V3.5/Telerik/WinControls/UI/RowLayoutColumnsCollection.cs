// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowLayoutColumnsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class RowLayoutColumnsCollection : IList<GridViewColumn>, ICollection<GridViewColumn>, IEnumerable<GridViewColumn>, IEnumerable
  {
    private List<GridViewColumn> collection = new List<GridViewColumn>();
    private bool rightToLeft;

    public bool RightToLeft
    {
      get
      {
        return this.rightToLeft;
      }
      set
      {
        this.rightToLeft = value;
      }
    }

    public IEnumerator<GridViewColumn> GetEnumerator()
    {
      return (IEnumerator<GridViewColumn>) new RowLayoutColumnsCollection.RenderColumnsCollectionEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public void Insert(int index, GridViewColumn item)
    {
      this.collection.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      this.collection.RemoveAt(index);
    }

    public GridViewColumn this[int index]
    {
      get
      {
        if (this.rightToLeft)
          index = this.collection.Count - index - 1;
        return this.collection[index];
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public int IndexOf(GridViewColumn value)
    {
      int num = this.collection.IndexOf(value);
      if (this.rightToLeft)
        num = this.collection.Count - num - 1;
      return num;
    }

    public int Count
    {
      get
      {
        return this.collection.Count;
      }
    }

    public void Add(GridViewColumn item)
    {
      this.collection.Add(item);
    }

    public void Clear()
    {
      this.collection.Clear();
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool Remove(GridViewColumn item)
    {
      return this.collection.Remove(item);
    }

    public bool Contains(GridViewColumn value)
    {
      return this.collection.Contains(value);
    }

    public void CopyTo(GridViewColumn[] array, int index)
    {
      if (this.rightToLeft)
        index = this.collection.Count - index - 1;
      this.collection.CopyTo(array, index);
    }

    public class RenderColumnsCollectionEnumerator : IEnumerator<GridViewColumn>, IDisposable, IEnumerator
    {
      private RowLayoutColumnsCollection collection;
      private int position;

      public RenderColumnsCollectionEnumerator(RowLayoutColumnsCollection collection)
      {
        this.collection = collection;
        this.position = -1;
      }

      public GridViewColumn Current
      {
        get
        {
          return this.collection[this.position];
        }
      }

      public void Dispose()
      {
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
