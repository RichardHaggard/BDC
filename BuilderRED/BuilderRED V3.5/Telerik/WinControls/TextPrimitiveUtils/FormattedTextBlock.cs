// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TextPrimitiveUtils.FormattedTextBlock
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.TextPrimitiveUtils
{
  public class FormattedTextBlock
  {
    private List<TextLine> lines = new List<TextLine>();

    public List<TextLine> Lines
    {
      get
      {
        return this.lines;
      }
      set
      {
        this.lines = value;
      }
    }

    public void ArrangeLines(bool textWrap, SizeF proposedSize)
    {
      if (!textWrap || (double) proposedSize.Width <= 0.0 && (double) proposedSize.Height <= 0.0)
        return;
      for (int index1 = 0; index1 < this.lines.Count; ++index1)
      {
        while ((double) FormattedTextBlock.GetTextLineSize(this.lines[index1]).Width > (double) proposedSize.Width && this.lines[index1].List.Count > 1)
        {
          int index2 = this.lines[index1].List.Count - 1;
          FormattedText formattedText = this.lines[index1].List[index2];
          this.lines[index1].List.RemoveAt(index2);
          if (index1 == this.lines.Count - 1)
            this.lines.Add(new TextLine());
          TextLine line = this.lines[index1 + 1];
          if (line.List.Count > 0 && !line.List[0].StartNewLine)
            line.List.Insert(0, formattedText);
          else
            this.lines.Insert(index1 + 1, new TextLine()
            {
              List = {
                formattedText
              }
            });
        }
      }
    }

    public void RecalculateBlockLines(bool TextWrap)
    {
      if (!TextWrap)
        return;
      for (int index1 = 0; index1 < this.Lines.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.Lines[index1].List.Count; ++index2)
        {
          FormattedText prototypeFormattedText = this.Lines[index1].List[index2];
          if (!string.IsNullOrEmpty(prototypeFormattedText.Text))
          {
            bool flag1 = prototypeFormattedText.Text.Length > 0 && char.IsWhiteSpace(prototypeFormattedText.Text[0]);
            bool flag2 = prototypeFormattedText.Text.Length > 0 && char.IsWhiteSpace(prototypeFormattedText.Text[prototypeFormattedText.Text.Length - 1]);
            string[] strArray = prototypeFormattedText.Text.Trim().Split(new char[1]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
            prototypeFormattedText.Text = string.Format("{0}{1}{2}", flag1 ? (object) " " : (object) string.Empty, strArray.Length > 0 ? (object) strArray[0] : (object) string.Empty, strArray.Length > 1 || flag2 ? (object) " " : (object) string.Empty);
            for (int index3 = 1; index3 < strArray.Length; ++index3)
              this.Lines[index1].List.Insert(index2 + index3, new FormattedText(prototypeFormattedText)
              {
                Text = strArray[index3] + (index3 < strArray.Length - 1 || flag2 ? " " : string.Empty)
              });
            if (strArray.Length > 0)
              index2 += strArray.Length - 1;
          }
        }
        int count = this.Lines[index1].List.Count;
        if (count > 0 && !string.IsNullOrEmpty(this.Lines[index1].List[count - 1].Text))
          this.Lines[index1].List[count - 1].Text = this.Lines[index1].List[count - 1].Text.TrimEnd();
      }
    }

    public SizeF GetTextSize(SizeF proposedSize, TextParams textParams)
    {
      using (StringFormat stringFormat = textParams.CreateStringFormat())
        return this.GetTextSize(proposedSize, textParams.useCompatibleTextRendering, stringFormat, TextFormatFlags.Default, textParams.textWrap);
    }

    public SizeF GetTextSize(
      SizeF proposedSize,
      bool useCompatibleTextRendering,
      StringFormat sf,
      TextFormatFlags textFormatFlags,
      bool textWrap)
    {
      if (textWrap)
      {
        this.RecalculateBlockLines(textWrap);
        this.ArrangeLines(textWrap, proposedSize);
      }
      SizeF empty1 = (SizeF) Size.Empty;
      SizeF empty2 = (SizeF) Size.Empty;
      foreach (TextLine line in this.lines)
      {
        SizeF textLineSize = FormattedTextBlock.GetTextLineSize(line);
        float height = (double) textLineSize.Height <= 2.0 ? 0.0f : textLineSize.Height;
        line.LineSize = new SizeF(textLineSize.Width, height);
        empty1.Height += height;
        empty1.Width = Math.Max(textLineSize.Width, empty1.Width);
      }
      return empty1;
    }

    private static SizeF GetTextLineSize(TextLine line)
    {
      SizeF sizeF = new SizeF(line.GetLineOffset(), 0.0f);
      SizeF empty = SizeF.Empty;
      foreach (FormattedText formattedText in line.List)
      {
        if (!string.IsNullOrEmpty(formattedText.Text) || formattedText.Image != null || formattedText.StartNewLine)
        {
          SizeF blockSize = formattedText.BlockSize;
          sizeF.Width += !formattedText.StartNewLine || !string.IsNullOrEmpty(formattedText.Text) || formattedText.Image != null ? blockSize.Width : 0.0f;
          sizeF.Height = Math.Max(sizeF.Height, blockSize.Height);
          line.BaseLine = Math.Max(line.BaseLine, formattedText.BaseLine);
        }
      }
      return sizeF;
    }

    public void PaintFormatTextBlock(
      IGraphics graphics,
      RectangleF paintingRectangleParam,
      bool useCompatibleTextRendering,
      StringFormat format,
      TextFormatFlags flags,
      bool textWrap,
      bool clipText)
    {
      if (this.lines.Count == 0)
        return;
      SizeF textSize = this.GetTextSize(paintingRectangleParam.Size, useCompatibleTextRendering, format, flags, textWrap);
      if ((double) textSize.Height > (double) paintingRectangleParam.Height)
        textSize.Height = paintingRectangleParam.Height;
      if ((double) textSize.Width > (double) paintingRectangleParam.Width)
        textSize.Width = paintingRectangleParam.Width;
      RectangleF rectangleF1 = LayoutUtils.VAlign(textSize, paintingRectangleParam, this.lines[0].GetLineAligment());
      rectangleF1.X += this.lines[0].GetLineOffset();
      foreach (TextLine line in this.lines)
      {
        RectangleF rectangleF2 = LayoutUtils.HAlign(line.LineSize, rectangleF1, line.GetLineAligment());
        rectangleF2.Height = line.LineSize.Height;
        if ((double) rectangleF2.X < (double) rectangleF1.X)
          rectangleF2.X = rectangleF1.X;
        if ((double) rectangleF2.Width > (double) rectangleF1.Width)
          rectangleF2.Width = rectangleF1.Width;
        if ((double) rectangleF2.Y < (double) rectangleF1.Y)
          rectangleF2.Y = rectangleF1.Y;
        if ((double) rectangleF2.Height > (double) rectangleF1.Height)
          rectangleF2.Height = rectangleF1.Height;
        float height = line.LineSize.Height;
        bool flag = true;
        float num = 0.0f;
        foreach (FormattedText formattedText in line.List)
        {
          if (!string.IsNullOrEmpty(formattedText.Text) || formattedText.Image != null)
          {
            if (flag && formattedText.ListType != FormattedText.HTMLLikeListType.None && !formattedText.ShouldDisplayBullet)
              num = formattedText.BulletFontSize;
            PointF currentLineBeginLocation = new PointF(rectangleF2.Location.X + line.GetLineOffset() + num, (float) Math.Ceiling((double) rectangleF2.Y));
            flag = false;
            if (Math.Floor((double) rectangleF2.Bottom - 1.0) > (double) paintingRectangleParam.Height + (double) paintingRectangleParam.Top)
              return;
            formattedText.PaintFormatText(graphics, rectangleF1, useCompatibleTextRendering, flags, currentLineBeginLocation, line.LineSize.Height, line.BaseLine, clipText);
            rectangleF2.Location = new PointF(rectangleF2.Location.X + formattedText.BlockSize.Width, rectangleF2.Location.Y);
            height = line.LineSize.Height;
          }
        }
        rectangleF1.Location = new PointF(rectangleF1.X, rectangleF1.Y + height);
      }
    }

    public void MouseUp(object sender, MouseEventArgs e)
    {
      FormattedText formattedText = this.IsMouseOverBlock(sender, e);
      if (formattedText == null)
        return;
      formattedText.FontColor = TinyHTMLParsers.LinkClickedColor;
      FormattedTextBlock.RunLink(formattedText.Link);
    }

    public void MouseMove(object sender, MouseEventArgs e)
    {
      if (this.IsMouseOverBlock(sender, e) == null)
        return;
      Cursor.Current = Cursors.Hand;
    }

    private FormattedText IsMouseOverBlock(object sender, MouseEventArgs e)
    {
      Point point = (sender as RadElement).PointFromControl(e.Location);
      int count1 = this.lines.Count;
      for (int index1 = 0; index1 < count1; ++index1)
      {
        TextLine line = this.lines[index1];
        int count2 = line.List.Count;
        for (int index2 = 0; index2 < count2; ++index2)
        {
          FormattedText formattedText = line.List[index2];
          if (!string.IsNullOrEmpty(formattedText.Link) && formattedText.DrawingRectangle.Contains((PointF) point))
            return formattedText;
        }
      }
      return (FormattedText) null;
    }

    private static void RunLink(string link)
    {
      try
      {
        if (link.Contains("~"))
          link = link.Replace("~", Application.StartupPath);
        Process.Start(link);
      }
      catch
      {
      }
    }
  }
}
