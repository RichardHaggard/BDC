// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadMessageLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Localization;

namespace Telerik.WinControls
{
  public class RadMessageLocalizationProvider : LocalizationProvider<RadMessageLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "Abort":
          return "Abort";
        case "Cancel":
          return "Cancel";
        case "Ignore":
          return "Ignore";
        case "No":
          return "No";
        case "OK":
          return "OK";
        case "Retry":
          return "Retry";
        case "Yes":
          return "Yes";
        case "Details":
          return "Details";
        default:
          return string.Empty;
      }
    }
  }
}
