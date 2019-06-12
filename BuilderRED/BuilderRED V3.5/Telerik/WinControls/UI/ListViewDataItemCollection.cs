// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  [Editor("Telerik.WinControls.UI.Design.ListViewItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  public class ListViewDataItemCollection : IList<ListViewDataItem>, ICollection<ListViewDataItem>, IEnumerable<ListViewDataItem>, IList, ICollection, IEnumerable, Telerik.WinControls.Data.INotifyCollectionChanged
  {
    private RadListViewElement owner;

    public ListViewDataItemCollection(RadListViewElement owner)
    {
      this.owner = owner;
      this.owner.ListSource.CollectionView.CollectionChanged += new Telerik.WinControls.Data.NotifyCollectionChangedEventHandler(this.CollectionView_CollectionChanged);
    }

    private void CollectionView_CollectionChanged(object sender, Telerik.WinControls.Data.NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
      {
        this.ProcessNewItems(e);
        if (!this.owner.ListSource.IsUpdating && this.Count <= 1)
          this.owner.DataView.EnsureDescriptors();
      }
      if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
        this.ProcessOldItems(e);
      if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Replace)
      {
        this.ProcessOldItems(e);
        this.ProcessNewItems(e);
      }
      if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Reset)
        this.ProcessResetOperation();
      this.owner.ViewElement.Scroller.UpdateScrollRange();
      this.owner.ViewElement.InvalidateMeasure(true);
      this.OnCollectionChanged(e);
    }

    private void ProcessResetOperation()
    {
      this.owner.SelectedItems.Reset();
      this.owner.CheckedItems.Reset();
      foreach (ListViewDataItem listViewItem in this.owner.ListSource)
      {
        listViewItem.Owner = this.owner;
        this.owner.SelectedItems.ProcessSelectedItem(listViewItem);
        this.owner.CheckedItems.ProcessCheckedItem(listViewItem);
      }
      if (!this.owner.SelectedItems.Contains(this.owner.SelectedItem))
        this.owner.SelectedItem = this.owner.SelectedItems.Count > 0 ? this.owner.SelectedItems[0] : (ListViewDataItem) null;
      if (this.owner.CurrentItem != null && !this.Contains(this.owner.CurrentItem))
        this.owner.ResetCurrentItem();
      if (this.owner.CurrentItem == null || !this.Contains(this.owner.CurrentItem))
        this.owner.CurrentItem = this.owner.SelectedItem;
      HybridDictionary hybridDictionary = new HybridDictionary();
      foreach (ListViewDataItem listViewDataItem in this.owner.ListSource)
        hybridDictionary.Add((object) listViewDataItem, (object) null);
      if (!this.owner.EnableCustomGrouping)
        return;
      foreach (ListViewDataItemGroup group in this.owner.Groups)
      {
        ListViewDataItem[] array = new ListViewDataItem[group.Items.Count];
        group.Items.CopyTo(array, 0);
        foreach (ListViewDataItem listViewDataItem in array)
        {
          if (!hybridDictionary.Contains((object) listViewDataItem))
          {
            listViewDataItem.Group = (ListViewDataItemGroup) null;
            listViewDataItem.Owner = (RadListViewElement) null;
          }
        }
      }
    }

    private void ProcessOldItems(Telerik.WinControls.Data.NotifyCollectionChangedEventArgs e)
    {
      foreach (ListViewDataItem oldItem in (IEnumerable) e.OldItems)
      {
        oldItem.Group = (ListViewDataItemGroup) null;
        oldItem.Owner = (RadListViewElement) null;
        this.owner.SelectedItems.ProcessSelectedItem(oldItem);
        this.owner.CheckedItems.ProcessCheckedItem(oldItem);
      }
    }

    private void ProcessNewItems(Telerik.WinControls.Data.NotifyCollectionChangedEventArgs e)
    {
      foreach (ListViewDataItem newItem in (IEnumerable) e.NewItems)
      {
        if (newItem.Owner != null && newItem.Owner != this.owner)
          newItem.Owner.Items.Remove(newItem);
        newItem.Owner = this.owner;
        this.owner.CheckedItems.ProcessCheckedItem(newItem);
        if (newItem.Selected)
          this.owner.SelectedItems.ProcessSelectedItem(newItem);
      }
    }

    public void BeginUpdate()
    {
      this.owner.ListSource.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    private void EndUpdate(bool notifyUpdates)
    {
      this.owner.ListSource.EndUpdate(notifyUpdates);
    }

    public void Add(string text)
    {
      ListViewDataItem listViewDataItem = this.owner.NewItem() as ListViewDataItem;
      if (listViewDataItem == null)
        return;
      listViewDataItem.Text = text;
      this.owner.ListSource.Add(listViewDataItem);
    }

    public void Add(object value)
    {
      ListViewDataItem listViewDataItem = this.owner.NewItem() as ListViewDataItem;
      if (listViewDataItem == null)
        return;
      listViewDataItem.Value = value;
      this.owner.ListSource.Add(listViewDataItem);
    }

    public void Add(params string[] values)
    {
      ListViewDataItem listViewDataItem = this.owner.NewItem() as ListViewDataItem;
      if (listViewDataItem == null)
        return;
      int length = values.GetLength(0);
      for (int index = 0; index < length; ++index)
        listViewDataItem[index] = (object) values[index];
      this.owner.ListSource.Add(listViewDataItem);
    }

    public void Add(params object[] values)
    {
      ListViewDataItem listViewDataItem = this.owner.NewItem() as ListViewDataItem;
      if (listViewDataItem == null)
        return;
      int length = values.GetLength(0);
      for (int index = 0; index < length; ++index)
        listViewDataItem[index] = values[index];
      this.owner.ListSource.Add(listViewDataItem);
    }

    public virtual void AddRange(params ListViewDataItem[] listViewDataItems)
    {
      this.BeginUpdate();
      for (int index = 0; index < listViewDataItems.Length; ++index)
        this.Add(listViewDataItems[index]);
      this.EndUpdate();
    }

    public virtual void AddRange(params string[] values)
    {
      this.BeginUpdate();
      for (int index = 0; index < values.Length; ++index)
        this.Add(values[index]);
      this.EndUpdate();
    }

    public int IndexOf(ListViewDataItem item)
    {
      return this.owner.ListSource.CollectionView.IndexOf(item);
    }

    public void Insert(int index, ListViewDataItem item)
    {
      try
      {
        if (item.Owner != null && item.Owner != this.owner)
          item.Owner.Items.Remove(item);
        item.Owner = this.owner;
        this.owner.ListSource.Insert(index, item);
      }
      catch (InvalidOperationException ex)
      {
        throw new InvalidOperationException("Items cannot be programmatically added to the RadListView's Items collection when the control is data-bound.", (Exception) ex);
      }
    }

    public ListViewDataItem this[int index]
    {
      get
      {
        return this.owner.ListSource.CollectionView[index];
      }
      set
      {
        this.owner.ListSource[this.owner.ListSource.IndexOf(this[index])] = value;
      }
    }

    public void Add(ListViewDataItem item)
    {
      if (item.Owner != null && item.Owner != this.owner)
        item.Owner.Items.Remove(item);
      item.Owner = this.owner;
      this.owner.ListSource.Add(item);
    }

    public void Clear()
    {
      foreach (ListViewDataItem listViewDataItem in this.owner.ListSource)
        listViewDataItem.Owner = (RadListViewElement) null;
      this.owner.ListSource.Clear();
    }

    public bool Contains(ListViewDataItem item)
    {
      return this.owner.ListSource.CollectionView.Contains(item);
    }

    public void CopyTo(ListViewDataItem[] array, int arrayIndex)
    {
      this.owner.ListSource.CollectionView.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.owner.ListSource.CollectionView.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.owner.ListSource.IsReadOnly;
      }
    }

    public bool Remove(ListViewDataItem item)
    {
      ListViewTraverser enumerator = this.owner.ViewElement.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Position = (object) item;
      if (enumerator.MovePrevious() && enumerator.Current != null && enumerator.Current != item)
      {
        this.owner.SelectedItem = enumerator.Current;
      }
      else
      {
        enumerator.Position = (object) item;
        this.owner.SelectedItem = !enumerator.MoveNext() || enumerator.Current == item ? (ListViewDataItem) null : enumerator.Current;
      }
      bool flag = this.owner.ListSource.Remove(item);
      item.Owner = (RadListViewElement) null;
      this.owner.SelectedItems.ProcessSelectedItem(item);
      this.owner.CheckedItems.ProcessCheckedItem(item);
      return flag;
    }

    public void RemoveAt(int index)
    {
      this.Remove(this[index]);
    }

    public IEnumerator<ListViewDataItem> GetEnumerator()
    {
      return this.owner.ListSource.CollectionView.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.owner.ListSource.CollectionView.GetEnumerator();
    }

    public event Telerik.WinControls.Data.NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(Telerik.WinControls.Data.NotifyCollectionChangedEventArgs args)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, args);
    }

    int IList.Add(object value)
    {
      ListViewDataItem listViewDataItem = value as ListViewDataItem;
      if (listViewDataItem == null)
        return -1;
      this.Add(listViewDataItem);
      return this.Count - 1;
    }

    public bool Contains(object value)
    {
      ListViewDataItem listViewDataItem = value as ListViewDataItem;
      if (listViewDataItem != null)
        return this.Contains(listViewDataItem);
      return false;
    }

    public int IndexOf(object value)
    {
      ListViewDataItem listViewDataItem = value as ListViewDataItem;
      if (listViewDataItem != null)
        return this.IndexOf(listViewDataItem);
      return -1;
    }

    public void Insert(int index, object value)
    {
      ListViewDataItem listViewDataItem = value as ListViewDataItem;
      if (listViewDataItem == null)
        return;
      this.Insert(index, listViewDataItem);
    }

    public bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public void Remove(object value)
    {
      ListViewDataItem listViewDataItem = value as ListViewDataItem;
      if (listViewDataItem == null)
        return;
      this.Remove(listViewDataItem);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        this[index] = value as ListViewDataItem;
      }
    }

    public void CopyTo(Array array, int index)
    {
      ((ICollection) this.owner.ListSource).CopyTo(array, index);
    }

    public bool IsSynchronized
    {
      get
      {
        return ((ICollection) this.owner.ListSource).IsSynchronized;
      }
    }

    public object SyncRoot
    {
      get
      {
        return ((ICollection) this.owner.ListSource).SyncRoot;
      }
    }
  }
}
