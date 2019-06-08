// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.NotBetweenFunction
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
  internal class NotBetweenFunction : CompositeFilterFunction
  {
    private static List<FilterFunction> simpleFunctions = new List<FilterFunction>((IEnumerable<FilterFunction>) new FilterFunction[2]{ (FilterFunction) new LessThanOrEqualToFunction(), (FilterFunction) new GreaterThanOrEqualToFunction() });
    private List<Type> defaultForTypes;
    private List<Type> applicableForTypes;

    public NotBetweenFunction()
    {
      this.parameterCount = 2;
      this.functionFormat = "([{0}] < {1}) OR ([{0}] > {2})";
      this.mustQuoteValues = true;
      this.gkfunction = GridKnownFunction.NotBetween;
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
        return LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionNotBetween");
      }
    }

    public override List<FilterFunction> SimpleFunctions
    {
      get
      {
        return NotBetweenFunction.simpleFunctions;
      }
    }

    public override FilterExpression.BinaryOperation BinaryOperation
    {
      get
      {
        return FilterExpression.BinaryOperation.OR;
      }
    }
  }
}
