// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ThemeSource
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using Telerik.WinControls.Design;
using Telerik.WinControls.Themes.Design;

namespace Telerik.WinControls
{
  public class ThemeSource : RadObject
  {
    private static readonly Hashtable locationsAlreadyLoaded = new Hashtable();
    private static readonly object locationLoadedMarker = new object();
    public static RadProperty ThemeLocationProperty = RadProperty.Register(nameof (ThemeLocation), typeof (string), typeof (ThemeSource), new RadPropertyMetadata((object) "", new PropertyChangedCallback(ThemeSource.OnThemeLocationChanged)));
    public static RadProperty StorageTypeProperty = RadProperty.Register(nameof (StorageType), typeof (ThemeStorageType), typeof (ThemeSource), new RadPropertyMetadata((object) ThemeStorageType.File, new PropertyChangedCallback(ThemeSource.OnStorageTypeChanged)));
    internal string loadError = string.Empty;
    private Assembly callingAssembly;
    private RadThemeManager ownerThemeManager;
    internal bool loadSucceeded;
    internal Theme theme;

    [Description("Indicates whether the specified theme was loaded successfully.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ThemeStorageValid
    {
      get
      {
        return this.loadSucceeded;
      }
    }

    [Description("Indicates the error message if theme was not loaded successfully.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ThemeLoadingError
    {
      get
      {
        return this.loadError;
      }
    }

    [Description("Name of the theme file or resource.")]
    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string ThemeLocation
    {
      get
      {
        return (string) this.GetValue(ThemeSource.ThemeLocationProperty);
      }
      set
      {
        if ((object) this.callingAssembly == null)
          this.callingAssembly = Assembly.GetCallingAssembly();
        int num = (int) this.SetValue(ThemeSource.ThemeLocationProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadThemeManager OwnerThemeManager
    {
      get
      {
        return this.ownerThemeManager;
      }
      set
      {
        this.ownerThemeManager = value;
        if (!this.SettingsAreValid)
          return;
        this.ReloadThemeFromStorage();
      }
    }

    [RadPropertyDefaultValue("StorageType", typeof (ThemeSource))]
    public ThemeStorageType StorageType
    {
      get
      {
        return (ThemeStorageType) this.GetValue(ThemeSource.StorageTypeProperty);
      }
      set
      {
        if ((object) this.callingAssembly == null)
          this.callingAssembly = Assembly.GetCallingAssembly();
        int num = (int) this.SetValue(ThemeSource.StorageTypeProperty, (object) value);
      }
    }

    [Description("Indicates whether property values are valid")]
    public bool SettingsAreValid
    {
      get
      {
        if (!string.IsNullOrEmpty(this.ThemeLocation))
          return this.OwnerThemeManager != null;
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Assembly CallingAssembly
    {
      get
      {
        return this.callingAssembly;
      }
      set
      {
        this.callingAssembly = value;
      }
    }

    protected override void DisposeManagedResources()
    {
      if (this.ownerThemeManager != null && this.ownerThemeManager.Site != null && (this.theme != null && !string.IsNullOrEmpty(this.theme.Name)))
        this.RemoveStyles(this.theme);
      base.DisposeManagedResources();
    }

    public virtual void ReloadThemeFromStorage()
    {
      Theme theme = (Theme) null;
      bool flag = this.OwnerThemeManager == null || !this.OwnerThemeManager.IsDesignMode && this.ownerThemeManager.LoadedThemes.Count == 0;
      if ((object) this.callingAssembly == null)
        this.callingAssembly = Assembly.GetCallingAssembly();
      if (flag && ThemeSource.locationsAlreadyLoaded[(object) (this.callingAssembly.FullName + this.ThemeLocation)] == ThemeSource.locationLoadedMarker)
        return;
      if (this.StorageType == ThemeStorageType.File)
      {
        theme = Theme.ReadXML(this.ThemeLocation);
        ThemeRepository.Add(theme);
      }
      else if ((object) this.callingAssembly != null)
      {
        if (this.ownerThemeManager != null && this.ownerThemeManager.Site != null && this.ownerThemeManager.Site.DesignMode)
        {
          theme = this.LoadThemeInDesingMode(this);
          this.loadSucceeded = true;
        }
        else
        {
          Stream stream = this.callingAssembly.GetManifestResourceStream(this.ThemeLocation) ?? Assembly.GetExecutingAssembly().GetManifestResourceStream(this.ThemeLocation);
          if (stream == null)
          {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if ((object) entryAssembly != null)
              stream = entryAssembly.GetManifestResourceStream(this.ThemeLocation);
          }
          if (stream != null)
          {
            using (stream)
            {
              theme = new XMLThemeReader().Read(stream);
              ThemeRepository.Add(theme);
            }
          }
        }
      }
      if (theme != null)
      {
        this.theme = theme;
        if (!string.IsNullOrEmpty(theme.Name))
          this.loadSucceeded = true;
      }
      if (!flag || !this.loadSucceeded)
        return;
      ThemeSource.locationsAlreadyLoaded[(object) (this.callingAssembly.FullName + this.ThemeLocation)] = ThemeSource.locationLoadedMarker;
    }

    private static void OnThemeLocationChanged(RadObject d, RadPropertyChangedEventArgs e)
    {
      ThemeSource themeSource = (ThemeSource) d;
      if (!themeSource.SettingsAreValid)
        return;
      themeSource.ReloadThemeFromStorage();
    }

    private static void OnStorageTypeChanged(RadObject d, RadPropertyChangedEventArgs e)
    {
      ThemeSource themeSource = (ThemeSource) d;
      if (!themeSource.SettingsAreValid)
        return;
      themeSource.ReloadThemeFromStorage();
    }

    private void RemoveStyles(Theme theme)
    {
      Theme theme1 = ThemeRepository.FindTheme(theme.Name);
      foreach (StyleGroup styleGroup1 in theme.StyleGroups)
      {
        for (int index = theme1.StyleGroups.Count - 1; index >= 0; --index)
        {
          StyleGroup styleGroup2 = theme1.StyleGroups[index];
          if (styleGroup2.IsCompatible(styleGroup1))
          {
            theme1.StyleGroups.Remove(styleGroup2);
            break;
          }
        }
      }
      foreach (StyleRepository repository1 in theme.Repositories)
      {
        for (int index = theme1.Repositories.Count - 1; index >= 0; --index)
        {
          StyleRepository repository2 = theme1.Repositories[index];
          if (repository2.Key == repository1.Key)
          {
            theme1.Repositories.Remove(repository2);
            break;
          }
        }
      }
      ThemeResolutionService.RaiseThemeChanged(theme.Name, "");
    }

    private Theme LoadThemeInDesingMode(ThemeSource themeSource)
    {
      ProjectManagement managementInstance = ProjectManagement.GetProjectManagementInstance((IServiceProvider) themeSource.OwnerThemeManager.Site);
      string activeProjectFullPath = managementInstance.Services.GetActiveProjectFullPath();
      if (!themeSource.SettingsAreValid)
        return this.theme;
      string configurationPropertyValue = (string) managementInstance.Services.GetProjectConfigurationPropertyValue("OutputPath");
      if (string.IsNullOrEmpty(activeProjectFullPath) || string.IsNullOrEmpty(configurationPropertyValue))
        return (Theme) null;
      string path1 = Path.Combine(activeProjectFullPath, configurationPropertyValue);
      string str = (string) null;
      if (themeSource.StorageType == ThemeStorageType.File)
      {
        if (Path.IsPathRooted(themeSource.ThemeLocation))
          return (Theme) null;
        string themeLocation = themeSource.ThemeLocation;
        if (path1 != null && !string.IsNullOrEmpty(themeLocation))
        {
          string path2 = themeLocation.Replace("~\\", "").Replace("~", "");
          str = Path.Combine(path1, path2);
          if (!File.Exists(str))
          {
            themeSource.loadError = "Path not found: " + str;
            return this.theme;
          }
        }
      }
      else if (themeSource.StorageType == ThemeStorageType.Resource)
      {
        string themeLocation = themeSource.ThemeLocation;
        string[] fileNameParts = themeLocation.Split('.');
        str = ThemeSource.SearchFile(activeProjectFullPath, fileNameParts);
        if (str == null)
        {
          themeSource.loadError = string.Format("Unable locate Resource file '{0}' in the project folder '{1}'", (object) string.Join(Path.DirectorySeparatorChar.ToString(), themeLocation.Split('.')), (object) activeProjectFullPath);
          return this.theme;
        }
      }
      if (str == null)
      {
        themeSource.loadError = "Unable to determine active project path.";
        return this.theme;
      }
      Theme theme = Theme.ReadXML(str);
      ThemeRepository.Add(theme);
      return theme;
    }

    private static string SearchFile(string baseFolder, string[] fileNameParts)
    {
      if (fileNameParts == null || fileNameParts.Length < 2)
        return (string) null;
      int startIndex = fileNameParts.Length - 2;
      string str = string.Join(".", fileNameParts, startIndex, 2);
      do
      {
        string path = Path.Combine(baseFolder, str);
        if (File.Exists(path))
          return path;
        --startIndex;
        if (startIndex > 0)
          str = fileNameParts[startIndex] + (object) Path.DirectorySeparatorChar + str;
      }
      while (startIndex > 0);
      return ThemeSource.SearchFileInSubDirectories(baseFolder, str);
    }

    private static string SearchFileInSubDirectories(string baseFolder, string fileName)
    {
      foreach (string directory in Directory.GetDirectories(baseFolder))
      {
        string path = Path.Combine(directory, fileName);
        if (File.Exists(path))
          return path;
      }
      return (string) null;
    }
  }
}
