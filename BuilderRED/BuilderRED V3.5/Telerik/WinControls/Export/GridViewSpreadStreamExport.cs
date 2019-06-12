// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewSpreadStreamExport
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
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  public class GridViewSpreadStreamExport
  {
    private HiddenOption hiddenColumnOption = HiddenOption.ExportAsHidden;
    private HiddenOption hiddenRowOption = HiddenOption.ExportAsHidden;
    private int sheetMaxRowsNumber = 65536;
    private bool exportGroupedColumns = true;
    private bool exportViewDefinition = true;
    private bool isFirstViewDefinitionRow = true;
    private ISpreadStreamExportRenderer spreadExportRenderer;
    private PagingExportOption pagingExportOption;
    private SpreadStreamExportFormat exportFormat;
    private SummariesOption summariesExportOption;
    private ChildViewExportMode childViewExportMode;
    private bool exportVisualSettings;
    private bool exportHierarchy;
    private bool applicationDoEvents;
    private RowElementProvider rowProvider;
    private CellElementProvider cellProvider;
    private BackgroundWorker worker;
    private Stack<GridViewHierarchyRowInfo> hierarchyRowsStack;
    private bool freezeHeaderRow;
    private bool freezePinnedRows;
    private bool freezePinnedColumns;
    private int pinnedRowsCount;
    private int pinnedColumnsCount;
    private List<int> pinnedColumnsIndicesOrder;
    private int[] rowColumns;
    private List<Point> rowSpanCells;
    private GridViewSpreadStreamExport.GroupNode tree;
    private List<RowDefinition> columnGroupRowDefinitions;

    public GridViewSpreadStreamExport(RadGridView radGridView)
    {
      this.RadGridViewToExport = radGridView;
    }

    public GridViewSpreadStreamExport(
      RadGridView radGridView,
      SpreadStreamExportFormat spreadExportFormat)
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
        if (this.ExportFormat == SpreadStreamExportFormat.Csv)
          return false;
        return this.exportVisualSettings;
      }
      set
      {
        this.exportVisualSettings = value;
      }
    }

    public string SheetName { get; set; }

    [DefaultValue(FileExportMode.NewSheetInExistingFile)]
    public FileExportMode FileExportMode { get; set; }

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

    public SpreadStreamExportFormat ExportFormat
    {
      get
      {
        return this.exportFormat;
      }
      set
      {
        if (!Enum.IsDefined(typeof (SpreadStreamExportFormat), (object) value))
          return;
        this.exportFormat = value;
      }
    }

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
        if (this.ExportFormat == SpreadStreamExportFormat.Xlsx)
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

    public event SpreadStreamCellFormattingEventHandler CellFormatting;

    protected virtual void OnCellFormatting(SpreadStreamCellFormattingEventArgs e)
    {
      if (this.CellFormatting == null)
        return;
      this.CellFormatting((object) this, e);
    }

    public event SpreadStreamRowEventHandler RowCreated;

    protected virtual void OnRowCreated(SpreadStreamRowEventArgs e)
    {
      if (this.RowCreated == null)
        return;
      this.RowCreated((object) this, e);
    }

    public event SpreadStreamRowEventHandler RowExporting;

    protected virtual void OnRowExporting(SpreadStreamRowEventArgs e)
    {
      if (this.RowExporting == null)
        return;
      this.RowExporting((object) this, e);
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

    public void RunExport(string fileName, ISpreadStreamExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      if (this.RadGridViewToExport.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.RadGridViewToExport.Invoke((Delegate) new GridViewSpreadStreamExport.RunExportCallback(this.RunExportCall), (object) fileName);
      }
      else
        this.RunExportCall(fileName);
    }

    public void RunExport(
      string fileName,
      ISpreadStreamExportRenderer exportRenderer,
      string sheetName)
    {
      this.SheetName = sheetName;
      this.RunExport(fileName, exportRenderer);
    }

    public void RunExportAsync(string fileName, ISpreadStreamExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      if (this.GetWorker().IsBusy)
        throw new Exception("There is an export operation that has not yet finished.");
      List<IGridViewSpreadExportRowInfoBase> exportRowInfos = (List<IGridViewSpreadExportRowInfoBase>) null;
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
      ISpreadStreamExportRenderer exportRenderer,
      string sheetName)
    {
      this.SheetName = sheetName;
      this.RunExportAsync(fileName, exportRenderer);
    }

    public void RunExport(Stream exportStream, ISpreadStreamExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      if (this.RadGridViewToExport.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.RadGridViewToExport.Invoke((Delegate) new GridViewSpreadStreamExport.RunExportToStreamCallback(this.RunExportCall), (object) exportStream);
      }
      else
        this.RunExportCall(exportStream);
    }

    public void RunExport(
      Stream exportStream,
      ISpreadStreamExportRenderer exportRenderer,
      string sheetName)
    {
      this.SheetName = sheetName;
      this.RunExport(exportStream, exportRenderer);
    }

    public void RunExportAsync(Stream exportStream, ISpreadStreamExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      if (this.GetWorker().IsBusy)
        throw new Exception("There is an export operation that has not yet finished.");
      List<IGridViewSpreadExportRowInfoBase> exportRowInfos = (List<IGridViewSpreadExportRowInfoBase>) null;
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
      ISpreadStreamExportRenderer exportRenderer,
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
      if (gridData1 != null)
      {
        string path = Path.GetDirectoryName(gridData1.FilePath) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(gridData1.FilePath) + this.spreadExportRenderer.GetFileExtension(this.ExportFormat);
        FileStream fileStream = this.FileExportMode != FileExportMode.NewSheetInExistingFile || !File.Exists(path) || this.ExportFormat != SpreadStreamExportFormat.Xlsx ? new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false) : new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
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
      this.spreadExportRenderer.CreateWorkbook(stream, this.ExportFormat, this.FileExportMode);
      int percentProgress = 0;
      int num1 = 0;
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      this.CreateFreezePanes();
      if (!this.spreadExportRenderer.CallOnWorksheetCreated())
        this.SetColumnWidths();
      for (int index1 = 0; index1 < gridData.ExportRowInfosBase.Count; ++index1)
      {
        if (this.GetWorker().CancellationPending)
        {
          e.Cancel = true;
          return true;
        }
        if (num1 > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          this.spreadExportRenderer.CallOnWorksheetCreated();
          num1 = 0;
        }
        GridViewSpreadStreamExportRowInfo streamExportRowInfo = gridData.ExportRowInfosBase[index1] as GridViewSpreadStreamExportRowInfo;
        this.spreadExportRenderer.CreateRow();
        if (this.ExportChildRowsGrouped)
          this.spreadExportRenderer.GroupCurrentRow(streamExportRowInfo.HierarchyLevel);
        if (streamExportRowInfo.ExportAsHidden)
          this.spreadExportRenderer.SetHiddenRow();
        else
          this.spreadExportRenderer.SetRowHeight(streamExportRowInfo.Height, true);
        object row1 = this.spreadExportRenderer.GetRow(true);
        this.OnRowCreated(new SpreadStreamRowEventArgs(this.spreadExportRenderer.RowIndex, streamExportRowInfo.CellInfos[0].RowType, (GridViewRowInfo) null, row1));
        GridViewSpreadStreamExportGroupRowInfo exportGroupRowInfo = streamExportRowInfo as GridViewSpreadStreamExportGroupRowInfo;
        if (exportGroupRowInfo != null)
        {
          this.spreadExportRenderer.SkipCells(exportGroupRowInfo.IndentCells);
          this.spreadExportRenderer.CreateMergedCells(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex + exportGroupRowInfo.ColumnSpan);
          IGridViewSpreadStreamExportCellInfo cellInfo = exportGroupRowInfo.CellInfos[0];
          if (cellInfo.CellStyleInfo != null)
            cellInfo.CellStyleInfo = this.spreadExportRenderer.CreateCellStyleFromLightStyle(cellInfo.CellStyleInfo);
          else if (this.CellFormatting != null)
          {
            this.spreadExportRenderer.CreateCellStyleFromTheme();
            cellInfo.CellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
          }
          else
          {
            if (!string.IsNullOrEmpty(cellInfo.ExportFormat))
              this.spreadExportRenderer.SetCellFormat(cellInfo.ExportFormat, true);
            this.SetValueForCell(cellInfo.ExportFormatType, cellInfo.Value);
            continue;
          }
          object cell = this.spreadExportRenderer.GetCell();
          SpreadStreamCellFormattingEventArgs e1 = new SpreadStreamCellFormattingEventArgs(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, cellInfo, cell);
          this.OnCellFormatting(e1);
          this.spreadExportRenderer.ApplyCellStyle(e1.CellStyleInfo, e1.ExportCell.ExportFormat);
          this.SetValueForCell(e1.ExportCell.ExportFormatType, e1.ExportCell.Value);
          for (int index2 = 0; index2 < exportGroupRowInfo.ColumnSpan; ++index2)
            this.AddBorderCellCore(e1.CellStyleInfo);
        }
        else
        {
          this.spreadExportRenderer.SkipCells(streamExportRowInfo.IndentCells);
          for (int index2 = 0; index2 < streamExportRowInfo.CellInfos.Count; ++index2)
          {
            int index3 = index2;
            if (this.FreezePinnedColumns && streamExportRowInfo.HierarchyLevel == 0)
              index3 = this.pinnedColumnsIndicesOrder[index3];
            IGridViewSpreadStreamExportCellInfo cellInfo = streamExportRowInfo.CellInfos[index3];
            this.spreadExportRenderer.CreateCell();
            if (cellInfo.CellStyleInfo != null)
              cellInfo.CellStyleInfo = this.spreadExportRenderer.CreateCellStyleFromLightStyle(cellInfo.CellStyleInfo);
            else if (this.CellFormatting != null)
            {
              this.spreadExportRenderer.CreateCellStyleFromTheme();
              cellInfo.CellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
            }
            else
            {
              if (!string.IsNullOrEmpty(cellInfo.ExportFormat))
                this.spreadExportRenderer.SetCellFormat(cellInfo.ExportFormat, true);
              this.SetValueForCell(cellInfo.ExportFormatType, cellInfo.Value);
              continue;
            }
            this.spreadExportRenderer.GetCellStyleInfo();
            object cell = this.spreadExportRenderer.GetCell();
            SpreadStreamCellFormattingEventArgs e1 = new SpreadStreamCellFormattingEventArgs(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, cellInfo, cell);
            this.OnCellFormatting(e1);
            this.spreadExportRenderer.ApplyCellStyle(e1.CellStyleInfo, e1.ExportCell.ExportFormat);
            this.SetValueForCell(e1.ExportCell.ExportFormatType, e1.ExportCell.Value);
          }
          object row2 = this.spreadExportRenderer.GetRow(true);
          this.OnRowExporting(new SpreadStreamRowEventArgs(this.spreadExportRenderer.RowIndex, streamExportRowInfo.CellInfos[0].RowType, (GridViewRowInfo) null, row2));
        }
        int num2 = index1 * 100 / gridData.ExportRowInfosBase.Count;
        if (percentProgress != num2)
        {
          percentProgress = num2;
          this.GetWorker().ReportProgress(percentProgress);
        }
        ++num1;
        gridData.ExportRowInfosBase[index1] = (IGridViewSpreadExportRowInfoBase) null;
      }
      this.spreadExportRenderer.FinishExport();
      return false;
    }

    private List<IGridViewSpreadExportRowInfoBase> GetGridDataSnapshot()
    {
      int visibleColumns = 0;
      this.ProcessPinnedColumns();
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.RadGridViewToExport.Columns)
      {
        if (this.ShouldExportColumn(column))
          ++visibleColumns;
      }
      this.rowProvider = (RowElementProvider) this.RadGridViewToExport.TableElement.RowElementProvider;
      this.cellProvider = (CellElementProvider) this.RadGridViewToExport.TableElement.CellElementProvider;
      List<IGridViewSpreadExportRowInfoBase> exportRowInfos = new List<IGridViewSpreadExportRowInfoBase>();
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
      List<IGridViewSpreadExportRowInfoBase> exportRowInfos,
      int visibleColumns,
      int currentIndent)
    {
      while (traverser.MoveNext())
      {
        GridViewRowInfo current = traverser.Current;
        System.Type type = current.GetType();
        if ((object) type == (object) typeof (GridViewTableHeaderRowInfo) || (object) type == (object) typeof (GridViewDataRowInfo) || ((object) type == (object) typeof (GridViewHierarchyRowInfo) || (object) type == (object) typeof (GridViewGroupRowInfo)) || (object) type == (object) typeof (GridViewSummaryRowInfo))
        {
          bool flag = this.IsVisibleRow(current);
          if (flag || this.HiddenRowOption != HiddenOption.DoNotExport)
          {
            bool exportAsHidden = !flag && this.HiddenRowOption == HiddenOption.ExportAsHidden;
            if (this.ExportHierarchy)
            {
              GridViewHierarchyRowInfo row = current as GridViewHierarchyRowInfo;
              if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0 && current is GridViewGroupRowInfo)
              {
                GridViewHierarchyRowInfo hierarchyRowInfo = this.hierarchyRowsStack.Peek();
                if (hierarchyRowInfo.Views.Count > 1 && hierarchyRowInfo.Views.IndexOf(hierarchyRowInfo.ActiveView) != hierarchyRowInfo.Views.Count - 1)
                  continue;
              }
              if (row != null && row.Views.Count > 1)
              {
                switch (this.ChildViewExportMode)
                {
                  case ChildViewExportMode.ExportFirstView:
                    row.ActiveView = row.Views[0];
                    break;
                  case ChildViewExportMode.SelectViewToExport:
                    ChildViewExportingEventArgs e = new ChildViewExportingEventArgs(row.Views.IndexOf(row.ActiveView), row);
                    this.OnChildViewExporting(e);
                    row.ActiveView = row.Views[e.ActiveViewIndex];
                    break;
                  case ChildViewExportMode.ExportAllViews:
                    this.TraverseAllChildViews(traverser);
                    current = traverser.Current;
                    break;
                }
              }
            }
            double num = (double) this.RadGridViewToExport.TableElement.RowScroller.ElementProvider.GetElementSize(current).Height;
            GridRowElement gridRowElement = (GridRowElement) null;
            List<GridCellElement> gridCellElementList = new List<GridCellElement>();
            if (this.ExportVisualSettings)
            {
              gridRowElement = (GridRowElement) this.rowProvider.GetElement(current, (object) null);
              gridRowElement.InitializeRowView(this.RadGridViewToExport.TableElement);
              this.RadGridViewToExport.TableElement.Children.Add((RadElement) gridRowElement);
              gridRowElement.Initialize(current);
              gridRowElement.SuspendLayout();
              for (int index = 0; index < gridRowElement.RowInfo.Cells.Count; ++index)
              {
                GridCellElement element = (GridCellElement) this.cellProvider.GetElement((GridViewColumn) gridRowElement.RowInfo.ViewTemplate.Columns[index], (object) gridRowElement);
                gridRowElement.Children.Add((RadElement) element);
                element.Initialize((GridViewColumn) gridRowElement.RowInfo.ViewTemplate.Columns[index], gridRowElement);
                gridCellElementList.Add(element);
                if (this.RadGridViewToExport.AutoSizeRows)
                  num = Math.Max(num, (double) GridExportUtils.GetCellDesiredSize(element).Height);
              }
            }
            int rowIndex = 0;
            if (this.CellFormatting != null)
              rowIndex = current.Index;
            GridViewGroupRowInfo viewGroupRowInfo = current as GridViewGroupRowInfo;
            if (viewGroupRowInfo != null)
            {
              GridGroupHeaderRowElement headerRowElement = (GridGroupHeaderRowElement) null;
              ISpreadStreamExportCellStyleInfo cellStyleInfo = (ISpreadStreamExportCellStyleInfo) null;
              string empty = string.Empty;
              string str;
              if (this.ExportVisualSettings)
              {
                headerRowElement = (GridGroupHeaderRowElement) this.rowProvider.CreateElement((GridViewRowInfo) viewGroupRowInfo, (object) null);
                headerRowElement.InitializeRowView(this.RadGridViewToExport.TableElement);
                this.RadGridViewToExport.TableElement.Children.Add((RadElement) headerRowElement);
                headerRowElement.Initialize((GridViewRowInfo) viewGroupRowInfo);
                headerRowElement.UpdateInfo();
                GridGroupContentCellElement contentCell = headerRowElement.ContentCell;
                this.spreadExportRenderer.GetCellFormat(true);
                this.spreadExportRenderer.CreateLightCellStyle(GridExportUtils.GetBackColor((LightVisualElement) contentCell), contentCell.ForeColor, contentCell.Font.FontFamily, (double) contentCell.Font.Size, contentCell.Font.Bold, contentCell.Font.Italic, contentCell.Font.Underline, contentCell.TextAlignment, contentCell.TextWrap, contentCell.BorderBoxStyle, contentCell.BorderColor, contentCell.BorderTopColor, contentCell.BorderBottomColor, contentCell.BorderRightColor, contentCell.BorderLeftColor);
                cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
                str = contentCell.Text;
              }
              else
              {
                str = viewGroupRowInfo.HeaderText;
                if (viewGroupRowInfo.ViewTemplate.SummaryRowGroupHeaders.Count > 0)
                  str = str + " | " + viewGroupRowInfo.GetSummary();
              }
              currentIndent = viewGroupRowInfo.GroupLevel;
              int columnSpan = visibleColumns - currentIndent + this.RadGridViewToExport.GroupDescriptors.Count - 1;
              List<IGridViewSpreadStreamExportCellInfo> streamExportCellInfoList = new List<IGridViewSpreadStreamExportCellInfo>(1);
              streamExportCellInfoList.Add((IGridViewSpreadStreamExportCellInfo) new GridViewSpreadStreamExportCellInfo(viewGroupRowInfo.GetType(), viewGroupRowInfo.Index, (System.Type) null, 0, (object) str, "@", DisplayFormatType.Text, cellStyleInfo));
              GridViewSpreadStreamExportGroupRowInfo exportGroupRowInfo = !this.ExportChildRowsGrouped ? new GridViewSpreadStreamExportGroupRowInfo((IList<IGridViewSpreadStreamExportCellInfo>) streamExportCellInfoList, currentIndent, exportAsHidden, 0, num, columnSpan) : new GridViewSpreadStreamExportGroupRowInfo((IList<IGridViewSpreadStreamExportCellInfo>) streamExportCellInfoList, currentIndent, exportAsHidden, viewGroupRowInfo.HierarchyLevel, num, columnSpan);
              exportRowInfos.Add((IGridViewSpreadExportRowInfoBase) exportGroupRowInfo);
              currentIndent = viewGroupRowInfo.GroupLevel + 1;
              if (this.ExportVisualSettings)
                GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, (GridRowElement) headerRowElement, false);
            }
            else
            {
              GridViewSummaryRowInfo summaryRow = current as GridViewSummaryRowInfo;
              if (summaryRow == null || this.ShouldExportSummaryRow(summaryRow))
              {
                List<IGridViewSpreadStreamExportCellInfo> streamExportCellInfoList = new List<IGridViewSpreadStreamExportCellInfo>();
                foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) current.ViewTemplate.Columns)
                {
                  int index = column.Index;
                  if (this.ShouldExportColumn((GridViewColumn) column))
                  {
                    object objectCellValue = this.GetObjectCellValue(current.Cells[index]);
                    string empty = string.Empty;
                    DisplayFormatType exportType = DisplayFormatType.None;
                    this.ExtractCellFormatAndExportType(column, current, objectCellValue, ref empty, ref exportType);
                    ISpreadStreamExportCellStyleInfo cellStyleInfo = (ISpreadStreamExportCellStyleInfo) null;
                    if (this.ExportVisualSettings)
                    {
                      GridCellElement gridCellElement = gridCellElementList[index];
                      ContentAlignment textAlignment = this.RadGridViewToExport.RightToLeft == RightToLeft.No ? gridCellElement.TextAlignment : GridExportUtils.ConvertToRightToLeftAlignment(gridCellElement.TextAlignment);
                      this.spreadExportRenderer.GetCellFormat(true);
                      this.spreadExportRenderer.CreateLightCellStyle(GridExportUtils.GetBackColor((LightVisualElement) gridCellElement), gridCellElement.ForeColor, gridCellElement.Font.FontFamily, (double) gridCellElement.Font.Size, gridCellElement.Font.Bold, gridCellElement.Font.Italic, gridCellElement.Font.Underline, textAlignment, gridCellElement.TextWrap, gridCellElement.BorderBoxStyle, gridCellElement.BorderColor, gridCellElement.BorderTopColor, gridCellElement.BorderBottomColor, gridCellElement.BorderRightColor, gridCellElement.BorderLeftColor);
                      cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
                    }
                    streamExportCellInfoList.Add((IGridViewSpreadStreamExportCellInfo) new GridViewSpreadStreamExportCellInfo(type, rowIndex, column.GetType(), index, objectCellValue, empty, exportType, cellStyleInfo));
                  }
                  if (this.ExportVisualSettings)
                    GridExportUtils.ReleaseCellElement(this.cellProvider, gridRowElement, gridCellElementList[index], false);
                }
                GridViewSpreadStreamExportRowInfo streamExportRowInfo = !this.ExportChildRowsGrouped ? new GridViewSpreadStreamExportRowInfo((IList<IGridViewSpreadStreamExportCellInfo>) streamExportCellInfoList, currentIndent, exportAsHidden, 0, num) : new GridViewSpreadStreamExportRowInfo((IList<IGridViewSpreadStreamExportCellInfo>) streamExportCellInfoList, currentIndent, exportAsHidden, current.HierarchyLevel, num);
                if ((object) type == (object) typeof (GridViewTableHeaderRowInfo))
                  streamExportRowInfo.IndentCells = this.RadGridViewToExport.GroupDescriptors.Count;
                exportRowInfos.Add((IGridViewSpreadExportRowInfoBase) streamExportRowInfo);
                if (this.ExportVisualSettings)
                  GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, gridRowElement, false);
              }
            }
          }
        }
      }
      return currentIndent;
    }

    private void ExtractCellFormatAndExportType(
      GridViewDataColumn col,
      GridViewRowInfo rowInfo,
      object value,
      ref string cellFormat,
      ref DisplayFormatType exportType)
    {
      if (rowInfo is GridViewDataRowInfo)
      {
        cellFormat = this.GetCellValueFormat(col);
        exportType = col.ExcelExportType;
        if (exportType != DisplayFormatType.Custom)
          return;
        if (TelerikHelper.IsNumericType(col.DataType))
        {
          exportType = DisplayFormatType.Fixed;
        }
        else
        {
          if ((object) col.DataType != (object) typeof (DateTime))
            return;
          exportType = DisplayFormatType.GeneralDate;
        }
      }
      else
      {
        if (value == null)
          return;
        if (rowInfo is GridViewTableHeaderRowInfo)
          cellFormat = "@";
        exportType = DisplayFormatType.Text;
      }
    }

    private bool CheckDateTimeValue(DateTime value)
    {
      bool flag = true;
      DateTime dateTime = new DateTime(1900, 1, 1);
      if (value < dateTime)
        flag = false;
      return flag;
    }

    internal void SetValueForCell(DisplayFormatType displayFormatType, object value)
    {
      switch (displayFormatType)
      {
        case DisplayFormatType.None:
        case DisplayFormatType.Text:
          this.spreadExportRenderer.SetCellValue(DataType.String, value);
          break;
        case DisplayFormatType.Fixed:
        case DisplayFormatType.Standard:
        case DisplayFormatType.Percent:
        case DisplayFormatType.Scientific:
        case DisplayFormatType.Currency:
          this.spreadExportRenderer.SetCellValue(DataType.Number, value);
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
          this.spreadExportRenderer.SetCellValue(DataType.DateTime, value);
          break;
        default:
          this.spreadExportRenderer.SetCellValue(DataType.Other, value);
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
      this.spreadExportRenderer.CreateRow();
      if (!this.IsVisibleRow(gridViewRowInfo) && this.hiddenRowOption == HiddenOption.ExportAsHidden)
        this.spreadExportRenderer.SetHiddenRow();
      else if (this.ExportVisualSettings)
        this.spreadExportRenderer.SetRowHeight((double) (int) GridExportUtils.GetRowHeight(this.RadGridViewToExport, this.rowProvider, this.cellProvider, gridViewRowInfo, true), true);
      if (this.ExportChildRowsGrouped && gridViewRowInfo.HierarchyLevel > 0)
        this.spreadExportRenderer.GroupCurrentRow(gridViewRowInfo.HierarchyLevel);
      object row1 = this.spreadExportRenderer.GetRow(true);
      this.OnRowCreated(new SpreadStreamRowEventArgs(this.spreadExportRenderer.RowIndex, gridViewRowInfo.GetType(), gridViewRowInfo, row1));
      if (gridViewRowInfo is GridViewGroupRowInfo)
      {
        this.AddGroupRow(gridViewRowInfo as GridViewGroupRowInfo, this.spreadExportRenderer.RowIndex);
      }
      else
      {
        List<GridCellElement> gridCellElementList = new List<GridCellElement>();
        GridRowElement gridRowElement = (GridRowElement) null;
        if (this.ExportVisualSettings)
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
        this.spreadExportRenderer.SkipCells(this.RadGridViewToExport.GroupDescriptors.Count);
        System.Type type = gridViewRowInfo.GetType();
        int rowIndex1 = 0;
        if (this.CellFormatting != null)
          rowIndex1 = gridViewRowInfo.Index;
        for (int index1 = 0; index1 < gridViewRowInfo.Cells.Count; ++index1)
        {
          int index2 = index1;
          if (this.FreezePinnedColumns && gridViewRowInfo.HierarchyLevel == 0)
            index2 = this.pinnedColumnsIndicesOrder[index2];
          GridViewDataColumn columnInfo = gridViewRowInfo.Cells[index2].ColumnInfo as GridViewDataColumn;
          if (columnInfo == null || !this.ShouldExportColumn((GridViewColumn) columnInfo))
          {
            if (this.ExportVisualSettings)
              GridExportUtils.ReleaseCellElement(this.cellProvider, gridRowElement, gridCellElementList[index2]);
          }
          else
          {
            object objectCellValue = this.GetObjectCellValue(gridViewRowInfo.Cells[index2]);
            string empty = string.Empty;
            DisplayFormatType exportType = DisplayFormatType.None;
            this.spreadExportRenderer.CreateCell();
            this.ExtractCellFormatAndExportType(columnInfo, gridViewRowInfo, objectCellValue, ref empty, ref exportType);
            if (this.ExportVisualSettings)
            {
              GridCellElement gridCellElement = gridCellElementList[index2];
              ContentAlignment textAlignment = this.RadGridViewToExport.RightToLeft == RightToLeft.No ? gridCellElement.TextAlignment : GridExportUtils.ConvertToRightToLeftAlignment(gridCellElement.TextAlignment);
              this.spreadExportRenderer.CreateCellStyle(GridExportUtils.GetBackColor((LightVisualElement) gridCellElement), gridCellElement.ForeColor, gridCellElement.Font.FontFamily, (double) gridCellElement.Font.Size, gridCellElement.Font.Bold, gridCellElement.Font.Italic, gridCellElement.Font.Underline, textAlignment, gridCellElement.TextWrap, gridCellElement.BorderBoxStyle, gridCellElement.BorderColor, gridCellElement.BorderTopColor, gridCellElement.BorderBottomColor, gridCellElement.BorderRightColor, gridCellElement.BorderLeftColor);
              GridExportUtils.ReleaseCellElement(this.cellProvider, gridRowElement, gridCellElementList[index2]);
            }
            else if (this.CellFormatting != null)
            {
              this.spreadExportRenderer.CreateCellStyleFromTheme();
            }
            else
            {
              if (!string.IsNullOrEmpty(empty))
                this.spreadExportRenderer.SetCellFormat(empty, true);
              this.SetValueForCell(exportType, objectCellValue);
              continue;
            }
            ISpreadStreamExportCellStyleInfo cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
            SpreadStreamCellFormattingEventArgs e = new SpreadStreamCellFormattingEventArgs(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, (IGridViewSpreadStreamExportCellInfo) new GridViewSpreadStreamExportCellInfo(type, rowIndex1, columnInfo.GetType(), columnInfo.Index, objectCellValue, empty, exportType, cellStyleInfo), this.spreadExportRenderer.GetCell());
            this.OnCellFormatting(e);
            this.spreadExportRenderer.ApplyCellStyle(e.CellStyleInfo, e.ExportCell.ExportFormat);
            this.SetValueForCell(e.ExportCell.ExportFormatType, e.ExportCell.Value);
          }
        }
        if (this.ExportVisualSettings)
          GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, gridRowElement);
      }
      object row2 = this.spreadExportRenderer.GetRow(true);
      this.OnRowExporting(new SpreadStreamRowEventArgs(this.spreadExportRenderer.RowIndex, gridViewRowInfo.GetType(), gridViewRowInfo, row2));
    }

    private void AddGroupRow(GridViewGroupRowInfo gridViewGroupRowInfo, int rowIndex)
    {
      GridGroupHeaderRowElement element = (GridGroupHeaderRowElement) this.rowProvider.CreateElement((GridViewRowInfo) gridViewGroupRowInfo, (object) null);
      element.InitializeRowView(this.RadGridViewToExport.TableElement);
      this.RadGridViewToExport.TableElement.Children.Add((RadElement) element);
      element.Initialize((GridViewRowInfo) gridViewGroupRowInfo);
      element.UpdateInfo();
      int groupLevel = gridViewGroupRowInfo.GroupLevel;
      this.spreadExportRenderer.SkipCells(groupLevel);
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
      this.spreadExportRenderer.CreateMergedCells(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex + num2);
      string text = element.ContentCell.Text;
      string str = "@";
      DisplayFormatType exportFormatType = DisplayFormatType.Text;
      if (this.ExportVisualSettings)
      {
        GridGroupContentCellElement contentCell = element.ContentCell;
        this.spreadExportRenderer.CreateCellStyle(GridExportUtils.GetBackColor((LightVisualElement) contentCell), contentCell.ForeColor, contentCell.Font.FontFamily, (double) contentCell.Font.Size, contentCell.Font.Bold, contentCell.Font.Italic, contentCell.Font.Underline, contentCell.TextAlignment, contentCell.TextWrap, contentCell.BorderBoxStyle, contentCell.BorderColor, contentCell.BorderTopColor, contentCell.BorderBottomColor, contentCell.BorderRightColor, contentCell.BorderLeftColor);
      }
      else if (this.CellFormatting != null)
      {
        this.spreadExportRenderer.CreateCellStyleFromTheme();
      }
      else
      {
        this.spreadExportRenderer.SetCellValue(text);
        this.spreadExportRenderer.SetCellFormat(str, true);
        GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, (GridRowElement) element);
        return;
      }
      ISpreadStreamExportCellStyleInfo cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
      SpreadStreamCellFormattingEventArgs e = new SpreadStreamCellFormattingEventArgs(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, (IGridViewSpreadStreamExportCellInfo) new GridViewSpreadStreamExportCellInfo(gridViewGroupRowInfo.GetType(), gridViewGroupRowInfo.Index, (System.Type) null, 0, (object) text, str, exportFormatType, cellStyleInfo), this.spreadExportRenderer.GetCell());
      this.OnCellFormatting(e);
      this.spreadExportRenderer.ApplyCellStyle(e.CellStyleInfo, e.ExportCell.ExportFormat);
      this.SetValueForCell(e.ExportCell.ExportFormatType, e.ExportCell.Value);
      for (int index = 0; index < num2; ++index)
        this.AddBorderCellCore(e.CellStyleInfo);
      if (!this.ExportVisualSettings)
        return;
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
      if (this.ExportVisualSettings)
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
      int num2 = num1;
      int columnIndex = count;
      IDictionary<int, GridViewSpreadStreamExport.ViewDefinitionExportRow> dictionary = (IDictionary<int, GridViewSpreadStreamExport.ViewDefinitionExportRow>) new SortedDictionary<int, GridViewSpreadStreamExport.ViewDefinitionExportRow>();
      IList<Point> pointList = (IList<Point>) new List<Point>();
      System.Type type = gridViewRowInfo.GetType();
      if (this.CellFormatting != null)
      {
        int index1 = gridViewRowInfo.Index;
      }
      for (int index2 = 0; index2 < rowLayout.RenderColumns.Count; ++index2)
      {
        GridViewColumn renderColumn = rowLayout.RenderColumns[index2];
        CellDefinition cellDefinition = (CellDefinition) null;
        int index3 = 0;
        if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
        {
          GridViewSpreadStreamExport.ViewDefinitionExportCell viewDefinitionCell = new GridViewSpreadStreamExport.ViewDefinitionExportCell();
          if (columnGroupRowLayout != null)
          {
            rectangleF = columnGroupRowLayout.GetCorrectedColumnBounds(gridViewRowInfo, renderColumn, false, new RectangleF(0.0f, 0.0f, rowLayout.DesiredSize.Width, rowLayout.DesiredSize.Height));
            if (renderColumn.Index >= 0)
            {
              for (int index4 = 0; index4 < this.columnGroupRowDefinitions.Count; ++index4)
              {
                for (int index5 = 0; index5 < this.columnGroupRowDefinitions[index4].Cells.Count; ++index5)
                {
                  if (this.columnGroupRowDefinitions[index4].Cells[index5].UniqueName == renderColumn.Name)
                  {
                    cellDefinition = this.columnGroupRowDefinitions[index4].Cells[index5];
                    num1 = rowIndex + index4;
                    index3 = index4;
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
              index3 = cellDefinition.RowIndex;
            }
            else
              continue;
          }
          if (!(rectangleF == RectangleF.Empty))
          {
            object empty = (object) string.Empty;
            string exportFormat = string.Empty;
            DisplayFormatType exportFormatType = DisplayFormatType.None;
            object obj;
            if (gridViewRowInfo is GridViewTableHeaderRowInfo)
            {
              GridViewCellInfo cell = this.RadGridViewToExport.MasterView.TableHeaderRow.Cells[renderColumn.Name];
              obj = (object) renderColumn.HeaderText;
              exportFormatType = DisplayFormatType.Text;
            }
            else if (gridViewRowInfo is GridViewSummaryRowInfo)
            {
              GridViewCellInfo cell = (gridViewRowInfo as GridViewSummaryRowInfo).Cells[renderColumn.Name];
              if (cell != null)
              {
                obj = cell.Value != null ? (object) cell.Value.ToString() : (object) string.Empty;
                exportFormatType = DisplayFormatType.Text;
              }
              else
                continue;
            }
            else
            {
              GridViewCellInfo cell = gridViewRowInfo.Cells[renderColumn.Name];
              if (cell != null)
              {
                GridViewColumn columnInfo1 = cell.ColumnInfo;
                object lookupValue = cell.Value;
                GridViewComboBoxColumn columnInfo2 = cell.ColumnInfo as GridViewComboBoxColumn;
                if (columnInfo2 != null && columnInfo2.HasLookupValue)
                  lookupValue = columnInfo2.GetLookupValue(lookupValue);
                obj = lookupValue ?? (object) string.Empty;
              }
              else
                continue;
            }
            GridViewSpreadStreamExport.ViewDefinitionExportRow definitionExportRow;
            if (cellDefinition != null)
            {
              columnIndex = count + cellDefinition.ColumnIndex;
              foreach (Point rowSpanCell in this.rowSpanCells)
              {
                if (rowSpanCell.Y == index3 && rowSpanCell.X == this.rowColumns[index3])
                  ++columnIndex;
              }
              num2 = num1 + cellDefinition.RowSpan - 1;
              int num3 = columnIndex + cellDefinition.ColSpan - 1;
              if (this.isFirstViewDefinitionRow && columnGroupRowLayout != null)
              {
                int treeDepth = this.tree.GetTreeDepth();
                int num4 = num1 + treeDepth;
                num2 += treeDepth;
                int num5 = 0;
                for (int index4 = num4 - 1; index4 >= 0; --index4)
                {
                  bool flag = true;
                  foreach (Point point in (IEnumerable<Point>) pointList)
                  {
                    if (point.X == index4 && point.Y == columnIndex)
                    {
                      flag = false;
                      break;
                    }
                  }
                  if (flag || pointList.Count == 0)
                    ++num5;
                }
                num1 = num4 - num5;
              }
              viewDefinitionCell.RowSpan = num2 - num1 + 1;
              viewDefinitionCell.ColSpan = num3 - columnIndex + 1;
              definitionExportRow = this.GetViewDefinitionExportRow(num1, dictionary);
            }
            else
            {
              int num3 = 0;
              GridViewSpreadStreamExport.GroupNode childNode = this.tree.GetChildNode(obj.ToString());
              if (childNode != null)
              {
                num1 = rowIndex + childNode.Level - 1;
                int previousNodesColSpan = this.tree.GetPreviousNodesColSpan(childNode);
                columnIndex = count + previousNodesColSpan;
                num3 = childNode.ColSpan - 1;
              }
              num2 = num1;
              int num4 = num3 + columnIndex;
              viewDefinitionCell.RowSpan = num2 - num1 + 1;
              viewDefinitionCell.ColSpan = num4 - columnIndex + 1;
              definitionExportRow = this.GetViewDefinitionExportRow(num1, dictionary);
            }
            if (this.isFirstViewDefinitionRow)
            {
              for (int x = num1; x < num1 + viewDefinitionCell.RowSpan; ++x)
              {
                for (int y = columnIndex; y < columnIndex + viewDefinitionCell.ColSpan; ++y)
                  pointList.Add(new Point(x, y));
              }
            }
            int index5 = renderColumn.Index;
            if (gridViewRowInfo is GridViewDataRowInfo)
            {
              exportFormat = this.GetCellValueFormat(renderColumn as GridViewDataColumn);
              exportFormatType = gridViewRowInfo.ViewTemplate.Columns[index5].ExcelExportType;
            }
            ISpreadStreamExportCellStyleInfo cellStyleInfo = (ISpreadStreamExportCellStyleInfo) null;
            if (this.ExportVisualSettings)
            {
              this.InitializeVirtualCellElement(gridRowElement, renderColumn);
              cellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
            }
            viewDefinitionCell.ExportCellInfo = (IGridViewSpreadStreamExportCellInfo) new GridViewSpreadStreamExportCellInfo(type, rowIndex, renderColumn.GetType(), index5, obj, exportFormat, exportFormatType, cellStyleInfo);
            definitionExportRow.Cells.Add(viewDefinitionCell);
            this.AddDefinitionRowBorderCells(num1, dictionary, viewDefinitionCell, columnIndex);
          }
        }
      }
      if (this.ExportVisualSettings)
        GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, gridRowElement);
      foreach (KeyValuePair<int, GridViewSpreadStreamExport.ViewDefinitionExportRow> kvp in (IEnumerable<KeyValuePair<int, GridViewSpreadStreamExport.ViewDefinitionExportRow>>) dictionary)
      {
        this.spreadExportRenderer.CreateRow();
        object row1 = this.spreadExportRenderer.GetRow(true);
        this.OnRowCreated(new SpreadStreamRowEventArgs(this.spreadExportRenderer.RowIndex, gridViewRowInfo.RowElementType, gridViewRowInfo, row1));
        foreach (GridViewSpreadStreamExport.ViewDefinitionExportCell cell1 in (IEnumerable<GridViewSpreadStreamExport.ViewDefinitionExportCell>) kvp.Value.Cells)
        {
          while (this.spreadExportRenderer.GetIsMerged(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex))
            this.AddBorderCell(kvp);
          if (cell1.RowSpan > 1 || cell1.ColSpan > 1)
            this.spreadExportRenderer.CreateMergedCells(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, this.spreadExportRenderer.RowIndex + cell1.RowSpan - 1, this.spreadExportRenderer.ColumnIndex + cell1.ColSpan - 1);
          else
            this.spreadExportRenderer.CreateCell();
          IGridViewSpreadStreamExportCellInfo exportCellInfo = cell1.ExportCellInfo;
          if (exportCellInfo.CellStyleInfo == null)
          {
            if (this.CellFormatting != null)
            {
              this.spreadExportRenderer.CreateCellStyleFromTheme();
              exportCellInfo.CellStyleInfo = this.spreadExportRenderer.GetCellStyleInfo();
            }
            else
            {
              if (!string.IsNullOrEmpty(exportCellInfo.ExportFormat))
                this.spreadExportRenderer.SetCellFormat(exportCellInfo.ExportFormat, true);
              this.SetValueForCell(exportCellInfo.ExportFormatType, exportCellInfo.Value);
              this.spreadExportRenderer.FinishCell();
              continue;
            }
          }
          object cell2 = this.spreadExportRenderer.GetCell();
          SpreadStreamCellFormattingEventArgs e = new SpreadStreamCellFormattingEventArgs(this.spreadExportRenderer.RowIndex, this.spreadExportRenderer.ColumnIndex, exportCellInfo, cell2);
          this.OnCellFormatting(e);
          this.spreadExportRenderer.ApplyCellStyle(e.CellStyleInfo, e.ExportCell.ExportFormat);
          this.SetValueForCell(e.ExportCell.ExportFormatType, e.ExportCell.Value);
          this.spreadExportRenderer.FinishCell();
          cell1.ExportCellInfo.CellStyleInfo = this.spreadExportRenderer.GetBordersFromExistingStyle(e.CellStyleInfo);
        }
        while (kvp.Value.BorderCells.Count > 0)
          this.AddBorderCell(kvp);
        object row2 = this.spreadExportRenderer.GetRow(true);
        this.OnRowExporting(new SpreadStreamRowEventArgs(this.spreadExportRenderer.RowIndex, gridViewRowInfo.GetType(), gridViewRowInfo, row2));
      }
      this.isFirstViewDefinitionRow = false;
      return num2 - rowIndex;
    }

    private void AddBorderCell(
      KeyValuePair<int, GridViewSpreadStreamExport.ViewDefinitionExportRow> kvp)
    {
      int key = -1;
      GridViewSpreadStreamExport.ViewDefinitionBorderCell definitionBorderCell = (GridViewSpreadStreamExport.ViewDefinitionBorderCell) null;
      using (IEnumerator<KeyValuePair<int, GridViewSpreadStreamExport.ViewDefinitionBorderCell>> enumerator = kvp.Value.BorderCells.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          KeyValuePair<int, GridViewSpreadStreamExport.ViewDefinitionBorderCell> current = enumerator.Current;
          key = current.Key;
          definitionBorderCell = current.Value;
        }
      }
      kvp.Value.BorderCells.Remove(key);
      this.AddBorderCellCore(definitionBorderCell.TopLeftCell.ExportCellInfo.CellStyleInfo);
      definitionBorderCell.TopLeftCell = (GridViewSpreadStreamExport.ViewDefinitionExportCell) null;
    }

    private void AddBorderCellCore(ISpreadStreamExportCellStyleInfo cellStyleInfo)
    {
      this.spreadExportRenderer.CreateCell();
      if (cellStyleInfo != null)
      {
        this.spreadExportRenderer.CreateBorderCellStyle(cellStyleInfo);
        this.spreadExportRenderer.ApplyCellStyle(this.spreadExportRenderer.GetCellStyleInfo(), string.Empty);
      }
      this.spreadExportRenderer.FinishCell();
    }

    private void AddDefinitionRowBorderCells(
      int currentRowIndex,
      IDictionary<int, GridViewSpreadStreamExport.ViewDefinitionExportRow> viewDefinitionRows,
      GridViewSpreadStreamExport.ViewDefinitionExportCell viewDefinitionCell,
      int columnIndex)
    {
      if (viewDefinitionCell.RowSpan > 1)
      {
        for (int index = 0; index < viewDefinitionCell.RowSpan; ++index)
        {
          int columnIndex1 = columnIndex;
          GridViewSpreadStreamExport.ViewDefinitionExportRow definitionExportRow = this.GetViewDefinitionExportRow(currentRowIndex + index, viewDefinitionRows);
          int colSpan = viewDefinitionCell.ColSpan;
          if (index == 0)
          {
            --colSpan;
            ++columnIndex1;
          }
          this.AddDefinitionColumnBorderCells(definitionExportRow, viewDefinitionCell, colSpan, columnIndex1);
        }
      }
      else
      {
        if (viewDefinitionCell.ColSpan <= 1)
          return;
        this.AddDefinitionColumnBorderCells(this.GetViewDefinitionExportRow(currentRowIndex, viewDefinitionRows), viewDefinitionCell, viewDefinitionCell.ColSpan - 1, columnIndex + 1);
      }
    }

    private void AddDefinitionColumnBorderCells(
      GridViewSpreadStreamExport.ViewDefinitionExportRow currentRow,
      GridViewSpreadStreamExport.ViewDefinitionExportCell viewDefinitionCell,
      int cellsCount,
      int columnIndex)
    {
      if (cellsCount <= 0)
        return;
      for (int index = 0; index < cellsCount; ++index)
      {
        int key = columnIndex + index;
        GridViewSpreadStreamExport.ViewDefinitionBorderCell definitionBorderCell = new GridViewSpreadStreamExport.ViewDefinitionBorderCell(viewDefinitionCell);
        currentRow.BorderCells.Add(key, definitionBorderCell);
      }
    }

    private GridViewSpreadStreamExport.ViewDefinitionExportRow GetViewDefinitionExportRow(
      int index,
      IDictionary<int, GridViewSpreadStreamExport.ViewDefinitionExportRow> definitionExportRows)
    {
      if (!definitionExportRows.ContainsKey(index))
        definitionExportRows.Add(index, new GridViewSpreadStreamExport.ViewDefinitionExportRow());
      return definitionExportRows[index];
    }

    private void CreateRowSpanCells(ColumnGroupRowLayout columnGroupRowLayout)
    {
      this.tree = new GridViewSpreadStreamExport.GroupNode((GridViewColumnGroup) null);
      this.tree.Level = 0;
      this.tree.ColSpan = -1;
      this.columnGroupRowDefinitions = new List<RowDefinition>();
      foreach (GridViewColumnGroup columnGroup in (Collection<GridViewColumnGroup>) columnGroupRowLayout.ViewDefinition.ColumnGroups)
      {
        GridViewSpreadStreamExport.GroupNode groupNode = new GridViewSpreadStreamExport.GroupNode(columnGroup);
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
                this.rowSpanCells.Insert(~this.rowSpanCells.BinarySearch(point, (IComparer<Point>) new GridViewSpreadStreamExport.PointComparer()), point);
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
      GridViewSpreadStreamExport.GroupNode node)
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
            GridViewSpreadStreamExport.GroupNode groupNode = new GridViewSpreadStreamExport.GroupNode(group1);
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
                this.rowSpanCells.Insert(~this.rowSpanCells.BinarySearch(point, (IComparer<Point>) new GridViewSpreadStreamExport.PointComparer()), point);
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
      this.spreadExportRenderer.CreateCellStyle(GridExportUtils.GetBackColor((LightVisualElement) element), element.ForeColor, element.Font.FontFamily, (double) element.Font.Size, element.Font.Bold, element.Font.Italic, element.Font.Underline, textAlignment, element.TextWrap, element.BorderBoxStyle, element.BorderColor, element.BorderTopColor, element.BorderBottomColor, element.BorderRightColor, element.BorderLeftColor);
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
        fileName = Path.GetDirectoryName(fileName) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName) + this.spreadExportRenderer.GetFileExtension(this.ExportFormat);
        FileStream fileStream = this.FileExportMode != FileExportMode.NewSheetInExistingFile || !File.Exists(fileName) || this.ExportFormat != SpreadStreamExportFormat.Xlsx ? new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false) : new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
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
      this.spreadExportRenderer.CreateWorkbook(stream, this.ExportFormat, this.FileExportMode);
      int num = 0;
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      this.CreateFreezePanes();
      bool flag = this.spreadExportRenderer.CallOnWorksheetCreated();
      this.rowProvider = (RowElementProvider) this.RadGridViewToExport.TableElement.RowElementProvider;
      this.cellProvider = (CellElementProvider) this.RadGridViewToExport.TableElement.CellElementProvider;
      ExportGridTraverser traverser = new ExportGridTraverser(this.RadGridViewToExport.MasterView);
      traverser.ProcessHierarchy = this.exportHierarchy;
      if (!flag && (object) this.RadGridViewToExport.MasterTemplate.ViewDefinition.GetType() == (object) typeof (TableViewDefinition))
        this.SetColumnWidths();
      if ((this.FreezeHeaderRow || this.FreezePinnedRows) && traverser.MoveNext())
      {
        if (traverser.Current is GridViewTableHeaderRowInfo)
        {
          this.AddRow(traverser.Current, num);
          ++num;
        }
        if (this.FreezePinnedRows)
        {
          foreach (GridViewRowInfo pinnedRow in this.RadGridViewToExport.MasterView.PinnedRows)
          {
            this.AddRow(pinnedRow, num);
            ++num;
          }
        }
      }
      TableViewRowLayoutBase rowLayout = (TableViewRowLayoutBase) null;
      if (this.RadGridViewToExport.ViewDefinition is ColumnGroupsViewDefinition)
        rowLayout = (TableViewRowLayoutBase) this.InitializeColumnGroupRowLayout();
      else if (this.RadGridViewToExport.ViewDefinition is HtmlViewDefinition)
        rowLayout = (TableViewRowLayoutBase) this.InitializeHtmlViewRowLayout();
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
        this.hierarchyRowsStack = new Stack<GridViewHierarchyRowInfo>();
      int currentRowNum = this.ProcessRows(traverser, rowLayout, num);
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
      {
        while (this.hierarchyRowsStack.Count > 0 && this.MoveToNextActiveViewOfLastHierarchyRow(traverser))
          currentRowNum = this.ProcessRows(traverser, rowLayout, currentRowNum);
      }
      this.spreadExportRenderer.FinishExport();
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

    private void CreateFreezePanes()
    {
      if (!this.FreezeHeaderRow && !this.FreezePinnedRows)
        return;
      ++this.pinnedRowsCount;
      if (this.FreezePinnedRows)
        this.pinnedRowsCount += this.RadGridViewToExport.MasterView.PinnedRows.Count;
      this.spreadExportRenderer.CreateFreezePanes(this.pinnedRowsCount, this.pinnedColumnsCount);
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
              this.spreadExportRenderer.CallOnWorksheetCreated();
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
              ++currentRowNum;
            }
          }
        }
      }
      return currentRowNum;
    }

    private void SetColumnWidths()
    {
      this.spreadExportRenderer.SkipColumns(this.RadGridViewToExport.GroupDescriptors.Count);
      for (int index1 = 0; index1 < this.RadGridViewToExport.Columns.Count; ++index1)
      {
        int index2 = index1;
        if (this.FreezePinnedColumns)
          index2 = this.pinnedColumnsIndicesOrder[index2];
        GridViewColumn column = (GridViewColumn) this.RadGridViewToExport.Columns[index2];
        if (this.ShouldExportColumn(column))
        {
          this.spreadExportRenderer.CreateColumn();
          if (!column.IsVisible && this.hiddenColumnOption == HiddenOption.ExportAsHidden)
            this.spreadExportRenderer.SetHiddenColumn();
          else if (this.ExportVisualSettings)
            this.spreadExportRenderer.SetColumnWidth((double) column.Width, true);
          else
            this.spreadExportRenderer.SetColumnWidth(8.43, false);
        }
      }
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
        this.ChildNodes = new List<GridViewSpreadStreamExport.GroupNode>();
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

      private List<GridViewSpreadStreamExport.GroupNode> ChildNodes { get; set; }

      public void AddChildNode(GridViewSpreadStreamExport.GroupNode child)
      {
        this.ChildNodes.Add(child);
        child.Level = this.Level + 1;
      }

      public int GetTreeDepth()
      {
        int num = 0;
        if (this.ChildNodes.Count == 0)
          return this.Level;
        foreach (GridViewSpreadStreamExport.GroupNode childNode in this.ChildNodes)
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
          foreach (GridViewSpreadStreamExport.GroupNode childNode in this.ChildNodes)
            num += childNode.GetTotalColSpan();
        }
        return num;
      }

      public GridViewSpreadStreamExport.GroupNode GetChildNode(string text)
      {
        foreach (GridViewSpreadStreamExport.GroupNode childNode1 in this.ChildNodes)
        {
          if (childNode1.Group.Text == text)
            return childNode1;
          GridViewSpreadStreamExport.GroupNode childNode2 = childNode1.GetChildNode(text);
          if (childNode2 != null)
            return childNode2;
        }
        return (GridViewSpreadStreamExport.GroupNode) null;
      }

      public int GetPreviousNodesColSpan(GridViewSpreadStreamExport.GroupNode node)
      {
        this.foundNode = false;
        this.levelColSpan = 0;
        this.CalculateNodeLevelColSpan(node, ref this.foundNode, ref this.levelColSpan);
        return this.levelColSpan;
      }

      private void CalculateNodeLevelColSpan(
        GridViewSpreadStreamExport.GroupNode node,
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
          foreach (GridViewSpreadStreamExport.GroupNode childNode in this.ChildNodes)
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

    private class ViewDefinitionExportRow
    {
      public ViewDefinitionExportRow()
      {
        this.Cells = (IList<GridViewSpreadStreamExport.ViewDefinitionExportCell>) new List<GridViewSpreadStreamExport.ViewDefinitionExportCell>();
        this.BorderCells = (IDictionary<int, GridViewSpreadStreamExport.ViewDefinitionBorderCell>) new SortedDictionary<int, GridViewSpreadStreamExport.ViewDefinitionBorderCell>();
      }

      public IList<GridViewSpreadStreamExport.ViewDefinitionExportCell> Cells { get; set; }

      public IDictionary<int, GridViewSpreadStreamExport.ViewDefinitionBorderCell> BorderCells { get; set; }
    }

    private class ViewDefinitionBorderCell
    {
      public ViewDefinitionBorderCell(
        GridViewSpreadStreamExport.ViewDefinitionExportCell topLeftCell)
      {
        this.TopLeftCell = topLeftCell;
      }

      public GridViewSpreadStreamExport.ViewDefinitionExportCell TopLeftCell { get; set; }
    }

    private class ViewDefinitionExportCell
    {
      public ViewDefinitionExportCell()
      {
        this.RowSpan = 1;
        this.ColSpan = 1;
      }

      public int RowSpan { get; set; }

      public int ColSpan { get; set; }

      public IGridViewSpreadStreamExportCellInfo ExportCellInfo { get; set; }
    }

    private delegate void RunExportCallback(string fileName);

    private delegate void RunExportToStreamCallback(Stream exportStream);
  }
}
