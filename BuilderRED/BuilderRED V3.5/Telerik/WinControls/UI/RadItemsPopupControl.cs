// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadItemsPopupControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public abstract class RadItemsPopupControl : RadPopupControlBase, IItemsControl
  {
    internal IntPtr activeHwnd = IntPtr.Zero;
    private Size minimum = Size.Empty;
    private Size maximum = Size.Empty;
    private RadItemsControlImpl itemsControlImpl;
    private RadItemOwnerCollection items;

    public event ItemSelectedEventHandler ItemSelected;

    public event ItemSelectedEventHandler ItemDeselected;

    public RadItemsPopupControl(RadElement owner)
      : base(owner)
    {
      this.items = new RadItemOwnerCollection();
      this.itemsControlImpl = new RadItemsControlImpl(this.items);
      this.itemsControlImpl.ItemSelected += new ItemSelectedEventHandler(this.OnItemsControlImpl_ItemSelected);
      this.itemsControlImpl.ItemDeselected += new ItemSelectedEventHandler(this.OnItemsControlImpl_ItemDeselected);
      this.itemsControlImpl.RollOverItemSelection = true;
      this.PopupOpening += new RadPopupOpeningEventHandler(this.RadItemsPopupControl_PopupOpening);
      this.PopupClosing += new RadPopupClosingEventHandler(this.RadItemsPopupControl_PopupClosing);
      this.PopupOpened += new RadPopupOpenedEventHandler(this.RadItemsPopupControl_PopupOpened);
      this.PopupClosed += new RadPopupClosedEventHandler(this.RadItemsPopupControl_PopupClosed);
    }

    private void OnItemsControlImpl_ItemDeselected(object sender, ItemSelectedEventArgs args)
    {
      if (this.ItemDeselected != null)
        this.ItemDeselected(sender, args);
      this.CallOnItemDeselected(args);
    }

    public void CallOnItemDeselected(ItemSelectedEventArgs args)
    {
      this.OnItemDeselected(args);
    }

    protected virtual void OnItemDeselected(ItemSelectedEventArgs args)
    {
    }

    private void OnItemsControlImpl_ItemSelected(object sender, ItemSelectedEventArgs e)
    {
      if (this.ItemSelected != null)
        this.ItemSelected(sender, e);
      this.CallOnItemSelected(e);
    }

    internal void CallOnItemSelected(ItemSelectedEventArgs args)
    {
      this.OnItemSelected(args);
    }

    protected virtual void OnItemSelected(ItemSelectedEventArgs args)
    {
    }

    private void RadItemsPopupControl_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      this.OnDropDownClosed(args);
    }

    private void RadItemsPopupControl_PopupOpened(object sender, EventArgs args)
    {
      this.OnDropDownOpened();
    }

    private void RadItemsPopupControl_PopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnDropDownClosing(args);
    }

    private void RadItemsPopupControl_PopupOpening(object sender, CancelEventArgs args)
    {
      this.OnDropDownOpening(args);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.activeHwnd = IntPtr.Zero;
      base.Dispose(disposing);
    }

    public event CancelEventHandler DropDownOpening;

    public event RadPopupClosingEventHandler DropDownClosing;

    public event EventHandler DropDownOpened;

    public event RadPopupClosedEventHandler DropDownClosed;

    public bool IsVisible
    {
      get
      {
        return PopupManager.Default.ContainsPopup((IPopupControl) this);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Browsable(true)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected virtual void OnDropDownOpening(CancelEventArgs args)
    {
      if (this.DropDownOpening == null)
        return;
      this.DropDownOpening((object) this, args);
    }

    protected virtual void OnDropDownClosing(RadPopupClosingEventArgs args)
    {
      if (this.DropDownClosing == null)
        return;
      this.DropDownClosing((object) this, args);
    }

    protected virtual void OnDropDownOpened()
    {
      if (this.DropDownOpened == null)
        return;
      this.DropDownOpened((object) this, EventArgs.Empty);
    }

    protected virtual void OnDropDownClosed(RadPopupClosedEventArgs args)
    {
      if (this.DropDownClosed == null)
        return;
      this.DropDownClosed((object) this, args);
    }

    public Size Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        this.minimum = value;
      }
    }

    public Size Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        this.maximum = value;
      }
    }

    protected void AutoUpdateBounds()
    {
      this.minimum = new Size(this.Width, 10);
      this.maximum = this.Size;
    }

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
  }
}
