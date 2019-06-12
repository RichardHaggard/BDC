// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewItemButtonsPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadPageViewItemButtonsPanel : RadPageViewButtonsPanel
  {
    private RadPageViewButtonElement closeButton;
    private RadPageViewPinButtonElement pinButton;
    private RadPageViewItem owner;
    private bool providerAttached;

    public RadPageViewItemButtonsPanel(RadPageViewItem owner)
    {
      this.owner = owner;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.pinButton = new RadPageViewPinButtonElement();
      this.pinButton.ThemeRole = "PageViewItemPinButton";
      this.pinButton.Click += new EventHandler(this.OnPinButtonClick);
      this.pinButton.ToolTipText = this.pinButton.IsPreview ? LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemPinButtonPreviewTooltip") : LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemPinButtonTooltip");
      this.pinButton.RadPropertyChanged += new RadPropertyChangedEventHandler(this.pinButton_RadPropertyChanged);
      this.Children.Add((RadElement) this.pinButton);
      this.closeButton = new RadPageViewButtonElement();
      this.closeButton.ThemeRole = "PageViewItemCloseButton";
      this.closeButton.Click += new EventHandler(this.OnCloseButtonClick);
      this.closeButton.ToolTipText = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemCloseButton");
      this.Children.Add((RadElement) this.closeButton);
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
      this.providerAttached = true;
    }

    private void pinButton_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadPageViewPinButtonElement.IsPreviewProperty)
        return;
      this.pinButton.ToolTipText = this.pinButton.IsPreview ? LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemPinButtonPreviewTooltip") : LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemPinButtonTooltip");
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.UpdateCloseButton();
      this.UpdatePinButton();
      if (this.providerAttached)
        return;
      this.providerAttached = true;
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
    }

    protected override void OnUnloaded(ComponentThemableElementTree oldTree)
    {
      base.OnUnloaded(oldTree);
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
      this.providerAttached = false;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.Parent == null)
        return;
      this.SetParent((RadElement) null);
      this.owner = (RadPageViewItem) null;
      this.closeButton.Click -= new EventHandler(this.OnCloseButtonClick);
      this.closeButton.ToolTipText = "";
      this.closeButton.Dispose();
      if (!this.providerAttached)
        return;
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
      this.providerAttached = false;
    }

    [Browsable(false)]
    public RadPageViewButtonElement CloseButton
    {
      get
      {
        return this.closeButton;
      }
    }

    [Browsable(false)]
    public RadPageViewButtonElement PinButton
    {
      get
      {
        return (RadPageViewButtonElement) this.pinButton;
      }
    }

    protected RadPageViewItem Owner
    {
      get
      {
        return this.owner;
      }
    }

    private void RadPageViewLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.closeButton.ToolTipText = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemCloseButton");
      this.pinButton.ToolTipText = this.pinButton.IsPreview ? LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemPinButtonPreviewTooltip") : LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ItemPinButtonTooltip");
    }

    protected virtual void OnCloseButtonClick(object sender, EventArgs e)
    {
      if (this.ElementTree != null && this.ElementTree.Control.Site != null || (this.owner.Owner == null || this.owner.Owner.SelectedItem == null) || this.owner.Owner.SelectedItem.Page.HasFocusedChildControl())
        return;
      this.owner.Owner.CloseItem(this.owner);
    }

    protected virtual void OnPinButtonClick(object sender, EventArgs e)
    {
      if (this.ElementTree != null && this.ElementTree.Control.Site != null)
        return;
      if (this.owner.IsPreview)
      {
        RadPageViewStripElement owner = this.owner.Owner as RadPageViewStripElement;
        if (owner == null)
          return;
        owner.PreviewItem = (RadPageViewItem) null;
      }
      else
      {
        RadPageViewItem owner = this.owner;
        owner.IsPinned = !owner.IsPinned;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadPageViewElement.ShowItemCloseButtonProperty)
        this.UpdateCloseButton();
      if (e.Property != RadPageViewStripElement.ShowItemPinButtonProperty)
        return;
      this.UpdatePinButton();
    }

    private void UpdateCloseButton()
    {
      if (this.closeButton.GetValueSource(RadElement.VisibilityProperty) >= ValueSource.Local)
        return;
      bool flag = false;
      if (this.owner.Owner != null && this.owner.Owner.Owner != null && this.owner.Owner.Owner.ViewMode == PageViewMode.Backstage)
        flag = true;
      if ((bool) this.GetValue(RadPageViewElement.ShowItemCloseButtonProperty) && !flag)
      {
        int num1 = (int) this.closeButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      else
      {
        int num2 = (int) this.closeButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
    }

    private void UpdatePinButton()
    {
      if (this.pinButton.GetValueSource(RadElement.VisibilityProperty) >= ValueSource.Local)
        return;
      bool flag = false;
      if (this.owner.Owner != null && this.owner.Owner.Owner != null && this.owner.Owner.Owner.ViewMode == PageViewMode.Backstage)
        flag = true;
      if ((bool) this.GetValue(RadPageViewStripElement.ShowItemPinButtonProperty) && !flag)
      {
        int num1 = (int) this.pinButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      else
      {
        int num2 = (int) this.pinButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
    }
  }
}
