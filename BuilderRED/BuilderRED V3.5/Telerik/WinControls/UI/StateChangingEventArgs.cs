// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class StateChangingEventArgs : CancelEventArgs
  {
    private ToggleState oldValue;
    private ToggleState newValue;
    private bool canceled;

    public ToggleState OldValue
    {
      get
      {
        return this.oldValue;
      }
    }

    public ToggleState NewValue
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

    public StateChangingEventArgs(ToggleState oldValue, ToggleState newValue, bool canceled)
    {
      this.oldValue = oldValue;
      this.newValue = newValue;
      this.canceled = canceled;
    }
  }
}
