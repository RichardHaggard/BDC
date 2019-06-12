// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertySetting
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class PropertySetting : IPropertySetting
  {
    private RadProperty property;
    private string name;
    private string fullName;
    private object value;
    private object endValue;
    private AnimatedPropertySetting animatedSetting;
    public PropertyMapper PropertyMapper;

    public PropertySetting()
    {
    }

    public PropertySetting(string propertyName, object value)
    {
      this.name = propertyName;
      this.value = value;
    }

    public PropertySetting(RadProperty property, object value)
    {
      this.property = property;
      this.name = property.Name;
      this.value = value;
    }

    public PropertySetting(PropertySetting setting)
    {
      this.property = setting.property;
      this.name = setting.name;
      this.fullName = setting.fullName;
      this.value = setting.value;
      this.endValue = setting.endValue;
      if (setting.animatedSetting == null)
        return;
      this.animatedSetting = new AnimatedPropertySetting();
      this.animatedSetting.StartValue = setting.animatedSetting.EndValue;
      this.animatedSetting.EndValue = setting.animatedSetting.EndValue;
      this.animatedSetting.ApplyDelay = setting.animatedSetting.ApplyDelay;
      this.animatedSetting.RemoveAfterApply = setting.animatedSetting.RemoveAfterApply;
      this.animatedSetting.Step = setting.animatedSetting.Step;
      this.animatedSetting.NumFrames = setting.animatedSetting.NumFrames;
      this.animatedSetting.Interval = setting.animatedSetting.Interval;
      this.animatedSetting.RandomDelay = setting.animatedSetting.RandomDelay;
    }

    public string Name
    {
      get
      {
        if (string.IsNullOrEmpty(this.name) && !string.IsNullOrEmpty(this.fullName))
        {
          string[] strArray = this.fullName.Split('.');
          if (strArray.Length > 1)
            return strArray[strArray.Length - 1];
        }
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public string FullName
    {
      get
      {
        return this.fullName;
      }
      set
      {
        this.fullName = value;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public object EndValue
    {
      get
      {
        return this.endValue;
      }
      set
      {
        this.endValue = value;
      }
    }

    public AnimatedPropertySetting AnimatedSetting
    {
      get
      {
        return this.animatedSetting;
      }
      set
      {
        this.animatedSetting = value;
      }
    }

    public RadProperty Property
    {
      get
      {
        if (this.property == null)
        {
          string property = this.FullName;
          if (string.IsNullOrEmpty(property))
            property = this.Name;
          if (!string.IsNullOrEmpty(property) && property.IndexOf('.') >= 0)
            this.property = XmlPropertySetting.DeserializePropertySafe(property);
        }
        return this.property;
      }
      set
      {
        this.property = value;
      }
    }

    public object GetCurrentValue(RadObject forObject)
    {
      if (this.animatedSetting != null)
        return this.animatedSetting.GetCurrentValue(forObject);
      if (this.value == null && this.endValue != null)
        return this.endValue;
      return this.value;
    }

    public void ApplyValue(RadObject element)
    {
      string propertyName = this.Name;
      if (this.PropertyMapper != null)
        propertyName = this.PropertyMapper(propertyName, element);
      if (this.property == null)
        this.property = PropertySetting.FindProperty(element.GetType(), propertyName, true);
      if (this.property != null && (!this.property.OwnerType.IsAssignableFrom(element.GetType()) || this.property.Name != propertyName))
        this.property = PropertySetting.FindProperty(element.GetType(), propertyName, true);
      if (this.property == null)
        return;
      if (this.endValue != null)
      {
        if (this.animatedSetting == null)
        {
          this.animatedSetting = new AnimatedPropertySetting();
          this.animatedSetting.Property = this.property;
          this.animatedSetting.StartValue = this.value;
          this.animatedSetting.EndValue = this.endValue;
        }
        this.animatedSetting.IsStyleSetting = true;
        this.animatedSetting.Property = this.property;
        this.animatedSetting.ApplyValue(element);
      }
      int num = (int) element.AddStylePropertySetting((IPropertySetting) this);
    }

    public void UnapplyValue(RadObject element)
    {
      if (this.animatedSetting != null)
        this.animatedSetting.UnapplyValue(element);
      else
        element.RemoveStylePropertySetting((IPropertySetting) this);
    }

    public static RadProperty FindProperty(
      Type currentType,
      string propertyName,
      bool fallback)
    {
      if (string.IsNullOrEmpty(propertyName))
        return (RadProperty) null;
      string[] strArray = propertyName.Split('.');
      if (strArray.Length > 1)
        propertyName = strArray[strArray.Length - 1];
      RadProperty safe = RadProperty.FindSafe(currentType, propertyName);
      if (safe == null && fallback)
      {
        for (currentType = currentType.BaseType; (object) currentType != null && (object) currentType != (object) typeof (RadObject); currentType = currentType.BaseType)
        {
          safe = RadProperty.FindSafe(currentType, propertyName);
          if (safe == null && (currentType.Name == "LightVisualElement" || currentType.Name == "UIChartElement"))
            safe = RadProperty.FindSafe(currentType, "Border" + propertyName);
          if (safe != null)
            break;
        }
      }
      return safe;
    }
  }
}
