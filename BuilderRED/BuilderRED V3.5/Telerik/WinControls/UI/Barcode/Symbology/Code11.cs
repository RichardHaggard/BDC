// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code11
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code11 : Symbology1D
  {
    private static readonly string Prefix = "*";
    private static readonly string Suffix = "*";
    private static readonly IList<char> Charset = (IList<char>) new List<char>();
    private static readonly IDictionary<char, string> Encoding;

    static Code11()
    {
      Code11.Charset.Add('0');
      Code11.Charset.Add('1');
      Code11.Charset.Add('2');
      Code11.Charset.Add('3');
      Code11.Charset.Add('4');
      Code11.Charset.Add('5');
      Code11.Charset.Add('6');
      Code11.Charset.Add('7');
      Code11.Charset.Add('8');
      Code11.Charset.Add('9');
      Code11.Charset.Add('-');
      Code11.Encoding = (IDictionary<char, string>) new Dictionary<char, string>();
      Code11.Encoding.Add('0', "101011");
      Code11.Encoding.Add('1', "1101011");
      Code11.Encoding.Add('2', "1001011");
      Code11.Encoding.Add('3', "1100101");
      Code11.Encoding.Add('4', "1011011");
      Code11.Encoding.Add('5', "1101101");
      Code11.Encoding.Add('6', "1001101");
      Code11.Encoding.Add('7', "1010011");
      Code11.Encoding.Add('8', "1101001");
      Code11.Encoding.Add('9', "110101");
      Code11.Encoding.Add('-', "101101");
      Code11.Encoding.Add('*', "1011001");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code11.Charset.Contains(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (this.Checksum)
        value += Code11.GetChecksum(value);
      if (!value.StartsWith(Code11.Prefix))
        value = Code11.Prefix + value;
      if (!value.EndsWith(Code11.Suffix))
        value += Code11.Suffix;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(Symbology1D.GapChar);
        stringBuilder.Append(Code11.Encoding[value[index]]);
      }
      return stringBuilder.ToString();
    }

    private static string GetChecksum(string value)
    {
      int length = value.Length;
      value += (string) (object) Code11.GetChecksum(value, 10, 11);
      if (length >= 10)
        value += (string) (object) Code11.GetChecksum(value, 9, 11);
      return value.Substring(length);
    }

    private static char GetChecksum(string value, int length, int modulo)
    {
      int num1 = 0;
      int num2 = 1;
      for (int index = value.Length - 1; index >= 0; --index)
      {
        int num3 = Code11.Charset.IndexOf(value[index]);
        num1 += num3 * num2++;
        if (num2 > length)
          num2 = 1;
      }
      int index1 = num1 % modulo;
      return Code11.Charset[index1];
    }
  }
}
