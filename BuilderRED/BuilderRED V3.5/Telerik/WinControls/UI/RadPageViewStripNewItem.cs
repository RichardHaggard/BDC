// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewStripNewItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  internal class RadPageViewStripNewItem : RadPageViewStripItem
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(14, 14);
      this.IsSystemItem = true;
      this.ToolTipText = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("NewItemTooltipText");
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
    }

    private void RadPageViewLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.ToolTipText = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("NewItemTooltipText");
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ButtonsPanel.Visibility = ElementVisibility.Collapsed;
    }
  }
}
