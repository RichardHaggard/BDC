// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnLayoutHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ColumnLayoutHelper
  {
    private TableViewRowLayout layout;
    private GridViewColumn resizedColumn;
    private int scrollableWidth;
    private List<TableViewCellArrangeInfo> scrollableColumns;
    private List<TableViewCellArrangeInfo> leftPinnedColumns;
    private List<TableViewCellArrangeInfo> rightPinnedColumns;
    private Dictionary<int, TableViewCellArrangeInfo> arrangeInfos;

    public ColumnLayoutHelper(TableViewRowLayout layout)
    {
      this.layout = layout;
      this.arrangeInfos = new Dictionary<int, TableViewCellArrangeInfo>();
      this.scrollableColumns = new List<TableViewCellArrangeInfo>();
      this.leftPinnedColumns = new List<TableViewCellArrangeInfo>();
      this.rightPinnedColumns = new List<TableViewCellArrangeInfo>();
      this.Initialize();
    }

    public Dictionary<int, TableViewCellArrangeInfo> ArrangeInfos
    {
      get
      {
        return this.arrangeInfos;
      }
    }

    public List<TableViewCellArrangeInfo> RightPinnedColumns
    {
      get
      {
        return this.rightPinnedColumns;
      }
    }

    public List<TableViewCellArrangeInfo> LeftPinnedColumns
    {
      get
      {
        return this.leftPinnedColumns;
      }
    }

    public TableViewRowLayout Layout
    {
      get
      {
        return this.layout;
      }
    }

    public IList<GridViewColumn> ScrollableColumns
    {
      get
      {
        return (IList<GridViewColumn>) new ColumnArrangeInfoCollection((IList<TableViewCellArrangeInfo>) this.scrollableColumns);
      }
    }

    public virtual void Initialize()
    {
      this.Reset();
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.Layout.RenderColumns)
        this.InitColumn(renderColumn);
    }

    public virtual int CalculateColumnsWidth(SizeF availableSize)
    {
      int num1 = 0;
      int num2 = this.ProcessColumnsCollection(this.leftPinnedColumns);
      int num3 = num1 + num2;
      this.scrollableWidth = this.ProcessColumnsCollection(this.scrollableColumns);
      int num4 = num3 + this.scrollableWidth;
      int num5 = this.ProcessColumnsCollection(this.rightPinnedColumns);
      int num6 = num4 + num5;
      this.Layout.Owner.ColumnScroller.ClientSize = new SizeF(availableSize.Width - (float) num2 - (float) num5, availableSize.Height);
      this.Layout.Owner.ColumnScroller.UpdateScrollRange(this.scrollableWidth - 1, true);
      return num6;
    }

    public virtual RectangleF GetCellArrangeRect(RectangleF client, GridCellElement cell)
    {
      TableViewCellArrangeInfo arrangeInfo = this.GetArrangeInfo(cell.ColumnInfo);
      if (arrangeInfo == null)
        return RectangleF.Empty;
      RectangleF rectangleF = new RectangleF(client.X + (float) arrangeInfo.OffsetX, client.Y, (float) arrangeInfo.Column.Width, client.Height);
      if (cell.ElementTree.Control.RightToLeft == RightToLeft.Yes && cell.ColumnInfo != null && (cell.ColumnInfo.PinPosition == PinnedColumnPosition.None && (double) this.scrollableWidth > (double) client.Width))
        rectangleF.X -= (float) this.scrollableWidth - client.Width;
      return rectangleF;
    }

    public virtual void StartColumnResize(GridViewColumn column)
    {
      this.resizedColumn = column;
      foreach (TableViewCellArrangeInfo viewCellArrangeInfo in this.arrangeInfos.Values)
        viewCellArrangeInfo.ResizeStartWidth = viewCellArrangeInfo.Column.Width;
    }

    public virtual void EndColumnResize()
    {
      foreach (TableViewCellArrangeInfo viewCellArrangeInfo in this.arrangeInfos.Values)
        viewCellArrangeInfo.TempScaleFactor = 0.0;
      this.resizedColumn = (GridViewColumn) null;
    }

    public virtual void ResizeColumn(int delta)
    {
      if (this.resizedColumn == null)
        return;
      TableViewCellArrangeInfo arrangeInfo = this.GetArrangeInfo(this.resizedColumn);
      arrangeInfo?.SetWidth(arrangeInfo.ResizeStartWidth + delta, false, this.Layout.Context == GridLayoutContext.Printer);
    }

    protected virtual TableViewCellArrangeInfo InitColumn(
      GridViewColumn column)
    {
      TableViewCellArrangeInfo viewCellArrangeInfo = new TableViewCellArrangeInfo(column);
      this.arrangeInfos[column.GetHashCode()] = viewCellArrangeInfo;
      switch (column.PinPosition)
      {
        case PinnedColumnPosition.Left:
          this.leftPinnedColumns.Add(viewCellArrangeInfo);
          break;
        case PinnedColumnPosition.Right:
          this.rightPinnedColumns.Add(viewCellArrangeInfo);
          break;
        case PinnedColumnPosition.None:
          this.scrollableColumns.Add(viewCellArrangeInfo);
          break;
      }
      return viewCellArrangeInfo;
    }

    protected virtual int ProcessColumnsCollection(List<TableViewCellArrangeInfo> columns)
    {
      int cellSpacing = this.Layout.Owner.CellSpacing;
      int count = columns.Count;
      int num = 0;
      for (int index = 0; index < count; ++index)
      {
        TableViewCellArrangeInfo column = columns[index];
        int columnWidth = this.Layout.GetColumnWidth(column.Column);
        column.SetWidth(columnWidth, true, this.Layout.Context == GridLayoutContext.Printer && !this.layout.Owner.RightToLeft);
        column.OffsetX = num;
        num += columnWidth + cellSpacing;
      }
      if (this.Layout.Owner.RightToLeft)
      {
        for (int index = 0; index < count; ++index)
        {
          TableViewCellArrangeInfo column = columns[index];
          column.OffsetX = num - column.OffsetX - column.Column.Width - cellSpacing;
        }
      }
      if (columns == this.scrollableColumns)
        num -= cellSpacing;
      return num;
    }

    public virtual TableViewCellArrangeInfo GetArrangeInfo(
      GridViewColumn column)
    {
      TableViewCellArrangeInfo viewCellArrangeInfo;
      this.arrangeInfos.TryGetValue(column.GetHashCode(), out viewCellArrangeInfo);
      return viewCellArrangeInfo;
    }

    protected virtual void Reset()
    {
      this.arrangeInfos.Clear();
      this.leftPinnedColumns.Clear();
      this.scrollableColumns.Clear();
      this.rightPinnedColumns.Clear();
    }
  }
}
