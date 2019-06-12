// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGridViewDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadGridViewDragDropService : RadDragDropService
  {
    private bool allowAutoScrollRowsWhileDragging = true;
    private bool allowAutoScrollColumnsWhileDragging = true;
    private RadGridViewElement gridViewElement;
    private RadLayeredWindow dragHintWindow;
    private IGridDragDropBehavior dragDropBehavior;
    private RadPosition lastDropPosition;

    public RadGridViewDragDropService(RadGridViewElement gridViewElement)
    {
      this.gridViewElement = gridViewElement;
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    public IGridDragDropBehavior Behavior
    {
      get
      {
        return this.dragDropBehavior;
      }
    }

    public bool AllowAutoScrollRowsWhileDragging
    {
      get
      {
        return this.allowAutoScrollRowsWhileDragging;
      }
      set
      {
        this.allowAutoScrollRowsWhileDragging = value;
      }
    }

    public bool AllowAutoScrollColumnsWhileDragging
    {
      get
      {
        return this.allowAutoScrollColumnsWhileDragging;
      }
      set
      {
        this.allowAutoScrollColumnsWhileDragging = value;
      }
    }

    internal static bool IsDroppedAtLeft(Point dropLocation, int width)
    {
      int num = width / 2;
      return dropLocation.X <= num;
    }

    internal static bool IsDroppedAtTop(Point dropLocation, int height)
    {
      int num = height / 2;
      return dropLocation.Y <= num;
    }

    internal static RadPosition GetDropPosition(Point dropLocation, Size size)
    {
      RadPosition radPosition1 = RadPosition.None;
      int num1 = size.Width / 2;
      int num2 = size.Width / 3;
      int num3 = 2 * size.Width / 3;
      RadPosition radPosition2 = dropLocation.X > num1 ? radPosition1 | RadPosition.Right : radPosition1 | RadPosition.Left;
      if (num2 < dropLocation.X && dropLocation.X < num3)
      {
        int num4 = size.Height / 2;
        if (dropLocation.Y <= num4)
          radPosition2 |= RadPosition.Top;
        else
          radPosition2 |= RadPosition.Bottom;
      }
      return radPosition2;
    }

    internal static void CalculateTargetIndex(
      bool isDroppedAtLeft,
      int itemsCount,
      ref int targetIndex,
      ref int draggedItemIndex)
    {
      if (isDroppedAtLeft)
      {
        if (draggedItemIndex - targetIndex >= 0 || targetIndex <= 0)
          return;
        --targetIndex;
      }
      else
      {
        if (draggedItemIndex - targetIndex <= 0 || targetIndex + 1 >= itemsCount)
          return;
        ++targetIndex;
      }
    }

    internal static void MoveOnLeftOrRight<T>(
      bool isDroppedAtLeft,
      Collection<T> collection,
      T targetItem,
      T draggedItem)
    {
      int targetIndex = collection.IndexOf(targetItem);
      int draggedItemIndex = collection.IndexOf(draggedItem);
      int count = collection.Count;
      RadGridViewDragDropService.CalculateTargetIndex(isDroppedAtLeft, count, ref targetIndex, ref draggedItemIndex);
      RadGridViewDragDropService.Move<T>(collection, draggedItemIndex, targetIndex);
    }

    internal static void Move<T>(Collection<T> collection, int oldIndex, int newIndex)
    {
      ObservableCollection<T> observableCollection = collection as ObservableCollection<T>;
      if (observableCollection != null)
      {
        observableCollection.Move(oldIndex, newIndex);
      }
      else
      {
        T obj = collection[oldIndex];
        collection.RemoveAt(oldIndex);
        collection.Insert(newIndex, obj);
      }
    }

    internal static T Search<T>(Collection<T> collection, Predicate<T> predicate) where T : class
    {
      foreach (T obj in collection)
      {
        if (predicate(obj))
          return obj;
      }
      return default (T);
    }

    internal static void InsertOnLeftOrRight<T>(
      bool isDroppedAtLeft,
      Collection<T> collection,
      T targetItem,
      T newItem)
    {
      int index = collection.IndexOf(targetItem);
      if (object.Equals((object) targetItem, (object) newItem))
        collection.RemoveAt(index);
      if (!isDroppedAtLeft)
        ++index;
      collection.Insert(index, newItem);
    }

    internal static Bitmap GetDragImageHint(
      ContentAlignment textAlignment,
      Bitmap hintImage,
      RectangleF textRectangle,
      int hintImageWidth)
    {
      int width = hintImage.Width;
      if (width > hintImageWidth)
      {
        width = hintImageWidth;
        int x = (hintImage.Width - hintImageWidth) / 2;
        Point location = Point.Empty;
        switch (textAlignment)
        {
          case ContentAlignment.TopCenter:
          case ContentAlignment.MiddleCenter:
          case ContentAlignment.BottomCenter:
            location = new Point(x, 0);
            break;
          case ContentAlignment.TopRight:
          case ContentAlignment.MiddleRight:
          case ContentAlignment.BottomRight:
            location = Point.Truncate(textRectangle.Location);
            break;
        }
        Rectangle cropRectangle = new Rectangle(location, new Size(width, hintImage.Height));
        hintImage = ImageHelper.Crop(hintImage, cropRectangle);
      }
      ImageHelper.ApplyMask(hintImage, (Brush) new LinearGradientBrush(new Rectangle(0, 0, width, hintImage.Height), Color.White, Color.Black, LinearGradientMode.Horizontal));
      return hintImage;
    }

    protected override void PerformStop()
    {
      SnapshotDragItem context = this.Context as SnapshotDragItem;
      if (context != null)
      {
        context.Capture = false;
        context.Item.IsMouseDown = false;
      }
      base.PerformStop();
      this.DisposeDragHint();
      this.dragDropBehavior = (IGridDragDropBehavior) null;
      this.lastDropPosition = RadPosition.None;
    }

    private void AutoScroll()
    {
      Point client = this.GridViewElement.ElementTree.Control.PointToClient(Control.MousePosition);
      GridTableElement tableElementAtPoint = this.GetTableElementAtPoint(client);
      object dataContext = (this.Context as ISupportDrag).GetDataContext();
      if (this.AllowAutoScrollRowsWhileDragging && dataContext is GridViewRowInfo)
        this.ScrollRows(tableElementAtPoint, client);
      else if (this.AllowAutoScrollColumnsWhileDragging && dataContext is GridViewColumn)
      {
        this.ScrollColumns(tableElementAtPoint, client);
      }
      else
      {
        if (!(dataContext is GroupFieldDragDropContext))
          return;
        this.ScrollColumns(tableElementAtPoint, client);
      }
    }

    private void ScrollColumns(GridTableElement tableElement, Point location)
    {
      GridTableHeaderRowElement rowElement = tableElement.GetRowElement((GridViewRowInfo) tableElement.ViewInfo.TableHeaderRow) as GridTableHeaderRowElement;
      if (rowElement == null)
        return;
      Rectangle boundingRectangle1 = rowElement.ControlBoundingRectangle;
      Rectangle boundingRectangle2 = rowElement.ScrollableColumns.ControlBoundingRectangle;
      int num = 0;
      if (location.X > boundingRectangle2.Right)
        num = location.X - boundingRectangle2.Right;
      else if (location.X < boundingRectangle2.X)
        num = location.X - boundingRectangle2.X;
      RadScrollBarElement hscrollBar = tableElement.HScrollBar;
      if (tableElement.RightToLeft)
        num *= -1;
      if (num == 0 || hscrollBar.Visibility != ElementVisibility.Visible)
        return;
      hscrollBar.Value = this.ClampValue(hscrollBar.Value + num, hscrollBar.Minimum, hscrollBar.Maximum - hscrollBar.LargeChange + 1);
    }

    private void ScrollRows(GridTableElement tableElement, Point location)
    {
      ScrollableRowsContainerElement scrollableRows = tableElement.ViewElement.ScrollableRows;
      RadScrollBarElement veritcalScrollbar = this.GetVeritcalScrollbar(tableElement);
      Rectangle boundingRectangle = scrollableRows.ControlBoundingRectangle;
      if (boundingRectangle.Contains(location) || location.X < boundingRectangle.X || location.X > boundingRectangle.Right)
        return;
      int num = 0;
      if (location.Y > boundingRectangle.Bottom)
        num = location.Y - boundingRectangle.Bottom;
      else if (location.Y < boundingRectangle.Y)
        num = location.Y - boundingRectangle.Y;
      if (num == 0 || veritcalScrollbar.Visibility != ElementVisibility.Visible)
        return;
      veritcalScrollbar.Value = this.ClampValue(veritcalScrollbar.Value + num, veritcalScrollbar.Minimum, veritcalScrollbar.Maximum - veritcalScrollbar.LargeChange + 1);
    }

    private Rectangle GetScrollableColumnBounds(GridTableElement tableElement)
    {
      PinnedRowsContainerElement topPinnedRows = tableElement.ViewElement.TopPinnedRows;
      ScrollableRowsContainerElement scrollableRows = tableElement.ViewElement.ScrollableRows;
      PinnedRowsContainerElement bottomPinnedRows = tableElement.ViewElement.BottomPinnedRows;
      RadElement radElement = (RadElement) null;
      if (topPinnedRows.Children.Count > 0)
        radElement = (RadElement) topPinnedRows;
      else if (scrollableRows.Children.Count > 0)
        radElement = (RadElement) scrollableRows;
      else if (bottomPinnedRows.Children.Count > 0)
        radElement = (RadElement) bottomPinnedRows;
      if (radElement == null)
        return Rectangle.Empty;
      Rectangle boundingRectangle = (radElement.Children[0] as GridVirtualizedRowElement).ScrollableColumns.ControlBoundingRectangle;
      if (tableElement.VScrollBar.Visibility != ElementVisibility.Collapsed)
        boundingRectangle.Width -= tableElement.VScrollBar.ControlBoundingRectangle.Width;
      if (tableElement.HScrollBar.Visibility != ElementVisibility.Collapsed)
        boundingRectangle.Height -= tableElement.HScrollBar.ControlBoundingRectangle.Height;
      return boundingRectangle;
    }

    protected GridTableElement GetTableElementAtPoint(Point point)
    {
      GridCellElement elementAtPoint = GridVisualElement.GetElementAtPoint<GridCellElement>((RadElementTree) this.GridViewElement.ElementTree, point);
      GridDetailViewCellElement detailViewCellElement = elementAtPoint as GridDetailViewCellElement;
      GridTableElement gridTableElement = elementAtPoint?.TableElement;
      if (detailViewCellElement != null)
        gridTableElement = detailViewCellElement.ChildTableElement;
      return gridTableElement ?? this.GridViewElement.TableElement;
    }

    private int ClampValue(int value, int minimum, int maximum)
    {
      return Math.Max(Math.Min(value, maximum), minimum);
    }

    private RadScrollBarElement GetVeritcalScrollbar(
      GridTableElement tableElement)
    {
      if (this.GridViewElement.UseScrollbarsInHierarchy)
        return tableElement.VScrollBar;
      return this.GridViewElement.TableElement.VScrollBar;
    }

    protected override void HandleMouseMove(Point mousePosition)
    {
      ISupportDrop dropTarget = this.DropTarget;
      this.AutoScroll();
      base.HandleMouseMove(mousePosition);
      RadElement radElement = dropTarget as RadElement;
      RadPosition radPosition = RadPosition.None;
      if (radElement != null)
        radPosition = RadGridViewDragDropService.GetDropPosition(radElement.PointFromScreen(mousePosition), radElement.ControlBoundingRectangle.Size);
      if (dropTarget != this.DropTarget || dropTarget == null || radPosition != this.lastDropPosition)
        this.SetDragDropBehavior();
      this.lastDropPosition = radPosition;
      if (this.dragHintWindow == null)
        return;
      this.UpdateDragHintLocation(mousePosition);
    }

    protected virtual void SetDragDropBehavior()
    {
      this.DisposeDragHint();
      this.dragDropBehavior = this.GetDragDropBehavior();
      if (this.dragDropBehavior == null)
        return;
      this.PrepareDragHint(this.DropTarget);
    }

    protected virtual IGridDragDropBehavior GetDragDropBehavior()
    {
      IGridDragDropBehavior dragDropBehavior = (IGridDragDropBehavior) null;
      ISupportDrop dropTarget = this.DropTarget;
      if (dropTarget is GridHeaderCellElement)
        dragDropBehavior = (IGridDragDropBehavior) new GridColumnDragDropBehvavior();
      else if (dropTarget is GridDataRowElement)
      {
        dragDropBehavior = (IGridDragDropBehavior) new GridRowDragDropBehavior();
      }
      else
      {
        GroupFieldElement groupFieldElement = dropTarget as GroupFieldElement;
      }
      dragDropBehavior?.Initialize(this.GridViewElement);
      return dragDropBehavior;
    }

    protected virtual void DisposeDragHint()
    {
      if (this.dragHintWindow == null)
        return;
      this.dragHintWindow.Dispose();
      this.dragHintWindow = (RadLayeredWindow) null;
    }

    protected virtual void UpdateDragHintLocation(Point mousePosition)
    {
      if (!this.CanCommit)
        this.dragHintWindow.Visible = false;
      else
        this.dragHintWindow.ShowWindow(this.dragDropBehavior.GetDragHintLocation(this.DropTarget, mousePosition));
    }

    protected virtual void PrepareDragHint(ISupportDrop dropTarget)
    {
      if (this.dragDropBehavior == null || this.dragDropBehavior.DragHint == null || this.dragDropBehavior.DragHint.Image == null)
        return;
      RadItem radItem = dropTarget as RadItem;
      if (radItem != null && radItem.ElementTree != null && (this.GridViewElement.ElementTree != null && radItem.ElementTree.Control != this.GridViewElement.ElementTree.Control))
        return;
      this.dragDropBehavior.UpdateDropContext(this.Context as ISupportDrag, dropTarget, this.beginPoint);
      Size dragHintSize = this.dragDropBehavior.GetDragHintSize(dropTarget);
      Bitmap bitmap = new Bitmap(dragHintSize.Width, dragHintSize.Height);
      Graphics g = Graphics.FromImage((Image) bitmap);
      this.dragDropBehavior.DragHint.Paint(g, new RectangleF(PointF.Empty, (SizeF) dragHintSize));
      g.Dispose();
      this.dragHintWindow = new RadLayeredWindow();
      this.dragHintWindow.BackgroundImage = (Image) bitmap;
    }

    protected override void OnPreviewDropTarget(PreviewDropTargetEventArgs e)
    {
      base.OnPreviewDropTarget(e);
      TemplateGroupsElement dropTarget = e.DropTarget as TemplateGroupsElement;
      if (dropTarget == null || dropTarget.CanDragOver(e.DragInstance))
        return;
      GroupFieldElement dragInstance = e.DragInstance as GroupFieldElement;
      if (dragInstance != null)
        e.DropTarget = (ISupportDrop) dragInstance.TemplateElement;
      else
        e.DropTarget = (ISupportDrop) dropTarget.GroupPanelElement;
    }

    protected override void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      base.OnPreviewDragOver(e);
      if (this.dragDropBehavior != null)
        return;
      this.DisposeDragHint();
    }
  }
}
