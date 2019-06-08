// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataItemGroupCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [Editor("Telerik.WinControls.UI.Design.ListViewGroupCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  public class ListViewDataItemGroupCollection : IList<ListViewDataItemGroup>, ICollection<ListViewDataItemGroup>, IEnumerable<ListViewDataItemGroup>, IList, ICollection, IEnumerable, INotifyCollectionChanged
  {
    protected ObservableCollection<ListViewDataItemGroup> autoGroups;
    protected ObservableCollection<ListViewDataItemGroup> customGroups;
    protected RadListViewElement owner;
    private int updateCount;

    internal ObservableCollection<ListViewDataItemGroup> AutoGroups
    {
      get
      {
        return this.autoGroups;
      }
    }

    private void EnsureAutoGroups()
    {
      if (this.updateCount > 0)
        return;
      this.BeginUpdate();
      if (this.owner.ListSource.CollectionView.Groups.Count == 0)
        this.AutoGroups.Clear();
      this.EndUpdate();
    }

    protected virtual void EnsureFilterPredicate(ListViewDataItemGroup item)
    {
      if (this.owner == null || !this.owner.EnableFiltering || this.owner.DataView.Filter == null)
        return;
      RadListSource<ListViewDataItem> innerList = item.Items.InnerList as RadListSource<ListViewDataItem>;
      if (innerList == null)
        return;
      innerList.CollectionView.Filter = this.owner.DataView.Filter;
    }

    public ListViewDataItemGroupCollection(RadListViewElement owner)
    {
      this.owner = owner;
      this.autoGroups = new ObservableCollection<ListViewDataItemGroup>();
      this.autoGroups.CollectionChanged += new NotifyCollectionChangedEventHandler(this.groups_CollectionChanged);
      this.customGroups = new ObservableCollection<ListViewDataItemGroup>();
      this.customGroups.CollectionChanged += new NotifyCollectionChangedEventHandler(this.groups_CollectionChanged);
    }

    public virtual void BeginUpdate()
    {
      ++this.updateCount;
    }

    public virtual void EndUpdate()
    {
      if (this.updateCount <= 0)
        return;
      --this.updateCount;
    }

    private bool IsCustomGrouping
    {
      get
      {
        if (!this.owner.EnableCustomGrouping)
          return this.owner.IsDesignMode;
        return true;
      }
    }

    public virtual void AddRange(
      params ListViewDataItemGroup[] listViewDataItemGroups)
    {
      this.BeginUpdate();
      for (int index = 0; index < listViewDataItemGroups.Length; ++index)
        this.Add(listViewDataItemGroups[index]);
      this.EndUpdate();
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    private void groups_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems != null && (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace))
      {
        foreach (ListViewDataItem newItem in (IEnumerable) e.NewItems)
          newItem.Owner = this.owner;
      }
      if (e.OldItems != null && (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace))
      {
        foreach (ListViewDataItem oldItem in (IEnumerable) e.OldItems)
          oldItem.Owner = (RadListViewElement) null;
      }
      if (e.Action == NotifyCollectionChangedAction.Batch || e.Action == NotifyCollectionChangedAction.Reset)
      {
        foreach (ListViewDataItem listViewDataItem in this)
          listViewDataItem.Owner = this.owner;
      }
      if (sender == this.customGroups && this.owner.EnableCustomGrouping)
      {
        this.OnNotifyCollectionChanged(e);
      }
      else
      {
        if (sender != this.autoGroups || this.owner.EnableCustomGrouping)
          return;
        this.OnNotifyCollectionChanged(e);
      }
    }

    private void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.updateCount != 0 || this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, e);
    }

    public int IndexOf(ListViewDataItemGroup item)
    {
      if (this.IsCustomGrouping)
        return this.customGroups.IndexOf(item);
      this.EnsureAutoGroups();
      return this.autoGroups.IndexOf(item);
    }

    public void Insert(int index, ListViewDataItemGroup item)
    {
      this.customGroups.Insert(index, item);
      this.EnsureFilterPredicate(item);
    }

    public void RemoveAt(int index)
    {
      this.customGroups.RemoveAt(index);
    }

    public ListViewDataItemGroup this[int index]
    {
      get
      {
        if (this.IsCustomGrouping)
          return this.customGroups[index];
        this.EnsureAutoGroups();
        return this.autoGroups[index];
      }
      set
      {
        this.customGroups[index] = value;
        this.EnsureFilterPredicate(value);
      }
    }

    public void Add(ListViewDataItemGroup item)
    {
      this.customGroups.Add(item);
      this.EnsureFilterPredicate(item);
    }

    public void Clear()
    {
      this.customGroups.Clear();
    }

    public bool Contains(ListViewDataItemGroup item)
    {
      if (this.IsCustomGrouping)
        return this.customGroups.Contains(item);
      this.EnsureAutoGroups();
      return this.autoGroups.Contains(item);
    }

    public void CopyTo(ListViewDataItemGroup[] array, int arrayIndex)
    {
      if (this.IsCustomGrouping)
      {
        this.customGroups.CopyTo(array, arrayIndex);
      }
      else
      {
        this.EnsureAutoGroups();
        this.autoGroups.CopyTo(array, arrayIndex);
      }
    }

    public int Count
    {
      get
      {
        if (this.IsCustomGrouping)
          return this.customGroups.Count;
        this.EnsureAutoGroups();
        return this.autoGroups.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return !this.IsCustomGrouping;
      }
    }

    public bool Remove(ListViewDataItemGroup item)
    {
      return this.customGroups.Remove(item);
    }

    public IEnumerator<ListViewDataItemGroup> GetEnumerator()
    {
      if (this.IsCustomGrouping)
        return this.customGroups.GetEnumerator();
      this.EnsureAutoGroups();
      return this.autoGroups.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      if (this.IsCustomGrouping)
        return (IEnumerator) this.customGroups.GetEnumerator();
      this.EnsureAutoGroups();
      return (IEnumerator) this.autoGroups.GetEnumerator();
    }

    int IList.Add(object value)
    {
      this.Add(value as ListViewDataItemGroup);
      return this.Count - 1;
    }

    bool IList.Contains(object value)
    {
      return this.Contains(value as ListViewDataItemGroup);
    }

    int IList.IndexOf(object value)
    {
      return this.IndexOf(value as ListViewDataItemGroup);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, value as ListViewDataItemGroup);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    void IList.Remove(object value)
    {
      this.Remove(value as ListViewDataItemGroup);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        this[index] = value as ListViewDataItemGroup;
      }
    }

    public void CopyTo(Array array, int index)
    {
      if (this.IsCustomGrouping)
      {
        ((ICollection) this.customGroups).CopyTo(array, index);
      }
      else
      {
        this.EnsureAutoGroups();
        ((ICollection) this.autoGroups).CopyTo(array, index);
      }
    }

    public bool IsSynchronized
    {
      get
      {
        if (!this.IsCustomGrouping)
          return ((ICollection) this.autoGroups).IsSynchronized;
        return ((ICollection) this.customGroups).IsSynchronized;
      }
    }

    public object SyncRoot
    {
      get
      {
        if (!this.IsCustomGrouping)
          return ((ICollection) this.autoGroups).SyncRoot;
        return ((ICollection) this.customGroups).SyncRoot;
      }
    }
  }
}
