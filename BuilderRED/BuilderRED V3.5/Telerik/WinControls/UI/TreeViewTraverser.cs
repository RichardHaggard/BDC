// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  public class TreeViewTraverser : ITraverser<RadTreeNode>, IEnumerator<RadTreeNode>, IDisposable, IEnumerator, IEnumerable
  {
    private int currentIndex = -1;
    private RadTreeViewElement owner;
    private RadTreeNode current;
    private TreeViewTraverser enumerator;

    public TreeViewTraverser(RadTreeViewElement owner)
    {
      this.owner = owner;
    }

    public TreeViewTraverser(RadTreeViewElement owner, RadTreeNode position)
      : this(owner)
    {
      this.Position = (object) position;
    }

    public event TreeViewTraversingEventHandler Traversing;

    protected virtual void OnTraversing(TreeViewTraversingEventArgs e)
    {
      TreeViewTraversingEventHandler traversing = this.Traversing;
      if (traversing == null)
        return;
      traversing((object) this, e);
    }

    protected bool OnTraversing()
    {
      if (this.Traversing == null)
        return true;
      TreeViewTraversingEventArgs e = new TreeViewTraversingEventArgs(this.current);
      this.OnTraversing(e);
      return e.Process;
    }

    public object Position
    {
      get
      {
        return (object) this.current;
      }
      set
      {
        this.current = value as RadTreeNode;
        if (this.current == null)
          return;
        this.currentIndex = this.current.Index;
      }
    }

    public bool MovePrevious()
    {
      while (this.MovePreviousCore())
      {
        if (this.OnTraversing())
          return true;
      }
      return false;
    }

    protected virtual bool MovePreviousCore()
    {
      int num = 0;
      if (this.current == null)
      {
        IEnumerator<RadTreeNode> enumerator = this.owner.Nodes.GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Visible || this.owner.IsInDesignMode)
          {
            this.current = enumerator.Current;
            this.currentIndex = num;
            return true;
          }
          ++num;
        }
        return false;
      }
      for (int index = this.currentIndex - 1; index >= 0; --index)
      {
        RadTreeNode node = this.current.parent.Nodes[index];
        if (node.Visible || this.owner.IsInDesignMode)
        {
          if (node.Expanded)
          {
            RadTreeNode lastVisibleNode = this.GetLastVisibleNode(node);
            if (lastVisibleNode != null)
            {
              this.current = lastVisibleNode;
              this.currentIndex = lastVisibleNode.Index;
              return true;
            }
          }
          this.current = node;
          this.currentIndex = index;
          return true;
        }
      }
      if (this.current.parent == this.owner.Root)
      {
        this.Reset();
        return false;
      }
      this.current = this.current.parent;
      if (this.current == null)
        return false;
      this.currentIndex = this.current.Index;
      return true;
    }

    private RadTreeNode GetLastVisibleNode(RadTreeNode parent)
    {
      for (int index = parent.Nodes.Count - 1; index >= 0; --index)
      {
        RadTreeNode node = parent.Nodes[index];
        if (node.Visible || this.owner.IsInDesignMode)
        {
          if (node.Expanded && node.Nodes.Count > 0)
            return this.GetLastVisibleNode(node);
          return node;
        }
      }
      return (RadTreeNode) null;
    }

    public bool MoveToEnd()
    {
      if (this.owner.Nodes.Count <= 0)
        return false;
      for (int index = this.owner.Nodes.Count - 1; index >= 0; --index)
      {
        this.current = this.owner.Nodes[index];
        if (this.OnTraversing())
          return true;
      }
      return false;
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
      while (this.MoveNextCore())
      {
        if (this.OnTraversing())
          return true;
      }
      return false;
    }

    protected virtual bool MoveNextCore()
    {
      int num = 0;
      if (this.current == null || this.current.nodes != null && this.current.Expanded)
      {
        IEnumerator<RadTreeNode> enumerator = (this.current == null ? (Collection<RadTreeNode>) this.owner.Nodes : (Collection<RadTreeNode>) this.current.nodes).GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Visible || this.owner.IsInDesignMode)
          {
            this.current = enumerator.Current;
            this.currentIndex = num;
            return true;
          }
          ++num;
        }
      }
      if (this.current != null)
      {
        for (RadTreeNode parent = this.current.parent; parent != null; parent = parent.parent)
        {
          for (int index = this.currentIndex + 1; index >= 0 && index < parent.nodes.Count; ++index)
          {
            RadTreeNode node = parent.nodes[index];
            if (node.Visible || this.owner.IsInDesignMode)
            {
              this.current = node;
              this.currentIndex = index;
              return true;
            }
          }
          this.currentIndex = parent.Index;
        }
      }
      this.current = (RadTreeNode) null;
      return false;
    }

    public void Reset()
    {
      this.current = (RadTreeNode) null;
      this.currentIndex = -1;
    }

    public IEnumerator GetEnumerator()
    {
      if (this.enumerator == null)
        this.enumerator = new TreeViewTraverser(this.owner);
      this.enumerator.Position = (object) this.current;
      return (IEnumerator) this.enumerator;
    }

    public bool MoveForward(RadTreeNode node)
    {
      while (this.Current != node)
      {
        if (!this.MoveNext())
          return false;
      }
      return true;
    }

    public bool MoveBackward(RadTreeNode node)
    {
      while (this.Current != node)
      {
        if (!this.MovePrevious())
          return false;
      }
      return true;
    }

    public void MoveTo(int index)
    {
      this.Reset();
      while (index != 0 && this.MoveNext())
        ++index;
    }

    public int MoveTo(RadTreeNode item)
    {
      int num = -1;
      this.Reset();
      while (this.MoveNext())
      {
        ++num;
        if (this.Current == item)
          return num;
      }
      return -1;
    }
  }
}
