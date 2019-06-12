// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.TextPrimitiveHtmlImpl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.TextPrimitiveUtils;

namespace Telerik.WinControls.Primitives
{
  public class TextPrimitiveHtmlImpl : ITextPrimitive
  {
    private readonly SizeF scalingSize = new SizeF(1f, 1f);
    private FormattedTextBlock textBlock = new FormattedTextBlock();
    protected bool isDirty;
    private bool useHTMLRendering;
    private bool isMouseAlreadyPress;

    public TextPrimitiveHtmlImpl()
    {
      this.isDirty = true;
    }

    public void PaintPrimitive(
      IGraphics graphics,
      float angle,
      SizeF scale,
      TextParams textParams)
    {
      if (this.isDirty)
      {
        this.isDirty = false;
        this.TextBlock = TinyHTMLParsers.Parse(textParams);
      }
      RectangleF paintingRectangle = textParams.paintingRectangle;
      StringFormat stringFormat = textParams.CreateStringFormat();
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      TextRenderingHint textRenderingHint = underlayGraphics.TextRenderingHint;
      underlayGraphics.TextRenderingHint = textParams.textRenderingHint;
      this.TextBlock.PaintFormatTextBlock(graphics, paintingRectangle, true, stringFormat, TextFormatFlags.Default, textParams.textWrap, textParams.ClipText);
      underlayGraphics.TextRenderingHint = textRenderingHint;
      stringFormat.Dispose();
    }

    public void PaintPrimitive(IGraphics graphics, TextParams textParams)
    {
      this.PaintPrimitive(graphics, 0.0f, this.scalingSize, textParams);
    }

    public SizeF MeasureOverride(SizeF availableSize, TextParams textParams)
    {
      this.isDirty = false;
      this.TextBlock = TinyHTMLParsers.Parse(textParams);
      Size size1 = Size.Ceiling(this.TextBlock.GetTextSize(availableSize, textParams));
      Size size2 = LayoutUtils.FlipSizeIf(textParams.textOrientation == Orientation.Vertical, size1);
      Size size3 = LayoutUtils.FlipSizeIf(textParams.textOrientation == Orientation.Vertical, size1);
      size2.Width = Math.Max(size2.Width, size3.Width);
      size2.Height = Math.Max(size2.Height, size3.Height);
      return (SizeF) size2;
    }

    public bool AllowHTMLRendering()
    {
      return this.useHTMLRendering;
    }

    public FormattedTextBlock TextBlock
    {
      get
      {
        return this.textBlock;
      }
      set
      {
        this.textBlock = value;
      }
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      this.textBlock.MouseMove(sender, e);
      if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.None)
      {
        if (this.isMouseAlreadyPress)
          return;
        this.isMouseAlreadyPress = true;
        this.textBlock.MouseUp(sender, e);
      }
      else
        this.isMouseAlreadyPress = false;
    }

    public SizeF GetTextSize(TextParams textParams)
    {
      if (this.isDirty)
      {
        this.isDirty = false;
        this.TextBlock = TinyHTMLParsers.Parse(textParams);
      }
      return this.TextBlock.GetTextSize(new SizeF(float.MaxValue, float.MaxValue), textParams);
    }

    public SizeF GetTextSize(SizeF proposedSize, TextParams textParams)
    {
      if (this.isDirty)
      {
        this.isDirty = false;
        this.TextBlock = TinyHTMLParsers.Parse(textParams);
      }
      return this.TextBlock.GetTextSize(proposedSize, textParams);
    }

    public SizeF MeasureOverride(
      SizeF availableSize,
      TextParams textParams,
      ITextElement element)
    {
      throw new NotImplementedException();
    }
  }
}
