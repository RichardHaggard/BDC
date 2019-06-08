// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ActivateMenuItemCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Commands;

namespace Telerik.WinControls.UI
{
  public class ActivateMenuItemCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length > 0 && this.CanExecute(settings[0]))
      {
        RadMenuItem setting = settings[0] as RadMenuItem;
        if (setting != null)
        {
          if (!setting.Selected)
            setting.Select();
          setting.ShowChildItems();
          return base.Execute(settings);
        }
      }
      return (object) null;
    }

    public override bool CanExecute(object parameter)
    {
      return typeof (RadMenuItem).IsAssignableFrom(parameter.GetType());
    }
  }
}
