// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TickFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TickFormattingEventArgs : EventArgs
  {
    private TrackBarTickElement tickElement;
    private int tickNumber;

    public TickFormattingEventArgs(TrackBarTickElement tickElement)
      : this(tickElement, -1)
    {
    }

    public TickFormattingEventArgs(TrackBarTickElement tickElement, int tickNumber)
    {
      this.tickElement = tickElement;
      this.tickNumber = tickNumber;
    }

    public virtual TrackBarTickElement TickElement
    {
      get
      {
        return this.tickElement;
      }
    }

    public int TickNumber
    {
      get
      {
        return this.tickNumber;
      }
    }
  }
}
