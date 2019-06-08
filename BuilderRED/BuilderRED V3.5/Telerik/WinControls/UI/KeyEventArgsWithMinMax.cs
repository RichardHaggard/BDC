// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.KeyEventArgsWithMinMax
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class KeyEventArgsWithMinMax : KeyEventArgs
  {
    private DateTime minDate;
    private DateTime maxDate;

    public KeyEventArgsWithMinMax(Keys keyData, DateTime min, DateTime max)
      : base(keyData)
    {
      this.minDate = min;
      this.maxDate = max;
    }

    public DateTime MaxDate
    {
      get
      {
        return this.maxDate;
      }
      set
      {
        this.maxDate = value;
      }
    }

    public DateTime MinDate
    {
      get
      {
        return this.minDate;
      }
      set
      {
        this.minDate = value;
      }
    }
  }
}
