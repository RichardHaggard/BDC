// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TextChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  public class TextChangingEventArgs : CancelEventArgs
  {
    private string oldValue;
    private string newValue;

    public string OldValue
    {
      get
      {
        return this.oldValue;
      }
    }

    public string NewValue
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

    public TextChangingEventArgs(string oldValue, string newValue)
    {
      this.oldValue = oldValue;
      this.newValue = newValue;
    }

    public TextChangingEventArgs(string oldValue, string newValue, bool cancel)
    {
      this.oldValue = oldValue;
      this.newValue = newValue;
      this.Cancel = cancel;
    }
  }
}
