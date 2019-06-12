// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.StringExtensions
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal static class StringExtensions
  {
    public static bool EndsWith(string s, string value)
    {
      return s.IndexOf(value) == s.Length - value.Length;
    }

    public static bool StartsWith(string s, string value)
    {
      return s.IndexOf(value) == 0;
    }

    public static bool Contains(string s, string value)
    {
      return s.IndexOf(value) >= 0;
    }
  }
}
