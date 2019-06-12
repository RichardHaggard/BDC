// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCustomSortingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCustomSortingEventArgs : EventArgs
  {
    private bool handled = true;
    private GridViewTemplate template;
    private GridViewRowInfo row1;
    private GridViewRowInfo row2;
    private int sortResult;

    public GridViewCustomSortingEventArgs(
      GridViewTemplate template,
      GridViewRowInfo row1,
      GridViewRowInfo row2)
      : this(template, row1, row2, 0)
    {
    }

    public GridViewCustomSortingEventArgs(
      GridViewTemplate template,
      GridViewRowInfo row1,
      GridViewRowInfo row2,
      int sortResult)
    {
      this.template = template;
      this.row1 = row1;
      this.row2 = row2;
      this.sortResult = sortResult;
    }

    public GridViewRowInfo Row1
    {
      get
      {
        return this.row1;
      }
    }

    public GridViewRowInfo Row2
    {
      get
      {
        return this.row2;
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }

    public int SortResult
    {
      get
      {
        return this.sortResult;
      }
      set
      {
        this.sortResult = value;
      }
    }

    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }
  }
}
