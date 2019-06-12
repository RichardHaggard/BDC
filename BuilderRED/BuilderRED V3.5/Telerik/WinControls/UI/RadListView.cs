// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Data Controls")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [Description("Displays a flat collection of labeled items, each represented by a ListViewDataItem.")]
  [DefaultProperty("Items")]
  [DefaultEvent("SelectedItemChanged")]
  [ComplexBindingProperties("DataSource", "DataMember")]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "CurrentItem")]
  [Designer("Telerik.WinControls.UI.Design.RadListViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadListView : RadControl
  {
    private Dictionary<string, object> initValues = new Dictionary<string, object>();
    private RadListViewElement listViewElement;

    public event EventHandler<ListViewGroupEventArgs> GroupExpanded
    {
      add
      {
        this.listViewElement.GroupExpanded += value;
      }
      remove
      {
        this.listViewElement.GroupExpanded -= value;
      }
    }

    public event EventHandler<ListViewGroupCancelEventArgs> GroupExpanding
    {
      add
      {
        this.listViewElement.GroupExpanding += value;
      }
      remove
      {
        this.listViewElement.GroupExpanding -= value;
      }
    }

    public new event EventHandler BindingContextChanged
    {
      add
      {
        this.listViewElement.BindingContextChanged += value;
      }
      remove
      {
        this.listViewElement.BindingContextChanged -= value;
      }
    }

    public event EventHandler BindingCompleted
    {
      add
      {
        this.listViewElement.BindingCompleted += value;
      }
      remove
      {
        this.listViewElement.BindingCompleted -= value;
      }
    }

    public event ListViewItemCancelEventHandler SelectedItemChanging
    {
      add
      {
        this.listViewElement.SelectedItemChanging += value;
      }
      remove
      {
        this.listViewElement.SelectedItemChanging -= value;
      }
    }

    public event EventHandler SelectedItemsChanged
    {
      add
      {
        this.listViewElement.SelectedItemsChanged += value;
      }
      remove
      {
        this.listViewElement.SelectedItemsChanged -= value;
      }
    }

    public event EventHandler SelectedItemChanged
    {
      add
      {
        this.listViewElement.SelectedItemChanged += value;
      }
      remove
      {
        this.listViewElement.SelectedItemChanged -= value;
      }
    }

    public event EventHandler SelectedIndexChanged
    {
      add
      {
        this.listViewElement.SelectedIndexChanged += value;
      }
      remove
      {
        this.listViewElement.SelectedIndexChanged -= value;
      }
    }

    public virtual event EventHandler ViewTypeChanged
    {
      add
      {
        this.ListViewElement.ViewTypeChanged += value;
      }
      remove
      {
        this.ListViewElement.ViewTypeChanged -= value;
      }
    }

    public virtual event ViewTypeChangingEventHandler ViewTypeChanging
    {
      add
      {
        this.ListViewElement.ViewTypeChanging += value;
      }
      remove
      {
        this.ListViewElement.ViewTypeChanging -= value;
      }
    }

    public event ListViewItemMouseEventHandler ItemMouseDown
    {
      add
      {
        this.listViewElement.ItemMouseDown += value;
      }
      remove
      {
        this.listViewElement.ItemMouseDown -= value;
      }
    }

    public event ListViewItemMouseEventHandler ItemMouseUp
    {
      add
      {
        this.listViewElement.ItemMouseUp += value;
      }
      remove
      {
        this.listViewElement.ItemMouseUp -= value;
      }
    }

    public event ListViewItemMouseEventHandler ItemMouseMove
    {
      add
      {
        this.listViewElement.ItemMouseMove += value;
      }
      remove
      {
        this.listViewElement.ItemMouseMove -= value;
      }
    }

    public event ListViewItemEventHandler ItemMouseHover
    {
      add
      {
        this.listViewElement.ItemMouseHover += value;
      }
      remove
      {
        this.listViewElement.ItemMouseHover -= value;
      }
    }

    public event ListViewItemEventHandler ItemMouseEnter
    {
      add
      {
        this.listViewElement.ItemMouseEnter += value;
      }
      remove
      {
        this.listViewElement.ItemMouseEnter -= value;
      }
    }

    public event ListViewItemEventHandler ItemMouseLeave
    {
      add
      {
        this.listViewElement.ItemMouseLeave += value;
      }
      remove
      {
        this.listViewElement.ItemMouseLeave -= value;
      }
    }

    public event ListViewItemEventHandler ItemMouseClick
    {
      add
      {
        this.listViewElement.ItemMouseClick += value;
      }
      remove
      {
        this.listViewElement.ItemMouseClick -= value;
      }
    }

    public event ListViewItemEventHandler ItemMouseDoubleClick
    {
      add
      {
        this.listViewElement.ItemMouseDoubleClick += value;
      }
      remove
      {
        this.listViewElement.ItemMouseDoubleClick -= value;
      }
    }

    public event ListViewItemCancelEventHandler ItemCheckedChanging
    {
      add
      {
        this.listViewElement.ItemCheckedChanging += value;
      }
      remove
      {
        this.listViewElement.ItemCheckedChanging -= value;
      }
    }

    public event ListViewItemEventHandler ItemCheckedChanged
    {
      add
      {
        this.listViewElement.ItemCheckedChanged += value;
      }
      remove
      {
        this.listViewElement.ItemCheckedChanged -= value;
      }
    }

    public event ListViewVisualItemEventHandler VisualItemFormatting
    {
      add
      {
        this.listViewElement.VisualItemFormatting += value;
      }
      remove
      {
        this.listViewElement.VisualItemFormatting -= value;
      }
    }

    public event ListViewItemCreatingEventHandler ItemCreating
    {
      add
      {
        this.listViewElement.ItemCreating += value;
      }
      remove
      {
        this.listViewElement.ItemCreating -= value;
      }
    }

    public event ListViewVisualItemCreatingEventHandler VisualItemCreating
    {
      add
      {
        this.listViewElement.VisualItemCreating += value;
      }
      remove
      {
        this.listViewElement.VisualItemCreating -= value;
      }
    }

    public virtual event ListViewCellFormattingEventHandler CellFormatting
    {
      add
      {
        this.listViewElement.CellFormatting += value;
      }
      remove
      {
        this.listViewElement.CellFormatting -= value;
      }
    }

    public event ListViewItemEventHandler ItemDataBound
    {
      add
      {
        this.listViewElement.ItemDataBound += value;
      }
      remove
      {
        this.listViewElement.ItemDataBound -= value;
      }
    }

    public event ListViewItemEventHandler CurrentItemChanged
    {
      add
      {
        this.listViewElement.CurrentItemChanged += value;
      }
      remove
      {
        this.listViewElement.CurrentItemChanged -= value;
      }
    }

    public event ListViewItemChangingEventHandler CurrentItemChanging
    {
      add
      {
        this.listViewElement.CurrentItemChanging += value;
      }
      remove
      {
        this.listViewElement.CurrentItemChanging -= value;
      }
    }

    public event ListViewItemEditorRequiredEventHandler EditorRequired
    {
      add
      {
        this.listViewElement.EditorRequired += value;
      }
      remove
      {
        this.listViewElement.EditorRequired -= value;
      }
    }

    public event ListViewItemEditingEventHandler ItemEditing
    {
      add
      {
        this.listViewElement.ItemEditing += value;
      }
      remove
      {
        this.listViewElement.ItemEditing -= value;
      }
    }

    public event ListViewItemEditorInitializedEventHandler EditorInitialized
    {
      add
      {
        this.listViewElement.EditorInitialized += value;
      }
      remove
      {
        this.listViewElement.EditorInitialized -= value;
      }
    }

    public event ListViewItemEditedEventHandler ItemEdited
    {
      add
      {
        this.listViewElement.ItemEdited += value;
      }
      remove
      {
        this.listViewElement.ItemEdited -= value;
      }
    }

    public event EventHandler ValidationError
    {
      add
      {
        this.listViewElement.ValidationError += value;
      }
      remove
      {
        this.listViewElement.ValidationError -= value;
      }
    }

    public event ListViewItemValidatingEventHandler ItemValidating
    {
      add
      {
        this.listViewElement.ItemValidating += value;
      }
      remove
      {
        this.listViewElement.ItemValidating -= value;
      }
    }

    public event ListViewItemValueChangedEventHandler ItemValueChanged
    {
      add
      {
        this.listViewElement.ItemValueChanged += value;
      }
      remove
      {
        this.listViewElement.ItemValueChanged -= value;
      }
    }

    public event ListViewItemValueChangingEventHandler ItemValueChanging
    {
      add
      {
        this.listViewElement.ItemValueChanging += value;
      }
      remove
      {
        this.listViewElement.ItemValueChanging -= value;
      }
    }

    public event ListViewColumnCreatingEventHandler ColumnCreating
    {
      add
      {
        this.listViewElement.ColumnCreating += value;
      }
      remove
      {
        this.listViewElement.ColumnCreating -= value;
      }
    }

    public virtual event ListViewCellElementCreatingEventHandler CellCreating
    {
      add
      {
        this.listViewElement.CellCreating += value;
      }
      remove
      {
        this.listViewElement.CellCreating -= value;
      }
    }

    public event ListViewItemCancelEventHandler ItemRemoving
    {
      add
      {
        this.listViewElement.ItemRemoving += value;
      }
      remove
      {
        this.listViewElement.ItemRemoving -= value;
      }
    }

    public event ListViewItemEventHandler ItemRemoved
    {
      add
      {
        this.listViewElement.ItemRemoved += value;
      }
      remove
      {
        this.listViewElement.ItemRemoved -= value;
      }
    }

    public RadListView()
    {
      this.Initialized += new EventHandler(this.RadListView_Initialized);
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
      this.EnableGesture(GestureType.Zoom);
    }

    protected virtual void RadListView_Initialized(object sender, EventArgs e)
    {
      this.Initialized -= new EventHandler(this.RadListView_Initialized);
      if (this.initValues.ContainsKey("HorizontalScrollState"))
        this.HorizontalScrollState = (ScrollState) this.initValues["HorizontalScrollState"];
      if (this.initValues.ContainsKey("VerticalScrollState"))
        this.VerticalScrollState = (ScrollState) this.initValues["VerticalScrollState"];
      if (this.initValues.ContainsKey("AllowArbitraryItemHeight"))
        this.AllowArbitraryItemHeight = (bool) this.initValues["AllowArbitraryItemHeight"];
      if (this.initValues.ContainsKey("AllowArbitraryItemWidth"))
        this.AllowArbitraryItemWidth = (bool) this.initValues["AllowArbitraryItemWidth"];
      if (this.initValues.ContainsKey("FullRowSelect"))
        this.FullRowSelect = (bool) this.initValues["FullRowSelect"];
      if (this.initValues.ContainsKey("ItemSize"))
        this.ItemSize = (Size) this.initValues["ItemSize"];
      if (this.initValues.ContainsKey("GroupItemSize"))
        this.GroupItemSize = (Size) this.initValues["GroupItemSize"];
      if (this.initValues.ContainsKey("GroupIndent"))
        this.GroupIndent = (int) this.initValues["GroupIndent"];
      if (this.initValues.ContainsKey("ItemSpacing"))
        this.ItemSpacing = (int) this.initValues["ItemSpacing"];
      if (this.initValues.ContainsKey("DataSource"))
        this.DataSource = this.initValues["DataSource"];
      if (this.initValues.ContainsKey("DisplayMember"))
        this.DisplayMember = (string) this.initValues["DisplayMember"];
      if (this.initValues.ContainsKey("ValueMember"))
        this.ValueMember = (string) this.initValues["ValueMember"];
      if (!this.initValues.ContainsKey("CheckedMember"))
        return;
      this.CheckedMember = (string) this.initValues["CheckedMember"];
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.listViewElement = this.CreateListViewElement();
      this.listViewElement.PropertyChanged += new PropertyChangedEventHandler(this.listViewElement_PropertyChanged);
      this.RootElement.Children.Add((RadElement) this.listViewElement);
    }

    private void listViewElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnNotifyPropertyChanged(e.PropertyName);
    }

    protected virtual RadListViewElement CreateListViewElement()
    {
      return new RadListViewElement();
    }

    [Description("Gets or sets a value indicating whether column names which differ only in the casing are allowed.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool CaseSensitiveColumnNames
    {
      get
      {
        return this.listViewElement.CaseSensitiveColumnNames;
      }
      set
      {
        this.listViewElement.CaseSensitiveColumnNames = value;
      }
    }

    [DefaultValue(CheckBoxesPosition.Left)]
    [Description("Gets or sets the position of the checkboxes when ShowCheckBoxes is true.")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public CheckBoxesPosition CheckBoxesPosition
    {
      get
      {
        return this.ListViewElement.CheckBoxesPosition;
      }
      set
      {
        this.listViewElement.CheckBoxesPosition = value;
      }
    }

    [DefaultValue(CheckBoxesAlignment.Center)]
    [Browsable(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the position of the checkboxes when ShowCheckBoxes is true.")]
    public CheckBoxesAlignment CheckBoxesAlignment
    {
      get
      {
        return this.ListViewElement.CheckBoxesAlignment;
      }
      set
      {
        this.listViewElement.CheckBoxesAlignment = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Category("Layout")]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the last added item in the RadListView DataSource will be selected by the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool SelectLastAddedItem
    {
      get
      {
        return this.ListViewElement.SelectLastAddedItem;
      }
      set
      {
        if (this.ListViewElement.SelectLastAddedItem == value)
          return;
        this.ListViewElement.SelectLastAddedItem = value;
      }
    }

    [Description("Gets or sets the display state of the horizontal scrollbar.")]
    [Category("Appearance")]
    [DefaultValue(ScrollState.AutoHide)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.ListViewElement.HorizontalScrollState;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (HorizontalScrollState)] = (object) value;
        else
          this.ListViewElement.HorizontalScrollState = value;
      }
    }

    [Description("Gets or sets the display state of the vertical scrollbar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(ScrollState.AutoHide)]
    [Browsable(true)]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.ListViewElement.VerticalScrollState;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (VerticalScrollState)] = (object) value;
        else
          this.ListViewElement.VerticalScrollState = value;
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
        return this.ListViewElement.ThreeStateMode;
      }
      set
      {
        this.ListViewElement.ThreeStateMode = value;
      }
    }

    [Description("Gets or sets value indicating if the user can reorder items via drag and drop.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Browsable(true)]
    public bool AllowDragDrop
    {
      get
      {
        return this.ListViewElement.AllowDragDrop;
      }
      set
      {
        this.ListViewElement.AllowDragDrop = value;
      }
    }

    [Description("Gets or sets a value indicating whether grid lines should be shown in DetailsView.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(false)]
    public virtual bool ShowGridLines
    {
      get
      {
        return this.listViewElement.ShowGridLines;
      }
      set
      {
        this.listViewElement.ShowGridLines = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether items can be selected with mouse dragging.")]
    [Browsable(true)]
    [Category("Behavior")]
    public bool EnableLassoSelection
    {
      get
      {
        return this.ListViewElement.EnableLassoSelection;
      }
      set
      {
        this.ListViewElement.EnableLassoSelection = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled. Always false when lasso selection is enabled.")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool EnableKineticScrolling
    {
      get
      {
        return this.listViewElement.EnableKineticScrolling;
      }
      set
      {
        this.listViewElement.EnableKineticScrolling = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether items should react on mouse hover.")]
    [DefaultValue(true)]
    public bool HotTracking
    {
      get
      {
        return this.listViewElement.HotTracking;
      }
      set
      {
        this.listViewElement.HotTracking = value;
      }
    }

    [Description("Gets or sets a value indicating whether the items should be sorted when clicking on header cells.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool EnableColumnSort
    {
      get
      {
        return this.listViewElement.EnableColumnSort;
      }
      set
      {
        this.listViewElement.EnableColumnSort = value;
      }
    }

    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(typeof (Size), "200,20")]
    [Description("Gets or sets the default item size.")]
    public virtual Size ItemSize
    {
      get
      {
        return this.listViewElement.ItemSize;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (ItemSize)] = (object) value;
        else
          this.listViewElement.ItemSize = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(typeof (Size), "200,20")]
    [Category("Layout")]
    [Description("Gets or sets the default item size.")]
    public Size GroupItemSize
    {
      get
      {
        return this.listViewElement.GroupItemSize;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (GroupItemSize)] = (object) value;
        else
          this.listViewElement.GroupItemSize = value;
      }
    }

    [Category("Layout")]
    [DefaultValue(25)]
    [Browsable(true)]
    [Description("Gets or sets the indent of the items when they are displayed in a group.")]
    public int GroupIndent
    {
      get
      {
        return this.listViewElement.GroupIndent;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (GroupIndent)] = (object) value;
        else
          this.listViewElement.GroupIndent = value;
      }
    }

    [DefaultValue(0)]
    [Description("Gets or sets the space between the items.")]
    [Category("Layout")]
    [Browsable(true)]
    public virtual int ItemSpacing
    {
      get
      {
        return this.listViewElement.ItemSpacing;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (ItemSpacing)] = (object) value;
        else
          this.listViewElement.ItemSpacing = value;
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection of filter descriptors by which you can apply filter rules to the items.")]
    public ListViewFilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.listViewElement.FilterDescriptors;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Predicate<ListViewDataItem> FilterPredicate
    {
      get
      {
        return this.listViewElement.DataView.Filter;
      }
      set
      {
        if (!(this.listViewElement.DataView.Filter != value))
          return;
        this.listViewElement.DataView.Filter = value;
        this.OnNotifyPropertyChanged(nameof (FilterPredicate));
      }
    }

    [Description("Gets a value indicating whether the control is in bound mode.")]
    [Browsable(false)]
    public bool IsDataBound
    {
      get
      {
        return this.listViewElement.IsDataBound;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Editor("Telerik.WinControls.UI.Design.ListViewGroupCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets a collection containing the groups of the RadListView.")]
    [Category("Data")]
    public virtual ListViewDataItemGroupCollection Groups
    {
      get
      {
        return this.listViewElement.Groups;
      }
    }

    [Browsable(true)]
    [DefaultValue("")]
    [Description("Gets or sets the value member.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string ValueMember
    {
      get
      {
        return this.listViewElement.ValueMember;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (ValueMember)] = (object) value;
        else
          this.listViewElement.ValueMember = value;
      }
    }

    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Category("Data")]
    [Description("Gets or sets the display member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string DisplayMember
    {
      get
      {
        return this.listViewElement.DisplayMember;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (DisplayMember)] = (object) value;
        else
          this.listViewElement.DisplayMember = value;
      }
    }

    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the checked member.")]
    [Category("Data")]
    [Browsable(true)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string CheckedMember
    {
      get
      {
        return this.listViewElement.CheckedMember;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (CheckedMember)] = (object) value;
        else
          this.listViewElement.CheckedMember = value;
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
        return this.listViewElement.EnableSorting;
      }
      set
      {
        this.listViewElement.EnableSorting = value;
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
        return this.listViewElement.EnableFiltering;
      }
      set
      {
        this.listViewElement.EnableFiltering = value;
      }
    }

    [Category("Data")]
    [Description("Gets or sets a value indicating whether grouping is enabled.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool EnableGrouping
    {
      get
      {
        return this.listViewElement.EnableGrouping;
      }
      set
      {
        this.listViewElement.EnableGrouping = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Data")]
    [Description("Gets or sets a value indicating whether custom grouping is enabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableCustomGrouping
    {
      get
      {
        return this.listViewElement.EnableCustomGrouping;
      }
      set
      {
        this.listViewElement.EnableCustomGrouping = value;
      }
    }

    [Description("Gets a collection of SortDescriptor which are used to define sorting rules over the ListViewDataItemCollection.")]
    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.listViewElement.SortDescriptors;
      }
    }

    [Description("Gets a collection of GroupDescriptor which are used to define grouping rules over the ListViewDataItemCollection.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.listViewElement.GroupDescriptors;
      }
    }

    [Category("Data")]
    [AttributeProvider(typeof (IListSource))]
    [Description("Gets or sets the data source of a RadListView.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public object DataSource
    {
      get
      {
        return this.listViewElement.DataSource;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (DataSource)] = value;
        else
          this.listViewElement.DataSource = value;
      }
    }

    [Description("Gets or sets the name of the list or table in the data source for which the RadListView is displaying data. ")]
    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Browsable(true)]
    [Category("Data")]
    public string DataMember
    {
      get
      {
        return this.listViewElement.DataMember;
      }
      set
      {
        if (!(this.listViewElement.DataMember != value))
          return;
        this.listViewElement.DataMember = value;
      }
    }

    [Description("Gets or sets the selected item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(true)]
    public ListViewDataItem SelectedItem
    {
      get
      {
        return this.listViewElement.SelectedItem;
      }
      set
      {
        this.listViewElement.SelectedItem = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the index of the selected item.")]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
      get
      {
        return this.listViewElement.SelectedIndex;
      }
      set
      {
        this.listViewElement.SelectedIndex = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets the current item.")]
    public ListViewDataItem CurrentItem
    {
      get
      {
        return this.listViewElement.CurrentItem;
      }
      set
      {
        this.listViewElement.CurrentItem = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the current column in Details View.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListViewDetailColumn CurrentColumn
    {
      get
      {
        return this.listViewElement.CurrentColumn;
      }
      set
      {
        this.listViewElement.CurrentColumn = value;
      }
    }

    [Browsable(false)]
    [Description("Indicates whether there is an active editor.")]
    public bool IsEditing
    {
      get
      {
        return this.listViewElement.IsEditing;
      }
    }

    [Description("Gets or sets a collection of ListViewDetailColumn object which represent the columns in DetailsView.")]
    [Editor("Telerik.WinControls.UI.Design.ListViewColumnCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public ListViewColumnCollection Columns
    {
      get
      {
        return this.listViewElement.Columns;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.ListViewItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets or sets a collection of ListViewDataItem object which represent the items in RadListView.")]
    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ListViewDataItemCollection Items
    {
      get
      {
        return this.listViewElement.Items;
      }
    }

    [DefaultValue(true)]
    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the column headers should be drawn.")]
    public virtual bool ShowColumnHeaders
    {
      get
      {
        return this.listViewElement.ShowColumnHeaders;
      }
      set
      {
        this.listViewElement.ShowColumnHeaders = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the items should be shown in groups.")]
    [Browsable(true)]
    public bool ShowGroups
    {
      get
      {
        return this.listViewElement.ShowGroups;
      }
      set
      {
        this.listViewElement.ShowGroups = value;
      }
    }

    [Browsable(false)]
    [Description("Gets a collection containing the selected items.")]
    public ListViewSelectedItemCollection SelectedItems
    {
      get
      {
        return this.listViewElement.SelectedItems;
      }
    }

    [Browsable(false)]
    [Description("Gets a collection containing the checked items.")]
    public ListViewCheckedItemCollection CheckedItems
    {
      get
      {
        return this.listViewElement.CheckedItems;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Description("Gets or sets value indicating whether checkboxes should be shown.")]
    [Category("Appearance")]
    public virtual bool ShowCheckBoxes
    {
      get
      {
        return this.listViewElement.ShowCheckBoxes;
      }
      set
      {
        this.listViewElement.ShowCheckBoxes = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets value indicating if the user can resize the columns.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public virtual bool AllowColumnResize
    {
      get
      {
        return this.listViewElement.AllowColumnResize;
      }
      set
      {
        this.listViewElement.AllowColumnResize = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets value indicating if the user can reorder columns via drag and drop.")]
    [Category("Behavior")]
    public virtual bool AllowColumnReorder
    {
      get
      {
        return this.listViewElement.AllowColumnReorder;
      }
      set
      {
        this.listViewElement.AllowColumnReorder = value;
      }
    }

    [Description("Gets or sets a value indicating whether the full row should be selected.")]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Browsable(true)]
    public virtual bool FullRowSelect
    {
      get
      {
        return this.listViewElement.FullRowSelect;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (FullRowSelect)] = (object) value;
        else
          this.listViewElement.FullRowSelect = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the items can have different width.")]
    public bool AllowArbitraryItemWidth
    {
      get
      {
        return this.listViewElement.AllowArbitraryItemWidth;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (AllowArbitraryItemWidth)] = (object) value;
        else
          this.listViewElement.AllowArbitraryItemWidth = value;
      }
    }

    [Description("Gets or sets a value indicating whether the items can have different height.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Appearance")]
    public virtual bool AllowArbitraryItemHeight
    {
      get
      {
        return this.listViewElement.AllowArbitraryItemHeight;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (AllowArbitraryItemHeight)] = (object) value;
        else
          this.listViewElement.AllowArbitraryItemHeight = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets value indicating whether multi selection is enabled.")]
    [Browsable(true)]
    public bool MultiSelect
    {
      get
      {
        return this.listViewElement.MultiSelect;
      }
      set
      {
        this.listViewElement.MultiSelect = value;
      }
    }

    [Description("Gets or sets value indicating whether editing is enabled.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Browsable(true)]
    public virtual bool AllowEdit
    {
      get
      {
        return this.listViewElement.AllowEdit;
      }
      set
      {
        this.listViewElement.AllowEdit = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Gets or sets value indicating whether the user can remove items with the Delete key.")]
    public bool AllowRemove
    {
      get
      {
        return this.listViewElement.AllowRemove;
      }
      set
      {
        this.listViewElement.AllowRemove = value;
      }
    }

    [Description("Gets the currently active editor.")]
    [Browsable(false)]
    public IInputEditor ActiveEditor
    {
      get
      {
        return this.listViewElement.ActiveEditor;
      }
    }

    [Category("Appearance")]
    [DefaultValue(ListViewType.ListView)]
    [Browsable(true)]
    [Description("Gets or sets the type of the view.")]
    public virtual ListViewType ViewType
    {
      get
      {
        return this.listViewElement.ViewType;
      }
      set
      {
        this.listViewElement.ViewType = value;
      }
    }

    [Description("Gets the RadListViewElement of RadListView")]
    [Browsable(false)]
    public RadListViewElement ListViewElement
    {
      get
      {
        return this.listViewElement;
      }
    }

    [Description("Gets or sets the height of the header in Details View.")]
    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(35f)]
    public virtual float HeaderHeight
    {
      get
      {
        return this.listViewElement.HeaderHeight;
      }
      set
      {
        this.listViewElement.HeaderHeight = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(120, 95));
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value that specifies how long the user must wait before searching with the keyboard is reset.")]
    [DefaultValue(300)]
    [Category("Behavior")]
    public int KeyboardSearchResetInterval
    {
      get
      {
        return this.listViewElement.KeyboardSearchResetInterval;
      }
      set
      {
        this.listViewElement.KeyboardSearchResetInterval = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value that determines whether the user can search for an item by typing characters when RadListControl is focused.")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool KeyboardSearchEnabled
    {
      get
      {
        return this.listViewElement.KeyboardSearchEnabled;
      }
      set
      {
        this.listViewElement.KeyboardSearchEnabled = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the string comparer used by the keyboard navigation functionality.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IFindStringComparer FindStringComparer
    {
      get
      {
        return this.listViewElement.ViewElement.FindStringComparer;
      }
      set
      {
        this.listViewElement.ViewElement.FindStringComparer = value;
      }
    }

    [Description("Gets or sets a value indicating whether the item's check state changes whenever the item is clicked.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(CheckOnClickMode.Off)]
    public virtual CheckOnClickMode CheckOnClickMode
    {
      get
      {
        return this.listViewElement.CheckOnClickMode;
      }
      set
      {
        this.listViewElement.CheckOnClickMode = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    public void BeginUpdate()
    {
      this.Items.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.Items.EndUpdate();
    }

    public ListViewDataItem FindItemByKey(object key)
    {
      return this.listViewElement.FindItemByKey(key);
    }

    public ListViewDataItem FindItemByKey(object key, bool searchVisibleItems)
    {
      return this.listViewElement.FindItemByKey(key, searchVisibleItems);
    }

    public void Select(ListViewDataItem[] items)
    {
      this.listViewElement.Select(items);
    }

    public bool BeginEdit()
    {
      return this.listViewElement.BeginEdit();
    }

    public bool EndEdit()
    {
      return this.listViewElement.EndEdit();
    }

    public bool CancelEdit()
    {
      return this.listViewElement.CancelEdit();
    }

    public void ExpandAll()
    {
      this.ListViewElement.ExpandAll();
    }

    public void CollapseAll()
    {
      this.ListViewElement.CollapseAll();
    }

    public void CheckSelectedItems()
    {
      this.ListViewElement.CheckSelectedItems();
    }

    public void UncheckSelectedItems()
    {
      this.ListViewElement.UncheckSelectedItems();
    }

    public void CheckAllItems()
    {
      this.ListViewElement.CheckAllItems();
    }

    public void UncheckAllItems()
    {
      this.ListViewElement.UncheckAllItems();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (this.ContainsFocus && this.IsEditing)
        return;
      if (!this.ContainsFocus)
        this.listViewElement.EndEdit();
      this.listViewElement.SynchronizeVisualItems();
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.listViewElement.SynchronizeVisualItems();
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.End:
        case Keys.Home:
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

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.listViewElement.ProcessMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.ListViewElement.ViewElement.VScrollBar.Capture || this.ListViewElement.ViewElement.VScrollBar.ThumbElement.Capture)
      {
        base.OnMouseUp(e);
      }
      else
      {
        if (this.listViewElement.ProcessMouseUp(e))
          return;
        base.OnMouseUp(e);
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.listViewElement.ProcessMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.listViewElement.ProcessKeyDown(e))
        return;
      base.OnKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.listViewElement.ProcessKeyPress(e))
        return;
      base.OnKeyPress(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.listViewElement.ProcessMouseWheel(e))
      {
        HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
        if (handledMouseEventArgs != null)
          handledMouseEventArgs.Handled = true;
      }
      base.OnMouseWheel(e);
    }

    protected override void OnThemeNameChanged(ThemeNameChangedEventArgs e)
    {
      base.OnThemeNameChanged(e);
      this.listViewElement.ViewElement.ViewElement.ElementProvider.ClearCache();
      this.listViewElement.ViewElement.ViewElement.SuspendLayout();
      this.listViewElement.ViewElement.ViewElement.DisposeChildren();
      this.listViewElement.ViewElement.ViewElement.ResumeLayout(true);
    }

    protected override bool CanEditElementAtDesignTime(RadElement element)
    {
      if (element is IVirtualizedElement<ListViewDataItem> || element is IVirtualizedElement<ListViewDetailColumn>)
        return false;
      return base.CanEditElementAtDesignTime(element);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadListViewAccessibleObject(this);
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "FilterPredicate" && this.EnableCustomGrouping)
      {
        foreach (ListViewDataItemGroup group in this.Groups)
        {
          RadListSource<ListViewDataItem> innerList = group.Items.InnerList as RadListSource<ListViewDataItem>;
          if (innerList != null)
            innerList.CollectionView.Filter = this.FilterPredicate;
        }
      }
      base.OnNotifyPropertyChanged(e);
    }
  }
}
