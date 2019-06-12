// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class GanttViewLocalizationProvider : LocalizationProvider<GanttViewLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "ContextMenuAdd":
          return "&Add";
        case "ContextMenuAddChild":
          return "Add &Child";
        case "ContextMenuAddSibling":
          return "Add &Sibling";
        case "ContextMenuDelete":
          return "&Delete";
        case "ContextMenuProgress":
          return "&Progress";
        case "TimelineWeek":
          return "Week";
        default:
          return string.Empty;
      }
    }
  }
}
