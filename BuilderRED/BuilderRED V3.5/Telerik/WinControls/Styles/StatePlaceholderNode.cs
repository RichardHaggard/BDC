// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.StatePlaceholderNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public class StatePlaceholderNode : StateNodeBase
  {
    public StatePlaceholderNode(string name)
      : base(name)
    {
    }

    public override ICollection<string> EvaluateState(RadObject targetItem)
    {
      return (ICollection<string>) new string[1]{ this.Name };
    }

    public override void AddAvailableStates(
      ICollection<StateDescriptionNode> newNodes,
      StateDescriptionNode node)
    {
      newNodes.Add(node.AddNode(this.Name));
    }
  }
}
