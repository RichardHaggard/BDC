// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BackstageItemStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class BackstageItemStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateEnabledStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Mouse states");
      StateNodeBase state1 = (StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new ComplexCondition((Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true), BinaryOperator.OrOperator, (Condition) new SimpleCondition(BackstageButtonItem.IsCurrentProperty, (object) true)));
      StateNodeBase state2 = (StateNodeBase) new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true));
      StateNodeBase state3 = (StateNodeBase) new StateNodeWithCondition("ContainsMouse", (Condition) new SimpleCondition(RadElement.ContainsMouseProperty, (object) true));
      StateNodeBase state4 = (StateNodeBase) new StateNodeWithCondition("RightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState(state3);
      compositeStateNode.AddState(state1);
      compositeStateNode.AddState(state2);
      compositeStateNode.AddState(state4);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
