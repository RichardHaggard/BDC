// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ExpressionNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  public abstract class ExpressionNode
  {
    protected CultureInfo Culture
    {
      get
      {
        return CultureInfo.InvariantCulture;
      }
    }

    public virtual bool IsConst
    {
      get
      {
        return false;
      }
    }

    public abstract object Eval(object row, object context);

    internal static bool IsFloat(StorageType type)
    {
      if (type != StorageType.Single && type != StorageType.Double)
        return type == StorageType.Decimal;
      return true;
    }

    internal static bool IsFloatSql(StorageType type)
    {
      if (type != StorageType.Single && type != StorageType.Double && (type != StorageType.Decimal && type != StorageType.SqlDouble) && (type != StorageType.SqlDecimal && type != StorageType.SqlMoney))
        return type == StorageType.SqlSingle;
      return true;
    }

    internal static bool IsInteger(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.UInt16) && (type != StorageType.UInt32 && type != StorageType.UInt64 && type != StorageType.SByte))
        return type == StorageType.Byte;
      return true;
    }

    internal static bool IsIntegerSql(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.UInt16) && (type != StorageType.UInt32 && type != StorageType.UInt64 && (type != StorageType.SByte && type != StorageType.Byte)) && (type != StorageType.SqlInt64 && type != StorageType.SqlInt32 && type != StorageType.SqlInt16))
        return type == StorageType.SqlByte;
      return true;
    }

    internal static bool IsNumeric(StorageType type)
    {
      if (!ExpressionNode.IsFloat(type))
        return ExpressionNode.IsInteger(type);
      return true;
    }

    internal static bool IsNumericSql(StorageType type)
    {
      if (!ExpressionNode.IsFloatSql(type))
        return ExpressionNode.IsIntegerSql(type);
      return true;
    }

    internal static bool IsSigned(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.SByte))
        return ExpressionNode.IsFloat(type);
      return true;
    }

    internal static bool IsSignedSql(StorageType type)
    {
      if (type != StorageType.Int16 && type != StorageType.Int32 && (type != StorageType.Int64 && type != StorageType.SByte) && (type != StorageType.SqlInt64 && type != StorageType.SqlInt32 && type != StorageType.SqlInt16))
        return ExpressionNode.IsFloatSql(type);
      return true;
    }

    internal static bool IsUnsigned(StorageType type)
    {
      if (type != StorageType.UInt16 && type != StorageType.UInt32 && type != StorageType.UInt64)
        return type == StorageType.Byte;
      return true;
    }

    internal static bool IsUnsignedSql(StorageType type)
    {
      if (type != StorageType.UInt16 && type != StorageType.UInt32 && (type != StorageType.UInt64 && type != StorageType.SqlByte))
        return type == StorageType.Byte;
      return true;
    }

    public static List<T> GetNodes<T>(ExpressionNode node) where T : ExpressionNode
    {
      List<T> objList = new List<T>();
      if (node == null)
        return objList;
      Stack<ExpressionNode> expressionNodeStack = new Stack<ExpressionNode>();
      expressionNodeStack.Push(node);
      while (expressionNodeStack.Count > 0)
      {
        ExpressionNode expressionNode1 = expressionNodeStack.Pop();
        if (expressionNode1 is T)
          objList.Add((T) expressionNode1);
        IEnumerable<ExpressionNode> childNodes = expressionNode1.GetChildNodes();
        if (childNodes != null)
        {
          foreach (ExpressionNode expressionNode2 in childNodes)
          {
            if (expressionNode2 != null)
              expressionNodeStack.Push(expressionNode2);
          }
        }
      }
      return objList;
    }

    public static bool IsIncrementalFiltering(
      string prevExpression,
      string filterExpression,
      bool caseSensitive)
    {
      if (string.IsNullOrEmpty(prevExpression))
        return false;
      if (prevExpression == filterExpression)
        return true;
      BinaryOpNode binaryOpNode1 = ExpressionParser.Parse(prevExpression, caseSensitive) as BinaryOpNode;
      BinaryOpNode binaryOpNode2 = ExpressionParser.Parse(filterExpression, caseSensitive) as BinaryOpNode;
      prevExpression = filterExpression;
      if (binaryOpNode1 == null || binaryOpNode2 == null || (binaryOpNode1.Op != Operator.Like || binaryOpNode2.Op != Operator.Like))
        return false;
      NameNode left1 = binaryOpNode1.Left as NameNode;
      NameNode left2 = binaryOpNode2.Left as NameNode;
      if (left1 == null || left2 == null || left1.Name != left2.Name)
        return false;
      ConstNode right1 = binaryOpNode1.Right as ConstNode;
      ConstNode right2 = binaryOpNode2.Right as ConstNode;
      if (right1 == null || right2 == null)
        return false;
      string str1 = right1.Value.ToString();
      string str2 = right2.Value.ToString();
      if (str1.Contains("%"))
      {
        string str3 = str1.Replace("%", string.Empty);
        string str4 = str2.Replace("%", string.Empty);
        if (str4.Length > 1 && str4.Substring(0, str4.Length - 1) == str3)
          return true;
      }
      return false;
    }

    public virtual IEnumerable<ExpressionNode> GetChildNodes()
    {
      return (IEnumerable<ExpressionNode>) new ExpressionNode[0];
    }
  }
}
