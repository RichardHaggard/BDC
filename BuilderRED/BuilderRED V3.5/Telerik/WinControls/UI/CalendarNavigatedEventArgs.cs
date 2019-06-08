// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarNavigatedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class CalendarNavigatedEventArgs : EventArgs
  {
    private bool isFastNavigation;
    private CalendarNavigationDirection direction;

    public CalendarNavigatedEventArgs(CalendarNavigationDirection direction, bool isFastNavigation)
    {
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
  }
}
