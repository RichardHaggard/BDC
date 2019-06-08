// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Code128
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Code128 : Symbology1D
  {
    private static readonly int suffix = 106;
    private static readonly IList<string> encoding = (IList<string>) new List<string>() { "11011001100", "11001101100", "11001100110", "10010011000", "10010001100", "10001001100", "10011001000", "10011000100", "10001100100", "11001001000", "11001000100", "11000100100", "10110011100", "10011011100", "10011001110", "10111001100", "10011101100", "10011100110", "11001110010", "11001011100", "11001001110", "11011100100", "11001110100", "11101101110", "11101001100", "11100101100", "11100100110", "11101100100", "11100110100", "11100110010", "11011011000", "11011000110", "11000110110", "10100011000", "10001011000", "10001000110", "10110001000", "10001101000", "10001100010", "11010001000", "11000101000", "11000100010", "10110111000", "10110001110", "10001101110", "10111011000", "10111000110", "10001110110", "11101110110", "11010001110", "11000101110", "11011101000", "11011100010", "11011101110", "11101011000", "11101000110", "11100010110", "11101101000", "11101100010", "11100011010", "11101111010", "11001000010", "11110001010", "10100110000", "10100001100", "10010110000", "10010000110", "10000101100", "10000100110", "10110010000", "10110000100", "10011010000", "10011000010", "10000110100", "10000110010", "11000010010", "11001010000", "11110111010", "11000010100", "10001111010", "10100111100", "10010111100", "10010011110", "10111100100", "10011110100", "10011110010", "11110100100", "11110010100", "11110010010", "11011011110", "11011110110", "11110110110", "10101111000", "10100011110", "10001011110", "10111101000", "10111100010", "11110101000", "11110100010", "10111011110", "10111101110", "11101011110", "11110101110", "11010000100", "11010010000", "11010011100", "11000111010" };

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (!Code128.IsValid(symbol))
          throw new InvalidSymbolException(symbol);
      }
    }

    private static bool IsValid(char symbol)
    {
      if (!Code128.IsNormal(symbol))
        return Code128.IsSpecial(symbol);
      return true;
    }

    private static bool IsNormal(char symbol)
    {
      return symbol <= '\x007F';
    }

    private static bool IsSpecial(char symbol)
    {
      if (symbol >= 'ô')
        return symbol <= 'ÿ';
      return false;
    }

    protected override string GetFormat(string value)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        char symbol = value[index];
        if (Code128.IsNormal(symbol))
          stringBuilder.Append(symbol);
      }
      return stringBuilder.ToString();
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      int[] indices = this.GetIndices(value);
      List<int> intList = new List<int>((IEnumerable<int>) indices);
      if (this.Checksum)
        intList.Add(Code128.GetChecksum(indices));
      intList.Add(Code128.suffix);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int index in intList)
        stringBuilder.Append(Code128.encoding[index]);
      stringBuilder.Append(Symbology1D.BarChar);
      stringBuilder.Append(Symbology1D.BarChar);
      return stringBuilder.ToString();
    }

    protected virtual int[] GetIndices(string value)
    {
      return Code128.GetIndices(value, 0, value.Length);
    }

    private static int[] GetIndices(string value, int start, int final)
    {
      List<int> intList = new List<int>();
      int final1;
      for (; start < final; start = final1)
      {
        final1 = Code128C.GetSwitch(value, start, final);
        if (final1 > start)
        {
          intList.AddRange((IEnumerable<int>) Code128C.GetIndices(value, start, final1));
        }
        else
        {
          int val1 = Code128A.GetSwitch(value, start, final);
          int val2 = Code128B.GetSwitch(value, start, final);
          final1 = Math.Max(val1, val2);
          if (val1 >= val2)
            intList.AddRange((IEnumerable<int>) Code128A.GetIndices(value, start, final1));
          else
            intList.AddRange((IEnumerable<int>) Code128B.GetIndices(value, start, final1));
        }
      }
      return intList.ToArray();
    }

    private static int GetChecksum(int[] array)
    {
      return Code128.GetChecksum(array, 103);
    }

    private static int GetChecksum(int[] array, int modulo)
    {
      int num = array[0];
      for (int index = 1; index < array.Length; ++index)
        num += array[index] * index;
      return num % modulo;
    }
  }
}
