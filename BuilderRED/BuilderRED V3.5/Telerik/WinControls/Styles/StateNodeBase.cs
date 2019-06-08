// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.StateNodeBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public abstract class StateNodeBase
  {
    private string name;
    private StateNodeBase trueStateLink;
    private StateNodeBase falseStateLink;

    public StateNodeBase FalseStateLink
    {
      get
      {
        return this.falseStateLink;
      }
      set
      {
        this.falseStateLink = value;
      }
    }

    public StateNodeBase TrueStateLink
    {
      get
      {
        return this.trueStateLink;
      }
      set
      {
        this.trueStateLink = value;
      }
    }

    public StateNodeBase(string stateName)
    {
      this.name = stateName;
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public abstract ICollection<string> EvaluateState(RadObject targetItem);

    public abstract void AddAvailableStates(
      ICollection<StateDescriptionNode> newNodes,
      StateDescriptionNode node);

    public virtual void AttachToElement(RadObject item, StateManagerAttachmentData attachmentData)
    {
      if (this.trueStateLink != null)
        this.trueStateLink.AttachToElement(item, attachmentData);
      if (this.falseStateLink == null)
        return;
      this.falseStateLink.AttachToElement(item, attachmentData);
    }
  }
}
