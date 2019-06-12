// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridExpanderItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GridExpanderItem : ExpanderItem
  {
    static GridExpanderItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GridExpanderItemStateManager(), typeof (GridExpanderItem));
    }

    public override bool Expanded
    {
      get
      {
        return base.Expanded;
      }
      set
      {
        base.Expanded = value;
        this.CheckChildViewExpandingEventIsCanceled(value);
      }
    }

    protected virtual void CheckChildViewExpandingEventIsCanceled(bool value)
    {
      if (this.Parent == null)
        return;
      GridCellElement ancestor = this.FindAncestor<GridCellElement>();
      if (ancestor == null || ancestor.RowInfo == null || ancestor.RowInfo.IsExpanded == value)
        return;
      base.Expanded = ancestor.RowInfo.IsExpanded;
    }
  }
}
