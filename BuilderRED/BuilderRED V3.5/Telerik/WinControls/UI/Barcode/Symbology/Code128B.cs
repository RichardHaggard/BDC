// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code128B
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code128B : Code128
  {
    private static readonly int Switch = 100;
    private static readonly int Prefix = 104;
    private static readonly IList<char> Charset = (IList<char>) new List<char>();

    static Code128B()
    {
      Code128B.Charset.Add(' ');
      Code128B.Charset.Add('!');
      Code128B.Charset.Add('"');
      Code128B.Charset.Add('#');
      Code128B.Charset.Add('$');
      Code128B.Charset.Add('%');
      Code128B.Charset.Add('&');
      Code128B.Charset.Add('\'');
      Code128B.Charset.Add('(');
      Code128B.Charset.Add(')');
      Code128B.Charset.Add('*');
      Code128B.Charset.Add('+');
      Code128B.Charset.Add(',');
      Code128B.Charset.Add('-');
      Code128B.Charset.Add('.');
      Code128B.Charset.Add('/');
      Code128B.Charset.Add('0');
      Code128B.Charset.Add('1');
      Code128B.Charset.Add('2');
      Code128B.Charset.Add('3');
      Code128B.Charset.Add('4');
      Code128B.Charset.Add('5');
      Code128B.Charset.Add('6');
      Code128B.Charset.Add('7');
      Code128B.Charset.Add('8');
      Code128B.Charset.Add('9');
      Code128B.Charset.Add(':');
      Code128B.Charset.Add(';');
      Code128B.Charset.Add('<');
      Code128B.Charset.Add('=');
      Code128B.Charset.Add('>');
      Code128B.Charset.Add('?');
      Code128B.Charset.Add('@');
      Code128B.Charset.Add('A');
      Code128B.Charset.Add('B');
      Code128B.Charset.Add('C');
      Code128B.Charset.Add('D');
      Code128B.Charset.Add('E');
      Code128B.Charset.Add('F');
      Code128B.Charset.Add('G');
      Code128B.Charset.Add('H');
      Code128B.Charset.Add('I');
      Code128B.Charset.Add('J');
      Code128B.Charset.Add('K');
      Code128B.Charset.Add('L');
      Code128B.Charset.Add('M');
      Code128B.Charset.Add('N');
      Code128B.Charset.Add('O');
      Code128B.Charset.Add('P');
      Code128B.Charset.Add('Q');
      Code128B.Charset.Add('R');
      Code128B.Charset.Add('S');
      Code128B.Charset.Add('T');
      Code128B.Charset.Add('U');
      Code128B.Charset.Add('V');
      Code128B.Charset.Add('W');
      Code128B.Charset.Add('X');
      Code128B.Charset.Add('Y');
      Code128B.Charset.Add('Z');
      Code128B.Charset.Add('[');
      Code128B.Charset.Add('\\');
      Code128B.Charset.Add(']');
      Code128B.Charset.Add('^');
      Code128B.Charset.Add('_');
      Code128B.Charset.Add('`');
      Code128B.Charset.Add('a');
      Code128B.Charset.Add('b');
      Code128B.Charset.Add('c');
      Code128B.Charset.Add('d');
      Code128B.Charset.Add('e');
      Code128B.Charset.Add('f');
      Code128B.Charset.Add('g');
      Code128B.Charset.Add('h');
      Code128B.Charset.Add('i');
      Code128B.Charset.Add('j');
      Code128B.Charset.Add('k');
      Code128B.Charset.Add('l');
      Code128B.Charset.Add('m');
      Code128B.Charset.Add('n');
      Code128B.Charset.Add('o');
      Code128B.Charset.Add('p');
      Code128B.Charset.Add('q');
      Code128B.Charset.Add('r');
      Code128B.Charset.Add('s');
      Code128B.Charset.Add('t');
      Code128B.Charset.Add('u');
      Code128B.Charset.Add('v');
      Code128B.Charset.Add('w');
      Code128B.Charset.Add('x');
      Code128B.Charset.Add('y');
      Code128B.Charset.Add('z');
      Code128B.Charset.Add('{');
      Code128B.Charset.Add('|');
      Code128B.Charset.Add('}');
      Code128B.Charset.Add('~');
      Code128B.Charset.Add('\x007F');
      Code128B.Charset.Add('ù');
      Code128B.Charset.Add('ø');
      Code128B.Charset.Add('û');
      Code128B.Charset.Add('ö');
      Code128B.Charset.Add('ú');
      Code128B.Charset.Add('ô');
      Code128B.Charset.Add('÷');
      Code128B.Charset.Add('ü');
      Code128B.Charset.Add('ý');
      Code128B.Charset.Add('þ');
      Code128B.Charset.Add('ÿ');
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code128B.Charset.Contains(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    protected override int[] GetIndices(string value)
    {
      return Code128B.GetIndices(value, 0, value.Length);
    }

    internal static int GetSwitch(string value, int start, int final)
    {
      for (int start1 = start; start1 < final; ++start1)
      {
        if (!Code128B.Charset.Contains(value[start1]) || Code128C.GetSwitch(value, start1, final) > start1)
          return start1;
      }
      return final;
    }

    internal static int[] GetIndices(string value, int start, int final)
    {
      List<int> intList = new List<int>();
      if (start > 0)
        intList.Add(Code128B.Switch);
      else
        intList.Add(Code128B.Prefix);
      for (int index = start; index < final; ++index)
        intList.Add(Code128B.Charset.IndexOf(value[index]));
      return intList.ToArray();
    }
  }
}
