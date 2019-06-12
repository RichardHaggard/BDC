// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabStripButtonItemStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TabStripButtonItemStateManager : ButtonItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Mouse states");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsDefault", (Condition) new SimpleCondition(RadButtonItem.IsDefaultProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(TabStripButtonItem.IsSelectedProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("Pressed", (Condition) new SimpleCondition(RadButtonItem.IsPressedProperty, (object) true));
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState(state5);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("Selected");
      base.AddDefaultVisibleStates(sm);
    }
  }
}
