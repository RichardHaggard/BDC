// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.TextModeDefinitionEntry
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class TextModeDefinitionEntry
  {
    public int TypeIndex;
    public int EntryValue;
    public int RowIndex;

    public TextModeDefinitionEntry(int asciiValue, int group, int rowIndex)
    {
      this.EntryValue = asciiValue;
      this.TypeIndex = group;
      this.RowIndex = rowIndex;
    }
  }
}
