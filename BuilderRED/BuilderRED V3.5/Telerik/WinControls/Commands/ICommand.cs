// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.ICommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Commands
{
  public interface ICommand
  {
    string Name { get; }

    string Type { get; }

    System.Type OwnerType { get; set; }

    System.Type ContextType { get; set; }

    object Execute();

    object Execute(params object[] settings);

    bool CanExecute(object target);

    string ToString();

    event EventHandler CanExecuteChanged;

    event CommandEventHandler HandleExecute;

    event CommandEventHandler Executed;
  }
}
