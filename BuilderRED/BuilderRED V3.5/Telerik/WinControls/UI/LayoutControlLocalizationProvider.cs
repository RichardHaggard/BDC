// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class LayoutControlLocalizationProvider : LocalizationProvider<LayoutControlLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "CustomizeDialogHiddenItems":
          return "Hidden Items ({0})";
        case "CustomizeDialogNewItems":
          return "New Items ({0})";
        case "CustomizeDialogPageItems":
          return "Items";
        case "CustomizeDialogPageStructure":
          return "Structure";
        case "CustomizeDialogRootItem":
          return "Root";
        case "CustomizeDialogLayoutItem":
          return "Layout Item";
        case "CustomizeDialogLabelItem":
          return "Label Item";
        case "CustomizeDialogSplitterItem":
          return "Splitter Item";
        case "CustomizeDialogSeparatorItem":
          return "Separator Item";
        case "CustomizeDialogGroupItem":
          return "Group Item";
        case "CustomizeDialogTabbedGroup":
          return "Tabbed Group";
        case "CustomizeDialogSaveLayout":
          return "Save Layout";
        case "CustomizeDialogLoadLayout":
          return "Load Layout";
        case "NewGroupDefaultText":
          return "Item Group";
        case "NewLabelDefaultText":
          return "Label Item";
        case "CustomizeDialogNewItemsEmptySpace":
          return "Empty Space";
        case "CustomizeDialogNewItemsLabel":
          return "Label";
        case "CustomizeDialogNewItemsSeparator":
          return "Separator";
        case "CustomizeDialogNewItemsSplitter":
          return "Splitter";
        case "CustomizeDialogNewItemsGroup":
          return "Group";
        case "CustomizeDialogNewItemsTabbedGroup":
          return "Tabbed Group";
        case "ContextMenuCustomizeText":
          return "Customize";
        case "ContextMenuHideItemText":
          return "Hide";
        case "CustomizeDialogText":
          return "Customize";
        case "ErrorBoxText":
          return "Error!";
        case "ErrorFileNotFoundText":
          return "File not found!";
        case "ErrorLoadingLayoutText":
          return "Error loading layout!";
        default:
          return id;
      }
    }
  }
}
