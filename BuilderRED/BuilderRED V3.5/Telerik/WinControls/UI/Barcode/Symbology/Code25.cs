// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code25
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public abstract class Code25 : Symbology1D
  {
    private static readonly IList<char> Charset = (IList<char>) new List<char>();

    static Code25()
    {
      Code25.Charset.Add('0');
      Code25.Charset.Add('1');
      Code25.Charset.Add('2');
      Code25.Charset.Add('3');
      Code25.Charset.Add('4');
      Code25.Charset.Add('5');
      Code25.Charset.Add('6');
      Code25.Charset.Add('7');
      Code25.Charset.Add('8');
      Code25.Charset.Add('9');
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char ch = value[index];
        if (!char.IsDigit(ch))
          throw new InvalidSymbolException(ch);
      }
    }

    protected static char GetChecksum(string value)
    {
      return Code25.GetChecksum(value, 3, 1, 10);
    }

    private static char GetChecksum(string value, int first, int second, int modulo)
    {
      int num1 = 0;
      int num2 = first;
      for (int index = value.Length - 1; index >= 0; --index)
      {
        int num3 = Code25.Charset.IndexOf(value[index]);
        num1 += num3 * num2;
        num2 = num2 != first ? first : second;
      }
      int index1 = num1 % modulo;
      if (index1 != 0)
        index1 = modulo - index1;
      return Code25.Charset[index1];
    }
  }
}
