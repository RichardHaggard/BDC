// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Localization.LocalizationProvider`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls.Localization
{
  public abstract class LocalizationProvider<T> where T : class, new()
  {
    private static T currentProvider = LocalizationProvider<T>.CreateDefaultLocalizationProvider();

    public static event EventHandler CurrentProviderChanged;

    private static void OnCurrentProviderChanged()
    {
      EventHandler currentProviderChanged = LocalizationProvider<T>.CurrentProviderChanged;
      if (currentProviderChanged == null)
        return;
      currentProviderChanged((object) LocalizationProvider<T>.currentProvider, EventArgs.Empty);
    }

    [Browsable(false)]
    public static T CurrentProvider
    {
      get
      {
        return LocalizationProvider<T>.currentProvider;
      }
      set
      {
        LocalizationProvider<T>.currentProvider = (object) value != null ? value : LocalizationProvider<T>.CreateDefaultLocalizationProvider();
        LocalizationProvider<T>.OnCurrentProviderChanged();
      }
    }

    public static T CreateDefaultLocalizationProvider()
    {
      return new T();
    }

    public virtual CultureInfo Culture
    {
      get
      {
        return CultureInfo.CreateSpecificCulture("en-US");
      }
    }

    public abstract string GetLocalizedString(string id);
  }
}
