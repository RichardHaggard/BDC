// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.TextPrimitiveImpl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class TextPrimitiveImpl : ITextPrimitive
  {
    private const ContentAlignment anyRight = ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
    private const ContentAlignment anyBottom = ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
    private const ContentAlignment anyCenter = ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
    private const ContentAlignment anyMiddle = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;
    private const ContentAlignment anyTop = ContentAlignment.TopLeft | ContentAlignment.TopCenter | ContentAlignment.TopRight;
    private SizeF measuredSize;
    private LayoutUtils.MeasureTextCache textMeasurementCache;

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
    }

    public void PaintPrimitive(IGraphics graphics, TextParams textParams)
    {
      this.PaintPrimitive(graphics, 0.0f, new SizeF(1f, 1f), textParams);
    }

    public void PaintPrimitive(
      IGraphics graphics,
      float angle,
      SizeF scale,
      TextParams textParams)
    {
      if (textParams.useCompatibleTextRendering)
      {
        graphics.DrawString(textParams, this.measuredSize);
      }
      else
      {
        TextFormatFlags textFormatFlags = this.CreateTextFormatFlags(textParams);
        Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
        GraphicsState gstate = underlayGraphics.Save();
        underlayGraphics.TextRenderingHint = textParams.textRenderingHint;
        if (!textParams.enabled || textParams.forceBackColor)
        {
          this.DrawTextWithGDI(underlayGraphics, textParams.text, textParams.font, Rectangle.Ceiling(textParams.paintingRectangle), textParams.backColor, textParams.foreColor, textFormatFlags);
        }
        else
        {
          underlayGraphics.ResetTransform();
          TextRenderer.DrawText((IDeviceContext) underlayGraphics, textParams.text, textParams.font, Rectangle.Ceiling(textParams.paintingRectangle), textParams.foreColor, textFormatFlags);
        }
        underlayGraphics.Restore(gstate);
      }
    }

    private void DrawTextWithGDI(
      Graphics graphics,
      string text,
      Font font,
      Rectangle drawRect,
      Color backColor,
      Color foreColor,
      TextFormatFlags flags)
    {
      if (drawRect.Width == 0 || drawRect.Height == 0)
        return;
      using (BufferedGraphics bufferedGraphics = BufferedGraphicsManager.Current.Allocate(graphics, drawRect))
      {
        using (Bitmap bitmap = new Bitmap(drawRect.Width, drawRect.Height, bufferedGraphics.Graphics))
        {
          using (Graphics target = Graphics.FromImage((Image) bitmap))
          {
            if (foreColor == Color.White)
              foreColor = Color.DarkGray;
            if (backColor == Color.Transparent || backColor == Color.Empty)
              backColor = Color.White;
            bufferedGraphics.Graphics.Clear(backColor);
            TextRenderer.DrawText((IDeviceContext) bufferedGraphics.Graphics, text, font, drawRect, foreColor, flags);
            bufferedGraphics.Render(target);
            bitmap.MakeTransparent(backColor);
            graphics.DrawImageUnscaledAndClipped((Image) bitmap, new Rectangle(Point.Empty, drawRect.Size));
          }
        }
      }
    }

    public SizeF MeasureOverride(SizeF availableSize, TextParams textParams)
    {
      SizeF textSize = this.GetTextSize(availableSize, textParams);
      SizeF sizeF1 = LayoutUtils.FlipSizeIf(textParams.textOrientation == Orientation.Vertical, textSize);
      SizeF sizeF2 = LayoutUtils.FlipSizeIf(textParams.textOrientation == Orientation.Vertical, textSize);
      sizeF1.Width = Math.Max(sizeF1.Width, sizeF2.Width);
      sizeF1.Height = Math.Max(sizeF1.Height, sizeF2.Height);
      return sizeF1;
    }

    public SizeF MeasureOverride(
      SizeF availableSize,
      TextParams textParams,
      ITextElement element)
    {
      return this.MeasureOverride(availableSize, textParams);
    }

    public SizeF GetTextSize(SizeF proposedSize, TextParams textParams)
    {
      SizeF sizeF = SizeF.Empty;
      if (string.IsNullOrEmpty(textParams.text) || (double) proposedSize.Width == 0.0)
        return SizeF.Empty;
      StringFormat stringFormat = textParams.CreateStringFormat();
      int width = float.IsInfinity(proposedSize.Width) ? int.MaxValue : (int) Math.Floor((double) proposedSize.Width);
      if (textParams.textOrientation == Orientation.Vertical)
        width = float.IsInfinity(proposedSize.Height) ? int.MaxValue : (int) Math.Floor((double) proposedSize.Height);
      string text = textParams.text;
      if (text.Length > RadGdiGraphics.GdiStringLengthLimit)
        text = text.Substring(0, RadGdiGraphics.GdiStringLengthLimit);
      if (textParams.useCompatibleTextRendering)
      {
        lock (MeasurementGraphics.SyncObject)
        {
          Graphics measurementGraphics = RadGdiGraphics.MeasurementGraphics;
          measurementGraphics.TextRenderingHint = textParams.textRenderingHint;
          sizeF = measurementGraphics.MeasureString(text, textParams.font ?? Control.DefaultFont, width, stringFormat);
          if (float.IsNaN(sizeF.Width) || float.IsNaN(sizeF.Height))
            sizeF = SizeF.Empty;
          sizeF.Height = (float) Math.Round((double) sizeF.Height);
          sizeF.Width = (float) Math.Ceiling((double) sizeF.Width);
        }
      }
      else if (!string.IsNullOrEmpty(text))
      {
        TextFormatFlags textFormatFlags = this.CreateTextFormatFlags(textParams);
        sizeF = (SizeF) TextRenderer.MeasureText(text, textParams.font, new Size(width, int.MaxValue), textFormatFlags);
      }
      stringFormat.Dispose();
      this.measuredSize = sizeF;
      return sizeF;
    }

    public SizeF GetTextSize(
      SizeF proposedSize,
      bool autoFitInProposedSize,
      TextParams textParams)
    {
      SizeF sizeF = SizeF.Empty;
      StringFormat stringFormat = textParams.CreateStringFormat();
      string s = textParams.text.Length <= RadGdiGraphics.GdiStringLengthLimit ? textParams.text : textParams.text.Substring(0, RadGdiGraphics.GdiStringLengthLimit);
      lock (MeasurementGraphics.SyncObject)
      {
        Graphics measurementGraphics = RadGdiGraphics.MeasurementGraphics;
        measurementGraphics.TextRenderingHint = textParams.textRenderingHint;
        string helperString = this.GetHelperString(s);
        sizeF = !autoFitInProposedSize ? measurementGraphics.MeasureString(helperString, textParams.font, 0, stringFormat) : measurementGraphics.MeasureString(helperString, textParams.font, proposedSize, stringFormat);
      }
      stringFormat.Dispose();
      this.measuredSize = sizeF;
      return sizeF;
    }

    public SizeF GetTextSize(TextParams textParams)
    {
      return this.GetTextSize(LayoutUtils.MaxSizeF, textParams);
    }

    public ContentAlignment RtlContentAlignment(TextParams textParams)
    {
      if (!textParams.rightToLeft)
        return textParams.alignment;
      if ((textParams.alignment & Telerik.WinControls.WindowsFormsUtils.AnyTopAlign) != (ContentAlignment) 0)
      {
        switch (textParams.alignment)
        {
          case ContentAlignment.TopLeft:
            return ContentAlignment.TopRight;
          case ContentAlignment.TopRight:
            return ContentAlignment.TopLeft;
        }
      }
      if ((textParams.alignment & Telerik.WinControls.WindowsFormsUtils.AnyMiddleAlign) != (ContentAlignment) 0)
      {
        switch (textParams.alignment)
        {
          case ContentAlignment.MiddleLeft:
            return ContentAlignment.MiddleRight;
          case ContentAlignment.MiddleRight:
            return ContentAlignment.MiddleLeft;
        }
      }
      if ((textParams.alignment & Telerik.WinControls.WindowsFormsUtils.AnyBottomAlign) == (ContentAlignment) 0)
        return textParams.alignment;
      switch (textParams.alignment)
      {
        case ContentAlignment.BottomLeft:
          return ContentAlignment.BottomRight;
        case ContentAlignment.BottomRight:
          return ContentAlignment.BottomLeft;
        default:
          return textParams.alignment;
      }
    }

    public TextFormatFlags CreateTextFormatFlags(TextParams textParams)
    {
      TextFormatFlags textFormatFlags = this.TranslateAlignmentForGDI(this.RtlContentAlignment(textParams));
      if (textParams.autoEllipsis)
        textFormatFlags |= TextFormatFlags.EndEllipsis;
      if (textParams.rightToLeft)
        textFormatFlags |= TextFormatFlags.RightToLeft;
      if (!textParams.useMnemonic)
        textFormatFlags |= TextFormatFlags.NoPrefix;
      if (!textParams.showKeyboardCues)
        textFormatFlags |= TextFormatFlags.HidePrefix;
      return (!textParams.textWrap ? textFormatFlags | TextFormatFlags.SingleLine : textFormatFlags | TextFormatFlags.WordBreak) | TextFormatFlags.PreserveGraphicsTranslateTransform;
    }

    internal TextFormatFlags TextFormatFlagsForAlignmentGDI(ContentAlignment align)
    {
      return this.TranslateAlignmentForGDI(align);
    }

    internal TextFormatFlags TranslateLineAlignmentForGDI(ContentAlignment align)
    {
      if ((align & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
        return TextFormatFlags.Right;
      return (align & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != (ContentAlignment) 0 ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Default;
    }

    internal TextFormatFlags TranslateAlignmentForGDI(ContentAlignment align)
    {
      TextFormatFlags textFormatFlags = TextFormatFlags.Default;
      switch (align)
      {
        case ContentAlignment.TopLeft:
          textFormatFlags = TextFormatFlags.Default;
          break;
        case ContentAlignment.TopCenter:
          textFormatFlags = TextFormatFlags.HorizontalCenter;
          break;
        case ContentAlignment.TopRight:
          textFormatFlags = TextFormatFlags.Right;
          break;
        case ContentAlignment.MiddleLeft:
          textFormatFlags = TextFormatFlags.VerticalCenter;
          break;
        case ContentAlignment.MiddleCenter:
          textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
          break;
        case ContentAlignment.MiddleRight:
          textFormatFlags = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
          break;
        case ContentAlignment.BottomLeft:
          textFormatFlags = TextFormatFlags.Bottom;
          break;
        case ContentAlignment.BottomCenter:
          textFormatFlags = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
          break;
        case ContentAlignment.BottomRight:
          textFormatFlags = TextFormatFlags.Bottom | TextFormatFlags.Right;
          break;
      }
      return textFormatFlags;
    }

    private string GetHelperString(string s)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < s.Length; ++index)
      {
        if (s[index].Equals(' '))
          stringBuilder.Append("a");
        else
          stringBuilder.Append(s[index]);
      }
      return stringBuilder.ToString();
    }

    internal LayoutUtils.MeasureTextCache MeasureTextCache
    {
      get
      {
        if (this.textMeasurementCache == null)
          this.textMeasurementCache = new LayoutUtils.MeasureTextCache();
        return this.textMeasurementCache;
      }
    }
  }
}
