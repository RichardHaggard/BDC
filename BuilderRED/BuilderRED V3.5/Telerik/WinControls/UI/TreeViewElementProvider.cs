// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class TreeViewElementProvider : VirtualizedPanelElementProvider<RadTreeNode, TreeNodeElement>
  {
    private RadTreeViewElement treeViewElement;

    public TreeViewElementProvider(RadTreeViewElement treeViewElement)
    {
      this.treeViewElement = treeViewElement;
    }

    protected RadTreeViewElement TreeViewElement
    {
      get
      {
        return this.treeViewElement;
      }
    }

    public override IVirtualizedElement<RadTreeNode> CreateElement(
      RadTreeNode data,
      object context)
    {
      CreateTreeNodeElementEventArgs e = new CreateTreeNodeElementEventArgs(data);
      this.treeViewElement.OnCreateNodeElement(e);
      if (e.NodeElement != null)
        return (IVirtualizedElement<RadTreeNode>) e.NodeElement;
      return base.CreateElement(data, context);
    }

    public override SizeF GetElementSize(RadTreeNode item)
    {
      return (SizeF) item.ActualSize;
    }
  }
}
