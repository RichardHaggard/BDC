// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SelectedTreeNodeCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  public class SelectedTreeNodeCollection : ReadOnlyCollection<RadTreeNode>
  {
    private RadTreeViewElement treeView;
    private bool performClear;

    public SelectedTreeNodeCollection(RadTreeViewElement treeView)
      : base((IList<RadTreeNode>) new List<RadTreeNode>())
    {
      this.treeView = treeView;
    }

    internal void ProcessSelectedNode(RadTreeNode radTreeNode)
    {
      if (radTreeNode.Selected)
        this.Items.Add(radTreeNode);
      else
        this.Items.Remove(radTreeNode);
      this.treeView.Update(RadTreeViewElement.UpdateActions.StateChanged, radTreeNode);
      this.treeView.OnSelectedNodesChanged(radTreeNode);
    }

    public void Clear()
    {
      if (this.performClear || this.Items.Count == 0)
        return;
      this.performClear = true;
      bool flag = true;
      this.treeView.BeginUpdate();
      for (int index = this.Items.Count - 1; index >= 0; --index)
      {
        RadTreeNode node = this.Items[index];
        RadTreeViewCancelEventArgs args = new RadTreeViewCancelEventArgs(node, RadTreeViewAction.Unknown);
        if (this.treeView.MultiSelect)
        {
          this.treeView.OnSelectedNodeChanging(args);
          if (args.Cancel)
          {
            flag = false;
            continue;
          }
        }
        node.Selected = false;
        this.Items.Remove(node);
        if (this.treeView.MultiSelect)
          this.treeView.OnSelectedNodeChanged(new RadTreeViewEventArgs(node));
      }
      this.treeView.EndUpdate(true, RadTreeViewElement.UpdateActions.StateChanged);
      if (flag)
        this.treeView.OnSelectedNodesCleared();
      this.performClear = false;
    }
  }
}
