﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.IGridViewSpreadExportCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  public interface IGridViewSpreadExportCellInfo
  {
    object Value { get; set; }

    string ExportFormat { get; set; }

    DisplayFormatType ExportFormatType { get; set; }
  }
}
