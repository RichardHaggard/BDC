// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FlightInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class FlightInfo
  {
    private string departureCity;
    private string departureAirport;
    private DateTime departureDateTime;
    private string arrivalCity;
    private string arrivalAirport;
    private DateTime arrivalDateTime;

    public FlightInfo(
      string departureCity,
      string departureAirport,
      DateTime departureDateTime,
      string arrivalCity,
      string arrivalAirport,
      DateTime arrivalDateTime)
    {
      this.departureCity = departureCity;
      this.departureAirport = departureAirport;
      this.departureDateTime = departureDateTime;
      this.arrivalCity = arrivalCity;
      this.arrivalAirport = arrivalAirport;
      this.arrivalDateTime = arrivalDateTime;
    }

    public string DepartureCity
    {
      get
      {
        return this.departureCity;
      }
      set
      {
        this.departureCity = value;
      }
    }

    public string DepartureAirport
    {
      get
      {
        return this.departureAirport;
      }
      set
      {
        this.departureAirport = value;
      }
    }

    public DateTime DepartureDateTime
    {
      get
      {
        return this.departureDateTime;
      }
      set
      {
        this.departureDateTime = value;
      }
    }

    public string ArrivalCity
    {
      get
      {
        return this.arrivalCity;
      }
      set
      {
        this.arrivalCity = value;
      }
    }

    public string ArrivalAirport
    {
      get
      {
        return this.arrivalAirport;
      }
      set
      {
        this.arrivalAirport = value;
      }
    }

    public DateTime ArrivalDateTime
    {
      get
      {
        return this.arrivalDateTime;
      }
      set
      {
        this.arrivalDateTime = value;
      }
    }
  }
}
