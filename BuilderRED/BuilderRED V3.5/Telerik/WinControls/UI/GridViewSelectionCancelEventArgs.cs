// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSelectionCancelEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewSelectionCancelEventArgs : CancelEventArgs
  {
    private List<GridViewRowInfo> rows = new List<GridViewRowInfo>();
    private GridViewColumn column;
    private int columnStartIndex;
    private int columnEndIndex;

    internal GridViewSelectionCancelEventArgs(GridCellElement cell)
    {
      if (cell == null)
        return;
      this.rows.Add(cell.RowInfo);
      this.column = cell.ColumnInfo;
      this.columnStartIndex = this.columnEndIndex = this.column.Index;
    }

    public GridViewSelectionCancelEventArgs(GridViewRowInfo row, GridViewColumn column)
    {
      this.rows.Add(row);
      this.column = column;
      if (this.column == null)
        return;
      this.columnStartIndex = this.columnEndIndex = this.column.Index;
    }

    public GridViewSelectionCancelEventArgs(
      IEnumerable<GridViewRowInfo> rows,
      GridViewColumn column)
    {
      this.rows.AddRange(rows);
      this.column = column;
      if (this.column == null)
        return;
      this.columnStartIndex = this.columnEndIndex = this.column.Index;
    }

    public GridViewSelectionCancelEventArgs(
      IEnumerable<GridViewRowInfo> rows,
      int columnStarIndex,
      int columnEndIndex)
    {
      this.rows.AddRange(rows);
      this.columnStartIndex = columnStarIndex;
      this.columnEndIndex = columnEndIndex;
    }

    public int ColumnStartIndex
    {
      get
      {
        return this.columnStartIndex;
      }
    }

    public int ColumnEndIndex
    {
      get
      {
        return this.columnEndIndex;
      }
    }

    public List<GridViewRowInfo> Rows
    {
      get
      {
        return this.rows;
      }
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public virtual int ColumnIndex
    {
      get
      {
        GridViewDataColumn column = this.column as GridViewDataColumn;
        if (column != null)
          return this.column.OwnerTemplate.Columns.IndexOf(column);
        return -1;
      }
    }
  }
}
