// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadItemElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadItemElementAccessibleObject : RadControlAccessibleObject
  {
    private AccessibleRole role = AccessibleRole.Default;
    private RadItem owner;

    public RadItemElementAccessibleObject(RadItem owner, string name)
      : base(owner.ElementTree.Control, name)
    {
      this.owner = owner;
    }

    public RadItemElementAccessibleObject(RadItem owner, AccessibleRole role, string name)
      : this(owner, name)
    {
      this.SetRole(role);
    }

    public override AccessibleStates State
    {
      get
      {
        AccessibleStates state = base.State;
        if (this.owner.IsMouseDown)
          state |= AccessibleStates.Pressed;
        return state;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return this.role;
      }
    }

    public void SetRole(AccessibleRole role)
    {
      this.role = role;
    }

    public override string Description
    {
      get
      {
        return this.owner.Text;
      }
    }

    public override string Name
    {
      get
      {
        return this.owner.Name;
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
  }
}
