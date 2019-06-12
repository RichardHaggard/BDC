// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxControlMeasurer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class TextBoxControlMeasurer
  {
    public const TextFormatFlags FormatFlags = TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.NoPadding;
    public const TextRenderingHint StringRenderingHint = TextRenderingHint.SystemDefault;
    public const int TabSize = 4;

    public static Size MeasureText(string text, Font font)
    {
      using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
      {
        graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
        Size size = TextRenderer.MeasureText((IDeviceContext) graphics, text, font, Size.Empty, TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.NoPadding);
        if (TextBoxWrapPanel.IsTab(text))
          size.Width = RadGdiGraphics.GetTextMetric(font).aveCharWidth * 4;
        return size;
      }
    }

    public static int BinarySearchWrapIndex(string text, Font font, float availableWidth)
    {
      if (text.Length == 0)
        return 0;
      int length = -1;
      int num1 = 0;
      int num2 = text.Length - 1;
      string empty = string.Empty;
      float num3 = float.MaxValue;
      int num4 = 0;
      while (num1 <= num2)
      {
        length = (num2 + num1) / 2;
        int width = TextBoxControlMeasurer.MeasureText(text.Substring(0, length), font).Width;
        if ((double) num3 < (double) availableWidth && (double) width > (double) availableWidth)
        {
          length = num4;
          break;
        }
        if ((double) width != (double) availableWidth)
        {
          if ((double) width < (double) availableWidth)
            num1 = length + 1;
          else
            num2 = length - 1;
          num4 = length;
          num3 = (float) width;
        }
        else
          break;
      }
      while (length < text.Length && (double) TextBoxControlMeasurer.MeasureText(text.Substring(0, length + 1), font).Width <= (double) availableWidth)
        ++length;
      return length;
    }

    public static int BinarySearchCaretIndex(string text, Font font, double availableWidth)
    {
      if (text.Length == 0)
        return 0;
      int index = -1;
      int num1 = 0;
      int num2 = text.Length - 1;
      int num3 = 0;
      while (num1 <= num2)
      {
        index = num1 + (num2 - num1 >> 1);
        num3 = TextBoxControlMeasurer.MeasureText(text.Substring(0, index + 1), font).Width;
        if ((double) num3 <= availableWidth)
          num1 = index + 1;
        else
          num2 = index - 1;
      }
      if (index + 1 < text.Length)
        ++index;
      else
        num3 = TextBoxControlMeasurer.MeasureText(text.Substring(0, index), font).Width;
      if ((double) num3 != availableWidth)
      {
        int num4 = TextBoxControlMeasurer.MeasureText(text[index].ToString(), font).Width / 2;
        if ((double) num3 < availableWidth)
        {
          if ((double) (num3 + num4) <= availableWidth)
            ++index;
        }
        else if ((double) (num3 - num4) > availableWidth)
          --index;
      }
      return Math.Max(index, 0);
    }
  }
}
