// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GridTraverser : ITraverser<GridViewRowInfo>, IEnumerator<GridViewRowInfo>, IDisposable, IEnumerator, IEnumerable
  {
    private Stack<ITraverser<GridViewRowInfo>> hierarchyTraversers = new Stack<ITraverser<GridViewRowInfo>>();
    private IHierarchicalRow rootRow;
    private IHierarchicalRow hierarchyRow;
    private ITraverser<GridViewRowInfo> traverser;
    private bool processHierarchy;
    private bool overDetailsRow;
    private bool oldRowIsGroupRow;
    private GridTraverser.TraversalModes mode;
    private GridViewInfo viewInfo;

    public GridTraverser(IHierarchicalRow hierarchyRow)
    {
      this.rootRow = hierarchyRow;
      this.hierarchyRow = hierarchyRow;
      this.Reset();
    }

    public GridTraverser(GridTraverser gridTraverser)
    {
      this.rootRow = gridTraverser.hierarchyRow;
      this.hierarchyRow = gridTraverser.hierarchyRow;
      this.mode = gridTraverser.mode;
      this.overDetailsRow = gridTraverser.overDetailsRow;
      if (gridTraverser.traverser is ViewInfoTraverser)
      {
        this.traverser = (ITraverser<GridViewRowInfo>) new ViewInfoTraverser((ViewInfoTraverser.ViewInfoEnumeratorPosition) gridTraverser.traverser.Position);
      }
      else
      {
        if (!(gridTraverser.traverser is HierarchyRowTraverser))
          return;
        if (this.hierarchyRow is GridViewGroupRowInfo)
          this.traverser = (ITraverser<GridViewRowInfo>) new GroupRowTraverser((HierarchyRowTraverser.HierarchyRowTraverserPosition) gridTraverser.traverser.Position);
        else
          this.traverser = (ITraverser<GridViewRowInfo>) new HierarchyRowTraverser((HierarchyRowTraverser.HierarchyRowTraverserPosition) gridTraverser.traverser.Position);
      }
    }

    public GridTraverser(GridViewInfo viewInfo)
      : this(viewInfo, GridTraverser.TraversalModes.AllRows)
    {
    }

    public GridTraverser(GridViewInfo viewInfo, GridTraverser.TraversalModes type)
    {
      this.mode = type;
      this.hierarchyRow = viewInfo.ParentRow == null ? (IHierarchicalRow) viewInfo.ViewTemplate : (IHierarchicalRow) viewInfo.ParentRow;
      this.rootRow = this.hierarchyRow;
      this.viewInfo = viewInfo;
      this.Reset();
    }

    object ITraverser<GridViewRowInfo>.Position
    {
      get
      {
        return (object) this.Position;
      }
      set
      {
        this.Position = (GridTraverser.GridTraverserPosition) value;
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
      if (this.traverser == null || !this.traverser.MoveToEnd())
        return false;
      while (!this.OnRowVisible())
      {
        if (!this.traverser.MovePrevious())
          return false;
      }
      return true;
    }

    public GridViewRowInfo Current
    {
      get
      {
        if (!this.overDetailsRow)
          return this.traverser.Current;
        GridViewHierarchyRowInfo current = this.traverser.Current as GridViewHierarchyRowInfo;
        if (current != null)
          return (GridViewRowInfo) current.ChildRow;
        return (GridViewRowInfo) null;
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
        if (this.OnRowVisible())
          return true;
      }
      return false;
    }

    public void Reset()
    {
      this.hierarchyRow = this.rootRow;
      this.hierarchyTraversers.Clear();
      this.CreateTraverser(this.viewInfo);
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new GridTraverser(this);
    }

    public GridTraverser.GridTraverserPosition Position
    {
      get
      {
        return new GridTraverser.GridTraverserPosition(this.rootRow, this.hierarchyRow, this.traverser != null ? this.traverser.Position : (object) null, this.overDetailsRow, this.mode);
      }
      set
      {
        this.rootRow = value.RootRow;
        this.hierarchyRow = value.HierarchyRow;
        this.overDetailsRow = value.OverDetailsRow;
        this.mode = value.Mode;
        if (value.Position is ViewInfoTraverser.ViewInfoEnumeratorPosition)
        {
          this.traverser = (ITraverser<GridViewRowInfo>) new ViewInfoTraverser((ViewInfoTraverser.ViewInfoEnumeratorPosition) value.Position);
        }
        else
        {
          if (!(value.Position is HierarchyRowTraverser.HierarchyRowTraverserPosition))
            return;
          if (this.hierarchyRow is GridViewGroupRowInfo)
            this.traverser = (ITraverser<GridViewRowInfo>) new GroupRowTraverser((HierarchyRowTraverser.HierarchyRowTraverserPosition) value.Position);
          else
            this.traverser = (ITraverser<GridViewRowInfo>) new HierarchyRowTraverser((HierarchyRowTraverser.HierarchyRowTraverserPosition) value.Position);
        }
      }
    }

    public bool ProcessHierarchy
    {
      get
      {
        return this.processHierarchy;
      }
      set
      {
        this.processHierarchy = value;
      }
    }

    public GridTraverser.TraversalModes TraversalMode
    {
      get
      {
        return this.mode;
      }
      set
      {
        this.mode = value;
      }
    }

    protected ITraverser<GridViewRowInfo> Traverser
    {
      get
      {
        return this.traverser;
      }
    }

    public event RowEnumeratorEventHandler RowVisible;

    protected virtual bool OnRowVisible()
    {
      if (this.traverser.Current == null || this.TraversalMode == GridTraverser.TraversalModes.ScrollableRows && this.traverser.Current.PinPosition != PinnedRowPosition.None)
        return false;
      if (this.RowVisible == null)
        return true;
      RowEnumeratorEventArgs e = new RowEnumeratorEventArgs(this.traverser.Current);
      this.RowVisible((object) this, e);
      return e.ProcessRow;
    }

    protected virtual bool CanStepInHierarchy()
    {
      if (this.traverser.Current != null && this.traverser.Current.IsExpanded)
        return this.traverser.Current.HasChildRows();
      return false;
    }

    public int GetRowCount()
    {
      int num = 0;
      GridTraverser.GridTraverserPosition position = this.Position;
      this.Reset();
      while (this.MoveNext())
        ++num;
      this.Position = position;
      return num;
    }

    public int GoToRow(GridViewRowInfo row)
    {
      GridTraverser.GridTraverserPosition position = this.Position;
      int num = 0;
      this.Reset();
      while (this.MoveNext())
      {
        if (this.Current == row)
          return num;
        ++num;
      }
      this.Position = position;
      return -1;
    }

    public GridViewRowInfo GoToRowIndex(int rowIndex)
    {
      GridTraverser.GridTraverserPosition position = this.Position;
      int num = 0;
      this.Reset();
      while (this.MoveNext())
      {
        if (num == rowIndex)
          return this.Current;
        ++num;
      }
      this.Position = position;
      return (GridViewRowInfo) null;
    }

    public bool MoveForward(GridViewRowInfo row)
    {
      while (this.Current != row)
      {
        if (!this.MoveNext())
          return false;
      }
      return true;
    }

    public bool MoveBackward(GridViewRowInfo row)
    {
      while (this.Current != row)
      {
        if (!this.MovePrevious())
          return false;
      }
      return true;
    }

    private bool MoveNextCore()
    {
      if (this.traverser == null)
        return false;
      IHierarchicalRow hierarchyRow = this.hierarchyRow;
      ITraverser<GridViewRowInfo> traverser = this.traverser;
      if (this.StepInHierarchy())
        return true;
      while (!this.traverser.MoveNext())
      {
        if (!this.StepOutOfHierarchy())
        {
          this.hierarchyRow = hierarchyRow;
          this.traverser = traverser;
          return false;
        }
      }
      return true;
    }

    private bool MovePreviousCore()
    {
      if (this.traverser == null)
        return false;
      if (this.overDetailsRow)
      {
        this.overDetailsRow = false;
        return true;
      }
      if (!this.traverser.MovePrevious())
        return this.StepOutOfHierarchy();
      this.StepInHierarchyBackward();
      return true;
    }

    private bool StepInHierarchy()
    {
      if (this.overDetailsRow)
        this.overDetailsRow = false;
      else if (this.CanStepInHierarchy())
      {
        GridViewHierarchyRowInfo current = this.traverser.Current as GridViewHierarchyRowInfo;
        if (current != null && !this.processHierarchy && !this.IsSelfReference(current))
        {
          this.overDetailsRow = true;
          return true;
        }
        if (this.traverser.Current != null)
        {
          this.hierarchyRow = (IHierarchicalRow) this.traverser.Current;
          this.hierarchyTraversers.Push(this.traverser);
          this.CreateTraverser();
        }
      }
      return false;
    }

    private bool StepOutOfHierarchy()
    {
      GridViewHierarchyRowInfo hierarchyRow1 = this.hierarchyRow as GridViewHierarchyRowInfo;
      if (this.hierarchyRow is GridViewGroupRowInfo || this.hierarchyRow.HasChildViews || this.processHierarchy && (this.hierarchyRow.Parent != null || this.hierarchyRow is GridViewHierarchyRowInfo))
      {
        IHierarchicalRow hierarchyRow2 = this.hierarchyRow;
        this.oldRowIsGroupRow = hierarchyRow2 is GridViewGroupRowInfo;
        this.hierarchyRow = this.hierarchyRow.Parent;
        GridViewInfo gridViewInfo = this.Current != null ? this.Current.ViewInfo : (GridViewInfo) null;
        bool flag = this.Current is GridViewHierarchyRowInfo;
        if (this.hierarchyRow == null)
          this.hierarchyRow = this.rootRow;
        if (this.hierarchyTraversers.Count > 0)
        {
          ITraverser<GridViewRowInfo> traverser = this.hierarchyTraversers.Pop();
          traverser.MovePrevious();
          traverser.MovePrevious();
          for (int index = 0; index < 5; ++index)
          {
            traverser.MoveNext();
            if (traverser.Current == hierarchyRow2)
            {
              this.traverser = traverser;
              return this.ProcessHierarchy || hierarchyRow1 == null || (hierarchyRow1.ViewTemplate.Templates.Count <= 0 || !hierarchyRow1.ViewTemplate.IsSelfReference) || (gridViewInfo == null || hierarchyRow1.ViewTemplate == gridViewInfo.ViewTemplate) && (flag || hierarchyRow1 != this.rootRow);
            }
          }
        }
        this.CreateTraverser();
        while (this.traverser.MoveNext())
        {
          if (this.traverser.Current == hierarchyRow2)
            return this.ProcessHierarchy || hierarchyRow1 == null || (hierarchyRow1.ViewTemplate.Templates.Count <= 0 || !hierarchyRow1.ViewTemplate.IsSelfReference) || (gridViewInfo == null || hierarchyRow1.ViewTemplate == gridViewInfo.ViewTemplate) && (flag || hierarchyRow1 != this.rootRow);
        }
      }
      return false;
    }

    private void StepInHierarchyBackward()
    {
      while (this.CanStepInHierarchy())
      {
        GridViewHierarchyRowInfo current = this.traverser.Current as GridViewHierarchyRowInfo;
        if (current != null && !this.processHierarchy && !this.IsSelfReference(current))
        {
          this.overDetailsRow = true;
          break;
        }
        this.hierarchyRow = (IHierarchicalRow) this.traverser.Current;
        this.hierarchyTraversers.Push(this.traverser);
        this.CreateTraverser();
        this.traverser.MoveToEnd();
      }
    }

    private bool IsSelfReference(GridViewHierarchyRowInfo hierarchyRow)
    {
      if (hierarchyRow == null || hierarchyRow.ChildRows == null || hierarchyRow.ChildRows.Count > 0 && hierarchyRow.ChildRows[0].ViewTemplate != hierarchyRow.ViewTemplate || hierarchyRow.ChildRows.Count == 0 && hierarchyRow.ViewTemplate.Templates.Count > 0)
        return false;
      return hierarchyRow.ViewTemplate.IsSelfReference;
    }

    private void CreateTraverser()
    {
      this.CreateTraverser((GridViewInfo) null);
    }

    protected virtual void CreateTraverser(GridViewInfo viewInfo)
    {
      GridViewHierarchyRowInfo hierarchyRow = this.hierarchyRow as GridViewHierarchyRowInfo;
      if (this.hierarchyRow.HasChildViews)
      {
        if (hierarchyRow != null && !this.IsSelfReference(hierarchyRow))
        {
          this.oldRowIsGroupRow = false;
          this.traverser = (ITraverser<GridViewRowInfo>) new ViewInfoTraverser(viewInfo != null ? viewInfo : hierarchyRow.ActiveView);
        }
        else
          this.traverser = (ITraverser<GridViewRowInfo>) new HierarchyRowTraverser((GridViewRowInfo) this.hierarchyRow);
      }
      else if (hierarchyRow != null && (this.processHierarchy || this.hierarchyRow == this.rootRow || this.oldRowIsGroupRow))
      {
        this.oldRowIsGroupRow = false;
        this.traverser = (ITraverser<GridViewRowInfo>) new ViewInfoTraverser(viewInfo != null ? viewInfo : hierarchyRow.ActiveView);
      }
      else if (this.hierarchyRow is GridViewTemplate)
        this.traverser = (ITraverser<GridViewRowInfo>) new ViewInfoTraverser(((GridViewTemplate) this.hierarchyRow).MasterViewInfo);
      else if (this.hierarchyRow is GridViewGroupRowInfo)
        this.traverser = (ITraverser<GridViewRowInfo>) new GroupRowTraverser((GridViewGroupRowInfo) this.hierarchyRow);
      ViewInfoTraverser traverser = this.traverser as ViewInfoTraverser;
      if (traverser != null)
      {
        if (this.mode == GridTraverser.TraversalModes.TopPinnedRows)
        {
          traverser.FilterByPinPosition = true;
          traverser.FilteredPinPosition = PinnedRowPosition.Top;
        }
        else if (this.mode == GridTraverser.TraversalModes.BottomPinnedRows)
        {
          traverser.FilterByPinPosition = true;
          traverser.FilteredPinPosition = PinnedRowPosition.Bottom;
        }
        else if (this.mode == GridTraverser.TraversalModes.ScrollableRows)
        {
          traverser.FilterByPinPosition = true;
          traverser.FilteredPinPosition = PinnedRowPosition.None;
        }
      }
      this.traverser.Reset();
    }

    public static bool IsNewRowVisible(GridViewNewRowInfo newRow)
    {
      if (newRow.IsVisible && !newRow.ViewInfo.ViewTemplate.MasterTemplate.GridReadOnly && !newRow.ViewInfo.ViewTemplate.ReadOnly)
        return newRow.ViewInfo.ViewTemplate.AllowAddNewRow;
      return false;
    }

    public enum TraversalModes
    {
      AllRows,
      TopPinnedRows,
      ScrollableRows,
      BottomPinnedRows,
    }

    public class GridTraverserPosition : IEquatable<GridTraverser.GridTraverserPosition>
    {
      private IHierarchicalRow rootRow;
      private IHierarchicalRow hierarchyRow;
      private object position;
      private bool overDetailsRow;
      private GridTraverser.TraversalModes mode;

      public GridTraverserPosition(
        IHierarchicalRow rootRow,
        IHierarchicalRow hierarchyRow,
        object position,
        bool overDetailsRow,
        GridTraverser.TraversalModes mode)
      {
        this.rootRow = rootRow;
        this.hierarchyRow = hierarchyRow;
        this.position = position;
        this.overDetailsRow = overDetailsRow;
        this.mode = mode;
      }

      public IHierarchicalRow RootRow
      {
        get
        {
          return this.rootRow;
        }
      }

      public IHierarchicalRow HierarchyRow
      {
        get
        {
          return this.hierarchyRow;
        }
      }

      public object Position
      {
        get
        {
          return this.position;
        }
      }

      public bool OverDetailsRow
      {
        get
        {
          return this.overDetailsRow;
        }
      }

      public GridTraverser.TraversalModes Mode
      {
        get
        {
          return this.mode;
        }
        set
        {
          this.mode = value;
        }
      }

      public bool Equals(GridTraverser.GridTraverserPosition position)
      {
        if (position == null || position.RootRow != this.RootRow || position.HierarchyRow != this.HierarchyRow)
          return false;
        return object.Equals(position.Position, this.Position);
      }

      public override bool Equals(object obj)
      {
        return this.Equals(obj as GridTraverser.GridTraverserPosition);
      }

      public override int GetHashCode()
      {
        return base.GetHashCode();
      }
    }
  }
}
