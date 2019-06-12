// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code25Standard
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code25Standard : Code25
  {
    private static readonly string Prefix = "[";
    private static readonly string Suffix = "]";
    private static readonly IDictionary<char, string> Encoding = (IDictionary<char, string>) new Dictionary<char, string>();

    static Code25Standard()
    {
      Code25Standard.Encoding.Add('0', "10101110111010");
      Code25Standard.Encoding.Add('1', "11101010101110");
      Code25Standard.Encoding.Add('2', "10111010101110");
      Code25Standard.Encoding.Add('3', "11101110101010");
      Code25Standard.Encoding.Add('4', "10101110101110");
      Code25Standard.Encoding.Add('5', "11101011101010");
      Code25Standard.Encoding.Add('6', "10111011101010");
      Code25Standard.Encoding.Add('7', "10101011101110");
      Code25Standard.Encoding.Add('8', "11101010111010");
      Code25Standard.Encoding.Add('9', "10111010111010");
      Code25Standard.Encoding.Add('[', "11011010");
      Code25Standard.Encoding.Add(']', "1101011");
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (!value.StartsWith(Code25Standard.Prefix))
        value = Code25Standard.Prefix + value;
      if (!value.EndsWith(Code25Standard.Suffix))
        value += Code25Standard.Suffix;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
        stringBuilder.Append(Code25Standard.Encoding[value[index]]);
      return stringBuilder.ToString();
    }
  }
}
