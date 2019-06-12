// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.AlphaNumeric
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class AlphaNumeric : Symbology2D
  {
    public AlphaNumeric()
    {
      this.Encoding = new Dictionary<char, string>();
      this.Encoding.Add('0', "0");
      this.Encoding.Add('1', "1");
      this.Encoding.Add('2', "2");
      this.Encoding.Add('3', "3");
      this.Encoding.Add('4', "4");
      this.Encoding.Add('5', "5");
      this.Encoding.Add('6', "6");
      this.Encoding.Add('7', "7");
      this.Encoding.Add('8', "8");
      this.Encoding.Add('9', "9");
      this.Encoding.Add('A', "10");
      this.Encoding.Add('B', "11");
      this.Encoding.Add('C', "12");
      this.Encoding.Add('D', "13");
      this.Encoding.Add('E', "14");
      this.Encoding.Add('F', "15");
      this.Encoding.Add('G', "16");
      this.Encoding.Add('H', "17");
      this.Encoding.Add('I', "18");
      this.Encoding.Add('J', "19");
      this.Encoding.Add('K', "20");
      this.Encoding.Add('L', "21");
      this.Encoding.Add('M', "22");
      this.Encoding.Add('N', "23");
      this.Encoding.Add('O', "24");
      this.Encoding.Add('P', "25");
      this.Encoding.Add('Q', "26");
      this.Encoding.Add('R', "27");
      this.Encoding.Add('S', "28");
      this.Encoding.Add('T', "29");
      this.Encoding.Add('U', "30");
      this.Encoding.Add('V', "31");
      this.Encoding.Add('W', "32");
      this.Encoding.Add('X', "33");
      this.Encoding.Add('Y', "34");
      this.Encoding.Add('Z', "35");
      this.Encoding.Add(' ', "36");
      this.Encoding.Add('$', "37");
      this.Encoding.Add('%', "38");
      this.Encoding.Add('*', "39");
      this.Encoding.Add('+', "40");
      this.Encoding.Add('-', "41");
      this.Encoding.Add('.', "42");
      this.Encoding.Add('/', "43");
      this.Encoding.Add(':', "44");
    }

    public string ValidateData(string valueToValidate)
    {
      valueToValidate = valueToValidate.ToUpper();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char key in valueToValidate)
      {
        if (this.Encoding.ContainsKey(key))
          stringBuilder.Append(key);
      }
      return stringBuilder.ToString();
    }

    public Dictionary<int, string> EncodeData(string dataToEncode)
    {
      bool flag = dataToEncode.Length % 2 != 0;
      Dictionary<int, string> dictionary1 = new Dictionary<int, string>();
      List<int> intList = new List<int>();
      Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
      for (int key = 0; key < dataToEncode.Length; ++key)
      {
        char index = dataToEncode[key];
        dictionary1.Add(key, this.Encoding[index]);
      }
      for (int index = 0; index < dictionary1.Count; index += 2)
      {
        int num1 = int.Parse(dictionary1[index]);
        if (index + 1 <= dictionary1.Count - 1)
        {
          int num2 = int.Parse(dictionary1[index + 1]);
          int num3 = 45 * num1 + num2;
          intList.Add(num3);
        }
        else
          intList.Add(num1);
      }
      for (int key = 0; key < intList.Count; ++key)
      {
        string str = Convert.ToString(intList[key], 2);
        if (flag && key == intList.Count - 1)
          dictionary2.Add(key, str.PadLeft(6, '0'));
        else
          dictionary2.Add(key, str.PadLeft(11, '0'));
      }
      return dictionary2;
    }
  }
}
