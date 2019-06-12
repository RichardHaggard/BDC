// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatProductCardDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ChatProductCardDataItem : BaseChatCardDataItem
  {
    private Image image;
    private string title;
    private string rating;
    private string subtitle;
    private string price;
    private IEnumerable<ChatCardAction> actions;

    public ChatProductCardDataItem(
      Image image,
      string title,
      string rating,
      string subtitle,
      string price,
      IEnumerable<ChatCardAction> actions,
      object userData)
      : base(userData)
    {
      this.image = image;
      this.title = title;
      this.rating = rating;
      this.subtitle = subtitle;
      this.price = price;
      this.actions = actions;
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

    public string Title
    {
      get
      {
        return this.title;
      }
      set
      {
        if (!(this.title != value))
          return;
        this.title = value;
        this.OnPropertyChanged(nameof (Title));
      }
    }

    public string Rating
    {
      get
      {
        return this.rating;
      }
      set
      {
        if (!(this.rating != value))
          return;
        this.rating = value;
        this.OnPropertyChanged(nameof (Rating));
      }
    }

    public string Subtitle
    {
      get
      {
        return this.subtitle;
      }
      set
      {
        if (!(this.subtitle != value))
          return;
        this.subtitle = value;
        this.OnPropertyChanged(nameof (Subtitle));
      }
    }

    public string Price
    {
      get
      {
        return this.price;
      }
      set
      {
        if (!(this.price != value))
          return;
        this.price = value;
        this.OnPropertyChanged(nameof (Price));
      }
    }

    public IEnumerable<ChatCardAction> Actions
    {
      get
      {
        return this.actions;
      }
      set
      {
        if (this.actions == value)
          return;
        this.actions = value;
        this.OnPropertyChanged(nameof (Actions));
      }
    }
  }
}
