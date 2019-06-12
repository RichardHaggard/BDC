// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ScrollService
  {
    private DateTime lastMove = DateTime.Now;
    private bool enableInertia = true;
    private double deceleration = 0.001;
    private double velocityFactor = 1.3;
    private double lastDirection = 1.0;
    private RadElement owner;
    private Point lastLocation;
    private Timer timer;
    private double delta;
    private RadScrollBarElement scrollBar;
    private bool running;
    private double velocity;
    private DateTime inertiaStart;
    private double totalInertiaOffset;
    private double previousInertiaOffset;
    private DateTime lastScroll;
    private int bufferedSteps;

    public ScrollService(RadElement owner, RadScrollBarElement scrollBar)
    {
      this.owner = owner;
      this.scrollBar = scrollBar;
      this.timer = new Timer();
      this.timer.Interval = 5;
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    public double VelocityFactor
    {
      get
      {
        return this.velocityFactor;
      }
      set
      {
        this.velocityFactor = value;
      }
    }

    public double Deceleration
    {
      get
      {
        return this.deceleration;
      }
      set
      {
        this.deceleration = value;
      }
    }

    public bool EnableInertia
    {
      get
      {
        return this.enableInertia;
      }
      set
      {
        this.enableInertia = value;
      }
    }

    public int Interval
    {
      get
      {
        return this.timer.Interval;
      }
      set
      {
        this.timer.Interval = value;
      }
    }

    public RadElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public bool IsScrolling
    {
      get
      {
        return this.running;
      }
    }

    public void Stop()
    {
      this.timer.Stop();
      this.running = false;
      this.delta = 0.0;
    }

    public void MouseDown(Point location)
    {
      this.Stop();
      this.lastLocation = location;
    }

    public void MouseUp(Point point)
    {
      if (Math.Abs(this.delta) > 0.0 && (DateTime.Now - this.lastMove).TotalMilliseconds < 100.0)
      {
        this.timer.Start();
        this.running = true;
        this.inertiaStart = DateTime.Now;
        this.totalInertiaOffset = 0.0;
        this.previousInertiaOffset = 0.0;
      }
      else
        this.Stop();
      this.lastLocation = point;
    }

    public void MouseMove(Point point)
    {
      RadItem owner = this.owner as RadItem;
      bool flag = owner != null && owner.Capture;
      if (Control.MouseButtons != MouseButtons.Left || !this.owner.ContainsMouse && !flag || this.timer.Enabled)
        return;
      if (!this.running)
      {
        this.running = true;
        this.lastLocation = point;
        this.delta = 0.0;
      }
      else
      {
        this.delta = this.scrollBar.ScrollType != ScrollType.Horizontal ? (double) (-point.Y + this.lastLocation.Y) : (double) (-point.X + this.lastLocation.X);
        DateTime now = DateTime.Now;
        if ((now - this.lastMove).TotalMilliseconds > 0.0)
          this.velocity = this.velocityFactor * Math.Abs(this.delta) / (DateTime.Now - this.lastMove).TotalMilliseconds;
        if (this.delta != 0.0)
          this.lastDirection = Math.Abs(this.delta) / this.delta;
        if ((now - this.lastMove).TotalMilliseconds > 0.0)
          this.lastMove = DateTime.Now;
        this.lastLocation = point;
        this.SetScrollValue((int) this.delta, true);
      }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (this.EnableInertia)
      {
        double totalMilliseconds = (DateTime.Now - this.inertiaStart).TotalMilliseconds;
        double num = (this.velocity * totalMilliseconds - this.deceleration * totalMilliseconds * totalMilliseconds) * this.lastDirection;
        this.totalInertiaOffset += num - this.previousInertiaOffset;
        if (Math.Sign(num - this.previousInertiaOffset) != Math.Sign(this.lastDirection))
        {
          this.Stop();
        }
        else
        {
          this.previousInertiaOffset = num;
          if ((int) this.totalInertiaOffset == 0)
            return;
          if (!this.SetScrollValue((int) this.totalInertiaOffset, false))
            this.Stop();
          this.totalInertiaOffset -= (double) (int) this.totalInertiaOffset;
        }
      }
      else
      {
        int smallChange = this.scrollBar.SmallChange;
        int step = this.scrollBar.ScrollType != ScrollType.Horizontal ? (int) ((double) smallChange * this.delta / ((double) this.owner.Size.Height / 20.0)) : (int) ((double) smallChange * this.delta / ((double) this.owner.Size.Width / 20.0));
        if (this.SetScrollValue(step, false) && step != 0)
          return;
        this.timer.Stop();
        this.running = false;
      }
    }

    private bool SetScrollValue(int step, bool buffered)
    {
      DateTime now = DateTime.Now;
      this.bufferedSteps += step;
      if (now - this.lastScroll < TimeSpan.FromMilliseconds(40.0) && buffered)
        return true;
      this.lastScroll = now;
      bool flag = true;
      int num = this.scrollBar.Value + this.bufferedSteps;
      if (!buffered)
        num = this.scrollBar.Value + step;
      this.bufferedSteps = 0;
      if (num > this.scrollBar.Maximum - this.scrollBar.LargeChange + 1)
      {
        num = this.scrollBar.Maximum - this.scrollBar.LargeChange + 1;
        flag = false;
      }
      if (num < this.scrollBar.Minimum)
      {
        num = this.scrollBar.Minimum;
        flag = false;
      }
      this.scrollBar.Value = num;
      return flag;
    }
  }
}
