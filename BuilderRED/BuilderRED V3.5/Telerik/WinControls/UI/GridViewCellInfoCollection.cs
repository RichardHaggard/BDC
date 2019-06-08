// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellInfoCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GridViewCellInfoCollection : ICollection, IEnumerable
  {
    private GridViewRowInfo rowInfo;
    private Dictionary<int, GridViewCellInfo> savedCells;

    public GridViewCellInfoCollection(GridViewRowInfo rowInfo)
    {
      this.rowInfo = rowInfo;
    }

    public GridViewCellInfo this[int index]
    {
      get
      {
        GridViewTemplate viewTemplate = this.rowInfo.ViewTemplate;
        if (viewTemplate == null)
          return (GridViewCellInfo) null;
        if (index < 0 || index >= viewTemplate.Columns.Count)
          return (GridViewCellInfo) null;
        GridViewColumn column = (GridViewColumn) viewTemplate.Columns[index];
        if (column == null)
          return (GridViewCellInfo) null;
        return this.GetCellInfo(column);
      }
    }

    public GridViewCellInfo this[string name]
    {
      get
      {
        GridViewTemplate viewTemplate = this.rowInfo.ViewTemplate;
        if (viewTemplate != null)
        {
          GridViewDataColumn column = viewTemplate.Columns[name];
          if (column != null)
            return this.GetCellInfo((GridViewColumn) column);
        }
        return (GridViewCellInfo) null;
      }
    }

    internal Dictionary<int, GridViewCellInfo> SavedCells
    {
      get
      {
        if (this.savedCells == null)
          this.savedCells = new Dictionary<int, GridViewCellInfo>();
        return this.savedCells;
      }
    }

    public void CopyTo(Array array, int index)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public int Count
    {
      get
      {
        return this.rowInfo.ViewTemplate.Columns.Count;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    public object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new GridViewCellInfoCollection.GridViewCellInfoCollectionEnumerator(this);
    }

    private GridViewCellInfo GetCellInfo(GridViewColumn column)
    {
      if (this.savedCells != null && this.savedCells.ContainsKey(column.GetHashCode()))
        return this.savedCells[column.GetHashCode()];
      if (!(column is GridViewHyperlinkColumn))
        return new GridViewCellInfo(this.rowInfo, column, this);
      GridViewHyperlinkCellInfo hyperlinkCellInfo = new GridViewHyperlinkCellInfo(this.rowInfo, column, this);
      hyperlinkCellInfo.PersistCellInfo();
      return (GridViewCellInfo) hyperlinkCellInfo;
    }

    public class GridViewCellInfoCollectionEnumerator : IEnumerator
    {
      private GridViewCellInfoCollection collection;
      private object currentObject;
      private int index;

      public GridViewCellInfoCollectionEnumerator(GridViewCellInfoCollection collection)
      {
        this.collection = collection;
        this.currentObject = (object) null;
        this.index = -1;
      }

      public object Current
      {
        get
        {
          return this.currentObject;
        }
      }

      public bool MoveNext()
      {
        if (this.index < this.collection.Count - 1)
        {
          this.currentObject = (object) this.collection[++this.index];
          return true;
        }
        this.currentObject = (object) null;
        return false;
      }

      public void Reset()
      {
        this.currentObject = (object) null;
        this.index = -1;
      }
    }
  }
}
