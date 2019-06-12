// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadControlAnimationTimer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadControlAnimationTimer
  {
    [ThreadStatic]
    private static Timer internalTimer = (Timer) null;
    [ThreadStatic]
    private static int timersReferenceCount = 0;
    private static Stopwatch tickCounter = Stopwatch.StartNew();
    private int interval;
    private bool enabled;
    private long lastTicks;
    private static int suspendedCount;

    public int Interval
    {
      get
      {
        return this.interval;
      }
      set
      {
        this.interval = value;
      }
    }

    public bool IsRunning
    {
      get
      {
        return this.enabled;
      }
    }

    public event EventHandler Tick;

    public void Start()
    {
      if (this.enabled)
        return;
      ++RadControlAnimationTimer.timersReferenceCount;
      this.enabled = true;
      this.EnsureTimer();
      RadControlAnimationTimer.internalTimer.Tick += new EventHandler(this.internalTimer_Tick);
    }

    public void Stop()
    {
      if (!this.enabled)
        return;
      --RadControlAnimationTimer.timersReferenceCount;
      this.enabled = false;
      if (RadControlAnimationTimer.internalTimer == null)
        return;
      RadControlAnimationTimer.internalTimer.Tick -= new EventHandler(this.internalTimer_Tick);
      if (RadControlAnimationTimer.timersReferenceCount != 0)
        return;
      RadControlAnimationTimer.internalTimer.Stop();
      RadControlAnimationTimer.internalTimer.Dispose();
      RadControlAnimationTimer.internalTimer = (Timer) null;
    }

    protected void OnTick(EventArgs e)
    {
      EventHandler tick = this.Tick;
      if (tick == null)
        return;
      tick((object) this, e);
    }

    private void internalTimer_Tick(object sender, EventArgs e)
    {
      if (RadControlAnimationTimer.suspendedCount > 0 || !this.enabled)
        return;
      long elapsedMilliseconds = RadControlAnimationTimer.tickCounter.ElapsedMilliseconds;
      if (elapsedMilliseconds - this.lastTicks <= (long) this.interval)
        return;
      this.lastTicks = elapsedMilliseconds;
      this.OnTick(EventArgs.Empty);
    }

    private void EnsureTimer()
    {
      if (RadControlAnimationTimer.internalTimer == null)
      {
        RadControlAnimationTimer.internalTimer = new Timer();
        RadControlAnimationTimer.internalTimer.Interval = 5;
        RadControlAnimationTimer.internalTimer.Start();
      }
      else
      {
        if (RadControlAnimationTimer.internalTimer.Enabled)
          return;
        RadControlAnimationTimer.internalTimer.Start();
      }
    }

    public static void Suspend()
    {
      ++RadControlAnimationTimer.suspendedCount;
    }

    internal static void Resume()
    {
      if (RadControlAnimationTimer.suspendedCount <= 0)
        return;
      --RadControlAnimationTimer.suspendedCount;
    }
  }
}
