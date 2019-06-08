// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DropDownButtonStateManagerFatory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class DropDownButtonStateManagerFatory : ItemStateManagerFactory
  {
    public override StateNodeBase CreateRootState()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Enabled", (Condition) new SimpleCondition(RadElement.EnabledProperty, (object) true));
      nodeWithCondition1.FalseStateLink = (StateNodeBase) new StatePlaceholderNode("Disabled");
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("DropDownOpened", (Condition) new SimpleCondition(RadDropDownButtonElement.IsDropDownShownProperty, (object) true));
      nodeWithCondition1.TrueStateLink = (StateNodeBase) nodeWithCondition2;
      nodeWithCondition2.FalseStateLink = this.CreateSpecificStates();
      return (StateNodeBase) nodeWithCondition1;
    }

    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Pressed", (Condition) new SimpleCondition(RadButtonItem.IsPressedProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("MouseOverStates");
      nodeWithCondition1.FalseStateLink = (StateNodeBase) compositeStateNode;
      nodeWithCondition1.FalseStateLink.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("Focused", (Condition) new SimpleCondition(RadElement.IsFocusedProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true)));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("ActionPart", (Condition) new SimpleCondition(RadDropDownButtonElement.MouseOverStateProperty, (object) DropDownButtonMouseOverState.OverActionButton));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      nodeWithCondition2.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("ArrowPart", (Condition) new SimpleCondition(RadDropDownButtonElement.MouseOverStateProperty, (object) DropDownButtonMouseOverState.OverArrowButton));
      return (StateNodeBase) nodeWithCondition1;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager res)
    {
      res.AddDefaultVisibleState("MouseOver.ActionPart");
      res.AddDefaultVisibleState("MouseOver.ArrowPart");
      res.AddDefaultVisibleState("Pressed");
      res.AddDefaultVisibleState("DropDownOpened");
    }
  }
}
