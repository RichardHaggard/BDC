// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StripViewElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class StripViewElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateEnabledStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Base States");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("LeftAlign", (Condition) new SimpleCondition(RadPageViewStripElement.StripAlignmentProperty, (object) StripViewAlignment.Left));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("RightAlign", (Condition) new SimpleCondition(RadPageViewStripElement.StripAlignmentProperty, (object) StripViewAlignment.Right));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("BottomAlign", (Condition) new SimpleCondition(RadPageViewStripElement.StripAlignmentProperty, (object) StripViewAlignment.Bottom));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("RightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state4);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      sm.AddDefaultVisibleState("LeftAlign");
      sm.AddDefaultVisibleState("RightAlign");
      sm.AddDefaultVisibleState("BottomAlign");
      sm.AddDefaultVisibleState("Disabled");
    }
  }
}
