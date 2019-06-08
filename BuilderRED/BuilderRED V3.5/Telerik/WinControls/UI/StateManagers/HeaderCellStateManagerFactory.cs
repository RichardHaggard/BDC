// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StateManagers.HeaderCellStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI.StateManagers
{
  public class HeaderCellStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("HeaderCellStates");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsSortedAscending", (Condition) new SimpleCondition(GridHeaderCellElement.IsSortedAscendingProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsSortedDescending", (Condition) new SimpleCondition(GridHeaderCellElement.IsSortedDescendingProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("IsSortedAscending");
      sm.AddDefaultVisibleState("IsSortedDescending");
    }
  }
}
