// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadImageSegment
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadImageSegment
  {
    private ImageSegments segment;
    private Image imagePart;
    private Rectangle sourceRect;
    private Padding renderMargins;

    public RadImageSegment(ImageSegments segment)
    {
      this.segment = segment;
    }

    public Padding RenderMargins
    {
      get
      {
        return this.renderMargins;
      }
    }

    public Rectangle SourceRect
    {
      get
      {
        return this.sourceRect;
      }
    }

    public ImageSegments Segment
    {
      get
      {
        return this.segment;
      }
    }

    public Image ImagePart
    {
      get
      {
        return this.imagePart;
      }
      set
      {
        this.imagePart = value;
      }
    }

    public void Dispose()
    {
      if (this.imagePart == null)
        return;
      this.imagePart.Dispose();
      this.imagePart = (Image) null;
    }

    public Rectangle GetDestinationRect(Rectangle paintRect, Padding margins)
    {
      Rectangle rectangle = Rectangle.Empty;
      switch (this.segment)
      {
        case ImageSegments.Left:
          rectangle = new Rectangle(paintRect.X, paintRect.Y + margins.Top, margins.Left, paintRect.Height - margins.Vertical);
          break;
        case ImageSegments.TopLeft:
          rectangle = new Rectangle(paintRect.X, paintRect.Y, margins.Left, margins.Top);
          break;
        case ImageSegments.Top:
          rectangle = new Rectangle(paintRect.X + margins.Left, paintRect.Y, paintRect.Width - margins.Horizontal, margins.Top);
          break;
        case ImageSegments.TopRight:
          rectangle = new Rectangle(paintRect.Right - margins.Right, paintRect.Y, margins.Right, margins.Top);
          break;
        case ImageSegments.Right:
          rectangle = new Rectangle(paintRect.Right - margins.Right, paintRect.Y + margins.Top, margins.Right, paintRect.Height - margins.Vertical);
          break;
        case ImageSegments.BottomRight:
          rectangle = new Rectangle(paintRect.Right - margins.Right, paintRect.Bottom - margins.Bottom, margins.Right, margins.Bottom);
          break;
        case ImageSegments.Bottom:
          rectangle = new Rectangle(paintRect.X + margins.Left, paintRect.Bottom - margins.Bottom, paintRect.Width - margins.Horizontal, margins.Bottom);
          break;
        case ImageSegments.BottomLeft:
          rectangle = new Rectangle(paintRect.X, paintRect.Bottom - margins.Bottom, margins.Left, margins.Bottom);
          break;
        case ImageSegments.Inner:
          rectangle = new Rectangle(paintRect.X + margins.Left, paintRect.Y + margins.Top, paintRect.Width - margins.Horizontal, paintRect.Height - margins.Vertical);
          break;
      }
      return rectangle;
    }

    public void UpdateSourceRect(Size imageSize, Padding margins)
    {
      this.renderMargins = margins;
      switch (this.segment)
      {
        case ImageSegments.Left:
          this.sourceRect = new Rectangle(0, margins.Top, margins.Left, imageSize.Height - margins.Vertical);
          break;
        case ImageSegments.TopLeft:
          this.sourceRect = new Rectangle(0, 0, margins.Left, margins.Top);
          break;
        case ImageSegments.Top:
          this.sourceRect = new Rectangle(margins.Left, 0, imageSize.Width - margins.Horizontal, margins.Top);
          break;
        case ImageSegments.TopRight:
          this.sourceRect = new Rectangle(imageSize.Width - margins.Right, 0, margins.Right, margins.Top);
          break;
        case ImageSegments.Right:
          this.sourceRect = new Rectangle(imageSize.Width - margins.Right, margins.Top, margins.Right, imageSize.Height - margins.Vertical);
          break;
        case ImageSegments.BottomRight:
          this.sourceRect = new Rectangle(imageSize.Width - margins.Right, imageSize.Height - margins.Bottom, margins.Right, margins.Bottom);
          break;
        case ImageSegments.Bottom:
          this.sourceRect = new Rectangle(margins.Left, imageSize.Height - margins.Bottom, imageSize.Width - margins.Horizontal, margins.Bottom);
          break;
        case ImageSegments.BottomLeft:
          this.sourceRect = new Rectangle(0, imageSize.Height - margins.Bottom, margins.Left, margins.Bottom);
          break;
        case ImageSegments.Inner:
          this.sourceRect = new Rectangle(margins.Left, margins.Top, imageSize.Width - margins.Horizontal, imageSize.Height - margins.Vertical);
          break;
      }
    }

    public void UpdateFromImage(Image image, Padding margins)
    {
      this.UpdateSourceRect(image.Size, margins);
      this.imagePart = this.GetImagePart(image);
    }

    private Image GetImagePart(Image image)
    {
      int width = this.sourceRect.Width;
      int height = this.sourceRect.Height;
      if (width <= 0 || height <= 0)
        return (Image) null;
      Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      Rectangle destRect = new Rectangle(0, 0, width, height);
      graphics.DrawImage(image, destRect, this.sourceRect.X, this.sourceRect.Y, width, height, GraphicsUnit.Pixel);
      graphics.Dispose();
      return (Image) bitmap;
    }
  }
}
