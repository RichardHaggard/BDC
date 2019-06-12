// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarOverflowPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layout;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarOverflowPanel : WrapLayoutPanel
  {
    protected override SizeF MeasureOverride(SizeF constraint)
    {
      RadCommandBarOverflowPanel.UVSize uvSize1 = new RadCommandBarOverflowPanel.UVSize(this.Orientation);
      RadCommandBarOverflowPanel.UVSize uvSize2 = new RadCommandBarOverflowPanel.UVSize(this.Orientation);
      RadCommandBarOverflowPanel.UVSize uvSize3 = new RadCommandBarOverflowPanel.UVSize(this.Orientation, constraint.Width, constraint.Height);
      float itemWidth = this.ItemWidth;
      float itemHeight = this.ItemHeight;
      bool flag1 = !float.IsNaN(itemWidth);
      bool flag2 = !float.IsNaN(itemHeight);
      SizeF availableSize = new SizeF(flag1 ? itemWidth : constraint.Width, flag2 ? itemHeight : constraint.Height);
      RadElementCollection children = this.Children;
      int index = 0;
      for (int count = children.Count; index < count; ++index)
      {
        RadCommandBarBaseItem commandBarBaseItem = children[index] as RadCommandBarBaseItem;
        if (commandBarBaseItem != null)
        {
          if (!commandBarBaseItem.VisibleInStrip)
            commandBarBaseItem.Measure(SizeF.Empty);
          else
            commandBarBaseItem.Measure(availableSize);
          RadCommandBarOverflowPanel.UVSize uvSize4 = new RadCommandBarOverflowPanel.UVSize(this.Orientation, flag1 ? itemWidth : commandBarBaseItem.DesiredSize.Width, flag2 ? itemHeight : commandBarBaseItem.DesiredSize.Height);
          if (DoubleUtil.GreaterThan(uvSize1.U + uvSize4.U, uvSize3.U))
          {
            uvSize2.U = Math.Max(uvSize1.U, uvSize2.U);
            uvSize2.V += uvSize1.V;
            uvSize1 = uvSize4;
            if (DoubleUtil.GreaterThan(uvSize4.U, uvSize3.U))
            {
              uvSize2.U = Math.Max(uvSize4.U, uvSize2.U);
              uvSize2.V += uvSize4.V;
              uvSize1 = new RadCommandBarOverflowPanel.UVSize(this.Orientation);
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
