// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.ByteMode2D
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class ByteMode2D : Symbology2D
  {
    internal ByteMode2D()
    {
      this.Encoding = new Dictionary<char, string>();
      this.Encoding.Add('\r', "13");
      this.Encoding.Add('\n', "10");
      this.Encoding.Add(' ', "32");
      this.Encoding.Add('!', "33");
      this.Encoding.Add('"', "34");
      this.Encoding.Add('#', "35");
      this.Encoding.Add('$', "36");
      this.Encoding.Add('%', "37");
      this.Encoding.Add('&', "38");
      this.Encoding.Add('\'', "39");
      this.Encoding.Add('(', "40");
      this.Encoding.Add(')', "41");
      this.Encoding.Add('*', "42");
      this.Encoding.Add('+', "43");
      this.Encoding.Add(',', "44");
      this.Encoding.Add('-', "45");
      this.Encoding.Add('.', "46");
      this.Encoding.Add('/', "47");
      this.Encoding.Add('0', "48");
      this.Encoding.Add('1', "49");
      this.Encoding.Add('2', "50");
      this.Encoding.Add('3', "51");
      this.Encoding.Add('4', "52");
      this.Encoding.Add('5', "53");
      this.Encoding.Add('6', "54");
      this.Encoding.Add('7', "55");
      this.Encoding.Add('8', "56");
      this.Encoding.Add('9', "57");
      this.Encoding.Add(':', "58");
      this.Encoding.Add(';', "59");
      this.Encoding.Add('<', "60");
      this.Encoding.Add('=', "61");
      this.Encoding.Add('>', "62");
      this.Encoding.Add('?', "63");
      this.Encoding.Add('@', "64");
      this.Encoding.Add('A', "65");
      this.Encoding.Add('B', "66");
      this.Encoding.Add('C', "67");
      this.Encoding.Add('D', "68");
      this.Encoding.Add('E', "69");
      this.Encoding.Add('F', "70");
      this.Encoding.Add('G', "71");
      this.Encoding.Add('H', "72");
      this.Encoding.Add('I', "73");
      this.Encoding.Add('J', "74");
      this.Encoding.Add('K', "75");
      this.Encoding.Add('L', "76");
      this.Encoding.Add('M', "77");
      this.Encoding.Add('N', "78");
      this.Encoding.Add('O', "79");
      this.Encoding.Add('P', "80");
      this.Encoding.Add('Q', "81");
      this.Encoding.Add('R', "82");
      this.Encoding.Add('S', "83");
      this.Encoding.Add('T', "84");
      this.Encoding.Add('U', "85");
      this.Encoding.Add('V', "86");
      this.Encoding.Add('W', "87");
      this.Encoding.Add('X', "88");
      this.Encoding.Add('Y', "89");
      this.Encoding.Add('Z', "90");
      this.Encoding.Add('[', "91");
      this.Encoding.Add('\\', "92");
      this.Encoding.Add(']', "93");
      this.Encoding.Add('^', "94");
      this.Encoding.Add('_', "95");
      this.Encoding.Add('`', "96");
      this.Encoding.Add('a', "97");
      this.Encoding.Add('b', "98");
      this.Encoding.Add('c', "99");
      this.Encoding.Add('d', "100");
      this.Encoding.Add('e', "101");
      this.Encoding.Add('f', "102");
      this.Encoding.Add('g', "103");
      this.Encoding.Add('h', "104");
      this.Encoding.Add('i', "105");
      this.Encoding.Add('j', "106");
      this.Encoding.Add('k', "107");
      this.Encoding.Add('l', "108");
      this.Encoding.Add('m', "109");
      this.Encoding.Add('n', "110");
      this.Encoding.Add('o', "111");
      this.Encoding.Add('p', "112");
      this.Encoding.Add('q', "113");
      this.Encoding.Add('r', "114");
      this.Encoding.Add('s', "115");
      this.Encoding.Add('t', "116");
      this.Encoding.Add('u', "117");
      this.Encoding.Add('v', "118");
      this.Encoding.Add('w', "119");
      this.Encoding.Add('x', "120");
      this.Encoding.Add('y', "121");
      this.Encoding.Add('z', "122");
      this.Encoding.Add('{', "123");
      this.Encoding.Add('|', "124");
      this.Encoding.Add('}', "125");
      this.Encoding.Add('~', "126");
      this.Encoding.Add('｡', "161");
      this.Encoding.Add('｢', "162");
      this.Encoding.Add('｣', "163");
      this.Encoding.Add('､', "164");
      this.Encoding.Add('･', "165");
      this.Encoding.Add('ｦ', "166");
      this.Encoding.Add('ｧ', "167");
      this.Encoding.Add('ｨ', "168");
      this.Encoding.Add('ｩ', "169");
      this.Encoding.Add('ｪ', "170");
      this.Encoding.Add('ｫ', "171");
      this.Encoding.Add('ｬ', "172");
      this.Encoding.Add('ｭ', "173");
      this.Encoding.Add('ｮ', "174");
      this.Encoding.Add('ｯ', "175");
      this.Encoding.Add('ｰ', "176");
      this.Encoding.Add('ｱ', "177");
      this.Encoding.Add('ｲ', "178");
      this.Encoding.Add('ｳ', "179");
      this.Encoding.Add('ｴ', "180");
      this.Encoding.Add('ｵ', "181");
      this.Encoding.Add('ｶ', "182");
      this.Encoding.Add('ｷ', "183");
      this.Encoding.Add('ｸ', "184");
      this.Encoding.Add('ｹ', "185");
      this.Encoding.Add('ｺ', "186");
      this.Encoding.Add('ｻ', "187");
      this.Encoding.Add('ｼ', "188");
      this.Encoding.Add('ｽ', "189");
      this.Encoding.Add('ｾ', "190");
      this.Encoding.Add('ｿ', "191");
      this.Encoding.Add('ﾀ', "192");
      this.Encoding.Add('ﾁ', "193");
      this.Encoding.Add('ﾂ', "194");
      this.Encoding.Add('ﾃ', "195");
      this.Encoding.Add('ﾄ', "196");
      this.Encoding.Add('ﾅ', "197");
      this.Encoding.Add('ﾆ', "198");
      this.Encoding.Add('ﾇ', "199");
      this.Encoding.Add('ﾈ', "200");
      this.Encoding.Add('ﾉ', "201");
      this.Encoding.Add('ﾊ', "202");
      this.Encoding.Add('ﾋ', "203");
      this.Encoding.Add('ﾌ', "204");
      this.Encoding.Add('ﾍ', "205");
      this.Encoding.Add('ﾎ', "206");
      this.Encoding.Add('ﾏ', "207");
      this.Encoding.Add('ﾐ', "208");
      this.Encoding.Add('ﾑ', "209");
      this.Encoding.Add('ﾒ', "210");
      this.Encoding.Add('ﾓ', "211");
      this.Encoding.Add('ﾔ', "212");
      this.Encoding.Add('ﾕ', "213");
      this.Encoding.Add('ﾖ', "214");
      this.Encoding.Add('ﾗ', "215");
      this.Encoding.Add('ﾘ', "216");
      this.Encoding.Add('ﾙ', "217");
      this.Encoding.Add('ﾚ', "218");
      this.Encoding.Add('ﾛ', "219");
      this.Encoding.Add('ﾜ', "220");
      this.Encoding.Add('ﾝ', "221");
      this.Encoding.Add('ﾞ', "222");
      this.Encoding.Add('ﾟ', "223");
    }

    public string ValidateData(string valueToValidate)
    {
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
      Dictionary<int, string> dictionary = new Dictionary<int, string>();
      for (int key = 0; key < dataToEncode.Length; ++key)
      {
        int num = int.Parse(this.Encoding[dataToEncode[key]]);
        dictionary.Add(key, Convert.ToString(num, 2).PadLeft(8, '0'));
      }
      return dictionary;
    }
  }
}
