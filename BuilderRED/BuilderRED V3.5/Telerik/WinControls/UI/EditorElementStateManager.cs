// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.EditorElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class EditorElementStateManager : ItemStateManagerFactory
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
      CompositeStateNode compositeStateNode = new CompositeStateNode("Enabled States");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("ContainsFocus", (Condition) new SimpleCondition(RadElement.ContainsFocusProperty, (object) true));
      compositeStateNode.AddState(state1);
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("ContainsMouse", (Condition) new SimpleCondition(RadElement.ContainsMouseProperty, (object) true));
      compositeStateNode.AddState(state2);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("ContainsFocus");
      sm.AddDefaultVisibleState("ContainsMouse");
      sm.AddDefaultVisibleState("Disabled");
    }
  }
}
