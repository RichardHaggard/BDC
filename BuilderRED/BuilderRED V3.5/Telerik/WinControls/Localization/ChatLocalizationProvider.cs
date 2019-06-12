// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Localization.ChatLocalizationProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Localization
{
  public class ChatLocalizationProvider : LocalizationProvider<ChatLocalizationProvider>
  {
    public override string GetLocalizedString(string id)
    {
      switch (id)
      {
        case "TypeAMessage":
          return "Type a message";
        case "OverlayOK":
          return "OK";
        case "OverlayCancel":
          return "Cancel";
        case "FlightCardDeparture":
          return "Departure";
        case "FlightCardArrival":
          return "Arrival";
        case "FlightCardPassenger":
          return "Passenger";
        case "FlightCardTotal":
          return "Total";
        default:
          return string.Empty;
      }
    }
  }
}
