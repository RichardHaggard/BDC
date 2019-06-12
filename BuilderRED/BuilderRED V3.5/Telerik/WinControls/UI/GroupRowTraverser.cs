// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupRowTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GroupRowTraverser : HierarchyRowTraverser
  {
    public GroupRowTraverser(GridViewGroupRowInfo hierarchyRow)
      : base((GridViewRowInfo) hierarchyRow)
    {
    }

    public GroupRowTraverser(HierarchyRowTraverser traverser)
      : base(traverser.Position)
    {
    }

    public GroupRowTraverser(
      HierarchyRowTraverser.HierarchyRowTraverserPosition position)
      : base(position)
    {
    }

    protected override int RowsCount
    {
      get
      {
        GridViewGroupRowInfo hierarchyRow = this.HierarchyRow as GridViewGroupRowInfo;
        if (hierarchyRow == null)
          return base.RowsCount;
        return hierarchyRow.TopSummaryRows.Count + hierarchyRow.ChildRows.Count + hierarchyRow.BottomSummaryRows.Count;
      }
    }

    protected override void SetCurrent()
    {
      GridViewGroupRowInfo hierarchyRow = this.HierarchyRow as GridViewGroupRowInfo;
      if (hierarchyRow == null)
        base.SetCurrent();
      else if (this.Index < hierarchyRow.TopSummaryRows.Count)
        this.Current = (GridViewRowInfo) hierarchyRow.TopSummaryRows[this.Index];
      else if (this.Index < hierarchyRow.TopSummaryRows.Count + hierarchyRow.ChildRows.Count)
      {
        this.Current = hierarchyRow.ChildRows[this.Index - hierarchyRow.TopSummaryRows.Count];
      }
      else
      {
        if (this.Index >= this.RowsCount)
          return;
        this.Current = (GridViewRowInfo) hierarchyRow.BottomSummaryRows[this.Index - (hierarchyRow.TopSummaryRows.Count + hierarchyRow.ChildRows.Count)];
      }
    }
  }
}
