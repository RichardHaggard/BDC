// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckedDropDownAutoCompleteBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CheckedDropDownAutoCompleteBoxElement : RadAutoCompleteBoxElement
  {
    private RadCheckedAutoCompleteBoxListElement checkedAutoCompleteBoxListElement;

    public RadCheckedAutoCompleteBoxListElement CheckedAutoCompleteBoxListElement
    {
      get
      {
        return this.checkedAutoCompleteBoxListElement;
      }
    }

    protected override RadTextBoxListElement CreateListElement()
    {
      this.checkedAutoCompleteBoxListElement = new RadCheckedAutoCompleteBoxListElement();
      return (RadTextBoxListElement) this.checkedAutoCompleteBoxListElement;
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadAutoCompleteBoxElement);
      }
    }

    public RadCheckedDropDownListElement OwnerElement
    {
      get
      {
        return this.checkedAutoCompleteBoxListElement.OwnerElement;
      }
      set
      {
        this.checkedAutoCompleteBoxListElement.OwnerElement = value;
      }
    }
  }
}
