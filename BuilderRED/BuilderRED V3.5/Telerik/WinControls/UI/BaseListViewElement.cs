// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseListViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public abstract class BaseListViewElement : VirtualizedScrollPanel<ListViewDataItem, BaseListViewVisualItem>
  {
    protected Timer beginEditTimer = new Timer();
    protected Timer groupSelectionTimer = new Timer();
    private string lastSearchCriteria = "";
    protected Point startPoint = Point.Empty;
    protected Point endPoint = Point.Empty;
    protected Dictionary<ListViewDataItem, bool> lassoInitialSelection = new Dictionary<ListViewDataItem, bool>();
    protected Point pointerOffset = Point.Empty;
    private Timer lassoTimer = new Timer();
    public static RadProperty ItemSizeProperty = RadProperty.Register(nameof (ItemSize), typeof (Size), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(200, 20), ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GroupItemSizeProperty = RadProperty.Register(nameof (GroupItemSize), typeof (Size), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(200, 20), ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GroupIndentProperty = RadProperty.Register(nameof (GroupIndent), typeof (int), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 25, ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AllowArbitraryItemHeightProperty = RadProperty.Register(nameof (AllowArbitraryItemHeight), typeof (bool), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AllowArbitraryItemWidthProperty = RadProperty.Register(nameof (AllowArbitraryItemWidth), typeof (bool), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FullRowSelectProperty = RadProperty.Register(nameof (FullRowSelect), typeof (bool), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectionRectangleColorProperty = RadProperty.Register(nameof (SelectionRectangleColor), typeof (Color), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(128, 51, 153, (int) byte.MaxValue), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SelectionRectangleBorderColorProperty = RadProperty.Register(nameof (SelectionRectangleBorderColor), typeof (Color), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb((int) byte.MaxValue, 51, 153, (int) byte.MaxValue), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DragHintProperty = RadProperty.Register(nameof (DragHint), typeof (RadImageShape), typeof (BaseListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    private RadListViewElement owner;
    protected ListViewDataItem anchor;
    private ScrollServiceBehavior scrollBehavior;
    protected ListViewDataItem lastClickedItem;
    protected BaseListViewVisualItem pressedItem;
    protected bool disableEditOnMouseUp;
    protected Keys lastModifierKeys;
    protected bool disableGroupSelectOnMouseUp;
    private ScrollState horizontalScrollState;
    private ScrollState verticalScrollState;
    internal Timer typingTimer;
    private StringBuilder searchBuffer;
    protected bool isLassoSelection;
    private int oldScrollOffset;
    private IFindStringComparer findStringComparer;

    static BaseListViewElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (BaseListViewElement));
    }

    public BaseListViewElement(RadListViewElement owner)
    {
      this.owner = owner;
      this.ViewElement.FitElementsToSize = this.FullRowSelect;
      this.Items = (IList<ListViewDataItem>) owner.Items;
      this.Scroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.Scroller.AllowHiddenScrolling = true;
      this.Scroller.ScrollerUpdated += new EventHandler(this.Scroller_ScrollerUpdated);
      this.scrollBehavior = new ScrollServiceBehavior();
      this.scrollBehavior.ScrollServices.Add(new ScrollService((RadElement) this.ViewElement, this.Scroller.Scrollbar));
      this.beginEditTimer = new Timer();
      this.beginEditTimer.Interval = SystemInformation.DoubleClickTime + 10;
      this.beginEditTimer.Tick += new EventHandler(this.beginEditTimer_Tick);
      this.groupSelectionTimer = new Timer();
      this.groupSelectionTimer.Interval = SystemInformation.DoubleClickTime + 10;
      this.groupSelectionTimer.Tick += new EventHandler(this.groupSelectionTimer_Tick);
      this.lassoTimer = new Timer();
      this.lassoTimer.Interval = 1;
      this.lassoTimer.Tick += new EventHandler(this.OnLassoTimerTick);
      this.AllowDrop = true;
      this.typingTimer = new Timer();
      this.typingTimer.Interval = 300;
      this.typingTimer.Tick += new EventHandler(this.typingTimer_Tick);
    }

    [Description("Gets or sets the display state of the horizontal scrollbar.")]
    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.horizontalScrollState;
      }
      set
      {
        if (this.horizontalScrollState == value)
          return;
        this.horizontalScrollState = value;
        this.OnNotifyPropertyChanged(nameof (HorizontalScrollState));
      }
    }

    [Description("Gets or sets the display state of the vertical scrollbar.")]
    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.verticalScrollState;
      }
      set
      {
        if (this.verticalScrollState == value)
          return;
        this.verticalScrollState = value;
        this.OnNotifyPropertyChanged(nameof (VerticalScrollState));
      }
    }

    [Browsable(false)]
    [VsbBrowsable(true)]
    public RadImageShape DragHint
    {
      get
      {
        return (RadImageShape) this.GetValue(BaseListViewElement.DragHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.DragHintProperty, (object) value);
      }
    }

    public ScrollServiceBehavior ScrollBehavior
    {
      get
      {
        return this.scrollBehavior;
      }
    }

    public override Orientation Orientation
    {
      get
      {
        return base.Orientation;
      }
      set
      {
        if (this.Orientation == value || !this.SupportsOrientation(value))
          return;
        base.Orientation = value;
        this.OnOrientationChanged();
        this.OnNotifyPropertyChanged(nameof (Orientation));
      }
    }

    public virtual RadListViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public virtual bool AllowArbitraryItemHeight
    {
      get
      {
        return (bool) this.GetValue(BaseListViewElement.AllowArbitraryItemHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.AllowArbitraryItemHeightProperty, (object) value);
      }
    }

    public virtual bool AllowArbitraryItemWidth
    {
      get
      {
        return (bool) this.GetValue(BaseListViewElement.AllowArbitraryItemWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.AllowArbitraryItemWidthProperty, (object) value);
      }
    }

    public virtual bool FullRowSelect
    {
      get
      {
        return (bool) this.GetValue(BaseListViewElement.FullRowSelectProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.FullRowSelectProperty, (object) value);
      }
    }

    public virtual Size ItemSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(BaseListViewElement.ItemSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.ItemSizeProperty, (object) value);
      }
    }

    public Size GroupItemSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(BaseListViewElement.GroupItemSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.GroupItemSizeProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    public Color SelectionRectangleColor
    {
      get
      {
        return (Color) this.GetValue(BaseListViewElement.SelectionRectangleColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.SelectionRectangleColorProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    public Color SelectionRectangleBorderColor
    {
      get
      {
        return (Color) this.GetValue(BaseListViewElement.SelectionRectangleBorderColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.SelectionRectangleBorderColorProperty, (object) value);
      }
    }

    public virtual int GroupIndent
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(BaseListViewElement.GroupIndentProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(BaseListViewElement.GroupIndentProperty, (object) value);
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

    protected override VirtualizedStackContainer<ListViewDataItem> CreateViewElement()
    {
      return (VirtualizedStackContainer<ListViewDataItem>) new BaseListViewContainer(this);
    }

    protected override ITraverser<ListViewDataItem> CreateItemTraverser(
      IList<ListViewDataItem> items)
    {
      return (ITraverser<ListViewDataItem>) new ListViewTraverser(this.owner);
    }

    protected override IVirtualizedElementProvider<ListViewDataItem> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<ListViewDataItem>) new ListViewVirtualizedElementProvider(this);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == BaseListViewElement.FullRowSelectProperty)
        this.ViewElement.FitElementsToSize = this.FullRowSelect;
      if (e.Property != BaseListViewElement.FullRowSelectProperty && e.Property != BaseListViewElement.AllowArbitraryItemHeightProperty && (e.Property != BaseListViewElement.AllowArbitraryItemWidthProperty && e.Property != BaseListViewElement.ItemSizeProperty) && (e.Property != BaseListViewElement.GroupIndentProperty && e.Property != BaseListViewElement.GroupItemSizeProperty))
        return;
      this.owner.Update(RadListViewElement.UpdateModes.RefreshAll);
    }

    protected override bool UpdateOnMeasure(SizeF availableSize)
    {
      if (float.IsInfinity(availableSize.Width))
        availableSize.Width = this.ViewElement.DesiredSize.Width;
      if (float.IsInfinity(availableSize.Height))
        availableSize.Height = this.ViewElement.DesiredSize.Height;
      bool flag = base.UpdateOnMeasure(availableSize);
      ElementVisibility visibility = this.HScrollBar.Visibility;
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      this.HScrollBar.LargeChange = Math.Max(0, (int) ((double) clientRectangle.Width - (double) this.VScrollBar.DesiredSize.Width - (double) this.ViewElement.Margin.Horizontal));
      this.HScrollBar.SmallChange = this.HScrollBar.LargeChange / 10;
      this.UpdateHScrollbarMaximum();
      SizeF size = clientRectangle.Size;
      if (this.HScrollBar.Visibility != ElementVisibility.Collapsed)
        size.Height -= this.HScrollBar.DesiredSize.Height;
      this.Scroller.ClientSize = size;
      if (this.HScrollBar.Visibility != visibility)
        return true;
      return flag;
    }

    protected override void DisposeManagedResources()
    {
      this.beginEditTimer.Stop();
      this.beginEditTimer.Tick -= new EventHandler(this.beginEditTimer_Tick);
      if (this.typingTimer != null)
      {
        this.typingTimer.Stop();
        this.typingTimer.Tick -= new EventHandler(this.typingTimer_Tick);
        this.typingTimer.Dispose();
        this.typingTimer = (Timer) null;
      }
      base.DisposeManagedResources();
    }

    protected override void PaintOverride(
      IGraphics screenRadGraphics,
      Rectangle clipRectangle,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      base.PaintOverride(screenRadGraphics, clipRectangle, angle, scale, useRelativeTransformation);
      if (!this.Capture)
        return;
      Rectangle rectangle = new Rectangle(new Point(Math.Min(this.startPoint.X, this.endPoint.X), Math.Min(this.startPoint.Y, this.endPoint.Y)), new Size(Math.Abs(this.endPoint.X - this.startPoint.X), Math.Abs(this.endPoint.Y - this.startPoint.Y)));
      rectangle.Intersect(this.ViewElement.ControlBoundingRectangle);
      screenRadGraphics.FillRectangle(rectangle, this.SelectionRectangleColor);
      screenRadGraphics.DrawRectangle(rectangle, this.SelectionRectangleBorderColor);
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return this.Owner.AllowDragDrop;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.FindStringComparer = (IFindStringComparer) new StartsWithFindStringComparer();
    }

    public ListViewDataItem GetItemAt(Point location)
    {
      return this.GetVisualItemAt(location)?.Data;
    }

    public virtual BaseListViewVisualItem GetVisualItemAt(Point location)
    {
      if (this.ElementTree == null)
        return (BaseListViewVisualItem) null;
      return this.ElementTree.GetElementAtPoint(location) as BaseListViewVisualItem;
    }

    public void ScrollTo(int delta)
    {
      RadScrollBarElement scrollbar = this.Scroller.Scrollbar;
      int num = scrollbar.Value - delta * scrollbar.SmallChange;
      if (num > scrollbar.Maximum - scrollbar.LargeChange + 1)
        num = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (num < scrollbar.Minimum)
        num = 0;
      else if (num > scrollbar.Maximum)
        num = scrollbar.Maximum;
      scrollbar.Value = num;
    }

    public virtual void EnsureItemVisible(ListViewDataItem item)
    {
      this.EnsureItemVisible(item, false);
    }

    public virtual void EnsureItemVisible(ListViewDataItem item, bool ensureHorizontally)
    {
      this.ViewElement.InvalidateMeasure();
      this.UpdateLayout();
      this.Scroller.UpdateScrollRange();
      this.EnsureItemVisibleVertical(item);
      if (ensureHorizontally)
        this.EnsureItemVisibleHorizontal(item);
      this.UpdateLayout();
    }

    public void ClearSelection()
    {
      this.owner.SelectedItems.Clear();
    }

    protected void ToggleItemCheckState(ListViewDataItem item)
    {
      switch (item.CheckState)
      {
        case Telerik.WinControls.Enumerations.ToggleState.Off:
          item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
          break;
        case Telerik.WinControls.Enumerations.ToggleState.On:
          if (this.Owner.ThreeStateMode)
          {
            item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
            break;
          }
          item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
          break;
        default:
          item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
          break;
      }
    }

    protected virtual bool SupportsOrientation(Orientation orientation)
    {
      return true;
    }

    protected virtual void EnsureItemVisibleHorizontal(ListViewDataItem item)
    {
    }

    protected virtual void EnsureItemVisibleVertical(ListViewDataItem item)
    {
      if (item == null)
        return;
      BaseListViewVisualItem element = this.GetElement(item);
      if (element == null)
      {
        this.UpdateLayout();
        if (this.ViewElement.Children.Count <= 0)
          return;
        if (this.GetItemIndex(item) <= this.GetItemIndex(((BaseListViewVisualItem) this.ViewElement.Children[0]).Data))
          this.Scroller.ScrollToItem(item, false);
        else
          this.EnsureItemVisibleVerticalCore(item);
      }
      else if (element.ControlBoundingRectangle.Bottom > this.ViewElement.ControlBoundingRectangle.Bottom)
      {
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + (element.ControlBoundingRectangle.Bottom - this.ViewElement.ControlBoundingRectangle.Bottom));
      }
      else
      {
        if (element.ControlBoundingRectangle.Top >= this.ViewElement.ControlBoundingRectangle.Top)
          return;
        this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - (this.ViewElement.ControlBoundingRectangle.Top - element.ControlBoundingRectangle.Top));
      }
    }

    protected virtual void EnsureItemVisibleVerticalCore(ListViewDataItem item)
    {
      if (item == null)
        return;
      bool flag = false;
      int num = 0;
      ListViewDataItem data = ((BaseListViewVisualItem) this.ViewElement.Children[this.ViewElement.Children.Count - 1]).Data;
      ListViewTraverser enumerator = (ListViewTraverser) this.Scroller.Traverser.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == item)
        {
          this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + num);
          this.UpdateLayout();
          BaseListViewVisualItem element = this.GetElement(item);
          if (element == null || element.ControlBoundingRectangle.Bottom <= this.ViewElement.ControlBoundingRectangle.Bottom)
            break;
          this.EnsureItemVisible(item);
          break;
        }
        if (enumerator.Current == data)
          flag = true;
        if (flag)
          num += (int) this.ViewElement.ElementProvider.GetElementSize(enumerator.Current).Height + this.ItemSpacing;
      }
    }

    protected virtual void OnOrientationChanged()
    {
    }

    public virtual void UpdateHScrollbarMaximum()
    {
    }

    protected virtual void UpdateHScrollbarVisibility()
    {
    }

    protected internal virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      this.pressedItem = (BaseListViewVisualItem) null;
      if (this.owner.EnableKineticScrolling && this.ViewElement.ContainsMouse)
      {
        bool flag = !this.ScrollBehavior.IsRunning;
        this.scrollBehavior.MouseUp(e.Location);
        if (!flag)
          return true;
      }
      else
      {
        if (this.isLassoSelection)
        {
          this.EndLassoSelection();
          return false;
        }
        this.scrollBehavior.Stop();
      }
      if (e.Button == MouseButtons.Left)
      {
        ListViewDataItem itemAt = this.GetItemAt(e.Location);
        if (itemAt == null || !itemAt.Enabled)
        {
          this.lastModifierKeys = Keys.None;
          this.groupSelectionTimer.Stop();
          this.beginEditTimer.Stop();
          this.lastClickedItem = (ListViewDataItem) null;
          return false;
        }
        if (itemAt is ListViewDataItemGroup)
        {
          if (!this.disableGroupSelectOnMouseUp && this.owner.MultiSelect)
          {
            this.lastClickedItem = itemAt;
            this.lastModifierKeys = Control.ModifierKeys;
            this.groupSelectionTimer.Start();
          }
          else if (!this.owner.MultiSelect)
          {
            this.ProcessSelection(itemAt, Control.ModifierKeys, true);
          }
          else
          {
            this.lastClickedItem = (ListViewDataItem) null;
            this.lastModifierKeys = Keys.None;
            this.groupSelectionTimer.Stop();
          }
          return false;
        }
        this.lastClickedItem = (ListViewDataItem) null;
        this.lastModifierKeys = Keys.None;
        this.groupSelectionTimer.Stop();
        if (itemAt.Selected && this.Owner.ShowCheckBoxes && (!this.Owner.AllowEdit && this.Owner.CheckOnClickMode != CheckOnClickMode.Off))
          this.ToggleItemCheckState(itemAt);
        else if (!itemAt.Selected && this.owner.ShowCheckBoxes && this.owner.CheckOnClickMode == CheckOnClickMode.FirstClick)
          this.ToggleItemCheckState(itemAt);
        if (itemAt != null && !this.disableEditOnMouseUp && (itemAt == this.owner.SelectedItem && Control.ModifierKeys == Keys.None) && this.lastClickedItem == null)
        {
          this.lastClickedItem = itemAt;
          this.beginEditTimer.Start();
        }
        else
        {
          this.beginEditTimer.Stop();
          this.lastClickedItem = (ListViewDataItem) null;
          this.ProcessSelection(itemAt, Control.ModifierKeys, true);
        }
      }
      else
      {
        this.lastModifierKeys = Keys.None;
        this.groupSelectionTimer.Stop();
        this.beginEditTimer.Stop();
        this.lastClickedItem = (ListViewDataItem) null;
      }
      return false;
    }

    protected internal virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.None && (this.owner.AllowDragDrop && this.GetVisualItemAt(e.Location) == this.pressedItem))
      {
        this.owner.DragDropService.Start((object) this.GetVisualItemAt(e.Location));
        if (this.owner.DragDropService.State == RadServiceState.Working)
          return false;
      }
      if (this.owner.EnableKineticScrolling && this.ViewElement.ContainsMouse)
        this.scrollBehavior.MouseMove(e.Location);
      if (!this.isLassoSelection && this.owner.EnableLassoSelection && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && this.ViewElement.ContainsMouse)
        this.BeginLassoSelection();
      if (!this.isLassoSelection || !this.owner.EnableLassoSelection)
      {
        if (this.Capture)
          this.EndLassoSelection();
        return false;
      }
      if (e.Location.X < this.ControlBoundingRectangle.Left)
      {
        this.pointerOffset.X = e.Location.X - this.ControlBoundingRectangle.Left;
        this.lassoTimer.Start();
      }
      else if (e.Location.X > this.ControlBoundingRectangle.Right)
      {
        this.pointerOffset.X = e.Location.X - this.ControlBoundingRectangle.Right;
        this.lassoTimer.Start();
      }
      else
        this.pointerOffset.X = 0;
      if (e.Location.Y < this.ControlBoundingRectangle.Top)
      {
        this.pointerOffset.Y = e.Location.Y - this.ControlBoundingRectangle.Top;
        this.lassoTimer.Start();
      }
      else if (e.Location.Y > this.ControlBoundingRectangle.Bottom)
      {
        this.pointerOffset.Y = e.Location.Y - this.ControlBoundingRectangle.Bottom;
        this.lassoTimer.Start();
      }
      else
        this.pointerOffset.Y = 0;
      if (this.ControlBoundingRectangle.Contains(e.Location))
        this.lassoTimer.Stop();
      if (this.isLassoSelection && this.endPoint != e.Location)
      {
        this.endPoint = e.Location;
        this.ProcessLassoSelection(this.GetSelectionRect());
        this.Invalidate();
      }
      return false;
    }

    protected internal virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      this.beginEditTimer.Stop();
      this.lastClickedItem = (ListViewDataItem) null;
      this.lastModifierKeys = Keys.None;
      this.groupSelectionTimer.Stop();
      this.pressedItem = this.GetVisualItemAt(e.Location);
      this.disableGroupSelectOnMouseUp = e.Clicks != 1;
      this.disableEditOnMouseUp = e.Clicks != 1;
      if (this.owner.EnableKineticScrolling)
        this.scrollBehavior.MouseDown(e.Location);
      else if (this.owner.EnableLassoSelection)
      {
        this.scrollBehavior.Stop();
        this.startPoint = this.endPoint = e.Location;
        this.Invalidate();
      }
      return false;
    }

    protected internal virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Escape:
          this.HandleEscapeKey(e);
          break;
        case Keys.Space:
          this.HandleSpaceKey(e);
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
        case Keys.Delete:
          this.HandleDeleteKey(e);
          break;
        case Keys.F2:
          this.HandleF2Key(e);
          break;
      }
      return false;
    }

    protected internal virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      this.HandleNavigation(e.KeyChar);
      return false;
    }

    protected virtual void HandlePageUpKey(KeyEventArgs e)
    {
      this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value - this.ControlBoundingRectangle.Height);
    }

    protected virtual void HandlePageDownKey(KeyEventArgs e)
    {
      this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + this.ControlBoundingRectangle.Height);
    }

    protected virtual void HandleDeleteKey(KeyEventArgs e)
    {
      if (this.owner.IsEditing || !this.owner.AllowRemove)
        return;
      this.owner.Items.BeginUpdate();
      List<ListViewDataItem> listViewDataItemList = new List<ListViewDataItem>((IEnumerable<ListViewDataItem>) this.owner.SelectedItems);
      while (listViewDataItemList.Count > 0)
      {
        int index = listViewDataItemList.Count - 1;
        if (!this.owner.OnItemRemoving(new ListViewItemCancelEventArgs(listViewDataItemList[index])))
        {
          this.owner.Items.Remove(listViewDataItemList[index]);
          this.owner.OnItemRemoved(new ListViewItemEventArgs(listViewDataItemList[index]));
        }
        listViewDataItemList.RemoveAt(index);
      }
      this.owner.Items.EndUpdate();
    }

    protected virtual void HandleEndKey(KeyEventArgs e)
    {
      ITraverser<ListViewDataItem> enumerator = this.Scroller.Traverser.GetEnumerator() as ITraverser<ListViewDataItem>;
      do
        ;
      while (enumerator.MoveNext());
      if (enumerator.Current == null)
        return;
      this.ProcessSelection(enumerator.Current, Control.ModifierKeys, false);
    }

    protected virtual void HandleHomeKey(KeyEventArgs e)
    {
      ITraverser<ListViewDataItem> enumerator = this.Scroller.Traverser.GetEnumerator() as ITraverser<ListViewDataItem>;
      enumerator.Reset();
      if (!enumerator.MoveNext() || enumerator.Current == null)
        return;
      this.ProcessSelection(enumerator.Current, Control.ModifierKeys, false);
    }

    protected virtual void HandleEscapeKey(KeyEventArgs e)
    {
      this.owner.CancelEdit();
    }

    protected virtual void HandleF2Key(KeyEventArgs e)
    {
      this.owner.BeginEdit();
    }

    protected virtual void HandleLeftKey(KeyEventArgs e)
    {
      ListViewDataItemGroup currentItem = this.owner.CurrentItem as ListViewDataItemGroup;
      if (currentItem == null)
        return;
      currentItem.Expanded = false;
    }

    protected virtual void HandleRightKey(KeyEventArgs e)
    {
      ListViewDataItemGroup currentItem = this.owner.CurrentItem as ListViewDataItemGroup;
      if (currentItem == null)
        return;
      currentItem.Expanded = true;
    }

    protected virtual void HandleDownKey(KeyEventArgs e)
    {
      ListViewDataItem nextItem = this.GetNextItem(this.owner.CurrentItem);
      if (nextItem == null)
        return;
      this.ProcessSelection(nextItem, Control.ModifierKeys, false);
    }

    protected virtual void HandleUpKey(KeyEventArgs e)
    {
      ListViewDataItem previousItem = this.GetPreviousItem(this.owner.CurrentItem);
      if (previousItem == null)
        return;
      this.ProcessSelection(previousItem, Control.ModifierKeys, false);
    }

    protected virtual void HandleSpaceKey(KeyEventArgs e)
    {
      if (this.owner.CurrentItem == null)
        return;
      if (this.Owner.ShowCheckBoxes && this.Owner.SelectedItems.Count > 0 && this.Owner.CurrentItem.Selected)
      {
        ListViewSelectedItemCollection selectedItems = this.Owner.SelectedItems;
        bool flag = true;
        Telerik.WinControls.Enumerations.ToggleState checkState = selectedItems[0].CheckState;
        foreach (ListViewDataItem listViewDataItem in (ReadOnlyCollection<ListViewDataItem>) selectedItems)
        {
          if (listViewDataItem.CheckState != checkState)
          {
            flag = false;
            break;
          }
        }
        foreach (ListViewDataItem listViewDataItem in (ReadOnlyCollection<ListViewDataItem>) selectedItems)
        {
          if (flag)
            this.ToggleItemCheckState(listViewDataItem);
          else
            listViewDataItem.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
        }
      }
      else
      {
        if (!this.owner.MultiSelect)
          this.ClearSelection();
        this.owner.CurrentItem.Selected = !this.owner.CurrentItem.Selected;
        if (!this.owner.CurrentItem.Selected)
          return;
        if (this.owner.CurrentItem.Selected)
          this.owner.SetSelectedItem(this.owner.CurrentItem);
        else
          this.owner.SetSelectedItem(this.owner.SelectedItems.Count > 0 ? this.owner.SelectedItems[this.owner.SelectedItems.Count - 1] : (ListViewDataItem) null);
        this.anchor = this.owner.SelectedItem;
      }
    }

    protected virtual void HandleNavigation(char keyChar)
    {
      if (!this.Owner.KeyboardSearchEnabled)
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
      ListViewDataItem firstMatch = this.GetFirstMatch(this.searchBuffer.ToString());
      if (firstMatch == null)
        return;
      this.ProcessSelection(firstMatch, Keys.None, false);
    }

    protected virtual ListViewDataItem GetFirstMatch(string searchCriteria)
    {
      ListViewTraverser listViewTraverser = new ListViewTraverser(this.Owner);
      ListViewDataItem listViewDataItem = this.Owner.SelectedItem;
      bool flag = false;
      if (listViewDataItem == null)
      {
        listViewTraverser.MoveTo(0);
        listViewDataItem = listViewTraverser.Current;
      }
      else
        listViewTraverser.MoveTo(listViewDataItem);
      listViewTraverser.MoveTo(listViewDataItem);
      while (listViewTraverser.Current == null || string.Equals(this.lastSearchCriteria, searchCriteria) || !this.FindStringComparer.Compare(listViewTraverser.Current.Text, searchCriteria))
      {
        while (listViewTraverser.MoveNext())
        {
          if (this.FindStringComparer.Compare(listViewTraverser.Current.Text, searchCriteria))
          {
            this.lastSearchCriteria = searchCriteria;
            return listViewTraverser.Current;
          }
        }
        if (!flag)
        {
          listViewTraverser.Reset();
          flag = true;
          if (listViewTraverser.Current != listViewDataItem)
            continue;
        }
        this.lastSearchCriteria = searchCriteria;
        return (ListViewDataItem) null;
      }
      this.lastSearchCriteria = searchCriteria;
      return listViewTraverser.Current;
    }

    protected virtual ListViewDataItem GetPreviousItem(ListViewDataItem currentItem)
    {
      ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Position = (object) currentItem;
      while (enumerator.MovePrevious())
      {
        if (enumerator.Current == null || enumerator.Current.Enabled)
          return enumerator.Current;
      }
      return (ListViewDataItem) null;
    }

    protected virtual ListViewDataItem GetNextItem(ListViewDataItem currentItem)
    {
      ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Position = (object) currentItem;
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == null || enumerator.Current.Enabled)
          return enumerator.Current;
      }
      return (ListViewDataItem) null;
    }

    protected internal virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      this.beginEditTimer.Stop();
      this.lastClickedItem = (ListViewDataItem) null;
      this.lastModifierKeys = Keys.None;
      this.groupSelectionTimer.Stop();
      int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
      int delta = Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
      int num2 = this.Scroller.Scrollbar.Value;
      this.ScrollTo(delta);
      return this.Scroller.Scrollbar.Value != num2;
    }

    protected internal virtual void ProcessSelection(
      ListViewDataItem item,
      Keys modifierKeys,
      bool isMouseSelection)
    {
      if (this.owner.OnSelectedItemChanging(new ListViewItemCancelEventArgs(item)))
        return;
      if (item == null)
      {
        this.ClearSelection();
        this.owner.SetSelectedItem((ListViewDataItem) null);
      }
      else
      {
        bool flag1 = (modifierKeys & Keys.Shift) == Keys.Shift;
        bool flag2 = (modifierKeys & Keys.Control) == Keys.Control;
        if (this.owner.MultiSelect && (flag1 && !flag2 || !flag2 || !isMouseSelection && !flag1 && !flag2))
          this.ClearSelection();
        if (this.owner.MultiSelect)
        {
          if (flag1)
          {
            ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
            if (enumerator == null)
              return;
            ListViewDataItemGroup viewDataItemGroup = item as ListViewDataItemGroup;
            if (viewDataItemGroup != null)
            {
              if (viewDataItemGroup.Items.Count == 0)
              {
                this.owner.CurrentItem = item;
                return;
              }
              item = viewDataItemGroup.Items[viewDataItemGroup.Items.Count - 1];
            }
            enumerator.Position = (object) null;
            bool flag3 = false;
            while (enumerator.MoveNext())
            {
              if (!flag3 && (enumerator.Current == item || enumerator.Current == this.anchor))
              {
                if (!(enumerator.Current is ListViewDataItemGroup))
                  enumerator.Current.Selected = true;
                flag3 = item != this.anchor;
              }
              else
              {
                if (flag3 && !(enumerator.Current is ListViewDataItemGroup))
                  enumerator.Current.Selected = true;
                if (enumerator.Current == item || enumerator.Current == this.anchor)
                  break;
              }
            }
            this.owner.SetSelectedItem(item);
          }
          else if (flag2)
          {
            if (isMouseSelection)
            {
              item.Selected = !item.Selected;
              if (item.Selected)
                this.owner.SetSelectedItem(item);
              else
                this.owner.SetSelectedItem(this.owner.SelectedItems.Count > 0 ? this.owner.SelectedItems[this.owner.SelectedItems.Count - 1] : (ListViewDataItem) null);
              this.anchor = item;
            }
          }
          else
          {
            item.Selected = true;
            this.owner.SetSelectedItem(item);
            this.anchor = item;
          }
        }
        else if (!flag2 && !(item is ListViewDataItemGroup))
        {
          this.ClearSelection();
          item.Selected = true;
          this.owner.SetSelectedItem(item);
          this.anchor = item;
        }
        this.owner.CurrentItem = item;
      }
    }

    protected void ProcessLassoSelection()
    {
      this.ProcessLassoSelection(this.GetSelectionRect());
    }

    protected virtual void ProcessLassoSelection(Rectangle selectionRect)
    {
      if (!this.owner.MultiSelect)
      {
        ListViewDataItem itemAt = this.GetItemAt(this.endPoint);
        if (itemAt == null || itemAt is ListViewDataItemGroup)
          return;
        this.ProcessSelection(itemAt, Keys.None, true);
      }
      else
      {
        ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
        int y = 0;
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Owner == this.owner)
          {
            Rectangle rectangle = new Rectangle(new Point(!this.owner.ShowGroups || this.owner.Groups.Count <= 0 || !this.owner.EnableCustomGrouping && !this.owner.EnableGrouping ? 0 : this.GroupIndent, y), enumerator.Current.ActualSize);
            if (this.RightToLeft)
              rectangle = LayoutUtils.RTLTranslateNonRelative(rectangle, this.ViewElement.ControlBoundingRectangle);
            this.ProcessItemLassoSelection(enumerator.Current, selectionRect.IntersectsWith(rectangle));
            y += enumerator.Current.ActualSize.Height + this.ItemSpacing;
          }
        }
      }
    }

    protected virtual void ProcessItemLassoSelection(
      ListViewDataItem currentItem,
      bool isIntersecting)
    {
      if (currentItem is ListViewDataItemGroup)
        return;
      switch (Control.ModifierKeys)
      {
        case Keys.Shift:
          currentItem.Selected = isIntersecting || this.lassoInitialSelection.ContainsKey(currentItem);
          if (!isIntersecting)
            break;
          this.lassoInitialSelection.Remove(currentItem);
          break;
        case Keys.Control:
          currentItem.Selected = isIntersecting ^ this.lassoInitialSelection.ContainsKey(currentItem);
          break;
        default:
          currentItem.Selected = isIntersecting;
          break;
      }
    }

    protected virtual void BeginLassoSelection()
    {
      this.lassoInitialSelection.Clear();
      if (Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift)
      {
        foreach (ListViewDataItem key in this.owner.Items)
        {
          if (key.Selected)
            this.lassoInitialSelection.Add(key, true);
        }
      }
      this.oldScrollOffset = this.Scroller.Scrollbar.Value;
      this.Capture = true;
      this.isLassoSelection = true;
    }

    protected virtual void EndLassoSelection()
    {
      this.Capture = false;
      this.lassoTimer.Stop();
      this.scrollBehavior.Stop();
      this.isLassoSelection = false;
      this.lassoInitialSelection.Clear();
      this.Invalidate();
    }

    protected virtual void OnLassoTimerTick(object sender, EventArgs e)
    {
      this.Scroller.Scrollbar.Value = Math.Max(0, Math.Min(this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1, this.Scroller.Scrollbar.Value + this.pointerOffset.Y));
    }

    public virtual Point GetDragHintLocation(
      BaseListViewVisualItem visualItem,
      Point mouseLocation)
    {
      if (this.DragHint == null)
        return Point.Empty;
      Rectangle screen = visualItem.ElementTree.Control.RectangleToScreen(visualItem.ControlBoundingRectangle);
      Padding empty = Padding.Empty;
      RadImageShape dragHint = this.DragHint;
      int height = dragHint.Image.Size.Height;
      Padding margins = dragHint.Margins;
      int num = mouseLocation.Y <= visualItem.Size.Height / 2 ? screen.Y : screen.Bottom;
      return new Point(screen.X - margins.Left, num - height / 2);
    }

    public virtual bool ShouldDropAfter(BaseListViewVisualItem targetElement, Point dropLocation)
    {
      return dropLocation.Y > targetElement.Size.Height / 2;
    }

    public virtual Size GetDragHintSize(ISupportDrop target)
    {
      if (this.DragHint == null)
        return Size.Empty;
      return new Size(this.owner.ControlBoundingRectangle.Width, this.owner.ViewElement.DragHint.Image.Size.Height);
    }

    private void groupSelectionTimer_Tick(object sender, EventArgs e)
    {
      this.groupSelectionTimer.Stop();
      if (this.lastClickedItem != null && this.lastClickedItem is ListViewDataItemGroup)
        this.ProcessSelection(this.lastClickedItem, this.lastModifierKeys, true);
      this.lastModifierKeys = Keys.None;
    }

    private void beginEditTimer_Tick(object sender, EventArgs e)
    {
      this.beginEditTimer.Stop();
      if (this.lastClickedItem != null && this.lastClickedItem.Selected)
        this.owner.BeginEdit();
      this.lastClickedItem = (ListViewDataItem) null;
    }

    protected virtual void OnScrollerUpdated()
    {
      this.owner.EndEdit();
      if (!this.Capture)
        return;
      int num = this.Scroller.Scrollbar.Value - this.oldScrollOffset;
      this.oldScrollOffset = this.Scroller.Scrollbar.Value;
      if (this.Orientation == Orientation.Vertical)
        this.startPoint.Y -= num;
      else
        this.startPoint.X -= num;
      this.ProcessLassoSelection(this.GetSelectionRect());
      this.Invalidate();
    }

    private void Scroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.OnScrollerUpdated();
    }

    private void typingTimer_Tick(object sender, EventArgs e)
    {
      this.typingTimer.Stop();
    }

    private Rectangle GetSelectionRect()
    {
      Rectangle rectangle = new Rectangle(new Point(Math.Min(this.startPoint.X, this.endPoint.X), Math.Min(this.startPoint.Y, this.endPoint.Y)), new Size(Math.Abs(this.endPoint.X - this.startPoint.X), Math.Abs(this.endPoint.Y - this.startPoint.Y)));
      if (this.Orientation == Orientation.Vertical)
        rectangle.Offset(0, this.Scroller.Scrollbar.Value);
      else
        rectangle.Offset(this.Scroller.Scrollbar.Value, 0);
      return rectangle;
    }

    protected int GetItemIndex(ListViewDataItem item)
    {
      ListViewTraverser enumerator = (ListViewTraverser) this.Scroller.Traverser.GetEnumerator();
      enumerator.Position = (object) null;
      int num = 0;
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == item)
          return num;
        ++num;
      }
      return -1;
    }

    protected void SetScrollValue(RadScrollBarElement scrollbar, int newValue)
    {
      if (newValue > scrollbar.Maximum - scrollbar.LargeChange + 1)
        newValue = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (newValue < scrollbar.Minimum)
        newValue = scrollbar.Minimum;
      scrollbar.Value = newValue;
    }
  }
}
