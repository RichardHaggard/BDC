// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewInfoCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewInfoCollection : Collection<GridViewInfo>, IReadOnlyCollection<GridViewInfo>, IEnumerable<GridViewInfo>, IEnumerable
  {
    public bool Contains(GridViewTemplate template)
    {
      return this.IndexOf(template) >= 0;
    }

    public int IndexOf(GridViewTemplate template)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ViewTemplate == template)
          return index;
      }
      return -1;
    }

    public GridViewInfo this[GridViewTemplate template]
    {
      get
      {
        int index = this.IndexOf(template);
        if (index >= 0)
          return this[index];
        return (GridViewInfo) null;
      }
    }
  }
}
