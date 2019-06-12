// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TableViewRowLayoutBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public abstract class TableViewRowLayoutBase : IGridRowLayout, IDisposable
  {
    private List<GridViewColumn> renderColumns = new List<GridViewColumn>();
    private List<GridViewIndentColumn> cachedIndentColumns = new List<GridViewIndentColumn>();
    private List<GridViewIndentColumn> indentColumns = new List<GridViewIndentColumn>();
    private GridViewTemplate viewTemplate;
    private GridTableElement owner;
    private GridViewRowHeaderColumn rowHeaderColumn;
    private GridViewDataColumn firstDataColumn;
    private GridViewDataColumn lastDataColumn;
    private bool ignoreColumnVisibility;
    private GridLayoutContext context;

    public GridViewTemplate ViewTemplate
    {
      get
      {
        return this.viewTemplate;
      }
    }

    public bool IgnoreColumnVisibility
    {
      get
      {
        return this.ignoreColumnVisibility;
      }
      set
      {
        this.ignoreColumnVisibility = value;
      }
    }

    public GridLayoutContext Context
    {
      get
      {
        return this.context;
      }
      set
      {
        this.context = value;
      }
    }

    public virtual int GetColumnWidth(GridViewColumn column)
    {
      int val1 = Math.Max((int) this.Owner.ColumnScroller.ElementProvider.GetElementSize(column).Width, column.MinWidth);
      if (column.MaxWidth > 0)
        return Math.Min(val1, column.MaxWidth);
      return val1;
    }

    public abstract int GetColumnOffset(GridViewColumn column);

    public GridTableElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public abstract SizeF DesiredSize { get; }

    public virtual SizeF GroupRowDesiredSize
    {
      get
      {
        return this.DesiredSize;
      }
    }

    public virtual IList<GridViewColumn> RenderColumns
    {
      get
      {
        return (IList<GridViewColumn>) this.renderColumns;
      }
    }

    public abstract IList<GridViewColumn> ScrollableColumns { get; }

    public GridViewDataColumn FirstDataColumn
    {
      get
      {
        return this.firstDataColumn;
      }
    }

    public GridViewDataColumn LastDataColumn
    {
      get
      {
        return this.lastDataColumn;
      }
    }

    public virtual void Initialize(GridTableElement tableElement)
    {
      this.owner = tableElement;
      this.viewTemplate = this.owner.ViewTemplate;
      this.InvalidateRenderColumns();
      if (this.Context == GridLayoutContext.Printer)
        return;
      tableElement.ColumnScroller.Traverser = (ITraverser<GridViewColumn>) new ItemsTraverser<GridViewColumn>(this.ScrollableColumns);
    }

    public abstract SizeF MeasureRow(SizeF availableSize);

    public abstract RectangleF ArrangeCell(RectangleF clientRect, GridCellElement cell);

    public abstract void StartColumnResize(GridViewColumn column);

    public abstract void EndColumnResize();

    public abstract void ResizeColumn(int delta);

    public virtual void InvalidateRenderColumns()
    {
      this.SetFirstDataColumn((GridViewDataColumn) null);
      this.SetLastDataColumn((GridViewDataColumn) null);
      this.cachedIndentColumns.AddRange((IEnumerable<GridViewIndentColumn>) this.indentColumns);
      this.indentColumns.Clear();
      this.RenderColumns.Clear();
      if (this.ViewTemplate.ColumnCount == 0)
        return;
      if (this.ViewTemplate.ShowRowHeaderColumn)
        this.RenderColumns.Add((GridViewColumn) this.GetRowHeaderColumn());
      if (this.ViewTemplate.EnableGrouping)
      {
        for (int level = 0; level < this.ViewTemplate.DataView.GroupDescriptors.Count; ++level)
          this.RenderColumns.Add((GridViewColumn) this.GetIndentColumn(level));
      }
      if (this.ViewTemplate.IsSelfReference || this.ViewTemplate.Templates.Count <= 0)
        return;
      this.RenderColumns.Add((GridViewColumn) this.GetIndentColumn(-1));
    }

    public abstract void InvalidateLayout();

    public virtual int GetRowHeight(GridViewRowInfo rowInfo)
    {
      GridViewDetailsRowInfo viewDetailsRowInfo = rowInfo as GridViewDetailsRowInfo;
      if (viewDetailsRowInfo != null && viewDetailsRowInfo.ActualHeight != -1)
        return viewDetailsRowInfo.ActualHeight;
      int val1 = rowInfo.Height;
      if (val1 < 1)
        val1 = !(rowInfo is GridViewTableHeaderRowInfo) ? (!(rowInfo is GridViewFilteringRowInfo) ? (!(rowInfo is GridViewSearchRowInfo) ? (!(rowInfo is GridViewDetailsRowInfo) ? (!(rowInfo is GridViewGroupRowInfo) ? this.Owner.RowHeight : this.Owner.GroupHeaderHeight) : this.Owner.ChildRowHeight) : this.Owner.SearchRowHeight) : this.Owner.FilterRowHeight) : this.Owner.TableHeaderHeight;
      return Math.Max(val1, 1);
    }

    public virtual SizeF MeasurePinnedColumns(PinnedColumnTraverser dataProvider)
    {
      SizeF empty = SizeF.Empty;
      int num = 0;
      CellElementProvider elementProvider = this.Owner.ColumnScroller.ElementProvider as CellElementProvider;
      foreach (GridViewColumn gridViewColumn in (ItemsTraverser<GridViewColumn>) dataProvider)
      {
        empty.Width += elementProvider.GetElementSize(gridViewColumn).Width;
        ++num;
      }
      if (num > 0)
        empty.Width += (float) ((num - 1) * this.Owner.CellSpacing);
      return empty;
    }

    public abstract void EnsureColumnsLayout();

    public virtual void Dispose()
    {
      this.viewTemplate = (GridViewTemplate) null;
      this.owner = (GridTableElement) null;
    }

    public virtual bool ColumnIsVisible(GridViewColumn column)
    {
      if (column == null || !column.IsVisible)
        return false;
      if (this.ViewTemplate.EnableGrouping && !this.ViewTemplate.ShowGroupedColumns)
        return !column.IsGrouped;
      return true;
    }

    private GridViewRowHeaderColumn GetRowHeaderColumn()
    {
      if (this.rowHeaderColumn == null)
        this.rowHeaderColumn = new GridViewRowHeaderColumn(this.ViewTemplate);
      return this.rowHeaderColumn;
    }

    private GridViewIndentColumn GetIndentColumn(int level)
    {
      for (int index = this.cachedIndentColumns.Count - 1; index >= 0; --index)
      {
        GridViewIndentColumn cachedIndentColumn = this.cachedIndentColumns[index];
        if (cachedIndentColumn.IndentLevel == level)
        {
          this.cachedIndentColumns.RemoveAt(index);
          this.indentColumns.Add(cachedIndentColumn);
          return cachedIndentColumn;
        }
      }
      GridViewIndentColumn viewIndentColumn = new GridViewIndentColumn(this.ViewTemplate, level);
      this.indentColumns.Add(viewIndentColumn);
      return viewIndentColumn;
    }

    protected void SetFirstDataColumn(GridViewDataColumn column)
    {
      this.firstDataColumn = column;
    }

    protected void SetLastDataColumn(GridViewDataColumn column)
    {
      this.lastDataColumn = column;
    }
  }
}
