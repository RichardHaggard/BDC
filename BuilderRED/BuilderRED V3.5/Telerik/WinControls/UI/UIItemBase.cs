// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.UIItemBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public abstract class UIItemBase : RadItem, IPrimitiveElement, IShapedElement, IFillElement, IBorderElement, IBoxStyle, IBoxElement
  {
    internal const long UIItemBaseLastStateKey = 4398046511104;
    private FillPrimitiveImpl fillPrimitiveImpl;
    private BorderPrimitiveImpl borderPrimitiveImpl;

    internal FillPrimitiveImpl FillPrimitiveImpl
    {
      get
      {
        if (this.fillPrimitiveImpl == null)
          this.fillPrimitiveImpl = new FillPrimitiveImpl((IFillElement) this, (IPrimitiveElement) this);
        return this.fillPrimitiveImpl;
      }
    }

    internal BorderPrimitiveImpl BorderPrimitiveImpl
    {
      get
      {
        if (this.borderPrimitiveImpl == null)
          this.borderPrimitiveImpl = new BorderPrimitiveImpl((IBorderElement) this, (IPrimitiveElement) this);
        return this.borderPrimitiveImpl;
      }
    }

    protected virtual void PaintFill(IGraphics graphics, float angle, SizeF scale)
    {
      RectangleF fillPaintRect = this.GetFillPaintRect(angle, scale);
      this.PaintFill(graphics, angle, scale, fillPaintRect);
    }

    protected virtual RectangleF GetFillPaintRect(float angle, SizeF scale)
    {
      Size size = this.Size;
      return this.GetPatchedRect(new RectangleF(0.0f, 0.0f, (float) size.Width, (float) size.Height), angle, scale);
    }

    protected virtual void PaintFill(
      IGraphics graphics,
      float angle,
      SizeF scale,
      RectangleF prefferedRectangle)
    {
      this.FillPrimitiveImpl.PaintFill(graphics, angle, scale, prefferedRectangle);
    }

    protected virtual void PaintBorder(IGraphics graphics, float angle, SizeF scale)
    {
      RectangleF borderPaintRect = this.GetBorderPaintRect(angle, scale);
      this.PaintBorder(graphics, angle, scale, borderPaintRect);
    }

    protected virtual void PaintBorder(
      IGraphics graphics,
      float angle,
      SizeF scale,
      RectangleF preferedRectangle)
    {
      this.BorderPrimitiveImpl.PaintBorder(graphics, angle, scale, preferedRectangle);
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      this.FillPrimitiveImpl.OnBoundsChanged((Rectangle) e.OldValue);
      base.OnBoundsChanged(e);
    }

    protected virtual RectangleF GetBorderPaintRect(float angle, SizeF scale)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (this.DrawBorder)
      {
        float borderWidth = this.BorderWidth;
        int num3 = (int) borderWidth / 2;
        float num4 = borderWidth % 2f;
        num1 = (float) num3;
        num2 = (float) num3 + num4;
      }
      Size size = this.Size;
      return this.GetPatchedRect(new RectangleF(num1, num1, (float) size.Width - (num1 + num2), (float) size.Height - (num1 + num2)), angle, scale);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      this.FillPrimitiveImpl.InvalidateFillCache(e.Property);
      base.OnPropertyChanged(e);
    }

    protected virtual bool ShouldUsePaintBuffer()
    {
      return this.DrawFill;
    }

    public abstract bool DrawFill { get; set; }

    public abstract bool DrawBorder { get; set; }

    public virtual float GetPaintingBorderWidth()
    {
      if (this.Parent != null)
        return (float) this.Parent.BorderThickness.Left;
      return 0.0f;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
    }

    bool IPrimitiveElement.ShouldUsePaintBuffer()
    {
      return this.ShouldUsePaintBuffer();
    }

    bool IPrimitiveElement.IsDesignMode
    {
      get
      {
        return this.IsDesignMode;
      }
    }

    float IPrimitiveElement.BorderThickness
    {
      get
      {
        return this.GetPaintingBorderWidth();
      }
    }

    RectangleF IPrimitiveElement.GetPaintRectangle(
      float left,
      float angle,
      SizeF scale)
    {
      return this.GetPaintRectangle(left, angle, scale);
    }

    RectangleF IPrimitiveElement.GetExactPaintingRectangle(
      float angle,
      SizeF scale)
    {
      return this.GetPatchedRect(new RectangleF(0.0f, 0.0f, (float) (this.Size.Width - 1), (float) (this.Size.Height - 1)), angle, scale);
    }

    ElementShape IShapedElement.GetCurrentShape()
    {
      return this.GetCurrentShape();
    }

    public abstract Color BackColor2 { get; set; }

    public abstract Color BackColor3 { get; set; }

    public abstract Color BackColor4 { get; set; }

    public abstract int NumberOfColors { get; set; }

    public abstract float GradientAngle { get; set; }

    public abstract float GradientPercentage { get; set; }

    public abstract float GradientPercentage2 { get; set; }

    public abstract GradientStyles GradientStyle { get; set; }

    public abstract Color BorderColor { get; set; }

    public abstract Color BorderColor2 { get; set; }

    public abstract Color BorderColor3 { get; set; }

    public abstract Color BorderColor4 { get; set; }

    public abstract Color BorderInnerColor { get; set; }

    public abstract Color BorderInnerColor2 { get; set; }

    public abstract Color BorderInnerColor3 { get; set; }

    public abstract Color BorderInnerColor4 { get; set; }

    public abstract BorderBoxStyle BorderBoxStyle { get; set; }

    public abstract BorderDrawModes BorderDrawMode { get; set; }

    public abstract GradientStyles BorderGradientStyle { get; set; }

    public abstract float BorderGradientAngle { get; set; }

    public abstract Color BorderLeftColor { get; set; }

    public abstract Color BorderLeftShadowColor { get; set; }

    public abstract Color BorderTopColor { get; set; }

    public abstract Color BorderTopShadowColor { get; set; }

    public abstract Color BorderRightColor { get; set; }

    public abstract Color BorderRightShadowColor { get; set; }

    public abstract Color BorderBottomColor { get; set; }

    public abstract Color BorderBottomShadowColor { get; set; }

    public abstract float BorderLeftWidth { get; set; }

    public abstract float BorderTopWidth { get; set; }

    public abstract float BorderRightWidth { get; set; }

    public abstract float BorderBottomWidth { get; set; }

    public abstract DashStyle BorderDashStyle { get; set; }

    public abstract float[] BorderDashPattern { get; set; }

    Color IBorderElement.ForeColor
    {
      get
      {
        return this.BorderColor;
      }
    }

    Color IBorderElement.ForeColor2
    {
      get
      {
        return this.BorderColor2;
      }
    }

    Color IBorderElement.ForeColor3
    {
      get
      {
        return this.BorderColor3;
      }
    }

    Color IBorderElement.ForeColor4
    {
      get
      {
        return this.BorderColor4;
      }
    }

    Color IBorderElement.InnerColor
    {
      get
      {
        return this.BorderInnerColor;
      }
    }

    Color IBorderElement.InnerColor2
    {
      get
      {
        return this.BorderInnerColor2;
      }
    }

    Color IBorderElement.InnerColor3
    {
      get
      {
        return this.BorderInnerColor3;
      }
    }

    Color IBorderElement.InnerColor4
    {
      get
      {
        return this.BorderInnerColor4;
      }
    }

    BorderBoxStyle IBorderElement.BoxStyle
    {
      get
      {
        return this.BorderBoxStyle;
      }
    }

    GradientStyles IBorderElement.GradientStyle
    {
      get
      {
        return this.BorderGradientStyle;
      }
    }

    float IBorderElement.GradientAngle
    {
      get
      {
        return this.BorderGradientAngle;
      }
    }

    Color IBoxStyle.LeftColor
    {
      get
      {
        return this.BorderLeftColor;
      }
      set
      {
      }
    }

    Color IBoxStyle.LeftShadowColor
    {
      get
      {
        return this.BorderLeftShadowColor;
      }
      set
      {
      }
    }

    Color IBoxStyle.TopColor
    {
      get
      {
        return this.BorderTopColor;
      }
      set
      {
      }
    }

    Color IBoxStyle.TopShadowColor
    {
      get
      {
        return this.BorderTopShadowColor;
      }
      set
      {
      }
    }

    Color IBoxStyle.RightColor
    {
      get
      {
        return this.BorderRightColor;
      }
      set
      {
      }
    }

    Color IBoxStyle.RightShadowColor
    {
      get
      {
        return this.BorderRightShadowColor;
      }
      set
      {
      }
    }

    Color IBoxStyle.BottomColor
    {
      get
      {
        return this.BorderBottomColor;
      }
      set
      {
      }
    }

    Color IBoxStyle.BottomShadowColor
    {
      get
      {
        return this.BorderBottomShadowColor;
      }
      set
      {
      }
    }

    float IBoxElement.Width
    {
      get
      {
        return this.BorderWidth;
      }
      set
      {
      }
    }

    public abstract float BorderWidth { get; set; }

    float IBoxElement.LeftWidth
    {
      get
      {
        return this.BorderLeftWidth;
      }
      set
      {
      }
    }

    float IBoxElement.TopWidth
    {
      get
      {
        return this.BorderTopWidth;
      }
      set
      {
      }
    }

    float IBoxElement.RightWidth
    {
      get
      {
        return this.BorderRightWidth;
      }
      set
      {
      }
    }

    float IBoxElement.BottomWidth
    {
      get
      {
        return this.BorderBottomWidth;
      }
      set
      {
      }
    }

    SizeF IBoxElement.Offset
    {
      get
      {
        if (this.BorderBoxStyle == BorderBoxStyle.SingleBorder)
          return new SizeF(this.BorderWidth, this.BorderWidth);
        return new SizeF(this.BorderLeftWidth, this.BorderTopWidth);
      }
    }

    SizeF IBoxElement.BorderSize
    {
      get
      {
        IBoxElement boxElement = (IBoxElement) this;
        return new SizeF(boxElement.HorizontalWidth, boxElement.VerticalWidth);
      }
    }

    float IBoxElement.HorizontalWidth
    {
      get
      {
        if (this.BorderBoxStyle == BorderBoxStyle.SingleBorder)
          return 2f * this.BorderWidth;
        return this.BorderLeftWidth + this.BorderRightWidth;
      }
    }

    float IBoxElement.VerticalWidth
    {
      get
      {
        if (this.BorderBoxStyle == BorderBoxStyle.SingleBorder)
          return 2f * this.BorderWidth;
        return this.BorderTopWidth + this.BorderBottomWidth;
      }
    }
  }
}
