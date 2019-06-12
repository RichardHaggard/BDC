// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public abstract class RadService : RadObject
  {
    private static readonly object StartingEventKey = new object();
    private static readonly object StartedEventKey = new object();
    private static readonly object StoppingEventKey = new object();
    private static readonly object StoppedEventKey = new object();
    private static readonly object EventPropertyChanging = new object();
    private static readonly object EventPropertyChanged = new object();
    private bool enabled;
    private RadServiceState state;
    private object context;

    protected RadService()
    {
      this.state = RadServiceState.Initial;
      this.enabled = true;
    }

    public virtual bool CanOperate()
    {
      return this.enabled;
    }

    public void Start(object context)
    {
      if (!this.CanStart(context))
        return;
      RadServiceStartingEventArgs e = new RadServiceStartingEventArgs(context);
      this.OnStarting(e);
      if (e.Cancel)
        return;
      this.SetContext(context);
      this.state = RadServiceState.Working;
      this.PerformStart();
      this.OnStarted();
    }

    public void Stop(bool commit)
    {
      if (this.state != RadServiceState.Paused && this.state != RadServiceState.Working)
        return;
      RadServiceStoppingEventArgs e = new RadServiceStoppingEventArgs(commit);
      this.OnStopping(e);
      if (e.Cancel)
        return;
      this.state = RadServiceState.Stopped;
      if (e.Commit)
        this.Commit();
      else
        this.Abort();
      this.PerformStop();
      this.SetContext((object) null);
      this.OnStopped();
    }

    public void Pause()
    {
      if (!this.Enabled || this.state != RadServiceState.Working)
        return;
      this.state = RadServiceState.Paused;
      this.PerformPause();
    }

    public void Resume()
    {
      if (!this.Enabled || this.state == RadServiceState.Working || this.state != RadServiceState.Paused)
        return;
      this.state = RadServiceState.Working;
      this.PerformResume();
    }

    protected virtual bool CanStart(object context)
    {
      if (!this.enabled || this.IsDesignMode && !this.AvailableAtDesignTime)
        return false;
      if (!this.IsContextValid(context))
        throw new InvalidOperationException("Invalid context for service " + this.ToString());
      if (this.state != RadServiceState.Initial)
        return this.state == RadServiceState.Stopped;
      return true;
    }

    protected virtual void OnStarted()
    {
      EventHandler eventHandler = this.Events[RadService.StartedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, EventArgs.Empty);
    }

    protected virtual void OnStarting(RadServiceStartingEventArgs e)
    {
      EventHandler<RadServiceStartingEventArgs> eventHandler = this.Events[RadService.StartingEventKey] as EventHandler<RadServiceStartingEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnStopped()
    {
      EventHandler eventHandler = this.Events[RadService.StoppedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, EventArgs.Empty);
    }

    protected virtual void OnStopping(RadServiceStoppingEventArgs e)
    {
      EventHandler<RadServiceStoppingEventArgs> eventHandler = this.Events[RadService.StoppingEventKey] as EventHandler<RadServiceStoppingEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual bool IsContextValid(object context)
    {
      return true;
    }

    protected virtual void PerformStart()
    {
    }

    protected virtual void PerformStop()
    {
    }

    protected virtual void Abort()
    {
    }

    protected virtual void Commit()
    {
    }

    protected virtual void PerformResume()
    {
    }

    protected virtual void PerformPause()
    {
    }

    protected virtual void SetContext(object context)
    {
      this.context = context;
    }

    public object Context
    {
      get
      {
        return this.context;
      }
    }

    public event EventHandler<RadServiceStartingEventArgs> Starting
    {
      add
      {
        this.Events.AddHandler(RadService.StartingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadService.StartingEventKey, (Delegate) value);
      }
    }

    public event EventHandler Started
    {
      add
      {
        this.Events.AddHandler(RadService.StartedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadService.StartedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadServiceStoppingEventArgs> Stopping
    {
      add
      {
        this.Events.AddHandler(RadService.StoppingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadService.StoppingEventKey, (Delegate) value);
      }
    }

    public event EventHandler Stopped
    {
      add
      {
        this.Events.AddHandler(RadService.StoppedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadService.StoppedEventKey, (Delegate) value);
      }
    }

    public virtual bool AvailableAtDesignTime
    {
      get
      {
        return false;
      }
    }

    public RadServiceState State
    {
      get
      {
        return this.state;
      }
    }

    public virtual string Name
    {
      get
      {
        return this.GetType().Name;
      }
    }

    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        if (this.enabled == value)
          return;
        this.enabled = value;
        this.OnEnabledChanged();
      }
    }

    protected virtual void OnEnabledChanged()
    {
      if (this.state != RadServiceState.Working && this.state != RadServiceState.Paused || this.Enabled)
        return;
      this.Stop(false);
    }
  }
}
