// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSelectedRowsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSelectedRowsCollection : ReadOnlyObservableCollection<GridViewRowInfo>
  {
    private MasterGridViewTemplate masterTemplate;
    private int update;

    public GridViewSelectedRowsCollection()
      : this((MasterGridViewTemplate) null)
    {
    }

    public GridViewSelectedRowsCollection(MasterGridViewTemplate masterTemplate)
      : base(new ObservableCollection<GridViewRowInfo>())
    {
      this.masterTemplate = masterTemplate;
      this.ObservableItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ObservableItems_CollectionChanged);
    }

    private ObservableCollection<GridViewRowInfo> ObservableItems
    {
      get
      {
        return this.Items as ObservableCollection<GridViewRowInfo>;
      }
    }

    internal void Add(GridViewRowInfo item, bool checkIfExists)
    {
      if (this.masterTemplate == null)
        throw new InvalidOperationException("The instance must have template.", (Exception) new NullReferenceException("MasterTemplate instance cannot be null."));
      if (this.masterTemplate.SelectionMode != GridViewSelectionMode.FullRowSelect || checkIfExists && this.Contains(item))
        return;
      this.Items.Add(item);
      item.IsSelected = true;
    }

    internal void Remove(GridViewRowInfo item)
    {
      this.Items.Remove(item);
      item.IsSelected = false;
    }

    internal void Clear()
    {
      this.Clear(false);
    }

    internal void Clear(bool suspendNotifications)
    {
      Queue<GridViewInfo> affectedViews = new Queue<GridViewInfo>();
      this.Clear(suspendNotifications, out affectedViews);
    }

    internal void Clear(out Queue<GridViewInfo> affectedViews)
    {
      this.Clear(this.update > 0, out affectedViews);
    }

    internal void Clear(bool suspendNotifications, out Queue<GridViewInfo> affectedViews)
    {
      affectedViews = new Queue<GridViewInfo>();
      for (int index = this.Count - 1; index >= 0; --index)
      {
        GridViewRowInfo row = this.Items[index];
        if (suspendNotifications)
          row.SuspendPropertyNotifications();
        row.IsSelected = false;
        if (!this.masterTemplate.ListSource.IsUpdating && this.masterTemplate.ListSource.Count > 0)
          GridViewSelectedRowsCollection.InvalidateConditionalFormatting(row);
        if (suspendNotifications)
          row.ResumePropertyNotifications();
        if (suspendNotifications && !affectedViews.Contains(row.ViewInfo))
          affectedViews.Enqueue(row.ViewInfo);
      }
      this.Items.Clear();
    }

    private static void InvalidateConditionalFormatting(GridViewRowInfo row)
    {
      for (int index = 0; index < row.ViewTemplate.Columns.Count; ++index)
      {
        if (row.ViewTemplate.Columns[index].ConditionalFormattingObjectList.Count > 0)
        {
          if (!row.IsAttached)
            break;
          row.InvalidateRow();
          break;
        }
      }
    }

    public void BeginUpdate()
    {
      this.ObservableItems.BeginUpdate();
      ++this.update;
    }

    public void EndUpdate(bool notifyUpdates)
    {
      --this.update;
      this.ObservableItems.EndUpdate(notifyUpdates);
    }

    private void ObservableItems_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.RaiseSelectionChanged(true);
    }

    private void RaiseSelectionChanged(bool nofityUpdates)
    {
      if (this.masterTemplate == null || !nofityUpdates || this.update != 0)
        return;
      this.masterTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.SelectionChanged, (object) this, EventArgs.Empty);
    }

    public GridViewRowInfo[] ToArray()
    {
      GridViewRowInfo[] gridViewRowInfoArray = new GridViewRowInfo[this.Count];
      for (int index = 0; index < this.Count; ++index)
        gridViewRowInfoArray[index] = this[index];
      return gridViewRowInfoArray;
    }
  }
}
