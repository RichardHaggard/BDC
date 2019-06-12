// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridColumnDragDropBehvavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridColumnDragDropBehvavior : GridDragDropBehavior
  {
    private Orientation orientation = Orientation.Vertical;

    public override RadImageShape DragHint
    {
      get
      {
        return this.GetDragHint();
      }
    }

    private RadImageShape GetDragHint()
    {
      if (this.orientation == Orientation.Vertical)
        return this.GridViewElement.TableElement.ColumnDragHint ?? new RadImageShape();
      return this.GridViewElement.TableElement.RowDragHint ?? new RadImageShape();
    }

    public override Point GetDragHintLocation(ISupportDrop dropTarget, Point mousePosition)
    {
      GridHeaderCellElement headerCellElement = dropTarget as GridHeaderCellElement;
      Rectangle boundingRectangle1 = headerCellElement.ControlBoundingRectangle;
      Rectangle boundingRectangle2 = headerCellElement.RowElement.TableElement.ViewElement.ControlBoundingRectangle;
      if (boundingRectangle1.Right > boundingRectangle2.Right)
        boundingRectangle1.Width -= boundingRectangle1.Right - boundingRectangle2.Right;
      if (boundingRectangle1.X < boundingRectangle2.X)
      {
        boundingRectangle1.Width -= boundingRectangle2.X - boundingRectangle1.X;
        boundingRectangle1.X = boundingRectangle2.X;
      }
      Rectangle screen = this.GridViewElement.ElementTree.Control.RectangleToScreen(boundingRectangle1);
      Size size = Size.Empty;
      if (this.DragHint.Image != null)
        size = this.DragHint.Image.Size;
      Padding margins = this.DragHint.Margins;
      Point dropLocation = headerCellElement.PointFromScreen(mousePosition);
      if (this.orientation == Orientation.Vertical)
        return new Point((RadGridViewDragDropService.IsDroppedAtLeft(dropLocation, headerCellElement.Size.Width) ? screen.X : screen.Right) - size.Width / 2, screen.Y - margins.Top);
      return new Point(screen.X - margins.Left, screen.Bottom - margins.Top - size.Height / 2);
    }

    public override Size GetDragHintSize(ISupportDrop dropTarget)
    {
      int width = 1;
      int height = 1;
      if (this.DragHint.Image != null)
      {
        width = this.DragHint.Image.Size.Width;
        height = this.DragHint.Image.Size.Height;
      }
      GridHeaderCellElement headerCellElement = dropTarget as GridHeaderCellElement;
      if (headerCellElement != null)
      {
        if (this.orientation == Orientation.Vertical)
          height = headerCellElement.Size.Height + this.DragHint.Margins.Vertical;
        else
          width = headerCellElement.Size.Width + this.DragHint.Margins.Horizontal;
      }
      return new Size(width, height);
    }

    public override void UpdateDropContext(
      ISupportDrag draggedContext,
      ISupportDrop dropTarget,
      Point? location)
    {
      GridColumnGroupCellElement groupCellElement = dropTarget as GridColumnGroupCellElement;
      GridHeaderCellElement headerCellElement = dropTarget as GridHeaderCellElement;
      GridViewGroupColumn gridViewGroupColumn = groupCellElement != null ? groupCellElement.ColumnInfo as GridViewGroupColumn : (GridViewGroupColumn) null;
      if (headerCellElement == null || !location.HasValue)
      {
        this.orientation = Orientation.Vertical;
      }
      else
      {
        if (headerCellElement.RowElement == null || headerCellElement.RowElement.TableElement == null)
          return;
        Rectangle boundingRectangle1 = headerCellElement.ControlBoundingRectangle;
        Rectangle boundingRectangle2 = headerCellElement.RowElement.TableElement.ViewElement.ControlBoundingRectangle;
        if (boundingRectangle1.Right > boundingRectangle2.Right)
          boundingRectangle1.Width -= boundingRectangle1.Right - boundingRectangle2.Right;
        if (boundingRectangle1.X < boundingRectangle2.X)
        {
          boundingRectangle1.Width -= boundingRectangle2.X - boundingRectangle1.X;
          boundingRectangle1.X = boundingRectangle2.X;
        }
        if ((RadGridViewDragDropService.GetDropPosition(headerCellElement.PointFromScreen(location.Value), headerCellElement.Size) & RadPosition.Bottom) != RadPosition.None && (gridViewGroupColumn != null && gridViewGroupColumn.Group.Rows.Count == 0 && draggedContext.GetDataContext() is GridViewGroupColumn || gridViewGroupColumn != null && gridViewGroupColumn.Group.Groups.Count == 0 && draggedContext.GetDataContext() is GridViewDataColumn || gridViewGroupColumn == null))
          this.orientation = Orientation.Horizontal;
        else
          this.orientation = Orientation.Vertical;
      }
    }
  }
}
