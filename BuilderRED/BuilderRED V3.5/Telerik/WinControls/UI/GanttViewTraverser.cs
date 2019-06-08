// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  public class GanttViewTraverser : ITraverser<GanttViewDataItem>, IEnumerator<GanttViewDataItem>, IDisposable, IEnumerator, IEnumerable
  {
    private int currentIndex = -1;
    private bool traverseAllItems;
    private RadGanttViewElement owner;
    private GanttViewDataItem current;
    private GanttViewTraverser enumerator;

    public GanttViewTraverser(RadGanttViewElement owner)
    {
      this.owner = owner;
    }

    public bool TraverseAllItems
    {
      get
      {
        return this.traverseAllItems;
      }
      set
      {
        this.traverseAllItems = value;
      }
    }

    public event GanttViewTraversingEventHandler Traversing;

    protected virtual void OnTraversing(GanttViewTraversingEventArgs e)
    {
      if (this.Traversing == null)
        return;
      this.Traversing((object) this, e);
    }

    protected bool OnTraversing()
    {
      if (this.Traversing == null)
        return true;
      GanttViewTraversingEventArgs e = new GanttViewTraversingEventArgs(this.current);
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
        this.current = value as GanttViewDataItem;
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

    public bool MoveToEnd()
    {
      if (!this.MoveNext())
        return false;
      GanttViewDataItem ganttViewDataItem = (GanttViewDataItem) null;
      while (this.MoveNext())
        ganttViewDataItem = this.Current;
      this.current = ganttViewDataItem;
      return true;
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
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.current;
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

    public void Reset()
    {
      this.current = (GanttViewDataItem) null;
      this.currentIndex = -1;
    }

    public IEnumerator GetEnumerator()
    {
      if (this.enumerator == null)
        this.enumerator = new GanttViewTraverser(this.owner);
      this.enumerator.Position = (object) this.current;
      return (IEnumerator) this.enumerator;
    }

    public bool MoveTo(GanttViewDataItem item)
    {
      this.Reset();
      while (this.MoveNext())
      {
        if (this.Current == item)
          return true;
      }
      return false;
    }

    public virtual bool MoveNextCore()
    {
      int num = 0;
      if (this.current == null || this.current.Items != null && (this.current.Expanded || this.TraverseAllItems))
      {
        IEnumerator<GanttViewDataItem> enumerator = (this.current == null ? (Collection<GanttViewDataItem>) this.owner.Items : (Collection<GanttViewDataItem>) this.current.Items).GetEnumerator();
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
        for (GanttViewDataItem parent = this.Current.parent; parent != null; parent = parent.parent)
        {
          for (int index = this.currentIndex + 1; index >= 0 && index < parent.Items.Count; ++index)
          {
            GanttViewDataItem ganttViewDataItem = parent.Items[index];
            if (ganttViewDataItem.Visible || this.owner.IsInDesignMode)
            {
              this.current = ganttViewDataItem;
              this.currentIndex = index;
              return true;
            }
          }
          this.currentIndex = parent.Index;
        }
      }
      this.current = (GanttViewDataItem) null;
      return false;
    }

    public virtual bool MovePreviousCore()
    {
      int num = 0;
      if (this.current == null)
      {
        IEnumerator<GanttViewDataItem> enumerator = this.owner.Items.GetEnumerator();
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
        GanttViewDataItem parent = this.Current.Parent == null ? this.owner.Root.Items[index] : this.Current.Parent.Items[index];
        if (parent.Visible || this.owner.IsInDesignMode)
        {
          if (parent.Expanded || this.TraverseAllItems)
          {
            GanttViewDataItem lastVisibleItem = this.GetLastVisibleItem(parent);
            if (lastVisibleItem != null)
            {
              this.current = lastVisibleItem;
              this.currentIndex = lastVisibleItem.Index;
              return true;
            }
          }
          this.current = parent;
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

    private GanttViewDataItem GetLastVisibleItem(GanttViewDataItem parent)
    {
      for (int index = parent.Items.Count - 1; index >= 0; --index)
      {
        GanttViewDataItem parent1 = parent.Items[index];
        if (parent1.Visible || this.owner.IsInDesignMode)
        {
          if ((parent1.Expanded || this.TraverseAllItems) && parent1.Items.Count > 0)
            return this.GetLastVisibleItem(parent1);
          return parent1;
        }
      }
      return (GanttViewDataItem) null;
    }
  }
}
