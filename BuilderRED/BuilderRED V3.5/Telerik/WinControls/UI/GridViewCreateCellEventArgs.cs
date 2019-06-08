// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCreateCellEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCreateCellEventArgs : EventArgs
  {
    private Type cellType;
    private GridRowElement row;
    private GridViewColumn column;
    private GridCellElement cellElement;

    public Type CellType
    {
      get
      {
        return this.cellType;
      }
      set
      {
        this.cellType = value;
      }
    }

    public GridRowElement Row
    {
      get
      {
        return this.row;
      }
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public GridCellElement CellElement
    {
      get
      {
        return this.cellElement;
      }
      set
      {
        this.cellElement = value;
      }
    }

    public GridViewCreateCellEventArgs(
      GridRowElement row,
      GridViewColumn column,
      Type defaultCellType)
    {
      this.row = row;
      this.column = column;
      this.cellType = defaultCellType;
    }
  }
}
