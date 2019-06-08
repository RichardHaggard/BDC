// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridCellInfo
  {
    private int rowIndex;
    private int columnIndex;
    private VirtualGridViewInfo viewInfo;

    public VirtualGridCellInfo(int rowIndex, int columnIndex, VirtualGridViewInfo viewInfo)
    {
      this.rowIndex = rowIndex;
      this.columnIndex = columnIndex;
      this.viewInfo = viewInfo;
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public int ColumnIndex
    {
      get
      {
        return this.columnIndex;
      }
    }

    public VirtualGridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }
  }
}
