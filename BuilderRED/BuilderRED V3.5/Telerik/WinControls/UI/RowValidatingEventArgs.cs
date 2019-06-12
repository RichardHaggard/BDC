// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RowValidatingEventArgs : CancelEventArgs
  {
    private GridViewRowInfo row;

    public RowValidatingEventArgs(GridViewRowInfo row)
    {
      this.row = row;
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.row;
      }
    }

    public int RowIndex
    {
      get
      {
        GridViewRowInfo row = this.row;
        if (row != null)
          return row.ViewTemplate.Rows.IndexOf(row);
        return -1;
      }
    }
  }
}
