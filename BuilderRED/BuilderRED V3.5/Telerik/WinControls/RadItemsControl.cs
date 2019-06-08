// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadItemsControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public abstract class RadItemsControl : RadControl, IItemsControl
  {
    private IItemsControl itemsControlImpl;

    public event ItemSelectedEventHandler ItemSelected;

    public event ItemSelectedEventHandler ItemDeselected;

    public RadItemsControl()
    {
      this.itemsControlImpl = this.GetItemsControlImpl();
      this.itemsControlImpl.ItemSelected += new ItemSelectedEventHandler(this.OnItemsControlImpl_ItemSelected);
      this.itemsControlImpl.ItemDeselected += new ItemSelectedEventHandler(this.OnItemsControlImpl_ItemDeselected);
    }

    protected virtual IItemsControl GetItemsControlImpl()
    {
      return (IItemsControl) new RadItemsControlImpl(this.Items);
    }

    [Description("Gets or sets whether the rollover items functionality of the RadItemsControl will be allowed.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public virtual bool RollOverItemSelection
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

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the RadItemsControl processes the keyboard.")]
    public virtual bool ProcessKeyboard
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

    public virtual bool HasKeyboardInput
    {
      get
      {
        return this.ContainsFocus;
      }
    }

    public abstract RadItemOwnerCollection Items { get; }

    public virtual RadItemOwnerCollection ActiveItems
    {
      get
      {
        return this.itemsControlImpl.ActiveItems;
      }
    }

    public virtual bool CanProcessMnemonic(char keyData)
    {
      return this.itemsControlImpl.CanProcessMnemonic(keyData);
    }

    public virtual bool CanNavigate(Keys keyData)
    {
      return this.itemsControlImpl.CanNavigate(keyData);
    }

    public virtual RadItem GetSelectedItem()
    {
      return this.itemsControlImpl.GetSelectedItem();
    }

    public virtual void SelectItem(RadItem item)
    {
      this.itemsControlImpl.SelectItem(item);
    }

    public virtual RadItem GetNextItem(RadItem item, bool forward)
    {
      return this.itemsControlImpl.GetNextItem(item, forward);
    }

    public virtual RadItem GetFirstVisibleItem()
    {
      return this.itemsControlImpl.GetFirstVisibleItem();
    }

    public virtual RadItem GetLastVisibleItem()
    {
      return this.itemsControlImpl.GetLastVisibleItem();
    }

    public virtual RadItem SelectNextItem(RadItem item, bool forward)
    {
      return this.itemsControlImpl.SelectNextItem(item, forward);
    }

    public virtual RadItem SelectFirstVisibleItem()
    {
      RadItem radItem = this.itemsControlImpl.SelectFirstVisibleItem();
      radItem?.Focus();
      return radItem;
    }

    public virtual RadItem SelectLastVisibleItem()
    {
      RadItem radItem = this.itemsControlImpl.SelectLastVisibleItem();
      radItem?.Focus();
      return radItem;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.GetSelectedItem()?.Focus();
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      bool flag1 = false;
      if (!this.ProcessKeyboard)
        return this.CallBaseProcessDialogKey(keyData);
      RadItem selectedItem = this.GetSelectedItem();
      if (this.ContainsFocus && this.Focused && (selectedItem != null && selectedItem.Enabled) && selectedItem.ProcessDialogKey(keyData))
        return true;
      bool flag2 = (keyData & (Keys.Control | Keys.Alt)) != Keys.None;
      Keys keyCode = keyData & Keys.KeyCode;
      switch (keyCode)
      {
        case Keys.Back:
          if (!this.ContainsFocus)
          {
            flag1 = this.ProcessTabKey(false);
            break;
          }
          break;
        case Keys.Tab:
          if (!flag2)
          {
            flag1 = this.ProcessTabKey((keyData & Keys.Shift) == Keys.None);
            break;
          }
          break;
        case Keys.End:
          if (this.Focused)
          {
            this.SelectLastVisibleItem();
            flag1 = true;
            break;
          }
          break;
        case Keys.Home:
          if (this.Focused)
          {
            this.SelectFirstVisibleItem();
            flag1 = true;
            break;
          }
          break;
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          flag1 = this.ProcessArrowKey(keyCode);
          break;
      }
      if (flag1)
        return flag1;
      return this.CallBaseProcessDialogKey(keyData);
    }

    protected virtual bool CallBaseProcessDialogKey(Keys keyData)
    {
      return base.ProcessDialogKey(keyData);
    }

    protected virtual bool OnHandleKeyDown(Message m)
    {
      return false;
    }

    protected override bool ProcessCmdKey(ref Message m, Keys keyData)
    {
      RadItem selectedItem = this.GetSelectedItem();
      if (selectedItem != null && selectedItem.ProcessCmdKey(ref m, keyData))
        return true;
      foreach (RadItem activeItem in (RadItemCollection) this.ActiveItems)
      {
        if (activeItem != selectedItem && activeItem.ProcessCmdKey(ref m, keyData))
          return true;
      }
      return base.ProcessCmdKey(ref m, keyData);
    }

    protected virtual bool ProcessArrowKey(Keys keyCode)
    {
      bool flag = false;
      switch (keyCode)
      {
        case Keys.Left:
        case Keys.Right:
          return this.ProcessLeftRightArrowKey(keyCode == Keys.Right);
        case Keys.Up:
        case Keys.Down:
          return this.ProcessUpDownArrowKey(keyCode == Keys.Down);
        default:
          return flag;
      }
    }

    protected virtual bool ProcessLeftRightArrowKey(bool right)
    {
      this.SelectNextItem(this.GetSelectedItem(), right);
      return true;
    }

    protected virtual bool ProcessUpDownArrowKey(bool down)
    {
      this.SelectNextItem(this.GetSelectedItem(), down);
      return true;
    }

    protected virtual bool ProcessTabKey(bool forward)
    {
      return false;
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

    private void OnItemsControlImpl_ItemSelected(object sender, ItemSelectedEventArgs args)
    {
      if (this.ItemSelected != null)
        this.ItemSelected(sender, args);
      this.CallOnItemSelected(args);
    }

    internal void CallOnItemSelected(ItemSelectedEventArgs args)
    {
      this.OnItemSelected(args);
    }

    protected virtual void OnItemSelected(ItemSelectedEventArgs args)
    {
    }

    protected override RadElement GetInputElement()
    {
      return (RadElement) this.GetSelectedItem();
    }

    protected override void Select(bool directed, bool forward)
    {
      if (!this.ProcessKeyboard)
      {
        base.Select(directed, forward);
      }
      else
      {
        bool flag = true;
        if (this.Parent != null)
        {
          IContainerControl containerControl = this.Parent.GetContainerControl();
          if (containerControl != null)
          {
            containerControl.ActiveControl = (Control) this;
            flag = containerControl.ActiveControl == this;
          }
        }
        if (!directed || !flag)
          return;
        this.SelectNextToolStripItem((RadItem) null, forward);
      }
    }

    protected virtual void GetChildMnemonicList(ArrayList mnemonicList)
    {
      foreach (RadItem activeItem in (RadItemCollection) this.ActiveItems)
      {
        char mnemonic = WindowsFormsUtils.GetMnemonic(activeItem.Text, true);
        if (mnemonic != char.MinValue)
          mnemonicList.Add((object) mnemonic);
      }
    }

    protected virtual void ChangeSelection(RadItem nextItem)
    {
      if (nextItem == null)
        return;
      RadHostItem radHostItem = nextItem as RadHostItem;
      if (this.ContainsFocus && !this.Focused)
        this.Focus();
      if (radHostItem == null)
        return;
      radHostItem.HostedControl.Select();
      radHostItem.HostedControl.Focus();
    }

    private RadItem SelectNextToolStripItem(RadItem start, bool forward)
    {
      RadItem nextItem = this.GetNextItem(start, forward);
      this.ChangeSelection(nextItem);
      return nextItem;
    }
  }
}
