// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridElementToolTipTextNeededEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridElementToolTipTextNeededEventArgs : ToolTipTextNeededEventArgs
  {
    private int rowIndex;
    private GridViewRowInfo row;

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.row;
      }
    }

    public GridElementToolTipTextNeededEventArgs(
      ToolTip toolTip,
      int rowIndex,
      GridViewRowInfo row,
      string tooltipText)
      : base(toolTip, tooltipText)
    {
      this.rowIndex = rowIndex;
      this.row = row;
    }
  }
}
