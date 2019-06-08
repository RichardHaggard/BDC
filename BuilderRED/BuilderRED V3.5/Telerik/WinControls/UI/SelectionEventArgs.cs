// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SelectionEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public sealed class SelectionEventArgs : CancelEventArgs
  {
    private DateTimeCollection dates;
    private List<DateTime> newDates;

    public SelectionEventArgs(DateTimeCollection dates)
    {
      this.dates = dates;
    }

    public SelectionEventArgs(DateTimeCollection dates, List<DateTime> newDates)
      : this(dates)
    {
      this.newDates = newDates;
    }

    public List<DateTime> NewDates
    {
      get
      {
        return this.newDates;
      }
    }

    public DateTimeCollection Dates
    {
      get
      {
        return this.dates;
      }
    }
  }
}
