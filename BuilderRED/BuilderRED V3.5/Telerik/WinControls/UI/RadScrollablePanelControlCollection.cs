// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollablePanelControlCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadScrollablePanelControlCollection : Control.ControlCollection
  {
    private RadScrollablePanel owner;

    public RadScrollablePanelControlCollection(RadScrollablePanel owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override void Add(Control value)
    {
      if (object.ReferenceEquals((object) value, (object) this.owner.VerticalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.HorizontalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.PanelContainer))
        throw new InvalidOperationException("Control already added!");
      this.owner.PanelContainer.Controls.Add(value);
    }

    public override void Remove(Control value)
    {
      if (object.ReferenceEquals((object) value, (object) this.owner.VerticalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.HorizontalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.PanelContainer))
        return;
      this.owner.PanelContainer.Controls.Remove(value);
    }

    public override void Clear()
    {
      this.owner.PanelContainer.Controls.Clear();
    }

    public override void SetChildIndex(Control value, int newIndex)
    {
      if (object.ReferenceEquals((object) value, (object) this.owner.VerticalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.HorizontalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.PanelContainer))
        base.SetChildIndex(value, newIndex);
      else
        this.owner.PanelContainer.Controls.SetChildIndex(value, newIndex);
    }

    public override int GetChildIndex(Control value, bool throwException)
    {
      if (object.ReferenceEquals((object) value, (object) this.owner.VerticalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.HorizontalScrollbar) || object.ReferenceEquals((object) value, (object) this.owner.PanelContainer))
        return base.GetChildIndex(value, throwException);
      return this.owner.PanelContainer.Controls.GetChildIndex(value, throwException);
    }

    internal void AddControlInternal(Control value)
    {
      base.Add(value);
    }
  }
}
