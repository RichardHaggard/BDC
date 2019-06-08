// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewSpreadExportCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  internal class GridViewSpreadExportCellInfo : IGridViewSpreadExportCellInfo
  {
    private object value;
    private string exportFormat;
    private DisplayFormatType exportFormatType;

    public GridViewSpreadExportCellInfo(
      object value,
      string exportFormat,
      DisplayFormatType exportFormatType)
    {
      this.value = value;
      this.exportFormat = exportFormat;
      this.exportFormatType = exportFormatType;
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public string ExportFormat
    {
      get
      {
        return this.exportFormat;
      }
      set
      {
        this.exportFormat = value;
      }
    }

    public DisplayFormatType ExportFormatType
    {
      get
      {
        return this.exportFormatType;
      }
      set
      {
        this.exportFormatType = value;
      }
    }
  }
}
