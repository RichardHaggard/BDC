// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layout.IntegralScrollWrapPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.Layout
{
  public class IntegralScrollWrapPanel : WrapLayoutPanel
  {
    private int lineCount = -1;
    private int maxColumns = int.MaxValue;
    private int maxRows = 2;
    private SizeF longestElementSize = SizeF.Empty;
    private Padding maxPadding = Padding.Empty;
    private int lineNumber;
    private int curElementMaxX;
    private int curElementMaxY;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ClipDrawing = true;
    }

    public int LineCount
    {
      get
      {
        return this.lineCount;
      }
    }

    [Description("Gets or sets the maximum number of columns to be shown in the in-ribbon part of the gallery.")]
    public int MaxColumns
    {
      get
      {
        return this.maxColumns;
      }
      set
      {
        if (this.maxColumns == value)
          return;
        this.maxColumns = value;
        this.InvalidateMeasure();
        this.InvalidateArrange();
        this.UpdateLayout();
        if (this.ElementTree == null)
          return;
        this.ElementTree.Control.Invalidate();
      }
    }

    [Description("Gets or sets the maximum number of columns to be shown in the in-ribbon part of the gallery.")]
    public int MaxRows
    {
      get
      {
        return this.maxRows;
      }
      set
      {
        if (this.maxRows == value)
          return;
        this.maxRows = value;
        this.InvalidateMeasure();
        this.InvalidateArrange();
        this.UpdateLayout();
        if (this.ElementTree == null)
          return;
        this.ElementTree.Control.Invalidate();
      }
    }

    public int CurrentLine
    {
      get
      {
        return this.lineNumber;
      }
    }

    public void ScrollToLine(int lineNumber)
    {
      this.lineNumber = lineNumber;
      this.InvalidateMeasure();
      this.InvalidateArrange();
      this.UpdateLayout();
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      this.ElementTree.Control.Invalidate();
    }

    public void ScrollToElement(RadElement scrollElement)
    {
      int num = this.Children.IndexOf(scrollElement);
      if (num == -1 || this.curElementMaxX == 0)
        return;
      this.ScrollToLine(num / this.curElementMaxX);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      int count = this.Children.Count;
      float val2_1 = 0.0f;
      float val2_2 = 0.0f;
      float num1 = 0.0f;
      float num2 = 0.0f;
      bool flag1 = false;
      int num3 = 0;
      foreach (RadElement child in this.Children)
        child.Visibility = ElementVisibility.Hidden;
      base.MeasureOverride(availableSize);
      this.FindMaxSizedElement(availableSize);
      if (this.ElementTree != null && this.ElementTree.Control != null)
        this.ElementTree.Control.Invalidate();
      float val2_3 = this.longestElementSize.Width + (float) this.maxPadding.Vertical;
      float val2_4 = this.longestElementSize.Height + (float) this.maxPadding.Horizontal;
      int num4 = 0;
      for (int index = 0; index < count; ++index)
      {
        RadElement child = this.Children[index];
        num1 = Math.Max(num1, val2_3);
        num2 = Math.Max(num2, val2_4);
        bool flag2 = false;
        if (this.MaxColumns > 0 && num3 >= this.MaxColumns)
          flag2 = true;
        ++num3;
        if ((double) val2_1 + (double) val2_3 <= (double) availableSize.Width && !flag2)
        {
          val2_1 += val2_3;
          num1 = Math.Max(num1, val2_1);
          child.Measure(availableSize);
        }
        else
        {
          flag1 = true;
          num3 = 0;
        }
        if (flag1)
        {
          val2_1 = 0.0f;
          flag1 = false;
          if (num4 < this.MaxRows)
          {
            if ((double) val2_2 + (double) val2_4 <= (double) availableSize.Height)
            {
              ++num4;
              val2_2 += val2_4;
              num2 = Math.Max(num2, val2_2);
              child.Measure(availableSize);
            }
          }
          else
            break;
        }
      }
      return new SizeF(num1, num2);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      int count = this.Children.Count;
      float x = 0.0f;
      float y = 0.0f;
      if (count == 0)
        return finalSize;
      this.curElementMaxX = (int) Math.Floor((double) finalSize.Width / (double) this.longestElementSize.Width);
      if (this.MaxColumns < this.curElementMaxX)
        this.curElementMaxX = this.MaxColumns;
      this.curElementMaxY = (int) Math.Floor((double) finalSize.Height / (double) this.longestElementSize.Height);
      if (this.MaxRows < this.curElementMaxY)
        this.curElementMaxY = this.MaxRows;
      this.lineCount = count / this.curElementMaxX + (count % this.curElementMaxX != 0 ? 1 : 0);
      int index1 = this.CurrentLine * this.curElementMaxX;
      for (int index2 = 0; index2 < this.curElementMaxY && index1 < count; ++index2)
      {
        for (int index3 = 0; index3 < this.curElementMaxX && index1 < count; ++index1)
        {
          RadElement child = this.Children[index1];
          child.Visibility = ElementVisibility.Visible;
          child.Arrange(new RectangleF(x, y, this.longestElementSize.Width, this.longestElementSize.Height));
          x += this.longestElementSize.Width;
          ++index3;
        }
        y += this.longestElementSize.Height;
        x = 0.0f;
      }
      return finalSize;
    }

    private void FindMaxSizedElement(SizeF availableSize)
    {
      int count = this.Children.Count;
      for (int index = 0; index < count; ++index)
      {
        RadElement child = this.Children[index];
        child.Measure(availableSize);
        if ((double) child.DesiredSize.Width > (double) this.longestElementSize.Width)
          this.longestElementSize.Width = child.DesiredSize.Width;
        if ((double) child.DesiredSize.Height > (double) this.longestElementSize.Height)
          this.longestElementSize.Height = child.DesiredSize.Height;
        if (child.Padding.Horizontal > this.maxPadding.Horizontal || child.Padding.Vertical > this.maxPadding.Vertical)
          this.maxPadding = child.Padding;
      }
    }
  }
}
