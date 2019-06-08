// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StretchColumnLayoutHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class StretchColumnLayoutHelper : ColumnLayoutHelper
  {
    private List<TableViewCellArrangeInfo> stretchableColumns = new List<TableViewCellArrangeInfo>();
    private List<TableViewCellArrangeInfo> nonStretchableColumns = new List<TableViewCellArrangeInfo>();
    private int stretchableColumnResizeIndex;
    private int availableWidth;
    private int stretchableWidth;
    private int nonStretchableWidth;
    private int startOffset;
    private int stretchableStartWidth;
    private bool saveScaleFactors;

    public StretchColumnLayoutHelper(TableViewRowLayout layout)
      : base(layout)
    {
    }

    public int AvailableWidth
    {
      get
      {
        return this.availableWidth;
      }
    }

    public int StretchableWidth
    {
      get
      {
        return this.stretchableWidth;
      }
    }

    public int FixedWidth
    {
      get
      {
        return this.nonStretchableWidth;
      }
    }

    public List<TableViewCellArrangeInfo> StretchableColumns
    {
      get
      {
        return this.stretchableColumns;
      }
    }

    public List<TableViewCellArrangeInfo> FixedColumns
    {
      get
      {
        return this.nonStretchableColumns;
      }
    }

    public override int CalculateColumnsWidth(SizeF availableSize)
    {
      int cellSpacing = this.Layout.Owner.CellSpacing;
      this.availableWidth = (int) availableSize.Width;
      this.nonStretchableWidth = 0;
      for (int index = 0; index < this.nonStretchableColumns.Count; ++index)
      {
        int columnWidth = this.Layout.GetColumnWidth(this.nonStretchableColumns[index].Column);
        if (index < this.nonStretchableColumns.Count - 1)
          columnWidth += cellSpacing;
        this.nonStretchableWidth += columnWidth;
        this.availableWidth -= columnWidth;
      }
      this.availableWidth -= this.stretchableColumns.Count * cellSpacing;
      int num1 = 0;
      for (int index = 0; index < this.stretchableColumns.Count; ++index)
      {
        GridViewColumn column = this.stretchableColumns[index].Column;
        num1 += this.Layout.GetColumnWidth(column);
      }
      int num2 = 0;
      for (int index = 0; index < this.stretchableColumns.Count; ++index)
      {
        TableViewCellArrangeInfo stretchableColumn = this.stretchableColumns[index];
        double columnWidth = (double) this.Layout.GetColumnWidth(stretchableColumn.Column);
        if (stretchableColumn.ScaleFactor == 0.0 || stretchableColumn.CachedWidth != columnWidth)
          stretchableColumn.ScaleFactor = columnWidth / (double) num1;
        stretchableColumn.SetWidth((int) Math.Round(stretchableColumn.ScaleFactor * (double) this.availableWidth), true, this.Layout.Context == GridLayoutContext.Printer);
        num2 += this.Layout.GetColumnWidth(stretchableColumn.Column) + cellSpacing;
      }
      int num3 = num2 + this.nonStretchableWidth;
      for (int index = this.stretchableColumns.Count - 1; num3 != this.availableWidth && index >= 0; --index)
      {
        TableViewCellArrangeInfo stretchableColumn = this.stretchableColumns[index];
        int columnWidth = this.Layout.GetColumnWidth(stretchableColumn.Column);
        if (columnWidth < stretchableColumn.Column.MaxWidth || stretchableColumn.Column.MaxWidth == 0)
        {
          int width = columnWidth - (num3 - (int) availableSize.Width);
          if (width >= stretchableColumn.Column.MinWidth)
          {
            int num4 = num3 + (width - columnWidth);
            stretchableColumn.SetWidth(width, true, this.Layout.Context == GridLayoutContext.Printer);
            break;
          }
        }
      }
      base.CalculateColumnsWidth(availableSize);
      return (int) availableSize.Width;
    }

    public override RectangleF GetCellArrangeRect(RectangleF client, GridCellElement cell)
    {
      RectangleF cellArrangeRect = base.GetCellArrangeRect(client, cell);
      this.GetArrangeInfo(cell.ColumnInfo);
      return cellArrangeRect;
    }

    public override void StartColumnResize(GridViewColumn column)
    {
      base.StartColumnResize(column);
      TableViewCellArrangeInfo arrangeInfo = this.GetArrangeInfo(column);
      if (arrangeInfo == null || !this.stretchableColumns.Contains(arrangeInfo))
        return;
      this.stretchableColumnResizeIndex = this.stretchableColumns.IndexOf(arrangeInfo);
      if (this.Layout.Owner.RightToLeft)
      {
        this.startOffset = arrangeInfo.OffsetX;
        this.stretchableWidth = arrangeInfo.OffsetX;
      }
      else
      {
        this.stretchableWidth = this.availableWidth - arrangeInfo.OffsetX - this.Layout.GetColumnWidth(arrangeInfo.Column);
        this.stretchableStartWidth = this.stretchableWidth;
      }
      if (this.stretchableColumnResizeIndex + 1 == this.stretchableColumns.Count - 1)
      {
        this.stretchableColumns[this.stretchableColumnResizeIndex + 1].ResizeStartScaleFactor = 1.0;
      }
      else
      {
        for (int index = this.stretchableColumnResizeIndex + 1; index < this.stretchableColumns.Count; ++index)
        {
          TableViewCellArrangeInfo stretchableColumn = this.stretchableColumns[index];
          stretchableColumn.ResizeStartScaleFactor = (double) stretchableColumn.ResizeStartWidth / (double) this.stretchableWidth;
        }
      }
      this.saveScaleFactors = false;
    }

    public override void ResizeColumn(int delta)
    {
      if (this.stretchableColumns.Count == 0)
        return;
      TableViewCellArrangeInfo stretchableColumn1 = this.stretchableColumns[this.stretchableColumnResizeIndex];
      stretchableColumn1.ScaleFactor = 0.0;
      int width1 = stretchableColumn1.ClampWidth(stretchableColumn1.ResizeStartWidth + delta);
      if (width1 == stretchableColumn1.ResizeStartWidth)
        return;
      int num1;
      if (this.Layout.Owner.RightToLeft)
      {
        num1 = this.startOffset - delta;
      }
      else
      {
        int num2 = stretchableColumn1.OffsetX - this.nonStretchableWidth;
        foreach (TableViewCellArrangeInfo stretchableColumn2 in this.nonStretchableColumns)
        {
          if (stretchableColumn2.Column.IsPinned)
            num2 += stretchableColumn2.Column.Width + this.Layout.Owner.CellSpacing;
        }
        num1 = this.availableWidth - num2 - width1;
      }
      List<StretchColumnLayoutHelper.ResizeInfo> resizeInfoList = new List<StretchColumnLayoutHelper.ResizeInfo>();
      int num3 = num1;
      int num4 = 0;
      for (int index = this.stretchableColumnResizeIndex + 1; index < this.stretchableColumns.Count; ++index)
      {
        TableViewCellArrangeInfo stretchableColumn2 = this.stretchableColumns[index];
        int width2 = stretchableColumn2.Column.Width;
        int width3 = (int) Math.Round(stretchableColumn2.ResizeStartScaleFactor * (double) num1);
        if (width3 != width2)
        {
          if (width3 > 0 && width3 >= stretchableColumn2.Column.MinWidth)
          {
            resizeInfoList.Add(new StretchColumnLayoutHelper.ResizeInfo(stretchableColumn2, width3));
            num4 += width2;
          }
          else
            num3 -= width2 + this.Layout.Owner.CellSpacing;
        }
      }
      if (resizeInfoList.Count > 0)
      {
        foreach (StretchColumnLayoutHelper.ResizeInfo resizeInfo in resizeInfoList)
        {
          resizeInfo.Info.ScaleFactor = 0.0;
          if (num3 != num1)
          {
            this.saveScaleFactors = true;
            if (resizeInfo.Info.TempScaleFactor == 0.0)
              resizeInfo.Info.TempScaleFactor = (double) resizeInfo.Info.Column.Width / (double) num4;
            resizeInfo.Width = (int) Math.Floor(resizeInfo.Info.TempScaleFactor * (double) num3);
          }
          resizeInfo.Info.SetWidth(resizeInfo.Width, true, this.Layout.Context == GridLayoutContext.Printer);
        }
        stretchableColumn1.SetWidth(width1, false, this.Layout.Context == GridLayoutContext.Printer);
      }
      if (num3 != num1)
        return;
      if (this.saveScaleFactors)
      {
        foreach (TableViewCellArrangeInfo viewCellArrangeInfo in this.ArrangeInfos.Values)
          viewCellArrangeInfo.TempScaleFactor = 0.0;
      }
      this.saveScaleFactors = false;
    }

    protected override TableViewCellArrangeInfo InitColumn(
      GridViewColumn column)
    {
      TableViewCellArrangeInfo viewCellArrangeInfo = base.InitColumn(column);
      if (!column.AllowResize)
        this.nonStretchableColumns.Add(viewCellArrangeInfo);
      else
        this.stretchableColumns.Add(viewCellArrangeInfo);
      return viewCellArrangeInfo;
    }

    protected override void Reset()
    {
      this.stretchableColumns.Clear();
      this.nonStretchableColumns.Clear();
      base.Reset();
    }

    private class ResizeInfo
    {
      public TableViewCellArrangeInfo Info;
      public int Width;

      public ResizeInfo(TableViewCellArrangeInfo info, int width)
      {
        this.Info = info;
        this.Width = width;
      }
    }
  }
}
