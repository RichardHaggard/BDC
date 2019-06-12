// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupFieldElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GroupFieldElementStateManager : ItemStateManagerFactoryBase
  {
    protected override ItemStateManagerBase CreateStateManager()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("Mouse states");
      compositeStateNode.AddState((StateNodeBase) new StateNodeWithCondition("ContainsMouse", (Condition) new SimpleCondition(RadElement.ContainsMouseProperty, (object) true)));
      compositeStateNode.AddState((StateNodeBase) new StateNodeWithCondition("MouseOver", (Condition) new SimpleCondition(RadElement.IsMouseOverProperty, (object) true)));
      compositeStateNode.AddState((StateNodeBase) new StateNodeWithCondition("MouseDown", (Condition) new SimpleCondition(RadElement.IsMouseDownProperty, (object) true)));
      ItemStateManager itemStateManager = new ItemStateManager((StateNodeBase) compositeStateNode);
      itemStateManager.AddDefaultVisibleState("MouseOver");
      itemStateManager.AddDefaultVisibleState("MouseDown");
      return (ItemStateManagerBase) itemStateManager;
    }
  }
}
