// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeImageElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TreeNodeImageElementStateManager : ExpanderItemStateManager
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsRootNode", (Condition) new SimpleCondition(TreeNodeImageElement.IsRootNodeProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("HasChildren", (Condition) new SimpleCondition(TreeNodeImageElement.HasChildrenProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(TreeNodeImageElement.IsSelectedProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("IsCurrent", (Condition) new SimpleCondition(TreeNodeImageElement.IsCurrentProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("IsExpanded", (Condition) new SimpleCondition(TreeNodeImageElement.IsExpandedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("HotTracking", (Condition) new SimpleCondition(TreeNodeImageElement.HotTrackingProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("TreeNodeImageElement states");
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition);
      compositeStateNode.AddState(state5);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsRootNode");
      sm.AddDefaultVisibleState("IsCurrent");
      sm.AddDefaultVisibleState("IsSelected");
      sm.AddDefaultVisibleState("IsSelected.IsCurrent");
      sm.AddDefaultVisibleState("IsExpanded");
      sm.AddDefaultVisibleState("HasChildren");
    }
  }
}
