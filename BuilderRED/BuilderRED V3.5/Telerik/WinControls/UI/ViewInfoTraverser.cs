// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ViewInfoTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class ViewInfoTraverser : ITraverser<GridViewRowInfo>, IEnumerator<GridViewRowInfo>, IDisposable, IEnumerator, IEnumerable
  {
    private GridViewInfo viewInfo;
    private GridViewRowInfo current;
    private ITraversable collection;
    private ViewInfoTraverser.Stages stage;
    private PinnedRowPosition pinPosition;
    private SystemRowPosition rowPosition;
    private int index;
    private PinnedRowPosition filteredPinPosition;
    private bool filterByPinPosition;
    private bool processHiddenRows;

    public ViewInfoTraverser(GridViewInfo viewInfo)
    {
      this.viewInfo = viewInfo;
      this.Reset();
    }

    public ViewInfoTraverser(ViewInfoTraverser traverser)
      : this(traverser.Position)
    {
    }

    public ViewInfoTraverser(
      ViewInfoTraverser.ViewInfoEnumeratorPosition position)
    {
      this.Position = position;
    }

    protected ITraversable Collection
    {
      get
      {
        if (this.collection == null)
          this.SetCollectionForStage();
        return this.collection;
      }
    }

    bool ITraverser<GridViewRowInfo>.MovePrevious()
    {
      return this.MovePrevious();
    }

    object ITraverser<GridViewRowInfo>.Position
    {
      get
      {
        return (object) this.Position;
      }
      set
      {
        this.Position = (ViewInfoTraverser.ViewInfoEnumeratorPosition) value;
      }
    }

    public bool MoveToEnd()
    {
      this.stage = !this.filterByPinPosition || this.filteredPinPosition != PinnedRowPosition.Top ? (!this.filterByPinPosition || this.filteredPinPosition != PinnedRowPosition.None ? ViewInfoTraverser.Stages.BottomPinnedSystemRows : ViewInfoTraverser.Stages.BottomSystemRows) : ViewInfoTraverser.Stages.TopPinnedRows;
      this.SetCollectionForStage();
      this.pinPosition = PinnedRowPosition.Bottom;
      this.rowPosition = SystemRowPosition.Bottom;
      do
        ;
      while (this.Collection.Count == 0 && this.ChangeCollectionBackward());
      if (this.Collection.Count > 0)
      {
        this.index = this.Collection.Count - 1;
        this.current = (GridViewRowInfo) this.Collection[this.index];
        if (!this.OnRowVisible())
          return this.MovePrevious();
        return true;
      }
      this.index = -1;
      this.current = (GridViewRowInfo) null;
      return false;
    }

    public GridViewRowInfo Current
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
      ViewInfoTraverser.ViewInfoEnumeratorPosition position = this.Position;
      while (this.MoveNextCore())
      {
        if (this.OnRowVisible())
          return true;
      }
      this.Position = position;
      return false;
    }

    public void Reset()
    {
      this.stage = !this.filterByPinPosition || this.filteredPinPosition != PinnedRowPosition.Bottom ? (!this.filterByPinPosition || this.filteredPinPosition != PinnedRowPosition.None ? ViewInfoTraverser.Stages.TopPinnedSystemRows : ViewInfoTraverser.Stages.TopSystemRows) : ViewInfoTraverser.Stages.BottomPinnedRows;
      this.SetCollectionForStage(false);
      this.collection = (ITraversable) null;
      this.index = -1;
      this.current = (GridViewRowInfo) null;
    }

    public IEnumerator GetEnumerator()
    {
      ViewInfoTraverser viewInfoTraverser = new ViewInfoTraverser(this.Position);
      viewInfoTraverser.Reset();
      return (IEnumerator) viewInfoTraverser;
    }

    public ViewInfoTraverser.ViewInfoEnumeratorPosition Position
    {
      get
      {
        return new ViewInfoTraverser.ViewInfoEnumeratorPosition(this.viewInfo, this.stage, this.Collection, this.index, this.pinPosition, this.rowPosition, this.filteredPinPosition, this.filterByPinPosition);
      }
      set
      {
        this.stage = value.Stage;
        this.viewInfo = value.ViewInfo;
        this.collection = value.Collection;
        this.index = value.Index;
        this.current = value.Current;
        this.pinPosition = value.PinPosition;
        this.rowPosition = value.RowPosition;
        this.filteredPinPosition = value.FilteredPinPosition;
        this.filterByPinPosition = value.FilterByPinPosition;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }

    public PinnedRowPosition FilteredPinPosition
    {
      get
      {
        return this.filteredPinPosition;
      }
      set
      {
        this.filteredPinPosition = value;
      }
    }

    public bool FilterByPinPosition
    {
      get
      {
        return this.filterByPinPosition;
      }
      set
      {
        this.filterByPinPosition = value;
      }
    }

    public bool ProcessHiddenRows
    {
      get
      {
        return this.processHiddenRows;
      }
      set
      {
        this.processHiddenRows = value;
      }
    }

    public event RowEnumeratorEventHandler RowVisible;

    protected virtual bool OnRowVisible()
    {
      if (this.current == null)
        return false;
      bool flag = (this.processHiddenRows || this.current.IsVisible) && this.pinPosition == this.current.PinPosition;
      if (this.pinPosition == PinnedRowPosition.None)
      {
        GridViewSystemRowInfo current = this.current as GridViewSystemRowInfo;
        if (current != null && current.RowPosition != this.rowPosition)
          flag = false;
      }
      if (this.viewInfo.ViewTemplate.GroupDescriptors.Count > 0 && this.current is GridViewSummaryRowInfo && !this.viewInfo.ViewTemplate.ShowTotals)
        flag = false;
      if (this.current is GridViewTableHeaderRowInfo)
        flag = flag && this.viewInfo.ViewTemplate.ShowColumnHeaders;
      else if (this.current is GridViewNewRowInfo)
        flag = flag && GridTraverser.IsNewRowVisible(this.current as GridViewNewRowInfo);
      else if (this.current is GridViewSearchRowInfo)
        flag = flag && this.viewInfo.ViewTemplate.AllowSearchRow;
      else if (this.current is GridViewFilteringRowInfo)
        flag = flag && this.viewInfo.ViewTemplate.EnableFiltering && this.viewInfo.ViewTemplate.ShowFilteringRow;
      else if (this.current is GridViewDetailsRowInfo)
      {
        GridViewHierarchyRowInfo owner = ((GridViewDetailsRowInfo) this.current).Owner as GridViewHierarchyRowInfo;
        if (owner != null)
          flag = flag && owner.IsExpanded;
      }
      if (this.RowVisible == null)
        return flag;
      RowEnumeratorEventArgs e = new RowEnumeratorEventArgs(this.current);
      e.ProcessRow = flag;
      this.RowVisible((object) this, e);
      return e.ProcessRow;
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

    public int GoToRow(GridViewRowInfo row)
    {
      ViewInfoTraverser.ViewInfoEnumeratorPosition position = this.Position;
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

    public int GetRowCount()
    {
      int num = 0;
      ViewInfoTraverser.ViewInfoEnumeratorPosition position = this.Position;
      this.Reset();
      while (this.MoveNext())
        ++num;
      this.Position = position;
      return num;
    }

    public GridViewRowInfo GoToRowIndex(int rowIndex)
    {
      ViewInfoTraverser.ViewInfoEnumeratorPosition position = this.Position;
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

    public bool IsFirstRow(GridViewRowInfo row)
    {
      ViewInfoTraverser.ViewInfoEnumeratorPosition position = this.Position;
      this.Reset();
      while (this.MoveNext())
      {
        if (this.Current.CanBeCurrent)
        {
          bool flag = this.Current == row;
          this.Position = position;
          return flag;
        }
      }
      this.Position = position;
      return false;
    }

    public bool IsLastRow(GridViewRowInfo row)
    {
      ViewInfoTraverser.ViewInfoEnumeratorPosition position = this.Position;
      this.GoToRow(row);
      while (this.MoveNext())
      {
        if (this.Current.CanBeCurrent)
        {
          this.Position = position;
          return false;
        }
      }
      this.Position = position;
      return true;
    }

    private bool MoveNextCore()
    {
      do
        ;
      while (this.index >= this.Collection.Count - 1 && this.ChangeCollectionForward());
      if (this.index >= this.Collection.Count - 1)
        return false;
      ++this.index;
      this.current = this.stage != ViewInfoTraverser.Stages.BottomSystemRows && this.stage != ViewInfoTraverser.Stages.BottomPinnedSystemRows || this.Collection != this.ViewInfo.SystemRows ? (GridViewRowInfo) this.Collection[this.index] : (GridViewRowInfo) this.Collection[this.Collection.Count - this.index - 1];
      return true;
    }

    private bool MovePreviousCore()
    {
      if (this.current is GridViewNewRowInfo && this.viewInfo.ViewTemplate.AddNewRowPosition == SystemRowPosition.Bottom)
        --this.index;
      do
        ;
      while (this.index <= 0 && this.ChangeCollectionBackward());
      if (this.index > 0 && this.Collection.Count > 0)
      {
        --this.index;
        if (this.index >= this.Collection.Count)
          this.index = this.Collection.Count - 1;
        this.current = (GridViewRowInfo) this.Collection[this.index];
        return true;
      }
      this.index = -1;
      this.current = (GridViewRowInfo) null;
      return false;
    }

    private bool ChangeCollectionForward()
    {
      if (this.stage >= ViewInfoTraverser.Stages.BottomPinnedSystemRows)
        return false;
      if (this.filterByPinPosition && this.filteredPinPosition == PinnedRowPosition.Top)
      {
        if (this.stage >= ViewInfoTraverser.Stages.TopPinnedRows)
          return false;
      }
      else if (this.filterByPinPosition && this.filteredPinPosition == PinnedRowPosition.None && this.stage >= ViewInfoTraverser.Stages.BottomSystemRows)
        return false;
      ++this.stage;
      this.SetCollectionForStage();
      this.index = -1;
      return true;
    }

    private bool ChangeCollectionBackward()
    {
      if (this.stage <= ViewInfoTraverser.Stages.TopPinnedSystemRows)
        return false;
      if (this.filterByPinPosition && this.filteredPinPosition == PinnedRowPosition.Bottom)
      {
        if (this.stage <= ViewInfoTraverser.Stages.BottomPinnedRows)
          return false;
      }
      else if (this.filterByPinPosition && this.filteredPinPosition == PinnedRowPosition.None && this.stage <= ViewInfoTraverser.Stages.TopSystemRows)
        return false;
      --this.stage;
      this.SetCollectionForStage();
      this.index = this.Collection.Count;
      return true;
    }

    private void SetCollectionForStage()
    {
      this.SetCollectionForStage(true);
    }

    private void SetCollectionForStage(bool initializeCollection)
    {
      this.rowPosition = SystemRowPosition.Top;
      switch (this.stage)
      {
        case ViewInfoTraverser.Stages.TopPinnedSystemRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SystemRows;
          this.pinPosition = PinnedRowPosition.Top;
          break;
        case ViewInfoTraverser.Stages.TopPinnedSummaryRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SummaryRows;
          this.pinPosition = PinnedRowPosition.Top;
          break;
        case ViewInfoTraverser.Stages.TopPinnedRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.PinnedRows;
          this.pinPosition = PinnedRowPosition.Top;
          break;
        case ViewInfoTraverser.Stages.TopSystemRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SystemRows;
          this.pinPosition = PinnedRowPosition.None;
          this.rowPosition = SystemRowPosition.Top;
          break;
        case ViewInfoTraverser.Stages.TopSummaryRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SummaryRows;
          this.pinPosition = PinnedRowPosition.None;
          this.rowPosition = SystemRowPosition.Top;
          break;
        case ViewInfoTraverser.Stages.ChildRows:
          if (initializeCollection && this.viewInfo.ParentRow != null)
            this.collection = !this.viewInfo.ViewTemplate.IsSelfReference ? (ITraversable) this.viewInfo.ChildRows : (ITraversable) this.viewInfo.ParentRow.ChildRows;
          else if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.ViewTemplate.ChildRows;
          this.pinPosition = PinnedRowPosition.None;
          break;
        case ViewInfoTraverser.Stages.BottomSummaryRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SummaryRows;
          this.pinPosition = PinnedRowPosition.None;
          this.rowPosition = SystemRowPosition.Bottom;
          break;
        case ViewInfoTraverser.Stages.BottomSystemRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SystemRows;
          this.pinPosition = PinnedRowPosition.None;
          this.rowPosition = SystemRowPosition.Bottom;
          break;
        case ViewInfoTraverser.Stages.BottomPinnedRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.PinnedRows;
          this.pinPosition = PinnedRowPosition.Bottom;
          break;
        case ViewInfoTraverser.Stages.BottomPinnedSummaryRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SummaryRows;
          this.pinPosition = PinnedRowPosition.Bottom;
          break;
        case ViewInfoTraverser.Stages.BottomPinnedSystemRows:
          if (initializeCollection)
            this.collection = (ITraversable) this.viewInfo.SystemRows;
          this.pinPosition = PinnedRowPosition.Bottom;
          break;
      }
    }

    public enum Stages
    {
      TopPinnedSystemRows,
      TopPinnedSummaryRows,
      TopPinnedRows,
      TopSystemRows,
      TopSummaryRows,
      ChildRows,
      BottomSummaryRows,
      BottomSystemRows,
      BottomPinnedRows,
      BottomPinnedSummaryRows,
      BottomPinnedSystemRows,
    }

    public class ViewInfoEnumeratorPosition : IEquatable<ViewInfoTraverser.ViewInfoEnumeratorPosition>
    {
      private GridViewInfo viewInfo;
      private ViewInfoTraverser.Stages stage;
      private ITraversable collection;
      private int index;
      private PinnedRowPosition pinPosition;
      private SystemRowPosition rowPosition;
      private PinnedRowPosition filteredPinPoisition;
      private bool filterByPinPosition;

      public ViewInfoEnumeratorPosition(
        ViewInfoTraverser.ViewInfoEnumeratorPosition position)
        : this(position.viewInfo, position.stage, position.collection, position.index, position.pinPosition, position.rowPosition, position.filteredPinPoisition, position.filterByPinPosition)
      {
      }

      public ViewInfoEnumeratorPosition(
        GridViewInfo viewInfo,
        ViewInfoTraverser.Stages stage,
        ITraversable collection,
        int index,
        PinnedRowPosition pinPosition,
        SystemRowPosition rowPosition,
        PinnedRowPosition filteredPinPoisition,
        bool filterByPinPosition)
      {
        this.viewInfo = viewInfo;
        this.stage = stage;
        this.collection = collection;
        this.index = index;
        this.pinPosition = pinPosition;
        this.rowPosition = rowPosition;
        this.filteredPinPoisition = filteredPinPoisition;
        this.filterByPinPosition = filterByPinPosition;
      }

      public GridViewInfo ViewInfo
      {
        get
        {
          return this.viewInfo;
        }
      }

      public ViewInfoTraverser.Stages Stage
      {
        get
        {
          return this.stage;
        }
        set
        {
          this.stage = value;
        }
      }

      public ITraversable Collection
      {
        get
        {
          return this.collection;
        }
        set
        {
          this.collection = value;
        }
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

      public GridViewRowInfo Current
      {
        get
        {
          if (this.collection != null && this.index >= 0 && this.index < this.collection.Count)
            return this.collection[this.index] as GridViewRowInfo;
          return (GridViewRowInfo) null;
        }
      }

      public PinnedRowPosition PinPosition
      {
        get
        {
          return this.pinPosition;
        }
      }

      public SystemRowPosition RowPosition
      {
        get
        {
          return this.rowPosition;
        }
      }

      public PinnedRowPosition FilteredPinPosition
      {
        get
        {
          return this.filteredPinPoisition;
        }
      }

      public bool FilterByPinPosition
      {
        get
        {
          return this.filterByPinPosition;
        }
      }

      public bool Equals(
        ViewInfoTraverser.ViewInfoEnumeratorPosition position)
      {
        if (position == null)
          return false;
        return position.stage == this.stage && position.collection == this.collection && (position.index == this.index && position.viewInfo == this.viewInfo) && position.pinPosition == this.pinPosition && position.rowPosition == this.rowPosition;
      }

      public override bool Equals(object obj)
      {
        return this.Equals(obj as ViewInfoTraverser.ViewInfoEnumeratorPosition);
      }

      public override int GetHashCode()
      {
        return base.GetHashCode();
      }
    }
  }
}
