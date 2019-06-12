// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewSpinEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class TreeViewSpinEditor : BaseSpinEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null)
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Control)
            break;
          editorElement.Validate();
          ownerElement.TreeViewElement.EndEdit();
          break;
        case Keys.Escape:
          ownerElement.TreeViewElement.CancelEdit();
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
      if (!(this.OwnerElement is TreeNodeElement))
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
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus) || ownerElement.TreeViewElement.IsPerformingEndEdit)
        return;
      ownerElement.TreeViewElement.EndEdit();
    }
  }
}
