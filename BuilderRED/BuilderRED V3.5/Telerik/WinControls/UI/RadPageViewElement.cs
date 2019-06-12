// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public abstract class RadPageViewElement : RadPageViewElementBase
  {
    public static RadProperty ShowItemCloseButtonProperty = RadProperty.Register(nameof (ShowItemCloseButton), typeof (bool), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemDragModeProperty = RadProperty.Register(nameof (ItemDragMode), typeof (PageViewItemDragMode), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) PageViewItemDragMode.None, ElementPropertyOptions.None));
    public static RadProperty ItemDragHintProperty = RadProperty.Register(nameof (ItemDragHint), typeof (RadImageShape), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    public static RadProperty ItemBorderAndFillOrientationProperty = RadProperty.Register(nameof (ItemBorderAndFillOrientation), typeof (PageViewContentOrientation), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) PageViewContentOrientation.Auto, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EnsureSelectedItemVisibleProperty = RadProperty.Register(nameof (EnsureSelectedItemVisible), typeof (bool), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemContentOrientationProperty = RadProperty.Register(nameof (ItemContentOrientation), typeof (PageViewContentOrientation), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) PageViewContentOrientation.Auto, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemSizeModeProperty = RadProperty.Register(nameof (ItemSizeMode), typeof (PageViewItemSizeMode), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) PageViewItemSizeMode.EqualHeight, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemSpacingProperty = RadProperty.Register(nameof (ItemSpacing), typeof (int), typeof (RadPageViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private bool selectionWrap = true;
    private Dictionary<int, RadPageViewElement.ItemUpdateInfo> itemsToUpdateOnHandleCreated = new Dictionary<int, RadPageViewElement.ItemUpdateInfo>();
    private string editText = string.Empty;
    private RadContextMenu itemListMenu;
    private RadPageViewContentAreaElement contentArea;
    private RadPageViewLabelElement header;
    private RadPageViewLabelElement footer;
    private RadPageViewReadonlyCollection<RadPageViewItem> items;
    private MouseButtons actionMouseButton;
    private RadDragDropService itemDragService;
    private IInputEditor activeEditor;
    private bool allowEdit;
    private RadPageView owner;
    internal RadPageViewItem selectedItem;
    private bool updateSelectedItemContent;
    private RadPageViewPage defaultPage;
    private Size itemSize;

    public event EventHandler ItemClicked;

    public event EventHandler<RadPageViewItemCreatingEventArgs> ItemCreating;

    public event EventHandler<RadPageViewItemSelectingEventArgs> ItemSelecting;

    public event EventHandler<RadPageViewItemSelectedEventArgs> ItemSelected;

    public event EventHandler<RadPageViewItemsChangedEventArgs> ItemsChanged;

    public event EventHandler<RadPageViewItemDroppingEventArgs> ItemDropping;

    public event EventHandler<RadPageViewItemDroppedEventArgs> ItemDropped;

    public event EventHandler<RadPageViewEditorEventArgs> EditorInitialized;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.itemDragService = (RadDragDropService) new RadPageViewDragDropService(this);
      this.items = new RadPageViewReadonlyCollection<RadPageViewItem>();
      this.actionMouseButton = MouseButtons.Left;
      this.itemListMenu = new RadContextMenu();
      this.itemListMenu.DropDownClosed += new EventHandler(this.OnItemListMenu_DropDownClosed);
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.AllowDrop = true;
      this.UpdateSelectedItemContent = true;
    }

    protected override void CreateChildElements()
    {
      this.contentArea = new RadPageViewContentAreaElement();
      this.contentArea.Owner = this;
      this.Children.Add((RadElement) this.contentArea);
      this.header = new RadPageViewLabelElement();
      this.header.Class = "PageViewHeader";
      this.header.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.header);
      this.footer = new RadPageViewLabelElement();
      this.footer.Class = "PageViewFooter";
      this.footer.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.footer);
    }

    protected override void DisposeManagedResources()
    {
      if (this.itemDragService != null && !this.itemDragService.IsDisposed)
        this.itemDragService.Dispose();
      this.itemListMenu.DropDownClosed -= new EventHandler(this.OnItemListMenu_DropDownClosed);
      this.itemListMenu.Dispose();
      this.items.Clear();
      this.selectedItem = (RadPageViewItem) null;
      this.owner = (RadPageView) null;
      base.DisposeManagedResources();
    }

    internal abstract PageViewLayoutInfo ItemLayoutInfo { get; }

    protected abstract RadElement ItemsParent { get; }

    [RadPropertyDefaultValue("ShowItemCloseButton", typeof (RadPageViewStripElement))]
    [Description("Determines CloseButton will be displayed in each item, allowing that item to be closed.")]
    [Category("Behavior")]
    public bool ShowItemCloseButton
    {
      get
      {
        return (bool) this.GetValue(RadPageViewElement.ShowItemCloseButtonProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.ShowItemCloseButtonProperty, (object) value);
      }
    }

    [VsbBrowsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadImageShape ItemDragHint
    {
      get
      {
        return (RadImageShape) this.GetValue(RadPageViewElement.ItemDragHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.ItemDragHintProperty, (object) value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadDragDropService ItemDragService
    {
      get
      {
        return this.itemDragService;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Cannot set null as item drag service.");
        this.itemDragService = value;
      }
    }

    [Description("Gets or sets the mode that controls item drag operation within the element.")]
    [DefaultValue(PageViewItemDragMode.None)]
    public PageViewItemDragMode ItemDragMode
    {
      get
      {
        return (PageViewItemDragMode) this.GetValue(RadPageViewElement.ItemDragModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.ItemDragModeProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("EnsureSelectedItemVisible", typeof (RadPageViewElement))]
    [Description("Determines whether the currently selected item will be automatically scrolled into view.")]
    public bool EnsureSelectedItemVisible
    {
      get
      {
        return (bool) this.GetValue(RadPageViewElement.EnsureSelectedItemVisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.EnsureSelectedItemVisibleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ItemSpacing", typeof (RadPageViewElement))]
    [Description("Gets or sets the spacing between two items within the element.")]
    public int ItemSpacing
    {
      get
      {
        return (int) this.GetValue(RadPageViewElement.ItemSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.ItemSpacingProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the text orientation of the item within the owning RadPageViewElement instance.")]
    [RadPropertyDefaultValue("ItemSizeMode", typeof (RadPageViewElement))]
    public PageViewItemSizeMode ItemSizeMode
    {
      get
      {
        return (PageViewItemSizeMode) this.GetValue(RadPageViewElement.ItemSizeModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.ItemSizeModeProperty, (object) value);
      }
    }

    [Description("Gets or sets the text orientation of the item within the owning RadPageViewElement instance.")]
    [RadPropertyDefaultValue("ItemContentOrientation", typeof (RadPageViewElement))]
    [Category("Appearance")]
    public PageViewContentOrientation ItemContentOrientation
    {
      get
      {
        return (PageViewContentOrientation) this.GetValue(RadPageViewElement.ItemContentOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.ItemContentOrientationProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ItemBorderAndFillOrientation", typeof (RadPageViewElement))]
    [Category("Appearance")]
    [Description("Defines how each item's border and fill is oriented within this instance.")]
    public PageViewContentOrientation ItemBorderAndFillOrientation
    {
      get
      {
        return (PageViewContentOrientation) this.GetValue(RadPageViewElement.ItemBorderAndFillOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewElement.ItemBorderAndFillOrientationProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPageView Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    [Browsable(false)]
    public RadPageViewElementBase ContentArea
    {
      get
      {
        return (RadPageViewElementBase) this.contentArea;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the header element of the view.")]
    public virtual RadPageViewLabelElement Header
    {
      get
      {
        return this.header;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the footer element of the view.")]
    public virtual RadPageViewLabelElement Footer
    {
      get
      {
        return this.footer;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadPageViewItem SelectedItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        this.VerifyUnboundMode();
        this.SetSelectedItem(value);
      }
    }

    [Description("Gets or sets the mouse button that will be used to select items. Equals to MouseButtons.Left by default.")]
    [DefaultValue(MouseButtons.Left)]
    public MouseButtons ActionMouseButton
    {
      get
      {
        return this.actionMouseButton;
      }
      set
      {
        this.actionMouseButton = value;
      }
    }

    [Browsable(false)]
    public IReadOnlyCollection<RadPageViewItem> Items
    {
      get
      {
        return (IReadOnlyCollection<RadPageViewItem>) this.items;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets whether the pages will be wrapped around when performing selection using the arrow keys.")]
    public bool SelectionWrap
    {
      get
      {
        return this.selectionWrap;
      }
      set
      {
        if (this.selectionWrap == value)
          return;
        this.selectionWrap = value;
        this.OnNotifyPropertyChanged(nameof (SelectionWrap));
      }
    }

    [Description("Determines whether selecting an item will update the element's ContentArea.")]
    [DefaultValue(true)]
    public bool UpdateSelectedItemContent
    {
      get
      {
        return this.updateSelectedItemContent;
      }
      set
      {
        this.updateSelectedItemContent = value;
      }
    }

    public RadPageViewPage DefaultPage
    {
      get
      {
        return this.defaultPage;
      }
      set
      {
        this.defaultPage = value;
      }
    }

    public Size ItemSize
    {
      get
      {
        return this.itemSize;
      }
      set
      {
        this.itemSize = value;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (this.owner == null)
        return;
      RadPageViewPage selectedPage = this.owner.SelectedPage;
      if (selectedPage != null)
        selectedPage.Visible = true;
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      if (!this.ElementTree.Control.IsHandleCreated)
        this.ElementTree.Control.HandleCreated += new EventHandler(this.Control_HandleCreated);
      else
        this.UpdateItemsOnHandleCreated();
    }

    protected abstract RadPageViewItem CreateItem();

    public virtual RadPageViewContentAreaElement GetContentAreaForItem(
      RadPageViewItem item)
    {
      return this.contentArea;
    }

    public virtual Rectangle GetContentAreaRectangle()
    {
      return this.GetClientRectangleFromContentElement(this.GetContentAreaForItem(this.selectedItem));
    }

    public virtual Rectangle GetClientRectangleFromContentElement(
      RadPageViewContentAreaElement contentArea)
    {
      Rectangle boundingRectangle = contentArea.ControlBoundingRectangle;
      Padding padding = contentArea.Padding;
      Padding borderThickness = LightVisualElement.GetBorderThickness((LightVisualElement) contentArea, false);
      boundingRectangle.X += padding.Left + borderThickness.Left;
      boundingRectangle.Y += padding.Top + borderThickness.Top;
      boundingRectangle.Width -= padding.Horizontal + borderThickness.Horizontal;
      boundingRectangle.Height -= padding.Vertical + borderThickness.Vertical;
      return boundingRectangle;
    }

    public virtual RectangleF GetItemsRect()
    {
      return RectangleF.Empty;
    }

    public void DisplayItemListMenu(RadPageViewElementBase menuOwner)
    {
      this.DisplayItemListMenu(menuOwner, HorizontalPopupAlignment.LeftToLeft, VerticalPopupAlignment.TopToBottom);
    }

    public void DisplayItemListMenu(
      RadPageViewElementBase menuOwner,
      HorizontalPopupAlignment hAlign,
      VerticalPopupAlignment vAlign)
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      if (this.itemListMenu.DropDown.IsDisplayed)
        this.itemListMenu.DropDown.ClosePopup(RadPopupCloseReason.CloseCalled);
      List<RadMenuItemBase> items = new List<RadMenuItemBase>();
      foreach (RadPageViewItem radPageViewItem in (List<RadPageViewItem>) this.items)
      {
        if (radPageViewItem.Visibility != ElementVisibility.Collapsed)
        {
          RadMenuItem radMenuItem = new RadMenuItem(radPageViewItem.Text);
          radMenuItem.Image = radPageViewItem.Image;
          radMenuItem.Click += new EventHandler(this.OnItemListMenuItem_Click);
          radMenuItem.Tag = (object) radPageViewItem;
          int num = (int) radMenuItem.Layout.Text.SetDefaultValueOverride(TextPrimitive.UseMnemonicProperty, (object) false);
          if (radPageViewItem == this.selectedItem)
            radMenuItem.IsChecked = true;
          items.Add((RadMenuItemBase) radMenuItem);
        }
      }
      Rectangle screen = this.ElementTree.Control.RectangleToScreen(menuOwner.ControlBoundingRectangle);
      this.DisplayItemListMenu(new RadPageViewMenuDisplayingEventArgs(items, screen, hAlign, vAlign));
    }

    protected internal virtual void DisplayItemListMenu(RadPageViewMenuDisplayingEventArgs e)
    {
      if (this.owner != null)
        this.owner.OnItemListMenuDisplaying(e);
      if (e.Cancel)
        return;
      foreach (RadItem radItem in e.Items)
        this.itemListMenu.Items.Add(radItem);
      this.itemListMenu.DropDown.HorizontalPopupAlignment = e.HAlign;
      this.itemListMenu.DropDown.VerticalPopupAlignment = e.VAlign;
      RadControl control = this.ElementTree.Control as RadControl;
      if (control != null)
        this.itemListMenu.ThemeName = control.ThemeName;
      if (!this.itemListMenu.DropDown.IsLoaded)
        this.itemListMenu.DropDown.LoadElementTree();
      else
        this.itemListMenu.DropDown.RootElement.UpdateLayout();
      this.itemListMenu.DropDown.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.itemListMenu.DropDown.ShowPopup(e.AlignRect);
      if (this.owner == null)
        return;
      this.owner.OnItemListMenuDisplayed(EventArgs.Empty);
    }

    private void OnItemListMenuItem_Click(object sender, EventArgs e)
    {
      RadMenuItemBase menuItem = sender as RadMenuItemBase;
      RadPageViewItem tag = menuItem.Tag as RadPageViewItem;
      if (this.selectedItem == tag)
        this.EnsureItemVisible(tag);
      else
        this.SelectItem(tag);
      this.ClearMenuItem(menuItem);
    }

    private void OnItemListMenu_DropDownClosed(object sender, EventArgs e)
    {
      for (int index = this.itemListMenu.Items.Count - 1; index >= 0; --index)
      {
        RadMenuItemBase menuItem = this.itemListMenu.Items[index] as RadMenuItemBase;
        if (this.itemListMenu.DropDown.ClickedItem != menuItem)
          this.ClearMenuItem(menuItem);
      }
    }

    private void ClearMenuItem(RadMenuItemBase menuItem)
    {
      menuItem.Click -= new EventHandler(this.OnItemListMenuItem_Click);
      menuItem.Dispose();
    }

    public void AddItem(RadPageViewItem item)
    {
      this.VerifyUnboundMode();
      this.AddItemCore(item);
    }

    public void InsertItem(int index, RadPageViewItem item)
    {
      this.VerifyUnboundMode();
      this.InsertItemCore(index, item);
    }

    protected virtual void AddItemCore(RadPageViewItem item)
    {
      this.items.Add(item);
      item.Owner = this;
      this.SyncronizeItem(item);
      this.OnItemsChanged((object) this, new RadPageViewItemsChangedEventArgs(item, ItemsChangeOperation.Inserted));
      RadElement itemsParent = this.ItemsParent;
      if (itemsParent.IsDisposed)
        return;
      itemsParent?.Children.Add((RadElement) item);
      if (this.selectedItem != null)
        return;
      this.SelectItem(item);
    }

    protected virtual void InsertItemCore(int index, RadPageViewItem item)
    {
      this.items.Insert(index, item);
      item.Owner = this;
      this.SyncronizeItem(item);
      this.OnItemsChanged((object) this, new RadPageViewItemsChangedEventArgs(item, ItemsChangeOperation.Inserted));
      this.ItemsParent?.Children.Insert(index, (RadElement) item);
      if (this.selectedItem != null)
        return;
      this.SelectItem(item);
    }

    public void RemoveItem(RadPageViewItem item)
    {
      this.VerifyUnboundMode();
      this.RemoveItemCore(item);
    }

    protected virtual void RemoveItemCore(RadPageViewItem item)
    {
      int index = this.items.IndexOf(item);
      this.items.RemoveAt(index);
      item.Owner = (RadPageViewElement) null;
      this.OnItemsChanged((object) this, new RadPageViewItemsChangedEventArgs(item, ItemsChangeOperation.Removed));
      RadElement itemsParent = this.ItemsParent;
      if (itemsParent != null && itemsParent.Children.Contains((RadElement) item))
        itemsParent.Children.Remove((RadElement) item);
      if (this.items.Count == 0 && this.ItemLayoutInfo != null && (this.ItemLayoutInfo.items != null && this.ItemLayoutInfo.items.Count > 0))
        this.ItemLayoutInfo.items.Clear();
      if (item != this.selectedItem)
        return;
      item.IsSelected = false;
      if (index >= this.items.Count)
        --index;
      RadPageViewItem radPageViewItem = (RadPageViewItem) null;
      if (index >= 0 && index < this.items.Count)
        radPageViewItem = this.items[index];
      this.SelectItem(radPageViewItem);
    }

    public void SwapItems(RadPageViewItem item1, RadPageViewItem item2)
    {
      this.VerifyUnboundMode();
      int index1 = this.items.IndexOf(item1);
      int index2 = this.items.IndexOf(item2);
      if (index1 == -1 || index2 == -1)
        throw new IndexOutOfRangeException();
      this.SwapItemsCore(index1, index2);
    }

    public void SwapItems(int index1, int index2)
    {
      this.VerifyUnboundMode();
      if (index1 < 0 || index1 >= this.items.Count || (index2 < 0 || index2 >= this.items.Count))
        throw new IndexOutOfRangeException();
      this.SwapItemsCore(index1, index2);
    }

    protected virtual void SwapItemsCore(int index1, int index2)
    {
      RadPageViewItem radPageViewItem = this.items[index1];
      this.items[index1] = this.items[index2];
      this.items[index2] = radPageViewItem;
    }

    protected virtual void SetItemIndex(int currentIndex, int newIndex)
    {
      int num = this.ItemsParent.Children.IndexOf((RadElement) this.items[0]);
      RadPageViewItem radPageViewItem = this.items[currentIndex];
      this.items.RemoveAt(currentIndex);
      this.items.Insert(newIndex, radPageViewItem);
      this.ItemsParent.Children.Move(currentIndex + num, newIndex + num);
      this.ItemsParent.InvalidateMeasure();
      this.OnItemsChanged((object) this, new RadPageViewItemsChangedEventArgs(this.items[currentIndex], ItemsChangeOperation.Set));
      this.OnItemsChanged((object) this, new RadPageViewItemsChangedEventArgs(this.items[newIndex], ItemsChangeOperation.Set));
    }

    public RadPageViewItem FindItem(RadElement content)
    {
      foreach (RadPageViewItem radPageViewItem in (List<RadPageViewItem>) this.items)
      {
        if (radPageViewItem.Content == content)
          return radPageViewItem;
      }
      return (RadPageViewItem) null;
    }

    public RadPageViewItem GetItemAt(int index)
    {
      return this.items[index];
    }

    public RadPageViewItem ItemFromPoint(Point client)
    {
      foreach (RadPageViewItem radPageViewItem in (List<RadPageViewItem>) this.items)
      {
        if (radPageViewItem.Visibility == ElementVisibility.Visible && radPageViewItem.ControlBoundingRectangle.Contains(client))
          return radPageViewItem;
      }
      return (RadPageViewItem) null;
    }

    public bool EnsureItemVisible(RadPageViewItem item)
    {
      if (item == null)
        throw new ArgumentNullException("Item");
      if (item.Owner != this)
        throw new InvalidOperationException("Could not EnsureVisible item that is not hosted on this instance.");
      return this.EnsureItemVisibleCore(item);
    }

    protected internal virtual void SynchronizeItemsIndices()
    {
    }

    protected virtual bool EnsureItemVisibleCore(RadPageViewItem item)
    {
      return false;
    }

    protected internal virtual void SetSelectedItem(RadPageViewItem item)
    {
      if (this.selectedItem == item)
        return;
      if (this.UpdateSelectedItemContent)
        this.SetSelectedContent(item);
      RadPageViewItemSelectingEventArgs args = new RadPageViewItemSelectingEventArgs(this.selectedItem, item);
      this.OnItemSelecting((object) this, args);
      if (args.Cancel)
        return;
      if (this.selectedItem != null)
      {
        this.selectedItem.IsSelected = false;
        this.EndEdit();
      }
      RadPageViewItem selectedItem = this.selectedItem;
      this.selectedItem = item;
      if (this.selectedItem != null)
      {
        this.selectedItem.IsSelected = true;
        this.header.Text = this.selectedItem.Title;
        this.footer.Text = this.selectedItem.Description;
        if (this.EnsureSelectedItemVisible)
          this.EnsureItemVisibleCore(item);
      }
      else
      {
        int num1 = (int) this.header.ResetValue(RadItem.TextProperty);
        int num2 = (int) this.footer.ResetValue(RadItem.TextProperty);
      }
      this.OnItemSelected((object) this, new RadPageViewItemSelectedEventArgs(selectedItem, this.selectedItem));
    }

    protected internal virtual bool OnItemContentChanging(
      RadPageViewItem item,
      RadElement newContent)
    {
      return true;
    }

    protected internal virtual void OnItemContentChanged(RadPageViewItem item)
    {
    }

    protected internal virtual void CloseItem(RadPageViewItem item)
    {
      if (this.owner != null)
        this.owner.Pages.Remove(item.Page);
      else
        this.RemoveItem(item);
    }

    protected internal virtual void OnItemPropertyChanged(
      RadPageViewItem item,
      RadPropertyChangedEventArgs e)
    {
      if (item != this.selectedItem || e.Property != RadItem.TextProperty && e.Property != RadPageViewItem.TitleProperty && e.Property != RadPageViewItem.DescriptionProperty)
        return;
      this.header.Text = item.Title;
      this.footer.Text = item.Description;
    }

    protected virtual void SetSelectedContent(RadPageViewItem item)
    {
      if (this.owner != null)
      {
        if (this.selectedItem != null && this.selectedItem.Page != null)
        {
          if (this.owner.IsHandleCreated)
            this.selectedItem.Page.Visible = false;
          else
            this.SetPageVisibleOnHandleCreated(this.selectedItem.Page, false);
          if ((object) this.GetType() != (object) typeof (RadPageViewStripElement))
            ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Collapsed", (object) this.selectedItem.Text);
        }
        if (item == null || item.Page == null)
          return;
        this.UpdatePageBounds(item.Page);
        if (this.owner.IsHandleCreated)
          item.Page.Visible = true;
        else
          this.SetPageVisibleOnHandleCreated(item.Page, true);
        if ((object) this.GetType() == (object) typeof (RadPageViewStripElement))
          return;
        ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Expanded", (object) item.Page.Text);
      }
      else
      {
        this.contentArea.Children.Clear();
        if (item == null || item.Content == null)
          return;
        item.Content.StretchHorizontally = true;
        item.Content.StretchVertically = true;
        this.contentArea.Children.Add(item.Content);
      }
    }

    protected virtual void SyncronizeItem(RadPageViewItem item)
    {
      this.UpdateItemOrientation((IEnumerable) new RadItem[1]
      {
        (RadItem) item
      });
      this.InvalidateMeasure();
    }

    private void VerifyUnboundMode()
    {
      if (this.owner != null)
        throw new InvalidOperationException("Cannot freely modify items of a bound RadPageViewElement. Use Pages collection of the owning RadPageView instead.");
    }

    internal virtual void SelectItem(RadPageViewItem item)
    {
      if (this.owner != null)
      {
        if (item != null)
          this.owner.SelectedPage = item.Page;
        else
          this.owner.SelectedPage = (RadPageViewPage) null;
      }
      else
        this.SetSelectedItem(item);
    }

    public bool SelectNextItem()
    {
      if (this.selectedItem == null)
        return false;
      return this.SelectNextItemCore(this.selectedItem, true, this.SelectionWrap);
    }

    public bool SelectPreviousItem()
    {
      if (this.selectedItem == null)
        return false;
      return this.SelectNextItemCore(this.selectedItem, false, this.SelectionWrap);
    }

    protected virtual bool SelectNextItemCore(RadPageViewItem current, bool forward, bool wrap)
    {
      int count = this.items.Count;
      if (count <= 1)
        return false;
      int num = this.items.IndexOf(current);
      int index = num;
      if (forward)
      {
        do
        {
          ++index;
          if (index >= count && wrap)
            index = 0;
        }
        while (index >= 0 && index < count && (index != num && !this.CanSelectItem(this.items[index])));
      }
      else
      {
        do
        {
          --index;
          if (index < 0 && wrap)
            index = count - 1;
        }
        while (index >= 0 && index < count && (index != num && !this.CanSelectItem(this.items[index])));
      }
      if (index < 0 || index >= count)
        return false;
      this.SelectItem(this.items[index]);
      return true;
    }

    protected virtual bool CanSelectItem(RadPageViewItem item)
    {
      if (item.Visibility == ElementVisibility.Visible)
        return item.Enabled;
      return false;
    }

    protected internal virtual void OnItemMouseDown(RadPageViewItem sender, MouseEventArgs e)
    {
      if (e.Button != this.actionMouseButton || e.Clicks != 1)
        return;
      if (!sender.IsSelected)
      {
        this.SelectItem(sender);
      }
      else
      {
        if (!this.EnsureSelectedItemVisible)
          return;
        this.EnsureItemVisible(sender);
        this.BeginEdit();
      }
    }

    protected internal virtual void OnItemMouseUp(RadPageViewItem sender, MouseEventArgs e)
    {
    }

    protected internal virtual void OnItemDrag(RadPageViewItem sender, MouseEventArgs e)
    {
      if (this.IsDesignMode || this.ItemDragMode == PageViewItemDragMode.None)
        return;
      this.StartItemDrag(sender);
    }

    protected internal virtual void OnItemClick(RadPageViewItem sender, EventArgs e)
    {
      if (this.ItemClicked == null)
        return;
      this.ItemClicked((object) sender, e);
    }

    protected virtual RadPageViewItem OnItemCreating(
      RadPageViewItemCreatingEventArgs args)
    {
      if (this.ItemCreating != null)
        this.ItemCreating((object) this, args);
      return args.Item;
    }

    public virtual void OnItemSelected(object sender, RadPageViewItemSelectedEventArgs args)
    {
      if (this.ItemSelected == null)
        return;
      this.ItemSelected(sender, args);
    }

    public virtual void OnItemSelecting(object sender, RadPageViewItemSelectingEventArgs args)
    {
      if (this.ItemSelecting == null)
        return;
      this.ItemSelecting(sender, args);
    }

    protected virtual void OnItemDropped(object sender, RadPageViewItemDroppedEventArgs args)
    {
      if (this.ItemDropped == null)
        return;
      this.ItemDropped(sender, args);
    }

    protected virtual void OnItemDropping(object sender, RadPageViewItemDroppingEventArgs args)
    {
      if (this.ItemDropping == null)
        return;
      this.ItemDropping(sender, args);
    }

    protected virtual void OnItemsChanged(object sender, RadPageViewItemsChangedEventArgs args)
    {
      if (this.ItemsChanged == null)
        return;
      this.ItemsChanged(sender, args);
    }

    protected virtual void OnEditorInitialized(object sender, RadPageViewEditorEventArgs e)
    {
      if (this.EditorInitialized == null)
        return;
      this.EditorInitialized(sender, e);
    }

    protected internal virtual void ProcessKeyDown(KeyEventArgs e)
    {
      if (this.IsNextKey(e.KeyCode) && !this.IsEditing)
        this.SelectNextItem();
      else if (this.IsPreviousKey(e.KeyCode) && !this.IsEditing)
        this.SelectPreviousItem();
      else if (e.KeyCode == Keys.F2)
        this.BeginEdit();
      else if (e.KeyCode == Keys.Escape)
      {
        this.CancelEdit();
      }
      else
      {
        if (e.KeyCode != Keys.Return || !this.IsEditing || !this.ActiveEditor.Validate())
          return;
        this.EndEdit();
      }
    }

    protected internal virtual bool IsNextKey(Keys key)
    {
      if (this.RightToLeft)
        return key == Keys.Left;
      return key == Keys.Right;
    }

    protected internal virtual bool IsPreviousKey(Keys key)
    {
      if (this.RightToLeft)
        return key == Keys.Right;
      return key == Keys.Left;
    }

    protected internal virtual void OnPageAdded(RadPageViewEventArgs e)
    {
      RadPageViewItem radPageViewItem1 = this.CreateItem();
      RadPageViewItem radPageViewItem2 = this.OnItemCreating(new RadPageViewItemCreatingEventArgs(e.Page));
      if (radPageViewItem2 != null)
      {
        if (!radPageViewItem1.GetType().IsAssignableFrom(radPageViewItem2.GetType()))
          throw new ArgumentException(string.Format("The type of the custom item must inherit from {0}", (object) radPageViewItem1.GetType()), "Item");
        radPageViewItem1 = radPageViewItem2;
      }
      radPageViewItem1.Attach(e.Page);
      this.AddItemCore(radPageViewItem1);
    }

    protected internal virtual void OnPageRemoved(RadPageViewEventArgs e)
    {
      RadPageViewItem radPageViewItem = e.Page.Item;
      this.RemoveItemCore(radPageViewItem);
      radPageViewItem.Detach();
    }

    protected internal virtual void OnPageIndexChanged(RadPageViewIndexChangedEventArgs e)
    {
      this.SetItemIndex(e.OldIndex, e.NewIndex);
    }

    protected internal virtual void OnSelectedPageChanged(RadPageViewEventArgs e)
    {
      RadPageViewItem radPageViewItem = (RadPageViewItem) null;
      if (e.Page != null)
        radPageViewItem = e.Page.Item;
      this.SetSelectedItem(radPageViewItem);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      Padding borderThickness = this.GetBorderThickness(false);
      SizeF size = this.GetClientRectangle(availableSize).Size;
      SizeF sizeF1 = new SizeF((float) (this.Padding.Horizontal + borderThickness.Horizontal), (float) (this.Padding.Vertical + borderThickness.Vertical));
      this.MeasureExternalElements(size, availableSize);
      if (this.header.Visibility == ElementVisibility.Visible)
      {
        this.header.Measure(size);
        size.Height -= this.header.DesiredSize.Height + (float) this.header.Margin.Vertical;
        sizeF1.Height += this.header.DesiredSize.Height + (float) this.header.Margin.Vertical;
      }
      if (this.footer.Visibility == ElementVisibility.Visible)
      {
        this.footer.Measure(size);
        size.Height -= this.footer.DesiredSize.Height + (float) this.footer.Margin.Vertical;
        sizeF1.Height += this.footer.DesiredSize.Height + (float) this.footer.Margin.Vertical;
      }
      SizeF sizeF2 = this.MeasureItems(size);
      sizeF1.Width += sizeF2.Width;
      sizeF1.Height += sizeF2.Height;
      if (this.StretchHorizontally && !float.IsInfinity(availableSize.Width))
        sizeF1.Width = availableSize.Width;
      if (this.StretchVertically && !float.IsInfinity(availableSize.Height))
        sizeF1.Height = availableSize.Height;
      sizeF1.Width = Math.Min(sizeF1.Width, availableSize.Width);
      sizeF1.Height = Math.Min(sizeF1.Height, availableSize.Height);
      return sizeF1;
    }

    protected void MeasureExternalElements(SizeF clientSize, SizeF availableSize)
    {
      foreach (RadElement child in this.Children)
      {
        if (this.IsChildElementExternal(child))
          child.Measure(clientSize);
      }
    }

    protected void ArrangeExternalElements(RectangleF clientRect, SizeF finalSize)
    {
      foreach (RadElement child in this.Children)
      {
        if (this.IsChildElementExternal(child))
          child.Arrange(clientRect);
      }
    }

    protected virtual bool IsChildElementExternal(RadElement element)
    {
      if (element != this.header && element != this.footer && element != this.contentArea)
        return !(element is RadPageViewElementBase);
      return false;
    }

    protected virtual SizeF MeasureItems(SizeF availableSize)
    {
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.ArrangeExternalElements(clientRectangle, finalSize);
      Padding margin;
      if (this.header.Visibility != ElementVisibility.Collapsed)
      {
        margin = this.header.Margin;
        RectangleF finalRect = new RectangleF(clientRectangle.X + (float) margin.Left, clientRectangle.Y + (float) margin.Top, clientRectangle.Width - (float) margin.Horizontal, this.header.DesiredSize.Height);
        this.header.Arrange(finalRect);
        clientRectangle.Y += finalRect.Height + (float) margin.Vertical;
        clientRectangle.Height = Math.Max(0.0f, clientRectangle.Height - finalRect.Height - (float) margin.Vertical);
      }
      if (this.footer.Visibility != ElementVisibility.Collapsed)
      {
        margin = this.footer.Margin;
        RectangleF finalRect = new RectangleF(clientRectangle.X + (float) margin.Left, clientRectangle.Bottom - this.footer.DesiredSize.Height - (float) margin.Bottom, clientRectangle.Width - (float) margin.Horizontal, this.footer.DesiredSize.Height);
        this.footer.Arrange(finalRect);
        clientRectangle.Height = Math.Max(0.0f, clientRectangle.Height - finalRect.Height - (float) margin.Vertical);
      }
      this.PerformArrange(clientRectangle);
      return finalSize;
    }

    protected virtual RectangleF PerformArrange(RectangleF clientRect)
    {
      RectangleF clientRect1 = this.ArrangeItems(clientRect);
      this.ArrangeContent(clientRect1);
      return clientRect1;
    }

    protected virtual RectangleF ArrangeContent(RectangleF clientRect)
    {
      if (this.contentArea.Visibility != ElementVisibility.Collapsed)
      {
        clientRect = LayoutUtils.DeflateRect(clientRect, this.contentArea.Margin);
        this.contentArea.Arrange(clientRect);
        if (this.selectedItem != null && this.owner != null)
          this.selectedItem.Page.Bounds = this.GetContentAreaRectangle();
      }
      return clientRect;
    }

    protected virtual RectangleF ArrangeItems(RectangleF itemsRect)
    {
      return RectangleF.Empty;
    }

    protected internal virtual void OnContentBoundsChanged()
    {
      if (this.owner == null || this.ArrangeInProgress)
        return;
      RadPageViewPage selectedPage = this.owner.SelectedPage;
      if (selectedPage == null)
        return;
      this.UpdatePageBounds(selectedPage);
    }

    protected virtual void UpdatePageBounds(RadPageViewPage page)
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      Rectangle contentAreaRectangle = this.GetContentAreaRectangle();
      if (!(page.Bounds != contentAreaRectangle))
        return;
      page.Bounds = contentAreaRectangle;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadPageViewElement.ItemContentOrientationProperty || e.Property == RadPageViewElement.ItemBorderAndFillOrientationProperty)
      {
        this.UpdateItemOrientation((IEnumerable) this.items);
      }
      else
      {
        if (e.Property != RadPageViewElement.EnsureSelectedItemVisibleProperty || !(bool) e.NewValue || this.selectedItem == null)
          return;
        this.EnsureItemVisible(this.selectedItem);
      }
    }

    protected internal virtual PageViewContentOrientation GetAutomaticItemOrientation(
      bool content)
    {
      return PageViewContentOrientation.Horizontal;
    }

    protected virtual void UpdateItemOrientation(IEnumerable items)
    {
      PageViewContentOrientation orientation1 = this.ItemContentOrientation;
      if (orientation1 == PageViewContentOrientation.Auto)
        orientation1 = this.GetAutomaticItemOrientation(true);
      PageViewContentOrientation orientation2 = this.ItemBorderAndFillOrientation;
      if (orientation2 == PageViewContentOrientation.Auto)
        orientation2 = this.GetAutomaticItemOrientation(false);
      foreach (RadPageViewItem radPageViewItem in items)
      {
        radPageViewItem.SetContentOrientation(orientation1, false);
        radPageViewItem.SetBorderAndFillOrientation(orientation2, false);
      }
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      if (this.Owner == null || this.Owner.Pages.Count <= 1)
        return;
      this.Owner.SuspendEvents();
      if (this.Owner.SelectedPage == this.Owner.Pages[0])
      {
        this.Owner.SelectedPage = this.Owner.Pages[1];
        this.Owner.SelectedPage = this.Owner.Pages[0];
      }
      else
      {
        RadPageViewPage selectedPage = this.Owner.SelectedPage;
        this.Owner.SelectedPage = this.Owner.Pages[0];
        this.Owner.SelectedPage = selectedPage;
      }
      this.Owner.ResumeEvents();
    }

    protected internal virtual void StartItemDrag(RadPageViewItem item)
    {
      this.itemDragService.Start((object) item);
    }

    protected internal virtual void EndItemDrag(RadPageViewItem item)
    {
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      RadPageViewItem dragItem = dragObject as RadPageViewItem;
      if (dragItem == null || dragItem.Owner != this)
        return false;
      RadPageViewItem hitItem = this.ItemFromPoint(mousePosition);
      if (!this.CanDropOverItem(dragItem, hitItem))
        return false;
      this.EnsureItemVisible(hitItem);
      this.ItemsParent.UpdateLayout();
      return hitItem.ControlBoundingRectangle.Contains(mousePosition);
    }

    protected virtual bool CanDropOverItem(RadPageViewItem dragItem, RadPageViewItem hitItem)
    {
      if (hitItem == null)
        return false;
      return hitItem != dragItem;
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      RadPageViewItem dragItem = dragObject as RadPageViewItem;
      if (dragItem == null)
        return;
      RadPageViewItem hitItem = this.ItemFromPoint(dropLocation);
      if (hitItem == null)
        return;
      this.PerformItemDrop(dragItem, hitItem);
    }

    protected internal void PerformItemDrop(RadPageViewItem dragItem, RadPageViewItem hitItem)
    {
      RadPageViewItemDroppingEventArgs args = new RadPageViewItemDroppingEventArgs(dragItem, hitItem);
      this.OnItemDropping((object) this, args);
      if (args.Cancel)
        return;
      if (this.owner != null)
      {
        int newIndex = this.owner.Pages.IndexOf(hitItem.Page);
        this.owner.Pages.ChangeIndex(dragItem.Page, newIndex);
      }
      else
      {
        int newIndex = this.items.IndexOf(hitItem);
        this.SetItemIndex(this.items.IndexOf(dragItem), newIndex);
      }
      this.OnItemDropped((object) this, new RadPageViewItemDroppedEventArgs(dragItem, hitItem));
    }

    protected internal virtual Padding GetNCMetrics()
    {
      return Padding.Empty;
    }

    protected internal virtual void OnNCPaint(Graphics g)
    {
    }

    protected internal virtual bool EnableNCPainting
    {
      get
      {
        return false;
      }
    }

    protected internal virtual bool EnableNCModification
    {
      get
      {
        return false;
      }
    }

    private void Control_HandleCreated(object sender, EventArgs e)
    {
      ((Control) sender).HandleCreated -= new EventHandler(this.Control_HandleCreated);
      this.UpdateItemsOnHandleCreated();
    }

    private void SetPageVisibleOnHandleCreated(RadPageViewPage page, bool visible)
    {
      if (page == null)
        return;
      int hashCode = page.GetHashCode();
      if (this.itemsToUpdateOnHandleCreated.ContainsKey(hashCode))
      {
        RadPageViewElement.ItemUpdateInfo itemUpdateInfo = this.itemsToUpdateOnHandleCreated[hashCode];
        itemUpdateInfo.visible = visible;
        if (itemUpdateInfo.visible != page.Visible)
          return;
        this.itemsToUpdateOnHandleCreated.Remove(hashCode);
      }
      else
      {
        RadPageViewElement.ItemUpdateInfo itemUpdateInfo = new RadPageViewElement.ItemUpdateInfo(page, visible);
        this.itemsToUpdateOnHandleCreated.Add(hashCode, itemUpdateInfo);
      }
    }

    private void UpdateItemsOnHandleCreated()
    {
      RadPageView control = this.ElementTree.Control as RadPageView;
      foreach (RadPageViewElement.ItemUpdateInfo itemUpdateInfo in this.itemsToUpdateOnHandleCreated.Values)
      {
        if (control != null && control.Pages.Contains(itemUpdateInfo.page))
          itemUpdateInfo.page.Visible = itemUpdateInfo.visible;
      }
      this.itemsToUpdateOnHandleCreated.Clear();
    }

    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(false)]
    public bool AllowEdit
    {
      get
      {
        return this.allowEdit;
      }
      set
      {
        if (this.allowEdit == value)
          return;
        this.allowEdit = value;
        this.OnNotifyPropertyChanged(nameof (AllowEdit));
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsEditing
    {
      get
      {
        return this.ActiveEditor != null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    public bool BeginEdit()
    {
      if (!this.allowEdit || this.activeEditor != null || this.selectedItem == null)
        return false;
      this.activeEditor = (IInputEditor) this.CreateEditor();
      this.selectedItem.Children.Add(((BaseInputEditor) this.activeEditor).EditorElement);
      ISupportInitialize activeEditor = this.activeEditor as ISupportInitialize;
      activeEditor?.BeginInit();
      this.activeEditor.Initialize((object) this.selectedItem, (object) this.selectedItem.Text);
      activeEditor?.EndInit();
      this.editText = this.selectedItem.Text;
      this.selectedItem.Text = string.Empty;
      this.OnEditorInitialized((object) this, new RadPageViewEditorEventArgs(this.selectedItem.Page, this.activeEditor));
      this.activeEditor.BeginEdit();
      return true;
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    public bool EndEdit()
    {
      return this.EndEditCore(true);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CancelEdit()
    {
      this.EndEditCore(false);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    protected virtual bool EndEditCore(bool commitChanges)
    {
      if (!this.IsEditing || this.selectedItem == null || this.ActiveEditor == null)
        return false;
      if (commitChanges && this.ActiveEditor.IsModified)
      {
        this.selectedItem.Text = this.ActiveEditor.Value.ToString();
      }
      else
      {
        this.ActiveEditor.Value = (object) this.editText;
        this.selectedItem.Text = this.editText;
      }
      this.activeEditor.EndEdit();
      this.selectedItem.Children.Remove(((BaseInputEditor) this.activeEditor).EditorElement);
      this.InvalidateMeasure();
      this.UpdateLayout();
      this.activeEditor = (IInputEditor) null;
      return false;
    }

    private BaseInputEditor CreateEditor()
    {
      return (BaseInputEditor) new RadPageViewElement.PageViewItemTextEditor(this);
    }

    private class ItemUpdateInfo
    {
      public RadPageViewPage page;
      public bool visible;

      public ItemUpdateInfo(RadPageViewPage page, bool visible)
      {
        this.page = page;
        this.visible = visible;
      }
    }

    [ToolboxItem(false)]
    public class PageViewItemTextEditor : BaseInputEditor
    {
      private RadPageViewElement owner;

      public PageViewItemTextEditor(RadPageViewElement owner)
      {
        this.owner = owner;
      }

      public override object Value
      {
        get
        {
          return (object) ((RadItem) this.EditorElement).Text;
        }
        set
        {
          ((RadItem) this.EditorElement).Text = Convert.ToString(value);
        }
      }

      public override System.Type DataType
      {
        get
        {
          return typeof (string);
        }
      }

      public override void Initialize(object owner, object value)
      {
        base.Initialize(owner, value);
        RadTextBoxControlElement editorElement = this.EditorElement as RadTextBoxControlElement;
        editorElement.Focus();
        editorElement.SelectAll();
        editorElement.TextChanged += new EventHandler(this.editorElement_TextChanged);
        editorElement.TextChanging += new TextChangingEventHandler(this.editorElement_TextChanging);
      }

      protected override RadElement CreateEditorElement()
      {
        return (RadElement) new RadPageViewElement.PageViewItemTextEditorElement();
      }

      public override bool Validate()
      {
        ValueChangingEventArgs changingEventArgs = new ValueChangingEventArgs(this.Value);
        this.OnValidating((CancelEventArgs) changingEventArgs);
        if (changingEventArgs.Cancel)
        {
          this.OnValidationError(new ValidationErrorEventArgs(this.Value, (Exception) null));
          return false;
        }
        this.OnValidated();
        return true;
      }

      public override void BeginEdit()
      {
        base.BeginEdit();
        this.EditorElement.PropertyChanged += new PropertyChangedEventHandler(this.EditorElement_PropertyChanged);
      }

      public override bool EndEdit()
      {
        RadTextBoxControlElement editorElement = this.EditorElement as RadTextBoxControlElement;
        editorElement.TextChanged -= new EventHandler(this.editorElement_TextChanged);
        editorElement.TextChanging -= new TextChangingEventHandler(this.editorElement_TextChanging);
        this.EditorElement.PropertyChanged -= new PropertyChangedEventHandler(this.EditorElement_PropertyChanged);
        return base.EndEdit();
      }

      private void EditorElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
      {
        if (!(e.PropertyName == "ContainsFocus") || this.EditorElement.ContainsFocus)
          return;
        if (this.owner.ActiveEditor.Validate())
          this.owner.EndEdit();
        else
          this.owner.CancelEdit();
      }

      private void editorElement_TextChanging(object sender, TextChangingEventArgs e)
      {
        ValueChangingEventArgs e1 = new ValueChangingEventArgs((object) e.NewValue, (object) e.OldValue);
        this.OnValueChanging(e1);
        e.Cancel = e1.Cancel;
      }

      private void editorElement_TextChanged(object sender, EventArgs e)
      {
        this.OnValueChanged();
        this.owner.EnsureItemVisible(this.owner.SelectedItem);
      }
    }

    public class PageViewItemTextEditorElement : RadTextBoxControlElement
    {
      protected override SizeF MeasureOverride(SizeF availableSize)
      {
        SizeF sizeF = base.MeasureOverride(availableSize);
        if (float.IsPositiveInfinity(availableSize.Width) && string.IsNullOrEmpty(this.Text))
          sizeF.Width = 50f;
        if (float.IsPositiveInfinity(availableSize.Height) && string.IsNullOrEmpty(this.Text))
          sizeF.Height = 20f;
        return sizeF;
      }

      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (RadTextBoxControlElement);
        }
      }
    }
  }
}
