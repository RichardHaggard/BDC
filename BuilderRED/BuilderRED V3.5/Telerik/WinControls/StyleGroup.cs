// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.StyleGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Telerik.WinControls
{
  public class StyleGroup
  {
    private List<PropertySettingGroup> settingGroups;
    private List<StyleRegistration> registrations;

    public StyleGroup()
    {
      this.settingGroups = new List<PropertySettingGroup>(50);
      this.registrations = new List<StyleRegistration>(3);
    }

    public StyleGroup(StyleRegistration registration)
      : this()
    {
      this.registrations.Add(registration);
    }

    public StyleGroup(string elementType)
      : this()
    {
      this.registrations.Add(new StyleRegistration(elementType));
    }

    public StyleGroup(StyleGroup styleGroup)
      : this()
    {
      foreach (PropertySettingGroup propertySettingGroup in styleGroup.PropertySettingGroups)
        this.PropertySettingGroups.Add(new PropertySettingGroup(propertySettingGroup));
      foreach (StyleRegistration registration in styleGroup.Registrations)
        this.registrations.Add(new StyleRegistration(registration));
    }

    public List<PropertySettingGroup> PropertySettingGroups
    {
      get
      {
        return this.settingGroups;
      }
    }

    public List<StyleRegistration> Registrations
    {
      get
      {
        return this.registrations;
      }
    }

    public bool IsCompatible(string controlType)
    {
      foreach (StyleRegistration registration in this.Registrations)
      {
        if (registration.IsCompatible(controlType))
          return true;
      }
      return false;
    }

    public bool IsCompatible(Control control)
    {
      foreach (StyleRegistration registration in this.Registrations)
      {
        if (registration.IsCompatible(control))
          return true;
      }
      return false;
    }

    public bool IsCompatible(IStylableNode item)
    {
      foreach (StyleRegistration registration in this.Registrations)
      {
        if (registration.IsCompatible(item))
          return true;
      }
      return false;
    }

    public bool IsCompatible(StyleGroup dstGroup)
    {
      string str = "";
      foreach (StyleRegistration registration1 in this.Registrations)
      {
        bool flag = false;
        if (str == string.Empty && registration1.RegistrationType == "ElementTypeControlType" && registration1.ElementType == "Telerik.WinControls.RootRadElement")
          str = registration1.ControlType;
        foreach (StyleRegistration registration2 in dstGroup.Registrations)
        {
          if (str != string.Empty && registration2.RegistrationType == "ElementTypeControlType" && registration2.ElementType == "Telerik.WinControls.RootRadElement")
            return str == registration2.ControlType;
          if (registration1.IsCompatible(registration2))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return false;
      }
      return true;
    }

    public StyleSheet CreateStyleSheet(RadObject element)
    {
      StyleSheet styleSheet = new StyleSheet();
      RadElement radElement = element as RadElement;
      IStylableNode stylableNode = element as IStylableNode;
      lock (this.settingGroups)
      {
        foreach (PropertySettingGroup propertySettingGroup in this.PropertySettingGroups)
        {
          if (propertySettingGroup.Selector != null)
          {
            if (radElement != null && (propertySettingGroup.Selector.Type == ElementSelectorTypes.ClassSelector || propertySettingGroup.Selector.Type == ElementSelectorTypes.TypeSelector))
            {
              bool flag = false;
              foreach (RadObject element1 in stylableNode.ChildrenHierarchy)
              {
                if (!(element1 is RadItem) && propertySettingGroup.Selector.IsCompatible(element1))
                {
                  styleSheet.PropertySettingGroups.Add(propertySettingGroup);
                  flag = true;
                  break;
                }
              }
              if (flag)
                continue;
            }
            if (propertySettingGroup.Selector.IsCompatible(element))
              styleSheet.PropertySettingGroups.Add(propertySettingGroup);
          }
        }
      }
      if (styleSheet.PropertySettingGroups.Count > 0)
        return styleSheet;
      return (StyleSheet) null;
    }

    public void Combine(StyleGroup group, bool replaceExistingStyles)
    {
      if (replaceExistingStyles)
      {
        this.PropertySettingGroups.Clear();
        this.PropertySettingGroups.AddRange((IEnumerable<PropertySettingGroup>) group.PropertySettingGroups);
      }
      else
      {
        foreach (PropertySettingGroup propertySettingGroup1 in group.PropertySettingGroups)
        {
          bool flag = false;
          foreach (PropertySettingGroup propertySettingGroup2 in this.PropertySettingGroups)
          {
            if (propertySettingGroup2.Selector.IsCompatible(propertySettingGroup1.Selector))
            {
              flag = true;
              using (List<PropertySetting>.Enumerator enumerator = propertySettingGroup1.PropertySettings.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  PropertySetting current = enumerator.Current;
                  PropertySetting propertySetting1 = (PropertySetting) null;
                  foreach (PropertySetting propertySetting2 in propertySettingGroup2.PropertySettings)
                  {
                    if (propertySetting2.Name == current.Name)
                    {
                      propertySetting1 = propertySetting2;
                      break;
                    }
                  }
                  if (propertySetting1 != null)
                    propertySettingGroup2.PropertySettings.Remove(propertySetting1);
                  propertySettingGroup2.PropertySettings.Add(current);
                }
                break;
              }
            }
          }
          if (!flag)
            this.PropertySettingGroups.Add(propertySettingGroup1);
        }
      }
      foreach (StyleRegistration registration1 in group.Registrations)
      {
        bool flag = false;
        foreach (StyleRegistration registration2 in this.Registrations)
        {
          if (registration2.IsCompatible(registration1))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.Registrations.Add(registration1);
      }
    }

    public void SaveXML(string themeName, string fileName)
    {
      using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8))
      {
        xmlTextWriter.Formatting = Formatting.Indented;
        XmlTheme xmlTheme = new XmlTheme();
        xmlTheme.ThemeName = themeName;
        xmlTheme.ThemeVersion = "2.0";
        XmlStyleSheet xmlStyleSheet = new XmlStyleSheet();
        foreach (PropertySettingGroup propertySettingGroup in this.PropertySettingGroups)
          xmlStyleSheet.PropertySettingGroups.Add(new XmlPropertySettingGroup(propertySettingGroup));
        xmlTheme.BuilderRegistrations = new XmlStyleBuilderRegistration[1]
        {
          new XmlStyleBuilderRegistration(this)
          {
            BuilderData = (XmlBuilderData) xmlStyleSheet
          }
        };
        xmlTheme.SaveToWriter((XmlWriter) xmlTextWriter);
      }
    }

    public Theme CreateTheme(string name)
    {
      Theme theme = new Theme(name);
      theme.StyleGroups.Add(new StyleGroup(this));
      Dictionary<string, StyleRepository> dictionary = new Dictionary<string, StyleRepository>();
      foreach (PropertySettingGroup propertySettingGroup in this.PropertySettingGroups)
      {
        foreach (StyleRepository repository in propertySettingGroup.Repositories)
        {
          if (!dictionary.ContainsKey(repository.Key))
            dictionary.Add(repository.Key, repository);
        }
      }
      foreach (StyleRepository repository in dictionary.Values)
        theme.Repositories.Add(new StyleRepository(repository));
      theme.MergeRepositories();
      return theme;
    }
  }
}
