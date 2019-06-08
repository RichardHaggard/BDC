// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StatusBarBoxLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class StatusBarBoxLayout : LayoutPanel
  {
    public static RadProperty ProportionProperty = RadProperty.Register("Proportion", typeof (int), typeof (StatusBarBoxLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (StatusBarBoxLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static readonly RadProperty StripPositionProperty = RadProperty.Register("StripPosition", typeof (StatusBarBoxLayout.StripPosition), typeof (StatusBarBoxLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StatusBarBoxLayout.StripPosition.First, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    [RefreshProperties(RefreshProperties.All)]
    public static RadProperty SpringProperty = RadProperty.RegisterAttached("Spring", typeof (bool), typeof (StatusBarBoxLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsParentArrange));

    public static int GetProportion(RadElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      return (int) element.GetValue(StatusBarBoxLayout.ProportionProperty);
    }

    [Category("Layout")]
    [RadPropertyDefaultValue("Orientation", typeof (StatusBarBoxLayout))]
    [Description("Orientation of the strip - could be horizontal or vertical")]
    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(StatusBarBoxLayout.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(StatusBarBoxLayout.OrientationProperty, (object) value);
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      int num3 = 0;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        SizeF defaultSize = (SizeF) ((VisualElement) child).DefaultSize;
        if (!(bool) child.GetValue(StatusBarBoxLayout.SpringProperty))
        {
          num1 += Math.Max(child.DesiredSize.Width, defaultSize.Width);
          num2 += Math.Max(child.DesiredSize.Height, defaultSize.Height);
        }
        else
          ++num3;
      }
      float num4 = this.Orientation != Orientation.Horizontal ? finalSize.Height - num2 : finalSize.Width - num1;
      float num5 = 0.0f;
      if (num3 != 0)
        num5 = num4 / (float) num3;
      PointF empty1 = PointF.Empty;
      SizeF empty2 = SizeF.Empty;
      bool flag = false;
      if (this.Orientation == Orientation.Horizontal)
      {
        empty2.Height = finalSize.Height;
        for (int index = 0; index < this.Children.Count; ++index)
        {
          RadElement child = this.Children[index];
          if ((bool) child.GetValue(StatusBarBoxLayout.SpringProperty) && (double) num5 <= (double) child.DesiredSize.Width)
          {
            flag = true;
            break;
          }
        }
        for (int index = 0; index < this.Children.Count; ++index)
        {
          RadElement child = this.Children[index];
          SizeF defaultSize = (SizeF) ((VisualElement) child).DefaultSize;
          empty2.Width = (bool) child.GetValue(StatusBarBoxLayout.SpringProperty) ? num5 - 1f : Math.Max(child.DesiredSize.Width, defaultSize.Width);
          RectangleF rectangleF = new RectangleF(empty1, empty2);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, new RectangleF(PointF.Empty, finalSize));
          child.Arrange(rectangleF);
          if (num3 == 0 && (double) empty1.X + (double) empty2.Width + (double) num3 >= (double) finalSize.Width && (double) finalSize.Width != 0.0 || num3 != 0 && flag && (double) empty1.X + (double) empty2.Width + (double) num3 >= (double) finalSize.Width || (double) empty2.Width < 0.0)
          {
            int num6 = (int) child.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
          }
          else
          {
            int num7 = (int) child.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
          }
          empty1.X += empty2.Width;
        }
      }
      else
      {
        empty2.Width = finalSize.Width;
        for (int index = 0; index < this.Children.Count; ++index)
        {
          RadElement child = this.Children[index];
          SizeF defaultSize = (SizeF) ((VisualElement) child).DefaultSize;
          empty2.Height = (bool) child.GetValue(StatusBarBoxLayout.SpringProperty) ? num5 : Math.Max(child.DesiredSize.Height, defaultSize.Height);
          child.Arrange(new RectangleF(empty1, empty2));
          empty1.Y += empty2.Height;
        }
      }
      return finalSize;
    }

    protected override SizeF MeasureOverride(SizeF constraint)
    {
      RadElementCollection children = this.Children;
      SizeF sizeF1 = new SizeF();
      SizeF availableSize = constraint;
      bool flag = this.Orientation == Orientation.Horizontal;
      if (flag)
      {
        availableSize.Width = float.PositiveInfinity;
        double width = (double) constraint.Width;
      }
      else
      {
        availableSize.Height = float.PositiveInfinity;
        double height = (double) constraint.Height;
      }
      int index = 0;
      for (int count = children.Count; index < count; ++index)
      {
        RadElement radElement = children[index];
        if (radElement != null)
        {
          radElement.Measure(availableSize);
          SizeF sizeF2 = (SizeF) Size.Empty;
          if (radElement.Visibility == ElementVisibility.Visible)
            sizeF2 = radElement.DesiredSize;
          if (flag)
          {
            sizeF1.Width += sizeF2.Width;
            sizeF1.Height = Math.Max(sizeF1.Height, sizeF2.Height);
            double width = (double) sizeF2.Width;
          }
          else
          {
            sizeF1.Width = Math.Max(sizeF1.Width, sizeF2.Width);
            sizeF1.Height += sizeF2.Height;
            double height = (double) sizeF2.Height;
          }
        }
      }
      return sizeF1;
    }

    public enum StripPosition
    {
      First,
      Last,
    }
  }
}
