// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.NodeExpandCollapseCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using Telerik.WinControls.Commands;

namespace Telerik.WinControls.UI
{
  public class NodeExpandCollapseCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length <= 0 || !this.CanExecute(settings[0]))
        return (object) null;
      RadTreeNode setting = settings[0] as RadTreeNode;
      bool flag = false;
      try
      {
        flag = settings.Length <= 1 ? setting.Expanded : (!(settings[1] is IList) ? !(bool) settings[1] : !(bool) ((IList) settings[1])[0]);
      }
      catch (Exception ex)
      {
        flag = setting.Expanded;
      }
      finally
      {
        if (flag)
          setting.Collapse();
        else
          setting.Expand();
      }
      return (object) setting.Expanded;
    }

    public override bool CanExecute(object parameter)
    {
      if (typeof (RadTreeNode).IsAssignableFrom(parameter.GetType()))
        return true;
      return base.CanExecute(parameter);
    }
  }
}
