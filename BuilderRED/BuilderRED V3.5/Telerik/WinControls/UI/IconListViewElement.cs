// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IconListViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class IconListViewElement : BaseListViewElement
  {
    public IconListViewElement(RadListViewElement owner)
      : base(owner)
    {
    }

    private int GetItemFlowOffset(ListViewDataItem item)
    {
      if (this.Orientation != Orientation.Vertical)
        return item.ActualSize.Height;
      return item.ActualSize.Width;
    }

    protected virtual ListViewDataItem GetUpperItem(ListViewDataItem currentItem)
    {
      ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Position = (object) currentItem;
      int num1 = 0;
      while (enumerator.MovePrevious() && enumerator.Current != null && !enumerator.Current.IsLastInRow)
        num1 += this.GetItemFlowOffset(enumerator.Current);
      if (!(currentItem is ListViewDataItemGroup) && currentItem != null)
        num1 += this.GetItemFlowOffset(currentItem) / 2;
      if (enumerator.Position == null)
        return (ListViewDataItem) null;
      do
        ;
      while (enumerator.MovePrevious() && enumerator.Current != null && !enumerator.Current.IsLastInRow);
      int num2 = 0;
      while (enumerator.MoveNext() && !enumerator.Current.IsLastInRow && num2 + this.GetItemFlowOffset(enumerator.Current) < num1)
        num2 += this.GetItemFlowOffset(enumerator.Current);
      return enumerator.Current;
    }

    protected virtual ListViewDataItem GetDownerItem(ListViewDataItem currentItem)
    {
      ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Position = (object) currentItem;
      int num1 = 0;
      while (enumerator.MovePrevious() && enumerator.Current != null && !enumerator.Current.IsLastInRow)
        num1 += this.GetItemFlowOffset(enumerator.Current);
      if (!(currentItem is ListViewDataItemGroup) && currentItem != null)
        num1 += this.GetItemFlowOffset(currentItem) / 2;
      enumerator.Position = (object) currentItem;
      do
        ;
      while (currentItem != null && !currentItem.IsLastInRow && (enumerator.MoveNext() && !enumerator.Current.IsLastInRow));
      int num2 = 0;
      while (enumerator.MoveNext())
      {
        if (num2 + this.GetItemFlowOffset(enumerator.Current) >= num1)
          return enumerator.Current;
        num2 += this.GetItemFlowOffset(enumerator.Current);
        if (enumerator.Current.IsLastInRow)
          break;
      }
      return (ListViewDataItem) null;
    }

    public override bool FullRowSelect
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ItemSize = new Size(64, 64);
    }

    protected override VirtualizedStackContainer<ListViewDataItem> CreateViewElement()
    {
      return (VirtualizedStackContainer<ListViewDataItem>) new IconListViewContainer((BaseListViewElement) this);
    }

    protected override ItemScroller<ListViewDataItem> CreateItemScroller()
    {
      return (ItemScroller<ListViewDataItem>) new IconListViewScroller();
    }

    protected override void HandleDownKey(KeyEventArgs e)
    {
      ListViewDataItem listViewDataItem = this.Orientation == Orientation.Vertical ? this.GetDownerItem(this.Owner.CurrentItem) : this.GetNextItem(this.Owner.CurrentItem);
      if (listViewDataItem == null)
        return;
      this.ProcessSelection(listViewDataItem, Control.ModifierKeys, false);
    }

    protected override void HandleLeftKey(KeyEventArgs e)
    {
      ListViewDataItemGroup currentItem = this.Owner.CurrentItem as ListViewDataItemGroup;
      if (currentItem != null)
      {
        currentItem.Expanded = false;
      }
      else
      {
        ListViewDataItem listViewDataItem = this.Orientation == Orientation.Vertical ? this.GetPreviousItem(this.Owner.CurrentItem) : this.GetUpperItem(this.Owner.CurrentItem);
        if (listViewDataItem == null)
          return;
        this.ProcessSelection(listViewDataItem, Control.ModifierKeys, false);
      }
    }

    protected override void HandleRightKey(KeyEventArgs e)
    {
      ListViewDataItemGroup currentItem = this.Owner.CurrentItem as ListViewDataItemGroup;
      if (currentItem != null)
      {
        currentItem.Expanded = true;
      }
      else
      {
        ListViewDataItem listViewDataItem = this.Orientation == Orientation.Vertical ? this.GetNextItem(this.Owner.CurrentItem) : this.GetDownerItem(this.Owner.CurrentItem);
        if (listViewDataItem == null)
          return;
        this.ProcessSelection(listViewDataItem, Control.ModifierKeys, false);
      }
    }

    protected override void HandleUpKey(KeyEventArgs e)
    {
      ListViewDataItem listViewDataItem = this.Orientation == Orientation.Vertical ? this.GetUpperItem(this.Owner.CurrentItem) : this.GetPreviousItem(this.Owner.CurrentItem);
      if (listViewDataItem == null)
        return;
      this.ProcessSelection(listViewDataItem, Control.ModifierKeys, false);
    }

    protected override void EnsureItemVisibleVertical(ListViewDataItem item)
    {
      if (this.Orientation == Orientation.Vertical)
      {
        base.EnsureItemVisibleVertical(item);
      }
      else
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
        else if (element.ControlBoundingRectangle.Right > this.ViewElement.ControlBoundingRectangle.Right)
        {
          this.SetScrollValue(this.HScrollBar, this.HScrollBar.Value + (element.ControlBoundingRectangle.Right - this.ViewElement.ControlBoundingRectangle.Right));
        }
        else
        {
          if (element.ControlBoundingRectangle.Left >= this.ViewElement.ControlBoundingRectangle.Left)
            return;
          this.SetScrollValue(this.HScrollBar, this.HScrollBar.Value - (this.ViewElement.ControlBoundingRectangle.Left - element.ControlBoundingRectangle.Left));
        }
      }
    }

    protected override void EnsureItemVisibleVerticalCore(ListViewDataItem item)
    {
      if (this.Orientation == Orientation.Horizontal)
      {
        this.EnsureItemVisibleHorizontal(item);
      }
      else
      {
        int val1 = 0;
        BaseListViewVisualItem element1 = this.GetElement(item);
        if (element1 == null)
        {
          this.Scroller.ScrollToItem(item);
          this.Scroller.Scrollbar.PerformLargeDecrement(1);
          this.ViewElement.UpdateLayout();
          element1 = this.GetElement(item);
        }
        if (element1 != null)
        {
          if (element1.ControlBoundingRectangle.Bottom <= this.ViewElement.ControlBoundingRectangle.Bottom)
            return;
          this.EnsureItemVisible(item);
        }
        else
        {
          ListViewTraverser enumerator = (ListViewTraverser) this.Scroller.Traverser.GetEnumerator();
          while (enumerator.MoveNext())
          {
            val1 = Math.Max(val1, (int) this.ViewElement.ElementProvider.GetElementSize(enumerator.Current).Height + this.ItemSpacing);
            if (enumerator.Current.IsLastInRow)
            {
              this.SetScrollValue(this.VScrollBar, this.VScrollBar.Value + val1);
              val1 = 0;
              this.UpdateLayout();
              BaseListViewVisualItem element2 = this.GetElement(item);
              if (element2 != null)
              {
                if (element2.ControlBoundingRectangle.Bottom <= this.ViewElement.ControlBoundingRectangle.Bottom)
                  break;
                this.EnsureItemVisible(item);
                break;
              }
            }
          }
        }
      }
    }

    protected override void EnsureItemVisibleHorizontal(ListViewDataItem item)
    {
      int val1 = 0;
      BaseListViewVisualItem element1 = this.GetElement(item);
      if (element1 == null)
      {
        this.Scroller.ScrollToItem(item);
        this.Scroller.Scrollbar.PerformLargeDecrement(1);
        this.ViewElement.UpdateLayout();
        element1 = this.GetElement(item);
      }
      if (element1 != null)
      {
        if (element1.ControlBoundingRectangle.Right <= this.ViewElement.ControlBoundingRectangle.Right)
          return;
        this.EnsureItemVisible(item);
      }
      else
      {
        ListViewTraverser enumerator = (ListViewTraverser) this.Scroller.Traverser.GetEnumerator();
        while (enumerator.MoveNext())
        {
          val1 = Math.Max(val1, (int) this.ViewElement.ElementProvider.GetElementSize(enumerator.Current).Width + this.ItemSpacing);
          if (enumerator.Current.IsLastInRow)
          {
            this.SetScrollValue(this.HScrollBar, this.HScrollBar.Value + val1);
            val1 = 0;
            this.UpdateLayout();
            BaseListViewVisualItem element2 = this.GetElement(item);
            if (element2 != null)
            {
              if (element2.ControlBoundingRectangle.Right <= this.ViewElement.ControlBoundingRectangle.Right)
                break;
              this.EnsureItemVisible(item);
              break;
            }
          }
        }
      }
    }

    protected override void OnOrientationChanged()
    {
      this.ScrollBehavior.ScrollServices.Clear();
      if (this.Orientation == Orientation.Vertical)
      {
        this.VScrollBar.ValueChanged -= new EventHandler(((VirtualizedScrollPanel<ListViewDataItem, BaseListViewVisualItem>) this).HScrollBar_ValueChanged);
        this.Scroller.Scrollbar = this.VScrollBar;
        this.HScrollBar.ValueChanged += new EventHandler(((VirtualizedScrollPanel<ListViewDataItem, BaseListViewVisualItem>) this).HScrollBar_ValueChanged);
      }
      else
      {
        this.HScrollBar.ValueChanged -= new EventHandler(((VirtualizedScrollPanel<ListViewDataItem, BaseListViewVisualItem>) this).HScrollBar_ValueChanged);
        this.Scroller.Scrollbar = this.HScrollBar;
        this.VScrollBar.ValueChanged += new EventHandler(((VirtualizedScrollPanel<ListViewDataItem, BaseListViewVisualItem>) this).HScrollBar_ValueChanged);
      }
      this.ScrollBehavior.ScrollServices.Add(new ScrollService((RadElement) this, this.Scroller.Scrollbar));
      this.Scroller.ScrollState = this.Orientation == Orientation.Vertical ? this.VerticalScrollState : this.HorizontalScrollState;
      this.UpdateFitToSizeMode();
      this.Scroller.UpdateScrollRange();
      this.Scroller.UpdateScrollStep();
      this.UpdateLayout();
      base.OnOrientationChanged();
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (!(propertyName == "HorizontalScrollState") && !(propertyName == "VerticalScrollState"))
        return;
      this.Scroller.ScrollState = this.Orientation == Orientation.Vertical ? this.VerticalScrollState : this.HorizontalScrollState;
    }

    protected override bool UpdateOnMeasure(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      RadScrollBarElement scrollBarElement1 = this.HScrollBar;
      RadScrollBarElement scrollBarElement2 = this.VScrollBar;
      if (this.Orientation == Orientation.Horizontal)
      {
        scrollBarElement1 = this.VScrollBar;
        scrollBarElement2 = this.HScrollBar;
      }
      ElementVisibility visibility = scrollBarElement1.Visibility;
      if (this.FitItemsToSize)
        scrollBarElement1.Visibility = ElementVisibility.Collapsed;
      else if (this.Orientation == Orientation.Vertical)
      {
        scrollBarElement1.LargeChange = Math.Max(0, (int) ((double) clientRectangle.Width - (double) scrollBarElement2.DesiredSize.Width - (double) this.ViewElement.Margin.Horizontal));
        scrollBarElement1.SmallChange = scrollBarElement1.LargeChange / 10;
        scrollBarElement1.Visibility = scrollBarElement1.LargeChange < scrollBarElement1.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
      else
      {
        scrollBarElement1.LargeChange = Math.Max(0, (int) ((double) clientRectangle.Height - (double) scrollBarElement2.DesiredSize.Height - (double) this.ViewElement.Margin.Vertical));
        scrollBarElement1.SmallChange = scrollBarElement1.LargeChange / 10;
        scrollBarElement1.Visibility = scrollBarElement1.LargeChange < scrollBarElement1.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
      this.Scroller.ClientSize = clientRectangle.Size;
      return visibility != scrollBarElement1.Visibility;
    }

    protected override void UpdateFitToSizeMode()
    {
      (this.Orientation == Orientation.Vertical ? this.HScrollBar : this.VScrollBar).Maximum = 0;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.BoundsProperty)
        return;
      this.Scroller.UpdateScrollRange();
    }

    protected override void scroller_ScrollerUpdated(object sender, EventArgs e)
    {
      if (this.Orientation == Orientation.Vertical)
        this.ViewElement.ScrollOffset = new SizeF(this.ViewElement.ScrollOffset.Width, (float) -this.Scroller.ScrollOffset);
      else
        this.ViewElement.ScrollOffset = new SizeF((float) -this.Scroller.ScrollOffset, this.ViewElement.ScrollOffset.Height);
    }

    protected override void HScrollBar_ValueChanged(object sender, EventArgs e)
    {
      if (this.Orientation == Orientation.Vertical)
        this.ViewElement.ScrollOffset = new SizeF((float) -this.HScrollBar.Value, this.ViewElement.ScrollOffset.Height);
      else
        this.ViewElement.ScrollOffset = new SizeF(this.ViewElement.ScrollOffset.Width, (float) -this.VScrollBar.Value);
      this.ViewElement.InvalidateMeasure();
    }

    protected override void OnScrollerUpdated()
    {
      base.OnScrollerUpdated();
      this.ViewElement.InvalidateMeasure();
      this.ViewElement.Invalidate();
    }

    protected override void ProcessLassoSelection(Rectangle selectionRect)
    {
      if (!this.Owner.MultiSelect)
      {
        base.ProcessLassoSelection(selectionRect);
      }
      else
      {
        Rectangle rectangle1 = new Rectangle(this.ViewElement.Padding.Left, this.ViewElement.Padding.Top, this.ViewElement.Size.Width - this.ViewElement.Padding.Horizontal, this.ViewElement.Size.Height - this.ViewElement.Padding.Vertical);
        if (this.RightToLeft)
          rectangle1 = LayoutUtils.RTLTranslateNonRelative(rectangle1, this.ControlBoundingRectangle);
        selectionRect.Offset(-rectangle1.Location.X, -rectangle1.Location.Y);
        ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
        int x1 = rectangle1.X;
        int y = rectangle1.Y;
        enumerator.Reset();
        int val1_1 = 0;
        int val1_2 = 0;
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Owner == this.Owner)
          {
            Size actualSize = enumerator.Current.ActualSize;
            val1_1 = Math.Max(val1_1, actualSize.Height);
            val1_2 = Math.Max(val1_2, actualSize.Width);
            if (enumerator.Current is ListViewDataItemGroup)
            {
              if (x1 != rectangle1.X)
              {
                int x2 = rectangle1.X;
                y += val1_1;
                val1_1 = actualSize.Height;
              }
              x1 = rectangle1.X;
              y += actualSize.Height + this.ItemSpacing;
            }
            else
            {
              if (x1 + actualSize.Width > rectangle1.Right && this.Orientation == Orientation.Vertical)
              {
                x1 = rectangle1.X;
                y += val1_1 + this.ItemSpacing;
                val1_1 = actualSize.Height;
              }
              else if (y + actualSize.Height > rectangle1.Bottom && this.Orientation == Orientation.Horizontal)
              {
                y = rectangle1.Y;
                x1 += val1_2 + this.ItemSpacing;
                val1_2 = actualSize.Width;
              }
              if (x1 == rectangle1.X && this.Owner.ShowGroups && (this.Owner.EnableCustomGrouping || this.Owner.EnableGrouping) && (this.Owner.Groups.Count > 0 && !this.Owner.FullRowSelect))
                x1 += this.Owner.GroupIndent;
              Rectangle rectangle2 = new Rectangle(new Point(x1, y), actualSize);
              if (this.RightToLeft)
                rectangle2 = LayoutUtils.RTLTranslateNonRelative(rectangle2, rectangle1);
              this.ProcessItemLassoSelection(enumerator.Current, selectionRect.IntersectsWith(rectangle2));
              if (this.Orientation == Orientation.Vertical)
                x1 += actualSize.Width + this.ItemSpacing;
              else
                y += actualSize.Height + this.ItemSpacing;
            }
          }
        }
      }
    }

    protected override void OnLassoTimerTick(object sender, EventArgs e)
    {
      this.Scroller.Scrollbar.Value = Math.Max(0, Math.Min(this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1, this.Scroller.Scrollbar.Value + (this.Orientation == Orientation.Vertical ? this.pointerOffset.Y : this.pointerOffset.X)));
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      if (this.Scroller.Scrollbar.ControlBoundingRectangle.Contains(args.Location))
        return;
      int num = this.Scroller.Scrollbar.Value - (this.Orientation == Orientation.Vertical ? args.Offset.Height : args.Offset.Width);
      if (num > this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1)
        num = this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1;
      if (num < this.Scroller.Scrollbar.Minimum)
        num = this.Scroller.Scrollbar.Minimum;
      this.Scroller.Scrollbar.Value = num;
      args.Handled = true;
    }

    public override Point GetDragHintLocation(
      BaseListViewVisualItem visualItem,
      Point mouseLocation)
    {
      if (this.Orientation == Orientation.Horizontal)
        return base.GetDragHintLocation(visualItem, mouseLocation);
      Rectangle screen = visualItem.ElementTree.Control.RectangleToScreen(visualItem.ControlBoundingRectangle);
      Padding empty = Padding.Empty;
      RadImageShape dragHint = this.DragHint;
      if (dragHint == null)
        return Point.Empty;
      int width = dragHint.Image.Size.Width;
      Padding margins = dragHint.Margins;
      return new Point((mouseLocation.X <= visualItem.Size.Width / 2 ? screen.X : screen.Right) - width / 2, screen.Y - margins.Top);
    }

    public override bool ShouldDropAfter(BaseListViewVisualItem targetElement, Point dropLocation)
    {
      if (this.Orientation == Orientation.Horizontal)
        return dropLocation.Y > targetElement.Size.Height / 2;
      return dropLocation.X > targetElement.Size.Width / 2;
    }

    public override Size GetDragHintSize(ISupportDrop target)
    {
      BaseListViewVisualItem listViewVisualItem = target as BaseListViewVisualItem;
      if (listViewVisualItem == null || this.DragHint == null)
        return Size.Empty;
      if (this.Orientation == Orientation.Horizontal)
        return new Size(listViewVisualItem.Size.Width, this.DragHint.Image.Size.Height);
      return new Size(this.DragHint.Image.Size.Width, listViewVisualItem.Size.Height);
    }
  }
}
