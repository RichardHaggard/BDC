// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.DefaultKey
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Licensing
{
  internal class DefaultKey : ILicenseKey
  {
    public DefaultKey()
    {
      this.Key = "default@telerik.com";
    }

    public string Key { get; set; }

    public bool IsValid()
    {
      return true;
    }
  }
}
