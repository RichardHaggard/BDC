// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.TreeNodeContentElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class TreeNodeContentElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsRootNode", (Condition) new SimpleCondition(TreeNodeContentElement.IsRootNodeProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("HasChildren", (Condition) new SimpleCondition(TreeNodeContentElement.HasChildrenProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(TreeNodeContentElement.HotTrackingProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(TreeNodeContentElement.IsSelectedProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("Current", (Condition) new SimpleCondition(TreeNodeContentElement.IsCurrentProperty, (object) true));
      StateNodeBase state6 = (StateNodeBase) new StateNodeWithCondition("Expanded", (Condition) new SimpleCondition(TreeNodeContentElement.IsExpandedProperty, (object) true));
      StateNodeBase state7 = (StateNodeBase) new StateNodeWithCondition("ControlInactive", (Condition) new SimpleCondition(TreeNodeContentElement.IsControlInactiveProperty, (object) true));
      StateNodeBase state8 = (StateNodeBase) new StateNodeWithCondition("FullRowSelect", (Condition) new SimpleCondition(TreeNodeContentElement.FullRowSelectProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("TreeNodeContentElement states");
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state7);
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState(state5);
      compositeStateNode.AddState(state6);
      compositeStateNode.AddState(state8);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("Current");
      sm.AddDefaultVisibleState("Selected");
      sm.AddDefaultVisibleState("Selected.Current");
      sm.AddDefaultVisibleState("MouseOver");
      sm.AddDefaultVisibleState("FullRowSelect");
      sm.AddDefaultVisibleState("ControlInactive.Current");
    }
  }
}
