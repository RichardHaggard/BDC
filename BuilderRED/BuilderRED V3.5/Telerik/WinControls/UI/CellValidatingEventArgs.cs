// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CellValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class CellValidatingEventArgs : GridViewCellCancelEventArgs
  {
    private object value;
    private object oldValue;

    public CellValidatingEventArgs(
      GridViewRowInfo row,
      GridViewColumn column,
      object value,
      object oldValue,
      IInputEditor editor)
      : base(row, column, editor)
    {
      this.value = value;
      this.oldValue = oldValue;
    }

    public object Value
    {
      get
      {
        return this.value;
      }
    }

    public object OldValue
    {
      get
      {
        return this.oldValue;
      }
    }
  }
}
