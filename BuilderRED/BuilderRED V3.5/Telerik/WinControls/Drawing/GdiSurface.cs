// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GdiSurface
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.Drawing
{
  public class GdiSurface : Surface
  {
    private TextRendererMode textRendererMode = TextRendererMode.GdiPlus;
    private const string MeasureGraphicsKey = "MeasureGraphics";
    private Graphics graphics;
    private GdiSurface.DrawBorderElement[] singleBorderParts;
    private GdiSurface.DrawBorderElement[] borderParts;

    public GdiSurface(Graphics graphics)
      : base((object) graphics)
    {
      this.graphics = graphics;
      this.borderParts = new GdiSurface.DrawBorderElement[3];
      this.borderParts[0] = new GdiSurface.DrawBorderElement(this.DrawSingeBorder);
      this.borderParts[1] = new GdiSurface.DrawBorderElement(this.DrawFourBorders);
      this.borderParts[2] = new GdiSurface.DrawBorderElement(this.DrawOuterInnerBorder);
      this.singleBorderParts = new GdiSurface.DrawBorderElement[3];
      this.singleBorderParts[0] = new GdiSurface.DrawBorderElement(this.DrawSolidBorder);
      this.singleBorderParts[1] = new GdiSurface.DrawBorderElement(this.DrawLinearBorder);
      this.singleBorderParts[2] = new GdiSurface.DrawBorderElement(this.DrawRadialBorder);
    }

    public TextRendererMode TextRendererMode
    {
      get
      {
        return this.textRendererMode;
      }
      set
      {
        this.textRendererMode = value;
      }
    }

    public override Path CreatePath()
    {
      return (Path) new GdiPath();
    }

    public override RadBrush CreateSolidBrush(Color color)
    {
      return (RadBrush) new GdiSolidBrush(color);
    }

    public override RadBrush CreateRadialBrush(
      PointF center,
      float xRadius,
      float yRadius,
      GradientStop[] colorStops)
    {
      return (RadBrush) new GdiRadialGradientBrush(center, xRadius, yRadius, colorStops);
    }

    public override RadBrush CreateLinearGradientBrush(
      RectangleF rectangle,
      GradientStop[] colorStops,
      float angle)
    {
      return (RadBrush) new GdiLinearGradientBrush(rectangle, colorStops, angle);
    }

    public override RadBrush CreateLinearGradientBrush(
      RectangleF rectangle,
      GradientStop[] colorStops,
      RadLinearGradientMode mode)
    {
      return (RadBrush) new GdiLinearGradientBrush(rectangle, colorStops, mode);
    }

    public override void BeginDraw(params object[] handles)
    {
      if (handles == null || handles.Length <= 0)
        return;
      this.graphics = handles[0] as Graphics;
    }

    public override void EndDraw()
    {
      object obj = (object) null;
      if (!this.Resources.TryGetValue("MeasureGraphics", out obj))
        return;
      this.graphics = obj as Graphics;
    }

    public override void DrawBorder(IBorderElement element, RectangleF rect)
    {
      this.borderParts[(int) element.BoxStyle](element, rect);
    }

    public override void DrawBorder(IBorderElement element, Path path)
    {
      Pen pen;
      if (element.GradientStyle == GradientStyles.Solid)
        pen = new Pen(element.ForeColor);
      else if (element.GradientStyle == GradientStyles.Linear)
      {
        GradientStop[] colorStops = new GradientStop[4]{ new GradientStop(element.ForeColor, 0.0f), new GradientStop(element.ForeColor2, 0.333f), new GradientStop(element.ForeColor3, 0.666f), new GradientStop(element.ForeColor4, 1f) };
        pen = new Pen((Brush) this.CreateLinearGradientBrush(new RectangleF(0.0f, 0.0f, element.BorderSize.Width, element.BorderSize.Height), colorStops, element.GradientAngle).RawBrush);
      }
      else
      {
        GradientStop[] colorStops = new GradientStop[4]{ new GradientStop(element.ForeColor, 0.0f), new GradientStop(element.ForeColor2, 0.333f), new GradientStop(element.ForeColor3, 0.666f), new GradientStop(element.ForeColor4, 1f) };
        float num1 = element.BorderSize.Width / 2f;
        float num2 = element.BorderSize.Height / 2f;
        pen = new Pen((Brush) this.CreateRadialBrush(new PointF(num1, num2), num1, num2, colorStops).RawBrush);
      }
      pen.Alignment = (double) element.Width < 2.0 ? PenAlignment.Inset : PenAlignment.Center;
      pen.Width = element.Width;
      pen.DashStyle = element.BorderDashStyle;
      this.graphics.DrawPath(pen, (GraphicsPath) path.RawPath);
      pen.Dispose();
    }

    public override void DrawText(ITextElement element, RadBrush brush, RectangleF rect)
    {
      if (this.textRendererMode == TextRendererMode.GdiPlus)
        this.graphics.DrawString(element.Text, element.Font, (Brush) brush.RawBrush, rect, (StringFormat) element.TextFormat);
      else
        TextRenderer.DrawText((IDeviceContext) this.graphics, element.Text, element.Font, Point.Round(rect.Location), element.ForeColor, element.TextFormat.TextFormatFlags);
    }

    public override SizeF MeasureText(ITextElement element, SizeF availableSize)
    {
      if (this.graphics == null)
      {
        if (!this.Resources.ContainsKey("MeasureGraphics"))
          return SizeF.Empty;
        this.graphics = this.Resources["MeasureGraphics"] as Graphics;
      }
      if (this.textRendererMode == TextRendererMode.GdiPlus)
      {
        if (element.TextFormat != null)
          return this.graphics.MeasureString(element.Text, element.Font, availableSize, (StringFormat) element.TextFormat);
        return this.graphics.MeasureString(element.Text, element.Font, availableSize);
      }
      if (element.TextFormat != null)
        return (SizeF) TextRenderer.MeasureText((IDeviceContext) this.graphics, element.Text, element.Font, availableSize.ToSize(), element.TextFormat.TextFormatFlags);
      return (SizeF) TextRenderer.MeasureText((IDeviceContext) this.graphics, element.Text, element.Font, availableSize.ToSize());
    }

    public override void DrawText(
      string text,
      Font font,
      RadBrush brush,
      RectangleF rect,
      TextFormat textFormat)
    {
      if (this.textRendererMode == TextRendererMode.GdiPlus)
      {
        if (textFormat != null)
          this.graphics.DrawString(text, font, (Brush) brush.RawBrush, rect, (StringFormat) textFormat);
        else
          this.graphics.DrawString(text, font, (Brush) brush.RawBrush, rect);
      }
      else if (textFormat != null)
        TextRenderer.DrawText((IDeviceContext) this.graphics, text, font, Point.Round(rect.Location), ((SolidBrush) brush.RawBrush).Color, textFormat.TextFormatFlags);
      else
        TextRenderer.DrawText((IDeviceContext) this.graphics, text, font, Point.Round(rect.Location), ((SolidBrush) brush.RawBrush).Color);
    }

    public override SizeF MeasureText(
      string text,
      Font font,
      SizeF availableSize,
      TextFormat textFormat)
    {
      if (this.graphics == null)
        this.graphics = this.Resources["MeasureGraphics"] as Graphics;
      if (this.textRendererMode == TextRendererMode.GdiPlus)
      {
        if (textFormat != null)
          return this.graphics.MeasureString(text, font, availableSize, (StringFormat) textFormat);
        return this.graphics.MeasureString(text, font, availableSize);
      }
      if (textFormat != null)
        return (SizeF) TextRenderer.MeasureText((IDeviceContext) this.graphics, text, font, availableSize.ToSize(), textFormat.TextFormatFlags);
      return (SizeF) TextRenderer.MeasureText((IDeviceContext) this.graphics, text, font, availableSize.ToSize());
    }

    public override void DrawImage(IImageElement element, RectangleF rect)
    {
      SizeF size = rect.Size;
      if (element.Image == null)
        return;
      int num = size == SizeF.Empty ? 1 : 0;
    }

    public override void DrawImage(Image image, RectangleF rect)
    {
      this.graphics.DrawImage(image, rect);
    }

    public override RoundedRectangle CreateRoundedRectangle(
      RectangleF rect,
      float radius)
    {
      return (RoundedRectangle) new GdiRoundedRectangle(rect, radius);
    }

    protected override Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    private void DrawSingeBorder(IBorderElement element, RectangleF rect)
    {
      this.singleBorderParts[(int) element.GradientStyle](element, rect);
    }

    private void DrawOuterInnerBorder(IBorderElement element, RectangleF rect)
    {
      this.singleBorderParts[(int) element.GradientStyle](element, rect);
      float num = -(float) Math.Floor((double) Math.Max(1f, Math.Max(1f, element.Width / 2f)));
      RectangleF rectangleF = RectangleF.Inflate(rect, num, num);
      Color[] gradientColors = new Color[4]{ element.InnerColor, element.InnerColor2, element.InnerColor3, element.InnerColor4 };
      this.singleBorderParts[(int) element.GradientStyle](element, rectangleF);
      this.DrawBorderRectImpl(element, rectangleF, gradientColors, element.Width);
    }

    private void DrawSolidBorder(IBorderElement element, RectangleF rect)
    {
      if (element.ForeColor.A == (byte) 0)
        return;
      using (Pen pen = new Pen(element.ForeColor))
      {
        pen.Width = element.Width;
        pen.Alignment = PenAlignment.Center;
        pen.DashStyle = element.BorderDashStyle;
        this.graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
      }
    }

    private void DrawLinearBorder(IBorderElement element, RectangleF rect)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, element.ForeColor, element.ForeColor2, element.GradientAngle))
      {
        using (Pen pen = new Pen((Brush) linearGradientBrush))
        {
          pen.Width = element.Width;
          pen.Alignment = PenAlignment.Center;
          pen.DashStyle = element.BorderDashStyle;
          this.graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }

    private void DrawRadialBorder(IBorderElement element, RectangleF rect)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(new PointF[4]{ new PointF(rect.X, rect.Y), new PointF(rect.Right, rect.Y), new PointF(rect.Right, rect.Bottom), new PointF(rect.Left, rect.Bottom) }))
      {
        pathGradientBrush.CenterColor = element.ForeColor;
        pathGradientBrush.SurroundColors = new Color[4]
        {
          element.ForeColor,
          element.ForeColor2,
          element.ForeColor3,
          element.ForeColor4
        };
        pathGradientBrush.CenterPoint = new PointF(rect.Width / 2f, rect.Height / 2f);
        using (Pen pen = new Pen((Brush) pathGradientBrush))
        {
          pen.Width = element.Width;
          pen.Alignment = PenAlignment.Inset;
          pen.DashStyle = element.BorderDashStyle;
          this.graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }

    private void DrawBorderRectImpl(
      IBorderElement element,
      RectangleF rectangle,
      Color[] gradientColors,
      float width)
    {
      if (element.BoxStyle == BorderBoxStyle.FourBorders)
      {
        this.DrawFourBorders(element, rectangle);
      }
      else
      {
        if (element.GradientStyle != GradientStyles.Solid)
          return;
        using (Pen pen = new Pen(gradientColors[0]))
        {
          pen.Width = width;
          pen.Alignment = PenAlignment.Center;
          pen.DashStyle = element.BorderDashStyle;
          this.graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
      }
    }

    private void DrawFourBorders(IBorderElement borderElement, RectangleF rectangle)
    {
      float height1 = (float) Math.Ceiling((double) borderElement.TopWidth == 1.0 ? 1.0 : (double) borderElement.TopWidth / 2.0);
      float height2 = (float) Math.Ceiling((double) borderElement.BottomWidth == 1.0 ? 1.0 : (double) borderElement.BottomWidth / 2.0);
      float width1 = (float) Math.Ceiling((double) borderElement.LeftWidth == 1.0 ? 1.0 : (double) borderElement.LeftWidth / 2.0);
      float width2 = (float) Math.Ceiling((double) borderElement.RightWidth == 1.0 ? 1.0 : (double) borderElement.RightWidth / 2.0);
      float height3 = (float) Math.Floor((double) borderElement.TopWidth == 1.0 ? 1.0 : (double) borderElement.TopWidth / 2.0);
      float height4 = (float) Math.Floor((double) borderElement.BottomWidth == 1.0 ? 1.0 : (double) borderElement.BottomWidth / 2.0);
      float width3 = (float) Math.Floor((double) borderElement.LeftWidth == 1.0 ? 1.0 : (double) borderElement.LeftWidth / 2.0);
      float width4 = (float) Math.Floor((double) borderElement.RightWidth == 1.0 ? 1.0 : (double) borderElement.RightWidth / 2.0);
      RectangleF rectangleF = new RectangleF(rectangle.Left + width1, rectangle.Top + height1, rectangle.Width - (width1 + width2), rectangle.Height - (height1 + height2));
      if ((double) borderElement.TopWidth > 0.0)
      {
        using (Brush brush1 = (Brush) new SolidBrush(borderElement.TopColor))
        {
          using (Brush brush2 = (Brush) new SolidBrush(borderElement.TopShadowColor))
          {
            this.graphics.FillRectangle(brush1, new RectangleF(rectangle.Left, rectangle.Top, rectangle.Width + 1f, height1));
            this.graphics.FillRectangle(brush2, new RectangleF(rectangleF.Left, rectangleF.Top, rectangleF.Width + 1f, height3));
          }
        }
      }
      if ((double) borderElement.BottomWidth > 0.0)
      {
        using (Brush brush1 = (Brush) new SolidBrush(borderElement.BottomColor))
        {
          using (Brush brush2 = (Brush) new SolidBrush(borderElement.BottomShadowColor))
          {
            this.graphics.FillRectangle(brush1, new RectangleF(rectangle.Left, (float) ((double) rectangle.Bottom - (double) height2 + 1.0), rectangle.Width + 1f, height2));
            this.graphics.FillRectangle(brush2, new RectangleF(rectangleF.Left, (float) ((double) rectangleF.Bottom - (double) height4 + 1.0), rectangleF.Width + 1f, height4));
          }
        }
      }
      if ((double) borderElement.LeftWidth > 0.0)
      {
        using (Brush brush1 = (Brush) new SolidBrush(borderElement.LeftColor))
        {
          using (Brush brush2 = (Brush) new SolidBrush(borderElement.LeftShadowColor))
          {
            float height5 = (float) ((double) rectangle.Height - ((double) height1 + (double) height2) + 1.0);
            float y1 = rectangle.Top + height1;
            float height6 = (float) ((double) rectangleF.Height - ((double) height3 + (double) height4) + 1.0);
            float y2 = rectangleF.Top + height3;
            if ((borderElement.BorderDrawMode & BorderDrawModes.LeftOverTop) != BorderDrawModes.HorizontalOverVertical)
            {
              height5 += height1;
              y1 -= height1;
              height6 += height3;
              y2 -= height3;
            }
            if ((borderElement.BorderDrawMode & BorderDrawModes.LeftOverBottom) != BorderDrawModes.HorizontalOverVertical)
            {
              height5 += height2;
              height6 += height4;
            }
            this.graphics.FillRectangle(brush1, new RectangleF(rectangle.Left, y1, width1, height5));
            this.graphics.FillRectangle(brush2, new RectangleF(rectangleF.Left, y2, width3, height6));
          }
        }
      }
      if ((double) borderElement.RightWidth <= 0.0)
        return;
      using (Brush brush1 = (Brush) new SolidBrush(borderElement.RightColor))
      {
        using (Brush brush2 = (Brush) new SolidBrush(borderElement.RightShadowColor))
        {
          float height5 = (float) ((double) rectangle.Height - ((double) height1 + (double) height2) + 1.0);
          float y1 = rectangle.Top + height1;
          float height6 = (float) ((double) rectangleF.Height - ((double) height3 + (double) height4) + 1.0);
          float y2 = rectangleF.Top + height3;
          if ((borderElement.BorderDrawMode & BorderDrawModes.RightOverTop) != BorderDrawModes.HorizontalOverVertical)
          {
            height5 += height1;
            y1 -= height1;
            height6 += height3;
            y2 -= height3;
          }
          if ((borderElement.BorderDrawMode & BorderDrawModes.RightOverBottom) != BorderDrawModes.HorizontalOverVertical)
          {
            height5 += height2;
            height6 += height4;
          }
          this.graphics.FillRectangle(brush1, new RectangleF((float) ((double) rectangle.Right - (double) width2 + 1.0), y1, width2, height5));
          this.graphics.FillRectangle(brush2, new RectangleF((float) ((double) rectangleF.Right - (double) width4 + 1.0), y2, width4, height6));
        }
      }
    }

    protected override void FillPathCore(Path path, RadBrush brush)
    {
      GraphicsPath rawPath = path.RawPath as GraphicsPath;
      this.graphics.FillPath(brush.RawBrush as Brush, rawPath);
    }

    protected override void FillOfficeGlass(
      Path inputPath,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2,
      bool fillEllipse)
    {
      CompositingQuality compositingQuality = this.graphics.CompositingQuality;
      SmoothingMode smoothingMode = this.graphics.SmoothingMode;
      this.graphics.CompositingQuality = CompositingQuality.HighQuality;
      this.graphics.SmoothingMode = SmoothingMode.HighQuality;
      base.FillOfficeGlass(inputPath, color1, color2, color3, color4, gradientPercentage, gradientPercentage2, fillEllipse);
      this.graphics.CompositingQuality = compositingQuality;
      this.graphics.SmoothingMode = smoothingMode;
    }

    protected override void FillRoundedRectangleCore(
      RoundedRectangle roundedRectangle,
      RadBrush brush)
    {
      GraphicsPath roundedRectangle1 = roundedRectangle.RawRoundedRectangle as GraphicsPath;
      Brush rawBrush = brush.RawBrush as Brush;
      SmoothingMode smoothingMode = this.graphics.SmoothingMode;
      this.graphics.SmoothingMode = SmoothingMode.AntiAlias;
      this.graphics.FillPath(rawBrush, roundedRectangle1);
      this.graphics.SmoothingMode = smoothingMode;
    }

    protected override void DrawPathCore(Path path, RadBrush brush, float width)
    {
      using (Pen pen = new Pen(brush.RawBrush as Brush, width))
        this.graphics.DrawPath(pen, path.RawPath as GraphicsPath);
    }

    private void DrawBitmap(Image image, int x, int y, double opacity)
    {
      if (opacity == 1.0)
      {
        this.graphics.DrawImage(image, new Rectangle(x, y, image.Size.Width, image.Size.Height));
      }
      else
      {
        ColorMatrix newColorMatrix = new ColorMatrix(this.BuildTransformMatrix(5, opacity));
        using (ImageAttributes imageAttr = new ImageAttributes())
        {
          imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
          Rectangle destRect = new Rectangle(x, y, image.Size.Width, image.Size.Height);
          try
          {
            this.graphics.DrawImage(image, destRect, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, imageAttr);
          }
          catch (OutOfMemoryException ex)
          {
            this.graphics.DrawImage(image, destRect, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel);
          }
        }
      }
    }

    private void DrawBitmap(Image image, int x, int y, int width, int height, double opacity)
    {
      if (opacity == 1.0)
      {
        this.graphics.DrawImage(image, new Rectangle(x, y, width, height));
      }
      else
      {
        ColorMatrix newColorMatrix = new ColorMatrix(this.BuildTransformMatrix(5, opacity));
        using (ImageAttributes imageAttr = new ImageAttributes())
        {
          imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
          Rectangle destRect = new Rectangle(x, y, width, height);
          try
          {
            this.graphics.DrawImage(image, destRect, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel, imageAttr);
          }
          catch (OutOfMemoryException ex)
          {
            this.graphics.DrawImage(image, destRect, 0, 0, image.Size.Width, image.Size.Height, GraphicsUnit.Pixel);
          }
        }
      }
    }

    private float[][] BuildTransformMatrix(int length, double opacity)
    {
      float[][] numArray1 = new float[length][];
      for (int index = 0; index < length; ++index)
      {
        float[] numArray2 = new float[length];
        numArray2[index] = 1f;
        numArray1[index] = numArray2;
      }
      numArray1[3][3] = (float) opacity;
      return numArray1;
    }

    private Bitmap CreateBitmapMask(Color maskColor, Bitmap bitmap)
    {
      Bitmap bitmap1 = (Bitmap) bitmap.Clone();
      for (int x = 0; x < bitmap.Size.Width; ++x)
      {
        for (int y = 0; y < bitmap.Size.Height; ++y)
        {
          if (bitmap.GetPixel(x, y).A > (byte) 0)
            bitmap1.SetPixel(x, y, maskColor);
        }
      }
      return bitmap1;
    }

    public override void FillPolygon(RadBrush brush, PointF[] points)
    {
      this.graphics.FillPolygon((Brush) brush.RawBrush, points);
    }

    public override void DrawLine(RadBrush brush, float x1, float y1, float x2, float y2)
    {
      using (Pen pen = new Pen((Brush) brush.RawBrush))
        this.graphics.DrawLine(pen, x1, y1, x2, y2);
    }

    public override void FillRectangle(
      RadBrush brush,
      float x,
      float y,
      float width,
      float height)
    {
      this.graphics.FillRectangle((Brush) brush.RawBrush, x, y, width, height);
    }

    public override void DrawRectangle(RadBrush brush, float x1, float y1, float x2, float y2)
    {
      using (Pen pen = new Pen((Brush) brush.RawBrush))
        this.graphics.DrawRectangle(pen, x1, y1, x2, y2);
    }

    private delegate void DrawBorderElement(IBorderElement element, RectangleF rect);
  }
}
