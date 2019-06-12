// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowInfoEnumerator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GridViewRowInfoEnumerator : ITraverser<GridViewRowInfo>, IEnumerator<GridViewRowInfo>, IDisposable, IEnumerator, IEnumerable
  {
    private IHierarchicalRow start;
    private GridViewRowInfoEnumerator.EnumeratorPosition position;
    private GridViewRowInfo current;
    private GridViewRowInfoEnumerator enumerator;

    public GridViewRowInfoEnumerator(IHierarchicalRow startPosition)
    {
      this.start = startPosition;
      this.position = new GridViewRowInfoEnumerator.EnumeratorPosition(this.start, -1);
    }

    public event RowEnumeratorEventHandler RowVisible;

    public event RowEnumeratorEventHandler StepInHierarchy;

    public object Position
    {
      get
      {
        return (object) new GridViewRowInfoEnumerator.EnumeratorPosition(this.position);
      }
      set
      {
        this.position = new GridViewRowInfoEnumerator.EnumeratorPosition((GridViewRowInfoEnumerator.EnumeratorPosition) value);
        this.current = this.position.Row;
      }
    }

    public bool MovePrevious()
    {
      while (this.MovePreviousCore())
      {
        if (this.OnRowVisible(this.current))
          return true;
      }
      return false;
    }

    public bool MoveToEnd()
    {
      throw new NotImplementedException();
    }

    public GridViewRowInfo Current
    {
      get
      {
        return this.current;
      }
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
        if (this.OnRowVisible(this.current))
          return true;
      }
      return false;
    }

    public void Reset()
    {
      this.position.Parent = this.start;
      this.position.Index = -1;
      this.current = (GridViewRowInfo) null;
    }

    public IEnumerator GetEnumerator()
    {
      if (this.enumerator == null)
      {
        this.enumerator = new GridViewRowInfoEnumerator(this.start);
        this.enumerator.RowVisible += new RowEnumeratorEventHandler(this.Enumerator_RowVisible);
        this.enumerator.StepInHierarchy += new RowEnumeratorEventHandler(this.Enumerator_StepInHierarchy);
      }
      this.enumerator.Position = (object) this.position;
      return (IEnumerator) this.enumerator;
    }

    private void Enumerator_StepInHierarchy(object sender, RowEnumeratorEventArgs e)
    {
      e.ProcessRow = this.OnStepInHierarchy(e.Row);
    }

    private void Enumerator_RowVisible(object sender, RowEnumeratorEventArgs e)
    {
      e.ProcessRow = this.OnRowVisible(e.Row);
    }

    public void Dispose()
    {
    }

    protected virtual bool OnRowVisible(GridViewRowInfo row)
    {
      if (this.RowVisible == null)
        return true;
      RowEnumeratorEventArgs e = new RowEnumeratorEventArgs(row);
      this.RowVisible((object) this, e);
      return e.ProcessRow;
    }

    protected virtual bool OnStepInHierarchy(GridViewRowInfo row)
    {
      if (row.ChildRows == null || row.ChildRows.Count == 0)
        return false;
      if (this.StepInHierarchy == null)
        return row.IsExpanded;
      RowEnumeratorEventArgs e = new RowEnumeratorEventArgs(row);
      e.ProcessRow = row.IsExpanded;
      this.StepInHierarchy((object) this, e);
      return e.ProcessRow;
    }

    private bool MoveNextCore()
    {
      GridViewRowInfoEnumerator.EnumeratorPosition position = this.Position as GridViewRowInfoEnumerator.EnumeratorPosition;
      IHierarchicalRow parent;
      for (; this.position.Parent != null; this.position.Parent = parent)
      {
        ++this.position.Index;
        if (this.position.Index < this.position.Parent.ChildRows.Count)
        {
          this.current = this.position.Parent.ChildRows[this.position.Index];
          if (this.OnStepInHierarchy(this.current))
          {
            this.position.Parent = (IHierarchicalRow) this.current;
            this.position.Index = -1;
          }
          return true;
        }
        parent = this.position.Parent.Parent;
        if (parent != null)
          this.position.Index = parent.ChildRows.IndexOf((GridViewRowInfo) this.position.Parent);
      }
      this.position = position;
      return false;
    }

    private bool MovePreviousCore()
    {
      if (this.position.Index == 0 && this.position.Parent != null && this.position.Parent.Parent != null)
      {
        IHierarchicalRow parent = this.position.Parent.Parent;
        this.position.Index = parent.ChildRows.IndexOf((GridViewRowInfo) this.position.Parent);
        this.position.Parent = parent;
        this.current = this.position.Parent.ChildRows[this.position.Index];
        return true;
      }
      if (this.position.Index > 0)
      {
        --this.position.Index;
        for (this.current = this.position.Parent.ChildRows[this.position.Index]; this.OnStepInHierarchy(this.current); this.current = this.position.Parent.ChildRows[this.position.Index])
        {
          this.position.Parent = (IHierarchicalRow) this.current;
          this.position.Index = this.position.Parent.ChildRows.Count - 1;
        }
        return true;
      }
      this.position.Index = -1;
      return false;
    }

    public class EnumeratorPosition : IEquatable<GridViewRowInfoEnumerator.EnumeratorPosition>
    {
      private IHierarchicalRow parent;
      private int index;

      public EnumeratorPosition(IHierarchicalRow parent, int index)
      {
        this.parent = parent;
        this.index = index;
      }

      public EnumeratorPosition(
        GridViewRowInfoEnumerator.EnumeratorPosition position)
      {
        this.parent = position.parent;
        this.index = position.index;
      }

      public int Index
      {
        get
        {
          return this.index;
        }
        set
        {
          this.index = value;
        }
      }

      public IHierarchicalRow Parent
      {
        get
        {
          return this.parent;
        }
        set
        {
          this.parent = value;
        }
      }

      public GridViewRowInfo Row
      {
        get
        {
          if (this.index == -1)
            return this.parent as GridViewRowInfo;
          return this.parent.ChildRows[this.index];
        }
      }

      public bool Equals(
        GridViewRowInfoEnumerator.EnumeratorPosition position)
      {
        if (position != null && position.Index == this.Index && position.Parent == this.Parent)
          return position.Row == this.Row;
        return false;
      }

      public override bool Equals(object obj)
      {
        return this.Equals(obj as GridViewRowInfoEnumerator.EnumeratorPosition);
      }

      public override int GetHashCode()
      {
        return base.GetHashCode();
      }
    }
  }
}
