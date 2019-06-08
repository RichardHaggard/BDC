// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.EncryptionSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.Zip
{
  public class EncryptionSettings : INotifyPropertyChanged
  {
    private string algorithm;

    public event PropertyChangedEventHandler PropertyChanged;

    public string Algorithm
    {
      get
      {
        return this.algorithm;
      }
      protected set
      {
        this.algorithm = value;
        this.OnPropertyChanged(nameof (Algorithm));
      }
    }

    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
