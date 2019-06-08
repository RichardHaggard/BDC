// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.CellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  public class CellFormattingEventArgs : EventArgs
  {
    private int gridColumnIndex;
    private int gridRowIndex;
    private Type gridRowInfoType;
    private GridViewCellInfo gridViewCellInfo;
    private object cellSelection;
    private ISpreadExportCellStyleInfo cellStyleInfo;

    public CellFormattingEventArgs(
      int gridRowIndex,
      int gridColumnIndex,
      Type gridRowInfoType,
      GridViewCellInfo gridViewCellInfo,
      object cellSelection,
      ISpreadExportCellStyleInfo cellStyleInfo)
    {
      this.gridRowIndex = gridRowIndex;
      this.gridColumnIndex = gridColumnIndex;
      this.gridRowInfoType = gridRowInfoType;
      this.gridViewCellInfo = gridViewCellInfo;
      this.cellSelection = cellSelection;
      this.cellStyleInfo = cellStyleInfo;
    }

    public int GridColumnIndex
    {
      get
      {
        return this.gridColumnIndex;
      }
    }

    public int GridRowIndex
    {
      get
      {
        return this.gridRowIndex;
      }
    }

    public Type GridRowInfoType
    {
      get
      {
        return this.gridRowInfoType;
      }
    }

    public GridViewCellInfo GridCellInfo
    {
      get
      {
        return this.gridViewCellInfo;
      }
    }

    public object CellSelection
    {
      get
      {
        return this.cellSelection;
      }
    }

    public ISpreadExportCellStyleInfo CellStyleInfo
    {
      get
      {
        return this.cellStyleInfo;
      }
    }
  }
}
