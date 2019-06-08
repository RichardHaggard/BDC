// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridExportState
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  internal class GridExportState
  {
    private GridViewRowInfo currentRow;
    private int vScrollBarValue;
    private int hScrollBarValue;
    private bool pagingState;
    private int currentPageIndex;
    private bool showGroupedColumns;

    public GridExportState(
      GridViewRowInfo currentRow,
      int vScrollBarValue,
      int hScrollBarValue,
      bool pagingState,
      int currentPageIndex,
      bool showGroupedColumns)
    {
      this.SelectedRows = (ICollection<GridViewRowInfo>) new List<GridViewRowInfo>();
      this.SelectedCells = (ICollection<GridViewCellInfo>) new List<GridViewCellInfo>();
      this.currentRow = currentRow;
      this.vScrollBarValue = vScrollBarValue;
      this.hScrollBarValue = hScrollBarValue;
      this.pagingState = pagingState;
      this.currentPageIndex = currentPageIndex;
      this.showGroupedColumns = showGroupedColumns;
    }

    public ICollection<GridViewRowInfo> SelectedRows { get; private set; }

    public ICollection<GridViewCellInfo> SelectedCells { get; private set; }

    public GridViewRowInfo CurrentRow
    {
      get
      {
        return this.currentRow;
      }
      set
      {
        this.currentRow = value;
      }
    }

    public int VScrollBarValue
    {
      get
      {
        return this.vScrollBarValue;
      }
      set
      {
        this.vScrollBarValue = value;
      }
    }

    public int HScrollBarValue
    {
      get
      {
        return this.hScrollBarValue;
      }
      set
      {
        this.hScrollBarValue = value;
      }
    }

    public bool PagingState
    {
      get
      {
        return this.pagingState;
      }
      set
      {
        this.pagingState = value;
      }
    }

    public int CurrentPageIndex
    {
      get
      {
        return this.currentPageIndex;
      }
      set
      {
        this.currentPageIndex = value;
      }
    }

    public bool ShowGroupedColumns
    {
      get
      {
        return this.showGroupedColumns;
      }
      set
      {
        this.showGroupedColumns = value;
      }
    }
  }
}
