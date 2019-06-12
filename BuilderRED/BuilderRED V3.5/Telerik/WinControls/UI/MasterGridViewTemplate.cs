// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MasterGridViewTemplate
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [DesignTimeVisible(false)]
  public class MasterGridViewTemplate : GridViewTemplate
  {
    private GridViewSelectionMode selectionMode = GridViewSelectionMode.FullRowSelect;
    private bool throwExceptionOnDataOperationInVirtualMode = true;
    private GridViewClipboardCutMode clipboardCutMode = GridViewClipboardCutMode.EnableWithoutHeaderText;
    private GridViewClipboardCopyMode clipboardCopyMode = GridViewClipboardCopyMode.EnableWithoutHeaderText;
    private GridViewClipboardPasteMode clipboardPasteMode = GridViewClipboardPasteMode.Enable;
    private GridViewRowInfo currentRow;
    private EventDispatcher eventDispatcher;
    private GridViewInfo currentView;
    private GridViewRelationCollection relations;
    private bool virtualMode;
    private bool settingPosition;
    private bool userSetCurrentRow;
    private GridViewSelectedRowsCollection selectedRows;
    private GridViewSelectedCellsCollection selectedCells;
    private bool multiSelect;
    private bool autoGenerateHierarchy;
    private bool gridReadOnly;
    private bool loading;
    private bool currentRowChangedInternally;
    private GridViewSynchronizationService synchronizationService;
    private bool addNewBoundRowBeforeEdit;
    private RadGridView ownerGrid;
    private GridViewColumn selfReferenceExpanderColumn;
    private bool copyFullRow;
    private bool copyCellOnly;
    internal bool SuspendEnsureVisible;

    public MasterGridViewTemplate()
    {
      this.synchronizationService = new GridViewSynchronizationService();
      this.SynchronizationService.AddListener((IGridViewEventListener) this);
      this.currentView = this.MasterViewInfo;
      this.relations = new GridViewRelationCollection();
      this.selectedRows = new GridViewSelectedRowsCollection(this);
      this.selectedCells = new GridViewSelectedCellsCollection(this);
    }

    protected override BindingContext CreateBindingContext()
    {
      return (BindingContext) null;
    }

    protected override void WireEvents()
    {
      base.WireEvents();
      this.DataView.PageChanging += new EventHandler<PageChangingEventArgs>(this.DataView_PageChanging);
      this.DataView.PageChanged += new EventHandler<EventArgs>(this.DataView_PageChanged);
    }

    protected internal override void UnwireEvents()
    {
      this.DataView.PageChanging -= new EventHandler<PageChangingEventArgs>(this.DataView_PageChanging);
      this.DataView.PageChanged -= new EventHandler<EventArgs>(this.DataView_PageChanged);
      base.UnwireEvents();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.ownerGrid = (RadGridView) null;
        this.synchronizationService.Dispose();
      }
      base.Dispose(disposing);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override GridViewHierarchyDataProvider HierarchyDataProvider
    {
      get
      {
        return base.HierarchyDataProvider;
      }
      set
      {
        if (!(value is GridViewSelfReferenceDataProvider) && value != null)
          throw new InvalidOperationException("Only GridViewSelfReferenceDataProvider can be applied to MasterTemplate");
        base.HierarchyDataProvider = value;
      }
    }

    [Browsable(false)]
    public GridViewSynchronizationService SynchronizationService
    {
      get
      {
        return this.synchronizationService;
      }
    }

    [Category("Data")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the RadGridView will automatically build hierarchy from DataSource.")]
    public bool AutoGenerateHierarchy
    {
      get
      {
        return this.autoGenerateHierarchy;
      }
      set
      {
        if (!this.SetProperty<bool>(nameof (AutoGenerateHierarchy), ref this.autoGenerateHierarchy, value) || this.DesignMode)
          return;
        this.GenerateHierarchy();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    [Browsable(false)]
    public GridViewRowInfo CurrentRow
    {
      get
      {
        return this.currentRow;
      }
      set
      {
        GridViewColumn column = value == null ? this.CurrentColumn : value.ViewTemplate.CurrentColumn;
        GridViewSynchronizationService.RaiseCurrentChanged((GridViewTemplate) this, value, column, true);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(null)]
    public GridViewInfo CurrentView
    {
      get
      {
        return this.currentView;
      }
      set
      {
        this.SetCurrentView(value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool VirtualMode
    {
      get
      {
        return this.virtualMode;
      }
      set
      {
        this.SetProperty<bool>(nameof (VirtualMode), ref this.virtualMode, value);
      }
    }

    [Category("Hierarchy")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewRelationCollection Relations
    {
      get
      {
        return this.relations;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override EventDispatcher EventDispatcher
    {
      get
      {
        if (this.eventDispatcher == null)
          this.eventDispatcher = new EventDispatcher();
        return this.eventDispatcher;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override MasterGridViewTemplate MasterTemplate
    {
      get
      {
        return this;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewSelectedRowsCollection SelectedRows
    {
      get
      {
        return this.selectedRows;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewSelectedCellsCollection SelectedCells
    {
      get
      {
        return this.selectedCells;
      }
    }

    [DefaultValue(false)]
    public bool MultiSelect
    {
      get
      {
        return this.multiSelect;
      }
      set
      {
        this.SetProperty<bool>(nameof (MultiSelect), ref this.multiSelect, value);
      }
    }

    [DefaultValue(GridViewSelectionMode.FullRowSelect)]
    public GridViewSelectionMode SelectionMode
    {
      get
      {
        return this.selectionMode;
      }
      set
      {
        this.SetProperty<GridViewSelectionMode>(nameof (SelectionMode), ref this.selectionMode, value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool GridReadOnly
    {
      get
      {
        return this.gridReadOnly;
      }
      set
      {
        this.SetProperty<bool>(nameof (GridReadOnly), ref this.gridReadOnly, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadGridView Owner
    {
      get
      {
        return this.ownerGrid;
      }
      internal set
      {
        this.ownerGrid = value;
      }
    }

    [Description("Gets or sets a value indicating whether the data in the current DataView can be paginated.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool EnablePaging
    {
      get
      {
        return this.ListSource.CollectionView.CanPage;
      }
      set
      {
        if (this.ListSource.CollectionView.CanPage == value)
          return;
        this.ListSource.CollectionView.CanPage = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnablePaging)));
      }
    }

    [Description("Gets or sets the columns the cells of which will contain the self-reference expander items.")]
    [DefaultValue(null)]
    [Browsable(true)]
    [Category("Behavior")]
    public GridViewColumn SelfReferenceExpanderColumn
    {
      get
      {
        return this.selfReferenceExpanderColumn;
      }
      set
      {
        if (this.selfReferenceExpanderColumn == value)
          return;
        this.selfReferenceExpanderColumn = value;
        this.OnNotifyPropertyChanged(nameof (SelfReferenceExpanderColumn));
      }
    }

    internal bool SettingPosition
    {
      get
      {
        return this.settingPosition;
      }
      set
      {
        this.settingPosition = value;
      }
    }

    [Description("Gets or sets a value indicating whether an exception will be thrown of one attemps to sort, filter or group in virtual mode.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(true)]
    public bool ThrowExceptionOnDataOperationInVirtualMode
    {
      get
      {
        return this.throwExceptionOnDataOperationInVirtualMode;
      }
      set
      {
        this.throwExceptionOnDataOperationInVirtualMode = value;
      }
    }

    protected override GridViewListSource CreateListSource()
    {
      return new GridViewListSource((GridViewTemplate) this);
    }

    public override void BeginUpdate()
    {
      this.synchronizationService.SuspendDispatch();
      base.BeginUpdate();
    }

    public override void EndUpdate(bool notify, DataViewChangedEventArgs e)
    {
      this.synchronizationService.ResumeDispatch();
      base.EndUpdate(notify, e);
      Stack<GridViewTemplate> gridViewTemplateStack = new Stack<GridViewTemplate>();
      gridViewTemplateStack.Push((GridViewTemplate) this);
      while (gridViewTemplateStack.Count > 0)
      {
        GridViewTemplate gridViewTemplate = gridViewTemplateStack.Pop();
        for (int index = 0; index < gridViewTemplate.Templates.Count; ++index)
          gridViewTemplateStack.Push(gridViewTemplate.Templates[index]);
        this.SynchronizationService.DispatchEvent(new GridViewEvent((object) this, (object) gridViewTemplate, (object[]) null, new GridViewEventInfo(KnownEvents.HierarchyChanged, GridEventType.Both, GridEventDispatchMode.Send)));
      }
    }

    internal void BeginLoad()
    {
      this.loading = true;
    }

    internal void EndLoad()
    {
      this.loading = false;
    }

    internal bool IsLoading
    {
      get
      {
        return this.loading;
      }
    }

    private GridViewTemplate GetTemplate(GridViewRowInfo row, GridViewColumn column)
    {
      GridViewTemplate gridViewTemplate = (GridViewTemplate) this;
      if (row != null && row.ViewTemplate != null)
        gridViewTemplate = row.ViewTemplate;
      else if (column != null && column.OwnerTemplate != null)
        gridViewTemplate = column.OwnerTemplate;
      return gridViewTemplate;
    }

    protected override void ResetCurrentRow()
    {
      base.ResetCurrentRow();
      this.ClearSelectedRowsOrCells();
    }

    protected override EventListenerPriority GetEventListenerPirotiy()
    {
      return EventListenerPriority.Highest;
    }

    protected override GridEventProcessMode GetEventProcessMode()
    {
      return GridEventProcessMode.All;
    }

    protected override GridViewEventResult PreProcessEventCore(
      GridViewEvent eventData)
    {
      if (this.SettingPosition || this.currentRowChangedInternally)
        return (GridViewEventResult) null;
      if (eventData.Info.Id != KnownEvents.CurrentChanged)
        return (GridViewEventResult) null;
      GridViewTemplate sender = eventData.Sender as GridViewTemplate;
      if (sender == null)
        return (GridViewEventResult) null;
      GridViewRowInfo defaultCurrentRow = eventData.Arguments[0] as GridViewRowInfo;
      GridViewColumn gridViewColumn = eventData.Arguments[1] as GridViewColumn;
      if (defaultCurrentRow == null && eventData.Originator is RadDataView<GridViewRowInfo>)
        defaultCurrentRow = MasterGridViewTemplate.GetDefaultCurrentRow(sender, sender.MasterViewInfo, true);
      if (defaultCurrentRow != this.currentRow)
        return (GridViewEventResult) null;
      if (gridViewColumn != sender.CurrentColumn)
        return (GridViewEventResult) null;
      return new GridViewEventResult(true, true);
    }

    protected override GridViewEventResult ProcessEventCore(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.CurrentChanged)
      {
        this.ProcessCurrentChanged(eventData);
        return (GridViewEventResult) null;
      }
      if (eventData.Info.Id == KnownEvents.TemplateDataSourceInitializing)
      {
        this.OnTemplateDataSourceInitializing(eventData.Sender as GridViewTemplate);
        return (GridViewEventResult) null;
      }
      if (!(eventData.Originator is GridViewColumnCollection) || (eventData.Info.Id != KnownEvents.CollectionChanged || this.CurrentColumn != null || this.ColumnCount <= 0))
        return base.ProcessEventCore(eventData);
      this.SetCurrentColumn(MasterGridViewTemplate.GetColumnAllowingForCurrent(eventData.Sender as GridViewTemplate), false);
      return (GridViewEventResult) null;
    }

    protected override GridViewEventResult PostProcessEventCore(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.Dispose)
      {
        this.DisposeRows(eventData.Arguments[0] as IList);
        return (GridViewEventResult) null;
      }
      if (eventData.Info.Id != KnownEvents.ViewChanged)
        return (GridViewEventResult) null;
      DataViewChangedEventArgs changedEventArgs = eventData.Arguments[0] as DataViewChangedEventArgs;
      if (changedEventArgs.Action != ViewChangedAction.Remove)
        return (GridViewEventResult) null;
      GridViewTemplate originator = eventData.Originator as GridViewTemplate;
      if (originator != null && originator.IsVirtualRows)
        return (GridViewEventResult) null;
      this.DisposeRows(changedEventArgs.NewItems);
      return (GridViewEventResult) null;
    }

    private void DisposeRows(IList rows)
    {
      if (rows == null)
        return;
      foreach (GridViewRowInfo row in (IEnumerable) rows)
      {
        if (row == this.currentRow)
        {
          if (this.ownerGrid != null && this.ownerGrid.IsInEditMode)
            this.ownerGrid.CloseEditor();
          this.currentRow = (GridViewRowInfo) null;
        }
        row.Dispose();
      }
    }

    private void OnTemplateDataSourceInitializing(GridViewTemplate template)
    {
      if (this.currentRow == null || this.currentRow.IsSystem || this.currentRow.ViewTemplate != template)
        return;
      this.currentRow = (GridViewRowInfo) null;
      this.currentRowChangedInternally = true;
    }

    private void ProcessCurrentChanged(GridViewEvent eventData)
    {
      if (this.SettingPosition)
        return;
      GridViewTemplate sender = eventData.Sender as GridViewTemplate;
      if (sender == null)
        return;
      bool flag = eventData.Originator == sender.DataView;
      if (flag && !this.currentRowChangedInternally && (this.currentRow != null && this.currentRow.ViewTemplate != sender || this.currentRow is GridViewSystemRowInfo && this.userSetCurrentRow))
        return;
      GridViewRowInfo defaultCurrentRow = eventData.Arguments[0] as GridViewRowInfo;
      GridViewColumn column = eventData.Arguments[1] as GridViewColumn;
      if (defaultCurrentRow == null && eventData.Originator is RadDataView<GridViewRowInfo>)
        defaultCurrentRow = MasterGridViewTemplate.GetDefaultCurrentRow(sender, sender.MasterViewInfo, true);
      this.SetPosition(defaultCurrentRow, column, !flag);
      this.currentRowChangedInternally = false;
    }

    internal static GridViewRowInfo GetDefaultCurrentRow(
      GridViewTemplate template,
      GridViewInfo viewInfo,
      bool checkItemCount)
    {
      if (checkItemCount && template.DataView.Count > 0)
        return (GridViewRowInfo) null;
      bool flag = template.MasterTemplate != null && template.MasterTemplate.GridReadOnly;
      if (viewInfo.TableAddNewRow.IsVisible && !flag && (!template.ReadOnly && template.AllowAddNewRow))
        return (GridViewRowInfo) viewInfo.TableAddNewRow;
      return (GridViewRowInfo) viewInfo.ParentRow;
    }

    private bool SetCurrentView(GridViewInfo newView)
    {
      if (this.currentView == newView || newView == null)
        return false;
      this.currentView = newView;
      this.OnViewChanged(new DataViewChangedEventArgs(ViewChangedAction.CurrentViewChanged, (object) newView));
      return true;
    }

    private bool ChangeCurrentColumn(
      GridViewColumn oldColumn,
      GridViewColumn newColumn,
      GridViewTemplate viewTemplate)
    {
      if (oldColumn == newColumn)
      {
        if (newColumn != null && !newColumn.IsCurrent)
          newColumn.IsCurrent = true;
        return false;
      }
      if (oldColumn != null && oldColumn.OwnerTemplate == viewTemplate)
        oldColumn.IsCurrent = false;
      viewTemplate.SetCurrentColumn(newColumn, false);
      return true;
    }

    private bool ChangeCurrentRow(
      GridViewRowInfo oldRow,
      GridViewRowInfo row,
      GridViewTemplate viewTemplate)
    {
      if (this.currentRow == row && !this.currentRowChangedInternally)
        return true;
      if (!this.ListSource.IsUpdating)
      {
        CurrentRowChangingEventArgs args = new CurrentRowChangingEventArgs(oldRow, row);
        this.EventDispatcher.RaiseEvent<CurrentRowChangingEventArgs>(EventDispatcher.CurrentRowChanging, (object) viewTemplate, args);
        if (args.Cancel)
          return false;
      }
      if (row != null)
        this.SetCurrentView(row.ViewInfo);
      if (this.currentRow != null)
        this.currentRow.IsCurrent = false;
      this.currentRow = row;
      if (!viewTemplate.dataSourceChanging)
        this.EnsureGroupsExpandedState(this.currentRow);
      if (this.currentRow != null)
        this.currentRow.IsCurrent = true;
      row?.ViewTemplate.OnRowSetAsCurrent(this.currentRow);
      if (!this.ListSource.IsUpdating)
      {
        GridViewSynchronizationService.ResumeEvent((GridViewTemplate) this, KnownEvents.CurrentChanged);
        CurrentRowChangedEventArgs args = new CurrentRowChangedEventArgs(oldRow, row);
        this.EventDispatcher.RaiseEvent<CurrentRowChangedEventArgs>(EventDispatcher.CurrentRowChanged, (object) this, args);
        GridViewSynchronizationService.SuspendEvent((GridViewTemplate) this, KnownEvents.CurrentChanged);
      }
      this.OnViewChanged(new DataViewChangedEventArgs(ViewChangedAction.CurrentRowChanged, (object) this.CurrentRow));
      return true;
    }

    private void EnsureGroupsExpandedState(GridViewRowInfo row)
    {
      if (row == null)
        return;
      for (GridViewRowInfo parent = row.Parent as GridViewRowInfo; parent != null; parent = parent.Parent as GridViewRowInfo)
        parent.IsExpanded = true;
    }

    protected bool SetPosition(GridViewRowInfo row, GridViewColumn column, bool validateRowChange)
    {
      if (this.SettingPosition || row != null && column != null && row.ViewTemplate != column.OwnerTemplate)
        return false;
      GridViewColumn oldColumn = this.currentView != null ? this.currentView.ViewTemplate.CurrentColumn : (GridViewColumn) null;
      GridViewRowInfo currentRow = this.currentRow;
      if (!this.currentRowChangedInternally && currentRow == row && oldColumn == column)
        return false;
      this.SettingPosition = true;
      bool flag = this.SetPositionCore(row, column, currentRow, oldColumn, validateRowChange);
      this.SettingPosition = false;
      return flag;
    }

    protected virtual bool SetPositionCore(
      GridViewRowInfo newRow,
      GridViewColumn newColumn,
      GridViewRowInfo oldRow,
      GridViewColumn oldColumn,
      bool validateRowChange)
    {
      if (validateRowChange)
      {
        PositionChangingEventArgs args = new PositionChangingEventArgs(newRow, newColumn);
        this.EventDispatcher.RaiseEvent<PositionChangingEventArgs>(EventDispatcher.PositionChanging, (object) this, args);
        if (args.Cancel)
          return false;
      }
      this.userSetCurrentRow = validateRowChange;
      GridViewTemplate template = this.GetTemplate(newRow, newColumn);
      if (!this.ChangeCurrentRow(oldRow, newRow, template))
        return false;
      if (oldRow == null && newRow != null && (template.Columns.Count > 0 && newColumn == null))
        newColumn = (GridViewColumn) template.Columns[0];
      newColumn = MasterGridViewTemplate.GetColumnAllowingForCurrent(template, newColumn);
      if (newColumn == null)
        return false;
      bool flag = this.ChangeCurrentColumn(this.currentView != null ? this.currentView.ViewTemplate.CurrentColumn : (GridViewColumn) null, newColumn, template);
      if (this.currentRow != null)
      {
        oldRow?.InvalidateRow();
        if (newColumn != null && newColumn.OwnerTemplate == this.currentRow.ViewTemplate && this.currentRow.Cells[newColumn.Name] != null)
          this.currentRow.Cells[newColumn.Name].EnsureVisible();
        else
          this.currentRow.EnsureVisible();
      }
      GridViewSynchronizationService.ResumeEvent((GridViewTemplate) this, KnownEvents.CurrentChanged);
      if (flag)
      {
        this.OnViewChanged(new DataViewChangedEventArgs(ViewChangedAction.CurrentColumnChanged, (object) this.CurrentColumn));
        CurrentColumnChangedEventArgs args = new CurrentColumnChangedEventArgs(oldColumn, newColumn);
        this.EventDispatcher.RaiseEvent<CurrentColumnChangedEventArgs>(EventDispatcher.CurrentColumnChanged, (object) template, args);
      }
      CurrentCellChangedEventArgs args1 = new CurrentCellChangedEventArgs(oldRow, oldColumn, newRow, newColumn);
      this.EventDispatcher.RaiseEvent<CurrentCellChangedEventArgs>(EventDispatcher.CurrentCellChanged, (object) this, args1);
      PositionChangedEventArgs args2 = new PositionChangedEventArgs(newRow, newColumn);
      this.EventDispatcher.RaiseEvent<PositionChangedEventArgs>(EventDispatcher.PositionChanged, (object) this, args2);
      return true;
    }

    internal static GridViewColumn GetColumnAllowingForCurrent(
      GridViewTemplate template,
      GridViewColumn newColumn)
    {
      if (newColumn != null && newColumn.CanBeCurrent && newColumn.IsVisible)
        return newColumn;
      return MasterGridViewTemplate.GetColumnAllowingForCurrent(template);
    }

    internal static GridViewColumn GetColumnAllowingForCurrent(
      GridViewTemplate template)
    {
      if (template.CurrentColumn != null)
        return template.CurrentColumn;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) template.Columns)
      {
        if (column.IsVisible && column.CanBeCurrent)
          return column;
      }
      return (GridViewColumn) null;
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "DataSource" || e.PropertyName == "DataMember")
        this.GenerateHierarchy();
      base.OnNotifyPropertyChanged(e);
    }

    private void DataView_PageChanging(object sender, PageChangingEventArgs e)
    {
      this.OnPageChanging(sender, e);
    }

    private void DataView_PageChanged(object sender, EventArgs e)
    {
      this.OnPageChanged(sender, e);
    }

    protected virtual void OnPageChanging(object sender, PageChangingEventArgs e)
    {
      this.EventDispatcher.RaiseEvent<PageChangingEventArgs>(EventDispatcher.PageChanging, sender, e);
    }

    protected virtual void OnPageChanged(object sender, EventArgs e)
    {
      this.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.PageChanged, sender, e);
    }

    public virtual bool MoveToFirstPage()
    {
      bool firstPage = this.DataView.MoveToFirstPage();
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this.ownerGrid, "GridNavigatorMoveToPage", (object) this.PageIndex);
      return firstPage;
    }

    public virtual bool MoveToLastPage()
    {
      bool lastPage = this.DataView.MoveToLastPage();
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this.ownerGrid, "GridNavigatorMoveToPage", (object) this.PageIndex);
      return lastPage;
    }

    public virtual bool MoveToNextPage()
    {
      bool nextPage = this.DataView.MoveToNextPage();
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this.ownerGrid, "GridNavigatorMoveToPage", (object) this.PageIndex);
      return nextPage;
    }

    public virtual bool MoveToPage(int pageIndex)
    {
      bool page = this.DataView.MoveToPage(pageIndex);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this.ownerGrid, "GridNavigatorMoveToPage", (object) this.PageIndex);
      return page;
    }

    public virtual bool MoveToPreviousPage()
    {
      bool previousPage = this.DataView.MoveToPreviousPage();
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this.ownerGrid, "GridNavigatorMoveToPage", (object) this.PageIndex);
      return previousPage;
    }

    public virtual bool CanChangePage
    {
      get
      {
        return this.DataView.CanChangePage;
      }
    }

    public virtual bool IsPageChanging
    {
      get
      {
        return this.DataView.IsPageChanging;
      }
    }

    public virtual int PageIndex
    {
      get
      {
        return this.DataView.PageIndex;
      }
    }

    [DefaultValue(20)]
    public virtual int PageSize
    {
      get
      {
        return this.DataView.PageSize;
      }
      set
      {
        this.DataView.PageSize = value;
      }
    }

    public virtual int TotalPages
    {
      get
      {
        return this.DataView.TotalPages;
      }
    }

    [DefaultValue(false)]
    public virtual bool PagingBeforeGrouping
    {
      get
      {
        return this.DataView.PagingBeforeGrouping;
      }
      set
      {
        this.DataView.PagingBeforeGrouping = value;
      }
    }

    public void Reset()
    {
      using (this.DeferRefresh())
      {
        this.Templates.Clear();
        this.Relations.Clear();
        this.Columns.Clear();
        if (this.ListSource.IsDataBound)
          this.ListSource.DataSource = (object) null;
        this.ListSource.Clear();
        this.autoGenerateHierarchy = false;
      }
    }

    private void GenerateHierarchy()
    {
      if (!this.AutoGenerateHierarchy)
        return;
      using (this.DeferRefresh())
      {
        this.relations.Clear();
        while (this.Templates.Count > 0)
        {
          GridViewTemplate template = this.Templates[0];
          this.Templates.RemoveAt(0);
          template.Dispose();
        }
        if (this.DataSource == null)
          return;
        ObjectRelation objectRelation1 = ObjectRelation.GetObjectRelation(this.DataSource, this.DataMember);
        if (objectRelation1 == null)
          return;
        Stack<ObjectRelation> objectRelationStack = new Stack<ObjectRelation>();
        for (int index = objectRelation1.ChildRelations.Count - 1; index >= 0; --index)
        {
          objectRelation1.ChildRelations[index].Tag = (object) this;
          objectRelationStack.Push(objectRelation1.ChildRelations[index]);
        }
        while (objectRelationStack.Count > 0)
        {
          ObjectRelation objectRelation2 = objectRelationStack.Pop();
          GridViewTemplate tag = objectRelation2.Tag as GridViewTemplate;
          GridViewTemplate childTemplate = new GridViewTemplate();
          if (objectRelation2 is SelfReferenceRelation)
          {
            this.relations.AddSelfReference(tag, objectRelation2.ParentRelationNames[0], objectRelation2.ChildRelationNames[0]);
          }
          else
          {
            tag.Templates.Add(childTemplate);
            childTemplate.Caption = objectRelation2.Name;
            if (objectRelation2 is DataSetObjectRelation)
              childTemplate.DataSource = objectRelation2.List;
            else
              childTemplate.AutoGenerateBoundColumns(ListBindingHelper.GetListItemProperties(objectRelation2.List));
            GridViewRelation gridViewRelation = new GridViewRelation(tag, childTemplate);
            gridViewRelation.RelationName = objectRelation2.Parent.Name + "_" + objectRelation2.Name;
            for (int index = 0; index < objectRelation2.ParentRelationNames.Length; ++index)
              gridViewRelation.ParentColumnNames.Add(objectRelation2.ParentRelationNames[index]);
            for (int index = 0; index < objectRelation2.ChildRelationNames.Length; ++index)
              gridViewRelation.ChildColumnNames.Add(objectRelation2.ChildRelationNames[index]);
            this.relations.Add(gridViewRelation);
            for (int index = 0; index < objectRelation2.ChildRelations.Count; ++index)
            {
              objectRelation2.ChildRelations[index].Tag = (object) childTemplate;
              objectRelationStack.Push(objectRelation2.ChildRelations[index]);
            }
          }
        }
      }
    }

    private void ClearSelectedRowsOrCells()
    {
      this.selectedRows.BeginUpdate();
      this.SelectedRows.Clear(true);
      this.selectedRows.EndUpdate(false);
      this.SelectedCells.BeginUpdate();
      this.SelectedCells.Clear();
      this.SelectedCells.EndUpdate(false);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(false)]
    public bool AddNewBoundRowBeforeEdit
    {
      get
      {
        return this.addNewBoundRowBeforeEdit;
      }
      set
      {
        if (this.addNewBoundRowBeforeEdit == value)
          return;
        this.addNewBoundRowBeforeEdit = value;
        this.OnNotifyPropertyChanged(nameof (AddNewBoundRowBeforeEdit));
      }
    }

    protected internal override void OnViewChanged(object sender, DataViewChangedEventArgs e)
    {
      base.OnViewChanged(sender, e);
      if (!this.EnablePaging || e.Action != ViewChangedAction.Reset || this.PageIndex < this.TotalPages)
        return;
      this.MoveToLastPage();
    }

    public override void EndInit()
    {
      if (this.Templates.Count == 0)
        this.GenerateHierarchy();
      base.EndInit();
    }

    private DataObject CopyContent(bool cut)
    {
      if (cut && this.ClipboardCutMode == GridViewClipboardCutMode.Disable)
        this.SetError(new GridViewCellCancelEventArgs(this.currentRow, this.CurrentColumn, (IInputEditor) null), "Clipboard cut is disabled.");
      if (!cut && this.ClipboardCopyMode == GridViewClipboardCopyMode.Disable)
        this.SetError(new GridViewCellCancelEventArgs(this.currentRow, this.CurrentColumn, (IInputEditor) null), "Clipboard copy is disabled.");
      DataObject dataObject = new DataObject();
      StringBuilder stringBuilder = (StringBuilder) null;
      string str = (string) null;
      GridViewClipboardEventArgs args1 = new GridViewClipboardEventArgs(false, DataFormats.Text, dataObject, (GridViewTemplate) this);
      this.EventDispatcher.RaiseEvent<GridViewClipboardEventArgs>(EventDispatcher.Copying, (object) this, args1);
      if (!args1.Cancel)
      {
        str = this.ProcessContent(DataFormats.Text, false, cut).ToString();
        dataObject.SetData(DataFormats.UnicodeText, false, (object) str);
        dataObject.SetData(DataFormats.Text, false, (object) str);
      }
      GridViewClipboardEventArgs args2 = new GridViewClipboardEventArgs(false, DataFormats.CommaSeparatedValue, dataObject, (GridViewTemplate) this);
      this.EventDispatcher.RaiseEvent<GridViewClipboardEventArgs>(EventDispatcher.Copying, (object) this, args2);
      if (!args2.Cancel)
      {
        stringBuilder = this.ProcessContent(DataFormats.CommaSeparatedValue, false, cut);
        dataObject.SetData(DataFormats.CommaSeparatedValue, false, (object) str);
      }
      GridViewClipboardEventArgs args3 = new GridViewClipboardEventArgs(false, DataFormats.Html, dataObject, (GridViewTemplate) this);
      this.EventDispatcher.RaiseEvent<GridViewClipboardEventArgs>(EventDispatcher.Copying, (object) this, args3);
      if (!args3.Cancel)
      {
        StringBuilder sbContent = this.ProcessContent(DataFormats.Html, cut, cut);
        MemoryStream utf8Stream = (MemoryStream) null;
        this.GetHtmlContent(sbContent, out utf8Stream);
        dataObject.SetData(DataFormats.Html, false, (object) utf8Stream);
      }
      return dataObject;
    }

    private StringBuilder ProcessContent(string format, bool cut, bool cutOperation)
    {
      StringBuilder content = new StringBuilder(1024);
      if (this.SelectionMode == GridViewSelectionMode.CellSelect)
      {
        GridViewCellInfo[] gridViewCellInfoArray = new GridViewCellInfo[this.SelectedCells.Count];
        this.SelectedCells.CopyTo(gridViewCellInfoArray, 0);
        this.CopySelected(gridViewCellInfoArray, format, cut, cutOperation, content);
      }
      else if (this.SelectionMode == GridViewSelectionMode.FullRowSelect)
      {
        if (this.copyCellOnly && !this.copyFullRow && (this.SelectedRows.Count == 1 && this.SelectedRows[0].ViewTemplate.CurrentColumn != null))
        {
          GridViewRowInfo selectedRow = this.SelectedRows[0];
          GridViewColumn currentColumn = selectedRow.ViewTemplate.CurrentColumn;
          this.CopySelected(new GridViewCellInfo[1]
          {
            selectedRow.Cells[currentColumn.Index]
          }, format, cut, cutOperation, content);
        }
        else
          this.CopyRows(format, cut, cutOperation, content);
      }
      return content;
    }

    public void BeginRowCopy()
    {
      this.copyFullRow = true;
    }

    public void EndRowCopy()
    {
      this.copyFullRow = false;
    }

    public void BeginCellCopy()
    {
      this.copyCellOnly = true;
    }

    public void EndCellCopy()
    {
      this.copyCellOnly = false;
    }

    private void CopyRows(string format, bool cut, bool cutOperation, StringBuilder content)
    {
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      if (this.SelectedRows.Count == 0)
        return;
      this.FormatClipboardStart(content, format);
      bool isSingleCell = this.SelectedRows.Count == 1 && this.SelectedRows[0].ViewTemplate.Columns.Count == 1 && this.SelectedRows.Count == 1 && (!cutOperation && this.ClipboardCopyMode != GridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText && (cutOperation && this.ClipboardCutMode != GridViewClipboardCutMode.EnableAlwaysIncludeHeaderText));
      for (int index1 = 0; index1 < this.SelectedRows.Count; ++index1)
      {
        if ((!cutOperation && this.ClipboardCopyMode == GridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText || cutOperation && this.ClipboardCutMode == GridViewClipboardCutMode.EnableAlwaysIncludeHeaderText) && index1 == 0)
        {
          this.FormatRowStart(content, format);
          bool flag = false;
          for (int index2 = 0; index2 < this.SelectedRows[index1].ViewTemplate.Columns.Count; ++index2)
          {
            GridViewColumn column = (GridViewColumn) this.SelectedRows[index1].ViewTemplate.Columns[index2];
            if (column.IsVisible)
            {
              if (flag)
                this.FormatSeparator(content, format);
              string str = column.HeaderText == null ? string.Empty : column.HeaderText;
              this.FormatClipboardValueNoTags(content, (object) str, false, format);
              flag = true;
            }
          }
          this.FormatRowEnd(content, format, false);
        }
        this.FormatRowStart(content, format);
        bool flag1 = false;
        for (int index2 = 0; index2 < this.SelectedRows[index1].ViewTemplate.Columns.Count; ++index2)
        {
          GridViewColumn column = (GridViewColumn) this.SelectedRows[index1].ViewTemplate.Columns[index2];
          if (column.IsVisible)
          {
            bool flag2 = this.SelectedRows[index1][column] == null;
            string str = string.Empty;
            if (!flag2)
              str = this.GetFormattedCellValue(this.SelectedRows[index1], column);
            if (flag1)
              this.FormatSeparator(content, format);
            this.FormatClipboardValueNoTags(content, (object) str, isSingleCell, format);
            flag1 = true;
          }
        }
        this.FormatRowEnd(content, format, index1 == this.SelectedRows.Count - 1);
        if (cut)
          gridViewRowInfoList.Add(this.SelectedRows[index1]);
      }
      this.FormatClipboardEnd(content, format);
      for (int index = 0; index < gridViewRowInfoList.Count; ++index)
        gridViewRowInfoList[index].Delete();
    }

    private string GetFormattedCellValue(GridViewRowInfo row, GridViewColumn column)
    {
      object obj = row[column];
      string str1;
      if (obj is DateTime)
      {
        string formatString = ((GridViewDataColumn) column).FormatString;
        string str2 = string.IsNullOrEmpty(formatString) ? ((DateTime) obj).ToString("yyyy-MM-dd HH:mm tt") : string.Format(formatString, obj);
        str1 = Convert.ToString(this.GetCellCopyContent(row.Cells[column.Name], (object) str2));
      }
      else if (obj is Color)
      {
        string html = ColorTranslator.ToHtml((Color) obj);
        str1 = Convert.ToString(this.GetCellCopyContent(row.Cells[column.Name], (object) html));
      }
      else
      {
        string str2 = Convert.ToString(row[column]);
        str1 = Convert.ToString(this.GetCellCopyContent(row.Cells[column.Name], (object) str2));
      }
      return str1;
    }

    private void CopySelected(
      GridViewCellInfo[] cells,
      string format,
      bool cut,
      bool cutOperation,
      StringBuilder content)
    {
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      GridTraverser gridTraverser = new GridTraverser(this.MasterViewInfo, GridTraverser.TraversalModes.AllRows);
      gridTraverser.ProcessHierarchy = true;
      while (gridTraverser.MoveNext())
        gridViewRowInfoList.Add(gridTraverser.Current);
      SortedDictionary<int, SortedList<int, object>> sortedDictionary = new SortedDictionary<int, SortedList<int, object>>();
      for (int index = 0; index < cells.Length; ++index)
      {
        int key = gridViewRowInfoList.IndexOf(cells[index].RowInfo);
        SortedList<int, object> sortedList = (SortedList<int, object>) null;
        if (!sortedDictionary.TryGetValue(key, out sortedList))
        {
          sortedList = new SortedList<int, object>();
          sortedDictionary.Add(key, sortedList);
        }
        if (sortedList.ContainsKey(cells[index].ColumnInfo.Index))
          sortedList[cells[index].ColumnInfo.Index] = (object) this.GetFormattedCellValue(cells[index].RowInfo, cells[index].ColumnInfo);
        else if (cells[index].ColumnInfo.IsVisible)
          sortedList.Add(cells[index].ColumnInfo.Index, (object) this.GetFormattedCellValue(cells[index].RowInfo, cells[index].ColumnInfo));
        if (cut)
          cells[index].Value = (object) null;
      }
      if ((!cutOperation && this.ClipboardCopyMode == GridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText || cutOperation && this.ClipboardCutMode == GridViewClipboardCutMode.EnableAlwaysIncludeHeaderText) && (sortedDictionary.Values.Count > 0 && !sortedDictionary.ContainsKey(-1)))
      {
        SortedList<int, object> sortedList = new SortedList<int, object>();
        IEnumerator<SortedList<int, object>> enumerator = (IEnumerator<SortedList<int, object>>) sortedDictionary.Values.GetEnumerator();
        if (enumerator.MoveNext())
        {
          foreach (int key in (IEnumerable<int>) enumerator.Current.Keys)
          {
            sortedList.Add(key, (object) this.CurrentView.ViewTemplate.Columns[key].HeaderText);
            sortedDictionary[-1] = sortedList;
          }
        }
      }
      int num = 0;
      foreach (SortedList<int, object> sortedList in sortedDictionary.Values)
      {
        for (int index = 0; index < sortedList.Values.Count; ++index)
          this.FormatClipboardValue(content, sortedList.Values[index], index == 0, index == sortedList.Values.Count - 1, num == 0, num == sortedDictionary.Count - 1, format);
        ++num;
      }
    }

    private object GetCellCopyContent(GridViewCellInfo cell)
    {
      GridViewCellValueEventArgs args = new GridViewCellValueEventArgs(cell.RowInfo, cell.ColumnInfo);
      args.Value = cell.Value;
      this.EventDispatcher.RaiseEvent<GridViewCellValueEventArgs>(EventDispatcher.CellClipboardCopy, (object) this, args);
      return args.Value;
    }

    private object GetCellCopyContent(GridViewCellInfo cell, object suggestedValue)
    {
      GridViewCellValueEventArgs args = new GridViewCellValueEventArgs(cell.RowInfo, cell.ColumnInfo);
      args.Value = suggestedValue;
      this.EventDispatcher.RaiseEvent<GridViewCellValueEventArgs>(EventDispatcher.CellClipboardCopy, (object) this, args);
      return args.Value;
    }

    private object GetCellPasteContent(GridViewCellInfo cell, object value)
    {
      GridViewCellValueEventArgs args = new GridViewCellValueEventArgs(cell.RowInfo, cell.ColumnInfo);
      args.Value = value;
      bool isSuspended = this.EventDispatcher.IsSuspended;
      if (isSuspended)
        this.EventDispatcher.ResumeNotifications();
      this.EventDispatcher.RaiseEvent<GridViewCellValueEventArgs>(EventDispatcher.CellClipboardPaste, (object) this, args);
      if (isSuspended)
        this.EventDispatcher.SuspendNotifications();
      return args.Value;
    }

    private void FormatClipboardStart(StringBuilder sb, string format)
    {
      if (!string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
        return;
      sb.Append("<TABLE>");
    }

    private void FormatClipboardEnd(StringBuilder sb, string format)
    {
      if (!string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
        return;
      sb.Append("</TABLE>");
    }

    private void FormatRowStart(StringBuilder sb, string format)
    {
      if (!string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
        return;
      sb.Append("<TR>");
    }

    private void FormatRowEnd(StringBuilder sb, string format, bool isLastRow)
    {
      if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
        sb.Append("</TR>");
      if (!string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase) && !string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) && !string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase) || isLastRow)
        return;
      sb.Append('\r');
      sb.Append('\n');
    }

    private void FormatSeparator(StringBuilder sb, string format)
    {
      bool flag = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
      if (!flag && !string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) && !string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
        return;
      sb.Append(flag ? ',' : '\t');
    }

    private void FormatClipboardValueNoTags(
      StringBuilder sb,
      object cellValue,
      bool isSingleCell,
      string format)
    {
      if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
      {
        sb.Append("<TD>");
        if (cellValue != null)
          this.EncodeAsHtml(cellValue.ToString(), (TextWriter) new StringWriter(sb, (IFormatProvider) CultureInfo.CurrentCulture));
        else
          sb.Append("&nbsp;");
        sb.Append("</TD>");
      }
      else
      {
        bool csv = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
        if (!csv && !string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) && !string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
          return;
        if (cellValue == null)
          cellValue = (object) '\t';
        if (isSingleCell)
        {
          sb.Append(cellValue.ToString());
        }
        else
        {
          bool escapeApplied = false;
          int length = sb.Length;
          this.EncodeText(cellValue.ToString(), csv, (TextWriter) new StringWriter(sb, (IFormatProvider) CultureInfo.CurrentCulture), ref escapeApplied);
          if (!escapeApplied)
            return;
          sb.Insert(length, '"');
        }
      }
    }

    private void FormatClipboardValue(
      StringBuilder sb,
      object cellValue,
      bool firstCell,
      bool lastCell,
      bool inFirstRow,
      bool inLastRow,
      string format)
    {
      if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
      {
        if (firstCell)
        {
          if (inFirstRow)
            sb.Append("<TABLE>");
          sb.Append("<TR>");
        }
        sb.Append("<TD>");
        if (cellValue != null)
          this.EncodeAsHtml(cellValue.ToString(), (TextWriter) new StringWriter(sb, (IFormatProvider) CultureInfo.CurrentCulture));
        else
          sb.Append("&nbsp;");
        sb.Append("</TD>");
        if (!lastCell)
          return;
        sb.Append("</TR>");
        if (!inLastRow)
          return;
        sb.Append("</TABLE>");
      }
      else
      {
        bool csv = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
        if (!csv && !string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) && !string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
          return;
        if (cellValue == null)
          cellValue = (object) '\t';
        if (firstCell && lastCell && (inFirstRow && inLastRow))
        {
          sb.Append(cellValue.ToString());
        }
        else
        {
          bool escapeApplied = false;
          int length = sb.Length;
          this.EncodeText(cellValue.ToString(), csv, (TextWriter) new StringWriter(sb, (IFormatProvider) CultureInfo.CurrentCulture), ref escapeApplied);
          if (escapeApplied)
            sb.Insert(length, '"');
        }
        if (lastCell)
        {
          if (inLastRow)
            return;
          sb.Append('\r');
          sb.Append('\n');
        }
        else
          sb.Append(csv ? ',' : '\t');
      }
    }

    private void EncodeText(string s, bool csv, TextWriter output, ref bool escapeApplied)
    {
      if (s == null)
        return;
      int length = s.Length;
      for (int index = 0; index < length; ++index)
      {
        char ch = s[index];
        switch (ch)
        {
          case '\t':
            if (!csv)
            {
              output.Write(' ');
              break;
            }
            output.Write('\t');
            break;
          case '"':
            if (csv)
            {
              output.Write("\"\"");
              escapeApplied = true;
              break;
            }
            output.Write('"');
            break;
          case ',':
            if (csv)
              escapeApplied = true;
            output.Write(',');
            break;
          default:
            output.Write(ch);
            break;
        }
      }
      if (!escapeApplied)
        return;
      output.Write('"');
    }

    private void EncodeAsHtml(string s, TextWriter output)
    {
      if (s == null)
        return;
      int length = s.Length;
      char ch1 = char.MinValue;
      for (int index = 0; index < length; ++index)
      {
        char ch2 = s[index];
        switch (ch2)
        {
          case '\n':
            output.Write("<br>");
            goto case '\r';
          case '\r':
            ch1 = ch2;
            continue;
          case ' ':
            if (ch1 == ' ')
            {
              output.Write("&nbsp;");
              goto case '\r';
            }
            else
            {
              output.Write(ch2);
              goto case '\r';
            }
          case '"':
            output.Write("&quot;");
            goto case '\r';
          case '&':
            output.Write("&amp;");
            goto case '\r';
          case '<':
            output.Write("&lt;");
            goto case '\r';
          case '>':
            output.Write("&gt;");
            goto case '\r';
          default:
            if (ch2 >= ' ' && ch2 < 'Ā')
            {
              output.Write("&#");
              output.Write(((int) ch2).ToString((IFormatProvider) NumberFormatInfo.InvariantInfo));
              output.Write(';');
              goto case '\r';
            }
            else
            {
              output.Write(ch2);
              goto case '\r';
            }
        }
      }
    }

    private void GetHtmlContent(StringBuilder sbContent, out MemoryStream utf8Stream)
    {
      int num = 135 + Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(sbContent.ToString())).Length;
      int count = num + 36;
      string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Version:1.0\r\nStartHTML:00000097\r\nEndHTML:{0}\r\nStartFragment:00000133\r\nEndFragment:{1}\r\n", (object) count.ToString("00000000", (IFormatProvider) CultureInfo.InvariantCulture), (object) num.ToString("00000000", (IFormatProvider) CultureInfo.InvariantCulture)) + "<HTML>\r\n<BODY>\r\n<!--StartFragment-->";
      sbContent.Insert(0, str);
      sbContent.Append("\r\n<!--EndFragment-->\r\n</BODY>\r\n</HTML>");
      byte[] buffer = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(sbContent.ToString()));
      utf8Stream = new MemoryStream(count + 1);
      utf8Stream.Write(buffer, 0, count);
      utf8Stream.WriteByte((byte) 0);
    }

    private List<List<string>> GetTextData()
    {
      List<List<string>> stringListList = new List<List<string>>();
      StringTokenizer stringTokenizer = new StringTokenizer(Clipboard.GetData(DataFormats.UnicodeText).ToString(), "\n");
      for (int index = stringTokenizer.Count(); index > 0; --index)
      {
        List<string> stringList = new List<string>();
        stringListList.Add(stringList);
        string str1 = stringTokenizer.NextToken();
        char[] chArray = new char[1]{ '\t' };
        foreach (string str2 in str1.Split(chArray))
          stringList.Add(str2.Trim());
      }
      return stringListList;
    }

    private List<List<string>> GetHtmlData()
    {
      List<List<string>> stringListList = new List<List<string>>();
      List<string> stringList = new List<string>();
      StringTokenizer stringTokenizer = new StringTokenizer(Clipboard.GetData(DataFormats.Html).ToString(), "<");
      for (int index = stringTokenizer.Count(); index > 0; --index)
      {
        string str1 = stringTokenizer.NextToken();
        if (str1.Equals("tr>", StringComparison.InvariantCultureIgnoreCase))
        {
          stringList = new List<string>();
          stringListList.Add(stringList);
        }
        if (str1.StartsWith("td>", StringComparison.InvariantCultureIgnoreCase))
        {
          int num = str1.IndexOf('>');
          if (num > 0)
          {
            string str2 = str1.Remove(0, num + 1).Replace("&nbsp;", " ").Replace("&quot;", "\"").Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");
            stringList.Add(str2);
          }
        }
      }
      return stringListList;
    }

    private List<List<string>> GetCsvData()
    {
      List<List<string>> stringListList = new List<List<string>>();
      StringTokenizer stringTokenizer1 = new StringTokenizer(Clipboard.GetData(DataFormats.CommaSeparatedValue).ToString(), "\n");
      for (int index1 = stringTokenizer1.Count(); index1 > 0; --index1)
      {
        List<string> stringList = new List<string>();
        stringListList.Add(stringList);
        StringTokenizer stringTokenizer2 = new StringTokenizer(stringTokenizer1.NextToken(), ",");
        for (int index2 = stringTokenizer2.Count(); index2 > 0; --index2)
        {
          string str = stringTokenizer2.NextToken().Trim();
          stringList.Add(str);
        }
      }
      return stringListList;
    }

    [Browsable(false)]
    [DefaultValue(GridViewClipboardCutMode.EnableWithoutHeaderText)]
    public GridViewClipboardCutMode ClipboardCutMode
    {
      get
      {
        return this.clipboardCutMode;
      }
      set
      {
        if (this.clipboardCutMode == value)
          return;
        this.clipboardCutMode = value;
        this.OnNotifyPropertyChanged(nameof (ClipboardCutMode));
      }
    }

    [Browsable(false)]
    [DefaultValue(GridViewClipboardCopyMode.EnableWithoutHeaderText)]
    public GridViewClipboardCopyMode ClipboardCopyMode
    {
      get
      {
        return this.clipboardCopyMode;
      }
      set
      {
        if (this.clipboardCopyMode == value)
          return;
        this.clipboardCopyMode = value;
        this.OnNotifyPropertyChanged(nameof (ClipboardCopyMode));
      }
    }

    [Browsable(false)]
    [DefaultValue(GridViewClipboardPasteMode.Enable)]
    public GridViewClipboardPasteMode ClipboardPasteMode
    {
      get
      {
        return this.clipboardPasteMode;
      }
      set
      {
        if (this.clipboardPasteMode == value)
          return;
        this.clipboardPasteMode = value;
        this.OnNotifyPropertyChanged(nameof (ClipboardPasteMode));
      }
    }

    public virtual DataObject GetClipboardContent()
    {
      return this.CopyContent(false);
    }

    public virtual void Cut()
    {
      Clipboard.SetDataObject((object) this.CopyContent(true));
    }

    public virtual void Copy()
    {
      Clipboard.SetDataObject((object) this.CopyContent(false));
    }

    public virtual void Paste()
    {
      if (this.ClipboardPasteMode == GridViewClipboardPasteMode.Disable || this.ReadOnly || !this.AllowEditRow)
        return;
      List<List<string>> clipboardData = this.GetClipboardData();
      if (this.ownerGrid.CurrentColumn == null || this.ownerGrid.CurrentColumn.Index == -1 || this.CurrentRow == null)
        return;
      if (this.CurrentRow.Index == -1)
        return;
      try
      {
        if (this.ClipboardPasteMode != GridViewClipboardPasteMode.EnableWithNotifications)
          this.CurrentView.ViewTemplate.BeginUpdate();
        List<GridViewRowInfo> rowsToPasteIn = this.GetRowsToPasteIn(clipboardData.Count);
        for (int index = 0; index < clipboardData.Count; ++index)
        {
          if (index < rowsToPasteIn.Count)
            this.PasteDataToRow(clipboardData[index], rowsToPasteIn[index]);
          else
            this.PasteDataToNewRow(clipboardData[index]);
        }
      }
      catch (Exception ex)
      {
        this.SetError(new GridViewCellCancelEventArgs((GridViewRowInfo) null, (GridViewColumn) null, (IInputEditor) null), ex);
      }
      finally
      {
        if (this.ClipboardPasteMode != GridViewClipboardPasteMode.EnableWithNotifications)
        {
          GridTableElement currentView = this.ownerGrid.GridViewElement.CurrentView as GridTableElement;
          int num1 = currentView.VScrollBar.Value;
          int num2 = currentView.HScrollBar.Value;
          this.CurrentView.ViewTemplate.EndUpdate();
          currentView.VScrollBar.Value = num1;
          currentView.HScrollBar.Value = num2;
        }
      }
    }

    protected virtual List<List<string>> GetClipboardData()
    {
      IDataObject dataObject = Clipboard.GetDataObject();
      List<List<string>> stringListList = new List<List<string>>();
      if (Clipboard.ContainsData(DataFormats.Html))
      {
        GridViewClipboardEventArgs args = new GridViewClipboardEventArgs(false, DataFormats.Html, dataObject as DataObject, (GridViewTemplate) this);
        this.EventDispatcher.RaiseEvent<GridViewClipboardEventArgs>(EventDispatcher.Pasting, (object) this, args);
        if (!args.Cancel)
          stringListList = this.GetHtmlData();
      }
      if (stringListList.Count == 0 && (Clipboard.ContainsData(DataFormats.UnicodeText) || Clipboard.ContainsData(DataFormats.Text)))
      {
        GridViewClipboardEventArgs args = new GridViewClipboardEventArgs(false, DataFormats.Text, dataObject as DataObject, (GridViewTemplate) this);
        this.EventDispatcher.RaiseEvent<GridViewClipboardEventArgs>(EventDispatcher.Pasting, (object) this, args);
        if (!args.Cancel)
          stringListList = this.GetTextData();
      }
      if (stringListList.Count == 0 && Clipboard.ContainsData(DataFormats.CommaSeparatedValue))
      {
        GridViewClipboardEventArgs args = new GridViewClipboardEventArgs(false, DataFormats.CommaSeparatedValue, dataObject as DataObject, (GridViewTemplate) this);
        this.EventDispatcher.RaiseEvent<GridViewClipboardEventArgs>(EventDispatcher.Pasting, (object) this, args);
        if (!args.Cancel)
          stringListList = this.GetCsvData();
      }
      return stringListList;
    }

    protected virtual List<GridViewRowInfo> GetRowsToPasteIn(
      int numberOfRowsToGet)
    {
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      if (this.IsSelfReference)
      {
        PrintGridTraverser printGridTraverser = new PrintGridTraverser(this.CurrentView);
        printGridTraverser.GoToRow(this.CurrentRow);
        int num = 0;
        while (num < numberOfRowsToGet)
        {
          gridViewRowInfoList.Add(printGridTraverser.Current);
          ++num;
          if (!printGridTraverser.MoveNext())
            break;
        }
      }
      else
      {
        GridViewChildRowCollection childRowCollection = this.CurrentView.CurrentRow.Parent != null ? this.CurrentView.CurrentRow.Parent.ChildRows : this.CurrentView.CurrentRow.ViewTemplate.ChildRows;
        int index1 = this.CurrentView.CurrentRow.Index;
        for (int index2 = 0; index2 + index1 < childRowCollection.Count && index2 < numberOfRowsToGet; ++index2)
          gridViewRowInfoList.Add(childRowCollection[index2 + index1]);
      }
      return gridViewRowInfoList;
    }

    protected virtual void PasteDataToRow(List<string> rowData, GridViewRowInfo row)
    {
      int index1 = this.ownerGrid.CurrentColumn.Index - 1;
      int index2 = 0;
      while (index2 < rowData.Count)
      {
        ++index1;
        if (index1 >= this.CurrentView.ViewTemplate.Columns.Count)
          break;
        GridViewColumn column = (GridViewColumn) this.CurrentView.ViewTemplate.Columns[index1];
        if (column.IsVisible && !column.ReadOnly && !row.Cells[index1].ReadOnly)
        {
          object obj = (object) rowData[index2];
          if (string.IsNullOrEmpty(rowData[index2]))
            obj = (object) null;
          else if ((object) this.CurrentView.ViewTemplate.Columns[index1].DataType == (object) typeof (string))
          {
            GridViewTextBoxColumn viewTextBoxColumn = column as GridViewTextBoxColumn;
            if (viewTextBoxColumn != null && viewTextBoxColumn.MaxLength > 0 && rowData[index2].Length > viewTextBoxColumn.MaxLength)
              obj = (object) rowData[index2].Substring(0, viewTextBoxColumn.MaxLength);
          }
          else if ((object) this.CurrentView.ViewTemplate.Columns[index1].DataType == (object) typeof (DateTime))
          {
            try
            {
              obj = (object) DateTime.Parse(rowData[index2], (IFormatProvider) this.CurrentView.ViewTemplate.Columns[index1].FormatInfo);
            }
            catch
            {
            }
          }
          else if ((object) this.CurrentView.ViewTemplate.Columns[index1].DataType == (object) typeof (Color))
          {
            try
            {
              obj = (object) ColorTranslator.FromHtml(rowData[index2]);
            }
            catch
            {
            }
          }
          object cellPasteContent = this.GetCellPasteContent(row.Cells[column.Name], obj);
          if (this.ClipboardPasteMode == GridViewClipboardPasteMode.EnableWithNotifications)
          {
            CellValidatingEventArgs args1 = new CellValidatingEventArgs(row, column, cellPasteContent, row.Cells[index1].Value, (IInputEditor) null);
            this.EventDispatcher.RaiseEvent<CellValidatingEventArgs>(EventDispatcher.CellValidating, (object) this, args1);
            if (!args1.Cancel)
            {
              row.Cells[index1].Value = cellPasteContent;
              CellValidatedEventArgs args2 = new CellValidatedEventArgs(row, column, cellPasteContent);
              this.EventDispatcher.RaiseEvent<CellValidatedEventArgs>(EventDispatcher.CellValidated, (object) this, args2);
            }
          }
          else
            row.Cells[index1].Value = cellPasteContent;
          ++index2;
        }
      }
    }

    protected virtual void PasteDataToNewRow(List<string> rowData)
    {
      GridViewRowInfo row = this.CurrentView.ViewTemplate.Rows.NewRow();
      int index1 = this.ownerGrid.CurrentColumn.Index;
      for (int index2 = 0; index2 < rowData.Count && index1 < this.CurrentView.ViewTemplate.ColumnCount; ++index1)
      {
        GridViewColumn column = (GridViewColumn) this.CurrentView.ViewTemplate.Columns[index1];
        if (column.IsVisible && !column.ReadOnly && !row.Cells[index1].ReadOnly)
        {
          object obj = (object) rowData[index2];
          if ((object) this.CurrentView.ViewTemplate.Columns[index1].DataType == (object) typeof (DateTime))
          {
            if (string.IsNullOrEmpty(rowData[index2]))
            {
              obj = (object) null;
            }
            else
            {
              try
              {
                obj = (object) DateTime.ParseExact(rowData[index2], "yyyy-MM-dd HH:mm tt", (IFormatProvider) null);
              }
              catch
              {
              }
            }
          }
          else if ((object) this.CurrentView.ViewTemplate.Columns[index1].DataType == (object) typeof (Color))
          {
            if (string.IsNullOrEmpty(rowData[index2]))
            {
              obj = (object) null;
            }
            else
            {
              try
              {
                obj = (object) ColorTranslator.FromHtml(rowData[index2]);
              }
              catch
              {
              }
            }
          }
          if (this.ClipboardPasteMode == GridViewClipboardPasteMode.EnableWithNotifications)
          {
            CellValidatingEventArgs args1 = new CellValidatingEventArgs(row, column, obj, row.Cells[index1].Value, (IInputEditor) null);
            this.EventDispatcher.RaiseEvent<CellValidatingEventArgs>(EventDispatcher.CellValidating, (object) this, args1);
            if (!args1.Cancel)
            {
              row.Cells[index1].Value = obj;
              CellValidatedEventArgs args2 = new CellValidatedEventArgs(row, column, obj);
              this.EventDispatcher.RaiseEvent<CellValidatedEventArgs>(EventDispatcher.CellValidated, (object) this, args2);
            }
          }
          else
            row.Cells[index1].Value = obj;
          ++index2;
        }
      }
      this.CurrentView.ViewTemplate.Rows.Add(row);
    }

    protected override bool AnalyzeQueueCore(List<GridViewEvent> events)
    {
      List<GridViewEvent> all = events.FindAll(new Predicate<GridViewEvent>(this.IsDisposedEvent));
      if (all.Count <= 0)
        return false;
      foreach (GridViewEvent gridViewEvent in all)
        events.Remove(gridViewEvent);
      events.AddRange((IEnumerable<GridViewEvent>) all);
      return true;
    }

    private bool IsDisposedEvent(GridViewEvent gridViewEvent)
    {
      return gridViewEvent.Info.Id == KnownEvents.Dispose;
    }
  }
}
