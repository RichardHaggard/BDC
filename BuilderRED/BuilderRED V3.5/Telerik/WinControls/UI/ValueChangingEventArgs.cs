// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ValueChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class ValueChangingEventArgs : CancelEventArgs
  {
    private object newValue;
    private object oldValue;

    public ValueChangingEventArgs(object newValue)
    {
      this.newValue = newValue;
    }

    public ValueChangingEventArgs(object newValue, object oldValue)
    {
      this.newValue = newValue;
      this.oldValue = oldValue;
    }

    public object NewValue
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

    public object OldValue
    {
      get
      {
        return this.oldValue;
      }
      set
      {
        this.oldValue = value;
      }
    }
  }
}
