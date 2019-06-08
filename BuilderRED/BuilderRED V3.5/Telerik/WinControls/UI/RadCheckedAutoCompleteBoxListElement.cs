// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedAutoCompleteBoxListElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCheckedAutoCompleteBoxListElement : RadAutoCompleteBoxListElement
  {
    private RadCheckedDropDownListElement ownerElement;

    public RadCheckedAutoCompleteBoxListElement()
    {
      this.BypassLayoutPolicies = true;
    }

    public RadCheckedDropDownListElement OwnerElement
    {
      get
      {
        return this.ownerElement;
      }
      set
      {
        this.ownerElement = value;
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadListElement);
      }
    }

    protected override bool AutoCompleteFilterOverride(RadListDataItem item)
    {
      if (this.AutoCompleteMode != AutoCompleteMode.Suggest)
        return base.AutoCompleteFilterOverride(item);
      string upperInvariant1 = (item.Text ?? string.Empty).ToUpperInvariant();
      string upperInvariant2 = this.PatternText.ToUpperInvariant();
      foreach (RadListDataItem checkedItem in (ReadOnlyCollection<RadCheckedListDataItem>) this.OwnerElement.CheckedItems)
      {
        if (checkedItem.CachedText == item.Text)
          return false;
      }
      return upperInvariant1.Contains(upperInvariant2);
    }
  }
}
