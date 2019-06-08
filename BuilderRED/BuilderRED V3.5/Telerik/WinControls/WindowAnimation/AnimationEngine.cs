// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.WindowAnimation.AnimationEngine
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Threading;

namespace Telerik.WinControls.WindowAnimation
{
  public class AnimationEngine : RadObject
  {
    protected StandardEasingCalculator calculator = new StandardEasingCalculator();
    protected int frames = 1;
    protected ManualResetEvent threadStart = new ManualResetEvent(false);
    private static readonly object AnimationFinishedEventKey = new object();
    private static readonly object AnimatingEventKey = new object();
    protected Thread thread;

    [Category("Action")]
    [Browsable(true)]
    public event AnimationEventHandler AnimationFinished
    {
      add
      {
        this.Events.AddHandler(AnimationEngine.AnimationFinishedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(AnimationEngine.AnimationFinishedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnAnimationFinished(AnimationEventArgs e)
    {
      AnimationEventHandler animationEventHandler = (AnimationEventHandler) this.Events[AnimationEngine.AnimationFinishedEventKey];
      if (animationEventHandler == null)
        return;
      animationEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnAnimating(AnimationEventArgs e)
    {
      AnimationEventHandler animationEventHandler = (AnimationEventHandler) this.Events[AnimationEngine.AnimatingEventKey];
      if (animationEventHandler == null)
        return;
      animationEventHandler((object) this, e);
    }

    [Browsable(true)]
    [Category("Action")]
    public event AnimationEventHandler Animating
    {
      add
      {
        this.Events.AddHandler(AnimationEngine.AnimatingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(AnimationEngine.AnimatingEventKey, (Delegate) value);
      }
    }

    protected override void DisposeUnmanagedResources()
    {
      base.DisposeUnmanagedResources();
      this.threadStart?.Dispose();
    }

    public RadEasingType EasingType
    {
      get
      {
        if (this.calculator != null)
          return this.calculator.EasingType;
        return RadEasingType.Default;
      }
      set
      {
        if (this.calculator == null || this.calculator.EasingType == value)
          return;
        this.calculator.EasingType = value;
      }
    }

    public int AnimationFrames
    {
      get
      {
        return this.frames;
      }
      set
      {
        if (value == 0)
          throw new InvalidOperationException("Frames can not be zero");
        this.frames = value;
      }
    }

    public int AnimationStep
    {
      get
      {
        return 1000 / this.frames;
      }
    }

    public void Start()
    {
      if (this.frames == 0)
        throw new InvalidOperationException("Frames can not be zero");
      this.threadStart.Reset();
      this.thread = new Thread(new ThreadStart(this.Animate));
      this.thread.IsBackground = true;
      this.thread.Start();
      this.threadStart.WaitOne();
    }

    public void Stop()
    {
      this.thread.Abort();
    }

    protected virtual void Animate()
    {
      this.threadStart.Set();
    }
  }
}
