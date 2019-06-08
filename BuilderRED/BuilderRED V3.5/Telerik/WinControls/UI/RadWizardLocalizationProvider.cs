// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWizardLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadWizardLocalizationProvider : LocalizationProvider<RadWizardLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "BackButtonText":
          return "<   Back";
        case "NextButtonText":
          return "Next   >";
        case "CancelButtonText":
          return "Cancel";
        case "FinishButtonText":
          return "Finish";
        case "HelpButtonText":
          return "<html><u>Help</u></html>";
        default:
          return string.Empty;
      }
    }
  }
}
