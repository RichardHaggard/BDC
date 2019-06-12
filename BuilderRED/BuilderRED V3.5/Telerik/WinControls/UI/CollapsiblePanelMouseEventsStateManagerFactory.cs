// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsiblePanelMouseEventsStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CollapsiblePanelMouseEventsStateManagerFactory : ButtonItemStateManagerFactory
  {
    private RadProperty expandDirectionProperty;
    private RadProperty isExpandedProperty;

    public CollapsiblePanelMouseEventsStateManagerFactory(
      RadProperty expandDirectionProperty,
      RadProperty isExpandedProperty)
    {
      this.expandDirectionProperty = expandDirectionProperty;
      this.isExpandedProperty = isExpandedProperty;
    }

    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode specificStates = base.CreateSpecificStates() as CompositeStateNode;
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("ExpandDirection=Down", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Down));
      nodeWithCondition1.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("ExpandDirection=Up", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Up));
      specificStates.AddState((StateNodeBase) nodeWithCondition1);
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("ExpandDirection=Left", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Left));
      nodeWithCondition2.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("ExpandDirection=Right", (Condition) new SimpleCondition(this.expandDirectionProperty, (object) RadDirection.Right));
      specificStates.AddState((StateNodeBase) nodeWithCondition2);
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("IsNotExpanded", (Condition) new SimpleCondition(this.isExpandedProperty, (object) false));
      specificStates.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) specificStates;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("ExpandDirection=Down");
      sm.AddDefaultVisibleState("ExpandDirection=Up");
      sm.AddDefaultVisibleState("ExpandDirection=Left");
      sm.AddDefaultVisibleState("ExpandDirection=Right");
      sm.AddDefaultVisibleState("IsNotExpanded");
    }
  }
}
