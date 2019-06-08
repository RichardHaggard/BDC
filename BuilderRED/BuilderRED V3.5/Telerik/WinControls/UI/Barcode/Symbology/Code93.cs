// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code93
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code93 : Symbology1D
  {
    private static readonly string Prefix = "*";
    private static readonly string Suffix = "*";
    private static readonly IList<char> Charset = (IList<char>) new List<char>();
    private static readonly IDictionary<char, string> Encoding;

    static Code93()
    {
      Code93.Charset.Add('0');
      Code93.Charset.Add('1');
      Code93.Charset.Add('2');
      Code93.Charset.Add('3');
      Code93.Charset.Add('4');
      Code93.Charset.Add('5');
      Code93.Charset.Add('6');
      Code93.Charset.Add('7');
      Code93.Charset.Add('8');
      Code93.Charset.Add('9');
      Code93.Charset.Add('A');
      Code93.Charset.Add('B');
      Code93.Charset.Add('C');
      Code93.Charset.Add('D');
      Code93.Charset.Add('E');
      Code93.Charset.Add('F');
      Code93.Charset.Add('G');
      Code93.Charset.Add('H');
      Code93.Charset.Add('I');
      Code93.Charset.Add('J');
      Code93.Charset.Add('K');
      Code93.Charset.Add('L');
      Code93.Charset.Add('M');
      Code93.Charset.Add('N');
      Code93.Charset.Add('O');
      Code93.Charset.Add('P');
      Code93.Charset.Add('Q');
      Code93.Charset.Add('R');
      Code93.Charset.Add('S');
      Code93.Charset.Add('T');
      Code93.Charset.Add('U');
      Code93.Charset.Add('V');
      Code93.Charset.Add('W');
      Code93.Charset.Add('X');
      Code93.Charset.Add('Y');
      Code93.Charset.Add('Z');
      Code93.Charset.Add('-');
      Code93.Charset.Add('.');
      Code93.Charset.Add(' ');
      Code93.Charset.Add('$');
      Code93.Charset.Add('/');
      Code93.Charset.Add('+');
      Code93.Charset.Add('%');
      Code93.Charset.Add('@');
      Code93.Charset.Add('#');
      Code93.Charset.Add('&');
      Code93.Charset.Add('~');
      Code93.Charset.Add('*');
      Code93.Encoding = (IDictionary<char, string>) new Dictionary<char, string>();
      Code93.Encoding.Add('0', "100010100");
      Code93.Encoding.Add('1', "101001000");
      Code93.Encoding.Add('2', "101000100");
      Code93.Encoding.Add('3', "101000010");
      Code93.Encoding.Add('4', "100101000");
      Code93.Encoding.Add('5', "100100100");
      Code93.Encoding.Add('6', "100100010");
      Code93.Encoding.Add('7', "101010000");
      Code93.Encoding.Add('8', "100010010");
      Code93.Encoding.Add('9', "100001010");
      Code93.Encoding.Add('A', "110101000");
      Code93.Encoding.Add('B', "110100100");
      Code93.Encoding.Add('C', "110100010");
      Code93.Encoding.Add('D', "110010100");
      Code93.Encoding.Add('E', "110010010");
      Code93.Encoding.Add('F', "110001010");
      Code93.Encoding.Add('G', "101101000");
      Code93.Encoding.Add('H', "101100100");
      Code93.Encoding.Add('I', "101100010");
      Code93.Encoding.Add('J', "100110100");
      Code93.Encoding.Add('K', "100011010");
      Code93.Encoding.Add('L', "101011000");
      Code93.Encoding.Add('M', "101001100");
      Code93.Encoding.Add('N', "101000110");
      Code93.Encoding.Add('O', "100101100");
      Code93.Encoding.Add('P', "100010110");
      Code93.Encoding.Add('Q', "110110100");
      Code93.Encoding.Add('R', "110110010");
      Code93.Encoding.Add('S', "110101100");
      Code93.Encoding.Add('T', "110100110");
      Code93.Encoding.Add('U', "110010110");
      Code93.Encoding.Add('V', "110011010");
      Code93.Encoding.Add('W', "101101100");
      Code93.Encoding.Add('X', "101100110");
      Code93.Encoding.Add('Y', "100110110");
      Code93.Encoding.Add('Z', "100111010");
      Code93.Encoding.Add('-', "100101110");
      Code93.Encoding.Add('.', "111010100");
      Code93.Encoding.Add(' ', "111010010");
      Code93.Encoding.Add('$', "111001010");
      Code93.Encoding.Add('/', "101101110");
      Code93.Encoding.Add('+', "101110110");
      Code93.Encoding.Add('%', "110101110");
      Code93.Encoding.Add('@', "100100110");
      Code93.Encoding.Add('#', "111011010");
      Code93.Encoding.Add('&', "111010110");
      Code93.Encoding.Add('~', "100110010");
      Code93.Encoding.Add('*', "101011110");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code93.Charset.Contains(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (this.Checksum)
        value += Code93.GetChecksum(value);
      if (!value.StartsWith(Code93.Prefix))
        value = Code93.Prefix + value;
      if (!value.EndsWith(Code93.Suffix))
        value += Code93.Suffix;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
        stringBuilder.Append(Code93.Encoding[value[index]]);
      stringBuilder.Append(Symbology1D.BarChar);
      return stringBuilder.ToString();
    }

    private static string GetChecksum(string value)
    {
      int length = value.Length;
      value += (string) (object) Code93.GetChecksum(value, 20, 47);
      value += (string) (object) Code93.GetChecksum(value, 15, 47);
      return value.Substring(length);
    }

    private static char GetChecksum(string value, int length, int modulo)
    {
      int num1 = 0;
      int num2 = 1;
      for (int index = value.Length - 1; index >= 0; --index)
      {
        int num3 = Code93.Charset.IndexOf(value[index]);
        num1 += num3 * num2++;
        if (num2 > length)
          num2 = 1;
      }
      int index1 = num1 % modulo;
      return Code93.Charset[index1];
    }
  }
}
