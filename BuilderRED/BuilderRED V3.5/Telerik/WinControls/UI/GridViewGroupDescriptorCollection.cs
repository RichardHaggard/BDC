// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewGroupDescriptorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewGroupDescriptorCollection : GroupDescriptorCollection, INotifyCollectionChanging
  {
    private GridViewTemplate owner;

    public GridViewGroupDescriptorCollection(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    protected override void InsertItem(int index, GroupDescriptor item)
    {
      if (this.owner.MasterTemplate != null && this.owner.MasterTemplate.VirtualMode && this.owner.MasterTemplate.ThrowExceptionOnDataOperationInVirtualMode)
        throw new InvalidOperationException("Grouping operation is not supported in VirtualMode.");
      if (this.OnGroupByChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Add, (object) item, index, -1)))
        return;
      if (item is GridGroupByExpression && !string.IsNullOrEmpty(item.Expression) && item.GroupNames.Count == 0)
        ((GridGroupByExpression) item).Update();
      base.InsertItem(index, item);
    }

    protected override void SetItem(int index, GroupDescriptor item)
    {
      if (this.OnGroupByChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Replace, (object) this[index], index, -1)))
        return;
      base.SetItem(index, item);
    }

    protected override void MoveItem(int oldIndex, int newIndex)
    {
      if (this.OnGroupByChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Move, (object) this[oldIndex], newIndex, oldIndex)))
        return;
      base.MoveItem(oldIndex, newIndex);
    }

    protected override void ClearItems()
    {
      if (this.OnGroupByChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Reset, (object) null, -1, -1)))
        return;
      base.ClearItems();
    }

    protected override void RemoveItem(int index)
    {
      if (this.OnGroupByChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Remove, (object) this[index], -1, index)))
        return;
      base.RemoveItem(index);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      base.OnCollectionChanged(args);
      if (this.Suspended)
        return;
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.CollectionChanged, GridEventType.Both, GridEventDispatchMode.Send);
      GridViewSynchronizationService.DispatchEvent(this.owner, new GridViewEvent((object) this, (object) null, new object[1]
      {
        (object) args
      }, eventInfo), false);
      this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangedEventArgs>(EventDispatcher.GroupByChanged, (object) this.owner, new GridViewCollectionChangedEventArgs(this.owner, args));
    }

    private bool OnGroupByChanging(GridViewCollectionChangingEventArgs e)
    {
      if (this.Suspended)
        return false;
      if (this.CollectionChanging != null)
        this.CollectionChanging((object) this, (NotifyCollectionChangingEventArgs) e);
      if (e.Cancel)
        return e.Cancel;
      this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.GroupByChanging, (object) this.owner, e);
      return e.Cancel;
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
