// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CompareNodesTagCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using Telerik.WinControls.Commands;

namespace Telerik.WinControls.UI
{
  public class CompareNodesTagCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length > 1 && this.CanExecute(settings[0]))
      {
        RadTreeNode setting = settings[0] as RadTreeNode;
        object obj = (settings[1] as IList)[0];
        if (setting != null && setting.Tag != null)
        {
          IComparable tag = setting.Tag as IComparable;
          if (tag != null && tag.CompareTo(obj) == 0 || setting.Tag.Equals(obj))
            return (object) setting;
        }
      }
      return (object) null;
    }

    public override bool CanExecute(object parameter)
    {
      if (typeof (RadTreeNode).IsAssignableFrom(parameter.GetType()))
        return true;
      return base.CanExecute(parameter);
    }
  }
}
