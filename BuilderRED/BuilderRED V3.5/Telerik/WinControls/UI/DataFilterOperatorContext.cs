// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterOperatorContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class DataFilterOperatorContext
  {
    private string name;
    private FilterOperator filterOperator;
    internal string id;

    public DataFilterOperatorContext(string id, FilterOperator filterOperator)
    {
      this.id = id;
      this.name = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString(id);
      this.filterOperator = filterOperator;
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public FilterOperator Operator
    {
      get
      {
        return this.filterOperator;
      }
    }

    public static FilterOperator GetOperator(
      FilterOperator filterOperator,
      string value)
    {
      switch (filterOperator)
      {
        case FilterOperator.IsLike:
          bool flag1 = value.EndsWith("%");
          bool flag2 = value.StartsWith("%");
          if (flag1 && flag2)
            return FilterOperator.Contains;
          if (flag2)
            return FilterOperator.EndsWith;
          if (flag1)
            return FilterOperator.StartsWith;
          return filterOperator;
        case FilterOperator.IsNotLike:
          bool flag3 = value.EndsWith("%");
          bool flag4 = value.StartsWith("%");
          if (flag3 && flag4)
            return FilterOperator.NotContains;
          if (flag4)
            return FilterOperator.StartsWith;
          if (flag3)
            return FilterOperator.EndsWith;
          return filterOperator;
        default:
          return filterOperator;
      }
    }

    public static object GetDisplayValue(FilterOperator filterOperator, object value)
    {
      if (value == null)
        return (object) string.Empty;
      string str = value.ToString();
      switch (filterOperator)
      {
        case FilterOperator.IsLike:
        case FilterOperator.IsNotLike:
        case FilterOperator.Contains:
        case FilterOperator.NotContains:
          return (object) str.Trim('%');
        case FilterOperator.StartsWith:
          return (object) str.TrimEnd('%');
        case FilterOperator.EndsWith:
          return (object) str.TrimStart('%');
        default:
          return value;
      }
    }

    public static string GetDisplayName(FilterOperator filterOperator, string value)
    {
      switch (filterOperator)
      {
        case FilterOperator.None:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionNoFilter");
        case FilterOperator.IsLike:
          bool flag1 = value.EndsWith("%");
          bool flag2 = value.StartsWith("%");
          if (flag1 && flag2)
            return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionContains");
          if (flag2)
            return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionEndsWith");
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionStartsWith");
        case FilterOperator.IsNotLike:
          bool flag3 = value.EndsWith("%");
          bool flag4 = value.StartsWith("%");
          if (flag3 && flag4)
            return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionDoesNotContain");
          if (flag4)
            return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionStartsWith");
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionEndsWith");
        case FilterOperator.IsLessThan:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionLessThan");
        case FilterOperator.IsLessThanOrEqualTo:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionLessThanOrEqualTo");
        case FilterOperator.IsEqualTo:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionEqualTo");
        case FilterOperator.IsNotEqualTo:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionNotEqualTo");
        case FilterOperator.IsGreaterThanOrEqualTo:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionGreaterThanOrEqualTo");
        case FilterOperator.IsGreaterThan:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionGreaterThan");
        case FilterOperator.StartsWith:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionStartsWith");
        case FilterOperator.EndsWith:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionEndsWith");
        case FilterOperator.Contains:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionContains");
        case FilterOperator.NotContains:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionDoesNotContain");
        case FilterOperator.IsNull:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionIsNull");
        case FilterOperator.IsNotNull:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionNotIsNull");
        case FilterOperator.IsContainedIn:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionIsContainedIn");
        case FilterOperator.IsNotContainedIn:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionIsNotContainedIn");
        default:
          return DataFilterOperatorContext.GetLocalizationStringById("FilterFunctionNoFilter");
      }
    }

    private static string GetLocalizationStringById(string id)
    {
      return LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString(id);
    }

    public static List<DataFilterOperatorContext> GetFilterOperations(
      Type dataType)
    {
      Type underlyingType = Nullable.GetUnderlyingType(dataType);
      if ((object) underlyingType != null)
        dataType = underlyingType;
      List<DataFilterOperatorContext> filterOperatorContextList = new List<DataFilterOperatorContext>();
      filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionNoFilter", FilterOperator.None));
      if ((object) dataType == (object) typeof (string) || (object) dataType == (object) typeof (Guid))
      {
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionContains", FilterOperator.Contains));
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionDoesNotContain", FilterOperator.NotContains));
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionStartsWith", FilterOperator.StartsWith));
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionEndsWith", FilterOperator.EndsWith));
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionIsContainedIn", FilterOperator.IsContainedIn));
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionIsNotContainedIn", FilterOperator.IsNotContainedIn));
      }
      if (TelerikHelper.IsNumericType(dataType) || (object) dataType == (object) typeof (DateTime) || ((object) dataType == (object) typeof (TimeSpan) || (object) dataType == (object) typeof (string)) || ((object) dataType == (object) typeof (char) || (object) dataType == (object) typeof (bool) || ((object) dataType == (object) typeof (ToggleState) || (object) dataType == (object) typeof (Color))) || ((object) dataType == (object) typeof (Guid) || dataType.IsEnum))
      {
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionEqualTo", FilterOperator.IsEqualTo));
        filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionNotEqualTo", FilterOperator.IsNotEqualTo));
        if ((object) dataType != (object) typeof (string) && (object) dataType != (object) typeof (char) && ((object) dataType != (object) typeof (bool) && (object) dataType != (object) typeof (ToggleState)) && (!dataType.IsEnum && (object) dataType != (object) typeof (Color)))
        {
          filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionGreaterThan", FilterOperator.IsGreaterThan));
          filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionLessThan", FilterOperator.IsLessThan));
          filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionGreaterThanOrEqualTo", FilterOperator.IsGreaterThanOrEqualTo));
          filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionLessThanOrEqualTo", FilterOperator.IsLessThanOrEqualTo));
        }
      }
      filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionIsNull", FilterOperator.IsNull));
      filterOperatorContextList.Add(new DataFilterOperatorContext("FilterFunctionNotIsNull", FilterOperator.IsNotNull));
      return filterOperatorContextList;
    }

    internal static IList<FilterOperator> GetFilterOperators(Type dataType)
    {
      List<DataFilterOperatorContext> filterOperations = DataFilterOperatorContext.GetFilterOperations(dataType);
      IList<FilterOperator> filterOperatorList = (IList<FilterOperator>) new List<FilterOperator>(filterOperations.Count);
      foreach (DataFilterOperatorContext filterOperatorContext in filterOperations)
        filterOperatorList.Add(filterOperatorContext.Operator);
      return filterOperatorList;
    }

    public static bool IsEditableFilterOperator(FilterOperator filterOperator)
    {
      if (filterOperator != FilterOperator.None && filterOperator != FilterOperator.IsNotNull)
        return filterOperator != FilterOperator.IsNull;
      return false;
    }

    internal static IList<DataFilterOperatorContext> GetContextFromOperators(
      IList<FilterOperator> list)
    {
      IList<DataFilterOperatorContext> filterOperatorContextList = (IList<DataFilterOperatorContext>) new List<DataFilterOperatorContext>(list.Count);
      foreach (FilterOperator filterOperator in (IEnumerable<FilterOperator>) list)
      {
        DataFilterOperatorContext filterOperatorContext = DataFilterOperatorContext.CreateDataFilterOperatorContext(filterOperator);
        if (filterOperatorContext != null)
          filterOperatorContextList.Add(filterOperatorContext);
      }
      return filterOperatorContextList;
    }

    private static DataFilterOperatorContext CreateDataFilterOperatorContext(
      FilterOperator filterOperator)
    {
      switch (filterOperator)
      {
        case FilterOperator.None:
          return new DataFilterOperatorContext("FilterFunctionNoFilter", FilterOperator.None);
        case FilterOperator.IsLessThan:
          return new DataFilterOperatorContext("FilterFunctionLessThan", FilterOperator.IsLessThan);
        case FilterOperator.IsLessThanOrEqualTo:
          return new DataFilterOperatorContext("FilterFunctionLessThanOrEqualTo", FilterOperator.IsLessThanOrEqualTo);
        case FilterOperator.IsEqualTo:
          return new DataFilterOperatorContext("FilterFunctionEqualTo", FilterOperator.IsEqualTo);
        case FilterOperator.IsNotEqualTo:
          return new DataFilterOperatorContext("FilterFunctionNotEqualTo", FilterOperator.IsNotEqualTo);
        case FilterOperator.IsGreaterThanOrEqualTo:
          return new DataFilterOperatorContext("FilterFunctionGreaterThanOrEqualTo", FilterOperator.IsGreaterThanOrEqualTo);
        case FilterOperator.IsGreaterThan:
          return new DataFilterOperatorContext("FilterFunctionGreaterThan", FilterOperator.IsGreaterThan);
        case FilterOperator.StartsWith:
          return new DataFilterOperatorContext("FilterFunctionStartsWith", FilterOperator.StartsWith);
        case FilterOperator.EndsWith:
          return new DataFilterOperatorContext("FilterFunctionEndsWith", FilterOperator.EndsWith);
        case FilterOperator.Contains:
          return new DataFilterOperatorContext("FilterFunctionContains", FilterOperator.Contains);
        case FilterOperator.NotContains:
          return new DataFilterOperatorContext("FilterFunctionDoesNotContain", FilterOperator.NotContains);
        case FilterOperator.IsNull:
          return new DataFilterOperatorContext("FilterFunctionIsNull", FilterOperator.IsNull);
        case FilterOperator.IsNotNull:
          return new DataFilterOperatorContext("FilterFunctionNotIsNull", FilterOperator.IsNotNull);
        case FilterOperator.IsContainedIn:
          return new DataFilterOperatorContext("FilterFunctionIsContainedIn", FilterOperator.IsContainedIn);
        case FilterOperator.IsNotContainedIn:
          return new DataFilterOperatorContext("FilterFunctionIsNotContainedIn", FilterOperator.IsNotContainedIn);
        default:
          return (DataFilterOperatorContext) null;
      }
    }
  }
}
