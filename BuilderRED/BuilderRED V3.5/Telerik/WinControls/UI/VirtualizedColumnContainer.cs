// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualizedColumnContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VirtualizedColumnContainer : VirtualizedStackContainer<GridViewColumn>
  {
    private bool scrollColumns = true;
    private GridRowElement context;
    private SizeF availableSize;
    private SizeF desiredSize;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ItemSpacing = -1;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Orientation = Orientation.Horizontal;
    }

    public GridRowElement Context
    {
      get
      {
        return this.context;
      }
      internal set
      {
        this.context = value;
      }
    }

    protected bool ScrollColumns
    {
      get
      {
        return this.scrollColumns;
      }
      set
      {
        this.scrollColumns = value;
      }
    }

    protected override void RemoveElement(int position)
    {
      GridVirtualizedCellElement child = this.Children[position] as GridVirtualizedCellElement;
      if (child != null)
      {
        if (child.RowElement == null || child.TableElement == null || (child.GridViewElement == null || child.GridViewElement.EditorManager == null) || !child.GridViewElement.IsInEditMode)
        {
          base.RemoveElement(position);
          return;
        }
        IEditableCell editableCell = child as IEditableCell;
        if (editableCell != null && editableCell.Editor != null && editableCell.Editor == child.GridViewElement.EditorManager.ActiveEditor)
          child.GridViewElement.EditorManager.CancelEdit();
      }
      base.RemoveElement(position);
    }

    protected override object GetElementContext()
    {
      return (object) this.context;
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.availableSize = availableSize;
      this.desiredSize = SizeF.Empty;
      return base.BeginMeasure(availableSize);
    }

    protected override SizeF EndMeasure()
    {
      SizeF empty = SizeF.Empty;
      PinnedColumnTraverser dataProvider = this.DataProvider as PinnedColumnTraverser;
      SizeF sizeF;
      if (!this.ScrollColumns && dataProvider != null)
      {
        sizeF = this.Context.TableElement.ViewElement.RowLayout.MeasurePinnedColumns(dataProvider);
      }
      else
      {
        sizeF = this.Context.TableElement.ViewElement.RowLayout.DesiredSize;
        sizeF.Width = Math.Min(this.availableSize.Width, sizeF.Width);
      }
      if (!this.Context.TableElement.GridViewElement.AutoSizeRows)
        return new SizeF(sizeF.Width, this.availableSize.Height);
      ColumnGroupRowLayout rowLayout = this.context.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (rowLayout != null)
        this.desiredSize.Height = rowLayout.MeasureAutoSizeRow(this.Children);
      return new SizeF(sizeF.Width, this.desiredSize.Height);
    }

    protected override bool MeasureElement(IVirtualizedElement<GridViewColumn> element)
    {
      GridCellElement cell = element as GridCellElement;
      if (cell == null)
        return false;
      RectangleF rectangleF = this.context.TableElement.ViewElement.RowLayout.ArrangeCell(new RectangleF(PointF.Empty, this.availableSize), cell);
      this.desiredSize.Height = Math.Max(this.desiredSize.Height, this.MeasureElementCore((RadElement) cell, this.availableSize).Height);
      if (!this.ScrollColumns || !(this.Context.TableElement.ViewElement.RowLayout is TableViewRowLayout))
        return true;
      int discreteScrollOffset = this.GetDiscreteScrollOffset();
      if (this.RightToLeft)
        return (double) rectangleF.Right - (double) this.ScrollOffset.Width + (double) discreteScrollOffset >= 0.0;
      return (double) rectangleF.Right - (double) discreteScrollOffset < (double) this.availableSize.Width;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRect = new RectangleF(PointF.Empty, finalSize);
      IGridRowLayout rowLayout = this.context.TableElement.ViewElement.RowLayout;
      ColumnGroupRowLayout columnGroupRowLayout = rowLayout as ColumnGroupRowLayout;
      if (columnGroupRowLayout != null && this.Context.TableElement.GridViewElement.AutoSizeRows)
        columnGroupRowLayout.BeginAutoSizeRowArrange(this.Children);
      foreach (RadElement child in this.Children)
      {
        RectangleF arrangeRect = rowLayout.ArrangeCell(clientRect, child as GridCellElement);
        if (this.ScrollColumns)
        {
          int discreteScrollOffset = this.GetDiscreteScrollOffset();
          if (this.RightToLeft)
            arrangeRect.X += (float) discreteScrollOffset;
          else
            arrangeRect.X -= (float) discreteScrollOffset;
        }
        this.ArrangeElementCore(child, finalSize, arrangeRect);
      }
      if (columnGroupRowLayout != null && this.Context.TableElement.GridViewElement.AutoSizeRows)
        columnGroupRowLayout.EndAutoSizeRowArrange();
      return finalSize;
    }

    protected override bool IsItemVisible(GridViewColumn data)
    {
      if (!this.ScrollColumns)
        return true;
      ColumnGroupRowLayout rowLayout = this.context.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (rowLayout == null)
        return true;
      if (data.IsPinned && this.ScrollColumns)
        return false;
      int num = 0;
      if (this.Context.TableElement.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
      {
        for (int index = 0; index < this.Context.TableElement.ColumnScroller.Scrollbar.Value; ++index)
          num += this.Context.TableElement.ColumnScroller.GetScrollHeight(rowLayout.ScrollableColumns[index]);
      }
      else
        num = this.context.TableElement.HScrollBar.Value;
      RectangleF viewRect = new RectangleF((float) num, 0.0f, this.availableSize.Width, this.availableSize.Height);
      return rowLayout.IsColumnVisible(data, viewRect);
    }

    protected virtual int GetDiscreteScrollOffset()
    {
      IGridRowLayout rowLayout = this.context.TableElement.ViewElement.RowLayout;
      int num = 0;
      if (this.Context.TableElement.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
      {
        for (int index = 0; index < this.Context.TableElement.ColumnScroller.Scrollbar.Value; ++index)
          num += this.Context.TableElement.ColumnScroller.GetScrollHeight(rowLayout.ScrollableColumns[index]) + this.Context.TableElement.CellSpacing;
      }
      else
        num = this.context.TableElement.HScrollBar.Value;
      return num;
    }
  }
}
