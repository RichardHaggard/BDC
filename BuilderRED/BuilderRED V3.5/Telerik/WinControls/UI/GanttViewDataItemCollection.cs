// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDataItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GanttViewDataItemCollection : NotifyCollection<GanttViewDataItem>
  {
    private GanttViewDataItem owner;
    private int version;

    public GanttViewDataItemCollection(GanttViewDataItem owner)
      : base((IList<GanttViewDataItem>) new GanttViewDataItemView(owner))
    {
      this.owner = owner;
    }

    [Browsable(false)]
    public GanttViewDataItem Owner
    {
      get
      {
        return this.owner;
      }
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        if (this.owner != null)
          return this.owner.GanttViewElement;
        return (RadGanttViewElement) null;
      }
    }

    public void Refresh()
    {
      if (this.owner.GanttViewElement == null)
        return;
      ++this.version;
      this.Update();
      this.owner.GanttViewElement.Update(RadGanttViewElement.UpdateActions.Resume);
    }

    public IGanttViewDataItemEnumerator GetGanttViewDataItemEnumerator()
    {
      return ((GanttViewDataItemView) this.Items).GetGanttViewDataItemEnumerator();
    }

    public IGanttViewDataItemEnumerator GetGanttViewDataItemEnumerator(
      int position)
    {
      return ((GanttViewDataItemView) this.Items).GetGanttViewDataItemEnumerator(position);
    }

    public IGanttViewDataItemEnumerator GetGanttViewDataItemEnumerator(
      GanttViewDataItem item)
    {
      return ((GanttViewDataItemView) this.Items).GetGanttViewDataItemEnumerator(item);
    }

    protected internal void Update()
    {
      if (this.Suspended)
        return;
      ((GanttViewDataItemView) this.Items).Update();
    }

    internal void UpdateView()
    {
      ((GanttViewDataItemView) this.Items).UpdateView();
    }

    protected override void InsertItem(int index, GanttViewDataItem item)
    {
      if (item.GanttViewElement != null && !this.owner.GanttViewElement.BindingProvider.IsDataBound)
        throw new ArgumentException(string.Format("Cannot add or insert the item '{0}' in more than one place. You must first remove it from its current location or clone it. Parameter name: {0}", (object) item.Title));
      if (this.owner.GanttViewElement != null)
      {
        if (!this.owner.GanttViewElement.PreProcess(this.owner, item, (object) "Insert", (object) index))
          return;
      }
      base.InsertItem(index, item);
      item.Parent = this.owner;
      if (!item.Selected || this.GanttViewElement == null)
        return;
      this.GanttViewElement.ProcessSelection(item);
    }

    protected override void SetItem(int index, GanttViewDataItem item)
    {
      if (this.owner.GanttViewElement != null)
      {
        if (!this.owner.GanttViewElement.PreProcess(this.owner, item, (object) nameof (SetItem), (object) index))
          return;
      }
      base.SetItem(index, item);
      item.Parent = this.owner;
    }

    protected override void RemoveItem(int index)
    {
      GanttViewDataItem parent1 = this.Items[index];
      if (parent1.Current && this.GanttViewElement != null)
      {
        RadGanttViewElement ganttViewElement = this.GanttViewElement;
        GanttViewDataItem ganttViewDataItem1 = parent1.PrevVisibleItem;
        if (ganttViewDataItem1 == null)
        {
          for (GanttViewDataItem ganttViewDataItem2 = parent1; ganttViewDataItem2 != null && ganttViewDataItem1 == null; ganttViewDataItem2 = ganttViewDataItem2.Parent)
            ganttViewDataItem1 = ganttViewDataItem2.NextItem;
        }
        ganttViewElement.SelectedItem = ganttViewDataItem1;
        if (ganttViewElement.SelectedItem != ganttViewDataItem1)
        {
          if (ganttViewDataItem1.Enabled)
            return;
          base.RemoveItem(index);
          return;
        }
      }
      if (this.owner.GanttViewElement != null)
      {
        if (!this.owner.GanttViewElement.PreProcess(this.owner, parent1, (object) "Remove", (object) index))
          return;
      }
      this.GanttViewElement.BeginUpdate();
      if (this.owner.GanttViewElement != null)
      {
        while (parent1.Items.Count > 0)
        {
          GanttViewDataItem ganttViewDataItem = parent1.Items[0];
          this.owner.GanttViewElement.PreProcess(parent1, ganttViewDataItem, (object) "Remove", (object) 0);
        }
      }
      this.GanttViewElement.EndUpdate(false, RadGanttViewElement.UpdateActions.None);
      GanttViewDataItem parent2 = parent1.Parent;
      base.RemoveItem(index);
      parent1.Parent = (GanttViewDataItem) null;
      parent1.GanttViewElement = (RadGanttViewElement) null;
      if (parent2 == null || parent2.items.Count != 0 || this.owner.GanttViewElement == null)
        return;
      this.owner.GanttViewElement.Update(RadGanttViewElement.UpdateActions.ItemStateChanged, parent2);
    }

    protected override void ClearItems()
    {
      RadGanttViewElement ganttViewElement = this.owner.GanttViewElement;
      if (ganttViewElement != null)
      {
        if (!ganttViewElement.PreProcess(this.owner, (GanttViewDataItem) null, (object) "Clear"))
          return;
      }
      foreach (GanttViewDataItem ganttViewDataItem in (IEnumerable<GanttViewDataItem>) this.Items)
      {
        ganttViewDataItem.parent = (GanttViewDataItem) null;
        ganttViewDataItem.GanttViewElement = (RadGanttViewElement) null;
      }
      base.ClearItems();
    }

    protected internal virtual void OnCollectionItemChanged(GanttViewDataItem item)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) item));
    }

    protected internal virtual void OnCollectionItemChanging(GanttViewDataItem item)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanging, (object) item));
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      base.OnCollectionChanged(args);
      if (this.Suspended || this.owner == null)
        return;
      RadGanttViewElement ganttViewElement = this.owner.GanttViewElement;
      if (ganttViewElement == null)
        return;
      if (args.Action == NotifyCollectionChangedAction.Add)
      {
        GanttViewDataItem newItem = (GanttViewDataItem) args.NewItems[0];
        ganttViewElement.Update(RadGanttViewElement.UpdateActions.ItemAdded, newItem);
        ganttViewElement.OnItemAdded(new GanttViewItemAddedEventArgs(newItem));
      }
      else if (args.Action == NotifyCollectionChangedAction.Remove)
      {
        GanttViewDataItem newItem = (GanttViewDataItem) args.NewItems[0];
        ganttViewElement.Update(RadGanttViewElement.UpdateActions.ItemRemoved, newItem);
        ganttViewElement.OnItemRemoved(new GanttViewItemRemovedEventArgs(newItem));
      }
      else if (args.Action == NotifyCollectionChangedAction.Move)
      {
        GanttViewDataItem newItem = (GanttViewDataItem) args.NewItems[0];
        ganttViewElement.Update(RadGanttViewElement.UpdateActions.ItemMoved, newItem);
      }
      else
      {
        if (args.Action != NotifyCollectionChangedAction.Reset)
          return;
        RadGanttViewElement.UpdateActions updateAction = this.owner is RadGanttViewElement.RootDataItem ? RadGanttViewElement.UpdateActions.Reset : RadGanttViewElement.UpdateActions.Resume;
        ganttViewElement.Update(updateAction);
      }
    }

    protected internal bool NeedsRefresh
    {
      get
      {
        int tasksVersion = this.owner.GanttViewElement.BindingProvider.GetTasksVersion();
        if (tasksVersion < 0)
          return false;
        return this.version != tasksVersion;
      }
    }

    protected internal void SyncVersion()
    {
      this.version = this.owner.GanttViewElement.BindingProvider.GetTasksVersion();
    }

    protected internal void ResetVersion()
    {
      this.version = -1;
    }

    protected internal bool IsEmpty
    {
      get
      {
        return ((GanttViewDataItemView) this.Items).IsEmpty;
      }
    }
  }
}
