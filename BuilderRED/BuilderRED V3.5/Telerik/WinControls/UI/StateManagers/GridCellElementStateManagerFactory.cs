// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.GridCellElementStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class GridCellElementStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsCurrent", (Condition) new SimpleCondition(GridCellElement.IsCurrentProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(GridCellElement.IsSelectedProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("IsCurrentRow", (Condition) new SimpleCondition(GridCellElement.IsCurrentRowProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("IsCurrentColumn", (Condition) new SimpleCondition(GridCellElement.IsCurrentColumnProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("IsPinned", (Condition) new SimpleCondition(GridCellElement.IsPinnedProperty, (object) true));
      StateNodeBase state6 = (StateNodeBase) new StateNodeWithCondition("IsSorted", (Condition) new SimpleCondition(GridCellElement.IsSortedProperty, (object) true));
      StateNodeBase state7 = (StateNodeBase) new StateNodeWithCondition("HotTracking", (Condition) new SimpleCondition(GridRowElement.HotTrackingProperty, (object) true));
      StateNodeBase state8 = (StateNodeBase) new StateNodeWithCondition("IsRowSelected", (Condition) new SimpleCondition(GridRowElement.IsSelectedProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("all states");
      compositeStateNode.AddState(state8);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState(state5);
      compositeStateNode.AddState(state6);
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state7);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("IsCurrentRow");
      sm.AddDefaultVisibleState("IsCurrentColumn");
    }
  }
}
