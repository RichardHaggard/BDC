// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class CommandBarLocalizationProvider : LocalizationProvider<CommandBarLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "CustomizeDialogChooseToolstripLabelText":
          return "Choose a toolstrip to rearrange:";
        case "CustomizeDialogCloseButtonText":
          return "Close";
        case "CustomizeDialogItemsPageTitle":
          return "Items";
        case "CustomizeDialogMoveDownButtonText":
          return "Move Down";
        case "CustomizeDialogMoveUpButtonText":
          return "Move Up";
        case "CustomizeDialogResetButtonText":
          return "Reset";
        case "CustomizeDialogTitle":
          return "Customize";
        case "CustomizeDialogToolstripsPageTitle":
          return "Toolstrips";
        case "OverflowMenuAddOrRemoveButtonsText":
          return "Add or Remove Buttons";
        case "OverflowMenuCustomizeText":
          return "Customize...";
        case "ContextMenuCustomizeText":
          return "Customize...";
        default:
          return string.Empty;
      }
    }
  }
}
