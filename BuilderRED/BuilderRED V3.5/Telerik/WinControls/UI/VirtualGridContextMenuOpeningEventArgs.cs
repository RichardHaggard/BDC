// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridContextMenuOpeningEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridContextMenuOpeningEventArgs : VirtualGridCellEventArgs
  {
    private RadDropDownMenu contextMenu;
    private bool cancel;

    public VirtualGridContextMenuOpeningEventArgs(
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo,
      RadDropDownMenu contextMenu)
      : base(rowIndex, columnIndex, viewInfo)
    {
      this.contextMenu = contextMenu;
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

    public RadDropDownMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        this.contextMenu = value;
      }
    }
  }
}
