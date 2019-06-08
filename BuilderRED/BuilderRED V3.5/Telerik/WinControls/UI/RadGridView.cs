// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGridView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI.Data;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadGridViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [TelerikToolboxCategory("Data Controls")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Displays data in tabular format, providing multi-level hierarchy, sorting, filtering, grouping and more")]
  [ComplexBindingProperties("DataSource", "DataMember")]
  public class RadGridView : RadControl, IPrintable
  {
    private bool? disableMouseEventsState = new bool?();
    private const int WM_CONTEXTMENU = 123;
    private RadGridViewElement gridViewElement;
    private ComponentXmlSerializationInfo xmlSerializationInfo;
    private bool isDisposing;
    private bool enableKineticScrolling;
    private bool mouseDownOnScrollBar;
    private Point mouseDownLocation;
    private GridPrintStyle printStyle;
    private IPrintSettingsDialogFactory printSettingsDialogFactory;
    private GridViewRowInfo currentRowCache;
    private int vScrollBarValue;
    private int hScrollBarValue;
    private bool pagingState;
    private int currentPageIndex;
    private Dictionary<GridViewColumn, int> columnWidths;

    public RadGridView()
    {
      this.PrintStyle = new GridPrintStyle();
      this.PrintStyle.GridView = this;
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
      this.EnableGesture(GestureType.Zoom);
    }

    protected override void Dispose(bool disposing)
    {
      this.isDisposing = true;
      base.Dispose(disposing);
      this.MasterTemplate.Dispose();
      this.UnWireEvents();
    }

    protected internal virtual bool IsDisposing
    {
      get
      {
        return this.isDisposing;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.gridViewElement = this.CreateGridViewElement();
      this.gridViewElement.Template.Owner = this;
      this.WireEvents();
      parent.Children.Add((RadElement) this.gridViewElement);
    }

    protected virtual RadGridViewElement CreateGridViewElement()
    {
      return new RadGridViewElement();
    }

    protected virtual void WireEvents()
    {
      this.gridViewElement.CreateCell += new GridViewCreateCellEventHandler(this.gridElement_CreateCell);
      this.gridViewElement.CreateRow += new GridViewCreateRowEventHandler(this.gridElement_CreateRow);
      this.gridViewElement.RowFormatting += new RowFormattingEventHandler(this.gridElement_RowFormatting);
      this.gridViewElement.ViewRowFormatting += new RowFormattingEventHandler(this.gridElement_ViewRowFormatting);
      this.gridViewElement.CellFormatting += new CellFormattingEventHandler(this.gridElement_CellFormatting);
      this.gridViewElement.ViewCellFormatting += new CellFormattingEventHandler(this.gridElement_ViewCellFormatting);
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangingEventArgs>(EventDispatcher.FilterChangingEvent, new EventHandler<GridViewCollectionChangingEventArgs>(this.OnFilterChanging));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangedEventArgs>(EventDispatcher.FilterChangedEvent, new EventHandler<GridViewCollectionChangedEventArgs>(this.OnFilterChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCustomFilteringEventArgs>(EventDispatcher.CustomFiltering, new EventHandler<GridViewCustomFilteringEventArgs>(this.OnCustomFiltering));
      this.MasterTemplate.EventDispatcher.AddListener<FilterExpressionChangedEventArgs>(EventDispatcher.FilterExpressionChanged, new EventHandler<FilterExpressionChangedEventArgs>(this.OnFilterExpressionChanged));
      this.MasterTemplate.EventDispatcher.AddListener<FilterPopupRequiredEventArgs>(EventDispatcher.FilterPopupRequired, new EventHandler<FilterPopupRequiredEventArgs>(this.OnFilterPopupRequired));
      this.MasterTemplate.EventDispatcher.AddListener<FilterPopupInitializedEventArgs>(EventDispatcher.FilterPopupInitialized, new EventHandler<FilterPopupInitializedEventArgs>(this.OnFilterPopupInitialized));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCreateCompositeFilterDialogEventArgs>(EventDispatcher.CreateCompositeFilterDialog, new EventHandler<GridViewCreateCompositeFilterDialogEventArgs>(this.OnCreateCompositeFilterDialog));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangingEventArgs>(EventDispatcher.SortChangingEvent, new EventHandler<GridViewCollectionChangingEventArgs>(this.OnSortChanging));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangedEventArgs>(EventDispatcher.SortChangedEvent, new EventHandler<GridViewCollectionChangedEventArgs>(this.OnSortChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCustomSortingEventArgs>(EventDispatcher.CustomSorting, new EventHandler<GridViewCustomSortingEventArgs>(this.OnCustomSorting));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangingEventArgs>(EventDispatcher.GroupByChanging, new EventHandler<GridViewCollectionChangingEventArgs>(this.OnGroupByChanging));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangedEventArgs>(EventDispatcher.GroupByChanged, new EventHandler<GridViewCollectionChangedEventArgs>(this.OnGroupByChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCustomGroupingEventArgs>(EventDispatcher.CustomGrouping, new EventHandler<GridViewCustomGroupingEventArgs>(this.OnCustomGrouping));
      this.MasterTemplate.EventDispatcher.AddListener<GroupExpandingEventArgs>(EventDispatcher.GroupExpanding, new EventHandler<GroupExpandingEventArgs>(this.OnGroupExpanding));
      this.MasterTemplate.EventDispatcher.AddListener<GroupExpandedEventArgs>(EventDispatcher.GroupExpanded, new EventHandler<GroupExpandedEventArgs>(this.OnGroupExpanded));
      this.MasterTemplate.EventDispatcher.AddListener<GroupSummaryEvaluationEventArgs>(EventDispatcher.GroupSummaryEvaluate, new EventHandler<GroupSummaryEvaluationEventArgs>(this.OnGroupSummaryEvaluate));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangingEventArgs>(EventDispatcher.RowsChanging, new EventHandler<GridViewCollectionChangingEventArgs>(this.OnRowsChanging));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCollectionChangedEventArgs>(EventDispatcher.RowsChanged, new EventHandler<GridViewCollectionChangedEventArgs>(this.OnRowsChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewRowEventArgs>(EventDispatcher.UserAddedRow, new EventHandler<GridViewRowEventArgs>(this.OnUserAddedRow));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewRowCancelEventArgs>(EventDispatcher.UserAddingRow, new EventHandler<GridViewRowCancelEventArgs>(this.OnUserAddingRow));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewRowCancelEventArgs>(EventDispatcher.UserDeletingRow, new EventHandler<GridViewRowCancelEventArgs>(this.OnUserDeletingRow));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewRowEventArgs>(EventDispatcher.UserDeletedRow, new EventHandler<GridViewRowEventArgs>(this.OnUserDeletedRow));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewRowSourceNeededEventArgs>(EventDispatcher.RowSourceNeeded, new EventHandler<GridViewRowSourceNeededEventArgs>(this.OnRowSourceNeeded));
      this.MasterTemplate.EventDispatcher.AddListener<CurrentRowChangingEventArgs>(EventDispatcher.CurrentRowChanging, new EventHandler<CurrentRowChangingEventArgs>(this.OnCurrentRowChanging));
      this.MasterTemplate.EventDispatcher.AddListener<CurrentRowChangedEventArgs>(EventDispatcher.CurrentRowChanged, new EventHandler<CurrentRowChangedEventArgs>(this.OnCurrentRowChanged));
      this.MasterTemplate.EventDispatcher.AddListener<RowValidatingEventArgs>(EventDispatcher.RowValidating, new EventHandler<RowValidatingEventArgs>(this.OnRowValidating));
      this.MasterTemplate.EventDispatcher.AddListener<RowValidatedEventArgs>(EventDispatcher.RowValidated, new EventHandler<RowValidatedEventArgs>(this.OnRowValidated));
      this.MasterTemplate.EventDispatcher.AddListener<RowHeightChangingEventArgs>(EventDispatcher.RowHeightChanging, new EventHandler<RowHeightChangingEventArgs>(this.OnRowHeightChanging));
      this.MasterTemplate.EventDispatcher.AddListener<RowHeightChangedEventArgs>(EventDispatcher.RowHeightChanged, new EventHandler<RowHeightChangedEventArgs>(this.OnRowHeightChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewRowPaintEventArgs>(EventDispatcher.RowPaint, new EventHandler<GridViewRowPaintEventArgs>(this.OnRowPaint));
      this.MasterTemplate.EventDispatcher.AddListener<MouseEventArgs>(EventDispatcher.RowMouseMove, new EventHandler<MouseEventArgs>(this.OnRowMouseMove));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCreateRowInfoEventArgs>(EventDispatcher.CreateRowInfo, new EventHandler<GridViewCreateRowInfoEventArgs>(this.OnCreateRowInfo));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellValueEventArgs>(EventDispatcher.CellValueNeeded, new EventHandler<GridViewCellValueEventArgs>(this.OnCellValueNeeded));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellValueEventArgs>(EventDispatcher.CellValuePushed, new EventHandler<GridViewCellValueEventArgs>(this.OnCellValuePushed));
      this.MasterTemplate.EventDispatcher.AddListener<DefaultValuesNeededEventArgs>(EventDispatcher.DefaultValuesNeeded, new EventHandler<DefaultValuesNeededEventArgs>(this.OnDefaultValuesNeeded));
      this.MasterTemplate.EventDispatcher.AddListener<CellValidatingEventArgs>(EventDispatcher.CellValidating, new EventHandler<CellValidatingEventArgs>(this.OnCellValidating));
      this.MasterTemplate.EventDispatcher.AddListener<CellValidatedEventArgs>(EventDispatcher.CellValidated, new EventHandler<CellValidatedEventArgs>(this.OnCellValidated));
      this.MasterTemplate.EventDispatcher.AddListener<CurrentCellChangedEventArgs>(EventDispatcher.CurrentCellChanged, new EventHandler<CurrentCellChangedEventArgs>(this.OnCurrentCellChanged));
      this.MasterTemplate.EventDispatcher.AddListener<ValueChangingEventArgs>(EventDispatcher.ValueChanging, new EventHandler<ValueChangingEventArgs>(this.OnValueChanging));
      this.MasterTemplate.EventDispatcher.AddListener<EventArgs>(EventDispatcher.ValueChanged, new EventHandler<EventArgs>(this.OnValueChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellEventArgs>(EventDispatcher.CellValueChanged, new EventHandler<GridViewCellEventArgs>(this.OnCellValueChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellPaintEventArgs>(EventDispatcher.CellPaint, new EventHandler<GridViewCellPaintEventArgs>(this.OnCellPaint));
      this.MasterTemplate.EventDispatcher.AddListener<MouseEventArgs>(EventDispatcher.CellMouseMove, new EventHandler<MouseEventArgs>(this.OnCellMouseMove));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellEventArgs>(EventDispatcher.CommandCellClick, new EventHandler<GridViewCellEventArgs>(this.OnCommandCellClick));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellEventArgs>(EventDispatcher.CellClick, new EventHandler<GridViewCellEventArgs>(this.OnCellClick));
      this.MasterTemplate.EventDispatcher.AddListener<ChildViewExpandingEventArgs>(EventDispatcher.ChildViewExpanding, new EventHandler<ChildViewExpandingEventArgs>(this.OnChildViewExpanding));
      this.MasterTemplate.EventDispatcher.AddListener<ChildViewExpandedEventArgs>(EventDispatcher.ChildViewExpanded, new EventHandler<ChildViewExpandedEventArgs>(this.OnChildViewExpanded));
      this.MasterTemplate.EventDispatcher.AddListener<CurrentColumnChangedEventArgs>(EventDispatcher.CurrentColumnChanged, new EventHandler<CurrentColumnChangedEventArgs>(this.OnCurrentColumnChanged));
      this.MasterTemplate.EventDispatcher.AddListener<ColumnWidthChangedEventArgs>(EventDispatcher.ColumnWidthChanged, new EventHandler<ColumnWidthChangedEventArgs>(this.OnColumnWidthChanged));
      this.MasterTemplate.EventDispatcher.AddListener<ColumnWidthChangingEventArgs>(EventDispatcher.ColumnWidthChanging, new EventHandler<ColumnWidthChangingEventArgs>(this.OnColumnWidthChanging));
      this.MasterTemplate.EventDispatcher.AddListener<EventArgs>(EventDispatcher.ConditionalFormattingFormShown, new EventHandler<EventArgs>(this.OnConditionalFormattingFormShown));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellCancelEventArgs>(EventDispatcher.CellBeginEdit, new EventHandler<GridViewCellCancelEventArgs>(this.OnCellBeginEdit));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellEventArgs>(EventDispatcher.CellEndEdit, new EventHandler<GridViewCellEventArgs>(this.OnCellEndEdit));
      this.MasterTemplate.EventDispatcher.AddListener<EditorRequiredEventArgs>(EventDispatcher.EditorRequired, new EventHandler<EditorRequiredEventArgs>(this.OnEditorRequired));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellEventArgs>(EventDispatcher.CellEditorInitialized, new EventHandler<GridViewCellEventArgs>(this.OnCellEditorInitialized));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, new EventHandler<GridViewSelectionCancelEventArgs>(this.OnSelectionChanging));
      this.MasterTemplate.EventDispatcher.AddListener<EventArgs>(EventDispatcher.SelectionChanged, new EventHandler<EventArgs>(this.OnSelectionChanged));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewDataErrorEventArgs>(EventDispatcher.DataError, new EventHandler<GridViewDataErrorEventArgs>(this.OnDataError));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewBindingCompleteEventArgs>(EventDispatcher.DataBindingComplete, new EventHandler<GridViewBindingCompleteEventArgs>(this.OnDataBindingComplete));
      this.MasterTemplate.EventDispatcher.AddListener<LayoutLoadedEventArgs>(EventDispatcher.LayoutLoaded, new EventHandler<LayoutLoadedEventArgs>(this.OnLayoutLoaded));
      this.MasterTemplate.EventDispatcher.AddListener<ContextMenuOpeningEventArgs>(EventDispatcher.ContextMenuOpening, new EventHandler<ContextMenuOpeningEventArgs>(this.OnContextMenuOpening));
      this.MasterTemplate.EventDispatcher.AddListener<ToolTipTextNeededEventArgs>(EventDispatcher.ToolTipTextNeeded, new EventHandler<ToolTipTextNeededEventArgs>(((RadControl) this).OnToolTipTextNeeded));
      this.MasterTemplate.EventDispatcher.AddListener<HyperlinkOpeningEventArgs>(EventDispatcher.HyperlinkOpening, new EventHandler<HyperlinkOpeningEventArgs>(this.OnHyperlinkOpening));
      this.MasterTemplate.EventDispatcher.AddListener<HyperlinkOpenedEventArgs>(EventDispatcher.HyperlinkOpened, new EventHandler<HyperlinkOpenedEventArgs>(this.OnHyperlinkOpened));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewClipboardEventArgs>(EventDispatcher.Copying, new EventHandler<GridViewClipboardEventArgs>(this.OnCopying));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewClipboardEventArgs>(EventDispatcher.Pasting, new EventHandler<GridViewClipboardEventArgs>(this.OnPasting));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellValueEventArgs>(EventDispatcher.CellClipboardCopy, new EventHandler<GridViewCellValueEventArgs>(this.OnCopyingCellClipboardContent));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewCellValueEventArgs>(EventDispatcher.CellClipboardPaste, new EventHandler<GridViewCellValueEventArgs>(this.OnPastingCellClipboardContent));
      this.MasterTemplate.EventDispatcher.AddListener<PageChangingEventArgs>(EventDispatcher.PageChanging, new EventHandler<PageChangingEventArgs>(this.OnPageChanging));
      this.MasterTemplate.EventDispatcher.AddListener<EventArgs>(EventDispatcher.PageChanged, new EventHandler<EventArgs>(this.OnPageChanged));
      this.MasterTemplate.EventDispatcher.AddListener<ColumnChooserItemElementCreatingEventArgs>(EventDispatcher.ColumnChooserItemElementCreating, new EventHandler<ColumnChooserItemElementCreatingEventArgs>(this.OnColumnChooserItemElementCreating));
      this.MasterTemplate.EventDispatcher.AddListener<ExpressionEditorFormCreatedEventArgs>(EventDispatcher.ExpressionEditorFormCreated, new EventHandler<ExpressionEditorFormCreatedEventArgs>(this.OnExpressionEditorFormCreated));
      this.MasterTemplate.EventDispatcher.AddListener<GridViewHeaderCellEventArgs>(EventDispatcher.HeaderCellToggleStateChanged, new EventHandler<GridViewHeaderCellEventArgs>(this.OnHeaderCellToggleStateChanged));
    }

    protected virtual void UnWireEvents()
    {
      this.gridViewElement.CreateCell -= new GridViewCreateCellEventHandler(this.gridElement_CreateCell);
      this.gridViewElement.CreateRow -= new GridViewCreateRowEventHandler(this.gridElement_CreateRow);
      this.gridViewElement.RowFormatting -= new RowFormattingEventHandler(this.gridElement_RowFormatting);
      this.gridViewElement.ViewRowFormatting -= new RowFormattingEventHandler(this.gridElement_ViewRowFormatting);
      this.gridViewElement.CellFormatting -= new CellFormattingEventHandler(this.gridElement_CellFormatting);
      this.gridViewElement.ViewCellFormatting -= new CellFormattingEventHandler(this.gridElement_ViewCellFormatting);
      this.MasterTemplate.EventDispatcher.ClearListeners();
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or set the text of the grid title.")]
    [Browsable(true)]
    [Category("Layout")]
    public string TitleText
    {
      get
      {
        return this.GridViewElement.TitleText;
      }
      set
      {
        this.GridViewElement.TitleText = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(Dock.Top)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating the position of the title.")]
    [Category("Layout")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public Dock TitlePosition
    {
      get
      {
        return this.GridViewElement.TitlePosition;
      }
      set
      {
        this.GridViewElement.TitlePosition = value;
      }
    }

    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents.")]
    [Category("Layout")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(false)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
        this.TableElement.InvalidateMeasure(true);
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(240, 150));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the position to place tabs for child views related with this template.")]
    [DefaultValue(TabPositions.Top)]
    [Browsable(true)]
    [Category("Appearance")]
    public TabPositions ChildViewTabsPosition
    {
      get
      {
        return this.MasterTemplate.ChildViewTabsPosition;
      }
      set
      {
        this.MasterTemplate.ChildViewTabsPosition = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets value indicating how user begins editing a cell.")]
    [DefaultValue(RadGridViewBeginEditMode.BeginEditOnKeystrokeOrF2)]
    public RadGridViewBeginEditMode BeginEditMode
    {
      get
      {
        return this.GridViewElement.BeginEditMode;
      }
      set
      {
        this.GridViewElement.BeginEditMode = value;
      }
    }

    [RadDescription("EnableHotTracking", typeof (GridTableElement))]
    [RadPropertyDefaultValue("EnableHotTracking", typeof (GridTableElement))]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool EnableHotTracking
    {
      get
      {
        GridTableElement tableElement = this.TableElement;
        if (tableElement != null)
          return tableElement.EnableHotTracking;
        return false;
      }
      set
      {
        GridTableElement tableElement = this.TableElement;
        if (tableElement != null)
          tableElement.EnableHotTracking = value;
        this.OnNotifyPropertyChanged(nameof (EnableHotTracking));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridTableElement TableElement
    {
      get
      {
        return this.gridViewElement.TableElement;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether alternating row color is enabled.")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public virtual bool EnableAlternatingRowColor
    {
      get
      {
        return this.MasterTemplate.EnableAlternatingRowColor;
      }
      set
      {
        this.MasterTemplate.EnableAlternatingRowColor = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value specifying if the custom drawing (e.g. CellPaint and RowPaint) is enabled")]
    [Category("Appearance")]
    public bool EnableCustomDrawing
    {
      get
      {
        return this.gridViewElement.EnableCustomDrawing;
      }
      set
      {
        this.gridViewElement.EnableCustomDrawing = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets value indicating whether the GridGroupPanel is visible.")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool ShowGroupPanel
    {
      get
      {
        return this.GridViewElement.ShowGroupPanel;
      }
      set
      {
        this.GridViewElement.ShowGroupPanel = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the selected item in the control remains highlighted when the control loses focus.")]
    [Browsable(true)]
    [Category("Appearance")]
    public bool HideSelection
    {
      get
      {
        return this.GridViewElement.HideSelection;
      }
      set
      {
        this.GridViewElement.HideSelection = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the text to use when there is no data.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool ShowNoDataText
    {
      get
      {
        return this.GridViewElement.ShowNoDataText;
      }
      set
      {
        this.GridViewElement.ShowNoDataText = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to use dedicated vertical scrollbars in hierarchy.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseScrollbarsInHierarchy
    {
      get
      {
        return this.GridViewElement.UseScrollbarsInHierarchy;
      }
      set
      {
        this.GridViewElement.UseScrollbarsInHierarchy = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether row height in a RadGridView will expand for multiline cell text")]
    [Category("Behavior")]
    public bool AutoSizeRows
    {
      get
      {
        return this.GridViewElement.AutoSizeRows;
      }
      set
      {
        this.GridViewElement.AutoSizeRows = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IRowView CurrentView
    {
      get
      {
        return this.GridViewElement.CurrentView;
      }
      set
      {
        this.GridViewElement.CurrentView = value;
      }
    }

    [DefaultValue(RadSortOrder.None)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public RadSortOrder ColumnChooserSortOrder
    {
      get
      {
        return this.GridViewElement.ColumnChooserSortOrder;
      }
      set
      {
        this.GridViewElement.ColumnChooserSortOrder = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewColumnChooser ColumnChooser
    {
      get
      {
        return this.GridViewElement.ColumnChooser;
      }
    }

    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("")]
    public IGridViewDefinition ViewDefinition
    {
      get
      {
        return this.MasterTemplate.ViewDefinition;
      }
      set
      {
        this.MasterTemplate.ViewDefinition = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to show cell errors.")]
    public bool ShowCellErrors
    {
      get
      {
        return this.GridViewElement.ShowCellErrors;
      }
      set
      {
        this.GridViewElement.ShowCellErrors = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether to show row errors.")]
    public bool ShowRowErrors
    {
      get
      {
        return this.GridViewElement.ShowRowErrors;
      }
      set
      {
        this.GridViewElement.ShowRowErrors = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public virtual IContextMenuManager ContextMenuManager
    {
      get
      {
        return this.gridViewElement.ContextMenuManager;
      }
    }

    [Category("Appearance")]
    [DefaultValue(RadGridViewSplitMode.None)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public RadGridViewSplitMode SplitMode
    {
      get
      {
        return this.GridViewElement.SplitMode;
      }
      set
      {
        this.GridViewElement.SplitMode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    public bool SynchronizeCurrentRowInSplitMode
    {
      get
      {
        return this.GridViewElement.SynchronizeCurrentRowInSplitMode;
      }
      set
      {
        this.GridViewElement.SynchronizeCurrentRowInSplitMode = value;
      }
    }

    [DefaultValue(GridExpandAnimationType.None)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or set a value indicating the animation effect that will be used when expanding/collapsing groups.")]
    public GridExpandAnimationType GroupExpandAnimationType
    {
      get
      {
        return this.gridViewElement.GroupExpandAnimationType;
      }
      set
      {
        this.gridViewElement.GroupExpandAnimationType = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether to show child view captions.")]
    [DefaultValue(false)]
    public bool ShowChildViewCaptions
    {
      get
      {
        return this.MasterTemplate.ShowChildViewCaptions;
      }
      set
      {
        this.MasterTemplate.ShowChildViewCaptions = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether the group panel will show scroll bars or it will expand to show all group headers.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool ShowGroupPanelScrollbars
    {
      get
      {
        return this.GridViewElement.ShowGroupPanelScrollbars;
      }
      set
      {
        this.GridViewElement.ShowGroupPanelScrollbars = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    [Description("Gets or sets a value indicating whether columns are created automatically when the DataSource or DataMember properties are set.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    public bool AutoGenerateColumns
    {
      get
      {
        return this.MasterTemplate.AutoGenerateColumns;
      }
      set
      {
        this.MasterTemplate.AutoGenerateColumns = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Description("Sets or gets a value indicating the initial state of group rows when data is grouped.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    public bool AutoExpandGroups
    {
      get
      {
        return this.MasterTemplate.AutoExpandGroups;
      }
      set
      {
        this.MasterTemplate.AutoExpandGroups = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CaseSensitive
    {
      get
      {
        return this.MasterTemplate.CaseSensitive;
      }
      set
      {
        this.MasterTemplate.CaseSensitive = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(ScrollState.AutoHide)]
    [Description("Gets or sets the display state of grid horizontal scrollbars.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.MasterTemplate.HorizontalScrollState;
      }
      set
      {
        this.MasterTemplate.HorizontalScrollState = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the display state of grid vertical scrollbars.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.MasterTemplate.VerticalScrollState;
      }
      set
      {
        this.MasterTemplate.VerticalScrollState = value;
      }
    }

    [Description("Gets the collection containing summary rows placed at the bottom of each DataGroup.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Data")]
    public GridViewSummaryRowItemCollection SummaryRowsBottom
    {
      get
      {
        return this.MasterTemplate.SummaryRowsBottom;
      }
    }

    [Category("Data")]
    [Description("Gets the collection containing summary rows placed on top of each DataGroup.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewSummaryRowItemCollection SummaryRowsTop
    {
      get
      {
        return this.MasterTemplate.SummaryRowsTop;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether user can drag a column header to grouping panel.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool AllowDragToGroup
    {
      get
      {
        return this.MasterTemplate.AllowDragToGroup;
      }
      set
      {
        this.MasterTemplate.AllowDragToGroup = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether user can reorder columns")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowColumnReorder
    {
      get
      {
        return this.MasterTemplate.AllowColumnReorder;
      }
      set
      {
        this.MasterTemplate.AllowColumnReorder = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether user can resize a row.")]
    public bool AllowRowResize
    {
      get
      {
        return this.MasterTemplate.AllowRowResize;
      }
      set
      {
        this.MasterTemplate.AllowRowResize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(SystemRowPosition.Top)]
    [Description("Gets or sets a value indicating the location of the new row in the view template.")]
    public SystemRowPosition AddNewRowPosition
    {
      get
      {
        return this.MasterTemplate.AddNewRowPosition;
      }
      set
      {
        this.MasterTemplate.AddNewRowPosition = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(SystemRowPosition.Top)]
    [Description("Gets or sets a value indicating the location of the search row.")]
    public SystemRowPosition SearchRowPosition
    {
      get
      {
        return this.MasterTemplate.SearchRowPosition;
      }
      set
      {
        this.MasterTemplate.SearchRowPosition = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether user can edit rows.")]
    public bool AllowEditRow
    {
      get
      {
        return this.MasterTemplate.AllowEditRow;
      }
      set
      {
        this.MasterTemplate.AllowEditRow = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the Column Chooser form is available to the user for this instance of GridViewTemplate")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowColumnChooser
    {
      get
      {
        return this.MasterTemplate.AllowColumnChooser;
      }
      set
      {
        this.MasterTemplate.AllowColumnChooser = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the user is able to reorder rows in the grid")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowRowReorder
    {
      get
      {
        return this.MasterTemplate.AllowRowReorder;
      }
      set
      {
        this.MasterTemplate.AllowRowReorder = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether context menu is displayed when user right clicks on a column header.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowColumnHeaderContextMenu
    {
      get
      {
        return this.MasterTemplate.AllowColumnHeaderContextMenu;
      }
      set
      {
        this.MasterTemplate.AllowColumnHeaderContextMenu = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether context menu is displayed when user right clicks on a row header.")]
    public bool AllowRowHeaderContextMenu
    {
      get
      {
        return this.MasterTemplate.AllowRowHeaderContextMenu;
      }
      set
      {
        this.MasterTemplate.AllowRowHeaderContextMenu = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether user can resize columns.")]
    public bool AllowColumnResize
    {
      get
      {
        return this.MasterTemplate.AllowColumnResize;
      }
      set
      {
        this.MasterTemplate.AllowColumnResize = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether context menu is displayed when user right clicks on a data cell.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowCellContextMenu
    {
      get
      {
        return this.MasterTemplate.AllowCellContextMenu;
      }
      set
      {
        this.MasterTemplate.AllowCellContextMenu = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool AllowAutoSizeColumns
    {
      get
      {
        return this.MasterTemplate.AllowAutoSizeColumns;
      }
      set
      {
        this.MasterTemplate.AllowAutoSizeColumns = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether user can delete rows.")]
    public bool AllowDeleteRow
    {
      get
      {
        return this.MasterTemplate.AllowDeleteRow;
      }
      set
      {
        this.MasterTemplate.AllowDeleteRow = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Data")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the user can add new rows through the grid.")]
    public bool AllowAddNewRow
    {
      get
      {
        return this.MasterTemplate.AllowAddNewRow;
      }
      set
      {
        this.MasterTemplate.AllowAddNewRow = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Data")]
    [Description("Gets or sets a value indicating whether the user can search through the data in the grid.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowSearchRow
    {
      get
      {
        return this.MasterTemplate.AllowSearchRow;
      }
      set
      {
        this.MasterTemplate.AllowSearchRow = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowMultiColumnSorting
    {
      get
      {
        return this.MasterTemplate.AllowMultiColumnSorting;
      }
      set
      {
        this.MasterTemplate.AllowMultiColumnSorting = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the columns by which the data is grouped are visible.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowGroupedColumns
    {
      get
      {
        return this.MasterTemplate.ShowGroupedColumns;
      }
      set
      {
        this.MasterTemplate.ShowGroupedColumns = value;
      }
    }

    [Category("Layout")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(GridViewAutoSizeColumnsMode.None)]
    [Description("Gets or sets a value indicating how column widths are determined.")]
    public GridViewAutoSizeColumnsMode AutoSizeColumnsMode
    {
      get
      {
        return this.MasterTemplate.AutoSizeColumnsMode;
      }
      set
      {
        this.MasterTemplate.AutoSizeColumnsMode = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Layout")]
    [DefaultValue(GridViewBottomPinnedRowsMode.Float)]
    [Description("Gets or sets a value indicating how bottom pinned rows are laid out.")]
    public GridViewBottomPinnedRowsMode BottomPinnedRowsMode
    {
      get
      {
        return this.MasterTemplate.BottomPinnedRowsMode;
      }
      set
      {
        this.MasterTemplate.BottomPinnedRowsMode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the row header column is visible.")]
    public bool ShowRowHeaderColumn
    {
      get
      {
        return this.MasterTemplate.ShowRowHeaderColumn;
      }
      set
      {
        this.MasterTemplate.ShowRowHeaderColumn = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the column headers are visible.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowColumnHeaders
    {
      get
      {
        return this.MasterTemplate.ShowColumnHeaders;
      }
      set
      {
        this.MasterTemplate.ShowColumnHeaders = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the filtering row should be visible.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowFilteringRow
    {
      get
      {
        return this.MasterTemplate.ShowFilteringRow;
      }
      set
      {
        this.MasterTemplate.ShowFilteringRow = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the header cell buttons are visible.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowHeaderCellButtons
    {
      get
      {
        return this.MasterTemplate.ShowHeaderCellButtons;
      }
      set
      {
        this.MasterTemplate.ShowHeaderCellButtons = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Data")]
    public GridViewTemplateCollection Templates
    {
      get
      {
        return this.MasterTemplate.Templates;
      }
    }

    public object Evaluate(string expression, IEnumerable<GridViewRowInfo> rows)
    {
      return this.MasterTemplate.ListSource.CollectionView.Evaluate(expression, rows);
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets value indicating whether users can sort data in the master view template.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableSorting
    {
      get
      {
        return this.MasterTemplate.EnableSorting;
      }
      set
      {
        this.MasterTemplate.EnableSorting = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the custom sorting functionality should be enabled. Use the CustomSorting event to apply the desired sorting.")]
    public bool EnableCustomSorting
    {
      get
      {
        return this.MasterTemplate.EnableCustomSorting;
      }
      set
      {
        this.MasterTemplate.EnableCustomSorting = value;
      }
    }

    [Description("Gets or Sets value indicating whether users can group data in the master view template.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableGrouping
    {
      get
      {
        return this.MasterTemplate.EnableGrouping;
      }
      set
      {
        this.MasterTemplate.EnableGrouping = value;
      }
    }

    [Description("Gets or sets a value indicating whether the custom grouping functionality should be enabled. Use the CustomGrouping event to group the data.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool EnableCustomGrouping
    {
      get
      {
        return this.MasterTemplate.EnableCustomGrouping;
      }
      set
      {
        this.MasterTemplate.EnableCustomGrouping = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets value indicating whether users can filter data in the master view template.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableFiltering
    {
      get
      {
        return this.MasterTemplate.EnableFiltering;
      }
      set
      {
        bool enableFiltering = this.MasterTemplate.EnableFiltering;
        if (enableFiltering == value)
          return;
        this.MasterTemplate.EnableFiltering = value;
        if (!this.DesignMode)
          return;
        ((IComponentChangeService) ((System.IServiceProvider) this.Site.GetService(typeof (IDesignerHost))).GetService(typeof (IComponentChangeService))).OnComponentChanged((object) this.MasterTemplate, (MemberDescriptor) null, (object) enableFiltering, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether the custom filtering functionality should be enabled. Use the CustomFiltering event to apply the desired filters.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool EnableCustomFiltering
    {
      get
      {
        return this.MasterTemplate.EnableCustomFiltering;
      }
      set
      {
        this.MasterTemplate.EnableCustomFiltering = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets value indicating whether users can paginate data in the master view template.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public bool EnablePaging
    {
      get
      {
        return this.MasterTemplate.EnablePaging;
      }
      set
      {
        this.MasterTemplate.EnablePaging = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(20)]
    [Description("Gets or sets value indicating the number of rows in the master view template when paging is enabled.")]
    [Browsable(true)]
    [Category("Behavior")]
    public int PageSize
    {
      get
      {
        return this.MasterTemplate.PageSize;
      }
      set
      {
        this.MasterTemplate.PageSize = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether you have provided your own data-management operations for the RadGridView control.")]
    [Category("Behavior")]
    public bool VirtualMode
    {
      get
      {
        return this.MasterTemplate.VirtualMode;
      }
      set
      {
        this.MasterTemplate.VirtualMode = value;
      }
    }

    [Description("Gets or sets the data source that the RadGridView is displaying data for.")]
    [AttributeProvider(typeof (IListSource))]
    [Category("Data")]
    [RadDefaultValue("DataSource", typeof (GridViewTemplate))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    public object DataSource
    {
      get
      {
        return this.GridViewElement.Template.DataSource;
      }
      set
      {
        this.GridViewElement.Template.DataSource = value;
      }
    }

    [RadDefaultValue("DataMember", typeof (GridViewTemplate))]
    [Browsable(true)]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the name of the list or table in the data source for which the RadGridView is displaying data. ")]
    public string DataMember
    {
      get
      {
        return this.GridViewElement.Template.DataMember;
      }
      set
      {
        this.GridViewElement.Template.DataMember = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewRowInfo CurrentRow
    {
      get
      {
        return this.GridViewElement.CurrentRow;
      }
      set
      {
        this.GridViewElement.CurrentRow = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewColumn CurrentColumn
    {
      get
      {
        return this.GridViewElement.CurrentColumn;
      }
      set
      {
        this.GridViewElement.CurrentColumn = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridDataCellElement CurrentCell
    {
      get
      {
        return this.GridViewElement.CurrentCell;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Point CurrentCellAddress
    {
      get
      {
        if (this.CurrentView != null)
          return this.CurrentView.CurrentCellAddress;
        return new Point(-1, -1);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the collection of rows selected by the user.")]
    public GridViewSelectedRowsCollection SelectedRows
    {
      get
      {
        return this.GridViewElement.Template.SelectedRows;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the collection of cells selected by the user.")]
    [Browsable(false)]
    public GridViewSelectedCellsCollection SelectedCells
    {
      get
      {
        return this.GridViewElement.Template.SelectedCells;
      }
    }

    [Description("Gets a collection that contains all the rows in the RadGridView.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewRowCollection Rows
    {
      get
      {
        return this.GridViewElement.Template.Rows;
      }
    }

    [MergableProperty(false)]
    [Editor("Telerik.WinControls.UI.Design.GridViewColumnCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewColumnCollection Columns
    {
      get
      {
        return this.GridViewElement.Template.Columns;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataGroupCollection Groups
    {
      get
      {
        return this.GridViewElement.Template.Groups;
      }
    }

    [Description("Gets collection of GridViewRelation instances that represent the hierarchical structure.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Category("Data")]
    public GridViewRelationCollection Relations
    {
      get
      {
        return this.GridViewElement.Template.Relations;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the top-most template level of the hierarchical data.")]
    [Category("Data")]
    [Browsable(false)]
    public MasterGridViewTemplate MasterTemplate
    {
      get
      {
        return this.gridViewElement.Template;
      }
    }

    [Description("Gets the top-most GridViewInfo level of the grid.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewInfo MasterView
    {
      get
      {
        return this.MasterTemplate.MasterViewInfo;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewChildRowCollection ChildRows
    {
      get
      {
        return this.MasterTemplate.ChildRows;
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.MasterTemplate.FilterDescriptors;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.MasterTemplate.SortDescriptors;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.MasterTemplate.GroupDescriptors;
      }
    }

    [DefaultValue(0)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public int RowCount
    {
      get
      {
        return this.MasterTemplate.RowCount;
      }
      set
      {
        this.MasterTemplate.RowCount = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DefaultValue(0)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ColumnCount
    {
      get
      {
        return this.MasterTemplate.ColumnCount;
      }
      set
      {
        this.MasterTemplate.ColumnCount = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the RadGridView will automatically build hierarchy from DataSource.")]
    [DefaultValue(false)]
    public bool AutoGenerateHierarchy
    {
      get
      {
        return this.MasterTemplate.AutoGenerateHierarchy;
      }
      set
      {
        this.MasterTemplate.AutoGenerateHierarchy = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsCurrentRowDirty
    {
      get
      {
        return false;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IPrintSettingsDialogFactory PrintSettingsDialogFactory
    {
      get
      {
        if (this.printSettingsDialogFactory == null)
          this.printSettingsDialogFactory = (IPrintSettingsDialogFactory) new GridViewPrintSettingsDialogFactory();
        return this.printSettingsDialogFactory;
      }
      set
      {
        this.printSettingsDialogFactory = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool EnableKineticScrolling
    {
      get
      {
        return this.enableKineticScrolling;
      }
      set
      {
        if (this.enableKineticScrolling == value)
          return;
        this.enableKineticScrolling = value;
        if (value)
          return;
        foreach (RadElement child in this.GridViewElement.Panel.Children)
          (child as GridTableElement)?.ScrollBehavior.Stop();
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating how the cells of the RadGridView can be selected.")]
    [Category("Behavior")]
    [DefaultValue(GridViewSelectionMode.FullRowSelect)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewSelectionMode SelectionMode
    {
      get
      {
        return this.MasterTemplate.SelectionMode;
      }
      set
      {
        this.MasterTemplate.SelectionMode = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the user is allowed to select more than one cell, row, or column of the RadGridView at a time.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MultiSelect
    {
      get
      {
        return this.MasterTemplate.MultiSelect;
      }
      set
      {
        this.MasterTemplate.MultiSelect = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the user can edit the cells of the RadGridView control.")]
    [Category("Behavior")]
    public bool ReadOnly
    {
      get
      {
        return this.MasterTemplate.GridReadOnly;
      }
      set
      {
        this.MasterTemplate.GridReadOnly = value;
      }
    }

    [Description("Gets or sets an instance of BaseGridBehavior or the instance that implements IGridBehavior interface.")]
    [Browsable(false)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IGridBehavior GridBehavior
    {
      get
      {
        return this.GridViewElement.GridBehavior;
      }
      set
      {
        this.GridViewElement.GridBehavior = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    public virtual IGridNavigator GridNavigator
    {
      get
      {
        return this.GridViewElement.Navigator;
      }
    }

    [Description("Gets or sets a value indicating whether the active editor should close when validation process fails.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool CloseEditorWhenValidationFails
    {
      get
      {
        return this.GridViewElement.EditorManager.CloseEditorWhenValidationFails;
      }
      set
      {
        this.GridViewElement.EditorManager.CloseEditorWhenValidationFails = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets value indicating if fast scrolling mode is turned on.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableFastScrolling
    {
      get
      {
        return this.TableElement.RowScroller.ScrollMode == ItemScrollerScrollModes.Deferred;
      }
      set
      {
        if (value)
          this.TableElement.RowScroller.ScrollMode = ItemScrollerScrollModes.Deferred;
        else
          this.TableElement.RowScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets value which defines the default behavior of the grid when Tab is pressed.")]
    [Browsable(true)]
    [Category("Behavior")]
    public bool StandardTab
    {
      get
      {
        return this.GridViewElement.StandardTab;
      }
      set
      {
        this.GridViewElement.StandardTab = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets value specifying the behavior when the user presses Enter while adding new row.")]
    [DefaultValue(RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow)]
    [Browsable(true)]
    public RadGridViewNewRowEnterKeyMode NewRowEnterKeyMode
    {
      get
      {
        return this.GridViewElement.NewRowEnterKeyMode;
      }
      set
      {
        this.GridViewElement.NewRowEnterKeyMode = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets value specifying the behavior when the user presses Enter.")]
    [Browsable(true)]
    [DefaultValue(RadGridViewEnterKeyMode.None)]
    public RadGridViewEnterKeyMode EnterKeyMode
    {
      get
      {
        return this.GridViewElement.EnterKeyMode;
      }
      set
      {
        this.GridViewElement.EnterKeyMode = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewEditManager EditorManager
    {
      get
      {
        return this.GridViewElement.EditorManager;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Fires when a cell needs to be painted.")]
    public event GridViewCellPaintEventHandler CellPaint;

    protected virtual void OnCellPaint(object sender, GridViewCellPaintEventArgs e)
    {
      if (this.CellPaint == null)
        return;
      this.CellPaint((object) this, e);
    }

    [Category("Appearance")]
    [Description("Fires when a row needs to be painted.")]
    [Browsable(true)]
    public event GridViewRowPaintEventHandler RowPaint;

    protected virtual void OnRowPaint(object sender, GridViewRowPaintEventArgs e)
    {
      if (this.RowPaint == null)
        return;
      this.RowPaint((object) this, e);
    }

    [Description("Fires when a ColumnChooserCreated is created.")]
    [Category("Appearance")]
    [Browsable(true)]
    public event ColumnChooserCreatedEventHandler ColumnChooserCreated
    {
      add
      {
        this.gridViewElement.ColumnChooserCreated += value;
      }
      remove
      {
        this.gridViewElement.ColumnChooserCreated -= value;
      }
    }

    [Category("Appearance")]
    [Description("Fires when a cell needs to be created.")]
    [Browsable(true)]
    public event GridViewCreateCellEventHandler CreateCell
    {
      add
      {
        this.gridViewElement.CreateCell += value;
      }
      remove
      {
        this.gridViewElement.CreateCell -= value;
      }
    }

    protected virtual void OnCreateCell(object sender, GridViewCreateCellEventArgs e)
    {
    }

    private void gridElement_CreateCell(object sender, GridViewCreateCellEventArgs e)
    {
      this.OnCreateCell(sender, e);
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Fires when a row needs to be created.")]
    public event GridViewCreateRowEventHandler CreateRow
    {
      add
      {
        this.gridViewElement.CreateRow += value;
      }
      remove
      {
        this.gridViewElement.CreateRow -= value;
      }
    }

    protected virtual void OnCreateRow(object sender, GridViewCreateRowEventArgs e)
    {
    }

    private void gridElement_CreateRow(object sender, GridViewCreateRowEventArgs e)
    {
      this.OnCreateRow(sender, e);
    }

    [Category("Action")]
    [Description("Fires when a data row is invalidated and needs to be formatted.")]
    [Browsable(true)]
    public event RowFormattingEventHandler RowFormatting
    {
      add
      {
        this.gridViewElement.RowFormatting += value;
      }
      remove
      {
        this.gridViewElement.RowFormatting -= value;
      }
    }

    protected void OnRowFormatting(object sender, RowFormattingEventArgs e)
    {
    }

    private void gridElement_RowFormatting(object sender, RowFormattingEventArgs e)
    {
      this.OnRowFormatting(sender, e);
    }

    [Description("Fires when a grid row is invalidated and needs to be formatted.")]
    [Browsable(true)]
    [Category("Action")]
    public event RowFormattingEventHandler ViewRowFormatting
    {
      add
      {
        this.gridViewElement.ViewRowFormatting += value;
      }
      remove
      {
        this.gridViewElement.ViewRowFormatting -= value;
      }
    }

    protected void OnViewRowFormatting(object sender, RowFormattingEventArgs e)
    {
    }

    private void gridElement_ViewRowFormatting(object sender, RowFormattingEventArgs e)
    {
      this.OnViewRowFormatting(sender, e);
    }

    [Category("Action")]
    [Description("Fires when the content of a data cell needs to be formatted for display.")]
    [Browsable(true)]
    public event CellFormattingEventHandler CellFormatting
    {
      add
      {
        this.gridViewElement.CellFormatting += value;
      }
      remove
      {
        this.gridViewElement.CellFormatting -= value;
      }
    }

    protected void OnCellFormatting(object sender, CellFormattingEventArgs e)
    {
    }

    private void gridElement_CellFormatting(object sender, CellFormattingEventArgs e)
    {
      this.OnCellFormatting(sender, e);
    }

    [Description("Fires when the content of a cell needs to be formatted for display.")]
    [Category("Action")]
    [Browsable(true)]
    public event CellFormattingEventHandler ViewCellFormatting
    {
      add
      {
        this.gridViewElement.ViewCellFormatting += value;
      }
      remove
      {
        this.gridViewElement.ViewCellFormatting -= value;
      }
    }

    protected void OnViewCellFormatting(object sender, CellFormattingEventArgs e)
    {
    }

    private void gridElement_ViewCellFormatting(object sender, CellFormattingEventArgs e)
    {
      this.OnViewCellFormatting(sender, e);
    }

    [Description("Fires when an element for editing a cell is showing.")]
    [Category("Action")]
    [Browsable(true)]
    public event EditorRequiredEventHandler EditorRequired;

    protected virtual void OnEditorRequired(object sender, EditorRequiredEventArgs e)
    {
      if (this.EditorRequired == null)
        return;
      this.EditorRequired(sender, e);
    }

    [Category("Data")]
    [Description("Fires when the cell is entering edit mode. The action can be canceled.")]
    [Browsable(true)]
    public event GridViewCellCancelEventHandler CellBeginEdit;

    protected virtual void OnCellBeginEdit(object sender, GridViewCellCancelEventArgs e)
    {
      if (this.CellBeginEdit == null)
        return;
      this.CellBeginEdit(sender, e);
    }

    [Description("Fires when an element for editing a cell is initialized and visible.")]
    [Category("Action")]
    [Browsable(true)]
    public event GridViewCellEventHandler CellEditorInitialized;

    protected virtual void OnCellEditorInitialized(object sender, GridViewCellEventArgs e)
    {
      if (this.CellEditorInitialized == null)
        return;
      this.CellEditorInitialized(sender, e);
    }

    [Category("Data")]
    [Description("Fires when the cell editing is finished.")]
    [Browsable(true)]
    public event GridViewCellEventHandler CellEndEdit;

    protected virtual void OnCellEndEdit(object sender, GridViewCellEventArgs e)
    {
      if (e.Column != null && e.Row != null)
        ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Edited", (object) string.Format("{0}.{1}", (object) e.Column.Name, e.Row[e.Column] != null ? (object) e.Row[e.Column].ToString() : (object) ""));
      if (this.CellEndEdit == null)
        return;
      this.CellEndEdit(sender, e);
    }

    [Browsable(true)]
    [Category("Action")]
    [Description("Fires when the value of a cell changes.")]
    public event EventHandler ValueChanged;

    protected virtual void OnValueChanged(object sender, EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged(sender, e);
    }

    [Category("Action")]
    [Description("Fires before the value in a cell is changing.")]
    [Browsable(true)]
    public event ValueChangingEventHandler ValueChanging;

    protected virtual void OnValueChanging(object sender, ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging(sender, e);
    }

    public event RowValidatingEventHandler RowValidating;

    protected virtual void OnRowValidating(object sender, RowValidatingEventArgs e)
    {
      if (this.RowValidating == null)
        return;
      this.RowValidating((object) this, e);
    }

    public event RowValidatedEventHandler RowValidated;

    protected virtual void OnRowValidated(object sender, RowValidatedEventArgs e)
    {
      if (this.RowValidated == null)
        return;
      this.RowValidated((object) this, e);
    }

    public event CellValidatingEventHandler CellValidating;

    protected virtual void OnCellValidating(object sender, CellValidatingEventArgs e)
    {
      if (this.CellValidating == null)
        return;
      this.CellValidating((object) this, e);
    }

    public event CellValidatedEventHandler CellValidated;

    protected virtual void OnCellValidated(object sender, CellValidatedEventArgs e)
    {
      if (this.CellValidated == null)
        return;
      this.CellValidated((object) this, e);
    }

    [Browsable(true)]
    [Description("Fires when the current cell is changed.")]
    [Category("Action")]
    public event CurrentCellChangedEventHandler CurrentCellChanged;

    protected virtual void OnCurrentCellChanged(object sender, CurrentCellChangedEventArgs e)
    {
      if (this.CurrentCellChanged != null)
        this.CurrentCellChanged((object) this, e);
      if (!this.AccessibilityRequested)
        return;
      int childID = 0;
      if (!this.EnableCodedUITests && this.AccessibilityObject != null)
      {
        if (e.NewCell != null && e.NewCell.Value != null)
        {
          if (e.NewCell.RowInfo != null)
          {
            childID = e.NewCell.RowInfo.Index;
            if (childID == -1)
            {
              childID = this.ChildRows.IndexOf(this.CurrentRow);
              if (childID > -1)
                ++childID;
            }
            if (childID == -1)
              childID = 0;
          }
          ((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription = "Row " + childID.ToString() + " Column " + e.NewCell.ColumnInfo.HeaderText + " Value " + e.NewCell.Value.ToString();
        }
        else
          ((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription = "";
      }
      if (e.NewCell == null)
        return;
      this.AccessibilityNotifyClients(AccessibleEvents.SelectionRemove, childID);
      this.AccessibilityNotifyClients(AccessibleEvents.SelectionAdd, childID);
      this.AccessibilityNotifyClients(AccessibleEvents.Focus, childID);
    }

    [Description("Fires when current row has changed, allows cancellation.")]
    [Browsable(true)]
    [Category("Action")]
    public event CurrentRowChangedEventHandler CurrentRowChanged;

    protected virtual void OnCurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
    {
      if (this.CurrentRowChanged != null)
        this.CurrentRowChanged((object) this, e);
      if (!this.AccessibilityRequested)
        return;
      int childID = 0;
      if (!this.EnableCodedUITests && this.AccessibilityObject != null)
      {
        if (e.CurrentRow != null && e.CurrentRow is GridViewGroupRowInfo)
          ((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription = ((GridViewGroupRowInfo) e.CurrentRow).HeaderText.ToString();
        else
          ((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription = "";
        ToolTip toolTip = new ToolTip();
        toolTip.InitialDelay = 0;
        toolTip.IsBalloon = true;
        toolTip.Show(((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription, (IWin32Window) this);
        toolTip.Dispose();
        this.AccessibilityNotifyClients(AccessibleEvents.Focus, 0);
      }
      if (e.CurrentRow == null)
        return;
      this.AccessibilityNotifyClients(AccessibleEvents.SelectionRemove, childID);
      this.AccessibilityNotifyClients(AccessibleEvents.SelectionAdd, childID);
      this.AccessibilityNotifyClients(AccessibleEvents.Focus, childID);
    }

    [Browsable(true)]
    [Description("Fires when a row is going to be changed, allows cancellation.")]
    [Category("Action")]
    public event CurrentRowChangingEventHandler CurrentRowChanging;

    protected virtual void OnCurrentRowChanging(object sender, CurrentRowChangingEventArgs e)
    {
      if (this.CurrentRowChanging == null)
        return;
      this.CurrentRowChanging((object) this, e);
    }

    [Browsable(true)]
    [Description("Fires when the current column has changed.")]
    [Category("Action")]
    public event CurrentColumnChangedEventHandler CurrentColumnChanged;

    protected virtual void OnCurrentColumnChanged(object sender, CurrentColumnChangedEventArgs e)
    {
      if (this.CurrentColumnChanged == null)
        return;
      this.CurrentColumnChanged((object) this, e);
    }

    [Description("Fires when the current selection is changing.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewSelectionCancelEventHandler SelectionChanging;

    protected virtual void OnSelectionChanging(object sender, GridViewSelectionCancelEventArgs e)
    {
      if (this.SelectionChanging == null)
        return;
      this.SelectionChanging((object) this, e);
    }

    [Browsable(true)]
    [Description("Fires when the current selection changes.")]
    [Category("Action")]
    public event EventHandler SelectionChanged;

    protected virtual void OnSelectionChanged(object sender, EventArgs e)
    {
      if (this.SelectionChanged != null)
        this.SelectionChanged((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "SelectionChanged");
    }

    [Description("Fires when the current page is changed.")]
    [Browsable(true)]
    [Category("Action")]
    public event EventHandler<EventArgs> PageChanged;

    protected virtual void OnPageChanged(object sender, EventArgs e)
    {
      if (this.PageChanged == null)
        return;
      this.PageChanged((object) this, e);
    }

    [Browsable(true)]
    [Description("Fires when the current page is changing.")]
    [Category("Action")]
    public event EventHandler<PageChangingEventArgs> PageChanging;

    protected virtual void OnPageChanging(object sender, PageChangingEventArgs e)
    {
      if (this.PageChanging == null)
        return;
      this.PageChanging((object) this, e);
    }

    [Description("Fires when a new column chooser item element is being created.")]
    [Browsable(true)]
    [Category("Action")]
    public event ColumnChooserItemElementCreatingEventHandler ColumnChooserItemElementCreating;

    protected virtual void OnColumnChooserItemElementCreating(
      object sender,
      ColumnChooserItemElementCreatingEventArgs e)
    {
      if (this.ColumnChooserItemElementCreating == null)
        return;
      this.ColumnChooserItemElementCreating((object) this, e);
    }

    [Description("Fires when a new expression editor form is created.")]
    [Category("Action")]
    [Browsable(true)]
    public event ExpressionEditorFormCreatedEventHandler ExpressionEditorFormCreated;

    protected virtual void OnExpressionEditorFormCreated(
      object sender,
      ExpressionEditorFormCreatedEventArgs e)
    {
      if (this.ExpressionEditorFormCreated == null)
        return;
      this.ExpressionEditorFormCreated((object) this, e);
    }

    [Description("Fires when the child view is expanded or collapsed.")]
    [Browsable(true)]
    [Category("Action")]
    public event ChildViewExpandedEventHandler ChildViewExpanded;

    protected virtual void OnChildViewExpanded(object sender, ChildViewExpandedEventArgs e)
    {
      if (this.ChildViewExpanded != null)
        this.ChildViewExpanded((object) this, e);
      if (this.AccessibilityRequested && !this.EnableCodedUITests)
      {
        ((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription = e.IsExpanded ? "Expanded" : "Collapsed";
        ToolTip toolTip = new ToolTip();
        toolTip.InitialDelay = 0;
        toolTip.IsBalloon = true;
        toolTip.Show(((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription, (IWin32Window) this);
        toolTip.Dispose();
        this.AccessibilityNotifyClients(AccessibleEvents.Focus, 0);
      }
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Expanded", (object) string.Format("{0}.{1}", (object) (e.ParentRow != null ? e.ParentRow.HierarchyLevel : 0), e.IsExpanded ? (object) "Expanded" : (object) "Collapsed"));
    }

    [Description("Fires when the child view is expanded or collapsed.")]
    [Category("Action")]
    [Browsable(true)]
    public event ChildViewExpandingEventHandler ChildViewExpanding;

    protected virtual void OnChildViewExpanding(object sender, ChildViewExpandingEventArgs e)
    {
      if (this.ChildViewExpanding == null)
        return;
      this.ChildViewExpanding((object) this, e);
    }

    [Description("Fires when the DataGroup is expanded or collapsed.")]
    [Category("Action")]
    [Browsable(true)]
    public event GroupExpandedEventHandler GroupExpanded;

    protected virtual void OnGroupExpanded(object sender, GroupExpandedEventArgs e)
    {
      if (this.GroupExpanded != null)
        this.GroupExpanded((object) this, e);
      if (this.AccessibilityRequested && !this.EnableCodedUITests)
      {
        ((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription = e.IsExpanded ? "Expanded" : "Collapsed";
        this.AccessibilityNotifyClients(AccessibleEvents.Focus, 0);
        ToolTip toolTip = new ToolTip();
        toolTip.InitialDelay = 0;
        toolTip.IsBalloon = true;
        toolTip.Show(((RadGridViewAccessibleObject) this.AccessibilityObject).CellDescription, (IWin32Window) this);
        toolTip.Dispose();
        this.AccessibilityNotifyClients(AccessibleEvents.Focus, 0);
      }
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Expanded", (object) string.Format("{0}.{1}", e.DataGroup != null ? (object) e.DataGroup.Header : (object) "", e.IsExpanded ? (object) "Expanded" : (object) "Collapsed"));
    }

    [Category("Action")]
    [Description("Fires when the DataGroup is expanding or collapsing. This behavior is valid when grouping or hierarchy mode is applied.")]
    [Browsable(true)]
    public event GroupExpandingEventHandler GroupExpanding;

    protected virtual void OnGroupExpanding(object sender, GroupExpandingEventArgs e)
    {
      if (this.GroupExpanding == null)
        return;
      this.GroupExpanding((object) this, e);
    }

    public event GridViewCreateRowInfoEventHandler CreateRowInfo;

    protected virtual void OnCreateRowInfo(object sender, GridViewCreateRowInfoEventArgs e)
    {
      GridViewCreateRowInfoEventHandler createRowInfo = this.CreateRowInfo;
      if (createRowInfo == null)
        return;
      createRowInfo((object) this, e);
    }

    [Browsable(true)]
    [Category("Action")]
    [Description("Occurs when the user has finished adding a row to the RadGridView.")]
    public event GridViewRowSourceNeededEventHandler RowSourceNeeded;

    protected virtual void OnRowSourceNeeded(object sender, GridViewRowSourceNeededEventArgs e)
    {
      if (this.RowSourceNeeded == null)
        return;
      this.RowSourceNeeded(sender, e);
    }

    [Description("Occurs when the user adding new row to the RadGridView.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewRowCancelEventHandler UserAddingRow;

    protected virtual void OnUserAddingRow(object sender, GridViewRowCancelEventArgs e)
    {
      if (this.UserAddingRow == null)
        return;
      this.UserAddingRow(sender, e);
    }

    [Description("Occurs when the user has finished adding a row to the RadGridView.")]
    [Category("Action")]
    [Browsable(true)]
    public event GridViewRowEventHandler UserAddedRow;

    protected virtual void OnUserAddedRow(object sender, GridViewRowEventArgs e)
    {
      if (this.UserAddedRow == null)
        return;
      this.UserAddedRow(sender, e);
    }

    [Description("Occurs when the user deletes a row from the RadGridView.")]
    [Category("Action")]
    [Browsable(true)]
    public event GridViewRowCancelEventHandler UserDeletingRow;

    protected virtual void OnUserDeletingRow(object sender, GridViewRowCancelEventArgs e)
    {
      if (this.UserDeletingRow == null)
        return;
      this.UserDeletingRow(sender, e);
    }

    [Browsable(true)]
    [Description("Occurs when the user has finished deleting a row from the RadGridView.")]
    [Category("Action")]
    public event GridViewRowEventHandler UserDeletedRow;

    protected virtual void OnUserDeletedRow(object sender, GridViewRowEventArgs e)
    {
      if (this.UserDeletedRow == null)
        return;
      this.UserDeletedRow(sender, e);
    }

    [Description("Fires when an unbound cell requires a value for a cell in order to format and display the cell.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public event QuestionEventHandler RowDirtyStateNeeded;

    protected virtual void OnRowDirtyStateNeeded(object sender, QuestionEventArgs e)
    {
      if (this.RowDirtyStateNeeded == null)
        return;
      this.RowDirtyStateNeeded(sender, e);
    }

    [Browsable(true)]
    [Description("Fires after the height of a row changes.")]
    [Category("Behavior")]
    public event RowHeightChangedEventHandler RowHeightChanged;

    protected virtual void OnRowHeightChanged(object sender, RowHeightChangedEventArgs args)
    {
      if (this.RowHeightChanged == null)
        return;
      this.RowHeightChanged((object) args.Row.ViewInfo, args);
    }

    [Browsable(true)]
    [Description("Fires before the height of a row changes.")]
    [Category("Behavior")]
    public event RowHeightChangingEventHandler RowHeightChanging;

    protected virtual void OnRowHeightChanging(object sender, RowHeightChangingEventArgs args)
    {
      if (this.RowHeightChanging == null)
        return;
      this.RowHeightChanging((object) args.Row.ViewInfo, args);
    }

    [Description("Fires when the mouse pointer moves over a row.")]
    [Browsable(true)]
    [Category("Action")]
    public event RowMouseMoveEventHandler RowMouseMove;

    protected void OnRowMouseMove(object sender, MouseEventArgs e)
    {
      RowMouseMoveEventHandler rowMouseMove = this.RowMouseMove;
      if (rowMouseMove == null)
        return;
      rowMouseMove(sender, (EventArgs) e);
    }

    [Browsable(true)]
    [Description("Fires when the Rows collection of a GridViewTemplate changes.")]
    [Category("Action")]
    public event GridViewCollectionChangedEventHandler RowsChanged;

    protected virtual void OnRowsChanged(object sender, GridViewCollectionChangedEventArgs args)
    {
      if (this.RowsChanged == null)
        return;
      this.RowsChanged(sender, args);
    }

    [Description("Fires before the Rows collection of a GridViewTemplate changes.")]
    [Category("Action")]
    [Browsable(true)]
    public event GridViewCollectionChangingEventHandler RowsChanging;

    protected virtual void OnRowsChanging(object sender, GridViewCollectionChangingEventArgs args)
    {
      if (this.RowsChanging == null)
        return;
      this.RowsChanging(sender, args);
    }

    [Browsable(true)]
    [Description("Fires when the user enters the row for new records, so that it can be populated with default values.")]
    [Category("Data")]
    public event GridViewRowEventHandler DefaultValuesNeeded;

    protected virtual void OnDefaultValuesNeeded(object sender, DefaultValuesNeededEventArgs e)
    {
      if (this.DefaultValuesNeeded == null)
        return;
      this.DefaultValuesNeeded(sender, (GridViewRowEventArgs) e);
    }

    [Description("Fires when the unbound cell requires a value for a cell in order to format and display the cell.")]
    [Browsable(false)]
    [Category("Data")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public event GridViewRowEventHandler NewRowNeeded;

    protected virtual void OnNewRowNeeded(object sender, GridViewRowEventArgs e)
    {
      if (this.NewRowNeeded == null)
        return;
      this.NewRowNeeded(sender, e);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Fires when the unbound cell requires a value for a cell in order to format and display the cell.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Category("Data")]
    public event QuestionEventHandler CancelRowEdit;

    protected virtual void OnCancelRowEdit(object sender, QuestionEventArgs e)
    {
      if (this.CancelRowEdit == null)
        return;
      this.CancelRowEdit(sender, e);
    }

    [Description("Fires when the unbound cell requires a value for a cell in order to format and display the cell.")]
    [Browsable(true)]
    [Category("Data")]
    public event GridViewCellValueEventHandler CellValueNeeded;

    protected virtual void OnCellValueNeeded(object sender, GridViewCellValueEventArgs e)
    {
      if (this.CellValueNeeded == null)
        return;
      this.CellValueNeeded(sender, e);
    }

    [Browsable(true)]
    [Description("Fires when the unbound cell changed and requires storage in the underlying data source.")]
    [Category("Data")]
    public event GridViewCellValueEventHandler CellValuePushed;

    protected virtual void OnCellValuePushed(object sender, GridViewCellValueEventArgs e)
    {
      if (this.CellValuePushed == null)
        return;
      this.CellValuePushed(sender, e);
    }

    [Description("Fires when a cell is clicked.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewCellEventHandler CellClick;

    protected virtual void OnCellClick(object sender, GridViewCellEventArgs e)
    {
      if (this.CellClick == null)
        return;
      this.CellClick(sender, e);
    }

    [Description("Fires when a cell is double clicked.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewCellEventHandler CellDoubleClick;

    protected void OnCellDoubleClick(object sender, GridViewCellEventArgs e)
    {
      if (this.CellDoubleClick == null)
        return;
      this.CellDoubleClick(sender, e);
    }

    [Description("Fires when the mouse pointer moves over the cell.")]
    [Category("Action")]
    [Browsable(true)]
    public event CellMouseMoveEventHandler CellMouseMove;

    protected void OnCellMouseMove(object sender, MouseEventArgs e)
    {
      if (this.CellMouseMove == null)
        return;
      this.CellMouseMove(sender, e);
    }

    [Category("Action")]
    [Description("Fires when the value of a cell changes.")]
    [Browsable(true)]
    public event GridViewCellEventHandler CellValueChanged;

    protected virtual void OnCellValueChanged(object sender, GridViewCellEventArgs e)
    {
      if (this.CellValueChanged == null)
        return;
      this.CellValueChanged(sender, e);
    }

    [Browsable(true)]
    [Category("Action")]
    [Description("Fires when a toggle state changed of RadCheckBoxElement in header cell.")]
    public event HeaderCellToggleStateChangedEventHandler HeaderCellToggleStateChanged;

    protected virtual void OnHeaderCellToggleStateChanged(
      object sender,
      GridViewHeaderCellEventArgs args)
    {
      if (this.HeaderCellToggleStateChanged == null)
        return;
      this.HeaderCellToggleStateChanged(sender, args);
    }

    [Category("Action")]
    [Description("Fires command cell is clicked.")]
    [Browsable(true)]
    public event CommandCellClickEventHandler CommandCellClick;

    protected virtual void OnCommandCellClick(object sender, GridViewCellEventArgs args)
    {
      if (this.CommandCellClick == null)
        return;
      this.CommandCellClick(sender, args);
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Fires when the width of a column changes.")]
    public event ColumnWidthChangedEventHandler ColumnWidthChanged;

    protected virtual void OnColumnWidthChanged(object sender, ColumnWidthChangedEventArgs args)
    {
      if (this.ColumnWidthChanged == null)
        return;
      this.ColumnWidthChanged((object) ((GridViewColumn) sender).OwnerTemplate, args);
    }

    [Browsable(true)]
    [Description("Fires before the width of a column changes.")]
    [Category("Behavior")]
    public event ColumnWidthChangingEventHandler ColumnWidthChanging;

    protected virtual void OnColumnWidthChanging(object sender, ColumnWidthChangingEventArgs args)
    {
      if (this.ColumnWidthChanging == null)
        return;
      this.ColumnWidthChanging((object) ((GridViewColumn) sender).OwnerTemplate, args);
    }

    public event EventHandler ConditionalFormattingFormShown;

    protected internal virtual void OnConditionalFormattingFormShown(object sender, EventArgs e)
    {
      if (this.ConditionalFormattingFormShown == null)
        return;
      this.ConditionalFormattingFormShown(sender, e);
    }

    [Description("Fires when an external data-parsing or validation operation throws an exception, or when an attempt to commit data to a data source fails.")]
    [Browsable(true)]
    [Category("Behavior")]
    public event GridViewDataErrorEventHandler DataError;

    protected virtual void OnDataError(object sender, GridViewDataErrorEventArgs e)
    {
      if (this.DataError != null)
      {
        this.DataError((object) this, e);
      }
      else
      {
        int num = (int) MessageBox.Show(e.Exception.Message, "Data Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    [Description("Fires when the data group requires in virtual mode.")]
    [Browsable(true)]
    [Category("Data")]
    public event GridViewDataGroupEventHandler DataGroupNeeded;

    protected virtual void OnDataGroupNeeded(object sender, GridViewDataGroupEventArgs e)
    {
      if (this.DataGroupNeeded == null)
        return;
      this.DataGroupNeeded(sender, e);
    }

    [Description("Fires when evaluation of group summary is undergoing.")]
    [Browsable(true)]
    [Category("Behavior")]
    public event GroupSummaryEvaluateEventHandler GroupSummaryEvaluate;

    protected virtual void OnGroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
    {
      if (this.GroupSummaryEvaluate == null)
        return;
      this.GroupSummaryEvaluate(sender, e);
    }

    [Category("Behavior")]
    [Description("Fires before a context menu is shown.")]
    [Browsable(true)]
    public event ContextMenuOpeningEventHandler ContextMenuOpening;

    protected virtual void OnContextMenuOpening(object sender, ContextMenuOpeningEventArgs args)
    {
      if (this.ContextMenuOpening == null)
        return;
      this.ContextMenuOpening((object) this, args);
    }

    [Browsable(true)]
    [Description("Fires after data binding operation has finished.")]
    [Category("Data")]
    public event GridViewBindingCompleteEventHandler DataBindingComplete;

    protected virtual void OnDataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
    {
      if (this.DataBindingComplete == null)
        return;
      this.DataBindingComplete((object) this, e);
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      if (this.BindingContext != null)
        this.MasterTemplate.BindingContext = this.BindingContext;
      base.OnBindingContextChanged(e);
    }

    [Description("Fires when filter change is attempted. Allows cancellation of the event.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewCollectionChangingEventHandler FilterChanging;

    protected internal void OnFilterChanging(object sender, GridViewCollectionChangingEventArgs e)
    {
      if (this.FilterChanging == null)
        return;
      this.FilterChanging((object) this, e);
    }

    [Description("Fires when the filter expressions collections is converted a string filtering expression. This is the last stage where you can change the filtering expression and the only place where you can access it as a string.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewFilterExpressionChangedEventHandler FilterExpressionChanged;

    protected internal void OnFilterExpressionChanged(
      object sender,
      FilterExpressionChangedEventArgs args)
    {
      if (this.FilterExpressionChanged == null)
        return;
      this.FilterExpressionChanged(sender, args);
    }

    [Description("Fires when the filter is changed.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewCollectionChangedEventHandler FilterChanged;

    protected internal void OnFilterChanged(object sender, GridViewCollectionChangedEventArgs e)
    {
      if (this.FilterChanged != null)
        this.FilterChanged((object) this, e);
      if (e.NewItems == null || e.NewItems.Count <= 0)
        return;
      FilterDescriptor newItem = e.NewItems[0] as FilterDescriptor;
      if (newItem == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Filtered", (object) string.Format("{0}.{1}", (object) newItem.PropertyName, (object) newItem.ToString()));
    }

    [Description("Fires for custom filtering operation.")]
    [Browsable(true)]
    [Category("Data")]
    public event GridViewCustomFilteringEventHandler CustomFiltering;

    protected virtual void OnCustomFiltering(object sender, GridViewCustomFilteringEventArgs e)
    {
      if (this.CustomFiltering == null)
        return;
      this.CustomFiltering((object) this, e);
    }

    [Category("Action")]
    [Browsable(true)]
    [Description("Fires when a filter popup is showing.")]
    public event FilterPopupRequiredEventHandler FilterPopupRequired;

    protected virtual void OnFilterPopupRequired(object sender, FilterPopupRequiredEventArgs e)
    {
      if (this.FilterPopupRequired == null)
        return;
      this.FilterPopupRequired(sender, e);
    }

    [Description("Fires when after a filter popup is initialized.")]
    [Browsable(true)]
    [Category("Action")]
    public event FilterPopupInitializedEventHandler FilterPopupInitialized;

    protected virtual void OnFilterPopupInitialized(
      object sender,
      FilterPopupInitializedEventArgs e)
    {
      if (this.FilterPopupInitialized == null)
        return;
      this.FilterPopupInitialized(sender, e);
    }

    [Description("Fires when a composite filter dialog is being created.")]
    [Browsable(true)]
    [Category("Action")]
    public event GridViewCreateCompositeFilterDialogEventHandler CreateCompositeFilterDialog;

    protected virtual void OnCreateCompositeFilterDialog(
      object sender,
      GridViewCreateCompositeFilterDialogEventArgs e)
    {
      if (this.CreateCompositeFilterDialog == null)
        return;
      this.CreateCompositeFilterDialog(sender, e);
    }

    [Description("Fires when sorting is changing.")]
    [Category("Action")]
    public event GridViewCollectionChangingEventHandler SortChanging;

    protected virtual void OnSortChanging(object sender, GridViewCollectionChangingEventArgs e)
    {
      if (this.SortChanging == null)
        return;
      this.SortChanging(sender, e);
    }

    [Category("Action")]
    [Description("Fires when sorting is changed.")]
    public event GridViewCollectionChangedEventHandler SortChanged;

    protected virtual void OnSortChanged(object sender, GridViewCollectionChangedEventArgs e)
    {
      if (this.SortChanged != null)
        this.SortChanged(sender, e);
      if (e.NewItems == null || e.NewItems.Count <= 0)
        return;
      SortDescriptor newItem = e.NewItems[0] as SortDescriptor;
      if (newItem == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Sorted", (object) string.Format("{0}.{1}", (object) newItem.PropertyName, (object) newItem.Direction.ToString()));
    }

    [Description("Fires for custom sorting operation.")]
    [Browsable(true)]
    [Category("Data")]
    public event GridViewCustomSortingEventHandler CustomSorting;

    protected virtual void OnCustomSorting(object sender, GridViewCustomSortingEventArgs e)
    {
      if (this.CustomSorting == null)
        return;
      this.CustomSorting((object) this, e);
    }

    [Description("Fires when grouping is changing.")]
    [Category("Action")]
    public event GridViewCollectionChangingEventHandler GroupByChanging;

    protected virtual void OnGroupByChanging(object sender, GridViewCollectionChangingEventArgs e)
    {
      if (this.GroupByChanging == null)
        return;
      this.GroupByChanging((object) this, e);
    }

    [Description("Fires when grouping is changed.")]
    [Category("Action")]
    public event GridViewCollectionChangedEventHandler GroupByChanged;

    protected virtual void OnGroupByChanged(object sender, GridViewCollectionChangedEventArgs e)
    {
      if (this.GroupByChanged != null)
        this.GroupByChanged((object) this, e);
      if (e.NewItems == null || e.NewItems.Count <= 0)
        return;
      GroupDescriptor newItem = e.NewItems[0] as GroupDescriptor;
      if (newItem == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Grouped", (object) newItem.ToString());
    }

    [Description("Fires for custom grouping operation.")]
    [Browsable(true)]
    [Category("Data")]
    public event GridViewCustomGroupingEventHandler CustomGrouping;

    protected virtual void OnCustomGrouping(object sender, GridViewCustomGroupingEventArgs e)
    {
      if (this.CustomGrouping == null)
        return;
      this.CustomGrouping((object) this, e);
    }

    [Browsable(true)]
    [Description("Fires when the current view in RadGridView has changed.")]
    [Category("Action")]
    public event GridViewCurrentViewChangedEventHandler CurrentViewChanged
    {
      add
      {
        this.gridViewElement.CurrentViewChanged += value;
      }
      remove
      {
        this.gridViewElement.CurrentViewChanged -= value;
      }
    }

    [Description("Fires when a link from a GridViewHyperlinkColumn is opening. Allows cancellation of the event.")]
    public event HyperlinkOpeningEventHandler HyperlinkOpening;

    protected internal void OnHyperlinkOpening(object sender, HyperlinkOpeningEventArgs e)
    {
      if (this.HyperlinkOpening == null)
        return;
      this.HyperlinkOpening((object) this, e);
    }

    [Description("Fires when a link from a GridViewHyperlinkColumn is opened.")]
    public event HyperlinkOpenedEventHandler HyperlinkOpened;

    protected internal void OnHyperlinkOpened(object sender, HyperlinkOpenedEventArgs e)
    {
      if (this.HyperlinkOpened == null)
        return;
      this.HyperlinkOpened((object) this, e);
    }

    [Description("Fires when the content of a print cell is painted, allows custom painting.")]
    [Category("Action")]
    [Browsable(true)]
    public event PrintCellPaintEventHandler PrintCellPaint;

    protected internal virtual void OnPrintCellPaint(object sender, PrintCellPaintEventArgs e)
    {
      if (this.PrintCellPaint == null)
        return;
      this.PrintCellPaint(sender, e);
    }

    [Description("Fires when the content of a cell needs to be formatted for printing.")]
    [Browsable(true)]
    [Category("Action")]
    public event PrintCellFormattingEventHandler PrintCellFormatting;

    protected internal virtual void OnPrintCellFormatting(
      object sender,
      PrintCellFormattingEventArgs e)
    {
      if (this.PrintCellFormatting == null)
        return;
      this.PrintCellFormatting(sender, e);
    }

    [Description("Fires for hierarchy rows with more than one child views.")]
    [Browsable(true)]
    [Category("Action")]
    public event ChildViewPrintingEventHandler ChildViewPrinting;

    protected internal virtual void OnChildViewPrinting(object sender, ChildViewPrintingEventArgs e)
    {
      if (this.ChildViewPrinting == null)
        return;
      this.ChildViewPrinting(sender, e);
    }

    [Description("Occurs when the RadGridView has prepared appropriate data formats that represent the copy selection, added the copy selection formats to a DataObject, and is ready to either place the DataObject on the Clipboard.")]
    [Browsable(true)]
    [Category("Behavior")]
    public event GridViewClipboardEventHandler Copying;

    protected virtual void OnCopying(object sender, GridViewClipboardEventArgs e)
    {
      if (this.Copying == null)
        return;
      this.Copying((object) this, e);
    }

    [Description("Occurs when the RadGridView is ready to paste data.")]
    [Browsable(true)]
    [Category("Behavior")]
    public event GridViewClipboardEventHandler Pasting;

    protected virtual void OnPasting(object sender, GridViewClipboardEventArgs e)
    {
      if (this.Pasting == null)
        return;
      this.Pasting((object) this, e);
    }

    [Description("Occurs when the RadGridView prepares each cell's value to be placed on the Clipboard.")]
    [Browsable(true)]
    [Category("Behavior")]
    public event GridViewCellValueEventHandler CopyingCellClipboardContent;

    protected virtual void OnCopyingCellClipboardContent(
      object sender,
      GridViewCellValueEventArgs e)
    {
      if (this.CopyingCellClipboardContent == null)
        return;
      this.CopyingCellClipboardContent((object) this, e);
    }

    [Category("Behavior")]
    [Description("Occurs when the RadGridView is ready to paste data to individual cells.")]
    [Browsable(true)]
    public event GridViewCellValueEventHandler PastingCellClipboardContent;

    protected virtual void OnPastingCellClipboardContent(
      object sender,
      GridViewCellValueEventArgs e)
    {
      if (this.PastingCellClipboardContent == null)
        return;
      this.PastingCellClipboardContent((object) this, e);
    }

    public void ShowColumnChooser()
    {
      this.GridViewElement.ShowColumnChooser();
    }

    public void ShowColumnChooser(GridViewTemplate template)
    {
      this.GridViewElement.ShowColumnChooser(template);
    }

    public void HideColumnChooser()
    {
      this.GridViewElement.HideColumnChooser();
    }

    public void ClearSelection()
    {
      this.GridNavigator.ClearSelection();
    }

    public void SelectAll()
    {
      this.GridNavigator.SelectAll();
    }

    public int DisplayedRowCount(bool includePartialRow)
    {
      return this.GridViewElement.CurrentView.DisplayedRowCount(includePartialRow);
    }

    public int DisplayedColumnCount(bool includePartialColumn)
    {
      return this.GridViewElement.CurrentView.DisplayedColumnCount(includePartialColumn);
    }

    public void BestFitColumns()
    {
      this.MasterTemplate.BestFitColumns();
    }

    public void BestFitColumns(BestFitColumnMode mode)
    {
      this.MasterTemplate.BestFitColumns(mode);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element is RadScrollViewer)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    public override void BeginInit()
    {
      base.BeginInit();
      this.MasterTemplate.BeginInit();
    }

    public override void EndInit()
    {
      base.EndInit();
      this.MasterTemplate.EndInit();
    }

    protected override bool CanEditElementAtDesignTime(RadElement element)
    {
      if (!(element is GridTableElement) && !(element is GroupPanelElement) && !(element is RadScrollBarElement))
        return element is RootRadElement;
      return true;
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      this.AccessibilityRequested = true;
      return (AccessibleObject) new RadGridViewAccessibleObject(this);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetChildPropertyValue && request.Data != null)
      {
        if (request.ControlType == "Cell")
        {
          this.GetCellPropertyValueRecursivly(request, this.AccessibilityObject);
          return;
        }
        if (request.ControlType == "Row")
        {
          this.GetRowPropertyValueRecursivly(request);
          return;
        }
      }
      base.ProcessCodedUIMessage(ref request);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg != 123)
      {
        base.WndProc(ref m);
      }
      else
      {
        RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(this.PointToClient(Control.MousePosition));
        if (elementAtPoint != null)
        {
          GridCellElement gridCellElement = elementAtPoint as GridCellElement ?? elementAtPoint.FindAncestor<GridCellElement>();
          if (gridCellElement is GridHeaderCellElement && this.AllowColumnHeaderContextMenu || gridCellElement is GridDataCellElement && this.AllowCellContextMenu || gridCellElement is GridRowHeaderCellElement && this.AllowRowHeaderContextMenu)
            return;
        }
        base.WndProc(ref m);
      }
    }

    private bool GetCellPropertyValueRecursivly(IPCMessage request, AccessibleObject root)
    {
      int childCount = root.GetChildCount();
      for (int index = 0; index < childCount; ++index)
      {
        AccessibleObject child = root.GetChild(index);
        if (child != null && child.Name == (string) request.Data)
        {
          RadItemAccessibleObject accessibleObject = child as RadItemAccessibleObject;
          if (accessibleObject != null && accessibleObject.Name == (string) request.Data)
          {
            GridViewCellInfo owner1 = accessibleObject.Owner as GridViewCellInfo;
            if (owner1 != null)
            {
              if (request.Message == "ForeColor")
              {
                request.Data = (object) owner1.Style.ForeColor;
                return true;
              }
              if (request.Message == "BackColor")
              {
                request.Data = (object) owner1.Style.BackColor;
                return true;
              }
              if (request.Message == "Selected")
              {
                request.Data = (object) owner1.IsSelected;
                return true;
              }
              if (request.Message == "RowIndex")
              {
                request.Data = (object) this.ChildRows.IndexOf(owner1.RowInfo);
                return true;
              }
              if (request.Message == "ColumnIndex")
              {
                request.Data = (object) owner1.ColumnInfo.Index;
                return true;
              }
              if (!(request.Message == "Text"))
                return false;
              request.Data = owner1 == null ? (object) "" : owner1.Value;
              return true;
            }
            GridViewRowInfo owner2 = accessibleObject.Owner as GridViewRowInfo;
            if (owner2 != null && request.Message == "CellCount")
            {
              request.Data = (object) owner2.Cells.Count;
              return true;
            }
          }
        }
        if (this.GetCellPropertyValueRecursivly(request, child))
          return true;
      }
      return false;
    }

    private bool GetRowPropertyValueRecursivly(IPCMessage request)
    {
      for (int index = 0; index < this.RowCount; ++index)
      {
        if ((string) request.Data == "Row " + (object) index)
        {
          if (request.Message == "CellCount")
          {
            request.Data = (object) this.Rows[index].Cells.Count;
            return true;
          }
          if (request.Message == "Selected")
          {
            request.Data = (object) this.Rows[index].IsSelected;
            return true;
          }
          if (request.Message == "RowIndex")
          {
            request.Data = (object) index;
            return true;
          }
        }
      }
      return true;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsInEditMode
    {
      get
      {
        return this.GridViewElement.IsInEditMode;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IInputEditor ActiveEditor
    {
      get
      {
        return this.GridViewElement.ActiveEditor;
      }
    }

    public virtual bool BeginEdit()
    {
      return this.GridViewElement.BeginEdit();
    }

    public virtual bool EndEdit()
    {
      return this.GridViewElement.EndEdit();
    }

    public virtual bool CancelEdit()
    {
      return this.GridViewElement.CancelEdit();
    }

    public virtual bool CloseEditor()
    {
      return this.GridViewElement.CloseEditor();
    }

    public virtual ComponentXmlSerializationInfo GetDefaultXmlSerializationInfo()
    {
      PropertySerializationMetadataCollection serializationMetadata = new PropertySerializationMetadataCollection();
      serializationMetadata.Add(typeof (RadGridView), "Name", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadGridView), "Visible", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadGridView), "DataSource", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadGridView), "DataMember", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadControl), "ThemeName", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewTemplate), "DataSource", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewTemplate), "DataMember", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (Control), "Controls", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (Control), "DataBindings", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadComponentElement), "DataBindings", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadElement), "Style", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewComboBoxColumn), "DataSource", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewComboBoxColumn), "DataMember", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewDataColumn), "Filter", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadGridView), "Relations", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewDataColumn), "ConditionalFormattingObjectList", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content));
      serializationMetadata.Add(typeof (Control), "Tag", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (RadControl), "RootElement", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewColumnGroupRow), "Columns", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (GridViewColumnGroupRow), "ColumnNames", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content));
      serializationMetadata.Add(typeof (Control), "Size", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (Control), "Location", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (Control), "Dock", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (Control), "Anchor", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(typeof (Control), "CausesValidation", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      serializationMetadata.Add(new PropertySerializationMetadata(typeof (GridViewColumn).FullName, "IsVisible", true));
      return new ComponentXmlSerializationInfo(serializationMetadata);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ComponentXmlSerializationInfo XmlSerializationInfo
    {
      get
      {
        if (this.xmlSerializationInfo == null)
          this.xmlSerializationInfo = this.GetDefaultXmlSerializationInfo();
        return this.xmlSerializationInfo;
      }
      set
      {
        this.xmlSerializationInfo = value;
      }
    }

    protected virtual GridViewLayoutSerializer CreateGridViewLayoutSerializer(
      ComponentXmlSerializationInfo info)
    {
      return new GridViewLayoutSerializer(info);
    }

    public virtual void SaveLayout(XmlWriter xmlWriter)
    {
      GridViewLayoutSerializer layoutSerializer = this.CreateGridViewLayoutSerializer(this.XmlSerializationInfo);
      xmlWriter.WriteStartElement(nameof (RadGridView));
      layoutSerializer.WriteObjectElement(xmlWriter, (object) this);
      xmlWriter.WriteEndElement();
    }

    public virtual void SaveLayout(Stream stream)
    {
      GridViewLayoutSerializer layoutSerializer = this.CreateGridViewLayoutSerializer(this.XmlSerializationInfo);
      StreamWriter streamWriter = new StreamWriter(stream);
      XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) streamWriter);
      xmlTextWriter.WriteStartElement(nameof (RadGridView));
      layoutSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      xmlTextWriter.WriteEndElement();
      streamWriter.Flush();
    }

    public virtual void SaveLayout(string fileName)
    {
      GridViewLayoutSerializer layoutSerializer = this.CreateGridViewLayoutSerializer(this.XmlSerializationInfo);
      using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8))
      {
        xmlTextWriter.Formatting = Formatting.Indented;
        xmlTextWriter.WriteStartElement(nameof (RadGridView));
        layoutSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      }
    }

    public void LoadFrom(IDataReader dataReader)
    {
      this.MasterTemplate.LoadFrom(dataReader);
    }

    public virtual void LoadLayout(XmlReader xmlReader)
    {
      string currentColumnName = this.CurrentColumn != null ? this.CurrentColumn.Name : string.Empty;
      Hashtable columnCollectionState = this.BeginLoadLayout();
      this.EndEdit();
      bool flag = false;
      try
      {
        this.MasterTemplate.BeginUpdate();
        GridViewLayoutSerializer layoutSerializer = this.CreateGridViewLayoutSerializer(this.XmlSerializationInfo);
        xmlReader.Read();
        layoutSerializer.ReadObjectElement(xmlReader, (object) this);
        this.EndLoadLayout(columnCollectionState);
        this.EnsureHierarchyProviders((GridViewTemplate) this.MasterTemplate);
        flag = true;
      }
      catch (Exception ex)
      {
        this.OnDataError((object) this, new GridViewDataErrorEventArgs(ex, -1, -1, GridViewDataErrorContexts.Parsing));
      }
      finally
      {
        this.MasterTemplate.EndUpdate();
        this.EnsureAutoGeneratedComboBoxColumns();
        this.EnsureExcelLikeFiltering(this.MasterTemplate);
      }
      if (!flag)
        return;
      this.UpdateCurrentColumn(currentColumnName);
      this.OnLayoutLoaded((object) this, new LayoutLoadedEventArgs());
    }

    private Hashtable BeginLoadLayout()
    {
      Hashtable hashtable = new Hashtable(4);
      Stack<GridViewTemplate> gridViewTemplateStack = new Stack<GridViewTemplate>();
      gridViewTemplateStack.Push((GridViewTemplate) this.MasterTemplate);
      while (gridViewTemplateStack.Count > 0)
      {
        GridViewTemplate owner = gridViewTemplateStack.Pop();
        GridViewColumnCollection columnCollection = new GridViewColumnCollection(owner);
        foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) owner.Columns)
        {
          if (!column.IsDataBound || column is GridViewComboBoxColumn)
            columnCollection.Add(column);
        }
        hashtable.Add((object) owner, (object) columnCollection);
        foreach (GridViewTemplate template in (Collection<GridViewTemplate>) owner.Templates)
          gridViewTemplateStack.Push(template);
      }
      return hashtable;
    }

    private void EndLoadLayout(Hashtable columnCollectionState)
    {
      foreach (GridViewTemplate key in (IEnumerable) columnCollectionState.Keys)
      {
        GridViewColumnCollection columnCollection = (GridViewColumnCollection) columnCollectionState[(object) key];
        if (columnCollection.Count != 0)
        {
          foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) key.Columns)
          {
            if (!string.IsNullOrEmpty(column.Expression))
            {
              foreach (GridViewRowInfo gridViewRowInfo in key.ListSource)
              {
                if (columnCollection.Contains(column.Name))
                  gridViewRowInfo.Cache.Remove((GridViewColumn) columnCollection[column.Name]);
              }
            }
            if (!column.IsDataBound && columnCollection.Contains(column.Name))
            {
              foreach (GridViewRowInfo gridViewRowInfo in key.ListSource)
                gridViewRowInfo.Cache.ReplaceKey(columnCollection[column.Name], column);
            }
            if (column is GridViewComboBoxColumn && columnCollection.Contains(column.Name))
              ((GridViewComboBoxColumn) column).DataSource = ((GridViewComboBoxColumn) columnCollection[column.Name]).DataSource;
          }
        }
      }
    }

    private void UpdateCurrentColumn(string currentColumnName)
    {
      this.MasterTemplate.ResetCurrentColumn();
      GridViewColumn column = (GridViewColumn) this.MasterTemplate.Columns[currentColumnName];
      if (column == null || !this.TableElement.ViewElement.RowLayout.RenderColumns.Contains(column))
        return;
      this.CurrentColumn = column;
    }

    [Category("Action")]
    [Browsable(true)]
    [Description("Fires when the layout is loaded.")]
    public event LayoutLoadedEventHandler LayoutLoaded;

    protected internal void OnLayoutLoaded(object sender, LayoutLoadedEventArgs e)
    {
      if (this.LayoutLoaded == null)
        return;
      this.LayoutLoaded((object) this, e);
    }

    public virtual void LoadLayout(string fileName)
    {
      if (!File.Exists(fileName))
      {
        int num = (int) MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(fileName))
        {
          this.CreateGridViewLayoutSerializer(this.XmlSerializationInfo);
          using (XmlTextReader xmlTextReader = new XmlTextReader((TextReader) streamReader))
            this.LoadLayout((XmlReader) xmlTextReader);
        }
      }
    }

    public virtual void LoadLayout(Stream stream)
    {
      if (stream == null || stream.Length <= 0L)
        return;
      if (stream.Position == stream.Length)
        stream.Position = 0L;
      StreamReader streamReader = new StreamReader(stream);
      this.CreateGridViewLayoutSerializer(this.XmlSerializationInfo);
      this.LoadLayout((XmlReader) new XmlTextReader((TextReader) streamReader));
    }

    private void EnsureHierarchyProviders(GridViewTemplate template)
    {
      foreach (GridViewTemplate template1 in (Collection<GridViewTemplate>) template.Templates)
      {
        template1.EnsureHierarchyProvider();
        this.EnsureHierarchyProviders(template1);
      }
    }

    private void EnsureExcelLikeFiltering(MasterGridViewTemplate masterTemplate)
    {
      Queue<GridViewTemplate> gridViewTemplateQueue = new Queue<GridViewTemplate>();
      gridViewTemplateQueue.Enqueue((GridViewTemplate) masterTemplate);
      while (gridViewTemplateQueue.Count > 0)
      {
        GridViewTemplate template1 = gridViewTemplateQueue.Dequeue();
        foreach (GridViewTemplate template2 in (Collection<GridViewTemplate>) template1.Templates)
          gridViewTemplateQueue.Enqueue(template2);
        List<FilterDescriptor> filterDescriptorList = new List<FilterDescriptor>((IEnumerable<FilterDescriptor>) template1.FilterDescriptors);
        template1.FilterDescriptors.Clear();
        template1.ExcelFilteredColumns.Clear();
        this.AddTemplateFilters(template1, (IList<FilterDescriptor>) filterDescriptorList);
      }
    }

    private void AddTemplateFilters(GridViewTemplate template, IList<FilterDescriptor> filters)
    {
      foreach (FilterDescriptor filter in (IEnumerable<FilterDescriptor>) filters)
      {
        string propertyName = filter.PropertyName;
        if (string.IsNullOrEmpty(propertyName) && filter is CompositeFilterDescriptor)
        {
          List<FilterDescriptor> filterDescriptorList = new List<FilterDescriptor>((IEnumerable<FilterDescriptor>) (filter as CompositeFilterDescriptor).FilterDescriptors);
          this.AddTemplateFilters(template, (IList<FilterDescriptor>) filterDescriptorList);
        }
        else
        {
          GridViewColumnValuesCollection valuesWithFilter = template.Columns[propertyName].DistinctValuesWithFilter;
          template.ExcelFilteredColumns.Add(template.Columns[propertyName]);
          template.Columns[propertyName].CreateSnapshot();
          template.FilterDescriptors.Add(filter);
        }
      }
    }

    private void EnsureAutoGeneratedComboBoxColumns()
    {
      Queue<GridViewTemplate> gridViewTemplateQueue = new Queue<GridViewTemplate>();
      gridViewTemplateQueue.Enqueue((GridViewTemplate) this.MasterTemplate);
      while (gridViewTemplateQueue.Count > 0)
      {
        GridViewTemplate gridViewTemplate = gridViewTemplateQueue.Dequeue();
        foreach (GridViewTemplate template in (Collection<GridViewTemplate>) gridViewTemplate.Templates)
          gridViewTemplateQueue.Enqueue(template);
        foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) gridViewTemplate.Columns)
        {
          if (column.IsAutoGenerated && column.DataType.IsEnum && column is GridViewComboBoxColumn)
            new EnumBinder(column.DataType)
            {
              Target = ((object) column),
              Source = column.DataType
            }.Target = (object) column;
        }
      }
    }

    private void SuspendComponentBehaviorMouseEvents()
    {
      this.disableMouseEventsState = new bool?(this.Behavior.DisableMouseEvents);
      this.Behavior.DisableMouseEvents = false;
    }

    private void ResumeComponentBehaviorMouseEvents()
    {
      if (!this.disableMouseEventsState.HasValue)
        return;
      this.Behavior.DisableMouseEvents = this.disableMouseEventsState.Value;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.mouseDownLocation = e.Location;
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseDown(e);
      this.ResumeComponentBehaviorMouseEvents();
      this.mouseDownOnScrollBar = this.IsPointOverScrollBar(e.Location);
      if (this.EnableKineticScrolling && this.CurrentRow != null && !this.mouseDownOnScrollBar)
      {
        bool flag = false;
        foreach (IRowView rowView in this.GridViewElement.GetRowViews(this.CurrentRow.ViewInfo))
        {
          GridTableElement gridTableElement = rowView as GridTableElement;
          if (gridTableElement != null)
          {
            flag |= gridTableElement.ScrollBehavior.IsRunning;
            gridTableElement.ScrollBehavior.MouseDown(e.Location);
          }
        }
        if (flag)
          return;
      }
      if (!this.GridBehaviorShouldHandleMouseEvent())
        return;
      this.GridBehavior.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.mouseDownOnScrollBar = false;
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseUp(e);
      this.ResumeComponentBehaviorMouseEvents();
      if (this.EnableKineticScrolling && this.CurrentRow != null && !this.IsPointOverScrollBar(e.Location))
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (IRowView rowView in this.GridViewElement.GetRowViews(this.CurrentRow.ViewInfo))
        {
          GridTableElement gridTableElement = rowView as GridTableElement;
          if (gridTableElement != null)
          {
            flag1 |= gridTableElement.ScrollBehavior.IsRunning;
            gridTableElement.ScrollBehavior.MouseUp(e.Location);
            flag2 |= gridTableElement.ScrollBehavior.IsRunning;
          }
        }
        if (flag2 || flag1)
          return;
      }
      if (!this.GridBehaviorShouldHandleMouseEvent())
        return;
      this.GridBehavior.OnMouseUp(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseClick(e);
      this.ResumeComponentBehaviorMouseEvents();
      RadElement elementAtPoint1 = this.ElementTree.GetElementAtPoint(this.mouseDownLocation);
      if (elementAtPoint1 == null || elementAtPoint1.FindAncestor<RadScrollBarElement>() != null)
        return;
      GridCellElement elementAtPoint2 = GridVisualElement.GetElementAtPoint<GridCellElement>((RadElementTree) this.ElementTree, e.Location);
      if (elementAtPoint2 != null && !(elementAtPoint2 is GridDetailViewCellElement) && (GridVisualElement.GetElementAtPoint<GridCellElement>((RadElementTree) this.ElementTree, this.mouseDownLocation) == elementAtPoint2 && e.Button == MouseButtons.Left))
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCellEventArgs>(EventDispatcher.CellClick, (object) elementAtPoint2, new GridViewCellEventArgs(elementAtPoint2.RowInfo, elementAtPoint2.ColumnInfo, this.GridViewElement.ActiveEditor));
      this.MasterTemplate.EventDispatcher.ResumeEvent(EventDispatcher.CellClick);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseDoubleClick(e);
      this.ResumeComponentBehaviorMouseEvents();
      if (this.GridViewElement.EditorManager.IsInEditMode && this.CurrentCell != null && this.CurrentCell.ControlBoundingRectangle.Contains(e.Location) || this.GridBehaviorShouldHandleMouseEvent() && this.GridBehavior.OnMouseDoubleClick(e))
        return;
      RadElement elementAtPoint1 = this.ElementTree.GetElementAtPoint(e.Location);
      if (elementAtPoint1 == null || elementAtPoint1.FindAncestor<RadScrollBarElement>() != null)
        return;
      GridCellElement elementAtPoint2 = GridVisualElement.GetElementAtPoint<GridCellElement>((RadElementTree) this.ElementTree, e.Location);
      if (elementAtPoint2 == null)
        return;
      elementAtPoint2.CallDoDoubleClick((EventArgs) e);
      this.OnCellDoubleClick((object) elementAtPoint2, new GridViewCellEventArgs(elementAtPoint2.RowInfo, elementAtPoint2.ColumnInfo, this.gridViewElement.ActiveEditor));
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseEnter(e);
      this.ResumeComponentBehaviorMouseEvents();
      if (!this.GridBehaviorShouldHandleMouseEvent())
        return;
      this.GridBehavior.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseLeave(e);
      this.ResumeComponentBehaviorMouseEvents();
      if (!this.GridBehaviorShouldHandleMouseEvent())
        return;
      this.GridBehavior.OnMouseLeave(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseMove(e);
      this.ResumeComponentBehaviorMouseEvents();
      bool flag1 = false;
      if (this.EnableKineticScrolling && !this.IsInEditMode && (this.CurrentRow != null && !this.IsPointOverScrollBar(e.Location)) && !this.mouseDownOnScrollBar)
      {
        foreach (IRowView rowView in this.GridViewElement.GetRowViews(this.CurrentRow.ViewInfo))
        {
          GridTableElement gridTableElement = rowView as GridTableElement;
          if (gridTableElement != null)
          {
            gridTableElement.ScrollBehavior.MouseMove(e.Location);
            flag1 |= gridTableElement.ScrollBehavior.IsRunning;
          }
        }
      }
      bool flag2 = flag1 & this.EnableKineticScrolling;
      if (!this.GridBehaviorShouldHandleMouseEvent() || this.GridViewElement.EditorManager.IsInEditMode || flag2)
        return;
      this.GridBehavior.OnMouseMove(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      this.SuspendComponentBehaviorMouseEvents();
      base.OnMouseWheel(e);
      this.ResumeComponentBehaviorMouseEvents();
      if (this.Behavior != null && this.Behavior.ToolTip != null && this.Behavior.ToolTip.Active)
        this.Behavior.ToolTip.Hide((IWin32Window) this);
      bool flag = this.ActiveEditor is RadTextBoxEditor;
      if (this.IsDisposed || this.GridBehavior == null || this.IsInEditMode && !flag || this.Behavior.DisableMouseEvents)
        return;
      this.GridBehavior.OnMouseWheel(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!this.IsDisposed && this.GridBehavior != null && (!this.IsInEditMode && this.GridBehavior.ProcessKeyDown(e)))
        return;
      base.OnKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (!this.IsDisposed && this.GridBehavior != null && (!this.IsInEditMode && this.GridBehavior.ProcessKeyPress(e)))
        return;
      base.OnKeyPress(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (!this.IsDisposed && this.GridBehavior != null && (!this.IsInEditMode && this.GridBehavior.ProcessKeyUp(e)))
        return;
      base.OnKeyUp(e);
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (!this.IsDisposed && (this.IsInEditMode || !this.GridViewElement.StandardTab) && (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift)))
      {
        KeyEventArgs keys = new KeyEventArgs(keyData);
        if (this.GridBehavior != null && this.GridBehavior.ProcessKey(keys))
          return true;
      }
      if (this.IsInEditMode && keyData == Keys.Escape)
      {
        this.GridBehavior.ProcessKey(new KeyEventArgs(keyData));
        return true;
      }
      if (this.IsInEditMode && keyData == Keys.Return)
        return false;
      return base.ProcessDialogKey(keyData);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.InvalidateMultiSelection();
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.InvalidateMultiSelection();
      if (this.CurrentRow == null)
        return;
      (this.CurrentRow.DataBoundItem as IEditableObject)?.BeginEdit();
    }

    protected virtual void InvalidateMultiSelection()
    {
      if (this.IsInEditMode)
        return;
      this.CurrentView.CurrentRow?.UpdateInfo();
      if (this.SelectionMode == GridViewSelectionMode.FullRowSelect)
      {
        foreach (GridViewRowInfo selectedRow in (ReadOnlyCollection<GridViewRowInfo>) this.SelectedRows)
          selectedRow.InvalidateRow();
      }
      else
      {
        if (this.SelectionMode != GridViewSelectionMode.CellSelect)
          return;
        foreach (GridViewCellInfo selectedCell in (ReadOnlyCollection<GridViewCellInfo>) this.SelectedCells)
          selectedCell.RowInfo.InvalidateRow();
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      if (!(propertyName == "AutoSize"))
        return;
      if (!this.AutoSize)
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = true;
      }
      else
      {
        this.RootElement.StretchHorizontally = false;
        this.RootElement.StretchVertically = false;
      }
    }

    protected override void OnValidating(CancelEventArgs e)
    {
      GridViewEditManager editorManager = this.gridViewElement.EditorManager;
      editorManager.IsValidating = true;
      GridViewNewRowInfo gridViewNewRowInfo = (GridViewNewRowInfo) null;
      if (this.CurrentView != null && this.CurrentRow != null)
      {
        if (this.IsInEditMode)
        {
          BaseGridEditor activeEditor = this.ActiveEditor as BaseGridEditor;
          if (activeEditor != null && !activeEditor.EndEditOnLostFocus)
          {
            base.OnValidating(e);
            editorManager.IsValidating = false;
            return;
          }
          e.Cancel = !this.EndEdit();
        }
        else
        {
          gridViewNewRowInfo = this.CurrentRow as GridViewNewRowInfo;
          if (gridViewNewRowInfo != null && gridViewNewRowInfo.IsModified)
          {
            RowValidatingEventArgs args1 = new RowValidatingEventArgs(this.CurrentRow);
            this.MasterTemplate.EventDispatcher.RaiseEvent<RowValidatingEventArgs>(EventDispatcher.RowValidating, (object) this, args1);
            e.Cancel = args1.Cancel;
            if (!args1.Cancel)
            {
              RowValidatedEventArgs args2 = new RowValidatedEventArgs(this.CurrentRow);
              this.MasterTemplate.EventDispatcher.RaiseEvent<RowValidatedEventArgs>(EventDispatcher.RowValidated, (object) this, args2);
              e.Cancel = !gridViewNewRowInfo.CallOnEndEdit();
            }
          }
        }
        if (gridViewNewRowInfo == null && !e.Cancel)
        {
          RowValidatingEventArgs args1 = new RowValidatingEventArgs(this.CurrentRow);
          this.MasterTemplate.EventDispatcher.RaiseEvent<RowValidatingEventArgs>(EventDispatcher.RowValidating, (object) this, args1);
          e.Cancel = args1.Cancel;
          if (!args1.Cancel)
          {
            RowValidatedEventArgs args2 = new RowValidatedEventArgs(this.CurrentRow);
            this.MasterTemplate.EventDispatcher.RaiseEvent<RowValidatedEventArgs>(EventDispatcher.RowValidated, (object) this, args2);
          }
        }
      }
      base.OnValidating(e);
      editorManager.IsValidating = false;
    }

    protected override void OnValidated(EventArgs e)
    {
      base.OnValidated(e);
      if (this.CurrentRow == null)
        return;
      (this.CurrentRow.DataBoundItem as IEditableObject)?.EndEdit();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      if (!this.IsInEditMode || this.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill || this.AutoSize)
        return;
      GridViewEditManager editorManager = this.EditorManager;
      bool whenValidationFails = editorManager.CloseEditorWhenValidationFails;
      editorManager.CloseEditorWhenValidationFails = true;
      editorManager.CloseEditor();
      this.vScrollBarValue = this.TableElement.VScrollBar.Value;
      this.hScrollBarValue = this.TableElement.HScrollBar.Value;
      this.MasterTemplate.Refresh();
      this.TableElement.VScrollBar.Value = this.vScrollBarValue;
      this.TableElement.HScrollBar.Value = this.hScrollBarValue;
      editorManager.CloseEditorWhenValidationFails = whenValidationFails;
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Prior:
        case Keys.Next:
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
        case Keys.Left | Keys.Shift:
        case Keys.Up | Keys.Shift:
        case Keys.Right | Keys.Shift:
        case Keys.Down | Keys.Shift:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    private bool GridBehaviorShouldHandleMouseEvent()
    {
      if (!this.IsDisposed && this.GridBehavior != null)
        return !this.Behavior.DisableMouseEvents;
      return false;
    }

    private bool IsPointOverScrollBar(Point location)
    {
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(location);
      if (elementAtPoint is RadScrollBarElement)
        return true;
      if (elementAtPoint != null)
        return elementAtPoint.Parent is RadScrollBarElement;
      return false;
    }

    public IDisposable DeferRefresh()
    {
      return this.MasterTemplate.DeferRefresh();
    }

    public void BeginUpdate()
    {
      this.MasterTemplate.BeginUpdate();
    }

    public void EndUpdate(bool notify)
    {
      this.MasterTemplate.EndUpdate(notify);
    }

    public void EndUpdate()
    {
      this.MasterTemplate.EndUpdate();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets a value indicating how the RadGridView is printed.")]
    [Browsable(true)]
    public GridPrintStyle PrintStyle
    {
      get
      {
        return this.printStyle;
      }
      set
      {
        this.printStyle = value;
      }
    }

    public virtual void Print()
    {
      this.Print(false);
    }

    public virtual void Print(bool showPrinterSettings)
    {
      RadPrintDocument document = new RadPrintDocument();
      this.Print(showPrinterSettings, document);
    }

    public virtual void Print(bool showPrinterSettings, RadPrintDocument document)
    {
      if (document == null)
        return;
      document.AssociatedObject = (IPrintable) this;
      if (showPrinterSettings)
      {
        if (new PrintDialog()
        {
          Document = ((PrintDocument) document),
          AllowPrintToFile = true,
          AllowCurrentPage = true,
          AllowSelection = true,
          AllowSomePages = true,
          UseEXDialog = true
        }.ShowDialog() != DialogResult.OK)
          return;
        document.Print();
      }
      else
        document.Print();
    }

    public virtual void PrintPreview()
    {
      this.PrintPreview(new RadPrintDocument());
    }

    public virtual void PrintPreview(RadPrintDocument document)
    {
      if (document == null)
        return;
      document.AssociatedObject = (IPrintable) this;
      RadPrintPreviewDialog printPreviewDialog = new RadPrintPreviewDialog(document);
      printPreviewDialog.ThemeName = this.ThemeName;
      int num = (int) printPreviewDialog.ShowDialog();
    }

    int IPrintable.BeginPrint(RadPrintDocument sender, PrintEventArgs args)
    {
      this.columnWidths = new Dictionary<GridViewColumn, int>();
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.Columns)
        this.columnWidths.Add(column, column.Width);
      if (this.PrintStyle.GridView != this)
        this.PrintStyle.GridView = this;
      this.currentRowCache = this.CurrentRow;
      this.vScrollBarValue = this.TableElement.VScrollBar.Value;
      this.hScrollBarValue = this.TableElement.HScrollBar.Value;
      this.pagingState = this.EnablePaging;
      this.currentPageIndex = this.MasterTemplate.PageIndex;
      this.CurrentRow = (GridViewRowInfo) null;
      this.TableElement.SuspendLayout();
      if (this.EnablePaging && this.PrintStyle.PrintAllPages)
        this.EnablePaging = false;
      this.PrintStyle.PrintTraverser.Reset();
      Margins margins = sender.DefaultPageSettings.Margins;
      int left = margins.Left;
      int y = sender.HeaderHeight + margins.Top;
      int height = sender.DefaultPageSettings.Bounds.Height - (sender.HeaderHeight + sender.FooterHeight + margins.Top + margins.Bottom);
      int width = sender.DefaultPageSettings.Bounds.Width - (margins.Left + margins.Right);
      return this.PrintStyle.GetNumberOfPages(new Rectangle(left, y, width, height));
    }

    bool IPrintable.EndPrint(RadPrintDocument sender, PrintEventArgs args)
    {
      this.PrintStyle.Reset();
      if (this.RightToLeft == RightToLeft.Yes)
      {
        foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.Columns)
          column.Width = (int) Math.Round((double) this.columnWidths[column] / (double) this.GridViewElement.DpiScaleFactor.Width);
      }
      try
      {
        this.CurrentRow = this.currentRowCache;
      }
      catch (NullReferenceException ex)
      {
      }
      if (this.pagingState && this.PrintStyle.PrintAllPages)
      {
        this.EnablePaging = true;
        this.MasterTemplate.MoveToPage(this.currentPageIndex);
      }
      this.TableElement.ResumeLayout(true, true);
      this.TableElement.VScrollBar.Value = this.vScrollBarValue;
      this.TableElement.HScrollBar.Value = this.hScrollBarValue;
      return true;
    }

    bool IPrintable.PrintPage(
      int pageNumber,
      RadPrintDocument sender,
      PrintPageEventArgs args)
    {
      Margins margins = sender.DefaultPageSettings.Margins;
      int left = margins.Left;
      int y = sender.HeaderHeight + margins.Top;
      int height = sender.DefaultPageSettings.Bounds.Height - (sender.HeaderHeight + sender.FooterHeight + margins.Top + margins.Bottom);
      int width = sender.DefaultPageSettings.Bounds.Width - (margins.Left + margins.Right);
      this.PrintStyle.DrawPage(new Rectangle(left, y, width, height), args.Graphics, pageNumber);
      return sender.PrintedPage < sender.PageCount;
    }

    Form IPrintable.GetSettingsDialog(RadPrintDocument document)
    {
      return this.PrintSettingsDialogFactory.CreateDialog(document);
    }

    [DefaultValue(GridViewClipboardCutMode.EnableWithoutHeaderText)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewClipboardCutMode ClipboardCutMode
    {
      get
      {
        return this.MasterTemplate.ClipboardCutMode;
      }
      set
      {
        this.MasterTemplate.ClipboardCutMode = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(GridViewClipboardCopyMode.EnableWithoutHeaderText)]
    public GridViewClipboardCopyMode ClipboardCopyMode
    {
      get
      {
        return this.MasterTemplate.ClipboardCopyMode;
      }
      set
      {
        this.MasterTemplate.ClipboardCopyMode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(GridViewClipboardPasteMode.Enable)]
    [Browsable(true)]
    public GridViewClipboardPasteMode ClipboardPasteMode
    {
      get
      {
        return this.MasterTemplate.ClipboardPasteMode;
      }
      set
      {
        this.MasterTemplate.ClipboardPasteMode = value;
      }
    }

    public virtual DataObject GetClipboardContent()
    {
      return this.MasterTemplate.GetClipboardContent();
    }

    public virtual void Copy()
    {
      this.MasterTemplate.Copy();
    }

    public virtual void Paste()
    {
      this.MasterTemplate.Paste();
    }

    public virtual void Cut()
    {
      this.MasterTemplate.Cut();
    }
  }
}
