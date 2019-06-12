// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StripViewItemShrinkStrategy
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class StripViewItemShrinkStrategy
  {
    private StripViewLayoutInfo layoutInfo;
    private StripViewItemShrinkStrategy.StripViewShrinkInfo shrinkInfo;

    public StripViewItemShrinkStrategy(StripViewLayoutInfo sizeInfo)
    {
      this.layoutInfo = sizeInfo;
      this.shrinkInfo = new StripViewItemShrinkStrategy.StripViewShrinkInfo();
    }

    public void Execute()
    {
      if (!this.CanShrink())
        return;
      this.UpdateShrinkInfo();
      if (!this.shrinkInfo.execute)
        return;
      this.layoutInfo.items.Sort((IComparer<PageViewItemSizeInfo>) new StripViewItemShrinkStrategy.ShrinkInfoComparer(this));
      if (this.shrinkInfo.collapse)
        this.CollapseItems();
      else
        this.ExpandItems();
    }

    private void CollapseItems()
    {
      int index1 = this.layoutInfo.itemCount - 1;
      for (int index2 = index1 - 1; (double) this.shrinkInfo.shrinkAmount > 0.0 && index2 >= 0; --index2)
      {
        float collapseDifference = this.GetCollapseDifference(index1, index2);
        if ((double) collapseDifference > 0.0)
        {
          int num = index2 + 1;
          int singleCollapse = this.GetSingleCollapse(collapseDifference, this.layoutInfo.itemCount - num);
          for (int index = index1; index >= num; --index)
            this.CollapseItem(this.layoutInfo.items[index], singleCollapse);
        }
      }
      if ((double) this.shrinkInfo.shrinkAmount <= 0.0)
        return;
      int evenCollapse = this.GetEvenCollapse((int) this.shrinkInfo.shrinkAmount, this.layoutInfo.itemCount);
      for (int index = this.layoutInfo.itemCount - 1; index >= 0; --index)
        this.CollapseItem(this.layoutInfo.items[index], evenCollapse);
    }

    private int GetEvenCollapse(int amount, int itemCount)
    {
      int num = amount % itemCount;
      return (amount + (itemCount - num)) / itemCount;
    }

    private float GetCollapseDifference(int index1, int index2)
    {
      return this.layoutInfo.items[index1].layoutLength - this.layoutInfo.items[index2].layoutLength;
    }

    private int GetSingleCollapse(float difference, int itemCount)
    {
      float num1 = Math.Min(this.shrinkInfo.shrinkAmount, difference);
      int num2;
      if ((double) num1 * (double) itemCount <= (double) this.shrinkInfo.shrinkAmount)
      {
        num2 = (int) num1;
      }
      else
      {
        int num3 = (int) this.shrinkInfo.shrinkAmount % itemCount;
        num2 = (int) (((double) this.shrinkInfo.shrinkAmount + (double) itemCount - (double) num3) / (double) itemCount);
      }
      return num2;
    }

    private void CollapseItem(PageViewItemSizeInfo item, int collapse)
    {
      int num = Math.Min((int) Math.Max(0.0f, item.layoutLength - item.minLength), collapse);
      SizeF newSize = !this.layoutInfo.vertical ? new SizeF(item.layoutSize.Width - (float) num, item.layoutSize.Height) : new SizeF(item.layoutSize.Width, item.layoutSize.Height - (float) num);
      item.SetLayoutSize(newSize);
      this.shrinkInfo.shrinkAmount -= (float) collapse;
    }

    private void ExpandItems()
    {
      int index1 = 0;
      int itemCount = this.layoutInfo.itemCount;
      for (; (double) this.shrinkInfo.shrinkAmount > 0.0 && index1 < this.layoutInfo.itemCount; ++index1)
      {
        float expandDifference = this.GetExpandDifference(index1);
        if ((double) expandDifference <= 0.0)
        {
          --itemCount;
        }
        else
        {
          int singleExpand = this.GetSingleExpand(expandDifference, itemCount);
          if (singleExpand > 0)
          {
            for (int index2 = index1; index2 < this.layoutInfo.itemCount; ++index2)
              this.ExpandItem(this.layoutInfo.items[index2], singleExpand);
          }
        }
      }
    }

    private void ExpandItem(PageViewItemSizeInfo item, int expand)
    {
      SizeF newSize = !this.layoutInfo.vertical ? new SizeF(item.currentSize.Width + (float) expand, item.layoutSize.Height) : new SizeF(item.layoutSize.Width, item.currentSize.Height + (float) expand);
      item.SetCurrentSize(newSize);
      item.SetLayoutSize(newSize);
      this.shrinkInfo.shrinkAmount -= (float) expand;
    }

    private float GetExpandDifference(int index)
    {
      PageViewItemSizeInfo viewItemSizeInfo = this.layoutInfo.items[index];
      float num = viewItemSizeInfo.layoutLength - viewItemSizeInfo.currentLength;
      viewItemSizeInfo.SetLayoutSize(viewItemSizeInfo.currentSize);
      return num;
    }

    private int GetSingleExpand(float difference, int itemCount)
    {
      if ((double) difference <= 0.0 || (double) this.shrinkInfo.shrinkAmount < (double) itemCount)
        return 0;
      if ((double) difference > (double) this.shrinkInfo.shrinkAmount)
        difference = this.shrinkInfo.shrinkAmount;
      while ((double) difference > 1.0 && (double) difference * (double) itemCount > (double) this.shrinkInfo.shrinkAmount)
        --difference;
      if ((double) difference * (double) itemCount > (double) this.shrinkInfo.shrinkAmount)
      {
        int num = (int) difference % itemCount;
        difference = Math.Max(1f, difference - (float) num);
      }
      return (int) difference;
    }

    private float GetSizeLength(SizeF size)
    {
      if (!this.layoutInfo.vertical)
        return size.Width;
      return size.Height;
    }

    private float GetPaddingLength(Padding padding)
    {
      return this.layoutInfo.vertical ? (float) padding.Vertical : (float) padding.Horizontal;
    }

    private void UpdateShrinkInfo()
    {
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        this.shrinkInfo.layoutLength += viewItemSizeInfo.layoutLength + (float) viewItemSizeInfo.marginLength;
        this.shrinkInfo.currentLength += viewItemSizeInfo.currentLength + (float) viewItemSizeInfo.marginLength;
        this.shrinkInfo.minLength += viewItemSizeInfo.minLength + (float) viewItemSizeInfo.marginLength;
      }
      int num = (this.layoutInfo.itemCount - 1) * this.layoutInfo.itemSpacing;
      this.shrinkInfo.layoutLength += this.layoutInfo.paddingLength + this.layoutInfo.borderLength + (float) num;
      this.shrinkInfo.currentLength += this.layoutInfo.paddingLength + this.layoutInfo.borderLength + (float) num;
      this.shrinkInfo.minLength += this.layoutInfo.paddingLength + this.layoutInfo.borderLength + (float) num;
      if ((double) this.GetSizeLength(this.layoutInfo.itemLayout.PreviousConstraint) == 0.0)
      {
        float layoutLength = this.shrinkInfo.layoutLength;
      }
      this.shrinkInfo.collapse = (double) this.shrinkInfo.currentLength >= (double) this.layoutInfo.availableLength || (double) this.shrinkInfo.currentLength == (double) this.shrinkInfo.minLength || this.layoutInfo.availableSize == this.layoutInfo.previousSize;
      this.shrinkInfo.shrinkAmount = !this.shrinkInfo.collapse ? this.layoutInfo.availableLength - this.shrinkInfo.currentLength : this.shrinkInfo.layoutLength - this.layoutInfo.availableLength;
      this.shrinkInfo.execute = (double) this.shrinkInfo.layoutLength > (double) this.layoutInfo.availableLength;
    }

    private bool CanShrink()
    {
      if (this.layoutInfo.contentOrientation == PageViewContentOrientation.Auto)
        return true;
      if (this.layoutInfo.vertical)
      {
        if (this.layoutInfo.contentOrientation != PageViewContentOrientation.Vertical270)
          return this.layoutInfo.contentOrientation == PageViewContentOrientation.Vertical90;
        return true;
      }
      if (this.layoutInfo.contentOrientation != PageViewContentOrientation.Horizontal)
        return this.layoutInfo.contentOrientation == PageViewContentOrientation.Horizontal180;
      return true;
    }

    private class ShrinkInfoComparer : IComparer<PageViewItemSizeInfo>
    {
      private StripViewItemShrinkStrategy strategy;

      public ShrinkInfoComparer(StripViewItemShrinkStrategy strategy)
      {
        this.strategy = strategy;
      }

      public int Compare(PageViewItemSizeInfo x, PageViewItemSizeInfo y)
      {
        float num1 = x.layoutLength;
        float num2 = y.layoutLength;
        if ((double) num1 == (double) num2 && this.strategy.shrinkInfo.collapse)
        {
          num1 = x.currentLength;
          num2 = y.currentLength;
        }
        return num1.CompareTo(num2);
      }
    }

    private class StripViewShrinkInfo
    {
      public float layoutLength;
      public float shrinkAmount;
      public float currentLength;
      public float minLength;
      public bool collapse;
      public bool execute;
    }
  }
}
