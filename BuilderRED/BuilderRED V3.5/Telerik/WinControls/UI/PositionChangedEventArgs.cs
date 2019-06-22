﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PositionChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class PositionChangedEventArgs : EventArgs
  {
    private GridViewRowInfo row;
    private GridViewColumn column;

    public PositionChangedEventArgs(GridViewRowInfo row, GridViewColumn column)
    {
      this.row = row;
      this.column = column;
    }

    public GridViewRowInfo Row
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
  }
}