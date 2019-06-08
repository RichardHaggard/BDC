// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListControlAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadListControlAccessibleObject : Control.ControlAccessibleObject
  {
    private Dictionary<RadListDataItem, RadListDataItemAccessibleObject> cachedObjects;

    public RadListControlAccessibleObject(RadListControl owner)
      : base((Control) owner)
    {
      this.cachedObjects = new Dictionary<RadListDataItem, RadListDataItemAccessibleObject>();
      this.List.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.Control_SelectedIndexChanged);
    }

    private void Control_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.NotifyClients(AccessibleEvents.Focus, e.Position);
    }

    public RadListControl List
    {
      get
      {
        return this.Owner as RadListControl;
      }
    }

    public override string Name
    {
      get
      {
        return this.Owner.Name;
      }
      set
      {
        base.Name = value;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.List;
      }
    }

    public override string Description
    {
      get
      {
        return "RadListControl ;" + this.Name + ";" + (object) this.GetChildCount() + ";" + (object) this.List.SelectedIndex;
      }
    }

    public override AccessibleObject GetChild(int index)
    {
      if (index == this.List.Items.Count)
        return (AccessibleObject) new RadScrollBarElementAccessibleObject(this.List.ListElement.VScrollBar);
      if (index == this.List.Items.Count + 1)
        return (AccessibleObject) new RadScrollBarElementAccessibleObject(this.List.ListElement.HScrollBar);
      return (AccessibleObject) this.GetItemAccessibleObject(this.List.Items[index]);
    }

    public override AccessibleObject HitTest(int x, int y)
    {
      Point client = this.List.PointToClient(new Point(x, y));
      RadListVisualItem elementAtPoint1 = this.List.ElementTree.GetElementAtPoint(client) as RadListVisualItem;
      if (elementAtPoint1 != null)
        return (AccessibleObject) this.GetItemAccessibleObject(elementAtPoint1.Data);
      RadScrollBarElement elementAtPoint2 = this.List.ElementTree.GetElementAtPoint(client) as RadScrollBarElement;
      if (elementAtPoint2 != null)
        return (AccessibleObject) new RadScrollBarElementAccessibleObject(elementAtPoint2);
      return (AccessibleObject) null;
    }

    public override int GetChildCount()
    {
      if (this.List.IsDisposed)
        return 0;
      int num = 0;
      if (this.List.ListElement.VScrollBar.Visibility == ElementVisibility.Visible)
        ++num;
      if (this.List.ListElement.HScrollBar.Visibility == ElementVisibility.Visible)
        ++num;
      return this.List.Items.Count + num;
    }

    public override AccessibleObject Navigate(AccessibleNavigation direction)
    {
      return (AccessibleObject) null;
    }

    public RadListDataItemAccessibleObject GetItemAccessibleObject(
      RadListDataItem item)
    {
      if (!this.cachedObjects.ContainsKey(item))
        this.cachedObjects.Add(item, new RadListDataItemAccessibleObject(item, (AccessibleObject) this));
      return this.cachedObjects[item];
    }
  }
}
