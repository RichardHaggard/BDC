// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewPdfExport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  public class GridViewPdfExport
  {
    private HiddenOption hiddenRowOption = HiddenOption.ExportAsHidden;
    private HiddenOption hiddenColumnOption = HiddenOption.ExportAsHidden;
    private string fileExtension = "pdf";
    private SizeF pageSize = GridExportUtils.ConvertMmToDipSize(new SizeF(210f, 297f));
    private Padding pageMargins = GridExportUtils.ConvertMmToDipPadding(new Padding(20, 20, 20, 20));
    private double textOffset = 2.0;
    private bool fitToPageWidth = true;
    private bool exportGroupedColumns = true;
    private double scale = 1.0;
    private double fitCoefficient = 1.0;
    private double rowIndent = 15.0;
    private bool exportHeaderRowOnEachPage = true;
    private PdfExportSettings settings = new PdfExportSettings();
    private DateTime exportStartDate = DateTime.MinValue;
    private int totalPagesCount = 3;
    private PageHeaderFooter header = new PageHeaderFooter(30.0, Control.DefaultFont, string.Empty, string.Empty, string.Empty, false);
    private PageHeaderFooter footer = new PageHeaderFooter(30.0, Control.DefaultFont, string.Empty, string.Empty, string.Empty, false);
    public const string LogoString = "[Logo]";
    public const string PageNumberString = "[Page #]";
    public const string TotalPagesString = "[Total Pages]";
    [Obsolete("Obsolete. Use DateExportedString instead.")]
    public const string DatePrintedString = "[Date Exported]";
    public const string DateExportedString = "[Date Exported]";
    [Obsolete("Obsolete. Use TimeExportedString instead.")]
    public const string TimePrintedString = "[Time Exported]";
    public const string TimeExportedString = "[Time Exported]";
    [Obsolete("Obsolete. Use UserNameExportedString instead.")]
    public const string UserNamePrintedString = "[Time Exported]";
    public const string UserNameExportedString = "[User Name]";
    private RadGridView radGridView;
    private IPdfExportRenderer pdfExportRenderer;
    private IPdfEditor editor;
    private PagingExportOption pagingExportOption;
    private SummariesOption summariesExportOption;
    private ChildViewExportMode childViewExportMode;
    private bool exportVisualSettings;
    private bool exportHierarchy;
    private bool applicationDoEvents;
    private double currentRowIndent;
    private double cellSelfReferenceIndent;
    private bool cancellationPending;
    private bool isDrawingHeaderRow;
    private GridViewTableHeaderRowInfo headerRow;
    private Stack<GridViewHierarchyRowInfo> hierarchyRowsStack;
    private int pageNumber;
    private bool showHeaderAndFooter;
    private Image logo;
    private ContentAlignment logoAlignment;
    private LogoLayout logoLayout;
    private RowElementProvider rowProvider;
    private CellElementProvider cellProvider;
    private BackgroundWorker worker;

    public GridViewPdfExport(RadGridView radGridView)
    {
      this.radGridView = radGridView;
      this.ExportViewDefinition = true;
      this.LogoAlignment = ContentAlignment.MiddleCenter;
      this.LogoLayout = LogoLayout.Stretch;
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

    [DefaultValue(false)]
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

    public RadGridView RadGridViewToExport
    {
      get
      {
        return this.radGridView;
      }
      set
      {
        this.radGridView = value;
      }
    }

    public string FileExtension
    {
      get
      {
        return this.fileExtension;
      }
      set
      {
        this.fileExtension = value;
      }
    }

    public SizeF PageSize
    {
      get
      {
        return GridExportUtils.ConvertDipToMmSize(this.pageSize);
      }
      set
      {
        this.pageSize = GridExportUtils.ConvertMmToDipSize(value);
      }
    }

    public Padding PageMargins
    {
      get
      {
        return GridExportUtils.ConvertDipToMmPadding(this.pageMargins);
      }
      set
      {
        this.pageMargins = GridExportUtils.ConvertMmToDipPadding(value);
      }
    }

    public bool FitToPageWidth
    {
      get
      {
        return this.fitToPageWidth;
      }
      set
      {
        this.fitToPageWidth = value;
      }
    }

    public double Scale
    {
      get
      {
        return this.scale;
      }
      set
      {
        this.scale = value;
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

    public bool ExportHeaderRowOnEachPage
    {
      get
      {
        return this.exportHeaderRowOnEachPage;
      }
      set
      {
        this.exportHeaderRowOnEachPage = value;
      }
    }

    public double RowIndent
    {
      get
      {
        return this.rowIndent;
      }
      set
      {
        this.rowIndent = value;
      }
    }

    public bool ShowHeaderAndFooter
    {
      get
      {
        return this.showHeaderAndFooter;
      }
      set
      {
        this.showHeaderAndFooter = value;
      }
    }

    public double HeaderHeight
    {
      get
      {
        return this.header.Height;
      }
      set
      {
        this.header.Height = value;
      }
    }

    public double FooterHeight
    {
      get
      {
        return this.footer.Height;
      }
      set
      {
        this.footer.Height = value;
      }
    }

    public Font HeaderFont
    {
      get
      {
        return this.header.Font;
      }
      set
      {
        this.header.Font = value;
      }
    }

    public Font FooterFont
    {
      get
      {
        return this.footer.Font;
      }
      set
      {
        this.footer.Font = value;
      }
    }

    public string LeftHeader
    {
      get
      {
        return this.header.LeftText;
      }
      set
      {
        this.header.LeftText = value;
      }
    }

    public string MiddleHeader
    {
      get
      {
        return this.header.CenterText;
      }
      set
      {
        this.header.CenterText = value;
      }
    }

    public string RightHeader
    {
      get
      {
        return this.header.RightText;
      }
      set
      {
        this.header.RightText = value;
      }
    }

    public string LeftFooter
    {
      get
      {
        return this.footer.LeftText;
      }
      set
      {
        this.footer.LeftText = value;
      }
    }

    public string MiddleFooter
    {
      get
      {
        return this.footer.CenterText;
      }
      set
      {
        this.footer.CenterText = value;
      }
    }

    public string RightFooter
    {
      get
      {
        return this.footer.RightText;
      }
      set
      {
        this.footer.RightText = value;
      }
    }

    public bool ReverseHeaderOnEvenPages
    {
      get
      {
        return this.header.ReverseOnEvenPages;
      }
      set
      {
        this.header.ReverseOnEvenPages = value;
      }
    }

    public bool ReverseFooterOnEvenPages
    {
      get
      {
        return this.footer.ReverseOnEvenPages;
      }
      set
      {
        this.footer.ReverseOnEvenPages = value;
      }
    }

    public Image Logo
    {
      get
      {
        return this.logo;
      }
      set
      {
        this.logo = value;
      }
    }

    public ContentAlignment LogoAlignment
    {
      get
      {
        return this.logoAlignment;
      }
      set
      {
        this.logoAlignment = value;
      }
    }

    public LogoLayout LogoLayout
    {
      get
      {
        return this.logoLayout;
      }
      set
      {
        this.logoLayout = value;
      }
    }

    public PdfExportSettings ExportSettings
    {
      get
      {
        return this.settings;
      }
      set
      {
        this.settings = value;
      }
    }

    [DefaultValue(true)]
    public bool ExportViewDefinition { get; set; }

    public event ChildViewExportingEventHandler ChildViewExporting;

    protected virtual void OnChildViewExporting(ChildViewExportingEventArgs e)
    {
      if (this.ChildViewExporting == null)
        return;
      this.ChildViewExporting((object) this, e);
    }

    public event PdfExportCellFormattingEventHandler CellFormatting;

    protected virtual void OnCellFormatting(PdfExportCellFormattingEventArgs e)
    {
      if (this.CellFormatting == null)
        return;
      this.CellFormatting((object) this, e);
    }

    public event ExportCellPaintEventHandler CellPaint;

    protected virtual void OnCellPaint(ExportCellPaintEventArgs e)
    {
      if (this.CellPaint == null)
        return;
      this.CellPaint((object) this, e);
    }

    public event EventHandler PdfExported;

    protected virtual void OnPdfExported(EventArgs e)
    {
      if (this.PdfExported == null)
        return;
      this.PdfExported((object) this, e);
    }

    public event ExportEventHandler HeaderExported;

    protected virtual void OnHeaderExported(ExportEventArgs e)
    {
      if (this.HeaderExported == null)
        return;
      this.HeaderExported((object) this, e);
    }

    public event ExportEventHandler FooterExported;

    protected virtual void OnFooterExported(ExportEventArgs e)
    {
      if (this.FooterExported == null)
        return;
      this.FooterExported((object) this, e);
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

    private void RunExportCall(Stream exportStream, IPdfExportRenderer pdfExportRenderer)
    {
      this.pdfExportRenderer = pdfExportRenderer;
      GridExportState state = this.SaveGridState();
      this.exportStartDate = DateTime.Now;
      if (!this.ShowHeaderAndFooter)
      {
        this.header.Height = 0.0;
        this.footer.Height = 0.0;
      }
      try
      {
        if (this.ExportToStream(pdfExportRenderer, exportStream))
          return;
        this.OnPdfExported(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.radGridView, "Export");
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreGridState(state);
        this.cancellationPending = false;
      }
    }

    private void RunExportCall(string fileName, IPdfExportRenderer pdfExportRenderer)
    {
      this.pdfExportRenderer = pdfExportRenderer;
      GridExportState state = this.SaveGridState();
      this.exportStartDate = DateTime.Now;
      if (!this.ShowHeaderAndFooter)
      {
        this.header.Height = 0.0;
        this.footer.Height = 0.0;
      }
      try
      {
        fileName = Path.GetDirectoryName(fileName) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName) + "." + this.fileExtension;
        FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
        using (fileStream)
        {
          if (this.ExportToStream(pdfExportRenderer, (Stream) fileStream))
            return;
        }
        this.OnPdfExported(new EventArgs());
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.radGridView, "Export", (object) fileName);
      }
      finally
      {
        this.applicationDoEvents = false;
        this.RestoreGridState(state);
        this.cancellationPending = false;
      }
    }

    private bool ExportToStream(IPdfExportRenderer pdfExportRenderer, Stream stream)
    {
      int currentRowNum = 0;
      this.rowProvider = (RowElementProvider) this.radGridView.TableElement.RowElementProvider;
      this.cellProvider = (CellElementProvider) this.radGridView.TableElement.CellElementProvider;
      pdfExportRenderer.CreateDocumentPageMatrixEditor(this.pageSize, ref this.editor);
      this.pageNumber = 1;
      this.editor.CreateMatrixPosition();
      this.editor.TranslatePosition((double) this.pageMargins.Left, (double) this.pageMargins.Top + this.header.Height);
      ExportGridTraverser traverser = new ExportGridTraverser(this.radGridView.MasterView);
      traverser.ProcessHierarchy = this.exportHierarchy;
      if (this.ShowHeaderAndFooter && (this.LeftHeader.Contains("[Total Pages]") || this.MiddleHeader.Contains("[Total Pages]") || (this.RightHeader.Contains("[Total Pages]") || this.LeftFooter.Contains("[Total Pages]")) || (this.MiddleFooter.Contains("[Total Pages]") || this.RightFooter.Contains("[Total Pages]"))))
        this.totalPagesCount = this.GetNumberOfPages(traverser);
      this.DrawHeader();
      this.DrawFooter();
      TableViewRowLayoutBase rowLayout = (TableViewRowLayoutBase) null;
      if (this.radGridView.ViewDefinition is ColumnGroupsViewDefinition)
        rowLayout = (TableViewRowLayoutBase) this.InitializeColumnGroupRowLayout();
      else if (this.radGridView.ViewDefinition is HtmlViewDefinition)
        rowLayout = (TableViewRowLayoutBase) this.InitializeHtmlViewRowLayout();
      if (this.RadGridViewToExport.GroupDescriptors.Count > 0)
        this.currentRowIndent = (double) (this.RadGridViewToExport.GroupDescriptors.Count - 1) * this.rowIndent;
      if (this.childViewExportMode == ChildViewExportMode.ExportAllViews)
        this.hierarchyRowsStack = new Stack<GridViewHierarchyRowInfo>();
      while (traverser.MoveNext())
      {
        if (this.cancellationPending)
          return true;
        currentRowNum = this.ProcessRow(traverser, rowLayout, currentRowNum);
      }
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0)
      {
        while (this.hierarchyRowsStack.Count > 0 && this.MoveToNextActiveViewOfLastHierarchyRow(traverser))
        {
          while (traverser.MoveNext())
          {
            if (this.cancellationPending)
              return true;
            currentRowNum = this.ProcessRow(traverser, rowLayout, currentRowNum);
          }
        }
      }
      this.CallCurrentRowPageExported();
      this.pdfExportRenderer.AddMatrixPagesLeftRight();
      if (this.settings == null)
        this.pdfExportRenderer.ExportDocument(stream, string.Empty, string.Empty, string.Empty);
      else
        this.pdfExportRenderer.ExportDocument(stream, this.settings.Author, this.settings.Title, this.settings.Description);
      return false;
    }

    private int ProcessRow(
      ExportGridTraverser traverser,
      TableViewRowLayoutBase rowLayout,
      int currentRowNum)
    {
      if (this.applicationDoEvents)
        Application.DoEvents();
      GridViewRowInfo gridViewRowInfo = traverser.Current;
      GridViewHierarchyRowInfo row = gridViewRowInfo as GridViewHierarchyRowInfo;
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0 && traverser.Current is GridViewGroupRowInfo)
      {
        GridViewHierarchyRowInfo hierarchyRowInfo = this.hierarchyRowsStack.Peek();
        if (hierarchyRowInfo.Views.Count > 1 && hierarchyRowInfo.Views.IndexOf(hierarchyRowInfo.ActiveView) != hierarchyRowInfo.Views.Count - 1)
          return currentRowNum;
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
            gridViewRowInfo = this.TraverseAllChildViews(traverser);
            break;
        }
      }
      if (this.ShouldExportRow(gridViewRowInfo))
      {
        if (this.ExportViewDefinition && (this.radGridView.ViewDefinition is ColumnGroupsViewDefinition || this.radGridView.ViewDefinition is HtmlViewDefinition))
        {
          if (gridViewRowInfo is GridViewGroupRowInfo)
            this.DrawRowWideCell(gridViewRowInfo, rowLayout);
          else
            this.DrawViewDefinitionRow(gridViewRowInfo, rowLayout, currentRowNum);
        }
        else
          this.DrawRow(gridViewRowInfo, currentRowNum);
        ++currentRowNum;
      }
      return currentRowNum;
    }

    private GridViewRowInfo TraverseAllChildViews(ExportGridTraverser traverser)
    {
      GridViewRowInfo current1 = traverser.Current;
      GridViewHierarchyRowInfo hierarchyRowInfo1 = current1 as GridViewHierarchyRowInfo;
      if (this.hierarchyRowsStack.Count == 0)
      {
        hierarchyRowInfo1.ActiveView = hierarchyRowInfo1.Views[0];
        this.hierarchyRowsStack.Push(hierarchyRowInfo1);
      }
      else
      {
        GridViewHierarchyRowInfo hierarchyRowInfo2 = this.hierarchyRowsStack.Peek();
        if (hierarchyRowInfo2.HierarchyLevel == hierarchyRowInfo1.HierarchyLevel)
        {
          if (hierarchyRowInfo2.Views.IndexOf(hierarchyRowInfo2.ActiveView) == hierarchyRowInfo2.Views.Count - 1)
          {
            this.hierarchyRowsStack.Pop();
            hierarchyRowInfo1.ActiveView = hierarchyRowInfo1.Views[0];
            this.hierarchyRowsStack.Push(hierarchyRowInfo1);
          }
          else if (traverser.MoveBackward((GridViewRowInfo) hierarchyRowInfo2))
          {
            GridViewHierarchyRowInfo current2 = traverser.Current as GridViewHierarchyRowInfo;
            current2.ActiveView = current2.Views[current2.Views.IndexOf(current2.ActiveView) + 1];
            traverser.MoveNext();
            current1 = traverser.Current;
          }
        }
        else if (hierarchyRowInfo2.HierarchyLevel < hierarchyRowInfo1.HierarchyLevel)
        {
          hierarchyRowInfo1.ActiveView = hierarchyRowInfo1.Views[0];
          this.hierarchyRowsStack.Push(hierarchyRowInfo1);
        }
      }
      return current1;
    }

    private ColumnGroupRowLayout InitializeColumnGroupRowLayout()
    {
      ColumnGroupRowLayout columnGroupRowLayout = new ColumnGroupRowLayout(this.radGridView.ViewDefinition as ColumnGroupsViewDefinition);
      columnGroupRowLayout.Initialize(this.radGridView.TableElement);
      this.SetupRowLayout((TableViewRowLayoutBase) columnGroupRowLayout);
      return columnGroupRowLayout;
    }

    private HtmlViewRowLayout InitializeHtmlViewRowLayout()
    {
      HtmlViewRowLayout htmlViewRowLayout = new HtmlViewRowLayout(this.radGridView.ViewDefinition as HtmlViewDefinition);
      htmlViewRowLayout.Initialize(this.radGridView.TableElement);
      this.SetupRowLayout((TableViewRowLayoutBase) htmlViewRowLayout);
      return htmlViewRowLayout;
    }

    private void SetupRowLayout(TableViewRowLayoutBase rowLayout)
    {
      rowLayout.IgnoreColumnVisibility = this.HiddenColumnOption == HiddenOption.ExportAlways;
      rowLayout.Context = GridLayoutContext.Printer;
      if (this.FitToPageWidth)
      {
        this.radGridView.BeginUpdate();
        GridViewAutoSizeColumnsMode autoSizeColumnsMode = rowLayout.ViewTemplate.AutoSizeColumnsMode;
        rowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        rowLayout.MeasureRow(new SizeF(this.pageSize.Width - (float) this.pageMargins.Horizontal, this.pageSize.Height - (float) this.pageMargins.Vertical));
        rowLayout.ViewTemplate.AutoSizeColumnsMode = autoSizeColumnsMode;
        this.radGridView.EndUpdate(false);
      }
      else
      {
        this.radGridView.BeginUpdate();
        GridViewAutoSizeColumnsMode autoSizeColumnsMode = rowLayout.ViewTemplate.AutoSizeColumnsMode;
        rowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
        rowLayout.MeasureRow(new SizeF((float) this.radGridView.Width, (float) this.radGridView.Height));
        rowLayout.ViewTemplate.AutoSizeColumnsMode = autoSizeColumnsMode;
        this.radGridView.EndUpdate(false);
      }
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
      GridViewPdfExportDataSnapshot gridData1 = e.Argument as GridViewPdfExportDataSnapshot;
      this.exportStartDate = DateTime.Now;
      if (gridData1 != null)
      {
        string path = Path.GetDirectoryName(gridData1.FilePath) + (object) Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(gridData1.FilePath) + "." + this.fileExtension;
        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 32768, false);
        using (fileStream)
        {
          if (this.ExportToStreamAsync(gridData1, e, (Stream) fileStream))
            return;
          this.GetWorker().ReportProgress(100);
          this.OnPdfExported(new EventArgs());
          ControlTraceMonitor.TrackAtomicFeature((RadControl) this.radGridView, "Export", (object) path);
        }
      }
      else
      {
        List<object> objectList = e.Argument as List<object>;
        GridViewPdfExportDataSnapshot gridData2 = objectList[0] as GridViewPdfExportDataSnapshot;
        Stream stream = objectList[1] as Stream;
        if (this.ExportToStreamAsync(gridData2, e, stream))
          return;
        this.GetWorker().ReportProgress(100);
        this.OnPdfExported(new EventArgs());
        e.Result = (object) stream;
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this.radGridView, "Export");
      }
    }

    private bool ExportToStreamAsync(
      GridViewPdfExportDataSnapshot gridData,
      DoWorkEventArgs e,
      Stream stream)
    {
      int percentProgress = 0;
      int num1 = 0;
      this.pdfExportRenderer.CreateDocumentPageMatrixEditor(this.pageSize, ref this.editor);
      this.pageNumber = 1;
      this.editor.CreateMatrixPosition();
      this.editor.TranslatePosition((double) this.pageMargins.Left, (double) this.pageMargins.Top + this.header.Height);
      this.DrawHeader();
      this.DrawFooter();
      if (this.RadGridViewToExport.GroupDescriptors.Count > 0)
        this.currentRowIndent = (double) (this.RadGridViewToExport.GroupDescriptors.Count - 1) * this.rowIndent;
      for (int index1 = 0; index1 < gridData.ExportRowInfos.Count; ++index1)
      {
        if (this.GetWorker().CancellationPending)
        {
          e.Cancel = true;
          return true;
        }
        GridViewPdfExportRowInfo exportRowInfo = gridData.ExportRowInfos[index1];
        double num2 = exportRowInfo.Height * this.Scale;
        this.pdfExportRenderer.CurrentMatrixColumn = 0;
        this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
        double num3 = (double) this.pageSize.Width - (double) this.pageMargins.Horizontal;
        int visibleColumnCount = this.GetVisibleColumnCount();
        if (this.editor.OffsetY + num2 > (double) this.pageSize.Height - (double) this.pageMargins.Bottom - this.footer.Height)
        {
          this.CallCurrentRowPageExported();
          this.editor = this.pdfExportRenderer.GetDownPageEditor();
          this.editor.CreateMatrixPosition();
          this.editor.TranslatePosition((double) this.pageMargins.Left, (double) this.pageMargins.Top + this.HeaderHeight);
          this.pageNumber = this.pdfExportRenderer.GetCurrentPageNumber();
          this.DrawHeader();
          this.DrawFooter();
          if (this.ExportHeaderRowOnEachPage && this.headerRow != null)
          {
            this.isDrawingHeaderRow = true;
            int currentMatrixColumn = this.pdfExportRenderer.CurrentMatrixColumn;
            this.DrawRow((GridViewRowInfo) this.headerRow, 0);
            this.pdfExportRenderer.CurrentMatrixColumn = currentMatrixColumn;
            this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
            this.isDrawingHeaderRow = false;
          }
        }
        if ((object) exportRowInfo.Type == (object) typeof (GridViewGroupRowInfo))
        {
          if (this.FitToPageWidth)
            this.fitCoefficient = num3 / (exportRowInfo.Width * this.Scale + 1.0);
          List<double> doubleList1 = new List<double>();
          if (!this.FitToPageWidth && exportRowInfo.Width > num3)
          {
            int index2 = 0;
            doubleList1.Add(0.0);
            foreach (GridPdfAsyncExportCellInfo cell in exportRowInfo.Cells)
            {
              double num4 = cell.ColumnWidth * this.Scale;
              if (doubleList1[index2] + num4 > num3 && doubleList1[index2] > 0.0)
              {
                doubleList1.Add(0.0);
                ++index2;
              }
              List<double> doubleList2;
              int index3;
              (doubleList2 = doubleList1)[index3 = index2] = doubleList2[index3] + num4;
            }
          }
          else
            doubleList1.Add(exportRowInfo.Width * this.fitCoefficient * this.Scale);
          this.editor.CreateMatrixPosition(this.editor.OffsetX, this.editor.OffsetY);
          this.editor.TranslatePosition(exportRowInfo.Indent, 0.0);
          this.OnCellFormatting(new PdfExportCellFormattingEventArgs((GridPdfExportCellElement) exportRowInfo.Cells[0], (GridViewRowInfo) null, (GridViewColumn) null));
          for (int index2 = 0; index2 < doubleList1.Count; ++index2)
          {
            double num4 = doubleList1[index2] - exportRowInfo.Indent;
            string[] text = this.ApplyLineBreaksToText((object) exportRowInfo.Cells[0].Text);
            this.DrawCell((GridPdfExportCellElement) exportRowInfo.Cells[0], text, num2, num4, num4, false);
            if (index2 < doubleList1.Count - 1)
            {
              bool flag = this.pdfExportRenderer.IsMatrixCurrentPageLastOnRow();
              double offsetY = this.editor.OffsetY;
              this.editor.TranslatePosition(-(this.editor.OffsetX - (double) this.pageMargins.Left), num2);
              this.editor = this.pdfExportRenderer.GetRightPageEditor();
              this.editor.CreateMatrixPosition();
              this.editor.TranslatePosition((double) this.pageMargins.Left + exportRowInfo.Indent, offsetY);
              this.pageNumber = this.pdfExportRenderer.GetCurrentPageNumber();
              if (flag)
              {
                this.DrawHeader();
                this.DrawFooter();
              }
            }
          }
        }
        else
        {
          if (this.FitToPageWidth)
            this.fitCoefficient = num3 / (exportRowInfo.Width * this.Scale + 1.0);
          if ((object) exportRowInfo.Type != (object) typeof (GridViewTableHeaderRowInfo))
          {
            this.editor.CreateMatrixPosition(this.editor.OffsetX, this.editor.OffsetY);
            this.editor.TranslatePosition(this.currentRowIndent * this.fitCoefficient * this.Scale, 0.0);
          }
          bool flag = true;
          for (int index2 = 0; index2 < exportRowInfo.Cells.Count; ++index2)
          {
            int index3 = index2;
            if (this.radGridView.RightToLeft == RightToLeft.Yes)
              index3 = exportRowInfo.Cells.Count - index2 - 1;
            double num4 = (exportRowInfo.Cells[index3].ColumnWidth * this.fitCoefficient - this.currentRowIndent * this.fitCoefficient / (double) visibleColumnCount) * this.Scale;
            if (flag && (object) exportRowInfo.Type == (object) typeof (GridViewTableHeaderRowInfo))
            {
              num4 += this.currentRowIndent * this.fitCoefficient * this.Scale;
              flag = false;
            }
            double num5 = num4 < 0.0 ? 0.0 : num4;
            this.OnCellFormatting(new PdfExportCellFormattingEventArgs((GridPdfExportCellElement) exportRowInfo.Cells[index3], (GridViewRowInfo) null, (GridViewColumn) null));
            string[] text = this.ApplyLineBreaksToText((object) exportRowInfo.Cells[index3].Text);
            this.DrawCell((GridPdfExportCellElement) exportRowInfo.Cells[index3], text, num2, num5, num5, false);
            this.editor.CreateMatrixPosition(this.editor.OffsetX, this.editor.OffsetY);
            this.editor.TranslatePosition(num5 % num3, 0.0);
          }
        }
        this.editor.TranslatePosition(-(this.editor.OffsetX - (double) this.pageMargins.Left), num2);
        int num6 = num1 * 100 / gridData.ExportRowInfos.Count;
        if (percentProgress != num6)
        {
          percentProgress = num6;
          this.GetWorker().ReportProgress(percentProgress);
        }
        ++num1;
      }
      this.CallCurrentRowPageExported();
      this.pdfExportRenderer.AddMatrixPagesLeftRight();
      if (this.settings == null)
        this.pdfExportRenderer.ExportDocument(stream, string.Empty, string.Empty, string.Empty);
      else
        this.pdfExportRenderer.ExportDocument(stream, this.settings.Author, this.settings.Title, this.settings.Description);
      return false;
    }

    private List<GridViewPdfExportRowInfo> GetGridDataSnapshot()
    {
      List<GridViewPdfExportRowInfo> exportRowInfos = new List<GridViewPdfExportRowInfo>();
      ExportGridTraverser traverser = new ExportGridTraverser(this.radGridView.MasterView);
      traverser.ProcessHierarchy = this.exportHierarchy;
      if (this.ShowHeaderAndFooter && (this.LeftHeader.Contains("[Total Pages]") || this.MiddleHeader.Contains("[Total Pages]") || (this.RightHeader.Contains("[Total Pages]") || this.LeftFooter.Contains("[Total Pages]")) || (this.MiddleFooter.Contains("[Total Pages]") || this.RightFooter.Contains("[Total Pages]"))))
        this.totalPagesCount = this.GetNumberOfPages(traverser);
      if (this.RadGridViewToExport.GroupDescriptors.Count > 0)
        this.currentRowIndent = (double) (this.RadGridViewToExport.GroupDescriptors.Count - 1) * this.rowIndent;
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
        this.hierarchyRowsStack = new Stack<GridViewHierarchyRowInfo>();
      this.CreateGridSnapshotRows(traverser, exportRowInfos);
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0)
      {
        while (this.hierarchyRowsStack.Count > 0 && this.MoveToNextActiveViewOfLastHierarchyRow(traverser))
          this.CreateGridSnapshotRows(traverser, exportRowInfos);
      }
      return exportRowInfos;
    }

    private void CreateGridSnapshotRows(
      ExportGridTraverser traverser,
      List<GridViewPdfExportRowInfo> exportRowInfos)
    {
      while (traverser.MoveNext())
      {
        GridViewRowInfo gridViewRowInfo = traverser.Current;
        GridViewHierarchyRowInfo row = gridViewRowInfo as GridViewHierarchyRowInfo;
        if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0 && traverser.Current is GridViewGroupRowInfo)
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
              gridViewRowInfo = this.TraverseAllChildViews(traverser);
              break;
          }
        }
        if (this.ShouldExportRow(gridViewRowInfo))
        {
          GridViewPdfExportRowInfo pdfExportRowInfo = new GridViewPdfExportRowInfo(gridViewRowInfo.GetType());
          double num1 = 0.0;
          pdfExportRowInfo.Height = GridExportUtils.GetRowHeight(this.RadGridViewToExport, this.rowProvider, this.cellProvider, gridViewRowInfo, false);
          int num2 = 0;
          if (this.CellFormatting != null || this.CellPaint != null)
            num2 = gridViewRowInfo.Index;
          System.Type type = gridViewRowInfo.GetType();
          if (gridViewRowInfo is GridViewGroupRowInfo)
          {
            GridViewGroupRowInfo viewGroupRowInfo = gridViewRowInfo as GridViewGroupRowInfo;
            GridPdfAsyncExportCellInfo asyncExportCellInfo = new GridPdfAsyncExportCellInfo();
            asyncExportCellInfo.RowIndex = num2;
            asyncExportCellInfo.RowType = type;
            asyncExportCellInfo.Text = viewGroupRowInfo.HeaderText;
            asyncExportCellInfo.TextAlignment = ContentAlignment.MiddleLeft;
            asyncExportCellInfo.BackColor = Color.LightGray;
            asyncExportCellInfo.NumberOfColors = 1;
            pdfExportRowInfo.Cells.Add(asyncExportCellInfo);
            foreach (GridViewCellInfo cell in gridViewRowInfo.Cells)
            {
              if (this.ShouldExportColumn(cell.ColumnInfo))
              {
                int width = cell.ColumnInfo.Width;
                if (pdfExportRowInfo.Cells.Count == 1)
                  pdfExportRowInfo.Cells[0].ColumnWidth = (double) width;
                else
                  pdfExportRowInfo.Cells.Add(new GridPdfAsyncExportCellInfo()
                  {
                    ColumnWidth = (double) width
                  });
                num1 += (double) width;
              }
            }
            pdfExportRowInfo.Width = num1;
            pdfExportRowInfo.Indent = (double) gridViewRowInfo.Group.Level * this.rowIndent * this.fitCoefficient;
          }
          else
          {
            for (int index = 0; index < gridViewRowInfo.Cells.Count; ++index)
            {
              GridPdfAsyncExportCellInfo asyncExportCellInfo = new GridPdfAsyncExportCellInfo();
              asyncExportCellInfo.RowIndex = num2;
              asyncExportCellInfo.ColumnIndex = index;
              asyncExportCellInfo.TextAlignment = ContentAlignment.MiddleCenter;
              asyncExportCellInfo.RowType = type;
              asyncExportCellInfo.ColumnType = gridViewRowInfo.Cells[index].ColumnInfo.GetType();
              if (gridViewRowInfo is GridViewTableHeaderRowInfo || gridViewRowInfo is GridViewHierarchyRowInfo)
              {
                asyncExportCellInfo.BackColor = Color.LightGray;
                asyncExportCellInfo.NumberOfColors = 1;
              }
              GridViewImageColumn columnInfo1 = gridViewRowInfo.Cells[index].ColumnInfo as GridViewImageColumn;
              if (columnInfo1 != null)
              {
                object result = (object) null;
                RadDataConverter.Instance.TryFormat(gridViewRowInfo.Cells[index].Value, typeof (Image), (IDataConversionInfoProvider) columnInfo1, out result);
                asyncExportCellInfo.Image = result as Image;
                asyncExportCellInfo.ImageAlignment = columnInfo1.ImageAlignment;
                asyncExportCellInfo.ImageLayout = columnInfo1.ImageLayout;
              }
              object objectCellValue = this.GetObjectCellValue(gridViewRowInfo.Cells[index]);
              GridViewDataColumn columnInfo2 = gridViewRowInfo.Cells[index].ColumnInfo as GridViewDataColumn;
              if (columnInfo2 != null && !string.IsNullOrEmpty(columnInfo2.FormatString))
              {
                GridViewComboBoxColumn viewComboBoxColumn = columnInfo2 as GridViewComboBoxColumn;
                if (viewComboBoxColumn != null)
                {
                  columnInfo2.DataType = viewComboBoxColumn.DisplayMemberDataType;
                  columnInfo2.DataTypeConverter = TypeDescriptor.GetConverter(viewComboBoxColumn.DisplayMemberDataType);
                }
                asyncExportCellInfo.Text = RadDataConverter.Instance.Format(objectCellValue, typeof (string), (IDataConversionInfoProvider) columnInfo2) as string;
              }
              else
                asyncExportCellInfo.Text = objectCellValue == null ? string.Empty : objectCellValue.ToString();
              if (this.ExportHeaderRowOnEachPage && gridViewRowInfo is GridViewTableHeaderRowInfo && this.headerRow == null)
              {
                this.headerRow = new GridViewTableHeaderRowInfo(gridViewRowInfo.ViewInfo);
                this.headerRow.Height = gridViewRowInfo.Height;
              }
              if (this.ShouldExportColumn(gridViewRowInfo.Cells[index].ColumnInfo))
              {
                num1 += (double) gridViewRowInfo.Cells[index].ColumnInfo.Width;
                asyncExportCellInfo.ColumnWidth = (double) gridViewRowInfo.Cells[index].ColumnInfo.Width;
              }
              pdfExportRowInfo.Cells.Add(asyncExportCellInfo);
            }
            pdfExportRowInfo.Width = num1;
          }
          exportRowInfos.Add(pdfExportRowInfo);
        }
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
      this.RadGridViewToExport.MasterTemplate.ShowGroupedColumns = this.ExportGroupedColumns;
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

    protected virtual Size GetRowSize(GridViewRowInfo row, TableViewRowLayoutBase rowLayout)
    {
      int width1 = 0;
      int height = rowLayout.GetRowHeight(row) + this.radGridView.TableElement.RowSpacing;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
      {
        if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
        {
          CellArrangeInfo cellArrangeInfo = (CellArrangeInfo) null;
          if (rowLayout is ColumnGroupRowLayout)
            cellArrangeInfo = (CellArrangeInfo) (rowLayout as ColumnGroupRowLayout).GetColumnData(renderColumn);
          else if (rowLayout is HtmlViewRowLayout)
            cellArrangeInfo = (CellArrangeInfo) (rowLayout as HtmlViewRowLayout).GetArrangeInfo(renderColumn);
          if (cellArrangeInfo != null)
          {
            int width2 = (int) cellArrangeInfo.Bounds.Width;
            if ((double) width1 < (double) cellArrangeInfo.Bounds.X + (double) width2)
              width1 = (int) cellArrangeInfo.Bounds.X + width2;
          }
        }
      }
      return new Size(width1, height);
    }

    private bool ShouldExportColumn(GridViewColumn gridColumn)
    {
      return (!gridColumn.IsGrouped || gridColumn.OwnerTemplate.ShowGroupedColumns) && (gridColumn.IsVisible || this.hiddenColumnOption == HiddenOption.ExportAlways);
    }

    private bool ShouldExportRow(GridViewRowInfo gridRowInfo)
    {
      if (!(gridRowInfo is GridViewTableHeaderRowInfo) && !(gridRowInfo is GridViewDataRowInfo) && (!(gridRowInfo is GridViewHierarchyRowInfo) && !(gridRowInfo is GridViewGroupRowInfo)) && !(gridRowInfo is GridViewSummaryRowInfo))
        return false;
      GridViewSummaryRowInfo summaryRow = gridRowInfo as GridViewSummaryRowInfo;
      return (summaryRow == null || this.ShouldExportSummaryRow(summaryRow)) && (this.IsVisibleRow(gridRowInfo) || this.hiddenRowOption == HiddenOption.ExportAlways);
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

    private int GetVisibleColumnCount()
    {
      int num = 0;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.radGridView.Columns)
      {
        if (this.ShouldExportColumn(column))
          ++num;
      }
      return num;
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
        obj = !(gridCell.ColumnInfo is GridViewComboBoxColumn) || ((GridViewComboBoxColumn) gridCell.ColumnInfo).DisplayMember == null ? gridCell.Value : ((GridViewComboBoxColumn) gridCell.ColumnInfo).GetLookupValue(gridCell.Value);
      }
      else
      {
        object nullValue = gridCell.ColumnInfo.OwnerTemplate.Columns[gridCell.ColumnInfo.Index].NullValue;
        if (nullValue != null)
          obj = nullValue;
      }
      return obj;
    }

    private GridVisualElement FindPainter(GridVisualElement current)
    {
      if (current == null)
        return (GridVisualElement) null;
      if (current.DrawFill)
        return current;
      return this.FindPainter(current.Parent as GridVisualElement);
    }

    private string[] ApplyLineBreaksToText(object value)
    {
      if (value == null)
        value = (object) string.Empty;
      return value.ToString().Split(new string[2]{ "\r\n", "\n" }, StringSplitOptions.None);
    }

    private void CallCurrentRowPageExported()
    {
      this.pdfExportRenderer.CallCurrentRowPageExported();
    }

    private GridPdfExportCellElement CreatePdfExportCellElement(
      GridCellElement cell)
    {
      GridPdfExportCellElement exportCellElement = new GridPdfExportCellElement() { RowType = cell.GetType(), Image = cell.Image, ImageAlignment = cell.ImageAlignment, ImageLayout = cell.ImageLayout, Font = cell.Font, ForeColor = cell.ForeColor, TextWrap = cell.TextWrap, TextAlignment = cell.TextAlignment, BorderColor = cell.BorderColor, BorderLeftColor = cell.BorderLeftColor, BorderLeftShadowColor = cell.BorderLeftShadowColor, BorderTopColor = cell.BorderTopColor, BorderTopShadowColor = cell.BorderTopShadowColor, BorderRightColor = cell.BorderRightColor, BorderRightShadowColor = cell.BorderRightShadowColor, BorderBottomColor = cell.BorderBottomColor, BorderBottomShadowColor = cell.BorderBottomShadowColor, BorderLeftWidth = cell.BorderLeftWidth, BorderTopWidth = cell.BorderTopWidth, BorderRightWidth = cell.BorderRightWidth, BorderBottomWidth = cell.BorderBottomWidth, BorderBoxStyle = cell.BorderBoxStyle };
      GridVisualElement painter = this.FindPainter((GridVisualElement) cell);
      if (painter != null)
      {
        exportCellElement.BackColor = painter.BackColor;
        exportCellElement.BackColor2 = painter.BackColor2;
        exportCellElement.BackColor3 = painter.BackColor3;
        exportCellElement.BackColor4 = painter.BackColor4;
        exportCellElement.GradientStyle = painter.GradientStyle;
        exportCellElement.NumberOfColors = painter.NumberOfColors;
      }
      return exportCellElement;
    }

    private SizeF CalculateContentOffset(
      ContentAlignment alignment,
      double rowHeight,
      SizeF size,
      double columnWidth)
    {
      SizeF sizeF = new SizeF(0.0f, 0.0f);
      switch (alignment)
      {
        case ContentAlignment.TopLeft:
          sizeF.Width = (float) this.textOffset;
          sizeF.Height = (float) this.textOffset;
          break;
        case ContentAlignment.TopCenter:
          sizeF.Width = (float) Math.Max(this.textOffset, (columnWidth - (double) size.Width) / 2.0);
          sizeF.Height = (float) this.textOffset;
          break;
        case ContentAlignment.TopRight:
          sizeF.Width = (float) Math.Max(this.textOffset, columnWidth - (double) size.Width - this.textOffset);
          sizeF.Height = (float) this.textOffset;
          break;
        case ContentAlignment.MiddleLeft:
          sizeF.Width = (float) this.textOffset;
          sizeF.Height = (float) Math.Max(this.textOffset, (rowHeight - (double) size.Height) / 2.0);
          break;
        case ContentAlignment.MiddleCenter:
          sizeF.Width = (float) Math.Max(this.textOffset, (columnWidth - (double) size.Width) / 2.0);
          sizeF.Height = (float) Math.Max(this.textOffset, (rowHeight - (double) size.Height) / 2.0);
          break;
        case ContentAlignment.MiddleRight:
          sizeF.Width = (float) Math.Max(this.textOffset, columnWidth - (double) size.Width - this.textOffset);
          sizeF.Height = (float) Math.Max(this.textOffset, (rowHeight - (double) size.Height) / 2.0);
          break;
        case ContentAlignment.BottomLeft:
          sizeF.Width = (float) this.textOffset;
          sizeF.Height = (float) Math.Max(this.textOffset, rowHeight - (double) size.Height - this.textOffset);
          break;
        case ContentAlignment.BottomCenter:
          sizeF.Width = (float) Math.Max(this.textOffset, (columnWidth - (double) size.Width) / 2.0);
          sizeF.Height = (float) Math.Max(this.textOffset, rowHeight - (double) size.Height - this.textOffset);
          break;
        case ContentAlignment.BottomRight:
          sizeF.Width = (float) Math.Max(this.textOffset, columnWidth - (double) size.Width - this.textOffset);
          sizeF.Height = (float) Math.Max(this.textOffset, rowHeight - (double) size.Height - this.textOffset);
          break;
      }
      return sizeF;
    }

    private int GetNumberOfPages(ExportGridTraverser traverser)
    {
      int horizontalPagesCount = 1;
      int verticalPagesCount = 1;
      float headerRowHeight = -1f;
      SizeF drawArea = new SizeF(this.pageSize.Width - (float) this.pageMargins.Horizontal, this.pageSize.Height - (float) this.pageMargins.Vertical);
      if (this.ShowHeaderAndFooter)
        drawArea.Height -= (float) (this.HeaderHeight + this.FooterHeight);
      double currentPageHeight = 0.0;
      double currentRowWidth = 0.0;
      traverser.Reset();
      traverser.ProcessHierarchy = this.ExportHierarchy;
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews)
        this.hierarchyRowsStack = new Stack<GridViewHierarchyRowInfo>();
      this.CalculateRowPages(traverser, drawArea, ref currentRowWidth, ref headerRowHeight, ref horizontalPagesCount, ref currentPageHeight, ref verticalPagesCount);
      if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0)
      {
        while (this.hierarchyRowsStack.Count > 0 && this.MoveToNextActiveViewOfLastHierarchyRow(traverser))
          this.CalculateRowPages(traverser, drawArea, ref currentRowWidth, ref headerRowHeight, ref horizontalPagesCount, ref currentPageHeight, ref verticalPagesCount);
      }
      traverser.Reset();
      if (!this.FitToPageWidth)
        return verticalPagesCount * horizontalPagesCount;
      return verticalPagesCount;
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

    private void CalculateRowPages(
      ExportGridTraverser traverser,
      SizeF drawArea,
      ref double currentRowWidth,
      ref float headerRowHeight,
      ref int horizontalPagesCount,
      ref double currentPageHeight,
      ref int verticalPagesCount)
    {
      while (traverser.MoveNext())
      {
        GridViewRowInfo gridViewRowInfo = traverser.Current;
        if (this.ShouldExportRow(gridViewRowInfo))
        {
          GridViewHierarchyRowInfo row = gridViewRowInfo as GridViewHierarchyRowInfo;
          if (this.ChildViewExportMode == ChildViewExportMode.ExportAllViews && this.hierarchyRowsStack.Count > 0 && traverser.Current is GridViewGroupRowInfo)
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
                gridViewRowInfo = this.TraverseAllChildViews(traverser);
                break;
            }
          }
          currentRowWidth = 0.0;
          int num = 1;
          double rowHeight = GridExportUtils.GetRowHeight(this.radGridView, this.rowProvider, this.cellProvider, gridViewRowInfo, this.ExportVisualSettings);
          if ((double) headerRowHeight == -1.0 && gridViewRowInfo is GridViewTableHeaderRowInfo)
          {
            headerRowHeight = (float) rowHeight;
            drawArea.Height -= headerRowHeight;
          }
          if (!this.FitToPageWidth)
          {
            foreach (GridViewCellInfo cell in gridViewRowInfo.Cells)
            {
              float width = (float) cell.ColumnInfo.Width;
              if (currentRowWidth + (double) width < (double) drawArea.Width)
              {
                currentRowWidth += (double) width;
              }
              else
              {
                if ((double) width > (double) drawArea.Width)
                {
                  for (; (double) width > (double) drawArea.Width; width -= drawArea.Width)
                    ++num;
                }
                else
                  ++num;
                currentRowWidth = (double) width;
              }
            }
            if (horizontalPagesCount < num)
              horizontalPagesCount = num;
          }
          if (currentPageHeight + rowHeight < (double) drawArea.Height)
          {
            currentPageHeight += rowHeight;
          }
          else
          {
            ++verticalPagesCount;
            currentPageHeight = rowHeight;
          }
        }
      }
    }

    protected virtual void DrawHeader()
    {
      if (!this.ShowHeaderAndFooter)
        return;
      this.editor.SavePosition();
      this.editor.CreateMatrixPosition();
      this.editor.TranslatePosition((double) this.pageMargins.Left, (double) this.pageMargins.Top);
      this.editor.SaveProperties();
      this.editor.SetTextFontSize((double) this.HeaderFont.Size);
      this.editor.TrySetFont(this.HeaderFont.Name, this.HeaderFont.Style);
      this.DrawHeaderFooter(this.header);
      this.OnHeaderExported(new ExportEventArgs(this.editor, new RectangleF((float) this.pageMargins.Left, (float) this.pageMargins.Top, this.pageSize.Width - (float) this.pageMargins.Horizontal, (float) this.HeaderHeight)));
      this.editor.RestoreProperties();
      this.editor.RestorePosition();
    }

    protected virtual void DrawFooter()
    {
      if (!this.ShowHeaderAndFooter)
        return;
      this.editor.SavePosition();
      this.editor.CreateMatrixPosition();
      this.editor.TranslatePosition((double) this.pageMargins.Left, (double) this.pageSize.Height - (double) this.pageMargins.Bottom - this.FooterHeight);
      this.editor.SaveProperties();
      this.editor.SetTextFontSize((double) this.FooterFont.Size);
      this.editor.TrySetFont(this.FooterFont.Name, this.FooterFont.Style);
      this.DrawHeaderFooter(this.footer);
      this.OnFooterExported(new ExportEventArgs(this.editor, new RectangleF((float) this.pageMargins.Left, (float) ((double) this.pageSize.Height - (double) this.pageMargins.Bottom - this.FooterHeight), this.pageSize.Width - (float) this.pageMargins.Horizontal, (float) this.FooterHeight)));
      this.editor.RestoreProperties();
      this.editor.RestorePosition();
    }

    private void DrawHeaderFooter(PageHeaderFooter headerFooter)
    {
      string str1 = !headerFooter.ReverseOnEvenPages || this.pageNumber % 2 != 0 ? headerFooter.LeftText : headerFooter.RightText;
      string centerText = headerFooter.CenterText;
      string str2 = !headerFooter.ReverseOnEvenPages || this.pageNumber % 2 != 0 ? headerFooter.RightText : headerFooter.LeftText;
      SizeF availableSize = new SizeF((float) (((double) this.pageSize.Width - (double) this.pageMargins.Horizontal) / 3.0), (float) headerFooter.Height);
      SizeF logoSize = this.CalculateLogoSize(availableSize);
      StringAlignment alignment1 = StringAlignment.Near;
      this.DrawHeaderFooterImage(str1, availableSize, logoSize, alignment1);
      StringAlignment alignment2 = StringAlignment.Center;
      this.DrawHeaderFooterImage(centerText, availableSize, logoSize, alignment2);
      StringAlignment alignment3 = StringAlignment.Far;
      this.DrawHeaderFooterImage(str2, availableSize, logoSize, alignment3);
      StringAlignment alignment4 = StringAlignment.Near;
      this.DrawHeaderFooterText(this.ParseHeaderFooterString(str1), headerFooter.Height, alignment4);
      StringAlignment alignment5 = StringAlignment.Center;
      this.DrawHeaderFooterText(this.ParseHeaderFooterString(centerText), headerFooter.Height, alignment5);
      StringAlignment alignment6 = StringAlignment.Far;
      this.DrawHeaderFooterText(this.ParseHeaderFooterString(str2), headerFooter.Height, alignment6);
    }

    private SizeF CalculateLogoSize(SizeF availableSize)
    {
      SizeF sizeF = SizeF.Empty;
      switch (this.LogoLayout)
      {
        case LogoLayout.None:
          sizeF = (SizeF) this.Logo.Size;
          break;
        case LogoLayout.Fit:
          float num1 = availableSize.Width / (float) this.Logo.Size.Width;
          float num2 = availableSize.Height / (float) this.Logo.Size.Height;
          float num3 = (double) num1 > (double) num2 ? num2 : num1;
          sizeF.Width = num3 * (float) this.Logo.Size.Width;
          sizeF.Height = num3 * (float) this.Logo.Size.Height;
          break;
        case LogoLayout.Stretch:
          sizeF = availableSize;
          break;
      }
      return sizeF;
    }

    private void DrawHeaderFooterText(string text, double height, StringAlignment alignment)
    {
      double width = (double) this.pageSize.Width - (double) this.pageMargins.Horizontal;
      this.pdfExportRenderer.CreateBlock();
      this.pdfExportRenderer.ApplyEditorGraphicAndTextPropertiesToBlock();
      this.pdfExportRenderer.InsertBlockText(text);
      SizeF size1 = this.pdfExportRenderer.MeasureBlock();
      SizeF size2 = new SizeF((float) width, (float) height);
      bool flag = false;
      if ((double) size1.Width > width)
      {
        flag = true;
        size1 = this.pdfExportRenderer.MeasureBlock(size2);
      }
      SizeF footerContentOffset = this.CalculateHeaderFooterContentOffset(height, size1, alignment);
      this.pdfExportRenderer.SetBlockLeftIndent((double) footerContentOffset.Width);
      this.editor.SavePosition();
      this.editor.TranslatePosition(0.0, (double) footerContentOffset.Height);
      SizeF size3 = this.pdfExportRenderer.MeasureBlock();
      if (flag)
        size3 = this.pdfExportRenderer.MeasureBlock(size2);
      if ((double) size3.Width > width || (double) size3.Height > height)
        this.editor.PushClipping(this.editor.OffsetX, this.editor.OffsetY, width, height - (double) footerContentOffset.Height);
      if (flag)
        this.pdfExportRenderer.DrawBlock(size3);
      else
        this.pdfExportRenderer.DrawBlock();
      this.editor.PopClipping();
      this.editor.RestorePosition();
    }

    private void DrawHeaderFooterImage(
      string text,
      SizeF availableSize,
      SizeF logoSize,
      StringAlignment alignment)
    {
      if (!this.HasLogoInHeaderFooterString(text) || this.Logo == null)
        return;
      byte[] byteArray = GridExportUtils.ConvertImageToByteArray(this.Logo);
      this.pdfExportRenderer.CreateBlock();
      using (Stream stream = (Stream) new MemoryStream(byteArray))
        this.pdfExportRenderer.InsertBlockImage(stream, (double) logoSize.Width, (double) logoSize.Height);
      SizeF logoOffset = this.CalculateLogoOffset(availableSize, logoSize, this.LogoAlignment);
      this.pdfExportRenderer.SetBlockLeftIndent((double) this.CalculateHeaderFooterContentOffset((double) availableSize.Height, availableSize, alignment).Width + (double) logoOffset.Width);
      this.editor.SavePosition();
      this.editor.TranslatePosition(0.0, (double) logoOffset.Height);
      this.pdfExportRenderer.DrawBlock();
      this.editor.RestorePosition();
    }

    private SizeF CalculateLogoOffset(
      SizeF availableSize,
      SizeF logoSize,
      ContentAlignment logoAlignment)
    {
      SizeF empty = SizeF.Empty;
      switch (logoAlignment)
      {
        case ContentAlignment.TopLeft:
          empty.Width = 0.0f;
          empty.Height = 0.0f;
          break;
        case ContentAlignment.TopCenter:
          empty.Width = (float) (((double) availableSize.Width - (double) logoSize.Width) / 2.0);
          empty.Height = 0.0f;
          break;
        case ContentAlignment.TopRight:
          empty.Width = availableSize.Width - logoSize.Width;
          empty.Height = 0.0f;
          break;
        case ContentAlignment.MiddleLeft:
          empty.Width = 0.0f;
          empty.Height = (float) (((double) availableSize.Height - (double) logoSize.Height) / 2.0);
          break;
        case ContentAlignment.MiddleCenter:
          empty.Width = (float) (((double) availableSize.Width - (double) logoSize.Width) / 2.0);
          empty.Height = (float) (((double) availableSize.Height - (double) logoSize.Height) / 2.0);
          break;
        case ContentAlignment.MiddleRight:
          empty.Width = availableSize.Width - logoSize.Width;
          empty.Height = (float) (((double) availableSize.Height - (double) logoSize.Height) / 2.0);
          break;
        case ContentAlignment.BottomLeft:
          empty.Width = 0.0f;
          empty.Height = availableSize.Height - logoSize.Height;
          break;
        case ContentAlignment.BottomCenter:
          empty.Width = (float) (((double) availableSize.Width - (double) logoSize.Width) / 2.0);
          empty.Height = availableSize.Height - logoSize.Height;
          break;
        case ContentAlignment.BottomRight:
          empty.Width = availableSize.Width - logoSize.Width;
          empty.Height = availableSize.Height - logoSize.Height;
          break;
      }
      if ((double) empty.Width < 0.0)
        empty.Width = 0.0f;
      if ((double) empty.Height < 0.0)
        empty.Height = 0.0f;
      return empty;
    }

    private SizeF CalculateHeaderFooterContentOffset(
      double height,
      SizeF size,
      StringAlignment alignment)
    {
      double num = (double) this.pageSize.Width - (double) this.pageMargins.Horizontal;
      SizeF sizeF = new SizeF(0.0f, 0.0f);
      sizeF.Height = ((float) height - size.Height) / 2f;
      switch (alignment)
      {
        case StringAlignment.Near:
          sizeF.Width = 0.0f;
          break;
        case StringAlignment.Center:
          sizeF.Width = ((float) num - size.Width) / 2f;
          break;
        case StringAlignment.Far:
          sizeF.Width = (float) num - size.Width;
          break;
      }
      sizeF.Width = (double) sizeF.Width > 0.0 ? sizeF.Width : 0.0f;
      sizeF.Height = (double) sizeF.Height > 0.0 ? sizeF.Height : 0.0f;
      return sizeF;
    }

    protected string ParseHeaderFooterString(string s)
    {
      s = s.Replace("[Page #]", this.pageNumber.ToString());
      s = s.Replace("[Total Pages]", this.totalPagesCount.ToString());
      s = s.Replace("[Date Exported]", this.exportStartDate.ToShortDateString());
      s = s.Replace("[Time Exported]", this.exportStartDate.ToShortTimeString());
      s = s.Replace("[User Name]", SystemInformation.UserName);
      s = s.Replace("[Logo]", string.Empty);
      return s;
    }

    private bool HasLogoInHeaderFooterString(string s)
    {
      return s.Contains("[Logo]");
    }

    private void DrawRow(GridViewRowInfo gridViewRowInfo, int rowIndex)
    {
      double num1 = 0.0;
      double num2 = GridExportUtils.GetRowHeight(this.RadGridViewToExport, this.rowProvider, this.cellProvider, gridViewRowInfo, this.ExportVisualSettings) * this.Scale;
      this.pdfExportRenderer.CurrentMatrixColumn = 0;
      this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
      double num3 = (double) this.pageSize.Width - (double) this.pageMargins.Horizontal;
      int visibleColumnCount = this.GetVisibleColumnCount();
      if (this.editor.OffsetY + num2 > (double) this.pageSize.Height - (double) this.pageMargins.Bottom - this.footer.Height)
      {
        this.CallCurrentRowPageExported();
        this.editor = this.pdfExportRenderer.GetDownPageEditor();
        this.editor.CreateMatrixPosition();
        this.editor.TranslatePosition((double) this.pageMargins.Left, (double) this.pageMargins.Top + this.HeaderHeight);
        this.pageNumber = this.pdfExportRenderer.GetCurrentPageNumber();
        this.DrawHeader();
        this.DrawFooter();
        if (this.ExportHeaderRowOnEachPage && this.headerRow != null)
        {
          this.isDrawingHeaderRow = true;
          int currentMatrixColumn = this.pdfExportRenderer.CurrentMatrixColumn;
          this.DrawRow((GridViewRowInfo) this.headerRow, 0);
          this.pdfExportRenderer.CurrentMatrixColumn = currentMatrixColumn;
          this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
          this.isDrawingHeaderRow = false;
        }
      }
      int num4 = 0;
      if (this.CellFormatting != null || this.CellPaint != null)
        num4 = gridViewRowInfo.Index;
      System.Type type = gridViewRowInfo.GetType();
      if (gridViewRowInfo is GridViewGroupRowInfo)
      {
        GridViewGroupRowInfo viewGroupRowInfo = gridViewRowInfo as GridViewGroupRowInfo;
        GridGroupHeaderRowElement headerRowElement = (GridGroupHeaderRowElement) null;
        GridPdfExportCellElement exportCellElement;
        if (this.ExportVisualSettings)
        {
          headerRowElement = (GridGroupHeaderRowElement) this.rowProvider.CreateElement(gridViewRowInfo, (object) null);
          headerRowElement.InitializeRowView(this.radGridView.TableElement);
          this.radGridView.TableElement.Children.Add((RadElement) headerRowElement);
          headerRowElement.Initialize(gridViewRowInfo);
          headerRowElement.UpdateInfo();
          exportCellElement = this.CreatePdfExportCellElement((GridCellElement) headerRowElement.ContentCell);
          exportCellElement.Text = headerRowElement.ContentCell.Text;
        }
        else
        {
          exportCellElement = new GridPdfExportCellElement();
          exportCellElement.Text = viewGroupRowInfo.HeaderText;
          exportCellElement.TextAlignment = ContentAlignment.MiddleLeft;
          exportCellElement.BackColor = Color.LightGray;
          exportCellElement.NumberOfColors = 1;
        }
        exportCellElement.RowType = type;
        exportCellElement.RowIndex = num4;
        foreach (GridViewCellInfo cell in gridViewRowInfo.Cells)
        {
          if (this.ShouldExportColumn(cell.ColumnInfo))
            num1 += (double) cell.ColumnInfo.Width;
        }
        if (this.FitToPageWidth)
          this.fitCoefficient = num3 / (num1 * this.Scale + 1.0);
        List<double> doubleList1 = new List<double>();
        if (!this.FitToPageWidth && num1 > num3)
        {
          int index1 = 0;
          doubleList1.Add(0.0);
          foreach (GridViewCellInfo cell in gridViewRowInfo.Cells)
          {
            if (this.ShouldExportColumn(cell.ColumnInfo))
            {
              double num5 = (double) cell.ColumnInfo.Width * this.Scale;
              if (doubleList1[index1] + num5 > num3 && doubleList1[index1] > 0.0)
              {
                doubleList1.Add(0.0);
                ++index1;
              }
              List<double> doubleList2;
              int index2;
              (doubleList2 = doubleList1)[index2 = index1] = doubleList2[index2] + num5;
            }
          }
        }
        else
          doubleList1.Add(num1 * this.fitCoefficient * this.Scale);
        double x = (double) gridViewRowInfo.Group.Level * this.rowIndent * this.fitCoefficient;
        this.editor.CreateMatrixPosition(this.editor.OffsetX, this.editor.OffsetY);
        this.editor.TranslatePosition(x, 0.0);
        this.OnCellFormatting(new PdfExportCellFormattingEventArgs(exportCellElement, gridViewRowInfo, (GridViewColumn) null));
        for (int index = 0; index < doubleList1.Count; ++index)
        {
          double num5 = doubleList1[index] - x;
          string[] text = this.ApplyLineBreaksToText((object) exportCellElement.Text);
          this.DrawCell(exportCellElement, text, num2, num5, num5, false);
          if (index < doubleList1.Count - 1)
          {
            bool flag = this.pdfExportRenderer.IsMatrixCurrentPageLastOnRow();
            double offsetY = this.editor.OffsetY;
            this.editor.TranslatePosition(-(this.editor.OffsetX - (double) this.pageMargins.Left), num2);
            this.editor = this.pdfExportRenderer.GetRightPageEditor();
            this.editor.CreateMatrixPosition();
            this.editor.TranslatePosition((double) this.pageMargins.Left + x, offsetY);
            this.pageNumber = this.pdfExportRenderer.GetCurrentPageNumber();
            if (flag)
            {
              this.DrawHeader();
              this.DrawFooter();
            }
          }
        }
        if (this.ExportVisualSettings)
          GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, (GridRowElement) headerRowElement);
      }
      else
      {
        List<GridPdfExportCellElement> exportCellElementList = new List<GridPdfExportCellElement>();
        List<GridCellElement> gridCellElementList = new List<GridCellElement>();
        GridRowElement gridRowElement = (GridRowElement) null;
        if (this.ExportVisualSettings)
        {
          gridRowElement = (GridRowElement) this.rowProvider.GetElement(gridViewRowInfo, (object) null);
          gridRowElement.InitializeRowView(this.radGridView.TableElement);
          this.radGridView.TableElement.Children.Add((RadElement) gridRowElement);
          gridRowElement.Initialize(gridViewRowInfo);
        }
        for (int index = 0; index < gridViewRowInfo.Cells.Count; ++index)
        {
          object objectCellValue = this.GetObjectCellValue(gridViewRowInfo.Cells[index]);
          GridPdfExportCellElement exportCellElement;
          if (this.ExportVisualSettings)
          {
            GridCellElement element = (GridCellElement) this.cellProvider.GetElement((GridViewColumn) gridRowElement.RowInfo.ViewTemplate.Columns[index], (object) gridRowElement);
            gridRowElement.Children.Add((RadElement) element);
            element.Initialize((GridViewColumn) gridRowElement.RowInfo.ViewTemplate.Columns[index], gridRowElement);
            gridCellElementList.Add(element);
            exportCellElement = this.CreatePdfExportCellElement(element);
            if (gridViewRowInfo.ViewInfo.ViewTemplate.IsSelfReference)
            {
              GridDataCellElement gridDataCellElement = element as GridDataCellElement;
              if (gridDataCellElement != null && gridDataCellElement.SelfReferenceLayout != null)
              {
                gridDataCellElement.SelfReferenceLayout.StackLayoutElement.Measure(new SizeF((float) element.ColumnInfo.Width, float.PositiveInfinity));
                this.cellSelfReferenceIndent = (double) gridDataCellElement.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
              }
            }
          }
          else
          {
            exportCellElement = new GridPdfExportCellElement();
            exportCellElement.TextAlignment = ContentAlignment.MiddleCenter;
            if (gridViewRowInfo is GridViewTableHeaderRowInfo || gridViewRowInfo is GridViewHierarchyRowInfo)
            {
              exportCellElement.BackColor = Color.LightGray;
              exportCellElement.NumberOfColors = 1;
            }
            GridViewImageColumn columnInfo = gridViewRowInfo.Cells[index].ColumnInfo as GridViewImageColumn;
            if (columnInfo != null)
            {
              object result = (object) null;
              RadDataConverter.Instance.TryFormat(gridViewRowInfo.Cells[index].Value, typeof (Image), (IDataConversionInfoProvider) columnInfo, out result);
              exportCellElement.Image = result as Image;
              exportCellElement.ImageAlignment = columnInfo.ImageAlignment;
              exportCellElement.ImageLayout = columnInfo.ImageLayout;
            }
          }
          GridViewDataColumn columnInfo1 = gridViewRowInfo.Cells[index].ColumnInfo as GridViewDataColumn;
          if (columnInfo1 != null && !string.IsNullOrEmpty(columnInfo1.FormatString))
          {
            GridViewComboBoxColumn viewComboBoxColumn = columnInfo1 as GridViewComboBoxColumn;
            if (viewComboBoxColumn != null)
            {
              columnInfo1.DataType = viewComboBoxColumn.DisplayMemberDataType;
              columnInfo1.DataTypeConverter = TypeDescriptor.GetConverter(viewComboBoxColumn.DisplayMemberDataType);
            }
            exportCellElement.Text = RadDataConverter.Instance.Format(objectCellValue, typeof (string), (IDataConversionInfoProvider) columnInfo1) as string;
          }
          else
            exportCellElement.Text = objectCellValue == null ? string.Empty : objectCellValue.ToString();
          exportCellElement.RowType = type;
          exportCellElement.ColumnType = gridViewRowInfo.Cells[index].ColumnInfo.GetType();
          exportCellElement.RowIndex = num4;
          exportCellElement.ColumnIndex = index;
          exportCellElementList.Add(exportCellElement);
          if (this.ExportHeaderRowOnEachPage && gridViewRowInfo is GridViewTableHeaderRowInfo && this.headerRow == null)
          {
            this.headerRow = new GridViewTableHeaderRowInfo(gridViewRowInfo.ViewInfo);
            this.headerRow.Height = gridViewRowInfo.Height;
          }
          if (this.ShouldExportColumn(gridViewRowInfo.Cells[index].ColumnInfo))
            num1 += (double) gridViewRowInfo.Cells[index].ColumnInfo.Width;
        }
        if (this.FitToPageWidth)
          this.fitCoefficient = num3 / (num1 * this.Scale + 1.0);
        if (!(gridViewRowInfo is GridViewTableHeaderRowInfo))
        {
          this.editor.CreateMatrixPosition(this.editor.OffsetX, this.editor.OffsetY);
          this.editor.TranslatePosition(this.currentRowIndent * this.fitCoefficient * this.Scale, 0.0);
        }
        bool flag = true;
        for (int index1 = 0; index1 < gridViewRowInfo.Cells.Count; ++index1)
        {
          int index2 = index1;
          if (this.radGridView.RightToLeft == RightToLeft.Yes)
            index2 = gridViewRowInfo.Cells.Count - index1 - 1;
          if (!this.ShouldExportColumn(gridViewRowInfo.Cells[index2].ColumnInfo))
          {
            if (this.exportVisualSettings && !this.isDrawingHeaderRow)
              GridExportUtils.ReleaseCellElement(this.cellProvider, gridRowElement, gridCellElementList[index2]);
          }
          else
          {
            double num5 = (double) gridViewRowInfo.Cells[index2].ColumnInfo.Width * this.fitCoefficient - this.currentRowIndent * this.fitCoefficient / (double) visibleColumnCount;
            if (this.cellSelfReferenceIndent != 0.0)
            {
              this.cellSelfReferenceIndent *= this.fitCoefficient - this.currentRowIndent * this.fitCoefficient / (double) visibleColumnCount;
              this.cellSelfReferenceIndent *= this.Scale;
            }
            double num6 = num5 * this.Scale;
            if (flag && gridViewRowInfo is GridViewTableHeaderRowInfo)
            {
              num6 += this.currentRowIndent * this.fitCoefficient * this.Scale;
              flag = false;
            }
            double num7 = num6 < 0.0 ? 0.0 : num6;
            this.OnCellFormatting(new PdfExportCellFormattingEventArgs(exportCellElementList[index2], gridViewRowInfo, gridViewRowInfo.Cells[index1].ColumnInfo));
            string[] text = this.ApplyLineBreaksToText((object) exportCellElementList[index2].Text);
            this.DrawCell(exportCellElementList[index2], text, num2, num7, num7, false);
            this.editor.CreateMatrixPosition(this.editor.OffsetX, this.editor.OffsetY);
            this.editor.TranslatePosition(num7 % num3, 0.0);
            if (!this.isDrawingHeaderRow && this.ExportVisualSettings)
              GridExportUtils.ReleaseCellElement(this.cellProvider, gridRowElement, gridCellElementList[index2]);
          }
        }
        if (this.ExportVisualSettings && !this.isDrawingHeaderRow)
          GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, gridRowElement);
      }
      this.editor.TranslatePosition(-(this.editor.OffsetX - (double) this.pageMargins.Left), num2);
    }

    private void DrawViewDefinitionRow(
      GridViewRowInfo gridViewRowInfo,
      TableViewRowLayoutBase rowLayout,
      int currentRowNum)
    {
      GridRowElement gridRowElement = (GridRowElement) null;
      if (this.ExportVisualSettings)
      {
        gridRowElement = (GridRowElement) this.rowProvider.GetElement(gridViewRowInfo, (object) null);
        gridRowElement.InitializeRowView(this.radGridView.TableElement);
        this.radGridView.TableElement.Children.Add((RadElement) gridRowElement);
        gridRowElement.Initialize(gridViewRowInfo);
      }
      float rowHeight = (float) rowLayout.GetRowHeight(gridViewRowInfo);
      if (this.editor.OffsetY + (double) rowHeight > (double) this.pageSize.Height - (double) this.pageMargins.Bottom - this.footer.Height)
      {
        this.CallCurrentRowPageExported();
        this.editor = this.pdfExportRenderer.GetDownPageEditor();
        this.editor.CreateMatrixPosition();
        this.editor.TranslatePosition((double) this.pageMargins.Left, (double) this.pageMargins.Top + this.HeaderHeight);
        this.pageNumber = this.pdfExportRenderer.GetCurrentPageNumber();
        this.DrawHeader();
        this.DrawFooter();
        if (this.ExportHeaderRowOnEachPage && this.headerRow != null)
        {
          this.isDrawingHeaderRow = true;
          int currentMatrixColumn = this.pdfExportRenderer.CurrentMatrixColumn;
          this.DrawViewDefinitionRow((GridViewRowInfo) this.headerRow, rowLayout, currentRowNum);
          this.pdfExportRenderer.CurrentMatrixColumn = currentMatrixColumn;
          this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
          this.isDrawingHeaderRow = false;
        }
      }
      if (this.ExportHeaderRowOnEachPage && gridViewRowInfo is GridViewTableHeaderRowInfo && this.headerRow == null)
        this.headerRow = new GridViewTableHeaderRowInfo(gridViewRowInfo.ViewInfo);
      RectangleF rectangleF = RectangleF.Empty;
      double num1 = 0.0;
      double offsetX = this.editor.OffsetX;
      double offsetY = this.editor.OffsetY;
      int num2 = 0;
      if (this.CellFormatting != null || this.CellPaint != null)
        num2 = gridViewRowInfo.Index;
      System.Type type = gridViewRowInfo.GetType();
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
      {
        if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
        {
          if (rowLayout is ColumnGroupRowLayout)
            rectangleF = (rowLayout as ColumnGroupRowLayout).GetCorrectedColumnBounds(gridViewRowInfo, renderColumn, false, new RectangleF(0.0f, 0.0f, rowLayout.DesiredSize.Width, rowHeight));
          else if (rowLayout is HtmlViewRowLayout)
          {
            HtmlViewCellArrangeInfo arrangeInfo = (rowLayout as HtmlViewRowLayout).GetArrangeInfo(renderColumn);
            if (arrangeInfo != null)
              rectangleF = arrangeInfo.Bounds;
            else
              continue;
          }
          if (!(rectangleF == RectangleF.Empty))
          {
            float num3 = this.pageSize.Width - (float) this.pageMargins.Horizontal;
            if (!this.FitToPageWidth)
            {
              int num4 = (int) ((double) rectangleF.X / (double) num3);
              if (this.pdfExportRenderer.CurrentMatrixColumn != num4)
              {
                this.pdfExportRenderer.CurrentMatrixColumn = 0;
                this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
                while (this.pdfExportRenderer.CurrentMatrixColumn != num4)
                  this.editor = this.pdfExportRenderer.GetRightPageEditor();
              }
              if (num4 == 0 || offsetY + (double) rectangleF.Y != this.editor.OffsetY)
              {
                this.editor.CreateMatrixPosition(offsetX, offsetY);
                this.editor.TranslatePosition((double) rectangleF.X % (double) num3, (double) rectangleF.Y);
              }
            }
            else
            {
              if ((double) rowLayout.DesiredSize.Width > (double) this.pageSize.Width - (double) this.pageMargins.Horizontal)
              {
                float num4 = (float) (((double) this.pageSize.Width - (double) this.pageMargins.Horizontal) / ((double) rowLayout.DesiredSize.Width + 1.0));
                rectangleF.X *= num4;
                rectangleF.Width *= num4;
              }
              this.editor.CreateMatrixPosition(offsetX, offsetY);
              this.editor.TranslatePosition((double) rectangleF.X, (double) rectangleF.Y);
            }
            GridPdfExportCellElement exportCellElement;
            if (gridViewRowInfo is GridViewTableHeaderRowInfo)
            {
              GridViewCellInfo cell = this.radGridView.MasterView.TableHeaderRow.Cells[renderColumn.Name];
              if (this.ExportVisualSettings)
              {
                exportCellElement = this.InitializeVirtualCellElement(gridRowElement, renderColumn);
              }
              else
              {
                exportCellElement = new GridPdfExportCellElement();
                exportCellElement.NumberOfColors = 1;
                exportCellElement.BackColor = Color.LightGray;
              }
              exportCellElement.Text = renderColumn.HeaderText;
              exportCellElement.TextAlignment = renderColumn.HeaderTextAlignment;
              exportCellElement.TextWrap = renderColumn.WrapText;
            }
            else if (gridViewRowInfo is GridViewSummaryRowInfo)
            {
              GridViewCellInfo cell = (gridViewRowInfo as GridViewSummaryRowInfo).Cells[renderColumn.Name];
              if (cell != null)
              {
                exportCellElement = !this.ExportVisualSettings ? new GridPdfExportCellElement() : this.InitializeVirtualCellElement(gridRowElement, renderColumn);
                exportCellElement.Text = cell.Value != null ? cell.Value.ToString() : string.Empty;
              }
              else
                continue;
            }
            else
            {
              GridViewCellInfo cell = gridViewRowInfo.Cells[renderColumn.Name];
              if (cell != null)
              {
                if (this.ExportVisualSettings)
                {
                  exportCellElement = this.InitializeVirtualCellElement(gridRowElement, renderColumn);
                }
                else
                {
                  exportCellElement = new GridPdfExportCellElement();
                  exportCellElement.BackColor = Color.White;
                  exportCellElement.BorderColor = Color.Black;
                }
                if (renderColumn is GridViewImageColumn)
                {
                  if (!this.ExportVisualSettings)
                  {
                    object obj = cell.Value;
                    if (obj == null || obj == DBNull.Value)
                      exportCellElement.Image = (Image) null;
                    else if (obj is byte[])
                      exportCellElement.Image = ImageHelper.GetImageFromBytes((byte[]) obj);
                    else if (obj is Image)
                      exportCellElement.Image = obj as Image;
                    if (exportCellElement.Image != null)
                    {
                      GridViewImageColumn columnInfo = cell.ColumnInfo as GridViewImageColumn;
                      if (columnInfo != null)
                      {
                        exportCellElement.ImageAlignment = columnInfo.ImageAlignment;
                        exportCellElement.ImageLayout = columnInfo.ImageLayout;
                      }
                      else
                      {
                        exportCellElement.ImageAlignment = ContentAlignment.MiddleCenter;
                        exportCellElement.ImageLayout = ImageLayout.None;
                      }
                    }
                  }
                }
                else
                {
                  GridViewDataColumn columnInfo1 = cell.ColumnInfo as GridViewDataColumn;
                  object lookupValue = cell.Value;
                  GridViewComboBoxColumn columnInfo2 = cell.ColumnInfo as GridViewComboBoxColumn;
                  if (columnInfo2 != null && columnInfo2.HasLookupValue)
                    lookupValue = columnInfo2.GetLookupValue(lookupValue);
                  if (columnInfo1 != null && !string.IsNullOrEmpty(columnInfo1.FormatString))
                  {
                    if (cell.ColumnInfo is GridViewComboBoxColumn)
                    {
                      GridViewComboBoxColumn columnInfo3 = (GridViewComboBoxColumn) cell.ColumnInfo;
                      columnInfo1.DataType = columnInfo3.DisplayMemberDataType;
                      columnInfo1.DataTypeConverter = TypeDescriptor.GetConverter(columnInfo3.DisplayMemberDataType);
                    }
                    exportCellElement.Text = RadDataConverter.Instance.Format(lookupValue, typeof (string), (IDataConversionInfoProvider) columnInfo1) as string;
                  }
                  else
                    exportCellElement.Text = lookupValue != null ? lookupValue.ToString() : string.Empty;
                  exportCellElement.TextAlignment = cell.ColumnInfo.TextAlignment;
                  exportCellElement.TextWrap = cell.ColumnInfo.WrapText;
                }
              }
              else
                continue;
            }
            exportCellElement.RowType = type;
            exportCellElement.ColumnType = renderColumn.GetType();
            exportCellElement.RowIndex = num2;
            exportCellElement.ColumnIndex = renderColumn.Index;
            this.OnCellFormatting(new PdfExportCellFormattingEventArgs(exportCellElement, gridViewRowInfo, renderColumn));
            string[] text = this.ApplyLineBreaksToText((object) exportCellElement.Text);
            this.DrawCell(exportCellElement, text, (double) rectangleF.Height, (double) rectangleF.Width, (double) rectangleF.Width, false);
            if (!this.FitToPageWidth)
            {
              this.editor.CreateMatrixPosition(this.editor.OffsetX, this.editor.OffsetY);
              this.editor.TranslatePosition((double) rectangleF.Width % (double) num3, 0.0);
            }
            num1 = Math.Max(num1, (double) rectangleF.Y + (double) rectangleF.Height);
          }
        }
      }
      if (!this.FitToPageWidth)
      {
        int num3 = 0;
        do
        {
          this.pdfExportRenderer.CurrentMatrixColumn = num3;
          this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
          ++num3;
          this.editor.CreateMatrixPosition(this.editor.OffsetX, offsetY);
          this.editor.TranslatePosition(0.0, num1);
        }
        while (!this.pdfExportRenderer.IsMatrixCurrentPageLastOnRow());
        this.pdfExportRenderer.CurrentMatrixColumn = 0;
        this.editor = this.pdfExportRenderer.GetCurrentPageEditor();
        this.editor.CreateMatrixPosition(offsetX, offsetY + num1);
      }
      else
      {
        this.editor.CreateMatrixPosition(offsetX, offsetY);
        this.editor.TranslatePosition(0.0, num1);
      }
      if (!this.ExportVisualSettings || this.isDrawingHeaderRow)
        return;
      GridExportUtils.ReleaseRowElement(this.radGridView, this.rowProvider, gridRowElement);
    }

    private GridPdfExportCellElement InitializeVirtualCellElement(
      GridRowElement vRowElement,
      GridViewColumn col)
    {
      int index1 = col.Index;
      int index2 = index1 < 0 ? 0 : index1;
      GridCellElement element = (GridCellElement) this.cellProvider.GetElement((GridViewColumn) vRowElement.RowInfo.ViewTemplate.Columns[index2], (object) vRowElement);
      vRowElement.Children.Add((RadElement) element);
      element.Initialize((GridViewColumn) vRowElement.RowInfo.ViewTemplate.Columns[index2], vRowElement);
      GridPdfExportCellElement exportCellElement = this.CreatePdfExportCellElement(element);
      GridExportUtils.ReleaseCellElement(this.cellProvider, vRowElement, element);
      return exportCellElement;
    }

    private void DrawRowWideCell(GridViewRowInfo gridViewRowInfo, TableViewRowLayoutBase rowLayout)
    {
      Size size = Size.Empty;
      if (rowLayout is ColumnGroupRowLayout)
        size = this.GetRowSize(gridViewRowInfo, (TableViewRowLayoutBase) (rowLayout as ColumnGroupRowLayout));
      else if (rowLayout is HtmlViewRowLayout)
        size = this.GetRowSize(gridViewRowInfo, (TableViewRowLayoutBase) (rowLayout as HtmlViewRowLayout));
      if (!(gridViewRowInfo is GridViewGroupRowInfo))
        return;
      GridViewGroupRowInfo viewGroupRowInfo = gridViewRowInfo as GridViewGroupRowInfo;
      GridPdfExportCellElement exportCellElement1;
      if (this.ExportVisualSettings)
      {
        GridGroupHeaderRowElement element = (GridGroupHeaderRowElement) this.rowProvider.CreateElement(gridViewRowInfo, (object) null);
        element.InitializeRowView(this.radGridView.TableElement);
        this.radGridView.TableElement.Children.Add((RadElement) element);
        element.Initialize(gridViewRowInfo);
        element.UpdateInfo();
        exportCellElement1 = this.CreatePdfExportCellElement((GridCellElement) element.ContentCell);
        exportCellElement1.Text = element.ContentCell.Text;
        GridExportUtils.ReleaseRowElement(this.RadGridViewToExport, this.rowProvider, (GridRowElement) element);
      }
      else
      {
        exportCellElement1 = new GridPdfExportCellElement();
        exportCellElement1.Text = viewGroupRowInfo.HeaderText;
        exportCellElement1.TextAlignment = ContentAlignment.MiddleLeft;
        exportCellElement1.BackColor = Color.LightGray;
        exportCellElement1.NumberOfColors = 1;
        string summary = viewGroupRowInfo.GetSummary();
        exportCellElement1.Text = viewGroupRowInfo.HeaderText;
        if (summary.Length > 0)
        {
          GridPdfExportCellElement exportCellElement2 = exportCellElement1;
          exportCellElement2.Text = exportCellElement2.Text + " | " + summary;
        }
      }
      exportCellElement1.RowType = gridViewRowInfo.GetType();
      exportCellElement1.RowIndex = 0;
      if (this.CellFormatting != null || this.CellPaint != null)
        exportCellElement1.RowIndex = gridViewRowInfo.Index;
      this.OnCellFormatting(new PdfExportCellFormattingEventArgs(exportCellElement1, gridViewRowInfo, (GridViewColumn) null));
      string[] text = this.ApplyLineBreaksToText((object) exportCellElement1.Text);
      this.DrawCell(exportCellElement1, text, (double) size.Height, (double) size.Width, (double) size.Width, false);
      this.editor.TranslatePosition(-(this.editor.OffsetX - (double) this.pageMargins.Left), (double) size.Height);
    }

    private double DrawCell(
      GridPdfExportCellElement cell,
      string[] value,
      double rowHeight,
      double cellWidth,
      double columnWidth,
      bool isNotCellFirstPage)
    {
      if (this.editor.OffsetX + cellWidth + (double) this.pageMargins.Right > (double) this.pageSize.Width && this.editor.OffsetX != (double) this.pageMargins.Left || isNotCellFirstPage)
      {
        bool flag = this.pdfExportRenderer.IsMatrixCurrentPageLastOnRow();
        double offsetY = this.editor.OffsetY;
        this.editor.TranslatePosition(-(this.editor.OffsetX - (double) this.pageMargins.Left), rowHeight);
        this.editor = this.pdfExportRenderer.GetRightPageEditor();
        this.pageNumber = this.pdfExportRenderer.GetCurrentPageNumber();
        if (flag)
        {
          this.DrawHeader();
          this.DrawFooter();
        }
        this.editor.CreateMatrixPosition();
        this.editor.TranslatePosition((double) this.pageMargins.Left + this.currentRowIndent * this.fitCoefficient, offsetY);
        if ((object) cell.RowType == (object) typeof (GridViewTableHeaderRowInfo))
        {
          this.editor.TranslatePosition(-this.currentRowIndent * this.fitCoefficient, 0.0);
          cellWidth += this.currentRowIndent * this.fitCoefficient;
        }
      }
      this.DrawCellFill(cell, rowHeight, cellWidth);
      double cellWidth1 = this.DrawCellBorders(cell, rowHeight, cellWidth, isNotCellFirstPage);
      if ((object) cell.ColumnType == (object) typeof (GridViewImageColumn) && (object) cell.RowType != (object) typeof (GridViewTableHeaderRowInfo))
        this.DrawImage(cell, rowHeight, cellWidth, columnWidth, isNotCellFirstPage);
      else
        this.DrawText(cell, value, rowHeight, cellWidth, columnWidth, isNotCellFirstPage);
      this.editor.SaveProperties();
      this.editor.SavePosition();
      this.OnCellPaint(new ExportCellPaintEventArgs(cell, this.editor, new RectangleF((float) this.editor.OffsetX, (float) this.editor.OffsetY, (float) (cellWidth - cellWidth1), (float) rowHeight)));
      this.editor.RestoreProperties();
      this.editor.RestorePosition();
      if (cellWidth1 > 0.0)
        cellWidth1 = this.DrawCell(cell, value, rowHeight, cellWidth1, columnWidth, true);
      return cellWidth1;
    }

    private void DrawImage(
      GridPdfExportCellElement cell,
      double rowHeight,
      double cellWidth,
      double columnWidth,
      bool isNotFirst)
    {
      if (cell.Image == null)
        return;
      double offsetX = this.editor.OffsetX;
      double offsetY = this.editor.OffsetY;
      this.editor.SavePosition();
      SizeF sizeF1 = new SizeF((float) (columnWidth - this.textOffset * 2.0), (float) (rowHeight - this.textOffset * 2.0));
      SizeF sizeF2 = SizeF.Empty;
      SizeF sizeF3 = new SizeF((float) this.textOffset, (float) this.textOffset);
      switch (cell.ImageLayout)
      {
        case ImageLayout.None:
          SizeF sizeF4 = new SizeF(Math.Min(sizeF1.Width, (float) cell.Image.Size.Width), Math.Min(sizeF1.Height, (float) cell.Image.Size.Height));
          RectangleF rectangleF = LayoutUtils.Align(sizeF4, (RectangleF) new Rectangle(Point.Empty, cell.Image.Size), cell.ImageAlignment);
          ContentAlignment alignment = cell.ImageAlignment;
          if (this.radGridView.RightToLeft == RightToLeft.Yes)
            alignment = GridExportUtils.ConvertToRightToLeftAlignment(alignment);
          sizeF3 = this.CalculateContentOffset(alignment, rowHeight, sizeF4, columnWidth);
          sizeF2.Width = rectangleF.Size.Width;
          sizeF2.Height = rectangleF.Size.Height;
          break;
        case ImageLayout.Center:
          PointF pointF = new PointF(Math.Max(0.0f, Math.Max(0.0f, (float) (((double) sizeF1.Width - (double) cell.Image.Width) / 2.0))), Math.Max(0.0f, Math.Max(0.0f, (float) (((double) sizeF1.Height - (double) cell.Image.Height) / 2.0))));
          sizeF3.Width += pointF.X;
          sizeF3.Height += pointF.Y;
          sizeF2.Width = Math.Min(sizeF1.Width, (float) cell.Image.Size.Width);
          sizeF2.Height = Math.Min(sizeF1.Height, (float) cell.Image.Size.Height);
          break;
        case ImageLayout.Stretch:
          sizeF3.Width = (float) this.textOffset;
          sizeF3.Height = (float) this.textOffset;
          sizeF2 = sizeF1;
          break;
        case ImageLayout.Zoom:
          if (cell.Image.Width != 0 && cell.Image.Height != 0)
          {
            float num1 = Math.Min(sizeF1.Width / (float) cell.Image.Width, sizeF1.Height / (float) cell.Image.Height);
            if ((double) num1 > 0.0)
            {
              float num2 = (float) Math.Round((double) cell.Image.Width * (double) num1);
              float num3 = (float) Math.Round((double) cell.Image.Height * (double) num1);
              sizeF3.Width += (sizeF1.Width - num2) / 2f;
              sizeF3.Height += (sizeF1.Height - num3) / 2f;
              sizeF2.Width = num2;
              sizeF2.Height = num3;
              break;
            }
            break;
          }
          break;
      }
      this.pdfExportRenderer.CreateBlock();
      using (Stream stream = (Stream) new MemoryStream())
      {
        cell.Image.Save(stream, ImageFormat.Png);
        this.pdfExportRenderer.InsertBlockImage(stream, (double) sizeF2.Width, (double) sizeF2.Height);
      }
      this.pdfExportRenderer.MeasureBlock();
      this.editor.TranslatePosition(0.0, (double) sizeF3.Height);
      this.pdfExportRenderer.SetBlockLeftIndent((double) sizeF3.Width);
      SizeF sizeF5 = this.pdfExportRenderer.MeasureBlock();
      if (isNotFirst)
      {
        this.editor.TranslatePosition(cellWidth - columnWidth, 0.0);
        this.editor.PushClipping(offsetX, offsetY, (double) this.pageSize.Width - (double) this.pageMargins.Horizontal, rowHeight);
      }
      else
      {
        double num = cellWidth - this.textOffset;
        if ((double) sizeF5.Width > num || (double) sizeF3.Height + (double) sizeF5.Height > rowHeight)
          this.editor.PushClipping(this.editor.OffsetX, this.editor.OffsetY, num >= 0.0 ? num : 0.0, rowHeight - (double) sizeF3.Height - this.textOffset);
        if (this.editor.OffsetX + cellWidth > (double) this.pageSize.Width - (double) this.pageMargins.Left)
          this.editor.PushClipping(this.editor.OffsetX, this.editor.OffsetY, Math.Max(0.0, (double) this.pageSize.Width - (double) this.pageMargins.Left - this.editor.OffsetX), rowHeight);
      }
      this.pdfExportRenderer.DrawBlock();
      this.editor.CreateMatrixPosition(offsetX, offsetY);
      this.editor.PopClipping();
    }

    private void DrawText(
      GridPdfExportCellElement cell,
      string[] value,
      double rowHeight,
      double cellWidth,
      double columnWidth,
      bool isNotFirst)
    {
      if (this.editor.OffsetX + (double) this.pageMargins.Left > (double) this.pageSize.Width || value == null || string.IsNullOrEmpty(value.ToString()))
        return;
      this.editor.SaveProperties();
      double offsetX = this.editor.OffsetX;
      double offsetY = this.editor.OffsetY;
      this.pdfExportRenderer.CreateBlock();
      if (this.exportVisualSettings)
      {
        this.editor.SetTextFontSize(((double) cell.Font.Size + 2.0) * this.Scale);
        this.editor.SetFillColor(cell.ForeColor);
        this.editor.TrySetFont(cell.Font.Name, cell.Font.Style);
      }
      else
      {
        if (cell.ForeColor.IsEmpty)
          cell.ForeColor = Color.Black;
        this.editor.SetFillColor(cell.ForeColor);
        this.editor.SetTextFontSize(12.0 * this.Scale);
      }
      this.pdfExportRenderer.ApplyEditorGraphicAndTextPropertiesToBlock();
      foreach (string str in value)
      {
        this.pdfExportRenderer.InsertBlockText(str ?? "");
        this.pdfExportRenderer.InsertBlockLineBreak();
      }
      SizeF size1 = new SizeF((float) (columnWidth - this.textOffset), (float) (rowHeight - this.textOffset));
      SizeF size2 = new SizeF(size1.Width - (float) this.textOffset, size1.Height - (float) this.textOffset);
      SizeF size3 = !cell.TextWrap ? this.pdfExportRenderer.MeasureBlock() : this.pdfExportRenderer.MeasureBlock(size2);
      ContentAlignment alignment = cell.TextAlignment;
      if (this.radGridView.RightToLeft == RightToLeft.Yes)
        alignment = GridExportUtils.ConvertToRightToLeftAlignment(alignment);
      SizeF contentOffset = this.CalculateContentOffset(alignment, rowHeight, size3, columnWidth);
      this.editor.TranslatePosition(0.0, (double) contentOffset.Height);
      if (this.cellSelfReferenceIndent != 0.0)
      {
        if (!alignment.ToString().Contains("Right"))
          contentOffset.Width += (float) this.cellSelfReferenceIndent / 2f;
        this.cellSelfReferenceIndent = 0.0;
      }
      this.pdfExportRenderer.SetBlockLeftIndent((double) contentOffset.Width);
      SizeF sizeF = !cell.TextWrap ? this.pdfExportRenderer.MeasureBlock() : this.pdfExportRenderer.MeasureBlock(size2);
      if (isNotFirst)
      {
        this.editor.TranslatePosition(cellWidth - columnWidth, 0.0);
        this.editor.PushClipping(offsetX, offsetY, (double) this.pageSize.Width - (double) this.pageMargins.Horizontal, rowHeight);
      }
      else
      {
        double num = cellWidth - this.textOffset;
        if ((double) sizeF.Width > num)
          this.editor.PushClipping(this.editor.OffsetX, this.editor.OffsetY, num > 0.0 ? num : 0.0, rowHeight);
        if (this.editor.OffsetX + cellWidth > (double) this.pageSize.Width - (double) this.pageMargins.Left)
          this.editor.PushClipping(this.editor.OffsetX, this.editor.OffsetY, Math.Max(0.0, (double) this.pageSize.Width - (double) this.pageMargins.Left - this.editor.OffsetX), rowHeight);
      }
      if (cell.TextWrap)
        this.pdfExportRenderer.DrawBlock(size1);
      else
        this.pdfExportRenderer.DrawBlock();
      this.editor.RestoreProperties();
      this.editor.CreateMatrixPosition(offsetX, offsetY);
      this.editor.PopClipping();
    }

    private void DrawCellFill(GridPdfExportCellElement cell, double rowHeight, double width)
    {
      if ((object) cell.RowType != (object) typeof (GridViewTableHeaderRowInfo) && (object) cell.RowType != (object) typeof (GridViewGroupRowInfo) && (!this.exportVisualSettings || this.editor.OffsetX + (double) this.pageMargins.Right > (double) this.pageSize.Width))
        return;
      PointF topLeft = new PointF(0.0f, 0.0f);
      PointF bottomRight = this.editor.OffsetX + width <= (double) this.pageSize.Width - (double) this.pageMargins.Right ? new PointF((float) width, (float) rowHeight) : new PointF((float) ((double) this.pageSize.Width - (double) this.pageMargins.Right - this.editor.OffsetX), (float) rowHeight);
      PointF pointF = new PointF(0.0f, (float) rowHeight);
      Color backColor = cell.BackColor;
      if (cell.NumberOfColors != 1 && cell.GradientStyle == GradientStyles.Linear)
        this.editor.SetLinearGradient(cell.NumberOfColors, new PointF((float) this.editor.OffsetX, (float) this.editor.OffsetY), new PointF((float) this.editor.OffsetX, (float) this.editor.OffsetY + pointF.Y), cell.BackColor, cell.BackColor2, cell.BackColor3, cell.BackColor4);
      else
        this.editor.SetFillColor(cell.BackColor);
      this.editor.DrawRectangle(topLeft, bottomRight);
    }

    private double DrawCellBorders(
      GridPdfExportCellElement cell,
      double rowHeight,
      double width,
      bool isNotFirst)
    {
      double num = 0.0;
      bool drawLeftBorder = !isNotFirst;
      bool drawRightBorder = !isNotFirst;
      PointF tLeft;
      PointF tRight;
      PointF bRight;
      PointF bLeft;
      if (!isNotFirst && this.editor.OffsetX + width < (double) this.pageSize.Width - (double) this.pageMargins.Right)
      {
        tLeft = new PointF(0.0f, 0.0f);
        tRight = new PointF((float) width, 0.0f);
        bRight = new PointF(tRight.X, (float) rowHeight);
        bLeft = new PointF(0.0f, (float) rowHeight);
      }
      else
      {
        if (this.editor.OffsetX >= (double) this.pageSize.Width - (double) this.pageMargins.Right)
          throw new ArgumentException("Internal bug in DrawCellBorders method.");
        tLeft = new PointF(0.0f, 0.0f);
        if (width > (double) this.pageSize.Width - (double) this.pageMargins.Right - this.editor.OffsetX)
        {
          tRight = new PointF((float) ((double) this.pageSize.Width - (double) this.pageMargins.Right - this.editor.OffsetX), 0.0f);
          drawRightBorder = false;
        }
        else
        {
          tRight = new PointF((float) width, 0.0f);
          drawRightBorder = true;
        }
        bRight = new PointF(tRight.X, (float) rowHeight);
        bLeft = new PointF(0.0f, (float) rowHeight);
        num = width - (double) tRight.X;
      }
      if (cell.BorderBoxStyle == BorderBoxStyle.FourBorders)
        this.DrawFourBorders(cell, ref tLeft, ref tRight, ref bRight, ref bLeft, drawLeftBorder, drawRightBorder);
      else if (cell.BorderBoxStyle == BorderBoxStyle.OuterInnerBorders)
        this.DrawOuterInnerBorders(cell, ref tLeft, ref tRight, ref bRight, ref bLeft, drawLeftBorder, drawRightBorder);
      else
        this.DrawSingleBorder(cell, ref tLeft, ref tRight, ref bRight, ref bLeft, drawLeftBorder, drawRightBorder);
      return num;
    }

    private void DrawSingleBorder(
      GridPdfExportCellElement cell,
      ref PointF tLeft,
      ref PointF tRight,
      ref PointF bRight,
      ref PointF bLeft,
      bool drawLeftBorder,
      bool drawRightBorder)
    {
      if (this.exportVisualSettings)
        this.editor.SetStrokeColor(cell.BorderColor);
      this.editor.DrawLine(tLeft, tRight);
      if (drawRightBorder)
        this.editor.DrawLine(tRight, bRight);
      this.editor.DrawLine(bRight, bLeft);
      if (!drawLeftBorder)
        return;
      this.editor.DrawLine(bLeft, tLeft);
    }

    private void DrawOuterInnerBorders(
      GridPdfExportCellElement cell,
      ref PointF tLeft,
      ref PointF tRight,
      ref PointF bRight,
      ref PointF bLeft,
      bool drawLeftBorder,
      bool drawRightBorder)
    {
      if (this.exportVisualSettings)
      {
        this.editor.SetStrokeColor(cell.BorderTopColor);
        this.editor.DrawLine(tLeft, tRight);
        this.editor.SetStrokeColor(cell.BorderTopShadowColor);
        this.editor.DrawLine(new PointF(tLeft.X + cell.BorderLeftWidth, tLeft.Y + cell.BorderTopWidth), new PointF(tRight.X - cell.BorderRightWidth, tRight.Y + cell.BorderTopWidth));
        if (drawRightBorder)
        {
          this.editor.SetStrokeColor(cell.BorderRightColor);
          this.editor.DrawLine(tRight, bRight);
          this.editor.SetStrokeColor(cell.BorderRightShadowColor);
          this.editor.DrawLine(new PointF(tRight.X - cell.BorderRightWidth, tRight.Y + cell.BorderTopWidth), new PointF(bRight.X - cell.BorderRightWidth, bRight.Y - cell.BorderBottomWidth));
        }
        this.editor.SetStrokeColor(cell.BorderBottomColor);
        this.editor.DrawLine(bRight, bLeft);
        this.editor.SetStrokeColor(cell.BorderBottomShadowColor);
        this.editor.DrawLine(new PointF(bRight.X - cell.BorderRightWidth, bRight.Y - cell.BorderBottomWidth), new PointF(bLeft.X + cell.BorderLeftWidth, bLeft.Y - cell.BorderBottomWidth));
        if (!drawLeftBorder)
          return;
        this.editor.SetStrokeColor(cell.BorderLeftColor);
        this.editor.DrawLine(bLeft, tLeft);
        this.editor.SetStrokeColor(cell.BorderLeftShadowColor);
        this.editor.DrawLine(new PointF(bLeft.X + cell.BorderLeftWidth, bLeft.Y - cell.BorderBottomWidth), new PointF(tLeft.X + cell.BorderLeftWidth, tLeft.Y + cell.BorderTopWidth));
      }
      else
      {
        this.editor.DrawLine(tLeft, tRight);
        if (drawRightBorder)
          this.editor.DrawLine(tRight, bRight);
        this.editor.DrawLine(bRight, bLeft);
        if (!drawLeftBorder)
          return;
        this.editor.DrawLine(bLeft, tLeft);
      }
    }

    private void DrawFourBorders(
      GridPdfExportCellElement cell,
      ref PointF tLeft,
      ref PointF tRight,
      ref PointF bRight,
      ref PointF bLeft,
      bool drawLeftBorder,
      bool drawRightBorder)
    {
      if (this.exportVisualSettings)
      {
        this.editor.SetStrokeColor(cell.BorderTopColor);
        this.editor.DrawLine(tLeft, tRight);
        if ((double) cell.BorderTopWidth > 0.0)
        {
          this.editor.SetStrokeColor(cell.BorderTopShadowColor);
          this.editor.DrawLine(new PointF(tLeft.X + cell.BorderLeftWidth, tLeft.Y + cell.BorderTopWidth), new PointF(tRight.X - cell.BorderRightWidth, tRight.Y + cell.BorderTopWidth));
        }
        if (drawRightBorder)
        {
          this.editor.SetStrokeColor(cell.BorderRightColor);
          this.editor.DrawLine(tRight, bRight);
          if ((double) cell.BorderRightWidth > 0.0)
          {
            this.editor.SetStrokeColor(cell.BorderRightShadowColor);
            this.editor.DrawLine(new PointF(tRight.X - cell.BorderRightWidth, tRight.Y + cell.BorderTopWidth), new PointF(bRight.X - cell.BorderRightWidth, bRight.Y - cell.BorderBottomWidth));
          }
        }
        this.editor.SetStrokeColor(cell.BorderBottomColor);
        this.editor.DrawLine(bRight, bLeft);
        if ((double) cell.BorderBottomWidth > 0.0)
        {
          this.editor.SetStrokeColor(cell.BorderBottomShadowColor);
          this.editor.DrawLine(new PointF(bRight.X - cell.BorderRightWidth, bRight.Y - cell.BorderBottomWidth), new PointF(bLeft.X + cell.BorderLeftWidth, bLeft.Y - cell.BorderBottomWidth));
        }
        if (!drawLeftBorder)
          return;
        this.editor.SetStrokeColor(cell.BorderLeftColor);
        this.editor.DrawLine(bLeft, tLeft);
        if ((double) cell.BorderLeftWidth <= 0.0)
          return;
        this.editor.SetStrokeColor(cell.BorderLeftShadowColor);
        this.editor.DrawLine(new PointF(bLeft.X + cell.BorderLeftWidth, bLeft.Y - cell.BorderBottomWidth), new PointF(tLeft.X + cell.BorderLeftWidth, tLeft.Y + cell.BorderTopWidth));
      }
      else
      {
        this.editor.DrawLine(tLeft, tRight);
        if (drawRightBorder)
          this.editor.DrawLine(tRight, bRight);
        this.editor.DrawLine(bRight, bLeft);
        if (!drawLeftBorder)
          return;
        this.editor.DrawLine(bLeft, tLeft);
      }
    }

    public void RunExport(Stream exportStream, IPdfExportRenderer exportRenderer)
    {
      if (this.radGridView.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.radGridView.Invoke((Delegate) new GridViewPdfExport.RunExportToStreamCallback(this.RunExportCall), (object) exportStream, (object) exportRenderer);
      }
      else
        this.RunExportCall(exportStream, exportRenderer);
    }

    public void RunExportAsync(Stream exportStream, IPdfExportRenderer exportRenderer)
    {
      this.pdfExportRenderer = exportRenderer;
      BackgroundWorker worker = this.GetWorker();
      this.ExportVisualSettings = false;
      if (worker.IsBusy)
        throw new Exception("There is an export operation that has not yet finished.");
      List<GridViewPdfExportRowInfo> exportRowInfos = (List<GridViewPdfExportRowInfo>) null;
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
        (object) new GridViewPdfExportDataSnapshot(string.Empty, exportRowInfos),
        (object) exportStream
      });
    }

    public void RunExport(string fileName, IPdfExportRenderer pdfExportRenderer)
    {
      if (this.radGridView.InvokeRequired)
      {
        this.applicationDoEvents = true;
        this.radGridView.Invoke((Delegate) new GridViewPdfExport.RunExportCallback(this.RunExportCall), (object) fileName, (object) pdfExportRenderer);
      }
      else
        this.RunExportCall(fileName, pdfExportRenderer);
    }

    public void RunExportAsync(string fileName, IPdfExportRenderer exportRenderer)
    {
      this.pdfExportRenderer = exportRenderer;
      BackgroundWorker worker = this.GetWorker();
      this.ExportVisualSettings = false;
      if (worker.IsBusy)
        throw new Exception("There is an export operation that has not yet finished.");
      List<GridViewPdfExportRowInfo> exportRowInfos = (List<GridViewPdfExportRowInfo>) null;
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
      this.GetWorker().RunWorkerAsync((object) new GridViewPdfExportDataSnapshot(fileName, exportRowInfos));
    }

    public void CancelExportAsync()
    {
      this.GetWorker().CancelAsync();
    }

    public void CancelExport()
    {
      this.cancellationPending = true;
    }

    private delegate void RunExportCallback(string fileName, IPdfExportRenderer pdfExportRenderer);

    private delegate void RunExportToStreamCallback(
      Stream exportStream,
      IPdfExportRenderer pdfExportRenderer);
  }
}
