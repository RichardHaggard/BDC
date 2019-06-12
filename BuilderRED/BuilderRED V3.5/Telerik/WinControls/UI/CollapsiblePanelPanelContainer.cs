// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelPanelContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CollapsiblePanelPanelContainer : RadScrollablePanelContainer
  {
    public CollapsiblePanelPanelContainer(RadScrollablePanel parentPanel)
      : base(parentPanel)
    {
    }

    public new Size Size
    {
      get
      {
        return base.Size;
      }
      set
      {
        base.Size = value;
        RadCollapsiblePanel collapsiblePanelParent = this.FindCollapsiblePanelParent();
        if (collapsiblePanelParent == null || !collapsiblePanelParent.IsInitializing)
          return;
        collapsiblePanelParent.ControlsContainer.Size = value;
      }
    }

    protected virtual RadCollapsiblePanel FindCollapsiblePanelParent()
    {
      Control parent = this.Parent;
      while (!(parent is RadCollapsiblePanel))
      {
        parent = parent.Parent;
        if (parent == null)
          return (RadCollapsiblePanel) null;
      }
      return parent as RadCollapsiblePanel;
    }
  }
}
