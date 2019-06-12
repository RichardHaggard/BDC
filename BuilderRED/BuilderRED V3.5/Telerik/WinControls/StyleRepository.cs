// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.StyleRepository
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class StyleRepository
  {
    private List<PropertySetting> settings;
    private string itemType;
    private string name;
    private string key;

    public StyleRepository()
    {
      this.settings = new List<PropertySetting>(1);
    }

    public StyleRepository(string key)
      : this()
    {
      this.key = key;
    }

    public StyleRepository(string itemType, string name, string key)
      : this()
    {
      this.itemType = itemType;
      this.name = name;
      this.key = key;
    }

    public StyleRepository(StyleRepository repository)
      : this()
    {
      this.itemType = repository.itemType;
      this.name = repository.name;
      this.key = repository.key;
      foreach (PropertySetting setting in repository.Settings)
        this.Settings.Add(new PropertySetting(setting));
    }

    public string ItemType
    {
      get
      {
        return this.itemType;
      }
      set
      {
        this.itemType = value;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public string Key
    {
      get
      {
        return this.key;
      }
      set
      {
        this.key = value;
      }
    }

    public List<PropertySetting> Settings
    {
      get
      {
        return this.settings;
      }
    }

    public void Initialize()
    {
      bool flag = true;
      PropertySetting setting1 = this.FindSetting("Visibility");
      if (setting1 != null)
        flag = (ElementVisibility) setting1.Value == ElementVisibility.Visible;
      if (this.ItemType == "Gradient")
      {
        PropertySetting setting2 = this.FindSetting("DrawFill");
        if (setting2 == null)
          this.Settings.Add(new PropertySetting("DrawFill", (object) flag));
        else
          setting2.Value = (object) flag;
      }
      else
      {
        if (!(this.ItemType == "Border"))
          return;
        PropertySetting setting2 = this.FindSetting("DrawBorder");
        if (setting2 == null)
          this.Settings.Add(new PropertySetting("DrawBorder", (object) flag));
        else
          setting2.Value = (object) flag;
      }
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

    public PropertySetting FindSetting(RadProperty property)
    {
      foreach (PropertySetting setting in this.settings)
      {
        if (setting.Property == property)
          return setting;
      }
      return (PropertySetting) null;
    }

    private void EnsureSetting(string name, PropertySetting setting)
    {
      if (setting == null || this.FindSetting(name) != null)
        return;
      this.settings.Add(new PropertySetting(setting));
    }
  }
}
