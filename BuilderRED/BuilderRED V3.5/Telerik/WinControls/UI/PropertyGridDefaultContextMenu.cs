// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridDefaultContextMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class PropertyGridDefaultContextMenu : RadContextMenu
  {
    private PropertyGridTableElement propertyTableElement;
    private PropertyGridMenuItem expandCollapseMenuItem;
    private PropertyGridMenuItem resetMenuItem;
    private PropertyGridMenuItem editMenuItem;
    private PropertyGridMenuItem showDescriptionMenuItem;
    private PropertyGridMenuItem showToolbarMenuItem;
    private RadMenuSeparatorItem firstSeparator;
    private PropertyGridMenuItem sortMenuItem;
    private PropertyGridMenuItem noSortMenuItem;
    private PropertyGridMenuItem alphabeticalMenuItem;
    private PropertyGridMenuItem categorizedMenuItem;
    private PropertyGridMenuItem categorizedAlphabeticalMenuItem;

    public PropertyGridDefaultContextMenu(PropertyGridTableElement propertyGridElement)
    {
      this.propertyTableElement = propertyGridElement;
      this.resetMenuItem = new PropertyGridMenuItem("Reset", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuReset"));
      this.Items.Add((RadItem) this.resetMenuItem);
      this.editMenuItem = new PropertyGridMenuItem("Edit", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuEdit"));
      this.Items.Add((RadItem) this.editMenuItem);
      this.expandCollapseMenuItem = new PropertyGridMenuItem("Expand", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuExpand"));
      this.Items.Add((RadItem) this.expandCollapseMenuItem);
      this.firstSeparator = new RadMenuSeparatorItem();
      this.Items.Add((RadItem) this.firstSeparator);
      this.sortMenuItem = new PropertyGridMenuItem("Sort", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuSort"));
      this.Items.Add((RadItem) this.sortMenuItem);
      this.noSortMenuItem = new PropertyGridMenuItem("NoSort", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuNoSort"));
      this.noSortMenuItem.Click += new EventHandler(this.menuItem_Click);
      this.sortMenuItem.Items.Add((RadItem) this.noSortMenuItem);
      this.alphabeticalMenuItem = new PropertyGridMenuItem("Alphabetical", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuAlphabetical"));
      this.alphabeticalMenuItem.Click += new EventHandler(this.menuItem_Click);
      this.sortMenuItem.Items.Add((RadItem) this.alphabeticalMenuItem);
      this.categorizedMenuItem = new PropertyGridMenuItem("Categorized", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCategorized"));
      this.categorizedMenuItem.Click += new EventHandler(this.menuItem_Click);
      this.sortMenuItem.Items.Add((RadItem) this.categorizedMenuItem);
      this.categorizedAlphabeticalMenuItem = new PropertyGridMenuItem("CategorizedAlphabetical", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCategorizedAlphabetical"));
      this.categorizedAlphabeticalMenuItem.Click += new EventHandler(this.menuItem_Click);
      this.sortMenuItem.Items.Add((RadItem) this.categorizedAlphabeticalMenuItem);
      this.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.showDescriptionMenuItem = new PropertyGridMenuItem("ShowDescription", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuShowDescription"));
      this.Items.Add((RadItem) this.showDescriptionMenuItem);
      this.showToolbarMenuItem = new PropertyGridMenuItem("ShowToolbar", LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuShowToolbar"));
      this.Items.Add((RadItem) this.showToolbarMenuItem);
      for (int index = 0; index < this.Items.Count; ++index)
        this.Items[index].Click += new EventHandler(this.menuItem_Click);
      LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.PropertyGridLocalizationProvider_CurrentProviderChanged);
    }

    public PropertyGridMenuItem ExpandCollapseMenuItem
    {
      get
      {
        return this.expandCollapseMenuItem;
      }
    }

    public PropertyGridMenuItem EditMenuItem
    {
      get
      {
        return this.editMenuItem;
      }
    }

    public PropertyGridMenuItem ResetMenuItem
    {
      get
      {
        return this.resetMenuItem;
      }
    }

    public PropertyGridMenuItem SortMenuItem
    {
      get
      {
        return this.sortMenuItem;
      }
    }

    public PropertyGridMenuItem ShowDescriptionMenuItem
    {
      get
      {
        return this.showDescriptionMenuItem;
      }
    }

    public PropertyGridMenuItem ShowToolbarMenuItem
    {
      get
      {
        return this.showToolbarMenuItem;
      }
    }

    protected override void OnDropDownOpening(CancelEventArgs args)
    {
      if (ThemeResolutionService.ApplicationThemeName == null && this.ThemeName != this.propertyTableElement.ElementTree.ThemeName)
        this.ThemeName = this.propertyTableElement.ElementTree.ThemeName;
      PropertyGridItemBase selectedGridItem1 = this.propertyTableElement.SelectedGridItem;
      if (selectedGridItem1 != null)
      {
        if (selectedGridItem1 is PropertyGridGroupItem)
        {
          this.EditMenuItem.Visibility = ElementVisibility.Collapsed;
          this.ResetMenuItem.Visibility = ElementVisibility.Collapsed;
        }
        else
        {
          this.EditMenuItem.Visibility = ElementVisibility.Visible;
          this.ResetMenuItem.Visibility = ElementVisibility.Visible;
          PropertyGridItem selectedGridItem2 = this.propertyTableElement.SelectedGridItem as PropertyGridItem;
          PropertyGridItemElementBase element = this.propertyTableElement.GetElement((PropertyGridItemBase) selectedGridItem2);
          this.ResetMenuItem.Enabled = ((PropertyGridItem) selectedGridItem1).IsModified;
          if (element is PropertyGridCheckBoxItemElement || selectedGridItem2.ReadOnly || this.propertyTableElement.ReadOnly)
            this.EditMenuItem.Enabled = false;
          else
            this.EditMenuItem.Enabled = true;
        }
      }
      this.showDescriptionMenuItem.IsChecked = this.propertyTableElement.PropertyGridElement.SplitElement.HelpVisible;
      this.showToolbarMenuItem.IsChecked = this.propertyTableElement.PropertyGridElement.ToolbarVisible;
      this.noSortMenuItem.IsChecked = this.propertyTableElement.PropertySort == PropertySort.NoSort;
      this.alphabeticalMenuItem.IsChecked = this.propertyTableElement.PropertySort == PropertySort.Alphabetical;
      this.categorizedMenuItem.IsChecked = this.propertyTableElement.PropertySort == PropertySort.Categorized;
      this.categorizedAlphabeticalMenuItem.IsChecked = this.propertyTableElement.PropertySort == PropertySort.CategorizedAlphabetical;
      bool rightToLeft = this.propertyTableElement.RightToLeft;
      this.EditMenuItem.RightToLeft = rightToLeft;
      this.ResetMenuItem.RightToLeft = rightToLeft;
      this.showDescriptionMenuItem.RightToLeft = rightToLeft;
      this.showToolbarMenuItem.RightToLeft = rightToLeft;
      this.expandCollapseMenuItem.RightToLeft = rightToLeft;
      this.sortMenuItem.RightToLeft = rightToLeft;
      this.noSortMenuItem.RightToLeft = rightToLeft;
      this.alphabeticalMenuItem.RightToLeft = rightToLeft;
      this.categorizedMenuItem.RightToLeft = rightToLeft;
      this.categorizedAlphabeticalMenuItem.RightToLeft = rightToLeft;
      this.SetSortItemsVisibility();
      base.OnDropDownOpening(args);
      int num = args.Cancel ? 1 : 0;
    }

    private void menuItem_Click(object sender, EventArgs e)
    {
      PropertyGridMenuItem propertyGridMenuItem = sender as PropertyGridMenuItem;
      if (propertyGridMenuItem == null)
        return;
      switch (propertyGridMenuItem.Command)
      {
        case "Reset":
          this.Reset();
          break;
        case "Edit":
          this.EditItem();
          break;
        case "Expand":
        case "Collapse":
          this.ExpandItem();
          break;
        case "ShowToolbar":
          this.ShowToolbar();
          break;
        case "ShowDescription":
          this.ShowDescription();
          break;
        case "NoSort":
          this.propertyTableElement.PropertySort = PropertySort.NoSort;
          break;
        case "Alphabetical":
          this.propertyTableElement.PropertySort = PropertySort.Alphabetical;
          break;
        case "Categorized":
          this.propertyTableElement.PropertySort = PropertySort.Categorized;
          break;
        case "CategorizedAlphabetical":
          this.propertyTableElement.PropertySort = PropertySort.CategorizedAlphabetical;
          break;
      }
    }

    private void PropertyGridLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      PropertyGridLocalizationProvider currentProvider = LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProvider;
      this.expandCollapseMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuExpand");
      this.resetMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuReset");
      this.editMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuEdit");
      this.showDescriptionMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuShowDescription");
      this.showToolbarMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuShowToolbar");
      this.sortMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuSort");
      this.noSortMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuNoSort");
      this.alphabeticalMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuAlphabetical");
      this.categorizedMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuCategorized");
      this.categorizedAlphabeticalMenuItem.Text = currentProvider.GetLocalizedString("ContextMenuCategorizedAlphabetical");
    }

    private void Reset()
    {
      PropertyGridItem selectedGridItem = this.propertyTableElement.SelectedGridItem as PropertyGridItem;
      if (selectedGridItem == null)
        return;
      selectedGridItem.ResetValue();
      this.propertyTableElement.EndEdit();
    }

    private void ShowDescription()
    {
      this.propertyTableElement.PropertyGridElement.SplitElement.HelpVisible = !this.propertyTableElement.PropertyGridElement.SplitElement.HelpVisible;
    }

    private void ShowToolbar()
    {
      this.propertyTableElement.PropertyGridElement.ToolbarVisible = !this.propertyTableElement.PropertyGridElement.ToolbarVisible;
    }

    private void ExpandItem()
    {
      PropertyGridItemBase selectedGridItem = this.propertyTableElement.SelectedGridItem;
      if (selectedGridItem == null)
        return;
      selectedGridItem.Expanded = !selectedGridItem.Expanded;
    }

    private void EditItem()
    {
      this.propertyTableElement.BeginEdit();
    }

    protected virtual void SetSortItemsVisibility()
    {
      bool canSort = this.propertyTableElement.CollectionView.CanSort;
      bool canGroup = this.propertyTableElement.CollectionView.CanGroup;
      if (canSort && canGroup)
      {
        this.sortMenuItem.Visibility = ElementVisibility.Visible;
        this.firstSeparator.Visibility = ElementVisibility.Visible;
        this.noSortMenuItem.Visibility = ElementVisibility.Visible;
        this.alphabeticalMenuItem.Visibility = ElementVisibility.Visible;
        this.categorizedMenuItem.Visibility = ElementVisibility.Visible;
        this.categorizedAlphabeticalMenuItem.Visibility = ElementVisibility.Visible;
      }
      if (!canSort && !canGroup)
      {
        this.sortMenuItem.Visibility = ElementVisibility.Collapsed;
        this.firstSeparator.Visibility = ElementVisibility.Collapsed;
      }
      else if (canSort && !canGroup)
      {
        this.sortMenuItem.Visibility = ElementVisibility.Visible;
        this.firstSeparator.Visibility = ElementVisibility.Visible;
        this.noSortMenuItem.Visibility = ElementVisibility.Visible;
        this.alphabeticalMenuItem.Visibility = ElementVisibility.Visible;
        this.categorizedMenuItem.Visibility = ElementVisibility.Collapsed;
        this.categorizedAlphabeticalMenuItem.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        if (canSort || !canGroup)
          return;
        this.sortMenuItem.Visibility = ElementVisibility.Visible;
        this.firstSeparator.Visibility = ElementVisibility.Visible;
        this.noSortMenuItem.Visibility = ElementVisibility.Visible;
        this.categorizedMenuItem.Visibility = ElementVisibility.Visible;
        this.alphabeticalMenuItem.Visibility = ElementVisibility.Collapsed;
        this.categorizedAlphabeticalMenuItem.Visibility = ElementVisibility.Collapsed;
      }
    }

    protected override void Dispose(bool disposing)
    {
      LocalizationProvider<PropertyGridLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.PropertyGridLocalizationProvider_CurrentProviderChanged);
      for (int index = 0; index < this.Items.Count; ++index)
        this.Items[index].Click -= new EventHandler(this.menuItem_Click);
      this.editMenuItem.Dispose();
      this.expandCollapseMenuItem.Dispose();
      this.showDescriptionMenuItem.Dispose();
      this.showToolbarMenuItem.Dispose();
      base.Dispose(disposing);
    }
  }
}
