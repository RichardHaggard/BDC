// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownListAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadDropDownListAccessibleObject : Control.ControlAccessibleObject
  {
    private Dictionary<RadListDataItem, RadListDataItemAccessibleObject> cachedObjects;
    private bool isXp;

    public RadDropDownListAccessibleObject(RadDropDownList owner)
      : base((Control) owner)
    {
      this.cachedObjects = new Dictionary<RadListDataItem, RadListDataItemAccessibleObject>();
      if (owner != null)
        owner.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.owner_SelectedIndexChanged);
      this.isXp = Environment.OSVersion.Version.Major <= 5;
    }

    public override AccessibleRole Role
    {
      get
      {
        if (this.Owner.AccessibleRole != AccessibleRole.Default)
          return this.Owner.AccessibleRole;
        return AccessibleRole.ComboBox;
      }
    }

    protected RadDropDownList List
    {
      get
      {
        return this.Owner as RadDropDownList;
      }
    }

    public override string Name
    {
      get
      {
        return this.List.Name;
      }
      set
      {
        this.List.Name = value;
      }
    }

    public override string Value
    {
      get
      {
        if (this.List.DropDownStyle != RadDropDownStyle.DropDownList)
          return "";
        return this.List.Text;
      }
      set
      {
        this.List.Text = value;
      }
    }

    public override AccessibleObject GetChild(int index)
    {
      if (index == 0 && this.List.DropDownStyle == RadDropDownStyle.DropDown)
        return this.List.DropDownListElement.TextBox.TextBoxItem.HostedControl.AccessibilityObject;
      return this.List.DropDownListElement.Popup.AccessibilityObject;
    }

    public override int GetChildCount()
    {
      return this.List.DropDownStyle != RadDropDownStyle.DropDown ? 1 : 2;
    }

    private void owner_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      bool flag = false;
      if (this.List.DropDownStyle == RadDropDownStyle.DropDown)
      {
        this.NotifyClients(AccessibleEvents.ValueChange, 0);
        flag = true;
      }
      if (this.isXp || ((RadDropDownList) this.Owner).DropDownStyle == RadDropDownStyle.DropDownList)
      {
        this.NotifyClients(AccessibleEvents.ValueChange);
        flag = true;
      }
      if (flag)
        return;
      this.NotifyClients(AccessibleEvents.ValueChange);
    }

    public RadListDataItemAccessibleObject GetItemAccessibleObject(
      RadListDataItem item)
    {
      if (item == null)
        return (RadListDataItemAccessibleObject) null;
      if (!this.cachedObjects.ContainsKey(item))
        this.cachedObjects.Add(item, new RadListDataItemAccessibleObject(item, (AccessibleObject) this));
      return this.cachedObjects[item];
    }
  }
}
