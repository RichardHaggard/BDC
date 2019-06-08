// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewRowEventArgs : EventArgs
  {
    private GridViewRowInfo[] rowInfos;

    public GridViewRowEventArgs(GridViewRowInfo[] rowInfo)
    {
      this.rowInfos = rowInfo;
    }

    public GridViewRowEventArgs(GridViewRowInfo rowInfo)
      : this(new GridViewRowInfo[1]{ rowInfo })
    {
    }

    public GridViewRowInfo[] Rows
    {
      get
      {
        return this.rowInfos;
      }
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.rowInfos[0];
      }
    }
  }
}
