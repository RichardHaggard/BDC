// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.InputKeyEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class InputKeyEventArgs : EventArgs
  {
    private Keys keys;
    private bool handled;

    public InputKeyEventArgs(Keys keys)
    {
      this.keys = keys;
      this.handled = false;
    }

    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    public Keys Keys
    {
      get
      {
        return this.keys;
      }
    }
  }
}
