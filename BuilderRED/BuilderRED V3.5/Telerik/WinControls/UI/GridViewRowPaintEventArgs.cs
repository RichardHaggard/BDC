// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowPaintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridViewRowPaintEventArgs : EventArgs
  {
    public readonly GridRowElement Row;
    public readonly Graphics Graphics;

    public GridViewRowPaintEventArgs(GridRowElement row, Graphics graphics)
    {
      this.Row = row;
      this.Graphics = graphics;
    }
  }
}
