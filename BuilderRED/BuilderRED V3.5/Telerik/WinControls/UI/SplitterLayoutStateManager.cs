// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitterLayoutStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SplitterLayoutStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateEnabledStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Base States");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsVertical", (Condition) new SimpleCondition(SplitterElementLayout.IsVerticalProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("UseSplitterButtons", (Condition) new SimpleCondition(SplitterElementLayout.UseSplitterButtonsProperty, (object) true));
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsVertical");
    }
  }
}
