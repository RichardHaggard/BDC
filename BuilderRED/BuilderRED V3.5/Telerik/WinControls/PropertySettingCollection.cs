// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertySettingCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  [Serializable]
  public class PropertySettingCollection : ICollection<IPropertySetting>, IEnumerable<IPropertySetting>, IEnumerable
  {
    private List<IPropertySetting> innerList = new List<IPropertySetting>(0);
    private Dictionary<PropertySettingCollection.InheritedSetting, IPropertySetting> mappedSettings = new Dictionary<PropertySettingCollection.InheritedSetting, IPropertySetting>();
    private List<PropertySettingCollection.InheritedSetting> inheritedItems;

    public IList<IPropertySetting> OriginalPropertySettings
    {
      get
      {
        return (IList<IPropertySetting>) this.innerList;
      }
    }

    public void AddRange(IEnumerable<IPropertySetting> items)
    {
      this.innerList.AddRange(items);
    }

    internal void AddInheritedPropertySetting(IPropertySetting setting, string settingType)
    {
      lock (Locker.SyncObj)
      {
        if (this.inheritedItems == null)
          this.inheritedItems = new List<PropertySettingCollection.InheritedSetting>();
        this.inheritedItems.Add(new PropertySettingCollection.InheritedSetting(settingType, setting));
      }
    }

    internal void ResetInheritedProperties()
    {
      lock (Locker.SyncObj)
      {
        if (this.inheritedItems == null)
          return;
        this.inheritedItems = (List<PropertySettingCollection.InheritedSetting>) null;
        this.mappedSettings.Clear();
      }
    }

    public bool ContainsInheritedSetting(RadProperty property)
    {
      return this.FindInheritedSetting(property) != null;
    }

    public IPropertySetting FindInheritedSetting(RadProperty property)
    {
      lock (Locker.SyncObj)
      {
        if (this.inheritedItems == null)
          return (IPropertySetting) null;
        foreach (PropertySettingCollection.InheritedSetting inheritedItem in this.inheritedItems)
        {
          if (inheritedItem.property == property)
            return inheritedItem.setting;
        }
      }
      return (IPropertySetting) null;
    }

    public void Add(IPropertySetting item)
    {
      this.innerList.Add(item);
    }

    public bool ContainsSetting(RadProperty property)
    {
      return this.FindSetting(property) != null;
    }

    public IEnumerable<IPropertySetting> EnumLocalSettings()
    {
      foreach (IPropertySetting inner in this.innerList)
        yield return inner;
    }

    public IPropertySetting FindSetting(RadProperty property)
    {
      foreach (IPropertySetting inner in this.innerList)
      {
        if (inner.Property == property)
          return inner;
      }
      return (IPropertySetting) null;
    }

    public bool RemoveSetting(RadProperty property)
    {
      int count = this.innerList.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.innerList[index].Property == property)
        {
          this.innerList.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public void Clear()
    {
      this.innerList.Clear();
    }

    public bool Contains(IPropertySetting item)
    {
      return this.innerList.Contains(item);
    }

    public void CopyTo(IPropertySetting[] array, int arrayIndex)
    {
      this.innerList.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.innerList.Count + (this.inheritedItems != null ? this.inheritedItems.Count : 0);
      }
    }

    bool ICollection<IPropertySetting>.IsReadOnly
    {
      get
      {
        return ((ICollection<IPropertySetting>) this.innerList).IsReadOnly;
      }
    }

    public bool Remove(IPropertySetting item)
    {
      return this.innerList.Remove(item);
    }

    IEnumerator<IPropertySetting> IEnumerable<IPropertySetting>.GetEnumerator()
    {
      foreach (IPropertySetting propertySetting in this.EnumeratePropertySettingsForElement((RadElement) null))
        yield return propertySetting;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      foreach (IPropertySetting propertySetting in this.EnumeratePropertySettingsForElement((RadElement) null))
        yield return (object) propertySetting;
    }

    internal IEnumerable<IPropertySetting> EnumeratePropertySettingsForElement(
      RadElement element)
    {
      return this.EnumeratePropertySettingsForElement(element, true, true);
    }

    internal IEnumerable<IPropertySetting> EnumeratePropertySettingsForElement(
      RadElement element,
      bool inherited,
      bool local)
    {
      lock (Locker.SyncObj)
      {
        if (this.inheritedItems != null && inherited)
        {
          foreach (PropertySettingCollection.InheritedSetting inheritedItem in this.inheritedItems)
          {
            IPropertySetting newSetting = this.MapProperty(inheritedItem, element);
            if (newSetting != null)
              yield return newSetting;
            else
              yield return inheritedItem.setting;
          }
        }
        if (local)
        {
          foreach (IPropertySetting inner in this.innerList)
            yield return inner;
        }
      }
    }

    private IPropertySetting MapProperty(
      PropertySettingCollection.InheritedSetting inheritedSetting,
      RadElement element)
    {
      if (element == null)
        return (IPropertySetting) null;
      RadProperty property = element.MapStyleProperty(inheritedSetting.property, inheritedSetting.settingType);
      if (property == null)
        return (IPropertySetting) null;
      PropertySettingCollection.InheritedSetting key = new PropertySettingCollection.InheritedSetting(inheritedSetting.settingType, property);
      IPropertySetting propertySetting;
      if (!this.mappedSettings.TryGetValue(key, out propertySetting))
      {
        lock (Locker.SyncObj)
        {
          if (!this.mappedSettings.TryGetValue(key, out propertySetting))
          {
            propertySetting.Property = property;
            this.mappedSettings[key] = propertySetting;
          }
        }
      }
      return propertySetting;
    }

    private struct InheritedSetting
    {
      public string settingType;
      public IPropertySetting setting;
      public RadProperty property;
      private int hash;

      public InheritedSetting(string type, IPropertySetting setting)
      {
        this.settingType = type;
        this.setting = setting;
        this.property = setting.Property;
        this.hash = this.property.NameHash;
        if (string.IsNullOrEmpty(type))
          return;
        this.hash ^= type.GetHashCode();
      }

      public InheritedSetting(string type, RadProperty property)
      {
        this.settingType = type;
        this.setting = (IPropertySetting) null;
        this.property = property;
        this.hash = this.property.NameHash;
        if (string.IsNullOrEmpty(type))
          return;
        this.hash ^= type.GetHashCode();
      }

      public override int GetHashCode()
      {
        return this.hash;
      }

      public override bool Equals(object obj)
      {
        PropertySettingCollection.InheritedSetting inheritedSetting = (PropertySettingCollection.InheritedSetting) obj;
        if (this.settingType == inheritedSetting.settingType)
          return this.property == inheritedSetting.property;
        return false;
      }
    }

    public struct PropertySettingsCollectionEnumerator : IEnumerator<IPropertySetting>, IDisposable, IEnumerator
    {
      private IEnumerator<IPropertySetting> ownerEnumerator;
      private IEnumerator<IPropertySetting> inheritedPropertiesEnumerator;
      private IEnumerator<IPropertySetting> activeEnumerator;

      public PropertySettingsCollectionEnumerator(PropertySettingCollection owner)
      {
        this.ownerEnumerator = ((IEnumerable<IPropertySetting>) owner.innerList).GetEnumerator();
        this.activeEnumerator = this.ownerEnumerator;
        if (owner.inheritedItems != null)
          this.inheritedPropertiesEnumerator = ((IEnumerable<IPropertySetting>) owner.inheritedItems).GetEnumerator();
        else
          this.inheritedPropertiesEnumerator = (IEnumerator<IPropertySetting>) null;
      }

      public IPropertySetting Current
      {
        get
        {
          return this.activeEnumerator.Current;
        }
      }

      public void Dispose()
      {
        this.ownerEnumerator.Dispose();
        if (this.inheritedPropertiesEnumerator == null)
          return;
        this.inheritedPropertiesEnumerator.Dispose();
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public bool MoveNext()
      {
        bool flag = this.activeEnumerator.MoveNext();
        if (!flag && this.activeEnumerator == this.ownerEnumerator && this.inheritedPropertiesEnumerator != null)
        {
          this.activeEnumerator = this.inheritedPropertiesEnumerator;
          flag = this.inheritedPropertiesEnumerator.MoveNext();
        }
        return flag;
      }

      public void Reset()
      {
        this.ownerEnumerator.Reset();
        if (this.inheritedPropertiesEnumerator == null)
          return;
        this.inheritedPropertiesEnumerator.Reset();
      }
    }
  }
}
