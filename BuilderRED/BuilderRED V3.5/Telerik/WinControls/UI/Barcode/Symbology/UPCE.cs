// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.UPCE
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class UPCE : Product1D
  {
    private static readonly string Prefix = "|0|";
    private static readonly string Suffix = "0|0|0|";
    private static readonly IDictionary<string, string> Parity = (IDictionary<string, string>) new Dictionary<string, string>();
    private static readonly IDictionary<string, string> Encoding;

    static UPCE()
    {
      UPCE.Parity.Add("00", "000111");
      UPCE.Parity.Add("01", "001011");
      UPCE.Parity.Add("02", "001101");
      UPCE.Parity.Add("03", "001110");
      UPCE.Parity.Add("04", "010011");
      UPCE.Parity.Add("05", "011001");
      UPCE.Parity.Add("06", "011100");
      UPCE.Parity.Add("07", "010101");
      UPCE.Parity.Add("08", "010110");
      UPCE.Parity.Add("09", "011010");
      UPCE.Parity.Add("10", "111000");
      UPCE.Parity.Add("11", "110100");
      UPCE.Parity.Add("12", "110010");
      UPCE.Parity.Add("13", "110001");
      UPCE.Parity.Add("14", "101100");
      UPCE.Parity.Add("15", "100110");
      UPCE.Parity.Add("16", "100011");
      UPCE.Parity.Add("17", "101010");
      UPCE.Parity.Add("18", "101001");
      UPCE.Parity.Add("19", "100101");
      UPCE.Encoding = (IDictionary<string, string>) new Dictionary<string, string>();
      UPCE.Encoding.Add("00", "0100111");
      UPCE.Encoding.Add("01", "0110011");
      UPCE.Encoding.Add("02", "0011011");
      UPCE.Encoding.Add("03", "0100001");
      UPCE.Encoding.Add("04", "0011101");
      UPCE.Encoding.Add("05", "0111001");
      UPCE.Encoding.Add("06", "0000101");
      UPCE.Encoding.Add("07", "0010001");
      UPCE.Encoding.Add("08", "0001001");
      UPCE.Encoding.Add("09", "0010111");
      UPCE.Encoding.Add("10", "0001101");
      UPCE.Encoding.Add("11", "0011001");
      UPCE.Encoding.Add("12", "0010011");
      UPCE.Encoding.Add("13", "0111101");
      UPCE.Encoding.Add("14", "0100011");
      UPCE.Encoding.Add("15", "0110001");
      UPCE.Encoding.Add("16", "0101111");
      UPCE.Encoding.Add("17", "0111011");
      UPCE.Encoding.Add("18", "0110111");
      UPCE.Encoding.Add("19", "0001011");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      int length = this.Checksum ? 11 : 12;
      if (value.Length > length)
        throw new InvalidLengthException(length);
      for (int index = 0; index < value.Length; ++index)
      {
        char ch = value[index];
        if (!char.IsDigit(ch))
          throw new InvalidSymbolException(ch);
      }
      value = this.GetSymbols(value, 12);
      if (!value.StartsWith("0") && !value.StartsWith("1"))
        throw new FormatException(value);
      string str1 = value.Substring(1, 5);
      string str2 = value.Substring(6, 5);
      if (str1.EndsWith("000") || str1.EndsWith("100") || str1.EndsWith("200"))
      {
        if (!str2.StartsWith("00"))
          throw new FormatException(value);
      }
      else if (str1.EndsWith("00"))
      {
        if (!str2.StartsWith("000"))
          throw new FormatException(value);
      }
      else if (str1.EndsWith("0"))
      {
        if (!str2.StartsWith("0000"))
          throw new FormatException(value);
      }
      else if (str2.CompareTo("00005") < 0 || str2.CompareTo("00009") > 0)
        throw new FormatException(value);
    }

    protected override string GetSymbols(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      value = this.GetSymbols(value, 12);
      string str = value.Substring(1, 5);
      value.Substring(6, 5);
      if (str.EndsWith("000") || str.EndsWith("100") || str.EndsWith("200"))
        return string.Format("{0}{1}{2}{3}", (object) value.Substring(0, 3), (object) value.Substring(8, 3), (object) value.Substring(3, 1), (object) value.Substring(11, 1));
      if (str.EndsWith("00"))
        return string.Format("{0}{1}3{2}", (object) value.Substring(0, 4), (object) value.Substring(9, 2), (object) value.Substring(11, 1));
      if (str.EndsWith("0"))
        return string.Format("{0}{1}4{2}", (object) value.Substring(0, 5), (object) value.Substring(10, 1), (object) value.Substring(11, 1));
      return string.Format("{0}{1}", (object) value.Substring(0, 6), (object) value.Substring(10, 2));
    }

    protected override string GetHeadText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(0, 1);
    }

    protected override string GetTailText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(7, 1);
    }

    protected override string GetLeftText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(1, 6);
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(UPCE.Prefix);
      string str1 = value.Substring(1, 6);
      string index1 = value.Substring(0, 1) + value.Substring(7, 1);
      string str2 = UPCE.Parity[index1];
      for (int index2 = 0; index2 < str1.Length; ++index2)
      {
        string index3 = str2[index2].ToString() + str1[index2].ToString();
        stringBuilder.Append(UPCE.Encoding[index3]);
      }
      stringBuilder.Append(UPCE.Suffix);
      return stringBuilder.ToString();
    }
  }
}
