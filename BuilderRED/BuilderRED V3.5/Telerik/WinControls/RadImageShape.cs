// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadImageShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (RadImageShapeTypeConverter))]
  [Editor("Telerik.WinControls.UI.Design.RadImageShapeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  public class RadImageShape : ICloneable
  {
    private SizeF dpiScale = new SizeF(1f, 1f);
    public const string SerializationSeparator = ";";
    private Image image;
    private InterpolationMode interpolationMode;
    private ImagePaintMode paintMode;
    private RotateFlipType rotateFlip;
    private bool useSegments;
    private ImageSegments visibleSegments;
    private Padding margins;
    private Padding padding;
    private float alpha;
    private RadImageSegment[] segments;
    private Image cachedImage;
    private bool imageDirty;
    private bool segmentsDirty;

    public RadImageShape()
    {
      this.interpolationMode = InterpolationMode.NearestNeighbor;
      this.paintMode = ImagePaintMode.Stretch;
      this.rotateFlip = RotateFlipType.RotateNoneFlipNone;
      this.useSegments = true;
      this.visibleSegments = ImageSegments.All;
      this.alpha = 1f;
      this.imageDirty = true;
      this.segmentsDirty = true;
    }

    [DefaultValue(RotateFlipType.RotateNoneFlipNone)]
    [Description("Gets or sets the RotateFlipType value that defines additional transform on the rendered image.")]
    public RotateFlipType RotateFlip
    {
      get
      {
        return this.rotateFlip;
      }
      set
      {
        if (this.rotateFlip == value)
          return;
        this.rotateFlip = value;
        this.imageDirty = true;
        this.segmentsDirty = true;
      }
    }

    [DefaultValue(InterpolationMode.NearestNeighbor)]
    [Description("Gets or sets the interpolation mode to be applied on the device context when image is rendered.")]
    public InterpolationMode InterpolationMode
    {
      get
      {
        return this.interpolationMode;
      }
      set
      {
        if (value == InterpolationMode.Default || value == InterpolationMode.Invalid)
          value = InterpolationMode.NearestNeighbor;
        this.interpolationMode = value;
      }
    }

    [DefaultValue(ImageSegments.All)]
    [Description("Determines which segments from the image will be painted.")]
    public ImageSegments VisibleSegments
    {
      get
      {
        return this.visibleSegments;
      }
      set
      {
        if (this.visibleSegments == value)
          return;
        this.visibleSegments = value;
      }
    }

    [Description("Determines whether the image will be rendered using segments.")]
    [DefaultValue(true)]
    public bool UseSegments
    {
      get
      {
        return this.useSegments;
      }
      set
      {
        if (this.useSegments == value)
          return;
        this.useSegments = value;
        this.segmentsDirty = true;
      }
    }

    [Description("Gets or sets the mode to be used when image is painted.")]
    [DefaultValue(ImagePaintMode.Stretch)]
    public ImagePaintMode PaintMode
    {
      get
      {
        return this.paintMode;
      }
      set
      {
        if (this.paintMode == value)
          return;
        this.paintMode = value;
      }
    }

    [XmlIgnore]
    [Description("Gets or sets the image to be rendered.")]
    [DefaultValue(null)]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        if (this.image == value)
          return;
        this.image = value;
        this.imageDirty = true;
        this.segmentsDirty = true;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ImageStream
    {
      get
      {
        if (this.image != null)
          return TelerikHelper.ImageToString(this.image);
        return string.Empty;
      }
      set
      {
        Image image = (Image) null;
        if (!string.IsNullOrEmpty(value))
          image = TelerikHelper.ImageFromString(value);
        this.Image = image;
      }
    }

    [DefaultValue(1f)]
    [Description("Gets or sets the opacity of the rendered image. Valid values are within the interval [0, 1].")]
    public float Alpha
    {
      get
      {
        return this.alpha;
      }
      set
      {
        value = Math.Max(0.0f, value);
        value = Math.Min(1f, value);
        if ((double) this.alpha == (double) value)
          return;
        this.alpha = value;
        this.imageDirty = true;
        this.segmentsDirty = true;
      }
    }

    [Description("Gets or sets the Padding structure that defines the margins of the segmented image.")]
    public Padding Margins
    {
      get
      {
        return this.margins;
      }
      set
      {
        if (this.margins == value)
          return;
        this.margins = value;
        this.segmentsDirty = true;
      }
    }

    [Description("Gets or sets the Padding structure that defines offset when the image is rendered to the destination rectangle.")]
    public Padding Padding
    {
      get
      {
        return this.padding;
      }
      set
      {
        if (this.padding == value)
          return;
        this.padding = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeImageStream()
    {
      return this.image != null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeMargins()
    {
      return this.margins != Padding.Empty;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializePadding()
    {
      return this.padding != Padding.Empty;
    }

    public object Clone()
    {
      RadImageShape radImageShape = new RadImageShape();
      if (this.image != null)
        radImageShape.image = this.image.Clone() as Image;
      radImageShape.alpha = this.alpha;
      radImageShape.interpolationMode = this.interpolationMode;
      radImageShape.margins = this.margins;
      radImageShape.padding = this.padding;
      radImageShape.paintMode = this.paintMode;
      radImageShape.rotateFlip = this.rotateFlip;
      radImageShape.useSegments = this.useSegments;
      radImageShape.visibleSegments = this.visibleSegments;
      return (object) radImageShape;
    }

    public RadImageSegment GetSegment(ImageSegments segment)
    {
      if (this.segments != null)
      {
        int length = this.segments.Length;
        for (int index = 0; index < length; ++index)
        {
          if (this.segments[index].Segment == segment)
            return this.segments[index];
        }
      }
      return (RadImageSegment) null;
    }

    public virtual void Paint(Graphics g, RectangleF bounds)
    {
      this.Paint(g, bounds, new SizeF(1f, 1f));
    }

    public virtual void Paint(Graphics g, RectangleF bounds, SizeF dpiScale)
    {
      lock (Locker.SyncObj)
      {
        Rectangle paintRect = LayoutUtils.DeflateRect(Rectangle.Round(bounds), this.padding);
        this.dpiScale = dpiScale;
        if (paintRect.Width <= 0 || paintRect.Height <= 0)
          return;
        InterpolationMode interpolationMode = g.InterpolationMode;
        g.InterpolationMode = this.interpolationMode;
        if (this.imageDirty)
          this.ResetImageCache();
        if (this.segmentsDirty)
          this.ResetSegments();
        if (this.cachedImage != null)
          this.PaintCore(g, paintRect);
        g.InterpolationMode = interpolationMode;
      }
    }

    protected virtual void PaintCore(Graphics g, Rectangle paintRect)
    {
      switch (this.paintMode)
      {
        case ImagePaintMode.None:
          g.ScaleTransform(this.dpiScale.Width, this.dpiScale.Height);
          g.DrawImageUnscaledAndClipped(this.cachedImage, new Rectangle(paintRect.Location, this.cachedImage.Size));
          g.ScaleTransform(1f / this.dpiScale.Width, 1f / this.dpiScale.Height);
          break;
        case ImagePaintMode.Stretch:
          this.PaintSegmented(g, paintRect);
          break;
        case ImagePaintMode.StretchXTileY:
          this.PaintStretchXTileY(g, paintRect);
          break;
        case ImagePaintMode.StretchYTileX:
          this.PaintStretchYTileX(g, paintRect);
          break;
        case ImagePaintMode.StretchXYTileInner:
          this.PaintStretchXYTileInner(g, paintRect);
          break;
        case ImagePaintMode.Center:
          g.DrawImageUnscaledAndClipped(this.cachedImage, this.CenterRect(this.cachedImage.Size, paintRect));
          break;
        case ImagePaintMode.CenterXStretchY:
          this.PaintSegmented(g, this.CenterRectX(this.cachedImage.Size, paintRect));
          break;
        case ImagePaintMode.CenterYStretchX:
          this.PaintSegmented(g, this.CenterRectY(this.cachedImage.Size, paintRect));
          break;
        case ImagePaintMode.CenterXTileY:
          this.PaintCenterXTileY(g, paintRect);
          break;
        case ImagePaintMode.CenterYTileX:
          this.PaintCenterYTileX(g, paintRect);
          break;
        case ImagePaintMode.Tile:
        case ImagePaintMode.TileFlipX:
        case ImagePaintMode.TileFlipXY:
        case ImagePaintMode.TileFlipY:
          TextureBrush textureBrush = new TextureBrush(this.cachedImage, this.GetWrapMode());
          textureBrush.TranslateTransform((float) paintRect.X, (float) paintRect.Y, MatrixOrder.Prepend);
          g.FillRectangle((Brush) textureBrush, paintRect);
          textureBrush.Dispose();
          break;
      }
    }

    private void PaintStretchXYTileInner(Graphics g, Rectangle paintRect)
    {
      ImageSegments visibleSegments = this.visibleSegments;
      this.visibleSegments &= ~ImageSegments.Inner;
      this.PaintSegmented(g, paintRect);
      this.visibleSegments = visibleSegments;
      if ((this.visibleSegments & ImageSegments.Inner) == (ImageSegments) 0)
        return;
      RadImageSegment segment = this.GetSegment(ImageSegments.Inner);
      if (segment.ImagePart == null)
        return;
      Padding paintMargins = this.GetPaintMargins(paintRect);
      Rectangle destinationRect = segment.GetDestinationRect(paintRect, paintMargins);
      if (destinationRect.Width <= 0 || destinationRect.Height <= 0)
        return;
      if (segment.RenderMargins != paintMargins)
        segment.UpdateSourceRect(this.cachedImage.Size, paintMargins);
      TextureBrush textureBrush = new TextureBrush(segment.ImagePart, WrapMode.Tile);
      textureBrush.TranslateTransform((float) destinationRect.X, (float) destinationRect.Y, MatrixOrder.Prepend);
      g.FillRectangle((Brush) textureBrush, destinationRect);
      textureBrush.Dispose();
    }

    private void PaintStretchYTileX(Graphics g, Rectangle paintRect)
    {
      Image image = (Image) new Bitmap(this.cachedImage.Width, paintRect.Height, PixelFormat.Format32bppArgb);
      Graphics g1 = Graphics.FromImage(image);
      g1.InterpolationMode = this.interpolationMode;
      this.PaintSegmented(g1, new Rectangle(0, 0, this.cachedImage.Width, paintRect.Height));
      TextureBrush textureBrush = new TextureBrush(image, WrapMode.Tile);
      textureBrush.TranslateTransform((float) paintRect.X, (float) paintRect.Y, MatrixOrder.Prepend);
      g.FillRectangle((Brush) textureBrush, paintRect);
      g1.Dispose();
      image.Dispose();
      textureBrush.Dispose();
    }

    private void PaintStretchXTileY(Graphics g, Rectangle paintRect)
    {
      Image image = (Image) new Bitmap(paintRect.Width, this.cachedImage.Height, PixelFormat.Format32bppArgb);
      Graphics g1 = Graphics.FromImage(image);
      g1.InterpolationMode = this.interpolationMode;
      this.PaintSegmented(g1, new Rectangle(0, 0, paintRect.Width, this.cachedImage.Height));
      TextureBrush textureBrush = new TextureBrush(image, WrapMode.Tile);
      textureBrush.TranslateTransform((float) paintRect.X, (float) paintRect.Y, MatrixOrder.Prepend);
      g.FillRectangle((Brush) textureBrush, paintRect);
      g1.Dispose();
      image.Dispose();
      textureBrush.Dispose();
    }

    private void PaintSegmented(Graphics g, Rectangle paintRect)
    {
      if (!this.useSegments)
      {
        g.DrawImage(this.cachedImage, paintRect);
      }
      else
      {
        Padding paintMargins = this.GetPaintMargins(paintRect);
        int length = this.segments.Length;
        for (int index = 0; index < length; ++index)
        {
          RadImageSegment segment = this.segments[index];
          if ((segment.Segment & this.visibleSegments) != (ImageSegments) 0)
          {
            Rectangle destinationRect = segment.GetDestinationRect(paintRect, paintMargins);
            if (destinationRect.Width > 0 && destinationRect.Height > 0)
            {
              if (segment.RenderMargins != paintMargins)
                segment.UpdateSourceRect(this.cachedImage.Size, paintMargins);
              g.DrawImage(this.cachedImage, destinationRect, segment.SourceRect, GraphicsUnit.Pixel);
            }
          }
        }
      }
    }

    private void PaintCenterXTileY(Graphics g, Rectangle paintRect)
    {
      paintRect = this.CenterRectX(this.cachedImage.Size, paintRect);
      Image image = (Image) new Bitmap(paintRect.Width, this.cachedImage.Height, PixelFormat.Format32bppArgb);
      Graphics g1 = Graphics.FromImage(image);
      g1.InterpolationMode = this.interpolationMode;
      this.PaintSegmented(g1, new Rectangle(0, 0, paintRect.Width, image.Height));
      TextureBrush textureBrush = new TextureBrush(image, WrapMode.Tile);
      textureBrush.TranslateTransform((float) paintRect.X, (float) paintRect.Y, MatrixOrder.Prepend);
      g.FillRectangle((Brush) textureBrush, paintRect);
      g1.Dispose();
      image.Dispose();
      textureBrush.Dispose();
    }

    private void PaintCenterYTileX(Graphics g, Rectangle paintRect)
    {
      paintRect = this.CenterRectY(this.cachedImage.Size, paintRect);
      Image image = (Image) new Bitmap(this.cachedImage.Width, paintRect.Height, PixelFormat.Format32bppArgb);
      Graphics g1 = Graphics.FromImage(image);
      g1.InterpolationMode = this.interpolationMode;
      this.PaintSegmented(g1, new Rectangle(0, 0, this.cachedImage.Width, paintRect.Height));
      TextureBrush textureBrush = new TextureBrush(image, WrapMode.Tile);
      textureBrush.TranslateTransform((float) paintRect.X, (float) paintRect.Y, MatrixOrder.Prepend);
      g.FillRectangle((Brush) textureBrush, paintRect);
      g1.Dispose();
      image.Dispose();
      textureBrush.Dispose();
    }

    public void Rotate(int degree)
    {
      if (degree < 0)
        degree = 360 + degree;
      if (degree % 90 > 0)
        throw new ArgumentException("Degree should be divided by 90 without remainder");
      RotateFlipType rotateFlipType = this.rotateFlip;
      switch (degree)
      {
        case 90:
          rotateFlipType = RotateFlipType.Rotate90FlipNone;
          break;
        case 180:
          rotateFlipType = RotateFlipType.Rotate180FlipNone;
          break;
        case 270:
          rotateFlipType = RotateFlipType.Rotate270FlipNone;
          break;
      }
      this.Margins = LayoutUtils.RotateMargin(this.margins, degree);
      this.padding = LayoutUtils.RotateMargin(this.padding, degree);
      this.RotateFlip = rotateFlipType;
    }

    public static string Serialize(RadImageShape shape)
    {
      if (shape == null)
        return string.Empty;
      MemoryStream memoryStream = new MemoryStream();
      XmlTextWriter xmlTextWriter = new XmlTextWriter((Stream) memoryStream, Encoding.UTF8);
      new XmlSerializer(typeof (RadImageShape)).Serialize((XmlWriter) xmlTextWriter, (object) shape);
      xmlTextWriter.Flush();
      string base64String = Convert.ToBase64String(memoryStream.ToArray());
      xmlTextWriter.Close();
      memoryStream.Close();
      return base64String;
    }

    public static RadImageShape Deserialize(string state)
    {
      if (string.IsNullOrEmpty(state))
        return (RadImageShape) null;
      MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(state));
      XmlTextReader xmlTextReader = new XmlTextReader((Stream) memoryStream);
      RadImageShape radImageShape = new XmlSerializer(typeof (RadImageShape)).Deserialize((XmlReader) xmlTextReader) as RadImageShape;
      xmlTextReader.Close();
      memoryStream.Close();
      return radImageShape;
    }

    private Rectangle CenterRectX(Size size, Rectangle rect)
    {
      rect.X += (rect.Width - size.Width) / 2;
      rect.Width = size.Width;
      return rect;
    }

    private Rectangle CenterRectY(Size size, Rectangle rect)
    {
      rect.Y += (rect.Height - size.Height) / 2;
      rect.Height = size.Height;
      return rect;
    }

    private Padding GetPaintMargins(Rectangle paintRect)
    {
      Padding margins = this.margins;
      int num1 = this.margins.Horizontal - paintRect.Width + 1;
      if (num1 > 0)
      {
        float num2 = (float) this.margins.Left / (float) this.margins.Horizontal;
        float num3 = (float) this.margins.Right / (float) this.margins.Horizontal;
        int num4 = (int) ((double) num2 * (double) num1);
        margins.Left -= num4;
        int num5 = (int) ((double) num3 * (double) num1);
        margins.Right -= num4;
        int num6 = num1 - (num4 + num5);
        if (num6 > 0)
        {
          if (this.margins.Left >= this.margins.Right)
            margins.Left -= num6;
          else
            margins.Right -= num6;
        }
      }
      int num7 = this.margins.Vertical - paintRect.Height;
      if (num7 > 0)
      {
        float num2 = (float) this.margins.Top / (float) this.margins.Vertical;
        float num3 = (float) this.margins.Bottom / (float) this.margins.Vertical;
        int num4 = (int) ((double) num2 * (double) num7);
        margins.Top -= num4;
        int num5 = (int) ((double) num3 * (double) num7);
        margins.Bottom -= num5;
        int num6 = num7 - (num4 + num5);
        if (num6 > 0)
        {
          if (this.margins.Top >= this.margins.Bottom)
            margins.Top -= num6;
          else
            margins.Bottom -= num6;
        }
      }
      return margins;
    }

    private Rectangle CenterRect(Size size, Rectangle rect)
    {
      int num1 = (rect.Width - size.Width) / 2;
      int num2 = (rect.Height - size.Height) / 2;
      return new Rectangle(rect.X + num1, rect.Y + num2, size.Width, size.Height);
    }

    private WrapMode GetWrapMode()
    {
      WrapMode wrapMode = WrapMode.Clamp;
      switch (this.paintMode)
      {
        case ImagePaintMode.Tile:
          wrapMode = WrapMode.Tile;
          break;
        case ImagePaintMode.TileFlipX:
          wrapMode = WrapMode.TileFlipX;
          break;
        case ImagePaintMode.TileFlipXY:
          wrapMode = WrapMode.TileFlipXY;
          break;
        case ImagePaintMode.TileFlipY:
          wrapMode = WrapMode.TileFlipY;
          break;
      }
      return wrapMode;
    }

    private void ResetImageCache()
    {
      if (this.cachedImage != null)
      {
        this.cachedImage.Dispose();
        this.cachedImage = (Image) null;
      }
      this.CacheImage();
      this.imageDirty = false;
      this.segmentsDirty = true;
    }

    private void ResetSegments()
    {
      this.DisposeSegments();
      this.BuildSegments();
      this.segmentsDirty = false;
    }

    private void CacheImage()
    {
      if (this.image == null)
        return;
      this.cachedImage = this.image.Clone() as Image;
      Bitmap cachedImage = this.cachedImage as Bitmap;
      if (cachedImage == null)
        return;
      if ((double) this.alpha < 1.0)
        ImageHelper.ApplyAlpha(cachedImage, this.alpha);
      if (this.rotateFlip == RotateFlipType.RotateNoneFlipNone)
        return;
      cachedImage.RotateFlip(this.rotateFlip);
    }

    private void BuildSegments()
    {
      if (this.cachedImage == null || !this.useSegments)
        return;
      this.segments = new RadImageSegment[9];
      for (int index = 0; index < 9; ++index)
        this.segments[index] = new RadImageSegment((ImageSegments) Math.Pow(2.0, (double) index));
      this.UpdateSegments();
    }

    private void UpdateSegments()
    {
      int length = this.segments.Length;
      for (int index = 0; index < length; ++index)
        this.segments[index].UpdateFromImage(this.cachedImage, this.margins);
    }

    private void DisposeSegments()
    {
      if (this.segments == null)
        return;
      int length = this.segments.Length;
      for (int index = 0; index < length; ++index)
        this.segments[index].Dispose();
      this.segments = (RadImageSegment[]) null;
    }
  }
}
