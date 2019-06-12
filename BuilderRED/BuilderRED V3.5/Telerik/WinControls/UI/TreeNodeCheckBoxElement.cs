// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeCheckBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class TreeNodeCheckBoxElement : RadCheckBoxElement
  {
    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadCheckBoxElement);
      }
    }

    public TreeNodeElement NodeElement
    {
      get
      {
        return this.FindAncestor<TreeNodeElement>();
      }
    }

    protected internal override void OnToggle()
    {
      if (this.IsThreeState)
      {
        base.OnToggle();
      }
      else
      {
        switch (this.ToggleState)
        {
          case ToggleState.Off:
            this.ToggleState = ToggleState.On;
            break;
          case ToggleState.On:
          case ToggleState.Indeterminate:
            this.ToggleState = ToggleState.Off;
            break;
        }
      }
    }

    protected override void OnToggleStateChanging(StateChangingEventArgs e)
    {
      base.OnToggleStateChanging(e);
      if (!e.Cancel || e.NewValue != ToggleState.Indeterminate || this.IsThreeState)
        return;
      TreeNodeElement nodeElement = this.NodeElement;
      if (nodeElement == null || !nodeElement.TreeViewElement.TriStateMode)
        return;
      e.Cancel = false;
    }
  }
}
