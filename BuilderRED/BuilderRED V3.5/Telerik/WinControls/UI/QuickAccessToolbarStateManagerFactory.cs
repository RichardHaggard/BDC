// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.QuickAccessToolbarStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class QuickAccessToolbarStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("QAToolbar states");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsBelowRibbon", (Condition) new SimpleCondition(RadRibbonBarElement.QuickAccessToolbarBelowRibbonProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsBackstageMode", (Condition) new SimpleCondition(RadRibbonBarElement.IsBackstageModeProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("RightToLeft", (Condition) new SimpleCondition(RadElement.RightToLeftProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
