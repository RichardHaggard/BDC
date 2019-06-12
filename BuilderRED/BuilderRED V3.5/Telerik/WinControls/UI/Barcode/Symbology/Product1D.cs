// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.Product1D
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public abstract class Product1D : Symbology1D
  {
    private static readonly char Padding = '0';
    private static readonly IList<char> Charset = (IList<char>) new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    private RectangleF headRect;
    private RectangleF tailRect;
    private RectangleF leftRect;
    private RectangleF rightRect;
    private SizeF headSize;
    private SizeF tailSize;
    private SizeF leftSize;
    private SizeF rightSize;
    private string headText;
    private string tailText;
    private string leftText;
    private string rightText;

    public override void ProcessValue(string value)
    {
      base.ProcessValue(value);
      this.headText = this.GetHeadText(this.barText);
      this.tailText = this.GetTailText(this.barText);
      this.leftText = this.GetLeftText(this.barText);
      this.rightText = this.GetRightText(this.barText);
    }

    public override SizeF MeasureContent(IMeasureContext context, SizeF size)
    {
      SizeF sizeF = base.MeasureContent(context, size);
      this.headSize = context.MeasureString(this.headText);
      this.tailSize = context.MeasureString(this.tailText);
      this.leftSize = context.MeasureString(this.leftText);
      this.rightSize = context.MeasureString(this.rightText);
      if (!this.Stretch)
        sizeF.Width = Math.Min(size.Width, sizeF.Width + this.headSize.Width + this.headSize.Width);
      return sizeF;
    }

    public override void ArrangeContent(IMeasureContext context, RectangleF bounds)
    {
      base.ArrangeContent(context, bounds);
      this.headRect = this.GetHeadRect(bounds);
      this.tailRect = this.GetTailRect(bounds);
      this.leftRect = this.GetLeftRect(bounds);
      this.rightRect = this.GetRightRect(bounds);
      if (this.ShowText && ((double) this.leftSize.Width - (double) this.leftRect.Width > 0.5 || (double) this.leftSize.Height - (double) this.leftRect.Height > 0.5 || ((double) this.rightSize.Width - (double) this.rightRect.Width > 0.5 || (double) this.rightSize.Height - (double) this.rightRect.Height > 0.5)))
        throw new InvalidSizeException(this.Value);
    }

    protected override RectangleF AlignToText(RectangleF bounds)
    {
      return bounds;
    }

    protected virtual string GetHeadText(string value)
    {
      return string.Empty;
    }

    protected virtual string GetTailText(string value)
    {
      return string.Empty;
    }

    protected virtual string GetLeftText(string value)
    {
      return string.Empty;
    }

    protected virtual string GetRightText(string value)
    {
      return string.Empty;
    }

    protected override RectangleF GetBarRect(RectangleF bounds)
    {
      bounds = base.GetBarRect(bounds);
      if (this.ShowText)
      {
        bounds.X += this.headSize.Width;
        bounds.Width -= this.headSize.Width;
        bounds.Width -= this.tailSize.Width;
      }
      return bounds;
    }

    protected virtual RectangleF GetHeadRect(RectangleF bounds)
    {
      bounds.Width = this.headSize.Width;
      return bounds;
    }

    protected virtual RectangleF GetTailRect(RectangleF bounds)
    {
      bounds.X += bounds.Width;
      bounds.X -= this.tailSize.Width;
      bounds.Width = this.tailSize.Width;
      return bounds;
    }

    protected virtual RectangleF GetLeftRect(RectangleF bounds)
    {
      bounds.X += this.headSize.Width;
      bounds.Width -= this.headSize.Width + this.tailSize.Width;
      int length = this.pattern.Length;
      if (length > 0)
      {
        int startIndex = this.pattern.IndexOf(Symbology1D.BarChar);
        if (startIndex > 0)
        {
          SizeF size = bounds.Size;
          int num1 = this.pattern.IndexOf(Symbology1D.TagChar, startIndex);
          if (num1 > 0)
            bounds.Width = size.Width * (float) (num1 - 1) / (float) length;
          int num2 = this.pattern.LastIndexOf(Symbology1D.TagChar, startIndex);
          if (num2 > 0)
          {
            bounds.X += size.Width * (float) (num2 + 2) / (float) length;
            bounds.Width -= size.Width * (float) (num2 + 2) / (float) length;
          }
        }
      }
      return bounds;
    }

    protected virtual RectangleF GetRightRect(RectangleF bounds)
    {
      bounds.X += this.headSize.Width;
      bounds.Width -= this.headSize.Width + this.tailSize.Width;
      int length = this.pattern.Length;
      if (length > 0)
      {
        int startIndex = this.pattern.LastIndexOf(Symbology1D.BarChar);
        if (startIndex > 0)
        {
          SizeF size = bounds.Size;
          int num1 = this.pattern.IndexOf(Symbology1D.TagChar, startIndex);
          if (num1 > 0)
            bounds.Width = size.Width * (float) (num1 - 1) / (float) length;
          int num2 = this.pattern.LastIndexOf(Symbology1D.TagChar, startIndex);
          if (num2 > 0)
          {
            bounds.X += size.Width * (float) (num2 + 2) / (float) length;
            bounds.Width -= size.Width * (float) (num2 + 2) / (float) length;
          }
        }
      }
      return bounds;
    }

    protected override void CreateTextElementsOverride(IElementFactory factory)
    {
      this.CreateTextElement(factory, this.headText, this.headSize, this.headRect);
      this.CreateTextElement(factory, this.tailText, this.tailSize, this.tailRect);
      this.CreateTextElement(factory, this.leftText, this.leftSize, this.leftRect);
      this.CreateTextElement(factory, this.rightText, this.rightSize, this.rightRect);
    }

    private void CreateTextElement(
      IElementFactory factory,
      string text,
      SizeF textSize,
      RectangleF bounds)
    {
      factory.CreateTextElement(text, Symbology1D.GetTextRect(textSize, bounds, this.TextAlign, this.LineAlign));
    }

    protected override void CreateBarsOverride(IElementFactory factory)
    {
      RectangleF barRect1 = this.barRect;
      RectangleF barRect2 = this.barRect;
      if (this.ShowText)
      {
        switch (this.LineAlign)
        {
          case StringAlignment.Near:
            barRect1.Y -= this.leftSize.Height / 2f;
            barRect1.Height += this.leftSize.Height / 2f;
            break;
          case StringAlignment.Far:
            barRect1.Height += this.leftSize.Height / 2f;
            break;
        }
      }
      int length = this.pattern.Length;
      int num1 = 0;
      int index = 0;
      for (; num1 < length; num1 = index)
      {
        while (index < length && (int) this.pattern[index] == (int) Symbology1D.TagChar)
          ++index;
        int num2 = index - num1;
        if (num2 > 0)
        {
          RectangleF rect = barRect1;
          rect.X += barRect1.Width * (float) num1 / (float) length;
          rect.Width = barRect1.Width * (float) num2 / (float) length;
          factory.CreateBarElement(rect);
        }
        int num3 = index;
        while (index < length && (int) this.pattern[index] == (int) Symbology1D.BarChar)
          ++index;
        int num4 = index - num3;
        if (num4 > 0)
        {
          RectangleF rect = barRect2;
          rect.X += barRect2.Width * (float) num3 / (float) length;
          rect.Width = barRect2.Width * (float) num4 / (float) length;
          factory.CreateBarElement(rect);
        }
        while (index < length && (int) this.pattern[index] == (int) Symbology1D.GapChar)
          ++index;
      }
    }

    protected string GetSymbols(string value, int length)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (this.Checksum)
        value += (string) (object) Product1D.GetChecksum(value);
      return value.PadLeft(length, Product1D.Padding);
    }

    private static char GetChecksum(string value)
    {
      return Product1D.GetChecksum(value, 3, 1, 10);
    }

    private static char GetChecksum(string value, int first, int second, int modulo)
    {
      int num1 = 0;
      int num2 = first;
      for (int index = value.Length - 1; index >= 0; --index)
      {
        int num3 = Product1D.Charset.IndexOf(value[index]);
        num1 += num3 * num2;
        num2 = num2 != first ? first : second;
      }
      int index1 = num1 % modulo;
      if (index1 != 0)
        index1 = modulo - index1;
      return Product1D.Charset[index1];
    }
  }
}
