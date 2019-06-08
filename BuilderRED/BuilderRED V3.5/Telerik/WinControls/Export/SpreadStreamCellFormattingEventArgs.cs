// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.SpreadStreamCellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Export
{
  public class SpreadStreamCellFormattingEventArgs : EventArgs
  {
    private int excelColumnIndex;
    private int excelRowIndex;
    private IGridViewSpreadStreamExportCellInfo exportCell;
    private object cell;

    public SpreadStreamCellFormattingEventArgs(
      int excelRowIndex,
      int excelColumnIndex,
      IGridViewSpreadStreamExportCellInfo exportCell,
      object cell)
    {
      this.excelRowIndex = excelRowIndex;
      this.excelColumnIndex = excelColumnIndex;
      this.exportCell = exportCell;
      this.cell = cell;
    }

    public int ExcelColumnIndex
    {
      get
      {
        return this.excelColumnIndex;
      }
    }

    public int ExcelRowIndex
    {
      get
      {
        return this.excelRowIndex;
      }
    }

    public Type GridRowInfoType
    {
      get
      {
        return this.exportCell.RowType;
      }
    }

    public IGridViewSpreadStreamExportCellInfo ExportCell
    {
      get
      {
        return this.exportCell;
      }
    }

    public ISpreadStreamExportCellStyleInfo CellStyleInfo
    {
      get
      {
        return this.exportCell.CellStyleInfo;
      }
    }

    public object Cell
    {
      get
      {
        return this.cell;
      }
    }
  }
}
