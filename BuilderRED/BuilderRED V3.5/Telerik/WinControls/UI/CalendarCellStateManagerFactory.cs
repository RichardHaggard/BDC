// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarCellStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class CalendarCellStateManagerFactory : ItemStateManagerFactory
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
      CompositeStateNode compositeStateNode1 = new CompositeStateNode("Cell States");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(CalendarCellElement.SelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true));
      nodeWithCondition2.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("Focused", (Condition) new SimpleCondition(CalendarCellElement.FocusedProperty, (object) true));
      CompositeStateNode compositeStateNode2 = new CompositeStateNode("mouse state tree");
      compositeStateNode2.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode2.AddState((StateNodeBase) nodeWithCondition3);
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("Header", (Condition) new SimpleCondition(CalendarCellElement.IsHeaderCellProperty, (object) true));
      compositeStateNode1.AddState((StateNodeBase) nodeWithCondition4);
      compositeStateNode1.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode1.AddState((StateNodeBase) compositeStateNode2);
      CompositeStateNode compositeStateNode3 = new CompositeStateNode("Day cells");
      nodeWithCondition4.FalseStateLink = (StateNodeBase) compositeStateNode3;
      StateNodeWithCondition nodeWithCondition5 = new StateNodeWithCondition("OutOfRange", (Condition) new SimpleCondition(CalendarCellElement.OutOfRangeProperty, (object) true));
      StateNodeWithCondition nodeWithCondition6 = new StateNodeWithCondition("OtherMonth", (Condition) new SimpleCondition(CalendarCellElement.OtherMonthProperty, (object) true));
      StateNodeWithCondition nodeWithCondition7 = new StateNodeWithCondition("Weekend", (Condition) new SimpleCondition(CalendarCellElement.WeekEndProperty, (object) true));
      StateNodeWithCondition nodeWithCondition8 = new StateNodeWithCondition("SpecialDay", (Condition) new SimpleCondition(CalendarCellElement.SpecialDayProperty, (object) true));
      StateNodeWithCondition nodeWithCondition9 = new StateNodeWithCondition("Today", (Condition) new SimpleCondition(CalendarCellElement.TodayProperty, (object) true));
      compositeStateNode3.AddState((StateNodeBase) nodeWithCondition5);
      compositeStateNode3.AddState((StateNodeBase) nodeWithCondition6);
      compositeStateNode3.AddState((StateNodeBase) nodeWithCondition9);
      compositeStateNode3.AddState((StateNodeBase) nodeWithCondition7);
      compositeStateNode3.AddState((StateNodeBase) nodeWithCondition8);
      return (StateNodeBase) compositeStateNode1;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("Selected");
      sm.AddDefaultVisibleState("Today");
      sm.AddDefaultVisibleState("Weekend");
    }
  }
}
