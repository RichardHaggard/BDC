// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.UPCSupplement5
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class UPCSupplement5 : Symbology1D
  {
    private static readonly char Padding = '0';
    private static readonly IList<char> Charset = (IList<char>) new List<char>();
    private static readonly IDictionary<char, string> Parity;
    private static readonly IDictionary<string, string> Encoding;

    static UPCSupplement5()
    {
      UPCSupplement5.Charset.Add('0');
      UPCSupplement5.Charset.Add('1');
      UPCSupplement5.Charset.Add('2');
      UPCSupplement5.Charset.Add('3');
      UPCSupplement5.Charset.Add('4');
      UPCSupplement5.Charset.Add('5');
      UPCSupplement5.Charset.Add('6');
      UPCSupplement5.Charset.Add('7');
      UPCSupplement5.Charset.Add('8');
      UPCSupplement5.Charset.Add('9');
      UPCSupplement5.Parity = (IDictionary<char, string>) new Dictionary<char, string>();
      UPCSupplement5.Parity.Add('0', "00111");
      UPCSupplement5.Parity.Add('1', "01011");
      UPCSupplement5.Parity.Add('2', "01101");
      UPCSupplement5.Parity.Add('3', "01110");
      UPCSupplement5.Parity.Add('4', "10011");
      UPCSupplement5.Parity.Add('5', "11001");
      UPCSupplement5.Parity.Add('6', "11100");
      UPCSupplement5.Parity.Add('7', "10101");
      UPCSupplement5.Parity.Add('8', "10110");
      UPCSupplement5.Parity.Add('9', "11010");
      UPCSupplement5.Encoding = (IDictionary<string, string>) new Dictionary<string, string>();
      UPCSupplement5.Encoding.Add(string.Empty, "1011");
      UPCSupplement5.Encoding.Add("00", "0100111");
      UPCSupplement5.Encoding.Add("01", "0110011");
      UPCSupplement5.Encoding.Add("02", "0011011");
      UPCSupplement5.Encoding.Add("03", "0100001");
      UPCSupplement5.Encoding.Add("04", "0011101");
      UPCSupplement5.Encoding.Add("05", "0111001");
      UPCSupplement5.Encoding.Add("06", "0000101");
      UPCSupplement5.Encoding.Add("07", "0010001");
      UPCSupplement5.Encoding.Add("08", "0001001");
      UPCSupplement5.Encoding.Add("09", "0010111");
      UPCSupplement5.Encoding.Add("10", "0001101");
      UPCSupplement5.Encoding.Add("11", "0011001");
      UPCSupplement5.Encoding.Add("12", "0010011");
      UPCSupplement5.Encoding.Add("13", "0111101");
      UPCSupplement5.Encoding.Add("14", "0100011");
      UPCSupplement5.Encoding.Add("15", "0110001");
      UPCSupplement5.Encoding.Add("16", "0101111");
      UPCSupplement5.Encoding.Add("17", "0111011");
      UPCSupplement5.Encoding.Add("18", "0110111");
      UPCSupplement5.Encoding.Add("19", "0001011");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      if (value.Length > 5)
        throw new InvalidLengthException(5);
      for (int index = 0; index < value.Length; ++index)
      {
        char ch = value[index];
        if (!char.IsDigit(ch))
          throw new InvalidSymbolException(ch);
      }
    }

    protected override string GetSymbols(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.PadLeft(5, UPCSupplement5.Padding);
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      char checksum = UPCSupplement5.GetChecksum(value);
      string str = UPCSupplement5.Parity[checksum];
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(UPCSupplement5.Encoding[string.Empty]);
      for (int index1 = 0; index1 < value.Length; ++index1)
      {
        if (stringBuilder.Length > 0)
        {
          stringBuilder.Append(Symbology1D.GapChar);
          stringBuilder.Append(Symbology1D.BarChar);
        }
        string index2 = str[index1].ToString() + value[index1].ToString();
        stringBuilder.Append(UPCSupplement5.Encoding[index2]);
      }
      return stringBuilder.ToString();
    }

    private static char GetChecksum(string value)
    {
      return UPCSupplement5.GetChecksum(value, 3, 9, 10);
    }

    private static char GetChecksum(string value, int first, int second, int modulo)
    {
      int num1 = 0;
      int num2 = first;
      for (int index = value.Length - 1; index >= 0; --index)
      {
        int num3 = UPCSupplement5.Charset.IndexOf(value[index]);
        num1 += num3 * num2;
        num2 = num2 != first ? first : second;
      }
      int index1 = num1 % modulo;
      return UPCSupplement5.Charset[index1];
    }
  }
}
