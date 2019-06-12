// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatFlightCardElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class ChatFlightCardElement : BaseChatCardElement
  {
    private StackLayoutPanel stackElement;
    private LightVisualElement passengerElement;
    private LightVisualElement nameElement;
    private StackLayoutElement topHorizontalStackElement;
    private LightVisualElement departureElement;
    private LightVisualElement arrivalElement;
    private StackLayoutElement totalStackElement;
    private LightVisualElement totalElement;
    private LightVisualElement totalSumElement;
    private Image planeImage;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stackElement = this.CreateStackElement();
      this.passengerElement = this.CreatePassengerElement();
      this.nameElement = this.CreateNameElement();
      this.topHorizontalStackElement = this.CreateTopHorizontalStackElement();
      this.departureElement = this.CreateDepartureElement();
      this.arrivalElement = this.CreateArrivalElement();
      this.totalStackElement = this.CreateTotalStackElement();
      this.totalElement = this.CreateTotalElement();
      this.totalSumElement = this.CreateTotalSumElement();
      this.topHorizontalStackElement.Children.Add((RadElement) this.departureElement);
      this.topHorizontalStackElement.Children.Add((RadElement) this.arrivalElement);
      this.totalStackElement.Children.Add((RadElement) this.totalElement);
      this.totalStackElement.Children.Add((RadElement) this.totalSumElement);
      this.stackElement.Children.Add((RadElement) this.passengerElement);
      this.stackElement.Children.Add((RadElement) this.nameElement);
      this.stackElement.Children.Add((RadElement) this.topHorizontalStackElement);
      this.stackElement.Children.Add((RadElement) this.totalStackElement);
      this.Children.Add((RadElement) this.stackElement);
    }

    public ChatFlightCardElement(ChatFlightCardDataItem item)
      : base((BaseChatCardDataItem) item)
    {
    }

    protected virtual StackLayoutPanel CreateStackElement()
    {
      StackLayoutPanel stackLayoutPanel = new StackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Vertical;
      stackLayoutPanel.ShouldHandleMouseInput = false;
      return stackLayoutPanel;
    }

    protected virtual LightVisualElement CreatePassengerElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("FlightCardPassenger");
      lightVisualElement.Padding = new Padding(10, 10, 10, 0);
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 10f);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateNameElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Padding = new Padding(10, 0, 10, 10);
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 10f);
      lightVisualElement.BorderBoxStyle = BorderBoxStyle.FourBorders;
      lightVisualElement.BorderBottomColor = Color.Gray;
      lightVisualElement.BorderBottomShadowColor = Color.Gray;
      lightVisualElement.BorderTopWidth = 0.0f;
      lightVisualElement.BorderLeftWidth = 0.0f;
      lightVisualElement.BorderRightWidth = 0.0f;
      lightVisualElement.DrawBorder = true;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual StackLayoutElement CreateTopHorizontalStackElement()
    {
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.Orientation = Orientation.Horizontal;
      stackLayoutElement.StretchHorizontally = true;
      stackLayoutElement.ShouldHandleMouseInput = false;
      return stackLayoutElement;
    }

    protected virtual LightVisualElement CreateDepartureElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.StretchHorizontally = true;
      lightVisualElement.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("FlightCardDeparture");
      lightVisualElement.Padding = new Padding(10);
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 10f);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateArrivalElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.StretchHorizontally = true;
      lightVisualElement.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("FlightCardArrival");
      lightVisualElement.Padding = new Padding(10);
      lightVisualElement.TextAlignment = ContentAlignment.MiddleRight;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 10f);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual FlightStackLayoutPanel CreateFlightStackElement()
    {
      FlightStackLayoutPanel stackLayoutPanel = new FlightStackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Horizontal;
      stackLayoutPanel.Margin = new Padding(10, 10, 10, 10);
      stackLayoutPanel.ShouldHandleMouseInput = false;
      return stackLayoutPanel;
    }

    protected virtual FlightCardAirportInfo CreateFlightDepartureElement(
      string city,
      string airport,
      string date,
      string time)
    {
      FlightCardAirportInfo flightCardAirportInfo = new FlightCardAirportInfo(city, airport, date, time);
      flightCardAirportInfo.ShouldHandleMouseInput = false;
      return flightCardAirportInfo;
    }

    protected virtual LightVisualElement CreateFlightImageElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.ImageLayout = ImageLayout.Zoom;
      lightVisualElement.MaxSize = new Size(90, 0);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual FlightCardAirportInfo CreateFlightArrivalElement(
      string city,
      string airport,
      string date,
      string time)
    {
      FlightCardAirportInfo flightCardAirportInfo = new FlightCardAirportInfo(city, airport, date, time);
      flightCardAirportInfo.CityElement.TextAlignment = ContentAlignment.MiddleRight;
      flightCardAirportInfo.AirportElement.TextAlignment = ContentAlignment.MiddleRight;
      flightCardAirportInfo.DateElement.TextAlignment = ContentAlignment.MiddleRight;
      flightCardAirportInfo.TimeElement.TextAlignment = ContentAlignment.MiddleRight;
      flightCardAirportInfo.ShouldHandleMouseInput = false;
      return flightCardAirportInfo;
    }

    protected virtual StackLayoutElement CreateTotalStackElement()
    {
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.Orientation = Orientation.Horizontal;
      stackLayoutElement.StretchHorizontally = true;
      stackLayoutElement.ShouldHandleMouseInput = false;
      return stackLayoutElement;
    }

    protected virtual LightVisualElement CreateTotalElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("FlightCardTotal");
      lightVisualElement.Padding = new Padding(10);
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 10f);
      lightVisualElement.BorderBoxStyle = BorderBoxStyle.FourBorders;
      lightVisualElement.BorderTopColor = Color.Gray;
      lightVisualElement.BorderTopShadowColor = Color.Gray;
      lightVisualElement.BorderBottomWidth = 0.0f;
      lightVisualElement.BorderLeftWidth = 0.0f;
      lightVisualElement.BorderRightWidth = 0.0f;
      lightVisualElement.DrawBorder = true;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateTotalSumElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Padding = new Padding(10);
      lightVisualElement.TextAlignment = ContentAlignment.MiddleRight;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 10f);
      lightVisualElement.BorderBoxStyle = BorderBoxStyle.FourBorders;
      lightVisualElement.BorderTopColor = Color.Gray;
      lightVisualElement.BorderTopShadowColor = Color.Gray;
      lightVisualElement.BorderBottomWidth = 0.0f;
      lightVisualElement.BorderLeftWidth = 0.0f;
      lightVisualElement.BorderRightWidth = 0.0f;
      lightVisualElement.DrawBorder = true;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected override void Synchronise()
    {
      base.Synchronise();
      ChatFlightCardDataItem dataItem = this.DataItem as ChatFlightCardDataItem;
      this.nameElement.Text = dataItem.PassangerName;
      this.planeImage = dataItem.Image;
      this.totalSumElement.Text = dataItem.Total;
      for (int index = 0; index < this.stackElement.Children.Count; ++index)
      {
        if (index < this.stackElement.Children.Count && this.stackElement.Children[index] is FlightStackLayoutPanel)
          this.stackElement.Children.RemoveAt(index);
      }
      int index1 = 3;
      foreach (FlightInfo flight in dataItem.Flights)
      {
        FlightStackLayoutPanel flightStackElement = this.CreateFlightStackElement();
        FlightCardAirportInfo departureElement = this.CreateFlightDepartureElement(flight.DepartureCity, flight.DepartureAirport, flight.DepartureDateTime.ToShortDateString(), flight.DepartureDateTime.ToShortTimeString());
        LightVisualElement flightImageElement = this.CreateFlightImageElement();
        flightImageElement.Image = this.planeImage;
        FlightCardAirportInfo flightArrivalElement = this.CreateFlightArrivalElement(flight.ArrivalCity, flight.ArrivalAirport, flight.ArrivalDateTime.ToShortDateString(), flight.ArrivalDateTime.ToShortTimeString());
        flightStackElement.Children.Add((RadElement) departureElement);
        flightStackElement.Children.Add((RadElement) flightImageElement);
        flightStackElement.Children.Add((RadElement) flightArrivalElement);
        this.stackElement.Children.Insert(index1, (RadElement) flightStackElement);
        ++index1;
      }
    }
  }
}
