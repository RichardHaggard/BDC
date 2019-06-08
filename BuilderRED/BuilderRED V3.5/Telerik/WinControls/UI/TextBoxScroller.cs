// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxScroller
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TextBoxScroller : IDisposable
  {
    private SizeF clientSize;
    private SizeF desiredSize;
    private ScrollState scrollState;
    private readonly RadScrollBarElement scrollBar;
    private Timer thumbTimer;
    private int thumbDelta;
    private int update;

    public TextBoxScroller(RadScrollBarElement scrollBar)
    {
      this.scrollBar = scrollBar;
      this.scrollBar.ClampValue = true;
      this.scrollBar.ValueChanged += new EventHandler(this.OnScrollBarValueChanged);
      this.scrollBar.Scroll += new ScrollEventHandler(this.OnScrollBarScroll);
      this.scrollBar.Visibility = ElementVisibility.Collapsed;
      this.thumbTimer = new Timer();
      this.thumbTimer.Interval = 10;
      this.thumbTimer.Enabled = false;
      this.thumbTimer.Tick += new EventHandler(this.OnThumbTimerTick);
      this.scrollState = ScrollState.AutoHide;
    }

    ~TextBoxScroller()
    {
      this.Dispose(false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.thumbTimer != null)
      {
        this.thumbTimer.Tick -= new EventHandler(this.OnThumbTimerTick);
        this.thumbTimer.Dispose();
        this.thumbTimer = (Timer) null;
      }
      this.scrollBar.ValueChanged -= new EventHandler(this.OnScrollBarValueChanged);
      this.scrollBar.Scroll -= new ScrollEventHandler(this.OnScrollBarScroll);
    }

    public int MaxValue
    {
      get
      {
        return this.scrollBar.Maximum - this.scrollBar.LargeChange + 1;
      }
    }

    public virtual int Value
    {
      get
      {
        return this.ScrollBar.Value;
      }
      set
      {
        if (value < this.scrollBar.Minimum)
          value = this.scrollBar.Minimum;
        if (value > this.MaxValue)
        {
          int num = this.MaxValue - 1;
          if (num != 0)
            ++num;
          value = num;
        }
        int num1 = this.scrollBar.Value;
        this.scrollBar.Value = value;
      }
    }

    public RadScrollBarElement ScrollBar
    {
      get
      {
        return this.scrollBar;
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

    public SizeF ClientSize
    {
      get
      {
        return this.clientSize;
      }
    }

    public SizeF DesiredSize
    {
      get
      {
        return this.desiredSize;
      }
    }

    public int LargeChange
    {
      get
      {
        return this.scrollBar.LargeChange;
      }
      set
      {
        this.scrollBar.LargeChange = value;
      }
    }

    public int SmallChange
    {
      get
      {
        return this.scrollBar.SmallChange;
      }
      set
      {
        this.scrollBar.SmallChange = value;
      }
    }

    public event EventHandler ScrollerUpdated;

    protected virtual void OnScrollerUpdated(EventArgs e)
    {
      EventHandler scrollerUpdated = this.ScrollerUpdated;
      if (scrollerUpdated == null)
        return;
      scrollerUpdated((object) this, e);
    }

    private void OnScrollBarScroll(object sender, ScrollEventArgs e)
    {
      if (e.Type != ScrollEventType.ThumbTrack || e.OldValue == e.NewValue)
        return;
      if (this.thumbDelta == 0)
      {
        this.SuspendNotifications();
        this.thumbTimer.Start();
      }
      this.thumbDelta += e.NewValue - e.OldValue;
    }

    private void OnThumbTimerTick(object sender, EventArgs e)
    {
      if (this.thumbDelta == 0)
        return;
      this.thumbTimer.Stop();
      this.update = 0;
      this.thumbDelta = 0;
      this.OnScrollerUpdated(EventArgs.Empty);
    }

    private void OnScrollBarValueChanged(object sender, EventArgs e)
    {
      if (this.update != 0)
        return;
      this.OnScrollerUpdated(EventArgs.Empty);
    }

    public void SuspendNotifications()
    {
      ++this.update;
    }

    public void ResumeNotifications()
    {
      if (this.update <= 0)
        return;
      --this.update;
    }

    public void UpdateScrollRange(SizeF clientSize, SizeF desiredSize)
    {
      bool flag = false;
      if (clientSize != SizeF.Empty && this.clientSize != clientSize)
      {
        this.clientSize = clientSize;
        flag = true;
      }
      if (this.desiredSize != desiredSize)
      {
        this.desiredSize = desiredSize;
        flag = true;
      }
      if (!flag)
        return;
      this.UpdateScrollBar();
    }

    protected virtual void UpdateScrollBar()
    {
      this.SuspendNotifications();
      bool flag = this.scrollBar.Value != this.scrollBar.Minimum && this.scrollBar.Value == this.MaxValue;
      float num1 = this.DesiredSize.Height;
      float num2 = this.ClientSize.Height;
      if (this.scrollBar.ScrollType == ScrollType.Horizontal)
      {
        num1 = (float) Math.Ceiling((double) this.DesiredSize.Width);
        num2 = (float) Math.Floor((double) this.ClientSize.Width);
      }
      this.scrollBar.Maximum = (int) num1;
      this.scrollBar.LargeChange = (int) num2;
      this.scrollBar.SmallChange = 20;
      if (flag)
        this.Value = this.MaxValue;
      this.SetScrollBarVisibility();
      if (this.scrollState == ScrollState.AutoHide && this.scrollBar.Visibility == ElementVisibility.Collapsed)
        this.scrollBar.Value = this.scrollBar.Minimum;
      this.ResumeNotifications();
    }

    protected virtual void SetScrollBarVisibility()
    {
      RadTextBoxControlElement parent = this.scrollBar.Parent as RadTextBoxControlElement;
      if ((parent == null || parent.ViewElement.Multiline) && (this.scrollState == ScrollState.AlwaysShow || this.scrollState == ScrollState.AutoHide && this.scrollBar.LargeChange < this.scrollBar.Maximum))
        this.scrollBar.Visibility = ElementVisibility.Visible;
      else
        this.scrollBar.Visibility = ElementVisibility.Collapsed;
    }
  }
}
