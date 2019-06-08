// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.IGridViewSpreadStreamExportCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Export
{
  public interface IGridViewSpreadStreamExportCellInfo : IGridViewSpreadExportCellInfo
  {
    Type RowType { get; set; }

    int RowIndex { get; set; }

    Type ColumnType { get; set; }

    int ColumnIndex { get; set; }

    ISpreadStreamExportCellStyleInfo CellStyleInfo { get; set; }
  }
}
