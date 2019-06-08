// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SearchBarTextBoxButtonStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SearchBarTextBoxButtonStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("SearchBarTextBoxButtonItemStates");
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("IsSearching", (Condition) new SimpleCondition(ToolbarTextBoxButton.IsSearchingProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("IsSearching");
    }
  }
}
