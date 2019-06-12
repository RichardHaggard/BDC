// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridTableElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.StateManagers;
using Telerik.WinControls.UI.VisualEffects;

namespace Telerik.WinControls.UI
{
  public class GridTableElement : ScrollViewElement<RowsContainerElement>, IRowView, IGridView, IGridViewEventListener
  {
    private SizeF cachedAvailableSize = SizeF.Empty;
    public static RadProperty AlternatingRowColorProperty = RadProperty.Register(nameof (AlternatingRowColor), typeof (Color), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Lavender, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CellSpacingProperty = RadProperty.Register(nameof (CellSpacing), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ChildRowHeightProperty = RadProperty.Register(nameof (ChildRowHeight), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 250, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ColumnDragHintProperty = RadProperty.Register(nameof (ColumnDragHint), typeof (RadImageShape), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    public static RadProperty CurrentRowHeaderImageProperty = RadProperty.Register(nameof (CurrentRowHeaderImage), typeof (Image), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EditRowHeaderImageProperty = RadProperty.Register(nameof (EditRowHeaderImage), typeof (Image), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EnableHotTrackingProperty = RadProperty.Register(nameof (EnableHotTracking), typeof (bool), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.None));
    public static RadProperty ErrorRowHeaderImageProperty = RadProperty.Register(nameof (ErrorRowHeaderImage), typeof (Image), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ExtendVerticalScrollBarProperty = RadProperty.Register(nameof (ExtendVerticalScrollBar), typeof (bool), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FilterRowHeightProperty = RadProperty.Register(nameof (FilterRowHeight), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GroupHeaderHeightProperty = RadProperty.Register(nameof (GroupHeaderHeight), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GroupIndentProperty = RadProperty.Register(nameof (GroupIndent), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HasColumnHeadersProperty = RadProperty.Register("HasColumnHeaders", typeof (bool), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MenuThemeNameProperty = RadProperty.Register(nameof (MenuThemeName), typeof (string), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) "", ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NewRowHeaderImageProperty = RadProperty.Register(nameof (NewRowHeaderImage), typeof (Image), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RowDragHintProperty = RadProperty.Register(nameof (RowDragHint), typeof (RadImageShape), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    public static RadProperty RowHeaderColumnWidthProperty = RadProperty.Register(nameof (RowHeaderColumnWidth), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RowHeightProperty = RadProperty.Register(nameof (RowHeight), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RowSpacingProperty = RadProperty.Register(nameof (RowSpacing), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ScrollBarThemeNameProperty = RadProperty.Register(nameof (ScrollBarThemeName), typeof (string), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) "", ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SearchHighlightColorProperty = RadProperty.Register(nameof (SearchHighlightColor), typeof (Color), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Yellow, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SearchRowHeaderImageProperty = RadProperty.Register(nameof (SearchRowHeaderImage), typeof (Image), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SearchRowHeightProperty = RadProperty.Register(nameof (SearchRowHeight), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 29, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowSelfReferenceLinesProperty = RadProperty.Register(nameof (ShowSelfReferenceLines), typeof (bool), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty TableHeaderHeightProperty = RadProperty.Register(nameof (TableHeaderHeight), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TreeLevelIndentProperty = RadProperty.Register(nameof (TreeLevelIndent), typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 19, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawHorizontalOuterBorderProperty = RadProperty.Register("DrawHorizontalOuterBorder", typeof (bool), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawVerticalOuterBorderProperty = RadProperty.Register("DrawVerticalOuterBorder", typeof (bool), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GridBorderWidthProperty = RadProperty.Register("GridBorderWidth", typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GridBorderHeightProperty = RadProperty.Register("GridBorderHeight", typeof (int), typeof (GridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    private BestFitHelper bestFitHelper;
    private GridVisibilityHelper visibilityHelper;
    private RowScroller rowScroller;
    private ItemScroller<GridViewColumn> columnScroller;
    private RadGridViewElement rootElement;
    private GridViewInfo viewInfo;
    private VisualRowsCollection visualRows;
    private GridRowElement hoveredRow;
    private RowViewCollection childViews;
    private int updateSuspendedCount;
    private bool updating;
    private SizeF forcedDesiredSize;
    private IRadPageViewProvider pageViewProvider;
    private PageViewMode pageViewMode;
    private bool scheduleUpdateScrollRange;
    private ScrollServiceBehavior scrollBehavior;
    private IGridFilterPopupInteraction gridFilterPopup;

    static GridTableElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TableElementStateManagerFactory(), typeof (GridTableElement));
    }

    public GridTableElement()
    {
      this.bestFitHelper = new BestFitHelper(this);
      this.visibilityHelper = new GridVisibilityHelper(this);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "TableElement";
      this.AllowDrop = true;
    }

    protected override void CreateChildElements()
    {
      this.rowScroller = new RowScroller(this);
      this.rowScroller.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(this.RowScroller_ToolTipTextNeeded);
      this.rowScroller.ItemHeight = this.RowHeight;
      this.rowScroller.ItemSpacing = this.RowSpacing;
      this.rowScroller.ElementProvider = (IVirtualizedElementProvider<GridViewRowInfo>) new Telerik.WinControls.UI.RowElementProvider(this);
      this.rowScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.columnScroller = new ItemScroller<GridViewColumn>();
      this.columnScroller.ItemHeight = 50;
      this.columnScroller.ItemSpacing = this.CellSpacing;
      this.columnScroller.ElementProvider = (IVirtualizedElementProvider<GridViewColumn>) new Telerik.WinControls.UI.CellElementProvider(this);
      this.columnScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      base.CreateChildElements();
      this.rowScroller.Scrollbar = this.VScrollBar;
      this.columnScroller.Scrollbar = this.HScrollBar;
      this.visualRows = new VisualRowsCollection(this.ViewElement);
      this.childViews = new RowViewCollection(this);
      this.ViewElement.ElementSpacing = this.RowSpacing;
      this.HScrollBar.RadPropertyChanged += new RadPropertyChangedEventHandler(this.HScrollBar_RadPropertyChanged);
      this.scrollBehavior = new ScrollServiceBehavior();
      this.scrollBehavior.Add(new ScrollService((RadElement) this.ViewElement.ScrollableRows, this.VScrollBar));
      this.scrollBehavior.Add(new ScrollService((RadElement) this.ViewElement.ScrollableRows, this.HScrollBar));
    }

    protected override void DisposeManagedResources()
    {
      this.UnWireEvents();
      base.DisposeManagedResources();
      this.Detach();
      this.columnScroller.Dispose();
      this.rowScroller.Dispose();
    }

    protected internal void UnWireEvents()
    {
      this.rowScroller.ToolTipTextNeeded -= new ToolTipTextNeededEventHandler(this.RowScroller_ToolTipTextNeeded);
      this.HScrollBar.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.HScrollBar_RadPropertyChanged);
      if (this.ElementTree == null)
        return;
      this.ElementTree.ComponentTreeHandler.ThemeNameChanged -= new ThemeNameChangedEventHandler(this.OnThemeName_Changed);
      this.ElementTree.Control.MouseLeave -= new EventHandler(this.Control_MouseLeave);
    }

    public ScrollServiceBehavior ScrollBehavior
    {
      get
      {
        return this.scrollBehavior;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal BestFitHelper BestFitHelper
    {
      get
      {
        return this.bestFitHelper;
      }
    }

    internal GridVisibilityHelper VisibilityHelper
    {
      get
      {
        return this.visibilityHelper;
      }
    }

    internal SizeF ForcedDesiredSize
    {
      get
      {
        return this.forcedDesiredSize;
      }
      set
      {
        this.forcedDesiredSize = value;
        this.InvalidateMeasure();
      }
    }

    [RadPropertyDefaultValue("RowHeight", typeof (GridTableElement))]
    public virtual int RowHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.RowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.RowHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TableHeaderHeight", typeof (GridTableElement))]
    public virtual int TableHeaderHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.TableHeaderHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.TableHeaderHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("GroupHeaderHeight", typeof (GridTableElement))]
    public virtual int GroupHeaderHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.GroupHeaderHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.GroupHeaderHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("FilterRowHeight", typeof (GridTableElement))]
    public virtual int FilterRowHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.FilterRowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.FilterRowHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("SearchRowHeight", typeof (GridTableElement))]
    public virtual int SearchRowHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.SearchRowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.SearchRowHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ChildRowHeight", typeof (GridTableElement))]
    public virtual int ChildRowHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.ChildRowHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.ChildRowHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("CellSpacing", typeof (GridTableElement))]
    [Description("Gets or sets the cell spacing.")]
    public virtual int CellSpacing
    {
      get
      {
        return (int) this.GetValue(GridTableElement.CellSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.CellSpacingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("RowSpacing", typeof (GridTableElement))]
    [Description("Gets or sets the row spacing.")]
    public virtual int RowSpacing
    {
      get
      {
        return (int) this.GetValue(GridTableElement.RowSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.RowSpacingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("RowHeaderColumnWidth", typeof (GridTableElement))]
    [Description("Gets or sets the width of the GridViewRowHeaderColumn row header column.")]
    public int RowHeaderColumnWidth
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.RowHeaderColumnWidthProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.RowHeaderColumnWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("GroupIndent", typeof (GridTableElement))]
    [Description("Gets or sets the width of the GridViewIndentColumn group indent column.")]
    public int GroupIndent
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.GroupIndentProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.GroupIndentProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TreeLevelIndent", typeof (GridTableElement))]
    [Description("Gets or sets the value that determines the indent width among expander primitives in self-reference hierarchy.")]
    public int TreeLevelIndent
    {
      get
      {
        return (int) ((double) (int) this.GetValue(GridTableElement.TreeLevelIndentProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.TreeLevelIndentProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("AlternatingRowColor", typeof (GridTableElement))]
    [Description("Gets or sets a value indicating the alternating row color for odd rows.")]
    public virtual Color AlternatingRowColor
    {
      get
      {
        return (Color) this.GetValue(GridTableElement.AlternatingRowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.AlternatingRowColorProperty, (object) value);
      }
    }

    [Description("Gets or sets a row header image for new row.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [Category("RowHeader")]
    public virtual Image NewRowHeaderImage
    {
      get
      {
        return (Image) this.GetValue(GridTableElement.NewRowHeaderImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.NewRowHeaderImageProperty, (object) value);
      }
    }

    [Category("RowHeader")]
    [Description("Gets or sets a row header image for search row.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image SearchRowHeaderImage
    {
      get
      {
        return (Image) this.GetValue(GridTableElement.SearchRowHeaderImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.SearchRowHeaderImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the color that will be used for highlighting search matches.")]
    [RadPropertyDefaultValue("SearchHighlightColor", typeof (GridTableElement))]
    public virtual Color SearchHighlightColor
    {
      get
      {
        return (Color) this.GetValue(GridTableElement.SearchHighlightColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.SearchHighlightColorProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets a row header image for editing current row.")]
    [Category("RowHeader")]
    public virtual Image EditRowHeaderImage
    {
      get
      {
        return (Image) this.GetValue(GridTableElement.EditRowHeaderImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.EditRowHeaderImageProperty, (object) value);
      }
    }

    [Description("Gets or sets a row header image for row with error.")]
    [Category("RowHeader")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image ErrorRowHeaderImage
    {
      get
      {
        return (Image) this.GetValue(GridTableElement.ErrorRowHeaderImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.ErrorRowHeaderImageProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets a row header image for current row.")]
    [Category("RowHeader")]
    public virtual Image CurrentRowHeaderImage
    {
      get
      {
        return (Image) this.GetValue(GridTableElement.CurrentRowHeaderImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.CurrentRowHeaderImageProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether the vertical scrollbar should be extended to encompass the upper right corner.")]
    [RadPropertyDefaultValue("ExtendVerticalScrollBar", typeof (GridTableElement))]
    public bool ExtendVerticalScrollBar
    {
      get
      {
        return (bool) this.GetValue(GridTableElement.ExtendVerticalScrollBarProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.ExtendVerticalScrollBarProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("EnableHotTracking", typeof (GridTableElement))]
    [Description("Gets or sets a boolean value that specifies if the hottracking is enabled.")]
    public bool EnableHotTracking
    {
      get
      {
        return (bool) this.GetValue(GridTableElement.EnableHotTrackingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.EnableHotTrackingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("MenuThemeName", typeof (GridTableElement))]
    [Description("Gets or sets a value indicating the name of the theme for the context menu in the current GridTableElement.")]
    public string MenuThemeName
    {
      get
      {
        return (string) this.GetValue(GridTableElement.MenuThemeNameProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.MenuThemeNameProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating the name of the theme for the scroll bars in the current GridTableElement.")]
    [RadPropertyDefaultValue("ScrollBarThemeName", typeof (GridTableElement))]
    public string ScrollBarThemeName
    {
      get
      {
        return (string) this.GetValue(GridTableElement.ScrollBarThemeNameProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.ScrollBarThemeNameProperty, (object) value);
      }
    }

    public RowScroller RowScroller
    {
      get
      {
        return this.rowScroller;
      }
    }

    public ItemScroller<GridViewColumn> ColumnScroller
    {
      get
      {
        return this.columnScroller;
      }
    }

    internal GridRowElement HoveredRow
    {
      get
      {
        return this.hoveredRow;
      }
      set
      {
        if (this.hoveredRow == value)
          return;
        if (this.hoveredRow != null)
        {
          int num1 = (int) this.hoveredRow.SetValue(GridRowElement.HotTrackingProperty, (object) false);
        }
        this.hoveredRow = value;
        if (this.hoveredRow == null || !this.EnableHotTracking)
          return;
        int num2 = (int) this.hoveredRow.SetValue(GridRowElement.HotTrackingProperty, (object) true);
      }
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        if (this.ViewInfo != null)
          return this.ViewInfo.ViewTemplate;
        return (GridViewTemplate) null;
      }
    }

    public MasterGridViewTemplate MasterTemplate
    {
      get
      {
        if (this.ViewTemplate != null)
          return this.ViewTemplate.MasterTemplate;
        return (MasterGridViewTemplate) null;
      }
    }

    public IVirtualizedElementProvider<GridViewRowInfo> RowElementProvider
    {
      get
      {
        return this.RowScroller.ElementProvider;
      }
      set
      {
        if (this.RowScroller.ElementProvider == value)
          return;
        this.RowScroller.ElementProvider = value;
        this.UpdateElementProviders();
      }
    }

    public IVirtualizedElementProvider<GridViewColumn> CellElementProvider
    {
      get
      {
        return this.ColumnScroller.ElementProvider;
      }
      set
      {
        if (this.ColumnScroller.ElementProvider == value)
          return;
        this.ColumnScroller.ElementProvider = value;
        this.UpdateElementProviders();
      }
    }

    public bool IsUpdating
    {
      get
      {
        return this.updateSuspendedCount > 0;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [VsbBrowsable(true)]
    [Browsable(false)]
    public RadImageShape RowDragHint
    {
      get
      {
        return (RadImageShape) this.GetValue(GridTableElement.RowDragHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.RowDragHintProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    [Browsable(false)]
    public RadImageShape ColumnDragHint
    {
      get
      {
        return (RadImageShape) this.GetValue(GridTableElement.ColumnDragHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.ColumnDragHintProperty, (object) value);
      }
    }

    public IRadPageViewProvider PageViewProvider
    {
      get
      {
        return this.pageViewProvider;
      }
      set
      {
        if (this.pageViewProvider == value)
          return;
        this.pageViewProvider = value;
        this.UpdateAll();
      }
    }

    public PageViewMode PageViewMode
    {
      get
      {
        return this.pageViewMode;
      }
      set
      {
        if (this.pageViewMode == value)
          return;
        this.pageViewMode = value;
        this.UpdateAll();
      }
    }

    public bool ShowSelfReferenceLines
    {
      get
      {
        return (bool) this.GetValue(GridTableElement.ShowSelfReferenceLinesProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridTableElement.ShowSelfReferenceLinesProperty, (object) value);
      }
    }

    internal IGridFilterPopupInteraction GridFilterPopup
    {
      get
      {
        return this.gridFilterPopup;
      }
      set
      {
        this.gridFilterPopup = value;
      }
    }

    public void Update(GridUINotifyAction action, params GridViewRowInfo[] rowInfos)
    {
      switch (action)
      {
        case GridUINotifyAction.Reset:
        case GridUINotifyAction.ExpandedChanged:
          this.UpdateAll();
          break;
        case GridUINotifyAction.DataChanged:
          this.UpdateCellContent();
          break;
        case GridUINotifyAction.StateChanged:
          this.UpdateRowInfo();
          break;
        case GridUINotifyAction.RowHeightChanged:
          this.ViewElement.UpdateRows();
          this.RowScroller.UpdateScrollRange();
          break;
      }
    }

    public virtual void ScrollTo(int row, int column)
    {
      this.ScrollToRow(row);
      this.columnScroller.ScrollToItem((GridViewColumn) this.ViewTemplate.Columns[column], false);
      this.UpdateLayout();
    }

    public virtual void ScrollToRow(int row)
    {
      if (this.ViewTemplate != null && this.ViewTemplate.Rows.Count > 0 && row == 0)
        this.RowScroller.ScrollToFirstRow();
      else
        this.ScrollToRow(this.GetRowToScroll(row));
    }

    private GridViewRowInfo GetRowToScroll(int row)
    {
      GridTraverser gridTraverser = new GridTraverser((IHierarchicalRow) this.ViewTemplate);
      gridTraverser.TraversalMode = GridTraverser.TraversalModes.ScrollableRows;
      gridTraverser.RowVisible += new RowEnumeratorEventHandler(this.RowToScrollTraverser_RowVisible);
      while (gridTraverser.MoveNext() && row > 0)
        --row;
      gridTraverser.RowVisible -= new RowEnumeratorEventHandler(this.RowToScrollTraverser_RowVisible);
      return gridTraverser.Current;
    }

    public virtual void ScrollToRow(GridViewRowInfo rowInfo)
    {
      if (rowInfo == null || rowInfo.IsPinned || (!rowInfo.IsVisible || rowInfo is GridViewDetailsRowInfo))
        return;
      this.ViewElement.InvalidateMeasure();
      this.ViewElement.UpdateLayout();
      if (this.GridViewElement.AutoSizeRows || this.ViewTemplate.Templates.Count > 0 && rowInfo.ViewTemplate.Parent != null)
      {
        this.ScrollToRowCore(rowInfo, false);
      }
      else
      {
        this.rowScroller.ScrollToItem(rowInfo, false);
        this.UpdateLayout();
      }
    }

    public virtual void ScrollToColumn(int columnIndex)
    {
      GridViewColumn column = (GridViewColumn) this.ViewTemplate.Columns[columnIndex];
      if (column == null || column.PinPosition != PinnedColumnPosition.None)
        return;
      if (!(this.ViewElement.RowLayout is TableViewRowLayout) && this.ViewElement.RowLayout is TableViewRowLayoutBase)
      {
        TableViewRowLayoutBase rowLayout = this.ViewElement.RowLayout as TableViewRowLayoutBase;
        if (rowLayout != null)
          this.SetScrollValue(this.columnScroller.Scrollbar, rowLayout.GetColumnOffset(column));
      }
      else
        this.columnScroller.ScrollToItem(column, false);
      this.UpdateLayout();
    }

    public virtual void Initialize(RadGridViewElement gridRootElement, GridViewInfo viewInfo)
    {
      this.rootElement = gridRootElement;
      this.rootElement.PropertyChanged += new PropertyChangedEventHandler(this.GridElement_PropertyChanged);
      this.viewInfo = viewInfo;
      GridViewTemplate viewTemplate = this.viewInfo.ViewTemplate;
      GridTraverser gridTraverser = new GridTraverser(this.ViewInfo, GridTraverser.TraversalModes.ScrollableRows);
      this.SetScrollState();
      this.rowScroller.Traverser = (ITraverser<GridViewRowInfo>) gridTraverser;
      this.rowScroller.Scrollbar.Maximum = 0;
      this.ViewElement.TopPinnedRows.DataProvider = (IEnumerable) new GridTraverser(this.viewInfo, GridTraverser.TraversalModes.TopPinnedRows);
      this.ViewElement.BottomPinnedRows.DataProvider = (IEnumerable) new GridTraverser(this.viewInfo, GridTraverser.TraversalModes.BottomPinnedRows);
      if (this.ViewElement.RowLayout != null && this.ViewElement.RowLayout.Owner != this)
        this.ViewElement.RowLayout.Initialize(this);
      this.ViewElement.TopPinnedRows.ItemSpacing = this.RowSpacing;
      this.ViewElement.ScrollableRows.ItemSpacing = this.RowSpacing;
      this.ViewElement.BottomPinnedRows.ItemSpacing = this.RowSpacing;
      this.RowScroller.ItemSpacing = this.RowSpacing;
      this.MasterTemplate.EventDispatcher.AddListener<EventArgs>(EventDispatcher.SelectionChanged, new EventHandler<EventArgs>(this.OnSelectionChanged));
      this.MasterTemplate.SynchronizationService.AddListener((IGridViewEventListener) this);
      this.UpdateNoDataText();
    }

    private void UpdateNoDataText()
    {
      if (this.ViewTemplate.Columns.Count > 0)
      {
        this.Text = string.Empty;
        if (this.NeverMeasured)
          this.UpdateLayout();
        this.RowScroller.UpdateScrollRange();
        this.ViewElement.Visibility = ElementVisibility.Visible;
      }
      else
      {
        this.SetChildrenVisibility(ElementVisibility.Collapsed);
        this.Text = this.GridViewElement.ShowNoDataText ? LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("NoDataText") : string.Empty;
      }
    }

    private void SetChildrenVisibility(ElementVisibility visibility)
    {
      foreach (RadElement child in this.Children)
        child.Visibility = visibility;
    }

    public virtual void Detach()
    {
      if (this.viewInfo == null)
        return;
      foreach (IGridView childView in this.ChildViews)
        childView.Detach();
      GridViewTemplate viewTemplate = this.viewInfo.ViewTemplate;
      viewTemplate.MasterTemplate.SynchronizationService.RemoveListener((IGridViewEventListener) this);
      viewTemplate.MasterTemplate.EventDispatcher.RemoveListener<EventArgs>(EventDispatcher.SelectionChanged, new EventHandler<EventArgs>(this.OnSelectionChanged));
      this.rootElement.PropertyChanged -= new PropertyChangedEventHandler(this.GridElement_PropertyChanged);
      this.viewInfo = (GridViewInfo) null;
    }

    public virtual void UpdateView()
    {
      this.UpdateAll();
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.rootElement;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }

    public bool BeginUpdate()
    {
      ++this.updateSuspendedCount;
      return true;
    }

    public bool EndUpdate()
    {
      return this.EndUpdate(true);
    }

    public bool EndUpdate(bool performUpdate)
    {
      if (this.updateSuspendedCount == 0)
        return false;
      --this.updateSuspendedCount;
      if (this.updateSuspendedCount > 0)
        return false;
      if (performUpdate)
        this.UpdateView();
      return true;
    }

    public virtual GridCellElement CurrentCell
    {
      get
      {
        if (this.MasterTemplate != null && this.MasterTemplate.CurrentRow != null && this.GridViewElement.CurrentView == this)
          return this.GetCellElement(this.MasterTemplate.CurrentRow, this.ViewTemplate.CurrentColumn);
        return (GridCellElement) null;
      }
    }

    public ReadOnlyCollection<IRowView> ChildViews
    {
      get
      {
        return new ReadOnlyCollection<IRowView>((IList<IRowView>) new List<IRowView>((IEnumerable<IRowView>) this.childViews));
      }
    }

    public bool IsCurrentView
    {
      get
      {
        return this.GridViewElement.CurrentView == this;
      }
    }

    public virtual GridRowElement CurrentRow
    {
      get
      {
        if (this.MasterTemplate == null)
          return (GridRowElement) null;
        return this.GetRowElement(this.MasterTemplate.CurrentRow);
      }
    }

    public virtual Point CurrentCellAddress
    {
      get
      {
        GridViewDataColumn currentColumn = this.ViewTemplate.CurrentColumn as GridViewDataColumn;
        return new Point(currentColumn != null ? this.ViewTemplate.Columns.IndexOf(currentColumn) : -1, this.CurrentRow != null ? this.VisualRows.IndexOf(this.CurrentRow) : -1);
      }
    }

    public virtual IList<GridRowElement> VisualRows
    {
      get
      {
        return (IList<GridRowElement>) this.visualRows;
      }
    }

    public virtual int RowsPerPage
    {
      get
      {
        return this.DisplayedRowCount(false);
      }
    }

    public virtual int DisplayedRowCount(bool includePartialRow)
    {
      int num = 0;
      foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.VisualRows)
      {
        if (!(visualRow.RowInfo is GridViewSystemRowInfo) || visualRow.RowInfo is GridViewNewRowInfo)
          ++num;
      }
      GridDataRowElement visualRow1 = this.VisualRows[this.VisualRows.Count - 1] as GridDataRowElement;
      if (!includePartialRow && visualRow1 != null && visualRow1.BoundingRectangle.Bottom > visualRow1.Parent.Size.Height)
        --num;
      return num;
    }

    public virtual int DisplayedColumnCount(bool includePartialColumn)
    {
      int num = 0;
      if (this.VisualRows.Count > 0)
      {
        int width = this.ViewElement.Size.Width;
        foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.VisualRows)
        {
          GridVirtualizedRowElement virtualizedRowElement = visualRow as GridVirtualizedRowElement;
          if (virtualizedRowElement != null)
          {
            width = virtualizedRowElement.ScrollableColumns.Size.Width;
            break;
          }
        }
        VisualCellsCollection visualCells = this.VisualRows[0].VisualCells;
        for (int index = 0; index < visualCells.Count; ++index)
        {
          GridCellElement gridCellElement = visualCells[index];
          if ((gridCellElement.IsPinned || (includePartialColumn || gridCellElement.BoundingRectangle.Right <= width) && gridCellElement.BoundingRectangle.Left <= width) && gridCellElement.ColumnInfo is GridViewDataColumn)
            ++num;
        }
      }
      return num;
    }

    public virtual GridRowElement GetRowElement(GridViewRowInfo rowInfo)
    {
      for (int index = 0; index < this.VisualRows.Count; ++index)
      {
        GridRowElement visualRow = this.VisualRows[index];
        if (visualRow.RowInfo == rowInfo)
          return visualRow;
      }
      GridViewSummaryRowInfo viewSummaryRowInfo = rowInfo as GridViewSummaryRowInfo;
      if (viewSummaryRowInfo != null)
      {
        for (int index = 0; index < this.VisualRows.Count; ++index)
        {
          GridRowElement visualRow = this.VisualRows[index];
          GridViewSummaryRowInfo rowInfo1 = visualRow.RowInfo as GridViewSummaryRowInfo;
          if (rowInfo1 != null && rowInfo1.SummaryRowItem == viewSummaryRowInfo.SummaryRowItem)
          {
            visualRow.RowInfo.Cache.Clear();
            return visualRow;
          }
        }
      }
      return (GridRowElement) null;
    }

    public virtual GridCellElement GetCellElement(
      GridViewRowInfo rowInfo,
      GridViewColumn column)
    {
      GridRowElement rowElement = this.GetRowElement(rowInfo);
      if (rowElement == null)
        return (GridCellElement) null;
      int count = rowElement.VisualCells.Count;
      for (int index = 0; index < count; ++index)
      {
        GridCellElement visualCell = rowElement.VisualCells[index];
        if (visualCell.ColumnInfo == column)
          return visualCell;
      }
      return (GridCellElement) null;
    }

    public virtual void InvalidateRow(GridViewRowInfo rowInfo)
    {
      foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.VisualRows)
      {
        if (visualRow.RowInfo == rowInfo)
        {
          visualRow.UpdateContent();
          visualRow.UpdateInfo();
          visualRow.Invalidate();
          break;
        }
      }
    }

    public virtual void InvalidateCell(GridViewRowInfo rowInfo, GridViewColumn column)
    {
      GridRowElement rowElement = this.GetRowElement(rowInfo);
      if (rowElement == null)
        return;
      foreach (GridCellElement visualCell in rowElement.VisualCells)
      {
        if (visualCell.ColumnInfo == column)
        {
          visualCell.SetContent();
          visualCell.UpdateInfo();
          visualCell.Invalidate();
          break;
        }
      }
    }

    public virtual bool EnsureRowVisible(GridViewRowInfo rowInfo)
    {
      if (rowInfo == null || !rowInfo.IsVisible || (this.MasterTemplate == null || this.MasterTemplate.SuspendEnsureVisible))
        return false;
      if (!this.GridViewElement.UseScrollbarsInHierarchy && this.viewInfo.ParentRow != null)
        return this.GridViewElement.TableElement.EnsureRowVisible(rowInfo);
      if (rowInfo.IsPinned && rowInfo.ViewInfo == this.ViewInfo)
        return false;
      if (this.MasterTemplate.Templates.Count <= 0 || this.GridViewElement.UseScrollbarsInHierarchy)
        return this.visibilityHelper.EnsureRowVisible(rowInfo);
      this.EnsureRowVisibleCore(rowInfo);
      GridTableElement rowView = this.GridViewElement.GetRowView(rowInfo.ViewInfo) as GridTableElement;
      if (rowView != null && rowView.GetRowElement(rowInfo) != null)
        return true;
      this.ScrollToRow(rowInfo);
      return true;
    }

    public virtual bool EnsureCellVisible(GridViewRowInfo rowInfo, GridViewColumn column)
    {
      if (this.MasterTemplate.Templates.Count > 0)
        this.UpdateLayout();
      this.EnsureRowVisible(rowInfo);
      if (!this.IsInValidState(true))
        return false;
      return this.visibilityHelper.EnsureCellVisible(rowInfo, column);
    }

    public virtual bool IsRowVisible(GridViewRowInfo value)
    {
      return this.GetRowElement(value) != null;
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (dataContext is GridViewColumn && !(this.ViewTemplate.ViewDefinition is ColumnGroupsViewDefinition))
      {
        GridViewColumn gridViewColumn = dataContext as GridViewColumn;
        gridViewColumn.IsVisible = true;
        GridViewGroupColumn gridViewGroupColumn = gridViewColumn as GridViewGroupColumn;
        if (gridViewGroupColumn == null || gridViewGroupColumn.Group.IsSuspended)
          return;
        PropertyChangedEventArgs changedEventArgs = new PropertyChangedEventArgs("IsVisible");
        this.ViewTemplate.OnViewChanged((object) gridViewGroupColumn.Group, new DataViewChangedEventArgs(ViewChangedAction.ColumnGroupPropertyChanged, (object) changedEventArgs));
      }
      else if (dataContext is GridViewColumnGroup)
      {
        (dataContext as GridViewColumnGroup).IsVisible = true;
      }
      else
      {
        if (!(dataContext is GroupFieldDragDropContext))
          return;
        GroupFieldDragDropContext fieldDragDropContext = dataContext as GroupFieldDragDropContext;
        TemplateGroupsElement.RaiseGroupByChanging(fieldDragDropContext.ViewTemplate, fieldDragDropContext.GroupDescription, NotifyCollectionChangedAction.Remove);
        fieldDragDropContext.GroupDescription.GroupNames.Remove(fieldDragDropContext.SortDescription);
        if (fieldDragDropContext.GroupDescription.GroupNames.Count == 0)
          fieldDragDropContext.ViewTemplate.DataView.GroupDescriptors.Remove(fieldDragDropContext.GroupDescription);
        TemplateGroupsElement.RaiseGroupByChanged(fieldDragDropContext.ViewTemplate, fieldDragDropContext.GroupDescription, NotifyCollectionChangedAction.Remove);
      }
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (dataContext is GridViewColumn)
        return !(dataContext as GridViewColumn).IsVisible;
      if (dataContext is GridViewColumnGroup)
        return !(dataContext as GridViewColumnGroup).IsVisible;
      return dataContext is GroupFieldDragDropContext;
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.UI;
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
        return GridEventProcessMode.Process;
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
      GridViewColumn sender1 = eventData.Sender as GridViewColumn;
      if (sender1 != null && sender1.OwnerTemplate == this.ViewTemplate)
        return this.ProcessColumnEvent(sender1, eventData);
      GridViewRowInfo sender2 = eventData.Sender as GridViewRowInfo;
      if (sender2 != null && sender2.ViewTemplate == this.ViewTemplate)
        return this.ProcessRowEvent(sender2, eventData);
      GridViewFilterDescriptorCollection sender3 = eventData.Sender as GridViewFilterDescriptorCollection;
      if (sender3 != null && sender3.Owner == this.ViewTemplate)
        return this.ProcessFilterDescriptorCollectionEvent(sender3, eventData);
      GridViewTemplate sender4 = eventData.Sender as GridViewTemplate;
      if (eventData.Info.Id == KnownEvents.ViewChanged && (eventData.Arguments[0] as DataViewChangedEventArgs).Action == ViewChangedAction.EnsureRowVisible || (eventData.Info.Id == KnownEvents.ViewChanged && (eventData.Arguments[0] as DataViewChangedEventArgs).Action == ViewChangedAction.EnsureCellVisible && (this.ViewInfo.ParentRow == null && this.ViewTemplate.Templates.Count > 0) || sender4 == this.ViewTemplate))
        return this.ProcessTemplateEvent(eventData);
      if (sender4 == this.MasterTemplate)
        return this.ProcessMasterTemplateEvent(eventData);
      if (eventData.Sender as GridViewInfo == this.viewInfo)
        return this.ProcessViewInfoEvent(eventData);
      if (eventData.Info.Id == KnownEvents.LocalizationProviderChanged && this.IsInValidState(true))
      {
        if (this.GridViewElement.IsInEditMode && this.GridViewElement.CurrentCell != null && this.GridViewElement.CurrentCell.ViewInfo == this.ViewInfo)
          this.GridViewElement.CloseEditor();
        this.UpdateNoDataText();
        this.ViewElement.ClearRows();
        this.ViewElement.UpdateRowsWhenColumnsChanged();
      }
      if (eventData.Info.Id == KnownEvents.HierarchyChanged)
      {
        if (eventData.Sender == this.ViewTemplate.Templates)
          this.ViewElement.RowLayout.InvalidateRenderColumns();
        this.ViewElement.RowLayout.InvalidateLayout();
        this.InvalidateMeasure(true);
      }
      if (eventData.Info.Id == KnownEvents.ViewChanged && (eventData.Arguments[0] as DataViewChangedEventArgs).Action == ViewChangedAction.Add)
      {
        this.RowScroller.ElementProvider.ClearCache();
        this.ColumnScroller.ElementProvider.ClearCache();
        this.InvalidateMeasure(true);
        this.ViewElement.UpdateRowsWhenColumnsChanged();
      }
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessViewInfoEvent(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id != KnownEvents.BatchPropertyChanged)
        return (GridViewEventResult) null;
      PropertyChangedEventArgs propertyChanged = eventData.Arguments[0] as PropertyChangedEventArgs;
      return this.ProcessBatchPropertyChanged(eventData.Originator as Type, propertyChanged);
    }

    protected virtual GridViewEventResult ProcessFilterDescriptorCollectionEvent(
      GridViewFilterDescriptorCollection filters,
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.CollectionChanged && this.ViewTemplate.EnableFiltering)
        this.GetRowElement((GridViewRowInfo) this.ViewInfo.TableFilteringRow)?.UpdateCells();
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessRowEvent(
      GridViewRowInfo row,
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.PropertyChanged)
        this.UpdateOnRowPropertyChanged(row, (GridPropertyChangedEventArgs) eventData.Arguments[0]);
      else if (eventData.Info.Id == KnownEvents.RowInvalidated)
      {
        GridRowElement rowElement = this.GetRowElement(row);
        if (rowElement != null)
        {
          rowElement.UpdateContent();
          rowElement.UpdateInfo();
        }
      }
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessColumnEvent(
      GridViewColumn column,
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.PropertyChanged)
        this.UpdateOnColumnPropertyChanged(column, (RadPropertyChangedEventArgs) eventData.Arguments[0]);
      else if (eventData.Info.Id == KnownEvents.ColumnDataSourceInitializing)
        this.UpdateCellContentByColumn(column);
      else if (GridViewSynchronizationService.IsConditionalFormattingCollectionChangedEvent(eventData))
        this.UpdateRowInfo();
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessMasterTemplateEvent(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id != KnownEvents.PropertyChanged || !((eventData.Arguments[0] as PropertyChangedEventArgs).PropertyName == "GridReadOnly"))
        return (GridViewEventResult) null;
      this.ViewElement.UpdateRows();
      this.RowScroller.UpdateScrollRange();
      return new GridViewEventResult(false, false);
    }

    protected virtual GridViewEventResult ProcessTemplateEvent(
      GridViewEvent eventData)
    {
      if (GridViewSynchronizationService.IsColumnsCollectionChangedEvent(eventData))
      {
        NotifyCollectionChangedEventArgs changedEventArgs = eventData.Arguments[0] as NotifyCollectionChangedEventArgs;
        if (changedEventArgs != null && changedEventArgs.Action == NotifyCollectionChangedAction.Move)
        {
          this.ViewElement.RowLayout.InvalidateRenderColumns();
          object position = this.ColumnScroller.Traverser.Position;
          this.ColumnScroller.Traverser = (ITraverser<GridViewColumn>) new ItemsTraverser<GridViewColumn>(this.ViewElement.RowLayout.ScrollableColumns);
          this.ColumnScroller.Traverser.Position = position;
          this.ViewElement.UpdateRows(true);
        }
        this.ViewElement.UpdateRowsWhenColumnsChanged();
        this.UpdateNoDataText();
      }
      else if (GridViewSynchronizationService.IsTemplatePropertyChangedEvent(eventData))
      {
        GridViewEventResult gridViewEventResult = (GridViewEventResult) null;
        if (eventData.Sender == this.MasterTemplate)
          gridViewEventResult = this.ProcessMasterTemplateEvent(eventData);
        if (gridViewEventResult == null)
          this.UpdateOnTemplatePropertyChanged((PropertyChangedEventArgs) eventData.Arguments[0]);
      }
      else if (eventData.Info.Id == KnownEvents.ViewChanged)
        this.UpdateView(eventData.Originator, eventData.Arguments[0] as DataViewChangedEventArgs);
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessBatchPropertyChanged(
      Type originatorType,
      PropertyChangedEventArgs propertyChanged)
    {
      if ((object) originatorType != (object) typeof (GridViewRowInfo))
        return (GridViewEventResult) null;
      foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.VisualRows)
      {
        if (propertyChanged.PropertyName == "IsSelected")
        {
          visualRow.IsSelected = visualRow.Data.IsSelected;
          visualRow.UpdateInfo();
        }
      }
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.PostProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    bool IGridViewEventListener.AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }

    private void UpdateView(object sender, DataViewChangedEventArgs args)
    {
      if (this.viewInfo == null || !this.IsInValidState(true) || (this.updateSuspendedCount > 0 || this.updating))
        return;
      this.updating = true;
      this.UpdateViewCore(sender, args);
      this.updating = false;
    }

    private void UpdateViewCore(object sender, DataViewChangedEventArgs args)
    {
      switch (args.Action)
      {
        case ViewChangedAction.Add:
          this.UpdateOnAdd(args);
          break;
        case ViewChangedAction.Remove:
          this.UpdateOnRemove(args);
          break;
        case ViewChangedAction.Move:
          this.ViewElement.UpdateRows();
          break;
        case ViewChangedAction.Reset:
          this.UpdateAll();
          break;
        case ViewChangedAction.ItemChanged:
          this.UpdateWhenItemChanged(args);
          break;
        case ViewChangedAction.FilteringChanged:
          this.UpdateOnFilteringChanged();
          break;
        case ViewChangedAction.SortingChanged:
          this.InvalidateRow((GridViewRowInfo) this.ViewInfo.TableHeaderRow);
          if (this.ViewTemplate.IsSelfReference)
            this.RowScroller.Traverser.Reset();
          if (this.ViewInfo.ChildRows.Count > 0)
          {
            GridViewRowInfo childRow = this.ViewInfo.ChildRows[0];
          }
          this.ViewElement.UpdateRows();
          foreach (GridRowElement child in this.ViewElement.ScrollableRows.Children)
            child.Synchronize();
          if (this.ViewTemplate.Templates.Count <= 0)
            break;
          this.RowScroller.UpdateScrollValue();
          break;
        case ViewChangedAction.GroupingChanged:
          this.UpdateWhenGroupingChanged();
          break;
        case ViewChangedAction.PagingChanged:
          this.UpdateWhenPagingChanged();
          break;
        case ViewChangedAction.DataChanged:
          if (this.MasterTemplate != null && this.MasterTemplate.IsSelfReference)
          {
            this.ViewElement.ClearRows();
            this.rowScroller.UpdateScrollRange();
          }
          this.UpdateCellContent();
          break;
        case ViewChangedAction.EnsureRowVisible:
          if (!this.CanEnsureVisibility)
            break;
          this.EnsureRowVisible((GridViewRowInfo) args.NewItems[0]);
          break;
        case ViewChangedAction.EnsureCellVisible:
          if (!this.CanEnsureVisibility)
            break;
          this.EnsureCellVisible((GridViewRowInfo) args.NewItems[0], (GridViewColumn) args.NewItems[1]);
          break;
        case ViewChangedAction.BestFitColumn:
          this.bestFitHelper.ProcessRequests();
          break;
        case ViewChangedAction.BeginEdit:
          this.GridViewElement.EditorManager.BeginEdit();
          break;
        case ViewChangedAction.ExpandedChanged:
          if (this.NeverMeasured)
            this.UpdateLayout();
          this.rowScroller.UpdateScrollRange();
          this.ViewElement.UpdateRows();
          break;
        case ViewChangedAction.ColumnGroupPropertyChanged:
          this.UpdateOnColumnGroupPropertyChanged((GridViewColumnGroup) sender, (PropertyChangedEventArgs) args.NewItems[0]);
          break;
        case ViewChangedAction.EndEdit:
          if (!this.GridViewElement.IsInEditMode)
            break;
          this.GridViewElement.EditorManager.EndEdit();
          break;
      }
    }

    private bool CanEnsureVisibility
    {
      get
      {
        return this.GridViewElement.SplitMode == RadGridViewSplitMode.None || this.GridViewElement.SynchronizeCurrentRowInSplitMode || this.GridViewElement.CurrentView == this;
      }
    }

    private void UpdateOnColumnPropertyChanged(GridViewColumn column, RadPropertyChangedEventArgs e)
    {
      if (e.Property == GridViewColumn.PinPositionProperty || e.Property == GridViewColumn.IsVisibleProperty)
      {
        this.ViewElement.UpdateRowsWhenColumnsChanged();
        if (e.Property == GridViewColumn.IsVisibleProperty && this.GridViewElement.AutoSizeRows)
        {
          this.BeginUpdate();
          foreach (GridViewRowInfo row in this.ViewTemplate.Rows)
            row.Height = -1;
          this.EndUpdate(false);
          this.UpdateLayout();
          this.RowScroller.UpdateScrollRange();
        }
      }
      else if (e.Property == GridViewDataColumn.FormatInfoProperty || e.Property == GridViewDataColumn.NullValueProperty || (e.Property == GridViewDataColumn.DataTypeConverterProperty || e.Property == GridViewDataColumn.DataTypeProperty) || e.Property == GridViewColumn.FieldNameProperty)
        this.UpdateCellContentByColumn(column);
      else if (e.Property == GridViewColumn.WidthProperty || e.Property == GridViewColumn.MaxWidthProperty)
      {
        ColumnGroupRowLayout rowLayout = this.ViewElement.RowLayout as ColumnGroupRowLayout;
        if (rowLayout != null && !rowLayout.ColumnWidthUpdateSuspended && e.Property == GridViewColumn.WidthProperty)
          rowLayout.InvalidateRenderColumns();
        this.ViewElement.UpdateRows(true);
        if (this.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
          this.ColumnScroller.UpdateScrollStep();
      }
      else if (e.Property == GridViewColumn.WrapTextProperty)
        this.ViewElement.UpdateRows(true);
      if (!this.ViewTemplate.ContainsColumnExpression && e.Property != GridViewColumn.ExpressionProperty)
        return;
      ExpressionAccessor.ExpressionErrorRaised = false;
      this.UpdateCellContent();
    }

    private void UpdateOnRowPropertyChanged(GridViewRowInfo row, GridPropertyChangedEventArgs e)
    {
      if (e.PropertyName == "IsExpanded")
        this.UpdateOnRowExpanded(row);
      else if (e.PropertyName == "Height" || e.PropertyName == "MinHeight" || e.PropertyName == "MaxHeight")
      {
        this.ViewElement.UpdateRows();
        this.RowScroller.UpdateScrollRange();
      }
      else if (e.PropertyName == "IsVisible")
      {
        if (row.IsVisible)
          this.RowScroller.UpdateScrollRange(this.RowScroller.Scrollbar.Maximum + (int) this.RowScroller.ElementProvider.GetElementSize(row).Height + this.RowSpacing, true);
        else if (this.RowScroller.Scrollbar.Maximum > 0)
          this.RowScroller.UpdateScrollRange(this.RowScroller.Scrollbar.Maximum - (int) this.RowScroller.ElementProvider.GetElementSize(row).Height - this.RowSpacing, true);
        this.ViewElement.UpdateRows();
      }
      else if (e.PropertyName == "PinPosition")
      {
        this.ViewElement.UpdateRows();
        this.RowScroller.UpdateScrollRange();
        this.UpdateLayout();
      }
      else if (e.PropertyName == "RowPosition")
      {
        this.ViewElement.UpdateRows();
        int num = this.VScrollBar.Value;
        this.SuspendLayout();
        this.VScrollBar.Value = 0;
        this.VScrollBar.Value = num;
        this.ResumeLayout(false);
      }
      else
      {
        if (!(e.PropertyName == "ErrorText"))
          return;
        this.InvalidateRow(row);
      }
    }

    private void UpdateOnColumnGroupPropertyChanged(
      GridViewColumnGroup group,
      PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Rows")
      {
        this.ViewElement.RowLayout.InvalidateLayout();
        if (!(this.ViewElement.RowLayout is ColumnGroupRowLayout) || this.ElementTree.Control != null && this.ElementTree.Control.FindForm() == null)
          this.ViewElement.RowLayout.InvalidateRenderColumns();
        this.ViewElement.UpdateRows(true);
      }
      if (e.PropertyName == "IsVisible" || e.PropertyName == "ShowHeader")
      {
        this.ViewElement.RowLayout.InvalidateLayout();
        this.ViewElement.RowLayout.InvalidateRenderColumns();
        this.ViewElement.UpdateRows(true);
      }
      if (e.PropertyName == "PinPosition")
        this.UpdateAll();
      else if (e.PropertyName == "Text")
      {
        this.UpdateCellContent();
      }
      else
      {
        if (!(e.PropertyName == "RowSpan"))
          return;
        this.ViewElement.UpdateRows();
      }
    }

    private void UpdateOnRowExpanded(GridViewRowInfo row)
    {
      if (row is GridViewGroupRowInfo && this.GridViewElement.GroupExpandAnimationType != GridExpandAnimationType.None)
      {
        GridExpandAnimation expandAnimation = GridAnimationFactory.GetExpandAnimation(this);
        GridGroupHeaderRowElement rowElement = this.GetRowElement(row) as GridGroupHeaderRowElement;
        if (rowElement != null)
        {
          int num = this.VisualRows.IndexOf((GridRowElement) rowElement) + 1;
          if (num > 0 && num < this.VisualRows.Count)
          {
            float maxOffset = 50f;
            expandAnimation.UpdateViewNeeded += new EventHandler(this.ExpandAnimation_UpdateViewNeeded);
            if (row.IsExpanded)
            {
              float groupRowMaxOffset = this.GetGroupRowMaxOffset(rowElement, num);
              expandAnimation.Expand(row, groupRowMaxOffset, num);
              return;
            }
            expandAnimation.Collapse(row, maxOffset, num);
            return;
          }
        }
      }
      this.UpdateOnRowExpandedCore(row);
    }

    private float GetGroupRowMaxOffset(
      GridGroupHeaderRowElement groupRowElement,
      int visualRowIndex)
    {
      float num = 50f;
      for (int index = visualRowIndex; index < this.VisualRows.Count; ++index)
      {
        GridRowElement visualRow = this.VisualRows[index];
        if (visualRow is GridGroupHeaderRowElement || index == this.VisualRows.Count - 1)
        {
          num = (float) (visualRow.BoundingRectangle.Top - groupRowElement.BoundingRectangle.Top);
          break;
        }
      }
      return num;
    }

    private void UpdateOnRowExpandedCore(GridViewRowInfo row)
    {
      int num = 0;
      GridViewHierarchyRowInfo hierarchyRowInfo1 = row as GridViewHierarchyRowInfo;
      GridViewGroupRowInfo viewGroupRowInfo = row as GridViewGroupRowInfo;
      if (viewGroupRowInfo != null)
      {
        for (GridViewRowInfo parent = viewGroupRowInfo.Parent as GridViewRowInfo; parent != null; parent = parent.Parent as GridViewRowInfo)
        {
          GridViewHierarchyRowInfo hierarchyRowInfo2 = parent as GridViewHierarchyRowInfo;
          if (hierarchyRowInfo2 != null)
            hierarchyRowInfo2.ChildRow.ActualHeight = -1;
        }
      }
      if (hierarchyRowInfo1 != null && hierarchyRowInfo1.ChildRow != null && row.IsExpanded)
      {
        num = hierarchyRowInfo1.ChildRow.Height;
        GridTraverser traverser = (GridTraverser) this.rowScroller.Traverser;
        GridTraverser.GridTraverserPosition position = traverser.Position;
        while (traverser.MoveNext())
        {
          if (traverser.Current == hierarchyRowInfo1)
          {
            traverser.MoveNext();
            hierarchyRowInfo1.ChildRow.IsLastRow = !traverser.MoveNext();
            break;
          }
        }
        traverser.Position = position;
      }
      this.ViewElement.UpdateRows();
      this.GridViewElement.UpdateLayout();
      if (hierarchyRowInfo1 != null && hierarchyRowInfo1.ChildRow != null && hierarchyRowInfo1.IsExpanded)
      {
        GridRowElement rowElement = this.GetRowElement((GridViewRowInfo) hierarchyRowInfo1.ChildRow);
        if (rowElement != null && rowElement.Size.Height == num)
          this.RowScroller.UpdateScrollRange(this.RowScroller.Scrollbar.Maximum + num, true);
      }
      if ((hierarchyRowInfo1 == null || !row.IsExpanded || this.ViewTemplate.IsSelfReference) && this.viewInfo.ParentRow == null)
      {
        if (hierarchyRowInfo1 != null && hierarchyRowInfo1.ChildRow != null)
          hierarchyRowInfo1.ChildRow.ActualHeight = -1;
        this.RowScroller.UpdateScrollRange();
      }
      else
      {
        if (hierarchyRowInfo1 != null)
        {
          GridViewHierarchyRowInfo parentRow = hierarchyRowInfo1.ViewInfo.ParentRow;
          List<GridViewInfo> gridViewInfoList = new List<GridViewInfo>();
          for (; parentRow != null; parentRow = parentRow.ViewInfo.ParentRow)
          {
            parentRow.ChildRow.ActualHeight = -1;
            gridViewInfoList.Add(parentRow.ViewInfo);
          }
          while (gridViewInfoList.Count > 0)
          {
            GridViewInfo viewInfo = gridViewInfoList[0];
            gridViewInfoList.RemoveAt(0);
            (this.GridViewElement.GetRowView(viewInfo) as GridTableElement)?.ViewElement.UpdateRows();
          }
        }
        this.RowScroller.UpdateScrollRange();
      }
    }

    private void UpdateOnFilteringChanged()
    {
      bool cancel = GridViewSearchRowInfo.Cancel;
      GridViewSearchRowInfo.Cancel = true;
      this.RowScroller.UpdateScrollRange();
      if (this.CurrentCell is GridFilterCellElement && this.GridViewElement.IsInEditMode && (this.ViewInfo.TableFilteringRow.RowPosition == SystemRowPosition.Bottom && this.ViewInfo.TableFilteringRow.PinPosition == PinnedRowPosition.None))
      {
        int num = this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1;
        if (num >= this.VScrollBar.Minimum && num <= this.VScrollBar.Maximum)
          this.VScrollBar.Value = num;
      }
      this.ViewElement.UpdateRows();
      this.UpdateCellContent((GridViewRowInfo) this.ViewInfo.TableFilteringRow);
      this.UpdateCellContent((GridViewRowInfo) this.ViewInfo.TableHeaderRow);
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
      {
        if (this.ViewInfo.Rows.Count > 0 && column.IsVisible && !string.IsNullOrEmpty(column.Expression))
          this.UpdateCellContentByColumn(column);
      }
      GridViewHierarchyRowInfo parentRow = this.viewInfo.ParentRow;
      if (parentRow != null && !this.GridViewElement.UseScrollbarsInHierarchy)
        parentRow.ChildRow.ActualHeight = -1;
      GridViewSearchRowInfo.Cancel = cancel;
    }

    private void UpdateWhenGroupingChanged()
    {
      if (this.rowScroller.Scrollbar.Minimum == 0 && this.rowScroller.Scrollbar.Maximum > 0)
        this.rowScroller.Scrollbar.Value = 0;
      this.RowScroller.UpdateScrollRange();
      this.ViewElement.UpdateRowsWhenColumnsChanged();
      GridViewHierarchyRowInfo parentRow = this.viewInfo.ParentRow;
      if (parentRow != null && !this.GridViewElement.UseScrollbarsInHierarchy)
        parentRow.ChildRow.Height = -1;
      if (parentRow == null)
        this.UpdateLayout();
      this.UpdateCellContent();
    }

    private void UpdateWhenPagingChanged()
    {
      this.ViewElement.UpdateRows();
      this.RowScroller.UpdateScrollRange();
    }

    private void UpdateWhenItemChanged(DataViewChangedEventArgs args)
    {
      GridViewRowInfo newItem = (GridViewRowInfo) args.NewItems[0];
      if (this.ViewTemplate.SortDescriptors.Count > 0 || this.ViewTemplate.GroupDescriptors.Count > 0 || this.ViewTemplate.DataView.Filter != null)
      {
        this.rowScroller.UpdateScrollRange();
        this.ViewElement.UpdateRows();
        this.UpdateCellContent();
      }
      else if (this.ViewTemplate.ContainsColumnExpression)
        this.UpdateCellContent();
      else
        this.UpdateCellContent(newItem);
      if (args.PropertyName == null)
        return;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
      {
        if (column.FieldName == args.PropertyName)
        {
          GridViewCheckBoxColumn viewCheckBoxColumn = column as GridViewCheckBoxColumn;
          if (viewCheckBoxColumn != null && viewCheckBoxColumn.EnableHeaderCheckBox)
            (this.GetCellElement((GridViewRowInfo) this.ViewInfo.TableHeaderRow, (GridViewColumn) viewCheckBoxColumn) as GridCheckBoxHeaderCellElement)?.SetCheckBoxState();
        }
      }
    }

    private void UpdateOnAdd(DataViewChangedEventArgs args)
    {
      GridViewRowInfo newItem = (GridViewRowInfo) args.NewItems[0];
      GridViewChildRowCollection childRows = this.ViewInfo.ChildRows;
      if (newItem.ViewInfo != this.ViewInfo)
        return;
      Predicate<GridViewRowInfo> filter = this.ViewTemplate.DataView.Filter;
      if (filter != null && !filter(newItem))
        return;
      if (this.RowScroller.Scrollbar.LargeChange == 0)
      {
        this.RowScroller.ClientSize = (SizeF) Size.Empty;
        this.ViewElement.InvalidateMeasure();
        this.ViewElement.UpdateLayout();
        this.rowScroller.UpdateScrollRange();
      }
      else
      {
        int scrollHeight = this.rowScroller.GetScrollHeight(newItem);
        int rowSpacing = this.RowSpacing;
        if (this.MasterTemplate != null && this.MasterTemplate.AutoExpandGroups && (newItem.Group != null && newItem.Group.ItemCount == 1))
        {
          scrollHeight += this.rowScroller.GetScrollHeight((GridViewRowInfo) newItem.Group.GroupRow);
          rowSpacing += this.RowSpacing;
        }
        if ((scrollHeight != 0 || this.ViewElement.RowLayout is TableViewRowLayout) && this.ViewTemplate.GroupDescriptors.Count <= 1 && (!this.MasterTemplate.EnablePaging || this.ViewInfo.ChildRows.Count < this.MasterTemplate.PageSize))
          this.rowScroller.UpdateScrollRange(this.rowScroller.Scrollbar.Maximum + scrollHeight + rowSpacing, true);
        else
          this.rowScroller.UpdateScrollRange();
      }
      this.ViewElement.UpdateRows();
      this.UpdateParentHeight();
      if (!this.ViewTemplate.ContainsColumnExpression)
        return;
      this.UpdateCellContent();
    }

    private void UpdateOnRemove(DataViewChangedEventArgs args)
    {
      this.rowScroller.UpdateScrollRange();
      this.ViewElement.UpdateRows();
      GridViewHierarchyRowInfo parentRow = this.viewInfo.ParentRow;
      if (parentRow != null && !this.GridViewElement.UseScrollbarsInHierarchy)
        parentRow.ChildRow.Height = -1;
      this.UpdateLayout();
      if (this.ViewTemplate != null && this.ViewTemplate.ContainsColumnExpression)
        this.UpdateCellContent();
      if (this.ViewInfo == null || this.ViewInfo.ChildRows.Count != 0)
        return;
      this.NotifyParentCell();
    }

    private void NotifyParentCell()
    {
      for (RadElement parent = this.Parent; parent != null; parent = parent.Parent)
      {
        GridDetailViewCellElement detailViewCellElement = parent as GridDetailViewCellElement;
        if (detailViewCellElement != null)
        {
          detailViewCellElement.UpdateTabItemsVisibility();
          break;
        }
      }
    }

    private void UpdateParentContainer(RadElement element)
    {
      for (; element != null; element = element.Parent)
      {
        ScrollableRowsContainerElement containerElement = element as ScrollableRowsContainerElement;
        if (containerElement != null)
        {
          this.UpdateParentContainer(element.Parent);
          containerElement.UpdateItems();
          break;
        }
      }
    }

    private void UpdateOnTemplatePropertyChanged(PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "AutoSizeColumnsMode")
      {
        this.SetScrollState();
        this.ViewElement.UpdateRows(true);
      }
      else if (e.PropertyName == "BottomPinnedRowsMode")
      {
        this.ViewElement.InvalidateArrange();
        this.ViewElement.UpdateLayout();
      }
      else if (e.PropertyName == "ShowFilterCellOperatorText")
        this.ViewInfo.TableFilteringRow.InvalidateRow();
      else if (e.PropertyName == "HorizontalScrollState" && this.ViewTemplate.AutoSizeColumnsMode != GridViewAutoSizeColumnsMode.Fill)
        this.columnScroller.ScrollState = this.ViewTemplate.HorizontalScrollState;
      else if (e.PropertyName == "VerticalScrollState" && this.ViewTemplate.AutoSizeColumnsMode != GridViewAutoSizeColumnsMode.Fill)
        this.SetScrollState();
      else if (e.PropertyName == "ShowColumnHeaders" || e.PropertyName == "ShowTotals" || (e.PropertyName == "ReadOnly" || e.PropertyName == "ShowFilteringRow") || (e.PropertyName == "EnableFiltering" || e.PropertyName == "AllowAddNewRow" || (e.PropertyName == "AddNewRowPosition" || e.PropertyName == "AllowSerachRow")) || e.PropertyName == "SearchRowPosition")
      {
        int num = (int) this.SetValue(GridTableElement.HasColumnHeadersProperty, (object) this.ViewTemplate.ShowColumnHeaders);
        this.ViewElement.UpdateRows();
        this.RowScroller.UpdateScrollRange();
      }
      else if (e.PropertyName == "SelfReferenceExpanderColumn")
      {
        this.ViewElement.ClearRows();
        this.ViewElement.UpdateRows();
      }
      else if (e.PropertyName == "ShowHeaderCellButtons")
      {
        this.ViewInfo.TableHeaderRow.InvalidateRow();
        this.UpdateView();
      }
      else if (e.PropertyName == "EnableAlternatingRowColor")
        this.UpdateRowInfo();
      else if (e.PropertyName == "SelectionMode" || e.PropertyName == "MultiSelect")
      {
        this.GridViewElement.Navigator.ClearSelection();
        this.UpdateRowInfo();
      }
      else if (e.PropertyName == "ShowGroupedColumns" || e.PropertyName == "ShowRowHeaderColumn")
      {
        this.ViewElement.UpdateRowsWhenColumnsChanged();
      }
      else
      {
        if (!(e.PropertyName == "ShowChildViewTabsAlways") && !(e.PropertyName == "SelfReferenceExpanderColumn"))
          return;
        this.ViewElement.ClearRows();
        this.ViewElement.UpdateRows();
      }
    }

    private void UpdateCellContent()
    {
      this.UpdateCellContentByColumn((GridViewColumn) null);
    }

    private void UpdateCellContentByColumn(GridViewColumn column)
    {
      foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.VisualRows)
      {
        foreach (GridCellElement visualCell in visualRow.VisualCells)
        {
          if (visualCell.ColumnInfo == column || column == null)
          {
            visualCell.SetContent();
            visualCell.UpdateInfo();
            if (column != null && visualRow.RowInfo != null && (visualRow is GridDataRowElement && visualRow.IsInValidState(true)))
              this.GridViewElement.CallRowFormatting((object) visualRow, new RowFormattingEventArgs(visualRow));
          }
        }
        if (column == null && visualRow.RowInfo != null && (visualRow is GridDataRowElement && visualRow.IsInValidState(true)))
          this.GridViewElement.CallRowFormatting((object) visualRow, new RowFormattingEventArgs(visualRow));
      }
      this.Invalidate();
    }

    private void UpdateCellContent(GridViewRowInfo rowInfo)
    {
      GridRowElement rowElement = this.GetRowElement(rowInfo);
      if (rowElement == null)
        return;
      rowElement.UpdateContent();
      rowElement.UpdateInfo();
      if (this.GridViewElement.AutoSizeRows)
        rowElement.InvalidateMeasure(true);
      this.Invalidate();
    }

    private void UpdateCellInfo()
    {
      foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.VisualRows)
      {
        foreach (GridCellElement visualCell in visualRow.VisualCells)
          visualCell.UpdateInfo();
      }
      this.Invalidate();
    }

    private void UpdateRowInfo()
    {
      foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.VisualRows)
        visualRow.UpdateInfo();
    }

    private void UpdateCellLayout()
    {
      for (int index = 0; index < this.VisualRows.Count; ++index)
        this.VisualRows[index].UpdateCells();
    }

    private void UpdateElementProviders()
    {
      this.ViewElement.TopPinnedRows.ElementProvider = this.RowElementProvider;
      this.ViewElement.BottomPinnedRows.ElementProvider = this.RowElementProvider;
      this.ViewElement.ScrollableRows.ElementProvider = this.RowElementProvider;
      this.ViewElement.ClearRows();
      this.ViewElement.UpdateRows();
    }

    private void UpdateParentHeight()
    {
      for (GridViewHierarchyRowInfo hierarchyRowInfo = this.viewInfo.ParentRow; hierarchyRowInfo != null; hierarchyRowInfo = hierarchyRowInfo.Parent as GridViewHierarchyRowInfo)
      {
        hierarchyRowInfo.ChildRow.ActualHeight = -1;
        (this.GridViewElement.GetRowView(hierarchyRowInfo.ViewInfo) as GridTableElement)?.ViewElement.UpdateRows();
      }
    }

    private void UpdateAll()
    {
      if (this.GridViewElement.IsInEditMode && this.GridViewElement.CurrentCell != null && this.GridViewElement.CurrentCell.ViewInfo == this.ViewInfo)
        this.GridViewElement.CloseEditor();
      this.UpdateNoDataText();
      this.SetScrollState();
      this.UpdateColumnsDpiScaleFactor();
      this.ViewElement.RowLayout.InvalidateRenderColumns();
      this.RowScroller.ScrollOffset = 0;
      if (this.ViewInfo.ChildRows.Count == 0)
      {
        if (this.rowScroller.Scrollbar.Minimum == 0 && this.rowScroller.Scrollbar.Maximum > 0)
          this.RowScroller.Scrollbar.Value = 0;
        this.RowScroller.Scrollbar.Maximum = 0;
        this.RowScroller.Scrollbar.LargeChange = 0;
        this.RowScroller.Scrollbar.SmallChange = 0;
        this.ViewElement.UpdateLayout();
      }
      else
      {
        if (this.RowScroller.Scrollbar.LargeChange == 0)
        {
          this.RowScroller.ClientSize = (SizeF) Size.Empty;
          this.ViewElement.InvalidateMeasure();
          this.ViewElement.UpdateLayout();
        }
        this.RowScroller.UpdateScrollRange();
      }
      this.ViewElement.ClearRows();
      this.ViewElement.UpdateRowsWhenColumnsChanged();
      this.bestFitHelper.ProcessRequests();
      if (this.VScrollBar.Minimum == 0 && this.VScrollBar.Maximum > 0)
        this.VScrollBar.Value = 0;
      if (this.HScrollBar.Minimum != 0 || this.HScrollBar.Maximum <= 0)
        return;
      this.HScrollBar.Value = 0;
    }

    private void SetScrollState()
    {
      if (this.ViewInfo.ParentRow != null && !this.GridViewElement.UseScrollbarsInHierarchy)
        this.rowScroller.ScrollState = ScrollState.AlwaysHide;
      else
        this.rowScroller.ScrollState = this.ViewTemplate.VerticalScrollState;
      this.ColumnScroller.ScrollState = this.ViewTemplate.HorizontalScrollState;
    }

    private void EnsureRowVisibleCore(GridViewRowInfo rowInfo)
    {
      if (this.ViewElement.ScrollableRows.Children.Count == 0 || rowInfo is GridViewDetailsRowInfo)
        return;
      if (rowInfo.ViewInfo == this.viewInfo)
      {
        if (this.EnsureRowVisibleInTableElement(this, this.GetRowElement(rowInfo), rowInfo))
          ;
      }
      else
      {
        GridTableElement rowView1 = (GridTableElement) this.GridViewElement.GetRowView(rowInfo.ViewInfo);
        if (rowView1 != null)
        {
          GridRowElement rowElement = rowView1.GetRowElement(rowInfo);
          if (this.EnsureRowVisibleInTableElement(rowView1, rowElement, rowInfo))
            ;
        }
        else
        {
          GridTraverser gridTraverser = new GridTraverser((GridTraverser) this.RowScroller.Traverser);
          gridTraverser.ProcessHierarchy = true;
          gridTraverser.TraversalMode = GridTraverser.TraversalModes.AllRows;
          int row1 = gridTraverser.GoToRow(rowInfo);
          gridTraverser.Reset();
          int row2 = gridTraverser.GoToRow(((GridRowElement) this.ViewElement.ScrollableRows.Children[0]).RowInfo);
          int num1 = this.VScrollBar.Value;
          if (row1 < row2)
            this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - this.VScrollBar.SmallChange);
          else
            this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + this.VScrollBar.SmallChange);
          GridTableElement rowView2 = (GridTableElement) this.GridViewElement.GetRowView(rowInfo.ViewInfo);
          if (rowView2 != null)
          {
            GridRowElement rowElement = rowView2.GetRowElement(rowInfo);
            while (rowElement == null)
            {
              int num2 = this.VScrollBar.Value;
              if (row1 < row2)
                this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - this.VScrollBar.SmallChange);
              else
                this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + this.VScrollBar.SmallChange);
              rowElement = rowView2.GetRowElement(rowInfo);
              if (num2 == this.VScrollBar.Value)
                break;
            }
            if (this.EnsureRowVisibleInTableElement(rowView2, rowElement, rowInfo))
              ;
          }
          else
          {
            if (rowInfo.ViewInfo == null || rowInfo.ViewInfo.ParentRow == null)
              return;
            this.EnsureRowVisibleCore((GridViewRowInfo) rowInfo.ViewInfo.ParentRow);
            (this.GetRowElement((GridViewRowInfo) rowInfo.ViewInfo.ParentRow.ChildRow) as GridDetailViewRowElement)?.ContentCell.SetContent();
            this.UpdateLayout();
            this.VScrollBar.Value = num1;
          }
        }
      }
    }

    private bool EnsureRowVisibleInTableElement(
      GridTableElement tableElement,
      GridRowElement rowElement,
      GridViewRowInfo rowInfo)
    {
      if (rowInfo.IsPinned)
        return this.EnsurePinnedRowVisibleInTableElement(tableElement, rowElement, rowInfo);
      if (rowElement != null)
      {
        int num1 = this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y - rowElement.ControlBoundingRectangle.Y;
        if (num1 > 0)
        {
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - num1);
        }
        else
        {
          int num2 = rowElement.ControlBoundingRectangle.Bottom - tableElement.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom;
          if (num2 > 0)
            this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num2);
          int num3 = rowElement.ControlBoundingRectangle.Bottom - this.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom;
          if (num3 > 0)
            this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num3);
        }
        return true;
      }
      GridTraverser gridTraverser1 = new GridTraverser((GridTraverser) this.RowScroller.Traverser);
      if (gridTraverser1.Current == rowInfo)
      {
        int num1 = (int) tableElement.RowElementProvider.GetElementSize(rowInfo).Height + tableElement.RowSpacing;
        int num2 = this.VScrollBar.Value;
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - num1);
        if (num2 != this.VScrollBar.Value)
        {
          rowElement = tableElement.GetRowElement(rowInfo);
          this.EnsureRowVisibleInTableElement(tableElement, rowElement, rowInfo);
        }
        return true;
      }
      if (tableElement.ViewElement.ScrollableRows.Children.Count > 0)
      {
        rowElement = (GridRowElement) tableElement.ViewElement.ScrollableRows.Children[tableElement.ViewElement.ScrollableRows.Children.Count - 1];
      }
      else
      {
        GridTraverser gridTraverser2 = new GridTraverser((GridTraverser) this.RowScroller.Traverser);
        gridTraverser2.ProcessHierarchy = true;
        gridTraverser2.TraversalMode = GridTraverser.TraversalModes.AllRows;
        int row1 = gridTraverser2.GoToRow(rowInfo);
        gridTraverser2.Reset();
        int row2 = gridTraverser2.GoToRow(((GridRowElement) this.ViewElement.ScrollableRows.Children[0]).RowInfo);
        if (row1 < row2)
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - this.VScrollBar.SmallChange);
        else
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + this.VScrollBar.SmallChange);
        if (tableElement.ViewElement.ScrollableRows.Children.Count == 0)
          return false;
        rowElement = (GridRowElement) tableElement.ViewElement.ScrollableRows.Children[tableElement.ViewElement.ScrollableRows.Children.Count - 1];
      }
      if (rowInfo.ViewInfo != this.viewInfo)
        gridTraverser1.ProcessHierarchy = true;
      do
        ;
      while (gridTraverser1.MoveNext() && rowElement.RowInfo != gridTraverser1.Current);
      gridTraverser1.MoveNext();
      if (gridTraverser1.Current != rowInfo)
        return false;
      this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + ((int) tableElement.RowElementProvider.GetElementSize(rowInfo).Height + tableElement.RowSpacing));
      return true;
    }

    private bool EnsurePinnedRowVisibleInTableElement(
      GridTableElement tableElement,
      GridRowElement rowElement,
      GridViewRowInfo rowInfo)
    {
      if (rowElement == null)
      {
        GridTraverser gridTraverser = new GridTraverser((GridTraverser) this.RowScroller.Traverser);
        gridTraverser.ProcessHierarchy = true;
        gridTraverser.TraversalMode = GridTraverser.TraversalModes.AllRows;
        int row1 = gridTraverser.GoToRow(rowInfo);
        gridTraverser.Reset();
        int row2 = gridTraverser.GoToRow(((GridRowElement) this.ViewElement.ScrollableRows.Children[0]).RowInfo);
        if (row1 < row2)
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - this.VScrollBar.SmallChange);
        else
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + this.VScrollBar.SmallChange);
        rowElement = this.GetChildRowElement(rowInfo);
      }
      if (rowElement == null)
        return false;
      int num1 = this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y - rowElement.ControlBoundingRectangle.Y;
      if (num1 > 0)
      {
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - num1);
      }
      else
      {
        int num2 = rowElement.ControlBoundingRectangle.Bottom - tableElement.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom;
        if (num2 > 0)
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num2);
      }
      return true;
    }

    public void BestFitColumn(GridViewColumn column)
    {
      this.bestFitHelper.BestFitColumn(column);
      this.bestFitHelper.ProcessRequests();
    }

    public void BestFitColumns()
    {
      this.bestFitHelper.BestFitColumns();
      this.bestFitHelper.ProcessRequests();
    }

    public void BestFitColumns(BestFitColumnMode mode)
    {
      this.bestFitHelper.BestFitColumns(mode);
      this.bestFitHelper.ProcessRequests();
    }

    internal bool CanBestFit()
    {
      if (this.CanExecuteLayoutOperation())
        return this.updateSuspendedCount == 0;
      return false;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.forcedDesiredSize != SizeF.Empty)
      {
        base.MeasureOverride(this.forcedDesiredSize);
        return this.forcedDesiredSize;
      }
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.cachedAvailableSize != availableSize)
      {
        this.cachedAvailableSize = availableSize;
        if (this.ViewTemplate.ViewDefinition is HtmlViewDefinition)
          this.ViewElement.UpdateRows(true);
      }
      bool flag = this.ViewInfo != null && this.ViewInfo.ParentRow == null && this.ElementTree.Control is RadGridView && this.ElementTree.Control.AutoSize;
      if (!float.IsInfinity(availableSize.Width) && !flag)
        sizeF.Width = !this.StretchHorizontally ? Math.Min(availableSize.Width, sizeF.Width) : availableSize.Width;
      if (!float.IsInfinity(availableSize.Height) && this.StretchVertically && !flag)
        sizeF.Height = availableSize.Height;
      if (this.scheduleUpdateScrollRange && availableSize != SizeF.Empty)
      {
        this.scheduleUpdateScrollRange = false;
        this.RowScroller.UpdateScrollRange();
      }
      return sizeF;
    }

    protected override void ArrangeVScrollBar(
      ref RectangleF viewElementRect,
      RectangleF hscrollBarRect,
      RectangleF clientRect)
    {
      if (this.ExtendVerticalScrollBar && this.ViewInfo != null && (this.ViewInfo.TableHeaderRow.PinPosition == PinnedRowPosition.Top && this.ViewInfo.TableHeaderRow.IsVisible) && this.ViewTemplate.ShowColumnHeaders)
      {
        int num = (int) this.RowScroller.ElementProvider.GetElementSize((GridViewRowInfo) this.ViewInfo.TableHeaderRow).Height + this.RowSpacing;
        clientRect.Height -= (float) num;
        clientRect.Y += (float) num;
      }
      base.ArrangeVScrollBar(ref viewElementRect, hscrollBarRect, clientRect);
    }

    private void RowScroller_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      this.MasterTemplate.EventDispatcher.RaiseEvent<ToolTipTextNeededEventArgs>(EventDispatcher.ToolTipTextNeeded, sender, e);
    }

    private void OnSelectionChanged(object sender, EventArgs e)
    {
      if (this.MasterTemplate.SelectionMode != GridViewSelectionMode.CellSelect)
        return;
      this.UpdateCellInfo();
    }

    private void GridElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (this.viewInfo == null)
        return;
      if (e.PropertyName == "ShowNoDataText")
        this.UpdateNoDataText();
      else if (e.PropertyName == "ReadOnly")
        this.ViewElement.UpdateRows();
      else if (e.PropertyName == "ShowRowErrors" || e.PropertyName == "ShowCellErrors")
        this.UpdateRowInfo();
      else if (e.PropertyName == "AutoSizeRows")
      {
        if (!this.GridViewElement.AutoSizeRows)
        {
          GridTraverser gridTraverser = new GridTraverser((IHierarchicalRow) this.ViewTemplate);
          while (gridTraverser.MoveNext())
          {
            gridTraverser.Current.SuspendPropertyNotifications();
            gridTraverser.Current.Height = -1;
            gridTraverser.Current.ResumePropertyNotifications();
          }
        }
        this.RowScroller.UpdateScrollRange();
        this.ViewElement.UpdateRows();
      }
      else
      {
        if (!(e.PropertyName == "UseScrollbarsInHierarchy"))
          return;
        GridTraverser gridTraverser = new GridTraverser(this.ViewInfo);
        while (gridTraverser.MoveNext())
        {
          GridViewDetailsRowInfo current = gridTraverser.Current as GridViewDetailsRowInfo;
          if (current != null)
          {
            current.SuspendPropertyNotifications();
            current.Height = -1;
            current.ResumePropertyNotifications();
          }
        }
        this.ViewElement.ClearRows();
        this.ViewElement.UpdateRows();
      }
    }

    private void Control_MouseLeave(object sender, EventArgs e)
    {
      this.HoveredRow = (GridRowElement) null;
    }

    private void HScrollBar_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.VisibilityProperty || this.viewInfo == null)
        return;
      GridViewHierarchyRowInfo parentRow = this.viewInfo.ParentRow;
      if (parentRow == null || this.GridViewElement.UseScrollbarsInHierarchy)
        return;
      parentRow.ChildRow.Height = -1;
    }

    public override void RemoveStylePropertySetting(IPropertySetting setting)
    {
      if (this.FindAncestor<GridDetailViewCellElement>() == null)
        this.GridViewElement.CloseEditor();
      base.RemoveStylePropertySetting(setting);
    }

    private void OnThemeName_Changed(object source, ThemeNameChangedEventArgs args)
    {
      this.ViewElement.ClearRows();
    }

    private void RowToScrollTraverser_RowVisible(object sender, RowEnumeratorEventArgs e)
    {
      e.ProcessRow = !(e.Row is GridViewSystemRowInfo);
    }

    private void ExpandAnimation_UpdateViewNeeded(object sender, EventArgs e)
    {
      this.UpdateOnRowExpandedCore((GridViewRowInfo) null);
      (sender as GridExpandAnimation).UpdateViewNeeded -= new EventHandler(this.ExpandAnimation_UpdateViewNeeded);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.ViewInfo == null)
        return;
      if (e.Property == GridTableElement.RowSpacingProperty)
      {
        this.rowScroller.ItemSpacing = (int) e.NewValue;
        this.ViewElement.ScrollableRows.ItemSpacing = (int) e.NewValue;
        this.ViewElement.ElementSpacing = (int) e.NewValue;
        if (this.Size == Size.Empty)
        {
          this.scheduleUpdateScrollRange = true;
        }
        else
        {
          this.ViewElement.UpdateRows();
          this.UpdateLayout();
          this.RowScroller.UpdateScrollRange();
        }
      }
      else if (e.Property == GridTableElement.CellSpacingProperty)
      {
        this.columnScroller.ItemSpacing = (int) e.NewValue;
        this.ViewElement.UpdateRows(true);
        this.UpdateLayout();
        this.RowScroller.UpdateScrollRange();
      }
      else if (e.Property == GridTableElement.RowHeaderColumnWidthProperty)
      {
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.ViewElement.RowLayout.RenderColumns)
        {
          GridViewRowHeaderColumn viewRowHeaderColumn = renderColumn as GridViewRowHeaderColumn;
          if (viewRowHeaderColumn != null)
          {
            viewRowHeaderColumn.Width = (int) e.NewValue;
            break;
          }
        }
        this.ViewElement.UpdateRows(true);
      }
      else if (e.Property == GridTableElement.GroupIndentProperty)
        this.ViewElement.UpdateRows(true);
      else if (e.Property == GridTableElement.RowHeightProperty)
      {
        this.rowScroller.ItemHeight = (int) e.NewValue;
        this.ViewElement.UpdateRows();
        this.RowScroller.UpdateScrollRange();
      }
      else if (e.Property == GridTableElement.TableHeaderHeightProperty || e.Property == GridTableElement.GroupHeaderHeightProperty || e.Property == GridTableElement.FilterRowHeightProperty)
      {
        this.ViewElement.UpdateRows();
        this.RowScroller.UpdateScrollRange();
      }
      else if (e.Property == GridTableElement.AlternatingRowColorProperty)
      {
        this.UpdateRowInfo();
      }
      else
      {
        if (e.Property != RadElement.RightToLeftProperty && e.Property != GridTableElement.ShowSelfReferenceLinesProperty)
          return;
        this.UpdateAll();
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.ElementTree.Control.MouseLeave += new EventHandler(this.Control_MouseLeave);
      this.ElementTree.ComponentTreeHandler.ThemeNameChanged += new ThemeNameChangedEventHandler(this.OnThemeName_Changed);
      if (this.ViewTemplate == null)
        return;
      int num = (int) this.SetValue(GridTableElement.HasColumnHeadersProperty, (object) this.ViewTemplate.ShowColumnHeaders);
    }

    private void ScrollToRowCore(GridViewRowInfo rowInfo, bool ensureVisible)
    {
      if (!this.GridViewElement.UseScrollbarsInHierarchy && this.viewInfo.ParentRow != null)
      {
        if (ensureVisible)
          this.GridViewElement.TableElement.EnsureRowVisible(rowInfo);
        else
          this.GridViewElement.TableElement.ScrollToRow(rowInfo);
      }
      else
      {
        if (!this.IsInValidState(true) || this.VScrollBar.LargeChange == 0)
          return;
        RadControl control = this.ElementTree.Control as RadControl;
        control?.SuspendUpdate();
        int newValue = this.VScrollBar.Value;
        GridRowElement childRowElement = this.GetChildRowElement(rowInfo);
        if (childRowElement == null)
          this.SetScrollValue(this.VScrollBar, 0);
        if (childRowElement == null && this.PageViewMode == PageViewMode.ExplorerBar)
        {
          control?.ResumeUpdate();
        }
        else
        {
          while (this.VScrollBar.Value < this.VScrollBar.Maximum)
          {
            if (childRowElement == null)
              childRowElement = this.GetChildRowElement(rowInfo);
            if (childRowElement != null)
            {
              this.ScrollToPartiallyVisibleRow(childRowElement, ensureVisible);
              this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + childRowElement.BoundingRectangle.Y);
              break;
            }
            bool flag = this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + this.VScrollBar.SmallChange);
            if (this.VScrollBar.Value >= this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1 && !flag)
            {
              this.SetScrollValue(this.VScrollBar, newValue);
              break;
            }
          }
          control?.ResumeUpdate();
        }
      }
    }

    private void ScrollToPartiallyVisibleRow(GridRowElement rowElement, bool ensureVisible)
    {
      while (rowElement.ControlBoundingRectangle.Y < this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y && rowElement.ControlBoundingRectangle.Bottom > this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y || rowElement.ControlBoundingRectangle.Y < this.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom && rowElement.ControlBoundingRectangle.Bottom > this.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom)
      {
        if (rowElement.ControlBoundingRectangle.Y < this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y && rowElement.ControlBoundingRectangle.Bottom > this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y)
        {
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - (this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y - rowElement.ControlBoundingRectangle.Y));
          break;
        }
        if (rowElement.ControlBoundingRectangle.Y < this.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom && rowElement.ControlBoundingRectangle.Bottom > this.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom)
        {
          int num = rowElement.ControlBoundingRectangle.Top - this.ViewElement.ScrollableRows.ControlBoundingRectangle.Top;
          if (rowElement.ControlBoundingRectangle.Bottom > this.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom)
            num -= rowElement.BoundingRectangle.Y;
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num);
          break;
        }
        int num1;
        if (ensureVisible)
        {
          num1 = rowElement.ControlBoundingRectangle.Bottom - this.ViewElement.ScrollableRows.ControlBoundingRectangle.Bottom;
          if (num1 < 0)
            break;
        }
        else
        {
          num1 = rowElement.ControlBoundingRectangle.Y - this.ViewElement.ScrollableRows.ControlBoundingRectangle.Y;
          if (num1 < 0)
            num1 = 0;
        }
        bool flag = this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num1);
        if (this.VScrollBar.Value >= this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1 || !flag)
          break;
      }
    }

    private bool SetScrollValue(RadScrollBarElement scrollbar, int newValue)
    {
      int maximum = this.VScrollBar.Maximum;
      if (newValue > scrollbar.Maximum - scrollbar.LargeChange + 1)
        newValue = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (newValue < scrollbar.Minimum)
        newValue = scrollbar.Minimum;
      scrollbar.Value = newValue;
      this.UpdateLayout();
      return maximum != this.VScrollBar.Maximum;
    }

    private GridRowElement GetChildRowElement(GridViewRowInfo rowInfo)
    {
      if (rowInfo.ViewInfo == this.ViewInfo)
        return this.GetRowElement(rowInfo);
      return (this.GridViewElement.GetRowView(rowInfo.ViewInfo) as GridTableElement)?.GetRowElement(rowInfo);
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      if (args.IsBegin && (this.RowScroller.Scrollbar.ControlBoundingRectangle.Contains(args.Location) || this.ColumnScroller.Scrollbar.ControlBoundingRectangle.Contains(args.Location) || this.ViewElement.TopPinnedRows.ControlBoundingRectangle.Contains(args.Location)))
        return;
      bool flag = this.GridViewElement.CurrentRow == null && this.GridViewElement.Template == this.ViewTemplate;
      if (!flag)
      {
        foreach (IRowView rowView in this.GridViewElement.GetRowViews(this.GridViewElement.CurrentRow.ViewInfo))
          flag |= rowView == this;
      }
      if (!flag)
        return;
      this.GridViewElement.EndEdit();
      int num1 = this.RowScroller.Scrollbar.Value - args.Offset.Height;
      if (num1 > this.RowScroller.Scrollbar.Maximum - this.RowScroller.Scrollbar.LargeChange + 1)
        num1 = this.RowScroller.Scrollbar.Maximum - this.RowScroller.Scrollbar.LargeChange + 1;
      if (num1 < this.RowScroller.Scrollbar.Minimum)
        num1 = this.RowScroller.Scrollbar.Minimum;
      RadScrollBarElement scrollbar1 = this.RowScroller.Scrollbar;
      if (num1 > scrollbar1.Maximum)
        num1 = scrollbar1.Maximum;
      else if (num1 < 0)
        num1 = 0;
      this.RowScroller.Scrollbar.Value = num1;
      int num2 = this.ColumnScroller.Scrollbar.Value - args.Offset.Width;
      if (num2 > this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1)
        num2 = this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1;
      if (num2 < this.ColumnScroller.Scrollbar.Minimum)
        num2 = this.ColumnScroller.Scrollbar.Minimum;
      RadScrollBarElement scrollbar2 = this.ColumnScroller.Scrollbar;
      if (num2 > scrollbar2.Maximum)
        num2 = scrollbar2.Maximum;
      else if (num2 < 0)
        num2 = 0;
      this.ColumnScroller.Scrollbar.Value = num2;
      args.Handled = true;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.UpdateColumnsDpiScaleFactor();
    }

    protected virtual void UpdateColumnsDpiScaleFactor()
    {
      if (this.ViewTemplate == null)
        return;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
        column.DpiScale = this.DpiScaleFactor;
    }
  }
}
