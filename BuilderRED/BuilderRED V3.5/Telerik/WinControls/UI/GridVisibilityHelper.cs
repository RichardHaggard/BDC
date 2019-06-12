// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridVisibilityHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class GridVisibilityHelper
  {
    private GridTableElement tableElement;

    public GridVisibilityHelper(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    protected RadGridViewElement GridViewElement
    {
      get
      {
        return this.tableElement.GridViewElement;
      }
    }

    protected RowsContainerElement ViewElement
    {
      get
      {
        return this.tableElement.ViewElement;
      }
    }

    protected GridViewInfo MasterViewInfo
    {
      get
      {
        return this.tableElement.ViewTemplate.MasterViewInfo;
      }
    }

    protected GridViewInfo ViewInfo
    {
      get
      {
        return this.tableElement.ViewInfo;
      }
    }

    protected RadScrollBarElement ParentVScrollBar
    {
      get
      {
        if (this.TableElement != this.MasterTableElement)
        {
          GridDetailViewRowElement detailViewRowElement = this.GetDetailViewRowElement();
          if (detailViewRowElement != null)
            return detailViewRowElement.TableElement.VScrollBar;
        }
        return this.MasterTableElement.VScrollBar;
      }
    }

    protected RadScrollBarElement ParentHScrollBar
    {
      get
      {
        if (this.TableElement != this.MasterTableElement)
        {
          GridDetailViewRowElement detailViewRowElement = this.GetDetailViewRowElement();
          if (detailViewRowElement != null)
            return detailViewRowElement.TableElement.HScrollBar;
        }
        return this.MasterTableElement.HScrollBar;
      }
    }

    protected GridDetailViewRowElement GetDetailViewRowElement()
    {
      for (RadElement radElement = (RadElement) this.TableElement; radElement != null; radElement = radElement.Parent)
      {
        GridDetailViewRowElement detailViewRowElement = radElement as GridDetailViewRowElement;
        if (detailViewRowElement != null)
          return detailViewRowElement;
      }
      return (GridDetailViewRowElement) null;
    }

    protected RadScrollBarElement MasterVScrollBar
    {
      get
      {
        return this.MasterTableElement.VScrollBar;
      }
    }

    protected RadScrollBarElement VScrollBar
    {
      get
      {
        return this.tableElement.VScrollBar;
      }
    }

    protected GridTableElement MasterTableElement
    {
      get
      {
        return this.GridViewElement.TableElement;
      }
    }

    protected GridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    protected bool UseScrollbarsInHierarchy
    {
      get
      {
        return this.GridViewElement.UseScrollbarsInHierarchy;
      }
    }

    protected int RowSpacing
    {
      get
      {
        return this.tableElement.RowSpacing;
      }
    }

    protected void UpdateLayout()
    {
      this.tableElement.UpdateLayout();
    }

    protected GridRowElement GetRowElement(GridViewRowInfo rowInfo)
    {
      return this.tableElement.GetRowElement(rowInfo);
    }

    protected RowScroller RowScroller
    {
      get
      {
        return this.tableElement.RowScroller;
      }
    }

    protected ItemScroller<GridViewColumn> ColumnScroller
    {
      get
      {
        return this.tableElement.ColumnScroller;
      }
    }

    protected bool ChangeScrollbarValue(RadScrollBarElement scrollbar, int value)
    {
      if (value > scrollbar.Maximum - scrollbar.LargeChange + 1)
        value = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (value < scrollbar.Minimum || value > scrollbar.Maximum)
        return false;
      scrollbar.Value = value;
      return true;
    }

    public virtual bool EnsureCellVisible(GridViewRowInfo rowInfo, GridViewColumn column)
    {
      if (!this.TableElement.ElementTree.Control.Visible || this.TableElement.ElementTree.Control.Size == Size.Empty)
        return false;
      this.EnsureRowVisible(rowInfo);
      GridRowElement rowElement = this.GetRowElement(rowInfo);
      if (rowElement == null || column == null || (!column.IsVisible || column.IsGrouped) || column.OwnerTemplate.Parent == null && column.IsPinned)
        return false;
      Rectangle empty = Rectangle.Empty;
      Rectangle rectangle = rowInfo.PinPosition != PinnedRowPosition.Top ? (rowInfo.PinPosition != PinnedRowPosition.Bottom ? this.ViewElement.ScrollableRows.ControlBoundingRectangle : this.ViewElement.BottomPinnedRows.ControlBoundingRectangle) : this.ViewElement.TopPinnedRows.ControlBoundingRectangle;
      bool flag1 = rowElement.ElementTree.Control.RightToLeft == RightToLeft.Yes;
      GridVirtualizedRowElement virtualizedRowElement = rowElement as GridVirtualizedRowElement;
      if (virtualizedRowElement != null)
      {
        rectangle.X = virtualizedRowElement.ScrollableColumns.ControlBoundingRectangle.X;
        if (flag1)
          rectangle.X -= virtualizedRowElement.TableElement.CellSpacing;
        rectangle.Width = virtualizedRowElement.ScrollableColumns.ControlBoundingRectangle.Width;
      }
      foreach (GridCellElement visualCell in rowElement.VisualCells)
      {
        if ((visualCell.ColumnInfo == null || !visualCell.ColumnInfo.IsPinned) && visualCell.ColumnInfo == column)
        {
          if (visualCell.ControlBoundingRectangle.Width > rectangle.Width)
            return false;
          if (visualCell.ControlBoundingRectangle.Right > rectangle.Right + 1)
          {
            if (this.TableElement.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
            {
              int num1 = this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1;
              int num2 = column.Index;
              if (num2 > num1)
                num2 = num1;
              this.ColumnScroller.Scrollbar.Value = num2;
            }
            else
            {
              int num = visualCell.ControlBoundingRectangle.Right - rectangle.Right;
              if (flag1)
                num *= -1;
              this.ChangeScrollbarValue(this.ColumnScroller.Scrollbar, this.ColumnScroller.Scrollbar.Value + num);
            }
          }
          if (visualCell.ControlBoundingRectangle.X < rectangle.X - 1)
          {
            if (this.TableElement.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
            {
              int num1 = this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1;
              int num2 = column.Index;
              if (num2 > num1)
                num2 = num1;
              this.ColumnScroller.Scrollbar.Value = num2;
            }
            else
            {
              int num = visualCell.ControlBoundingRectangle.X - rectangle.X;
              if (flag1)
                num *= -1;
              this.ChangeScrollbarValue(this.ColumnScroller.Scrollbar, this.ColumnScroller.Scrollbar.Value + num);
            }
          }
          return true;
        }
      }
      if (rowInfo is GridViewGroupRowInfo)
        return false;
      ColumnGroupRowLayout rowLayout = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (rowLayout != null)
      {
        this.ChangeScrollbarValue(this.ColumnScroller.Scrollbar, rowLayout.GetColumnOffset(column));
        return true;
      }
      int num3 = this.ColumnScroller.Scrollbar.Value;
      bool flag2;
      if (this.TableElement.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
      {
        int num1 = this.ColumnScroller.Scrollbar.Maximum - this.ColumnScroller.Scrollbar.LargeChange + 1;
        int num2 = column.Index;
        if (num2 > num1)
          num2 = num1;
        this.ColumnScroller.Scrollbar.Value = num2;
        this.TableElement.Invalidate();
        flag2 = true;
      }
      else
        flag2 = this.ColumnScroller.ScrollToItem(column, false);
      if (num3 < this.ColumnScroller.Scrollbar.Value)
      {
        this.UpdateLayout();
        foreach (GridCellElement visualCell in rowElement.VisualCells)
        {
          if ((visualCell.ColumnInfo == null || !visualCell.ColumnInfo.IsPinned) && visualCell.ColumnInfo == column)
            this.ChangeScrollbarValue(this.ColumnScroller.Scrollbar, this.ColumnScroller.Scrollbar.Value + (flag1 ? rectangle.Left - visualCell.ControlBoundingRectangle.Left : visualCell.ControlBoundingRectangle.Right - rectangle.Right));
        }
      }
      return flag2;
    }

    public virtual bool EnsureRowVisible(GridViewRowInfo rowInfo)
    {
      if (rowInfo.ViewInfo != this.ViewInfo || rowInfo.ViewInfo == this.MasterViewInfo && rowInfo.PinPosition != PinnedRowPosition.None || (!this.TableElement.ElementTree.Control.Visible && !(this.TableElement.ElementTree.Control is MultiColumnComboGridView) || (this.TableElement.ElementTree.Control.Size == Size.Empty || (SizeF) this.TableElement.Size == SizeF.Empty)))
        return false;
      if (this.TableElement.MasterTemplate != null && this.TableElement.MasterTemplate.EnablePaging)
      {
        int pageIndex = this.TableElement.MasterTemplate.PageIndex;
        if (this.TableElement.MasterTemplate.EnableGrouping && this.TableElement.MasterTemplate.GroupDescriptors.Count > 0)
        {
          int num = -2;
          IEnumerator enumerator = (IEnumerator) new PagedGroupedTraverser(((RadDataView<GridViewRowInfo>) this.TableElement.MasterTemplate.DataView).GroupBuilder.Groups.GroupList);
          while (enumerator.MoveNext())
          {
            GridViewGroupRowInfo current = enumerator.Current as GridViewGroupRowInfo;
            if (current != null && current.GroupLevel == 0)
              ++num;
            if (enumerator.Current == rowInfo)
              break;
          }
          if (num >= 0)
            pageIndex = num / this.TableElement.MasterTemplate.PageSize;
        }
        else
          pageIndex = this.TableElement.ViewTemplate.DataView.GetItemPage(rowInfo);
        if (pageIndex != this.TableElement.MasterTemplate.PageIndex && !(rowInfo is GridViewNewRowInfo))
        {
          this.TableElement.MasterTemplate.MoveToPage(pageIndex);
          this.TableElement.ViewElement.UpdateRows(true);
          if (this.TableElement.GridViewElement != null)
            this.GridViewElement.PagingPanelElement.UpdateView();
        }
      }
      this.UpdateLayout();
      GridViewDetailsRowInfo viewDetailsRowInfo = rowInfo as GridViewDetailsRowInfo;
      if (viewDetailsRowInfo == null)
        return this.EnsureRowVisibleCore(rowInfo);
      RadScrollBarElement vScrollBar = this.UseScrollbarsInHierarchy ? this.VScrollBar : this.MasterVScrollBar;
      if (this.GetRowElement((GridViewRowInfo) viewDetailsRowInfo) is GridDetailViewRowElement || !this.EnsureRowVisibleByTraverser(vScrollBar, (GridViewRowInfo) viewDetailsRowInfo, (float) vScrollBar.SmallChange))
        return false;
      this.UpdateLayout();
      return true;
    }

    protected virtual bool EnsureRowVisibleCore(GridViewRowInfo rowInfo)
    {
      bool isTraverserd = false;
      bool isLastRow = false;
      bool isFirstRow = false;
      GridRowElement row = this.GetRowElement(rowInfo);
      this.DetermineScrollableRowPosition(rowInfo, out isFirstRow, out isLastRow);
      ScrollableRowsContainerElement scrollableRows = this.ViewElement.ScrollableRows;
      RadScrollBarElement vericalScrollBar = this.UseScrollbarsInHierarchy ? this.VScrollBar : this.MasterVScrollBar;
      if (row == null && this.TryEnsureRowVisibilityByTraverser(rowInfo, isLastRow, vericalScrollBar, scrollableRows, out row, out isTraverserd))
        return isTraverserd;
      RectangleF boundingRectangle1 = (RectangleF) this.MasterTableElement.ViewElement.ScrollableRows.ControlBoundingRectangle;
      RectangleF innerClientRectangle = this.GetInnerClientRectangle(rowInfo);
      Rectangle boundingRectangle2 = row.ControlBoundingRectangle;
      int scrollValue = vericalScrollBar.Value;
      if (this.GridViewElement.SplitMode != RadGridViewSplitMode.None && this.MasterTableElement != row.TableElement)
        return false;
      if ((double) boundingRectangle2.Bottom > (double) boundingRectangle1.Bottom || (double) boundingRectangle2.Bottom > (double) innerClientRectangle.Bottom)
        this.EnsureRowVisibilityAtBottom(rowInfo, isLastRow, (RectangleF) boundingRectangle2, innerClientRectangle, ref vericalScrollBar, ref scrollValue);
      else if ((double) boundingRectangle2.Y < (double) boundingRectangle1.Y || this.ParentHScrollBar.Visibility == ElementVisibility.Visible && this.ParentHScrollBar.ControlBoundingRectangle.IntersectsWith(boundingRectangle2) || this.UseScrollbarsInHierarchy && (double) boundingRectangle2.Y < (double) innerClientRectangle.Y)
        this.EnsureRowVisibilityAtTop((RectangleF) boundingRectangle2, boundingRectangle1, innerClientRectangle, ref vericalScrollBar, ref scrollValue);
      return this.ChangeScrollbarValue(vericalScrollBar, scrollValue);
    }

    protected virtual void EnsureRowVisibilityAtTop(
      RectangleF rowRect,
      RectangleF clientRect,
      RectangleF innerClientRect,
      ref RadScrollBarElement vericalScrollBar,
      ref int scrollValue)
    {
      if (this.UseScrollbarsInHierarchy)
      {
        if ((double) rowRect.Y < (double) innerClientRect.Y && (double) innerClientRect.Y < (double) clientRect.Y)
        {
          this.ChangeScrollbarValue(this.VScrollBar, this.VScrollBar.Value + (int) ((double) rowRect.Y - (double) innerClientRect.Y));
          this.UpdateLayout();
        }
        if ((double) innerClientRect.Y < (double) clientRect.Y)
        {
          vericalScrollBar = this.ParentVScrollBar;
          scrollValue = vericalScrollBar.Value;
        }
      }
      Rectangle rect = new Rectangle((int) rowRect.X, (int) rowRect.Y, (int) rowRect.Size.Width, (int) rowRect.Height);
      Rectangle boundingRectangle = this.ParentHScrollBar.ControlBoundingRectangle;
      if (boundingRectangle.IntersectsWith(rect))
      {
        if (rect.Y < boundingRectangle.Y)
          scrollValue += boundingRectangle.Y - rect.Y;
        if ((double) rowRect.Bottom > (double) boundingRectangle.Bottom)
          scrollValue += rect.Bottom - boundingRectangle.Bottom;
        scrollValue += this.ParentHScrollBar.Size.Height;
      }
      else
        scrollValue += (int) ((double) rowRect.Y - (double) clientRect.Y);
    }

    protected virtual void EnsureRowVisibilityAtBottom(
      GridViewRowInfo rowInfo,
      bool isLastRow,
      RectangleF rowRect,
      RectangleF innerClientRect,
      ref RadScrollBarElement vericalScrollBar,
      ref int scrollValue)
    {
      RadScrollBarElement hscrollBar = this.TableElement.HScrollBar;
      Rectangle boundingRectangle = hscrollBar.ControlBoundingRectangle;
      Size size1 = this.TableElement.Size;
      Size size2 = this.ViewElement.TopPinnedRows.Size;
      int initialDeltaAtBottom = this.GetInitialDeltaAtBottom(size1);
      int num = (int) ((double) rowRect.Bottom - (double) innerClientRect.Bottom);
      if (size1.Height == 0 || size1.Height + this.TableElement.RowSpacing <= size2.Height || (double) size1.Height - (double) rowRect.Height + (double) num == (double) size2.Height)
      {
        vericalScrollBar = this.UseScrollbarsInHierarchy ? this.ParentVScrollBar : this.MasterVScrollBar;
        scrollValue = vericalScrollBar.Value;
        if (this.IsScrollBarVisible(hscrollBar))
          initialDeltaAtBottom += boundingRectangle.Size.Height;
      }
      else if (this.IsScrollBarVisible(hscrollBar))
        initialDeltaAtBottom += (int) ((double) innerClientRect.Bottom - (double) boundingRectangle.Top);
      else if (this.IsScrollBarVisible(this.ParentHScrollBar))
        initialDeltaAtBottom += (int) ((double) innerClientRect.Bottom - (double) this.ParentHScrollBar.ControlBoundingRectangle.Top);
      this.EnsureScrollingDelta(rowInfo, isLastRow, ref initialDeltaAtBottom);
      scrollValue += num + initialDeltaAtBottom;
    }

    private bool IsScrollBarVisible(RadScrollBarElement scrollBar)
    {
      if (scrollBar.Visibility != ElementVisibility.Visible)
        return false;
      return this.MasterTableElement.ViewElement.ScrollableRows.ControlBoundingRectangle.IntersectsWith(scrollBar.ControlBoundingRectangle);
    }

    protected int GetInitialDeltaAtBottom(Size tableElementSize)
    {
      int topOffset = 0;
      int bottomOffset = 0;
      int num = 0;
      this.CalculateOffset(out topOffset, out bottomOffset);
      if (tableElementSize.Height == 0)
        num += topOffset + bottomOffset;
      return num;
    }

    protected virtual bool TryEnsureRowVisibilityByTraverser(
      GridViewRowInfo rowInfo,
      bool isLastRow,
      RadScrollBarElement verticalScrollBar,
      ScrollableRowsContainerElement scrollableRows,
      out GridRowElement row,
      out bool isTraverserd)
    {
      int num = verticalScrollBar.Maximum - verticalScrollBar.LargeChange + 1;
      if (isLastRow && num != verticalScrollBar.Value)
      {
        this.ChangeScrollbarValue(verticalScrollBar, num);
        this.UpdateLayout();
      }
      row = this.GetRowElement(rowInfo);
      isTraverserd = false;
      if (row != null)
        return false;
      int height = (int) this.RowScroller.ElementProvider.GetElementSize(rowInfo).Height;
      this.EnsureScrollingDelta(rowInfo, isLastRow, ref height);
      if (this.UseScrollbarsInHierarchy && scrollableRows.Children.Count > 0)
      {
        GridDetailViewRowElement child = scrollableRows.Children[scrollableRows.Children.Count - 1] as GridDetailViewRowElement;
        height += child != null ? child.RowInfo.Height - child.Size.Height : 0;
      }
      isTraverserd = this.EnsureRowVisibleByTraverser(verticalScrollBar, rowInfo, (float) height);
      return true;
    }

    protected void EnsureScrollingDelta(GridViewRowInfo rowInfo, bool isLastRow, ref int delta)
    {
      if (!isLastRow && !rowInfo.IsPinned)
        return;
      delta -= this.RowSpacing;
    }

    protected void DetermineScrollableRowPosition(
      GridViewRowInfo rowInfo,
      out bool isFirstRow,
      out bool isLastRow)
    {
      isFirstRow = false;
      isLastRow = false;
      if (rowInfo.IsPinned)
        return;
      GridViewInfo viewInfo = this.GridViewElement.Template.MasterViewInfo;
      if (this.GridViewElement.UseScrollbarsInHierarchy)
        viewInfo = this.TableElement.ViewInfo;
      GridTraverser gridTraverser = new GridTraverser(viewInfo);
      gridTraverser.Reset();
      gridTraverser.MoveNext();
      isFirstRow = gridTraverser.Current == rowInfo;
      if (isFirstRow)
        return;
      gridTraverser.MoveToEnd();
      isLastRow = gridTraverser.Current == rowInfo;
    }

    protected void CalculateOffset(out int topOffset, out int bottomOffset)
    {
      topOffset = 0;
      bottomOffset = 0;
      if (this.TableElement == this.MasterTableElement)
        return;
      topOffset = Math.Abs(this.TableElement.ControlBoundingRectangle.Y - this.TableElement.Parent.Parent.ControlBoundingRectangle.Y);
      bottomOffset = Math.Abs(this.TableElement.Parent.Parent.ControlBoundingRectangle.Bottom - this.TableElement.ControlBoundingRectangle.Bottom);
    }

    protected RectangleF GetInnerClientRectangle(GridViewRowInfo rowInfo)
    {
      if (this.TableElement == this.MasterTableElement)
        return (RectangleF) this.MasterTableElement.ViewElement.ScrollableRows.ControlBoundingRectangle;
      if (rowInfo.PinPosition == PinnedRowPosition.Top)
        return (RectangleF) this.ViewElement.TopPinnedRows.ControlBoundingRectangle;
      if (rowInfo.PinPosition == PinnedRowPosition.Bottom)
        return (RectangleF) this.ViewElement.BottomPinnedRows.ControlBoundingRectangle;
      return (RectangleF) this.ViewElement.ScrollableRows.ControlBoundingRectangle;
    }

    protected virtual bool EnsureRowVisibleByTraverser(
      RadScrollBarElement vScrollBar,
      GridViewRowInfo rowInfo,
      float delta)
    {
      ScrollableRowsContainerElement scrollableRows = this.ViewElement.ScrollableRows;
      RadElementCollection children = scrollableRows.Children;
      int num1 = vScrollBar.Maximum - vScrollBar.LargeChange + 1;
      while (vScrollBar.Maximum != 0 && children.Count == 0)
      {
        this.ChangeScrollbarValue(vScrollBar, vScrollBar.Value + 1);
        this.UpdateLayout();
        if (scrollableRows.Children.Count > 0 || vScrollBar.Value == num1 || vScrollBar.Value == vScrollBar.Maximum)
          break;
      }
      if (children.Count == 0)
        return false;
      if (this.EnsureRowVisibleByTraverserDown(vScrollBar, rowInfo, scrollableRows, delta))
        return true;
      GridRowElement child = (GridRowElement) scrollableRows.Children[0];
      if (((GridTraverser) ((IEnumerable) this.RowScroller).GetEnumerator()).Current == rowInfo)
      {
        int num2 = vScrollBar.Value - (int) delta;
        if (num2 < vScrollBar.Minimum)
          num2 = vScrollBar.Minimum;
        else if (num2 > vScrollBar.Maximum)
          num2 = vScrollBar.Maximum;
        return this.ChangeScrollbarValue(vScrollBar, num2);
      }
      GridViewGroupRowInfo parent = rowInfo.Parent as GridViewGroupRowInfo;
      if (parent != null && !parent.IsExpanded)
        return false;
      return this.RowScroller.ScrollToItem(rowInfo, false);
    }

    private bool EnsureRowVisibleByTraverserDown(
      RadScrollBarElement vScrollBar,
      GridViewRowInfo rowInfo,
      ScrollableRowsContainerElement rows,
      float delta)
    {
      GridRowElement child = (GridRowElement) rows.Children[rows.Children.Count - 1];
      GridTraverser enumerator = (GridTraverser) ((IEnumerable) this.RowScroller).GetEnumerator();
      while (enumerator.Current is GridViewDetailsRowInfo)
        enumerator.MoveNext();
      enumerator.MovePrevious();
      if (enumerator.Current == rowInfo)
        return this.ChangeScrollbarValue(vScrollBar, vScrollBar.Value - (int) delta);
      do
        ;
      while (enumerator.MoveNext() && enumerator.Current != child.RowInfo);
      enumerator.MoveNext();
      if (enumerator.Current == rowInfo)
        return this.ChangeScrollbarValue(vScrollBar, vScrollBar.Value + (int) delta);
      return false;
    }
  }
}
