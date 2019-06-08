// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridRowValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridRowValidatingEventArgs : VirtualGridRowEventArgs
  {
    private bool cancel;

    public VirtualGridRowValidatingEventArgs(int rowIndex, VirtualGridViewInfo viewInfo)
      : base(rowIndex, viewInfo)
    {
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
  }
}
