// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCreateRowEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCreateRowEventArgs : EventArgs
  {
    private Type rowType;
    private GridViewRowInfo rowInfo;
    private GridRowElement rowElement;

    public GridViewRowInfo RowInfo
    {
      get
      {
        return this.rowInfo;
      }
    }

    public GridRowElement RowElement
    {
      get
      {
        return this.rowElement;
      }
      set
      {
        this.rowElement = value;
      }
    }

    public Type RowType
    {
      get
      {
        return this.rowType;
      }
      set
      {
        this.rowType = value;
      }
    }

    public GridViewCreateRowEventArgs(GridViewRowInfo rowInfo, Type defaultRowType)
    {
      this.rowInfo = rowInfo;
      this.rowType = defaultRowType;
    }
  }
}
