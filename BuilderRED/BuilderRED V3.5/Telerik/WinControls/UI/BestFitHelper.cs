// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BestFitHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  internal class BestFitHelper
  {
    private GridTableElement tableElement;
    private bool updateRowsHeight;
    private int processing;

    public BestFitHelper(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    protected GridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    protected BestFitQueue BestFitQueue
    {
      get
      {
        return this.TableElement.ViewTemplate.BestFitQueue;
      }
    }

    public void BestFitColumn(GridViewColumn column)
    {
      this.BestFitQueue.EnqueueBestFitColumn(column);
      this.ProcessRequests();
    }

    public void BestFitColumns()
    {
      this.BestFitQueue.EnqueueBestFitColumns();
      this.ProcessRequests();
    }

    public void BestFitColumns(BestFitColumnMode mode)
    {
      this.BestFitQueue.EnqueueBestFitColumns(mode);
      this.ProcessRequests();
    }

    public void ProcessRequests()
    {
      if (this.processing > 0 || this.tableElement == null || this.tableElement.ViewTemplate == null)
        return;
      ++this.processing;
      if (this.BestFitQueue.Count == 0 || !this.TableElement.CanBestFit())
      {
        --this.processing;
      }
      else
      {
        this.TableElement.UpdateLayout();
        if (this.TableElement.VisualRows.Count == 0 || this.tableElement.ViewTemplate == null || this.TableElement.ViewTemplate.Columns.Count == 0)
          --this.processing;
        else if (this.TableElement.ViewInfo.ParentRow != null && this.TableElement.DesiredSize == SizeF.Empty)
        {
          this.TableElement.GridViewElement.UpdateLayout();
          --this.processing;
        }
        else
        {
          this.updateRowsHeight = false;
          while (this.BestFitQueue.Count > 0)
          {
            BestFitQueue.BestFitRequest bestFitRequest = this.BestFitQueue.Dequeue();
            BestFitColumnMode? autoSizeMode = bestFitRequest.AutoSizeMode;
            if (bestFitRequest.Operation == BestFitQueue.BestFitOperation.BestFitAllColumns)
            {
              this.BestFitColumnsCore(autoSizeMode);
            }
            else
            {
              GridViewGroupColumn column = bestFitRequest.Column as GridViewGroupColumn;
              if (column != null)
              {
                this.BestFitGroupColumn(column, true, new BestFitColumnMode?());
              }
              else
              {
                BestFitColumnMode mode = autoSizeMode.HasValue ? autoSizeMode.Value : bestFitRequest.Column.AutoSizeMode;
                this.BestFitColumnCore(bestFitRequest.Column, mode);
              }
            }
          }
          this.UpdateLayout();
          this.SynchronizeViews();
          --this.processing;
        }
      }
    }

    protected virtual void BestFitColumnsCore(BestFitColumnMode? mode)
    {
      BestFitColumnMode? nullable = mode;
      if ((nullable.GetValueOrDefault() != BestFitColumnMode.None ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
        return;
      List<GridViewDataColumn> columns = new List<GridViewDataColumn>((IEnumerable<GridViewDataColumn>) this.tableElement.ViewTemplate.Columns);
      ColumnGroupRowLayout groupLayout = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (groupLayout != null)
        columns.Sort((Comparison<GridViewDataColumn>) ((A, B) =>
        {
          ColumnGroupsCellArrangeInfo columnData1 = groupLayout.GetColumnData((GridViewColumn) A);
          ColumnGroupsCellArrangeInfo columnData2 = groupLayout.GetColumnData((GridViewColumn) B);
          if (columnData1 == columnData2)
            return 0;
          if (columnData1 == null)
            return -1;
          if (columnData2 == null)
            return 1;
          int num1 = columnData1.Group.Rows.IndexOf(columnData1.Row);
          int num2 = columnData2.Group.Rows.IndexOf(columnData2.Row);
          if (columnData1.Group != columnData2.Group)
            return columnData1.Bounds.X.CompareTo(columnData2.Bounds.X);
          if (num1 != num2)
            return num1.CompareTo(num2);
          return columnData1.Bounds.X.CompareTo(columnData2.Bounds.X);
        }));
      float[] numArray = new float[this.tableElement.ViewTemplate.ColumnCount];
      int index1 = -1;
      if (mode.HasValue && mode.Value == BestFitColumnMode.AllCells)
      {
        numArray = this.CalculateColumnWidths(columns);
      }
      else
      {
        foreach (GridViewColumn column in columns)
        {
          if (!(column is GridViewGroupColumn))
          {
            ++index1;
            BestFitColumnMode mode1 = mode.HasValue ? mode.Value : column.AutoSizeMode;
            if (column.IsVisible && mode1 != BestFitColumnMode.None)
            {
              float columnWidth = this.CalculateColumnWidth(column, mode1);
              float val1 = numArray[index1];
              numArray[index1] = Math.Max(val1, columnWidth);
            }
          }
        }
      }
      for (int index2 = 0; index2 < numArray.Length; ++index2)
      {
        GridViewColumn column = (GridViewColumn) columns[index2];
        if (column != null && column.IsVisible && column.AutoSizeMode != BestFitColumnMode.None && ((!column.IsGrouped || column.OwnerTemplate.ShowGroupedColumns) && column.CanStretch))
          this.SetColumnWidth(column, numArray[index2]);
      }
      if (this.tableElement.ViewTemplate.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill)
        return;
      this.BestFitGroupColumns(mode);
    }

    protected virtual void BestFitColumnCore(GridViewColumn column, BestFitColumnMode mode)
    {
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (mode == BestFitColumnMode.None || !column.IsVisible)
        return;
      float columnWidth = this.CalculateColumnWidth(column, mode);
      this.SetColumnWidth(column, columnWidth);
    }

    protected virtual float[] CalculateColumnWidths(List<GridViewDataColumn> columns)
    {
      float[] widths = new float[columns.Count];
      IEnumerator<GridViewRowInfo> enumerator = (IEnumerator<GridViewRowInfo>) new ExpandedRowTraverser(this.tableElement.ViewInfo);
      while (enumerator.MoveNext())
        widths = this.MeasureCells(enumerator.Current, columns, widths);
      return widths;
    }

    protected virtual float CalculateColumnWidth(GridViewColumn column, BestFitColumnMode mode)
    {
      float val1 = 0.0f;
      IEnumerator<GridViewRowInfo> enumerator;
      if (mode == BestFitColumnMode.AllCells)
      {
        enumerator = (IEnumerator<GridViewRowInfo>) new ExpandedRowTraverser(this.tableElement.ViewInfo);
        foreach (GridViewRowInfo row in column.OwnerTemplate.Rows)
          val1 = Math.Max(val1, this.MeasureCell(row, column, mode));
      }
      else
        enumerator = (IEnumerator<GridViewRowInfo>) new VisualRowEnumerator(this.tableElement.VisualRows);
      enumerator.Reset();
      while (enumerator.MoveNext())
        val1 = Math.Max(val1, this.MeasureCell(enumerator.Current, column, mode));
      return val1;
    }

    protected virtual float[] MeasureCells(
      GridViewRowInfo row,
      List<GridViewDataColumn> columns,
      float[] widths)
    {
      IVirtualizedElementProvider<GridViewColumn> elementProvider1 = this.tableElement.ColumnScroller.ElementProvider;
      IVirtualizedElementProvider<GridViewRowInfo> elementProvider2 = this.tableElement.RowScroller.ElementProvider;
      if (this.CanBestFitRow(row))
      {
        GridRowElement element1 = elementProvider2.GetElement(row, (object) null) as GridRowElement;
        element1.InitializeRowView(this.TableElement);
        this.TableElement.Children.Add((RadElement) element1);
        element1.Initialize(row);
        for (int index = 0; index < columns.Count; ++index)
        {
          if (columns[index].IsVisible && columns[index].AutoSizeMode != BestFitColumnMode.None)
          {
            GridCellElement element2 = elementProvider1.GetElement((GridViewColumn) columns[index], (object) element1) as GridCellElement;
            element1.Children.Add((RadElement) element2);
            element2.Initialize((GridViewColumn) columns[index], element1);
            element2.SetContent();
            element2.UpdateInfo();
            (element2 as GridHeaderCellElement)?.UpdateArrowState();
            element2.ResetLayout(true);
            widths[index] = Math.Max(widths[index], this.GetCellDesiredWidth(element2));
            element1.Children.Remove((RadElement) element2);
            this.Detach(elementProvider1, element2);
          }
        }
        this.TableElement.Children.Remove((RadElement) element1);
        this.Detach(elementProvider2, element1);
      }
      return widths;
    }

    protected virtual float MeasureCell(
      GridViewRowInfo row,
      GridViewColumn column,
      BestFitColumnMode mode)
    {
      float val1 = 0.0f;
      IVirtualizedElementProvider<GridViewColumn> elementProvider1 = this.tableElement.ColumnScroller.ElementProvider;
      IVirtualizedElementProvider<GridViewRowInfo> elementProvider2 = this.tableElement.RowScroller.ElementProvider;
      if (this.CanBestFitRow(row))
      {
        GridRowElement element1 = elementProvider2.GetElement(row, (object) null) as GridRowElement;
        element1.InitializeRowView(this.TableElement);
        this.TableElement.Children.Add((RadElement) element1);
        element1.Initialize(row);
        GridCellElement element2 = elementProvider1.GetElement(column, (object) element1) as GridCellElement;
        if (!element2.CanBestFit(mode))
        {
          this.Detach(elementProvider1, element2);
          this.Detach(elementProvider2, element1);
          return val1;
        }
        element1.Children.Add((RadElement) element2);
        element2.Initialize(column, element1);
        element2.SetContent();
        element2.UpdateInfo();
        (element2 as GridHeaderCellElement)?.UpdateArrowState();
        element2.ResetLayout(true);
        val1 = Math.Max(val1, this.GetCellDesiredWidth(element2));
        element1.Children.Remove((RadElement) element2);
        this.TableElement.Children.Remove((RadElement) element1);
        this.Detach(elementProvider1, element2);
        this.Detach(elementProvider2, element1);
      }
      return val1;
    }

    protected virtual void BestFitGroupColumns(BestFitColumnMode? mode)
    {
      ColumnGroupRowLayout rowLayout = this.tableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (rowLayout == null)
        return;
      foreach (GridViewGroupColumn groupColumn in this.GetGroupColumns(rowLayout))
        this.BestFitGroupColumn(groupColumn, false, mode);
    }

    private void BestFitGroupColumn(
      GridViewGroupColumn column,
      bool bestFitChildColumns,
      BestFitColumnMode? mode)
    {
      GridTableHeaderRowElement headerRowElement = this.GetHeaderRowElement();
      IVirtualizedElementProvider<GridViewColumn> elementProvider = this.tableElement.ColumnScroller.ElementProvider;
      ColumnGroupRowLayout rowLayout = this.tableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (rowLayout == null || headerRowElement == null)
        return;
      GridColumnGroupCellElement element = elementProvider.GetElement((GridViewColumn) column, (object) headerRowElement) as GridColumnGroupCellElement;
      if (element == null || !element.CanBestFit(column.AutoSizeMode))
        return;
      float num = this.MeasureCell((GridRowElement) headerRowElement, (GridViewColumn) column, element);
      this.Detach(elementProvider, (GridCellElement) element);
      ColumnGroupsCellArrangeInfo columnData = rowLayout.GetColumnData((GridViewColumn) column);
      if ((double) columnData.Bounds.Width < (double) num)
      {
        rowLayout.StartColumnResize((GridViewColumn) column);
        rowLayout.ResizeColumn((int) ((double) num - (double) columnData.Bounds.Width));
        rowLayout.InvalidateLayout();
        this.TableElement.ViewElement.InvalidateMeasure(true);
        this.TableElement.ViewElement.UpdateLayout();
        rowLayout.EndColumnResize();
      }
      if (!bestFitChildColumns)
        return;
      foreach (GridViewColumn dataColumn in this.GetDataColumns(column.Group))
        this.BestFitColumnCore(dataColumn, mode.HasValue ? mode.Value : column.AutoSizeMode);
    }

    private IEnumerable<GridViewColumn> GetDataColumns(
      GridViewColumnGroup group)
    {
      if (group.Groups.Count > 0)
      {
        foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) group.Groups)
        {
          foreach (GridViewColumn dataColumn in this.GetDataColumns(group1))
            yield return dataColumn;
        }
      }
      else
      {
        foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
        {
          foreach (string columnName in (Collection<string>) row.ColumnNames)
            yield return (GridViewColumn) this.tableElement.ViewTemplate.Columns[columnName];
        }
      }
    }

    private List<GridViewGroupColumn> GetGroupColumns(
      ColumnGroupRowLayout columGroupRowLayout)
    {
      List<GridViewGroupColumn> gridViewGroupColumnList = new List<GridViewGroupColumn>();
      foreach (GridViewColumn scrollableColumn in (IEnumerable<GridViewColumn>) columGroupRowLayout.ScrollableColumns)
      {
        GridViewGroupColumn gridViewGroupColumn = scrollableColumn as GridViewGroupColumn;
        if (gridViewGroupColumn != null)
          gridViewGroupColumnList.Add(gridViewGroupColumn);
      }
      return gridViewGroupColumnList;
    }

    private GridTableHeaderRowElement GetHeaderRowElement()
    {
      foreach (GridRowElement visualRow in (VisualRowsCollection) this.TableElement.VisualRows)
      {
        GridTableHeaderRowElement headerRowElement = visualRow as GridTableHeaderRowElement;
        if (headerRowElement != null)
          return headerRowElement;
      }
      return (GridTableHeaderRowElement) null;
    }

    private float MeasureCell(
      GridRowElement row,
      GridViewColumn column,
      GridColumnGroupCellElement cell)
    {
      cell.Initialize(column, row);
      cell.SetContent();
      cell.UpdateInfo();
      row.SuspendLayout();
      row.Children.Add((RadElement) cell);
      cell.ResetLayout(true);
      float cellDesiredWidth = this.GetCellDesiredWidth((GridCellElement) cell);
      row.Children.Remove((RadElement) cell);
      row.ResumeLayout(false);
      return cellDesiredWidth;
    }

    private void Detach(
      IVirtualizedElementProvider<GridViewColumn> cellElementProvider,
      GridCellElement cell)
    {
      GridVirtualizedCellElement virtualizedCellElement = cell as GridVirtualizedCellElement;
      if (virtualizedCellElement != null)
      {
        cellElementProvider.CacheElement((IVirtualizedElement<GridViewColumn>) virtualizedCellElement);
        virtualizedCellElement.Detach();
        if (virtualizedCellElement.Parent == null || !virtualizedCellElement.Parent.Children.Contains((RadElement) virtualizedCellElement))
          return;
        virtualizedCellElement.Parent.Children.Remove((RadElement) virtualizedCellElement);
      }
      else
        cell.Dispose();
    }

    private void Detach(
      IVirtualizedElementProvider<GridViewRowInfo> rowElementProvider,
      GridRowElement row)
    {
      GridVirtualizedRowElement virtualizedRowElement = row as GridVirtualizedRowElement;
      if (virtualizedRowElement != null)
      {
        rowElementProvider.CacheElement((IVirtualizedElement<GridViewRowInfo>) virtualizedRowElement);
        virtualizedRowElement.Detach();
        if (virtualizedRowElement.Parent == null || !virtualizedRowElement.Parent.Children.Contains((RadElement) virtualizedRowElement))
          return;
        virtualizedRowElement.Parent.Children.Remove((RadElement) virtualizedRowElement);
      }
      else
        row.Dispose();
    }

    protected virtual bool CanBestFitRow(GridViewRowInfo row)
    {
      return row is GridViewTableHeaderRowInfo || row is GridViewDataRowInfo || (row is GridViewFilteringRowInfo || row is GridViewSummaryRowInfo);
    }

    protected virtual float GetCellDesiredWidth(GridCellElement cell)
    {
      cell.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
      return cell.DesiredSize.Width;
    }

    private float GetFilterCellDesiredWidth(GridFilterCellElement filterCell, float contentWidth)
    {
      float num = 0.0f;
      string localizedString = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionNoFilter");
      if (filterCell.FilterOperatorText != null && filterCell.FilterOperatorText.Text != localizedString)
      {
        SizeF availableSize = new SizeF(float.MaxValue, float.MaxValue);
        TextParams textParams = filterCell.FilterOperatorText.TextParams;
        num = filterCell.FilterOperatorText.Impl.MeasureOverride(availableSize, textParams).Width;
      }
      float width = filterCell.FilterButton.DesiredSize.Width;
      return contentWidth + num + width;
    }

    protected virtual void SetColumnWidth(GridViewColumn column, float desiredWidth)
    {
      if (column == null)
        return;
      if (column.MaxWidth > 0 && (double) desiredWidth > (double) column.MaxWidth)
        desiredWidth = (float) column.MaxWidth;
      if ((double) desiredWidth < (double) column.MinWidth)
        desiredWidth = (float) column.MinWidth;
      if (!this.updateRowsHeight)
        this.updateRowsHeight = (double) column.Width != (double) desiredWidth && column.WrapText && this.tableElement.GridViewElement.AutoSizeRows;
      TableViewRowLayout rowLayout1 = this.TableElement.ViewElement.RowLayout as TableViewRowLayout;
      ColumnGroupRowLayout rowLayout2 = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (column.OwnerTemplate.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill && rowLayout1 != null)
        rowLayout1.StretchColumn(column, (int) desiredWidth);
      else if (rowLayout2 != null)
        rowLayout2.SetBestFitWidth(column, desiredWidth);
      else
        column.Width = (int) Math.Round((double) desiredWidth / (double) column.DpiScale.Width);
    }

    private void UpdateLayout()
    {
      this.TableElement.ViewElement.UpdateRows();
      if (!this.TableElement.GridViewElement.AutoSizeRows)
        return;
      this.TableElement.InvalidateArrange();
    }

    private void SynchronizeViews()
    {
      if (this.TableElement.GridViewElement.SplitMode == RadGridViewSplitMode.None)
        return;
      foreach (RadElement child in this.TableElement.GridViewElement.Panel.Children)
      {
        GridTableElement gridTableElement = child as GridTableElement;
        if (gridTableElement != null && gridTableElement != this.TableElement)
        {
          gridTableElement.ViewElement.UpdateRows();
          if (gridTableElement.GridViewElement.AutoSizeRows)
            gridTableElement.InvalidateArrange();
        }
      }
    }
  }
}
