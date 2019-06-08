// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.Aggregates
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace Telerik.Data.Expressions
{
  internal static class Aggregates
  {
    private static IEnumerable<object> NotNullValue(
      IEnumerable data,
      ExpressionNode node,
      object context)
    {
      IEnumerator enumerator = data.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          object row = enumerator.Current;
          object value = node.Eval(row, context);
          if (value != null && DBNull.Value != value)
            yield return value;
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        disposable?.Dispose();
      }
    }

    [Description("Returns a sum of the values of the specified expression.")]
    public static object Sum(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      int count;
      StorageType type;
      return Aggregates.Sum(data, node, context.GlobalContext, nameof (Sum), out count, out type);
    }

    [Description("Returns the average of all non-null values from the specified expression.")]
    public static object Avg(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      int count;
      StorageType type;
      return Aggregates.Mean(data, node, context.GlobalContext, nameof (Avg), out count, out type);
    }

    [Description("Returns the first value from the specified expression.")]
    public static object First(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      using (IEnumerator<object> enumerator = Aggregates.NotNullValue(data, node, context.GlobalContext).GetEnumerator())
      {
        if (enumerator.MoveNext())
          return enumerator.Current;
      }
      return (object) null;
    }

    [Description("Returns the last value from the specified expression.")]
    public static object Last(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      object obj1 = (object) null;
      foreach (object obj2 in Aggregates.NotNullValue(data, node, context.GlobalContext))
        obj1 = obj2;
      return obj1;
    }

    [Description("Returns the minimum value from all non-null values of the specified expression.")]
    public static object Min(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      object obj1 = (object) null;
      foreach (object obj2 in Aggregates.NotNullValue(data, node, context.GlobalContext))
      {
        if (obj1 == null)
          obj1 = obj2;
        else if (0 < Aggregates.Compare(obj1, obj2, context, nameof (Min)))
          obj1 = obj2;
      }
      return obj1;
    }

    [Description("Returns the maximum value from all non-null values of the specified expression.")]
    public static object Max(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      object obj1 = (object) null;
      foreach (object obj2 in Aggregates.NotNullValue(data, node, context.GlobalContext))
      {
        if (obj1 == null)
          obj1 = obj2;
        else if (0 > Aggregates.Compare(obj1, obj2, context, nameof (Max)))
          obj1 = obj2;
      }
      return obj1;
    }

    [Description("Returns a count of the values from the specified expression.")]
    public static object Count(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      int num = 0;
      foreach (object obj in data)
        ++num;
      return (object) num;
    }

    [Description("Returns the standard deviation from the specified expression")]
    public static object StDev(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      IList<Decimal> numList = (IList<Decimal>) new List<Decimal>();
      Decimal num1 = new Decimal(0);
      foreach (object obj in Aggregates.NotNullValue(data, node, (object) context))
      {
        StorageType storageType = DataStorageHelper.GetStorageType(obj.GetType());
        if (ExpressionNode.IsNumeric(storageType))
        {
          Decimal num2 = Convert.ToDecimal(obj);
          num1 += num2;
          numList.Add(num2);
        }
        else
        {
          if (storageType != StorageType.String && storageType != StorageType.SqlString)
            throw new NotSupportedException("Standard deviation is not supported for type " + storageType.ToString());
          Decimal result;
          if (Decimal.TryParse(obj.ToString(), out result))
          {
            num1 += result;
            numList.Add(result);
          }
        }
      }
      if (numList.Count == 0)
        return (object) 0;
      Decimal num3 = num1 / (Decimal) numList.Count;
      Decimal num4 = new Decimal(0);
      foreach (Decimal num2 in (IEnumerable<Decimal>) numList)
      {
        Decimal num5 = num2 - num3;
        Decimal num6 = num5 * num5;
        num4 += num6;
      }
      return (object) Aggregates.DecimalSqrt(num4 / (Decimal) numList.Count, new Decimal?());
    }

    private static Decimal DecimalSqrt(Decimal x, Decimal? guess = null)
    {
      Decimal valueOrDefault = guess.GetValueOrDefault(x / new Decimal(2));
      Decimal num1 = x / valueOrDefault;
      Decimal num2 = (valueOrDefault + num1) / new Decimal(2);
      if (num2 == valueOrDefault)
        return num2;
      return Aggregates.DecimalSqrt(x, new Decimal?(num2));
    }

    [Description("Returns the variance from the specified expression")]
    public static object Var(FunctionContext context, IEnumerable data, ExpressionNode node)
    {
      IList<Decimal> numList = (IList<Decimal>) new List<Decimal>();
      Decimal num1 = new Decimal(0);
      Decimal num2 = new Decimal(0);
      foreach (object obj in Aggregates.NotNullValue(data, node, context.GlobalContext))
      {
        StorageType storageType = DataStorageHelper.GetStorageType(obj.GetType());
        if (ExpressionNode.IsNumeric(storageType))
        {
          Decimal num3 = Convert.ToDecimal(obj);
          numList.Add(num3);
          num2 += num3;
        }
        else
        {
          if (storageType != StorageType.String && storageType != StorageType.SqlString)
            throw new NotSupportedException("Standard deviation is not supported for type " + storageType.ToString());
          Decimal result;
          if (Decimal.TryParse(obj.ToString(), out result))
          {
            numList.Add(result);
            num2 += result;
          }
        }
      }
      if (numList.Count == 0)
        return (object) 0;
      Decimal num4 = num2 / (Decimal) numList.Count;
      foreach (Decimal num3 in (IEnumerable<Decimal>) numList)
      {
        Decimal num5 = num3 - num4;
        num1 += num5 * num5;
      }
      return (object) (num1 / (Decimal) numList.Count);
    }

    private static int Compare(object value1, object value2, FunctionContext context, string exp)
    {
      Type type1 = value1.GetType();
      Type type2 = value2.GetType();
      StorageType storageType1 = DataStorageHelper.GetStorageType(type1);
      StorageType storageType2 = DataStorageHelper.GetStorageType(type2);
      bool flag1 = DataStorageHelper.IsSqlType(storageType1);
      bool flag2 = DataStorageHelper.IsSqlType(storageType2);
      if (flag1 && DataStorageHelper.IsObjectSqlNull(value1) || value1 == DBNull.Value)
        return -1;
      if (flag2 && DataStorageHelper.IsObjectSqlNull(value2) || value2 == DBNull.Value)
        return 1;
      StorageType resultType = flag1 || flag2 ? DataStorageHelper.ResultSqlType(storageType1, storageType2, false, false, Operator.LessThen) : DataStorageHelper.ResultType(storageType1, storageType2, false, false, Operator.LessThen);
      if (resultType == StorageType.Empty)
        throw InvalidExpressionException.TypeMismatch(exp);
      return Operator.BinaryCompare(value1, value2, resultType, Operator.LessOrEqual, new OperatorContext((object) null, context.FormatProvider, (object) null));
    }

    private static object Sum(
      IEnumerable data,
      ExpressionNode node,
      object context,
      string exp,
      out int count,
      out StorageType type)
    {
      count = 0;
      type = StorageType.Empty;
      object obj1 = (object) 0;
      foreach (object obj2 in Aggregates.NotNullValue(data, node, context))
      {
        ++count;
        if (type == StorageType.Empty)
        {
          type = DataStorageHelper.GetStorageType(obj2.GetType());
          if (count == 1 && type == StorageType.String)
            obj1 = (object) new Decimal(0);
          if (type != StorageType.SqlDecimal && type != StorageType.SqlMoney && (type != StorageType.Decimal && type != StorageType.Single))
          {
            if (ExpressionNode.IsUnsignedSql(type))
              type = StorageType.UInt64;
            if (ExpressionNode.IsInteger(type))
              type = StorageType.Int64;
            else if (ExpressionNode.IsIntegerSql(type))
              type = StorageType.SqlInt64;
            else if (ExpressionNode.IsFloat(type))
              type = StorageType.Double;
            else if (ExpressionNode.IsFloatSql(type))
              type = StorageType.SqlDouble;
          }
        }
        obj1 = Aggregates.Add(obj1, obj2, type);
      }
      return obj1;
    }

    private static object Add(object value1, object value2, StorageType type)
    {
      switch (type)
      {
        case StorageType.Char:
          return (object) ((int) Convert.ToChar(value1) + (int) Convert.ToChar(value2));
        case StorageType.SByte:
          return (object) ((int) Convert.ToSByte(value1) + (int) Convert.ToSByte(value2));
        case StorageType.Byte:
          return (object) ((int) Convert.ToByte(value1) + (int) Convert.ToByte(value2));
        case StorageType.Int16:
          return (object) ((int) Convert.ToInt16(value1) + (int) Convert.ToInt16(value2));
        case StorageType.UInt16:
          return (object) ((int) Convert.ToUInt16(value1) + (int) Convert.ToUInt16(value2));
        case StorageType.Int32:
          return (object) (Convert.ToInt32(value1) + Convert.ToInt32(value2));
        case StorageType.UInt32:
          return (object) (uint) ((int) Convert.ToUInt32(value1) + (int) Convert.ToUInt32(value2));
        case StorageType.Int64:
          return (object) (Convert.ToInt64(value1) + Convert.ToInt64(value2));
        case StorageType.UInt64:
          return (object) (ulong) ((long) Convert.ToUInt64(value1) + (long) Convert.ToUInt64(value2));
        case StorageType.Single:
          return (object) (float) ((double) Convert.ToSingle(value1) + (double) Convert.ToSingle(value2));
        case StorageType.Double:
          return (object) (Convert.ToDouble(value1) + Convert.ToDouble(value2));
        case StorageType.Decimal:
          return (object) (Convert.ToDecimal(value1) + Convert.ToDecimal(value2));
        case StorageType.TimeSpan:
          TimeSpan result1;
          TimeSpan.TryParse(Convert.ToString(value1), out result1);
          TimeSpan result2;
          TimeSpan.TryParse(Convert.ToString(value2), out result2);
          return (object) (result1 + result2);
        case StorageType.String:
          Decimal result3;
          Decimal.TryParse(Convert.ToString(value1), out result3);
          Decimal result4;
          Decimal.TryParse(Convert.ToString(value2), out result4);
          return (object) (result3 + result4);
        case StorageType.SqlByte:
          return (object) ((SqlByte) value1 + (SqlByte) value2);
        case StorageType.SqlDecimal:
          return (object) ((SqlDecimal) value1 + (SqlDecimal) value2);
        case StorageType.SqlDouble:
          return (object) ((SqlDouble) value1 + (SqlDouble) value2);
        case StorageType.SqlInt16:
          return (object) ((SqlInt16) value1 + (SqlInt16) value2);
        case StorageType.SqlInt32:
          return (object) ((SqlInt32) value1 + (SqlInt32) value2);
        case StorageType.SqlInt64:
          return (object) ((SqlInt64) value1 + (SqlInt64) value2);
        case StorageType.SqlMoney:
          return (object) ((SqlMoney) value1 + (SqlMoney) value2);
        case StorageType.SqlSingle:
          return (object) ((SqlSingle) value1 + (SqlSingle) value2);
        case StorageType.SqlString:
          return (object) ((SqlString) value1 + (SqlString) value2);
        default:
          throw InvalidExpressionException.TypeMismatch(nameof (Add));
      }
    }

    private static object Subtract(object value1, object value2, StorageType type)
    {
      switch (type)
      {
        case StorageType.Char:
          return (object) ((int) Convert.ToChar(value1) - (int) Convert.ToChar(value2));
        case StorageType.SByte:
          return (object) ((int) Convert.ToSByte(value1) - (int) Convert.ToSByte(value2));
        case StorageType.Byte:
          return (object) ((int) Convert.ToByte(value1) - (int) Convert.ToByte(value2));
        case StorageType.Int16:
          return (object) ((int) Convert.ToInt16(value1) - (int) Convert.ToInt16(value2));
        case StorageType.UInt16:
          return (object) ((int) Convert.ToUInt16(value1) - (int) Convert.ToUInt16(value2));
        case StorageType.Int32:
          return (object) (Convert.ToInt32(value1) - Convert.ToInt32(value2));
        case StorageType.UInt32:
          return (object) (uint) ((int) Convert.ToUInt32(value1) - (int) Convert.ToUInt32(value2));
        case StorageType.Int64:
          return (object) (Convert.ToInt64(value1) - Convert.ToInt64(value2));
        case StorageType.UInt64:
          return (object) (ulong) ((long) Convert.ToUInt64(value1) - (long) Convert.ToUInt64(value2));
        case StorageType.Single:
          return (object) (float) ((double) Convert.ToSingle(value1) - (double) Convert.ToSingle(value2));
        case StorageType.Double:
          return (object) (Convert.ToDouble(value1) - Convert.ToDouble(value2));
        case StorageType.Decimal:
          return (object) (Convert.ToDecimal(value1) - Convert.ToDecimal(value2));
        case StorageType.TimeSpan:
          return (object) ((TimeSpan) value1 - (TimeSpan) value2);
        case StorageType.String:
          Decimal result1;
          Decimal.TryParse(Convert.ToString(value1), out result1);
          Decimal result2;
          Decimal.TryParse(Convert.ToString(value2), out result2);
          return (object) (result1 - result2);
        case StorageType.SqlByte:
          return (object) ((SqlByte) value1 + (SqlByte) value2);
        case StorageType.SqlDecimal:
          return (object) ((SqlDecimal) value1 - (SqlDecimal) value2);
        case StorageType.SqlDouble:
          return (object) ((SqlDouble) value1 - (SqlDouble) value2);
        case StorageType.SqlInt16:
          return (object) ((SqlInt16) value1 - (SqlInt16) value2);
        case StorageType.SqlInt32:
          return (object) ((SqlInt32) value1 - (SqlInt32) value2);
        case StorageType.SqlInt64:
          return (object) ((SqlInt64) value1 - (SqlInt64) value2);
        case StorageType.SqlMoney:
          return (object) ((SqlMoney) value1 - (SqlMoney) value2);
        case StorageType.SqlSingle:
          return (object) ((SqlSingle) value1 - (SqlSingle) value2);
        default:
          throw InvalidExpressionException.TypeMismatch(nameof (Subtract));
      }
    }

    private static object Multiply(object value1, object value2, StorageType type)
    {
      switch (type)
      {
        case StorageType.Char:
          return (object) ((int) Convert.ToChar(value1) * (int) Convert.ToChar(value2));
        case StorageType.SByte:
          return (object) ((int) Convert.ToSByte(value1) * (int) Convert.ToSByte(value2));
        case StorageType.Byte:
          return (object) ((int) Convert.ToByte(value1) * (int) Convert.ToByte(value2));
        case StorageType.Int16:
          return (object) ((int) Convert.ToInt16(value1) * (int) Convert.ToInt16(value2));
        case StorageType.UInt16:
          return (object) ((int) Convert.ToUInt16(value1) * (int) Convert.ToUInt16(value2));
        case StorageType.Int32:
          return (object) (Convert.ToInt32(value1) * Convert.ToInt32(value2));
        case StorageType.UInt32:
          return (object) (uint) ((int) Convert.ToUInt32(value1) * (int) Convert.ToUInt32(value2));
        case StorageType.Int64:
          return (object) (Convert.ToInt64(value1) * Convert.ToInt64(value2));
        case StorageType.UInt64:
          return (object) (ulong) ((long) Convert.ToUInt64(value1) * (long) Convert.ToUInt64(value2));
        case StorageType.Single:
          return (object) (float) ((double) Convert.ToSingle(value1) * (double) Convert.ToSingle(value2));
        case StorageType.Double:
          return (object) (Convert.ToDouble(value1) * Convert.ToDouble(value2));
        case StorageType.Decimal:
          return (object) (Convert.ToDecimal(value1) * Convert.ToDecimal(value2));
        case StorageType.String:
          Decimal result1;
          Decimal.TryParse(Convert.ToString(value1), out result1);
          Decimal result2;
          Decimal.TryParse(Convert.ToString(value2), out result2);
          return (object) (result1 * result2);
        case StorageType.SqlByte:
          return (object) ((SqlByte) value1 * (SqlByte) value2);
        case StorageType.SqlDecimal:
          return (object) ((SqlDecimal) value1 * (SqlDecimal) value2);
        case StorageType.SqlDouble:
          return (object) ((SqlDouble) value1 * (SqlDouble) value2);
        case StorageType.SqlInt16:
          return (object) ((SqlInt16) value1 * (SqlInt16) value2);
        case StorageType.SqlInt32:
          return (object) ((SqlInt32) value1 * (SqlInt32) value2);
        case StorageType.SqlInt64:
          return (object) ((SqlInt64) value1 * (SqlInt64) value2);
        case StorageType.SqlMoney:
          return (object) ((SqlMoney) value1 * (SqlMoney) value2);
        case StorageType.SqlSingle:
          return (object) ((SqlSingle) value1 * (SqlSingle) value2);
        default:
          throw InvalidExpressionException.TypeMismatch(nameof (Multiply));
      }
    }

    private static object Mean(
      IEnumerable data,
      ExpressionNode node,
      object context,
      string exp,
      out int count,
      out StorageType type)
    {
      count = 0;
      type = StorageType.Empty;
      object obj = Aggregates.Sum(data, node, context, exp, out count, out type);
      if (count == 0)
        return (object) 0;
      switch (type)
      {
        case StorageType.Char:
          return (object) ((int) (char) obj / count);
        case StorageType.SByte:
        case StorageType.Byte:
        case StorageType.Int16:
        case StorageType.UInt16:
        case StorageType.Int32:
        case StorageType.UInt32:
          return (object) (Convert.ToDouble(obj) / (double) count);
        case StorageType.Int64:
        case StorageType.UInt64:
          return (object) (Convert.ToDecimal(obj) / (Decimal) count);
        case StorageType.Single:
          return (object) (float) ((double) (float) obj / (double) count);
        case StorageType.Double:
          return (object) ((double) obj / (double) count);
        case StorageType.Decimal:
        case StorageType.String:
          return (object) ((Decimal) obj / (Decimal) count);
        case StorageType.TimeSpan:
          return (object) new TimeSpan(((TimeSpan) obj).Ticks / (long) count);
        case StorageType.SqlByte:
          return (object) ((SqlDouble) ((SqlByte) obj) / (SqlDouble) ((double) count));
        case StorageType.SqlDecimal:
          return (object) ((SqlDouble) ((SqlDecimal) obj) / (SqlDouble) ((double) count));
        case StorageType.SqlDouble:
          return (object) ((SqlDouble) obj / (SqlDouble) ((double) count));
        case StorageType.SqlInt16:
          return (object) ((SqlDouble) ((SqlInt16) obj) / (SqlDouble) ((double) (short) count));
        case StorageType.SqlInt32:
          return (object) ((SqlDouble) ((SqlInt32) obj) / (SqlDouble) ((double) count));
        case StorageType.SqlInt64:
          return (object) ((SqlDouble) ((SqlInt64) obj) / (SqlDouble) ((double) count));
        case StorageType.SqlMoney:
          return (object) ((SqlMoney) obj / (SqlMoney) ((long) count));
        case StorageType.SqlSingle:
          return (object) ((SqlSingle) obj / (SqlSingle) ((float) count));
        default:
          throw InvalidExpressionException.TypeMismatch(exp);
      }
    }
  }
}
