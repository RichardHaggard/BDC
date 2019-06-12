// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewPdfExportDataSnapshot
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Export
{
  internal class GridViewPdfExportDataSnapshot
  {
    private string filePath;
    private List<GridViewPdfExportRowInfo> exportRowInfos;

    public GridViewPdfExportDataSnapshot(
      string filePath,
      List<GridViewPdfExportRowInfo> exportRowInfos)
    {
      this.filePath = filePath;
      this.exportRowInfos = exportRowInfos;
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

    internal List<GridViewPdfExportRowInfo> ExportRowInfos
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
  }
}
