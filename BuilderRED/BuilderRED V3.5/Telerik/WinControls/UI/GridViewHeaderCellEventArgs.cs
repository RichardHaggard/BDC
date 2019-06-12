// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewHeaderCellEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class GridViewHeaderCellEventArgs : GridViewCellEventArgsBase
  {
    private ToggleState state;

    public GridViewHeaderCellEventArgs(
      GridViewRowInfo row,
      GridViewColumn column,
      ToggleState state)
      : base(row, column)
    {
      this.state = state;
    }

    public ToggleState State
    {
      get
      {
        return this.state;
      }
    }
  }
}
