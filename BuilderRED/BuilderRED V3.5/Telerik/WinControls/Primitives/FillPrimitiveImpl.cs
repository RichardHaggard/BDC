// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.FillPrimitiveImpl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class FillPrimitiveImpl
  {
    private readonly IFillElement fillElement;
    private readonly IPrimitiveElement primitiveElement;
    private IShapedElement shapedElement;
    private SizeF lastScale;
    private FillElementPaintBuffer fillElementPaintBuffer;

    public FillPrimitiveImpl(IFillElement fillElement, IPrimitiveElement radElement)
    {
      this.fillElement = fillElement;
      this.primitiveElement = radElement;
      this.shapedElement = fillElement as IShapedElement;
    }

    private FillElementPaintBuffer FillElementPaintBuffer
    {
      get
      {
        if (this.fillElementPaintBuffer == null && this.primitiveElement != null && this.primitiveElement.ElementTree != null)
          this.fillElementPaintBuffer = new FillElementPaintBuffer(this.fillElement, this.primitiveElement.ElementTree.ComponentTreeHandler.Behavior.BitmapRepository);
        return this.fillElementPaintBuffer;
      }
    }

    private bool IsTransparent()
    {
      if (this.fillElement.BackColor.A == (byte) 0)
      {
        int numberOfColors = this.fillElement.NumberOfColors;
        if (numberOfColors <= 1 || this.fillElement.BackColor2.A == (byte) 0 && (numberOfColors <= 2 || this.fillElement.BackColor3.A == (byte) 0 && (numberOfColors <= 3 || this.fillElement.BackColor4.A == (byte) 0)))
          return true;
      }
      return false;
    }

    public void PaintFill(IGraphics graphics, float angle, SizeF scale)
    {
      RectangleF paintRectangle = this.primitiveElement.GetPaintRectangle(this.primitiveElement.BorderThickness, angle, scale);
      this.PaintFill(graphics, angle, scale, paintRectangle);
    }

    public void PaintFill(IGraphics graphics, float angle, SizeF scale, RectangleF paintRect)
    {
      if (this.IsTransparent())
        return;
      Size desired = Size.Round(paintRect.Size);
      if (desired.Width <= 0 || desired.Height <= 0)
        return;
      graphics.ChangeSmoothingMode(this.fillElement.SmoothingMode);
      this.lastScale = scale;
      ElementShape elementShape = (ElementShape) null;
      if (this.primitiveElement != null)
        elementShape = this.primitiveElement.GetCurrentShape();
      else if (this.shapedElement != null)
        elementShape = this.shapedElement.GetCurrentShape();
      FillElementPaintBuffer elementPaintBuffer = this.FillElementPaintBuffer;
      if (elementShape != null && elementPaintBuffer != null)
      {
        string str = elementShape.SerializeProperties();
        int shapeHash = string.IsNullOrEmpty(str) ? elementShape.GetHashCode() : str.GetHashCode();
        elementPaintBuffer.SetShapeHash(shapeHash);
      }
      bool flag = elementPaintBuffer != null && !elementPaintBuffer.IsDisabled && elementPaintBuffer.ShouldUsePaintBuffer() && this.primitiveElement.ShouldUsePaintBuffer();
      try
      {
        if (flag)
        {
          if (!this.primitiveElement.IsDesignMode && elementPaintBuffer.PaintFromBuffer(graphics, scale, desired))
          {
            graphics.RestoreSmoothingMode();
            return;
          }
          graphics.ChangeOpacity(1.0);
          if (!this.primitiveElement.IsDesignMode)
            elementPaintBuffer.SetGraphics(graphics, scale);
        }
      }
      catch
      {
        flag = false;
      }
      GraphicsPath path = (GraphicsPath) null;
      if (elementShape != null)
      {
        path = elementShape.CreatePath(paintRect);
        graphics.PushCurrentClippingPath(path);
      }
      this.FillRectangle(graphics, paintRect);
      if (path != null)
      {
        graphics.PopCurrentClippingPath();
        path.Dispose();
      }
      if (flag)
      {
        graphics.RestoreOpacity();
        if (!this.primitiveElement.IsDesignMode)
          elementPaintBuffer.ResetGraphics(graphics, scale);
      }
      graphics.RestoreSmoothingMode();
    }

    public void PaintFill(IGraphics graphics, GraphicsPath clippingPath, RectangleF paintRect)
    {
      graphics.ChangeSmoothingMode(this.fillElement.SmoothingMode);
      if (clippingPath != null)
        graphics.PushCurrentClippingPath(clippingPath);
      if (this.fillElement.GradientStyle == GradientStyles.Radial)
      {
        RadGdiGraphics radGdiGraphics = graphics as RadGdiGraphics;
        if (radGdiGraphics != null)
        {
          int val2 = 4;
          int numberOfColors = this.fillElement.NumberOfColors;
          Color[] colorStops = new Color[Math.Min(Math.Max(numberOfColors, 1), val2)];
          float[] colorOffsets = new float[Math.Min(Math.Max(numberOfColors, 1), val2)];
          FillPrimitiveImpl.FillColorStopsAndOffsets(colorStops, colorOffsets, numberOfColors, this.fillElement);
          radGdiGraphics.FillGradientPath(clippingPath, paintRect, colorStops, colorOffsets, this.fillElement.GradientStyle, this.fillElement.GradientAngle, this.fillElement.GradientPercentage, this.fillElement.GradientPercentage2);
        }
      }
      else if (this.fillElement.GradientStyle == GradientStyles.Linear)
      {
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(clippingPath))
        {
          pathGradientBrush.CenterPoint = new PointF(paintRect.X + paintRect.Width / 2f, paintRect.Y + paintRect.Height / 2f);
          pathGradientBrush.CenterColor = this.fillElement.BackColor2;
          pathGradientBrush.SurroundColors = new Color[1]
          {
            this.fillElement.BackColor
          };
          pathGradientBrush.SetBlendTriangularShape(this.fillElement.GradientPercentage, this.fillElement.GradientPercentage2);
          ((Graphics) graphics.UnderlayGraphics).FillPath((Brush) pathGradientBrush, clippingPath);
        }
      }
      else
        this.FillRectangle(graphics, paintRect);
      if (clippingPath != null)
        graphics.PopCurrentClippingPath();
      graphics.RestoreSmoothingMode();
    }

    private void FillRectangle(IGraphics g, RectangleF rect)
    {
      FillPrimitiveImpl.FillRectangle(g, rect, this.fillElement);
    }

    public static void FillRectangle(IGraphics g, RectangleF rect, IFillElement fillElement)
    {
      int val2 = 4;
      int numberOfColors = fillElement.NumberOfColors;
      Color[] colorStops = new Color[Math.Min(Math.Max(numberOfColors, 1), val2)];
      float[] colorOffsets = new float[Math.Min(Math.Max(numberOfColors, 1), val2)];
      FillPrimitiveImpl.FillColorStopsAndOffsets(colorStops, colorOffsets, numberOfColors, fillElement);
      switch (fillElement.GradientStyle)
      {
        case GradientStyles.Solid:
          g.FillRectangle(rect, fillElement.BackColor);
          break;
        case GradientStyles.Linear:
        case GradientStyles.Radial:
          if (numberOfColors < 2 || numberOfColors == 2 && fillElement.BackColor == fillElement.BackColor2)
          {
            g.FillRectangle(rect, fillElement.BackColor);
            break;
          }
          g.FillGradientRectangle(rect, colorStops, colorOffsets, fillElement.GradientStyle, fillElement.GradientAngle, fillElement.GradientPercentage, fillElement.GradientPercentage2);
          break;
        case GradientStyles.Glass:
          g.FillGlassRectangle(Rectangle.Round(rect), fillElement.BackColor, fillElement.BackColor2, fillElement.BackColor3, fillElement.BackColor4, fillElement.GradientPercentage, fillElement.GradientPercentage2);
          break;
        case GradientStyles.OfficeGlass:
          g.FillOfficeGlassRectangle(Rectangle.Round(rect), fillElement.BackColor, fillElement.BackColor2, fillElement.BackColor3, fillElement.BackColor4, fillElement.GradientPercentage, fillElement.GradientPercentage2, true);
          break;
        case GradientStyles.OfficeGlassRect:
          g.FillOfficeGlassRectangle(Rectangle.Round(rect), fillElement.BackColor, fillElement.BackColor2, fillElement.BackColor3, fillElement.BackColor4, fillElement.GradientPercentage, fillElement.GradientPercentage2, false);
          break;
        case GradientStyles.Gel:
          g.FillGellRectangle(Rectangle.Round(rect), colorStops, fillElement.GradientPercentage, fillElement.GradientPercentage2);
          break;
        case GradientStyles.Vista:
          g.FillVistaRectangle(Rectangle.Round(rect), fillElement.BackColor, fillElement.BackColor2, fillElement.BackColor3, fillElement.BackColor4, fillElement.GradientPercentage, fillElement.GradientPercentage2);
          break;
      }
    }

    private static void FillColorStopsAndOffsets(
      Color[] colorStops,
      float[] colorOffsets,
      int numberOfColors,
      IFillElement fillElement)
    {
      if (numberOfColors > 0)
      {
        colorStops[0] = fillElement.BackColor;
        colorOffsets[0] = 0.0f;
      }
      if (numberOfColors <= 1)
        return;
      colorStops[1] = fillElement.BackColor2;
      colorOffsets[colorStops.Length - 1] = 1f;
      if (numberOfColors <= 2)
        return;
      colorStops[2] = fillElement.BackColor3;
      colorOffsets[1] = fillElement.GradientPercentage;
      if (numberOfColors <= 3)
        return;
      colorStops[3] = fillElement.BackColor4;
      colorOffsets[2] = fillElement.GradientPercentage2;
    }

    public void OnBoundsChanged(Rectangle oldBounds)
    {
      this.FillElementPaintBuffer?.RemoveBitmapsBySize(oldBounds.Size, this.lastScale);
    }

    public void InvalidateFillCache(RadProperty property)
    {
      this.FillElementPaintBuffer?.InvalidateCachedPrimitiveHash(property);
    }
  }
}
