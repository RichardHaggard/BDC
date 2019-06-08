// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCellInfoEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class VirtualGridCellInfoEventArgs : EventArgs
  {
    private VirtualGridCellInfo cellInfo;

    public VirtualGridCellInfoEventArgs(VirtualGridCellInfo cellInfo)
    {
      this.cellInfo = cellInfo;
    }

    public VirtualGridCellInfo CellInfo
    {
      get
      {
        return this.cellInfo;
      }
    }
  }
}
