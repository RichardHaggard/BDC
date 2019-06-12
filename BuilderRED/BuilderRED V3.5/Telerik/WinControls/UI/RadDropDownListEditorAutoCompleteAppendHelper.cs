// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownListEditorAutoCompleteAppendHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class RadDropDownListEditorAutoCompleteAppendHelper : AutoCompleteAppendHelper
  {
    public RadDropDownListEditorAutoCompleteAppendHelper(RadDropDownListEditorElement element)
      : base((RadDropDownListElement) element)
    {
    }

    protected override void SetEditableElementText(int itemIndex)
    {
      this.Owner.SelectedIndex = itemIndex;
      base.SetEditableElementText(itemIndex);
    }
  }
}
