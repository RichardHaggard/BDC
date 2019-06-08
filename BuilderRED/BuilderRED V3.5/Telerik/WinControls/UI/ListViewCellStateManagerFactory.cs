// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewCellStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  internal class ListViewCellStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Current", (Condition) new SimpleCondition(DetailListViewCellElement.CurrentProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsSorted", (Condition) new SimpleCondition(DetailListViewCellElement.IsSortedProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("ListViewCellElement states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
