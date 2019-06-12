// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutManagerPart
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class LayoutManagerPart : VisualElementLayoutPart
  {
    private SizeF lastFinalSize = SizeF.Empty;
    private IVisualLayoutPart left;
    private IVisualLayoutPart right;
    private bool isDirty;

    public LayoutManagerPart(LightVisualElement owner)
      : base(owner)
    {
    }

    public IVisualLayoutPart LeftPart
    {
      get
      {
        return this.left;
      }
      set
      {
        this.left = value;
      }
    }

    public IVisualLayoutPart RightPart
    {
      get
      {
        return this.right;
      }
      set
      {
        this.right = value;
      }
    }

    public bool IsDirty
    {
      set
      {
        this.isDirty = value;
      }
    }

    public override SizeF Measure(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      SizeF sizeF1 = SizeF.Empty;
      SizeF sizeF2 = SizeF.Empty;
      switch (this.Owner.TextImageRelation)
      {
        case TextImageRelation.Overlay:
          if (this.left != null)
            sizeF1 = this.left.Measure(availableSize);
          if (this.right != null)
            sizeF2 = this.right.Measure(availableSize);
          empty.Height = Math.Max(sizeF1.Height, sizeF2.Height);
          empty.Width = Math.Max(sizeF1.Width, sizeF2.Width);
          break;
        case TextImageRelation.ImageAboveText:
        case TextImageRelation.TextAboveImage:
          if (this.left != null)
            sizeF1 = this.left.Measure(availableSize);
          if (this.right != null)
          {
            sizeF2 = this.right.Measure(new SizeF(availableSize.Width, availableSize.Height - sizeF1.Height));
            if (sizeF1 == (SizeF) Size.Empty && float.IsInfinity(availableSize.Width) && (this.left != null && this.Owner.Image != null) && this.Owner.ImageLayout == ImageLayout.Zoom)
            {
              sizeF1 = this.left.Measure(new SizeF(sizeF2.Width, availableSize.Height));
              sizeF2 = this.right.Measure(new SizeF(availableSize.Width - sizeF1.Width, availableSize.Height));
            }
          }
          empty.Height += sizeF1.Height;
          empty.Height += sizeF2.Height;
          empty.Width = Math.Max(sizeF1.Width, sizeF2.Width);
          break;
        case TextImageRelation.ImageBeforeText:
        case TextImageRelation.TextBeforeImage:
          if (this.left != null)
            sizeF1 = this.left.Measure(availableSize);
          if (this.right != null)
          {
            sizeF2 = this.right.Measure(new SizeF(availableSize.Width - sizeF1.Width, availableSize.Height));
            if (sizeF1 == (SizeF) Size.Empty && float.IsInfinity(availableSize.Height) && (this.left != null && this.Owner.Image != null) && this.Owner.ImageLayout == ImageLayout.Zoom)
            {
              sizeF1 = this.left.Measure(new SizeF(availableSize.Width, sizeF2.Height));
              sizeF2 = this.right.Measure(new SizeF(availableSize.Width - sizeF1.Width, availableSize.Height));
            }
          }
          empty.Width += sizeF1.Width;
          empty.Width += sizeF2.Width;
          empty.Height = Math.Max(sizeF1.Height, sizeF2.Height);
          break;
      }
      this.DesiredSize = empty;
      return empty;
    }

    public override SizeF Arrange(RectangleF bounds)
    {
      SizeF size = bounds.Size;
      if (this.lastFinalSize != size || this.isDirty)
      {
        this.lastFinalSize = size;
        this.Measure(size);
      }
      SizeF sizeF1 = size;
      RectangleF rectangleF1 = new RectangleF(bounds.Location, size);
      RectangleF empty1 = RectangleF.Empty;
      RectangleF region2 = RectangleF.Empty;
      SizeF empty2 = SizeF.Empty;
      SizeF empty3 = SizeF.Empty;
      ContentAlignment contentAlignment = this.Owner.ImageAlignment;
      TextImageRelation relation = this.Owner.TextImageRelation;
      if (this.Owner.RightToLeft)
      {
        contentAlignment = TelerikAlignHelper.RtlTranslateContent(contentAlignment);
        relation = TelerikAlignHelper.RtlTranslateRelation(relation);
      }
      if (this.left != null && this.Owner.Image == null)
      {
        this.left.Arrange(rectangleF1);
        if (this.right != null)
          this.right.Arrange(rectangleF1);
        return size;
      }
      if (this.right != null && (string.IsNullOrEmpty(this.Owner.Text) || !this.Owner.DrawText))
      {
        this.right.Arrange(rectangleF1);
        if (this.left != null)
          this.left.Arrange(rectangleF1);
        return size;
      }
      SizeF invariantLength1 = (SizeF) this.GetInvariantLength(Size.Ceiling(this.left.DesiredSize), this.left.Margin);
      SizeF invariantLength2 = (SizeF) this.GetInvariantLength(Size.Ceiling(this.right.DesiredSize), this.right.Margin);
      LayoutUtils.SubAlignedRegion(sizeF1, invariantLength1, relation);
      SizeF sizeF2 = LayoutUtils.AddAlignedRegion(invariantLength2, invariantLength1, relation);
      RectangleF empty4 = (RectangleF) Rectangle.Empty;
      empty4.Size = LayoutUtils.UnionSizes(sizeF1, sizeF2);
      empty4.X += rectangleF1.X;
      empty4.Y += rectangleF1.Y;
      bool flag1 = (TelerikAlignHelper.ImageAlignToRelation(contentAlignment) & relation) != TextImageRelation.Overlay;
      bool flag2 = (TelerikAlignHelper.TextAlignToRelation(this.Owner.TextAlignment) & relation) != TextImageRelation.Overlay;
      if (flag1)
        LayoutUtils.SplitRegion(empty4, invariantLength1, (AnchorStyles) relation, out empty1, out region2);
      else if (flag2)
        LayoutUtils.SplitRegion(empty4, invariantLength2, (AnchorStyles) LayoutUtils.GetOppositeTextImageRelation(relation), out region2, out empty1);
      else if (relation == TextImageRelation.Overlay)
      {
        LayoutUtils.SplitRegion(empty4, invariantLength1, (AnchorStyles) relation, out empty1, out region2);
      }
      else
      {
        LayoutUtils.SplitRegion(LayoutUtils.Align(sizeF2, empty4, ContentAlignment.MiddleCenter), invariantLength1, (AnchorStyles) relation, out empty1, out region2);
        LayoutUtils.ExpandRegionsToFillBounds(empty4, (AnchorStyles) relation, ref empty1, ref region2);
      }
      if (relation == TextImageRelation.TextBeforeImage || relation == TextImageRelation.ImageBeforeText)
      {
        float num = Math.Min(region2.Bottom, rectangleF1.Bottom);
        region2.Y = Math.Max(Math.Min(region2.Y, rectangleF1.Y + (float) (((double) rectangleF1.Height - (double) region2.Height) / 2.0)), rectangleF1.Y);
        region2.Height = num - region2.Y;
      }
      if (relation == TextImageRelation.TextAboveImage || relation == TextImageRelation.ImageAboveText)
      {
        float num = Math.Min(region2.Right, rectangleF1.Right);
        region2.X = Math.Max(Math.Min(region2.X, rectangleF1.X + (float) (((double) rectangleF1.Width - (double) region2.Width) / 2.0)), rectangleF1.X);
        region2.Width = num - region2.X;
      }
      if (relation == TextImageRelation.ImageBeforeText && (double) empty1.Size.Width != 0.0)
      {
        empty1.Width = Math.Max(0.0f, Math.Min(sizeF1.Width - region2.Width, empty1.Width));
        region2.X = empty1.X + empty1.Width;
      }
      if (relation == TextImageRelation.ImageAboveText && (double) empty1.Size.Height != 0.0)
      {
        empty1.Height = Math.Max(0.0f, Math.Min(sizeF1.Height - region2.Height, empty1.Height));
        region2.Y = empty1.Y + empty1.Height;
      }
      region2 = RectangleF.Intersect(region2, rectangleF1);
      RectangleF rectangleF2 = LayoutUtils.Align(invariantLength1, empty1, contentAlignment);
      if ((double) rectangleF2.Width > (double) empty1.Width)
        rectangleF2.X = empty1.Width - invariantLength1.Width;
      if ((double) rectangleF2.Height > (double) empty1.Height)
        rectangleF2.Y = empty1.Height - invariantLength1.Height;
      region2.Size = SizeF.Subtract(region2.Size, (SizeF) this.left.Margin.Size);
      empty1.Size = SizeF.Subtract(empty1.Size, (SizeF) this.right.Margin.Size);
      this.left.Arrange(empty1);
      this.right.Arrange(region2);
      return size;
    }

    private SizeF GetMinSizeLen(SizeF size1, SizeF size2)
    {
      if ((double) size1.Width > (double) size2.Width)
        return size2;
      return size1;
    }

    private Size GetInvariantLength(Size size, Padding margin)
    {
      size.Height += margin.Vertical;
      size.Width += margin.Horizontal;
      return size;
    }
  }
}
