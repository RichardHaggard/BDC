// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnArrangeInfoCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  internal class ColumnArrangeInfoCollection : IList<GridViewColumn>, ICollection<GridViewColumn>, IEnumerable<GridViewColumn>, IEnumerable
  {
    private IList<TableViewCellArrangeInfo> arrangeInfos;

    public ColumnArrangeInfoCollection(IList<TableViewCellArrangeInfo> arrangeInfos)
    {
      this.arrangeInfos = arrangeInfos;
    }

    public int IndexOf(GridViewColumn item)
    {
      for (int index = 0; index < this.arrangeInfos.Count; ++index)
      {
        if (this.arrangeInfos[index].Column == item)
          return index;
      }
      return -1;
    }

    public void Insert(int index, GridViewColumn item)
    {
      throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    public GridViewColumn this[int index]
    {
      get
      {
        return this.arrangeInfos[index].Column;
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void Add(GridViewColumn item)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool Contains(GridViewColumn item)
    {
      return this.IndexOf(item) >= 0;
    }

    public void CopyTo(GridViewColumn[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        return this.arrangeInfos.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public bool Remove(GridViewColumn item)
    {
      throw new NotImplementedException();
    }

    public IEnumerator<GridViewColumn> GetEnumerator()
    {
      return (IEnumerator<GridViewColumn>) new ColumnArrangeInfoCollection.ColumnArrangeInfoCollectionEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new ColumnArrangeInfoCollection.ColumnArrangeInfoCollectionEnumerator(this);
    }

    public class ColumnArrangeInfoCollectionEnumerator : IEnumerator<GridViewColumn>, IDisposable, IEnumerator
    {
      private ColumnArrangeInfoCollection collection;
      private int position;

      public ColumnArrangeInfoCollectionEnumerator(ColumnArrangeInfoCollection collection)
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
        this.collection = (ColumnArrangeInfoCollection) null;
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
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
