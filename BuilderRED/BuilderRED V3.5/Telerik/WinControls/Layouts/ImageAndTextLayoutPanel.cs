// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.ImageAndTextLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.Layouts
{
  public class ImageAndTextLayoutPanel : LayoutPanel
  {
    public static RadProperty ImageAlignmentProperty = RadProperty.Register(nameof (ImageAlignment), typeof (ContentAlignment), typeof (ImageAndTextLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty TextAlignmentProperty = RadProperty.Register(nameof (TextAlignment), typeof (ContentAlignment), typeof (ImageAndTextLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty TextImageRelationProperty = RadProperty.Register(nameof (TextImageRelation), typeof (TextImageRelation), typeof (ImageAndTextLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextImageRelation.Overlay, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty IsImagePrimitiveProperty = RadProperty.Register("IsImagePrimitive", typeof (bool), typeof (ImageAndTextLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty IsTextPrimitiveProperty = RadProperty.Register("IsTextPrimitive", typeof (bool), typeof (ImageAndTextLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty DisplayStyleProperty = RadProperty.Register(nameof (DisplayStyle), typeof (DisplayStyle), typeof (ImageAndTextLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DisplayStyle.ImageAndText, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    private RadElement imageElement;
    private RadElement textElement;

    protected override void DisposeManagedResources()
    {
      this.imageElement = (RadElement) null;
      this.textElement = (RadElement) null;
      base.DisposeManagedResources();
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("ImageAlignment", typeof (ImageAndTextLayoutPanel))]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(ImageAndTextLayoutPanel.ImageAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImageAndTextLayoutPanel.ImageAlignmentProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("TextAlignment", typeof (ImageAndTextLayoutPanel))]
    public ContentAlignment TextAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(ImageAndTextLayoutPanel.TextAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImageAndTextLayoutPanel.TextAlignmentProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TextImageRelation", typeof (ImageAndTextLayoutPanel))]
    [Category("Behavior")]
    public TextImageRelation TextImageRelation
    {
      get
      {
        return (TextImageRelation) this.GetValue(ImageAndTextLayoutPanel.TextImageRelationProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImageAndTextLayoutPanel.TextImageRelationProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("DisplayStyle", typeof (ImageAndTextLayoutPanel))]
    [Category("Behavior")]
    public DisplayStyle DisplayStyle
    {
      get
      {
        return (DisplayStyle) this.GetValue(ImageAndTextLayoutPanel.DisplayStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(ImageAndTextLayoutPanel.DisplayStyleProperty, (object) value);
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.SetImageAndTextAlignment();
      this.SetTextAndImageVisibility();
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      base.OnChildrenChanged(child, changeOperation);
      switch (changeOperation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          if (this.IsTextElement(child))
          {
            this.textElement = child;
            break;
          }
          if (this.IsImageElement(child))
          {
            this.imageElement = child;
            break;
          }
          break;
        case ItemsChangeOperation.Removed:
          if (this.IsTextElement(child))
          {
            this.textElement = (RadElement) null;
            break;
          }
          if (this.IsImageElement(child))
          {
            this.imageElement = (RadElement) null;
            break;
          }
          break;
        case ItemsChangeOperation.Cleared:
          this.textElement = (RadElement) null;
          this.imageElement = (RadElement) null;
          break;
      }
      this.SetTextAndImageVisibility();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.ElementState != ElementState.Loaded)
        return;
      if (e.Property == ImageAndTextLayoutPanel.DisplayStyleProperty)
      {
        this.SetTextAndImageVisibility();
      }
      else
      {
        if (e.Property != ImageAndTextLayoutPanel.ImageAlignmentProperty && e.Property != ImageAndTextLayoutPanel.TextAlignmentProperty)
          return;
        this.SetImageAndTextAlignment();
      }
    }

    private bool IsPrimitiveNullOrEmpty(RadElement element)
    {
      if (element == null || element.Visibility == ElementVisibility.Collapsed)
        return true;
      BasePrimitive basePrimitive = element as BasePrimitive;
      if (basePrimitive != null)
        return basePrimitive.IsEmpty;
      return false;
    }

    private bool IsImageElement(RadElement element)
    {
      if (element != null)
        return (bool) element.GetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty);
      return false;
    }

    private bool IsTextElement(RadElement element)
    {
      if (element != null)
        return (bool) element.GetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty);
      return false;
    }

    private SizeF CombineSizes(SizeF textSize, SizeF imageSize)
    {
      SizeF sizeF = LayoutUtils.UnionSizes(textSize, imageSize);
      if (LayoutUtils.IsVerticalRelation(this.TextImageRelation))
        sizeF.Height = textSize.Height + imageSize.Height;
      else if (LayoutUtils.IsHorizontalRelation(this.TextImageRelation))
        sizeF.Width = textSize.Width + imageSize.Width;
      return sizeF;
    }

    private void SetTextAndImageVisibility()
    {
      ElementVisibility elementVisibility1 = ElementVisibility.Visible;
      ElementVisibility elementVisibility2 = ElementVisibility.Visible;
      switch (this.DisplayStyle)
      {
        case DisplayStyle.None:
          elementVisibility1 = ElementVisibility.Collapsed;
          elementVisibility2 = ElementVisibility.Collapsed;
          break;
        case DisplayStyle.Text:
          elementVisibility1 = ElementVisibility.Visible;
          elementVisibility2 = ElementVisibility.Collapsed;
          break;
        case DisplayStyle.Image:
          elementVisibility1 = ElementVisibility.Collapsed;
          elementVisibility2 = ElementVisibility.Visible;
          break;
        case DisplayStyle.ImageAndText:
          elementVisibility1 = ElementVisibility.Visible;
          elementVisibility2 = ElementVisibility.Visible;
          break;
      }
      if (this.textElement != null)
        this.textElement.Visibility = elementVisibility1;
      if (this.imageElement == null)
        return;
      this.imageElement.Visibility = elementVisibility2;
    }

    private void SetImageAndTextAlignment()
    {
      if (this.textElement != null)
      {
        this.textElement.Alignment = this.TextAlignment;
        TextPrimitive textElement = this.textElement as TextPrimitive;
        if (textElement != null)
          textElement.TextAlignment = this.TextAlignment;
      }
      if (this.imageElement == null)
        return;
      this.imageElement.Alignment = this.ImageAlignment;
    }

    private Size GetInvariantLength(Size size, Padding margin)
    {
      size.Height += margin.Vertical;
      size.Width += margin.Horizontal;
      return size;
    }

    private SizeF GetInvariantLength(SizeF size, Padding margin)
    {
      size.Height += (float) margin.Vertical;
      size.Width += (float) margin.Horizontal;
      return size;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      this.IsPrimitiveNullOrEmpty(this.imageElement);
      this.IsPrimitiveNullOrEmpty(this.textElement);
      switch (this.TextImageRelation)
      {
        case TextImageRelation.Overlay:
          if (this.imageElement != null)
          {
            this.imageElement.Measure(availableSize);
            empty.Width = Math.Max(empty.Width, this.imageElement.DesiredSize.Width);
            empty.Height = Math.Max(empty.Height, this.imageElement.DesiredSize.Height);
          }
          if (this.textElement != null)
          {
            this.textElement.Measure(availableSize);
            empty.Width = Math.Max(empty.Width, this.textElement.DesiredSize.Width);
            empty.Height = Math.Max(empty.Height, this.textElement.DesiredSize.Height);
            break;
          }
          break;
        case TextImageRelation.ImageAboveText:
        case TextImageRelation.TextAboveImage:
          if (this.imageElement != null && this.DisplayStyle != DisplayStyle.Text)
          {
            this.imageElement.Measure(availableSize);
            empty.Height += this.imageElement.DesiredSize.Height;
            empty.Width = Math.Max(empty.Width, this.imageElement.DesiredSize.Width);
          }
          if (this.textElement != null && this.DisplayStyle != DisplayStyle.Image)
          {
            this.textElement.Measure(new SizeF(availableSize.Width, availableSize.Height - (this.imageElement != null ? this.imageElement.DesiredSize.Height : 0.0f)));
            empty.Height += this.textElement.DesiredSize.Height;
            empty.Width = Math.Max(empty.Width, this.textElement.DesiredSize.Width);
            break;
          }
          break;
        case TextImageRelation.ImageBeforeText:
        case TextImageRelation.TextBeforeImage:
          if (this.imageElement != null && this.DisplayStyle != DisplayStyle.Text)
          {
            this.imageElement.Measure(availableSize);
            empty.Width += this.imageElement.DesiredSize.Width;
            empty.Height = Math.Max(empty.Height, this.imageElement.DesiredSize.Height);
          }
          if (this.textElement != null && this.DisplayStyle != DisplayStyle.Image)
          {
            this.textElement.Measure(new SizeF(availableSize.Width - (this.imageElement != null ? this.imageElement.DesiredSize.Width : 0.0f), availableSize.Height));
            empty.Width += this.textElement.DesiredSize.Width;
            empty.Height = Math.Max(empty.Height, this.textElement.DesiredSize.Height);
            break;
          }
          break;
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF empty1 = RectangleF.Empty;
      RectangleF region2 = RectangleF.Empty;
      SizeF empty2 = SizeF.Empty;
      SizeF empty3 = SizeF.Empty;
      ContentAlignment contentAlignment1 = this.ImageAlignment;
      ContentAlignment contentAlignment2 = this.TextAlignment;
      TextImageRelation relation = this.TextImageRelation;
      if (this.RightToLeft)
      {
        contentAlignment1 = TelerikAlignHelper.RtlTranslateContent(contentAlignment1);
        contentAlignment2 = TelerikAlignHelper.RtlTranslateContent(contentAlignment2);
        relation = TelerikAlignHelper.RtlTranslateRelation(relation);
      }
      bool flag1 = this.IsPrimitiveNullOrEmpty(this.imageElement);
      bool flag2 = this.IsPrimitiveNullOrEmpty(this.textElement);
      if (this.imageElement != null && (this.DisplayStyle == DisplayStyle.Image || flag2))
      {
        this.imageElement.Arrange(clientRectangle);
        return finalSize;
      }
      if (this.textElement != null && this.DisplayStyle == DisplayStyle.Text && flag1)
      {
        this.textElement.Arrange(clientRectangle);
        return finalSize;
      }
      SizeF sizeF1 = SizeF.Empty;
      SizeF sizeF2 = SizeF.Empty;
      if (this.imageElement != null)
        sizeF1 = (SizeF) this.GetInvariantLength(Size.Ceiling(this.imageElement.DesiredSize), this.imageElement.Margin);
      if (this.textElement != null)
        sizeF2 = (SizeF) this.GetInvariantLength(Size.Ceiling(this.textElement.DesiredSize), this.textElement.Margin);
      LayoutUtils.SubAlignedRegion(finalSize, sizeF1, relation);
      SizeF sizeF3 = LayoutUtils.AddAlignedRegion(sizeF2, sizeF1, relation);
      RectangleF empty4 = (RectangleF) Rectangle.Empty;
      empty4.Size = LayoutUtils.UnionSizes(finalSize, sizeF3);
      empty4.X += clientRectangle.X;
      empty4.Y += clientRectangle.Y;
      bool flag3 = (TelerikAlignHelper.ImageAlignToRelation(contentAlignment1) & relation) != TextImageRelation.Overlay;
      bool flag4 = (TelerikAlignHelper.TextAlignToRelation(contentAlignment2) & relation) != TextImageRelation.Overlay;
      if (flag3)
        LayoutUtils.SplitRegion(empty4, sizeF1, (AnchorStyles) relation, out empty1, out region2);
      else if (flag4)
        LayoutUtils.SplitRegion(empty4, sizeF2, (AnchorStyles) LayoutUtils.GetOppositeTextImageRelation(relation), out region2, out empty1);
      else if (relation == TextImageRelation.Overlay)
      {
        LayoutUtils.SplitRegion(empty4, sizeF1, (AnchorStyles) relation, out empty1, out region2);
      }
      else
      {
        LayoutUtils.SplitRegion(LayoutUtils.Align(sizeF3, empty4, ContentAlignment.MiddleCenter), sizeF1, (AnchorStyles) relation, out empty1, out region2);
        LayoutUtils.ExpandRegionsToFillBounds(empty4, (AnchorStyles) relation, ref empty1, ref region2);
      }
      if (relation == TextImageRelation.TextBeforeImage || relation == TextImageRelation.ImageBeforeText)
      {
        float num = Math.Min(region2.Bottom, clientRectangle.Bottom);
        region2.Y = Math.Max(Math.Min(region2.Y, clientRectangle.Y + (float) (((double) clientRectangle.Height - (double) region2.Height) / 2.0)), clientRectangle.Y);
        region2.Height = num - region2.Y;
      }
      if (relation == TextImageRelation.TextAboveImage || relation == TextImageRelation.ImageAboveText)
      {
        float num = Math.Min(region2.Right, clientRectangle.Right);
        region2.X = Math.Max(Math.Min(region2.X, clientRectangle.X + (float) (((double) clientRectangle.Width - (double) region2.Width) / 2.0)), clientRectangle.X);
        region2.Width = num - region2.X;
      }
      if (relation == TextImageRelation.ImageBeforeText && (double) empty1.Size.Width != 0.0)
      {
        empty1.Width = Math.Max(0.0f, Math.Min(finalSize.Width - region2.Width, empty1.Width));
        region2.X = empty1.X + empty1.Width;
      }
      if (relation == TextImageRelation.ImageAboveText && (double) empty1.Size.Height != 0.0)
      {
        empty1.Height = Math.Max(0.0f, Math.Min(finalSize.Height - region2.Height, empty1.Height));
        region2.Y = empty1.Y + empty1.Height;
      }
      region2 = RectangleF.Intersect(region2, clientRectangle);
      RectangleF rectangleF = LayoutUtils.Align(sizeF1, empty1, contentAlignment1);
      if ((double) rectangleF.Width > (double) empty1.Width)
        rectangleF.X = empty1.Width - sizeF1.Width;
      if ((double) rectangleF.Height > (double) empty1.Height)
        rectangleF.Y = empty1.Height - sizeF1.Height;
      region2 = LayoutUtils.Align(sizeF2, region2, contentAlignment2);
      if (this.imageElement != null)
      {
        region2.Size = SizeF.Subtract(region2.Size, (SizeF) this.imageElement.Margin.Size);
        this.imageElement.Arrange(empty1);
      }
      if (this.textElement != null)
      {
        empty1.Size = SizeF.Subtract(empty1.Size, (SizeF) this.textElement.Margin.Size);
        this.textElement.Arrange(region2);
      }
      return finalSize;
    }
  }
}
