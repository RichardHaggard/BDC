// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridColumnEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridColumnEventArgs : VirtualGridEventArgs
  {
    private int columnIndex;

    public VirtualGridColumnEventArgs(int columnIndex, VirtualGridViewInfo viewInfo)
      : base(viewInfo)
    {
      this.columnIndex = columnIndex;
    }

    public int ColumnIndex
    {
      get
      {
        return this.columnIndex;
      }
    }
  }
}
