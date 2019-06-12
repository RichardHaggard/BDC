// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Helper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;

namespace Telerik.Licensing
{
  internal static class Helper
  {
    public const int MaxDoubleDigits = 16;

    public static bool IsWhiteSpace(char ch)
    {
      return ch == ' ';
    }

    public static bool TryParseUInt64Core(
      string str,
      bool parseHex,
      out ulong result,
      out bool sign)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      if (str.Length >= 2 && str.Substring(0, 2).ToLower() == "0x")
      {
        str = str.Substring(2);
        parseHex = true;
      }
      bool flag = true;
      result = 0UL;
      int length = str.Length;
      int index = 0;
      while (index < length && Helper.IsWhiteSpace(str[index]))
        ++index;
      NumberFormatInfo numberFormat = CultureInfo.CurrentUICulture.NumberFormat;
      string positiveSign = numberFormat.PositiveSign;
      string negativeSign = numberFormat.NegativeSign;
      sign = false;
      while (index < length)
      {
        char ch = str[index];
        if (!parseHex && (int) ch == (int) negativeSign[0])
        {
          sign = true;
          ++index;
        }
        else if (!parseHex && (int) ch == (int) positiveSign[0])
        {
          sign = false;
          ++index;
        }
        else
        {
          if ((!parseHex || (ch < 'A' || ch > 'F') && (ch < 'a' || ch > 'f')) && (ch < '0' || ch > '9'))
            return false;
          break;
        }
      }
      if (index >= length)
        return false;
      uint num1 = 0;
      uint num2 = 0;
      if (parseHex)
      {
        do
        {
          char ch = str[index];
          uint num3;
          if (ch >= '0' && ch <= '9')
            num3 = (uint) ch - 48U;
          else if (ch >= 'A' && ch <= 'F')
            num3 = (uint) ((int) ch - 65 + 10);
          else if (ch >= 'a' && ch <= 'f')
            num3 = (uint) ((int) ch - 97 + 10);
          else
            break;
          if (flag)
          {
            ulong num4 = (ulong) num1 * 16UL;
            ulong num5 = (ulong) num2 * 16UL + (num4 >> 32);
            if (num5 > (ulong) uint.MaxValue)
            {
              flag = false;
            }
            else
            {
              ulong num6 = (num4 & (ulong) uint.MaxValue) + (ulong) num3;
              ulong num7 = num5 + (num6 >> 32);
              if (num7 > (ulong) uint.MaxValue)
              {
                flag = false;
              }
              else
              {
                num1 = (uint) num6;
                num2 = (uint) num7;
              }
            }
          }
          ++index;
        }
        while (index < length);
      }
      else
      {
        do
        {
          char ch = str[index];
          if (ch >= '0' && ch <= '9')
          {
            uint num3 = (uint) ch - 48U;
            if (flag)
            {
              ulong num4 = (ulong) num1 * 10UL;
              ulong num5 = (ulong) num2 * 10UL + (num4 >> 32);
              if (num5 > (ulong) uint.MaxValue)
              {
                flag = false;
              }
              else
              {
                ulong num6 = (num4 & (ulong) uint.MaxValue) + (ulong) num3;
                ulong num7 = num5 + (num6 >> 32);
                if (num7 > (ulong) uint.MaxValue)
                {
                  flag = false;
                }
                else
                {
                  num1 = (uint) num6;
                  num2 = (uint) num7;
                }
              }
            }
            ++index;
          }
          else
            break;
        }
        while (index < length);
      }
      if (index < length)
      {
        while (Helper.IsWhiteSpace(str[index]))
        {
          ++index;
          if (index >= length)
            break;
        }
        if (index < length)
          return false;
      }
      result = (ulong) num2 << 32 | (ulong) num1;
      return flag;
    }
  }
}
