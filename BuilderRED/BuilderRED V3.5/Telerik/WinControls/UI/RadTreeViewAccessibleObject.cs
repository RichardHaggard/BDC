// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeViewAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadTreeViewAccessibleObject : Control.ControlAccessibleObject
  {
    private RadTreeView owner;
    private Dictionary<RadTreeNode, RadTreeNodeAccessibleObject> cachedObjects;
    private Dictionary<int, RadTreeNode> cachedObjectIndices;

    public RadTreeViewAccessibleObject(RadTreeView owner)
      : base((Control) owner)
    {
      this.cachedObjects = new Dictionary<RadTreeNode, RadTreeNodeAccessibleObject>();
      this.cachedObjectIndices = new Dictionary<int, RadTreeNode>();
      this.owner = owner;
      this.owner.NodeExpandedChanged += new RadTreeView.TreeViewEventHandler(this.owner_NodeExpandedChanged);
      this.owner.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(this.owner_SelectedNodeChanged);
      this.owner.Nodes.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Nodes_CollectionChanged);
    }

    public override AccessibleObject GetFocused()
    {
      if (this.owner == null || this.owner.Nodes.Count <= 0)
        return base.GetFocused();
      foreach (RadTreeNode node in this.owner.TreeViewElement.GetNodes())
      {
        AccessibleObject accessibleObject = this.GetNodeAccessibleObject(node);
        if (accessibleObject != null && (accessibleObject.State & AccessibleStates.Focused) != AccessibleStates.None)
          return accessibleObject;
      }
      if ((this.State & AccessibleStates.Focused) != AccessibleStates.None)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.Outline;
      }
    }

    public override int GetChildCount()
    {
      if (this.cachedObjects.Count > 0)
        return this.cachedObjects.Count;
      int num = 0;
      foreach (RadTreeNode node in this.owner.TreeViewElement.GetNodes())
        ++num;
      return num;
    }

    public override AccessibleObject GetChild(int index)
    {
      if (this.cachedObjectIndices.Count == 0)
      {
        int key = 0;
        foreach (RadTreeNode node in this.owner.TreeViewElement.GetNodes())
        {
          this.cachedObjectIndices.Add(key, node);
          ++key;
        }
      }
      if (!this.cachedObjectIndices.ContainsKey(index))
        return (AccessibleObject) null;
      RadTreeNode cachedObjectIndex = this.cachedObjectIndices[index];
      if (cachedObjectIndex != null)
        return this.GetNodeAccessibleObject(cachedObjectIndex);
      return (AccessibleObject) null;
    }

    public override string Description
    {
      get
      {
        return "RadTreeNode ;" + this.Name + ";" + (object) this.GetChildCount();
      }
    }

    private int GetChildIndex(RadTreeNode node)
    {
      int num = 0;
      foreach (RadTreeNode node1 in this.owner.TreeViewElement.GetNodes())
      {
        if (node == node1)
          return num;
        ++num;
      }
      return -1;
    }

    public override Rectangle Bounds
    {
      get
      {
        Rectangle bounds = base.Bounds;
        bounds.Inflate(2, 2);
        return bounds;
      }
    }

    public AccessibleObject GetNodeAccessibleObject(RadTreeNode node)
    {
      if (!this.cachedObjects.ContainsKey(node))
      {
        if (node.Parent == null)
          this.cachedObjects.Add(node, new RadTreeNodeAccessibleObject(node, (AccessibleObject) this, this));
        else
          this.cachedObjects.Add(node, new RadTreeNodeAccessibleObject(node, this.GetNodeAccessibleObject(node.Parent), this));
      }
      return (AccessibleObject) this.cachedObjects[node];
    }

    private void owner_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
    {
      this.NotifyClients(AccessibleEvents.Focus, this.GetChildIndex(e.Node));
    }

    private void owner_NodeExpandedChanged(object sender, RadTreeViewEventArgs e)
    {
      this.NotifyClients(AccessibleEvents.SelectionRemove, this.GetChildIndex(e.Node));
      this.NotifyClients(AccessibleEvents.SelectionAdd, this.GetChildIndex(e.Node));
      this.NotifyClients(AccessibleEvents.Focus, this.GetChildIndex(e.Node));
    }

    private void Nodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.cachedObjects.Clear();
      this.cachedObjectIndices.Clear();
    }
  }
}
