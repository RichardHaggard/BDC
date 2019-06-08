// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.SqlConvert
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Xml;

namespace Telerik.Data.Expressions
{
  internal static class SqlConvert
  {
    public static object ChangeType(object value, Type type, IFormatProvider formatProvider)
    {
      return SqlConvert.ChangeType2(value, DataStorageHelper.GetStorageType(type), type, formatProvider);
    }

    public static object ChangeType2(
      object value,
      StorageType stype,
      Type type,
      IFormatProvider formatProvider)
    {
      switch (stype)
      {
        case StorageType.SqlBinary:
          return (object) SqlConvert.ConvertToSqlBinary(value);
        case StorageType.SqlBoolean:
          return (object) SqlConvert.ConvertToSqlBoolean(value);
        case StorageType.SqlByte:
          return (object) SqlConvert.ConvertToSqlByte(value);
        case StorageType.SqlBytes:
          return (object) SqlConvert.ConvertToSqlBytes(value);
        case StorageType.SqlChars:
          return (object) SqlConvert.ConvertToSqlChars(value);
        case StorageType.SqlDateTime:
          return (object) SqlConvert.ConvertToSqlDateTime(value);
        case StorageType.SqlDecimal:
          return (object) SqlConvert.ConvertToSqlDecimal(value);
        case StorageType.SqlDouble:
          return (object) SqlConvert.ConvertToSqlDouble(value);
        case StorageType.SqlGuid:
          return (object) SqlConvert.ConvertToSqlGuid(value);
        case StorageType.SqlInt16:
          return (object) SqlConvert.ConvertToSqlInt16(value);
        case StorageType.SqlInt32:
          return (object) SqlConvert.ConvertToSqlInt32(value);
        case StorageType.SqlInt64:
          return (object) SqlConvert.ConvertToSqlInt64(value);
        case StorageType.SqlMoney:
          return (object) SqlConvert.ConvertToSqlMoney(value);
        case StorageType.SqlSingle:
          return (object) SqlConvert.ConvertToSqlSingle(value);
        case StorageType.SqlString:
          return (object) SqlConvert.ConvertToSqlString(value);
        default:
          if (DBNull.Value == value || value == null)
            return (object) DBNull.Value;
          Type type1 = value.GetType();
          StorageType storageType = DataStorageHelper.GetStorageType(type1);
          switch (storageType)
          {
            case StorageType.SqlBinary:
            case StorageType.SqlBoolean:
            case StorageType.SqlByte:
            case StorageType.SqlBytes:
            case StorageType.SqlChars:
            case StorageType.SqlDateTime:
            case StorageType.SqlDecimal:
            case StorageType.SqlDouble:
            case StorageType.SqlGuid:
            case StorageType.SqlInt16:
            case StorageType.SqlInt32:
            case StorageType.SqlInt64:
            case StorageType.SqlMoney:
            case StorageType.SqlSingle:
            case StorageType.SqlString:
              throw InvalidExpressionException.SqlConvertFailed(type1, type);
            default:
              if (StorageType.String != stype)
              {
                if (StorageType.TimeSpan != stype)
                {
                  if (StorageType.String == storageType)
                  {
                    switch (stype)
                    {
                      case StorageType.Boolean:
                        if (!("1" != (string) value))
                          return (object) true;
                        if ("0" == (string) value)
                          return (object) false;
                        break;
                      case StorageType.Char:
                        return (object) ((IConvertible) (string) value).ToChar(formatProvider);
                      case StorageType.SByte:
                        return (object) ((IConvertible) (string) value).ToSByte(formatProvider);
                      case StorageType.Byte:
                        return (object) ((IConvertible) (string) value).ToByte(formatProvider);
                      case StorageType.Int16:
                        return (object) ((IConvertible) (string) value).ToInt16(formatProvider);
                      case StorageType.UInt16:
                        return (object) ((IConvertible) (string) value).ToUInt16(formatProvider);
                      case StorageType.Int32:
                        return (object) ((IConvertible) (string) value).ToInt32(formatProvider);
                      case StorageType.UInt32:
                        return (object) ((IConvertible) (string) value).ToUInt32(formatProvider);
                      case StorageType.Int64:
                        return (object) ((IConvertible) (string) value).ToInt64(formatProvider);
                      case StorageType.UInt64:
                        return (object) ((IConvertible) (string) value).ToUInt64(formatProvider);
                      case StorageType.Single:
                        return (object) ((IConvertible) (string) value).ToSingle(formatProvider);
                      case StorageType.Double:
                        return (object) ((IConvertible) (string) value).ToDouble(formatProvider);
                      case StorageType.Decimal:
                        return (object) ((IConvertible) (string) value).ToDecimal(formatProvider);
                      case StorageType.DateTime:
                        return (object) ((IConvertible) (string) value).ToDateTime(formatProvider);
                      case StorageType.TimeSpan:
                        return (object) XmlConvert.ToTimeSpan((string) value);
                      case StorageType.String:
                        return (object) (string) value;
                      case StorageType.Guid:
                        return (object) XmlConvert.ToGuid((string) value);
                      case StorageType.Uri:
                        return (object) new Uri((string) value);
                    }
                  }
                  return Convert.ChangeType(value, type, formatProvider);
                }
                switch (storageType)
                {
                  case StorageType.Int32:
                    return (object) new TimeSpan((long) (int) value);
                  case StorageType.Int64:
                    return (object) new TimeSpan((long) value);
                  case StorageType.String:
                    return (object) XmlConvert.ToTimeSpan((string) value);
                  default:
                    return (object) (TimeSpan) value;
                }
              }
              else
              {
                switch (storageType)
                {
                  case StorageType.Boolean:
                    return (object) ((bool) value).ToString(formatProvider);
                  case StorageType.Char:
                    return (object) ((char) value).ToString(formatProvider);
                  case StorageType.SByte:
                    return (object) ((sbyte) value).ToString(formatProvider);
                  case StorageType.Byte:
                    return (object) ((byte) value).ToString(formatProvider);
                  case StorageType.Int16:
                    return (object) ((short) value).ToString(formatProvider);
                  case StorageType.UInt16:
                    return (object) ((ushort) value).ToString(formatProvider);
                  case StorageType.Int32:
                    return (object) ((int) value).ToString(formatProvider);
                  case StorageType.UInt32:
                    return (object) ((uint) value).ToString(formatProvider);
                  case StorageType.Int64:
                    return (object) ((long) value).ToString(formatProvider);
                  case StorageType.UInt64:
                    return (object) ((ulong) value).ToString(formatProvider);
                  case StorageType.Single:
                    return (object) ((float) value).ToString(formatProvider);
                  case StorageType.Double:
                    return (object) ((double) value).ToString(formatProvider);
                  case StorageType.Decimal:
                    return (object) ((Decimal) value).ToString(formatProvider);
                  case StorageType.DateTime:
                    return (object) ((DateTime) value).ToString(formatProvider);
                  case StorageType.TimeSpan:
                    return (object) XmlConvert.ToString((TimeSpan) value);
                  case StorageType.String:
                    return (object) (string) value;
                  case StorageType.Guid:
                    return (object) XmlConvert.ToString((Guid) value);
                  case StorageType.CharArray:
                    return (object) new string((char[]) value);
                  default:
                    IConvertible convertible = value as IConvertible;
                    if (convertible != null)
                      return (object) convertible.ToString(formatProvider);
                    IFormattable formattable = value as IFormattable;
                    if (formattable != null)
                      return (object) formattable.ToString((string) null, formatProvider);
                    return (object) value.ToString();
                }
              }
          }
      }
    }

    public static object ChangeTypeForXML(object value, Type type)
    {
      StorageType storageType1 = DataStorageHelper.GetStorageType(type);
      StorageType storageType2 = DataStorageHelper.GetStorageType(value.GetType());
      switch (storageType1)
      {
        case StorageType.Boolean:
          if (!("1" != (string) value))
            return (object) true;
          if ("0" == (string) value)
            return (object) false;
          return (object) XmlConvert.ToBoolean((string) value);
        case StorageType.Char:
          return (object) XmlConvert.ToChar((string) value);
        case StorageType.SByte:
          return (object) XmlConvert.ToSByte((string) value);
        case StorageType.Byte:
          return (object) XmlConvert.ToByte((string) value);
        case StorageType.Int16:
          return (object) XmlConvert.ToInt16((string) value);
        case StorageType.UInt16:
          return (object) XmlConvert.ToUInt16((string) value);
        case StorageType.Int32:
          return (object) XmlConvert.ToInt32((string) value);
        case StorageType.UInt32:
          return (object) XmlConvert.ToUInt32((string) value);
        case StorageType.Int64:
          return (object) XmlConvert.ToInt64((string) value);
        case StorageType.UInt64:
          return (object) XmlConvert.ToUInt64((string) value);
        case StorageType.Single:
          return (object) XmlConvert.ToSingle((string) value);
        case StorageType.Double:
          return (object) XmlConvert.ToDouble((string) value);
        case StorageType.Decimal:
          return (object) XmlConvert.ToDecimal((string) value);
        case StorageType.DateTime:
          return (object) XmlConvert.ToDateTime((string) value, XmlDateTimeSerializationMode.RoundtripKind);
        case StorageType.TimeSpan:
          StorageType storageType3 = storageType2;
          switch (storageType3)
          {
            case StorageType.Int32:
              return (object) new TimeSpan((long) (int) value);
            case StorageType.Int64:
              return (object) new TimeSpan((long) value);
            default:
              if (storageType3 == StorageType.String)
                return (object) XmlConvert.ToTimeSpan((string) value);
              return (object) (TimeSpan) value;
          }
        case StorageType.Guid:
          return (object) XmlConvert.ToGuid((string) value);
        case StorageType.Uri:
          return (object) new Uri((string) value);
        case StorageType.SqlBinary:
          return (object) new SqlBinary(Convert.FromBase64String((string) value));
        case StorageType.SqlBoolean:
          return (object) new SqlBoolean(XmlConvert.ToBoolean((string) value));
        case StorageType.SqlByte:
          return (object) new SqlByte(XmlConvert.ToByte((string) value));
        case StorageType.SqlBytes:
          return (object) new SqlBytes(Convert.FromBase64String((string) value));
        case StorageType.SqlChars:
          return (object) new SqlChars(((string) value).ToCharArray());
        case StorageType.SqlDateTime:
          return (object) new SqlDateTime(XmlConvert.ToDateTime((string) value, XmlDateTimeSerializationMode.RoundtripKind));
        case StorageType.SqlDecimal:
          return (object) SqlDecimal.Parse((string) value);
        case StorageType.SqlDouble:
          return (object) new SqlDouble(XmlConvert.ToDouble((string) value));
        case StorageType.SqlGuid:
          return (object) new SqlGuid(XmlConvert.ToGuid((string) value));
        case StorageType.SqlInt16:
          return (object) new SqlInt16(XmlConvert.ToInt16((string) value));
        case StorageType.SqlInt32:
          return (object) new SqlInt32(XmlConvert.ToInt32((string) value));
        case StorageType.SqlInt64:
          return (object) new SqlInt64(XmlConvert.ToInt64((string) value));
        case StorageType.SqlMoney:
          return (object) new SqlMoney(XmlConvert.ToDecimal((string) value));
        case StorageType.SqlSingle:
          return (object) new SqlSingle(XmlConvert.ToSingle((string) value));
        case StorageType.SqlString:
          return (object) new SqlString((string) value);
        default:
          if (DBNull.Value == value || value == null)
            return (object) DBNull.Value;
          switch (storageType2)
          {
            case StorageType.Boolean:
              return (object) XmlConvert.ToString((bool) value);
            case StorageType.Char:
              return (object) XmlConvert.ToString((char) value);
            case StorageType.SByte:
              return (object) XmlConvert.ToString((sbyte) value);
            case StorageType.Byte:
              return (object) XmlConvert.ToString((byte) value);
            case StorageType.Int16:
              return (object) XmlConvert.ToString((short) value);
            case StorageType.UInt16:
              return (object) XmlConvert.ToString((ushort) value);
            case StorageType.Int32:
              return (object) XmlConvert.ToString((int) value);
            case StorageType.UInt32:
              return (object) XmlConvert.ToString((uint) value);
            case StorageType.Int64:
              return (object) XmlConvert.ToString((long) value);
            case StorageType.UInt64:
              return (object) XmlConvert.ToString((ulong) value);
            case StorageType.Single:
              return (object) XmlConvert.ToString((float) value);
            case StorageType.Double:
              return (object) XmlConvert.ToString((double) value);
            case StorageType.Decimal:
              return (object) XmlConvert.ToString((Decimal) value);
            case StorageType.DateTime:
              return (object) XmlConvert.ToString((DateTime) value, XmlDateTimeSerializationMode.RoundtripKind);
            case StorageType.TimeSpan:
              return (object) XmlConvert.ToString((TimeSpan) value);
            case StorageType.String:
              return (object) (string) value;
            case StorageType.Guid:
              return (object) XmlConvert.ToString((Guid) value);
            case StorageType.CharArray:
              return (object) new string((char[]) value);
            case StorageType.SqlBinary:
              return (object) Convert.ToBase64String(((SqlBinary) value).Value);
            case StorageType.SqlBoolean:
              return (object) XmlConvert.ToString(((SqlBoolean) value).Value);
            case StorageType.SqlByte:
              return (object) XmlConvert.ToString(((SqlByte) value).Value);
            case StorageType.SqlBytes:
              return (object) Convert.ToBase64String(((SqlBytes) value).Value);
            case StorageType.SqlChars:
              return (object) new string(((SqlChars) value).Value);
            case StorageType.SqlDateTime:
              return (object) XmlConvert.ToString(((SqlDateTime) value).Value, XmlDateTimeSerializationMode.RoundtripKind);
            case StorageType.SqlDecimal:
              return (object) ((SqlDecimal) value).ToString();
            case StorageType.SqlDouble:
              return (object) XmlConvert.ToString(((SqlDouble) value).Value);
            case StorageType.SqlGuid:
              return (object) XmlConvert.ToString(((SqlGuid) value).Value);
            case StorageType.SqlInt16:
              return (object) XmlConvert.ToString(((SqlInt16) value).Value);
            case StorageType.SqlInt32:
              return (object) XmlConvert.ToString(((SqlInt32) value).Value);
            case StorageType.SqlInt64:
              return (object) XmlConvert.ToString(((SqlInt64) value).Value);
            case StorageType.SqlMoney:
              return (object) XmlConvert.ToString(((SqlMoney) value).Value);
            case StorageType.SqlSingle:
              return (object) XmlConvert.ToString(((SqlSingle) value).Value);
            case StorageType.SqlString:
              return (object) ((SqlString) value).Value;
            default:
              IConvertible convertible = value as IConvertible;
              if (convertible != null)
                return (object) convertible.ToString((IFormatProvider) CultureInfo.InvariantCulture);
              IFormattable formattable = value as IFormattable;
              if (formattable != null)
                return (object) formattable.ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture);
              return (object) value.ToString();
          }
      }
    }

    public static SqlBinary ConvertToSqlBinary(object value)
    {
      if (value == DBNull.Value)
        return SqlBinary.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.ByteArray:
          return (SqlBinary) ((byte[]) value);
        case StorageType.SqlBinary:
          return (SqlBinary) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlBinary));
      }
    }

    public static SqlBoolean ConvertToSqlBoolean(object value)
    {
      if (value == DBNull.Value || value == null)
        return SqlBoolean.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Boolean:
          return (SqlBoolean) ((bool) value);
        case StorageType.SqlBoolean:
          return (SqlBoolean) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlBoolean));
      }
    }

    public static SqlByte ConvertToSqlByte(object value)
    {
      if (value == DBNull.Value)
        return SqlByte.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlByte) ((byte) value);
        case StorageType.SqlByte:
          return (SqlByte) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlByte));
      }
    }

    public static SqlBytes ConvertToSqlBytes(object value)
    {
      if (value == DBNull.Value)
        return SqlBytes.Null;
      Type type = value.GetType();
      if (DataStorageHelper.GetStorageType(type) != StorageType.SqlBytes)
        throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlBytes));
      return (SqlBytes) value;
    }

    public static SqlChars ConvertToSqlChars(object value)
    {
      if (value == DBNull.Value)
        return SqlChars.Null;
      Type type = value.GetType();
      if (DataStorageHelper.GetStorageType(type) != StorageType.SqlChars)
        throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlChars));
      return (SqlChars) value;
    }

    public static SqlDateTime ConvertToSqlDateTime(object value)
    {
      if (value == DBNull.Value)
        return SqlDateTime.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.DateTime:
          return (SqlDateTime) ((DateTime) value);
        case StorageType.SqlDateTime:
          return (SqlDateTime) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlDateTime));
      }
    }

    public static SqlDecimal ConvertToSqlDecimal(object value)
    {
      if (value == DBNull.Value)
        return SqlDecimal.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlDecimal) ((long) (byte) value);
        case StorageType.Int16:
          return (SqlDecimal) ((long) (short) value);
        case StorageType.UInt16:
          return (SqlDecimal) ((long) (ushort) value);
        case StorageType.Int32:
          return (SqlDecimal) ((long) (int) value);
        case StorageType.UInt32:
          return (SqlDecimal) ((long) (uint) value);
        case StorageType.Int64:
          return (SqlDecimal) ((long) value);
        case StorageType.UInt64:
          return (SqlDecimal) ((long) value);
        case StorageType.Decimal:
          return (SqlDecimal) ((Decimal) value);
        case StorageType.SqlByte:
          return (SqlDecimal) ((SqlByte) value);
        case StorageType.SqlDecimal:
          return (SqlDecimal) value;
        case StorageType.SqlInt16:
          return (SqlDecimal) ((SqlInt16) value);
        case StorageType.SqlInt32:
          return (SqlDecimal) ((SqlInt32) value);
        case StorageType.SqlInt64:
          return (SqlDecimal) ((SqlInt64) value);
        case StorageType.SqlMoney:
          return (SqlDecimal) ((SqlMoney) value);
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlDecimal));
      }
    }

    public static SqlDouble ConvertToSqlDouble(object value)
    {
      if (value == DBNull.Value)
        return SqlDouble.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlDouble) ((double) (byte) value);
        case StorageType.Int16:
          return (SqlDouble) ((double) (short) value);
        case StorageType.UInt16:
          return (SqlDouble) ((double) (ushort) value);
        case StorageType.Int32:
          return (SqlDouble) ((double) (int) value);
        case StorageType.UInt32:
          return (SqlDouble) ((double) (uint) value);
        case StorageType.Int64:
          return (SqlDouble) ((double) (long) value);
        case StorageType.UInt64:
          return (SqlDouble) ((double) (ulong) value);
        case StorageType.Single:
          return (SqlDouble) ((double) (float) value);
        case StorageType.Double:
          return (SqlDouble) ((double) value);
        case StorageType.SqlByte:
          return (SqlDouble) ((SqlByte) value);
        case StorageType.SqlDecimal:
          return (SqlDouble) ((SqlDecimal) value);
        case StorageType.SqlDouble:
          return (SqlDouble) value;
        case StorageType.SqlInt16:
          return (SqlDouble) ((SqlInt16) value);
        case StorageType.SqlInt32:
          return (SqlDouble) ((SqlInt32) value);
        case StorageType.SqlInt64:
          return (SqlDouble) ((SqlInt64) value);
        case StorageType.SqlMoney:
          return (SqlDouble) ((SqlMoney) value);
        case StorageType.SqlSingle:
          return (SqlDouble) ((SqlSingle) value);
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlDouble));
      }
    }

    public static SqlGuid ConvertToSqlGuid(object value)
    {
      if (value == DBNull.Value)
        return SqlGuid.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Guid:
          return (SqlGuid) ((Guid) value);
        case StorageType.SqlGuid:
          return (SqlGuid) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlGuid));
      }
    }

    public static SqlInt16 ConvertToSqlInt16(object value)
    {
      if (value == DBNull.Value)
        return SqlInt16.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlInt16) ((short) (byte) value);
        case StorageType.Int16:
          return (SqlInt16) ((short) value);
        case StorageType.SqlByte:
          return (SqlInt16) ((SqlByte) value);
        case StorageType.SqlInt16:
          return (SqlInt16) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlInt16));
      }
    }

    public static SqlInt32 ConvertToSqlInt32(object value)
    {
      if (value == DBNull.Value)
        return SqlInt32.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlInt32) ((int) (byte) value);
        case StorageType.Int16:
          return (SqlInt32) ((int) (short) value);
        case StorageType.UInt16:
          return (SqlInt32) ((int) (ushort) value);
        case StorageType.Int32:
          return (SqlInt32) ((int) value);
        case StorageType.SqlByte:
          return (SqlInt32) ((SqlByte) value);
        case StorageType.SqlInt16:
          return (SqlInt32) ((SqlInt16) value);
        case StorageType.SqlInt32:
          return (SqlInt32) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlInt32));
      }
    }

    public static SqlInt64 ConvertToSqlInt64(object value)
    {
      if (value == DBNull.Value)
        return (SqlInt64) SqlInt32.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlInt64) ((long) (byte) value);
        case StorageType.Int16:
          return (SqlInt64) ((long) (short) value);
        case StorageType.UInt16:
          return (SqlInt64) ((long) (ushort) value);
        case StorageType.Int32:
          return (SqlInt64) ((long) (int) value);
        case StorageType.UInt32:
          return (SqlInt64) ((long) (uint) value);
        case StorageType.Int64:
          return (SqlInt64) ((long) value);
        case StorageType.SqlByte:
          return (SqlInt64) ((SqlByte) value);
        case StorageType.SqlInt16:
          return (SqlInt64) ((SqlInt16) value);
        case StorageType.SqlInt32:
          return (SqlInt64) ((SqlInt32) value);
        case StorageType.SqlInt64:
          return (SqlInt64) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlInt64));
      }
    }

    public static SqlMoney ConvertToSqlMoney(object value)
    {
      if (value == DBNull.Value)
        return SqlMoney.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlMoney) ((long) (byte) value);
        case StorageType.Int16:
          return (SqlMoney) ((long) (short) value);
        case StorageType.UInt16:
          return (SqlMoney) ((long) (ushort) value);
        case StorageType.Int32:
          return (SqlMoney) ((long) (int) value);
        case StorageType.UInt32:
          return (SqlMoney) ((long) (uint) value);
        case StorageType.Int64:
          return (SqlMoney) ((long) value);
        case StorageType.UInt64:
          return (SqlMoney) ((long) value);
        case StorageType.Decimal:
          return (SqlMoney) ((Decimal) value);
        case StorageType.SqlByte:
          return (SqlMoney) ((SqlByte) value);
        case StorageType.SqlInt16:
          return (SqlMoney) ((SqlInt16) value);
        case StorageType.SqlInt32:
          return (SqlMoney) ((SqlInt32) value);
        case StorageType.SqlInt64:
          return (SqlMoney) ((SqlInt64) value);
        case StorageType.SqlMoney:
          return (SqlMoney) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlMoney));
      }
    }

    public static SqlSingle ConvertToSqlSingle(object value)
    {
      if (value == DBNull.Value)
        return SqlSingle.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.Byte:
          return (SqlSingle) ((float) (byte) value);
        case StorageType.Int16:
          return (SqlSingle) ((float) (short) value);
        case StorageType.UInt16:
          return (SqlSingle) ((float) (ushort) value);
        case StorageType.Int32:
          return (SqlSingle) ((float) (int) value);
        case StorageType.UInt32:
          return (SqlSingle) ((float) (uint) value);
        case StorageType.Int64:
          return (SqlSingle) ((float) (long) value);
        case StorageType.UInt64:
          return (SqlSingle) ((float) (ulong) value);
        case StorageType.Single:
          return (SqlSingle) ((float) value);
        case StorageType.SqlByte:
          return (SqlSingle) ((SqlByte) value);
        case StorageType.SqlDecimal:
          return (SqlSingle) ((SqlDecimal) value);
        case StorageType.SqlInt16:
          return (SqlSingle) ((SqlInt16) value);
        case StorageType.SqlInt32:
          return (SqlSingle) ((SqlInt32) value);
        case StorageType.SqlInt64:
          return (SqlSingle) ((SqlInt64) value);
        case StorageType.SqlMoney:
          return (SqlSingle) ((SqlMoney) value);
        case StorageType.SqlSingle:
          return (SqlSingle) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlSingle));
      }
    }

    public static SqlString ConvertToSqlString(object value)
    {
      if (value == DBNull.Value || value == null)
        return SqlString.Null;
      Type type = value.GetType();
      switch (DataStorageHelper.GetStorageType(type))
      {
        case StorageType.String:
          return (SqlString) ((string) value);
        case StorageType.SqlString:
          return (SqlString) value;
        default:
          throw InvalidExpressionException.SqlConvertFailed(type, typeof (SqlString));
      }
    }
  }
}
