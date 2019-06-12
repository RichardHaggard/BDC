// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TextPrimitiveUtils.FormattedText
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.TextPrimitiveUtils
{
  public class FormattedText
  {
    internal string text = string.Empty;
    private const float DefaultOffsetSize = 30f;
    private const char BulletSymbol = '·';
    private string fontName;
    private Color fontColor;
    private FontStyle fontStyle;
    private ContentAlignment contentAlignment;
    private SizeF blockSize;
    private float fontSize;
    private string htmlTag;
    private Color? bgColor;
    private Image image;
    private string link;
    private int offset;
    private float offsetSize;
    private int number;
    protected FormattedText.HTMLLikeListType prevListType;
    private FormattedText.HTMLLikeListType listType;
    private bool shouldDisplayBullet;
    private bool isDirtySize;
    private bool startNewLine;
    private SizeF bulletSize;
    private bool isClosingTag;
    private RectangleF drawingRectangle;
    private FontStyle bulletFontStyle;
    private float bulletFontSize;
    private string bulletFontName;
    private float baseLine;
    [ThreadStatic]
    private static Dictionary<int, float> baseLines;

    public FormattedText()
    {
    }

    public FormattedText(FormattedText prototypeFormattedText)
    {
      this.FontName = prototypeFormattedText.fontName;
      this.fontColor = prototypeFormattedText.fontColor;
      this.fontStyle = prototypeFormattedText.fontStyle;
      this.FontSize = prototypeFormattedText.FontSize;
      this.contentAlignment = prototypeFormattedText.contentAlignment;
      this.htmlTag = prototypeFormattedText.htmlTag;
      this.bgColor = prototypeFormattedText.bgColor;
      this.link = prototypeFormattedText.link;
      this.offset = prototypeFormattedText.offset;
      this.offsetSize = prototypeFormattedText.offsetSize;
      this.number = prototypeFormattedText.number;
      this.listType = prototypeFormattedText.listType;
      this.bulletFontName = prototypeFormattedText.bulletFontName;
      this.bulletFontSize = prototypeFormattedText.bulletFontSize;
      this.bulletFontStyle = prototypeFormattedText.bulletFontStyle;
      if (!string.IsNullOrEmpty(prototypeFormattedText.text))
        return;
      this.shouldDisplayBullet = prototypeFormattedText.shouldDisplayBullet;
    }

    public string HtmlTag
    {
      get
      {
        return this.htmlTag;
      }
      set
      {
        this.htmlTag = value;
      }
    }

    public float FontSize
    {
      get
      {
        return this.fontSize;
      }
      set
      {
        this.fontSize = (double) value >= 0.0 ? value : 7f;
        this.isDirtySize = true;
      }
    }

    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
        this.isDirtySize = true;
      }
    }

    public SizeF BlockSize
    {
      get
      {
        if (this.isDirtySize || this.blockSize == SizeF.Empty && !string.IsNullOrEmpty(this.text))
        {
          this.blockSize = this.GetTextBlockSize(true, TextFormatFlags.Default);
          this.isDirtySize = false;
        }
        return this.blockSize;
      }
    }

    public ContentAlignment ContentAlignment
    {
      get
      {
        return this.contentAlignment;
      }
      set
      {
        this.contentAlignment = value;
      }
    }

    public FontStyle FontStyle
    {
      get
      {
        return this.fontStyle;
      }
      set
      {
        this.fontStyle = this.SelectEffectiveStyle(this.fontName, value);
        this.isDirtySize = true;
      }
    }

    public Color FontColor
    {
      get
      {
        return this.fontColor;
      }
      set
      {
        this.fontColor = value;
      }
    }

    public string FontName
    {
      get
      {
        return this.fontName;
      }
      set
      {
        this.fontName = value;
        this.fontStyle = this.SelectEffectiveStyle(this.fontName, this.fontStyle);
        this.isDirtySize = true;
      }
    }

    public Color? BgColor
    {
      get
      {
        return this.bgColor;
      }
      set
      {
        this.bgColor = value;
      }
    }

    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
        this.isDirtySize = true;
      }
    }

    public string Link
    {
      set
      {
        this.link = value;
      }
      get
      {
        return this.link;
      }
    }

    public int Offset
    {
      set
      {
        this.offset = value;
        this.offsetSize = (float) value * 30f;
      }
      get
      {
        return this.offset;
      }
    }

    public float OffsetSize
    {
      get
      {
        return this.offsetSize;
      }
    }

    public int Number
    {
      set
      {
        this.number = value;
      }
      get
      {
        return this.number;
      }
    }

    public bool ShouldDisplayBullet
    {
      set
      {
        this.shouldDisplayBullet = value;
      }
      get
      {
        return this.shouldDisplayBullet;
      }
    }

    public string BulletFontName
    {
      set
      {
        this.bulletFontName = value;
      }
      get
      {
        return this.bulletFontName;
      }
    }

    public FontStyle BulletFontStyle
    {
      set
      {
        this.bulletFontStyle = value;
      }
      get
      {
        return this.bulletFontStyle;
      }
    }

    public float BulletFontSize
    {
      set
      {
        this.bulletFontSize = value;
      }
      get
      {
        return this.bulletFontSize;
      }
    }

    public FormattedText.HTMLLikeListType ListType
    {
      set
      {
        this.listType = value;
      }
      get
      {
        return this.listType;
      }
    }

    public bool StartNewLine
    {
      set
      {
        this.startNewLine = value;
      }
      get
      {
        return this.startNewLine;
      }
    }

    public SizeF BulletSize
    {
      get
      {
        return this.bulletSize;
      }
    }

    public bool IsClosingTag
    {
      set
      {
        this.isClosingTag = value;
      }
      get
      {
        return this.isClosingTag;
      }
    }

    public RectangleF DrawingRectangle
    {
      get
      {
        return this.drawingRectangle;
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

    public SizeF GetTextBlockSize(
      bool useCompatibleTextRendering,
      TextFormatFlags textFormatFlags)
    {
      SizeF sizeF1 = SizeF.Empty;
      if (this.image != null)
        sizeF1 = (SizeF) this.image.Size;
      if (string.IsNullOrEmpty(this.Text) && !this.startNewLine)
        return sizeF1;
      string text = this.text;
      if (string.IsNullOrEmpty(this.Text) && this.startNewLine)
        text = "\n";
      if (text != null)
        text = text.Replace("&nbsp;", " ");
      using (Font font = new Font(this.fontName, this.FontSize, this.FontStyle))
      {
        if (!useCompatibleTextRendering)
        {
          SizeF sizeF2 = (SizeF) TextRenderer.MeasureText(text, font, new Size(int.MaxValue, int.MaxValue), textFormatFlags);
          sizeF1.Width += sizeF2.Width;
          sizeF1.Height = Math.Max(sizeF1.Height, sizeF2.Height);
          return sizeF1;
        }
        using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic))
        {
          stringFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap;
          stringFormat.Alignment = StringAlignment.Near;
          stringFormat.LineAlignment = StringAlignment.Near;
          stringFormat.Trimming = StringTrimming.None;
          stringFormat.HotkeyPrefix = HotkeyPrefix.None;
          SizeF empty = SizeF.Empty;
          lock (MeasurementGraphics.SyncObject)
          {
            Graphics measurementGraphics = RadGdiGraphics.MeasurementGraphics;
            this.bulletSize = this.MeasureBullet(measurementGraphics, stringFormat);
            this.baseLine = FormattedText.GetBaseLineFromFont(font);
            SizeF sizeF2 = measurementGraphics.MeasureString(text, font, 0, stringFormat);
            sizeF1.Width += this.bulletSize.Width + sizeF2.Width;
            ++sizeF1.Width;
            sizeF1.Height = Math.Max(sizeF1.Height, this.bulletSize.Height);
            sizeF1.Height = Math.Max(sizeF1.Height, sizeF2.Height);
          }
        }
      }
      return sizeF1;
    }

    private static float GetBaseLineFromFont(Font font)
    {
      int hashCode = font.GetHashCode();
      if (FormattedText.baseLines == null)
        FormattedText.baseLines = new Dictionary<int, float>(4);
      if (FormattedText.baseLines.ContainsKey(hashCode))
        return FormattedText.baseLines[hashCode];
      FontTextMetrics textMetric = RadGdiGraphics.GetTextMetric(font);
      FormattedText.baseLines.Add(hashCode, (float) textMetric.ascent);
      return (float) textMetric.ascent;
    }

    private SizeF MeasureBullet(Graphics graphics, StringFormat sf)
    {
      if (!this.shouldDisplayBullet)
        return SizeF.Empty;
      Font font = this.listType != FormattedText.HTMLLikeListType.List ? new Font(this.bulletFontName, this.bulletFontSize, this.bulletFontStyle) : new Font("Symbol", this.bulletFontSize, FontStyle.Regular);
      SizeF sizeF = graphics.MeasureString(this.ProduceTextBullets(), font, 0, sf);
      font.Dispose();
      return sizeF;
    }

    private string ProduceTextBullets()
    {
      if (!this.shouldDisplayBullet)
        return string.Empty;
      switch (this.listType)
      {
        case FormattedText.HTMLLikeListType.None:
          return string.Empty;
        case FormattedText.HTMLLikeListType.OrderedList:
          return this.number.ToString() + ". ";
        case FormattedText.HTMLLikeListType.List:
          return 183.ToString() + " ";
        default:
          return string.Empty;
      }
    }

    private FontStyle SelectEffectiveStyle(string fontName, FontStyle fontStyle)
    {
      FontFamily fontFamily;
      try
      {
        fontFamily = ThemeResolutionService.GetCustomFont(fontName) ?? new FontFamily(fontName);
      }
      catch
      {
        return FontStyle.Regular;
      }
      FontStyle style = fontStyle;
      if (fontFamily.IsStyleAvailable(style))
        return style;
      FontStyle fontStyle1 = FontStyle.Bold | FontStyle.Italic;
      FontStyle fontStyle2 = style & fontStyle1;
      List<FontStyle> fontStyleList = new List<FontStyle>(4);
      if (fontStyle2 != fontStyle1)
      {
        fontStyleList.Add(FontStyle.Regular);
        fontStyleList.Add(FontStyle.Italic);
        fontStyleList.Add(FontStyle.Bold);
      }
      else
      {
        fontStyleList.Add(fontStyle1);
        fontStyleList.Add(FontStyle.Bold);
        fontStyleList.Add(FontStyle.Italic);
        fontStyleList.Add(FontStyle.Regular);
      }
      fontStyleList.Remove(fontStyle2);
      for (int index = 0; index < fontStyleList.Count; ++index)
      {
        if (fontFamily.IsStyleAvailable(fontStyleList[index]))
        {
          fontStyle2 = fontStyleList[index];
          break;
        }
      }
      fontStyle &= FontStyle.Underline | FontStyle.Strikeout;
      return fontStyle | fontStyle2;
    }

    public void PaintFormatText(
      IGraphics graphics,
      RectangleF paintingRectangle,
      bool useCompatibleTextRendering,
      TextFormatFlags flags,
      PointF currentLineBeginLocation,
      float lineHeight,
      float lineBaseLine,
      bool clipText)
    {
      if (this.image != null)
      {
        graphics.DrawBitmap(this.image, (int) currentLineBeginLocation.X, (int) currentLineBeginLocation.Y);
        currentLineBeginLocation.X += (float) this.image.Size.Width;
      }
      if (string.IsNullOrEmpty(this.Text) || (double) currentLineBeginLocation.X >= (double) paintingRectangle.Right)
        return;
      string s1 = this.Text.Replace("&nbsp;", " ");
      using (Font font = new Font(this.FontName, this.FontSize, this.FontStyle))
      {
        if (!useCompatibleTextRendering)
        {
          TextRenderer.DrawText((IDeviceContext) graphics.UnderlayGraphics, this.ProduceTextBullets() + s1, font, new Rectangle((int) Math.Round((double) currentLineBeginLocation.X), (int) Math.Round((double) currentLineBeginLocation.Y), (int) Math.Round((double) this.BlockSize.Width), (int) Math.Round((double) lineHeight)), this.FontColor, flags);
        }
        else
        {
          SizeF sizeF = new SizeF(Math.Min(this.BlockSize.Width, paintingRectangle.Right - currentLineBeginLocation.X), Math.Min(this.BlockSize.Height, paintingRectangle.Height));
          using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic))
          {
            if (!clipText)
              stringFormat.FormatFlags |= StringFormatFlags.NoClip;
            else
              stringFormat.FormatFlags &= ~StringFormatFlags.NoClip;
            stringFormat.FormatFlags &= ~StringFormatFlags.LineLimit;
            stringFormat.FormatFlags &= ~StringFormatFlags.FitBlackBox;
            stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoWrap;
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            stringFormat.Trimming = StringTrimming.None;
            stringFormat.HotkeyPrefix = HotkeyPrefix.None;
            if (this.bgColor.HasValue)
              this.DrawBgColor(currentLineBeginLocation, sizeF, graphics);
            RectangleF textRectangle = new RectangleF(currentLineBeginLocation, sizeF);
            SolidBrush brush = new SolidBrush(this.FontColor);
            Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
            this.DrawBullets(underlayGraphics, textRectangle, stringFormat, brush);
            this.drawingRectangle = new RectangleF(textRectangle.X + this.bulletSize.Width, textRectangle.Y + (lineBaseLine - this.baseLine), textRectangle.Width + 1f, textRectangle.Height);
            if (clipText)
              ++this.drawingRectangle.Width;
            if ((font.Style & FontStyle.Underline) == FontStyle.Underline)
            {
              string s2 = s1.Replace(' ', '_');
              underlayGraphics.DrawString(s2, font, (Brush) brush, (RectangleF) Rectangle.Round(this.drawingRectangle), stringFormat);
            }
            else
              underlayGraphics.DrawString(s1, font, (Brush) brush, (RectangleF) Rectangle.Round(this.drawingRectangle), stringFormat);
            brush.Dispose();
          }
        }
      }
    }

    private void DrawBgColor(PointF location, SizeF textSize, IGraphics graphics)
    {
      Rectangle BorderRectangle = new Rectangle(new Point((int) ((double) location.X + (double) this.bulletSize.Width + 1.0), (int) location.Y - 1), textSize.ToSize() + new Size(1, 1));
      graphics.FillRectangle(BorderRectangle, this.bgColor.Value);
    }

    private void DrawBullets(
      Graphics g,
      RectangleF textRectangle,
      StringFormat sf,
      SolidBrush brush)
    {
      if (!this.shouldDisplayBullet)
        return;
      Font font;
      if (this.listType == FormattedText.HTMLLikeListType.List)
      {
        font = new Font("Symbol", this.bulletFontSize, FontStyle.Regular);
      }
      else
      {
        using (FontFamily fontFamily = new FontFamily(this.bulletFontName))
          font = !fontFamily.IsStyleAvailable(FontStyle.Regular) ? new Font(this.bulletFontName, this.bulletFontSize, this.SelectEffectiveStyle(this.bulletFontName, this.bulletFontStyle)) : new Font(this.bulletFontName, this.bulletFontSize, FontStyle.Regular);
      }
      g.DrawString(this.ProduceTextBullets(), font, (Brush) brush, LayoutUtils.VAlign(this.bulletSize, textRectangle, ContentAlignment.MiddleCenter), sf);
      font.Dispose();
    }

    public enum HTMLLikeListType : byte
    {
      None,
      OrderedList,
      List,
    }
  }
}
