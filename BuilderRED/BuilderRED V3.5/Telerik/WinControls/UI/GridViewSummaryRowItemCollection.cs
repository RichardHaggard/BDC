// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSummaryRowItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [ListBindable(BindableSupport.No)]
  public class GridViewSummaryRowItemCollection : ObservableCollection<GridViewSummaryRowItem>
  {
    public GridViewSummaryRowItem this[string name]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index][name] != null)
            return this[index];
        }
        return (GridViewSummaryRowItem) null;
      }
    }
  }
}
