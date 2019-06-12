// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListDataItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadListDataItemCollection : IList<RadListDataItem>, ICollection<RadListDataItem>, IEnumerable<RadListDataItem>, IList, ICollection, IEnumerable
  {
    private ListDataLayer dataLayer;
    private RadListElement ownerListElement;
    private bool useDataView;

    public RadListDataItemCollection(ListDataLayer dataLayer, RadListElement ownerListElement)
      : this(dataLayer, ownerListElement, true)
    {
    }

    internal RadListDataItemCollection(
      ListDataLayer dataLayer,
      RadListElement ownerListElement,
      bool useDataView)
    {
      this.dataLayer = dataLayer;
      this.ownerListElement = ownerListElement;
      this.useDataView = useDataView;
    }

    public ListDataLayer Owner
    {
      get
      {
        return this.dataLayer;
      }
    }

    public RadListElement OwnerListElement
    {
      get
      {
        return this.ownerListElement;
      }
    }

    public RadListDataItem First
    {
      get
      {
        if (this.Count > 0)
          return this[0];
        return (RadListDataItem) null;
      }
    }

    public RadListDataItem Last
    {
      get
      {
        if (this.Count > 0)
          return this[this.Count - 1];
        return (RadListDataItem) null;
      }
    }

    public int IndexOf(RadListDataItem item)
    {
      if (this.useDataView)
        return this.dataLayer.DataView.IndexOf(item);
      return this.dataLayer.ListSource.IndexOf(item);
    }

    public int IndexOf(string text)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Text.Equals(text, StringComparison.InvariantCulture))
          return index;
      }
      return -1;
    }

    public void Insert(int index, RadListDataItem item)
    {
      this.OwnerListElement.CheckReadyForUnboundMode();
      item.DataLayer = this.dataLayer;
      item.Owner = this.ownerListElement;
      NotifyCollectionChangingEventArgs args1 = new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.Add, (object) item);
      this.ownerListElement.OnItemsChanging(args1);
      if (args1.Cancel)
        return;
      NotifyCollectionChangedEventArgs args2 = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) item);
      this.dataLayer.ListSource.Insert(index, item);
      this.ownerListElement.OnItemsChanged(args2);
    }

    public void RemoveAt(int index)
    {
      this.OwnerListElement.CheckReadyForUnboundMode();
      RadListDataItem radListDataItem = this.useDataView ? this.dataLayer.DataView[index] : this.dataLayer.ListSource[index];
      NotifyCollectionChangingEventArgs args = new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.Remove, (object) radListDataItem);
      this.ownerListElement.OnItemsChanging(args);
      if (args.Cancel)
        return;
      radListDataItem.DataLayer = (ListDataLayer) null;
      radListDataItem.Owner = (RadListElement) null;
      this.dataLayer.ListSource.Remove(radListDataItem);
      this.ownerListElement.OnItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) radListDataItem));
    }

    public RadListDataItem this[int index]
    {
      get
      {
        if (this.useDataView)
          return this.dataLayer.DataView[index];
        return this.dataLayer.ListSource[index];
      }
      set
      {
        this.OwnerListElement.CheckReadyForUnboundMode();
        RadListDataItem radListDataItem = this.dataLayer.ListSource[index];
        NotifyCollectionChangingEventArgs args = new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.Replace, (object) value, (object) radListDataItem);
        this.ownerListElement.OnItemsChanging(args);
        if (args.Cancel)
          return;
        radListDataItem.DataLayer = (ListDataLayer) null;
        radListDataItem.Owner = (RadListElement) null;
        if (radListDataItem.Selected)
          ((IList) this.ownerListElement.SelectedItems).Remove((object) radListDataItem);
        this.dataLayer.ListSource[index] = value;
        value.DataLayer = this.dataLayer;
        value.Owner = this.ownerListElement;
        this.ownerListElement.OnItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (object) value, (object) radListDataItem));
      }
    }

    public void AddRange(IEnumerable<RadListDataItem> range)
    {
      this.OwnerListElement.CheckReadyForUnboundMode();
      List<RadListDataItem> radListDataItemList = new List<RadListDataItem>(range);
      NotifyCollectionChangingEventArgs args = new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.Batch, (IList) radListDataItemList);
      this.ownerListElement.OnItemsChanging(args);
      if (args.Cancel)
        return;
      this.ownerListElement.SuspendItemsChangeEvents = true;
      this.dataLayer.ListSource.BeginUpdate();
      foreach (RadListDataItem radListDataItem in range)
        this.Add(radListDataItem);
      this.dataLayer.ListSource.EndUpdate();
      this.ownerListElement.SuspendItemsChangeEvents = false;
      this.ownerListElement.OnItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Batch, (IList) radListDataItemList));
    }

    public virtual void AddRange(IEnumerable<string> textStrings)
    {
      List<RadListDataItem> radListDataItemList = new List<RadListDataItem>();
      foreach (string textString in textStrings)
        radListDataItemList.Add(new RadListDataItem(textString));
      this.AddRange((IEnumerable<RadListDataItem>) radListDataItemList);
    }

    public virtual void Add(string itemText)
    {
      this.Add(new RadListDataItem(itemText));
    }

    public virtual void Add(RadListDataItem item)
    {
      this.OwnerListElement.CheckReadyForUnboundMode();
      item.DataLayer = this.dataLayer;
      NotifyCollectionChangingEventArgs args = new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.Add, (object) item);
      this.ownerListElement.OnItemsChanging(args);
      if (args.Cancel)
        return;
      if (this.dataLayer.ListSource.IsDataBound)
        this.dataLayer.ListSource.AddNew().Text = item.Text;
      else
        this.dataLayer.ListSource.Add(item);
      item.Owner = this.ownerListElement;
      this.ownerListElement.OnItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) item));
    }

    public void Clear()
    {
      this.OwnerListElement.CheckReadyForUnboundMode();
      if (!this.OwnerListElement.IsDesignMode)
        this.OwnerListElement.ClearSelected();
      foreach (RadListDataItem radListDataItem in this.dataLayer.ListSource)
      {
        radListDataItem.DataLayer = (ListDataLayer) null;
        radListDataItem.Owner = (RadListElement) null;
      }
      this.ownerListElement.ActiveItem = (RadListDataItem) null;
      this.dataLayer.ListSource.Clear();
      this.ownerListElement.OnItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      this.ownerListElement.UpdateSelectedIndexOnItemsChanged();
    }

    public bool Contains(RadListDataItem item)
    {
      if (this.useDataView)
        return this.Owner.DataView.Contains(item);
      return this.Owner.ListSource.Contains(item);
    }

    public bool Contains(string text)
    {
      return this.IndexOf(text) >= 0;
    }

    public void CopyTo(RadListDataItem[] array, int arrayIndex)
    {
      for (int index = arrayIndex; index < this.Count; ++index)
        array[index] = this[index];
    }

    public int Count
    {
      get
      {
        if (this.useDataView)
          return this.Owner.DataView.Count;
        return this.Owner.ListSource.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.Owner.ListSource.IsReadOnly;
      }
    }

    public bool Remove(RadListDataItem item)
    {
      this.OwnerListElement.CheckReadyForUnboundMode();
      if (item == null)
        return false;
      NotifyCollectionChangingEventArgs args = new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.Remove, (object) item);
      this.ownerListElement.OnItemsChanging(args);
      if (args.Cancel)
        return false;
      item.DataLayer = (ListDataLayer) null;
      item.Owner = (RadListElement) null;
      bool flag = this.dataLayer.ListSource.Remove(item);
      this.ownerListElement.OnItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) item));
      return flag;
    }

    public IEnumerator<RadListDataItem> GetEnumerator()
    {
      if (this.useDataView)
        return this.dataLayer.DataView.GetEnumerator();
      return this.dataLayer.ListSource.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      if (this.useDataView)
        return ((IEnumerable) this.dataLayer.DataView).GetEnumerator();
      return ((IEnumerable) this.dataLayer.ListSource).GetEnumerator();
    }

    int IList.Add(object value)
    {
      RadListDataItem radListDataItem = (RadListDataItem) value;
      this.Add(radListDataItem);
      return radListDataItem.RowIndex;
    }

    bool IList.Contains(object value)
    {
      return this.Contains((RadListDataItem) value);
    }

    int IList.IndexOf(object value)
    {
      return this.IndexOf((RadListDataItem) value);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, (RadListDataItem) value);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return ((IList) this.Owner.ListSource).IsFixedSize;
      }
    }

    void IList.Remove(object value)
    {
      this.Remove((RadListDataItem) value);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        this[index] = (RadListDataItem) value;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      ((ICollection) this.Owner.ListSource).CopyTo(array, index);
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return ((ICollection) this.Owner.ListSource).IsSynchronized;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return ((ICollection) this.Owner.ListSource).SyncRoot;
      }
    }
  }
}
