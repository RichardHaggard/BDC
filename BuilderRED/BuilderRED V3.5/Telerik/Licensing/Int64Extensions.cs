// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Int64Extensions
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  internal static class Int64Extensions
  {
    public static long Parse(string str)
    {
      long result;
      if (Int64Extensions.TryParse(str, out result))
        return result;
      throw new Exception();
    }

    public static long Parse(string str, NumberStyle style)
    {
      if (style == NumberStyle.Hexadecimal)
        return Int64Extensions.ParseHex(str);
      return Int64Extensions.Parse(str);
    }

    public static bool TryParse(string str, out long result)
    {
      result = 0L;
      ulong result1;
      bool sign;
      if (Helper.TryParseUInt64Core(str, false, out result1, out sign))
      {
        if (!sign)
        {
          if (result1 <= (ulong) long.MaxValue)
          {
            result = (long) result1;
            return true;
          }
        }
        else if (result1 <= 9223372036854775808UL)
        {
          result = -(long) result1;
          return true;
        }
      }
      return false;
    }

    private static long ParseHex(string str)
    {
      ulong result;
      if (Int64Extensions.TryParseHex(str, out result))
        return (long) result;
      throw new Exception();
    }

    private static bool TryParseHex(string str, out ulong result)
    {
      bool sign;
      return Helper.TryParseUInt64Core(str, true, out result, out sign);
    }
  }
}
