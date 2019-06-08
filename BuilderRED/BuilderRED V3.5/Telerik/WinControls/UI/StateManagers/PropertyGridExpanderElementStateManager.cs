// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.PropertyGridExpanderElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class PropertyGridExpanderElementStateManager : ExpanderItemStateManager
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("ExpanderItemStates");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsExpanded", (Condition) new SimpleCondition(PropertyGridExpanderElement.IsExpandedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(PropertyGridExpanderElement.IsSelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("IsChildItem", (Condition) new SimpleCondition(PropertyGridItemElement.IsChildItemProperty, (object) true));
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("IsInactive", (Condition) new SimpleCondition(PropertyGridExpanderElement.IsControlInactiveProperty, (object) true));
      StateNodeWithCondition nodeWithCondition5 = new StateNodeWithCondition("IsInEditMode", (Condition) new SimpleCondition(PropertyGridExpanderElement.IsInEditModeProperty, (object) true));
      StateNodeWithCondition nodeWithCondition6 = new StateNodeWithCondition("IsRightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition5);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition6);
      return (StateNodeBase) compositeStateNode;
    }

    protected override ItemStateManager CreateStateManagerCore()
    {
      return (ItemStateManager) new PropertyGridExpanderStateManager(this.RootState);
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("MouseOver");
      sm.AddDefaultVisibleState("MouseDown");
      sm.AddDefaultVisibleState("Disabled");
      sm.AddDefaultVisibleState("IsInactive");
      sm.AddDefaultVisibleState("IsChildItem");
      sm.AddDefaultVisibleState("IsSelected");
      sm.AddDefaultVisibleState("IsSelected" + (object) '.' + "IsExpanded");
      sm.AddDefaultVisibleState("IsInEditMode");
    }
  }
}
