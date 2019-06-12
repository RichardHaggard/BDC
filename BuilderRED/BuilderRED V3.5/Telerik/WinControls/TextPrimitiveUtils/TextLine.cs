// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TextPrimitiveUtils.TextLine
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.TextPrimitiveUtils
{
  public class TextLine
  {
    private System.Collections.Generic.List<FormattedText> list;
    private SizeF lineSize;
    private float baseLine;

    public TextLine()
    {
      this.list = new System.Collections.Generic.List<FormattedText>();
      this.lineSize = SizeF.Empty;
    }

    public SizeF LineSize
    {
      get
      {
        return this.lineSize;
      }
      set
      {
        this.lineSize = value;
      }
    }

    public float BaseLine
    {
      get
      {
        return this.baseLine;
      }
      set
      {
        this.baseLine = value;
      }
    }

    public System.Collections.Generic.List<FormattedText> List
    {
      get
      {
        return this.list;
      }
      set
      {
        this.list = value;
      }
    }

    public ContentAlignment GetLineAligment()
    {
      if (this.list != null && this.list.Count > 0)
        return this.list[0].ContentAlignment;
      return ContentAlignment.MiddleLeft;
    }

    public float GetLineOffset()
    {
      if (this.list != null && this.list.Count > 0)
        return this.list[0].OffsetSize;
      return 0.0f;
    }
  }
}
