// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridViewCellEventArgs : GridViewCellEventArgsBase
  {
    private IInputEditor activeEditor;

    public GridViewCellEventArgs(
      GridViewRowInfo row,
      GridViewColumn column,
      IInputEditor activeEditor)
      : base(row, column)
    {
      this.activeEditor = activeEditor;
    }

    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
    }

    public object Value
    {
      get
      {
        if (this.Column is GridViewDataColumn)
          return this.Row[this.Column];
        return (object) null;
      }
    }
  }
}
