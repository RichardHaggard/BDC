﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonBarSystemButtonStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  internal class RibbonBarSystemButtonStateManager : ItemStateManagerFactory
  {
    protected override StateNodeBase CreateSpecificStates()
    {
      CompositeStateNode compositeStateNode = new CompositeStateNode("FormStates");
      StateNodeWithCondition nodeWithCondition1 = new StateNodeWithCondition("FormMaximized", (Condition) new SimpleCondition(RadRibbonBarElement.RibbonFormWindowStateProperty, (object) FormWindowState.Maximized));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition1);
      StateNodeWithCondition nodeWithCondition2 = new StateNodeWithCondition("FormNormal", (Condition) new SimpleCondition(RadRibbonBarElement.RibbonFormWindowStateProperty, (object) FormWindowState.Normal));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition2);
      StateNodeWithCondition nodeWithCondition3 = new StateNodeWithCondition("FormMinimized", (Condition) new SimpleCondition(RadRibbonBarElement.RibbonFormWindowStateProperty, (object) FormWindowState.Minimized));
      compositeStateNode.AddState((StateNodeBase) nodeWithCondition3);
      return (StateNodeBase) compositeStateNode;
    }
  }
}
