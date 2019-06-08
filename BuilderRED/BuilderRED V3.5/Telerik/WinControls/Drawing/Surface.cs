// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.Surface
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.Drawing
{
  public abstract class Surface
  {
    private float opacity;
    private readonly object deviceContext;
    private readonly Dictionary<string, object> resources;

    public Surface()
      : this((object) null)
    {
    }

    public Surface(object deviceContext)
    {
      this.opacity = 1f;
      this.resources = new Dictionary<string, object>(4);
      this.deviceContext = deviceContext;
    }

    public float Opacity
    {
      get
      {
        return this.opacity;
      }
      set
      {
        this.opacity = value;
      }
    }

    public object DeviceContext
    {
      get
      {
        return this.deviceContext;
      }
    }

    public Dictionary<string, object> Resources
    {
      get
      {
        return this.resources;
      }
    }

    protected abstract Graphics Graphics { get; }

    public abstract Path CreatePath();

    public abstract RadBrush CreateSolidBrush(Color color);

    public abstract RadBrush CreateRadialBrush(
      PointF center,
      float xRadius,
      float yRadius,
      GradientStop[] colorStops);

    public abstract RadBrush CreateLinearGradientBrush(
      RectangleF rectangle,
      GradientStop[] colorStops,
      float angle);

    public abstract RadBrush CreateLinearGradientBrush(
      RectangleF rectangle,
      GradientStop[] colorStops,
      RadLinearGradientMode mode);

    public abstract RoundedRectangle CreateRoundedRectangle(
      RectangleF rect,
      float radius);

    public virtual RadMorphologyEffect CreateMorphologyEffect()
    {
      return RadMorphologyEffect.Empty;
    }

    public virtual RadGaussianBlurEffect CreateGaussianBlurEffect()
    {
      return RadGaussianBlurEffect.Empty;
    }

    public virtual RadDisplacementMapEffect CreateDisplacementMapEffect()
    {
      return RadDisplacementMapEffect.Empty;
    }

    public abstract void BeginDraw(params object[] resources);

    public abstract void EndDraw();

    public abstract SizeF MeasureText(
      string text,
      Font font,
      SizeF availableSize,
      TextFormat textFormat);

    public abstract void DrawText(
      string text,
      Font font,
      RadBrush brush,
      RectangleF rect,
      TextFormat textFormat);

    public abstract void DrawLine(RadBrush brush, float x1, float y1, float x2, float y2);

    public abstract void DrawRectangle(RadBrush brush, float x1, float y1, float x2, float y2);

    public abstract void DrawImage(Image image, RectangleF rect);

    public abstract void FillRectangle(
      RadBrush brush,
      float x,
      float y,
      float width,
      float height);

    public abstract void FillPolygon(RadBrush brush, PointF[] points);

    public abstract SizeF MeasureText(ITextElement element, SizeF availableSize);

    public abstract void DrawText(ITextElement element, RadBrush brush, RectangleF rect);

    public abstract void DrawBorder(IBorderElement element, RectangleF rect);

    public abstract void DrawBorder(IBorderElement element, Path path);

    public abstract void DrawImage(IImageElement element, RectangleF rect);

    public virtual void BeginEffects(Size viewportSize)
    {
    }

    public virtual void EndEffects(EffectCollection effects)
    {
    }

    protected abstract void DrawPathCore(Path path, RadBrush brush, float width);

    protected abstract void FillPathCore(Path path, RadBrush brush);

    protected abstract void FillRoundedRectangleCore(
      RoundedRectangle roundedRectangle,
      RadBrush brush);

    protected void FillEllipse(RectangleF rectangle, RadBrush brush)
    {
      using (Path path = this.CreatePath())
      {
        path.AddEllipse(rectangle);
        this.FillPathCore(path, brush);
      }
    }

    protected void FillRectangle(RectangleF rectangle, RadBrush brush)
    {
      using (Path path = this.CreatePath())
      {
        path.AddRectangle(rectangle);
        this.FillPathCore(path, brush);
      }
    }

    protected void DrawRectangle(RectangleF rectangle, RadBrush brush, float width)
    {
      using (Path path = this.CreatePath())
      {
        path.AddRectangle(rectangle);
        this.DrawPathCore(path, brush, width);
      }
    }

    protected void DrawEllipse(RectangleF rectangle, RadBrush brush, float width)
    {
      using (Path path = this.CreatePath())
      {
        path.AddEllipse(rectangle);
        this.DrawPathCore(path, brush, width);
      }
    }

    public void FillRectangle(IFillElement element, RectangleF rect)
    {
      switch (element.GradientStyle)
      {
        case GradientStyles.Glass:
        case GradientStyles.OfficeGlass:
        case GradientStyles.OfficeGlassRect:
        case GradientStyles.Gel:
        case GradientStyles.Vista:
          rect = (RectangleF) Rectangle.Round(rect);
          break;
      }
      using (Path path = this.CreatePath())
      {
        path.AddRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        this.FillPath(element, path);
      }
    }

    public void FillPath(IFillElement element, Path path)
    {
      GradientStyles gradientStyle = element.GradientStyle;
      int numberOfColors = element.NumberOfColors;
      float gradientPercentage = element.GradientPercentage;
      float gradientPercentage2 = element.GradientPercentage2;
      if (gradientStyle == GradientStyles.Gel || gradientStyle == GradientStyles.Radial || gradientStyle == GradientStyles.Linear)
      {
        GradientStop[] colorStops = this.GetColorStops(element);
        switch (gradientStyle)
        {
          case GradientStyles.Linear:
          case GradientStyles.Radial:
            Color color1 = this.GetColor(element.BackColor);
            Color color2 = this.GetColor(element.BackColor2);
            if (numberOfColors < 2 || numberOfColors == 2 && color1 == color2)
            {
              this.FillSolid(path, color1);
              break;
            }
            if (gradientStyle == GradientStyles.Linear)
            {
              this.FillLinear(path, colorStops, element.GradientAngle);
              break;
            }
            this.FillRadial(path, colorStops);
            break;
          case GradientStyles.Gel:
            this.FillGel(path, colorStops, gradientPercentage, gradientPercentage2);
            break;
        }
      }
      else
      {
        Color color1 = this.GetColor(element.BackColor);
        Color color2 = this.GetColor(element.BackColor2);
        Color color3 = this.GetColor(element.BackColor3);
        Color color4 = this.GetColor(element.BackColor4);
        switch (gradientStyle)
        {
          case GradientStyles.Solid:
            this.FillSolid(path, color1);
            break;
          case GradientStyles.Glass:
            this.FillGlass(path, color1, color2, color3, color4, gradientPercentage, gradientPercentage2);
            break;
          case GradientStyles.OfficeGlass:
          case GradientStyles.OfficeGlassRect:
            this.FillOfficeGlass(path, color1, color2, color3, color4, gradientPercentage, gradientPercentage2, gradientStyle == GradientStyles.OfficeGlass);
            break;
          case GradientStyles.Vista:
            this.FillVista(path, color1, color2, color3, color4, gradientPercentage, gradientPercentage2);
            break;
        }
      }
    }

    private void FillSolid(Path path, Color color)
    {
      using (RadBrush solidBrush = this.CreateSolidBrush(color))
        this.FillPathCore(path, solidBrush);
    }

    private void FillRadial(Path path, GradientStop[] colorStops)
    {
    }

    private void FillLinear(Path path, GradientStop[] colorStops, float gradientAngle)
    {
      using (RadBrush linearGradientBrush = this.CreateLinearGradientBrush(path.GetBounds(), colorStops, gradientAngle))
        this.FillPathCore(path, linearGradientBrush);
    }

    private void FillGel(
      Path path,
      GradientStop[] colorStops,
      float gradientPercentage,
      float gradientPercentage2)
    {
      if (colorStops.Length < 2)
        this.FillSolid(path, colorStops[0].Color);
      else
        this.FillLinear(path, colorStops, 90f);
      RectangleF bounds = path.GetBounds();
      float num1 = bounds.Height / 2f;
      float num2 = (float) (int) Math.Round(100.0 * (double) gradientPercentage * 0.2);
      float num3 = (float) (int) Math.Round((double) num1 * (double) gradientPercentage2 * 0.2);
      RectangleF rect = new RectangleF(bounds.X + num2, bounds.Y, (float) ((double) bounds.Width - (double) num2 * 2.0 - 1.0), num1 - num3 * 2f);
      if (this.IsInvalidRectangle(rect))
        return;
      using (RoundedRectangle roundedRectangle = this.CreateRoundedRectangle(rect, rect.Height / 2f))
      {
        RectangleF rectangle = RectangleF.Inflate(rect, 1f, 1f);
        Color color = this.GetColor(Color.White);
        GradientStop gradientStop1 = new GradientStop(this.ReduceAlphaBasedOnOriginal(253, color), 0.0f);
        GradientStop gradientStop2 = new GradientStop(this.ReduceAlphaBasedOnOriginal(42, color), 1f);
        using (RadBrush linearGradientBrush = this.CreateLinearGradientBrush(rectangle, new GradientStop[2]{ gradientStop1, gradientStop2 }, RadLinearGradientMode.Vertical))
          this.FillRoundedRectangleCore(roundedRectangle, linearGradientBrush);
      }
    }

    protected virtual void FillOfficeGlass(
      Path inputPath,
      Color color1,
      Color color2,
      Color color3,
      Color color4,
      float gradientPercentage,
      float gradientPercentage2,
      bool fillEllipse)
    {
      RectangleF bounds = inputPath.GetBounds();
      RectangleF rectangleF = new RectangleF(bounds.X + 1f, bounds.Y + 1f, bounds.Width - 2f, bounds.Height - 2f);
      int topHeight = (int) Math.Round((double) rectangleF.Height - (double) rectangleF.Height / 2.0 * (double) gradientPercentage2);
      this.FillOfficeGlassMain(color3, rectangleF, fillEllipse);
      Graphics graphics = this.Graphics;
      if (!this.FillOfficeGlassBottomGlow(graphics, color3, color4, rectangleF, topHeight, fillEllipse))
        return;
      this.FillOfficeGlassTopLightGlow(graphics, color1, color2, rectangleF, gradientPercentage, fillEllipse);
      if (!this.FillOfficeGlassTopInnerGlow(graphics, color2, color3, rectangleF, fillEllipse))
        return;
      int borderThicness = Math.Max(1, (int) ((double) rectangleF.Width * 0.02));
      this.FillOfficeGlassTopInnerBorder(rectangleF, color1, color4, borderThicness, fillEllipse);
      this.FillOfficeGlassOuterBorder(rectangleF, color3, borderThicness, fillEllipse);
    }

    protected virtual void FillOfficeGlassOuterBorder(
      RectangleF originalRectangle,
      Color color3,
      int borderThicness,
      bool fillEllipse)
    {
      GradientStop gradientStop1 = new GradientStop(this.ReduceAlphaBasedOnOriginal(100, color3), 0.0f);
      GradientStop gradientStop2 = new GradientStop(color3, 1f);
      using (RadBrush linearGradientBrush = this.CreateLinearGradientBrush(originalRectangle, new GradientStop[2]{ gradientStop1, gradientStop2 }, RadLinearGradientMode.Vertical))
      {
        if (fillEllipse)
          this.DrawEllipse(originalRectangle, linearGradientBrush, (float) borderThicness);
        else
          this.DrawRectangle(originalRectangle, linearGradientBrush, (float) borderThicness);
      }
    }

    protected virtual void FillOfficeGlassTopInnerBorder(
      RectangleF originalRectangle,
      Color color1,
      Color color4,
      int borderThicness,
      bool fillEllipse)
    {
      RectangleF rectangleF = RectangleF.Inflate(originalRectangle, (float) -borderThicness, (float) -borderThicness);
      if (this.IsInvalidRectangle(rectangleF))
        return;
      Color color2 = this.GetColor(this.ReduceAlphaBasedOnOriginal(50, color1));
      Color color3 = this.GetColor(this.ReduceAlphaBasedOnOriginal(50, color4));
      GradientStop gradientStop1 = new GradientStop(color2, 0.0f);
      GradientStop gradientStop2 = new GradientStop(color3, 1f);
      using (RadBrush linearGradientBrush = this.CreateLinearGradientBrush(rectangleF, new GradientStop[2]{ gradientStop1, gradientStop2 }, RadLinearGradientMode.Vertical))
      {
        if (fillEllipse)
          this.DrawEllipse(rectangleF, linearGradientBrush, (float) borderThicness);
        else
          this.DrawRectangle(rectangleF, linearGradientBrush, (float) borderThicness);
      }
    }

    protected virtual bool FillOfficeGlassTopInnerGlow(
      Graphics graphics,
      Color color2,
      Color color3,
      RectangleF originalRectangle,
      bool fillEllipse)
    {
      for (int index = 0; index < 3; ++index)
      {
        using (GraphicsPath path = new GraphicsPath())
        {
          RectangleF rect = RectangleF.Inflate(originalRectangle, 0.0f, 0.0f);
          rect.Y += (float) (int) ((double) originalRectangle.Height * 0.08);
          rect.Height = (float) (int) ((double) originalRectangle.Height * 0.77);
          rect.Width = (float) (int) ((double) rect.Width * 0.8);
          rect.X = (float) ((double) originalRectangle.Width / 2.0 - (double) rect.Width / 2.0) + rect.X;
          if (this.IsInvalidRectangle(rect))
            return false;
          path.AddEllipse(rect);
          using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
          {
            pathGradientBrush.SurroundColors = new Color[1]
            {
              this.GetColor(this.ReduceAlphaBasedOnOriginal(0, color2))
            };
            pathGradientBrush.CenterColor = this.GetColor(this.ReduceAlphaBasedOnOriginal(130, color3));
            if (fillEllipse)
              graphics.FillEllipse((Brush) pathGradientBrush, originalRectangle);
            else
              graphics.FillRectangle((Brush) pathGradientBrush, originalRectangle);
          }
        }
      }
      return true;
    }

    protected virtual void FillOfficeGlassTopLightGlow(
      Graphics graphics,
      Color color1,
      Color color2,
      RectangleF originalRectangle,
      float gradientPercentage,
      bool fillEllipse)
    {
      using (GraphicsPath path = new GraphicsPath())
      {
        RectangleF rect = RectangleF.Inflate(originalRectangle, 0.0f, 0.0f);
        rect.Y = rect.Y - rect.Height / 2f - (float) (int) ((double) rect.Height * 0.02);
        rect.Width = (float) (int) ((double) rect.Width * (1.2 + (double) gradientPercentage));
        rect.X = (float) ((double) originalRectangle.Width / 2.0 - (double) rect.Width / 2.0) + rect.X;
        path.AddEllipse(rect);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
        {
          pathGradientBrush.SurroundColors = new Color[1]
          {
            this.ReduceAlphaBasedOnOriginal(5, color2)
          };
          pathGradientBrush.CenterPoint = new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height * 0.5f);
          pathGradientBrush.CenterColor = this.ReduceAlphaBasedOnOriginal(200, color1);
          if (fillEllipse)
            graphics.FillEllipse((Brush) pathGradientBrush, originalRectangle);
          else
            graphics.FillRectangle((Brush) pathGradientBrush, originalRectangle);
        }
      }
    }

    protected virtual bool FillOfficeGlassBottomGlow(
      Graphics graphics,
      Color color3,
      Color color4,
      RectangleF originalRectangle,
      int topHeight,
      bool fillEllipse)
    {
      RectangleF rect = originalRectangle;
      rect.Y += (float) (topHeight - (int) ((double) rect.Height * 0.1));
      rect.Width = (float) (int) ((double) rect.Width * 1.5);
      rect.X = (float) ((double) originalRectangle.Width / 2.0 - (double) rect.Width / 2.0) + rect.X;
      if (this.IsInvalidRectangle(rect))
        return false;
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(rect);
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
        {
          pathGradientBrush.SurroundColors = new Color[1]
          {
            this.ReduceAlphaBasedOnOriginal(100, color3)
          };
          pathGradientBrush.CenterColor = this.ReduceAlphaBasedOnOriginal(220, color4);
          for (int index = 0; index < 2; ++index)
          {
            if (fillEllipse)
              graphics.FillEllipse((Brush) pathGradientBrush, originalRectangle);
            else
              graphics.FillRectangle((Brush) pathGradientBrush, originalRectangle);
          }
        }
      }
      return true;
    }

    protected virtual void FillOfficeGlassMain(
      Color color3,
      RectangleF innerRectangle,
      bool fillEllipse)
    {
      using (RadBrush solidBrush = this.CreateSolidBrush(color3))
      {
        if (fillEllipse)
          this.FillEllipse(innerRectangle, solidBrush);
        else
          this.FillRectangle(innerRectangle, solidBrush);
      }
    }

    private void FillGlass(
      Path path,
      Color backColor,
      Color backColor2,
      Color backColor3,
      Color backColor4,
      float gradientPercentage,
      float gradientPercentage2)
    {
    }

    private void FillVista(
      Path path,
      Color backColor,
      Color backColor2,
      Color backColor3,
      Color backColor4,
      float gradientPercentage,
      float gradientPercentage2)
    {
    }

    private bool IsInvalidRectangle(RectangleF rect)
    {
      int width = (int) rect.Width;
      int height = (int) rect.Height;
      return width <= 0 || height <= 0;
    }

    private GradientStop[] GetColorStops(IFillElement element)
    {
      float gradientPercentage = element.GradientPercentage;
      float gradientPercentage2 = element.GradientPercentage2;
      GradientStyles gradientStyle = element.GradientStyle;
      Color color1 = this.GetColor(element.BackColor);
      Color color2 = this.GetColor(element.BackColor2);
      Color color3 = this.GetColor(element.BackColor3);
      Color color4 = this.GetColor(element.BackColor4);
      int numberOfColors = element.NumberOfColors;
      int colorCount = Math.Min(Math.Max(numberOfColors, 1), 4);
      GradientStop[] colorStops = new GradientStop[colorCount];
      for (int index = 0; index < colorCount; ++index)
        colorStops[index] = new GradientStop();
      if (gradientStyle == GradientStyles.Gel)
      {
        gradientPercentage = 0.0f;
        gradientPercentage2 = 1f;
      }
      if (numberOfColors > 0)
      {
        colorStops[0].Color = color1;
        colorStops[0].Position = 0.0f;
        if (numberOfColors > 1)
        {
          colorStops[1].Color = color2;
          colorStops[colorCount - 1].Position = 1f;
          if (numberOfColors > 2)
          {
            colorStops[2].Color = color3;
            colorStops[1].Position = gradientPercentage;
            if (numberOfColors > 3)
            {
              colorStops[3].Color = this.GetColor(color4);
              colorStops[2].Position = gradientPercentage2;
            }
          }
        }
      }
      switch (gradientStyle)
      {
        case GradientStyles.Radial:
          colorStops = this.GetFillRadialGradientStops(colorStops, colorCount, gradientPercentage, gradientPercentage2);
          break;
        case GradientStyles.Gel:
          colorStops = this.GetFillGelGradientStops(colorStops, colorCount);
          break;
      }
      return colorStops;
    }

    protected virtual GradientStop[] GetFillGelGradientStops(
      GradientStop[] colorStops,
      int colorCount)
    {
      if (colorCount > 3)
      {
        colorStops[0].Position = 0.0f;
        colorStops[1].Position = 0.333f;
        colorStops[2].Position = 0.666f;
        colorStops[3].Position = 1f;
      }
      else if (colorCount > 2)
      {
        colorStops[0].Position = 0.0f;
        colorStops[1].Position = 0.5f;
        colorStops[2].Position = 1f;
      }
      else if (colorCount == 2)
      {
        colorStops[0].Position = 0.0f;
        colorStops[1].Position = 1f;
      }
      return colorStops;
    }

    protected virtual GradientStop[] GetFillRadialGradientStops(
      GradientStop[] colorStops,
      int colorCount,
      float gradientPercentage,
      float gradientPercentage2)
    {
      switch (colorCount)
      {
        case 3:
          colorStops[0].Position = 0.0f;
          colorStops[1].Position = gradientPercentage;
          colorStops[2].Position = 1f;
          break;
        case 4:
          colorStops[0].Position = 0.0f;
          colorStops[1].Position = gradientPercentage;
          colorStops[2].Position = gradientPercentage2;
          colorStops[3].Position = 1f;
          break;
      }
      return colorStops;
    }

    protected Color ReduceAlphaBasedOnOriginal(int newAlpha, Color color)
    {
      return Color.FromArgb((int) ((double) newAlpha * ((double) color.A / (double) byte.MaxValue)), color);
    }

    protected Color GetColor(Color original)
    {
      if ((double) this.opacity == 1.0)
        return original;
      return Color.FromArgb(Math.Min((int) byte.MaxValue, Math.Max(0, (int) ((double) original.A * (double) this.opacity))), original);
    }
  }
}
