// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.GridRowElementStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class GridRowElementStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateEnabledStates()
    {
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(GridRowElement.IsSelectedProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("IsCurrent", (Condition) new SimpleCondition(GridRowElement.IsCurrentProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("HotTracking", (Condition) new SimpleCondition(GridRowElement.HotTrackingProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("ContainsCurrentCell", (Condition) new SimpleCondition(GridRowElement.ContainsCurrentCellProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("ContainsSelectedCells", (Condition) new SimpleCondition(GridRowElement.ContainsSelectedCellsProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("row element states");
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState(state5);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsSelected");
      sm.AddDefaultVisibleState("IsCurrent");
      sm.AddDefaultVisibleState("HotTracking");
    }
  }
}
