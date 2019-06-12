// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public abstract class ChatMessage : INotifyPropertyChanged
  {
    private Author author;
    private object userData;
    private DateTime timeStamp;

    public ChatMessage(Author author, DateTime timeStamp, object userData)
    {
      this.author = author;
      this.userData = userData;
      this.timeStamp = timeStamp;
    }

    public Author Author
    {
      get
      {
        return this.author;
      }
      set
      {
        if (this.author == value)
          return;
        this.author = value;
        this.OnPropertyChanged(nameof (Author));
      }
    }

    public DateTime TimeStamp
    {
      get
      {
        return this.timeStamp;
      }
      set
      {
        if (!(this.timeStamp != value))
          return;
        this.timeStamp = value;
        this.OnPropertyChanged(nameof (TimeStamp));
      }
    }

    public object UserData
    {
      get
      {
        return this.userData;
      }
      set
      {
        if (this.userData == value)
          return;
        this.userData = value;
        this.OnPropertyChanged(nameof (UserData));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
