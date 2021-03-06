﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToggleSwitchThumbElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToggleSwitchThumbElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("States");
      StateNodeBase state = (StateNodeBase) new StateNodeWithCondition("IsOn", (Condition) new SimpleCondition(ToggleSwitchThumbElement.IsOnProperty, (object) true));
      compositeStateNode.AddState(state);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("IsOn");
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("Disabled.IsOn");
    }

    protected override ItemStateManager CreateStateManagerCore()
    {
      return (ItemStateManager) new ToggleSwitchThumbButtonStateManager(this.RootState);
    }
  }
}
