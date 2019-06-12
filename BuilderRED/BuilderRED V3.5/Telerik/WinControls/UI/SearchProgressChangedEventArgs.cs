// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SearchProgressChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class SearchProgressChangedEventArgs : EventArgs
  {
    private string searchCriteria;
    private GridSearchResultCellInfo cell;
    private GridSearchResultCellCollection cells;
    private bool searchFinished;

    public SearchProgressChangedEventArgs(
      string criteria,
      GridSearchResultCellInfo cell,
      GridSearchResultCellCollection cells,
      bool searchFinished)
    {
      this.searchCriteria = criteria;
      this.cell = cell;
      this.cells = cells;
      this.searchFinished = searchFinished;
    }

    public string SearchCriteria
    {
      get
      {
        return this.searchCriteria;
      }
    }

    public GridSearchResultCellInfo Cell
    {
      get
      {
        return this.cell;
      }
    }

    public GridSearchResultCellCollection Cells
    {
      get
      {
        return this.cells;
      }
    }

    public bool SearchFinished
    {
      get
      {
        return this.searchFinished;
      }
    }
  }
}
