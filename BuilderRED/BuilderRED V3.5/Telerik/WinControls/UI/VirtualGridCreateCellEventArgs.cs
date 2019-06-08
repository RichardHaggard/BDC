// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCreateCellEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class VirtualGridCreateCellEventArgs
  {
    private int rowIndex;
    private int columnIndex;
    private Type cellType;
    private VirtualGridViewInfo viewInfo;
    private VirtualGridCellElement cellElement;

    public VirtualGridCreateCellEventArgs(
      int columnIndex,
      int rowIndex,
      Type cellType,
      VirtualGridViewInfo viewInfo)
    {
      this.columnIndex = columnIndex;
      this.rowIndex = rowIndex;
      this.cellType = cellType;
      this.viewInfo = viewInfo;
    }

    public int ColumnIndex
    {
      get
      {
        return this.columnIndex;
      }
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public VirtualGridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }

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

    public VirtualGridCellElement CellElement
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
  }
}
