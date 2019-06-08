// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadStatusStripAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadStatusStripAccessibleObject : RadControlAccessibleObject
  {
    private RadStatusStrip owner;

    public RadStatusStripAccessibleObject(RadStatusStrip owner, string name)
      : base((Control) owner, name)
    {
      this.owner = owner;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      RadElement radElement = (RadElement) this.owner.Items[index];
      if (radElement is RadCheckBoxElement)
        return (AccessibleObject) new RadCheckBoxElementAccessibleObject((RadCheckBoxElement) radElement, "RadCheckBoxElement at possition " + (object) index);
      if (radElement is RadButtonElement)
        return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) radElement, radElement.Name);
      if (radElement is RadTextBoxElement)
        return (AccessibleObject) new RadTextBoxElementAccessibleObject((RadTextBoxItem) radElement);
      if (radElement is RadLabelElement)
        return (AccessibleObject) new RadLabelElementAccessibleObject((RadLabelElement) radElement, radElement.Name);
      if (radElement is RadDropDownButtonElement)
        return (AccessibleObject) new RadDropDownButtonElementAccessibleObject((RadDropDownButtonElement) radElement, radElement.Name);
      return (AccessibleObject) new RadStatusStripItemAccessibleObject(this.owner.Items[index], this, this.owner.Items[index].Name);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return this.owner.Items.Count;
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.StatusBar;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Description + this.owner.Text;
      }
      set
      {
        this.owner.Name = value;
      }
    }

    public override object OwnerElement
    {
      get
      {
        return (object) this.owner;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.owner.ElementTree.Control.PointToScreen(this.owner.StatusBarElement.ControlBoundingRectangle.Location), this.owner.Size);
      }
    }
  }
}
