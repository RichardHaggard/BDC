// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RoutedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class RoutedEventArgs : EventArgs
  {
    private EventArgs originalEventArgs = EventArgs.Empty;
    private bool canceled;
    private RoutingDirection direction;
    private RoutedEvent routedEvent;

    public RoutedEventArgs(EventArgs args, RoutedEvent routedEvent)
    {
      this.originalEventArgs = args;
      this.RoutedEvent = routedEvent;
    }

    public EventArgs OriginalEventArgs
    {
      get
      {
        return this.originalEventArgs;
      }
      set
      {
        this.originalEventArgs = value;
      }
    }

    public RoutedEvent RoutedEvent
    {
      get
      {
        return this.routedEvent;
      }
      set
      {
        this.routedEvent = value;
      }
    }

    public bool Canceled
    {
      get
      {
        return this.canceled;
      }
      set
      {
        this.canceled = value;
      }
    }

    public RoutingDirection Direction
    {
      get
      {
        return this.direction;
      }
      set
      {
        this.direction = value;
      }
    }
  }
}
