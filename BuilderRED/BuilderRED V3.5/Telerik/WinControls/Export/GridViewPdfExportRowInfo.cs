// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridViewPdfExportRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.Export
{
  internal class GridViewPdfExportRowInfo
  {
    public GridViewPdfExportRowInfo(Type type)
    {
      this.Cells = new List<GridPdfAsyncExportCellInfo>();
      this.Type = type;
    }

    public Type Type { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public double Indent { get; set; }

    public List<GridPdfAsyncExportCellInfo> Cells { get; set; }
  }
}
