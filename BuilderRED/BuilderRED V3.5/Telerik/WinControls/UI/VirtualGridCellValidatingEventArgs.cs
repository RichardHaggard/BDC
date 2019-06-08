// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCellValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridCellValidatingEventArgs : VirtualGridCellEventArgs
  {
    private bool cancel;
    private object newValue;

    public VirtualGridCellValidatingEventArgs(
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo,
      object newValue)
      : base(rowIndex, columnIndex, viewInfo)
    {
      this.newValue = newValue;
    }

    public bool Cancel
    {
      get
      {
        return this.cancel;
      }
      set
      {
        this.cancel = value;
      }
    }

    public object NewValue
    {
      get
      {
        return this.newValue;
      }
    }
  }
}
