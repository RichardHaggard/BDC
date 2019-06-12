// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewItemStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadPageViewItemStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateEnabledStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Base States");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(RadPageViewItem.IsSelectedProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("Pinned", (Condition) new SimpleCondition(RadPageViewItem.IsPinnedProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("Preview", (Condition) new SimpleCondition(RadPageViewItem.IsPreviewProperty, (object) true));
      StateNodeBase state6 = (StateNodeBase) new StateNodeWithCondition("RightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state4);
      compositeStateNode.AddState(state5);
      compositeStateNode.AddState(state6);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("Selected");
      sm.AddDefaultVisibleState("Pinned");
      sm.AddDefaultVisibleState("MouseOver");
      sm.AddDefaultVisibleState("MouseDown");
      sm.AddDefaultVisibleState("Preview");
      sm.AddDefaultVisibleState("RightToLeft");
      sm.AddDefaultVisibleState("Disabled");
    }
  }
}
