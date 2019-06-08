﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.PropertyGridSpreadExportCellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  public class PropertyGridSpreadExportCellFormattingEventArgs : EventArgs
  {
    private int rowIndex;
    private PropertyGridSpreadExportCell exportCell;
    private PropertyGridItemBase item;

    public PropertyGridSpreadExportCellFormattingEventArgs(
      PropertyGridSpreadExportCell exportCell,
      PropertyGridItemBase item,
      int rowIndex)
    {
      this.exportCell = exportCell;
      this.item = item;
      this.rowIndex = rowIndex;
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public PropertyGridSpreadExportCell ExportCell
    {
      get
      {
        return this.exportCell;
      }
    }

    public PropertyGridItemBase Item
    {
      get
      {
        return this.item;
      }
    }
  }
}
