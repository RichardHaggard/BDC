// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeLinesContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TreeNodeLinesContainer : StackLayoutElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = true;
      this.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.ElementSpacing = 0;
      this.Padding = new Padding(0);
      this.BorderWidth = 0.0f;
      this.ShouldHandleMouseInput = false;
    }

    public TreeNodeElement NodeElement
    {
      get
      {
        return this.FindAncestor<TreeNodeElement>();
      }
    }

    public virtual void Synchronize()
    {
      TreeNodeElement nodeElement = this.NodeElement;
      if (nodeElement == null)
        return;
      if (nodeElement.Data.Parent is RadTreeViewElement.RootTreeNode && !nodeElement.TreeViewElement.ShowRootLines)
      {
        this.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        this.Visibility = ElementVisibility.Visible;
        int level = nodeElement.Data.Level;
        if (!nodeElement.TreeViewElement.FullLazyMode && !nodeElement.Data.HasNodes)
          ++level;
        else if (!nodeElement.TreeViewElement.ShowExpandCollapse)
          ++level;
        while (this.Children.Count > level)
          this.Children.RemoveAt(this.Children.Count - 1);
        while (this.Children.Count < level)
          this.Children.Add((RadElement) new TreeNodeLineElement(nodeElement));
        if (this.Children.Count <= 0)
          return;
        this.UpdateLines();
      }
    }

    protected virtual void UpdateLines()
    {
      TreeNodeElement nodeElement = this.NodeElement;
      bool showLines = nodeElement.TreeViewElement.ShowLines;
      bool showRootLines = nodeElement.TreeViewElement.ShowRootLines;
      if (!showLines)
      {
        foreach (RadElement child in this.Children)
          child.Visibility = ElementVisibility.Hidden;
      }
      else
      {
        foreach (RadElement child in this.Children)
          child.Visibility = ElementVisibility.Visible;
      }
      if (!showRootLines && nodeElement.IsRootNode)
      {
        foreach (RadElement child in this.Children)
          child.Visibility = ElementVisibility.Collapsed;
      }
      if (this.Children.Count > 0 && !nodeElement.TreeViewElement.ShowExpandCollapse && (!this.NodeElement.IsRootNode || !showLines))
        this.Children[this.Children.Count - 1].Visibility = ElementVisibility.Collapsed;
      if (!showLines)
        return;
      int index = this.Children.Count - 1;
      RadTreeNode node = nodeElement.Data;
      if (showLines && showRootLines && (!nodeElement.TreeViewElement.ShowExpandCollapse && this.NodeElement.IsRootNode))
      {
        RadTreeNode nextNode = node.NextNode;
        this.UpdateLine((TreeNodeLineElement) this.Children[0], node, nextNode, nodeElement);
      }
      else
      {
        if ((nodeElement.TreeViewElement.FullLazyMode || node.Nodes.Count > 0) && (!showLines || this.Children.Count <= 0 || (nodeElement.TreeViewElement.ShowExpandCollapse || this.NodeElement.IsRootNode) || (node.Parent == null || !showRootLines)) && (!showLines || showRootLines || (node.parent == null || nodeElement.TreeViewElement.ShowExpandCollapse) || this.Children.Count <= 0))
          node = node.Parent;
        for (; node != null && index >= 0; --index)
        {
          RadTreeNode nextNode = node.NextNode;
          this.UpdateLine((TreeNodeLineElement) this.Children[index], node, nextNode, nodeElement);
          node = node.Parent;
        }
      }
    }

    protected virtual void UpdateLine(
      TreeNodeLineElement lineElement,
      RadTreeNode node,
      RadTreeNode nextNode,
      TreeNodeElement lastNode)
    {
      if (this.Children[0] == lineElement && !lastNode.TreeViewElement.ShowRootLines)
        lineElement.Visibility = ElementVisibility.Collapsed;
      else if (this.Children[this.Children.Count - 1] == lineElement && !lastNode.TreeViewElement.ShowExpandCollapse && (!lastNode.TreeViewElement.ShowRootLines && lastNode.HasChildren) && node.Parent == null)
      {
        lineElement.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        lineElement.Visibility = ElementVisibility.Visible;
        lineElement.ForeColor = lastNode.TreeViewElement.LineColor;
        lineElement.LineStyle = (DashStyle) lastNode.TreeViewElement.LineStyle;
        if (node == lastNode.Data)
        {
          if (node.Nodes.Count > 0)
          {
            if (!lastNode.TreeViewElement.ShowExpandCollapse)
            {
              if (nextNode == null)
                lineElement.Type = TreeNodeLineElement.LinkType.RightBottomAngleShape;
              else
                lineElement.Type = TreeNodeLineElement.LinkType.TShape;
            }
            else if (node.Parent != null && node.Parent.NextNode == null)
              lineElement.Visibility = ElementVisibility.Hidden;
            else
              lineElement.Type = TreeNodeLineElement.LinkType.VerticalLine;
          }
          else if (nextNode == null)
            lineElement.Type = TreeNodeLineElement.LinkType.RightBottomAngleShape;
          else if (node.Parent == null && node.PrevVisibleNode == null)
            lineElement.Type = TreeNodeLineElement.LinkType.RightTopAngleShape;
          else
            lineElement.Type = node.TreeViewElement == null || !node.TreeViewElement.FullLazyMode ? TreeNodeLineElement.LinkType.TShape : TreeNodeLineElement.LinkType.VerticalLine;
        }
        else if (nextNode == null)
          lineElement.Visibility = ElementVisibility.Hidden;
        else if (node.Parent != null && node.Parent.NextNode == null && lineElement == this.Children[0])
          lineElement.Visibility = ElementVisibility.Hidden;
        else
          lineElement.Type = TreeNodeLineElement.LinkType.VerticalLine;
      }
    }
  }
}
