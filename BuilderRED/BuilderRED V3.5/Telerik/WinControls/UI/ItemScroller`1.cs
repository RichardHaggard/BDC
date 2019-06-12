// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ItemScroller`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ItemScroller<T> : IEnumerable, IDisposable
  {
    private int itemHeight = 20;
    private int maxItemWidth = -1;
    private int oldScrollValue = -1;
    private bool asynchronousScrolling = true;
    private ITraverser<T> traverser;
    private RadScrollBarElement scrollbar;
    private IVirtualizedElementProvider<T> elementProvider;
    private ItemScrollerScrollModes scrollMode;
    private ToolTip toolTip;
    private SizeF clientSize;
    private int itemSpacing;
    protected int currentItemWidth;
    protected int cachedScrollOffset;
    protected bool scrollbarChanged;
    private ScrollState scrollState;
    private int thumbDelta;
    private Timer thumbTimer;
    private bool suspendScrollbarValueChanged;
    private bool suspendScrollerUpdated;
    private bool disposed;
    private bool isToolTipVisible;
    private bool allowHiddenScrolling;

    public bool AllowHiddenScrolling
    {
      get
      {
        return this.allowHiddenScrolling;
      }
      set
      {
        this.allowHiddenScrolling = value;
      }
    }

    public int MaxItemWidth
    {
      get
      {
        return this.maxItemWidth;
      }
      internal set
      {
        this.maxItemWidth = value;
      }
    }

    public ScrollState ScrollState
    {
      get
      {
        return this.scrollState;
      }
      set
      {
        if (this.scrollState == value)
          return;
        this.scrollState = value;
        this.SetScrollBarVisibility();
      }
    }

    public ITraverser<T> Traverser
    {
      get
      {
        return this.traverser;
      }
      set
      {
        if (this.traverser == value)
          return;
        this.traverser = value;
      }
    }

    public RadScrollBarElement Scrollbar
    {
      get
      {
        return this.scrollbar;
      }
      set
      {
        if (this.scrollbar == value)
          return;
        if (this.thumbTimer == null)
        {
          this.thumbTimer = new Timer();
          this.thumbTimer.Interval = 10;
          this.thumbTimer.Tick += new EventHandler(this.thumbTimer_Tick);
        }
        if (this.scrollbar != null)
        {
          this.scrollbar.Scroll -= new ScrollEventHandler(this.scrollbar_Scroll);
          this.scrollbar.ValueChanged -= new EventHandler(this.scrollbar_ValueChanged);
        }
        this.scrollbar = value;
        if (this.scrollbar == null)
          return;
        this.scrollbar.Scroll += new ScrollEventHandler(this.scrollbar_Scroll);
        this.scrollbar.ValueChanged += new EventHandler(this.scrollbar_ValueChanged);
      }
    }

    public IVirtualizedElementProvider<T> ElementProvider
    {
      get
      {
        return this.elementProvider;
      }
      set
      {
        if (this.elementProvider == value)
          return;
        this.elementProvider = value;
      }
    }

    public ItemScrollerScrollModes ScrollMode
    {
      get
      {
        return this.scrollMode;
      }
      set
      {
        if (this.scrollMode == value)
          return;
        this.scrollMode = value;
        if (value != ItemScrollerScrollModes.Deferred)
          this.DisposeToolTip();
        this.UpdateScrollRange();
      }
    }

    public SizeF ClientSize
    {
      get
      {
        return this.clientSize;
      }
      set
      {
        if (!(this.clientSize != value))
          return;
        this.clientSize = value;
        this.UpdateScrollStep();
      }
    }

    public int ItemHeight
    {
      get
      {
        return this.itemHeight;
      }
      set
      {
        if (this.itemHeight == value)
          return;
        this.itemHeight = value;
        this.UpdateScrollStep();
      }
    }

    public int ItemSpacing
    {
      get
      {
        return this.itemSpacing;
      }
      set
      {
        if (this.itemSpacing == value)
          return;
        this.itemSpacing = value;
        this.UpdateScrollRange();
      }
    }

    public int ScrollOffset
    {
      get
      {
        return this.cachedScrollOffset;
      }
      set
      {
        if (this.cachedScrollOffset == value)
          return;
        this.cachedScrollOffset = value;
        this.OnScrollerUpdated(EventArgs.Empty);
      }
    }

    public object Position
    {
      get
      {
        return this.traverser.Position;
      }
    }

    public ToolTip ToolTip
    {
      get
      {
        if (this.isToolTipVisible)
          return this.toolTip;
        return (ToolTip) null;
      }
      protected set
      {
        this.toolTip = value;
      }
    }

    public bool AsynchronousScrolling
    {
      get
      {
        return this.asynchronousScrolling;
      }
      set
      {
        if (this.asynchronousScrolling == value)
          return;
        this.asynchronousScrolling = value;
        if (this.thumbTimer == null)
          return;
        this.thumbTimer.Enabled = value;
      }
    }

    public event EventHandler ScrollerUpdated;

    public event ToolTipTextNeededEventHandler ToolTipTextNeeded;

    protected virtual void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      ToolTipTextNeededEventHandler toolTipTextNeeded = this.ToolTipTextNeeded;
      if (toolTipTextNeeded == null)
        return;
      toolTipTextNeeded(sender, e);
    }

    protected virtual void OnScrollerUpdated(EventArgs e)
    {
      EventHandler scrollerUpdated = this.ScrollerUpdated;
      if (scrollerUpdated == null || this.suspendScrollerUpdated)
        return;
      scrollerUpdated((object) this, e);
    }

    private void scrollbar_Scroll(object sender, ScrollEventArgs e)
    {
      this.scrollbarChanged = this.UpdateOnScroll(e);
    }

    private void scrollbar_ValueChanged(object sender, EventArgs e)
    {
      if (this.suspendScrollbarValueChanged)
        return;
      if (!this.scrollbarChanged)
      {
        if (this.oldScrollValue == -1)
          this.ScrollTo(this.scrollbar.Value);
        else if (this.oldScrollValue < this.scrollbar.Value)
          this.ScrollDown(this.scrollbar.Value - this.oldScrollValue);
        else if (this.scrollbar.Value == this.scrollbar.Minimum)
          this.ScrollToBegin();
        else
          this.ScrollUp(this.oldScrollValue - this.scrollbar.Value);
      }
      this.OnScrollerUpdated(EventArgs.Empty);
      this.scrollbarChanged = false;
      this.oldScrollValue = this.scrollbar.Value;
    }

    private void thumbTimer_Tick(object sender, EventArgs e)
    {
      if (this.thumbDelta == 0)
        return;
      if (this.thumbDelta > 0)
        this.ScrollDown(this.thumbDelta);
      else
        this.ScrollUp(-this.thumbDelta);
      this.thumbDelta = 0;
      this.thumbTimer.Stop();
      if (this.isToolTipVisible)
        this.ShowToolTip();
      this.OnScrollerUpdated(EventArgs.Empty);
      this.oldScrollValue = this.scrollbar.Value;
      this.suspendScrollbarValueChanged = false;
      this.suspendScrollerUpdated = false;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      if (this.traverser != null)
        return this.traverser.GetEnumerator();
      return (IEnumerator) null;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed || !disposing)
        return;
      if (this.scrollbar != null)
      {
        this.scrollbar.Scroll -= new ScrollEventHandler(this.scrollbar_Scroll);
        this.scrollbar.ValueChanged -= new EventHandler(this.scrollbar_ValueChanged);
        this.scrollbar = (RadScrollBarElement) null;
      }
      if (this.thumbTimer != null)
      {
        this.thumbTimer.Dispose();
        this.thumbTimer = (Timer) null;
      }
      this.DisposeToolTip();
      this.disposed = true;
    }

    private void DisposeToolTip()
    {
      if (this.toolTip == null)
        return;
      this.toolTip.Dispose();
      this.toolTip = (ToolTip) null;
    }

    public virtual bool ScrollToItem(T item)
    {
      return this.ScrollToItem(item, !this.AllowHiddenScrolling);
    }

    public virtual bool ScrollToItem(T item, bool checkScrollVisibility)
    {
      if (checkScrollVisibility && (this.scrollbar == null || this.scrollbar.Visibility == ElementVisibility.Collapsed))
        return false;
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        int num1 = this.scrollbar.Maximum - this.scrollbar.LargeChange + 1;
        if (num1 <= this.scrollbar.Minimum)
          return false;
        if ((object) item == null)
        {
          this.scrollbar.Value = this.scrollbar.Minimum;
          return true;
        }
        object position = this.traverser.Position;
        object obj = (object) null;
        int num2 = 0;
        this.traverser.Reset();
        while (this.traverser.MoveNext())
        {
          int num3 = this.GetScrollHeight(this.traverser.Current) + this.itemSpacing;
          if (item.Equals((object) this.traverser.Current))
          {
            this.traverser.MovePrevious();
            this.cachedScrollOffset = 0;
            this.scrollbarChanged = true;
            if (this.scrollbar.Value <= this.scrollbar.Maximum && num2 < this.scrollbar.Maximum)
            {
              if (num2 < this.scrollbar.Minimum)
                num2 = this.scrollbar.Minimum;
              this.scrollbar.Value = num2;
            }
            if (obj != null)
            {
              this.traverser.Position = obj;
              this.traverser.MovePrevious();
              this.scrollbar.Value = num1;
            }
            return true;
          }
          if (num2 + num3 < num1)
            num2 += num3;
          else if (obj == null)
            obj = this.traverser.Position;
        }
        this.traverser.Position = position;
        return false;
      }
      int num = 0;
      object position1 = this.traverser.Position;
      this.traverser.Reset();
      while (this.traverser.MoveNext())
      {
        if (item.Equals((object) this.traverser.Current))
        {
          this.scrollbarChanged = true;
          this.traverser.MovePrevious();
          if (num < this.scrollbar.Maximum - this.scrollbar.LargeChange + 1)
            this.scrollbar.Value = num;
          else if (this.Scrollbar.Minimum < this.scrollbar.Maximum - this.scrollbar.LargeChange + 1)
          {
            this.scrollbar.Value = this.scrollbar.Maximum - this.scrollbar.LargeChange + 1;
          }
          else
          {
            this.scrollbarChanged = false;
            this.traverser.Position = position1;
            return false;
          }
          this.scrollbarChanged = false;
          return true;
        }
        ++num;
      }
      this.scrollbarChanged = false;
      this.traverser.Position = position1;
      return false;
    }

    protected virtual bool ScrollToBegin()
    {
      this.cachedScrollOffset = 0;
      this.traverser.Reset();
      return true;
    }

    protected virtual bool ScrollToEnd()
    {
      return false;
    }

    protected virtual bool ScrollTo(int position)
    {
      this.ScrollToBegin();
      return this.ScrollDown(position);
    }

    protected virtual bool ScrollDown(int step)
    {
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.scrollMode == ItemScrollerScrollModes.Deferred)
      {
        object position = this.traverser.Position;
        while (step > 0 && this.traverser.MoveNext())
        {
          int num = this.GetScrollHeight(this.traverser.Current) + this.itemSpacing - this.cachedScrollOffset;
          if (num > step)
          {
            this.cachedScrollOffset += step;
            break;
          }
          step -= num;
          this.cachedScrollOffset = 0;
          position = this.traverser.Position;
        }
        this.traverser.Position = position;
        return true;
      }
      while (step-- > 0)
        this.traverser.MoveNext();
      return true;
    }

    public virtual int GetScrollHeight(T item)
    {
      int num = 0;
      if (this.ElementProvider != null)
      {
        SizeF elementSize = this.ElementProvider.GetElementSize(item);
        if (this.scrollbar.ScrollType == ScrollType.Vertical)
        {
          num = (int) elementSize.Height;
          this.currentItemWidth = (int) elementSize.Width;
        }
        else
        {
          num = (int) elementSize.Width;
          this.currentItemWidth = (int) elementSize.Height;
        }
      }
      return num;
    }

    protected virtual bool ScrollUp(int step)
    {
      if (this.scrollMode == ItemScrollerScrollModes.Smooth || this.scrollMode == ItemScrollerScrollModes.Deferred)
      {
        while (step > 0)
        {
          if ((object) this.traverser.Current == null)
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
          int num = this.GetScrollHeight(this.traverser.Current) + this.itemSpacing;
          step -= this.cachedScrollOffset;
          this.cachedScrollOffset = 0;
          if (num >= step)
          {
            this.cachedScrollOffset = num - step;
            step = 0;
          }
          else
            step -= num;
          if (!this.traverser.MovePrevious())
            return true;
        }
        return true;
      }
      if (step <= 0 || (object) this.traverser.Current == null)
        return false;
      while (step-- > 0)
        this.traverser.MovePrevious();
      return true;
    }

    protected virtual bool UpdateOnScroll(ScrollEventArgs e)
    {
      if (e.NewValue != e.OldValue)
      {
        switch (e.Type)
        {
          case ScrollEventType.SmallDecrement:
          case ScrollEventType.LargeDecrement:
            return this.ScrollUp(e.OldValue - e.NewValue);
          case ScrollEventType.SmallIncrement:
          case ScrollEventType.LargeIncrement:
            return this.ScrollDown(e.NewValue - e.OldValue);
          case ScrollEventType.ThumbTrack:
            if (this.asynchronousScrolling)
            {
              if (this.thumbDelta == 0)
              {
                this.suspendScrollerUpdated = this.ScrollMode == ItemScrollerScrollModes.Deferred;
                this.thumbTimer.Start();
              }
              this.thumbDelta += e.NewValue - e.OldValue;
              this.suspendScrollbarValueChanged = true;
            }
            else
            {
              int step = e.NewValue - e.OldValue;
              if (step != 0)
              {
                if (step > 0)
                  this.ScrollDown(step);
                else if (step < 0)
                  this.ScrollUp(-step);
              }
            }
            return true;
          case ScrollEventType.First:
            return this.ScrollToBegin();
          case ScrollEventType.Last:
            return this.ScrollToEnd();
        }
      }
      else if (this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (e.Type == ScrollEventType.ThumbTrack)
          this.ShowToolTip();
        else if (e.Type == ScrollEventType.EndScroll)
        {
          this.HideToolTip();
          this.suspendScrollerUpdated = false;
          this.OnScrollerUpdated(EventArgs.Empty);
        }
      }
      return false;
    }

    public virtual void UpdateScrollRange()
    {
      if (this.scrollbar == null || this.traverser == null)
        return;
      int num1 = 0;
      int num2 = 0;
      object position = this.traverser.Position;
      this.traverser.Reset();
      this.maxItemWidth = 0;
      while (this.traverser.MoveNext())
      {
        int num3 = this.GetScrollHeight(this.traverser.Current) + this.itemSpacing;
        this.maxItemWidth = Math.Max(this.maxItemWidth, this.currentItemWidth);
        num1 += num3;
        ++num2;
      }
      if (num2 > 0)
      {
        if (this.itemSpacing > 0)
          num1 -= this.itemSpacing;
        if (this.itemSpacing == 0)
          --num1;
      }
      this.traverser.Position = position;
      if (num1 < 0)
        return;
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (this.scrollbar.Maximum == num1)
          return;
        this.scrollbar.Maximum = num1;
        this.SetScrollBarVisibility();
        this.UpdateScrollValue();
      }
      else
      {
        if (this.scrollbar.Maximum == num2 - 1 || num2 - 1 <= 0)
          return;
        this.scrollbar.Maximum = num2 - 1;
        this.UpdateScrollStep();
        this.SetScrollBarVisibility();
      }
    }

    public virtual void UpdateScrollValue()
    {
      int position = this.scrollbar.Value;
      int num = this.scrollbar.Maximum - this.scrollbar.LargeChange + 1;
      this.oldScrollValue = -1;
      this.scrollbarChanged = false;
      if (num >= 0 && position > num && position > this.scrollbar.Minimum)
        this.scrollbar.Value = num;
      else if (!this.AllowHiddenScrolling && this.scrollbar.Visibility != ElementVisibility.Visible)
        this.scrollbar.Value = this.scrollbar.Minimum;
      else
        this.ScrollTo(position);
    }

    public virtual void UpdateScrollRange(int width, bool updateScrollValue)
    {
      if (this.scrollbar == null)
        return;
      if (this.ScrollMode == ItemScrollerScrollModes.Smooth || this.ScrollMode == ItemScrollerScrollModes.Deferred)
      {
        if (this.scrollbar.Maximum == width)
          return;
        this.scrollbar.Maximum = width >= 0 ? width : 0;
        this.SetScrollBarVisibility();
        if (updateScrollValue)
        {
          this.UpdateScrollValue();
        }
        else
        {
          int num = this.scrollbar.Maximum - this.scrollbar.LargeChange + 1;
          if (num <= 0 || this.scrollbar.Value <= num || num <= this.scrollbar.Minimum)
            return;
          this.scrollbar.Value = num;
        }
      }
      else
        this.UpdateScrollRange();
    }

    public virtual void UpdateScrollStep()
    {
      if (this.traverser == null || this.scrollbar == null || (double) this.ClientSize.Width <= 0.0 || (double) this.ClientSize.Height <= 0.0)
        return;
      float f = this.ClientSize.Height;
      if (this.scrollbar.ScrollType == ScrollType.Horizontal)
        f = this.ClientSize.Width;
      if (this.scrollMode == ItemScrollerScrollModes.Smooth || this.scrollMode == ItemScrollerScrollModes.Deferred)
      {
        this.scrollbar.SmallChange = this.ItemHeight + this.ItemSpacing;
        this.scrollbar.LargeChange = !float.IsPositiveInfinity(f) ? (int) f : 0;
        this.SetScrollBarVisibility();
        this.UpdateScrollValue();
      }
      else
      {
        int num1 = 0;
        int num2 = 0;
        object position = this.traverser.Position;
        float num3 = this.scrollbar.ScrollType == ScrollType.Vertical ? this.clientSize.Height : this.clientSize.Width;
        while (this.traverser.MoveNext())
        {
          num1 += this.GetScrollHeight(this.traverser.Current) + this.itemSpacing;
          if ((double) num1 < (double) num3)
            ++num2;
          else
            break;
        }
        this.traverser.Position = position;
        this.scrollbar.SmallChange = 1;
        while (this.scrollbar.Value + num2 > this.scrollbar.Maximum - 1)
          --num2;
        if (this.scrollbar.Value + num2 < this.scrollbar.Maximum - 1 && num2 > 1)
          this.scrollbar.LargeChange = num2;
        this.SetScrollBarVisibility();
      }
    }

    protected void SetScrollBarVisibility()
    {
      if (this.scrollbar.ScrollType == ScrollType.Horizontal && float.IsPositiveInfinity(this.clientSize.Width) || this.scrollbar.ScrollType == ScrollType.Vertical && float.IsPositiveInfinity(this.clientSize.Height))
        this.scrollbar.Visibility = ElementVisibility.Collapsed;
      else if (this.scrollState == ScrollState.AlwaysShow || this.scrollState == ScrollState.AutoHide && this.scrollbar.LargeChange < this.scrollbar.Maximum)
        this.scrollbar.Visibility = ElementVisibility.Visible;
      else
        this.scrollbar.Visibility = ElementVisibility.Collapsed;
    }

    protected virtual void ShowToolTip()
    {
      Control control = this.scrollbar.ElementTree.Control;
      if (control == null)
        return;
      if (this.toolTip == null)
        this.toolTip = new ToolTip();
      string toolTipText = this.GetToolTipText();
      int x = 0;
      int y = 0;
      using (Graphics graphics = control.CreateGraphics())
      {
        SizeF sizeF = graphics.MeasureString(toolTipText, SystemFonts.DialogFont);
        if (this.scrollbar.ScrollType == ScrollType.Vertical)
        {
          x = this.scrollbar.ControlBoundingRectangle.Left - (int) sizeF.Width - 20;
          y = this.scrollbar.ThumbElement.ControlBoundingRectangle.Y + this.scrollbar.ThumbElement.Size.Height / 2 - 10;
        }
        else
        {
          int height = (int) sizeF.Height;
          x = this.scrollbar.ThumbElement.ControlBoundingRectangle.X + this.scrollbar.ThumbElement.Size.Width / 2 - 20;
          y = this.scrollbar.ControlBoundingRectangle.Top - height - 10;
        }
      }
      this.toolTip.Show(toolTipText, (IWin32Window) control, x, y);
      this.isToolTipVisible = true;
    }

    protected virtual string GetToolTipText()
    {
      int currentItemIndex = this.GetCurrentItemIndex();
      ItemScrollerToolTipTextNeededEventArgs<T> textNeededEventArgs = new ItemScrollerToolTipTextNeededEventArgs<T>(this.toolTip, currentItemIndex, this.traverser.Current, "Position: " + (object) currentItemIndex);
      this.OnToolTipTextNeeded((object) this, (ToolTipTextNeededEventArgs) textNeededEventArgs);
      return textNeededEventArgs.ToolTipText;
    }

    protected virtual int GetCurrentItemIndex()
    {
      object current = (object) this.traverser.Current;
      if (current == null)
        return 0;
      object position = this.traverser.Position;
      this.traverser.Reset();
      int num = -1;
      while (this.traverser.MoveNext())
      {
        ++num;
        if (object.Equals((object) this.traverser.Current, current))
        {
          this.traverser.Position = position;
          return num + 1;
        }
      }
      this.traverser.Position = position;
      return -1;
    }

    protected virtual void HideToolTip()
    {
      if (this.toolTip != null)
        this.toolTip.Hide((IWin32Window) this.scrollbar.ElementTree.Control);
      this.isToolTipVisible = false;
    }
  }
}
