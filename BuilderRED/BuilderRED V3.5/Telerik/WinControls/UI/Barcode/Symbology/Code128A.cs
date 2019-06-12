// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code128A
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code128A : Code128
  {
    private static readonly int Switch = 101;
    private static readonly int Prefix = 103;
    private static readonly IList<char> Charset = (IList<char>) new List<char>();

    static Code128A()
    {
      Code128A.Charset.Add(' ');
      Code128A.Charset.Add('!');
      Code128A.Charset.Add('"');
      Code128A.Charset.Add('#');
      Code128A.Charset.Add('$');
      Code128A.Charset.Add('%');
      Code128A.Charset.Add('&');
      Code128A.Charset.Add('\'');
      Code128A.Charset.Add('(');
      Code128A.Charset.Add(')');
      Code128A.Charset.Add('*');
      Code128A.Charset.Add('+');
      Code128A.Charset.Add(',');
      Code128A.Charset.Add('-');
      Code128A.Charset.Add('.');
      Code128A.Charset.Add('/');
      Code128A.Charset.Add('0');
      Code128A.Charset.Add('1');
      Code128A.Charset.Add('2');
      Code128A.Charset.Add('3');
      Code128A.Charset.Add('4');
      Code128A.Charset.Add('5');
      Code128A.Charset.Add('6');
      Code128A.Charset.Add('7');
      Code128A.Charset.Add('8');
      Code128A.Charset.Add('9');
      Code128A.Charset.Add(':');
      Code128A.Charset.Add(';');
      Code128A.Charset.Add('<');
      Code128A.Charset.Add('=');
      Code128A.Charset.Add('>');
      Code128A.Charset.Add('?');
      Code128A.Charset.Add('@');
      Code128A.Charset.Add('A');
      Code128A.Charset.Add('B');
      Code128A.Charset.Add('C');
      Code128A.Charset.Add('D');
      Code128A.Charset.Add('E');
      Code128A.Charset.Add('F');
      Code128A.Charset.Add('G');
      Code128A.Charset.Add('H');
      Code128A.Charset.Add('I');
      Code128A.Charset.Add('J');
      Code128A.Charset.Add('K');
      Code128A.Charset.Add('L');
      Code128A.Charset.Add('M');
      Code128A.Charset.Add('N');
      Code128A.Charset.Add('O');
      Code128A.Charset.Add('P');
      Code128A.Charset.Add('Q');
      Code128A.Charset.Add('R');
      Code128A.Charset.Add('S');
      Code128A.Charset.Add('T');
      Code128A.Charset.Add('U');
      Code128A.Charset.Add('V');
      Code128A.Charset.Add('W');
      Code128A.Charset.Add('X');
      Code128A.Charset.Add('Y');
      Code128A.Charset.Add('Z');
      Code128A.Charset.Add('[');
      Code128A.Charset.Add('\\');
      Code128A.Charset.Add(']');
      Code128A.Charset.Add('^');
      Code128A.Charset.Add('_');
      Code128A.Charset.Add(char.MinValue);
      Code128A.Charset.Add('\x0001');
      Code128A.Charset.Add('\x0002');
      Code128A.Charset.Add('\x0003');
      Code128A.Charset.Add('\x0004');
      Code128A.Charset.Add('\x0005');
      Code128A.Charset.Add('\x0006');
      Code128A.Charset.Add('\a');
      Code128A.Charset.Add('\b');
      Code128A.Charset.Add('\t');
      Code128A.Charset.Add('\n');
      Code128A.Charset.Add('\v');
      Code128A.Charset.Add('\f');
      Code128A.Charset.Add('\r');
      Code128A.Charset.Add('\x000E');
      Code128A.Charset.Add('\x000F');
      Code128A.Charset.Add('\x0010');
      Code128A.Charset.Add('\x0011');
      Code128A.Charset.Add('\x0012');
      Code128A.Charset.Add('\x0013');
      Code128A.Charset.Add('\x0014');
      Code128A.Charset.Add('\x0015');
      Code128A.Charset.Add('\x0016');
      Code128A.Charset.Add('\x0017');
      Code128A.Charset.Add('\x0018');
      Code128A.Charset.Add('\x0019');
      Code128A.Charset.Add('\x001A');
      Code128A.Charset.Add('\x001B');
      Code128A.Charset.Add('\x001C');
      Code128A.Charset.Add('\x001D');
      Code128A.Charset.Add('\x001E');
      Code128A.Charset.Add('\x001F');
      Code128A.Charset.Add('ù');
      Code128A.Charset.Add('ø');
      Code128A.Charset.Add('û');
      Code128A.Charset.Add('ö');
      Code128A.Charset.Add('õ');
      Code128A.Charset.Add('ú');
      Code128A.Charset.Add('÷');
      Code128A.Charset.Add('ü');
      Code128A.Charset.Add('ý');
      Code128A.Charset.Add('þ');
      Code128A.Charset.Add('ÿ');
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code128A.Charset.Contains(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    protected override int[] GetIndices(string value)
    {
      return Code128A.GetIndices(value, 0, value.Length);
    }

    internal static int GetSwitch(string value, int start, int final)
    {
      for (int start1 = start; start1 < final; ++start1)
      {
        if (!Code128A.Charset.Contains(value[start1]) || Code128C.GetSwitch(value, start1, final) > start1)
          return start1;
      }
      return final;
    }

    internal static int[] GetIndices(string value, int start, int final)
    {
      List<int> intList = new List<int>();
      if (start > 0)
        intList.Add(Code128A.Switch);
      else
        intList.Add(Code128A.Prefix);
      for (int index = start; index < final; ++index)
        intList.Add(Code128A.Charset.IndexOf(value[index]));
      return intList.ToArray();
    }
  }
}
