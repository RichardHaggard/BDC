// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.CompositeStateNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public class CompositeStateNode : StateNodeBase
  {
    private LinkedList<StateNodeBase> states = new LinkedList<StateNodeBase>();

    public CompositeStateNode(string name)
      : base(name)
    {
    }

    public IEnumerable<StateNodeBase> States
    {
      get
      {
        return (IEnumerable<StateNodeBase>) this.states;
      }
    }

    public void AddState(StateNodeBase state)
    {
      this.states.AddLast(state);
    }

    public override ICollection<string> EvaluateState(RadObject targetItem)
    {
      ICollection<string> strings = (ICollection<string>) new LinkedList<string>();
      foreach (StateNodeBase state in this.states)
      {
        foreach (string str in (IEnumerable<string>) state.EvaluateState(targetItem))
          strings.Add(str);
      }
      return strings;
    }

    public override void AddAvailableStates(
      ICollection<StateDescriptionNode> newNodes,
      StateDescriptionNode node)
    {
      for (LinkedListNode<StateNodeBase> linkedListNode = this.states.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
      {
        LinkedList<StateDescriptionNode> linkedList1 = new LinkedList<StateDescriptionNode>();
        linkedListNode.Value.AddAvailableStates((ICollection<StateDescriptionNode>) linkedList1, node);
        LinkedListNode<StateNodeBase> next = linkedListNode.Next;
        foreach (StateDescriptionNode stateDescriptionNode in linkedList1)
          newNodes.Add(stateDescriptionNode);
        for (; next != null; next = next.Next)
        {
          LinkedList<StateDescriptionNode> linkedList2 = new LinkedList<StateDescriptionNode>();
          foreach (StateDescriptionNode node1 in linkedList1)
            next.Value.AddAvailableStates((ICollection<StateDescriptionNode>) linkedList2, node1);
          foreach (StateDescriptionNode stateDescriptionNode in linkedList2)
          {
            newNodes.Add(stateDescriptionNode);
            linkedList1.AddFirst(stateDescriptionNode);
          }
        }
      }
    }

    public override void AttachToElement(RadObject item, StateManagerAttachmentData attachmentData)
    {
      base.AttachToElement(item, attachmentData);
      if (this.states == null)
        return;
      foreach (StateNodeBase state in this.states)
        state.AttachToElement(item, attachmentData);
    }
  }
}
