// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColorDialogLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class ColorDialogLocalizationProvider : LocalizationProvider<ColorDialogLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "ColorDialogProfessionalTab":
          return "Professional";
        case "ColorDialogWebTab":
          return "Web";
        case "ColorDialogSystemTab":
          return "System";
        case "ColorDialogBasicTab":
          return "Basic";
        case "ColorDialogAddCustomColorButton":
          return "Add Custom Color";
        case "ColorDialogOKButton":
          return "OK";
        case "ColorDialogCancelButton":
          return "Cancel";
        case "ColorDialogNewColorLabel":
          return "New";
        case "ColorDialogCurrentColorLabel":
          return "Current";
        case "ColorDialogCaption":
          return "Color dialog";
        default:
          return id;
      }
    }
  }
}
