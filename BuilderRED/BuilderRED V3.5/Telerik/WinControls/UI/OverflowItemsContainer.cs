// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.OverflowItemsContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class OverflowItemsContainer : RadPageViewElementBase
  {
    public static RadProperty ItemSelectedProperty = RadProperty.Register(nameof (ItemSelectedProperty), typeof (bool), typeof (RadPageViewButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private RadDropDownMenu overflowMenu;
    private RadMenuItem showMoreButtons;
    private RadMenuItem showFewerButtons;
    private RadMenuItem addRemoveButtons;
    private RadPageViewOutlookOverflowButton overflowButtonElement;
    private StackLayoutPanel buttonsContainer;
    private RadPageViewOutlookElement owner;

    [Browsable(false)]
    public StackLayoutPanel OverflowButtonsContainer
    {
      get
      {
        return this.buttonsContainer;
      }
    }

    [Browsable(false)]
    public RadPageViewOutlookOverflowButton OverflowButtonElement
    {
      get
      {
        return this.overflowButtonElement;
      }
    }

    [Browsable(false)]
    public RadDropDownMenu OverflowMenu
    {
      get
      {
        return this.overflowMenu;
      }
    }

    [Browsable(false)]
    public RadMenuItem ShowFewerButtonsItem
    {
      get
      {
        return this.showFewerButtons;
      }
    }

    [Browsable(false)]
    public RadMenuItem ShowMoreButtonsItem
    {
      get
      {
        return this.showMoreButtons;
      }
    }

    [Browsable(false)]
    public RadMenuItem AddRemoveButtonsItem
    {
      get
      {
        return this.addRemoveButtons;
      }
    }

    public OverflowItemsContainer(RadPageViewOutlookElement owner)
    {
      this.owner = owner;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.overflowButtonElement = new RadPageViewOutlookOverflowButton();
      this.overflowButtonElement.ThemeRole = "OverflowMenuButton";
      this.overflowButtonElement.StretchVertically = true;
      this.overflowMenu = new RadDropDownMenu();
      this.buttonsContainer = new StackLayoutPanel();
      RadPageViewLocalizationProvider currentProvider = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider;
      this.showMoreButtons = new RadMenuItem(currentProvider.GetLocalizedString("ShowMoreButtonsItemCaption"));
      int num1 = (int) this.showMoreButtons.SetDefaultValueOverride(RadButtonItem.ImageAlignmentProperty, (object) ContentAlignment.MiddleCenter);
      this.showFewerButtons = new RadMenuItem(currentProvider.GetLocalizedString("ShowFewerButtonsItemCaption"));
      int num2 = (int) this.showFewerButtons.SetDefaultValueOverride(RadButtonItem.ImageAlignmentProperty, (object) ContentAlignment.MiddleCenter);
      this.addRemoveButtons = new RadMenuItem(currentProvider.GetLocalizedString("AddRemoveButtonsItemCaption"));
      this.overflowMenu.Items.Add((RadItem) this.showMoreButtons);
      this.overflowMenu.Items.Add((RadItem) this.showFewerButtons);
      this.overflowMenu.Items.Add((RadItem) this.addRemoveButtons);
      this.WireEvents();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.buttonsContainer.Orientation = Orientation.Horizontal;
      this.buttonsContainer.StretchVertically = true;
      this.buttonsContainer.Children.Insert(this.buttonsContainer.Children.Count, (RadElement) this.overflowButtonElement);
      this.Children.Add((RadElement) this.buttonsContainer);
    }

    private void WireEvents()
    {
      this.overflowButtonElement.Click += new EventHandler(this.OnOverflowButtonElement_Click);
      this.showMoreButtons.Click += new EventHandler(this.OnShowMoreButtons_Click);
      this.showFewerButtons.Click += new EventHandler(this.OnShowFewerButtons_Click);
      this.overflowMenu.PopupOpened += new RadPopupOpenedEventHandler(this.OnOverflowMenu_Shown);
      this.overflowMenu.PopupClosed += new RadPopupClosedEventHandler(this.overflowMenu_PopupClosed);
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
    }

    private void UnwireEvents()
    {
      this.overflowButtonElement.Click -= new EventHandler(this.OnOverflowButtonElement_Click);
      this.showMoreButtons.Click -= new EventHandler(this.OnShowMoreButtons_Click);
      this.showFewerButtons.Click -= new EventHandler(this.OnShowFewerButtons_Click);
      this.overflowMenu.PopupOpened -= new RadPopupOpenedEventHandler(this.OnOverflowMenu_Shown);
      this.overflowMenu.PopupClosed -= new RadPopupClosedEventHandler(this.overflowMenu_PopupClosed);
      LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadPageViewLocalizationProvider_CurrentProviderChanged);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.buttonsContainer.Arrange(new RectangleF()
      {
        X = this.RightToLeft ? 0.0f : clientRectangle.Width - (this.buttonsContainer.DesiredSize.Width + (float) this.buttonsContainer.Margin.Right),
        Y = clientRectangle.Y,
        Size = new SizeF(this.buttonsContainer.DesiredSize.Width, clientRectangle.Height)
      });
      return sizeF;
    }

    private RadPageViewOutlookAssociatedButton CreateCollapsedItemButton(
      RadPageViewOutlookItem item)
    {
      RadPageViewOutlookAssociatedButton associatedButton = new RadPageViewOutlookAssociatedButton();
      associatedButton.StretchVertically = true;
      associatedButton.ThemeRole = "ItemAssociatedButton";
      if (item.Image != null)
        associatedButton.Image = item.Image;
      else
        associatedButton.Image = (Image) RadPageViewOutlookElement.AssociatedButtonDefaultImage;
      associatedButton.Tag = (object) item;
      return associatedButton;
    }

    public void RegisterCollapsedItem(RadPageViewOutlookItem stackItem)
    {
      RadPageViewOutlookAssociatedButton collapsedItemButton = this.CreateCollapsedItemButton(stackItem);
      collapsedItemButton.Click += new EventHandler(this.OnHiddenItemButton_Click);
      stackItem.AssociatedOverflowButton = collapsedItemButton;
      collapsedItemButton.ToolTipText = stackItem.Text;
      this.buttonsContainer.Children.Insert(0, (RadElement) collapsedItemButton);
    }

    public void UnregisterCollapsedItem(RadPageViewOutlookItem stackItem)
    {
      RadPageViewButtonElement associatedOverflowButton = (RadPageViewButtonElement) stackItem.AssociatedOverflowButton;
      associatedOverflowButton.Click -= new EventHandler(this.OnHiddenItemButton_Click);
      this.buttonsContainer.Children.Remove((RadElement) associatedOverflowButton);
      stackItem.AssociatedOverflowButton = (RadPageViewOutlookAssociatedButton) null;
    }

    private int GetVisibleAssociatedButtonsCount()
    {
      int num = 0;
      foreach (RadPageViewButtonElement child in this.buttonsContainer.Children)
      {
        if (child is RadPageViewOutlookAssociatedButton && child.Visibility == ElementVisibility.Visible)
          ++num;
      }
      return num;
    }

    private void PrepareDropDownItems()
    {
      foreach (RadElement radElement in (RadItemCollection) this.addRemoveButtons.Items)
        radElement.Click -= new EventHandler(this.OnAvailableItem_Click);
      this.addRemoveButtons.Items.Clear();
      foreach (RadPageViewOutlookItem pageViewOutlookItem in (IEnumerable<RadPageViewItem>) this.owner.Items)
      {
        RadMenuItem radMenuItem = new RadMenuItem(pageViewOutlookItem.Text, (object) pageViewOutlookItem);
        radMenuItem.Image = pageViewOutlookItem.Image;
        radMenuItem.Layout.ImagePrimitive.ImageScaling = ImageScaling.SizeToFit;
        radMenuItem.IsChecked = !this.owner.UncheckedItems.Contains(pageViewOutlookItem);
        radMenuItem.Click += new EventHandler(this.OnAvailableItem_Click);
        this.addRemoveButtons.Items.Add((RadItem) radMenuItem);
      }
      int associatedButtonsCount = this.GetVisibleAssociatedButtonsCount();
      this.showMoreButtons.Enabled = associatedButtonsCount > 0;
      this.showFewerButtons.Enabled = associatedButtonsCount != this.owner.Items.Count - this.owner.UncheckedItems.Count;
    }

    private void RadPageViewLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      RadPageViewLocalizationProvider currentProvider = LocalizationProvider<RadPageViewLocalizationProvider>.CurrentProvider;
      this.showMoreButtons.Text = currentProvider.GetLocalizedString("ShowMoreButtonsItemCaption");
      this.showFewerButtons.Text = currentProvider.GetLocalizedString("ShowFewerButtonsItemCaption");
      this.addRemoveButtons.Text = currentProvider.GetLocalizedString("AddRemoveButtonsItemCaption");
    }

    private void OnShowFewerButtons_Click(object sender, EventArgs e)
    {
      this.owner.DragGripDown();
    }

    private void OnShowMoreButtons_Click(object sender, EventArgs e)
    {
      this.owner.DragGripUp();
    }

    private void OnAvailableItem_Click(object sender, EventArgs e)
    {
      RadMenuItem radMenuItem = sender as RadMenuItem;
      RadPageViewOutlookItem tag = radMenuItem.Tag as RadPageViewOutlookItem;
      if (radMenuItem.IsChecked)
      {
        this.owner.UncheckItem(tag);
        if (tag.AssociatedOverflowButton != null)
          tag.AssociatedOverflowButton.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        this.owner.CheckItem(tag);
        if (tag.AssociatedOverflowButton != null)
          tag.AssociatedOverflowButton.Visibility = ElementVisibility.Visible;
      }
      radMenuItem.IsChecked = !radMenuItem.IsChecked;
    }

    private void OnHiddenItemButton_Click(object sender, EventArgs e)
    {
      RadPageViewButtonElement viewButtonElement = sender as RadPageViewButtonElement;
      this.owner.SelectItem((RadPageViewItem) (viewButtonElement.Tag as RadPageViewStackItem));
      this.owner.OnItemAssociatedButtonClick((object) viewButtonElement, e);
    }

    private void OnOverflowButtonElement_Click(object sender, EventArgs e)
    {
      this.PrepareDropDownItems();
      if (!this.overflowMenu.IsLoaded)
        this.overflowMenu.LoadElementTree();
      if (this.overflowMenu.ThemeName != this.ElementTree.ThemeName)
        this.overflowMenu.ThemeName = this.ElementTree.ThemeName;
      Rectangle screen = this.ElementTree.Control.RectangleToScreen(this.overflowButtonElement.ControlBoundingRectangle);
      this.overflowMenu.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.overflowMenu.HorizontalPopupAlignment = this.RightToLeft ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
      this.overflowMenu.ShowPopup(screen);
    }

    private void overflowMenu_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      int num = (int) this.overflowButtonElement.SetValue(RadPageViewOutlookOverflowButton.OverflowMenuOpenedProperty, (object) false);
    }

    private void OnOverflowMenu_Shown(object sender, EventArgs args)
    {
      int num = (int) this.overflowButtonElement.SetValue(RadPageViewOutlookOverflowButton.OverflowMenuOpenedProperty, (object) true);
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }
  }
}
