// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridTableElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class VirtualGridTableElement : ScrollViewElement<VirtualRowsContainerElement>
  {
    public static RadProperty HeaderRowHeightProperty = RadProperty.Register(nameof (HeaderRowHeight), typeof (int), typeof (VirtualGridTableElement), new RadPropertyMetadata((object) 30));
    public static RadProperty FilterRowHeightProperty = RadProperty.Register(nameof (FilterRowHeight), typeof (int), typeof (VirtualGridTableElement), new RadPropertyMetadata((object) 26));
    public static RadProperty NewRowHeightProperty = RadProperty.Register(nameof (NewRowHeight), typeof (int), typeof (VirtualGridTableElement), new RadPropertyMetadata((object) 24));
    public static RadProperty RowHeightProperty = RadProperty.Register(nameof (RowHeight), typeof (int), typeof (VirtualGridTableElement), new RadPropertyMetadata((object) 24));
    public static RadProperty AlternatingRowColorProperty = RadProperty.Register(nameof (AlternatingRowColor), typeof (Color), typeof (VirtualGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Lavender, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EnableHotTrackingProperty = RadProperty.Register(nameof (EnableHotTracking), typeof (bool), typeof (VirtualGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.None));
    public static RadProperty IndentColumnWidthProperty = RadProperty.Register(nameof (IndentColumnWidth), typeof (int), typeof (VirtualGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty RowWaitingImageProperty = RadProperty.Register(nameof (RowWaitingImage), typeof (Image), typeof (VirtualGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Resources.preloader, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RowErrorImageProperty = RadProperty.Register(nameof (RowErrorImage), typeof (Image), typeof (VirtualGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Resources.validation_icon, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CurrentRowHeaderImageProperty = RadProperty.Register(nameof (CurrentRowHeaderImage), typeof (Image), typeof (VirtualGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EditRowHeaderImageProperty = RadProperty.Register(nameof (EditRowHeaderImage), typeof (Image), typeof (VirtualGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private RadVirtualGridElement gridElement;
    private VirtualGridViewInfo viewInfo;
    private VirtualGridItemScroller rowScroller;
    private VirtualGridItemScroller columnScroller;
    private float parentRowOffset;
    private VirtualGridPagingPanelElement pagingPanel;
    private VirtualGridWaitingElement waitingElement;
    private BaseVirtualGridColumnLayout columnLayout;

    public VirtualGridTableElement(RadVirtualGridElement gridElement, VirtualGridViewInfo viewInfo)
    {
      this.gridElement = gridElement;
      this.viewInfo = viewInfo;
      this.viewInfo.PropertyChanged += new PropertyChangedEventHandler(this.OnViewInfoPropertyChanged);
      this.viewInfo.BindProperties(this);
      this.UpdateOnViewInfoChanged();
      this.ColumnLayout = (BaseVirtualGridColumnLayout) new StandardVirtualGridColumnLayout();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawFill = true;
      this.BackColor = Color.White;
      this.GradientStyle = GradientStyles.Solid;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.SetScrollState();
    }

    [Description("Gets or sets an image for the indent cell of a row that is indicating the row is busy.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RadPropertyDefaultValue("RowWaitingImage", typeof (VirtualGridTableElement))]
    public Image RowWaitingImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridTableElement.RowWaitingImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.RowWaitingImageProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("RowErrorImage", typeof (VirtualGridTableElement))]
    [Description("Gets or sets an image for the indent cell of a row containing a data error.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image RowErrorImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridTableElement.RowErrorImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.RowErrorImageProperty, (object) value);
      }
    }

    [Description("Gets or sets an image for the indent cell of the current row.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RadPropertyDefaultValue("CurrentRowHeaderImage", typeof (VirtualGridTableElement))]
    public virtual Image CurrentRowHeaderImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridTableElement.CurrentRowHeaderImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.CurrentRowHeaderImageProperty, (object) value);
      }
    }

    [Description("Gets or sets an image for the indent cell of a row that is currently in edit mode.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RadPropertyDefaultValue("EditRowHeaderImage", typeof (VirtualGridTableElement))]
    public virtual Image EditRowHeaderImage
    {
      get
      {
        return (Image) this.GetValue(VirtualGridTableElement.EditRowHeaderImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.EditRowHeaderImageProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    [RadPropertyDefaultValue("AlternatingRowColor", typeof (VirtualGridTableElement))]
    public virtual Color AlternatingRowColor
    {
      get
      {
        return (Color) this.GetValue(VirtualGridTableElement.AlternatingRowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.AlternatingRowColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("EnableHotTracking", typeof (VirtualGridTableElement))]
    [Description(" Gets or sets a value indicating whether there is a visual indication for the row currently under the mouse.")]
    [VsbBrowsable(true)]
    public bool EnableHotTracking
    {
      get
      {
        return (bool) this.GetValue(VirtualGridTableElement.EnableHotTrackingProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.EnableHotTrackingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("FilterRowHeight", typeof (VirtualGridTableElement))]
    [VsbBrowsable(true)]
    public int FilterRowHeight
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(VirtualGridTableElement.FilterRowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.FilterRowHeightProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    [RadPropertyDefaultValue("NewRowHeight", typeof (VirtualGridTableElement))]
    public int NewRowHeight
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(VirtualGridTableElement.NewRowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.NewRowHeightProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    [RadPropertyDefaultValue("HeaderRowHeight", typeof (VirtualGridTableElement))]
    public int HeaderRowHeight
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(VirtualGridTableElement.HeaderRowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.HeaderRowHeightProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    [RadPropertyDefaultValue("RowHeight", typeof (VirtualGridTableElement))]
    public int RowHeight
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(VirtualGridTableElement.RowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.RowHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("IndentColumnWidth", typeof (VirtualGridTableElement))]
    [VsbBrowsable(true)]
    public int IndentColumnWidth
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(VirtualGridTableElement.IndentColumnWidthProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridTableElement.IndentColumnWidthProperty, (object) value);
      }
    }

    public VirtualGridPagingPanelElement PagingPanelElement
    {
      get
      {
        return this.pagingPanel;
      }
    }

    public VirtualGridWaitingElement WaitingElement
    {
      get
      {
        return this.waitingElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VirtualGridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
      set
      {
        if (this.viewInfo == value)
          return;
        if (this.viewInfo != null)
        {
          this.viewInfo.PropertyChanged -= new PropertyChangedEventHandler(this.OnViewInfoPropertyChanged);
          this.viewInfo.UnbindProperties(this);
        }
        this.viewInfo = value;
        if (this.viewInfo != null)
        {
          this.viewInfo.PropertyChanged += new PropertyChangedEventHandler(this.OnViewInfoPropertyChanged);
          this.viewInfo.BindProperties(this);
        }
        this.UpdateOnViewInfoChanged();
      }
    }

    public VirtualGridTableViewState RowsViewState
    {
      get
      {
        return this.ViewInfo.RowsViewState;
      }
    }

    public VirtualGridTableViewState ColumnsViewState
    {
      get
      {
        return this.ViewInfo.ColumnsViewState;
      }
    }

    public int RowCount
    {
      get
      {
        return this.ViewInfo.RowCount;
      }
      set
      {
        this.ViewInfo.RowCount = value;
      }
    }

    public int ColumnCount
    {
      get
      {
        return this.ViewInfo.ColumnCount;
      }
      set
      {
        this.ViewInfo.ColumnCount = value;
      }
    }

    public int ColumnWidth
    {
      get
      {
        return (int) Math.Round((double) this.ViewInfo.ColumnWidth * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        this.ViewInfo.ColumnWidth = value;
      }
    }

    public int RowSpacing
    {
      get
      {
        return this.ViewInfo.RowSpacing;
      }
      set
      {
        this.ViewInfo.RowSpacing = value;
      }
    }

    public int CellSpacing
    {
      get
      {
        return this.ViewInfo.CellSpacing;
      }
      set
      {
        this.ViewInfo.CellSpacing = value;
      }
    }

    public BaseVirtualGridColumnLayout ColumnLayout
    {
      get
      {
        return this.columnLayout;
      }
      set
      {
        this.columnLayout = value;
        this.columnLayout.Initialize(this);
      }
    }

    public VirtualGridItemScroller RowScroller
    {
      get
      {
        return this.rowScroller;
      }
    }

    public VirtualGridItemScroller ColumnScroller
    {
      get
      {
        return this.columnScroller;
      }
    }

    public RadVirtualGridElement GridElement
    {
      get
      {
        return this.gridElement;
      }
    }

    internal float ParentRowOffset
    {
      get
      {
        return this.parentRowOffset;
      }
      set
      {
        this.parentRowOffset = value;
      }
    }

    public void BeginUpdate()
    {
      this.RowsViewState.BeginUpdate();
      this.ColumnsViewState.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.RowsViewState.EndUpdate();
      this.ColumnsViewState.EndUpdate();
      this.RowScroller.UpdateScrollRange();
      this.ColumnScroller.UpdateScrollRange();
    }

    public void SetRowHeight(int rowIndex, int height)
    {
      this.ViewInfo.SetRowHeight(rowIndex, height);
    }

    public void SetRowHeight(int height, params int[] rowIndices)
    {
      this.RowsViewState.BeginUpdate();
      foreach (int rowIndex in rowIndices)
        this.ViewInfo.SetRowHeight(rowIndex, height);
      this.RowsViewState.EndUpdate();
    }

    public int GetRowHeight(int rowIndex)
    {
      switch (rowIndex)
      {
        case -3:
          return this.FilterRowHeight;
        case -2:
          return this.NewRowHeight;
        case -1:
          return this.HeaderRowHeight;
        default:
          return this.ViewInfo.GetRowHeight(rowIndex);
      }
    }

    public int GetColumnWidth(int columnIndex)
    {
      if (columnIndex == -1)
        return this.IndentColumnWidth;
      return this.ViewInfo.GetColumnWidth(columnIndex);
    }

    public void SetColumnWidth(int columnIndex, int width)
    {
      this.ViewInfo.SetColumnWidth(columnIndex, width);
    }

    public void SetColumnWidth(int width, params int[] columnIndices)
    {
      this.ColumnsViewState.BeginUpdate();
      foreach (int columnIndex in columnIndices)
        this.ViewInfo.SetColumnWidth(columnIndex, width);
      this.ColumnsViewState.EndUpdate();
    }

    public void SetRowPinPosition(int rowIndex, PinnedRowPosition pinPosition)
    {
      this.ViewInfo.SetRowPinPosition(rowIndex, pinPosition);
    }

    public void SetColumnPinPosition(int columnIndex, PinnedColumnPosition pinPosition)
    {
      this.ViewInfo.SetColumnPinPosition(columnIndex, pinPosition);
    }

    public bool IsRowPinned(int rowIndex)
    {
      return this.RowsViewState.IsPinned(rowIndex);
    }

    public bool IsColumnPinned(int columnIndex)
    {
      return this.ColumnsViewState.IsPinned(columnIndex);
    }

    public bool ExpandRow(int rowIndex)
    {
      return this.ViewInfo.ExpandRow(rowIndex);
    }

    public bool CollapseRow(int rowIndex)
    {
      return this.ViewInfo.CollapseRow(rowIndex);
    }

    public bool IsRowExpanded(int rowIndex)
    {
      return this.ViewInfo.IsRowExpanded(rowIndex);
    }

    public VirtualGridViewInfo GetChildViewInfo(int rowIndex)
    {
      return this.ViewInfo.GetChildViewInfo(rowIndex);
    }

    public void ScrollTo(int delta)
    {
      this.ScrollTo(delta, this.RowScroller.Scrollbar);
    }

    public void ScrollTo(int delta, RadScrollBarElement scrollBar)
    {
      int num = scrollBar.Value - delta * scrollBar.SmallChange;
      if (num > scrollBar.Maximum - scrollBar.LargeChange + 1)
        num = scrollBar.Maximum - scrollBar.LargeChange + 1;
      if (num < scrollBar.Minimum)
        num = 0;
      else if (num > scrollBar.Maximum)
        num = scrollBar.Maximum;
      scrollBar.Value = num;
    }

    public void SynchronizeRows()
    {
      this.SynchronizeRows(true);
    }

    public void SynchronizeRows(bool recursive)
    {
      this.SynchronizeRows(true, true);
    }

    public void SynchronizeRows(bool recursive, bool updateContent)
    {
      if (recursive)
      {
        foreach (VirtualGridRowElement descendant in this.GetDescendants((Predicate<RadElement>) (x => x is VirtualGridRowElement), TreeTraversalMode.BreadthFirst))
          descendant.Synchronize(updateContent);
      }
      else
      {
        foreach (VirtualGridRowElement rowElement in this.ViewElement.GetRowElements())
          rowElement.Synchronize(updateContent);
      }
    }

    public void SynchronizeRow(int rowIndex, bool updateContent)
    {
      foreach (VirtualGridRowElement rowElement in this.ViewElement.GetRowElements())
      {
        if (rowElement.RowIndex == rowIndex)
        {
          rowElement.Synchronize(updateContent);
          break;
        }
      }
    }

    protected virtual void OnViewInfoPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (this.IsDisposed || this.IsDisposing)
        return;
      bool flag = e.PropertyName == "ColumnSizes[]" || e.PropertyName == "ColumnWidth";
      if (e.PropertyName == "RowCount" || e.PropertyName == "RowHeight" || (e.PropertyName == "RowSpacing" || e.PropertyName == "RowSizes[]"))
      {
        this.ViewElement.UpdateElementSpacing();
        if (this.RowScroller != null)
        {
          this.RowScroller.ItemSpacing = this.ViewInfo.RowSpacing;
          this.RowScroller.UpdateScrollRange();
        }
        this.UpdateNoDataText();
        if (this.GridElement != null && this.GridElement.EnablePaging && this.ViewInfo.ParentViewInfo == null)
          this.PagingPanelElement.UpdateView();
      }
      else if (e.PropertyName == "ColumnCount" || e.PropertyName == "ColumnWidth" || (e.PropertyName == "CellSpacing" || flag))
      {
        this.ViewElement.UpdateElementSpacing();
        if (flag)
          this.SynchronizeRows(false, false);
        else
          this.SynchronizeRows(false);
        this.ColumnScroller.ItemSpacing = this.ViewInfo.CellSpacing;
        this.ColumnScroller.UpdateScrollRange();
        this.UpdateNoDataText();
      }
      else if (e.PropertyName == "TopPinnedRows[]" || e.PropertyName == "BottomPinnedRows[]" || (e.PropertyName == "ShowFilterRow" || e.PropertyName == "ShowHeaderRow") || e.PropertyName == "ShowNewRow")
      {
        this.InvalidatePinnedRows();
        this.InvalidateMeasure();
      }
      else if (e.PropertyName == "LeftPinnedColumns[]" || e.PropertyName == "RightPinnedColumns[]")
      {
        foreach (VirtualGridRowElement rowElement in this.ViewElement.GetRowElements())
          rowElement.InvalidatePinnedColumns();
        this.UpdateOnViewInfoChanged();
      }
      else if (e.PropertyName == "WaitingRows[]")
        this.SynchronizeRows();
      else if (e.PropertyName == "ColumnDataTypes[]")
      {
        if (this.GridElement.AllowFiltering)
        {
          foreach (VirtualGridRowElement child in this.ViewElement.TopPinnedRows.Children)
          {
            if (child is VirtualGridFilterRowElement)
            {
              child.Synchronize();
              break;
            }
          }
        }
      }
      else if (e.PropertyName == "RowErrorTexts[]")
      {
        foreach (VirtualGridRowElement descendant in this.GetDescendants((Predicate<RadElement>) (x => x is VirtualGridRowElement), TreeTraversalMode.BreadthFirst))
          descendant.SynchronizeIndentCell();
      }
      else if (e.PropertyName == "ExpandedRows[]" || e.PropertyName == "EnableAlternatingRowColor")
      {
        for (int index = 0; index < this.ViewElement.ScrollableRows.Children.Count; ++index)
          (this.ViewElement.ScrollableRows.Children[index] as VirtualGridRowElement)?.Synchronize();
      }
      else if (e.PropertyName == "IsWaiting")
      {
        if (this.ViewInfo.IsWaiting)
        {
          this.waitingElement.StartWaiting();
        }
        else
        {
          this.waitingElement.StopWaiting();
          this.SynchronizeRows();
        }
      }
      else if (e.PropertyName == "AllowColumnSort")
      {
        foreach (VirtualGridRowElement child in this.ViewElement.TopPinnedRows.Children)
        {
          if (child is VirtualGridHeaderRowElement)
            child.Synchronize();
        }
      }
      else if (e.PropertyName == "EnablePaging")
      {
        if (this.ViewInfo.EnablePaging)
        {
          if (this.pagingPanel != null)
          {
            this.pagingPanel.Dispose();
            this.pagingPanel = (VirtualGridPagingPanelElement) null;
          }
          this.pagingPanel = new VirtualGridPagingPanelElement(this, this.ViewInfo);
          this.pagingPanel.UpdateView();
          this.Children.Add((RadElement) this.pagingPanel);
        }
        else if (this.pagingPanel != null)
        {
          this.pagingPanel.Dispose();
          this.pagingPanel = (VirtualGridPagingPanelElement) null;
        }
      }
      else if (e.PropertyName == "PageIndex")
      {
        if (this.ViewInfo.EnablePaging && this.pagingPanel != null)
        {
          this.pagingPanel.UpdateView();
          this.RowScroller.UpdateScrollRange();
        }
      }
      else if (e.PropertyName == "PageSize")
      {
        if (this.pagingPanel != null)
          this.pagingPanel.UpdateView();
        this.RowScroller.UpdateScrollRange();
      }
      else if (e.PropertyName == "VerticalScrollState" || e.PropertyName == "HorizontalScrollState")
        this.SetScrollState();
      else if (e.PropertyName == "AutoSizeColumnsMode")
      {
        switch (this.ViewInfo.AutoSizeColumnsMode)
        {
          case VirtualGridAutoSizeColumnsMode.None:
            this.ColumnLayout = (BaseVirtualGridColumnLayout) new StandardVirtualGridColumnLayout();
            break;
          case VirtualGridAutoSizeColumnsMode.Fill:
            this.ColumnLayout = (BaseVirtualGridColumnLayout) new StretchVirtualGridColumnLayout();
            break;
        }
      }
      this.InvalidateMeasure(true);
      if (!flag)
        this.UpdateLayout();
      this.GridElement.OnViewInfoPropertyChanged(this.ViewInfo, e.PropertyName);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != VirtualGridTableElement.AlternatingRowColorProperty && e.Property != VirtualGridTableElement.CurrentRowHeaderImageProperty && (e.Property != VirtualGridTableElement.EditRowHeaderImageProperty && e.Property != RadElement.StyleProperty))
        return;
      this.SynchronizeRows();
    }

    protected virtual void UpdateOnViewInfoChanged()
    {
      if (this.rowScroller != null)
        this.rowScroller.Dispose();
      this.rowScroller = new VirtualGridItemScroller(this.RowsViewState);
      this.rowScroller.ElementProvider = (IVirtualizedElementProvider<int>) new VirtualRowsElementProvider(this);
      this.rowScroller.ItemHeight = this.RowHeight;
      this.rowScroller.ItemSpacing = this.RowSpacing;
      this.rowScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.rowScroller.Scrollbar = this.VScrollBar;
      this.rowScroller.Traverser = (ITraverser<int>) new VirtualGridTraverser(this.RowsViewState);
      this.rowScroller.UpdateScrollRange();
      this.rowScroller.ScrollerUpdated += new EventHandler(this.rowScroller_ScrollerUpdated);
      if (this.columnScroller != null)
        this.columnScroller.Dispose();
      this.columnScroller = new VirtualGridItemScroller(this.ColumnsViewState);
      this.columnScroller.ElementProvider = (IVirtualizedElementProvider<int>) new VirtualCellsElementProvider(this);
      this.columnScroller.ItemHeight = this.ColumnWidth;
      this.columnScroller.ItemSpacing = this.CellSpacing;
      this.columnScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.columnScroller.Scrollbar = this.HScrollBar;
      this.columnScroller.Traverser = (ITraverser<int>) new VirtualGridTraverser(this.ColumnsViewState);
      this.columnScroller.UpdateScrollRange();
      this.columnScroller.ScrollerUpdated += new EventHandler(this.columnScroller_ScrollerUpdated);
      this.ViewElement.TableElement = this;
      if (this.pagingPanel != null)
      {
        this.pagingPanel.Dispose();
        this.pagingPanel = (VirtualGridPagingPanelElement) null;
      }
      if (this.ViewInfo.EnablePaging)
      {
        this.pagingPanel = new VirtualGridPagingPanelElement(this, this.ViewInfo);
        this.pagingPanel.UpdateView();
        this.Children.Add((RadElement) this.pagingPanel);
      }
      if (this.waitingElement == null)
      {
        this.waitingElement = new VirtualGridWaitingElement();
        this.waitingElement.Visibility = ElementVisibility.Collapsed;
        this.Children.Add((RadElement) this.waitingElement);
      }
      this.SetScrollState();
      if (this.ViewInfo.IsWaiting)
      {
        this.waitingElement.StartWaiting();
        this.InvalidateMeasure();
      }
      else
      {
        this.waitingElement.StopWaiting();
        this.InvalidateMeasure();
      }
      this.RowScroller.UpdateScrollRange();
      this.ColumnScroller.UpdateScrollRange();
      this.InvalidatePinnedRows();
      this.InvalidateMeasure(true);
      this.UpdateNoDataText();
    }

    protected internal void UpdateNoDataText()
    {
      if (this.ViewInfo.ColumnCount > 0 && this.ViewInfo.RowCount > 0 || this.GridElement.FilterDescriptors.Count > 0)
      {
        this.Text = string.Empty;
        this.ViewElement.Visibility = ElementVisibility.Visible;
      }
      else
      {
        this.ViewElement.Visibility = ElementVisibility.Collapsed;
        this.Text = this.GridElement.ShowNoDataText ? LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("NoDataText") : string.Empty;
      }
    }

    private void SetScrollState()
    {
      if (this.ViewInfo.ParentViewInfo != null && !this.GridElement.UseScrollbarsInHierarchy)
        this.rowScroller.ScrollState = ScrollState.AlwaysHide;
      else
        this.rowScroller.ScrollState = this.ViewInfo.VerticalScrollState;
      this.ColumnScroller.ScrollState = this.ViewInfo.HorizontalScrollState;
    }

    internal void InvalidatePinnedRows()
    {
      this.ViewElement.TopPinnedRows.DisposeChildren();
      this.ViewElement.BottomPinnedRows.DisposeChildren();
      if (this.ViewInfo.ShowHeaderRow)
        this.ViewElement.TopPinnedRows.Children.Add((RadElement) this.RowScroller.ElementProvider.GetElement(-1, (object) null));
      if (this.ViewInfo.ShowNewRow)
        this.ViewElement.TopPinnedRows.Children.Add((RadElement) this.RowScroller.ElementProvider.GetElement(-2, (object) null));
      if (this.ViewInfo.ShowFilterRow)
        this.ViewElement.TopPinnedRows.Children.Add((RadElement) this.RowScroller.ElementProvider.GetElement(-3, (object) null));
      foreach (int topPinnedItem in this.RowsViewState.TopPinnedItems)
      {
        VirtualGridRowElement element = (VirtualGridRowElement) this.RowScroller.ElementProvider.GetElement(topPinnedItem, (object) null);
        this.ViewElement.TopPinnedRows.Children.Add((RadElement) element);
        element.Attach(topPinnedItem, (object) null);
      }
      foreach (int bottomPinnedItem in this.RowsViewState.BottomPinnedItems)
      {
        VirtualGridRowElement element = (VirtualGridRowElement) this.RowScroller.ElementProvider.GetElement(bottomPinnedItem, (object) null);
        this.ViewElement.BottomPinnedRows.Children.Add((RadElement) element);
        element.Attach(bottomPinnedItem, (object) null);
      }
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.RowsViewState.DpiScaleChanged(scaleFactor);
      this.ColumnsViewState.DpiScaleChanged(scaleFactor);
    }

    private void columnScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.ViewElement.InvalidateMeasure(true);
    }

    private void rowScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      if (this.RowScroller.Scrollbar.Value == this.RowScroller.Scrollbar.Minimum)
        this.RowScroller.ScrollOffset = 0;
      (this.ViewElement.Children[0] as ScrollableVirtualRowsContainer).ScrollOffset = new SizeF(0.0f, (float) -this.RowScroller.ScrollOffset);
      this.ViewElement.InvalidateMeasure(true);
      this.Invalidate();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.PagingPanelElement != null)
      {
        this.PagingPanelElement.Measure(availableSize);
        availableSize.Height -= this.PagingPanelElement.DesiredSize.Height;
      }
      this.waitingElement.Measure(availableSize);
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.UpdateOnMeasure(availableSize))
        base.MeasureOverride(availableSize);
      if (this.ViewInfo.AutoSizeColumnsMode == VirtualGridAutoSizeColumnsMode.Fill)
        this.ColumnLayout.CalculateColumnWidths(availableSize);
      if (this.PagingPanelElement != null)
        sizeF.Height += this.PagingPanelElement.DesiredSize.Height;
      return sizeF;
    }

    protected virtual bool UpdateOnMeasure(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      ElementVisibility visibility1 = this.HScrollBar.Visibility;
      ElementVisibility visibility2 = this.VScrollBar.Visibility;
      SizeF size = clientRectangle.Size;
      if (this.HScrollBar.Visibility == ElementVisibility.Visible)
        size.Height -= this.HScrollBar.DesiredSize.Height;
      if (this.VScrollBar.Visibility == ElementVisibility.Visible)
        size.Width -= this.VScrollBar.DesiredSize.Width;
      this.RowScroller.ClientSize = size;
      size.Width -= (float) this.IndentColumnWidth;
      this.ColumnScroller.ClientSize = size;
      if (visibility1 == this.HScrollBar.Visibility)
        return visibility2 != this.VScrollBar.Visibility;
      return true;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = finalSize;
      if (this.PagingPanelElement != null)
        sizeF.Height -= this.PagingPanelElement.DesiredSize.Height;
      this.waitingElement.Arrange(new RectangleF(PointF.Empty, sizeF));
      base.ArrangeOverride(sizeF);
      if (this.PagingPanelElement != null)
        this.PagingPanelElement.Arrange(new RectangleF(new PointF(0.0f, sizeF.Height), this.PagingPanelElement.DesiredSize));
      return finalSize;
    }
  }
}
