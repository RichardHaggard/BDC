// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeNodeAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  public class RadTreeNodeAccessibleObject : RadItemAccessibleObject
  {
    private RadTreeNode owner;
    private AccessibleObject parent;
    private RadTreeViewAccessibleObject treeViewAccObject;

    public RadTreeNodeAccessibleObject(
      RadTreeNode owner,
      AccessibleObject parent,
      RadTreeViewAccessibleObject treeViewAccObject)
    {
      this.owner = owner;
      this.parent = parent;
      this.treeViewAccObject = treeViewAccObject;
    }

    public override object Owner
    {
      get
      {
        return (object) this.owner;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        TreeNodeElement element = this.owner.TreeViewElement.GetElement(this.owner);
        if (element != null)
          return new Rectangle(this.owner.TreeView.PointToScreen(element.ControlBoundingRectangle.Location), element.Size);
        return Rectangle.Empty;
      }
    }

    public override string Name
    {
      get
      {
        return this.owner.Text;
      }
      set
      {
        base.Name = value;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.OutlineItem;
      }
    }

    public override AccessibleStates State
    {
      get
      {
        if (!this.owner.Enabled)
          return base.State;
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (!this.IsVisible(this.owner))
          accessibleStates |= AccessibleStates.Invisible | AccessibleStates.Offscreen;
        else if (this.owner.Nodes.Count > 0)
        {
          if (this.owner.Expanded)
            accessibleStates |= AccessibleStates.Expanded;
          else
            accessibleStates |= AccessibleStates.Collapsed;
        }
        if (this.owner.Current)
          accessibleStates |= AccessibleStates.Focused;
        if (this.owner.Selected)
          accessibleStates |= AccessibleStates.Selected;
        return accessibleStates;
      }
    }

    public override AccessibleObject Parent
    {
      get
      {
        return this.parent;
      }
    }

    public override string Value
    {
      get
      {
        int num = 0;
        for (RadTreeNode radTreeNode = this.owner; radTreeNode.Parent != null; radTreeNode = radTreeNode.Parent)
          ++num;
        return num.ToString();
      }
      set
      {
        base.Value = value;
      }
    }

    public override AccessibleObject GetSelected()
    {
      return (AccessibleObject) null;
    }

    public override AccessibleObject Navigate(AccessibleNavigation navdir)
    {
      switch (navdir)
      {
        case AccessibleNavigation.Up:
          int index1 = this.owner.parent.Nodes.IndexOf(this.owner) - 1;
          if (this.owner.parent.Nodes.Count > 0 && index1 >= 0)
            return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.parent.Nodes[index1]);
          return (AccessibleObject) null;
        case AccessibleNavigation.Down:
          int index2 = this.owner.parent.Nodes.IndexOf(this.owner) + 1;
          if (this.owner.parent.Nodes.Count > index2)
            return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.parent.Nodes[index2]);
          return (AccessibleObject) null;
        case AccessibleNavigation.Left:
          if (this.owner.Parent != null)
            return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.Parent);
          return (AccessibleObject) null;
        case AccessibleNavigation.Right:
          if (this.owner.HasNodes)
            return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.Nodes[0]);
          return (AccessibleObject) null;
        case AccessibleNavigation.Next:
          if (this.owner.HasNodes)
            return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.Nodes[0]);
          for (RadTreeNode radTreeNode = this.owner; radTreeNode.parent != null; radTreeNode = radTreeNode.parent)
          {
            int index3 = radTreeNode.parent.Nodes.IndexOf(radTreeNode) + 1;
            if (radTreeNode.parent.Nodes.Count > index3)
              return this.treeViewAccObject.GetNodeAccessibleObject(radTreeNode.parent.Nodes[index3]);
          }
          return (AccessibleObject) null;
        case AccessibleNavigation.Previous:
          int index4 = this.owner.parent.Nodes.IndexOf(this.owner) - 1;
          if (this.owner.parent.Nodes.Count > 0 && index4 >= 0)
            return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.parent.Nodes[index4]);
          if (this.owner.Parent != null)
            return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.Parent);
          return (AccessibleObject) null;
        default:
          return (AccessibleObject) null;
      }
    }

    public override string DefaultAction
    {
      get
      {
        return !this.owner.Expanded ? "Expand" : "Collapse";
      }
    }

    public override void DoDefaultAction()
    {
      if (this.owner.Expanded)
        this.owner.Collapse();
      else
        this.owner.Expand();
    }

    public override int GetChildCount()
    {
      return this.owner.Nodes.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      return this.treeViewAccObject.GetNodeAccessibleObject(this.owner.Nodes[index]);
    }

    private bool IsVisible(RadTreeNode node)
    {
      if (this.owner.TreeViewElement.GetElement(this.owner) == null)
        return false;
      for (RadTreeNode parent = node.Parent; parent != null; parent = parent.Parent)
      {
        if (!parent.Expanded)
          return false;
      }
      return true;
    }

    public override string Description
    {
      get
      {
        return "RadTreeNode ;" + this.Name + ";" + (object) this.GetChildCount();
      }
    }
  }
}
