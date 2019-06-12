// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SizableDropDownMenuLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class SizableDropDownMenuLayout : RadDropDownMenuLayout
  {
    private StackLayoutElement stack;
    private float leftColumnWidth;
    private float rightColumnWidth;
    private float leftColumnMaxPadding;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stack = new StackLayoutElement();
      this.stack.Orientation = Orientation.Vertical;
      this.stack.StretchHorizontally = true;
      this.stack.StretchVertically = true;
      this.Children.Add((RadElement) this.stack);
    }

    public StackLayoutElement Stack
    {
      get
      {
        return this.stack;
      }
    }

    public override float LeftColumnWidth
    {
      get
      {
        return this.leftColumnWidth;
      }
    }

    public override float RightColumnWidth
    {
      get
      {
        return this.rightColumnWidth;
      }
    }

    public override float LeftColumnMaxPadding
    {
      get
      {
        return this.leftColumnMaxPadding;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.ElementTree == null)
        return base.MeasureOverride(availableSize);
      this.stack.Measure(availableSize);
      SizeF desiredSize = this.stack.DesiredSize;
      this.leftColumnWidth = 0.0f;
      foreach (RadElement child in this.stack.Children)
      {
        RadMenuItem radMenuItem = child as RadMenuItem;
        if (radMenuItem != null)
        {
          this.leftColumnWidth = Math.Max(this.leftColumnWidth, radMenuItem.LeftColumnElement.DesiredSize.Width);
          this.rightColumnWidth = Math.Max(this.rightColumnWidth, radMenuItem.RightColumnElement.DesiredSize.Width);
          this.leftColumnMaxPadding = Math.Max(this.leftColumnMaxPadding, (float) (radMenuItem.Padding.Left + radMenuItem.BorderThickness.Left + radMenuItem.Margin.Left));
        }
      }
      this.leftColumnWidth = Math.Max(this.leftColumnWidth, (float) this.LeftColumnMinWidth);
      desiredSize.Width += this.leftColumnWidth + (float) this.Padding.Horizontal;
      desiredSize.Height += (float) this.Padding.Vertical;
      return desiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF arrangeSize)
    {
      this.stack.Arrange(new RectangleF(PointF.Empty, arrangeSize));
      return arrangeSize;
    }
  }
}
