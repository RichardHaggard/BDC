// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowInfoEventComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewRowInfoEventComparer : GridViewRowInfoComparer
  {
    public GridViewRowInfoEventComparer(SortDescriptorCollection context)
      : base(context)
    {
    }

    public override int Compare(GridViewRowInfo x, GridViewRowInfo y)
    {
      GridViewCustomSortingEventArgs args = new GridViewCustomSortingEventArgs(x.ViewTemplate, x, y);
      x.ViewTemplate.EventDispatcher.RaiseEvent<GridViewCustomSortingEventArgs>(EventDispatcher.CustomSorting, (object) x.ViewTemplate, args);
      if (args.Handled)
        return args.SortResult;
      return base.Compare(x, y);
    }
  }
}
