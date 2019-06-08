// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.LexicalException
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Data.Expressions
{
  internal class LexicalException : InvalidExpressionException
  {
    public static Exception InvalidDate(string date)
    {
      return LexicalException.CreateException("The expression contains invalid date constant '{0}'.", (object) date);
    }

    public static Exception UnexpectedToken(
      string expectedToken,
      string currentToken,
      int pos)
    {
      return LexicalException.CreateException("Expected {0}, but actual token at the position {2} is {1}.", (object) expectedToken, (object) currentToken, (object) pos);
    }

    public static Exception UnknownToken(string token, int position)
    {
      return LexicalException.CreateException("Cannot interpret token '{0}' at position {1}.", (object) token, (object) position);
    }

    public static Exception InvalidString(string str)
    {
      return LexicalException.CreateException("The expression contains an invalid string constant: {0}.", (object) str);
    }

    public static Exception InvalidName(string name)
    {
      return LexicalException.CreateException("The expression contains invalid name: '{0}'.", (object) name);
    }

    public static Exception InvalidHex(string text)
    {
      return LexicalException.CreateException("The expression contains invalid hexadecimal number: '{0}'.", (object) text);
    }

    private static Exception CreateException(string format, params object[] args)
    {
      return (Exception) new LexicalException(string.Format(format, args));
    }

    public LexicalException(string message)
      : base(message)
    {
    }
  }
}
