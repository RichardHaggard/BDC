// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.LessThanFunction
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.Data
{
  internal class LessThanFunction : FilterFunction
  {
    private List<Type> defaultForTypes;
    private List<Type> applicableForTypes;
    private List<Type> notApllicableForTypes;

    public LessThanFunction()
    {
      this.parameterCount = 1;
      this.functionFormat = "[{0}] < {1}";
      this.mustQuoteValues = true;
      this.gkfunction = GridKnownFunction.LessThan;
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
            typeof (object)
          });
        return this.applicableForTypes;
      }
    }

    public override List<Type> NotApplicableForTypes
    {
      get
      {
        if (this.notApllicableForTypes == null)
          this.notApllicableForTypes = new List<Type>((IEnumerable<Type>) new Type[3]
          {
            typeof (bool),
            typeof (byte[]),
            typeof (Image)
          });
        return this.notApllicableForTypes;
      }
    }

    public override string DisplayName
    {
      get
      {
        return LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionLessThan");
      }
    }
  }
}
