// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ShortcutEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class ShortcutEventArgs : EventArgs
  {
    private Control focusedControl;
    private RadShortcut shortcut;
    private bool handled;

    public ShortcutEventArgs(Control focused, RadShortcut shortcut)
    {
      this.focusedControl = focused;
      this.shortcut = shortcut;
    }

    public Control FocusedControl
    {
      get
      {
        return this.focusedControl;
      }
    }

    public RadShortcut Shortcut
    {
      get
      {
        return this.shortcut;
      }
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
  }
}
