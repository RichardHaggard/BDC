// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Paint.IGraphics
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.Paint
{
  public interface IGraphics
  {
    Rectangle ClipRectangle { get; }

    object UnderlayGraphics { get; }

    object SaveState();

    void RestoreState(object state);

    double Opacity { get; }

    void ChangeOpacity(double opacity);

    void RestoreOpacity();

    void ChangeSmoothingMode(SmoothingMode smoothingMode);

    void RestoreSmoothingMode();

    void DrawRectangle(Rectangle rectangle, Color color);

    void DrawRectangle(
      Rectangle rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth);

    void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth);

    void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle);

    void DrawRectangle(
      RectangleF rectangle,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern);

    void ExcludeClip(Rectangle rectangle);

    void FillPath(Color color, GraphicsPath path);

    void FillPath(
      Color[] colorStops,
      float[] colorOffsets,
      float angle,
      float gradientPercentage,
      float gradientPercentage2,
      Rectangle rectangle,
      GraphicsPath path);

    void DrawBlurShadow(GraphicsPath path, Rectangle r, float offset, Color color);

    void DrawLinearGradientRectangle(
      RectangleF rectangle,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle);

    void DrawLinearGradientRectangle(
      RectangleF rectangle,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle);

    void DrawLinearGradientRectangle(
      RectangleF rectangle,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle,
      float[] dashPattern);

    void DrawRadialGradientRectangle(
      Rectangle rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth);

    void DrawRadialGradientRectangle(
      RectangleF rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth);

    void DrawRadialGradientRectangle(
      RectangleF rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle);

    void DrawRadialGradientRectangle(
      RectangleF rectangle,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern);

    void DrawCustomGradientRectangle(
      Rectangle rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth);

    void DrawCustomGradientRectangle(
      RectangleF rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth);

    void DrawCustomGradientRectangle(
      RectangleF rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle);

    void DrawCustomGradientRectangle(
      RectangleF rectangle,
      GraphicsPath path,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern);

    void DrawEllipse(Rectangle BorderRectangle, Color color);

    void DrawString(
      string drawString,
      Rectangle rectangle,
      Font font,
      Color color,
      StringFormat stringFormat,
      Orientation orientation,
      bool flipText);

    void DrawString(
      string drawString,
      RectangleF rectangle,
      Font font,
      Color color,
      StringFormat stringFormat,
      Orientation orientation,
      bool flipText);

    void DrawString(TextParams textParams, SizeF measuredSize);

    void DrawString(
      string drawString,
      RectangleF rectangle,
      Font font,
      Color color,
      ContentAlignment alignment,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText);

    [Obsolete("Use the overload without ContentAlignment parameter. ContentAlignment should be provided through the StringFormat parameter.")]
    void DrawString(
      string drawString,
      Rectangle rectangle,
      Font font,
      Color color,
      ContentAlignment alignment,
      StringFormat stringFormat,
      ShadowSettings shadow,
      TextRenderingHint textRendering,
      Orientation orientation,
      bool flipText);

    void DrawImage(Rectangle rectangle, Image image, ContentAlignment alignment, bool enabled);

    void DrawImage(Point point, Image image, bool enabled);

    void DrawBitmap(Image image, int x, int y);

    void DrawBitmap(Image image, int x, int y, double opacity);

    void DrawBitmap(Image image, int x, int y, int width, int height);

    void DrawBitmap(Image image, int x, int y, int width, int height, double opacity);

    void DrawPath(GraphicsPath path, Color color, PenAlignment penAlignment, float penWidth);

    void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle);

    void DrawPath(
      GraphicsPath path,
      Color color,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPattern);

    void DrawLinearGradientPath(
      GraphicsPath path,
      RectangleF bounds,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle);

    void DrawLinearGradientPath(
      GraphicsPath path,
      RectangleF bounds,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle);

    void DrawLinearGradientPath(
      GraphicsPath path,
      RectangleF bounds,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      float angle,
      DashStyle dashStyle,
      float[] dashPAttern);

    void DrawLine(Color color, int x1, int y1, int x2, int y2);

    void DrawLine(Color color, float x1, float y1, float x2, float y2);

    void DrawLine(Color color, float x1, float y1, float x2, float y2, float width);

    void DrawLine(Color color, DashStyle dashStyle, int x1, int y1, int x2, int y2);

    void DrawRadialGradientPath(
      GraphicsPath path,
      Rectangle bounds,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth);

    void DrawRadialGradientPath(
      GraphicsPath path,
      Rectangle bounds,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle);

    void DrawRadialGradientPath(
      GraphicsPath path,
      Rectangle bounds,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth,
      DashStyle dashStyle,
      float[] dashPAttern);

    void DrawCustomGradientPath(
      GraphicsPath path,
      GraphicsPath gradientPath,
      Color color,
      Color[] gradientColors,
      PenAlignment penAlignment,
      float penWidth);

    Bitmap CreateBitmapMask(Color maskColor, Bitmap bitmap);

    void FillRectangle(Rectangle BorderRectangle, Color color);

    void FillRectangle(RectangleF BorderRectangle, Color color);

    void FillTextureRectangle(Rectangle rectangle, Image texture);

    void FillTextureRectangle(Rectangle rectangle, Image texture, WrapMode wrapMode);

    void FillTextureRectangle(RectangleF rectangle, Image texture);

    void FillTextureRectangle(RectangleF rectangle, Image texture, WrapMode wrapMode);

    void FillGradientRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      GradientStyles style,
      float angle);

    void FillGradientRectangle(
      Rectangle rectangle,
      Color[] colorStops,
      float[] colorOffsets,
      GradientStyles style,
      float angle,
      float gradientPercentage,
      float gradientPercentage2);

    void FillGradientRectangle(
      RectangleF rectangle,
      Color[] colorStops,
      float[] colorOffsets,
      GradientStyles style,
      float angle,
      float gradientPercentage,
      float gradientPercentage2);

    void FillGradientRectangle(Rectangle rectangle, Color color1, Color color2, float angle);

    void FillGlassRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2);

    void FillOfficeGlassRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2,
      bool drawEllipse);

    void FillVistaRectangle(
      Rectangle rectangle,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2);

    void FillGellRectangle(
      Rectangle rectangle,
      Color[] colorStops,
      float gradientPercentage,
      float gradientPercentage2);

    void FillPolygon(Color color, Point[] points);

    void FillPolygon(Color color, PointF[] points);

    void FillPolygon(Brush brush, PointF[] points);

    void DrawRoundRect(Rectangle rectangle, Color color, float borderWidth, int radius);

    void TranslateTransform(int offsetX, int offsetY);

    void TranslateTransform(float offsetX, float offsetY);

    void RotateTransform(float angleInDegrees);

    void ResetTransform();

    void ScaleTransform(SizeF scale);

    void PushCurrentClippingPath(GraphicsPath path);

    GraphicsPath PopCurrentClippingPath();

    SizeF MeasureString(string text, Font font, StringFormat stringFormat);

    void DrawBorder(RectangleF rectangle, IBorderElement borderElement);
  }
}
