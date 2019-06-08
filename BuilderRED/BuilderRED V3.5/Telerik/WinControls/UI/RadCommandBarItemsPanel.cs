// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarItemsPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarItemsPanel : StackLayoutPanel
  {
    private bool allowOverflow = true;
    private RadCommandBarBaseItemCollection items;
    private LayoutPanel overflowPanel;

    public event EventHandler ItemOverflowed;

    public event EventHandler ItemOutOfOverflow;

    protected virtual void OnItemOverflowed(object sender, EventArgs args)
    {
      if (this.ItemOverflowed == null)
        return;
      this.ItemOverflowed(sender, args);
    }

    protected virtual void OnItemOutOfOverflow(object sender, EventArgs args)
    {
      if (this.ItemOutOfOverflow == null)
        return;
      this.ItemOutOfOverflow(sender, args);
    }

    public bool AllowOverflow
    {
      get
      {
        return this.allowOverflow;
      }
      set
      {
        this.allowOverflow = value;
      }
    }

    public RadCommandBarItemsPanel(
      RadCommandBarBaseItemCollection itemsCollection,
      LayoutPanel overflowPannel)
    {
      this.items = itemsCollection;
      this.overflowPanel = overflowPannel;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      bool flag1 = this.Orientation == Orientation.Horizontal;
      float sumOfSpace = 0.0f;
      bool flag2 = false;
      int count1 = this.items.Count;
      int count2 = this.overflowPanel.Children.Count;
      if (count1 == 0 && count2 == 0)
        return RadControl.GetDpiScaledSize(flag1 ? new SizeF(30f, 0.0f) : new SizeF(0.0f, 30f));
      CommandBarStripElement parent = this.Parent as CommandBarStripElement;
      int currentIndex;
      for (currentIndex = 0; currentIndex < count1; ++currentIndex)
      {
        RadCommandBarBaseItem commandBarBaseItem = this.items[currentIndex];
        if (commandBarBaseItem != null)
        {
          if (!commandBarBaseItem.VisibleInStrip && parent.Site == null)
          {
            commandBarBaseItem.Measure(SizeF.Empty);
          }
          else
          {
            commandBarBaseItem.Measure(LayoutUtils.InfinitySize);
            float num1 = flag1 ? commandBarBaseItem.DesiredSize.Width : commandBarBaseItem.DesiredSize.Height;
            float num2 = flag1 ? availableSize.Width : availableSize.Height;
            if (parent.Site == null)
            {
              if ((double) sumOfSpace + (double) num1 > (double) num2 && this.allowOverflow)
              {
                flag2 = true;
                break;
              }
              if (flag1)
                commandBarBaseItem.Measure(new SizeF(Math.Max(availableSize.Width - sumOfSpace, 0.0f), availableSize.Height));
              else
                commandBarBaseItem.Measure(new SizeF(availableSize.Width, Math.Max(availableSize.Height - sumOfSpace, 0.0f)));
            }
            if (flag1)
            {
              empty.Width += commandBarBaseItem.DesiredSize.Width;
              empty.Height = Math.Max(empty.Height, commandBarBaseItem.DesiredSize.Height);
            }
            else
            {
              empty.Height += commandBarBaseItem.DesiredSize.Height;
              empty.Width = Math.Max(empty.Width, commandBarBaseItem.DesiredSize.Width);
            }
            sumOfSpace += num1;
          }
        }
      }
      if (flag2 && parent.Site == null && this.allowOverflow)
        this.HandleOverflowedItems(count1, currentIndex);
      else if (parent.Site == null)
        this.MeasureOverflowedItems(count2, availableSize, sumOfSpace, ref empty);
      if (this.overflowPanel.Children.Count > 0)
      {
        if (this.Orientation == Orientation.Horizontal)
          empty.Width = availableSize.Width;
        else
          empty.Height = availableSize.Height;
      }
      return empty;
    }

    private void MeasureOverflowedItems(
      int overflowedItemsCount,
      SizeF availableSize,
      float sumOfSpace,
      ref SizeF totalSize)
    {
      bool flag = this.Orientation == Orientation.Horizontal;
      for (int index = 0; index < overflowedItemsCount; ++index)
      {
        RadCommandBarBaseItem child = this.overflowPanel.Children[0] as RadCommandBarBaseItem;
        if (child != null)
        {
          if (!child.VisibleInStrip)
          {
            child.Measure(SizeF.Empty);
            this.items.Add(child);
            this.OnItemOutOfOverflow((object) child, new EventArgs());
          }
          else
          {
            child.Measure(LayoutUtils.InfinitySize);
            float num1 = flag ? child.DesiredSize.Width : child.DesiredSize.Height;
            float num2 = flag ? availableSize.Width : availableSize.Height;
            if ((double) sumOfSpace + (double) num1 > (double) num2)
              break;
            this.items.Add(child);
            this.OnItemOutOfOverflow((object) child, new EventArgs());
            if (flag)
            {
              child.Measure(new SizeF(availableSize.Width - sumOfSpace, availableSize.Height));
              totalSize.Width += child.DesiredSize.Width;
              totalSize.Height = Math.Max(totalSize.Height, child.DesiredSize.Height);
            }
            else
            {
              child.Measure(new SizeF(availableSize.Width, availableSize.Height - sumOfSpace));
              totalSize.Height += child.DesiredSize.Height;
              totalSize.Width = Math.Max(totalSize.Width, child.DesiredSize.Width);
            }
            sumOfSpace += num1;
          }
        }
      }
    }

    private void HandleOverflowedItems(int visibleItemsCount, int currentIndex)
    {
      for (int index = visibleItemsCount - 1; index >= currentIndex; --index)
      {
        RadCommandBarBaseItem commandBarBaseItem = this.items[index];
        this.items.Remove((RadItem) commandBarBaseItem);
        this.overflowPanel.Children.Insert(0, (RadElement) commandBarBaseItem);
        this.OnItemOverflowed((object) commandBarBaseItem, new EventArgs());
      }
    }

    protected override SizeF ArrangeOverride(SizeF arrangeSize)
    {
      RadCommandBarBaseItemCollection items = this.items;
      int count = items.Count;
      SizeF empty = SizeF.Empty;
      bool flag1 = false;
      bool flag2 = false;
      if (flag1 || flag2)
      {
        for (int index = 0; index < count; ++index)
        {
          RadElement radElement = (RadElement) items[index];
          if (flag1)
            empty.Height = Math.Max(radElement.DesiredSize.Height, empty.Height);
          if (flag2)
            empty.Width = Math.Max(radElement.DesiredSize.Width, empty.Width);
        }
      }
      bool flag3 = this.Orientation == Orientation.Horizontal;
      bool rightToLeft = this.RightToLeft;
      float num = 0.0f;
      RectangleF finalRect = new RectangleF(PointF.Empty, arrangeSize);
      if (flag3 && rightToLeft)
        finalRect.X = arrangeSize.Width;
      float f = this.ArrangeStretchedItems(arrangeSize);
      for (int index = 0; index < count; ++index)
      {
        RadElement radElement = (RadElement) items[index];
        SizeF desiredSize = radElement.DesiredSize;
        if (radElement.Visibility == ElementVisibility.Collapsed)
        {
          radElement.Arrange(new RectangleF(PointF.Empty, desiredSize));
        }
        else
        {
          if (flag1)
            desiredSize.Height = !flag3 ? empty.Height : Math.Max(arrangeSize.Height, empty.Height);
          if (flag2)
            desiredSize.Width = !flag3 ? Math.Max(arrangeSize.Width, empty.Width) : empty.Width;
          if (radElement.StretchHorizontally && flag3 && !float.IsInfinity(f))
            desiredSize.Width = f;
          if (radElement.StretchVertically && !flag3 && !float.IsInfinity(f))
            desiredSize.Height = f;
          if (flag3)
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
            if (flag1)
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
            if (flag2)
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
          if (radElement.StretchVertically && flag3)
            finalRect.Height = arrangeSize.Height;
          else if (radElement.StretchHorizontally && !flag3)
            finalRect.Width = arrangeSize.Width;
          radElement.Arrange(finalRect);
        }
      }
      return arrangeSize;
    }

    private float ArrangeStretchedItems(SizeF arrangeSize)
    {
      int count = this.items.Count;
      float num1 = 0.0f;
      float num2 = this.Orientation == Orientation.Horizontal ? arrangeSize.Width : arrangeSize.Height;
      int num3 = 0;
      for (int index = 0; index < count; ++index)
      {
        RadCommandBarBaseItem commandBarBaseItem = this.items[index];
        if (commandBarBaseItem != null && commandBarBaseItem.VisibleInStrip && commandBarBaseItem.Visibility != ElementVisibility.Collapsed)
        {
          if ((!commandBarBaseItem.StretchHorizontally || this.Orientation != Orientation.Horizontal) && (!commandBarBaseItem.StretchVertically || this.Orientation != Orientation.Vertical))
            num1 += this.Orientation == Orientation.Horizontal ? commandBarBaseItem.DesiredSize.Width : commandBarBaseItem.DesiredSize.Height;
          else
            ++num3;
        }
      }
      float num4 = num2 - num1;
      if ((double) num4 == 0.0)
        return 0.0f;
      return num4 / (float) num3;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == StackLayoutPanel.OrientationProperty)
      {
        CommandBarStripElement parent = this.Parent as CommandBarStripElement;
        foreach (RadCommandBarBaseItem commandBarBaseItem in parent.Items)
        {
          if (commandBarBaseItem.InheritsParentOrientation)
            commandBarBaseItem.Orientation = this.Orientation;
        }
        foreach (RadCommandBarBaseItem child in parent.OverflowButton.ItemsLayout.Children)
        {
          if (child.InheritsParentOrientation)
            child.Orientation = this.Orientation;
        }
      }
      base.OnPropertyChanged(e);
    }

    internal SizeF GetExpectedSize(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      foreach (RadElement child in this.Children)
      {
        RadCommandBarBaseItem commandBarBaseItem = child as RadCommandBarBaseItem;
        if (commandBarBaseItem != null && commandBarBaseItem.VisibleInStrip)
        {
          commandBarBaseItem.Measure(availableSize);
          if (this.Orientation == Orientation.Horizontal)
          {
            empty.Width += commandBarBaseItem.DesiredSize.Width;
            empty.Height = Math.Max(empty.Height, commandBarBaseItem.DesiredSize.Height);
          }
          else
          {
            empty.Height += commandBarBaseItem.DesiredSize.Height;
            empty.Width = Math.Max(empty.Width, commandBarBaseItem.DesiredSize.Width);
          }
        }
      }
      foreach (RadElement child in this.overflowPanel.Children)
      {
        RadCommandBarBaseItem commandBarBaseItem = child as RadCommandBarBaseItem;
        if (commandBarBaseItem != null && commandBarBaseItem.VisibleInStrip)
        {
          child.Measure(availableSize);
          if (this.Orientation == Orientation.Horizontal)
          {
            empty.Width += child.DesiredSize.Width;
            empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
          }
          else
          {
            empty.Height += child.DesiredSize.Height;
            empty.Width = Math.Max(empty.Width, child.DesiredSize.Width);
          }
        }
      }
      return empty;
    }
  }
}
