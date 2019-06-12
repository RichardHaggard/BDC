// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ImagePrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class ImagePrimitive : BasePrimitive, IImageElement
  {
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageIndexProperty = RadProperty.Register(nameof (ImageIndex), typeof (int), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageKeyProperty = RadProperty.Register(nameof (ImageKey), typeof (string), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageScalingProperty = RadProperty.Register(nameof (ImageScaling), typeof (ImageScaling), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ImageScaling.None, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TransparentColorProperty = RadProperty.Register(nameof (TransparentColor), typeof (Color), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ScaleSizeProperty = RadProperty.Register(nameof (ScaleSize), typeof (Size), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(16, 16), ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty UseSmallImageListProperty = RadProperty.Register(nameof (UseSmallImageList), typeof (bool), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty ImageLayoutProperty = RadProperty.Register(nameof (ImageLayout), typeof (ImageLayout), typeof (ImagePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ImageLayout.Center, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    internal const long ImageInvalidatedStateKey = 137438953472;
    internal const long IsImageUpdatingStateKey = 274877906944;
    internal const long IsImageListUpdatingStateKey = 549755813888;
    internal const long IsSetFromImageListStateKey = 1099511627776;
    internal const long CurrentlyAnimatingStateKey = 2199023255552;
    internal const long ImageClonedStateKey = 4398046511104;
    internal const long ImagePrimitiveLastStateKey = 4398046511104;
    private RotateFlipType rotateFlip;
    private Image cachedImage;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.Auto;
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.BitState[137438953472L] = true;
    }

    public override Telerik.WinControls.Filter GetStylablePropertiesFilter()
    {
      return (Telerik.WinControls.Filter) PropertyFilter.ImagePrimitiveFilter;
    }

    [DefaultValue(false)]
    public override bool StretchHorizontally
    {
      get
      {
        return base.StretchHorizontally;
      }
      set
      {
        base.StretchHorizontally = value;
      }
    }

    [DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    public ImageLayout ImageLayout
    {
      get
      {
        return (ImageLayout) this.GetValue(ImagePrimitive.ImageLayoutProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.ImageLayoutProperty, (object) value);
      }
    }

    [Description("Gets or sets the desired size to be used when displaying the image. Works when ImageScalingMode.FitToSize is applied.")]
    public Size ScaleSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(ImagePrimitive.ScaleSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.ScaleSizeProperty, (object) value);
      }
    }

    [Description("Gets or sets the image that is displayed.")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("Image", typeof (ImagePrimitive))]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image Image
    {
      get
      {
        return (Image) this.GetValue(ImagePrimitive.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.ImageProperty, (object) value);
      }
    }

    [Browsable(false)]
    public Image RenderImage
    {
      get
      {
        return this.GetPaintImage();
      }
    }

    private void BuildImageCache(Image image)
    {
      if (this.cachedImage != null && this.BitState[4398046511104L])
      {
        this.cachedImage.Dispose();
        this.cachedImage = (Image) null;
      }
      this.BitState[4398046511104L] = false;
      this.cachedImage = image;
      if (this.cachedImage == null)
        return;
      lock (this.cachedImage)
      {
        if (ImageAnimator.CanAnimate(this.cachedImage))
          return;
      }
      Bitmap bitmap = this.cachedImage.Clone() as Bitmap;
      if (bitmap == null)
        return;
      if (!bitmap.RawFormat.Equals((object) ImageFormat.Icon))
        bitmap.MakeTransparent(this.TransparentColor);
      if (this.rotateFlip != RotateFlipType.RotateNoneFlipNone)
        bitmap.RotateFlip(this.rotateFlip);
      this.cachedImage = (Image) bitmap;
      this.BitState[4398046511104L] = true;
    }

    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ImageIndex", typeof (ImagePrimitive))]
    [Category("Appearance")]
    [Description("Gets or sets the image list index value of the displayed image.")]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public int ImageIndex
    {
      get
      {
        return (int) this.GetValue(ImagePrimitive.ImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.ImageIndexProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ImageKey", typeof (ImagePrimitive))]
    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public string ImageKey
    {
      get
      {
        return (string) this.GetValue(ImagePrimitive.ImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.ImageKeyProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("UseSmallImageList", typeof (ImagePrimitive))]
    [Category("Image")]
    public bool UseSmallImageList
    {
      get
      {
        return (bool) this.GetValue(ImagePrimitive.UseSmallImageListProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.UseSmallImageListProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ImageScaling", typeof (ImagePrimitive))]
    [Category("Image")]
    [Description("ToolStripItemImageScalingDescr")]
    public ImageScaling ImageScaling
    {
      get
      {
        return (ImageScaling) this.GetValue(ImagePrimitive.ImageScalingProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.ImageScalingProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual int ActualIndex
    {
      get
      {
        if (this.ImageIndex >= 0)
          return this.ImageIndex;
        if (this.ElementTree != null && this.ElementTree.ComponentTreeHandler.ImageList != null)
          return this.ElementTree.ComponentTreeHandler.ImageList.Images.IndexOfKey(this.ImageKey);
        return -1;
      }
    }

    private ImageList ImageList
    {
      get
      {
        if (this.ElementTree == null)
          return (ImageList) null;
        if (this.UseSmallImageList)
          return this.ElementTree.ComponentTreeHandler.SmallImageList;
        return this.ElementTree.ComponentTreeHandler.ImageList;
      }
    }

    private Size GetImageSize()
    {
      if (this.ImageScaling == ImageScaling.SizeToFit)
        return this.ScaleSize;
      Image image = this.Image;
      if (image == null)
        return Size.Empty;
      lock (image)
      {
        try
        {
          if (RadControl.EnableDpiScaling)
            return new Size((int) Math.Round((double) image.Size.Width * (double) this.DpiScaleFactor.Width), (int) Math.Round((double) image.Size.Height * (double) this.DpiScaleFactor.Height));
          return image.Size;
        }
        catch
        {
          return Size.Empty;
        }
      }
    }

    [Browsable(false)]
    public override bool IsEmpty
    {
      get
      {
        return this.Image == null;
      }
    }

    private bool IsImageListSet
    {
      get
      {
        bool flag1 = this.ImageIndex >= 0 || this.ImageKey != string.Empty;
        bool flag2 = this.ImageList != null;
        if (flag1)
          return flag2;
        return false;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.UpdateImage();
    }

    protected override void OnUnloaded(ComponentThemableElementTree oldTree)
    {
      base.OnUnloaded(oldTree);
      this.UpdateImage();
    }

    private void UpdateImage()
    {
      if (this.GetBitState(274877906944L))
        return;
      this.BitState[549755813888L] = true;
      if (!this.IsImageListSet)
      {
        if (this.GetBitState(1099511627776L))
        {
          int num1 = (int) this.SetValue(ImagePrimitive.ImageProperty, (object) null);
        }
      }
      else if (this.ImageIndex >= 0 && this.ImageIndex < this.ImageList.Images.Count)
      {
        int num2 = (int) this.SetValue(ImagePrimitive.ImageProperty, (object) new Bitmap(this.ImageList.Images[this.ImageIndex]));
        this.BitState[1099511627776L] = true;
      }
      else if (!string.IsNullOrEmpty(this.ImageKey) && this.ImageList.Images.IndexOfKey(this.ImageKey) >= 0)
      {
        int num2 = (int) this.SetValue(ImagePrimitive.ImageProperty, (object) new Bitmap(this.ImageList.Images[this.ImageKey]));
        this.BitState[1099511627776L] = true;
      }
      this.BitState[549755813888L] = false;
      this.BitState[137438953472L] = true;
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (this.ElementState != ElementState.Loaded || args.RoutedEvent != RootRadElement.OnRoutedImageListChanged && args.RoutedEvent != RadElement.ParentChangedEvent)
        return;
      this.UpdateImage();
      this.InvalidateMeasure();
      this.InvalidateArrange();
      if (this.Parent == null)
        return;
      this.Parent.InvalidateMeasure();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == ImagePrimitive.ImageIndexProperty && !this.GetBitState(274877906944L))
      {
        this.BitState[137438953472L] = true;
        this.BitState[274877906944L] = true;
        int newValue = (int) e.NewValue;
        if (newValue >= 0 && this.ImageList != null && newValue < this.ImageList.Images.Count)
        {
          int num = (int) this.SetValue(ImagePrimitive.ImageProperty, (object) new Bitmap(this.ImageList.Images[newValue]));
          this.BitState[1099511627776L] = true;
        }
        else
        {
          int num1 = (int) this.ResetValue(ImagePrimitive.ImageProperty, ValueResetFlags.Local);
        }
        int num2 = (int) this.ResetValue(ImagePrimitive.ImageKeyProperty, ValueResetFlags.Local);
        this.BitState[274877906944L] = false;
      }
      if (e.Property == ImagePrimitive.ImageKeyProperty && !this.GetBitState(274877906944L))
      {
        this.BitState[137438953472L] = true;
        this.BitState[274877906944L] = true;
        string newValue = (string) e.NewValue;
        if (!string.IsNullOrEmpty(newValue) && this.ImageList != null && this.ImageList.Images.IndexOfKey(newValue) >= 0)
        {
          int num = (int) this.SetValue(ImagePrimitive.ImageProperty, (object) new Bitmap(this.ImageList.Images[newValue]));
          this.BitState[1099511627776L] = true;
        }
        else
        {
          int num1 = (int) this.ResetValue(ImagePrimitive.ImageProperty, ValueResetFlags.Local);
        }
        int num2 = (int) this.ResetValue(ImagePrimitive.ImageIndexProperty, ValueResetFlags.Local);
        this.BitState[274877906944L] = false;
      }
      if (e.Property == ImagePrimitive.ImageProperty && !this.GetBitState(274877906944L) && !this.GetBitState(549755813888L))
      {
        this.BitState[137438953472L] = true;
        this.BitState[274877906944L] = true;
        this.BitState[1099511627776L] = false;
        int num1 = (int) this.ResetValue(ImagePrimitive.ImageIndexProperty, ValueResetFlags.Local);
        int num2 = (int) this.ResetValue(ImagePrimitive.ImageKeyProperty, ValueResetFlags.Local);
        this.BitState[2199023255552L] = false;
        ImageAnimator.StopAnimate(this.Image, new EventHandler(this.OnFrameChanged));
        this.BitState[274877906944L] = false;
      }
      if (e.Property == ImagePrimitive.TransparentColorProperty || e.Property == VisualElement.OpacityProperty)
        this.BitState[137438953472L] = true;
      base.OnPropertyChanged(e);
    }

    [Description("Transparent color to be used on the image")]
    [RadPropertyDefaultValue("TransparentColor", typeof (ImagePrimitive))]
    [Category("Image")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color TransparentColor
    {
      get
      {
        return (Color) this.GetValue(ImagePrimitive.TransparentColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImagePrimitive.TransparentColorProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      Size imageSize = this.GetImageSize();
      Padding padding = this.Padding;
      imageSize.Width += padding.Horizontal;
      imageSize.Height += padding.Vertical;
      return (SizeF) imageSize;
    }

    protected override void DisposeManagedResources()
    {
      if (this.cachedImage != null)
      {
        if (this.BitState[4398046511104L])
          this.cachedImage.Dispose();
        this.cachedImage = (Image) null;
      }
      base.DisposeManagedResources();
    }

    [Description("Gets or sets the type of rotate/flip to be applied.")]
    [DefaultValue(RotateFlipType.RotateNoneFlipNone)]
    public RotateFlipType RotateFlipType
    {
      get
      {
        return this.rotateFlip;
      }
      set
      {
        if (this.rotateFlip == value)
          return;
        this.rotateFlip = value;
        this.BitState[137438953472L] = true;
        this.Invalidate();
      }
    }

    private void AnimateImage(Image image)
    {
      if (!ImageAnimator.CanAnimate(image) || this.GetBitState(2199023255552L))
        return;
      ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
      this.BitState[2199023255552L] = true;
    }

    private void OnFrameChanged(object o, EventArgs e)
    {
      this.Invalidate();
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      try
      {
        Image paintImage = this.GetPaintImage();
        if (paintImage == null)
          return;
        Rectangle imageBounds = this.GetImageBounds(paintImage);
        if (imageBounds.Width <= 0 || imageBounds.Height <= 0)
          return;
        Graphics underlayGraphics = graphics.UnderlayGraphics as Graphics;
        if (graphics.Opacity < 1.0)
        {
          this.PaintWithOpacity(underlayGraphics, paintImage, imageBounds, (float) graphics.Opacity);
        }
        else
        {
          switch (this.ImageLayout)
          {
            case ImageLayout.None:
              underlayGraphics.DrawImageUnscaledAndClipped(paintImage, imageBounds);
              break;
            case ImageLayout.Tile:
              TextureBrush textureBrush = new TextureBrush(paintImage, WrapMode.Tile);
              underlayGraphics.FillRectangle((Brush) textureBrush, imageBounds);
              textureBrush.Dispose();
              break;
            default:
              underlayGraphics.DrawImage(paintImage, imageBounds);
              break;
          }
        }
      }
      catch
      {
      }
    }

    private void PaintWithOpacity(
      Graphics rawGraphics,
      Image image,
      Rectangle bounds,
      float opacity)
    {
      ImageAttributes opacityAttributes = RadGdiGraphics.GetOpacityAttributes(opacity);
      int width1 = image.Width;
      int height1 = image.Height;
      switch (this.ImageLayout)
      {
        case ImageLayout.None:
          int width2 = bounds.Width;
          int height2 = bounds.Height;
          rawGraphics.DrawImage(image, bounds, 0, 0, width2, height2, GraphicsUnit.Pixel, opacityAttributes);
          break;
        case ImageLayout.Tile:
          for (int x = bounds.X; x <= bounds.Height; x += image.Height)
          {
            for (int y = bounds.Y; y <= bounds.Width; y += image.Width)
            {
              Rectangle destRect = new Rectangle(y, x, width1, height1);
              rawGraphics.DrawImage(image, destRect, 0, 0, width1, height1, GraphicsUnit.Pixel, opacityAttributes);
            }
          }
          break;
        default:
          rawGraphics.DrawImage(image, bounds, 0, 0, width1, height1, GraphicsUnit.Pixel, opacityAttributes);
          break;
      }
      opacityAttributes.Dispose();
    }

    private Image GetPaintImage()
    {
      Image image1 = this.Image;
      if (image1 == null)
        return (Image) null;
      Image image2;
      lock (image1)
      {
        if (ImageAnimator.CanAnimate(image1))
        {
          this.AnimateImage(image1);
          ImageAnimator.UpdateFrames();
          image2 = image1;
        }
        else
        {
          if (this.BitState[137438953472L])
          {
            this.BuildImageCache(image1);
            this.BitState[137438953472L] = false;
          }
          image2 = this.cachedImage;
        }
      }
      return image2;
    }

    private Rectangle GetImageBounds(Image image)
    {
      Size imageSize = this.GetImageSize();
      Rectangle bounds = this.Bounds;
      Padding padding = this.Padding;
      int left = padding.Left;
      int top = padding.Top;
      int num1 = bounds.Width - padding.Horizontal;
      int num2 = bounds.Height - padding.Vertical;
      switch (this.ImageLayout)
      {
        case ImageLayout.None:
          num1 = Math.Min(num1, imageSize.Width);
          num2 = Math.Min(num2, imageSize.Height);
          break;
        case ImageLayout.Center:
          left += (num1 - imageSize.Width) / 2;
          top += (num2 - imageSize.Height) / 2;
          break;
        case ImageLayout.Zoom:
          float num3 = Math.Min((float) num1 / (float) imageSize.Width, (float) num2 / (float) imageSize.Height);
          int num4 = (int) ((double) imageSize.Width * (double) num3 + 0.5);
          int num5 = (int) ((double) imageSize.Height * (double) num3 + 0.5);
          left += (num1 - num4) / 2;
          top += (num2 - num5) / 2;
          num1 = num4;
          num2 = num5;
          break;
      }
      return new Rectangle(left, top, num1, num2);
    }
  }
}
