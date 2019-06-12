// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDropDownListEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewDropDownListEditor : BaseDropDownListEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
      this.selectionStart = editorElement.SelectionStart;
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null)
        return;
      if (e.KeyCode == Keys.Return && e.Modifiers != Keys.Control)
        ownerElement.Data.Owner.EndEdit();
      else if (e.KeyCode == Keys.Escape)
      {
        ownerElement.Data.Owner.CancelEdit();
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
      {
        if (editorElement.DropDownStyle != RadDropDownStyle.DropDownList)
          return;
        e.Handled = true;
      }
      else
      {
        if (e.KeyCode != Keys.Down || e.Modifiers != Keys.Alt)
          return;
        ((PopupEditorBaseElement) this.EditorElement).ShowPopup();
        e.Handled = true;
      }
    }

    public override void OnValueChanged()
    {
      base.OnValueChanged();
      if (((PopupEditorBaseElement) this.EditorElement).IsPopupOpen)
        return;
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null || ownerElement.Data.Owner == null || !ownerElement.Data.Owner.IsEditing)
        return;
      ownerElement.Data.Owner.EndEdit();
    }

    protected override void OnLostFocus()
    {
      base.OnLostFocus();
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus))
        return;
      ownerElement.Data.Owner.EndEdit();
    }
  }
}
