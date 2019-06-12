// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewFilterDescriptorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewFilterDescriptorCollection : FilterDescriptorCollection, INotifyCollectionChanging
  {
    private GridViewTemplate owner;

    public GridViewFilterDescriptorCollection(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    public GridViewTemplate Owner
    {
      get
      {
        return this.owner;
      }
    }

    protected override void InsertItem(int index, FilterDescriptor item)
    {
      if (this.owner.MasterTemplate != null && this.owner.MasterTemplate.VirtualMode && this.owner.MasterTemplate.ThrowExceptionOnDataOperationInVirtualMode)
        throw new InvalidOperationException("Filtering operation is not supported in VirtualMode.");
      if (this.Contains(item))
        return;
      int index1 = this.owner.Columns.IndexOf(item.PropertyName);
      if (index1 >= 0 && !this.owner.Columns[index1].AllowFiltering)
      {
        this.owner.Columns[index1].FilterDescriptor = item;
      }
      else
      {
        if (this.owner != null)
        {
          if (!this.OnFilterChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Add, (IList) new object[1]
          {
            (object) item
          }, (IList) new object[0], index, -1, new PropertyChangingEventArgsEx(item.PropertyName, (object) null, item.Value))))
            return;
        }
        base.InsertItem(index, item);
        item.PropertyChanging += new PropertyChangingEventHandlerEx(this.Item_PropertyChanging);
        item.PropertyChanged += new PropertyChangedEventHandler(this.Item_PropertyChanged);
      }
    }

    protected override void SetItem(int index, FilterDescriptor item)
    {
      if (this.owner != null)
      {
        if (!this.OnFilterChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Replace, (IList) new object[1]
        {
          (object) item
        }, (IList) new object[1]
        {
          (object) this[index]
        }, index, -1, new PropertyChangingEventArgsEx(this[index].PropertyName, this[index].Value, item.Value))))
          return;
      }
      this[index].PropertyChanged -= new PropertyChangedEventHandler(this.Item_PropertyChanged);
      this[index].PropertyChanging -= new PropertyChangingEventHandlerEx(this.Item_PropertyChanging);
      base.SetItem(index, item);
      item.PropertyChanged += new PropertyChangedEventHandler(this.Item_PropertyChanged);
      item.PropertyChanging += new PropertyChangingEventHandlerEx(this.Item_PropertyChanging);
    }

    protected override void MoveItem(int oldIndex, int newIndex)
    {
      if (this.owner != null)
      {
        if (!this.OnFilterChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Move, (IList) new object[1]
        {
          (object) this[newIndex]
        }, (IList) new object[1]
        {
          (object) this[oldIndex]
        }, newIndex, oldIndex, new PropertyChangingEventArgsEx(this[oldIndex].PropertyName, this[oldIndex].Value, this[newIndex].Value))))
          return;
      }
      base.MoveItem(oldIndex, newIndex);
    }

    protected override void RemoveItem(int index)
    {
      if (this.owner != null)
      {
        if (!this.OnFilterChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Remove, (IList) new object[0], (IList) new object[1]
        {
          (object) this[index]
        }, -1, index, new PropertyChangingEventArgsEx(this[index].PropertyName, this[index].Value, (object) null))))
          return;
      }
      this[index].PropertyChanging -= new PropertyChangingEventHandlerEx(this.Item_PropertyChanging);
      this[index].PropertyChanged -= new PropertyChangedEventHandler(this.Item_PropertyChanged);
      base.RemoveItem(index);
    }

    protected override void ClearItems()
    {
      if (this.Count == 0)
        return;
      FilterDescriptor[] array = new FilterDescriptor[this.Count];
      this.CopyTo(array, 0);
      if (this.owner != null)
      {
        if (!this.OnFilterChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.Reset, (IList) new object[0], (IList) new object[1]
        {
          (object) array
        }, -1, -1, (PropertyChangingEventArgsEx) null)))
          return;
      }
      for (int index = 0; index < this.Count; ++index)
      {
        this[index].PropertyChanged -= new PropertyChangedEventHandler(this.Item_PropertyChanged);
        this[index].PropertyChanging -= new PropertyChangingEventHandlerEx(this.Item_PropertyChanging);
      }
      base.ClearItems();
    }

    private void Item_PropertyChanging(object sender, PropertyChangingEventArgsEx e)
    {
      if (this.Suspended)
        return;
      FilterDescriptor filterDescriptor1 = sender as FilterDescriptor;
      if (filterDescriptor1 == null || !(e.PropertyName == "Value") && !(e.PropertyName == "Operator"))
        return;
      FilterDescriptor filterDescriptor2 = (FilterDescriptor) filterDescriptor1.Clone();
      if (e.PropertyName == "Value")
        filterDescriptor2.Value = e.NewValue;
      if (e.PropertyName == "Operator")
        filterDescriptor2.Operator = (FilterOperator) e.NewValue;
      int num = this.IndexOf(filterDescriptor1);
      e.Cancel = (!this.OnFilterChanging(new GridViewCollectionChangingEventArgs(this.owner, NotifyCollectionChangedAction.ItemChanging, (IList) new object[1]
      {
        (object) filterDescriptor2
      }, (IList) new object[1]
      {
        (object) filterDescriptor1
      }, num, num, e)) ? 1 : 0) != 0;
      if (e.Cancel || filterDescriptor1.Operator == filterDescriptor2.Operator && filterDescriptor1.Value == filterDescriptor2.Value)
        return;
      this.BeginUpdate();
      filterDescriptor1.Operator = filterDescriptor2.Operator;
      filterDescriptor1.Value = filterDescriptor2.Value;
      if (e.PropertyName == "Value")
        e.NewValue = filterDescriptor2.Value;
      if (e.PropertyName == "Operator")
        e.NewValue = (object) filterDescriptor2.Operator;
      base.EndUpdate(false);
    }

    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (this.Suspended)
        return;
      FilterDescriptor filterDescriptor = sender as FilterDescriptor;
      if (filterDescriptor == null)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) filterDescriptor));
    }

    public override void EndUpdate(bool notify)
    {
      base.EndUpdate(notify);
      if (notify)
        return;
      this.owner.DataView.BeginUpdate();
      this.owner.DataView.FilterExpression = this.Expression;
      this.owner.DataView.EndUpdate(false);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      base.OnCollectionChanged(args);
      if (this.Suspended || this.owner == null)
        return;
      this.ApplyFilter();
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.CollectionChanged, GridEventType.Both, GridEventDispatchMode.Send);
      GridViewSynchronizationService.DispatchEvent(this.owner, new GridViewEvent((object) this, (object) null, new object[1]
      {
        (object) args
      }, eventInfo), false);
      this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangedEventArgs>(EventDispatcher.FilterChangedEvent, (object) this.owner, new GridViewCollectionChangedEventArgs(this.owner, args));
      if (!this.owner.IsSelfReference || !this.owner.EnableHierarchyFiltering)
        return;
      this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.GroupingChanged));
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

    private void ApplyFilter()
    {
      RadCollectionView<GridViewRowInfo> dataView = this.owner.DataView;
      string expression = this.Expression;
      if (string.Compare(dataView.FilterExpression, expression) == 0)
        return;
      FilterExpressionChangedEventArgs args = new FilterExpressionChangedEventArgs(expression);
      this.owner.OnViewChanged((object) this.owner, new DataViewChangedEventArgs(ViewChangedAction.FilterExpressionChanged));
      this.owner.EventDispatcher.RaiseEvent<FilterExpressionChangedEventArgs>(EventDispatcher.FilterExpressionChanged, (object) this.owner, args);
      dataView.FilterExpression = args.FilterExpression;
      if (this.owner.MasterTemplate == null)
        return;
      GridViewRowInfo currentRow = this.owner.MasterTemplate.CurrentRow;
      if (currentRow == null || currentRow.ViewInfo == null || currentRow.ViewInfo.ChildRows.Contains(currentRow))
        return;
      currentRow.ViewTemplate.ListSource.CollectionView.MoveCurrentToPosition(-1);
    }

    public override string Expression
    {
      get
      {
        if (this.Count == 0)
          return string.Empty;
        return this.GetExpression();
      }
      set
      {
        base.Expression = value;
      }
    }

    protected virtual string GetExpression()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.Count; ++index)
      {
        FilterDescriptor filterDescriptor1 = this[index];
        CompositeFilterDescriptor filterDescriptor2 = filterDescriptor1 as CompositeFilterDescriptor;
        string str;
        if (filterDescriptor2 != null)
        {
          foreach (FilterDescriptor filterDescriptor3 in (Collection<FilterDescriptor>) filterDescriptor2.FilterDescriptors)
          {
            if (!string.IsNullOrEmpty(filterDescriptor3.PropertyName))
              this.owner.Columns.GetColumnByFieldName(filterDescriptor3.PropertyName);
          }
          str = CompositeFilterDescriptor.GetCompositeExpression(filterDescriptor2, new Function<FilterDescriptor, object>(this.FormatDescriptorValue));
        }
        else if (!string.IsNullOrEmpty(filterDescriptor1.PropertyName) && this.owner.Columns.GetColumnByFieldName(filterDescriptor1.PropertyName) != null)
        {
          DateFilterDescriptor dateTimeFilterDescriptor = filterDescriptor1 as DateFilterDescriptor;
          str = dateTimeFilterDescriptor == null ? FilterDescriptor.GetExpression(filterDescriptor1, new Function<FilterDescriptor, object>(this.FormatDescriptorValue)) : DateFilterDescriptor.GetExpression(dateTimeFilterDescriptor);
        }
        else
          continue;
        if (!string.IsNullOrEmpty(str))
          stringList.Add(str);
      }
      return string.Join(this.LogicalOperator == FilterLogicalOperator.And ? " AND " : " OR ", stringList.ToArray());
    }

    protected virtual object FormatDescriptorValue(FilterDescriptor descriptor)
    {
      if (descriptor == null)
        return (object) null;
      object obj = descriptor.Value;
      GridViewDateTimeColumn column = this.owner.Columns[descriptor.PropertyName] as GridViewDateTimeColumn;
      if (column != null && obj != null)
        obj = (object) GridViewHelper.ClampDateTime(Convert.ToDateTime(obj), column.FilteringMode);
      return obj;
    }

    private bool OnFilterChanging(GridViewCollectionChangingEventArgs e)
    {
      if (this.Suspended)
        return true;
      this.owner.ProcessingData = true;
      this.owner.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.FilterChangingEvent, (object) this.owner, e);
      if (e.Cancel)
      {
        this.owner.ProcessingData = false;
        return false;
      }
      this.owner.ProcessingData = false;
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
