// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemBaseAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemBaseAccessibleObject : AccessibleObject
  {
    private RadPropertyGridAccessibilityInstance parent;

    public PropertyGridItemBaseAccessibleObject(RadPropertyGridAccessibilityInstance parent)
    {
      this.parent = parent;
    }

    public override AccessibleObject Parent
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return (AccessibleObject) this.parent;
      }
    }

    public RadPropertyGrid Control
    {
      get
      {
        return this.parent.Control;
      }
    }

    public virtual T GetElement<T>(PropertyGridItemBase item) where T : PropertyGridItemElementBase
    {
      foreach (PropertyGridItemElementBase child in this.Control.PropertyGridElement.PropertyTableElement.FindDescendant<VirtualizedStackContainer<PropertyGridItemBase>>().Children)
      {
        if (child is T && ((T) child).Data == item)
          return (T) child;
      }
      return default (T);
    }
  }
}
