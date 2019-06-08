// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadItemsControlImpl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadItemsControlImpl : IItemsControl
  {
    private bool rolloverItemSelection;
    private bool processKeyboard;
    private RadItem selectedItem;
    private RadItemOwnerCollection ownerCollection;

    public event ItemSelectedEventHandler ItemSelected;

    public event ItemSelectedEventHandler ItemDeselected;

    public RadItemsControlImpl(RadItemOwnerCollection items)
    {
      this.ownerCollection = items;
    }

    public bool CanProcessMnemonic(char keyData)
    {
      return false;
    }

    public bool CanNavigate(Keys keyData)
    {
      return false;
    }

    public RadItem GetSelectedItem()
    {
      return this.selectedItem;
    }

    public void SelectItem(RadItem item)
    {
      if (this.selectedItem != item && this.selectedItem != null && this.ItemDeselected != null)
        this.ItemDeselected((object) this, new ItemSelectedEventArgs(this.selectedItem));
      this.selectedItem = item;
      if (this.ItemSelected == null || item == null)
        return;
      this.ItemSelected((object) this, new ItemSelectedEventArgs(item));
    }

    public RadItem GetNextItem(RadItem item, bool forward)
    {
      if (this.rolloverItemSelection)
      {
        if (forward)
        {
          RadItem lastVisibleItem = this.GetLastVisibleItem();
          if (item == lastVisibleItem)
            return this.GetFirstVisibleItem();
        }
        else
        {
          RadItem firstVisibleItem = this.GetFirstVisibleItem();
          if (item == firstVisibleItem)
            return this.GetLastVisibleItem();
        }
      }
      RadItem radItem = (RadItem) null;
      bool flag = false;
      foreach (RadItem activeItem in (RadItemCollection) this.ActiveItems)
      {
        if (item == null && activeItem.Selectable && activeItem.Visibility == ElementVisibility.Visible)
          return activeItem;
        if (activeItem == item)
        {
          if (!forward)
            return radItem;
          flag = true;
        }
        else
        {
          if (activeItem.Selectable && activeItem.Visibility == ElementVisibility.Visible)
            radItem = activeItem;
          else if (flag)
            radItem = (RadItem) null;
          if (flag && radItem != null)
            return radItem;
        }
      }
      return (RadItem) null;
    }

    public RadItem SelectNextItem(RadItem item, bool forward)
    {
      RadItem nextItem = this.GetNextItem(item, forward);
      if (nextItem == null)
        return (RadItem) null;
      this.SelectItem(nextItem);
      return nextItem;
    }

    public RadItem GetFirstVisibleItem()
    {
      foreach (RadItem activeItem in (RadItemCollection) this.ActiveItems)
      {
        if (activeItem.Selectable && activeItem.Visibility == ElementVisibility.Visible)
          return activeItem;
      }
      return (RadItem) null;
    }

    public RadItem GetLastVisibleItem()
    {
      for (int index = this.ActiveItems.Count - 1; index >= 0; --index)
      {
        if (this.ActiveItems[index].Selectable && this.ActiveItems[index].Visibility == ElementVisibility.Visible)
          return this.ActiveItems[index];
      }
      return (RadItem) null;
    }

    public RadItem SelectFirstVisibleItem()
    {
      RadItem firstVisibleItem = this.GetFirstVisibleItem();
      if (firstVisibleItem == null)
        return (RadItem) null;
      this.SelectItem(firstVisibleItem);
      return firstVisibleItem;
    }

    public RadItem SelectLastVisibleItem()
    {
      RadItem lastVisibleItem = this.GetLastVisibleItem();
      if (lastVisibleItem == null)
        return (RadItem) null;
      this.SelectItem(lastVisibleItem);
      return lastVisibleItem;
    }

    public RadItemOwnerCollection ActiveItems
    {
      get
      {
        return this.Items;
      }
    }

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.ownerCollection;
      }
    }

    public bool RollOverItemSelection
    {
      get
      {
        return this.rolloverItemSelection;
      }
      set
      {
        this.rolloverItemSelection = value;
      }
    }

    public bool ProcessKeyboard
    {
      get
      {
        return this.processKeyboard;
      }
      set
      {
        this.processKeyboard = value;
      }
    }
  }
}
