// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridFilterRowBehavior : GridRowBehavior
  {
    protected override bool OnMouseDownLeft(MouseEventArgs e)
    {
      GridFilterCellElement cellAtPoint = this.GetCellAtPoint(e.Location) as GridFilterCellElement;
      if (cellAtPoint == null || !cellAtPoint.FilterButton.ControlBoundingRectangle.Contains(e.Location))
        return base.OnMouseDownLeft(e);
      if (cellAtPoint.Children.Count > 2 && cellAtPoint.Children[2] is RadCheckBoxEditorElement && cellAtPoint.Children[2].ControlBoundingRectangle.Contains(e.Location))
        return base.OnMouseDownLeft(e);
      this.GridViewElement.EditorManager.CloseEditor();
      return false;
    }

    protected override bool CanEnterEditMode(GridViewRowInfo rowInfo)
    {
      return true;
    }
  }
}
