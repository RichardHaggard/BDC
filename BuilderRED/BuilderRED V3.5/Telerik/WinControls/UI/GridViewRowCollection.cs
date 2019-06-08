// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewRowCollection : IList<GridViewRowInfo>, ICollection<GridViewRowInfo>, IEnumerable<GridViewRowInfo>, IEnumerable, INotifyCollectionChanged
  {
    private GridViewTemplate owner;
    private bool suspendNotifications;
    internal bool addingThroughUI;
    private GridViewRowInfo prevSelectedRow;

    public GridViewRowCollection(GridViewTemplate owner)
    {
      this.owner = owner;
      if (this.owner.MasterTemplate != null)
        this.prevSelectedRow = this.owner.MasterTemplate.CurrentRow;
      this.owner.ListSource.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ListSource_CollectionChanged);
    }

    private void ListSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        this.AttachRows(e.NewItems);
        if (!this.suspendNotifications)
        {
          if (this.Count <= 1)
            this.owner.DataView.EnsureDescriptors();
          this.UpdateChildRowViewInfo((GridViewRowInfo) e.NewItems[0]);
          if (((GridViewRowInfo) e.NewItems[0]).Parent is MasterGridViewTemplate)
            ++((GridViewRowInfo) e.NewItems[0]).ViewInfo.Version;
          this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.Add, e.NewItems));
          if (this.owner.SelectLastAddedRow && e.NewItems.Count > 0)
          {
            GridViewSynchronizationService.RaiseCurrentChanged(this.owner, this.owner.DataView.CurrentItem, this.owner.CurrentColumn, this.owner.DataView == null);
            if (!this.addingThroughUI)
            {
              if (this.prevSelectedRow != null)
              {
                GridViewSynchronizationService.SuspendEvent(this.owner, KnownEvents.CurrentChanged);
                this.prevSelectedRow.IsCurrent = false;
                this.prevSelectedRow.IsSelected = false;
                GridViewSynchronizationService.ResumeEvent(this.owner, KnownEvents.CurrentChanged);
              }
              GridViewRowInfo newItem = (GridViewRowInfo) e.NewItems[0];
              newItem.IsCurrent = true;
              newItem.IsSelected = true;
              this.prevSelectedRow = newItem;
              newItem.EnsureVisible();
            }
          }
          if (this.owner.IsSelfReference)
            this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.SortingChanged));
        }
      }
      GridViewInfo gridViewInfo = (GridViewInfo) null;
      GridViewRowInfo gridViewRowInfo = (GridViewRowInfo) null;
      bool flag1 = false;
      if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        GridViewRowInfo newItem = e.NewItems[0] as GridViewRowInfo;
        flag1 = newItem.IsPinned;
        if (newItem != null && newItem.ViewTemplate != null && newItem.ViewTemplate.HierarchyDataProvider != null)
        {
          int val1 = this.owner.MasterTemplate.Owner.TableElement.RowScroller.Scrollbar.Value;
          newItem.ViewTemplate.HierarchyDataProvider.Refresh();
          newItem.ViewTemplate.Refresh();
          RadScrollBarElement scrollbar = this.owner.MasterTemplate.Owner.TableElement.RowScroller.Scrollbar;
          scrollbar.Value = Math.Min(val1, scrollbar.Maximum - scrollbar.LargeChange + 1);
        }
        if (newItem != null && newItem.IsCurrent && (this.owner.Parent != null && !this.suspendNotifications))
        {
          gridViewInfo = newItem.ViewInfo;
          gridViewRowInfo = GridViewRowCollection.NavigateAfterRemove(newItem, (List<GridViewRowInfo>) null);
        }
        if (!this.suspendNotifications)
          this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.Remove, e.NewItems));
      }
      this.OnCollectionChanged(e);
      if (gridViewInfo != null)
      {
        GridViewRelationDataProvider hierarchyDataProvider = gridViewInfo.ViewTemplate.HierarchyDataProvider as GridViewRelationDataProvider;
        if (hierarchyDataProvider != null && this.owner.MasterTemplate.SynchronizationService.IsDispatchSuspended)
          hierarchyDataProvider.Refresh();
        gridViewInfo.Refresh();
      }
      GridViewCollectionChangedEventArgs args = new GridViewCollectionChangedEventArgs(this.owner, e.Action, e.NewItems, e.OldItems, e.NewStartingIndex, e.OldStartingIndex, e.PropertyName);
      bool flag2 = true;
      if (e.Action == NotifyCollectionChangedAction.ItemChanging || e.Action == NotifyCollectionChangedAction.ItemChanged)
        flag2 = !this.owner.ListSource.IsDataBound || e.Action == NotifyCollectionChangedAction.ItemChanged && !string.IsNullOrEmpty(e.PropertyName);
      if (flag2)
      {
        this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangedEventArgs>(EventDispatcher.RowsChanged, (object) this, args);
        this.UpdateHierarchyView(e.NewItems, e.PropertyName);
      }
      if (flag1)
        this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.Remove), GridEventType.UI);
      if (gridViewInfo == null || this.owner.MasterTemplate == null)
        return;
      if (this.owner.MasterTemplate.SynchronizationService.IsDispatchSuspended)
      {
        if (gridViewRowInfo == null)
          return;
        this.owner.MasterTemplate.CurrentRowToSetOnEndUpdate = gridViewRowInfo;
      }
      else
      {
        if (!flag1)
          this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.Remove), GridEventType.UI);
        if (gridViewRowInfo == null)
          return;
        this.owner.MasterTemplate.CurrentRow = gridViewRowInfo;
      }
    }

    private void UpdateHierarchyView(IList list, string propertyName)
    {
      if (list == null || list.Count <= 0)
        return;
      GridViewHierarchyRowInfo hierarchyRowInfo = list[0] as GridViewHierarchyRowInfo;
      if (hierarchyRowInfo == null || hierarchyRowInfo.ViewTemplate == null || hierarchyRowInfo.ViewTemplate.HierarchyDataProvider == null || !hierarchyRowInfo.ViewTemplate.HierarchyDataProvider.Relation.ChildColumnNames.Contains(propertyName) && !hierarchyRowInfo.ViewTemplate.HierarchyDataProvider.Relation.ParentColumnNames.Contains(propertyName))
        return;
      hierarchyRowInfo.ViewTemplate.HierarchyDataProvider.Refresh();
      hierarchyRowInfo.ViewTemplate.Refresh();
    }

    private void UpdateChildRowViewInfo(GridViewRowInfo newRow)
    {
      GridViewTemplate parent = this.owner.Parent;
      if (parent == null)
        return;
      GridViewRelationDataProvider hierarchyDataProvider = this.owner.HierarchyDataProvider as GridViewRelationDataProvider;
      if (hierarchyDataProvider == null)
        return;
      hierarchyDataProvider.Refresh();
      foreach (GridViewRowInfo childRow in parent.ChildRows)
      {
        bool flag = true;
        for (int index = 0; index < hierarchyDataProvider.Relation.ParentColumnNames.Count; ++index)
        {
          if (!object.Equals(childRow.Cells[hierarchyDataProvider.Relation.ParentColumnNames[index]].Value, newRow.Cells[hierarchyDataProvider.Relation.ChildColumnNames[index]].Value))
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          using (IEnumerator<GridViewInfo> enumerator = ((GridViewHierarchyRowInfo) childRow).Views.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              GridViewInfo current = enumerator.Current;
              if (current.ViewTemplate == this.owner)
              {
                newRow.ViewInfo = current;
                break;
              }
            }
            break;
          }
        }
      }
    }

    private void AttachRows(IList rows)
    {
      foreach (GridViewRowInfo row in (IEnumerable) rows)
        row.Attach();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewTemplate Owner
    {
      get
      {
        return this.owner;
      }
    }

    public virtual void AddRange(params GridViewRowInfo[] rows)
    {
      if (this.owner.IsVirtualRows)
        return;
      this.owner.BeginUpdate();
      for (int index = 0; index < rows.Length; ++index)
        this.Add(rows[index]);
      this.owner.EndUpdate();
    }

    public int Add(params object[] values)
    {
      if (this.owner.IsVirtualRows)
        return -1;
      GridViewRowInfo gridViewRowInfo1 = values[0] as GridViewRowInfo;
      if (gridViewRowInfo1 != null)
      {
        this.Add(gridViewRowInfo1);
        return gridViewRowInfo1.Index;
      }
      if (this.OnRowsChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Add, (object) null, this.Count, -1)))
        return -1;
      bool notifyUpdates = this.Count == 0;
      this.owner.ListSource.BeginUpdate();
      GridViewRowInfo gridViewRowInfo2 = this.owner.ListSource.AddNew();
      IEditableObject dataBoundItem = gridViewRowInfo2.DataBoundItem as IEditableObject;
      dataBoundItem?.BeginEdit();
      int num = Math.Min(this.owner.Columns.Count, values.Length);
      for (int index = 0; index < num; ++index)
      {
        GridViewDataColumn column = this.owner.Columns[index];
        gridViewRowInfo2[(GridViewColumn) column] = RadDataConverter.Instance.Parse((IDataConversionInfoProvider) column, values[index]);
      }
      dataBoundItem?.EndEdit();
      this.owner.ListSource.EndUpdate(notifyUpdates);
      ((ICancelAddNew) this.owner.ListSource).EndNew(this.owner.ListSource.Count - 1);
      gridViewRowInfo2.Attach();
      if (this.owner.DataSource == null && this.owner.SortDescriptors.Count > 0)
      {
        this.owner.SortDescriptors.BeginUpdate();
        this.owner.SortDescriptors.EndUpdate(true);
      }
      return gridViewRowInfo2.Index;
    }

    public GridViewRowInfo AddNew()
    {
      if (this.owner.IsVirtualRows)
        return (GridViewRowInfo) null;
      if (this.OnRowsChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Add, (object) null, this.Count, -1)))
        return (GridViewRowInfo) null;
      int count = this.Count;
      this.owner.ListSource.BeginUpdate();
      GridViewRowInfo gridViewRowInfo = this.owner.ListSource.AddNew();
      this.owner.ListSource.EndUpdate(false);
      ((ICancelAddNew) this.owner.ListSource).EndNew(this.owner.ListSource.Count - 1);
      if (count != this.Count)
        gridViewRowInfo.Attach();
      return gridViewRowInfo;
    }

    public GridViewRowInfo NewRow()
    {
      return ((IDataItemSource) this.owner).NewItem() as GridViewRowInfo;
    }

    public void Move(int oldIndex, int newIndex)
    {
      if (this.owner.IsVirtualRows)
        return;
      try
      {
        this.owner.ListSource.Move(oldIndex, newIndex);
      }
      catch (Exception ex)
      {
        this.owner.MasterTemplate.SetError(new GridViewCellCancelEventArgs(this[oldIndex], (GridViewColumn) null, (IInputEditor) null), ex);
      }
    }

    internal void UnwireEvents()
    {
      this.owner.ListSource.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.ListSource_CollectionChanged);
    }

    private void DispatchDataViewChangedEvent(DataViewChangedEventArgs args)
    {
      this.DispatchDataViewChangedEvent(args, GridEventType.Both);
    }

    private void DispatchDataViewChangedEvent(DataViewChangedEventArgs args, GridEventType type)
    {
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.ViewChanged, type, GridEventDispatchMode.Send);
      GridViewSynchronizationService.DispatchEvent(this.owner, new GridViewEvent((object) this.owner, (object) this.owner, new object[1]
      {
        (object) args
      }, eventInfo), false);
    }

    private GridViewRowInfo GetPrev(GridViewRowInfo row)
    {
      IHierarchicalRow hierarchicalRow1 = (IHierarchicalRow) row;
      IHierarchicalRow hierarchicalRow2 = row.Parent ?? (IHierarchicalRow) row.ViewTemplate;
      Stack<IHierarchicalRow> hierarchicalRowStack = new Stack<IHierarchicalRow>();
      hierarchicalRowStack.Push(hierarchicalRow2);
      while (hierarchicalRowStack.Count > 0)
      {
        IHierarchicalRow hierarchicalRow3 = hierarchicalRowStack.Pop();
        if (hierarchicalRow3 == null)
          return (GridViewRowInfo) null;
        if (hierarchicalRow3.ChildRows.Count > 1)
        {
          int num = hierarchicalRow3.ChildRows.IndexOf((GridViewRowInfo) hierarchicalRow1);
          if (num == 0)
            return (GridViewRowInfo) null;
          GridViewGroupRowInfo viewGroupRowInfo = hierarchicalRow3.ChildRows[num - 1] as GridViewGroupRowInfo;
          if (viewGroupRowInfo == null)
            return hierarchicalRow3.ChildRows[num - 1];
          while (viewGroupRowInfo.Group.Groups.Count > 0)
            viewGroupRowInfo = viewGroupRowInfo.Group.Groups[viewGroupRowInfo.Group.Groups.Count - 1].GroupRow;
          return viewGroupRowInfo.Group[viewGroupRowInfo.Group.ItemCount - 1];
        }
        hierarchicalRow1 = hierarchicalRow3;
        hierarchicalRowStack.Push(hierarchicalRow3.Parent);
      }
      return (GridViewRowInfo) null;
    }

    private GridViewRowInfo GetNext(GridViewRowInfo row)
    {
      IHierarchicalRow current1 = (IHierarchicalRow) row;
      IHierarchicalRow parent1 = row.Parent ?? (IHierarchicalRow) row.ViewTemplate;
      Stack<GridViewRowCollection.Navigator> navigatorStack = new Stack<GridViewRowCollection.Navigator>();
      GridViewRowCollection.Navigator navigator1 = new GridViewRowCollection.Navigator(parent1, current1);
      navigatorStack.Push(navigator1);
      while (navigatorStack.Count > 0)
      {
        GridViewRowCollection.Navigator navigator2 = navigatorStack.Pop();
        IHierarchicalRow parent2 = navigator2.Parent;
        IHierarchicalRow current2 = navigator2.Current;
        if (parent2 == null)
          return (GridViewRowInfo) null;
        if (parent2.ChildRows.Count > 1)
        {
          int num = parent2.ChildRows.IndexOf((GridViewRowInfo) current2);
          if (num == parent2.ChildRows.Count - 1)
            return (GridViewRowInfo) null;
          GridViewGroupRowInfo viewGroupRowInfo = parent2.ChildRows[num + 1] as GridViewGroupRowInfo;
          if (viewGroupRowInfo == null)
            return parent2.ChildRows[num + 1];
          while (viewGroupRowInfo.Group.Groups.Count > 0)
            viewGroupRowInfo = viewGroupRowInfo.Group.Groups[0].GroupRow;
          return viewGroupRowInfo.Group[0];
        }
        GridViewRowCollection.Navigator navigator3 = new GridViewRowCollection.Navigator(parent2.Parent, parent2);
        navigatorStack.Push(navigator3);
      }
      return (GridViewRowInfo) null;
    }

    private void SetCurrent(GridViewRowInfo row)
    {
    }

    private bool OnRowsChanging(GridViewCollectionChangingEventArgs e)
    {
      if (this.owner.IsUpdating)
        return false;
      if (e.Cancel)
        return e.Cancel;
      this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.RowsChanging, (object) this.owner, e);
      return e.Cancel;
    }

    public void Add(GridViewRowInfo item)
    {
      if (this.owner.IsVirtualRows)
        return;
      try
      {
        if (this.OnRowsChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Add, (object) item, this.Count, -1)))
          return;
        this.owner.ListSource.Add(item);
        item.Attach();
      }
      catch (InvalidOperationException ex)
      {
        throw new InvalidOperationException("Rows cannot be programmatically added to the RadGridView's rows collection when the control is data-bound..", (Exception) ex);
      }
    }

    public void Clear()
    {
      if (this.owner.IsVirtualRows)
        return;
      try
      {
        if (this.OnRowsChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Reset, (object) null, -1, -1)))
          return;
        this.owner.MasterViewInfo.PinnedRows.Clear();
        this.owner.ListSource.Clear();
      }
      catch (ArgumentException ex)
      {
        this.Owner.SetError(new GridViewCellCancelEventArgs((GridCellElement) null, (IInputEditor) null), (Exception) ex);
      }
    }

    public bool Contains(GridViewRowInfo item)
    {
      return this.owner.ListSource.Contains(item);
    }

    public void CopyTo(GridViewRowInfo[] array, int arrayIndex)
    {
      this.owner.ListSource.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.owner.ListSource.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.owner.ListSource.IsReadOnly;
      }
    }

    public bool Remove(GridViewRowInfo item)
    {
      return this.Remove(item, true);
    }

    internal bool Remove(GridViewRowInfo item, bool selectCurrentRow)
    {
      if (this.owner.IsVirtualRows)
        return false;
      int index = this.IndexOf(item);
      if (index < 0)
        return false;
      int count = this.Count;
      this.RemoveAt(index, selectCurrentRow);
      return count != this.Count;
    }

    public IEnumerator<GridViewRowInfo> GetEnumerator()
    {
      return this.owner.ListSource.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public int IndexOf(GridViewRowInfo item)
    {
      return this.owner.ListSource.IndexOf(item);
    }

    public void Insert(int index, GridViewRowInfo item)
    {
      if (this.owner.IsVirtualRows)
        return;
      try
      {
        if (this.OnRowsChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Add, (object) item, index, -1)))
          return;
        this.owner.ListSource.Insert(index, item);
        item.Attach();
      }
      catch (InvalidOperationException ex)
      {
        throw new InvalidOperationException("Rows cannot be programmatically added to the RadGridView's rows collection when the control is data-bound..", (Exception) ex);
      }
    }

    public void RemoveAt(int index)
    {
      this.RemoveAt(index, true);
    }

    private void RemoveAt(int index, bool selectCurrentRow)
    {
      if (index < 0 || index >= this.Count)
        throw new IndexOutOfRangeException("Invalid Index.");
      GridViewRowInfo gridViewRowInfo = this[index];
      GridViewInfo viewInfo = gridViewRowInfo.ViewInfo;
      if (this.OnRowsChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Remove, (object) gridViewRowInfo, -1, index)))
        return;
      int count = this.Count;
      GridViewSynchronizationService.SuspendEvent(this.owner, KnownEvents.CurrentChanged);
      GridViewRowInfo navRow = (GridViewRowInfo) null;
      bool flag = selectCurrentRow && this.owner.MasterTemplate != null && gridViewRowInfo == this.owner.MasterTemplate.CurrentRow;
      if (flag)
        navRow = GridViewRowCollection.NavigateAfterRemove(this.owner.MasterTemplate.CurrentRow, (List<GridViewRowInfo>) null);
      this.suspendNotifications = true;
      gridViewRowInfo.ViewInfo.PinnedRows.Remove(gridViewRowInfo);
      this.owner.ListSource.RemoveAt(index);
      this.suspendNotifications = false;
      GridViewSynchronizationService.ResumeEvent(this.owner, KnownEvents.CurrentChanged);
      if (count != this.Count)
      {
        this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.Remove, (object) gridViewRowInfo));
        if (flag)
        {
          if (navRow == null && this.Owner.AllowAddNewRow)
            navRow = (GridViewRowInfo) viewInfo.TableAddNewRow;
          if (navRow != null && navRow.ViewInfo != null)
          {
            this.ExpandRow(navRow);
            this.owner.MasterTemplate.CurrentRow = navRow;
          }
          if (this.Owner.MasterTemplate.CurrentRow == null)
            this.Owner.MasterTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.SelectionChanged, (object) this.Owner, EventArgs.Empty);
        }
        gridViewRowInfo.Detach();
      }
      else
      {
        gridViewRowInfo.Attach();
        if (!gridViewRowInfo.IsPinned)
          return;
        viewInfo.PinnedRows.Add(gridViewRowInfo);
      }
    }

    private void ExpandRow(GridViewRowInfo navRow)
    {
      if (navRow == null)
        return;
      GridViewRowInfo parent = navRow.Parent as GridViewRowInfo;
      if (parent == null)
        return;
      parent.IsExpanded = true;
    }

    private static GridViewRowInfo ChangeTheChildViewAfterRemove(
      GridViewRowInfo startFrom)
    {
      if (startFrom.ViewInfo.ChildRows.Count == 1 && startFrom.ViewInfo.ParentRow != null)
      {
        foreach (GridViewInfo view in (IEnumerable<GridViewInfo>) startFrom.ViewInfo.ParentRow.Views)
        {
          if (view != startFrom.ViewInfo.ParentRow.ActiveView && view.ChildRows.Count > 0)
          {
            foreach (GridViewRowInfo childRow in view.ChildRows)
            {
              if (childRow.CanBeCurrent && childRow.IsVisible)
                return childRow;
            }
          }
        }
      }
      return (GridViewRowInfo) null;
    }

    internal static GridViewRowInfo NavigateAfterRemove(
      GridViewRowInfo startFrom,
      List<GridViewRowInfo> notAllowedRows)
    {
      GridViewRowInfo row = startFrom;
      GridTraverser gridTraverser = new GridTraverser(row.ViewInfo);
      gridTraverser.GoToRow(row);
      GridTraverser.GridTraverserPosition position = gridTraverser.Position;
      GridViewRowInfo newCurrentRowInfo = GridViewRowCollection.ChangeTheChildViewAfterRemove(startFrom);
      if (newCurrentRowInfo != null)
        return newCurrentRowInfo;
      while (gridTraverser.MoveNext())
      {
        if (gridTraverser.Current.CanBeCurrent && (notAllowedRows == null || !notAllowedRows.Contains(gridTraverser.Current)))
        {
          if (gridTraverser.Current is GridViewGroupRowInfo)
          {
            if (!gridTraverser.Current.IsExpanded)
              gridTraverser.Current.IsExpanded = true;
          }
          else
          {
            if (gridTraverser.Current is GridViewDataRowInfo || gridTraverser.Current is GridViewNewRowInfo)
            {
              newCurrentRowInfo = gridTraverser.Current;
              break;
            }
            break;
          }
        }
      }
      if (newCurrentRowInfo == null)
      {
        gridTraverser.Position = position;
        gridTraverser.ProcessHierarchy = true;
        while (gridTraverser.MovePrevious())
        {
          if (gridTraverser.Current.CanBeCurrent && (notAllowedRows == null || !notAllowedRows.Contains(gridTraverser.Current)))
          {
            if (gridTraverser.Current is GridViewGroupRowInfo)
            {
              if (!gridTraverser.Current.IsExpanded)
              {
                gridTraverser.Current.IsExpanded = true;
                gridTraverser.Position = position;
              }
            }
            else
            {
              if (gridTraverser.Current is GridViewDataRowInfo || gridTraverser.Current is GridViewNewRowInfo)
              {
                newCurrentRowInfo = gridTraverser.Current;
                break;
              }
              position = gridTraverser.Position;
            }
          }
        }
      }
      if (newCurrentRowInfo == null)
        return (GridViewRowInfo) null;
      if (GridViewRowCollection.HasDeletedParentRow(newCurrentRowInfo, notAllowedRows))
        newCurrentRowInfo = GridViewRowCollection.NavigateAfterRemove(newCurrentRowInfo.Parent as GridViewRowInfo, notAllowedRows);
      return newCurrentRowInfo;
    }

    private static bool HasDeletedParentRow(
      GridViewRowInfo newCurrentRowInfo,
      List<GridViewRowInfo> notAllowedRows)
    {
      if (newCurrentRowInfo.Parent == null || newCurrentRowInfo.Parent is GridViewTemplate || notAllowedRows == null)
        return false;
      foreach (GridViewRowInfo notAllowedRow in notAllowedRows)
      {
        if (notAllowedRow == newCurrentRowInfo.Parent)
          return true;
      }
      return false;
    }

    public GridViewRowInfo this[int index]
    {
      get
      {
        return this.owner.ListSource[index];
      }
      set
      {
        this.owner.ListSource[index] = value;
      }
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, args);
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
    }

    private void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      object oldItem,
      object item,
      int index)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, oldItem, item, index));
    }

    private class Navigator
    {
      public IHierarchicalRow Parent;
      public IHierarchicalRow Current;

      public Navigator(IHierarchicalRow parent, IHierarchicalRow current)
      {
        this.Parent = parent;
        this.Current = current;
      }
    }
  }
}
