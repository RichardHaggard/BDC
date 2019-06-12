// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Planet
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class Planet : Symbology1D
  {
    private static readonly IList<char> Charset = (IList<char>) new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    private static readonly IDictionary<char, string> Encoding = (IDictionary<char, string>) new Dictionary<char, string>() { { '0', "00111" }, { '1', "11100" }, { '2', "11010" }, { '3', "11001" }, { '4', "10110" }, { '5', "10101" }, { '6', "10011" }, { '7', "01110" }, { '8', "01101" }, { '9', "01011" } };

    protected override void CreateBarsOverride(IElementFactory factory)
    {
      int length = this.pattern.Length;
      for (int index = 0; index < length; ++index)
      {
        RectangleF barRect = this.barRect;
        barRect.X += this.barRect.Width * (float) index / (float) length;
        barRect.Width = this.barRect.Width * 0.5f / (float) length;
        if ((int) this.pattern[index] == (int) Symbology1D.GapChar)
        {
          barRect.Y += this.barRect.Height * 0.5f;
          barRect.Height = this.barRect.Height * 0.5f;
        }
        factory.CreateBarElement(barRect);
      }
    }

    protected override float GetLength()
    {
      return base.GetLength() * 2f;
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
        value += (string) (object) Planet.GetChecksum(value);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Symbology1D.BarChar);
      for (int index = 0; index < value.Length; ++index)
        stringBuilder.Append(Planet.Encoding[value[index]]);
      stringBuilder.Append(Symbology1D.BarChar);
      return stringBuilder.ToString();
    }

    private static char GetChecksum(string value)
    {
      return Planet.GetChecksum(value, 10);
    }

    private static char GetChecksum(string value, int modulo)
    {
      int num = 0;
      for (int index = 0; index < value.Length; ++index)
        num += Planet.Charset.IndexOf(value[index]);
      int index1 = num % modulo;
      if (index1 != 0)
        index1 = modulo - index1;
      return Planet.Charset[index1];
    }
  }
}
