// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiColumnComboGridBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class MultiColumnComboGridBehavior : BaseGridBehavior
  {
    private GridViewRowInfo oldCurrentRow;

    public override bool OnMouseDown(MouseEventArgs e)
    {
      this.oldCurrentRow = this.GridViewElement.Template.CurrentRow;
      return base.OnMouseDown(e);
    }

    public override bool OnMouseUp(MouseEventArgs e)
    {
      bool flag1 = base.OnMouseUp(e);
      GridViewRowInfo currentRow = this.GridViewElement.Template.CurrentRow;
      bool flag2 = this.oldCurrentRow != currentRow;
      if (currentRow != null)
      {
        GridRowElement rowElement = this.GridViewElement.TableElement.GetRowElement(currentRow);
        if (rowElement != null)
          flag2 |= rowElement.ControlBoundingRectangle.Contains(e.Location);
      }
      if (flag2)
      {
        MultiColumnComboPopupForm parent = this.GridViewElement.ElementTree.Control.Parent as MultiColumnComboPopupForm;
        if (parent != null)
        {
          RadMultiColumnComboBoxElement ownerElement = parent.OwnerElement as RadMultiColumnComboBoxElement;
          ownerElement?.ClearFilter();
          if (parent.CanClosePopup(RadPopupCloseReason.Mouse))
            parent.ClosePopup(RadPopupCloseReason.Mouse);
          if (currentRow != null && !currentRow.IsCurrent)
            currentRow.IsCurrent = true;
          if (ownerElement != null)
          {
            ownerElement.SetText(this.GridViewElement.Template.CurrentRow);
            ownerElement.SelectAll();
          }
        }
      }
      this.oldCurrentRow = (GridViewRowInfo) null;
      return flag1;
    }
  }
}
