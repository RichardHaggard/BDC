// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SeparatorElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SeparatorElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("IsVertical", (Condition) new SimpleCondition(SeparatorElement.OrientationProperty, (object) Orientation.Vertical));
      CompositeStateNode compositeStateNode = new CompositeStateNode("SeparatorElement item states");
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition);
      return (StateNodeBase) compositeStateNode;
    }

    protected override ItemStateManagerBase CreateStateManager()
    {
      ItemStateManagerBase stateManager = base.CreateStateManager();
      stateManager.AddDefaultVisibleState("IsVertical");
      return stateManager;
    }
  }
}
