// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseGridPrintRenderer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class BaseGridPrintRenderer : IGridPrintRenderer
  {
    private RadGridView gridView;

    public BaseGridPrintRenderer(RadGridView grid)
    {
      this.gridView = grid;
    }

    public RadGridView GridView
    {
      get
      {
        return this.gridView;
      }
    }

    protected virtual CellPrintElement CreateHeaderCellPrintElement(
      GridViewColumn column)
    {
      CellPrintElement cellPrintElement = new CellPrintElement();
      cellPrintElement.BackColor = this.GridView.PrintStyle.HeaderCellBackColor;
      cellPrintElement.BorderColor = this.GridView.PrintStyle.BorderColor;
      Color backColor = cellPrintElement.BackColor;
      if (cellPrintElement.BackColor != Color.Transparent && cellPrintElement.BackColor != Color.Empty)
        cellPrintElement.DrawFill = true;
      cellPrintElement.Text = column.HeaderText;
      cellPrintElement.TextAlignment = column.HeaderTextAlignment;
      cellPrintElement.StringTrimming |= StringTrimming.EllipsisCharacter;
      if (this.GridView.RightToLeft == RightToLeft.Yes)
        cellPrintElement.StringFormatFlags |= StringFormatFlags.DirectionRightToLeft;
      if (!column.WrapText)
        cellPrintElement.StringFormatFlags |= StringFormatFlags.NoWrap;
      return cellPrintElement;
    }

    protected virtual CellPrintElement CreateGroupCellPrintElement(
      GridViewGroupRowInfo row)
    {
      CellPrintElement cellPrintElement1 = new CellPrintElement();
      cellPrintElement1.BackColor = this.GridView.PrintStyle.GroupRowBackColor;
      cellPrintElement1.BorderColor = this.GridView.PrintStyle.BorderColor;
      Color backColor = cellPrintElement1.BackColor;
      if (cellPrintElement1.BackColor != Color.Transparent && cellPrintElement1.BackColor != Color.Empty)
        cellPrintElement1.DrawFill = true;
      string summary = row.GetSummary();
      cellPrintElement1.Text = row.HeaderText;
      if (summary.Length > 0)
      {
        CellPrintElement cellPrintElement2 = cellPrintElement1;
        cellPrintElement2.Text = cellPrintElement2.Text + " | " + summary;
      }
      if (this.GridView.RightToLeft == RightToLeft.Yes)
        cellPrintElement1.StringFormatFlags |= StringFormatFlags.DirectionRightToLeft;
      cellPrintElement1.TextAlignment = ContentAlignment.MiddleLeft;
      return cellPrintElement1;
    }

    protected virtual CellPrintElement CreateDataCellPrintElement(
      GridViewCellInfo cellInfo)
    {
      CellPrintElement cellPrintElement;
      if (cellInfo.ColumnInfo is GridViewImageColumn)
      {
        cellPrintElement = this.CreateImageCellPrintElement(cellInfo);
        cellPrintElement.DrawText = false;
      }
      else
        cellPrintElement = new CellPrintElement();
      if (this.GridView.PrintStyle.PrintAlternatingRowColor && this.GridView.PrintStyle.PrintTraverser.Current.Index % 2 == 1)
        cellPrintElement.BackColor = this.GridView.PrintStyle.AlternatingRowColor;
      else
        cellPrintElement.BackColor = this.GridView.PrintStyle.CellBackColor;
      cellPrintElement.BorderColor = this.GridView.PrintStyle.BorderColor;
      Color backColor = cellPrintElement.BackColor;
      if (cellPrintElement.BackColor != Color.Transparent && cellPrintElement.BackColor != Color.Empty)
        cellPrintElement.DrawFill = true;
      GridViewDataColumn columnInfo1 = cellInfo.ColumnInfo as GridViewDataColumn;
      object lookupValue = cellInfo.Value;
      GridViewComboBoxColumn columnInfo2 = cellInfo.ColumnInfo as GridViewComboBoxColumn;
      if (columnInfo2 != null && columnInfo2.HasLookupValue)
        lookupValue = columnInfo2.GetLookupValue(lookupValue);
      if (columnInfo1 != null && !string.IsNullOrEmpty(columnInfo1.FormatString))
      {
        if (cellInfo.ColumnInfo is GridViewComboBoxColumn)
        {
          GridViewComboBoxColumn columnInfo3 = (GridViewComboBoxColumn) cellInfo.ColumnInfo;
          columnInfo1.DataType = columnInfo3.DisplayMemberDataType;
          columnInfo1.DataTypeConverter = TypeDescriptor.GetConverter(columnInfo3.DisplayMemberDataType);
          cellPrintElement.Text = RadDataConverter.Instance.Format(lookupValue, typeof (string), (IDataConversionInfoProvider) columnInfo1) as string;
        }
        else
          cellPrintElement.Text = RadDataConverter.Instance.Format(lookupValue, typeof (string), (IDataConversionInfoProvider) columnInfo1) as string;
      }
      else
        cellPrintElement.Text = lookupValue != null ? lookupValue.ToString() : string.Empty;
      cellPrintElement.TextAlignment = cellInfo.ColumnInfo.TextAlignment;
      cellPrintElement.StringTrimming = StringTrimming.EllipsisCharacter;
      if (this.GridView.RightToLeft == RightToLeft.Yes)
        cellPrintElement.StringFormatFlags |= StringFormatFlags.DirectionRightToLeft;
      if (!cellInfo.ColumnInfo.WrapText)
        cellPrintElement.StringFormatFlags |= StringFormatFlags.NoWrap;
      return cellPrintElement;
    }

    protected virtual CellPrintElement CreateSummaryCellPrintElement(
      GridViewCellInfo cellInfo)
    {
      CellPrintElement cellPrintElement = new CellPrintElement();
      cellPrintElement.BackColor = this.GridView.PrintStyle.SummaryCellBackColor;
      cellPrintElement.BorderColor = this.GridView.PrintStyle.BorderColor;
      Color backColor = cellPrintElement.BackColor;
      if (cellPrintElement.BackColor != Color.Transparent && cellPrintElement.BackColor != Color.Empty)
        cellPrintElement.DrawFill = true;
      cellPrintElement.Text = cellInfo.Value != null ? cellInfo.Value.ToString() : string.Empty;
      if (this.GridView.RightToLeft == RightToLeft.Yes)
        cellPrintElement.StringFormatFlags |= StringFormatFlags.DirectionRightToLeft;
      return cellPrintElement;
    }

    protected virtual CellPrintElement CreateImageCellPrintElement(
      GridViewCellInfo cellInfo)
    {
      CellPrintElement cellPrintElement = new CellPrintElement();
      object obj = cellInfo.Value;
      if (obj == null || obj == DBNull.Value)
      {
        cellPrintElement.Image = (Image) null;
        return cellPrintElement;
      }
      if (obj is byte[])
        cellPrintElement.Image = ImageHelper.GetImageFromBytes((byte[]) obj);
      else if (obj is Image)
        cellPrintElement.Image = obj as Image;
      if (cellPrintElement.Image != null)
      {
        GridViewImageColumn columnInfo = cellInfo.ColumnInfo as GridViewImageColumn;
        cellPrintElement.ImageLayout = columnInfo != null ? columnInfo.ImageLayout : ImageLayout.Stretch;
        cellPrintElement.ImageAlignment = columnInfo != null ? columnInfo.ImageAlignment : ContentAlignment.MiddleCenter;
      }
      return cellPrintElement;
    }

    protected internal virtual int GetDataRowHeight(
      GridViewRowInfo row,
      TableViewRowLayoutBase rowLayout)
    {
      if (!this.GridView.AutoSizeRows)
        return rowLayout.GetRowHeight(row);
      IVirtualizedElementProvider<GridViewColumn> elementProvider1 = this.GridView.TableElement.ColumnScroller.ElementProvider;
      IVirtualizedElementProvider<GridViewRowInfo> elementProvider2 = this.GridView.TableElement.RowScroller.ElementProvider;
      float val1 = 0.0f;
      GridRowElement element1 = elementProvider2.GetElement(row, (object) null) as GridRowElement;
      element1.InitializeRowView(this.GridView.TableElement);
      element1.Initialize(row);
      this.GridView.TableElement.Children.Add((RadElement) element1);
      if (row is GridViewGroupRowInfo)
      {
        GridGroupHeaderRowElement headerRowElement = element1 as GridGroupHeaderRowElement;
        headerRowElement.Measure(new SizeF(rowLayout.DesiredSize.Width, float.PositiveInfinity));
        val1 = Math.Max(val1, headerRowElement.DesiredSize.Height);
      }
      else
      {
        foreach (GridViewColumn column in (Collection<GridViewDataColumn>) row.ViewTemplate.Columns)
        {
          if (!(column is GridViewRowHeaderColumn) && !(column is GridViewIndentColumn) && column.IsVisible)
          {
            GridCellElement element2 = elementProvider1.GetElement(column, (object) element1) as GridCellElement;
            element1.Children.Add((RadElement) element2);
            element2.Initialize(column, element1);
            element2.SetContent();
            element2.UpdateInfo();
            (element2 as GridHeaderCellElement)?.UpdateArrowState();
            element2.ResetLayout(true);
            val1 = Math.Max(val1, this.GetCellDesiredSize(element2).Height);
            element1.Children.Remove((RadElement) element2);
            this.Detach(elementProvider1, element2);
          }
        }
      }
      this.GridView.TableElement.Children.Remove((RadElement) element1);
      this.Detach(elementProvider2, element1);
      return (int) val1;
    }

    protected virtual SizeF GetCellDesiredSize(GridCellElement cell)
    {
      cell.Measure(new SizeF((float) cell.ColumnInfo.Width, float.PositiveInfinity));
      return cell.DesiredSize;
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

    public abstract void DrawPage(
      PrintGridTraverser traverser,
      Rectangle drawArea,
      Graphics graphics,
      GridPrintSettings settings,
      int pageNumber);

    public abstract void Reset();

    public abstract System.Type ViewDefinitionType { get; }

    public event PrintCellPaintEventHandler PrintCellPaint;

    protected virtual void OnPrintCellPaint(PrintCellPaintEventArgs e)
    {
      if (this.PrintCellPaint == null)
        return;
      this.PrintCellPaint((object) this, e);
    }

    public event PrintCellFormattingEventHandler PrintCellFormatting;

    protected virtual void OnPrintCellFormatting(PrintCellFormattingEventArgs e)
    {
      if (this.PrintCellFormatting == null)
        return;
      this.PrintCellFormatting((object) this, e);
    }

    public event ChildViewPrintingEventHandler ChildViewPrinting;

    protected internal virtual void OnChildViewPrinting(ChildViewPrintingEventArgs e)
    {
      if (this.ChildViewPrinting == null)
        return;
      this.ChildViewPrinting((object) this, e);
    }
  }
}
