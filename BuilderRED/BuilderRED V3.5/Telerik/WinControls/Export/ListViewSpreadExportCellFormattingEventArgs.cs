// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.ListViewSpreadExportCellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  public class ListViewSpreadExportCellFormattingEventArgs : EventArgs
  {
    private int rowIndex;
    private ListViewSpreadExportCell exportCell;
    private ListViewDataItem dataItem;

    public ListViewSpreadExportCellFormattingEventArgs(
      ListViewSpreadExportCell exportCell,
      ListViewDataItem dataItem,
      int rowIndex)
    {
      this.exportCell = exportCell;
      this.dataItem = dataItem;
      this.rowIndex = rowIndex;
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public ListViewSpreadExportCell ExportCell
    {
      get
      {
        return this.exportCell;
      }
    }

    public ListViewDataItem DataItem
    {
      get
      {
        return this.dataItem;
      }
    }
  }
}
