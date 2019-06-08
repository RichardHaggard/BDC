// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridValueElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridValueElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("PropertyValueElement element states");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsModified", (Condition) new SimpleCondition(PropertyGridValueElement.IsModifiedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("ContainsError", (Condition) new SimpleCondition(PropertyGridValueElement.ContainsErrorProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("IsRightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsModified");
      sm.AddDefaultVisibleState("ContainsError");
    }
  }
}
