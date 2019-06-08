// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LineInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class LineInfo
  {
    private ITextBlock startTextBlock;
    private ITextBlock endTextBlock;
    private SizeF size;

    public LineInfo()
      : this((ITextBlock) null, (ITextBlock) null, SizeF.Empty)
    {
    }

    public LineInfo(ITextBlock startBlock, ITextBlock endBlock, SizeF size)
    {
      this.startTextBlock = startBlock;
      this.endTextBlock = endBlock;
      this.size = size;
    }

    public ITextBlock StartBlock
    {
      get
      {
        return this.startTextBlock;
      }
      set
      {
        this.startTextBlock = value;
      }
    }

    public ITextBlock EndBlock
    {
      get
      {
        return this.endTextBlock;
      }
      set
      {
        this.endTextBlock = value;
      }
    }

    public PointF Location
    {
      get
      {
        if (this.startTextBlock == null)
          return PointF.Empty;
        ITextBlock textBlock = this.startTextBlock;
        if (TextBoxWrapPanel.IsWhitespace(this.startTextBlock.Text) && !TextBoxWrapPanel.IsTab(this.startTextBlock.Text))
          textBlock = this.endTextBlock;
        RectangleF boundingRectangle = (RectangleF) textBlock.ControlBoundingRectangle;
        PointF location = boundingRectangle.Location;
        if ((double) boundingRectangle.Height < (double) this.size.Height)
          location.Y = boundingRectangle.Bottom - this.size.Height;
        return location;
      }
    }

    public SizeF Size
    {
      get
      {
        return this.size;
      }
      set
      {
        if (!(value != this.size))
          return;
        this.size = value;
      }
    }

    public RectangleF ControlBoundingRectangle
    {
      get
      {
        return new RectangleF(this.Location, this.size);
      }
    }

    public override string ToString()
    {
      return string.Format("StartOffset: {0} EndOffset: {1}", (object) this.startTextBlock.Offset, (object) (this.endTextBlock.Offset + this.endTextBlock.Length));
    }
  }
}
