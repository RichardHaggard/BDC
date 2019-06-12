// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewSpreadExport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  public class GridViewSpreadExport
  {
    private HiddenOption hiddenColumnOption = HiddenOption.ExportAsHidden;
    private HiddenOption hiddenRowOption = HiddenOption.ExportAsHidden;
    private int sheetMaxRowsNumber = 65536;
    private bool exportGroupedColumns = true;
    private bool exportViewDefinition = true;
    private int imageOffset = 2;
    private bool isFirstViewDefinitionRow = true;
    private ISpreadExportRenderer spreadExportRenderer;
    private PagingExportOption pagingExportOption;
    private SpreadExportFormat exportFormat;
    private SummariesOption summariesExportOption;
    private ChildViewExportMode childViewExportMode;
    private bool exportVisualSettings;
    private bool exportHierarchy;
    private bool applicationDoEvents;
    private RowElementProvider rowProvider;
    private CellElementProvider cellProvider;
    private BackgroundWorker worker;
    private bool freezeHeaderRow;
    private bool freezePinnedRows;
    private bool freezePinnedColumns;
    private int pinnedRowsCount;
    private int pinnedColumnsCount;
    private List<int> pinnedColumnsIndicesOrder;
    private Stack<GridViewSpreadExport.RowGroup> hierarchyRowGroups;
    private Stack<GridViewSpreadExport.RowGroup> readyToExportRowGroups;
    private int lastRowHierarchyLevel;
    private Stack<GridViewHierarchyRowInfo> hierarchyRowsStack;
    private int[] rowColumns;
    private List<Point> rowSpanCells;
    private GridViewSpreadExport.GroupNode tree;
    private List<RowDefinition> columnGroupRowDefinitions;

    public GridViewSpreadExport(RadGridView radGridView)
    {
      this.RadGridViewToExport = radGridView;
    }

    public GridViewSpreadExport(RadGridView radGridView, SpreadExportFormat spreadExportFormat)
      : this(radGridView)
    {
      this.ExportFormat = spreadExportFormat;
    }

    public bool ExportHierarchy
    {
      get
      {
        return this.exportHierarchy;
      }
      set
      {
        this.exportHierarchy = value;
      }
    }

    public bool ExportVisualSettings
    {
      get
      {
        return this.exportVisualSettings;
      }
      set
      {
        this.exportVisualSettings = value;
      }
    }

    public string SheetName { get; set; }

    [DefaultValue(ExcelMaxRows._65536)]
    [CLSCompliant(false)]
    public ExcelMaxRows SheetMaxRows
    {
      get
      {
        return (ExcelMaxRows) this.sheetMaxRowsNumber;
      }
      set
      {
        switch (value)
        {
          case ExcelMaxRows._65536:
            this.sheetMaxRowsNumber = 65536;
            break;
          case ExcelMaxRows._1048576:
            this.sheetMaxRowsNumber = 1048576;
            break;
        }
      }
    }

    public SummariesOption SummariesExportOption
    {
      get
      {
        return this.summariesExportOption;
      }
      set
      {
        this.summariesExportOption = value;
      }
    }

    public HiddenOption HiddenColumnOption
    {
      get
      {
        return this.hiddenColumnOption;
      }
      set
      {
        this.hiddenColumnOption = value;
      }
    }

    public HiddenOption HiddenRowOption
    {
      get
      {
        return this.hiddenRowOption;
      }
      set
      {
        this.hiddenRowOption = value;
      }
    }

    public PagingExportOption PagingExportOption
    {
      get
      {
        return this.pagingExportOption;
      }
      set
      {
        this.pagingExportOption = value;
      }
    }

    public ChildViewExportMode ChildViewExportMode
    {
      get
      {
        return this.childViewExportMode;
      }
      set
      {
        this.childViewExportMode = value;
      }
    }

    public RadGridView RadGridViewToExport { get; set; }

    public SpreadExportFormat ExportFormat
    {
      get
      {
        return this.exportFormat;
      }
      set
      {
        this.exportFormat = value;
      }
    }

    [DefaultValue(FileExportMode.NewSheetInExistingFile)]
    public FileExportMode FileExportMode { get; set; }

    public bool ExportGroupedColumns
    {
      get
      {
        return this.exportGroupedColumns;
      }
      set
      {
        this.exportGroupedColumns = value;
      }
    }

    public bool FreezeHeaderRow
    {
      get
      {
        return this.freezeHeaderRow;
      }
      set
      {
        this.freezeHeaderRow = value;
      }
    }

    public bool FreezePinnedRows
    {
      get
      {
        return this.freezePinnedRows;
      }
      set
      {
        this.freezePinnedRows = value;
      }
    }

    public bool FreezePinnedColumns
    {
      get
      {
        return this.freezePinnedColumns;
      }
      set
      {
        this.freezePinnedColumns = value;
      }
    }

    public bool ExportChildRowsGrouped { get; set; }

    [DefaultValue(true)]
    public bool ExportViewDefinition
    {
      get
      {
        if (this.ExportFormat == SpreadExportFormat.Xlsx)
          return this.exportViewDefinition;
        return false;
      }
      set
      {
        this.exportViewDefinition = value;
      }
    }

    public event ChildViewExportingEventHandler ChildViewExporting;

    protected virtual void OnChildViewExporting(ChildViewExportingEventArgs e)
    {
      if (this.ChildViewExporting == null)
        return;
      this.ChildViewExporting((object) this, e);
    }

    public event CellFormattingEventHandler CellFormatting;

    protected virtual void OnCellFormatting(CellFormattingEventArgs e)
    {
      if (this.CellFormatting == null)
        return;
      this.CellFormatting((object) this, e);
    }

    public event EventHandler ExportCompleted;

    protected virtual void OnExportCompleted(EventArgs e)
    {
      if (this.ExportCompleted == null)
        return;
      this.ExportCompleted((object) this, e);
    }

    public event ProgressChangedEventHandler AsyncExportProgressChanged;

    protected virtual void OnAsyncExportProgressChanged(ProgressChangedEventArgs e)
    {
      if (this.AsyncExportProgressChanged == null)
        return;
      this.AsyncExportProgressChanged((object) this, e);
    }

    public event AsyncCompletedEventHandler AsyncExportCompleted;

    protected virtual void OnAsyncExportCompleted(AsyncCompletedEventArgs e)
    {
      if (this.AsyncExportCompleted == null)
        return;
      this.AsyncExportCompleted((object) this, e);
    }

    [SecurityCritical]
    public void RunExport(string fileName, ISpreadExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      this.spreadExportRenderer.RegisterFormatProvider(this.ExportFormat);
      if (this.RadGridViewToExport.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.RadGridViewToExport.Invoke((Delegate) new GridViewSpreadExport.RunExportCallback(this.RunExportCall), (object) fileName);
      }
      else
        this.RunExportCall(fileName);
    }

    public void RunExport(string fileName, ISpreadExportRenderer exportRenderer, string sheetName)
    {
      this.SheetName = sheetName;
      this.RunExport(fileName, exportRenderer);
    }

    public void RunExportAsync(string fileName, ISpreadExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      if (this.GetWorker().IsBusy)
        throw new Exception("There is an export operation that has not yet finished.");
      List<IGridViewSpreadExportRowInfo> exportRowInfos = (List<IGridViewSpreadExportRowInfo>) null;
      if (this.RadGridViewToExport.InvokeRequired)
      {
        this.RadGridViewToExport.Invoke((Delegate) (() =>
        {
          GridExportState state = this.SaveGridState();
          exportRowInfos = this.GetGridDataSnapshot();
          this.RestoreGridState(state);
        }));
      }
      else
      {
        GridExportState state = this.SaveGridState();
        exportRowInfos = this.GetGridDataSnapshot();
        this.RestoreGridState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new GridDataSnapshot(fileName, exportRowInfos));
    }

    public void RunExportAsync(
      string fileName,
      ISpreadExportRenderer exportRenderer,
      string sheetName)
    {
      this.SheetName = sheetName;
      this.RunExportAsync(fileName, exportRenderer);
    }

    public void RunExport(Stream exportStream, ISpreadExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      this.spreadExportRenderer.RegisterFormatProvider(this.ExportFormat);
      if (this.RadGridViewToExport.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.RadGridViewToExport.Invoke((Delegate) new GridViewSpreadExport.RunExportToStreamCallback(this.RunExportCall), (object) exportStream);
      }
      else
        this.RunExportCall(exportStream);
    }

    public void RunExport(
      Stream exportStream,
      ISpreadExportRenderer exportRenderer,
      string sheetName)
    {
      this.SheetName = sheetName;
      this.RunExport(exportStream, exportRenderer);
    }

    public void RunExportAsync(Stream exportStream, ISpreadExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      if (this.GetWorker().IsBusy)
        throw new Exception("There is an export operation that has not yet finished.");
      List<IGridViewSpreadExportRowInfo> exportRowInfos = (List<IGridViewSpreadExportRowInfo>) null;
      if (this.RadGridViewToExport.InvokeRequired)
      {
        this.RadGridViewToExport.Invoke((Delegate) (() =>
        {
          GridExportState state = this.SaveGridState();
          exportRowInfos = this.GetGridDataSnapshot();
          this.RestoreGridState(state);
        }));
      }
      else
      {
        GridExportState state = this.SaveGridState();
        exportRowInfos = this.GetGridDataSnapshot();
        this.RestoreGridState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new List<object>(2)
      {
        (object) new GridDataSnapshot(string.Empty, exportRowInfos),
        (object) exportStream
      });
    }

    public void RunExportAsync(
      Stream exportStream,
      ISpreadExportRenderer exportRenderer,
      string sheetName)
    {
      this.SheetName = sheetName;
      this.RunExportAsync(exportStream, exportRenderer);
    }

    public void CancelExportAsync()
    {
      this.GetWorker().CancelAsync();
    }

    private BackgroundWorker GetWorker()
    {
      if (this.worker == null)
      {
        this.worker = new BackgroundWorker();
        this.worker.WorkerReportsProgress = true;
        this.worker.WorkerSupportsCancellation = true;
        this.worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
        this.worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
        this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
      }
      return this.worker;
    }

    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.OnAsyncExportCompleted((AsyncCompletedEventArgs) e);
    }

    private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.OnAsyncExportProgressChanged(e);
    }

    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      GridDataSnapshot gridData1 = e.Argument as GridDataSnapshot;
      this.spreadExportRenderer.RegisterFormatProvider(this.ExportFormat);
      this.spreadExportRenderer.GetFormatProvider(this.ExportFormat);
      if (gridData1 != null)
      {
        string path = Path.GetDirectoryName(gridData1.FilePath) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(gridData1.FilePath) + this.spreadExportRenderer.GetFileExtension(this.ExportFormat);
        FileStream fileStream;
        if (this.FileExportMode == FileExportMode.NewSheetInExistingFile && File.Exists(path) && this.ExportFormat == SpreadExportFormat.Xlsx)
        {
          fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
          this.spreadExportRenderer.ImportWorkbook((Stream) fileStream);
        }
        else
        {
          fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
          this.spreadExportRenderer.CreateWorkbook();
        }
        using (fileStream)
        {
          if (this.ExportToStreamAsync(gridData1, e, (Stream) fileStream))
            return;
        }
        this.GetWorker().ReportProgress(100);
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.RadGridViewToExport, "Export", (object) path);
      }
      else
      {
        this.spreadExportRenderer.CreateWorkbook();
        List<object> objectList = e.Argument as List<object>;
        GridDataSnapshot gridData2 = objectList[0] as GridDataSnapshot;
        Stream stream = objectList[1] as Stream;
        if (this.ExportToStreamAsync(gridData2, e, stream))
          return;
        this.GetWorker().ReportProgress(100);
        e.Result = (object) stream;
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.RadGridViewToExport, "Export");
      }
    }

    private bool ExportToStreamAsync(GridDataSnapshot gridData, DoWorkEventArgs e, Stream stream)
    {
      int percentProgress = 0;
      int num1 = 0;
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      for (int index = 0; index < this.RadGridViewToExport.Columns.Count; ++index)
      {
        GridViewDataColumn column = this.RadGridViewToExport.Columns[index];
        if (this.ShouldExportColumn((GridViewColumn) column) && !column.IsVisible && this.HiddenColumnOption != HiddenOption.ExportAlways)
          this.spreadExportRenderer.SetWorksheetColumnWidth(index + this.RadGridViewToExport.GroupDescriptors.Count, 0.0, true);
      }
      if (this.ExportChildRowsGrouped)
        this.InitializeRowGroupDataStructures();
      for (int index = 0; index < gridData.ExportRowInfos.Count; ++index)
      {
        if (this.GetWorker().CancellationPending)
        {
          e.Cancel = true;
          return true;
        }
        if (num1 > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          num1 = 0;
        }
        object worksheet = this.spreadExportRenderer.GetWorksheet();
        gridData.ExportRowInfos[index].AddToWorksheet(worksheet, num1);
        if (this.ExportChildRowsGrouped)
          this.ProcessCurrentRowGrouping(gridData.ExportRowInfos[index].HierarchyLevel, num1);
        int num2 = index * 100 / gridData.ExportRowInfos.Count;
        if (percentProgress != num2)
        {
          percentProgress = num2;
          this.GetWorker().ReportProgress(percentProgress);
        }
        ++num1;
      }
      if (this.ExportChildRowsGrouped)
        this.GroupWorksheetRows(num1);
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
      return false;
    }

    private List<IGridViewSpreadExportRowInfo> GetGridDataSnapshot()
    {
      int visibleColumns = 0;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.RadGridViewToExport.Columns)
      {
        if (this.ShouldExportColumn(column))
          ++visibleColumns;
      }
      List<IGridViewSpreadExportRowInfo> exportRowInfos = new List<IGridViewSpreadExportRowInfo>();
      ExportGridTraverser traverser = new ExportGridTraverser(this.RadGridViewToExport.MasterView);
      traverser.ProcessHierarchy = this.ExportHierarchy;
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
        this.hierarchyRowsStack = new Stack<GridViewHierarchyRowInfo>();
      int currentIndent = 0;
      int snapshotRows = this.CreateSnapshotRows(traverser, exportRowInfos, visibleColumns, currentIndent);
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
      {
        while (this.hierarchyRowsStack.Count > 0 && this.MoveToNextActiveViewOfLastHierarchyRow(traverser))
          this.CreateSnapshotRows(traverser, exportRowInfos, visibleColumns, snapshotRows);
      }
      return exportRowInfos;
    }

    private int CreateSnapshotRows(
      ExportGridTraverser traverser,
      List<IGridViewSpreadExportRowInfo> exportRowInfos,
      int visibleColumns,
      int currentIndent)
    {
      while (traverser.MoveNext())
      {
        bool flag = this.IsVisibleRow(traverser.Current);
        if (flag || this.HiddenRowOption != HiddenOption.DoNotExport)
        {
          bool exportAsHidden = !flag && this.HiddenRowOption == HiddenOption.ExportAsHidden;
          if (this.ExportHierarchy)
          {
            GridViewHierarchyRowInfo current = traverser.Current as GridViewHierarchyRowInfo;
            if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0 && traverser.Current is GridViewGroupRowInfo)
            {
              GridViewHierarchyRowInfo hierarchyRowInfo = this.hierarchyRowsStack.Peek();
              if (hierarchyRowInfo.Views.Count > 1 && hierarchyRowInfo.Views.IndexOf(hierarchyRowInfo.ActiveView) != hierarchyRowInfo.Views.Count - 1)
                continue;
            }
            if (current != null && current.Views.Count > 1)
            {
              switch (this.ChildViewExportMode)
              {
                case ChildViewExportMode.ExportFirstView:
                  current.ActiveView = current.Views[0];
                  break;
                case ChildViewExportMode.SelectViewToExport:
                  ChildViewExportingEventArgs e = new ChildViewExportingEventArgs(current.Views.IndexOf(current.ActiveView), current);
                  this.OnChildViewExporting(e);
                  current.ActiveView = current.Views[e.ActiveViewIndex];
                  break;
                case ChildViewExportMode.ExportAllViews:
                  this.TraverseAllChildViews(traverser);
                  break;
              }
            }
          }
          GridViewDataRowInfo current1 = traverser.Current as GridViewDataRowInfo;
          if (current1 != null)
          {
            List<IGridViewSpreadExportCellInfo> cells = new List<IGridViewSpreadExportCellInfo>();
            foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) current1.ViewTemplate.Columns)
            {
              if (this.ShouldExportColumn((GridViewColumn) column))
              {
                string cellValueFormat = this.GetCellValueFormat(column);
                object objectCellValue = this.GetObjectCellValue(current1.Cells[column.Index]);
                cells.Add((IGridViewSpreadExportCellInfo) new GridViewSpreadExportCellInfo(objectCellValue, cellValueFormat, column.ExcelExportType));
              }
            }
            IGridViewSpreadExportRowInfo spreadExportRowInfo = !this.ExportChildRowsGrouped ? this.spreadExportRenderer.CreateGridViewExportDataRowInfo(currentIndent, cells, exportAsHidden) : this.spreadExportRenderer.CreateGridViewExportDataRowInfo(currentIndent, cells, exportAsHidden, current1.HierarchyLevel);
            exportRowInfos.Add(spreadExportRowInfo);
          }
          else
          {
            GridViewGroupRowInfo current2 = traverser.Current as GridViewGroupRowInfo;
            if (current2 != null)
            {
              currentIndent = current2.GroupLevel;
              int colSpan = visibleColumns - currentIndent + this.RadGridViewToExport.GroupDescriptors.Count - 1;
              string content = current2.HeaderText;
              if (current2.ViewTemplate.SummaryRowGroupHeaders.Count > 0)
                content = content + " | " + current2.GetSummary();
              IGridViewSpreadExportRowInfo spreadExportRowInfo = !this.ExportChildRowsGrouped ? this.spreadExportRenderer.CreateGridViewExportGroupRowInfo(currentIndent, content, colSpan, exportAsHidden) : this.spreadExportRenderer.CreateGridViewExportGroupRowInfo(currentIndent, content, colSpan, exportAsHidden, current2.HierarchyLevel);
              exportRowInfos.Add(spreadExportRowInfo);
              currentIndent = current2.GroupLevel + 1;
            }
            else
            {
              GridViewTableHeaderRowInfo current3 = traverser.Current as GridViewTableHeaderRowInfo;
              if (current3 != null)
              {
                List<IGridViewSpreadExportCellInfo> cells = new List<IGridViewSpreadExportCellInfo>();
                for (int index = 0; index < current3.Cells.Count; ++index)
                {
                  GridViewDataColumn columnInfo = current3.Cells[index].ColumnInfo as GridViewDataColumn;
                  if (this.ShouldExportColumn((GridViewColumn) columnInfo))
                    cells.Add((IGridViewSpreadExportCellInfo) new GridViewSpreadExportCellInfo((object) columnInfo.HeaderText, (string) null, DisplayFormatType.Text));
                }
                IGridViewSpreadExportRowInfo spreadExportRowInfo = !this.ExportChildRowsGrouped ? this.spreadExportRenderer.CreateGridViewExportDataRowInfo(this.RadGridViewToExport.GroupDescriptors.Count, cells, exportAsHidden) : this.spreadExportRenderer.CreateGridViewExportDataRowInfo(this.RadGridViewToExport.GroupDescriptors.Count, cells, exportAsHidden, current3.HierarchyLevel);
                exportRowInfos.Add(spreadExportRowInfo);
              }
              else
              {
                GridViewSummaryRowInfo current4 = traverser.Current as GridViewSummaryRowInfo;
                if (current4 != null && this.ShouldExportSummaryRow(current4))
                {
                  List<IGridViewSpreadExportCellInfo> cells = new List<IGridViewSpreadExportCellInfo>();
                  foreach (GridViewCellInfo cell in current4.Cells)
                  {
                    if (this.ShouldExportColumn(cell.ColumnInfo))
                      cells.Add((IGridViewSpreadExportCellInfo) new GridViewSpreadExportCellInfo((object) current4.GetSummary(cell.ColumnInfo as GridViewDataColumn), (string) null, DisplayFormatType.None));
                  }
                  IGridViewSpreadExportRowInfo spreadExportRowInfo = !this.ExportChildRowsGrouped ? this.spreadExportRenderer.CreateGridViewExportDataRowInfo(this.RadGridViewToExport.GroupDescriptors.Count, cells, exportAsHidden) : this.spreadExportRenderer.CreateGridViewExportDataRowInfo(this.RadGridViewToExport.GroupDescriptors.Count, cells, exportAsHidden, current4.HierarchyLevel);
                  exportRowInfos.Add(spreadExportRowInfo);
                }
              }
            }
          }
        }
      }
      return currentIndent;
    }

    internal void SetValueForCellSelection(DisplayFormatType displayFormatType, object value)
    {
      switch (displayFormatType)
      {
        case DisplayFormatType.None:
        case DisplayFormatType.Text:
          this.spreadExportRenderer.SetCellSelectionValue(DataType.String, value);
          break;
        case DisplayFormatType.Fixed:
        case DisplayFormatType.Standard:
        case DisplayFormatType.Percent:
        case DisplayFormatType.Scientific:
        case DisplayFormatType.Currency:
          this.spreadExportRenderer.SetCellSelectionValue(DataType.Number, value);
          break;
        case DisplayFormatType.GeneralDate:
        case DisplayFormatType.ShortDate:
        case DisplayFormatType.MediumDate:
        case DisplayFormatType.LongDate:
        case DisplayFormatType.ShortDateTime:
        case DisplayFormatType.LongDateTime:
        case DisplayFormatType.LongTime:
        case DisplayFormatType.MediumTime:
        case DisplayFormatType.ShortTime:
          this.spreadExportRenderer.SetCellSelectionValue(DataType.DateTime, value);
          break;
        default:
          this.spreadExportRenderer.SetCellSelectionValue(DataType.Other, value);
          break;
      }
    }

    private object GetObjectCellValue(GridViewCellInfo gridCell)
    {
      object obj = (object) null;
      if (gridCell.RowInfo is GridViewTableHeaderRowInfo)
        obj = (object) gridCell.ColumnInfo.HeaderText;
      else if (gridCell.RowInfo is GridViewSummaryRowInfo)
        obj = (object) ((GridViewSummaryRowInfo) gridCell.RowInfo).GetSummary(gridCell.ColumnInfo as GridViewDataColumn);
      else if (gridCell.RowInfo is GridViewGroupRowInfo)
        obj = (object) (gridCell.RowInfo as GridViewGroupRowInfo).Group.Header;
      else if (gridCell.Value != DBNull.Value && gridCell.Value != null)
      {
        GridViewComboBoxColumn columnInfo = gridCell.ColumnInfo as GridViewComboBoxColumn;
        obj = columnInfo == null || columnInfo.DisplayMember == null ? gridCell.Value : columnInfo.GetLookupValue(gridCell.Value);
      }
      else
      {
        object nullValue = gridCell.ColumnInfo.OwnerTemplate.Columns[gridCell.ColumnInfo.Index].NullValue;
        if (nullValue != null)
          obj = nullValue;
      }
      return obj;
    }

    private void AddRow(GridViewRowInfo gridViewRowInfo, int rowIndex)
    {
      if (!this.IsVisibleRow(gridViewRowInfo) && this.hiddenRowOption == HiddenOption.ExportAsHidden)
        this.spreadExportRenderer.SetWorksheetRowHeight(rowIndex, 0, true);
      else if (this.exportVisualSettings)
      {
        int rowHeight = (int) GridExportUtils.GetRowHeight(this.RadGridViewToExport, this.rowProvider, this.cellProvider, gridViewRowInfo, true);
        this.spreadExportRenderer.SetWorksheetRowHeight(rowIndex, rowHeight, true);
      }
      if (gridViewRowInfo is GridViewGroupRowInfo)
      {
        this.AddGroupRow(gridViewRowInfo as GridViewGroupRowInfo, rowIndex);
      }
      else
      {
        List<GridCellElement> gridCellElementList = new List<GridCellElement>();
        GridRowElement gridRowElement = (GridRowElement) null;
        if (this.exportVisualSettings)
        {
          gridRowElement = (GridRowElement) this.rowProvider.GetElement(gridViewRowInfo, (object) null);
          gridRowElement.InitializeRowView(this.RadGridViewToExport.TableElement);
          this.RadGridViewToExport.TableElement.Children.Add((RadElement) gridRowElement);
          gridRowElement.Initialize(gridViewRowInfo);
          gridRowElement.SuspendLayout();
          for (int index = 0; index < gridRowElement.RowInfo.Cells.Count; ++index)
          {
            GridCellElement element = (GridCellElement) this.cellProvider.GetElement((GridViewColumn) gridRowElement.RowInfo.ViewTemplate.Columns[index], (object) gridRowElement);
            gridRowElement.Children.Add((RadElement) element);
            element.Initialize((GridViewColumn) gridRowElement.RowInfo.ViewTemplate.Columns[index], gridRowElement);
            gridCellElementList.Add(element);
          }
        }
        int skippedCells = 0;
        for (int columnIndex1 = 0; columnIndex1 < gridViewRowInfo.Cells.Count; ++columnIndex1)
        {
          int index = columnIndex1;
          if (this.FreezePinnedColumns && gridViewRowInfo.HierarchyLevel == 0)
            index = this.pinnedColumnsIndicesOrder[index];
          GridViewColumn columnInfo = gridViewRowInfo.Cells[index].ColumnInfo;
          if (!this.ShouldExportColumn(columnInfo))
          {
            if (this.exportVisualSettings)
              GridExportUtils.ReleaseCellElement(this.cellProvider, gridRowElement, gridCellElementList[index]);
            ++skippedCells;
          }
          else
          {
            int columnIndex2 = columnIndex1 + this.RadGridViewToExport.GroupDescriptors.Count - skippedCells;
            if (this.exportVisualSettings && gridViewRowInfo is GridViewTableHeaderRowInfo)
            {
              int num = columnInfo.Width;
              if (gridViewRowInfo.HierarchyLevel > 0)
              {
                int worksheetColumnWidth = this.spreadExportRenderer.GetWorksheetColumnWidth(columnIndex2);
                if (worksheetColumnWidth > columnInfo.Width)
                  num = worksheetColumnWidth;
              }
              this.spreadExportRenderer.SetWorksheetColumnWidth(columnIndex2, (double) num, false);
            }
            if (!columnInfo.IsVisible && this.hiddenColumnOption == HiddenOption.ExportAsHidden)
              this.spreadExportRenderer.SetWorksheetColumnWidth(columnIndex2, 0.0, false);
            object objectCellValue = this.GetObjectCellValue(gridViewRowInfo.Cells[index]);
            this.spreadExportRenderer.CreateCellSelection(rowIndex, columnIndex1 + this.RadGridViewToExport.GroupDescriptors.Count - skippedCells);
            if (gridViewRowInfo is GridViewDataRowInfo)
            {
              GridViewImageColumn imageColumn = columnInfo as GridViewImageColumn;
              if (imageColumn != null)
              {
                Point empty = Point.Empty;
                ContentAlignment imageAlignment;
                ImageLayout imageLayout;
                if (this.ExportVisualSettings)
                {
                  imageAlignment = gridCellElementList[index].ImageAlignment;
                  imageLayout = gridCellElementList[index].ImageLayout;
                  empty.X = (int) gridCellElementList[index].Layout.LeftPart.Bounds.Location.X;
                  empty.Y = (int) gridCellElementList[index].Layout.LeftPart.Bounds.Location.Y;
                }
                else
                {
                  imageAlignment = imageColumn.ImageAlignment;
                  imageLayout = imageColumn.ImageLayout;
                  empty.X = this.imageOffset;
                  empty.Y = this.imageOffset;
                }
                this.AddFloatingImage(gridViewRowInfo, index, imageColumn, imageAlignment, imageLayout, rowIndex, columnIndex1, skippedCells, empty);
              }
              else
              {
                string cellValueFormat = this.GetCellValueFormat(columnInfo as GridViewDataColumn);
                DisplayFormatType excelExportType = gridViewRowInfo.ViewTemplate.Columns[index].ExcelExportType;
                this.spreadExportRenderer.SetCellSelectionFormat(cellValueFormat);
                this.SetValueForCellSelection(excelExportType, objectCellValue);
              }
            }
            else if (objectCellValue == null)
            {
              this.spreadExportRenderer.ClearCellSelectionValue();
            }
            else
            {
              if (gridViewRowInfo is GridViewTableHeaderRowInfo)
                this.spreadExportRenderer.SetCellSelectionFormat("@");
              this.spreadExportRenderer.SetCellSelectionValue(objectCellValue.ToString());
            }
            if (this.exportVisualSettings)
            {
              GridCellElement gridCellElement = gridCellElementList[index];
              ContentAlignment textAlignment = this.RadGridViewToExport.RightToLeft == RightToLeft.No ? gridCellElement.TextAlignment : GridExportUtils.ConvertToRightToLeftAlignment(gridCellElement.TextAlignment);
              this.spreadExportRenderer.CreateCellStyleInfo(GridExportUtils.GetBackColor((LightVisualElement) gridCellElement), gridCellElement.ForeColor, gridCellElement.Font.FontFamily, (double) gridCellElement.Font.Size, gridCellElement.Font.Bold, gridCellElement.Font.Italic, gridCellElement.Font.Underline, textAlignment, gridCellElement.TextWrap, gridCellElement.BorderBoxStyle, gridCellElement.BorderColor, gridCellElement.BorderTopColor, gridCellElement.BorderBottomColor, gridCellElement.BorderRightColor, gridCellElement.BorderLeftColor);
              GridExportUtils.ReleaseCellElement(this.cellProvider, gridRowElement, gridCellElementList[index]);
            }
            else if (this.CellFormatting != null)
              this.spreadExportRenderer.CreateCellStyleInfo();
            else
              continue;
            object cellSelection = this.spreadExportRenderer.GetCellSelection();
            ISpreadExportCellStyleInfo cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
            this.OnCellFormatting(new CellFormattingEventArgs(rowIndex, index, gridViewRowInfo.GetType(), gridViewRowInfo.Cells[index], cellSelection, cellStyleInfo));
          }
        }
        if (!this.exportVisualSettings)
          return;
        GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, gridRowElement);
      }
    }

    private void AddFloatingImage(
      GridViewRowInfo gridViewRowInfo,
      int colIndex,
      GridViewImageColumn imageColumn,
      ContentAlignment imageAlignment,
      ImageLayout imageLayout,
      int rowIndex,
      int columnIndex,
      int skippedCells,
      Point imageOffset)
    {
      object result = (object) null;
      RadDataConverter.Instance.TryFormat(gridViewRowInfo.Cells[colIndex].Value, typeof (Image), (IDataConversionInfoProvider) imageColumn, out result);
      Image image = result as Image;
      if (image == null)
        return;
      int offsetX = imageOffset.X;
      int offsetY = imageOffset.Y;
      if (offsetX == 0)
        offsetX = this.imageOffset;
      if (offsetY == 0)
        offsetY = this.imageOffset;
      int num1 = this.spreadExportRenderer.GetWorksheetColumnWidth(columnIndex) - offsetX * 2;
      int num2 = (int) this.spreadExportRenderer.GetWorksheetRowHeight(rowIndex) - offsetY * 2;
      Size size = image.Size;
      switch (imageLayout)
      {
        case ImageLayout.None:
        case ImageLayout.Tile:
          RectangleF rectangleF = LayoutUtils.Align(new SizeF((float) Math.Min(num1, size.Width), (float) Math.Min(num2, size.Height)), (RectangleF) new Rectangle(Point.Empty, new Size(num1, num2)), imageAlignment);
          offsetX += (int) rectangleF.X;
          offsetY += (int) rectangleF.Y;
          num1 = (int) rectangleF.Size.Width;
          num2 = (int) rectangleF.Size.Height;
          break;
        case ImageLayout.Center:
          PointF pointF = new PointF(Math.Max(0.0f, (float) Math.Max(0, (num1 - image.Width) / 2)), Math.Max(0.0f, (float) Math.Max(0, (num2 - image.Height) / 2)));
          offsetX += (int) pointF.X;
          offsetY += (int) pointF.Y;
          num1 = Math.Min(num1, image.Size.Width);
          num2 = Math.Min(num2, image.Size.Height);
          break;
        case ImageLayout.Zoom:
          if (image.Width != 0 && image.Height != 0)
          {
            float num3 = Math.Min((float) num1 / (float) image.Width, (float) num2 / (float) image.Height);
            if ((double) num3 > 0.0)
            {
              int num4 = (int) Math.Round((double) image.Width * (double) num3);
              int num5 = (int) Math.Round((double) image.Height * (double) num3);
              offsetX += (num1 - num4) / 2;
              offsetY += (num2 - num5) / 2;
              num1 = num4;
              num2 = num5;
              break;
            }
            break;
          }
          break;
      }
      if (offsetY < imageOffset.Y)
        offsetY = imageOffset.Y;
      byte[] byteArray = GridExportUtils.ConvertImageToByteArray(image);
      this.spreadExportRenderer.CreateFloatingImage(rowIndex, columnIndex + this.RadGridViewToExport.GroupDescriptors.Count - skippedCells, offsetX, offsetY, byteArray, "png", num1, num2, 0);
    }

    private void AddGroupRow(GridViewGroupRowInfo gridViewGroupRowInfo, int rowIndex)
    {
      GridGroupHeaderRowElement element = (GridGroupHeaderRowElement) this.rowProvider.CreateElement((GridViewRowInfo) gridViewGroupRowInfo, (object) null);
      element.InitializeRowView(this.RadGridViewToExport.TableElement);
      this.RadGridViewToExport.TableElement.Children.Add((RadElement) element);
      element.Initialize((GridViewRowInfo) gridViewGroupRowInfo);
      element.UpdateInfo();
      int groupLevel = gridViewGroupRowInfo.GroupLevel;
      int num1 = 0;
      if (this.rowColumns != null)
      {
        num1 = this.rowColumns[0];
      }
      else
      {
        foreach (GridViewCellInfo cell in gridViewGroupRowInfo.Cells)
        {
          if (this.ShouldExportColumn(cell.ColumnInfo))
            ++num1;
        }
      }
      int num2 = num1 + (this.RadGridViewToExport.GroupDescriptors.Count - groupLevel - 1);
      this.spreadExportRenderer.CreateCellSelection(rowIndex, groupLevel, rowIndex, groupLevel + num2);
      this.spreadExportRenderer.MergeCellSelection();
      this.spreadExportRenderer.SetCellSelectionFormat("@");
      this.spreadExportRenderer.SetCellSelectionValue(element.ContentCell.Text);
      if (this.exportVisualSettings)
      {
        GridGroupContentCellElement contentCell = element.ContentCell;
        this.spreadExportRenderer.CreateCellStyleInfo(GridExportUtils.GetBackColor((LightVisualElement) contentCell), contentCell.ForeColor, contentCell.Font.FontFamily, (double) contentCell.Font.Size, contentCell.Font.Bold, contentCell.Font.Italic, contentCell.Font.Underline, contentCell.TextAlignment, contentCell.TextWrap, contentCell.BorderBoxStyle, contentCell.BorderColor, contentCell.BorderTopColor, contentCell.BorderBottomColor, contentCell.BorderRightColor, contentCell.BorderLeftColor);
      }
      else if (this.CellFormatting != null)
        this.spreadExportRenderer.CreateCellStyleInfo();
      object cellSelection = this.spreadExportRenderer.GetCellSelection();
      ISpreadExportCellStyleInfo cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
      if (cellStyleInfo != null)
        this.OnCellFormatting(new CellFormattingEventArgs(rowIndex, 0, gridViewGroupRowInfo.GetType(), gridViewGroupRowInfo.Cells[0], cellSelection, cellStyleInfo));
      GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, (GridRowElement) element);
    }

    private int AddViewDefinitionRow(
      GridViewRowInfo gridViewRowInfo,
      TableViewRowLayoutBase rowLayout,
      int rowIndex)
    {
      GridRowElement gridRowElement = (GridRowElement) null;
      ColumnGroupRowLayout columnGroupRowLayout = (ColumnGroupRowLayout) null;
      HtmlViewRowLayout htmlRowLayout = (HtmlViewRowLayout) null;
      if (rowLayout is ColumnGroupRowLayout)
        columnGroupRowLayout = rowLayout as ColumnGroupRowLayout;
      else if (rowLayout is HtmlViewRowLayout)
        htmlRowLayout = rowLayout as HtmlViewRowLayout;
      if (columnGroupRowLayout != null && this.rowSpanCells == null)
        this.CreateRowSpanCells(columnGroupRowLayout);
      else if (htmlRowLayout != null && this.rowSpanCells == null)
        this.CreateRowSpanCells(htmlRowLayout);
      if (this.exportVisualSettings)
      {
        gridRowElement = (GridRowElement) this.rowProvider.GetElement(gridViewRowInfo, (object) null);
        gridRowElement.InitializeRowView(this.RadGridViewToExport.TableElement);
        this.RadGridViewToExport.TableElement.Children.Add((RadElement) gridRowElement);
        gridRowElement.Initialize(gridViewRowInfo);
        gridRowElement.SuspendLayout();
      }
      RectangleF rectangleF = RectangleF.Empty;
      int count = this.RadGridViewToExport.GroupDescriptors.Count;
      int num1 = rowIndex;
      int toRowIndex = num1;
      int num2 = count;
      for (int index1 = 0; index1 < rowLayout.RenderColumns.Count; ++index1)
      {
        GridViewColumn renderColumn = rowLayout.RenderColumns[index1];
        CellDefinition cellDefinition = (CellDefinition) null;
        int index2 = 0;
        if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
        {
          if (columnGroupRowLayout != null)
          {
            rectangleF = columnGroupRowLayout.GetCorrectedColumnBounds(gridViewRowInfo, renderColumn, false, new RectangleF(0.0f, 0.0f, rowLayout.DesiredSize.Width, rowLayout.DesiredSize.Height));
            if (renderColumn.Index >= 0)
            {
              for (int index3 = 0; index3 < this.columnGroupRowDefinitions.Count; ++index3)
              {
                for (int index4 = 0; index4 < this.columnGroupRowDefinitions[index3].Cells.Count; ++index4)
                {
                  if (this.columnGroupRowDefinitions[index3].Cells[index4].UniqueName == renderColumn.Name)
                  {
                    cellDefinition = this.columnGroupRowDefinitions[index3].Cells[index4];
                    num1 = rowIndex + index3;
                    index2 = index3;
                  }
                }
              }
            }
          }
          else if (htmlRowLayout != null)
          {
            HtmlViewCellArrangeInfo arrangeInfo = htmlRowLayout.GetArrangeInfo(renderColumn);
            if (arrangeInfo != null)
            {
              rectangleF = arrangeInfo.Bounds;
              cellDefinition = arrangeInfo.Cell;
              num1 = rowIndex + cellDefinition.RowIndex;
              index2 = cellDefinition.RowIndex;
            }
            else
              continue;
          }
          if (!(rectangleF == RectangleF.Empty))
          {
            object obj;
            if (gridViewRowInfo is GridViewTableHeaderRowInfo)
            {
              GridViewCellInfo cell = this.RadGridViewToExport.MasterView.TableHeaderRow.Cells[renderColumn.Name];
              obj = (object) renderColumn.HeaderText;
            }
            else if (gridViewRowInfo is GridViewSummaryRowInfo)
            {
              GridViewCellInfo cell = (gridViewRowInfo as GridViewSummaryRowInfo).Cells[renderColumn.Name];
              if (cell != null)
                obj = cell.Value != null ? (object) cell.Value.ToString() : (object) string.Empty;
              else
                continue;
            }
            else
            {
              GridViewCellInfo cell = gridViewRowInfo.Cells[renderColumn.Name];
              if (cell != null)
              {
                GridViewColumn columnInfo1 = cell.ColumnInfo;
                object cellValue = this.GetObjectCellValue(cell);
                GridViewComboBoxColumn columnInfo2 = cell.ColumnInfo as GridViewComboBoxColumn;
                if (columnInfo2 != null && columnInfo2.HasLookupValue)
                  cellValue = columnInfo2.GetLookupValue(cellValue);
                obj = cellValue ?? (object) string.Empty;
              }
              else
                continue;
            }
            if (cellDefinition != null)
            {
              num2 = count + cellDefinition.ColumnIndex;
              foreach (Point rowSpanCell in this.rowSpanCells)
              {
                if (rowSpanCell.Y == index2 && rowSpanCell.X == this.rowColumns[index2])
                  ++num2;
              }
              toRowIndex = num1 + cellDefinition.RowSpan - 1;
              int toColumnIndex = num2 + cellDefinition.ColSpan - 1;
              if (this.isFirstViewDefinitionRow && columnGroupRowLayout != null)
              {
                int treeDepth = this.tree.GetTreeDepth();
                int num3 = num1 + treeDepth;
                toRowIndex += treeDepth;
                int num4 = 0;
                for (int rowIndex1 = num3 - 1; rowIndex1 >= 0 && !this.spreadExportRenderer.GetIsMerged(rowIndex1, num2); --rowIndex1)
                {
                  this.spreadExportRenderer.CreateCellSelection(rowIndex1, num2);
                  object cellSelectionValue = this.spreadExportRenderer.GetCellSelectionValue();
                  if (cellSelectionValue == null || cellSelectionValue.ToString() == string.Empty)
                    ++num4;
                  else
                    break;
                }
                num1 = num3 - num4;
              }
              this.spreadExportRenderer.CreateCellSelection(num1, num2, toRowIndex, toColumnIndex);
              this.spreadExportRenderer.MergeCellSelection();
              if (num1 == toRowIndex)
                this.spreadExportRenderer.SetWorksheetRowHeight(num1, (int) rectangleF.Height, true);
              if (num2 == toColumnIndex)
                this.spreadExportRenderer.SetWorksheetColumnWidth(num2, (double) renderColumn.Width, false);
            }
            else
            {
              int num3 = 0;
              GridViewSpreadExport.GroupNode childNode = this.tree.GetChildNode(obj.ToString());
              if (childNode != null)
              {
                num1 = rowIndex + childNode.Level - 1;
                int previousNodesColSpan = this.tree.GetPreviousNodesColSpan(childNode);
                num2 = count + previousNodesColSpan;
                num3 = childNode.ColSpan - 1;
              }
              toRowIndex = num1;
              int toColumnIndex = num3 + num2;
              this.spreadExportRenderer.CreateCellSelection(num1, num2, toRowIndex, toColumnIndex);
              this.spreadExportRenderer.MergeCellSelection();
              if (num1 == toRowIndex)
                this.spreadExportRenderer.SetWorksheetRowHeight(num1, (int) rectangleF.Height, true);
              if (num2 == toColumnIndex)
                this.spreadExportRenderer.SetWorksheetColumnWidth(num2, (double) renderColumn.Width, false);
            }
            int index3 = renderColumn.Index;
            if (gridViewRowInfo is GridViewDataRowInfo)
            {
              GridViewImageColumn imageColumn = renderColumn as GridViewImageColumn;
              if (imageColumn != null)
              {
                this.AddFloatingImage(gridViewRowInfo, index3, imageColumn, imageColumn.ImageAlignment, imageColumn.ImageLayout, num1, num2, 0, new Point(this.imageOffset, this.imageOffset));
              }
              else
              {
                string cellValueFormat = this.GetCellValueFormat(renderColumn as GridViewDataColumn);
                DisplayFormatType excelExportType = gridViewRowInfo.ViewTemplate.Columns[index3].ExcelExportType;
                this.spreadExportRenderer.SetCellSelectionFormat(cellValueFormat);
                this.SetValueForCellSelection(excelExportType, obj);
                if (excelExportType.ToString().Contains("Date") || excelExportType.ToString().Contains("Time"))
                  this.spreadExportRenderer.SetCellSelectionFormat(cellValueFormat);
              }
            }
            else if (obj == null || obj.ToString() == string.Empty)
              this.spreadExportRenderer.ClearCellSelectionValue();
            else
              this.spreadExportRenderer.SetCellSelectionValue(obj.ToString());
            if (this.ExportVisualSettings)
              this.InitializeVirtualCellElement(gridRowElement, renderColumn);
            else if (this.CellFormatting != null)
              this.spreadExportRenderer.CreateCellStyleInfo();
            else
              continue;
            object cellSelection = this.spreadExportRenderer.GetCellSelection();
            ISpreadExportCellStyleInfo cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
            this.OnCellFormatting(new CellFormattingEventArgs(rowIndex, index3, gridViewRowInfo.GetType(), gridViewRowInfo.Cells[index3], cellSelection, cellStyleInfo));
          }
        }
      }
      if (this.exportVisualSettings)
        GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, gridRowElement);
      this.isFirstViewDefinitionRow = false;
      return toRowIndex - rowIndex;
    }

    private void CreateRowSpanCells(ColumnGroupRowLayout columnGroupRowLayout)
    {
      this.tree = new GridViewSpreadExport.GroupNode((GridViewColumnGroup) null);
      this.tree.Level = 0;
      this.tree.ColSpan = -1;
      this.columnGroupRowDefinitions = new List<RowDefinition>();
      foreach (GridViewColumnGroup columnGroup in (Collection<GridViewColumnGroup>) columnGroupRowLayout.ViewDefinition.ColumnGroups)
      {
        GridViewSpreadExport.GroupNode groupNode = new GridViewSpreadExport.GroupNode(columnGroup);
        this.tree.AddChildNode(groupNode);
        if (columnGroup.IsVisible || columnGroupRowLayout.IgnoreColumnVisibility)
          this.ProcessGroup(columnGroupRowLayout, columnGroup, groupNode);
      }
      this.rowColumns = new int[this.columnGroupRowDefinitions.Count];
      this.rowSpanCells = new List<Point>();
      int num = 0;
      for (int index1 = 0; index1 < this.columnGroupRowDefinitions.Count; ++index1)
      {
        foreach (CellDefinition cell in (Collection<CellDefinition>) this.columnGroupRowDefinitions[index1].Cells)
        {
          GridViewColumn column = (GridViewColumn) columnGroupRowLayout.ViewTemplate.Columns[cell.UniqueName];
          if (column != null && (column.IsVisible || columnGroupRowLayout.IgnoreColumnVisibility))
          {
            foreach (Point rowSpanCell in this.rowSpanCells)
            {
              if (rowSpanCell.Y == index1 && rowSpanCell.X == this.rowColumns[index1])
                ++this.rowColumns[index1];
            }
            cell.ColumnIndex = this.rowColumns[index1];
            this.rowColumns[index1] += cell.ColSpan;
            for (int index2 = 0; index2 < cell.ColSpan; ++index2)
            {
              for (int index3 = 1; index3 < cell.RowSpan; ++index3)
              {
                Point point = new Point(cell.ColumnIndex + index2, index1 + index3);
                this.rowSpanCells.Insert(~this.rowSpanCells.BinarySearch(point, (IComparer<Point>) new GridViewSpreadExport.PointComparer()), point);
              }
            }
          }
        }
        if (this.rowColumns[index1] > num)
          num = this.rowColumns[index1];
        else if (this.rowColumns[index1] < num)
          this.rowColumns[index1] = num;
      }
    }

    private void ProcessGroup(
      ColumnGroupRowLayout columnGroupRowLayout,
      GridViewColumnGroup group,
      GridViewSpreadExport.GroupNode node)
    {
      if (group.Groups.Count == 0)
      {
        List<RowDefinition> rowDefinitionList = new List<RowDefinition>();
        int num1 = 0;
        for (int index1 = 0; index1 < group.Rows.Count; ++index1)
        {
          GridViewColumnGroupRow row = group.Rows[index1];
          if (num1 < row.ColumnNames.Count)
            num1 = row.ColumnNames.Count;
          if (row.ColumnNames.Count != 0)
          {
            if (rowDefinitionList.Count < index1 + 1)
              rowDefinitionList.Add(new RowDefinition());
            for (int index2 = 0; index2 < row.ColumnNames.Count; ++index2)
            {
              GridViewColumn column = (GridViewColumn) columnGroupRowLayout.ViewTemplate.Columns[row.ColumnNames[index2]];
              if (column == null || !column.IsVisible)
                --num1;
              else
                rowDefinitionList[index1].Cells.Add(new CellDefinition()
                {
                  ColumnIndex = column.Index,
                  UniqueName = column.Name,
                  Width = column.Width
                });
            }
          }
          else
            break;
        }
        for (int index1 = 0; index1 < rowDefinitionList.Count; ++index1)
        {
          RowDefinition rowDefinition = rowDefinitionList[index1];
          int index2 = 0;
          int num2 = num1 - rowDefinition.Cells.Count;
          while (num2 > 0)
          {
            ++rowDefinition.Cells[index2].ColSpan;
            ++index2;
            --num2;
            if (index2 >= rowDefinition.Cells.Count)
              index2 = 0;
          }
        }
        if (this.columnGroupRowDefinitions.Count > 0 && this.columnGroupRowDefinitions.Count > rowDefinitionList.Count)
        {
          for (int index1 = 0; index1 < rowDefinitionList[0].Cells.Count; ++index1)
          {
            int num2 = this.columnGroupRowDefinitions.Count - rowDefinitionList.Count;
            int index2 = 0;
            while (num2 > 0)
            {
              ++rowDefinitionList[index2].Cells[index1].RowSpan;
              ++index2;
              --num2;
              if (index2 >= rowDefinitionList.Count)
                index2 = 0;
            }
          }
        }
        else if (this.columnGroupRowDefinitions.Count > 0 && this.columnGroupRowDefinitions.Count < rowDefinitionList.Count)
        {
          for (int index1 = 0; index1 < this.columnGroupRowDefinitions[0].Cells.Count; ++index1)
          {
            int num2 = rowDefinitionList.Count - this.columnGroupRowDefinitions.Count;
            int index2 = 0;
            while (num2 > 0)
            {
              ++this.columnGroupRowDefinitions[index2].Cells[index1].RowSpan;
              ++index2;
              --num2;
              if (index2 >= this.columnGroupRowDefinitions.Count)
                index2 = 0;
            }
          }
        }
        for (int index = 0; index < rowDefinitionList.Count; ++index)
        {
          RowDefinition rowDefinition = rowDefinitionList[index];
          if (this.columnGroupRowDefinitions.Count < index + 1)
          {
            this.columnGroupRowDefinitions.Add(rowDefinition);
          }
          else
          {
            foreach (CellDefinition cell in (Collection<CellDefinition>) rowDefinition.Cells)
              this.columnGroupRowDefinitions[index].Cells.Add(cell);
          }
        }
        node.ColSpan = num1;
      }
      else
      {
        foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) group.Groups)
        {
          if (group1.IsVisible || columnGroupRowLayout.IgnoreColumnVisibility)
          {
            GridViewSpreadExport.GroupNode groupNode = new GridViewSpreadExport.GroupNode(group1);
            node.AddChildNode(groupNode);
            this.ProcessGroup(columnGroupRowLayout, group1, groupNode);
          }
        }
      }
    }

    private void CreateRowSpanCells(HtmlViewRowLayout htmlRowLayout)
    {
      this.rowColumns = new int[htmlRowLayout.RowTemplate.Rows.Count];
      this.rowSpanCells = new List<Point>();
      int num = 0;
      for (int index1 = 0; index1 < htmlRowLayout.RowTemplate.Rows.Count; ++index1)
      {
        foreach (CellDefinition cell in (Collection<CellDefinition>) htmlRowLayout.RowTemplate.Rows[index1].Cells)
        {
          GridViewColumn column = (GridViewColumn) htmlRowLayout.ViewTemplate.Columns[cell.UniqueName];
          if (column != null && (column.IsVisible || htmlRowLayout.IgnoreColumnVisibility))
          {
            foreach (Point rowSpanCell in this.rowSpanCells)
            {
              if (rowSpanCell.Y == index1 && rowSpanCell.X == this.rowColumns[index1])
                ++this.rowColumns[index1];
            }
            cell.ColumnIndex = this.rowColumns[index1];
            this.rowColumns[index1] += cell.ColSpan;
            for (int index2 = 0; index2 < cell.ColSpan; ++index2)
            {
              for (int index3 = 1; index3 < cell.RowSpan; ++index3)
              {
                Point point = new Point(cell.ColumnIndex + index2, index1 + index3);
                this.rowSpanCells.Insert(~this.rowSpanCells.BinarySearch(point, (IComparer<Point>) new GridViewSpreadExport.PointComparer()), point);
              }
            }
          }
        }
        if (this.rowColumns[index1] > num)
          num = this.rowColumns[index1];
        else if (this.rowColumns[index1] < num)
          this.rowColumns[index1] = num;
      }
    }

    private void InitializeVirtualCellElement(GridRowElement vRowElement, GridViewColumn col)
    {
      int index1 = col.Index;
      int index2 = index1 < 0 ? 0 : index1;
      GridCellElement element = (GridCellElement) this.cellProvider.GetElement((GridViewColumn) vRowElement.RowInfo.ViewTemplate.Columns[index2], (object) vRowElement);
      vRowElement.Children.Add((RadElement) element);
      element.Initialize((GridViewColumn) vRowElement.RowInfo.ViewTemplate.Columns[index2], vRowElement);
      ContentAlignment textAlignment = this.RadGridViewToExport.RightToLeft == RightToLeft.No ? element.TextAlignment : GridExportUtils.ConvertToRightToLeftAlignment(element.TextAlignment);
      this.spreadExportRenderer.CreateCellStyleInfo(GridExportUtils.GetBackColor((LightVisualElement) element), element.ForeColor, element.Font.FontFamily, (double) element.Font.Size, element.Font.Bold, element.Font.Italic, element.Font.Underline, textAlignment, element.TextWrap, element.BorderBoxStyle, element.BorderColor, element.BorderTopColor, element.BorderBottomColor, element.BorderRightColor, element.BorderLeftColor);
      GridExportUtils.ReleaseCellElement(this.cellProvider, vRowElement, element);
    }

    private string GetCellValueFormat(GridViewDataColumn column)
    {
      if (column.ExcelExportType == DisplayFormatType.Custom && !string.IsNullOrEmpty(column.ExcelExportFormatString))
        return column.ExcelExportFormatString;
      return this.GetCellValueFormat(column.ExcelExportType);
    }

    private string GetCellValueFormat(DisplayFormatType exportType)
    {
      CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
      switch (exportType)
      {
        case DisplayFormatType.None:
          return string.Empty;
        case DisplayFormatType.Text:
          return "@";
        case DisplayFormatType.Fixed:
        case DisplayFormatType.Standard:
          return "0" + currentCulture.NumberFormat.NumberDecimalSeparator + "00";
        case DisplayFormatType.Percent:
          return "0" + currentCulture.NumberFormat.PercentDecimalSeparator + "00%";
        case DisplayFormatType.Scientific:
          return "0" + currentCulture.NumberFormat.NumberDecimalSeparator + "00E+00";
        case DisplayFormatType.GeneralDate:
        case DisplayFormatType.ShortDate:
          return currentCulture.DateTimeFormat.ShortDatePattern;
        case DisplayFormatType.MediumDate:
          return currentCulture.DateTimeFormat.ShortDatePattern;
        case DisplayFormatType.LongDate:
          return currentCulture.DateTimeFormat.LongDatePattern;
        case DisplayFormatType.ShortDateTime:
          return this.PrepareTimePattern(currentCulture.DateTimeFormat.ShortDatePattern + " " + currentCulture.DateTimeFormat.ShortTimePattern);
        case DisplayFormatType.LongDateTime:
          return this.PrepareTimePattern(currentCulture.DateTimeFormat.LongDatePattern + " " + currentCulture.DateTimeFormat.LongTimePattern);
        case DisplayFormatType.Currency:
          return currentCulture.NumberFormat.CurrencySymbol + "#" + currentCulture.NumberFormat.CurrencyGroupSeparator + "##0" + currentCulture.NumberFormat.CurrencyDecimalSeparator + "00_)";
        case DisplayFormatType.LongTime:
          return this.PrepareTimePattern(currentCulture.DateTimeFormat.LongTimePattern);
        case DisplayFormatType.MediumTime:
          return this.PrepareTimePattern(currentCulture.DateTimeFormat.ShortTimePattern);
        case DisplayFormatType.ShortTime:
          return this.PrepareTimePattern(currentCulture.DateTimeFormat.ShortTimePattern);
        case DisplayFormatType.Custom:
          return (string) null;
        default:
          return (string) null;
      }
    }

    private string PrepareTimePattern(string pattern)
    {
      return pattern.Replace(" tt", " AM/PM");
    }

    private void RunExportCall(Stream exportStream)
    {
      GridExportState state = this.SaveGridState();
      this.pinnedRowsCount = 0;
      this.pinnedColumnsCount = 0;
      try
      {
        this.spreadExportRenderer.GetFormatProvider(this.ExportFormat);
        this.spreadExportRenderer.CreateWorkbook();
        this.ProcessPinnedColumns();
        this.ExportToStream(exportStream);
        this.OnExportCompleted(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.RadGridViewToExport, "Export");
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreGridState(state);
      }
    }

    private void RunExportCall(string fileName)
    {
      GridExportState state = this.SaveGridState();
      this.pinnedRowsCount = 0;
      this.pinnedColumnsCount = 0;
      try
      {
        this.spreadExportRenderer.GetFormatProvider(this.ExportFormat);
        fileName = Path.GetDirectoryName(fileName) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName) + this.spreadExportRenderer.GetFileExtension(this.ExportFormat);
        FileStream fileStream;
        if (this.FileExportMode == FileExportMode.NewSheetInExistingFile && File.Exists(fileName) && this.ExportFormat == SpreadExportFormat.Xlsx)
        {
          fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
          this.spreadExportRenderer.ImportWorkbook((Stream) fileStream);
        }
        else
        {
          fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
          this.spreadExportRenderer.CreateWorkbook();
        }
        this.ProcessPinnedColumns();
        using (fileStream)
          this.ExportToStream((Stream) fileStream);
        this.OnExportCompleted(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.RadGridViewToExport, "Export", (object) fileName);
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreGridState(state);
      }
    }

    private void ProcessPinnedColumns()
    {
      if (!this.FreezePinnedColumns)
        return;
      int count = this.RadGridViewToExport.Columns.Count;
      this.pinnedColumnsIndicesOrder = new List<int>(count);
      int index1 = 0;
      for (int index2 = 0; index2 < count; ++index2)
      {
        if (this.RadGridViewToExport.Columns[index2].IsPinned)
        {
          this.pinnedColumnsIndicesOrder.Insert(index1, index2);
          ++index1;
          ++this.pinnedColumnsCount;
        }
        else
          this.pinnedColumnsIndicesOrder.Add(index2);
      }
    }

    private void ExportToStream(Stream stream)
    {
      int num1 = 0;
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      this.rowProvider = (RowElementProvider) this.RadGridViewToExport.TableElement.RowElementProvider;
      this.cellProvider = (CellElementProvider) this.RadGridViewToExport.TableElement.CellElementProvider;
      ExportGridTraverser traverser = new ExportGridTraverser(this.RadGridViewToExport.MasterView);
      traverser.ProcessHierarchy = this.exportHierarchy;
      TableViewRowLayoutBase rowLayout = (TableViewRowLayoutBase) null;
      if (this.ExportViewDefinition)
      {
        if (this.RadGridViewToExport.ViewDefinition is ColumnGroupsViewDefinition)
          rowLayout = (TableViewRowLayoutBase) this.InitializeColumnGroupRowLayout();
        else if (this.RadGridViewToExport.ViewDefinition is HtmlViewDefinition)
          rowLayout = (TableViewRowLayoutBase) this.InitializeHtmlViewRowLayout();
      }
      if ((this.FreezeHeaderRow || this.FreezePinnedRows) && traverser.MoveNext())
      {
        if (traverser.Current is GridViewTableHeaderRowInfo)
        {
          if (rowLayout != null)
          {
            int num2 = this.AddViewDefinitionRow(traverser.Current, rowLayout, num1) + 1;
            this.pinnedRowsCount += num2;
            num1 += num2;
          }
          else
          {
            this.AddRow(traverser.Current, num1);
            ++this.pinnedRowsCount;
            ++num1;
          }
        }
        if (this.FreezePinnedRows)
        {
          foreach (GridViewRowInfo pinnedRow in this.RadGridViewToExport.MasterView.PinnedRows)
          {
            if (rowLayout != null)
            {
              int num2 = this.AddViewDefinitionRow(pinnedRow, rowLayout, num1) + 1;
              this.pinnedRowsCount += num2;
              num1 += num2;
            }
            else
            {
              this.AddRow(pinnedRow, num1);
              ++this.pinnedRowsCount;
              ++num1;
            }
          }
        }
      }
      this.InitializeRowGroupDataStructures();
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
        this.hierarchyRowsStack = new Stack<GridViewHierarchyRowInfo>();
      int currentRowNum = this.ProcessRows(traverser, rowLayout, num1);
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
      {
        while (this.hierarchyRowsStack.Count > 0 && this.MoveToNextActiveViewOfLastHierarchyRow(traverser))
          currentRowNum = this.ProcessRows(traverser, rowLayout, currentRowNum);
      }
      if (this.ExportChildRowsGrouped)
        this.GroupWorksheetRows(currentRowNum);
      this.spreadExportRenderer.CreateFreezePanes(this.pinnedRowsCount, this.pinnedColumnsCount);
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
    }

    private bool MoveToNextActiveViewOfLastHierarchyRow(ExportGridTraverser traverser)
    {
      GridViewHierarchyRowInfo hierarchyRowInfo = this.hierarchyRowsStack.Peek();
      traverser.Reset();
      if (traverser.MoveForward((GridViewRowInfo) hierarchyRowInfo))
      {
        if (hierarchyRowInfo.Views.IndexOf(hierarchyRowInfo.ActiveView) == hierarchyRowInfo.Views.Count - 1)
        {
          this.hierarchyRowsStack.Pop();
          return false;
        }
        if (traverser.MoveBackward((GridViewRowInfo) hierarchyRowInfo))
        {
          GridViewHierarchyRowInfo current = traverser.Current as GridViewHierarchyRowInfo;
          current.ActiveView = current.Views[current.Views.IndexOf(current.ActiveView) + 1];
        }
      }
      return true;
    }

    private int ProcessRows(
      ExportGridTraverser traverser,
      TableViewRowLayoutBase rowLayout,
      int currentRowNum)
    {
      while (traverser.MoveNext())
      {
        if (this.applicationDoEvents)
          Application.DoEvents();
        GridViewHierarchyRowInfo current1 = traverser.Current as GridViewHierarchyRowInfo;
        if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0 && traverser.Current is GridViewGroupRowInfo)
        {
          GridViewHierarchyRowInfo hierarchyRowInfo = this.hierarchyRowsStack.Peek();
          if (hierarchyRowInfo.Views.Count > 1 && hierarchyRowInfo.Views.IndexOf(hierarchyRowInfo.ActiveView) != hierarchyRowInfo.Views.Count - 1)
            continue;
        }
        if (current1 != null && current1.Views.Count > 1)
        {
          switch (this.ChildViewExportMode)
          {
            case ChildViewExportMode.ExportFirstView:
              current1.ActiveView = current1.Views[0];
              break;
            case ChildViewExportMode.SelectViewToExport:
              ChildViewExportingEventArgs e = new ChildViewExportingEventArgs(current1.Views.IndexOf(current1.ActiveView), current1);
              this.OnChildViewExporting(e);
              current1.ActiveView = current1.Views[e.ActiveViewIndex];
              break;
            case ChildViewExportMode.ExportAllViews:
              this.TraverseAllChildViews(traverser);
              break;
          }
        }
        if (traverser.Current is GridViewTableHeaderRowInfo || traverser.Current is GridViewDataRowInfo || (traverser.Current is GridViewHierarchyRowInfo || traverser.Current is GridViewGroupRowInfo) || traverser.Current is GridViewSummaryRowInfo)
        {
          GridViewSummaryRowInfo current2 = traverser.Current as GridViewSummaryRowInfo;
          if (current2 == null || this.ShouldExportSummaryRow(current2))
          {
            if (currentRowNum > this.sheetMaxRowsNumber)
            {
              this.spreadExportRenderer.AddWorksheet(this.SheetName);
              currentRowNum = 0;
            }
            if ((this.IsVisibleRow(traverser.Current) || this.hiddenRowOption != HiddenOption.DoNotExport) && (!this.FreezePinnedRows || !traverser.Current.IsPinned || traverser.Current is GridViewTableHeaderRowInfo))
            {
              if (this.ExportViewDefinition && (this.RadGridViewToExport.ViewDefinition is ColumnGroupsViewDefinition || this.RadGridViewToExport.ViewDefinition is HtmlViewDefinition))
              {
                if (traverser.Current is GridViewGroupRowInfo)
                {
                  this.AddGroupRow(traverser.Current as GridViewGroupRowInfo, currentRowNum);
                }
                else
                {
                  int num = this.AddViewDefinitionRow(traverser.Current, rowLayout, currentRowNum);
                  currentRowNum += num;
                }
              }
              else
                this.AddRow(traverser.Current, currentRowNum);
              if (this.ExportChildRowsGrouped)
                this.ProcessCurrentRowGrouping(traverser.Current.HierarchyLevel, currentRowNum);
              ++currentRowNum;
            }
          }
        }
      }
      return currentRowNum;
    }

    private void TraverseAllChildViews(ExportGridTraverser traverser)
    {
      GridViewHierarchyRowInfo current1 = traverser.Current as GridViewHierarchyRowInfo;
      if (this.hierarchyRowsStack.Count == 0)
      {
        current1.ActiveView = current1.Views[0];
        this.hierarchyRowsStack.Push(current1);
      }
      else
      {
        GridViewHierarchyRowInfo hierarchyRowInfo = this.hierarchyRowsStack.Peek();
        if (hierarchyRowInfo.HierarchyLevel == current1.HierarchyLevel)
        {
          if (hierarchyRowInfo.Views.IndexOf(hierarchyRowInfo.ActiveView) == hierarchyRowInfo.Views.Count - 1)
          {
            this.hierarchyRowsStack.Pop();
            current1.ActiveView = current1.Views[0];
            this.hierarchyRowsStack.Push(current1);
          }
          else
          {
            if (!traverser.MoveBackward((GridViewRowInfo) hierarchyRowInfo))
              return;
            GridViewHierarchyRowInfo current2 = traverser.Current as GridViewHierarchyRowInfo;
            current2.ActiveView = current2.Views[current2.Views.IndexOf(current2.ActiveView) + 1];
            traverser.MoveNext();
          }
        }
        else
        {
          if (hierarchyRowInfo.HierarchyLevel >= current1.HierarchyLevel)
            return;
          current1.ActiveView = current1.Views[0];
          this.hierarchyRowsStack.Push(current1);
        }
      }
    }

    private ColumnGroupRowLayout InitializeColumnGroupRowLayout()
    {
      ColumnGroupRowLayout columnGroupRowLayout = new ColumnGroupRowLayout(this.RadGridViewToExport.ViewDefinition as ColumnGroupsViewDefinition);
      columnGroupRowLayout.Initialize(this.RadGridViewToExport.TableElement);
      this.SetupRowLayout((TableViewRowLayoutBase) columnGroupRowLayout);
      return columnGroupRowLayout;
    }

    private HtmlViewRowLayout InitializeHtmlViewRowLayout()
    {
      HtmlViewRowLayout htmlViewRowLayout = new HtmlViewRowLayout(this.RadGridViewToExport.ViewDefinition as HtmlViewDefinition);
      htmlViewRowLayout.Initialize(this.RadGridViewToExport.TableElement);
      this.SetupRowLayout((TableViewRowLayoutBase) htmlViewRowLayout);
      return htmlViewRowLayout;
    }

    private void SetupRowLayout(TableViewRowLayoutBase rowLayout)
    {
      rowLayout.IgnoreColumnVisibility = this.HiddenColumnOption == HiddenOption.ExportAlways;
      rowLayout.Context = GridLayoutContext.Printer;
      this.RadGridViewToExport.BeginUpdate();
      GridViewAutoSizeColumnsMode autoSizeColumnsMode = rowLayout.ViewTemplate.AutoSizeColumnsMode;
      rowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
      rowLayout.MeasureRow(new SizeF((float) this.RadGridViewToExport.Width, (float) this.RadGridViewToExport.Height));
      rowLayout.ViewTemplate.AutoSizeColumnsMode = autoSizeColumnsMode;
      this.RadGridViewToExport.EndUpdate(false);
    }

    private void InitializeRowGroupDataStructures()
    {
      this.hierarchyRowGroups = new Stack<GridViewSpreadExport.RowGroup>();
      this.readyToExportRowGroups = new Stack<GridViewSpreadExport.RowGroup>();
      this.lastRowHierarchyLevel = 0;
    }

    private void ProcessCurrentRowGrouping(int currentRowHierarchyLevel, int currentRowNum)
    {
      if (currentRowHierarchyLevel > this.lastRowHierarchyLevel)
        this.hierarchyRowGroups.Push(new GridViewSpreadExport.RowGroup(currentRowNum, currentRowHierarchyLevel));
      else if (currentRowHierarchyLevel < this.lastRowHierarchyLevel)
      {
        for (int index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0; currentRowHierarchyLevel < index && this.hierarchyRowGroups.Count > 0; index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0)
        {
          GridViewSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
          rowGroup.EndRow = currentRowNum - 1;
          this.readyToExportRowGroups.Push(rowGroup);
        }
      }
      this.lastRowHierarchyLevel = currentRowHierarchyLevel;
    }

    private void GroupWorksheetRows(int currentRowNum)
    {
      while (this.hierarchyRowGroups.Count > 0)
      {
        GridViewSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
        rowGroup.EndRow = currentRowNum - 1;
        this.readyToExportRowGroups.Push(rowGroup);
      }
      while (this.readyToExportRowGroups.Count > 0)
      {
        GridViewSpreadExport.RowGroup rowGroup = this.readyToExportRowGroups.Pop();
        this.spreadExportRenderer.GroupRows(rowGroup.StartRow, rowGroup.EndRow, rowGroup.Level);
      }
    }

    private GridExportState SaveGridState()
    {
      EventDispatcher eventDispatcher = this.RadGridViewToExport.MasterTemplate.EventDispatcher;
      eventDispatcher.SuspendEvent(EventDispatcher.CellValidating);
      eventDispatcher.SuspendEvent(EventDispatcher.CellValidated);
      eventDispatcher.SuspendEvent(EventDispatcher.RowValidating);
      eventDispatcher.SuspendEvent(EventDispatcher.RowValidated);
      eventDispatcher.SuspendEvent(EventDispatcher.SelectionChanging);
      eventDispatcher.SuspendEvent(EventDispatcher.SelectionChanged);
      GridExportState gridExportState = new GridExportState(this.RadGridViewToExport.CurrentRow, this.RadGridViewToExport.TableElement.VScrollBar.Value, this.RadGridViewToExport.TableElement.HScrollBar.Value, this.RadGridViewToExport.EnablePaging, this.RadGridViewToExport.MasterTemplate.PageIndex, this.RadGridViewToExport.MasterTemplate.ShowGroupedColumns);
      foreach (GridViewRowInfo selectedRow in (ReadOnlyCollection<GridViewRowInfo>) this.RadGridViewToExport.SelectedRows)
        gridExportState.SelectedRows.Add(selectedRow);
      foreach (GridViewCellInfo selectedCell in (ReadOnlyCollection<GridViewCellInfo>) this.RadGridViewToExport.SelectedCells)
        gridExportState.SelectedCells.Add(selectedCell);
      this.RadGridViewToExport.CurrentRow = (GridViewRowInfo) null;
      this.RadGridViewToExport.TableElement.SuspendLayout();
      this.RadGridViewToExport.MasterTemplate.ShowGroupedColumns = this.exportGroupedColumns;
      if (this.RadGridViewToExport.EnablePaging && this.pagingExportOption == PagingExportOption.AllPages)
        this.RadGridViewToExport.EnablePaging = false;
      return gridExportState;
    }

    private void RestoreGridState(GridExportState state)
    {
      this.RadGridViewToExport.CurrentRow = state.CurrentRow;
      foreach (GridViewRowInfo selectedRow in (IEnumerable<GridViewRowInfo>) state.SelectedRows)
        selectedRow.IsSelected = true;
      foreach (GridViewCellInfo selectedCell in (IEnumerable<GridViewCellInfo>) state.SelectedCells)
        selectedCell.IsSelected = true;
      if (state.PagingState)
      {
        this.RadGridViewToExport.EnablePaging = true;
        this.RadGridViewToExport.MasterTemplate.MoveToPage(state.CurrentPageIndex);
      }
      this.RadGridViewToExport.MasterTemplate.ShowGroupedColumns = state.ShowGroupedColumns;
      this.RadGridViewToExport.TableElement.ResumeLayout(true, true);
      this.RadGridViewToExport.TableElement.VScrollBar.Value = state.VScrollBarValue;
      this.RadGridViewToExport.TableElement.HScrollBar.Value = state.HScrollBarValue;
      EventDispatcher eventDispatcher = this.RadGridViewToExport.MasterTemplate.EventDispatcher;
      eventDispatcher.ResumeEvent(EventDispatcher.CellValidating);
      eventDispatcher.ResumeEvent(EventDispatcher.CellValidated);
      eventDispatcher.ResumeEvent(EventDispatcher.RowValidating);
      eventDispatcher.ResumeEvent(EventDispatcher.RowValidated);
      eventDispatcher.ResumeEvent(EventDispatcher.SelectionChanging);
      eventDispatcher.ResumeEvent(EventDispatcher.SelectionChanged);
    }

    private bool ShouldExportColumn(GridViewColumn gridColumn)
    {
      return (!gridColumn.IsGrouped || gridColumn.OwnerTemplate.ShowGroupedColumns) && (gridColumn.IsVisible || this.HiddenColumnOption != HiddenOption.DoNotExport);
    }

    private bool ShouldExportSummaryRow(GridViewSummaryRowInfo summaryRow)
    {
      if (this.SummariesExportOption == SummariesOption.ExportAll)
        return true;
      if (this.SummariesExportOption == SummariesOption.DoNotExport)
        return false;
      if (this.SummariesExportOption == SummariesOption.ExportOnlyTop)
        return summaryRow.ViewTemplate.SummaryRowsTop.Contains(summaryRow.SummaryRowItem);
      if (this.SummariesExportOption == SummariesOption.ExportOnlyBottom)
        return summaryRow.ViewTemplate.SummaryRowsBottom.Contains(summaryRow.SummaryRowItem);
      return false;
    }

    private bool IsVisibleRow(GridViewRowInfo gridRowInfo)
    {
      bool isVisible = gridRowInfo.IsVisible;
      if (this.exportHierarchy && isVisible)
      {
        do
        {
          gridRowInfo = gridRowInfo.Parent as GridViewRowInfo;
          if (gridRowInfo != null)
            isVisible &= gridRowInfo.IsVisible;
        }
        while (gridRowInfo != null && isVisible);
      }
      return isVisible;
    }

    private class GroupNode
    {
      private int colSpan;
      private bool foundNode;
      private int levelColSpan;

      public GroupNode(GridViewColumnGroup group)
      {
        this.Group = group;
        this.ChildNodes = new List<GridViewSpreadExport.GroupNode>();
      }

      public GridViewColumnGroup Group { get; private set; }

      public int Level { get; set; }

      public int ColSpan
      {
        get
        {
          if (this.ChildNodes.Count == 0)
            return this.colSpan;
          return this.GetTotalColSpan();
        }
        set
        {
          this.colSpan = value;
        }
      }

      private List<GridViewSpreadExport.GroupNode> ChildNodes { get; set; }

      public void AddChildNode(GridViewSpreadExport.GroupNode child)
      {
        this.ChildNodes.Add(child);
        child.Level = this.Level + 1;
      }

      public int GetTreeDepth()
      {
        int num = 0;
        if (this.ChildNodes.Count == 0)
          return this.Level;
        foreach (GridViewSpreadExport.GroupNode childNode in this.ChildNodes)
        {
          int treeDepth = childNode.GetTreeDepth();
          if (treeDepth > num)
            num = treeDepth;
        }
        return num;
      }

      public int GetTotalColSpan()
      {
        int num = 0;
        if (this.ChildNodes.Count == 0)
        {
          num = this.ColSpan;
        }
        else
        {
          foreach (GridViewSpreadExport.GroupNode childNode in this.ChildNodes)
            num += childNode.GetTotalColSpan();
        }
        return num;
      }

      public GridViewSpreadExport.GroupNode GetChildNode(string text)
      {
        foreach (GridViewSpreadExport.GroupNode childNode1 in this.ChildNodes)
        {
          if (childNode1.Group.Text == text)
            return childNode1;
          GridViewSpreadExport.GroupNode childNode2 = childNode1.GetChildNode(text);
          if (childNode2 != null)
            return childNode2;
        }
        return (GridViewSpreadExport.GroupNode) null;
      }

      public int GetPreviousNodesColSpan(GridViewSpreadExport.GroupNode node)
      {
        this.foundNode = false;
        this.levelColSpan = 0;
        this.CalculateNodeLevelColSpan(node, ref this.foundNode, ref this.levelColSpan);
        return this.levelColSpan;
      }

      private void CalculateNodeLevelColSpan(
        GridViewSpreadExport.GroupNode node,
        ref bool isFoundNode,
        ref int levelColSpanUpToNow)
      {
        if (isFoundNode)
          return;
        if (this == node)
          isFoundNode = true;
        else if (this.Level == node.Level)
        {
          levelColSpanUpToNow += this.ColSpan;
        }
        else
        {
          if (this.ChildNodes.Count == 0)
            levelColSpanUpToNow += this.ColSpan;
          foreach (GridViewSpreadExport.GroupNode childNode in this.ChildNodes)
            childNode.CalculateNodeLevelColSpan(node, ref isFoundNode, ref levelColSpanUpToNow);
        }
      }
    }

    private class PointComparer : IComparer<Point>
    {
      public int Compare(Point x, Point y)
      {
        if (x.Y == y.Y)
          return x.X.CompareTo(y.X);
        return x.Y.CompareTo(y.Y);
      }
    }

    private class RowGroup
    {
      public RowGroup(int startRow, int level)
      {
        this.StartRow = startRow;
        this.Level = level;
      }

      public int StartRow { get; set; }

      public int Level { get; set; }

      public int EndRow { get; set; }
    }

    private delegate void RunExportCallback(string fileName);

    private delegate void RunExportToStreamCallback(Stream exportStream);
  }
}
