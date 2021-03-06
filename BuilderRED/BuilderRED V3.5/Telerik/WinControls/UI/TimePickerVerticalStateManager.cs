﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimePickerVerticalStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TimePickerVerticalStateManager : ItemStateManagerFactory
  {
    private RadProperty clockBeforeTablesProperty;

    public TimePickerVerticalStateManager(RadProperty clockBeforeTablesProperty)
    {
      this.clockBeforeTablesProperty = clockBeforeTablesProperty;
    }

    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("ClockBeforeTables1", (Condition) new SimpleCondition(this.clockBeforeTablesProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("TimePickerVerticalStateManager states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition);
      return (StateNodeBase) compositeStateNode;
    }

    protected override ItemStateManagerBase CreateStateManager()
    {
      ItemStateManagerBase stateManager = base.CreateStateManager();
      stateManager.AddDefaultVisibleState("ClockBeforeTables1");
      return stateManager;
    }
  }
}
