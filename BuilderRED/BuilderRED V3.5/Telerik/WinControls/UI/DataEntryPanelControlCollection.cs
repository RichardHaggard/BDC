// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataEntryPanelControlCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataEntryPanelControlCollection : RadScrollablePanelControlCollection
  {
    private RadDataEntry owner;

    public DataEntryPanelControlCollection(RadDataEntry owner)
      : base((RadScrollablePanel) owner)
    {
      this.owner = owner;
    }

    public override int GetChildIndex(Control value, bool throwException)
    {
      if (object.ReferenceEquals((object) value, (object) this.owner.ValidationPanel))
        return base.GetChildIndex(value, false);
      return base.GetChildIndex(value, throwException);
    }
  }
}
