// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSearchRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSearchRowInfo : GridViewSystemRowInfo, IGridViewEventListener
  {
    private int initialSearchResultsTreshold = 100;
    private int searchResultsGroupSize = 111;
    private int searchResultCurrentIndex = -1;
    private GridSearchResultCellCollection searchResult = new GridSearchResultCellCollection();
    private Hashtable cache = new Hashtable();
    private bool highlightResults = true;
    private bool automaticallySelectFirstResult = true;
    private CompareOptions compareOptions = CompareOptions.OrdinalIgnoreCase;
    private bool closeOnEscape = true;
    private bool showCloseButton = true;
    private bool showClearButton = true;
    private bool isSearchAsync = true;
    private int searchDelay = 100;
    public static bool Cancel;
    private BackgroundWorker worker;
    private bool isSearching;
    private bool isNavigating;
    private bool selectionChangedByUser;
    private string searchCriteria;
    private bool caseSensitive;
    private bool searchFromCurrentPosition;
    private int suspendedCount;
    private CultureInfo culture;

    public GridViewSearchRowInfo(GridViewInfo viewInfo)
      : base(viewInfo)
    {
      this.SuspendPropertyNotifications();
      this.PinPosition = PinnedRowPosition.Top;
      this.ViewInfo.PinnedRows.UpdateRow((GridViewRowInfo) this);
      this.ResumePropertyNotifications();
      if (viewInfo.ViewTemplate.MasterTemplate != null && viewInfo.ViewTemplate.MasterTemplate.SynchronizationService != null)
        viewInfo.ViewTemplate.MasterTemplate.SynchronizationService.AddListener((IGridViewEventListener) this);
      this.worker = this.CreateBackgroundWorker();
    }

    protected virtual BackgroundWorker CreateBackgroundWorker()
    {
      BackgroundWorker backgroundWorker = new BackgroundWorker();
      backgroundWorker.WorkerSupportsCancellation = true;
      backgroundWorker.WorkerReportsProgress = true;
      backgroundWorker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
      backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
      backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
      return backgroundWorker;
    }

    public CompareOptions CompareOptions
    {
      get
      {
        return this.compareOptions;
      }
      set
      {
        if (this.compareOptions == value)
          return;
        this.compareOptions = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (CompareOptions), (object) null, (object) value));
      }
    }

    public CultureInfo Culture
    {
      get
      {
        if (this.culture != null)
          return this.culture;
        return Thread.CurrentThread.CurrentCulture;
      }
      set
      {
        if (this.culture == value)
          return;
        this.culture = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (Culture), (object) null, (object) value));
      }
    }

    public override AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.None;
      }
    }

    public override System.Type RowElementType
    {
      get
      {
        return typeof (GridSearchRowElement);
      }
    }

    public int CurrentResultIndex
    {
      get
      {
        return this.searchResultCurrentIndex;
      }
    }

    public int CurrentSearchResultsCount
    {
      get
      {
        return this.searchResult.Count;
      }
    }

    public bool IsSearchSuspended
    {
      get
      {
        return this.suspendedCount > 0;
      }
    }

    public bool IsSearching
    {
      get
      {
        return this.isSearching;
      }
    }

    public int InitialSearchResultsTreshold
    {
      get
      {
        return this.initialSearchResultsTreshold;
      }
      set
      {
        this.initialSearchResultsTreshold = value;
      }
    }

    public int SearchResultsGroupSize
    {
      get
      {
        return this.searchResultsGroupSize;
      }
      set
      {
        this.searchResultsGroupSize = value;
      }
    }

    public bool CaseSensitive
    {
      get
      {
        return this.caseSensitive;
      }
      set
      {
        if (this.caseSensitive == value)
          return;
        this.caseSensitive = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (CaseSensitive), (object) null, (object) value));
      }
    }

    public bool SearchFromCurrentPosition
    {
      get
      {
        return this.searchFromCurrentPosition;
      }
      set
      {
        if (this.searchFromCurrentPosition == value)
          return;
        this.searchFromCurrentPosition = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (SearchFromCurrentPosition), (object) null, (object) value));
      }
    }

    public bool CloseOnEscape
    {
      get
      {
        return this.closeOnEscape;
      }
      set
      {
        this.closeOnEscape = value;
      }
    }

    [DefaultValue(true)]
    public bool ShowCloseButton
    {
      get
      {
        return this.showCloseButton;
      }
      set
      {
        if (this.showCloseButton == value)
          return;
        this.showCloseButton = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (ShowCloseButton), (object) null, (object) value));
      }
    }

    [DefaultValue(false)]
    public bool ShowClearButton
    {
      get
      {
        return this.showClearButton;
      }
      set
      {
        if (this.showClearButton == value)
          return;
        this.showClearButton = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (ShowClearButton), (object) null, (object) value));
      }
    }

    public bool HighlightResults
    {
      get
      {
        return this.highlightResults;
      }
      set
      {
        this.highlightResults = value;
      }
    }

    public bool AutomaticallySelectFirstResult
    {
      get
      {
        return this.automaticallySelectFirstResult;
      }
      set
      {
        this.automaticallySelectFirstResult = value;
      }
    }

    public string SearchCriteria
    {
      get
      {
        return this.searchCriteria;
      }
    }

    [DefaultValue(true)]
    public bool IsSearchAsync
    {
      get
      {
        return this.isSearchAsync;
      }
      set
      {
        this.isSearchAsync = value;
      }
    }

    public int SearchDelay
    {
      get
      {
        return this.searchDelay;
      }
      set
      {
        if (this.searchDelay == value)
          return;
        this.searchDelay = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (SearchDelay), (object) null, (object) value));
      }
    }

    protected override int CompareToSystemRowInfo(GridViewSystemRowInfo row)
    {
      if (row is GridViewTableHeaderRowInfo || row is GridViewFilteringRowInfo)
        return 1;
      if (row is GridViewNewRowInfo)
        return this.PinPosition == PinnedRowPosition.Top ? 1 : -1;
      if (row is GridViewSearchRowInfo)
        return 0;
      return base.CompareToSystemRowInfo(row);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (!(e.PropertyName == "RowPosition"))
        return;
      this.ViewTemplate.SearchRowPosition = this.RowPosition;
    }

    protected virtual bool MatchesSearchCriteria(
      string searchCriteria,
      GridViewRowInfo row,
      GridViewColumn col)
    {
      if (row == null || searchCriteria == null || searchCriteria.Length == 0)
        return false;
      GridViewGroupRowInfo viewGroupRowInfo = row as GridViewGroupRowInfo;
      if (col == null && viewGroupRowInfo == null)
        return false;
      string source = viewGroupRowInfo == null ? this.GetCellFormattedValue(row, col) : viewGroupRowInfo.HeaderText;
      if (source == null)
        return false;
      CompareOptions options = CompareOptions.Ordinal;
      if (!this.CaseSensitive)
        options = this.CompareOptions;
      return this.Culture.CompareInfo.IndexOf(source, searchCriteria, options) >= 0;
    }

    public virtual string GetCellFormattedValue(GridViewRowInfo row, GridViewColumn column)
    {
      GridViewDataColumn gridViewDataColumn = column as GridViewDataColumn;
      if (gridViewDataColumn == null || string.IsNullOrEmpty(gridViewDataColumn.FormatString) && !(gridViewDataColumn is GridViewComboBoxColumn))
        Convert.ToString(row[column]);
      GridViewComboBoxColumn viewComboBoxColumn1 = gridViewDataColumn as GridViewComboBoxColumn;
      object lookupValue = row[column];
      if (viewComboBoxColumn1 != null && viewComboBoxColumn1.HasLookupValue)
        lookupValue = viewComboBoxColumn1.GetLookupValue(lookupValue);
      GridDataConversionInfo dataConversionInfo = new GridDataConversionInfo(column as IDataConversionInfoProvider);
      GridViewComboBoxColumn viewComboBoxColumn2 = column as GridViewComboBoxColumn;
      if (viewComboBoxColumn2 != null)
      {
        dataConversionInfo.DataType = viewComboBoxColumn2.DisplayMemberDataType;
        dataConversionInfo.DataTypeConverter = TypeDescriptor.GetConverter(viewComboBoxColumn2.DisplayMemberDataType);
      }
      return RadDataConverter.Instance.Format(lookupValue, typeof (string), (IDataConversionInfoProvider) dataConversionInfo) as string;
    }

    public virtual void Search(string criteria)
    {
      if (this.IsSearchSuspended)
        return;
      this.searchCriteria = criteria;
      this.selectionChangedByUser = false;
      if (this.worker.IsBusy)
      {
        if (!this.worker.CancellationPending && !GridViewSearchRowInfo.Cancel)
        {
          this.worker.CancelAsync();
          GridViewSearchRowInfo.Cancel = true;
        }
        Application.DoEvents();
      }
      foreach (GridSearchResultCellInfo searchResultCellInfo in (List<GridSearchResultCellInfo>) this.searchResult)
        searchResultCellInfo.RowInfo.SearchCache.Clear();
      string cacheKey = this.GetCacheKey();
      if (this.cache.ContainsKey((object) cacheKey))
      {
        this.searchResult = new GridSearchResultCellCollection((IEnumerable<GridSearchResultCellInfo>) (this.cache[(object) cacheKey] as GridSearchResultCellCollection));
        this.searchResultCurrentIndex = -1;
        foreach (GridSearchResultCellInfo searchResultCellInfo in (List<GridSearchResultCellInfo>) this.searchResult)
        {
          GridViewGroupRowInfo rowInfo = searchResultCellInfo.RowInfo as GridViewGroupRowInfo;
          GridViewColumn gridViewColumn = searchResultCellInfo.ColumnInfo;
          if (rowInfo != null)
            gridViewColumn = this.GetGroupedColumnInfo(rowInfo);
          GridViewDataColumn gridViewDataColumn = gridViewColumn as GridViewDataColumn;
          if (gridViewDataColumn == null || gridViewDataColumn.AllowSearching)
          {
            if (searchResultCellInfo.RowInfo.SearchCache.ContainsKey((object) gridViewColumn))
              searchResultCellInfo.RowInfo.SearchCache[(object) gridViewColumn] = (object) criteria;
            else
              searchResultCellInfo.RowInfo.SearchCache.Add((object) gridViewColumn, (object) criteria);
          }
        }
        if (this.searchResult.Count > 0 && this.AutomaticallySelectFirstResult)
          this.SelectNextSearchResult();
        this.OnSearchProgressChanged(new SearchProgressChangedEventArgs(this.searchCriteria, (GridSearchResultCellInfo) null, this.searchResult, true));
      }
      else
      {
        while (this.worker.IsBusy)
          Application.DoEvents();
        this.searchResult.Clear();
        this.searchResultCurrentIndex = -1;
        GridViewSearchRowInfo.Cancel = false;
        if (!string.IsNullOrEmpty(criteria))
        {
          this.isSearching = true;
          if (this.IsSearchAsync)
            this.worker.RunWorkerAsync((object) criteria);
          else
            this.TraverseRows((DoWorkEventArgs) null);
        }
        else
          this.OnSearchProgressChanged(new SearchProgressChangedEventArgs(this.searchCriteria, (GridSearchResultCellInfo) null, this.searchResult, true));
      }
    }

    protected virtual void TraverseRows(DoWorkEventArgs e = null)
    {
      if (e != null)
        e.Result = (object) false;
      bool flag = false;
      GridViewRowInfo currentRow = this.ViewTemplate.MasterTemplate.Owner.CurrentRow;
      GridSearchResultCellCollection resultCells = (GridSearchResultCellCollection) null;
      IEnumerator enumerator = (IEnumerator) this.CreateTraverser();
      if (this.ViewTemplate.MasterTemplate.EnablePaging)
        enumerator = this.CreatePagedTraverser(this.ViewTemplate.MasterTemplate.EnableGrouping && this.ViewTemplate.MasterTemplate.GroupDescriptors.Count > 0);
      int traverserRowIndex = -1;
      while (enumerator.MoveNext())
      {
        ++traverserRowIndex;
        if (GridViewSearchRowInfo.Cancel && !this.IsSearchAsync)
        {
          GridViewSearchRowInfo.Cancel = false;
          e.Result = (object) true;
          return;
        }
        if (!(enumerator.Current is GridViewTableHeaderRowInfo) && !(enumerator.Current is GridViewNewRowInfo) && (!(enumerator.Current is GridViewFilteringRowInfo) && !(enumerator.Current is GridViewSearchRowInfo)))
        {
          if (this.SearchFromCurrentPosition)
          {
            if (enumerator.Current == currentRow)
              flag = true;
            if (currentRow != null && !flag)
              continue;
          }
          GridViewGroupRowInfo current1 = enumerator.Current as GridViewGroupRowInfo;
          if (current1 != null)
          {
            if (GridViewSearchRowInfo.Cancel && !this.IsSearchAsync)
            {
              GridViewSearchRowInfo.Cancel = false;
              e.Result = (object) true;
              return;
            }
            if (this.MatchesSearchCriteria(this.searchCriteria, (GridViewRowInfo) current1, (GridViewColumn) null))
            {
              GridViewColumn groupedColumnInfo = this.GetGroupedColumnInfo(current1);
              if (current1.SearchCache.ContainsKey((object) groupedColumnInfo))
                current1.SearchCache[(object) groupedColumnInfo] = (object) this.searchCriteria;
              else
                current1.SearchCache.Add((object) groupedColumnInfo, (object) this.searchCriteria);
              GridSearchResultCellInfo resultCell = new GridSearchResultCellInfo((GridViewRowInfo) current1, (GridViewColumn) null, traverserRowIndex, 0);
              if (this.searchResult.Count < this.InitialSearchResultsTreshold)
              {
                this.ReportSearchProgress(0, resultCell, (GridSearchResultCellCollection) null);
              }
              else
              {
                if (resultCells == null)
                  resultCells = new GridSearchResultCellCollection(this.SearchResultsGroupSize);
                resultCells.Add(resultCell);
                if (resultCells.Count == this.SearchResultsGroupSize)
                {
                  this.ReportSearchProgress(0, (GridSearchResultCellInfo) null, resultCells);
                  resultCells = (GridSearchResultCellCollection) null;
                }
              }
            }
          }
          else
          {
            GridViewRowInfo current2 = enumerator.Current as GridViewRowInfo;
            int traverserColumnIndex = -1;
            for (int index = 0; index < current2.ViewTemplate.Columns.Count; ++index)
            {
              GridViewColumn column = (GridViewColumn) current2.ViewTemplate.Columns[index];
              ++traverserColumnIndex;
              if (GridViewSearchRowInfo.Cancel && !this.IsSearchAsync)
              {
                GridViewSearchRowInfo.Cancel = false;
                e.Result = (object) true;
                return;
              }
              GridViewDataColumn gridViewDataColumn = column as GridViewDataColumn;
              if ((gridViewDataColumn == null || gridViewDataColumn.AllowSearching) && (column.IsVisible && !column.IsGrouped) && (!(column is GridViewImageColumn) && this.MatchesSearchCriteria(this.searchCriteria, current2, column)))
              {
                if (current2.SearchCache.ContainsKey((object) column))
                  current2.SearchCache[(object) column] = (object) this.searchCriteria;
                else
                  current2.SearchCache.Add((object) column, (object) this.searchCriteria);
                GridSearchResultCellInfo resultCell = new GridSearchResultCellInfo(current2, column, traverserRowIndex, traverserColumnIndex);
                if (this.searchResult.Count < this.InitialSearchResultsTreshold)
                {
                  this.ReportSearchProgress(0, resultCell, (GridSearchResultCellCollection) null);
                }
                else
                {
                  if (resultCells == null)
                    resultCells = new GridSearchResultCellCollection();
                  resultCells.Add(resultCell);
                  if (resultCells.Count == this.SearchResultsGroupSize)
                  {
                    this.ReportSearchProgress(0, (GridSearchResultCellInfo) null, resultCells);
                    resultCells = (GridSearchResultCellCollection) null;
                  }
                }
              }
            }
          }
        }
      }
      if (resultCells != null && resultCells.Count > 0)
        this.ReportSearchProgress(100, (GridSearchResultCellInfo) null, resultCells);
      else
        this.ReportSearchProgress(100, (GridSearchResultCellInfo) null, (GridSearchResultCellCollection) null);
    }

    protected virtual void ReportSearchProgress(
      int percent,
      GridSearchResultCellInfo resultCell,
      GridSearchResultCellCollection resultCells)
    {
      if (resultCell != null)
      {
        this.searchResult.Add(resultCell);
        if (this.searchResult.Count == 1 && this.AutomaticallySelectFirstResult)
          this.SelectNextSearchResult();
      }
      else if (resultCells != null)
      {
        foreach (GridSearchResultCellInfo resultCell1 in (List<GridSearchResultCellInfo>) resultCells)
          this.searchResult.Add(resultCell1);
      }
      this.OnSearchProgressChanged(new SearchProgressChangedEventArgs(this.searchCriteria, resultCell, resultCells, percent == 100));
    }

    private GridViewColumn GetGroupedColumnInfo(GridViewGroupRowInfo groupRow)
    {
      return groupRow.Group.GroupDescriptor != null ? (GridViewColumn) groupRow.ViewTemplate.Columns[groupRow.Group.GroupDescriptor.GroupNames[0].PropertyName] : (GridViewColumn) groupRow.ViewTemplate.Columns[groupRow.ViewTemplate.GroupDescriptors[groupRow.GroupLevel].GroupNames[0].PropertyName];
    }

    public virtual void SelectNextSearchResult()
    {
      if (this.searchResult.Count == 0 || this.ViewTemplate.MasterTemplate.CurrentRow is GridViewSystemRowInfo && this.ViewTemplate.MasterTemplate.Owner.EditorManager.IsInEditMode)
        return;
      if (this.selectionChangedByUser)
      {
        this.selectionChangedByUser = false;
        if (this.ViewTemplate.MasterTemplate.Owner.CurrentRow == null && this.ViewTemplate.MasterTemplate.Owner.CurrentColumn == null)
        {
          this.searchResultCurrentIndex = -1;
        }
        else
        {
          int num = this.searchResult.BinarySearch(new GridSearchResultCellInfo((GridViewRowInfo) null, (GridViewColumn) null, this.GetCurrentCellTraverserRowIndex(), this.GetCurrentCellTraverserColumnIndex()), (IComparer<GridSearchResultCellInfo>) new GridViewSearchRowInfo.TraverserIndexComparer());
          if (num < 0)
            num = ~num;
          this.searchResultCurrentIndex = num - 1;
        }
      }
      this.isNavigating = true;
      if ((!this.isSearching || !this.IsSearchAsync) && this.searchResultCurrentIndex == this.searchResult.Count - 1)
      {
        this.searchResultCurrentIndex = 0;
        this.SetCurrent(this.searchResult[this.searchResultCurrentIndex], true);
      }
      else if (this.searchResultCurrentIndex < this.searchResult.Count - 1)
      {
        ++this.searchResultCurrentIndex;
        this.SetCurrent(this.searchResult[this.searchResultCurrentIndex], true);
      }
      this.isNavigating = false;
    }

    public virtual void SelectPreviousSearchResult()
    {
      if (this.searchResult.Count == 0)
        return;
      if (this.selectionChangedByUser)
      {
        this.selectionChangedByUser = false;
        if (this.ViewTemplate.MasterTemplate.Owner.CurrentRow == null && this.ViewTemplate.MasterTemplate.Owner.CurrentColumn == null)
        {
          this.searchResultCurrentIndex = 0;
        }
        else
        {
          int num = this.searchResult.BinarySearch(new GridSearchResultCellInfo((GridViewRowInfo) null, (GridViewColumn) null, this.GetCurrentCellTraverserRowIndex(), this.GetCurrentCellTraverserColumnIndex()), (IComparer<GridSearchResultCellInfo>) new GridViewSearchRowInfo.TraverserIndexComparer());
          if (num < 0)
            num = ~num;
          this.searchResultCurrentIndex = num;
        }
      }
      this.isNavigating = true;
      if ((!this.isSearching || !this.IsSearchAsync) && this.searchResultCurrentIndex == 0)
      {
        this.searchResultCurrentIndex = this.searchResult.Count - 1;
        this.SetCurrent(this.searchResult[this.searchResultCurrentIndex], true);
      }
      else if (this.searchResultCurrentIndex > 0)
      {
        --this.searchResultCurrentIndex;
        this.SetCurrent(this.searchResult[this.searchResultCurrentIndex], true);
      }
      this.isNavigating = false;
    }

    protected virtual void SetCurrent(GridSearchResultCellInfo cell, bool checkInvokeRequired)
    {
      if (this.ViewTemplate.MasterTemplate.Owner.InvokeRequired)
        this.ViewTemplate.MasterTemplate.Owner.Invoke((Delegate) new GridViewSearchRowInfo.SetCurrentHandler(this.SetCurrent), (object) cell);
      else
        this.SetCurrent(cell);
    }

    protected virtual void SetCurrent(GridSearchResultCellInfo cell)
    {
      this.EnsurePageVisible(cell.RowInfo);
      this.ViewTemplate.MasterTemplate.Owner.CurrentRow = cell.RowInfo;
      this.ViewTemplate.MasterTemplate.Owner.CurrentColumn = cell.ColumnInfo;
    }

    protected virtual bool EnsurePageVisible(GridViewRowInfo row)
    {
      if (!this.ViewTemplate.MasterTemplate.EnablePaging)
        return false;
      if (this.ViewTemplate.MasterTemplate.EnablePaging && this.ViewTemplate.MasterTemplate.EnableGrouping && this.ViewTemplate.MasterTemplate.GroupDescriptors.Count > 0)
      {
        int num = -1;
        IEnumerator pagedTraverser = this.CreatePagedTraverser(true);
        while (pagedTraverser.MoveNext())
        {
          if (pagedTraverser.Current is GridViewGroupRowInfo)
            ++num;
          if (pagedTraverser.Current == row)
            break;
        }
        if (num >= 0)
          return this.ViewTemplate.MasterTemplate.MoveToPage(num / this.ViewTemplate.MasterTemplate.PageSize);
      }
      else
      {
        for (int pageIndex = 0; pageIndex < this.ViewTemplate.MasterTemplate.TotalPages; ++pageIndex)
        {
          foreach (object obj in (IEnumerable<GridViewRowInfo>) ((RadDataView<GridViewRowInfo>) this.ViewTemplate.MasterTemplate.DataView).Indexer.GetItemsOnPage(pageIndex))
          {
            if (obj.Equals((object) row))
              return this.ViewTemplate.MasterTemplate.MoveToPage(pageIndex);
          }
        }
      }
      return false;
    }

    protected virtual int GetCurrentCellTraverserRowIndex()
    {
      GridTraverser traverser = this.CreateTraverser();
      int num = -1;
      while (traverser.MoveNext())
      {
        ++num;
        if (traverser.Current == this.ViewTemplate.MasterTemplate.Owner.CurrentRow)
          return num;
      }
      return -1;
    }

    protected virtual int GetCurrentCellTraverserColumnIndex()
    {
      if (this.ViewTemplate.MasterTemplate.Owner.CurrentRow == null)
        return -1;
      this.CreateTraverser();
      int num = -1;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.MasterTemplate.Owner.CurrentRow.ViewTemplate.Columns)
      {
        ++num;
        if (column == this.ViewTemplate.MasterTemplate.Owner.CurrentColumn)
          return num;
      }
      return -1;
    }

    protected virtual GridTraverser CreateTraverser()
    {
      PrintGridTraverser printGridTraverser = new PrintGridTraverser(this.ViewTemplate.MasterViewInfo);
      printGridTraverser.TraversalMode = GridTraverser.TraversalModes.AllRows;
      printGridTraverser.ProcessHiddenRows = false;
      printGridTraverser.ProcessHierarchy = true;
      return (GridTraverser) printGridTraverser;
    }

    protected virtual IEnumerator CreatePagedTraverser(bool grouped)
    {
      if (grouped)
        return (IEnumerator) new PagedGroupedTraverser(((RadDataView<GridViewRowInfo>) this.ViewTemplate.MasterTemplate.DataView).GroupBuilder.Groups.GroupList);
      return (IEnumerator) ((RadDataView<GridViewRowInfo>) this.ViewTemplate.MasterTemplate.DataView).Indexer.Items.GetEnumerator();
    }

    public void SuspendSearch()
    {
      ++this.suspendedCount;
    }

    public void ResumeSearch()
    {
      this.ResumeSearch(true);
    }

    public void ResumeSearch(bool performSearch)
    {
      --this.suspendedCount;
      if (this.suspendedCount > 0 || !performSearch)
        return;
      this.Search(this.SearchCriteria);
    }

    public event SearchProgressChangedEventHandler SearchProgressChanged;

    protected internal virtual void OnSearchProgressChanged(SearchProgressChangedEventArgs e)
    {
      if (this.SearchProgressChanged == null)
        return;
      this.SearchProgressChanged((object) this, e);
    }

    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.TraverseRows(e);
    }

    private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.ReportSearchProgress(e.ProgressPercentage, e.UserState as GridSearchResultCellInfo, e.UserState as GridSearchResultCellCollection);
    }

    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      bool result = (bool) e.Result;
      if (!result && !string.IsNullOrEmpty(this.searchCriteria))
      {
        string cacheKey = this.GetCacheKey();
        if (!this.cache.ContainsKey((object) cacheKey))
          this.cache.Add((object) cacheKey, (object) new GridSearchResultCellCollection((IEnumerable<GridSearchResultCellInfo>) this.searchResult));
      }
      this.isSearching = false;
      if (result || string.IsNullOrEmpty(this.searchCriteria))
        return;
      this.OnSearchProgressChanged(new SearchProgressChangedEventArgs(this.searchCriteria, (GridSearchResultCellInfo) null, this.searchResult, true));
    }

    private string GetCacheKey()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(this.searchCriteria);
      sb.Append(this.CaseSensitive.ToString());
      sb.Append(this.SearchFromCurrentPosition.ToString());
      this.AppendTemplateCacheKey(sb, (GridViewTemplate) this.ViewTemplate.MasterTemplate, "MasterTemplate");
      return sb.ToString();
    }

    private void AppendTemplateCacheKey(
      StringBuilder sb,
      GridViewTemplate template,
      string templateName)
    {
      sb.Append(string.Format(";{0}", (object) templateName));
      foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) template.Columns)
      {
        if (column.AllowSearching)
          sb.Append(string.Format("{0},", (object) column.Index));
      }
      --sb.Length;
      foreach (GridViewTemplate template1 in (Collection<GridViewTemplate>) template.Templates)
        this.AppendTemplateCacheKey(sb, template1, template1.Caption);
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.Data;
      }
    }

    EventListenerPriority IGridViewEventListener.Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    GridEventProcessMode IGridViewEventListener.DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.PostProcess;
      }
    }

    GridViewEventResult IGridViewEventListener.PreProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.ProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.PostProcessEvent(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.ViewChanged)
      {
        DataViewChangedEventArgs changedEventArgs = eventData.Arguments[0] as DataViewChangedEventArgs;
        if (changedEventArgs.Action == ViewChangedAction.Add || changedEventArgs.Action == ViewChangedAction.Remove || (changedEventArgs.Action == ViewChangedAction.FilteringChanged || changedEventArgs.Action == ViewChangedAction.GroupingChanged) || (changedEventArgs.Action == ViewChangedAction.SortingChanged || changedEventArgs.Action == ViewChangedAction.ItemChanged || changedEventArgs.Action == ViewChangedAction.Reset))
        {
          this.cache.Clear();
          this.Search(this.searchCriteria);
        }
        else if ((changedEventArgs.Action == ViewChangedAction.CurrentCellChanged || changedEventArgs.Action == ViewChangedAction.CurrentColumnChanged || changedEventArgs.Action == ViewChangedAction.CurrentRowChanged) && !this.isNavigating)
          this.selectionChangedByUser = true;
      }
      else if (eventData.Info.Id == KnownEvents.PropertyChanged && eventData.Sender is GridViewDataColumn && (eventData.Arguments[0] as RadPropertyChangedEventArgs).Property.Name == "IsVisible")
      {
        this.cache.Clear();
        this.Search(this.searchCriteria);
      }
      return (GridViewEventResult) null;
    }

    bool IGridViewEventListener.AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }

    protected delegate void SetCurrentHandler(GridSearchResultCellInfo cell);

    public class TraverserIndexComparer : IComparer<GridSearchResultCellInfo>
    {
      public int Compare(GridSearchResultCellInfo x, GridSearchResultCellInfo y)
      {
        int num = x.TraverserRowIndex.CompareTo(y.TraverserRowIndex);
        if (num == 0)
          num = x.TraverserColumnIndex.CompareTo(y.TraverserColumnIndex);
        return num;
      }
    }
  }
}
