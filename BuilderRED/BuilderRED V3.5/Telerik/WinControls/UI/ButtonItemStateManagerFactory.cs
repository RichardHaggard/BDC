// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ButtonItemStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ButtonItemStateManagerFactory : ItemStateManagerFactory
  {
    public override StateNodeBase CreateRootState()
    {
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Enabled", (Condition) new SimpleCondition(RadElement.EnabledProperty, (object) true));
      nodeWithCondition.TrueStateLink = this.CreateSpecificStates();
      nodeWithCondition.FalseStateLink = (StateNodeBase) new StatePlaceholderNode("Disabled");
      return (StateNodeBase) nodeWithCondition;
    }

    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Mouse states");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("Pressed", (Condition) new SimpleCondition(RadButtonItem.IsPressedProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("IsDefault", (Condition) new SimpleCondition(RadButtonItem.IsDefaultProperty, (object) true));
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state3);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("MouseOver");
      sm.AddDefaultVisibleState("MouseDown");
      sm.AddDefaultVisibleState("Pressed");
      sm.AddDefaultVisibleState("IsDefault");
      sm.AddDefaultVisibleState("Disabled");
    }
  }
}
