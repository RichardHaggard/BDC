// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridFilterRowElement : GridVirtualizedRowElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "FilterRowFill";
    }

    public override bool IsCompatible(GridViewRowInfo data, object context)
    {
      return data is GridViewFilteringRowInfo;
    }

    public override bool CanApplyFormatting
    {
      get
      {
        return false;
      }
    }

    public override RadDropDownMenu MergeMenus(
      RadDropDownMenu contextMenu,
      params object[] parameters)
    {
      if (contextMenu == null)
        return (RadDropDownMenu) null;
      int index = contextMenu.Items.Count - 1;
      if (index >= 0)
        contextMenu.Items.RemoveAt(index);
      return contextMenu;
    }
  }
}
