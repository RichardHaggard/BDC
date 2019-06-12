// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckBoxLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class CheckBoxLayoutPanel : LayoutPanel
  {
    public static RadProperty CheckAlignmentProperty = RadProperty.Register(nameof (CheckAlignment), typeof (ContentAlignment), typeof (CheckBoxLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsBodyProperty = RadProperty.Register("IsBodyElement", typeof (bool), typeof (CheckBoxLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty IsCheckmarkProperty = RadProperty.Register("IsCheckElement", typeof (bool), typeof (CheckBoxLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty CheckMarkOffsetProperty = RadProperty.Register(nameof (CheckMarkOffset), typeof (int), typeof (CheckBoxLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    private Size checkSize = Size.Empty;
    internal RectangleF checkArea = RectangleF.Empty;
    private RadElement checkElement;
    private RadElement bodyElement;
    private ContentAlignment contentAlignment;
    internal RectangleF checkBounds;

    [RadPropertyDefaultValue("CheckMarkOffset", typeof (CheckBoxLayoutPanel))]
    [Category("Appearance")]
    [Description("Offset between the check and body elements")]
    public int CheckMarkOffset
    {
      get
      {
        return (int) this.GetValue(CheckBoxLayoutPanel.CheckMarkOffsetProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckBoxLayoutPanel.CheckMarkOffsetProperty, (object) value);
      }
    }

    public ContentAlignment CheckAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(CheckBoxLayoutPanel.CheckAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(CheckBoxLayoutPanel.CheckAlignmentProperty, (object) value);
      }
    }

    private bool EnsureBodyOrCheckElements()
    {
      if (this.bodyElement != null && this.checkElement != null)
        return true;
      foreach (RadElement child in this.GetChildren(ChildrenListOptions.Normal))
      {
        if ((bool) child.GetValue(CheckBoxLayoutPanel.IsBodyProperty))
          this.bodyElement = child;
        else if ((bool) child.GetValue(CheckBoxLayoutPanel.IsCheckmarkProperty))
          this.checkElement = child;
      }
      if (this.bodyElement == null)
        return this.checkElement != null;
      return true;
    }

    private void LayoutCheckmark(RectangleF fieldRectangle, bool newLayout)
    {
      float num = newLayout ? this.checkElement.DesiredSize.Width : (float) this.checkSize.Width;
      this.checkBounds = !newLayout ? (RectangleF) new Rectangle(Point.Empty, this.checkSize) : new RectangleF((PointF) Point.Empty, this.checkElement.DesiredSize);
      this.contentAlignment = !this.RightToLeft ? this.CheckAlignment : TelerikAlignHelper.RtlTranslateContent(this.CheckAlignment);
      if ((double) num <= 0.0)
        return;
      RectangleF rectangleF = fieldRectangle;
      RectangleF finalRect = rectangleF;
      if ((this.contentAlignment & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
        this.checkBounds.X = rectangleF.X + rectangleF.Width - this.checkBounds.Width;
      else if ((this.contentAlignment & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != (ContentAlignment) 0)
        this.checkBounds.X = rectangleF.X + (float) (((double) rectangleF.Width - (double) this.checkBounds.Width) / 2.0);
      if ((this.contentAlignment & (ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
        this.checkBounds.Y = rectangleF.Y + rectangleF.Height - this.checkBounds.Height;
      else if ((this.contentAlignment & (ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight)) != (ContentAlignment) 0)
        this.checkBounds.Y = rectangleF.Y + (float) (((double) rectangleF.Height - (double) this.checkBounds.Height) / 2.0);
      switch (this.contentAlignment)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          finalRect.X += num + (float) this.CheckMarkOffset;
          finalRect.Width -= num + (float) this.CheckMarkOffset;
          break;
        case ContentAlignment.TopCenter:
          finalRect.Y += num;
          finalRect.Height -= num;
          break;
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          finalRect.Width -= num + (float) this.CheckMarkOffset;
          break;
        case ContentAlignment.BottomCenter:
          finalRect.Height -= num;
          break;
      }
      if (newLayout)
      {
        this.checkElement.Arrange(this.checkBounds);
        this.bodyElement.Arrange(finalRect);
      }
      else
      {
        this.checkElement.Bounds = Rectangle.Round(this.checkBounds);
        this.bodyElement.Bounds = Rectangle.Round(finalRect);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (!this.EnsureBodyOrCheckElements())
        return SizeF.Empty;
      SizeF availableSize1 = availableSize;
      if (this.checkElement != null)
      {
        this.checkElement.Measure(availableSize);
        availableSize1 = this.GetBodyElementAvailableSize(availableSize);
      }
      if (this.bodyElement != null)
        this.bodyElement.Measure(availableSize1);
      return this.CombineSizes();
    }

    private SizeF GetBodyElementAvailableSize(SizeF availableSize)
    {
      SizeF sizeF = SizeF.Empty;
      switch (this.CheckAlignment)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomLeft:
        case ContentAlignment.BottomRight:
          sizeF = new SizeF(availableSize.Width - this.checkElement.DesiredSize.Width - (float) this.CheckMarkOffset, availableSize.Height);
          break;
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          sizeF = new SizeF(availableSize.Width, availableSize.Height - this.checkElement.DesiredSize.Height - (float) this.CheckMarkOffset);
          break;
      }
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (!this.EnsureBodyOrCheckElements())
        return base.ArrangeOverride(finalSize);
      if (this.checkElement != null && this.bodyElement != null)
      {
        this.LayoutCheckmark(new RectangleF(PointF.Empty, finalSize), true);
      }
      else
      {
        if (this.checkElement != null)
          this.checkElement.Arrange(new RectangleF(new PointF(0.0f, 0.0f), this.checkElement.DesiredSize));
        if (this.bodyElement != null)
          this.bodyElement.Arrange(new RectangleF(new PointF(0.0f, 0.0f), this.bodyElement.DesiredSize));
      }
      return finalSize;
    }

    private SizeF CombineSizes()
    {
      if (this.bodyElement != null && this.checkElement != null)
      {
        if (this.bodyElement.DesiredSize == SizeF.Empty && this.checkElement.DesiredSize != SizeF.Empty)
          return this.checkElement.DesiredSize;
        if (this.CheckAlignment == ContentAlignment.MiddleCenter)
          return new SizeF(LayoutUtils.UnionSizes(this.bodyElement.DesiredSize, this.checkElement.DesiredSize));
        if (LayoutUtils.IsVerticalAlignment(this.CheckAlignment))
          return new SizeF(Math.Max(this.bodyElement.DesiredSize.Width, this.checkElement.DesiredSize.Width), this.bodyElement.DesiredSize.Height + this.checkElement.DesiredSize.Height + (float) this.CheckMarkOffset);
        return new SizeF(this.bodyElement.DesiredSize.Width + this.checkElement.DesiredSize.Width + (float) this.CheckMarkOffset, Math.Max(this.bodyElement.DesiredSize.Height, this.checkElement.DesiredSize.Height));
      }
      if (this.bodyElement == null && this.checkElement != null)
        return this.checkElement.DesiredSize;
      if (this.bodyElement != null && this.checkElement == null)
        return this.bodyElement.DesiredSize;
      return SizeF.Empty;
    }

    private Size CombineSizes(Size imageTextSize, Size checkSize)
    {
      if (this.CheckAlignment == ContentAlignment.MiddleCenter)
        return LayoutUtils.UnionSizes(imageTextSize, checkSize);
      if (LayoutUtils.IsVerticalAlignment(this.CheckAlignment))
        return new Size(Math.Max(imageTextSize.Width, checkSize.Width), imageTextSize.Height + checkSize.Height + this.CheckMarkOffset);
      return new Size(imageTextSize.Width + checkSize.Width + this.CheckMarkOffset, Math.Max(imageTextSize.Height, checkSize.Height));
    }
  }
}
