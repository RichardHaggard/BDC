// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HtmlViewRowLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class HtmlViewRowLayout : TableViewRowLayoutBase
  {
    private SizeF desiredSize = SizeF.Empty;
    private List<GridViewColumn> systemColumns = new List<GridViewColumn>();
    private List<GridViewColumn> scrollableColumns = new List<GridViewColumn>();
    private Dictionary<int, HtmlViewCellArrangeInfo> arrangeInfos = new Dictionary<int, HtmlViewCellArrangeInfo>();
    private List<int> columnWidths = new List<int>();
    private HtmlViewDefinition viewDefinition;
    private int rowsHeight;
    private int fixedWidth;
    private GridViewColumn resizedColumn;
    private int resizedColumnOriginalWidth;
    private SizeF cachedAvailableSize;

    public HtmlViewRowLayout(HtmlViewDefinition viewDefinition)
    {
      this.viewDefinition = viewDefinition;
    }

    public override SizeF DesiredSize
    {
      get
      {
        return this.desiredSize;
      }
    }

    public override IList<GridViewColumn> ScrollableColumns
    {
      get
      {
        return (IList<GridViewColumn>) this.scrollableColumns;
      }
    }

    public RowTemplate RowTemplate
    {
      get
      {
        return this.viewDefinition.RowTemplate;
      }
    }

    public override SizeF MeasureRow(SizeF availableSize)
    {
      if (this.cachedAvailableSize == availableSize || this.Owner == null)
        return this.desiredSize;
      this.cachedAvailableSize = availableSize;
      this.MinimizeRowTemplate();
      this.CalculateColumnWidths(availableSize);
      int num1 = 0;
      for (int index = 0; index < this.columnWidths.Count; ++index)
        num1 += this.columnWidths[index];
      int width1 = num1 + (this.columnWidths.Count - 1) * this.Owner.CellSpacing;
      int num2 = 0;
      for (int index = 0; index < this.RowTemplate.Rows.Count; ++index)
        num2 += this.RowTemplate.Rows[index].Height;
      this.rowsHeight = num2;
      int num3 = num2 + (this.RowTemplate.Rows.Count - 1) * this.Owner.CellSpacing;
      this.fixedWidth = 0;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.RenderColumns)
      {
        HtmlViewCellArrangeInfo arrangeInfo = this.GetArrangeInfo(renderColumn);
        if (this.systemColumns.Contains(renderColumn))
        {
          int columnWidth = this.GetColumnWidth(renderColumn);
          arrangeInfo.Bounds = new RectangleF((float) this.fixedWidth, 0.0f, (float) columnWidth, (float) num3);
          this.fixedWidth += columnWidth + this.Owner.CellSpacing;
        }
        else
          arrangeInfo.Bounds = this.GetCellBounds(arrangeInfo.Cell);
        float width2 = Math.Max(arrangeInfo.Bounds.Width, (float) renderColumn.MinWidth);
        if (renderColumn.MaxWidth > 0)
          width2 = Math.Min(arrangeInfo.Bounds.Width, (float) renderColumn.MaxWidth);
        arrangeInfo.Bounds = new RectangleF(arrangeInfo.Bounds.X, arrangeInfo.Bounds.Y, width2, arrangeInfo.Bounds.Height);
      }
      this.desiredSize = new SizeF((float) (width1 + this.fixedWidth), (float) num3);
      this.Owner.ColumnScroller.ClientSize = new SizeF(availableSize.Width - (float) this.fixedWidth, availableSize.Height);
      this.Owner.ColumnScroller.UpdateScrollRange(width1, true);
      return this.desiredSize;
    }

    public override RectangleF ArrangeCell(RectangleF clientRect, GridCellElement cell)
    {
      RectangleF rectangleF1 = this.ArrangeCellCore(cell.ColumnInfo, cell.RowInfo, clientRect);
      if (this.ViewTemplate.IsSelfReference && cell is GridDataCellElement && (this.FirstDataColumn != null && cell.ColumnInfo != this.FirstDataColumn))
      {
        RectangleF rectangleF2 = this.ArrangeCellCore((GridViewColumn) this.FirstDataColumn, cell.RowInfo, clientRect);
        ((GridDataCellElement) cell).IsLeftMost = cell.RightToLeft ? (double) Math.Abs(rectangleF2.Right - rectangleF1.Right) < 0.01 : (double) Math.Abs(rectangleF2.X - rectangleF1.X) < 0.01;
      }
      return rectangleF1;
    }

    private RectangleF ArrangeCellCore(
      GridViewColumn columnInfo,
      GridViewRowInfo rowInfo,
      RectangleF clientRect)
    {
      HtmlViewCellArrangeInfo arrangeInfo = this.GetArrangeInfo(columnInfo);
      if (arrangeInfo == null)
        return RectangleF.Empty;
      RectangleF bounds = arrangeInfo.Bounds;
      if ((double) clientRect.Height != (double) this.DesiredSize.Height && !(rowInfo is GridViewTableHeaderRowInfo))
      {
        if (arrangeInfo.Cell == null)
        {
          bounds.Height = clientRect.Height;
        }
        else
        {
          int rowIndex = arrangeInfo.Cell.RowIndex;
          int num = rowIndex * this.Owner.CellSpacing;
          bounds.Y = (float) ((int) Math.Ceiling((double) this.TranslateY(bounds.Y - (float) num, clientRect.Height)) + num);
          bounds.Height = (float) (int) Math.Ceiling((double) this.TranslateY(bounds.Height, clientRect.Height));
          if (rowIndex + arrangeInfo.Cell.RowSpan == this.RowTemplate.Rows.Count && (double) bounds.Bottom != (double) clientRect.Height)
            bounds.Height = clientRect.Height - bounds.Y;
        }
      }
      return bounds;
    }

    public override void StartColumnResize(GridViewColumn column)
    {
      this.resizedColumn = column;
      this.resizedColumnOriginalWidth = this.GetColumnWidth(column);
    }

    public override void EndColumnResize()
    {
      this.resizedColumn = (GridViewColumn) null;
    }

    public override void ResizeColumn(int delta)
    {
      if (this.Context == GridLayoutContext.Printer)
        return;
      this.resizedColumn.Width = (int) Math.Round((double) (this.resizedColumnOriginalWidth + delta) / (double) this.resizedColumn.DpiScale.Width);
    }

    public override int GetRowHeight(GridViewRowInfo rowInfo)
    {
      GridViewDetailsRowInfo viewDetailsRowInfo = rowInfo as GridViewDetailsRowInfo;
      if (viewDetailsRowInfo != null && viewDetailsRowInfo.ActualHeight != -1)
        return viewDetailsRowInfo.ActualHeight;
      if (rowInfo.Height != -1)
        return rowInfo.Height;
      if (rowInfo is GridViewGroupRowInfo)
        return this.Owner.GroupHeaderHeight;
      return (int) this.desiredSize.Height;
    }

    public override void InvalidateRenderColumns()
    {
      base.InvalidateRenderColumns();
      this.arrangeInfos.Clear();
      this.systemColumns.Clear();
      this.scrollableColumns.Clear();
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.RenderColumns)
      {
        this.systemColumns.Add(renderColumn);
        this.arrangeInfos.Add(renderColumn.GetHashCode(), new HtmlViewCellArrangeInfo((CellDefinition) null, renderColumn));
      }
      foreach (RowDefinition row in (Collection<RowDefinition>) this.RowTemplate.Rows)
      {
        foreach (CellDefinition cell in (Collection<CellDefinition>) row.Cells)
        {
          GridViewDataColumn column = this.ViewTemplate.Columns[cell.UniqueName];
          if (column != null && (column.IsVisible || this.IgnoreColumnVisibility))
          {
            this.arrangeInfos.Add(column.GetHashCode(), new HtmlViewCellArrangeInfo(cell, (GridViewColumn) column));
            this.RenderColumns.Add((GridViewColumn) column);
            this.scrollableColumns.Add((GridViewColumn) column);
          }
        }
      }
      RowDefinitionsCollection rows = this.RowTemplate.Rows;
      for (int index = 0; index < rows.Count; ++index)
      {
        RowDefinition rowDefinition = rows[index];
        if (rowDefinition.Cells.Count > 0)
        {
          this.SetFirstDataColumn(this.ViewTemplate.Columns[rowDefinition.Cells[0].UniqueName]);
          break;
        }
      }
      for (int index = rows.Count - 1; index >= 0; --index)
      {
        RowDefinition rowDefinition = rows[index];
        int count = rowDefinition.Cells.Count;
        if (count > 0)
        {
          this.SetLastDataColumn(this.ViewTemplate.Columns[rowDefinition.Cells[count - 1].UniqueName]);
          break;
        }
      }
    }

    public override void InvalidateLayout()
    {
      this.cachedAvailableSize = SizeF.Empty;
    }

    public override void EnsureColumnsLayout()
    {
    }

    private void MinimizeRowTemplate()
    {
      for (int index = this.RowTemplate.Rows.Count - 1; index >= 0; --index)
      {
        if (this.RowTemplate.Rows[index].Cells.Count == 0)
          this.RowTemplate.Rows.RemoveAt(index);
      }
    }

    private void CalculateColumnWidths(SizeF availableSize)
    {
      int[] numArray = new int[this.RowTemplate.Rows.Count];
      List<Point> pointList = new List<Point>();
      this.columnWidths.Clear();
      for (int index1 = 0; index1 < this.RowTemplate.Rows.Count; ++index1)
      {
        foreach (CellDefinition cell in (Collection<CellDefinition>) this.RowTemplate.Rows[index1].Cells)
        {
          GridViewColumn column = (GridViewColumn) this.ViewTemplate.Columns[cell.UniqueName];
          if (column != null && (column.IsVisible || this.IgnoreColumnVisibility))
          {
            foreach (Point point in pointList)
            {
              if (point.Y == index1 && point.X == numArray[index1])
                ++numArray[index1];
            }
            cell.ColumnIndex = numArray[index1];
            numArray[index1] += cell.ColSpan;
            while (this.columnWidths.Count <= cell.ColumnIndex)
              this.columnWidths.Add(0);
            int columnWidth = this.GetColumnWidth((GridViewColumn) this.ViewTemplate.Columns[cell.UniqueName]);
            this.columnWidths[cell.ColumnIndex] = Math.Max(this.columnWidths[cell.ColumnIndex], columnWidth);
            for (int index2 = 0; index2 < cell.ColSpan; ++index2)
            {
              for (int index3 = 1; index3 < cell.RowSpan; ++index3)
              {
                Point point = new Point(cell.ColumnIndex + index2, index1 + index3);
                int num = pointList.BinarySearch(point, (IComparer<Point>) new HtmlViewRowLayout.PointComparer());
                pointList.Insert(~num, point);
              }
            }
          }
        }
      }
      if (this.ViewTemplate.AutoSizeColumnsMode != GridViewAutoSizeColumnsMode.Fill)
        return;
      int num1 = (int) availableSize.Width - this.fixedWidth;
      int fixedWidth = this.fixedWidth;
      int num2 = 0;
      for (int index = 0; index < this.columnWidths.Count; ++index)
        num2 += this.columnWidths[index];
      for (int index = 0; index < this.columnWidths.Count; ++index)
      {
        int val2 = num1 * this.columnWidths[index] / num2;
        if (index == this.columnWidths.Count - 1 && (double) val2 != (double) availableSize.Width)
          val2 = (int) availableSize.Width - fixedWidth;
        int num3 = Math.Max(this.GetColumnWidth((GridViewColumn) this.ViewTemplate.Columns[index]), val2);
        this.columnWidths[index] = num3;
        fixedWidth += num3 + this.Owner.CellSpacing;
      }
    }

    private float TranslateY(float y, float actualHeight)
    {
      double num = (double) y * 100.0 / (double) this.rowsHeight;
      return (float) ((double) actualHeight * num / 100.0);
    }

    private RectangleF GetCellBounds(CellDefinition cell)
    {
      int num = this.RowTemplate.CellPadding + this.Owner.CellSpacing;
      RectangleF empty = RectangleF.Empty;
      for (int index = 0; index < cell.ColumnIndex; ++index)
        empty.X += (float) (this.columnWidths[index] + num);
      for (int index = 0; index < cell.RowIndex; ++index)
        empty.Y += (float) (this.RowTemplate.Rows[index].Height + num);
      for (int index = 0; index < cell.ColSpan && index + cell.ColumnIndex < this.columnWidths.Count; ++index)
      {
        empty.Width += (float) (this.columnWidths[index + cell.ColumnIndex] + this.RowTemplate.CellPadding);
        if (index > 0)
          empty.Width += (float) this.Owner.CellSpacing;
      }
      for (int index = 0; index < cell.RowSpan && index + cell.RowIndex < this.RowTemplate.Rows.Count; ++index)
      {
        int height = this.RowTemplate.Rows[index + cell.RowIndex].Height;
        empty.Height += (float) (height + this.RowTemplate.CellPadding);
        if (index > 0)
          empty.Height += (float) this.Owner.CellSpacing;
      }
      return empty;
    }

    public HtmlViewCellArrangeInfo GetArrangeInfo(GridViewColumn column)
    {
      if (this.arrangeInfos.ContainsKey(column.GetHashCode()))
        return this.arrangeInfos[column.GetHashCode()];
      return (HtmlViewCellArrangeInfo) null;
    }

    public override int GetColumnOffset(GridViewColumn column)
    {
      HtmlViewCellArrangeInfo arrangeInfo = this.GetArrangeInfo(column);
      if (arrangeInfo == null)
        return 0;
      return (int) arrangeInfo.Bounds.X;
    }

    private class PointComparer : IComparer<Point>
    {
      public int Compare(Point x, Point y)
      {
        if (x.Y == y.Y)
          return x.X.CompareTo(y.X);
        return x.Y.CompareTo(y.Y);
      }
    }
  }
}
