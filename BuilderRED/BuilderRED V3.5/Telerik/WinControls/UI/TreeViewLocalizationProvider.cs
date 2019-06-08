// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class TreeViewLocalizationProvider : LocalizationProvider<TreeViewLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "ContextMenuCollapse":
          return "C&ollapse";
        case "ContextMenuDelete":
          return "&Delete";
        case "ContextMenuEdit":
          return "&Edit";
        case "ContextMenuExpand":
          return "E&xpand";
        case "ContextMenuNew":
          return "&New";
        case "ContextMenuCopy":
          return "&Copy";
        case "ContextMenuCut":
          return "Cu&t";
        case "ContextMenuPaste":
          return "&Paste";
        default:
          return string.Empty;
      }
    }
  }
}
