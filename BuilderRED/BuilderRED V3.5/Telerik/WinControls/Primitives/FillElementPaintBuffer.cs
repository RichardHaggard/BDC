// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.FillElementPaintBuffer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  internal class FillElementPaintBuffer
  {
    private int cachedPrimitiveHash = -1;
    private static Dictionary<int, object> CacheRelatedPropertyNames = new Dictionary<int, object>(30);
    private readonly IFillElement fillElement;
    private readonly FillRepository bitmapRepository;
    private Bitmap paintBuffer;
    private Graphics graphics;
    private int shapeHash;

    public bool IsDisabled
    {
      get
      {
        if (this.bitmapRepository != null)
          return this.bitmapRepository.DisableBitmapCache;
        return false;
      }
    }

    static FillElementPaintBuffer()
    {
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("Bounds".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("NumberOfColors".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("BackColor".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("BackColor2".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("BackColor3".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("BackColor4".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("GradientPercentage".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("GradientPercentage2".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("GradientStyle".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("GradientAngle".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("Shape".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("GradientAngleCorrection".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("AngleTransform".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("SmoothingMode".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("VisualState".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("FillPadding".GetHashCode(), (object) null);
      FillElementPaintBuffer.CacheRelatedPropertyNames.Add("ItemContentOrientation".GetHashCode(), (object) null);
    }

    public FillElementPaintBuffer(IFillElement fillElement, FillRepository fillRepository)
    {
      this.fillElement = fillElement;
      this.bitmapRepository = fillRepository;
      this.bitmapRepository.DisableBitmapCache = true;
    }

    public void InvalidateCachedPrimitiveHash(RadProperty property)
    {
      if (!FillElementPaintBuffer.CacheRelatedPropertyNames.ContainsKey(property.NameHash))
        return;
      this.cachedPrimitiveHash = -1;
    }

    public void InvalidateCachedPrimitiveHash()
    {
      this.cachedPrimitiveHash = -1;
    }

    public virtual bool ShouldUsePaintBuffer()
    {
      int numberOfColors = this.fillElement.NumberOfColors;
      if (this.fillElement.GradientStyle != GradientStyles.Solid)
      {
        switch (numberOfColors)
        {
          case 0:
          case 1:
            goto label_3;
          case 2:
            if (!(this.fillElement.BackColor == this.fillElement.BackColor2))
              break;
            goto label_3;
        }
        return true;
      }
label_3:
      return false;
    }

    public bool PaintFromBuffer(IGraphics g, SizeF scale, Size desired)
    {
      Size scaled = this.GetScaled(desired, scale);
      int primitiveHash = this.GetPrimitiveHash(desired);
      if (this.paintBuffer != null && this.bitmapRepository.DisableBitmapCache)
        this.paintBuffer.Dispose();
      this.paintBuffer = this.bitmapRepository.GetBitmapBySizeAndHash(scaled, primitiveHash);
      if (this.paintBuffer == null)
      {
        if (scaled.Width == 0 || scaled.Height == 0)
          return true;
        this.paintBuffer = new Bitmap(scaled.Width, scaled.Height);
        this.bitmapRepository.AddNewBitmap(scaled, primitiveHash, this.paintBuffer);
        return false;
      }
      Graphics underlayGraphics = g.UnderlayGraphics as Graphics;
      Matrix matrix = (Matrix) null;
      if ((double) scale.Width != 1.0 || (double) scale.Height != 1.0)
      {
        matrix = underlayGraphics.Transform;
        underlayGraphics.ScaleTransform(1f / scale.Width, 1f / scale.Height);
      }
      g.DrawBitmap((Image) this.paintBuffer, 0, 0);
      if (matrix != null)
        underlayGraphics.Transform = matrix;
      return true;
    }

    public void SetGraphics(IGraphics g, SizeF scale)
    {
      RadGdiGraphics radGdiGraphics = (RadGdiGraphics) g;
      this.graphics = radGdiGraphics.Graphics;
      Graphics graphics = Graphics.FromImage((Image) this.paintBuffer);
      graphics.ScaleTransform(scale.Width, scale.Height);
      graphics.SmoothingMode = radGdiGraphics.Graphics.SmoothingMode;
      ((RadGdiGraphics) g).Graphics = graphics;
    }

    public void ResetGraphics(IGraphics g, SizeF scale)
    {
      if (this.graphics == null)
        return;
      ((RadGdiGraphics) g).Graphics.Dispose();
      ((RadGdiGraphics) g).Graphics = this.graphics;
      Matrix matrix = (Matrix) null;
      if ((double) scale.Width != 1.0 || (double) scale.Height != 1.0)
      {
        matrix = ((RadGdiGraphics) g).Graphics.Transform;
        ((RadGdiGraphics) g).Graphics.ScaleTransform(1f / scale.Width, 1f / scale.Height);
      }
      g.DrawBitmap((Image) this.paintBuffer, 0, 0);
      if (matrix == null)
        return;
      ((RadGdiGraphics) g).Graphics.Transform = matrix;
    }

    public void RemoveBitmapsBySize(Size size, SizeF scale)
    {
      this.bitmapRepository.RemoveBitmapsBySize(this.GetScaled(size, scale));
    }

    private int GetPrimitiveHash(Size desired)
    {
      if (this.cachedPrimitiveHash != -1)
        return this.cachedPrimitiveHash;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(desired.Width);
      stringBuilder.Append(";");
      stringBuilder.Append(desired.Height);
      stringBuilder.Append(";");
      stringBuilder.Append(this.fillElement.NumberOfColors);
      stringBuilder.Append(";");
      stringBuilder.Append(this.fillElement.BackColor.ToArgb().ToString());
      stringBuilder.Append(this.fillElement.BackColor2.ToArgb().ToString());
      stringBuilder.Append(this.fillElement.BackColor3.ToArgb().ToString());
      stringBuilder.Append(this.fillElement.BackColor4.ToArgb().ToString());
      stringBuilder.Append(";");
      stringBuilder.Append(this.fillElement.GradientPercentage);
      stringBuilder.Append(";");
      stringBuilder.Append(this.fillElement.GradientPercentage2);
      stringBuilder.Append(";");
      stringBuilder.Append(this.GetGradientStyleAsString());
      stringBuilder.Append(";");
      stringBuilder.Append(this.fillElement.GradientAngle);
      stringBuilder.Append(this.shapeHash);
      this.cachedPrimitiveHash = stringBuilder.ToString().GetHashCode();
      return this.cachedPrimitiveHash;
    }

    private string GetGradientStyleAsString()
    {
      return Enum.GetName(typeof (GradientStyles), (object) this.fillElement.GradientStyle) ?? string.Empty;
    }

    private Size GetScaled(Size original, SizeF scale)
    {
      return new Size((int) ((double) original.Width * (double) scale.Width), (int) ((double) original.Height * (double) scale.Height));
    }

    public void SetShapeHash(int shapeHash)
    {
      if (this.shapeHash == shapeHash)
        return;
      this.shapeHash = shapeHash;
      this.InvalidateCachedPrimitiveHash();
    }
  }
}
