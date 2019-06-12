// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.Commands
{
  public class CommandEventArgs : CancelEventArgs
  {
    private object target;
    private object[] settings;
    private ICommand command;

    public CommandEventArgs(object command, params object[] settings)
    {
      this.command = command as ICommand;
      this.settings = settings;
    }

    public CommandEventArgs(object command, object target, params object[] settings)
      : this(command, settings)
    {
      this.target = target;
    }

    public object Target
    {
      get
      {
        return this.target;
      }
      set
      {
        this.target = value;
      }
    }

    public object[] Settings
    {
      get
      {
        return this.settings;
      }
    }
  }
}
