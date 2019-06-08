// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  public class RadListElement : VirtualizedScrollPanel<RadListDataItem, RadListVisualItem>
  {
    public static readonly RadProperty CaseSensitiveSortProperty = RadProperty.Register(nameof (CaseSensitiveSort), typeof (bool), typeof (RadListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    private static readonly RadProperty CollapsibleGroupItemsOffsetProperty = RadProperty.Register(nameof (CollapsibleGroupItemsOffset), typeof (float), typeof (RadListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 15, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange));
    private static readonly RadProperty NonCollapsibleGroupItemsOffsetProperty = RadProperty.Register(nameof (NonCollapsibleGroupItemsOffset), typeof (float), typeof (RadListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange));
    internal static readonly RadProperty ItemHeightProperty = RadProperty.Register(nameof (ItemHeight), typeof (int), typeof (RadListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 18, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange));
    public static RadProperty AlternatingItemColorProperty = RadProperty.Register(nameof (AlternatingItemColor), typeof (Color), typeof (RadListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Lavender, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EnableAlternatingItemColorProperty = RadProperty.Register(nameof (EnableAlternatingItemColor), typeof (bool), typeof (RadListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private float cachedCollapsibleGroupItemsOffset = 15f;
    private float cachedNonCollapsibleGroupItemsOffset = 4f;
    private SelectionMode selectionMode = SelectionMode.One;
    private int oldSelectedIndex = -1;
    private int selectedIndexBeforeSelectionMode = -1;
    private string formatString = "";
    private IFormatProvider formatInfo = (IFormatProvider) CultureInfo.CurrentCulture;
    private bool formattingEnabled = true;
    private ItemTextComparisonMode itemTextComparisonMode = ItemTextComparisonMode.UserText;
    private bool keyboardSearchEnabled = true;
    private int searchStartIndex = -1;
    private int bindingContextPosition = -1;
    private int indexBeforeItemsChange = -1;
    private Point dragStart = Point.Empty;
    internal const int DefaultItemHeight = 18;
    internal const int DefaultItemHeightWithDescription = 36;
    private bool allowDragDrop;
    private DefaultListControlStackContainer viewElement;
    private int beginUpdateCount;
    private int subscriptionCounter;
    private bool suspendSelectionEvents;
    private bool suspendItemsChangeEvents;
    private RadListDataItem oldSelectedItem;
    private ListDataLayer dataLayer;
    private SortDescriptorCollection sortDescriptors;
    private RadListDataItem preFilterItem;
    internal ListGroupFactory groupFactory;
    private int suspendGroupRefresh;
    private bool showGroups;
    private RadListDataItem activeListItem;
    private RadListDataItemSelectedCollection selectedItems;
    private object newValue;
    private object oldValue;
    private IFindStringComparer findStringComparer;
    private Timer searchTimer;
    private StringBuilder searchBuffer;
    private object bindingContextDataSource;
    private int shiftSelectStartIndex;
    private ListControlDragDropService dragDropService;
    private ScrollServiceBehavior scrollBehavior;
    private bool enableKineticScrolling;
    private bool readOnly;
    private bool isDescriptionText;
    private bool isRemoving;
    private bool scheduleUpdateScroller;
    private bool bindingContextChanged;
    private bool handleSelectionOneInProgress;

    public RadListElement()
    {
      this.viewElement = (DefaultListControlStackContainer) this.ViewElement;
      this.Items = (IList<RadListDataItem>) this.dataLayer.Items;
      this.NotifyParentOnMouseInput = true;
      this.AllowDrag = true;
      this.AllowDrop = true;
      this.dragDropService = new ListControlDragDropService(this);
      this.scrollBehavior = new ScrollServiceBehavior();
      this.scrollBehavior.Add(new ScrollService((RadElement) this.ViewElement, this.HScrollBar));
      this.scrollBehavior.Add(new ScrollService((RadElement) this.ViewElement, this.VScrollBar));
      this.VScrollBar.ClampValue = true;
    }

    public ListControlDragDropService DragDropService
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

    protected internal virtual bool IsDescriptionText
    {
      get
      {
        return this.isDescriptionText;
      }
      set
      {
        this.isDescriptionText = value;
      }
    }

    protected override void WireEvents()
    {
      base.WireEvents();
      this.WireCurrentPosition();
      this.dataLayer.DataView.CollectionChanged += new NotifyCollectionChangedEventHandler(this.DataView_CollectionChanged);
    }

    protected override void UnwireEvents()
    {
      base.UnwireEvents();
      this.UnwireCurrentPosition();
      this.dataLayer.DataView.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.DataView_CollectionChanged);
      if (!(this.DataSource is Component))
        return;
      (this.DataSource as Component).Disposed -= new EventHandler(this.ListElementDataSource_Disposed);
    }

    protected virtual IFindStringComparer CreateStringComparer()
    {
      return (IFindStringComparer) new StartsWithFindStringComparer();
    }

    protected internal virtual int GetDefaultItemHeight()
    {
      return TelerikDpiHelper.ScaleInt(!string.IsNullOrEmpty(this.DescriptionTextMember) || this.IsDescriptionText ? 36 : 18, this.DpiScaleFactor);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.dragStart = e.Location;
    }

    protected virtual ListDataLayer CreateDataLayer()
    {
      return new ListDataLayer(this);
    }

    protected override IVirtualizedElementProvider<RadListDataItem> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<RadListDataItem>) new ListElementProvider(this);
    }

    protected override VirtualizedStackContainer<RadListDataItem> CreateViewElement()
    {
      return (VirtualizedStackContainer<RadListDataItem>) new DefaultListControlStackContainer();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.groupFactory = new ListGroupFactory(this);
      this.showGroups = false;
      this.DrawBorder = true;
      BindingContext bindingContext = this.BindingContext;
      this.selectedItems = new RadListDataItemSelectedCollection(this);
      this.dataLayer = this.CreateDataLayer();
      this.dataLayer.DataView.GroupFactory = (IGroupFactory<RadListDataItem>) this.groupFactory;
      this.dataLayer.DataView.CanGroup = false;
      this.dataLayer.DisplayMember = "";
      this.dataLayer.ValueMember = "";
      this.dataLayer.ChangeCurrentOnAdd = false;
      this.dataLayer.DataView.PropertyChanged += new PropertyChangedEventHandler(this.DataView_PropertyChanged);
      this.dataLayer.DataView.Comparer = (IComparer<RadListDataItem>) new RadListElement.ListItemAscendingComparer();
      this.sortDescriptors = this.dataLayer.DataView.SortDescriptors;
      this.FindStringComparer = this.CreateStringComparer();
      this.searchTimer = new Timer();
      this.searchTimer.Interval = 300;
      this.searchTimer.Tick += new EventHandler(this.SearchTimer_Tick);
      this.searchBuffer = new StringBuilder();
    }

    protected override void InitializeItemScroller(ItemScroller<RadListDataItem> scroller)
    {
      base.InitializeItemScroller(scroller);
      scroller.ScrollMode = ItemScrollerScrollModes.Smooth;
    }

    public ListDataLayer DataLayer
    {
      get
      {
        return this.dataLayer;
      }
    }

    internal ListGroupCollection Groups
    {
      get
      {
        return this.groupFactory.Groups;
      }
    }

    internal bool ShowGroups
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
        this.dataLayer.DataView.CanGroup = value;
        this.UpdateItemTraverser();
      }
    }

    private bool IsOldSelectedIndexInInitialState
    {
      get
      {
        return this.oldSelectedIndex == -2;
      }
    }

    protected bool HasSelectedValueChanged
    {
      get
      {
        bool flag = false;
        if (this.newValue != null && !this.newValue.Equals(this.oldValue))
          flag = true;
        if (!flag && this.oldValue != null && !this.oldValue.Equals(this.newValue))
          flag = true;
        return flag;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets value indicating if the user can reorder items via drag and drop. Always false when using kinetic scrolling")]
    [Browsable(true)]
    [Category("Behavior")]
    public bool AllowDragDrop
    {
      get
      {
        if (this.EnableKineticScrolling)
          return false;
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

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether alternating item color is enabled.")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public virtual bool EnableAlternatingItemColor
    {
      get
      {
        return (bool) this.GetValue(RadListElement.EnableAlternatingItemColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadListElement.EnableAlternatingItemColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("AlternatingItemColor", typeof (RadListElement))]
    [Description("Gets or sets a value indidcating the alternating item color for odd items.")]
    public virtual Color AlternatingItemColor
    {
      get
      {
        return (Color) this.GetValue(RadListElement.AlternatingItemColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadListElement.AlternatingItemColorProperty, (object) value);
      }
    }

    [DefaultValue(true)]
    public override bool FitItemsToSize
    {
      get
      {
        return base.FitItemsToSize;
      }
      set
      {
        foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) this.Items)
          radListDataItem.MeasuredSize = SizeF.Empty;
        base.FitItemsToSize = value;
      }
    }

    public ScrollServiceBehavior ScrollBehavior
    {
      get
      {
        return this.scrollBehavior;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
    [Category("Behavior")]
    public bool EnableKineticScrolling
    {
      get
      {
        return this.enableKineticScrolling;
      }
      set
      {
        this.enableKineticScrolling = value;
        if (value)
          return;
        this.scrollBehavior.Stop();
      }
    }

    public RadListDataItem FindItemExact(string text, bool caseSensitive)
    {
      foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) this.Items)
      {
        if (radListDataItem.Text.Equals(text, caseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
          return radListDataItem;
      }
      return (RadListDataItem) null;
    }

    internal float CollapsibleGroupItemsOffset
    {
      get
      {
        return this.cachedCollapsibleGroupItemsOffset * this.DpiScaleFactor.Height;
      }
      set
      {
        if ((double) value == (double) this.cachedCollapsibleGroupItemsOffset)
          return;
        this.cachedCollapsibleGroupItemsOffset = value;
        int num = (int) this.SetValue(RadListElement.CollapsibleGroupItemsOffsetProperty, (object) value);
      }
    }

    internal float NonCollapsibleGroupItemsOffset
    {
      get
      {
        return this.cachedNonCollapsibleGroupItemsOffset * this.DpiScaleFactor.Height;
      }
      set
      {
        if ((double) value == (double) this.cachedNonCollapsibleGroupItemsOffset)
          return;
        this.cachedNonCollapsibleGroupItemsOffset = value;
        int num = (int) this.SetValue(RadListElement.NonCollapsibleGroupItemsOffsetProperty, (object) value);
      }
    }

    public virtual bool IsUpdating
    {
      get
      {
        return this.beginUpdateCount > 0;
      }
    }

    public bool SuspendItemsChangeEvents
    {
      get
      {
        return this.suspendItemsChangeEvents;
      }
      set
      {
        this.suspendItemsChangeEvents = value;
        this.OnNotifyPropertyChanged(nameof (SuspendItemsChangeEvents));
      }
    }

    public bool CaseSensitiveSort
    {
      get
      {
        return (bool) this.GetValue(RadListElement.CaseSensitiveSortProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadListElement.CaseSensitiveSortProperty, (object) value);
      }
    }

    public int KeyboardSearchResetInterval
    {
      get
      {
        return this.searchTimer.Interval;
      }
      set
      {
        if (this.searchTimer.Interval == value)
          return;
        this.searchTimer.Interval = value;
        this.OnNotifyPropertyChanged(nameof (KeyboardSearchResetInterval));
      }
    }

    public bool KeyboardSearchEnabled
    {
      get
      {
        return this.keyboardSearchEnabled;
      }
      set
      {
        if (this.keyboardSearchEnabled == value)
          return;
        this.keyboardSearchEnabled = value;
        this.OnNotifyPropertyChanged(nameof (KeyboardSearchEnabled));
      }
    }

    public ItemTextComparisonMode ItemTextComparisonMode
    {
      get
      {
        return this.itemTextComparisonMode;
      }
      set
      {
        if (this.itemTextComparisonMode == value)
          return;
        this.itemTextComparisonMode = value;
        this.OnNotifyPropertyChanged(nameof (ItemTextComparisonMode));
      }
    }

    public Predicate<RadListDataItem> Filter
    {
      get
      {
        return this.dataLayer.DataView.Filter;
      }
      set
      {
        if (this.dataLayer.DataView.Filter == value)
          return;
        this.preFilterItem = this.SelectedItem;
        this.dataLayer.DataView.Filter = value;
        this.OnNotifyPropertyChanged(nameof (Filter));
        this.EnsureSelectedIndexOnFilterChanged();
      }
    }

    private void EnsureSelectedIndexOnFilterChanged()
    {
      if (this.preFilterItem != null)
      {
        int num = this.Items.IndexOf(this.preFilterItem);
        if (num >= 0)
        {
          this.SelectedIndex = num;
          this.preFilterItem = (RadListDataItem) null;
          return;
        }
      }
      if (this.Items.Count <= 0)
        return;
      this.SelectedIndex = 0;
    }

    public string FilterExpression
    {
      get
      {
        return this.dataLayer.DataView.FilterExpression;
      }
      set
      {
        if (this.dataLayer.DataView.FilterExpression == value)
          return;
        this.dataLayer.DataView.FilterExpression = value;
        this.OnNotifyPropertyChanged(nameof (FilterExpression));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Browsable(false)]
    public IFindStringComparer FindStringComparer
    {
      get
      {
        return this.findStringComparer;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("The StringSearchComparer can not be set to null.");
        this.findStringComparer = value;
        this.OnNotifyPropertyChanged(nameof (FindStringComparer));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IComparer<RadListDataItem> ItemsSortComparer
    {
      get
      {
        return this.dataLayer.DataView.Comparer;
      }
      set
      {
        if (value == null)
          throw new ArgumentException("ItemsSortComparer can not be null.");
        this.SetSortComparer(value, ListSortDirection.Ascending);
        this.OnNotifyPropertyChanged(nameof (ItemsSortComparer));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadListDataItem ActiveItem
    {
      get
      {
        return this.activeListItem;
      }
      set
      {
        if (value != null && value.Owner != this)
          throw new ArgumentException("Provided item does not exist in the Items collection.");
        this.UpdateActiveItem(value, true);
      }
    }

    public IReadOnlyCollection<RadListDataItem> SelectedItems
    {
      get
      {
        return (IReadOnlyCollection<RadListDataItem>) this.selectedItems;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool SuspendSelectionEvents
    {
      get
      {
        return this.suspendSelectionEvents;
      }
      set
      {
        if (this.suspendSelectionEvents == value)
          return;
        this.suspendSelectionEvents = value;
        this.OnNotifyPropertyChanged(nameof (SuspendSelectionEvents));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SelectionMode SelectionMode
    {
      get
      {
        return this.selectionMode;
      }
      set
      {
        this.SetSelectionMode(value);
      }
    }

    public object DataSource
    {
      get
      {
        return this.dataLayer.DataSource;
      }
      set
      {
        if (this.DataSource == value)
          return;
        if (this.DataSource is Component)
          (this.DataSource as Component).Disposed -= new EventHandler(this.ListElementDataSource_Disposed);
        if (value == null)
        {
          this.dataLayer.DisplayMember = "";
          this.dataLayer.ValueMember = "";
          this.DisposeItems();
          this.dataLayer.ChangeCurrentOnAdd = false;
        }
        else
        {
          this.CheckReadyForBinding();
          if (value is Component)
            (value as Component).Disposed += new EventHandler(this.ListElementDataSource_Disposed);
          this.dataLayer.ChangeCurrentOnAdd = true;
        }
        this.BeginUpdate();
        this.oldValue = this.SelectedValue;
        this.ViewElement.Children.Clear();
        this.ViewElement.ElementProvider.ClearCache();
        this.activeListItem = (RadListDataItem) null;
        if (this.selectedItems.Count > 0)
          this.selectedItems.Clear();
        this.dataLayer.DataSource = value;
        this.EndUpdate();
        this.OnNotifyPropertyChanged(nameof (DataSource));
        this.OnDataBindingComplete((object) this, new ListBindingCompleteEventArgs(ListChangedType.Reset));
      }
    }

    private bool BindListSource(object value)
    {
      if (value is IListSource && this.dataLayer.DisplayMember.Contains("."))
      {
        int length = this.dataLayer.DisplayMember.LastIndexOf(".");
        if (length != -1)
        {
          this.dataLayer.DataSource = (object) this.BindingContext[value, this.dataLayer.DisplayMember.Substring(0, length)];
          this.dataLayer.DisplayMember = this.dataLayer.DisplayMember.Substring(length + 1);
          int num = this.dataLayer.ValueMember.LastIndexOf(".");
          if (!string.IsNullOrEmpty(this.dataLayer.ValueMember) && num != -1)
            this.dataLayer.ValueMember = this.dataLayer.ValueMember.Substring(num + 1);
          return true;
        }
      }
      return false;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectedIndex
    {
      get
      {
        if (this.selectedItems.Count == 0 || this.Items.Count == 0)
          return -1;
        return this.dataLayer.CurrentPosition;
      }
      set
      {
        if (value >= 0 && value < this.Items.Count)
          this.SetSelectedItem(this.Items[value]);
        this.SetSelectedIndex(value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadListDataItem SelectedItem
    {
      get
      {
        if (this.selectedItems.Count == 0)
          return (RadListDataItem) null;
        if (this.dataLayer.CurrentPosition == -1)
          return (RadListDataItem) null;
        if (this.Items.Count == 0)
          return (RadListDataItem) null;
        try
        {
          return this.dataLayer.CurrentItem;
        }
        catch (ArgumentOutOfRangeException ex)
        {
          return (RadListDataItem) null;
        }
      }
      set
      {
        this.SetSelectedItem(value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object SelectedValue
    {
      get
      {
        if (this.selectedItems.Count == 0)
          return (object) null;
        if (this.dataLayer.CurrentPosition == -1)
          return (object) null;
        if (this.Items.Count == 0)
          return (object) null;
        return this.dataLayer.CurrentItem?.Value;
      }
      set
      {
        this.SetSelectedValue(value);
      }
    }

    public string DataMember
    {
      get
      {
        return this.dataLayer.DataMember;
      }
      set
      {
        this.dataLayer.DataMember = value;
      }
    }

    public string DisplayMember
    {
      get
      {
        return this.dataLayer.DisplayMember;
      }
      set
      {
        if (this.dataLayer.DisplayMember == value)
          return;
        this.dataLayer.DisplayMember = value ?? "";
        this.viewElement.ForceVisualStateUpdate();
        this.OnNotifyPropertyChanged(nameof (DisplayMember));
      }
    }

    [DefaultValue("")]
    public string DescriptionTextMember
    {
      get
      {
        return this.dataLayer.DescriptionTextMember;
      }
      set
      {
        if (this.dataLayer.DescriptionTextMember == value)
          return;
        this.dataLayer.DescriptionTextMember = value ?? "";
        int num = (int) this.SetDefaultValueOverride(RadListElement.ItemHeightProperty, (object) this.GetDefaultItemHeight());
        this.viewElement.ForceVisualStateUpdate();
        this.OnNotifyPropertyChanged(nameof (DescriptionTextMember));
      }
    }

    internal string CheckedMember
    {
      get
      {
        return this.dataLayer.CheckedMember;
      }
      set
      {
        if (this.dataLayer.CheckedMember == value)
          return;
        this.dataLayer.CheckedMember = value ?? "";
        this.viewElement.ForceVisualStateUpdate();
        this.OnNotifyPropertyChanged(nameof (CheckedMember));
      }
    }

    public string ValueMember
    {
      get
      {
        return this.dataLayer.ValueMember;
      }
      set
      {
        if (this.dataLayer.ValueMember == value)
          return;
        this.dataLayer.ValueMember = value ?? "";
        this.newValue = this.SelectedValue;
        this.OnSelectedValueChanged(this.SelectedIndex);
        this.OnNotifyPropertyChanged(nameof (ValueMember));
        if (!(this.DisplayMember == ""))
          return;
        this.DisplayMember = value;
      }
    }

    public int ItemHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadListElement.ItemHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadListElement.ItemHeightProperty, (object) value);
      }
    }

    public SortStyle SortStyle
    {
      get
      {
        if (this.sortDescriptors.Count > 0)
          return this.GetSortStyle(this.sortDescriptors[0].Direction);
        return SortStyle.None;
      }
      set
      {
        this.SetSortStyle(value);
      }
    }

    public bool FormattingEnabled
    {
      get
      {
        return this.formattingEnabled;
      }
      set
      {
        this.formattingEnabled = value;
        this.OnNotifyPropertyChanged(nameof (FormattingEnabled));
      }
    }

    public string FormatString
    {
      get
      {
        return this.formatString;
      }
      set
      {
        if (this.formatString == value)
          return;
        this.formatString = value;
        this.OnNotifyPropertyChanged(nameof (FormatString));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IFormatProvider FormatInfo
    {
      get
      {
        return this.formatInfo;
      }
      set
      {
        if (this.formatInfo == value)
          return;
        this.formatInfo = value;
        this.OnNotifyPropertyChanged(nameof (FormatInfo));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ItemScrollerScrollModes ScrollMode
    {
      get
      {
        return this.Scroller.ScrollMode;
      }
      set
      {
        ItemScrollerScrollModes scrollMode = this.Scroller.ScrollMode;
        this.Scroller.ScrollMode = value;
        if (value == scrollMode)
          return;
        this.OnNotifyPropertyChanged(nameof (ScrollMode));
      }
    }

    public bool IsFilterActive
    {
      get
      {
        if (this.Filter == null)
          return !string.IsNullOrEmpty(this.FilterExpression);
        return true;
      }
    }

    public bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        this.readOnly = value;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [Description("Fires after data binding operation has finished.")]
    public event ListBindingCompleteEventHandler DataBindingComplete;

    protected virtual void OnDataBindingComplete(object sender, ListBindingCompleteEventArgs e)
    {
      if (this.DataBindingComplete == null)
        return;
      this.DataBindingComplete((object) this, e);
    }

    public event EventHandler SelectedValueChanged;

    public event Telerik.WinControls.UI.Data.PositionChangedEventHandler SelectedIndexChanged;

    public event Telerik.WinControls.UI.Data.PositionChangingEventHandler SelectedIndexChanging;

    public event ListItemDataBindingEventHandler ItemDataBinding;

    public event ListItemDataBoundEventHandler ItemDataBound;

    public event CreatingVisualListItemEventHandler CreatingVisualItem;

    public event SortStyleChangedEventHandler SortStyleChanged;

    public event VisualListItemFormattingEventHandler VisualItemFormatting;

    public event NotifyCollectionChangedEventHandler ItemsChanged;

    public event NotifyCollectionChangingEventHandler ItemsChanging;

    public event RadPropertyChangedEventHandler DataItemPropertyChanged;

    public event NotifyCollectionChangedEventHandler SelectedItemsChanged
    {
      add
      {
        this.selectedItems.CollectionChanged += value;
      }
      remove
      {
        this.selectedItems.CollectionChanged -= value;
      }
    }

    public event NotifyCollectionChangingEventHandler SelectedItemsChanging
    {
      add
      {
        this.selectedItems.CollectionChanging += value;
      }
      remove
      {
        this.selectedItems.CollectionChanging -= value;
      }
    }

    public void ClearSelected()
    {
      this.SelectRange(-1, -1);
    }

    internal void SuspendGroupRefresh()
    {
      ++this.suspendGroupRefresh;
    }

    internal void ResumeGroupRefresh(bool performGroupRefresh)
    {
      if (this.suspendGroupRefresh > 0)
        --this.suspendGroupRefresh;
      if (!performGroupRefresh)
        return;
      this.UpdateItemTraverser();
    }

    internal void RefreshGroups()
    {
      this.UpdateItemTraverser();
    }

    public void ScrollToActiveItem()
    {
      this.ScrollToItem(this.activeListItem);
    }

    public void Rebind()
    {
      this.dataLayer.ListSource.Reset();
    }

    public void BeginUpdate()
    {
      ++this.beginUpdateCount;
      this.dataLayer.ListSource.BeginUpdate();
    }

    public void EndUpdate()
    {
      if (this.beginUpdateCount == 0)
        return;
      --this.beginUpdateCount;
      this.dataLayer.ListSource.EndUpdate();
      if (this.beginUpdateCount > 0)
        return;
      this.ViewElement.UpdateItems();
      this.Scroller.UpdateScrollRange();
      int currentPosition = this.dataLayer.CurrentPosition;
      if (this.SelectedIndex != currentPosition)
        this.ProcessSelection(currentPosition, false, InputType.Code, 0);
      if (this.oldSelectedIndex != currentPosition)
        this.OnSelectedIndexChanged(currentPosition);
      this.UpdateLayout();
      this.Invalidate();
    }

    internal void EndUpdate(bool notify)
    {
      if (!notify)
        --this.beginUpdateCount;
      else
        this.EndUpdate();
    }

    public virtual IDisposable DeferRefresh()
    {
      this.BeginUpdate();
      return (IDisposable) new RadListElement.DeferHelper(this);
    }

    public void SelectAll()
    {
      if (this.selectionMode == SelectionMode.One || this.selectionMode == SelectionMode.None)
        throw new InvalidOperationException("Selecting all items is not a valid operation in the current selection mode. SelectionMode = " + this.selectionMode.ToString() + ".");
      this.SelectRange(0, this.Items.Count - 1);
    }

    public void SelectRange(int startIndex, int endIndex)
    {
      if (this.selectedItems.Count > 0)
        this.selectedItems.Clear();
      if (startIndex == -1 || endIndex == -1)
        return;
      if (!this.IsIndexValid(startIndex))
        throw new ArgumentException("Start index is out of range.");
      if (!this.IsIndexValid(endIndex))
        throw new ArgumentException("End index is out of range.");
      this.UnwireCurrentPosition();
      this.HandleMultiSelectRange(startIndex, endIndex);
      this.WireCurrentPosition();
    }

    public void ScrollByPage(int pageCount)
    {
      if (pageCount == 0)
        return;
      RadScrollBarElement scrollbar = this.Scroller.Scrollbar;
      int num = pageCount * scrollbar.LargeChange + scrollbar.Value;
      this.ClampValue(scrollbar.Minimum, scrollbar.Maximum - scrollbar.LargeChange, ref num);
      scrollbar.Value = num;
    }

    public void ScrollToItem(RadListDataItem item)
    {
      if (item == null || this.ViewElement.Children.Count == 0)
        return;
      RadListVisualItem visualItem = item.VisualItem;
      bool flag = this.ScrollMode == ItemScrollerScrollModes.Discrete;
      if (visualItem != null)
      {
        if (visualItem.ControlBoundingRectangle.Y < this.ViewElement.ControlBoundingRectangle.Y)
        {
          this.VScrollBar.Value -= flag ? 1 : this.ViewElement.ControlBoundingRectangle.Y - visualItem.ControlBoundingRectangle.Y;
        }
        else
        {
          if (visualItem.ControlBoundingRectangle.Bottom <= this.ViewElement.ControlBoundingRectangle.Bottom)
            return;
          this.VScrollBar.Value += flag ? 1 : visualItem.ControlBoundingRectangle.Bottom - this.ViewElement.ControlBoundingRectangle.Bottom;
        }
      }
      else
      {
        int visibleItemIndex1 = this.GetLastVisibleItemIndex();
        if (item.RowIndex == visibleItemIndex1 + 1)
        {
          RadListVisualItem child = (RadListVisualItem) this.ViewElement.Children[this.ViewElement.Children.Count - 1];
          int num1 = this.VScrollBar.Value;
          int num2;
          if (flag)
          {
            num2 = num1 + 1;
          }
          else
          {
            int num3 = child.ControlBoundingRectangle.Bottom - this.ViewElement.ControlBoundingRectangle.Bottom;
            if (num3 < 0)
              return;
            num2 = num1 + (num3 + this.Scroller.GetScrollHeight(item) + this.ItemSpacing);
          }
          this.VScrollBar.Value = num2;
        }
        else
        {
          if (flag)
          {
            int visibleItemIndex2 = this.GetFirstVisibleItemIndex();
            if (item.RowIndex == visibleItemIndex2 - 1)
            {
              --this.VScrollBar.Value;
              return;
            }
          }
          this.Scroller.ScrollToItem(item);
        }
      }
    }

    public int FindString(string s)
    {
      if (this.Items.Count == 0)
        return -1;
      return this.FindString(s, 0);
    }

    public int FindString(string s, int startIndex)
    {
      for (int index = startIndex; index < this.Items.Count; ++index)
      {
        RadListDataItem dataItem = this.Items[index];
        if (dataItem.Enabled && this.FindStringComparer.Compare(this.GetItemText(dataItem), s))
          return dataItem.RowIndex;
      }
      for (int index = 0; index < startIndex; ++index)
      {
        RadListDataItem dataItem = this.Items[index];
        if (dataItem.Enabled && this.FindStringComparer.Compare(this.GetItemText(dataItem), s))
          return dataItem.RowIndex;
      }
      return -1;
    }

    public int FindStringExact(string s)
    {
      return this.FindStringExact(s, 0);
    }

    public int FindStringExact(string s, int startIndex)
    {
      IFindStringComparer findStringComparer = this.FindStringComparer;
      this.FindStringComparer = (IFindStringComparer) new ExactFindStringComparer();
      int num = this.FindString(s, startIndex);
      this.FindStringComparer = findStringComparer;
      return num;
    }

    public int FindStringNonWrapping(string s)
    {
      return this.FindStringNonWrapping(s, 0);
    }

    public int FindStringNonWrapping(string s, int startIndex)
    {
      for (int index = startIndex; index < this.Items.Count; ++index)
      {
        if (this.FindStringComparer.Compare(this.GetItemText(this.Items[index]), s))
          return index;
      }
      return -1;
    }

    protected internal void UpdateItemTraverser()
    {
      if (this.suspendGroupRefresh != 0)
        return;
      this.dataLayer.DataView.LazyRefresh();
    }

    protected int GetMinorScrollOffset(int direction)
    {
      Rectangle rectangle = Rectangle.Round(this.GetClientRectangle(this.PreviousConstraint));
      if (direction == 1)
        return this.viewElement.Children[this.viewElement.Children.Count - 1].BoundingRectangle.Bottom - rectangle.Height;
      if (direction != -1)
        return 0;
      int top = this.viewElement.Children[0].BoundingRectangle.Top;
      return rectangle.Top - top;
    }

    protected bool ItemFullyVisible(RadListDataItem item)
    {
      if (item == null)
        return false;
      RadListVisualItem visualItem = item.VisualItem;
      if (visualItem != null)
        return !this.IsItemPartiallyVisible(visualItem);
      return false;
    }

    protected int GetLastVisibleItemIndex()
    {
      if (this.viewElement.Children.Count == 0)
        return -1;
      return ((RadListVisualItem) this.viewElement.Children[this.viewElement.Children.Count - 1]).Data.RowIndex;
    }

    protected int GetFirstVisibleItemIndex()
    {
      if (this.viewElement.Children.Count == 0)
        return -1;
      return ((RadListVisualItem) this.viewElement.Children[0]).Data.RowIndex;
    }

    protected int GetMiddleVisibleItemIndex()
    {
      RadElementCollection children = this.viewElement.Children;
      if (children.Count == 0)
        return -1;
      return ((RadListVisualItem) children[children.Count / 2]).Data.RowIndex;
    }

    protected bool IsItemPartiallyVisible(RadListVisualItem item)
    {
      Rectangle bounds = this.viewElement.Bounds;
      bool flag = bounds.Contains(item.BoundingRectangle);
      bounds.Intersect(item.BoundingRectangle);
      if (item.ControlBoundingRectangle.Bottom > this.viewElement.ControlBoundingRectangle.Bottom)
        return true;
      if (!flag)
        return !bounds.IsEmpty;
      return false;
    }

    protected void DataView_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Comparer"))
        return;
      this.dataLayer.DataView.GroupPredicate = new GroupPredicate<RadListDataItem>(this.DataViewGroupPredicate);
    }

    protected object DataViewGroupPredicate(RadListDataItem item, int level)
    {
      if (item.Group == null)
        return (object) 0;
      return item.Group.Key;
    }

    private void ListElementDataSource_Disposed(object sender, EventArgs e)
    {
      this.DisposeItems();
    }

    private void DataView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
    {
      if (this.beginUpdateCount > 0)
        return;
      switch (args.Action)
      {
        case NotifyCollectionChangedAction.Add:
          this.HandleItemsAdded(args);
          break;
        case NotifyCollectionChangedAction.Remove:
          this.HandleItemsRemoved(args);
          break;
        case NotifyCollectionChangedAction.Replace:
          this.HandleItemsReplaced(args);
          break;
        case NotifyCollectionChangedAction.Reset:
          this.HandleItemsReset(args);
          break;
        case NotifyCollectionChangedAction.ItemChanged:
          this.SynchronizeVisualItems();
          break;
      }
      if (this.activeListItem != null && !this.IsIndexValid(this.activeListItem.RowIndex))
        this.UpdateActiveItem(this.activeListItem, false);
      this.Scroller.UpdateScrollRange();
      this.ViewElement.UpdateItems();
      this.InvalidateArrange();
      this.Invalidate();
      this.ScrollToActiveItem();
    }

    protected virtual void SynchronizeVisualItems()
    {
      foreach (RadListVisualItem child in this.ViewElement.Children)
        child.Synchronize();
      this.Invalidate();
    }

    protected virtual void HandleItemsReplaced(NotifyCollectionChangedEventArgs args)
    {
      for (int index = 0; index < args.NewItems.Count; ++index)
      {
        RadListDataItem newItem = (RadListDataItem) args.NewItems[index];
        newItem.Owner = this;
        newItem.DataLayer = this.dataLayer;
        if (((RadListDataItem) args.OldItems[index]).Active)
          newItem.Active = true;
      }
    }

    protected virtual void HandleItemsReset(NotifyCollectionChangedEventArgs args)
    {
      this.activeListItem = (RadListDataItem) null;
      this.oldSelectedItem = (RadListDataItem) null;
      if (this.oldSelectedIndex != -1 && !this.bindingContextChanged)
      {
        this.OnSelectedIndexChanged(-1);
        this.oldSelectedIndex = -1;
      }
      if (this.selectedItems.Count > 0)
        this.selectedItems.Clear();
      this.ViewElement.DisposeChildren();
      this.ViewElement.ElementProvider.ClearCache();
      this.ViewElement.UpdateItems();
      this.UpdateFitToSizeMode();
      this.Scroller.UpdateScrollRange();
    }

    protected internal virtual void OnSelectedItemAdded(RadListDataItem newItem)
    {
      if (!newItem.Enabled)
        return;
      switch (this.SelectionMode)
      {
        case SelectionMode.None:
          newItem.Selected = false;
          newItem.Active = false;
          break;
        case SelectionMode.One:
          if (this.SelectedIndex != -1 && !newItem.Selected)
            break;
          this.SelectedItem = newItem;
          break;
        default:
          if (this.SelectedIndex == -1)
          {
            this.SelectedItem = newItem;
            break;
          }
          this.selectedItems.Add(newItem);
          newItem.Active = false;
          break;
      }
    }

    protected virtual void OnActiveItemAdded(RadListDataItem newItem)
    {
      if (this.ActiveItem != null)
      {
        newItem.Active = false;
      }
      else
      {
        switch (this.SelectionMode)
        {
          case SelectionMode.None:
            newItem.Active = false;
            break;
          case SelectionMode.MultiSimple:
            this.ActiveItem = newItem;
            break;
          default:
            this.OnSelectedItemAdded(newItem);
            break;
        }
      }
    }

    protected virtual void HandleItemsAdded(NotifyCollectionChangedEventArgs args)
    {
      foreach (RadListDataItem newItem in (IEnumerable) args.NewItems)
      {
        if (!newItem.Enabled)
          break;
        if (newItem.Selected)
          this.OnSelectedItemAdded(newItem);
        else if (newItem.Active)
          this.OnActiveItemAdded(newItem);
      }
    }

    internal void UpdateSelectedIndexOnItemsChanged()
    {
      int index = this.GetIndex(this.ActiveItem);
      if (index == this.oldSelectedIndex)
        return;
      this.SuspendSelectionEvents = true;
      this.SelectedItem = this.ActiveItem;
      this.SuspendSelectionEvents = false;
      this.OnSelectedIndexChanged(index);
    }

    protected virtual void HandleItemsRemoved(NotifyCollectionChangedEventArgs args)
    {
      if (args.Action != NotifyCollectionChangedAction.Remove)
        return;
      foreach (RadListDataItem newItem in (IEnumerable) args.NewItems)
      {
        if (newItem == this.activeListItem)
          this.UpdateActiveItem(newItem, false);
        if (newItem.Selected)
        {
          newItem.Owner = (RadListElement) null;
          this.selectedItems.Remove(newItem);
          int newIndex = -1;
          if (this.selectedItems.Count == 0 && (this.selectionMode == SelectionMode.One || this.selectionMode == SelectionMode.MultiExtended))
            newIndex = this.oldSelectedIndex;
          if (this.IsIndexValid(newIndex))
          {
            this.ProcessSelection(newIndex, false, InputType.Mouse, 0);
            break;
          }
        }
      }
    }

    private void dataLayer_CurrentPositionChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      if (e.Position == this.oldSelectedIndex)
        return;
      if (this.bindingContextPosition != -1 && this.bindingContextDataSource == this.DataSource && this.DataSource != null)
      {
        this.UnwireCurrentPosition();
        this.dataLayer.CurrentPosition = this.bindingContextPosition;
        this.WireCurrentPosition();
      }
      this.bindingContextPosition = -1;
      this.bindingContextDataSource = (object) null;
      this.ProcessSelection(e.Position, false, InputType.Code, 0);
    }

    private void SearchTimer_Tick(object sender, EventArgs e)
    {
      this.searchTimer.Stop();
    }

    protected internal virtual void OnSelectedIndexChanged(int newIndex)
    {
      this.searchStartIndex = newIndex;
      if (this.SuspendSelectionEvents)
        return;
      if (this.SelectedIndexChanged != null)
        this.SelectedIndexChanged((object) this, new Telerik.WinControls.UI.Data.PositionChangedEventArgs(newIndex));
      this.newValue = this.SelectedValue;
      this.oldSelectedIndex = this.SelectedIndex;
      this.OnNotifyPropertyChanged("SelectedValue");
      this.OnSelectedValueChanged(this.SelectedIndex);
    }

    protected virtual bool OnSelectedIndexChanging(int newIndex)
    {
      if (this.SuspendSelectionEvents)
        return false;
      this.oldValue = this.GetValueByIndex(this.oldSelectedIndex);
      if (newIndex == this.oldSelectedIndex || this.SelectedIndexChanging == null)
        return false;
      PositionChangingCancelEventArgs e = new PositionChangingCancelEventArgs(newIndex);
      this.SelectedIndexChanging((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnSelectedValueChanged(int newIndex)
    {
      if (this.SuspendSelectionEvents || !this.HasSelectedValueChanged || this.SelectedValueChanged == null)
        return;
      this.SelectedValueChanged((object) this, (EventArgs) new Telerik.WinControls.UI.Data.ValueChangedEventArgs(newIndex, this.newValue, this.oldValue));
    }

    protected internal virtual RadListDataItem OnListItemDataBinding()
    {
      if (this.ItemDataBinding == null)
        return (RadListDataItem) null;
      ListItemDataBindingEventArgs args = new ListItemDataBindingEventArgs();
      this.ItemDataBinding((object) this, args);
      return args.NewItem;
    }

    protected internal virtual void OnListItemDataBound(RadListDataItem newItem)
    {
      if (this.ItemDataBound == null)
        return;
      this.ItemDataBound((object) this, new ListItemDataBoundEventArgs(newItem));
    }

    protected internal virtual RadListVisualItem OnCreatingVisualListItem(
      RadListVisualItem item)
    {
      if (this.CreatingVisualItem == null)
        return (RadListVisualItem) null;
      CreatingVisualListItemEventArgs args = new CreatingVisualListItemEventArgs();
      args.VisualItem = item;
      this.CreatingVisualItem((object) this, args);
      return args.VisualItem;
    }

    protected virtual void OnSortStyleChanged(SortStyle sortStyle)
    {
      if (this.SortStyleChanged == null)
        return;
      this.SortStyleChanged((object) this, new SortStyleChangedEventArgs(sortStyle));
    }

    protected internal virtual void OnVisualItemFormatting(RadListVisualItem item)
    {
      if (this.VisualItemFormatting == null)
        return;
      this.VisualItemFormatting((object) this, new VisualItemFormattingEventArgs(item));
    }

    protected internal virtual void OnMouseWheel(int delta)
    {
      if (this.scheduleUpdateScroller)
      {
        this.scheduleUpdateScroller = false;
        this.Scroller.UpdateScrollRange();
      }
      RadScrollBarElement scrollbar = this.Scroller.Scrollbar;
      int num1 = Math.Max(1, delta / SystemInformation.MouseWheelScrollDelta);
      int num2 = Math.Sign(delta) * num1 * SystemInformation.MouseWheelScrollLines;
      int num3 = scrollbar.Value - num2 * scrollbar.SmallChange;
      int max = scrollbar.Maximum - scrollbar.LargeChange + 1;
      this.ClampValue(scrollbar.Minimum, max, ref num3);
      this.Scroller.Scrollbar.Value = num3;
    }

    protected internal virtual void OnItemsChanged(NotifyCollectionChangedEventArgs args)
    {
      object position = this.Scroller.Traverser.Position;
      this.UpdateItemTraverser();
      this.Scroller.Traverser.Position = position;
      if (this.beginUpdateCount > 0 || this.SuspendItemsChangeEvents)
        return;
      int index = this.GetIndex(this.oldSelectedItem);
      if (this.indexBeforeItemsChange != index && this.IsIndexValid(index))
      {
        this.SuspendSelectionEvents = true;
        this.dataLayer.CurrentPosition = index;
        this.SuspendSelectionEvents = false;
        this.OnSelectedIndexChanged(index);
      }
      this.scheduleUpdateScroller = true;
      if (this.ItemsChanged == null)
        return;
      this.ItemsChanged((object) this, args);
    }

    protected internal virtual void OnItemsChanging(NotifyCollectionChangingEventArgs args)
    {
      if (this.beginUpdateCount > 0 || this.SuspendItemsChangeEvents)
        return;
      this.oldSelectedItem = this.SelectedItem;
      this.indexBeforeItemsChange = this.GetIndex(this.SelectedItem);
      if (this.ItemsChanging == null)
        return;
      this.ItemsChanging((object) this, args);
    }

    private void DataItemSelectedPropertyChanged(RadListDataItem item, bool newVal)
    {
      if (this.selectedItems.Clearing)
        return;
      switch (this.SelectionMode)
      {
        case SelectionMode.None:
          if (!newVal)
            break;
          throw new InvalidOperationException("An item can not be selected when SelectionMode is None.");
        case SelectionMode.One:
          if (item == this.SelectedItem || !newVal)
            break;
          this.HandleSelectOne(this.GetIndex(item));
          break;
        default:
          if (newVal)
          {
            if (this.selectedItems.Contains(item))
              break;
            this.selectedItems.Add(item);
            break;
          }
          this.MultiSimpleRemove(this.GetIndex(item), true, item);
          break;
      }
    }

    protected internal virtual void OnDataItemPropertyChanged(
      object sender,
      RadPropertyChangedEventArgs args)
    {
      if (args.Property == RadListDataItem.SelectedProperty)
        this.DataItemSelectedPropertyChanged((RadListDataItem) sender, (bool) args.NewValue);
      if (args.Property == RadListDataItem.ActiveProperty && (bool) args.NewValue)
        this.UpdateActiveItem((RadListDataItem) sender, (bool) args.NewValue);
      if (this.DataItemPropertyChanged == null)
        return;
      this.DataItemPropertyChanged(sender, args);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadObject.BindingContextProperty)
      {
        this.bindingContextDataSource = this.DataSource;
        this.bindingContextPosition = this.dataLayer.CurrentPosition;
        this.bindingContextChanged = true;
        this.dataLayer.BindingContext = (BindingContext) e.NewValue;
        this.bindingContextChanged = false;
      }
      else if (e.Property == RadListElement.CaseSensitiveSortProperty)
      {
        if (this.SortStyle == SortStyle.None)
          return;
        this.dataLayer.Refresh();
      }
      else if (e.Property == RadListElement.ItemHeightProperty)
      {
        this.Scroller.ItemHeight = (int) e.NewValue;
        this.ViewElement.ElementProvider.DefaultElementSize = new SizeF(0.0f, (float) (int) e.NewValue);
        this.Scroller.UpdateScrollRange();
        this.ViewElement.UpdateItems();
      }
      else
      {
        if (e.Property != RadListElement.EnableAlternatingItemColorProperty && e.Property != RadListElement.AlternatingItemColorProperty)
          return;
        this.SynchronizeVisualItems();
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.dataLayer.BindingContext = this.ElementTree.Control.BindingContext;
      this.ViewElement.UpdateItems();
      this.ViewElement.InvalidateMeasure();
      this.ViewElement.InvalidateArrange();
      this.Scroller.UpdateScrollRange();
      this.HScrollBar.Maximum = this.Scroller.MaxItemWidth;
    }

    protected override void DisposeManagedResources()
    {
      this.searchTimer.Stop();
      this.searchTimer.Tick -= new EventHandler(this.SearchTimer_Tick);
      this.searchTimer.Dispose();
      this.DisposeItems();
      this.dataLayer.Dispose();
      base.DisposeManagedResources();
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (sender is RadScrollBarElement || sender is ScrollBarThumb)
      {
        if (!this.scheduleUpdateScroller)
          return;
        this.scheduleUpdateScroller = false;
        this.Scroller.UpdateScrollRange();
      }
      else
      {
        this.HandleKeyboard((object) sender, args);
        this.HandleMouse((object) sender, args);
      }
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      if (!this.AutoSizeItems)
        return;
      this.ViewElement.UpdateItems();
      this.ViewElement.InvalidateMeasure();
      this.ViewElement.InvalidateArrange();
      this.Scroller.UpdateScrollRange();
      this.HScrollBar.Maximum = this.Scroller.MaxItemWidth;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      if (this.VScrollBar.ControlBoundingRectangle.Contains(args.Location) || this.HScrollBar.ControlBoundingRectangle.Contains(args.Location))
        return;
      int num1 = this.VScrollBar.Value - args.Offset.Height;
      if (num1 > this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1)
        num1 = this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1;
      if (num1 < this.VScrollBar.Minimum)
        num1 = this.VScrollBar.Minimum;
      this.VScrollBar.Value = num1;
      int num2 = this.HScrollBar.Value - args.Offset.Width;
      if (num2 > this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1)
        num2 = this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1;
      if (num2 < this.HScrollBar.Minimum)
        num2 = this.HScrollBar.Minimum;
      this.HScrollBar.Value = num2;
      args.Handled = true;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.Scroller.UpdateScrollRange();
    }

    public virtual bool OnControlMouseUp(MouseEventArgs e)
    {
      if (!this.EnableKineticScrolling)
        return false;
      bool isRunning = this.scrollBehavior.IsRunning;
      this.scrollBehavior.MouseUp(e.Location);
      return isRunning;
    }

    public virtual bool OnControlMouseDown(MouseEventArgs e)
    {
      if (!this.EnableKineticScrolling)
        return false;
      this.scrollBehavior.MouseDown(e.Location);
      return false;
    }

    public virtual bool OnControlMouseMove(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && (this.ControlBoundingRectangle.Contains(e.Location) && RadDragDropService.ShouldBeginDrag(e.Location, this.dragStart) && this.AllowDragDrop))
      {
        RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location);
        RadListVisualItem radListVisualItem = this.ElementTree.GetElementAtPoint(e.Location) as RadListVisualItem ?? elementAtPoint.FindAncestor<RadListVisualItem>();
        if (radListVisualItem != null)
        {
          radListVisualItem.Data.Selected = radListVisualItem.Data.Active = true;
          this.dragDropService.Start((object) radListVisualItem);
        }
        if (this.dragDropService.State == RadServiceState.Working)
          return false;
      }
      if (!this.EnableKineticScrolling)
        return false;
      this.scrollBehavior.MouseMove(e.Location);
      return false;
    }

    private void HandleMouse(object sender, RoutedEventArgs args)
    {
      RoutedEvent routedEvent = args.RoutedEvent;
      if (routedEvent != RadElement.MouseUpEvent && routedEvent != RadElement.MouseDownEvent && routedEvent != RadElement.MouseClickedEvent)
        return;
      MouseEventArgs originalEventArgs = args.OriginalEventArgs as MouseEventArgs;
      if (originalEventArgs == null || originalEventArgs.Button != MouseButtons.Left || this.ElementTree == null)
        return;
      RadListVisualItem parentListVisualItem = this.FindParentListVisualItem(this.ElementTree.GetElementAtPoint(originalEventArgs.Location));
      if (parentListVisualItem == null)
        return;
      this.HandleMouseCore(routedEvent, parentListVisualItem);
    }

    protected RadListVisualItem FindParentListVisualItem(RadElement child)
    {
      for (; child != null; child = child.Parent)
      {
        RadListVisualItem radListVisualItem = child as RadListVisualItem;
        if (radListVisualItem != null)
          return radListVisualItem;
      }
      return (RadListVisualItem) null;
    }

    private void HandleMouseCore(RoutedEvent routed, RadListVisualItem clickedListVisualItem)
    {
      MouseNotification reason = MouseNotification.Click;
      if (routed == RadElement.MouseUpEvent)
        reason = MouseNotification.MouseUp;
      this.ProcessMouseSelection(clickedListVisualItem.Data, reason);
    }

    private void ProcessMouseSelection(RadListDataItem item, MouseNotification reason)
    {
      if (reason != MouseNotification.MouseUp)
        return;
      this.ProcessSelection(this.GetIndex(item), false, InputType.Mouse, 0);
    }

    private void HandleKeyboard(object sender, RoutedEventArgs args)
    {
      if (args.RoutedEvent == RadItem.KeyDownEvent)
      {
        KeyEventArgs originalEventArgs = (KeyEventArgs) args.OriginalEventArgs;
        originalEventArgs.Handled = this.ProcessKeyboardSelection(originalEventArgs.KeyCode);
      }
      if (args.RoutedEvent != RadItem.KeyPressEvent)
        return;
      KeyPressEventArgs originalEventArgs1 = (KeyPressEventArgs) args.OriginalEventArgs;
      this.ProcessKeyboardSearch(originalEventArgs1.KeyChar);
      originalEventArgs1.Handled = true;
    }

    protected internal virtual void ProcessKeyboardSearch(char character)
    {
      if (!this.KeyboardSearchEnabled)
        return;
      if (this.searchTimer.Enabled)
      {
        this.searchTimer.Stop();
        this.searchTimer.Start();
      }
      else
      {
        this.searchBuffer = new StringBuilder();
        this.searchTimer.Start();
      }
      this.searchBuffer.Append(character);
      string str = RadListElement.IsSameLetter(this.searchBuffer) ? this.searchBuffer[0].ToString() : this.searchBuffer.ToString();
      if (str.Length > 1 && this.IsIndexValid(this.searchStartIndex) && this.FindStringComparer.Compare(this.GetItemText(this.Items[this.searchStartIndex]), str))
        return;
      if (this.searchStartIndex == -1)
      {
        int startIndex = this.SelectedIndex > -1 ? this.SelectedIndex + 1 : 0;
        this.searchStartIndex = this.FindString(str, startIndex);
      }
      else
        this.searchStartIndex = this.FindString(str, this.searchStartIndex + 1);
      if (this.IsIndexValid(this.searchStartIndex))
      {
        if (this.SelectionMode == SelectionMode.MultiSimple)
          this.ActiveItem = this.Items[this.searchStartIndex];
        else if (this.SelectionMode != SelectionMode.None)
          this.HandleSelectOne(this.searchStartIndex);
        this.ScrollToActiveItem();
      }
      else
        this.searchStartIndex = -1;
    }

    private static bool IsSameLetter(StringBuilder sb)
    {
      for (int index = 1; index < sb.Length; ++index)
      {
        if ((int) sb[index] != (int) sb[index - 1])
          return false;
      }
      return true;
    }

    private void OnSpace()
    {
      if (this.selectionMode == SelectionMode.MultiSimple || this.selectionMode == SelectionMode.MultiExtended && Control.ModifierKeys == Keys.Control)
      {
        if (this.activeListItem == null)
          return;
        this.ProcessSelection(this.GetIndex(this.activeListItem), false, InputType.Mouse, 0);
      }
      else if (this.selectionMode == SelectionMode.One)
      {
        if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
          this.ProcessKeyboardSelection(Keys.Up);
        else
          this.ProcessKeyboardSelection(Keys.Down);
      }
      else
      {
        if (this.selectionMode != SelectionMode.MultiExtended)
          return;
        this.ProcessSelection(this.GetIndex(this.activeListItem), false, InputType.Mouse, 0);
      }
    }

    internal virtual bool ProcessKeyboardSelection(Keys keyCode)
    {
      if (keyCode == Keys.Space)
      {
        this.OnSpace();
        return true;
      }
      int selectionDirection = this.GetSelectionDirection(keyCode);
      if (selectionDirection == 0)
        return false;
      int newIndex = this.GetIndex(this.activeListItem) + selectionDirection;
      if (!this.IsIndexValid(newIndex))
        return false;
      if (Control.ModifierKeys == Keys.Control)
      {
        this.ActiveItem = this.Items[this.GetAvaibleIndex(newIndex, InputType.Keyboard, selectionDirection)];
        return true;
      }
      this.ProcessSelection(newIndex, false, InputType.Keyboard, selectionDirection);
      return true;
    }

    private int GetSelectionDirection(Keys keyCode)
    {
      switch (keyCode)
      {
        case Keys.Left:
        case Keys.Up:
          return -1;
        case Keys.Right:
        case Keys.Down:
          return 1;
        default:
          return 0;
      }
    }

    internal void HomeEndSelect(RadListDataItem item)
    {
      if (this.SelectionMode == SelectionMode.MultiExtended)
      {
        if (Control.ModifierKeys == Keys.Shift)
        {
          int selectedIndex = this.SelectedIndex;
          if (!this.IsIndexValid(selectedIndex))
            return;
          this.SuspendSelectionEvents = true;
          if (this.selectedItems.Count > 0)
            this.selectedItems.Clear();
          this.SelectRange(selectedIndex, item.RowIndex);
          this.shiftSelectStartIndex = selectedIndex;
          this.SuspendSelectionEvents = false;
        }
        else if (item != null)
          this.HandleSelectOne(item.RowIndex);
      }
      if (this.SelectionMode == SelectionMode.One && item != null)
        this.HandleSelectOne(item.RowIndex);
      this.ScrollToItem(item);
    }

    private void ClearSelection()
    {
      if (this.OnSelectedIndexChanging(-1))
        return;
      if (this.selectedItems.Count > 0)
        this.selectedItems.Clear();
      this.UpdateActiveItem(this.activeListItem, false);
      this.activeListItem = (RadListDataItem) null;
      if (this.IsOldSelectedIndexInInitialState || this.oldSelectedIndex == -1)
        return;
      this.OnSelectedIndexChanged(-1);
    }

    private int GetAvaibleIndex(int newIndex, InputType inputType, int dir)
    {
      if (dir == 0 || inputType != InputType.Keyboard)
        return newIndex;
      for (; newIndex >= 0 && newIndex < this.Items.Count; newIndex += dir)
      {
        if (this.Items[newIndex].Enabled)
          return newIndex;
      }
      return -1;
    }

    private void ProcessSelection(int newIndex, bool onMouseDrag, InputType inputType, int dir)
    {
      if (this.ReadOnly && inputType != InputType.Code)
        return;
      newIndex = this.GetAvaibleIndex(newIndex, inputType, dir);
      if (!this.IsIndexValid(newIndex))
      {
        this.ClearSelection();
      }
      else
      {
        this.UnwireCurrentPosition();
        bool flag = true;
        if (this.ElementTree != null && this.ElementTree.Control is RadControl)
          flag = !((RadControl) this.ElementTree.Control).IsInitializing;
        if (flag)
        {
          switch (this.selectionMode)
          {
            case SelectionMode.One:
              this.HandleSelectOne(newIndex);
              break;
            case SelectionMode.MultiSimple:
              this.HandleMultiSimple(newIndex, inputType);
              break;
            case SelectionMode.MultiExtended:
              this.HandleMultiExtended(newIndex, onMouseDrag, inputType, Control.ModifierKeys == Keys.Shift, Control.ModifierKeys == Keys.Control);
              break;
          }
        }
        this.WireCurrentPosition();
        if (!this.handleSelectionOneInProgress)
          this.ScrollToActiveItem();
        if (this.selectionMode == SelectionMode.None)
          return;
        this.viewElement.ForceVisualStateUpdate();
      }
    }

    private void HandleMultiExtended(
      int newIndex,
      bool onMouseDrag,
      InputType inputType,
      bool shiftKeyPressed,
      bool controlKeyPressed)
    {
      if (shiftKeyPressed || onMouseDrag)
      {
        int selectStartIndex = this.shiftSelectStartIndex;
        if (!this.IsIndexValid(selectStartIndex))
          return;
        this.HandleMultiSelectRange(selectStartIndex, newIndex);
      }
      else if (controlKeyPressed)
        this.HandleMultiSimple(newIndex, inputType);
      else
        this.HandleSelectOne(newIndex);
    }

    private void HandleMultiSimple(int newIndex, InputType inputType)
    {
      this.shiftSelectStartIndex = newIndex;
      switch (inputType)
      {
        case InputType.Mouse:
          this.HandleMouseMultiSimple(newIndex, true);
          break;
        case InputType.Keyboard:
          if (this.selectedItems.Count > 0)
            this.selectedItems.Clear();
          this.HandleMouseMultiSimple(newIndex, true);
          break;
        case InputType.Code:
          this.HandleCodeMultiSimple(newIndex);
          break;
      }
      this.UpdateActiveItem(this.dataLayer.GetItemAtIndex(newIndex), true);
    }

    private void HandleCodeMultiSimple(int newIndex)
    {
      this.HandleMouseMultiSimple(newIndex, false);
    }

    private void HandleMouseMultiSimple(int newIndex, bool changeCurrentPosition)
    {
      RadListDataItem itemAtIndex = this.dataLayer.GetItemAtIndex(newIndex);
      if (itemAtIndex.Selected)
      {
        this.MultiSimpleRemove(newIndex, changeCurrentPosition, itemAtIndex);
      }
      else
      {
        if (!itemAtIndex.Enabled)
          return;
        this.MultiSimpleAdd(newIndex, changeCurrentPosition, itemAtIndex);
      }
    }

    private void MultiSimpleAdd(
      int newIndex,
      bool changeCurrentPosition,
      RadListDataItem itemToAdd)
    {
      if (this.OnSelectedIndexChanging(newIndex))
        return;
      this.selectedItems.Add(itemToAdd);
      if (changeCurrentPosition)
      {
        this.isRemoving = true;
        this.dataLayer.CurrentPosition = newIndex;
        this.isRemoving = false;
      }
      this.OnSelectedIndexChanged(this.SelectedIndex);
    }

    private void MultiSimpleRemove(
      int newIndex,
      bool changeCurrentPosition,
      RadListDataItem itemToRemove)
    {
      if (this.isRemoving)
        return;
      this.isRemoving = true;
      if (newIndex == this.SelectedIndex)
      {
        int num = this.selectedItems.IndexOf(itemToRemove);
        int index1 = num - 1;
        RadListDataItem radListDataItem = (RadListDataItem) null;
        if (index1 > -1)
        {
          radListDataItem = this.selectedItems[index1];
        }
        else
        {
          int index2 = num + 1;
          if (index2 < this.selectedItems.Count)
            radListDataItem = this.selectedItems[index2];
        }
        int index3 = this.GetIndex(radListDataItem);
        if (this.OnSelectedIndexChanging(index3))
        {
          this.isRemoving = false;
          return;
        }
        this.selectedItems.Remove(itemToRemove);
        if (changeCurrentPosition && index3 > -1)
          this.dataLayer.CurrentPosition = index3;
        this.OnSelectedIndexChanged(index3);
      }
      else
        this.selectedItems.Remove(itemToRemove);
      this.isRemoving = false;
    }

    private void HandleSelectOne(int newIndex)
    {
      if (this.handleSelectionOneInProgress)
        return;
      this.shiftSelectStartIndex = newIndex;
      if (this.OnSelectedIndexChanging(newIndex))
        return;
      this.handleSelectionOneInProgress = true;
      bool flag = false;
      if (newIndex != this.oldSelectedIndex)
        flag = true;
      if (this.selectedItems.Count > 0)
        this.selectedItems.Clear();
      RadListDataItem itemAtIndex = this.dataLayer.GetItemAtIndex(newIndex);
      this.selectedItems.Add(itemAtIndex);
      this.SelectedIndex = newIndex;
      this.UpdateActiveItem(itemAtIndex, true);
      this.handleSelectionOneInProgress = false;
      if (!flag)
        return;
      this.OnSelectedIndexChanged(newIndex);
    }

    private void HandleMultiSelectRange(int startIndex, int endIndex)
    {
      bool flag = false;
      if (startIndex > endIndex)
      {
        this.SwapIntegers(ref startIndex, ref endIndex);
        flag = true;
      }
      for (int index1 = this.selectedItems.Count - 1; index1 > -1; --index1)
      {
        int index2 = this.GetIndex(this.selectedItems[index1]);
        if (index2 < startIndex || index2 > endIndex)
        {
          this.selectedItems.RemoveAt(index1);
          int index3 = index1 - 1;
          if (index3 > -1 && index3 < this.selectedItems.Count)
            this.UpdateActiveItem(this.selectedItems[index3], true);
        }
      }
      List<RadListDataItem> itemRange = this.dataLayer.GetItemRange(startIndex, endIndex);
      if (flag)
      {
        for (int index = itemRange.Count - 1; index >= 0; --index)
        {
          RadListDataItem radListDataItem = itemRange[index];
          if (!radListDataItem.Selected)
          {
            this.selectedItems.Add(radListDataItem);
            this.UpdateActiveItem(radListDataItem, true);
          }
        }
      }
      else
      {
        for (int index = 0; index < itemRange.Count; ++index)
        {
          RadListDataItem radListDataItem = itemRange[index];
          if (!radListDataItem.Selected)
          {
            this.selectedItems.Add(radListDataItem);
            this.UpdateActiveItem(radListDataItem, true);
          }
        }
      }
    }

    protected virtual void UpdateActiveItem(RadListDataItem item, bool active)
    {
      if (this.activeListItem == item && active)
        return;
      if (this.activeListItem != null)
      {
        int num1 = (int) this.activeListItem.SetValue(RadListDataItem.ActiveProperty, (object) false);
      }
      this.activeListItem = !active ? (RadListDataItem) null : item;
      if (item != null)
      {
        int num2 = (int) item.SetValue(RadListDataItem.ActiveProperty, (object) active);
      }
      this.OnNotifyPropertyChanged("ActiveItem");
    }

    protected virtual void SetSelectedValue(object value)
    {
      if (value == this.SelectedValue)
        return;
      if (value == null || Convert.IsDBNull(value))
      {
        this.ProcessSelection(-1, false, InputType.Code, 0);
        this.OnNotifyPropertyChanged("SelectedValue");
      }
      else
      {
        foreach (RadListDataItem radListDataItem in this.dataLayer.ListSource)
        {
          object obj = radListDataItem.Value;
          if (obj != null && obj.GetType().IsEnum)
            value = Enum.Parse(obj.GetType(), Convert.ToString(value));
          if (obj == value || obj != null && obj.Equals(value))
          {
            this.ProcessSelection(this.GetIndex(radListDataItem), false, InputType.Code, 0);
            this.OnNotifyPropertyChanged("SelectedValue");
            return;
          }
          if (obj == null && radListDataItem.Text != null && (value is string && radListDataItem.Text == value.ToString()))
          {
            this.ProcessSelection(this.GetIndex(radListDataItem), false, InputType.Code, 0);
            this.OnNotifyPropertyChanged("SelectedValue");
            return;
          }
        }
        this.SelectedIndex = -1;
      }
    }

    protected void SetSelectedItem(RadListDataItem value)
    {
      if (value != null && value.Owner != this)
        value.Owner = this;
      if (this.SelectedItem == value)
        return;
      this.SetSelectedItemCore(value);
      this.OnNotifyPropertyChanged("SelectedItem");
    }

    protected virtual void SetSelectedItemCore(RadListDataItem item)
    {
      if (item == null)
      {
        this.ClearSelection();
      }
      else
      {
        switch (this.SelectionMode)
        {
          case SelectionMode.One:
          case SelectionMode.MultiExtended:
            this.ProcessSelection(this.GetIndex(item), false, InputType.Mouse, 0);
            break;
          case SelectionMode.MultiSimple:
            int index = this.GetIndex(item);
            if (this.OnSelectedIndexChanging(index))
              break;
            this.UnwireCurrentPosition();
            this.dataLayer.CurrentItem = item;
            if (!this.selectedItems.Contains(item))
              this.selectedItems.Add(item);
            this.WireCurrentPosition();
            this.OnSelectedIndexChanged(index);
            break;
        }
      }
    }

    protected virtual void SetSelectedIndex(int value)
    {
      if (value == this.SelectedIndex)
        return;
      if (value == -1)
      {
        if (this.OnSelectedIndexChanging(value))
          return;
        if (this.selectedItems.Count > 0)
          this.selectedItems.Clear();
        this.OnSelectedIndexChanged(value);
        this.UpdateActiveItem(this.activeListItem, false);
        this.dataLayer.DataView.MoveCurrentToPosition(value);
        this.OnNotifyPropertyChanged("SelectedIndex");
      }
      else
      {
        if (this.dataLayer.CurrentPosition == value)
          this.ProcessSelection(value, false, InputType.Code, 0);
        else
          this.dataLayer.DataView.MoveCurrentToPosition(value);
        this.OnNotifyPropertyChanged("SelectedIndex");
      }
    }

    private void CheckReadyForBinding()
    {
      if (this.Items.Count == 0)
        return;
      object dataSource = this.DataSource;
    }

    internal void CheckReadyForUnboundMode()
    {
      object dataSource = this.DataSource;
    }

    private object GetValueByIndex(int index)
    {
      if (this.IsIndexValid(index))
        return this.dataLayer.DataView[index].Value;
      return (object) null;
    }

    protected int GetIndex(RadListDataItem item)
    {
      if (item == null)
        return -1;
      return item.RowIndex;
    }

    internal string GetItemText(RadListDataItem dataItem)
    {
      if (this.itemTextComparisonMode == ItemTextComparisonMode.DataText)
        return dataItem.CachedText;
      return dataItem.Text;
    }

    protected bool IsIndexValid(int value)
    {
      if (value >= 0)
        return value < this.dataLayer.GetVisibleItemsCount();
      return false;
    }

    private void SwapIntegers(ref int a, ref int b)
    {
      int num = a;
      a = b;
      b = num;
    }

    private void DisposeItems()
    {
      foreach (DisposableObject disposableObject in (IEnumerable<RadListDataItem>) this.Items)
        disposableObject.Dispose();
    }

    private SortStyle GetSortStyle(ListSortDirection direction)
    {
      switch (direction)
      {
        case ListSortDirection.Ascending:
          return SortStyle.Ascending;
        case ListSortDirection.Descending:
          return SortStyle.Descending;
        default:
          return SortStyle.None;
      }
    }

    private void SetSortComparer(IComparer<RadListDataItem> comparer, ListSortDirection direction)
    {
      SortStyle sortStyle = this.SortStyle;
      this.SuspendSelectionEvents = true;
      RadListDataItem selectedItem = this.SelectedItem;
      int index1 = this.GetIndex(selectedItem);
      this.sortDescriptors.Clear();
      if (comparer != null)
      {
        this.dataLayer.DataView.Comparer = comparer;
        this.sortDescriptors.Add(this.GetSortPropertyName(), direction);
      }
      if (this.SelectedItem != selectedItem)
        this.SelectedItem = selectedItem;
      this.SuspendSelectionEvents = false;
      int index2 = this.GetIndex(selectedItem);
      if (index2 != index1)
        this.OnSelectedIndexChanged(index2);
      if (sortStyle == this.SortStyle)
        return;
      this.OnSortStyleChanged(this.SortStyle);
      this.OnNotifyPropertyChanged("SortStyle");
    }

    private void SetSortStyle(SortStyle value)
    {
      if (value == this.SortStyle)
        return;
      ListSortDirection direction = ListSortDirection.Ascending;
      IComparer<RadListDataItem> comparer = (IComparer<RadListDataItem>) new RadListElement.ListItemAscendingComparer();
      switch (value)
      {
        case SortStyle.Descending:
          direction = ListSortDirection.Descending;
          comparer = (IComparer<RadListDataItem>) new RadListElement.ListItemDescendingComparer();
          break;
        case SortStyle.None:
          comparer = (IComparer<RadListDataItem>) null;
          break;
      }
      this.SetSortComparer(comparer, direction);
    }

    private void SetSelectionMode(SelectionMode mode)
    {
      if (mode == this.selectionMode)
        return;
      if (mode == SelectionMode.None)
        this.selectedIndexBeforeSelectionMode = this.SelectedIndex;
      if (mode != SelectionMode.None && this.selectedIndexBeforeSelectionMode > -1)
      {
        this.SelectedIndex = this.selectedIndexBeforeSelectionMode;
        this.selectedIndexBeforeSelectionMode = -1;
      }
      this.selectionMode = mode;
      int currentPosition = this.dataLayer.CurrentPosition;
      if (!this.IsIndexValid(currentPosition))
        return;
      RadListDataItem radListDataItem = this.Items[currentPosition];
      switch (this.selectionMode)
      {
        case SelectionMode.None:
          this.SetSelectedIndex(-1);
          break;
        case SelectionMode.One:
          if (currentPosition > -1)
          {
            this.ProcessSelection(currentPosition, false, InputType.Code, 0);
            break;
          }
          break;
        case SelectionMode.MultiSimple:
        case SelectionMode.MultiExtended:
          if (currentPosition > -1 && !radListDataItem.Selected)
          {
            this.ProcessSelection(currentPosition, false, InputType.Code, 0);
            break;
          }
          break;
      }
      this.OnNotifyPropertyChanged("SelectionMode");
    }

    private string GetSortPropertyName()
    {
      if (this.DataSource == null && this.DisplayMember == "")
        return "Text";
      return this.DisplayMember;
    }

    private void ClampValue(int min, int max, ref int value)
    {
      if (value > max)
        value = max;
      if (value >= min)
        return;
      value = min;
    }

    private void WireCurrentPosition()
    {
      ++this.subscriptionCounter;
      this.dataLayer.CurrentPositionChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.dataLayer_CurrentPositionChanged);
    }

    private void UnwireCurrentPosition()
    {
      --this.subscriptionCounter;
      this.dataLayer.CurrentPositionChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.dataLayer_CurrentPositionChanged);
    }

    internal void InvokeMultiExtendedSelection(
      int index,
      InputType input,
      bool shift,
      bool control)
    {
      this.UnwireCurrentPosition();
      this.HandleMultiExtended(index, false, input, shift, control);
      this.WireCurrentPosition();
    }

    internal RadListDataItem GetTopVisibleItem()
    {
      return ((RadListVisualItem) this.viewElement.Children[0]).Data;
    }

    private class DeferHelper : IDisposable
    {
      private RadListElement listElement;

      public DeferHelper(RadListElement listElement)
      {
        this.listElement = listElement;
      }

      public void Dispose()
      {
        if (this.listElement == null)
          return;
        this.listElement.EndUpdate();
        this.listElement = (RadListElement) null;
      }
    }

    private class ListItemAscendingComparer : IComparer<RadListDataItem>
    {
      public virtual int Compare(RadListDataItem x, RadListDataItem y)
      {
        bool ignoreCase = false;
        if (x.Owner != null)
          ignoreCase = !x.Owner.CaseSensitiveSort;
        return string.Compare(x.CachedText, y.CachedText, ignoreCase);
      }
    }

    private class ListItemDescendingComparer : IComparer<RadListDataItem>
    {
      public virtual int Compare(RadListDataItem x, RadListDataItem y)
      {
        bool ignoreCase = false;
        if (x.Owner != null)
          ignoreCase = !x.Owner.CaseSensitiveSort;
        return string.Compare(y.CachedText, x.CachedText, ignoreCase);
      }
    }
  }
}
