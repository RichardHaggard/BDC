// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelControlsContainerControlsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CollapsiblePanelControlsContainerControlsCollection : RadScrollablePanelControlCollection
  {
    private RadScrollablePanel owner;

    public CollapsiblePanelControlsContainerControlsCollection(RadScrollablePanel owner)
      : base(owner)
    {
      this.owner = owner;
    }

    public override void Add(Control value)
    {
      if (this.Contains(value))
        return;
      this.owner.PanelContainer.Controls.Add(value);
    }
  }
}
