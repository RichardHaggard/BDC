// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewBaseItemElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GanttViewBaseItemElementStateManager : RadGanttViewBaseStateManager
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(GanttViewBaseItemElement.SelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("Current", (Condition) new SimpleCondition(GanttViewBaseItemElement.CurrentProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("Gantt view base item element states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      return (StateNodeBase) compositeStateNode;
    }

    protected override ItemStateManagerBase CreateStateManager()
    {
      ItemStateManagerBase stateManager = base.CreateStateManager();
      stateManager.AddDefaultVisibleState("Selected");
      stateManager.AddDefaultVisibleState("Current");
      return stateManager;
    }
  }
}
