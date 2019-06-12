// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseChatCardDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class BaseChatCardDataItem : INotifyPropertyChanged
  {
    private object userData;

    public BaseChatCardDataItem(object userData)
    {
      this.userData = userData;
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

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
