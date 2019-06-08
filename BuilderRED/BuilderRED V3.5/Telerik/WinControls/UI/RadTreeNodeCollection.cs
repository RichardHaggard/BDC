// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeNodeCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadTreeNodeCollection : NotifyCollection<RadTreeNode>
  {
    private RadTreeNode owner;
    private int version;

    internal RadTreeNodeCollection(RadTreeNode owner)
      : base((IList<RadTreeNode>) new TreeNodeView(owner))
    {
      this.owner = owner;
    }

    [Browsable(false)]
    public RadTreeNode Owner
    {
      get
      {
        return this.owner;
      }
    }

    public RadTreeView TreeView
    {
      get
      {
        if (this.owner != null)
          return this.owner.TreeView;
        return (RadTreeView) null;
      }
    }

    public void Refresh()
    {
      if (this.owner.TreeViewElement == null)
        return;
      ++this.version;
      this.Update();
      this.owner.TreeViewElement.Update(RadTreeViewElement.UpdateActions.Resume);
    }

    public RadTreeNode Add(string text)
    {
      RadTreeNode radTreeNode = this.owner == null || this.owner.TreeViewElement == null ? new RadTreeNode() : (RadTreeNode) ((IDataItemSource) this.owner.TreeViewElement).NewItem();
      this.Add(radTreeNode);
      radTreeNode.Text = text;
      radTreeNode.Name = text;
      return radTreeNode;
    }

    public virtual RadTreeNode Add(string text, int imageIndex)
    {
      RadTreeNode radTreeNode = this.Add(text);
      radTreeNode.ImageIndex = imageIndex;
      return radTreeNode;
    }

    public virtual RadTreeNode Add(string text, string imageKey)
    {
      RadTreeNode radTreeNode = this.Add(text);
      radTreeNode.ImageKey = imageKey;
      return radTreeNode;
    }

    public virtual RadTreeNode Add(string key, string text, int imageIndex)
    {
      RadTreeNode radTreeNode = this.Add(text);
      radTreeNode.Name = key;
      radTreeNode.ImageIndex = imageIndex;
      return radTreeNode;
    }

    public virtual RadTreeNode Add(string key, string text, string imageKey)
    {
      RadTreeNode radTreeNode = this.Add(text);
      radTreeNode.Name = key;
      radTreeNode.ImageKey = imageKey;
      return radTreeNode;
    }

    public void Remove(string name)
    {
      int index = this.IndexOf(name);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    public bool Contains(string name)
    {
      if (string.IsNullOrEmpty(name))
        return false;
      return this.IndexOf(name) >= 0;
    }

    public int IndexOf(string name)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
          return index;
      }
      return -1;
    }

    public RadTreeNode this[string name]
    {
      get
      {
        foreach (RadTreeNode radTreeNode in (Collection<RadTreeNode>) this)
        {
          if (radTreeNode.Name == name)
            return radTreeNode;
        }
        foreach (RadTreeNode radTreeNode in (Collection<RadTreeNode>) this)
        {
          if (radTreeNode.Text == name)
            return radTreeNode;
        }
        return (RadTreeNode) null;
      }
    }

    public ITreeNodeEnumerator GetNodeEnumerator()
    {
      return ((TreeNodeView) this.Items).GetNodeEnumerator();
    }

    public ITreeNodeEnumerator GetNodeEnumerator(int position)
    {
      return ((TreeNodeView) this.Items).GetNodeEnumerator(position);
    }

    public ITreeNodeEnumerator GetNodeEnumerator(RadTreeNode node)
    {
      return ((TreeNodeView) this.Items).GetNodeEnumerator(node);
    }

    protected internal void Update()
    {
      if (this.Suspended)
        return;
      ((TreeNodeView) this.Items).Update();
      this.owner.InvalidateOnState();
    }

    internal void UpdateView()
    {
      ((TreeNodeView) this.Items).UpdateView();
    }

    protected override void InsertItem(int index, RadTreeNode item)
    {
      if (item.treeView != null && !this.owner.TreeViewElement.bindingProvider.IsDataBound)
        throw new ArgumentException(string.Format("Cannot add or insert the item '{0}' in more than one place. You must first remove it from its current location or clone it. Parameter name: {0}", (object) item.Text));
      if (this.owner.treeView != null)
      {
        if (!this.owner.treeView.PreProcess(this.owner, item, (object) "Insert", (object) index))
          return;
      }
      item.Parent = this.owner;
      if (this.TreeView != null)
      {
        this.TreeView.TreeViewElement.TreeNodeProvider.SuspendUpdate();
        this.TreeView.TreeViewElement.TreeNodeProvider.SetCurrent(item);
        this.TreeView.TreeViewElement.TreeNodeProvider.ResumeUpdate();
      }
      base.InsertItem(index, item);
    }

    protected override void SetItem(int index, RadTreeNode item)
    {
      if (this.owner.treeView != null)
      {
        if (!this.owner.treeView.PreProcess(this.owner, item, (object) nameof (SetItem), (object) index))
          return;
      }
      item.Parent = this.owner;
      base.SetItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
      RadTreeNode radTreeNode1 = (RadTreeNode) null;
      RadTreeNode radTreeNode2 = this.Items[index];
      RadTreeViewElement radTreeViewElement = this.TreeView != null ? this.TreeView.TreeViewElement : (RadTreeViewElement) null;
      bool flag = false;
      if (radTreeNode2.Current && radTreeViewElement != null)
      {
        radTreeNode1 = radTreeNode2.PrevVisibleNode;
        if (radTreeNode1 == null)
        {
          for (RadTreeNode radTreeNode3 = radTreeNode2; radTreeNode3 != null && radTreeNode1 == null; radTreeNode3 = radTreeNode3.Parent)
            radTreeNode1 = radTreeNode3.NextNode;
        }
        flag = true;
      }
      if (this.owner.treeView != null && radTreeViewElement != null)
      {
        if (!this.owner.treeView.PreProcess(this.owner, radTreeNode2, (object) "Remove", (object) index))
          return;
      }
      if (flag)
      {
        radTreeViewElement.SelectedNode = radTreeNode1;
        if (radTreeViewElement.SelectedNode != radTreeNode1)
        {
          if (radTreeNode1.Enabled)
            return;
          base.RemoveItem(index);
          return;
        }
      }
      RadTreeNode parent = radTreeNode2.Parent;
      radTreeNode2.Parent = (RadTreeNode) null;
      radTreeNode2.TreeViewElement = (RadTreeViewElement) null;
      base.RemoveItem(index);
      if (parent == null || parent.nodes.Count != 0 || this.owner.TreeViewElement == null)
        return;
      this.owner.TreeViewElement.Update(RadTreeViewElement.UpdateActions.NodeStateChanged, parent);
    }

    protected override void ClearItems()
    {
      RadTreeViewElement treeViewElement = this.owner.TreeViewElement;
      if (treeViewElement != null)
      {
        if (!treeViewElement.PreProcess(this.owner, (RadTreeNode) null, (object) "Clear"))
          return;
      }
      foreach (RadTreeNode radTreeNode in (IEnumerable<RadTreeNode>) this.Items)
        radTreeNode.Parent = (RadTreeNode) null;
      base.ClearItems();
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      base.OnCollectionChanged(args);
      if (this.Suspended || this.owner == null)
        return;
      RadTreeViewElement treeViewElement = this.owner.TreeViewElement;
      if (treeViewElement == null)
        return;
      if (!treeViewElement.IsSuspended)
      {
        this.owner.InvalidateOnState();
        this.owner.UpdateParentCheckState();
      }
      if (args.Action == NotifyCollectionChangedAction.Add)
      {
        RadTreeNode newItem = (RadTreeNode) args.NewItems[0];
        treeViewElement.Update(RadTreeViewElement.UpdateActions.ItemAdded, newItem);
        treeViewElement.OnNodeAdded(new RadTreeViewEventArgs(newItem));
      }
      else if (args.Action == NotifyCollectionChangedAction.Remove)
      {
        RadTreeNode newItem = (RadTreeNode) args.NewItems[0];
        treeViewElement.Update(RadTreeViewElement.UpdateActions.ItemRemoved, newItem);
        treeViewElement.OnNodeRemoved(new RadTreeViewEventArgs(newItem));
      }
      else if (args.Action == NotifyCollectionChangedAction.Move)
      {
        RadTreeNode newItem = (RadTreeNode) args.NewItems[0];
        treeViewElement.Update(RadTreeViewElement.UpdateActions.ItemMoved, newItem);
      }
      else
      {
        if (args.Action != NotifyCollectionChangedAction.Reset)
          return;
        RadTreeViewElement.UpdateActions updateAction = this.owner is RadTreeViewElement.RootTreeNode ? RadTreeViewElement.UpdateActions.Reset : RadTreeViewElement.UpdateActions.Resume;
        treeViewElement.Update(updateAction);
      }
    }

    protected internal bool NeedsRefresh
    {
      get
      {
        if (this.owner == null || this.owner.treeView == null || this.owner.treeView.bindingProvider == null)
          return false;
        int levelVersion = this.owner.treeView.bindingProvider.GetLevelVersion(this.owner.BoundIndex);
        if (levelVersion < 0)
          return false;
        return this.version != levelVersion;
      }
    }

    protected internal void SyncVersion()
    {
      this.version = this.owner.treeView.bindingProvider.GetLevelVersion(this.owner.BoundIndex);
    }

    protected internal void ResetVersion()
    {
      this.version = -1;
    }

    protected internal bool IsEmpty
    {
      get
      {
        return ((TreeNodeView) this.Items).IsEmpty;
      }
    }
  }
}
