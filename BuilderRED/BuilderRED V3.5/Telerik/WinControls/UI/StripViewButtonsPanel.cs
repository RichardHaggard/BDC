// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StripViewButtonsPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class StripViewButtonsPanel : RadPageViewButtonsPanel
  {
    private RadPageViewStripButtonElement itemListButton;
    private RadPageViewStripButtonElement scrollLeftButton;
    private RadPageViewStripButtonElement scrollRightButton;
    private RadPageViewStripButtonElement closeButton;

    static StripViewButtonsPanel()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new StripViewElementStateManager(), typeof (StripViewButtonsPanel));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(2);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      RadPageViewLocalizationProvider currentProvider = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider;
      this.scrollLeftButton = new RadPageViewStripButtonElement();
      this.scrollLeftButton.ThemeRole = "StripViewLeftScrollButton";
      this.scrollLeftButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.Back;
      this.scrollLeftButton.Tag = (object) StripViewButtons.LeftScroll;
      this.scrollLeftButton.ToolTipText = currentProvider.GetLocalizedString("LeftScrollButton");
      this.scrollLeftButton.Click += new EventHandler(this.OnButtonClick);
      this.scrollRightButton = new RadPageViewStripButtonElement();
      this.scrollRightButton.ThemeRole = "StripViewRightScrollButton";
      this.scrollRightButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.Next;
      this.scrollRightButton.Tag = (object) StripViewButtons.RightScroll;
      this.scrollRightButton.ToolTipText = currentProvider.GetLocalizedString("RightScrollButton");
      this.scrollRightButton.Click += new EventHandler(this.OnButtonClick);
      this.itemListButton = new RadPageViewStripButtonElement();
      this.itemListButton.ThemeRole = "StripViewItemListButton";
      this.itemListButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.DropDown2;
      this.itemListButton.Tag = (object) StripViewButtons.ItemList;
      this.itemListButton.ToolTipText = currentProvider.GetLocalizedString("ItemListButton");
      this.itemListButton.Click += new EventHandler(this.OnButtonClick);
      this.closeButton = new RadPageViewStripButtonElement();
      this.closeButton.ThemeRole = "StripViewCloseButton";
      this.closeButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.Close;
      this.closeButton.Tag = (object) StripViewButtons.Close;
      this.closeButton.ToolTipText = currentProvider.GetLocalizedString("CloseButton");
      this.closeButton.Click += new EventHandler(this.OnButtonClick);
      if (this.RightToLeft && this.ContentOrientation != PageViewContentOrientation.Vertical270 && this.ContentOrientation != PageViewContentOrientation.Vertical90)
      {
        this.Children.Add((RadElement) this.closeButton);
        this.Children.Add((RadElement) this.itemListButton);
        this.Children.Add((RadElement) this.scrollLeftButton);
        this.Children.Add((RadElement) this.scrollRightButton);
      }
      else
      {
        this.Children.Add((RadElement) this.scrollLeftButton);
        this.Children.Add((RadElement) this.scrollRightButton);
        this.Children.Add((RadElement) this.itemListButton);
        this.Children.Add((RadElement) this.closeButton);
      }
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
    }

    private void RadPageViewLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      RadPageViewLocalizationProvider currentProvider = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider;
      this.scrollLeftButton.ToolTipText = currentProvider.GetLocalizedString("LeftScrollButton");
      this.scrollRightButton.ToolTipText = currentProvider.GetLocalizedString("RightScrollButton");
      this.itemListButton.ToolTipText = currentProvider.GetLocalizedString("ItemListButton");
      this.closeButton.ToolTipText = currentProvider.GetLocalizedString("CloseButton");
    }

    [Browsable(false)]
    public RadPageViewStripButtonElement ScrollLeftButton
    {
      get
      {
        return this.scrollLeftButton;
      }
    }

    [Browsable(false)]
    public RadPageViewStripButtonElement ScrollRightButton
    {
      get
      {
        return this.scrollRightButton;
      }
    }

    [Browsable(false)]
    public RadPageViewStripButtonElement ItemListButton
    {
      get
      {
        return this.itemListButton;
      }
    }

    [Browsable(false)]
    public RadPageViewStripButtonElement CloseButton
    {
      get
      {
        return this.closeButton;
      }
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.UpdateButtonsVisibility();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadPageViewStripElement.StripButtonsProperty || e.Property == RadPageViewStripElement.ItemFitModeProperty)
        this.UpdateButtonsVisibility();
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      this.SuspendLayout();
      this.Children.Clear();
      if (this.RightToLeft && this.ContentOrientation != PageViewContentOrientation.Vertical270 && this.ContentOrientation != PageViewContentOrientation.Vertical90)
      {
        this.Children.Add((RadElement) this.closeButton);
        this.Children.Add((RadElement) this.itemListButton);
        this.Children.Add((RadElement) this.scrollLeftButton);
        this.Children.Add((RadElement) this.scrollRightButton);
      }
      else
      {
        this.Children.Add((RadElement) this.scrollLeftButton);
        this.Children.Add((RadElement) this.scrollRightButton);
        this.Children.Add((RadElement) this.itemListButton);
        this.Children.Add((RadElement) this.closeButton);
      }
      this.ResumeLayout(true);
    }

    private void UpdateButtonsVisibility()
    {
      StripViewButtons autoButtons = (StripViewButtons) this.GetValue(RadPageViewStripElement.StripButtonsProperty);
      if (autoButtons == StripViewButtons.Auto)
        autoButtons = this.GetAutoButtons();
      if ((autoButtons & StripViewButtons.LeftScroll) == StripViewButtons.LeftScroll)
        this.scrollLeftButton.Visibility = ElementVisibility.Visible;
      else
        this.scrollLeftButton.Visibility = ElementVisibility.Collapsed;
      if ((autoButtons & StripViewButtons.RightScroll) == StripViewButtons.RightScroll)
        this.scrollRightButton.Visibility = ElementVisibility.Visible;
      else
        this.scrollRightButton.Visibility = ElementVisibility.Collapsed;
      if ((autoButtons & StripViewButtons.ItemList) == StripViewButtons.ItemList)
        this.itemListButton.Visibility = ElementVisibility.Visible;
      else
        this.itemListButton.Visibility = ElementVisibility.Collapsed;
      if ((autoButtons & StripViewButtons.Close) == StripViewButtons.Close)
        this.closeButton.Visibility = ElementVisibility.Visible;
      else
        this.closeButton.Visibility = ElementVisibility.Collapsed;
    }

    private StripViewButtons GetAutoButtons()
    {
      switch ((StripViewItemFitMode) this.GetValue(RadPageViewStripElement.ItemFitModeProperty))
      {
        case StripViewItemFitMode.None:
        case StripViewItemFitMode.Fill:
          return StripViewButtons.VS2005Style;
        default:
          return StripViewButtons.VS2008Style;
      }
    }

    private void OnButtonClick(object sender, EventArgs e)
    {
      this.FindAncestor<StripViewItemContainer>()?.OnStripButtonClicked(sender as RadPageViewStripButtonElement);
    }
  }
}
