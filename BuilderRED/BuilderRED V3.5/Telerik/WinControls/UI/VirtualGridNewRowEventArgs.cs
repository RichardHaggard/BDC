// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridNewRowEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class VirtualGridNewRowEventArgs : EventArgs
  {
    private Dictionary<int, object> newValues;

    public VirtualGridNewRowEventArgs(Dictionary<int, object> newValues)
    {
      this.newValues = newValues;
    }

    public Dictionary<int, object> NewValues
    {
      get
      {
        return this.newValues;
      }
    }
  }
}
