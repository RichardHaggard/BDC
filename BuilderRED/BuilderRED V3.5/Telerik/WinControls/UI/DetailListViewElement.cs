// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public class DetailListViewElement : BaseListViewElement
  {
    public static RadProperty ColumnDragHintProperty = RadProperty.Register(nameof (ColumnDragHint), typeof (RadImageShape), typeof (DetailListViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    private DetailListViewColumnContainer columnContainer;
    private DetailListViewColumnScroller columnScroller;
    private DetailListViewDragDropService columnDragDropService;
    private RadScrollBarElement columnsScrollBar;
    private Cursor originalMouseCursor;
    private int oldHorizontalScrollOffset;
    private float cornerCellWidth;
    private ListViewBestFitHelper bestFitHelper;

    public DetailListViewElement(RadListViewElement owner)
      : base(owner)
    {
      this.columnScroller.Traverser = (ITraverser<ListViewDetailColumn>) new ListViewColumnTraverser((IList<ListViewDetailColumn>) this.Owner.Columns);
      this.columnContainer.DataProvider = (IEnumerable) this.columnScroller;
      this.columnScroller.ScrollerUpdated += new EventHandler(this.columnScroller_ScrollerUpdated);
      this.columnDragDropService = new DetailListViewDragDropService(this);
      this.ScrollBehavior.ScrollServices.Add(new ScrollService((RadElement) this.ViewElement, this.columnsScrollBar));
      this.ItemSpacing = -1;
      this.bestFitHelper = new ListViewBestFitHelper(this);
    }

    [Browsable(false)]
    [VsbBrowsable(true)]
    public RadImageShape ColumnDragHint
    {
      get
      {
        return (RadImageShape) this.GetValue(DetailListViewElement.ColumnDragHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(DetailListViewElement.ColumnDragHintProperty, (object) value);
      }
    }

    public DetailListViewColumnContainer ColumnContainer
    {
      get
      {
        return this.columnContainer;
      }
    }

    public DetailListViewDragDropService ColumnDragDropService
    {
      get
      {
        return this.columnDragDropService;
      }
      set
      {
        this.columnDragDropService = value;
      }
    }

    public DetailListViewColumnScroller ColumnScroller
    {
      get
      {
        return this.columnScroller;
      }
    }

    public RadScrollBarElement ColumnScrollBar
    {
      get
      {
        return this.columnsScrollBar;
      }
    }

    public override int GroupIndent
    {
      get
      {
        if (this.IsDesignMode)
          return base.GroupIndent;
        return 0;
      }
      set
      {
        base.GroupIndent = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal ListViewBestFitHelper BestFitHelper
    {
      get
      {
        return this.bestFitHelper;
      }
    }

    protected override VirtualizedStackContainer<ListViewDataItem> CreateViewElement()
    {
      return (VirtualizedStackContainer<ListViewDataItem>) new DetailsListViewContainer((BaseListViewElement) this);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.columnContainer = new DetailListViewColumnContainer(this);
      this.columnContainer.StretchHorizontally = true;
      this.columnContainer.StretchVertically = true;
      this.columnContainer.Orientation = Orientation.Horizontal;
      this.columnContainer.ElementProvider = (IVirtualizedElementProvider<ListViewDetailColumn>) new DetailListViewHeaderCellElementProvider();
      this.Children.Add((RadElement) this.columnContainer);
      this.columnsScrollBar = new RadScrollBarElement();
      this.columnsScrollBar.ScrollType = ScrollType.Horizontal;
      this.columnsScrollBar.MinSize = new Size(0, RadScrollBarElement.HorizontalScrollBarHeight);
      this.columnsScrollBar.ScrollTimerDelay = 1;
      this.Children.Add((RadElement) this.columnsScrollBar);
      this.columnScroller = new DetailListViewColumnScroller();
      this.columnScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.columnScroller.ElementProvider = this.columnContainer.ElementProvider;
      this.columnScroller.Scrollbar = this.columnsScrollBar;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      this.MeasureChildElements(clientRectangle);
      ElementVisibility visibility = this.VScrollBar.Visibility;
      this.ViewElement.Measure(new SizeF(clientRectangle.Width - this.VScrollBar.DesiredSize.Width, availableSize.Height - this.columnsScrollBar.DesiredSize.Height - this.columnContainer.DesiredSize.Height));
      if (this.UpdateOnMeasure(availableSize) || visibility != this.VScrollBar.Visibility)
      {
        this.MeasureChildElements(clientRectangle);
        this.UpdateOnMeasure(availableSize);
        this.ViewElement.Measure(new SizeF(clientRectangle.Width - this.VScrollBar.DesiredSize.Width, availableSize.Height - this.columnsScrollBar.DesiredSize.Height - this.columnContainer.DesiredSize.Height));
      }
      this.cornerCellWidth = this.CalculateCornerCellWidth();
      if ((double) this.cornerCellWidth > 0.0 && this.Owner.ShowColumnHeaders)
        this.columnContainer.Measure(new SizeF(clientRectangle.Width - this.VScrollBar.DesiredSize.Width - this.cornerCellWidth, clientRectangle.Height));
      SizeF desiredSize = this.ViewElement.DesiredSize;
      desiredSize.Height += this.columnContainer.DesiredSize.Height + this.columnsScrollBar.DesiredSize.Height;
      desiredSize.Width += this.VScrollBar.DesiredSize.Width;
      return desiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.columnContainer.Visibility != ElementVisibility.Collapsed)
      {
        RectangleF rectangleF = new RectangleF(clientRectangle.X + this.cornerCellWidth, clientRectangle.Y, clientRectangle.Width - this.VScrollBar.DesiredSize.Width - this.cornerCellWidth, this.columnContainer.DesiredSize.Height);
        if (this.RightToLeft)
          rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRectangle);
        this.columnContainer.Arrange(rectangleF);
      }
      RectangleF viewElementRect = new RectangleF(clientRectangle.X, clientRectangle.Y + this.columnContainer.DesiredSize.Height, clientRectangle.Width - this.VScrollBar.DesiredSize.Width, clientRectangle.Height - this.columnContainer.DesiredSize.Height);
      if (this.RightToLeft)
        viewElementRect = LayoutUtils.RTLTranslateNonRelative(viewElementRect, clientRectangle);
      this.Layout.Arrange(clientRectangle);
      RectangleF hscrollBarRect = this.ArrangeHScrollBar(ref viewElementRect, clientRectangle);
      this.ArrangeVScrollBar(ref viewElementRect, hscrollBarRect, clientRectangle);
      viewElementRect.Width = Math.Max(1f, viewElementRect.Width);
      viewElementRect.Height = Math.Max(1f, viewElementRect.Height);
      this.ViewElement.Arrange(viewElementRect);
      return finalSize;
    }

    protected override RectangleF ArrangeHScrollBar(
      ref RectangleF viewElementRect,
      RectangleF clientRect)
    {
      RectangleF finalRect = RectangleF.Empty;
      if (this.columnsScrollBar.Visibility != ElementVisibility.Collapsed)
      {
        int num = (int) this.columnsScrollBar.DesiredSize.Height;
        if (num == 0)
          num = RadScrollBarElement.HorizontalScrollBarHeight;
        float y = clientRect.Bottom - (float) num;
        float width = clientRect.Width - this.VScrollBar.DesiredSize.Width;
        finalRect = new RectangleF(clientRect.X, y, width, (float) num);
        if (this.RightToLeft && this.VScrollBar.Visibility != ElementVisibility.Collapsed)
          finalRect.X += this.VScrollBar.DesiredSize.Width;
        this.columnsScrollBar.Arrange(finalRect);
        viewElementRect.Height -= this.columnsScrollBar.DesiredSize.Height;
      }
      return finalRect;
    }

    protected override bool UpdateOnMeasure(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      RadScrollBarElement columnsScrollBar = this.columnsScrollBar;
      RadScrollBarElement vscrollBar = this.VScrollBar;
      ElementVisibility visibility1 = columnsScrollBar.Visibility;
      ElementVisibility visibility2 = vscrollBar.Visibility;
      SizeF size = clientRectangle.Size;
      size.Height -= this.columnContainer.DesiredSize.Height;
      size.Width -= this.CalculateCornerCellWidth();
      if (columnsScrollBar.Visibility == ElementVisibility.Visible)
        size.Height -= this.columnsScrollBar.DesiredSize.Height;
      this.Scroller.ClientSize = size;
      this.columnScroller.ClientSize = size;
      this.columnScroller.UpdateScrollRange();
      this.Scroller.UpdateScrollRange();
      bool flag = false;
      if (vscrollBar.Visibility == ElementVisibility.Visible)
      {
        size.Width -= vscrollBar.DesiredSize.Width;
        this.Scroller.ClientSize = size;
        this.columnScroller.ClientSize = size;
        flag = true;
      }
      if (columnsScrollBar.Visibility != visibility1 && columnsScrollBar.Visibility == ElementVisibility.Visible)
      {
        size.Height -= this.columnsScrollBar.DesiredSize.Height;
        this.Scroller.ClientSize = size;
        this.columnScroller.ClientSize = size;
        flag = true;
      }
      if (flag)
      {
        this.columnScroller.UpdateScrollRange();
        this.Scroller.UpdateScrollRange();
      }
      if (visibility1 == columnsScrollBar.Visibility)
        return visibility2 != this.columnsScrollBar.Visibility;
      return true;
    }

    public override BaseListViewVisualItem GetVisualItemAt(Point location)
    {
      RadElement radElement = this.ElementTree.GetElementAtPoint(location);
      while (radElement != null && !(radElement is BaseListViewVisualItem))
        radElement = radElement.Parent;
      return radElement as BaseListViewVisualItem;
    }

    protected override void HandleLeftKey(KeyEventArgs e)
    {
      int num = this.Owner.Columns.IndexOf(this.Owner.CurrentColumn);
      ITraverser<ListViewDetailColumn> enumerator = (ITraverser<ListViewDetailColumn>) this.columnScroller.Traverser.GetEnumerator();
      enumerator.Position = (object) num;
      if (!enumerator.MovePrevious())
        return;
      this.Owner.CurrentColumn = enumerator.Current;
    }

    protected override void HandleRightKey(KeyEventArgs e)
    {
      int num = this.Owner.Columns.IndexOf(this.Owner.CurrentColumn);
      ITraverser<ListViewDetailColumn> enumerator = (ITraverser<ListViewDetailColumn>) this.columnScroller.Traverser.GetEnumerator();
      enumerator.Position = (object) num;
      if (!enumerator.MoveNext())
        return;
      this.Owner.CurrentColumn = enumerator.Current;
    }

    protected internal override bool ProcessMouseMove(MouseEventArgs e)
    {
      bool flag = base.ProcessMouseMove(e);
      this.Owner.ColumnResizingBehavior.HandleMouseMove(e.Location);
      DetailListViewHeaderCellElement elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as DetailListViewHeaderCellElement;
      if (this.Owner.ColumnResizingBehavior.IsResizing)
        return flag;
      if (elementAtPoint != null && elementAtPoint.IsInResizeLocation(e.Location))
      {
        if (this.originalMouseCursor == (Cursor) null)
          this.originalMouseCursor = this.ElementTree.Control.Cursor;
        this.ElementTree.Control.Cursor = Cursors.SizeWE;
      }
      else if (this.originalMouseCursor != (Cursor) null)
      {
        this.ElementTree.Control.Cursor = this.originalMouseCursor;
        this.originalMouseCursor = (Cursor) null;
      }
      return flag;
    }

    protected internal override bool ProcessMouseUp(MouseEventArgs e)
    {
      if (this.Owner.EnableKineticScrolling && this.ViewElement.ContainsMouse)
      {
        this.ScrollBehavior.MouseUp(e.Location);
      }
      else
      {
        if (this.Capture)
        {
          base.ProcessMouseUp(e);
          return false;
        }
        this.ScrollBehavior.Stop();
      }
      bool isResizing = this.Owner.ColumnResizingBehavior.IsResizing;
      this.Owner.ColumnResizingBehavior.EndResize();
      if (e.Button == MouseButtons.Left)
      {
        ListViewDataItem itemAt = this.GetItemAt(e.Location);
        DetailListViewCellElement elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as DetailListViewCellElement;
        DetailListViewHeaderCellElement headerCellUnderMouse = elementAtPoint as DetailListViewHeaderCellElement;
        if (headerCellUnderMouse != null && !isResizing)
          this.SortByHeaderCell(headerCellUnderMouse, e.Location);
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
          if (itemAt != null && !this.disableGroupSelectOnMouseUp)
          {
            this.lastClickedItem = itemAt;
            this.lastModifierKeys = Control.ModifierKeys;
            this.groupSelectionTimer.Start();
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
        bool flag = this.ElementTree.GetElementAtPoint(e.Location) is ListViewItemCheckbox;
        if (itemAt.Selected && this.Owner.ShowCheckBoxes && (!this.Owner.AllowEdit && this.Owner.CheckOnClickMode != CheckOnClickMode.Off) && !flag)
          this.ToggleItemCheckState(itemAt);
        else if (!itemAt.Selected && this.Owner.ShowCheckBoxes && (this.Owner.CheckOnClickMode == CheckOnClickMode.FirstClick && !flag))
          this.ToggleItemCheckState(itemAt);
        if (itemAt != null && !this.disableEditOnMouseUp && (itemAt == this.Owner.SelectedItem && Control.ModifierKeys == Keys.None) && (this.lastClickedItem == null && elementAtPoint != null && elementAtPoint.Data == this.Owner.CurrentColumn))
        {
          this.lastClickedItem = itemAt;
          this.beginEditTimer.Start();
        }
        else
        {
          this.beginEditTimer.Stop();
          this.lastClickedItem = (ListViewDataItem) null;
          if (Control.ModifierKeys != Keys.None || itemAt != this.Owner.SelectedItem)
            this.ProcessSelection(itemAt, Control.ModifierKeys, true);
          if (elementAtPoint != null && elementAtPoint.Data != null)
            elementAtPoint.Data.Current = true;
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

    protected override void ProcessLassoSelection(Rectangle selectionRect)
    {
      if (!this.Owner.MultiSelect)
      {
        base.ProcessLassoSelection(selectionRect);
      }
      else
      {
        ListViewTraverser enumerator = this.Scroller.Traverser.GetEnumerator() as ListViewTraverser;
        int headerHeight = (int) this.Owner.HeaderHeight;
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Owner == this.Owner)
          {
            Rectangle rectangle = new Rectangle(new Point(!this.Owner.ShowGroups || this.Owner.Groups.Count <= 0 || !this.Owner.EnableCustomGrouping && !this.Owner.EnableGrouping ? 0 : this.GroupIndent, headerHeight), enumerator.Current.ActualSize);
            if (this.RightToLeft)
              rectangle = LayoutUtils.RTLTranslateNonRelative(rectangle, this.ViewElement.ControlBoundingRectangle);
            this.ProcessItemLassoSelection(enumerator.Current, selectionRect.IntersectsWith(rectangle));
            headerHeight += enumerator.Current.ActualSize.Height + this.ItemSpacing;
          }
        }
      }
    }

    protected override void OnLassoTimerTick(object sender, EventArgs e)
    {
      this.Scroller.Scrollbar.Value = Math.Max(0, Math.Min(this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1, this.Scroller.Scrollbar.Value + this.pointerOffset.Y));
      this.ColumnScroller.Scrollbar.Value = Math.Max(0, Math.Min(this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1, this.ColumnScroller.Scrollbar.Value + this.pointerOffset.X));
    }

    protected override bool SupportsOrientation(Orientation orientation)
    {
      return orientation == Orientation.Vertical;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      if (args.IsBegin && (this.Scroller.Scrollbar.ControlBoundingRectangle.Contains(args.Location) || this.ColumnScroller.Scrollbar.ControlBoundingRectangle.Contains(args.Location) || this.ColumnContainer.ControlBoundingRectangle.Contains(args.Location)))
        return;
      int num1 = this.Scroller.Scrollbar.Value - args.Offset.Height;
      if (num1 > this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1)
        num1 = this.Scroller.Scrollbar.Maximum - this.Scroller.Scrollbar.LargeChange + 1;
      if (num1 < this.Scroller.Scrollbar.Minimum)
        num1 = this.Scroller.Scrollbar.Minimum;
      this.Scroller.Scrollbar.Value = num1;
      int num2 = this.ColumnScroller.Scrollbar.Value - args.Offset.Width;
      if (num2 > this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1)
        num2 = this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1;
      if (num2 < this.ColumnScroller.Scrollbar.Minimum)
        num2 = this.ColumnScroller.Scrollbar.Minimum;
      this.ColumnScroller.Scrollbar.Value = num2;
      args.Handled = true;
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName == "HorizontalScrollState")
        this.columnScroller.ScrollState = this.HorizontalScrollState;
      if (!(propertyName == "VerticalScrollState"))
        return;
      this.Scroller.ScrollState = this.VerticalScrollState;
    }

    public virtual void EnsureColumnVisible(ListViewDetailColumn column)
    {
      DetailListViewHeaderCellElement headerCell = this.GetHeaderCell(column);
      if (headerCell == null)
      {
        this.columnContainer.UpdateLayout();
        if (this.columnContainer.Children.Count > 0)
        {
          if (this.Owner.Columns.IndexOf(column) <= this.Owner.Columns.IndexOf(((DetailListViewCellElement) this.columnContainer.Children[0]).Data))
            this.columnScroller.ScrollToItem(column, false);
          else
            this.EnsureColumnVisibleCore(column);
        }
      }
      else if (headerCell.ControlBoundingRectangle.Width > this.columnContainer.ControlBoundingRectangle.Width)
        this.SetScrollValue(this.ColumnScrollBar, this.ColumnScrollBar.Value - (this.RightToLeft ? this.columnContainer.ControlBoundingRectangle.Right - headerCell.ControlBoundingRectangle.Right : this.columnContainer.ControlBoundingRectangle.Left - headerCell.ControlBoundingRectangle.Left) * (this.RightToLeft ? -1 : 1));
      else if (headerCell.ControlBoundingRectangle.Right > this.columnContainer.ControlBoundingRectangle.Right)
        this.SetScrollValue(this.ColumnScrollBar, this.ColumnScrollBar.Value + (headerCell.ControlBoundingRectangle.Right - this.columnContainer.ControlBoundingRectangle.Right) * (this.RightToLeft ? -1 : 1));
      else if (headerCell.ControlBoundingRectangle.Left < this.columnContainer.ControlBoundingRectangle.Left)
        this.SetScrollValue(this.ColumnScrollBar, this.ColumnScrollBar.Value - (this.columnContainer.ControlBoundingRectangle.Left - headerCell.ControlBoundingRectangle.Left) * (this.RightToLeft ? -1 : 1));
      this.ViewElement.InvalidateMeasure();
      this.UpdateLayout();
    }

    private DetailListViewHeaderCellElement GetHeaderCell(
      ListViewDetailColumn column)
    {
      foreach (DetailListViewHeaderCellElement child in this.columnContainer.Children)
      {
        if (child.Data == column)
          return child;
      }
      return (DetailListViewHeaderCellElement) null;
    }

    protected virtual void EnsureColumnVisibleCore(ListViewDetailColumn column)
    {
      bool flag1 = false;
      int num = 0;
      ListViewDetailColumn data = ((DetailListViewCellElement) this.columnContainer.Children[this.columnContainer.Children.Count - 1]).Data;
      ItemsTraverser<ListViewDetailColumn> enumerator = (ItemsTraverser<ListViewDetailColumn>) this.ColumnScroller.Traverser.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current == column)
        {
          this.SetScrollValue(this.ColumnScrollBar, this.ColumnScrollBar.Value + num);
          this.UpdateLayout();
          DetailListViewHeaderCellElement headerCell = this.GetHeaderCell(column);
          bool flag2 = headerCell != null && headerCell.ControlBoundingRectangle.Right > this.columnContainer.ControlBoundingRectangle.Right;
          bool flag3 = headerCell != null && headerCell.ControlBoundingRectangle.Left < this.columnContainer.ControlBoundingRectangle.Left && this.RightToLeft;
          if (!flag2 && !flag3)
            break;
          this.EnsureColumnVisible(column);
          break;
        }
        if (enumerator.Current == data)
          flag1 = true;
        if (flag1)
          num += (int) this.columnContainer.ElementProvider.GetElementSize(enumerator.Current).Width + this.columnContainer.ItemSpacing;
      }
    }

    protected virtual float CalculateCornerCellWidth()
    {
      float val2 = this.ViewElement.Children.Count != 0 || !this.Owner.ShowCheckBoxes ? 0.0f : 16f;
      foreach (RadElement child in this.ViewElement.Children)
      {
        DetailListViewVisualItem listViewVisualItem = child as DetailListViewVisualItem;
        if (listViewVisualItem != null)
          val2 = Math.Max(listViewVisualItem.ToggleElement.DesiredSize.Width, val2);
      }
      return val2;
    }

    private void SortByHeaderCell(
      DetailListViewHeaderCellElement headerCellUnderMouse,
      Point location)
    {
      ListViewDetailColumn data = headerCellUnderMouse.Data;
      if (!data.Owner.EnableSorting || !data.Owner.EnableColumnSort || (headerCellUnderMouse.IsInResizeLocation(location) || data.Owner.ColumnResizingBehavior.IsResizing))
        return;
      int index = data.Owner.SortDescriptors.IndexOf(data.Name);
      ListSortDirection direction = ListSortDirection.Ascending;
      bool flag = true;
      if (index >= 0)
      {
        direction = data.Owner.SortDescriptors[index].Direction;
        if (direction == ListSortDirection.Ascending)
          direction = ListSortDirection.Descending;
        else
          flag = false;
      }
      data.Owner.SortDescriptors.BeginUpdate();
      data.Owner.SortDescriptors.Clear();
      if (flag)
        data.Owner.SortDescriptors.Add(data.Name, direction);
      data.Owner.SortDescriptors.EndUpdate();
      headerCellUnderMouse.Synchronize();
    }

    private void MeasureChildElements(RectangleF clientRect)
    {
      if (this.columnsScrollBar.Visibility != ElementVisibility.Collapsed)
        this.columnsScrollBar.Measure(clientRect.Size);
      if (this.VScrollBar.Visibility != ElementVisibility.Collapsed)
        this.VScrollBar.Measure(clientRect.Size);
      if (this.Owner.ShowColumnHeaders)
      {
        this.columnContainer.Visibility = ElementVisibility.Visible;
        this.columnContainer.Measure(new SizeF(clientRect.Width - this.VScrollBar.DesiredSize.Width, clientRect.Height));
      }
      else
        this.columnContainer.Visibility = ElementVisibility.Collapsed;
    }

    protected internal virtual bool CanBestFit()
    {
      if (this.ElementState == ElementState.Loaded && !this.IsLayoutSuspended)
        return !this.Owner.IsLayoutSuspended;
      return false;
    }

    public void BestFitColumn(ListViewDetailColumn column)
    {
      this.BestFitHelper.BestFitColumn(column);
    }

    public void BestFitColumns()
    {
      this.bestFitHelper.BestFitColumns();
    }

    public void BestFitColumns(ListViewBestFitColumnMode mode)
    {
      this.bestFitHelper.BestFitColumns(mode);
    }

    private void columnScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.Owner.EndEdit();
      this.columnContainer.InvalidateMeasure();
      this.ViewElement.InvalidateArrange();
      if (!this.Capture)
        return;
      this.startPoint.X -= this.ColumnScroller.Scrollbar.Value - this.oldHorizontalScrollOffset;
      this.oldHorizontalScrollOffset = this.ColumnScroller.Scrollbar.Value;
      this.ProcessLassoSelection();
      this.Invalidate();
    }
  }
}
