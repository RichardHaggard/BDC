// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Formatter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class Formatter
  {
    private static System.Type stringType = typeof (string);
    private static System.Type booleanType = typeof (bool);
    private static System.Type checkStateType = typeof (CheckState);
    private static object parseMethodNotFound = new object();
    private static object defaultDataSourceNullValue = (object) DBNull.Value;

    private static object ChangeType(object value, System.Type type, IFormatProvider formatInfo)
    {
      try
      {
        if (formatInfo == null)
          formatInfo = (IFormatProvider) CultureInfo.CurrentCulture;
        return Convert.ChangeType(value, type, formatInfo);
      }
      catch (InvalidCastException ex)
      {
        throw new FormatException(ex.Message, (Exception) ex);
      }
    }

    private static bool EqualsFormattedNullValue(
      object value,
      object formattedNullValue,
      IFormatProvider formatInfo)
    {
      if (formattedNullValue is string && value is string)
        return string.Compare((string) value, (string) formattedNullValue, true, Formatter.GetFormatterCulture(formatInfo)) == 0;
      return object.Equals(value, formattedNullValue);
    }

    public static object FormatObject(
      object value,
      System.Type targetType,
      TypeConverter sourceConverter,
      TypeConverter targetConverter,
      string formatString,
      IFormatProvider formatInfo,
      object formattedNullValue,
      object dataSourceNullValue)
    {
      if (Formatter.IsNullData(value, dataSourceNullValue))
        value = (object) DBNull.Value;
      System.Type type = targetType;
      targetType = Formatter.NullableUnwrap(targetType);
      sourceConverter = Formatter.NullableUnwrap(sourceConverter);
      targetConverter = Formatter.NullableUnwrap(targetConverter);
      bool flag = (object) targetType != (object) type;
      object obj = Formatter.FormatObjectInternal(value, targetType, sourceConverter, targetConverter, formatString, formatInfo, formattedNullValue);
      if (type.IsValueType && obj == null && !flag)
        throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
      return obj;
    }

    private static object FormatObjectInternal(
      object value,
      System.Type targetType,
      TypeConverter sourceConverter,
      TypeConverter targetConverter,
      string formatString,
      IFormatProvider formatInfo,
      object formattedNullValue)
    {
      if (value == DBNull.Value || value == null)
      {
        if (formattedNullValue != null)
          return formattedNullValue;
        if ((object) targetType == (object) Formatter.stringType)
          return (object) string.Empty;
        if ((object) targetType == (object) Formatter.checkStateType)
          return (object) CheckState.Indeterminate;
        return (object) null;
      }
      if ((object) targetType == (object) Formatter.stringType && value is IFormattable && !string.IsNullOrEmpty(formatString))
        return (object) (value as IFormattable).ToString(formatString, formatInfo);
      System.Type type = value.GetType();
      TypeConverter converter1 = TypeDescriptor.GetConverter(type);
      if (sourceConverter != null && sourceConverter != converter1 && sourceConverter.CanConvertTo(targetType))
        return sourceConverter.ConvertTo((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
      TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
      if (targetConverter != null && targetConverter != converter2 && targetConverter.CanConvertFrom(type))
        return targetConverter.ConvertFrom((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value);
      if ((object) targetType == (object) Formatter.checkStateType)
      {
        if ((object) type == (object) Formatter.booleanType)
          return (object) (CheckState) ((bool) value ? 1 : 0);
        if (sourceConverter == null)
          sourceConverter = converter1;
        if (sourceConverter != null && sourceConverter.CanConvertTo(Formatter.booleanType))
          return (object) (CheckState) ((bool) sourceConverter.ConvertTo((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value, Formatter.booleanType) ? 1 : 0);
      }
      if (targetType.IsAssignableFrom(type))
        return value;
      if (sourceConverter == null)
        sourceConverter = converter1;
      if (targetConverter == null)
        targetConverter = converter2;
      if (sourceConverter != null && sourceConverter.CanConvertTo(targetType))
        return sourceConverter.ConvertTo((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
      if (targetConverter != null && targetConverter.CanConvertFrom(type))
        return targetConverter.ConvertFrom((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value);
      if (!(value is IConvertible))
        throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
      return Formatter.ChangeType(value, targetType, formatInfo);
    }

    private static string GetCantConvertMessage(object value, System.Type targetType)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, value == null ? "Formatter Cant Convert Null" : "Formatter Cant Convert", value, (object) targetType.Name);
    }

    public static object GetDefaultDataSourceNullValue(System.Type type)
    {
      if ((object) type != null && !type.IsValueType)
        return (object) null;
      return Formatter.defaultDataSourceNullValue;
    }

    private static CultureInfo GetFormatterCulture(IFormatProvider formatInfo)
    {
      if (formatInfo is CultureInfo)
        return formatInfo as CultureInfo;
      return CultureInfo.CurrentCulture;
    }

    public static object InvokeStringParseMethod(
      object value,
      System.Type targetType,
      IFormatProvider formatInfo)
    {
      try
      {
        System.Type[] types1 = new System.Type[3]
        {
          Formatter.stringType,
          typeof (NumberStyles),
          typeof (IFormatProvider)
        };
        MethodInfo method1 = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, (Binder) null, types1, (ParameterModifier[]) null);
        if ((object) method1 != null)
        {
          object[] parameters = new object[3]
          {
            (object) (string) value,
            (object) NumberStyles.Any,
            (object) formatInfo
          };
          return method1.Invoke((object) null, parameters);
        }
        System.Type[] types2 = new System.Type[2]
        {
          Formatter.stringType,
          typeof (IFormatProvider)
        };
        MethodInfo method2 = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, (Binder) null, types2, (ParameterModifier[]) null);
        if ((object) method2 != null)
        {
          object[] parameters = new object[2]
          {
            (object) (string) value,
            (object) formatInfo
          };
          return method2.Invoke((object) null, parameters);
        }
        System.Type[] types3 = new System.Type[1]
        {
          Formatter.stringType
        };
        MethodInfo method3 = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, (Binder) null, types3, (ParameterModifier[]) null);
        if ((object) method3 == null)
          return Formatter.parseMethodNotFound;
        object[] parameters1 = new object[1]
        {
          (object) (string) value
        };
        return method3.Invoke((object) null, parameters1);
      }
      catch (TargetInvocationException ex)
      {
        throw new FormatException(ex.InnerException.Message, ex.InnerException);
      }
    }

    public static bool IsNullData(object value, object dataSourceNullValue)
    {
      if (value != null && value != DBNull.Value)
        return object.Equals(value, Formatter.NullData(value.GetType(), dataSourceNullValue));
      return true;
    }

    private static TypeConverter NullableUnwrap(TypeConverter typeConverter)
    {
      NullableConverter nullableConverter = typeConverter as NullableConverter;
      if (nullableConverter == null)
        return typeConverter;
      return nullableConverter.UnderlyingTypeConverter;
    }

    private static System.Type NullableUnwrap(System.Type type)
    {
      if ((object) type == (object) Formatter.stringType)
        return Formatter.stringType;
      System.Type underlyingType = Nullable.GetUnderlyingType(type);
      if ((object) underlyingType != null)
        return underlyingType;
      return type;
    }

    public static object NullData(System.Type type, object dataSourceNullValue)
    {
      if (!type.IsGenericType || (object) type.GetGenericTypeDefinition() != (object) typeof (Nullable<>) || dataSourceNullValue != null && dataSourceNullValue != DBNull.Value)
        return dataSourceNullValue;
      return (object) null;
    }

    public static object ParseObject(
      object value,
      System.Type targetType,
      System.Type sourceType,
      TypeConverter targetConverter,
      TypeConverter sourceConverter,
      IFormatProvider formatInfo,
      object formattedNullValue,
      object dataSourceNullValue)
    {
      System.Type type = targetType;
      sourceType = Formatter.NullableUnwrap(sourceType);
      targetType = Formatter.NullableUnwrap(targetType);
      sourceConverter = Formatter.NullableUnwrap(sourceConverter);
      targetConverter = Formatter.NullableUnwrap(targetConverter);
      object objectInternal = Formatter.ParseObjectInternal(value, targetType, sourceType, targetConverter, sourceConverter, formatInfo, formattedNullValue);
      if (objectInternal == DBNull.Value)
        return Formatter.NullData(type, dataSourceNullValue);
      return objectInternal;
    }

    private static object ParseObjectInternal(
      object value,
      System.Type targetType,
      System.Type sourceType,
      TypeConverter targetConverter,
      TypeConverter sourceConverter,
      IFormatProvider formatInfo,
      object formattedNullValue)
    {
      if (Formatter.EqualsFormattedNullValue(value, formattedNullValue, formatInfo) || value == DBNull.Value)
        return (object) DBNull.Value;
      TypeConverter converter1 = TypeDescriptor.GetConverter(targetType);
      if (targetConverter != null && converter1 != targetConverter && targetConverter.CanConvertFrom(sourceType))
        return targetConverter.ConvertFrom((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value);
      TypeConverter converter2 = TypeDescriptor.GetConverter(sourceType);
      if (sourceConverter != null && converter2 != sourceConverter && sourceConverter.CanConvertTo(targetType))
        return sourceConverter.ConvertTo((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
      if (value is string)
      {
        object method = Formatter.InvokeStringParseMethod(value, targetType, formatInfo);
        if (method != Formatter.parseMethodNotFound)
          return method;
      }
      else if (value is CheckState)
      {
        CheckState checkState = (CheckState) value;
        if (checkState == CheckState.Indeterminate)
          return (object) DBNull.Value;
        if ((object) targetType == (object) Formatter.booleanType)
          return (object) (checkState == CheckState.Checked);
        if (targetConverter == null)
          targetConverter = converter1;
        if (targetConverter != null && targetConverter.CanConvertFrom(Formatter.booleanType))
          return targetConverter.ConvertFrom((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), (object) (checkState == CheckState.Checked));
      }
      else if (value != null && targetType.IsAssignableFrom(value.GetType()))
        return value;
      if (targetConverter == null)
        targetConverter = converter1;
      if (sourceConverter == null)
        sourceConverter = converter2;
      if (targetConverter != null && targetConverter.CanConvertFrom(sourceType))
        return targetConverter.ConvertFrom((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value);
      if (sourceConverter != null && sourceConverter.CanConvertTo(targetType))
        return sourceConverter.ConvertTo((ITypeDescriptorContext) null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
      if (!(value is IConvertible))
        throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
      return Formatter.ChangeType(value, targetType, formatInfo);
    }
  }
}
