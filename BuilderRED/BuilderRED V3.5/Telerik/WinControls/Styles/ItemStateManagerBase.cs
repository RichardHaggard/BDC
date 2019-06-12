// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.ItemStateManagerBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls.Styles
{
  public abstract class ItemStateManagerBase
  {
    private static string stateDelimiterString = new string('.', 1);
    private LinkedList<string> defaultVisibleStates = new LinkedList<string>();
    public const char stateDelimiter = '.';

    public virtual string GetInitialState(RadObject item)
    {
      IStylableElement stylableElement = item as IStylableElement;
      if (stylableElement != null)
        return stylableElement.ThemeRole;
      return item.GetType().Name;
    }

    protected void SetItemState(RadObject item, params string[] stateNames)
    {
      if (item == null)
        return;
      IStylableElement stylableElement = item as IStylableElement;
      if (stylableElement == null)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(stylableElement.ThemeRole);
      if (stateNames != null && stateNames.Length > 0 && stateNames[0].Length > 0)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append('.');
        stringBuilder.Append(string.Join(ItemStateManagerBase.stateDelimiterString, stateNames));
      }
      stylableElement.VisualState = stringBuilder.ToString();
    }

    public virtual StateManagerAttachmentData AttachToItem(RadObject item)
    {
      StateManagerAttachmentData attachData = new StateManagerAttachmentData(item, new RadItemStateChangedEventHandler(this.ItemStateChanged));
      this.AttachToItemOverride(attachData, item);
      this.ItemStateChanged(item, (RadPropertyChangedEventArgs) null);
      return attachData;
    }

    public virtual void Detach(StateManagerAttachmentData data)
    {
      data?.Dispose();
    }

    public bool VerifyState(string state)
    {
      return this.VerifyState(string.Empty, 46.ToString() + state);
    }

    public virtual bool VerifyState(string themeRoleName, string key)
    {
      string[] stateNames = key.Split('.');
      StateDescriptionNode availableStates = this.GetAvailableStates(themeRoleName);
      if (stateNames.Length == 0 || availableStates.StateName != stateNames[0])
        return false;
      return this.MatchDescriptionNodeToState(availableStates, stateNames, 1);
    }

    private bool MatchDescriptionNodeToState(
      StateDescriptionNode rootState,
      string[] stateNames,
      int stateNamesIndex)
    {
      if (stateNamesIndex >= stateNames.Length)
        return true;
      string stateName = stateNames[stateNamesIndex];
      StateDescriptionNode rootState1 = (StateDescriptionNode) null;
      foreach (StateDescriptionNode node in rootState.Nodes)
      {
        if (node.StateName == stateName)
        {
          rootState1 = node;
          break;
        }
      }
      if (rootState1 == null)
        return false;
      return this.MatchDescriptionNodeToState(rootState1, stateNames, stateNamesIndex + 1);
    }

    public IEnumerable<string> DefaultVisibleStates
    {
      get
      {
        return (IEnumerable<string>) this.defaultVisibleStates;
      }
    }

    public void AddDefaultVisibleState(string state)
    {
      if (this.defaultVisibleStates == null)
        this.defaultVisibleStates = new LinkedList<string>();
      if (!this.VerifyState(state))
        throw new InvalidOperationException(string.Format("Default state added for {0} that is not recognized by StateManager - {1}", (object) state, (object) this));
      this.defaultVisibleStates.AddLast(state);
    }

    public void RemoveDefaultVisibleState(string state)
    {
      if (this.defaultVisibleStates == null)
        return;
      if (!this.VerifyState(state))
        throw new InvalidOperationException(string.Format("Default state added for {0} that is not recognized by StateManager - {1}", (object) state, (object) this));
      this.defaultVisibleStates.Remove(state);
    }

    public IEnumerable<string> GetStateFallbackList(RadItem item)
    {
      LinkedList<string> linkedList = new LinkedList<string>();
      string visualState = item.VisualState;
      if (string.IsNullOrEmpty(visualState))
        return (IEnumerable<string>) linkedList;
      string[] strArray = visualState.Split('.');
      if (strArray.Length < 2)
      {
        linkedList.AddLast(visualState);
        return (IEnumerable<string>) linkedList;
      }
      for (int index = strArray.Length - 1; index > 0; --index)
      {
        for (int startIndex = 1; startIndex <= index; ++startIndex)
          linkedList.AddLast(item.ThemeRole + ItemStateManagerBase.stateDelimiterString + string.Join(ItemStateManagerBase.stateDelimiterString, strArray, startIndex, index - startIndex + 1));
      }
      linkedList.AddLast(strArray[0]);
      return (IEnumerable<string>) linkedList;
    }

    public string GetStateFullName(string itemThemeRole, string stateName)
    {
      if (string.IsNullOrEmpty(stateName))
        return itemThemeRole;
      return itemThemeRole + ItemStateManagerBase.stateDelimiterString + stateName;
    }

    public static string CombineStateNames(string stateName, string stateName1)
    {
      string str = stateName;
      if (!string.IsNullOrEmpty(stateName1))
      {
        if (!string.IsNullOrEmpty(str))
          str += (string) (object) '.';
        str += stateName1;
      }
      return str;
    }

    public abstract void ItemStateChanged(
      RadObject senderItem,
      RadPropertyChangedEventArgs changeArgs);

    protected abstract void AttachToItemOverride(
      StateManagerAttachmentData attachData,
      RadObject item);

    public abstract StateDescriptionNode GetAvailableStates(string themeRoleName);
  }
}
