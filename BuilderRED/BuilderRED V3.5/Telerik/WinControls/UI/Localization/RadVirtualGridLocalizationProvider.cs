// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Localization.RadVirtualGridLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI.Localization
{
  public class RadVirtualGridLocalizationProvider : LocalizationProvider<RadVirtualGridLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "NoDataText":
          return "No data to display";
        case "FilterFunctionsBetween":
          return "Between";
        case "FilterFunctionContains":
          return "Contains";
        case "FilterFunctionDoesNotContain":
          return "Does not contain";
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
        case "FilterOperatorNoFilter":
          return "No filter";
        case "FilterOperatorCustom":
          return "Custom";
        case "FilterOperatorIsLike":
          return "Like";
        case "FilterOperatorNotIsLike":
          return "NotLike";
        case "FilterOperatorLessThan":
          return "LessThan";
        case "FilterOperatorLessThanOrEqualTo":
          return "LessThanOrEquals";
        case "FilterOperatorEqualTo":
          return "Equals";
        case "FilterOperatorNotEqualTo":
          return "NotEquals";
        case "FilterOperatorGreaterThanOrEqualTo":
          return "GreaterThanOrEquals";
        case "FilterOperatorGreaterThan":
          return "GreaterThan";
        case "FilterOperatorStartsWith":
          return "StartsWith";
        case "FilterOperatorEndsWith":
          return "EndsWith";
        case "FilterOperatorContains":
          return "Contains";
        case "FilterOperatorDoesNotContain":
          return "NotContains";
        case "FilterOperatorIsNull":
          return "IsNull";
        case "FilterOperatorNotIsNull":
          return "NotNull";
        case "FilterOperatorIsContainedIn":
          return "ContainedIn";
        case "FilterOperatorNotIsContainedIn":
          return "NotContainedIn";
        case "AddNewRowString":
          return "Click here to add a new row";
        case "PagingPanelPagesLabel":
          return "Page";
        case "PagingPanelOfPagesLabel":
          return "of";
        case "BestFitMenuItem":
          return "Best Fit";
        case "ClearSortingMenuItem":
          return "Clear Sorting";
        case "SortDescendingMenuItem":
          return "Sort Descending";
        case "SortAscendingMenuItem":
          return "Sort Ascending";
        case "PinAtRightMenuItem":
          return "Pin at right";
        case "PinAtLeftMenuItem":
          return "Pin at left";
        case "PinAtBottomMenuItem":
          return "Pin at bottom";
        case "PinAtTopMenuItem":
          return "Pin at top";
        case "UnpinColumnMenuItem":
          return "Unpin Column";
        case "UnpinRowMenuItem":
          return "Unpin Row";
        case "PinMenuItem":
          return "Pinned state";
        case "DeleteRowMenuItem":
          return "Delete Row";
        case "ClearValueMenuItem":
          return "Clear Value";
        case "EditMenuItem":
          return "Edit";
        case "PasteMenuItem":
          return "Paste";
        case "CutMenuItem":
          return "Cut";
        case "CopyMenuItem":
          return "Copy";
        default:
          return string.Empty;
      }
    }
  }
}
