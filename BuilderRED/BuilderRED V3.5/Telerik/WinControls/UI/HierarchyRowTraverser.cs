// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HierarchyRowTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class HierarchyRowTraverser : ITraverser<GridViewRowInfo>, IEnumerator<GridViewRowInfo>, IDisposable, IEnumerator, IEnumerable
  {
    private GridViewRowInfo hierarchyRow;
    private GridViewRowInfo current;
    private int index;

    public HierarchyRowTraverser(GridViewRowInfo hierarchyRow)
    {
      this.hierarchyRow = hierarchyRow;
      this.Reset();
    }

    public HierarchyRowTraverser(HierarchyRowTraverser traverser)
      : this(traverser.Position)
    {
    }

    public HierarchyRowTraverser(
      HierarchyRowTraverser.HierarchyRowTraverserPosition position)
    {
      this.Position = position;
    }

    object ITraverser<GridViewRowInfo>.Position
    {
      get
      {
        return (object) this.Position;
      }
      set
      {
        this.Position = (HierarchyRowTraverser.HierarchyRowTraverserPosition) value;
      }
    }

    public bool MovePrevious()
    {
      while (this.MovePreviousCore())
      {
        if (this.OnRowVisible())
          return true;
      }
      return false;
    }

    public bool MoveToEnd()
    {
      if (this.index != this.RowsCount - 1)
      {
        this.index = this.RowsCount - 1;
        if (this.index >= 0)
        {
          this.SetCurrent();
          if (!this.OnRowVisible())
            this.MovePrevious();
          return true;
        }
        this.current = (GridViewRowInfo) null;
      }
      return false;
    }

    private bool MovePreviousCore()
    {
      if (!this.IsInValidState)
        return false;
      if (this.index > 0)
      {
        --this.index;
        this.SetCurrent();
        return true;
      }
      this.current = (GridViewRowInfo) null;
      this.index = -1;
      return false;
    }

    public GridViewRowInfo Current
    {
      get
      {
        return this.current;
      }
      internal set
      {
        this.current = value;
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
        if (this.OnRowVisible())
          return true;
      }
      return false;
    }

    private bool MoveNextCore()
    {
      if (!this.IsInValidState || this.index >= this.RowsCount - 1)
        return false;
      ++this.index;
      this.SetCurrent();
      return true;
    }

    public void Reset()
    {
      this.index = -1;
      this.current = (GridViewRowInfo) null;
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this;
    }

    public HierarchyRowTraverser.HierarchyRowTraverserPosition Position
    {
      get
      {
        return new HierarchyRowTraverser.HierarchyRowTraverserPosition(this.hierarchyRow, this.current, this.index);
      }
      set
      {
        if (value == null)
          return;
        this.hierarchyRow = value.HierarchyRow;
        this.current = value.Current;
        this.index = value.Index;
      }
    }

    public GridViewRowInfo HierarchyRow
    {
      get
      {
        return this.hierarchyRow;
      }
    }

    protected int Index
    {
      get
      {
        return this.index;
      }
    }

    private bool IsInValidState
    {
      get
      {
        if (this.hierarchyRow != null)
          return this.hierarchyRow.IsValid;
        return false;
      }
    }

    protected virtual int RowsCount
    {
      get
      {
        return this.hierarchyRow.ChildRows.Count;
      }
    }

    protected virtual void SetCurrent()
    {
      if (this.index >= this.hierarchyRow.ChildRows.Count)
        return;
      this.current = this.hierarchyRow.ChildRows[this.index];
    }

    public event RowEnumeratorEventHandler RowVisible;

    protected virtual bool OnRowVisible()
    {
      if (this.current.PinPosition != PinnedRowPosition.None)
        return false;
      if (this.RowVisible == null)
        return this.current.IsVisible;
      RowEnumeratorEventArgs e = new RowEnumeratorEventArgs(this.current);
      e.ProcessRow = this.current.IsVisible;
      this.RowVisible((object) this, e);
      return e.ProcessRow;
    }

    public class HierarchyRowTraverserPosition : IEquatable<HierarchyRowTraverser.HierarchyRowTraverserPosition>
    {
      private GridViewRowInfo hierarchyRow;
      private GridViewRowInfo current;
      private int index;

      public HierarchyRowTraverserPosition(
        GridViewRowInfo hierarchyRow,
        GridViewRowInfo current,
        int index)
      {
        this.hierarchyRow = hierarchyRow;
        this.current = current;
        this.index = index;
      }

      public GridViewRowInfo HierarchyRow
      {
        get
        {
          return this.hierarchyRow;
        }
      }

      public GridViewRowInfo Current
      {
        get
        {
          return this.current;
        }
      }

      public int Index
      {
        get
        {
          return this.index;
        }
      }

      public bool Equals(
        HierarchyRowTraverser.HierarchyRowTraverserPosition position)
      {
        if (position == null || position.index != this.index || position.current != this.current)
          return false;
        return position.hierarchyRow == this.hierarchyRow;
      }

      public override bool Equals(object obj)
      {
        return this.Equals(obj as HierarchyRowTraverser.HierarchyRowTraverserPosition);
      }

      public override int GetHashCode()
      {
        return base.GetHashCode();
      }
    }
  }
}
