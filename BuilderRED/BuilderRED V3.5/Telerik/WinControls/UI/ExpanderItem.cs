// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExpanderItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ExpanderItem : LightVisualElement
  {
    public static RoutedEvent ExpandedChangedEvent = RadElement.RegisterRoutedEvent(nameof (ExpandedChangedEvent), typeof (ExpanderItem));
    public static readonly RadProperty SignPaddingProperty = RadProperty.Register(nameof (SignPadding), typeof (Padding), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty SignWidthProperty = RadProperty.Register(nameof (SignWidth), typeof (float), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty SignBorderWidthProperty = RadProperty.Register(nameof (SignBorderWidth), typeof (float), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty SignBorderPaddingProperty = RadProperty.Register(nameof (SignBorderPadding), typeof (Padding), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Padding.Empty, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty DrawSignBorderProperty = RadProperty.Register(nameof (DrawSignBorder), typeof (bool), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty DrawSignFillProperty = RadProperty.Register(nameof (DrawSignFill), typeof (bool), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignBorderColorProperty = RadProperty.Register(nameof (SignBorderColor), typeof (Color), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.Control), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignBackColorProperty = RadProperty.Register(nameof (SignBackColor), typeof (Color), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.Control), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignBackColor2Property = RadProperty.Register(nameof (SignBackColor2), typeof (Color), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.Control), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignBackColor3Property = RadProperty.Register(nameof (SignBackColor3), typeof (Color), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlDark), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignBackColor4Property = RadProperty.Register(nameof (SignBackColor4), typeof (Color), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlLightLight), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignNumberOfColorsProperty = RadProperty.Register(nameof (SignNumberOfColors), typeof (int), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignGradientStyleProperty = RadProperty.Register(nameof (SignGradientStyle), typeof (GradientStyles), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Linear, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignGradientAngleProperty = RadProperty.Register(nameof (SignGradientAngle), typeof (float), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignGradientPercentageProperty = RadProperty.Register(nameof (SignGradientPercentage), typeof (float), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.5f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignGradientPercentage2Property = RadProperty.Register(nameof (SignGradientPercentage2), typeof (float), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.666f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignStyleProperty = RadProperty.Register(nameof (SignStyle), typeof (SignStyles), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SignStyles.PlusMinus, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SquareSignSizeProperty = RadProperty.Register(nameof (SquareSignSize), typeof (bool), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignSizeProperty = RadProperty.Register(nameof (SignSize), typeof (SizeF), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new SizeF(9f, 9f), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ExpandedProperty = RadProperty.Register(nameof (Expanded), typeof (bool), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SignImageProperty = RadProperty.Register(nameof (SignImage), typeof (Image), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LinkLineStyleProperty = RadProperty.Register(nameof (LinkLineStyle), typeof (DashStyle), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DashStyle.Solid, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LinkOrientationProperty = RadProperty.Register(nameof (LinkOrientation), typeof (ExpanderItem.LinkLineOrientation), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ExpanderItem.LinkLineOrientation.None, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LinkLineColorProperty = RadProperty.Register(nameof (LinkLineColor), typeof (Color), typeof (ExpanderItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Black, ElementPropertyOptions.AffectsDisplay));
    protected Image cachedSignImage;

    static ExpanderItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ExpanderItemStateManager(), typeof (ExpanderItem));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
      this.StretchHorizontally = false;
      this.StretchVertically = true;
      this.BypassLayoutPolicies = true;
      this.ShouldPaint = true;
    }

    protected override void DisposeManagedResources()
    {
      if (this.cachedSignImage != null)
      {
        this.cachedSignImage.Dispose();
        this.cachedSignImage = (Image) null;
      }
      base.DisposeManagedResources();
    }

    public Padding SignPadding
    {
      get
      {
        return TelerikDpiHelper.ScalePadding((Padding) this.GetValue(ExpanderItem.SignPaddingProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignPaddingProperty, (object) value);
      }
    }

    public float SignWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(ExpanderItem.SignWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignWidthProperty, (object) value);
      }
    }

    public float SignBorderWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(ExpanderItem.SignBorderWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignBorderWidthProperty, (object) value);
      }
    }

    public Padding SignBorderPadding
    {
      get
      {
        return TelerikDpiHelper.ScalePadding((Padding) this.GetValue(ExpanderItem.SignBorderPaddingProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignBorderPaddingProperty, (object) value);
      }
    }

    public bool DrawSignBorder
    {
      get
      {
        return (bool) this.GetValue(ExpanderItem.DrawSignBorderProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.DrawSignBorderProperty, (object) value);
      }
    }

    public bool DrawSignFill
    {
      get
      {
        return (bool) this.GetValue(ExpanderItem.DrawSignFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.DrawSignFillProperty, (object) value);
      }
    }

    public virtual Color SignBorderColor
    {
      get
      {
        return (Color) this.GetValue(ExpanderItem.SignBorderColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignBorderColorProperty, (object) value);
      }
    }

    public virtual Color SignBackColor
    {
      get
      {
        return (Color) this.GetValue(ExpanderItem.SignBackColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignBackColorProperty, (object) value);
      }
    }

    public virtual Color SignBackColor2
    {
      get
      {
        return (Color) this.GetValue(ExpanderItem.SignBackColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignBackColor2Property, (object) value);
      }
    }

    public virtual Color SignBackColor3
    {
      get
      {
        return (Color) this.GetValue(ExpanderItem.SignBackColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignBackColor3Property, (object) value);
      }
    }

    public virtual Color SignBackColor4
    {
      get
      {
        return (Color) this.GetValue(ExpanderItem.SignBackColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignBackColor4Property, (object) value);
      }
    }

    public virtual int SignNumberOfColors
    {
      get
      {
        return (int) this.GetValue(ExpanderItem.SignNumberOfColorsProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignNumberOfColorsProperty, (object) value);
      }
    }

    public virtual GradientStyles SignGradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(ExpanderItem.SignGradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignGradientStyleProperty, (object) value);
      }
    }

    public virtual float SignGradientAngle
    {
      get
      {
        return (float) this.GetValue(ExpanderItem.SignGradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignGradientAngleProperty, (object) value);
      }
    }

    public virtual float SignGradientPercentage
    {
      get
      {
        return (float) this.GetValue(ExpanderItem.SignGradientPercentageProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignGradientPercentageProperty, (object) value);
      }
    }

    public virtual float SignGradientPercentage2
    {
      get
      {
        return (float) this.GetValue(ExpanderItem.SignGradientPercentage2Property);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignGradientPercentage2Property, (object) value);
      }
    }

    public virtual SignStyles SignStyle
    {
      get
      {
        return (SignStyles) this.GetValue(ExpanderItem.SignStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignStyleProperty, (object) value);
      }
    }

    public virtual bool SquareSignSize
    {
      get
      {
        return (bool) this.GetValue(ExpanderItem.SquareSignSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SquareSignSizeProperty, (object) value);
      }
    }

    public virtual SizeF SignSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSizeF((SizeF) this.GetValue(ExpanderItem.SignSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignSizeProperty, (object) value);
      }
    }

    public virtual bool Expanded
    {
      get
      {
        return (bool) this.GetValue(ExpanderItem.ExpandedProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.ExpandedProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image SignImage
    {
      get
      {
        return this.cachedSignImage;
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.SignImageProperty, (object) value);
      }
    }

    public ExpanderItem.LinkLineOrientation LinkOrientation
    {
      get
      {
        return (ExpanderItem.LinkLineOrientation) this.GetValue(ExpanderItem.LinkOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.LinkOrientationProperty, (object) value);
      }
    }

    public DashStyle LinkLineStyle
    {
      get
      {
        return (DashStyle) this.GetValue(ExpanderItem.LinkLineStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.LinkLineStyleProperty, (object) value);
      }
    }

    public Color LinkLineColor
    {
      get
      {
        return (Color) this.GetValue(ExpanderItem.LinkLineColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(ExpanderItem.LinkLineColorProperty, (object) value);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == ExpanderItem.SignImageProperty)
      {
        if (e.NewValue != null)
          this.UpdateSignImage((Image) e.NewValue);
        else
          this.cachedSignImage = (Image) null;
      }
      else if (e.Property == ExpanderItem.ExpandedProperty)
        this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs(EventArgs.Empty, ExpanderItem.ExpandedChangedEvent));
      base.OnPropertyChanged(e);
    }

    protected virtual void UpdateSignImage(Image newImage)
    {
      if (newImage == null)
        return;
      this.cachedSignImage = (Image) new Bitmap((int) Math.Round((double) newImage.Width * (double) this.DpiScaleFactor.Width), (int) Math.Round((double) newImage.Height * (double) this.DpiScaleFactor.Height));
      Graphics.FromImage(this.cachedSignImage).DrawImage(newImage, new RectangleF((PointF) Point.Empty, (SizeF) this.cachedSignImage.Size));
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.UpdateSignImage((Image) this.GetValue(ExpanderItem.SignImageProperty));
    }

    protected override bool IsPropertyCancelable(RadPropertyMetadata metadata)
    {
      return true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.ToggleExpanded();
    }

    protected virtual void ToggleExpanded()
    {
      this.Expanded = !this.Expanded;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      SizeF sizeF = this.SignSize;
      if (this.SignStyle == SignStyles.Image && this.cachedSignImage != null)
        sizeF = (SizeF) this.cachedSignImage.Size;
      sizeF.Width += (float) this.Padding.Horizontal;
      sizeF.Height += (float) this.Padding.Vertical;
      return sizeF;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      float val1_1;
      float val1_2;
      if (this.SquareSignSize)
      {
        val1_1 = Math.Min(this.SignSize.Width, this.SignSize.Height);
        val1_2 = val1_1;
      }
      else if (this.SignStyle == SignStyles.Image && this.cachedSignImage != null)
      {
        val1_1 = (float) this.cachedSignImage.Size.Width;
        val1_2 = (float) this.cachedSignImage.Size.Height;
      }
      else
      {
        val1_1 = this.SignSize.Width;
        val1_2 = this.SignSize.Height;
      }
      RectangleF rectangleF = new RectangleF((float) this.Padding.Left, (float) this.Padding.Top, (float) (this.Size.Width - this.Padding.Horizontal), (float) (this.Size.Height - this.Padding.Vertical));
      float width1 = (float) Math.Round((double) Math.Min(val1_1, rectangleF.Width));
      float height1 = (float) Math.Round((double) Math.Min(val1_2, rectangleF.Height));
      RectangleF signBorder = new RectangleF((float) Math.Round((double) rectangleF.Left + ((double) rectangleF.Width - (double) width1) / 2.0), (float) Math.Round((double) rectangleF.Top + ((double) rectangleF.Height - (double) height1) / 2.0), width1, height1);
      if (this.DrawSignFill)
        this.PaintFill(graphics, Rectangle.Round(signBorder));
      if (this.DrawSignBorder)
      {
        if ((double) this.SignBorderWidth == 1.0)
        {
          --signBorder.Width;
          --signBorder.Height;
        }
        this.PaintBorder(graphics, (RectangleF) Rectangle.Round(signBorder));
      }
      float x = signBorder.Left + (float) this.SignPadding.Left;
      float y = signBorder.Top + (float) this.SignPadding.Top;
      float width2 = signBorder.Width - (float) this.SignPadding.Horizontal;
      float height2 = signBorder.Height - (float) this.SignPadding.Vertical;
      if (this.DrawSignBorder)
      {
        x += this.SignBorderWidth;
        y += this.SignBorderWidth;
        width2 -= this.SignBorderWidth * 2f;
        height2 -= this.SignBorderWidth * 2f;
        if ((double) width2 % 2.0 == 1.0)
          ++width2;
        if ((double) height2 % 2.0 == 1.0)
          ++height2;
      }
      RectangleF signRect = new RectangleF(x, y, width2, height2);
      this.PaintSign(graphics, signRect);
      this.PaintSignLines(graphics, signRect, signBorder);
    }

    protected virtual void PaintFill(IGraphics g, Rectangle rect)
    {
      if (this.SignBackColor.A == (byte) 0 && (this.SignNumberOfColors <= 1 || this.SignBackColor2.A == (byte) 0 && (this.SignNumberOfColors <= 2 || this.SignBackColor3.A == (byte) 0 && (this.SignNumberOfColors <= 3 || this.SignBackColor4.A == (byte) 0))) || (this.Size.Width <= 0 || this.Size.Height <= 0))
        return;
      int val2 = 4;
      Color[] colorStops = new Color[Math.Min(Math.Max(this.SignNumberOfColors, 1), val2)];
      float[] colorOffsets = new float[Math.Min(Math.Max(this.SignNumberOfColors, 1), val2)];
      if (this.SignNumberOfColors > 0)
      {
        colorStops[0] = this.SignBackColor;
        colorOffsets[0] = 0.0f;
      }
      if (this.SignNumberOfColors > 1)
      {
        colorStops[1] = this.SignBackColor2;
        colorOffsets[colorStops.Length - 1] = 1f;
      }
      if (this.SignNumberOfColors > 2)
      {
        colorStops[2] = this.SignBackColor3;
        colorOffsets[1] = this.SignGradientPercentage;
      }
      if (this.SignNumberOfColors > 3)
      {
        colorStops[3] = this.SignBackColor4;
        colorOffsets[2] = this.SignGradientPercentage2;
      }
      switch (this.SignGradientStyle)
      {
        case GradientStyles.Solid:
          g.FillRectangle(rect, this.SignBackColor);
          break;
        case GradientStyles.Linear:
        case GradientStyles.Radial:
          if (this.SignNumberOfColors < 2)
          {
            g.FillRectangle(rect, this.SignBackColor);
            break;
          }
          g.FillGradientRectangle(rect, colorStops, colorOffsets, this.SignGradientStyle, this.SignGradientAngle, this.SignGradientPercentage, this.SignGradientPercentage2);
          break;
        case GradientStyles.Glass:
          g.FillGlassRectangle(rect, this.SignBackColor, this.SignBackColor2, this.SignBackColor3, this.SignBackColor4, this.SignGradientPercentage, this.SignGradientPercentage2);
          break;
        case GradientStyles.OfficeGlass:
          g.FillOfficeGlassRectangle(rect, this.SignBackColor, this.SignBackColor2, this.SignBackColor3, this.SignBackColor4, this.SignGradientPercentage, this.SignGradientPercentage2, true);
          break;
        case GradientStyles.OfficeGlassRect:
          g.FillOfficeGlassRectangle(rect, this.SignBackColor, this.SignBackColor2, this.SignBackColor3, this.SignBackColor4, this.SignGradientPercentage, this.SignGradientPercentage2, false);
          break;
        case GradientStyles.Gel:
          g.FillGellRectangle(rect, colorStops, this.SignGradientPercentage, this.SignGradientPercentage2);
          break;
        case GradientStyles.Vista:
          g.FillVistaRectangle(rect, this.SignBackColor, this.SignBackColor2, this.SignBackColor3, this.SignBackColor4, this.SignGradientPercentage, this.SignGradientPercentage2);
          break;
      }
    }

    protected virtual void PaintBorder(IGraphics g, RectangleF signBorder)
    {
      g.DrawRectangle(signBorder, this.SignBorderColor, PenAlignment.Inset, this.SignBorderWidth);
    }

    protected virtual void PaintSign(IGraphics g, RectangleF signRect)
    {
      if (this.SignStyle == SignStyles.Image)
      {
        if (this.cachedSignImage == null)
          return;
        Point point = new Point(Math.Max(0, (this.Size.Width - this.cachedSignImage.Width) / 2), Math.Max(0, (this.Size.Height - this.cachedSignImage.Height) / 2));
        point.X = Math.Min(point.X, this.Size.Width);
        point.Y = Math.Min(point.Y, this.Size.Height);
        Size size = new Size(Math.Min(this.Size.Width, this.cachedSignImage.Size.Width), Math.Min(this.Size.Height, this.cachedSignImage.Size.Height));
        this.PaintSignImage(g, (PointF) point, (SizeF) size);
      }
      else
      {
        if ((double) signRect.Width <= (double) this.SignWidth || (double) signRect.Height <= (double) this.SignWidth)
          return;
        this.PaintSignShape(g, signRect);
      }
    }

    protected void PaintSignImage(IGraphics g, PointF pos, SizeF sz)
    {
      Graphics underlayGraphics = (Graphics) g.UnderlayGraphics;
      Image image = this.cachedSignImage;
      if (this.Opacity != 1.0)
      {
        image = (Image) new Bitmap(this.cachedSignImage);
        ImageHelper.ApplyAlpha(image as Bitmap, Convert.ToSingle(this.Opacity));
      }
      underlayGraphics.DrawImageUnscaledAndClipped(image, Rectangle.Round(new RectangleF(pos, sz)));
    }

    protected void PaintSignShape(IGraphics g, RectangleF signRect)
    {
      using (Pen pen = new Pen(this.ForeColor, this.SignWidth))
      {
        pen.Alignment = PenAlignment.Inset;
        Graphics underlayGraphics = (Graphics) g.UnderlayGraphics;
        if (this.SignStyle == SignStyles.PlusMinus)
        {
          if (this.Expanded)
          {
            underlayGraphics.DrawLine(pen, signRect.Left, signRect.Top + signRect.Height / 2f, signRect.Right, signRect.Top + signRect.Height / 2f);
          }
          else
          {
            float num1 = (float) Math.Round((double) signRect.Top + (double) signRect.Height / 2.0);
            underlayGraphics.DrawLine(pen, signRect.Left, num1, signRect.Right, num1);
            float num2 = (float) Math.Floor((double) signRect.Left + (double) signRect.Width / 2.0);
            underlayGraphics.DrawLine(pen, num2, signRect.Top, num2, signRect.Bottom);
          }
        }
        else if (this.SignStyle == SignStyles.Arrow)
        {
          if (this.Expanded)
          {
            underlayGraphics.DrawLine(pen, signRect.Left, signRect.Bottom, signRect.Left + signRect.Width / 2f, signRect.Top);
            underlayGraphics.DrawLine(pen, signRect.Left + signRect.Width / 2f, signRect.Top, signRect.Right, signRect.Bottom);
          }
          else
          {
            underlayGraphics.DrawLine(pen, signRect.Left, signRect.Top, signRect.Left + signRect.Width / 2f, signRect.Bottom);
            underlayGraphics.DrawLine(pen, signRect.Left + signRect.Width / 2f, signRect.Bottom, signRect.Right, signRect.Top);
          }
        }
        else
        {
          if (this.SignStyle != SignStyles.Triangle)
            return;
          using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
          {
            if (this.Expanded)
            {
              PointF[] points = new PointF[3]{ new PointF(signRect.X, signRect.Bottom), new PointF(signRect.Right, signRect.Bottom), new PointF(signRect.X + signRect.Width / 2f, signRect.Y) };
              underlayGraphics.FillPolygon((Brush) solidBrush, points);
            }
            else
            {
              PointF[] points = new PointF[3]{ new PointF(signRect.X, signRect.Y), new PointF(signRect.X + signRect.Width / 2f, signRect.Bottom), new PointF(signRect.Right, signRect.Y) };
              underlayGraphics.FillPolygon((Brush) solidBrush, points);
            }
          }
        }
      }
    }

    protected virtual void PaintSignLines(IGraphics g, RectangleF signRect, RectangleF signBorder)
    {
      if (this.LinkOrientation == ExpanderItem.LinkLineOrientation.None)
        return;
      int num1 = (int) Math.Round((double) signRect.Left + (double) signRect.Width / 2.0);
      if ((this.LinkOrientation & ExpanderItem.LinkLineOrientation.Top) != ExpanderItem.LinkLineOrientation.None)
      {
        int top = this.Padding.Top;
        int y2 = (int) Math.Round((double) signBorder.Top) - 1;
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, num1, top, num1, y2);
      }
      if ((this.LinkOrientation & ExpanderItem.LinkLineOrientation.Bottom) != ExpanderItem.LinkLineOrientation.None)
      {
        int y1 = (int) Math.Round((double) signBorder.Bottom);
        int y2 = this.Size.Height - this.Padding.Vertical;
        if (this.DrawSignBorder && (double) this.SignBorderWidth == 1.0)
          ++y1;
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, num1, y1, num1, y2);
      }
      if ((this.LinkOrientation & ExpanderItem.LinkLineOrientation.Horizontal) == ExpanderItem.LinkLineOrientation.None)
        return;
      int num2 = this.Size.Height / 2;
      if (this.RightToLeft)
      {
        int x2 = (int) Math.Round((double) signRect.Left);
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, 0, num2, x2, num2);
      }
      else
      {
        int x1 = (int) Math.Round((double) signRect.Right);
        g.DrawLine(this.LinkLineColor, this.LinkLineStyle, x1, num2, this.Size.Width, num2);
      }
    }

    [Flags]
    public enum LinkLineOrientation
    {
      None = 0,
      Bottom = 1,
      Top = 2,
      Horizontal = 4,
    }
  }
}
