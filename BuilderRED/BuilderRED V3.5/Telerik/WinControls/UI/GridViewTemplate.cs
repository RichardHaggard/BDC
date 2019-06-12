// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewTemplate
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Telerik.Data.Expressions;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [DesignTimeVisible(false)]
  public class GridViewTemplate : Component, IDataItemSource, IHierarchicalRow, INotifyPropertyChangingEx, INotifyPropertyChanged, ISupportInitializeNotification, ISupportInitialize, IGridViewEventListener
  {
    private string caption = string.Empty;
    private int summaryRowsVersion = -1;
    private TabPositions childViewTabsPosition = TabPositions.Top;
    private GridViewAutoSizeColumnsMode autoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
    private readonly List<GridViewDataColumn> excelFilteredColumns = new List<GridViewDataColumn>();
    private const int STRING_BUFFER = 1024;
    private const long ReadOnlyState = 1;
    private const long CurrentUpdateState = 2;
    private const long IsInitializedState = 4;
    private const long EnableAlternatingRowColorState = 8;
    private const long AllowRowReorderState = 16;
    private const long AllowDeleteRowState = 32;
    private const long AllowCellContextMenuState = 64;
    private const long AllowAutosizeColumnsState = 128;
    private const long AllowColumnResizeState = 256;
    private const long AllowColumnHeaderContextMenuState = 512;
    private const long AllowRowHeaderContextMenuState = 1024;
    private const long AllowColumnRemoveState = 2048;
    private const long AllowColumnChooserState = 4096;
    private const long AllowEditRowState = 8192;
    private const long AllowAddNewRowState = 16384;
    private const long AllowSearchRowState = 32768;
    private const long AllowNaturalSortState = 65536;
    private const long AllowMultiColumnSortingState = 131072;
    private const long AllowRowResizeState = 262144;
    private const long AllowColumnReorderState = 524288;
    private const long AllowDragToGroupState = 1048576;
    private const long ShowRowHeaderColumnState = 2097152;
    private const long ShowGroupedColumnsState = 4194304;
    private const long AutoExpandGroupsState = 8388608;
    private const long AutoGenerateColumnsState = 16777216;
    private const long HierarchyModeState = 33554432;
    private const long DirtyChildRowsState = 67108864;
    private const long ShowTotalsState = 134217728;
    private const long ShowParentGroupSummariesState = 268435456;
    private const long ContainsCurrentRowState = 536870912;
    private const long ShowChildViewTabsAlwaysState = 1073741824;
    private const long ContainsColumnExpressionState = 2147483648;
    private const long EnableCustomFilteringState = 4294967296;
    private const long EnableHierarchyFilteringState = 8589934592;
    private const long EnableCustomSortingState = 17179869184;
    private const long EnableCustomGroupingState = 34359738368;
    private const long ShowFilterCellOperatorTextState = 68719476736;
    private const long ParentCollectionAddRangeState = 137438953472;
    private const long ProcessingDataState = 274877906944;
    private const long AutoUpdateObjectRelationalSourceState = 549755813888;
    private RadBitVector64 state;
    private int update;
    private object tag;
    private string newRowText;
    private ISite ownerSite;
    private BestFitQueue bestFitQueue;
    private GridViewTemplate parent;
    private BindingContext bindingContext;
    private GridViewListSource listSource;
    private GridViewInfo gridViewInfo;
    private GridViewColumn currentColumn;
    private GridViewColumnCollection columns;
    private List<GridViewColumn> pinnedColumns;
    private GridViewRowCollection rows;
    private GridViewHierarchyDataProvider hierarchyDataProvider;
    private GridViewTemplateCollection childGridViewTemplates;
    private GridViewSummaryRowItemCollection summaryRowGroupHeaders;
    private GridViewSummaryRowItemCollection summaryRowsBottom;
    private GridViewSummaryRowItemCollection summaryRowsTop;
    private FilterExpressionCollection filteringCollection;
    private GridGroupByExpressionCollection groupingCollection;
    private RadSortExpressionCollection sortCollection;
    private IGridViewDefinition viewDefinition;
    private IGridViewDefinition cachedViewDefinition;
    private ScrollState horizontalScrollState;
    private ScrollState verticalScrollState;
    private GridViewBottomPinnedRowsMode bottomPinnedRowsMode;
    private SystemRowPosition addNewRowPosition;
    private SystemRowPosition searchRowPosition;
    private GridViewRelation relation;
    private bool showHeaderCellButtons;
    internal bool dataSourceChanging;
    private bool rowExpandedWhileEventDispatcherWasSuspended;
    internal GridViewRowInfo CurrentRowToSetOnEndUpdate;
    private Telerik.Data.Expressions.ExpressionNode expressionNode;
    private byte[] metaDataHash;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public List<GridViewDataColumn> ExcelFilteredColumns
    {
      get
      {
        return this.excelFilteredColumns;
      }
    }

    public event DataViewChangedEventHandler ViewChanged;

    public event GridViewCreateRowInfoEventHandler CreateRowInfo;

    protected internal virtual void OnCreateRowInfo(GridViewCreateRowInfoEventArgs e)
    {
      GridViewCreateRowInfoEventHandler createRowInfo = this.CreateRowInfo;
      if (createRowInfo != null)
        createRowInfo((object) this, e);
      if (this.MasterTemplate == null)
        return;
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCreateRowInfoEventArgs>(EventDispatcher.CreateRowInfo, (object) this, e);
    }

    public GridViewTemplate()
    {
      this.state = new RadBitVector64(0L);
      this.state[32L] = true;
      this.state[64L] = true;
      this.state[128L] = true;
      this.state[256L] = true;
      this.state[512L] = true;
      this.state[1024L] = true;
      this.state[2048L] = true;
      this.state[4096L] = true;
      this.state[8192L] = true;
      this.state[16384L] = true;
      this.state[32768L] = false;
      this.state[65536L] = false;
      this.state[131072L] = true;
      this.state[262144L] = true;
      this.state[524288L] = true;
      this.state[1048576L] = true;
      this.state[2097152L] = true;
      this.state[16777216L] = true;
      this.state[4L] = true;
      this.state[68719476736L] = true;
      this.state[549755813888L] = true;
      this.summaryRowGroupHeaders = new GridViewSummaryRowItemCollection();
      this.summaryRowGroupHeaders.CollectionChanged += new NotifyCollectionChangedEventHandler(this.summaryRowGroupHeaders_CollectionChanged);
      this.summaryRowsBottom = new GridViewSummaryRowItemCollection();
      this.summaryRowsBottom.CollectionChanged += new NotifyCollectionChangedEventHandler(this.summaryRows_CollectionChanged);
      this.summaryRowsTop = new GridViewSummaryRowItemCollection();
      this.summaryRowsTop.CollectionChanged += new NotifyCollectionChangedEventHandler(this.summaryRows_CollectionChanged);
      this.bindingContext = this.CreateBindingContext();
      this.listSource = this.CreateListSource();
      this.listSource.CollectionView.LoadData((IEnumerable<GridViewRowInfo>) this.listSource);
      this.childGridViewTemplates = new GridViewTemplateCollection(this);
      this.columns = new GridViewColumnCollection(this);
      this.rows = new GridViewRowCollection(this);
      this.pinnedColumns = new List<GridViewColumn>();
      this.WireEvents();
      this.filteringCollection = new FilterExpressionCollection(this);
      this.sortCollection = (RadSortExpressionCollection) this.listSource.CollectionView.SortDescriptors;
      this.groupingCollection = (GridGroupByExpressionCollection) this.listSource.CollectionView.GroupDescriptors;
      this.gridViewInfo = new GridViewInfo(this);
      this.viewDefinition = (IGridViewDefinition) new TableViewDefinition();
      this.bestFitQueue = new BestFitQueue(this);
    }

    protected virtual GridViewListSource CreateListSource()
    {
      return new GridViewListSource(this, (RadCollectionView<GridViewRowInfo>) null);
    }

    protected virtual BindingContext CreateBindingContext()
    {
      return new BindingContext();
    }

    protected override void Dispose(bool disposing)
    {
      this.CurrentRowToSetOnEndUpdate = (GridViewRowInfo) null;
      this.UnwireEvents();
      this.listSource.Dispose();
      if (this.hierarchyDataProvider != null)
      {
        this.hierarchyDataProvider.Dispose();
        this.hierarchyDataProvider = (GridViewHierarchyDataProvider) null;
      }
      base.Dispose(disposing);
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.Data;
      }
    }

    GridEventProcessMode IGridViewEventListener.DesiredProcessMode
    {
      get
      {
        return this.GetEventProcessMode();
      }
    }

    EventListenerPriority IGridViewEventListener.Priority
    {
      get
      {
        return this.GetEventListenerPirotiy();
      }
    }

    GridViewEventResult IGridViewEventListener.ProcessEvent(
      GridViewEvent eventData)
    {
      return this.ProcessEventCore(eventData);
    }

    GridViewEventResult IGridViewEventListener.PreProcessEvent(
      GridViewEvent eventData)
    {
      return this.PreProcessEventCore(eventData);
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

    protected virtual GridEventProcessMode GetEventProcessMode()
    {
      return GridEventProcessMode.Process | GridEventProcessMode.PostProcess;
    }

    protected virtual EventListenerPriority GetEventListenerPirotiy()
    {
      return EventListenerPriority.High;
    }

    protected virtual GridViewEventResult PreProcessEventCore(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessEventCore(
      GridViewEvent eventData)
    {
      GridViewColumn sender = eventData.Sender as GridViewColumn;
      if (sender != null && sender.OwnerTemplate == eventData.Originator && eventData.Info.Id == KnownEvents.PropertyChanged)
        return sender.OwnerTemplate.ProcessColumnPropertyChangedEvent(sender, eventData.Arguments[0] as RadPropertyChangedEventArgs);
      if (eventData.Arguments != null && eventData.Arguments.Length > 0)
      {
        DataViewChangedEventArgs changedEventArgs = eventData.Arguments[0] as DataViewChangedEventArgs;
        if (changedEventArgs != null && (changedEventArgs.Action == ViewChangedAction.Remove || changedEventArgs.Action == ViewChangedAction.Add))
          this.RefreshAggregates(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, changedEventArgs.NewItems));
      }
      if (eventData.Info.Id == KnownEvents.SummaryItemChanged)
        (eventData.Originator as GridViewTemplate)?.RefreshAggregates();
      if (eventData.Info.Id == KnownEvents.HierarchyChanged)
        ((GridViewTemplate) eventData.Originator)?.EnsureHierarchyProvider();
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessColumnPropertyChangedEvent(
      GridViewColumn column,
      RadPropertyChangedEventArgs e)
    {
      RadProperty property = e.Property;
      if (column is GridViewDataColumn && (property == GridViewColumn.ExpressionProperty || property == GridViewColumn.FieldNameProperty))
      {
        if (string.IsNullOrEmpty(column.Expression))
        {
          foreach (GridViewRowInfo row in this.Rows)
            row.Cache[column] = (object) null;
        }
        this.RebuildColumnAccessors();
      }
      else if (property == GridViewDataColumn.DataTypeProperty && e.NewValue != null)
        this.MergeFilterDescriptors(column.FieldName, (System.Type) e.NewValue);
      else if (property == GridViewColumn.PinPositionProperty)
      {
        if ((PinnedColumnPosition) e.NewValue == PinnedColumnPosition.None)
        {
          this.ResetSystemRowsCache(column);
          this.pinnedColumns.Remove(column);
        }
        else if (this.pinnedColumns.IndexOf(column) == -1)
          this.pinnedColumns.Add(column);
      }
      return (GridViewEventResult) null;
    }

    private void RemoveGroupDescriptor(GridViewColumn column)
    {
      for (int index1 = this.GroupDescriptors.Count - 1; index1 >= 0; --index1)
      {
        GroupDescriptor groupDescriptor = this.GroupDescriptors[index1];
        for (int index2 = groupDescriptor.GroupNames.Count - 1; index2 >= 0; --index2)
        {
          if (groupDescriptor.GroupNames[index2].PropertyName == column.Name)
            groupDescriptor.GroupNames.RemoveAt(index2);
        }
        if (groupDescriptor.GroupNames.Count == 0)
          this.GroupDescriptors.RemoveAt(index1);
      }
    }

    protected virtual void DispatchEvent(GridViewEvent gridEvent)
    {
      this.MasterTemplate?.SynchronizationService.DispatchEvent(gridEvent);
    }

    protected virtual void SuspendEvent(KnownEvents eventId)
    {
      this.MasterTemplate?.SynchronizationService.SuspendEvent(eventId);
    }

    protected virtual void ResumeEvent(KnownEvents eventId)
    {
      this.MasterTemplate?.SynchronizationService.ResumeEvent(eventId);
    }

    protected virtual void DispatchEvent(GridViewEvent gridEvent, bool postUI)
    {
      GridViewSynchronizationService.DispatchEvent(this, gridEvent, postUI);
    }

    IDataItem IDataItemSource.NewItem()
    {
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs(this.Templates.Count > 0 || this.HierarchyDataProvider != null ? (GridViewRowInfo) new GridViewHierarchyRowInfo(this.gridViewInfo) : (GridViewRowInfo) new GridViewDataRowInfo(this.gridViewInfo), this.gridViewInfo);
      this.OnCreateRowInfo(e);
      return (IDataItem) e.RowInfo;
    }

    void IDataItemSource.Initialize()
    {
      this.BeginCurrentRowUpdate(true);
      bool flag = this.IsFilteringPerformed();
      if (!flag)
      {
        this.ResetCurrentRow();
        this.ResetCurrentColumn();
      }
      if (this.listSource.IsDataBound)
        this.InitializeBoundColumns();
      else if (this.Site == null && this.OwnerSite == null || !this.state[137438953472L])
        this.ClearAutoGeneratedColumns();
      if (this.columns.Count > 0 && !flag)
        this.SetCurrentColumn(MasterGridViewTemplate.GetColumnAllowingForCurrent(this), true);
      this.RebuildColumnAccessors();
      this.PurgeDataOperation();
      if (this.hierarchyDataProvider != null)
      {
        this.hierarchyDataProvider.SuspendNotifications();
        this.hierarchyDataProvider.Refresh();
        this.hierarchyDataProvider.ResumeNotifications();
      }
      this.EndCurrentRowUpdate(true, true);
    }

    void IDataItemSource.MetadataChanged(PropertyDescriptor pd)
    {
      if (!this.AutoGenerateColumns)
        return;
      for (int index = 0; index < this.Columns.Count; ++index)
      {
        PropertyDescriptor propertyDescriptor = this.ListSource.BoundProperties.Find(this.Columns[index].FieldName, true);
        if (pd == propertyDescriptor)
          return;
      }
      GridViewDataColumn column = this.AutoGenerateGridColumnFromTypeConverter(pd) ?? GridViewHelper.AutoGenerateGridColumn(pd.PropertyType, (ISite) null);
      column.FieldName = pd.Name;
      string str = GridViewHelper.GetCaption(pd) ?? pd.DisplayName;
      column.HeaderText = str;
      column.IsAutoGenerated = true;
      column.ReadOnly = pd.IsReadOnly;
      column.OwnerTemplate = this;
      this.columns.Add(column);
      this.UpdateAccessor(column);
    }

    void IDataItemSource.BindingComplete()
    {
      if (!this.listSource.IsDataBound)
        return;
      this.EventDispatcher.RaiseEvent<GridViewBindingCompleteEventArgs>(EventDispatcher.DataBindingComplete, (object) this, new GridViewBindingCompleteEventArgs(ListChangedType.Reset));
    }

    private bool IsFilteringPerformed()
    {
      if (this.MasterTemplate != null)
        return this.MasterTemplate.SynchronizationService.ContainsEvent(new Predicate<GridViewEvent>(this.IsFilterExpressionChanged));
      return false;
    }

    private bool IsFilterExpressionChanged(GridViewEvent gridEvent)
    {
      if (gridEvent.Info.Id == KnownEvents.ViewChanged)
        return (gridEvent.Arguments[0] as DataViewChangedEventArgs).Action == ViewChangedAction.FilterExpressionChanged;
      return false;
    }

    private void InitializeBoundColumns()
    {
      if (this.ListSource.BoundProperties != null)
      {
        foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) this.Columns)
        {
          if (!column.IsAutoGenerated && column.GetValueSource(GridViewDataColumn.DataTypeProperty) != ValueSource.Local)
          {
            PropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(column);
            if (propertyDescriptor != null)
              this.SetColumnDataType(column, propertyDescriptor.PropertyType);
          }
        }
        if (this.Parent == null && this.Rows.Count > 0)
        {
          object dataBoundItem = this.Rows[0].DataBoundItem;
          foreach (GridViewTemplate template in (Collection<GridViewTemplate>) this.Templates)
          {
            if (!this.InitializeChildColumnsInRelationalHierarchy(template, dataBoundItem))
              break;
          }
        }
      }
      if (!this.AutoGenerateColumns || this.MergeAndClearSchema() && !this.DesignMode)
        return;
      PropertyDescriptorCollection boundProperties = this.ListSource.BoundProperties;
      if (boundProperties == null)
        return;
      this.AutoGenerateBoundColumns(boundProperties);
    }

    private bool InitializeChildColumnsInRelationalHierarchy(
      GridViewTemplate template,
      object dataBoundItem)
    {
      if (dataBoundItem == null || !(template.HierarchyDataProvider is GridViewObjectRelationalDataProvider) || template.relation == null)
        return false;
      PropertyDescriptor propertyDescriptor1 = ListBindingHelper.GetListItemProperties(dataBoundItem).Find(template.relation.ChildColumnNames[0], true);
      if (propertyDescriptor1 != null)
      {
        IEnumerable enumerable = propertyDescriptor1.GetValue(dataBoundItem) as IEnumerable;
        if (enumerable != null)
        {
          IEnumerator enumerator = enumerable.GetEnumerator();
          if (enumerator != null && enumerator.MoveNext())
          {
            dataBoundItem = enumerator.Current;
            enumerator.Reset();
          }
          PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties((object) enumerable);
          if (template.AutoGenerateColumns)
          {
            template.AutoGenerateBoundColumns(listItemProperties);
          }
          else
          {
            foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) template.Columns)
            {
              PropertyDescriptor propertyDescriptor2 = listItemProperties.Find(column.FieldName, true);
              if (propertyDescriptor2 == null && column.FieldName.Contains("."))
              {
                string[] strArray = column.FieldName.Split('.');
                propertyDescriptor2 = listItemProperties.Find(strArray[0], true);
                template.ListSource.BoundProperties.Add(propertyDescriptor2);
                column.Accessor = (Accessor) new BoundAccessor((GridViewColumn) column);
                for (int index = 1; index < strArray.Length && propertyDescriptor2 != null; ++index)
                  propertyDescriptor2 = propertyDescriptor2.GetChildProperties().Find(strArray[index], true);
              }
              else
                template.ListSource.BoundProperties.Add(propertyDescriptor2);
              if (propertyDescriptor2 != null)
                this.SetColumnDataType(column, propertyDescriptor2.PropertyType);
            }
            foreach (PropertyDescriptor propertyDescriptor2 in listItemProperties)
            {
              GridViewDataColumn[] columnByFieldName = template.Columns.GetColumnByFieldName(propertyDescriptor2.Name);
              if (columnByFieldName.Length > 0)
                this.SetColumnDataType(columnByFieldName[0], propertyDescriptor2.PropertyType);
            }
          }
        }
      }
      foreach (GridViewTemplate template1 in (Collection<GridViewTemplate>) template.Templates)
      {
        if (!this.InitializeChildColumnsInRelationalHierarchy(template1, dataBoundItem))
          break;
      }
      return true;
    }

    private PropertyDescriptor GetPropertyDescriptor(GridViewDataColumn column)
    {
      PropertyDescriptor propertyDescriptor = this.ListSource.BoundProperties.Find(column.FieldName, true);
      if (propertyDescriptor == null && column.FieldName.Contains("."))
      {
        string[] strArray = column.FieldName.Split('.');
        propertyDescriptor = this.ListSource.BoundProperties.Find(strArray[0], true);
        for (int index = 1; index < strArray.Length && propertyDescriptor != null; ++index)
          propertyDescriptor = propertyDescriptor.GetChildProperties().Find(strArray[index], true);
      }
      return propertyDescriptor;
    }

    internal void AutoGenerateBoundColumns(PropertyDescriptorCollection boundProperties)
    {
      this.columns.BeginUpdate();
      for (int index = 0; index < boundProperties.Count; ++index)
      {
        if ((object) boundProperties[index].PropertyType != (object) typeof (IBindingList))
        {
          string name = boundProperties[index].Name;
          if (this.Columns.GetColumnByFieldName(name).Length <= 0 && !this.HasSubPropertyColumns(boundProperties[index]))
          {
            GridViewDataColumn gridViewDataColumn = this.AutoGenerateGridColumnFromTypeConverter(boundProperties[index]) ?? GridViewHelper.AutoGenerateGridColumn(boundProperties[index].PropertyType, this.OwnerSite ?? this.Site);
            gridViewDataColumn.FieldName = name;
            string str = GridViewHelper.GetCaption(boundProperties[index]) ?? boundProperties[index].DisplayName;
            gridViewDataColumn.HeaderText = str;
            gridViewDataColumn.IsAutoGenerated = true;
            gridViewDataColumn.ReadOnly = boundProperties[index].IsReadOnly;
            gridViewDataColumn.OwnerTemplate = this;
            this.columns.Add(gridViewDataColumn);
          }
        }
      }
      this.columns.EndUpdate(false);
    }

    private bool HasSubPropertyColumns(PropertyDescriptor property)
    {
      if (property.GetChildProperties().Count > 0 && property.GetType().Name != "DataColumnPropertyDescriptor")
      {
        for (int index = 0; index < this.columns.Count; ++index)
        {
          if (this.columns[index].FieldName.Contains(property.Name + (object) '.'))
            return true;
        }
      }
      return false;
    }

    private void PurgeDataOperation()
    {
      if (this.OwnerSite != null || this.Site != null)
        return;
      int index1 = 0;
      this.sortCollection.BeginUpdate();
      while (index1 < this.sortCollection.Count)
      {
        if (!this.Columns.Contains(this.sortCollection[index1].PropertyName))
          this.sortCollection.RemoveAt(index1);
        else
          ++index1;
      }
      this.sortCollection.EndUpdate(false);
      int index2 = 0;
      this.groupingCollection.BeginUpdate();
      while (index2 < this.groupingCollection.Count)
      {
        int index3 = 0;
        while (index3 < this.groupingCollection[index2].GroupNames.Count)
        {
          if (!this.Columns.Contains(this.groupingCollection[index2].GroupNames[index3].PropertyName))
            this.groupingCollection[index2].GroupNames.RemoveAt(index3);
          else
            ++index3;
        }
        if (this.groupingCollection[index2].GroupNames.Count == 0)
          this.groupingCollection.RemoveAt(index2);
        else
          ++index2;
      }
      this.groupingCollection.EndUpdate(false);
      int index4 = 0;
      this.filteringCollection.BeginUpdate();
      while (index4 < this.filteringCollection.Count)
      {
        if (!this.Columns.Contains(this.filteringCollection[index4].PropertyName))
          this.filteringCollection.RemoveAt(index4);
        else
          ++index4;
      }
      this.filteringCollection.EndUpdate(false);
    }

    private GridViewDataColumn AutoGenerateGridColumnFromTypeConverter(
      PropertyDescriptor descriptor)
    {
      if ((object) descriptor.PropertyType == (object) typeof (Image) || (object) descriptor.PropertyType == (object) typeof (Color) || !this.HasCustomTypeConverter(descriptor))
        return (GridViewDataColumn) null;
      System.Type[] typeArray = new System.Type[4]
      {
        typeof (Telerik.WinControls.Enumerations.ToggleState),
        typeof (Decimal),
        typeof (DateTime),
        typeof (string)
      };
      foreach (System.Type type in typeArray)
      {
        if (descriptor.Converter.CanConvertTo(type))
        {
          GridViewDataColumn gridColumn = GridViewHelper.AutoGenerateGridColumn(type, (ISite) null);
          gridColumn.DataTypeConverter = descriptor.Converter;
          return gridColumn;
        }
      }
      return (GridViewDataColumn) null;
    }

    private bool HasCustomTypeConverter(PropertyDescriptor descriptor)
    {
      foreach (Attribute attribute in descriptor.Attributes)
      {
        if (attribute is TypeConverterAttribute)
          return true;
      }
      return false;
    }

    private void RebuildColumnAccessors()
    {
      this.state[2147483648L] = false;
      for (int index = 0; index < this.columns.Count; ++index)
        this.UpdateAccessor(this.columns[index]);
    }

    private void UpdateAccessor(GridViewDataColumn column)
    {
      if (this.MasterTemplate != null && this.MasterTemplate.VirtualMode)
        column.Accessor = (Accessor) new VirtualAccessor((GridViewColumn) column);
      else if (!string.IsNullOrEmpty(column.Expression))
      {
        column.Accessor = (Accessor) new ExpressionAccessor((GridViewColumn) column);
        this.state[2147483648L] = true;
      }
      else if (this.IsDataBoundColumn((GridViewColumn) column))
      {
        column.Accessor = (Accessor) new BoundAccessor((GridViewColumn) column);
        this.SetDataBoundColumnDataType(column);
      }
      else if (this.HierarchyDataProvider is GridViewEventDataProvider)
        column.Accessor = (Accessor) new VirtualHierarchyAccessor((GridViewColumn) column);
      else if (this.relation != null && this.relation.IsObjectRelational)
      {
        column.Accessor = (Accessor) new ObjectRelationalAccessor((GridViewColumn) column);
      }
      else
      {
        if ((object) column.Accessor.GetType() == (object) typeof (Accessor))
          return;
        column.Accessor = new Accessor((GridViewColumn) column);
      }
    }

    private void SetDataBoundColumnDataType(GridViewDataColumn column)
    {
      System.Type dataType = column.DataType;
      PropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(column);
      if (propertyDescriptor != null)
        dataType = propertyDescriptor.PropertyType;
      if ((!column.IsAutoGenerated || (object) column.DataType == (object) dataType) && (column.IsAutoGenerated || column.GetValueSource(GridViewDataColumn.DataTypeProperty) == ValueSource.Local))
        return;
      this.SuspendEvent(KnownEvents.PropertyChanged);
      this.SetColumnDataType(column, dataType);
      this.ResumeEvent(KnownEvents.PropertyChanged);
    }

    private void SetColumnDataType(GridViewDataColumn column, System.Type dataType)
    {
      if ((object) dataType == (object) column.DataType)
        return;
      column.SuspendPropertyNotifications();
      column.DataType = dataType;
      int num = (int) column.SetDefaultValueOverride(GridViewDataColumn.DataTypeConverterProperty, (object) null);
      column.ResumePropertyNotifications();
    }

    internal bool IsDataBoundColumn(GridViewColumn column)
    {
      return this.ListSource.IsDataBound && !string.IsNullOrEmpty(column.FieldName) && column is GridViewDataColumn && (this.ListSource.BoundProperties.Find(column.FieldName, true) != null || GridViewHelper.ContainsInnerDescriptor(this.ListSource.BoundProperties, column.FieldName));
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public BindingContext BindingContext
    {
      get
      {
        return this.bindingContext;
      }
      internal set
      {
        if (this.bindingContext == value)
          return;
        Stack<GridViewTemplate> gridViewTemplateStack = new Stack<GridViewTemplate>();
        gridViewTemplateStack.Push(this);
        while (gridViewTemplateStack.Count > 0)
        {
          GridViewTemplate gridViewTemplate = gridViewTemplateStack.Pop();
          gridViewTemplate.bindingContext = value;
          gridViewTemplate.OnBindingContextChanged(EventArgs.Empty);
          for (int index = 0; index < gridViewTemplate.Templates.Count; ++index)
            gridViewTemplateStack.Push(gridViewTemplate.Templates[index]);
        }
      }
    }

    public event EventHandler BindingContextChanged;

    protected virtual void OnBindingContextChanged(EventArgs e)
    {
      EventHandler bindingContextChanged = this.BindingContextChanged;
      if (bindingContextChanged != null)
        bindingContextChanged((object) this, e);
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.Columns)
        column.Initialize();
    }

    IHierarchicalRow IHierarchicalRow.Parent
    {
      get
      {
        return (IHierarchicalRow) this.Parent;
      }
    }

    bool IHierarchicalRow.HasChildViews
    {
      get
      {
        return false;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewChildRowCollection ChildRows
    {
      get
      {
        return this.gridViewInfo.ChildRows;
      }
    }

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected virtual bool OnPropertyChanging(string propertyName)
    {
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName);
      this.OnPropertyChanging(e);
      return !e.Cancel;
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgsEx e)
    {
      if (this.PropertyChanging != null)
        this.PropertyChanging((object) this, e);
      if (e.Cancel)
        return;
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.PropertyChanging, GridEventType.UI, GridEventDispatchMode.Send);
      this.DispatchEvent(new GridViewEvent((object) this, (object) null, new object[1]
      {
        (object) e
      }, eventInfo));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected internal virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.PropertyChanged, GridEventType.UI, GridEventDispatchMode.Send);
      this.DispatchEvent(new GridViewEvent((object) this, (object) null, new object[1]
      {
        (object) e
      }, eventInfo));
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    protected virtual bool SetProperty<T>(string propertyName, ref T propertyField, T value)
    {
      if (!((object) propertyField is System.ValueType) && object.Equals((object) propertyField, (object) value))
        return false;
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName, (object) propertyField, (object) value);
      this.OnPropertyChanging(e);
      if (e.Cancel || !((object) propertyField is System.ValueType) && object.Equals((object) propertyField, e.NewValue))
        return false;
      propertyField = (T) e.NewValue;
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
      return true;
    }

    [Browsable(false)]
    public event EventHandler Initialized;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInitialized
    {
      get
      {
        return this.state[4L];
      }
    }

    public virtual void BeginInit()
    {
      this.BeginUpdate();
      this.state[4L] = false;
    }

    public virtual void EndInit()
    {
      this.state[4L] = true;
      this.EndUpdate();
      if (this.cachedViewDefinition != null)
      {
        this.ViewDefinition = this.cachedViewDefinition;
        this.cachedViewDefinition = (IGridViewDefinition) null;
      }
      if (this.Initialized == null)
        return;
      this.Initialized((object) this, EventArgs.Empty);
    }

    protected virtual void WireEvents()
    {
      this.ListSource.CollectionView.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CollectionView_CollectionChanged);
      this.ListSource.CollectionView.CurrentChanged += new EventHandler(this.CollectionView_CurrentChanged);
      this.Columns.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Columns_CollectionChanged);
    }

    protected internal virtual void UnwireEvents()
    {
      this.ListSource.CollectionView.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.CollectionView_CollectionChanged);
      this.ListSource.CollectionView.CurrentChanged -= new EventHandler(this.CollectionView_CurrentChanged);
      this.Columns.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.Columns_CollectionChanged);
      this.rows.UnwireEvents();
    }

    private void CollectionView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Add)
        return;
      if (e.Action == NotifyCollectionChangedAction.Reset && e.ResetReason == CollectionResetReason.Refresh)
      {
        GridViewSelfReferenceDataProvider hierarchyDataProvider = this.HierarchyDataProvider as GridViewSelfReferenceDataProvider;
        if (hierarchyDataProvider != null && this.relation != null)
          hierarchyDataProvider.Refresh();
      }
      this.OnViewChanged(this.GetViewChangedArgs(e));
    }

    private void CollectionView_CurrentChanged(object sender, EventArgs e)
    {
      CurrentChangedEventArgs changedEventArgs = e as CurrentChangedEventArgs;
      if (changedEventArgs != null && changedEventArgs.Reason == CurrentChangeReason.Add)
        return;
      this.UpdateCurrentRow(this.DataView.CurrentItem, this.currentColumn, (object) this.DataView);
    }

    protected virtual void ResetCurrentRow()
    {
      MasterGridViewTemplate masterTemplate = this.MasterTemplate;
      if (masterTemplate == null)
        return;
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.TemplateDataSourceInitializing, GridEventType.Data, GridEventDispatchMode.Send);
      masterTemplate.SynchronizationService.DispatchEvent(new GridViewEvent((object) this, (object) this, (object[]) null, eventInfo));
    }

    protected internal virtual void ResetCurrentColumn()
    {
      if (this.currentColumn != null)
      {
        this.currentColumn.SuspendPropertyNotifications();
        this.currentColumn.IsCurrent = false;
        this.currentColumn.ResumePropertyNotifications();
      }
      this.currentColumn = (GridViewColumn) null;
    }

    private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.RebuildColumnAccessors();
      if (e.Action == NotifyCollectionChangedAction.Remove && e.NewItems != null && e.NewItems.Count > 0)
      {
        this.BeginUpdate();
        GridViewColumn newItem = e.NewItems[0] as GridViewColumn;
        if (newItem != null)
        {
          this.sortCollection.Remove(newItem.Name);
          this.groupingCollection.Remove(newItem.Name);
          this.filteringCollection.Remove(newItem.Name);
          this.pinnedColumns.Remove(newItem);
          this.ResetSystemRowsCache(newItem);
          if (this.currentColumn == newItem)
          {
            if (this.columns.Count == 0)
              this.currentColumn = (GridViewColumn) null;
            if (e.NewStartingIndex < this.columns.Count)
              this.currentColumn = (GridViewColumn) this.columns[e.NewStartingIndex];
            if (this.columns.Count > 0 && e.NewStartingIndex == this.columns.Count)
              this.currentColumn = (GridViewColumn) this.columns[this.columns.Count - 1];
          }
        }
        if (this.currentColumn != null && this.currentColumn.OwnerTemplate != this)
          this.currentColumn = (GridViewColumn) null;
        this.EndUpdate(false);
      }
      if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        this.pinnedColumns.Clear();
        this.ResetSystemRowsCache();
        if (this.columns.Count == 0)
        {
          MasterGridViewTemplate masterTemplate = this.MasterTemplate;
          if (masterTemplate != null && masterTemplate.Owner != null && masterTemplate.Owner.IsInEditMode)
            masterTemplate.Owner.CloseEditor();
          this.ResetCurrentColumn();
        }
      }
      if (this.currentColumn != null)
        this.currentColumn.IsCurrent = true;
      if (this.columns.Count == 0)
      {
        this.PurgeDataOperation();
      }
      else
      {
        this.DataView.BeginUpdate();
        this.DataView.Refresh();
        this.DataView.EndUpdate(false);
      }
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.CollectionChanged, GridEventType.Both, GridEventDispatchMode.Send);
      this.DispatchEvent(new GridViewEvent((object) this, sender, new object[1]
      {
        (object) e
      }, eventInfo), false);
      this.EventDispatcher.RaiseEvent<NotifyCollectionChangedEventArgs>(EventDispatcher.ViewColumnsChanged, (object) this, e);
    }

    protected void OnViewChanged(DataViewChangedEventArgs e)
    {
      this.OnViewChanged((object) this, e);
    }

    protected internal virtual void OnViewChanged(object sender, DataViewChangedEventArgs e)
    {
      if (this.update != 0 || this.state[2L])
        return;
      this.state[2L] = true;
      this.DispatchDataViewChangedEvent(sender, e);
      DataViewChangedEventHandler viewChanged = this.ViewChanged;
      if (viewChanged != null)
        viewChanged(sender, e);
      this.state[2L] = false;
      if (e.Action == ViewChangedAction.ItemChanged || e.Action == ViewChangedAction.DataChanged || (e.Action == ViewChangedAction.Reset || e.Action == ViewChangedAction.PagingChanged) || (e.Action == ViewChangedAction.Add || e.Action == ViewChangedAction.Remove || (e.Action == ViewChangedAction.FilteringChanged || e.Action == ViewChangedAction.SortingChanged)))
        this.RefreshAggregates(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, e.NewItems));
      if (this.gridViewInfo.PinnedRows.Count <= 0)
        return;
      if (e.Action == ViewChangedAction.SortingChanged)
      {
        this.gridViewInfo.PinnedRows.Sort();
      }
      else
      {
        if (e.Action != ViewChangedAction.Reset)
          return;
        this.gridViewInfo.PinnedRows.Clear();
      }
    }

    private void DispatchDataViewChangedEvent(object sender, DataViewChangedEventArgs args)
    {
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.ViewChanged, GridEventType.Both, GridEventDispatchMode.Send);
      this.DispatchEvent(new GridViewEvent((object) this, sender, new object[1]
      {
        (object) args
      }, eventInfo), false);
    }

    private void summaryRows_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      GridViewSummaryRowCollection summaryRows = this.MasterViewInfo.SummaryRows;
      ViewChangedAction action = ViewChangedAction.Reset;
      GridViewSummaryRowInfo viewSummaryRowInfo = (GridViewSummaryRowInfo) null;
      ++this.summaryRowsVersion;
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        GridViewSummaryRowItem newItem = (GridViewSummaryRowItem) e.NewItems[0];
        newItem.Template = this;
        viewSummaryRowInfo = summaryRows.Add(this.MasterViewInfo, newItem, sender == this.summaryRowsTop);
        action = ViewChangedAction.Add;
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        GridViewSummaryRowItem newItem = (GridViewSummaryRowItem) e.NewItems[0];
        newItem.Template = (GridViewTemplate) null;
        viewSummaryRowInfo = summaryRows.Remove(newItem);
        action = ViewChangedAction.Remove;
        Queue<DataGroup> dataGroupQueue = new Queue<DataGroup>();
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
          dataGroupQueue.Enqueue(group);
        while (dataGroupQueue.Count > 0)
        {
          DataGroup dataGroup = dataGroupQueue.Dequeue();
          foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) dataGroup.Groups)
            dataGroupQueue.Enqueue(group);
          if (dataGroup.GroupRow != null)
          {
            dataGroup.GroupRow.BottomSummaryRows.Remove(newItem);
            dataGroup.GroupRow.TopSummaryRows.Remove(newItem);
          }
        }
      }
      else if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        foreach (GridViewSummaryRowItem viewSummaryRowItem in (Collection<GridViewSummaryRowItem>) sender)
          viewSummaryRowItem.Template = (GridViewTemplate) null;
        this.ClearAllSummariesFromGroups();
        summaryRows.Clear();
        summaryRows.AddRows(this.MasterViewInfo, this.summaryRowsTop, true);
        summaryRows.AddRows(this.MasterViewInfo, this.summaryRowsBottom, false);
        action = ViewChangedAction.Reset;
      }
      if (viewSummaryRowInfo != null)
        this.OnViewChanged(new DataViewChangedEventArgs(action, (object) viewSummaryRowInfo));
      else
        this.OnViewChanged(new DataViewChangedEventArgs(action));
    }

    protected virtual void ClearAllSummariesFromGroups()
    {
      Queue<DataGroup> dataGroupQueue = new Queue<DataGroup>();
      foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
        dataGroupQueue.Enqueue(group);
      while (dataGroupQueue.Count > 0)
      {
        DataGroup dataGroup = dataGroupQueue.Dequeue();
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) dataGroup.Groups)
          dataGroupQueue.Enqueue(group);
        if (dataGroup.GroupRow != null)
        {
          dataGroup.GroupRow.BottomSummaryRows.Clear();
          dataGroup.GroupRow.TopSummaryRows.Clear();
        }
      }
    }

    private void summaryRowGroupHeaders_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        foreach (GridViewSummaryRowItem newItem in (IEnumerable) e.NewItems)
          newItem.Template = this;
      }
      if (e.Action != NotifyCollectionChangedAction.Remove)
        return;
      foreach (GridViewSummaryRowItem newItem in (IEnumerable) e.NewItems)
        newItem.Template = (GridViewTemplate) null;
    }

    [Browsable(false)]
    public bool IsUpdating
    {
      get
      {
        return this.update > 0;
      }
    }

    [DefaultValue(true)]
    public bool SelectLastAddedRow
    {
      get
      {
        return this.DataView.ChangeCurrentOnAdd;
      }
      set
      {
        if (this.DataView.ChangeCurrentOnAdd == value)
          return;
        this.DataView.ChangeCurrentOnAdd = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal BestFitQueue BestFitQueue
    {
      get
      {
        return this.bestFitQueue;
      }
    }

    internal bool IsDesignMode
    {
      get
      {
        return this.DesignMode;
      }
    }

    [Browsable(true)]
    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the text displayed in the new row. If this values is null or empty the text from the localization provider will be used.")]
    public string NewRowText
    {
      get
      {
        return this.newRowText;
      }
      set
      {
        if (!(this.newRowText != value))
          return;
        this.newRowText = value;
        this.OnNotifyPropertyChanged(nameof (NewRowText));
      }
    }

    [TypeConverter(typeof (StringConverter))]
    [Bindable(true)]
    [DefaultValue(null)]
    [Localizable(false)]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value)
          return;
        this.tag = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Tag)));
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the filter operator text should be shown in the filter cell.")]
    [DefaultValue(true)]
    public bool ShowFilterCellOperatorText
    {
      get
      {
        return this.state[68719476736L];
      }
      set
      {
        if (this.state[68719476736L] == value)
          return;
        this.state[68719476736L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowFilterCellOperatorText)));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the display state of grid horizontal scrollbars.")]
    [Category("Appearance")]
    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.horizontalScrollState;
      }
      set
      {
        this.SetProperty<ScrollState>(nameof (HorizontalScrollState), ref this.horizontalScrollState, value);
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    [Description("Gets or sets the display state of grid vertical scrollbars.")]
    [Category("Behavior")]
    [Browsable(true)]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.verticalScrollState;
      }
      set
      {
        this.SetProperty<ScrollState>(nameof (VerticalScrollState), ref this.verticalScrollState, value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether user can drag a column header to grouping panel.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool AllowDragToGroup
    {
      get
      {
        return this.state[1048576L];
      }
      set
      {
        if (this.state[1048576L] == value)
          return;
        this.state[1048576L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowDragToGroup)));
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether user can reorder columns")]
    public bool AllowColumnReorder
    {
      get
      {
        return this.state[524288L];
      }
      set
      {
        if (this.state[524288L] == value)
          return;
        this.state[524288L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowColumnReorder)));
      }
    }

    [Description("Gets or sets a value indicating whether user can resize a row.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool AllowRowResize
    {
      get
      {
        return this.state[262144L];
      }
      set
      {
        if (this.state[262144L] == value)
          return;
        this.state[262144L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowRowResize)));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the underlying source in Object-Relational binding should be automatically updated.")]
    [Category("Data")]
    [Browsable(true)]
    public bool AutoUpdateObjectRelationalSource
    {
      get
      {
        return this.state[549755813888L];
      }
      set
      {
        if (this.state[549755813888L] == value)
          return;
        this.state[549755813888L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AutoUpdateObjectRelationalSource)));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the caption text.")]
    [Category("Appearance")]
    [DefaultValue("")]
    [MergableProperty(false)]
    [Localizable(true)]
    public string Caption
    {
      get
      {
        return this.caption;
      }
      set
      {
        this.SetProperty<string>(nameof (Caption), ref this.caption, value);
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether alternating row color is enabled.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    public virtual bool EnableAlternatingRowColor
    {
      get
      {
        return this.state[8L];
      }
      set
      {
        this.SetEnableAlternatingRowColor(value);
      }
    }

    protected virtual void SetEnableAlternatingRowColor(bool value)
    {
      if (this.state[8L] == value)
        return;
      this.state[8L] = value;
      foreach (GridViewTemplate template in (Collection<GridViewTemplate>) this.Templates)
        template.EnableAlternatingRowColor = value;
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("EnableAlternatingRowColor"));
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    [Browsable(false)]
    public virtual GridViewHierarchyDataProvider HierarchyDataProvider
    {
      get
      {
        return this.hierarchyDataProvider;
      }
      set
      {
        if (this.hierarchyDataProvider == value)
          return;
        if (this.hierarchyDataProvider != null)
          this.hierarchyDataProvider.Dispose();
        this.hierarchyDataProvider = value;
        this.relation = (GridViewRelation) null;
        this.EnsureRowType(this.parent);
        this.RebuildColumnAccessors();
        this.EnsureHierarchyProvider();
        this.OnNotifyPropertyChanged(nameof (HierarchyDataProvider));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the collection containing the summary items placed in the group header.")]
    [Category("Data")]
    public GridViewSummaryRowItemCollection SummaryRowGroupHeaders
    {
      get
      {
        return this.summaryRowGroupHeaders;
      }
    }

    [Description("Gets the collection containing summary rows placed at the bottom of each DataGroup.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public GridViewSummaryRowItemCollection SummaryRowsBottom
    {
      get
      {
        return this.summaryRowsBottom;
      }
    }

    [Description("Gets the collection containing summary rows placed on top of each DataGroup.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Category("Data")]
    public GridViewSummaryRowItemCollection SummaryRowsTop
    {
      get
      {
        return this.summaryRowsTop;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool CaseSensitive
    {
      get
      {
        return this.ListSource.CollectionView.CaseSensitive;
      }
      set
      {
        if (this.ListSource.CollectionView.CaseSensitive == value)
          return;
        this.columns.AllowCaseSensitiveNames = value;
        this.listSource.UseCaseSensitiveFieldNames = value;
        this.ListSource.CollectionView.CaseSensitive = value;
        this.sortCollection.UseCaseSensitiveFieldNames = value;
        this.filteringCollection.UseCaseSensitiveFieldNames = value;
        this.groupingCollection.UseCaseSensitiveFieldNames = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (CaseSensitive)));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ReadOnlyCollection<GridViewColumn> PinnedColumns
    {
      get
      {
        return this.pinnedColumns.AsReadOnly();
      }
    }

    [DefaultValue("")]
    [Category("Data")]
    [Browsable(true)]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Description("Gets or sets the name of the list or table in the data source for which the GridViewTemplate is displaying data. ")]
    public string DataMember
    {
      get
      {
        return this.ListSource.DataMember;
      }
      set
      {
        if (this.ProcessingData)
        {
          this.MasterTemplate.SetError(new GridViewCellCancelEventArgs((GridCellElement) null, (IInputEditor) null), "The DataMember property cannot be set during internal data-processing operations.");
        }
        else
        {
          if (!(this.ListSource.DataMember != value) || !this.OnPropertyChanging(nameof (DataMember)))
            return;
          this.ListSource.DataMember = value;
          this.OnNotifyPropertyChanged(nameof (DataMember));
        }
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof (IListSource))]
    [DefaultValue(null)]
    [Description("Gets or sets the data source that the GridViewTemplate is displaying data for.")]
    [Category("Data")]
    public object DataSource
    {
      get
      {
        return this.ListSource.DataSource;
      }
      set
      {
        if (this.ProcessingData)
        {
          this.MasterTemplate.SetError(new GridViewCellCancelEventArgs((GridCellElement) null, (IInputEditor) null), "The DataSource property cannot be set during internal data-processing operations.");
        }
        else
        {
          if (this.MasterTemplate != null && this.MasterTemplate.AddNewBoundRowBeforeEdit)
            this.gridViewInfo.TableAddNewRow.ClearBoundRow();
          if (this.ListSource.DataSource == value)
            return;
          this.dataSourceChanging = true;
          if (this.OnPropertyChanging(nameof (DataSource)))
          {
            this.gridViewInfo.PinnedRows.Clear();
            this.ListSource.DataSource = value;
            this.RefreshHierarchyProvider();
            this.OnNotifyPropertyChanged(nameof (DataSource));
          }
          this.dataSourceChanging = false;
        }
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public GridViewTemplateCollection Templates
    {
      get
      {
        return this.childGridViewTemplates;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewInfo MasterViewInfo
    {
      get
      {
        return this.gridViewInfo;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataGroupCollection Groups
    {
      get
      {
        if (this.IsSelfReference && this.gridViewInfo.LoadedData != null)
        {
          if (this.gridViewInfo.LoadedData.Groups is DataGroupCollection)
            return (DataGroupCollection) this.gridViewInfo.LoadedData.Groups;
          return DataGroupCollection.Empty;
        }
        if (this.ListSource.CollectionView.Groups is DataGroupCollection)
          return (DataGroupCollection) this.ListSource.CollectionView.Groups;
        return DataGroupCollection.Empty;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [NotifyParentProperty(true)]
    [MergableProperty(false)]
    [Editor("Telerik.WinControls.UI.Design.GridViewColumnCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual GridViewColumnCollection Columns
    {
      get
      {
        return this.columns;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewColumn CurrentColumn
    {
      get
      {
        return this.currentColumn;
      }
      set
      {
        if (this.currentColumn == value)
          return;
        GridViewSynchronizationService.RaiseCurrentChanged(this, this.MasterTemplate.CurrentRow, value, true);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (CurrentColumn)));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewRowCollection Rows
    {
      get
      {
        return this.rows;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewTemplate Parent
    {
      get
      {
        return this.parent;
      }
      internal set
      {
        if (this.parent == value)
          return;
        this.parent = value;
        this.BindingContext = this.Parent.BindingContext;
        if (this.Parent.EnableAlternatingRowColor)
          this.EnableAlternatingRowColor = true;
        if (!(this.HierarchyDataProvider is GridViewEventDataProvider))
          return;
        this.EnsureRowType(this.parent);
        this.RebuildColumnAccessors();
        this.EnsureHierarchyProvider();
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetParent(GridViewTemplate parent)
    {
      this.Parent = parent;
    }

    [Browsable(false)]
    public int HierarchyLevel
    {
      get
      {
        int num = 0;
        for (IHierarchicalRow parent = (IHierarchicalRow) this.parent; parent != null; parent = parent.Parent)
          ++num;
        return num;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ISite OwnerSite
    {
      get
      {
        return this.ownerSite;
      }
      set
      {
        this.ownerSite = value;
      }
    }

    protected internal virtual void EnsureHierarchyProvider()
    {
      if (this.MasterTemplate == null)
        return;
      GridViewRelation relation = this.MasterTemplate.Relations.Find(this.parent, this);
      if (relation != this.relation)
      {
        this.relation = relation;
        if (this.hierarchyDataProvider != null)
          this.hierarchyDataProvider.Dispose();
        this.hierarchyDataProvider = GridViewHierarchyDataProvider.Create(relation);
        this.EnsureRowType(this.parent);
        this.RebuildColumnAccessors();
        if (!this.IsSelfReference)
          return;
        this.EnableHierarchyFiltering = true;
      }
      else
      {
        if (this.relation != null && !this.relation.IsValid)
        {
          this.MasterTemplate.Relations.Remove(this.relation);
          this.relation = (GridViewRelation) null;
          if (this.hierarchyDataProvider != null)
            this.hierarchyDataProvider.Dispose();
          this.hierarchyDataProvider = (GridViewHierarchyDataProvider) null;
        }
        if (this.hierarchyDataProvider == null)
          return;
        this.hierarchyDataProvider.Refresh();
      }
    }

    private void RefreshHierarchyProvider()
    {
      this.RefreshHierarchyProvider(this);
    }

    private void RefreshHierarchyProvider(GridViewTemplate template)
    {
      this.RefreshFilterDescriptors(template);
      if (template.HierarchyDataProvider != null)
        template.HierarchyDataProvider.Refresh();
      foreach (GridViewTemplate template1 in (Collection<GridViewTemplate>) template.Templates)
        this.RefreshHierarchyProvider(template1);
    }

    private void RefreshFilterDescriptors(GridViewTemplate template)
    {
      if (template.FilterDescriptors.Count == 0)
        return;
      this.EventDispatcher.SuspendEvent(EventDispatcher.FilterChangedEvent);
      FilterDescriptorCollection descriptorCollection = new FilterDescriptorCollection();
      foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) template.FilterDescriptors)
        descriptorCollection.Add(filterDescriptor);
      template.FilterDescriptors.Clear();
      foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) descriptorCollection)
        template.FilterDescriptors.Add(filterDescriptor);
      this.EventDispatcher.ResumeEvent(EventDispatcher.FilterChangedEvent);
    }

    protected virtual void EnsureRowType(GridViewTemplate gridViewTemplate)
    {
      if (gridViewTemplate == null)
        gridViewTemplate = this;
      if (gridViewTemplate.listSource.Count == 0)
        return;
      GridViewHierarchyRowInfo hierarchyRowInfo = gridViewTemplate.listSource[0] as GridViewHierarchyRowInfo;
      if (hierarchyRowInfo != null && hierarchyRowInfo.ChildRow != null)
        return;
      if (gridViewTemplate.DataSource != null || !(gridViewTemplate.listSource[0] is GridViewDataRowInfo))
      {
        gridViewTemplate.ListSource.Reset();
      }
      else
      {
        for (int index = 0; index < gridViewTemplate.ListSource.Count; ++index)
          gridViewTemplate.ListSource[index] = (GridViewRowInfo) new GridViewHierarchyRowInfo(gridViewTemplate.ListSource[index] as GridViewDataRowInfo);
        gridViewTemplate.Refresh();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual MasterGridViewTemplate MasterTemplate
    {
      get
      {
        if (this.parent != null)
          return this.parent.MasterTemplate;
        return (MasterGridViewTemplate) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual EventDispatcher EventDispatcher
    {
      get
      {
        MasterGridViewTemplate masterTemplate = this.MasterTemplate;
        if (masterTemplate != null && masterTemplate.EventDispatcher != null)
          return masterTemplate.EventDispatcher;
        return EventDispatcher.Empty;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public int ColumnCount
    {
      get
      {
        return this.columns.Count;
      }
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (ColumnCount));
        bool flag = this.columns.Count == 0 && value > 0;
        if (this.columns.Count == value)
          return;
        this.columns.BeginUpdate();
        while (this.columns.Count < value)
          this.columns.Add((GridViewDataColumn) new GridViewTextBoxColumn("Column" + (this.columns.Count + 1).ToString(), ""));
        while (this.columns.Count > value)
        {
          int index = this.columns.Count - 1;
          this.ResetSystemRowsCache((GridViewColumn) this.columns[index]);
          this.columns.RemoveAt(index);
        }
        this.columns.EndUpdate();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ColumnCount)));
        if (!flag)
          return;
        this.OnViewChanged(new DataViewChangedEventArgs(ViewChangedAction.Reset));
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0)]
    [Browsable(false)]
    public int RowCount
    {
      get
      {
        return this.rows.Count;
      }
      set
      {
        if (this.ListSource.IsDataBound)
          throw new InvalidOperationException("This operation is valid only for unbound or virtual mode.");
        if (this.parent != null)
          throw new InvalidOperationException("This operation is not valid gor child GridViewTemplate.");
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (RowCount));
        if (this.ListSource.Count == value)
          return;
        this.BeginUpdate();
        if (this.ListSource.Count < value)
        {
          ((IDataItemSource) this).NewItem();
          int num = value - this.ListSource.Count;
          for (int index = 0; index < num; ++index)
            this.ListSource.AddNew().Attach();
        }
        else
        {
          while (this.ListSource.Count > value)
            this.ListSource.RemoveAt(this.ListSource.Count - 1);
        }
        this.EndUpdate();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (RowCount)));
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether user can edit rows.")]
    [DefaultValue(true)]
    public bool AllowEditRow
    {
      get
      {
        return this.state[8192L];
      }
      set
      {
        if (this.state[8192L] == value)
          return;
        this.state[8192L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowEditRow)));
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the Column Chooser form is available to the user for this instance of GridViewTemplate")]
    [Browsable(true)]
    public bool AllowColumnChooser
    {
      get
      {
        return this.state[4096L];
      }
      set
      {
        if (this.state[4096L] == value)
          return;
        this.state[4096L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowColumnChooser)));
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the user is able to reorder rows in the grid")]
    public bool AllowRowReorder
    {
      get
      {
        return this.state[16L];
      }
      set
      {
        if (this.state[16L] == value)
          return;
        this.state[16L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowRowReorder)));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether context menu is displayed when user rightclicks on a column header.")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool AllowColumnHeaderContextMenu
    {
      get
      {
        return this.state[512L];
      }
      set
      {
        if (this.state[512L] == value)
          return;
        this.state[512L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowColumnHeaderContextMenu)));
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether context menu is displayed when user right clicks on a row header.")]
    [Browsable(true)]
    public bool AllowRowHeaderContextMenu
    {
      get
      {
        return this.state[1024L];
      }
      set
      {
        if (this.state[1024L] == value)
          return;
        this.state[1024L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowRowHeaderContextMenu)));
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether user can resize columns.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool AllowColumnResize
    {
      get
      {
        return this.state[256L];
      }
      set
      {
        if (this.state[256L] == value)
          return;
        this.state[256L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowColumnResize)));
      }
    }

    [Description("Gets or sets a value indicating whether context menu is displayed when user right clicks on a data cell.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [Category("Behavior")]
    public bool AllowCellContextMenu
    {
      get
      {
        return this.state[64L];
      }
      set
      {
        if (this.state[64L] == value)
          return;
        this.state[64L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowCellContextMenu)));
      }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool AllowAutoSizeColumns
    {
      get
      {
        return this.state[128L];
      }
      set
      {
        if (this.state[128L] == value)
          return;
        this.state[128L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowAutoSizeColumns)));
      }
    }

    [DefaultValue(true)]
    [Category("Data")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether user can delete rows.")]
    public bool AllowDeleteRow
    {
      get
      {
        return this.state[32L];
      }
      set
      {
        if (this.state[32L] == value)
          return;
        this.state[32L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowDeleteRow)));
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the user can add new rows through the grid.")]
    public bool AllowAddNewRow
    {
      get
      {
        return this.state[16384L];
      }
      set
      {
        if (this.state[16384L] == value)
          return;
        this.state[16384L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowAddNewRow)));
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the user can add new rows through the grid.")]
    [Category("Data")]
    public bool AllowSearchRow
    {
      get
      {
        return this.state[32768L];
      }
      set
      {
        if (this.state[32768L] == value)
          return;
        this.state[32768L] = value;
        if (this.IsInitialized)
          this.MasterViewInfo.TableSearchRow.IsVisible = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowSearchRow)));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the data can be sorted by the end-users.")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool EnableSorting
    {
      get
      {
        return this.ListSource.CollectionView.CanSort;
      }
      set
      {
        if (this.ListSource.CollectionView.CanSort == value)
          return;
        this.listSource.CollectionView.CanSort = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableSorting)));
      }
    }

    [Description("Gets or sets a value indicating whether the data can be sorted programatically.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Browsable(true)]
    public bool EnableCustomSorting
    {
      get
      {
        return this.state[17179869184L];
      }
      set
      {
        if (this.state[17179869184L] == value)
          return;
        this.state[17179869184L] = value;
        this.DataView.Comparer = !value ? (IComparer<GridViewRowInfo>) new GridViewRowInfoComparer(this.SortDescriptors) : (IComparer<GridViewRowInfo>) new GridViewRowInfoEventComparer(this.SortDescriptors);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableCustomSorting)));
      }
    }

    [Description("Gets or sets a value indicating whether the data in the current GridViewTemplate can be grouped by users.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool EnableGrouping
    {
      get
      {
        return this.ListSource.CollectionView.CanGroup;
      }
      set
      {
        if (this.ListSource.CollectionView.CanGroup == value)
          return;
        this.listSource.CollectionView.CanGroup = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableGrouping)));
        if (value)
          return;
        this.listSource.CollectionView.GroupDescriptors.Clear();
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the data can be grouped programatically.")]
    public bool EnableCustomGrouping
    {
      get
      {
        return this.state[34359738368L];
      }
      set
      {
        if (this.state[34359738368L] == value)
          return;
        this.state[34359738368L] = value;
        this.DataView.GroupPredicate = !value ? this.DataView.DefaultGroupPredicate : new Telerik.WinControls.Data.GroupPredicate<GridViewRowInfo>(this.PerformGrouping);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableCustomGrouping)));
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the data in the current DataView can be filtered.")]
    public bool EnableFiltering
    {
      get
      {
        return this.ListSource.CollectionView.CanFilter;
      }
      set
      {
        if (this.listSource.CollectionView.CanFilter == value)
          return;
        this.listSource.CollectionView.CanFilter = value;
        if (value && this.state[4294967296L] && this.DataView.Filter != new Predicate<GridViewRowInfo>(this.PerformFiltering))
          this.DataView.Filter = new Predicate<GridViewRowInfo>(this.PerformFiltering);
        if (this.IsInitialized && this.gridViewInfo != null && this.gridViewInfo.TableHeaderRow != null)
          this.gridViewInfo.TableHeaderRow.InvalidateRow();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableFiltering)));
      }
    }

    [Description("Gets or sets a value indicating whether the data can be filtered programatically.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool EnableCustomFiltering
    {
      get
      {
        return this.state[4294967296L];
      }
      set
      {
        if (this.state[4294967296L] == value)
          return;
        this.state[4294967296L] = value;
        if (!this.EnableFiltering)
          return;
        this.DataView.Filter = !value ? (!this.EnableHierarchyFiltering ? this.DataView.DefaultFilter : new Predicate<GridViewRowInfo>(this.PerformHierarchyFilter)) : new Predicate<GridViewRowInfo>(this.PerformFiltering);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableCustomFiltering)));
      }
    }

    [Category("Behavior")]
    [Browsable(false)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the data can be filtered using parent/child relationship.")]
    public bool EnableHierarchyFiltering
    {
      get
      {
        return this.state[8589934592L];
      }
      set
      {
        if (this.state[8589934592L] == value)
          return;
        this.state[8589934592L] = value;
        this.DataView.Filter = !this.EnableCustomFiltering ? (!value ? this.DataView.DefaultFilter : new Predicate<GridViewRowInfo>(this.PerformHierarchyFilter)) : new Predicate<GridViewRowInfo>(this.PerformFiltering);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableHierarchyFiltering)));
      }
    }

    [DefaultValue(GridViewAutoSizeColumnsMode.None)]
    [Description("Gets or sets a value indicating how column widths are determined.")]
    [Browsable(true)]
    [Category("Layout")]
    public GridViewAutoSizeColumnsMode AutoSizeColumnsMode
    {
      get
      {
        return this.autoSizeColumnsMode;
      }
      set
      {
        this.SetProperty<GridViewAutoSizeColumnsMode>(nameof (AutoSizeColumnsMode), ref this.autoSizeColumnsMode, value);
      }
    }

    [Category("Layout")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating how bottom pinned rows are layed out.")]
    [DefaultValue(GridViewBottomPinnedRowsMode.Float)]
    public GridViewBottomPinnedRowsMode BottomPinnedRowsMode
    {
      get
      {
        return this.bottomPinnedRowsMode;
      }
      set
      {
        this.SetProperty<GridViewBottomPinnedRowsMode>(nameof (BottomPinnedRowsMode), ref this.bottomPinnedRowsMode, value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the columns by which the data is grouped are visible.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool ShowGroupedColumns
    {
      get
      {
        return this.state[4194304L];
      }
      set
      {
        if (this.state[4194304L] == value)
          return;
        this.state[4194304L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowGroupedColumns)));
      }
    }

    [DefaultValue(true)]
    [Browsable(true)]
    public bool AllowMultiColumnSorting
    {
      get
      {
        return this.state[131072L];
      }
      set
      {
        if (this.state[131072L] == value)
          return;
        this.state[131072L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AllowMultiColumnSorting)));
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether the data in this template can be modified.")]
    public bool ReadOnly
    {
      get
      {
        return this.state[1L];
      }
      set
      {
        if (this.state[1L] == value)
          return;
        this.state[1L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ReadOnly)));
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Browsable(true)]
    public bool AutoGenerateColumns
    {
      get
      {
        return this.state[16777216L];
      }
      set
      {
        if (this.state[16777216L] == value)
          return;
        this.state[16777216L] = value;
        if (value && this.ListSource.IsDataBound)
          ((IDataItemSource) this).Initialize();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AutoGenerateColumns)));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return (FilterDescriptorCollection) this.filteringCollection;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Category("Data")]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return (SortDescriptorCollection) this.sortCollection;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Category("Data")]
    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.DataView.GroupDescriptors;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("")]
    [DefaultValue(null)]
    public IGridViewDefinition ViewDefinition
    {
      get
      {
        return this.viewDefinition;
      }
      set
      {
        if (!this.IsInitialized)
        {
          this.cachedViewDefinition = value;
        }
        else
        {
          if (this.viewDefinition == value || value == null)
            return;
          if (this.viewDefinition != null)
            (this.viewDefinition as IDisposable)?.Dispose();
          this.viewDefinition = value;
          if (this.viewDefinition is ColumnGroupsViewDefinition)
            ((ColumnGroupsViewDefinition) this.viewDefinition).ViewTemplate = this;
          this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ViewDefinition)));
        }
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Sets or gets a value indicating the initial state of group rows when data is grouped.")]
    public bool AutoExpandGroups
    {
      get
      {
        return this.state[8388608L];
      }
      set
      {
        if (this.state[8388608L] == value)
          return;
        this.state[8388608L] = value;
        if (value)
          this.ExpandAllGroups();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AutoExpandGroups)));
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(SystemRowPosition.Top)]
    [Description("Gets or sets a vlue indicating the location of the new row in the view template.")]
    public SystemRowPosition AddNewRowPosition
    {
      get
      {
        return this.addNewRowPosition;
      }
      set
      {
        if (this.addNewRowPosition == value)
          return;
        this.addNewRowPosition = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AddNewRowPosition)));
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(SystemRowPosition.Top)]
    [Description("Gets or sets a vlue indicating the location of the new row in the view template.")]
    public SystemRowPosition SearchRowPosition
    {
      get
      {
        return this.searchRowPosition;
      }
      set
      {
        if (this.searchRowPosition == value)
          return;
        this.searchRowPosition = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (SearchRowPosition)));
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the row header column is visible.")]
    public bool ShowRowHeaderColumn
    {
      get
      {
        return this.state[2097152L];
      }
      set
      {
        if (this.state[2097152L] == value)
          return;
        this.state[2097152L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowRowHeaderColumn)));
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the column headers are visible.")]
    public bool ShowColumnHeaders
    {
      get
      {
        return this.MasterViewInfo.TableHeaderRow.IsVisible;
      }
      set
      {
        if (this.MasterViewInfo.TableHeaderRow.IsVisible == value)
          return;
        this.MasterViewInfo.TableHeaderRow.IsVisible = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowColumnHeaders)));
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating wheter the filtering row should be visible.")]
    [DefaultValue(true)]
    public bool ShowFilteringRow
    {
      get
      {
        return this.MasterViewInfo.TableFilteringRow.IsVisible;
      }
      set
      {
        if (this.MasterViewInfo.TableFilteringRow.IsVisible == value)
          return;
        this.MasterViewInfo.TableFilteringRow.IsVisible = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowFilteringRow)));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the header cell buttons are visible.")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool ShowHeaderCellButtons
    {
      get
      {
        return this.showHeaderCellButtons;
      }
      set
      {
        this.showHeaderCellButtons = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowHeaderCellButtons)));
      }
    }

    [DefaultValue(TabPositions.Top)]
    [Description("Gets or sets the position to place tabs for child views related with this template.")]
    [Browsable(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public TabPositions ChildViewTabsPosition
    {
      get
      {
        return this.childViewTabsPosition;
      }
      set
      {
        this.SetProperty<TabPositions>(nameof (ChildViewTabsPosition), ref this.childViewTabsPosition, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsSelfReference
    {
      get
      {
        return this.hierarchyDataProvider is GridViewSelfReferenceDataProvider;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsVirtualRows
    {
      get
      {
        if (this.hierarchyDataProvider != null)
          return this.hierarchyDataProvider.IsVirtual;
        return false;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether total summary rows are visible in grouping.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool ShowTotals
    {
      get
      {
        return this.state[134217728L];
      }
      set
      {
        if (this.state[134217728L] == value)
          return;
        this.state[134217728L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowTotals)));
      }
    }

    [Description("Gets or sets a value indicating whether parent group summary rows are visible in grouping.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool ShowParentGroupSummaries
    {
      get
      {
        return this.state[268435456L];
      }
      set
      {
        if (this.state[268435456L] == value)
          return;
        this.state[268435456L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowParentGroupSummaries)));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to show child view captions.")]
    public bool ShowChildViewCaptions
    {
      get
      {
        return this.state[1073741824L];
      }
      set
      {
        if (this.state[1073741824L] == value)
          return;
        this.state[1073741824L] = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("ShowChildViewTabsAlways"));
      }
    }

    [Description("Gets a value indicating if the template contains a column with defined expression.")]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool ContainsColumnExpression
    {
      get
      {
        return this.state[2147483648L];
      }
    }

    public virtual IDisposable DeferRefresh()
    {
      this.BeginUpdate();
      return (IDisposable) new GridViewTemplate.DeferHelper(this);
    }

    private void EndDefer()
    {
      this.EndUpdate();
    }

    public void SetError(GridViewCellCancelEventArgs e, string message)
    {
      this.SetError(e, new Exception(message));
    }

    public void SetError(GridViewCellCancelEventArgs e, Exception exception)
    {
      GridViewDataErrorEventArgs args = new GridViewDataErrorEventArgs(exception, 0, 0, GridViewDataErrorContexts.Commit);
      if (e != null)
        args = new GridViewDataErrorEventArgs(exception, e.ColumnIndex, e.RowIndex, GridViewDataErrorContexts.Commit);
      this.EventDispatcher.RaiseEvent<GridViewDataErrorEventArgs>(EventDispatcher.DataError, (object) this, args);
      if (args.ThrowException)
        throw args.Exception;
      int num = args.Cancel ? 1 : 0;
    }

    public virtual void Refresh()
    {
      this.DataView.Refresh();
    }

    public virtual void Refresh(params GridViewColumn[] affectedColumns)
    {
      this.RebuildColumnAccessors();
      this.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.DataChanged, (IList) affectedColumns));
    }

    public virtual void BeginUpdate()
    {
      this.BeginCurrentRowUpdate(false);
      ++this.update;
      this.ListSource.BeginUpdate();
      this.EventDispatcher.SuspendNotifications();
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    public void EndUpdate(bool notify)
    {
      this.EndUpdate(notify, new DataViewChangedEventArgs(ViewChangedAction.Reset));
    }

    public void EndUpdate(DataViewChangedEventArgs e)
    {
      this.EndUpdate(true, e);
    }

    public virtual void EndUpdate(bool notify, DataViewChangedEventArgs e)
    {
      if (this.update == 0)
        return;
      this.EventDispatcher.ResumeNotifications();
      this.ListSource.EndUpdate(notify);
      --this.update;
      if (this.update > 0)
        return;
      if (this.rowExpandedWhileEventDispatcherWasSuspended)
      {
        this.rowExpandedWhileEventDispatcherWasSuspended = false;
        this.MarkLastRow();
      }
      if (notify)
        this.OnViewChanged(e);
      DeleteRowDataViewChangedEventArgs changedEventArgs = e as DeleteRowDataViewChangedEventArgs;
      if (changedEventArgs != null)
      {
        this.UpdateCurrentOnRowDeleted(changedEventArgs.ViewInfo, changedEventArgs.HierarchyRow, changedEventArgs.HierachyRowIndex);
        if (changedEventArgs.HierarchyRow != null && changedEventArgs.HierarchyRow.IsCurrent)
          changedEventArgs.HierarchyRow.IsExpanded = false;
      }
      else
        this.EndCurrentRowUpdate(false, notify);
      if (this.CurrentRowToSetOnEndUpdate == null)
        return;
      this.MasterTemplate.CurrentRow = this.CurrentRowToSetOnEndUpdate;
      this.CurrentRowToSetOnEndUpdate = (GridViewRowInfo) null;
    }

    public virtual void NotifyRowExpanded()
    {
      this.rowExpandedWhileEventDispatcherWasSuspended = true;
    }

    public void LoadFrom(IDataReader dataReader)
    {
      this.BeginUpdate();
      this.ListSource.DataSource = (object) null;
      this.ListSource.Clear();
      if (this.AutoGenerateColumns)
      {
        this.ClearAutoGeneratedColumns();
        this.AutoGenerateColumnsFromReader(dataReader);
        while (dataReader.Read())
        {
          GridViewRowInfo gridViewRowInfo = this.ListSource.AddNew();
          gridViewRowInfo.Attach();
          for (int index = 0; index < dataReader.FieldCount; ++index)
            gridViewRowInfo[(GridViewColumn) this.columns[index]] = dataReader[index];
        }
      }
      else
      {
        while (dataReader.Read())
        {
          GridViewRowInfo gridViewRowInfo = this.ListSource.AddNew();
          gridViewRowInfo.Attach();
          for (int i = 0; i < dataReader.FieldCount; ++i)
          {
            GridViewColumn column = (GridViewColumn) this.Columns[dataReader.GetName(i)];
            if (column != null)
              gridViewRowInfo[column] = dataReader[i];
          }
        }
      }
      this.EndUpdate();
      this.ListSource.Position = 0;
    }

    public void BestFitColumns()
    {
      this.bestFitQueue.EnqueueBestFitColumns();
      this.OnViewChanged(new DataViewChangedEventArgs(ViewChangedAction.BestFitColumn));
    }

    public void BestFitColumns(BestFitColumnMode mode)
    {
      this.bestFitQueue.EnqueueBestFitColumns(mode);
      this.OnViewChanged(new DataViewChangedEventArgs(ViewChangedAction.BestFitColumn));
    }

    public void ExpandAllGroups()
    {
      if (this.MasterTemplate == null)
        return;
      this.MasterTemplate.SynchronizationService.SuspendDispatch();
      if (this.MasterTemplate.EnablePaging)
      {
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) (this.DataView as GridDataView).GroupBuilder.Groups)
          group.Expand(true);
      }
      else
        this.ExpandOrCollapseAllGroupsRecursively(this.ChildRows, true);
      this.MasterTemplate.SynchronizationService.ResumeDispatch();
      this.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ExpandedChanged));
    }

    public void CollapseAllGroups()
    {
      this.MasterTemplate.SynchronizationService.BeginDispatch();
      if (this.MasterTemplate.EnablePaging)
      {
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) (this.DataView as GridDataView).GroupBuilder.Groups)
          group.Collapse(true);
      }
      else
        this.ExpandOrCollapseAllGroupsRecursively(this.ChildRows, false);
      this.MasterTemplate.SynchronizationService.EndDispatch(true);
      this.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ExpandedChanged));
    }

    public void ExpandAll()
    {
      this.MasterTemplate.BeginUpdate();
      this.MasterTemplate.SynchronizationService.SuspendDispatch();
      this.ExpandOrCollapseRecursively(this.ChildRows, true);
      this.MasterTemplate.SynchronizationService.ResumeDispatch();
      this.MasterTemplate.EndUpdate(false);
      this.MasterTemplate.Owner.TableElement.Update(GridUINotifyAction.ExpandedChanged);
      this.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ExpandedChanged));
    }

    public void CollapseAll()
    {
      this.MasterTemplate.Owner.TableElement.BeginUpdate();
      this.MasterTemplate.SynchronizationService.SuspendDispatch();
      this.ExpandOrCollapseRecursively(this.ChildRows, false);
      this.MasterTemplate.SynchronizationService.ResumeDispatch();
      this.MasterTemplate.Owner.TableElement.EndUpdate(false);
      this.MasterTemplate.Owner.TableElement.Update(GridUINotifyAction.ExpandedChanged);
      this.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ExpandedChanged));
    }

    private void ExpandOrCollapseAllGroupsRecursively(
      GridViewChildRowCollection childRows,
      bool expand)
    {
      foreach (GridViewRowInfo childRow in childRows)
      {
        GridViewGroupRowInfo viewGroupRowInfo = childRow as GridViewGroupRowInfo;
        if (viewGroupRowInfo != null && viewGroupRowInfo.IsVisible)
        {
          viewGroupRowInfo.IsExpanded = expand;
          this.ExpandOrCollapseAllGroupsRecursively(viewGroupRowInfo.ChildRows, expand);
        }
      }
    }

    private void ExpandOrCollapseRecursively(GridViewChildRowCollection childRows, bool expand)
    {
      foreach (GridViewRowInfo childRow in childRows)
      {
        GridViewHierarchyRowInfo hierarchyRowInfo = childRow as GridViewHierarchyRowInfo;
        if ((childRow is GridViewGroupRowInfo || hierarchyRowInfo != null && hierarchyRowInfo.HasChildRows()) && childRow.IsVisible)
        {
          childRow.IsExpanded = expand;
          this.ExpandOrCollapseRecursively(childRow.ChildRows, expand);
        }
      }
    }

    private void MarkLastRow()
    {
      GridTraverser traverser = (GridTraverser) this.MasterTemplate.Owner.TableElement.RowScroller.Traverser;
      GridTraverser.GridTraverserPosition position = traverser.Position;
      traverser.MoveToEnd();
      GridViewHierarchyRowInfo current = traverser.Current as GridViewHierarchyRowInfo;
      if (current != null && current.ChildRow != null)
        current.ChildRow.IsLastRow = true;
      traverser.Position = position;
    }

    private void RefreshAggregates()
    {
      this.RefreshAggregates(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    internal void RefreshAggregates(NotifyCollectionChangedEventArgs e)
    {
      this.RefreshTotalSummaries(e);
      this.RefreshSummaryRowsInGroup(e);
      this.RefreshGroupSummaries(e);
    }

    private void RefreshTotalSummaries(NotifyCollectionChangedEventArgs e)
    {
      if (e == null || e.NewItems == null || (e.NewItems.Count < 1 || !(e.NewItems[0] is GridViewRowInfo)))
      {
        this.InvalidateSummaryRows(this.gridViewInfo);
      }
      else
      {
        foreach (GridViewRowInfo newItem in (IEnumerable) e.NewItems)
        {
          if (newItem != null)
            this.InvalidateSummaryRows(newItem.ViewInfo ?? this.gridViewInfo);
        }
      }
    }

    private void RefreshSummaryRowsInGroup(NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems != null && e.NewItems.Count > 0)
      {
        GridViewRowInfo newItem = e.NewItems[0] as GridViewRowInfo;
        GridViewGroupRowInfo viewGroupRowInfo = newItem != null ? newItem.Parent as GridViewGroupRowInfo : (GridViewGroupRowInfo) null;
        if (viewGroupRowInfo != null)
        {
          this.InvalidateGroupSummaryRows(viewGroupRowInfo.Group, true);
          return;
        }
      }
      if (this.Groups == null)
        return;
      foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
        this.InvalidateGroupSummaryRows(group, true);
    }

    internal void InvalidateGroupSummaryRows(DataGroup group, bool invalidateChildren)
    {
      this.InvalidateSummaryRowsCore(group, invalidateChildren);
      for (group = group.Parent as DataGroup; group != null; group = group.Parent as DataGroup)
        this.InvalidateGroupSummaryRows(group, false);
    }

    private void InvalidateSummaryRowsCore(DataGroup group, bool invalidateChildren)
    {
      foreach (GridViewSummaryRowInfo bottomSummaryRow in group.GroupRow.BottomSummaryRows)
      {
        bottomSummaryRow.ClearCache();
        bottomSummaryRow.InvalidateRow();
      }
      foreach (GridViewSummaryRowInfo topSummaryRow in group.GroupRow.TopSummaryRows)
      {
        topSummaryRow.ClearCache();
        topSummaryRow.InvalidateRow();
      }
      if (!invalidateChildren)
        return;
      foreach (DataGroup group1 in (ReadOnlyCollection<Group<GridViewRowInfo>>) group.Groups)
        this.InvalidateSummaryRowsCore(group1, invalidateChildren);
    }

    private void RefreshGroupSummaries(NotifyCollectionChangedEventArgs e)
    {
      if (e != null && e.NewItems != null && e.NewItems.Count > 0)
      {
        if (e.NewItems[0] is GridViewColumn)
        {
          GridViewTemplate gridViewTemplate = (GridViewTemplate) null;
          foreach (GridViewColumn newItem in (IEnumerable) e.NewItems)
          {
            if (this.ColumnHasSummaryItem(newItem))
            {
              gridViewTemplate = newItem.OwnerTemplate;
              break;
            }
          }
          if (gridViewTemplate == null || this.Groups == null)
            return;
          foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
            this.ClearGroupRowsCache(group);
        }
        else
        {
          foreach (GridViewRowInfo newItem in (IEnumerable) e.NewItems)
            this.RefreshAggregates(newItem.Group);
        }
      }
      else
      {
        if (this.Groups == null)
          return;
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
          this.RefreshAggregates(group);
      }
    }

    private bool ColumnHasSummaryItem(GridViewColumn column)
    {
      foreach (Collection<GridViewSummaryItem> summaryRowGroupHeader in (Collection<GridViewSummaryRowItem>) this.summaryRowGroupHeaders)
      {
        foreach (GridViewSummaryItem gridViewSummaryItem in summaryRowGroupHeader)
        {
          if (gridViewSummaryItem.Name == column.Name)
            return true;
        }
      }
      return false;
    }

    private void ClearGroupRowsCache(DataGroup group)
    {
      if (group.GroupRow != null)
        group.GroupRow.ClearCache();
      if (group.Groups == null)
        return;
      foreach (DataGroup group1 in (ReadOnlyCollection<Group<GridViewRowInfo>>) group.Groups)
        this.ClearGroupRowsCache(group1);
    }

    private void RefreshAggregates(DataGroup group)
    {
      for (; group != null; group = (DataGroup) group.Parent)
      {
        if (group.GroupRow != null)
        {
          group.GroupRow.ClearCache();
          group.GroupRow.InvalidateRow();
        }
      }
    }

    private void InvalidateSummaryRows(GridViewInfo viewInfo)
    {
      if (viewInfo == null)
        return;
      for (int index = 0; index < viewInfo.SummaryRows.Count; ++index)
      {
        GridViewSummaryRowInfo summaryRow = viewInfo.SummaryRows[index];
        summaryRow.ClearCache();
        summaryRow.InvalidateRow();
      }
    }

    internal void OnRowSetAsCurrent(GridViewRowInfo row)
    {
      if (!(row is GridViewDataRowInfo))
        return;
      GridViewTemplate parent = this.parent;
      if (row.Equals((object) this.DataView.CurrentItem) && this.IsSyncBindingSourcePos(row))
        return;
      if (this.ListSource.IsDataBound)
      {
        if (this.HierarchyDataProvider is GridViewRelationDataProvider)
        {
          if ((object) this.GetType() != (object) typeof (MasterGridViewTemplate))
          {
            ((ICurrencyManagerProvider) this.ListSource).CurrencyManager.Position = this.ListSource.IndexOf(row);
            return;
          }
        }
      }
      try
      {
        this.DataView.MoveCurrentTo(row);
      }
      catch (Exception ex)
      {
        this.SetError(new GridViewCellCancelEventArgs(row, (GridViewColumn) null, (IInputEditor) null), ex);
      }
    }

    private bool IsSyncBindingSourcePos(GridViewRowInfo row)
    {
      if (this.listSource.Position >= 0 && this.listSource.Position < this.listSource.Count)
        return this.listSource[this.listSource.Position].Equals((object) row);
      return false;
    }

    private void UpdateCurrentOnRowDeleted(
      GridViewInfo viewInfo,
      GridViewHierarchyRowInfo hierarchyRow,
      int rowIndex)
    {
      GridViewRowInfo row = (GridViewRowInfo) null;
      GridViewColumn currentColumn = viewInfo.ViewTemplate.CurrentColumn;
      if (hierarchyRow != null && viewInfo.ChildRows.Count <= 0)
        row = MasterGridViewTemplate.GetDefaultCurrentRow(this, viewInfo, false);
      if (row == null)
        row = this.DataView.CurrentItem;
      if (row == null)
        row = MasterGridViewTemplate.GetDefaultCurrentRow(this, viewInfo, false);
      if (row != null)
        currentColumn = row.ViewTemplate.CurrentColumn;
      GridViewSynchronizationService.RaiseCurrentChanged(this, row, currentColumn, false);
    }

    private void UpdateCurrentRow(
      GridViewRowInfo currentRow,
      GridViewColumn currentColumn,
      object originator)
    {
      GridViewSynchronizationService.RaiseCurrentChanged(this, currentRow, currentColumn, originator == null);
    }

    private void BeginCurrentRowUpdate(bool suspendService)
    {
      if (suspendService)
        GridViewSynchronizationService.SuspendEvent(this, KnownEvents.CurrentChanged);
      if (this.update > 0)
        return;
      MasterGridViewTemplate masterTemplate = this.MasterTemplate;
      if (masterTemplate == null || masterTemplate.CurrentRow == null || masterTemplate.CurrentRow.ViewTemplate != this)
        return;
      this.state[536870912L] = true;
    }

    private void EndCurrentRowUpdate(bool resumeService, bool update)
    {
      if (resumeService)
        GridViewSynchronizationService.ResumeEvent(this, KnownEvents.CurrentChanged);
      if (this.update > 0 || this.IsFilteringPerformed())
        return;
      bool flag = false;
      if (update)
      {
        flag = this.state[536870912L];
        if (!flag)
          flag = this.MasterTemplate != null;
      }
      this.state[536870912L] = false;
      if (!flag)
        return;
      this.UpdateCurrentRow(this.DataView.CurrentItem, this.currentColumn, (object) this.DataView);
    }

    internal bool ProcessingData
    {
      get
      {
        return this.state[274877906944L];
      }
      set
      {
        this.state[274877906944L] = value;
      }
    }

    private bool PerformFiltering(GridViewRowInfo row)
    {
      GridViewCustomFilteringEventArgs args = new GridViewCustomFilteringEventArgs(this, row);
      this.EventDispatcher.RaiseEvent<GridViewCustomFilteringEventArgs>(EventDispatcher.CustomFiltering, (object) this, args);
      if (args.Handled)
        return args.Visible;
      return this.DataView.DefaultFilter(row);
    }

    private bool PerformHierarchyFilter(GridViewRowInfo rowInfo)
    {
      if (this.FilterDescriptors.Count == 0)
        return true;
      ExpressionContext context = ExpressionContext.Context;
      context.Clear();
      foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.FilterDescriptors)
        this.AddToContext(context, filterDescriptor, rowInfo);
      this.expressionNode = DataUtils.Parse(this.FilterDescriptors.Expression, false);
      object obj1 = this.expressionNode.Eval((object) null, (object) context);
      if (obj1 is bool && (bool) obj1)
        return true;
      GridViewHierarchyRowInfo hierarchyRowInfo1 = rowInfo as GridViewHierarchyRowInfo;
      if (hierarchyRowInfo1 == null)
        return this.DataView.DefaultFilter(rowInfo);
      Stack<GridViewHierarchyRowInfo> hierarchyRowInfoStack = new Stack<GridViewHierarchyRowInfo>();
      if (hierarchyRowInfo1.HasChildRows())
        hierarchyRowInfoStack.Push(hierarchyRowInfo1);
      while (hierarchyRowInfoStack.Count > 0)
      {
        GridViewHierarchyRowInfo hierarchyRowInfo2 = hierarchyRowInfoStack.Pop();
        if (hierarchyRowInfo2.childRows != null)
        {
          foreach (GridViewRowInfo childRow in hierarchyRowInfo2.childRows)
          {
            context.Clear();
            foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.FilterDescriptors)
              this.AddToContext(context, filterDescriptor, childRow);
            object obj2 = this.expressionNode.Eval((object) null, (object) context);
            if (obj2 is bool && (bool) obj2)
              return true;
            GridViewHierarchyRowInfo hierarchyRowInfo3 = childRow as GridViewHierarchyRowInfo;
            if (hierarchyRowInfo3 != null && hierarchyRowInfo3.childRows != null && hierarchyRowInfo3.childRows.Count > 0)
              hierarchyRowInfoStack.Push(hierarchyRowInfo3);
          }
        }
      }
      return false;
    }

    private void AddToContext(
      ExpressionContext context,
      FilterDescriptor descriptor,
      GridViewRowInfo rowInfo)
    {
      CompositeFilterDescriptor filterDescriptor1 = descriptor as CompositeFilterDescriptor;
      if (filterDescriptor1 != null)
      {
        foreach (FilterDescriptor filterDescriptor2 in (Collection<FilterDescriptor>) filterDescriptor1.FilterDescriptors)
          this.AddToContext(context, filterDescriptor2, rowInfo);
      }
      else
      {
        if (context.ContainsKey(descriptor.PropertyName))
          return;
        object int32 = this.Columns[descriptor.PropertyName].GetValue(rowInfo, GridViewDataOperation.Filtering);
        if (int32 is Enum)
          int32 = (object) Convert.ToInt32(int32);
        context.Add(descriptor.PropertyName, int32);
      }
    }

    private object PerformGrouping(GridViewRowInfo row, int level)
    {
      GridViewCustomGroupingEventArgs args = new GridViewCustomGroupingEventArgs(this, row, level);
      this.EventDispatcher.RaiseEvent<GridViewCustomGroupingEventArgs>(EventDispatcher.CustomGrouping, (object) this, args);
      if (args.Handled)
        return args.GroupKey;
      return this.DataView.DefaultGroupPredicate(row, level);
    }

    private NotifyCollectionChangedAction ConvertNotificationReason(
      ViewChangedAction action)
    {
      switch (action)
      {
        case ViewChangedAction.Add:
          return NotifyCollectionChangedAction.Add;
        case ViewChangedAction.Remove:
          return NotifyCollectionChangedAction.Remove;
        case ViewChangedAction.Replace:
          return NotifyCollectionChangedAction.Replace;
        case ViewChangedAction.Move:
          return NotifyCollectionChangedAction.Move;
        case ViewChangedAction.Reset:
          return NotifyCollectionChangedAction.Reset;
        case ViewChangedAction.ItemChanged:
          return NotifyCollectionChangedAction.ItemChanged;
        default:
          throw new ArgumentException("Conversion failed! You cannot convert NotifyCollectionChangedAction: " + action.ToString() + "to a valid ViewChangedAction value");
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Predicate<GridViewRowInfo> FilterPredicate
    {
      get
      {
        return this.DataView.Filter;
      }
      set
      {
        if (!(this.DataView.Filter != value))
          return;
        this.DataView.Filter = value;
        this.OnNotifyPropertyChanged(nameof (FilterPredicate));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Telerik.WinControls.Data.GroupPredicate<GridViewRowInfo> GroupPredicate
    {
      get
      {
        return this.DataView.GroupPredicate;
      }
      set
      {
        if (!(this.DataView.GroupPredicate != value))
          return;
        this.DataView.GroupPredicate = value;
        this.OnNotifyPropertyChanged(nameof (GroupPredicate));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IComparer<GridViewRowInfo> SortComparer
    {
      get
      {
        return this.DataView.Comparer;
      }
      set
      {
        if (this.DataView.Comparer == value)
          return;
        this.DataView.Comparer = value;
        this.OnNotifyPropertyChanged(nameof (SortComparer));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IComparer<Group<GridViewRowInfo>> GroupComparer
    {
      get
      {
        return this.DataView.GroupComparer;
      }
      set
      {
        if (this.DataView.GroupComparer == value)
          return;
        this.DataView.GroupComparer = value;
        if (this.IsSelfReference && this.gridViewInfo.LoadedData != null)
          this.gridViewInfo.LoadedData.GroupComparer = value;
        this.OnNotifyPropertyChanged(nameof (GroupComparer));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadListSource<GridViewRowInfo> ListSource
    {
      get
      {
        return (RadListSource<GridViewRowInfo>) this.listSource;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadCollectionView<GridViewRowInfo> DataView
    {
      get
      {
        return this.ListSource.CollectionView;
      }
    }

    internal int SummaryRowsVersion
    {
      get
      {
        return this.summaryRowsVersion;
      }
      set
      {
        this.summaryRowsVersion = value;
      }
    }

    private DataViewChangedEventArgs GetViewChangedArgs(
      NotifyCollectionChangedEventArgs e)
    {
      ViewChangedAction action = ViewChangedAction.DataChanged;
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          action = ViewChangedAction.Add;
          break;
        case NotifyCollectionChangedAction.Remove:
          action = ViewChangedAction.Remove;
          break;
        case NotifyCollectionChangedAction.Replace:
          action = ViewChangedAction.Replace;
          break;
        case NotifyCollectionChangedAction.Move:
          action = ViewChangedAction.Move;
          break;
        case NotifyCollectionChangedAction.Reset:
          switch (e.ResetReason)
          {
            case CollectionResetReason.FilteringChanged:
              action = ViewChangedAction.FilteringChanged;
              break;
            case CollectionResetReason.GroupingChanged:
              action = ViewChangedAction.GroupingChanged;
              break;
            case CollectionResetReason.SortingChanged:
              action = ViewChangedAction.SortingChanged;
              break;
            case CollectionResetReason.PagingChanged:
              action = ViewChangedAction.PagingChanged;
              break;
            default:
              action = ViewChangedAction.Reset;
              break;
          }
        case NotifyCollectionChangedAction.Batch:
          action = ViewChangedAction.DataChanged;
          break;
        case NotifyCollectionChangedAction.ItemChanging:
          action = ViewChangedAction.ItemChanging;
          break;
        case NotifyCollectionChangedAction.ItemChanged:
          action = ViewChangedAction.ItemChanged;
          break;
      }
      return new DataViewChangedEventArgs(action, e.NewItems, e.OldItems, e.NewStartingIndex, e.PropertyName);
    }

    private void AutoGenerateColumnsFromReader(IDataReader dataReader)
    {
      this.columns.BeginUpdate();
      for (int i = 0; i < dataReader.FieldCount; ++i)
      {
        string name = dataReader.GetName(i);
        System.Type fieldType = dataReader.GetFieldType(i);
        GridViewDataColumn gridViewDataColumn = (object) fieldType == (object) typeof (bool) || (object) fieldType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState) ? (GridViewDataColumn) new GridViewCheckBoxColumn() : ((object) fieldType == (object) typeof (byte[]) || (object) fieldType == (object) typeof (Image) || ((object) fieldType == (object) typeof (Icon) || (object) fieldType == (object) typeof (Bitmap)) ? (GridViewDataColumn) new GridViewImageColumn() : ((object) fieldType != (object) typeof (DateTime) ? ((object) fieldType == (object) typeof (Decimal) || (object) fieldType == (object) typeof (int) ? (GridViewDataColumn) new GridViewDecimalColumn() : ((object) fieldType != (object) typeof (Color) ? (GridViewDataColumn) new GridViewTextBoxColumn() : (GridViewDataColumn) new GridViewColorColumn())) : (GridViewDataColumn) new GridViewDateTimeColumn()));
        gridViewDataColumn.Name = name;
        gridViewDataColumn.FieldName = string.Empty;
        gridViewDataColumn.HeaderText = name;
        gridViewDataColumn.DataType = fieldType;
        gridViewDataColumn.IsAutoGenerated = true;
        this.columns.Add(gridViewDataColumn);
      }
      this.columns.EndUpdate(false);
    }

    private void ClearAutoGeneratedColumns()
    {
      if (!this.AutoGenerateColumns)
        return;
      this.columns.BeginUpdate();
      int index = 0;
      while (index < this.columns.Count)
      {
        if (this.columns[index].IsAutoGenerated)
        {
          this.ResetSystemRowsCache((GridViewColumn) this.columns[index]);
          this.columns.RemoveAt(index);
        }
        else
          ++index;
      }
      this.columns.EndUpdate(false);
    }

    private bool MergeAndClearSchema()
    {
      bool flag1 = false;
      this.columns.BeginUpdate();
      int index1 = 0;
      Dictionary<string, PropertyDescriptor> descriptors = (Dictionary<string, PropertyDescriptor>) null;
      byte[] metadataHash = this.ComputeMetadataHash();
      bool flag2 = false;
      if (this.AutoGenerateColumns && this.metaDataHash != null)
      {
        descriptors = new Dictionary<string, PropertyDescriptor>(this.listSource.BoundProperties.Count);
        for (int index2 = 0; index2 < this.listSource.BoundProperties.Count; ++index2)
        {
          if (!descriptors.ContainsKey(this.listSource.BoundProperties[index2].Name))
            descriptors.Add(this.listSource.BoundProperties[index2].Name, this.listSource.BoundProperties[index2]);
        }
        flag2 = !Telerik.WinControls.ClientUtils.ArraysEqual(metadataHash, this.metaDataHash);
      }
      while (index1 < this.columns.Count)
      {
        PropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(this.columns[index1]);
        GridViewDataColumn column = this.columns[index1];
        if (!flag1)
          flag1 = propertyDescriptor != null;
        if (column.IsAutoGenerated && propertyDescriptor == null)
        {
          if (this.currentColumn == column)
            this.currentColumn = (GridViewColumn) null;
          this.FilterDescriptors.Remove(column.FieldName);
          this.ResetSystemRowsCache((GridViewColumn) this.columns[index1]);
          this.columns.RemoveAt(index1);
        }
        else
        {
          if (propertyDescriptor != null)
          {
            if (this.AutoGenerateColumns && this.metaDataHash != null)
              descriptors.Remove(propertyDescriptor.Name);
            if ((object) column.DataType != (object) propertyDescriptor.PropertyType)
              this.MergeFilterDescriptors(column.FieldName, propertyDescriptor.PropertyType);
          }
          ++index1;
        }
      }
      if (flag2 && this.AutoGenerateColumns && this.metaDataHash != null)
        this.AutoGenerateColumnsFrom(descriptors);
      this.columns.EndUpdate(false);
      this.metaDataHash = metadataHash;
      return flag1;
    }

    private byte[] ComputeMetadataHash()
    {
      StringBuilder stringBuilder = new StringBuilder(1024);
      for (int index = 0; index < this.listSource.BoundProperties.Count; ++index)
        stringBuilder.Append(this.listSource.BoundProperties[index].Name);
      return new SHA1CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(stringBuilder.ToString()));
    }

    private void AutoGenerateColumnsFrom(Dictionary<string, PropertyDescriptor> descriptors)
    {
      foreach (PropertyDescriptor descriptor in descriptors.Values)
      {
        if ((object) descriptor.PropertyType != (object) typeof (IBindingList))
        {
          string name = descriptor.Name;
          GridViewDataColumn gridViewDataColumn = this.AutoGenerateGridColumnFromTypeConverter(descriptor) ?? GridViewHelper.AutoGenerateGridColumn(descriptor.PropertyType, (ISite) null);
          gridViewDataColumn.FieldName = name;
          string str = GridViewHelper.GetCaption(descriptor) ?? descriptor.DisplayName;
          gridViewDataColumn.HeaderText = str;
          gridViewDataColumn.IsAutoGenerated = true;
          gridViewDataColumn.ReadOnly = descriptor.IsReadOnly;
          gridViewDataColumn.OwnerTemplate = this;
          this.columns.Add(gridViewDataColumn);
        }
      }
    }

    private void MergeFilterDescriptors(string propertyName, System.Type propertyType)
    {
      List<FilterOperationContext> supportedOperators = FilterOperationContext.GetFilterOperations(propertyType);
      Predicate<FilterDescriptor> predicate = (Predicate<FilterDescriptor>) (filterDescriptor =>
      {
        if (filterDescriptor is CompositeFilterDescriptor)
          return false;
        foreach (FilterOperationContext operationContext in supportedOperators)
        {
          if (operationContext.Operator == filterDescriptor.Operator)
            return false;
        }
        return true;
      });
      this.FilterDescriptors.Remove(propertyName, predicate);
    }

    private void ResetSystemRowsCache()
    {
      this.gridViewInfo.TableFilteringRow.Cache.Clear();
    }

    private void ResetSystemRowsCache(GridViewColumn column)
    {
      this.gridViewInfo.TableFilteringRow.Cache.Remove(column);
    }

    internal void SetCurrentColumn(GridViewColumn column, bool suspendCurrentChanged)
    {
      this.currentColumn = column;
      if (this.currentColumn == null)
        return;
      if (suspendCurrentChanged)
        this.currentColumn.SuspendPropertyNotifications();
      this.currentColumn.IsCurrent = true;
      if (!suspendCurrentChanged)
        return;
      this.currentColumn.ResumePropertyNotifications();
    }

    internal void BeginAddRange()
    {
      this.state[137438953472L] = true;
    }

    internal void EndAddRange()
    {
      this.state[137438953472L] = false;
    }

    private class DeferHelper : IDisposable
    {
      private GridViewTemplate template;

      public DeferHelper(GridViewTemplate template)
      {
        this.template = template;
      }

      public void Dispose()
      {
        if (this.template == null)
          return;
        this.template.EndDefer();
        this.template = (GridViewTemplate) null;
      }
    }
  }
}
