// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XMLThemeReader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Telerik.WinControls
{
  public class XMLThemeReader : IThemeReader
  {
    private Dictionary<string, ElementReader> readers = new Dictionary<string, ElementReader>(13);
    private bool storeFullPropertyName;

    public XMLThemeReader()
    {
      this.readers.Add("Telerik.WinControls.Styles.XmlRepositoryItem", new ElementReader(this.ReadRepositoryItem));
      this.readers.Add("XmlPropertySettingGroup", new ElementReader(this.ReadPropertySettingGroup));
      this.readers.Add("RadStylesheetRelation", new ElementReader(this.ReadStyleSheetRelation));
      this.readers.Add("XmlVisualStateSelector", new ElementReader(this.ReadSelector));
      this.readers.Add("XmlClassSelector", new ElementReader(this.ReadSelector));
      this.readers.Add("XmlTypeSelector", new ElementReader(this.ReadSelector));
      this.readers.Add("ChildSelector", new ElementReader(this.ReadSelector));
      this.readers.Add("XmlPropertySetting", new ElementReader(this.ReadPropertySetting));
      this.readers.Add("XmlAnimatedPropertySetting", new ElementReader(this.ReadPropertySetting));
      this.readers.Add("XmlTheme", new ElementReader(this.ReadXmlTheme));
      this.readers.Add("XmlStyleBuilderRegistration", new ElementReader(this.ReadStyleRegistration));
    }

    public XMLThemeReader(bool storeFullPropertyName)
      : this()
    {
      this.storeFullPropertyName = storeFullPropertyName;
    }

    public bool StoreFullPropertyName
    {
      get
      {
        return this.storeFullPropertyName;
      }
      set
      {
        this.storeFullPropertyName = value;
      }
    }

    public Theme Read(Theme theme, XmlTextReader reader)
    {
      using (reader)
      {
        ReaderContext context = new ReaderContext();
        context.theme = theme;
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element)
          {
            int depth = reader.Depth;
            string name = reader.Name;
            if (this.readers.ContainsKey(reader.Name))
            {
              ElementReader reader1 = this.readers[reader.Name];
              if (reader1 != null)
                reader1(context, reader);
            }
            if (depth != context.depth && name != "XmlPropertySetting" && name != "XmlAnimatedPropertySetting")
            {
              context.depth = depth;
              context.parentNode = name;
            }
          }
          if (reader.NodeType == XmlNodeType.EndElement)
          {
            if (context.currentRepository != null && reader.Name == "Telerik.WinControls.Styles.XmlRepositoryItem")
              context.currentRepository.Initialize();
            else if (context.currentSettingGroup != null && reader.Name == "XmlPropertySettingGroup" && context.currentSettingGroup.Selector == null)
            {
              context.currentStyleGroup.PropertySettingGroups.Remove(context.currentSettingGroup);
              context.currentSettingGroup = (PropertySettingGroup) null;
            }
          }
        }
        reader.Close();
        reader.Dispose();
      }
      return theme;
    }

    public Theme Read(string filePath)
    {
      if (filePath.StartsWith("~"))
        filePath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + filePath.Substring(1);
      return this.Read(new Theme(), new XmlTextReader(filePath));
    }

    public Theme Read(Stream stream)
    {
      if (stream == null)
        return (Theme) null;
      return this.Read(new Theme(), new XmlTextReader(stream));
    }

    private void ReadXmlTheme(ReaderContext context, XmlTextReader reader)
    {
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == "ThemeName")
        {
          context.theme.Name = reader.Value;
          break;
        }
      }
    }

    private void ReadStyleRegistration(ReaderContext context, XmlTextReader reader)
    {
      StyleGroup styleGroup = new StyleGroup();
      context.theme.StyleGroups.Add(styleGroup);
      context.currentStyleGroup = styleGroup;
    }

    private void ReadPropertySetting(ReaderContext context, XmlTextReader reader)
    {
      PropertySetting propertySetting = this.ReadPropertySettingCore(context.theme, reader);
      if (context.parentNode == "Telerik.WinControls.Styles.XmlRepositoryItem")
      {
        if (context.currentRepository == null)
          return;
        context.currentRepository.Settings.Add(propertySetting);
      }
      else
      {
        if (!(context.parentNode == "PropertySettings") || context.currentSettingGroup == null)
          return;
        context.currentSettingGroup.PropertySettings.Add(propertySetting);
      }
    }

    private PropertySetting ReadPropertySettingCore(
      Theme theme,
      XmlTextReader reader)
    {
      string fullName = "";
      string str1 = "";
      string str2 = "";
      PropertySetting propertySetting = new PropertySetting();
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == "Property")
          fullName = reader.Value;
        else if (reader.Name == "Value")
          str1 = reader.Value;
        else if (reader.Name == "EndValue")
          str2 = reader.Value;
      }
      if (!string.IsNullOrEmpty(fullName))
      {
        try
        {
          string[] strArray = fullName.Split('.');
          string propertyName = "";
          if (strArray.Length > 1)
          {
            propertyName = strArray[strArray.Length - 1];
            string.Join(".", strArray, 0, strArray.Length - 1);
          }
          if (this.storeFullPropertyName)
            propertySetting.FullName = fullName;
          else
            propertySetting.Name = propertyName;
          if (!string.IsNullOrEmpty(str1))
            propertySetting.Value = PropertyReader.Deserialize(fullName, propertyName, str1);
          if (!string.IsNullOrEmpty(str2))
            propertySetting.EndValue = PropertyReader.Deserialize(fullName, propertyName, str2);
        }
        catch (Exception ex)
        {
        }
      }
      return propertySetting;
    }

    private void ReadRepositoryItem(ReaderContext context, XmlTextReader reader)
    {
      StyleRepository styleRepository = new StyleRepository();
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == "ItemType")
          styleRepository.ItemType = reader.Value;
        else if (reader.Name == "DisplayName")
          styleRepository.Name = reader.Value;
        else if (reader.Name == "Key")
          styleRepository.Key = reader.Value;
      }
      context.theme.Repositories.Add(styleRepository);
      context.currentRepository = styleRepository;
    }

    private void ReadPropertySettingGroup(ReaderContext context, XmlTextReader reader)
    {
      PropertySettingGroup propertySettingGroup = new PropertySettingGroup();
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == "BasedOn")
        {
          propertySettingGroup.BasedOn = reader.Value;
          break;
        }
      }
      context.currentStyleGroup.PropertySettingGroups.Add(propertySettingGroup);
      context.currentSettingGroup = propertySettingGroup;
    }

    private void ReadStyleSheetRelation(ReaderContext context, XmlTextReader reader)
    {
      StyleRegistration styleRegistration = new StyleRegistration();
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == "RegistrationType")
          styleRegistration.RegistrationType = reader.Value;
        else if (reader.Name == "ControlType")
          styleRegistration.ControlType = reader.Value;
        else if (reader.Name == "ControlName")
          styleRegistration.ControlName = reader.Value;
        else if (reader.Name == "ElementType")
          styleRegistration.ElementType = reader.Value;
        else if (reader.Name == "ElementName")
          styleRegistration.ElementName = reader.Value;
      }
      context.currentStyleGroup.Registrations.Add(styleRegistration);
    }

    private void ReadSelector(ReaderContext context, XmlTextReader reader)
    {
      string name = reader.Name;
      ElementSelector elementSelector = new ElementSelector();
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == "VisualState")
        {
          elementSelector.Value = reader.Value;
          elementSelector.Type = ElementSelectorTypes.VisualStateSelector;
          break;
        }
        if (reader.Name == "ElementClass")
        {
          elementSelector.Value = reader.Value;
          elementSelector.Type = ElementSelectorTypes.ClassSelector;
          break;
        }
        if (reader.Name == "ElementType")
        {
          elementSelector.Value = reader.Value;
          elementSelector.Type = ElementSelectorTypes.TypeSelector;
          break;
        }
      }
      if (name == "ChildSelector")
        context.currentSettingGroup.Selector.ChildSelector = elementSelector;
      else
        context.currentSettingGroup.Selector = elementSelector;
    }
  }
}
