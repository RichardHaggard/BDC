// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatWeatherCardDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ChatWeatherCardDataItem : BaseChatCardDataItem
  {
    private string city;
    private Image image;
    private string temperature;
    private string[] data;

    public ChatWeatherCardDataItem(
      string city,
      Image image,
      string temperature,
      object userData,
      params string[] data)
      : base(userData)
    {
      this.city = city;
      this.image = image;
      this.temperature = temperature;
      this.data = data;
    }

    public ChatWeatherCardDataItem(
      string city,
      Image image,
      string temperature,
      params string[] data)
      : this(city, image, temperature, (object) null, data)
    {
    }

    public string City
    {
      get
      {
        return this.city;
      }
      set
      {
        if (!(this.city != value))
          return;
        this.city = value;
        this.OnPropertyChanged(nameof (City));
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

    public string Temperature
    {
      get
      {
        return this.temperature;
      }
      set
      {
        if (!(this.temperature != value))
          return;
        this.temperature = value;
        this.OnPropertyChanged(nameof (Temperature));
      }
    }

    public string[] Data
    {
      get
      {
        return this.data;
      }
      set
      {
        if (this.data == value)
          return;
        this.data = value;
        this.OnPropertyChanged(nameof (Data));
      }
    }
  }
}
