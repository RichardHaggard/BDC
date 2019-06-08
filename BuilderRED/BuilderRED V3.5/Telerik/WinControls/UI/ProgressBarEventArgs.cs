// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ProgressBarEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ProgressBarEventArgs : EventArgs
  {
    private ProgressBarSenderEvent senderEvent;

    public ProgressBarEventArgs(ProgressBarSenderEvent senderEvent)
    {
      this.senderEvent = senderEvent;
    }

    public ProgressBarSenderEvent SenderEvent
    {
      get
      {
        return this.senderEvent;
      }
      set
      {
        this.senderEvent = value;
      }
    }
  }
}
