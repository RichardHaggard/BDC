// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class TreeNodeView : IList<RadTreeNode>, ICollection<RadTreeNode>, IEnumerable<RadTreeNode>, IEnumerable
  {
    internal const int StateAttached = 1;
    internal const int StateSorted = 2;
    internal const int StateFiltered = 4;
    internal const int StateModified = 8;
    internal const int StateReset = 16;
    internal const int StateNodeAdded = 32;
    internal const int StateNodeRemoved = 64;
    internal const int StateNodeChanged = 128;
    internal const int StateNodeMoved = 256;
    internal const int StateNodeReplaced = 512;
    internal const int StateSuspend = 1024;
    private RadTreeNode owner;
    private IList<RadTreeNode> nodes;
    private IList<RadTreeNode> view;
    private BitVector32 state;
    private RBOrderedMultiTree<RadTreeNode> rbTree;

    public TreeNodeView(RadTreeNode owner)
    {
      this.owner = owner;
      this.view = this.nodes = (IList<RadTreeNode>) new List<RadTreeNode>();
    }

    public int IndexOf(RadTreeNode item)
    {
      this.Attach();
      int index = this.view.IndexOf(item);
      if (index == -1)
        index = 0;
      if (index >= 0)
      {
        for (; index < this.view.Count; ++index)
        {
          if (item == this.view[index])
            return index;
        }
        index = -1;
      }
      return index;
    }

    public void Insert(int index, RadTreeNode item)
    {
      if (this.IsAttached)
      {
        if (this.owner.treeView.IsSuspended)
          this.state[32] = true;
        if (this.state[4] && this.state[2])
        {
          if (this.owner.treeView.PassesFilter(item))
            this.view.Add(item);
        }
        else if (this.state[2])
          this.view.Add(item);
        else if (this.state[4] && this.owner.treeView.PassesFilter(item))
        {
          if (index < this.view.Count)
            this.view.Insert(index, item);
          else
            this.view.Add(item);
        }
      }
      this.nodes.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      if (this.IsAttached)
      {
        if (this.owner.treeView.IsSuspended)
          this.state[64] = true;
        if (this.state[2] || this.state[4] || this.view != this.nodes)
        {
          RadTreeNode radTreeNode = this.view[index];
          index = this.nodes.IndexOf(radTreeNode);
          this.view.Remove(radTreeNode);
        }
      }
      this.nodes.RemoveAt(index);
    }

    public RadTreeNode this[int index]
    {
      get
      {
        this.Attach();
        return this.view[index];
      }
      set
      {
        int index1 = index;
        if (this.IsAttached)
        {
          if (this.owner.treeView.IsSuspended)
            this.state[128] = true;
          else if (this.state[2] || this.state[4])
          {
            RadTreeNode radTreeNode = this[index];
            index1 = this.nodes.IndexOf(radTreeNode);
            this.view.Remove(radTreeNode);
            this.view.Add(value);
          }
        }
        this.nodes[index1] = value;
      }
    }

    public void Add(RadTreeNode item)
    {
      if (this.IsAttached)
      {
        if (this.owner.treeView.IsSuspended)
          this.state[128] = true;
        else if (this.state[4])
        {
          if (this.owner.treeView.PassesFilter(item))
            this.view.Add(item);
        }
        else if (this.state[2])
          this.view.Add(item);
      }
      this.nodes.Add(item);
    }

    public void Clear()
    {
      if (this.view != null && (this.state[2] || this.state[4]))
        this.view.Clear();
      this.nodes.Clear();
    }

    public bool Contains(RadTreeNode item)
    {
      return this.IndexOf(item) >= 0;
    }

    public void CopyTo(RadTreeNode[] array, int arrayIndex)
    {
      this.Attach();
      this.view.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        this.Attach();
        return this.view.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool Remove(RadTreeNode item)
    {
      int count = this.view.Count;
      int index = this.IndexOf(item);
      if (index >= 0)
        this.RemoveAt(index);
      return count != this.view.Count;
    }

    public IEnumerator<RadTreeNode> GetEnumerator()
    {
      this.Attach();
      return this.view.GetEnumerator();
    }

    public ITreeNodeEnumerator GetNodeEnumerator()
    {
      this.Attach();
      return (ITreeNodeEnumerator) new TreeNodeView.TreeNodeEnumerator(this);
    }

    public ITreeNodeEnumerator GetNodeEnumerator(int position)
    {
      this.Attach();
      return (ITreeNodeEnumerator) new TreeNodeView.TreeNodeEnumerator(this, position);
    }

    public ITreeNodeEnumerator GetNodeEnumerator(RadTreeNode node)
    {
      this.Attach();
      return (ITreeNodeEnumerator) new TreeNodeView.TreeNodeEnumerator(this, node);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    private bool IsAttached
    {
      get
      {
        return this.owner.treeView != null;
      }
    }

    protected internal bool Update()
    {
      if (this.state.Data < 1 || this.owner.treeView == null || this.owner.treeView.IsSuspended)
        return false;
      return this.UpdateView();
    }

    protected internal bool UpdateView()
    {
      bool flag1 = this.owner.treeView.SortDescriptors.Count != 0;
      bool flag2 = this.owner.treeView.FilterDescriptors.Count != 0;
      if (!flag1 && !flag2)
      {
        this.state[4] = false;
        this.state[2] = false;
        this.view = this.nodes;
        return true;
      }
      if (flag1)
      {
        this.rbTree = new RBOrderedMultiTree<RadTreeNode>(this.owner.treeView.Comparer);
        this.view = this.rbTree.Collection;
      }
      else
      {
        this.rbTree = (RBOrderedMultiTree<RadTreeNode>) null;
        this.view = (IList<RadTreeNode>) new List<RadTreeNode>(this.nodes.Count);
      }
      if (flag2)
      {
        for (int index = 0; index < this.nodes.Count; ++index)
        {
          if (this.owner.treeView.PassesFilter(this.nodes[index]))
            this.view.Add(this.nodes[index]);
        }
      }
      else
      {
        for (int index = 0; index < this.nodes.Count; ++index)
          this.view.Add(this.nodes[index]);
      }
      bool flag3 = this.state[1];
      this.state = new BitVector32();
      this.state[2] = flag1;
      this.state[4] = flag2;
      this.state[1] = flag3;
      return true;
    }

    private void Attach()
    {
      if (this.state[1] || !this.IsAttached)
        return;
      this.UpdateView();
      this.state[1] = true;
    }

    protected internal bool IsEmpty
    {
      get
      {
        return this.nodes.Count == 0;
      }
    }

    public class TreeNodeEnumerator : ITreeNodeEnumerator, IEnumerator<RadTreeNode>, IDisposable, IEnumerator
    {
      private object position;
      private RadTreeNode current;
      private TreeNodeView items;
      private bool binaryMode;

      public TreeNodeEnumerator(TreeNodeView items)
      {
        this.items = items;
        this.binaryMode = !(this.items.view is List<RadTreeNode>);
        this.Reset();
      }

      public TreeNodeEnumerator(TreeNodeView items, int position)
        : this(items)
      {
        if (position < 0 || position >= this.items.Count)
          return;
        this.current = this.items[position];
        this.position = (object) position;
      }

      public TreeNodeEnumerator(TreeNodeView items, RadTreeNode node)
        : this(items)
      {
        if (this.binaryMode)
        {
          this.position = (object) this.items.rbTree.Find(node);
          if (this.position == null)
            return;
          this.current = node;
        }
        else
        {
          int num = this.items.IndexOf(node);
          if (num < 0)
            return;
          this.current = node;
          this.position = (object) num;
        }
      }

      public RadTreeNode Current
      {
        get
        {
          return this.current;
        }
      }

      public void Dispose()
      {
        this.items = (TreeNodeView) null;
        this.position = (object) null;
        this.current = (RadTreeNode) null;
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public bool MoveNext()
      {
        if (this.position == null)
          return false;
        if (this.binaryMode)
        {
          RBOrderedTreeNode<RadTreeNode> rbOrderedTreeNode = this.items.rbTree.Next((RBOrderedTreeNode<RadTreeNode>) this.position);
          if (rbOrderedTreeNode != null)
          {
            this.current = rbOrderedTreeNode.Key;
            this.position = (object) rbOrderedTreeNode;
            return true;
          }
          this.current = (RadTreeNode) null;
          return false;
        }
        int index = (int) this.position + 1;
        if (index < this.items.Count)
        {
          this.current = this.items[index];
          this.position = (object) index;
          return true;
        }
        this.current = (RadTreeNode) null;
        return false;
      }

      public bool MovePrev()
      {
        if (this.position == null)
          return false;
        if (this.binaryMode)
        {
          RBOrderedTreeNode<RadTreeNode> rbOrderedTreeNode = this.items.rbTree.Previous((RBOrderedTreeNode<RadTreeNode>) this.position);
          if (rbOrderedTreeNode != null)
          {
            this.current = rbOrderedTreeNode.Key;
            this.position = (object) rbOrderedTreeNode;
            return true;
          }
          this.current = (RadTreeNode) null;
          return false;
        }
        int index = (int) this.position - 1;
        if (index >= 0 && index < this.items.Count)
        {
          this.current = this.items[index];
          this.position = (object) index;
          return true;
        }
        this.current = (RadTreeNode) null;
        return false;
      }

      public void Reset()
      {
        this.current = (RadTreeNode) null;
        if (this.items == null || this.items.Count == 0)
          return;
        if (this.binaryMode)
          this.position = (object) this.items.rbTree.Find(this.items[0]);
        else
          this.position = (object) -1;
      }
    }
  }
}
