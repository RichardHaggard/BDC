// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowHeightChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RowHeightChangingEventArgs : CancelEventArgs
  {
    private GridViewRowInfo row;
    private int newHeight;

    public RowHeightChangingEventArgs(GridViewRowInfo row, int newHeight)
    {
      this.row = row;
      this.newHeight = newHeight;
    }

    public RowHeightChangingEventArgs(GridViewRowInfo row, int newHeight, bool cancel)
      : base(cancel)
    {
      this.row = row;
      this.newHeight = newHeight;
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.row;
      }
    }

    public int NewHeight
    {
      get
      {
        return this.newHeight;
      }
    }
  }
}
