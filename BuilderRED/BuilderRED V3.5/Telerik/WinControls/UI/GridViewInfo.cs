// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class GridViewInfo
  {
    private int version = -1;
    private int summaryRowsVersion = -1;
    internal int summaryValueVersion = -1;
    private GridViewTemplate ownerTemplate;
    private GridViewChildRowCollection childRows;
    private GridViewHierarchyRowInfo parentRow;
    private GridViewSystemRowCollection systemRows;
    private GridViewPinnedRowCollection pinnedRows;
    private GridViewSummaryRowCollection summaryRows;
    private GridViewTableHeaderRowInfo tableHeaderRow;
    private GridViewFilteringRowInfo tableFilteringRow;
    private GridViewNewRowInfo tableAddNewRow;
    private GridViewSearchRowInfo tableSearchRow;
    private ICollectionView<GridViewRowInfo> data;

    public GridViewInfo(GridViewTemplate ownerTemplate)
    {
      this.ownerTemplate = ownerTemplate;
      this.pinnedRows = new GridViewPinnedRowCollection(this);
      this.summaryRows = new GridViewSummaryRowCollection((IList<GridViewSummaryRowInfo>) new List<GridViewSummaryRowInfo>());
      this.systemRows = new GridViewSystemRowCollection(this);
      this.childRows = new GridViewChildRowCollection();
      this.ownerTemplate.PropertyChanged += new PropertyChangedEventHandler(this.OnTemplatePropertyChanged);
    }

    public GridViewInfo(GridViewTemplate ownerTemplate, GridViewHierarchyRowInfo parentRow)
      : this(ownerTemplate)
    {
      this.parentRow = parentRow;
    }

    internal ICollectionView<GridViewRowInfo> LoadedData
    {
      get
      {
        return this.data;
      }
    }

    public virtual bool NeedsRefresh
    {
      get
      {
        bool flag = this.version != this.ownerTemplate.DataView.Version;
        if (!flag && (!this.ownerTemplate.EnableHierarchyFiltering && this.ownerTemplate.IsSelfReference || this.ownerTemplate.EnableCustomFiltering && this.ownerTemplate.GroupDescriptors.Count > 0 && this.childRows.Count == 0))
          flag = true;
        return flag;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewTemplate ViewTemplate
    {
      get
      {
        return this.ownerTemplate;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewHierarchyRowInfo ParentRow
    {
      get
      {
        return this.parentRow;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int CurrentIndex
    {
      get
      {
        return this.ChildRows.IndexOf(this.CurrentRow);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewTableHeaderRowInfo TableHeaderRow
    {
      get
      {
        this.InitializeTableHeaderRow();
        return this.tableHeaderRow;
      }
    }

    protected virtual void InitializeTableHeaderRow()
    {
      if (this.tableHeaderRow != null)
        return;
      this.tableHeaderRow = this.CreateTableHeaderRow();
      this.systemRows.Add((GridViewSystemRowInfo) this.tableHeaderRow);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewFilteringRowInfo TableFilteringRow
    {
      get
      {
        this.InitializeTableFilteringRow();
        return this.tableFilteringRow;
      }
    }

    protected virtual void InitializeTableFilteringRow()
    {
      if (this.tableFilteringRow != null)
        return;
      this.tableFilteringRow = this.CreateFilteringRow();
      this.systemRows.Add((GridViewSystemRowInfo) this.tableFilteringRow);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewNewRowInfo TableAddNewRow
    {
      get
      {
        this.InitializeTableAddNewRow();
        return this.tableAddNewRow;
      }
    }

    protected virtual void InitializeTableAddNewRow()
    {
      if (this.tableAddNewRow != null)
        return;
      this.tableAddNewRow = this.CreateAddNewRow();
      this.systemRows.Add((GridViewSystemRowInfo) this.tableAddNewRow);
      this.TableAddNewRow.SuspendPropertyNotifications();
      this.SynchronizeNewRowPosition();
      this.TableAddNewRow.ResumePropertyNotifications();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewSearchRowInfo TableSearchRow
    {
      get
      {
        this.InitializeTableSearchRow();
        return this.tableSearchRow;
      }
    }

    protected virtual void InitializeTableSearchRow()
    {
      if (this.tableSearchRow != null)
        return;
      this.tableSearchRow = this.CreateSearchRow();
      this.tableSearchRow.SuspendPropertyNotifications();
      this.tableSearchRow.IsVisible = this.ownerTemplate.AllowSearchRow;
      this.tableSearchRow.ResumePropertyNotifications();
      this.systemRows.Add((GridViewSystemRowInfo) this.tableSearchRow);
      this.SynchronizeSearchRowPosition();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewSystemRowCollection SystemRows
    {
      get
      {
        this.InitializeTableHeaderRow();
        this.InitializeTableAddNewRow();
        this.InitializeTableSearchRow();
        this.InitializeTableFilteringRow();
        return this.systemRows;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasChildRows()
    {
      if (this.ownerTemplate.EnableFiltering && this.ownerTemplate.FilterDescriptors.Count > 0)
        return true;
      if ((this.parentRow != null || this.ViewTemplate.IsSelfReference) && (this.ViewTemplate.HierarchyDataProvider != null && !this.ViewTemplate.HierarchyDataProvider.IsValid))
        return false;
      if (this.ChildRows.Count > 0 || GridTraverser.IsNewRowVisible(this.TableAddNewRow) || this.ownerTemplate.AllowSearchRow)
        return true;
      if (this.ownerTemplate.EnableFiltering)
        return this.ownerTemplate.ShowFilteringRow;
      return false;
    }

    public GridViewPinnedRowCollection PinnedRows
    {
      get
      {
        return this.pinnedRows;
      }
    }

    public GridViewSummaryRowCollection SummaryRows
    {
      get
      {
        return this.summaryRows;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewRowInfo CurrentRow
    {
      get
      {
        return this.ownerTemplate.MasterTemplate.CurrentRow;
      }
      set
      {
        this.ownerTemplate.MasterTemplate.CurrentRow = value;
      }
    }

    public GridViewChildRowCollection ChildRows
    {
      get
      {
        if (this.NeedsRefresh)
          this.Refresh();
        if (this.summaryRowsVersion != this.ownerTemplate.SummaryRowsVersion)
          this.RefreshSummaryRows();
        return this.childRows;
      }
    }

    public IHierarchicalRow FindParent(GridViewRowInfo rowInfo)
    {
      if (rowInfo == null)
        throw new ArgumentNullException(nameof (rowInfo));
      if (rowInfo.ViewInfo != this)
        throw new InvalidOperationException(string.Format("The instance of {0} is owned by another view", (object) rowInfo));
      if (this.ownerTemplate.DataView.GroupDescriptors.Count > 0)
        return this.FindGroupParent(rowInfo);
      return (IHierarchicalRow) this.parentRow;
    }

    protected virtual IHierarchicalRow FindGroupParent(GridViewRowInfo rowInfo)
    {
      if (this.NeedsRefresh)
        this.Refresh();
      if (this.data.Groups.Count > 0)
      {
        DataGroup itemGroup = (this.data.Groups[0] as DataGroup).GroupBuilder.GetItemGroup(rowInfo) as DataGroup;
        if (itemGroup != null && itemGroup.IndexOf(rowInfo) >= 0)
          return (IHierarchicalRow) itemGroup.GroupRow;
        return (IHierarchicalRow) null;
      }
      Stack<GroupCollection<GridViewRowInfo>> groupCollectionStack = new Stack<GroupCollection<GridViewRowInfo>>();
      groupCollectionStack.Push(this.data.Groups);
      while (groupCollectionStack.Count > 0)
      {
        foreach (DataGroup dataGroup in (ReadOnlyCollection<Group<GridViewRowInfo>>) groupCollectionStack.Pop())
        {
          if (dataGroup.IndexOf(rowInfo) >= 0)
            return (IHierarchicalRow) dataGroup.GroupRow;
          if (dataGroup.Groups.Count > 0)
            groupCollectionStack.Push((GroupCollection<GridViewRowInfo>) dataGroup.Groups);
        }
      }
      return (IHierarchicalRow) null;
    }

    public virtual void Refresh()
    {
      this.data = this.parentRow != null || this.ViewTemplate.IsSelfReference ? this.LoadHierarchicalData(this.parentRow, this.data) : (ICollectionView<GridViewRowInfo>) this.ownerTemplate.DataView;
      if (this.data == null)
      {
        this.childRows.Load((IList<GridViewRowInfo>) new List<GridViewRowInfo>());
      }
      else
      {
        if (!this.ViewTemplate.IsVirtualRows)
          this.ownerTemplate.DataView.LazyRefresh();
        if (this.ownerTemplate.EnableGrouping && this.ownerTemplate.DataView.GroupDescriptors.Count > 0)
        {
          DataGroupCollection groups = this.data.Groups as DataGroupCollection;
          if (groups != null)
          {
            this.childRows.Load((IReadOnlyCollection<GridViewRowInfo>) groups);
            foreach (GridViewRowInfo childRow in this.childRows)
            {
              childRow.SetParent((GridViewRowInfo) this.parentRow);
              childRow.ViewInfo = this;
            }
          }
          else
            this.childRows.Load((IList<GridViewRowInfo>) new List<GridViewRowInfo>());
        }
        else
        {
          IReadOnlyCollection<GridViewRowInfo> data = (IReadOnlyCollection<GridViewRowInfo>) this.data;
          foreach (GridViewRowInfo gridViewRowInfo in (IEnumerable<GridViewRowInfo>) data)
            gridViewRowInfo.SetParent((GridViewRowInfo) this.parentRow);
          this.childRows.Load(data);
        }
        this.version = this.ownerTemplate.DataView.Version;
      }
    }

    protected internal ICollectionView<GridViewRowInfo> LoadHierarchicalData(
      GridViewHierarchyRowInfo parent,
      ICollectionView<GridViewRowInfo> sourceView)
    {
      if (this.ViewTemplate.HierarchyDataProvider == null)
      {
        this.childRows = GridViewChildRowCollection.Empty;
        this.version = this.ownerTemplate.DataView.Version;
        return (ICollectionView<GridViewRowInfo>) null;
      }
      IList<GridViewRowInfo> childRows = this.ViewTemplate.HierarchyDataProvider.GetChildRows((GridViewRowInfo) parent, this);
      for (int index = 0; index < childRows.Count; ++index)
      {
        childRows[index].ViewInfo = this;
        childRows[index].SetParent((GridViewRowInfo) parent);
      }
      if (sourceView == null || this.ViewTemplate.IsSelfReference)
      {
        Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
        if (sourceView != null)
        {
          for (int index = 0; index < sourceView.Groups.Count; ++index)
          {
            DataGroup group = sourceView.Groups[index] as DataGroup;
            string header = group.Header;
            if (string.IsNullOrEmpty(header))
              header = index.ToString();
            dictionary.Add(header, group.IsExpanded);
          }
        }
        sourceView = (ICollectionView<GridViewRowInfo>) new SnapshotCollectionView<GridViewRowInfo>((IEnumerable<GridViewRowInfo>) childRows, this.ownerTemplate.DataView);
        if (this.ownerTemplate.GroupPredicate != null)
          ((SnapshotCollectionView<GridViewRowInfo>) sourceView).GroupPredicate = this.ownerTemplate.GroupPredicate;
        for (int index = 0; index < sourceView.Groups.Count; ++index)
        {
          DataGroup group = sourceView.Groups[index] as DataGroup;
          string header = group.Header;
          if (string.IsNullOrEmpty(header))
            header = index.ToString();
          if (dictionary.ContainsKey(header))
            group.GroupRow.IsExpanded = dictionary[header];
        }
      }
      else
        ((SnapshotCollectionView<GridViewRowInfo>) sourceView).Load((IEnumerable<GridViewRowInfo>) childRows);
      return sourceView;
    }

    public void EnsureVisible()
    {
      if (this.parentRow == null || this.parentRow.ChildRow == null)
        return;
      this.parentRow.ChildRow.EnsureVisible();
    }

    internal int Version
    {
      get
      {
        return this.version;
      }
      set
      {
        this.version = value;
      }
    }

    private bool IsVisibleInHierarchy
    {
      get
      {
        if (this.parentRow != null && this.parentRow.IsAttached)
          return this.parentRow.IsExpanded;
        return false;
      }
    }

    internal DataGroup FindGroup(DataGroup dataGroup)
    {
      if (this.data.Groups.Count == 0)
        return (DataGroup) null;
      Stack<GroupCollection<GridViewRowInfo>> groupCollectionStack = new Stack<GroupCollection<GridViewRowInfo>>();
      groupCollectionStack.Push(this.data.Groups);
      while (groupCollectionStack.Count > 0)
      {
        GroupCollection<GridViewRowInfo> groupCollection = groupCollectionStack.Pop();
        int index = groupCollection.IndexOf((Group<GridViewRowInfo>) dataGroup);
        if (index >= 0)
          return this.data.Groups[index] as DataGroup;
        foreach (Group<GridViewRowInfo> group in (ReadOnlyCollection<Group<GridViewRowInfo>>) groupCollection)
          groupCollectionStack.Push(group.Groups);
      }
      return (DataGroup) null;
    }

    internal object Evaluate(string expression, IEnumerable<GridViewRowInfo> rows)
    {
      return this.ownerTemplate.DataView.Evaluate(expression, rows);
    }

    protected virtual GridViewTableHeaderRowInfo CreateTableHeaderRow()
    {
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewTableHeaderRowInfo(this), this);
      this.ownerTemplate.OnCreateRowInfo(e);
      return e.RowInfo as GridViewTableHeaderRowInfo;
    }

    protected virtual GridViewNewRowInfo CreateAddNewRow()
    {
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewNewRowInfo(this), this);
      this.ownerTemplate.OnCreateRowInfo(e);
      return e.RowInfo as GridViewNewRowInfo;
    }

    protected virtual GridViewSearchRowInfo CreateSearchRow()
    {
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewSearchRowInfo(this), this);
      this.ownerTemplate.OnCreateRowInfo(e);
      return e.RowInfo as GridViewSearchRowInfo;
    }

    protected virtual GridViewFilteringRowInfo CreateFilteringRow()
    {
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewFilteringRowInfo(this), this);
      this.ownerTemplate.OnCreateRowInfo(e);
      return e.RowInfo as GridViewFilteringRowInfo;
    }

    private void OnTemplatePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!this.ownerTemplate.IsInitialized)
        return;
      if (e.PropertyName == "AddNewRowPosition")
      {
        this.SynchronizeNewRowPosition();
      }
      else
      {
        if (!(e.PropertyName == "SearchRowPosition"))
          return;
        this.SynchronizeSearchRowPosition();
      }
    }

    private void SynchronizeNewRowPosition()
    {
      this.TableAddNewRow.RowPosition = this.ownerTemplate.AddNewRowPosition;
      if (this.ownerTemplate.AddNewRowPosition != SystemRowPosition.Bottom)
        return;
      this.TableAddNewRow.PinPosition = PinnedRowPosition.None;
    }

    private void SynchronizeSearchRowPosition()
    {
      this.TableSearchRow.RowPosition = this.ownerTemplate.SearchRowPosition;
      if (this.ownerTemplate.SearchRowPosition == SystemRowPosition.Bottom)
      {
        this.TableSearchRow.PinPosition = PinnedRowPosition.Bottom;
      }
      else
      {
        if (this.ownerTemplate.SearchRowPosition != SystemRowPosition.Top)
          return;
        this.TableSearchRow.PinPosition = PinnedRowPosition.Top;
      }
    }

    private void RefreshSummaryRows()
    {
      int count = this.summaryRows.Count;
      for (int index = 0; index < count; ++index)
      {
        if (!this.ownerTemplate.SummaryRowsTop.Contains(this.summaryRows[index].SummaryRowItem) && !this.ownerTemplate.SummaryRowsBottom.Contains(this.summaryRows[index].SummaryRowItem))
        {
          this.summaryRows.Remove(this.summaryRows[index].SummaryRowItem);
          --count;
        }
      }
      foreach (GridViewSummaryRowItem summaryItem in (Collection<GridViewSummaryRowItem>) this.ownerTemplate.SummaryRowsTop)
      {
        if (!this.summaryRows.Contains(summaryItem))
          this.summaryRows.Add(this, summaryItem, true);
      }
      foreach (GridViewSummaryRowItem summaryItem in (Collection<GridViewSummaryRowItem>) this.ownerTemplate.SummaryRowsBottom)
      {
        if (!this.summaryRows.Contains(summaryItem))
          this.summaryRows.Add(this, summaryItem, false);
      }
      foreach (GridViewSummaryRowInfo summaryRow in this.summaryRows)
        summaryRow.HierarchyRow = this.parentRow;
      this.summaryRowsVersion = this.ownerTemplate.SummaryRowsVersion;
    }

    public GridViewChildRowCollection Rows
    {
      get
      {
        return this.ChildRows;
      }
    }
  }
}
