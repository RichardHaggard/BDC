// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSearchResultCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridSearchResultCellInfo
  {
    private int traverserRowIndex;
    private int traverserColumnIndex;
    private GridViewRowInfo rowInfo;
    private GridViewColumn column;

    public GridSearchResultCellInfo(GridViewRowInfo rowInfo, GridViewColumn column)
    {
      this.rowInfo = rowInfo;
      this.column = column;
    }

    public GridSearchResultCellInfo(
      GridViewRowInfo rowInfo,
      GridViewColumn column,
      int traverserRowIndex,
      int traverserColumnIndex)
    {
      this.rowInfo = rowInfo;
      this.column = column;
      this.traverserRowIndex = traverserRowIndex;
      this.traverserColumnIndex = traverserColumnIndex;
    }

    public GridViewRowInfo RowInfo
    {
      get
      {
        return this.rowInfo;
      }
      set
      {
        this.rowInfo = value;
      }
    }

    public GridViewColumn ColumnInfo
    {
      get
      {
        return this.column;
      }
      set
      {
        this.column = value;
      }
    }

    public int TraverserRowIndex
    {
      get
      {
        return this.traverserRowIndex;
      }
      set
      {
        this.traverserRowIndex = value;
      }
    }

    public int TraverserColumnIndex
    {
      get
      {
        return this.traverserColumnIndex;
      }
      set
      {
        this.traverserColumnIndex = value;
      }
    }
  }
}
