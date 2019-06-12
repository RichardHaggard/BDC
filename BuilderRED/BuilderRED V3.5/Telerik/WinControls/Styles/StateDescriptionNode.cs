// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.StateDescriptionNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public class StateDescriptionNode
  {
    private List<StateDescriptionNode> nodes = new List<StateDescriptionNode>(0);
    private string stateName;

    public virtual string StateName
    {
      get
      {
        return this.stateName;
      }
    }

    public StateDescriptionNode(string nodeStateName)
    {
      this.stateName = nodeStateName;
    }

    public List<StateDescriptionNode> Nodes
    {
      get
      {
        return this.nodes;
      }
    }

    public IEnumerable<string> GetStatesEnumerator()
    {
      return this.GetStatesEnumerator(string.Empty);
    }

    public IEnumerable<string> GetStatesEnumerator(string parentNodeStateName)
    {
      if (parentNodeStateName != string.Empty)
      {
        // ISSUE: reference to a compiler-generated field
        this.parentNodeStateName += (string) (object) '.';
      }
      string nodeFullName = parentNodeStateName + this.StateName;
      yield return nodeFullName;
      foreach (StateDescriptionNode node in this.nodes)
      {
        foreach (string str in node.GetStatesEnumerator(nodeFullName))
          yield return str;
      }
    }

    public StateDescriptionNode AddNode(string stateName)
    {
      StateDescriptionNode stateDescriptionNode = new StateDescriptionNode(stateName);
      this.nodes.Add(stateDescriptionNode);
      return stateDescriptionNode;
    }

    public override string ToString()
    {
      return this.StateName;
    }
  }
}
