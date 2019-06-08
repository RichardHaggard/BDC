// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.BorderPrimitiveImpl
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
  public class BorderPrimitiveImpl
  {
    private readonly IBorderElement borderElement;
    private readonly IPrimitiveElement primitiveElement;
    private IShapedElement shapedElement;

    public BorderPrimitiveImpl(IBorderElement borderElement, IPrimitiveElement radElement)
    {
      this.borderElement = borderElement;
      this.primitiveElement = radElement;
      this.shapedElement = borderElement as IShapedElement;
    }

    public virtual void PaintBorder(IGraphics graphics, float angle, SizeF scale)
    {
      RectangleF paintRectangle = this.primitiveElement.GetPaintRectangle(this.primitiveElement.BorderThickness, angle, scale);
      this.PaintBorder(graphics, angle, scale, paintRectangle);
    }

    private bool IsTransparent()
    {
      if (this.borderElement.BoxStyle == BorderBoxStyle.SingleBorder)
        return this.IsOuterBorderTransparent();
      if (this.borderElement.BoxStyle == BorderBoxStyle.OuterInnerBorders && this.IsOuterBorderTransparent())
        return this.IsInnerBorderTransparent();
      return false;
    }

    private bool IsOuterBorderTransparent()
    {
      return this.borderElement.ForeColor.A == (byte) 0 && (this.borderElement.GradientStyle == GradientStyles.Solid || this.borderElement.ForeColor2.A == (byte) 0 && this.borderElement.ForeColor3.A == (byte) 0 && this.borderElement.ForeColor4.A == (byte) 0);
    }

    private bool IsInnerBorderTransparent()
    {
      return this.borderElement.InnerColor.A == (byte) 0 && (this.borderElement.GradientStyle == GradientStyles.Solid || this.borderElement.InnerColor2.A == (byte) 0 && this.borderElement.InnerColor3.A == (byte) 0 && this.borderElement.InnerColor4.A == (byte) 0);
    }

    public virtual void PaintBorder(
      IGraphics graphics,
      float angle,
      SizeF scale,
      RectangleF preferedRectangle)
    {
      if (this.primitiveElement != null)
      {
        Size size = this.primitiveElement.Size;
        if (size.Width <= 0 || size.Height <= 0)
          return;
      }
      if (this.IsTransparent() || (double) this.borderElement.Width <= 0.0 && (this.borderElement.BoxStyle != BorderBoxStyle.FourBorders || (double) this.borderElement.LeftWidth <= 0.0 && (double) this.borderElement.RightWidth <= 0.0 && ((double) this.borderElement.TopWidth <= 0.0 && (double) this.borderElement.BottomWidth <= 0.0)))
        return;
      ElementShape shape = (ElementShape) null;
      if (this.primitiveElement != null)
        shape = this.primitiveElement.GetCurrentShape();
      else if (this.shapedElement != null)
        shape = this.shapedElement.GetCurrentShape();
      GraphicsPath clippingPath = (GraphicsPath) null;
      if (shape != null)
        clippingPath = shape.CreatePath(preferedRectangle);
      if (clippingPath != null)
      {
        this.PaintBorder(graphics, shape, clippingPath, preferedRectangle);
        clippingPath.Dispose();
      }
      else
      {
        Color[] gradientColors1 = new Color[4]{ this.borderElement.ForeColor, this.borderElement.ForeColor2, this.borderElement.ForeColor3, this.borderElement.ForeColor4 };
        if (this.borderElement.BoxStyle == BorderBoxStyle.OuterInnerBorders)
        {
          this.DrawRectangle(graphics, preferedRectangle, gradientColors1, this.borderElement.Width);
          float val2 = Math.Max(1f, this.borderElement.Width / 2f);
          Color[] gradientColors2 = new Color[4]{ this.borderElement.InnerColor, this.borderElement.InnerColor2, this.borderElement.InnerColor3, this.borderElement.InnerColor4 };
          float num = -(float) Math.Floor((double) Math.Max(1f, val2));
          RectangleF rectangle = RectangleF.Inflate(preferedRectangle, num, num);
          this.DrawRectangle(graphics, rectangle, gradientColors2, this.borderElement.Width);
        }
        else
        {
          Rectangle rectangle = Rectangle.Round(preferedRectangle);
          if (this.borderElement.BoxStyle == BorderBoxStyle.FourBorders && this.primitiveElement != null)
            rectangle = Rectangle.Round(this.primitiveElement.GetExactPaintingRectangle(angle, scale));
          if ((double) this.borderElement.Width == 1.0 && this.borderElement.BoxStyle == BorderBoxStyle.SingleBorder && (rectangle.Width <= 0 || rectangle.Height <= 0))
            return;
          this.DrawRectangle(graphics, (RectangleF) rectangle, gradientColors1, this.borderElement.Width);
        }
      }
    }

    public void PaintBorder(
      IGraphics graphics,
      ElementShape shape,
      GraphicsPath clippingPath,
      RectangleF preferedRectangle)
    {
      Color[] gradientColors1 = new Color[4]{ this.borderElement.ForeColor, this.borderElement.ForeColor2, this.borderElement.ForeColor3, this.borderElement.ForeColor4 };
      if (this.borderElement.BoxStyle == BorderBoxStyle.OuterInnerBorders)
      {
        float num1 = Math.Max(1f, this.borderElement.Width / 2f);
        this.DrawPath(graphics, clippingPath, preferedRectangle, gradientColors1, num1);
        float num2 = -(float) Math.Floor((double) Math.Max(1f, num1));
        RectangleF rectangleF = RectangleF.Inflate(preferedRectangle, num2, num2);
        GraphicsPath path = shape?.CreatePath(rectangleF);
        if (path == null)
          return;
        Color[] gradientColors2 = new Color[4]{ this.borderElement.InnerColor, this.borderElement.InnerColor2, this.borderElement.InnerColor3, this.borderElement.InnerColor4 };
        this.DrawPath(graphics, path, rectangleF, gradientColors2, this.borderElement.Width / 2f);
        path.Dispose();
      }
      else
      {
        if ((double) this.borderElement.Width <= 0.0)
          return;
        this.DrawPath(graphics, clippingPath, preferedRectangle, gradientColors1, this.borderElement.Width);
      }
    }

    private void DrawRectangle(
      IGraphics graphics,
      RectangleF rectangle,
      Color[] gradientColors,
      float width)
    {
      if (this.borderElement.BoxStyle == BorderBoxStyle.FourBorders)
      {
        graphics.DrawBorder(rectangle, this.borderElement);
      }
      else
      {
        graphics.ChangeSmoothingMode(this.borderElement.SmoothingMode);
        if (this.borderElement.GradientStyle == GradientStyles.Solid)
          graphics.DrawRectangle(rectangle, gradientColors[0], PenAlignment.Center, width, this.borderElement.BorderDashStyle, this.borderElement.BorderDashPattern);
        else if (this.borderElement.GradientStyle == GradientStyles.Linear)
          graphics.DrawLinearGradientRectangle(rectangle, gradientColors, PenAlignment.Center, width, this.borderElement.GradientAngle, this.borderElement.BorderDashStyle, this.borderElement.BorderDashPattern);
        else if (this.borderElement.GradientStyle == GradientStyles.Radial)
          graphics.DrawRadialGradientRectangle(rectangle, gradientColors[0], gradientColors, PenAlignment.Inset, width, this.borderElement.BorderDashStyle, this.borderElement.BorderDashPattern);
        graphics.RestoreSmoothingMode();
      }
    }

    private void DrawPath(
      IGraphics graphics,
      GraphicsPath path,
      RectangleF rectangle,
      Color[] gradientColors,
      float width)
    {
      graphics.ChangeSmoothingMode(this.borderElement.SmoothingMode);
      PenAlignment penAlignment = (double) width <= 1.0 ? PenAlignment.Inset : PenAlignment.Center;
      if (this.borderElement.GradientStyle == GradientStyles.Solid)
        graphics.DrawPath(path, gradientColors[0], penAlignment, width, this.borderElement.BorderDashStyle, this.borderElement.BorderDashPattern);
      else if (this.borderElement.GradientStyle == GradientStyles.Linear)
        graphics.DrawLinearGradientPath(path, rectangle, gradientColors, penAlignment, width, this.borderElement.GradientAngle, this.borderElement.BorderDashStyle, this.borderElement.BorderDashPattern);
      else if (this.borderElement.GradientStyle == GradientStyles.Radial)
        graphics.DrawRadialGradientPath(path, Rectangle.Round(rectangle), gradientColors[0], new Color[3]
        {
          gradientColors[1],
          gradientColors[2],
          gradientColors[3]
        }, penAlignment, width, this.borderElement.BorderDashStyle, this.borderElement.BorderDashPattern);
      graphics.RestoreSmoothingMode();
    }
  }
}
