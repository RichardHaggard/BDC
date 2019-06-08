// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TableViewRowLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class TableViewRowLayout : TableViewRowLayoutBase
  {
    private ColumnLayoutHelper layoutHelper;
    private SizeF desiredSize;
    private SizeF groupRowDesiredSize;
    private SizeF cachedAvailableSize;
    private GridViewAutoSizeColumnsMode autoSizeColumnsMode;

    public ColumnLayoutHelper LayoutImpl
    {
      get
      {
        if (this.layoutHelper == null)
          this.UpdateLayoutHelper();
        return this.layoutHelper;
      }
    }

    public override SizeF DesiredSize
    {
      get
      {
        return this.desiredSize;
      }
    }

    public override SizeF GroupRowDesiredSize
    {
      get
      {
        return this.groupRowDesiredSize;
      }
    }

    public override IList<GridViewColumn> ScrollableColumns
    {
      get
      {
        return this.LayoutImpl.ScrollableColumns;
      }
    }

    public override SizeF MeasureRow(SizeF availableSize)
    {
      if (availableSize == SizeF.Empty)
        return availableSize;
      if (this.cachedAvailableSize == availableSize && (double) availableSize.Width != double.PositiveInfinity || (double) this.cachedAvailableSize.Width == (double) availableSize.Width && this.Owner.GridViewElement.AutoSizeRows && this.autoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill)
        return this.desiredSize;
      this.cachedAvailableSize = availableSize;
      if (this.layoutHelper == null || this.autoSizeColumnsMode != this.ViewTemplate.AutoSizeColumnsMode)
        this.UpdateLayoutHelper();
      if (this.layoutHelper != null)
      {
        this.desiredSize = new SizeF((float) this.LayoutImpl.CalculateColumnsWidth(availableSize), 0.0f);
        this.groupRowDesiredSize = (double) availableSize.Width >= (double) this.desiredSize.Width ? this.desiredSize : availableSize;
      }
      return this.desiredSize;
    }

    public override RectangleF ArrangeCell(RectangleF clientRect, GridCellElement cell)
    {
      GridDataCellElement gridDataCellElement = cell as GridDataCellElement;
      if (gridDataCellElement != null)
        gridDataCellElement.IsLeftMost = false;
      return this.LayoutImpl.GetCellArrangeRect(clientRect, cell);
    }

    public override int GetColumnWidth(GridViewColumn column)
    {
      if (this.Context == GridLayoutContext.Printer)
      {
        TableViewCellArrangeInfo arrangeInfo = this.layoutHelper.GetArrangeInfo(column);
        if (arrangeInfo != null)
          return (int) arrangeInfo.CachedWidth;
      }
      return base.GetColumnWidth(column);
    }

    public override int GetColumnOffset(GridViewColumn column)
    {
      TableViewCellArrangeInfo arrangeInfo = this.LayoutImpl.GetArrangeInfo(column);
      if (arrangeInfo == null)
        return 0;
      return (int) arrangeInfo.Bounds.X;
    }

    public override void StartColumnResize(GridViewColumn column)
    {
      this.LayoutImpl.StartColumnResize(column);
    }

    public override void EndColumnResize()
    {
      this.LayoutImpl.EndColumnResize();
    }

    public override void ResizeColumn(int delta)
    {
      this.LayoutImpl.ResizeColumn(delta);
    }

    public override void InvalidateLayout()
    {
      this.cachedAvailableSize = SizeF.Empty;
    }

    public void StretchColumn(GridViewColumn column, int desiredWidth)
    {
      if (this.Owner.ViewTemplate.AutoSizeColumnsMode != GridViewAutoSizeColumnsMode.Fill)
      {
        this.layoutHelper.GetArrangeInfo(column)?.SetWidth(desiredWidth, false, this.Context == GridLayoutContext.Printer);
      }
      else
      {
        bool flag = false;
        int num = 0;
        StretchColumnLayoutHelper layoutHelper = (StretchColumnLayoutHelper) this.layoutHelper;
        foreach (TableViewCellArrangeInfo stretchableColumn in layoutHelper.StretchableColumns)
        {
          if (stretchableColumn.Column == column)
            flag = true;
          else if (flag)
            num += stretchableColumn.Column.MinWidth + this.Owner.CellSpacing;
          else
            num += stretchableColumn.Column.Width + this.Owner.CellSpacing;
        }
        int availableWidth = layoutHelper.AvailableWidth;
        if (num + desiredWidth > availableWidth)
          desiredWidth -= num + desiredWidth - availableWidth - this.Owner.CellSpacing;
        this.StartColumnResize(column);
        this.ResizeColumn(desiredWidth - column.Width);
        this.EndColumnResize();
      }
    }

    public override void EnsureColumnsLayout()
    {
      if (this.layoutHelper.ScrollableColumns.Count == 0)
      {
        this.InvalidateLayout();
      }
      else
      {
        if (!(this.layoutHelper.GetArrangeInfo(this.layoutHelper.ScrollableColumns[0]).Bounds == RectangleF.Empty))
          return;
        this.InvalidateLayout();
      }
    }

    public override void InvalidateRenderColumns()
    {
      base.InvalidateRenderColumns();
      int count1 = this.RenderColumns.Count;
      int count2 = this.RenderColumns.Count;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
      {
        if ((column.IsVisible || this.IgnoreColumnVisibility) && (!this.ViewTemplate.EnableGrouping || this.ViewTemplate.ShowGroupedColumns || !column.IsGrouped))
        {
          if (column.PinPosition == PinnedColumnPosition.Left)
          {
            this.RenderColumns.Insert(count1, column);
            ++count1;
            ++count2;
          }
          else if (column.PinPosition == PinnedColumnPosition.Right)
          {
            this.RenderColumns.Add(column);
          }
          else
          {
            this.RenderColumns.Insert(count2, column);
            ++count2;
          }
        }
      }
      for (int index = 0; index < this.RenderColumns.Count && this.FirstDataColumn == null; ++index)
        this.SetFirstDataColumn(this.RenderColumns[index] as GridViewDataColumn);
      for (int index = this.RenderColumns.Count - 1; index >= 0 && this.LastDataColumn == null; --index)
        this.SetLastDataColumn(this.RenderColumns[index] as GridViewDataColumn);
      this.LayoutImpl.Initialize();
    }

    protected virtual void UpdateLayoutHelper()
    {
      if (this.ViewTemplate == null)
        return;
      this.layoutHelper = this.ViewTemplate.AutoSizeColumnsMode != GridViewAutoSizeColumnsMode.Fill ? new ColumnLayoutHelper(this) : (ColumnLayoutHelper) new StretchColumnLayoutHelper(this);
      this.autoSizeColumnsMode = this.ViewTemplate.AutoSizeColumnsMode;
    }
  }
}
