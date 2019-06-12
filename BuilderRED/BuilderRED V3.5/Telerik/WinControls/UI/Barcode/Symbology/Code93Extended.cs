// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code93Extended
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code93Extended : Code93
  {
    private static readonly IDictionary<char, string> Encoding = (IDictionary<char, string>) new Dictionary<char, string>();

    static Code93Extended()
    {
      Code93Extended.Encoding.Add(char.MinValue, "~U");
      Code93Extended.Encoding.Add('\x0001', "@A");
      Code93Extended.Encoding.Add('\x0002', "@B");
      Code93Extended.Encoding.Add('\x0003', "@C");
      Code93Extended.Encoding.Add('\x0004', "@D");
      Code93Extended.Encoding.Add('\x0005', "@E");
      Code93Extended.Encoding.Add('\x0006', "@F");
      Code93Extended.Encoding.Add('\a', "@G");
      Code93Extended.Encoding.Add('\b', "@H");
      Code93Extended.Encoding.Add('\t', "@I");
      Code93Extended.Encoding.Add('\n', "@J");
      Code93Extended.Encoding.Add('\v', "@K");
      Code93Extended.Encoding.Add('\f', "@L");
      Code93Extended.Encoding.Add('\r', "@M");
      Code93Extended.Encoding.Add('\x000E', "@N");
      Code93Extended.Encoding.Add('\x000F', "@O");
      Code93Extended.Encoding.Add('\x0010', "@P");
      Code93Extended.Encoding.Add('\x0011', "@Q");
      Code93Extended.Encoding.Add('\x0012', "@R");
      Code93Extended.Encoding.Add('\x0013', "@S");
      Code93Extended.Encoding.Add('\x0014', "@T");
      Code93Extended.Encoding.Add('\x0015', "@U");
      Code93Extended.Encoding.Add('\x0016', "@V");
      Code93Extended.Encoding.Add('\x0017', "@W");
      Code93Extended.Encoding.Add('\x0018', "@X");
      Code93Extended.Encoding.Add('\x0019', "@Y");
      Code93Extended.Encoding.Add('\x001A', "@Z");
      Code93Extended.Encoding.Add('\x001B', "~A");
      Code93Extended.Encoding.Add('\x001C', "~B");
      Code93Extended.Encoding.Add('\x001D', "~C");
      Code93Extended.Encoding.Add('\x001E', "~D");
      Code93Extended.Encoding.Add('\x001F', "~E");
      Code93Extended.Encoding.Add(' ', " ");
      Code93Extended.Encoding.Add('!', "#A");
      Code93Extended.Encoding.Add('"', "#B");
      Code93Extended.Encoding.Add('#', "#C");
      Code93Extended.Encoding.Add('$', "#D");
      Code93Extended.Encoding.Add('%', "#E");
      Code93Extended.Encoding.Add('&', "#F");
      Code93Extended.Encoding.Add('\'', "#G");
      Code93Extended.Encoding.Add('(', "#H");
      Code93Extended.Encoding.Add(')', "#I");
      Code93Extended.Encoding.Add('*', "#J");
      Code93Extended.Encoding.Add('+', "#K");
      Code93Extended.Encoding.Add(',', "#L");
      Code93Extended.Encoding.Add('-', "-");
      Code93Extended.Encoding.Add('.', ".");
      Code93Extended.Encoding.Add('/', "#O");
      Code93Extended.Encoding.Add('0', "0");
      Code93Extended.Encoding.Add('1', "1");
      Code93Extended.Encoding.Add('2', "2");
      Code93Extended.Encoding.Add('3', "3");
      Code93Extended.Encoding.Add('4', "4");
      Code93Extended.Encoding.Add('5', "5");
      Code93Extended.Encoding.Add('6', "6");
      Code93Extended.Encoding.Add('7', "7");
      Code93Extended.Encoding.Add('8', "8");
      Code93Extended.Encoding.Add('9', "9");
      Code93Extended.Encoding.Add(':', "#Z");
      Code93Extended.Encoding.Add(';', "~F");
      Code93Extended.Encoding.Add('<', "~G");
      Code93Extended.Encoding.Add('=', "~H");
      Code93Extended.Encoding.Add('>', "~I");
      Code93Extended.Encoding.Add('?', "~J");
      Code93Extended.Encoding.Add('@', "~V");
      Code93Extended.Encoding.Add('A', "A");
      Code93Extended.Encoding.Add('B', "B");
      Code93Extended.Encoding.Add('C', "C");
      Code93Extended.Encoding.Add('D', "D");
      Code93Extended.Encoding.Add('E', "E");
      Code93Extended.Encoding.Add('F', "F");
      Code93Extended.Encoding.Add('G', "G");
      Code93Extended.Encoding.Add('H', "H");
      Code93Extended.Encoding.Add('I', "I");
      Code93Extended.Encoding.Add('J', "J");
      Code93Extended.Encoding.Add('K', "K");
      Code93Extended.Encoding.Add('L', "L");
      Code93Extended.Encoding.Add('M', "M");
      Code93Extended.Encoding.Add('N', "N");
      Code93Extended.Encoding.Add('O', "O");
      Code93Extended.Encoding.Add('P', "P");
      Code93Extended.Encoding.Add('Q', "Q");
      Code93Extended.Encoding.Add('R', "R");
      Code93Extended.Encoding.Add('S', "S");
      Code93Extended.Encoding.Add('T', "T");
      Code93Extended.Encoding.Add('U', "U");
      Code93Extended.Encoding.Add('V', "V");
      Code93Extended.Encoding.Add('W', "W");
      Code93Extended.Encoding.Add('X', "X");
      Code93Extended.Encoding.Add('Y', "Y");
      Code93Extended.Encoding.Add('Z', "Z");
      Code93Extended.Encoding.Add('[', "~K");
      Code93Extended.Encoding.Add('\\', "~L");
      Code93Extended.Encoding.Add(']', "~M");
      Code93Extended.Encoding.Add('^', "~N");
      Code93Extended.Encoding.Add('_', "~O");
      Code93Extended.Encoding.Add('`', "~W");
      Code93Extended.Encoding.Add('a', "&A");
      Code93Extended.Encoding.Add('b', "&B");
      Code93Extended.Encoding.Add('c', "&C");
      Code93Extended.Encoding.Add('d', "&D");
      Code93Extended.Encoding.Add('e', "&E");
      Code93Extended.Encoding.Add('f', "&F");
      Code93Extended.Encoding.Add('g', "&G");
      Code93Extended.Encoding.Add('h', "&H");
      Code93Extended.Encoding.Add('i', "&I");
      Code93Extended.Encoding.Add('j', "&J");
      Code93Extended.Encoding.Add('k', "&K");
      Code93Extended.Encoding.Add('l', "&L");
      Code93Extended.Encoding.Add('m', "&M");
      Code93Extended.Encoding.Add('n', "&N");
      Code93Extended.Encoding.Add('o', "&O");
      Code93Extended.Encoding.Add('p', "&P");
      Code93Extended.Encoding.Add('q', "&Q");
      Code93Extended.Encoding.Add('r', "&R");
      Code93Extended.Encoding.Add('s', "&S");
      Code93Extended.Encoding.Add('t', "&T");
      Code93Extended.Encoding.Add('u', "&U");
      Code93Extended.Encoding.Add('v', "&V");
      Code93Extended.Encoding.Add('w', "&W");
      Code93Extended.Encoding.Add('x', "&X");
      Code93Extended.Encoding.Add('y', "&Y");
      Code93Extended.Encoding.Add('z', "&Z");
      Code93Extended.Encoding.Add('{', "~P");
      Code93Extended.Encoding.Add('|', "~Q");
      Code93Extended.Encoding.Add('}', "~R");
      Code93Extended.Encoding.Add('~', "~S");
      Code93Extended.Encoding.Add('\x007F', "~T");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code93Extended.IsValid(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    private static bool IsValid(char symbol)
    {
      return symbol <= '\x007F';
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
        stringBuilder.Append(Code93Extended.Encoding[value[index]]);
      return base.GetEncoding(stringBuilder.ToString());
    }
  }
}
