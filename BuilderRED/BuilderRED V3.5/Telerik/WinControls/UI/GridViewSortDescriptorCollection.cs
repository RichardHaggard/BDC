// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSortDescriptorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewSortDescriptorCollection : SortDescriptorCollection, INotifyCollectionChanging
  {
    private GridViewTemplate owner;

    public GridViewSortDescriptorCollection(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    protected override void InsertItem(int index, SortDescriptor item)
    {
      if (this.owner.MasterTemplate != null && this.owner.MasterTemplate.VirtualMode && this.owner.MasterTemplate.ThrowExceptionOnDataOperationInVirtualMode)
        throw new InvalidOperationException("Sorting operation is not supported in VirtualMode.");
      if (!this.OnSortChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Add, (IList) new object[1]
      {
        (object) item
      }, (IList) new object[0], index, -1, new PropertyChangingEventArgsEx(item.PropertyName, (object) null, (object) item.Direction))))
        return;
      base.InsertItem(index, item);
      item.PropertyChanging += new PropertyChangingEventHandlerEx(this.item_PropertyChanging);
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    protected override void SetItem(int index, SortDescriptor item)
    {
      if (!this.OnSortChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Replace, (IList) new object[1]
      {
        (object) item
      }, (IList) new object[1]{ (object) this[index] }, index, -1, new PropertyChangingEventArgsEx(this[index].PropertyName, (object) this[index].Direction, (object) item.Direction))))
        return;
      this[index].PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      this[index].PropertyChanging -= new PropertyChangingEventHandlerEx(this.item_PropertyChanging);
      base.SetItem(index, item);
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
      item.PropertyChanging += new PropertyChangingEventHandlerEx(this.item_PropertyChanging);
    }

    protected override void MoveItem(int oldIndex, int newIndex)
    {
      if (this.owner != null)
      {
        if (!this.OnSortChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Move, (IList) new object[1]
        {
          (object) this[newIndex]
        }, (IList) new object[1]
        {
          (object) this[oldIndex]
        }, newIndex, oldIndex, new PropertyChangingEventArgsEx(this[oldIndex].PropertyName, (object) this[oldIndex].Direction, (object) this[newIndex].Direction))))
          return;
      }
      base.MoveItem(oldIndex, newIndex);
    }

    protected override void RemoveItem(int index)
    {
      if (!this.OnSortChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Remove, (IList) new object[0], (IList) new object[1]
      {
        (object) this[index]
      }, -1, index, new PropertyChangingEventArgsEx(this[index].PropertyName, (object) this[index].Direction, (object) null))))
        return;
      this[index].PropertyChanging -= new PropertyChangingEventHandlerEx(this.item_PropertyChanging);
      this[index].PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      base.RemoveItem(index);
    }

    protected override void ClearItems()
    {
      if (this.Count == 0)
        return;
      SortDescriptor[] array = new SortDescriptor[this.Count];
      this.CopyTo(array, 0);
      if (this.owner != null)
      {
        if (!this.OnSortChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Reset, (IList) new object[0], (IList) new object[1]
        {
          (object) array
        }, -1, -1, (PropertyChangingEventArgsEx) null)))
          return;
      }
      for (int index = 0; index < this.Count; ++index)
      {
        this[index].PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
        this[index].PropertyChanging -= new PropertyChangingEventHandlerEx(this.item_PropertyChanging);
      }
      base.ClearItems();
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }

    private void item_PropertyChanging(object sender, PropertyChangingEventArgsEx e)
    {
      if (this.Suspended)
        return;
      SortDescriptor sortDescriptor1 = sender as SortDescriptor;
      if (sortDescriptor1 == null || !(e.PropertyName == "PropertyName") && !(e.PropertyName == "Direction"))
        return;
      SortDescriptor sortDescriptor2 = (SortDescriptor) sortDescriptor1.Clone();
      if (e.PropertyName == "PropertyName")
        sortDescriptor2.PropertyName = e.NewValue.ToString();
      if (e.PropertyName == "Direction")
        sortDescriptor2.Direction = (ListSortDirection) e.NewValue;
      int num = this.IndexOf(sortDescriptor1);
      e.Cancel = (!this.OnSortChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.ItemChanging, (IList) new object[1]
      {
        (object) sortDescriptor2
      }, (IList) new object[1]
      {
        (object) sortDescriptor1
      }, num, num, e)) ? 1 : 0) != 0;
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      base.OnCollectionChanged(args);
      if (this.Suspended)
        return;
      this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangedEventArgs>(EventDispatcher.SortChangedEvent, (object) this.owner, new GridViewCollectionChangedEventArgs(this.owner, args));
    }

    private bool OnSortChanging(GridViewCollectionChangingEventArgs e)
    {
      if (this.Suspended)
        return true;
      this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.SortChangingEvent, (object) this.owner, e);
      if (e.Cancel)
        return false;
      if (this.CollectionChanging != null)
        this.CollectionChanging((object) this, (NotifyCollectionChangingEventArgs) e);
      return !e.Cancel;
    }

    public event NotifyCollectionChangingEventHandler CollectionChanging;

    protected virtual void OnCollectionChanging(NotifyCollectionChangingEventArgs args)
    {
      if (this.CollectionChanging == null || this.Suspended)
        return;
      this.CollectionChanging((object) this, args);
    }

    private void OnCollectionChanging(NotifyCollectionChangedAction action)
    {
      this.OnCollectionChanging(new NotifyCollectionChangingEventArgs(action));
    }

    private void OnCollectionChanging(NotifyCollectionChangedAction action, object item, int index)
    {
      this.OnCollectionChanging(new NotifyCollectionChangingEventArgs(action, item, index));
    }

    private void OnCollectionChanging(
      NotifyCollectionChangedAction action,
      object oldItem,
      object item,
      int index)
    {
      this.OnCollectionChanging(new NotifyCollectionChangingEventArgs(action, oldItem, item, index));
    }
  }
}
