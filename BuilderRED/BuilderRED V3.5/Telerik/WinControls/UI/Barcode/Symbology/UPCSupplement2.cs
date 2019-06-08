// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.UPCSupplement2
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class UPCSupplement2 : Symbology1D
  {
    private static readonly char Padding = '0';
    private static readonly IList<string> Parity = (IList<string>) new List<string>();
    private static readonly IDictionary<string, string> Encoding;

    static UPCSupplement2()
    {
      UPCSupplement2.Parity.Add("11");
      UPCSupplement2.Parity.Add("10");
      UPCSupplement2.Parity.Add("01");
      UPCSupplement2.Parity.Add("00");
      UPCSupplement2.Encoding = (IDictionary<string, string>) new Dictionary<string, string>();
      UPCSupplement2.Encoding.Add(string.Empty, "1011");
      UPCSupplement2.Encoding.Add("00", "0100111");
      UPCSupplement2.Encoding.Add("01", "0110011");
      UPCSupplement2.Encoding.Add("02", "0011011");
      UPCSupplement2.Encoding.Add("03", "0100001");
      UPCSupplement2.Encoding.Add("04", "0011101");
      UPCSupplement2.Encoding.Add("05", "0111001");
      UPCSupplement2.Encoding.Add("06", "0000101");
      UPCSupplement2.Encoding.Add("07", "0010001");
      UPCSupplement2.Encoding.Add("08", "0001001");
      UPCSupplement2.Encoding.Add("09", "0010111");
      UPCSupplement2.Encoding.Add("10", "0001101");
      UPCSupplement2.Encoding.Add("11", "0011001");
      UPCSupplement2.Encoding.Add("12", "0010011");
      UPCSupplement2.Encoding.Add("13", "0111101");
      UPCSupplement2.Encoding.Add("14", "0100011");
      UPCSupplement2.Encoding.Add("15", "0110001");
      UPCSupplement2.Encoding.Add("16", "0101111");
      UPCSupplement2.Encoding.Add("17", "0111011");
      UPCSupplement2.Encoding.Add("18", "0110111");
      UPCSupplement2.Encoding.Add("19", "0001011");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      if (value.Length > 2)
        throw new InvalidLengthException(2);
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
      return value.PadLeft(2, UPCSupplement2.Padding);
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      int index1 = Convert.ToInt32(value) % UPCSupplement2.Parity.Count;
      string str = UPCSupplement2.Parity[index1];
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(UPCSupplement2.Encoding[string.Empty]);
      for (int index2 = 0; index2 < value.Length; ++index2)
      {
        string index3 = str[index2].ToString() + value[index2].ToString();
        stringBuilder.Append(UPCSupplement2.Encoding[index3]);
      }
      return stringBuilder.ToString();
    }
  }
}
