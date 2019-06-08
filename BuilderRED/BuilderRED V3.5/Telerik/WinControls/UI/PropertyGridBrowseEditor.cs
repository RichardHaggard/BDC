// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridBrowseEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridBrowseEditor : BaseBrowseEditor
  {
    public override void BeginEdit()
    {
      ((RadBrowseEditorElement) this.EditorElement).DialogClosed += new DialogClosedEventHandler(this.PropertyGridBrowseEditor_DialogClosed);
      base.BeginEdit();
    }

    public override bool EndEdit()
    {
      ((RadBrowseEditorElement) this.EditorElement).DialogClosed -= new DialogClosedEventHandler(this.PropertyGridBrowseEditor_DialogClosed);
      return base.EndEdit();
    }

    private void PropertyGridBrowseEditor_DialogClosed(object sender, DialogClosedEventArgs e)
    {
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true))
        return;
      ownerElement.PropertyTableElement.EndEdit();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      RadBrowseEditorElement editorElement = this.EditorElement as RadBrowseEditorElement;
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null)
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (!this.Validate())
            break;
          ownerElement.PropertyTableElement.EndEdit();
          break;
        case Keys.Escape:
          ownerElement.Data.PropertyGridTableElement.CancelEdit();
          break;
        case Keys.Delete:
          if (editorElement.TextBoxItem.SelectionLength != editorElement.TextBoxItem.TextLength)
            break;
          editorElement.Text = string.Empty;
          break;
      }
    }

    protected override void OnLostFocus()
    {
      base.OnLostFocus();
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus))
        return;
      ownerElement.PropertyTableElement.EndEdit();
    }

    public override void OnValueChanging(ValueChangingEventArgs e)
    {
      base.OnValueChanging(e);
      (this.OwnerElement as PropertyGridItemElement)?.PropertyTableElement.OnValueChanging((object) this, e);
    }

    public override void OnValueChanged()
    {
      base.OnValueChanged();
      (this.OwnerElement as PropertyGridItemElement)?.PropertyTableElement.OnValueChanged((object) this, EventArgs.Empty);
    }
  }
}
