// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Symbology1D
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public abstract class Symbology1D : SymbologyBase
  {
    protected static readonly char BarChar = '1';
    protected static readonly char GapChar = '0';
    protected static readonly char TagChar = '|';
    private SizeF textSize = SizeF.Empty;
    private RectangleF textRect = RectangleF.Empty;
    protected RectangleF barRect;
    protected string barText;
    protected string pattern;

    public override void ProcessValue(string value)
    {
      base.ProcessValue(value);
      string symbols = this.GetSymbols(value);
      this.pattern = this.GetEncoding(symbols);
      this.barText = this.GetFormat(symbols);
    }

    public override SizeF MeasureContent(IMeasureContext context, SizeF size)
    {
      base.MeasureContent(context, size);
      if (this.ShowText)
      {
        this.textSize = context.MeasureString(this.barText);
        this.ValidateTextSize(this.textSize, size);
      }
      if (!this.Stretch)
        size.Width = this.GetLength();
      size.Width = Math.Max(size.Width, this.textSize.Width);
      return size;
    }

    private void ValidateTextSize(SizeF textSize, SizeF availableSize)
    {
      if ((double) textSize.Width > (double) availableSize.Width || (double) textSize.Height > (double) availableSize.Height)
        throw new InvalidSizeException(this.barText);
    }

    public override void ArrangeContent(IMeasureContext context, RectangleF bounds)
    {
      this.textRect = this.GetTextRect(bounds);
      this.barRect = this.GetBarRect(bounds);
    }

    protected virtual float GetLength()
    {
      return (float) (this.pattern.Length * this.Module);
    }

    protected static RectangleF GetTextRect(
      SizeF textSize,
      RectangleF bounds,
      StringAlignment textAlign,
      StringAlignment lineAlign)
    {
      if ((double) textSize.Width < (double) bounds.Width)
      {
        switch (textAlign)
        {
          case StringAlignment.Center:
            bounds.X += (float) (((double) bounds.Width - (double) textSize.Width) / 2.0);
            break;
          case StringAlignment.Far:
            bounds.X += bounds.Width - textSize.Width;
            break;
        }
        bounds.Width = textSize.Width;
      }
      if ((double) textSize.Height < (double) bounds.Height)
      {
        switch (lineAlign)
        {
          case StringAlignment.Center:
            bounds.Y += (float) (((double) bounds.Height - (double) textSize.Height) / 2.0);
            break;
          case StringAlignment.Far:
            bounds.Y = bounds.Bottom - textSize.Height;
            break;
        }
      }
      bounds.Height = textSize.Height;
      bounds.Width = textSize.Width;
      return bounds;
    }

    protected virtual RectangleF GetTextRect(RectangleF bounds)
    {
      if (!this.ShowText)
        return RectangleF.Empty;
      return Symbology1D.GetTextRect(this.textSize, bounds, this.TextAlign, this.LineAlign);
    }

    protected virtual RectangleF GetBarRect(RectangleF bounds)
    {
      if (this.ShowText)
      {
        switch (this.LineAlign)
        {
          case StringAlignment.Near:
            bounds.Y += this.textSize.Height;
            bounds.Height -= this.textSize.Height;
            break;
          case StringAlignment.Far:
            bounds.Height -= this.textSize.Height;
            break;
        }
      }
      return this.AlignToText(bounds);
    }

    protected virtual RectangleF AlignToText(RectangleF bounds)
    {
      if (!this.Stretch)
      {
        float length = this.GetLength();
        if ((double) length < (double) bounds.Width)
        {
          bounds.X += (float) (((double) bounds.Width - (double) length) / 2.0);
          bounds.Width = length;
        }
      }
      return bounds;
    }

    protected override void CreateTextElementsOverride(IElementFactory factory)
    {
      factory.CreateTextElement(this.barText, this.textRect);
    }

    protected override void CreateBarsOverride(IElementFactory factory)
    {
      int length = this.pattern.Length;
      int num1 = 0;
      int index = 0;
      for (; num1 < length; num1 = index)
      {
        while (index < length && (int) this.pattern[index] == (int) Symbology1D.BarChar)
          ++index;
        int num2 = index - num1;
        if (num2 > 0)
        {
          RectangleF barRect = this.barRect;
          barRect.X += this.barRect.Width * (float) num1 / (float) length;
          barRect.Width = this.barRect.Width * (float) num2 / (float) length;
          factory.CreateBarElement(barRect);
        }
        while (index < length && (int) this.pattern[index] == (int) Symbology1D.GapChar)
          ++index;
      }
    }

    protected virtual string GetSymbols(string value)
    {
      return value;
    }

    protected virtual string GetEncoding(string value)
    {
      return value;
    }

    protected virtual string GetFormat(string value)
    {
      return value;
    }
  }
}
