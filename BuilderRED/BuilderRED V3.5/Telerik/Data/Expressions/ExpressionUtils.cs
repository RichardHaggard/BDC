// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ExpressionUtils
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace Telerik.Data.Expressions
{
  internal static class ExpressionUtils
  {
    private static readonly string[] aggregates = new string[9]
    {
      "sum",
      "avg",
      "count",
      "first",
      "last",
      "min",
      "max",
      "stdev",
      "var"
    };

    public static bool IsExpression(string value)
    {
      return (value ?? "").StartsWith("=");
    }

    public static bool IsAggregateExpression(string value)
    {
      value = value.ToLower();
      foreach (object aggregate in ExpressionUtils.aggregates)
      {
        string str = string.Format("={0}(", aggregate);
        if (value.StartsWith(str))
          return true;
      }
      return false;
    }

    public static string GetFieldReference(string value)
    {
      Match match = Regex.Match(value, "^=Fields\\.(\\w+)$");
      if (!match.Success)
        match = Regex.Match(value, "^=(\\w+)$");
      if (match.Success)
        return match.Groups[1].Value;
      return string.Empty;
    }

    public static bool IsNumericType(Type type)
    {
      if ((object) type != (object) typeof (sbyte) && (object) type != (object) typeof (byte) && ((object) type != (object) typeof (short) && (object) type != (object) typeof (ushort)) && ((object) type != (object) typeof (int) && (object) type != (object) typeof (uint) && ((object) type != (object) typeof (long) && (object) type != (object) typeof (ulong))) && ((object) type != (object) typeof (float) && (object) type != (object) typeof (double) && ((object) type != (object) typeof (Decimal) && (object) type != (object) typeof (SqlByte)) && ((object) type != (object) typeof (SqlDecimal) && (object) type != (object) typeof (SqlDouble) && ((object) type != (object) typeof (SqlInt16) && (object) type != (object) typeof (SqlInt32)))) && ((object) type != (object) typeof (SqlInt64) && (object) type != (object) typeof (SqlMoney)))
        return (object) type == (object) typeof (SqlSingle);
      return true;
    }

    public static string SplitName(string name)
    {
      return Regex.Replace(name, "(\\p{Ll})(\\p{Lu})|_+", "$1 $2");
    }
  }
}
