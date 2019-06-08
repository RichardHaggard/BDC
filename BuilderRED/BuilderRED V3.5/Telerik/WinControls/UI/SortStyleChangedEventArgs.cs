// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SortStyleChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class SortStyleChangedEventArgs : EventArgs
  {
    private SortStyle style;

    public SortStyleChangedEventArgs(SortStyle sortStyle)
    {
      this.style = sortStyle;
    }

    public SortStyle SortStyle
    {
      get
      {
        return this.style;
      }
    }
  }
}
