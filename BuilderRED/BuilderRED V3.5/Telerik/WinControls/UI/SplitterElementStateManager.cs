// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitterElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SplitterElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateEnabledStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Base States");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("IsVertical", (Condition) new SimpleCondition(SplitterElement.IsVerticalProperty, (object) true));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("IsCollapsed", (Condition) new SimpleCondition(SplitterElement.IsCollapsedProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("LeftAlignment", (Condition) new SimpleCondition(SplitterElement.SplitterAlignmentProperty, (object) RadDirection.Left));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("RightAlignment", (Condition) new SimpleCondition(SplitterElement.SplitterAlignmentProperty, (object) RadDirection.Right));
      StateNodeBase state5 = (StateNodeBase) new StateNodeWithCondition("UpAlignment", (Condition) new SimpleCondition(SplitterElement.SplitterAlignmentProperty, (object) RadDirection.Up));
      StateNodeBase state6 = (StateNodeBase) new StateNodeWithCondition("DownAlignment", (Condition) new SimpleCondition(SplitterElement.SplitterAlignmentProperty, (object) RadDirection.Down));
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
      sm.AddDefaultVisibleState("IsVertical");
    }
  }
}
