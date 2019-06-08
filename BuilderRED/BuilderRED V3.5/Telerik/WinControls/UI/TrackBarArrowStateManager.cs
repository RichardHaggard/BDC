// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarArrowStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TrackBarArrowStateManager : ButtonItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode specificStates = (CompositeStateNode) base.CreateSpecificStates();
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("IsVertical", (Condition) new SimpleCondition(TrackBarArrowButton.IsVerticalProperty, (object) true));
      specificStates.AddState((StateNodeBase) nodeWithCondition);
      return (StateNodeBase) specificStates;
    }

    protected override ItemStateManagerBase CreateStateManager()
    {
      ItemStateManagerBase stateManager = base.CreateStateManager();
      stateManager.AddDefaultVisibleState("IsVertical");
      return stateManager;
    }
  }
}
