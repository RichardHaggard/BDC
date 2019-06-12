// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGridViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Interfaces;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class RadGridViewElement : GridVisualElement, IRadServiceProvider, IGridViewEventListener
  {
    private Point columnChooserLocation = Point.Empty;
    private RadGridViewNewRowEnterKeyMode newRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow;
    private RadGridViewBeginEditMode beginEditMode = RadGridViewBeginEditMode.BeginEditOnKeystrokeOrF2;
    private bool showCellErrors = true;
    private bool showRowErrors = true;
    private bool showNoDataText = true;
    private bool showGroupPanel = true;
    private RadSortOrder columnChooserSortOrder = RadSortOrder.None;
    private const float MinimumFinalHeight = 25f;
    private const float MinimumFinalWidth = 25f;
    private MasterGridViewTemplate template;
    private IContextMenuManager contextMenuManager;
    private GridViewEditManager viewEditManager;
    private DockLayoutPanel panel;
    private IRowView currentView;
    private GridViewColumnChooser columnChooser;
    private LightVisualElement titleLabel;
    private GroupPanelElement groupPanelElement;
    private PagingPanelElement pagingPanelElement;
    private Dictionary<string, RadService> services;
    private IGridNavigator navigator;
    private IGridBehavior gridBehavior;
    private GridExpandAnimationType groupExpandAnimationType;
    private RadGridViewEnterKeyMode enterKeyMode;
    private bool enableCustomDrawing;
    private bool hideSelection;
    private bool standardTab;
    private bool autoSizeRows;
    private bool useScrollbarsInHierarchy;
    private bool updateSplitMode;
    private bool synchronizeCurrentRowInSplitMode;
    private RadGridViewSplitMode splitMode;

    public RadGridViewElement()
    {
      this.navigator = (IGridNavigator) new BaseGridNavigator();
      this.navigator.Initialize(this);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.services = new Dictionary<string, RadService>();
      this.RegisterService((RadService) new RadGridViewDragDropService(this));
      this.template = this.CreateTemplate();
      this.template.SynchronizationService.AddListener((IGridViewEventListener) this);
      this.template.PropertyChanged += new PropertyChangedEventHandler(this.Template_PropertyChanged);
    }

    protected virtual MasterGridViewTemplate CreateTemplate()
    {
      return new MasterGridViewTemplate();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      LocalizationProvider<RadGridLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadGridLocalizationProvider_CurrentProviderChanged);
      this.panel = new DockLayoutPanel();
      this.panel.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.panel.LastChildFill = true;
      this.panel.ChildrenChanged += new ChildrenChangedEventHandler(this.Panel_ChildrenChanged);
      this.Children.Add((RadElement) this.panel);
      this.titleLabel = new LightVisualElement();
      this.titleLabel.DrawText = true;
      this.titleLabel.Visibility = ElementVisibility.Collapsed;
      this.panel.Children.Add((RadElement) this.titleLabel);
      this.groupPanelElement = this.CreateGroupPanelElement();
      this.groupPanelElement.ServiceProvider = (IRadServiceProvider) this;
      this.Panel.Children.Add((RadElement) this.groupPanelElement);
      this.pagingPanelElement = this.CreatePagingPanelElement();
      this.Panel.Children.Add((RadElement) this.pagingPanelElement);
      this.SetViewElement();
      int num1 = (int) this.Panel.Children[0].SetValue(DockLayoutPanel.DockProperty, (object) Dock.Top);
      int num2 = (int) this.Panel.Children[1].SetValue(DockLayoutPanel.DockProperty, (object) Dock.Top);
      int num3 = (int) this.Panel.Children[2].SetValue(DockLayoutPanel.DockProperty, (object) Dock.Bottom);
    }

    private void SetViewElement()
    {
      IRowView rowView = this.GetRowView(this.template.MasterViewInfo);
      Color color = Color.Empty;
      bool flag = true;
      if (rowView != null)
      {
        GridTableElement gridTableElement = rowView as GridTableElement;
        if (gridTableElement != null)
        {
          if (gridTableElement.GetValueSource(GridTableElement.AlternatingRowColorProperty) == ValueSource.Local)
            color = gridTableElement.AlternatingRowColor;
          if (gridTableElement.GetValueSource(GridTableElement.EnableHotTrackingProperty) == ValueSource.Local)
            flag = gridTableElement.EnableHotTracking;
        }
        ((DisposableObject) rowView).Dispose();
      }
      IRowView viewUiElement = this.template.ViewDefinition.CreateViewUIElement(this.template.MasterViewInfo);
      if (viewUiElement != null)
      {
        GridTableElement gridTableElement = viewUiElement as GridTableElement;
        if (gridTableElement != null)
        {
          if (!color.IsEmpty)
            gridTableElement.AlternatingRowColor = color;
          gridTableElement.EnableHotTracking = flag;
        }
      }
      this.panel.Children.Add((RadElement) viewUiElement);
    }

    protected virtual GroupPanelElement CreateGroupPanelElement()
    {
      return new GroupPanelElement();
    }

    protected virtual PagingPanelElement CreatePagingPanelElement()
    {
      return new PagingPanelElement();
    }

    protected override void DisposeManagedResources()
    {
      LocalizationProvider<RadGridLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadGridLocalizationProvider_CurrentProviderChanged);
      if (this.columnChooser != null)
      {
        this.columnChooser.Dispose();
        this.columnChooser = (GridViewColumnChooser) null;
      }
      this.template.SynchronizationService.RemoveListener((IGridViewEventListener) this);
      this.template.PropertyChanged -= new PropertyChangedEventHandler(this.Template_PropertyChanged);
      this.EditorManager = (GridViewEditManager) null;
      if (this.contextMenuManager != null && this.contextMenuManager is IDisposable)
        ((IDisposable) this.contextMenuManager).Dispose();
      base.DisposeManagedResources();
    }

    public LightVisualElement TitleLabelElement
    {
      get
      {
        return this.titleLabel;
      }
    }

    public string TitleText
    {
      get
      {
        return this.titleLabel.Text;
      }
      set
      {
        this.titleLabel.Text = value;
        if (value != null && value != string.Empty)
          this.titleLabel.Visibility = ElementVisibility.Visible;
        else
          this.titleLabel.Visibility = ElementVisibility.Collapsed;
      }
    }

    public Dock TitlePosition
    {
      get
      {
        return (Dock) this.titleLabel.GetValue(DockLayoutPanel.DockProperty);
      }
      set
      {
        int num = (int) this.titleLabel.SetValue(DockLayoutPanel.DockProperty, (object) value);
        if (value == Dock.Left || value == Dock.Right)
          this.titleLabel.TextOrientation = Orientation.Vertical;
        else
          this.titleLabel.TextOrientation = Orientation.Horizontal;
      }
    }

    public GridExpandAnimationType GroupExpandAnimationType
    {
      get
      {
        return this.groupExpandAnimationType;
      }
      set
      {
        if (this.groupExpandAnimationType == value)
          return;
        this.groupExpandAnimationType = value;
        this.OnNotifyPropertyChanged(nameof (GroupExpandAnimationType));
      }
    }

    public GroupPanelElement GroupPanelElement
    {
      get
      {
        return this.groupPanelElement;
      }
    }

    [Description("Gets or sets a value indicating whether the group panel will show scroll bars or it will expand to show all group headers.")]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool ShowGroupPanelScrollbars
    {
      get
      {
        return this.GroupPanelElement.ShowScrollBars;
      }
      set
      {
        this.GroupPanelElement.ShowScrollBars = value;
      }
    }

    public PagingPanelElement PagingPanelElement
    {
      get
      {
        return this.pagingPanelElement;
      }
    }

    public bool UseScrollbarsInHierarchy
    {
      get
      {
        return this.useScrollbarsInHierarchy;
      }
      set
      {
        if (this.useScrollbarsInHierarchy == value)
          return;
        this.useScrollbarsInHierarchy = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (UseScrollbarsInHierarchy)));
      }
    }

    public DockLayoutPanel Panel
    {
      get
      {
        return this.panel;
      }
    }

    public IRowView CurrentView
    {
      get
      {
        return this.currentView;
      }
      set
      {
        if (value == this.currentView)
          return;
        this.template.CurrentView = value?.ViewInfo;
        this.currentView = value;
      }
    }

    public MasterGridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }

    [Description("Gets or sets an instance of BaseGridBehavior or the instance that implements IGridBehavior interface.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Browsable(false)]
    public virtual IGridBehavior GridBehavior
    {
      get
      {
        if (this.gridBehavior == null)
        {
          this.gridBehavior = (IGridBehavior) new BaseGridBehavior();
          this.gridBehavior.Initialize(this);
        }
        return this.gridBehavior;
      }
      set
      {
        if (value == null)
          return;
        this.gridBehavior = value;
        this.gridBehavior.Initialize(this);
      }
    }

    [Description("Gets or sets a value indicating whether row height in a RadGridView will expand for multiline cell text")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AutoSizeRows
    {
      get
      {
        return this.autoSizeRows;
      }
      set
      {
        if (this.autoSizeRows == value)
          return;
        this.autoSizeRows = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (AutoSizeRows)));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public GridTableElement TableElement
    {
      get
      {
        foreach (RadElement child in this.Panel.Children)
        {
          GridTableElement gridTableElement = child as GridTableElement;
          if (gridTableElement != null)
            return gridTableElement;
        }
        return (GridTableElement) null;
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
        return this.beginEditMode;
      }
      set
      {
        this.beginEditMode = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsInEditMode
    {
      get
      {
        return this.EditorManager.IsInEditMode;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewRowInfo CurrentRow
    {
      get
      {
        return this.Template.CurrentRow;
      }
      set
      {
        this.Template.CurrentRow = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewColumn CurrentColumn
    {
      get
      {
        return this.Template.CurrentView.ViewTemplate.CurrentColumn;
      }
      set
      {
        this.Template.CurrentView.ViewTemplate.CurrentColumn = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridDataCellElement CurrentCell
    {
      get
      {
        return this.CurrentView.CurrentCell as GridDataCellElement;
      }
    }

    [Description("Gets or sets a value indicating whether the selected item in the control remains highlighted when the control loses focus.")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool HideSelection
    {
      get
      {
        return this.hideSelection;
      }
      set
      {
        if (this.hideSelection == value)
          return;
        this.hideSelection = value;
        if (this.CurrentView != null && this.CurrentView.CurrentRow != null)
          this.CurrentView.CurrentRow.UpdateInfo();
        if (!this.Template.MultiSelect)
          return;
        this.CurrentView.UpdateView();
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets value indicating whether the GridGroupPanel is visible.")]
    public bool ShowGroupPanel
    {
      get
      {
        return this.showGroupPanel;
      }
      set
      {
        if (this.showGroupPanel == value)
          return;
        this.showGroupPanel = value;
        this.groupPanelElement.UpdateVisibility();
        this.OnNotifyPropertyChanged(nameof (ShowGroupPanel));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the PaintCell and PaintRow events are enabled.")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool EnableCustomDrawing
    {
      get
      {
        return this.enableCustomDrawing;
      }
      set
      {
        this.enableCustomDrawing = value;
      }
    }

    public IGridNavigator Navigator
    {
      get
      {
        return this.navigator;
      }
      set
      {
        if (value == this.navigator)
          return;
        this.navigator = value;
        if (this.navigator == null)
          return;
        this.navigator.Initialize(this);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets value specifying the behavior when the user presses Enter while adding new row.")]
    [Browsable(true)]
    [DefaultValue(RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow)]
    public RadGridViewNewRowEnterKeyMode NewRowEnterKeyMode
    {
      get
      {
        return this.newRowEnterKeyMode;
      }
      set
      {
        this.newRowEnterKeyMode = value;
        this.OnNotifyPropertyChanged(nameof (NewRowEnterKeyMode));
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets value specifying the behavior when the user presses Enter.")]
    [DefaultValue(RadGridViewEnterKeyMode.None)]
    public RadGridViewEnterKeyMode EnterKeyMode
    {
      get
      {
        return this.enterKeyMode;
      }
      set
      {
        this.enterKeyMode = value;
        this.OnNotifyPropertyChanged(nameof (EnterKeyMode));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether to show cell errors.")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool ShowCellErrors
    {
      get
      {
        return this.showCellErrors;
      }
      set
      {
        if (this.showCellErrors == value)
          return;
        this.showCellErrors = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowCellErrors)));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to show row errors.")]
    [Browsable(true)]
    [Category("Appearance")]
    public bool ShowRowErrors
    {
      get
      {
        return this.showRowErrors;
      }
      set
      {
        if (this.showRowErrors == value)
          return;
        this.showRowErrors = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowRowErrors)));
      }
    }

    [Description("Gets or sets value which defines the default behavior of the grid when Tab is pressed.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool StandardTab
    {
      get
      {
        return this.standardTab;
      }
      set
      {
        if (this.standardTab == value)
          return;
        this.standardTab = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (StandardTab)));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(RadGridViewSplitMode.None)]
    public RadGridViewSplitMode SplitMode
    {
      get
      {
        return this.splitMode;
      }
      set
      {
        if (this.splitMode == value)
          return;
        this.splitMode = value;
        if (!this.GridControl.IsInitializing && this.ElementState == ElementState.Loaded)
          this.UpdateSplitMode();
        else
          this.updateSplitMode = true;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (SplitMode)));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Description("Gets or sets the text to use when there is no data.")]
    [Category("Appearance")]
    public bool ShowNoDataText
    {
      get
      {
        return this.showNoDataText;
      }
      set
      {
        if (this.showNoDataText == value)
          return;
        this.showNoDataText = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowNoDataText)));
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public bool SynchronizeCurrentRowInSplitMode
    {
      get
      {
        return this.synchronizeCurrentRowInSplitMode;
      }
      set
      {
        if (this.synchronizeCurrentRowInSplitMode == value)
          return;
        this.synchronizeCurrentRowInSplitMode = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (SynchronizeCurrentRowInSplitMode)));
      }
    }

    [Description("Fires when a cell needs to be created.")]
    [Category("Appearance")]
    [Browsable(true)]
    public event GridViewCreateCellEventHandler CreateCell;

    protected virtual void OnCreateCell(object sender, GridViewCreateCellEventArgs e)
    {
      if (this.CreateCell == null)
        return;
      this.CreateCell(sender, e);
    }

    internal void CallCreateCell(GridViewCreateCellEventArgs e)
    {
      this.OnCreateCell(this.GetDefaultEventSender(), e);
    }

    private object GetDefaultEventSender()
    {
      return (object) (this.ElementTree.Control as RadGridView) ?? (object) this;
    }

    [Category("Appearance")]
    [Description("Fires when a row needs to be created.")]
    [Browsable(true)]
    public event GridViewCreateRowEventHandler CreateRow;

    protected virtual void OnCreateRow(object sender, GridViewCreateRowEventArgs e)
    {
      if (this.CreateRow == null)
        return;
      this.CreateRow(sender, e);
    }

    internal void CallCreateRow(GridViewCreateRowEventArgs e)
    {
      this.OnCreateRow(this.GetDefaultEventSender(), e);
    }

    [Category("Action")]
    [Description("Fires when a data row is invalidated and needs to be formatted.")]
    [Browsable(true)]
    public event RowFormattingEventHandler RowFormatting;

    protected void OnRowFormatting(object sender, RowFormattingEventArgs e)
    {
      if (this.RowFormatting == null)
        return;
      this.RowFormatting(sender, e);
    }

    internal void CallRowFormatting(object sender, RowFormattingEventArgs e)
    {
      this.OnRowFormatting(sender, e);
    }

    [Description("Fires when a grid row is invalidated and needs to be formatted.")]
    [Category("Action")]
    [Browsable(true)]
    public event RowFormattingEventHandler ViewRowFormatting;

    protected void OnViewRowFormatting(object sender, RowFormattingEventArgs e)
    {
      if (this.ViewRowFormatting == null)
        return;
      this.ViewRowFormatting(sender, e);
    }

    internal void CallViewRowFormatting(object sender, RowFormattingEventArgs e)
    {
      this.OnViewRowFormatting(sender, e);
    }

    [Description("Fires when the content of a data cell needs to be formatted for display.")]
    [Category("Action")]
    [Browsable(true)]
    public event CellFormattingEventHandler CellFormatting;

    protected internal virtual void OnCellFormatting(object sender, CellFormattingEventArgs e)
    {
      if (this.CellFormatting == null)
        return;
      this.CellFormatting(sender, e);
    }

    [Category("Action")]
    [Description("Fires when the content of a cell needs to be formatted for display.")]
    [Browsable(true)]
    public event CellFormattingEventHandler ViewCellFormatting;

    protected internal virtual void OnViewCellFormatting(object sender, CellFormattingEventArgs e)
    {
      if (this.ViewCellFormatting == null)
        return;
      this.ViewCellFormatting(sender, e);
    }

    [Category("Action")]
    [Description("Fires when the current view in RadGridView has changed.")]
    [Browsable(true)]
    public event GridViewCurrentViewChangedEventHandler CurrentViewChanged;

    protected virtual void OnCurrentViewChanged(GridViewCurrentViewChangedEventArgs e)
    {
      GridViewCurrentViewChangedEventHandler currentViewChanged = this.CurrentViewChanged;
      if (currentViewChanged == null)
        return;
      currentViewChanged((object) this, e);
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Fires when a ColumnChooserCreated is created.")]
    public event ColumnChooserCreatedEventHandler ColumnChooserCreated;

    protected virtual void OnColumnChooserCreated(object sender, ColumnChooserCreatedEventArgs e)
    {
      if (this.ColumnChooserCreated == null)
        return;
      this.ColumnChooserCreated(sender, e);
    }

    protected virtual void OnUserChangedCurrentRow(object sender, EventArgs e)
    {
      this.Navigator.ClearSelection();
      if (this.Template.CurrentRow == null)
        return;
      this.Template.CurrentRow.IsSelected = true;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.template.BindingContext = this.ElementTree.Control.BindingContext;
      this.ElementTree.Control.SizeChanged += new EventHandler(this.Control_SizeChanged);
      this.template.OwnerSite = this.ElementTree.Control.Site;
    }

    private void Panel_ChildrenChanged(object sender, ChildrenChangedEventArgs e)
    {
      IGridView child = e.Child as IGridView;
      if (child != null)
      {
        if (e.ChangeOperation == ItemsChangeOperation.Inserted)
        {
          child.Initialize(this, this.template.MasterViewInfo);
          if (this.currentView == null)
          {
            IRowView rowView = child as IRowView;
            if (rowView != null)
              this.currentView = rowView;
          }
        }
        if (e.ChangeOperation == ItemsChangeOperation.Removed)
        {
          child.Detach();
          if (child == this.CurrentView)
            this.CurrentView = (IRowView) null;
        }
      }
      if (e.ChangeOperation != ItemsChangeOperation.Cleared)
        return;
      this.CurrentView = (IRowView) null;
    }

    private void ColumnChooser_LocationChanged(object sender, EventArgs e)
    {
      this.columnChooserLocation = this.ColumnChooser.Location;
    }

    private void RadGridLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.Template.SynchronizationService.DispatchEvent(new GridViewEvent((object) this, (object) this, (object[]) null, new GridViewEventInfo(KnownEvents.LocalizationProviderChanged, GridEventType.UI, GridEventDispatchMode.Post)));
    }

    private void Control_SizeChanged(object sender, EventArgs e)
    {
      this.InitalizeSplitGridSizes();
    }

    private void Template_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ViewDefinition") || !this.Template.SynchronizationService.IsDispatchSuspended)
        return;
      this.SetViewElement();
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      if (!this.updateSplitMode)
        return;
      this.UpdateSplitMode();
      this.updateSplitMode = false;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewEditManager EditorManager
    {
      get
      {
        if (this.viewEditManager == null)
          this.viewEditManager = this.CreateEditorManager();
        return this.viewEditManager;
      }
      set
      {
        if (value == this.viewEditManager)
          return;
        if (this.viewEditManager != null)
          this.viewEditManager.Dispose();
        if (value != null && value.GridViewElement != this)
          throw new InvalidOperationException("The GridViewElement associated with the GridViewEditManager is different than this instance.");
        this.viewEditManager = value;
      }
    }

    protected virtual GridViewEditManager CreateEditorManager()
    {
      return new GridViewEditManager(this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IInputEditor ActiveEditor
    {
      get
      {
        return this.EditorManager.ActiveEditor;
      }
    }

    public bool BeginEdit()
    {
      return this.EditorManager.BeginEdit();
    }

    public bool EndEdit()
    {
      return this.EditorManager.EndEdit();
    }

    public bool CancelEdit()
    {
      return this.EditorManager.CancelEdit();
    }

    public bool CloseEditor()
    {
      return this.EditorManager.CloseEditor();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadSortOrder ColumnChooserSortOrder
    {
      get
      {
        return this.columnChooserSortOrder;
      }
      set
      {
        if (this.columnChooserSortOrder == value)
          return;
        this.columnChooserSortOrder = value;
        this.ColumnChooser.SortOrder = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewColumnChooser ColumnChooser
    {
      get
      {
        if (this.columnChooser != null && this.columnChooser.IsDisposed)
        {
          this.columnChooser.LocationChanged -= new EventHandler(this.ColumnChooser_LocationChanged);
          this.columnChooser = (GridViewColumnChooser) null;
        }
        if (this.columnChooser == null)
        {
          this.columnChooser = new GridViewColumnChooser((GridViewTemplate) this.Template, this);
          ColumnChooserCreatedEventArgs e = new ColumnChooserCreatedEventArgs(this.columnChooser);
          this.OnColumnChooserCreated(this.GetDefaultEventSender(), e);
          this.columnChooser = e.ColumnChooser;
          this.columnChooser.LocationChanged += new EventHandler(this.ColumnChooser_LocationChanged);
        }
        this.columnChooser.SortOrder = this.columnChooserSortOrder;
        return this.columnChooser;
      }
    }

    public void ShowColumnChooser()
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      if (this.IsColumnChooserVisible)
        this.ColumnChooser.Hide();
      if (this.columnChooserLocation != Point.Empty)
        this.ColumnChooser.Location = this.columnChooserLocation;
      Screen screen = Screen.FromControl(this.ElementTree.Control);
      if (!screen.Bounds.Contains(this.ColumnChooser.Location))
        this.ColumnChooser.Location = Point.Add(screen.Bounds.Location, new Size(50, 50));
      this.ColumnChooser.Owner = this.ElementTree.Control.FindForm();
      this.ColumnChooser.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.ColumnChooser.Scale(this.DpiScaleFactor);
      this.ColumnChooser.Show();
    }

    internal bool IsColumnChooserVisible
    {
      get
      {
        if (this.columnChooser != null)
          return this.columnChooser.Visible;
        return false;
      }
    }

    public void ShowColumnChooser(GridViewTemplate template)
    {
      this.ColumnChooser.Template = template;
      this.ShowColumnChooser();
    }

    public void HideColumnChooser()
    {
      if (!this.IsColumnChooserVisible)
        return;
      this.ColumnChooser.Hide();
      this.ColumnChooser.Template = (GridViewTemplate) null;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IContextMenuManager ContextMenuManager
    {
      get
      {
        if (this.contextMenuManager == null)
          this.contextMenuManager = (IContextMenuManager) new GridViewContextMenuManager(this);
        return this.contextMenuManager;
      }
    }

    public void RegisterService(RadService service)
    {
      string name = service.Name;
      RadService radService = (RadService) null;
      this.services.TryGetValue(name, out radService);
      if (radService != null)
      {
        if (radService == service)
          return;
        this.services[name] = service;
      }
      else
        this.services.Add(name, service);
    }

    public T GetService<T>() where T : RadService
    {
      foreach (RadService radService in this.services.Values)
      {
        if (radService is T)
          return radService as T;
      }
      return default (T);
    }

    public RadDragDropService GetDragDropService()
    {
      foreach (RadService radService in this.services.Values)
      {
        if (radService is RadDragDropService)
          return radService as RadDragDropService;
      }
      return (RadDragDropService) null;
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
      if (GridViewSynchronizationService.IsTemplatePropertyChangingEvent(eventData))
        return this.ProcessTemplatePropertyChanging(eventData);
      if (eventData.Sender != this.template)
        return (GridViewEventResult) null;
      if (GridViewSynchronizationService.IsTemplatePropertyChangedEvent(eventData))
        return this.ProcessTemplatePropertyChangedEvent(eventData);
      if (eventData.Info.Id == KnownEvents.ViewChanged)
        return this.ProcessViewChangedEvent(eventData);
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessTemplatePropertyChanging(
      GridViewEvent eventData)
    {
      PropertyChangingEventArgsEx changingEventArgsEx = eventData.Arguments[0] as PropertyChangingEventArgsEx;
      if (this.IsInEditMode && (changingEventArgsEx.PropertyName == "DataSource" || changingEventArgsEx.PropertyName == "DataMember"))
        this.EndEdit();
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessViewChangedEvent(
      GridViewEvent eventData)
    {
      DataViewChangedEventArgs changedEventArgs = eventData.Arguments[0] as DataViewChangedEventArgs;
      if (changedEventArgs.Action == ViewChangedAction.Reset)
        this.groupPanelElement.UpdateVisibility();
      else if (changedEventArgs.Action == ViewChangedAction.CurrentViewChanged)
      {
        GridViewInfo oldView = (GridViewInfo) null;
        if (this.currentView != null)
          oldView = this.currentView.ViewInfo;
        IRowView rowView = this.GetRowView((GridViewInfo) changedEventArgs.NewItems[0]);
        if (rowView != null)
          this.currentView = rowView;
        this.OnCurrentViewChanged(new GridViewCurrentViewChangedEventArgs(oldView, (GridViewInfo) changedEventArgs.NewItems[0]));
      }
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessTemplatePropertyChangedEvent(
      GridViewEvent eventData)
    {
      PropertyChangedEventArgs changedEventArgs = eventData.Arguments[0] as PropertyChangedEventArgs;
      if (changedEventArgs.PropertyName == "ViewDefinition")
        this.SetViewElement();
      if (changedEventArgs.PropertyName == "EnableGrouping")
        this.groupPanelElement.UpdateVisibility();
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

    public IRowView GetRowView(GridViewInfo viewInfo)
    {
      foreach (RadElement child in this.Panel.Children)
      {
        IRowView view = child as IRowView;
        if (view != null)
        {
          if (view.ViewInfo == viewInfo)
            return view;
          IRowView rowView = this.GetRowView(view, viewInfo);
          if (rowView != null)
            return rowView;
        }
      }
      return (IRowView) null;
    }

    private IRowView GetRowView(IRowView view, GridViewInfo viewInfo)
    {
      if (view.ViewInfo == viewInfo)
        return view;
      foreach (IRowView childView in view.ChildViews)
      {
        IRowView rowView = this.GetRowView(childView, viewInfo);
        if (rowView != null)
          return rowView;
      }
      return (IRowView) null;
    }

    public IEnumerable<IRowView> GetRowViews(GridViewInfo viewInfo)
    {
      List<IRowView> results = new List<IRowView>();
      foreach (RadElement child in this.Panel.Children)
      {
        IRowView view = child as IRowView;
        if (view != null)
        {
          if (view.ViewInfo == viewInfo)
            results.Add(view);
          else
            this.GetRowViews(view, viewInfo, results);
        }
      }
      return (IEnumerable<IRowView>) results;
    }

    private void GetRowViews(IRowView view, GridViewInfo viewInfo, List<IRowView> results)
    {
      if (view.ViewInfo == viewInfo)
      {
        results.Add(view);
      }
      else
      {
        foreach (IRowView childView in view.ChildViews)
          this.GetRowViews(childView, viewInfo, results);
      }
    }

    private void UpdateSplitMode()
    {
      for (int index = this.Panel.Children.Count - 1; index >= 0; --index)
      {
        RadElement child = this.Panel.Children[index];
        if (!(child is GroupPanelElement))
          this.Panel.Children.Remove(child);
      }
      RadElement viewUiElement1 = (RadElement) this.template.ViewDefinition.CreateViewUIElement(this.template.MasterViewInfo);
      this.panel.Children.Add(viewUiElement1);
      if (this.splitMode == RadGridViewSplitMode.None)
      {
        viewUiElement1.AutoSize = true;
      }
      else
      {
        RadElement viewUiElement2 = (RadElement) this.template.ViewDefinition.CreateViewUIElement(this.template.MasterViewInfo);
        this.panel.Children.Add(viewUiElement2);
        if (this.splitMode == RadGridViewSplitMode.Horizontal)
        {
          int num1 = (int) viewUiElement1.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Top);
          int num2 = (int) viewUiElement2.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Bottom);
        }
        else
        {
          int num1 = (int) viewUiElement1.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Left);
          int num2 = (int) viewUiElement2.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Right);
        }
        this.InitalizeSplitGridSizes();
        this.Panel.InvalidateMeasure();
      }
    }

    internal void InitalizeSplitGridSizes()
    {
      if (this.splitMode == RadGridViewSplitMode.None)
        return;
      RectangleF clientRectangle = this.GetClientRectangle((SizeF) this.Size);
      int height = (int) clientRectangle.Height - this.GroupPanelElement.Size.Height;
      foreach (RadElement child in this.Panel.Children)
      {
        GridTableElement gridTableElement = child as GridTableElement;
        if (gridTableElement != null)
          gridTableElement.ForcedDesiredSize = this.splitMode != RadGridViewSplitMode.Horizontal ? (SizeF) new Size((int) clientRectangle.Width / 2, height) : (SizeF) new Size((int) clientRectangle.Width, height / 2);
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if ((double) finalSize.Height > 25.0 && (double) finalSize.Width > 25.0)
        return base.ArrangeOverride(finalSize);
      return finalSize;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.Template.BeginUpdate();
      this.Template.EndUpdate();
    }
  }
}
