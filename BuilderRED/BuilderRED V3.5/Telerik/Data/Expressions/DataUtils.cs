// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.DataUtils
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.Data.Expressions
{
  public class DataUtils
  {
    public static int CompareNulls(object xValue, object yValue)
    {
      return DataStorageHelper.CompareNulls(xValue, yValue);
    }

    public static ExpressionNode Parse(string expression, bool caseSensitive)
    {
      return ExpressionParser.Parse(expression, caseSensitive);
    }

    public static bool TryParse(
      string expression,
      bool caseSensitive,
      out ExpressionNode expressionNode)
    {
      return ExpressionParser.TryParse(expression, false, out expressionNode);
    }

    public static Exception CreateInvalidExpressionException(string message)
    {
      return (Exception) new InvalidExpressionException(message);
    }

    public static List<SortDescriptor> ParseSortString(string fieldName)
    {
      return DataStorageHelper.ParseSortString(fieldName);
    }

    public static string EscapeName(string name)
    {
      return DataStorageHelper.EscapeName(name);
    }

    public static string EscapeLikeValue(string name)
    {
      return DataStorageHelper.EscapeLikeValue(name);
    }

    public static object GetValue(
      PropertyDescriptorCollection descriptors,
      string propertyPath,
      object dataObject)
    {
      string[] strArray = propertyPath.Split('.');
      if (strArray.Length == 1)
        return descriptors[strArray[0]].GetValue(dataObject);
      PropertyDescriptor propertyDescriptor = descriptors[strArray[0]];
      object component = propertyDescriptor.GetValue(dataObject);
      for (int index = 1; index < strArray.Length && propertyDescriptor != null; ++index)
      {
        propertyDescriptor = propertyDescriptor.GetChildProperties()[strArray[index]];
        if (index + 1 != strArray.Length)
          component = propertyDescriptor.GetValue(component);
      }
      return propertyDescriptor.GetValue(component);
    }

    public static void SetValue(
      PropertyDescriptorCollection descriptors,
      string propertyPath,
      object dataObject,
      object value)
    {
      string[] strArray = propertyPath.Split('.');
      if (strArray.Length == 1)
      {
        descriptors[strArray[0]].SetValue(dataObject, value);
      }
      else
      {
        PropertyDescriptor propertyDescriptor = descriptors[strArray[0]];
        object component = propertyDescriptor.GetValue(dataObject);
        for (int index = 1; index < strArray.Length && propertyDescriptor != null; ++index)
        {
          propertyDescriptor = propertyDescriptor.GetChildProperties()[strArray[index]];
          if (index + 1 != strArray.Length)
            component = propertyDescriptor.GetValue(component);
        }
        propertyDescriptor?.SetValue(component, value);
      }
    }
  }
}
