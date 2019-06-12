// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertySettingGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class PropertySettingGroup
  {
    public const char BasedOnDelimiter = ',';
    private string basedOn;
    private ElementSelector selector;
    private List<PropertySetting> settings;
    private List<StyleRepository> repositories;

    public PropertySettingGroup()
    {
      this.settings = new List<PropertySetting>(4);
      this.repositories = new List<StyleRepository>(1);
    }

    public PropertySettingGroup(string basedOn)
      : this()
    {
      this.basedOn = basedOn;
    }

    public PropertySettingGroup(string basedOn, ElementSelector selector)
      : this(basedOn)
    {
      this.selector = selector;
    }

    public PropertySettingGroup(string className, params PropertySetting[] propertySettings)
      : this()
    {
      this.selector = (ElementSelector) new WrapSelector((IElementSelector) new ClassSelector(className));
      this.settings.AddRange((IEnumerable<PropertySetting>) propertySettings);
    }

    public PropertySettingGroup(
      string className,
      Condition condition,
      params PropertySetting[] propertySettings)
      : this()
    {
      this.selector = (ElementSelector) new WrapSelector((IElementSelector) new ClassSelector(className, condition));
      this.settings.AddRange((IEnumerable<PropertySetting>) propertySettings);
    }

    public PropertySettingGroup(Type type, params PropertySetting[] propertySettings)
      : this()
    {
      this.selector = (ElementSelector) new WrapSelector((IElementSelector) new TypeSelector(type));
      this.settings.AddRange((IEnumerable<PropertySetting>) propertySettings);
    }

    public PropertySettingGroup(
      Type type,
      Condition condition,
      params PropertySetting[] propertySettings)
      : this()
    {
      this.selector = (ElementSelector) new WrapSelector((IElementSelector) new TypeSelector(type, condition));
      this.settings.AddRange((IEnumerable<PropertySetting>) propertySettings);
    }

    public PropertySettingGroup(PropertySettingGroup sourceGroup)
      : this()
    {
      this.basedOn = sourceGroup.basedOn;
      foreach (PropertySetting propertySetting in sourceGroup.PropertySettings)
        this.PropertySettings.Add(new PropertySetting(propertySetting));
      this.selector = new ElementSelector(sourceGroup.Selector);
    }

    public string BasedOn
    {
      get
      {
        return this.basedOn;
      }
      set
      {
        this.basedOn = value;
      }
    }

    public ElementSelector Selector
    {
      get
      {
        return this.selector;
      }
      set
      {
        this.selector = value;
      }
    }

    public List<PropertySetting> PropertySettings
    {
      get
      {
        return this.settings;
      }
    }

    public List<StyleRepository> Repositories
    {
      get
      {
        return this.repositories;
      }
    }

    public void Apply(RadObject element)
    {
      lock (this.repositories)
      {
        foreach (StyleRepository repository in this.Repositories)
        {
          foreach (PropertySetting setting in repository.Settings)
            setting.ApplyValue(element);
        }
      }
      foreach (PropertySetting propertySetting in this.PropertySettings)
        propertySetting.ApplyValue(element);
    }

    public PropertySetting FindSetting(RadProperty property)
    {
      foreach (PropertySetting setting in this.settings)
      {
        if (setting.Property == property)
          return setting;
      }
      return (PropertySetting) null;
    }

    public PropertySetting FindSetting(string name)
    {
      foreach (PropertySetting setting in this.settings)
      {
        if (setting.Name == name)
          return setting;
      }
      return (PropertySetting) null;
    }

    public string[] GetBasedOnRepositoryItems()
    {
      if (string.IsNullOrEmpty(this.basedOn))
        return new string[0];
      string[] strArray = this.basedOn.Trim().Split(',');
      int length = strArray.Length;
      for (int index = 0; index < length; ++index)
        strArray[index] = strArray[index].Trim();
      return strArray;
    }

    public bool IsBasedOnRepositoryItem(string itemKey)
    {
      foreach (string onRepositoryItem in this.GetBasedOnRepositoryItems())
      {
        if (onRepositoryItem == itemKey)
          return true;
      }
      return false;
    }

    public void AssociateWithRepositoryItem(string key)
    {
      if (string.IsNullOrEmpty(key) || this.IsBasedOnRepositoryItem(key))
        return;
      if (string.IsNullOrEmpty(this.basedOn))
      {
        this.basedOn = key;
      }
      else
      {
        PropertySettingGroup propertySettingGroup = this;
        propertySettingGroup.basedOn = propertySettingGroup.basedOn + (object) ',' + key;
      }
    }

    public void ResetRepositoryItemAssociation(string itemKey)
    {
      if (string.IsNullOrEmpty(this.basedOn))
        return;
      if (string.IsNullOrEmpty(itemKey))
      {
        this.Repositories.Clear();
      }
      else
      {
        string[] onRepositoryItems = this.GetBasedOnRepositoryItems();
        int num = -1;
        int startIndex = 0;
        for (int index = 0; index < onRepositoryItems.Length; ++index)
        {
          if (onRepositoryItems[index] == itemKey)
          {
            num = index;
            break;
          }
          startIndex += onRepositoryItems[index].Length + 1;
        }
        if (num == -1)
          return;
        int length = itemKey.Length;
        if (onRepositoryItems.Length > 1)
        {
          if (num == onRepositoryItems.Length - 1)
          {
            --startIndex;
            ++length;
          }
          else
            ++length;
        }
        this.basedOn = this.basedOn.Remove(startIndex, length);
        for (int index = this.repositories.Count - 1; index >= 0; --index)
        {
          if (this.repositories[index].Key == itemKey)
          {
            this.repositories.RemoveAt(index);
            break;
          }
        }
      }
    }
  }
}
