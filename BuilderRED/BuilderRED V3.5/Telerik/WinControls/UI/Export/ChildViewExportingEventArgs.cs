// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Export.ChildViewExportingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI.Export
{
  public class ChildViewExportingEventArgs : EventArgs
  {
    private int activeViewIndex;
    private GridViewHierarchyRowInfo parentRow;

    public ChildViewExportingEventArgs(int activeViewIndex, GridViewHierarchyRowInfo row)
    {
      this.activeViewIndex = activeViewIndex;
      this.parentRow = row;
    }

    public int ActiveViewIndex
    {
      get
      {
        return this.activeViewIndex;
      }
      set
      {
        this.activeViewIndex = value;
      }
    }

    public GridViewHierarchyRowInfo ParentRow
    {
      get
      {
        return this.parentRow;
      }
    }
  }
}
