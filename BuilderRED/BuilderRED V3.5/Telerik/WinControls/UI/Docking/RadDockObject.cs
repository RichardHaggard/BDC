// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Docking.RadDockObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI.Docking
{
  [Serializable]
  public class RadDockObject : IDisposable, INotifyPropertyChanged, INotifyPropertyChangingEx
  {
    private static object EventDisposed = new object();
    private static object EventPropertyChanging = new object();
    private static object EventPropertyChanged = new object();
    [NonSerialized]
    private bool disposing;
    [NonSerialized]
    private bool isDisposed;
    [NonSerialized]
    private EventHandlerList events;

    public event EventHandler Disposed
    {
      add
      {
        this.Events.AddHandler(RadDockObject.EventDisposed, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDockObject.EventDisposed, (Delegate) value);
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected void Dispose(bool managed)
    {
      if (this.isDisposed || this.disposing)
        return;
      this.disposing = true;
      if (managed)
        this.DisposeManagedResources();
      this.DisposeUnmanagedResources();
      this.disposing = false;
      this.isDisposed = true;
    }

    protected virtual void DisposeManagedResources()
    {
      if (this.events == null)
        return;
      EventHandler eventHandler = this.events[RadDockObject.EventDisposed] as EventHandler;
      this.events.Dispose();
      if (eventHandler == null)
        return;
      eventHandler((object) this, EventArgs.Empty);
    }

    protected virtual void DisposeUnmanagedResources()
    {
    }

    public event PropertyChangingEventHandlerEx PropertyChanging
    {
      add
      {
        this.Events.AddHandler(RadDockObject.EventPropertyChanging, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDockObject.EventPropertyChanging, (Delegate) value);
      }
    }

    public event PropertyChangedEventHandler PropertyChanged
    {
      add
      {
        this.Events.AddHandler(RadDockObject.EventPropertyChanged, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDockObject.EventPropertyChanged, (Delegate) value);
      }
    }

    protected virtual bool OnPropertyChanging(string propName)
    {
      PropertyChangingEventHandlerEx changingEventHandlerEx = this.Events[RadDockObject.EventPropertyChanging] as PropertyChangingEventHandlerEx;
      if (changingEventHandlerEx != null)
        changingEventHandlerEx((object) this, new PropertyChangingEventArgsEx(propName));
      return true;
    }

    protected virtual void OnPropertyChanged(string propName)
    {
      PropertyChangedEventHandler changedEventHandler = this.Events[RadDockObject.EventPropertyChanged] as PropertyChangedEventHandler;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, new PropertyChangedEventArgs(propName));
    }

    protected virtual bool ShouldSerializeProperty(string propName)
    {
      return false;
    }

    protected EventHandlerList Events
    {
      get
      {
        if (this.events == null)
          this.events = new EventHandlerList();
        return this.events;
      }
    }

    [Browsable(false)]
    public bool Disposing
    {
      get
      {
        return this.disposing;
      }
    }

    [Browsable(false)]
    public bool IsDisposed
    {
      get
      {
        return this.isDisposed;
      }
    }
  }
}
