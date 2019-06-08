// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class DataFilterLocalizationProvider : LocalizationProvider<DataFilterLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "LogicalOperatorAnd":
          return "All";
        case "LogicalOperatorOr":
          return "Any";
        case "LogicalOperatorDescription":
          return "of the following are true";
        case "FieldNullText":
          return "Choose field";
        case "ValueElementNullText":
          return "Enter a value";
        case "AddNewButtonText":
          return "Add";
        case "AddNewButtonExpression":
          return "Expression";
        case "AddNewButtonGroup":
          return "Group";
        case "DialogTitle":
          return "Data Filter";
        case "DialogOKButton":
          return "OK";
        case "DialogCancelButton":
          return "Cancel";
        case "DialogApplyButton":
          return "Apply";
        case "ErrorAddNodeDialogTitle":
          return "RadDataFilter Error";
        case "ErrorAddNodeDialogText":
          return "Cannot add entries to the control - missing property descriptors. \nDataSource is not set and/or DataFilterDescriptorItems are not added to the Descriptors collection of the control.";
        case "FilterFunctionsBetween":
          return "Between";
        case "FilterFunctionContains":
          return "Contains";
        case "FilterFunctionDoesNotContain":
          return "Does not contain";
        case "FilterFunctionIsContainedIn":
          return "Is in list";
        case "FilterFunctionIsNotContainedIn":
          return "Not in list";
        case "FilterFunctionEndsWith":
          return "Ends with";
        case "FilterFunctionEqualTo":
          return "Equals";
        case "FilterFunctionGreaterThan":
          return "Greater than";
        case "FilterFunctionGreaterThanOrEqualTo":
          return "Greater than or equal to";
        case "FilterFunctionIsEmpty":
          return "Is empty";
        case "FilterFunctionIsNull":
          return "Is null";
        case "FilterFunctionLessThan":
          return "Less than";
        case "FilterFunctionLessThanOrEqualTo":
          return "Less than or equal to";
        case "FilterFunctionNoFilter":
          return "No filter";
        case "FilterFunctionNotBetween":
          return "Not between";
        case "FilterFunctionNotEqualTo":
          return "Not equal to";
        case "FilterFunctionNotIsEmpty":
          return "Is not empty";
        case "FilterFunctionNotIsNull":
          return "Is not null";
        case "FilterFunctionStartsWith":
          return "Starts with";
        case "FilterFunctionsCustom":
          return "Custom";
        default:
          return string.Empty;
      }
    }
  }
}
