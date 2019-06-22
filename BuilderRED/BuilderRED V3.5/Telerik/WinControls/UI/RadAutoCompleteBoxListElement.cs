﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadAutoCompleteBoxListElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadAutoCompleteBoxListElement : RadTextBoxListElement
  {
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
      return (item.Text ?? string.Empty).ToUpperInvariant().Contains(this.PatternText.ToUpperInvariant());
    }
  }
}