// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataCellStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ListViewDataCellStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Current", (Condition) new SimpleCondition(DetailListViewCellElement.CurrentProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(DetailListViewDataCellElement.SelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("CurrentRow", (Condition) new SimpleCondition(DetailListViewDataCellElement.CurrentRowProperty, (object) true));
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("ShowGrid", (Condition) new SimpleCondition(DetailListViewDataCellElement.ShowGridLinesProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("ListViewCellElement states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
