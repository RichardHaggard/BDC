// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.SpreadStreamRowEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  public class SpreadStreamRowEventArgs : EventArgs
  {
    private int gridRowIndex;
    private Type gridRowInfoType;
    private GridViewRowInfo gridViewRowInfo;
    private object row;

    public SpreadStreamRowEventArgs(
      int gridRowIndex,
      Type gridRowInfoType,
      GridViewRowInfo gridViewRowInfo,
      object row)
    {
      this.gridRowIndex = gridRowIndex;
      this.gridRowInfoType = gridRowInfoType;
      this.gridViewRowInfo = gridViewRowInfo;
      this.row = row;
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

    public GridViewRowInfo GridRowInfo
    {
      get
      {
        return this.gridViewRowInfo;
      }
    }

    public object Row
    {
      get
      {
        return this.row;
      }
    }
  }
}
