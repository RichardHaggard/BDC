// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridDateTimeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridDateTimeEditor : BaseDateTimeEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      RadDateTimePickerElement editorElement = this.EditorElement as RadDateTimePickerElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null)
        return;
      if (e.KeyCode == Keys.Return)
        ownerElement.PropertyTableElement.EndEdit();
      else if (e.KeyCode == Keys.Escape)
        ownerElement.PropertyTableElement.CancelEdit();
      else
        base.OnKeyDown(e);
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
