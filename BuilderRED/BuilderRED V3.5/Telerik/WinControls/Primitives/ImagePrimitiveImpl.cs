// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ImagePrimitiveImpl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ImagePrimitiveImpl
  {
    private readonly IImageElement imageElement;

    public ImagePrimitiveImpl(IImageElement imageElement)
    {
      this.imageElement = imageElement;
    }

    public virtual void PaintImage(
      IGraphics graphics,
      Image image,
      RectangleF rect,
      ImageLayout imageLayout,
      ContentAlignment imageAlignment,
      float opacity,
      bool rtl)
    {
      SizeF size = rect.Size;
      if (image == null || size == SizeF.Empty)
        return;
      if (rtl)
        imageAlignment = TelerikAlignHelper.RtlTranslateContent(imageAlignment);
      switch (imageLayout)
      {
        case ImageLayout.None:
          RectangleF rectangleF = LayoutUtils.Align(new SizeF(Math.Min(rect.Width, (float) image.Size.Width), Math.Min(rect.Height, (float) image.Size.Height)), rect, imageAlignment);
          ((Graphics) graphics.UnderlayGraphics).DrawImageUnscaledAndClipped(image, new Rectangle(new Point((int) rectangleF.Location.X, (int) rectangleF.Location.Y), rectangleF.Size.ToSize()));
          break;
        case ImageLayout.Tile:
          float num1 = rect.Y - (float) image.Height;
          do
          {
            num1 += (float) image.Height;
            float num2 = rect.X - (float) image.Width;
            do
            {
              num2 += (float) image.Width;
              if ((double) opacity == 1.0)
                graphics.DrawBitmap(image, (int) num2, (int) num1);
              else
                graphics.DrawBitmap(image, (int) num2, (int) num1, (double) opacity);
            }
            while ((double) num2 < (double) rect.X + (double) rect.Width - (double) image.Width);
          }
          while ((double) num1 <= (double) rect.Y + (double) rect.Height - (double) image.Height);
          break;
        case ImageLayout.Center:
          PointF pointF = new PointF(Math.Max(0.0f, rect.X + Math.Max(0.0f, (float) (((double) size.Width - (double) image.Width) / 2.0))), Math.Max(0.0f, rect.Y + Math.Max(0.0f, (float) (((double) size.Height - (double) image.Height) / 2.0))));
          pointF.X = Math.Min(pointF.X, rect.Right);
          pointF.Y = Math.Min(pointF.Y, rect.Bottom);
          SizeF sizeF = new SizeF(Math.Min(rect.Width, (float) image.Size.Width), Math.Min(rect.Height, (float) image.Size.Height));
          if ((double) opacity == 1.0)
          {
            ((Graphics) graphics.UnderlayGraphics).DrawImageUnscaledAndClipped(image, new Rectangle(new Point((int) pointF.X, (int) pointF.Y), sizeF.ToSize()));
            break;
          }
          graphics.DrawBitmap(image, (int) pointF.X, (int) pointF.Y, (double) opacity);
          break;
        case ImageLayout.Stretch:
          if ((double) opacity == 1.0)
          {
            graphics.DrawBitmap(image, (int) rect.X, (int) rect.Y, (int) size.Width, (int) size.Height);
            break;
          }
          graphics.DrawBitmap(image, 0, 0, (int) size.Width, (int) size.Height, (double) opacity);
          break;
        case ImageLayout.Zoom:
          if (image.Width == 0 || image.Height == 0)
            break;
          float num3 = Math.Min(size.Width / (float) image.Width, size.Height / (float) image.Height);
          if ((double) num3 <= 0.0)
            break;
          int width = (int) Math.Round((double) image.Width * (double) num3);
          int height = (int) Math.Round((double) image.Height * (double) num3);
          int x = (int) rect.X + (int) (((double) size.Width - (double) width) / 2.0);
          int y = (int) rect.Y + (int) (((double) size.Height - (double) height) / 2.0);
          if ((double) opacity == 1.0)
          {
            graphics.DrawBitmap(image, x, y, width, height);
            break;
          }
          graphics.DrawBitmap(image, x, y, width, height, (double) opacity);
          break;
      }
    }
  }
}
