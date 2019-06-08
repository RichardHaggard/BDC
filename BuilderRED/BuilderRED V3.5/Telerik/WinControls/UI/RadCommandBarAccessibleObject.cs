// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadCommandBarAccessibleObject : Control.ControlAccessibleObject
  {
    private RadCommandBar owner;
    private Dictionary<RadCommandBarBaseItem, RadCommandBarItemAccessibleObject> cachedObjects;

    public RadCommandBarAccessibleObject(RadCommandBar owner)
      : base((Control) owner)
    {
      this.owner = owner;
      this.cachedObjects = new Dictionary<RadCommandBarBaseItem, RadCommandBarItemAccessibleObject>();
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      int num = 0;
      foreach (CommandBarRowElement row in this.owner.Rows)
      {
        foreach (CommandBarStripElement strip in row.Strips)
        {
          foreach (RadCommandBarBaseItem commandBarBaseItem in strip.Items)
          {
            if (index == num)
            {
              string name = commandBarBaseItem.Name;
              if (string.IsNullOrEmpty(name))
                name = "item " + (strip.Items.IndexOf(commandBarBaseItem) + 1).ToString();
              return this.GetAccessibleObject(commandBarBaseItem, name);
            }
            ++num;
          }
        }
      }
      return (AccessibleObject) null;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      int num = 0;
      foreach (CommandBarRowElement row in this.owner.Rows)
      {
        foreach (CommandBarStripElement strip in row.Strips)
          num += strip.Items.Count;
      }
      return num;
    }

    public AccessibleObject GetAccessibleObject(
      RadCommandBarBaseItem item,
      string name)
    {
      if (!this.cachedObjects.ContainsKey(item))
        this.cachedObjects.Add(item, new RadCommandBarItemAccessibleObject(item, this, name));
      return (AccessibleObject) this.cachedObjects[item];
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.ToolBar;
      }
    }
  }
}
