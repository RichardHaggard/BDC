// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FindByTextCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using Telerik.WinControls.Commands;

namespace Telerik.WinControls.UI
{
  public class FindByTextCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length > 1 && this.CanExecute(settings[0]))
      {
        RadTreeNode setting1 = settings[0] as RadTreeNode;
        IList setting2 = settings[1] as IList;
        this.ValidateArguments(setting2);
        string str = setting2[0] as string;
        if (setting1 != null && !string.IsNullOrEmpty(str) && setting1.Text == str)
          return (object) setting1;
      }
      return (object) null;
    }

    private void ValidateArguments(IList args)
    {
      if (args == null)
        throw new ArgumentException("Command arguments can not be null. FindByTextCommand accepts a single string parameter.");
      if (args.Count == 0)
        throw new ArgumentException("Command arguments can not be empty. FindByTextCommand accepts a single string parameter.");
      if (!(args[0] is string))
        throw new ArgumentException("Command argument must be of type string.");
    }

    public override bool CanExecute(object parameter)
    {
      if (typeof (RadTreeNode).IsAssignableFrom(parameter.GetType()))
        return true;
      return base.CanExecute(parameter);
    }
  }
}
