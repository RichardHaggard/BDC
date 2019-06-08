// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ThemeResolutionService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls
{
  public class ThemeResolutionService
  {
    private static object syncRoot = new object();
    private static bool allowAnimations = true;
    private static int themeChangeSuspendCounter = 0;
    private static string applicationThemeName = (string) null;
    private static Hashtable registeredBuildersName = new Hashtable();
    private static Hashtable registeredBuildersByElementNameControlID = new Hashtable();
    private static Hashtable registeredBuildersByElementNameControlType = new Hashtable();
    private static Hashtable registeredBuildersByElementTypeControlID = new Hashtable();
    private static Hashtable registeredBuildersByElementTypeControlType = new Hashtable();
    private static Hashtable registeredBuildersDefaultByElementType = new Hashtable();
    private static Hashtable registeredStyleRepositoriesByThemeName = new Hashtable();
    private static WeakReferenceList<IThemeChangeListener> themeChangeListeners = new WeakReferenceList<IThemeChangeListener>(true);
    private static LinkedList<ThemeResolutionService.ThemeChangeInfo> themesChangeDuringSuspend = new LinkedList<ThemeResolutionService.ThemeChangeInfo>();
    private static PrivateFontCollection customFonts = new PrivateFontCollection();
    private const string asteriskThemeName = "*";

    static ThemeResolutionService()
    {
      string str = "Telerik.WinControls.Resources.";
      ThemeResolutionService.LoadFont(str + "Roboto-Medium.ttf");
      ThemeResolutionService.LoadFont(str + "Roboto-Regular.ttf");
      ThemeResolutionService.LoadFont(str + "TelerikWebUI.ttf");
      ThemeResolutionService.LoadFont(str + "WebComponentsIcons.ttf");
      ThemeResolutionService.LoadFont(str + "Awesome-Brands-Regular.ttf");
      ThemeResolutionService.LoadFont(str + "Awesome-Regular.ttf");
      ThemeResolutionService.LoadFont(str + "Awesome-Solid.ttf");
    }

    public static FontFamily GetCustomFont(string fontName)
    {
      if (string.IsNullOrEmpty(fontName))
        return (FontFamily) null;
      foreach (FontFamily family in ThemeResolutionService.customFonts.Families)
      {
        if (family.Name == fontName)
          return family;
      }
      return (FontFamily) null;
    }

    public static FontFamily[] GetCustomFonts()
    {
      return ThemeResolutionService.customFonts.Families;
    }

    [DllImport("Gdi32.dll")]
    private static extern IntPtr AddFontMemResourceEx(
      IntPtr pbFont,
      uint cbFont,
      IntPtr pdv,
      [In] ref uint pcFonts);

    public static int LoadFont(string resourcePath)
    {
      return ThemeResolutionService.LoadFont(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath));
    }

    public static int LoadFont(Stream fontStream)
    {
      IntPtr num = Marshal.AllocCoTaskMem((int) fontStream.Length);
      byte[] numArray = new byte[fontStream.Length];
      fontStream.Read(numArray, 0, (int) fontStream.Length);
      Marshal.Copy(numArray, 0, num, (int) fontStream.Length);
      uint pcFonts = 0;
      ThemeResolutionService.AddFontMemResourceEx(num, (uint) numArray.Length, IntPtr.Zero, ref pcFonts);
      ThemeResolutionService.customFonts.AddMemoryFont(num, (int) fontStream.Length);
      fontStream.Close();
      Marshal.FreeCoTaskMem(num);
      return (int) pcFonts;
    }

    public static event ThemeChangedHandler ApplicationThemeChanged;

    public static string ApplicationThemeName
    {
      get
      {
        return ThemeResolutionService.applicationThemeName;
      }
      set
      {
        if (!(ThemeResolutionService.applicationThemeName != value))
          return;
        ThemeResolutionService.applicationThemeName = value;
        ThemeResolutionService.RaiseThemeChanged(ThemeResolutionService.applicationThemeName, (string) null);
        if (ThemeResolutionService.ApplicationThemeChanged == null)
          return;
        ThemeResolutionService.ApplicationThemeChanged((object) null, new ThemeChangedEventArgs(ThemeResolutionService.applicationThemeName));
      }
    }

    public static bool AllowAnimations
    {
      get
      {
        return ThemeResolutionService.allowAnimations;
      }
      set
      {
        ThemeResolutionService.allowAnimations = value;
      }
    }

    public static string ControlDefaultThemeName
    {
      get
      {
        return "ControlDefault";
      }
    }

    public static string SystemThemeName
    {
      get
      {
        return "System";
      }
    }

    public static void SubscribeForThemeChanged(IThemeChangeListener listener)
    {
      lock (ThemeResolutionService.syncRoot)
        ThemeResolutionService.themeChangeListeners.Add(listener);
    }

    public static void UnsubscribeFromThemeChanged(IThemeChangeListener listener)
    {
      lock (ThemeResolutionService.syncRoot)
        ThemeResolutionService.themeChangeListeners.Remove(listener);
    }

    public static ICollection GetAvailableThemes()
    {
      return ThemeRepository.LoadedThemes;
    }

    public static List<Theme> GetAvailableThemes(IComponentTreeHandler control)
    {
      List<Theme> themeList = new List<Theme>();
      if (control == null)
        return themeList;
      ThemeRepository.LoadRegisteredThemes();
      foreach (Theme loadedTheme in (IEnumerable) ThemeRepository.LoadedThemes)
      {
        if (!(loadedTheme.Name == "*"))
        {
          foreach (StyleGroup styleGroup in loadedTheme.StyleGroups)
          {
            if (styleGroup.IsCompatible(control as Control))
            {
              themeList.Add(loadedTheme);
              break;
            }
          }
        }
      }
      return themeList;
    }

    public static Theme GetTheme(string themeName)
    {
      return ThemeRepository.FindTheme(themeName);
    }

    public static void ApplyThemeToControlTree(Control control, string themeName)
    {
      IComponentTreeHandler componentTreeHandler = control as IComponentTreeHandler;
      if (componentTreeHandler != null)
        componentTreeHandler.ThemeName = themeName;
      foreach (Control control1 in (ArrangedElementCollection) control.Controls)
        ThemeResolutionService.ApplyThemeToControlTree(control1, themeName);
    }

    public static bool LoadPackageFile(string filePath)
    {
      Theme theme = new TSSPThemeReader().Read(filePath);
      if (theme == null)
        return false;
      ThemeRepository.Add(theme);
      return true;
    }

    public static bool LoadPackageFile(string filePath, bool throwOnError)
    {
      if (!File.Exists(filePath))
      {
        if (throwOnError)
          throw new FileNotFoundException("File '" + filePath + "' does not exist.");
        return false;
      }
      if (new FileInfo(filePath).Extension != ".tssp")
      {
        if (throwOnError)
          throw new ArgumentException("Provided file is not a valid RadThemePackage");
        return false;
      }
      Theme theme = new TSSPThemeReader().Read(filePath);
      if (theme == null)
        return false;
      ThemeRepository.Add(theme);
      return true;
    }

    public static bool LoadPackageResource(string resourcePath)
    {
      return ThemeResolutionService.LoadPackageResource(new ThemeResolutionService.ResourceParams()
      {
        CallingAssembly = Assembly.GetCallingAssembly(),
        ExecutingAssembly = Assembly.GetExecutingAssembly(),
        ResourcePath = resourcePath
      }, true);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool LoadPackageResource(Assembly sourceAssembly, string resourcePath)
    {
      return ThemeResolutionService.LoadPackageResource(new ThemeResolutionService.ResourceParams()
      {
        UserAssembly = sourceAssembly,
        ResourcePath = resourcePath
      }, true);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool LoadPackageResource(
      ThemeResolutionService.ResourceParams resourceParams,
      bool throwOnError)
    {
      Assembly assembly = resourceParams.UserAssembly;
      string resourcePath = resourceParams.ResourcePath;
      if ((object) assembly == null)
      {
        assembly = resourceParams.CallingAssembly;
        if ((object) assembly == null)
          assembly = Assembly.GetCallingAssembly();
      }
      Stream manifestResourceStream = assembly.GetManifestResourceStream(resourcePath);
      if (manifestResourceStream == null)
      {
        Assembly executingAssembly = resourceParams.ExecutingAssembly;
        if ((object) executingAssembly == null)
          executingAssembly = Assembly.GetExecutingAssembly();
        manifestResourceStream = executingAssembly.GetManifestResourceStream(resourcePath);
      }
      if (manifestResourceStream == null)
      {
        if (throwOnError)
          throw new ArgumentException("Specified resource does not exist in the provided assembly.");
        return false;
      }
      using (manifestResourceStream)
      {
        Theme theme = new TSSPThemeReader().Read(manifestResourceStream);
        if (theme != null)
        {
          ThemeRepository.Add(theme);
          return true;
        }
      }
      return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RegisterThemeFromStorage(ThemeStorageType storageType, string themeLocation)
    {
      XMLThemeReader xmlThemeReader = new XMLThemeReader();
      if (storageType == ThemeStorageType.File)
      {
        Theme theme = xmlThemeReader.Read(themeLocation);
        if (theme == null)
          return;
        ThemeRepository.Add(theme);
      }
      else
      {
        using (Stream manifestResourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(themeLocation))
        {
          Theme theme = xmlThemeReader.Read(manifestResourceStream);
          if (theme == null || string.IsNullOrEmpty(theme.Name))
            return;
          ThemeRepository.Add(theme);
        }
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RegisterThemeFromStorage(
      ThemeStorageType storageType,
      Assembly assembly,
      string themeLocation)
    {
      ThemeResolutionService.RegisterThemeFromStorage(storageType, new ThemeResolutionService.ResourceParams()
      {
        CallingAssembly = Assembly.GetCallingAssembly(),
        ExecutingAssembly = Assembly.GetExecutingAssembly(),
        ResourcePath = themeLocation,
        UserAssembly = assembly
      });
    }

    public static void RegisterThemeFromStorage(
      ThemeStorageType storageType,
      ThemeResolutionService.ResourceParams resourceParams)
    {
      ThemeSource themeSource = new ThemeSource();
      Assembly assembly = resourceParams.UserAssembly;
      if ((object) assembly == null)
        assembly = resourceParams.CallingAssembly;
      themeSource.CallingAssembly = assembly;
      themeSource.StorageType = storageType;
      themeSource.ThemeLocation = resourceParams.ResourcePath;
      themeSource.ReloadThemeFromStorage();
    }

    public static void SuspendThemeChange()
    {
      ++ThemeResolutionService.themeChangeSuspendCounter;
    }

    public static void ResumeThemeChange()
    {
      ThemeResolutionService.ResumeThemeChange(true);
    }

    public static void ResumeThemeChange(bool raiseChanged)
    {
      lock (ThemeResolutionService.syncRoot)
      {
        if (ThemeResolutionService.themeChangeSuspendCounter > 0)
          --ThemeResolutionService.themeChangeSuspendCounter;
        if (ThemeResolutionService.themeChangeSuspendCounter != 0)
          return;
        if (raiseChanged)
        {
          foreach (ThemeResolutionService.ThemeChangeInfo themeChangeInfo in ThemeResolutionService.themesChangeDuringSuspend)
            ThemeResolutionService.RaiseThemeChanged(themeChangeInfo.ThemeName, themeChangeInfo.TargetThemeClassName);
        }
        ThemeResolutionService.themesChangeDuringSuspend.Clear();
      }
    }

    public static void RaiseThemeChanged(string themeName, string controlThemeClassName)
    {
      lock (ThemeResolutionService.syncRoot)
      {
        if (ThemeResolutionService.themeChangeSuspendCounter > 0)
        {
          ThemeResolutionService.ThemeChangeInfo themeChangeInfo = new ThemeResolutionService.ThemeChangeInfo(themeName, controlThemeClassName);
          if (ThemeResolutionService.themesChangeDuringSuspend.Contains(themeChangeInfo))
            return;
          ThemeResolutionService.themesChangeDuringSuspend.AddLast(themeChangeInfo);
        }
        else
        {
          ThemeChangedEventArgs e = new ThemeChangedEventArgs(themeName);
          foreach (IThemeChangeListener themeChangeListener in ThemeResolutionService.themeChangeListeners)
          {
            if (string.IsNullOrEmpty(controlThemeClassName) || string.CompareOrdinal(controlThemeClassName, themeChangeListener.ControlThemeClassName) == 0)
              themeChangeListener.OnThemeChanged(e);
          }
        }
      }
    }

    public static void RemoveThemeRegistration(string themeName)
    {
      ThemeRepository.Remove(themeName);
    }

    public static event ResolveStyleBuilderEventHandler ResolveStyleBuilder;

    public static Theme EnsureThemeRegistered(string themeName)
    {
      Theme theme = ThemeRepository.FindTheme(themeName);
      if (theme == null)
      {
        theme = new Theme(themeName);
        ThemeRepository.Add(theme);
      }
      return theme;
    }

    public static void ClearTheme(string themeName)
    {
      lock (ThemeResolutionService.syncRoot)
      {
        ArrayList arrayList = new ArrayList();
        foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersByElementTypeControlType)
        {
          ThemeResolutionService.TripleNameKey key = (ThemeResolutionService.TripleNameKey) dictionaryEntry.Key;
          if (string.Compare(key.Name3, themeName, true) == 0)
            arrayList.Add((object) key);
        }
        foreach (object key in arrayList)
          ThemeResolutionService.registeredBuildersByElementTypeControlType.Remove(key);
        arrayList.Clear();
        foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersByElementNameControlType)
        {
          ThemeResolutionService.TripleNameKey key = (ThemeResolutionService.TripleNameKey) dictionaryEntry.Key;
          if (string.Compare(key.Name3, themeName, true) == 0)
            arrayList.Add((object) key);
        }
        foreach (object key in arrayList)
          ThemeResolutionService.registeredBuildersByElementNameControlType.Remove(key);
        arrayList.Clear();
        foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersByElementTypeControlID)
        {
          ThemeResolutionService.TripleNameKey key = (ThemeResolutionService.TripleNameKey) dictionaryEntry.Key;
          if (string.Compare(key.Name3, themeName, true) == 0)
            arrayList.Add((object) key);
        }
        foreach (object key in arrayList)
          ThemeResolutionService.registeredBuildersByElementTypeControlID.Remove(key);
        arrayList.Clear();
        foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersDefaultByElementType)
        {
          ThemeResolutionService.TripleNameKey key = (ThemeResolutionService.TripleNameKey) dictionaryEntry.Key;
          if (string.Compare(key.Name3, themeName, true) == 0)
            arrayList.Add((object) key);
        }
        foreach (object key in arrayList)
          ThemeResolutionService.registeredBuildersDefaultByElementType.Remove(key);
        arrayList.Clear();
        foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersName)
        {
          string key = (string) dictionaryEntry.Key;
          if (string.Compare(key, themeName, true) == 0)
            arrayList.Add((object) key);
        }
        foreach (object key in arrayList)
          ThemeResolutionService.registeredBuildersName.Remove(key);
      }
      ThemeResolutionService.RaiseThemeChanged(themeName, (string) null);
    }

    public static StyleGroup[] GetStyleSheetBuilders(string themeName)
    {
      if (string.IsNullOrEmpty(themeName))
        themeName = ThemeResolutionService.ControlDefaultThemeName;
      ArrayList res = new ArrayList();
      foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersDefaultByElementType)
      {
        ThemeResolutionService.TripleNameKey key = (ThemeResolutionService.TripleNameKey) dictionaryEntry.Key;
        if (string.Compare(key.Name2, themeName, true) == 0)
        {
          StyleGroup builder = (StyleGroup) dictionaryEntry.Value;
          BuilderRegistrationType regType = BuilderRegistrationType.ElementTypeDefault;
          string name3 = key.Name3;
          string controlType = (string) null;
          string elementName = (string) null;
          string controlName = (string) null;
          ThemeResolutionService.AddBuilderToList(res, builder, regType, name3, controlType, elementName, controlName);
        }
      }
      foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersByElementTypeControlType)
      {
        ThemeResolutionService.TripleNameKey key = (ThemeResolutionService.TripleNameKey) dictionaryEntry.Key;
        if (string.Compare(key.Name3, themeName, true) == 0)
        {
          StyleGroup builder = (StyleGroup) dictionaryEntry.Value;
          BuilderRegistrationType regType = BuilderRegistrationType.ElementTypeControlType;
          string name2 = key.Name2;
          string name1 = key.Name1;
          string elementName = (string) null;
          string controlName = (string) null;
          ThemeResolutionService.AddBuilderToList(res, builder, regType, name2, name1, elementName, controlName);
        }
      }
      foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersName)
      {
        if (string.Compare((string) dictionaryEntry.Key, themeName, true) == 0)
        {
          StyleGroup builder = (StyleGroup) dictionaryEntry.Value;
          BuilderRegistrationType regType = BuilderRegistrationType.ElementTypeGlobal;
          string elementType = (string) null;
          string controlType = (string) null;
          string elementName = (string) null;
          string controlName = (string) null;
          ThemeResolutionService.AddBuilderToList(res, builder, regType, elementType, controlType, elementName, controlName);
        }
      }
      StyleGroup[] styleGroupArray = new StyleGroup[res.Count];
      res.CopyTo((Array) styleGroupArray, 0);
      return styleGroupArray;
    }

    public static StyleGroup GetStyleSheetBuilder(RadElement element)
    {
      return ThemeResolutionService.GetStyleSheetBuilder(element, (string) null);
    }

    public static StyleGroup GetStyleSheetBuilder(
      RadElement element,
      string proposedThemeName)
    {
      if (!element.CanHaveOwnStyle)
        return (StyleGroup) null;
      IComponentTreeHandler control = (IComponentTreeHandler) null;
      string fullName = element.GetThemeEffectiveType().FullName;
      string name = element.Name;
      bool rootElementHasStyle = !(element is RootRadElement) && element.ElementTree != null;
      StyleSheet rootElementStyle = (StyleSheet) null;
      if (rootElementHasStyle)
        rootElementStyle = element.ElementTree.RootElement.Style;
      return ThemeResolutionService.GetStyleSheetBuilder(control, fullName, name, proposedThemeName, rootElementHasStyle, rootElementStyle);
    }

    public static StyleGroup GetStyleSheetBuilder(
      IComponentTreeHandler control,
      string elementTypeFullName,
      string elementName,
      string proposedThemeName)
    {
      return ThemeResolutionService.GetStyleSheetBuilder(control, elementTypeFullName, elementName, proposedThemeName, false, (StyleSheet) null);
    }

    private static void AddBuilderToList(
      ArrayList res,
      StyleGroup builder,
      BuilderRegistrationType regType,
      string elementType,
      string controlType,
      string elementName,
      string controlName)
    {
      bool flag = false;
      foreach (StyleGroup re in res)
      {
        if (re == builder)
        {
          StyleRegistration styleRegistration = new StyleRegistration(regType.ToString(), elementType, controlType, elementName, controlName);
          re.Registrations.Add(styleRegistration);
          flag = true;
        }
      }
      if (flag)
        return;
      StyleGroup styleGroup = new StyleGroup();
      StyleRegistration styleRegistration1 = new StyleRegistration(regType.ToString(), elementType, controlType, elementName, controlName);
      styleGroup.Registrations.Add(styleRegistration1);
      res.Add((object) styleGroup);
    }

    public static void RegisterStyleBuilder(StyleGroup styleBuilderRegistration, string themeName)
    {
      foreach (StyleRegistration registration in styleBuilderRegistration.Registrations)
      {
        switch (registration.RegistrationType)
        {
          case "ElementTypeDefault":
            ThemeResolutionService.RegisterElementTypeDefaultStyleBuilder(themeName, registration.ElementType, styleBuilderRegistration);
            continue;
          case "ElementTypeControlType":
            ThemeResolutionService.RegisterControlStyleBuilder(registration.ControlType, registration.ElementType, styleBuilderRegistration, themeName);
            continue;
          case "ElementTypeControlName":
            ThemeResolutionService.RegisterStyleBuilderByControlName(registration.ControlName, registration.ElementType, styleBuilderRegistration, themeName);
            continue;
          default:
            continue;
        }
      }
      ThemeResolutionService.EnsureThemeRegistered(themeName);
    }

    public static void RegisterElementTypeDefaultStyleBuilder(
      string themeName,
      string elementTypeName,
      StyleGroup builder)
    {
      ThemeResolutionService.EnsureThemeRegistered(ThemeResolutionService.ControlDefaultThemeName);
      ThemeResolutionService.TripleNameKey tripleNameKey = new ThemeResolutionService.TripleNameKey("", themeName, elementTypeName);
      ThemeResolutionService.registeredBuildersDefaultByElementType[(object) tripleNameKey] = (object) builder;
    }

    public static void RegisterControlStyleBuilder(
      string controlTypeName,
      string elementTypeName,
      StyleGroup builder,
      string themeName)
    {
      ThemeResolutionService.EnsureThemeRegistered(themeName);
      ThemeResolutionService.TripleNameKey tripleNameKey = new ThemeResolutionService.TripleNameKey(controlTypeName, elementTypeName, themeName);
      ThemeResolutionService.registeredBuildersByElementTypeControlType[(object) tripleNameKey] = (object) builder;
      ThemeResolutionService.RaiseThemeChanged(themeName, controlTypeName);
    }

    public static void RegisterStyleBuilderByControlName(
      string controlName,
      string elementTypeName,
      StyleGroup builder,
      string themeName)
    {
      ThemeResolutionService.EnsureThemeRegistered(themeName);
      ThemeResolutionService.TripleNameKey tripleNameKey = new ThemeResolutionService.TripleNameKey("__ID" + controlName, elementTypeName, themeName);
      ThemeResolutionService.registeredBuildersByElementTypeControlID[(object) tripleNameKey] = (object) builder;
    }

    public static XmlStyleRepository GetThemeRepository(
      string themeName,
      bool useAsterikTheme,
      bool useAppThemeName)
    {
      if (useAppThemeName && !string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName))
        themeName = ThemeResolutionService.ApplicationThemeName;
      else if (string.IsNullOrEmpty(themeName))
        themeName = ThemeResolutionService.ControlDefaultThemeName;
      XmlStyleRepository xmlStyleRepository = ThemeResolutionService.registeredStyleRepositoriesByThemeName[(object) themeName] as XmlStyleRepository;
      if (xmlStyleRepository == null && useAsterikTheme)
        xmlStyleRepository = ThemeResolutionService.registeredStyleRepositoriesByThemeName[(object) "*"] as XmlStyleRepository;
      return xmlStyleRepository;
    }

    public static void UnregisterStyleSheetBuilder(StyleGroup builder)
    {
      foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersDefaultByElementType)
      {
        if (object.ReferenceEquals((object) builder, dictionaryEntry.Value))
        {
          ThemeResolutionService.registeredBuildersDefaultByElementType.Remove(dictionaryEntry.Key);
          return;
        }
      }
      foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersByElementTypeControlType)
      {
        if (object.ReferenceEquals((object) builder, dictionaryEntry.Value))
        {
          ThemeResolutionService.registeredBuildersByElementTypeControlType.Remove(dictionaryEntry.Key);
          return;
        }
      }
      foreach (DictionaryEntry dictionaryEntry in ThemeResolutionService.registeredBuildersName)
      {
        if (object.ReferenceEquals((object) builder, dictionaryEntry.Value))
        {
          ThemeResolutionService.registeredBuildersName.Remove(dictionaryEntry.Key);
          break;
        }
      }
    }

    public static void RegisterThemeRepository(XmlStyleRepository styleRepository, string themeName)
    {
      XmlStyleRepository xmlStyleRepository = ThemeResolutionService.registeredStyleRepositoriesByThemeName[(object) themeName] as XmlStyleRepository;
      if (xmlStyleRepository != null)
        xmlStyleRepository.MergeWith(styleRepository);
      else
        ThemeResolutionService.registeredStyleRepositoriesByThemeName[(object) themeName] = (object) styleRepository;
      ThemeResolutionService.RaiseThemeChanged(themeName, (string) null);
    }

    private static StyleGroup GetStyleSheetBuilder(
      IComponentTreeHandler control,
      string elementTypeFullName,
      string elementName,
      string proposedThemeName,
      bool rootElementHasStyle,
      StyleSheet rootElementStyle)
    {
      string controlType = typeof (RadControl).FullName;
      string themeName = (string) null;
      string controlID = string.Empty;
      if (control != null)
      {
        themeName = control.ThemeName;
        Control control1 = control as Control;
        if (control1 != null)
          controlID = control1.Name;
        controlType = control.ThemeClassName;
      }
      StyleGroup styleGroup = (StyleGroup) null;
      if (ThemeResolutionService.applicationThemeName != null)
        styleGroup = ThemeResolutionService.LookUpStyleBuilder(controlID, controlType, elementName, elementTypeFullName, rootElementHasStyle, rootElementStyle, ThemeResolutionService.applicationThemeName);
      bool flag = false;
      if (styleGroup == null && !string.IsNullOrEmpty(proposedThemeName))
      {
        flag = true;
        styleGroup = ThemeResolutionService.LookUpStyleBuilder(controlID, controlType, elementName, elementTypeFullName, rootElementHasStyle, rootElementStyle, proposedThemeName);
      }
      if (!flag && styleGroup == null && !string.IsNullOrEmpty(themeName))
      {
        flag = true;
        styleGroup = ThemeResolutionService.LookUpStyleBuilder(controlID, controlType, elementName, elementTypeFullName, rootElementHasStyle, rootElementStyle, themeName);
      }
      if (!flag && styleGroup == null)
        styleGroup = ThemeResolutionService.LookUpStyleBuilder(controlID, controlType, elementName, elementTypeFullName, rootElementHasStyle, rootElementStyle, ThemeResolutionService.ControlDefaultThemeName);
      if (styleGroup == null)
        styleGroup = ThemeResolutionService.LookUpStyleBuilder(controlID, controlType, elementName, elementTypeFullName, rootElementHasStyle, rootElementStyle, "*");
      return styleGroup;
    }

    private static StyleGroup LookUpStyleBuilder(
      string controlID,
      string controlType,
      string elementName,
      string elementTypeFullName,
      bool rootElementHasStyle,
      StyleSheet rootElementStyle,
      string themeName)
    {
      ThemeResolutionService.TripleNameKey tripleNameKey1 = new ThemeResolutionService.TripleNameKey(elementName ?? "", themeName ?? "", elementTypeFullName);
      StyleGroup builder = ThemeResolutionService.registeredBuildersByElementNameControlID[(object) tripleNameKey1] as StyleGroup;
      int num = 0;
      if (builder == null)
      {
        ThemeResolutionService.TripleNameKey tripleNameKey2 = new ThemeResolutionService.TripleNameKey(controlID ?? "", themeName ?? "", elementTypeFullName);
        builder = ThemeResolutionService.registeredBuildersByElementTypeControlID[(object) tripleNameKey2] as StyleGroup;
        num = 1;
      }
      if (builder == null)
      {
        ThemeResolutionService.TripleNameKey tripleNameKey2 = new ThemeResolutionService.TripleNameKey(controlType, elementTypeFullName, themeName ?? "");
        builder = ThemeResolutionService.registeredBuildersByElementTypeControlID[(object) tripleNameKey2] as StyleGroup;
        num = 2;
      }
      if (builder == null)
      {
        ThemeResolutionService.TripleNameKey tripleNameKey2 = new ThemeResolutionService.TripleNameKey(controlType, elementTypeFullName, themeName ?? "");
        builder = ThemeResolutionService.registeredBuildersByElementTypeControlType[(object) tripleNameKey2] as StyleGroup;
        num = 3;
      }
      if (builder == null)
      {
        ThemeResolutionService.TripleNameKey tripleNameKey2 = new ThemeResolutionService.TripleNameKey("", themeName ?? "", elementTypeFullName);
        builder = ThemeResolutionService.registeredBuildersDefaultByElementType[(object) tripleNameKey2] as StyleGroup;
        num = 4;
      }
      if (builder == null && themeName != null)
      {
        builder = ThemeResolutionService.registeredBuildersName[(object) themeName] as StyleGroup;
        num = 5;
      }
      ResolveStyleBuilderEventHandler resolveStyleBuilder = ThemeResolutionService.ResolveStyleBuilder;
      if (resolveStyleBuilder != null)
      {
        ResolveStyleBuilderEventArgs e = new ResolveStyleBuilderEventArgs(themeName, builder);
        resolveStyleBuilder((object) null, e);
        builder = e.Builder;
      }
      return builder;
    }

    private class ThemeChangeInfo
    {
      private string themeName;
      private string targetThemeClassName;

      public ThemeChangeInfo(string themeName, string targetThemeClassName)
      {
        this.themeName = themeName;
        this.targetThemeClassName = targetThemeClassName;
      }

      public string ThemeName
      {
        get
        {
          return this.themeName;
        }
      }

      public string TargetThemeClassName
      {
        get
        {
          return this.targetThemeClassName;
        }
      }

      public override bool Equals(object obj)
      {
        ThemeResolutionService.ThemeChangeInfo themeChangeInfo = obj as ThemeResolutionService.ThemeChangeInfo;
        if (themeChangeInfo == null || !(themeChangeInfo.themeName == this.themeName))
          return false;
        return themeChangeInfo.targetThemeClassName == this.targetThemeClassName;
      }

      public override int GetHashCode()
      {
        return this.themeName.GetHashCode() ^ this.targetThemeClassName.GetHashCode();
      }
    }

    public class ResourceParams
    {
      public string ResourcePath;
      public Assembly CallingAssembly;
      public Assembly ExecutingAssembly;
      public Assembly UserAssembly;
    }

    private class TripleNameKey
    {
      private int hashCode;
      private string name1;
      private string name2;
      private string name3;

      public TripleNameKey(string name1, string name2, string name3)
      {
        this.name1 = name1;
        this.name2 = name2;
        this.name3 = name3;
        this.hashCode = this.Name1.ToUpper().GetHashCode() ^ this.Name2.ToUpper().GetHashCode() ^ this.Name3.GetHashCode();
      }

      public string Name1
      {
        get
        {
          return this.name1;
        }
      }

      public string Name2
      {
        get
        {
          return this.name2;
        }
      }

      public string Name3
      {
        get
        {
          return this.name3;
        }
      }

      public override bool Equals(object o)
      {
        if (o != null && o is ThemeResolutionService.TripleNameKey)
          return this.Equals((ThemeResolutionService.TripleNameKey) o);
        return false;
      }

      public bool Equals(ThemeResolutionService.TripleNameKey key)
      {
        if (string.Compare(this.Name1, key.Name1, true) == 0 && string.Compare(this.Name2, key.Name2, true) == 0)
          return string.Compare(this.Name3, key.Name3, true) == 0;
        return false;
      }

      public override int GetHashCode()
      {
        return this.hashCode;
      }
    }
  }
}
