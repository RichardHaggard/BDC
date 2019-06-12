// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadRibbonBarAccessibleObject : Control.ControlAccessibleObject
  {
    private RadRibbonBar ribbonBar;
    protected internal RadItem selected;

    public RadRibbonBarAccessibleObject(RadRibbonBar owner)
      : base((Control) owner)
    {
      this.ribbonBar = owner;
    }

    public RadRibbonBar RibbonBar
    {
      get
      {
        return this.ribbonBar;
      }
    }

    public override AccessibleObject GetSelected()
    {
      RadItem ribbonSelectedItem = this.GetRibbonSelectedItem();
      if (ribbonSelectedItem == null)
        return (AccessibleObject) null;
      if (ribbonSelectedItem == this.ribbonBar.RibbonBarElement.ApplicationButtonElement)
        return (AccessibleObject) new RadApplicationMenuButtonElementAccessibleObject(this.ribbonBar.RibbonBarElement.ApplicationButtonElement, "RibbonBarElement.ApplicationButtonElement");
      return (AccessibleObject) new RadRibbonBarItemAccessibleObject(this.selected, this, ribbonSelectedItem.Name);
    }

    public override AccessibleStates State
    {
      get
      {
        AccessibleStates state = base.State;
        if (((RadRibbonBar) this.Owner).RibbonBarElement.IsMouseDown)
          state |= AccessibleStates.Pressed;
        return state;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        if (this.ribbonBar.EnableCodedUITests)
          return AccessibleRole.MenuBar;
        switch (this.GetRibbonSelectedItem())
        {
          case RibbonTab _:
            return AccessibleRole.PageTab;
          case RadCheckBoxElement _:
            return AccessibleRole.CheckButton;
          case RadRadioButtonElement _:
            return AccessibleRole.RadioButton;
          case RadDropDownListElement _:
            return AccessibleRole.ComboBox;
          case RadButtonElement _:
            return AccessibleRole.PushButton;
          default:
            return AccessibleRole.MenuBar;
        }
      }
    }

    public override string Description
    {
      get
      {
        if (this.RibbonBar.EnableCodedUITests)
          return base.Description;
        RadItem ribbonSelectedItem = this.GetRibbonSelectedItem();
        if (ribbonSelectedItem == null)
          return this.ribbonBar.Name;
        if (ribbonSelectedItem == this.ribbonBar.RibbonBarElement.ApplicationButtonElement)
          return "Application Menu";
        return ribbonSelectedItem.Text;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Description;
      }
      set
      {
        this.Owner.Name = value;
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void DoDefaultAction()
    {
      this.ribbonBar.RibbonBarElement.ApplicationButtonElement.CallDoClick(EventArgs.Empty);
    }

    public override int GetChildCount()
    {
      int num = this.ribbonBar.QuickAccessToolBarItems.Count + 2;
      for (int index = 0; index < this.ribbonBar.CommandTabs.Count; ++index)
        num += this.GetRibbonTabChildrenCount(this.ribbonBar.CommandTabs[index] as RibbonTab);
      return num + this.ribbonBar.CommandTabs.Count;
    }

    private int GetRibbonTabChildrenCount(RibbonTab ribbonTab)
    {
      int num = 0;
      for (int index = 0; index < ribbonTab.Items.Count; ++index)
        num += this.GetRibbonBarGroupChildrenCount(ribbonTab.Items[index] as RadRibbonBarGroup);
      return num;
    }

    private int GetRibbonBarGroupChildrenCount(RadRibbonBarGroup group)
    {
      int num = 0;
      for (int index = 0; index < group.Items.Count; ++index)
        num += this.GetRibbonBarGroupChildrenCount(group.Items[index]);
      return num;
    }

    private int GetRibbonBarGroupChildrenCount(RadItem item)
    {
      RadRibbonBarButtonGroup ribbonBarButtonGroup = item as RadRibbonBarButtonGroup;
      if (ribbonBarButtonGroup == null)
        return 1;
      int num = 0;
      for (int index = 0; index < ribbonBarButtonGroup.Items.Count; ++index)
        num += this.GetRibbonBarGroupChildrenCount(ribbonBarButtonGroup.Items[index]);
      return num;
    }

    public override AccessibleObject GetChild(int index)
    {
      if (index == 0)
        return (AccessibleObject) new RadApplicationMenuButtonElementAccessibleObject(this.ribbonBar.RibbonBarElement.ApplicationButtonElement, "RibbonBarElement.ApplicationButtonElement");
      --index;
      AccessibleObject barAccesibleObject = this.GetQuickAccessBarAccesibleObject(index);
      if (barAccesibleObject != null)
        return barAccesibleObject;
      index = index - this.ribbonBar.QuickAccessToolBarItems.Count - 1;
      for (int index1 = 0; index1 < this.ribbonBar.CommandTabs.Count; ++index1)
      {
        if (index == index1)
          return (AccessibleObject) new RadRibbonBarItemAccessibleObject(this.ribbonBar.CommandTabs[index1], this, index1.ToString());
      }
      index -= this.ribbonBar.CommandTabs.Count;
      int count = 0;
      for (int index1 = 0; index1 < this.ribbonBar.CommandTabs.Count; ++index1)
      {
        RibbonTab commandTab = this.ribbonBar.CommandTabs[index1] as RibbonTab;
        for (int index2 = 0; index2 < commandTab.Items.Count; ++index2)
        {
          RadRibbonBarGroup radRibbonBarGroup = commandTab.Items[index2] as RadRibbonBarGroup;
          for (int index3 = 0; index3 < radRibbonBarGroup.Items.Count; ++index3)
          {
            RadItem barGroupChildren = this.GetRibbonBarGroupChildren(radRibbonBarGroup.Items[index3], ref count, index);
            if (barGroupChildren != null)
            {
              if (barGroupChildren.IsMouseOver)
                this.selected = barGroupChildren;
              return (AccessibleObject) new RadRibbonBarItemAccessibleObject(barGroupChildren, this, barGroupChildren.Name);
            }
          }
        }
      }
      return (AccessibleObject) null;
    }

    private AccessibleObject GetQuickAccessBarAccesibleObject(int index)
    {
      if (index < this.ribbonBar.QuickAccessToolBarItems.Count)
      {
        RadItem accessToolBarItem = this.ribbonBar.QuickAccessToolBarItems[index];
        string name = accessToolBarItem.Name;
        if (string.IsNullOrEmpty(name))
          name = "QuickAccessToolBarItems" + (object) index;
        if (accessToolBarItem is RadDropDownButtonElement)
          return (AccessibleObject) new RadDropDownButtonElementAccessibleObject(accessToolBarItem as RadDropDownButtonElement, name);
        if (accessToolBarItem is RadButtonElement)
          return (AccessibleObject) new RadRibbonBarItemAccessibleObject(accessToolBarItem, this, accessToolBarItem.Name);
        return new AccessibleObject();
      }
      if (index != this.ribbonBar.QuickAccessToolBarItems.Count)
        return (AccessibleObject) null;
      RadQuickAccessOverflowButton overflowButtonElement = this.ribbonBar.RibbonBarElement.QuickAccessToolBar.OverflowButtonElement;
      string name1 = overflowButtonElement.Name;
      if (string.IsNullOrEmpty(name1))
        name1 = "overFlowButton" + (object) index;
      return (AccessibleObject) new RadDropDownButtonElementAccessibleObject((RadDropDownButtonElement) overflowButtonElement, name1);
    }

    private RadItem GetRibbonBarGroupChildren(RadItem item, ref int count, int index)
    {
      RadRibbonBarButtonGroup ribbonBarButtonGroup = item as RadRibbonBarButtonGroup;
      if (ribbonBarButtonGroup != null)
      {
        for (int index1 = 0; index1 < ribbonBarButtonGroup.Items.Count; ++index1)
        {
          RadItem barGroupChildren = this.GetRibbonBarGroupChildren(ribbonBarButtonGroup.Items[index1], ref count, index);
          if (barGroupChildren != null)
            return barGroupChildren;
        }
        return (RadItem) null;
      }
      if (count == index)
        return item;
      ++count;
      return (RadItem) null;
    }

    public RadItem GetRibbonSelectedItem()
    {
      if (this.ribbonBar.RibbonBarElement.ApplicationButtonElement.IsMouseOver)
        return (RadItem) this.ribbonBar.RibbonBarElement.ApplicationButtonElement;
      foreach (RadItem accessToolBarItem in (RadItemCollection) this.ribbonBar.QuickAccessToolBarItems)
      {
        if (accessToolBarItem.IsMouseOver)
          return accessToolBarItem;
      }
      foreach (RibbonTab commandTab in (RadItemCollection) this.ribbonBar.CommandTabs)
      {
        if (commandTab.IsMouseOver)
          return (RadItem) commandTab;
        foreach (RadRibbonBarGroup radRibbonBarGroup in (RadItemCollection) commandTab.Items)
        {
          foreach (RadItem radItem in (RadItemCollection) radRibbonBarGroup.Items)
          {
            RadItem groupSelectedItem = this.GetButtonGroupSelectedItem(radItem);
            if (groupSelectedItem != null)
              return groupSelectedItem;
          }
        }
      }
      return (RadItem) null;
    }

    public RadItem GetButtonGroupSelectedItem(RadItem item)
    {
      RadRibbonBarButtonGroup ribbonBarButtonGroup = item as RadRibbonBarButtonGroup;
      if (ribbonBarButtonGroup != null)
      {
        foreach (RadItem radItem in (RadItemCollection) ribbonBarButtonGroup.Items)
        {
          RadItem groupSelectedItem = this.GetButtonGroupSelectedItem(radItem);
          if (groupSelectedItem != null)
            return groupSelectedItem;
        }
      }
      else if (item.IsMouseOver)
        return item;
      return (RadItem) null;
    }
  }
}
