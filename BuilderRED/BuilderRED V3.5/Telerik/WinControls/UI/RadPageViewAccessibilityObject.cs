// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewAccessibilityObject
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
  public class RadPageViewAccessibilityObject : Control.ControlAccessibleObject
  {
    private RadPageView ownerPageView;

    public RadPageViewAccessibilityObject(RadPageView owner)
      : base((Control) owner)
    {
      this.ownerPageView = owner;
      this.ownerPageView.SelectedPageChanged += new EventHandler(this.owner_SelectedPageChanged);
    }

    public RadPageView OwnerPageView
    {
      get
      {
        return this.ownerPageView;
      }
    }

    private void owner_SelectedPageChanged(object sender, EventArgs e)
    {
      this.NotifyClients(AccessibleEvents.Focus);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return this.ownerPageView.Pages.Count;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      return (AccessibleObject) new RadPageViewPageAccessibilityObject(this.ownerPageView.Pages[index], this, this.ownerPageView.Pages[index].Text);
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.PageTabList;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        if (!this.ownerPageView.EnableCodedUITests)
          return this.Description + this.ownerPageView.Text + " " + (this.ownerPageView.SelectedPage != null ? this.ownerPageView.SelectedPage.Text : "");
        return this.ownerPageView.Text;
      }
      set
      {
        this.ownerPageView.Name = value;
      }
    }
  }
}
