// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.TreeViewSpreadExportDataSnapshot
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Export
{
  internal class TreeViewSpreadExportDataSnapshot
  {
    private string filePath;
    private List<TreeViewSpreadExportRow> exportRows;

    public TreeViewSpreadExportDataSnapshot(
      string filePath,
      List<TreeViewSpreadExportRow> exportRows)
    {
      this.filePath = filePath;
      this.exportRows = exportRows;
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

    internal List<TreeViewSpreadExportRow> ExportRows
    {
      get
      {
        return this.exportRows;
      }
      set
      {
        this.exportRows = value;
      }
    }
  }
}
