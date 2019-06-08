// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCustomGroupingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewCustomGroupingEventArgs : EventArgs
  {
    private bool handled = true;
    private GridViewTemplate template;
    private GridViewRowInfo row;
    private int level;
    private object groupKey;

    public GridViewCustomGroupingEventArgs(
      GridViewTemplate template,
      GridViewRowInfo row,
      int level)
      : this(template, row, level, (object) null)
    {
    }

    public GridViewCustomGroupingEventArgs(
      GridViewTemplate template,
      GridViewRowInfo row,
      int level,
      object groupKey)
    {
      this.template = template;
      this.row = row;
      this.level = level;
      this.groupKey = groupKey;
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }

    public GridViewRowInfo Row
    {
      get
      {
        return this.row;
      }
    }

    public int Level
    {
      get
      {
        return this.level;
      }
    }

    public object GroupKey
    {
      get
      {
        return this.groupKey;
      }
      set
      {
        this.groupKey = value;
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
