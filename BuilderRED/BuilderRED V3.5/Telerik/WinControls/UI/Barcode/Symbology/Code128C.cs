// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code128C
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code128C : Code128
  {
    private static readonly int Switch = 99;
    private static readonly int Prefix = 105;
    private static readonly IList<char> Charset = (IList<char>) new List<char>();
    private static readonly IList<string> Codeset;

    static Code128C()
    {
      Code128C.Charset.Add('0');
      Code128C.Charset.Add('1');
      Code128C.Charset.Add('2');
      Code128C.Charset.Add('3');
      Code128C.Charset.Add('4');
      Code128C.Charset.Add('5');
      Code128C.Charset.Add('6');
      Code128C.Charset.Add('7');
      Code128C.Charset.Add('8');
      Code128C.Charset.Add('9');
      Code128C.Charset.Add('õ');
      Code128C.Charset.Add('ô');
      Code128C.Charset.Add('÷');
      Code128C.Charset.Add('ü');
      Code128C.Charset.Add('ý');
      Code128C.Charset.Add('þ');
      Code128C.Charset.Add('ÿ');
      Code128C.Codeset = (IList<string>) new List<string>();
      Code128C.Codeset.Add("00");
      Code128C.Codeset.Add("01");
      Code128C.Codeset.Add("02");
      Code128C.Codeset.Add("03");
      Code128C.Codeset.Add("04");
      Code128C.Codeset.Add("05");
      Code128C.Codeset.Add("06");
      Code128C.Codeset.Add("07");
      Code128C.Codeset.Add("08");
      Code128C.Codeset.Add("09");
      Code128C.Codeset.Add("10");
      Code128C.Codeset.Add("11");
      Code128C.Codeset.Add("12");
      Code128C.Codeset.Add("13");
      Code128C.Codeset.Add("14");
      Code128C.Codeset.Add("15");
      Code128C.Codeset.Add("16");
      Code128C.Codeset.Add("17");
      Code128C.Codeset.Add("18");
      Code128C.Codeset.Add("19");
      Code128C.Codeset.Add("20");
      Code128C.Codeset.Add("21");
      Code128C.Codeset.Add("22");
      Code128C.Codeset.Add("23");
      Code128C.Codeset.Add("24");
      Code128C.Codeset.Add("25");
      Code128C.Codeset.Add("26");
      Code128C.Codeset.Add("27");
      Code128C.Codeset.Add("28");
      Code128C.Codeset.Add("29");
      Code128C.Codeset.Add("30");
      Code128C.Codeset.Add("31");
      Code128C.Codeset.Add("32");
      Code128C.Codeset.Add("33");
      Code128C.Codeset.Add("34");
      Code128C.Codeset.Add("35");
      Code128C.Codeset.Add("36");
      Code128C.Codeset.Add("37");
      Code128C.Codeset.Add("38");
      Code128C.Codeset.Add("39");
      Code128C.Codeset.Add("40");
      Code128C.Codeset.Add("41");
      Code128C.Codeset.Add("42");
      Code128C.Codeset.Add("43");
      Code128C.Codeset.Add("44");
      Code128C.Codeset.Add("45");
      Code128C.Codeset.Add("46");
      Code128C.Codeset.Add("47");
      Code128C.Codeset.Add("48");
      Code128C.Codeset.Add("49");
      Code128C.Codeset.Add("50");
      Code128C.Codeset.Add("51");
      Code128C.Codeset.Add("52");
      Code128C.Codeset.Add("53");
      Code128C.Codeset.Add("54");
      Code128C.Codeset.Add("55");
      Code128C.Codeset.Add("56");
      Code128C.Codeset.Add("57");
      Code128C.Codeset.Add("58");
      Code128C.Codeset.Add("59");
      Code128C.Codeset.Add("60");
      Code128C.Codeset.Add("61");
      Code128C.Codeset.Add("62");
      Code128C.Codeset.Add("63");
      Code128C.Codeset.Add("64");
      Code128C.Codeset.Add("65");
      Code128C.Codeset.Add("66");
      Code128C.Codeset.Add("67");
      Code128C.Codeset.Add("68");
      Code128C.Codeset.Add("69");
      Code128C.Codeset.Add("70");
      Code128C.Codeset.Add("71");
      Code128C.Codeset.Add("72");
      Code128C.Codeset.Add("73");
      Code128C.Codeset.Add("74");
      Code128C.Codeset.Add("75");
      Code128C.Codeset.Add("76");
      Code128C.Codeset.Add("77");
      Code128C.Codeset.Add("78");
      Code128C.Codeset.Add("79");
      Code128C.Codeset.Add("80");
      Code128C.Codeset.Add("81");
      Code128C.Codeset.Add("82");
      Code128C.Codeset.Add("83");
      Code128C.Codeset.Add("84");
      Code128C.Codeset.Add("85");
      Code128C.Codeset.Add("86");
      Code128C.Codeset.Add("87");
      Code128C.Codeset.Add("88");
      Code128C.Codeset.Add("89");
      Code128C.Codeset.Add("90");
      Code128C.Codeset.Add("91");
      Code128C.Codeset.Add("92");
      Code128C.Codeset.Add("93");
      Code128C.Codeset.Add("94");
      Code128C.Codeset.Add("95");
      Code128C.Codeset.Add("96");
      Code128C.Codeset.Add("97");
      Code128C.Codeset.Add("98");
      Code128C.Codeset.Add("99");
      Code128C.Codeset.Add("õ");
      Code128C.Codeset.Add("ô");
      Code128C.Codeset.Add("÷");
      Code128C.Codeset.Add("ü");
      Code128C.Codeset.Add("ý");
      Code128C.Codeset.Add("þ");
      Code128C.Codeset.Add("ÿ");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      Code128C.TestChars(value);
      Code128C.TestCodes(value);
    }

    private static void TestChars(string value)
    {
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code128C.Charset.Contains(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    private static void TestCodes(string value)
    {
      int num;
      for (int startIndex = 0; startIndex < value.Length; startIndex = num)
      {
        int length = !char.IsDigit(value[startIndex]) ? 1 : 2;
        num = startIndex + length;
        if (num > value.Length)
          throw new FormatException(value);
        string str = value.Substring(startIndex, length);
        if (!Code128C.Codeset.Contains(str))
          throw new FormatException(value);
      }
    }

    protected override int[] GetIndices(string value)
    {
      return Code128C.GetIndices(value, 0, value.Length);
    }

    internal static int GetSwitch(string value, int start, int final)
    {
      int startIndex;
      int num;
      for (startIndex = start; startIndex < final; startIndex = num)
      {
        int length = !char.IsDigit(value[startIndex]) ? 1 : 2;
        num = startIndex + length;
        if (num > final)
          return startIndex;
        string str = value.Substring(startIndex, length);
        if (!Code128C.Codeset.Contains(str))
          return startIndex;
      }
      return startIndex;
    }

    internal static int[] GetIndices(string value, int start, int final)
    {
      List<int> intList = new List<int>();
      if (start > 0)
        intList.Add(Code128C.Switch);
      else
        intList.Add(Code128C.Prefix);
      int num;
      for (int startIndex = start; startIndex < final; startIndex = num)
      {
        int length = !char.IsDigit(value[startIndex]) ? 1 : 2;
        num = startIndex + length;
        if (num <= final)
        {
          string str = value.Substring(startIndex, length);
          intList.Add(Code128C.Codeset.IndexOf(str));
        }
      }
      return intList.ToArray();
    }
  }
}
