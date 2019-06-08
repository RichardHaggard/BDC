// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewTextBoxEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewTextBoxEditor : BaseTextBoxEditor
  {
    protected override RadElement CreateEditorElement()
    {
      BaseTextBoxEditorElement boxEditorElement = new BaseTextBoxEditorElement();
      boxEditorElement.TextBoxItem.Alignment = ContentAlignment.MiddleLeft;
      boxEditorElement.StretchVertically = true;
      return (RadElement) boxEditorElement;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null)
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Shift)
            break;
          ownerElement.Data.Owner.EndEdit();
          break;
        case Keys.Escape:
          ownerElement.Data.Owner.CancelEdit();
          break;
        case Keys.Up:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtFirstLine))
            break;
          ownerElement.Data.Owner.EndEdit();
          ownerElement.Data.Owner.ProcessKeyDown(e);
          break;
        case Keys.Down:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtLastLine))
            break;
          ownerElement.Data.Owner.EndEdit();
          ownerElement.Data.Owner.ProcessKeyDown(e);
          break;
      }
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
