// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class GridViewHelper
  {
    public static DateTime ClampDateTime(
      DateTime value,
      GridViewTimeFilteringMode filteringMode)
    {
      switch (filteringMode)
      {
        case GridViewTimeFilteringMode.Time:
          value = DateTime.MinValue.AddHours((double) value.Hour).AddMinutes((double) value.Minute);
          break;
        case GridViewTimeFilteringMode.Date:
          value = value.Date;
          break;
        case GridViewTimeFilteringMode.DateTime:
          value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, 0);
          break;
      }
      return value;
    }

    public static string GetCaption(PropertyDescriptor descriptor)
    {
      try
      {
        PropertyInfo property = descriptor.GetType().GetProperty("Column", BindingFlags.Instance | BindingFlags.NonPublic);
        if ((object) property == null)
          return (string) null;
        return (property.GetValue((object) descriptor, (object[]) null) as DataColumn).Caption;
      }
      catch
      {
        return (string) null;
      }
    }

    public static FilterOperator GetDefaultFilterOperator(System.Type dataType)
    {
      if ((object) dataType == (object) typeof (string))
        return FilterOperator.Contains;
      return GridViewHelper.IsNumeric(dataType) || (object) dataType == (object) typeof (DateTime) || (dataType.IsEnum || (object) dataType == (object) typeof (char)) ? FilterOperator.IsEqualTo : FilterOperator.None;
    }

    public static System.Type[] GetValidTypes(
      System.Type baseType,
      ITypeDiscoveryService typeResolutionService)
    {
      List<System.Type> typeList = new List<System.Type>();
      ICollection collection = typeResolutionService == null ? GridViewHelper.GetTypes(baseType) : typeResolutionService.GetTypes(baseType, false);
      typeList.Add(typeof (GridViewTextBoxColumn));
      foreach (System.Type type in (IEnumerable) collection)
      {
        if (type.GetCustomAttributes(typeof (ObsoleteAttribute), true).Length <= 0 && (object) type != (object) typeof (GridViewTextBoxColumn) && (object) type != (object) baseType)
          typeList.Add(type);
      }
      return typeList.ToArray();
    }

    public static ICollection GetTypes(System.Type baseType)
    {
      ArrayList arrayList = new ArrayList();
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (!assembly.FullName.StartsWith("System"))
        {
          try
          {
            foreach (System.Type type in assembly.GetTypes())
            {
              if ((object) type != null && type.IsSubclassOf(baseType))
                arrayList.Add((object) type);
            }
          }
          catch
          {
          }
        }
      }
      return (ICollection) arrayList;
    }

    public static void ShowErrorDialog(IUIService uiService, Exception ex)
    {
      if (uiService != null)
      {
        uiService.ShowError(ex);
      }
      else
      {
        string message = ex.Message;
        if (message == null || message.Length == 0)
          message = ex.ToString();
        int num = (int) MessageBox.Show(message, (string) null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions) 0);
      }
    }

    public static string GetBindingSourceNamePrefix(object dataSource, string dataMember)
    {
      if (!string.IsNullOrEmpty(dataMember))
        return dataMember;
      if (dataSource == null)
        return "";
      System.Type type = dataSource as System.Type;
      if ((object) type != null)
        return type.Name;
      IComponent component = dataSource as IComponent;
      if (component != null)
      {
        ISite site = component.Site;
        if (site != null && !string.IsNullOrEmpty(site.Name))
          return site.Name;
      }
      return dataSource.GetType().Name;
    }

    public static string BuildNameFromText(
      string text,
      System.Type componentType,
      System.IServiceProvider serviceProvider)
    {
      if (serviceProvider == null)
        return (string) null;
      INameCreationService service1 = serviceProvider.GetService(typeof (INameCreationService)) as INameCreationService;
      IContainer service2 = (IContainer) serviceProvider.GetService(typeof (IContainer));
      if (service1 == null || service2 == null)
        return (string) null;
      string name1 = service1.CreateName(service2, componentType);
      if (text == null || text.Length == 0 || text == "-")
        return name1;
      string name2 = componentType.Name;
      StringBuilder stringBuilder = new StringBuilder(text.Length + name2.Length);
      bool flag = false;
      for (int index = 0; index < text.Length; ++index)
      {
        char c = text[index];
        if (flag)
        {
          if (char.IsLower(c))
            c = char.ToUpper(c, CultureInfo.CurrentCulture);
          flag = false;
        }
        if (char.IsLetterOrDigit(c))
        {
          if (stringBuilder.Length == 0)
          {
            if (!char.IsDigit(c))
            {
              if (char.IsLower(c) != char.IsLower(name1[0]))
                c = !char.IsLower(c) ? char.ToLower(c, CultureInfo.CurrentCulture) : char.ToUpper(c, CultureInfo.CurrentCulture);
            }
            else
              continue;
          }
          stringBuilder.Append(c);
        }
        else if (char.IsWhiteSpace(c))
          flag = true;
      }
      if (stringBuilder.Length == 0)
        return name1;
      stringBuilder.Append(name2);
      string name3 = stringBuilder.ToString();
      if (service2.Components[name3] == null)
      {
        if (!service1.IsValidName(name3))
          return name1;
        return name3;
      }
      string name4 = name3;
      int num = 1;
      while (!service1.IsValidName(name4) || service2.Components[name4] != null)
      {
        name4 = name3 + num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        ++num;
      }
      return name4;
    }

    public static string GetUniqueName(GridViewColumnCollection collection, string baseName)
    {
      if (string.IsNullOrEmpty(baseName))
        return string.Empty;
      if (collection[baseName] == null && char.IsNumber(baseName[baseName.Length - 1]))
        return baseName;
      int num = 1;
      while (collection.Contains(string.Format("{0}{1}", (object) baseName, (object) num)))
        ++num;
      return baseName + (object) num;
    }

    public static string GetUniqueName(ColumnGroupCollection collection, string baseName)
    {
      if (string.IsNullOrEmpty(baseName))
        return string.Empty;
      if (collection[baseName] == null && char.IsNumber(baseName[baseName.Length - 1]))
        return baseName;
      int num = 1;
      while (collection.Contains(string.Format("{0}{1}", (object) baseName, (object) num)))
        ++num;
      return baseName + (object) num;
    }

    public static bool IsUniqueName(GridViewColumnCollection collection, string name)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
          return false;
      }
      return true;
    }

    public static GridViewDataColumn AutoGenerateGridColumn(
      System.Type columnType,
      ISite site)
    {
      if (columnType.IsGenericType && (object) columnType.GetGenericTypeDefinition() == (object) typeof (Nullable<>))
        columnType = Nullable.GetUnderlyingType(columnType);
      GridViewDataColumn gridViewDataColumn;
      if ((object) columnType == (object) typeof (bool) || (object) columnType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState))
        gridViewDataColumn = (GridViewDataColumn) new GridViewCheckBoxColumn();
      else if ((object) columnType == (object) typeof (byte[]) || (object) columnType == (object) typeof (Image) || ((object) columnType == (object) typeof (Icon) || (object) columnType == (object) typeof (Bitmap)))
        gridViewDataColumn = (GridViewDataColumn) new GridViewImageColumn();
      else if ((object) columnType == (object) typeof (DateTime))
        gridViewDataColumn = (GridViewDataColumn) new GridViewDateTimeColumn();
      else if (GridViewHelper.IsNumeric(columnType) || GridViewHelper.IsNumericSql(columnType))
      {
        gridViewDataColumn = (GridViewDataColumn) new GridViewDecimalColumn();
        gridViewDataColumn.DataType = columnType;
      }
      else if ((object) columnType == (object) typeof (Color))
        gridViewDataColumn = (GridViewDataColumn) new GridViewColorColumn();
      else if (columnType.IsEnum)
      {
        GridViewComboBoxColumn viewComboBoxColumn = new GridViewComboBoxColumn();
        (EnumBinder) columnType.Target = (object) viewComboBoxColumn;
        if (site != null)
        {
          IDesignerHost service = (IDesignerHost) site.GetService(typeof (IDesignerHost));
          if (service != null)
          {
            EnumBinder component = (EnumBinder) service.CreateComponent(typeof (EnumBinder));
            component.Source = columnType;
            component.Target = (object) viewComboBoxColumn;
          }
        }
        viewComboBoxColumn.DataType = columnType;
        gridViewDataColumn = (GridViewDataColumn) viewComboBoxColumn;
      }
      else
        gridViewDataColumn = (GridViewDataColumn) new GridViewTextBoxColumn();
      return gridViewDataColumn;
    }

    public static PropertyDescriptor FindSubPropertyDescriptor(
      PropertyDescriptorCollection descriptors,
      string fieldName)
    {
      string[] strArray = fieldName.Split('.');
      if (strArray.Length < 2)
        return (PropertyDescriptor) null;
      PropertyDescriptor propertyDescriptor = descriptors.Find(strArray[0], true);
      for (int index = 1; index < strArray.Length && propertyDescriptor != null; ++index)
        propertyDescriptor = propertyDescriptor.GetChildProperties().Find(strArray[index], true);
      return propertyDescriptor;
    }

    public static bool ContainsInnerDescriptor(
      PropertyDescriptorCollection descriptors,
      string fieldName)
    {
      return GridViewHelper.FindSubPropertyDescriptor(descriptors, fieldName) != null;
    }

    public static bool IsInteger(System.Type type)
    {
      if ((object) type != (object) typeof (short) && (object) type != (object) typeof (int) && ((object) type != (object) typeof (long) && (object) type != (object) typeof (ushort)) && ((object) type != (object) typeof (uint) && (object) type != (object) typeof (ulong) && (object) type != (object) typeof (sbyte)))
        return (object) type == (object) typeof (byte);
      return true;
    }

    public static bool IsIntegerSql(System.Type type)
    {
      if ((object) type != (object) typeof (short) && (object) type != (object) typeof (int) && ((object) type != (object) typeof (long) && (object) type != (object) typeof (ushort)) && ((object) type != (object) typeof (uint) && (object) type != (object) typeof (ulong) && ((object) type != (object) typeof (sbyte) && (object) type != (object) typeof (byte))) && ((object) type != (object) typeof (SqlInt64) && (object) type != (object) typeof (SqlInt32) && (object) type != (object) typeof (SqlInt16)))
        return (object) type == (object) typeof (SqlByte);
      return true;
    }

    public static bool IsFloat(System.Type type)
    {
      if ((object) type != (object) typeof (float) && (object) type != (object) typeof (double))
        return (object) type == (object) typeof (Decimal);
      return true;
    }

    public static bool IsFloatSql(System.Type type)
    {
      if ((object) type != (object) typeof (float) && (object) type != (object) typeof (double) && ((object) type != (object) typeof (Decimal) && (object) type != (object) typeof (SqlDouble)) && ((object) type != (object) typeof (SqlDecimal) && (object) type != (object) typeof (SqlMoney)))
        return (object) type == (object) typeof (SqlSingle);
      return true;
    }

    public static bool IsNumeric(System.Type type)
    {
      if (!GridViewHelper.IsFloat(type))
        return GridViewHelper.IsInteger(type);
      return true;
    }

    internal static bool IsNumericSql(System.Type type)
    {
      if (!GridViewHelper.IsFloatSql(type))
        return GridViewHelper.IsIntegerSql(type);
      return true;
    }

    public static bool IsSigned(System.Type type)
    {
      if ((object) type != (object) typeof (short) && (object) type != (object) typeof (int) && ((object) type != (object) typeof (long) && (object) type != (object) typeof (sbyte)))
        return GridViewHelper.IsFloat(type);
      return true;
    }

    public static bool IsSignedSql(System.Type type)
    {
      if ((object) type != (object) typeof (short) && (object) type != (object) typeof (int) && ((object) type != (object) typeof (long) && (object) type != (object) typeof (sbyte)) && ((object) type != (object) typeof (SqlInt64) && (object) type != (object) typeof (SqlInt32) && (object) type != (object) typeof (SqlInt16)))
        return GridViewHelper.IsFloatSql(type);
      return true;
    }

    public static bool IsUnsigned(System.Type type)
    {
      if ((object) type != (object) typeof (ushort) && (object) type != (object) typeof (uint) && (object) type != (object) typeof (ulong))
        return (object) type == (object) typeof (byte);
      return true;
    }

    public static bool IsUnsignedSql(System.Type type)
    {
      if ((object) type != (object) typeof (ushort) && (object) type != (object) typeof (uint) && ((object) type != (object) typeof (ulong) && (object) type != (object) typeof (SqlByte)))
        return (object) type == (object) typeof (byte);
      return true;
    }

    public static bool IsBindableType(System.Type type)
    {
      if (!type.IsPrimitive && (object) type != (object) typeof (string) && ((object) type != (object) typeof (DateTime) && (object) type != (object) typeof (Guid)) && ((object) type != (object) typeof (Decimal) && (object) type != (object) typeof (byte[])))
        return type.IsEnum;
      return true;
    }

    public static RadSortOrder GetSortDirection(ListSortDirection listSortDirection)
    {
      switch (listSortDirection)
      {
        case ListSortDirection.Ascending:
          return RadSortOrder.Ascending;
        case ListSortDirection.Descending:
          return RadSortOrder.Descending;
        default:
          return RadSortOrder.None;
      }
    }

    internal static ListSortDirection GetSortDirection(RadSortOrder radSortOrder)
    {
      switch (radSortOrder)
      {
        case RadSortOrder.Ascending:
          return ListSortDirection.Ascending;
        case RadSortOrder.Descending:
          return ListSortDirection.Descending;
        default:
          return ListSortDirection.Ascending;
      }
    }

    internal static FilterLogicalOperator GetLogicalOperator(
      FilterExpression.BinaryOperation binaryOperation)
    {
      return binaryOperation == FilterExpression.BinaryOperation.OR ? FilterLogicalOperator.Or : FilterLogicalOperator.And;
    }

    internal static FilterOperator GetFilterOperator(GridKnownFunction function)
    {
      switch (function)
      {
        case GridKnownFunction.Contains:
          return FilterOperator.Contains;
        case GridKnownFunction.DoesNotContain:
          return FilterOperator.NotContains;
        case GridKnownFunction.StartsWith:
          return FilterOperator.StartsWith;
        case GridKnownFunction.EndsWith:
          return FilterOperator.EndsWith;
        case GridKnownFunction.EqualTo:
          return FilterOperator.IsEqualTo;
        case GridKnownFunction.NotEqualTo:
          return FilterOperator.IsNotEqualTo;
        case GridKnownFunction.GreaterThan:
          return FilterOperator.IsGreaterThan;
        case GridKnownFunction.LessThan:
          return FilterOperator.IsLessThan;
        case GridKnownFunction.GreaterThanOrEqualTo:
          return FilterOperator.IsGreaterThanOrEqualTo;
        case GridKnownFunction.LessThanOrEqualTo:
          return FilterOperator.IsLessThanOrEqualTo;
        case GridKnownFunction.IsEmpty:
          return FilterOperator.IsLike;
        case GridKnownFunction.NotIsEmpty:
          return FilterOperator.IsNotLike;
        case GridKnownFunction.IsNull:
          return FilterOperator.IsNull;
        case GridKnownFunction.NotIsNull:
          return FilterOperator.IsNotNull;
        default:
          return FilterOperator.None;
      }
    }

    internal static FilterExpression.BinaryOperation GetLogicalOperator(
      FilterLogicalOperator filterLogicalOperator)
    {
      return filterLogicalOperator == FilterLogicalOperator.Or ? FilterExpression.BinaryOperation.OR : FilterExpression.BinaryOperation.AND;
    }
  }
}
