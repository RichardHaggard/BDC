// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.CompressionSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.Zip
{
  public class CompressionSettings : INotifyPropertyChanged
  {
    private CompressionMethod compressionMethod;

    public event PropertyChangedEventHandler PropertyChanged;

    public CompressionMethod Method
    {
      get
      {
        return this.compressionMethod;
      }
      protected set
      {
        this.compressionMethod = value;
        this.OnPropertyChanged(nameof (Method));
      }
    }

    internal virtual void CopyFrom(CompressionSettings baseSettings)
    {
    }

    internal virtual void PrepareForZip(CentralDirectoryHeader header = null)
    {
    }

    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
