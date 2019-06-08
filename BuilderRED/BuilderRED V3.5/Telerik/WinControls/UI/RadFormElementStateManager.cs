// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadFormElementStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      return (StateNodeBase) new StateNodeWithCondition("IsFormActive", (Condition) new SimpleCondition(RadFormElement.IsFormActiveProperty, (object) true));
    }
  }
}
