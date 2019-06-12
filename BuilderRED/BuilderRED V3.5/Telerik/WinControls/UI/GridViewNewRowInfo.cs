// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewNewRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewNewRowInfo : GridViewSystemRowInfo
  {
    private bool newRowInitialized;
    private bool moveToLastRow;
    private HybridDictionary changedColumns;
    private GridViewRowInfo boundRow;
    private GridViewRowInfo addedRow;
    private bool deferUserAddedRow;

    public GridViewNewRowInfo(GridViewInfo gridViewInfo)
      : base(gridViewInfo)
    {
      this.SuspendPropertyNotifications();
      this.MinHeight = 18;
      this.PinPosition = PinnedRowPosition.Top;
      this.ViewInfo.PinnedRows.UpdateRow((GridViewRowInfo) this);
      this.ResumePropertyNotifications();
      this.changedColumns = new HybridDictionary();
    }

    public override Type RowElementType
    {
      get
      {
        return typeof (GridNewRowElement);
      }
    }

    public override AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.Current;
      }
    }

    internal bool MoveToLastRow
    {
      get
      {
        return this.moveToLastRow;
      }
      set
      {
        this.moveToLastRow = value;
      }
    }

    internal override object this[GridViewColumn column]
    {
      get
      {
        if (this.boundRow != null)
          return this.boundRow[column];
        return base[column];
      }
      set
      {
        GridViewRowInfo rowInfo = (GridViewRowInfo) this;
        if (this.boundRow != null)
          rowInfo = this.boundRow;
        this.SetValue(rowInfo, column, value);
      }
    }

    public override object DataBoundItem
    {
      get
      {
        if (this.boundRow != null)
          return this.boundRow.DataBoundItem;
        return base.DataBoundItem;
      }
    }

    internal GridViewRowInfo AddedRow
    {
      get
      {
        return this.addedRow;
      }
    }

    private void SetValue(GridViewRowInfo rowInfo, GridViewColumn column, object value)
    {
      if (rowInfo[column] == value)
        return;
      rowInfo.ViewTemplate.ListSource.BeginUpdate();
      if (rowInfo == this)
        base[column] = value;
      else
        rowInfo[column] = value;
      rowInfo.ViewTemplate.ListSource.EndUpdate(false);
      rowInfo.IsModified = true;
      this.InvalidateRow();
      if (this.changedColumns.Contains((object) column))
        return;
      this.changedColumns.Add((object) column, (object) true);
    }

    internal bool Validated { get; set; }

    protected override int CompareToSystemRowInfo(GridViewSystemRowInfo row)
    {
      if (row is GridViewTableHeaderRowInfo)
        return 1;
      if (row is GridViewFilteringRowInfo)
        return -1;
      if (row is GridViewSearchRowInfo)
        return this.PinPosition == PinnedRowPosition.Top ? -1 : 1;
      if (row is GridViewNewRowInfo)
        return 0;
      return base.CompareToSystemRowInfo(row);
    }

    public void InitializeNewRow()
    {
      if (this.newRowInitialized)
        return;
      if (!this.ViewTemplate.IsVirtualRows)
      {
        if (this.ViewTemplate.ListSource.IsDataBound && this.ViewTemplate.MasterTemplate.AddNewBoundRowBeforeEdit)
        {
          this.ViewTemplate.BeginUpdate();
          this.boundRow = this.ViewTemplate.ListSource.AddNew();
          this.boundRow.IsVisible = false;
          this.ViewTemplate.EndUpdate(false);
          (this.boundRow.DataBoundItem as IEditableObject)?.BeginEdit();
        }
        this.FillParentData();
      }
      if (this.ViewTemplate != null && this.ViewTemplate.ListSource != null)
        this.ViewTemplate.ListSource.CreateATransactionForEveryValueSetting = false;
      DefaultValuesNeededEventArgs args = new DefaultValuesNeededEventArgs((GridViewRowInfo) this);
      this.ViewTemplate.EventDispatcher.RaiseEvent<DefaultValuesNeededEventArgs>(EventDispatcher.DefaultValuesNeeded, (object) this.ViewTemplate, args);
      this.IsModified = args.AddWithDefaultValues;
      this.newRowInitialized = true;
    }

    private void FillParentData()
    {
      GridViewHierarchyRowInfo parent = this.Parent as GridViewHierarchyRowInfo;
      if (parent == null)
        return;
      GridViewRelation relation = this.ViewTemplate.HierarchyDataProvider.Relation;
      if (relation == null)
        return;
      for (int index = 0; index < relation.ChildColumnNames.Count; ++index)
      {
        GridViewColumn column = (GridViewColumn) this.ViewTemplate.Columns[relation.ChildColumnNames[index]];
        if (column != null)
          this[column] = parent[(GridViewColumn) parent.ViewTemplate.Columns[relation.ParentColumnNames[index]]];
      }
      this.InvalidateRow();
    }

    public GridViewRowInfo EndAddNewRow()
    {
      if (!this.newRowInitialized || !this.IsModified)
        return (GridViewRowInfo) null;
      GridViewCollectionChangingEventArgs args1 = new GridViewCollectionChangingEventArgs(this.ViewTemplate, Telerik.WinControls.Data.NotifyCollectionChangedAction.Add, (object) null, this.ViewTemplate.Rows.Count, -1);
      this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.RowsChanging, (object) this.ViewTemplate, args1);
      if (args1.Cancel)
        return (GridViewRowInfo) null;
      GridViewRowCancelEventArgs args2 = new GridViewRowCancelEventArgs(new GridViewRowInfo[1]
      {
        (GridViewRowInfo) this
      });
      this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewRowCancelEventArgs>(EventDispatcher.UserAddingRow, (object) this.ViewTemplate, args2);
      if (args2.Cancel)
        return (GridViewRowInfo) null;
      if (this.ViewTemplate.IsVirtualRows)
      {
        GridViewObjectRelationalDataProvider hierarchyDataProvider = this.ViewTemplate.HierarchyDataProvider as GridViewObjectRelationalDataProvider;
        GridViewRowInfo rowInfo = (GridViewRowInfo) this;
        if (hierarchyDataProvider != null && this.ViewTemplate.AutoUpdateObjectRelationalSource)
        {
          rowInfo = hierarchyDataProvider.AddNewRow(this) ?? (GridViewRowInfo) this;
          this.ViewInfo.Refresh();
          if (rowInfo != this)
          {
            if (!this.deferUserAddedRow)
            {
              GridViewRowEventArgs args3 = new GridViewRowEventArgs(rowInfo);
              this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewRowEventArgs>(EventDispatcher.UserAddedRow, (object) this.ViewTemplate, args3);
            }
            else
            {
              this.addedRow = rowInfo;
              this.deferUserAddedRow = false;
            }
            if (this.moveToLastRow)
            {
              rowInfo.IsCurrent = true;
              this.moveToLastRow = false;
            }
            else
              this.ViewTemplate.MasterTemplate.CurrentRow = (GridViewRowInfo) this.ViewInfo.TableAddNewRow;
          }
        }
        else
        {
          GridViewRowEventArgs args3 = new GridViewRowEventArgs((GridViewRowInfo) this);
          this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewRowEventArgs>(EventDispatcher.UserAddedRow, (object) this.ViewTemplate, args3);
        }
        GridViewSynchronizationService.DispatchEvent(this.ViewTemplate, new GridViewEvent((object) this.ViewTemplate, (object) this.ViewTemplate, new object[1]
        {
          (object) new DataViewChangedEventArgs(ViewChangedAction.Add, (object) this)
        }, new GridViewEventInfo(KnownEvents.ViewChanged, GridEventType.Both, GridEventDispatchMode.Send)), false);
        return rowInfo;
      }
      int count = this.ViewTemplate.ListSource.Count;
      GridViewRowInfo rowInfo1 = this.boundRow;
      try
      {
        this.ViewTemplate.Rows.addingThroughUI = true;
        this.ViewTemplate.BeginUpdate();
        IEditableObject editableObject = rowInfo1 != null ? rowInfo1.DataBoundItem as IEditableObject : (IEditableObject) null;
        if (rowInfo1 == null)
        {
          rowInfo1 = this.ViewTemplate.ListSource.AddNew();
          editableObject = rowInfo1.DataBoundItem as IEditableObject;
          editableObject?.BeginEdit();
        }
        else
          this.boundRow.IsVisible = true;
        rowInfo1.AddingNewDataRow = true;
        bool flag = false;
        for (int index = 0; index < this.ViewTemplate.Columns.Count; ++index)
        {
          GridViewColumn column = (GridViewColumn) this.ViewTemplate.Columns[index];
          object obj = this[column];
          if (obj == null)
          {
            GridViewCheckBoxColumn viewCheckBoxColumn = column as GridViewCheckBoxColumn;
            if (viewCheckBoxColumn != null && viewCheckBoxColumn.DataType.IsValueType)
            {
              object instance = Activator.CreateInstance(viewCheckBoxColumn.DataType);
              rowInfo1[column] = instance;
            }
          }
          else if (this.changedColumns.Contains((object) column))
          {
            flag = true;
            rowInfo1[column] = obj;
          }
        }
        if (flag)
        {
          editableObject?.EndEdit();
          this.ViewTemplate.EndUpdate(false);
          GridViewSynchronizationService.SuspendEvent(this.ViewTemplate, KnownEvents.CurrentChanged);
          ((ICancelAddNew) this.ViewTemplate.ListSource).EndNew(this.ViewTemplate.ListSource.Count - 1);
          GridViewSynchronizationService.ResumeEvent(this.ViewTemplate, KnownEvents.CurrentChanged);
          if (!this.deferUserAddedRow)
          {
            GridViewRowEventArgs args3 = new GridViewRowEventArgs(rowInfo1);
            this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewRowEventArgs>(EventDispatcher.UserAddedRow, (object) this.ViewTemplate, args3);
            this.addedRow = (GridViewRowInfo) null;
          }
          else
            this.addedRow = rowInfo1;
          this.deferUserAddedRow = false;
          if (this.moveToLastRow)
          {
            rowInfo1.IsCurrent = true;
            this.moveToLastRow = false;
          }
          else
            this.ViewTemplate.MasterTemplate.CurrentRow = (GridViewRowInfo) this.ViewInfo.TableAddNewRow;
        }
        else
        {
          if (this.boundRow != null || this.ViewTemplate.ListSource.Count > count)
            ((ICancelAddNew) this.ViewTemplate.ListSource).CancelNew(this.ViewTemplate.ListSource.Count - 1);
          this.ViewTemplate.EndUpdate(false);
        }
      }
      catch (Exception ex)
      {
        if (this.boundRow != null || this.ViewTemplate.ListSource.Count > count)
          ((ICancelAddNew) this.ViewTemplate.ListSource).CancelNew(this.ViewTemplate.ListSource.Count - 1);
        this.ViewTemplate.EndUpdate(false);
        this.ViewTemplate.SetError(new GridViewCellCancelEventArgs((GridCellElement) null, (IInputEditor) null), ex);
        return (GridViewRowInfo) null;
      }
      finally
      {
        this.boundRow = (GridViewRowInfo) null;
        this.newRowInitialized = false;
        this.IsModified = false;
        this.ClearCache();
        this.changedColumns.Clear();
        this.InvalidateRow();
        this.ViewTemplate.Rows.addingThroughUI = false;
        if (this.ViewTemplate != null && this.ViewTemplate.ListSource != null)
          this.ViewTemplate.ListSource.CreateATransactionForEveryValueSetting = true;
        if (rowInfo1 != null)
          rowInfo1.AddingNewDataRow = false;
        if (this.ViewInfo.ParentRow != null && this.ViewInfo.SummaryRows.Count > 0)
        {
          ++this.ViewInfo.summaryValueVersion;
          this.ViewTemplate.MasterTemplate.SynchronizationService.DispatchEvent(new GridViewEvent((object) this.ViewTemplate, (object) this, new object[1]
          {
            (object) new DataViewChangedEventArgs(ViewChangedAction.DataChanged, (IList) null)
          }, new GridViewEventInfo(KnownEvents.ViewChanged, GridEventType.Both, GridEventDispatchMode.Send)));
        }
      }
      return rowInfo1;
    }

    internal void DeferUserAddedRow()
    {
      this.deferUserAddedRow = true;
    }

    internal void ResetUserAddedRow()
    {
      this.deferUserAddedRow = false;
    }

    internal void RaiseUserAddedRow()
    {
      if (this.addedRow == null)
        return;
      GridViewRowEventArgs args = new GridViewRowEventArgs(this.addedRow);
      this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewRowEventArgs>(EventDispatcher.UserAddedRow, (object) this.ViewTemplate, args);
      this.addedRow = (GridViewRowInfo) null;
      this.deferUserAddedRow = false;
    }

    internal void ClearBoundRow()
    {
      this.boundRow = (GridViewRowInfo) null;
    }

    public void CancelAddNewRow()
    {
      if (!this.newRowInitialized)
        return;
      if (this.boundRow != null)
        this.ViewTemplate.ListSource.Remove(this.boundRow);
      this.addedRow = (GridViewRowInfo) null;
      this.deferUserAddedRow = false;
      this.boundRow = (GridViewRowInfo) null;
      this.newRowInitialized = false;
      this.IsModified = false;
      this.ClearCache();
      if (this.ViewInfo.ParentRow == null)
        this.IsCurrent = false;
      this.InvalidateRow();
    }

    protected override bool OnBeginEdit()
    {
      bool flag = base.OnBeginEdit();
      this.InitializeNewRow();
      return flag;
    }

    protected override bool OnEndEdit()
    {
      base.OnEndEdit();
      return this.EndAddNewRow() != null;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.PropertyName == "IsCurrent" && !this.IsCurrent)
      {
        this.ClearCache();
        this.newRowInitialized = false;
        if (this.boundRow != null)
          this.ViewTemplate.ListSource.Remove(this.boundRow);
      }
      if (!(e.PropertyName == "RowPosition"))
        return;
      this.ViewTemplate.AddNewRowPosition = this.RowPosition;
    }
  }
}
