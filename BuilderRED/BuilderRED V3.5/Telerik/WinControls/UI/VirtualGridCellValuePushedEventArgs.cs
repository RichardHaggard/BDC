// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCellValuePushedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridCellValuePushedEventArgs : VirtualGridCellEventArgs
  {
    private object value;

    public VirtualGridCellValuePushedEventArgs(
      object value,
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo)
      : base(rowIndex, columnIndex, viewInfo)
    {
      this.value = value;
    }

    public object Value
    {
      get
      {
        return this.value;
      }
    }
  }
}
