// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.XmlRepositoryItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.Styles
{
  public class XmlRepositoryItem : List<XmlPropertySetting>
  {
    private static object syncRoot = new object();
    private static string[] badItems = new string[9]{ "DrawFill", "DrawBorder", "BorderColor", "BorderColor2", "BorderColor3", "BorderColor4", "BorderGradientAngle", "BorderGradientStyle", "BorderWidth" };
    private string key;
    private ElementVisibility? visibilityModifierValue;
    private string itemType;
    private string displayName;
    private List<IPropertySetting> deserializedPropertySettings;

    public XmlRepositoryItem()
    {
    }

    public XmlRepositoryItem(StyleRepository repository)
    {
      this.key = repository.Key;
      this.displayName = repository.Name;
      this.itemType = repository.ItemType;
      foreach (PropertySetting setting in repository.Settings)
      {
        bool flag = false;
        foreach (string badItem in XmlRepositoryItem.badItems)
        {
          if (badItem == setting.Name)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          string fullName;
          if (!string.IsNullOrEmpty(setting.FullName))
            fullName = setting.FullName;
          else if (setting.Property != null)
            fullName = setting.Property.FullName;
          else
            continue;
          if (setting.EndValue != null)
          {
            XmlAnimatedPropertySetting animatedPropertySetting = new XmlAnimatedPropertySetting();
            animatedPropertySetting.Property = fullName;
            animatedPropertySetting.Value = setting.Value;
            animatedPropertySetting.EndValue = setting.EndValue;
            this.Add((XmlPropertySetting) animatedPropertySetting);
          }
          else
            this.Add(new XmlPropertySetting()
            {
              Property = fullName,
              Value = setting.Value
            });
        }
      }
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

    public string DisplayName
    {
      get
      {
        return this.displayName;
      }
      set
      {
        this.displayName = value;
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

    private void InitializeVisibilityModifierValue(IPropertySetting setting)
    {
      PropertySetting propertySetting = setting as PropertySetting;
      if (propertySetting == null || !(propertySetting.Value is ElementVisibility))
        return;
      this.visibilityModifierValue = new ElementVisibility?((ElementVisibility) propertySetting.Value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<IPropertySetting> DeserializedPropertySettings
    {
      get
      {
        if (this.deserializedPropertySettings == null)
        {
          lock (XmlRepositoryItem.syncRoot)
          {
            if (this.deserializedPropertySettings == null)
            {
              this.deserializedPropertySettings = new List<IPropertySetting>();
              foreach (XmlPropertySetting xmlPropertySetting in (List<XmlPropertySetting>) this)
              {
                IPropertySetting setting = xmlPropertySetting.Deserialize();
                AnimatedPropertySetting animatedPropertySetting = setting as AnimatedPropertySetting;
                if (animatedPropertySetting != null)
                  animatedPropertySetting.IsStyleSetting = true;
                if (object.ReferenceEquals((object) setting.Property, (object) RadElement.VisibilityProperty))
                  this.InitializeVisibilityModifierValue(setting);
                this.deserializedPropertySettings.Add(setting);
              }
              this.EnsureLightVisualElementProperties();
            }
          }
        }
        return this.deserializedPropertySettings;
      }
    }

    private void EnsureLightVisualElementProperties()
    {
      bool flag = true;
      if (this.visibilityModifierValue.HasValue)
        flag = this.visibilityModifierValue.Value == ElementVisibility.Visible;
      if (this.itemType == "Gradient")
      {
        RadProperty property = XmlPropertySetting.DeserializePropertySafe("Telerik.WinControls.UI.LightVisualElement.DrawFill");
        if (property == null)
          return;
        this.deserializedPropertySettings.Add((IPropertySetting) new PropertySetting(property, (object) flag));
      }
      else
      {
        if (!(this.itemType == "Border"))
          return;
        RadProperty property = XmlPropertySetting.DeserializePropertySafe("Telerik.WinControls.UI.LightVisualElement.DrawBorder");
        if (property == null)
          return;
        this.deserializedPropertySettings.Add((IPropertySetting) new PropertySetting(property, (object) flag));
      }
    }

    public void Reset()
    {
      this.deserializedPropertySettings = (List<IPropertySetting>) null;
      this.Clear();
    }
  }
}
