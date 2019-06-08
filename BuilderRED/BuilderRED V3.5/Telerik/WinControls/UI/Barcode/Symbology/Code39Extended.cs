// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code39Extended
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code39Extended : Code39
  {
    private static readonly IDictionary<char, string> Encoding = (IDictionary<char, string>) new Dictionary<char, string>();

    static Code39Extended()
    {
      Code39Extended.Encoding.Add(char.MinValue, "%U");
      Code39Extended.Encoding.Add('\x0001', "$A");
      Code39Extended.Encoding.Add('\x0002', "$B");
      Code39Extended.Encoding.Add('\x0003', "$C");
      Code39Extended.Encoding.Add('\x0004', "$D");
      Code39Extended.Encoding.Add('\x0005', "$E");
      Code39Extended.Encoding.Add('\x0006', "$F");
      Code39Extended.Encoding.Add('\a', "$G");
      Code39Extended.Encoding.Add('\b', "$H");
      Code39Extended.Encoding.Add('\t', "$I");
      Code39Extended.Encoding.Add('\n', "$J");
      Code39Extended.Encoding.Add('\v', "$K");
      Code39Extended.Encoding.Add('\f', "$L");
      Code39Extended.Encoding.Add('\r', "$M");
      Code39Extended.Encoding.Add('\x000E', "$N");
      Code39Extended.Encoding.Add('\x000F', "$O");
      Code39Extended.Encoding.Add('\x0010', "$P");
      Code39Extended.Encoding.Add('\x0011', "$Q");
      Code39Extended.Encoding.Add('\x0012', "$R");
      Code39Extended.Encoding.Add('\x0013', "$S");
      Code39Extended.Encoding.Add('\x0014', "$T");
      Code39Extended.Encoding.Add('\x0015', "$U");
      Code39Extended.Encoding.Add('\x0016', "$V");
      Code39Extended.Encoding.Add('\x0017', "$W");
      Code39Extended.Encoding.Add('\x0018', "$X");
      Code39Extended.Encoding.Add('\x0019', "$Y");
      Code39Extended.Encoding.Add('\x001A', "$Z");
      Code39Extended.Encoding.Add('\x001B', "%A");
      Code39Extended.Encoding.Add('\x001C', "%B");
      Code39Extended.Encoding.Add('\x001D', "%C");
      Code39Extended.Encoding.Add('\x001E', "%D");
      Code39Extended.Encoding.Add('\x001F', "%E");
      Code39Extended.Encoding.Add(' ', " ");
      Code39Extended.Encoding.Add('!', "/A");
      Code39Extended.Encoding.Add('"', "/B");
      Code39Extended.Encoding.Add('#', "/C");
      Code39Extended.Encoding.Add('$', "/D");
      Code39Extended.Encoding.Add('%', "/E");
      Code39Extended.Encoding.Add('&', "/F");
      Code39Extended.Encoding.Add('\'', "/G");
      Code39Extended.Encoding.Add('(', "/H");
      Code39Extended.Encoding.Add(')', "/I");
      Code39Extended.Encoding.Add('*', "/J");
      Code39Extended.Encoding.Add('+', "/K");
      Code39Extended.Encoding.Add(',', "/L");
      Code39Extended.Encoding.Add('-', "-");
      Code39Extended.Encoding.Add('.', ".");
      Code39Extended.Encoding.Add('/', "/O");
      Code39Extended.Encoding.Add('0', "0");
      Code39Extended.Encoding.Add('1', "1");
      Code39Extended.Encoding.Add('2', "2");
      Code39Extended.Encoding.Add('3', "3");
      Code39Extended.Encoding.Add('4', "4");
      Code39Extended.Encoding.Add('5', "5");
      Code39Extended.Encoding.Add('6', "6");
      Code39Extended.Encoding.Add('7', "7");
      Code39Extended.Encoding.Add('8', "8");
      Code39Extended.Encoding.Add('9', "9");
      Code39Extended.Encoding.Add(':', "/Z");
      Code39Extended.Encoding.Add(';', "%F");
      Code39Extended.Encoding.Add('<', "%G");
      Code39Extended.Encoding.Add('=', "%H");
      Code39Extended.Encoding.Add('>', "%I");
      Code39Extended.Encoding.Add('?', "%J");
      Code39Extended.Encoding.Add('@', "%V");
      Code39Extended.Encoding.Add('A', "A");
      Code39Extended.Encoding.Add('B', "B");
      Code39Extended.Encoding.Add('C', "C");
      Code39Extended.Encoding.Add('D', "D");
      Code39Extended.Encoding.Add('E', "E");
      Code39Extended.Encoding.Add('F', "F");
      Code39Extended.Encoding.Add('G', "G");
      Code39Extended.Encoding.Add('H', "H");
      Code39Extended.Encoding.Add('I', "I");
      Code39Extended.Encoding.Add('J', "J");
      Code39Extended.Encoding.Add('K', "K");
      Code39Extended.Encoding.Add('L', "L");
      Code39Extended.Encoding.Add('M', "M");
      Code39Extended.Encoding.Add('N', "N");
      Code39Extended.Encoding.Add('O', "O");
      Code39Extended.Encoding.Add('P', "P");
      Code39Extended.Encoding.Add('Q', "Q");
      Code39Extended.Encoding.Add('R', "R");
      Code39Extended.Encoding.Add('S', "S");
      Code39Extended.Encoding.Add('T', "T");
      Code39Extended.Encoding.Add('U', "U");
      Code39Extended.Encoding.Add('V', "V");
      Code39Extended.Encoding.Add('W', "W");
      Code39Extended.Encoding.Add('X', "X");
      Code39Extended.Encoding.Add('Y', "Y");
      Code39Extended.Encoding.Add('Z', "Z");
      Code39Extended.Encoding.Add('[', "%K");
      Code39Extended.Encoding.Add('\\', "%L");
      Code39Extended.Encoding.Add(']', "%M");
      Code39Extended.Encoding.Add('^', "%N");
      Code39Extended.Encoding.Add('_', "%O");
      Code39Extended.Encoding.Add('`', "%W");
      Code39Extended.Encoding.Add('a', "+A");
      Code39Extended.Encoding.Add('b', "+B");
      Code39Extended.Encoding.Add('c', "+C");
      Code39Extended.Encoding.Add('d', "+D");
      Code39Extended.Encoding.Add('e', "+E");
      Code39Extended.Encoding.Add('f', "+F");
      Code39Extended.Encoding.Add('g', "+G");
      Code39Extended.Encoding.Add('h', "+H");
      Code39Extended.Encoding.Add('i', "+I");
      Code39Extended.Encoding.Add('j', "+J");
      Code39Extended.Encoding.Add('k', "+K");
      Code39Extended.Encoding.Add('l', "+L");
      Code39Extended.Encoding.Add('m', "+M");
      Code39Extended.Encoding.Add('n', "+N");
      Code39Extended.Encoding.Add('o', "+O");
      Code39Extended.Encoding.Add('p', "+P");
      Code39Extended.Encoding.Add('q', "+Q");
      Code39Extended.Encoding.Add('r', "+R");
      Code39Extended.Encoding.Add('s', "+S");
      Code39Extended.Encoding.Add('t', "+T");
      Code39Extended.Encoding.Add('u', "+U");
      Code39Extended.Encoding.Add('v', "+V");
      Code39Extended.Encoding.Add('w', "+W");
      Code39Extended.Encoding.Add('x', "+X");
      Code39Extended.Encoding.Add('y', "+Y");
      Code39Extended.Encoding.Add('z', "+Z");
      Code39Extended.Encoding.Add('{', "%P");
      Code39Extended.Encoding.Add('|', "%Q");
      Code39Extended.Encoding.Add('}', "%R");
      Code39Extended.Encoding.Add('~', "%S");
      Code39Extended.Encoding.Add('\x007F', "%T");
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code39Extended.IsValid(symbol))
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
        stringBuilder.Append(Code39Extended.Encoding[value[index]]);
      return base.GetEncoding(stringBuilder.ToString());
    }
  }
}
