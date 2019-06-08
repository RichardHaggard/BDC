// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.DataStorageHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using Telerik.WinControls.Data;

namespace Telerik.Data.Expressions
{
  internal static class DataStorageHelper
  {
    private static readonly Type[] types = new Type[39]
    {
      null,
      typeof (object),
      typeof (DBNull),
      typeof (bool),
      typeof (char),
      typeof (sbyte),
      typeof (byte),
      typeof (short),
      typeof (ushort),
      typeof (int),
      typeof (uint),
      typeof (long),
      typeof (ulong),
      typeof (float),
      typeof (double),
      typeof (Decimal),
      typeof (DateTime),
      typeof (TimeSpan),
      typeof (string),
      typeof (Guid),
      typeof (byte[]),
      typeof (char[]),
      typeof (Type),
      typeof (Uri),
      typeof (SqlBinary),
      typeof (SqlBoolean),
      typeof (SqlByte),
      typeof (SqlBytes),
      typeof (SqlChars),
      typeof (SqlDateTime),
      typeof (SqlDecimal),
      typeof (SqlDouble),
      typeof (SqlGuid),
      typeof (SqlInt16),
      typeof (SqlInt32),
      typeof (SqlInt64),
      typeof (SqlMoney),
      typeof (SqlSingle),
      typeof (SqlString)
    };

    public static string ParseEnumerbale(IEnumerable enumebrable)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string format = "{0},";
      foreach (object obj in enumebrable)
      {
        Decimal result;
        if (!Decimal.TryParse(obj.ToString(), out result))
        {
          format = "'{0}',";
          break;
        }
      }
      foreach (object obj in enumebrable)
      {
        string str = Convert.ToString(obj).Trim('\'');
        stringBuilder.Append(string.Format(format, (object) str));
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return stringBuilder.ToString();
    }

    public static List<SortDescriptor> ParseSortString(string sortString)
    {
      List<SortDescriptor> sortDescriptorList = new List<SortDescriptor>();
      char[] chArray = new char[1]{ ',' };
      foreach (string str1 in sortString.Split(chArray))
      {
        string str2 = str1.Trim();
        int length = str2.Length;
        bool flag = true;
        if (length >= 5 && string.Compare(str2, length - 4, " ASC", 0, 4, true, CultureInfo.InvariantCulture) == 0)
          str2 = str2.Substring(0, length - 4).Trim();
        else if (length >= 6 && string.Compare(str2, length - 5, " DESC", 0, 5, true, CultureInfo.InvariantCulture) == 0)
        {
          flag = false;
          str2 = str2.Substring(0, length - 5).Trim();
        }
        if (str2.StartsWith("["))
        {
          if (!str2.EndsWith("]"))
            throw new ArgumentException("Invalid sort expression");
          str2 = str2.Substring(1, str2.Length - 2);
        }
        sortDescriptorList.Add(new SortDescriptor(str2, flag ? ListSortDirection.Ascending : ListSortDirection.Descending));
      }
      return sortDescriptorList;
    }

    public static List<GroupDescriptor> ParseGroupString(string groupString)
    {
      List<GroupDescriptor> groupDescriptorList = new List<GroupDescriptor>();
      char[] chArray = new char[1]{ ';' };
      foreach (string str in groupString.Split(chArray))
      {
        GroupDescriptor groupDescriptor = new GroupDescriptor(str.Trim());
        groupDescriptorList.Add(groupDescriptor);
      }
      return groupDescriptorList;
    }

    public static object SecureCreateInstance(Type type, object[] args)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      if (!type.IsPublic && !type.IsNestedPublic)
        new ReflectionPermission(PermissionState.Unrestricted).Demand();
      return Activator.CreateInstance(type, args);
    }

    public static StorageType ResultSqlType(
      StorageType left,
      StorageType right,
      bool lc,
      bool rc,
      Operator op)
    {
      int precedence1 = (int) DataStorageHelper.GetPrecedence(left);
      if (precedence1 == 0)
        return StorageType.Empty;
      int precedence2 = (int) DataStorageHelper.GetPrecedence(right);
      if (precedence2 == 0)
        return StorageType.Empty;
      if (Operator.IsLogical(op))
      {
        if (left != StorageType.Boolean && left != StorageType.SqlBoolean || right != StorageType.Boolean && right != StorageType.SqlBoolean)
          return StorageType.Empty;
        return left == StorageType.Boolean && right == StorageType.Boolean ? StorageType.Boolean : StorageType.SqlBoolean;
      }
      if (op == Operator.Plus)
      {
        if (left == StorageType.SqlString || right == StorageType.SqlString)
          return StorageType.SqlString;
        if (left == StorageType.String || right == StorageType.String)
          return StorageType.String;
      }
      if (left == StorageType.SqlBinary && right != StorageType.SqlBinary || left != StorageType.SqlBinary && right == StorageType.SqlBinary || (left == StorageType.SqlGuid && right != StorageType.SqlGuid || left != StorageType.SqlGuid && right == StorageType.SqlGuid) || (precedence1 > 19 && precedence2 < 20 || precedence1 < 20 && precedence2 > 19))
        return StorageType.Empty;
      if (precedence1 > 19)
      {
        if (op == Operator.Plus || op == Operator.Minus)
        {
          if (left == StorageType.TimeSpan)
            return right;
          if (right == StorageType.TimeSpan)
            return left;
          return StorageType.Empty;
        }
        if (!Operator.IsRelational(op))
          return StorageType.Empty;
        return left;
      }
      DataTypePrecedence code = (DataTypePrecedence) Math.Max(precedence1, precedence2);
      DataStorageHelper.GetPrecedenceType(code);
      StorageType type = DataStorageHelper.GetPrecedenceType((DataTypePrecedence) DataStorageHelper.SqlResultType((int) code));
      if (Operator.IsArithmetical(op) && type != StorageType.String && (type != StorageType.Char && type != StorageType.SqlString) && (!DataStorageHelper.IsNumericSql(left) || !DataStorageHelper.IsNumericSql(right)))
        return StorageType.Empty;
      if (op == Operator.Multiply && DataStorageHelper.IsIntegerSql(type))
        return StorageType.SqlDouble;
      if (type == StorageType.SqlMoney && left != StorageType.SqlMoney && right != StorageType.SqlMoney)
        type = StorageType.SqlDecimal;
      if (!DataStorageHelper.IsMixedSql(left, right) || !DataStorageHelper.IsUnsignedSql(type))
        return type;
      if (code >= DataTypePrecedence.UInt64)
        throw InvalidExpressionException.AmbiguousBinop(op, DataStorageHelper.GetTypeStorage(left), DataStorageHelper.GetTypeStorage(right));
      return DataStorageHelper.GetPrecedenceType(code + 1);
    }

    public static StorageType ResultType(
      StorageType left,
      StorageType right,
      bool lc,
      bool rc,
      Operator op)
    {
      if (left == StorageType.Guid && right == StorageType.Guid && Operator.IsRelational(op) || left == StorageType.String && right == StorageType.Guid && Operator.IsRelational(op))
        return left;
      if (left == StorageType.Guid && right == StorageType.String && Operator.IsRelational(op))
        return right;
      int precedence1 = (int) DataStorageHelper.GetPrecedence(left);
      if (precedence1 == 0)
        return StorageType.Empty;
      int precedence2 = (int) DataStorageHelper.GetPrecedence(right);
      if (precedence2 == 0)
        return StorageType.Empty;
      if (Operator.IsLogical(op))
        return left == StorageType.Boolean && right == StorageType.Boolean ? StorageType.Boolean : StorageType.Empty;
      if (op == Operator.Plus && (left == StorageType.String || right == StorageType.String))
        return StorageType.String;
      DataTypePrecedence code = (DataTypePrecedence) Math.Max(precedence1, precedence2);
      StorageType precedenceType = DataStorageHelper.GetPrecedenceType(code);
      if (Operator.IsArithmetical(op) && precedenceType != StorageType.String && precedenceType != StorageType.Char && (!DataStorageHelper.IsNumeric(left) || !DataStorageHelper.IsNumeric(right)))
        return StorageType.Empty;
      if (op == Operator.Multiply && DataStorageHelper.IsInteger(precedenceType))
        return StorageType.Double;
      if (!DataStorageHelper.IsMixed(left, right))
        return precedenceType;
      if (lc && !rc)
        return right;
      if (!lc && rc)
        return left;
      if (!DataStorageHelper.IsUnsigned(precedenceType))
        return precedenceType;
      if (code >= DataTypePrecedence.UInt64)
        throw InvalidExpressionException.AmbiguousBinop(op, DataStorageHelper.GetTypeStorage(left), DataStorageHelper.GetTypeStorage(right));
      return DataStorageHelper.GetPrecedenceType(code + 1);
    }

    public static bool IsFloat(StorageType type)
    {
      if (type != StorageType.Single && type != StorageType.Double)
        return type == StorageType.Decimal;
      return true;
    }

    public static bool IsFloatSql(StorageType type)
    {
      if (type != StorageType.Single && type != StorageType.Double && (type != StorageType.Decimal && type != StorageType.SqlDouble) && (type != StorageType.SqlDecimal && type != StorageType.SqlMoney))
        return type == StorageType.SqlSingle;
      return true;
    }

    public static bool IsInteger(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.UInt16) && (type != StorageType.UInt32 && type != StorageType.UInt64 && type != StorageType.SByte))
        return type == StorageType.Byte;
      return true;
    }

    public static bool IsIntegerSql(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.UInt16) && (type != StorageType.UInt32 && type != StorageType.UInt64 && (type != StorageType.SByte && type != StorageType.Byte)) && (type != StorageType.SqlInt64 && type != StorageType.SqlInt32 && type != StorageType.SqlInt16))
        return type == StorageType.SqlByte;
      return true;
    }

    public static bool IsNumeric(StorageType type)
    {
      if (!DataStorageHelper.IsFloat(type))
        return DataStorageHelper.IsInteger(type);
      return true;
    }

    public static bool IsNumericSql(StorageType type)
    {
      if (!DataStorageHelper.IsFloatSql(type))
        return DataStorageHelper.IsIntegerSql(type);
      return true;
    }

    public static bool IsUnknown(object value)
    {
      return DataStorageHelper.IsObjectNull(value);
    }

    public static bool IsSigned(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.SByte))
        return DataStorageHelper.IsFloat(type);
      return true;
    }

    public static bool IsSignedSql(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.SByte) && (type != StorageType.SqlInt64 && type != StorageType.SqlInt32 && type != StorageType.SqlInt16))
        return DataStorageHelper.IsFloatSql(type);
      return true;
    }

    public static bool IsUnsigned(StorageType type)
    {
      if (type != StorageType.UInt16 && type != StorageType.UInt32 && type != StorageType.UInt64)
        return type == StorageType.Byte;
      return true;
    }

    public static bool IsUnsignedSql(StorageType type)
    {
      if (type != StorageType.UInt16 && type != StorageType.UInt32 && (type != StorageType.UInt64 && type != StorageType.SqlByte))
        return type == StorageType.Byte;
      return true;
    }

    public static bool ToBoolean(object value)
    {
      if (DataStorageHelper.IsUnknown(value))
        return false;
      if (value is bool)
        return (bool) value;
      if (value is SqlBoolean)
        return ((SqlBoolean) value).IsTrue;
      if (!(value is string))
        throw InvalidExpressionException.DatavalueConvertion(value, typeof (bool), (Exception) null);
      try
      {
        return bool.Parse((string) value);
      }
      catch (Exception ex)
      {
        throw InvalidExpressionException.DatavalueConvertion(value, typeof (bool), ex);
      }
    }

    public static int SqlResultType(int typeCode)
    {
      switch (typeCode)
      {
        case -8:
          return -7;
        case -7:
        case -6:
        case -4:
        case -3:
        case -1:
        case 0:
        case 2:
        case 5:
        case 8:
        case 11:
        case 13:
        case 15:
        case 17:
        case 19:
          return typeCode;
        case -5:
          return -4;
        case -2:
          return -1;
        case 1:
          return 2;
        case 3:
        case 4:
          return 5;
        case 6:
        case 7:
          return 8;
        case 9:
        case 10:
          return 11;
        case 12:
          return 13;
        case 14:
          return 15;
        case 16:
          return 17;
        case 18:
          return 19;
        case 20:
          return 21;
        case 23:
          return 24;
        default:
          return typeCode;
      }
    }

    public static bool IsMixed(StorageType left, StorageType right)
    {
      if (DataStorageHelper.IsSigned(left) && DataStorageHelper.IsUnsigned(right))
        return true;
      if (DataStorageHelper.IsUnsigned(left))
        return DataStorageHelper.IsSigned(right);
      return false;
    }

    public static bool IsMixedSql(StorageType left, StorageType right)
    {
      if (DataStorageHelper.IsSignedSql(left) && DataStorageHelper.IsUnsignedSql(right))
        return true;
      if (DataStorageHelper.IsUnsignedSql(left))
        return DataStorageHelper.IsSignedSql(right);
      return false;
    }

    public static Type GetTypeStorage(StorageType storageType)
    {
      return DataStorageHelper.types[(int) storageType];
    }

    public static StorageType GetStorageType(Type dataType)
    {
      for (int index = 0; index < DataStorageHelper.types.Length; ++index)
      {
        if ((object) dataType == (object) DataStorageHelper.types[index])
          return (StorageType) index;
      }
      TypeCode typeCode = Type.GetTypeCode(dataType);
      if (TypeCode.Object != typeCode)
        return (StorageType) typeCode;
      return StorageType.String;
    }

    public static bool IsSqlType(StorageType storageType)
    {
      return StorageType.SqlBinary <= storageType;
    }

    public static bool IsSqlType(object obj)
    {
      if (obj == null)
        return false;
      return DataStorageHelper.IsSqlType(obj.GetType());
    }

    public static bool IsSqlType(Type dataType)
    {
      for (int index = 24; index < DataStorageHelper.types.Length; ++index)
      {
        if ((object) dataType == (object) DataStorageHelper.types[index])
          return true;
      }
      return false;
    }

    public static bool IsObjectNull(object value)
    {
      if (value != null && DBNull.Value != value)
        return DataStorageHelper.IsObjectSqlNull(value);
      return true;
    }

    public static bool IsObjectSqlNull(object value)
    {
      INullable nullable = value as INullable;
      if (nullable != null)
        return nullable.IsNull;
      return false;
    }

    public static DataTypePrecedence GetPrecedence(StorageType storageType)
    {
      switch (storageType)
      {
        case StorageType.Boolean:
          return DataTypePrecedence.Boolean;
        case StorageType.Char:
          return DataTypePrecedence.Char;
        case StorageType.SByte:
          return DataTypePrecedence.SByte;
        case StorageType.Byte:
          return DataTypePrecedence.Byte;
        case StorageType.Int16:
          return DataTypePrecedence.Int16;
        case StorageType.UInt16:
          return DataTypePrecedence.UInt16;
        case StorageType.Int32:
          return DataTypePrecedence.Int32;
        case StorageType.UInt32:
          return DataTypePrecedence.UInt32;
        case StorageType.Int64:
          return DataTypePrecedence.Int64;
        case StorageType.UInt64:
          return DataTypePrecedence.UInt64;
        case StorageType.Single:
          return DataTypePrecedence.Single;
        case StorageType.Double:
          return DataTypePrecedence.Double;
        case StorageType.Decimal:
          return DataTypePrecedence.Decimal;
        case StorageType.DateTime:
          return DataTypePrecedence.DateTime;
        case StorageType.TimeSpan:
          return DataTypePrecedence.TimeSpan;
        case StorageType.String:
          return DataTypePrecedence.String;
        case StorageType.SqlBinary:
          return DataTypePrecedence.SqlBinary;
        case StorageType.SqlBoolean:
          return DataTypePrecedence.SqlBoolean;
        case StorageType.SqlByte:
          return DataTypePrecedence.SqlByte;
        case StorageType.SqlBytes:
          return DataTypePrecedence.SqlBytes;
        case StorageType.SqlChars:
          return DataTypePrecedence.SqlChars;
        case StorageType.SqlDateTime:
          return DataTypePrecedence.SqlDateTime;
        case StorageType.SqlDecimal:
          return DataTypePrecedence.SqlDecimal;
        case StorageType.SqlDouble:
          return DataTypePrecedence.SqlDouble;
        case StorageType.SqlGuid:
          return DataTypePrecedence.SqlGuid;
        case StorageType.SqlInt16:
          return DataTypePrecedence.SqlInt16;
        case StorageType.SqlInt32:
          return DataTypePrecedence.SqlInt32;
        case StorageType.SqlInt64:
          return DataTypePrecedence.SqlInt64;
        case StorageType.SqlMoney:
          return DataTypePrecedence.SqlMoney;
        case StorageType.SqlSingle:
          return DataTypePrecedence.SqlSingle;
        case StorageType.SqlString:
          return DataTypePrecedence.SqlString;
        default:
          return DataTypePrecedence.Error;
      }
    }

    public static StorageType GetPrecedenceType(DataTypePrecedence code)
    {
      switch (code)
      {
        case DataTypePrecedence.SqlBinary:
          return StorageType.SqlBinary;
        case DataTypePrecedence.Char:
          return StorageType.Char;
        case DataTypePrecedence.String:
          return StorageType.String;
        case DataTypePrecedence.SqlString:
          return StorageType.SqlString;
        case DataTypePrecedence.SqlGuid:
          return StorageType.SqlGuid;
        case DataTypePrecedence.Boolean:
          return StorageType.Boolean;
        case DataTypePrecedence.SqlBoolean:
          return StorageType.SqlBoolean;
        case DataTypePrecedence.SByte:
          return StorageType.SByte;
        case DataTypePrecedence.SqlByte:
          return StorageType.SqlByte;
        case DataTypePrecedence.Byte:
          return StorageType.Byte;
        case DataTypePrecedence.Int16:
          return StorageType.Int16;
        case DataTypePrecedence.SqlInt16:
          return StorageType.SqlInt16;
        case DataTypePrecedence.UInt16:
          return StorageType.UInt16;
        case DataTypePrecedence.Int32:
          return StorageType.Int32;
        case DataTypePrecedence.SqlInt32:
          return StorageType.SqlInt32;
        case DataTypePrecedence.UInt32:
          return StorageType.UInt32;
        case DataTypePrecedence.Int64:
          return StorageType.Int64;
        case DataTypePrecedence.SqlInt64:
          return StorageType.SqlInt64;
        case DataTypePrecedence.UInt64:
          return StorageType.UInt64;
        case DataTypePrecedence.SqlMoney:
          return StorageType.SqlMoney;
        case DataTypePrecedence.Decimal:
          return StorageType.Decimal;
        case DataTypePrecedence.SqlDecimal:
          return StorageType.SqlDecimal;
        case DataTypePrecedence.Single:
          return StorageType.Single;
        case DataTypePrecedence.SqlSingle:
          return StorageType.SqlSingle;
        case DataTypePrecedence.Double:
          return StorageType.Double;
        case DataTypePrecedence.SqlDouble:
          return StorageType.SqlDouble;
        case DataTypePrecedence.TimeSpan:
          return StorageType.TimeSpan;
        case DataTypePrecedence.DateTime:
          return StorageType.DateTime;
        case DataTypePrecedence.SqlDateTime:
          return StorageType.SqlDateTime;
        default:
          return StorageType.Empty;
      }
    }

    public static int CompareNulls(object val1, object val2)
    {
      if (val1 == val2)
        return 0;
      if (val1 == DBNull.Value)
        return val2 == null ? 1 : -1;
      if (val2 == DBNull.Value)
        return val1 == null ? -1 : 1;
      if (val1 == null)
        return -1;
      return val2 == null ? 1 : 0;
    }

    public static int BinarySearch<T>(IList<T> list, T value)
    {
      return DataStorageHelper.BinarySearch<T>(list, value, (IComparer<T>) Comparer<T>.Default);
    }

    public static int BinarySearch<T>(IList<T> list, T value, IComparer<T> comparer)
    {
      if (object.ReferenceEquals((object) null, (object) list))
        throw new ArgumentNullException(nameof (list));
      if (object.ReferenceEquals((object) null, (object) comparer))
        throw new ArgumentNullException(nameof (comparer));
      int num1 = 0;
      int num2 = list.Count - 1;
      while (num1 <= num2)
      {
        int index = (num1 + num2) / 2;
        int num3 = comparer.Compare(value, list[index]);
        if (num3 == 0)
          return index;
        if (num3 < 0)
          num2 = index - 1;
        else
          num1 = index + 1;
      }
      return -1;
    }

    public static string EscapeName(string name)
    {
      if (string.IsNullOrEmpty(name))
        return name;
      StringBuilder stringBuilder = new StringBuilder(name);
      stringBuilder.Replace("\\", "\\\\");
      stringBuilder.Replace("]", "\\]");
      stringBuilder.Insert(0, "[");
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string EscapeLikeValue(string valueWithoutWildcards)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char ch in DataStorageHelper.EscapeValue(valueWithoutWildcards))
      {
        switch (ch)
        {
          case '%':
          case '*':
          case '[':
          case ']':
            stringBuilder.Append("[").Append(ch).Append("]");
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
      }
      return stringBuilder.ToString();
    }

    public static string EscapeValue(string valueWithoutWildcards)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < valueWithoutWildcards.Length; ++index)
      {
        char valueWithoutWildcard = valueWithoutWildcards[index];
        if (valueWithoutWildcard == '\'')
          stringBuilder.Append("''");
        else
          stringBuilder.Append(valueWithoutWildcard);
      }
      return stringBuilder.ToString();
    }
  }
}
