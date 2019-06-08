// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuItemBaseStateManagerFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadMenuItemBaseStateManagerFactory : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Selected");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("Selected", (Condition) new SimpleCondition(RadMenuItemBase.SelectedProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("IsPopupShown", (Condition) new SimpleCondition(RadMenuItemBase.IsPopupShownProperty, (object) true));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      return (StateNodeBase) compositeStateNode;
    }

    protected override void AddDefaultVisibleStates(ItemStateManager sm)
    {
      base.AddDefaultVisibleStates(sm);
      sm.AddDefaultVisibleState("Selected");
      sm.AddDefaultVisibleState("Selected.IsPopupShown");
    }
  }
}
