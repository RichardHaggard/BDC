// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CSSThemeReader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;
using System.Text;

namespace Telerik.WinControls
{
  public class CSSThemeReader : IThemeReader
  {
    public Theme ReadText(string text)
    {
      CSSParser parser = new CSSParser();
      parser.ReadText(text);
      return this.Parse(parser);
    }

    public Theme Read(string filePath)
    {
      CSSParser parser = new CSSParser();
      parser.Read(filePath);
      return this.Parse(parser);
    }

    public Theme Read(Stream stream)
    {
      CSSParser parser = new CSSParser();
      parser.Read(stream);
      return this.Parse(parser);
    }

    private Theme Parse(CSSParser parser)
    {
      Theme theme = new Theme();
      theme.StyleGroups.Add(new StyleGroup());
      foreach (CSSGroup group in parser.groups)
      {
        if (group.name == "theme")
        {
          theme.Name = group["name"].value;
          StyleRegistration styleRegistration = new StyleRegistration();
          theme.StyleGroups[0].Registrations.Add(styleRegistration);
          if (group.Contains("elementType"))
          {
            styleRegistration.RegistrationType = "ElementTypeDefault";
            styleRegistration.ElementType = group["elementType"].value;
          }
          if (group.Contains("controlType"))
          {
            styleRegistration.RegistrationType = "ElementTypeControlType";
            styleRegistration.ControlType = group["controlType"].value;
            styleRegistration.ElementType = "Telerik.WinControls.RootRadElement";
          }
        }
        else if (group.name.StartsWith("#"))
        {
          StyleRepository styleRepository = new StyleRepository();
          styleRepository.Key = group.name.Remove(0, 1).Trim();
          foreach (CSSItem cssItem in group.items)
          {
            PropertySetting propertySetting = this.CreatePropertySetting(cssItem);
            styleRepository.Settings.Add(propertySetting);
          }
          theme.Repositories.Add(styleRepository);
        }
        else
        {
          PropertySettingGroup propertySettingGroup = new PropertySettingGroup();
          if (group.BasedOn.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < group.BasedOn.Count; ++index)
            {
              string str = group.BasedOn[index];
              stringBuilder.Append(str);
              if (index < group.BasedOn.Count - 1)
                stringBuilder.Append(",");
            }
            propertySettingGroup.BasedOn = stringBuilder.ToString();
          }
          propertySettingGroup.Selector = new ElementSelector();
          propertySettingGroup.Selector.Type = ElementSelectorTypes.VisualStateSelector;
          propertySettingGroup.Selector.Value = group.name;
          if (!string.IsNullOrEmpty(group.childName))
          {
            propertySettingGroup.Selector.ChildSelector = new ElementSelector(group.childName);
            propertySettingGroup.Selector.ChildSelector.IsRecursive = true;
          }
          theme.StyleGroups[0].PropertySettingGroups.Add(propertySettingGroup);
          foreach (CSSItem cssItem in group.items)
          {
            if (cssItem.name == "selectChild")
            {
              propertySettingGroup.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.VisualStateSelector, cssItem.value);
              propertySettingGroup.Selector.ChildSelector.IsRecursive = true;
            }
            else
            {
              if (cssItem.name == "selectChildClass")
                propertySettingGroup.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.ClassSelector, cssItem.value);
              PropertySetting propertySetting = this.CreatePropertySetting(cssItem);
              propertySettingGroup.PropertySettings.Add(propertySetting);
            }
          }
        }
      }
      return theme;
    }

    protected virtual PropertySetting CreatePropertySetting(CSSItem item)
    {
      if (item.childItems.Count <= 0)
        return new PropertySetting(item.name, PropertyReader.Deserialize(item.name, item.value));
      PropertySetting propertySetting = new PropertySetting(item.name, (object) null)
      {
        Value = PropertyReader.Deserialize(item.name, item["Value"]),
        EndValue = PropertyReader.Deserialize(item.name, item["EndValue"]),
        AnimatedSetting = new AnimatedPropertySetting()
      };
      propertySetting.AnimatedSetting.StartValue = propertySetting.Value;
      propertySetting.AnimatedSetting.EndValue = propertySetting.EndValue;
      propertySetting.AnimatedSetting.IsStyleSetting = true;
      if (item["MaxValue"] != null)
        propertySetting.AnimatedSetting.MaxValue = PropertyReader.Deserialize(item.name, item["MaxValue"]);
      string str1 = item["EasingType"];
      if (!string.IsNullOrEmpty(str1))
        propertySetting.AnimatedSetting.ApplyEasingType = (RadEasingType) Enum.Parse(typeof (RadEasingType), str1);
      string s1 = item["Interval"];
      if (!string.IsNullOrEmpty(s1))
        propertySetting.AnimatedSetting.Interval = int.Parse(s1);
      string s2 = item["Frames"];
      if (!string.IsNullOrEmpty(s2))
        propertySetting.AnimatedSetting.NumFrames = int.Parse(s2);
      string s3 = item["ApplyDelay"];
      if (!string.IsNullOrEmpty(s3))
        propertySetting.AnimatedSetting.ApplyDelay = int.Parse(s3);
      string s4 = item["RandomDelay"];
      if (!string.IsNullOrEmpty(s4))
        propertySetting.AnimatedSetting.RandomDelay = int.Parse(s4);
      string str2 = item["RemoveAfterApply"];
      if (!string.IsNullOrEmpty(str2))
        propertySetting.AnimatedSetting.RemoveAfterApply = bool.Parse(str2);
      return propertySetting;
    }
  }
}
