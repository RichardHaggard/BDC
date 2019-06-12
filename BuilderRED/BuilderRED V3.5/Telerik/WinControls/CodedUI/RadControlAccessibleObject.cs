// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CodedUI.RadControlAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.CodedUI
{
  public abstract class RadControlAccessibleObject : Control.ControlAccessibleObject
  {
    private string name;

    public RadControlAccessibleObject(Control ownerControl, string name)
      : base(ownerControl)
    {
      this.name = name;
    }

    public abstract object OwnerElement { get; }

    public override string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }
  }
}
