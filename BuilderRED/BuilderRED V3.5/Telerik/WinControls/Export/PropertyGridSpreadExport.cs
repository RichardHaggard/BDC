// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.PropertyGridSpreadExport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  public class PropertyGridSpreadExport
  {
    private int sheetMaxRowsNumber = 65536;
    private int itemIndent = 20;
    private RadPropertyGrid propertyGrid;
    private PropertyGridItemElementProvider elementProvider;
    private ISpreadExportRenderer spreadExportRenderer;
    private SpreadExportFormat exportFormat;
    private bool exportVisualSettings;
    private bool applicationDoEvents;
    private int depthOfTree;
    private HiddenOption collapsedItemOption;
    private bool isGridGrouped;
    private BackgroundWorker worker;
    private List<int> columnWidths;
    private int textCellWidth;
    private int valueCellWidth;
    private Stack<PropertyGridSpreadExport.RowGroup> hierarchyRowGroups;
    private Stack<PropertyGridSpreadExport.RowGroup> readyToExportRowGroups;
    private int lastRowHierarchyLevel;

    public PropertyGridSpreadExport(RadPropertyGrid radPropertyGrid)
    {
      this.propertyGrid = radPropertyGrid;
    }

    public PropertyGridSpreadExport(
      RadPropertyGrid radPropertyGrid,
      SpreadExportFormat exportFormat)
      : this(radPropertyGrid)
    {
      this.ExportFormat = exportFormat;
    }

    public string SheetName { get; set; }

    [DefaultValue(FileExportMode.NewSheetInExistingFile)]
    public FileExportMode FileExportMode { get; set; }

    public bool ExportChildItemsGrouped { get; set; }

    public bool ExportDescriptions { get; set; }

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

    [CLSCompliant(false)]
    [DefaultValue(ExcelMaxRows._65536)]
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

    public int ItemIndent
    {
      get
      {
        return this.itemIndent;
      }
      set
      {
        if (value < 0)
          throw new ArgumentException("ItemIndent cannot be negative.", nameof (ItemIndent));
        this.itemIndent = value;
      }
    }

    public HiddenOption CollapsedItemOption
    {
      get
      {
        return this.collapsedItemOption;
      }
      set
      {
        this.collapsedItemOption = value;
      }
    }

    public event PropertyGridSpreadExportCellFormattingEventHandler CellFormatting;

    protected virtual void OnCellFormatting(PropertyGridSpreadExportCellFormattingEventArgs e)
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

    public void RunExport(string fileName, ISpreadExportRenderer exportRenderer)
    {
      this.spreadExportRenderer = exportRenderer;
      this.spreadExportRenderer.RegisterFormatProvider(this.ExportFormat);
      if (this.propertyGrid.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.propertyGrid.Invoke((Delegate) new PropertyGridSpreadExport.RunExportCallback(this.RunExportCall), (object) fileName);
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
      List<PropertyGridSpreadExportRow> exportRows = (List<PropertyGridSpreadExportRow>) null;
      if (this.propertyGrid.InvokeRequired)
      {
        this.propertyGrid.Invoke((Delegate) (() =>
        {
          PropertyGridExportState state = this.SavePropertyGridState();
          exportRows = this.GetPropertyGridDataSnapshot();
          this.RestorePropertyGridState(state);
        }));
      }
      else
      {
        PropertyGridExportState state = this.SavePropertyGridState();
        exportRows = this.GetPropertyGridDataSnapshot();
        this.RestorePropertyGridState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new PropertyGridSpreadExportDataSnapshot(fileName, exportRows));
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
      if (this.propertyGrid.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.propertyGrid.Invoke((Delegate) new PropertyGridSpreadExport.RunExportToStreamCallback(this.RunExportCall), (object) exportStream);
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
      List<PropertyGridSpreadExportRow> exportRows = (List<PropertyGridSpreadExportRow>) null;
      if (this.propertyGrid.InvokeRequired)
      {
        this.propertyGrid.Invoke((Delegate) (() =>
        {
          PropertyGridExportState state = this.SavePropertyGridState();
          exportRows = this.GetPropertyGridDataSnapshot();
          this.RestorePropertyGridState(state);
        }));
      }
      else
      {
        PropertyGridExportState state = this.SavePropertyGridState();
        exportRows = this.GetPropertyGridDataSnapshot();
        this.RestorePropertyGridState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new List<object>(2)
      {
        (object) new PropertyGridSpreadExportDataSnapshot(string.Empty, exportRows),
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
      this.spreadExportRenderer.RegisterFormatProvider(this.ExportFormat);
      this.spreadExportRenderer.GetFormatProvider(this.ExportFormat);
      this.columnWidths = new List<int>();
      PropertyGridSpreadExportDataSnapshot propertyGridData1 = e.Argument as PropertyGridSpreadExportDataSnapshot;
      if (propertyGridData1 != null)
      {
        string path = Path.GetDirectoryName(propertyGridData1.FilePath) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(propertyGridData1.FilePath) + this.spreadExportRenderer.GetFileExtension(this.ExportFormat);
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
          this.ExportToStreamAsync(propertyGridData1, (Stream) fileStream);
        this.GetWorker().ReportProgress(100);
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.propertyGrid, "Export", (object) path);
      }
      else
      {
        this.spreadExportRenderer.CreateWorkbook();
        List<object> objectList = e.Argument as List<object>;
        PropertyGridSpreadExportDataSnapshot propertyGridData2 = objectList[0] as PropertyGridSpreadExportDataSnapshot;
        Stream stream = objectList[1] as Stream;
        this.ExportToStreamAsync(propertyGridData2, stream);
        this.GetWorker().ReportProgress(100);
        e.Result = (object) stream;
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.propertyGrid, "Export");
      }
    }

    private void ExportToStreamAsync(
      PropertyGridSpreadExportDataSnapshot propertyGridData,
      Stream stream)
    {
      int percentProgress = 0;
      int num1 = 0;
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      for (int index = 0; index < propertyGridData.ExportRows.Count; ++index)
      {
        if (num1 > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          num1 = 0;
          this.columnWidths.Clear();
        }
        this.AddRow(propertyGridData.ExportRows[index], num1);
        int num2 = index * 100 / propertyGridData.ExportRows.Count;
        if (percentProgress != num2)
        {
          percentProgress = num2;
          this.GetWorker().ReportProgress(percentProgress);
        }
        ++num1;
      }
      if (this.ExportChildItemsGrouped)
        this.GroupWorksheetRows(num1);
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
    }

    private List<PropertyGridSpreadExportRow> GetPropertyGridDataSnapshot()
    {
      List<PropertyGridSpreadExportRow> gridSpreadExportRowList = new List<PropertyGridSpreadExportRow>();
      if (this.ExportVisualSettings)
        this.elementProvider = this.propertyGrid.PropertyGridElement.PropertyTableElement.ViewElement.ElementProvider as PropertyGridItemElementProvider;
      int num = 0;
      PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this.propertyGrid.PropertyGridElement.PropertyTableElement);
      propertyGridTraverser.TraverseHirarchy = true;
      bool skipHiddenChildItems = false;
      bool exportChildItemsHidden = false;
      int collapsedParentLevel = 0;
      this.valueCellWidth = this.propertyGrid.PropertyGridElement.SplitElement.PropertyTableElement.ValueColumnWidth;
      this.textCellWidth = this.propertyGrid.PropertyGridElement.PropertyTableElement.Size.Width - this.propertyGrid.PropertyGridElement.PropertyTableElement.VScrollBar.Size.Width - this.valueCellWidth;
      if (this.ExportChildItemsGrouped)
        this.InitializeRowGroupDataStructures();
      while (propertyGridTraverser.MoveNext())
      {
        PropertyGridItemBase current = propertyGridTraverser.Current;
        int level = current.Level;
        if (current is PropertyGridItem && this.isGridGrouped)
          ++level;
        if (!skipHiddenChildItems || collapsedParentLevel >= level)
        {
          skipHiddenChildItems = false;
          PropertyGridSpreadExportRow exportRow = this.CreateExportRow(current, level, num, ref skipHiddenChildItems, ref collapsedParentLevel, ref exportChildItemsHidden);
          gridSpreadExportRowList.Add(exportRow);
          if (this.ExportChildItemsGrouped)
            this.ProcessCurrentRowGrouping(level, num);
          ++num;
        }
      }
      return gridSpreadExportRowList;
    }

    private void RunExportCall(Stream exportStream)
    {
      PropertyGridExportState state = this.SavePropertyGridState();
      this.columnWidths = new List<int>();
      if (this.ExportVisualSettings)
        this.elementProvider = this.propertyGrid.PropertyGridElement.PropertyTableElement.ViewElement.ElementProvider as PropertyGridItemElementProvider;
      try
      {
        this.spreadExportRenderer.GetFormatProvider(this.ExportFormat);
        this.spreadExportRenderer.CreateWorkbook();
        this.ExportToStream(exportStream);
        this.OnExportCompleted(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.propertyGrid, "Export");
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestorePropertyGridState(state);
      }
    }

    private void RunExportCall(string fileName)
    {
      PropertyGridExportState state = this.SavePropertyGridState();
      this.columnWidths = new List<int>();
      if (this.ExportVisualSettings)
        this.elementProvider = this.propertyGrid.PropertyGridElement.PropertyTableElement.ViewElement.ElementProvider as PropertyGridItemElementProvider;
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
        using (fileStream)
          this.ExportToStream((Stream) fileStream);
        this.OnExportCompleted(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.propertyGrid, "Export", (object) fileName);
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestorePropertyGridState(state);
      }
    }

    private void ExportToStream(Stream stream)
    {
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      int num = 0;
      PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this.propertyGrid.PropertyGridElement.PropertyTableElement);
      propertyGridTraverser.TraverseHirarchy = true;
      bool skipHiddenChildItems = false;
      bool exportChildItemsHidden = false;
      int collapsedParentLevel = 0;
      this.valueCellWidth = this.propertyGrid.PropertyGridElement.SplitElement.PropertyTableElement.ValueColumnWidth;
      this.textCellWidth = this.propertyGrid.PropertyGridElement.PropertyTableElement.Size.Width - this.propertyGrid.PropertyGridElement.PropertyTableElement.VScrollBar.Size.Width - this.valueCellWidth;
      if (this.ExportChildItemsGrouped)
        this.InitializeRowGroupDataStructures();
      while (propertyGridTraverser.MoveNext())
      {
        if (this.applicationDoEvents)
          Application.DoEvents();
        if (num > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          num = 0;
          this.columnWidths.Clear();
        }
        PropertyGridItemBase current = propertyGridTraverser.Current;
        int level = current.Level;
        if (current is PropertyGridItem && this.isGridGrouped)
          ++level;
        if (!skipHiddenChildItems || collapsedParentLevel >= level)
        {
          skipHiddenChildItems = false;
          this.AddRow(this.CreateExportRow(current, level, num, ref skipHiddenChildItems, ref collapsedParentLevel, ref exportChildItemsHidden), num);
          if (this.ExportChildItemsGrouped)
            this.ProcessCurrentRowGrouping(level, num);
          ++num;
        }
      }
      if (this.ExportChildItemsGrouped)
        this.GroupWorksheetRows(num);
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
    }

    private PropertyGridSpreadExportRow CreateExportRow(
      PropertyGridItemBase item,
      int itemLevel,
      int currentRowIndex,
      ref bool skipHiddenChildItems,
      ref int collapsedParentLevel,
      ref bool exportChildItemsHidden)
    {
      PropertyGridSpreadExportRow gridSpreadExportRow = new PropertyGridSpreadExportRow();
      if (!item.Expanded && this.CollapsedItemOption == HiddenOption.DoNotExport)
      {
        skipHiddenChildItems = true;
        collapsedParentLevel = itemLevel;
      }
      if (exportChildItemsHidden && collapsedParentLevel >= itemLevel)
        exportChildItemsHidden = false;
      if (item is PropertyGridItem)
      {
        PropertyGridItem propertyGridItem = item as PropertyGridItem;
        PropertyGridItemElement propertyGridItemElement = (PropertyGridItemElement) null;
        if (this.ExportVisualSettings)
        {
          propertyGridItemElement = this.elementProvider.GetElement((PropertyGridItemBase) propertyGridItem, (object) null) as PropertyGridItemElement;
          this.propertyGrid.PropertyGridElement.PropertyTableElement.ViewElement.Children.Add((RadElement) propertyGridItemElement);
          propertyGridItemElement.Attach((PropertyGridItemBase) propertyGridItem, (object) null);
        }
        for (int index = 0; index < itemLevel; ++index)
        {
          PropertyGridSpreadExportIndentCell exportIndentCell = new PropertyGridSpreadExportIndentCell();
          exportIndentCell.Size = new Size(this.ItemIndent, exportChildItemsHidden ? 0 : -1);
          if (this.ExportVisualSettings)
          {
            if (index == 0 && this.isGridGrouped)
            {
              if (itemLevel > 1)
                this.GetStylesFromVisualCell((PropertyGridSpreadExportCell) exportIndentCell, (LightVisualElement) propertyGridItemElement.HeaderElement);
              else
                this.GetStylesFromVisualCell((PropertyGridSpreadExportCell) exportIndentCell, (LightVisualElement) propertyGridItemElement.ExpanderElement);
            }
            else
              this.GetStylesFromVisualCell((PropertyGridSpreadExportCell) exportIndentCell, (LightVisualElement) propertyGridItemElement.IndentElement);
            this.GetBordersFromVisualCell((PropertyGridSpreadExportCell) exportIndentCell, (LightVisualElement) propertyGridItemElement);
          }
          this.OnCellFormatting(new PropertyGridSpreadExportCellFormattingEventArgs((PropertyGridSpreadExportCell) exportIndentCell, (PropertyGridItemBase) propertyGridItem, currentRowIndex));
          gridSpreadExportRow.Cells.Add((PropertyGridSpreadExportCell) exportIndentCell);
        }
        PropertyGridSpreadExportContentCell exportContentCell1 = new PropertyGridSpreadExportContentCell();
        exportContentCell1.Text = propertyGridItem.Label;
        exportContentCell1.ColSpan += this.depthOfTree - propertyGridItem.Level;
        exportContentCell1.Size = new Size(this.textCellWidth - itemLevel * this.ItemIndent, exportChildItemsHidden ? 0 : -1);
        gridSpreadExportRow.Cells.Add((PropertyGridSpreadExportCell) exportContentCell1);
        if (this.ExportVisualSettings)
        {
          this.GetStylesFromVisualCell((PropertyGridSpreadExportCell) exportContentCell1, (LightVisualElement) propertyGridItemElement.TextElement);
          this.GetBordersFromVisualCell((PropertyGridSpreadExportCell) exportContentCell1, (LightVisualElement) propertyGridItemElement);
        }
        this.OnCellFormatting(new PropertyGridSpreadExportCellFormattingEventArgs((PropertyGridSpreadExportCell) exportContentCell1, (PropertyGridItemBase) propertyGridItem, currentRowIndex));
        PropertyGridSpreadExportContentCell exportContentCell2 = new PropertyGridSpreadExportContentCell();
        exportContentCell2.Value = propertyGridItem.Value;
        exportContentCell2.Text = propertyGridItem.FormattedValue;
        exportContentCell2.Size = new Size(this.valueCellWidth, exportChildItemsHidden ? 0 : -1);
        if (this.ExportVisualSettings)
        {
          this.GetStylesFromVisualCell((PropertyGridSpreadExportCell) exportContentCell2, (LightVisualElement) propertyGridItemElement.ValueElement);
          this.GetBordersFromVisualCell((PropertyGridSpreadExportCell) exportContentCell2, (LightVisualElement) propertyGridItemElement);
        }
        this.OnCellFormatting(new PropertyGridSpreadExportCellFormattingEventArgs((PropertyGridSpreadExportCell) exportContentCell2, (PropertyGridItemBase) propertyGridItem, currentRowIndex));
        gridSpreadExportRow.Cells.Add((PropertyGridSpreadExportCell) exportContentCell2);
        if (this.ExportDescriptions)
        {
          PropertyGridSpreadExportContentCell exportContentCell3 = new PropertyGridSpreadExportContentCell();
          exportContentCell3.Text = propertyGridItem.Description;
          exportContentCell3.Size = new Size((this.textCellWidth + this.valueCellWidth) * 2, exportChildItemsHidden ? 0 : -1);
          if (this.ExportVisualSettings)
          {
            this.GetStylesFromVisualCell((PropertyGridSpreadExportCell) exportContentCell3, (LightVisualElement) propertyGridItemElement.ValueElement);
            this.GetBordersFromVisualCell((PropertyGridSpreadExportCell) exportContentCell3, (LightVisualElement) propertyGridItemElement);
          }
          this.OnCellFormatting(new PropertyGridSpreadExportCellFormattingEventArgs((PropertyGridSpreadExportCell) exportContentCell3, (PropertyGridItemBase) propertyGridItem, currentRowIndex));
          gridSpreadExportRow.Cells.Add((PropertyGridSpreadExportCell) exportContentCell3);
        }
        if (this.ExportVisualSettings)
        {
          this.elementProvider.CacheElement((IVirtualizedElement<PropertyGridItemBase>) propertyGridItemElement);
          propertyGridItemElement.Detach();
          this.propertyGrid.PropertyGridElement.PropertyTableElement.ViewElement.Children.Remove((RadElement) propertyGridItemElement);
        }
      }
      else if (item is PropertyGridGroupItem)
      {
        this.isGridGrouped = true;
        PropertyGridGroupItem propertyGridGroupItem = item as PropertyGridGroupItem;
        PropertyGridGroupElement gridGroupElement = (PropertyGridGroupElement) null;
        if (this.ExportVisualSettings)
        {
          gridGroupElement = this.elementProvider.GetElement((PropertyGridItemBase) propertyGridGroupItem, (object) null) as PropertyGridGroupElement;
          this.propertyGrid.PropertyGridElement.PropertyTableElement.ViewElement.Children.Add((RadElement) gridGroupElement);
          gridGroupElement.Attach((PropertyGridItemBase) propertyGridGroupItem, (object) null);
        }
        PropertyGridSpreadExportContentCell exportContentCell = new PropertyGridSpreadExportContentCell();
        exportContentCell.Text = propertyGridGroupItem.Label;
        ++exportContentCell.ColSpan;
        ++exportContentCell.ColSpan;
        exportContentCell.ColSpan += this.depthOfTree;
        if (this.ExportDescriptions)
          ++exportContentCell.ColSpan;
        exportContentCell.Size = new Size(this.textCellWidth + this.valueCellWidth + this.depthOfTree * this.ItemIndent, -1);
        if (this.ExportVisualSettings)
        {
          this.GetStylesFromVisualCell((PropertyGridSpreadExportCell) exportContentCell, (LightVisualElement) gridGroupElement.TextElement);
          this.GetBordersFromVisualCell((PropertyGridSpreadExportCell) exportContentCell, (LightVisualElement) gridGroupElement.TextElement);
        }
        this.OnCellFormatting(new PropertyGridSpreadExportCellFormattingEventArgs((PropertyGridSpreadExportCell) exportContentCell, (PropertyGridItemBase) propertyGridGroupItem, currentRowIndex));
        gridSpreadExportRow.Cells.Add((PropertyGridSpreadExportCell) exportContentCell);
        if (this.ExportVisualSettings)
        {
          this.elementProvider.CacheElement((IVirtualizedElement<PropertyGridItemBase>) gridGroupElement);
          gridGroupElement.Detach();
          this.propertyGrid.PropertyGridElement.PropertyTableElement.ViewElement.Children.Remove((RadElement) gridGroupElement);
        }
      }
      if (!exportChildItemsHidden)
      {
        exportChildItemsHidden = !item.Expanded && this.CollapsedItemOption == HiddenOption.ExportAsHidden;
        collapsedParentLevel = itemLevel;
      }
      return gridSpreadExportRow;
    }

    private PropertyGridExportState SavePropertyGridState()
    {
      PropertyGridExportState propertyGridExportState = new PropertyGridExportState() { SelectedItem = this.propertyGrid.SelectedGridItem, HorizontalScrollbarValue = this.propertyGrid.PropertyGridElement.PropertyTableElement.HScrollBar.Value, VerticalScrolbarValue = this.propertyGrid.PropertyGridElement.PropertyTableElement.VScrollBar.Value };
      this.propertyGrid.SelectedGridItem = (PropertyGridItemBase) null;
      int num = 0;
      Queue<PropertyGridItemBase> propertyGridItemBaseQueue = new Queue<PropertyGridItemBase>();
      foreach (PropertyGridItemBase propertyGridItemBase in (ReadOnlyCollection<PropertyGridItem>) this.propertyGrid.Items)
        propertyGridItemBaseQueue.Enqueue(propertyGridItemBase);
      while (propertyGridItemBaseQueue.Count > 0)
      {
        PropertyGridItemBase propertyGridItemBase = propertyGridItemBaseQueue.Dequeue();
        if (propertyGridItemBase.Level > num)
          num = propertyGridItemBase.Level;
        foreach (PropertyGridItemBase gridItem in (ReadOnlyCollection<PropertyGridItem>) propertyGridItemBase.GridItems)
          propertyGridItemBaseQueue.Enqueue(gridItem);
      }
      this.depthOfTree = num;
      return propertyGridExportState;
    }

    private void RestorePropertyGridState(PropertyGridExportState state)
    {
      this.propertyGrid.SelectedGridItem = state.SelectedItem;
      this.propertyGrid.PropertyGridElement.PropertyTableElement.HScrollBar.Value = state.HorizontalScrollbarValue;
      this.propertyGrid.PropertyGridElement.PropertyTableElement.VScrollBar.Value = state.VerticalScrolbarValue;
    }

    private void AddRow(PropertyGridSpreadExportRow row, int currentRowIndex)
    {
      int index1 = 0;
      int rowHeight = -1;
      for (int index2 = 0; index2 < row.Cells.Count; ++index2)
      {
        PropertyGridSpreadExportCell cell = row.Cells[index2];
        if (rowHeight < cell.Size.Height)
          rowHeight = cell.Size.Height;
        while (this.columnWidths.Count <= index1)
          this.columnWidths.Add(0);
        if (cell is PropertyGridSpreadExportIndentCell)
        {
          if (cell.Size.Width > this.columnWidths[index1])
          {
            this.columnWidths[index1] = cell.Size.Width;
            this.spreadExportRenderer.SetWorksheetColumnWidth(index1, (double) cell.Size.Width, false);
          }
          this.CreateCell(cell, currentRowIndex, index1);
        }
        else
        {
          if (rowHeight >= 0)
            this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, rowHeight, true);
          int num = this.isGridGrouped ? this.depthOfTree + 1 : this.depthOfTree;
          if (cell.Size.Width > this.columnWidths[index1] && num <= index1)
          {
            this.columnWidths[index1] = cell.Size.Width;
            this.spreadExportRenderer.SetWorksheetColumnWidth(index1, (double) cell.Size.Width, false);
          }
          this.CreateCell(cell, currentRowIndex, index1);
        }
        index1 += cell.ColSpan;
      }
    }

    protected virtual void CreateCell(
      PropertyGridSpreadExportCell cell,
      int currentRowIndex,
      int currentColumnIndex)
    {
      this.spreadExportRenderer.CreateCellSelection(currentRowIndex, currentColumnIndex, currentRowIndex, currentColumnIndex + cell.ColSpan - 1);
      this.spreadExportRenderer.MergeCellSelection();
      PropertyGridSpreadExportContentCell exportContentCell = cell as PropertyGridSpreadExportContentCell;
      if (exportContentCell != null)
      {
        bool flag = false;
        if (!string.IsNullOrEmpty(exportContentCell.FormatString))
        {
          this.spreadExportRenderer.SetCellSelectionFormat(exportContentCell.FormatString);
          flag = true;
        }
        if (exportContentCell.Value != null)
        {
          DataType dataType = new DataTypeConvertor().ConvertToDataType(exportContentCell.Value);
          switch (dataType)
          {
            case DataType.String:
            case DataType.Other:
              if (!flag)
                this.spreadExportRenderer.SetCellSelectionFormat("@");
              this.spreadExportRenderer.SetCellSelectionValue(exportContentCell.Text);
              break;
            default:
              this.spreadExportRenderer.SetCellSelectionValue(dataType, exportContentCell.Value);
              break;
          }
        }
        else if (!string.IsNullOrEmpty(exportContentCell.Text))
        {
          if (!flag)
            this.spreadExportRenderer.SetCellSelectionFormat("@");
          this.spreadExportRenderer.SetCellSelectionValue(exportContentCell.Text);
        }
        object cellSelectionValue = this.spreadExportRenderer.GetCellSelectionValue();
        if (cellSelectionValue is DateTime && !this.CheckDateTimeValue((DateTime) cellSelectionValue))
          throw new FormatException("The DateTime value is not supported in Excel!");
      }
      if (!this.ExportVisualSettings && this.CellFormatting == null)
        return;
      if (cell.Font == null)
        cell.Font = Control.DefaultFont;
      this.spreadExportRenderer.CreateCellStyleInfo(cell.BackColor, cell.ForeColor, cell.Font.FontFamily, (double) cell.Font.Size, cell.Font.Bold, cell.Font.Italic, cell.Font.Underline, cell.TextAlignment, cell.TextWrap, cell.BorderBoxStyle, cell.BorderColor, cell.BorderTopColor, cell.BorderBottomColor, cell.BorderRightColor, cell.BorderLeftColor);
    }

    protected virtual void GetStylesFromVisualCell(
      PropertyGridSpreadExportCell cell,
      LightVisualElement visualCell)
    {
      if (visualCell == null)
        return;
      cell.Font = visualCell.Font;
      cell.TextAlignment = visualCell.TextAlignment;
      cell.TextWrap = visualCell.TextWrap;
      cell.BackColor = this.GetBackColor(visualCell);
      cell.ForeColor = visualCell.ForeColor;
    }

    protected virtual void GetBordersFromVisualCell(
      PropertyGridSpreadExportCell cell,
      LightVisualElement visualCell)
    {
      cell.BorderBoxStyle = visualCell.BorderBoxStyle;
      cell.BorderColor = visualCell.BorderColor;
      cell.BorderLeftColor = visualCell.BorderLeftColor;
      cell.BorderTopColor = visualCell.BorderTopColor;
      cell.BorderRightColor = visualCell.BorderRightColor;
      cell.BorderBottomColor = visualCell.BorderBottomColor;
    }

    protected virtual byte[] ConvertImageToByteArray(Image image)
    {
      return (byte[]) new ImageConverter().ConvertTo((object) image, typeof (byte[]));
    }

    private bool CheckDateTimeValue(DateTime value)
    {
      bool flag = true;
      DateTime dateTime = new DateTime(1900, 1, 1);
      if (value < dateTime)
        flag = false;
      return flag;
    }

    private void InitializeRowGroupDataStructures()
    {
      this.hierarchyRowGroups = new Stack<PropertyGridSpreadExport.RowGroup>();
      this.readyToExportRowGroups = new Stack<PropertyGridSpreadExport.RowGroup>();
      this.lastRowHierarchyLevel = 0;
    }

    private void ProcessCurrentRowGrouping(int currentRowHierarchyLevel, int currentRowNum)
    {
      if (currentRowHierarchyLevel > this.lastRowHierarchyLevel)
        this.hierarchyRowGroups.Push(new PropertyGridSpreadExport.RowGroup(currentRowNum, currentRowHierarchyLevel));
      else if (currentRowHierarchyLevel < this.lastRowHierarchyLevel)
      {
        for (int index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0; currentRowHierarchyLevel < index && this.hierarchyRowGroups.Count > 0; index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0)
        {
          PropertyGridSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
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
        PropertyGridSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
        rowGroup.EndRow = currentRowNum - 1;
        this.readyToExportRowGroups.Push(rowGroup);
      }
      while (this.readyToExportRowGroups.Count > 0)
      {
        PropertyGridSpreadExport.RowGroup rowGroup = this.readyToExportRowGroups.Pop();
        this.spreadExportRenderer.GroupRows(rowGroup.StartRow, rowGroup.EndRow, rowGroup.Level);
      }
    }

    private Color GetBackColor(LightVisualElement element)
    {
      RadElement radElement = (RadElement) element;
      if (element.BackColor.A < (byte) 200 || !element.DrawFill)
      {
        while (radElement.Parent != null)
        {
          radElement = radElement.Parent;
          if (radElement is LightVisualElement && (((VisualElement) radElement).BackColor.A > (byte) 200 && ((UIItemBase) radElement).DrawFill))
            break;
        }
      }
      LightVisualElement lightVisualElement = (LightVisualElement) radElement;
      return this.ColorMixer(lightVisualElement.GradientStyle, lightVisualElement.NumberOfColors, lightVisualElement.GradientPercentage, lightVisualElement.GradientPercentage2, lightVisualElement.BackColor, lightVisualElement.BackColor2, lightVisualElement.BackColor3, lightVisualElement.BackColor4);
    }

    private Color ColorMixer(
      GradientStyles gradientSyle,
      int numberOfColors,
      float gradientPercent,
      float gradientPercent2,
      params Color[] colors)
    {
      if (numberOfColors < 1 || numberOfColors > 4)
        throw new ArgumentException("Invalid number of colors. Should be between 1 and 4");
      Color color;
      if (gradientSyle != GradientStyles.Solid)
      {
        switch (numberOfColors)
        {
          case 1:
            break;
          case 2:
            color = this.ColorMixer(colors[0], colors[1]);
            goto label_8;
          case 3:
            float num1 = 0.0f;
            float num2 = 0.0f;
            float num3 = 0.0f;
            float num4 = num1 + (float) ((double) colors[0].R * (double) gradientPercent / 2.0);
            float num5 = num2 + (float) ((double) colors[0].G * (double) gradientPercent / 2.0);
            float num6 = num3 + (float) ((double) colors[0].B * (double) gradientPercent / 2.0);
            float num7 = num4 + (float) ((int) colors[1].R / 2);
            float num8 = num5 + (float) ((int) colors[1].G / 2);
            float num9 = num6 + (float) ((int) colors[1].B / 2);
            color = Color.FromArgb((int) (num7 + (float) colors[2].R * (float) ((1.0 - (double) gradientPercent) / 2.0)), (int) (num8 + (float) colors[2].G * (float) ((1.0 - (double) gradientPercent) / 2.0)), (int) (num9 + (float) colors[2].B * (float) ((1.0 - (double) gradientPercent) / 2.0)));
            goto label_8;
          default:
            float num10 = 0.0f;
            float num11 = 0.0f;
            float num12 = 0.0f;
            float num13 = num10 + (float) ((double) colors[0].R * (double) gradientPercent / 2.0);
            float num14 = num11 + (float) ((double) colors[0].G * (double) gradientPercent / 2.0);
            float num15 = num12 + (float) ((double) colors[0].B * (double) gradientPercent / 2.0);
            float num16 = num13 + (float) colors[1].R * (float) ((double) gradientPercent / 2.0 + ((double) gradientPercent2 - (double) gradientPercent) / 2.0);
            float num17 = num14 + (float) colors[1].G * (float) ((double) gradientPercent / 2.0 + ((double) gradientPercent2 - (double) gradientPercent) / 2.0);
            float num18 = num15 + (float) colors[1].B * (float) ((double) gradientPercent / 2.0 + ((double) gradientPercent2 - (double) gradientPercent) / 2.0);
            float num19 = num16 + (float) colors[2].R * (float) (((double) gradientPercent2 - (double) gradientPercent) / 2.0 + (1.0 - (double) gradientPercent2) / 2.0);
            float num20 = num17 + (float) colors[2].G * (float) (((double) gradientPercent2 - (double) gradientPercent) / 2.0 + (1.0 - (double) gradientPercent2) / 2.0);
            float num21 = num18 + (float) colors[2].B * (float) (((double) gradientPercent2 - (double) gradientPercent) / 2.0 + (1.0 - (double) gradientPercent2) / 2.0);
            color = Color.FromArgb((int) (num19 + (float) colors[3].R * (float) ((1.0 - (double) gradientPercent2) / 2.0)), (int) (num20 + (float) colors[3].G * (float) ((1.0 - (double) gradientPercent2) / 2.0)), (int) (num21 + (float) colors[3].B * (float) ((1.0 - (double) gradientPercent2) / 2.0)));
            goto label_8;
        }
      }
      color = colors[0];
label_8:
      return color;
    }

    private Color ColorMixer(params Color[] colors)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < colors.Length; ++index)
      {
        num1 += (int) colors[index].R;
        num2 += (int) colors[index].G;
        num3 += (int) colors[index].B;
      }
      return Color.FromArgb(num1 / colors.Length, num2 / colors.Length, num3 / colors.Length);
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
