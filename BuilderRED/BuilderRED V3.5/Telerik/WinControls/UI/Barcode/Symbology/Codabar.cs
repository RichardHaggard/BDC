// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Codabar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Codabar : Symbology1D
  {
    private static readonly IList<char> prefixes = (IList<char>) new List<char>() { 'A', 'B', 'C', 'D' };
    private static readonly IList<char> suffixes = (IList<char>) new List<char>() { 'A', 'B', 'C', 'D' };
    private static readonly IDictionary<char, string> encoding = (IDictionary<char, string>) new Dictionary<char, string>() { { '0', "101010011" }, { '1', "101011001" }, { '2', "101001011" }, { '3', "110010101" }, { '4', "101101001" }, { '5', "110101001" }, { '6', "100101011" }, { '7', "100101101" }, { '8', "100110101" }, { '9', "110100101" }, { '-', "101001101" }, { '$', "101100101" }, { ':', "1101011011" }, { '/', "1101101011" }, { '.', "1101101101" }, { '+', "101100110011" }, { 'A', "1011001001" }, { 'B', "1010010011" }, { 'C', "1001001011" }, { 'D', "1010011001" } };

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char ch = value[index];
        if (!Codabar.encoding.ContainsKey(ch))
          throw new InvalidSymbolException(ch);
      }
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      char ch1 = value[0];
      if (!Codabar.prefixes.Contains(ch1))
        value = ((int) Codabar.prefixes[0]).ToString() + value;
      char ch2 = value[value.Length - 1];
      if (!Codabar.suffixes.Contains(ch2))
        value += (string) (object) Codabar.suffixes[0];
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(Symbology1D.GapChar);
        stringBuilder.Append(Codabar.encoding[value[index]]);
      }
      return stringBuilder.ToString();
    }
  }
}
