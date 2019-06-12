// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewTimePickerEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewTimePickerEditor : BaseTimePickerEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      RadTimePickerElement editorElement = this.EditorElement as RadTimePickerElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null)
        return;
      if (e.KeyCode == Keys.Return)
        ownerElement.Data.Owner.EndEdit();
      else if (e.KeyCode == Keys.Escape)
        ownerElement.Data.Owner.CancelEdit();
      else
        base.OnKeyDown(e);
    }

    protected override void OnLostFocus()
    {
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus))
        return;
      ownerElement.Data.Owner.EndEdit();
    }
  }
}
