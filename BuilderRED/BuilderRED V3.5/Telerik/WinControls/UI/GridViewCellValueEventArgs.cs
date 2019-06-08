// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellValueEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCellValueEventArgs : EventArgs
  {
    private int columnIndex = -1;
    private int rowIndex = -1;
    private object val;
    private GridViewRowInfo rowInfo;
    private GridViewColumn column;

    public GridViewCellValueEventArgs(int columnIndex, int rowIndex)
    {
      if (columnIndex < -1)
        throw new ArgumentOutOfRangeException(nameof (columnIndex));
      if (rowIndex < -1)
        throw new ArgumentOutOfRangeException(nameof (rowIndex));
      this.columnIndex = columnIndex;
      this.rowIndex = rowIndex;
    }

    internal GridViewCellValueEventArgs(GridViewRowInfo rowInfo, GridViewColumn column)
    {
      this.rowInfo = rowInfo;
      this.column = column;
    }

    public int ColumnIndex
    {
      get
      {
        if (this.columnIndex == -1 && this.column.OwnerTemplate != null)
          return this.column.OwnerTemplate.Columns.IndexOf(this.column as GridViewDataColumn);
        return this.columnIndex;
      }
    }

    public int RowIndex
    {
      get
      {
        if (this.rowInfo != null)
          return this.rowInfo.Index;
        return this.rowIndex;
      }
    }

    public object Value
    {
      get
      {
        return this.val;
      }
      set
      {
        this.val = value;
      }
    }

    public GridViewRowInfo RowInfo
    {
      get
      {
        return this.rowInfo;
      }
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
    }
  }
}
