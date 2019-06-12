// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewSpreadStreamExportGroupRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Export
{
  public class GridViewSpreadStreamExportGroupRowInfo : GridViewSpreadStreamExportRowInfo
  {
    public GridViewSpreadStreamExportGroupRowInfo(
      IList<IGridViewSpreadStreamExportCellInfo> cellInfos,
      int indent,
      bool exportAsHidden,
      int hierarchyLevel,
      double height,
      int columnSpan)
      : base(cellInfos, indent, exportAsHidden, hierarchyLevel, height)
    {
      this.ColumnSpan = columnSpan;
    }

    public int ColumnSpan { get; set; }
  }
}
