// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridHeaderRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridHeaderRowBehavior : GridRowBehavior
  {
    private int resizeCursorDistance = 3;
    private Cursor originalCursor;
    private Point mouseDownPoint;
    private GridViewColumn columnToResize;
    private GridCellElement cellToResize;
    private bool mouseIsDown;
    private bool columnResizeStarted;
    private bool dragDropStarted;
    private bool disableSortOrderChange;
    private IGridRowLayout rowLayout;
    private GridCellElement mouseDownCell;

    public bool ResizingColumn
    {
      get
      {
        return this.columnResizeStarted;
      }
    }

    public int ResizeCursorDistance
    {
      get
      {
        return this.resizeCursorDistance;
      }
      set
      {
        this.resizeCursorDistance = value;
      }
    }

    public override bool OnMouseDown(MouseEventArgs e)
    {
      this.disableSortOrderChange = false;
      bool flag = true;
      if (this.GridViewElement.IsInEditMode)
        flag = this.GridViewElement.EndEdit();
      if (e.Button == MouseButtons.Left)
      {
        this.FindCellToResize(e.Location);
        if (this.columnToResize != null)
        {
          this.mouseIsDown = true;
          this.mouseDownPoint = e.Location;
          this.GridControl.Capture = true;
          this.RootGridBehavior.LockBehavior((IGridBehavior) this);
          this.cellToResize = this.GridViewElement.ElementTree.GetElementAtPoint(e.Location) as GridCellElement;
          this.mouseDownCell = this.cellToResize;
        }
        else
        {
          if (!flag)
            return true;
          GridHeaderCellElement cellAtPoint = this.RootGridBehavior.CellAtPoint as GridHeaderCellElement;
          if (GridVisualElement.GetElementAtPoint<RadButtonElement>((RadElementTree) this.GridViewElement.ElementTree, e.Location) != null)
            return false;
          this.mouseDownCell = (GridCellElement) cellAtPoint;
          if (cellAtPoint == null || this.GetVisibleColumnsCount(cellAtPoint) < 2)
            return false;
          RadDragDropService service = this.GridViewElement.GetService<RadDragDropService>();
          if (service != null)
          {
            service.Start((object) new SnapshotDragItem((RadItem) cellAtPoint));
            service.Stopped += new EventHandler(this.dragDropService_Stopped);
            this.dragDropStarted = service.State == RadServiceState.Working;
          }
        }
      }
      else if (e.Button == MouseButtons.Right)
        this.RootGridBehavior.CellAtPoint?.ShowContextMenu();
      return false;
    }

    public override bool OnMouseUp(MouseEventArgs e)
    {
      GridCellElement mouseDownCell = this.mouseDownCell;
      this.mouseDownCell = (GridCellElement) null;
      if (this.cellToResize != null)
      {
        int num = (int) this.cellToResize.SetValue(RadElement.IsMouseDownProperty, (object) false);
        this.cellToResize = (GridCellElement) null;
      }
      if (this.columnToResize != null && this.rowLayout != null)
      {
        this.rowLayout.EndColumnResize();
        if (this.GridViewElement.AutoSizeRows && this.columnToResize.WrapText)
        {
          RadElement radElement = this.GridViewElement.ElementTree.GetElementAtPoint(e.Location);
          while (radElement != null && !(radElement is GridTableElement))
            radElement = radElement.Parent;
        }
        this.RootGridBehavior.UnlockBehavior((IGridBehavior) this);
        this.mouseIsDown = false;
        this.GridControl.Capture = false;
        this.columnResizeStarted = false;
        this.rowLayout = (IGridRowLayout) null;
        this.columnToResize = (GridViewColumn) null;
        this.ResetCursor();
        return false;
      }
      if (this.dragDropStarted)
      {
        RadService service = (RadService) this.GridViewElement.GetService<RadDragDropService>();
        this.dragDropStarted = false;
        if (service != null && (service.State == RadServiceState.Working || service.State == RadServiceState.Initial))
          return true;
      }
      else
      {
        GridHeaderCellElement cellAtPoint = this.RootGridBehavior.CellAtPoint as GridHeaderCellElement;
        if (cellAtPoint == null || (e.Location.X < cellAtPoint.ControlBoundingRectangle.X + 3 || this.disableSortOrderChange))
          return false;
        if (this.MasterTemplate.CurrentRow is GridViewNewRowInfo && this.GridViewElement.ContextMenuManager.ContextMenu == null)
          this.MasterTemplate.CurrentRow = (GridViewRowInfo) null;
        if (e.Button == MouseButtons.Left)
        {
          if (cellAtPoint != null && cellAtPoint == mouseDownCell)
          {
            RadSortOrder sortOrder;
            switch (cellAtPoint.SortOrder)
            {
              case RadSortOrder.Ascending:
                sortOrder = RadSortOrder.Descending;
                break;
              case RadSortOrder.None:
                sortOrder = RadSortOrder.Ascending;
                break;
              default:
                GridViewDataColumn columnInfo = cellAtPoint.ColumnInfo as GridViewDataColumn;
                sortOrder = columnInfo == null || columnInfo.AllowNaturalSort ? RadSortOrder.None : RadSortOrder.Ascending;
                break;
            }
            cellAtPoint.Sort(sortOrder);
          }
          return false;
        }
      }
      return base.OnMouseUp(e);
    }

    public override bool OnMouseMove(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.None && !this.FindCellToResize(e.Location))
        this.ResetCursor();
      if (e.Button == MouseButtons.Left && !this.dragDropStarted && this.rowLayout != null)
      {
        int delta = e.Location.X - this.mouseDownPoint.X;
        if (delta != 0)
        {
          if (!this.columnResizeStarted)
          {
            this.rowLayout.StartColumnResize(this.columnToResize);
            this.columnResizeStarted = true;
          }
          if (this.GridViewElement.RightToLeft)
            delta = -delta;
          this.rowLayout.ResizeColumn(delta);
          this.GridViewElement.Template.EventDispatcher.SuspendEvent(EventDispatcher.CellClick);
        }
      }
      return false;
    }

    public override bool OnMouseLeave(EventArgs e)
    {
      if (!this.mouseIsDown)
        this.ResetCursor();
      return false;
    }

    public override bool OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.columnToResize != null && this.columnToResize.CanStretch && this.columnToResize.OwnerTemplate.AllowAutoSizeColumns)
      {
        if (this.RootGridBehavior.RowAtPoint is GridTableHeaderRowElement)
        {
          if (this.cellToResize != null)
          {
            this.cellToResize = this.GridViewElement.ElementTree.GetElementAtPoint(e.Location) as GridCellElement;
            this.cellToResize.TableElement.BestFitColumn(this.columnToResize);
          }
          else
            this.columnToResize.BestFit();
          this.disableSortOrderChange = true;
        }
        this.ResetCursor();
      }
      this.columnResizeStarted = false;
      this.mouseIsDown = false;
      this.GridControl.Capture = false;
      this.RootGridBehavior.UnlockBehavior((IGridBehavior) this);
      return base.OnMouseDoubleClick(e);
    }

    private void dragDropService_Stopped(object sender, EventArgs e)
    {
      ((RadService) sender).Stopped -= new EventHandler(this.dragDropService_Stopped);
      this.dragDropStarted = false;
    }

    private void SetResizeCursor(GridViewColumn column)
    {
      if (!(this.originalCursor == (Cursor) null))
        return;
      this.originalCursor = this.GridControl.Cursor;
      this.GridControl.Cursor = Cursors.SizeWE;
      this.columnToResize = column;
    }

    private void ResetCursor()
    {
      if (!(this.originalCursor != (Cursor) null))
        return;
      this.GridControl.Cursor = this.originalCursor;
      this.originalCursor = (Cursor) null;
      this.rowLayout = (IGridRowLayout) null;
      this.columnToResize = (GridViewColumn) null;
    }

    private bool FindCellToResize(Point pt)
    {
      GridTableHeaderRowElement rowAtPoint = this.RootGridBehavior.RowAtPoint as GridTableHeaderRowElement;
      if (rowAtPoint == null || pt.X > rowAtPoint.ControlBoundingRectangle.Right)
        return false;
      for (int index = 0; index < rowAtPoint.VisualCells.Count; ++index)
      {
        GridCellElement visualCell = rowAtPoint.VisualCells[index];
        GridHeaderCellElement headerCellElement = visualCell as GridHeaderCellElement;
        if (headerCellElement != null && headerCellElement.ColumnInfo != null && (headerCellElement.ColumnInfo.AllowResize && headerCellElement.ColumnInfo.OwnerTemplate.AllowColumnResize))
        {
          if (headerCellElement.ColumnInfo.MaxWidth == 0 || headerCellElement.ColumnInfo.MaxWidth != headerCellElement.ColumnInfo.MinWidth)
          {
            int delta1 = 0;
            int delta2 = 0;
            bool flag = this.IsMouseInCellBounds(pt, visualCell, out delta1);
            GridCellElement elementAtPoint = headerCellElement.ElementTree.GetElementAtPoint(pt) as GridCellElement;
            if (flag && elementAtPoint != null && (this.IsMouseInCellBounds(pt, elementAtPoint, out delta2) && delta2 < delta1))
            {
              this.SetResizeCursor(elementAtPoint.ColumnInfo);
              this.rowLayout = rowAtPoint.TableElement.ViewElement.RowLayout;
              return true;
            }
            if (flag)
            {
              this.SetResizeCursor(headerCellElement.ColumnInfo);
              this.rowLayout = rowAtPoint.TableElement.ViewElement.RowLayout;
              return true;
            }
          }
        }
      }
      return false;
    }

    private bool IsMouseInCellBounds(Point pt, GridCellElement cell, out int delta)
    {
      bool flag;
      if (this.GridViewElement.RightToLeft)
      {
        flag = pt.X >= cell.ControlBoundingRectangle.Left - this.ResizeCursorDistance && pt.X <= cell.ControlBoundingRectangle.Left + this.ResizeCursorDistance;
        delta = pt.X - cell.ControlBoundingRectangle.Left;
      }
      else
      {
        flag = pt.X >= cell.ControlBoundingRectangle.Right - this.ResizeCursorDistance && pt.X <= cell.ControlBoundingRectangle.Right + this.ResizeCursorDistance;
        delta = pt.X - cell.ControlBoundingRectangle.Right;
      }
      return ((flag ? 1 : 0) & (pt.Y > cell.ControlBoundingRectangle.Bottom ? 0 : (pt.Y >= cell.ControlBoundingRectangle.Top ? 1 : 0))) != 0;
    }

    private int GetVisibleColumnsCount(GridHeaderCellElement cell)
    {
      if (cell == null || cell.TableElement == null)
        return 0;
      IList<GridViewColumn> renderColumns = cell.TableElement.ViewElement.RowLayout.RenderColumns;
      int num = 0;
      foreach (GridViewColumn gridViewColumn in (IEnumerable<GridViewColumn>) renderColumns)
      {
        if (gridViewColumn is GridViewDataColumn)
          ++num;
      }
      return num;
    }
  }
}
