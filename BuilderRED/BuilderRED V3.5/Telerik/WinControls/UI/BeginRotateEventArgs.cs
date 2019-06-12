// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BeginRotateEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class BeginRotateEventArgs : CancelEventArgs
  {
    private int from;
    private int to;

    public BeginRotateEventArgs(int from, int to)
    {
      this.from = from;
      this.to = to;
    }

    public int From
    {
      get
      {
        return this.from;
      }
    }

    public int To
    {
      get
      {
        return this.to;
      }
      set
      {
        this.to = value;
      }
    }
  }
}
