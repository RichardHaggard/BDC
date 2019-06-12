// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadPageViewLocalizationProvider : LocalizationProvider<RadPageViewLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "CloseButton":
          return "Close Selected Page";
        case "ItemListButton":
          return "Available Pages";
        case "LeftScrollButton":
          return "Scroll Strip Left";
        case "RightScrollButton":
          return "Scroll Strip Right";
        case "ShowMoreButtonsItemCaption":
          return "Show More Buttons";
        case "ShowFewerButtonsItemCaption":
          return "Show Fewer Buttons";
        case "AddRemoveButtonsItemCaption":
          return "Add or Remove Buttons";
        case "ItemCloseButton":
          return "Close Page";
        case "ItemPinButtonTooltip":
          return "Toggle pin status";
        case "ItemPinButtonPreviewTooltip":
          return "Keep open";
        case "NewItemTooltipText":
          return "Add New Page";
        default:
          return string.Empty;
      }
    }
  }
}
