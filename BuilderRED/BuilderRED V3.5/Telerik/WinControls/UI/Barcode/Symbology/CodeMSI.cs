// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.CodeMSI
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class CodeMSI : Symbology1D
  {
    private static readonly string Prefix = "[";
    private static readonly string Suffix = "]";
    private static readonly IList<char> Charset = (IList<char>) new List<char>();
    private static readonly IDictionary<char, string> Encoding;
    private CheckMSI algorithm;

    static CodeMSI()
    {
      CodeMSI.Charset.Add('0');
      CodeMSI.Charset.Add('1');
      CodeMSI.Charset.Add('2');
      CodeMSI.Charset.Add('3');
      CodeMSI.Charset.Add('4');
      CodeMSI.Charset.Add('5');
      CodeMSI.Charset.Add('6');
      CodeMSI.Charset.Add('7');
      CodeMSI.Charset.Add('8');
      CodeMSI.Charset.Add('9');
      CodeMSI.Encoding = (IDictionary<char, string>) new Dictionary<char, string>();
      CodeMSI.Encoding.Add('0', "100100100100");
      CodeMSI.Encoding.Add('1', "100100100110");
      CodeMSI.Encoding.Add('2', "100100110100");
      CodeMSI.Encoding.Add('3', "100100110110");
      CodeMSI.Encoding.Add('4', "100110100100");
      CodeMSI.Encoding.Add('5', "100110100110");
      CodeMSI.Encoding.Add('6', "100110110100");
      CodeMSI.Encoding.Add('7', "100110110110");
      CodeMSI.Encoding.Add('8', "110100100100");
      CodeMSI.Encoding.Add('9', "110100100110");
      CodeMSI.Encoding.Add('[', "110");
      CodeMSI.Encoding.Add(']', "1001");
    }

    public CheckMSI Algorithm
    {
      get
      {
        return this.algorithm;
      }
      set
      {
        this.algorithm = value;
      }
    }

    protected override void ValidateValue(string value)
    {
      if (string.IsNullOrEmpty(value))
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        char ch = value[index];
        if (!char.IsDigit(ch))
          throw new InvalidSymbolException(ch);
      }
    }

    protected override string GetEncoding(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (this.Checksum)
        value += this.GetChecksum(value);
      if (!value.StartsWith(CodeMSI.Prefix))
        value = CodeMSI.Prefix + value;
      if (!value.EndsWith(CodeMSI.Suffix))
        value += CodeMSI.Suffix;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
        stringBuilder.Append(CodeMSI.Encoding[value[index]]);
      return stringBuilder.ToString();
    }

    private string GetChecksum(string value)
    {
      int length = value.Length;
      switch (this.algorithm)
      {
        case CheckMSI.Modulo10:
          value += (string) (object) CodeMSI.GetChecksum(value, 10);
          break;
        case CheckMSI.Modulo11:
          value += (string) (object) CodeMSI.GetChecksum(value, 7, 11);
          break;
        case CheckMSI.Modulo1010:
          value += (string) (object) CodeMSI.GetChecksum(value, 10);
          value += (string) (object) CodeMSI.GetChecksum(value, 10);
          break;
        case CheckMSI.Modulo1110:
          value += (string) (object) CodeMSI.GetChecksum(value, 7, 11);
          value += (string) (object) CodeMSI.GetChecksum(value, 10);
          break;
      }
      return value.Substring(length);
    }

    private static char GetChecksum(string value, int modulo)
    {
      int num1 = 0;
      int num2 = 0;
      for (int index = value.Length - 1; index >= 0; --index)
      {
        int num3 = ((int) value[index] - 48) * (++num2 % 2 + 1);
        int num4 = num3 % 10 + num3 / 10;
        num1 += num4;
      }
      return CodeMSI.Charset[num1 * (modulo - 1) % modulo];
    }

    private static char GetChecksum(string value, int length, int modulo)
    {
      int num1 = 0;
      int num2 = 2;
      for (int index = value.Length - 1; index >= 0; --index)
      {
        int num3 = CodeMSI.Charset.IndexOf(value[index]);
        num1 += num3 * num2++;
        if (num2 > length)
          num2 = 2;
      }
      int index1 = (11 - num1 % modulo) % modulo;
      return CodeMSI.Charset[index1];
    }
  }
}
