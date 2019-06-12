// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.GetPropertyValueCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Reflection;

namespace Telerik.WinControls.Commands
{
  public class GetPropertyValueCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length > 1 && this.CanExecute(settings[0]))
      {
        object setting1 = settings[0];
        string setting2 = settings[1] as string;
        if (setting1 != null && !string.IsNullOrEmpty(setting2))
        {
          PropertyInfo property = setting1.GetType().GetProperty(setting2);
          base.Execute(settings);
          return property.GetValue(setting1, (object[]) null);
        }
      }
      return (object) null;
    }

    public override bool CanExecute(object parameter)
    {
      if (typeof (object).IsAssignableFrom(parameter.GetType()))
        return true;
      return base.CanExecute(parameter);
    }
  }
}
