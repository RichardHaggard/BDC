// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewCellElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewCellElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("CurrentColumn", (Condition) new SimpleCondition(GanttViewTextViewCellElement.CurrentColumnProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("CurrentRow", (Condition) new SimpleCondition(GanttViewTextViewCellElement.CurrentRowProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(GanttViewTextViewCellElement.SelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("HotTracking", (Condition) new SimpleCondition(GanttViewTextViewCellElement.HotTrackingProperty, (object) true));
      StateNodeWithCondition nodeWithCondition5 = new StateNodeWithCondition("IsFirstCellState", (Condition) new SimpleCondition(GanttViewTextViewCellElement.IsFirstCellProperty, (object) true));
      StateNodeWithCondition nodeWithCondition6 = new StateNodeWithCondition("IsLastCellState", (Condition) new SimpleCondition(GanttViewTextViewCellElement.IsLastCellProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("GanttViewCellElement states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition5);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition6);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
