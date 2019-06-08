// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridDataSnapshot
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Export
{
  internal class GridDataSnapshot
  {
    private string filePath;
    private List<IGridViewSpreadExportRowInfo> exportRowInfos;
    private List<IGridViewSpreadExportRowInfoBase> exportRowInfosBase;

    public GridDataSnapshot(string filePath, List<IGridViewSpreadExportRowInfo> exportRowInfos)
    {
      this.filePath = filePath;
      this.exportRowInfos = exportRowInfos;
    }

    public GridDataSnapshot(
      string filePath,
      List<IGridViewSpreadExportRowInfoBase> exportRowInfosBase)
    {
      this.filePath = filePath;
      this.exportRowInfosBase = exportRowInfosBase;
    }

    public string FilePath
    {
      get
      {
        return this.filePath;
      }
      set
      {
        this.filePath = value;
      }
    }

    internal List<IGridViewSpreadExportRowInfo> ExportRowInfos
    {
      get
      {
        return this.exportRowInfos;
      }
      set
      {
        this.exportRowInfos = value;
      }
    }

    internal List<IGridViewSpreadExportRowInfoBase> ExportRowInfosBase
    {
      get
      {
        return this.exportRowInfosBase;
      }
      set
      {
        this.exportRowInfosBase = value;
      }
    }
  }
}
