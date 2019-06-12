// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DefaultEncryptionSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  public class DefaultEncryptionSettings : EncryptionSettings
  {
    private string password;

    public DefaultEncryptionSettings()
    {
      this.Algorithm = "DEFAULT";
    }

    public string Password
    {
      get
      {
        return this.password;
      }
      set
      {
        this.password = value;
        this.OnPropertyChanged(nameof (Password));
      }
    }

    internal uint FileTime { get; set; }
  }
}
