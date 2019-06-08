// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellPaintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridViewCellPaintEventArgs : EventArgs
  {
    public readonly GridCellElement Cell;
    public readonly Graphics Graphics;

    public GridViewCellPaintEventArgs(GridCellElement cell, Graphics graphics)
    {
      this.Cell = cell;
      this.Graphics = graphics;
    }
  }
}
