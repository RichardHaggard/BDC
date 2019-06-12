// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.TreeViewSpreadExport
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
  public class TreeViewSpreadExport
  {
    private int sheetMaxRowsNumber = 65536;
    private Size defaultItemSize = Size.Empty;
    private int nodeIndent = 20;
    private RadTreeView treeView;
    private TreeViewElementProvider elementProvider;
    private ISpreadExportRenderer spreadExportRenderer;
    private SpreadExportFormat exportFormat;
    private bool exportVisualSettings;
    private bool applicationDoEvents;
    private int depthOfTree;
    private HiddenOption collapsedNodeOption;
    private BackgroundWorker worker;
    private List<int> columnWidths;
    private Stack<TreeViewSpreadExport.RowGroup> hierarchyRowGroups;
    private Stack<TreeViewSpreadExport.RowGroup> readyToExportRowGroups;
    private int lastRowHierarchyLevel;

    public TreeViewSpreadExport(RadTreeView radTreeView)
    {
      this.treeView = radTreeView;
    }

    public TreeViewSpreadExport(RadTreeView radTreeView, SpreadExportFormat exportFormat)
      : this(radTreeView)
    {
      this.ExportFormat = exportFormat;
    }

    public string SheetName { get; set; }

    [DefaultValue(FileExportMode.NewSheetInExistingFile)]
    public FileExportMode FileExportMode { get; set; }

    public bool ExportImages { get; set; }

    public bool ExportChildNodesGrouped { get; set; }

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

    public int NodeIndent
    {
      get
      {
        return this.nodeIndent;
      }
      set
      {
        if (value < 0)
          throw new ArgumentException("NodeIndent cannot be negative.", nameof (NodeIndent));
        this.nodeIndent = value;
      }
    }

    public HiddenOption CollapsedNodeOption
    {
      get
      {
        if (this.collapsedNodeOption == HiddenOption.ExportAsHidden && this.ExportFormat == SpreadExportFormat.Pdf)
          return HiddenOption.DoNotExport;
        return this.collapsedNodeOption;
      }
      set
      {
        this.collapsedNodeOption = value;
      }
    }

    public event TreeViewSpreadExportCellFormattingEventHandler CellFormatting;

    protected virtual void OnCellFormatting(TreeViewSpreadExportCellFormattingEventArgs e)
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
      if (this.treeView.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.treeView.Invoke((Delegate) new TreeViewSpreadExport.RunExportCallback(this.RunExportCall), (object) fileName);
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
      List<TreeViewSpreadExportRow> exportRows = (List<TreeViewSpreadExportRow>) null;
      if (this.treeView.InvokeRequired)
      {
        this.treeView.Invoke((Delegate) (() =>
        {
          TreeViewExportState state = this.SaveTreeViewState();
          exportRows = this.GetTreeViewDataSnapshot();
          this.RestoreTreeViewState(state);
        }));
      }
      else
      {
        TreeViewExportState state = this.SaveTreeViewState();
        exportRows = this.GetTreeViewDataSnapshot();
        this.RestoreTreeViewState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new TreeViewSpreadExportDataSnapshot(fileName, exportRows));
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
      if (this.treeView.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.treeView.Invoke((Delegate) new TreeViewSpreadExport.RunExportToStreamCallback(this.RunExportCall), (object) exportStream);
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
      List<TreeViewSpreadExportRow> exportRows = (List<TreeViewSpreadExportRow>) null;
      if (this.treeView.InvokeRequired)
      {
        this.treeView.Invoke((Delegate) (() =>
        {
          TreeViewExportState state = this.SaveTreeViewState();
          exportRows = this.GetTreeViewDataSnapshot();
          this.RestoreTreeViewState(state);
        }));
      }
      else
      {
        TreeViewExportState state = this.SaveTreeViewState();
        exportRows = this.GetTreeViewDataSnapshot();
        this.RestoreTreeViewState(state);
      }
      this.GetWorker().RunWorkerAsync((object) new List<object>(2)
      {
        (object) new TreeViewSpreadExportDataSnapshot(string.Empty, exportRows),
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
      TreeViewSpreadExportDataSnapshot treeViewData1 = e.Argument as TreeViewSpreadExportDataSnapshot;
      if (treeViewData1 != null)
      {
        string path = Path.GetDirectoryName(treeViewData1.FilePath) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(treeViewData1.FilePath) + this.spreadExportRenderer.GetFileExtension(this.ExportFormat);
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
          this.ExportToStreamAsync(treeViewData1, (Stream) fileStream);
        this.GetWorker().ReportProgress(100);
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.treeView, "Export", (object) path);
      }
      else
      {
        this.spreadExportRenderer.CreateWorkbook();
        List<object> objectList = e.Argument as List<object>;
        TreeViewSpreadExportDataSnapshot treeViewData2 = objectList[0] as TreeViewSpreadExportDataSnapshot;
        Stream stream = objectList[1] as Stream;
        this.ExportToStreamAsync(treeViewData2, stream);
        this.GetWorker().ReportProgress(100);
        e.Result = (object) stream;
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.treeView, "Export");
      }
    }

    private void ExportToStreamAsync(TreeViewSpreadExportDataSnapshot treeViewData, Stream stream)
    {
      int percentProgress = 0;
      int num1 = 0;
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      for (int index = 0; index < treeViewData.ExportRows.Count; ++index)
      {
        if (num1 > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          num1 = 0;
          this.columnWidths.Clear();
        }
        this.AddRow(treeViewData.ExportRows[index], num1);
        int num2 = index * 100 / treeViewData.ExportRows.Count;
        if (percentProgress != num2)
        {
          percentProgress = num2;
          this.GetWorker().ReportProgress(percentProgress);
        }
        ++num1;
      }
      if (this.ExportChildNodesGrouped)
        this.GroupWorksheetRows(num1);
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
    }

    private List<TreeViewSpreadExportRow> GetTreeViewDataSnapshot()
    {
      List<TreeViewSpreadExportRow> viewSpreadExportRowList = new List<TreeViewSpreadExportRow>();
      this.defaultItemSize = new Size(this.treeView.Width, this.treeView.ItemHeight);
      if (this.ExportVisualSettings)
        this.elementProvider = this.treeView.TreeViewElement.ViewElement.ElementProvider as TreeViewElementProvider;
      int num = 0;
      IEnumerator<RadTreeNode> enumerator = this.EnumerateNodes().GetEnumerator();
      bool skipHiddenChildNodes = false;
      bool exportChildNodesHidden = false;
      int collapsedParentLevel = 0;
      if (this.ExportChildNodesGrouped)
        this.InitializeRowGroupDataStructures();
      while (enumerator.MoveNext())
      {
        RadTreeNode current = enumerator.Current;
        if (!skipHiddenChildNodes || collapsedParentLevel >= current.Level)
        {
          skipHiddenChildNodes = false;
          TreeViewSpreadExportRow exportRow = this.CreateExportRow(current, num, ref skipHiddenChildNodes, ref collapsedParentLevel, ref exportChildNodesHidden);
          viewSpreadExportRowList.Add(exportRow);
          if (this.ExportChildNodesGrouped)
            this.ProcessCurrentRowGrouping(current.Level, num);
          ++num;
        }
      }
      return viewSpreadExportRowList;
    }

    private IEnumerable<RadTreeNode> EnumerateNodes()
    {
      foreach (RadTreeNode node in (Collection<RadTreeNode>) this.treeView.Nodes)
      {
        foreach (RadTreeNode enumerateNode in this.EnumerateNodes(node))
          yield return enumerateNode;
      }
    }

    private IEnumerable<RadTreeNode> EnumerateNodes(RadTreeNode node)
    {
      yield return node;
      foreach (RadTreeNode node1 in (Collection<RadTreeNode>) node.Nodes)
      {
        foreach (RadTreeNode enumerateNode in this.EnumerateNodes(node1))
          yield return enumerateNode;
      }
    }

    private void RunExportCall(Stream exportStream)
    {
      TreeViewExportState state = this.SaveTreeViewState();
      this.columnWidths = new List<int>();
      this.defaultItemSize = new Size(this.treeView.Width, this.treeView.ItemHeight);
      if (this.ExportVisualSettings)
        this.elementProvider = this.treeView.TreeViewElement.ViewElement.ElementProvider as TreeViewElementProvider;
      try
      {
        this.spreadExportRenderer.GetFormatProvider(this.ExportFormat);
        this.spreadExportRenderer.CreateWorkbook();
        this.ExportToStream(exportStream);
        this.OnExportCompleted(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.treeView, "Export");
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreTreeViewState(state);
      }
    }

    private void RunExportCall(string fileName)
    {
      TreeViewExportState state = this.SaveTreeViewState();
      this.columnWidths = new List<int>();
      this.defaultItemSize = new Size(this.treeView.Width, this.treeView.ItemHeight);
      if (this.ExportVisualSettings)
        this.elementProvider = this.treeView.TreeViewElement.ViewElement.ElementProvider as TreeViewElementProvider;
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
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.treeView, "Export", (object) fileName);
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreTreeViewState(state);
      }
    }

    private void ExportToStream(Stream stream)
    {
      this.spreadExportRenderer.AddWorksheet(this.SheetName);
      int num = 0;
      IEnumerator<RadTreeNode> enumerator = this.EnumerateNodes().GetEnumerator();
      bool skipHiddenChildNodes = false;
      bool exportChildNodesHidden = false;
      int collapsedParentLevel = 0;
      if (this.ExportChildNodesGrouped)
        this.InitializeRowGroupDataStructures();
      while (enumerator.MoveNext())
      {
        if (this.applicationDoEvents)
          Application.DoEvents();
        if (num > this.sheetMaxRowsNumber)
        {
          this.spreadExportRenderer.AddWorksheet(this.SheetName);
          num = 0;
          this.columnWidths.Clear();
        }
        RadTreeNode current = enumerator.Current;
        if (!skipHiddenChildNodes || collapsedParentLevel >= current.Level)
        {
          skipHiddenChildNodes = false;
          this.AddRow(this.CreateExportRow(current, num, ref skipHiddenChildNodes, ref collapsedParentLevel, ref exportChildNodesHidden), num);
          if (this.ExportChildNodesGrouped)
            this.ProcessCurrentRowGrouping(current.Level, num);
          ++num;
        }
      }
      if (this.ExportChildNodesGrouped)
        this.GroupWorksheetRows(num);
      this.spreadExportRenderer.CallWorkbookCreated();
      this.spreadExportRenderer.Export(stream);
    }

    private TreeViewSpreadExportRow CreateExportRow(
      RadTreeNode treeNode,
      int currentRowIndex,
      ref bool skipHiddenChildNodes,
      ref int collapsedParentLevel,
      ref bool exportChildNodesHidden)
    {
      if (!treeNode.Expanded && this.CollapsedNodeOption == HiddenOption.DoNotExport)
      {
        skipHiddenChildNodes = true;
        collapsedParentLevel = treeNode.Level;
      }
      if (exportChildNodesHidden && collapsedParentLevel >= treeNode.Level)
        exportChildNodesHidden = false;
      TreeNodeElement treeNodeElement1 = (TreeNodeElement) null;
      TreeNodeElement treeNodeElement2 = (TreeNodeElement) null;
      int num = 1;
      RadTreeViewVirtualizedContainer virtualizedContainer = (RadTreeViewVirtualizedContainer) null;
      int height = treeNode.ActualSize.Height == 0 ? this.defaultItemSize.Height : treeNode.ActualSize.Height;
      if (this.ExportVisualSettings)
      {
        virtualizedContainer = this.treeView.TreeViewElement.ViewElement as RadTreeViewVirtualizedContainer;
        if (this.treeView.TreeViewElement.AllowAlternatingRowColor && currentRowIndex % 2 != 0)
        {
          treeNodeElement1 = new TreeNodeElement();
          virtualizedContainer.Children.Add((RadElement) treeNodeElement1);
          num = 2;
        }
        treeNodeElement2 = this.elementProvider.GetElement(treeNode, (object) null) as TreeNodeElement;
        virtualizedContainer.Children.Add((RadElement) treeNodeElement2);
        treeNodeElement2.Attach(treeNode, (object) null);
      }
      TreeViewSpreadExportRow viewSpreadExportRow = new TreeViewSpreadExportRow();
      for (int index = 0; index < treeNode.Level; ++index)
      {
        TreeViewSpreadExportIndentCell exportIndentCell = new TreeViewSpreadExportIndentCell();
        exportIndentCell.Size = new Size(this.NodeIndent, exportChildNodesHidden ? 0 : -1);
        if (this.ExportVisualSettings && treeNodeElement2.LinesContainerElement.LayoutableChildrenCount > 0)
          this.GetStylesFromVisualCell((TreeViewSpreadExportCell) exportIndentCell, (LightVisualElement) treeNodeElement2.ContentElement);
        this.OnCellFormatting(new TreeViewSpreadExportCellFormattingEventArgs((TreeViewSpreadExportCell) exportIndentCell, treeNode, currentRowIndex));
        viewSpreadExportRow.Cells.Add((TreeViewSpreadExportCell) exportIndentCell);
      }
      if (this.ExportImages)
      {
        TreeViewSpreadExportImageCell spreadExportImageCell = new TreeViewSpreadExportImageCell();
        if (treeNode.Image != null && !exportChildNodesHidden)
        {
          spreadExportImageCell.Size = new Size(treeNode.Image.Size.Width, height);
          spreadExportImageCell.Image = treeNode.Image;
          spreadExportImageCell.ImageAlignment = ContentAlignment.MiddleLeft;
        }
        else
          spreadExportImageCell.Size = new Size(this.NodeIndent, exportChildNodesHidden ? 0 : -1);
        if (this.ExportVisualSettings)
        {
          if (treeNodeElement2.ImageElement.Image != null)
          {
            spreadExportImageCell.Image = treeNodeElement2.ImageElement.Image;
            spreadExportImageCell.Size = new Size(treeNodeElement2.ImageElement.Image.Size.Width, height);
          }
          spreadExportImageCell.ImageAlignment = treeNodeElement2.ImageElement.ImageAlignment;
          this.GetStylesFromVisualCell((TreeViewSpreadExportCell) spreadExportImageCell, (LightVisualElement) treeNodeElement2.ContentElement);
        }
        this.OnCellFormatting(new TreeViewSpreadExportCellFormattingEventArgs((TreeViewSpreadExportCell) spreadExportImageCell, treeNode, currentRowIndex));
        viewSpreadExportRow.Cells.Add((TreeViewSpreadExportCell) spreadExportImageCell);
      }
      int width = (treeNode.ActualSize.Width == 0 ? this.defaultItemSize.Width : treeNode.ActualSize.Width) - treeNode.Level * this.NodeIndent;
      TreeViewSpreadExportContentCell exportContentCell = new TreeViewSpreadExportContentCell();
      exportContentCell.Text = treeNode.Text;
      exportContentCell.Size = new Size(width, exportChildNodesHidden ? 0 : height);
      exportContentCell.ColSpan += this.depthOfTree - treeNode.Level;
      if (this.ExportVisualSettings)
        this.GetStylesFromVisualCell((TreeViewSpreadExportCell) exportContentCell, (LightVisualElement) treeNodeElement2.ContentElement);
      this.OnCellFormatting(new TreeViewSpreadExportCellFormattingEventArgs((TreeViewSpreadExportCell) exportContentCell, treeNode, currentRowIndex));
      viewSpreadExportRow.Cells.Add((TreeViewSpreadExportCell) exportContentCell);
      if (this.ExportVisualSettings)
      {
        this.elementProvider.CacheElement((IVirtualizedElement<RadTreeNode>) treeNodeElement2);
        treeNodeElement2.Detach();
        virtualizedContainer.Children.Remove((RadElement) treeNodeElement2);
        if (num == 2)
          virtualizedContainer.Children.Remove((RadElement) treeNodeElement1);
      }
      if (!exportChildNodesHidden)
      {
        exportChildNodesHidden = !treeNode.Expanded && this.CollapsedNodeOption == HiddenOption.ExportAsHidden;
        collapsedParentLevel = treeNode.Level;
      }
      return viewSpreadExportRow;
    }

    private TreeViewExportState SaveTreeViewState()
    {
      TreeViewExportState treeViewExportState = new TreeViewExportState() { HorizontalScrollbarValue = this.treeView.HScrollBar.Value, VerticalScrolbarValue = this.treeView.VScrollBar.Value };
      int num = 0;
      Queue<RadTreeNode> radTreeNodeQueue = new Queue<RadTreeNode>();
      foreach (RadTreeNode node in (Collection<RadTreeNode>) this.treeView.TreeViewElement.Root.Nodes)
        radTreeNodeQueue.Enqueue(node);
      while (radTreeNodeQueue.Count > 0)
      {
        RadTreeNode radTreeNode = radTreeNodeQueue.Dequeue();
        if (radTreeNode.Level > num)
          num = radTreeNode.Level;
        if (radTreeNode.Selected)
          treeViewExportState.SelectedNodes.Add(radTreeNode);
        if (radTreeNode.Current)
        {
          treeViewExportState.CurrentNode = radTreeNode;
          radTreeNode.Current = false;
        }
        foreach (RadTreeNode node in (Collection<RadTreeNode>) radTreeNode.Nodes)
          radTreeNodeQueue.Enqueue(node);
      }
      this.depthOfTree = num;
      this.treeView.SelectedNodes.Clear();
      return treeViewExportState;
    }

    private void RestoreTreeViewState(TreeViewExportState state)
    {
      foreach (RadTreeNode selectedNode in state.SelectedNodes)
        selectedNode.Selected = true;
      if (state.CurrentNode != null)
        state.CurrentNode.Current = true;
      this.treeView.HScrollBar.Value = state.HorizontalScrollbarValue;
      this.treeView.VScrollBar.Value = state.VerticalScrolbarValue;
    }

    private void AddRow(TreeViewSpreadExportRow row, int currentRowIndex)
    {
      int index1 = 0;
      for (int index2 = 0; index2 < row.Cells.Count; ++index2)
      {
        TreeViewSpreadExportCell cell1 = row.Cells[index2];
        if (this.columnWidths.Count <= index1)
          this.columnWidths.Add(0);
        if (cell1 is TreeViewSpreadExportIndentCell)
        {
          if (cell1.Size.Width > this.columnWidths[index1])
          {
            this.columnWidths[index1] = cell1.Size.Width;
            this.spreadExportRenderer.SetWorksheetColumnWidth(index1, (double) cell1.Size.Width, false);
          }
          this.CreateCell(cell1, currentRowIndex, index1);
        }
        else if (cell1 is TreeViewSpreadExportImageCell)
        {
          TreeViewSpreadExportImageCell cell2 = cell1 as TreeViewSpreadExportImageCell;
          if (cell2.Image != null)
          {
            this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell2.Image.Size.Height, true);
            if (cell2.Image.Size.Width > this.columnWidths[index1])
            {
              this.columnWidths[index1] = cell2.Image.Size.Width;
              this.spreadExportRenderer.SetWorksheetColumnWidth(index1, (double) cell2.Image.Size.Width, false);
            }
            this.CreateImageOverCell(cell2, currentRowIndex, index1);
          }
          this.CreateCell((TreeViewSpreadExportCell) cell2, currentRowIndex, index1);
        }
        else
        {
          this.spreadExportRenderer.SetWorksheetRowHeight(currentRowIndex, cell1.Size.Height, true);
          int num = this.ExportImages ? this.depthOfTree + 1 : this.depthOfTree;
          if (cell1.Size.Width > this.columnWidths[index1] && num <= index1)
          {
            this.columnWidths[index1] = cell1.Size.Width;
            this.spreadExportRenderer.SetWorksheetColumnWidth(index1, (double) cell1.Size.Width, false);
          }
          this.CreateCell(cell1, currentRowIndex, index1);
        }
        ++index1;
      }
    }

    protected virtual void CreateImageOverCell(
      TreeViewSpreadExportImageCell cell,
      int currentRowIndex,
      int currentColumnIndex)
    {
      if (cell.Image == null)
        return;
      int offsetX = 0;
      int offsetY = 0;
      if (cell.ImageAlignment.ToString().Contains("Middle"))
        offsetY = (cell.Size.Height - cell.Image.Height) / 2;
      else if (cell.ImageAlignment.ToString().Contains("Bottom"))
        offsetY = cell.Size.Height - cell.Image.Height;
      Image image = cell.Image;
      byte[] byteArray = this.ConvertImageToByteArray(image);
      this.spreadExportRenderer.CreateFloatingImage(currentRowIndex, currentColumnIndex, offsetX, offsetY, byteArray, "png", image.Width, image.Height, 0);
    }

    protected virtual void CreateCell(
      TreeViewSpreadExportCell cell,
      int currentRowIndex,
      int currentColumnIndex)
    {
      this.spreadExportRenderer.CreateCellSelection(currentRowIndex, currentColumnIndex, currentRowIndex, currentColumnIndex + cell.ColSpan - 1);
      if (cell.ColSpan > 1)
        this.spreadExportRenderer.MergeCellSelection();
      TreeViewSpreadExportContentCell exportContentCell = cell as TreeViewSpreadExportContentCell;
      if (exportContentCell != null)
      {
        if (!string.IsNullOrEmpty(exportContentCell.FormatString))
          this.spreadExportRenderer.SetCellSelectionFormat(exportContentCell.FormatString);
        if (exportContentCell.Value != null)
          this.spreadExportRenderer.SetCellSelectionValue(new DataTypeConvertor().ConvertToDataType(exportContentCell.Value), exportContentCell.Value);
        else if (!string.IsNullOrEmpty(exportContentCell.Text))
          this.spreadExportRenderer.SetCellSelectionValue(exportContentCell.Text);
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
      TreeViewSpreadExportCell cell,
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
      this.hierarchyRowGroups = new Stack<TreeViewSpreadExport.RowGroup>();
      this.readyToExportRowGroups = new Stack<TreeViewSpreadExport.RowGroup>();
      this.lastRowHierarchyLevel = 0;
    }

    private void ProcessCurrentRowGrouping(int currentRowHierarchyLevel, int currentRowNum)
    {
      if (currentRowHierarchyLevel > this.lastRowHierarchyLevel)
        this.hierarchyRowGroups.Push(new TreeViewSpreadExport.RowGroup(currentRowNum, currentRowHierarchyLevel));
      else if (currentRowHierarchyLevel < this.lastRowHierarchyLevel)
      {
        for (int index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0; currentRowHierarchyLevel < index && this.hierarchyRowGroups.Count > 0; index = this.hierarchyRowGroups.Count > 0 ? this.hierarchyRowGroups.Peek().Level : 0)
        {
          TreeViewSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
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
        TreeViewSpreadExport.RowGroup rowGroup = this.hierarchyRowGroups.Pop();
        rowGroup.EndRow = currentRowNum - 1;
        this.readyToExportRowGroups.Push(rowGroup);
      }
      while (this.readyToExportRowGroups.Count > 0)
      {
        TreeViewSpreadExport.RowGroup rowGroup = this.readyToExportRowGroups.Pop();
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
