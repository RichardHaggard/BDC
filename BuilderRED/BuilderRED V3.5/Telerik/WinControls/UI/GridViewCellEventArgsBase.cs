// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellEventArgsBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCellEventArgsBase : EventArgs
  {
    private GridViewRowInfo row;
    private GridViewColumn column;

    public GridViewCellEventArgsBase(GridViewRowInfo row, GridViewColumn column)
    {
      this.row = row;
      this.column = column;
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

    public override bool Equals(object obj)
    {
      GridViewCellEventArgs viewCellEventArgs = obj as GridViewCellEventArgs;
      if (viewCellEventArgs == null || viewCellEventArgs.Row != this.Row)
        return false;
      return viewCellEventArgs.Column == this.Column;
    }

    public override int GetHashCode()
    {
      int num1 = 0;
      if (this.row != null)
        num1 = this.row.GetHashCode();
      int num2 = 0;
      if (this.column != null)
        num2 = this.column.GetHashCode();
      return base.GetHashCode() ^ num1 ^ num2;
    }
  }
}
