// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewElementBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class RadPageViewElementBase : LightVisualElement
  {
    public static RadProperty BorderPaddingProperty = RadProperty.Register(nameof (BorderPadding), typeof (Padding), typeof (RadPageViewElementBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FillPaddingProperty = RadProperty.Register(nameof (FillPadding), typeof (Padding), typeof (RadPageViewElementBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.AffectsDisplay));
    private PageViewContentOrientation contentOrientation;
    private PageViewContentOrientation borderAndFillOrientation;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.CaptureOnMouseDown = true;
      this.borderAndFillOrientation = PageViewContentOrientation.Horizontal;
      this.contentOrientation = PageViewContentOrientation.Horizontal;
      this.ImageLayout = ImageLayout.None;
      this.ClipDrawing = true;
      this.BypassLayoutPolicies = true;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PageViewContentOrientation ContentOrientation
    {
      get
      {
        return this.contentOrientation;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public PageViewContentOrientation BorderAndFillOrientation
    {
      get
      {
        return this.borderAndFillOrientation;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the padding that defines the offset of element's fill.. This does not affect element's layout logic such as size and location but has only appearance impact.")]
    public Padding FillPadding
    {
      get
      {
        return TelerikDpiHelper.ScalePadding((Padding) this.GetValue(RadPageViewElementBase.FillPaddingProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElementBase.FillPaddingProperty, (object) value);
      }
    }

    [Description("Gets or sets the padding that defines the offset of the border. This does not affect element's layout logic such as size and location but has only appearance impact.")]
    [Category("Appearance")]
    public Padding BorderPadding
    {
      get
      {
        return TelerikDpiHelper.ScalePadding((Padding) this.GetValue(RadPageViewElementBase.BorderPaddingProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElementBase.BorderPaddingProperty, (object) value);
      }
    }

    protected override PointF CalcLayoutOffset(PointF startPoint)
    {
      Point location = this.Location;
      startPoint.X += (float) location.X;
      startPoint.Y += (float) location.Y;
      return startPoint;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF size = this.GetClientRectangle(availableSize).Size;
      SizeF childSize = this.MeasureChildren(size);
      SizeF sizeF = this.ApplyMinMaxSize(this.ApplyClientOffset(this.CalculateMeasuredSize(this.MeasureContent(size), childSize)));
      switch (this.contentOrientation)
      {
        case PageViewContentOrientation.Vertical90:
        case PageViewContentOrientation.Vertical270:
          sizeF = new SizeF(sizeF.Height, sizeF.Width);
          break;
      }
      return sizeF;
    }

    protected virtual SizeF CalculateMeasuredSize(SizeF contentSize, SizeF childSize)
    {
      SizeF sizeF = contentSize;
      if ((double) childSize.Width > (double) sizeF.Width)
        sizeF.Width = childSize.Width;
      if ((double) childSize.Height > (double) sizeF.Height)
        sizeF.Height = childSize.Height;
      return sizeF;
    }

    protected virtual SizeF MeasureContent(SizeF availableSize)
    {
      if (this.contentOrientation == PageViewContentOrientation.Vertical270 || this.contentOrientation == PageViewContentOrientation.Vertical90)
        availableSize = new SizeF(availableSize.Height, availableSize.Width);
      return this.Layout.Measure(availableSize);
    }

    protected SizeF GetLightVisualElementSize(SizeF avaliable)
    {
      return base.MeasureOverride(avaliable);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.ArrangeChildren(finalSize);
      this.ArrangeContent(finalSize);
      return finalSize;
    }

    protected virtual void ArrangeContent(SizeF finalSize)
    {
      if (this.contentOrientation == PageViewContentOrientation.Vertical270 || this.contentOrientation == PageViewContentOrientation.Vertical90)
        finalSize = new SizeF(finalSize.Height, finalSize.Width);
      this.Layout.Arrange(this.GetClientRectangle(finalSize));
    }

    protected virtual void ArrangeChildren(SizeF finalSize)
    {
      int count = this.Children.Count;
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      for (int index = 0; index < count; ++index)
        this.Children[index].Arrange(clientRectangle);
    }

    protected SizeF ApplyClientOffset(SizeF measured)
    {
      Padding padding1 = this.Padding;
      Padding padding2 = TelerikDpiHelper.ScalePadding(this.GetBorderThickness(true), this.DpiScaleFactor);
      measured.Width += (float) (padding1.Horizontal + padding2.Horizontal);
      measured.Height += (float) (padding1.Vertical + padding2.Vertical);
      return measured;
    }

    protected SizeF ApplyMinMaxSize(SizeF measured)
    {
      SizeF minSize = (SizeF) this.MinSize;
      SizeF maxSize = (SizeF) this.MaxSize;
      measured.Width = Math.Max(minSize.Width, measured.Width);
      measured.Height = Math.Max(minSize.Height, measured.Height);
      if ((double) maxSize.Width > 0.0)
        measured.Width = Math.Min(maxSize.Width, measured.Width);
      if ((double) maxSize.Height > 0.0)
        measured.Height = Math.Min(maxSize.Height, measured.Height);
      return measured;
    }

    protected internal virtual void SetContentOrientation(
      PageViewContentOrientation orientation,
      bool recursive)
    {
      if (this.contentOrientation != orientation)
      {
        this.contentOrientation = orientation;
        this.FillPrimitiveImpl.InvalidateFillCache(RadPageViewElement.ItemContentOrientationProperty);
        this.InvalidateMeasure();
      }
      if (!recursive)
        return;
      foreach (RadElement child in this.Children)
        (child as RadPageViewElementBase)?.SetContentOrientation(orientation, recursive);
    }

    protected internal virtual void SetBorderAndFillOrientation(
      PageViewContentOrientation orientation,
      bool recursive)
    {
      if (this.borderAndFillOrientation == orientation)
        return;
      this.borderAndFillOrientation = orientation;
      this.FillPrimitiveImpl.InvalidateFillCache(RadPageViewElement.ItemContentOrientationProperty);
      if (!recursive)
        return;
      foreach (RadElement child in this.Children)
        (child as RadPageViewElementBase)?.SetBorderAndFillOrientation(orientation, recursive);
    }

    protected virtual object ApplyOrientationTransform(
      IGraphics graphics,
      PageViewContentOrientation orientation)
    {
      if (orientation == PageViewContentOrientation.Auto || orientation == PageViewContentOrientation.Horizontal)
        return (object) null;
      object obj = graphics.SaveState();
      float angle = 0.0f;
      float offsetX = 0.0f;
      float offsetY = 0.0f;
      this.CalculateRotationAndOffset(orientation, ref angle, ref offsetX, ref offsetY);
      graphics.TranslateTransform(offsetX, offsetY);
      graphics.RotateTransform(angle);
      return obj;
    }

    public override bool HitTest(Point point)
    {
      if (this.Size.Width == 0 || this.Size.Height == 0)
        return false;
      if (this.Shape == null)
        return base.HitTest(point);
      Size size = this.Size;
      if (this.BorderAndFillOrientation == PageViewContentOrientation.Vertical270 || this.BorderAndFillOrientation == PageViewContentOrientation.Vertical90)
        size = new Size(this.Size.Height, this.Size.Width);
      using (GraphicsPath path = this.Shape.CreatePath(new Rectangle(Point.Empty, size)))
      {
        bool flag = false;
        using (Matrix gdiMatrix = this.TotalTransform.ToGdiMatrix())
        {
          float angle = 0.0f;
          float offsetX = 0.0f;
          float offsetY = 0.0f;
          this.CalculateRotationAndOffset(this.BorderAndFillOrientation, ref angle, ref offsetX, ref offsetY);
          gdiMatrix.Translate(offsetX, offsetY);
          gdiMatrix.Rotate(angle);
          path.Transform(gdiMatrix);
          flag = path.IsVisible(point);
        }
        return flag;
      }
    }

    protected virtual void CalculateRotationAndOffset(
      PageViewContentOrientation orientation,
      ref float angle,
      ref float offsetX,
      ref float offsetY)
    {
      switch (orientation)
      {
        case PageViewContentOrientation.Horizontal180:
          angle = 180f;
          offsetX = (float) (this.Bounds.Right - 1);
          offsetY = (float) (this.Bounds.Bottom - 1);
          break;
        case PageViewContentOrientation.Vertical90:
          angle = 90f;
          offsetX = (float) (this.Bounds.Right - 1);
          offsetY = (float) this.Bounds.Y;
          break;
        case PageViewContentOrientation.Vertical270:
          angle = 270f;
          offsetX = (float) this.Bounds.X;
          offsetY = (float) (this.Bounds.Bottom - 1);
          break;
      }
    }

    protected override void PrePaintElement(IGraphics graphics)
    {
      RadImageShape backgroundShape = this.BackgroundShape;
      RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;
      Padding padding = Padding.Empty;
      if (backgroundShape != null)
      {
        rotateFlipType = backgroundShape.RotateFlip;
        padding = backgroundShape.Padding;
        switch (this.borderAndFillOrientation)
        {
          case PageViewContentOrientation.Horizontal180:
            backgroundShape.RotateFlip = RotateFlipType.Rotate180FlipNone;
            backgroundShape.Padding = new Padding(padding.Right, padding.Bottom, padding.Left, padding.Top);
            break;
          case PageViewContentOrientation.Vertical90:
            backgroundShape.RotateFlip = RotateFlipType.Rotate90FlipNone;
            backgroundShape.Padding = new Padding(padding.Bottom, padding.Left, padding.Top, padding.Right);
            break;
          case PageViewContentOrientation.Vertical270:
            backgroundShape.RotateFlip = RotateFlipType.Rotate270FlipNone;
            backgroundShape.Padding = new Padding(padding.Top, padding.Right, padding.Bottom, padding.Left);
            break;
        }
      }
      base.PrePaintElement(graphics);
      if (backgroundShape == null)
        return;
      backgroundShape.RotateFlip = rotateFlipType;
      backgroundShape.Padding = padding;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.DrawFill)
      {
        object state = this.CorrectFillAndBorderOrientation(graphics);
        this.PaintFill(graphics, angle, scale);
        if (state != null)
          graphics.RestoreState(state);
      }
      object state1 = this.ApplyOrientationTransform(graphics, this.contentOrientation);
      this.PaintContent(graphics);
      if (this.DrawBorder)
      {
        if (state1 != null)
          graphics.RestoreState(state1);
        state1 = this.CorrectFillAndBorderOrientation(graphics);
        this.PaintBorder(graphics, angle, scale);
      }
      if (state1 == null)
        return;
      graphics.RestoreState(state1);
    }

    protected override void PaintFill(
      IGraphics graphics,
      float angle,
      SizeF scale,
      RectangleF rect)
    {
      rect = this.ModifyBorderAndFillPaintRect(rect, this.FillPadding);
      base.PaintFill(graphics, angle, scale, rect);
    }

    protected override void PaintBorder(
      IGraphics graphics,
      float angle,
      SizeF scale,
      RectangleF rect)
    {
      rect = this.ModifyBorderAndFillPaintRect(rect, this.BorderPadding);
      base.PaintBorder(graphics, angle, scale, rect);
    }

    protected virtual object CorrectFillAndBorderOrientation(IGraphics g)
    {
      return this.ApplyOrientationTransform(g, this.borderAndFillOrientation);
    }

    protected virtual RectangleF ModifyBorderAndFillPaintRect(
      RectangleF preferred,
      Padding padding)
    {
      if (this.borderAndFillOrientation == PageViewContentOrientation.Vertical90 || this.borderAndFillOrientation == PageViewContentOrientation.Vertical270)
        preferred.Size = new SizeF(preferred.Height, preferred.Width);
      return LayoutUtils.DeflateRect(preferred, padding);
    }
  }
}
