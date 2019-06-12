// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.CurrentChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Data
{
  public class CurrentChangedEventArgs : EventArgs
  {
    private CurrentChangeReason reason;

    public CurrentChangedEventArgs(CurrentChangeReason reason)
    {
      this.reason = reason;
    }

    public CurrentChangeReason Reason
    {
      get
      {
        return this.reason;
      }
    }
  }
}
