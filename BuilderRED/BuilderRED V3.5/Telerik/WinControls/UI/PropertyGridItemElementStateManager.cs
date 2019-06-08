// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemElementStateManager : PropertyGridItemElementBaseStateManager
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode specificStates = (CompositeStateNode) base.CreateSpecificStates();
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("IsChildItem", (Condition) new SimpleCondition(PropertyGridItemElement.IsChildItemProperty, (object) true));
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsModified", (Condition) new SimpleCondition(PropertyGridItemElement.IsModifiedProperty, (object) true));
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("IsReadOnly", (Condition) new SimpleCondition(PropertyGridItemElement.IsReadOnlyProperty, (object) true));
      specificStates.AddState((StateNodeBase) nodeWithCondition1);
      specificStates.AddState((StateNodeBase) nodeWithCondition2);
      specificStates.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) specificStates;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("IsChildItem");
      sm.AddDefaultVisibleState("IsModified");
      sm.AddDefaultVisibleState("IsReadOnly");
    }
  }
}
