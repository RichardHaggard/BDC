// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterButtonStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GridFilterButtonStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("FilterButtonStates");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsFilterMenuShown", (Condition) new SimpleCondition(GridFilterButtonElement.IsFilterMenuShownProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsFilterApplied", (Condition) new SimpleCondition(GridFilterCellElement.IsFilterAppliedProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
