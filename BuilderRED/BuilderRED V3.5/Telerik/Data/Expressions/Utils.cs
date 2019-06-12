// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.Utils
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Data.Expressions
{
  internal static class Utils
  {
    public static string TokenToString(Token token)
    {
      try
      {
        return string.Format("token {0} ({1})", (object) (int) token, (object) Enum.GetName(typeof (Token), (object) token));
      }
      catch (Exception ex)
      {
        return "Unknown token " + ((int) token).ToString();
      }
    }

    public static bool IsHexDigit(char ch)
    {
      switch (ch)
      {
        case '0':
        case '1':
        case '2':
        case '3':
        case '4':
        case '5':
        case '6':
        case '7':
        case '8':
        case '9':
        case 'A':
        case 'B':
        case 'C':
        case 'D':
        case 'E':
        case 'F':
        case 'a':
        case 'b':
        case 'c':
        case 'd':
        case 'e':
        case 'f':
          return true;
        default:
          return false;
      }
    }
  }
}
