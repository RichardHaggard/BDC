// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadListViewElement : LightVisualElement, IDataItemSource
  {
    private string displayMember = "";
    private string valueMember = "";
    private string checkedMember = "";
    private Dictionary<System.Type, IInputEditor> cachedEditors = new Dictionary<System.Type, IInputEditor>();
    private CheckBoxesAlignment checkBoxesAlignment = CheckBoxesAlignment.Center;
    private bool allowEdit = true;
    private bool allowRemove = true;
    private bool allowColumnReorder = true;
    private bool showColumnHeaders = true;
    private bool hotTracking = true;
    private float headerHeight = 35f;
    private bool setThroughCode = true;
    private ListViewType viewType;
    private BaseListViewElement viewElement;
    private ListViewDataItemCollection items;
    private ListViewListSource listSource;
    private BindingContext bindingContext;
    private ListViewDataItemGroupCollection groups;
    private ListViewFilterDescriptorCollection filterDescriptors;
    private ListViewColumnCollection columns;
    private ColumnResizingBehavior resizingBehavior;
    private ListViewDragDropService dragDropService;
    private CheckBoxesPosition checkBoxesPosition;
    protected object cachedOldValue;
    private bool allowDragDrop;
    private bool showCheckBoxes;
    private bool showGroups;
    private bool enableColumnSort;
    private bool enableKineticScrolling;
    private bool isEndingEdit;
    private bool isEditorInitializing;
    private bool enableCustomGrouping;
    private bool enableSorting;
    private bool enableFiltering;
    private bool enableGrouping;
    private bool multiSelect;
    private bool enableLassoSelection;
    private bool showGridLines;
    private bool threeStateMode;
    private IInputEditor activeEditor;
    private ListViewSelectedItemCollection selectedItems;
    private ListViewCheckedItemCollection checkedItems;
    private ListViewDataItem currentItem;
    private ListViewDataItem selectedItem;
    private ListViewDetailColumn currentColumn;
    private bool itemsMeasureValid;
    private int oldSelectedIndex;
    private CheckOnClickMode checkOnClickMode;
    internal bool isSynchronizing;

    public event EventHandler<ListViewGroupEventArgs> GroupExpanded;

    public event EventHandler<ListViewGroupCancelEventArgs> GroupExpanding;

    public event ListViewItemCancelEventHandler SelectedItemChanging;

    public event EventHandler SelectedItemsChanged;

    public event EventHandler SelectedItemChanged;

    public event EventHandler SelectedIndexChanged;

    public event EventHandler ViewTypeChanged;

    public event ViewTypeChangingEventHandler ViewTypeChanging;

    public event ListViewItemMouseEventHandler ItemMouseDown;

    public event ListViewItemMouseEventHandler ItemMouseUp;

    public event ListViewItemMouseEventHandler ItemMouseMove;

    public event ListViewItemEventHandler ItemMouseHover;

    public event ListViewItemEventHandler ItemMouseEnter;

    public event ListViewItemEventHandler ItemMouseLeave;

    public event ListViewItemEventHandler ItemMouseClick;

    public event ListViewItemEventHandler ItemMouseDoubleClick;

    public event ListViewItemCancelEventHandler ItemCheckedChanging;

    public event ListViewItemEventHandler ItemCheckedChanged;

    public event ListViewVisualItemEventHandler VisualItemFormatting;

    public event ListViewItemCreatingEventHandler ItemCreating;

    public event ListViewVisualItemCreatingEventHandler VisualItemCreating;

    public event ListViewCellFormattingEventHandler CellFormatting;

    public event ListViewItemEventHandler ItemDataBound;

    public event ListViewItemEventHandler CurrentItemChanged;

    public event ListViewItemChangingEventHandler CurrentItemChanging;

    public event ListViewItemEditorRequiredEventHandler EditorRequired;

    public event ListViewItemEditingEventHandler ItemEditing;

    public event ListViewItemEditorInitializedEventHandler EditorInitialized;

    public event ListViewItemEditedEventHandler ItemEdited;

    public event EventHandler ValidationError;

    public event ListViewItemValidatingEventHandler ItemValidating;

    public event ListViewItemValueChangedEventHandler ItemValueChanged;

    public event ListViewItemValueChangingEventHandler ItemValueChanging;

    public event ListViewColumnCreatingEventHandler ColumnCreating;

    public event ListViewCellElementCreatingEventHandler CellCreating;

    public event ListViewItemCancelEventHandler ItemRemoving;

    public event ListViewItemEventHandler ItemRemoved;

    protected internal virtual bool OnSelectedItemChanging(ListViewItemCancelEventArgs args)
    {
      if (this.SelectedItemChanging != null)
        this.SelectedItemChanging((object) this, args);
      return args.Cancel;
    }

    protected virtual void OnSelectedIndexChanged()
    {
      if (this.SelectedIndexChanged != null)
        this.SelectedIndexChanged((object) this, EventArgs.Empty);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "SelectionChanged", (object) this.SelectedIndex);
    }

    protected virtual void OnSelectedItemChanged(ListViewDataItem item)
    {
      if (this.SelectedItemChanged == null)
        return;
      this.SelectedItemChanged((object) this, (EventArgs) new ListViewItemEventArgs(item));
    }

    protected internal virtual void OnSelectedItemsChanged()
    {
      if (this.SelectedItemsChanged == null)
        return;
      this.SelectedItemsChanged((object) this, EventArgs.Empty);
    }

    protected virtual bool OnViewTypeChanging(ViewTypeChangingEventArgs args)
    {
      if (this.ViewTypeChanging != null)
        this.ViewTypeChanging((object) this, args);
      return args.Cancel;
    }

    protected virtual void OnViewTypeChanged()
    {
      if (this.ViewTypeChanged == null)
        return;
      this.ViewTypeChanged((object) this, EventArgs.Empty);
    }

    protected internal virtual void OnCellFormatting(ListViewCellFormattingEventArgs args)
    {
      if (this.CellFormatting == null)
        return;
      this.CellFormatting((object) this, args);
    }

    protected internal virtual bool OnItemCheckedChanging(ListViewItemCancelEventArgs args)
    {
      if (this.ItemCheckedChanging != null)
        this.ItemCheckedChanging((object) this, args);
      return args.Cancel;
    }

    protected internal virtual void OnItemCheckedChanged(ListViewItemEventArgs args)
    {
      if (this.ItemCheckedChanged == null)
        return;
      this.ItemCheckedChanged((object) this, args);
    }

    protected internal virtual void OnItemMouseEnter(ListViewItemEventArgs args)
    {
      if (this.ItemMouseEnter == null)
        return;
      this.ItemMouseEnter((object) this, args);
    }

    protected internal virtual void OnItemMouseLeave(ListViewItemEventArgs args)
    {
      if (this.ItemMouseLeave == null)
        return;
      this.ItemMouseLeave((object) this, args);
    }

    protected internal virtual void OnItemMouseDown(ListViewItemMouseEventArgs args)
    {
      if (this.ItemMouseDown == null)
        return;
      this.ItemMouseDown((object) this, args);
    }

    protected internal virtual void OnItemMouseUp(ListViewItemMouseEventArgs args)
    {
      if (this.ItemMouseUp == null)
        return;
      this.ItemMouseUp((object) this, args);
    }

    protected internal virtual void OnItemMouseMove(ListViewItemMouseEventArgs args)
    {
      if (this.ItemMouseMove == null)
        return;
      this.ItemMouseMove((object) this, args);
    }

    protected internal virtual void OnItemMouseHover(ListViewItemEventArgs args)
    {
      if (this.ItemMouseHover == null)
        return;
      this.ItemMouseHover((object) this, args);
    }

    protected internal virtual void OnItemMouseClick(ListViewItemEventArgs args)
    {
      if (this.ItemMouseClick == null)
        return;
      this.ItemMouseClick((object) this, args);
    }

    protected internal virtual void OnItemMouseDoubleClick(ListViewItemEventArgs args)
    {
      if (this.ItemMouseDoubleClick == null)
        return;
      this.ItemMouseDoubleClick((object) this, args);
    }

    protected virtual void OnCurrecntItemChanged(ListViewItemEventArgs args)
    {
      if (this.CurrentItemChanged == null)
        return;
      this.CurrentItemChanged((object) this, args);
    }

    protected virtual bool OnCurrentItemChanging(ListViewItemChangingEventArgs args)
    {
      if (this.CurrentItemChanging != null)
        this.CurrentItemChanging((object) this, args);
      return args.Cancel;
    }

    protected internal virtual void OnValueChanged(ListViewItemValueChangedEventArgs args)
    {
      if (this.ItemValueChanged == null)
        return;
      this.ItemValueChanged((object) this, args);
    }

    protected internal virtual bool OnValueChanging(ListViewItemValueChangingEventArgs args)
    {
      if (this.ItemValueChanging != null)
        this.ItemValueChanging((object) this, args);
      return args.Cancel;
    }

    protected virtual void OnValidationError(EventArgs args)
    {
      if (this.ValidationError == null)
        return;
      this.ValidationError((object) this, args);
    }

    protected virtual void OnValueValidating(ListViewItemValidatingEventArgs args)
    {
      if (this.ItemValidating == null)
        return;
      this.ItemValidating((object) this, args);
    }

    protected virtual void OnEdited(ListViewItemEditedEventArgs args)
    {
      if (this.ItemEdited != null)
        this.ItemEdited((object) this, args);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Edited", args.VisualItem != null ? (object) args.VisualItem.Text : (object) "");
    }

    protected internal virtual void OnItemDataBound(ListViewDataItem item)
    {
      if (this.ItemDataBound == null)
        return;
      this.ItemDataBound((object) this, new ListViewItemEventArgs(item));
    }

    protected internal virtual void OnVisualItemFormatting(BaseListViewVisualItem item)
    {
      if (this.VisualItemFormatting == null)
        return;
      this.VisualItemFormatting((object) this, new ListViewVisualItemEventArgs(item));
    }

    protected virtual void OnDataItemCreating(ListViewItemCreatingEventArgs args)
    {
      if (this.ItemCreating == null)
        return;
      this.ItemCreating((object) this, args);
    }

    protected internal virtual void OnVisualItemCreating(ListViewVisualItemCreatingEventArgs args)
    {
      if (this.VisualItemCreating == null)
        return;
      this.VisualItemCreating((object) this, args);
    }

    protected virtual void OnColumnCreating(ListViewColumnCreatingEventArgs args)
    {
      if (this.ColumnCreating == null)
        return;
      this.ColumnCreating((object) this, args);
    }

    protected internal virtual void OnCellCreating(ListViewCellElementCreatingEventArgs args)
    {
      if (this.CellCreating == null)
        return;
      this.CellCreating((object) this, args);
    }

    protected internal virtual bool OnItemRemoving(ListViewItemCancelEventArgs args)
    {
      if (this.ItemRemoving == null)
        return false;
      this.ItemRemoving((object) this, args);
      return args.Cancel;
    }

    protected internal virtual void OnItemRemoved(ListViewItemEventArgs args)
    {
      if (this.ItemRemoved == null)
        return;
      this.ItemRemoved((object) this, args);
    }

    internal virtual bool OnGroupExpanding(ListViewDataItemGroup group)
    {
      if (this.GroupExpanding == null)
        return false;
      ListViewGroupCancelEventArgs e = new ListViewGroupCancelEventArgs(group);
      this.GroupExpanding((object) this, e);
      return e.Cancel;
    }

    internal virtual void OnGroupExpanded(ListViewDataItemGroup group)
    {
      if (this.GroupExpanded == null)
        return;
      this.GroupExpanded((object) this, new ListViewGroupEventArgs(group));
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether column names which differ only in the casing are allowed.")]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool CaseSensitiveColumnNames
    {
      get
      {
        return this.columns.AllowCaseSensitiveNames;
      }
      set
      {
        this.columns.AllowCaseSensitiveNames = value;
        this.listSource.UseCaseSensitiveFieldNames = value;
        this.SortDescriptors.UseCaseSensitiveFieldNames = value;
        this.FilterDescriptors.UseCaseSensitiveFieldNames = value;
        this.GroupDescriptors.UseCaseSensitiveFieldNames = value;
      }
    }

    public CheckBoxesPosition CheckBoxesPosition
    {
      get
      {
        if (this.ViewType == ListViewType.DetailsView)
          return CheckBoxesPosition.Left;
        return this.checkBoxesPosition;
      }
      set
      {
        if (this.checkBoxesPosition == value)
          return;
        this.checkBoxesPosition = value;
        this.Update(RadListViewElement.UpdateModes.RefreshLayout);
        this.SynchronizeVisualItems();
      }
    }

    public CheckBoxesAlignment CheckBoxesAlignment
    {
      get
      {
        return this.checkBoxesAlignment;
      }
      set
      {
        if (this.checkBoxesAlignment == value)
          return;
        this.checkBoxesAlignment = value;
        this.SynchronizeVisualItems();
      }
    }

    public ListViewDragDropService DragDropService
    {
      get
      {
        return this.dragDropService;
      }
      set
      {
        this.dragDropService = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the last added item in the RadListView DataSource will be selected by the control.")]
    [Browsable(true)]
    public bool SelectLastAddedItem
    {
      get
      {
        return this.DataView.ChangeCurrentOnAdd;
      }
      set
      {
        if (this.DataView.ChangeCurrentOnAdd == value)
          return;
        this.DataView.ChangeCurrentOnAdd = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(ScrollState.AutoHide)]
    [Description("Gets or sets the display state of the horizontal scrollbar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.ViewElement.HorizontalScrollState;
      }
      set
      {
        this.ViewElement.HorizontalScrollState = value;
      }
    }

    [Description("Gets or sets the display state of the vertical scrollbar.")]
    [Category("Appearance")]
    [DefaultValue(ScrollState.AutoHide)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.ViewElement.VerticalScrollState;
      }
      set
      {
        this.ViewElement.VerticalScrollState = value;
      }
    }

    [Description("Gets or sets a value indicating whether the checkboxes should be in ThreeState mode.")]
    [Browsable(true)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    public bool ThreeStateMode
    {
      get
      {
        return this.threeStateMode;
      }
      set
      {
        if (this.threeStateMode == value)
          return;
        this.threeStateMode = value;
        this.SynchronizeVisualItems();
        this.OnNotifyPropertyChanged(nameof (ThreeStateMode));
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether grid lines shoud be shown in DetailsView.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    public bool ShowGridLines
    {
      get
      {
        return this.showGridLines;
      }
      set
      {
        if (this.showGridLines == value)
          return;
        this.showGridLines = value;
        this.SynchronizeVisualItems();
        this.OnNotifyPropertyChanged(nameof (ShowGridLines));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether items can be selected with mouse dragging.")]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    public bool EnableLassoSelection
    {
      get
      {
        return this.enableLassoSelection;
      }
      set
      {
        if (value)
          this.ViewElement.ScrollBehavior.Stop();
        if (this.enableLassoSelection == value)
          return;
        this.enableLassoSelection = value;
        this.OnNotifyPropertyChanged(nameof (EnableLassoSelection));
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether items should react on mouse hover.")]
    [DefaultValue(true)]
    public bool HotTracking
    {
      get
      {
        return this.hotTracking;
      }
      set
      {
        if (this.hotTracking == value)
          return;
        this.hotTracking = value;
        this.OnNotifyPropertyChanged(nameof (HotTracking));
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled. Always false when lasso selection is enabled.")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool EnableKineticScrolling
    {
      get
      {
        if (this.EnableLassoSelection)
          return false;
        return this.enableKineticScrolling;
      }
      set
      {
        if (this.enableKineticScrolling == value)
          return;
        this.enableKineticScrolling = value;
        if (!value)
          this.viewElement.ScrollBehavior.Stop();
        this.OnNotifyPropertyChanged(nameof (EnableKineticScrolling));
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the items should be sorted when clicking on header cells.")]
    public bool EnableColumnSort
    {
      get
      {
        return this.enableColumnSort;
      }
      set
      {
        if (this.enableColumnSort == value)
          return;
        this.enableColumnSort = value;
        this.OnNotifyPropertyChanged(nameof (EnableColumnSort));
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the column headers should be drawn.")]
    [DefaultValue(true)]
    public bool ShowColumnHeaders
    {
      get
      {
        return this.showColumnHeaders;
      }
      set
      {
        if (this.showColumnHeaders == value)
          return;
        this.showColumnHeaders = value;
        this.Update(RadListViewElement.UpdateModes.InvalidateMeasure);
        this.OnNotifyPropertyChanged(nameof (ShowColumnHeaders));
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the items should be shown in groups.")]
    [Category("Appearance")]
    public bool ShowGroups
    {
      get
      {
        return this.showGroups;
      }
      set
      {
        if (this.showGroups == value)
          return;
        this.showGroups = value;
        this.Update(RadListViewElement.UpdateModes.RefreshLayout | RadListViewElement.UpdateModes.UpdateScroll);
        this.OnNotifyPropertyChanged(nameof (ShowGroups));
      }
    }

    [Description("Gets or sets value indicating whether checkboxes should be shown.")]
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool ShowCheckBoxes
    {
      get
      {
        return this.showCheckBoxes;
      }
      set
      {
        if (this.showCheckBoxes == value)
          return;
        this.showCheckBoxes = value;
        if (!this.FullRowSelect && this.AllowArbitraryItemWidth)
          this.IsItemsMeasureValid = false;
        this.ViewElement.ViewElement.Children.Clear();
        this.OnNotifyPropertyChanged(nameof (ShowCheckBoxes));
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets value indicating if the user can reorder columns via drag and drop.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool AllowColumnReorder
    {
      get
      {
        return this.allowColumnReorder;
      }
      set
      {
        if (this.allowColumnReorder == value)
          return;
        this.allowColumnReorder = value;
        this.OnNotifyPropertyChanged(nameof (AllowColumnReorder));
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets value indicating if the user can reorder items via drag and drop. Always false when using data source, grouping, filtering, sorting, kinetic scrolling or lasso selection")]
    public bool AllowDragDrop
    {
      get
      {
        return this.allowDragDrop;
      }
      set
      {
        if (this.allowDragDrop == value)
          return;
        this.allowDragDrop = value;
        this.OnNotifyPropertyChanged(nameof (AllowDragDrop));
      }
    }

    [Description("Gets or sets value indicating if the user can resize the columns.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool AllowColumnResize
    {
      get
      {
        return this.resizingBehavior.AllowColumnResize;
      }
      set
      {
        if (this.resizingBehavior.AllowColumnResize == value)
          return;
        this.resizingBehavior.AllowColumnResize = value;
        this.OnNotifyPropertyChanged(nameof (AllowColumnResize));
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the current column in Details View.")]
    public ListViewDetailColumn CurrentColumn
    {
      get
      {
        return this.currentColumn;
      }
      set
      {
        if (this.currentColumn == value)
          return;
        if (this.currentColumn != null)
          this.currentColumn.SetCurrent(false);
        this.EndEdit();
        this.currentColumn = value;
        this.EnsureColumnVisible(this.currentColumn);
        if (this.currentColumn != null)
          this.currentColumn.SetCurrent(true);
        this.OnNotifyPropertyChanged(nameof (CurrentColumn));
      }
    }

    [Description("Indicates whether there is an active editor.")]
    [Browsable(false)]
    public bool IsEditing
    {
      get
      {
        return this.ActiveEditor != null;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the current item.")]
    public ListViewDataItem CurrentItem
    {
      get
      {
        return this.currentItem;
      }
      set
      {
        if (this.currentItem == value || this.OnCurrentItemChanging(new ListViewItemChangingEventArgs(this.currentItem, value)))
          return;
        if (this.currentItem != null)
          this.currentItem.Current = false;
        this.currentItem = value;
        if (this.currentItem != null)
        {
          if (this.Items.Contains(value))
          {
            this.listSource.CollectionView.CurrentChanged -= new EventHandler(this.CollectionView_CurrentChanged);
            this.listSource.CollectionView.MoveCurrentTo(this.currentItem);
            this.listSource.CollectionView.CurrentChanged += new EventHandler(this.CollectionView_CurrentChanged);
          }
          this.currentItem.Current = true;
          this.EnsureItemVisible(this.currentItem);
        }
        this.OnCurrecntItemChanged(new ListViewItemEventArgs(this.currentItem));
      }
    }

    public void ResetCurrentItem()
    {
      if (this.currentItem != null && this.Items.Contains(this.currentItem))
        this.currentItem.Current = false;
      this.currentItem = (ListViewDataItem) null;
    }

    [Description("Gets or sets the index of the selected item.")]
    [Browsable(false)]
    public int SelectedIndex
    {
      get
      {
        if (this.selectedItem == null)
          return -1;
        return this.items.IndexOf(this.selectedItem);
      }
      set
      {
        if (this.items.Count > value && value >= 0)
          this.SelectedItem = this.items[value];
        else
          this.SelectedItem = (ListViewDataItem) null;
      }
    }

    [Description("Gets or sets the selected item.")]
    [Browsable(false)]
    public ListViewDataItem SelectedItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        if (this.selectedItem == value)
          return;
        this.setThroughCode = true;
        this.ViewElement.ProcessSelection(value, Keys.None, false);
      }
    }

    [Description("Gets a collection containing the selected items.")]
    [Browsable(false)]
    public ListViewSelectedItemCollection SelectedItems
    {
      get
      {
        return this.selectedItems;
      }
    }

    [Browsable(false)]
    [Description("Gets a collection containing the checked items.")]
    public ListViewCheckedItemCollection CheckedItems
    {
      get
      {
        return this.checkedItems;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets value indicating whether multi selection is enabled.")]
    public bool MultiSelect
    {
      get
      {
        return this.multiSelect;
      }
      set
      {
        if (this.multiSelect == value)
          return;
        this.multiSelect = value;
        this.OnNotifyPropertyChanged(nameof (MultiSelect));
      }
    }

    [Description("Gets or sets value indicating whether editing is enabled.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool AllowEdit
    {
      get
      {
        return this.allowEdit;
      }
      set
      {
        if (this.allowEdit == value)
          return;
        this.allowEdit = value;
        this.OnNotifyPropertyChanged(nameof (AllowEdit));
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets value indicating whether the user can remove items with the Delete key.")]
    [Category("Behavior")]
    public bool AllowRemove
    {
      get
      {
        return this.allowRemove;
      }
      set
      {
        if (this.allowRemove == value)
          return;
        this.allowRemove = value;
        this.OnNotifyPropertyChanged(nameof (AllowRemove));
      }
    }

    [Browsable(false)]
    [Description("Gets the currently active editor.")]
    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the items can have different height.")]
    public bool AllowArbitraryItemHeight
    {
      get
      {
        return this.ViewElement.AllowArbitraryItemHeight;
      }
      set
      {
        this.ViewElement.AllowArbitraryItemHeight = value;
      }
    }

    [Description("Gets or sets a value indicating whether the items can have different width.")]
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool AllowArbitraryItemWidth
    {
      get
      {
        return this.ViewElement.AllowArbitraryItemWidth;
      }
      set
      {
        this.ViewElement.AllowArbitraryItemWidth = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the full row should be selected.")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool FullRowSelect
    {
      get
      {
        return this.ViewElement.FullRowSelect;
      }
      set
      {
        this.ViewElement.FullRowSelect = value;
      }
    }

    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(typeof (Size), "0,0")]
    [Description("Gets or sets the default item size.")]
    public Size ItemSize
    {
      get
      {
        return this.ViewElement.ItemSize;
      }
      set
      {
        this.ViewElement.ItemSize = value;
      }
    }

    [Category("Layout")]
    [DefaultValue(typeof (Size), "0,0")]
    [Browsable(true)]
    [Description("Gets or sets the default group item size.")]
    public Size GroupItemSize
    {
      get
      {
        return this.ViewElement.GroupItemSize;
      }
      set
      {
        this.ViewElement.GroupItemSize = value;
      }
    }

    [DefaultValue(25)]
    [Category("Layout")]
    [Browsable(true)]
    [Description("Gets or sets the indent of the items when they are displayed in a group.")]
    public int GroupIndent
    {
      get
      {
        return this.ViewElement.GroupIndent;
      }
      set
      {
        this.ViewElement.GroupIndent = value;
      }
    }

    [Description("Gets or sets the fill color of the lasso selection rectangle.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SelectionRectangleColor
    {
      get
      {
        return this.ViewElement.SelectionRectangleColor;
      }
      set
      {
        this.ViewElement.SelectionRectangleColor = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the fill color of the lasso selection rectangle.")]
    public Color SelectionRectangleBorderColor
    {
      get
      {
        return this.ViewElement.SelectionRectangleBorderColor;
      }
      set
      {
        this.ViewElement.SelectionRectangleBorderColor = value;
      }
    }

    [DefaultValue(0)]
    [Description("Gets or sets the space between the items.")]
    [Category("Layout")]
    [Browsable(true)]
    public int ItemSpacing
    {
      get
      {
        return this.ViewElement.ItemSpacing;
      }
      set
      {
        this.ViewElement.ItemSpacing = value;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets a collection of ListViewDetailColumn object which represent the columns in DetailsView.")]
    [Editor("Telerik.WinControls.UI.Design.ListViewColumnCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public ListViewColumnCollection Columns
    {
      get
      {
        return this.columns;
      }
    }

    [Description("Gets a value indicating whether the control is in bound mode.")]
    [Browsable(false)]
    public bool IsDataBound
    {
      get
      {
        return this.DataSource != null;
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection containing the groups of the RadListViewElement.")]
    [Editor("Telerik.WinControls.UI.Design.ListViewGroupCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public ListViewDataItemGroupCollection Groups
    {
      get
      {
        return this.groups;
      }
    }

    [Description("Gets or sets the value member.")]
    [Browsable(true)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    [Category("Data")]
    public string ValueMember
    {
      get
      {
        return this.valueMember;
      }
      set
      {
        if (!(this.valueMember != value))
          return;
        this.valueMember = value;
        this.SynchronizeVisualItems();
        this.ViewElement.UpdateLayout();
        this.OnNotifyPropertyChanged(nameof (ValueMember));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(true)]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [DefaultValue("")]
    [Category("Data")]
    [Description("Gets or sets the display member.")]
    public string DisplayMember
    {
      get
      {
        return this.displayMember;
      }
      set
      {
        if (!(this.displayMember != value))
          return;
        this.displayMember = value;
        this.IsItemsMeasureValid = false;
        this.SynchronizeVisualItems();
        this.ViewElement.UpdateLayout();
        this.OnNotifyPropertyChanged(nameof (DisplayMember));
      }
    }

    [Browsable(true)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    [Category("Data")]
    [Description("Gets or sets the checked member.")]
    public string CheckedMember
    {
      get
      {
        return this.checkedMember;
      }
      set
      {
        if (!(this.checkedMember != value))
          return;
        this.checkedMember = value;
        this.UpdateCheckedItems();
        this.SynchronizeVisualItems();
        this.ViewElement.UpdateLayout();
        this.OnNotifyPropertyChanged(nameof (CheckedMember));
      }
    }

    [Browsable(false)]
    [Description("Gets the DataView collection")]
    public RadCollectionView<ListViewDataItem> DataView
    {
      get
      {
        return this.listSource.CollectionView;
      }
    }

    [Category("Data")]
    [Description("Gets or sets a value indicating whether sorting is enabled.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool EnableSorting
    {
      get
      {
        if (!this.IsDesignMode)
          return this.listSource.CollectionView.CanSort;
        return this.enableSorting;
      }
      set
      {
        if (!this.IsDesignMode && this.listSource.CollectionView.CanSort != value)
        {
          this.listSource.CollectionView.CanSort = value;
          this.OnNotifyPropertyChanged(nameof (EnableSorting));
        }
        else
          this.enableSorting = value;
      }
    }

    [Description("Gets or sets a value indicating whether filtering is enabled.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    [Category("Data")]
    public bool EnableFiltering
    {
      get
      {
        if (!this.IsDesignMode)
          return this.listSource.CollectionView.CanFilter;
        return this.enableFiltering;
      }
      set
      {
        if (!this.IsDesignMode && this.listSource.CollectionView.CanFilter != value)
        {
          this.listSource.CollectionView.CanFilter = value;
          this.OnNotifyPropertyChanged(nameof (EnableFiltering));
        }
        else
          this.enableFiltering = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether filtering is enabled.")]
    [Category("Data")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool EnableGrouping
    {
      get
      {
        if (!this.IsDesignMode)
          return this.listSource.CollectionView.CanGroup;
        return this.enableGrouping;
      }
      set
      {
        if (!this.IsDesignMode && this.listSource.CollectionView.CanGroup != value)
        {
          this.listSource.CollectionView.CanGroup = value;
          this.Update(RadListViewElement.UpdateModes.RefreshLayout | RadListViewElement.UpdateModes.UpdateScroll);
          this.OnNotifyPropertyChanged(nameof (EnableGrouping));
        }
        else
          this.enableGrouping = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether custom grouping is enabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    [Category("Data")]
    public bool EnableCustomGrouping
    {
      get
      {
        return this.enableCustomGrouping;
      }
      set
      {
        if (value == this.enableCustomGrouping)
          return;
        this.enableCustomGrouping = value;
        this.Update(RadListViewElement.UpdateModes.RefreshLayout | RadListViewElement.UpdateModes.UpdateScroll);
        this.OnNotifyPropertyChanged(nameof (EnableCustomGrouping));
      }
    }

    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection of filter descriptors by which you can apply filter rules to the items.")]
    [Browsable(true)]
    public ListViewFilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.filterDescriptors;
      }
    }

    [Description("Gets a collection of SortDescriptor which are used to define sorting rules over the ListViewDataItemCollection.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.listSource.CollectionView.SortDescriptors;
      }
    }

    [Description("Gets a collection of GroupDescriptor which are used to define grouping rules over the ListViewDataItemCollection.")]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.listSource.CollectionView.GroupDescriptors;
      }
    }

    [Description("Gets the source of the items.")]
    [Browsable(false)]
    public RadListSource<ListViewDataItem> ListSource
    {
      get
      {
        return (RadListSource<ListViewDataItem>) this.listSource;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Browsable(true)]
    [Editor("Telerik.WinControls.UI.Design.ListViewItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets or sets a collection of ListViewDataItem object which represent the items in RadListViewElement.")]
    public ListViewDataItemCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Description("Gets the element that represents the active view.")]
    [Browsable(false)]
    public BaseListViewElement ViewElement
    {
      get
      {
        return this.viewElement;
      }
    }

    [Description("Gets or sets the type of the view.")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(ListViewType.ListView)]
    public virtual ListViewType ViewType
    {
      get
      {
        return this.viewType;
      }
      set
      {
        if (this.viewType == value || this.OnViewTypeChanging(new ViewTypeChangingEventArgs(this.viewType, value)))
          return;
        this.viewElement.ScrollBehavior.Stop();
        this.Children.Remove((RadElement) this.viewElement);
        this.viewElement.Dispose();
        this.viewType = value;
        this.viewElement = this.CreateViewElement();
        this.Children.Add((RadElement) this.viewElement);
        this.InvalidateMeasure(true);
        this.Update(RadListViewElement.UpdateModes.RefreshAll);
        this.OnViewTypeChanged();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the data source of a RadListViewElementElement.")]
    [AttributeProvider(typeof (IListSource))]
    [DefaultValue(null)]
    [Browsable(true)]
    [Category("Data")]
    public object DataSource
    {
      get
      {
        return this.listSource.DataSource;
      }
      set
      {
        if (value == this.DataSource)
          return;
        if (value == null)
        {
          this.DisplayMember = "";
          this.ValueMember = "";
          this.CheckedMember = "";
        }
        this.listSource.DataSource = value;
      }
    }

    public string DataMember
    {
      get
      {
        return this.ListSource.DataMember;
      }
      set
      {
        if (!(this.ListSource.DataMember != value))
          return;
        this.ListSource.DataMember = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the height of the header in Details View.")]
    [Category("Layout")]
    [DefaultValue(35f)]
    public float HeaderHeight
    {
      get
      {
        return this.headerHeight * this.DpiScaleFactor.Height;
      }
      set
      {
        if ((double) this.headerHeight == (double) value)
          return;
        this.headerHeight = value;
        this.Update(RadListViewElement.UpdateModes.RefreshLayout | RadListViewElement.UpdateModes.UpdateScroll);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets the ColumnResizingBehavior that is responsible for resizing the columns.")]
    public ColumnResizingBehavior ColumnResizingBehavior
    {
      get
      {
        return this.resizingBehavior;
      }
      set
      {
        this.resizingBehavior = value;
      }
    }

    public bool KeyboardSearchEnabled { get; set; }

    public int KeyboardSearchResetInterval
    {
      get
      {
        return this.viewElement.typingTimer.Interval;
      }
      set
      {
        this.viewElement.typingTimer.Interval = value;
      }
    }

    internal bool IsItemsMeasureValid
    {
      get
      {
        return this.itemsMeasureValid;
      }
      set
      {
        this.itemsMeasureValid = value;
      }
    }

    public CheckOnClickMode CheckOnClickMode
    {
      get
      {
        return this.checkOnClickMode;
      }
      set
      {
        this.checkOnClickMode = value;
      }
    }

    internal void SetSelectedItem(ListViewDataItem item)
    {
      if (this.selectedItem == item || item is ListViewDataItemGroup)
        return;
      this.EndEdit();
      this.selectedItem = item;
      this.OnNotifyPropertyChanged("SelectedIndex");
      this.OnNotifyPropertyChanged("SelectedItem");
      this.CallSelectedIndexChanged();
      this.OnSelectedItemChanged(item);
    }

    private void CallSelectedIndexChanged()
    {
      int selectedIndex = this.SelectedIndex;
      if (this.setThroughCode)
      {
        if (this.oldSelectedIndex != selectedIndex)
        {
          this.OnSelectedIndexChanged();
          this.oldSelectedIndex = selectedIndex;
        }
        this.setThroughCode = false;
      }
      else
      {
        if (this.oldSelectedIndex == selectedIndex || selectedIndex == -1)
          return;
        this.OnSelectedIndexChanged();
        this.oldSelectedIndex = selectedIndex;
      }
    }

    public virtual bool BeginEdit()
    {
      if (!this.allowEdit || this.activeEditor != null || (this.SelectedItems.Count > 1 || this.SelectedItem == null) || this.viewType == ListViewType.DetailsView && this.currentColumn == null)
        return false;
      this.EnsureItemVisible(this.selectedItem, true);
      if (this.viewType == ListViewType.DetailsView)
        this.EnsureColumnVisible(this.currentColumn);
      BaseListViewVisualItem element = this.viewElement.GetElement(this.selectedItem);
      if (element == null)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      ListViewItemEditorRequiredEventArgs e1 = new ListViewItemEditorRequiredEventArgs(this.SelectedItem, typeof (ListViewTextBoxEditor));
      this.OnEditorRequired(e1);
      IInputEditor editor = e1.Editor as IInputEditor ?? this.GetEditor(e1.EditorType);
      if (editor == null)
        return false;
      this.activeEditor = editor;
      ListViewItemEditingEventArgs e2 = new ListViewItemEditingEventArgs(this.SelectedItem, (IValueEditor) editor);
      this.OnEditing(e2);
      if (e2.Cancel)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      this.isEditorInitializing = true;
      element.AddEditor(editor);
      ISupportInitialize activeEditor = this.activeEditor as ISupportInitialize;
      activeEditor?.BeginInit();
      this.InitializeEditor(element, activeEditor, editor);
      this.isEditorInitializing = false;
      return true;
    }

    protected virtual void InitializeEditor(
      BaseListViewVisualItem visualItem,
      ISupportInitialize initializable,
      IInputEditor editor)
    {
      if (this.ViewType == ListViewType.DetailsView)
        this.activeEditor.Initialize((object) visualItem, this.SelectedItem[this.currentColumn]);
      else
        this.activeEditor.Initialize((object) visualItem, this.SelectedItem.Value);
      initializable?.EndInit();
      this.OnEditorInitialized(new ListViewItemEditorInitializedEventArgs(visualItem, (IValueEditor) editor));
      RadControl radControl = this.ElementTree == null || this.ElementTree.Control == null ? (RadControl) null : this.ElementTree.Control as RadControl;
      if (radControl != null && TelerikHelper.IsMaterialTheme(radControl.ThemeName))
      {
        BaseInputEditor activeEditor = this.activeEditor as BaseInputEditor;
        if (activeEditor != null)
          activeEditor.EditorElement.StretchVertically = true;
      }
      this.activeEditor.BeginEdit();
      this.cachedOldValue = this.ViewType != ListViewType.DetailsView ? this.SelectedItem.Value : this.SelectedItem[this.currentColumn];
    }

    public bool EndEdit()
    {
      return this.EndEditCore(true);
    }

    public bool CancelEdit()
    {
      return this.EndEditCore(false);
    }

    protected virtual void OnEditorInitialized(ListViewItemEditorInitializedEventArgs e)
    {
      if (this.EditorInitialized == null)
        return;
      this.EditorInitialized((object) this, e);
    }

    protected virtual void OnEditing(ListViewItemEditingEventArgs e)
    {
      if (this.ItemEditing == null)
        return;
      this.ItemEditing((object) this, e);
    }

    protected virtual IInputEditor GetEditor(System.Type editorType)
    {
      IInputEditor inputEditor = (IInputEditor) null;
      if (!this.cachedEditors.TryGetValue(editorType, out inputEditor))
        inputEditor = Activator.CreateInstance(editorType) as IInputEditor;
      if (inputEditor != null && !this.cachedEditors.ContainsValue(inputEditor))
        this.cachedEditors.Add(editorType, inputEditor);
      return inputEditor;
    }

    protected virtual void OnEditorRequired(ListViewItemEditorRequiredEventArgs e)
    {
      if (this.EditorRequired == null)
        return;
      this.EditorRequired((object) this, e);
    }

    protected virtual bool EndEditCore(bool commitChanges)
    {
      if (!this.IsEditing || this.isEndingEdit || this.isEditorInitializing)
        return false;
      this.isEndingEdit = true;
      BaseListViewVisualItem element = this.viewElement.GetElement(this.SelectedItem);
      if (element == null)
      {
        this.isEndingEdit = false;
        return false;
      }
      object newValue = this.ActiveEditor.Value;
      bool isModified = this.ActiveEditor.IsModified;
      this.activeEditor.EndEdit();
      element.RemoveEditor(this.activeEditor);
      this.activeEditor = (IInputEditor) null;
      if (commitChanges && isModified)
        this.SaveEditorValue(element, newValue);
      this.Update(RadListViewElement.UpdateModes.RefreshLayout);
      this.OnEdited(new ListViewItemEditedEventArgs(element, (IValueEditor) this.activeEditor, !commitChanges));
      this.ElementTree.Control.Focus();
      this.isEndingEdit = false;
      return true;
    }

    protected virtual void SaveEditorValue(BaseListViewVisualItem visualItem, object newValue)
    {
      if (object.Equals(newValue, (object) string.Empty))
        newValue = (object) null;
      ListViewItemValidatingEventArgs args = new ListViewItemValidatingEventArgs(visualItem, this.cachedOldValue, newValue);
      this.OnValueValidating(args);
      if (args.Cancel)
      {
        this.OnValidationError(EventArgs.Empty);
      }
      else
      {
        newValue = args.NewValue;
        this.SetSelectedItemValue(new ListViewItemValueChangingEventArgs(this.SelectedItem, newValue, this.cachedOldValue), newValue);
      }
    }

    protected virtual void SetSelectedItemValue(
      ListViewItemValueChangingEventArgs valueChangingArgs,
      object newValue)
    {
      if (this.ViewType == ListViewType.DetailsView)
      {
        if (this.OnValueChanging(valueChangingArgs))
          return;
        this.SelectedItem[this.CurrentColumn] = newValue;
        this.OnValueChanged(new ListViewItemValueChangedEventArgs(this.SelectedItem));
      }
      else
        this.SelectedItem.Value = newValue;
    }

    public event EventHandler BindingContextChanged;

    public event EventHandler BindingCompleted;

    public override BindingContext BindingContext
    {
      get
      {
        return this.bindingContext;
      }
      set
      {
        if (this.bindingContext == value)
          return;
        this.bindingContext = value;
        this.OnBindingContextChanged(EventArgs.Empty);
      }
    }

    public IDataItem NewItem()
    {
      ListViewDataItem listViewDataItem = new ListViewDataItem();
      listViewDataItem.Owner = this;
      ListViewItemCreatingEventArgs args = new ListViewItemCreatingEventArgs(listViewDataItem);
      this.OnDataItemCreating(args);
      if (args.Item != listViewDataItem)
        args.Item.Owner = this;
      return (IDataItem) args.Item;
    }

    public void Initialize()
    {
      this.ViewElement.ViewElement.SuspendLayout(true);
      this.ViewElement.ViewElement.Children.Clear();
      this.ViewElement.ViewElement.ElementProvider.ClearCache();
      this.SelectedItems.Reset();
      this.CheckedItems.Reset();
      this.selectedItem = (ListViewDataItem) null;
      this.currentColumn = (ListViewDetailColumn) null;
      this.currentItem = (ListViewDataItem) null;
      if (this.IsDataBound)
      {
        this.InitializeBoundColumns();
        this.RebuildColumnAccessors();
      }
      this.ViewElement.ViewElement.ResumeLayout(true, true);
    }

    public void BindingComplete()
    {
      if (this.BindingCompleted == null)
        return;
      this.BindingCompleted((object) this, EventArgs.Empty);
    }

    protected virtual BindingContext CreateBindingContext()
    {
      return new BindingContext();
    }

    protected virtual void OnBindingContextChanged(EventArgs e)
    {
      EventHandler bindingContextChanged = this.BindingContextChanged;
      if (bindingContextChanged == null)
        return;
      bindingContextChanged((object) this, e);
    }

    void IDataItemSource.MetadataChanged(PropertyDescriptor pd)
    {
    }

    protected virtual BaseListViewElement CreateViewElement()
    {
      BaseListViewElement baseListViewElement;
      switch (this.viewType)
      {
        case ListViewType.ListView:
          baseListViewElement = (BaseListViewElement) new SimpleListViewElement(this);
          break;
        case ListViewType.IconsView:
          baseListViewElement = (BaseListViewElement) new IconListViewElement(this);
          break;
        case ListViewType.DetailsView:
          baseListViewElement = (BaseListViewElement) new DetailListViewElement(this);
          break;
        default:
          baseListViewElement = (BaseListViewElement) new SimpleListViewElement(this);
          break;
      }
      return baseListViewElement;
    }

    protected virtual void WireEvents()
    {
      this.groups.CollectionChanged += new NotifyCollectionChangedEventHandler(this.groups_CollectionChanged);
      this.columns.CollectionChanged += new NotifyCollectionChangedEventHandler(this.columns_CollectionChanged);
      this.filterDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.filterDescriptors_CollectionChanged);
      this.ListSource.CollectionView.SortDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SortDescriptors_CollectionChanged);
      this.listSource.CollectionView.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CollectionView_CollectionChanged);
      this.listSource.CollectionView.CurrentChanged += new EventHandler(this.CollectionView_CurrentChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.groups.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.groups_CollectionChanged);
      this.columns.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.columns_CollectionChanged);
      this.filterDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.filterDescriptors_CollectionChanged);
      this.ListSource.CollectionView.SortDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.SortDescriptors_CollectionChanged);
      this.listSource.CollectionView.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.CollectionView_CollectionChanged);
      this.listSource.CollectionView.CurrentChanged -= new EventHandler(this.CollectionView_CurrentChanged);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.bindingContext = this.CreateBindingContext();
      this.listSource = new ListViewListSource(this);
      this.listSource.CollectionView.CanGroup = false;
      this.listSource.CollectionView.CanSort = false;
      this.listSource.CollectionView.CanFilter = false;
      this.listSource.CollectionView.GroupFactory = (IGroupFactory<ListViewDataItem>) new ListViewGroupFactory(this);
      this.filterDescriptors = new ListViewFilterDescriptorCollection();
      this.items = new ListViewDataItemCollection(this);
      this.groups = new ListViewDataItemGroupCollection(this);
      this.columns = new ListViewColumnCollection(this);
      this.selectedItems = new ListViewSelectedItemCollection(this);
      this.checkedItems = new ListViewCheckedItemCollection(this);
      this.WireEvents();
      this.viewElement = this.CreateViewElement();
      this.Children.Add((RadElement) this.viewElement);
      this.resizingBehavior = new ColumnResizingBehavior(this);
      this.resizingBehavior.AllowColumnResize = true;
      this.dragDropService = new ListViewDragDropService(this);
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      foreach (KeyValuePair<System.Type, IInputEditor> cachedEditor in this.cachedEditors)
      {
        IInputEditor inputEditor = cachedEditor.Value;
        (inputEditor as BaseInputEditor)?.EditorElement?.Dispose();
        (inputEditor as IDisposable)?.Dispose();
      }
      this.cachedEditors.Clear();
      this.listSource.Dispose();
      base.DisposeManagedResources();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (float.IsInfinity(availableSize.Width))
        availableSize.Width = sizeF.Width;
      if (float.IsInfinity(availableSize.Height))
        availableSize.Height = sizeF.Height;
      return availableSize;
    }

    public void BeginUpdate()
    {
      this.items.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.items.EndUpdate();
    }

    public virtual ListViewDataItem FindItemByKey(object key)
    {
      return this.FindItemByKey(key, false);
    }

    public virtual ListViewDataItem FindItemByKey(
      object key,
      bool searchVisibleItems)
    {
      if (!searchVisibleItems)
      {
        foreach (ListViewDataItem listViewDataItem in this.Items)
        {
          if (object.Equals(listViewDataItem.Key, key))
            return listViewDataItem;
        }
      }
      else
      {
        ITraverser<ListViewDataItem> enumerator = this.ViewElement.Scroller.Traverser.GetEnumerator() as ITraverser<ListViewDataItem>;
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current != null && object.Equals(enumerator.Current.Key, key))
            return enumerator.Current;
        }
      }
      return (ListViewDataItem) null;
    }

    public virtual void Update(RadListViewElement.UpdateModes updateMode)
    {
      if ((updateMode & RadListViewElement.UpdateModes.UpdateScroll) == RadListViewElement.UpdateModes.UpdateScroll)
        this.ViewElement.Scroller.UpdateScrollRange();
      if ((updateMode & RadListViewElement.UpdateModes.InvalidateItems) == RadListViewElement.UpdateModes.InvalidateItems)
        this.IsItemsMeasureValid = false;
      if ((updateMode & RadListViewElement.UpdateModes.InvalidateMeasure) == RadListViewElement.UpdateModes.InvalidateMeasure)
        this.ViewElement.InvalidateMeasure(true);
      if ((updateMode & RadListViewElement.UpdateModes.UpdateLayout) == RadListViewElement.UpdateModes.UpdateLayout)
        this.UpdateLayout();
      if ((updateMode & RadListViewElement.UpdateModes.Invalidate) != RadListViewElement.UpdateModes.Invalidate)
        return;
      this.ViewElement.ViewElement.Invalidate();
    }

    public virtual void SynchronizeVisualItems()
    {
      this.isSynchronizing = true;
      for (int index = 0; index < this.ViewElement.ViewElement.Children.Count; ++index)
        ((BaseListViewVisualItem) this.ViewElement.ViewElement.Children[index]).Synchronize();
      this.isSynchronizing = false;
      this.Invalidate();
    }

    public virtual void EnsureItemVisible(ListViewDataItem item)
    {
      this.ViewElement.EnsureItemVisible(item);
    }

    public virtual void EnsureItemVisible(ListViewDataItem item, bool ensureHorizontally)
    {
      this.ViewElement.EnsureItemVisible(item, ensureHorizontally);
    }

    public virtual void EnsureColumnVisible(ListViewDetailColumn listViewDetailColumn)
    {
      (this.ViewElement as DetailListViewElement)?.EnsureColumnVisible(listViewDetailColumn);
    }

    public void Select(ListViewDataItem[] items)
    {
      if (!this.MultiSelect)
      {
        if (items.GetLength(0) <= 0)
          return;
        this.viewElement.ProcessSelection(items[items.GetLength(0) - 1], Keys.None, true);
      }
      else
      {
        foreach (ListViewDataItem listViewDataItem in items)
        {
          if (listViewDataItem.Owner == this)
            listViewDataItem.Selected = true;
        }
        if (items.GetLength(0) <= 0)
          return;
        this.SetSelectedItem(items[items.GetLength(0) - 1]);
      }
    }

    public void ExpandAll()
    {
      foreach (ListViewDataItemGroup group in this.Groups)
        group.Expanded = true;
    }

    public void CollapseAll()
    {
      foreach (ListViewDataItemGroup group in this.Groups)
        group.Expanded = false;
    }

    public void CheckSelectedItems()
    {
      this.SetItemsCheckState((IEnumerable<ListViewDataItem>) this.SelectedItems, Telerik.WinControls.Enumerations.ToggleState.On);
    }

    public void UncheckSelectedItems()
    {
      this.SetItemsCheckState((IEnumerable<ListViewDataItem>) this.SelectedItems, Telerik.WinControls.Enumerations.ToggleState.Off);
    }

    public void CheckAllItems()
    {
      this.SetItemsCheckState((IEnumerable<ListViewDataItem>) this.Items, Telerik.WinControls.Enumerations.ToggleState.On);
    }

    public void UncheckAllItems()
    {
      this.SetItemsCheckState((IEnumerable<ListViewDataItem>) this.Items, Telerik.WinControls.Enumerations.ToggleState.Off);
    }

    public void UpdateCheckedItems()
    {
      this.CheckedItems.Reset();
      foreach (ListViewDataItem listViewItem in this.Items)
        this.CheckedItems.ProcessCheckedItem(listViewItem);
    }

    private void CollectionView_CurrentChanged(object sender, EventArgs e)
    {
      if (this.IsDesignMode || !this.SelectLastAddedItem)
        return;
      this.ViewElement.ProcessSelection(this.listSource.CollectionView.CurrentItem, Keys.None, true);
    }

    private void SortDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      int num = this.viewElement.Scroller.Scrollbar.Value;
      if (this.ShowGroups && this.groups.Count > 0 && (this.EnableGrouping || this.EnableCustomGrouping))
      {
        foreach (ListViewDataItemGroup group in this.Groups)
        {
          RadListSource<ListViewDataItem> innerList = group.Items.InnerList as RadListSource<ListViewDataItem>;
          if (innerList != null)
          {
            innerList.BeginUpdate();
            innerList.CollectionView.SortDescriptors.Clear();
            foreach (SortDescriptor sortDescriptor in (Collection<SortDescriptor>) this.SortDescriptors)
              innerList.CollectionView.SortDescriptors.Add(sortDescriptor);
            innerList.EndUpdate();
          }
        }
        this.Update(RadListViewElement.UpdateModes.RefreshLayout);
        this.SynchronizeVisualItems();
      }
      this.viewElement.Scroller.Scrollbar.Value = 0;
      this.viewElement.Scroller.Traverser.Reset();
      this.viewElement.Scroller.Scrollbar.Value = num;
      DetailListViewElement viewElement = this.viewElement as DetailListViewElement;
      if (viewElement == null)
        return;
      foreach (DetailListViewCellElement child in viewElement.ColumnContainer.Children)
        child.Synchronize();
    }

    private void filterDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.listSource.CollectionView.FilterExpression = this.filterDescriptors.Expression;
      if (!this.ShowGroups || this.groups.Count <= 0 || !this.EnableGrouping && !this.EnableCustomGrouping)
        return;
      foreach (ListViewDataItemGroup group in this.Groups)
      {
        RadListSource<ListViewDataItem> innerList = group.Items.InnerList as RadListSource<ListViewDataItem>;
        if (innerList != null)
          innerList.CollectionView.FilterExpression = this.filterDescriptors.Expression;
      }
      this.Update(RadListViewElement.UpdateModes.RefreshLayout | RadListViewElement.UpdateModes.UpdateScroll);
    }

    private void groups_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.SynchronizeVisualItems();
      this.Update(RadListViewElement.UpdateModes.RefreshLayout | RadListViewElement.UpdateModes.UpdateScroll);
    }

    private void columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.RebuildColumnAccessors();
      this.Update(RadListViewElement.UpdateModes.RefreshLayout);
    }

    private void CollectionView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Batch || e.Action == NotifyCollectionChangedAction.Reset)
        this.UpdateCheckedItems();
      else if (e.NewItems != null)
      {
        foreach (ListViewDataItem newItem in (IEnumerable) e.NewItems)
          this.CheckedItems.ProcessCheckedItem(newItem);
      }
      if (e.Action == NotifyCollectionChangedAction.ItemChanged || e.Action == NotifyCollectionChangedAction.ItemChanging)
      {
        foreach (ListViewDataItem newItem in (IEnumerable) e.NewItems)
        {
          BaseListViewVisualItem element = this.ViewElement.GetElement(newItem);
          newItem.IsMeasureValid = false;
          element?.Synchronize();
        }
        this.Update(RadListViewElement.UpdateModes.Invalidate | RadListViewElement.UpdateModes.UpdateScroll);
      }
      else
      {
        if (e.Action == NotifyCollectionChangedAction.Reset)
          this.ViewElement.ViewElement.DisposeChildren();
        this.SynchronizeVisualItems();
        this.Update(RadListViewElement.UpdateModes.UpdateLayout | RadListViewElement.UpdateModes.UpdateScroll);
        this.CallSelectedIndexChanged();
      }
    }

    protected override void OnStyleChanged(RadPropertyChangedEventArgs e)
    {
      base.OnStyleChanged(e);
      if (e.NewValue == null)
        return;
      this.ViewElement.ViewElement.DisposeChildren();
      this.ViewElement.ViewElement.ElementProvider.ClearCache();
      this.ViewElement.ViewElement.InvalidateMeasure();
    }

    private void InitializeBoundColumns()
    {
      if (this.IsDesignMode)
        return;
      this.columns.BeginUpdate();
      this.columns.Clear();
      PropertyDescriptorCollection boundProperties = this.ListSource.BoundProperties;
      for (int index = 0; index < boundProperties.Count; ++index)
      {
        if ((object) boundProperties[index].PropertyType != (object) typeof (IBindingList))
        {
          string headerText = this.GetCaption(boundProperties[index]) ?? boundProperties[index].DisplayName;
          ListViewColumnCreatingEventArgs args = new ListViewColumnCreatingEventArgs(new ListViewDetailColumn(boundProperties[index].Name, headerText) { FieldName = boundProperties[index].Name, Owner = this });
          this.OnColumnCreating(args);
          this.columns.Add(args.Column);
        }
      }
      this.columns.EndUpdate(false);
    }

    private string GetCaption(PropertyDescriptor descriptor)
    {
      PropertyInfo property = descriptor.GetType().GetProperty("Column", BindingFlags.Instance | BindingFlags.NonPublic);
      if ((object) property == null)
        return (string) null;
      return (property.GetValue((object) descriptor, (object[]) null) as DataColumn)?.Caption;
    }

    private void RebuildColumnAccessors()
    {
      for (int index = 0; index < this.columns.Count; ++index)
      {
        ListViewDetailColumn column = this.columns[index];
        column.Accessor = this.IsDataBound ? (ListViewAccessor) new ListViewBoundAccessor(column) : new ListViewAccessor(column);
      }
    }

    private void SetItemsCheckState(IEnumerable<ListViewDataItem> items, Telerik.WinControls.Enumerations.ToggleState newItemState)
    {
      if (items == null)
        return;
      foreach (ListViewDataItem listViewDataItem in items)
        listViewDataItem.CheckState = newItemState;
    }

    public virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      if (this.ElementState == ElementState.Loaded)
        return this.viewElement.ProcessMouseUp(e);
      return false;
    }

    public virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      if (this.ElementState == ElementState.Loaded)
        return this.viewElement.ProcessMouseMove(e);
      return false;
    }

    public virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      if (this.ElementState == ElementState.Loaded)
        return this.viewElement.ProcessMouseDown(e);
      return false;
    }

    public virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      if (this.ElementState == ElementState.Loaded)
        return this.viewElement.ProcessKeyDown(e);
      return false;
    }

    public virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      if (this.ElementState == ElementState.Loaded)
        return this.viewElement.ProcessKeyPress(e);
      return false;
    }

    public virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      if (this.ElementState == ElementState.Loaded)
        return this.viewElement.ProcessMouseWheel(e);
      return false;
    }

    public void ScrollTo(int delta)
    {
      this.ViewElement.ScrollTo(delta);
    }

    public enum UpdateModes
    {
      InvalidateItems = 1,
      InvalidateMeasure = 2,
      UpdateLayout = 4,
      Invalidate = 8,
      RefreshLayout = 14, // 0x0000000E
      UpdateScroll = 16, // 0x00000010
      RefreshAll = 31, // 0x0000001F
    }
  }
}
