// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GraphicsContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.Drawing
{
  public class GraphicsContext
  {
    private static readonly string D2DAssemblyPath = Application.StartupPath + "\\Telerik.WinControls.Direct2D.dll";
    private Surface surface;
    private DrawingMode drawingMode;

    public GraphicsContext(Surface deviceSurface)
    {
      if (deviceSurface == null)
        deviceSurface = this.CreateSurfaceFactory().CreateSurface() ?? new SurfaceFactory().CreateSurface();
      this.surface = deviceSurface;
    }

    public GraphicsContext()
      : this((Surface) null)
    {
    }

    private SurfaceFactory CreateSurfaceFactory()
    {
      SurfaceFactory surfaceFactory = (SurfaceFactory) null;
      Version version = Environment.OSVersion.Version;
      if (version.Major >= 6 && (version.Minor > 0 || version.Build >= 6002))
        surfaceFactory = SurfaceFactory.CreateFromFile(GraphicsContext.D2DAssemblyPath);
      if (surfaceFactory == null)
        surfaceFactory = new SurfaceFactory();
      return surfaceFactory;
    }

    public Dictionary<string, object> Resources
    {
      get
      {
        return this.surface.Resources;
      }
    }

    public DrawingMode DrawingMode
    {
      get
      {
        return this.drawingMode;
      }
    }

    public Surface Surface
    {
      get
      {
        return this.surface;
      }
    }

    public Path CreatePath()
    {
      return this.surface.CreatePath();
    }

    public RadBrush CreateSolidBrush(Color color)
    {
      return this.surface.CreateSolidBrush(color);
    }

    public RadBrush CreateLinearGradientBrush(
      RectangleF rectangle,
      GradientStop[] colorStops,
      float angle)
    {
      return this.surface.CreateLinearGradientBrush(rectangle, colorStops, angle);
    }

    public RadDisplacementMapEffect CreateDisplacementMapEffect()
    {
      return this.surface.CreateDisplacementMapEffect();
    }

    public RadGaussianBlurEffect CreateGaussianBlurEffect()
    {
      return this.surface.CreateGaussianBlurEffect();
    }

    public RadMorphologyEffect CreateMorphologyEffect()
    {
      return this.surface.CreateMorphologyEffect();
    }

    public void BeginEffects(Size viewportSize)
    {
      this.surface.BeginEffects(viewportSize);
    }

    public void EndEffects(EffectCollection effects)
    {
      this.surface.EndEffects(effects);
    }

    public void BeginDraw(params object[] handles)
    {
      this.surface.BeginDraw(handles);
    }

    public void EndDraw()
    {
      this.surface.EndDraw();
    }

    public void DrawBorder(IBorderElement element, RectangleF rect)
    {
      this.surface.DrawBorder(element, rect);
    }

    public void DrawBorder(IBorderElement element, Path path)
    {
      this.surface.DrawBorder(element, path);
    }

    public void DrawText(
      string text,
      Font font,
      RadBrush brush,
      RectangleF rect,
      TextFormat format)
    {
      this.surface.DrawText(text, font, brush, rect, format);
    }

    public void DrawText(string text, Font font, RadBrush brush, RectangleF rect)
    {
      this.surface.DrawText(text, font, brush, rect, (TextFormat) null);
    }

    public void DrawText(string text, Font font, Color color, RectangleF rect)
    {
      RadBrush solidBrush = this.surface.CreateSolidBrush(color);
      this.surface.DrawText(text, font, solidBrush, rect, (TextFormat) null);
    }

    public void DrawText(
      string text,
      Font font,
      RadBrush brush,
      float x,
      float y,
      TextFormat textFormat)
    {
      this.surface.DrawText(text, font, brush, new RectangleF(x, y, 0.0f, 0.0f), textFormat);
    }

    public void DrawText(string text, Font font, RadBrush brush, float x, float y)
    {
      this.surface.DrawText(text, font, brush, new RectangleF(x, y, 0.0f, 0.0f), (TextFormat) null);
    }

    public void DrawText(
      string text,
      Font font,
      RadBrush brush,
      PointF point,
      TextFormat textFormat)
    {
      this.surface.DrawText(text, font, brush, new RectangleF(point.X, point.Y, 0.0f, 0.0f), textFormat);
    }

    public void DrawText(string text, Font font, RadBrush brush, PointF point)
    {
      this.surface.DrawText(text, font, brush, new RectangleF(point.X, point.Y, 0.0f, 0.0f), (TextFormat) null);
    }

    public void DrawText(ITextElement element, RadBrush brush, RectangleF rect)
    {
      this.surface.DrawText(element, brush, rect);
    }

    public void DrawText(ITextElement element, Color color, RectangleF rect)
    {
      RadBrush solidBrush = this.surface.CreateSolidBrush(color);
      this.surface.DrawText(element, solidBrush, rect);
    }

    public void DrawText(ITextElement element, RadBrush brush, PointF point)
    {
      this.surface.DrawText(element, brush, new RectangleF(point, SizeF.Empty));
    }

    public void DrawText(ITextElement element, PointF point)
    {
      using (RadBrush solidBrush = this.surface.CreateSolidBrush(element.ForeColor))
        this.surface.DrawText(element, solidBrush, new RectangleF(point, SizeF.Empty));
    }

    public void DrawText(ITextElement element, RectangleF rect)
    {
      using (RadBrush solidBrush = this.surface.CreateSolidBrush(element.ForeColor))
        this.surface.DrawText(element, solidBrush, rect);
    }

    public void DrawText(ITextElement element, RadBrush brush, float x, float y)
    {
      this.surface.DrawText(element, brush, new RectangleF(new PointF(x, y), SizeF.Empty));
    }

    public SizeF MeasureText(
      string text,
      Font font,
      SizeF availableSize,
      TextFormat textFormat)
    {
      return this.surface.MeasureText(text, font, availableSize, textFormat);
    }

    public SizeF MeasureText(string text, Font font, SizeF availableSize)
    {
      return this.surface.MeasureText(text, font, availableSize, (TextFormat) null);
    }

    public SizeF MeasureText(ITextElement element, SizeF availableSize)
    {
      return this.surface.MeasureText(element, availableSize);
    }

    public void FillRectangle(IFillElement element, RectangleF rect)
    {
      this.surface.FillRectangle(element, rect);
    }

    public void FillPath(IFillElement element, Path path)
    {
      this.surface.FillPath(element, path);
    }

    public void DrawImage(IImageElement element, RectangleF rect)
    {
      this.surface.DrawImage(element, rect);
    }

    public void DrawImage(Image image, RectangleF rect)
    {
      this.surface.DrawImage(image, rect);
    }

    public void FillPolygon(RadBrush brush, PointF[] points)
    {
      this.surface.FillPolygon(brush, points);
    }

    public void FillPolygon(Color color, PointF[] points)
    {
      this.surface.FillPolygon(this.surface.CreateSolidBrush(color), points);
    }

    public void DrawLine(RadBrush brush, float x1, float y1, float x2, float y2)
    {
      this.surface.DrawLine(brush, x1, y1, x2, y2);
    }

    public void DrawLine(RadBrush brush, PointF pt1, PointF pt2)
    {
      this.surface.DrawLine(brush, pt1.X, pt1.Y, pt2.X, pt2.Y);
    }

    public void DrawLine(Color color, float x1, float y1, float x2, float y2)
    {
      this.surface.DrawLine(this.surface.CreateSolidBrush(color), x1, y1, x2, y2);
    }

    public void DrawLine(Color color, PointF pt1, PointF pt2)
    {
      this.surface.DrawLine(this.surface.CreateSolidBrush(color), pt1.X, pt1.Y, pt2.X, pt2.Y);
    }

    public void FillRectangle(RadBrush brush, float x, float y, float width, float height)
    {
      this.surface.FillRectangle(brush, x, y, width, height);
    }

    public void FillRectangle(RadBrush brush, RectangleF rect)
    {
      this.surface.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
    }

    public void FillRectangle(Color color, float x, float y, float width, float height)
    {
      this.surface.FillRectangle(this.surface.CreateSolidBrush(color), x, y, width, height);
    }

    public void FillRectangle(Color color, RectangleF rect)
    {
      this.surface.FillRectangle(this.surface.CreateSolidBrush(color), rect.X, rect.Y, rect.Width, rect.Height);
    }

    public void DrawRectangle(RadBrush brush, float x1, float y1, float x2, float y2)
    {
      this.surface.DrawRectangle(brush, x1, y1, x2, y2);
    }

    public void DrawRectangle(RadBrush brush, RectangleF rect)
    {
      this.surface.DrawRectangle(brush, rect.Left, rect.Top, rect.Right, rect.Bottom);
    }

    public void DrawRectangle(Color color, float x1, float y1, float x2, float y2)
    {
      this.DrawRectangle(this.surface.CreateSolidBrush(color), x1, y1, x2, y2);
    }

    public void DrawRectangle(Color color, RectangleF rect)
    {
      this.DrawRectangle(this.surface.CreateSolidBrush(color), rect);
    }
  }
}
