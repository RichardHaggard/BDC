// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadStackViewport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadStackViewport : StackLayoutPanel, IRadScrollViewport
  {
    internal const long ExtentSizeInvalidatedStateKey = 137438953472;
    internal const long RadStackViewportLastStateKey = 137438953472;
    private Size extentSize;

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      base.OnChildrenChanged(child, changeOperation);
      this.BitState[137438953472L] = true;
    }

    public virtual Size GetExtentSize()
    {
      if (this.GetBitState(137438953472L))
      {
        this.BitState[137438953472L] = false;
        this.extentSize = this.CalcExtentSize();
      }
      return this.extentSize;
    }

    public virtual void InvalidateViewport()
    {
      this.BitState[137438953472L] = true;
    }

    public virtual Point ResetValue(Point currentValue, Size viewportSize, Size extentSize)
    {
      if (this.Children.Count <= 0)
        return currentValue;
      Point point = currentValue;
      if (this.Orientation == Orientation.Vertical)
      {
        point.X = RadCanvasViewport.ValidatePosition(currentValue.X, extentSize.Width - viewportSize.Width);
        if (this.GetOffsetFromIndex(currentValue.Y) > extentSize.Height - viewportSize.Height)
          point.Y = this.Children.Count - this.GetLastFullVisibleItemsNum();
      }
      else
      {
        if (this.GetOffsetFromIndex(currentValue.X) > extentSize.Width - viewportSize.Width)
          point.X = this.Children.Count - this.GetLastFullVisibleItemsNum();
        point.Y = RadCanvasViewport.ValidatePosition(currentValue.Y, extentSize.Height - viewportSize.Height);
      }
      return point;
    }

    public virtual void DoScroll(Point oldValue, Point newValue)
    {
      Point point = newValue;
      if (this.Orientation == Orientation.Vertical)
        point.Y = this.GetOffsetFromIndex(newValue.Y);
      else
        point.X = this.GetOffsetFromIndex(newValue.X);
      this.PositionOffset = new SizeF((float) -point.X, (float) -point.Y);
    }

    public virtual Size ScrollOffsetForChildVisible(
      RadElement childElement,
      Point currentScrollValue)
    {
      int indexToOffset = this.Children.IndexOf(childElement);
      if (indexToOffset < 0 || indexToOffset >= this.Children.Count)
        return Size.Empty;
      Rectangle dest = new Rectangle(Point.Empty, this.Size);
      Rectangle src = new Rectangle(childElement.BoundingRectangle.Location, Size.Add(childElement.BoundingRectangle.Size, childElement.Margin.Size));
      src.Offset((int) Math.Round((double) this.PositionOffset.Width), (int) Math.Round((double) this.PositionOffset.Height));
      Size size1 = RadCanvasViewport.CalcMinOffset(src, dest);
      Size size2 = new Size(-size1.Width, -size1.Height);
      if (this.Orientation == Orientation.Vertical)
        size2.Height = this.ConvertPixelOffsetToIndexOffset(currentScrollValue.Y, indexToOffset, size2.Height);
      else
        size2.Width = this.ConvertPixelOffsetToIndexOffset(currentScrollValue.X, indexToOffset, size2.Width);
      return size2;
    }

    public virtual ScrollPanelParameters GetScrollParams(
      Size viewportSize,
      Size extentSize)
    {
      int collapsedChildrenNum = this.GetNonCollapsedChildrenNum();
      if (collapsedChildrenNum == 0)
        return new ScrollPanelParameters(0, 0, 0, 0, 0, 0, 0, 0);
      int horizMaximum = this.Orientation == Orientation.Vertical ? Math.Max(1, extentSize.Width) : Math.Max(1, collapsedChildrenNum - 1);
      int vertMaximum = this.Orientation == Orientation.Vertical ? Math.Max(1, collapsedChildrenNum - 1) : Math.Max(1, extentSize.Height);
      int horizSmallChange = this.Orientation == Orientation.Vertical ? 16 : 1;
      int vertSmallChange = this.Orientation == Orientation.Vertical ? 1 : 16;
      int horizLargeChange = this.Orientation == Orientation.Vertical ? Math.Max(1, viewportSize.Width) : this.GetLastFullVisibleItemsNum();
      int vertLargeChange = this.Orientation == Orientation.Vertical ? this.GetLastFullVisibleItemsNum() : Math.Max(1, viewportSize.Height);
      return new ScrollPanelParameters(0, horizMaximum, horizSmallChange, horizLargeChange, 0, vertMaximum, vertSmallChange, vertLargeChange);
    }

    protected virtual Size CalcExtentSize()
    {
      return Size.Round(this.DesiredSize);
    }

    private int ConvertPixelOffsetToIndexOffset(int topIndex, int indexToOffset, int pixelOffset)
    {
      int count = this.Children.Count;
      if (pixelOffset == 0 || topIndex < 0 || (topIndex >= count || indexToOffset < 0) || indexToOffset >= count)
        return 0;
      int num = 0;
      if (pixelOffset <= 0)
        return -(topIndex - indexToOffset);
      for (int index = topIndex; index < count; ++index)
      {
        RadElement child = this.Children[index];
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          Size size = Size.Add(child.BoundingRectangle.Size, child.Margin.Size);
          num += this.Orientation == Orientation.Vertical ? size.Height : size.Width;
          if (num >= pixelOffset)
            return index - topIndex + 1;
        }
      }
      return count - this.GetLastFullVisibleItemsNum() - topIndex;
    }

    protected virtual int GetLastFullVisibleItemsNum()
    {
      if (this.Children.Count <= 0)
        return 0;
      int num1 = this.Orientation == Orientation.Vertical ? this.Size.Height : this.Size.Width;
      int num2 = 0;
      for (int index = this.Children.Count - 1; index >= 0; --index)
      {
        RadElement child = this.Children[index];
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          Size size = Size.Add(child.BoundingRectangle.Size, child.Margin.Size);
          num2 += this.Orientation == Orientation.Vertical ? size.Height : size.Width;
          if (num2 > num1)
            return Math.Max(1, this.Children.Count - index - 1);
        }
      }
      return this.Children.Count;
    }

    protected virtual int GetNonCollapsedChildrenNum()
    {
      int num = 0;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        if (this.Children[index].Visibility != ElementVisibility.Collapsed)
          ++num;
      }
      return num;
    }

    protected int GetOffsetFromIndex(int topIndex)
    {
      if (this.Children.Count <= 0 || topIndex <= 0)
        return 0;
      int num1 = 0;
      int num2 = Math.Min(topIndex, this.Children.Count);
      for (int index = 0; index < num2; ++index)
      {
        RadElement child = this.Children[index];
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          Size size = Size.Add(child.BoundingRectangle.Size, child.Margin.Size);
          num1 += this.Orientation == Orientation.Vertical ? size.Height : size.Width;
        }
      }
      return num1;
    }
  }
}
