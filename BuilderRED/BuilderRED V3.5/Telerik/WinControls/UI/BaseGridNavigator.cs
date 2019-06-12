// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseGridNavigator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BaseGridNavigator : IGridNavigator, IGridViewEventListener
  {
    private AnchoredPosition anchoredPosition = new AnchoredPosition((GridViewColumn) null, (GridTraverser.GridTraverserPosition) null);
    private Queue<GridViewInfo> affectedViews = new Queue<GridViewInfo>();
    private bool moveNextFromAnchorPosition;
    private bool settingCurrentPosition;
    private GridNavigationContext context;
    private RadGridViewElement gridViewElement;
    private GridTraverser enumerator;
    private GridViewColumn originalColumn;

    public virtual void Initialize(RadGridViewElement element)
    {
      this.gridViewElement = element;
      this.enumerator = new GridTraverser((IHierarchicalRow) this.MasterTemplate);
      this.enumerator.ProcessHierarchy = true;
      if (this.gridViewElement.Template.SynchronizationService.ContainsListener((IGridViewEventListener) this))
        return;
      this.gridViewElement.Template.SynchronizationService.AddListener((IGridViewEventListener) this);
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    public MasterGridViewTemplate MasterTemplate
    {
      get
      {
        return this.gridViewElement.Template;
      }
    }

    protected GridViewInfo ViewInfo
    {
      get
      {
        return this.MasterTemplate.CurrentView;
      }
    }

    protected GridViewTemplate ViewTemplate
    {
      get
      {
        return this.ViewInfo.ViewTemplate;
      }
    }

    protected IList<GridViewColumn> Columns
    {
      get
      {
        return (this.GridViewElement.CurrentView as GridTableElement)?.ViewElement.RowLayout.RenderColumns;
      }
    }

    protected GridViewRowInfo CurrentRow
    {
      get
      {
        return this.MasterTemplate.CurrentRow;
      }
    }

    protected GridViewColumn CurrentColumn
    {
      get
      {
        return this.ViewTemplate.CurrentColumn;
      }
    }

    protected int CurrentColumnIndex
    {
      get
      {
        return this.Columns.IndexOf(this.CurrentColumn);
      }
    }

    protected bool IsMouseSelection
    {
      get
      {
        if (this.context != null)
          return this.context.InputType == GridNavigationInputType.Mouse;
        return false;
      }
    }

    protected bool IsRightMouseButtonClicked
    {
      get
      {
        if (this.context != null && this.context.InputType == GridNavigationInputType.Mouse)
          return this.context.MouseButtons == MouseButtons.Right;
        return false;
      }
    }

    protected bool IsShiftButtonPressed
    {
      get
      {
        if (this.context != null && (this.context.ModifierKeys & Keys.Shift) == Keys.Shift)
          return (this.context.ModifierKeys & Keys.Tab) != Keys.Tab;
        return false;
      }
    }

    protected bool IsControlButtonPressed
    {
      get
      {
        if (this.context != null)
          return (this.context.ModifierKeys & Keys.Control) == Keys.Control;
        return false;
      }
    }

    protected bool RightToLeft
    {
      get
      {
        return this.GridViewElement.RightToLeft;
      }
    }

    protected GridNavigationContext Context
    {
      get
      {
        return this.context;
      }
    }

    protected AnchoredPosition AnchoredPosition
    {
      get
      {
        return this.anchoredPosition;
      }
    }

    protected GridTraverser Traverser
    {
      get
      {
        return this.enumerator;
      }
    }

    public virtual void SelectAll()
    {
      if (!this.MasterTemplate.MultiSelect)
        return;
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      GridViewRowInfoEnumerator rowInfoEnumerator = new GridViewRowInfoEnumerator((IHierarchicalRow) this.GridViewElement.Template);
      while (rowInfoEnumerator.MoveNext())
      {
        GridViewRowInfo current = rowInfoEnumerator.Current;
        if (current.CanBeSelected)
          gridViewRowInfoList.Add(current);
      }
      GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs((IEnumerable<GridViewRowInfo>) gridViewRowInfoList, (GridViewColumn) null);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
      if (args.Cancel)
        return;
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
        this.SelectAllRows();
      else
        this.SelectAllCells();
    }

    public virtual void ClearSelection()
    {
      bool notifyUpdates = this.MasterTemplate.SelectedRows.Count != 0 || this.MasterTemplate.SelectedCells.Count != 0;
      GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs((GridCellElement) null);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
      if (args.Cancel)
        return;
      this.MasterTemplate.SelectedRows.BeginUpdate();
      this.MasterTemplate.SelectedRows.Clear(true);
      this.MasterTemplate.SelectedRows.EndUpdate(false);
      this.MasterTemplate.SelectedCells.BeginUpdate();
      this.MasterTemplate.SelectedCells.Clear();
      this.MasterTemplate.SelectedCells.EndUpdate(notifyUpdates);
      if (!notifyUpdates)
        return;
      foreach (GridRowElement enumDescendant in this.GridViewElement.TableElement.EnumDescendants(new Predicate<RadElement>(this.IsRowElement), TreeTraversalMode.BreadthFirst))
        enumDescendant.IsSelected = false;
      this.GridViewElement.TableElement.ViewElement.UpdateRows();
    }

    private bool IsRowElement(RadElement element)
    {
      if (element is GridRowElement)
        return !(element is GridDetailViewRowElement);
      return false;
    }

    public virtual bool DeleteSelectedRows()
    {
      List<GridViewRowInfo> rows = (List<GridViewRowInfo>) null;
      if (this.MasterTemplate.MultiSelect)
      {
        GridViewSelectionMode selectionMode = this.MasterTemplate.SelectionMode;
        if (selectionMode == GridViewSelectionMode.FullRowSelect && this.MasterTemplate.SelectedRows.Count > 0)
        {
          rows = new List<GridViewRowInfo>();
          rows.AddRange((IEnumerable<GridViewRowInfo>) this.MasterTemplate.SelectedRows);
        }
        else if (selectionMode == GridViewSelectionMode.CellSelect && this.MasterTemplate.SelectedCells.Count > 0)
          rows = new List<GridViewRowInfo>(this.MasterTemplate.SelectedCells.DistinctRows);
      }
      if (rows == null)
      {
        if (this.MasterTemplate.CurrentRow == null)
          return false;
        rows = new List<GridViewRowInfo>();
        rows.Add(this.MasterTemplate.CurrentRow);
      }
      GridViewRowCancelEventArgs args1 = new GridViewRowCancelEventArgs(rows.ToArray());
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewRowCancelEventArgs>(EventDispatcher.UserDeletingRow, (object) this, args1);
      if (args1.Cancel)
        return false;
      if (this.GridViewElement.IsInEditMode)
        this.GridViewElement.CloseEditor();
      if (this.ViewTemplate.IsVirtualRows)
      {
        List<GridViewInfo> gridViewInfoList = new List<GridViewInfo>();
        rows.Sort((Comparison<GridViewRowInfo>) ((a, b) => b.HierarchyLevel.CompareTo(a.HierarchyLevel)));
        foreach (GridViewRowInfo row in rows)
        {
          GridViewObjectRelationalDataProvider hierarchyDataProvider = row.ViewTemplate.HierarchyDataProvider as GridViewObjectRelationalDataProvider;
          if (hierarchyDataProvider != null && row.ViewTemplate.AutoUpdateObjectRelationalSource)
          {
            hierarchyDataProvider.RemoveRow(row);
            if (!gridViewInfoList.Contains(row.ViewInfo))
              gridViewInfoList.Add(row.ViewInfo);
          }
        }
        foreach (GridViewInfo gridViewInfo in gridViewInfoList)
          gridViewInfo.Refresh();
        this.MasterTemplate.SelectedCells.BeginUpdate();
        this.MasterTemplate.SelectedCells.Clear();
        this.MasterTemplate.SelectedCells.EndUpdate(false);
        GridViewRowEventArgs args2 = new GridViewRowEventArgs(rows.ToArray());
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewRowEventArgs>(EventDispatcher.UserDeletedRow, (object) this, args2);
        if (gridViewInfoList.Count == 0)
          rows[0].ViewInfo.Refresh();
        GridViewSynchronizationService.DispatchEvent(this.ViewTemplate, new GridViewEvent((object) this.ViewTemplate, (object) this.ViewTemplate, new object[1]
        {
          (object) new DataViewChangedEventArgs(ViewChangedAction.Remove, (IList) null)
        }, new GridViewEventInfo(KnownEvents.ViewChanged, GridEventType.Both, GridEventDispatchMode.Send)), false);
        return true;
      }
      if (rows.Count > 1)
        return this.RemoveRows(rows);
      this.MasterTemplate.SelectedCells.BeginUpdate();
      this.MasterTemplate.SelectedCells.Clear();
      this.MasterTemplate.SelectedCells.EndUpdate(false);
      if (this.ViewTemplate.Rows.Remove(rows[0]))
      {
        GridViewRowEventArgs args2 = new GridViewRowEventArgs(rows[0]);
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewRowEventArgs>(EventDispatcher.UserDeletedRow, (object) this, args2);
      }
      return false;
    }

    private bool RemoveRows(List<GridViewRowInfo> rows)
    {
      int count = this.ViewTemplate.Rows.Count;
      GridViewSynchronizationService synchronizationService = this.ViewTemplate.MasterTemplate.SynchronizationService;
      GridViewRowInfo gridViewRowInfo = GridViewRowCollection.NavigateAfterRemove(this.MasterTemplate.CurrentRow, rows);
      synchronizationService.SuspendEvent(KnownEvents.ViewChanged);
      synchronizationService.SuspendDispatch(GridEventType.UI);
      Dictionary<GridViewTemplate, List<GridViewRowInfo>> dictionary = new Dictionary<GridViewTemplate, List<GridViewRowInfo>>();
      for (int index = 0; index < rows.Count; ++index)
      {
        GridViewRowInfo row = rows[index];
        GridViewTemplate viewTemplate = row.ViewTemplate;
        if (!dictionary.ContainsKey(viewTemplate))
        {
          List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
          dictionary.Add(viewTemplate, gridViewRowInfoList);
          gridViewRowInfoList.Add(row);
        }
        else
          dictionary[viewTemplate].Add(row);
        bool isCurrent = row.IsCurrent;
        if (viewTemplate.Rows.Remove(row, false) && isCurrent)
        {
          viewTemplate.MasterTemplate.EventDispatcher.SuspendEvent(EventDispatcher.CurrentRowChanging);
          viewTemplate.MasterTemplate.EventDispatcher.SuspendEvent(EventDispatcher.CurrentRowChanged);
          viewTemplate.MasterTemplate.CurrentRow = (GridViewRowInfo) null;
          viewTemplate.MasterTemplate.EventDispatcher.ResumeEvent(EventDispatcher.CurrentRowChanging);
          viewTemplate.MasterTemplate.EventDispatcher.ResumeEvent(EventDispatcher.CurrentRowChanged);
        }
      }
      synchronizationService.ResumeEvent(KnownEvents.ViewChanged);
      synchronizationService.ResumeDispatch(GridEventType.UI);
      synchronizationService.SuspendDispatch(GridEventType.Data);
      foreach (KeyValuePair<GridViewTemplate, List<GridViewRowInfo>> keyValuePair in dictionary)
      {
        GridViewTemplate key = keyValuePair.Key;
        List<GridViewRowInfo> gridViewRowInfoList = keyValuePair.Value;
        key.OnViewChanged((object) key, new DataViewChangedEventArgs(ViewChangedAction.Remove, (IList) gridViewRowInfoList));
      }
      synchronizationService.ResumeDispatch(GridEventType.Data);
      if (gridViewRowInfo != null)
      {
        GridViewRowInfo parent = gridViewRowInfo.Parent as GridViewRowInfo;
        if (parent != null)
          parent.IsExpanded = true;
        this.MasterTemplate.CurrentRow = gridViewRowInfo;
      }
      bool flag = count != this.ViewTemplate.Rows.Count;
      if (flag)
      {
        GridViewRowEventArgs args = new GridViewRowEventArgs(rows.ToArray());
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewRowEventArgs>(EventDispatcher.UserDeletedRow, (object) this, args);
      }
      return flag;
    }

    public virtual void BeginSelection(GridNavigationContext context)
    {
      this.context = context;
    }

    public virtual void EndSelection()
    {
      this.context = (GridNavigationContext) null;
      this.originalColumn = (GridViewColumn) null;
    }

    public virtual bool Select(GridViewRowInfo row, GridViewColumn column)
    {
      if ((this.settingCurrentPosition || row == null || !row.CanBeCurrent || row == this.enumerator.Current && row.IsCurrent && (column == null || column.IsCurrent)) && (this.MasterTemplate.SelectionMode != GridViewSelectionMode.FullRowSelect || this.context == null || ((this.context.ModifierKeys & Keys.Control) != Keys.None || (this.context.ModifierKeys & Keys.Shift) != Keys.None)))
        return false;
      return this.SelectOverride(row, column);
    }

    protected virtual bool SelectOverride(GridViewRowInfo row, GridViewColumn column)
    {
      this.originalColumn = column;
      if (row == null)
        return false;
      column = MasterGridViewTemplate.GetColumnAllowingForCurrent(row.ViewTemplate, column);
      if (this.originalColumn != column)
        this.GridViewElement.EditorManager.AllowEditMode = false;
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      this.enumerator.Reset();
      this.moveNextFromAnchorPosition = false;
      GridViewHierarchyRowInfo parent = row.Parent as GridViewHierarchyRowInfo;
      GridViewInfo gridViewInfo = (GridViewInfo) null;
      if (parent != null)
      {
        gridViewInfo = parent.ActiveView;
        parent.ActiveView = row.ViewInfo;
      }
      while (this.enumerator.MoveNext())
      {
        if (object.Equals((object) this.anchoredPosition.RowPosition, (object) this.enumerator.Position))
          this.moveNextFromAnchorPosition = true;
        if (this.enumerator.Current == row)
        {
          bool flag = this.SelectCore(row, column) || row.IsSelected;
          if (!flag)
            this.enumerator.Position = position;
          return flag;
        }
      }
      if (parent != null)
        parent.ActiveView = gridViewInfo;
      this.enumerator.Position = position;
      return false;
    }

    protected virtual bool SelectCore(GridViewRowInfo row, GridViewColumn column)
    {
      GridViewRowInfo currentRow = this.MasterTemplate.CurrentRow;
      GridViewColumn currentColumn = this.MasterTemplate.CurrentColumn;
      if (!GridViewSynchronizationService.IsEventSuspended((GridViewTemplate) this.MasterTemplate, KnownEvents.CurrentChanged))
      {
        this.settingCurrentPosition = true;
        GridViewSynchronizationService.RaiseCurrentChanged(this.ViewTemplate, row, column, true);
        this.settingCurrentPosition = false;
        if (this.MasterTemplate.CurrentRow != row)
          return false;
      }
      if (!this.IsControlButtonPressed)
      {
        if (!this.IsShiftButtonPressed)
          this.anchoredPosition = new AnchoredPosition(column, this.enumerator.Position);
        else if (this.anchoredPosition != null && this.anchoredPosition.Column == null)
          this.anchoredPosition.Column = column;
      }
      if (this.MasterTemplate.MultiSelect && (this.IsShiftButtonPressed || this.IsControlButtonPressed))
        return this.DoMultiSelect(currentRow, currentColumn, row, column);
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
        return this.FullRowSelect(currentRow, row);
      return this.CellSelect(row, column);
    }

    protected virtual bool CellSelect(GridViewRowInfo row, GridViewColumn column)
    {
      if (this.originalColumn is GridViewRowHeaderColumn)
      {
        this.MasterTemplate.SelectedCells.BeginUpdate();
        this.MasterTemplate.SelectedCells.Clear();
        if (!this.MasterTemplate.SelectedCells.HasSelectedCells(row.Cells))
          this.MasterTemplate.SelectedCells.AddRange(row.Cells);
        this.MasterTemplate.SelectedCells.EndUpdate(true);
        return true;
      }
      if (column == null)
        return false;
      GridViewCellInfo cell = row.Cells[column.Name];
      if (cell == null || this.MasterTemplate.SelectedCells.Count == 1 && cell.IsSelected)
        return false;
      GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs(row, column);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
      if (args.Cancel)
        return false;
      this.MasterTemplate.SelectedCells.BeginUpdate();
      if (!this.IsRightMouseButtonClicked || !cell.IsSelected)
        this.MasterTemplate.SelectedCells.Clear();
      cell.IsSelected = true;
      this.MasterTemplate.SelectedCells.EndUpdate(true);
      return true;
    }

    protected virtual bool FullRowSelect(GridViewRowInfo oldCurrentRow, GridViewRowInfo row)
    {
      if (this.MasterTemplate.SelectedRows.Count == 1 && row.IsSelected || row == null)
        return false;
      GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs(row, (GridViewColumn) null);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
      if (args.Cancel)
        return false;
      bool flag1 = !this.IsRightMouseButtonClicked || !row.IsSelected;
      this.MasterTemplate.SelectedRows.BeginUpdate();
      if (oldCurrentRow != null && oldCurrentRow != row && flag1)
        oldCurrentRow.IsSelected = false;
      Queue<GridViewRowInfo> gridViewRowInfoQueue = new Queue<GridViewRowInfo>();
      if (flag1 && this.MasterTemplate.SelectedRows.Count != 0)
      {
        if (this.GridViewElement.Template.Templates.Count > 0)
        {
          foreach (GridViewRowInfo selectedRow in (ReadOnlyCollection<GridViewRowInfo>) this.MasterTemplate.SelectedRows)
          {
            GridTableElement rowView = this.GridViewElement.GetRowView(selectedRow.ViewInfo) as GridTableElement;
            if (rowView != null)
            {
              GridRowElement rowElement = rowView.GetRowElement(selectedRow);
              if (rowElement != null && rowElement.IsSelected)
              {
                rowElement.IsSelected = false;
                gridViewRowInfoQueue.Enqueue(rowElement.Data);
              }
            }
          }
        }
        else if (this.GridViewElement != null && this.GridViewElement.TableElement != null)
        {
          foreach (RadElement visualRow in (IEnumerable<GridRowElement>) this.GridViewElement.TableElement.VisualRows)
          {
            GridRowElement gridRowElement = visualRow as GridRowElement;
            if (gridRowElement != null && gridRowElement.RowInfo != null && (gridRowElement.RowInfo.ViewInfo != null && gridRowElement.IsSelected))
            {
              gridViewRowInfoQueue.Enqueue(gridRowElement.Data);
              gridRowElement.IsSelected = false;
            }
          }
        }
        this.MasterTemplate.SelectedRows.BeginUpdate();
        this.MasterTemplate.SelectedRows.Clear(true);
        this.MasterTemplate.SelectedRows.EndUpdate(false);
        while (gridViewRowInfoQueue.Count > 0)
          gridViewRowInfoQueue.Dequeue().InvalidateRow();
      }
      row.IsSelected = true;
      bool flag2 = true;
      if (row.CanBeSelected)
        flag2 = row.IsSelected;
      else if (row.CanBeCurrent)
        flag2 = row.IsCurrent;
      this.MasterTemplate.SelectedRows.EndUpdate(true);
      return flag2;
    }

    protected virtual bool DoMultiSelect(
      GridViewRowInfo oldRow,
      GridViewColumn oldColumn,
      GridViewRowInfo row,
      GridViewColumn column)
    {
      if (!this.IsShiftButtonPressed && this.IsControlButtonPressed && !this.IsMouseSelection)
        return false;
      bool notifyUpdates = false;
      this.BeginUpdateSelectedCollection();
      if (this.IsShiftButtonPressed)
      {
        this.ClearSelectedCollection();
        notifyUpdates = this.DoMultiSelectCore();
        this.UpdateAffectedViewInfos();
      }
      else if (this.IsControlButtonPressed && this.IsMouseSelection && row.CanBeSelected)
      {
        if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
        {
          GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs(row, column);
          this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
          if (args.Cancel)
          {
            this.EndUpdateSelectedCollection(false);
            return false;
          }
          row.IsSelected = !row.IsSelected;
        }
        else
        {
          GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs(row, column);
          this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
          if (args.Cancel)
          {
            this.EndUpdateSelectedCollection(false);
            return false;
          }
          this.SetSelectedCell(row, column);
        }
        notifyUpdates = true;
        this.anchoredPosition = new AnchoredPosition(column, this.enumerator.Position);
      }
      this.EndUpdateSelectedCollection(notifyUpdates);
      return notifyUpdates;
    }

    private void UpdateAffectedViewInfos()
    {
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.BatchPropertyChanged, GridEventType.UI, GridEventDispatchMode.Send);
      GridViewSynchronizationService synchronizationService = this.MasterTemplate.SynchronizationService;
      while (this.affectedViews.Count > 0)
      {
        GridViewEvent gridEvent = new GridViewEvent((object) this.affectedViews.Dequeue(), (object) typeof (GridViewRowInfo), new object[1]{ (object) new PropertyChangedEventArgs("IsSelected") }, eventInfo);
        synchronizationService.DispatchEvent(gridEvent);
      }
    }

    protected virtual void SetSelectedCell(GridViewRowInfo row, GridViewColumn column)
    {
      if (this.originalColumn is GridViewRowHeaderColumn)
      {
        if (this.MasterTemplate.SelectedCells.HasSelectedCells(row.Cells))
          this.MasterTemplate.SelectedCells.RemoveRange(row.Cells);
        else
          this.MasterTemplate.SelectedCells.AddRange(row.Cells);
      }
      else
        row.Cells[column.Name].IsSelected = !row.Cells[column.Name].IsSelected;
    }

    private bool GetSelectedCells()
    {
      GridTraverser.GridTraverserPosition targetPostion = (GridTraverser.GridTraverserPosition) null;
      GridTraverser traverser = this.CreateTraverser(this.enumerator.Position, out targetPostion);
      int startColumnIndex = -1;
      int endColumnIndex = -1;
      this.GetColumnsBoundaryIndex(out startColumnIndex, out endColumnIndex);
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      do
      {
        GridViewRowInfo current = traverser.Current;
        if (current.CanBeSelected)
          gridViewRowInfoList.Add(current);
      }
      while (!object.Equals((object) targetPostion, (object) traverser.Position) && traverser.MoveNext());
      GridViewSelectionCancelEventArgs args = this.MasterTemplate.SelectionMode != GridViewSelectionMode.CellSelect ? new GridViewSelectionCancelEventArgs((IEnumerable<GridViewRowInfo>) gridViewRowInfoList, (GridViewColumn) null) : new GridViewSelectionCancelEventArgs((IEnumerable<GridViewRowInfo>) gridViewRowInfoList, startColumnIndex, endColumnIndex);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
      return args.Cancel;
    }

    protected virtual bool DoMultiSelectCore()
    {
      if (this.GetSelectedCells())
        return false;
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      if (this.anchoredPosition.RowPosition == position && this.anchoredPosition.Column == this.MasterTemplate.CurrentColumn)
        return false;
      GridViewSynchronizationService synchronizationService = this.MasterTemplate.SynchronizationService;
      synchronizationService.SuspendDispatch();
      GridTraverser.GridTraverserPosition targetPostion = (GridTraverser.GridTraverserPosition) null;
      GridTraverser traverser = this.CreateTraverser(position, out targetPostion);
      int startColumnIndex = -1;
      int endColumnIndex = -1;
      this.GetColumnsBoundaryIndex(out startColumnIndex, out endColumnIndex);
      do
      {
        GridViewRowInfo current = traverser.Current;
        if (current.CanBeSelected)
        {
          if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.CellSelect)
          {
            this.SetSelectedCells(current, startColumnIndex, endColumnIndex);
          }
          else
          {
            current.SuspendPropertyNotifications();
            this.MasterTemplate.SelectedRows.Add(current, false);
            current.ResumePropertyNotifications();
            if (!this.affectedViews.Contains(current.ViewInfo))
              this.affectedViews.Enqueue(current.ViewInfo);
          }
        }
      }
      while (!object.Equals((object) targetPostion, (object) traverser.Position) && traverser.MoveNext());
      synchronizationService.ResumeDispatch();
      this.GridViewElement.TableElement.ViewElement.UpdateRows();
      return true;
    }

    private GridTraverser CreateTraverser(
      GridTraverser.GridTraverserPosition currentPosition,
      out GridTraverser.GridTraverserPosition targetPostion)
    {
      GridTraverser gridTraverser = new GridTraverser((IHierarchicalRow) this.GridViewElement.Template);
      gridTraverser.ProcessHierarchy = true;
      targetPostion = (GridTraverser.GridTraverserPosition) null;
      if (this.moveNextFromAnchorPosition)
      {
        gridTraverser.Position = this.anchoredPosition.RowPosition;
        targetPostion = currentPosition;
      }
      else
      {
        gridTraverser.Position = currentPosition;
        targetPostion = this.anchoredPosition.RowPosition;
      }
      return gridTraverser;
    }

    private void GetColumnsBoundaryIndex(out int startColumnIndex, out int endColumnIndex)
    {
      startColumnIndex = -1;
      endColumnIndex = -1;
      if (this.MasterTemplate.SelectionMode != GridViewSelectionMode.CellSelect)
        return;
      int val1 = this.anchoredPosition.Column != null ? this.anchoredPosition.Column.Index : 0;
      int index = this.GridViewElement.CurrentColumn.Index;
      startColumnIndex = Math.Min(val1, index);
      endColumnIndex = Math.Max(val1, index);
    }

    private void SetSelectedCells(GridViewRowInfo row, int startColumnIndex, int endColumnIndex)
    {
      if (endColumnIndex >= row.Cells.Count)
        endColumnIndex = row.Cells.Count - 1;
      for (int index = startColumnIndex; index <= endColumnIndex; ++index)
        row.Cells[index].IsSelected = true;
    }

    private void ClearSelectedCollection()
    {
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
        this.MasterTemplate.SelectedRows.Clear(out this.affectedViews);
      else
        this.MasterTemplate.SelectedCells.Clear();
    }

    private void BeginUpdateSelectedCollection()
    {
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
        this.MasterTemplate.SelectedRows.BeginUpdate();
      else
        this.MasterTemplate.SelectedCells.BeginUpdate();
    }

    private void EndUpdateSelectedCollection(bool notifyUpdates)
    {
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
        this.MasterTemplate.SelectedRows.EndUpdate(notifyUpdates);
      else
        this.MasterTemplate.SelectedCells.EndUpdate(notifyUpdates);
    }

    private void SelectAllCells()
    {
      GridViewRowInfoEnumerator rowInfoEnumerator = new GridViewRowInfoEnumerator((IHierarchicalRow) this.GridViewElement.Template);
      List<GridViewCellInfo> gridViewCellInfoList = new List<GridViewCellInfo>();
      this.MasterTemplate.SelectedCells.BeginUpdate();
      this.MasterTemplate.SelectedCells.Clear();
      while (rowInfoEnumerator.MoveNext())
      {
        GridViewRowInfo current = rowInfoEnumerator.Current;
        if (current.CanBeSelected)
        {
          foreach (GridViewCellInfo cell in current.Cells)
            this.MasterTemplate.SelectedCells.Add(cell);
        }
      }
      this.MasterTemplate.SelectedCells.EndUpdate(true);
    }

    private void SelectAllRows()
    {
      GridViewRowInfoEnumerator rowInfoEnumerator = new GridViewRowInfoEnumerator((IHierarchicalRow) this.GridViewElement.Template);
      this.MasterTemplate.SelectedRows.BeginUpdate();
      this.MasterTemplate.SelectedRows.Clear();
      while (rowInfoEnumerator.MoveNext())
      {
        GridViewRowInfo current = rowInfoEnumerator.Current;
        if (current.CanBeSelected)
        {
          current.SuspendPropertyNotifications();
          this.MasterTemplate.SelectedRows.Add(current, false);
          current.ResumePropertyNotifications();
        }
      }
      this.MasterTemplate.SelectedRows.EndUpdate(true);
      this.GridViewElement.TableElement.ViewElement.ScrollableRows.ClearItems();
      this.GridViewElement.TableElement.ViewElement.UpdateRows();
    }

    public virtual bool SelectFirstRow()
    {
      ViewInfoTraverser.ViewInfoEnumeratorPosition position = this.enumerator.Position.Position as ViewInfoTraverser.ViewInfoEnumeratorPosition;
      if (position != null && position.Stage == ViewInfoTraverser.Stages.ChildRows && position.Index == 0)
        return false;
      this.enumerator.Reset();
      this.enumerator.RowVisible += new RowEnumeratorEventHandler(this.Enumerator_RowVisible);
      bool nextRow = this.FindNextRow(1, true);
      this.enumerator.RowVisible -= new RowEnumeratorEventHandler(this.Enumerator_RowVisible);
      return nextRow;
    }

    public virtual bool SelectLastRow()
    {
      this.enumerator.Reset();
      this.enumerator.RowVisible += new RowEnumeratorEventHandler(this.Enumerator_RowVisible);
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      while (this.enumerator.MoveNext())
        position = this.enumerator.Position;
      this.enumerator.Position = position;
      do
        ;
      while (this.enumerator.Current != null && !this.enumerator.Current.CanBeCurrent && this.enumerator.MovePrevious());
      this.enumerator.RowVisible -= new RowEnumeratorEventHandler(this.Enumerator_RowVisible);
      this.GridViewElement.TableElement.RowScroller.Scrollbar.PerformLast();
      return this.SelectCore(this.enumerator.Current, this.enumerator.Current.ViewTemplate.CurrentColumn);
    }

    public virtual bool SelectRow(GridViewRowInfo row)
    {
      return this.NavigateToRow(row, true);
    }

    protected virtual bool NavigateToRow(GridViewRowInfo row, bool select)
    {
      if (this.settingCurrentPosition || row == null || !row.CanBeCurrent)
        return false;
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      this.enumerator.Reset();
      this.moveNextFromAnchorPosition = false;
      while (this.enumerator.MoveNext())
      {
        if (object.Equals((object) this.anchoredPosition.RowPosition, (object) this.enumerator.Position))
          this.moveNextFromAnchorPosition = true;
        if (this.enumerator.Current == row)
        {
          if (select)
            return this.SelectCore(row, this.MasterTemplate.CurrentColumn);
          return true;
        }
      }
      this.enumerator.Position = position;
      return false;
    }

    public virtual bool SelectNextRow(int step)
    {
      return this.FindNextRow(step, true);
    }

    public virtual bool SelectPreviousRow(int step)
    {
      return this.FindNextRow(step, false);
    }

    public virtual bool IsFirstRow(GridViewRowInfo row)
    {
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      this.enumerator.Reset();
      while (this.enumerator.MoveNext())
      {
        if (this.enumerator.Current.CanBeCurrent)
        {
          bool flag = this.enumerator.Current == row;
          this.enumerator.Position = position;
          return flag;
        }
      }
      this.enumerator.Position = position;
      return false;
    }

    public virtual bool IsLastRow(GridViewRowInfo row)
    {
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      this.enumerator.GoToRow(row);
      while (this.enumerator.MoveNext())
      {
        if (this.enumerator.Current.CanBeCurrent)
        {
          this.enumerator.Position = position;
          return false;
        }
      }
      this.enumerator.Position = position;
      return true;
    }

    private bool FindNextRow(int step, bool moveNextDirection)
    {
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      if (!this.MoveToNextRow(step, moveNextDirection))
        return false;
      GridViewColumn column = this.MasterTemplate.CurrentView.ViewTemplate.CurrentColumn;
      GridViewRowInfo current = this.enumerator.Current;
      if (this.MasterTemplate.CurrentRow != null && current.ViewTemplate != this.MasterTemplate.CurrentRow.ViewTemplate)
        column = current.ViewTemplate.CurrentColumn ?? MasterGridViewTemplate.GetColumnAllowingForCurrent(current.ViewTemplate);
      if (column == null)
        return false;
      bool flag = this.SelectCore(current, column);
      if (!flag)
        this.enumerator.Position = position;
      return flag;
    }

    private bool MoveToNextRow(int step, bool moveNextDirection)
    {
      if (object.Equals((object) this.anchoredPosition.RowPosition, (object) this.enumerator.Position))
        this.moveNextFromAnchorPosition = moveNextDirection;
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      while (step > 0 && (moveNextDirection ? (this.enumerator.MoveNext() ? 1 : 0) : (this.enumerator.MovePrevious() ? 1 : 0)) != 0)
      {
        if (this.enumerator.Current.CanBeCurrent)
        {
          --step;
          if (object.Equals((object) this.anchoredPosition.RowPosition, (object) this.enumerator.Position))
            this.moveNextFromAnchorPosition = !this.moveNextFromAnchorPosition;
        }
      }
      if (this.enumerator.Current != null && this.enumerator.Current.ViewInfo == null)
        return false;
      if (this.enumerator.Current != null && this.enumerator.Current.CanBeCurrent)
        return true;
      this.enumerator.Position = position;
      return false;
    }

    public virtual bool SelectFirstColumn()
    {
      GridViewColumn selectableColumn = this.GetFirstSelectableColumn(this.Columns);
      if (selectableColumn == null)
        return false;
      return this.SelectCore(this.CurrentRow, selectableColumn);
    }

    public virtual bool SelectLastColumn()
    {
      GridViewColumn selectableColumn = this.GetLastSelectableColumn(this.Columns);
      if (selectableColumn == null)
        return false;
      return this.SelectCore(this.CurrentRow, selectableColumn);
    }

    private bool SwapColumnSelection
    {
      get
      {
        if (this.GridViewElement.ElementTree.Control.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
          return this.GridViewElement.Template.ViewDefinition is HtmlViewDefinition;
        return false;
      }
    }

    public virtual bool SelectNextColumn()
    {
      GridViewRowInfo row = this.CurrentRow;
      this.enumerator.GoToRow(this.CurrentRow);
      GridTraverser.GridTraverserPosition position = this.enumerator.Position;
      int index = 0;
      bool flag1 = false;
      bool flag2 = false;
      int offset = this.SwapColumnSelection ? -1 : 1;
      if (this.FindNextColumnIndex(this.CurrentColumnIndex, ref index, offset))
      {
        flag2 = this.MoveToNextRow(1, true);
        if (flag2)
          row = this.enumerator.Current;
        flag1 = true;
      }
      if (flag1 && !flag2)
        return this.SelectFirstColumn();
      if (index >= 0 && index < this.Columns.Count)
      {
        GridViewColumn column = this.Columns[index];
        if (column.OwnerTemplate != row.ViewTemplate)
          column = this.GetFirstSelectableColumn((IList<GridViewColumn>) row.ViewTemplate.Columns.ToList());
        if (!this.SelectCore(row, column))
        {
          this.enumerator.Position = position;
          return false;
        }
      }
      return true;
    }

    public virtual bool SelectPreviousColumn()
    {
      GridViewRowInfo row = this.CurrentRow;
      bool flag1 = false;
      bool flag2 = false;
      int index = 0;
      int offset = this.SwapColumnSelection ? 1 : -1;
      if (this.FindNextColumnIndex(this.CurrentColumnIndex, ref index, offset))
      {
        flag2 = this.MoveToNextRow(1, false);
        if (flag2)
          row = this.enumerator.Current;
        flag1 = true;
      }
      if (flag1 && !flag2)
        return this.SelectLastColumn();
      if (index < 0 || index >= this.Columns.Count)
        return true;
      GridViewColumn column = this.Columns[index];
      if (column.OwnerTemplate != row.ViewTemplate)
        column = this.GetLastSelectableColumn((IList<GridViewColumn>) row.ViewTemplate.Columns.ToList());
      return this.SelectCore(row, column);
    }

    public virtual bool IsLastColumn(GridViewColumn column)
    {
      int index = 0;
      return this.FindNextColumnIndex(this.Columns.IndexOf((GridViewColumn) (column as GridViewDataColumn)), ref index, 1);
    }

    public virtual bool IsFirstColumn(GridViewColumn column)
    {
      int index = 0;
      return this.FindNextColumnIndex(this.Columns.IndexOf((GridViewColumn) (column as GridViewDataColumn)), ref index, -1);
    }

    public virtual bool IsFirstEditableColumn(GridViewColumn column)
    {
      GridViewColumn gridViewColumn = (GridViewColumn) null;
      for (int index = 0; index < this.Columns.Count; ++index)
      {
        if (!this.Columns[index].ReadOnly)
        {
          gridViewColumn = this.Columns[index];
          break;
        }
      }
      return gridViewColumn == column;
    }

    public virtual bool IsLastEditableColumn(GridViewColumn column)
    {
      GridViewColumn gridViewColumn = (GridViewColumn) null;
      for (int index = this.Columns.Count - 1; index >= 0; --index)
      {
        if (!this.Columns[index].ReadOnly)
        {
          gridViewColumn = this.Columns[index];
          break;
        }
      }
      return gridViewColumn == column;
    }

    private bool FindNextColumnIndex(int startIndex, ref int index, int offset)
    {
      if (this.Columns.Count == 0)
        return false;
      bool flag = false;
      int num1 = this.Columns.Count * 2;
      int num2 = 0;
      int index1 = startIndex + offset;
      do
      {
        if (index1 >= this.Columns.Count)
        {
          index1 = 0;
          flag = true;
        }
        if (index1 < 0)
        {
          index1 = this.Columns.Count - 1;
          flag = true;
        }
        if (!this.Columns[index1].CanBeCurrent || this.GridViewElement.IsInEditMode && this.Columns[index1].ReadOnly && !(this.CurrentRow is GridViewFilteringRowInfo))
        {
          index1 += offset;
          ++num2;
        }
        else
          goto label_10;
      }
      while (num2 <= num1);
      index1 = startIndex;
label_10:
      index = index1;
      return flag;
    }

    private GridViewColumn GetFirstSelectableColumn(IList<GridViewColumn> columns)
    {
      int index = 0;
      while (index < columns.Count && (!columns[index].CanBeCurrent || !columns[index].IsVisible))
        ++index;
      if (index >= 0 && index < columns.Count && (columns[index].CanBeCurrent && columns[index].IsVisible))
        return columns[index];
      return (GridViewColumn) null;
    }

    private GridViewColumn GetLastSelectableColumn(IList<GridViewColumn> columns)
    {
      int index = columns.Count - 1;
      while (index >= 0 && (!columns[index].CanBeCurrent || !columns[index].IsVisible))
        --index;
      if (index >= 0 && index < columns.Count && (columns[index].CanBeCurrent && columns[index].IsVisible))
        return columns[index];
      return (GridViewColumn) null;
    }

    private void Enumerator_RowVisible(object sender, RowEnumeratorEventArgs e)
    {
      e.ProcessRow = e.Row is GridViewDataRowInfo;
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.UI;
      }
    }

    EventListenerPriority IGridViewEventListener.Priority
    {
      get
      {
        return this.GetEventListenerPirotiy();
      }
    }

    protected virtual EventListenerPriority GetEventListenerPirotiy()
    {
      return EventListenerPriority.Normal;
    }

    GridEventProcessMode IGridViewEventListener.DesiredProcessMode
    {
      get
      {
        return this.GetEventProcessMode();
      }
    }

    protected virtual GridEventProcessMode GetEventProcessMode()
    {
      return GridEventProcessMode.Process;
    }

    GridViewEventResult IGridViewEventListener.PreProcessEvent(
      GridViewEvent eventData)
    {
      return this.PreProcessEventCore(eventData);
    }

    protected virtual GridViewEventResult PreProcessEventCore(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.ProcessEvent(
      GridViewEvent eventData)
    {
      return this.ProcessEventCore(eventData);
    }

    protected virtual GridViewEventResult ProcessEventCore(
      GridViewEvent eventData)
    {
      MasterGridViewTemplate template = this.gridViewElement.Template;
      if (eventData.Info.Id == KnownEvents.CurrentChanged)
        return this.ProcessCurrentChangedEvent(eventData);
      if (eventData.Info.Id == KnownEvents.ViewChanged && (eventData.Arguments[0] as DataViewChangedEventArgs).Action == ViewChangedAction.Reset && (eventData.Sender is MasterGridViewTemplate && this.MasterTemplate.CurrentRow is GridViewDataRowInfo))
      {
        GridTraverser gridTraverser = new GridTraverser(this.GridViewElement.Template.MasterViewInfo);
        gridTraverser.GoToRow(this.MasterTemplate.CurrentRow);
        this.anchoredPosition = new AnchoredPosition(template.CurrentColumn, gridTraverser.Position);
      }
      if (GridViewSynchronizationService.IsRowPropertyChangedEvent(eventData))
      {
        PropertyChangedEventArgs changedEventArgs = (PropertyChangedEventArgs) eventData.Arguments[0];
        if ((eventData.Sender as GridViewRowInfo).IsCurrent && (changedEventArgs.PropertyName == "IsVisible" || changedEventArgs.PropertyName == "PinPosition" || changedEventArgs.PropertyName == "RowPosition"))
          this.SelectRow(template.CurrentRow);
      }
      else if (eventData.Sender == template && eventData.Info.Id == KnownEvents.ViewChanged)
        return this.ProcessViewChangedEvent(eventData);
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessCurrentChangedEvent(
      GridViewEvent eventData)
    {
      if (!(eventData.Sender is GridViewTemplate) || this.settingCurrentPosition)
        return (GridViewEventResult) null;
      if (eventData.Originator == null && this.MasterTemplate.CurrentRow == null)
      {
        this.ClearSelection();
        this.enumerator.Reset();
      }
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessViewChangedEvent(
      GridViewEvent eventData)
    {
      if (this.settingCurrentPosition)
        return (GridViewEventResult) null;
      MasterGridViewTemplate template = this.gridViewElement.Template;
      DataViewChangedEventArgs changedEventArgs = eventData.Arguments[0] as DataViewChangedEventArgs;
      if (changedEventArgs.Action == ViewChangedAction.ItemChanged)
      {
        if (template.SortDescriptors.Count > 0)
          this.NavigateToRow(template.CurrentRow, false);
      }
      else if (changedEventArgs.Action == ViewChangedAction.SortingChanged || changedEventArgs.Action == ViewChangedAction.Move)
        this.NavigateToRow(template.CurrentRow, false);
      else if (changedEventArgs.Action == ViewChangedAction.CurrentRowChanged)
        this.Select(template.CurrentRow, template.CurrentColumn);
      else if (changedEventArgs.Action == ViewChangedAction.CurrentColumnChanged)
        this.SelectOverride(template.CurrentRow, template.CurrentColumn);
      else if (changedEventArgs.Action == ViewChangedAction.GroupingChanged)
      {
        if (template.CurrentRow != null && template.CurrentColumn != null)
          this.SelectOverride(template.CurrentRow, template.CurrentColumn);
      }
      else if (changedEventArgs.Action == ViewChangedAction.FilteringChanged && !this.GridViewElement.IsInEditMode && !template.MasterViewInfo.TableFilteringRow.IsSuspended)
      {
        GridViewDataRowInfo currentRow = template.CurrentRow as GridViewDataRowInfo;
        if (template.MultiSelect)
        {
          int index = 0;
          if (template.SelectionMode == GridViewSelectionMode.FullRowSelect)
          {
            while (index < template.SelectedRows.Count)
            {
              if (!template.ChildRows.Contains(template.SelectedRows[index]))
                template.SelectedRows.Remove(template.SelectedRows[index]);
              else
                ++index;
            }
          }
          else if (template.SelectionMode == GridViewSelectionMode.CellSelect)
          {
            while (index < template.SelectedCells.Count)
            {
              if (!template.ChildRows.Contains(template.SelectedCells[index].RowInfo))
                template.SelectedCells.Remove(template.SelectedCells[index]);
              else
                ++index;
            }
          }
          foreach (GridViewRowInfo selectedRow in (ReadOnlyCollection<GridViewRowInfo>) template.SelectedRows)
          {
            if (!template.ChildRows.Contains(selectedRow))
            {
              selectedRow.IsCurrent = false;
              selectedRow.IsSelected = false;
            }
          }
        }
        if (template.ChildRows.Count > 0 && (currentRow == null || !template.ChildRows.Contains((GridViewRowInfo) currentRow)))
        {
          GridViewRowInfo childRow = template.ChildRows[0];
          childRow.IsCurrent = true;
          childRow.IsSelected = true;
          this.enumerator.GoToRow(childRow);
        }
      }
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.PostProcessEvent(
      GridViewEvent eventData)
    {
      return this.PostProcessEventCore(eventData);
    }

    protected virtual GridViewEventResult PostProcessEventCore(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    bool IGridViewEventListener.AnalyzeQueue(List<GridViewEvent> events)
    {
      return this.AnalyzeQueueCore(events);
    }

    protected virtual bool AnalyzeQueueCore(List<GridViewEvent> events)
    {
      return false;
    }
  }
}
