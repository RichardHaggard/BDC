// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.InvalidExpressionException
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Data.Expressions
{
  public class InvalidExpressionException : Exception
  {
    public static Exception AmbiguousBinop(Operator op, Type type1, Type type2)
    {
      return InvalidExpressionException.CreateException("Operator '{0}' is ambiguous on operands of type '{1}' and '{2}'. Cannot mix signed and unsigned types. Please use explicit Convert() function.", (object) op, (object) type1, (object) type2);
    }

    public static Exception TypeMismatch(string exp)
    {
      return InvalidExpressionException.CreateException("Type mismatch in expression '{0}'.", (object) exp);
    }

    public static Exception TypeMismatchInBinop(Operator op, Type type1, Type type2)
    {
      return InvalidExpressionException.CreateException("Cannot perform '{0}' operation on {1} and {2}.", (object) op, (object) type1, (object) type2);
    }

    public static Exception DatavalueConvertion(
      object value,
      Type type,
      Exception innerException)
    {
      return InvalidExpressionException.CreateException(innerException, "Cannot convert value '{0}' to Type: {1}.");
    }

    public static Exception SqlConvertFailed(Type type1, Type type2)
    {
      return InvalidExpressionException.CreateException("Cannot convert from Type: '{0}' to Type: {1}.", (object) type1, (object) type2);
    }

    public static Exception InWithoutParentheses()
    {
      return InvalidExpressionException.CreateException("Syntax error: The items following the IN keyword must be separated by commas and be enclosed in parentheses.");
    }

    public static Exception UnsupportedOperator(Operator op)
    {
      return InvalidExpressionException.CreateException("The expression contains unsupported operator '{0}'.", (object) op);
    }

    public static Exception UndefinedFunction(string name)
    {
      return InvalidExpressionException.CreateException("The expression contains undefined function call {0}().", (object) name);
    }

    public static Exception UndefinedObject(string name)
    {
      return InvalidExpressionException.CreateException("The expression contains object '{0}' that is not defined in the current context.", (object) name);
    }

    public static Exception ErrorInFunc(string name, Exception innerException)
    {
      return InvalidExpressionException.CreateException(innerException, "An error has occured while executing function {0}(). Check InnerException for further information.", (object) name);
    }

    public static Exception ArgumentTypeInteger(string name, int index)
    {
      return InvalidExpressionException.CreateException("Type mismatch in function argument: {0}(), argument {1}, expected one of the Integer types.", (object) name, (object) index);
    }

    public static Exception InvalidPattern(string pattern)
    {
      return InvalidExpressionException.CreateException("Invalid LIKE pattern: {0}", (object) pattern);
    }

    public InvalidExpressionException(string message)
      : base(message)
    {
    }

    public InvalidExpressionException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    private static Exception CreateException(string format, params object[] args)
    {
      return (Exception) new InvalidExpressionException(string.Format(format, args));
    }

    private static Exception CreateException(
      Exception innerException,
      string format,
      params object[] args)
    {
      return (Exception) new InvalidExpressionException(string.Format(format, args), innerException);
    }
  }
}
