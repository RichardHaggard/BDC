// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LineInfoBlockIndexComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class LineInfoBlockIndexComparer : IComparer<LineInfo>
  {
    private int blockIndex;

    public LineInfoBlockIndexComparer(int blockIndex)
    {
      this.blockIndex = blockIndex;
    }

    public int Compare(LineInfo line, LineInfo nullLine)
    {
      ITextBlock startBlock = line.StartBlock;
      ITextBlock endBlock = line.EndBlock;
      if (startBlock.Index <= this.blockIndex && this.blockIndex <= endBlock.Index)
        return 0;
      return this.blockIndex < startBlock.Index ? 1 : -1;
    }
  }
}
