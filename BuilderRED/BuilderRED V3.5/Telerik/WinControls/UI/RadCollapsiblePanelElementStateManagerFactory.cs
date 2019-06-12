// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCollapsiblePanelElementStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadCollapsiblePanelElementStateManagerFactory : ItemStateManagerFactory
  {
    private RadProperty expandDirectionProperty;
    private RadProperty isExpandedProperty;

    public RadCollapsiblePanelElementStateManagerFactory(
      RadProperty expandDirectionProperty,
      RadProperty isExpandedProperty)
    {
      this.expandDirectionProperty = expandDirectionProperty;
      this.isExpandedProperty = isExpandedProperty;
    }

    public override StateNodeBase CreateRootState()
    {
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Enabled", (Condition) new SimpleCondition(RadElement.EnabledProperty, (object) true));
      nodeWithCondition.TrueStateLink = this.CreateSpecificStates();
      nodeWithCondition.FalseStateLink = (StateNodeBase) new StatePlaceholderNode("Disabled");
      return (StateNodeBase) nodeWithCondition;
    }

    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("ExpandDirection");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("ExpandDirection=Down", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Down));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("ExpandDirection=Up", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Up));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("ExpandDirection=Right", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Right));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("ExpandDirection=Left", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Left));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      StateNodeWithCondition nodeWithCondition5 = new StateNodeWithCondition("IsNotExpanded", (Condition) new SimpleCondition(this.isExpandedProperty, (object) false));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition5);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("ExpandDirection=Down");
      sm.AddDefaultVisibleState("ExpandDirection=Up");
      sm.AddDefaultVisibleState("ExpandDirection=Left");
      sm.AddDefaultVisibleState("ExpandDirection=Right");
      sm.AddDefaultVisibleState("IsNotExpanded");
      sm.AddDefaultVisibleState("Disabled");
    }
  }
}
