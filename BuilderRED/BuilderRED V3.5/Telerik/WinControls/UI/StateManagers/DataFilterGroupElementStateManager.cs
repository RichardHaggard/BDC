// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.DataFilterGroupElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class DataFilterGroupElementStateManager : TreeNodeElementStateManager
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("States");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("LogicalOperator=And", (Condition) new SimpleCondition(DataFilterGroupElement.LogicalOperatorProperty, (object) FilterLogicalOperator.And));
      state1.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("LogicalOperator=Or", (Condition) new SimpleCondition(DataFilterGroupElement.LogicalOperatorProperty, (object) FilterLogicalOperator.Or));
      compositeStateNode.AddState(state1);
      CompositeStateNode specificStates = base.CreateSpecificStates() as CompositeStateNode;
      if (specificStates != null)
      {
        foreach (StateNodeBase state2 in specificStates.States)
          compositeStateNode.AddState(state2);
      }
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("LogicalOperator=And");
      sm.AddDefaultVisibleState("LogicalOperator=Or");
      base.AddDefaultVisibleStates(sm);
    }
  }
}
