// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ExpressionParser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  internal class ExpressionParser : ILexerClient, IDisposable
  {
    private static readonly Dictionary<int, ExpressionNode> expressionHive = new Dictionary<int, ExpressionNode>();
    private Stack<ExpressionNode> nodeStack = new Stack<ExpressionNode>(100);
    private Stack<ExpressionParser.OperatorInfo> opStack = new Stack<ExpressionParser.OperatorInfo>(100);
    private Lexer lexer;
    private OperandType prevOperand;
    private CompareOptions compareFlags;

    public static bool TryParse(
      string expression,
      bool caseSensitiveLike,
      out ExpressionNode expressionNode)
    {
      expressionNode = (ExpressionNode) null;
      string text = (expression ?? "").Trim();
      if (text.StartsWith("="))
        text = text.Substring(1);
      int key = text.GetHashCode() ^ caseSensitiveLike.GetHashCode();
      if (ExpressionParser.expressionHive.ContainsKey(key))
      {
        expressionNode = ExpressionParser.expressionHive[key];
        return true;
      }
      if (string.IsNullOrEmpty(text))
        return false;
      using (ExpressionParser expressionParser = new ExpressionParser(text, caseSensitiveLike))
      {
        if (!expressionParser.TryParse(false, out expressionNode))
          return false;
      }
      ExpressionParser.expressionHive[key] = expressionNode;
      return true;
    }

    public static ExpressionNode Parse(string expression, bool caseSensitiveLike)
    {
      string text = (expression ?? "").Trim();
      if (text.StartsWith("="))
        text = text.Substring(1);
      int key = text.GetHashCode() ^ caseSensitiveLike.GetHashCode();
      if (ExpressionParser.expressionHive.ContainsKey(key))
        return ExpressionParser.expressionHive[key];
      if (!(string.Empty != text))
        return (ExpressionNode) null;
      ExpressionNode expressionNode = (ExpressionNode) null;
      using (ExpressionParser expressionParser = new ExpressionParser(text, caseSensitiveLike))
        expressionNode = expressionParser.Parse();
      ExpressionParser.expressionHive[key] = expressionNode;
      return expressionNode;
    }

    public static ExpressionNode ParseNoCache(
      string expression,
      bool caseSensitiveLike)
    {
      string text = (expression ?? "").Trim();
      if (text.StartsWith("="))
        text = text.Substring(1);
      if (!(string.Empty != text))
        return (ExpressionNode) null;
      using (ExpressionParser expressionParser = new ExpressionParser(text, caseSensitiveLike))
        return expressionParser.Parse();
    }

    OperandType ILexerClient.PrevOperand
    {
      get
      {
        return this.prevOperand;
      }
    }

    private ExpressionParser(string text, bool caseSensitiveLike)
    {
      this.compareFlags = !caseSensitiveLike ? CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth : CompareOptions.None;
      this.lexer = new Lexer((ILexerClient) this, text);
    }

    public void Dispose()
    {
      this.opStack.Clear();
      this.nodeStack.Clear();
      this.lexer = (Lexer) null;
      this.opStack = (Stack<ExpressionParser.OperatorInfo>) null;
      this.nodeStack = (Stack<ExpressionNode>) null;
    }

    private void StartParse()
    {
      this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Noop, Operator.Noop));
      this.lexer.StartRead();
    }

    private bool TryParse(bool enableExceptions, out ExpressionNode expressionNode)
    {
      expressionNode = (ExpressionNode) null;
      int num = 0;
      this.StartParse();
      Token token;
      do
      {
        try
        {
          token = this.lexer.Read();
        }
        catch (LexicalException ex)
        {
          if (enableExceptions)
            throw ex;
          return false;
        }
        switch (token)
        {
          case Token.Name:
          case Token.Numeric:
          case Token.Decimal:
          case Token.Float:
          case Token.NumericHex:
          case Token.StringConst:
          case Token.Date:
            if (this.prevOperand != OperandType.None)
            {
              if (enableExceptions)
                throw ParserException.MissingOperator(this.lexer.TokenString);
              return false;
            }
            this.prevOperand = OperandType.Scalar;
            this.nodeStack.Push(this.CreateScalarNode(token, this.lexer.TokenString));
            break;
          case Token.ListSeparator:
            if (this.prevOperand == OperandType.None)
            {
              if (enableExceptions)
                throw ParserException.MissingOperandBefore(",");
              return false;
            }
            this.BuildExpression(3);
            if (this.opStack.Peek().Type != ExpressionParser.NodeType.Call)
            {
              if (enableExceptions)
                throw ParserException.SystaxError();
              return false;
            }
            ExpressionNode expressionNode1 = this.nodeStack.Pop();
            FunctionNode functionNode1 = (FunctionNode) this.nodeStack.Pop();
            functionNode1.AddArgument(expressionNode1);
            this.nodeStack.Push((ExpressionNode) functionNode1);
            this.prevOperand = OperandType.None;
            break;
          case Token.LeftParen:
            ++num;
            if (this.prevOperand != OperandType.None)
            {
              this.BuildExpression(22);
              this.prevOperand = OperandType.None;
              if (!(this.nodeStack.Peek() is NameNode))
              {
                if (enableExceptions)
                  throw ParserException.SystaxError();
                return false;
              }
              NameNode nameNode = (NameNode) this.nodeStack.Pop();
              this.nodeStack.Push(!AggregateNode.IsAggregare(nameNode.Name) ? (ExpressionNode) new FunctionNode(nameNode.Parent, nameNode.Name) : (ExpressionNode) new AggregateNode(nameNode.Name));
              this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Call, Operator.Proc));
              break;
            }
            ExpressionParser.OperatorInfo operatorInfo1 = this.opStack.Peek();
            if (operatorInfo1.Type != ExpressionParser.NodeType.Binop || operatorInfo1.Operator != Operator.In)
            {
              this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Paren, Operator.Proc));
              break;
            }
            this.nodeStack.Push((ExpressionNode) new FunctionNode((ExpressionNode) null, "In"));
            this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Call, Operator.Proc));
            break;
          case Token.RightParen:
            if (this.prevOperand != OperandType.None)
              this.BuildExpression(3);
            if (this.opStack.Count <= 1)
            {
              if (enableExceptions)
                throw ParserException.TooManyRightParentheses();
              return false;
            }
            ExpressionParser.OperatorInfo operatorInfo2 = this.opStack.Pop();
            if (this.prevOperand == OperandType.None && operatorInfo2.Type != ExpressionParser.NodeType.Call)
            {
              if (enableExceptions)
                throw ParserException.MissingOperand(operatorInfo2.Operator.ToString());
              return false;
            }
            if (operatorInfo2.Type == ExpressionParser.NodeType.Call)
            {
              if (this.prevOperand != OperandType.None)
              {
                ExpressionNode expressionNode2 = this.nodeStack.Pop();
                FunctionNode functionNode2 = (FunctionNode) this.nodeStack.Pop();
                functionNode2.AddArgument(expressionNode2);
                functionNode2.Check();
                this.nodeStack.Push((ExpressionNode) functionNode2);
              }
            }
            else
            {
              ExpressionNode right = this.nodeStack.Pop();
              this.nodeStack.Push((ExpressionNode) new UnaryOpNode(Operator.Noop, right));
            }
            this.prevOperand = OperandType.Expr;
            --num;
            break;
          case Token.ZeroOp:
            if (this.prevOperand != OperandType.None)
            {
              if (enableExceptions)
                throw ParserException.MissingOperator(this.lexer.TokenString);
              return false;
            }
            this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Zop, this.lexer.Operator));
            this.prevOperand = OperandType.Expr;
            break;
          case Token.UnaryOp:
            this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Unop, this.lexer.Operator));
            break;
          case Token.BinaryOp:
            if (this.prevOperand != OperandType.None)
            {
              this.prevOperand = OperandType.None;
              Operator betweenAnd = this.lexer.Operator;
              ExpressionParser.NodeType type = ExpressionParser.NodeType.Binop;
              if (betweenAnd == Operator.And)
              {
                this.BuildExpression(Operator.BetweenAnd.Priority);
                ExpressionParser.OperatorInfo operatorInfo3 = this.opStack.Peek();
                if (betweenAnd == Operator.And && operatorInfo3.Operator == Operator.Between)
                {
                  betweenAnd = Operator.BetweenAnd;
                  type = ExpressionParser.NodeType.TernaryOp2;
                }
              }
              this.BuildExpression(betweenAnd.Priority);
              this.opStack.Push(new ExpressionParser.OperatorInfo(type, betweenAnd));
              break;
            }
            if (Operator.Plus == this.lexer.Operator)
            {
              this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Unop, Operator.UnaryPlus));
              break;
            }
            if (Operator.Minus == this.lexer.Operator)
            {
              this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.Unop, Operator.Negative));
              break;
            }
            if (enableExceptions)
              throw ParserException.MissingOperandBefore(this.lexer.Operator.ToString());
            return false;
          case Token.TernaryOp:
            if (this.prevOperand != OperandType.None)
            {
              Operator op = this.lexer.Operator;
              this.prevOperand = OperandType.None;
              this.BuildExpression(op.Priority);
              this.opStack.Push(new ExpressionParser.OperatorInfo(ExpressionParser.NodeType.TernaryOp, op));
              break;
            }
            break;
          case Token.Dot:
            ExpressionNode expressionNode3 = this.nodeStack.Peek();
            if (!(expressionNode3 is NameNode))
            {
              if (!(expressionNode3 is FunctionNode))
              {
                if (!(expressionNode3 is ConstNode))
                {
                  if (enableExceptions)
                    throw ParserException.UnknownToken(this.lexer.TokenString, this.lexer.StartPos);
                  return false;
                }
              }
            }
            try
            {
              token = this.lexer.Read();
            }
            catch (LexicalException ex)
            {
              if (enableExceptions)
                throw ex;
              return false;
            }
            if (Token.Name != token)
            {
              if (enableExceptions)
                throw ParserException.UnknownToken(this.lexer.TokenString, this.lexer.StartPos);
              return false;
            }
            this.nodeStack.Push((ExpressionNode) new NameNode(this.nodeStack.Pop(), this.lexer.TokenString));
            break;
          case Token.Parameter:
            try
            {
              token = this.lexer.Read();
            }
            catch (LexicalException ex)
            {
              if (enableExceptions)
                throw ex;
              return false;
            }
            if (Token.Name != token)
            {
              if (enableExceptions)
                throw ParserException.UnknownToken(this.lexer.TokenString, this.lexer.StartPos);
              return false;
            }
            this.nodeStack.Push((ExpressionNode) new NameNode((ExpressionNode) new NameNode((ExpressionNode) null, "Parameters"), this.lexer.TokenString));
            this.prevOperand = OperandType.Scalar;
            break;
          case Token.EOF:
            if (this.prevOperand != OperandType.None)
            {
              this.BuildExpression(3);
              if (this.opStack.Count != 1)
              {
                if (enableExceptions)
                  throw ParserException.MissingRightParen();
                return false;
              }
              break;
            }
            if (this.nodeStack.Count > 0)
            {
              if (enableExceptions)
                throw ParserException.MissingOperand(this.opStack.Peek().Operator.ToString());
              return false;
            }
            break;
          default:
            if (enableExceptions)
              throw ParserException.UnknownToken(this.lexer.TokenString, this.lexer.StartPos);
            return false;
        }
      }
      while (Token.EOF != token);
      if (this.nodeStack.Count != 1)
        return false;
      expressionNode = this.nodeStack.Peek();
      return expressionNode != null;
    }

    private ExpressionNode Parse()
    {
      ExpressionNode expressionNode = (ExpressionNode) null;
      this.TryParse(true, out expressionNode);
      return expressionNode;
    }

    private void BuildExpression(int priority)
    {
      ExpressionNode expressionNode = (ExpressionNode) null;
      ExpressionParser.OperatorInfo operatorInfo1;
      while (true)
      {
        operatorInfo1 = this.opStack.Peek();
        if (operatorInfo1.Operator.Priority >= priority)
        {
          this.opStack.Pop();
          switch (operatorInfo1.Type)
          {
            case ExpressionParser.NodeType.Unop:
              expressionNode = (ExpressionNode) null;
              ExpressionNode right1 = this.nodeStack.Pop();
              if (Operator.IsUnary(operatorInfo1.Operator))
              {
                this.nodeStack.Push((ExpressionNode) new UnaryOpNode(operatorInfo1.Operator, right1));
                continue;
              }
              goto label_7;
            case ExpressionParser.NodeType.UnopSpec:
              goto label_2;
            case ExpressionParser.NodeType.Binop:
              ExpressionNode right2 = this.nodeStack.Pop();
              ExpressionNode left = this.nodeStack.Pop();
              if (Operator.IsBinary(operatorInfo1.Operator))
              {
                this.nodeStack.Push((ExpressionNode) new BinaryOpNode(operatorInfo1.Operator, left, right2, this.compareFlags));
                continue;
              }
              if (Operator.Like == operatorInfo1.Operator)
              {
                this.nodeStack.Push((ExpressionNode) new LikeNode(operatorInfo1.Operator, left, right2, this.compareFlags));
                continue;
              }
              goto label_12;
            case ExpressionParser.NodeType.BinopSpec:
              goto label_19;
            case ExpressionParser.NodeType.Zop:
              this.nodeStack.Push((ExpressionNode) new ZeroOpNode(operatorInfo1.Operator));
              continue;
            case ExpressionParser.NodeType.TernaryOp2:
              ExpressionParser.OperatorInfo operatorInfo2 = this.opStack.Pop();
              if (operatorInfo2.Type == ExpressionParser.NodeType.TernaryOp)
              {
                if (this.nodeStack.Count >= 3)
                {
                  ExpressionNode op3 = this.nodeStack.Pop();
                  ExpressionNode op2 = this.nodeStack.Pop();
                  ExpressionNode op1 = this.nodeStack.Pop();
                  this.nodeStack.Push((ExpressionNode) new TernaryOpNode(operatorInfo2.Operator, op1, op2, op3));
                  continue;
                }
                goto label_16;
              }
              else
                goto label_14;
            default:
              continue;
          }
        }
        else
          break;
      }
      return;
label_19:
      return;
label_2:
      return;
label_7:
      throw ParserException.UnsupportedOperator(operatorInfo1.Operator.ToString());
label_12:
      throw ParserException.UnsupportedOperator(operatorInfo1.Operator.ToString());
label_14:
      throw ParserException.MissingOperator("Ternary operator requires 2 operators");
label_16:
      throw ParserException.MissingOperand("Ternary operator requires 3 operands");
    }

    private ExpressionNode CreateScalarNode(Token token, string str)
    {
      switch (token)
      {
        case Token.Name:
          return (ExpressionNode) new NameNode((ExpressionNode) null, str);
        case Token.Numeric:
          return (ExpressionNode) new ConstNode(ValueType.Numeric, str);
        case Token.Decimal:
          return (ExpressionNode) new ConstNode(ValueType.Decimal, str);
        case Token.Float:
          return (ExpressionNode) new ConstNode(ValueType.Float, str);
        case Token.NumericHex:
          return (ExpressionNode) new ConstNode(ValueType.Numeric, str.Substring(2));
        case Token.StringConst:
          return (ExpressionNode) new ConstNode(ValueType.String, str);
        case Token.Date:
          str = str.Substring(1, str.Length - 2);
          return (ExpressionNode) new ConstNode(ValueType.Date, str);
        default:
          throw new ArgumentException("Unexpected token: " + Utils.TokenToString(token));
      }
    }

    private enum NodeType
    {
      Noop,
      Unop,
      UnopSpec,
      Binop,
      BinopSpec,
      Zop,
      Call,
      Const,
      Name,
      Paren,
      Conv,
      TernaryOp,
      TernaryOp2,
    }

    [DebuggerDisplay("{Operator.Name} ({Type})")]
    private sealed class OperatorInfo
    {
      public readonly ExpressionParser.NodeType Type;
      public readonly Operator Operator;

      public OperatorInfo(ExpressionParser.NodeType type, Operator op)
      {
        this.Type = type;
        this.Operator = op;
      }
    }
  }
}
