// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewSpreadStreamExportRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Export
{
  public class GridViewSpreadStreamExportRowInfo : IGridViewSpreadExportRowInfoBase
  {
    public GridViewSpreadStreamExportRowInfo(
      IList<IGridViewSpreadStreamExportCellInfo> cellInfos,
      int indent,
      bool exportAsHidden,
      int hierarchyLevel,
      double height)
    {
      this.CellInfos = cellInfos;
      this.IndentCells = indent;
      this.ExportAsHidden = exportAsHidden;
      this.HierarchyLevel = hierarchyLevel;
      this.Height = height;
    }

    public IList<IGridViewSpreadStreamExportCellInfo> CellInfos { get; set; }

    public int IndentCells { get; set; }

    public bool ExportAsHidden { get; set; }

    public int HierarchyLevel { get; set; }

    public double Height { get; set; }
  }
}
