// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ThemeSettingsOverrideCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls
{
  internal class ThemeSettingsOverrideCollection : ObservableCollection<ThemeSettingOverride>
  {
    public void ApplySettings(IStylableElement element)
    {
      List<ThemeSettingOverride> list = new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this);
      ThemeSettingsOverrideCollection.BubbleSort<ThemeSettingOverride>(list, (Comparison<ThemeSettingOverride>) ((a, b) => a.StatesCount.CompareTo(b.StatesCount)));
      foreach (ThemeSettingOverride themeSettingOverride in list)
      {
        bool isChildSetting = themeSettingOverride.IsChildSetting;
        bool flag = themeSettingOverride.IsVisualStateCompatible(element);
        if (!isChildSetting && flag)
          themeSettingOverride.ApplyValue((IStylableNode) element);
        else if (isChildSetting && flag)
        {
          foreach (IStylableNode stylableNode in element.ChildrenHierarchy)
          {
            if (themeSettingOverride.IsChildElementCompatible(stylableNode))
              themeSettingOverride.ApplyValue(stylableNode);
          }
        }
      }
    }

    private static void BubbleSort<T>(List<T> list, Comparison<T> comparison)
    {
      bool flag = true;
      while (flag)
      {
        flag = false;
        for (int index = 0; index < list.Count - 1; ++index)
        {
          if (comparison(list[index], list[index + 1]) > 0)
          {
            T obj = list[index];
            list[index] = list[index + 1];
            list[index + 1] = obj;
            flag = true;
          }
        }
      }
    }

    public void RemoveSetting(RadProperty property)
    {
      foreach (ThemeSettingOverride themeSettingOverride in new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this))
      {
        if (themeSettingOverride.Property == property)
          this.Remove(themeSettingOverride);
      }
    }

    internal void RemoveSetting(RadProperty property, RadItem radItem)
    {
      foreach (ThemeSettingOverride setting in new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this))
      {
        if (setting.Property == property)
          this.RemoveSettingCore(radItem, setting);
      }
    }

    public void RemoveSetting(RadProperty property, string state)
    {
      foreach (ThemeSettingOverride themeSettingOverride in new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this))
      {
        if (themeSettingOverride.Property == property && themeSettingOverride.VisualState == state)
        {
          this.Remove(themeSettingOverride);
          themeSettingOverride.Dispose();
        }
      }
    }

    internal void RemoveSetting(RadProperty property, string state, RadItem radItem)
    {
      foreach (ThemeSettingOverride setting in new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this))
      {
        if (setting.Property == property && setting.VisualState == state)
          this.RemoveSettingCore(radItem, setting);
      }
    }

    internal void RemoveSetting(
      RadProperty property,
      string state,
      RadItem radItem,
      string childElementClass)
    {
      foreach (ThemeSettingOverride setting in new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this))
      {
        if (setting.Property == property && setting.VisualState == state && (setting.IsChildSetting && setting.ChildElementClass == childElementClass))
          this.RemoveSettingCore(radItem, setting);
      }
    }

    internal void RemoveSetting(
      RadProperty property,
      string state,
      RadItem radItem,
      Type childElementType)
    {
      foreach (ThemeSettingOverride setting in new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this))
      {
        if (setting.Property == property && setting.VisualState == state && (setting.IsChildSetting && (object) setting.ChildElementType == (object) childElementType))
          this.RemoveSettingCore(radItem, setting);
      }
    }

    internal void RemoveAllSettings(RadItem radItem)
    {
      foreach (ThemeSettingOverride setting in new List<ThemeSettingOverride>((IEnumerable<ThemeSettingOverride>) this))
        this.RemoveSettingCore(radItem, setting);
    }

    private void RemoveSettingCore(RadItem radItem, ThemeSettingOverride setting)
    {
      setting.PropertySetting.UnapplyValue((RadObject) radItem);
      this.Remove(setting);
      setting.Dispose();
    }
  }
}
