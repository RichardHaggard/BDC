// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewPrintSettingsDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewPrintSettingsDialog : PrintSettingsDialog
  {
    protected GanttViewPrintSettingsControl printSettingControl;
    private RadThemeComponentBase.ThemeContext context;

    public GanttViewPrintSettingsDialog()
    {
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
    }

    public GanttViewPrintSettingsDialog(RadPrintDocument document)
      : base(document)
    {
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
    }

    protected override Control CreateFormatControl()
    {
      this.printSettingControl = new GanttViewPrintSettingsControl();
      return (Control) this.printSettingControl;
    }

    protected override void LocalizeStrings()
    {
      base.LocalizeStrings();
      if (this.printSettingControl == null)
        return;
      this.printSettingControl.LocalizeStrings();
    }

    protected override void LoadSettings()
    {
      base.LoadSettings();
      RadGanttView associatedObject = this.PrintDocument.AssociatedObject as RadGanttView;
      if (this.printSettingControl == null)
        return;
      this.printSettingControl.LoadPrintSettings(associatedObject.PrintSettings);
    }

    protected override void ApplySettings()
    {
      base.ApplySettings();
      RadGanttView associatedObject = this.PrintDocument.AssociatedObject as RadGanttView;
      if (associatedObject.Site != null)
        (associatedObject.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService)?.OnComponentChanging((object) associatedObject, (MemberDescriptor) null);
      associatedObject.PrintSettings = this.printSettingControl.PrintSettings;
      if (associatedObject.Site == null)
        return;
      (associatedObject.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService)?.OnComponentChanged((object) associatedObject, (MemberDescriptor) null, (object) null, (object) null);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.context.CorrectPositions();
        this.printSettingControl.CorrectPositions();
      }
      else if (TelerikHelper.IsMaterialTheme(this.ThemeName))
      {
        this.pageFormat.Size = new Size(693, 456);
        this.printSettingControl.Size = new Size(693, 456);
      }
      this.printSettingControl.AdjustControlsForTouchLayout(this.ThemeName);
    }
  }
}
