// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewDateTimeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class TreeViewDateTimeEditor : BaseDateTimeEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      RadDateTimePickerElement editorElement = this.EditorElement as RadDateTimePickerElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null)
        return;
      if (e.KeyCode == Keys.Return)
        ownerElement.TreeViewElement.EndEdit();
      else if (e.KeyCode == Keys.Escape)
        ownerElement.TreeViewElement.CancelEdit();
      else
        base.OnKeyDown(e);
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
