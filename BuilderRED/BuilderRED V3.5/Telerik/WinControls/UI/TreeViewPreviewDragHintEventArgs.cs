// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewPreviewDragHintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class TreeViewPreviewDragHintEventArgs : PreviewDragHintEventArgs
  {
    private DropPosition dropPosition;

    public TreeViewPreviewDragHintEventArgs(ISupportDrag dragInstance, DropPosition dropPosition)
      : base(dragInstance)
    {
      this.dropPosition = dropPosition;
    }

    public DropPosition DropPosition
    {
      get
      {
        return this.dropPosition;
      }
      set
      {
        this.dropPosition = value;
      }
    }
  }
}
