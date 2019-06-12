// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPropertyGridAccessibilityInstance
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadPropertyGridAccessibilityInstance : System.Windows.Forms.Control.ControlAccessibleObject
  {
    private RadPropertyGrid control;

    public RadPropertyGridAccessibilityInstance(RadPropertyGrid control)
      : base((System.Windows.Forms.Control) control)
    {
      this.control = control;
    }

    public RadPropertyGrid Control
    {
      get
      {
        return this.control;
      }
      set
      {
        this.control = value;
      }
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.control.Items.Count.ToString() + " Rows, 2 Columns.";
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      if (this.control.IsDisposed)
        return 0;
      PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this.control.PropertyGridElement.PropertyTableElement);
      int num = 0;
      while (propertyGridTraverser.MoveNext())
        ++num;
      propertyGridTraverser.Reset();
      return num;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      PropertyGridTraverser propertyGridTraverser = new PropertyGridTraverser(this.control.PropertyGridElement.PropertyTableElement);
      int num = 0;
      do
        ;
      while (propertyGridTraverser.MoveNext() && num++ < index);
      PropertyGridItemBase current = propertyGridTraverser.Current;
      if (current is PropertyGridGroupItem)
        return (AccessibleObject) new PropertyGridGroupAccessibleObject((PropertyGridGroupItem) current, this);
      return (AccessibleObject) new PropertyGridItemAccessibleObject((PropertyGridItem) current, this);
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.Table;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return "RadPropertyGrid with " + (this.control.Items.Count + 1).ToString() + " Rows, 2 Columns.";
      }
      set
      {
        this.Control.Name = value;
      }
    }
  }
}
