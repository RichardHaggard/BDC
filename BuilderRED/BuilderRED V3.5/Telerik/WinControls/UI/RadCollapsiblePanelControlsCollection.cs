// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCollapsiblePanelControlsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCollapsiblePanelControlsCollection : Control.ControlCollection
  {
    private RadCollapsiblePanel owner;

    public RadCollapsiblePanelControlsCollection(RadCollapsiblePanel owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override void Add(Control value)
    {
      if (value == this.owner.ControlsContainer)
        return;
      this.owner.ControlsContainer.Controls.Add(value);
    }

    public override void Clear()
    {
      this.owner.ControlsContainer.Controls.Clear();
    }

    public override void SetChildIndex(Control child, int newIndex)
    {
      if (child != this.owner.ControlsContainer)
        this.owner.ControlsContainer.Controls.SetChildIndex(child, newIndex);
      else
        base.SetChildIndex(child, newIndex);
    }

    public override int GetChildIndex(Control child, bool throwException)
    {
      if (child != this.owner.ControlsContainer)
        return this.owner.ControlsContainer.Controls.GetChildIndex(child, throwException);
      return base.GetChildIndex(child, throwException);
    }

    internal void AddInternal(Control value)
    {
      base.Add(value);
    }
  }
}
