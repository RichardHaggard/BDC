// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarCustomizeDialogProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CommandBarCustomizeDialogProvider
  {
    private static CommandBarCustomizeDialogProvider currentProvider = CommandBarCustomizeDialogProvider.CreateDefaultCustomizeDialogProvider();

    public static event EventHandler CurrentProviderChanged;

    public static event EventHandler CustomizeDialogOpened;

    public static event CancelEventHandler CustomizeDialogOpening;

    private static void OnCurrentProviderChanged()
    {
      EventHandler currentProviderChanged = CommandBarCustomizeDialogProvider.CurrentProviderChanged;
      if (currentProviderChanged == null)
        return;
      currentProviderChanged((object) CommandBarCustomizeDialogProvider.currentProvider, EventArgs.Empty);
    }

    protected static bool OnDialogOpening(object sender)
    {
      if (CommandBarCustomizeDialogProvider.CustomizeDialogOpening == null)
        return false;
      CancelEventArgs e = new CancelEventArgs();
      CommandBarCustomizeDialogProvider.CustomizeDialogOpening(sender, e);
      return e.Cancel;
    }

    protected static void OnDialogOpened(object sender)
    {
      if (CommandBarCustomizeDialogProvider.CustomizeDialogOpened == null)
        return;
      CommandBarCustomizeDialogProvider.CustomizeDialogOpened(sender, new EventArgs());
    }

    public virtual Form ShowCustomizeDialog(object sender, CommandBarStripInfoHolder infoHolder)
    {
      CommandBarCustomizeDialog barCustomizeDialog = new CommandBarCustomizeDialog(infoHolder);
      RadElement radElement = sender as RadElement;
      RadControl radControl = sender as RadControl;
      if (radControl == null && radElement != null && radElement.ElementTree != null)
        radControl = radElement.ElementTree.Control as RadControl;
      if (sender is CommandBarStripElement)
      {
        barCustomizeDialog.stripsListControl.SelectedValue = sender;
        barCustomizeDialog.radPageView.SelectedPage = barCustomizeDialog.toolstripItemsPage;
      }
      else if (sender is RadCommandBar)
        barCustomizeDialog.radPageView.SelectedPage = barCustomizeDialog.toolstripsPage;
      if (radControl != null)
      {
        barCustomizeDialog.ThemeName = radControl.ThemeName;
        barCustomizeDialog.RightToLeft = radControl.RightToLeft;
      }
      else if (radElement != null)
        barCustomizeDialog.RightToLeft = radElement.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      if (CommandBarCustomizeDialogProvider.OnDialogOpening((object) barCustomizeDialog))
        return (Form) null;
      CommandBarCustomizeDialogProvider.OnDialogOpened((object) barCustomizeDialog);
      int num = (int) barCustomizeDialog.ShowDialog();
      return (Form) barCustomizeDialog;
    }

    [Browsable(false)]
    public static CommandBarCustomizeDialogProvider CurrentProvider
    {
      get
      {
        return CommandBarCustomizeDialogProvider.currentProvider;
      }
      set
      {
        CommandBarCustomizeDialogProvider.currentProvider = value != null ? value : CommandBarCustomizeDialogProvider.CreateDefaultCustomizeDialogProvider();
        CommandBarCustomizeDialogProvider.OnCurrentProviderChanged();
      }
    }

    private static CommandBarCustomizeDialogProvider CreateDefaultCustomizeDialogProvider()
    {
      return new CommandBarCustomizeDialogProvider();
    }
  }
}
