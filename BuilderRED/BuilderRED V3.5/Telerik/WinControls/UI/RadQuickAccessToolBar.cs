// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadQuickAccessToolBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadQuickAccessToolBar : StackLayoutElement
  {
    private RadMenuItem toolbarPositionMenuItem = new RadMenuItem();
    private RadMenuItem minimizeRibonMenuItem = new RadMenuItem();
    public static RadProperty IsCollapsedByUserProperty = RadProperty.Register("IsCollapsedByUser", typeof (bool), typeof (RadQuickAccessToolBar), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private RadItemOwnerCollection items;
    private InnerItem quickAccessItemsPanel;
    private RadQuickAccessOverflowButton overFlowButton;

    static RadQuickAccessToolBar()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new QuickAccessToolbarStateManagerFactory(), typeof (RadQuickAccessToolBar));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new Type[9]
      {
        typeof (RadButtonElement),
        typeof (RadToggleButtonElement),
        typeof (RadRepeatButtonElement),
        typeof (RadCheckBoxElement),
        typeof (RadImageButtonElement),
        typeof (RadRadioButtonElement),
        typeof (RadDropDownButtonElement),
        typeof (RadSplitButtonElement),
        typeof (CommandBarSeparator)
      };
      this.items.DefaultType = typeof (RadButtonElement);
      this.items.ItemsChanged += new ItemChangedDelegate(this.OnRadQuickAccessBar_ItemsChanged);
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.FitInAvailableSize = true;
      this.SmoothingMode = SmoothingMode.AntiAlias;
    }

    protected override void CreateChildElements()
    {
      this.quickAccessItemsPanel = new InnerItem();
      if (!this.DesignMode)
        this.quickAccessItemsPanel.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.quickAccessItemsPanel);
      this.overFlowButton = new RadQuickAccessOverflowButton();
      this.overFlowButton.Class = "QuickAccessToolBarOverFlow";
      this.overFlowButton.Click += new EventHandler(this.overFlowButton_Click);
      this.overFlowButton.Visibility = ElementVisibility.Collapsed;
      int num = (int) this.overFlowButton.OverFlowPrimitive.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleCenter);
      this.overFlowButton.ThemeRole = "QuickAccessOverflowButton";
      this.overFlowButton.StretchHorizontally = false;
      this.Children.Add((RadElement) this.overFlowButton);
      this.items.Owner = (RadElement) this.quickAccessItemsPanel.ContentLayout;
    }

    public RadRibbonBarElement ParentRibbonBar
    {
      get
      {
        return this.FindAncestor<RadRibbonBarElement>();
      }
    }

    public InnerItem InnerItem
    {
      get
      {
        return this.quickAccessItemsPanel;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadMenuItem ToolbarPositionMenuItem
    {
      get
      {
        return this.toolbarPositionMenuItem;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadMenuItem MinimizeRibonMenuItem
    {
      get
      {
        return this.minimizeRibonMenuItem;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadQuickAccessOverflowButton OverflowButtonElement
    {
      get
      {
        return this.overFlowButton;
      }
    }

    public void SetItemVisibility(RadItem item, bool isVisible)
    {
      if (!this.items.Contains(item))
        throw new InvalidOperationException("Item not found in the target collection.");
      if (!isVisible)
        item.Visibility = ElementVisibility.Collapsed;
      else
        item.Visibility = ElementVisibility.Visible;
      int num = (int) item.SetValue(RadQuickAccessToolBar.IsCollapsedByUserProperty, (object) !isVisible);
      this.InvalidateMeasure(true);
      this.UpdateLayout();
      this.ParentRibbonBar.RibbonCaption.InvalidateMeasure(true);
      this.ParentRibbonBar.RibbonCaption.UpdateLayout();
    }

    public RadItem GetLastVisibleItem()
    {
      for (int index = this.Items.Count - 1; index > -1; --index)
      {
        RadItem radItem = this.Items[index];
        if (radItem.Visibility == ElementVisibility.Visible)
          return radItem;
      }
      return (RadItem) null;
    }

    public RadItem GetFirstCollapsedItem()
    {
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem.Visibility == ElementVisibility.Collapsed && !(bool) radItem.GetValue(RadQuickAccessToolBar.IsCollapsedByUserProperty))
          return radItem;
      }
      return (RadItem) null;
    }

    private void OnRadQuickAccessBar_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation == ItemsChangeOperation.Inserted && ((object) target.GetType() == (object) typeof (RadButtonElement) || target.GetType().IsSubclassOf(typeof (RadButtonElement))))
      {
        RadButtonElement radButtonElement = target as RadButtonElement;
        radButtonElement.Class = "RibbonBarButtonElement";
        radButtonElement.BorderElement.Class = "ButtonInRibbonBorder";
        radButtonElement.ButtonFillElement.Class = "ButtonInRibbonFill";
        radButtonElement.StretchHorizontally = false;
        radButtonElement.StretchVertically = false;
      }
      if (this.DesignMode)
        return;
      ElementVisibility elementVisibility = this.Items.Count > 0 ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      int num1 = (int) this.overFlowButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) elementVisibility);
      int num2 = (int) this.quickAccessItemsPanel.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) elementVisibility);
    }

    private void overFlowButton_Click(object sender, EventArgs e)
    {
      this.PrepareDropDownItems();
    }

    private void OnMinimizeItem_Click(object sender, EventArgs e)
    {
      if (this.ParentRibbonBar.ElementTree == null)
        return;
      RadRibbonBar control = (RadRibbonBar) this.ParentRibbonBar.ElementTree.Control;
      if (control.CommandTabs.Count <= 0)
        return;
      control.Expanded = !control.Expanded;
    }

    private void OnMenuItemQuickAccessPosition_Click(object sender, EventArgs e)
    {
      this.toolbarPositionMenuItem.Text = this.ParentRibbonBar.LocalizationSettings.ShowQuickAccessMenuBelowItemText;
      this.ParentRibbonBar.QuickAccessToolbarBelowRibbon = !this.ParentRibbonBar.QuickAccessToolbarBelowRibbon;
      if (this.ParentRibbonBar.QuickAccessToolbarBelowRibbon)
        this.toolbarPositionMenuItem.Text = this.ParentRibbonBar.LocalizationSettings.ShowQuickAccessMenuAboveItemText;
      this.ParentRibbonBar.InvalidateMeasure(true);
      this.ParentRibbonBar.UpdateLayout();
    }

    private void OnMenuItemShowHideItem_Click(object sender, EventArgs e)
    {
      RadItem associatedItem = (sender as RadMenuAssociatedItem).AssociatedItem;
      this.ResetQuickAccessItemVisibility(sender as RadMenuAssociatedItem, (bool) associatedItem.GetValue(RadQuickAccessToolBar.IsCollapsedByUserProperty));
    }

    private void UnwireEvents()
    {
      this.overFlowButton.Items.Remove((RadItem) this.toolbarPositionMenuItem);
      this.overFlowButton.Items.Remove((RadItem) this.minimizeRibonMenuItem);
      while (this.overFlowButton.Items.Count > 0)
      {
        RadItem radItem = this.overFlowButton.Items[0];
        radItem.Click -= new EventHandler(this.OnMenuItemShowHideItem_Click);
        radItem.Dispose();
      }
      this.toolbarPositionMenuItem.Click -= new EventHandler(this.OnMenuItemQuickAccessPosition_Click);
      this.minimizeRibonMenuItem.Click -= new EventHandler(this.OnMinimizeItem_Click);
    }

    private void PrepareDropDownItems()
    {
      this.UnwireEvents();
      foreach (RadItem associatedItem in (RadItemCollection) this.Items)
      {
        if (!(associatedItem is CommandBarSeparator))
        {
          RadMenuAssociatedItem menuAssociatedItem = new RadMenuAssociatedItem(associatedItem);
          menuAssociatedItem.Text = associatedItem.Text;
          if (!(bool) associatedItem.GetValue(RadQuickAccessToolBar.IsCollapsedByUserProperty))
            menuAssociatedItem.IsChecked = true;
          else
            menuAssociatedItem.IsChecked = false;
          menuAssociatedItem.Click += new EventHandler(this.OnMenuItemShowHideItem_Click);
          this.overFlowButton.Items.Add((RadItem) menuAssociatedItem);
        }
      }
      this.overFlowButton.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.toolbarPositionMenuItem.Text = this.ParentRibbonBar.QuickAccessToolbarBelowRibbon ? this.ParentRibbonBar.LocalizationSettings.ShowQuickAccessMenuAboveItemText : this.ParentRibbonBar.LocalizationSettings.ShowQuickAccessMenuBelowItemText;
      this.overFlowButton.Items.Add((RadItem) this.toolbarPositionMenuItem);
      this.overFlowButton.Items.Add((RadItem) this.minimizeRibonMenuItem);
      RadRibbonBar control = this.ParentRibbonBar.ElementTree.Control as RadRibbonBar;
      if (control != null)
      {
        if (control.Expanded)
          this.minimizeRibonMenuItem.Text = this.ParentRibbonBar.LocalizationSettings.MinimizeRibbonItemText;
        else
          this.minimizeRibonMenuItem.Text = this.ParentRibbonBar.LocalizationSettings.MaximizeRibbonItemText;
      }
      this.toolbarPositionMenuItem.Click += new EventHandler(this.OnMenuItemQuickAccessPosition_Click);
      this.minimizeRibonMenuItem.Click += new EventHandler(this.OnMinimizeItem_Click);
    }

    private void ResetQuickAccessItemVisibility(RadMenuAssociatedItem item, bool visible)
    {
      if (item.AssociatedItem == null)
        return;
      item.IsChecked = visible;
      this.SetItemVisibility(item.AssociatedItem, visible);
    }
  }
}
