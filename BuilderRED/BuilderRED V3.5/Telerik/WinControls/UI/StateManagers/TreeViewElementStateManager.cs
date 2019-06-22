﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.TreeViewElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class TreeViewElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("Focused", (Condition) new SimpleCondition(RadElement.IsFocusedProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("TreeViewElement states");
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("Focused");
      sm.AddDefaultVisibleState("MouseOver");
    }
  }
}