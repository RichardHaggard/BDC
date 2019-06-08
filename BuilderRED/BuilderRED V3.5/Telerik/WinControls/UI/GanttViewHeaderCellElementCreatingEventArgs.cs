// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewHeaderCellElementCreatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GanttViewHeaderCellElementCreatingEventArgs : EventArgs
  {
    private GanttViewTextViewHeaderCellElement cellElement;

    public GanttViewHeaderCellElementCreatingEventArgs(
      GanttViewTextViewHeaderCellElement cellElement)
    {
      this.cellElement = cellElement;
    }

    public GanttViewTextViewHeaderCellElement CellElement
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
