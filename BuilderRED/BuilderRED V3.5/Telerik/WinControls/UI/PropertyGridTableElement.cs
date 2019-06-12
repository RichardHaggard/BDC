// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridTableElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.PropertyGridData;

namespace Telerik.WinControls.UI
{
  public class PropertyGridTableElement : VirtualizedScrollPanel<PropertyGridItemBase, PropertyGridItemElementBase>, IDataItemSource
  {
    public static RadProperty ItemHeightProperty = RadProperty.Register(nameof (ItemHeight), typeof (int), typeof (PropertyGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 24, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemIndentProperty = RadProperty.Register(nameof (ItemIndent), typeof (int), typeof (PropertyGridTableElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private PropertyGridTableElement.UpdateActions resumeAction = PropertyGridTableElement.UpdateActions.Resume;
    private Dictionary<System.Type, IInputEditor> cachedEditors = new Dictionary<System.Type, IInputEditor>();
    private bool autoExpandGroups = true;
    private string lastSearchCriteria = "";
    private int updateSuspendedCount;
    private RadListSource<PropertyGridItem> listSource;
    private FilterDescriptorCollection filterDescriptors;
    private int minimumColumnWidth;
    private PropertyGridTraverser traverser;
    private object currentObject;
    private object[] currentObjects;
    private PropertyGridItemBase selectedItem;
    private bool pendingScrollerUpdates;
    private int valueColumnWidth;
    private float ratio;
    private float currentAvailableWidth;
    private bool readOnly;
    private IInputEditor activeEditor;
    private object cachedOldValue;
    private PropertyGridRootItemsCollection items;
    private PropertyGridGroupItemCollection groupItems;
    private PropertySort propertySort;
    private RadPropertyGridBeginEditModes beginEditMode;
    private Timer clickTimer;
    private bool doubleClick;
    private Point mouseDownLocation;
    private RadContextMenu contextMenu;
    private bool wasInEditModeOnMouseDown;
    private bool wasSelectedOnMouseDown;
    private bool selectionWasCanceled;
    private ScrollServiceBehavior scrollBehavior;
    private bool enableKineticScrolling;
    private bool useCachedValues;
    private bool enableCustomGrouping;
    private bool overrideBuiltInEditors;
    private Timer typingTimer;
    private StringBuilder searchBuffer;
    private IFindStringComparer findStringComparer;
    protected internal bool isChanging;
    public CreatePropertyGridItemEventHandler CreateItem;
    public CreatePropertyGridItemElementEventHandler CreateItemElement;
    private int sort;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ratio = 0.5f;
      this.minimumColumnWidth = 30;
      this.pendingScrollerUpdates = true;
      this.NotifyParentOnMouseInput = true;
      this.propertySort = PropertySort.NoSort;
      this.typingTimer = new Timer();
      this.typingTimer.Interval = 300;
      this.typingTimer.Tick += new EventHandler(this.typingTimer_Tick);
      this.FindStringComparer = (IFindStringComparer) new StartsWithFindStringComparer();
    }

    protected override void CreateChildElements()
    {
      this.items = new PropertyGridRootItemsCollection(this);
      this.groupItems = new PropertyGridGroupItemCollection(this);
      this.listSource = new RadListSource<PropertyGridItem>((IDataItemSource) this);
      this.listSource.CollectionView.GroupFactory = (IGroupFactory<PropertyGridItem>) new PropertyGridGroupFactory(this);
      this.filterDescriptors = (FilterDescriptorCollection) new PropertyGridFilterDescriptorCollection();
      this.traverser = new PropertyGridTraverser(this);
      this.clickTimer = new Timer();
      this.scrollBehavior = new ScrollServiceBehavior();
      base.CreateChildElements();
      this.ViewElement.NotifyParentOnMouseInput = true;
      this.Scroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.Scroller.Traverser = (ITraverser<PropertyGridItemBase>) this.traverser;
      this.ItemSpacing = -1;
      this.scrollBehavior.ScrollServices.Add(new ScrollService((RadElement) this.ViewElement, this.VScrollBar));
    }

    protected override IVirtualizedElementProvider<PropertyGridItemBase> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<PropertyGridItemBase>) new PropertyGridItemElementProvider(this);
    }

    protected override void WireEvents()
    {
      base.WireEvents();
      this.listSource.CollectionChanged += new NotifyCollectionChangedEventHandler(this.listSource_CollectionChanged);
      this.listSource.CollectionView.SortDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SortDescriptors_CollectionChanged);
      this.filterDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.FilterDescriptors_CollectionChanged);
      this.listSource.CollectionView.GroupDescriptors.CollectionChanged += new NotifyCollectionChangedEventHandler(this.GroupDescriptors_CollectionChanged);
      this.clickTimer.Tick += new EventHandler(this.clickTimer_Tick);
    }

    protected override void UnwireEvents()
    {
      base.UnwireEvents();
      this.listSource.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.listSource_CollectionChanged);
      this.listSource.CollectionView.SortDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.SortDescriptors_CollectionChanged);
      this.filterDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.FilterDescriptors_CollectionChanged);
      this.listSource.CollectionView.GroupDescriptors.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.GroupDescriptors_CollectionChanged);
      this.clickTimer.Tick -= new EventHandler(this.clickTimer_Tick);
    }

    protected override void DisposeManagedResources()
    {
      foreach (KeyValuePair<System.Type, IInputEditor> cachedEditor in this.cachedEditors)
      {
        IInputEditor inputEditor = cachedEditor.Value;
        (inputEditor as BaseInputEditor)?.EditorElement?.Dispose();
        (inputEditor as IDisposable)?.Dispose();
      }
      this.cachedEditors.Clear();
      this.UnwireEvents();
      this.UnsubscribeINotifyObjects();
      if (this.ContextMenu != null && (object) this.ContextMenu.GetType() == (object) typeof (PropertyGridDefaultContextMenu))
        this.ContextMenu.Dispose();
      if (this.clickTimer != null)
        this.clickTimer.Dispose();
      if (this.typingTimer != null)
      {
        this.typingTimer.Stop();
        this.typingTimer.Tick -= new EventHandler(this.typingTimer_Tick);
        this.typingTimer.Dispose();
        this.typingTimer = (Timer) null;
      }
      this.listSource.Dispose();
      base.DisposeManagedResources();
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the data can be grouped programmatically.")]
    public bool EnableCustomGrouping
    {
      get
      {
        return this.enableCustomGrouping;
      }
      set
      {
        if (this.enableCustomGrouping == value)
          return;
        this.enableCustomGrouping = value;
        this.CollectionView.GroupPredicate = !value ? this.CollectionView.DefaultGroupPredicate : new GroupPredicate<PropertyGridItem>(this.PerformGrouping);
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (EnableCustomGrouping)));
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the values of the items should be invalidated the next time a grouping and/or sorting is performed.")]
    public virtual bool UseCachedValues
    {
      get
      {
        return this.useCachedValues;
      }
      set
      {
        this.useCachedValues = value;
      }
    }

    public ScrollServiceBehavior ScrollBehavior
    {
      get
      {
        return this.scrollBehavior;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
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
        if (this.enableKineticScrolling == value)
          return;
        this.enableKineticScrolling = value;
        if (value)
          return;
        this.ScrollBehavior.Stop();
      }
    }

    public PropertyGridElement PropertyGridElement
    {
      get
      {
        return this.FindAncestor<PropertyGridElement>();
      }
    }

    public IValueEditor ActiveEditor
    {
      get
      {
        return (IValueEditor) this.activeEditor;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the mode in which the properties will be displayed in the PropertyGridTableElement.")]
    [DefaultValue(PropertySort.NoSort)]
    [Browsable(true)]
    public PropertySort PropertySort
    {
      get
      {
        return this.propertySort;
      }
      set
      {
        if (this.propertySort == value)
          return;
        this.propertySort = value;
        this.PerformPropertySort(value);
        this.OnNotifyPropertyChanged(nameof (PropertySort));
      }
    }

    [Description("Gets or sets the minimum width columns can have.")]
    [DefaultValue(30)]
    [Category("Appearance")]
    [Browsable(true)]
    public int MinimumColumnWidth
    {
      get
      {
        return this.minimumColumnWidth;
      }
      set
      {
        this.minimumColumnWidth = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [Description("Gets a value indicating whether there are currently open editors.")]
    public bool IsEditing
    {
      get
      {
        return this.activeEditor != null;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the user is allowed to edit the values of the properties.")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        this.readOnly = value;
        this.OnNotifyPropertyChanged(nameof (ReadOnly));
      }
    }

    [Category("Behavior")]
    [Description("Indicates whether editors specified with an EditorAttribute will be used without considering built-in editors.")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool OverrideBuiltInEditors
    {
      get
      {
        return this.overrideBuiltInEditors;
      }
      set
      {
        this.overrideBuiltInEditors = value;
      }
    }

    [Description("Gets or sets the width of the \"column\" that holds the values.")]
    [DefaultValue(-1)]
    [Category("Layout")]
    [Browsable(true)]
    public int ValueColumnWidth
    {
      get
      {
        return this.valueColumnWidth;
      }
      set
      {
        this.SetValueColumnWidth(value, true);
      }
    }

    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the groups will be expanded or collapse upon creation.")]
    [Category("Behavior")]
    public bool AutoExpandGroups
    {
      get
      {
        return this.autoExpandGroups;
      }
      set
      {
        this.autoExpandGroups = value;
        foreach (PropertyGridGroup group in (ReadOnlyCollection<Group<PropertyGridItem>>) this.CollectionView.Groups)
          group.GroupItem.Expanded = value;
        this.OnNotifyPropertyChanged(nameof (AutoExpandGroups));
      }
    }

    [Browsable(false)]
    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.CollectionView.GroupDescriptors;
      }
    }

    [Browsable(false)]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.filterDescriptors;
      }
    }

    [Browsable(false)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.listSource.CollectionView.SortDescriptors;
      }
    }

    [DefaultValue(SortOrder.None)]
    [Browsable(false)]
    public SortOrder SortOrder
    {
      get
      {
        if (this.SortDescriptors.Count > 0)
        {
          switch (this.SortDescriptors[0].Direction)
          {
            case ListSortDirection.Ascending:
              return SortOrder.Ascending;
            case ListSortDirection.Descending:
              return SortOrder.Descending;
          }
        }
        return SortOrder.None;
      }
      set
      {
        if (value == SortOrder.None)
        {
          if (this.SortDescriptors.Count <= 0)
            return;
          this.SortDescriptors.RemoveAt(0);
        }
        else
        {
          ListSortDirection direction = ListSortDirection.Ascending;
          if (value == SortOrder.Descending)
            direction = ListSortDirection.Descending;
          if (this.SortDescriptors.Count == 0)
            this.SortDescriptors.Add("Name", direction);
          else
            this.SortDescriptors[0].Direction = direction;
        }
      }
    }

    [DefaultValue(24)]
    [Description("Gets or sets a value indicating the height of the RadPropertyGrid items.")]
    [Browsable(true)]
    public int ItemHeight
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(PropertyGridTableElement.ItemHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridTableElement.ItemHeightProperty, (object) value);
        this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
      }
    }

    [DefaultValue(20)]
    [Browsable(true)]
    [Description("Gets or sets the width of the indentation of subitems.")]
    [Category("Layout")]
    public int ItemIndent
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(PropertyGridTableElement.ItemIndentProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridTableElement.ItemIndentProperty, (object) value);
        this.InvalidateMeasure(true);
      }
    }

    [Description("Gets or sets the object which properties the RadPropertyGrid is displaying.")]
    [DefaultValue(null)]
    [Browsable(false)]
    public object SelectedObject
    {
      get
      {
        if (this.currentObject != null)
          return this.currentObject;
        return (object) null;
      }
      set
      {
        PropertyGridSelectedObjectChangingEventArgs e = new PropertyGridSelectedObjectChangingEventArgs(value);
        this.OnSelectedObjectChnging(e);
        if (e.Cancel)
          return;
        this.UnsubscribeINotifyObjects();
        this.SelectedGridItem = (PropertyGridItemBase) null;
        this.currentObject = value;
        this.BindToSingleObject();
        this.CollectionView.Refresh();
        this.OnSelectedObjectChanged(new PropertyGridSelectedObjectChangedEventArgs(value));
      }
    }

    [Description("Gets or sets the objects which properties the RadPropertyGrid is displaying.")]
    [Browsable(false)]
    [DefaultValue(null)]
    public object[] SelectedObjects
    {
      get
      {
        return this.currentObjects;
      }
      set
      {
        PropertyGridSelectedObjectChangingEventArgs e = new PropertyGridSelectedObjectChangingEventArgs((object) value);
        this.OnSelectedObjectChnging(e);
        if (e.Cancel)
          return;
        this.UnsubscribeINotifyObjects();
        this.SelectedGridItem = (PropertyGridItemBase) null;
        this.currentObjects = value;
        this.BindToMultipleObjects();
        this.CollectionView.Refresh();
        this.OnSelectedObjectChanged(new PropertyGridSelectedObjectChangedEventArgs((object) value));
      }
    }

    [Browsable(false)]
    [Description("Gets the collection of items the RadPropertyGrid is bound to.")]
    public RadCollectionView<PropertyGridItem> CollectionView
    {
      get
      {
        return this.listSource.CollectionView;
      }
    }

    public RadListSource<PropertyGridItem> ListSource
    {
      get
      {
        return this.listSource;
      }
    }

    public PropertyGridItemBase SelectedGridItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        if (this.selectedItem == value)
          return;
        this.ProcessSelection(value, false);
      }
    }

    public virtual PropertyGridGroupItemCollection Groups
    {
      get
      {
        return this.groupItems;
      }
    }

    public virtual PropertyGridRootItemsCollection PropertyItems
    {
      get
      {
        return this.items;
      }
    }

    public virtual RadContextMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        if (this.contextMenu == value)
          return;
        this.contextMenu = value;
        this.OnNotifyPropertyChanged(nameof (ContextMenu));
      }
    }

    [Browsable(true)]
    [DefaultValue(RadPropertyGridBeginEditModes.BeginEditOnClick)]
    [Description("Gets or sets a value indicating how user begins editing a cell.")]
    public RadPropertyGridBeginEditModes BeginEditMode
    {
      get
      {
        return this.beginEditMode;
      }
      set
      {
        if (this.beginEditMode == value)
          return;
        this.beginEditMode = value;
        this.OnNotifyPropertyChanged(nameof (BeginEditMode));
      }
    }

    [Description("Gets or sets the distance between property grid items.")]
    [DefaultValue(-1)]
    [Browsable(true)]
    public override int ItemSpacing
    {
      get
      {
        return base.ItemSpacing;
      }
      set
      {
        if (this.ItemSpacing == value)
          return;
        base.ItemSpacing = value;
        this.ViewElement.Invalidate();
      }
    }

    public bool KeyboardSearchEnabled { get; set; }

    public int KeyboardSearchResetInterval
    {
      get
      {
        return this.typingTimer.Interval;
      }
      set
      {
        this.typingTimer.Interval = value;
      }
    }

    public IFindStringComparer FindStringComparer
    {
      get
      {
        return this.findStringComparer;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("The FindStringComparer can not be set to null.");
        this.findStringComparer = value;
        this.OnNotifyPropertyChanged(nameof (FindStringComparer));
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [Description("Fires for custom grouping operation.")]
    public event PropertyGridCustomGroupingEventHandler CustomGrouping;

    protected internal virtual void OnCustomGrouping(PropertyGridCustomGroupingEventArgs e)
    {
      PropertyGridCustomGroupingEventHandler customGrouping = this.CustomGrouping;
      if (customGrouping == null)
        return;
      customGrouping((object) this, e);
    }

    public event PropertyGridSelectedObjectChangingEventHandler SelectedObjectChanging;

    protected internal virtual void OnSelectedObjectChnging(
      PropertyGridSelectedObjectChangingEventArgs e)
    {
      PropertyGridSelectedObjectChangingEventHandler selectedObjectChanging = this.SelectedObjectChanging;
      if (selectedObjectChanging == null)
        return;
      selectedObjectChanging((object) this, e);
    }

    public event PropertyGridSelectedObjectChangedEventHandler SelectedObjectChanged;

    protected internal virtual void OnSelectedObjectChanged(
      PropertyGridSelectedObjectChangedEventArgs e)
    {
      PropertyGridSelectedObjectChangedEventHandler selectedObjectChanged = this.SelectedObjectChanged;
      if (selectedObjectChanged == null)
        return;
      selectedObjectChanged((object) this, e);
    }

    public event PropertyGridItemFormattingEventHandler ItemFormatting;

    protected internal virtual void OnItemFormatting(PropertyGridItemFormattingEventArgs e)
    {
      PropertyGridItemFormattingEventHandler itemFormatting = this.ItemFormatting;
      if (itemFormatting == null)
        return;
      itemFormatting((object) this, e);
    }

    public event PropertyGridMouseEventHandler ItemMouseDown;

    protected internal virtual void OnItemMouseDown(PropertyGridMouseEventArgs e)
    {
      PropertyGridMouseEventHandler itemMouseDown = this.ItemMouseDown;
      if (itemMouseDown == null)
        return;
      itemMouseDown((object) this, e);
    }

    public event RadPropertyGridEventHandler ItemMouseClick;

    protected internal virtual void OnItemMouseClick(RadPropertyGridEventArgs e)
    {
      RadPropertyGridEventHandler itemMouseClick = this.ItemMouseClick;
      if (itemMouseClick == null)
        return;
      itemMouseClick((object) this, e);
    }

    public event RadPropertyGridEventHandler ItemMouseDoubleClick;

    protected internal virtual void OnItemMouseDoubleClick(RadPropertyGridEventArgs e)
    {
      RadPropertyGridEventHandler mouseDoubleClick = this.ItemMouseDoubleClick;
      if (mouseDoubleClick == null)
        return;
      mouseDoubleClick((object) this, e);
    }

    public event PropertyGridMouseEventHandler ItemMouseMove;

    protected internal virtual void OnItemMouseMove(PropertyGridMouseEventArgs e)
    {
      PropertyGridMouseEventHandler itemMouseMove = this.ItemMouseMove;
      if (itemMouseMove == null)
        return;
      itemMouseMove((object) this, e);
    }

    public event RadPropertyGridCancelEventHandler ItemExpandedChanging;

    protected internal virtual void OnItemExpandedChanging(RadPropertyGridCancelEventArgs e)
    {
      RadPropertyGridCancelEventHandler expandedChanging = this.ItemExpandedChanging;
      if (expandedChanging == null)
        return;
      expandedChanging((object) this, e);
    }

    public event RadPropertyGridEventHandler ItemExpandedChanged;

    protected internal virtual void OnItemExpandedChanged(RadPropertyGridEventArgs e)
    {
      RadPropertyGridEventHandler itemExpandedChanged = this.ItemExpandedChanged;
      if (itemExpandedChanged != null)
        itemExpandedChanged((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Expanded");
    }

    public event RadPropertyGridCancelEventHandler SelectedGridItemChanging;

    protected internal virtual void OnSelectedGridItemChanging(RadPropertyGridCancelEventArgs args)
    {
      RadPropertyGridCancelEventHandler gridItemChanging = this.SelectedGridItemChanging;
      if (gridItemChanging == null)
        return;
      gridItemChanging((object) this, args);
    }

    public event RadPropertyGridEventHandler SelectedGridItemChanged;

    protected internal virtual void OnSelectedGridItemChanged(RadPropertyGridEventArgs args)
    {
      RadPropertyGridEventHandler selectedGridItemChanged = this.SelectedGridItemChanged;
      if (selectedGridItemChanged == null)
        return;
      selectedGridItemChanged((object) this, args);
    }

    public event PropertyGridEditorRequiredEventHandler EditorRequired;

    protected internal virtual void OnEditorRequired(PropertyGridEditorRequiredEventArgs e)
    {
      PropertyGridEditorRequiredEventHandler editorRequired = this.EditorRequired;
      if (editorRequired == null)
        return;
      editorRequired((object) this, e);
    }

    public event PropertyGridItemEditingEventHandler Editing;

    protected internal virtual void OnEditing(PropertyGridItemEditingEventArgs e)
    {
      PropertyGridItemEditingEventHandler editing = this.Editing;
      if (editing == null)
        return;
      editing((object) this, e);
    }

    public event PropertyGridItemEditorInitializedEventHandler EditorInitialized;

    protected internal virtual void OnEditorInitialized(PropertyGridItemEditorInitializedEventArgs e)
    {
      PropertyGridItemEditorInitializedEventHandler editorInitialized = this.EditorInitialized;
      if (editorInitialized == null)
        return;
      editorInitialized((object) this, e);
    }

    public event PropertyGridItemEditedEventHandler Edited;

    protected internal virtual void OnEdited(PropertyGridItemEditedEventArgs e)
    {
      PropertyGridItemEditedEventHandler edited = this.Edited;
      if (edited == null)
        return;
      edited((object) this, e);
    }

    public event PropertyGridItemValueChangingEventHandler PropertyValueChanging;

    protected internal virtual void OnPropertyValueChanging(PropertyGridItemValueChangingEventArgs e)
    {
      if (this.isChanging)
        return;
      this.isChanging = true;
      PropertyGridItemValueChangingEventHandler propertyValueChanging = this.PropertyValueChanging;
      if (propertyValueChanging != null)
        propertyValueChanging((object) this, e);
      this.isChanging = false;
    }

    public event PropertyGridItemValueChangedEventHandler PropertyValueChanged;

    protected internal virtual void OnPropertyValueChanged(PropertyGridItemValueChangedEventArgs e)
    {
      PropertyGridItemValueChangedEventHandler propertyValueChanged = this.PropertyValueChanged;
      if (propertyValueChanged != null)
        propertyValueChanged((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Edited");
    }

    public event PropertyGridContextMenuOpeningEventHandler ContextMenuOpening;

    protected internal virtual void OnContextMenuOpening(PropertyGridContextMenuOpeningEventArgs e)
    {
      PropertyGridContextMenuOpeningEventHandler contextMenuOpening = this.ContextMenuOpening;
      if (contextMenuOpening == null)
        return;
      contextMenuOpening((object) this, e);
    }

    protected internal virtual void OnCreateItem(CreatePropertyGridItemEventArgs e)
    {
      CreatePropertyGridItemEventHandler createItem = this.CreateItem;
      if (createItem == null)
        return;
      createItem((object) this, e);
    }

    protected internal virtual void OnCreateItemElement(CreatePropertyGridItemElementEventArgs e)
    {
      CreatePropertyGridItemElementEventHandler createItemElement = this.CreateItemElement;
      if (createItemElement == null)
        return;
      createItemElement((object) this, e);
    }

    public event PropertyValidatingEventHandler PropertyValidating;

    protected internal virtual void OnPropertyValidating(PropertyValidatingEventArgs e)
    {
      PropertyValidatingEventHandler propertyValidating = this.PropertyValidating;
      if (propertyValidating == null)
        return;
      propertyValidating((object) this, e);
    }

    public event PropertyValidatedEventHandler PropertyValidated;

    protected internal virtual void OnPropertyValidated(PropertyValidatedEventArgs e)
    {
      PropertyValidatedEventHandler propertyValidated = this.PropertyValidated;
      if (propertyValidated == null)
        return;
      propertyValidated((object) this, e);
    }

    [Description("Fires before the value in an editor is changing.")]
    public event ValueChangingEventHandler ValueChanging;

    protected internal virtual void OnValueChanging(object sender, ValueChangingEventArgs e)
    {
      ValueChangingEventHandler valueChanging = this.ValueChanging;
      if (valueChanging == null)
        return;
      valueChanging(sender, e);
    }

    [Description("Fires when the value of a editor changes.")]
    public event EventHandler ValueChanged;

    protected internal virtual void OnValueChanged(object sender, EventArgs e)
    {
      EventHandler valueChanged = this.ValueChanged;
      if (valueChanged == null)
        return;
      valueChanged(sender, e);
    }

    [Description("Allows you to raise ValueChanged event when using custom editor.")]
    public void RaiseValueChanged(EventArgs e)
    {
      this.OnValueChanged((object) this, e);
    }

    [Description("Allows you to raise ValueChanging event when using custom editor.")]
    public void RaiseValueChanging(ValueChangingEventArgs e)
    {
      this.OnValueChanging((object) this, e);
    }

    public void BestFit()
    {
      this.BestFit(PropertyGridBestFitMode.MaximizeVisibility);
    }

    public void BestFit(PropertyGridBestFitMode mode)
    {
      List<WidthCountPair> labelsList = new List<WidthCountPair>();
      List<WidthCountPair> valuesList = new List<WidthCountPair>();
      this.FillBestFitDictionaries(out labelsList, out valuesList, mode);
      for (int index = 0; index < labelsList.Count - 1; ++index)
        labelsList[index + 1].Count += labelsList[index].Count;
      for (int index = 0; index < valuesList.Count - 1; ++index)
        valuesList[index + 1].Count += valuesList[index].Count;
      WidthCountPairComparer countPairComparer = new WidthCountPairComparer(false);
      labelsList.Sort((IComparer<WidthCountPair>) countPairComparer);
      valuesList.Sort((IComparer<WidthCountPair>) countPairComparer);
      int num1 = this.Size.Width - this.ItemIndent;
      switch (mode)
      {
        case PropertyGridBestFitMode.MaximizeLabelColumnVisibility:
          labelsList.Reverse();
          for (int index = 0; index < labelsList.Count; ++index)
          {
            if (labelsList[index].Width < num1 - this.MinimumColumnWidth && labelsList[index].Width > this.MinimumColumnWidth)
            {
              this.ValueColumnWidth = num1 - labelsList[index].Width - this.ItemIndent;
              break;
            }
          }
          break;
        case PropertyGridBestFitMode.MaximizeValueColumnVisibility:
          valuesList.Reverse();
          for (int index = 0; index < valuesList.Count; ++index)
          {
            if (valuesList[index].Width < num1 - this.MinimumColumnWidth && valuesList[index].Width > this.MinimumColumnWidth)
            {
              this.ValueColumnWidth = valuesList[index].Width;
              break;
            }
          }
          break;
        case PropertyGridBestFitMode.MaximizeVisibility:
          labelsList.Reverse();
          valuesList.Reverse();
          int num2 = 0;
          int num3 = 0;
          for (int index1 = 0; index1 < labelsList.Count; ++index1)
          {
            for (int index2 = 0; index2 < valuesList.Count; ++index2)
            {
              int num4 = labelsList[index1].Width + valuesList[index2].Width;
              if (num4 <= num1 && labelsList[index1].Count + valuesList[index2].Count > num2)
              {
                num2 = labelsList[index1].Count + valuesList[index2].Count;
                int width = labelsList[index1].Width;
                num3 = valuesList[index2].Width;
                if (num4 < num1)
                  num3 = (int) Math.Round((double) num3 / (double) num4 * (double) num1);
              }
            }
          }
          if (num3 <= 0)
            break;
          this.ValueColumnWidth = num3;
          break;
      }
    }

    private void FillBestFitDictionaries(
      out List<WidthCountPair> labelsList,
      out List<WidthCountPair> valuesList,
      PropertyGridBestFitMode mode)
    {
      labelsList = new List<WidthCountPair>();
      valuesList = new List<WidthCountPair>();
      WidthCountPairComparer countPairComparer = new WidthCountPairComparer(true);
      using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
      {
        foreach (PropertyGridItem propertyItem in this.PropertyItems)
        {
          if (propertyItem != null)
          {
            if (mode == PropertyGridBestFitMode.MaximizeVisibility || mode == PropertyGridBestFitMode.MaximizeLabelColumnVisibility)
            {
              WidthCountPair widthCountPair = new WidthCountPair((int) Math.Ceiling((double) graphics.MeasureString(propertyItem.Label, this.ElementTree.Control.Font).Width) + 2, 1);
              int index = labelsList.BinarySearch(widthCountPair, (IComparer<WidthCountPair>) countPairComparer);
              if (index >= 0)
                ++labelsList[index].Count;
              else
                labelsList.Insert(~index, widthCountPair);
            }
            if (mode == PropertyGridBestFitMode.MaximizeVisibility || mode == PropertyGridBestFitMode.MaximizeValueColumnVisibility)
            {
              WidthCountPair widthCountPair = new WidthCountPair((int) Math.Ceiling((double) graphics.MeasureString(propertyItem.FormattedValue, this.ElementTree.Control.Font).Width) + 2, 1);
              int index = valuesList.BinarySearch(widthCountPair, (IComparer<WidthCountPair>) countPairComparer);
              if (index >= 0)
                ++valuesList[index].Count;
              else
                valuesList.Insert(~index, widthCountPair);
            }
          }
        }
      }
    }

    public void BeginUpdate()
    {
      ++this.updateSuspendedCount;
      this.resumeAction = PropertyGridTableElement.UpdateActions.Resume;
    }

    public void EndUpdate()
    {
      this.EndUpdate(true, this.resumeAction);
    }

    public void EndUpdate(bool performUpdate, PropertyGridTableElement.UpdateActions action)
    {
      if (this.updateSuspendedCount == 0)
        return;
      --this.updateSuspendedCount;
      if (this.updateSuspendedCount != 0 || !performUpdate)
        return;
      this.Update(action);
    }

    public void Update(
      PropertyGridTableElement.UpdateActions updateAction)
    {
      if (this.updateSuspendedCount > 0)
      {
        if (updateAction != PropertyGridTableElement.UpdateActions.Reset)
          return;
        this.resumeAction = PropertyGridTableElement.UpdateActions.Reset;
      }
      else
      {
        switch (updateAction)
        {
          case PropertyGridTableElement.UpdateActions.Reset:
            this.VScrollBar.Value = this.VScrollBar.Minimum;
            this.ViewElement.ElementProvider.ClearCache();
            this.ViewElement.DisposeChildren();
            this.ViewElement.ElementProvider.ClearCache();
            break;
          case PropertyGridTableElement.UpdateActions.ExpandedChanged:
            this.ViewElement.InvalidateMeasure(true);
            this.Scroller.UpdateScrollRange();
            this.SynchronizeVisualItems();
            if (this.VScrollBar.Visibility != ElementVisibility.Visible || this.VScrollBar.Value <= this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1)
              return;
            this.SetScrollValue(this.VScrollBar, this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1);
            return;
        }
        if (updateAction == PropertyGridTableElement.UpdateActions.Reset || updateAction == PropertyGridTableElement.UpdateActions.Resume || updateAction == PropertyGridTableElement.UpdateActions.SortChanged)
          this.UpdateScrollers(updateAction);
        this.ViewElement.UpdateItems();
        if (updateAction != PropertyGridTableElement.UpdateActions.StateChanged && updateAction != PropertyGridTableElement.UpdateActions.SortChanged && (updateAction != PropertyGridTableElement.UpdateActions.Resume && updateAction != PropertyGridTableElement.UpdateActions.ValueChanged))
          return;
        this.SynchronizeVisualItems();
      }
    }

    public PropertyGridItemElementBase GetElementAt(int x, int y)
    {
      if (!this.IsInValidState(true))
        return (PropertyGridItemElementBase) null;
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(new Point(x, y));
      if (elementAtPoint == null)
        return (PropertyGridItemElementBase) null;
      if (elementAtPoint is PropertyGridItemElementBase)
        return elementAtPoint as PropertyGridItemElementBase;
      return elementAtPoint.FindAncestor<PropertyGridItemElementBase>();
    }

    public void EnsureVisible(PropertyGridItemBase item)
    {
      PropertyGridItemElementBase element = this.GetElement(item);
      if (element == null)
      {
        if (this.ViewElement.Children.Count > 0)
        {
          PropertyGridItemBase data1 = ((PropertyGridItemElementBase) this.ViewElement.Children[0]).Data;
          PropertyGridItemBase data2 = ((PropertyGridItemElementBase) this.ViewElement.Children[this.ViewElement.Children.Count - 1]).Data;
          int index = this.traverser.GetIndex(data1);
          if (this.traverser.GetIndex(item) <= index)
            this.Scroller.ScrollToItem(item);
          else
            this.EnsureVisibleCore(item);
        }
      }
      else if (element.ControlBoundingRectangle.Bottom > this.ViewElement.ControlBoundingRectangle.Bottom)
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + Math.Max(1, element.ControlBoundingRectangle.Bottom - this.ViewElement.ControlBoundingRectangle.Bottom));
      else if (element.ControlBoundingRectangle.Top <= this.ViewElement.ControlBoundingRectangle.Top)
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - Math.Max(1, this.ViewElement.ControlBoundingRectangle.Top - element.ControlBoundingRectangle.Top));
      this.UpdateLayout();
    }

    public void ScrollToItem(PropertyGridItemBase item)
    {
      this.Scroller.ScrollToItem(item);
    }

    public RadContextMenu GetElementContextMenu(PropertyGridItemElementBase element)
    {
      if (element == null)
        return (RadContextMenu) null;
      RadContextMenu contextMenu = this.ContextMenu;
      if (element != null && element.Data.ContextMenu != null)
        contextMenu = element.Data.ContextMenu;
      if (element == null && contextMenu is PropertyGridDefaultContextMenu)
        contextMenu = (RadContextMenu) null;
      if (contextMenu != null)
      {
        PropertyGridDefaultContextMenu defaultContextMenu = contextMenu as PropertyGridDefaultContextMenu;
        if (defaultContextMenu != null)
        {
          if (element.Data.Expanded && element.Data.GridItems.Count > 0)
            defaultContextMenu.ExpandCollapseMenuItem.Text = LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCollapse");
          else
            defaultContextMenu.ExpandCollapseMenuItem.Text = LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuExpand");
          defaultContextMenu.ExpandCollapseMenuItem.Enabled = element.Data.GridItems.Count > 0;
          defaultContextMenu.EditMenuItem.Enabled = !this.ReadOnly;
        }
        if (element != null)
          this.SelectedGridItem = element.Data;
        PropertyGridContextMenuOpeningEventArgs e = new PropertyGridContextMenuOpeningEventArgs(element.Data, contextMenu);
        this.OnContextMenuOpening(e);
        if (!e.Cancel)
          return contextMenu;
      }
      return (RadContextMenu) null;
    }

    public void ResetColumnWidths()
    {
      this.ratio = 0.5f;
      this.ValueColumnWidth = (int) ((double) this.currentAvailableWidth * (double) this.ratio);
    }

    public virtual void SortSubItems()
    {
      foreach (PropertyGridItem propertyGridItem in this.CollectionView)
      {
        if (propertyGridItem.GridItems != null && propertyGridItem.GridItems.Count > 1 && propertyGridItem.Expanded)
          propertyGridItem.GridItems.Sort(this.CollectionView.Comparer);
      }
    }

    protected virtual void EnsureVisibleCore(PropertyGridItemBase item)
    {
      bool flag = false;
      int num = 0;
      PropertyGridItemBase data = ((PropertyGridItemElementBase) this.ViewElement.Children[this.ViewElement.Children.Count - 1]).Data;
      PropertyGridTraverser enumerator = (PropertyGridTraverser) this.Scroller.Traverser.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == item)
        {
          int maximum = this.VScrollBar.Maximum;
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num);
          this.UpdateLayout();
          PropertyGridItemElementBase element = this.GetElement(item);
          if (element == null || element.ControlBoundingRectangle.Bottom <= this.ViewElement.ControlBoundingRectangle.Bottom)
            break;
          this.EnsureVisible(item);
          break;
        }
        if (enumerator.Current == data)
          flag = true;
        if (flag)
          num += (int) this.ViewElement.ElementProvider.GetElementSize(enumerator.Current).Height + this.ItemSpacing;
      }
    }

    protected virtual void PerformPropertySort(PropertySort propertySort)
    {
      this.UseCachedValues = true;
      if (this.SortDescriptors.Count > 0)
        this.SortDescriptors.Clear();
      if (this.GroupDescriptors.Count > 0)
        this.GroupDescriptors.Clear();
      switch (propertySort)
      {
        case PropertySort.NoSort:
          this.SortDescriptors.Add("SortOrder", ListSortDirection.Ascending);
          break;
        case PropertySort.Alphabetical:
          this.SortDescriptors.Add("Label", ListSortDirection.Ascending);
          break;
        case PropertySort.Categorized:
          this.SortDescriptors.Add("SortOrder", ListSortDirection.Ascending);
          this.GroupDescriptors.Add("Category", ListSortDirection.Ascending);
          break;
        case PropertySort.CategorizedAlphabetical:
          this.SortDescriptors.Add("Label", ListSortDirection.Ascending);
          this.GroupDescriptors.Add("Category", ListSortDirection.Ascending);
          break;
      }
      this.PropertyGridElement?.ToolbarElement.SyncronizeToggleButtons();
      this.Update(PropertyGridTableElement.UpdateActions.Reset);
      this.UseCachedValues = false;
    }

    protected virtual PropertyGridItem GetSelectedObjectDefaultProperty()
    {
      object[] customAttributes = this.SelectedObject.GetType().GetCustomAttributes(typeof (DefaultPropertyAttribute), false);
      string str = string.Empty;
      if (customAttributes.Length > 0)
        str = ((DefaultPropertyAttribute) customAttributes[0]).Name;
      PropertyGridItem propertyGridItem1 = (PropertyGridItem) null;
      if (str != string.Empty && this.CollectionView.Count > 0)
      {
        foreach (PropertyGridItem propertyGridItem2 in this.CollectionView)
        {
          if (propertyGridItem2.Name == str)
            propertyGridItem1 = propertyGridItem2;
        }
      }
      return propertyGridItem1;
    }

    protected virtual void UpdateScrollers(
      PropertyGridTableElement.UpdateActions updateAction)
    {
      this.Scroller.UpdateScrollRange();
    }

    protected virtual void SynchronizeVisualItems()
    {
      foreach (PropertyGridItemElementBase child in this.ViewElement.Children)
        child.Synchronize();
    }

    private void UnsubscribeINotifyObjects()
    {
      if (this.currentObject != null)
      {
        if (this.currentObject is INotifyPropertyChanged)
          ((INotifyPropertyChanged) this.SelectedObject).PropertyChanged -= new PropertyChangedEventHandler(this.PropertyGridTableElement_PropertyChanged);
        if (this.currentObject is INotifyCollectionChanged)
          ((INotifyCollectionChanged) this.SelectedObject).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.PropertyGridTableElement_CollectionChanged);
      }
      if (this.currentObjects == null)
        return;
      foreach (object currentObject in this.currentObjects)
      {
        if (currentObject is INotifyPropertyChanged)
          ((INotifyPropertyChanged) currentObject).PropertyChanged -= new PropertyChangedEventHandler(this.PropertyGridTableElement_PropertyChanged);
      }
    }

    private void BindToSingleObject()
    {
      this.ClearOldData();
      this.currentObjects = (object[]) null;
      if (this.currentObject == null)
        return;
      if (this.currentObject is RadPropertyStore)
      {
        this.ListSource.BeginUpdate();
        this.ListSource.Clear();
        RadPropertyStore currentObject = this.currentObject as RadPropertyStore;
        for (int index = 0; index < currentObject.Count; ++index)
        {
          PropertyGridItem owner = this.NewItem() as PropertyGridItem;
          PropertyStorePropertyDescriptor propertyDescriptor = new PropertyStorePropertyDescriptor(currentObject[index]);
          DescriptorItemAccessor descriptorItemAccessor = new DescriptorItemAccessor(owner, (PropertyDescriptor) propertyDescriptor);
          owner.Accessor = (IItemAccessor) descriptorItemAccessor;
          owner.SortOrder = index;
          owner.DefaultSortOrder = true;
          this.ListSource.Add(owner);
        }
        this.ListSource.EndUpdate(true);
      }
      else
        this.ListSource.DataSource = (object) TypeDescriptor.GetProperties(this.currentObject, new Attribute[1]
        {
          (Attribute) new BrowsableAttribute(true)
        }, false);
      if (this.currentObject is RadPropertyStore)
        ((INotifyCollectionChanged) this.currentObject).CollectionChanged += new NotifyCollectionChangedEventHandler(this.PropertyGridTableElement_CollectionChanged);
      if (this.currentObject is INotifyPropertyChanged)
        ((INotifyPropertyChanged) this.currentObject).PropertyChanged += new PropertyChangedEventHandler(this.PropertyGridTableElement_PropertyChanged);
      if (this.PropertySort != PropertySort.NoSort)
        return;
      this.PerformPropertySort(PropertySort.NoSort);
    }

    private void BindToMultipleObjects()
    {
      this.ClearOldData();
      this.currentObject = (object) null;
      if (this.currentObjects == null)
        return;
      foreach (object currentObject in this.currentObjects)
      {
        if (currentObject is INotifyPropertyChanged)
          ((INotifyPropertyChanged) currentObject).PropertyChanged += new PropertyChangedEventHandler(this.PropertyGridTableElement_PropertyChanged);
      }
      MultiObjectCollection objectCollection = new MultiObjectCollection(this.currentObjects);
      this.currentObject = (object) objectCollection;
      this.ListSource.DataSource = (object) TypeDescriptor.GetProperties((object) objectCollection, new Attribute[1]
      {
        (Attribute) new BrowsableAttribute(true)
      }, false);
      if (this.PropertySort != PropertySort.NoSort)
        return;
      this.PerformPropertySort(PropertySort.NoSort);
    }

    private void ClearOldData()
    {
      this.ListSource.DataSource = (object) null;
      this.ListSource.Clear();
    }

    private void PropertyGridTableElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Update(PropertyGridTableElement.UpdateActions.ValueChanged);
    }

    private void PropertyGridTableElement_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.ItemChanged)
        this.Update(PropertyGridTableElement.UpdateActions.ValueChanged);
      if (e.Action == NotifyCollectionChangedAction.Add)
      {
        foreach (PropertyStoreItem newItem in (IEnumerable) e.NewItems)
        {
          PropertyGridItem owner = this.NewItem() as PropertyGridItem;
          PropertyStorePropertyDescriptor propertyDescriptor = new PropertyStorePropertyDescriptor(newItem);
          DescriptorItemAccessor descriptorItemAccessor = new DescriptorItemAccessor(owner, (PropertyDescriptor) propertyDescriptor);
          owner.Accessor = (IItemAccessor) descriptorItemAccessor;
          this.ListSource.Add(owner);
          this.ListSource.CollectionView.EnsureDescriptors();
        }
      }
      if (e.Action == NotifyCollectionChangedAction.Remove)
      {
        foreach (PropertyStoreItem newItem in (IEnumerable) e.NewItems)
        {
          foreach (PropertyGridItem propertyGridItem in this.ListSource)
          {
            if (propertyGridItem.Name == newItem.PropertyName)
            {
              this.ListSource.Remove(propertyGridItem);
              break;
            }
          }
        }
      }
      if (e.Action == NotifyCollectionChangedAction.Reset)
        this.ListSource.Clear();
      this.Update(PropertyGridTableElement.UpdateActions.Reset);
    }

    private bool IsScrollBar(Point point)
    {
      if (this.ElementTree == null)
        return false;
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(point);
      if (elementAtPoint == null)
        return false;
      if (elementAtPoint is RadScrollBarElement)
        return true;
      return elementAtPoint.FindAncestor<RadScrollBarElement>() != null;
    }

    private bool IsExpander(Point point)
    {
      if (this.ElementTree == null)
        return false;
      return this.ElementTree.GetElementAtPoint(point) is ExpanderItem;
    }

    private bool IsEditor(Point point)
    {
      if (this.ElementTree == null || this.activeEditor == null)
        return false;
      RadElement editorElement = ((BaseInputEditor) this.activeEditor).EditorElement;
      return editorElement != null && editorElement.ControlBoundingRectangle.Contains(point);
    }

    private bool IsCheckBox(Point point)
    {
      if (this.ElementTree == null)
        return false;
      return this.ElementTree.GetElementAtPoint(point) is RadCheckBoxElement;
    }

    private void SetScrollValue(RadScrollBarElement scrollbar, int newValue)
    {
      if (newValue > scrollbar.Maximum - scrollbar.LargeChange + 1)
        newValue = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (newValue < scrollbar.Minimum)
        newValue = scrollbar.Minimum;
      scrollbar.Value = newValue;
    }

    private void SetValueColumnWidth(int value, bool resetRatio)
    {
      this.valueColumnWidth = value >= this.minimumColumnWidth ? ((double) value <= (double) this.currentAvailableWidth - (double) this.minimumColumnWidth ? value : (int) this.currentAvailableWidth - this.minimumColumnWidth) : this.minimumColumnWidth;
      if (resetRatio)
        this.ratio = (float) this.valueColumnWidth / this.currentAvailableWidth;
      this.ViewElement.InvalidateMeasure(true);
      this.OnNotifyPropertyChanged("ValueColumnWidth");
    }

    protected override void scroller_ScrollerUpdated(object sender, EventArgs e)
    {
      if (!this.EndEdit())
        this.CancelEdit();
      this.ViewElement.ScrollOffset = new SizeF(this.ViewElement.ScrollOffset.Width, (float) -this.Scroller.ScrollOffset);
      this.ViewElement.InvalidateMeasure(true);
    }

    private object PerformGrouping(PropertyGridItem item, int level)
    {
      PropertyGridCustomGroupingEventArgs e = new PropertyGridCustomGroupingEventArgs(this, item);
      this.OnCustomGrouping(e);
      if (e.Handled)
        return e.GroupKey;
      return this.CollectionView.DefaultGroupPredicate(item, level);
    }

    private void SortDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.CancelEdit();
      this.SortSubItems();
      this.Update(PropertyGridTableElement.UpdateActions.SortChanged);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Sorted");
    }

    private void FilterDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.CancelEdit();
      this.listSource.CollectionView.FilterExpression = this.filterDescriptors.Expression;
      this.traverser.Reset();
      if (this.filterDescriptors.Count > 0 && this.filterDescriptors[0].Value != null)
        this.PropertyGridElement.ToolbarElement.SearchTextBoxElement.Text = this.filterDescriptors[0].Value.ToString();
      this.Update(PropertyGridTableElement.UpdateActions.Resume);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Filtered");
    }

    private void GroupDescriptors_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.CancelEdit();
      this.traverser.Reset();
      this.PropertyGridElement.ToolbarElement.SyncronizeToggleButtons();
      this.Update(PropertyGridTableElement.UpdateActions.Resume);
      this.SelectedGridItem = this.selectedItem;
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Grouped");
    }

    private void listSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.CancelEdit();
      if (e.Action == NotifyCollectionChangedAction.Reset)
        this.Update(PropertyGridTableElement.UpdateActions.Reset);
      else
        this.Scroller.UpdateScrollRange();
    }

    private void clickTimer_Tick(object sender, EventArgs e)
    {
      this.clickTimer.Stop();
      this.BeginEdit();
    }

    public virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      if (this.EnableKineticScrolling)
      {
        bool isRunning = this.ScrollBehavior.IsRunning;
        this.ScrollBehavior.MouseDown(e.Location);
        if (isRunning && this.ScrollBehavior.IsRunning)
          return false;
      }
      if (this.IsEditor(e.Location))
        return false;
      if (this.IsCheckBox(e.Location))
      {
        this.selectionWasCanceled = false;
        return false;
      }
      PropertyGridItemElementBase elementAt = this.GetElementAt(e.Location.X, e.Location.Y);
      this.wasSelectedOnMouseDown = elementAt != null && elementAt.Data.Selected;
      this.wasInEditModeOnMouseDown = this.IsEditing;
      if (this.IsEditing)
      {
        if (!this.EndEdit())
          return false;
        if (elementAt != null && this.selectedItem == elementAt.Data)
        {
          this.selectionWasCanceled = true;
          return true;
        }
      }
      if (elementAt == null)
        return false;
      this.selectionWasCanceled = !this.ProcessSelection(elementAt.Data, true);
      this.mouseDownLocation = e.Location;
      return this.selectionWasCanceled;
    }

    public virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      if (this.EnableKineticScrolling)
      {
        bool isRunning = this.ScrollBehavior.IsRunning;
        this.ScrollBehavior.MouseUp(e.Location);
        if (isRunning)
          return false;
      }
      if (this.selectionWasCanceled)
        return true;
      PropertyGridItemElementBase elementAt = this.GetElementAt(e.Location.X, e.Location.Y);
      if (this.doubleClick || this.IsEditor(e.Location) || (this.IsCheckBox(e.Location) || elementAt == null) || !elementAt.ControlBoundingRectangle.Contains(this.mouseDownLocation))
      {
        this.clickTimer.Stop();
        this.doubleClick = false;
        return false;
      }
      PropertyGridItemElement propertyGridItemElement1 = elementAt as PropertyGridItemElement;
      if (this.IsScrollBar(e.Location) || propertyGridItemElement1 != null && propertyGridItemElement1.IsInResizeLocation(e.Location))
      {
        if (this.IsEditing)
          this.EndEdit();
        return false;
      }
      if (e.Button == MouseButtons.Left)
      {
        PropertyGridItemElement propertyGridItemElement2 = elementAt as PropertyGridItemElement;
        if (propertyGridItemElement2 != null && (propertyGridItemElement2.TextElement as PropertyGridTextElement).PropertyValueButton.ControlBoundingRectangle.Contains(e.Location))
          return false;
        if ((this.beginEditMode == RadPropertyGridBeginEditModes.BeginEditOnClick || this.wasInEditModeOnMouseDown) && !this.IsExpander(e.Location))
          return this.BeginEdit();
        if (this.beginEditMode == RadPropertyGridBeginEditModes.BeginEditOnSecondClick && (this.wasSelectedOnMouseDown || this.wasInEditModeOnMouseDown) && !this.IsExpander(e.Location))
        {
          this.clickTimer.Interval = SystemInformation.DoubleClickTime;
          this.clickTimer.Start();
        }
      }
      return false;
    }

    public virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      if (this.EnableKineticScrolling)
        this.ScrollBehavior.MouseMove(e.Location);
      if (this.IsScrollBar(e.Location))
        this.ElementTree.Control.Cursor = Cursors.Default;
      return false;
    }

    public virtual bool ProecessMouseEnter(EventArgs e)
    {
      return false;
    }

    public virtual bool ProecessMouseLeave(EventArgs e)
    {
      return false;
    }

    public virtual bool ProcessMouseClick(MouseEventArgs e)
    {
      return false;
    }

    public virtual bool ProcessMouseDoubleClick(MouseEventArgs e)
    {
      PropertyGridItemElementBase elementAt = this.GetElementAt(e.Location.X, e.Location.Y);
      PropertyGridGroupElement gridGroupElement = elementAt as PropertyGridGroupElement;
      if (gridGroupElement != null)
        gridGroupElement.Data.Expanded = !gridGroupElement.Data.Expanded;
      PropertyGridItemElement propertyGridItemElement = elementAt as PropertyGridItemElement;
      bool flag = false;
      if (propertyGridItemElement != null && propertyGridItemElement.IsInResizeLocation(e.Location))
      {
        this.ResetColumnWidths();
        flag = true;
      }
      if (elementAt != null && elementAt.Data.GridItems.Count > 0)
      {
        if (this.activeEditor != null && !this.EndEdit())
        {
          this.doubleClick = true;
          return false;
        }
        if (!this.IsExpander(e.Location) && !flag && this.beginEditMode == RadPropertyGridBeginEditModes.BeginEditOnSecondClick)
          elementAt.Data.Expanded = !elementAt.Data.Expanded;
      }
      else if (this.beginEditMode == RadPropertyGridBeginEditModes.BeginEditOnSecondClick && !this.IsCheckBox(e.Location) && elementAt is PropertyGridItemElement)
        this.BeginEdit();
      this.doubleClick = true;
      return false;
    }

    public virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      if (!this.IsEditing || this.selectedItem is PropertyGridItem && (object) ((PropertyGridItem) this.selectedItem).PropertyType == (object) typeof (bool))
      {
        int num = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - Math.Sign(e.Delta) * num * SystemInformation.MouseWheelScrollLines * this.VScrollBar.SmallChange);
      }
      return false;
    }

    public virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      PropertyGridItem selectedItem = this.selectedItem as PropertyGridItem;
      if (this.IsEditing && selectedItem != null && (object) selectedItem.PropertyType != (object) typeof (bool))
        return false;
      switch (e.KeyCode)
      {
        case Keys.Return:
        case Keys.Space:
        case Keys.F2:
          this.HandleF2Key(e);
          return true;
        case Keys.Escape:
          this.HandleEscapeKey(e);
          break;
        case Keys.Prior:
          this.HandlePageUpKey(e);
          break;
        case Keys.Next:
          this.HandlePageDownKey(e);
          break;
        case Keys.End:
          this.HandleEndKey(e);
          break;
        case Keys.Home:
          this.HandleHomeKey(e);
          break;
        case Keys.Left:
          this.HandleLeftKey(e);
          break;
        case Keys.Up:
          this.HandleUpKey(e);
          break;
        case Keys.Right:
          this.HandleRightKey(e);
          break;
        case Keys.Down:
          this.HandleDownKey(e);
          break;
        case Keys.Add:
          this.HandleAddKey(e);
          break;
        case Keys.Subtract:
          this.HandleSubtractKey(e);
          break;
      }
      return false;
    }

    protected internal virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      this.HandleNavigation(e.KeyChar);
      return false;
    }

    public virtual bool ProcessContextMenu(Point location)
    {
      if (this.IsScrollBar(location) || this.IsExpander(location) || this.activeEditor != null && !this.EndEdit())
        return false;
      RadContextMenu elementContextMenu = this.GetElementContextMenu(this.GetElementAt(location.X, location.Y));
      if (elementContextMenu == null)
        return false;
      if (elementContextMenu is PropertyGridDefaultContextMenu)
        ((PropertyGridDefaultContextMenu) elementContextMenu).EditMenuItem.Enabled = !this.wasInEditModeOnMouseDown || !this.selectionWasCanceled;
      elementContextMenu.Show(this.ElementTree.Control, location);
      return true;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (!this.pendingScrollerUpdates)
        return;
      this.pendingScrollerUpdates = false;
      this.Update(PropertyGridTableElement.UpdateActions.Resume);
    }

    protected override void OnAutoSizeChanged()
    {
      if (!this.AutoSizeItems && this.Items.Count > 0)
      {
        foreach (PropertyGridItemBase propertyGridItemBase in (IEnumerable<PropertyGridItemBase>) this.Items)
          propertyGridItemBase.ItemHeight = -1;
      }
      base.OnAutoSizeChanged();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadObject.BindingContextProperty)
        return;
      EventHandler bindingContextChanged = this.BindingContextChanged;
      if (bindingContextChanged == null)
        return;
      bindingContextChanged((object) this, (EventArgs) e);
    }

    protected virtual bool ProcessSelection(PropertyGridItemBase item, bool isMouseSelection)
    {
      if (this.selectedItem == item)
      {
        if (this.IsEditing)
          return this.EndEdit();
        return true;
      }
      RadPropertyGridCancelEventArgs args = new RadPropertyGridCancelEventArgs(item);
      this.OnSelectedGridItemChanging(args);
      if (args.Cancel || this.selectedItem != null && this.IsEditing && !this.EndEdit())
        return false;
      this.selectedItem = item;
      this.Update(PropertyGridTableElement.UpdateActions.StateChanged);
      if (!isMouseSelection)
        this.EnsureVisible(item);
      this.OnNotifyPropertyChanged("SelectedItem");
      this.OnSelectedGridItemChanged(new RadPropertyGridEventArgs(item));
      return true;
    }

    private void HandleEscapeKey(KeyEventArgs e)
    {
      this.CancelEdit();
    }

    private void HandleSubtractKey(KeyEventArgs e)
    {
      if (this.selectedItem == null)
        return;
      this.selectedItem.Collapse();
    }

    private void HandleAddKey(KeyEventArgs e)
    {
      if (this.selectedItem == null)
        return;
      this.selectedItem.Expand();
    }

    private void HandleHomeKey(KeyEventArgs e)
    {
      PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this);
      if (!propertyGridTraverser.MoveToFirst())
        return;
      this.ProcessSelection(propertyGridTraverser.Current, false);
    }

    private void HandleEndKey(KeyEventArgs e)
    {
      PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this);
      if (!propertyGridTraverser.MoveToEnd())
        return;
      this.ProcessSelection(propertyGridTraverser.Current, false);
    }

    private void HandleLeftKey(KeyEventArgs e)
    {
      if (this.selectedItem == null)
        return;
      if (this.selectedItem.Expandable && this.selectedItem.Expanded)
        this.selectedItem.Collapse();
      else
        this.HandleUpKey(e);
    }

    private void HandleRightKey(KeyEventArgs e)
    {
      if (this.selectedItem == null)
        return;
      if (this.selectedItem.Expandable && !this.selectedItem.Expanded)
        this.selectedItem.Expand();
      else
        this.HandleDownKey(e);
    }

    private void HandleUpKey(KeyEventArgs e)
    {
      if (this.selectedItem == null)
      {
        PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this);
        if (!propertyGridTraverser.MoveNext())
          return;
        this.ProcessSelection(propertyGridTraverser.Current, false);
      }
      else
      {
        PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this);
        propertyGridTraverser.MoveTo(this.selectedItem);
        if (!propertyGridTraverser.MovePrevious())
          return;
        PropertyGridItemBase current = propertyGridTraverser.Current;
        if (!propertyGridTraverser.MovePrevious())
          return;
        this.ProcessSelection(current, false);
      }
    }

    private void HandleDownKey(KeyEventArgs e)
    {
      if (this.selectedItem == null)
      {
        PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this);
        if (!propertyGridTraverser.MoveNext())
          return;
        this.ProcessSelection(propertyGridTraverser.Current, false);
      }
      else
      {
        PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this);
        propertyGridTraverser.MoveTo(this.selectedItem);
        if (!propertyGridTraverser.MoveNext())
          return;
        this.ProcessSelection(propertyGridTraverser.Current, false);
      }
    }

    private void HandleF2Key(KeyEventArgs e)
    {
      this.BeginEdit();
    }

    private void HandlePageUpKey(KeyEventArgs e)
    {
      PropertyGridItemElementBase fullVisibleElement1 = this.GetFirstFullVisibleElement();
      if (fullVisibleElement1 == null)
        return;
      if (!fullVisibleElement1.Data.Selected)
      {
        this.SelectedGridItem = fullVisibleElement1.Data;
      }
      else
      {
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - (this.ViewElement.ControlBoundingRectangle.Height - fullVisibleElement1.ControlBoundingRectangle.Bottom));
        this.ViewElement.UpdateItems();
        this.UpdateLayout();
        PropertyGridItemElementBase fullVisibleElement2 = this.GetFirstFullVisibleElement();
        if (fullVisibleElement2 == null)
          return;
        this.SelectedGridItem = fullVisibleElement2.Data;
      }
    }

    private void HandlePageDownKey(KeyEventArgs e)
    {
      PropertyGridItemElementBase fullVisibleElement1 = this.GetLastFullVisibleElement();
      if (fullVisibleElement1 == null)
        return;
      if (!fullVisibleElement1.Data.Selected)
      {
        this.SelectedGridItem = fullVisibleElement1.Data;
      }
      else
      {
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + fullVisibleElement1.ControlBoundingRectangle.Top);
        this.ViewElement.UpdateItems();
        this.UpdateLayout();
        PropertyGridItemElementBase fullVisibleElement2 = this.GetLastFullVisibleElement();
        if (fullVisibleElement2 == null)
          return;
        this.SelectedGridItem = fullVisibleElement2.Data;
      }
    }

    private PropertyGridItemElementBase GetLastFullVisibleElement()
    {
      for (int index = this.ViewElement.Children.Count - 1; index >= 0; --index)
      {
        PropertyGridItemElementBase child = (PropertyGridItemElementBase) this.ViewElement.Children[index];
        if (child.ControlBoundingRectangle.Bottom <= this.ViewElement.ControlBoundingRectangle.Bottom)
          return child;
      }
      return (PropertyGridItemElementBase) null;
    }

    private PropertyGridItemElementBase GetFirstFullVisibleElement()
    {
      for (int index = 0; index < this.ViewElement.Children.Count - 1; ++index)
      {
        PropertyGridItemElementBase child = (PropertyGridItemElementBase) this.ViewElement.Children[index];
        if (child.ControlBoundingRectangle.Top >= this.ViewElement.ControlBoundingRectangle.Top)
          return child;
      }
      return (PropertyGridItemElementBase) null;
    }

    private bool IsSameLetter(StringBuilder sb)
    {
      for (int index = 1; index < sb.Length; ++index)
      {
        if ((int) sb[index] != (int) sb[index - 1])
          return false;
      }
      return true;
    }

    protected virtual PropertyGridItemBase GetFirstMatch(
      string searchCriteria,
      IList<PropertyGridItem> items)
    {
      PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this);
      PropertyGridItemBase propertyGridItemBase = this.selectedItem;
      bool flag = false;
      if (propertyGridItemBase == null)
      {
        propertyGridTraverser.MoveTo(0);
        propertyGridItemBase = propertyGridTraverser.Current;
      }
      else
        propertyGridTraverser.MoveTo(propertyGridItemBase);
      while (propertyGridTraverser.Current == null || string.Equals(this.lastSearchCriteria, searchCriteria) || !this.FindStringComparer.Compare(propertyGridTraverser.Current.Label, searchCriteria))
      {
        while (propertyGridTraverser.MoveNext())
        {
          if (this.FindStringComparer.Compare(propertyGridTraverser.Current.Label, searchCriteria))
          {
            this.lastSearchCriteria = searchCriteria;
            return propertyGridTraverser.Current;
          }
        }
        if (!flag)
        {
          propertyGridTraverser.Reset();
          flag = true;
          if (propertyGridTraverser.Current != propertyGridItemBase)
            continue;
        }
        this.lastSearchCriteria = searchCriteria;
        return (PropertyGridItemBase) null;
      }
      this.lastSearchCriteria = searchCriteria;
      return propertyGridTraverser.Current;
    }

    private void typingTimer_Tick(object sender, EventArgs e)
    {
      this.typingTimer.Stop();
    }

    private void HandleNavigation(char keyChar)
    {
      if (!this.KeyboardSearchEnabled)
        return;
      PropertyGridItem selectedItem = this.selectedItem as PropertyGridItem;
      if (this.IsEditing && selectedItem != null && (object) selectedItem.PropertyType != (object) typeof (bool))
        return;
      if (this.typingTimer.Enabled)
      {
        this.typingTimer.Stop();
        this.typingTimer.Start();
      }
      else
      {
        this.searchBuffer = new StringBuilder();
        this.typingTimer.Start();
      }
      this.searchBuffer.Append(keyChar);
      PropertyGridItemBase firstMatch = this.GetFirstMatch(this.IsSameLetter(this.searchBuffer) ? this.searchBuffer[0].ToString() : this.searchBuffer.ToString(), (IList<PropertyGridItem>) this.items);
      if (firstMatch == null)
        return;
      this.ProcessSelection(firstMatch, false);
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      this.EndEdit();
      int num = this.Scroller.Scrollbar.Value - args.Offset.Height;
      if (this.Scroller.Scrollbar.ControlBoundingRectangle.Contains(args.Location))
        return;
      if (num > this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1)
        num = this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1;
      if (num < this.Scroller.Scrollbar.Minimum)
        num = this.Scroller.Scrollbar.Minimum;
      this.Scroller.Scrollbar.Value = num;
      args.Handled = true;
    }

    public event EventHandler BindingContextChanged;

    public IDataItem NewItem()
    {
      return this.NewItem((PropertyGridItem) null);
    }

    public IDataItem NewItem(PropertyGridItem parentItem)
    {
      CreatePropertyGridItemEventArgs e = new CreatePropertyGridItemEventArgs(typeof (PropertyGridItem), parentItem);
      this.OnCreateItem(e);
      PropertyGridItem propertyGridItem = (PropertyGridItem) null;
      if (e.Item != null)
        propertyGridItem = e.Item as PropertyGridItem;
      if (propertyGridItem == null)
        propertyGridItem = new PropertyGridItem(this, parentItem);
      else if (propertyGridItem.Parent != parentItem)
        propertyGridItem.SetParent(parentItem);
      propertyGridItem.SortOrder = this.sort++;
      propertyGridItem.DefaultSortOrder = true;
      return (IDataItem) propertyGridItem;
    }

    public void Initialize()
    {
      (this.listSource.CollectionView.Comparer as PropertyGridItemComparer)?.Update();
    }

    public void BindingComplete()
    {
    }

    void IDataItemSource.MetadataChanged(PropertyDescriptor pd)
    {
    }

    protected virtual System.Type GetEditorTypeForItem(PropertyGridItem item)
    {
      System.Type type = item.PropertyType;
      if (type.IsGenericType && (object) type.GetGenericTypeDefinition() == (object) typeof (Nullable<>))
        type = type.GetGenericArguments()[0];
      BaseInputEditor editor1 = item.PropertyDescriptor.GetEditor(typeof (BaseInputEditor)) as BaseInputEditor;
      if (editor1 != null)
        return editor1.GetType();
      UITypeEditor editor2 = item.PropertyDescriptor.GetEditor(typeof (UITypeEditor)) as UITypeEditor;
      if (this.OverrideBuiltInEditors && editor2 != null)
        return typeof (PropertyGridUITypeEditor);
      if (item.TypeConverter.GetStandardValuesSupported((ITypeDescriptorContext) item) && editor2 == null)
        return typeof (PropertyGridDropDownListEditor);
      if ((object) type == (object) typeof (Color))
        return typeof (PropertyGridColorEditor);
      if ((object) type == (object) typeof (Image))
        return typeof (PropertyGridBrowseEditor);
      if ((object) type == (object) typeof (DateTime))
        return typeof (PropertyGridDateTimeEditor);
      if (editor2 != null && (object) type != (object) typeof (string))
        return typeof (PropertyGridUITypeEditor);
      if (TelerikHelper.IsNumericType(type))
        return typeof (PropertyGridSpinEditor);
      return typeof (PropertyGridTextBoxEditor);
    }

    public virtual bool BeginEdit()
    {
      if (this.readOnly || this.activeEditor != null || (this.SelectedGridItem == null || this.selectedItem is PropertyGridGroupItem))
        return false;
      PropertyGridItem selectedItem1 = this.selectedItem as PropertyGridItem;
      if (selectedItem1 != null && (!selectedItem1.Enabled || !this.IsItemEditable(selectedItem1)))
        return false;
      this.EnsureVisible(this.SelectedGridItem);
      PropertyGridEditorRequiredEventArgs e1 = new PropertyGridEditorRequiredEventArgs(this.SelectedGridItem, this.GetEditorTypeForItem((PropertyGridItem) this.selectedItem));
      this.OnEditorRequired(e1);
      IInputEditor editor = e1.Editor as IInputEditor ?? this.GetEditor(e1.EditorType);
      if (editor == null)
        return false;
      this.activeEditor = editor;
      PropertyGridItemEditingEventArgs e2 = new PropertyGridItemEditingEventArgs(this.selectedItem, (IValueEditor) editor);
      this.OnEditing(e2);
      if (e2.Cancel)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      if (this.GetElement(this.selectedItem) == null)
        this.SelectedGridItem.EnsureVisible();
      PropertyGridItemElement element = this.GetElement(this.SelectedGridItem) as PropertyGridItemElement;
      if (element == null)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      element.AddEditor(editor);
      ISupportInitialize activeEditor1 = this.activeEditor as ISupportInitialize;
      activeEditor1?.BeginInit();
      PropertyGridItem selectedItem2 = this.selectedItem as PropertyGridItem;
      if (selectedItem2 == null)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      object formattedValue = selectedItem2.Value;
      if (this.activeEditor is PropertyGridTextBoxEditor || this.activeEditor is PropertyGridColorEditor || this.activeEditor is RadBrowseEditor)
        formattedValue = (object) selectedItem1.FormattedValue;
      this.activeEditor.Initialize((object) element, formattedValue);
      activeEditor1?.EndInit();
      this.OnEditorInitialized(new PropertyGridItemEditorInitializedEventArgs((PropertyGridItemElementBase) element, (IValueEditor) editor));
      RadControl radControl = this.ElementTree == null || this.ElementTree.Control == null ? (RadControl) null : this.ElementTree.Control as RadControl;
      if (radControl != null && TelerikHelper.IsMaterialTheme(radControl.ThemeName))
      {
        BaseInputEditor activeEditor2 = this.activeEditor as BaseInputEditor;
        if (activeEditor2 != null)
          activeEditor2.EditorElement.StretchVertically = true;
      }
      this.activeEditor.BeginEdit();
      this.cachedOldValue = (object) selectedItem2.FormattedValue;
      return true;
    }

    public ImageFormat GetImageFormat(Image image)
    {
      if (image.RawFormat.Equals((object) ImageFormat.Jpeg))
        return ImageFormat.Jpeg;
      if (image.RawFormat.Equals((object) ImageFormat.Bmp))
        return ImageFormat.Bmp;
      if (image.RawFormat.Equals((object) ImageFormat.Png))
        return ImageFormat.Png;
      if (image.RawFormat.Equals((object) ImageFormat.Emf))
        return ImageFormat.Emf;
      if (image.RawFormat.Equals((object) ImageFormat.Exif))
        return ImageFormat.Exif;
      if (image.RawFormat.Equals((object) ImageFormat.Gif))
        return ImageFormat.Gif;
      if (image.RawFormat.Equals((object) ImageFormat.Icon))
        return ImageFormat.Icon;
      if (image.RawFormat.Equals((object) ImageFormat.MemoryBmp))
        return ImageFormat.MemoryBmp;
      if (image.RawFormat.Equals((object) ImageFormat.Tiff))
        return ImageFormat.Tiff;
      return ImageFormat.Wmf;
    }

    public bool EndEdit()
    {
      if (this.isChanging)
        return false;
      return this.EndEditCore(true);
    }

    public void CancelEdit()
    {
      this.EndEditCore(false);
    }

    protected virtual bool EndEditCore(bool commitChanges)
    {
      this.clickTimer.Stop();
      if (!this.IsEditing)
        return false;
      PropertyGridItemElement element = this.GetElement(this.SelectedGridItem) as PropertyGridItemElement;
      if (element == null)
        return false;
      PropertyGridItem selectedItem = this.selectedItem as PropertyGridItem;
      if (selectedItem == null)
        return false;
      if (commitChanges && this.activeEditor.IsModified)
      {
        PropertyValidatingEventArgs e = new PropertyValidatingEventArgs(element.Data, this.activeEditor.Value, selectedItem.Value);
        this.OnPropertyValidating(e);
        if (e.Cancel)
        {
          ((BaseInputEditor) this.ActiveEditor).EditorElement.Focus();
          return false;
        }
        this.OnPropertyValidated(new PropertyValidatedEventArgs(element.Data));
        if (selectedItem != null)
        {
          if ((object) selectedItem.PropertyType == (object) typeof (Image))
          {
            Image image = (Image) null;
            string str = (string) null;
            if (this.activeEditor.Value != null)
              str = this.activeEditor.Value.ToString();
            if (File.Exists(str))
              image = Image.FromFile(str);
            else if (!string.IsNullOrEmpty(str))
            {
              try
              {
                byte[] buffer = Convert.FromBase64String(str);
                using (MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length))
                {
                  memoryStream.Write(buffer, 0, buffer.Length);
                  image = Image.FromStream((Stream) memoryStream, true);
                }
              }
              catch (Exception ex)
              {
                this.CancelEdit();
                return true;
              }
            }
            selectedItem.Value = (object) image;
          }
          else
            selectedItem.Value = this.activeEditor.Value;
        }
      }
      if (this.activeEditor != null)
        this.activeEditor.EndEdit();
      selectedItem.ErrorMessage = string.Empty;
      element.RemoveEditor(this.activeEditor);
      this.InvalidateMeasure();
      this.UpdateLayout();
      this.OnEdited(new PropertyGridItemEditedEventArgs(element, (IValueEditor) this.activeEditor, !commitChanges));
      this.activeEditor = (IInputEditor) null;
      this.Focus();
      return true;
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

    protected virtual bool IsItemEditable(PropertyGridItem item)
    {
      return !item.ReadOnly || (object) item.PropertyType.GetInterface("ICollection") != null || item.Accessor is ImmutableItemAccessor;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      float num = availableSize.Width - (float) this.ItemIndent;
      if (this.VScrollBar.Visibility == ElementVisibility.Visible)
        num -= this.VScrollBar.DesiredSize.Width;
      this.currentAvailableWidth = num;
      this.SetValueColumnWidth((int) ((double) num * (double) this.ratio), false);
      return sizeF;
    }

    public enum UpdateActions
    {
      Reset,
      Resume,
      ExpandedChanged,
      StateChanged,
      SortChanged,
      ValueChanged,
    }
  }
}
