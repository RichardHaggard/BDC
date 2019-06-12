// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.Commands
{
  public class CommandList : ICommandSource, IEnumerable
  {
    private Hashtable commands = new Hashtable();

    public bool AddCommand(ICommand command)
    {
      if (this.commands.Contains((object) command.Name))
        return false;
      this.commands.Add((object) command.Name, (object) command);
      return true;
    }

    public void AddCommands(List<ICommand> list)
    {
      foreach (ICommand command in list)
        this.AddCommand(command);
    }

    public void AddCommands(CommandList list)
    {
      foreach (ICommand command in list)
        this.AddCommand(command);
    }

    public bool RemoveCommand(string id)
    {
      if (!this.commands.Contains((object) id))
        return false;
      this.commands.Remove((object) id);
      return true;
    }

    public bool Contains(string id)
    {
      return this.commands.Contains((object) id);
    }

    public ICommand GetCommand(string id)
    {
      if (this.commands.Contains((object) id))
        return (ICommand) this.commands[(object) id];
      return (ICommand) null;
    }

    public ICommand this[string id]
    {
      get
      {
        return this.GetCommand(id);
      }
      set
      {
        if (!this.commands.Contains((object) id))
          return;
        this.commands[(object) id] = (object) value;
      }
    }

    public IEnumerator GetEnumerator()
    {
      return this.commands.Values.GetEnumerator();
    }

    public bool RemoveCommand(ICommand command)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public bool Contains(ICommand command)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public ICommand this[object id]
    {
      get
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }

    public List<ICommand> Commands
    {
      get
      {
        throw new Exception("The method or operation is not implemented.");
      }
    }
  }
}
