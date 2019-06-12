// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.LikeNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Data.SqlTypes;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  internal class LikeNode : BinaryOpNode
  {
    private const int match_left = 1;
    private const int match_right = 2;
    private const int match_middle = 3;
    private const int match_exact = 4;
    private const int match_all = 5;
    private CompareOptions compareFlags;
    private int kind;

    public LikeNode(
      Operator op,
      ExpressionNode left,
      ExpressionNode right,
      CompareOptions compareFlags)
      : base(op, left, right)
    {
      this.compareFlags = compareFlags;
    }

    public override object Eval(object row, object context)
    {
      object obj1 = this.Left.Eval(row, context);
      if (!DataStorageHelper.IsObjectNull(obj1))
      {
        if (!(obj1 is string) && !(obj1 is SqlString))
        {
          Guid guid = obj1 as Guid;
        }
        object obj2 = this.Right.Eval(row, context);
        if (DataStorageHelper.IsObjectNull(obj2))
          return (object) DBNull.Value;
        if (!(obj2 is string) && !(obj2 is SqlString) && !(obj1 is Guid))
          throw InvalidExpressionException.TypeMismatchInBinop(Operator.Like, typeof (string), obj2.GetType());
        string str = obj1.ToString();
        string string2 = this.AnalizePattern(obj2.ToString());
        char[] chArray = new char[2]{ ' ', '　' };
        CompareInfo compareInfo = this.Culture.CompareInfo;
        switch (this.kind)
        {
          case 1:
            return (object) (0 == compareInfo.IndexOf(str, string2, this.compareFlags));
          case 2:
            string suffix = string2.TrimEnd(chArray);
            return (object) compareInfo.IsSuffix(str, suffix, this.compareFlags);
          case 3:
            return (object) (0 <= compareInfo.IndexOf(str, string2, this.compareFlags));
          case 4:
            return (object) (0 == compareInfo.Compare(str, string2, this.compareFlags));
          case 5:
            return (object) true;
        }
      }
      return (object) DBNull.Value;
    }

    internal string AnalizePattern(string pat)
    {
      int length1 = pat.Length;
      char[] destination = new char[length1 + 1];
      pat.CopyTo(0, destination, 0, length1);
      destination[length1] = char.MinValue;
      char[] chArray1 = new char[length1 + 1];
      int length2 = 0;
      int num1 = 0;
      int index1 = 0;
      while (index1 < length1)
      {
        if (destination[index1] == '*' || destination[index1] == '%')
        {
          while ((destination[index1] == '*' || destination[index1] == '%') && index1 < length1)
            ++index1;
          if (index1 < length1 && length2 > 0 || num1 >= 2)
            throw InvalidExpressionException.InvalidPattern(pat);
          ++num1;
        }
        else if (destination[index1] == '[')
        {
          int num2 = index1 + 1;
          if (num2 >= length1)
            throw InvalidExpressionException.InvalidPattern(pat);
          char[] chArray2 = chArray1;
          int index2 = length2++;
          char[] chArray3 = destination;
          int index3 = num2;
          int index4 = index3 + 1;
          int num3 = (int) chArray3[index3];
          chArray2[index2] = (char) num3;
          if (index4 >= length1)
            throw InvalidExpressionException.InvalidPattern(pat);
          if (destination[index4] != ']')
            throw InvalidExpressionException.InvalidPattern(pat);
          index1 = index4 + 1;
        }
        else
        {
          chArray1[length2++] = destination[index1];
          ++index1;
        }
      }
      string str = new string(chArray1, 0, length2);
      if (num1 == 0)
      {
        this.kind = 4;
        return str;
      }
      if (length2 > 0)
      {
        if (destination[0] == '*' || destination[0] == '%')
        {
          if (destination[length1 - 1] == '*' || destination[length1 - 1] == '%')
          {
            this.kind = 3;
            return str;
          }
          this.kind = 2;
          return str;
        }
        this.kind = 1;
        return str;
      }
      this.kind = 5;
      return str;
    }
  }
}
