// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Interfaces.DockableDeserializedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.Interfaces
{
  public class DockableDeserializedEventArgs : EventArgs
  {
    private Guid id = Guid.Empty;
    private Control dockable;

    public DockableDeserializedEventArgs(Control dockable, Guid id)
    {
      this.dockable = dockable;
      this.id = id;
    }

    public Control Dockable
    {
      get
      {
        return this.dockable;
      }
    }

    public Guid Id
    {
      get
      {
        return this.id;
      }
    }
  }
}
