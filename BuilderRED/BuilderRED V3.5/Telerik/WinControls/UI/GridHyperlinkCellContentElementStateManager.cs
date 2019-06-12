// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridHyperlinkCellContentElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GridHyperlinkCellContentElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Visited", (Condition) new SimpleCondition(GridHyperlinkCellContentElement.VisitedProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("Visited");
    }
  }
}
