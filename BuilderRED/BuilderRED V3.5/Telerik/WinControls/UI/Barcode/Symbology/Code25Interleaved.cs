// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code25Interleaved
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code25Interleaved : Code25
  {
    private static readonly string Prefix = "[";
    private static readonly string Suffix = "]";
    private static readonly string Padding = "0";
    private static readonly IDictionary<string, string> Encoding = (IDictionary<string, string>) new Dictionary<string, string>();

    static Code25Interleaved()
    {
      Code25Interleaved.Encoding.Add("00", "10101100110010");
      Code25Interleaved.Encoding.Add("01", "10010110110100");
      Code25Interleaved.Encoding.Add("02", "10100110110100");
      Code25Interleaved.Encoding.Add("03", "10010011011010");
      Code25Interleaved.Encoding.Add("04", "10101100110100");
      Code25Interleaved.Encoding.Add("05", "10010110011010");
      Code25Interleaved.Encoding.Add("06", "10100110011010");
      Code25Interleaved.Encoding.Add("07", "10101101100100");
      Code25Interleaved.Encoding.Add("08", "10010110110010");
      Code25Interleaved.Encoding.Add("09", "10100110110010");
      Code25Interleaved.Encoding.Add("10", "11010100100110");
      Code25Interleaved.Encoding.Add("11", "11001010101100");
      Code25Interleaved.Encoding.Add("12", "11010010101100");
      Code25Interleaved.Encoding.Add("13", "11001001010110");
      Code25Interleaved.Encoding.Add("14", "11010100101100");
      Code25Interleaved.Encoding.Add("15", "11001010010110");
      Code25Interleaved.Encoding.Add("16", "11010010010110");
      Code25Interleaved.Encoding.Add("17", "11010101001100");
      Code25Interleaved.Encoding.Add("18", "11001010100110");
      Code25Interleaved.Encoding.Add("19", "11010010100110");
      Code25Interleaved.Encoding.Add("20", "10110100100110");
      Code25Interleaved.Encoding.Add("21", "10011010101100");
      Code25Interleaved.Encoding.Add("22", "10110010101100");
      Code25Interleaved.Encoding.Add("23", "10011001010110");
      Code25Interleaved.Encoding.Add("24", "10110100101100");
      Code25Interleaved.Encoding.Add("25", "10011010010110");
      Code25Interleaved.Encoding.Add("26", "10110010010110");
      Code25Interleaved.Encoding.Add("27", "10110101001100");
      Code25Interleaved.Encoding.Add("28", "10011010100110");
      Code25Interleaved.Encoding.Add("29", "10110010100110");
      Code25Interleaved.Encoding.Add("30", "11011010010010");
      Code25Interleaved.Encoding.Add("31", "11001101010100");
      Code25Interleaved.Encoding.Add("32", "11011001010100");
      Code25Interleaved.Encoding.Add("33", "11001100101010");
      Code25Interleaved.Encoding.Add("34", "11011010010100");
      Code25Interleaved.Encoding.Add("35", "11001101001010");
      Code25Interleaved.Encoding.Add("36", "11011001001010");
      Code25Interleaved.Encoding.Add("37", "11011010100100");
      Code25Interleaved.Encoding.Add("38", "11001101010010");
      Code25Interleaved.Encoding.Add("39", "11011001010010");
      Code25Interleaved.Encoding.Add("40", "10101100100110");
      Code25Interleaved.Encoding.Add("41", "10010110101100");
      Code25Interleaved.Encoding.Add("42", "10100110101100");
      Code25Interleaved.Encoding.Add("43", "10010011010110");
      Code25Interleaved.Encoding.Add("44", "10101100101100");
      Code25Interleaved.Encoding.Add("45", "10010110010110");
      Code25Interleaved.Encoding.Add("46", "10100110010110");
      Code25Interleaved.Encoding.Add("47", "10101101001100");
      Code25Interleaved.Encoding.Add("48", "10010110100110");
      Code25Interleaved.Encoding.Add("49", "10100110100110");
      Code25Interleaved.Encoding.Add("50", "11010110010010");
      Code25Interleaved.Encoding.Add("51", "11001011010100");
      Code25Interleaved.Encoding.Add("52", "11010011010100");
      Code25Interleaved.Encoding.Add("53", "11001001101010");
      Code25Interleaved.Encoding.Add("54", "11010110010100");
      Code25Interleaved.Encoding.Add("55", "11001011001010");
      Code25Interleaved.Encoding.Add("56", "11010011001010");
      Code25Interleaved.Encoding.Add("57", "11010110100100");
      Code25Interleaved.Encoding.Add("58", "11001011010010");
      Code25Interleaved.Encoding.Add("59", "11010011010010");
      Code25Interleaved.Encoding.Add("60", "10110110010010");
      Code25Interleaved.Encoding.Add("61", "10011011010100");
      Code25Interleaved.Encoding.Add("62", "10110011010100");
      Code25Interleaved.Encoding.Add("63", "10011001101010");
      Code25Interleaved.Encoding.Add("64", "10110110010100");
      Code25Interleaved.Encoding.Add("65", "10011011001010");
      Code25Interleaved.Encoding.Add("66", "10110011001010");
      Code25Interleaved.Encoding.Add("67", "10110110100100");
      Code25Interleaved.Encoding.Add("68", "10011011010010");
      Code25Interleaved.Encoding.Add("69", "10110011010010");
      Code25Interleaved.Encoding.Add("70", "10101001100110");
      Code25Interleaved.Encoding.Add("71", "10010101101100");
      Code25Interleaved.Encoding.Add("72", "10100101101100");
      Code25Interleaved.Encoding.Add("73", "10010010110110");
      Code25Interleaved.Encoding.Add("74", "10101001101100");
      Code25Interleaved.Encoding.Add("75", "10010100110110");
      Code25Interleaved.Encoding.Add("76", "10100100110110");
      Code25Interleaved.Encoding.Add("77", "10101011001100");
      Code25Interleaved.Encoding.Add("78", "10010101100110");
      Code25Interleaved.Encoding.Add("79", "10100101100110");
      Code25Interleaved.Encoding.Add("80", "11010100110010");
      Code25Interleaved.Encoding.Add("81", "11001010110100");
      Code25Interleaved.Encoding.Add("82", "11010010110100");
      Code25Interleaved.Encoding.Add("83", "11001001011010");
      Code25Interleaved.Encoding.Add("84", "11010100110100");
      Code25Interleaved.Encoding.Add("85", "11001010011010");
      Code25Interleaved.Encoding.Add("86", "11010010011010");
      Code25Interleaved.Encoding.Add("87", "11010101100100");
      Code25Interleaved.Encoding.Add("88", "11001010110010");
      Code25Interleaved.Encoding.Add("89", "11010010110010");
      Code25Interleaved.Encoding.Add("90", "10110100110010");
      Code25Interleaved.Encoding.Add("91", "10011010110100");
      Code25Interleaved.Encoding.Add("92", "10110010110100");
      Code25Interleaved.Encoding.Add("93", "10011001011010");
      Code25Interleaved.Encoding.Add("94", "10110100110100");
      Code25Interleaved.Encoding.Add("95", "10011010011010");
      Code25Interleaved.Encoding.Add("96", "10110010011010");
      Code25Interleaved.Encoding.Add("97", "10110101100100");
      Code25Interleaved.Encoding.Add("98", "10011010110010");
      Code25Interleaved.Encoding.Add("99", "10110010110010");
      Code25Interleaved.Encoding.Add("[", "1010");
      Code25Interleaved.Encoding.Add("]", "1101");
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (this.Checksum)
        value += (string) (object) Code25.GetChecksum(value);
      if (value.Length % 2 != 0)
        value = Code25Interleaved.Padding + value;
      if (!value.StartsWith(Code25Interleaved.Prefix))
        value = Code25Interleaved.Prefix + value;
      if (!value.EndsWith(Code25Interleaved.Suffix))
        value += Code25Interleaved.Suffix;
      StringBuilder stringBuilder = new StringBuilder();
      int length;
      for (int startIndex = 0; startIndex < value.Length; startIndex += length)
      {
        length = !char.IsDigit(value[startIndex]) ? 1 : 2;
        string index = value.Substring(startIndex, length);
        stringBuilder.Append(Code25Interleaved.Encoding[index]);
      }
      return stringBuilder.ToString();
    }
  }
}
