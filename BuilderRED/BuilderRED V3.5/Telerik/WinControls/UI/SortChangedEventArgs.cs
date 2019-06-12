// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SortChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class SortChangedEventArgs : EventArgs
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

    public SortChangedEventArgs(SortChangeType sortChangedType)
    {
      this.SortChangeType = sortChangedType;
    }

    public SortChangedEventArgs(SortChangeType sortChangedType, GridSortField sortExpression)
    {
      this.SortChangeType = sortChangedType;
      this.OldSortExpression = this.NewSortExpression = sortExpression;
    }

    public SortChangedEventArgs(
      SortChangeType sortChangedType,
      GridSortField sortExpression,
      int index)
    {
      this.SortChangeType = sortChangedType;
      this.OldSortExpression = this.NewSortExpression = sortExpression;
      this.OldIndex = this.NewIndex = index;
    }

    public SortChangedEventArgs(
      SortChangeType sortChangedType,
      GridSortField sortExpression,
      int oldIndex,
      int newIndex)
    {
      this.SortChangeType = sortChangedType;
      this.OldSortExpression = this.NewSortExpression = sortExpression;
      this.OldIndex = oldIndex;
      this.NewIndex = newIndex;
    }

    public SortChangedEventArgs(
      SortChangeType sortChangedType,
      GridSortField oldSortExpression,
      GridSortField newSortExpression,
      int index)
    {
      this.SortChangeType = sortChangedType;
      this.OldSortExpression = oldSortExpression;
      this.NewSortExpression = newSortExpression;
      this.OldIndex = this.NewIndex = index;
    }

    public SortChangedEventArgs(
      SortChangeType sortChangedType,
      GridSortField oldSortExpression,
      GridSortField newSortExpression,
      int oldIndex,
      int newIndex)
    {
      this.SortChangeType = sortChangedType;
      this.OldSortExpression = oldSortExpression;
      this.NewSortExpression = newSortExpression;
      this.OldIndex = oldIndex;
      this.NewIndex = newIndex;
    }
  }
}
