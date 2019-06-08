﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownArrowButtonElementStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadDropDownArrowButtonElementStateManagerFactory : ButtonItemStateManagerFactory
  {
    private RadProperty dropDownProperty;

    public RadDropDownArrowButtonElementStateManagerFactory(RadProperty dropDownProperty)
    {
      this.dropDownProperty = dropDownProperty;
    }

    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode specificStates = (CompositeStateNode) base.CreateSpecificStates();
      StateNodeBase state = (StateNodeBase) new StateNodeWithCondition("IsDropDownShown", (Condition) new SimpleCondition(this.dropDownProperty, (object) true));
      specificStates.AddState(state);
      return (StateNodeBase) specificStates;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("IsDropDownShown");
    }
  }
}
