// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ItemStateManagerFactoryRegistry
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls
{
  public sealed class ItemStateManagerFactoryRegistry
  {
    private static Type RadItemType = typeof (RadItem);
    private static Type StylableElement = typeof (IStylableElement);
    private static Dictionary<Type, ItemStateManagerFactoryBase> stateManagersByElementThemeEffectiveType = new Dictionary<Type, ItemStateManagerFactoryBase>();

    private ItemStateManagerFactoryRegistry()
    {
    }

    public static void AddStateManagerFactory(
      ItemStateManagerFactoryBase stateManagerFactory,
      Type themeType)
    {
      ItemStateManagerFactoryRegistry.stateManagersByElementThemeEffectiveType[themeType] = stateManagerFactory;
    }

    public static ItemStateManagerFactoryBase GetStateManagerFactory(
      Type themeType)
    {
      return ItemStateManagerFactoryRegistry.GetStateManagerFactory(themeType, false);
    }

    public static ItemStateManagerFactoryBase GetStateManagerFactory(
      Type themeType,
      bool searchBaseTypes)
    {
      if ((object) themeType == (object) ItemStateManagerFactoryRegistry.RadItemType)
        return (ItemStateManagerFactoryBase) null;
      if (!ItemStateManagerFactoryRegistry.StylableElement.IsAssignableFrom(themeType) && !ItemStateManagerFactoryRegistry.RadItemType.IsAssignableFrom(themeType))
        throw new ArgumentException("Only IStylableElement inheritors may have StateManager registered");
      ItemStateManagerFactoryBase managerFactoryBase;
      ItemStateManagerFactoryRegistry.stateManagersByElementThemeEffectiveType.TryGetValue(themeType, out managerFactoryBase);
      if (managerFactoryBase == null && searchBaseTypes)
      {
        Type type = typeof (RadItem);
        for (Type baseType = themeType.BaseType; (object) baseType != (object) type; baseType = baseType.BaseType)
        {
          ItemStateManagerFactoryRegistry.stateManagersByElementThemeEffectiveType.TryGetValue(baseType, out managerFactoryBase);
          if (managerFactoryBase != null)
            break;
        }
      }
      return managerFactoryBase;
    }
  }
}
