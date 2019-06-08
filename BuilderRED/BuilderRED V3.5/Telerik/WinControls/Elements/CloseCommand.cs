// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Elements.CloseCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Commands;

namespace Telerik.WinControls.Elements
{
  public class CloseCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length <= 0 || !this.CanExecute(settings[0]))
        return (object) null;
      object setting = settings[0];
      if (typeof (Form).IsAssignableFrom(setting.GetType()))
        (setting as Form).Close();
      else if (typeof (Control).IsAssignableFrom(setting.GetType()))
        (setting as Control).Hide();
      else if (typeof (RadItem).IsAssignableFrom(setting.GetType()))
        (setting as RadItem).Visibility = ElementVisibility.Hidden;
      return base.Execute(settings);
    }

    public override bool CanExecute(object parameter)
    {
      if (typeof (Control).IsAssignableFrom(parameter.GetType()))
        return true;
      return base.CanExecute(parameter);
    }
  }
}
