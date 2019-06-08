// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewCellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ListViewCellFormattingEventArgs : EventArgs
  {
    private DetailListViewCellElement cellElement;

    public ListViewCellFormattingEventArgs(DetailListViewCellElement cellElement)
    {
      this.cellElement = cellElement;
    }

    public DetailListViewCellElement CellElement
    {
      get
      {
        return this.cellElement;
      }
    }
  }
}
