// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExpanderElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ExpanderElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsExpanded", (Condition) new SimpleCondition(ExpanderItem.ExpandedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("HotTracking", (Condition) new SimpleCondition(TreeNodeElement.HotTrackingProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("ExpanderElement States");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsExpanded");
      sm.AddDefaultVisibleState("HotTracking");
    }
  }
}
