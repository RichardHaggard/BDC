// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.EAN13
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class EAN13 : Product1D
  {
    private static readonly string Right = "2";
    private static readonly string Prefix = "|0|";
    private static readonly string Suffix = "|0|";
    private static readonly string Center = "0|0|0";
    private static readonly IDictionary<char, string> Parity = (IDictionary<char, string>) new Dictionary<char, string>();
    private static readonly IDictionary<string, string> Encoding;

    static EAN13()
    {
      EAN13.Parity.Add('0', "111111");
      EAN13.Parity.Add('1', "110100");
      EAN13.Parity.Add('2', "110010");
      EAN13.Parity.Add('3', "110001");
      EAN13.Parity.Add('4', "101100");
      EAN13.Parity.Add('5', "100110");
      EAN13.Parity.Add('6', "100011");
      EAN13.Parity.Add('7', "101010");
      EAN13.Parity.Add('8', "101001");
      EAN13.Parity.Add('9', "100101");
      EAN13.Encoding = (IDictionary<string, string>) new Dictionary<string, string>();
      EAN13.Encoding.Add("00", "0100111");
      EAN13.Encoding.Add("01", "0110011");
      EAN13.Encoding.Add("02", "0011011");
      EAN13.Encoding.Add("03", "0100001");
      EAN13.Encoding.Add("04", "0011101");
      EAN13.Encoding.Add("05", "0111001");
      EAN13.Encoding.Add("06", "0000101");
      EAN13.Encoding.Add("07", "0010001");
      EAN13.Encoding.Add("08", "0001001");
      EAN13.Encoding.Add("09", "0010111");
      EAN13.Encoding.Add("10", "0001101");
      EAN13.Encoding.Add("11", "0011001");
      EAN13.Encoding.Add("12", "0010011");
      EAN13.Encoding.Add("13", "0111101");
      EAN13.Encoding.Add("14", "0100011");
      EAN13.Encoding.Add("15", "0110001");
      EAN13.Encoding.Add("16", "0101111");
      EAN13.Encoding.Add("17", "0111011");
      EAN13.Encoding.Add("18", "0110111");
      EAN13.Encoding.Add("19", "0001011");
      EAN13.Encoding.Add("20", "1110010");
      EAN13.Encoding.Add("21", "1100110");
      EAN13.Encoding.Add("22", "1101100");
      EAN13.Encoding.Add("23", "1000010");
      EAN13.Encoding.Add("24", "1011100");
      EAN13.Encoding.Add("25", "1001110");
      EAN13.Encoding.Add("26", "1010000");
      EAN13.Encoding.Add("27", "1000100");
      EAN13.Encoding.Add("28", "1001000");
      EAN13.Encoding.Add("29", "1110100");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      int length = this.Checksum ? 12 : 13;
      if (value.Length > length)
        throw new InvalidLengthException(length);
      for (int index = 0; index < value.Length; ++index)
      {
        char ch = value[index];
        if (!char.IsDigit(ch))
          throw new InvalidSymbolException(ch);
      }
    }

    protected override string GetSymbols(string value)
    {
      return this.GetSymbols(value, 13);
    }

    protected override string GetHeadText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(0, 1);
    }

    protected override string GetLeftText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(1, 6);
    }

    protected override string GetRightText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(7, 6);
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(EAN13.Prefix);
      string str1 = EAN13.Parity[value[0]];
      string str2 = value.Substring(1, 6);
      for (int index1 = 0; index1 < str2.Length; ++index1)
      {
        string index2 = str1[index1].ToString() + str2[index1].ToString();
        stringBuilder.Append(EAN13.Encoding[index2]);
      }
      stringBuilder.Append(EAN13.Center);
      foreach (int num in value.Substring(7, 6))
      {
        string index = EAN13.Right + (object) (char) num;
        stringBuilder.Append(EAN13.Encoding[index]);
      }
      stringBuilder.Append(EAN13.Suffix);
      return stringBuilder.ToString();
    }
  }
}
