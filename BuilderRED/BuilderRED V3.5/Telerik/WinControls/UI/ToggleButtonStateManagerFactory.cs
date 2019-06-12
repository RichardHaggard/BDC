// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToggleButtonStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToggleButtonStateManagerFactory : ItemStateManagerFactory
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
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Pressed", (Condition) new SimpleCondition(RadButtonItem.IsPressedProperty, (object) true));
      nodeWithCondition1.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("ToggleState");
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("ToggleState=On", (Condition) new SimpleCondition(RadToggleButtonElement.ToggleStateProperty, (object) ToggleState.On));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      nodeWithCondition2.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("ToggleState=Intermediate", (Condition) new SimpleCondition(RadToggleButtonElement.ToggleStateProperty, (object) ToggleState.Indeterminate));
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager res)
    {
      res.AddDefaultVisibleState("MouseOver");
      res.AddDefaultVisibleState("Pressed");
      res.AddDefaultVisibleState("ToggleState=On");
      res.AddDefaultVisibleState("ToggleState=On.MouseOver");
      res.AddDefaultVisibleState("ToggleState=On.Pressed");
      res.AddDefaultVisibleState("ToggleState=Intermediate");
      res.AddDefaultVisibleState("ToggleState=Intermediate.MouseOver");
      res.AddDefaultVisibleState("ToggleState=Intermediate.Pressed");
      res.AddDefaultVisibleState("Disabled");
      res.AddDefaultVisibleState("Disabled.ToggleState=On");
      res.AddDefaultVisibleState("Disabled.ToggleState=Intermediate");
    }

    protected override ItemStateManager CreateStateManagerCore()
    {
      return (ItemStateManager) new ToggleButtonStateManager(this.RootState);
    }
  }
}
