// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewDropDownListEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class TreeViewDropDownListEditor : BaseDropDownListEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
      this.selectionStart = editorElement.SelectionStart;
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null)
        return;
      if (e.KeyCode == Keys.Return && e.Modifiers != Keys.Control)
        ownerElement.TreeViewElement.EndEdit();
      else if (e.KeyCode == Keys.Escape)
      {
        ownerElement.TreeViewElement.CancelEdit();
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
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null || ownerElement.TreeViewElement == null || !ownerElement.TreeViewElement.IsEditing)
        return;
      ownerElement.TreeViewElement.EndEdit();
    }

    protected override void OnLostFocus()
    {
      base.OnLostFocus();
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus) || ownerElement.TreeViewElement.IsPerformingEndEdit)
        return;
      ownerElement.TreeViewElement.EndEdit();
    }
  }
}
