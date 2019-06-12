// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridTextBoxControlEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridTextBoxControlEditor : BaseTextBoxControlEditor
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null)
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          ownerElement.PropertyTableElement.EndEdit();
          break;
        case Keys.Escape:
          ownerElement.PropertyTableElement.CancelEdit();
          break;
        case Keys.Up:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtFirstLine))
            break;
          ownerElement.PropertyTableElement.EndEdit();
          ownerElement.PropertyTableElement.ProcessKeyDown(e);
          break;
        case Keys.Down:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtLastLine))
            break;
          ownerElement.PropertyTableElement.EndEdit();
          ownerElement.PropertyTableElement.ProcessKeyDown(e);
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
