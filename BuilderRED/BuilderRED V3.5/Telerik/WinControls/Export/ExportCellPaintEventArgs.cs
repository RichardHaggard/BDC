// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.ExportCellPaintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Export
{
  public class ExportCellPaintEventArgs : ExportEventArgs
  {
    private GridPdfExportCellElement cell;

    public ExportCellPaintEventArgs(
      GridPdfExportCellElement cell,
      IPdfEditor editor,
      RectangleF rectangle)
      : base(editor, rectangle)
    {
      this.cell = cell;
    }

    public GridPdfExportCellElement Cell
    {
      get
      {
        return this.cell;
      }
    }
  }
}
