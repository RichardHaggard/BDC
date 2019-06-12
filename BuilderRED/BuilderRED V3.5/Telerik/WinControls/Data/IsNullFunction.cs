// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.IsNullFunction
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.Data
{
  internal class IsNullFunction : FilterFunction
  {
    private List<Type> defaultForTypes;
    private List<Type> applicableForTypes;

    public IsNullFunction()
    {
      this.parameterCount = 0;
      this.functionFormat = "[{0}] IS NULL";
      this.mustQuoteValues = false;
      this.gkfunction = GridKnownFunction.IsNull;
    }

    public override List<Type> DefaultForTypes
    {
      get
      {
        if (this.defaultForTypes == null)
          this.defaultForTypes = new List<Type>();
        return this.defaultForTypes;
      }
    }

    public override List<Type> ApplicableForTypes
    {
      get
      {
        if (this.applicableForTypes == null)
          this.applicableForTypes = new List<Type>((IEnumerable<Type>) new Type[1]
          {
            typeof (string)
          });
        return this.applicableForTypes;
      }
    }

    public override string DisplayName
    {
      get
      {
        return LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionIsNull");
      }
    }
  }
}
