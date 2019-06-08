// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PrintCellPaintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class PrintCellPaintEventArgs : EventArgs
  {
    private Graphics graphics;
    private GridViewRowInfo rowInfo;
    private GridViewColumn column;
    private Rectangle cellRect;

    public PrintCellPaintEventArgs(
      Graphics graphics,
      GridViewRowInfo row,
      GridViewColumn column,
      Rectangle cellRect)
    {
      this.graphics = graphics;
      this.rowInfo = row;
      this.column = column;
      this.cellRect = cellRect;
    }

    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.rowInfo;
      }
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public Rectangle CellRect
    {
      get
      {
        return this.cellRect;
      }
      set
      {
        this.cellRect = value;
      }
    }
  }
}
