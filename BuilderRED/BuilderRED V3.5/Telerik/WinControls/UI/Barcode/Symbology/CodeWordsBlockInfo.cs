// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.CodeWordsBlockInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class CodeWordsBlockInfo
  {
    public int CodeWordsPerBlock { get; set; }

    public int FirstBlockCount { get; set; }

    public int FirstDataCodeWords { get; set; }

    public int SecondBlockCount { get; set; }

    public int SecondBlockCodeWords { get; set; }

    public CodeWordsBlockInfo(
      int codeWordsPerBlock,
      int firstBlockCount,
      int firstDataCodeWords,
      int secondBlockCount,
      int secondBlockCodeWords)
    {
      this.CodeWordsPerBlock = codeWordsPerBlock;
      this.FirstBlockCount = firstBlockCount;
      this.FirstDataCodeWords = firstDataCodeWords;
      this.SecondBlockCount = secondBlockCount;
      this.SecondBlockCodeWords = secondBlockCodeWords;
    }
  }
}
