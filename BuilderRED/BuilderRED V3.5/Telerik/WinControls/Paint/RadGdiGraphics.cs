// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Paint.RadGdiGraphics
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.Paint
{
  public class RadGdiGraphics : IGraphics, IDisposable
  {
    private SmoothingMode oldSmoothingMode = SmoothingMode.None;
    private double opacity = 1.0;
    private double oldOpacity = 1.0;
    private const int blurAmount = 6;
    public static readonly int GdiStringLengthLimit;
    [ThreadStatic]
    private static Graphics measurementGraphics;
    private Graphics graphics;
    private Rectangle clipRectangle;
    private Stack<GraphicsPath> clippingPathStack;
    private Region oldClip;
    private Region graphicsClip;

    public static Graphics MeasurementGraphics
    {
      get
      {
        if (RadGdiGraphics.measurementGraphics == null)
          RadGdiGraphics.measurementGraphics = Telerik.WinControls.MeasurementGraphics.CreateMeasurementGraphics().Graphics;
        return RadGdiGraphics.measurementGraphics;
      }
    }

    public static FontTextMetrics GetTextMetric(Font font)
    {
      Telerik.WinControls.NativeMethods.TEXTMETRIC tm = new Telerik.WinControls.NativeMethods.TEXTMETRIC();
      lock (Telerik.WinControls.MeasurementGraphics.SyncObject)
      {
        Graphics measurementGraphics = RadGdiGraphics.MeasurementGraphics;
        IntPtr hdc = measurementGraphics.GetHdc();
        IntPtr hfont = font.ToHfont();
        IntPtr handle = Telerik.WinControls.NativeMethods.SelectObject(new HandleRef((object) null, hdc), new HandleRef((object) null, hfont));
        Telerik.WinControls.NativeMethods.GetTextMetrics(hdc, ref tm);
        Telerik.WinControls.NativeMethods.SelectObject(new HandleRef((object) null, hdc), new HandleRef((object) null, handle));
        Telerik.WinControls.NativeMethods.DeleteObject(new HandleRef((object) null, hfont));
        measurementGraphics.ReleaseHdc(hdc);
      }
      return new FontTextMetrics() { height = tm.tmHeight, ascent = tm.tmAscent, descent = tm.tmDescent, internalLeading = tm.tmInternalLeading, externalLeading = tm.tmExternalLeading, aveCharWidth = tm.tmAveCharWidth, maxCharWidth = tm.tmMaxCharWidth, weight = tm.tmWeight, overhang = tm.tmOverhang, digitizedAspectX = tm.tmDigitizedAspectX, digitizedAspectY = tm.tmDigitizedAspectY };
    }

    public static ImageAttributes GetOpacityAttributes(float opacity)
    {
      ImageAttributes imageAttributes = new ImageAttributes();
      imageAttributes.SetColorMatrix(new ColorMatrix()
      {
        Matrix00 = 1f,
        Matrix11 = 1f,
        Matrix22 = 1f,
        Matrix33 = opacity,
        Matrix44 = 1f
      });
      return imageAttributes;
    }

    static RadGdiGraphics()
    {
      if (Environment.OSVersion.Version.Major > 6 || Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2)
        RadGdiGraphics.GdiStringLengthLimit = 32000;
      else
        RadGdiGraphics.GdiStringLengthLimit = (int) ushort.MaxValue;
    }

    public RadGdiGraphics(Graphics graphics)
    {
      this.graphics = graphics;
    }

    private Stack<GraphicsPath> ClippingPathStack
    {
      get
      {
        if (this.clippingPathStack == null)
          this.clippingPathStack = new Stack<GraphicsPath>();
        return this.clippingPathStack;
      }
    }

    public Rectangle ClipRectangle
    {
      get
      {
        return this.clipRectangle;
      }
      set
      {
        this.clipRectangle = value;
      }
    }

    public object UnderlayGraphics
    {
      get
      {
        return (object) this.graphics;
      }
    }

    public double Opacity
    {
      get
      {
        return this.opacity;
      }
    }

    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
      set
      {
        this.graphics = value;
      }
    }

    public void ChangeOpacity(double opacity)
    {
      this.oldOpacity = this.opacity;
      this.opacity = opacity;
    }

    public void RestoreOpacity()
    {
      this.opacity = this.oldOpacity;
    }

    private Color GetColor(Color original)
    {
      if (this.opacity == 1.0)
        return original;
      return Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) original.A * this.opacity))), original);
    }

    private Color[] GetColors(Color[] original)
    {
      Color[] colorArray = new Color[original.Length];
      for (int index = 0; index < original.Length; ++index)
        colorArray[index] = this.GetColor(original[index]);
      return colorArray;
    }

    public virtual void ChangeSmoothingMode(SmoothingMode smoothingMode)
    {
      this.oldSmoothingMode = this.graphics.SmoothingMode;
      this.graphics.SmoothingMode = smoothingMode;
    }

    public virtual void RestoreSmoothingMode()
    {
      this.graphics.SmoothingMode = this.oldSmoothingMode;
    }

    public virtual void ExcludeClip(Rectangle rectangle)
    {
      this.graphics.ExcludeClip(rectangle);
    }

    public void RotateTransform(float angleInDegrees)
    {
      this.graphics.RotateTransform(angleInDegrees);
    }

    public void TranslateTransform(float offsetX, float offsetY)
    {
      this.graphics.TranslateTransform(offsetX, offsetY);
    }

    public void TranslateTransform(int offsetX, int offsetY)
    {
      this.graphics.TranslateTransform((float) offsetX, (float) offsetY);
    }

    public void ScaleTransform(SizeF scale)
    {
      this.graphics.ScaleTransform(scale.Width, scale.Height);
    }

    public void ResetTransform()
    {
      this.graphics.ResetTransform();
    }

    public object SaveState()
    {
      return (object) this.graphics.Save();
    }

    public void RestoreState(object state)
    {
      GraphicsState gstate = state as GraphicsState;
      if (gstate == null)
        return;
      this.graphics.Restore(gstate);
    }

    public void PushCurrentClippingPath(GraphicsPath path)
    {
      this.ClippingPathStack.Push(path);
    }

    public GraphicsPath PopCurrentClippingPath()
    {
      return this.ClippingPathStack.Pop();
    }

    public SizeF MeasureString(string text, Font font, StringFormat stringFormat)
    {
      return this.MeasureString(text, font, 0, stringFormat);
    }

    public SizeF MeasureString(
      string text,
      Font font,
      int availableWidth,
      StringFormat stringFormat)
    {
      return this.graphics.MeasureString(text.Length <= RadGdiGraphics.GdiStringLengthLimit ? text : text.Substring(0, RadGdiGraphics.GdiStringLengthLimit), font, availableWidth, stringFormat);
    }

    public GraphicsPath GetRoundedRect(RectangleF baseRect, float radius)
    {
      if ((double) radius <= 0.0)
      {
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddRectangle(baseRect);
        graphicsPath.CloseFigure();
        return graphicsPath;
      }
      if ((double) radius >= (double) Math.Min(baseRect.Width, baseRect.Height) / 2.0)
        return this.GetCapsule(baseRect);
      float num = radius * 2f;
      SizeF size = new SizeF(num, num);
      RectangleF rect = new RectangleF(baseRect.Location, size);
      GraphicsPath graphicsPath1 = new GraphicsPath();
      graphicsPath1.AddArc(rect, 180f, 90f);
      rect.X = baseRect.Right - num;
      graphicsPath1.AddArc(rect, 270f, 90f);
      rect.Y = baseRect.Bottom - num;
      graphicsPath1.AddArc(rect, 0.0f, 90f);
      rect.X = baseRect.Left;
      graphicsPath1.AddArc(rect, 90f, 90f);
      graphicsPath1.CloseFigure();
      return graphicsPath1;
    }

    protected bool CheckValidRectangle(RectangleF rectangle)
    {
      return !rectangle.IsEmpty && (double) rectangle.Width > 0.0 && (double) rectangle.Height > 0.0;
    }

    protected bool CheckValidRectangle(Rectangle rectangle)
    {
      return !rectangle.IsEmpty && rectangle.Width > 0 && rectangle.Height > 0;
    }

    public virtual void DrawEllipse(Rectangle rectangle, Color color)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (Pen pen = new Pen(color))
        this.graphics.DrawEllipse(pen, rectangle);
    }

    public virtual void DrawLine(Color color, int x1, int y1, int x2, int y2)
    {
      using (Pen pen = new Pen(color))
        this.graphics.DrawLine(pen, x1, y1, x2, y2);
    }

    public virtual void DrawLine(Color color, float x1, float y1, float x2, float y2)
    {
      using (Pen pen = new Pen(color))
        this.graphics.DrawLine(pen, x1, y1, x2, y2);
    }

    public virtual void DrawLine(
      Color color,
      float x1,
      float y1,
      float x2,
      float y2,
      float width)
    {
      using (Pen pen = new Pen(color, width))
        this.graphics.DrawLine(pen, x1, y1, x2, y2);
    }

    public virtual void DrawLine(
      Color color,
      DashStyle dashStyle,
      int x1,
      int y1,
      int x2,
      int y2)
    {
      using (Pen pen = new Pen(color))
      {
        pen.DashStyle = dashStyle;
        this.graphics.DrawLine(pen, x1, y1, x2, y2);
      }
    }

    public virtual void DrawLine(
      Color color,
      DashStyle dashStyle,
      float[] dashPattern,
      int x1,
      int y1,
      int x2,
      int y2)
    {
      using (Pen pen = new Pen(color))
      {
        pen.DashStyle = dashStyle;
        if (dashStyle == DashStyle.Custom && dashPattern != null)
          pen.DashPattern = dashPattern;
        this.graphics.DrawLine(pen, x1, y1, x2, y2);
      }
    }

    public virtual void DrawRectangle(Rectangle rectangle, Color color)
    {
      this.DrawRectangle((RectangleF) rectangle, color, PenAlignment.Center, 1f, (Brush) null);
    }

    public virtual void DrawRectangle(
      Rectangle rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawRectangle((RectangleF) rectangle, color, penAlignment, penWidth);
    }

    public virtual void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawRectangle(rectangle, color, penAlignment, penWidth, (Brush) null);
    }

    public virtual void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle)
    {
      this.DrawRectangle(rectangle, color, penAlignment, penWidth, (Brush) null, dashStyle);
    }

    public virtual void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      this.DrawRectangle(rectangle, color, penAlignment, penWidth, (Brush) null, dashStyle, dashPattern);
    }

    public virtual void DrawLinearGradientRectangle(
      RectangleF rectangle,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle)
    {
      this.DrawLinearGradientRectangle(rectangle, gradientColors, penAlignment, penWidth, angle, DashStyle.Solid);
    }

    public virtual void DrawLinearGradientRectangle(
      RectangleF rectangle,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle)
    {
      if ((double) rectangle.Width == 0.0 || (double) rectangle.Height == 0.0)
        return;
      using (LinearGradientBrush brush = new LinearGradientBrush(rectangle, gradientColors[0], gradientColors[1], angle))
      {
        this.SetGradientBrush(gradientColors, brush);
        this.DrawRectangle(rectangle, gradientColors[0], penAlignment, penWidth, (Brush) brush, dashStyle);
      }
    }

    public virtual void DrawLinearGradientRectangle(
      RectangleF rectangle,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      if ((double) rectangle.Width == 0.0 || (double) rectangle.Height == 0.0)
        return;
      using (LinearGradientBrush brush = new LinearGradientBrush(rectangle, gradientColors[0], gradientColors[1], angle))
      {
        this.SetGradientBrush(gradientColors, brush);
        this.DrawRectangle(rectangle, gradientColors[0], penAlignment, penWidth, (Brush) brush, dashStyle, dashPattern);
      }
    }

    private void SetGradientBrush(Color[] gradientColors, LinearGradientBrush brush)
    {
      if (gradientColors.Length <= 2)
        return;
      ColorBlend colorBlend = new ColorBlend();
      brush.WrapMode = WrapMode.TileFlipXY;
      colorBlend.Colors = this.GetColors(gradientColors);
      colorBlend.Positions = new float[4]
      {
        0.0f,
        0.333f,
        0.666f,
        1f
      };
      brush.InterpolationColors = colorBlend;
    }

    public virtual void DrawRadialGradientRectangle(
      RectangleF rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawRadialGradientRectangle(rectangle, color, gradientColors, penAlignment, penWidth, DashStyle.Solid);
    }

    public virtual void DrawRadialGradientRectangle(
      Rectangle rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawRadialGradientRectangle((RectangleF) rectangle, color, gradientColors, penAlignment, penWidth);
    }

    public virtual void DrawRadialGradientRectangle(
      RectangleF rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle)
    {
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(rectangle);
        this.DrawCustomGradientRectangle(rectangle, path, color, gradientColors, penAlignment, penWidth, dashStyle);
      }
    }

    public virtual void DrawRadialGradientRectangle(
      RectangleF rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(rectangle);
        this.DrawCustomGradientRectangle(rectangle, path, color, gradientColors, penAlignment, penWidth, dashStyle, dashPattern);
      }
    }

    public virtual void DrawCustomGradientRectangle(
      RectangleF rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawCustomGradientRectangle(rectangle, path, color, gradientColors, penAlignment, penWidth, DashStyle.Solid);
    }

    public virtual void DrawCustomGradientRectangle(
      Rectangle rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawCustomGradientRectangle((RectangleF) rectangle, path, color, gradientColors, penAlignment, penWidth);
    }

    public virtual void DrawCustomGradientRectangle(
      RectangleF rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
      {
        pathGradientBrush.CenterColor = this.GetColor(color);
        pathGradientBrush.SurroundColors = this.GetColors(gradientColors);
        this.DrawRectangle(rectangle, color, penAlignment, penWidth, (Brush) pathGradientBrush, dashStyle);
      }
    }

    public virtual void DrawCustomGradientRectangle(
      RectangleF rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
      {
        pathGradientBrush.CenterColor = this.GetColor(color);
        pathGradientBrush.SurroundColors = this.GetColors(gradientColors);
        this.DrawRectangle(rectangle, color, penAlignment, penWidth, (Brush) pathGradientBrush, dashStyle, dashPattern);
      }
    }

    private void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (Pen pen = new Pen(this.GetColor(color)))
      {
        pen.Width = penWidth;
        pen.Alignment = penAlignment;
        if (brush != null)
          pen.Brush = brush;
        this.graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
      }
    }

    private void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (Pen pen = new Pen(this.GetColor(color)))
      {
        pen.Width = penWidth;
        pen.Alignment = penAlignment;
        if (brush != null)
          pen.Brush = brush;
        pen.DashStyle = dashStyle;
        this.graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
      }
    }

    private void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (Pen pen = new Pen(this.GetColor(color)))
      {
        pen.Width = penWidth;
        pen.Alignment = penAlignment;
        if (brush != null)
          pen.Brush = brush;
        pen.DashStyle = dashStyle;
        if (dashStyle == DashStyle.Custom && dashPattern != null)
          pen.DashPattern = dashPattern;
        this.graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
      }
    }

    public virtual void DrawBorder(RectangleF rectangle, IBorderElement borderElement)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      this.ChangeSmoothingMode(borderElement.SmoothingMode);
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
        using (Brush brush1 = (Brush) new SolidBrush(this.GetColor(borderElement.TopColor)))
        {
          using (Brush brush2 = (Brush) new SolidBrush(this.GetColor(borderElement.TopShadowColor)))
          {
            this.graphics.FillRectangle(brush1, new RectangleF(rectangle.Left, rectangle.Top, rectangle.Width + 1f, height1));
            this.graphics.FillRectangle(brush2, new RectangleF(rectangleF.Left, rectangleF.Top, rectangleF.Width + 1f, height3));
          }
        }
      }
      if ((double) borderElement.BottomWidth > 0.0)
      {
        using (Brush brush1 = (Brush) new SolidBrush(this.GetColor(borderElement.BottomColor)))
        {
          using (Brush brush2 = (Brush) new SolidBrush(this.GetColor(borderElement.BottomShadowColor)))
          {
            this.graphics.FillRectangle(brush1, new RectangleF(rectangle.Left, (float) ((double) rectangle.Bottom - (double) height2 + 1.0), rectangle.Width + 1f, height2));
            this.graphics.FillRectangle(brush2, new RectangleF(rectangleF.Left, (float) ((double) rectangleF.Bottom - (double) height4 + 1.0), rectangleF.Width + 1f, height4));
          }
        }
      }
      if ((double) borderElement.LeftWidth > 0.0)
      {
        using (Brush brush1 = (Brush) new SolidBrush(this.GetColor(borderElement.LeftColor)))
        {
          using (Brush brush2 = (Brush) new SolidBrush(this.GetColor(borderElement.LeftShadowColor)))
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
      if ((double) borderElement.RightWidth > 0.0)
      {
        using (Brush brush1 = (Brush) new SolidBrush(this.GetColor(borderElement.RightColor)))
        {
          using (Brush brush2 = (Brush) new SolidBrush(this.GetColor(borderElement.RightShadowColor)))
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
      this.RestoreSmoothingMode();
    }

    public void FillTextureRectangle(Rectangle rectangle, Image texture)
    {
      this.FillTextureRectangle(rectangle, texture, WrapMode.Tile);
    }

    public void FillTextureRectangle(Rectangle rectangle, Image texture, WrapMode wrapMode)
    {
      this.FillTextureRectangle((RectangleF) rectangle, texture, wrapMode);
    }

    public void FillTextureRectangle(RectangleF rectangle, Image texture)
    {
      this.FillTextureRectangle(rectangle, texture, WrapMode.Tile);
    }

    public void FillTextureRectangle(RectangleF rectangle, Image texture, WrapMode wrapMode)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (TextureBrush textureBrush = new TextureBrush(texture, wrapMode))
      {
        Matrix matrix = new Matrix();
        matrix.Translate(rectangle.X, rectangle.Y);
        textureBrush.Transform = matrix;
        this.graphics.FillRectangle((Brush) textureBrush, rectangle);
      }
    }

    public virtual void FillRectangle(Rectangle rectangle, Color color)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (Brush brush = (Brush) new SolidBrush(this.GetColor(color)))
        this.graphics.FillRectangle(brush, rectangle);
    }

    public virtual void FillRectangle(RectangleF rectangle, Color color)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (Brush brush = (Brush) new SolidBrush(this.GetColor(color)))
      {
        GraphicsPath path = this.UseFillPath(rectangle);
        if (path != null)
          this.graphics.FillPath(brush, path);
        else
          this.graphics.FillRectangle(brush, rectangle);
      }
    }

    public virtual void FillGradientRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      GradientStyles style,
      float angle)
    {
      Color[] colorStops = new Color[4]{ this.GetColor(color1), this.GetColor(color2), this.GetColor(color3), this.GetColor(color4) };
      float[] colorOffsets = new float[2]{ 0.0f, 1f };
      this.FillGradientRectangle(rectangle, colorStops, colorOffsets, style, angle, 0.0f, 0.0f);
    }

    public virtual void FillGradientRectangle(
      Rectangle rectangle,
      Color[] colorStops,
      float[] colorOffsets,
      GradientStyles style,
      float angle,
      float gradientPercentage,
      float gradientPercentage2)
    {
      this.FillGradientRectangle((RectangleF) rectangle, colorStops, colorOffsets, style, angle, gradientPercentage, gradientPercentage2);
    }

    public virtual void FillGradientPath(
      GraphicsPath path,
      RectangleF rectangle,
      Color[] colorStops,
      float[] colorOffsets,
      GradientStyles style,
      float angle,
      float gradientPercentage,
      float gradientPercentage2)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
      {
        pathGradientBrush.CenterPoint = new PointF(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f);
        ColorBlend colorBlend = new ColorBlend();
        pathGradientBrush.CenterColor = this.GetColor(colorStops[0]);
        colorBlend.Colors = this.GetColors(colorStops);
        if (colorStops.Length == 4)
          colorBlend.Positions = new float[4]
          {
            0.0f,
            gradientPercentage,
            gradientPercentage2,
            1f
          };
        else if (colorStops.Length == 3)
          colorBlend.Positions = new float[3]
          {
            0.0f,
            gradientPercentage,
            1f
          };
        else
          colorBlend.Positions = colorOffsets;
        pathGradientBrush.InterpolationColors = colorBlend;
        this.graphics.FillPath((Brush) pathGradientBrush, path);
      }
    }

    public virtual void FillGradientRectangle(
      RectangleF rectangle,
      Color[] colorStops,
      float[] colorOffsets,
      GradientStyles style,
      float angle,
      float gradientPercentage,
      float gradientPercentage2)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      if (style == GradientStyles.Radial)
      {
        using (GraphicsPath path = new GraphicsPath())
        {
          path.AddEllipse(rectangle);
          using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
          {
            pathGradientBrush.CenterPoint = new PointF(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f);
            ColorBlend colorBlend = new ColorBlend();
            pathGradientBrush.CenterColor = this.GetColor(colorStops[0]);
            colorBlend.Colors = this.GetColors(colorStops);
            if (colorStops.Length == 4)
              colorBlend.Positions = new float[4]
              {
                0.0f,
                gradientPercentage,
                gradientPercentage2,
                1f
              };
            else if (colorStops.Length == 3)
              colorBlend.Positions = new float[3]
              {
                0.0f,
                gradientPercentage,
                1f
              };
            else
              colorBlend.Positions = colorOffsets;
            pathGradientBrush.InterpolationColors = colorBlend;
            this.graphics.FillPath((Brush) pathGradientBrush, path);
          }
        }
      }
      else
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, Color.Black, Color.Black, angle))
        {
          linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
          linearGradientBrush.InterpolationColors = new ColorBlend()
          {
            Colors = this.GetColors(colorStops),
            Positions = colorOffsets
          };
          GraphicsPath path = this.UseFillPath(rectangle);
          if (path != null)
            this.graphics.FillPath((Brush) linearGradientBrush, path);
          else
            this.graphics.FillRectangle((Brush) linearGradientBrush, rectangle);
        }
      }
    }

    private GraphicsPath UseFillPath(RectangleF rectangle)
    {
      if (this.ClippingPathStack.Count == 0)
        return (GraphicsPath) null;
      return this.ClippingPathStack.Peek();
    }

    public virtual void FillGradientRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      float angle)
    {
      Color[] colorStops = new Color[2]{ color1, color2 };
      float[] colorOffsets = new float[2]{ 0.0f, 1f };
      this.FillGradientRectangle(rectangle, colorStops, colorOffsets, GradientStyles.Linear, angle, 0.0f, 0.0f);
    }

    public virtual void FillGlassRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      (RectangleF) rectangle.Inflate(-0.5f, -0.5f);
      this.SetClippingByPath(rectangle);
      this.ChangeSmoothingMode(SmoothingMode.None);
      Rectangle rect1 = rectangle;
      int num = (int) Math.Round((double) rectangle.Height * (1.0 - (double) gradientPercentage));
      rect1.Height = num + 1;
      if (color1.A > (byte) 3)
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this.GetColor(color1), this.GetColor(color2), LinearGradientMode.Vertical))
          this.graphics.FillRectangle((Brush) linearGradientBrush, rect1);
      }
      Rectangle rect2 = rectangle;
      rect2.Y = rectangle.Top + num;
      rect2.Height = rectangle.Height - num;
      using (SolidBrush solidBrush = new SolidBrush(this.GetColor(color3)))
        this.graphics.FillRectangle((Brush) solidBrush, rect2);
      Rectangle rectangle1 = rect2;
      rectangle1.Y = rectangle.Top + (int) ((double) num * (double) gradientPercentage2);
      rectangle1.Height = (int) ((double) (rectangle.Height - (rectangle1.Y - rectangle.Top)) * 2.0);
      rectangle1.Width = (int) ((double) rectangle1.Width * 1.6);
      rectangle1.X = (int) ((double) rectangle.X - (double) (rectangle1.Width - rectangle.Width) / 2.0);
      if (!this.CheckValidRectangle(rectangle1))
        return;
      if (color1.A > (byte) 3)
      {
        using (GraphicsPath path = new GraphicsPath())
        {
          path.AddArc(rectangle1, 180f, 180f);
          using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
          {
            pathGradientBrush.SurroundColors = new Color[1]
            {
              Color.Transparent
            };
            pathGradientBrush.CenterColor = this.GetColor(color4);
            pathGradientBrush.CenterPoint = new PointF((float) rectangle.Width / 2f, (float) rectangle1.Y + (float) rectangle1.Height / 2f);
            pathGradientBrush.FocusScales = new PointF(0.5f, 0.5f);
            this.graphics.FillRectangle((Brush) pathGradientBrush, rectangle);
          }
        }
      }
      this.RestoreClippingByPath();
      this.RestoreSmoothingMode();
    }

    private GraphicsPath SetClippingByPath(Rectangle rectangle)
    {
      this.oldClip = (Region) null;
      this.graphicsClip = (Region) null;
      GraphicsPath path = this.UseFillPath((RectangleF) rectangle);
      if (path != null)
      {
        this.oldClip = this.graphics.Clip;
        this.graphicsClip = new Region(path);
        this.graphics.Clip = this.graphicsClip;
      }
      return path;
    }

    private void RestoreClippingByPath()
    {
      if (this.oldClip != null)
        this.graphics.Clip = this.oldClip;
      if (this.graphicsClip == null)
        return;
      this.graphicsClip.Dispose();
    }

    public virtual void FillGlassRectangleNew(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2)
    {
      if (!this.CheckValidRectangle(rectangle))
        return;
      this.ChangeSmoothingMode(SmoothingMode.None);
      Rectangle rect1 = rectangle;
      int num = (int) Math.Round((double) rectangle.Height * (1.0 - (double) gradientPercentage));
      rect1.Height = num + 1;
      Graphics graphics = this.graphics;
      Bitmap bitmap = (Bitmap) null;
      GraphicsPath path1 = this.UseFillPath((RectangleF) rectangle);
      if (path1 != null)
      {
        bitmap = new Bitmap(rectangle.Width, rectangle.Height);
        graphics = Graphics.FromImage((Image) bitmap);
        graphics.TranslateTransform((float) -rectangle.X, (float) -rectangle.Y);
      }
      if (color1.A > (byte) 3)
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this.GetColor(color1), this.GetColor(color2), LinearGradientMode.Vertical))
          graphics.FillRectangle((Brush) linearGradientBrush, rect1);
      }
      Rectangle rect2 = rectangle;
      rect2.Y = rectangle.Top + num;
      rect2.Height = rectangle.Height - num;
      using (SolidBrush solidBrush = new SolidBrush(this.GetColor(color3)))
        graphics.FillRectangle((Brush) solidBrush, rect2);
      Rectangle rectangle1 = rect2;
      rectangle1.Y = rectangle.Top + (int) ((double) num * (double) gradientPercentage2);
      rectangle1.Height = (int) ((double) (rectangle.Height - (rectangle1.Y - rectangle.Top)) * 2.0);
      rectangle1.Width = (int) ((double) rectangle1.Width * 1.6);
      rectangle1.X = (int) ((double) rectangle.X - (double) (rectangle1.Width - rectangle.Width) / 2.0);
      if (!this.CheckValidRectangle(rectangle1))
        return;
      if (color1.A > (byte) 3)
      {
        using (GraphicsPath path2 = new GraphicsPath())
        {
          path2.AddArc(rectangle1, 180f, 180f);
          using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path2))
          {
            pathGradientBrush.SurroundColors = new Color[1]
            {
              Color.Transparent
            };
            pathGradientBrush.CenterColor = this.GetColor(color4);
            pathGradientBrush.CenterPoint = new PointF(pathGradientBrush.CenterPoint.X, (float) rectangle1.Y + (float) rectangle1.Height / 2f);
            pathGradientBrush.FocusScales = new PointF(0.5f, 0.5f);
            graphics.FillRectangle((Brush) pathGradientBrush, rectangle);
          }
        }
      }
      if (path1 != null)
      {
        graphics.TranslateTransform((float) rectangle.X, (float) rectangle.Y);
        using (TextureBrush textureBrush = new TextureBrush((Image) bitmap))
        {
          textureBrush.TranslateTransform((float) rect2.X, (float) rect2.Y);
          textureBrush.WrapMode = WrapMode.Clamp;
          this.graphics.FillPath((Brush) textureBrush, path1);
        }
        bitmap.Dispose();
      }
      this.RestoreSmoothingMode();
    }

    public void FillVistaRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2)
    {
      this.DrawOfficeGlassRectangle(this.graphics, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1), this.GetColor(color1), this.GetColor(color2), this.GetColor(color3), this.GetColor(color4), gradientPercentage, gradientPercentage2);
    }

    private static GraphicsPath OfficeGlassRoundedRectangleTop(
      Rectangle boundingRect,
      int cornerRadius,
      int margin)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddArc(boundingRect.X + margin, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
      graphicsPath.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
      graphicsPath.CloseFigure();
      Rectangle rect = boundingRect;
      rect.Y = (int) graphicsPath.GetBounds().Bottom;
      graphicsPath.AddRectangle(rect);
      return graphicsPath;
    }

    private static GraphicsPath OfficeGlassRoundedRectangleBottom(
      Rectangle boundingRect,
      int cornerRadius,
      int margin)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0.0f, 90f);
      graphicsPath.AddArc(boundingRect.X + margin, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90f, 90f);
      graphicsPath.CloseFigure();
      Rectangle rect = boundingRect;
      rect.Height = boundingRect.Height - (int) graphicsPath.GetBounds().Height;
      graphicsPath.AddRectangle(rect);
      return graphicsPath;
    }

    private void DrawOfficeGlassInnerArea(
      Graphics g,
      GraphicsPath innerPath,
      Rectangle rec,
      Color surroundColor)
    {
      innerPath.AddRectangle(rec);
      if (this.IsInvalidRectangle(rec))
        return;
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(innerPath))
      {
        pathGradientBrush.SurroundColors = new Color[1]
        {
          this.ReduceAlphaBasedOnOriginal(20, this.GetColor(surroundColor))
        };
        pathGradientBrush.CenterColor = this.GetColor(Color.White);
        g.FillPath((Brush) pathGradientBrush, innerPath);
      }
    }

    private void DrawOfficeGlassRectangle(
      Graphics g,
      Rectangle buttonRect,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage1,
      float gradientPercentage2)
    {
      if (this.IsInvalidRectangle(buttonRect))
        return;
      int cornerRadius = Math.Max(1, Math.Min(buttonRect.Width, buttonRect.Height) / 10);
      if ((double) gradientPercentage2 == 0.0)
        gradientPercentage2 = 0.1f;
      Rectangle rectangle1 = new Rectangle(buttonRect.X, buttonRect.Y, buttonRect.Width, (int) ((double) gradientPercentage1 * (double) buttonRect.Height / 3.0) + 1);
      Rectangle rectangle2 = new Rectangle(buttonRect.X, buttonRect.Y + (int) ((double) gradientPercentage1 * (double) buttonRect.Height / 3.0), buttonRect.Width, (int) ((double) gradientPercentage2 * (double) buttonRect.Height / 3.0));
      Rectangle rectangle3 = new Rectangle(buttonRect.X, buttonRect.Y + (int) ((double) gradientPercentage1 * (double) buttonRect.Height / 3.0) + (int) ((double) gradientPercentage2 * (double) buttonRect.Height / 3.0), buttonRect.Width, buttonRect.Height - (int) ((double) gradientPercentage1 * (double) buttonRect.Height / 3.0) - (int) ((double) gradientPercentage2 * (double) buttonRect.Height / 3.0));
      if (this.IsInvalidRectangle(rectangle1) || this.IsInvalidRectangle(rectangle2) || this.IsInvalidRectangle(rectangle3))
        return;
      using (LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(rectangle1, this.ReduceAlphaBasedOnOriginal(50, color2), color2, LinearGradientMode.Vertical))
      {
        using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rectangle2, color4, color1, LinearGradientMode.Vertical))
        {
          using (LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(rectangle3, color3, color4, LinearGradientMode.Vertical))
          {
            using (GraphicsPath path1 = RadGdiGraphics.OfficeGlassRoundedRectangleTop(rectangle1, cornerRadius, 0))
            {
              using (GraphicsPath path2 = new GraphicsPath())
              {
                using (GraphicsPath path3 = RadGdiGraphics.OfficeGlassRoundedRectangleBottom(rectangle3, cornerRadius, 0))
                {
                  path2.AddRectangle(rectangle2);
                  g.FillPath((Brush) linearGradientBrush1, path1);
                  g.FillPath((Brush) linearGradientBrush2, path2);
                  g.FillPath((Brush) linearGradientBrush3, path3);
                }
              }
            }
          }
        }
      }
      using (GraphicsPath innerPath = new GraphicsPath())
        this.DrawOfficeGlassInnerArea(g, innerPath, buttonRect, color4);
    }

    private bool IsInvalidRectangle(Rectangle rec)
    {
      return rec.Width <= 0 || rec.Height <= 0;
    }

    private bool IsInvalidRectangle(RectangleF rec)
    {
      return (int) rec.Width <= 0 || (int) rec.Height <= 0;
    }

    public void FillOfficeGlassRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2,
      bool drawEllipse)
    {
      if (rectangle.IsEmpty)
        return;
      color1 = this.GetColor(color1);
      color2 = this.GetColor(color2);
      color3 = this.GetColor(color3);
      color4 = this.GetColor(color4);
      Rectangle rect1 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
      this.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      GraphicsPath path1 = this.UseFillPath((RectangleF) rectangle);
      int num1 = (int) Math.Round((double) rect1.Height - (double) (rect1.Height / 2) * (double) gradientPercentage2);
      Rectangle rect2 = rect1;
      if (drawEllipse)
      {
        using (SolidBrush solidBrush = new SolidBrush(color3))
          this.graphics.FillEllipse((Brush) solidBrush, rect2);
      }
      else if (path1 != null)
      {
        using (SolidBrush solidBrush = new SolidBrush(color3))
          this.graphics.FillPath((Brush) solidBrush, path1);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(color3))
          this.graphics.FillRectangle((Brush) solidBrush, rect2);
      }
      this.graphics.CompositingQuality = CompositingQuality.HighQuality;
      for (int index = 0; index < 2; ++index)
      {
        using (GraphicsPath path2 = new GraphicsPath())
        {
          Rectangle rectangle1 = rect1;
          rectangle1.Y += num1 - (int) ((double) rectangle1.Height * 0.1);
          rectangle1.Width = (int) ((double) rectangle1.Width * 1.5);
          rectangle1.X = rect1.Width / 2 - rectangle1.Width / 2 + rectangle1.X;
          if (this.IsInvalidRectangle(rectangle1))
          {
            this.RestoreSmoothingMode();
            return;
          }
          path2.AddEllipse(rectangle1);
          using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path2))
          {
            pathGradientBrush.SurroundColors = new Color[1]
            {
              this.ReduceAlphaBasedOnOriginal(100, color3)
            };
            pathGradientBrush.CenterColor = this.ReduceAlphaBasedOnOriginal(220, color4);
            if (drawEllipse)
              this.graphics.FillEllipse((Brush) pathGradientBrush, rect1);
            else if (path1 != null)
              this.graphics.FillPath((Brush) pathGradientBrush, path1);
            else
              this.graphics.FillRectangle((Brush) pathGradientBrush, rect1);
          }
        }
      }
      using (GraphicsPath path2 = new GraphicsPath())
      {
        Rectangle rect3 = Rectangle.Inflate(rect1, 0, 0);
        rect3.Y = rect3.Y - rect3.Height / 2 - (int) ((double) rect3.Height * 0.02);
        rect3.Width = (int) ((double) rect3.Width * (1.2 + (double) gradientPercentage));
        rect3.X = rect1.Width / 2 - rect3.Width / 2 + rect3.X;
        path2.AddEllipse(rect3);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path2))
        {
          pathGradientBrush.SurroundColors = new Color[1]
          {
            this.ReduceAlphaBasedOnOriginal(5, color2)
          };
          pathGradientBrush.CenterPoint = new PointF((float) (rect3.X + rect3.Width / 2), (float) rect3.Y + (float) rect3.Height * 0.5f);
          pathGradientBrush.CenterColor = this.ReduceAlphaBasedOnOriginal(200, color1);
          if (drawEllipse)
            this.graphics.FillEllipse((Brush) pathGradientBrush, rect1);
          else if (path1 != null)
            this.graphics.FillPath((Brush) pathGradientBrush, path1);
          else
            this.graphics.FillRectangle((Brush) pathGradientBrush, rect1);
        }
      }
      for (int index = 0; index < 3; ++index)
      {
        using (GraphicsPath path2 = new GraphicsPath())
        {
          Rectangle rectangle1 = Rectangle.Inflate(rect1, 0, 0);
          rectangle1.Y += (int) ((double) rect1.Height * 0.08);
          rectangle1.Height = (int) ((double) rect1.Height * 0.77);
          rectangle1.Width = (int) ((double) rectangle1.Width * 0.8);
          rectangle1.X = rect1.Width / 2 - rectangle1.Width / 2 + rectangle1.X;
          if (this.IsInvalidRectangle(rectangle1))
          {
            this.RestoreSmoothingMode();
            return;
          }
          path2.AddEllipse(rectangle1);
          using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path2))
          {
            pathGradientBrush.SurroundColors = new Color[1]
            {
              this.GetColor(this.ReduceAlphaBasedOnOriginal(0, color2))
            };
            pathGradientBrush.CenterColor = this.GetColor(this.ReduceAlphaBasedOnOriginal(130, color3));
            if (drawEllipse)
              this.graphics.FillEllipse((Brush) pathGradientBrush, rect1);
            else if (path1 != null)
              this.graphics.FillPath((Brush) pathGradientBrush, path1);
            else
              this.graphics.FillRectangle((Brush) pathGradientBrush, rect1);
          }
        }
      }
      Color color5 = this.GetColor(this.ReduceAlphaBasedOnOriginal(50, color1));
      Color color6 = this.GetColor(this.ReduceAlphaBasedOnOriginal(50, color4));
      int num2 = Math.Max(1, (int) ((double) rect1.Width * 0.02));
      Rectangle rectangle2 = Rectangle.Inflate(rect1, -num2, -num2);
      if (!this.IsInvalidRectangle(rectangle2))
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle2, color5, color6, LinearGradientMode.Vertical))
        {
          linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
          using (Pen pen = new Pen((Brush) linearGradientBrush))
          {
            pen.Width = (float) num2;
            if (drawEllipse)
              this.graphics.DrawEllipse(pen, rectangle2);
            else if (path1 != null)
              this.graphics.DrawPath(pen, path1);
            else
              this.graphics.DrawRectangle(pen, rectangle2);
          }
        }
      }
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, this.ReduceAlphaBasedOnOriginal(100, color3), color3, LinearGradientMode.Vertical))
      {
        linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
        using (Pen pen = new Pen((Brush) linearGradientBrush))
        {
          pen.Width = (float) num2;
          if (drawEllipse)
            this.graphics.DrawEllipse(pen, rect1);
          else if (path1 != null)
            this.graphics.DrawPath(pen, path1);
          else
            this.graphics.DrawRectangle(pen, rect1);
        }
      }
      this.RestoreSmoothingMode();
    }

    private Color ReduceAlphaBasedOnOriginal(int newA, Color color)
    {
      return Color.FromArgb((int) ((double) newA * ((double) color.A / (double) byte.MaxValue)), color);
    }

    public virtual void FillGellRectangle(
      Rectangle rectangle,
      Color[] colorStops,
      float gradientPercentage,
      float gradientPercentage2)
    {
      this.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      float[] numArray = new float[1];
      if (colorStops.Length < 2)
      {
        this.FillRectangle(rectangle, colorStops[0]);
      }
      else
      {
        float[] colorOffsets;
        if (colorStops.Length > 3)
          colorOffsets = new float[4]
          {
            0.0f,
            0.333f,
            0.666f,
            1f
          };
        else if (colorStops.Length > 2)
          colorOffsets = new float[3]{ 0.0f, 0.5f, 1f };
        else
          colorOffsets = new float[2]{ 0.0f, 1f };
        this.FillGradientRectangle(rectangle, colorStops, colorOffsets, GradientStyles.Linear, 90f, 0.0f, 1f);
      }
      float num1 = (float) rectangle.Height / 2f;
      float num2 = (float) (int) Math.Round(100.0 * (double) gradientPercentage * 0.2);
      float num3 = (float) (int) Math.Round((double) num1 * (double) gradientPercentage2 * 0.2);
      RectangleF rectangleF = new RectangleF((float) rectangle.X + num2, (float) rectangle.Y, (float) ((double) rectangle.Width - (double) num2 * 2.0 - 1.0), num1 - num3 * 2f);
      if (!this.IsInvalidRectangle(rectangleF))
      {
        using (GraphicsPath roundedRect = this.GetRoundedRect(rectangleF, rectangleF.Height))
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(RectangleF.Inflate(rectangleF, 1f, 1f), this.ReduceAlphaBasedOnOriginal(253, this.GetColor(Color.White)), this.ReduceAlphaBasedOnOriginal(42, this.GetColor(Color.White)), LinearGradientMode.Vertical))
            this.graphics.FillPath((Brush) linearGradientBrush, roundedRect);
        }
      }
      this.RestoreSmoothingMode();
    }

    public void FillPolygon(Color color, Point[] points)
    {
      using (Brush brush = (Brush) new SolidBrush(color))
        this.graphics.FillPolygon(brush, points);
    }

    public void FillPolygon(Brush brush, PointF[] points)
    {
      this.graphics.FillPolygon(brush, points);
    }

    public void FillPolygon(Color color, PointF[] points)
    {
      using (Brush brush = (Brush) new SolidBrush(color))
        this.graphics.FillPolygon(brush, points);
    }

    public virtual void DrawRoundRect(
      Rectangle rectangle,
      Color color,
      float borderWidth,
      int radius)
    {
      using (GraphicsPath roundedRect = this.GetRoundedRect((RectangleF) rectangle, (float) radius))
      {
        using (Pen pen = new Pen(this.GetColor(color), borderWidth))
          this.graphics.DrawPath(pen, roundedRect);
      }
    }

    public virtual void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawPath(path, color, penAlignment, penWidth, DashStyle.Solid);
    }

    public virtual void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle)
    {
      this.DrawPath(path, color, penAlignment, penWidth, (Brush) null, dashStyle);
    }

    public virtual void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      this.DrawPath(path, color, penAlignment, penWidth, (Brush) null, dashStyle, dashPattern);
    }

    public static RectangleF NormalizeRect(RectangleF actual)
    {
      float x = actual.X;
      float y = actual.Y;
      float width = actual.Width;
      float height = actual.Height;
      if ((double) actual.Width < 0.0)
      {
        x -= actual.Width;
        width = -actual.Width;
      }
      if ((double) actual.Height < 0.0)
      {
        y -= actual.Height;
        height = -actual.Height;
      }
      return new RectangleF(x, y, width, height);
    }

    public void DrawLinearGradientPath(
      GraphicsPath path,
      RectangleF bounds,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle)
    {
      this.DrawLinearGradientPath(path, bounds, gradientColors, penAlignment, penWidth, angle, DashStyle.Solid);
    }

    public void DrawLinearGradientPath(
      GraphicsPath path,
      RectangleF bounds,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle)
    {
      if ((double) bounds.Width == 0.0 || (double) bounds.Height == 0.0)
        return;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(RadGdiGraphics.NormalizeRect(bounds), gradientColors[0], gradientColors[1], angle))
      {
        if (gradientColors.Length > 2)
        {
          ColorBlend colorBlend = new ColorBlend();
          colorBlend.Colors = this.GetColors(gradientColors);
          if (gradientColors.Length == 4)
            colorBlend.Positions = new float[4]
            {
              0.0f,
              0.333f,
              0.666f,
              1f
            };
          else if (gradientColors.Length == 3)
            colorBlend.Positions = new float[3]
            {
              0.0f,
              0.5f,
              1f
            };
          linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
          linearGradientBrush.InterpolationColors = colorBlend;
        }
        this.DrawPath(path, gradientColors[0], penAlignment, penWidth, (Brush) linearGradientBrush, dashStyle);
      }
    }

    public void DrawLinearGradientPath(
      GraphicsPath path,
      RectangleF bounds,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      if ((double) bounds.Width == 0.0 || (double) bounds.Height == 0.0)
        return;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(RadGdiGraphics.NormalizeRect(bounds), gradientColors[0], gradientColors[1], angle))
      {
        if (gradientColors.Length > 2)
        {
          ColorBlend colorBlend = new ColorBlend();
          colorBlend.Colors = this.GetColors(gradientColors);
          if (gradientColors.Length == 4)
            colorBlend.Positions = new float[4]
            {
              0.0f,
              0.333f,
              0.666f,
              1f
            };
          else if (gradientColors.Length == 3)
            colorBlend.Positions = new float[3]
            {
              0.0f,
              0.5f,
              1f
            };
          linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
          linearGradientBrush.InterpolationColors = colorBlend;
        }
        this.DrawPath(path, gradientColors[0], penAlignment, penWidth, (Brush) linearGradientBrush, dashStyle, dashPattern);
      }
    }

    public void DrawRadialGradientPath(
      GraphicsPath path,
      Rectangle bounds,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawRadialGradientPath(path, bounds, color, gradientColors, penAlignment, penWidth, DashStyle.Solid);
    }

    public void DrawRadialGradientPath(
      GraphicsPath path,
      Rectangle bounds,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle)
    {
      using (GraphicsPath gradientPath = new GraphicsPath())
      {
        gradientPath.AddEllipse(bounds);
        this.DrawCustomGradientPath(path, gradientPath, color, gradientColors, penAlignment, penWidth, dashStyle);
      }
    }

    public void DrawRadialGradientPath(
      GraphicsPath path,
      Rectangle bounds,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      using (GraphicsPath gradientPath = new GraphicsPath())
      {
        gradientPath.AddEllipse(bounds);
        this.DrawCustomGradientPath(path, gradientPath, color, gradientColors, penAlignment, penWidth, dashStyle, dashPattern);
      }
    }

    public void DrawCustomGradientPath(
      GraphicsPath path,
      GraphicsPath gradientPath,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth)
    {
      this.DrawCustomGradientPath(path, gradientPath, color, gradientColors, penAlignment, penWidth, DashStyle.Solid);
    }

    public void DrawCustomGradientPath(
      GraphicsPath path,
      GraphicsPath gradientPath,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(gradientPath))
      {
        pathGradientBrush.CenterColor = this.GetColor(color);
        pathGradientBrush.SurroundColors = this.GetColors(gradientColors);
        this.DrawPath(path, color, penAlignment, penWidth, (Brush) pathGradientBrush, dashStyle);
      }
    }

    public void DrawCustomGradientPath(
      GraphicsPath path,
      GraphicsPath gradientPath,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(gradientPath))
      {
        pathGradientBrush.CenterColor = this.GetColor(color);
        pathGradientBrush.SurroundColors = this.GetColors(gradientColors);
        this.DrawPath(path, color, penAlignment, penWidth, (Brush) pathGradientBrush, dashStyle, dashPattern);
      }
    }

    private void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush)
    {
      this.DrawPath(path, color, penAlignment, penWidth, brush, DashStyle.Solid);
    }

    private void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle)
    {
      this.DrawPath(path, color, penAlignment, penWidth, brush, dashStyle, LineJoin.MiterClipped);
    }

    private void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle,
      float[] dashPattern)
    {
      this.DrawPath(path, color, penAlignment, penWidth, brush, dashStyle, LineJoin.MiterClipped, dashPattern);
    }

    private void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle,
      LineJoin lineJoin)
    {
      this.DrawPath(path, color, penAlignment, penWidth, brush, dashStyle, lineJoin, 1f);
    }

    private void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle,
      LineJoin lineJoin,
      float[] dashPattern)
    {
      this.DrawPath(path, color, penAlignment, penWidth, brush, dashStyle, lineJoin, 1f, dashPattern);
    }

    private void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle,
      LineJoin lineJoin,
      float miterLimit)
    {
      using (Pen pen = new Pen(this.GetColor(color)))
      {
        pen.Width = penWidth;
        pen.Alignment = penAlignment;
        pen.DashStyle = dashStyle;
        pen.LineJoin = lineJoin;
        pen.MiterLimit = miterLimit;
        if (brush != null)
          pen.Brush = brush;
        this.graphics.DrawPath(pen, path);
      }
    }

    private void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      Brush brush,
      DashStyle dashStyle,
      LineJoin lineJoin,
      float miterLimit,
      float[] dashPattern)
    {
      using (Pen pen = new Pen(this.GetColor(color)))
      {
        pen.Width = penWidth;
        pen.Alignment = penAlignment;
        pen.DashStyle = dashStyle;
        if (dashStyle == DashStyle.Custom && dashPattern != null)
          pen.DashPattern = dashPattern;
        pen.LineJoin = lineJoin;
        pen.MiterLimit = miterLimit;
        if (brush != null)
          pen.Brush = brush;
        this.graphics.DrawPath(pen, path);
      }
    }

    public void FillPath(Color color, GraphicsPath path)
    {
      this.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      using (SolidBrush solidBrush = new SolidBrush(color))
        this.graphics.FillPath((Brush) solidBrush, path);
      this.RestoreSmoothingMode();
    }

    public void DrawBlurShadow(GraphicsPath path, Rectangle r, float offset, Color color)
    {
      float x = (float) (1.0 - (double) offset * 2.0 / (double) r.Width);
      float y = (float) (1.0 - (double) offset * 2.0 / (double) r.Height);
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
      {
        pathGradientBrush.CenterPoint = PointF.Empty;
        pathGradientBrush.CenterColor = color;
        pathGradientBrush.FocusScales = new PointF(x, y);
        pathGradientBrush.SurroundColors = new Color[1]
        {
          Color.FromArgb(100, color)
        };
        this.graphics.FillPath((Brush) pathGradientBrush, path);
      }
    }

    public void FillPath(
      Color[] colorStops,
      float[] colorOffsets,
      float angle,
      float gradientPercentage,
      float gradientPercentage2,
      Rectangle rectangle,
      GraphicsPath path)
    {
      if (this.IsInvalidRectangle(rectangle))
        return;
      this.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      Brush brush;
      if (colorStops.Length == 1)
        brush = (Brush) new SolidBrush(this.GetColor(colorStops[0]));
      else
        brush = (Brush) new LinearGradientBrush(rectangle, Color.Black, Color.Black, angle)
        {
          InterpolationColors = new ColorBlend()
          {
            Colors = this.GetColors(colorStops),
            Positions = colorOffsets
          }
        };
      this.graphics.FillPath(brush, path);
      brush.Dispose();
      this.RestoreSmoothingMode();
    }

    public Image ImageFromText(string strText, Font fnt, Color clrFore, Color clrBack)
    {
      Bitmap bitmap1 = (Bitmap) null;
      using (Graphics graphics1 = Graphics.FromHwnd(IntPtr.Zero))
      {
        SizeF sizeF = graphics1.MeasureString(strText, fnt);
        using (Bitmap bitmap2 = new Bitmap((int) sizeF.Width, (int) sizeF.Height))
        {
          using (Graphics graphics2 = Graphics.FromImage((Image) bitmap2))
          {
            using (SolidBrush solidBrush1 = new SolidBrush(this.ReduceAlphaBasedOnOriginal(16, this.GetColor(clrBack))))
            {
              using (SolidBrush solidBrush2 = new SolidBrush(clrFore))
              {
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                graphics2.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics2.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics2.DrawString(strText, fnt, (Brush) solidBrush1, 0.0f, 0.0f);
                bitmap1 = new Bitmap(bitmap2.Width + 6, bitmap2.Height + 6);
                using (Graphics graphics3 = Graphics.FromImage((Image) bitmap1))
                {
                  graphics3.SmoothingMode = SmoothingMode.HighQuality;
                  graphics3.InterpolationMode = InterpolationMode.HighQualityBilinear;
                  graphics3.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                  for (int x = 0; x <= 6; ++x)
                  {
                    for (int y = 0; y <= 6; ++y)
                      graphics3.DrawImageUnscaled((Image) bitmap2, x, y);
                  }
                  graphics3.DrawString(strText, fnt, (Brush) solidBrush2, 3f, 3f);
                }
              }
            }
          }
        }
      }
      return (Image) bitmap1;
    }

    public virtual void DrawString(TextParams textParams, SizeF measuredSize)
    {
      if ((double) textParams.paintingRectangle.Width <= 0.0 || (double) textParams.paintingRectangle.Height <= 0.0)
        return;
      StringFormat stringFormat = textParams.CreateStringFormat();
      this.DrawString(textParams.text, textParams.paintingRectangle, textParams.font, textParams.foreColor, stringFormat, textParams.shadow, textParams.textRenderingHint, textParams.textOrientation, textParams.flipText, textParams.highlightRanges, textParams.highlightColor);
      stringFormat.Dispose();
    }

    public virtual void DrawString(
      string s,
      Rectangle rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      Orientation orientation,
      bool flipText)
    {
      this.DrawString(s, (RectangleF) rectangle, font, foreColor, stringFormat, (ShadowSettings) null, TextRenderingHint.SystemDefault, orientation, flipText, (CharacterRange[]) null, Color.Empty);
    }

    public virtual void DrawString(
      string s,
      RectangleF rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      Orientation orientation,
      bool flipText)
    {
      this.DrawString(s, rectangle, font, foreColor, stringFormat, (ShadowSettings) null, TextRenderingHint.SystemDefault, orientation, flipText, (CharacterRange[]) null, Color.Empty);
    }

    public virtual void DrawString(
      string s,
      Rectangle rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      Orientation orientation,
      bool flipText,
      CharacterRange[] highlightRanges,
      Color highlightColor)
    {
      this.DrawString(s, (RectangleF) rectangle, font, foreColor, stringFormat, (ShadowSettings) null, TextRenderingHint.SystemDefault, orientation, flipText, highlightRanges, highlightColor);
    }

    public virtual void DrawString(
      string s,
      RectangleF rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      Orientation orientation,
      bool flipText,
      CharacterRange[] highlightRanges,
      Color highlightColor)
    {
      this.DrawString(s, rectangle, font, foreColor, stringFormat, (ShadowSettings) null, TextRenderingHint.SystemDefault, orientation, flipText, highlightRanges, highlightColor);
    }

    public virtual void DrawString(
      string s,
      Rectangle rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText)
    {
      this.DrawString(s, (RectangleF) rectangle, font, foreColor, stringFormat, shadow, textRendering, orientation, flipText, (CharacterRange[]) null, Color.Empty);
    }

    public virtual void DrawString(
      string s,
      RectangleF rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText)
    {
      this.DrawString(s, rectangle, font, foreColor, stringFormat, shadow, textRendering, orientation, flipText, (CharacterRange[]) null, Color.Empty);
    }

    public virtual void DrawString(
      string s,
      Rectangle rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText,
      CharacterRange[] highlightRanges,
      Color highlightColor)
    {
      this.DrawString(s, (RectangleF) rectangle, font, foreColor, stringFormat, shadow, textRendering, orientation, flipText, (CharacterRange[]) null, Color.Empty);
    }

    public virtual void DrawString(
      string s,
      RectangleF rectangle,
      Font font,
      Color foreColor,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText,
      CharacterRange[] highlightRanges,
      Color highlightColor)
    {
      if (!this.CheckValidString(s, font, rectangle))
        return;
      string str = s.Length <= RadGdiGraphics.GdiStringLengthLimit ? s : s.Substring(0, RadGdiGraphics.GdiStringLengthLimit);
      RectangleF rectangleF = rectangle;
      SizeF sizeF = SizeF.Empty;
      float angle = 0.0f;
      TextRenderingHint textRenderingHint = this.graphics.TextRenderingHint;
      if (this.graphics.TextRenderingHint != textRendering)
        this.graphics.TextRenderingHint = textRendering;
      if (orientation != Orientation.Horizontal || flipText)
      {
        sizeF = new SizeF(new PointF(rectangleF.X + rectangleF.Width / 2f, rectangleF.Y + rectangleF.Height / 2f));
        rectangleF.X = (float) (-(double) rectangleF.Width / 2.0);
        rectangleF.Y = (float) (-(double) rectangleF.Height / 2.0);
      }
      if (orientation == Orientation.Vertical)
      {
        rectangleF = new RectangleF(rectangleF.Y, rectangleF.X, rectangleF.Height, rectangleF.Width);
        angle = 90f;
      }
      if (flipText)
        angle += 180f;
      if (orientation != Orientation.Horizontal || flipText)
      {
        this.graphics.TranslateTransform(sizeF.Width, sizeF.Height);
        this.graphics.RotateTransform(angle);
      }
      if (shadow != null && !shadow.Depth.IsEmpty)
      {
        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int) ((double) shadow.ShadowColor.A * this.opacity), this.graphics.GetNearestColor(shadow.ShadowColor))))
        {
          RectangleF layoutRectangle = new RectangleF(rectangleF.X + (float) shadow.Depth.X, rectangleF.Y + (float) shadow.Depth.Y, rectangleF.Width, rectangleF.Height);
          try
          {
            this.graphics.DrawString(str, font, (Brush) solidBrush, layoutRectangle, stringFormat);
          }
          catch
          {
            this.graphics.DrawString(str, Control.DefaultFont, (Brush) solidBrush, layoutRectangle, stringFormat);
          }
        }
      }
      if (highlightRanges != null && highlightColor != Color.Empty)
      {
        using (Brush brush = (Brush) new SolidBrush(highlightColor))
        {
          stringFormat.SetMeasurableCharacterRanges(highlightRanges);
          foreach (Region measureCharacterRange in this.graphics.MeasureCharacterRanges(str, font, rectangleF, stringFormat))
          {
            Rectangle rect = Rectangle.Round(measureCharacterRange.GetBounds(this.graphics));
            this.graphics.FillRectangle(brush, rect);
          }
        }
      }
      using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int) ((double) foreColor.A * this.opacity), this.graphics.GetNearestColor(foreColor))))
      {
        try
        {
          this.graphics.DrawString(str, font, (Brush) solidBrush, rectangleF, stringFormat);
        }
        catch
        {
          this.graphics.DrawString(str, Control.DefaultFont, (Brush) solidBrush, rectangleF, stringFormat);
        }
      }
      if (this.graphics.TextRenderingHint != textRenderingHint)
        this.graphics.TextRenderingHint = textRenderingHint;
      if (orientation == Orientation.Horizontal && !flipText)
        return;
      this.graphics.RotateTransform(-angle);
      this.graphics.TranslateTransform(-sizeF.Width, -sizeF.Height);
    }

    public virtual void DrawImage(
      Rectangle rectangle,
      Image image,
      ContentAlignment alignment,
      bool enabled)
    {
      if (image == null)
        return;
      this.DrawImage(this.CalculateCoordinates(alignment, rectangle, image.Size.Width, image.Size.Height), image, enabled);
    }

    public virtual void DrawImage(Point point, Image image, bool enabled)
    {
      if (enabled)
        this.DrawBitmap(image, point.X, point.Y);
      else
        ControlPaint.DrawImageDisabled(this.graphics, image, point.X, point.Y, Color.Transparent);
    }

    public virtual void DrawBitmap(Image image, int x, int y)
    {
      this.DrawBitmap(image, x, y, this.opacity);
    }

    public virtual void DrawBitmap(Image image, int x, int y, double opacity)
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

    public virtual void DrawBitmap(Image image, int x, int y, int width, int height)
    {
      this.DrawBitmap(image, x, y, width, height, this.opacity);
    }

    public virtual void DrawBitmap(
      Image image,
      int x,
      int y,
      int width,
      int height,
      double opacity)
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

    public Bitmap CreateBitmapMask(Color maskColor, Bitmap bitmap)
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

    private bool CheckValidString(string drawString, Font font, Rectangle rectangle)
    {
      return !string.IsNullOrEmpty(drawString) && font != null && (rectangle.Height >= 1 && rectangle.Width >= 1);
    }

    private bool CheckValidString(string drawString, Font font, RectangleF rectangle)
    {
      return !string.IsNullOrEmpty(drawString) && font != null && ((double) rectangle.Height >= 1.0 && (double) rectangle.Width >= 1.0);
    }

    private Point CalculateCoordinates(
      ContentAlignment alignment,
      Rectangle rectangle,
      int width,
      int height)
    {
      int left = rectangle.Left;
      int top = rectangle.Top;
      if (alignment <= ContentAlignment.MiddleCenter)
      {
        switch (alignment)
        {
          case ContentAlignment.TopCenter:
            left += (rectangle.Width - width) / 2;
            break;
          case ContentAlignment.TopRight:
            left += rectangle.Width - width;
            break;
          case ContentAlignment.MiddleLeft:
            top += (rectangle.Height - height) / 2;
            break;
          case ContentAlignment.MiddleCenter:
            left += (rectangle.Width - width) / 2;
            top += (rectangle.Height - height) / 2;
            break;
        }
      }
      else if (alignment <= ContentAlignment.BottomLeft)
      {
        if (alignment == ContentAlignment.MiddleRight)
        {
          left += rectangle.Width - width;
          top += (rectangle.Height - height) / 2;
        }
        else if (alignment == ContentAlignment.BottomLeft)
          top += rectangle.Height - height;
      }
      else if (alignment != ContentAlignment.BottomCenter)
      {
        if (alignment == ContentAlignment.BottomRight)
        {
          left += rectangle.Width - width;
          top += rectangle.Height - height;
        }
      }
      else
      {
        left += (rectangle.Width - width) / 2;
        top += rectangle.Height - height;
      }
      return new Point(left, top);
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

    private GraphicsPath GetCapsule(RectangleF baseRect)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      if ((double) baseRect.Height > 0.0)
      {
        if ((double) baseRect.Width > 0.0)
        {
          try
          {
            if ((double) baseRect.Width > (double) baseRect.Height)
            {
              float height = baseRect.Height;
              SizeF size = new SizeF(height, height);
              RectangleF rect = new RectangleF(baseRect.Location, size);
              graphicsPath.AddArc(rect, 90f, 180f);
              rect.X = baseRect.Right - height;
              graphicsPath.AddArc(rect, 270f, 180f);
            }
            else if ((double) baseRect.Width < (double) baseRect.Height)
            {
              float width = baseRect.Width;
              SizeF size = new SizeF(width, width);
              RectangleF rect = new RectangleF(baseRect.Location, size);
              graphicsPath.AddArc(rect, 180f, 180f);
              rect.Y = baseRect.Bottom - width;
              graphicsPath.AddArc(rect, 0.0f, 180f);
            }
            else
              graphicsPath.AddEllipse(baseRect);
          }
          catch (Exception ex)
          {
            graphicsPath.AddEllipse(baseRect);
          }
          finally
          {
            graphicsPath.CloseFigure();
          }
          return graphicsPath;
        }
      }
      graphicsPath.AddEllipse(baseRect);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }

    public void Destroy()
    {
      this.Dispose(true);
    }

    public void Dispose()
    {
      this.Dispose(false);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool finalizing)
    {
      if (finalizing)
        return;
      this.graphics.Dispose();
    }

    ~RadGdiGraphics()
    {
      this.Destroy();
    }

    [Obsolete("Use the overload without ContentAlignment parameter. ContentAlignment should be provided through the StringFormat parameter.")]
    public virtual void DrawString(
      string s,
      Rectangle rectangle,
      Font font,
      Color foreColor,
      ContentAlignment alignment,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText)
    {
      stringFormat.Alignment = TelerikAlignHelper.TranslateAlignment(alignment);
      stringFormat.LineAlignment = TelerikAlignHelper.TranslateLineAlignment(alignment);
      this.DrawString(s, (RectangleF) rectangle, font, foreColor, stringFormat, shadow, textRendering, orientation, flipText, (CharacterRange[]) null, Color.Empty);
    }

    [Obsolete("Use the overload without ContentAlignment parameter. ContentAlignment should be provided through the StringFormat parameter.")]
    public virtual void DrawString(
      string s,
      RectangleF rectangle,
      Font font,
      Color foreColor,
      ContentAlignment alignment,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText)
    {
      stringFormat.Alignment = TelerikAlignHelper.TranslateAlignment(alignment);
      stringFormat.LineAlignment = TelerikAlignHelper.TranslateLineAlignment(alignment);
      this.DrawString(s, rectangle, font, foreColor, stringFormat, shadow, textRendering, orientation, flipText, (CharacterRange[]) null, Color.Empty);
    }
  }
}
