// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBlockOffsetComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;

namespace Telerik.WinControls.UI
{
  public class TextBlockOffsetComparer : IComparer
  {
    private int offset;

    public TextBlockOffsetComparer(int offset)
    {
      this.offset = offset;
    }

    public int Compare(object x, object nullObject)
    {
      ITextBlock textBlock = x as ITextBlock;
      int offset = textBlock.Offset;
      int num = offset + textBlock.Length;
      if (offset <= this.offset && this.offset <= num)
        return 0;
      return this.offset < offset ? 1 : -1;
    }
  }
}
