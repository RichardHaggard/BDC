// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.LayoutUtils
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Layouts
{
  public class LayoutUtils
  {
    public static Size InvalidSize = new Size(int.MinValue, int.MinValue);
    public static Padding InvalidPadding = new Padding(int.MinValue, int.MinValue, int.MinValue, int.MinValue);
    public static Rectangle InvalidBounds = new Rectangle(int.MinValue, int.MinValue, int.MinValue, int.MinValue);
    public static readonly SizeF InfinitySize = new SizeF(float.PositiveInfinity, float.PositiveInfinity);
    public static readonly SizeF MaxSizeF = new SizeF(float.MaxValue, float.MaxValue);
    public static readonly Size MaxSize = new Size(int.MaxValue, int.MaxValue);
    public static readonly Rectangle MaxRectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);
    private static readonly AnchorStyles[] dockingToAnchor = new AnchorStyles[6]{ AnchorStyles.Top | AnchorStyles.Left, AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left, AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right, AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right };
    public static readonly string TestString = "j^";
    public const ContentAlignment AnyBottom = ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
    public const ContentAlignment AnyCenter = ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
    public const ContentAlignment AnyLeft = ContentAlignment.TopLeft | ContentAlignment.MiddleLeft | ContentAlignment.BottomLeft;
    public const ContentAlignment AnyMiddle = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;
    public const ContentAlignment AnyRight = ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
    public const ContentAlignment AnyTop = ContentAlignment.TopLeft | ContentAlignment.TopCenter | ContentAlignment.TopRight;
    public const AnchorStyles HorizontalAnchorStyles = AnchorStyles.Left | AnchorStyles.Right;
    public const AnchorStyles VerticalAnchorStyles = AnchorStyles.Top | AnchorStyles.Bottom;

    public static Padding RotateMargin(Padding margin, int angle)
    {
      if (angle < 0)
        angle = 360 + angle;
      switch (angle)
      {
        case 90:
          margin = new Padding(margin.Bottom, margin.Left, margin.Top, margin.Right);
          break;
        case 180:
          margin = new Padding(margin.Right, margin.Bottom, margin.Left, margin.Top);
          break;
        case 270:
          margin = new Padding(margin.Top, margin.Right, margin.Bottom, margin.Left);
          break;
      }
      return margin;
    }

    public static Size AddAlignedRegion(
      Size textSize,
      Size imageSize,
      TextImageRelation relation)
    {
      if (relation == TextImageRelation.Overlay)
        return new Size(Math.Max(textSize.Width, imageSize.Width), Math.Max(textSize.Height, imageSize.Height));
      return LayoutUtils.AddAlignedRegionCore(textSize, imageSize, LayoutUtils.IsVerticalRelation(relation));
    }

    public static SizeF AddAlignedRegion(
      SizeF textSize,
      SizeF imageSize,
      TextImageRelation relation)
    {
      if (relation == TextImageRelation.Overlay)
        return new SizeF(Math.Max(textSize.Width, imageSize.Width), Math.Max(textSize.Height, imageSize.Height));
      return LayoutUtils.AddAlignedRegionCore(textSize, imageSize, LayoutUtils.IsVerticalRelation(relation));
    }

    public static Size AddAlignedRegionCore(Size currentSize, Size contentSize, bool vertical)
    {
      if (vertical)
      {
        currentSize.Width = Math.Max(currentSize.Width, contentSize.Width);
        currentSize.Height += contentSize.Height;
        return currentSize;
      }
      currentSize.Width += contentSize.Width;
      currentSize.Height = Math.Max(currentSize.Height, contentSize.Height);
      return currentSize;
    }

    public static SizeF AddAlignedRegionCore(
      SizeF currentSize,
      SizeF contentSize,
      bool vertical)
    {
      if (vertical)
      {
        currentSize.Width = Math.Max(currentSize.Width, contentSize.Width);
        currentSize.Height += contentSize.Height;
        return currentSize;
      }
      currentSize.Width += contentSize.Width;
      currentSize.Height = Math.Max(currentSize.Height, contentSize.Height);
      return currentSize;
    }

    public static Rectangle Align(
      Size alignThis,
      Rectangle withinThis,
      ContentAlignment align)
    {
      return LayoutUtils.VAlign(alignThis, LayoutUtils.HAlign(alignThis, withinThis, align), align);
    }

    public static RectangleF Align(
      SizeF alignThis,
      RectangleF withinThis,
      ContentAlignment align)
    {
      return LayoutUtils.VAlign(alignThis, LayoutUtils.HAlign(alignThis, withinThis, align), align);
    }

    public static Rectangle Align(
      Size alignThis,
      Rectangle withinThis,
      AnchorStyles anchorStyles)
    {
      return LayoutUtils.VAlign(alignThis, LayoutUtils.HAlign(alignThis, withinThis, anchorStyles), anchorStyles);
    }

    public static Rectangle AlignAndStretch(
      Size fitThis,
      Rectangle withinThis,
      AnchorStyles anchorStyles)
    {
      return LayoutUtils.Align(LayoutUtils.Stretch(fitThis, withinThis.Size, anchorStyles), withinThis, anchorStyles);
    }

    public static bool AreWidthAndHeightLarger(Size size1, Size size2)
    {
      if (size1.Width >= size2.Width)
        return size1.Height >= size2.Height;
      return false;
    }

    public static Padding ClampNegativePaddingToZero(Padding padding)
    {
      if (padding.All < 0)
      {
        padding.Left = Math.Max(0, padding.Left);
        padding.Top = Math.Max(0, padding.Top);
        padding.Right = Math.Max(0, padding.Right);
        padding.Bottom = Math.Max(0, padding.Bottom);
      }
      return padding;
    }

    public static int ContentAlignmentToIndex(ContentAlignment alignment)
    {
      int index1 = (int) LayoutUtils.xContentAlignmentToIndex((int) (alignment & (ContentAlignment) 15));
      int index2 = (int) LayoutUtils.xContentAlignmentToIndex((int) alignment >> 4 & 15);
      int index3 = (int) LayoutUtils.xContentAlignmentToIndex((int) alignment >> 8 & 15);
      return ((index2 != 0 ? 4 : 0) | (index3 != 0 ? 8 : 0) | index1 | index2 | index3) - 1;
    }

    public static Size ConvertZeroToUnbounded(Size size)
    {
      if (size.Width == 0)
        size.Width = int.MaxValue;
      if (size.Height == 0)
        size.Height = int.MaxValue;
      return size;
    }

    public static Rectangle DeflateRect(Rectangle rect, Padding padding)
    {
      rect.X += padding.Left;
      rect.Y += padding.Top;
      rect.Width -= padding.Horizontal;
      rect.Height -= padding.Vertical;
      return rect;
    }

    public static RectangleF DeflateRect(RectangleF rect, Padding padding)
    {
      rect.X += (float) padding.Left;
      rect.Y += (float) padding.Top;
      rect.Width -= (float) padding.Horizontal;
      rect.Height -= (float) padding.Vertical;
      return rect;
    }

    public static void ExpandRegionsToFillBounds(
      Rectangle bounds,
      AnchorStyles region1Align,
      ref Rectangle region1,
      ref Rectangle region2)
    {
      switch (region1Align)
      {
        case AnchorStyles.Top:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Bottom);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Top);
          break;
        case AnchorStyles.Bottom:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Top);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Bottom);
          break;
        case AnchorStyles.Left:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Right);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Left);
          break;
        case AnchorStyles.Right:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Left);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Right);
          break;
      }
    }

    public static void ExpandRegionsToFillBounds(
      RectangleF bounds,
      AnchorStyles region1Align,
      ref RectangleF region1,
      ref RectangleF region2)
    {
      switch (region1Align)
      {
        case AnchorStyles.Top:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Bottom);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Top);
          break;
        case AnchorStyles.Bottom:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Top);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Bottom);
          break;
        case AnchorStyles.Left:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Right);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Left);
          break;
        case AnchorStyles.Right:
          region1 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region1, AnchorStyles.Left);
          region2 = LayoutUtils.SubstituteSpecifiedBounds(bounds, region2, AnchorStyles.Right);
          break;
      }
    }

    public static Padding FlipPadding(Padding padding)
    {
      if (padding.All == -1)
      {
        int top = padding.Top;
        padding.Top = padding.Left;
        padding.Left = top;
        int bottom = padding.Bottom;
        padding.Bottom = padding.Right;
        padding.Right = bottom;
      }
      return padding;
    }

    public static Point FlipPoint(Point point)
    {
      int x = point.X;
      point.X = point.Y;
      point.Y = x;
      return point;
    }

    public static Rectangle FlipRectangle(Rectangle rect)
    {
      rect.Location = LayoutUtils.FlipPoint(rect.Location);
      rect.Size = LayoutUtils.FlipSize(rect.Size);
      return rect;
    }

    public static Rectangle FlipRectangleIf(bool condition, Rectangle rect)
    {
      if (!condition)
        return rect;
      return LayoutUtils.FlipRectangle(rect);
    }

    public static Size FlipSize(Size size)
    {
      int width = size.Width;
      size.Width = size.Height;
      size.Height = width;
      return size;
    }

    public static SizeF FlipSize(SizeF size)
    {
      float width = size.Width;
      size.Width = size.Height;
      size.Height = width;
      return size;
    }

    public static Size FlipSizeIf(bool condition, Size size)
    {
      if (!condition)
        return size;
      return LayoutUtils.FlipSize(size);
    }

    public static SizeF FlipSizeIf(bool condition, SizeF size)
    {
      if (!condition)
        return size;
      return LayoutUtils.FlipSize(size);
    }

    private static AnchorStyles GetOppositeAnchor(AnchorStyles anchor)
    {
      AnchorStyles anchorStyles = AnchorStyles.None;
      if (anchor != AnchorStyles.None)
      {
        for (int index = 1; index <= 8; index <<= 1)
        {
          switch (anchor & (AnchorStyles) index)
          {
            case AnchorStyles.Top:
              anchorStyles |= AnchorStyles.Bottom;
              break;
            case AnchorStyles.Bottom:
              anchorStyles |= AnchorStyles.Top;
              break;
            case AnchorStyles.Left:
              anchorStyles |= AnchorStyles.Right;
              break;
            case AnchorStyles.Right:
              anchorStyles |= AnchorStyles.Left;
              break;
          }
        }
      }
      return anchorStyles;
    }

    public static TextImageRelation GetOppositeTextImageRelation(
      TextImageRelation relation)
    {
      return (TextImageRelation) LayoutUtils.GetOppositeAnchor((AnchorStyles) relation);
    }

    public static bool IsRightAlignment(ContentAlignment align)
    {
      return (align & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != (ContentAlignment) 0;
    }

    public static Rectangle HAlign(
      Size alignThis,
      Rectangle withinThis,
      ContentAlignment align)
    {
      if ((align & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
        withinThis.X += withinThis.Width - alignThis.Width;
      else if ((align & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != (ContentAlignment) 0)
        withinThis.X += (withinThis.Width - alignThis.Width) / 2;
      withinThis.Width = alignThis.Width;
      return withinThis;
    }

    public static RectangleF HAlign(
      SizeF alignThis,
      RectangleF withinThis,
      ContentAlignment align)
    {
      if ((align & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
        withinThis.X += withinThis.Width - alignThis.Width;
      else if ((align & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != (ContentAlignment) 0)
        withinThis.X += (float) (((double) withinThis.Width - (double) alignThis.Width) / 2.0);
      withinThis.Width = alignThis.Width;
      return withinThis;
    }

    public static Rectangle HAlign(
      Size alignThis,
      Rectangle withinThis,
      AnchorStyles anchorStyles)
    {
      if ((anchorStyles & AnchorStyles.Right) != AnchorStyles.None)
        withinThis.X += withinThis.Width - alignThis.Width;
      else if (anchorStyles == AnchorStyles.None || (anchorStyles & (AnchorStyles.Left | AnchorStyles.Right)) == AnchorStyles.None)
        withinThis.X += (withinThis.Width - alignThis.Width) / 2;
      withinThis.Width = alignThis.Width;
      return withinThis;
    }

    public static Rectangle InflateRect(Rectangle rect, Padding padding)
    {
      rect.X -= padding.Left;
      rect.Y -= padding.Top;
      rect.Width += padding.Horizontal;
      rect.Height += padding.Vertical;
      return rect;
    }

    public static Size IntersectSizes(Size a, Size b)
    {
      return new Size(Math.Min(a.Width, b.Width), Math.Min(a.Height, b.Height));
    }

    public static bool IsHorizontalAlignment(ContentAlignment align)
    {
      return !LayoutUtils.IsVerticalAlignment(align);
    }

    public static bool IsHorizontalRelation(TextImageRelation relation)
    {
      return (relation & (TextImageRelation.ImageBeforeText | TextImageRelation.TextBeforeImage)) != TextImageRelation.Overlay;
    }

    public static bool IsIntersectHorizontally(Rectangle rect1, Rectangle rect2)
    {
      return rect1.IntersectsWith(rect2) && (rect1.X <= rect2.X && rect1.X + rect1.Width >= rect2.X + rect2.Width || rect2.X <= rect1.X && rect2.X + rect2.Width >= rect1.X + rect1.Width);
    }

    public static bool IsIntersectVertically(Rectangle rect1, Rectangle rect2)
    {
      return rect1.IntersectsWith(rect2) && (rect1.Y <= rect2.Y && rect1.Y + rect1.Width >= rect2.Y + rect2.Width || rect2.Y <= rect1.Y && rect2.Y + rect2.Width >= rect1.Y + rect1.Width);
    }

    public static bool IsVerticalAlignment(ContentAlignment align)
    {
      return (align & (ContentAlignment.TopCenter | ContentAlignment.BottomCenter)) != (ContentAlignment) 0;
    }

    public static bool IsVerticalRelation(TextImageRelation relation)
    {
      return (relation & (TextImageRelation.ImageAboveText | TextImageRelation.TextAboveImage)) != TextImageRelation.Overlay;
    }

    public static bool IsZeroWidthOrHeight(Rectangle rectangle)
    {
      if (rectangle.Width != 0)
        return rectangle.Height == 0;
      return true;
    }

    public static bool IsZeroWidthOrHeight(Size size)
    {
      if (size.Width != 0)
        return size.Height == 0;
      return true;
    }

    public static Size OldGetLargestStringSizeInCollection(Font font, ICollection objects)
    {
      Size empty = Size.Empty;
      if (objects != null)
      {
        foreach (object obj in (IEnumerable) objects)
        {
          Size size = TextRenderer.MeasureText(obj.ToString(), font, new Size((int) short.MaxValue, (int) short.MaxValue), TextFormatFlags.SingleLine);
          empty.Width = Math.Max(empty.Width, size.Width);
          empty.Height = Math.Max(empty.Height, size.Height);
        }
      }
      return empty;
    }

    public static Rectangle RTLTranslate(Rectangle bounds, Rectangle withinBounds)
    {
      bounds.X = withinBounds.Width - bounds.Right;
      return bounds;
    }

    public static RectangleF RTLTranslate(RectangleF bounds, RectangleF withinBounds)
    {
      bounds.X = withinBounds.Width - bounds.Right;
      return bounds;
    }

    public static RectangleF RTLTranslateNonRelative(
      RectangleF bounds,
      RectangleF withinBounds)
    {
      bounds.X = withinBounds.Right - bounds.Right + withinBounds.X;
      return bounds;
    }

    public static Rectangle RTLTranslateNonRelative(
      Rectangle bounds,
      Rectangle withinBounds)
    {
      bounds.X = withinBounds.Width - bounds.Right + withinBounds.X;
      return bounds;
    }

    public static double GetDistance(Point from, Point to)
    {
      return Math.Sqrt((double) ((from.X - to.X) * (from.X - to.X) + (from.Y - to.Y) * (from.Y - to.Y)));
    }

    public static void SplitRegion(
      Rectangle bounds,
      Size specifiedContent,
      AnchorStyles region1Align,
      out Rectangle region1,
      out Rectangle region2)
    {
      Rectangle rectangle;
      region2 = rectangle = bounds;
      region1 = rectangle;
      switch (region1Align)
      {
        case AnchorStyles.Top:
          region1.Height = specifiedContent.Height;
          region2.Y += specifiedContent.Height;
          region2.Height -= specifiedContent.Height;
          break;
        case AnchorStyles.Bottom:
          region1.Y += bounds.Height - specifiedContent.Height;
          region1.Height = specifiedContent.Height;
          region2.Height -= specifiedContent.Height;
          break;
        case AnchorStyles.Left:
          region1.Width = specifiedContent.Width;
          region2.X += specifiedContent.Width;
          region2.Width -= specifiedContent.Width;
          break;
        case AnchorStyles.Right:
          region1.X += bounds.Width - specifiedContent.Width;
          region1.Width = specifiedContent.Width;
          region2.Width -= specifiedContent.Width;
          break;
      }
    }

    public static void SplitRegion(
      RectangleF bounds,
      SizeF specifiedContent,
      AnchorStyles region1Align,
      out RectangleF region1,
      out RectangleF region2)
    {
      RectangleF rectangleF;
      region2 = rectangleF = bounds;
      region1 = rectangleF;
      switch (region1Align)
      {
        case AnchorStyles.Top:
          region1.Height = specifiedContent.Height;
          region2.Y += specifiedContent.Height;
          region2.Height -= specifiedContent.Height;
          break;
        case AnchorStyles.Bottom:
          region1.Y += bounds.Height - specifiedContent.Height;
          region1.Height = specifiedContent.Height;
          region2.Height -= specifiedContent.Height;
          break;
        case AnchorStyles.Left:
          region1.Width = specifiedContent.Width;
          region2.X += specifiedContent.Width;
          region2.Width -= specifiedContent.Width;
          break;
        case AnchorStyles.Right:
          region1.X += bounds.Width - specifiedContent.Width;
          region1.Width = specifiedContent.Width;
          region2.Width -= specifiedContent.Width;
          break;
      }
    }

    public static Size Stretch(Size stretchThis, Size withinThis, AnchorStyles anchorStyles)
    {
      Size size = new Size((anchorStyles & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right) ? withinThis.Width : stretchThis.Width, (anchorStyles & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom) ? withinThis.Height : stretchThis.Height);
      if (size.Width > withinThis.Width)
        size.Width = withinThis.Width;
      if (size.Height > withinThis.Height)
        size.Height = withinThis.Height;
      return size;
    }

    public static Size SubAlignedRegion(
      Size currentSize,
      Size contentSize,
      TextImageRelation relation)
    {
      if (relation == TextImageRelation.Overlay)
        return currentSize;
      return LayoutUtils.SubAlignedRegionCore(currentSize, contentSize, LayoutUtils.IsVerticalRelation(relation));
    }

    public static SizeF SubAlignedRegion(
      SizeF currentSize,
      SizeF contentSize,
      TextImageRelation relation)
    {
      if (relation == TextImageRelation.Overlay)
        return currentSize;
      return LayoutUtils.SubAlignedRegionCore(currentSize, contentSize, LayoutUtils.IsVerticalRelation(relation));
    }

    public static Size SubAlignedRegionCore(Size currentSize, Size contentSize, bool vertical)
    {
      if (vertical)
      {
        currentSize.Height -= contentSize.Height;
        return currentSize;
      }
      currentSize.Width -= contentSize.Width;
      return currentSize;
    }

    public static SizeF SubAlignedRegionCore(
      SizeF currentSize,
      SizeF contentSize,
      bool vertical)
    {
      if (vertical)
      {
        currentSize.Height -= contentSize.Height;
        return currentSize;
      }
      currentSize.Width -= contentSize.Width;
      return currentSize;
    }

    private static Rectangle SubstituteSpecifiedBounds(
      Rectangle originalBounds,
      Rectangle substitutionBounds,
      AnchorStyles specified)
    {
      return Rectangle.FromLTRB((specified & AnchorStyles.Left) != AnchorStyles.None ? substitutionBounds.Left : originalBounds.Left, (specified & AnchorStyles.Top) != AnchorStyles.None ? substitutionBounds.Top : originalBounds.Top, (specified & AnchorStyles.Right) != AnchorStyles.None ? substitutionBounds.Right : originalBounds.Right, (specified & AnchorStyles.Bottom) != AnchorStyles.None ? substitutionBounds.Bottom : originalBounds.Bottom);
    }

    private static RectangleF SubstituteSpecifiedBounds(
      RectangleF originalBounds,
      RectangleF substitutionBounds,
      AnchorStyles specified)
    {
      return RectangleF.FromLTRB((specified & AnchorStyles.Left) != AnchorStyles.None ? substitutionBounds.Left : originalBounds.Left, (specified & AnchorStyles.Top) != AnchorStyles.None ? substitutionBounds.Top : originalBounds.Top, (specified & AnchorStyles.Right) != AnchorStyles.None ? substitutionBounds.Right : originalBounds.Right, (specified & AnchorStyles.Bottom) != AnchorStyles.None ? substitutionBounds.Bottom : originalBounds.Bottom);
    }

    public static Size UnionSizes(Size a, Size b)
    {
      return new Size(Math.Max(a.Width, b.Width), Math.Max(a.Height, b.Height));
    }

    public static SizeF UnionSizes(SizeF a, SizeF b)
    {
      return new SizeF(Math.Max(a.Width, b.Width), Math.Max(a.Height, b.Height));
    }

    public static Rectangle VAlign(
      Size alignThis,
      Rectangle withinThis,
      ContentAlignment align)
    {
      if ((align & (ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
        withinThis.Y += withinThis.Height - alignThis.Height;
      else if ((align & (ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight)) != (ContentAlignment) 0)
        withinThis.Y += (withinThis.Height - alignThis.Height) / 2;
      withinThis.Height = alignThis.Height;
      return withinThis;
    }

    public static RectangleF VAlign(
      SizeF alignThis,
      RectangleF withinThis,
      ContentAlignment align)
    {
      if ((align & (ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
        withinThis.Y += withinThis.Height - alignThis.Height;
      else if ((align & (ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight)) != (ContentAlignment) 0)
        withinThis.Y += (float) (((double) withinThis.Height - (double) alignThis.Height) / 2.0);
      withinThis.Height = alignThis.Height;
      return withinThis;
    }

    public static Rectangle VAlign(
      Size alignThis,
      Rectangle withinThis,
      AnchorStyles anchorStyles)
    {
      if ((anchorStyles & AnchorStyles.Bottom) != AnchorStyles.None)
        withinThis.Y += withinThis.Height - alignThis.Height;
      else if (anchorStyles == AnchorStyles.None || (anchorStyles & (AnchorStyles.Top | AnchorStyles.Bottom)) == AnchorStyles.None)
        withinThis.Y += (withinThis.Height - alignThis.Height) / 2;
      withinThis.Height = alignThis.Height;
      return withinThis;
    }

    private static byte xContentAlignmentToIndex(int threeBitFlag)
    {
      if (threeBitFlag != 4)
        return (byte) threeBitFlag;
      return 3;
    }

    public sealed class MeasureTextCache
    {
      private const int MaxCacheSize = 6;
      private int nextCacheEntry;
      private LayoutUtils.MeasureTextCache.PreferredSizeCache[] sizeCacheList;
      private Size unconstrainedPreferredSize;

      public MeasureTextCache()
      {
        this.unconstrainedPreferredSize = LayoutUtils.InvalidSize;
        this.nextCacheEntry = -1;
      }

      public Size GetTextSize(
        string text,
        Font font,
        Size proposedConstraints,
        TextFormatFlags flags)
      {
        if (!this.TextRequiresWordBreak(text, font, proposedConstraints, flags))
          return this.unconstrainedPreferredSize;
        if (this.sizeCacheList == null)
          this.sizeCacheList = new LayoutUtils.MeasureTextCache.PreferredSizeCache[6];
        foreach (LayoutUtils.MeasureTextCache.PreferredSizeCache sizeCache in this.sizeCacheList)
        {
          if (sizeCache.ConstrainingSize == proposedConstraints || sizeCache.ConstrainingSize.Width == proposedConstraints.Width && sizeCache.PreferredSize.Height <= proposedConstraints.Height)
            return sizeCache.PreferredSize;
        }
        Size preferredSize = TextRenderer.MeasureText(text, font, proposedConstraints, flags);
        this.nextCacheEntry = (this.nextCacheEntry + 1) % 6;
        this.sizeCacheList[this.nextCacheEntry] = new LayoutUtils.MeasureTextCache.PreferredSizeCache(proposedConstraints, preferredSize);
        return preferredSize;
      }

      private Size GetUnconstrainedSize(string text, Font font, TextFormatFlags flags)
      {
        if (this.unconstrainedPreferredSize == LayoutUtils.InvalidSize)
        {
          flags &= ~TextFormatFlags.WordBreak;
          this.unconstrainedPreferredSize = TextRenderer.MeasureText(text, font, LayoutUtils.MaxSize, flags);
        }
        return this.unconstrainedPreferredSize;
      }

      public void InvalidateCache()
      {
        this.unconstrainedPreferredSize = LayoutUtils.InvalidSize;
        this.sizeCacheList = (LayoutUtils.MeasureTextCache.PreferredSizeCache[]) null;
      }

      public bool TextRequiresWordBreak(string text, Font font, Size size, TextFormatFlags flags)
      {
        return this.GetUnconstrainedSize(text, font, flags).Width > size.Width;
      }

      private struct PreferredSizeCache
      {
        public Size ConstrainingSize;
        public Size PreferredSize;

        public PreferredSizeCache(Size constrainingSize, Size preferredSize)
        {
          this.ConstrainingSize = constrainingSize;
          this.PreferredSize = preferredSize;
        }
      }
    }
  }
}
