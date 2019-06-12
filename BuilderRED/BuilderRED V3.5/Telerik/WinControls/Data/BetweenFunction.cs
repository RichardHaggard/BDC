// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.BetweenFunction
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
  internal class BetweenFunction : CompositeFilterFunction
  {
    private static List<FilterFunction> simpleFunctions = new List<FilterFunction>((IEnumerable<FilterFunction>) new FilterFunction[2]{ (FilterFunction) new GreaterThanOrEqualToFunction(), (FilterFunction) new LessThanOrEqualToFunction() });
    private List<Type> defaultForTypes;
    private List<Type> applicableForTypes;

    public BetweenFunction()
    {
      this.parameterCount = 2;
      this.functionFormat = "([{0}] >= {1}) AND ([{0}] <= {2})";
      this.mustQuoteValues = true;
      this.gkfunction = GridKnownFunction.Between;
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
          this.applicableForTypes = new List<Type>((IEnumerable<Type>) new Type[5]
          {
            typeof (int),
            typeof (double),
            typeof (float),
            typeof (DateTime),
            typeof (Decimal)
          });
        return this.applicableForTypes;
      }
    }

    public override string DisplayName
    {
      get
      {
        return LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionsBetween");
      }
    }

    public override List<FilterFunction> SimpleFunctions
    {
      get
      {
        return BetweenFunction.simpleFunctions;
      }
    }

    public override FilterExpression.BinaryOperation BinaryOperation
    {
      get
      {
        return FilterExpression.BinaryOperation.AND;
      }
    }
  }
}
