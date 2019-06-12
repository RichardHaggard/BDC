// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.TreeNodeExpanderItemStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class TreeNodeExpanderItemStateManager : ExpanderItemStateManager
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("ExpanderItemStates");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsExpanded", (Condition) new SimpleCondition(ExpanderItem.ExpandedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(TreeNodeExpanderItem.IsSelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("HotTracking", (Condition) new SimpleCondition(TreeNodeExpanderItem.HotTrackingProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      return (StateNodeBase) compositeStateNode;
    }

    protected override ItemStateManager CreateStateManagerCore()
    {
      return (ItemStateManager) new TreeExpanderStateManager(this.RootState);
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsExpanded");
      sm.AddDefaultVisibleState("IsSelected");
      sm.AddDefaultVisibleState("IsSelected.IsExpanded");
      sm.AddDefaultVisibleState("HotTracking");
      sm.AddDefaultVisibleState("HotTracking.IsExpanded");
    }
  }
}
