// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SortChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class SortChangingEventArgs : CancelEventArgs
  {
    public readonly int NewIndex = -1;
    public readonly int OldIndex = -1;
    public readonly SortChangeType SortChangeType;
    public readonly GridSortField OldSortExpression;
    public readonly GridSortField NewSortExpression;

    public GridSortField SortExpression
    {
      get
      {
        return this.NewSortExpression;
      }
    }

    public int Index
    {
      get
      {
        return this.NewIndex;
      }
    }

    public SortChangingEventArgs(SortChangeType sortChangeType)
    {
      this.SortChangeType = sortChangeType;
    }

    public SortChangingEventArgs(SortChangeType sortChangeType, GridSortField sortExpression)
    {
      this.SortChangeType = sortChangeType;
      this.OldSortExpression = this.NewSortExpression = sortExpression;
    }

    public SortChangingEventArgs(
      SortChangeType sortChangeType,
      GridSortField sortExpression,
      int index)
    {
      this.SortChangeType = sortChangeType;
      this.OldSortExpression = this.NewSortExpression = sortExpression;
      this.OldIndex = this.NewIndex = index;
    }

    public SortChangingEventArgs(
      SortChangeType sortChangeType,
      GridSortField sortExpression,
      int oldIndex,
      int newIndex)
    {
      this.SortChangeType = sortChangeType;
      this.OldSortExpression = this.NewSortExpression = sortExpression;
      this.OldIndex = oldIndex;
      this.NewIndex = newIndex;
    }

    public SortChangingEventArgs(
      SortChangeType sortChangeType,
      GridSortField oldSortExpression,
      GridSortField newSortExpression,
      int index)
    {
      this.SortChangeType = sortChangeType;
      this.OldSortExpression = oldSortExpression;
      this.NewSortExpression = newSortExpression;
      this.OldIndex = this.NewIndex = index;
    }

    public SortChangingEventArgs(
      SortChangeType sortChangeType,
      GridSortField oldSortExpression,
      GridSortField newSortExpression,
      int oldIndex,
      int newIndex)
    {
      this.SortChangeType = sortChangeType;
      this.OldSortExpression = oldSortExpression;
      this.NewSortExpression = newSortExpression;
      this.OldIndex = oldIndex;
      this.NewIndex = newIndex;
    }

    public void Move(int oldIndex, int newIndex)
    {
    }

    public virtual void BeginUpdate()
    {
    }

    public virtual void BeginItemUpdate()
    {
    }

    public void EndItemUpdate()
    {
      this.EndItemUpdate(true);
    }

    public virtual void EndItemUpdate(bool notifyUpdates)
    {
    }

    public virtual void EndUpdate(bool notifyUpdates)
    {
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    public bool IsUpdated
    {
      get
      {
        return true;
      }
    }
  }
}
