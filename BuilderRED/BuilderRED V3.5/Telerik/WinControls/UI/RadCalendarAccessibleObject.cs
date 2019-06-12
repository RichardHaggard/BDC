// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalendarAccessibleObject
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
  public class RadCalendarAccessibleObject : Control.ControlAccessibleObject
  {
    private RadCalendar owner;

    public RadCalendarAccessibleObject(RadCalendar owner)
      : base((Control) owner)
    {
      this.owner = owner;
      this.owner.SelectionChanged += new EventHandler(this.owner_SelectionChanged);
    }

    public RadCalendar Owner
    {
      get
      {
        return this.owner;
      }
    }

    private void owner_SelectionChanged(object sender, EventArgs e)
    {
      this.NotifyClients(AccessibleEvents.Focus, 0);
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.Table;
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return this.owner.CalendarElement.View.Rows * this.owner.CalendarElement.View.Columns;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      return (AccessibleObject) new RadCalendarCellAccessibleObject(this, index / this.owner.CalendarElement.View.Columns, index % this.owner.CalendarElement.View.Columns);
    }
  }
}
