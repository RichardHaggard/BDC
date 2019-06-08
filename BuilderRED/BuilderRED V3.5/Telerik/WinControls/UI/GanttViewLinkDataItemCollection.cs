// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewLinkDataItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GanttViewLinkDataItemCollection : NotifyCollection<GanttViewLinkDataItem>
  {
    private RadGanttViewElement ganttViewElement;
    private int version;

    public GanttViewLinkDataItemCollection(RadGanttViewElement ganttViewElement)
    {
      this.ganttViewElement = ganttViewElement;
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttViewElement;
      }
    }

    public void Refresh()
    {
      if (this.GanttViewElement == null)
        return;
      ++this.version;
      this.GanttViewElement.Update(RadGanttViewElement.UpdateActions.Resume);
    }

    protected internal bool NeedsRefresh
    {
      get
      {
        int tasksVersion = this.GanttViewElement.BindingProvider.GetTasksVersion();
        if (tasksVersion < 0)
          return false;
        return this.version != tasksVersion;
      }
    }

    protected internal void SyncVersion()
    {
      this.version = this.GanttViewElement.BindingProvider.GetTasksVersion();
    }

    protected internal void ResetVersion()
    {
      this.version = -1;
    }

    protected override void InsertItem(int index, GanttViewLinkDataItem item)
    {
      if (item.GanttViewElement != null && !this.GanttViewElement.BindingProvider.IsDataBound)
        throw new ArgumentException(string.Format("Cannot add or insert the item in more than one place. You must first remove it from its current location or clone it. Link parameters: Start {0}, End {1}, Type {2}", (object) item.StartItem.Title, (object) item.EndItem.Title, (object) item.LinkType));
      if (this.GanttViewElement != null)
      {
        if (!this.GanttViewElement.PreProcess(item, (object) "Insert", (object) index))
          return;
      }
      item.GanttViewElement = this.GanttViewElement;
      base.InsertItem(index, item);
    }

    protected override void SetItem(int index, GanttViewLinkDataItem item)
    {
      if (this.GanttViewElement != null)
      {
        if (!this.GanttViewElement.PreProcess(item, (object) nameof (SetItem), (object) index))
          return;
      }
      item.GanttViewElement = this.GanttViewElement;
      base.SetItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
      GanttViewLinkDataItem viewLinkDataItem = this.Items[index];
      if (this.GanttViewElement != null)
      {
        if (!this.GanttViewElement.PreProcess(viewLinkDataItem, (object) "Remove", (object) index))
          return;
      }
      if (viewLinkDataItem.Selected)
        viewLinkDataItem.Selected = false;
      viewLinkDataItem.GanttViewElement = (RadGanttViewElement) null;
      base.RemoveItem(index);
    }

    protected override void ClearItems()
    {
      RadGanttViewElement ganttViewElement = this.GanttViewElement;
      if (ganttViewElement != null)
      {
        if (!ganttViewElement.PreProcess((GanttViewLinkDataItem) null, (object) "Clear"))
          return;
      }
      foreach (GanttViewLinkDataItem viewLinkDataItem in (IEnumerable<GanttViewLinkDataItem>) this.Items)
        viewLinkDataItem.GanttViewElement = (RadGanttViewElement) null;
      base.ClearItems();
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      base.OnCollectionChanged(args);
      if (this.Suspended)
        return;
      RadGanttViewElement ganttViewElement = this.GanttViewElement;
      if (ganttViewElement == null)
        return;
      if (args.Action == NotifyCollectionChangedAction.Add)
      {
        GanttViewLinkDataItem newItem = (GanttViewLinkDataItem) args.NewItems[0];
        ganttViewElement.Update(RadGanttViewElement.UpdateActions.LinkAdded, newItem);
        ganttViewElement.OnLinkAdded(new GanttViewLinkAddedEventArgs(newItem));
      }
      else if (args.Action == NotifyCollectionChangedAction.Remove)
      {
        GanttViewLinkDataItem newItem = (GanttViewLinkDataItem) args.NewItems[0];
        ganttViewElement.Update(RadGanttViewElement.UpdateActions.LinkRemoved, newItem);
        ganttViewElement.OnLinkRemoved(new GanttViewLinkRemovedEventArgs(newItem));
      }
      else
      {
        if (args.Action != NotifyCollectionChangedAction.Reset)
          return;
        RadGanttViewElement.UpdateActions updateAction = RadGanttViewElement.UpdateActions.Reset;
        ganttViewElement.Update(updateAction);
      }
    }
  }
}
