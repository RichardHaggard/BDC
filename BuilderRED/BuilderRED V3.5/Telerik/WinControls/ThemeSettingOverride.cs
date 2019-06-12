// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ThemeSettingOverride
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  internal class ThemeSettingOverride : IDisposable
  {
    private RadProperty property;
    private string visualState;
    private object value;
    private string childElementClass;
    private Type childElementType;
    private PropertySetting propertySetting;
    private string[] states;

    public RadProperty Property
    {
      get
      {
        return this.property;
      }
    }

    public string VisualState
    {
      get
      {
        return this.visualState;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
    }

    public string ChildElementClass
    {
      get
      {
        return this.childElementClass;
      }
    }

    public Type ChildElementType
    {
      get
      {
        return this.childElementType;
      }
    }

    internal int StatesCount
    {
      get
      {
        if (this.states == null)
          this.states = this.visualState.Split('.');
        if (this.states != null)
          return this.states.Length;
        return !string.IsNullOrEmpty(this.visualState) ? 1 : 0;
      }
    }

    internal PropertySetting PropertySetting
    {
      get
      {
        return this.propertySetting;
      }
      set
      {
        this.propertySetting = value;
      }
    }

    public bool IsChildSetting
    {
      get
      {
        if (string.IsNullOrEmpty(this.childElementClass))
          return (object) this.childElementType != null;
        return true;
      }
    }

    public ThemeSettingOverride(RadProperty property, object value, string visualState)
    {
      this.property = property;
      this.visualState = visualState;
      this.value = value;
      this.propertySetting = new PropertySetting(this.property, this.value);
    }

    public ThemeSettingOverride(
      RadProperty property,
      object value,
      string visualState,
      string childElementClass)
      : this(property, value, visualState)
    {
      this.childElementClass = childElementClass;
    }

    public ThemeSettingOverride(
      RadProperty property,
      object value,
      string visualState,
      Type childElementType)
      : this(property, value, visualState)
    {
      this.childElementType = childElementType;
    }

    public bool IsChildElementCompatible(IStylableNode childElement)
    {
      if (!string.IsNullOrEmpty(this.childElementClass))
        return childElement.Class == this.childElementClass;
      if ((object) this.childElementType != null)
        return (object) childElement.GetThemeEffectiveType() == (object) this.childElementType;
      return false;
    }

    public bool IsVisualStateCompatible(IStylableElement stylable)
    {
      if (!this.visualState.Contains("."))
      {
        if (this.visualState == string.Empty)
          return stylable.ThemeRole == stylable.VisualState;
        return stylable.VisualState.Contains(this.visualState);
      }
      if (this.states == null)
        this.states = this.visualState.Split('.');
      string[] strArray;
      if (!stylable.VisualState.Contains("."))
        strArray = new string[1]{ stylable.VisualState };
      else
        strArray = stylable.VisualState.Split('.');
      string[] array = strArray;
      foreach (string state in this.states)
      {
        if (Array.IndexOf<string>(array, state) < 0)
          return false;
      }
      return true;
    }

    public void ApplyValue(IStylableNode element)
    {
      element.ApplySettings(new PropertySettingGroup()
      {
        PropertySettings = {
          this.propertySetting
        }
      });
    }

    public void Dispose()
    {
      this.PropertySetting.PropertyMapper = (PropertyMapper) null;
      this.PropertySetting = (PropertySetting) null;
    }
  }
}
