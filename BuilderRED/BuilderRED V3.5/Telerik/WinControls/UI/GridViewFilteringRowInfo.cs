// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewFilteringRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewFilteringRowInfo : GridViewSystemRowInfo, IGridViewEventListener
  {
    private int update;

    public GridViewFilteringRowInfo(GridViewInfo gridViewInfo)
      : base(gridViewInfo)
    {
      this.SuspendPropertyNotifications();
      this.PinPosition = PinnedRowPosition.Top;
      this.ViewInfo.PinnedRows.UpdateRow((GridViewRowInfo) this);
      this.ResumePropertyNotifications();
      if (gridViewInfo.ViewTemplate.MasterTemplate == null || gridViewInfo.ViewTemplate.MasterTemplate.SynchronizationService == null)
        return;
      gridViewInfo.ViewTemplate.MasterTemplate.SynchronizationService.AddListener((IGridViewEventListener) this);
    }

    internal bool IsSuspended
    {
      get
      {
        return this.update > 0;
      }
    }

    public override Type RowElementType
    {
      get
      {
        return typeof (GridFilterRowElement);
      }
    }

    public override AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.Current;
      }
    }

    internal override object this[GridViewColumn column]
    {
      get
      {
        FilterDescriptor filterDescriptor = this.GetFilterDescriptor(column as GridViewDataColumn);
        if (filterDescriptor != null && filterDescriptor.Operator != FilterOperator.None && (filterDescriptor.Operator != FilterOperator.IsNull && filterDescriptor.Operator != FilterOperator.IsNotNull))
          return filterDescriptor.Value;
        return (object) null;
      }
      set
      {
        bool isDetached = false;
        GridViewDataColumn dataColumn = column as GridViewDataColumn;
        FilterDescriptor filterDescriptor = this.GetFilterDescriptor(dataColumn, out isDetached);
        if (filterDescriptor == null)
          return;
        FilterOperator filterOperator = filterDescriptor.Operator;
        bool flag = !isDetached && (value == null || object.Equals(value, (object) string.Empty) || object.Equals(value, (object) char.MinValue));
        this.SuspendUpdate();
        if (flag)
        {
          if (!dataColumn.SetFilterDescriptor((FilterDescriptor) null))
          {
            this.ResumeUpdate();
            return;
          }
          filterDescriptor = this.GetFilterDescriptor(dataColumn);
        }
        filterDescriptor.Operator = filterOperator;
        GridViewDateTimeColumn viewDateTimeColumn = dataColumn as GridViewDateTimeColumn;
        if (viewDateTimeColumn != null && value is DateTime)
          value = (object) GridViewHelper.ClampDateTime(Convert.ToDateTime(value), viewDateTimeColumn.FilteringMode);
        filterDescriptor.Value = value;
        if (isDetached && value != null)
          this.SetFilterDesriptor(dataColumn, filterDescriptor);
        this.ResumeUpdate();
      }
    }

    internal void SuspendUpdate()
    {
      ++this.update;
    }

    internal void ResumeUpdate()
    {
      if (this.update <= 0)
        return;
      --this.update;
    }

    internal FilterDescriptor GetFilterDescriptor(GridViewDataColumn dataColumn)
    {
      bool isDetached = false;
      return this.GetFilterDescriptor(dataColumn, out isDetached);
    }

    internal FilterDescriptor GetFilterDescriptor(
      GridViewDataColumn dataColumn,
      out bool isDetached)
    {
      isDetached = false;
      FilterDescriptor filterDescriptor = dataColumn.FilterDescriptor;
      FilterExpression filterExpression = filterDescriptor as FilterExpression;
      if (filterExpression != null && filterExpression.FilterDescriptors.Count == 1)
        filterDescriptor = filterExpression.FilterDescriptors[0];
      if (filterDescriptor == null)
      {
        filterDescriptor = this.Cache[(GridViewColumn) dataColumn] as FilterDescriptor;
        isDetached = true;
      }
      else
        this.Cache[(GridViewColumn) dataColumn] = (object) filterDescriptor;
      if (filterDescriptor != null && filterDescriptor.PropertyName != dataColumn.Name)
        filterDescriptor = (FilterDescriptor) null;
      if (filterDescriptor == null)
      {
        FilterOperator filterOperator = this.GetDefaultFilterOperator(dataColumn);
        if (dataColumn is GridViewMultiComboBoxColumn)
          filterOperator = FilterOperator.IsEqualTo;
        filterDescriptor = this.CreateFilterDescriptor(dataColumn, filterOperator);
        isDetached = true;
      }
      return filterDescriptor;
    }

    private FilterOperator GetDefaultFilterOperator(GridViewDataColumn dataColumn)
    {
      Type dataType = dataColumn.DataType;
      GridViewComboBoxColumn viewComboBoxColumn = dataColumn as GridViewComboBoxColumn;
      if (viewComboBoxColumn != null)
        dataType = viewComboBoxColumn.FilteringMemberDataType;
      return GridViewHelper.GetDefaultFilterOperator(dataType);
    }

    internal FilterDescriptor CreateFilterDescriptor(
      GridViewDataColumn dataColumn,
      FilterOperator filterOperator)
    {
      FilterDescriptor filterDescriptor = !(dataColumn is GridViewDateTimeColumn) ? new FilterDescriptor(dataColumn.Name, filterOperator, (object) null) : (FilterDescriptor) new DateFilterDescriptor(dataColumn.Name, filterOperator, new DateTime?(), false);
      this.Cache[(GridViewColumn) dataColumn] = (object) filterDescriptor;
      return filterDescriptor;
    }

    internal bool SetFilterDesriptor(GridViewDataColumn dataColumn, FilterDescriptor descriptor)
    {
      bool isDetached = false;
      bool flag = false;
      FilterDescriptor filterDescriptor1 = this.GetFilterDescriptor(dataColumn, out isDetached);
      this.SuspendUpdate();
      if (dataColumn.SetFilterDescriptor(descriptor))
      {
        if (descriptor != null)
          this.Cache[(GridViewColumn) dataColumn] = (object) descriptor;
        flag = true;
      }
      if ((isDetached || flag) && descriptor == null)
      {
        filterDescriptor1.Operator = FilterOperator.None;
        if (filterDescriptor1 is CompositeFilterDescriptor)
        {
          dataColumn.FilterDescriptor = (FilterDescriptor) null;
          FilterDescriptor filterDescriptor2 = new FilterDescriptor(dataColumn.Name, FilterOperator.None, (object) null);
          this.Cache[(GridViewColumn) dataColumn] = (object) filterDescriptor2;
        }
      }
      this.ResumeUpdate();
      return flag;
    }

    protected override int CompareToSystemRowInfo(GridViewSystemRowInfo row)
    {
      if (row == null || row is GridViewTableHeaderRowInfo || row is GridViewNewRowInfo)
        return 1;
      if (row is GridViewSearchRowInfo)
        return -1;
      if (row is GridViewFilteringRowInfo)
        return 0;
      return base.CompareToSystemRowInfo(row);
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.Data;
      }
    }

    EventListenerPriority IGridViewEventListener.Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    GridEventProcessMode IGridViewEventListener.DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.Process | GridEventProcessMode.AnalyzeQueue;
      }
    }

    GridViewEventResult IGridViewEventListener.PreProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.ProcessEvent(
      GridViewEvent eventData)
    {
      GridViewFilterDescriptorCollection sender1 = eventData.Sender as GridViewFilterDescriptorCollection;
      GridViewTemplate sender2 = eventData.Sender as GridViewTemplate;
      GridViewDataColumn sender3 = eventData.Sender as GridViewDataColumn;
      if (sender2 == this.ViewTemplate && eventData.Info.Id == KnownEvents.ViewChanged)
        this.ProcessViewChanged(eventData.Arguments[0] as DataViewChangedEventArgs);
      else if (sender3 != null && sender3.OwnerTemplate == this.ViewTemplate && (eventData.Info.Id == KnownEvents.PropertyChanged && sender3.FilterDescriptor == null))
        this.CreateFilterDescriptor(sender3, this.GetDefaultFilterOperator(sender3));
      else if (sender1 != null && sender1.Owner == this.ViewTemplate && eventData.Info.Id == KnownEvents.CollectionChanged)
        this.ProcessFilterDescriptorCollectionChanged(eventData.Arguments[0] as NotifyCollectionChangedEventArgs);
      return (GridViewEventResult) null;
    }

    private void ProcessViewChanged(DataViewChangedEventArgs args)
    {
      if (args.Action != ViewChangedAction.Reset)
        return;
      bool flag = false;
      foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
      {
        if (column.FilterDescriptor == null)
        {
          this.CreateFilterDescriptor(column, this.GetDefaultFilterOperator(column));
          if (column is GridViewCheckBoxColumn)
            flag = true;
        }
      }
      if (!flag)
        return;
      this.InvalidateRow();
    }

    private void ProcessFilterDescriptorCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.update > 0)
        return;
      if (e.Action == NotifyCollectionChangedAction.Reset)
        this.ProcessViewChanged(new DataViewChangedEventArgs(ViewChangedAction.Reset));
      else if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        GridViewDataColumn filterDescriptor = this.GetDataColumnByFilterDescriptor(e.NewItems[0] as FilterDescriptor);
        if (filterDescriptor == null)
          return;
        this.CreateFilterDescriptor(filterDescriptor, this.GetDefaultFilterOperator(filterDescriptor));
      }
      else
      {
        if (e.Action != NotifyCollectionChangedAction.Add && e.Action != NotifyCollectionChangedAction.ItemChanged)
          return;
        FilterDescriptor newItem = e.NewItems[0] as FilterDescriptor;
        GridViewDataColumn filterDescriptor = this.GetDataColumnByFilterDescriptor(newItem);
        if (filterDescriptor == null)
          return;
        this.Cache[(GridViewColumn) filterDescriptor] = (object) newItem;
      }
    }

    private GridViewDataColumn GetDataColumnByFilterDescriptor(
      FilterDescriptor descriptor)
    {
      GridViewDataColumn gridViewDataColumn = (GridViewDataColumn) null;
      if (descriptor != null && descriptor.IsFilterEditor && !string.IsNullOrEmpty(descriptor.PropertyName))
        gridViewDataColumn = this.ViewTemplate.Columns[descriptor.PropertyName];
      return gridViewDataColumn;
    }

    GridViewEventResult IGridViewEventListener.PostProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    bool IGridViewEventListener.AnalyzeQueue(List<GridViewEvent> events)
    {
      int count = events.Count;
      for (int index = count - 1; index >= 0; --index)
      {
        GridViewEvent gridViewEvent = events[index];
        if (gridViewEvent.Info.Id == KnownEvents.ViewChanged)
        {
          DataViewChangedEventArgs changedEventArgs = gridViewEvent.Arguments[0] as DataViewChangedEventArgs;
          if (changedEventArgs.Action == ViewChangedAction.FilteringChanged || changedEventArgs.Action == ViewChangedAction.FilterExpressionChanged)
          {
            events.RemoveAll(new Predicate<GridViewEvent>(this.IsGridViewEventToRemove));
            return count != events.Count;
          }
        }
      }
      return false;
    }

    private bool IsGridViewEventToRemove(GridViewEvent gridEvent)
    {
      if (GridViewSynchronizationService.IsTemplatePropertyChangingEvent(gridEvent))
        return (gridEvent.Arguments[0] as PropertyChangingEventArgsEx).PropertyName == "DataSource";
      if (gridEvent.Info.Id != KnownEvents.ViewChanged)
        return false;
      DataViewChangedEventArgs changedEventArgs = gridEvent.Arguments[0] as DataViewChangedEventArgs;
      int action = (int) changedEventArgs.Action;
      return changedEventArgs.Action == ViewChangedAction.Reset;
    }
  }
}
