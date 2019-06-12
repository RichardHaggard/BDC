// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxElementStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadTextBoxElementStateManager : EditorElementStateManager
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      StateNodeWithCondition nodeWithCondition = new StateNodeWithCondition("IsNullText", (Condition) new SimpleCondition(RadTextBoxItem.IsNullTextProperty, (object) true));
      CompositeStateNode specificStates = (CompositeStateNode) base.CreateSpecificStates();
      specificStates.AddState((StateNodeBase) nodeWithCondition);
      return (StateNodeBase) specificStates;
    }
  }
}
