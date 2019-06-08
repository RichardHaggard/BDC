// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterStringProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;
using System.Text;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public static class GridFilterStringProvider
  {
    public static string GetFilterString(FilterDescriptor descriptor)
    {
      return GridFilterStringProvider.GetFilterString(descriptor, (GridViewComboBoxColumn) null, 0);
    }

    public static string GetFilterString(
      FilterDescriptor descriptor,
      GridViewComboBoxColumn lookupColumn)
    {
      return GridFilterStringProvider.GetFilterString(descriptor, lookupColumn, 0);
    }

    public static string GetFilterString(
      FilterDescriptor descriptor,
      GridViewComboBoxColumn lookupColumn,
      int maxTextLength)
    {
      if (descriptor == null)
        return LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterOperatorNoFilter");
      StringBuilder sb = new StringBuilder();
      GridFilterStringProvider.CreateFilterStringValues(descriptor, ref sb, lookupColumn);
      if (maxTextLength > 0 && sb.Length > maxTextLength)
      {
        sb.Remove(maxTextLength, sb.Length - maxTextLength);
        sb.Append("...");
      }
      return sb.ToString();
    }

    private static void CreateFilterStringValues(
      FilterDescriptor descriptor,
      ref StringBuilder sb,
      GridViewComboBoxColumn lookupColumn)
    {
      if (descriptor is CompositeFilterDescriptor)
      {
        bool flag = false;
        CompositeFilterDescriptor filterDescriptor1 = (CompositeFilterDescriptor) descriptor;
        if (filterDescriptor1.NotOperator)
        {
          sb.Append(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterCompositeNotOperator"));
          sb.Append(" ");
          flag = true;
        }
        foreach (FilterDescriptor filterDescriptor2 in (Collection<FilterDescriptor>) filterDescriptor1.FilterDescriptors)
        {
          if (sb.Length > 0 && !flag)
          {
            string str = string.Empty;
            switch (filterDescriptor1.LogicalOperator)
            {
              case FilterLogicalOperator.And:
                str = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterLogicalOperatorAnd");
                break;
              case FilterLogicalOperator.Or:
                str = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterLogicalOperatorOr");
                break;
            }
            sb.Append(str);
            sb.Append(' ');
          }
          flag = false;
          GridFilterStringProvider.CreateFilterStringValues(filterDescriptor2, ref sb, lookupColumn);
        }
      }
      else
      {
        sb.Append(GridFilterStringProvider.GetLocalizedFilterOperator(descriptor.Operator));
        if (descriptor.Operator == FilterOperator.None || descriptor.Operator == FilterOperator.IsNull || descriptor.Operator == FilterOperator.IsNotNull)
          return;
        sb.Append(": \"");
        if (lookupColumn == null)
        {
          sb.Append(descriptor.Value);
        }
        else
        {
          object lookupValue = descriptor.Value;
          if (lookupColumn != null && lookupColumn.FilteringMode == GridViewFilteringMode.ValueMember)
            lookupValue = lookupColumn.GetLookupValue(descriptor.Value);
          if (lookupValue != null)
            sb.Append(lookupValue);
          else
            sb.Append(descriptor.Value);
        }
        sb.Append("\" ");
      }
    }

    private static string GetLocalizedFilterOperator(FilterOperator filterOperator)
    {
      RadGridLocalizationProvider currentProvider = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider;
      switch (filterOperator)
      {
        case FilterOperator.IsLike:
          return currentProvider.GetLocalizedString("FilterOperatorIsLike");
        case FilterOperator.IsNotLike:
          return currentProvider.GetLocalizedString("FilterOperatorNotIsLike");
        case FilterOperator.IsLessThan:
          return currentProvider.GetLocalizedString("FilterOperatorLessThan");
        case FilterOperator.IsLessThanOrEqualTo:
          return currentProvider.GetLocalizedString("FilterOperatorLessThanOrEqualTo");
        case FilterOperator.IsEqualTo:
          return currentProvider.GetLocalizedString("FilterOperatorEqualTo");
        case FilterOperator.IsNotEqualTo:
          return currentProvider.GetLocalizedString("FilterOperatorNotEqualTo");
        case FilterOperator.IsGreaterThanOrEqualTo:
          return currentProvider.GetLocalizedString("FilterOperatorGreaterThanOrEqualTo");
        case FilterOperator.IsGreaterThan:
          return currentProvider.GetLocalizedString("FilterOperatorGreaterThan");
        case FilterOperator.StartsWith:
          return currentProvider.GetLocalizedString("FilterOperatorStartsWith");
        case FilterOperator.EndsWith:
          return currentProvider.GetLocalizedString("FilterOperatorEndsWith");
        case FilterOperator.Contains:
          return currentProvider.GetLocalizedString("FilterOperatorContains");
        case FilterOperator.NotContains:
          return currentProvider.GetLocalizedString("FilterOperatorDoesNotContain");
        case FilterOperator.IsNull:
          return currentProvider.GetLocalizedString("FilterOperatorIsNull");
        case FilterOperator.IsNotNull:
          return currentProvider.GetLocalizedString("FilterOperatorNotIsNull");
        case FilterOperator.IsContainedIn:
          return currentProvider.GetLocalizedString("FilterOperatorIsContainedIn");
        case FilterOperator.IsNotContainedIn:
          return currentProvider.GetLocalizedString("FilterOperatorNotIsContainedIn");
        default:
          return LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterOperatorNoFilter");
      }
    }
  }
}
