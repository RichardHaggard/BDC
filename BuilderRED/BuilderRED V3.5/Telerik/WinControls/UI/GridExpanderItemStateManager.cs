// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridExpanderItemStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GridExpanderItemStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("GridExpanderItemStates");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsExpanded", (Condition) new SimpleCondition(ExpanderItem.ExpandedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsCurrentRow", (Condition) new SimpleCondition(GridRowElement.IsCurrentProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("RowHottracking", (Condition) new SimpleCondition(GridRowElement.HotTrackingProperty, (object) true));
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("IsRowSelected", (Condition) new SimpleCondition(GridRowElement.IsSelectedProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsExpanded");
    }
  }
}
