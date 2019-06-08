// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class TreeViewDragDropService : RadDragDropService
  {
    private RadTreeViewElement owner;
    private RadLayeredWindow dropHintWindow;
    private Color dropHintColor;
    private bool showDragHint;
    private bool showDropHint;
    private RadTreeNode draggedNode;
    private Image dragHintImageCache;

    public TreeViewDragDropService(RadTreeViewElement owner)
    {
      this.owner = owner;
      this.dropHintColor = Color.Empty;
      this.showDragHint = true;
      this.showDropHint = true;
    }

    public Color DropHintColor
    {
      get
      {
        return this.dropHintColor;
      }
      set
      {
        this.dropHintColor = value;
      }
    }

    public bool ShowDropHint
    {
      get
      {
        return this.showDropHint;
      }
      set
      {
        this.showDropHint = value;
      }
    }

    public bool ShowDragHint
    {
      get
      {
        return this.showDragHint;
      }
      set
      {
        this.showDragHint = value;
      }
    }

    protected virtual bool IsCopyingNodes
    {
      get
      {
        return (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
      }
    }

    protected RadTreeViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    protected override void OnStarted()
    {
      base.OnStarted();
      this.dragHintImageCache = ((ISupportDrag) this.Context).GetDragHint();
    }

    protected override void OnStopped()
    {
      this.dragHintImageCache = (Image) null;
      base.OnStopped();
    }

    protected override void PerformStart()
    {
      base.PerformStart();
      this.draggedNode = (this.Context as TreeNodeElement).Data;
    }

    protected override void SetHintWindowPosition(Point mousePt)
    {
      this.owner.ElementTree.Control.PointToClient(mousePt);
      TreeNodeElement dropTarget = this.DropTarget as TreeNodeElement;
      if (dropTarget != null)
      {
        DropPosition dropPosition = this.GetDropPosition(dropTarget.PointFromScreen(mousePt), dropTarget);
        TreeViewPreviewDragHintEventArgs e = new TreeViewPreviewDragHintEventArgs(this.Context as ISupportDrag, dropPosition);
        this.OnPreviewDragHintWithDropPosition(e);
        Image image1 = (Image) null;
        if (e.UseDefaultHint)
        {
          switch (dropPosition)
          {
            case DropPosition.BeforeNode:
              image1 = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.RadTreeViewDropBefore;
              break;
            case DropPosition.AfterNode:
              image1 = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.RadTreeViewDropAfter;
              break;
            case DropPosition.AsChildNode:
              image1 = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.RadTreeViewDropAsChild;
              break;
          }
          if (image1 != null && this.dragHintImageCache != null && this.ShowDragHint)
          {
            Image image2 = (Image) new Bitmap(image1.Width + this.dragHintImageCache.Width, Math.Max(image1.Height, this.dragHintImageCache.Height));
            using (Graphics graphics = Graphics.FromImage(image2))
            {
              graphics.Clear(Color.White);
              graphics.DrawImage(image1, new RectangleF(0.0f, (float) (image2.Height - image1.Height) / 2f, (float) image1.Width, (float) image1.Height));
              graphics.DrawImage(this.dragHintImageCache, new RectangleF((float) image1.Width, (float) (image2.Height - this.dragHintImageCache.Height) / 2f, (float) this.dragHintImageCache.Width, (float) this.dragHintImageCache.Height));
              graphics.DrawRectangle(Pens.LightGray, new Rectangle(0, 0, image2.Width - 1, image2.Height - 1));
            }
            this.HintWindow.BackgroundImage = image2;
          }
          base.SetHintWindowPosition(new Point(mousePt.X - 20, mousePt.Y));
          return;
        }
        this.HintWindow.BackgroundImage = e.DragHint;
      }
      base.SetHintWindowPosition(new Point(mousePt.X, mousePt.Y));
    }

    protected virtual void OnPreviewDragHintWithDropPosition(TreeViewPreviewDragHintEventArgs e)
    {
      base.OnPreviewDragHint((PreviewDragHintEventArgs) e);
    }

    protected override void OnPreviewDragHint(PreviewDragHintEventArgs e)
    {
      if (!this.ShowDragHint)
      {
        e.DragHint = (Image) null;
        e.UseDefaultHint = false;
      }
      TreeViewPreviewDragHintEventArgs dragHintEventArgs = new TreeViewPreviewDragHintEventArgs(e.DragInstance, DropPosition.None);
      dragHintEventArgs.DragHint = e.DragHint;
      dragHintEventArgs.UseDefaultHint = e.UseDefaultHint;
      base.OnPreviewDragHint((PreviewDragHintEventArgs) dragHintEventArgs);
    }

    protected override void OnPreviewDragDrop(RadDropEventArgs e)
    {
      base.OnPreviewDragDrop(e);
      if (this.CancelPreviewDragDrop(e))
        return;
      bool flag1 = this.owner.Scroller.Traverser.Current == this.draggedNode;
      RadTreeNode radTreeNode1 = (RadTreeNode) null;
      if (flag1)
        radTreeNode1 = this.draggedNode.NextNode ?? this.draggedNode.PrevNode;
      TreeNodeElement hitTarget = e.HitTarget as TreeNodeElement;
      RadTreeViewElement radTreeViewElement = hitTarget == null ? e.HitTarget as RadTreeViewElement : hitTarget.TreeViewElement;
      List<RadTreeNode> selectedNodes = this.GetSelectedNodes(this.draggedNode);
      List<RadTreeNode> draggedNodes = this.GetDraggedNodes(this.draggedNode);
      bool flag2 = radTreeViewElement != null && radTreeViewElement != this.owner;
      radTreeViewElement?.BeginUpdate();
      if (flag2)
        this.owner.BeginUpdate();
      if (hitTarget != null && hitTarget.Data != null)
        this.PerformDragDrop(e.DropLocation, hitTarget, draggedNodes);
      else if (radTreeViewElement != null)
      {
        bool isCopyingNodes = this.IsCopyingNodes;
        foreach (RadTreeNode radTreeNode2 in draggedNodes)
        {
          RadTreeNode radTreeNode3 = radTreeNode2;
          if (this.OnDragEnding(DropPosition.AsChildNode, radTreeNode3, (RadTreeNode) null))
          {
            if (isCopyingNodes)
              radTreeNode3 = this.CreateTreeNode(radTreeNode3);
            else
              radTreeNode3.Remove();
            radTreeViewElement.Nodes.Add(radTreeNode3);
            this.owner.OnDragEnded(new RadTreeViewDragEventArgs(radTreeNode3, (RadTreeNode) null));
          }
        }
      }
      else if (draggedNodes.Count > 0)
        this.owner.OnDragEnded(new RadTreeViewDragEventArgs(draggedNodes[0], (RadTreeNode) null));
      if (flag2)
        this.owner.EndUpdate();
      if (flag1)
        this.owner.Scroller.ScrollToItem(radTreeNode1);
      if (radTreeViewElement == null)
        return;
      radTreeViewElement.EndUpdate();
      radTreeViewElement.SelectedNode = this.draggedNode;
      foreach (RadTreeNode radTreeNode2 in draggedNodes)
        radTreeNode2.Selected = true;
      foreach (RadTreeNode radTreeNode2 in selectedNodes)
        radTreeNode2.Selected = true;
    }

    protected virtual bool CancelPreviewDragDrop(RadDropEventArgs e)
    {
      return e.Handled;
    }

    protected DropPosition GetDropPosition(
      Point dropLocation,
      TreeNodeElement targetNodeElement)
    {
      int num = targetNodeElement.Size.Height / 3;
      int y = dropLocation.Y;
      if (y < num)
        return DropPosition.BeforeNode;
      return y >= num && y <= num * 2 ? DropPosition.AsChildNode : DropPosition.AfterNode;
    }

    protected virtual void PerformDragDrop(
      Point dropLocation,
      TreeNodeElement targetNodeElement,
      List<RadTreeNode> draggedNodes)
    {
      RadTreeNode data = targetNodeElement.Data;
      DropPosition dropPosition = this.GetDropPosition(dropLocation, targetNodeElement);
      if (dropPosition == DropPosition.AsChildNode)
      {
        bool isCopyingNodes = this.IsCopyingNodes;
        int index = 0;
        while (index < draggedNodes.Count)
        {
          if (!this.OnDragEnding(dropPosition, draggedNodes[index], data))
            draggedNodes.RemoveAt(index);
          else
            ++index;
        }
        if (this.owner.bindingProvider.CanDrop || data.treeView != null && data.treeView.bindingProvider.CanDrop)
        {
          this.owner.bindingProvider.DropNodes(data, draggedNodes);
          foreach (RadTreeNode draggedNode in draggedNodes)
            this.owner.OnDragEnded(new RadTreeViewDragEventArgs(draggedNode, data));
        }
        else
        {
          foreach (RadTreeNode draggedNode in draggedNodes)
          {
            RadTreeNode radTreeNode = draggedNode;
            if (isCopyingNodes)
              radTreeNode = this.CreateTreeNode(radTreeNode);
            else
              radTreeNode.Remove();
            data.Nodes.Add(radTreeNode);
            this.owner.OnDragEnded(new RadTreeViewDragEventArgs(radTreeNode, data));
          }
        }
        data.Expand();
      }
      else
        this.PerformDragDropCore(dropPosition, data, draggedNodes);
    }

    protected virtual void PerformDragDropCore(
      DropPosition position,
      RadTreeNode targetNode,
      List<RadTreeNode> draggedNodes)
    {
      RadTreeNode parent = targetNode.Parent;
      RadTreeNodeCollection nodes = targetNode.TreeViewElement.Nodes;
      if (parent != null)
        nodes = parent.Nodes;
      bool flag = position == DropPosition.BeforeNode;
      int index1 = nodes.IndexOf(targetNode);
      if (!flag && index1 + 1 <= nodes.Count)
        ++index1;
      bool isCopyingNodes = this.IsCopyingNodes;
      int index2 = 0;
      while (index2 < draggedNodes.Count)
      {
        if (!this.OnDragEnding(position, draggedNodes[index2], targetNode))
          draggedNodes.RemoveAt(index2);
        else
          ++index2;
      }
      if (this.owner.bindingProvider.CanDrop || targetNode.treeView != null && targetNode.treeView.bindingProvider.CanDrop)
      {
        this.owner.bindingProvider.DropNodes(targetNode.Parent, draggedNodes);
        foreach (RadTreeNode draggedNode in draggedNodes)
          this.owner.OnDragEnded(new RadTreeViewDragEventArgs(draggedNode, targetNode));
      }
      else
      {
        foreach (RadTreeNode draggedNode in draggedNodes)
        {
          RadTreeNode radTreeNode = draggedNode;
          if (radTreeNode.Parent == parent && radTreeNode.TreeViewElement == targetNode.TreeViewElement && nodes.IndexOf(radTreeNode) < index1)
            --index1;
          if (isCopyingNodes)
            radTreeNode = this.CreateTreeNode(radTreeNode);
          else
            radTreeNode.Remove();
          nodes.Insert(index1, radTreeNode);
          ++index1;
          this.owner.OnDragEnded(new RadTreeViewDragEventArgs(radTreeNode, targetNode));
        }
      }
    }

    protected virtual RadTreeNode CreateTreeNode(RadTreeNode sourceTreeNode)
    {
      return sourceTreeNode.Clone() as RadTreeNode;
    }

    private bool OnDragEnding(DropPosition position, RadTreeNode node, RadTreeNode targetNode)
    {
      RadTreeViewDragCancelEventArgs e = new RadTreeViewDragCancelEventArgs(node, targetNode);
      e.DropPosition = position;
      this.owner.OnDragEnding(e);
      return !e.Cancel;
    }

    protected virtual List<RadTreeNode> GetDraggedNodes(RadTreeNode draggedNode)
    {
      SelectedTreeNodeCollection selectedNodes = draggedNode.TreeViewElement.SelectedNodes;
      List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>();
      foreach (RadTreeNode radTreeNode in (ReadOnlyCollection<RadTreeNode>) selectedNodes)
      {
        RadTreeNode parent = radTreeNode.Parent;
        bool flag = true;
        for (; parent != null; parent = parent.Parent)
        {
          if (parent.Selected)
          {
            flag = false;
            break;
          }
        }
        if (flag)
          radTreeNodeList.Add(radTreeNode);
      }
      if (radTreeNodeList.Count == 0)
        radTreeNodeList.Add(draggedNode);
      return radTreeNodeList;
    }

    protected virtual List<RadTreeNode> GetSelectedNodes(RadTreeNode draggedNode)
    {
      SelectedTreeNodeCollection selectedNodes = draggedNode.TreeViewElement.SelectedNodes;
      List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>();
      foreach (RadTreeNode radTreeNode in (ReadOnlyCollection<RadTreeNode>) selectedNodes)
        radTreeNodeList.Add(radTreeNode);
      return radTreeNodeList;
    }

    protected override bool PrepareContext()
    {
      TreeNodeElement context = this.Context as TreeNodeElement;
      if (context != null)
      {
        RadTreeViewDragCancelEventArgs e = new RadTreeViewDragCancelEventArgs(context.Data, (RadTreeNode) null);
        this.owner.OnDragStarting(e);
        if (e.Cancel)
          return false;
      }
      bool flag = base.PrepareContext();
      if (context != null)
        this.owner.OnDragStarted(new RadTreeViewDragEventArgs(context.Data, (RadTreeNode) null));
      return flag;
    }

    protected override bool IsDropTargetValid(ISupportDrop dropTarget)
    {
      TreeNodeElement treeNodeElement = dropTarget as TreeNodeElement;
      if (treeNodeElement != null)
        return treeNodeElement.Data != this.draggedNode;
      return base.IsDropTargetValid(dropTarget);
    }

    protected override bool CanStart(object context)
    {
      if (base.CanStart(context) && this.IsInValidState(this.owner))
        return this.owner.AllowDragDrop;
      return false;
    }

    private bool IsInValidState(RadTreeViewElement tree)
    {
      return !tree.IsEditing;
    }

    protected override void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      TreeNodeElement hitTarget1 = e.HitTarget as TreeNodeElement;
      RadTreeViewElement hitTarget2 = e.HitTarget as RadTreeViewElement;
      if (hitTarget2 != null)
        e.CanDrop = hitTarget2.ElementTree.Control.AllowDrop && hitTarget2.Nodes.Count == 0 && this.IsInValidState(hitTarget2);
      else if (hitTarget1 != null)
      {
        DropPosition dropPosition = this.GetDropPosition(this.DropLocation, hitTarget1);
        e.CanDrop = this.CanDragOver(dropPosition, hitTarget1);
        RadTreeViewDragCancelEventArgs e1 = new RadTreeViewDragCancelEventArgs(this.draggedNode, hitTarget1.Data);
        e1.Cancel = !e.CanDrop;
        e1.DropPosition = dropPosition;
        this.owner.OnDragOverNode(e1);
        e.CanDrop = !e1.Cancel;
      }
      base.OnPreviewDragOver(e);
    }

    protected virtual bool CanDragOver(DropPosition dropPosition, TreeNodeElement targetNodeElement)
    {
      RadTreeViewElement treeViewElement = targetNodeElement.TreeViewElement;
      if (!targetNodeElement.Enabled || !treeViewElement.ElementTree.Control.AllowDrop || !this.IsInValidState(treeViewElement) || !targetNodeElement.Data.AllowDrop && dropPosition == DropPosition.AsChildNode)
        return false;
      RadTreeNode data = targetNodeElement.Data;
      List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>((IEnumerable<RadTreeNode>) this.draggedNode.TreeViewElement.SelectedNodes);
      if (radTreeNodeList.Count == 0)
        radTreeNodeList.Add(this.draggedNode);
      foreach (RadTreeNode radTreeNode in radTreeNodeList)
      {
        if (radTreeNode == data)
          return false;
        for (RadTreeNode parent = data.Parent; parent != null; parent = parent.Parent)
        {
          if (parent == radTreeNode)
            return false;
        }
        if (dropPosition == DropPosition.AsChildNode && radTreeNode.Parent == data)
          return false;
      }
      if (dropPosition == DropPosition.AsChildNode && !data.Expanded && data.HasNodes)
      {
        treeViewElement.AutoExpand(data);
        this.HintWindow.Hide();
        this.HintWindow.Show();
      }
      if (treeViewElement.AutoScrollOnDragging(targetNodeElement))
      {
        this.HintWindow.Hide();
        this.HintWindow.Show();
      }
      return true;
    }

    protected override void HandleMouseMove(Point mousePosition)
    {
      base.HandleMouseMove(mousePosition);
      if (this.Initialized)
      {
        TreeNodeElement context = this.Context as TreeNodeElement;
        if (context != null)
          this.owner.OnItemDrag(new RadTreeViewEventArgs(context.Data));
      }
      TreeNodeElement dropTarget = this.DropTarget as TreeNodeElement;
      if (dropTarget == null || !this.CanShowDropHint(mousePosition) || !this.CanCommit)
      {
        this.DisposeHint();
      }
      else
      {
        if (this.dropHintWindow == null)
          this.PrepareDragHint(dropTarget);
        if (this.dropHintWindow == null)
          return;
        this.UpdateHintPosition(mousePosition);
      }
    }

    protected override void OnStopping(RadServiceStoppingEventArgs e)
    {
      base.OnStopping(e);
      if (e.Commit)
        return;
      this.owner.draggedNode = (RadTreeNode) null;
    }

    protected override void PerformStop()
    {
      base.PerformStop();
      this.DisposeHint();
      this.draggedNode = (RadTreeNode) null;
    }

    protected virtual void DisposeHint()
    {
      if (this.dropHintWindow == null)
        return;
      this.dropHintWindow.Dispose();
      this.dropHintWindow = (RadLayeredWindow) null;
    }

    protected virtual bool CanShowDropHint(Point mousePosition)
    {
      TreeNodeElement dropTarget = this.DropTarget as TreeNodeElement;
      if (dropTarget == null || !this.ShowDropHint)
        return false;
      this.owner.ElementTree.Control.RectangleToScreen(dropTarget.ControlBoundingRectangle);
      Point point = dropTarget.PointFromScreen(mousePosition);
      int num = dropTarget.Size.Height / 3;
      if (point.Y >= num)
        return point.Y > num * 2;
      return true;
    }

    protected virtual void PrepareDragHint(TreeNodeElement nodeElement)
    {
      RadTreeViewElement treeViewElement = nodeElement.TreeViewElement;
      Bitmap bitmap = (Bitmap) null;
      Size size = Size.Empty;
      if (this.dropHintColor != Color.Empty)
      {
        size = new Size(this.GetDropHintWidth(treeViewElement), 1);
        bitmap = new Bitmap(size.Width, size.Height);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          using (SolidBrush solidBrush = new SolidBrush(this.DropHintColor))
            graphics.FillRectangle((Brush) solidBrush, new Rectangle(Point.Empty, size));
        }
      }
      else if (treeViewElement.ItemDropHint != null)
      {
        size = new Size(this.GetDropHintWidth(treeViewElement), treeViewElement.ItemDropHint.Image.Size.Height);
        bitmap = new Bitmap(size.Width, size.Height);
        using (Graphics g = Graphics.FromImage((Image) bitmap))
          treeViewElement.ItemDropHint.Paint(g, new RectangleF(PointF.Empty, (SizeF) size));
      }
      if (bitmap == null)
        return;
      this.dropHintWindow = new RadLayeredWindow();
      this.dropHintWindow.BackgroundImage = (Image) bitmap;
    }

    private int GetDropHintWidth(RadTreeViewElement treeView)
    {
      int num = treeView.ControlBoundingRectangle.Width - LightVisualElement.GetBorderThickness((LightVisualElement) treeView, true).Horizontal;
      if (treeView.VScrollBar.Visibility == ElementVisibility.Visible)
        num -= treeView.VScrollBar.Size.Width;
      return num;
    }

    protected virtual void UpdateHintPosition(Point mousePosition)
    {
      TreeNodeElement dropTarget = this.DropTarget as TreeNodeElement;
      Rectangle screen = dropTarget.ElementTree.Control.RectangleToScreen(dropTarget.ControlBoundingRectangle);
      Padding padding = Padding.Empty;
      int num1 = 1;
      if (this.DropHintColor == Color.Empty)
      {
        RadImageShape itemDropHint = this.owner.ItemDropHint;
        num1 = itemDropHint.Image.Size.Height;
        padding = itemDropHint.Margins;
      }
      int num2 = dropTarget.PointFromScreen(mousePosition).Y <= dropTarget.Size.Height / 2 ? screen.Y : screen.Bottom;
      Point screenLocation = new Point(screen.X - padding.Left, num2 - num1 / 2);
      this.dropHintWindow.TopMost = true;
      this.dropHintWindow.ShowWindow(screenLocation);
    }
  }
}
