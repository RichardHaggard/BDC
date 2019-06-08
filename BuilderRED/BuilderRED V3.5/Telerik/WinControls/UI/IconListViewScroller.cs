// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IconListViewScroller
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class IconListViewScroller : ItemScroller<ListViewDataItem>
  {
    private int groupsHeight;

    public override void UpdateScrollRange()
    {
      if (this.Scrollbar == null || this.Traverser == null)
        return;
      if (this.Scrollbar.ScrollType == ScrollType.Vertical)
        this.UpdateVerticalScrollRange();
      else
        this.UpdateHorizontalScrollRange();
    }

    public override int GetScrollHeight(ListViewDataItem item)
    {
      if (!(this.Traverser as ListViewTraverser).IsGroupingEnabled || item.Owner.ViewElement.Orientation != Orientation.Horizontal || !(item is ListViewDataItemGroup))
        return base.GetScrollHeight(item);
      this.currentItemWidth = this.groupsHeight;
      if (!(item as ListViewDataItemGroup).Expanded)
        return item.ActualSize.Width;
      return item.Owner.GroupIndent;
    }

    private int GetGroupsHeight()
    {
      int val1 = 0;
      ListViewTraverser enumerator = this.Traverser.GetEnumerator() as ListViewTraverser;
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current is ListViewDataItemGroup)
          val1 = Math.Max(val1, enumerator.Current.ActualSize.Height);
      }
      return val1;
    }

    private void UpdateHorizontalScrollRange()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      object position = this.Traverser.Position;
      this.Traverser.Reset();
      int val1 = 0;
      this.groupsHeight = this.GetGroupsHeight();
      ListViewDataItem current1 = this.Traverser.Current;
      bool flag = false;
      while (this.Traverser.MoveNext())
      {
        flag = false;
        int scrollHeight = this.GetScrollHeight(this.Traverser.Current);
        int currentItemWidth = this.currentItemWidth;
        ListViewDataItemGroup current2 = this.Traverser.Current as ListViewDataItemGroup;
        if ((double) (num3 + currentItemWidth) > (double) this.ClientSize.Height - (double) this.Scrollbar.DesiredSize.Height || current2 != null)
        {
          if (current1 != null)
            current1.IsLastInRow = true;
          if (current2 != null)
          {
            num3 = 0;
            int num4 = num2 + current2.Owner.GroupIndent;
            if (!current2.Expanded)
            {
              num2 = num4 + current2.ActualSize.Width;
              if (!(current1 is ListViewDataItemGroup))
              {
                num2 += val1;
                flag = true;
              }
            }
            else
              num2 = num4 + val1;
          }
          else
          {
            num3 = this.groupsHeight;
            num2 += val1 + this.ItemSpacing;
          }
          val1 = 0;
        }
        else if (current1 != null)
          current1.IsLastInRow = current1 is ListViewDataItemGroup;
        int num5 = currentItemWidth + this.ItemSpacing;
        num3 += num5;
        ++num1;
        current1 = this.Traverser.Current;
        val1 = Math.Max(val1, scrollHeight);
      }
      if (current1 != null)
        current1.IsLastInRow = true;
      if (num3 != 0 && !flag)
        num2 += val1;
      this.Traverser.Position = position;
      if (num2 < 0)
        return;
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (this.Scrollbar.Maximum == num2)
          return;
        this.Scrollbar.Maximum = num2;
        this.SetScrollBarVisibility();
        this.UpdateScrollValue();
      }
      else
      {
        if (this.Scrollbar.Maximum == num1 - 1 || num1 - 1 <= 0)
          return;
        this.Scrollbar.Maximum = num1 - 1;
        this.UpdateScrollStep();
        this.SetScrollBarVisibility();
      }
    }

    private void UpdateVerticalScrollRange()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      object position = this.Traverser.Position;
      this.Traverser.Reset();
      int val1 = 0;
      ListViewDataItem current = this.Traverser.Current;
      while (this.Traverser.MoveNext())
      {
        int val2 = this.GetScrollHeight(this.Traverser.Current) + this.ItemSpacing;
        if ((double) (num2 + this.currentItemWidth) > (double) this.ClientSize.Width - (double) this.Scrollbar.DesiredSize.Width || this.Traverser.Current is ListViewDataItemGroup)
        {
          if (current != null)
            current.IsLastInRow = true;
          num2 = (this.Traverser as ListViewTraverser).IsGroupingEnabled ? this.Traverser.Current.Owner.GroupIndent : 0;
          num3 += val1;
          val1 = 0;
        }
        else if (current != null)
          current.IsLastInRow = current is ListViewDataItemGroup;
        this.currentItemWidth += this.ItemSpacing;
        num2 += this.currentItemWidth;
        ++num1;
        current = this.Traverser.Current;
        val1 = Math.Max(val1, val2);
      }
      if (current != null)
        current.IsLastInRow = true;
      if (num2 != 0)
        num3 += val1;
      this.Traverser.Position = position;
      if (num3 < 0)
        return;
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (this.Scrollbar.Maximum == num3)
          return;
        this.Scrollbar.Maximum = num3;
        this.SetScrollBarVisibility();
        this.UpdateScrollValue();
      }
      else
      {
        if (this.Scrollbar.Maximum == num1 - 1 || num1 - 1 <= 0)
          return;
        this.Scrollbar.Maximum = num1 - 1;
        this.UpdateScrollStep();
        this.SetScrollBarVisibility();
      }
    }

    protected override bool ScrollDown(int step)
    {
      if (this.Scrollbar != null && this.Scrollbar.ScrollType == ScrollType.Horizontal)
        return this.ScrollDownHorizontal(step);
      return this.ScrollDownVertical(step);
    }

    private bool ScrollDownVertical(int step)
    {
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        while (step > 0)
        {
          int val1 = 0;
          object position1 = this.Traverser.Position;
          bool flag = false;
          while (this.Traverser.MoveNext())
          {
            val1 = Math.Max(val1, this.GetScrollHeight(this.Traverser.Current) + this.ItemSpacing);
            if (this.Traverser.Current == null || this.Traverser.Current.IsLastInRow)
            {
              flag = this.Traverser.Current != null;
              break;
            }
          }
          if (flag)
          {
            object position2 = this.Traverser.Position;
            this.Traverser.Position = position1;
            int num = val1 - this.ScrollOffset;
            if (num > step)
            {
              this.cachedScrollOffset += step;
              break;
            }
            step -= num;
            this.cachedScrollOffset = 0;
            this.Traverser.Position = position2;
          }
          else
          {
            this.Traverser.Position = position1;
            return true;
          }
        }
        return true;
      }
      while (step-- > 0)
        this.Traverser.MoveNext();
      return true;
    }

    private bool ScrollDownHorizontal(int step)
    {
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        while (step > 0)
        {
          int val1 = 0;
          object position1 = this.Traverser.Position;
          bool flag = false;
          while (this.Traverser.MoveNext())
          {
            val1 = Math.Max(val1, this.GetScrollHeight(this.Traverser.Current));
            if (this.Traverser.Current == null || this.Traverser.Current.IsLastInRow)
            {
              if (position1 != null && !(position1 is ListViewDataItemGroup) && ((position1 as ListViewDataItem).IsLastInRow && !(this.Traverser.Current is ListViewDataItemGroup)))
                val1 += this.ItemSpacing;
              flag = this.Traverser.Current != null;
              break;
            }
          }
          if (flag)
          {
            object position2 = this.Traverser.Position;
            this.Traverser.Position = position1;
            int num = val1 - this.ScrollOffset;
            if (num > step)
            {
              this.cachedScrollOffset += step;
              break;
            }
            step -= num;
            this.cachedScrollOffset = 0;
            this.Traverser.Position = position2;
          }
          else
          {
            this.Traverser.Position = position1;
            return true;
          }
        }
        return true;
      }
      while (step-- > 0)
        this.Traverser.MoveNext();
      return true;
    }

    protected override bool ScrollUp(int step)
    {
      if (this.Scrollbar != null && this.Scrollbar.ScrollType == ScrollType.Horizontal)
        return this.ScrollUpHorizontal(step);
      return this.ScrollUpVertical(step);
    }

    private bool ScrollUpVertical(int step)
    {
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        while (step > 0)
        {
          if (this.ScrollOffset > step)
          {
            this.cachedScrollOffset -= step;
            break;
          }
          int val1 = this.Traverser.Current != null ? this.GetScrollHeight(this.Traverser.Current) + this.ItemSpacing : 0;
          while (this.Traverser.MovePrevious() && (this.Traverser.Current != null && !this.Traverser.Current.IsLastInRow))
            val1 = Math.Max(val1, this.GetScrollHeight(this.Traverser.Current) + this.ItemSpacing);
          if (this.ScrollOffset == 0 && val1 == 0)
            return false;
          step -= this.ScrollOffset;
          this.cachedScrollOffset = val1;
        }
        return true;
      }
      while (step-- > 0)
        this.Traverser.MovePrevious();
      return true;
    }

    private bool ScrollUpHorizontal(int step)
    {
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        while (step > 0)
        {
          if (this.ScrollOffset > step)
          {
            this.cachedScrollOffset -= step;
            break;
          }
          int val1 = this.Traverser.Current != null ? this.GetScrollHeight(this.Traverser.Current) : 0;
          while (this.Traverser.MovePrevious() && (this.Traverser.Current != null && !this.Traverser.Current.IsLastInRow))
            val1 = Math.Max(val1, this.GetScrollHeight(this.Traverser.Current));
          if (this.ScrollOffset == 0 && val1 == 0)
            return false;
          step -= this.ScrollOffset;
          this.cachedScrollOffset = val1;
        }
        return true;
      }
      while (step-- > 0)
        this.Traverser.MovePrevious();
      return true;
    }

    public override void UpdateScrollStep()
    {
      this.UpdateScrollRange();
      bool flag1 = this.Traverser.Current == null;
      bool flag2 = !flag1;
      while (!flag1 && !this.Traverser.Current.IsLastInRow)
      {
        if (this.Traverser.MovePrevious() || this.Traverser.Current.IsLastInRow)
          flag1 = true;
      }
      base.UpdateScrollStep();
      if (!flag2)
        return;
      this.OnScrollerUpdated(EventArgs.Empty);
    }

    public override bool ScrollToItem(ListViewDataItem item, bool checkScrollVisibility)
    {
      if (checkScrollVisibility && (this.Scrollbar == null || this.Scrollbar.Visibility == ElementVisibility.Collapsed))
        return false;
      if (this.ScrollMode != ItemScrollerScrollModes.Smooth && this.ScrollMode != ItemScrollerScrollModes.Deferred)
        return base.ScrollToItem(item, checkScrollVisibility);
      int num1 = this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1;
      if (num1 <= this.Scrollbar.Minimum)
        return false;
      if (item == null)
      {
        this.Scrollbar.Value = this.Scrollbar.Minimum;
        return true;
      }
      object position = this.Traverser.Position;
      int num2 = 0;
      int val1 = 0;
      this.Traverser.Reset();
      while (this.Traverser.MoveNext())
      {
        if (item.Equals((object) this.Traverser.Current))
        {
          do
            ;
          while (this.Traverser.MovePrevious() && this.Traverser.Current != null && !this.Traverser.Current.IsLastInRow);
          this.cachedScrollOffset = 0;
          this.scrollbarChanged = true;
          if (this.Scrollbar.Value <= this.Scrollbar.Maximum && num2 < this.Scrollbar.Maximum)
          {
            if (num2 < this.Scrollbar.Minimum)
              num2 = this.Scrollbar.Minimum;
            this.Scrollbar.Value = num2;
          }
          return true;
        }
        val1 = Math.Max(val1, this.GetScrollHeight(this.Traverser.Current) + this.ItemSpacing);
        if (this.Traverser.Current != null && this.Traverser.Current.IsLastInRow)
        {
          if (num2 + val1 >= num1)
          {
            do
              ;
            while (this.Traverser.MovePrevious() && this.Traverser.Current != null && !this.Traverser.Current.IsLastInRow);
            this.cachedScrollOffset = num1 - num2;
            this.scrollbarChanged = true;
            this.Scrollbar.Value = num1;
            return true;
          }
          num2 += val1;
          val1 = 0;
        }
      }
      this.Traverser.Position = position;
      return false;
    }
  }
}
