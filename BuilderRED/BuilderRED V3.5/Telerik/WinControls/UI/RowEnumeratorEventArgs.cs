// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowEnumeratorEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RowEnumeratorEventArgs : EventArgs
  {
    private GridViewRowInfo row;
    private bool processRow;

    public RowEnumeratorEventArgs(GridViewRowInfo row)
    {
      this.row = row;
      this.processRow = true;
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.row;
      }
    }

    public bool ProcessRow
    {
      get
      {
        return this.processRow;
      }
      set
      {
        this.processRow = value;
      }
    }
  }
}
