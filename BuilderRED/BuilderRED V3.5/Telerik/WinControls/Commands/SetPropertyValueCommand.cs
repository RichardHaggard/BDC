// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.SetPropertyValueCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;

namespace Telerik.WinControls.Commands
{
  public class SetPropertyValueCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length <= 1 || !this.CanExecute(settings[0]))
        return (object) false;
      object setting1 = settings[0];
      string empty = string.Empty;
      object setting2;
      string setting3;
      if (settings[1] is IList)
      {
        if (((ICollection) settings[1]).Count < 2)
          return (object) null;
        setting2 = ((IList) settings[1])[0];
        setting3 = ((IList) settings[1])[1] as string;
      }
      else
      {
        setting2 = settings[1];
        setting3 = settings[2] as string;
      }
      if (setting1 != null && !string.IsNullOrEmpty(setting3))
        setting1.GetType().GetProperty(setting3).SetValue(setting1, setting2, (object[]) null);
      return base.Execute(settings);
    }

    public override bool CanExecute(object parameter)
    {
      if (typeof (object).IsAssignableFrom(parameter.GetType()))
        return true;
      return base.CanExecute(parameter);
    }
  }
}
