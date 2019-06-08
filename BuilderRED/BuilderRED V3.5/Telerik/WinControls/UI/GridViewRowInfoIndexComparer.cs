// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowInfoIndexComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class GridViewRowInfoIndexComparer : IComparer<GridViewRowInfo>
  {
    private SortOrder order;

    public GridViewRowInfoIndexComparer(SortOrder order)
    {
      this.order = order;
    }

    public int Compare(GridViewRowInfo x, GridViewRowInfo y)
    {
      if (x.Index == y.Index)
        return 0;
      return x.Index < y.Index || this.order == SortOrder.Descending ? -1 : 1;
    }
  }
}
