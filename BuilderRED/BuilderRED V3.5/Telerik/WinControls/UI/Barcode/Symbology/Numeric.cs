// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Numeric
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Numeric : Symbology2D
  {
    public Numeric()
    {
      this.CharSet = new List<char>();
      this.CharSet.Add('0');
      this.CharSet.Add('1');
      this.CharSet.Add('2');
      this.CharSet.Add('3');
      this.CharSet.Add('4');
      this.CharSet.Add('5');
      this.CharSet.Add('6');
      this.CharSet.Add('7');
      this.CharSet.Add('8');
      this.CharSet.Add('9');
    }

    public string ValidateData(string valueToValidate)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char ch in valueToValidate)
      {
        if (this.CharSet.Contains(ch))
          stringBuilder.Append(ch);
      }
      return stringBuilder.ToString();
    }

    public Dictionary<int, string> EncodeData(string dataToEncode)
    {
      Dictionary<int, string> binaryResult = new Dictionary<int, string>();
      this.EncodeSectionData(binaryResult, dataToEncode);
      return binaryResult;
    }

    private void EncodeSectionData(Dictionary<int, string> binaryResult, string rawData)
    {
      if (rawData.Length >= 3)
      {
        string s = rawData.Substring(0, 3);
        rawData = rawData.Substring(3);
        string str = Convert.ToString(int.Parse(s), 2).PadLeft(10, '0');
        binaryResult.Add(binaryResult.Count, str);
        this.EncodeSectionData(binaryResult, rawData);
      }
      else if (rawData.Length == 2)
      {
        string str = Convert.ToString(int.Parse(rawData), 2).PadLeft(7, '0');
        binaryResult.Add(binaryResult.Count, str);
      }
      else if (rawData.Length == 1)
      {
        string str = Convert.ToString(int.Parse(rawData), 2).PadLeft(4, '0');
        binaryResult.Add(binaryResult.Count, str);
      }
      else
      {
        if (binaryResult.Count != 0)
          return;
        string str = Convert.ToString(0, 2).PadLeft(4, '0');
        binaryResult.Add(binaryResult.Count, str);
      }
    }
  }
}
