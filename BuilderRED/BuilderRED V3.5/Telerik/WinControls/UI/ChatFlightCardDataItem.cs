// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatFlightCardDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ChatFlightCardDataItem : BaseChatCardDataItem
  {
    private string passengerName;
    private IEnumerable<FlightInfo> flights;
    private Image image;
    private string total;

    public ChatFlightCardDataItem(
      string passengerName,
      IEnumerable<FlightInfo> flights,
      Image image,
      string total,
      object userData)
      : base(userData)
    {
      this.passengerName = passengerName;
      this.flights = flights;
      this.image = image;
      this.total = total;
    }

    public string PassangerName
    {
      get
      {
        return this.passengerName;
      }
      set
      {
        if (!(this.passengerName != value))
          return;
        this.passengerName = value;
        this.OnPropertyChanged(nameof (PassangerName));
      }
    }

    public IEnumerable<FlightInfo> Flights
    {
      get
      {
        return this.flights;
      }
      set
      {
        if (this.flights == value)
          return;
        this.flights = value;
        this.OnPropertyChanged(nameof (Flights));
      }
    }

    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        if (this.image == value)
          return;
        this.image = value;
        this.OnPropertyChanged(nameof (Image));
      }
    }

    public string Total
    {
      get
      {
        return this.total;
      }
      set
      {
        if (!(this.total != value))
          return;
        this.total = value;
        this.OnPropertyChanged(nameof (Total));
      }
    }
  }
}
