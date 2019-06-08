// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CurrentCellChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CurrentCellChangedEventArgs : EventArgs
  {
    private GridViewColumn oldColumn;
    private GridViewRowInfo oldRow;
    private GridViewColumn newColumn;
    private GridViewRowInfo newRow;

    public GridViewCellInfo CurrentCell
    {
      get
      {
        if (this.oldRow == null || this.oldColumn == null)
          return (GridViewCellInfo) null;
        return this.oldRow.Cells[this.oldColumn.Name];
      }
    }

    public GridViewCellInfo NewCell
    {
      get
      {
        if (this.newRow == null || this.newColumn == null)
          return (GridViewCellInfo) null;
        return this.newRow.Cells[this.newColumn.Name];
      }
    }

    public CurrentCellChangedEventArgs(
      GridViewRowInfo oldRow,
      GridViewColumn oldColumn,
      GridViewRowInfo newRow,
      GridViewColumn newColumn)
    {
      this.oldRow = oldRow;
      this.oldColumn = oldColumn;
      this.newRow = newRow;
      this.newColumn = newColumn;
    }
  }
}
