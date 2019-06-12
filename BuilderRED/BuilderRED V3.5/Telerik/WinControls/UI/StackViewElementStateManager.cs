// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StackViewElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class StackViewElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("StackPositions");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("BottomStackPosition", (Condition) new SimpleCondition(RadPageViewStackElement.StackPositionProperty, (object) StackViewPosition.Bottom));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("TopStackPosition", (Condition) new SimpleCondition(RadPageViewStackElement.StackPositionProperty, (object) StackViewPosition.Top));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("LeftStackPosition", (Condition) new SimpleCondition(RadPageViewStackElement.StackPositionProperty, (object) StackViewPosition.Left));
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("RightStackPosition", (Condition) new SimpleCondition(RadPageViewStackElement.StackPositionProperty, (object) StackViewPosition.Right));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("LeftStackPosition");
      sm.AddDefaultVisibleState("TopStackPosition");
      sm.AddDefaultVisibleState("RightStackPosition");
      sm.AddDefaultVisibleState("BottomStackPosition");
    }
  }
}
