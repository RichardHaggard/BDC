// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewSpinEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GanttViewSpinEditor : BaseSpinEditor
  {
    public override void Initialize(object owner, object value)
    {
      base.Initialize(owner, value);
      if (value == null)
        return;
      this.ValueType = value.GetType();
    }

    public override void OnLostFocus()
    {
      base.OnLostFocus();
      GanttViewTextItemElement ownerElement = this.OwnerElement as GanttViewTextItemElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || this.EditorElement.Parent.ContainsMouse))
        return;
      ownerElement.TextView.GanttViewElement.EndEdit();
    }

    public override object Value
    {
      get
      {
        return Convert.ChangeType(base.Value, this.ValueType);
      }
      set
      {
        base.Value = value;
      }
    }
  }
}
