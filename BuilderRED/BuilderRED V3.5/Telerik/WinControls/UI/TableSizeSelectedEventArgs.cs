// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TableSizeSelectedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  internal class TableSizeSelectedEventArgs : EventArgs
  {
    private int rows;
    private int columns;

    public TableSizeSelectedEventArgs(int rows, int columns)
    {
      this.rows = rows;
      this.columns = columns;
    }

    public int Rows
    {
      get
      {
        return this.rows;
      }
    }

    public int Columns
    {
      get
      {
        return this.columns;
      }
    }
  }
}
