// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.UInt32Extensions
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal static class UInt32Extensions
  {
    public static bool TryParse(string str, NumberStyle style, out uint result)
    {
      ulong result1;
      bool sign;
      bool uint64Core = Helper.TryParseUInt64Core(str, style == NumberStyle.Hexadecimal, out result1, out sign);
      result = (uint) result1;
      if (uint64Core)
        return !sign;
      return false;
    }
  }
}
