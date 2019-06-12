// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.ListViewSpreadExport
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
  public class ListViewSpreadExport
  {
    private int sheetMaxRowsNumber = 65536;
    private RadListView listView;
    private IVirtualizedElementProvider<ListViewDataItem> elementProvider;
    private ISpreadExportRenderer spreadExportRenderer;
    private ListViewColumnTraverser columnTraverser;
    private SpreadExportFormat exportFormat;
    private bool exportVisualSettings;
    private bool applicationDoEvents;
    private HiddenOption collapsedItemOption;
    private ListViewType viewType;
    private Stack<ListViewSpreadExport.RowGroup> hierarchyRowGroups;
    private Stack<ListViewSpreadExport.RowGroup> readyToExportRowGroups;
    private int lastRowHierarchyLevel;
    private BackgroundWorker worker;

    public ListViewSpreadExport(RadListView radListView)
    {
      this.listView = radListView;
      this.viewType = this.listView.ViewType;
    }

    public ListViewSpreadExport(RadListView radListView, SpreadExportFormat exportFormat)
      : this(radListView)
    {
      this.ExportFormat = exportFormat;
    }

    public string SheetName { get; set; }

    [DefaultValue(FileExportMode.NewSheetInExistingFile)]
    public FileExportMode FileExportMode { get; set; }

    public bool ExportImages { get; set; }

    public bool ExportChildItemsGrouped { get; set; }

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

    public event ListViewSpreadExportCellFormattingEventHandler CellFormatting;

    protected virtual void OnCellFormatting(ListViewSpreadExportCellFormattingEventArgs e)
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
      if (this.listView.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.listView.Invoke((Delegate) new ListViewSpreadExport.RunExportCallback(this.RunExportCall), (object) fileName);
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
      List<ListViewSpreadExportRow> exportRows = (List<ListViewSpreadExportRow>) null;
      if (this.listView.InvokeRequired)
      {
        this.listView.Invoke((Delegate) (() =>
        {
          ListViewExportState state = this.SaveListViewState();
          exportRows = this.GetListViewDataSnapshot();
          this.RestoreListViewState(state);
        }));
      }
      else
      {
        ListViewExportState state = this.SaveListViewState();
        exportRows = this.GetListViewDataSnapshot();
        this.RestoreListViewState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new ListViewSpreadExportDataSnapshot(fileName, exportRows));
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
      if (this.listView.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.listView.Invoke((Delegate) new ListViewSpreadExport.RunExportToStreamCallback(this.RunExportCall), (object) exportStream);
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
      List<ListViewSpreadExportRow> exportRows = (List<ListViewSpreadExportRow>) null;
      if (this.listView.InvokeRequired)
      {
        this.listView.Invoke((Delegate) (() =>
        {
          ListViewExportState state = this.SaveListViewState();
          exportRows = this.GetListViewDataSnapshot();
          this.RestoreListViewState(state);
        }));
      }
      else
      {
        ListViewExportState state = this.SaveListViewState();
        exportRows = this.GetListViewDataSnapshot();
        this.RestoreListViewState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new List<object>(2)
      {
        (object) new ListViewSpreadExportDataSnapshot(string.Empty, exportRows),
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
      ListViewSpreadExportDataSnapshot listViewData1 = e.Argument as ListViewSpreadExportDataSnapshot;
      if (listViewData1 != null)
      {
        string path = Path.GetDirectoryName(listViewData1.FilePath) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(listViewData1.FilePath) + this.spreadExportRenderer.GetFileExtension(this.ExportFormat);
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
          this.ExportToStreamAsync(listViewData1, (Stream) fileStream);
        this.GetWorker().ReportProgress(100);
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.listView, "Export", (object) path);
      }
      else
      {
        this.spreadExportRenderer.CreateWorkbook();
        List<object> objectList = e.Argument as List<object>;
        ListViewSpreadExportDataSnapshot listViewData2 = objectList[0] as ListViewSpreadExportDataSnapshot;
        Stream stream = objectList[1] as Stream;
        this.ExportToStreamAsync(listViewData2, stream);
        e.Result = (object) stream;
        this.GetWorker().ReportProgress(100);
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.listView, "Export");
      }
    }

    private void ExportToStreamAsync(ListViewSpreadExportDataSnapshot listViewData, Stream stream)
    {
      int percentProgress = 0;
      int num1 = 0;
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      for (int index = 0; index < listViewData.ExportRows.Count; ++index)
      {
        if (num1 > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          num1 = 0;
        }
        int num2 = this.AddRow(listViewData.ExportRows[index], num1);
        int num3 = index * 100 / listViewData.ExportRows.Count;
        if (percentProgress != num3)
        {
          percentProgress = num3;
          this.GetWorker().ReportProgress(percentProgress);
        }
        num1 += num2;
      }
      if (this.ExportChildItemsGrouped)
        this.GroupWorksheetRows(num1);
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
    }

    private List<ListViewSpreadExportRow> GetListViewDataSnapshot()
    {
      List<ListViewSpreadExportRow> viewSpreadExportRowList = new List<ListViewSpreadExportRow>();
      if (this.ExportVisualSettings)
        this.elementProvider = this.listView.ListViewElement.ViewElement.ViewElement.ElementProvider;
      bool isItemInGroup = false;
      int currentRowIndex = 0;
      bool flag = true;
      bool exportChildNodesHidden = false;
      ListViewTraverser traverser = new ListViewTraverser(this.listView.ListViewElement);
      if (this.ExportChildItemsGrouped)
        this.InitializeRowGroupDataStructures();
      if (this.viewType == ListViewType.IconsView && this.listView.ListViewElement.ViewElement.Orientation == Orientation.Horizontal)
      {
        List<List<ListViewDataItem>> iconsViewCellMatrix = this.CreateIconsViewCellMatrix(traverser);
        for (int index1 = 0; index1 < iconsViewCellMatrix.Count; ++index1)
        {
          ListViewSpreadExportRow viewSpreadExportRow = new ListViewSpreadExportRow();
          for (int index2 = 0; index2 < iconsViewCellMatrix[index1].Count; ++index2)
          {
            ListViewDataItem listViewDataItem = iconsViewCellMatrix[index1][index2];
            ListViewSpreadExportCell spreadExportCell = this.CreateSpreadExportCell(listViewDataItem, exportChildNodesHidden);
            BaseListViewVisualItem visualItem = this.CreateVisualItem(listViewDataItem);
            this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) visualItem);
            this.ReleaseVisualItem(visualItem);
            this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, listViewDataItem, currentRowIndex));
            viewSpreadExportRow.Cells.Add(spreadExportCell);
          }
          int num = 1;
          if (traverser.Current.Image != null && this.ExportImages && (traverser.Current.TextImageRelation == TextImageRelation.TextAboveImage || traverser.Current.TextImageRelation == TextImageRelation.ImageAboveText))
            num = 2;
          viewSpreadExportRowList.Add(viewSpreadExportRow);
          currentRowIndex += num;
        }
      }
      else
      {
        while (traverser.MoveNext())
        {
          if (this.viewType == ListViewType.DetailsView)
          {
            if (this.columnTraverser == null)
              this.columnTraverser = (this.listView.ListViewElement.ViewElement as DetailListViewElement).ColumnScroller.Traverser as ListViewColumnTraverser;
            if (flag)
            {
              ListViewSpreadExportRow headerRow = this.CreateHeaderRow(traverser.Current);
              viewSpreadExportRowList.Add(headerRow);
              ++currentRowIndex;
            }
          }
          ListViewSpreadExportRow exportRow = this.CreateExportRow(traverser, ref exportChildNodesHidden, ref isItemInGroup, ref currentRowIndex);
          flag = false;
          int num = 1;
          if (traverser.Current.Image != null && this.ExportImages && (traverser.Current.TextImageRelation == TextImageRelation.TextAboveImage || traverser.Current.TextImageRelation == TextImageRelation.ImageAboveText))
            num = 2;
          viewSpreadExportRowList.Add(exportRow);
          if (this.ExportChildItemsGrouped)
            this.ProcessCurrentRowGrouping(traverser.Current is ListViewDataItemGroup ? 0 : 1, currentRowIndex);
          currentRowIndex += num;
        }
      }
      return viewSpreadExportRowList;
    }

    private void RunExportCall(Stream exportStream)
    {
      ListViewExportState state = this.SaveListViewState();
      if (this.ExportVisualSettings)
        this.elementProvider = this.listView.ListViewElement.ViewElement.ViewElement.ElementProvider;
      try
      {
        this.spreadExportRenderer.GetFormatProvider(this.ExportFormat);
        this.spreadExportRenderer.CreateWorkbook();
        this.ExportToStream(exportStream);
        this.OnExportCompleted(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.listView, "Export");
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreListViewState(state);
      }
    }

    private void RunExportCall(string fileName)
    {
      ListViewExportState state = this.SaveListViewState();
      if (this.ExportVisualSettings)
        this.elementProvider = this.listView.ListViewElement.ViewElement.ViewElement.ElementProvider;
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
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.listView, "Export", (object) fileName);
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreListViewState(state);
      }
    }

    private void ExportToStream(Stream stream)
    {
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      bool isItemInGroup = false;
      int currentRowIndex = 0;
      bool flag = true;
      bool exportChildNodesHidden = false;
      ListViewTraverser traverser = new ListViewTraverser(this.listView.ListViewElement);
      if (this.ExportChildItemsGrouped)
        this.InitializeRowGroupDataStructures();
      if (this.viewType == ListViewType.IconsView && this.listView.ListViewElement.ViewElement.Orientation == Orientation.Horizontal)
      {
        this.AddIconsListViewHorizontalOrientationRows(this.CreateIconsViewCellMatrix(traverser));
      }
      else
      {
        while (traverser.MoveNext())
        {
          if (this.applicationDoEvents)
            Application.DoEvents();
          if (currentRowIndex > this.sheetMaxRowsNumber)
          {
            this.spreadExportRenderer.AddWorksheet(this.SheetName);
            currentRowIndex = 0;
          }
          if (this.viewType == ListViewType.DetailsView)
          {
            if (this.columnTraverser == null)
              this.columnTraverser = (this.listView.ListViewElement.ViewElement as DetailListViewElement).ColumnScroller.Traverser as ListViewColumnTraverser;
            if (flag)
            {
              ListViewSpreadExportRow headerRow = this.CreateHeaderRow(traverser.Current);
              currentRowIndex += this.AddRow(headerRow, currentRowIndex);
            }
          }
          ListViewSpreadExportRow exportRow = this.CreateExportRow(traverser, ref exportChildNodesHidden, ref isItemInGroup, ref currentRowIndex);
          flag = false;
          int num = this.AddRow(exportRow, currentRowIndex);
          if (this.ExportChildItemsGrouped)
            this.ProcessCurrentRowGrouping(traverser.Current is ListViewDataItemGroup ? 0 : 1, currentRowIndex);
          currentRowIndex += num;
        }
        if (this.ExportChildItemsGrouped)
          this.GroupWorksheetRows(currentRowIndex);
      }
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
    }

    private void AddIconsListViewHorizontalOrientationRows(List<List<ListViewDataItem>> cellMatrix)
    {
      int num1 = 0;
      for (int index1 = 0; index1 < cellMatrix.Count; ++index1)
      {
        if (this.applicationDoEvents)
          Application.DoEvents();
        if (num1 > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          num1 = 0;
        }
        ListViewSpreadExportRow row = new ListViewSpreadExportRow();
        for (int index2 = 0; index2 < cellMatrix[index1].Count; ++index2)
        {
          ListViewDataItem listViewDataItem = cellMatrix[index1][index2];
          ListViewSpreadExportCell spreadExportCell = this.CreateSpreadExportCell(listViewDataItem, false);
          BaseListViewVisualItem visualItem = this.CreateVisualItem(listViewDataItem);
          this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) visualItem);
          this.ReleaseVisualItem(visualItem);
          this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, listViewDataItem, num1));
          row.Cells.Add(spreadExportCell);
        }
        int num2 = this.AddRow(row, num1);
        num1 += num2;
      }
    }

    private ListViewSpreadExportRow CreateExportRow(
      ListViewTraverser traverser,
      ref bool exportChildNodesHidden,
      ref bool isItemInGroup,
      ref int currentRowIndex)
    {
      ListViewDataItem current1 = traverser.Current;
      ListViewSpreadExportRow viewSpreadExportRow = new ListViewSpreadExportRow();
      if (current1 is ListViewDataItemGroup)
      {
        exportChildNodesHidden = false;
        isItemInGroup = true;
        ListViewSpreadExportCell spreadExportCell = this.CreateSpreadExportCell(current1, exportChildNodesHidden);
        ListViewDataItemGroup viewDataItemGroup = current1 as ListViewDataItemGroup;
        BaseListViewVisualItem visualItem = this.CreateVisualItem(current1);
        this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) visualItem);
        this.ReleaseVisualItem(visualItem);
        int num = 0;
        if (this.viewType == ListViewType.ListView || this.viewType == ListViewType.IconsView)
        {
          foreach (ListViewDataItem listViewDataItem in viewDataItemGroup.Items)
          {
            ++num;
            if (this.ExportImages && listViewDataItem.TextImageRelation.ToString().Contains("Before"))
              ++num;
            if (this.viewType == ListViewType.IconsView)
            {
              if (listViewDataItem.IsLastInRow)
                break;
            }
            if (this.viewType == ListViewType.ListView)
            {
              ++num;
              break;
            }
          }
        }
        else
        {
          num = this.columnTraverser.Collection.Count;
          if (this.ExportImages)
            ++num;
        }
        spreadExportCell.ColSpan = num;
        this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, (ListViewDataItem) viewDataItemGroup, currentRowIndex));
        viewSpreadExportRow.Cells.Add(spreadExportCell);
        if (!exportChildNodesHidden)
          exportChildNodesHidden = !viewDataItemGroup.Expanded && this.CollapsedItemOption == HiddenOption.ExportAsHidden;
        if (!viewDataItemGroup.Expanded && this.CollapsedItemOption != HiddenOption.DoNotExport)
          viewDataItemGroup.Expanded = true;
      }
      else if (this.viewType == ListViewType.ListView)
      {
        BaseListViewVisualItem visualItem = this.CreateVisualItem(current1);
        if (isItemInGroup)
        {
          ListViewSpreadExportCell spreadExportCell = new ListViewSpreadExportCell();
          spreadExportCell.Size = new Size(this.listView.GroupIndent, 0);
          this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) visualItem);
          this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, current1, currentRowIndex));
          viewSpreadExportRow.Cells.Add(spreadExportCell);
        }
        ListViewSpreadExportCell spreadExportCell1 = this.CreateSpreadExportCell(current1, exportChildNodesHidden);
        this.GetStylesFromVisualCell(spreadExportCell1, (LightVisualElement) visualItem);
        this.ReleaseVisualItem(visualItem);
        this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell1, current1, currentRowIndex));
        viewSpreadExportRow.Cells.Add(spreadExportCell1);
      }
      else if (this.viewType == ListViewType.IconsView)
      {
        ListViewSpreadExportCell spreadExportCell1 = this.CreateSpreadExportCell(current1, exportChildNodesHidden);
        BaseListViewVisualItem visualItem1 = this.CreateVisualItem(current1);
        this.GetStylesFromVisualCell(spreadExportCell1, (LightVisualElement) visualItem1);
        this.ReleaseVisualItem(visualItem1);
        this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell1, current1, currentRowIndex));
        viewSpreadExportRow.Cells.Add(spreadExportCell1);
        while (traverser.MoveNext())
        {
          ListViewDataItem current2 = traverser.Current;
          if (current2 is ListViewDataItemGroup)
          {
            traverser.MovePrevious();
            break;
          }
          ListViewSpreadExportCell spreadExportCell2 = this.CreateSpreadExportCell(current2, exportChildNodesHidden);
          BaseListViewVisualItem visualItem2 = this.CreateVisualItem(current2);
          this.GetStylesFromVisualCell(spreadExportCell2, (LightVisualElement) visualItem2);
          this.ReleaseVisualItem(visualItem2);
          this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell2, current2, currentRowIndex));
          viewSpreadExportRow.Cells.Add(spreadExportCell2);
          if (current2.IsLastInRow)
            break;
        }
      }
      else
      {
        this.columnTraverser.Reset();
        DetailListViewVisualItem listViewVisualItem = (DetailListViewVisualItem) null;
        if (this.ExportVisualSettings)
        {
          listViewVisualItem = this.CreateVisualItem(current1) as DetailListViewVisualItem;
          listViewVisualItem.Attach(current1, (object) null);
        }
        if (this.ExportImages)
        {
          ListViewSpreadExportCell spreadExportCell = new ListViewSpreadExportCell();
          spreadExportCell.Type = typeof (Image);
          spreadExportCell.Image = current1.Image;
          spreadExportCell.ImageAlignment = current1.ImageAlignment;
          spreadExportCell.TextImageRelation = current1.TextImageRelation;
          if (spreadExportCell.Image != null)
            spreadExportCell.Size = new Size(current1.Image.Size.Width, exportChildNodesHidden ? 0 : current1.ActualSize.Height);
          this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) listViewVisualItem);
          this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, current1, currentRowIndex));
          viewSpreadExportRow.Cells.Add(spreadExportCell);
        }
        while (this.columnTraverser.MoveNext())
        {
          ListViewDetailColumn current2 = this.columnTraverser.Current;
          object obj = current1[current2];
          ListViewSpreadExportCell spreadExportCell = new ListViewSpreadExportCell();
          spreadExportCell.Type = current1.GetType();
          spreadExportCell.TextAlignment = current1.TextAlignment;
          spreadExportCell.Value = obj;
          spreadExportCell.Size = new Size((int) current2.Width, exportChildNodesHidden ? 0 : current1.ActualSize.Height);
          if (this.ExportVisualSettings)
          {
            DetailListViewDataCellElement element = (listViewVisualItem.CellsContainer.ElementProvider as DetailListViewDataCellElementProvider).GetElement(current2, (object) null) as DetailListViewDataCellElement;
            listViewVisualItem.CellsContainer.Children.Add((RadElement) element);
            element.Synchronize();
            this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) element);
            listViewVisualItem.CellsContainer.Children.Remove((RadElement) element);
            element.Dispose();
          }
          this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, current1, currentRowIndex));
          viewSpreadExportRow.Cells.Add(spreadExportCell);
        }
        if (listViewVisualItem != null)
          this.ReleaseVisualItem((BaseListViewVisualItem) listViewVisualItem);
        this.columnTraverser.Reset();
      }
      return viewSpreadExportRow;
    }

    private ListViewSpreadExportRow CreateHeaderRow(
      ListViewDataItem listViewDataItem)
    {
      ListViewSpreadExportRow viewSpreadExportRow = new ListViewSpreadExportRow();
      if (this.ExportImages)
      {
        ListViewSpreadExportCell spreadExportCell = new ListViewSpreadExportCell();
        BaseListViewVisualItem visualItem = this.CreateVisualItem(listViewDataItem);
        this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) visualItem);
        this.ReleaseVisualItem(visualItem);
        this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, listViewDataItem, 0));
        viewSpreadExportRow.Cells.Add(spreadExportCell);
      }
      DetailListViewColumnContainer columnContainer = (this.listView.ListViewElement.ViewElement as DetailListViewElement).ColumnContainer;
      foreach (ListViewDetailColumn data in (IEnumerable<ListViewDetailColumn>) this.columnTraverser.Collection)
      {
        if (data.Visible)
        {
          ListViewSpreadExportCell spreadExportCell = new ListViewSpreadExportCell();
          spreadExportCell.Text = data.HeaderText;
          spreadExportCell.Size = new Size((int) data.Width, (int) columnContainer.DesiredSize.Height);
          spreadExportCell.Font = Control.DefaultFont;
          spreadExportCell.Type = typeof (DetailListViewHeaderCellElement);
          if (this.ExportVisualSettings)
          {
            DetailListViewHeaderCellElement element = (columnContainer.ElementProvider as DetailListViewHeaderCellElementProvider).GetElement(data, (object) null) as DetailListViewHeaderCellElement;
            columnContainer.Children.Add((RadElement) element);
            element.Attach(data, (object) null);
            element.Synchronize();
            this.GetStylesFromVisualCell(spreadExportCell, (LightVisualElement) element);
            element.Detach();
            columnContainer.Children.Remove((RadElement) element);
            element.Dispose();
          }
          this.OnCellFormatting(new ListViewSpreadExportCellFormattingEventArgs(spreadExportCell, listViewDataItem, 0));
          viewSpreadExportRow.Cells.Add(spreadExportCell);
        }
      }
      return viewSpreadExportRow;
    }

    protected virtual ListViewSpreadExportCell CreateSpreadExportCell(
      ListViewDataItem listViewDataItem,
      bool exportAsHidden)
    {
      ListViewSpreadExportCell spreadExportCell = new ListViewSpreadExportCell();
      spreadExportCell.Type = listViewDataItem.GetType();
      spreadExportCell.Text = listViewDataItem.Text;
      spreadExportCell.TextAlignment = listViewDataItem.TextAlignment;
      spreadExportCell.Size = new Size(listViewDataItem.ActualSize.Width, exportAsHidden ? 0 : listViewDataItem.ActualSize.Height);
      if (this.ExportImages && listViewDataItem.Image != null)
      {
        spreadExportCell.Image = listViewDataItem.Image;
        spreadExportCell.ImageAlignment = listViewDataItem.ImageAlignment;
        spreadExportCell.TextImageRelation = listViewDataItem.TextImageRelation;
      }
      return spreadExportCell;
    }

    protected virtual BaseListViewVisualItem CreateVisualItem(
      ListViewDataItem listViewDataItem)
    {
      BaseListViewVisualItem listViewVisualItem = (BaseListViewVisualItem) null;
      if (this.ExportVisualSettings)
      {
        listViewVisualItem = this.elementProvider.GetElement(listViewDataItem, (object) null) as BaseListViewVisualItem;
        this.listView.ListViewElement.ViewElement.ViewElement.Children.Add((RadElement) listViewVisualItem);
        listViewVisualItem.Attach(listViewDataItem, (object) null);
      }
      return listViewVisualItem;
    }

    protected virtual void ReleaseVisualItem(BaseListViewVisualItem visualItem)
    {
      if (visualItem == null)
        return;
      this.elementProvider.CacheElement((IVirtualizedElement<ListViewDataItem>) visualItem);
      visualItem.Detach();
      this.listView.ListViewElement.ViewElement.ViewElement.Children.Remove((RadElement) visualItem);
      visualItem = (BaseListViewVisualItem) null;
    }

    private int AddRow(ListViewSpreadExportRow row, int currentRowIndex)
    {
      int num1 = 0;
      int num2 = 1;
      int num3 = -1;
      for (int index = 0; index < row.Cells.Count; ++index)
      {
        ListViewSpreadExportCell cell = row.Cells[index];
        if ((object) cell.Type == null && cell.Image == null && cell.Text == null)
        {
          this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Size.Width, false);
          this.CreateTextCell(cell, currentRowIndex, num1);
        }
        else if ((object) cell.Type == (object) typeof (Image))
        {
          if (cell.Size.Height > num3)
          {
            num3 = cell.Size.Height;
            this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell.Size.Height, true);
          }
          if (cell.Image != null)
            this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Image.Size.Width, false);
          this.CreateImageOverCell(cell, currentRowIndex, num1);
        }
        else if (cell.Image != null && this.ExportImages)
        {
          switch (cell.TextImageRelation)
          {
            case TextImageRelation.Overlay:
              if (cell.Size.Height > num3)
              {
                num3 = cell.Size.Height;
                this.spreadExportRenderer.SetWorksheetRowHeight(num1, cell.Size.Height, true);
              }
              this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Size.Width, false);
              this.CreateImageOverCell(cell, currentRowIndex, num1);
              this.CreateTextCell(cell, currentRowIndex, num1);
              break;
            case TextImageRelation.ImageAboveText:
              this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Size.Width, false);
              if (cell.Image.Height > num3)
              {
                num3 = cell.Image.Height;
                this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell.Image.Height, true);
              }
              this.CreateImageOverCell(cell, currentRowIndex, num1);
              num2 = 2;
              this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex + 1, cell.Size.Height - cell.Image.Height, true);
              this.CreateTextCell(cell, currentRowIndex + 1, num1);
              break;
            case TextImageRelation.TextAboveImage:
              this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Size.Width, false);
              if (cell.Size.Height - cell.Image.Height > num3)
              {
                num3 = cell.Size.Height - cell.Image.Height;
                this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell.Size.Height - cell.Image.Height, true);
              }
              this.CreateTextCell(cell, currentRowIndex, num1);
              num2 = 2;
              this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex + 1, cell.Image.Height, true);
              this.CreateImageOverCell(cell, currentRowIndex + 1, num1);
              break;
            case TextImageRelation.ImageBeforeText:
              if (cell.Size.Height > num3)
              {
                num3 = cell.Size.Height;
                this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell.Size.Height, true);
              }
              this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Image.Size.Width, false);
              this.CreateImageOverCell(cell, currentRowIndex, num1);
              ++num1;
              this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) (cell.Size.Width - cell.Image.Size.Width), false);
              this.CreateTextCell(cell, currentRowIndex, num1);
              break;
            case TextImageRelation.TextBeforeImage:
              if (cell.Size.Height > num3)
              {
                num3 = cell.Size.Height;
                this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell.Size.Height, true);
              }
              this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) (cell.Size.Width - cell.Image.Size.Width), false);
              this.CreateTextCell(cell, currentRowIndex, num1);
              ++num1;
              this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Image.Size.Width, false);
              this.CreateImageOverCell(cell, currentRowIndex, num1);
              break;
          }
        }
        else
        {
          if (cell.Size.Height > num3)
          {
            num3 = cell.Size.Height;
            this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell.Size.Height, true);
          }
          if ((object) cell.Type == (object) typeof (ListViewDataItemGroup))
          {
            this.spreadExportRenderer.CreateCellSelection(currentRowIndex, num1, currentRowIndex, num1 + cell.ColSpan - 1);
            this.spreadExportRenderer.MergeCellSelection();
          }
          else
          {
            this.spreadExportRenderer.SetWorksheetColumnWidth(num1, (double) cell.Size.Width, false);
            this.spreadExportRenderer.CreateCellSelection(currentRowIndex, num1);
          }
          this.CreateTextCell(cell, currentRowIndex, num1);
        }
        ++num1;
      }
      return num2;
    }

    protected virtual void CreateImageOverCell(
      ListViewSpreadExportCell cell,
      int currentRowIndex,
      int currentColumnIndex)
    {
      if (cell.Image != null)
      {
        int offsetX = 0;
        int offsetY = 0;
        if (cell.ImageAlignment.ToString().Contains("Middle"))
          offsetY = (cell.Size.Height - cell.Image.Height) / 2;
        else if (cell.ImageAlignment.ToString().Contains("Bottom"))
          offsetY = cell.Size.Height - cell.Image.Height;
        if (offsetY < 0)
          offsetY = 0;
        Image image = cell.Image;
        byte[] byteArray = this.ConvertImageToByteArray(image);
        this.spreadExportRenderer.CreateFloatingImage(currentRowIndex, currentColumnIndex, offsetX, offsetY, byteArray, "png", image.Width, image.Height, 0);
      }
      this.spreadExportRenderer.CreateCellSelection(currentRowIndex, currentColumnIndex);
      if (!this.ExportVisualSettings && this.CellFormatting == null)
        return;
      Font font = cell.Font ?? Control.DefaultFont;
      this.spreadExportRenderer.CreateCellStyleInfo(cell.BackColor, cell.ForeColor, font.FontFamily, (double) font.Size, font.Bold, font.Italic, font.Underline, cell.TextAlignment, cell.TextWrap, cell.BorderBoxStyle, cell.BorderColor, cell.BorderTopColor, cell.BorderBottomColor, cell.BorderRightColor, cell.BorderLeftColor);
    }

    protected virtual void CreateTextCell(
      ListViewSpreadExportCell cell,
      int currentRowIndex,
      int currentColumnIndex)
    {
      this.spreadExportRenderer.CreateCellSelection(currentRowIndex, currentColumnIndex);
      if (!string.IsNullOrEmpty(cell.FormatString))
        this.spreadExportRenderer.SetCellSelectionFormat(cell.FormatString);
      if (cell.Value != null)
        this.spreadExportRenderer.SetCellSelectionValue(new DataTypeConvertor().ConvertToDataType(cell.Value), cell.Value);
      else if (!string.IsNullOrEmpty(cell.Text))
        this.spreadExportRenderer.SetCellSelectionValue(cell.Text);
      object cellSelectionValue = this.spreadExportRenderer.GetCellSelectionValue();
      if (cellSelectionValue is DateTime && !this.CheckDateTimeValue((DateTime) cellSelectionValue))
        throw new FormatException("The DateTime value is not supported in Excel!");
      if (!this.ExportVisualSettings && this.CellFormatting == null)
        return;
      Font font = cell.Font ?? Control.DefaultFont;
      this.spreadExportRenderer.CreateCellStyleInfo(cell.BackColor, cell.ForeColor, font.FontFamily, (double) font.Size, font.Bold, font.Italic, font.Underline, cell.TextAlignment, cell.TextWrap, cell.BorderBoxStyle, cell.BorderColor, cell.BorderTopColor, cell.BorderBottomColor, cell.BorderRightColor, cell.BorderLeftColor);
    }

    private void InitializeRowGroupDataStructures()
    {
      this.hierarchyRowGroups = new Stack<ListViewSpreadExport.RowGroup>();
      this.readyToExportRowGroups = new Stack<ListViewSpreadExport.RowGroup>();
      this.lastRowHierarchyLevel = 0;
    }

    private void ProcessCurrentRowGrouping(int currentRowHierarchyLevel, int currentRowNum)
    {
      if (currentRowHierarchyLevel > this.lastRowHierarchyLevel)
        this.hierarchyRowGroups.Push(new ListViewSpreadExport.RowGroup(currentRowNum, currentRowHierarchyLevel));
      else if (currentRowHierarchyLevel < this.lastRowHierarchyLevel)
      {
        for (int index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0; currentRowHierarchyLevel < index && this.hierarchyRowGroups.Count > 0; index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0)
        {
          ListViewSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
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
        ListViewSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
        rowGroup.EndRow = currentRowNum - 1;
        this.readyToExportRowGroups.Push(rowGroup);
      }
      while (this.readyToExportRowGroups.Count > 0)
      {
        ListViewSpreadExport.RowGroup rowGroup = this.readyToExportRowGroups.Pop();
        this.spreadExportRenderer.GroupRows(rowGroup.StartRow, rowGroup.EndRow, rowGroup.Level);
      }
    }

    private List<List<ListViewDataItem>> CreateIconsViewCellMatrix(
      ListViewTraverser traverser)
    {
      List<List<ListViewDataItem>> listViewDataItemListList = new List<List<ListViewDataItem>>();
      int index = 0;
      while (traverser.MoveNext())
      {
        ListViewDataItem current = traverser.Current;
        if (listViewDataItemListList.Count == index)
          listViewDataItemListList.Add(new List<ListViewDataItem>());
        listViewDataItemListList[index].Add(current);
        ++index;
        if (current.IsLastInRow)
          index = 0;
      }
      return listViewDataItemListList;
    }

    private ListViewExportState SaveListViewState()
    {
      ListViewExportState listViewExportState = new ListViewExportState() { CurrentItem = this.listView.CurrentItem, HorizontalScrollbarValue = this.listView.ListViewElement.ViewElement.HScrollBar.Value, VerticalScrolbarValue = this.listView.ListViewElement.ViewElement.VScrollBar.Value, CurrentColumn = this.listView.CurrentColumn };
      this.listView.CurrentColumn = (ListViewDetailColumn) null;
      this.listView.CurrentItem = (ListViewDataItem) null;
      foreach (ListViewDataItem selectedItem in (ReadOnlyCollection<ListViewDataItem>) this.listView.SelectedItems)
        listViewExportState.SelectedItems.Add(selectedItem);
      foreach (ListViewDataItemGroup group in this.listView.Groups)
      {
        if (!group.Expanded)
          listViewExportState.CollapsedItems.Add(group);
      }
      this.listView.SelectedItems.Clear();
      return listViewExportState;
    }

    private void RestoreListViewState(ListViewExportState state)
    {
      foreach (ListViewDataItem selectedItem in state.SelectedItems)
        selectedItem.Selected = true;
      foreach (ListViewDataItemGroup collapsedItem in state.CollapsedItems)
        collapsedItem.Expanded = false;
      this.listView.CurrentColumn = state.CurrentColumn;
      this.listView.CurrentItem = state.CurrentItem;
      this.listView.ListViewElement.ViewElement.HScrollBar.Value = state.HorizontalScrollbarValue;
      this.listView.ListViewElement.ViewElement.VScrollBar.Value = state.VerticalScrolbarValue;
    }

    protected virtual void GetStylesFromVisualCell(
      ListViewSpreadExportCell cell,
      LightVisualElement visualCell)
    {
      if (visualCell == null)
        return;
      cell.Font = visualCell.Font;
      cell.TextAlignment = visualCell.TextAlignment;
      cell.TextWrap = visualCell.TextWrap;
      cell.BackColor = this.GetBackColor(visualCell);
      cell.ForeColor = visualCell.ForeColor;
      cell.BorderBoxStyle = visualCell.BorderBoxStyle;
      cell.BorderColor = visualCell.BorderColor;
      cell.BorderLeftColor = visualCell.BorderLeftColor;
      cell.BorderTopColor = visualCell.BorderTopColor;
      cell.BorderRightColor = visualCell.BorderRightColor;
      cell.BorderBottomColor = visualCell.BorderBottomColor;
    }

    private byte[] ConvertImageToByteArray(Image image)
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
