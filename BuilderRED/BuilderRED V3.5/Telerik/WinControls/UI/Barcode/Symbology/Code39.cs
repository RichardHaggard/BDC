// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code39
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code39 : Symbology1D
  {
    private static readonly string Prefix = "*";
    private static readonly string Suffix = "*";
    private static readonly IList<char> Charset = (IList<char>) new List<char>();
    private static readonly IDictionary<char, string> Encoding;

    static Code39()
    {
      Code39.Charset.Add('0');
      Code39.Charset.Add('1');
      Code39.Charset.Add('2');
      Code39.Charset.Add('3');
      Code39.Charset.Add('4');
      Code39.Charset.Add('5');
      Code39.Charset.Add('6');
      Code39.Charset.Add('7');
      Code39.Charset.Add('8');
      Code39.Charset.Add('9');
      Code39.Charset.Add('A');
      Code39.Charset.Add('B');
      Code39.Charset.Add('C');
      Code39.Charset.Add('D');
      Code39.Charset.Add('E');
      Code39.Charset.Add('F');
      Code39.Charset.Add('G');
      Code39.Charset.Add('H');
      Code39.Charset.Add('I');
      Code39.Charset.Add('J');
      Code39.Charset.Add('K');
      Code39.Charset.Add('L');
      Code39.Charset.Add('M');
      Code39.Charset.Add('N');
      Code39.Charset.Add('O');
      Code39.Charset.Add('P');
      Code39.Charset.Add('Q');
      Code39.Charset.Add('R');
      Code39.Charset.Add('S');
      Code39.Charset.Add('T');
      Code39.Charset.Add('U');
      Code39.Charset.Add('V');
      Code39.Charset.Add('W');
      Code39.Charset.Add('X');
      Code39.Charset.Add('Y');
      Code39.Charset.Add('Z');
      Code39.Charset.Add('-');
      Code39.Charset.Add('.');
      Code39.Charset.Add(' ');
      Code39.Charset.Add('$');
      Code39.Charset.Add('/');
      Code39.Charset.Add('+');
      Code39.Charset.Add('%');
      Code39.Charset.Add('*');
      Code39.Encoding = (IDictionary<char, string>) new Dictionary<char, string>();
      Code39.Encoding.Add('0', "101001101101");
      Code39.Encoding.Add('1', "110100101011");
      Code39.Encoding.Add('2', "101100101011");
      Code39.Encoding.Add('3', "110110010101");
      Code39.Encoding.Add('4', "101001101011");
      Code39.Encoding.Add('5', "110100110101");
      Code39.Encoding.Add('6', "101100110101");
      Code39.Encoding.Add('7', "101001011011");
      Code39.Encoding.Add('8', "110100101101");
      Code39.Encoding.Add('9', "101100101101");
      Code39.Encoding.Add('A', "110101001011");
      Code39.Encoding.Add('B', "101101001011");
      Code39.Encoding.Add('C', "110110100101");
      Code39.Encoding.Add('D', "101011001011");
      Code39.Encoding.Add('E', "110101100101");
      Code39.Encoding.Add('F', "101101100101");
      Code39.Encoding.Add('G', "101010011011");
      Code39.Encoding.Add('H', "110101001101");
      Code39.Encoding.Add('I', "101101001101");
      Code39.Encoding.Add('J', "101011001101");
      Code39.Encoding.Add('K', "110101010011");
      Code39.Encoding.Add('L', "101101010011");
      Code39.Encoding.Add('M', "110110101001");
      Code39.Encoding.Add('N', "101011010011");
      Code39.Encoding.Add('O', "110101101001");
      Code39.Encoding.Add('P', "101101101001");
      Code39.Encoding.Add('Q', "101010110011");
      Code39.Encoding.Add('R', "110101011001");
      Code39.Encoding.Add('S', "101101011001");
      Code39.Encoding.Add('T', "101011011001");
      Code39.Encoding.Add('U', "110010101011");
      Code39.Encoding.Add('V', "100110101011");
      Code39.Encoding.Add('W', "110011010101");
      Code39.Encoding.Add('X', "100101101011");
      Code39.Encoding.Add('Y', "110010110101");
      Code39.Encoding.Add('Z', "100110110101");
      Code39.Encoding.Add('-', "100101011011");
      Code39.Encoding.Add('.', "110010101101");
      Code39.Encoding.Add(' ', "100110101101");
      Code39.Encoding.Add('$', "100100100101");
      Code39.Encoding.Add('/', "100100101001");
      Code39.Encoding.Add('+', "100101001001");
      Code39.Encoding.Add('%', "101001001001");
      Code39.Encoding.Add('*', "100101101101");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code39.Charset.Contains(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (this.Checksum)
        value += (string) (object) Code39.GetChecksum(value);
      if (!value.StartsWith(Code39.Prefix))
        value = Code39.Prefix + value;
      if (!value.EndsWith(Code39.Suffix))
        value += Code39.Suffix;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(Symbology1D.GapChar);
        stringBuilder.Append(Code39.Encoding[value[index]]);
      }
      return stringBuilder.ToString();
    }

    private static char GetChecksum(string value)
    {
      return Code39.GetChecksum(value, 43);
    }

    private static char GetChecksum(string value, int module)
    {
      int num = 0;
      for (int index = 0; index < value.Length; ++index)
        num += Code39.Charset.IndexOf(value[index]);
      int index1 = num % module;
      return Code39.Charset[index1];
    }
  }
}
