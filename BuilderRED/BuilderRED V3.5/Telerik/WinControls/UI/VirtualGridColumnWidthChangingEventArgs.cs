// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridColumnWidthChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridColumnWidthChangingEventArgs : VirtualGridColumnEventArgs
  {
    private bool cancel;
    private int oldWidth;
    private int newWidth;

    public VirtualGridColumnWidthChangingEventArgs(
      int columnIndex,
      int oldWidth,
      int newWidth,
      VirtualGridViewInfo viewInfo)
      : base(columnIndex, viewInfo)
    {
      this.newWidth = newWidth;
      this.oldWidth = oldWidth;
    }

    public int OldWidth
    {
      get
      {
        return this.oldWidth;
      }
    }

    public int NewWidth
    {
      get
      {
        return this.newWidth;
      }
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
