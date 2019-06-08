// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Theme
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class Theme : ICloneable
  {
    private bool designTimeVisible = true;
    private string name;
    private List<StyleGroup> styleGroups;
    private List<StyleRepository> repositories;

    public Theme()
    {
      this.styleGroups = new List<StyleGroup>(70);
      this.repositories = new List<StyleRepository>(800);
    }

    public Theme(string name)
      : this()
    {
      this.Name = name;
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

    public List<StyleGroup> StyleGroups
    {
      get
      {
        return this.styleGroups;
      }
    }

    public List<StyleRepository> Repositories
    {
      get
      {
        return this.repositories;
      }
    }

    public StyleGroup FindStyleGroup(string controlType)
    {
      foreach (StyleGroup styleGroup in this.styleGroups)
      {
        if (styleGroup.IsCompatible(controlType))
          return styleGroup;
      }
      return (StyleGroup) null;
    }

    public StyleGroup FindStyleGroup(Control control)
    {
      foreach (StyleGroup styleGroup in this.styleGroups)
      {
        if (styleGroup.IsCompatible(control))
          return styleGroup;
      }
      return (StyleGroup) null;
    }

    public StyleGroup FindStyleGroup(IStylableNode item)
    {
      foreach (StyleGroup styleGroup in this.styleGroups)
      {
        if (styleGroup.IsCompatible(item))
          return styleGroup;
      }
      return (StyleGroup) null;
    }

    public StyleRepository FindRepository(string key)
    {
      foreach (StyleRepository repository in this.Repositories)
      {
        if (repository.Key == key)
          return repository;
      }
      return (StyleRepository) null;
    }

    public void MergeRepositories()
    {
      lock (this.repositories)
      {
        foreach (StyleGroup styleGroup in this.StyleGroups)
        {
          lock (styleGroup.PropertySettingGroups)
          {
            foreach (PropertySettingGroup propertySettingGroup in styleGroup.PropertySettingGroups)
            {
              if (!string.IsNullOrEmpty(propertySettingGroup.BasedOn))
              {
                string basedOn = propertySettingGroup.BasedOn;
                char[] chArray = new char[1]{ ',' };
                foreach (string key in basedOn.Split(chArray))
                {
                  StyleRepository repository1 = this.FindRepository(key);
                  if (repository1 != null)
                  {
                    bool flag = false;
                    foreach (StyleRepository repository2 in propertySettingGroup.Repositories)
                    {
                      if (repository2.Key == repository1.Key)
                      {
                        flag = true;
                        break;
                      }
                    }
                    if (!flag)
                    {
                      propertySettingGroup.Repositories.Add(repository1);
                      if (repository1.ItemType == "Border")
                      {
                        foreach (PropertySetting setting in repository1.Settings)
                          setting.PropertyMapper = new PropertyMapper(Theme.BorderPropertyMapper);
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    public bool IsCompatible(string themeName)
    {
      string name = this.Name;
      char[] chArray = new char[1]{ ',' };
      foreach (string str in name.Split(chArray))
      {
        if (str.Trim() == themeName)
          return true;
      }
      return false;
    }

    public bool DesignTimeVisible
    {
      get
      {
        return this.designTimeVisible;
      }
      set
      {
        this.designTimeVisible = value;
      }
    }

    public static Theme ReadTSSP(string filePath)
    {
      return new TSSPThemeReader().Read(filePath);
    }

    public static Theme ReadXML(string filePath)
    {
      return new XMLThemeReader().Read(filePath);
    }

    public static Theme ReadCSS(string filePath)
    {
      return new CSSThemeReader().Read(filePath);
    }

    public static Theme ReadCSSText(string text)
    {
      return new CSSThemeReader().ReadText(text);
    }

    public static Theme ReadFile(string filePath)
    {
      switch (Path.GetExtension(filePath).ToLower())
      {
        case ".tssp":
          return Theme.ReadTSSP(filePath);
        case ".xml":
          return Theme.ReadXML(filePath);
        case ".css":
          return Theme.ReadCSS(filePath);
        default:
          return (Theme) null;
      }
    }

    public static Theme ReadResource(Assembly resourceAssembly, string resourcePath)
    {
      Theme theme = (Theme) null;
      try
      {
        using (Stream manifestResourceStream = resourceAssembly.GetManifestResourceStream(resourcePath))
        {
          if (manifestResourceStream != null)
            theme = new TSSPThemeReader().Read(manifestResourceStream);
        }
      }
      catch
      {
        int num = (int) MessageBox.Show(string.Format("Failed to load {0} theme.", (object) resourcePath));
      }
      return theme;
    }

    public object Clone()
    {
      Theme theme = new Theme();
      theme.Name = this.name;
      theme.designTimeVisible = this.designTimeVisible;
      foreach (StyleRepository repository in this.Repositories)
        theme.Repositories.Add(new StyleRepository(repository));
      foreach (StyleGroup styleGroup in this.StyleGroups)
        theme.StyleGroups.Add(new StyleGroup(styleGroup));
      theme.MergeRepositories();
      return (object) theme;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void Combine(Theme theme, bool mergeRepositories, bool replaceExistingStyles)
    {
      foreach (StyleGroup styleGroup1 in theme.StyleGroups)
      {
        bool flag = false;
        foreach (StyleGroup styleGroup2 in this.styleGroups)
        {
          if (styleGroup2.IsCompatible(styleGroup1))
          {
            styleGroup2.Combine(styleGroup1, replaceExistingStyles);
            flag = true;
            break;
          }
        }
        if (!flag)
          this.styleGroups.Add(styleGroup1);
      }
      foreach (StyleGroup styleGroup in theme.StyleGroups)
      {
        foreach (PropertySettingGroup propertySettingGroup in styleGroup.PropertySettingGroups)
          propertySettingGroup.Repositories.Clear();
      }
      if (!mergeRepositories)
        return;
      foreach (StyleRepository repository1 in theme.Repositories)
      {
        StyleRepository repository2 = this.FindRepository(repository1.Key);
        if (repository2 != null)
          this.Repositories.Remove(repository2);
        this.Repositories.Add(repository1);
      }
      this.MergeRepositories();
    }

    public static string BorderPropertyMapper(string propertyName, RadObject targetObject)
    {
      if (targetObject is RadItem || !(targetObject is RadElement))
      {
        if (propertyName == "ForeColor")
          return "BorderColor";
        if (propertyName == "GradientStyle" || propertyName == "GradientAngle" || (propertyName == "Width" || propertyName == "BoxStyle") || (propertyName == "InnerColor" || propertyName == "InnerColor2" || (propertyName == "InnerColor3" || propertyName == "InnerColor4")) || (propertyName == "LeftColor" || propertyName == "RightColor" || (propertyName == "TopColor" || propertyName == "BottomColor")))
          return "Border" + propertyName;
        if (propertyName == "ForeColor2" || propertyName == "ForeColor3" || propertyName == "ForeColor4")
          return propertyName.Replace("Fore", "Border");
      }
      return propertyName;
    }
  }
}
