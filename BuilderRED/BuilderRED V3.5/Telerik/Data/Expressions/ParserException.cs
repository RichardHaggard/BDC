// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ParserException
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Data.Expressions
{
  internal class ParserException : InvalidExpressionException
  {
    public static Exception MissingOperator(string token)
    {
      return ParserException.CreateException("Missing operator before {0} operand.", (object) token);
    }

    public static Exception MissingOperandBefore(string op)
    {
      return ParserException.CreateException("Syntax error: Missing operand before '{0}' operator.", (object) op);
    }

    public static Exception UnsupportedOperator(string op)
    {
      return ParserException.CreateException("The expression contains unsupported operator '{0}'.", (object) op);
    }

    public static Exception MissingRightParen()
    {
      return ParserException.CreateException("The expression is missing the closing parenthesis.");
    }

    public static Exception MissingOperand(string op)
    {
      return ParserException.CreateException("Syntax error: Missing operand after '{0}' operator.", (object) op);
    }

    public static Exception SystaxError()
    {
      return ParserException.CreateException("Syntax error in the expression.");
    }

    public static Exception UnknownToken(string token, int position)
    {
      return ParserException.CreateException("Cannot interpret token '{0}' at position {1}.", (object) token, (object) position);
    }

    public static Exception TooManyRightParentheses()
    {
      return ParserException.CreateException("The expression has too many closing parentheses.");
    }

    public static Exception AggregateArgument()
    {
      return ParserException.CreateException("Syntax error in aggregate argument: Expecting a single column argument.");
    }

    private ParserException(string message)
      : base(message)
    {
    }

    private static Exception CreateException(string format, params object[] args)
    {
      return (Exception) new ParserException(string.Format(format, args));
    }
  }
}
