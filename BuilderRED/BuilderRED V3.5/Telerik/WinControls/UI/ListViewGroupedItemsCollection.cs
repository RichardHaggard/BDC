// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewGroupedItemsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class ListViewGroupedItemsCollection : IReadOnlyCollection<ListViewDataItem>, IEnumerable<ListViewDataItem>, IEnumerable, IDisposable
  {
    private IList<ListViewDataItem> innerList;
    private bool isBound;

    protected internal IList<ListViewDataItem> InnerList
    {
      get
      {
        return this.innerList;
      }
      set
      {
        if (this.innerList == value)
          return;
        if (this.innerList is RadListSource<ListViewDataItem>)
          ((RadListSource<ListViewDataItem>) this.innerList).Dispose();
        this.innerList = value;
        this.isBound = !(value is RadListSource<ListViewDataItem>);
      }
    }

    public ListViewGroupedItemsCollection()
    {
      this.ResetListSource();
    }

    public int Count
    {
      get
      {
        if (!this.isBound)
          return ((RadListSource<ListViewDataItem>) this.innerList).CollectionView.Count;
        return this.innerList.Count;
      }
    }

    public ListViewDataItem this[int index]
    {
      get
      {
        if (!this.isBound)
          return ((RadListSource<ListViewDataItem>) this.innerList).CollectionView[index];
        return this.innerList[index];
      }
    }

    public bool Contains(ListViewDataItem value)
    {
      if (!this.isBound)
        return ((RadListSource<ListViewDataItem>) this.innerList).CollectionView.Contains(value);
      return this.innerList.Contains(value);
    }

    public void CopyTo(ListViewDataItem[] array, int index)
    {
      if (!this.isBound)
        ((RadListSource<ListViewDataItem>) this.innerList).CollectionView.CopyTo(array, index);
      else
        this.innerList.CopyTo(array, index);
    }

    public int IndexOf(ListViewDataItem value)
    {
      if (!this.isBound)
        return ((RadListSource<ListViewDataItem>) this.innerList).CollectionView.IndexOf(value);
      return this.innerList.IndexOf(value);
    }

    public IEnumerator<ListViewDataItem> GetEnumerator()
    {
      if (!this.isBound)
        return ((RadListSource<ListViewDataItem>) this.innerList).CollectionView.GetEnumerator();
      return this.innerList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      if (!this.isBound)
        return (IEnumerator) ((RadListSource<ListViewDataItem>) this.innerList).CollectionView.GetEnumerator();
      return (IEnumerator) this.innerList.GetEnumerator();
    }

    public void ResetListSource()
    {
      this.InnerList = (IList<ListViewDataItem>) new RadListSource<ListViewDataItem>()
      {
        CollectionView = {
          ChangeCurrentOnAdd = false
        }
      };
    }

    public void Dispose()
    {
      if (!(this.InnerList is IDisposable))
        return;
      ((IDisposable) this.InnerList).Dispose();
    }
  }
}
