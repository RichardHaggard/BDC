// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadServiceStartingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  public class RadServiceStartingEventArgs : CancelEventArgs
  {
    private object context;

    public RadServiceStartingEventArgs(object context)
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
  }
}
