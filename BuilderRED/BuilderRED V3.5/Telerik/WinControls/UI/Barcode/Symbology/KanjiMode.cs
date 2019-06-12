// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.KanjiMode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class KanjiMode
  {
    private const string ShiftJsPath = "KanjiShiftJS.txt";
    private const string UniPath = "KanjiUni.txt";
    private const int ValidStep = 4;
    private List<string> unicodeValues;
    private List<string> shiftJISCodeValues;

    public KanjiMode()
    {
      this.unicodeValues = BarcodeResources.GetEncodedValues("KanjiUni.txt", 4);
      this.shiftJISCodeValues = BarcodeResources.GetEncodedValues("KanjiShiftJS.txt", 4);
    }

    public Dictionary<int, string> EncodeData(string dataToEncode)
    {
      Dictionary<int, string> dictionary = new Dictionary<int, string>();
      for (int key = 0; key < dataToEncode.Length; ++key)
      {
        int num1 = int.Parse(this.shiftJISCodeValues[this.unicodeValues.IndexOf(Convert.ToString((int) dataToEncode[key], 16).ToUpper())], NumberStyles.HexNumber);
        if (this.shiftJISCodeValues.Contains(Convert.ToString(num1, 16).ToUpper()))
        {
          int num2 = 33088;
          int num3 = 40956;
          int num4 = 57408;
          int num5 = 60351;
          int num6 = num1;
          if (num6 >= num2 && num6 <= num3)
          {
            int num7 = num6 - 33088;
            int num8 = int.Parse(Convert.ToString(num7, 16).ToUpper().PadLeft(4, '0').Substring(0, 2), NumberStyles.HexNumber) * 192 + int.Parse(Convert.ToString(num7, 16).ToUpper().PadLeft(4, '0').Substring(2), NumberStyles.HexNumber);
            dictionary.Add(key, Convert.ToString(num8, 2).PadLeft(13, '0'));
          }
          else if (num6 >= num4 && num6 <= num5)
          {
            int num7 = num6 - 49472;
            int num8 = int.Parse(Convert.ToString(num7, 16).ToUpper().PadLeft(4, '0').Substring(0, 2), NumberStyles.HexNumber) * 192 + int.Parse(Convert.ToString(num7, 16).ToUpper().PadLeft(4, '0').Substring(2), NumberStyles.HexNumber);
            dictionary.Add(key, Convert.ToString(num8, 2).PadLeft(13, '0'));
          }
        }
      }
      return dictionary;
    }

    public string ValidateData(string valueToValidate)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < valueToValidate.Length; ++index)
      {
        if (this.unicodeValues.Contains(Convert.ToString((int) valueToValidate[index], 16).ToUpper()))
          stringBuilder.Append(valueToValidate[index]);
      }
      return stringBuilder.ToString();
    }
  }
}
