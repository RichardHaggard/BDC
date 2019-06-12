// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.DisposableObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls
{
  [CLSCompliant(true)]
  [Serializable]
  public class DisposableObject : IDisposable
  {
    private static object syncRoot = new object();
    private static readonly object DisposedEventKey = new object();
    private static readonly object DisposingEventKey = new object();
    internal const long DisposingStateKey = 1;
    internal const long DisposedStateKey = 2;
    internal const long DisposableObjectLastStateKey = 2;
    [NonSerialized]
    private RadBitVector64 bitStateVector;
    [NonSerialized]
    private EventHandlerList events;

    public DisposableObject()
    {
      this.bitStateVector = new RadBitVector64(0L);
    }

    protected internal bool GetBitState(long key)
    {
      return this.bitStateVector[key];
    }

    protected internal virtual void SetBitState(long key, bool value)
    {
      if (this.bitStateVector[key] == value)
        return;
      bool oldValue = this.bitStateVector[key];
      this.bitStateVector[key] = value;
      this.OnBitStateChanged(key, oldValue, value);
    }

    protected virtual void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
    }

    [CLSCompliant(false)]
    protected RadBitVector64 BitState
    {
      get
      {
        return this.bitStateVector;
      }
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
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler Disposed
    {
      add
      {
        this.Events.AddHandler(DisposableObject.DisposedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(DisposableObject.DisposedEventKey, (Delegate) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler Disposing
    {
      add
      {
        this.Events.AddHandler(DisposableObject.DisposingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(DisposableObject.DisposingEventKey, (Delegate) value);
      }
    }

    [Browsable(false)]
    public bool IsDisposing
    {
      get
      {
        return this.bitStateVector[1L];
      }
    }

    [Browsable(false)]
    public bool IsDisposed
    {
      get
      {
        return this.bitStateVector[2L];
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected void Dispose(bool disposing)
    {
      if (this.IsDisposing || this.IsDisposed)
        return;
      this.SetBitState(1L, true);
      if (this.events != null)
      {
        EventHandler eventHandler = this.events[DisposableObject.DisposingEventKey] as EventHandler;
        if (eventHandler != null)
          eventHandler((object) this, EventArgs.Empty);
      }
      this.PerformDispose(disposing);
      this.SetBitState(1L, false);
      this.SetBitState(2L, true);
    }

    protected virtual void PerformDispose(bool disposing)
    {
      lock (DisposableObject.syncRoot)
      {
        if (disposing)
          this.DisposeManagedResources();
        this.DisposeUnmanagedResources();
      }
    }

    protected virtual void DisposeManagedResources()
    {
      if (this.events == null)
        return;
      EventHandler eventHandler = this.events[DisposableObject.DisposedEventKey] as EventHandler;
      this.events.Dispose();
      if (eventHandler == null)
        return;
      eventHandler((object) this, EventArgs.Empty);
    }

    protected virtual void DisposeUnmanagedResources()
    {
    }
  }
}
