// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatWeatherCardElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class ChatWeatherCardElement : BaseChatCardElement
  {
    private StackLayoutPanel stackElement;
    private LightVisualElement cityElement;
    private LightVisualElement imageElement;
    private LightVisualElement temperatureElement;
    private LightVisualElement separator;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MaxSize = new Size(250, 0);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stackElement = this.CreateStackElement();
      this.cityElement = this.CreateCityElement();
      this.imageElement = this.CreateImageElement();
      this.temperatureElement = this.CreateTemperatureElement();
      this.separator = this.CreateSeparatorElement();
      this.stackElement.Children.Add((RadElement) this.cityElement);
      this.stackElement.Children.Add((RadElement) this.imageElement);
      this.stackElement.Children.Add((RadElement) this.temperatureElement);
      this.stackElement.Children.Add((RadElement) this.separator);
      this.Children.Add((RadElement) this.stackElement);
    }

    public ChatWeatherCardElement(ChatWeatherCardDataItem item)
      : base((BaseChatCardDataItem) item)
    {
    }

    protected virtual StackLayoutPanel CreateStackElement()
    {
      StackLayoutPanel stackLayoutPanel = new StackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Vertical;
      stackLayoutPanel.NotifyParentOnMouseInput = true;
      return stackLayoutPanel;
    }

    protected virtual LightVisualElement CreateCityElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Padding = new Padding(10, 10, 10, 0);
      lightVisualElement.TextWrap = true;
      lightVisualElement.TextAlignment = ContentAlignment.MiddleCenter;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 16f);
      lightVisualElement.Shape = (ElementShape) new RoundRectShape(20, true, false, true, false);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateImageElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.ImageLayout = ImageLayout.Zoom;
      lightVisualElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateTemperatureElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Padding = new Padding(10, 10, 10, 0);
      lightVisualElement.TextWrap = true;
      lightVisualElement.TextAlignment = ContentAlignment.MiddleCenter;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 24f);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateSeparatorElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.BackColor = Color.Gray;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateDataElement(string text)
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.TextAlignment = ContentAlignment.MiddleCenter;
      lightVisualElement.ShouldHandleMouseInput = false;
      lightVisualElement.Text = text;
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

    public LightVisualElement ImageElement
    {
      get
      {
        return this.imageElement;
      }
      set
      {
        this.imageElement = value;
      }
    }

    public LightVisualElement TemperatureElement
    {
      get
      {
        return this.temperatureElement;
      }
      set
      {
        this.temperatureElement = value;
      }
    }

    public LightVisualElement Separator
    {
      get
      {
        return this.separator;
      }
      set
      {
        this.separator = value;
      }
    }

    protected override void Synchronise()
    {
      base.Synchronise();
      ChatWeatherCardDataItem dataItem = this.DataItem as ChatWeatherCardDataItem;
      this.cityElement.Text = dataItem.City;
      this.ImageElement.Image = dataItem.Image;
      this.TemperatureElement.Text = dataItem.Temperature;
      while (this.StackElement.Children.Count > 3)
        this.StackElement.Children.RemoveAt(3);
      foreach (string text in dataItem.Data)
        this.StackElement.Children.Add((RadElement) this.CreateDataElement(text));
    }
  }
}
