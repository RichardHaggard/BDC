// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewTextBoxEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TreeViewTextBoxEditor : BaseTextBoxEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null || ownerElement.TreeViewElement == null)
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          ownerElement.TreeViewElement.EndEdit();
          break;
        case Keys.Escape:
          ownerElement.TreeViewElement.CancelEdit();
          break;
        case Keys.Up:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtFirstLine))
            break;
          RadTreeViewElement treeViewElement1 = ownerElement.TreeViewElement;
          ownerElement.TreeViewElement.EndEdit();
          treeViewElement1.Update(RadTreeViewElement.UpdateActions.Reset);
          treeViewElement1.ProcessKeyDown(e);
          break;
        case Keys.Down:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtLastLine))
            break;
          RadTreeViewElement treeViewElement2 = ownerElement.TreeViewElement;
          ownerElement.TreeViewElement.EndEdit();
          treeViewElement2.Update(RadTreeViewElement.UpdateActions.Reset);
          treeViewElement2.ProcessKeyDown(e);
          break;
      }
    }

    protected override void OnLostFocus()
    {
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus) || ownerElement.TreeViewElement.IsPerformingEndEdit)
        return;
      ownerElement.TreeViewElement.EndEdit();
    }
  }
}
