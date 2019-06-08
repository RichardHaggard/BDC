// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGanttViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadGanttViewElement : LightVisualElement
  {
    public static RadProperty HeaderHeightProperty = RadProperty.Register(nameof (HeaderHeight), typeof (int), typeof (RadGanttViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 50, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ItemHeightProperty = RadProperty.Register(nameof (ItemHeight), typeof (int), typeof (RadGanttViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 25, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty SplitterWidthProperty = RadProperty.Register(nameof (SplitterWidth), typeof (int), typeof (RadGanttViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ItemSpacingProperty = RadProperty.Register(nameof (ItemSpacing), typeof (int), typeof (RadGanttViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ReadOnlyProperty = RadProperty.Register(nameof (ReadOnly), typeof (bool), typeof (RadGanttViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty MinimumLinkLengthProperty = RadProperty.Register(nameof (MinimumLinkLength), typeof (int), typeof (RadGanttViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty MinimumTaskWidthProperty = RadProperty.Register(nameof (MinimumTaskWidth), typeof (int), typeof (RadGanttViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 8, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private float ratio = 0.5f;
    private int minimumColumnWidth = 5;
    private bool allowSummaryEditing = true;
    private bool allowDefaultContextMenu = true;
    private RadGanttViewElement.UpdateActions resumeAction = RadGanttViewElement.UpdateActions.Resume;
    private Dictionary<System.Type, IInputEditor> cachedEditors = new Dictionary<System.Type, IInputEditor>();
    private bool disableEnsureItemVisibleHorizontal;
    private int updateCurrentItemChanged;
    private int updateSelectedItemChanged;
    private int updateSelectionChanged;
    private int updateSuspendedCount;
    private GanttViewTextViewElement textViewElement;
    private GanttViewViewsSplitterElement splitterElement;
    private GanttViewGraphicalViewElement graphicalViewElement;
    private GanttViewLinkDataItemCollection links;
    private GanttViewDataItem root;
    private GanttViewDataItem itemToEnsureVisible;
    private GanttViewDataItem selectedItem;
    private GanttViewLinkDataItem selectedLink;
    private GanttViewTextViewColumn currentColumn;
    private bool enableCustomPainting;
    private RadContextMenu contextMenu;
    private GanttViewDragDropService dragDropService;
    private BaseGanttViewBehavior ganttViewBehavior;
    private ScrollServiceBehavior scrollBehavior;
    private bool enableKineticScrolling;
    private LinkTypeConverter linkTypeConverter;
    private IComparer<GanttViewDataItem> comparer;
    private FilterDescriptorCollection filterDescriptors;
    private Predicate<GanttViewDataItem> filterPredicate;
    private RadGanttViewElement.UpdateActions previousEndUpdateAction;
    private RadGanttViewElement.UpdateActions pendingScrollerUpdates;
    private GanttViewBindingProvider bindingProvider;
    private WeakReference dataProvider;
    private GanttViewBeginEditModes beginEditMode;
    private IInputEditor activeEditor;
    private object cachedOldValue;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.LinkTypeConverter = new LinkTypeConverter();
      this.dragDropService = new GanttViewDragDropService(this);
      this.GanttViewBehavior = new BaseGanttViewBehavior();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.textViewElement = this.CreateTextViewElement(this);
      this.splitterElement = this.CreateViewsSplitterElement();
      this.graphicalViewElement = this.CreateGraphicalViewElement(this);
      this.Children.Add((RadElement) this.textViewElement);
      this.Children.Add((RadElement) this.splitterElement);
      this.Children.Add((RadElement) this.graphicalViewElement);
      this.scrollBehavior = new ScrollServiceBehavior();
      this.scrollBehavior.ScrollServices.Add(new ScrollService((RadElement) this, this.GraphicalViewElement.VScrollBar));
      this.scrollBehavior.ScrollServices.Add(new ScrollService((RadElement) this.GraphicalViewElement, this.GraphicalViewElement.HorizontalScrollBarElement));
      this.scrollBehavior.ScrollServices.Add(new ScrollService((RadElement) this.TextViewElement, this.TextViewElement.ColumnScroller.Scrollbar));
    }

    public RadGanttViewElement()
    {
      this.filterDescriptors = new FilterDescriptorCollection();
      this.bindingProvider = new GanttViewBindingProvider(this);
      this.root = (GanttViewDataItem) new RadGanttViewElement.RootDataItem(this);
      this.OnRootCreated(EventArgs.Empty);
    }

    protected virtual GanttViewTextViewElement CreateTextViewElement(
      RadGanttViewElement ganttView)
    {
      return new GanttViewTextViewElement(ganttView);
    }

    protected virtual GanttViewViewsSplitterElement CreateViewsSplitterElement()
    {
      return new GanttViewViewsSplitterElement();
    }

    protected virtual GanttViewGraphicalViewElement CreateGraphicalViewElement(
      RadGanttViewElement ganttView)
    {
      return new GanttViewGraphicalViewElement(ganttView);
    }

    public bool AllowSummaryEditing
    {
      get
      {
        return this.allowSummaryEditing;
      }
      set
      {
        this.allowSummaryEditing = value;
      }
    }

    public bool HasLinks
    {
      get
      {
        if (this.links == null)
          return false;
        return this.links.Count > 0;
      }
    }

    public GanttViewLinkDataItemCollection Links
    {
      get
      {
        if (this.links == null)
          this.links = new GanttViewLinkDataItemCollection(this);
        if (this.links.NeedsRefresh)
        {
          this.BeginUpdate();
          this.bindingProvider.SuspendUpdate();
          this.links = new GanttViewLinkDataItemCollection(this);
          this.links.BeginUpdate();
          IList<GanttViewLinkDataItem> linkItems = this.bindingProvider.GetLinkItems();
          if (linkItems != null)
            this.links.AddRange((IEnumerable<GanttViewLinkDataItem>) linkItems);
          this.links.EndUpdate(false);
          this.links.SyncVersion();
          this.bindingProvider.ResumeUpdate();
          this.EndUpdate(false, RadGanttViewElement.UpdateActions.Resume);
        }
        return this.links;
      }
    }

    public GanttViewDataItemCollection Items
    {
      get
      {
        return this.Root.Items;
      }
    }

    public GanttViewTextViewElement TextViewElement
    {
      get
      {
        return this.textViewElement;
      }
    }

    public GanttViewGraphicalViewElement GraphicalViewElement
    {
      get
      {
        return this.graphicalViewElement;
      }
    }

    public GanttViewViewsSplitterElement SplitterElement
    {
      get
      {
        return this.splitterElement;
      }
    }

    public GanttViewTextViewColumnCollection Columns
    {
      get
      {
        return this.TextViewElement.Columns;
      }
    }

    internal GanttViewDataItem Root
    {
      get
      {
        return this.root;
      }
    }

    [DefaultValue(false)]
    public bool EnableCustomPainting
    {
      get
      {
        return this.enableCustomPainting;
      }
      set
      {
        this.enableCustomPainting = value;
      }
    }

    public float Ratio
    {
      get
      {
        return this.ratio;
      }
      set
      {
        float num = value;
        if ((double) value < 1.0 / 500.0)
          num = 1f / 500f;
        else if ((double) value > 0.982999980449677)
          num = 0.983f;
        if ((double) this.ratio == (double) num)
          return;
        this.ratio = value;
        this.InvalidateMeasure();
      }
    }

    [DefaultValue(5)]
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

    [DefaultValue(25)]
    public int ItemHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(RadGanttViewElement.ItemHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(RadGanttViewElement.ItemHeightProperty, (object) value);
      }
    }

    [DefaultValue(0)]
    public int ItemSpacing
    {
      get
      {
        return (int) this.GetValue(RadGanttViewElement.ItemSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGanttViewElement.ItemSpacingProperty, (object) value);
      }
    }

    [DefaultValue(50)]
    public int HeaderHeight
    {
      get
      {
        return (int) ((double) (int) this.GetValue(RadGanttViewElement.HeaderHeightProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(RadGanttViewElement.HeaderHeightProperty, (object) value);
      }
    }

    [DefaultValue(20)]
    public int MinimumLinkLength
    {
      get
      {
        return (int) ((double) (int) this.GetValue(RadGanttViewElement.MinimumLinkLengthProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(RadGanttViewElement.MinimumLinkLengthProperty, (object) value);
      }
    }

    [DefaultValue(5)]
    public int SplitterWidth
    {
      get
      {
        return (int) ((double) (int) this.GetValue(RadGanttViewElement.SplitterWidthProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(RadGanttViewElement.SplitterWidthProperty, (object) value);
      }
    }

    [DefaultValue(8)]
    public int MinimumTaskWidth
    {
      get
      {
        return (int) this.GetValue(RadGanttViewElement.MinimumTaskWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGanttViewElement.MinimumTaskWidthProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return (bool) this.GetValue(RadGanttViewElement.ReadOnlyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGanttViewElement.ReadOnlyProperty, (object) value);
      }
    }

    public bool IsEditing
    {
      get
      {
        return this.ActiveEditor != null;
      }
    }

    public IValueEditor ActiveEditor
    {
      get
      {
        return (IValueEditor) this.activeEditor;
      }
    }

    public GanttViewBeginEditModes BeginEditMode
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

    public GanttViewDragDropService DragDropService
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

    public BaseGanttViewBehavior GanttViewBehavior
    {
      get
      {
        return this.ganttViewBehavior;
      }
      set
      {
        if (this.ganttViewBehavior == value)
          return;
        this.ganttViewBehavior = value;
        this.ganttViewBehavior.GanttViewElement = this;
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
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
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

    public LinkTypeConverter LinkTypeConverter
    {
      get
      {
        return this.linkTypeConverter;
      }
      set
      {
        if (this.linkTypeConverter == value)
          return;
        this.linkTypeConverter = value;
        this.linkTypeConverter.GanttViewElement = this;
      }
    }

    public GanttViewDataItem SelectedItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        if (this.selectedItem == value)
          return;
        if (value != null)
          this.ProcessSelection(value);
        else
          this.ProcessCurrentItem(value);
      }
    }

    public GanttViewLinkDataItem SelectedLink
    {
      get
      {
        return this.selectedLink;
      }
      set
      {
        if (this.selectedLink == value)
          return;
        this.ProcessSelection(value);
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the current column.")]
    public GanttViewTextViewColumn CurrentColumn
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
          this.currentColumn.Current = false;
        this.currentColumn = value;
        if (this.currentColumn != null)
          this.currentColumn.Current = true;
        this.OnNotifyPropertyChanged(nameof (CurrentColumn));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.filterDescriptors;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IComparer<GanttViewDataItem> Comparer
    {
      get
      {
        return this.comparer;
      }
      set
      {
        if (this.comparer == value)
          return;
        this.comparer = value;
        this.Update(RadGanttViewElement.UpdateActions.Resume);
        this.OnNotifyPropertyChanged(nameof (Comparer));
      }
    }

    public bool IsDataBound
    {
      get
      {
        return this.DataSource != null;
      }
    }

    public IGanttViewDataProvider DataProvider
    {
      get
      {
        if (this.dataProvider != null && this.dataProvider.IsAlive)
          return this.dataProvider.Target as IGanttViewDataProvider;
        return (IGanttViewDataProvider) null;
      }
      set
      {
        if (value == null)
        {
          this.dataProvider = (WeakReference) null;
          this.DataSource = (object) null;
        }
        else
        {
          if (this.dataProvider != null && this.dataProvider.IsAlive && this.dataProvider.Target.Equals((object) value))
            return;
          this.dataProvider = new WeakReference((object) value, false);
          this.DataSource = (object) null;
          foreach (GanttViewTextViewColumn column in value.Columns)
            this.Columns.Add(column);
          this.TaskDataMember = value.TaskDataMember;
          this.ChildMember = value.ChildMember;
          this.ParentMember = value.ParentMember;
          this.TitleMember = value.TitleMember;
          this.StartMember = value.StartMember;
          this.EndMember = value.EndMember;
          this.ProgressMember = value.ProgressMember;
          this.LinkDataMember = value.LinkDataMember;
          this.LinkStartMember = value.LinkStartMember;
          this.LinkEndMember = value.LinkEndMember;
          this.LinkTypeMember = value.LinkTypeMember;
          this.DataSource = value.DataSource;
          DateTime dateTime1 = DateTime.MaxValue;
          DateTime dateTime2 = DateTime.MinValue;
          if (this.Items.Count == 0)
          {
            dateTime1 = DateTime.Now;
            dateTime2 = DateTime.Now.AddDays(1.0);
          }
          foreach (GanttViewDataItem ganttViewDataItem in (Collection<GanttViewDataItem>) this.Items)
          {
            if (dateTime1 > ganttViewDataItem.Start)
              dateTime1 = ganttViewDataItem.Start;
            if (dateTime2 < ganttViewDataItem.End)
              dateTime2 = ganttViewDataItem.End;
          }
          TimeSpan onePixelTime = this.GraphicalViewElement.OnePixelTime;
          this.GraphicalViewElement.TimelineEnd = dateTime2.AddDays(1.0);
          this.GraphicalViewElement.TimelineStart = dateTime1.AddDays(-1.0);
          this.GraphicalViewElement.OnePixelTime = onePixelTime;
        }
      }
    }

    public object DataSource
    {
      get
      {
        return this.bindingProvider.DataSource;
      }
      set
      {
        if (this.SelectedItem != null)
          this.SelectedItem = (GanttViewDataItem) null;
        if (this.SelectedLink != null)
          this.SelectedLink = (GanttViewLinkDataItem) null;
        this.bindingProvider.DataSource = value;
      }
    }

    public string TaskDataMember
    {
      get
      {
        return this.bindingProvider.TaskDataMember;
      }
      set
      {
        this.bindingProvider.TaskDataMember = value;
      }
    }

    public string ParentMember
    {
      get
      {
        return this.bindingProvider.ParentMember;
      }
      set
      {
        this.bindingProvider.ParentMember = value;
      }
    }

    public string ChildMember
    {
      get
      {
        return this.bindingProvider.ChildMember;
      }
      set
      {
        this.bindingProvider.ChildMember = value;
      }
    }

    public string TitleMember
    {
      get
      {
        return this.bindingProvider.TitleMember;
      }
      set
      {
        this.bindingProvider.TitleMember = value;
      }
    }

    public string StartMember
    {
      get
      {
        return this.bindingProvider.StartMember;
      }
      set
      {
        this.bindingProvider.StartMember = value;
      }
    }

    public string EndMember
    {
      get
      {
        return this.bindingProvider.EndMember;
      }
      set
      {
        this.bindingProvider.EndMember = value;
      }
    }

    public string ProgressMember
    {
      get
      {
        return this.bindingProvider.ProgressMember;
      }
      set
      {
        this.bindingProvider.ProgressMember = value;
      }
    }

    public string LinkDataMember
    {
      get
      {
        return this.bindingProvider.LinkDataMember;
      }
      set
      {
        this.bindingProvider.LinkDataMember = value;
      }
    }

    public string LinkStartMember
    {
      get
      {
        return this.bindingProvider.LinkStartMember;
      }
      set
      {
        this.bindingProvider.LinkStartMember = value;
      }
    }

    public string LinkEndMember
    {
      get
      {
        return this.bindingProvider.LinkEndMember;
      }
      set
      {
        this.bindingProvider.LinkEndMember = value;
      }
    }

    public string LinkTypeMember
    {
      get
      {
        return this.bindingProvider.LinkTypeMember;
      }
      set
      {
        this.bindingProvider.LinkTypeMember = value;
      }
    }

    internal GanttViewBindingProvider BindingProvider
    {
      get
      {
        return this.bindingProvider;
      }
    }

    public virtual GanttViewDataItemProvider DataItemProvider
    {
      get
      {
        return (GanttViewDataItemProvider) this.bindingProvider;
      }
    }

    public bool DisableEnsureItemVisibleHorizontal
    {
      get
      {
        return this.disableEnsureItemVisibleHorizontal;
      }
      set
      {
        this.disableEnsureItemVisibleHorizontal = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or  a value indicating whether the control is in design mode.")]
    public bool IsInDesignMode
    {
      get
      {
        if (this.ElementTree != null && this.ElementTree.Control != null)
          return this.ElementTree.Control.Site != null;
        return false;
      }
    }

    [Description("Gets or sets a value indicating whether the default context menu may be shown.")]
    [DefaultValue(true)]
    public bool AllowDefaultContextMenu
    {
      get
      {
        return this.allowDefaultContextMenu;
      }
      set
      {
        this.allowDefaultContextMenu = value;
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

    public bool ShowTimelineTodayIndicator
    {
      get
      {
        return this.GraphicalViewElement.ShowTimelineTodayIndicator;
      }
      set
      {
        this.GraphicalViewElement.ShowTimelineTodayIndicator = value;
      }
    }

    public bool ShowTodayIndicator
    {
      get
      {
        return this.GraphicalViewElement.ShowTodayIndicator;
      }
      set
      {
        this.GraphicalViewElement.ShowTodayIndicator = value;
      }
    }

    public void Update(RadGanttViewElement.UpdateActions updateAction)
    {
      if (this.updateSuspendedCount > 0)
      {
        if (updateAction == RadGanttViewElement.UpdateActions.Reset)
          this.resumeAction = RadGanttViewElement.UpdateActions.Reset;
        if (this.previousEndUpdateAction != RadGanttViewElement.UpdateActions.StateChanged || updateAction != RadGanttViewElement.UpdateActions.ExpandedChanged)
          return;
        this.previousEndUpdateAction = RadGanttViewElement.UpdateActions.Resume;
      }
      else
      {
        this.TextViewElement.Update(updateAction);
        this.GraphicalViewElement.Update(updateAction);
        this.GraphicalViewElement.UpdateTextViewScroller();
      }
    }

    public void Update(
      RadGanttViewElement.UpdateActions updateAction,
      params GanttViewLinkDataItem[] items)
    {
      switch (updateAction)
      {
        case RadGanttViewElement.UpdateActions.LinkAdded:
          foreach (GanttViewLinkDataItem link in items)
          {
            if (link.StartItem != null && link.EndItem != null)
              this.GraphicalViewElement.CalculateLinkLines(link, new Point?());
          }
          break;
        case RadGanttViewElement.UpdateActions.LinkRemoved:
          foreach (GanttViewLinkDataItem viewLinkDataItem in items)
          {
            if (this.SelectedLink == viewLinkDataItem)
            {
              this.SelectedLink.Selected = false;
              this.SelectedLink = (GanttViewLinkDataItem) null;
            }
          }
          break;
      }
      this.GraphicalViewElement.Invalidate();
    }

    public void Update(
      RadGanttViewElement.UpdateActions updateAction,
      params GanttViewDataItem[] items)
    {
      if (this.updateSuspendedCount > 0)
        return;
      if (updateAction == RadGanttViewElement.UpdateActions.ExpandedChanged)
      {
        if (this.ElementState != ElementState.Loaded)
        {
          this.pendingScrollerUpdates = RadGanttViewElement.UpdateActions.Resume;
          return;
        }
        if (!this.UpdateOnExpandedChanged(items[0]))
          return;
      }
      if (updateAction == RadGanttViewElement.UpdateActions.ItemAdded)
      {
        if (this.ElementState != ElementState.Loaded)
        {
          this.pendingScrollerUpdates = RadGanttViewElement.UpdateActions.Resume;
          return;
        }
        this.UpdateScrollersOnAdd(items[0]);
        this.GraphicalViewElement.UpdateInnerState();
      }
      if (updateAction == RadGanttViewElement.UpdateActions.ItemRemoved || updateAction == RadGanttViewElement.UpdateActions.ItemMoved)
      {
        if (this.ElementState != ElementState.Loaded)
        {
          this.pendingScrollerUpdates = RadGanttViewElement.UpdateActions.Resume;
          return;
        }
        if (updateAction == RadGanttViewElement.UpdateActions.ItemRemoved)
        {
          foreach (GanttViewDataItem ganttViewDataItem in items)
          {
            foreach (GanttViewLinkDataItem link in (Collection<GanttViewLinkDataItem>) this.Links)
            {
              if (link.StartItem == ganttViewDataItem)
                link.StartItem = (GanttViewDataItem) null;
              else if (link.EndItem == ganttViewDataItem)
                link.EndItem = (GanttViewDataItem) null;
            }
          }
        }
        this.UpdateScrollers((GanttViewDataItem) null, updateAction);
        this.GraphicalViewElement.UpdateInnerState();
      }
      bool flag = true;
      GanttViewTextItemElement viewTextItemElement = (GanttViewTextItemElement) null;
      foreach (GanttViewTextItemElement child in this.TextViewElement.ViewElement.Children)
      {
        if (child.Data == items[0])
        {
          viewTextItemElement = child;
          break;
        }
      }
      if (viewTextItemElement != null && updateAction == RadGanttViewElement.UpdateActions.ItemStateChanged)
      {
        viewTextItemElement.Synchronize();
        flag = false;
      }
      GanttGraphicalViewBaseItemElement viewBaseItemElement = (GanttGraphicalViewBaseItemElement) null;
      foreach (GanttGraphicalViewBaseItemElement child in this.GraphicalViewElement.ViewElement.Children)
      {
        if (child.Data == items[0])
        {
          viewBaseItemElement = child;
          break;
        }
      }
      if (viewBaseItemElement != null && (updateAction == RadGanttViewElement.UpdateActions.ItemStateChanged || updateAction == RadGanttViewElement.UpdateActions.ItemEdited))
      {
        viewBaseItemElement.Synchronize();
        viewBaseItemElement.InvalidateMeasure(true);
        viewBaseItemElement.InvalidateArrange(true);
        flag = false;
      }
      if (!flag)
        return;
      this.Update(updateAction);
    }

    protected virtual bool UpdateOnExpandedChanged(GanttViewDataItem item)
    {
      this.TextViewElement.Scroller.UpdateScrollRange();
      this.GraphicalViewElement.Scroller.UpdateScrollRange();
      int newValue = this.GraphicalViewElement.Scroller.Scrollbar.Value;
      this.GraphicalViewElement.Scroller.Scrollbar.Value = 0;
      this.SetScrollValue(this.GraphicalViewElement.Scroller.Scrollbar, newValue);
      return true;
    }

    protected virtual void UpdateScrollersOnAdd(GanttViewDataItem item)
    {
      if (!this.IsItemVisible(item))
      {
        this.SynchronizeItemElements();
      }
      else
      {
        this.TextViewElement.Scroller.UpdateScrollRange();
        this.GraphicalViewElement.Scroller.UpdateScrollRange();
        this.SynchronizeItemElements();
      }
    }

    protected virtual void UpdateScrollers(
      GanttViewDataItem skipItem,
      RadGanttViewElement.UpdateActions updateAction)
    {
      this.TextViewElement.UpdateScrollers(updateAction);
      this.GraphicalViewElement.UpdateScrollers(updateAction);
    }

    protected virtual void SynchronizeItemElements()
    {
      this.TextViewElement.SynchronizeItemElements();
      this.GraphicalViewElement.SynchronizeItemElements();
    }

    protected internal virtual GanttViewDataItem CreateNewTask()
    {
      CreateGanttDataItemEventArgs e = new CreateGanttDataItemEventArgs();
      this.OnCreateDataItem(e);
      if (e.Item != null)
        return e.Item;
      return new GanttViewDataItem() { Title = "<New task>" };
    }

    protected internal virtual GanttViewLinkDataItem CreateNewLink()
    {
      CreateGanttLinkDataItemEventArgs e = new CreateGanttLinkDataItemEventArgs();
      this.OnCreateLinkDataItem(e);
      if (e.LinkDataItem != null)
        return e.LinkDataItem;
      return new GanttViewLinkDataItem();
    }

    public override BindingContext BindingContext
    {
      get
      {
        return base.BindingContext;
      }
      set
      {
        if (this.BindingContext == value)
          return;
        base.BindingContext = value;
        this.OnBindingContextChanged(EventArgs.Empty);
      }
    }

    public void BeginUpdate()
    {
      ++this.updateSuspendedCount;
    }

    public void EndUpdate()
    {
      this.EndUpdate(true, this.resumeAction);
      this.resumeAction = RadGanttViewElement.UpdateActions.Resume;
    }

    public void EndUpdate(bool performUpdate, RadGanttViewElement.UpdateActions action)
    {
      if (action < this.previousEndUpdateAction)
        this.previousEndUpdateAction = action;
      if (this.updateSuspendedCount == 0)
        return;
      --this.updateSuspendedCount;
      if (this.updateSuspendedCount != 0 || !performUpdate)
        return;
      if (action > this.previousEndUpdateAction)
        action = this.previousEndUpdateAction;
      if (action <= RadGanttViewElement.UpdateActions.ItemEdited)
        this.UpdateItems();
      if (this.ElementState == ElementState.Unloaded)
      {
        this.pendingScrollerUpdates = RadGanttViewElement.UpdateActions.Reset;
      }
      else
      {
        this.Update(action);
        if (this.itemToEnsureVisible == null)
          return;
        this.EnsureVisible(this.itemToEnsureVisible);
        this.itemToEnsureVisible = (GanttViewDataItem) null;
      }
    }

    protected internal virtual void ProcessSelection(GanttViewDataItem item)
    {
      if (item == null || this.updateSelectionChanged > 0 || item.Selected && item.Current)
        return;
      ++this.updateSelectionChanged;
      this.BeginUpdate();
      RadGanttViewElement.UpdateActions previousEndUpdateAction = this.previousEndUpdateAction;
      this.previousEndUpdateAction = RadGanttViewElement.UpdateActions.StateChanged;
      if (!item.Current)
      {
        ++this.updateSelectedItemChanged;
        if (!this.ProcessCurrentItem(item))
        {
          this.EndUpdate(false, RadGanttViewElement.UpdateActions.StateChanged);
          --this.updateSelectionChanged;
          --this.updateSelectedItemChanged;
          return;
        }
        --this.updateSelectedItemChanged;
      }
      this.EndUpdate(true, RadGanttViewElement.UpdateActions.StateChanged);
      --this.updateSelectionChanged;
      this.previousEndUpdateAction = previousEndUpdateAction;
      this.OnSelectedItemChanged(item);
    }

    protected internal virtual bool ProcessCurrentItem(GanttViewDataItem item)
    {
      if (item != null && !item.Enabled)
        return false;
      if (this.updateCurrentItemChanged > 0)
        return true;
      ++this.updateCurrentItemChanged;
      GanttViewSelectedItemChangingEventArgs e = new GanttViewSelectedItemChangingEventArgs(item);
      this.OnSelectedItemChanging(e);
      if (e.Cancel)
      {
        --this.updateCurrentItemChanged;
        return false;
      }
      this.ClearSelection();
      if (this.selectedItem != null)
        this.selectedItem.Current = false;
      this.selectedItem = item;
      if (this.BindingProvider != null)
        this.BindingProvider.SetCurrent(this.selectedItem);
      if (this.selectedItem != null)
      {
        this.selectedItem.Current = true;
        this.selectedItem.Selected = true;
        this.BringIntoView(this.selectedItem);
      }
      --this.updateCurrentItemChanged;
      this.Update(RadGanttViewElement.UpdateActions.StateChanged);
      this.OnSelectedItemChanged(item);
      return true;
    }

    protected internal virtual void ProcessSelection(GanttViewLinkDataItem link)
    {
      if (link != null && link.Selected)
        return;
      GanttViewSelectedLinkChangingEventArgs e = new GanttViewSelectedLinkChangingEventArgs(link);
      this.OnSelectedLinkChanging(e);
      if (e.Cancel)
        return;
      if (this.SelectedLink != null)
        this.SelectedLink.Selected = false;
      this.selectedLink = link;
      if (this.SelectedLink != null)
      {
        this.SelectedLink.Selected = true;
        this.SelectedLink.ShowItemsHandles();
      }
      this.OnSelectedLinkChanged(new GanttViewSelectedLinkChangedEventArgs(link));
    }

    public void EnsureVisible(GanttViewDataItem item)
    {
      GanttViewBaseItemElement element = this.GraphicalViewElement.GetElement(item);
      if (this.updateSuspendedCount > 0)
      {
        this.itemToEnsureVisible = item;
      }
      else
      {
        GanttViewBaseItemElement itemElement = this.EnsureItemVisibleVertical(item, element);
        this.EnsureItemVisibleHorizontal(item, itemElement);
        this.UpdateLayout();
      }
    }

    protected virtual GanttViewBaseItemElement EnsureItemVisibleVertical(
      GanttViewDataItem item,
      GanttViewBaseItemElement itemElement)
    {
      if (itemElement == null)
      {
        this.UpdateLayout();
        if (this.GraphicalViewElement.ViewElement.Children.Count > 0)
        {
          if (this.GetItemIndex(item) <= this.GetItemIndex(((GanttViewBaseItemElement) this.GraphicalViewElement.ViewElement.Children[0]).Data))
            this.GraphicalViewElement.Scroller.ScrollToItem(item);
          else
            itemElement = this.EnsureItemVisibleVerticalCore(item);
        }
      }
      else if (itemElement.ControlBoundingRectangle.Bottom > this.GraphicalViewElement.ViewElement.ControlBoundingRectangle.Bottom)
        this.SetScrollValue(this.GraphicalViewElement.VScrollBar, this.GraphicalViewElement.VScrollBar.Value + (itemElement.ControlBoundingRectangle.Bottom - this.graphicalViewElement.ViewElement.ControlBoundingRectangle.Bottom));
      else if (itemElement.ControlBoundingRectangle.Top < this.GraphicalViewElement.ViewElement.ControlBoundingRectangle.Top)
        this.SetScrollValue(this.GraphicalViewElement.VScrollBar, this.GraphicalViewElement.VScrollBar.Value - (this.GraphicalViewElement.ViewElement.ControlBoundingRectangle.Top - itemElement.ControlBoundingRectangle.Top));
      return itemElement;
    }

    protected virtual GanttViewBaseItemElement EnsureItemVisibleVerticalCore(
      GanttViewDataItem item)
    {
      bool flag = false;
      int num = 0;
      GanttViewDataItem data = ((GanttViewBaseItemElement) this.GraphicalViewElement.ViewElement.Children[this.GraphicalViewElement.ViewElement.Children.Count - 1]).Data;
      GanttViewTraverser enumerator = (GanttViewTraverser) this.GraphicalViewElement.Scroller.Traverser.GetEnumerator();
      GanttViewBaseItemElement viewBaseItemElement = (GanttViewBaseItemElement) null;
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == item)
        {
          int maximum = this.GraphicalViewElement.VScrollBar.Maximum;
          this.SetScrollValue(this.GraphicalViewElement.VScrollBar, this.GraphicalViewElement.VScrollBar.Value + num);
          this.UpdateLayout();
          viewBaseItemElement = this.GraphicalViewElement.GetElement(item);
          if (viewBaseItemElement != null && viewBaseItemElement.ControlBoundingRectangle.Bottom > this.GraphicalViewElement.ViewElement.ControlBoundingRectangle.Bottom)
          {
            this.EnsureVisible(item);
            break;
          }
          break;
        }
        if (enumerator.Current == data)
          flag = true;
        if (flag)
          num += (int) this.GraphicalViewElement.ViewElement.ElementProvider.GetElementSize(enumerator.Current).Height + this.ItemSpacing;
      }
      return viewBaseItemElement;
    }

    protected virtual void EnsureItemVisibleHorizontal(
      GanttViewDataItem item,
      GanttViewBaseItemElement itemElement)
    {
    }

    public void BringIntoView(GanttViewDataItem item)
    {
      if (item == null)
        return;
      RadGanttViewElement.UpdateActions action = RadGanttViewElement.UpdateActions.StateChanged;
      this.BeginUpdate();
      for (GanttViewDataItem parent = item.Parent; parent != null; parent = parent.Parent)
      {
        if (!parent.Expanded)
        {
          parent.Expand();
          action = RadGanttViewElement.UpdateActions.Resume;
        }
      }
      this.EndUpdate(true, action);
      this.EnsureVisible(item);
    }

    public void ClearSelection()
    {
      if (this.selectedItem != null)
      {
        this.selectedItem.Current = false;
        this.selectedItem.Selected = false;
      }
      this.SelectedItem = (GanttViewDataItem) null;
    }

    public virtual bool BeginEdit()
    {
      if (this.ReadOnly || this.IsEditing || (this.SelectedItem == null || this.CurrentColumn == null))
        return false;
      GanttViewDataItem selectedItem = this.SelectedItem;
      if (!selectedItem.Enabled || selectedItem.ReadOnly || !this.AllowSummaryEditing && selectedItem.Items.Count > 0 && (this.CurrentColumn.FieldName == this.StartMember || this.CurrentColumn.FieldName == this.EndMember || this.CurrentColumn.FieldName == this.ProgressMember))
        return false;
      System.Type editorType = this.GetEditorType(selectedItem, this.CurrentColumn);
      GanttViewEditorRequiredEventArgs e1 = new GanttViewEditorRequiredEventArgs(selectedItem, this.CurrentColumn, editorType);
      this.OnEditorRequired(e1);
      IInputEditor editor = e1.Editor ?? this.GetEditor(e1.EditorType);
      if (editor == null)
        return false;
      this.activeEditor = editor;
      GanttViewItemEditingEventArgs e2 = new GanttViewItemEditingEventArgs(selectedItem, this.CurrentColumn, editor);
      this.OnItemEditing(e2);
      if (e2.Cancel)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      if (this.TextViewElement.GetElement(selectedItem) == null)
        this.EnsureVisible(selectedItem);
      GanttViewTextItemElement element = this.TextViewElement.GetElement(selectedItem) as GanttViewTextItemElement;
      if (element == null)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      GanttViewTextViewCellElement cellElement = element.GetCellElement(this.CurrentColumn);
      if (cellElement == null)
      {
        this.activeEditor = (IInputEditor) null;
        return false;
      }
      cellElement.AddEditor(editor);
      ISupportInitialize activeEditor1 = this.activeEditor as ISupportInitialize;
      activeEditor1?.BeginInit();
      object obj = selectedItem[this.CurrentColumn];
      this.activeEditor.Initialize((object) element, obj);
      activeEditor1?.EndInit();
      this.OnEditorInitialized(new GanttViewItemEditorInitializedEventArgs(selectedItem, editor));
      RadControl radControl = this.ElementTree == null || this.ElementTree.Control == null ? (RadControl) null : this.ElementTree.Control as RadControl;
      if (radControl != null && TelerikHelper.IsMaterialTheme(radControl.ThemeName))
      {
        BaseInputEditor activeEditor2 = this.activeEditor as BaseInputEditor;
        if (activeEditor2 != null)
          activeEditor2.EditorElement.StretchVertically = true;
      }
      this.activeEditor.BeginEdit();
      this.cachedOldValue = obj;
      return true;
    }

    public bool EndEdit()
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Edited");
      return this.EndEditCore(true);
    }

    public void CancelEdit()
    {
      this.EndEditCore(false);
    }

    protected virtual bool EndEditCore(bool commitChanges)
    {
      this.GanttViewBehavior.ClickTimer.Stop();
      if (!this.IsEditing)
        return false;
      GanttViewDataItem selectedItem = this.SelectedItem;
      if (selectedItem == null)
        return false;
      GanttViewTextItemElement element = this.TextViewElement.GetElement(selectedItem) as GanttViewTextItemElement;
      if (element == null)
        return false;
      GanttViewTextViewCellElement cellElement = element.GetCellElement(this.CurrentColumn);
      if (commitChanges)
      {
        GanttViewItemValidatingEventArgs e = new GanttViewItemValidatingEventArgs(selectedItem, this.CurrentColumn, this.activeEditor.Value, selectedItem[this.CurrentColumn]);
        this.OnItemValidating(e);
        if (e.Cancel)
          return false;
        this.OnItemValidated(new GanttViewItemValidatedEventArgs(selectedItem, this.CurrentColumn));
        if (selectedItem != null)
          selectedItem[this.CurrentColumn] = this.activeEditor.Value;
      }
      if (this.activeEditor != null)
        this.activeEditor.EndEdit();
      cellElement.RemoveEditor(this.activeEditor);
      this.GraphicalViewElement.UpdateInnerState();
      this.InvalidateMeasure(true);
      this.InvalidateArrange(true);
      this.UpdateLayout();
      this.OnItemEdited(new GanttViewItemEditedEventArgs(selectedItem, this.activeEditor, !commitChanges));
      this.activeEditor = (IInputEditor) null;
      return true;
    }

    private System.Type GetEditorType(GanttViewDataItem item, GanttViewTextViewColumn column)
    {
      System.Type type = (System.Type) null;
      if ((object) column.DataType != null)
        type = column.DataType;
      else if (this.IsDataBound)
      {
        PropertyDescriptor boundProperty = this.BindingProvider.BoundProperties[column.FieldName];
        if (boundProperty != null)
          type = boundProperty.PropertyType;
      }
      else
      {
        PropertyInfo property = item.GetType().GetProperty(column.FieldName);
        if ((object) property != null)
          type = property.PropertyType;
      }
      if (this.IsNumericType(type))
        return typeof (GanttViewSpinEditor);
      if ((object) type == (object) typeof (DateTime))
        return typeof (GanttViewDateTimeEditor);
      return typeof (GanttViewTextBoxEditor);
    }

    private bool IsNumericType(System.Type type)
    {
      if ((object) type == null || type.IsArray)
        return false;
      switch (System.Type.GetTypeCode(type))
      {
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return true;
        default:
          return false;
      }
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

    private int GetItemIndex(GanttViewDataItem position)
    {
      if (this.root.Items.Count == 0)
        return -1;
      int num = 0;
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this);
      while (ganttViewTraverser.MoveNext() && ganttViewTraverser.Current != position)
        ++num;
      return num;
    }

    private void SetScrollValue(RadScrollBarElement scrollbar, int newValue)
    {
      if (newValue > scrollbar.Maximum - scrollbar.LargeChange + 1)
        newValue = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (newValue < scrollbar.Minimum)
        newValue = scrollbar.Minimum;
      scrollbar.Value = newValue;
    }

    private bool IsItemVisible(GanttViewDataItem item)
    {
      if (!item.Visible && !this.IsInDesignMode)
        return false;
      for (item = item.Parent; item != null; item = item.Parent)
      {
        if (!item.Expanded || !item.Visible && !this.IsInDesignMode)
          return false;
      }
      return true;
    }

    private void UpdateItems()
    {
      if (this.IsSuspended)
        return;
      Stack<GanttViewDataItemCollection> dataItemCollectionStack = new Stack<GanttViewDataItemCollection>();
      dataItemCollectionStack.Push(this.root.Items);
      this.root.Items.Update();
      while (dataItemCollectionStack.Count > 0)
      {
        GanttViewDataItemCollection dataItemCollection = dataItemCollectionStack.Pop();
        for (int index = 0; index < dataItemCollection.Count; ++index)
        {
          if (dataItemCollection[index].Items != null)
          {
            dataItemCollection[index].Items.Update();
            dataItemCollectionStack.Push(dataItemCollection[index].Items);
          }
        }
      }
    }

    internal bool IsSuspended
    {
      get
      {
        return this.updateSuspendedCount > 0;
      }
    }

    internal bool PassesFilter(GanttViewDataItem node)
    {
      return this.filterPredicate(node);
    }

    internal bool PreProcess(
      GanttViewDataItem parent,
      GanttViewDataItem item,
      params object[] metadata)
    {
      if (this.bindingProvider.IsDataBound && !this.bindingProvider.PreProcess(parent, item, metadata))
        return false;
      if (!this.IsSuspended && metadata != null)
      {
        string str = (string) metadata[0];
        if (str == "Add" || str == "Insert")
        {
          item.GanttViewElement = this;
          GanttViewItemAddingEventArgs e = new GanttViewItemAddingEventArgs(item);
          this.OnItemAdding(e);
          if (this.bindingProvider.IsDataBound)
            this.bindingProvider.PostProcess(parent, item, e.Cancel ? (object) "Remove" : (object) "EndEdit");
          if (e.Cancel)
          {
            item.GanttViewElement = (RadGanttViewElement) null;
            return false;
          }
        }
      }
      return true;
    }

    internal bool PreProcess(GanttViewLinkDataItem item, params object[] metadata)
    {
      if (this.bindingProvider.IsDataBound && !this.bindingProvider.PreProcess(item, metadata))
        return false;
      if (!this.IsSuspended && metadata != null)
      {
        string str = (string) metadata[0];
        if (str == "Add" || str == "Insert")
        {
          item.GanttViewElement = this;
          GanttViewLinkAddingEventArgs e = new GanttViewLinkAddingEventArgs(item);
          this.OnLinkAdding(e);
          if (this.bindingProvider.IsDataBound)
            this.bindingProvider.PostProcess(item, e.Cancel ? (object) "Remove" : (object) "EndEdit");
          if (e.Cancel)
          {
            item.GanttViewElement = (RadGanttViewElement) null;
            return false;
          }
        }
      }
      return true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left || !this.SplitterElement.IsMouseOver)
        return;
      this.ProcessSelection((GanttViewLinkDataItem) null);
      this.Capture = true;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Capture)
        return;
      this.Ratio = (float) e.X / (float) this.Size.Width;
      this.InvalidateMeasure();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Capture = false;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.TextViewElement.ViewElement.ElementProvider.ClearCache();
      this.GraphicalViewElement.ViewElement.ElementProvider.ClearCache();
      if (this.pendingScrollerUpdates == RadGanttViewElement.UpdateActions.None)
        return;
      this.Update(this.pendingScrollerUpdates);
      this.pendingScrollerUpdates = RadGanttViewElement.UpdateActions.None;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      SizeF availableSize1 = new SizeF((float) this.SplitterWidth, clientRectangle.Height);
      SizeF availableSize2 = new SizeF(clientRectangle.Width * this.Ratio - (float) (this.SplitterWidth / 2), clientRectangle.Height);
      SizeF availableSize3 = new SizeF(clientRectangle.Width - availableSize2.Width - (float) this.SplitterWidth, clientRectangle.Height);
      if (float.IsPositiveInfinity(clientRectangle.Height))
      {
        availableSize1.Height = float.PositiveInfinity;
        availableSize2.Height = float.PositiveInfinity;
        availableSize3.Height = float.PositiveInfinity;
      }
      if (float.IsPositiveInfinity(clientRectangle.Width))
      {
        availableSize2.Width = float.PositiveInfinity;
        availableSize3.Width = float.PositiveInfinity;
      }
      this.TextViewElement.Measure(availableSize2);
      this.SplitterElement.Measure(availableSize1);
      this.GraphicalViewElement.Measure(availableSize3);
      return new SizeF(this.TextViewElement.DesiredSize.Width + (float) this.SplitterWidth + this.GraphicalViewElement.DesiredSize.Width, this.TextViewElement.DesiredSize.Height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF finalRect1 = new RectangleF(clientRectangle.X, clientRectangle.Y, (float) Math.Round((double) clientRectangle.Width * (double) this.Ratio - (double) this.SplitterWidth / 2.0), clientRectangle.Height);
      RectangleF finalRect2 = new RectangleF(finalRect1.Right, clientRectangle.Y, (float) this.SplitterWidth, clientRectangle.Height);
      RectangleF finalRect3 = new RectangleF(finalRect2.Right, clientRectangle.Y, clientRectangle.Width - finalRect1.Width - (float) this.SplitterWidth, clientRectangle.Height);
      this.TextViewElement.Arrange(finalRect1);
      this.SplitterElement.Arrange(finalRect2);
      this.GraphicalViewElement.Arrange(finalRect3);
      return new SizeF(finalRect1.Width + finalRect2.Width + finalRect3.Width, finalRect1.Height + finalRect2.Height + finalRect3.Height);
    }

    public event GanttViewItemChildIdNeededEventHandler ItemChildIdNeeded;

    protected internal virtual void OnItemChildIdNeeded(GanttViewItemChildIdNeededEventArgs e)
    {
      if (this.ItemChildIdNeeded == null)
        return;
      this.ItemChildIdNeeded((object) this, e);
    }

    public event GanttViewItemPaintEventHandler ItemPaint;

    protected internal virtual void OnItemPaint(GanttViewItemPaintEventArgs e)
    {
      if (this.ItemPaint == null)
        return;
      this.ItemPaint((object) this, e);
    }

    public event GanttViewContextMenuOpeningEventHandler ContextMenuOpening;

    protected internal virtual void OnContextMenuOpening(GanttViewContextMenuOpeningEventArgs e)
    {
      if (this.ContextMenuOpening == null)
        return;
      this.ContextMenuOpening((object) this, e);
    }

    public event EventHandler BindingContextChanged;

    protected internal virtual void OnBindingContextChanged(EventArgs e)
    {
      EventHandler bindingContextChanged = this.BindingContextChanged;
      if (bindingContextChanged != null)
        bindingContextChanged((object) this, e);
      this.bindingProvider.Reset();
      foreach (GanttViewTextViewColumn column in (Collection<GanttViewTextViewColumn>) this.Columns)
        column.Initialize();
    }

    public event CreateGanttDataItemEventHandler CreateDataItem;

    protected internal virtual void OnCreateDataItem(CreateGanttDataItemEventArgs e)
    {
      if (this.CreateDataItem == null)
        return;
      this.CreateDataItem((object) this, e);
    }

    public event CreateGanttLinkDataItemEventHandler CreateLinkDataItem;

    protected internal virtual void OnCreateLinkDataItem(CreateGanttLinkDataItemEventArgs e)
    {
      if (this.CreateLinkDataItem == null)
        return;
      this.CreateLinkDataItem((object) this, e);
    }

    public event GanttItemAddingEventHandler ItemAdding;

    protected internal virtual void OnItemAdding(GanttViewItemAddingEventArgs e)
    {
      if (this.ItemAdding == null)
        return;
      this.ItemAdding((object) this, e);
    }

    public event GanttLinkAddingEventHandler LinkAdding;

    protected internal virtual void OnLinkAdding(GanttViewLinkAddingEventArgs e)
    {
      if (this.LinkAdding == null)
        return;
      this.LinkAdding((object) this, e);
    }

    public event GanttItemDataErrorEventHandler ItemDataError;

    protected internal virtual void OnItemDataError(GanttViewItemDataErrorEventArgs e)
    {
      if (this.ItemDataError == null)
        return;
      this.ItemDataError((object) this, e);
    }

    public event GanttLinkDataErrorEventHandler LinkDataError;

    protected internal virtual void OnLinkDataError(GanttViewLinkDataErrorEventArgs e)
    {
      if (this.LinkDataError == null)
        return;
      this.LinkDataError((object) this, e);
    }

    public event GanttViewSelectedItemChangingEventHandler SelectedItemChanging;

    protected internal virtual void OnSelectedItemChanging(GanttViewSelectedItemChangingEventArgs e)
    {
      if (this.SelectedItemChanging == null)
        return;
      this.SelectedItemChanging((object) this, e);
    }

    public event GanttViewSelectedLinkChangingEventHandler SelectedLinkChanging;

    protected internal virtual void OnSelectedLinkChanging(GanttViewSelectedLinkChangingEventArgs e)
    {
      if (this.SelectedLinkChanging == null)
        return;
      this.SelectedLinkChanging((object) this, e);
    }

    public event GanttViewSelectedItemChangedEventHandler SelectedItemChanged;

    protected internal virtual void OnSelectedItemChanged(GanttViewSelectedItemChangedEventArgs e)
    {
      if (this.SelectedItemChanged == null)
        return;
      this.SelectedItemChanged((object) this, e);
    }

    public event GanttViewSelectedLinkChangedEventHandler SelectedLinkChanged;

    protected internal virtual void OnSelectedLinkChanged(GanttViewSelectedLinkChangedEventArgs e)
    {
      if (this.SelectedLinkChanged == null)
        return;
      this.SelectedLinkChanged((object) this, e);
    }

    private void OnSelectedItemChanged(GanttViewDataItem item)
    {
      if (this.updateSelectedItemChanged > 0)
        return;
      this.OnNotifyPropertyChanged("SelectedItem");
      this.OnSelectedItemChanged(new GanttViewSelectedItemChangedEventArgs(item));
    }

    public event GanttViewExpandedChangingEventHandler ItemExpandedChanging;

    protected internal bool OnItemExpandedChanging(GanttViewDataItem item)
    {
      GanttViewExpandedChangingEventArgs e = new GanttViewExpandedChangingEventArgs(item);
      this.OnItemExpandedChanging(e);
      return !e.Cancel;
    }

    protected internal virtual void OnItemExpandedChanging(GanttViewExpandedChangingEventArgs e)
    {
      if (this.ItemExpandedChanging == null)
        return;
      this.ItemExpandedChanging((object) this, e);
    }

    public event GanttViewExpandedChangedEventHandler ItemExpandedChanged;

    protected internal virtual void OnItemExpandedChanged(GanttViewExpandedChangedEventArgs e)
    {
      if (this.ItemExpandedChanged == null)
        return;
      this.ItemExpandedChanged((object) this, e);
    }

    public event GanttViewItemDataBoundEventHandler ItemDataBound;

    protected internal virtual void OnItemDataBound(GanttViewItemDataBoundEventArgs e)
    {
      if (this.ItemDataBound == null)
        return;
      this.ItemDataBound((object) this, e);
    }

    public event GanttViewItemAddedEventHandler ItemAdded;

    protected internal virtual void OnItemAdded(GanttViewItemAddedEventArgs e)
    {
      if (this.ItemAdded == null)
        return;
      this.ItemAdded((object) this, e);
    }

    public event GanttViewItemRemovedEventHandler ItemRemoved;

    protected internal virtual void OnItemRemoved(GanttViewItemRemovedEventArgs e)
    {
      if (this.ItemRemoved == null)
        return;
      this.ItemRemoved((object) this, e);
    }

    public event GanttViewItemChangedEventhandler ItemChanged;

    protected internal virtual void OnItemChanged(GanttViewItemChangedEventArgs e)
    {
      if (this.ItemChanged == null)
        return;
      this.ItemChanged((object) this, e);
    }

    public event GanttViewLinkDataBoundEventHandler LinkDataBound;

    protected internal virtual void OnLinkDataBound(GanttViewLinkDataBoundEventArgs e)
    {
      if (this.LinkDataBound == null)
        return;
      this.LinkDataBound((object) this, e);
    }

    public event GanttViewLinkAddedEventHandler LinkAdded;

    protected internal virtual void OnLinkAdded(GanttViewLinkAddedEventArgs e)
    {
      if (this.LinkAdded == null)
        return;
      this.LinkAdded((object) this, e);
    }

    public event GanttViewLinkRemovedEventHandler LinkRemoved;

    protected internal virtual void OnLinkRemoved(GanttViewLinkRemovedEventArgs e)
    {
      if (this.LinkRemoved == null)
        return;
      this.LinkRemoved((object) this, e);
    }

    public event GanttViewHeaderCellElementCreatingEventHandler HeaderCellElementCreating;

    protected internal virtual void OnHeaderCellCreating(
      GanttViewHeaderCellElementCreatingEventArgs e)
    {
      if (this.HeaderCellElementCreating == null)
        return;
      this.HeaderCellElementCreating((object) this, e);
    }

    public event GanttViewDataCellElementCreatingEventHandler DataCellElementCreating;

    protected internal virtual void OnDataCellCreating(GanttViewDataCellElementCreatingEventArgs e)
    {
      if (this.DataCellElementCreating == null)
        return;
      this.DataCellElementCreating((object) this, e);
    }

    public event GanttViewTextViewCellFormattingEventHandler TextViewCellFormatting;

    protected internal virtual void OnTextViewCellFormatting(
      GanttViewTextViewCellFormattingEventArgs e)
    {
      if (this.TextViewCellFormatting == null)
        return;
      this.TextViewCellFormatting((object) this, e);
    }

    public event GanttViewTextViewItemFormattingEventHandler TextViewItemFormatting;

    protected internal virtual void OnTextViewItemFormatting(
      GanttViewTextViewItemFormattingEventArgs e)
    {
      if (this.TextViewItemFormatting == null)
        return;
      this.TextViewItemFormatting((object) this, e);
    }

    public event GanttViewTimelineItemFormattingEventHandler TimelineItemFormatting;

    protected internal virtual void OnTimelineItemFormatting(
      GanttViewTimelineItemFormattingEventArgs e)
    {
      if (this.TimelineItemFormatting == null)
        return;
      this.TimelineItemFormatting((object) this, e);
    }

    public event GanttViewGraphicalViewItemFormattingEventHandler GraphicalViewItemFormatting;

    protected internal virtual void OnGraphicalViewItemFormatting(
      GanttViewGraphicalViewItemFormattingEventArgs e)
    {
      if (this.GraphicalViewItemFormatting == null)
        return;
      this.GraphicalViewItemFormatting((object) this, e);
    }

    public event GanttViewLinkItemFormattingEventHandler GraphicalViewLinkItemFormatting;

    protected internal virtual void OnGraphicalViewLinkItemFormatting(
      GanttViewLinkItemFormattingEventArgs e)
    {
      if (this.GraphicalViewLinkItemFormatting == null)
        return;
      this.GraphicalViewLinkItemFormatting((object) this, e);
    }

    public event GanttViewItemElementCreatingEventHandler ItemElementCreating;

    protected internal virtual void OnItemElementCreating(GanttViewItemElementCreatingEventArgs e)
    {
      if (this.ItemElementCreating == null)
        return;
      this.ItemElementCreating((object) this, e);
    }

    public event GanttViewTimelineItemElementCreatingEventHandler TimelineItemElementCreating;

    protected internal virtual void OnTimelineItemElementCreating(
      GanttViewTimelineItemElementCreatingEventArgs e)
    {
      if (this.TimelineItemElementCreating == null)
        return;
      this.TimelineItemElementCreating((object) this, e);
    }

    public event GanttViewEditorRequiredEventHandler EditorRequired;

    protected internal virtual void OnEditorRequired(GanttViewEditorRequiredEventArgs e)
    {
      if (this.EditorRequired == null)
        return;
      this.EditorRequired((object) this, e);
    }

    public event GanttViewItemEditingEventHandler ItemEditing;

    protected internal virtual void OnItemEditing(GanttViewItemEditingEventArgs e)
    {
      if (this.ItemEditing == null)
        return;
      this.ItemEditing((object) this, e);
    }

    public event GanttViewItemEditorInitializedEventHandler EditorInitialized;

    protected internal virtual void OnEditorInitialized(GanttViewItemEditorInitializedEventArgs e)
    {
      if (this.EditorInitialized == null)
        return;
      this.EditorInitialized((object) this, e);
    }

    public event GanttViewItemValidatingEventHandler ItemValidating;

    protected internal virtual void OnItemValidating(GanttViewItemValidatingEventArgs e)
    {
      if (this.ItemValidating == null)
        return;
      this.ItemValidating((object) this, e);
    }

    public event GanttViewItemValidatedEventHandler ItemValidated;

    protected internal virtual void OnItemValidated(GanttViewItemValidatedEventArgs e)
    {
      if (this.ItemValidated == null)
        return;
      this.ItemValidated((object) this, e);
    }

    public event GanttViewItemEditedEventHandler ItemEdited;

    protected internal virtual void OnItemEdited(GanttViewItemEditedEventArgs e)
    {
      if (this.ItemEdited == null)
        return;
      this.ItemEdited((object) this, e);
    }

    public event EventHandler RootCreated;

    protected internal virtual void OnRootCreated(EventArgs e)
    {
      if (this.RootCreated == null)
        return;
      this.RootCreated((object) this, e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property.Name == "ItemSpacing")
      {
        this.TextViewElement.ItemSpacing = (int) e.NewValue;
        this.GraphicalViewElement.ItemSpacing = this.TextViewElement.ItemSpacing;
        this.GraphicalViewElement.UpdateInnerState();
        this.GraphicalViewElement.UpdateTextViewScroller();
      }
      else
      {
        if (!(e.Property.Name == "ItemHeight"))
          return;
        this.GraphicalViewElement.UpdateInnerState();
      }
    }

    public virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseDown(e);
    }

    public virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseMove(e);
    }

    public virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseUp(e);
    }

    public virtual bool ProcessMouseClick(MouseEventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseClick(e);
    }

    public virtual bool ProcessDoubleClick(MouseEventArgs e)
    {
      return this.GanttViewBehavior.ProcessDoubleClick(e);
    }

    public virtual bool ProcessMouseEnter(EventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseEnter(e);
    }

    public virtual bool ProcessMouseLeave(EventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseLeave(e);
    }

    public virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseWheel(e);
    }

    public virtual bool ProcessMouseHover(EventArgs e)
    {
      return this.GanttViewBehavior.ProcessMouseHover(e);
    }

    public virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      return this.GanttViewBehavior.ProcessKeyDown(e);
    }

    public virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      return this.GanttViewBehavior.ProcessKeyPress(e);
    }

    public virtual bool ProcessKeyUp(KeyEventArgs e)
    {
      return this.GanttViewBehavior.ProcessKeyUp(e);
    }

    internal class RootDataItem : GanttViewDataItem
    {
      public RootDataItem(RadGanttViewElement ganttView)
      {
        this.Title = "RootTask";
        this.GanttViewElement = ganttView;
        this.Expanded = true;
      }
    }

    public enum UpdateActions
    {
      None,
      Reset,
      Resume,
      ItemAdded,
      ItemRemoved,
      ItemMoved,
      ItemEdited,
      LinkAdded,
      LinkRemoved,
      ExpandedChanged,
      StateChanged,
      ItemStateChanged,
    }
  }
}
