// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LineInfoOffsetComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class LineInfoOffsetComparer : IComparer<LineInfo>
  {
    private int offset;

    public LineInfoOffsetComparer(int offset)
    {
      this.offset = offset;
    }

    public int Compare(LineInfo line, LineInfo nullLine)
    {
      ITextBlock startBlock = line.StartBlock;
      ITextBlock endBlock = line.EndBlock;
      if (startBlock.Offset <= this.offset && this.offset <= endBlock.Offset + endBlock.Length)
        return 0;
      return this.offset < startBlock.Offset ? 1 : -1;
    }
  }
}
