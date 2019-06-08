// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DeselectChildNodesCommand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Commands;

namespace Telerik.WinControls.UI
{
  public class DeselectChildNodesCommand : CommandBase
  {
    public override object Execute(params object[] settings)
    {
      if (settings.Length > 0 && this.CanExecute(settings[0]))
      {
        RadTreeNode setting = settings[0] as RadTreeNode;
        RadTreeView treeView = setting.TreeView;
        if (treeView != null && treeView.SelectedNodes.Contains(setting))
        {
          setting.Selected = false;
          return (object) setting;
        }
      }
      return base.Execute(settings);
    }

    public override bool CanExecute(object parameter)
    {
      if (parameter is RadTreeNode)
        return true;
      return base.CanExecute(parameter);
    }
  }
}
