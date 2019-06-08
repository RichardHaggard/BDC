// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCarouselElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadCarouselElement : RadItem, IDisposable
  {
    internal static readonly object SelectedIndexChangedEventKey = new object();
    internal static readonly object SelectedValueChangedEventKey = new object();
    internal static readonly object NewCarouselItemCreatingEventKey = new object();
    internal static readonly object SelectedItemChangedEventKey = new object();
    internal static readonly object ItemDataBoundEventKey = new object();
    private static readonly object ItemLeavingEventKey = new object();
    private static readonly object ItemEnteringEventKey = new object();
    private CarouselItemClickAction itemClickDefaultAction = CarouselItemClickAction.SelectItem;
    private int oldIndex = -1;
    private double itemReflectionPercentage = 0.333;
    private Size navigationButtonsOffset = new Size(0, 0);
    private NavigationButtonsPosition buttonsPositon = NavigationButtonsPosition.Bottom;
    private int autoLoopPauseInterval = 3;
    private int autoAnimationCount = -1;
    private RadEasingType? originalEasingType = new RadEasingType?();
    private RadEasingType orignalEasing = RadEasingType.Default;
    private const int autoloopTimerInterval = 200;
    private FillPrimitive backgroundPrimitive;
    private BorderPrimitive borderPrimitive;
    private CarouselItemsContainer carouselItemContainer;
    private RadItem selectedItem;
    private int selectedIndex;
    private BindingMemberInfo valueMember;
    private bool caseSensitive;
    private bool enableKeyboardNavigation;
    private bool clearItemsSilently;
    private int updateCount;
    private bool dataSourceDisposing;
    private CurrencyManager dataManager;
    private object dataSource;
    private bool inSetDataConnection;
    private bool isDataSourceInitEventHooked;
    private bool isDataSourceInitialized;
    private RadItemCollection boundItems;
    private bool formattingEnabled;
    private RadRepeatButtonElement btnPrev;
    private RadRepeatButtonElement btnNext;
    private Timer timer;
    private int suspendAutoLoopTicks;
    private object hoveredItem;
    private bool autoLoopPaused;
    private double totalPanOffset;
    private int originalAnimationDelay;

    public event EventHandler AnimationStarted;

    public event EventHandler AnimationFinished;

    public CarouselItemsContainer CarouselItemContainer
    {
      get
      {
        return this.carouselItemContainer;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.boundItems = new RadItemCollection();
      this.boundItems.ItemsChanged += new ItemChangedDelegate(this.boundItems_ItemsChanged);
      this.BypassLayoutPolicies = true;
      BindingContext bindingContext = this.BindingContext;
      this.CanFocus = true;
    }

    protected override void CreateChildElements()
    {
      if (this.backgroundPrimitive == null)
      {
        this.backgroundPrimitive = new FillPrimitive();
        this.Children.Insert(0, (RadElement) this.backgroundPrimitive);
      }
      if (this.borderPrimitive == null)
      {
        this.borderPrimitive = new BorderPrimitive();
        this.borderPrimitive.ZIndex = 12;
        this.Children.Insert(1, (RadElement) this.borderPrimitive);
      }
      if (this.carouselItemContainer == null)
      {
        this.carouselItemContainer = new CarouselItemsContainer(this);
        this.carouselItemContainer.ZIndex = 5;
        this.Children.Insert(2, (RadElement) this.carouselItemContainer);
        this.carouselItemContainer.Items.ItemsChanged += new ItemChangedDelegate(this.ContainerItems_ItemsChanged);
      }
      this.btnPrev = new RadRepeatButtonElement("Previous");
      this.btnPrev.Class = "PreviousButton";
      this.btnPrev.ImagePrimitive.Class = "PreviousButtonImage";
      this.btnPrev.ZIndex = 10;
      this.btnPrev.Click += new EventHandler(this.btnPrev_Click);
      this.btnPrev.Interval = 500;
      this.Children.Add((RadElement) this.btnPrev);
      this.btnNext = new RadRepeatButtonElement("Next");
      this.btnNext.Class = "NextButton";
      this.btnNext.ImagePrimitive.Class = "NextButtonImage";
      this.btnNext.Click += new EventHandler(this.btnNext_Click);
      this.btnNext.Interval = 500;
      this.btnNext.ZIndex = 11;
      this.Children.Add((RadElement) this.btnNext);
      base.CreateChildElements();
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.CallOnSelectedIndexChanged(new SelectedIndexChangedEventArgs(this.oldIndex, this.selectedIndex));
      this.CallOnSelectedItemChanged(EventArgs.Empty);
    }

    protected internal override bool IsInputKey(InputKeyEventArgs e)
    {
      if (e.Keys != Keys.Left && e.Keys != Keys.Right && e.Keys != Keys.Return)
        return false;
      e.Handled = true;
      this.ProcessOnKeyDown(new KeyEventArgs(e.Keys));
      return true;
    }

    protected virtual void ProcessOnKeyDown(KeyEventArgs e)
    {
      if (!this.enableKeyboardNavigation || e.Handled)
        return;
      switch (e.KeyData)
      {
        case Keys.Return:
        case Keys.Space:
          if (this.SelectedIndex <= -1 || this.SelectedIndex >= this.Items.Count)
            break;
          this.CarouselItem_Click((object) this.Items[this.selectedIndex], EventArgs.Empty);
          break;
        case Keys.Left:
          this.btnPrev_Click((object) this, EventArgs.Empty);
          break;
        case Keys.Right:
          this.btnNext_Click((object) this, EventArgs.Empty);
          break;
      }
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (this.ItemsContainer.EnableAutoLoop)
        this.suspendAutoLoopTicks = this.SuspendTicksCount;
      if (this.Items.Count == 0)
        return;
      if (this.SelectedIndex < this.Items.Count - 1)
        ++this.SelectedIndex;
      else
        this.SelectedIndex = 0;
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
      if (this.ItemsContainer.EnableAutoLoop)
        this.suspendAutoLoopTicks = this.SuspendTicksCount;
      if (this.Items.Count == 0)
        return;
      if (this.SelectedIndex > 0)
        --this.SelectedIndex;
      else
        this.SelectedIndex = this.Items.Count - 1;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      RectangleF finalRect1 = RectangleF.Empty;
      RectangleF finalRect2 = RectangleF.Empty;
      switch (this.buttonsPositon)
      {
        case NavigationButtonsPosition.Left:
          finalRect1 = new RectangleF(new PointF((float) this.NavigationButtonsOffset.Width, (float) this.navigationButtonsOffset.Height + this.btnPrev.DesiredSize.Height), this.btnPrev.DesiredSize);
          finalRect2 = new RectangleF(new PointF((float) this.NavigationButtonsOffset.Width, sizeF.Height - (float) this.NavigationButtonsOffset.Height - this.btnNext.DesiredSize.Height), this.btnNext.DesiredSize);
          break;
        case NavigationButtonsPosition.Right:
          finalRect1 = new RectangleF(new PointF(sizeF.Width - (float) this.NavigationButtonsOffset.Width - this.btnPrev.DesiredSize.Width, (float) this.navigationButtonsOffset.Height + this.btnPrev.DesiredSize.Height), this.btnPrev.DesiredSize);
          finalRect2 = new RectangleF(new PointF(sizeF.Width - (float) this.NavigationButtonsOffset.Width - this.btnPrev.DesiredSize.Width, sizeF.Height - (float) this.NavigationButtonsOffset.Height - this.btnNext.DesiredSize.Height), this.btnNext.DesiredSize);
          break;
        case NavigationButtonsPosition.Top:
          finalRect1 = new RectangleF(new PointF((float) this.NavigationButtonsOffset.Width, (float) this.navigationButtonsOffset.Height + this.btnPrev.DesiredSize.Height), this.btnPrev.DesiredSize);
          finalRect2 = new RectangleF(new PointF(sizeF.Width - (float) this.NavigationButtonsOffset.Width - this.btnNext.DesiredSize.Width, (float) this.NavigationButtonsOffset.Height + this.btnNext.DesiredSize.Height), this.btnNext.DesiredSize);
          break;
        case NavigationButtonsPosition.Bottom:
          finalRect1 = new RectangleF(new PointF((float) this.NavigationButtonsOffset.Width, sizeF.Height - (float) this.navigationButtonsOffset.Height - this.btnPrev.DesiredSize.Height), this.btnPrev.DesiredSize);
          finalRect2 = new RectangleF(new PointF(sizeF.Width - (float) this.NavigationButtonsOffset.Width - this.btnNext.DesiredSize.Width, sizeF.Height - (float) this.NavigationButtonsOffset.Height - this.btnNext.DesiredSize.Height), this.btnNext.DesiredSize);
          break;
      }
      this.btnPrev.Arrange(finalRect1);
      this.btnNext.Arrange(finalRect2);
      return sizeF;
    }

    private void ContainerItems_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          target.Click += new EventHandler(this.CarouselItem_Click);
          target.MouseEnter += new EventHandler(this.element_MouseEnter);
          target.MouseLeave += new EventHandler(this.element_MouseLeave);
          break;
        case ItemsChangeOperation.Removed:
          target.Click -= new EventHandler(this.CarouselItem_Click);
          target.MouseEnter -= new EventHandler(this.element_MouseEnter);
          target.MouseLeave -= new EventHandler(this.element_MouseLeave);
          break;
      }
      this.hoveredItem = (object) null;
      this.ReEvaluateAutoLoopPauseCondition();
    }

    internal void CallOnItemLeaving(ItemLeavingEventArgs args)
    {
      this.OnItemLeaving(args);
    }

    internal void CallOnItemEntering(ItemEnteringEventArgs args)
    {
      this.OnItemEntering(args);
    }

    protected void OnItemLeaving(ItemLeavingEventArgs args)
    {
      ItemLeavingEventHandler leavingEventHandler = (ItemLeavingEventHandler) this.Events[RadCarouselElement.ItemLeavingEventKey];
      if (leavingEventHandler == null)
        return;
      leavingEventHandler((object) this, args);
    }

    [Category("Behavior")]
    [Description("Occurs when an Item is about to leave carousel view")]
    public event ItemLeavingEventHandler ItemLeaving
    {
      add
      {
        this.Events.AddHandler(RadCarouselElement.ItemLeavingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCarouselElement.ItemLeavingEventKey, (Delegate) value);
      }
    }

    protected void OnItemEntering(ItemEnteringEventArgs args)
    {
      ItemEnteringEventHandler enteringEventHandler = (ItemEnteringEventHandler) this.Events[RadCarouselElement.ItemEnteringEventKey];
      if (enteringEventHandler == null)
        return;
      enteringEventHandler((object) this, args);
    }

    [Category("Behavior")]
    [Description("Occurs when an Item is about to enter carousel view")]
    public event ItemEnteringEventHandler ItemEntering
    {
      add
      {
        this.Events.AddHandler(RadCarouselElement.ItemEnteringEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCarouselElement.ItemEnteringEventKey, (Delegate) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Description("Occurs when new databound carousel item is created.")]
    [Browsable(false)]
    public event NewCarouselItemCreatingEventHandler NewCarouselItemCreating
    {
      add
      {
        this.Events.AddHandler(RadCarouselElement.NewCarouselItemCreatingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCarouselElement.NewCarouselItemCreatingEventKey, (Delegate) value);
      }
    }

    protected virtual void OnNewCarouselItemCreating(NewCarouselItemCreatingEventArgs e)
    {
      NewCarouselItemCreatingEventHandler creatingEventHandler = (NewCarouselItemCreatingEventHandler) this.Events[RadCarouselElement.NewCarouselItemCreatingEventKey];
      if (creatingEventHandler == null)
        return;
      creatingEventHandler((object) this, e);
    }

    internal void CallOnNewCarouselItemCreating(NewCarouselItemCreatingEventArgs e)
    {
      this.OnNewCarouselItemCreating(e);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [Description("Occurs after an Item is databound.")]
    [Browsable(false)]
    public event ItemDataBoundEventHandler ItemDataBound
    {
      add
      {
        this.Events.AddHandler(RadCarouselElement.ItemDataBoundEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCarouselElement.ItemDataBoundEventKey, (Delegate) value);
      }
    }

    protected virtual void OnItemDataBound(ItemDataBoundEventArgs e)
    {
      ItemDataBoundEventHandler boundEventHandler = (ItemDataBoundEventHandler) this.Events[RadCarouselElement.ItemDataBoundEventKey];
      if (boundEventHandler == null)
        return;
      boundEventHandler((object) this, e);
    }

    internal void CallOnItemDataBound(ItemDataBoundEventArgs e)
    {
      this.OnItemDataBound(e);
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Occurs when the selected items is changed.")]
    [Browsable(false)]
    public event EventHandler SelectedItemChanged
    {
      add
      {
        this.Events.AddHandler(RadCarouselElement.SelectedItemChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCarouselElement.SelectedItemChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSelectedItemChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadCarouselElement.SelectedItemChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    internal void CallOnSelectedItemChanged(EventArgs e)
    {
      this.OnSelectedItemChanged(e);
    }

    [Description("Occurs when the SelectedValue property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler SelectedValueChanged
    {
      add
      {
        this.Events.AddHandler(RadCarouselElement.SelectedValueChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCarouselElement.SelectedValueChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSelectedValueChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadCarouselElement.SelectedValueChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    internal void CallOnSelectedValueChanged(EventArgs e)
    {
      this.OnSelectedValueChanged(e);
    }

    [Browsable(true)]
    [Description("Occurs when the SelectedIndex property has changed.")]
    [Category("Behavior")]
    public event EventHandler SelectedIndexChanged
    {
      add
      {
        this.Events.AddHandler(RadCarouselElement.SelectedIndexChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCarouselElement.SelectedIndexChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
    {
      this.OnNotifyPropertyChanged("SelectedIndex");
      EventHandler eventHandler = (EventHandler) this.Events[RadCarouselElement.SelectedIndexChangedEventKey];
      if (eventHandler != null)
        eventHandler((object) this, (EventArgs) e);
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "SelectionChanged");
    }

    internal void CallOnSelectedIndexChanged(SelectedIndexChangedEventArgs e)
    {
      this.OnSelectedIndexChanged(e);
    }

    internal void AddEventHandler(object key, Delegate value)
    {
      this.Events.AddHandler(key, value);
    }

    internal void RemoveEventHandler(object key, Delegate value)
    {
      this.Events.RemoveHandler(key, value);
    }

    [Description("Gets a collection representing the items contained in this RadCarousel.")]
    public RadItemCollection Items
    {
      get
      {
        return this.carouselItemContainer.Items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CarouselItemsContainer ItemsContainer
    {
      get
      {
        return this.carouselItemContainer;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether sorting is case-sensitive.")]
    public bool CaseSensitive
    {
      get
      {
        return this.caseSensitive;
      }
      set
      {
        this.caseSensitive = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the keyboard navigation is enabled.")]
    [Browsable(true)]
    public bool EnableKeyboardNavigation
    {
      get
      {
        return this.enableKeyboardNavigation;
      }
      set
      {
        this.enableKeyboardNavigation = value;
      }
    }

    public RadItem FindItemExact(string text)
    {
      foreach (RadItem radItem in this.Items)
      {
        if (radItem.Text.Equals(text, this.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
          return radItem;
      }
      return (RadItem) null;
    }

    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the currently selected item.")]
    public virtual object SelectedItem
    {
      get
      {
        return (object) this.selectedItem;
      }
      set
      {
        if (this.selectedItem == value)
          return;
        if (value is RadItem)
        {
          RadItem radItem = (RadItem) value;
          if (this.Items.Contains(radItem))
          {
            this.SelectedIndex = this.Items.IndexOf(radItem);
            this.selectedItem = radItem;
          }
        }
        else if (value is string)
        {
          RadItem itemExact = this.FindItemExact((string) value);
          if (itemExact != null)
          {
            this.SelectedIndex = this.Items.IndexOf(itemExact);
            this.selectedItem = itemExact;
          }
        }
        else if (value == null)
        {
          this.SelectedIndex = -1;
          this.selectedItem = (RadItem) null;
        }
        this.OnSelectedItemChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the index specifying the currently selected item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Behavior")]
    public virtual int SelectedIndex
    {
      get
      {
        return this.selectedIndex;
      }
      set
      {
        if (this.selectedIndex == value || this.clearItemsSilently)
          return;
        this.CarouselItemContainer.SelectedIndex = value;
        this.ClearSelectedCollectionsSilently();
        if (value >= 0 && value < this.Items.Count)
        {
          this.selectedItem = this.Items[value];
          this.selectedIndex = value;
        }
        else
        {
          this.selectedItem = (RadItem) null;
          this.selectedIndex = -1;
        }
        this.hoveredItem = (object) null;
        this.ReEvaluateAutoLoopPauseCondition();
        this.RaiseSelectedIndexChanged();
      }
    }

    private void boundItems_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          if (target == null)
            break;
          this.Items.Add(target);
          break;
        case ItemsChangeOperation.Clearing:
          this.BeginUpdate();
          try
          {
            using (RadItemCollection.RadItemEnumerator enumerator = this.boundItems.GetEnumerator())
            {
              while (enumerator.MoveNext())
                this.Items.Remove(enumerator.Current);
              break;
            }
          }
          finally
          {
            this.EndUpdate();
          }
      }
    }

    private void ClearSelectedCollectionsSilently()
    {
    }

    private void RaiseSelectedIndexChanged()
    {
      this.OnSelectedIndexChanged(new SelectedIndexChangedEventArgs(this.oldIndex, this.selectedIndex));
      this.oldIndex = this.selectedIndex;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets value specifying the currently selected item.")]
    [Browsable(false)]
    [Bindable(true)]
    public object SelectedValue
    {
      get
      {
        return (object) null;
      }
      set
      {
        if (this.dataManager != null && string.IsNullOrEmpty(this.valueMember.BindingField))
          throw new InvalidOperationException("List Control Empty ValueMember");
        if (this.SelectedValue != null && this.SelectedValue.Equals(value))
          return;
        int num1 = -1;
        if (value != null)
        {
          int num2 = 0;
          while (num2 < this.Items.Count)
            ++num2;
        }
        this.SelectedIndex = num1;
        this.OnNotifyPropertyChanged(nameof (SelectedValue));
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the property to use as the actual value for the items.")]
    [Browsable(false)]
    [Category("Data")]
    public string ValueMember
    {
      get
      {
        return this.valueMember.BindingMember;
      }
      set
      {
        if (value == null)
          value = string.Empty;
        BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(value);
        if (bindingMemberInfo.Equals((object) this.valueMember))
          return;
        if (this.ValueMember.Length == 0)
          this.SetDataConnection(this.DataSource, false);
        if (this.dataManager != null && value != null && (value.Length != 0 && !this.BindingMemberInfoInDataManager(bindingMemberInfo)))
          throw new ArgumentException("List Control Wrong Value Member", nameof (value));
        this.valueMember = bindingMemberInfo;
        this.OnNotifyPropertyChanged(nameof (ValueMember));
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether formatting is applied to the DisplayMember property.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FormattingEnabled
    {
      get
      {
        return this.formattingEnabled;
      }
      set
      {
        if (value == this.formattingEnabled)
          return;
        this.formattingEnabled = value;
        this.RefreshItems();
        this.OnNotifyPropertyChanged(nameof (FormattingEnabled));
      }
    }

    private bool BindingMemberInfoInDataManager(BindingMemberInfo bindingMemberInfo)
    {
      if (this.dataManager != null)
      {
        PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
        int count = itemProperties.Count;
        for (int index = 0; index < count; ++index)
        {
          if (!typeof (IList).IsAssignableFrom(itemProperties[index].PropertyType) && itemProperties[index].Name.Equals(bindingMemberInfo.BindingField))
            return true;
        }
        for (int index = 0; index < count; ++index)
        {
          if (!typeof (IList).IsAssignableFrom(itemProperties[index].PropertyType) && string.Compare(itemProperties[index].Name, bindingMemberInfo.BindingField, true, CultureInfo.CurrentCulture) == 0)
            return true;
        }
      }
      return false;
    }

    [AttributeProvider(typeof (IListSource))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    [Description("Gets or sets the data source.")]
    [Category("Data")]
    public object DataSource
    {
      get
      {
        return this.dataSource;
      }
      set
      {
        if (value != null && !(value is IList) && !(value is IListSource))
          throw new ArgumentException("Bad Data Source For Complex Binding");
        if (this.dataSource == value)
          return;
        try
        {
          this.SetDataConnection(value, false);
        }
        catch
        {
        }
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(CarouselItemClickAction.SelectItem)]
    public CarouselItemClickAction ItemClickDefaultAction
    {
      get
      {
        return this.itemClickDefaultAction;
      }
      set
      {
        this.itemClickDefaultAction = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0.333)]
    [Browsable(false)]
    public double ItemReflectionPercentage
    {
      get
      {
        return this.itemReflectionPercentage;
      }
      set
      {
        this.itemReflectionPercentage = value;
        foreach (RadElement child1 in this.Children)
        {
          CarouselItemsContainer carouselItemsContainer = child1 as CarouselItemsContainer;
          if (carouselItemsContainer != null)
          {
            foreach (CarouselContentItem child2 in carouselItemsContainer.Children)
              child2.reflectionPrimitive.ItemReflectionPercentage = value;
          }
        }
      }
    }

    [Browsable(false)]
    [DefaultValue(30)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int AnimationFrames
    {
      get
      {
        return this.CarouselItemContainer.AnimationFrames;
      }
      set
      {
        this.CarouselItemContainer.AnimationFrames = value;
      }
    }

    [DefaultValue(40)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int AnimationDelay
    {
      get
      {
        return this.CarouselItemContainer.AnimationDelay;
      }
      set
      {
        this.CarouselItemContainer.AnimationDelay = value;
      }
    }

    [DefaultValue(3)]
    [Category("AutoLoopBehavior")]
    [Description("Gets or sets a value indicating the interval (in seconds) after which the carousel will resume looping when in auto-loop mode.")]
    public int AutoLoopPauseInterval
    {
      get
      {
        return this.autoLoopPauseInterval;
      }
      set
      {
        this.autoLoopPauseInterval = value;
      }
    }

    public void BeginUpdate()
    {
      ++this.updateCount;
      this.ItemsContainer.BeginUpdate();
    }

    public void EndUpdate()
    {
      if (this.updateCount > 0)
        --this.updateCount;
      this.ItemsContainer.EndUpdate();
    }

    protected virtual void RefreshItems()
    {
      try
      {
        this.BeginUpdate();
        if (this.DataManager == null || this.DataManager.Count == 0)
          return;
        this.clearItemsSilently = true;
        this.Items.Clear();
        this.boundItems.Clear();
        this.clearItemsSilently = false;
        for (int index = 0; index < this.DataManager.Count; ++index)
        {
          object dataItem = this.DataManager.List[index];
          RadItem boundCarouselItem = this.CreateDataBoundCarouselItem(dataItem);
          this.boundItems.Add(boundCarouselItem);
          this.RaiseItemDataBound(boundCarouselItem, dataItem);
        }
        if (this.DataManager == null)
          return;
        this.SelectedIndex = this.DataManager.Position;
      }
      finally
      {
        this.EndUpdate();
      }
    }

    protected virtual void SetItemCore(int index, object value)
    {
      RadItem boundCarouselItem = this.CreateDataBoundCarouselItem(value);
      this.Items[index] = boundCarouselItem;
      this.RaiseItemDataBound(boundCarouselItem, value);
    }

    private void RaiseItemDataBound(RadItem item, object dataItem)
    {
      this.OnItemDataBound(new ItemDataBoundEventArgs(item, dataItem));
    }

    internal virtual RadItem CreateDataBoundCarouselItem(object item)
    {
      if (item is RadItem)
        return item as RadItem;
      object itemValue = this.GetItemValue(item);
      RadItem newCarouselItem = this.CreateNewCarouselItem();
      newCarouselItem.Tag = itemValue;
      return newCarouselItem;
    }

    protected virtual RadItem CreateNewCarouselItem()
    {
      NewCarouselItemCreatingEventArgs e = new NewCarouselItemCreatingEventArgs((RadItem) new CarouselGenericItem());
      this.OnNewCarouselItemCreating(e);
      return e.NewCarouselItem;
    }

    private void CarouselItem_Click(object sender, EventArgs e)
    {
      if (this.ItemClickDefaultAction != CarouselItemClickAction.SelectItem)
        return;
      this.SelectedItem = sender;
      if (!this.ItemsContainer.EnableAutoLoop)
        return;
      this.suspendAutoLoopTicks = this.SuspendTicksCount;
    }

    public virtual object GetItemValue(object item)
    {
      object obj = (object) null;
      if (item != null)
      {
        try
        {
          PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(item).Find(this.valueMember.BindingField, true);
          if (propertyDescriptor != null)
            obj = propertyDescriptor.GetValue(item);
        }
        catch
        {
        }
      }
      return obj;
    }

    private void SetDataConnection(object newDataSource, bool force)
    {
      bool flag1 = this.dataSource != newDataSource;
      if (this.inSetDataConnection)
        return;
      try
      {
        if (force || flag1)
        {
          this.inSetDataConnection = true;
          IList list = this.DataManager != null ? this.DataManager.List : (IList) null;
          bool flag2 = this.DataManager == null;
          this.DisposeDataSource();
          this.dataSource = newDataSource;
          this.InitializeDataSource();
          if (this.isDataSourceInitialized)
          {
            CurrencyManager currencyManager = (CurrencyManager) null;
            if (newDataSource != null && this.BindingContext != null && newDataSource != Convert.DBNull)
              currencyManager = (CurrencyManager) this.BindingContext[newDataSource];
            if (this.dataManager != currencyManager)
            {
              if (this.dataManager != null)
              {
                this.dataManager.ItemChanged -= new ItemChangedEventHandler(this.DataManagerItemChanged);
                this.dataManager.PositionChanged -= new EventHandler(this.DataManagerPositionChanged);
              }
              this.dataManager = currencyManager;
              if (this.dataManager != null)
              {
                this.dataManager.ItemChanged += new ItemChangedEventHandler(this.DataManagerItemChanged);
                this.dataManager.PositionChanged += new EventHandler(this.DataManagerPositionChanged);
              }
            }
            if (this.dataManager != null && force && (list != this.dataManager.List || flag2))
              this.DataManagerItemChanged();
          }
        }
        if (!flag1)
          return;
        this.OnNotifyPropertyChanged("DataSource");
      }
      finally
      {
        this.inSetDataConnection = false;
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      switch (propertyName)
      {
        case "DataSource":
          if (!this.dataSourceDisposing)
          {
            this.BeginUpdate();
            this.SelectedIndex = -1;
            this.boundItems.Clear();
            this.EndUpdate();
          }
          this.RefreshItems();
          break;
        case "DisplayMember":
          this.RefreshItems();
          break;
        case "ValueMember":
          this.RefreshItems();
          break;
        case "SelectedIndex":
          if (this.DataManager != null && (!this.FormattingEnabled || this.SelectedIndex != -1) && this.SelectedIndex >= this.Items.Count - this.boundItems.Count)
            this.DataManager.Position = this.SelectedIndex - this.Items.Count + this.boundItems.Count;
          this.OnSelectedValueChanged(EventArgs.Empty);
          break;
        case "AnimationFrames":
        case "AnimationDelay":
          this.ItemsContainer.ForceUpdate();
          break;
      }
      base.OnNotifyPropertyChanged(propertyName);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.IsMouseOverElementProperty)
      {
        this.ReEvaluateAutoLoopPauseCondition();
      }
      else
      {
        if (e.Property != RadObject.BindingContextProperty || this.dataSource == null)
          return;
        this.SetDataConnection(this.dataSource, true);
      }
    }

    private int SuspendTicksCount
    {
      get
      {
        return 5 * this.AutoLoopPauseInterval;
      }
    }

    internal void ChangedAutoLoop()
    {
      if (this.IsDesignMode)
        return;
      if (this.ItemsContainer.EnableAutoLoop)
      {
        this.originalEasingType = new RadEasingType?();
        this.InitLoopTimer();
        for (int index = 0; index < this.Items.Count; ++index)
        {
          RadItem radItem = this.Items[index];
          radItem.MouseEnter += new EventHandler(this.element_MouseEnter);
          radItem.MouseLeave += new EventHandler(this.element_MouseLeave);
        }
        this.timer.Start();
      }
      else
      {
        this.timer.Stop();
        this.autoAnimationCount = -1;
        if (this.originalEasingType.HasValue)
          this.ItemsContainer.EasingType = this.originalEasingType.Value;
        for (int index = 0; index < this.Items.Count; ++index)
        {
          RadItem radItem = this.Items[index];
          radItem.MouseEnter -= new EventHandler(this.element_MouseEnter);
          radItem.MouseLeave -= new EventHandler(this.element_MouseLeave);
        }
      }
    }

    private void InitLoopTimer()
    {
      if (this.timer != null)
        return;
      this.timer = new Timer();
      this.timer.Interval = 200;
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      this.AutoLoopNextFrame();
    }

    private void AutoLoopNextFrame()
    {
      --this.suspendAutoLoopTicks;
      if (this.autoLoopPaused || this.suspendAutoLoopTicks > 0 || !this.CanApplyAutoLoop())
        return;
      this.suspendAutoLoopTicks = 0;
      if (!this.originalEasingType.HasValue)
        this.originalEasingType = new RadEasingType?(this.ItemsContainer.EasingType);
      this.ItemsContainer.EasingType = RadEasingType.Linear;
      if (this.autoAnimationCount > 0 || this.ItemsContainer.Items.Count <= 0)
        return;
      this.autoAnimationCount = 1;
      if (this.ItemsContainer.AutoLoopDirection == AutoLoopDirections.Forward)
      {
        ++this.ItemsContainer.SelectedIndex;
      }
      else
      {
        if (this.ItemsContainer.AutoLoopDirection != AutoLoopDirections.Backward)
          return;
        --this.ItemsContainer.SelectedIndex;
      }
    }

    private bool CanApplyAutoLoop()
    {
      if (!this.ItemsContainer.EnableAutoLoop || this.ElementTree == null || (!this.ElementTree.Control.IsHandleCreated || !this.ElementTree.Control.Visible))
        return false;
      Form form = this.ElementTree.Control.FindForm();
      return form != null && form.WindowState != FormWindowState.Minimized;
    }

    public virtual void OnAnimationFinished()
    {
      if (this.AnimationFinished != null)
        this.AnimationFinished((object) this, EventArgs.Empty);
      --this.autoAnimationCount;
      if (!this.CanApplyAutoLoop() || this.autoLoopPaused || (this.suspendAutoLoopTicks > 0 || this.autoAnimationCount != 0))
        return;
      this.ElementTree.Control.BeginInvoke((Delegate) new MethodInvoker(this.AutoLoopNextFrame));
    }

    public virtual void OnAnimationStarted()
    {
      if (this.AnimationStarted == null)
        return;
      this.AnimationStarted((object) this, EventArgs.Empty);
    }

    private void element_MouseEnter(object sender, EventArgs e)
    {
      this.hoveredItem = sender;
      this.ReEvaluateAutoLoopPauseCondition();
    }

    private void element_MouseLeave(object sender, EventArgs e)
    {
      if (sender != this.hoveredItem)
        return;
      this.hoveredItem = (object) null;
      this.ReEvaluateAutoLoopPauseCondition();
    }

    private void ReEvaluateAutoLoopPauseCondition()
    {
      if (!this.ItemsContainer.EnableAutoLoop)
      {
        this.autoLoopPaused = false;
      }
      else
      {
        bool autoLoopPaused = this.autoLoopPaused;
        AutoLoopPauseConditions loopPauseCondition = this.ItemsContainer.AutoLoopPauseCondition;
        if ((loopPauseCondition & AutoLoopPauseConditions.OnMouseOverCarousel) == AutoLoopPauseConditions.OnMouseOverCarousel)
          this.autoLoopPaused = this.IsMouseOverElement;
        if ((loopPauseCondition & AutoLoopPauseConditions.OnMouseOverItem) == AutoLoopPauseConditions.OnMouseOverItem)
          this.autoLoopPaused = this.hoveredItem != null;
        if (this.autoLoopPaused)
        {
          if (!this.originalEasingType.HasValue)
            return;
          this.ItemsContainer.EasingType = this.originalEasingType.Value;
        }
        else
        {
          if (!autoLoopPaused)
            return;
          this.suspendAutoLoopTicks = this.SuspendTicksCount;
        }
      }
    }

    protected virtual void DataManagerPositionChanged(object sender, EventArgs e)
    {
      if (this.dataManager == null || this.boundItems.Count <= 0)
        return;
      this.SelectedIndex = this.dataManager.Position + this.Items.Count - this.boundItems.Count;
    }

    private void InitializeDataSource()
    {
      if (this.dataSource is IComponent)
        ((IComponent) this.dataSource).Disposed += new EventHandler(this.DataSourceDisposed);
      ISupportInitializeNotification dataSource = this.dataSource as ISupportInitializeNotification;
      if (dataSource != null && !dataSource.IsInitialized)
      {
        dataSource.Initialized += new EventHandler(this.DataSourceInitialized);
        this.isDataSourceInitEventHooked = true;
        this.isDataSourceInitialized = false;
      }
      else
        this.isDataSourceInitialized = true;
    }

    private void DataSourceInitialized(object sender, EventArgs e)
    {
      this.SetDataConnection(this.dataSource, true);
    }

    private void DisposeDataSource()
    {
      if (this.dataSource is IComponent)
        ((IComponent) this.dataSource).Disposed -= new EventHandler(this.DataSourceDisposed);
      ISupportInitializeNotification dataSource = this.dataSource as ISupportInitializeNotification;
      if (dataSource == null || !this.isDataSourceInitEventHooked)
        return;
      dataSource.Initialized -= new EventHandler(this.DataSourceInitialized);
      this.isDataSourceInitEventHooked = false;
    }

    private void DataSourceDisposed(object sender, EventArgs e)
    {
      this.dataSourceDisposing = true;
      this.SetDataConnection((object) null, true);
    }

    protected virtual CurrencyManager DataManager
    {
      get
      {
        return this.dataManager;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadRepeatButtonElement ButtonPrevious
    {
      get
      {
        return this.btnPrev;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadRepeatButtonElement ButtonNext
    {
      get
      {
        return this.btnNext;
      }
    }

    [DefaultValue(typeof (Size), "0,0")]
    [Description("Represents the navigation buttons offset")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Size NavigationButtonsOffset
    {
      get
      {
        return this.navigationButtonsOffset;
      }
      set
      {
        this.navigationButtonsOffset = value;
        this.InvalidateArrange();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual NavigationButtonsPosition ButtonPositions
    {
      get
      {
        return this.buttonsPositon;
      }
      set
      {
        this.buttonsPositon = value;
        this.InvalidateArrange();
      }
    }

    protected virtual void DataManagerItemChanged(object sender, ItemChangedEventArgs e)
    {
      if (this.dataManager == null)
        return;
      if (e.Index == -1)
        this.DataManagerItemChanged();
      else
        this.SetItemCore(e.Index, this.dataManager.List[e.Index]);
    }

    protected virtual void DataManagerItemChanged()
    {
      this.SetItemsCore(this.dataManager.List);
      int num = this.dataManager.Position + this.Items.Count - this.boundItems.Count;
      if (this.SelectedIndex == num)
      {
        if (num <= -1 || num >= this.boundItems.Count)
          return;
        this.selectedIndex = -1;
        this.selectedItem = (RadItem) null;
        this.SelectedItem = (object) this.boundItems[this.dataManager.Position].Text;
        this.OnSelectedItemChanged(EventArgs.Empty);
      }
      else
        this.SelectedIndex = num;
    }

    protected virtual void SetItemsCore(IList items)
    {
      this.BeginUpdate();
      this.clearItemsSilently = true;
      this.Items.Clear();
      this.boundItems.Clear();
      this.clearItemsSilently = false;
      if (items.Count > 0)
      {
        for (int index = 0; index < items.Count; ++index)
        {
          object dataItem = items[index];
          RadItem boundCarouselItem = this.CreateDataBoundCarouselItem(dataItem);
          this.boundItems.Add(boundCarouselItem);
          this.RaiseItemDataBound(boundCarouselItem, dataItem);
        }
      }
      this.EndUpdate();
    }

    void IDisposable.Dispose()
    {
      if (this.timer != null)
      {
        this.timer.Tick -= new EventHandler(this.timer_Tick);
        this.timer.Dispose();
        this.timer = (Timer) null;
      }
      this.DisposeDataSource();
      this.boundItems.ItemsChanged -= new ItemChangedDelegate(this.boundItems_ItemsChanged);
      if (this.carouselItemContainer != null)
        this.carouselItemContainer.Items.ItemsChanged -= new ItemChangedDelegate(this.ContainerItems_ItemsChanged);
      if (this.btnPrev != null)
        this.btnPrev.Click -= new EventHandler(this.btnPrev_Click);
      if (this.btnNext == null)
        return;
      this.btnNext.Click -= new EventHandler(this.btnNext_Click);
    }

    public RadItem FindItemStartingWith(string startsWith)
    {
      if (!string.IsNullOrEmpty(startsWith))
      {
        foreach (RadItem radItem in this.Items)
        {
          if (radItem.Text != null && radItem.Text.StartsWith(startsWith, !this.CaseSensitive, CultureInfo.InvariantCulture))
            return radItem;
        }
      }
      return (RadItem) null;
    }

    public RadItem FindItemContaining(string subString)
    {
      if (!string.IsNullOrEmpty(subString))
      {
        foreach (RadItem radItem in this.Items)
        {
          StringComparison comparisonType = this.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
          if (radItem.Text != null && radItem.Text.IndexOf(subString, 0, comparisonType) >= 0)
            return radItem;
        }
      }
      return (RadItem) null;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      args.Handled = true;
      if (args.IsBegin)
      {
        this.orignalEasing = this.CarouselItemContainer.EasingType;
        this.originalAnimationDelay = this.CarouselItemContainer.AnimationDelay;
        this.CarouselItemContainer.EasingType = RadEasingType.Linear;
        this.totalPanOffset = 0.0;
      }
      else if (args.IsEnd)
      {
        this.CarouselItemContainer.EasingType = this.orignalEasing;
        this.CarouselItemContainer.AnimationDelay = this.originalAnimationDelay;
      }
      else
      {
        this.CarouselItemContainer.AnimationDelay = 20 - 15 * Math.Min(Math.Abs(args.Offset.Width / 50), 1);
        this.totalPanOffset += (double) args.Offset.Width;
        double pathLength = this.GetPathLength();
        if (args.Offset.Width == 0 || Math.Abs(this.totalPanOffset) < pathLength / (double) this.Items.Count || this.CarouselItemContainer.IsAnimationRunning)
          return;
        int direction = this.GetDirection();
        if (direction == 0)
          return;
        if (Math.Sign(this.totalPanOffset) != Math.Sign(args.Offset.Width) && args.Offset.Width != 0)
          this.totalPanOffset = 0.0;
        this.totalPanOffset -= pathLength / (double) this.Items.Count * (double) Math.Sign(this.totalPanOffset);
        int num = this.SelectedIndex + direction * Math.Sign(this.totalPanOffset);
        while (num < 0)
          num += this.Items.Count;
        while (num >= this.Items.Count)
          num -= this.Items.Count;
        this.SelectedIndex = num;
      }
    }

    private double GetPathLength()
    {
      CarouselParameterPath carouselPath = this.CarouselItemContainer.CarouselPath as CarouselParameterPath;
      if (carouselPath == null)
        return 0.0;
      Rectangle bounds = this.ElementTree.Control.Bounds;
      double num1 = 0.01;
      double num2 = 0.0;
      Point3D point3D1 = (Point3D) carouselPath.EvaluateByParameter((VisualElement) null, new CarouselPathAnimationData(), 0.0);
      if (carouselPath.EnableRelativePath)
        point3D1 = carouselPath.ConvertFromRelativePath(point3D1, bounds.Size);
      for (; num1 <= 1.0; num1 += 0.01)
      {
        Point3D point = (Point3D) carouselPath.EvaluateByParameter((VisualElement) null, new CarouselPathAnimationData(), num1);
        if (carouselPath.EnableRelativePath)
          point = carouselPath.ConvertFromRelativePath(point, bounds.Size);
        Point3D point3D2 = new Point3D(point.X, point.Y, point.Z);
        point3D2.Subtract(point3D1);
        num2 += point3D2.Length();
        point3D1 = point;
      }
      return num2;
    }

    private int GetDirection()
    {
      CarouselParameterPath carouselPath = this.CarouselItemContainer.CarouselPath as CarouselParameterPath;
      if (carouselPath == null)
        return 0;
      double num1 = double.NegativeInfinity;
      double num2 = double.NegativeInfinity;
      Rectangle bounds = this.ElementTree.Control.Bounds;
      for (double num3 = 0.0; num3 <= 1.0; num3 += 0.001)
      {
        Point3D point = (Point3D) carouselPath.EvaluateByParameter((VisualElement) null, new CarouselPathAnimationData(), num3);
        if (carouselPath.EnableRelativePath)
          point = carouselPath.ConvertFromRelativePath(point, bounds.Size);
        if (point.Z > num1)
        {
          num1 = point.Z;
          num2 = num3;
        }
      }
      return ((Point3D) carouselPath.EvaluateByParameter((VisualElement) null, new CarouselPathAnimationData(), num2)).X >= ((Point3D) carouselPath.EvaluateByParameter((VisualElement) null, new CarouselPathAnimationData(), num2 + 0.1)).X ? -1 : 1;
    }
  }
}
