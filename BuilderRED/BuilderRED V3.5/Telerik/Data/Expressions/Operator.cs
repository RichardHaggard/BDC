// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.Operator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Threading;

namespace Telerik.Data.Expressions
{
  public sealed class Operator
  {
    public static Operator Noop = new Operator("", 0, (Delegate) new Operator.UnaryFunc(Operator.RValueFunc));
    public static Operator Negative = new Operator("-", 20, (Delegate) new Operator.UnaryFunc(Operator.NegativeFunc));
    public static Operator UnaryPlus = new Operator("+", 20, (Delegate) new Operator.UnaryFunc(Operator.PositiveFunc));
    public static Operator Not = new Operator(nameof (Not), 9, (Delegate) new Operator.UnaryFunc(Operator.NotFunc));
    public static Operator BetweenAnd = new Operator(nameof (BetweenAnd), 12, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator In = new Operator(nameof (In), 11, (Delegate) new Operator.BinaryFunc(Operator.InFunc));
    public static Operator Between = new Operator(nameof (Between), 11, (Delegate) new Operator.TernaryFunc(Operator.BetweenFunc));
    public static Operator EqualTo = new Operator("=", 13, (Delegate) new Operator.BinaryFunc(Operator.EqualToFunc));
    public static Operator GreaterThen = new Operator(">", 13, (Delegate) new Operator.BinaryFunc(Operator.GreaterThenFunc));
    public static Operator LessThen = new Operator("<", 13, (Delegate) new Operator.BinaryFunc(Operator.LessThenFunc));
    public static Operator GreaterOrEqual = new Operator(">=", 13, (Delegate) new Operator.BinaryFunc(Operator.GreaterOrEqualFunc));
    public static Operator LessOrEqual = new Operator("<=", 13, (Delegate) new Operator.BinaryFunc(Operator.LessOrEqualFunc));
    public static Operator NotEqual = new Operator("<>", 13, (Delegate) new Operator.BinaryFunc(Operator.NotEqualFunc));
    public static Operator Is = new Operator(nameof (Is), 10, (Delegate) new Operator.BinaryFunc(Operator.IsFunc));
    public static Operator Like = new Operator(nameof (Like), 11, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator Plus = new Operator("+", 16, (Delegate) new Operator.BinaryFunc(Operator.AddFunc));
    public static Operator Minus = new Operator("-", 16, (Delegate) new Operator.BinaryFunc(Operator.SubtractFunc));
    public static Operator Multiply = new Operator("*", 19, (Delegate) new Operator.BinaryFunc(Operator.MultiplyFunc));
    public static Operator Divide = new Operator("/", 19, (Delegate) new Operator.BinaryFunc(Operator.DivideFunc));
    public static Operator Modulo = new Operator("Mod", 17, (Delegate) new Operator.BinaryFunc(Operator.ModuloFunc));
    public static Operator BitwiseAnd = new Operator("&", 8, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator BitwiseOr = new Operator("|", 7, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator BitwiseNot = new Operator("^", 6, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator BitwiseXor = new Operator("~", 9, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator And = new Operator(nameof (And), 8, (Delegate) new Operator.BinaryFunc(Operator.AndFunc));
    public static Operator Or = new Operator(nameof (Or), 7, (Delegate) new Operator.BinaryFunc(Operator.OrFunc));
    public static Operator Proc = new Operator(nameof (Proc), 2, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator Iff = new Operator(nameof (Iff), 22, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator Qual = new Operator(".", 23, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator Dot = new Operator(".", 23, (Delegate) new Operator.BinaryFunc(Operator.NotImpl));
    public static Operator Null = new Operator(nameof (Null), 24, (Delegate) (() => (object) null));
    public static Operator True = new Operator(nameof (True), 24, (Delegate) (() => (object) true));
    public static Operator False = new Operator(nameof (False), 24, (Delegate) (() => (object) false));
    public static Operator IsNot = new Operator("Is Not", 24, (Delegate) new Operator.UnaryFunc(Operator.IsNotFunc));
    private static readonly Operator[] unaryOps = new Operator[3]
    {
      Operator.UnaryPlus,
      Operator.Negative,
      Operator.Not
    };
    private static readonly Operator[] binaryOps = new Operator[17]
    {
      Operator.In,
      Operator.EqualTo,
      Operator.GreaterThen,
      Operator.LessThen,
      Operator.GreaterOrEqual,
      Operator.LessOrEqual,
      Operator.NotEqual,
      Operator.Is,
      Operator.Plus,
      Operator.Minus,
      Operator.Multiply,
      Operator.Divide,
      Operator.Modulo,
      Operator.And,
      Operator.Or,
      Operator.Between,
      Operator.BetweenAnd
    };
    private static Dictionary<int, Operator.ParsedSetCondition> previousExpressions = new Dictionary<int, Operator.ParsedSetCondition>();
    private static readonly object syncObj = new object();
    private const int MAX_CACHED_EXPRESSIONS = 50;
    public readonly int Priority;
    public readonly string Name;
    public readonly Delegate Func;

    public static bool IsUnary(Operator op)
    {
      return Array.IndexOf<Operator>(Operator.unaryOps, op) >= 0;
    }

    public static bool IsTernary(Operator op)
    {
      return op == Operator.BetweenAnd;
    }

    public static bool IsBinary(Operator op)
    {
      return Array.IndexOf<Operator>(Operator.binaryOps, op) >= 0;
    }

    internal static bool IsArithmetical(Operator op)
    {
      if (op != Operator.Plus && op != Operator.Minus && (op != Operator.Multiply && op != Operator.Divide))
        return op == Operator.Modulo;
      return true;
    }

    internal static bool IsLogical(Operator op)
    {
      if (Operator.And != op && Operator.Or != op && Operator.Not != op)
        return Operator.Is == op;
      return true;
    }

    internal static bool IsRelational(Operator op)
    {
      if (Operator.EqualTo != op && Operator.GreaterThen != op && (Operator.LessThen != op && Operator.GreaterOrEqual != op) && Operator.LessOrEqual != op)
        return Operator.NotEqual == op;
      return true;
    }

    private Operator(string name, int priority, Delegate func)
    {
      this.Priority = priority;
      this.Name = name;
      this.Func = func;
    }

    public override string ToString()
    {
      return this.Name;
    }

    private static object NotImpl(Operand lhs, Operand rhs, OperatorContext context)
    {
      throw new NotImplementedException("The requested op is not implemented.");
    }

    private static object NullFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      return (object) null;
    }

    private static object RValueFunc(Operand rhs, OperatorContext context)
    {
      object returnValue;
      if (Operator.TryParseSetCondition(rhs, out returnValue))
        return returnValue;
      Operator.EvaluateIteratively(rhs);
      return rhs.Value;
    }

    private static void EvaluateIteratively(Operand rhs)
    {
      Stack<Operand> operandStack = new Stack<Operand>();
      operandStack.Push(rhs);
      while (operandStack.Count > 0)
      {
        Operand operand = operandStack.Peek();
        if (!operand.HasValue)
        {
          BinaryOpNode node = operand.Node as BinaryOpNode;
          if (node != null)
          {
            Operand leftOperand = node.GetLeftOperand(rhs.Row, operand.ExpressionContext);
            Operand rightOperand = node.GetRightOperand(rhs.Row, operand.ExpressionContext);
            if (leftOperand.HasValue && rightOperand.HasValue)
            {
              operand.ForceEvaluate();
            }
            else
            {
              if (!leftOperand.HasValue)
                operandStack.Push(node.GetLeftOperand(rhs.Row, operand.ExpressionContext));
              if (!rightOperand.HasValue)
                operandStack.Push(node.GetRightOperand(rhs.Row, operand.ExpressionContext));
            }
          }
          else
            operand.ForceEvaluate();
        }
        else
          operandStack.Pop();
      }
    }

    private static bool TryParseSetCondition(Operand rhs, out object returnValue)
    {
      returnValue = (object) null;
      int managedThreadId = Thread.CurrentThread.ManagedThreadId;
      Operator.ParsedSetCondition parsedSetCondition1 = (Operator.ParsedSetCondition) null;
      lock (Operator.syncObj)
      {
        if (Operator.previousExpressions.Count > 50)
          Operator.previousExpressions.Clear();
        parsedSetCondition1 = !Operator.previousExpressions.ContainsKey(managedThreadId) || Operator.previousExpressions[managedThreadId].ExpressionNode != rhs.Node ? (Operator.ParsedSetCondition) null : Operator.previousExpressions[managedThreadId];
      }
      if (parsedSetCondition1 == null)
      {
        List<ConstNode> values;
        NameNode nameNode;
        bool isContainsOperator;
        if (!Operator.TryParseSetCondition(rhs.Node, Operator.EqualTo, Operator.Or, out values, out nameNode, out isContainsOperator) && !Operator.TryParseSetCondition(rhs.Node, Operator.NotEqual, Operator.And, out values, out nameNode, out isContainsOperator))
          return false;
        Operator.ParsedSetCondition parsedSetCondition2 = new Operator.ParsedSetCondition();
        parsedSetCondition2.IsContainsOperator = isContainsOperator;
        parsedSetCondition2.ExpressionNode = rhs.Node;
        foreach (ConstNode node in values)
        {
          if (node == null)
            parsedSetCondition2.ValuesContainNull = true;
          else
            parsedSetCondition2.AddConstNode(node, rhs.Row, rhs.ExpressionContext);
        }
        lock (Operator.syncObj)
          Operator.previousExpressions[managedThreadId] = parsedSetCondition2;
        returnValue = parsedSetCondition2.PassesFilter(nameNode, rhs.Row, rhs.ExpressionContext);
        return true;
      }
      NameNode firstNameNode = Operator.FindFirstNameNode(rhs.Node);
      returnValue = parsedSetCondition1.PassesFilter(firstNameNode, rhs.Row, rhs.ExpressionContext);
      return true;
    }

    private static NameNode FindFirstNameNode(ExpressionNode node)
    {
      Stack<ExpressionNode> expressionNodeStack = new Stack<ExpressionNode>();
      expressionNodeStack.Push(node);
      while (expressionNodeStack.Count > 0)
      {
        ExpressionNode expressionNode = expressionNodeStack.Pop();
        BinaryOpNode binaryOpNode = expressionNode as BinaryOpNode;
        UnaryOpNode unaryOpNode = expressionNode as UnaryOpNode;
        NameNode nameNode = expressionNode as NameNode;
        if (binaryOpNode != null)
        {
          expressionNodeStack.Push(binaryOpNode.Left);
          expressionNodeStack.Push(binaryOpNode.Right);
        }
        if (unaryOpNode != null)
          expressionNodeStack.Push(unaryOpNode.Right);
        if (nameNode != null)
          return nameNode;
      }
      return (NameNode) null;
    }

    private static bool TryParseSetCondition(
      ExpressionNode node,
      Operator equalityOperator,
      Operator logicalOperator,
      out List<ConstNode> values,
      out NameNode nameNode,
      out bool isContainsOperator)
    {
      isContainsOperator = logicalOperator == Operator.Or;
      Stack<ExpressionNode> expressionNodeStack = new Stack<ExpressionNode>();
      expressionNodeStack.Push(node);
      values = new List<ConstNode>();
      nameNode = (NameNode) null;
      while (expressionNodeStack.Count > 0)
      {
        ExpressionNode expressionNode = expressionNodeStack.Pop();
        BinaryOpNode binaryOpNode = expressionNode as BinaryOpNode;
        UnaryOpNode unaryOpNode = expressionNode as UnaryOpNode;
        if (binaryOpNode != null)
        {
          if (binaryOpNode.Op == logicalOperator)
          {
            BinaryOpNode left1 = binaryOpNode.Left as BinaryOpNode;
            BinaryOpNode right1 = binaryOpNode.Right as BinaryOpNode;
            UnaryOpNode left2 = binaryOpNode.Left as UnaryOpNode;
            UnaryOpNode right2 = binaryOpNode.Right as UnaryOpNode;
            if (left2 != null && right1 == null || right2 != null && left1 == null || left2 != null && right2 != null || left2 == null && right2 == null && (left1 == null || right1 == null) || left2 == null && right2 == null && (left1.Op != equalityOperator || right1.Op != equalityOperator) && ((left1.Op != equalityOperator || right1.Op != logicalOperator) && (left1.Op != logicalOperator || right1.Op != equalityOperator)) && ((left1.Op != logicalOperator || right1.Op != Operator.Is) && (left1.Op != Operator.Is || right1.Op != equalityOperator)))
              return false;
            expressionNodeStack.Push(binaryOpNode.Left);
            expressionNodeStack.Push(binaryOpNode.Right);
          }
          else if (binaryOpNode.Op == equalityOperator)
          {
            NameNode nameNode1 = binaryOpNode.Left as NameNode ?? binaryOpNode.Right as NameNode;
            ConstNode constNode = binaryOpNode.Left as ConstNode ?? binaryOpNode.Right as ConstNode;
            if (nameNode1 == null || constNode == null || nameNode != null && nameNode.Name != nameNode1.Name)
              return false;
            values.Add(constNode);
            nameNode = nameNode ?? nameNode1;
          }
          else
          {
            if (binaryOpNode.Op != Operator.Is || !isContainsOperator)
              return false;
            NameNode nameNode1 = binaryOpNode.Left as NameNode ?? binaryOpNode.Right as NameNode;
            ZeroOpNode zeroOpNode = binaryOpNode.Left as ZeroOpNode ?? binaryOpNode.Right as ZeroOpNode;
            if (nameNode1 == null || zeroOpNode == null || nameNode != null && nameNode.Name != nameNode1.Name)
              return false;
            values.Add((ConstNode) null);
            nameNode = nameNode ?? nameNode1;
          }
        }
        else
        {
          if (unaryOpNode == null || isContainsOperator)
            return false;
          UnaryOpNode right1 = unaryOpNode.Right as UnaryOpNode;
          if (right1 == null || right1.Op != Operator.Noop)
            return false;
          BinaryOpNode right2 = right1.Right as BinaryOpNode;
          if (right2.Op != Operator.Is)
            return false;
          NameNode nameNode1 = right2.Left as NameNode ?? right2.Right as NameNode;
          ZeroOpNode zeroOpNode = right2.Left as ZeroOpNode ?? right2.Right as ZeroOpNode;
          if (nameNode1 == null || zeroOpNode == null || nameNode != null && nameNode.Name != nameNode1.Name)
            return false;
          values.Add((ConstNode) null);
          nameNode = nameNode ?? nameNode1;
        }
      }
      return nameNode != null;
    }

    private static object AddFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.Plus, lhs, rhs, out resultType, out retValue))
        return retValue;
      switch (resultType)
      {
        case StorageType.Char:
        case StorageType.String:
          return (object) (Convert.ToString(lhs.Value, context.FormatProvider) + Convert.ToString(rhs.Value, context.FormatProvider));
        case StorageType.SByte:
          return (object) Convert.ToSByte((object) ((int) Convert.ToSByte(lhs.Value, context.FormatProvider) + (int) Convert.ToSByte(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int16:
          return (object) Convert.ToInt16((object) ((int) Convert.ToInt16(lhs.Value, context.FormatProvider) + (int) Convert.ToInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.UInt16:
          return (object) Convert.ToUInt16((object) ((int) Convert.ToUInt16(lhs.Value, context.FormatProvider) + (int) Convert.ToUInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int32:
          return (object) (Convert.ToInt32(lhs.Value, context.FormatProvider) + Convert.ToInt32(rhs.Value, context.FormatProvider));
        case StorageType.UInt32:
          return (object) (uint) ((int) Convert.ToUInt32(lhs.Value, context.FormatProvider) + (int) Convert.ToUInt32(rhs.Value, context.FormatProvider));
        case StorageType.Int64:
          return (object) (Convert.ToInt64(lhs.Value, context.FormatProvider) + Convert.ToInt64(rhs.Value, context.FormatProvider));
        case StorageType.UInt64:
          return (object) (ulong) ((long) Convert.ToUInt64(lhs.Value, context.FormatProvider) + (long) Convert.ToUInt64(rhs.Value, context.FormatProvider));
        case StorageType.Single:
          return (object) (float) ((double) Convert.ToSingle(lhs.Value, context.FormatProvider) + (double) Convert.ToSingle(rhs.Value, context.FormatProvider));
        case StorageType.Double:
          return (object) (Convert.ToDouble(lhs.Value, context.FormatProvider) + Convert.ToDouble(rhs.Value, context.FormatProvider));
        case StorageType.Decimal:
          return (object) (Convert.ToDecimal(lhs.Value, context.FormatProvider) + Convert.ToDecimal(rhs.Value, context.FormatProvider));
        case StorageType.DateTime:
          if (lhs.Value is TimeSpan && rhs.Value is DateTime)
            return (object) ((DateTime) rhs.Value + (TimeSpan) lhs.Value);
          if (lhs.Value is DateTime && rhs.Value is TimeSpan)
            return (object) ((DateTime) lhs.Value + (TimeSpan) rhs.Value);
          break;
        case StorageType.TimeSpan:
          return (object) ((TimeSpan) lhs.Value + (TimeSpan) rhs.Value);
        case StorageType.SqlByte:
          return (object) (SqlConvert.ConvertToSqlByte(lhs.Value) + SqlConvert.ConvertToSqlByte(rhs.Value));
        case StorageType.SqlDateTime:
          if (lhs.Value is TimeSpan && rhs.Value is SqlDateTime)
            return (object) SqlConvert.ConvertToSqlDateTime((object) (SqlConvert.ConvertToSqlDateTime(rhs.Value).Value + (TimeSpan) lhs.Value));
          if (lhs.Value is SqlDateTime && rhs.Value is TimeSpan)
            return (object) SqlConvert.ConvertToSqlDateTime((object) (SqlConvert.ConvertToSqlDateTime(lhs.Value).Value + (TimeSpan) rhs.Value));
          break;
        case StorageType.SqlDecimal:
          return (object) (SqlConvert.ConvertToSqlDecimal(lhs.Value) + SqlConvert.ConvertToSqlDecimal(rhs.Value));
        case StorageType.SqlDouble:
          return (object) (SqlConvert.ConvertToSqlDouble(lhs.Value) + SqlConvert.ConvertToSqlDouble(rhs.Value));
        case StorageType.SqlInt16:
          return (object) (SqlConvert.ConvertToSqlInt16(lhs.Value) + SqlConvert.ConvertToSqlInt16(rhs.Value));
        case StorageType.SqlInt32:
          return (object) (SqlConvert.ConvertToSqlInt32(lhs.Value) + SqlConvert.ConvertToSqlInt32(rhs.Value));
        case StorageType.SqlInt64:
          return (object) (SqlConvert.ConvertToSqlInt64(lhs.Value) + SqlConvert.ConvertToSqlInt64(rhs.Value));
        case StorageType.SqlMoney:
          return (object) (SqlConvert.ConvertToSqlMoney(lhs.Value) + SqlConvert.ConvertToSqlMoney(rhs.Value));
        case StorageType.SqlSingle:
          return (object) (SqlConvert.ConvertToSqlSingle(lhs.Value) + SqlConvert.ConvertToSqlSingle(rhs.Value));
        case StorageType.SqlString:
          return (object) (SqlConvert.ConvertToSqlString(lhs.Value) + SqlConvert.ConvertToSqlString(rhs.Value));
      }
      throw InvalidExpressionException.TypeMismatch(Operator.Plus.ToString());
    }

    private static object SubtractFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.Minus, lhs, rhs, out resultType, out retValue))
        return retValue;
      switch (resultType)
      {
        case StorageType.SByte:
          return (object) Convert.ToSByte((object) ((int) Convert.ToSByte(lhs.Value, context.FormatProvider) - (int) Convert.ToSByte(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Byte:
          return (object) Convert.ToByte((object) ((int) Convert.ToByte(lhs.Value, context.FormatProvider) - (int) Convert.ToByte(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int16:
          return (object) Convert.ToInt16((object) ((int) Convert.ToInt16(lhs.Value, context.FormatProvider) - (int) Convert.ToInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.UInt16:
          return (object) Convert.ToUInt16((object) ((int) Convert.ToUInt16(lhs.Value, context.FormatProvider) - (int) Convert.ToUInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int32:
          return (object) (Convert.ToInt32(lhs.Value, context.FormatProvider) - Convert.ToInt32(rhs.Value, context.FormatProvider));
        case StorageType.UInt32:
          return (object) (uint) ((int) Convert.ToUInt32(lhs.Value, context.FormatProvider) - (int) Convert.ToUInt32(rhs.Value, context.FormatProvider));
        case StorageType.Int64:
          return (object) (Convert.ToInt64(lhs.Value, context.FormatProvider) - Convert.ToInt64(rhs.Value, context.FormatProvider));
        case StorageType.UInt64:
          return (object) (ulong) ((long) Convert.ToUInt64(lhs.Value, context.FormatProvider) - (long) Convert.ToUInt64(rhs.Value, context.FormatProvider));
        case StorageType.Single:
          return (object) (float) ((double) Convert.ToSingle(lhs.Value, context.FormatProvider) - (double) Convert.ToSingle(rhs.Value, context.FormatProvider));
        case StorageType.Double:
          return (object) (Convert.ToDouble(lhs.Value, context.FormatProvider) - Convert.ToDouble(rhs.Value, context.FormatProvider));
        case StorageType.Decimal:
          return (object) (Convert.ToDecimal(lhs.Value, context.FormatProvider) - Convert.ToDecimal(rhs.Value, context.FormatProvider));
        case StorageType.DateTime:
          return (object) ((DateTime) lhs.Value - (TimeSpan) rhs.Value);
        case StorageType.TimeSpan:
          if (lhs.Value is DateTime)
            return (object) ((DateTime) lhs.Value - (DateTime) rhs.Value);
          return (object) ((TimeSpan) lhs.Value - (TimeSpan) rhs.Value);
        case StorageType.SqlByte:
          return (object) (SqlConvert.ConvertToSqlByte(lhs.Value) - SqlConvert.ConvertToSqlByte(rhs.Value));
        case StorageType.SqlDateTime:
          if (lhs.Value is TimeSpan && rhs.Value is SqlDateTime)
            return (object) SqlConvert.ConvertToSqlDateTime((object) (SqlConvert.ConvertToSqlDateTime(rhs.Value).Value - (TimeSpan) lhs.Value));
          if (lhs.Value is SqlDateTime && rhs.Value is TimeSpan)
            return (object) SqlConvert.ConvertToSqlDateTime((object) (SqlConvert.ConvertToSqlDateTime(lhs.Value).Value - (TimeSpan) rhs.Value));
          break;
        case StorageType.SqlDecimal:
          return (object) (SqlConvert.ConvertToSqlDecimal(lhs.Value) - SqlConvert.ConvertToSqlDecimal(rhs.Value));
        case StorageType.SqlDouble:
          return (object) (SqlConvert.ConvertToSqlDouble(lhs.Value) - SqlConvert.ConvertToSqlDouble(rhs.Value));
        case StorageType.SqlInt16:
          return (object) (SqlConvert.ConvertToSqlInt16(lhs.Value) - SqlConvert.ConvertToSqlInt16(rhs.Value));
        case StorageType.SqlInt32:
          return (object) (SqlConvert.ConvertToSqlInt32(lhs.Value) - SqlConvert.ConvertToSqlInt32(rhs.Value));
        case StorageType.SqlInt64:
          return (object) (SqlConvert.ConvertToSqlInt64(lhs.Value) - SqlConvert.ConvertToSqlInt64(rhs.Value));
        case StorageType.SqlMoney:
          return (object) (SqlConvert.ConvertToSqlMoney(lhs.Value) - SqlConvert.ConvertToSqlMoney(rhs.Value));
        case StorageType.SqlSingle:
          return (object) (SqlConvert.ConvertToSqlSingle(lhs.Value) - SqlConvert.ConvertToSqlSingle(rhs.Value));
      }
      throw InvalidExpressionException.TypeMismatch(Operator.Minus.ToString());
    }

    private static object MultiplyFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.Multiply, lhs, rhs, out resultType, out retValue))
        return retValue;
      switch (resultType)
      {
        case StorageType.SByte:
          return (object) Convert.ToSByte((object) ((int) Convert.ToSByte(lhs.Value, context.FormatProvider) * (int) Convert.ToSByte(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Byte:
          return (object) Convert.ToByte((object) ((int) Convert.ToByte(lhs.Value, context.FormatProvider) * (int) Convert.ToByte(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int16:
          return (object) Convert.ToInt16((object) ((int) Convert.ToInt16(lhs.Value, context.FormatProvider) * (int) Convert.ToInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.UInt16:
          return (object) Convert.ToUInt16((object) ((int) Convert.ToUInt16(lhs.Value, context.FormatProvider) * (int) Convert.ToUInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int32:
          return (object) (Convert.ToInt32(lhs.Value, context.FormatProvider) * Convert.ToInt32(rhs.Value, context.FormatProvider));
        case StorageType.UInt32:
          return (object) (uint) ((int) Convert.ToUInt32(lhs.Value, context.FormatProvider) * (int) Convert.ToUInt32(rhs.Value, context.FormatProvider));
        case StorageType.Int64:
          return (object) (Convert.ToInt64(lhs.Value, context.FormatProvider) * Convert.ToInt64(rhs.Value, context.FormatProvider));
        case StorageType.UInt64:
          return (object) (ulong) ((long) Convert.ToUInt64(lhs.Value, context.FormatProvider) * (long) Convert.ToUInt64(rhs.Value, context.FormatProvider));
        case StorageType.Single:
          return (object) (float) ((double) Convert.ToSingle(lhs.Value, context.FormatProvider) * (double) Convert.ToSingle(rhs.Value, context.FormatProvider));
        case StorageType.Double:
          return (object) (Convert.ToDouble(lhs.Value, context.FormatProvider) * Convert.ToDouble(rhs.Value, context.FormatProvider));
        case StorageType.Decimal:
          return (object) (Convert.ToDecimal(lhs.Value, context.FormatProvider) * Convert.ToDecimal(rhs.Value, context.FormatProvider));
        case StorageType.SqlByte:
          return (object) (SqlConvert.ConvertToSqlByte(lhs.Value) * SqlConvert.ConvertToSqlByte(rhs.Value));
        case StorageType.SqlDecimal:
          return (object) (SqlConvert.ConvertToSqlDecimal(lhs.Value) * SqlConvert.ConvertToSqlDecimal(rhs.Value));
        case StorageType.SqlDouble:
          return (object) (SqlConvert.ConvertToSqlDouble(lhs.Value) * SqlConvert.ConvertToSqlDouble(rhs.Value));
        case StorageType.SqlInt16:
          return (object) (SqlConvert.ConvertToSqlInt16(lhs.Value) * SqlConvert.ConvertToSqlInt16(rhs.Value));
        case StorageType.SqlInt32:
          return (object) (SqlConvert.ConvertToSqlInt32(lhs.Value) * SqlConvert.ConvertToSqlInt32(rhs.Value));
        case StorageType.SqlInt64:
          return (object) (SqlConvert.ConvertToSqlInt64(lhs.Value) * SqlConvert.ConvertToSqlInt64(rhs.Value));
        case StorageType.SqlMoney:
          return (object) (SqlConvert.ConvertToSqlMoney(lhs.Value) * SqlConvert.ConvertToSqlMoney(rhs.Value));
        case StorageType.SqlSingle:
          return (object) (SqlConvert.ConvertToSqlSingle(lhs.Value) * SqlConvert.ConvertToSqlSingle(rhs.Value));
        default:
          throw InvalidExpressionException.TypeMismatch(Operator.Multiply.ToString());
      }
    }

    private static bool IsZero(object value)
    {
      if (value is short || value is int || (value is long || value is sbyte) || (value is byte || value is char))
        return Convert.ToInt64(value) == 0L;
      if (value is float || value is double || value is Decimal)
        return Convert.ToDecimal(value) == new Decimal(0);
      return false;
    }

    private static bool IsPositive(object value)
    {
      if (value is short || value is int || (value is long || value is sbyte) || (value is byte || value is char || (value is float || value is double)) || value is Decimal)
        return Convert.ToDecimal(value) > new Decimal(0);
      return true;
    }

    private static object DivideFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.Divide, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (Operator.IsZero(rhs.Value))
      {
        if (Operator.IsZero(lhs.Value))
          return (object) float.NaN;
        return (object) (float) (Operator.IsPositive(lhs.Value) ? double.PositiveInfinity : double.NegativeInfinity);
      }
      switch (resultType)
      {
        case StorageType.SByte:
          return (object) Convert.ToSByte((object) ((int) Convert.ToSByte(lhs.Value, context.FormatProvider) / (int) Convert.ToSByte(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Byte:
          return (object) Convert.ToByte((object) ((int) Convert.ToByte(lhs.Value, context.FormatProvider) / (int) Convert.ToByte(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int16:
          return (object) Convert.ToInt16((object) ((int) Convert.ToInt16(lhs.Value, context.FormatProvider) / (int) Convert.ToInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.UInt16:
          return (object) Convert.ToUInt16((object) ((int) Convert.ToUInt16(lhs.Value, context.FormatProvider) / (int) Convert.ToUInt16(rhs.Value, context.FormatProvider)), context.FormatProvider);
        case StorageType.Int32:
          return (object) (Convert.ToInt32(lhs.Value, context.FormatProvider) / Convert.ToInt32(rhs.Value, context.FormatProvider));
        case StorageType.UInt32:
          return (object) (Convert.ToUInt32(lhs.Value, context.FormatProvider) / Convert.ToUInt32(rhs.Value, context.FormatProvider));
        case StorageType.Int64:
          return (object) (Convert.ToInt64(lhs.Value, context.FormatProvider) / Convert.ToInt64(rhs.Value, context.FormatProvider));
        case StorageType.UInt64:
          return (object) (Convert.ToUInt64(lhs.Value, context.FormatProvider) / Convert.ToUInt64(rhs.Value, context.FormatProvider));
        case StorageType.Single:
          return (object) (float) ((double) Convert.ToSingle(lhs.Value, context.FormatProvider) / (double) Convert.ToSingle(rhs.Value, context.FormatProvider));
        case StorageType.Double:
          double num = Convert.ToDouble(rhs.Value, context.FormatProvider);
          return (object) (Convert.ToDouble(lhs.Value, context.FormatProvider) / num);
        case StorageType.Decimal:
          return (object) (Convert.ToDecimal(lhs.Value, context.FormatProvider) / Convert.ToDecimal(rhs.Value, context.FormatProvider));
        case StorageType.SqlByte:
          return (object) (SqlConvert.ConvertToSqlByte(lhs.Value) / SqlConvert.ConvertToSqlByte(rhs.Value));
        case StorageType.SqlDecimal:
          return (object) (SqlConvert.ConvertToSqlDecimal(lhs.Value) / SqlConvert.ConvertToSqlDecimal(rhs.Value));
        case StorageType.SqlDouble:
          return (object) (SqlConvert.ConvertToSqlDouble(lhs.Value) / SqlConvert.ConvertToSqlDouble(rhs.Value));
        case StorageType.SqlInt16:
          return (object) (SqlConvert.ConvertToSqlInt16(lhs.Value) / SqlConvert.ConvertToSqlInt16(rhs.Value));
        case StorageType.SqlInt32:
          return (object) (SqlConvert.ConvertToSqlInt32(lhs.Value) / SqlConvert.ConvertToSqlInt32(rhs.Value));
        case StorageType.SqlInt64:
          return (object) (SqlConvert.ConvertToSqlInt64(lhs.Value) / SqlConvert.ConvertToSqlInt64(rhs.Value));
        case StorageType.SqlMoney:
          return (object) (SqlConvert.ConvertToSqlMoney(lhs.Value) / SqlConvert.ConvertToSqlMoney(rhs.Value));
        case StorageType.SqlSingle:
          return (object) (SqlConvert.ConvertToSqlSingle(lhs.Value) / SqlConvert.ConvertToSqlSingle(rhs.Value));
        default:
          throw InvalidExpressionException.TypeMismatch(Operator.Divide.ToString());
      }
    }

    private static object ModuloFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.Modulo, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (!DataStorageHelper.IsIntegerSql(resultType))
        throw InvalidExpressionException.TypeMismatch(Operator.Plus.ToString());
      if (resultType == StorageType.UInt64)
        return (object) (Convert.ToUInt64(lhs.Value, context.FormatProvider) % Convert.ToUInt64(rhs.Value, context.FormatProvider));
      if (!DataStorageHelper.IsSqlType(resultType))
        return Convert.ChangeType((object) (Convert.ToInt64(lhs.Value, context.FormatProvider) % Convert.ToInt64(rhs.Value, context.FormatProvider)), DataStorageHelper.GetTypeStorage(resultType), context.FormatProvider);
      SqlInt64 sqlInt64 = SqlConvert.ConvertToSqlInt64(lhs.Value) % SqlConvert.ConvertToSqlInt64(rhs.Value);
      switch (resultType)
      {
        case StorageType.SqlByte:
          return (object) sqlInt64.ToSqlByte();
        case StorageType.SqlInt16:
          return (object) sqlInt64.ToSqlInt16();
        case StorageType.SqlInt32:
          return (object) sqlInt64.ToSqlInt32();
        default:
          return (object) sqlInt64;
      }
    }

    private static object AndFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      if (DataStorageHelper.IsObjectNull(lhs.Value) || DataStorageHelper.IsSqlType(lhs.Value) && DataStorageHelper.IsObjectSqlNull(lhs.Value))
        return (object) DBNull.Value;
      if (!(lhs.Value is bool) && !(lhs.Value is SqlBoolean))
        throw InvalidExpressionException.TypeMismatch(Operator.And.ToString());
      if (lhs.Value is bool)
      {
        if (DataStorageHelper.IsObjectNull(rhs.Value) || DataStorageHelper.IsSqlType(rhs.Value) && DataStorageHelper.IsObjectSqlNull(rhs.Value))
          return (object) DBNull.Value;
        if (!(bool) lhs.Value)
          return (object) false;
        if (!(rhs.Value is bool) && !(rhs.Value is SqlBoolean))
          throw InvalidExpressionException.TypeMismatch(Operator.And.ToString());
        if (rhs.Value is bool)
          return (object) (bool) rhs.Value;
        return (object) ((SqlBoolean) rhs.Value).IsTrue;
      }
      if (((SqlBoolean) lhs.Value).IsFalse)
        return (object) false;
      return (object) true;
    }

    private static object OrFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      if (DataStorageHelper.IsObjectNull(lhs.Value))
      {
        if (DataStorageHelper.IsObjectNull(rhs.Value))
          return (object) DBNull.Value;
        if (!(rhs.Value is bool) && !(rhs.Value is SqlBoolean))
          throw InvalidExpressionException.TypeMismatch(Operator.Or.ToString());
        return (object) (bool) (rhs.Value is bool ? ((bool) rhs.Value ? 1 : 0) : (((SqlBoolean) rhs.Value).IsTrue ? 1 : 0));
      }
      if (!(lhs.Value is bool) && !(lhs.Value is SqlBoolean))
        throw InvalidExpressionException.TypeMismatch(Operator.Or.ToString());
      if (DataStorageHelper.IsObjectNull(rhs.Value))
        return lhs.Value;
      if (!(rhs.Value is bool) && !(rhs.Value is SqlBoolean))
        throw InvalidExpressionException.TypeMismatch(Operator.Or.ToString());
      if ((bool) lhs.Value)
        return (object) true;
      return (object) (bool) rhs.Value;
    }

    private static object NegativeFunc(Operand rhs, OperatorContext context)
    {
      object obj = rhs.Value;
      if (DataStorageHelper.IsUnknown(obj))
        return (object) DBNull.Value;
      StorageType storageType = DataStorageHelper.GetStorageType(obj.GetType());
      if (!DataStorageHelper.IsNumericSql(storageType))
        throw InvalidExpressionException.TypeMismatch(Operator.Not.ToString());
      switch (storageType)
      {
        case StorageType.Byte:
          return (object) (int) -(byte) obj;
        case StorageType.Int16:
          return (object) (int) -(short) obj;
        case StorageType.Int32:
          return (object) -(int) obj;
        case StorageType.Int64:
          return (object) -(long) obj;
        case StorageType.Single:
          return (object) (float) -(double) (float) obj;
        case StorageType.Double:
          return (object) -(double) obj;
        case StorageType.Decimal:
          return (object) -(Decimal) obj;
        case StorageType.SqlDecimal:
          return (object) -(SqlDecimal) obj;
        case StorageType.SqlDouble:
          return (object) -(SqlDouble) obj;
        case StorageType.SqlInt16:
          return (object) -(SqlInt16) obj;
        case StorageType.SqlInt32:
          return (object) -(SqlInt32) obj;
        case StorageType.SqlInt64:
          return (object) -(SqlInt64) obj;
        case StorageType.SqlMoney:
          return (object) -(SqlMoney) obj;
        case StorageType.SqlSingle:
          return (object) -(SqlSingle) obj;
        default:
          return (object) DBNull.Value;
      }
    }

    private static object PositiveFunc(Operand rhs, OperatorContext context)
    {
      if (DataStorageHelper.IsUnknown(rhs.Value))
        return (object) DBNull.Value;
      if (!DataStorageHelper.IsNumericSql(DataStorageHelper.GetStorageType(rhs.Value.GetType())))
        throw InvalidExpressionException.TypeMismatch(Operator.UnaryPlus.ToString());
      return rhs.Value;
    }

    private static object NotFunc(Operand rhs, OperatorContext context)
    {
      object obj = rhs.Value;
      if (DataStorageHelper.IsUnknown(obj))
        return (object) DBNull.Value;
      if (!(obj is SqlBoolean))
      {
        if (DataStorageHelper.ToBoolean(obj))
          return (object) false;
        return (object) true;
      }
      if (((SqlBoolean) obj).IsFalse)
        return (object) SqlBoolean.True;
      if (!((SqlBoolean) obj).IsTrue)
        throw InvalidExpressionException.UnsupportedOperator(Operator.Not);
      return (object) SqlBoolean.False;
    }

    private static object InFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      if (DataStorageHelper.IsObjectNull(lhs.Value))
        return (object) false;
      IEnumerable enumerable = (IEnumerable) null;
      if (rhs.Node is FunctionNode && ((FunctionNode) rhs.Node).IsGlobal && ((FunctionNode) rhs.Node).Name == "In")
      {
        FunctionNode node = (FunctionNode) rhs.Node;
        if (node.Arguments != null && node.Arguments.Count > 0)
        {
          ArrayList arrayList = new ArrayList(node.Arguments.Count);
          for (int index = 0; index < node.Arguments.Count; ++index)
          {
            object obj = node.Arguments[index].Eval(context.Row, context.ExpressionContext);
            arrayList.Add(obj);
          }
          enumerable = (IEnumerable) arrayList;
        }
      }
      else if (rhs.Value is IEnumerable)
        enumerable = (IEnumerable) rhs.Value;
      else
        enumerable = (IEnumerable) new object[1]
        {
          rhs.Value
        };
      if (enumerable != null)
      {
        foreach (object obj in enumerable)
        {
          if (obj != DBNull.Value && (!DataStorageHelper.IsSqlType(obj) || !DataStorageHelper.IsObjectSqlNull(obj)))
          {
            StorageType storageType = DataStorageHelper.GetStorageType(lhs.Value.GetType());
            if (Operator.BinaryCompare(lhs.Value, obj, storageType, Operator.EqualTo, context) == 0)
              return (object) true;
          }
        }
      }
      return (object) false;
    }

    private static object EqualToFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.EqualTo, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (lhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(lhs.Value) || !DataStorageHelper.IsObjectSqlNull(lhs.Value)) && (rhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(rhs.Value) || !DataStorageHelper.IsObjectSqlNull(rhs.Value))))
        return (object) (0 == Operator.BinaryCompare(lhs.Value, rhs.Value, resultType, Operator.EqualTo, context));
      return (object) DBNull.Value;
    }

    private static object GreaterThenFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.GreaterThen, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (lhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(lhs.Value) || !DataStorageHelper.IsObjectSqlNull(lhs.Value)) && (rhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(rhs.Value) || !DataStorageHelper.IsObjectSqlNull(rhs.Value))))
        return (object) (0 < Operator.BinaryCompare(lhs.Value, rhs.Value, resultType, Operator.GreaterThen, context));
      return (object) DBNull.Value;
    }

    private static object GreaterOrEqualFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.GreaterOrEqual, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (lhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(lhs.Value) || !DataStorageHelper.IsObjectSqlNull(lhs.Value)) && (rhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(rhs.Value) || !DataStorageHelper.IsObjectSqlNull(rhs.Value))))
        return (object) (0 <= Operator.BinaryCompare(lhs.Value, rhs.Value, resultType, Operator.GreaterOrEqual, context));
      return (object) DBNull.Value;
    }

    private static object LessThenFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.LessThen, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (lhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(lhs.Value) || !DataStorageHelper.IsObjectSqlNull(lhs.Value)) && (rhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(rhs.Value) || !DataStorageHelper.IsObjectSqlNull(rhs.Value))))
        return (object) (0 > Operator.BinaryCompare(lhs.Value, rhs.Value, resultType, Operator.LessThen, context));
      return (object) DBNull.Value;
    }

    private static object LessOrEqualFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.LessOrEqual, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (lhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(lhs.Value) || !DataStorageHelper.IsObjectSqlNull(lhs.Value)) && (rhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(rhs.Value) || !DataStorageHelper.IsObjectSqlNull(rhs.Value))))
        return (object) (0 >= Operator.BinaryCompare(lhs.Value, rhs.Value, resultType, Operator.LessOrEqual, context));
      return (object) DBNull.Value;
    }

    private static object NotEqualFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      StorageType resultType;
      object retValue;
      if (!Operator.GetResultType(Operator.NotEqual, lhs, rhs, out resultType, out retValue))
        return retValue;
      if (lhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(lhs.Value) || !DataStorageHelper.IsObjectSqlNull(lhs.Value)) && (rhs.Value != DBNull.Value && (!DataStorageHelper.IsSqlType(rhs.Value) || !DataStorageHelper.IsObjectSqlNull(rhs.Value))))
        return (object) (0 != Operator.BinaryCompare(lhs.Value, rhs.Value, resultType, Operator.NotEqual, context));
      return (object) !object.Equals(lhs.Value, rhs.Value);
    }

    private static object IsFunc(Operand lhs, Operand rhs, OperatorContext context)
    {
      if (!DataStorageHelper.IsObjectNull(lhs.Value) && (!DataStorageHelper.IsSqlType(lhs.Value) || !DataStorageHelper.IsObjectSqlNull(lhs.Value)))
        return (object) false;
      return (object) true;
    }

    private static object IsNotFunc(Operand lhs, OperatorContext context)
    {
      if (DataStorageHelper.IsObjectNull(lhs.Value) || DataStorageHelper.IsSqlType(lhs.Value) && DataStorageHelper.IsObjectSqlNull(lhs.Value))
        return (object) false;
      return (object) true;
    }

    private static object BetweenFunc(
      Operand op1,
      Operand op2,
      Operand op3,
      OperatorContext context)
    {
      object obj1 = op1.Value;
      object obj2 = op2.Value;
      object obj3 = op3.Value;
      StorageType storageType = DataStorageHelper.GetStorageType(obj1.GetType());
      int num1 = Operator.BinaryCompare(obj2, obj1, storageType, Operator.LessOrEqual, context);
      int num2 = Operator.BinaryCompare(obj1, obj3, storageType, Operator.LessOrEqual, context);
      return (object) (bool) (num1 > 0 ? 0 : (num2 <= 0 ? 1 : 0));
    }

    internal static int BinaryCompare(
      object value1,
      object value2,
      StorageType resultType,
      Operator op,
      OperatorContext context)
    {
      try
      {
        if (!DataStorageHelper.IsSqlType(resultType))
        {
          switch (resultType)
          {
            case StorageType.Boolean:
              if (op != Operator.EqualTo)
              {
                if (op != Operator.NotEqual)
                  break;
              }
              return Convert.ToInt32((object) DataStorageHelper.ToBoolean(value1), context.FormatProvider) - Convert.ToInt32((object) DataStorageHelper.ToBoolean(value2), context.FormatProvider);
            case StorageType.Char:
              return Convert.ToInt32(value1, context.FormatProvider).CompareTo(Convert.ToInt32(value2, context.FormatProvider));
            case StorageType.SByte:
            case StorageType.Byte:
            case StorageType.Int16:
            case StorageType.UInt16:
            case StorageType.Int32:
              return Convert.ToInt32(value1, context.FormatProvider).CompareTo(Convert.ToInt32(value2, context.FormatProvider));
            case StorageType.UInt32:
            case StorageType.Int64:
            case StorageType.UInt64:
            case StorageType.Decimal:
              return Decimal.Compare(Convert.ToDecimal(value1, context.FormatProvider), Convert.ToDecimal(value2, context.FormatProvider));
            case StorageType.Single:
              return Convert.ToSingle(value1, context.FormatProvider).CompareTo(Convert.ToSingle(value2, context.FormatProvider));
            case StorageType.Double:
              return Convert.ToDouble(value1, context.FormatProvider).CompareTo(Convert.ToDouble(value2, context.FormatProvider));
            case StorageType.DateTime:
              return DateTime.Compare(Convert.ToDateTime(value1, context.FormatProvider), Convert.ToDateTime(value2, context.FormatProvider));
            case StorageType.TimeSpan:
              return TimeSpan.Compare(TimeSpan.Parse(value1.ToString().Replace("'", "")), TimeSpan.Parse(value2.ToString().Replace("'", "")));
            case StorageType.String:
              return string.Compare(Convert.ToString(value1, context.FormatProvider), Convert.ToString(value2, context.FormatProvider), (context.CompareOptions & CompareOptions.IgnoreCase) == CompareOptions.IgnoreCase);
            case StorageType.Guid:
              return ((Guid) value1).CompareTo((Guid) value2);
          }
        }
        else
        {
          switch (resultType)
          {
            case StorageType.SByte:
            case StorageType.Byte:
            case StorageType.Int16:
            case StorageType.UInt16:
            case StorageType.Int32:
            case StorageType.SqlByte:
            case StorageType.SqlInt16:
            case StorageType.SqlInt32:
              return SqlConvert.ConvertToSqlInt32(value1).CompareTo(SqlConvert.ConvertToSqlInt32(value2));
            case StorageType.UInt32:
            case StorageType.Int64:
            case StorageType.SqlInt64:
              return SqlConvert.ConvertToSqlInt64(value1).CompareTo(SqlConvert.ConvertToSqlInt64(value2));
            case StorageType.UInt64:
            case StorageType.SqlDecimal:
              return SqlConvert.ConvertToSqlDecimal(value1).CompareTo(SqlConvert.ConvertToSqlDecimal(value2));
            case StorageType.SqlBinary:
              return SqlConvert.ConvertToSqlBinary(value1).CompareTo(SqlConvert.ConvertToSqlBinary(value2));
            case StorageType.SqlBoolean:
              if (op != Operator.EqualTo)
              {
                if (op != Operator.NotEqual)
                  break;
              }
              if ((object) value1.GetType() != (object) typeof (SqlBoolean) || (object) value2.GetType() != (object) typeof (SqlBoolean) && (object) value2.GetType() != (object) typeof (bool))
              {
                if ((object) value2.GetType() == (object) typeof (SqlBoolean))
                {
                  if ((object) value1.GetType() != (object) typeof (SqlBoolean))
                  {
                    if ((object) value1.GetType() != (object) typeof (bool))
                      break;
                  }
                }
                else
                  break;
              }
              return SqlConvert.ConvertToSqlBoolean(value1).CompareTo(SqlConvert.ConvertToSqlBoolean(value2));
            case StorageType.SqlDateTime:
              return SqlConvert.ConvertToSqlDateTime(value1).CompareTo(SqlConvert.ConvertToSqlDateTime(value2));
            case StorageType.SqlDouble:
              return SqlConvert.ConvertToSqlDouble(value1).CompareTo(SqlConvert.ConvertToSqlDouble(value2));
            case StorageType.SqlGuid:
              return ((SqlGuid) value1).CompareTo(value2);
            case StorageType.SqlMoney:
              return SqlConvert.ConvertToSqlMoney(value1).CompareTo(SqlConvert.ConvertToSqlMoney(value2));
            case StorageType.SqlSingle:
              return SqlConvert.ConvertToSqlSingle(value1).CompareTo(SqlConvert.ConvertToSqlSingle(value2));
            case StorageType.SqlString:
              return string.Compare(value1.ToString(), value2.ToString());
          }
        }
      }
      catch (Exception ex)
      {
      }
      throw InvalidExpressionException.TypeMismatchInBinop(op, value1.GetType(), value2.GetType());
    }

    internal static bool GetResultType(
      Operator op,
      Operand lhs,
      Operand rhs,
      out StorageType resultType,
      out object retValue)
    {
      object obj1 = lhs.Value;
      object obj2 = rhs.Value;
      retValue = (object) null;
      resultType = StorageType.Empty;
      Type type1 = (Type) null;
      Type type2 = (Type) null;
      if (obj1 != null)
        type1 = obj1.GetType();
      if (obj2 != null)
        type2 = obj2.GetType();
      StorageType storageType1 = DataStorageHelper.GetStorageType(type1);
      StorageType storageType2 = DataStorageHelper.GetStorageType(type2);
      bool flag1 = DataStorageHelper.IsSqlType(storageType1);
      bool flag2 = DataStorageHelper.IsSqlType(storageType2);
      if (flag1 && DataStorageHelper.IsObjectSqlNull(obj1))
      {
        retValue = obj1;
        return false;
      }
      if (flag2 && DataStorageHelper.IsObjectSqlNull(obj2))
      {
        retValue = obj2;
        return false;
      }
      if (DataStorageHelper.IsObjectNull(obj1) || DataStorageHelper.IsObjectNull(obj2))
      {
        retValue = (object) DBNull.Value;
        return false;
      }
      resultType = flag1 || flag2 ? DataStorageHelper.ResultSqlType(storageType1, storageType2, lhs.IsConst, rhs.IsConst, op) : DataStorageHelper.ResultType(storageType1, storageType2, lhs.IsConst, rhs.IsConst, op);
      if (resultType == StorageType.Empty)
        throw InvalidExpressionException.TypeMismatchInBinop(op, type1, type2);
      return true;
    }

    public delegate object ZeroFunc();

    public delegate object UnaryFunc(Operand operand, OperatorContext context);

    public delegate object BinaryFunc(Operand lhs, Operand rhs, OperatorContext context);

    public delegate object TernaryFunc(
      Operand op1,
      Operand op2,
      Operand op3,
      OperatorContext context);

    private class ParsedSetCondition
    {
      private Dictionary<string, List<ConstNode>> values = new Dictionary<string, List<ConstNode>>();
      public ExpressionNode ExpressionNode;
      public bool IsContainsOperator;
      public bool ValuesContainNull;

      public void AddConstNode(ConstNode node, object row, object context)
      {
        string key = Convert.ToString(node.Eval(row, context));
        if (!this.values.ContainsKey(key))
          this.values.Add(key, new List<ConstNode>());
        this.values[key].Add(node);
      }

      public object PassesFilter(NameNode nameNode, object row, object context)
      {
        object obj1 = nameNode.Eval(row, context);
        if (obj1 != null && obj1.GetType().IsEnum)
          obj1 = (object) Convert.ToInt32(obj1);
        if (obj1 == null || obj1 == DBNull.Value)
        {
          if (this.ValuesContainNull)
            return (object) this.IsContainsOperator;
          return (object) !this.IsContainsOperator;
        }
        string key = !(obj1 is double) ? Convert.ToString(obj1) : ((double) obj1).ToString("G17", (IFormatProvider) CultureInfo.InvariantCulture);
        if (!this.values.ContainsKey(key))
          return (object) !this.IsContainsOperator;
        foreach (ConstNode constNode in this.values[key])
        {
          object obj2 = new BinaryOpNode(Operator.EqualTo, (ExpressionNode) nameNode, (ExpressionNode) constNode).Eval(row, context);
          if (obj2 is bool && (bool) obj2)
            return (object) this.IsContainsOperator;
        }
        return (object) !this.IsContainsOperator;
      }
    }
  }
}
