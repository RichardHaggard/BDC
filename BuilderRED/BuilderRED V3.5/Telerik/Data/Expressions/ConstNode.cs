// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ConstNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  internal class ConstNode : ExpressionNode
  {
    private object value;

    public override bool IsConst
    {
      get
      {
        return true;
      }
    }

    public ConstNode(ValueType type, string text)
    {
      this.Init(type, text);
    }

    public override object Eval(object row, object context)
    {
      return this.value;
    }

    private void Init(ValueType type, string text)
    {
      switch (type)
      {
        case ValueType.Null:
          this.value = (object) DBNull.Value;
          break;
        case ValueType.Bool:
          this.value = (object) Convert.ToBoolean(text);
          break;
        case ValueType.Numeric:
          try
          {
            this.value = (object) Convert.ToInt32(text);
            break;
          }
          catch (Exception ex1)
          {
            try
            {
              this.value = (object) int.Parse(text, NumberStyles.HexNumber);
              break;
            }
            catch (Exception ex2)
            {
              try
              {
                this.value = (object) Convert.ToInt64(text);
                break;
              }
              catch (Exception ex3)
              {
                try
                {
                  this.value = (object) Convert.ToDouble(text, (IFormatProvider) NumberFormatInfo.InvariantInfo);
                  break;
                }
                catch (Exception ex4)
                {
                  this.value = (object) text;
                  break;
                }
              }
            }
          }
        case ValueType.String:
          char ch = text[0];
          char[] charArray = text.ToCharArray(1, text.Length - 2);
          int length = 0;
          for (int index = 0; index < charArray.Length; ++index)
          {
            if ((int) charArray[index] == (int) ch)
              ++index;
            charArray[length] = charArray[index];
            ++length;
          }
          text = new string(charArray, 0, length);
          this.value = (object) text;
          break;
        case ValueType.Float:
          this.value = (object) Convert.ToDouble(text, (IFormatProvider) NumberFormatInfo.InvariantInfo);
          break;
        case ValueType.Decimal:
          try
          {
            this.value = (object) Convert.ToDecimal(text, (IFormatProvider) NumberFormatInfo.InvariantInfo);
            break;
          }
          catch (Exception ex1)
          {
            try
            {
              this.value = (object) Convert.ToDouble(text, (IFormatProvider) NumberFormatInfo.InvariantInfo);
              break;
            }
            catch (Exception ex2)
            {
              this.value = (object) text;
              break;
            }
          }
        case ValueType.Date:
          this.value = (object) DateTime.Parse(text, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        default:
          throw new ArgumentOutOfRangeException(type.ToString(), nameof (type));
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
    }

    public override string ToString()
    {
      return "Const(" + this.value + ")";
    }
  }
}
