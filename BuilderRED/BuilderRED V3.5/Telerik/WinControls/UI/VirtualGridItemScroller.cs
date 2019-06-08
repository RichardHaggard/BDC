// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridItemScroller
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class VirtualGridItemScroller : ItemScroller<int>
  {
    private VirtualGridTableViewState viewState;

    public VirtualGridItemScroller(VirtualGridTableViewState viewState)
    {
      this.viewState = viewState;
    }

    public override bool ScrollToItem(int item, bool checkScrollVisibility)
    {
      if (item < 0 || item > this.viewState.ItemCount || checkScrollVisibility && (this.Scrollbar == null || this.Scrollbar.Visibility == ElementVisibility.Collapsed))
        return false;
      if (item == this.Traverser.Current)
        return true;
      int num = this.GetItemOffset(item);
      if (num > this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1)
        num = this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1;
      this.Scrollbar.Value = num;
      return true;
    }

    protected override bool ScrollDown(int step)
    {
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (step == 0)
          return true;
        object position = this.Traverser.Position;
        this.Traverser.MoveNext();
        int num = this.GetScrollHeight(this.Traverser.Current) + this.ItemSpacing - this.cachedScrollOffset;
        if (num > step)
        {
          this.cachedScrollOffset += step;
          this.Traverser.Position = position;
        }
        else
        {
          step -= num;
          this.cachedScrollOffset = 0;
          int targetIndex;
          this.cachedScrollOffset = this.viewState.GetScrollDownTarget(this.Traverser.Current, step, out targetIndex);
          this.Traverser.Position = (object) targetIndex;
        }
        return true;
      }
      this.Traverser.Position = (object) (this.Traverser.Current + step);
      return true;
    }

    protected override bool ScrollUp(int step)
    {
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (step == 0)
          return true;
        if (this.Traverser.Position == null)
        {
          if (this.cachedScrollOffset > step)
            this.cachedScrollOffset -= step;
          else
            this.cachedScrollOffset = 0;
          return true;
        }
        if (this.cachedScrollOffset >= step)
        {
          this.cachedScrollOffset -= step;
          return true;
        }
        step -= this.cachedScrollOffset;
        this.cachedScrollOffset = 0;
        int targetIndex;
        this.cachedScrollOffset = this.viewState.GetScrollUpTarget(this.Traverser.Current, step, out targetIndex);
        this.Traverser.Position = (object) targetIndex;
        if (targetIndex < -1)
          this.cachedScrollOffset = 0;
        return true;
      }
      if (step <= 0 || this.Traverser.Position == null)
        return false;
      this.Traverser.Position = (object) (this.Traverser.Current - step);
      return true;
    }

    public override void UpdateScrollRange()
    {
      if (this.Scrollbar == null || this.Traverser == null)
        return;
      int num1 = this.viewState.GetTotalItemSize();
      int num2 = this.viewState.ItemCount;
      if (this.viewState.EnablePaging)
      {
        int itemIndex = Math.Min(this.viewState.ItemCount - 1, this.viewState.PageIndex * this.viewState.PageSize);
        num1 = this.viewState.GetItemOffset(Math.Min(this.viewState.ItemCount, itemIndex + this.viewState.PageSize)) - this.viewState.GetItemOffset(itemIndex);
        num2 = this.viewState.PageSize;
      }
      if (num1 < 0)
        return;
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (this.Scrollbar.Maximum == num1)
          return;
        this.Scrollbar.Maximum = num1;
        this.SetScrollBarVisibility();
        this.UpdateScrollValue();
      }
      else
      {
        if (this.Scrollbar.Maximum == num2 - 1 || num2 - 1 <= 0)
          return;
        this.Scrollbar.Maximum = num2 - 1;
        this.UpdateScrollStep();
        this.SetScrollBarVisibility();
      }
    }

    public override void UpdateScrollRange(int width, bool updateScrollValue)
    {
      if (this.Scrollbar == null)
        return;
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (this.Scrollbar.Maximum == width)
          return;
        this.Scrollbar.Maximum = width >= 0 ? width : 0;
        this.SetScrollBarVisibility();
        if (updateScrollValue)
        {
          this.UpdateScrollValue();
        }
        else
        {
          int num = this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1;
          if (num <= 0 || this.Scrollbar.Value <= num || num <= this.Scrollbar.Minimum)
            return;
          this.Scrollbar.Value = num;
        }
      }
      else
        this.UpdateScrollRange();
    }

    public override int GetScrollHeight(int item)
    {
      return this.viewState.GetItemSize(item);
    }

    public int GetItemOffset(int item)
    {
      return this.viewState.GetItemOffset(item);
    }
  }
}
