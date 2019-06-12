// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDataConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class RadDataConverter
  {
    private static object SyncRoot = new object();
    private static readonly List<string> trueValues = new List<string>((IEnumerable<string>) new string[4]
    {
      "true",
      "t",
      "on",
      "1"
    });
    private static RadDataConverter instance;
    private readonly Type StringType;
    private readonly Type BooleanType;
    private readonly Type ToggleStateType;
    private readonly Type ColorType;

    private RadDataConverter()
    {
      this.StringType = typeof (string);
      this.BooleanType = typeof (bool);
      this.ToggleStateType = typeof (ToggleState);
      this.ColorType = typeof (Color);
    }

    public static RadDataConverter Instance
    {
      get
      {
        if (RadDataConverter.instance == null)
        {
          lock (RadDataConverter.SyncRoot)
          {
            if (RadDataConverter.instance == null)
              RadDataConverter.instance = new RadDataConverter();
          }
        }
        return RadDataConverter.instance;
      }
    }

    public object Format(
      object value,
      Type targetType,
      IDataConversionInfoProvider converstionInfoProvider)
    {
      return this.Format(value, targetType, true, converstionInfoProvider);
    }

    public object Format(
      object value,
      Type targetType,
      bool coerceNullValue,
      IDataConversionInfoProvider converstionInfoProvider)
    {
      if (converstionInfoProvider == null)
        return value;
      bool flag = (object) targetType == (object) this.StringType && !(value is string);
      CultureInfo formatterCulture = this.GetFormatterCulture((IFormatProvider) converstionInfoProvider.FormatInfo);
      if (flag)
        targetType = converstionInfoProvider.DataType;
      try
      {
        value = this.FormatCore(value, targetType, formatterCulture, converstionInfoProvider, coerceNullValue);
        if (flag)
          value = this.FormatCore(value, this.StringType, formatterCulture, converstionInfoProvider, false);
      }
      catch (Exception ex)
      {
        this.ThrowFormatException(ex);
      }
      return value;
    }

    public Exception TryFormat(
      object value,
      Type targetType,
      IDataConversionInfoProvider converstionInfoProvider,
      out object result)
    {
      try
      {
        result = this.Format(value, targetType, converstionInfoProvider);
        return (Exception) null;
      }
      catch (Exception ex)
      {
        result = (object) null;
        return ex;
      }
    }

    public object Parse(
      IDataConversionInfoProvider converstionInfoProvider,
      object value)
    {
      if (converstionInfoProvider == null)
        return value;
      Type sourceType1 = this.NullableUnwrap(value?.GetType());
      Type type = this.NullableUnwrap(converstionInfoProvider.DataType);
      TypeConverter dataTypeConverter = this.NullableUnwrap(converstionInfoProvider.DataTypeConverter);
      object obj = (object) Nullable.GetUnderlyingType(converstionInfoProvider.DataType) == null || value != null && value != DBNull.Value ? (value != null && !string.IsNullOrEmpty(Convert.ToString(value)) || (object) Nullable.GetUnderlyingType(converstionInfoProvider.DataType) == null ? this.ParseCore(value, type, sourceType1, dataTypeConverter, converstionInfoProvider, true) : converstionInfoProvider.DataTypeConverter.ConvertFrom(converstionInfoProvider as ITypeDescriptorContext, converstionInfoProvider.FormatInfo, value)) : (object) null;
      if (obj == null)
      {
        obj = this.NullData(type, converstionInfoProvider.DataSourceNullValue);
        if (obj != null)
        {
          Type sourceType2 = this.NullableUnwrap(obj.GetType());
          obj = this.ParseCore(obj, type, sourceType2, dataTypeConverter, converstionInfoProvider, false);
        }
      }
      return obj;
    }

    public Exception TryParse(
      IDataConversionInfoProvider converstionInfoProvider,
      object value,
      out object result)
    {
      try
      {
        result = this.Parse(converstionInfoProvider, value);
        return (Exception) null;
      }
      catch (Exception ex)
      {
        result = (object) null;
        return ex;
      }
    }

    private object FormatCore(
      object value,
      Type targetType,
      CultureInfo cultureInfo,
      IDataConversionInfoProvider dataColumn,
      bool checkForNullValue)
    {
      if (Convert.IsDBNull(value))
        value = (object) null;
      targetType = this.NullableUnwrap(targetType);
      TypeConverter dataTypeConverter = this.NullableUnwrap(dataColumn.DataTypeConverter);
      object result = (object) null;
      if (checkForNullValue && this.FormatNullValue(value, targetType, cultureInfo, dataTypeConverter, dataColumn, out result))
        return result;
      if (value == null)
        return (object) null;
      Type type = value.GetType();
      TypeConverter converter1 = this.GetConverter(type);
      if (dataTypeConverter != null && dataTypeConverter != converter1)
      {
        if (dataTypeConverter.CanConvertTo(dataColumn as ITypeDescriptorContext, targetType))
        {
          try
          {
            return dataTypeConverter.ConvertTo(dataColumn as ITypeDescriptorContext, cultureInfo, value, targetType);
          }
          catch (FormatException ex)
          {
            if ((object) type == (object) targetType)
              return value;
            throw ex;
          }
        }
      }
      if ((object) targetType == (object) this.ToggleStateType && this.FormatToggleStateValue(value, type, dataTypeConverter, dataColumn, converter1, cultureInfo, out result))
        return result;
      if ((object) targetType == (object) this.StringType)
        return (object) this.FormatString(cultureInfo, dataColumn.FormatString, value);
      if (targetType.IsAssignableFrom(type))
        return value;
      if (converter1 != null && converter1 != dataTypeConverter)
      {
        if (converter1.CanConvertTo(dataColumn as ITypeDescriptorContext, targetType))
        {
          try
          {
            return converter1.ConvertTo(dataColumn as ITypeDescriptorContext, cultureInfo, value, targetType);
          }
          catch (FormatException ex)
          {
            if ((object) type == (object) targetType)
              return value;
            throw ex;
          }
        }
      }
      TypeConverter converter2 = this.GetConverter(targetType);
      if (converter2 != null && converter2.CanConvertFrom(dataColumn as ITypeDescriptorContext, type))
        return converter2.ConvertFrom(dataColumn as ITypeDescriptorContext, cultureInfo, value);
      return this.ChangeType(value, targetType, (IFormatProvider) cultureInfo);
    }

    private bool FormatToggleStateValue(
      object value,
      Type type,
      TypeConverter dataTypeConverter,
      IDataConversionInfoProvider dataColumn,
      TypeConverter valueConverter,
      CultureInfo cultureInfo,
      out object result)
    {
      result = (object) null;
      if ((object) type == (object) this.BooleanType)
      {
        result = (object) (ToggleState) ((bool) value ? 1 : 0);
        return true;
      }
      if ((object) type == (object) this.StringType && value != null)
      {
        string lower = value.ToString().ToLower();
        result = (object) (ToggleState) (RadDataConverter.trueValues.Contains(lower) ? 1 : 0);
        return true;
      }
      if (dataTypeConverter == null)
        dataTypeConverter = valueConverter;
      if (dataTypeConverter == null || !dataTypeConverter.CanConvertTo(dataColumn as ITypeDescriptorContext, this.BooleanType))
        return false;
      bool flag = (bool) dataTypeConverter.ConvertTo(dataColumn as ITypeDescriptorContext, cultureInfo, value, this.BooleanType);
      result = (object) (ToggleState) (flag ? 1 : 0);
      return true;
    }

    private string FormatString(CultureInfo cultureInfo, string formatString, object value)
    {
      if (string.IsNullOrEmpty(formatString))
        return Convert.ToString(value, (IFormatProvider) cultureInfo);
      return string.Format((IFormatProvider) cultureInfo, formatString, value);
    }

    private bool FormatNullValue(
      object value,
      Type targetType,
      CultureInfo cultureInfo,
      TypeConverter dataTypeConverter,
      IDataConversionInfoProvider column,
      out object result)
    {
      result = (object) null;
      if (value == DBNull.Value || value == null)
      {
        if (column.NullValue != null)
        {
          result = this.FormatCore(column.NullValue, targetType, cultureInfo, column, false);
          return true;
        }
        if ((object) targetType == (object) this.StringType)
        {
          result = (object) string.Empty;
          return true;
        }
        if ((object) targetType == (object) this.ToggleStateType)
        {
          result = (object) ToggleState.Indeterminate;
          return true;
        }
        if ((object) targetType == (object) this.ColorType)
        {
          result = (object) Color.Empty;
          return true;
        }
      }
      return false;
    }

    private object ParseCore(
      object value,
      Type targetType,
      Type sourceType,
      TypeConverter dataTypeConverter,
      IDataConversionInfoProvider dataColumn,
      bool checkFormattedNullValue)
    {
      if (checkFormattedNullValue && this.EqualsNullValue(value, dataColumn))
        return (object) null;
      CultureInfo formatterCulture = this.GetFormatterCulture((IFormatProvider) dataColumn.FormatInfo);
      TypeConverter converter1 = this.GetConverter(targetType);
      if (dataTypeConverter != null && converter1 != dataTypeConverter && dataTypeConverter.CanConvertFrom(dataColumn as ITypeDescriptorContext, sourceType))
        return dataTypeConverter.ConvertFrom(dataColumn as ITypeDescriptorContext, formatterCulture, value);
      if ((object) targetType != null && (object) sourceType != null && (object) targetType == (object) sourceType)
        return value;
      TypeConverter converter2 = this.GetConverter(sourceType);
      if (dataTypeConverter != null && dataTypeConverter is ColorConverter && converter2 is StringConverter)
        return dataTypeConverter.ConvertFromString(dataColumn as ITypeDescriptorContext, formatterCulture, value.ToString());
      if (dataTypeConverter != null && dataTypeConverter is ColorConverter && (!(converter2 is StringConverter) && dataTypeConverter.CanConvertFrom(dataColumn as ITypeDescriptorContext, sourceType)))
        return dataTypeConverter.ConvertFrom(dataColumn as ITypeDescriptorContext, formatterCulture, value);
      if (dataTypeConverter != null && dataTypeConverter is StringConverter && converter2 is ColorConverter)
        return (object) dataTypeConverter.ConvertToString(dataColumn as ITypeDescriptorContext, formatterCulture, value);
      if (dataTypeConverter != null && !(dataTypeConverter is StringConverter) && (converter2 is ColorConverter && dataTypeConverter.CanConvertTo(dataColumn as ITypeDescriptorContext, targetType)))
        return dataTypeConverter.ConvertTo(dataColumn as ITypeDescriptorContext, formatterCulture, value, targetType);
      if (dataTypeConverter != null && converter2 != dataTypeConverter && dataTypeConverter.CanConvertTo(dataColumn as ITypeDescriptorContext, targetType))
        return dataTypeConverter.ConvertTo(dataColumn as ITypeDescriptorContext, formatterCulture, value, targetType);
      object result = (object) null;
      if (this.ParseValueCore(value, targetType, converter1, dataTypeConverter, dataColumn, formatterCulture, out result))
        return result;
      if (dataTypeConverter == null)
        dataTypeConverter = converter1;
      if (this.ParseWithTypeConverter(value, sourceType, dataTypeConverter, dataColumn, formatterCulture, out result))
        return result;
      if (dataTypeConverter == converter1)
        dataTypeConverter = converter2;
      if (this.ParseWithTypeConverter(value, sourceType, dataTypeConverter, dataColumn, formatterCulture, out result))
        return result;
      return this.ChangeType(value, targetType, (IFormatProvider) formatterCulture);
    }

    private TypeConverter GetConverter(Type type)
    {
      if ((object) type == null)
        return (TypeConverter) null;
      return TypeDescriptor.GetConverter(type);
    }

    private bool ParseWithTypeConverter(
      object value,
      Type sourceType,
      TypeConverter dataTypeConverter,
      IDataConversionInfoProvider dataColumn,
      CultureInfo cultureInfo,
      out object result)
    {
      result = (object) null;
      if (dataTypeConverter == null || !dataTypeConverter.CanConvertFrom(dataColumn as ITypeDescriptorContext, sourceType))
        return false;
      result = dataTypeConverter.ConvertFrom(dataColumn as ITypeDescriptorContext, cultureInfo, value);
      return true;
    }

    private bool ParseValueCore(
      object value,
      Type targetType,
      TypeConverter targetTypeConverter,
      TypeConverter dataTypeConverter,
      IDataConversionInfoProvider dataColumn,
      CultureInfo cultureInfo,
      out object result)
    {
      result = (object) null;
      if (value is string)
        return ((object) targetType != (object) typeof (char) || !string.IsNullOrEmpty(value as string)) && this.InvokeStringParseMethod(value, targetType, (IFormatProvider) cultureInfo, out result);
      if (value is ToggleState)
      {
        if (this.ParseToggleState(value, targetType, targetTypeConverter, dataTypeConverter, dataColumn, cultureInfo, out result))
          return true;
      }
      else if (value != null && targetType.IsAssignableFrom(value.GetType()))
      {
        result = value;
        return true;
      }
      return false;
    }

    private bool ParseToggleState(
      object value,
      Type targetType,
      TypeConverter targetTypeConverter,
      TypeConverter dataTypeConverter,
      IDataConversionInfoProvider dataColumn,
      CultureInfo cultureInfo,
      out object result)
    {
      result = (object) null;
      ToggleState toggleState = (ToggleState) value;
      if ((object) targetType == (object) typeof (ToggleState))
      {
        result = (object) toggleState;
        return true;
      }
      if (toggleState == ToggleState.Indeterminate)
      {
        result = (object) null;
        return true;
      }
      if ((object) targetType == (object) this.BooleanType)
      {
        result = (object) (toggleState == ToggleState.On);
        return true;
      }
      if (dataTypeConverter == null)
        dataTypeConverter = targetTypeConverter;
      if (dataTypeConverter == null || !dataTypeConverter.CanConvertFrom(dataColumn as ITypeDescriptorContext, this.BooleanType))
        return false;
      result = dataTypeConverter.ConvertFrom(dataColumn as ITypeDescriptorContext, cultureInfo, (object) (toggleState == ToggleState.On));
      return true;
    }

    public bool EqualsNullValue(object value, IDataConversionInfoProvider dataColumn)
    {
      if (value == DBNull.Value || value == null)
        return true;
      CultureInfo formatterCulture = this.GetFormatterCulture((IFormatProvider) dataColumn.FormatInfo);
      string nullValue = dataColumn.NullValue as string;
      string strA = value as string;
      if (nullValue != null && strA != null)
        return string.Compare(strA, nullValue, true, formatterCulture) == 0;
      return object.Equals(value, dataColumn.NullValue);
    }

    private bool InvokeStringParseMethod(
      object value,
      Type targetType,
      IFormatProvider formatInfo,
      out object result)
    {
      result = (object) null;
      try
      {
        Type[] types1 = new Type[3]
        {
          this.StringType,
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
          result = method1.Invoke((object) null, parameters);
          return true;
        }
        Type[] types2 = new Type[2]
        {
          this.StringType,
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
          result = method2.Invoke((object) null, parameters);
          return true;
        }
        Type[] types3 = new Type[1]{ this.StringType };
        MethodInfo method3 = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, (Binder) null, types3, (ParameterModifier[]) null);
        if ((object) method3 != null)
        {
          object[] parameters = new object[1]
          {
            (object) (string) value
          };
          result = method3.Invoke((object) null, parameters);
          return true;
        }
      }
      catch (TargetInvocationException ex)
      {
        this.ThrowFormatException((Exception) ex);
      }
      return false;
    }

    private object ChangeType(object value, Type targetType, IFormatProvider formatInfo)
    {
      IConvertible convertible = value as IConvertible;
      if (convertible == null)
        this.ThrowFormatException((Exception) null, this.GetCantConvertMessage(value, targetType));
      object obj = (object) null;
      try
      {
        if (string.IsNullOrEmpty(convertible.ToString()))
          return (object) false;
        obj = Convert.ChangeType((object) convertible, targetType, formatInfo);
      }
      catch (InvalidCastException ex)
      {
        this.ThrowFormatException((Exception) ex);
      }
      return obj;
    }

    private void ThrowFormatException(Exception ex)
    {
      this.ThrowFormatException(ex, string.Empty);
    }

    private void ThrowFormatException(Exception ex, string message)
    {
      if (ex == null && !string.IsNullOrEmpty(message))
        ex = (Exception) new FormatException(message);
      if (ex is FormatException)
        throw ex;
      if (ex is InvalidCastException)
        throw new FormatException(ex.Message, ex);
      if (ex is TargetInvocationException)
        throw new FormatException(ex.InnerException.Message, ex.InnerException);
      if (ex.InnerException is FormatException)
        throw ex.InnerException;
      throw new FormatException("Converter cannot process the value with this parameters.", ex);
    }

    private CultureInfo GetFormatterCulture(IFormatProvider formatInfo)
    {
      return formatInfo as CultureInfo ?? CultureInfo.CurrentCulture;
    }

    private bool IsNullData(object value, object dataSourceNullValue)
    {
      if (value != null && value != DBNull.Value)
        return object.Equals(value, this.NullData(value.GetType(), dataSourceNullValue));
      return true;
    }

    private string GetCantConvertMessage(object value, Type targetType)
    {
      return value != null ? "Converter can't convert" : "Converter can't convert null value";
    }

    private TypeConverter NullableUnwrap(TypeConverter typeConverter)
    {
      NullableConverter nullableConverter = typeConverter as NullableConverter;
      if (nullableConverter == null)
        return typeConverter;
      return nullableConverter.UnderlyingTypeConverter;
    }

    private Type NullableUnwrap(Type nullableType)
    {
      if ((object) nullableType == null)
        return (Type) null;
      if ((object) nullableType == (object) this.StringType)
        return this.StringType;
      Type underlyingType = Nullable.GetUnderlyingType(nullableType);
      if ((object) underlyingType != null)
        return underlyingType;
      return nullableType;
    }

    private object NullData(Type type, object dataSourceNullValue)
    {
      if (!type.IsGenericType || (object) type.GetGenericTypeDefinition() != (object) typeof (Nullable<>) || dataSourceNullValue != null && dataSourceNullValue != DBNull.Value)
        return dataSourceNullValue;
      return (object) null;
    }
  }
}
