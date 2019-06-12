// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FormImageBorderPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class FormImageBorderPrimitive : BasePrimitive
  {
    public static RadProperty TopLeftEndProperty = RadProperty.Register(nameof (TopLeftEnd), typeof (Image), typeof (FormImageBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TopRightEndProperty = RadProperty.Register(nameof (TopRightEnd), typeof (Image), typeof (FormImageBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LeftTextureProperty = RadProperty.Register(nameof (LeftTexture), typeof (Image), typeof (FormImageBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BottomLeftCornerProperty = RadProperty.Register(nameof (BottomLeftCorner), typeof (Image), typeof (FormImageBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BottomTextureProperty = RadProperty.Register(nameof (BottomTexture), typeof (Image), typeof (FormImageBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BottomRightCornerProperty = RadProperty.Register(nameof (BottomRightCorner), typeof (Image), typeof (FormImageBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RightTextureProperty = RadProperty.Register(nameof (RightTexture), typeof (Image), typeof (FormImageBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    private const int DEFAULT_NON_IMAGE_WIDTH = 3;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.FitToSizeMode = RadFitToSizeMode.FitToParentContent;
    }

    public override Padding BorderThickness
    {
      get
      {
        Padding borderWidth = this.BorderWidth;
        return new Padding(borderWidth.Left, 0, borderWidth.Right, borderWidth.Bottom);
      }
      set
      {
        base.BorderThickness = value;
      }
    }

    public Padding BorderWidth
    {
      get
      {
        RadForm radForm = this.ElementTree == null || this.ElementTree.Control == null ? (RadForm) null : this.ElementTree.Control as RadForm;
        if (radForm != null && TelerikHelper.IsMaterialTheme(radForm.ThemeName))
        {
          int num = TelerikDpiHelper.ScaleInt(1, this.DpiScaleFactor);
          return new Padding(num, 0, num, num);
        }
        Padding padding = new Padding(this.GetAvailableLeftWidth(), 0, this.GetAvailableRightWidth(), this.GetAvailableBottomHeight());
        if (!(padding == Padding.Empty))
          return padding;
        int num1 = TelerikDpiHelper.ScaleInt(3, this.DpiScaleFactor);
        return new Padding(num1, 0, num1, num1);
      }
    }

    public Image TopLeftEnd
    {
      get
      {
        return this.GetValue(FormImageBorderPrimitive.TopLeftEndProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(FormImageBorderPrimitive.TopLeftEndProperty, (object) value);
      }
    }

    public Image TopRightEnd
    {
      get
      {
        return this.GetValue(FormImageBorderPrimitive.TopRightEndProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(FormImageBorderPrimitive.TopRightEndProperty, (object) value);
      }
    }

    public Image LeftTexture
    {
      get
      {
        return this.GetValue(FormImageBorderPrimitive.LeftTextureProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(FormImageBorderPrimitive.LeftTextureProperty, (object) value);
      }
    }

    public Image BottomTexture
    {
      get
      {
        return this.GetValue(FormImageBorderPrimitive.BottomTextureProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(FormImageBorderPrimitive.BottomTextureProperty, (object) value);
      }
    }

    public Image RightTexture
    {
      get
      {
        return this.GetValue(FormImageBorderPrimitive.RightTextureProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(FormImageBorderPrimitive.RightTextureProperty, (object) value);
      }
    }

    public Image BottomLeftCorner
    {
      get
      {
        return this.GetValue(FormImageBorderPrimitive.BottomLeftCornerProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(FormImageBorderPrimitive.BottomLeftCornerProperty, (object) value);
      }
    }

    public Image BottomRightCorner
    {
      get
      {
        return this.GetValue(FormImageBorderPrimitive.BottomRightCornerProperty) as Image;
      }
      set
      {
        int num = (int) this.SetValue(FormImageBorderPrimitive.BottomRightCornerProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintPrimitive(graphics, angle, scale);
      if (!this.IsAtLeastOneImageLoaded())
      {
        this.PaintBackground(graphics);
      }
      else
      {
        this.PaintLeftTopEnd(graphics);
        this.PaintLeftBorderTexture(graphics);
        this.PaintBottomLeftCorner(graphics);
        this.PaintBottomTexture(graphics);
        this.PaintBottomRightCorner(graphics);
        this.PaintRightBorderTexture(graphics);
        this.PaintRightTopEnd(graphics);
      }
    }

    private bool IsAtLeastOneImageLoaded()
    {
      bool flag1 = false;
      bool flag2 = this.LeftTexture != null || flag1;
      bool flag3 = this.BottomTexture != null || flag2;
      return this.RightTexture != null || flag3;
    }

    private int GetAvailableLeftWidth()
    {
      int val1 = 0;
      if (this.LeftTexture != null)
      {
        val1 = this.LeftTexture.Width;
        if (this.TopLeftEnd != null)
          val1 = Math.Min(val1, this.TopLeftEnd.Width);
        if (this.BottomLeftCorner != null)
          val1 = Math.Min(val1, this.BottomLeftCorner.Width);
      }
      return val1;
    }

    private int GetAvailableBottomHeight()
    {
      int val1 = 0;
      if (this.BottomTexture != null)
      {
        val1 = this.BottomTexture.Height;
        if (this.BottomLeftCorner != null)
          val1 = Math.Min(val1, this.BottomLeftCorner.Height);
        if (this.BottomRightCorner != null)
          val1 = Math.Min(val1, this.BottomRightCorner.Height);
      }
      return val1;
    }

    private int GetAvailableRightWidth()
    {
      int val1 = 0;
      if (this.RightTexture != null)
      {
        val1 = this.RightTexture.Width;
        if (this.TopRightEnd != null)
          val1 = Math.Min(val1, this.TopRightEnd.Width);
        if (this.BottomRightCorner != null)
          val1 = Math.Min(val1, this.BottomRightCorner.Width);
      }
      return val1;
    }

    private void PaintBackground(IGraphics graphics)
    {
      Rectangle BorderRectangle1 = new Rectangle(Point.Empty, new Size(3, this.Size.Height));
      graphics.FillRectangle(BorderRectangle1, this.BackColor);
      Rectangle BorderRectangle2 = new Rectangle(new Point(3, this.Size.Height - 3), new Size(this.Size.Width - 6, 3));
      graphics.FillRectangle(BorderRectangle2, this.BackColor);
      Rectangle BorderRectangle3 = new Rectangle(new Point(this.Size.Width - 3, 0), new Size(3, this.Size.Height));
      graphics.FillRectangle(BorderRectangle3, this.BackColor);
    }

    private void PaintLeftTopEnd(IGraphics graphics)
    {
      if (this.TopLeftEnd == null)
        return;
      Rectangle rectangle = new Rectangle(0, 0, this.GetAvailableLeftWidth(), this.TopLeftEnd.Height);
      graphics.FillTextureRectangle(rectangle, this.TopLeftEnd, WrapMode.Tile);
    }

    private void PaintRightTopEnd(IGraphics graphics)
    {
      if (this.TopRightEnd == null)
        return;
      int availableRightWidth = this.GetAvailableRightWidth();
      Rectangle rectangle = new Rectangle(this.Size.Width - availableRightWidth, 0, availableRightWidth, this.TopRightEnd.Height);
      graphics.DrawImage(rectangle, this.TopRightEnd, ContentAlignment.TopLeft, true);
    }

    private void PaintLeftBorderTexture(IGraphics graphics)
    {
      if (this.LeftTexture == null)
        return;
      int x = 0;
      int y = this.TopLeftEnd != null ? this.TopLeftEnd.Height : 0;
      int height = this.Size.Height;
      int availableLeftWidth = this.GetAvailableLeftWidth();
      if (this.TopLeftEnd != null)
        height -= this.TopLeftEnd.Height;
      if (this.BottomLeftCorner != null)
        height -= this.GetAvailableBottomHeight();
      Rectangle rectangle = new Rectangle(x, y, availableLeftWidth, height);
      graphics.FillTextureRectangle(rectangle, this.LeftTexture, WrapMode.Tile);
    }

    private void PaintBottomLeftCorner(IGraphics graphics)
    {
      if (this.BottomLeftCorner == null)
        return;
      int availableBottomHeight = this.GetAvailableBottomHeight();
      int availableLeftWidth = this.GetAvailableLeftWidth();
      Rectangle rectangle = new Rectangle(0, this.Size.Height - availableBottomHeight, availableLeftWidth, availableBottomHeight);
      graphics.DrawImage(rectangle, this.BottomLeftCorner, ContentAlignment.TopLeft, true);
    }

    private void PaintBottomTexture(IGraphics graphics)
    {
      if (this.BottomTexture == null)
        return;
      int availableRightWidth = this.GetAvailableRightWidth();
      int availableLeftWidth = this.GetAvailableLeftWidth();
      int width = this.Size.Width;
      int availableBottomHeight = this.GetAvailableBottomHeight();
      Rectangle rectangle = new Rectangle(availableLeftWidth, this.Size.Height - availableBottomHeight, width - availableLeftWidth - availableRightWidth, availableBottomHeight);
      graphics.FillTextureRectangle(rectangle, this.BottomTexture, WrapMode.Tile);
    }

    private void PaintBottomRightCorner(IGraphics graphics)
    {
      if (this.BottomRightCorner == null)
        return;
      int availableRightWidth = this.GetAvailableRightWidth();
      int availableBottomHeight = this.GetAvailableBottomHeight();
      Rectangle rectangle = new Rectangle(this.Size.Width - availableRightWidth, this.Size.Height - availableBottomHeight, availableRightWidth, availableBottomHeight);
      graphics.DrawImage(rectangle, this.BottomRightCorner, ContentAlignment.TopLeft, true);
    }

    private void PaintRightBorderTexture(IGraphics graphics)
    {
      if (this.RightTexture == null)
        return;
      int height = this.Size.Height;
      int availableRightWidth = this.GetAvailableRightWidth();
      int x = this.Size.Width - availableRightWidth;
      int y = this.TopRightEnd != null ? this.TopRightEnd.Height : 0;
      if (this.TopRightEnd != null)
        height -= this.TopRightEnd.Height;
      if (this.BottomRightCorner != null)
        height -= this.GetAvailableBottomHeight();
      Rectangle rectangle = new Rectangle(x, y, availableRightWidth, height);
      graphics.FillTextureRectangle(rectangle, this.RightTexture, WrapMode.Tile);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.TopRightEnd != null)
        this.TopLeftEnd.Dispose();
      if (this.LeftTexture != null)
        this.LeftTexture.Dispose();
      if (this.BottomLeftCorner != null)
        this.BottomLeftCorner.Dispose();
      if (this.BottomTexture != null)
        this.BottomTexture.Dispose();
      if (this.BottomRightCorner != null)
        this.BottomRightCorner.Dispose();
      if (this.RightTexture != null)
        this.RightTexture.Dispose();
      if (this.TopRightEnd == null)
        return;
      this.TopRightEnd.Dispose();
    }
  }
}
