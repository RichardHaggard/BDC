// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PrintGridTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class PrintGridTraverser : GridTraverser
  {
    private bool processHiddenRows;

    public PrintGridTraverser(GridViewInfo viewInfo)
      : base(viewInfo)
    {
    }

    public bool ProcessHiddenRows
    {
      get
      {
        return this.processHiddenRows;
      }
      set
      {
        if (this.processHiddenRows == value)
          return;
        this.processHiddenRows = value;
      }
    }

    protected override bool CanStepInHierarchy()
    {
      GridViewHierarchyRowInfo current = this.Traverser.Current as GridViewHierarchyRowInfo;
      if (current != null)
      {
        foreach (GridViewInfo view in (IEnumerable<GridViewInfo>) current.Views)
        {
          if (view.ChildRows.Count > 0)
            return true;
        }
        return false;
      }
      if (this.Traverser.Current != null)
        return this.Traverser.Current.HasChildRows();
      return false;
    }

    protected override void CreateTraverser(GridViewInfo viewInfo)
    {
      base.CreateTraverser(viewInfo);
      ViewInfoTraverser traverser = this.Traverser as ViewInfoTraverser;
      if (traverser == null)
        return;
      traverser.ProcessHiddenRows = this.ProcessHiddenRows;
    }
  }
}
