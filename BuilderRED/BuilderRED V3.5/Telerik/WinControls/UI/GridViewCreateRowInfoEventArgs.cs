// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCreateRowInfoEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCreateRowInfoEventArgs : EventArgs
  {
    private GridViewRowInfo rowInfo;
    private GridViewInfo viewInfo;

    public GridViewCreateRowInfoEventArgs(GridViewRowInfo row, GridViewInfo view)
    {
      this.rowInfo = row;
      this.viewInfo = view;
    }

    public GridViewRowInfo RowInfo
    {
      get
      {
        return this.rowInfo;
      }
      set
      {
        if (this.rowInfo == value)
          return;
        this.rowInfo = value;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }
  }
}
