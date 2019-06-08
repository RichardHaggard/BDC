// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowSourceNeededEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GridViewRowSourceNeededEventArgs : EventArgs
  {
    private IList<GridViewRowInfo> sourceCollection;
    private GridViewTemplate template;
    private GridViewRowInfo parentRow;

    public GridViewRowSourceNeededEventArgs(
      GridViewRowInfo parentRow,
      GridViewTemplate template,
      IList<GridViewRowInfo> sourceCollection)
    {
      this.parentRow = parentRow;
      this.template = template;
      this.sourceCollection = sourceCollection;
    }

    public GridViewRowInfo ParentRow
    {
      get
      {
        return this.parentRow;
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
    }

    public IList<GridViewRowInfo> SourceCollection
    {
      get
      {
        return this.sourceCollection;
      }
    }
  }
}
