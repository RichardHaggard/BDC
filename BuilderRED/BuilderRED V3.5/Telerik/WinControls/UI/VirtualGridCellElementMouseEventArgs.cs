// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCellElementMouseEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VirtualGridCellElementMouseEventArgs : VirtualGridCellElementEventArgs
  {
    private MouseEventArgs mouseEventArgs;

    public VirtualGridCellElementMouseEventArgs(
      VirtualGridCellElement cellElement,
      VirtualGridViewInfo viewInfo,
      MouseEventArgs mouseEventArgs)
      : base(cellElement, viewInfo)
    {
      this.mouseEventArgs = mouseEventArgs;
    }

    public MouseEventArgs MouseEventArgs
    {
      get
      {
        return this.mouseEventArgs;
      }
    }
  }
}
