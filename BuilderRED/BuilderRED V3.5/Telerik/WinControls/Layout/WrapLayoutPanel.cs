// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layout.WrapLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.Layout
{
  public class WrapLayoutPanel : LayoutPanel
  {
    public static readonly RadProperty ItemHeightProperty = RadProperty.Register(nameof (ItemHeight), typeof (float), typeof (WrapLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) float.NaN, ElementPropertyOptions.AffectsMeasure), new ValidateValueCallback(WrapLayoutPanel.IsWidthHeightValid));
    public static readonly RadProperty ItemWidthProperty = RadProperty.Register(nameof (ItemWidth), typeof (float), typeof (WrapLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) float.NaN, ElementPropertyOptions.AffectsMeasure), new ValidateValueCallback(WrapLayoutPanel.IsWidthHeightValid));
    public static readonly RadProperty OrientationProperty = StackLayoutPanel.OrientationProperty.AddOwner(typeof (WrapLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.AffectsMeasure, new PropertyChangedCallback(WrapLayoutPanel.OnOrientationChanged)));
    private Orientation orientation;
    private bool stretchItems;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.orientation = (Orientation) WrapLayoutPanel.OrientationProperty.GetMetadata((RadObject) this).GetDefaultValue((RadObject) this, WrapLayoutPanel.OrientationProperty);
    }

    private int ElementComparisonBySizeDesc(RadElement element1, RadElement element2)
    {
      return new WrapLayoutPanel.UVSize(this.Orientation, element1.DesiredSize.Width, element1.DesiredSize.Height).U.CompareTo(new WrapLayoutPanel.UVSize(this.Orientation, element2.DesiredSize.Width, element2.DesiredSize.Height).U) * -1;
    }

    private void ArrangeLine(
      float VPosition,
      float lineV,
      int start,
      int end,
      bool useItemU,
      float itemU,
      float totalU)
    {
      float num1 = 0.0f;
      bool flag = this.Orientation == Orientation.Horizontal;
      RadElementCollection children = this.Children;
      float num2 = 0.0f;
      if (this.stretchItems)
        num2 = this.DetermineStretchedItemU(start, end, children, totalU);
      for (int index = start; index < end; ++index)
      {
        RadElement radElement = children[index];
        if (radElement != null)
        {
          WrapLayoutPanel.UVSize uvSize = new WrapLayoutPanel.UVSize(this.Orientation, radElement.DesiredSize.Width, radElement.DesiredSize.Height);
          float num3 = useItemU ? itemU : uvSize.U;
          if (this.stretchItems && radElement.Visibility != ElementVisibility.Collapsed && (double) num3 < (double) num2)
            num3 = num2;
          RectangleF finalRect = new RectangleF(flag ? num1 : VPosition, flag ? VPosition : num1, flag ? num3 : lineV, flag ? lineV : num3);
          if (this.RightToLeft && flag)
            finalRect.X = totalU - finalRect.X - finalRect.Width;
          radElement.Arrange(finalRect);
          num1 += num3;
        }
      }
    }

    private float DetermineStretchedItemU(
      int start,
      int end,
      RadElementCollection internalChildren,
      float totalU)
    {
      List<RadElement> radElementList = new List<RadElement>();
      for (int index = start; index < end; ++index)
      {
        RadElement internalChild = internalChildren[index];
        if (internalChild != null && internalChild.Visibility != ElementVisibility.Collapsed)
          radElementList.Add(internalChild);
      }
      int count = radElementList.Count;
      float num1 = totalU / (float) count;
      float num2 = totalU;
      radElementList.Sort(new Comparison<RadElement>(this.ElementComparisonBySizeDesc));
      for (int index = 0; index < radElementList.Count; ++index)
      {
        RadElement radElement = radElementList[index];
        WrapLayoutPanel.UVSize uvSize = new WrapLayoutPanel.UVSize(this.Orientation, radElement.DesiredSize.Width, radElement.DesiredSize.Height);
        if ((double) uvSize.U > (double) num1)
        {
          num2 -= uvSize.U;
          --count;
          num1 = num2 / (float) count;
        }
      }
      return num1;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      int start = 0;
      float itemWidth = this.ItemWidth;
      float itemHeight = this.ItemHeight;
      float VPosition = 0.0f;
      float itemU = this.Orientation == Orientation.Horizontal ? itemWidth : itemHeight;
      float totalU = this.Orientation == Orientation.Horizontal ? finalSize.Width : finalSize.Height;
      WrapLayoutPanel.UVSize uvSize1 = new WrapLayoutPanel.UVSize(this.Orientation);
      WrapLayoutPanel.UVSize uvSize2 = new WrapLayoutPanel.UVSize(this.Orientation, finalSize.Width, finalSize.Height);
      bool flag1 = !float.IsNaN(itemWidth);
      bool flag2 = !float.IsNaN(itemHeight);
      bool useItemU = this.Orientation == Orientation.Horizontal ? flag1 : flag2;
      RadElementCollection children = this.Children;
      int index = 0;
      for (int count = children.Count; index < count; ++index)
      {
        RadElement radElement = children[index];
        if (radElement != null)
        {
          WrapLayoutPanel.UVSize uvSize3 = new WrapLayoutPanel.UVSize(this.Orientation, flag1 ? itemWidth : radElement.DesiredSize.Width, flag2 ? itemHeight : radElement.DesiredSize.Height);
          if (DoubleUtil.GreaterThan(uvSize1.U + uvSize3.U, uvSize2.U))
          {
            this.ArrangeLine(VPosition, uvSize1.V, start, index, useItemU, itemU, totalU);
            VPosition += uvSize1.V;
            uvSize1 = uvSize3;
            if (DoubleUtil.GreaterThan(uvSize3.U, uvSize2.U))
            {
              this.ArrangeLine(VPosition, uvSize3.V, index, ++index, useItemU, itemU, totalU);
              VPosition += uvSize3.V;
              uvSize1 = new WrapLayoutPanel.UVSize(this.Orientation);
            }
            start = index;
          }
          else
          {
            uvSize1.U += uvSize3.U;
            uvSize1.V = Math.Max(uvSize3.V, uvSize1.V);
          }
        }
      }
      if (start < children.Count)
        this.ArrangeLine(VPosition, uvSize1.V, start, children.Count, useItemU, itemU, totalU);
      return finalSize;
    }

    private static bool IsWidthHeightValid(object value, RadObject obj)
    {
      float f = (float) value;
      if (float.IsNaN(f))
        return true;
      if ((double) f >= 0.0)
        return !float.IsPositiveInfinity(f);
      return false;
    }

    protected override SizeF MeasureOverride(SizeF constraint)
    {
      WrapLayoutPanel.UVSize uvSize1 = new WrapLayoutPanel.UVSize(this.Orientation);
      WrapLayoutPanel.UVSize uvSize2 = new WrapLayoutPanel.UVSize(this.Orientation);
      WrapLayoutPanel.UVSize uvSize3 = new WrapLayoutPanel.UVSize(this.Orientation, constraint.Width, constraint.Height);
      float itemWidth = this.ItemWidth;
      float itemHeight = this.ItemHeight;
      bool flag1 = !float.IsNaN(itemWidth);
      bool flag2 = !float.IsNaN(itemHeight);
      SizeF availableSize = new SizeF(flag1 ? itemWidth : constraint.Width, flag2 ? itemHeight : constraint.Height);
      RadElementCollection children = this.Children;
      int index = 0;
      for (int count = children.Count; index < count; ++index)
      {
        RadElement radElement = children[index];
        if (radElement != null)
        {
          radElement.Measure(availableSize);
          WrapLayoutPanel.UVSize uvSize4 = new WrapLayoutPanel.UVSize(this.Orientation, flag1 ? itemWidth : radElement.DesiredSize.Width, flag2 ? itemHeight : radElement.DesiredSize.Height);
          if (DoubleUtil.GreaterThan(uvSize1.U + uvSize4.U, uvSize3.U))
          {
            uvSize2.U = Math.Max(uvSize1.U, uvSize2.U);
            uvSize2.V += uvSize1.V;
            uvSize1 = uvSize4;
            if (DoubleUtil.GreaterThan(uvSize4.U, uvSize3.U))
            {
              uvSize2.U = Math.Max(uvSize4.U, uvSize2.U);
              uvSize2.V += uvSize4.V;
              uvSize1 = new WrapLayoutPanel.UVSize(this.Orientation);
            }
          }
          else
          {
            uvSize1.U += uvSize4.U;
            uvSize1.V = Math.Max(uvSize4.V, uvSize1.V);
          }
        }
      }
      uvSize2.U = Math.Max(uvSize1.U, uvSize2.U);
      uvSize2.V += uvSize1.V;
      return new SizeF(uvSize2.Width, uvSize2.Height);
    }

    private static void OnOrientationChanged(RadObject d, RadPropertyChangedEventArgs e)
    {
      ((WrapLayoutPanel) d).orientation = (Orientation) e.NewValue;
    }

    public bool StretchItems
    {
      get
      {
        return this.stretchItems;
      }
      set
      {
        if (this.stretchItems == value)
          return;
        this.stretchItems = value;
        this.InvalidateMeasure();
      }
    }

    [TypeConverter(typeof (LengthConverter))]
    public float ItemHeight
    {
      get
      {
        return (float) this.GetValue(WrapLayoutPanel.ItemHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(WrapLayoutPanel.ItemHeightProperty, (object) value);
      }
    }

    [TypeConverter(typeof (LengthConverter))]
    public float ItemWidth
    {
      get
      {
        return (float) this.GetValue(WrapLayoutPanel.ItemWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(WrapLayoutPanel.ItemWidthProperty, (object) value);
      }
    }

    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        int num = (int) this.SetValue(WrapLayoutPanel.OrientationProperty, (object) value);
      }
    }

    private struct UVSize
    {
      internal float U;
      internal float V;
      private Orientation orientation;

      internal UVSize(Orientation orientation, float width, float height)
      {
        this.U = this.V = 0.0f;
        this.orientation = orientation;
        this.Width = width;
        this.Height = height;
      }

      internal UVSize(Orientation orientation)
      {
        this.U = this.V = 0.0f;
        this.orientation = orientation;
      }

      internal float Width
      {
        get
        {
          if (this.orientation != Orientation.Horizontal)
            return this.V;
          return this.U;
        }
        set
        {
          if (this.orientation == Orientation.Horizontal)
            this.U = value;
          else
            this.V = value;
        }
      }

      internal float Height
      {
        get
        {
          if (this.orientation != Orientation.Horizontal)
            return this.U;
          return this.V;
        }
        set
        {
          if (this.orientation == Orientation.Horizontal)
            this.V = value;
          else
            this.U = value;
        }
      }
    }
  }
}
