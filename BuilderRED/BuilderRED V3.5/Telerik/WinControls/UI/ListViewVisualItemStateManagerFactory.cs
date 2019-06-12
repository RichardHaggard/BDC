// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewVisualItemStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ListViewVisualItemStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsHotTracking", (Condition) new SimpleCondition(BaseListViewVisualItem.HotTrackingProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("Current", (Condition) new SimpleCondition(BaseListViewVisualItem.CurrentProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(BaseListViewVisualItem.SelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition4 = new StateNodeWithCondition("ControlInactive", (Condition) new SimpleCondition(BaseListViewVisualItem.IsControlInactiveProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("ListViewVisualItem states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition4);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
