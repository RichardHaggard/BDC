// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.OverflowAssociatedButtonStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class OverflowAssociatedButtonStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      return (StateNodeBase) new StateNodeWithCondition("AssociatedItemSelected", (Condition) new SimpleCondition(OverflowItemsContainer.ItemSelectedProperty, (object) true));
    }
  }
}
