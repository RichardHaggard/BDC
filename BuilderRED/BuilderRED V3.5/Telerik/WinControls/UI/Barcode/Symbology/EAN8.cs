// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.EAN8
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class EAN8 : Product1D
  {
    private static readonly string Left = "0";
    private static readonly string Right = "1";
    private static readonly string Prefix = "|0|";
    private static readonly string Suffix = "|0|";
    private static readonly string Center = "0|0|0";
    private static readonly IDictionary<string, string> Encoding = (IDictionary<string, string>) new Dictionary<string, string>();

    static EAN8()
    {
      EAN8.Encoding.Add("00", "0001101");
      EAN8.Encoding.Add("01", "0011001");
      EAN8.Encoding.Add("02", "0010011");
      EAN8.Encoding.Add("03", "0111101");
      EAN8.Encoding.Add("04", "0100011");
      EAN8.Encoding.Add("05", "0110001");
      EAN8.Encoding.Add("06", "0101111");
      EAN8.Encoding.Add("07", "0111011");
      EAN8.Encoding.Add("08", "0110111");
      EAN8.Encoding.Add("09", "0001011");
      EAN8.Encoding.Add("10", "1110010");
      EAN8.Encoding.Add("11", "1100110");
      EAN8.Encoding.Add("12", "1101100");
      EAN8.Encoding.Add("13", "1000010");
      EAN8.Encoding.Add("14", "1011100");
      EAN8.Encoding.Add("15", "1001110");
      EAN8.Encoding.Add("16", "1010000");
      EAN8.Encoding.Add("17", "1000100");
      EAN8.Encoding.Add("18", "1001000");
      EAN8.Encoding.Add("19", "1110100");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      int length = this.Checksum ? 7 : 8;
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
      return this.GetSymbols(value, 8);
    }

    protected override string GetLeftText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(0, 4);
    }

    protected override string GetRightText(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      return value.Substring(4, 4);
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(EAN8.Prefix);
      foreach (int num in value.Substring(0, 4))
      {
        string index = EAN8.Left + (object) (char) num;
        stringBuilder.Append(EAN8.Encoding[index]);
      }
      stringBuilder.Append(EAN8.Center);
      foreach (int num in value.Substring(4, 4))
      {
        string index = EAN8.Right + (object) (char) num;
        stringBuilder.Append(EAN8.Encoding[index]);
      }
      stringBuilder.Append(EAN8.Suffix);
      return stringBuilder.ToString();
    }
  }
}
