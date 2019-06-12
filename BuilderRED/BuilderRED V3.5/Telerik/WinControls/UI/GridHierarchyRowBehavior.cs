// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridHierarchyRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridHierarchyRowBehavior : GridDataRowBehavior
  {
    protected override bool CanEnterEditMode(GridViewRowInfo rowInfo)
    {
      GridExpanderItem elementAtPoint = GridVisualElement.GetElementAtPoint<GridExpanderItem>((RadElementTree) this.GridViewElement.ElementTree, this.MouseDownLocation);
      if (base.CanEnterEditMode(rowInfo))
        return elementAtPoint == null;
      return false;
    }

    protected override bool ProcessEnterKey(KeyEventArgs keys)
    {
      if (this.IsInEditMode || this.BeginEditMode == RadGridViewBeginEditMode.BeginEditOnEnter)
        return base.ProcessEnterKey(keys);
      this.GridViewElement.CurrentRow.IsExpanded = !this.GridViewElement.CurrentRow.IsExpanded;
      return true;
    }
  }
}
