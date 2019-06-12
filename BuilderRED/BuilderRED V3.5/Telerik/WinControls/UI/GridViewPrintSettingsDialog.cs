// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewPrintSettingsDialog
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
  public class GridViewPrintSettingsDialog : PrintSettingsDialog
  {
    protected GridViewPrintStyleSettings printStyleSettingControl;
    private RadThemeComponentBase.ThemeContext context;

    public GridViewPrintSettingsDialog()
    {
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
    }

    public GridViewPrintSettingsDialog(RadPrintDocument document)
      : base(document)
    {
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
    }

    protected override Control CreateFormatControl()
    {
      this.printStyleSettingControl = new GridViewPrintStyleSettings();
      return (Control) this.printStyleSettingControl;
    }

    protected override void LocalizeStrings()
    {
      base.LocalizeStrings();
      if (this.printStyleSettingControl == null)
        return;
      this.printStyleSettingControl.LocalizeStrings();
    }

    protected override void LoadSettings()
    {
      base.LoadSettings();
      RadGridView associatedObject = this.PrintDocument.AssociatedObject as RadGridView;
      if (associatedObject.PrintStyle.GridView == null)
        associatedObject.PrintStyle.GridView = associatedObject;
      if (this.printStyleSettingControl == null)
        return;
      this.printStyleSettingControl.LoadPrintStyle(associatedObject.PrintStyle);
    }

    protected override void ApplySettings()
    {
      base.ApplySettings();
      RadGridView associatedObject = this.PrintDocument.AssociatedObject as RadGridView;
      if (associatedObject.Site != null)
        (associatedObject.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService)?.OnComponentChanging((object) associatedObject, (MemberDescriptor) null);
      associatedObject.PrintStyle = this.printStyleSettingControl.PrintStyle;
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
        this.printStyleSettingControl.CorrectPositions();
      }
      else if (TelerikHelper.IsMaterialTheme(this.ThemeName))
      {
        this.pageFormat.Size = new Size(693, 456);
        this.printStyleSettingControl.Size = new Size(693, 456);
      }
      this.printStyleSettingControl.AdjustControlsForTouchLayout(this.ThemeName);
    }
  }
}
