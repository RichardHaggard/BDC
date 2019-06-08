// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarNavigatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class CalendarNavigatingEventArgs : CancelEventArgs
  {
    private DateTime startDate;
    private bool isFastNavigation;
    private CalendarNavigationDirection direction;

    public CalendarNavigatingEventArgs(
      DateTime startDate,
      CalendarNavigationDirection direction,
      bool isFastNavigation)
    {
      this.startDate = startDate;
      this.direction = direction;
      this.isFastNavigation = isFastNavigation;
    }

    public bool IsFastNavigation
    {
      get
      {
        return this.isFastNavigation;
      }
    }

    public CalendarNavigationDirection Direction
    {
      get
      {
        return this.direction;
      }
    }

    public DateTime StartDate
    {
      get
      {
        return this.startDate;
      }
      set
      {
        this.startDate = value;
      }
    }
  }
}
