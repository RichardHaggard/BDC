﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewPinButtonElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadPageViewPinButtonElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateEnabledStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Base States");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("Preview", (Condition) new SimpleCondition(RadPageViewPinButtonElement.IsPreviewProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(RadPageViewButtonElement.IsSelectedProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("Pinned", (Condition) new SimpleCondition(RadPageViewPinButtonElement.IsPinnedProperty, (object) true));
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState(state5);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("MouseOver");
      sm.AddDefaultVisibleState("MouseDown");
      sm.AddDefaultVisibleState("Disabled");
      sm.AddDefaultVisibleState("Preview");
      sm.AddDefaultVisibleState("Selected");
      sm.AddDefaultVisibleState("Pinned");
    }
  }
}
