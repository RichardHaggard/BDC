// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCustomFilteringEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCustomFilteringEventArgs : EventArgs
  {
    private bool visible = true;
    private bool handled = true;
    private GridViewTemplate template;
    private GridViewRowInfo row;

    public GridViewCustomFilteringEventArgs(GridViewTemplate template, GridViewRowInfo row)
      : this(template, row, true)
    {
    }

    public GridViewCustomFilteringEventArgs(
      GridViewTemplate template,
      GridViewRowInfo row,
      bool visible)
    {
      this.template = template;
      this.row = row;
      this.visible = visible;
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.row;
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }

    public bool Visible
    {
      get
      {
        return this.visible;
      }
      set
      {
        this.visible = value;
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
