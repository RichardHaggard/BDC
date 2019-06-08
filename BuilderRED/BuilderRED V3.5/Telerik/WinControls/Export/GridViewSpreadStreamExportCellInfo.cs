// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewSpreadStreamExportCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  public class GridViewSpreadStreamExportCellInfo : IGridViewSpreadStreamExportCellInfo, IGridViewSpreadExportCellInfo
  {
    public GridViewSpreadStreamExportCellInfo(
      Type rowType,
      int rowIndex,
      Type ColumnType,
      int columnIndex,
      object value,
      string exportFormat,
      DisplayFormatType exportFormatType,
      ISpreadStreamExportCellStyleInfo cellStyleInfo)
    {
      this.RowType = rowType;
      this.RowIndex = rowIndex;
      this.ColumnType = ColumnType;
      this.ColumnIndex = columnIndex;
      this.Value = value;
      this.ExportFormat = exportFormat;
      this.ExportFormatType = exportFormatType;
      this.CellStyleInfo = cellStyleInfo;
    }

    public Type RowType { get; set; }

    public int RowIndex { get; set; }

    public Type ColumnType { get; set; }

    public int ColumnIndex { get; set; }

    public object Value { get; set; }

    public string ExportFormat { get; set; }

    public DisplayFormatType ExportFormatType { get; set; }

    public ISpreadStreamExportCellStyleInfo CellStyleInfo { get; set; }
  }
}
