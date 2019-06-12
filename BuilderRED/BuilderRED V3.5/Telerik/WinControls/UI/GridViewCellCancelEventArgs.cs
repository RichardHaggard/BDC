// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellCancelEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewCellCancelEventArgs : CancelEventArgs
  {
    private GridViewRowInfo row;
    private GridViewColumn column;
    private IInputEditor activeEditor;

    internal GridViewCellCancelEventArgs(GridCellElement cell, IInputEditor editor)
    {
      if (cell != null)
      {
        this.row = cell.RowInfo;
        this.column = cell.ColumnInfo;
      }
      this.activeEditor = editor;
    }

    public GridViewCellCancelEventArgs(
      GridViewRowInfo row,
      GridViewColumn column,
      IInputEditor editor)
    {
      this.row = row;
      this.column = column;
      this.activeEditor = editor;
    }

    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.row;
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

    public virtual int RowIndex
    {
      get
      {
        GridViewRowInfo row = this.row;
        if (row != null)
          return this.row.ViewTemplate.Rows.IndexOf(row);
        return -1;
      }
    }
  }
}
