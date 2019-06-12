// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSizableDropDownMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadSizableDropDownMenu : RadEditorPopupControlBase, IItemsControl
  {
    private readonly Stack<RadMenuItemBase> itemsToClose = new Stack<RadMenuItemBase>();
    private readonly object syncObj = new object();
    private SizableDropDownMenuElement menuElement;
    private RadItemsControlImpl itemsControlImpl;
    private Timer childDropDownTimeout;

    public RadSizableDropDownMenu()
      : this((RadItem) null)
    {
    }

    public RadSizableDropDownMenu(RadItem owner)
      : base(owner)
    {
      this.menuElement = new SizableDropDownMenuElement();
      this.SizingGripDockLayout.Children.Add((RadElement) this.menuElement);
      this.SizingMode = SizingMode.UpDownAndRightBottom;
      this.itemsControlImpl = new RadItemsControlImpl(this.Items);
      this.itemsControlImpl.RollOverItemSelection = true;
      this.itemsControlImpl.ItemDeselected += new ItemSelectedEventHandler(this.itemsControlImpl_ItemDeselected);
      this.itemsControlImpl.ItemSelected += new ItemSelectedEventHandler(this.itemsControlImpl_ItemSelected);
      this.childDropDownTimeout = new Timer();
      this.childDropDownTimeout.Interval = SystemInformation.MenuShowDelay == 0 ? 40 : SystemInformation.MenuShowDelay;
      this.childDropDownTimeout.Tick += new EventHandler(this.OnChildrenDropDown_TimeOut);
      this.PopupClosed += new RadPopupClosedEventHandler(this.RadSizableDropDownMenu_PopupClosed);
    }

    private void RadSizableDropDownMenu_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      this.childDropDownTimeout.Tag = (object) null;
      this.childDropDownTimeout.Stop();
    }

    private void OnChildrenDropDown_TimeOut(object sender, EventArgs e)
    {
      this.childDropDownTimeout.Stop();
      this.childDropDownTimeout.Tag = (object) null;
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem == null)
        return;
      lock (this.syncObj)
      {
        while (this.itemsToClose.Count > 0)
        {
          this.itemsToClose.Peek().HideChildItems();
          this.itemsToClose.Pop();
        }
      }
      selectedItem.ShowChildItems();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      RadMenuItemBase elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location) as RadMenuItemBase;
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (elementAtPoint == null || !elementAtPoint.Enabled)
      {
        base.OnMouseMove(e);
      }
      else
      {
        if (selectedItem != null && !object.ReferenceEquals((object) selectedItem, (object) elementAtPoint))
        {
          selectedItem.Deselect();
          lock (this.syncObj)
            this.itemsToClose.Push(selectedItem);
        }
        if (object.ReferenceEquals((object) elementAtPoint, this.childDropDownTimeout.Tag))
        {
          this.childDropDownTimeout.Stop();
          this.childDropDownTimeout.Tag = (object) null;
        }
        else if (!object.ReferenceEquals((object) selectedItem, (object) elementAtPoint))
        {
          if (this.childDropDownTimeout.Tag == null)
            this.childDropDownTimeout.Tag = (object) selectedItem;
          this.childDropDownTimeout.Start();
        }
        base.OnMouseMove(e);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.childDropDownTimeout.Stop();
      base.OnMouseDown(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      this.EnsurePopupWidth();
      base.OnPaint(e);
    }

    private void EnsurePopupWidth()
    {
      int width = this.menuElement.LayoutPanel.Size.Width;
      if (this.Size.Width >= width)
        return;
      this.Size = new Size(width, this.Size.Height);
    }

    public override bool OnKeyDown(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Back:
          return false;
        case Keys.Return:
          RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
          if (selectedItem == null)
            return false;
          this.BeginInvoke((Delegate) new RadSizableDropDownMenu.PerformClickInvoker(this.PerformItemClick), (object) selectedItem);
          return true;
        case Keys.Left:
        case Keys.Right:
          if (!this.ContainsFocus || this.Focused)
            return this.ProcessLeftRightNavigationKey(keyData == Keys.Left);
          return false;
        case Keys.Up:
        case Keys.Down:
          if (!this.ContainsFocus || this.Focused)
            return this.ProcessUpDownNavigationKey(keyData == Keys.Up);
          return false;
        default:
          if (this.ProcessMnemonic(keyData))
            return true;
          return base.OnKeyDown(keyData);
      }
    }

    private void itemsControlImpl_ItemSelected(object sender, ItemSelectedEventArgs args)
    {
      this.OnItemSelected(args, sender);
    }

    protected virtual void OnItemSelected(ItemSelectedEventArgs args, object sender)
    {
      if (args.Item is RadMenuItemBase)
      {
        RadMenuItemBase radMenuItemBase = args.Item as RadMenuItemBase;
        radMenuItemBase.Selected = true;
        if (radMenuItemBase.Items.Count == 0 || !radMenuItemBase.IsPopupShown)
          this.AccessibilityNotifyClients(AccessibleEvents.Focus, this.Items.IndexOf((RadItem) radMenuItemBase));
      }
      if (this.ItemSelected == null)
        return;
      this.ItemSelected(sender, args);
    }

    private void itemsControlImpl_ItemDeselected(object sender, ItemSelectedEventArgs args)
    {
      this.OnItemDeselected(args, sender);
    }

    protected virtual void OnItemDeselected(ItemSelectedEventArgs args, object sender)
    {
      if (args.Item is RadMenuItemBase)
        (args.Item as RadMenuItemBase).Selected = false;
      if (this.ItemDeselected == null)
        return;
      this.ItemDeselected(sender, args);
    }

    public SizableDropDownMenuElement MenuElement
    {
      get
      {
        return this.menuElement;
      }
    }

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.MenuElement.Items;
      }
    }

    protected void PerformItemClick(RadMenuItemBase menuItem)
    {
      if (menuItem == null || !menuItem.Enabled)
        return;
      if (menuItem.HasChildren)
      {
        menuItem.ShowChildItems();
        menuItem.DropDown.SelectFirstVisibleItem();
      }
      menuItem.PerformClick();
    }

    protected virtual bool ProcessMnemonic(Keys keyData)
    {
      uint num1 = Telerik.WinControls.NativeMethods.MapVirtualKey((uint) keyData, 2U);
      if (num1 < 0U || num1 > (uint) ushort.MaxValue)
        return false;
      char charCode = Convert.ToChar(num1);
      List<RadItem> radItemList = new List<RadItem>();
      int num2 = -1;
      RadItem selectedItem = this.GetSelectedItem();
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (Control.IsMnemonic(charCode, radItem.Text) && radItem.Enabled && radItem.Visibility == ElementVisibility.Visible)
        {
          radItemList.Add(radItem);
          if (selectedItem == radItem)
            num2 = radItemList.Count - 1;
        }
      }
      if (radItemList.Count == 1)
      {
        radItemList[0].Select();
        this.BeginInvoke((Delegate) new RadSizableDropDownMenu.PerformClickInvoker(this.PerformItemClick), (object) radItemList[0]);
        return true;
      }
      if (radItemList.Count <= 0)
        return false;
      int index = (num2 + 1) % radItemList.Count;
      radItemList[index].Focus();
      this.SelectItem(radItemList[index]);
      return true;
    }

    protected virtual bool ProcessLeftRightNavigationKey(bool isLeft)
    {
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem == null)
        return false;
      if (!isLeft)
      {
        if (!selectedItem.HasChildren)
          return false;
        selectedItem.ShowChildItems();
        selectedItem.DropDown.SelectFirstVisibleItem();
        return true;
      }
      if (selectedItem.HierarchyParent == null || selectedItem.HierarchyParent.IsRootItem && this.OwnerPopup == null)
        return false;
      this.ClosePopup(RadPopupCloseReason.Keyboard);
      return true;
    }

    protected virtual bool ProcessUpDownNavigationKey(bool isUp)
    {
      RadMenuItemBase selectedItem = this.GetSelectedItem() as RadMenuItemBase;
      if (selectedItem != null)
        this.SelectNextItem((RadItem) selectedItem, !isUp);
      else if (!isUp)
        this.SelectFirstVisibleItem();
      else
        this.SelectLastVisibleItem();
      return true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.childDropDownTimeout.Dispose();
      base.Dispose(disposing);
    }

    public event ItemSelectedEventHandler ItemSelected;

    public event ItemSelectedEventHandler ItemDeselected;

    public virtual bool CanProcessMnemonic(char keyData)
    {
      return this.itemsControlImpl.CanProcessMnemonic(keyData);
    }

    public virtual bool CanNavigate(Keys keyData)
    {
      return this.itemsControlImpl.CanNavigate(keyData);
    }

    public RadItem GetSelectedItem()
    {
      return this.itemsControlImpl.GetSelectedItem();
    }

    public void SelectItem(RadItem item)
    {
      this.itemsControlImpl.SelectItem(item);
    }

    public RadItem GetNextItem(RadItem item, bool forward)
    {
      return this.itemsControlImpl.GetNextItem(item, forward);
    }

    public RadItem SelectNextItem(RadItem item, bool forward)
    {
      return this.itemsControlImpl.SelectNextItem(item, forward);
    }

    public RadItem GetFirstVisibleItem()
    {
      return this.itemsControlImpl.GetFirstVisibleItem();
    }

    public RadItem GetLastVisibleItem()
    {
      return this.itemsControlImpl.GetLastVisibleItem();
    }

    public RadItem SelectFirstVisibleItem()
    {
      return this.itemsControlImpl.SelectFirstVisibleItem();
    }

    public RadItem SelectLastVisibleItem()
    {
      return this.itemsControlImpl.SelectLastVisibleItem();
    }

    public RadItemOwnerCollection ActiveItems
    {
      get
      {
        return this.itemsControlImpl.ActiveItems;
      }
    }

    public bool RollOverItemSelection
    {
      get
      {
        return this.itemsControlImpl.RollOverItemSelection;
      }
      set
      {
        this.itemsControlImpl.RollOverItemSelection = value;
      }
    }

    public bool ProcessKeyboard
    {
      get
      {
        return this.itemsControlImpl.ProcessKeyboard;
      }
      set
      {
        this.itemsControlImpl.ProcessKeyboard = value;
      }
    }

    private delegate void PerformClickInvoker(RadMenuItemBase item);
  }
}
