// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridViewInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class VirtualGridViewInfo : RadObject
  {
    public static RadProperty HeaderRowHeightProperty = RadProperty.Register(nameof (HeaderRowHeight), typeof (int), typeof (VirtualGridViewInfo), new RadPropertyMetadata((object) 30));
    public static RadProperty FilterRowHeightProperty = RadProperty.Register(nameof (FilterRowHeight), typeof (int), typeof (VirtualGridViewInfo), new RadPropertyMetadata((object) 26));
    public static RadProperty NewRowHeightProperty = RadProperty.Register(nameof (NewRowHeight), typeof (int), typeof (VirtualGridViewInfo), new RadPropertyMetadata((object) 24));
    public static RadProperty RowHeightProperty = RadProperty.Register(nameof (RowHeight), typeof (int), typeof (VirtualGridViewInfo), new RadPropertyMetadata((object) 24));
    private Dictionary<int, object> newRowValues = new Dictionary<int, object>();
    private Dictionary<int, FilterOperator> filterRowValues = new Dictionary<int, FilterOperator>();
    private Dictionary<int, string> rowErrorTexts = new Dictionary<int, string>();
    private Dictionary<int, bool> waitingRows = new Dictionary<int, bool>();
    private Dictionary<int, System.Type> columnDataTypes = new Dictionary<int, System.Type>();
    private List<int> customColumns = new List<int>();
    private VirtualGridAutoSizeColumnsMode autoSizeColumnsMode = VirtualGridAutoSizeColumnsMode.None;
    private bool isExpanded = true;
    private bool showHeaderRow = true;
    private bool showFilterRow = true;
    private bool showNewRow = true;
    private bool allowColumnSort = true;
    private bool allowColumnResize = true;
    private bool allowColumnHeaderContextMenu = true;
    private bool allowCellContextMenu = true;
    private bool allowEdit = true;
    private bool allowDelete = true;
    private bool allowCut = true;
    private bool allowCopy = true;
    private bool allowPaste = true;
    private int minRowHeight = 5;
    private int minColumnWidth = 5;
    private int expandedHeight = 250;
    private string name = string.Empty;
    private int parentRowIndex = -1;
    private Padding padding = new Padding(9);
    public const int DefaultRowHeight = 24;
    public const int DefaultHeaderRowHeight = 30;
    public const int DefaultFilterRowHeight = 26;
    public const int DefaultNewRowHeight = 24;
    public const int DefaultColumnWidth = 100;
    public const int DefaultSpacing = -1;
    private VirtualGridTableViewState rowsViewState;
    private VirtualGridTableViewState columnsViewState;
    private SortDescriptorCollection sortDescriptors;
    private FilterDescriptorCollection filterDescriptors;
    private RadVirtualGridElement gridElement;
    private ScrollState horizontalScrollState;
    private ScrollState verticalScrollState;
    private bool isWaiting;
    private bool allowRowResize;
    private bool enableAlternatingRowColor;
    private bool allowMultiColumnSorting;
    private object tag;
    private VirtualGridViewInfo parentViewInfo;
    private Dictionary<int, VirtualGridViewInfo> childViewInfos;

    public VirtualGridViewInfo(VirtualGridViewInfo parentViewInfo, int parentRowIndex)
    {
      this.gridElement = parentViewInfo.gridElement;
      this.parentRowIndex = parentRowIndex;
      this.parentViewInfo = parentViewInfo;
      this.InitializeViewInfo();
    }

    public VirtualGridViewInfo(RadVirtualGridElement gridElement)
    {
      this.gridElement = gridElement;
      this.InitializeViewInfo();
    }

    protected virtual void InitializeViewInfo()
    {
      if (this.parentViewInfo != null)
      {
        this.rowsViewState = new VirtualGridTableViewState(this.parentViewInfo.RowCount, 24, this.parentViewInfo.RowSpacing, true);
        this.columnsViewState = new VirtualGridTableViewState(this.parentViewInfo.ColumnCount, 100, this.parentViewInfo.CellSpacing, false);
        this.showFilterRow = this.parentViewInfo.showFilterRow;
        this.showHeaderRow = this.parentViewInfo.showHeaderRow;
        this.allowColumnSort = this.parentViewInfo.allowColumnSort;
        this.showNewRow = this.parentViewInfo.showNewRow;
        this.allowColumnHeaderContextMenu = this.parentViewInfo.allowColumnHeaderContextMenu;
        this.allowCellContextMenu = this.parentViewInfo.allowCellContextMenu;
        this.allowEdit = this.parentViewInfo.allowEdit;
        this.allowCut = this.parentViewInfo.allowCut;
        this.allowCopy = this.parentViewInfo.allowCopy;
        this.allowPaste = this.parentViewInfo.allowPaste;
        this.enableAlternatingRowColor = this.parentViewInfo.enableAlternatingRowColor;
        this.allowColumnResize = this.parentViewInfo.allowColumnResize;
        this.allowRowResize = this.parentViewInfo.allowRowResize;
      }
      else
      {
        this.rowsViewState = new VirtualGridTableViewState(0, 24, -1, true);
        this.columnsViewState = new VirtualGridTableViewState(0, 100, -1, false);
      }
      this.rowsViewState.PropertyChanged += new PropertyChangedEventHandler(this.rowsViewState_PropertyChanged);
      this.columnsViewState.PropertyChanged += new PropertyChangedEventHandler(this.columnsViewState_PropertyChanged);
      this.rowsViewState.PageIndexChanging += new VirtualGridPageChangingEventHandler(this.rowsViewState_PageIndexChanging);
      this.rowsViewState.PageIndexChanged += new EventHandler(this.rowsViewState_PageIndexChanged);
      this.childViewInfos = new Dictionary<int, VirtualGridViewInfo>();
      this.sortDescriptors = new SortDescriptorCollection();
      this.sortDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.sortDescriptors_CollectionChanged);
      this.filterDescriptors = new FilterDescriptorCollection();
      this.filterDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.filterDescriptors_CollectionChanged);
    }

    [DefaultValue(true)]
    public bool AllowEdit
    {
      get
      {
        return this.allowEdit;
      }
      set
      {
        if (this.allowEdit == value)
          return;
        this.allowEdit = value;
        this.OnNotifyPropertyChanged(nameof (AllowEdit));
      }
    }

    [DefaultValue(true)]
    public bool AllowDelete
    {
      get
      {
        return this.allowDelete;
      }
      set
      {
        if (this.allowDelete == value)
          return;
        this.allowDelete = value;
        this.OnNotifyPropertyChanged(nameof (AllowDelete));
      }
    }

    [DefaultValue(true)]
    public bool AllowCut
    {
      get
      {
        return this.allowCut;
      }
      set
      {
        if (this.allowCut == value)
          return;
        this.allowCut = value;
        this.OnNotifyPropertyChanged(nameof (AllowCut));
      }
    }

    [DefaultValue(true)]
    public bool AllowCopy
    {
      get
      {
        return this.allowCopy;
      }
      set
      {
        if (this.allowCopy == value)
          return;
        this.allowCopy = value;
        this.OnNotifyPropertyChanged(nameof (AllowCopy));
      }
    }

    [DefaultValue(true)]
    public bool AllowPaste
    {
      get
      {
        return this.allowPaste;
      }
      set
      {
        if (this.allowPaste == value)
          return;
        this.allowPaste = value;
        this.OnNotifyPropertyChanged(nameof (AllowPaste));
      }
    }

    [DefaultValue(false)]
    public bool EnableAlternatingRowColor
    {
      get
      {
        return this.enableAlternatingRowColor;
      }
      set
      {
        if (this.enableAlternatingRowColor == value)
          return;
        this.enableAlternatingRowColor = value;
        this.OnNotifyPropertyChanged(nameof (EnableAlternatingRowColor));
      }
    }

    [DefaultValue(true)]
    public bool AllowColumnHeaderContextMenu
    {
      get
      {
        return this.allowColumnHeaderContextMenu;
      }
      set
      {
        if (this.allowColumnHeaderContextMenu == value)
          return;
        this.allowColumnHeaderContextMenu = value;
        this.OnNotifyPropertyChanged(nameof (AllowColumnHeaderContextMenu));
      }
    }

    [DefaultValue(true)]
    public bool AllowCellContextMenu
    {
      get
      {
        return this.allowCellContextMenu;
      }
      set
      {
        if (this.allowCellContextMenu == value)
          return;
        this.allowCellContextMenu = value;
        this.OnNotifyPropertyChanged(nameof (AllowCellContextMenu));
      }
    }

    [DefaultValue(true)]
    public bool AllowColumnResize
    {
      get
      {
        return this.allowColumnResize;
      }
      set
      {
        if (this.allowColumnResize == value)
          return;
        this.allowColumnResize = value;
        this.OnNotifyPropertyChanged(nameof (AllowColumnResize));
      }
    }

    [DefaultValue(false)]
    public bool AllowRowResize
    {
      get
      {
        return this.allowRowResize;
      }
      set
      {
        if (this.allowRowResize == value)
          return;
        this.allowRowResize = value;
        this.OnNotifyPropertyChanged(nameof (AllowRowResize));
      }
    }

    [DefaultValue(true)]
    public bool AllowColumnSort
    {
      get
      {
        return this.allowColumnSort;
      }
      set
      {
        if (this.allowColumnSort == value)
          return;
        this.allowColumnSort = value;
        this.OnNotifyPropertyChanged(nameof (AllowColumnSort));
      }
    }

    [DefaultValue(false)]
    public bool AllowMultiColumnSorting
    {
      get
      {
        return this.allowMultiColumnSorting;
      }
      set
      {
        if (this.allowMultiColumnSorting == value)
          return;
        this.allowMultiColumnSorting = value;
        this.OnNotifyPropertyChanged(nameof (AllowMultiColumnSorting));
      }
    }

    [DefaultValue(5)]
    public int MinRowHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.minRowHeight, this.GridElement.DpiScaleFactor);
      }
      set
      {
        if (this.minRowHeight == value)
          return;
        this.minRowHeight = value;
        this.OnNotifyPropertyChanged(nameof (MinRowHeight));
        this.RowHeight = Math.Max(this.RowHeight, this.MinRowHeight);
        this.rowsViewState.BeginUpdate();
        foreach (KeyValuePair<int, int> itemSize in this.rowsViewState.GetItemSizes())
        {
          if (itemSize.Value < this.MinRowHeight)
            this.rowsViewState.SetItemSize(itemSize.Key, this.MinRowHeight);
        }
        this.rowsViewState.EndUpdate();
      }
    }

    [DefaultValue(5)]
    public int MinColumnWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.minColumnWidth, this.GridElement.DpiScaleFactor);
      }
      set
      {
        if (this.minColumnWidth == value)
          return;
        this.minColumnWidth = value;
        this.OnNotifyPropertyChanged(nameof (MinColumnWidth));
        this.ColumnWidth = Math.Max(this.ColumnWidth, this.MinColumnWidth);
        this.columnsViewState.BeginUpdate();
        foreach (KeyValuePair<int, int> itemSize in this.columnsViewState.GetItemSizes())
        {
          if (itemSize.Value < this.MinColumnWidth)
            this.columnsViewState.SetItemSize(itemSize.Key, this.MinColumnWidth);
        }
        this.columnsViewState.EndUpdate();
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.horizontalScrollState;
      }
      set
      {
        if (this.horizontalScrollState == value)
          return;
        this.horizontalScrollState = value;
        this.OnNotifyPropertyChanged(nameof (HorizontalScrollState));
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.verticalScrollState;
      }
      set
      {
        if (this.verticalScrollState == value)
          return;
        this.verticalScrollState = value;
        this.OnNotifyPropertyChanged(nameof (VerticalScrollState));
      }
    }

    [DefaultValue(VirtualGridAutoSizeColumnsMode.None)]
    [Description("Gets or sets a value indicating how column widths are determined.")]
    public VirtualGridAutoSizeColumnsMode AutoSizeColumnsMode
    {
      get
      {
        return this.autoSizeColumnsMode;
      }
      set
      {
        if (this.autoSizeColumnsMode == value)
          return;
        this.autoSizeColumnsMode = value;
        this.OnNotifyPropertyChanged(nameof (AutoSizeColumnsMode));
      }
    }

    [DefaultValue(typeof (Padding), "9, 9, 9, 9")]
    public Padding Padding
    {
      get
      {
        return TelerikDpiHelper.ScalePadding(this.padding, this.GridElement.DpiScaleFactor);
      }
      set
      {
        if (!(this.padding != value))
          return;
        this.padding = value;
        this.OnNotifyPropertyChanged(nameof (Padding));
      }
    }

    [DefaultValue(true)]
    public bool ShowHeaderRow
    {
      get
      {
        return this.showHeaderRow;
      }
      set
      {
        if (this.showHeaderRow == value)
          return;
        this.showHeaderRow = value;
        this.OnNotifyPropertyChanged(nameof (ShowHeaderRow));
      }
    }

    [DefaultValue(true)]
    public bool ShowFilterRow
    {
      get
      {
        return this.showFilterRow;
      }
      set
      {
        if (this.showFilterRow == value)
          return;
        this.showFilterRow = value;
        this.OnNotifyPropertyChanged(nameof (ShowFilterRow));
      }
    }

    [DefaultValue(true)]
    public bool ShowNewRow
    {
      get
      {
        return this.showNewRow;
      }
      set
      {
        if (this.showNewRow == value)
          return;
        this.showNewRow = value;
        this.OnNotifyPropertyChanged(nameof (ShowNewRow));
      }
    }

    [DefaultValue(26)]
    public int FilterRowHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(VirtualGridViewInfo.FilterRowHeightProperty), this.GridElement.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridViewInfo.FilterRowHeightProperty, (object) value);
      }
    }

    [DefaultValue(24)]
    public int NewRowHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(VirtualGridViewInfo.NewRowHeightProperty), this.GridElement.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridViewInfo.NewRowHeightProperty, (object) value);
      }
    }

    [DefaultValue(30)]
    public int HeaderRowHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(VirtualGridViewInfo.HeaderRowHeightProperty), this.GridElement.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridViewInfo.HeaderRowHeightProperty, (object) value);
      }
    }

    [DefaultValue(24)]
    public int RowHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(VirtualGridViewInfo.RowHeightProperty), this.GridElement.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridViewInfo.RowHeightProperty, (object) Math.Max(value, this.MinRowHeight));
      }
    }

    [DefaultValue(-1)]
    public int RowSpacing
    {
      get
      {
        return this.rowsViewState.ItemSpacing;
      }
      set
      {
        this.rowsViewState.ItemSpacing = value;
      }
    }

    [DefaultValue(false)]
    public bool IsWaiting
    {
      get
      {
        return this.isWaiting;
      }
      set
      {
        if (this.isWaiting == value)
          return;
        this.isWaiting = value;
        this.OnNotifyPropertyChanged(nameof (IsWaiting));
      }
    }

    public Dictionary<int, FilterOperator> FilterRowValues
    {
      get
      {
        return this.filterRowValues;
      }
    }

    public Dictionary<int, object> NewRowValues
    {
      get
      {
        return this.newRowValues;
      }
    }

    public RadVirtualGridElement GridElement
    {
      get
      {
        return this.gridElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.sortDescriptors;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.filterDescriptors;
      }
    }

    public int ParentRowIndex
    {
      get
      {
        return this.parentRowIndex;
      }
    }

    public bool IsExpanded
    {
      get
      {
        return this.isExpanded;
      }
      private set
      {
        if (this.isExpanded == value)
          return;
        this.isExpanded = value;
        this.OnNotifyPropertyChanged(nameof (IsExpanded));
      }
    }

    [DefaultValue("")]
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        if (!(this.name != value))
          return;
        this.name = value;
        this.OnNotifyPropertyChanged(nameof (Name));
      }
    }

    [DefaultValue(null)]
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
        this.OnNotifyPropertyChanged(nameof (Tag));
      }
    }

    public int TotalPages
    {
      get
      {
        return this.rowsViewState.TotalPages;
      }
    }

    [DefaultValue(false)]
    public bool EnablePaging
    {
      get
      {
        return this.rowsViewState.EnablePaging;
      }
      set
      {
        this.rowsViewState.EnablePaging = value;
      }
    }

    [DefaultValue(100)]
    public int PageSize
    {
      get
      {
        return this.rowsViewState.PageSize;
      }
      set
      {
        this.rowsViewState.PageSize = value;
      }
    }

    [DefaultValue(0)]
    public int PageIndex
    {
      get
      {
        return this.rowsViewState.PageIndex;
      }
      set
      {
        this.rowsViewState.PageIndex = value;
      }
    }

    public VirtualGridViewInfo ParentViewInfo
    {
      get
      {
        return this.parentViewInfo;
      }
    }

    public int HierarchyLevel
    {
      get
      {
        if (this.parentViewInfo == null)
          return 0;
        return this.parentViewInfo.HierarchyLevel + 1;
      }
    }

    [DefaultValue(250)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ExpandedHeight
    {
      get
      {
        return this.expandedHeight;
      }
      set
      {
        if (this.expandedHeight == value)
          return;
        this.expandedHeight = value;
        this.OnNotifyPropertyChanged(nameof (ExpandedHeight));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public VirtualGridTableViewState RowsViewState
    {
      get
      {
        return this.rowsViewState;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public VirtualGridTableViewState ColumnsViewState
    {
      get
      {
        return this.columnsViewState;
      }
    }

    [DefaultValue(0)]
    public int RowCount
    {
      get
      {
        return this.rowsViewState.ItemCount;
      }
      set
      {
        this.rowsViewState.ItemCount = value;
      }
    }

    [DefaultValue(0)]
    public int ColumnCount
    {
      get
      {
        return this.columnsViewState.ItemCount;
      }
      set
      {
        this.columnsViewState.ItemCount = value;
      }
    }

    [DefaultValue(100)]
    public int ColumnWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.columnsViewState.DefaultItemSize, this.GridElement.DpiScaleFactor);
      }
      set
      {
        this.columnsViewState.DefaultItemSize = Math.Max(value, this.MinColumnWidth);
      }
    }

    [DefaultValue(-1)]
    public int CellSpacing
    {
      get
      {
        return this.columnsViewState.ItemSpacing;
      }
      set
      {
        this.columnsViewState.ItemSpacing = value;
      }
    }

    public bool ExpandRow(int rowIndex)
    {
      if (this.childViewInfos.ContainsKey(rowIndex) && this.childViewInfos[rowIndex].IsExpanded)
        return false;
      VirtualGridViewInfo childViewInfo = this.GetChildViewInfo(rowIndex, true);
      VirtualGridRowExpandingEventArgs args = new VirtualGridRowExpandingEventArgs(rowIndex, childViewInfo, this);
      this.GridElement.OnRowExpanding(args);
      if (args.Cancel)
        return false;
      if (!this.childViewInfos.ContainsKey(rowIndex))
        this.childViewInfos.Add(rowIndex, new VirtualGridViewInfo(this, rowIndex));
      this.childViewInfos[rowIndex].IsExpanded = true;
      int size = this.GridElement.UseScrollbarsInHierarchy ? this.childViewInfos[rowIndex].ExpandedHeight : this.childViewInfos[rowIndex].GetTotalRowHeight();
      this.rowsViewState.SetExpandedSize(rowIndex, size);
      this.childViewInfos[rowIndex].ExpandedHeight = size;
      this.OnNotifyPropertyChanged("ExpandedRows[]");
      this.GridElement.OnRowExpanded(new VirtualGridRowExpandedEventArgs(rowIndex, childViewInfo, this));
      return true;
    }

    public bool CollapseRow(int rowIndex)
    {
      VirtualGridViewInfo childViewInfo = this.GetChildViewInfo(rowIndex, true);
      VirtualGridRowExpandingEventArgs args = new VirtualGridRowExpandingEventArgs(rowIndex, childViewInfo, this);
      this.GridElement.OnRowCollapsing(args);
      if (args.Cancel || !this.childViewInfos.ContainsKey(rowIndex) || !this.childViewInfos[rowIndex].IsExpanded)
        return false;
      this.childViewInfos[rowIndex].IsExpanded = false;
      this.rowsViewState.ResetExpandedSize(rowIndex);
      this.OnNotifyPropertyChanged("ExpandedRows[]");
      this.GridElement.OnRowCollapsed(new VirtualGridRowExpandedEventArgs(rowIndex, childViewInfo, this));
      return true;
    }

    public bool IsRowExpanded(int rowIndex)
    {
      if (this.childViewInfos.ContainsKey(rowIndex))
        return this.childViewInfos[rowIndex].IsExpanded;
      return false;
    }

    public void SetRowHeight(int rowIndex, int height)
    {
      if (rowIndex >= this.RowsViewState.ItemCount)
        return;
      height = Math.Max(this.MinRowHeight, height);
      if (!this.GridElement.OnRowHeightChanging(new VirtualGridRowHeightChangingEventArgs(rowIndex, this.GetRowHeight(rowIndex), height, this)))
        return;
      this.RowsViewState.SetItemSize(rowIndex, height);
      this.GridElement.OnRowHeightChanged(new VirtualGridRowEventArgs(rowIndex, this));
    }

    public int GetRowHeight(int rowIndex)
    {
      return this.RowsViewState.GetItemSize(rowIndex, false);
    }

    public int GetColumnWidth(int columnIndex)
    {
      return this.ColumnsViewState.GetItemSize(columnIndex, false);
    }

    public void SetColumnWidth(int columnIndex, int width)
    {
      if (columnIndex >= this.ColumnsViewState.ItemCount)
        return;
      width = Math.Max(this.MinColumnWidth, width);
      if (!this.GridElement.OnColumnWidthChanging(new VirtualGridColumnWidthChangingEventArgs(columnIndex, this.GetColumnWidth(columnIndex), width, this)))
        return;
      this.ColumnsViewState.SetItemSize(columnIndex, width);
      this.GridElement.OnColumnWidthChanged(new VirtualGridColumnEventArgs(columnIndex, this));
    }

    public void SetRowPinPosition(int rowIndex, PinnedRowPosition position)
    {
      this.RowsViewState.SetPinPosition(rowIndex, position);
    }

    public PinnedRowPosition GetRowPinPosition(int rowIndex)
    {
      return this.RowsViewState.GetPinPosition(rowIndex);
    }

    public void SetColumnPinPosition(int columnIndex, PinnedColumnPosition pinPosition)
    {
      int num;
      switch (pinPosition)
      {
        case PinnedColumnPosition.Left:
          num = 0;
          break;
        case PinnedColumnPosition.Right:
          num = 1;
          break;
        default:
          num = 2;
          break;
      }
      PinnedRowPosition position = (PinnedRowPosition) num;
      this.ColumnsViewState.SetPinPosition(columnIndex, position);
    }

    public PinnedColumnPosition GetColumnPinPosition(int columnIndex)
    {
      int num;
      switch (this.ColumnsViewState.GetPinPosition(columnIndex))
      {
        case PinnedRowPosition.Top:
          num = 0;
          break;
        case PinnedRowPosition.Bottom:
          num = 1;
          break;
        default:
          num = 2;
          break;
      }
      return (PinnedColumnPosition) num;
    }

    public bool IsRowPinned(int rowIndex)
    {
      return this.RowsViewState.IsPinned(rowIndex);
    }

    public bool IsColumnPinned(int columnIndex)
    {
      return this.ColumnsViewState.IsPinned(columnIndex);
    }

    public VirtualGridViewInfo GetChildViewInfo(int rowIndex)
    {
      if (!this.childViewInfos.ContainsKey(rowIndex))
        return (VirtualGridViewInfo) null;
      return this.childViewInfos[rowIndex];
    }

    public VirtualGridViewInfo GetChildViewInfo(int rowIndex, bool forceCreate)
    {
      if (!this.childViewInfos.ContainsKey(rowIndex))
        this.childViewInfos.Add(rowIndex, this.CreateChildViewInfo(rowIndex));
      return this.childViewInfos[rowIndex];
    }

    protected virtual VirtualGridViewInfo CreateChildViewInfo(int rowIndex)
    {
      return new VirtualGridViewInfo(this, rowIndex)
      {
        IsExpanded = false
      };
    }

    public void MoveToPage(int pageIndex)
    {
      this.rowsViewState.PageIndex = pageIndex;
    }

    public void MoveToFirstPage()
    {
      this.rowsViewState.PageIndex = 0;
    }

    public void MoveToPreviousPage()
    {
      --this.rowsViewState.PageIndex;
    }

    public void MoveToNextPage()
    {
      ++this.rowsViewState.PageIndex;
    }

    public void MoveToLastPage()
    {
      this.rowsViewState.PageIndex = this.TotalPages - 1;
    }

    public void SetRowErrorText(int rowIndex, string errorText)
    {
      if (string.IsNullOrEmpty(errorText))
      {
        this.ClearRowErrorText(rowIndex);
      }
      else
      {
        this.rowErrorTexts[rowIndex] = errorText;
        this.OnNotifyPropertyChanged("RowErrorTexts[]");
      }
    }

    public bool RowHasError(int rowIndex)
    {
      return this.rowErrorTexts.ContainsKey(rowIndex);
    }

    public string GetRowErrorText(int rowIndex)
    {
      if (this.rowErrorTexts.ContainsKey(rowIndex))
        return this.rowErrorTexts[rowIndex];
      return (string) null;
    }

    public void ClearRowErrorText(int rowIndex)
    {
      if (!this.rowErrorTexts.Remove(rowIndex))
        return;
      this.OnNotifyPropertyChanged("RowErrorTexts[]");
    }

    public void StartRowWaiting(int rowIndex)
    {
      if (this.waitingRows.ContainsKey(rowIndex))
        return;
      this.waitingRows.Add(rowIndex, true);
      this.OnNotifyPropertyChanged("WaitingRows[]");
    }

    public void StopRowWaiting(int rowIndex)
    {
      if (!this.waitingRows.ContainsKey(rowIndex))
        return;
      this.waitingRows.Remove(rowIndex);
      this.OnNotifyPropertyChanged("WaitingRows[]");
    }

    public bool IsRowWaiting(int rowIndex)
    {
      return this.waitingRows.ContainsKey(rowIndex);
    }

    public void SetColumnDataType(int columnIndex, System.Type dataType)
    {
      this.ColumnDataTypes[columnIndex] = dataType;
      this.OnNotifyPropertyChanged("ColumnDataTypes[]");
    }

    public void SetColumnDataType(params System.Type[] dataTypes)
    {
      for (int columnIndex = 0; columnIndex < dataTypes.Length; ++columnIndex)
        this.SetColumnDataType(columnIndex, dataTypes[columnIndex]);
      this.OnNotifyPropertyChanged("ColumnDataTypes[]");
    }

    public System.Type GetColumnDataType(int columnIndex)
    {
      if (this.ColumnDataTypes.ContainsKey(columnIndex))
        return this.ColumnDataTypes[columnIndex];
      return (System.Type) null;
    }

    public bool RegisterCustomColumn(int columnIndex)
    {
      int num = this.CustomColumns.BinarySearch(columnIndex);
      if (num >= 0)
        return false;
      this.CustomColumns.Insert(~num, columnIndex);
      foreach (VirtualGridRowElement descendant in this.gridElement.TableElement.GetDescendants((Predicate<RadElement>) (x => x is VirtualGridRowElement), TreeTraversalMode.BreadthFirst))
        descendant.CellContainer.Children.Clear();
      return true;
    }

    public bool UnregisterCustomColumn(int columnIndex)
    {
      int index = this.CustomColumns.BinarySearch(columnIndex);
      if (index < 0)
        return false;
      this.CustomColumns.RemoveAt(index);
      foreach (VirtualGridRowElement descendant in this.gridElement.TableElement.GetDescendants((Predicate<RadElement>) (x => x is VirtualGridRowElement), TreeTraversalMode.BreadthFirst))
        descendant.CellContainer.Children.Clear();
      return true;
    }

    public bool IsCustomColumn(int columnIndex)
    {
      return this.CustomColumns.BinarySearch(columnIndex) >= 0;
    }

    public void ResetViewState()
    {
      int num1 = (int) this.ResetValue(VirtualGridViewInfo.HeaderRowHeightProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(VirtualGridViewInfo.FilterRowHeightProperty, ValueResetFlags.Local);
      int num3 = (int) this.ResetValue(VirtualGridViewInfo.NewRowHeightProperty, ValueResetFlags.Local);
      this.RowsViewState.ItemSizes.Clear();
      this.RowsViewState.ExpandedSizes.Clear();
      this.RowsViewState.TopPinnedItemsList.Clear();
      this.RowsViewState.BottomPinnedItemsList.Clear();
      this.ColumnsViewState.ItemSizes.Clear();
      this.ColumnsViewState.ExpandedSizes.Clear();
      this.ColumnsViewState.TopPinnedItemsList.Clear();
      this.ColumnsViewState.BottomPinnedItemsList.Clear();
      foreach (KeyValuePair<int, VirtualGridViewInfo> childViewInfo in this.childViewInfos)
        childViewInfo.Value.ResetViewState();
    }

    private void filterDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.GridElement.OnFilterDescriptorsChanged(this);
    }

    private void sortDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.GridElement.OnSortDescriptorsChanged(this);
    }

    private void columnsViewState_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "ItemCount")
        this.OnNotifyPropertyChanged("ColumnCount");
      else if (e.PropertyName == "DefaultItemSize")
        this.OnNotifyPropertyChanged("ColumnWidth");
      else if (e.PropertyName == "ItemSpacing")
        this.OnNotifyPropertyChanged("CellSpacing");
      else if (e.PropertyName == "ItemSizes[]")
        this.OnNotifyPropertyChanged("ColumnSizes[]");
      else if (e.PropertyName == "TopPinnedItems[]")
      {
        this.OnNotifyPropertyChanged("LeftPinnedColumns[]");
      }
      else
      {
        if (!(e.PropertyName == "BottomPinnedItems[]"))
          return;
        this.OnNotifyPropertyChanged("RightPinnedColumns[]");
      }
    }

    private void rowsViewState_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "EnablePaging" || e.PropertyName == "PageSize" || e.PropertyName == "PageIndex")
        this.OnNotifyPropertyChanged(e.PropertyName);
      else if (e.PropertyName == "ItemCount")
        this.OnNotifyPropertyChanged("RowCount");
      else if (e.PropertyName == "DefaultItemSize")
        this.OnNotifyPropertyChanged("RowHeight");
      else if (e.PropertyName == "ItemSpacing")
        this.OnNotifyPropertyChanged("RowSpacing");
      else if (e.PropertyName == "ItemSizes[]")
        this.OnNotifyPropertyChanged("RowSizes[]");
      else if (e.PropertyName == "TopPinnedItems[]")
      {
        this.OnNotifyPropertyChanged("TopPinnedRows[]");
      }
      else
      {
        if (!(e.PropertyName == "BottomPinnedItems[]"))
          return;
        this.OnNotifyPropertyChanged("BottomPinnedRows[]");
      }
    }

    private void rowsViewState_PageIndexChanged(object sender, EventArgs e)
    {
      this.GridElement.OnPageIndexChanged(new VirtualGridEventArgs(this));
    }

    private void rowsViewState_PageIndexChanging(object sender, VirtualGridPageChangingEventArgs e)
    {
      VirtualGridPageChangingEventArgs args = new VirtualGridPageChangingEventArgs(e.OldIndex, e.NewIndex, this);
      this.GridElement.OnPageIndexChanging(args);
      e.Cancel = args.Cancel;
    }

    public void BindProperties(VirtualGridTableElement source)
    {
      int num1 = (int) this.BindProperty(VirtualGridViewInfo.RowHeightProperty, (RadObject) source, VirtualGridTableElement.RowHeightProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.BindProperty(VirtualGridViewInfo.FilterRowHeightProperty, (RadObject) source, VirtualGridTableElement.FilterRowHeightProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.BindProperty(VirtualGridViewInfo.NewRowHeightProperty, (RadObject) source, VirtualGridTableElement.NewRowHeightProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.BindProperty(VirtualGridViewInfo.HeaderRowHeightProperty, (RadObject) source, VirtualGridTableElement.HeaderRowHeightProperty, PropertyBindingOptions.TwoWay);
    }

    public void UnbindProperties(VirtualGridTableElement source)
    {
      int num1 = (int) this.UnbindProperty(VirtualGridViewInfo.RowHeightProperty);
      int num2 = (int) this.UnbindProperty(VirtualGridViewInfo.FilterRowHeightProperty);
      int num3 = (int) this.UnbindProperty(VirtualGridViewInfo.NewRowHeightProperty);
      int num4 = (int) this.UnbindProperty(VirtualGridViewInfo.HeaderRowHeightProperty);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == VirtualGridViewInfo.FilterRowHeightProperty)
        this.OnNotifyPropertyChanged("FilterRowHeight");
      else if (e.Property == VirtualGridViewInfo.NewRowHeightProperty)
        this.OnNotifyPropertyChanged("NewRowHeight");
      else if (e.Property == VirtualGridViewInfo.HeaderRowHeightProperty)
      {
        this.OnNotifyPropertyChanged("HeaderRowHeight");
      }
      else
      {
        if (e.Property != VirtualGridViewInfo.RowHeightProperty)
          return;
        this.RowsViewState.DefaultItemSize = (int) e.NewValue;
      }
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      this.HandlePropertyChange(e.PropertyName);
      base.OnNotifyPropertyChanged(e);
    }

    protected void HandlePropertyChange(string propertyName)
    {
      if (propertyName == "HeaderRowHeight" || propertyName == "FilterRowHeight" || (propertyName == "NewRowHeight" || propertyName == "RowSizes[]") || propertyName == "RowCount")
      {
        if (this.GridElement.UseScrollbarsInHierarchy)
          return;
        this.ExpandedHeight = this.GetTotalRowHeight();
      }
      else
      {
        if (!(propertyName == "ExpandedHeight") || this.GridElement.UseScrollbarsInHierarchy || this.parentViewInfo == null)
          return;
        this.parentViewInfo.OnChildViewInfoHeightChanged(this);
      }
    }

    private void OnChildViewInfoHeightChanged(VirtualGridViewInfo childView)
    {
      if (!this.childViewInfos.ContainsKey(childView.ParentRowIndex) || this.childViewInfos[childView.ParentRowIndex] != childView || !childView.IsExpanded)
        return;
      this.rowsViewState.SetExpandedSize(childView.ParentRowIndex, childView.ExpandedHeight);
    }

    public int GetTotalRowHeight()
    {
      int totalItemSize = this.RowsViewState.GetTotalItemSize();
      if (this.ShowHeaderRow)
        totalItemSize += this.HeaderRowHeight;
      if (this.ShowFilterRow)
        totalItemSize += this.FilterRowHeight;
      if (this.ShowNewRow)
        totalItemSize += this.NewRowHeight;
      int num = totalItemSize + this.Padding.Vertical;
      if (this.GridElement.TableElement.HScrollBar.Visibility != ElementVisibility.Collapsed)
        num += (int) Math.Max((float) RadScrollBarElement.HorizontalScrollBarHeight, this.GridElement.TableElement.HScrollBar.DesiredSize.Height);
      return num;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Dictionary<int, System.Type> ColumnDataTypes
    {
      get
      {
        return this.columnDataTypes;
      }
      set
      {
        this.columnDataTypes = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<int> CustomColumns
    {
      get
      {
        return this.customColumns;
      }
      set
      {
        this.customColumns = value;
      }
    }
  }
}
