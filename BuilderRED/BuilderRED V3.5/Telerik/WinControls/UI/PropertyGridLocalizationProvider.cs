// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class PropertyGridLocalizationProvider : LocalizationProvider<PropertyGridLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "ContextMenuReset":
          return "Reset";
        case "ContextMenuEdit":
          return "Edit";
        case "ContextMenuExpand":
          return "Expand";
        case "ContextMenuCollapse":
          return "Collapse";
        case "ContextMenuShowDescription":
          return "Show description";
        case "ContextMenuShowToolbar":
          return "Show toolbar";
        case "ContextMenuSort":
          return "Sort";
        case "ContextMenuNoSort":
          return "No Sort";
        case "ContextMenuAlphabetical":
          return "Alphabetical";
        case "ContextMenuCategorized":
          return "Categorized";
        case "ContextMenuCategorizedAlphabetical":
          return "Categorized Alphabetical";
        default:
          return "";
      }
    }
  }
}
