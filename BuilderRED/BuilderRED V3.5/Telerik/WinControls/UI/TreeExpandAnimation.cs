// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeExpandAnimation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public abstract class TreeExpandAnimation
  {
    private RadTreeViewElement treeViewElement;

    public TreeExpandAnimation(RadTreeViewElement treeViewElement)
    {
      this.treeViewElement = treeViewElement;
    }

    public RadTreeViewElement TreeViewElement
    {
      get
      {
        return this.treeViewElement;
      }
    }

    public abstract void Expand(RadTreeNode treeNode);

    public abstract void Collapse(RadTreeNode treeNode);

    protected void UpdateViewOnExpandChanged(RadTreeNode node)
    {
      this.treeViewElement.Update(RadTreeViewElement.UpdateActions.ExpandedChanged, node);
      this.treeViewElement.UpdateLayout();
    }

    protected List<TreeNodeElement> GetAssociatedNodes(RadTreeNode node)
    {
      List<TreeNodeElement> treeNodeElementList = new List<TreeNodeElement>();
      RadElementCollection children = this.TreeViewElement.ViewElement.Children;
      for (int index = 0; index < children.Count; ++index)
      {
        TreeNodeElement treeNodeElement = children[index] as TreeNodeElement;
        if (this.IsHierarchyChild(node, treeNodeElement.Data))
          treeNodeElementList.Add(treeNodeElement);
      }
      return treeNodeElementList;
    }

    private bool IsHierarchyChild(RadTreeNode node, RadTreeNode child)
    {
      for (RadTreeNode parent = child.Parent; parent != null; parent = parent.Parent)
      {
        if (parent == node)
          return true;
      }
      return false;
    }
  }
}
