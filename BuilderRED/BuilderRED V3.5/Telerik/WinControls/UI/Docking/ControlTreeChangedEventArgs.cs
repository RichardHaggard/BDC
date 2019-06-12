// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Docking.ControlTreeChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.Docking
{
  public class ControlTreeChangedEventArgs : EventArgs
  {
    private Control parent;
    private Control child;
    private ControlTreeChangeAction action;

    public ControlTreeChangedEventArgs(
      Control parent,
      Control child,
      ControlTreeChangeAction action)
    {
      this.parent = parent;
      this.child = child;
      this.action = action;
    }

    public Control Parent
    {
      get
      {
        return this.parent;
      }
    }

    public Control Child
    {
      get
      {
        return this.child;
      }
    }

    public ControlTreeChangeAction Action
    {
      get
      {
        return this.action;
      }
    }
  }
}
