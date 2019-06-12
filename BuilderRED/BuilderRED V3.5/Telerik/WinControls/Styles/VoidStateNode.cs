﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.VoidStateNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public class VoidStateNode : StateNodeBase
  {
    public VoidStateNode()
      : base("No State")
    {
    }

    public override ICollection<string> EvaluateState(RadObject targetItem)
    {
      return (ICollection<string>) new string[0];
    }

    public override void AddAvailableStates(
      ICollection<StateDescriptionNode> newNodes,
      StateDescriptionNode node)
    {
    }
  }
}
