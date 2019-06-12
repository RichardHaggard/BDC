// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDataCellElementCreatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GanttViewDataCellElementCreatingEventArgs : EventArgs
  {
    private GanttViewTextViewCellElement cellElement;

    public GanttViewDataCellElementCreatingEventArgs(GanttViewTextViewCellElement cellElement)
    {
      this.cellElement = cellElement;
    }

    public GanttViewTextViewCellElement CellElement
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
