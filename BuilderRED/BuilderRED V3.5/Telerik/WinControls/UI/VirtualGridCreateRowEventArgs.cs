// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCreateRowEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class VirtualGridCreateRowEventArgs
  {
    private int rowIndex;
    private Type rowType;
    private VirtualGridViewInfo viewInfo;
    private VirtualGridRowElement rowElement;

    public VirtualGridCreateRowEventArgs(int rowIndex, Type rowType, VirtualGridViewInfo viewInfo)
    {
      this.rowIndex = rowIndex;
      this.rowType = rowType;
      this.viewInfo = viewInfo;
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

    public VirtualGridRowElement RowElement
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
  }
}
