// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemElementBaseStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemElementBaseStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("PropertyGridItemElementBaseItemStates");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(PropertyGridItemElementBase.IsSelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsExpanded", (Condition) new SimpleCondition(PropertyGridItemElementBase.IsExpandedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("IsInactive", (Condition) new SimpleCondition(PropertyGridItemElementBase.IsControlInactiveProperty, (object) true));
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("IsRightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("IsSelected");
      sm.AddDefaultVisibleState("IsExpanded");
      sm.AddDefaultVisibleState("IsInactive");
    }
  }
}
