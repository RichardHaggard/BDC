// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.StateNodeWithCondition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public class StateNodeWithCondition : StateNodeBase
  {
    private Condition condition;

    public Condition Condition
    {
      set
      {
        this.condition = value;
      }
      get
      {
        return this.condition;
      }
    }

    public StateNodeWithCondition(string name)
      : base(name)
    {
    }

    public StateNodeWithCondition(string name, Condition condition)
      : this(name)
    {
      this.condition = condition;
    }

    public override ICollection<string> EvaluateState(RadObject targetItem)
    {
      ICollection<string> strings = (ICollection<string>) new LinkedList<string>();
      if (this.Condition.Evaluate(targetItem))
      {
        if (this.TrueStateLink != null)
          strings = this.TrueStateLink.EvaluateState(targetItem);
        else
          strings.Add(this.Name);
      }
      else if (this.FalseStateLink != null)
        strings = this.FalseStateLink.EvaluateState(targetItem);
      return strings;
    }

    public override void AddAvailableStates(
      ICollection<StateDescriptionNode> newNodes,
      StateDescriptionNode node)
    {
      if (this.TrueStateLink != null)
        this.TrueStateLink.AddAvailableStates(newNodes, node);
      else
        newNodes.Add(node.AddNode(this.Name));
      if (this.FalseStateLink == null)
        return;
      this.FalseStateLink.AddAvailableStates(newNodes, node);
    }

    public override void AttachToElement(RadObject item, StateManagerAttachmentData attachmentData)
    {
      base.AttachToElement(item, attachmentData);
      if (this.condition == null)
        return;
      attachmentData.AddEventHandlers(this.condition.AffectedProperties);
    }
  }
}
