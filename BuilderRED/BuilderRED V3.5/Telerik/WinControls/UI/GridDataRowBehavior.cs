// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDataRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridDataRowBehavior : GridRowBehavior
  {
    protected override bool OnMouseDownLeft(MouseEventArgs e)
    {
      GridRowElement rowAtPoint = this.GetRowAtPoint(e.Location);
      if (rowAtPoint != null && rowAtPoint.GridViewElement.TableElement.PageViewMode == PageViewMode.ExplorerBar)
      {
        GridViewHierarchyRowInfo parent = rowAtPoint.RowInfo.Parent as GridViewHierarchyRowInfo;
        if (parent != null)
        {
          this.GridViewElement.CurrentRow.IsSelected = false;
          parent.ActiveView = rowAtPoint.RowInfo.ViewInfo;
        }
      }
      return base.OnMouseDownLeft(e);
    }
  }
}
