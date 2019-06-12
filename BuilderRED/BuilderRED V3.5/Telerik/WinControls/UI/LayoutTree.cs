// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutTree
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class LayoutTree
  {
    private LayoutControlContainerElement owner;
    private LayoutTreeNode root;

    public LayoutControlContainerElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public LayoutTreeNode Root
    {
      get
      {
        return this.root;
      }
    }

    public LayoutTree(LayoutControlContainerElement owner)
    {
      this.owner = owner;
    }

    public void Rebuild(List<LayoutControlItemBase> items, Rectangle bounds)
    {
      if (items.Count == 0)
        return;
      this.root = new LayoutTreeNode();
      this.root.Bounds = (RectangleF) bounds;
      if (!this.TrySplit(this.root, items, true) && !this.TrySplit(this.root, items, false))
        this.FixInvalidLayout(this.root, items);
      this.UpdateItemsBounds();
      this.ResetOriginalBounds(this.root);
    }

    public void ResetOriginalBounds()
    {
      this.ResetOriginalBounds(this.root);
    }

    public void ResetOriginalBounds(LayoutTreeNode node)
    {
      if (node == null)
        return;
      node.OriginalBounds = node.Bounds;
      node.OriginalSplitPosition = node.SplitPosition;
      this.ResetOriginalBounds(node.Left);
      this.ResetOriginalBounds(node.Right);
    }

    private bool TrySplit(LayoutTreeNode node, List<LayoutControlItemBase> items, bool vertical)
    {
      if (items.Count == 1)
      {
        node.Item = items[0];
        return true;
      }
      List<LayoutTree.BoundsEndpoint> boundsEndpointList = new List<LayoutTree.BoundsEndpoint>();
      foreach (LayoutControlItemBase layoutControlItemBase in items)
      {
        boundsEndpointList.Add(new LayoutTree.BoundsEndpoint(vertical ? layoutControlItemBase.Bounds.Top : layoutControlItemBase.Bounds.Left, false, layoutControlItemBase));
        boundsEndpointList.Add(new LayoutTree.BoundsEndpoint(vertical ? layoutControlItemBase.Bounds.Bottom : layoutControlItemBase.Bounds.Right, true, layoutControlItemBase));
      }
      boundsEndpointList.Sort(new Comparison<LayoutTree.BoundsEndpoint>(this.CompareEndpoints));
      List<LayoutControlItemBase> layoutControlItemBaseList = new List<LayoutControlItemBase>();
      Dictionary<LayoutControlItemBase, object> dictionary = new Dictionary<LayoutControlItemBase, object>();
      int num = -1;
      for (int index = 0; index < boundsEndpointList.Count; ++index)
      {
        if (!boundsEndpointList[index].IsEnd)
        {
          dictionary.Add(boundsEndpointList[index].Item, (object) null);
        }
        else
        {
          dictionary.Remove(boundsEndpointList[index].Item);
          layoutControlItemBaseList.Add(boundsEndpointList[index].Item);
          if (dictionary.Count == 0 && layoutControlItemBaseList.Count < items.Count && (num == -1 || (double) Math.Abs((float) num - (float) items.Count / 2f) > (double) Math.Abs((float) layoutControlItemBaseList.Count - (float) items.Count / 2f)))
            num = layoutControlItemBaseList.Count;
        }
      }
      if (num == -1)
        return false;
      List<LayoutControlItemBase> range1 = layoutControlItemBaseList.GetRange(0, num);
      List<LayoutControlItemBase> range2 = layoutControlItemBaseList.GetRange(num, layoutControlItemBaseList.Count - num);
      node.SplitType = vertical ? Orientation.Vertical : Orientation.Horizontal;
      node.SplitPosition = vertical ? (float) range2[0].Bounds.Top : (float) range2[0].Bounds.Left;
      LayoutTreeNode node1 = new LayoutTreeNode();
      node1.Parent = node;
      node1.Bounds = this.CalculateSplitBounds(node, true);
      LayoutTreeNode node2 = new LayoutTreeNode();
      node2.Parent = node;
      node2.Bounds = this.CalculateSplitBounds(node, false);
      node.Left = node1;
      node.Right = node2;
      if (!this.TrySplit(node1, range1, true) && !this.TrySplit(node1, range1, false))
        this.FixInvalidLayout(node1, range1);
      if (!this.TrySplit(node2, range2, true) && !this.TrySplit(node2, range2, false))
        this.FixInvalidLayout(node2, range2);
      return true;
    }

    private void FixInvalidLayout(LayoutTreeNode node, List<LayoutControlItemBase> items)
    {
      List<LayoutTree.BoundsEndpoint> boundsEndpointList = new List<LayoutTree.BoundsEndpoint>();
      foreach (LayoutControlItemBase layoutControlItemBase in items)
      {
        boundsEndpointList.Add(new LayoutTree.BoundsEndpoint(layoutControlItemBase.Bounds.Left, false, layoutControlItemBase));
        boundsEndpointList.Add(new LayoutTree.BoundsEndpoint(layoutControlItemBase.Bounds.Right, true, layoutControlItemBase));
      }
      boundsEndpointList.Sort(new Comparison<LayoutTree.BoundsEndpoint>(this.CompareEndpoints));
      List<LayoutControlItemBase> layoutControlItemBaseList1 = new List<LayoutControlItemBase>();
      Dictionary<LayoutControlItemBase, object> dictionary = new Dictionary<LayoutControlItemBase, object>();
      int num1 = -1;
      int num2 = int.MaxValue;
      int x = 0;
      for (int index = 0; index < boundsEndpointList.Count; ++index)
      {
        if (!boundsEndpointList[index].IsEnd)
        {
          dictionary.Add(boundsEndpointList[index].Item, (object) null);
        }
        else
        {
          dictionary.Remove(boundsEndpointList[index].Item);
          layoutControlItemBaseList1.Add(boundsEndpointList[index].Item);
          if (dictionary.Count <= num2 && x != boundsEndpointList[index].Location && layoutControlItemBaseList1.Count < items.Count && (dictionary.Count < num2 || (double) Math.Abs((float) num1 - (float) items.Count / 2f) > (double) Math.Abs((float) layoutControlItemBaseList1.Count - (float) items.Count / 2f)))
          {
            num1 = layoutControlItemBaseList1.Count;
            num2 = dictionary.Count;
            x = boundsEndpointList[index].Location;
          }
        }
      }
      if (num1 == -1)
        throw new InvalidOperationException("Items' layout is invalid");
      if ((double) x == (double) node.Bounds.Right)
        x = Math.Max(x - 30, (int) (((double) node.Bounds.Left + (double) node.Bounds.Right) / 2.0));
      List<LayoutControlItemBase> range1 = layoutControlItemBaseList1.GetRange(0, num1);
      List<LayoutControlItemBase> range2 = layoutControlItemBaseList1.GetRange(num1, layoutControlItemBaseList1.Count - num1);
      List<LayoutControlItemBase> layoutControlItemBaseList2 = new List<LayoutControlItemBase>();
      foreach (LayoutControlItemBase layoutControlItemBase in range2)
      {
        if (layoutControlItemBase.Bounds.X < x)
          layoutControlItemBaseList2.Add(layoutControlItemBase);
      }
      foreach (LayoutControlItemBase layoutControlItemBase in layoutControlItemBaseList2)
      {
        if (Math.Abs(layoutControlItemBase.Bounds.X - x) > Math.Abs(layoutControlItemBase.Bounds.Right - x) && range2.Count > 1)
        {
          layoutControlItemBase.Bounds = new Rectangle(layoutControlItemBase.Bounds.Location, new Size(Math.Max(30, x - layoutControlItemBase.Bounds.X), layoutControlItemBase.Bounds.Height));
          range1.Add(layoutControlItemBase);
          range2.Remove(layoutControlItemBase);
        }
        else
          layoutControlItemBase.Bounds = new Rectangle(new Point(x, layoutControlItemBase.Bounds.Y), new Size(Math.Max(30, layoutControlItemBase.Bounds.Right - x), layoutControlItemBase.Bounds.Height));
      }
      node.SplitType = Orientation.Horizontal;
      node.SplitPosition = (float) range2[0].Bounds.Left;
      LayoutTreeNode node1 = new LayoutTreeNode();
      node1.Parent = node;
      node1.Bounds = this.CalculateSplitBounds(node, true);
      LayoutTreeNode node2 = new LayoutTreeNode();
      node2.Parent = node;
      node2.Bounds = this.CalculateSplitBounds(node, false);
      node.Left = node1;
      node.Right = node2;
      if (!this.TrySplit(node1, range1, true) && !this.TrySplit(node1, range1, false))
        this.FixInvalidLayout(node1, range1);
      if (this.TrySplit(node2, range2, true) || this.TrySplit(node2, range2, false))
        return;
      this.FixInvalidLayout(node2, range2);
    }

    private RectangleF CalculateSplitBounds(LayoutTreeNode node, bool left)
    {
      return this.CalculateSplitBounds(node, node.Bounds, node.SplitType, node.SplitPosition, left);
    }

    private RectangleF CalculateSplitBounds(
      LayoutTreeNode node,
      RectangleF bounds,
      Orientation splitType,
      float splitPosition,
      bool left)
    {
      if (splitType == Orientation.Horizontal)
      {
        if (node.Right != null && (double) node.Right.MaxSize.Width != 0.0)
          splitPosition = Math.Max(splitPosition, bounds.Right - node.Right.MaxSize.Width);
        if (node.Right != null && (double) node.Right.MinSize.Width != 0.0)
          splitPosition = Math.Min(splitPosition, bounds.Right - node.Right.MinSize.Width);
        if (node.Left != null && (double) node.Left.MaxSize.Width != 0.0)
          splitPosition = Math.Min(splitPosition, bounds.Left + node.Left.MaxSize.Width);
        if (node.Left != null && (double) node.Left.MinSize.Width != 0.0)
          splitPosition = Math.Max(splitPosition, bounds.Left + node.Left.MinSize.Width);
        if (left)
          return new RectangleF(bounds.X, bounds.Y, splitPosition - bounds.X, bounds.Height);
        return new RectangleF(splitPosition, bounds.Y, bounds.Right - splitPosition, bounds.Height);
      }
      if (node.Right != null && (double) node.Right.MaxSize.Height != 0.0)
        splitPosition = Math.Max(splitPosition, bounds.Bottom - node.Right.MaxSize.Height);
      if (node.Right != null && (double) node.Right.MinSize.Height != 0.0)
        splitPosition = Math.Min(splitPosition, bounds.Bottom - node.Right.MinSize.Height);
      if (node.Left != null && (double) node.Left.MaxSize.Height != 0.0)
        splitPosition = Math.Min(splitPosition, bounds.Top + node.Left.MaxSize.Height);
      if (node.Left != null && (double) node.Left.MinSize.Height != 0.0)
        splitPosition = Math.Max(splitPosition, bounds.Top + node.Left.MinSize.Height);
      if (left)
        return new RectangleF(bounds.X, bounds.Y, bounds.Width, splitPosition - bounds.Y);
      return new RectangleF(bounds.X, splitPosition, bounds.Width, bounds.Bottom - splitPosition);
    }

    private int CompareEndpoints(LayoutTree.BoundsEndpoint A, LayoutTree.BoundsEndpoint B)
    {
      if (A.Location != B.Location)
        return A.Location.CompareTo(B.Location);
      if (A.Item != B.Item)
        return B.IsEnd.CompareTo(A.IsEnd);
      return A.IsEnd.CompareTo(B.IsEnd);
    }

    public void UpdateItemsBounds()
    {
      if (this.root == null)
        return;
      this.UpdateItemsBounds(this.root);
      this.owner.FindAncestor<LayoutControlContainerElement>()?.LayoutTree.UpdateItemsBounds();
    }

    private void UpdateItemsBounds(LayoutTreeNode node)
    {
      if (node.Item != null)
      {
        node.Item.Bounds = this.ToRectangle(node.Bounds);
        node.Bounds = (RectangleF) node.Item.Bounds;
        node.MinSize = (SizeF) node.Item.MinSize;
        node.MaxSize = (SizeF) node.Item.MaxSize;
      }
      else
      {
        RectangleF bounds = node.Left.Bounds;
        this.UpdateItemsBounds(node.Left);
        this.UpdateItemsBounds(node.Right);
        float diff = node.SplitType == Orientation.Horizontal ? node.Left.Bounds.Width - bounds.Width : node.Left.Bounds.Height - bounds.Height;
        if ((double) Math.Abs(diff) > 1.40129846432482E-45)
          this.Shift(node.Right, diff, node.SplitType);
        node.Bounds = RectangleF.Union(node.Left.Bounds, node.Right.Bounds);
        node.MinSize = this.UnionMinSize(node.Left.MinSize, node.Right.MinSize, node.SplitType);
        node.MaxSize = this.UnionMaxSize(node.Left.MaxSize, node.Right.MaxSize, node.SplitType);
      }
    }

    private SizeF UnionMaxSize(SizeF size1, SizeF size2, Orientation orientation)
    {
      if (orientation == Orientation.Horizontal)
        return new SizeF((double) size1.Width == 0.0 || (double) size2.Width == 0.0 ? 0.0f : size1.Width + size2.Width, (double) size1.Height == 0.0 ? size2.Height : ((double) size2.Height == 0.0 ? size1.Height : Math.Min(size1.Height, size2.Height)));
      return new SizeF((double) size1.Width == 0.0 ? size2.Width : ((double) size2.Width == 0.0 ? size1.Width : Math.Min(size1.Width, size2.Width)), (double) size1.Height == 0.0 || (double) size2.Height == 0.0 ? 0.0f : size1.Height + size2.Height);
    }

    private SizeF UnionMinSize(SizeF size1, SizeF size2, Orientation orientation)
    {
      if (orientation == Orientation.Horizontal)
        return new SizeF(size1.Width + size2.Width, Math.Max(size1.Height, size2.Height));
      return new SizeF(Math.Max(size1.Width, size2.Width), size1.Height + size2.Height);
    }

    private void Shift(LayoutTreeNode node, float diff, Orientation direction)
    {
      if (node.Item != null)
      {
        RectangleF bounds = node.Bounds;
        bounds.Offset(direction == Orientation.Horizontal ? new PointF(diff, 0.0f) : new PointF(0.0f, diff));
        node.Item.Bounds = this.ToRectangle(bounds);
        node.Bounds = (RectangleF) node.Item.Bounds;
      }
      else
      {
        this.Shift(node.Right, diff, direction);
        this.Shift(node.Left, diff, direction);
        node.Bounds = RectangleF.Union(node.Left.Bounds, node.Right.Bounds);
      }
    }

    internal void MoveSplitter(float splitterDiff, LayoutTreeNode node)
    {
      node = node ?? this.root;
      if (node == null || node.Item != null)
        return;
      float splitPosition1 = node.SplitPosition + splitterDiff;
      this.ChangeBounds(this.CalculateSplitBounds(node, node.Bounds, node.SplitType, splitPosition1, true), node.Left);
      float splitPosition2 = node.SplitType == Orientation.Horizontal ? node.Left.Bounds.Right : node.Left.Bounds.Bottom;
      RectangleF splitBounds = this.CalculateSplitBounds(node, node.Bounds, node.SplitType, splitPosition2, false);
      if (node.SplitType == Orientation.Vertical)
        splitBounds.Width = Math.Max(splitBounds.Width, node.Left.Bounds.Width);
      else
        splitBounds.Height = Math.Max(splitBounds.Height, node.Left.Bounds.Height);
      this.ChangeBounds(splitBounds, node.Right);
      if (node.SplitType == Orientation.Vertical && (double) node.Right.Bounds.Width > (double) node.Left.Bounds.Width)
      {
        RectangleF bounds = node.Left.Bounds;
        bounds.Width = node.Right.Bounds.Width;
        this.ChangeBounds(bounds, node.Left);
      }
      else if (node.SplitType == Orientation.Horizontal && (double) node.Right.Bounds.Height > (double) node.Left.Bounds.Height)
      {
        RectangleF bounds = node.Left.Bounds;
        bounds.Height = node.Right.Bounds.Height;
        this.ChangeBounds(bounds, node.Left);
      }
      node.SplitPosition = splitPosition2;
      node.Bounds = RectangleF.Union(node.Left.Bounds, node.Right.Bounds);
    }

    internal void ChangeBounds(RectangleF bounds)
    {
      this.ChangeBounds(bounds, this.root);
    }

    internal void ChangeBounds(RectangleF bounds, LayoutTreeNode node)
    {
      if (node == null)
        return;
      if (node.Item != null)
      {
        node.Item.Bounds = this.ToRectangle(bounds);
        node.Bounds = (RectangleF) node.Item.Bounds;
      }
      else
      {
        float splitPosition1 = node.SplitType == Orientation.Horizontal ? bounds.Left + (node.OriginalSplitPosition - node.OriginalBounds.Left) * bounds.Width / node.OriginalBounds.Width : bounds.Top + (node.OriginalSplitPosition - node.OriginalBounds.Top) * bounds.Height / node.OriginalBounds.Height;
        this.ChangeBounds(this.CalculateSplitBounds(node, bounds, node.SplitType, splitPosition1, true), node.Left);
        float splitPosition2 = node.SplitType == Orientation.Horizontal ? node.Left.Bounds.Right : node.Left.Bounds.Bottom;
        RectangleF splitBounds = this.CalculateSplitBounds(node, bounds, node.SplitType, splitPosition2, false);
        if (node.SplitType == Orientation.Vertical)
          splitBounds.Width = Math.Max(splitBounds.Width, node.Left.Bounds.Width);
        else
          splitBounds.Height = Math.Max(splitBounds.Height, node.Left.Bounds.Height);
        this.ChangeBounds(splitBounds, node.Right);
        if (node.SplitType == Orientation.Vertical && (double) node.Right.Bounds.Width > (double) node.Left.Bounds.Width)
        {
          RectangleF bounds1 = node.Left.Bounds;
          bounds1.Width = node.Right.Bounds.Width;
          this.ChangeBounds(bounds1, node.Left);
        }
        else if (node.SplitType == Orientation.Horizontal && (double) node.Right.Bounds.Height > (double) node.Left.Bounds.Height)
        {
          RectangleF bounds1 = node.Left.Bounds;
          bounds1.Height = node.Right.Bounds.Height;
          this.ChangeBounds(bounds1, node.Left);
        }
        node.SplitPosition = splitPosition2;
        node.Bounds = RectangleF.Union(node.Left.Bounds, node.Right.Bounds);
      }
    }

    private Rectangle ToRectangle(RectangleF rect)
    {
      return new Rectangle((int) Math.Round((double) rect.X), (int) Math.Round((double) rect.Y), (int) Math.Round((double) rect.Right) - (int) Math.Round((double) rect.X), (int) Math.Round((double) rect.Bottom) - (int) Math.Round((double) rect.Y));
    }

    public LayoutTreeNode FindNodeByItem(LayoutControlItemBase item)
    {
      foreach (LayoutTreeNode enumChildNode in this.EnumChildNodes(this.root))
      {
        if (enumChildNode.Item == item)
          return enumChildNode;
      }
      return (LayoutTreeNode) null;
    }

    private IEnumerable<LayoutTreeNode> EnumChildNodes(LayoutTreeNode node)
    {
      if (node != null)
      {
        if (node.Item != null)
          yield return node;
        if (node.Left != null)
        {
          foreach (LayoutTreeNode enumChildNode in this.EnumChildNodes(node.Left))
            yield return enumChildNode;
        }
        if (node.Right != null)
        {
          foreach (LayoutTreeNode enumChildNode in this.EnumChildNodes(node.Right))
            yield return enumChildNode;
        }
        yield return node;
      }
    }

    public LayoutTreeNode FindGroupBySplitter(Point point, Orientation splitType)
    {
      return this.FindGroupBySplitter(point, splitType, this.root);
    }

    public LayoutTreeNode FindGroupBySplitter(
      Point point,
      Orientation splitType,
      LayoutTreeNode node)
    {
      if (node == null || node.Left == null || node.Right == null)
        return (LayoutTreeNode) null;
      if (node.SplitType == Orientation.Horizontal && splitType == Orientation.Horizontal)
      {
        if ((double) Math.Abs(node.SplitPosition - (float) point.X) < 4.0 && (double) node.Bounds.Top < (double) point.Y && (double) point.Y < (double) node.Bounds.Bottom)
          return node;
      }
      else if (node.SplitType == Orientation.Vertical && splitType == Orientation.Vertical && ((double) Math.Abs(node.SplitPosition - (float) point.Y) < 4.0 && (double) node.Bounds.Left < (double) point.X) && (double) point.X < (double) node.Bounds.Right)
        return node;
      if (this.CalculateSplitBounds(node, true).Contains((PointF) point))
        return this.FindGroupBySplitter(point, splitType, node.Left);
      return this.FindGroupBySplitter(point, splitType, node.Right);
    }

    public LayoutControlDropTargetInfo GetDropTargetNode(
      DraggableLayoutControlItem dropTargetElement,
      Point mousePosition)
    {
      return this.GetDropTargetNode(dropTargetElement, mousePosition, (System.Type) null);
    }

    public LayoutControlDropTargetInfo GetDropTargetNode(
      DraggableLayoutControlItem dropTargetElement,
      Point mousePosition,
      System.Type dragContext)
    {
      LayoutControlContainerElement ancestor = dropTargetElement.AssociatedItem.FindAncestor<LayoutControlContainerElement>();
      mousePosition.Offset(-ancestor.ControlBoundingRectangle.X, -ancestor.ControlBoundingRectangle.Y);
      LayoutControlDropTargetInfo controlDropTargetInfo = new LayoutControlDropTargetInfo();
      Rectangle bounds = dropTargetElement.Bounds;
      Point point = new Point(mousePosition.X - bounds.X, mousePosition.Y - bounds.Y);
      LayoutTreeNode nodeByItem = this.FindNodeByItem(dropTargetElement.AssociatedItem);
      if (dropTargetElement.AssociatedItem is LayoutControlGroupItem && ((LayoutControlGroupItem) dropTargetElement.AssociatedItem).Items.Count == 0)
      {
        LayoutControlGroupItem associatedItem = dropTargetElement.AssociatedItem as LayoutControlGroupItem;
        Point location = new Point(dropTargetElement.ControlBoundingRectangle.X - this.owner.ControlBoundingRectangle.X, dropTargetElement.ControlBoundingRectangle.Y - this.owner.ControlBoundingRectangle.Y);
        location.Offset(associatedItem.ContainerElement.BoundingRectangle.Location);
        controlDropTargetInfo.TargetBounds = new Rectangle(location, associatedItem.ContainerElement.BoundingRectangle.Size);
        controlDropTargetInfo.TargetNode = nodeByItem;
        controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Center;
        return controlDropTargetInfo;
      }
      if (dropTargetElement.AssociatedItem is LayoutControlTabbedGroup)
      {
        LayoutControlTabbedGroup associatedItem = dropTargetElement.AssociatedItem as LayoutControlTabbedGroup;
        if ((object) dragContext != null && typeof (LayoutControlGroupItem).IsAssignableFrom(dragContext) && ((double) point.Y / (double) bounds.Height >= 0.333333343267441 && (double) point.Y / (double) bounds.Height <= 0.666666686534882 && ((double) point.X / (double) bounds.Width > 0.25 && (double) point.X / (double) bounds.Width < 0.75) && associatedItem.ItemGroups.Count == 0 || associatedItem.TabStrip.ItemContainer.BoundingRectangle.Contains(point)))
        {
          Point location = new Point((int) nodeByItem.Bounds.Location.X, (int) nodeByItem.Bounds.Location.Y);
          location.Offset(associatedItem.TabStrip.ContentArea.BoundingRectangle.Location);
          controlDropTargetInfo.TargetTabIndex = associatedItem.GetTargetDropIndex(point);
          controlDropTargetInfo.TargetBounds = controlDropTargetInfo.TargetTabIndex >= 0 ? associatedItem.GetTargetBounds(controlDropTargetInfo.TargetTabIndex) : new Rectangle(location, associatedItem.TabStrip.ContentArea.BoundingRectangle.Size);
          controlDropTargetInfo.TargetNode = nodeByItem;
          controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Center;
          return controlDropTargetInfo;
        }
      }
      if ((double) point.Y / (double) bounds.Height <= 0.16666667163372)
      {
        float num = Math.Min(0.99f, (float) point.Y / ((float) bounds.Height / 6f));
        List<LayoutTreeNode> groupsWithSameEdge = this.GetParentGroupsWithSameEdge(nodeByItem, bounds.Top, RadDirection.Up);
        if (groupsWithSameEdge.Count == 0)
          return (LayoutControlDropTargetInfo) null;
        int index = Math.Min(groupsWithSameEdge.Count - 1, (int) ((double) groupsWithSameEdge.Count * (double) num));
        LayoutTreeNode layoutTreeNode = groupsWithSameEdge[index];
        controlDropTargetInfo.TargetBounds = new Rectangle((int) layoutTreeNode.Bounds.X, (int) layoutTreeNode.Bounds.Y, (int) layoutTreeNode.Bounds.Width, 26);
        controlDropTargetInfo.TargetNode = layoutTreeNode;
        controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Top;
      }
      else if ((double) point.Y / (double) bounds.Height > 0.16666667163372 && (double) point.Y / (double) bounds.Height < 0.333333343267441)
      {
        controlDropTargetInfo.TargetBounds = new Rectangle(bounds.Location, new Size(bounds.Width, bounds.Height / 2));
        controlDropTargetInfo.TargetNode = nodeByItem;
        controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Top;
      }
      else if ((double) point.Y / (double) bounds.Height > 0.666666686534882 && (double) point.Y / (double) bounds.Height < 0.833333313465118)
      {
        controlDropTargetInfo.TargetBounds = new Rectangle(new Point(bounds.X, bounds.Y + bounds.Height / 2), new Size(bounds.Width, bounds.Height / 2));
        controlDropTargetInfo.TargetNode = nodeByItem;
        controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Bottom;
      }
      else if ((double) point.Y / (double) bounds.Height >= 0.833333313465118)
      {
        float num = Math.Min(0.99f, (float) (bounds.Height - point.Y) / ((float) bounds.Height / 6f));
        List<LayoutTreeNode> groupsWithSameEdge = this.GetParentGroupsWithSameEdge(nodeByItem, bounds.Bottom, RadDirection.Down);
        if (groupsWithSameEdge.Count == 0)
          return (LayoutControlDropTargetInfo) null;
        int index = Math.Min(groupsWithSameEdge.Count - 1, (int) ((double) groupsWithSameEdge.Count * (double) num));
        LayoutTreeNode layoutTreeNode = groupsWithSameEdge[index];
        controlDropTargetInfo.TargetBounds = new Rectangle((int) layoutTreeNode.Bounds.X, (int) layoutTreeNode.Bounds.Bottom - 26, (int) layoutTreeNode.Bounds.Width, 26);
        controlDropTargetInfo.TargetNode = layoutTreeNode;
        controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Bottom;
      }
      else if ((double) point.Y / (double) bounds.Height >= 0.333333343267441 && (double) point.Y / (double) bounds.Height <= 0.666666686534882)
      {
        if ((double) point.X / (double) bounds.Width <= 0.25)
        {
          float num = Math.Min(0.99f, (float) point.X / ((float) bounds.Width / 4f));
          List<LayoutTreeNode> groupsWithSameEdge = this.GetParentGroupsWithSameEdge(nodeByItem, bounds.Left, RadDirection.Left);
          if (groupsWithSameEdge.Count == 0)
            return (LayoutControlDropTargetInfo) null;
          int index = Math.Min(groupsWithSameEdge.Count - 1, (int) ((double) groupsWithSameEdge.Count * (double) num));
          LayoutTreeNode layoutTreeNode = groupsWithSameEdge[index];
          controlDropTargetInfo.TargetBounds = new Rectangle((int) layoutTreeNode.Bounds.X, (int) layoutTreeNode.Bounds.Y, 86, (int) layoutTreeNode.Bounds.Height);
          controlDropTargetInfo.TargetNode = layoutTreeNode;
          controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Left;
        }
        else if ((double) point.X / (double) bounds.Width > 0.25 && (double) point.X / (double) bounds.Width <= 0.5)
        {
          controlDropTargetInfo.TargetBounds = new Rectangle(bounds.Location, new Size(bounds.Width / 2, bounds.Height));
          controlDropTargetInfo.TargetNode = nodeByItem;
          controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Left;
        }
        else if ((double) point.X / (double) bounds.Width > 0.5 && (double) point.X / (double) bounds.Width < 0.75)
        {
          controlDropTargetInfo.TargetBounds = new Rectangle(new Point(bounds.X + bounds.Width / 2, bounds.Y), new Size(bounds.Width / 2, bounds.Height));
          controlDropTargetInfo.TargetNode = nodeByItem;
          controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Right;
        }
        else if ((double) point.X / (double) bounds.Width >= 0.75)
        {
          float num = Math.Min(0.99f, (float) (bounds.Width - point.X) / ((float) bounds.Width / 4f));
          List<LayoutTreeNode> groupsWithSameEdge = this.GetParentGroupsWithSameEdge(nodeByItem, bounds.Right, RadDirection.Right);
          if (groupsWithSameEdge.Count == 0)
            return (LayoutControlDropTargetInfo) null;
          int index = Math.Min(groupsWithSameEdge.Count - 1, (int) ((double) groupsWithSameEdge.Count * (double) num));
          LayoutTreeNode layoutTreeNode = groupsWithSameEdge[index];
          controlDropTargetInfo.TargetBounds = new Rectangle((int) layoutTreeNode.Bounds.Right - 86, (int) layoutTreeNode.Bounds.Y, 86, (int) layoutTreeNode.Bounds.Height);
          controlDropTargetInfo.TargetNode = layoutTreeNode;
          controlDropTargetInfo.TargetPosition = LayoutControlDropPosition.Right;
        }
      }
      return controlDropTargetInfo;
    }

    private List<LayoutTreeNode> GetParentGroupsWithSameEdge(
      LayoutTreeNode itemNode,
      int edgeLocation,
      RadDirection edgeSpecified)
    {
      List<LayoutTreeNode> layoutTreeNodeList = new List<LayoutTreeNode>();
      for (LayoutTreeNode layoutTreeNode = itemNode; layoutTreeNode != null; layoutTreeNode = layoutTreeNode.Parent)
      {
        if (edgeSpecified == RadDirection.Up && (double) edgeLocation == (double) layoutTreeNode.Bounds.Top || edgeSpecified == RadDirection.Left && (double) edgeLocation == (double) layoutTreeNode.Bounds.Left || (edgeSpecified == RadDirection.Right && (double) edgeLocation == (double) layoutTreeNode.Bounds.Right || edgeSpecified == RadDirection.Down && (double) edgeLocation == (double) layoutTreeNode.Bounds.Bottom))
          layoutTreeNodeList.Add(layoutTreeNode);
      }
      layoutTreeNodeList.Reverse();
      return layoutTreeNodeList;
    }

    internal void HandleDrop(
      DraggableLayoutControlItem dropTargetElement,
      LayoutControlItemBase draggedElement,
      Point mousePosition)
    {
      Point mousePosition1 = mousePosition;
      mousePosition.Offset(-this.owner.ControlBoundingRectangle.X, -this.owner.ControlBoundingRectangle.Y);
      if (draggedElement is DraggableLayoutControlItem)
        draggedElement = ((DraggableLayoutControlItem) draggedElement).AssociatedItem;
      LayoutTreeNode draggedNode = this.FindNodeByItem(draggedElement);
      if (draggedNode != null)
      {
        if (!this.RemoveNode(draggedNode, false))
          return;
      }
      else
      {
        draggedNode = new LayoutTreeNode();
        draggedNode.Item = draggedElement;
      }
      LayoutControlDropTargetInfo dropTargetNode = this.GetDropTargetNode(dropTargetElement, mousePosition1, draggedElement.GetType());
      if (dropTargetNode.TargetNode == null)
        return;
      LayoutTreeNode targetNode = dropTargetNode.TargetNode;
      LayoutControlDropPosition targetPosition = dropTargetNode.TargetPosition;
      RectangleF targetBounds = (RectangleF) dropTargetNode.TargetBounds;
      ILayoutControlItemsHost controlItemsHost = dropTargetElement.AssociatedItem.GetParentItemsContainer();
      if (targetPosition == LayoutControlDropPosition.Center)
      {
        LayoutControlGroupItem associatedItem1 = dropTargetElement.AssociatedItem as LayoutControlGroupItem;
        LayoutControlTabbedGroup associatedItem2 = dropTargetElement.AssociatedItem as LayoutControlTabbedGroup;
        if (associatedItem1 != null)
          controlItemsHost = (ILayoutControlItemsHost) associatedItem1.ContainerElement;
        if (associatedItem2 != null)
          controlItemsHost = (ILayoutControlItemsHost) null;
      }
      ILayoutControlItemsHost parentItemsContainer = draggedElement.GetParentItemsContainer();
      if (controlItemsHost != parentItemsContainer)
      {
        parentItemsContainer?.Items.Remove((RadItem) draggedElement);
        controlItemsHost?.Items.Add((RadItem) draggedElement);
      }
      if (dropTargetElement.AssociatedItem is LayoutControlGroupItem && targetPosition == LayoutControlDropPosition.Center)
      {
        (dropTargetElement.AssociatedItem as LayoutControlGroupItem).ContainerElement.RebuildLayoutTree();
      }
      else
      {
        if (dropTargetElement.AssociatedItem is LayoutControlTabbedGroup && targetPosition == LayoutControlDropPosition.Center)
        {
          LayoutControlTabbedGroup associatedItem = (LayoutControlTabbedGroup) dropTargetElement.AssociatedItem;
          LayoutControlGroupItem controlGroupItem = draggedElement as LayoutControlGroupItem;
          if (controlGroupItem != null)
          {
            if (dropTargetNode.TargetTabIndex >= 0 && dropTargetNode.TargetTabIndex <= associatedItem.ItemGroups.Count)
            {
              associatedItem.ItemGroups.Insert(dropTargetNode.TargetTabIndex, (RadItem) controlGroupItem);
              return;
            }
            associatedItem.ItemGroups.Add((RadItem) controlGroupItem);
            return;
          }
        }
        LayoutTreeNode node = new LayoutTreeNode();
        node.Item = targetNode.Item;
        node.Parent = targetNode;
        node.Left = targetNode.Left;
        node.Right = targetNode.Right;
        node.OriginalBounds = targetNode.OriginalBounds;
        node.Bounds = targetNode.Bounds;
        node.SplitType = targetNode.SplitType;
        node.SplitPosition = targetNode.SplitPosition;
        node.OriginalSplitPosition = targetNode.OriginalSplitPosition;
        node.MinSize = targetNode.MinSize;
        node.MaxSize = targetNode.MaxSize;
        if (targetNode.Left != null)
          targetNode.Left.Parent = node;
        if (targetNode.Right != null)
          targetNode.Right.Parent = node;
        draggedNode.Parent = targetNode;
        draggedNode.Bounds = targetBounds;
        targetNode.Item = (LayoutControlItemBase) null;
        targetNode.SplitType = targetPosition == LayoutControlDropPosition.Top || targetPosition == LayoutControlDropPosition.Bottom ? Orientation.Vertical : Orientation.Horizontal;
        targetNode.Left = targetPosition == LayoutControlDropPosition.Left || targetPosition == LayoutControlDropPosition.Top ? draggedNode : node;
        targetNode.Right = targetPosition == LayoutControlDropPosition.Left || targetPosition == LayoutControlDropPosition.Top ? node : draggedNode;
        switch (targetPosition)
        {
          case LayoutControlDropPosition.Left:
            this.ChangeBounds(new RectangleF(targetNode.Bounds.X + targetBounds.Width, targetNode.Bounds.Y, targetNode.Bounds.Width - targetBounds.Width, targetNode.Bounds.Height), node);
            break;
          case LayoutControlDropPosition.Right:
            this.ChangeBounds(new RectangleF(targetNode.Bounds.X, targetNode.Bounds.Y, targetNode.Bounds.Width - targetBounds.Width, targetNode.Bounds.Height), node);
            break;
          case LayoutControlDropPosition.Top:
            this.ChangeBounds(new RectangleF(targetNode.Bounds.X, targetNode.Bounds.Y + targetBounds.Height, targetNode.Bounds.Width, targetNode.Bounds.Height - targetBounds.Height), node);
            break;
          case LayoutControlDropPosition.Bottom:
            this.ChangeBounds(new RectangleF(targetNode.Bounds.X, targetNode.Bounds.Y, targetNode.Bounds.Width, targetNode.Bounds.Height - targetBounds.Height), node);
            break;
        }
        targetNode.SplitPosition = targetNode.OriginalSplitPosition = targetNode.SplitType == Orientation.Vertical ? targetNode.Right.Bounds.Top : targetNode.Right.Bounds.Left;
        targetNode.OriginalBounds = targetNode.Bounds;
        this.ChangeBounds(this.root.Bounds);
        this.UpdateItemsBounds();
        this.owner.PerformControlLayout();
        if (this.owner.ElementTree == null || this.owner.ElementTree.Control == null)
          return;
        (this.owner.ElementTree.Control as RadLayoutControl)?.OnHandleDropCompleted((object) this);
      }
    }

    public void RemoveItem(LayoutControlItemBase item)
    {
      this.RemoveNode(this.FindNodeByItem(item), true);
    }

    private bool RemoveNode(LayoutTreeNode draggedNode, bool changeBounds)
    {
      if (draggedNode == this.root)
        this.root = (LayoutTreeNode) null;
      if (draggedNode == null || draggedNode.Parent == null || (draggedNode.Right != null || draggedNode.Left != null))
        return false;
      LayoutTreeNode parent = draggedNode.Parent;
      if (this.IsLeftChild(draggedNode))
      {
        parent.Right.Parent = parent.Parent;
        if (parent.Parent == null)
          this.root = parent.Right;
        else if (this.IsLeftChild(parent))
          parent.Parent.Left = parent.Right;
        else
          parent.Parent.Right = parent.Right;
        if (changeBounds)
        {
          this.ChangeBounds(parent.Bounds, draggedNode.Parent.Right);
          parent.Right.OriginalBounds = parent.Right.Bounds;
        }
        draggedNode.Parent = (LayoutTreeNode) null;
      }
      else
      {
        parent.Left.Parent = parent.Parent;
        if (parent.Parent == null)
          this.root = parent.Left;
        else if (this.IsLeftChild(parent))
          parent.Parent.Left = parent.Left;
        else
          parent.Parent.Right = parent.Left;
        if (changeBounds)
        {
          this.ChangeBounds(parent.Bounds, draggedNode.Parent.Left);
          parent.Left.OriginalBounds = parent.Left.Bounds;
        }
        draggedNode.Parent = (LayoutTreeNode) null;
      }
      return true;
    }

    private bool IsLeftChild(LayoutTreeNode node)
    {
      if (node.Parent == null)
        return false;
      return node.Parent.Left == node;
    }

    internal class BoundsEndpoint
    {
      internal int Location;
      internal bool IsEnd;
      internal LayoutControlItemBase Item;

      public BoundsEndpoint(int location, bool isEnd, LayoutControlItemBase item)
      {
        this.Location = location;
        this.IsEnd = isEnd;
        this.Item = item;
      }
    }
  }
}
