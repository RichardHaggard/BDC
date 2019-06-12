// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeViewDragCancelEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class RadTreeViewDragCancelEventArgs : RadTreeViewCancelEventArgs
  {
    private ArrowDirection direction = ArrowDirection.Left;
    private RadTreeNode targetNode;
    private DropPosition dropPosition;

    public RadTreeViewDragCancelEventArgs(RadTreeNode node, RadTreeNode targetNode)
      : this(node, targetNode, ArrowDirection.Left, false)
    {
    }

    public RadTreeViewDragCancelEventArgs(
      RadTreeNode node,
      RadTreeNode targetNode,
      ArrowDirection direction,
      bool cancel)
      : base(node, cancel)
    {
      this.targetNode = targetNode;
      this.direction = direction;
    }

    public RadTreeNode TargetNode
    {
      get
      {
        return this.targetNode;
      }
    }

    public DropPosition DropPosition
    {
      get
      {
        return this.dropPosition;
      }
      protected internal set
      {
        this.dropPosition = value;
      }
    }
  }
}
