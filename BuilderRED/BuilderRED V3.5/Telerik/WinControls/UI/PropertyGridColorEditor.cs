// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridColorEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridColorEditor : BaseColorEditor
  {
    public override void BeginEdit()
    {
      base.BeginEdit();
      ((RadColorBoxElement) this.EditorElement).DialogClosed += new DialogClosedEventHandler(this.PropertyGridColorEditor_DialogClosed);
    }

    public override bool EndEdit()
    {
      ((RadColorBoxElement) this.EditorElement).DialogClosed -= new DialogClosedEventHandler(this.PropertyGridColorEditor_DialogClosed);
      return base.EndEdit();
    }

    private void PropertyGridColorEditor_DialogClosed(object sender, DialogClosedEventArgs e)
    {
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true))
        return;
      ownerElement.PropertyTableElement.EndEdit();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      RadColorBoxElement editorElement = this.EditorElement as RadColorBoxElement;
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

    protected override void OnKeyUp(KeyEventArgs e)
    {
      RadColorBoxElement editorElement = this.EditorElement as RadColorBoxElement;
      if (!(this.OwnerElement is PropertyGridItemElement))
        return;
      int selectionStart = editorElement.TextBoxItem.SelectionStart;
      int selectionLength = editorElement.TextBoxItem.SelectionLength;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if ((!this.RightToLeft || selectionStart != editorElement.Text.Length) && (this.RightToLeft || selectionStart != 0 || selectionLength != 0))
            break;
          editorElement.Validate();
          break;
        case Keys.Right:
          if ((!this.RightToLeft || selectionStart != 0) && (this.RightToLeft || selectionStart != editorElement.Text.Length))
            break;
          editorElement.Validate();
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
