// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollBarThumbStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ScrollBarThumbStateManager : ItemStateManagerFactory
  {
    public override StateNodeBase CreateRootState()
    {
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("Enabled", (Condition) new SimpleCondition(RadElement.EnabledProperty, (object) true));
      nodeWithCondition.TrueStateLink = this.CreateSpecificStates();
      nodeWithCondition.FalseStateLink = (StateNodeBase) new StatePlaceholderNode("Disabled");
      return (StateNodeBase) nodeWithCondition;
    }

    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Pressed", (Condition) new SimpleCondition(ScrollBarThumb.IsPressedProperty, (object) true));
      nodeWithCondition1.FalseStateLink = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true));
      CompositeStateNode compositeStateNode = new CompositeStateNode("ScrollType");
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("ScrollType=Vertical", (Condition) new SimpleCondition(RadScrollBarElement.ScrollTypeProperty, (object) ScrollType.Vertical));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("RightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager res)
    {
      res.AddDefaultVisibleState("MouseOver");
      res.AddDefaultVisibleState("Pressed");
      res.AddDefaultVisibleState("ScrollType=Vertical");
      res.AddDefaultVisibleState("ScrollType=Vertical.MouseOver");
      res.AddDefaultVisibleState("ScrollType=Vertical.Pressed");
      res.AddDefaultVisibleState("Disabled");
    }
  }
}
