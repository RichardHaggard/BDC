// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDataItemView
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
  public class GanttViewDataItemView : IList<GanttViewDataItem>, ICollection<GanttViewDataItem>, IEnumerable<GanttViewDataItem>, IEnumerable
  {
    internal const int StateAttached = 1;
    internal const int StateSorted = 2;
    internal const int StateFiltered = 4;
    internal const int StateModified = 8;
    internal const int StateReset = 16;
    internal const int StateItemAdded = 32;
    internal const int StateItemRemoved = 64;
    internal const int StateItemChanged = 128;
    internal const int StateItemMoved = 256;
    internal const int StateItemReplaced = 512;
    internal const int StateSuspend = 1024;
    private GanttViewDataItem owner;
    private IList<GanttViewDataItem> items;
    private IList<GanttViewDataItem> view;
    private BitVector32 state;
    private RBOrderedMultiTree<GanttViewDataItem> rbTree;

    public GanttViewDataItem this[int index]
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
          if (this.owner.GanttViewElement.IsSuspended)
            this.state[128] = true;
          else if (this.state[2] || this.state[4])
          {
            GanttViewDataItem ganttViewDataItem = this[index];
            index1 = this.items.IndexOf(ganttViewDataItem);
            this.view.Remove(ganttViewDataItem);
            this.view.Add(value);
          }
        }
        this.items[index1] = value;
      }
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

    private bool IsAttached
    {
      get
      {
        return this.owner.GanttViewElement != null;
      }
    }

    protected internal bool IsEmpty
    {
      get
      {
        return this.items.Count == 0;
      }
    }

    public GanttViewDataItemView(GanttViewDataItem owner)
    {
      this.owner = owner;
      this.view = this.items = (IList<GanttViewDataItem>) new List<GanttViewDataItem>();
    }

    public int IndexOf(GanttViewDataItem item)
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

    public void Insert(int index, GanttViewDataItem item)
    {
      if (this.IsAttached)
      {
        if (this.owner.GanttViewElement.IsSuspended)
          this.state[32] = true;
        else if (this.state[4] && this.state[2])
        {
          if (this.owner.GanttViewElement.PassesFilter(item))
            this.view.Add(item);
        }
        else if (this.state[2])
          this.view.Add(item);
        else if (this.state[4] && this.owner.GanttViewElement.PassesFilter(item))
        {
          if (index < this.view.Count)
            this.view.Insert(index, item);
          else
            this.view.Add(item);
        }
      }
      this.items.Insert(index, item);
    }

    public void Add(GanttViewDataItem item)
    {
      if (this.IsAttached)
      {
        if (this.owner.GanttViewElement.IsSuspended)
          this.state[128] = true;
        else if (this.state[4])
        {
          if (this.owner.GanttViewElement.PassesFilter(item))
            this.view.Add(item);
        }
        else if (this.state[2])
          this.view.Add(item);
      }
      this.items.Add(item);
    }

    public void RemoveAt(int index)
    {
      if (this.IsAttached)
      {
        if (this.owner.GanttViewElement.IsSuspended)
        {
          this.state[32] = true;
          if (this.view != this.items)
            index = this.items.IndexOf(this.view[index]);
        }
        else if (this.state[2] || this.state[4])
        {
          GanttViewDataItem ganttViewDataItem = this.view[index];
          index = this.items.IndexOf(ganttViewDataItem);
          this.view.Remove(ganttViewDataItem);
        }
      }
      this.items.RemoveAt(index);
    }

    public void Clear()
    {
      if (this.view != null && (this.state[2] || this.state[4]))
        this.view.Clear();
      this.items.Clear();
    }

    public bool Contains(GanttViewDataItem item)
    {
      return this.IndexOf(item) >= 0;
    }

    public void CopyTo(GanttViewDataItem[] array, int arrayIndex)
    {
      this.Attach();
      this.view.CopyTo(array, arrayIndex);
    }

    public bool Remove(GanttViewDataItem item)
    {
      int count = this.view.Count;
      int index = this.IndexOf(item);
      if (index >= 0)
        this.RemoveAt(index);
      return count != this.view.Count;
    }

    public IEnumerator<GanttViewDataItem> GetEnumerator()
    {
      this.Attach();
      return this.view.GetEnumerator();
    }

    public IGanttViewDataItemEnumerator GetGanttViewDataItemEnumerator()
    {
      this.Attach();
      return (IGanttViewDataItemEnumerator) new GanttViewDataItemView.GanttViewDataItemEnumerator(this);
    }

    public IGanttViewDataItemEnumerator GetGanttViewDataItemEnumerator(
      int position)
    {
      this.Attach();
      return (IGanttViewDataItemEnumerator) new GanttViewDataItemView.GanttViewDataItemEnumerator(this, position);
    }

    public IGanttViewDataItemEnumerator GetGanttViewDataItemEnumerator(
      GanttViewDataItem item)
    {
      this.Attach();
      return (IGanttViewDataItemEnumerator) new GanttViewDataItemView.GanttViewDataItemEnumerator(this, item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    private void Attach()
    {
      if (this.state[1] || !this.IsAttached)
        return;
      this.UpdateView();
      this.state[1] = true;
    }

    protected internal bool Update()
    {
      if (this.state.Data < 1 || this.owner.GanttViewElement == null || this.owner.GanttViewElement.IsSuspended)
        return false;
      return this.UpdateView();
    }

    protected internal bool UpdateView()
    {
      bool flag1 = this.owner.GanttViewElement.FilterDescriptors.Count != 0;
      if (!flag1)
      {
        this.state[4] = false;
        this.state[2] = false;
        this.view = this.items;
        return true;
      }
      this.rbTree = (RBOrderedMultiTree<GanttViewDataItem>) null;
      this.view = (IList<GanttViewDataItem>) new List<GanttViewDataItem>(this.items.Count);
      if (flag1)
      {
        for (int index = 0; index < this.items.Count; ++index)
        {
          if (this.owner.GanttViewElement.PassesFilter(this.items[index]))
            this.view.Add(this.items[index]);
        }
      }
      else
      {
        for (int index = 0; index < this.items.Count; ++index)
          this.view.Add(this.items[index]);
      }
      bool flag2 = this.state[1];
      this.state = new BitVector32();
      this.state[4] = flag1;
      this.state[1] = flag2;
      return true;
    }

    public class GanttViewDataItemEnumerator : IGanttViewDataItemEnumerator, IEnumerator<GanttViewDataItem>, IDisposable, IEnumerator
    {
      private object position;
      private GanttViewDataItem current;
      private GanttViewDataItemView items;
      private bool binaryMode;

      public GanttViewDataItemEnumerator(GanttViewDataItemView items)
      {
        this.items = items;
        this.binaryMode = !(this.items.view is List<RadTreeNode>);
        this.Reset();
      }

      public GanttViewDataItemEnumerator(GanttViewDataItemView items, int position)
        : this(items)
      {
        if (position < 0 || position >= this.items.Count)
          return;
        this.current = this.items[position];
        this.position = (object) position;
      }

      public GanttViewDataItemEnumerator(GanttViewDataItemView items, GanttViewDataItem item)
        : this(items)
      {
        if (this.binaryMode)
        {
          this.position = (object) this.items.rbTree.Find(item);
          if (this.position == null)
            return;
          this.current = item;
        }
        else
        {
          int num = this.items.IndexOf(item);
          if (num < 0)
            return;
          this.current = item;
          this.position = (object) num;
        }
      }

      public GanttViewDataItem Current
      {
        get
        {
          return this.current;
        }
      }

      public void Dispose()
      {
        this.items = (GanttViewDataItemView) null;
        this.position = (object) null;
        this.current = (GanttViewDataItem) null;
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
          RBOrderedTreeNode<GanttViewDataItem> rbOrderedTreeNode = this.items.rbTree.Next((RBOrderedTreeNode<GanttViewDataItem>) this.position);
          if (rbOrderedTreeNode != null)
          {
            this.current = rbOrderedTreeNode.Key;
            this.position = (object) rbOrderedTreeNode;
            return true;
          }
          this.current = (GanttViewDataItem) null;
          return false;
        }
        int index = (int) this.position + 1;
        if (index < this.items.Count)
        {
          this.current = this.items[index];
          this.position = (object) index;
          return true;
        }
        this.current = (GanttViewDataItem) null;
        return false;
      }

      public bool MovePrev()
      {
        if (this.position == null)
          return false;
        if (this.binaryMode)
        {
          RBOrderedTreeNode<GanttViewDataItem> rbOrderedTreeNode = this.items.rbTree.Previous((RBOrderedTreeNode<GanttViewDataItem>) this.position);
          if (rbOrderedTreeNode != null)
          {
            this.current = rbOrderedTreeNode.Key;
            this.position = (object) rbOrderedTreeNode;
            return true;
          }
          this.current = (GanttViewDataItem) null;
          return false;
        }
        int index = (int) this.position - 1;
        if (index >= 0 && index < this.items.Count)
        {
          this.current = this.items[index];
          this.position = (object) index;
          return true;
        }
        this.current = (GanttViewDataItem) null;
        return false;
      }

      public void Reset()
      {
        this.current = (GanttViewDataItem) null;
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
