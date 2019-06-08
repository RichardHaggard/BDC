// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Elements.ClickCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Commands;

namespace Telerik.WinControls.Elements
{
  public class ClickCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length <= 0 || !this.CanExecute(settings[0]))
        return (object) false;
      object setting = settings[0];
      if (typeof (RadItem).IsAssignableFrom(setting.GetType()))
        (setting as RadItem).CallDoClick(EventArgs.Empty);
      if (typeof (RadControl).IsAssignableFrom(setting.GetType()) && setting is RadControl)
        (setting as RadControl).Behavior.OnClick(EventArgs.Empty);
      return base.Execute(settings);
    }
  }
}
