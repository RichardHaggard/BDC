// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewSpinEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewSpinEditor : BaseSpinEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null)
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Control)
            break;
          editorElement.Validate();
          ownerElement.Data.Owner.EndEdit();
          break;
        case Keys.Escape:
          ownerElement.Data.Owner.CancelEdit();
          break;
        case Keys.Delete:
          if (this.selectionLength != editorElement.TextBoxItem.TextLength)
            break;
          editorElement.Text = (string) null;
          break;
      }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      if (!(this.OwnerElement is BaseListViewVisualItem))
        return;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if ((!this.RightToLeft || this.selectionStart != editorElement.Text.Length) && (this.RightToLeft || this.selectionStart != 0 || this.selectionLength != 0))
            break;
          editorElement.Validate();
          break;
        case Keys.Right:
          if ((!this.RightToLeft || this.selectionStart != 0) && (this.RightToLeft || this.selectionStart != editorElement.Text.Length))
            break;
          editorElement.Validate();
          break;
      }
    }

    public override void OnLostFocus()
    {
      BaseListViewVisualItem ownerElement = this.OwnerElement as BaseListViewVisualItem;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus))
        return;
      ownerElement.Data.Owner.EndEdit();
    }
  }
}
