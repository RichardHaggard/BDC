// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFilterComposeMenuItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class RadFilterComposeMenuItem : RadMenuItem
  {
    private FilterDescriptor filterDescriptor;

    public RadFilterComposeMenuItem()
    {
    }

    public RadFilterComposeMenuItem(string id)
      : base(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString(id))
    {
    }

    public FilterDescriptor FilterDescriptor
    {
      get
      {
        return this.filterDescriptor;
      }
      set
      {
        this.filterDescriptor = value;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMenuItem);
      }
    }
  }
}
