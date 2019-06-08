// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarThumbElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TrackBarThumbElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("all states");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsSelected", (Condition) new SimpleCondition(TrackBarThumbElement.IsSelectedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsVertical", (Condition) new SimpleCondition(TrackBarElementWithOrientation.IsVerticalProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      return (StateNodeBase) compositeStateNode;
    }

    protected override ItemStateManagerBase CreateStateManager()
    {
      ItemStateManagerBase stateManager = base.CreateStateManager();
      stateManager.AddDefaultVisibleState("IsSelected");
      stateManager.AddDefaultVisibleState("IsVertical");
      return stateManager;
    }
  }
}
