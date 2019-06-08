// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.VirtualGridCellElementStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class VirtualGridCellElementStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsCurrent", (Condition) new SimpleCondition(VirtualGridCellElement.IsCurrentProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(VirtualGridCellElement.IsSelectedProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("IsCurrentRow", (Condition) new SimpleCondition(VirtualGridCellElement.IsCurrentRowProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("IsCurrentColumn", (Condition) new SimpleCondition(VirtualGridCellElement.IsCurrentColumnProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("IsPinned", (Condition) new SimpleCondition(VirtualGridCellElement.IsPinnedProperty, (object) true));
      StateNodeBase state6 = (StateNodeBase) new StateNodeWithCondition("IsSorted", (Condition) new SimpleCondition(VirtualGridCellElement.IsSortedProperty, (object) true));
      StateNodeBase state7 = (StateNodeBase) new StateNodeWithCondition("HotTracking", (Condition) new SimpleCondition(VirtualGridRowElement.HotTrackingProperty, (object) true));
      StateNodeBase state8 = (StateNodeBase) new StateNodeWithCondition("IsRowSelected", (Condition) new SimpleCondition(VirtualGridRowElement.IsSelectedProperty, (object) true));
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
