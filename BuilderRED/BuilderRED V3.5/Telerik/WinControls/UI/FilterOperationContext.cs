// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterOperationContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class FilterOperationContext
  {
    private string name;
    private FilterOperator filterOperator;
    internal string id;

    public FilterOperationContext(string id, FilterOperator filterOperator)
    {
      this.id = id;
      this.name = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString(id);
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

    public static List<FilterOperationContext> GetFilterOperations(
      Type dataType)
    {
      Type underlyingType = Nullable.GetUnderlyingType(dataType);
      if ((object) underlyingType != null)
        dataType = underlyingType;
      List<FilterOperationContext> operationContextList = new List<FilterOperationContext>();
      operationContextList.Add(new FilterOperationContext("FilterFunctionNoFilter", FilterOperator.None));
      if ((object) dataType == (object) typeof (string) || (object) dataType == (object) typeof (Guid))
      {
        operationContextList.Add(new FilterOperationContext("FilterFunctionContains", FilterOperator.Contains));
        operationContextList.Add(new FilterOperationContext("FilterFunctionDoesNotContain", FilterOperator.NotContains));
        operationContextList.Add(new FilterOperationContext("FilterFunctionStartsWith", FilterOperator.StartsWith));
        operationContextList.Add(new FilterOperationContext("FilterFunctionEndsWith", FilterOperator.EndsWith));
      }
      if (GridViewHelper.IsNumeric(dataType) || (object) dataType == (object) typeof (DateTime) || ((object) dataType == (object) typeof (TimeSpan) || (object) dataType == (object) typeof (string)) || ((object) dataType == (object) typeof (char) || (object) dataType == (object) typeof (bool) || ((object) dataType == (object) typeof (ToggleState) || (object) dataType == (object) typeof (Color))) || ((object) dataType == (object) typeof (Guid) || dataType.IsEnum))
      {
        operationContextList.Add(new FilterOperationContext("FilterFunctionEqualTo", FilterOperator.IsEqualTo));
        operationContextList.Add(new FilterOperationContext("FilterFunctionNotEqualTo", FilterOperator.IsNotEqualTo));
        if ((object) dataType != (object) typeof (string) && (object) dataType != (object) typeof (char) && ((object) dataType != (object) typeof (bool) && (object) dataType != (object) typeof (ToggleState)) && (!dataType.IsEnum && (object) dataType != (object) typeof (Color)))
        {
          operationContextList.Add(new FilterOperationContext("FilterFunctionGreaterThan", FilterOperator.IsGreaterThan));
          operationContextList.Add(new FilterOperationContext("FilterFunctionLessThan", FilterOperator.IsLessThan));
          operationContextList.Add(new FilterOperationContext("FilterFunctionGreaterThanOrEqualTo", FilterOperator.IsGreaterThanOrEqualTo));
          operationContextList.Add(new FilterOperationContext("FilterFunctionLessThanOrEqualTo", FilterOperator.IsLessThanOrEqualTo));
        }
      }
      operationContextList.Add(new FilterOperationContext("FilterFunctionIsNull", FilterOperator.IsNull));
      operationContextList.Add(new FilterOperationContext("FilterFunctionNotIsNull", FilterOperator.IsNotNull));
      return operationContextList;
    }
  }
}
