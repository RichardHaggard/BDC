// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.StackLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.Layouts
{
  public class StackLayoutPanel : LayoutPanel
  {
    public static RadProperty RowProperty = RadProperty.Register("Row", typeof (int), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty CollapseElementsOnResizeProperty = RadProperty.Register(nameof (CollapseElementsOnResize), typeof (bool), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty AllElementsEqualSizeProperty = RadProperty.Register(nameof (AllElementsEqualSize), typeof (bool), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ChildrenForcedSizeProperty = RadProperty.Register(nameof (ChildrenForcedSize), typeof (Size), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Size.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty FlipMaxSizeDimensionsProperty = RadProperty.Register(nameof (FlipMaxSizeDimensions), typeof (bool), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty UseParentSizeAsAvailableSizeProperty = RadProperty.Register(nameof (UseParentSizeAsAvailableSize), typeof (bool), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty IsInStripModeProperty = RadProperty.Register(nameof (IsInStripMode), typeof (bool), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty EqualChildrenWidthProperty = RadProperty.Register(nameof (EqualChildrenWidth), typeof (bool), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty EqualChildrenHeightProperty = RadProperty.Register(nameof (EqualChildrenHeight), typeof (bool), typeof (StackLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    protected const long StackLayoutPanelLastStateKey = 68719476736;

    [RadPropertyDefaultValue("Orientation", typeof (StackLayoutPanel))]
    [Category("Behavior")]
    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(StackLayoutPanel.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.OrientationProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("AllElementsEqualSize", typeof (StackLayoutPanel))]
    [Category("Behavior")]
    public bool AllElementsEqualSize
    {
      get
      {
        return (bool) this.GetValue(StackLayoutPanel.AllElementsEqualSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.AllElementsEqualSizeProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("EqualChildrenWidth", typeof (StackLayoutPanel))]
    public bool EqualChildrenWidth
    {
      get
      {
        return (bool) this.GetValue(StackLayoutPanel.EqualChildrenWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.EqualChildrenWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("EqualChildrenHeight", typeof (StackLayoutPanel))]
    [Category("Behavior")]
    public bool EqualChildrenHeight
    {
      get
      {
        return (bool) this.GetValue(StackLayoutPanel.EqualChildrenHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.EqualChildrenHeightProperty, (object) value);
      }
    }

    public Size ChildrenForcedSize
    {
      get
      {
        return (Size) this.GetValue(StackLayoutPanel.ChildrenForcedSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.ChildrenForcedSizeProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("FlipMaxSizeDimensions", typeof (StackLayoutPanel))]
    public bool FlipMaxSizeDimensions
    {
      get
      {
        return (bool) this.GetValue(StackLayoutPanel.FlipMaxSizeDimensionsProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.FlipMaxSizeDimensionsProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("CollapseElementsOnResize", typeof (StackLayoutPanel))]
    [Category("Behavior")]
    public bool CollapseElementsOnResize
    {
      get
      {
        return (bool) this.GetValue(StackLayoutPanel.CollapseElementsOnResizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.CollapseElementsOnResizeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("UseParentSizeAsAvailableSize", typeof (StackLayoutPanel))]
    [Category("Behavior")]
    public bool UseParentSizeAsAvailableSize
    {
      get
      {
        return (bool) this.GetValue(StackLayoutPanel.UseParentSizeAsAvailableSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.UseParentSizeAsAvailableSizeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("IsInStripMode", typeof (StackLayoutPanel))]
    [Category("Behavior")]
    public bool IsInStripMode
    {
      get
      {
        return (bool) this.GetValue(StackLayoutPanel.IsInStripModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutPanel.IsInStripModeProperty, (object) value);
      }
    }

    public override bool InvalidateChildrenOnChildChanged
    {
      get
      {
        return this.Orientation == Orientation.Vertical;
      }
    }

    private Size GetHorizontalSize()
    {
      int num1 = 0;
      int val1 = 0;
      int num2 = 0;
      int val2 = 0;
      int height = 0;
      foreach (RadElement child in this.GetChildren(ChildrenListOptions.Normal))
      {
        int num3 = (int) child.GetValue(StackLayoutPanel.RowProperty);
        if (num3 != num1)
        {
          height += val1;
          num2 = Math.Max(num2, val2);
          val1 = 0;
          val2 = 0;
          num1 = num3;
        }
        Rectangle rectangle = new Rectangle(child.BoundingRectangle.Location, Size.Add(child.BoundingRectangle.Size, child.Margin.Size));
        val2 += rectangle.Width;
        val1 = Math.Max(val1, rectangle.Height);
      }
      if (this.LayoutableChildrenCount > 0)
      {
        height += val1;
        num2 = Math.Max(num2, val2);
      }
      return new Size(num2, height);
    }

    private Size GetVerticalSize()
    {
      int height = 0;
      int num = 0;
      foreach (RadElement child in this.GetChildren(ChildrenListOptions.Normal))
      {
        Rectangle rectangle = new Rectangle(child.BoundingRectangle.Location, Size.Add(child.BoundingRectangle.Size, child.Margin.Size));
        height += rectangle.Height;
        num = Math.Max(num, rectangle.Width);
      }
      return new Size(num, height);
    }

    protected override SizeF ArrangeOverride(SizeF arrangeSize)
    {
      RadElementCollection children = this.Children;
      int count = children.Count;
      SizeF empty = SizeF.Empty;
      bool equalChildrenHeight = this.EqualChildrenHeight;
      bool equalChildrenWidth = this.EqualChildrenWidth;
      if (equalChildrenHeight || equalChildrenWidth)
      {
        for (int index = 0; index < count; ++index)
        {
          RadElement radElement = children[index];
          if (equalChildrenHeight)
            empty.Height = Math.Max(radElement.DesiredSize.Height, empty.Height);
          if (equalChildrenWidth)
            empty.Width = Math.Max(radElement.DesiredSize.Width, empty.Width);
        }
      }
      bool flag = this.Orientation == Orientation.Horizontal;
      bool rightToLeft = this.RightToLeft;
      float num = 0.0f;
      RectangleF finalRect = new RectangleF(PointF.Empty, arrangeSize);
      if (flag && rightToLeft)
        finalRect.X = arrangeSize.Width;
      for (int index = 0; index < count; ++index)
      {
        RadElement radElement = children[index];
        SizeF desiredSize = radElement.DesiredSize;
        if (radElement.Visibility == ElementVisibility.Collapsed)
        {
          radElement.Arrange(new RectangleF(PointF.Empty, desiredSize));
        }
        else
        {
          if (equalChildrenHeight)
            desiredSize.Height = !flag ? empty.Height : Math.Max(arrangeSize.Height, empty.Height);
          if (equalChildrenWidth)
            desiredSize.Width = !flag ? Math.Max(arrangeSize.Width, empty.Width) : empty.Width;
          if (flag)
          {
            if (rightToLeft)
            {
              num = desiredSize.Width;
              finalRect.X -= num;
            }
            else
            {
              finalRect.X += num;
              num = desiredSize.Width;
            }
            finalRect.Width = num;
            if (equalChildrenHeight)
            {
              SizeF size = finalRect.Size;
              finalRect.Height = desiredSize.Height;
              RectangleF rectangleF = LayoutUtils.Align(finalRect.Size, new RectangleF(PointF.Empty, size), this.Alignment);
              finalRect.Y += rectangleF.Y;
            }
            else
              finalRect.Height = arrangeSize.Height;
          }
          else
          {
            finalRect.Y += num;
            num = desiredSize.Height;
            finalRect.Height = num;
            if (equalChildrenWidth)
            {
              SizeF size = finalRect.Size;
              finalRect.Width = desiredSize.Width;
              ContentAlignment align = rightToLeft ? TelerikAlignHelper.RtlTranslateContent(this.Alignment) : this.Alignment;
              RectangleF rectangleF = LayoutUtils.Align(finalRect.Size, new RectangleF(PointF.Empty, size), align);
              finalRect.X += rectangleF.X;
            }
            else
              finalRect.Width = arrangeSize.Width;
          }
          radElement.Arrange(finalRect);
        }
      }
      return arrangeSize;
    }

    protected override SizeF MeasureOverride(SizeF constraint)
    {
      RadElementCollection children = this.Children;
      int count = children.Count;
      bool flag = this.Orientation == Orientation.Horizontal;
      SizeF availableSize = constraint;
      if (flag)
        availableSize.Width = float.PositiveInfinity;
      else
        availableSize.Height = float.PositiveInfinity;
      for (int index = 0; index < count; ++index)
        children[index].Measure(availableSize);
      SizeF empty1 = SizeF.Empty;
      bool equalChildrenHeight = this.EqualChildrenHeight;
      bool equalChildrenWidth = this.EqualChildrenWidth;
      if (equalChildrenHeight || equalChildrenWidth)
      {
        for (int index = 0; index < count; ++index)
        {
          RadElement radElement = children[index];
          if (equalChildrenHeight)
            empty1.Height = Math.Max(radElement.DesiredSize.Height, empty1.Height);
          if (equalChildrenWidth)
            empty1.Width = Math.Max(radElement.DesiredSize.Width, empty1.Width);
        }
      }
      SizeF empty2 = SizeF.Empty;
      for (int index = 0; index < count; ++index)
      {
        SizeF desiredSize = children[index].DesiredSize;
        if (equalChildrenHeight)
          desiredSize.Height = empty1.Height;
        if (equalChildrenWidth)
          desiredSize.Width = empty1.Width;
        if (flag)
        {
          empty2.Width += desiredSize.Width;
          empty2.Height = Math.Max(empty2.Height, desiredSize.Height);
        }
        else
        {
          empty2.Width = Math.Max(empty2.Width, desiredSize.Width);
          empty2.Height += desiredSize.Height;
        }
      }
      return empty2;
    }
  }
}
