// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IconListViewContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class IconListViewContainer : BaseListViewContainer
  {
    private float currentY;
    private float currentX;
    private RectangleF clientRect;
    private float maxItemHeight;
    private float maxItemWidth;

    public IconListViewContainer(BaseListViewElement owner)
      : base(owner)
    {
    }

    private bool Grouped
    {
      get
      {
        if (!this.owner.Owner.ShowGroups)
          return false;
        if (!this.owner.Owner.EnableCustomGrouping)
          return this.owner.Owner.EnableGrouping;
        return true;
      }
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.clientRect = new RectangleF((float) this.Padding.Left, (float) this.Padding.Top, availableSize.Width - (float) this.Padding.Horizontal, availableSize.Height - (float) this.Padding.Vertical);
      if (this.Grouped && this.Orientation == Orientation.Horizontal)
      {
        float groupsHeight = this.GetGroupsHeight();
        this.clientRect.Y += groupsHeight;
        this.clientRect.Height -= groupsHeight;
      }
      if (this.Orientation == Orientation.Vertical)
      {
        this.currentX = this.clientRect.X;
        this.currentY = this.clientRect.Y + this.ScrollOffset.Height;
      }
      else
      {
        this.currentX = this.clientRect.X + this.ScrollOffset.Width;
        this.currentY = this.clientRect.Y;
      }
      this.maxItemHeight = 0.0f;
      this.maxItemWidth = 0.0f;
      return base.BeginMeasure(availableSize);
    }

    private float GetGroupsHeight()
    {
      if (!this.owner.Owner.AllowArbitraryItemHeight || this.owner.Owner.Groups.Count == 0)
        return (float) this.owner.Owner.GroupItemSize.Height;
      float val2 = 0.0f;
      foreach (ListViewDataItemGroup group in this.owner.Owner.Groups)
      {
        if (group.Visible)
          val2 = Math.Max(group.Size.Height > 0 ? (float) group.Size.Height : (float) this.owner.GroupItemSize.Height, val2);
      }
      return val2;
    }

    protected override void MeasureElements()
    {
      if (this.Grouped && this.Orientation == Orientation.Horizontal)
      {
        int position = 0;
        IEnumerator enumerator = this.DataProvider.GetEnumerator();
        if (!enumerator.MoveNext())
          return;
        ListViewDataItem current1 = enumerator.Current as ListViewDataItem;
        if (current1 is ListViewDataItemGroup)
          this.currentX += (float) this.owner.GroupIndent;
        ListViewDataItemGroup current2 = enumerator.Current as ListViewDataItemGroup;
        ListViewDataItemGroup viewDataItemGroup = current2 ?? current1.Group;
        if (current2 != null)
          enumerator.MoveNext();
        bool flag = true;
        do
        {
          BaseListViewGroupVisualItem groupElement = (BaseListViewGroupVisualItem) null;
          if (viewDataItemGroup != null && viewDataItemGroup.Visible)
          {
            groupElement = this.UpdateElement(position, (ListViewDataItem) viewDataItemGroup) as BaseListViewGroupVisualItem;
            viewDataItemGroup = (ListViewDataItemGroup) null;
            ++position;
          }
          do
          {
            ListViewDataItem current3 = enumerator.Current as ListViewDataItem;
            if (current3 is ListViewDataItemGroup)
            {
              flag = true;
              break;
            }
            if (current3 != null && this.IsItemVisible(current3))
            {
              IVirtualizedElement<ListViewDataItem> element = this.UpdateElement(position, current3);
              if (element != null)
              {
                ++position;
                if (!this.MeasureElement(element))
                {
                  flag = false;
                  break;
                }
              }
            }
          }
          while (enumerator.MoveNext() && !(enumerator.Current is ListViewDataItemGroup));
          if ((double) this.currentY != (double) this.clientRect.Top)
          {
            this.currentY = this.clientRect.Top;
            this.currentX += this.maxItemWidth;
            this.maxItemWidth = 0.0f;
          }
          if (groupElement != null)
            this.MeasureGroupElement(groupElement);
          if (enumerator.Current is ListViewDataItemGroup)
            viewDataItemGroup = enumerator.Current as ListViewDataItemGroup;
          this.currentX += (float) this.owner.GroupIndent;
          if ((double) this.currentX >= (double) this.clientRect.Right)
            flag = false;
        }
        while (flag && enumerator.MoveNext());
        if (viewDataItemGroup != null)
        {
          BaseListViewGroupVisualItem groupElement = this.UpdateElement(position, (ListViewDataItem) viewDataItemGroup) as BaseListViewGroupVisualItem;
          if (groupElement != null)
          {
            this.MeasureGroupElement(groupElement);
            ++position;
          }
        }
        while (position < this.Children.Count)
          this.RemoveElement(position);
      }
      else
        base.MeasureElements();
    }

    private void MeasureGroupElement(BaseListViewGroupVisualItem groupElement)
    {
      ListViewDataItemGroup data = groupElement.Data as ListViewDataItemGroup;
      if (groupElement.HasVisibleItems())
      {
        int itemSpacing = this.owner.ItemSpacing;
        float num1 = 0.0f;
        float width = 0.0f;
        float val1 = 0.0f;
        int num2 = 1;
        foreach (ListViewDataItem listViewDataItem in data.Items)
        {
          float num3 = num1 + (float) listViewDataItem.ActualSize.Height;
          if (num2 > 1)
            num3 += (float) itemSpacing;
          if ((double) num3 > (double) this.clientRect.Height)
          {
            num1 = 0.0f;
            num2 = 1;
            width += val1;
            if (this.owner.Orientation == Orientation.Horizontal)
              width += (float) itemSpacing;
            val1 = 0.0f;
          }
          num1 += (float) listViewDataItem.ActualSize.Height;
          if (num2 > 1)
            num1 += (float) itemSpacing;
          val1 = Math.Max(val1, (float) listViewDataItem.ActualSize.Width);
          ++num2;
        }
        if ((double) val1 > 0.0)
          width += val1;
        int num4 = !this.owner.Owner.AllowArbitraryItemHeight || data.Size.Height <= 0 ? this.owner.GroupItemSize.Height : data.Size.Height;
        groupElement.Measure(new SizeF(width, (float) num4));
      }
      else
      {
        groupElement.Measure(this.clientRect.Size);
        this.currentX += groupElement.DesiredSize.Width;
        this.currentY = this.clientRect.Top;
      }
    }

    protected override bool MeasureElement(IVirtualizedElement<ListViewDataItem> element)
    {
      RadElement element1 = element as RadElement;
      BaseListViewGroupVisualItem viewGroupVisualItem = element as BaseListViewGroupVisualItem;
      if (element1 == null)
        return false;
      SizeF sizeF = this.MeasureElementCore(element1, this.clientRect.Size);
      bool flag = true;
      if (this.Orientation == Orientation.Vertical)
      {
        if ((double) this.currentX + (double) sizeF.Width > (double) this.clientRect.Right)
        {
          this.currentX = this.clientRect.X;
          this.currentY += this.maxItemHeight;
          this.maxItemHeight = 0.0f;
          if ((double) this.currentY >= (double) this.clientRect.Bottom)
            flag = false;
        }
        this.currentX += sizeF.Width;
        this.maxItemHeight = Math.Max(this.maxItemHeight, sizeF.Height);
      }
      else if (viewGroupVisualItem != null)
      {
        this.currentY = this.clientRect.Y;
        this.currentX += this.maxItemWidth + (float) this.owner.GroupIndent;
        this.maxItemWidth = 0.0f;
        if ((double) this.currentX >= (double) this.clientRect.Right)
          flag = false;
      }
      else
      {
        if ((double) this.currentY + (double) sizeF.Height > (double) this.clientRect.Bottom)
        {
          this.currentY = this.clientRect.Y;
          this.currentX += this.maxItemWidth;
          this.maxItemWidth = 0.0f;
          if ((double) this.currentX >= (double) this.clientRect.Right)
            flag = false;
        }
        this.currentY += sizeF.Height;
        this.maxItemWidth = Math.Max(this.maxItemWidth, sizeF.Width);
      }
      this.cachedDesiredSize.Width = Math.Max(this.cachedDesiredSize.Width, this.currentX + sizeF.Width);
      this.cachedDesiredSize.Height = Math.Max(this.cachedDesiredSize.Height, this.currentY + sizeF.Height);
      return flag;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.Orientation == Orientation.Vertical)
        return this.ArrangeVertical(finalSize);
      if (!this.Grouped)
        return this.ArrangeUngroupedHorizontal(finalSize);
      return this.ArrangeGroupedHorizontal(finalSize);
    }

    protected virtual SizeF ArrangeVertical(SizeF finalSize)
    {
      float x = this.clientRect.X;
      float y = this.clientRect.Y + this.ScrollOffset.Height;
      float val1_1 = 0.0f;
      float val1_2 = 0.0f;
      foreach (RadElement child in this.Children)
      {
        BaseListViewVisualItem listViewVisualItem = child as BaseListViewVisualItem;
        if (listViewVisualItem != null && listViewVisualItem.Data != null)
        {
          Size size = this.ElementProvider.GetElementSize(listViewVisualItem.Data).ToSize();
          if (child is BaseListViewGroupVisualItem)
          {
            if ((double) x != (double) this.clientRect.X)
            {
              x = this.clientRect.X;
              y += val1_1;
              val1_1 = (float) size.Height;
            }
            val1_1 = Math.Max(val1_1, (float) size.Height);
            val1_2 = Math.Max(val1_2, (float) size.Width);
            RectangleF rectangleF = new RectangleF(new PointF(x, y), (SizeF) size);
            if (this.RightToLeft)
              rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, this.clientRect);
            child.Arrange(rectangleF);
            x = this.clientRect.X;
            y += (float) (size.Height + this.ItemSpacing);
          }
          else
          {
            if ((double) x + (double) size.Width > (double) this.clientRect.Right && (double) x != (double) this.clientRect.X)
            {
              x = this.clientRect.X;
              y += val1_1 + (float) this.ItemSpacing;
              val1_1 = (float) size.Height;
            }
            val1_1 = Math.Max(val1_1, (float) size.Height);
            val1_2 = Math.Max(val1_2, (float) size.Width);
            if ((double) x == (double) this.clientRect.X && this.Grouped && (this.owner.Owner.Groups.Count > 0 && !this.owner.Owner.FullRowSelect))
              x += (float) this.owner.Owner.GroupIndent;
            RectangleF rectangleF = new RectangleF(new PointF(x, y), (SizeF) size);
            if (this.RightToLeft)
              rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, this.clientRect);
            child.Arrange(rectangleF);
            x += (float) (size.Width + this.ItemSpacing);
          }
        }
      }
      return finalSize;
    }

    protected virtual SizeF ArrangeUngroupedHorizontal(SizeF finalSize)
    {
      float x = this.clientRect.X + this.ScrollOffset.Width;
      float y = this.clientRect.Y;
      float val1_1 = 0.0f;
      float val1_2 = 0.0f;
      foreach (RadElement child in this.Children)
      {
        BaseListViewVisualItem listViewVisualItem = child as BaseListViewVisualItem;
        if (listViewVisualItem != null && listViewVisualItem.Data != null)
        {
          Size size = this.ElementProvider.GetElementSize(listViewVisualItem.Data).ToSize();
          if ((double) y + (double) size.Height > (double) this.clientRect.Bottom && (double) y != (double) this.clientRect.Y)
          {
            y = this.clientRect.Y;
            x += val1_2 + (float) this.ItemSpacing;
            val1_2 = (float) size.Width;
          }
          val1_1 = Math.Max(val1_1, (float) size.Height);
          val1_2 = Math.Max(val1_2, (float) size.Width);
          if ((double) x == (double) this.clientRect.X && this.Grouped && (this.owner.Owner.Groups.Count > 0 && !this.owner.Owner.FullRowSelect))
            x += (float) this.owner.Owner.GroupIndent;
          RectangleF rectangleF = new RectangleF(new PointF(x, y), (SizeF) size);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, this.clientRect);
          child.Arrange(rectangleF);
          y += (float) (size.Height + this.ItemSpacing);
        }
      }
      return finalSize;
    }

    protected virtual SizeF ArrangeGroupedHorizontal(SizeF finalSize)
    {
      float x = this.clientRect.X + this.ScrollOffset.Width;
      float y = this.clientRect.Y;
      float val1_1 = 0.0f;
      float val1_2 = 0.0f;
      int num = 0;
      BaseListViewGroupVisualItem viewGroupVisualItem = (BaseListViewGroupVisualItem) null;
      foreach (RadElement child in this.Children)
      {
        BaseListViewVisualItem listViewVisualItem = child as BaseListViewVisualItem;
        ++num;
        if (listViewVisualItem != null && listViewVisualItem.Data != null)
        {
          Size size = this.ElementProvider.GetElementSize(listViewVisualItem.Data).ToSize();
          if (child is BaseListViewGroupVisualItem)
          {
            if ((double) y != (double) this.clientRect.Y)
            {
              y = this.clientRect.Y;
              x += val1_2;
              val1_2 = 0.0f;
            }
            if (num == 1)
              viewGroupVisualItem = child as BaseListViewGroupVisualItem;
            else if (viewGroupVisualItem != null)
            {
              SizeF elementSize = this.ElementProvider.GetElementSize(viewGroupVisualItem.Data);
              RectangleF finalRect = new RectangleF(new PointF(x - elementSize.Width, (float) this.Padding.Top), elementSize);
              viewGroupVisualItem.Arrange(finalRect);
              viewGroupVisualItem = (BaseListViewGroupVisualItem) null;
            }
            if (((BaseListViewVisualItem) child).Data != this.owner.Scroller.Traverser.Current && (this.owner.Scroller.Traverser.Current == null || this.owner.Scroller.Traverser.Current.Group != ((BaseListViewVisualItem) child).Data))
              x += (float) this.owner.GroupIndent;
            RectangleF rectangleF = new RectangleF(new PointF(x, (float) this.Padding.Top), (SizeF) size);
            if (this.RightToLeft)
              rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, this.clientRect);
            child.Arrange(rectangleF);
            if (!(child as BaseListViewGroupVisualItem).HasVisibleItems())
              x += (float) size.Width;
          }
          else
          {
            if ((double) y + (double) size.Height > (double) this.clientRect.Bottom)
            {
              y = this.clientRect.Y;
              x += val1_2 + (float) this.ItemSpacing;
              val1_2 = (float) size.Width;
            }
            val1_1 = Math.Max(val1_1, (float) size.Height);
            val1_2 = Math.Max(val1_2, (float) size.Width);
            RectangleF rectangleF = new RectangleF(new PointF(x, y), (SizeF) size);
            if (this.RightToLeft)
              rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, this.clientRect);
            child.Arrange(rectangleF);
            y += (float) (size.Height + this.ItemSpacing);
          }
        }
      }
      return finalSize;
    }
  }
}
