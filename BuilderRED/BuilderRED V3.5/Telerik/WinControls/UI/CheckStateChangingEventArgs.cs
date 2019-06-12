// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckStateChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CheckStateChangingEventArgs : CancelEventArgs
  {
    private CheckState oldValue;
    private CheckState newValue;

    public CheckState OldValue
    {
      get
      {
        return this.oldValue;
      }
    }

    public CheckState NewValue
    {
      get
      {
        return this.newValue;
      }
      set
      {
        this.newValue = value;
      }
    }

    public CheckStateChangingEventArgs(CheckState oldValue, CheckState newValue, bool canceled)
    {
      this.oldValue = oldValue;
      this.newValue = newValue;
      this.Cancel = canceled;
    }
  }
}
