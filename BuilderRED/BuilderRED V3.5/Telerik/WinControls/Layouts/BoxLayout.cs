// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.BoxLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.Layouts
{
  public class BoxLayout : LayoutPanel
  {
    public static RadProperty ProportionProperty = RadProperty.Register("Proportion", typeof (float), typeof (BoxLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.0f, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (BoxLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty StripPositionProperty = RadProperty.Register("StripPosition", typeof (BoxLayout.StripPosition), typeof (BoxLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) BoxLayout.StripPosition.First, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private Orientation? orientationCache = new Orientation?();

    protected override void InitializeFields()
    {
      base.InitializeFields();
    }

    public static float GetProportion(RadElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      return (float) element.GetValue(BoxLayout.ProportionProperty);
    }

    public static void SetProportion(RadElement element, float proportion)
    {
      if (element == null)
        return;
      int num = (int) element.SetValue(BoxLayout.ProportionProperty, (object) proportion);
    }

    [Category("Layout")]
    [Description("Orientation of the strip - could be horizontal or vertical")]
    [RadPropertyDefaultValue("Orientation", typeof (BoxLayout))]
    public Orientation Orientation
    {
      get
      {
        if (!this.orientationCache.HasValue || !this.orientationCache.HasValue)
          this.orientationCache = new Orientation?((Orientation) this.GetValue(BoxLayout.OrientationProperty));
        return this.orientationCache.Value;
      }
      set
      {
        int num = (int) this.SetValue(BoxLayout.OrientationProperty, (object) value);
      }
    }

    private void InvalidateOrientation()
    {
      this.orientationCache = new Orientation?();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == BoxLayout.OrientationProperty)
        this.InvalidateOrientation();
      base.OnPropertyChanged(e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      float num = 0.0f;
      float width = availableSize.Width;
      float height = availableSize.Height;
      List<RadElement> radElementList1 = new List<RadElement>();
      List<RadElement> radElementList2 = new List<RadElement>();
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        if (child.AutoSize)
        {
          float proportion = BoxLayout.GetProportion(child);
          if ((double) proportion == 0.0)
          {
            child.Measure(availableSize);
            if (this.Orientation == Orientation.Horizontal)
            {
              width -= child.DesiredSize.Width;
              empty.Width += child.DesiredSize.Width;
              empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
            }
            else
            {
              height -= child.DesiredSize.Height;
              empty.Height += child.DesiredSize.Height;
              empty.Width = Math.Max(empty.Width, child.DesiredSize.Width);
            }
            radElementList2.Add(child);
          }
          else
            radElementList1.Add(child);
          num += proportion;
        }
      }
      if ((double) num == 0.0)
      {
        width = availableSize.Width;
        height = availableSize.Height;
      }
      for (int index = 0; index < radElementList1.Count; ++index)
      {
        RadElement element = radElementList1[index];
        if (element != null && element.AutoSize)
        {
          float proportion = BoxLayout.GetProportion(element);
          if (this.Orientation == Orientation.Horizontal)
          {
            SizeF availableSize1 = new SizeF(proportion * width / num, height);
            element.Measure(availableSize1);
            empty.Width += element.DesiredSize.Width;
            empty.Height = Math.Max(empty.Height, element.DesiredSize.Height);
          }
          else
          {
            SizeF availableSize1 = new SizeF(width, proportion * height / num);
            element.Measure(availableSize1);
            empty.Height += element.DesiredSize.Height;
            empty.Width = Math.Max(empty.Width, element.DesiredSize.Width);
          }
        }
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      PointF empty1 = PointF.Empty;
      PointF empty2 = PointF.Empty;
      if (this.Orientation == Orientation.Horizontal)
        empty2.X = finalSize.Width;
      if (this.Orientation == Orientation.Vertical)
        empty2.Y = finalSize.Height;
      SizeF empty3 = SizeF.Empty;
      List<RadElement> radElementList1 = new List<RadElement>();
      List<RadElement> radElementList2 = new List<RadElement>();
      List<RadElement> radElementList3 = new List<RadElement>();
      float width1 = finalSize.Width;
      float height = finalSize.Height;
      float num1 = 0.0f;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        if (child.AutoSize && child != null)
        {
          float proportion = BoxLayout.GetProportion(child);
          if (this.Orientation == Orientation.Horizontal)
          {
            if (child.StretchHorizontally)
              radElementList1.Add(child);
            else
              radElementList2.Add(child);
          }
          else if (child.StretchVertically)
            radElementList1.Add(child);
          else
            radElementList2.Add(child);
          if ((double) proportion != 0.0)
            radElementList3.Add(child);
          else if (this.Orientation == Orientation.Horizontal)
            width1 -= child.DesiredSize.Width;
          else
            height -= child.DesiredSize.Height;
          num1 += proportion;
        }
      }
      if ((double) num1 == 0.0)
      {
        width1 = finalSize.Width;
        height = finalSize.Height;
      }
      for (int index = 0; index < radElementList2.Count; ++index)
      {
        RadElement element = radElementList2[index];
        if (element.AutoSize)
        {
          SizeF size = SizeF.Empty;
          float proportion = BoxLayout.GetProportion(element);
          if ((double) proportion == 0.0)
            size = element.DesiredSize;
          if (this.Orientation == Orientation.Horizontal)
          {
            if ((double) proportion != 0.0)
              size = new SizeF(width1 * proportion / num1, height);
            empty3.Width += size.Width;
            size.Height = height;
            if ((BoxLayout.StripPosition) element.GetValue(BoxLayout.StripPositionProperty) == BoxLayout.StripPosition.First ^ this.RightToLeft)
            {
              element.Arrange(new RectangleF(empty1, size));
              empty1.X += size.Width;
            }
            else
            {
              empty2.X -= size.Width;
              element.Arrange(new RectangleF(empty2, size));
            }
          }
          else
          {
            if ((double) proportion != 0.0)
              size = new SizeF(width1, height * proportion / num1);
            empty3.Height += element.DesiredSize.Height;
            size.Width = width1;
            switch ((BoxLayout.StripPosition) element.GetValue(BoxLayout.StripPositionProperty))
            {
              case BoxLayout.StripPosition.First:
                element.Arrange(new RectangleF(empty1, size));
                empty1.Y += size.Height;
                continue;
              case BoxLayout.StripPosition.Last:
                empty2.Y -= size.Height;
                element.Arrange(new RectangleF(empty2, size));
                continue;
              default:
                continue;
            }
          }
        }
      }
      for (int index = 0; index < radElementList1.Count; ++index)
      {
        float num2 = 0.0f;
        float num3 = 0.0f;
        RadElement element = radElementList1[index];
        if (element.AutoSize)
        {
          SizeF size = SizeF.Empty;
          float proportion = BoxLayout.GetProportion(element);
          if ((double) proportion == 0.0)
            size = element.DesiredSize;
          if (this.Orientation == Orientation.Horizontal)
          {
            if ((double) proportion != 0.0)
              size = new SizeF(width1 * proportion / num1, height);
            else
              size.Width = width1 - empty3.Width;
            float width2 = size.Width;
            size.Height = height;
            if (!this.RightToLeft)
            {
              element.Arrange(new RectangleF(empty1, size));
              empty1.X += width2;
              empty1.Y += num3;
            }
            else
            {
              empty2.X -= width2;
              empty2.Y -= num3;
              element.Arrange(new RectangleF(empty2, size));
            }
          }
          else
          {
            if ((double) proportion != 0.0)
            {
              size = new SizeF(width1, height * proportion / num1);
              num3 = size.Height;
            }
            else
            {
              size.Height = height - empty3.Height;
              size.Width = width1;
            }
            element.Arrange(new RectangleF(empty1, size));
            empty1.X += num2;
            empty1.Y += num3;
          }
        }
      }
      return finalSize;
    }

    public enum StripPosition
    {
      First,
      Last,
    }
  }
}
