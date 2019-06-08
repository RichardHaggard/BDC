// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DeleteRowDataViewChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;

namespace Telerik.WinControls.UI
{
  internal class DeleteRowDataViewChangedEventArgs : DataViewChangedEventArgs
  {
    private int hierarchyRowIndex = -1;
    private GridViewHierarchyRowInfo hierarchyRow;
    private GridViewInfo viewInfo;

    public DeleteRowDataViewChangedEventArgs(
      GridViewInfo viewInfo,
      GridViewHierarchyRowInfo hierarchyRow,
      int hierarchyRowIndex,
      GridViewRowInfo[] rows)
      : base(ViewChangedAction.Remove, (IList) rows)
    {
      this.viewInfo = viewInfo;
      this.hierarchyRow = hierarchyRow;
      this.hierarchyRowIndex = hierarchyRowIndex;
    }

    public int HierachyRowIndex
    {
      get
      {
        return this.hierarchyRowIndex;
      }
    }

    public GridViewHierarchyRowInfo HierarchyRow
    {
      get
      {
        return this.hierarchyRow;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }
  }
}
