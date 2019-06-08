// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.UPCA
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class UPCA : Product1D
  {
    private static readonly string Left = "0";
    private static readonly string Right = "1";
    private static readonly string Prefix = "|0|";
    private static readonly string Suffix = "|0|";
    private static readonly string Center = "0|0|0";
    private static readonly IDictionary<string, string> Encoding = (IDictionary<string, string>) new Dictionary<string, string>();

    static UPCA()
    {
      UPCA.Encoding.Add("00", "0001101");
      UPCA.Encoding.Add("01", "0011001");
      UPCA.Encoding.Add("02", "0010011");
      UPCA.Encoding.Add("03", "0111101");
      UPCA.Encoding.Add("04", "0100011");
      UPCA.Encoding.Add("05", "0110001");
      UPCA.Encoding.Add("06", "0101111");
      UPCA.Encoding.Add("07", "0111011");
      UPCA.Encoding.Add("08", "0110111");
      UPCA.Encoding.Add("09", "0001011");
      UPCA.Encoding.Add("10", "1110010");
      UPCA.Encoding.Add("11", "1100110");
      UPCA.Encoding.Add("12", "1101100");
      UPCA.Encoding.Add("13", "1000010");
      UPCA.Encoding.Add("14", "1011100");
      UPCA.Encoding.Add("15", "1001110");
      UPCA.Encoding.Add("16", "1010000");
      UPCA.Encoding.Add("17", "1000100");
      UPCA.Encoding.Add("18", "1001000");
      UPCA.Encoding.Add("19", "1110100");
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
    }

    protected override string GetSymbols(string value)
    {
      return this.GetSymbols(value, 12);
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
      return value.Substring(11, 1);
    }

    protected override string GetLeftText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(1, 5);
    }

    protected override string GetRightText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(6, 5);
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(UPCA.Prefix);
      foreach (int num in value.Substring(0, 6))
      {
        string index = UPCA.Left + (object) (char) num;
        stringBuilder.Append(UPCA.Encoding[index]);
      }
      stringBuilder.Append(UPCA.Center);
      foreach (int num in value.Substring(6, 6))
      {
        string index = UPCA.Right + (object) (char) num;
        stringBuilder.Append(UPCA.Encoding[index]);
      }
      stringBuilder.Append(UPCA.Suffix);
      return stringBuilder.ToString();
    }
  }
}
