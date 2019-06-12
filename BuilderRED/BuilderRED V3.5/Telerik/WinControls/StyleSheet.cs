// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.StyleSheet
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class StyleSheet
  {
    private List<PropertySettingGroup> settings;

    public StyleSheet()
    {
      this.settings = new List<PropertySettingGroup>(100);
    }

    public List<PropertySettingGroup> PropertySettingGroups
    {
      get
      {
        return this.settings;
      }
    }

    public void Apply(RadObject radObject, bool initializing)
    {
      IStylableElement stylable = radObject as IStylableElement;
      if (stylable != null)
      {
        if (!this.ApplySimpleSettings(stylable))
          this.ApplyVisualStateSettings(stylable, initializing);
      }
      else
      {
        IStylableNode stylableNode = radObject as IStylableNode;
        if (stylableNode != null)
        {
          foreach (PropertySettingGroup setting in this.settings)
          {
            if (setting.Selector == null || setting.Selector.IsCompatible(radObject))
              stylableNode.ApplySettings(setting);
          }
        }
      }
      if (!(radObject is RadItem))
        return;
      ((RadItem) radObject).ApplyThemeSettingsOverride();
    }

    private bool ApplySimpleSettings(IStylableElement stylable)
    {
      RadObject testElement = stylable as RadObject;
      List<PropertySettingGroup> propertySettingGroupList = (List<PropertySettingGroup>) null;
      foreach (PropertySettingGroup setting in this.settings)
      {
        if (setting.Selector == null)
          stylable.ApplySettings(setting);
        else if (setting.Selector.Type != ElementSelectorTypes.VisualStateSelector)
        {
          ElementSelector selector = setting.Selector;
          if (setting.Selector.IsValid(testElement, string.Empty))
          {
            if (setting.Selector.ChildSelector == null)
            {
              stylable.ApplySettings(setting);
              selector = (ElementSelector) null;
            }
            else
              selector = setting.Selector.ChildSelector;
          }
          else if (setting.Selector.ChildSelector != null && setting.Selector.ChildSelector.Type == ElementSelectorTypes.VisualStateSelector)
          {
            if (propertySettingGroupList == null)
              propertySettingGroupList = new List<PropertySettingGroup>();
            propertySettingGroupList.Add(setting);
            continue;
          }
          if (selector != null)
            this.ApplySimpleSelectorToChildren((IStylableNode) stylable, setting, selector);
        }
      }
      if (propertySettingGroupList != null)
      {
        foreach (string stateFallback in this.GetStateFallbackList(stylable))
        {
          bool flag = false;
          foreach (PropertySettingGroup group in propertySettingGroupList)
          {
            if (group.Selector.ChildSelector.IsValid(testElement, stateFallback))
            {
              stylable.ApplySettings(group);
              flag = true;
              break;
            }
          }
          if (flag)
            return true;
        }
      }
      return false;
    }

    private bool ApplySimpleSelectorToChildren(
      IStylableNode stylable,
      PropertySettingGroup group,
      ElementSelector selector)
    {
      foreach (RadObject child in stylable.Children)
      {
        if (selector.IsCompatible(child))
        {
          (child as IStylableNode).ApplySettings(group);
          return true;
        }
        if (this.ApplySimpleSelectorToChildren(child as IStylableNode, group, selector))
          return true;
      }
      return false;
    }

    private void ApplyVisualStateSettings(IStylableElement stylable, bool initializing)
    {
      RadObject testElement = stylable as RadObject;
      if (initializing && stylable.VisualState != stylable.ThemeRole)
      {
        foreach (PropertySettingGroup setting in this.settings)
        {
          if (setting.Selector == null || setting.Selector.Type == ElementSelectorTypes.VisualStateSelector && setting.Selector.IsValid(testElement, stylable.ThemeRole))
          {
            if (setting.Selector.ChildSelector == null)
              stylable.ApplySettings(setting);
            else if (setting.Selector.ChildSelector.Type != ElementSelectorTypes.VisualStateSelector || setting.Selector.ChildSelector.IsRecursive)
              this.ApplyChildVisualStateSelector(stylable, setting);
          }
        }
      }
      foreach (string stateFallback in this.GetStateFallbackList(stylable))
      {
        bool flag = false;
        foreach (PropertySettingGroup setting in this.settings)
        {
          if (setting.Selector != null && setting.Selector.Type == ElementSelectorTypes.VisualStateSelector && setting.Selector.IsValid(testElement, stateFallback))
          {
            if (setting.Selector.ChildSelector == null)
              stylable.ApplySettings(setting);
            else if (setting.Selector.ChildSelector.Type != ElementSelectorTypes.VisualStateSelector)
              this.ApplyChildVisualStateSelector(stylable, setting);
            flag = true;
          }
        }
        if (flag)
          break;
      }
    }

    private void ApplyChildVisualStateSelector(
      IStylableElement stylable,
      PropertySettingGroup group)
    {
      foreach (RadObject testElement in stylable.ChildrenHierarchy)
      {
        if (group.Selector.ChildSelector.Type == ElementSelectorTypes.VisualStateSelector)
        {
          IStylableElement stylableElement = testElement as IStylableElement;
          if (stylableElement != null)
          {
            bool flag = false;
            foreach (string stateFallback in this.GetStateFallbackList(stylableElement))
            {
              if (group.Selector.ChildSelector.IsValid(testElement, stateFallback))
              {
                stylableElement.ApplySettings(group);
                flag = true;
                break;
              }
            }
            if (flag)
              break;
          }
        }
        else if (group.Selector.ChildSelector.IsValid(testElement, string.Empty))
        {
          (testElement as IStylableNode).ApplySettings(group);
          break;
        }
      }
    }

    private LinkedList<string> GetStateFallbackList(IStylableElement item)
    {
      LinkedList<string> linkedList = new LinkedList<string>();
      string visualState = item.VisualState;
      if (string.IsNullOrEmpty(visualState))
        return linkedList;
      string[] strArray = visualState.Split('.');
      if (strArray.Length < 2)
      {
        linkedList.AddLast(visualState);
        return linkedList;
      }
      for (int index = strArray.Length - 1; index > 0; --index)
      {
        for (int startIndex = 1; startIndex <= index; ++startIndex)
          linkedList.AddLast(item.ThemeRole + "." + string.Join(".", strArray, startIndex, index - startIndex + 1));
      }
      linkedList.AddLast(strArray[0]);
      return linkedList;
    }
  }
}
