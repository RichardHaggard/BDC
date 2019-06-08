// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RowFormattingEventArgs : EventArgs
  {
    private GridRowElement row;

    public RowFormattingEventArgs(GridRowElement row)
    {
      this.row = row;
    }

    public GridRowElement RowElement
    {
      get
      {
        return this.row;
      }
    }
  }
}
