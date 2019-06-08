// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ThemeRepository
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;

namespace Telerik.WinControls
{
  public class ThemeRepository
  {
    private static object lockObject = new object();
    private static HybridDictionary themes = new HybridDictionary();
    private static HybridDictionary registeredThemes = new HybridDictionary();
    private static Theme controlDefault;

    public static Theme ControlDefault
    {
      get
      {
        if (ThemeRepository.controlDefault == null)
        {
          using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Telerik.WinControls.ControlDefault.tssp"))
          {
            Theme theme = new TSSPThemeReader().Read(manifestResourceStream);
            if (theme != null)
            {
              ThemeRepository.controlDefault = theme;
              ThemeRepository.controlDefault.MergeRepositories();
            }
          }
        }
        return ThemeRepository.controlDefault;
      }
      set
      {
        if (value != null)
        {
          Theme theme = ThemeRepository.FindTheme(value.Name);
          if (theme != value)
            ThemeRepository.Add(theme);
        }
        ThemeRepository.controlDefault = value;
      }
    }

    public static ICollection LoadedThemes
    {
      get
      {
        return ThemeRepository.themes.Values;
      }
    }

    public static IEnumerable<string> AvailableThemeNames
    {
      get
      {
        IEnumerator enumerator1 = ThemeRepository.themes.Keys.GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            string name = (string) enumerator1.Current;
            yield return name;
          }
        }
        finally
        {
          IDisposable disposable = enumerator1 as IDisposable;
          disposable?.Dispose();
        }
        IEnumerator enumerator2 = ThemeRepository.registeredThemes.Keys.GetEnumerator();
        try
        {
          while (enumerator2.MoveNext())
          {
            string name = (string) enumerator2.Current;
            yield return name;
          }
        }
        finally
        {
          IDisposable disposable = enumerator2 as IDisposable;
          disposable?.Dispose();
        }
      }
    }

    public static Theme FindTheme(string themeName)
    {
      return ThemeRepository.FindTheme(themeName, true);
    }

    public static Theme FindTheme(string themeName, bool fallbackToControlDefault)
    {
      lock (ThemeRepository.lockObject)
      {
        if (string.IsNullOrEmpty(themeName) || themeName == "ControlDefault")
          return ThemeRepository.ControlDefault;
        if (ThemeRepository.themes.Contains((object) themeName))
          return (Theme) ThemeRepository.themes[(object) themeName];
        if (ThemeRepository.registeredThemes.Contains((object) themeName))
        {
          RadThemeComponentBase registeredTheme = (RadThemeComponentBase) ThemeRepository.registeredThemes[(object) themeName];
          ThemeRepository.registeredThemes.Remove((object) themeName);
          registeredTheme.Load();
          if (registeredTheme.Site == null && !registeredTheme.IsDesignMode)
            registeredTheme.Dispose();
          return ThemeRepository.FindTheme(themeName, fallbackToControlDefault);
        }
        if (fallbackToControlDefault)
          return ThemeRepository.ControlDefault;
      }
      return (Theme) null;
    }

    public static void Add(Theme theme)
    {
      ThemeRepository.Add(theme, true);
    }

    public static void Add(Theme theme, bool replaceExistingStyle)
    {
      lock (ThemeRepository.lockObject)
      {
        string name = theme.Name;
        char[] chArray = new char[1]{ ',' };
        foreach (string str in name.Split(chArray))
        {
          string themeName = str.Trim();
          if (themeName == "*" || themeName == "ControlDefault")
          {
            if (theme != ThemeRepository.ControlDefault)
            {
              if (ThemeRepository.ControlDefault != null)
              {
                ThemeRepository.ControlDefault.Combine(theme, true, replaceExistingStyle);
              }
              else
              {
                ThemeRepository.controlDefault = theme;
                theme.MergeRepositories();
              }
            }
          }
          else
          {
            Theme theme1 = ThemeRepository.FindTheme(themeName, false);
            if (theme1 != null)
            {
              if (theme != theme1)
                theme1.Combine(theme, true, replaceExistingStyle);
            }
            else
            {
              theme.MergeRepositories();
              ThemeRepository.themes.Add((object) themeName, (object) theme);
            }
          }
        }
      }
      ThemeResolutionService.RaiseThemeChanged(theme.Name, (string) null);
    }

    public static void Remove(string themeName)
    {
      lock (ThemeRepository.lockObject)
      {
        if (ThemeRepository.registeredThemes.Contains((object) themeName))
          ThemeRepository.registeredThemes.Remove((object) themeName);
        if (!ThemeRepository.themes.Contains((object) themeName))
          return;
        ThemeRepository.themes.Remove((object) themeName);
        ThemeResolutionService.RaiseThemeChanged(themeName, (string) null);
      }
    }

    public static void RegisterTheme(RadThemeComponentBase theme)
    {
      lock (ThemeRepository.lockObject)
      {
        if (ThemeRepository.registeredThemes.Contains((object) theme.ThemeName))
          return;
        ThemeRepository.registeredThemes.Add((object) theme.ThemeName, (object) theme);
      }
    }

    internal static void LoadRegisteredThemes()
    {
      lock (ThemeRepository.lockObject)
      {
        List<RadThemeComponentBase> themeComponentBaseList = new List<RadThemeComponentBase>();
        foreach (RadThemeComponentBase themeComponentBase in (IEnumerable) ThemeRepository.registeredThemes.Values)
          themeComponentBaseList.Add(themeComponentBase);
        foreach (RadThemeComponentBase themeComponentBase in themeComponentBaseList)
          themeComponentBase.Load();
        ThemeRepository.registeredThemes.Clear();
      }
    }
  }
}
