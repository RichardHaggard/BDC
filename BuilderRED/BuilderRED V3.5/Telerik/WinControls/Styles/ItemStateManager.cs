// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.ItemStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public class ItemStateManager : ItemStateManagerBase
  {
    private StateNodeBase rootState;

    public ItemStateManager(StateNodeBase rootState)
    {
      this.rootState = rootState;
    }

    public StateNodeBase RootState
    {
      get
      {
        return this.rootState;
      }
    }

    protected override void AttachToItemOverride(
      StateManagerAttachmentData attachmentData,
      RadObject item)
    {
      if (this.rootState == null)
        return;
      this.rootState.AttachToElement(item, attachmentData);
    }

    public virtual void StateInvalidated(RadObject item, StateNodeBase elementStateBase)
    {
      ICollection<string> state = this.rootState.EvaluateState(item);
      if (state.Count == 0)
      {
        this.SetItemState(item, string.Empty);
      }
      else
      {
        string stateName = string.Empty;
        foreach (string stateName1 in (IEnumerable<string>) state)
          stateName = ItemStateManagerBase.CombineStateNames(stateName, stateName1);
        this.SetItemState(item, stateName);
      }
    }

    public override void ItemStateChanged(
      RadObject senderItem,
      RadPropertyChangedEventArgs changeArgs)
    {
      this.StateInvalidated(senderItem, this.rootState);
    }

    public override StateDescriptionNode GetAvailableStates(string themeRoleName)
    {
      StateDescriptionNode node = new StateDescriptionNode(themeRoleName);
      this.rootState.AddAvailableStates((ICollection<StateDescriptionNode>) new LinkedList<StateDescriptionNode>(), node);
      return node;
    }
  }
}
