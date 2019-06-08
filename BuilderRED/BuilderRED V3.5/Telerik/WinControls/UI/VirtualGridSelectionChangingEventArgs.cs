// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridSelectionChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridSelectionChangingEventArgs : VirtualGridCellEventArgs
  {
    private VirtualGridSelectionAction selectionAction;
    private bool cancel;

    public VirtualGridSelectionChangingEventArgs(
      VirtualGridSelectionAction selectionAction,
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo)
      : base(rowIndex, columnIndex, viewInfo)
    {
      this.selectionAction = selectionAction;
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

    public VirtualGridSelectionAction SelectionAction
    {
      get
      {
        return this.selectionAction;
      }
    }
  }
}
