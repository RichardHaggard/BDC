// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FlightCardAirportInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class FlightCardAirportInfo : LightVisualElement
  {
    private StackLayoutPanel stackElement;
    private LightVisualElement cityElement;
    private LightVisualElement airportElement;
    private LightVisualElement dateElement;
    private LightVisualElement timeElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stackElement = this.CreateStackElement();
      this.cityElement = this.CreateCityElement();
      this.airportElement = this.CreateAirportElement();
      this.dateElement = this.CreateDateElement();
      this.timeElement = this.CreateTimeElement();
      this.stackElement.Children.Add((RadElement) this.cityElement);
      this.stackElement.Children.Add((RadElement) this.airportElement);
      this.stackElement.Children.Add((RadElement) this.dateElement);
      this.stackElement.Children.Add((RadElement) this.timeElement);
      this.Children.Add((RadElement) this.stackElement);
    }

    public FlightCardAirportInfo(string city, string airport, string date, string time)
    {
      this.cityElement.Text = city;
      this.airportElement.Text = airport;
      this.dateElement.Text = date;
      this.timeElement.Text = time;
    }

    private StackLayoutPanel CreateStackElement()
    {
      StackLayoutPanel stackLayoutPanel = new StackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Vertical;
      stackLayoutPanel.ShouldHandleMouseInput = false;
      return stackLayoutPanel;
    }

    private LightVisualElement CreateCityElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    private LightVisualElement CreateAirportElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 22f);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    private LightVisualElement CreateDateElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    private LightVisualElement CreateTimeElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    public StackLayoutPanel StackElement
    {
      get
      {
        return this.stackElement;
      }
      set
      {
        this.stackElement = value;
      }
    }

    public LightVisualElement CityElement
    {
      get
      {
        return this.cityElement;
      }
      set
      {
        this.cityElement = value;
      }
    }

    public LightVisualElement AirportElement
    {
      get
      {
        return this.airportElement;
      }
      set
      {
        this.airportElement = value;
      }
    }

    public LightVisualElement DateElement
    {
      get
      {
        return this.dateElement;
      }
      set
      {
        this.dateElement = value;
      }
    }

    public LightVisualElement TimeElement
    {
      get
      {
        return this.timeElement;
      }
      set
      {
        this.timeElement = value;
      }
    }
  }
}
