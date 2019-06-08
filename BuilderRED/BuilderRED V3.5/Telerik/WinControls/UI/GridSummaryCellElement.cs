// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSummaryCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridSummaryCellElement : GridVirtualizedCellElement
  {
    public GridSummaryCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      if (data is GridViewDataColumn)
        return context is GridSummaryRowElement;
      return false;
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      base.Initialize(column, row);
    }

    public virtual object[] Values
    {
      get
      {
        GridViewSummaryRowInfo rowInfo = (GridViewSummaryRowInfo) this.RowInfo;
        GridViewDataColumn columnInfo = this.ColumnInfo as GridViewDataColumn;
        if (columnInfo != null)
          return rowInfo.GetSummaryValues(columnInfo);
        return new object[0];
      }
    }

    public override object Value
    {
      get
      {
        return this.RowInfo[this.ColumnInfo];
      }
      set
      {
      }
    }

    internal override bool CanBestFit(BestFitColumnMode bestFitMode)
    {
      return (bestFitMode & BestFitColumnMode.SummaryRowCells) > (BestFitColumnMode) 0;
    }
  }
}
