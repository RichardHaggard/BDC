﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterPopupInitializedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class FilterPopupInitializedEventArgs : EventArgs
  {
    private GridViewDataColumn dataColumn;
    private IGridFilterPopup filterPopup;

    public IGridFilterPopup FilterPopup
    {
      get
      {
        return this.filterPopup;
      }
      set
      {
        this.filterPopup = value;
      }
    }

    public GridViewDataColumn Column
    {
      get
      {
        return this.dataColumn;
      }
    }

    public FilterPopupInitializedEventArgs(
      GridViewDataColumn dataColumn,
      IGridFilterPopup filterPopup)
    {
      this.dataColumn = dataColumn;
      this.filterPopup = filterPopup;
    }
  }
}
